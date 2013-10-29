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
using DevExpress.XtraBars;
using DevExpress.XtraGrid;

namespace AfyaPro_NextGen
{
    public partial class frmCUSCustomerGroups : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsClientGroups pMdtClientGroups;
        private AfyaPro_MT.clsPriceCategories pMdtPriceCategories;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataRow pSelectedRow = null;
        internal string gDataState = "";
        private bool pFirstTimeLoad = true;
        private DataTable pDtPriceCategories = new DataTable("pricecategories");

        #endregion

        #region frmCUSCustomerGroups
        public frmCUSCustomerGroups()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCUSCustomerGroups";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                
                this.CancelButton = cmdClose;

                pMdtPriceCategories = (AfyaPro_MT.clsPriceCategories)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPriceCategories),
                    Program.gMiddleTier + "clsPriceCategories");

                pMdtClientGroups = (AfyaPro_MT.clsClientGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsClientGroups),
                    Program.gMiddleTier + "clsClientGroups");

                pMdtAutoCodes = (AfyaPro_MT.clsAutoCodes)Activator.GetObject(
                    typeof(AfyaPro_MT.clsAutoCodes),
                    Program.gMiddleTier + "clsAutoCodes");

                pDtPriceCategories.Columns.Add("pricename", typeof(System.String));
                pDtPriceCategories.Columns.Add("pricedescription", typeof(System.String));
                cboPriceCategory.Properties.DataSource = pDtPriceCategories;
                cboPriceCategory.Properties.DisplayMember = "pricedescription";
                cboPriceCategory.Properties.ValueMember = "pricename";

                this.Fill_LookupData();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmCUSCustomerGroups_Load
        private void frmCUSCustomerGroups_Load(object sender, EventArgs e)
        {
            switch (gDataState.Trim().ToLower())
            {
                case "new": Mode_New(); break;
                case "edit": Mode_Edit(); break;
            }

            this.Load_Controls();
        }
        #endregion

        #region frmCUSCustomerGroups_Activated
        private void frmCUSCustomerGroups_Activated(object sender, EventArgs e)
        {
            if (pFirstTimeLoad == true)
            {
                if (gDataState.Trim().ToLower() == "new")
                {
                    if (txtCode.Text.Trim().ToLower() == "<<---new--->>")
                    {
                        txtDescription.Focus();
                    }
                    else
                    {
                        txtCode.Focus();
                    }
                }
                else
                {
                    txtDescription.Focus();
                }
                pFirstTimeLoad = false;
            }
        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            DataRow mNewRow;
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region pricecategories
                
                pDtPriceCategories.Rows.Clear();
                DataTable mDtPriceCategories = pMdtPriceCategories.View_Active(Program.gLanguageName, "frmBLSPriceCategories");
                foreach (DataRow mDataRow in mDtPriceCategories.Rows)
                {
                    mNewRow = pDtPriceCategories.NewRow();
                    mNewRow["pricename"] = mDataRow["pricename"].ToString();
                    mNewRow["pricedescription"] = mDataRow["pricedescription"].ToString();
                    pDtPriceCategories.Rows.Add(mNewRow);
                    pDtPriceCategories.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtPriceCategories.Columns)
                {
                    mDataColumn.Caption = mDtPriceCategories.Columns[mDataColumn.ColumnName].Caption;
                }

                #endregion
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(txbCode);
            mObjectsList.Add(txbDescription);
            mObjectsList.Add(grpProperties);
            mObjectsList.Add(chkInActive);
            mObjectsList.Add(chkHasSubGroups);
            mObjectsList.Add(chkIdSensitive);
            mObjectsList.Add(chkStrictActivation);
            mObjectsList.Add(chkHasCeiling);
            mObjectsList.Add(txbCeilingAmount);
            mObjectsList.Add(txbPriceCategory);
            mObjectsList.Add(chkPromptPayment);
            mObjectsList.Add(cmdOk);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region Grid_Settings
        internal void Grid_Settings(GridControl mGridControl)
        {
            string mFunctionName = "Grid_Settings";

            try
            {
                if (mGridControl.Visible == false)
                {
                    mGridControl.Visible = true;
                }

                mGridControl.DataSource = null;

                //prepare grid view
                DevExpress.XtraGrid.Views.Grid.GridView mGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
                mGridView.OptionsBehavior.Editable = false;
                mGridView.OptionsView.ShowGroupPanel = false;
                mGridControl.MainView = mGridView;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Mode_New
        private void Mode_New()
        {
            String mFunctionName = "Mode_New";

            try
            {
                Int16 mGenerateCode = pMdtAutoCodes.Auto_Generate_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.clientgroupcode));
                if (mGenerateCode == -1)
                {
                    Program.Display_Server_Error("");
                    return;
                }

                txtCode.Text = "";
                this.Data_Clear();

                if (mGenerateCode == 1)
                {
                    txtCode.Text = "<<---New--->>";
                    txtCode.Enabled = false;
                    txtDescription.Focus();
                }
                else
                {
                    txtCode.Enabled = true;
                    txtCode.Focus();
                }

                gDataState = "New";
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Mode_Edit
        private void Mode_Edit()
        {
            string mFunctionName = "Mode_Edit";

            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                this.Data_Clear();
                pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                txtCode.Text = pSelectedRow["code"].ToString();
                txtDescription.Text = pSelectedRow["description"].ToString();
                chkInActive.Checked = Convert.ToBoolean(pSelectedRow["inactive"]);
                chkHasSubGroups.Checked = Convert.ToBoolean(pSelectedRow["hassubgroups"]);
                chkIdSensitive.Checked = Convert.ToBoolean(pSelectedRow["hasid"]);
                chkStrictActivation.Checked = Convert.ToBoolean(pSelectedRow["strictactivation"]);
                chkHasCeiling.Checked = Convert.ToBoolean(pSelectedRow["hasceiling"]);
                chkPromptPayment.Checked = Convert.ToBoolean(pSelectedRow["promptpayment"]);
                chkItemsSensitive.Checked = Convert.ToBoolean(pSelectedRow["itemssensitive"]);
                chkDiagnosesSensitive.Checked = Convert.ToBoolean(pSelectedRow["diagnosessensitive"]);
                chkGenerateInvoice.Checked = Convert.ToBoolean(pSelectedRow["generateinvoicewhenpreparingbill"]);
                txtCeilingAmount.Text = pSelectedRow["ceilingamount"].ToString();
                cboPriceCategory.ItemIndex = Program.Get_LookupItemIndex(cboPriceCategory, "pricename", pSelectedRow["pricecategory"].ToString());

                gDataState = "Edit";
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Data_Clear
        private void Data_Clear()
        {
            txtDescription.Text = "";
            chkInActive.Checked = false;
            chkHasSubGroups.Checked = false;
            chkIdSensitive.Checked = false;
            chkStrictActivation.Checked = false;
            chkHasCeiling.Checked = false;
            txtCeilingAmount.Text = "";
            chkPromptPayment.Checked = false;
            chkItemsSensitive.Checked = false;
            chkDiagnosesSensitive.Checked = false;
            chkGenerateInvoice.Checked = false;
            cboPriceCategory.EditValue = null;
        }
        #endregion

        #region Data_Fill
        internal void Data_Fill(GridControl mGridControl)
        {
            string mFunctionName = "Data_Fill";

            try
            {
                //load data
                DataTable mDtCountries = pMdtClientGroups.View("", "", Program.gLanguageName, mGridControl.Name);
                mGridControl.DataSource = mDtCountries;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Data_New
        private void Data_New()
        {
            double mCeilingAmount = 0;
            Int16 mGenerateCode = 0;
            String mFunctionName = "Data_New";

            #region validation
            if (txtCode.Text.Trim() == "" && txtCode.Text.Trim().ToLower() != "<<---new--->>")
            {
                Program.Display_Error("Invalid code");
                txtCode.Focus();
                return;
            }

            if (txtDescription.Text.Trim() == "")
            {
                Program.Display_Error("Invalid description");
                txtDescription.Focus();
                return;
            }
            #endregion

            try
            {
                if (txtCode.Text.Trim().ToLower() == "<<---new--->>")
                {
                    mGenerateCode = 1;
                }

                if (Program.IsMoney(txtCeilingAmount.Text) == true)
                {
                    if (chkHasCeiling.Checked == true)
                    {
                        mCeilingAmount = Convert.ToDouble(txtCeilingAmount.Text);
                    }
                }

                //add 
                pResult = pMdtClientGroups.Add(mGenerateCode, txtCode.Text, txtDescription.Text,
                Convert.ToInt16(chkIdSensitive.Checked), Convert.ToInt16(chkHasCeiling.Checked),
                mCeilingAmount, Convert.ToInt16(chkPromptPayment.Checked), 
                cboPriceCategory.GetColumnValue("pricename").ToString().Trim(),
                Convert.ToInt16(chkHasSubGroups.Checked), Convert.ToInt16(chkStrictActivation.Checked),
                Convert.ToInt16(chkItemsSensitive.Checked), Convert.ToInt16(chkDiagnosesSensitive.Checked),
                Convert.ToInt16(chkGenerateInvoice.Checked), Convert.ToInt16(chkInActive.Checked));
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

                //refresh
                this.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                this.Mode_New();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Data_Edit
        private void Data_Edit()
        {
            double mCeilingAmount = 0;
            String mFunctionName = "Data_Edit";

            #region validation
            if (txtCode.Text.Trim() == "")
            {
                Program.Display_Error("Invalid code");
                txtCode.Focus();
                return;
            }

            if (txtDescription.Text.Trim() == "")
            {
                Program.Display_Error("Invalid description");
                txtDescription.Focus();
                return;
            }
            #endregion

            try
            {
                if (Program.IsMoney(txtCeilingAmount.Text) == true)
                {
                    if (chkHasCeiling.Checked == true)
                    {
                        mCeilingAmount = Convert.ToDouble(txtCeilingAmount.Text);
                    }
                }

                //edit 
                pResult = pMdtClientGroups.Edit(txtCode.Text, txtDescription.Text,
                Convert.ToInt16(chkIdSensitive.Checked), Convert.ToInt16(chkHasCeiling.Checked),
                mCeilingAmount, Convert.ToInt16(chkPromptPayment.Checked),
                cboPriceCategory.GetColumnValue("pricename").ToString().Trim(),
                Convert.ToInt16(chkHasSubGroups.Checked), Convert.ToInt16(chkStrictActivation.Checked),
                Convert.ToInt16(chkItemsSensitive.Checked), Convert.ToInt16(chkDiagnosesSensitive.Checked),
                Convert.ToInt16(chkGenerateInvoice.Checked), Convert.ToInt16(chkInActive.Checked));

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

                //refresh
                this.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                this.Close();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Data_Delete
        internal void Data_Delete()
        {
            String mFunctionName = "Data_Delete";

            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                DialogResult mResp = Program.Confirm_Deletion(pSelectedRow["code"].ToString().Trim() + "'   '"
                    + pSelectedRow["description"].ToString().Trim() + "'");

                if (mResp != DialogResult.Yes)
                {
                    return;
                }

                //add 
                pResult = pMdtClientGroups.Delete(pSelectedRow["code"].ToString().Trim());
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

                //refresh
                this.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdOk_Click
        private void cmdOk_Click(object sender, EventArgs e)
        {
            switch (gDataState.Trim().ToLower())
            {
                case "new": this.Data_New(); break;
                case "edit": this.Data_Edit(); break;
            }
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region cmdItems_Click
        private void cmdItems_Click(object sender, EventArgs e)
        {
            string mGroupCode = txtCode.Text.Trim();
            string mPriceFieldName = cboPriceCategory.GetColumnValue("pricename").ToString().Trim();
            string mPriceDesc = cboPriceCategory.GetColumnValue("pricedescription").ToString().Trim();

            frmCUSCustomerGroupItems mCUSCustomerGroupItems =
                new frmCUSCustomerGroupItems(mGroupCode, mPriceFieldName, mPriceDesc);
            mCUSCustomerGroupItems.ShowDialog();
        }
        #endregion

        #region cmdDiagnoses_Click
        private void cmdDiagnoses_Click(object sender, EventArgs e)
        {
            string mGroupCode = txtCode.Text.Trim();

            frmCUSCustomerGroupDiagnoses mCUSCustomerGroupDiagnoses = new frmCUSCustomerGroupDiagnoses(mGroupCode);
            mCUSCustomerGroupDiagnoses.ShowDialog();
        }
        #endregion
    }
}