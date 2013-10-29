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
    public partial class frmMTUDiagnosesMapping : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsMtuhaDiagnoses pMdtMtuhaDiagnoses;
        private AfyaPro_MT.clsDiagnoses pMdtDiagnoses;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataRow pSelectedRow = null;

        private bool pCheckedCheckBox = false;
        private bool pCheckedGrid = false;

        private DataTable pDtDiagnoses = new DataTable("diagnoses");
        private DataTable pDtDiagnosesMapped = new DataTable("diagnosesmapped");

        #endregion

        #region frmMTUDiagnosesMapping
        public frmMTUDiagnosesMapping()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmMTUDiagnosesMapping";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                this.CancelButton = cmdClose;

                pMdtMtuhaDiagnoses = (AfyaPro_MT.clsMtuhaDiagnoses)Activator.GetObject(
                    typeof(AfyaPro_MT.clsMtuhaDiagnoses),
                    Program.gMiddleTier + "clsMtuhaDiagnoses");

                pMdtDiagnoses = (AfyaPro_MT.clsDiagnoses)Activator.GetObject(
                    typeof(AfyaPro_MT.clsDiagnoses),
                    Program.gMiddleTier + "clsDiagnoses");

                pDtDiagnoses.Columns.Add("selected", typeof(System.Boolean));
                pDtDiagnoses.Columns.Add("code", typeof(System.String));
                pDtDiagnoses.Columns.Add("description", typeof(System.String));

                pDtDiagnosesMapped.Columns.Add("selected", typeof(System.Boolean));
                pDtDiagnosesMapped.Columns.Add("code", typeof(System.String));
                pDtDiagnosesMapped.Columns.Add("description", typeof(System.String));

                grdDiagnoses.DataSource = pDtDiagnoses;
                grdDiagnosesMapped.DataSource = pDtDiagnosesMapped;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmMTUDiagnosesMapping_Load
        private void frmMTUDiagnosesMapping_Load(object sender, EventArgs e)
        {
            this.Load_Controls();
            this.Mode_Edit();

            radSearchBy.SelectedIndex = 1;
        }
        #endregion

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(txbCode);
            mObjectsList.Add(txbDescription);
            mObjectsList.Add(txbSearchBy);
            mObjectsList.Add(radSearchBy.Properties.Items[0]);
            mObjectsList.Add(radSearchBy.Properties.Items[1]);
            mObjectsList.Add(txbOptions);
            mObjectsList.Add(radOptions.Properties.Items[0]);
            mObjectsList.Add(radOptions.Properties.Items[1]);
            mObjectsList.Add(radOptions.Properties.Items[2]);
            mObjectsList.Add(radOptions.Properties.Items[3]);
            mObjectsList.Add(txbSearch);
            mObjectsList.Add(cmdSearch);
            mObjectsList.Add(grpUnmapped);
            mObjectsList.Add(grpMapped);
            mObjectsList.Add(cmdMap);
            mObjectsList.Add(cmdUnmap);
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

                DataTable mDtDiagnosesMapped = pMdtMtuhaDiagnoses.View_Mapping("mtuhacode='" + txtCode.Text.Trim() + "'",
                    "", Program.gLanguageName, "");
                pDtDiagnosesMapped.Rows.Clear();

                string mDiagnosesFilter = "";
                foreach (DataRow mDataRow in mDtDiagnosesMapped.Rows)
                {
                    if (mDiagnosesFilter.Trim() == "")
                    {
                        mDiagnosesFilter = "'" + mDataRow["diagnosiscode"].ToString().Trim() + "'";
                    }
                    else
                    {
                        mDiagnosesFilter = mDiagnosesFilter + ",'" + mDataRow["diagnosiscode"].ToString().Trim() + "'";
                    }
                }

                if (mDiagnosesFilter.Trim() == "")
                {
                    mDiagnosesFilter = "1=2";
                }
                else
                {
                    mDiagnosesFilter = "code in (" + mDiagnosesFilter + ")";
                }

                DataTable mDtDiagnoses = pMdtDiagnoses.View(mDiagnosesFilter, "description", Program.gLanguageName, "");

                foreach (DataRow mDataRow in mDtDiagnoses.Rows)
                {
                    DataRow mNewRow = pDtDiagnosesMapped.NewRow();
                    mNewRow["selected"] = false;
                    mNewRow["code"] = mDataRow["code"];
                    mNewRow["description"] = mDataRow["description"];
                    pDtDiagnosesMapped.Rows.Add(mNewRow);
                    pDtDiagnosesMapped.AcceptChanges();
                }

                txtSearchText.Focus();
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
        }
        #endregion

        #region Data_Fill
        internal void Data_Fill(GridControl mGridControl)
        {
            string mFunctionName = "Data_Fill";

            try
            {
                //load data
                DataTable mDtDiagnoses = pMdtMtuhaDiagnoses.View("", "", Program.gLanguageName, mGridControl.Name);
                mGridControl.DataSource = mDtDiagnoses;
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
            String mFunctionName = "Data_Edit";

            #region validation
            if (txtCode.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.MTU_DiagnosisCodeIsInvalid.ToString());
                txtCode.Focus();
                return;
            }

            if (txtDescription.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.MTU_DiagnosisDescriptionIsInvalid.ToString());
                txtDescription.Focus();
                return;
            }
            #endregion

            try
            {
                //add 
                pResult = pMdtMtuhaDiagnoses.Mtuha_Diagnoses_Mapping(txtCode.Text, 
                    txtDescription.Text, pDtDiagnosesMapped, Program.gCurrentUser.Code);
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
                Program.Display_Info(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SettingsSavedSuccessfully.ToString());
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Search
        private void Search()
        {
            string mFunctionName = "Search";

            try
            {
                Program.gMdiForm.Cursor = Cursors.WaitCursor;

                string mFieldName = "code";
                if (radSearchBy.SelectedIndex == 1)
                {
                    mFieldName = "description";
                }
                string mSearchText = txtSearchText.Text.Trim();
                string mRowFilter = "";
                pDtDiagnoses.Clear();

                if (txtSearchText.Text.Trim() == "")
                {
                    mSearchText = "1=2";
                }

                switch (radOptions.SelectedIndex)
                {
                    case 0: mRowFilter = mFieldName + " like '%" + mSearchText + "%'"; break;
                    case 1: mRowFilter = mFieldName + " like '" + mSearchText + "%'"; break;
                    case 2: mRowFilter = mFieldName + " like '%" + mSearchText + "'"; break;
                    case 3: mRowFilter = mFieldName + " = '" + mSearchText + "'"; break;
                }

                mRowFilter = "(" + mRowFilter + ")";

                DataTable mDtDiagnoses = pMdtDiagnoses.View(mRowFilter, mFieldName, Program.gLanguageName, "");

                DataView mDvDiagnosesMapped = new DataView();
                mDvDiagnosesMapped.Table = pDtDiagnosesMapped;
                mDvDiagnosesMapped.Sort = "code";

                foreach (DataRow mDataRow in mDtDiagnoses.Rows)
                {
                    if (mDvDiagnosesMapped.Find(mDataRow["code"].ToString().Trim()) == -1)
                    {
                        DataRow mNewRow = pDtDiagnoses.NewRow();
                        mNewRow["selected"] = false;
                        mNewRow["code"] = mDataRow["code"];
                        mNewRow["description"] = mDataRow["description"];
                        pDtDiagnoses.Rows.Add(mNewRow);
                        pDtDiagnoses.AcceptChanges();
                    }
                }

                Program.gMdiForm.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Program.gMdiForm.Cursor = Cursors.Default;
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region txtSearchText_KeyDown
        private void txtSearchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Search();
            }
        }
        #endregion

        #region cmdSearch_Click
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            this.Search();
        }
        #endregion

        #region cmdMap_Click
        private void cmdMap_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdMap_Click";

            try
            {
                if (viewDiagnoses.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow mSelectedRow = viewDiagnoses.GetDataRow(viewDiagnoses.FocusedRowHandle);

                DataView mDvSelected = new DataView();
                mDvSelected.Table = pDtDiagnosesMapped;
                mDvSelected.Sort = "code";

                int mRowIndex = mDvSelected.Find(mSelectedRow["code"].ToString().Trim());

                if (mRowIndex < 0)
                {
                    DataRow mNewRow = pDtDiagnosesMapped.NewRow();
                    mNewRow["selected"] = false;
                    mNewRow["code"] = mSelectedRow["code"];
                    mNewRow["description"] = mSelectedRow["description"];
                    pDtDiagnosesMapped.Rows.Add(mNewRow);
                    pDtDiagnosesMapped.AcceptChanges();
                }

                mSelectedRow.Delete();
                pDtDiagnoses.AcceptChanges();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdUnmap_Click
        private void cmdUnmap_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdRemove_Click";

            try
            {
                if (viewDiagnosesMapped.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow mSelectedRow = viewDiagnosesMapped.GetDataRow(viewDiagnosesMapped.FocusedRowHandle);

                DataView mDvAvailable = new DataView();
                mDvAvailable.Table = pDtDiagnoses;
                mDvAvailable.Sort = "code";

                int mRowIndex = mDvAvailable.Find(mSelectedRow["code"].ToString().Trim());

                if (mRowIndex < 0)
                {
                    DataRow mNewRow = pDtDiagnoses.NewRow();
                    mNewRow["selected"] = false;
                    mNewRow["code"] = mSelectedRow["code"];
                    mNewRow["description"] = mSelectedRow["description"];
                    pDtDiagnoses.Rows.Add(mNewRow);
                    pDtDiagnoses.AcceptChanges();
                }

                mSelectedRow.Delete();
                pDtDiagnosesMapped.AcceptChanges();
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
            this.Data_Edit();
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region search diagnoses

        #region chkAllInvoices_CheckedChanged
        private void chkAllInvoices_CheckedChanged(object sender, EventArgs e)
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

        #region chkselected_CheckedChanged
        void chkselected_CheckedChanged(object sender, EventArgs e)
        {
            if (pCheckedGrid == true)
            {
                grdDiagnoses.FocusedView.PostEditor();

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

        #region chkAllInvoices_Enter
        private void chkAllInvoices_Enter(object sender, EventArgs e)
        {
            pCheckedGrid = false;
            pCheckedCheckBox = true;
        }
        #endregion

        #region grdDiagnoses_Enter
        private void grdDiagnoses_Enter(object sender, EventArgs e)
        {
            pCheckedCheckBox = false;
            pCheckedGrid = true;
        }
        #endregion

        #endregion

        private void cmdSearchMtuha_Click(object sender, EventArgs e)
        {

        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtCode_Leave(object sender, EventArgs e)
        {

        }
    }
}