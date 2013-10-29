/*
This file is part of AfyaPro.

	Copyright (C) 2013 AfyaPro Foundation.

    AfyaPro is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    AfyaPro is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with AfyaPro.  If not, see <http://www.gnu.org/licenses/>.

*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AfyaPro_NextGen
{
    public partial class frmCUSCustomerGroupItems : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsClientGroups pMdtClientGroups;
        private AfyaPro_MT.clsBillItems pMdtBillItems;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private string pGroupCode = "";
        private string pPriceFieldName = "";
        private DataTable pDtItems = new DataTable("items");

        private bool pCheckedCheckBox = false;
        private bool pCheckedGrid = false;

        #endregion

        #region frmCUSCustomerGroupItems
        public frmCUSCustomerGroupItems(string mGroupCode, string mPriceFieldName, string mPriceDesc)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmBILPayBillsGroups";
            try
            {
                this.Icon = Program.gMdiForm.Icon;
                pGroupCode = mGroupCode;
                pPriceFieldName = mPriceFieldName;
                price.Caption = mPriceDesc;

                pMdtClientGroups = (AfyaPro_MT.clsClientGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsClientGroups),
                    Program.gMiddleTier + "clsClientGroups");

                pMdtBillItems = (AfyaPro_MT.clsBillItems)Activator.GetObject(
                    typeof(AfyaPro_MT.clsBillItems),
                    Program.gMiddleTier + "clsBillItems");

                pDtItems.Columns.Add("selected", typeof(System.Boolean));
                pDtItems.Columns.Add("code", typeof(System.String));
                pDtItems.Columns.Add("description", typeof(System.String));
                pDtItems.Columns.Add("price", typeof(System.Double));
                pDtItems.Columns.Add("percent", typeof(System.Double));

                DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit mCheckEdit =
                    new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
                mCheckEdit.CheckedChanged += new EventHandler(mCheckEdit_CheckedChanged);
                viewCUSCustomerGroupItems.Columns["selected"].ColumnEdit = mCheckEdit;

                grdCUSCustomerGroupItems.DataSource = pDtItems;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmCUSCustomerGroupItems_Load
        private void frmCUSCustomerGroupItems_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCUSCustomerGroupItems_Load";

            try
            {
                this.Fill_Items();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_Items
        private void Fill_Items()
        {
            string mFunctionName = "Fill_Items";

            try
            {
                DataTable mDtItems = pMdtBillItems.View("", "", "", "");
                DataTable mDtGroupItems = pMdtClientGroups.View_Items(
                    "groupcode='" + pGroupCode.Trim() + "'", "", "", "");

                DataView mDvGroupItems = new DataView();
                mDvGroupItems.Table = mDtGroupItems;
                mDvGroupItems.Sort = "itemcode";

                pDtItems.Rows.Clear();
                foreach (DataRow mDataRow in mDtItems.Rows)
                {
                    bool mSelected = false;
                    double mPercent = 100;

                    int mRowIndex = mDvGroupItems.Find(mDataRow["code"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mSelected = true;
                        mPercent = Convert.ToDouble(mDvGroupItems[mRowIndex]["pricingpercent"]);
                    }

                    DataRow mNewRow = pDtItems.NewRow();
                    mNewRow["selected"] = mSelected;
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    mNewRow["price"] = Convert.ToDouble(mDataRow[pPriceFieldName]);
                    mNewRow["percent"] = mPercent;
                    pDtItems.Rows.Add(mNewRow);
                    pDtItems.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region chkAll_CheckedChanged
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (pCheckedCheckBox == true)
            {
                foreach (DataRow mDataRow in pDtItems.Rows)
                {
                    mDataRow["selected"] = chkAll.Checked;
                }
            }
        }
        #endregion

        #region mCheckEdit_CheckedChanged
        void mCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (pCheckedGrid == true)
            {
                grdCUSCustomerGroupItems.FocusedView.PostEditor();

                int mChecked = 0;
                int mUnChecked = 0;

                foreach (DataRow mDataRow in pDtItems.Rows)
                {
                    if (Convert.ToBoolean(mDataRow["selected"]) == true)
                    {
                        mChecked++;
                    }
                    else
                    {
                        mUnChecked++;
                    }
                }

                if (mChecked == pDtItems.Rows.Count)
                {
                    chkAll.CheckState = CheckState.Checked;
                }
                else if (mUnChecked == pDtItems.Rows.Count)
                {
                    chkAll.CheckState = CheckState.Unchecked;
                }
                else
                {
                    chkAll.CheckState = CheckState.Indeterminate;
                }
            }
        }
        #endregion

        #region chkAll_Enter
        private void chkAll_Enter(object sender, EventArgs e)
        {
            pCheckedGrid = false;
            pCheckedCheckBox = true;
        }
        #endregion

        #region grdCUSCustomerGroupItems_Enter
        private void grdCUSCustomerGroupItems_Enter(object sender, EventArgs e)
        {
            pCheckedCheckBox = false;
            pCheckedGrid = true;
        }
        #endregion

        #region cmdApply_Click
        private void cmdApply_Click(object sender, EventArgs e)
        {
            if (Program.IsMoney(txtPercent.Text) == false)
            {
                return;
            }

            foreach (DataRow mDataRow in pDtItems.Rows)
            {
                if (Convert.ToBoolean(mDataRow["selected"]) == true)
                {
                    mDataRow.BeginEdit();
                    mDataRow["percent"] = Convert.ToDouble(txtPercent.Text);
                    mDataRow.EndEdit();
                    pDtItems.AcceptChanges();
                }
            }
        }
        #endregion

        #region cmdSearch_Click
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            frmSearchBillingItem mSearchBillingItem = new frmSearchBillingItem(txtCode, false, null, "");
            mSearchBillingItem.ShowDialog();
        }
        #endregion

        #region txtCode_EditValueChanged
        private void txtCode_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Columns.GridColumn mColumn = viewCUSCustomerGroupItems.Columns["code"];

            int mRowHandle = 0;
            bool mRowFound = false;
            while (mRowFound == false)
            {
                mRowHandle = viewCUSCustomerGroupItems.LocateByValue(mRowHandle, mColumn, txtCode.Text);
                if (mRowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    DataRow mSelectedRow = viewCUSCustomerGroupItems.GetDataRow(mRowHandle);

                    mSelectedRow.BeginEdit();
                    mSelectedRow["selected"] = !Convert.ToBoolean(mSelectedRow["selected"]);
                    mSelectedRow.EndEdit();

                    mRowFound = true;
                    break;
                }

                mRowHandle++;
            }

            if (mRowFound == true)
            {
                viewCUSCustomerGroupItems.FocusedRowHandle = mRowHandle;
            }
        }
        #endregion

        #region cmdSave_Click
        private void cmdSave_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdSave_Click";

            try
            {
                //edit 
                pResult = pMdtClientGroups.Save_Items(pGroupCode, pDtItems);

                if (pResult.Exe_Result == 0)
                {
                    Program.Display_Error(pResult.Exe_Message);
                    return;
                }
                if (pResult.Exe_Result == -1)
                {
                    Program.Display_Server_Error(pResult.Exe_Message);
                    return;
                }

                Program.Display_Info(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SettingsSavedSuccessfully.ToString());
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}