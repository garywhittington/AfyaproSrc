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
    public partial class frmCUSCustomerGroupDiagnoses : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsClientGroups pMdtClientGroups;
        private AfyaPro_MT.clsDiagnoses pMdtDiagnoses;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private string pGroupCode = "";
        private DataTable pDtDiagnoses = new DataTable("diagnoses");

        private bool pCheckedCheckBox = false;
        private bool pCheckedGrid = false;

        #endregion

        #region frmCUSCustomerGroupDiagnoses
        public frmCUSCustomerGroupDiagnoses(string mGroupCode)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmBILPayBillsGroups";
            try
            {
                this.Icon = Program.gMdiForm.Icon;
                pGroupCode = mGroupCode;

                pMdtClientGroups = (AfyaPro_MT.clsClientGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsClientGroups),
                    Program.gMiddleTier + "clsClientGroups");

                pMdtDiagnoses = (AfyaPro_MT.clsDiagnoses)Activator.GetObject(
                    typeof(AfyaPro_MT.clsDiagnoses),
                    Program.gMiddleTier + "clsDiagnoses");

                pDtDiagnoses.Columns.Add("selected", typeof(System.Boolean));
                pDtDiagnoses.Columns.Add("code", typeof(System.String));
                pDtDiagnoses.Columns.Add("description", typeof(System.String));

                DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit mCheckEdit =
                    new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
                mCheckEdit.CheckedChanged += new EventHandler(mCheckEdit_CheckedChanged);
                viewCUSCustomerGroupDiagnoses.Columns["selected"].ColumnEdit = mCheckEdit;

                grdCUSCustomerGroupDiagnoses.DataSource = pDtDiagnoses;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmCUSCustomerGroupDiagnoses_Load
        private void frmCUSCustomerGroupDiagnoses_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCUSCustomerGroupDiagnoses_Load";

            try
            {
                this.Fill_Diagnoses();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_Diagnoses
        private void Fill_Diagnoses()
        {
            string mFunctionName = "Fill_Diagnoses";

            try
            {
                DataTable mDtDiagnoses = pMdtDiagnoses.View("", "", "", "");
                DataTable mDtGroupDiagnoses = pMdtClientGroups.View_Diagnoses(
                    "groupcode='" + pGroupCode.Trim() + "'", "", "", "");

                DataView mDvGroupDiagnoses = new DataView();
                mDvGroupDiagnoses.Table = mDtGroupDiagnoses;
                mDvGroupDiagnoses.Sort = "diagnosiscode";

                pDtDiagnoses.Rows.Clear();
                foreach (DataRow mDataRow in mDtDiagnoses.Rows)
                {
                    bool mSelected = false;

                    int mRowIndex = mDvGroupDiagnoses.Find(mDataRow["code"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mSelected = true;
                    }

                    DataRow mNewRow = pDtDiagnoses.NewRow();
                    mNewRow["selected"] = mSelected;
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtDiagnoses.Rows.Add(mNewRow);
                    pDtDiagnoses.AcceptChanges();
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
                foreach (DataRow mDataRow in pDtDiagnoses.Rows)
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
                grdCUSCustomerGroupDiagnoses.FocusedView.PostEditor();

                int mChecked = 0;
                int mUnChecked = 0;

                foreach (DataRow mDataRow in pDtDiagnoses.Rows)
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

                if (mChecked == pDtDiagnoses.Rows.Count)
                {
                    chkAll.CheckState = CheckState.Checked;
                }
                else if (mUnChecked == pDtDiagnoses.Rows.Count)
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

        #region grdCUSCustomerGroupDiagnoses_Enter
        private void grdCUSCustomerGroupDiagnoses_Enter(object sender, EventArgs e)
        {
            pCheckedCheckBox = false;
            pCheckedGrid = true;
        }
        #endregion

        #region cmdSearch_Click
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            frmSearchDiagnosis mSearchDiagnosis = new frmSearchDiagnosis(txtCode, false, null);
            mSearchDiagnosis.ShowDialog();
        }
        #endregion

        #region txtCode_EditValueChanged
        private void txtCode_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Columns.GridColumn mColumn = viewCUSCustomerGroupDiagnoses.Columns["code"];

            int mRowHandle = 0;
            bool mRowFound = false;
            while (mRowFound == false)
            {
                mRowHandle = viewCUSCustomerGroupDiagnoses.LocateByValue(mRowHandle, mColumn, txtCode.Text);
                if (mRowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    DataRow mSelectedRow = viewCUSCustomerGroupDiagnoses.GetDataRow(mRowHandle);

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
                viewCUSCustomerGroupDiagnoses.FocusedRowHandle = mRowHandle;
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
                pResult = pMdtClientGroups.Save_Diagnoses(pGroupCode, pDtDiagnoses);

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