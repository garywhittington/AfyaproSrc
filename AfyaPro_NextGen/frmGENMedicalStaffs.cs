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
    public partial class frmGENMedicalStaffs : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsTreatmentPoints pMdtTreatmentPoints;
        private AfyaPro_MT.clsMedicalStaffs pMdtMedicalStaffs;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataTable pDtCategories = new DataTable("staffcategories");
        private DataTable pDtTreatmentPoints = new DataTable("treatmentpoints");
        private DataRow pSelectedRow = null;
        internal string gDataState = "";
        private bool pFirstTimeLoad = true;

        #endregion

        #region frmGENMedicalStaffs
        public frmGENMedicalStaffs()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmGENMedicalStaffs";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                
                this.CancelButton = cmdClose;

                pMdtMedicalStaffs = (AfyaPro_MT.clsMedicalStaffs)Activator.GetObject(
                    typeof(AfyaPro_MT.clsMedicalStaffs),
                    Program.gMiddleTier + "clsMedicalStaffs");

                pMdtAutoCodes = (AfyaPro_MT.clsAutoCodes)Activator.GetObject(
                    typeof(AfyaPro_MT.clsAutoCodes),
                    Program.gMiddleTier + "clsAutoCodes");

                pMdtTreatmentPoints = (AfyaPro_MT.clsTreatmentPoints)Activator.GetObject(
                    typeof(AfyaPro_MT.clsTreatmentPoints),
                    Program.gMiddleTier + "clsTreatmentPoints");

                pDtCategories.Columns.Add("code", typeof(System.String));
                pDtCategories.Columns.Add("description", typeof(System.String));
                cboCategory.Properties.DataSource = pDtCategories;
                cboCategory.Properties.DisplayMember = "description";
                cboCategory.Properties.ValueMember = "code";

                #region TreatmentPoints
                pDtTreatmentPoints.Columns.Add("code", typeof(System.String));
                pDtTreatmentPoints.Columns.Add("description", typeof(System.String));
                cboTreatmentPoint.Properties.DataSource = pDtTreatmentPoints;
                cboTreatmentPoint.Properties.DisplayMember = "description";
                cboTreatmentPoint.Properties.ValueMember = "code";
                #endregion

                this.Fill_LookupData();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmGENMedicalStaffs_Load
        private void frmGENMedicalStaffs_Load(object sender, EventArgs e)
        {
            switch (gDataState.Trim().ToLower())
            {
                case "new": Mode_New(); break;
                case "edit": Mode_Edit(); break;
            }

            this.Load_Controls();
        }
        #endregion

        #region frmGENMedicalStaffs_Activated
        private void frmGENMedicalStaffs_Activated(object sender, EventArgs e)
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

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(txbStaffCategory);
            mObjectsList.Add(txbCode);
            mObjectsList.Add(txbDescription);
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
                mGridView.OptionsView.ShowGroupPanel = true;
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
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.staffcode));
                if (mGenerateCode == -1)
                {
                    Program.Display_Server_Error("");
                    return;
                }

                txtCode.Text = "";
                this.Data_Clear();

                #region display owning groups if any
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain.MainView;

                if (mGridView.FocusedRowHandle >= 0)
                {
                    pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);
                    cboCategory.ItemIndex = Program.Get_LookupItemIndex(cboCategory, "code", "category" + pSelectedRow["category"].ToString());
                }
                #endregion

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

                cboCategory.EditValue = null;
                this.Data_Clear();
                pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                cboCategory.ItemIndex = Program.Get_LookupItemIndex(cboCategory, "code", "category" + pSelectedRow["category"].ToString());
                txtCode.Text = pSelectedRow["code"].ToString();
                txtDescription.Text = pSelectedRow["description"].ToString();

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
            cboTreatmentPoint.EditValue = null;
        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            DataRow mNewRow;
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region Categories

                pDtCategories.Rows.Clear();
                DataTable mDtCategories = pMdtMedicalStaffs.Get_Categories(Program.gLanguageName, "grdGENMedicalStaffs");
                foreach (DataRow mDataRow in mDtCategories.Rows)
                {
                    mNewRow = pDtCategories.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtCategories.Rows.Add(mNewRow);
                    pDtCategories.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtCategories.Columns)
                {
                    mDataColumn.Caption = mDtCategories.Columns[mDataColumn.ColumnName].Caption;
                }

                #endregion

                #region TreatmentPoints

                pDtTreatmentPoints.Rows.Clear();
                DataTable mDtTreatmentPoints = pMdtTreatmentPoints.View("", "code", Program.gLanguageName, "grdGENTreatmentPoints");
                foreach (DataRow mDataRow in mDtTreatmentPoints.Rows)
                {
                    mNewRow = pDtTreatmentPoints.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtTreatmentPoints.Rows.Add(mNewRow);
                    pDtTreatmentPoints.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtTreatmentPoints.Columns)
                {
                    mDataColumn.Caption = mDtTreatmentPoints.Columns[mDataColumn.ColumnName].Caption;
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

        #region Data_Fill
        internal void Data_Fill(GridControl mGridControl)
        {
            string mFunctionName = "Data_Fill";

            try
            {
                //load data
                DataTable mDtMedicalStaffs = pMdtMedicalStaffs.View("", "", Program.gLanguageName, mGridControl.Name);
                mGridControl.DataSource = mDtMedicalStaffs;

                //expand all groups
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)mGridControl.MainView;
                mGridView.ExpandAllGroups();
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
            Int16 mGenerateCode = 0;
            String mFunctionName = "Data_New";

            #region validation
            if (cboCategory.ItemIndex == -1)
            {
                Program.Display_Error("Invalid category");
                cboCategory.Focus();
                return;
            }

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

                string mTreatmentPointCode = "";
                if (cboTreatmentPoint.ItemIndex != -1)
                {
                    mTreatmentPointCode = cboTreatmentPoint.GetColumnValue("code").ToString().Trim();
                }

                //add category
                pResult = pMdtMedicalStaffs.Add(mGenerateCode, txtCode.Text, txtDescription.Text,
                    Convert.ToInt16(cboCategory.GetColumnValue("code").ToString().Substring(8)),
                    Program.gCurrentUser.Code, mTreatmentPointCode);
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
            String mFunctionName = "Data_Edit";

            #region validation
            if (cboCategory.ItemIndex == -1)
            {
                Program.Display_Error("Invalid category");
                cboCategory.Focus();
                return;
            }

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
                string mTreatmentPointCode = "";
                if (cboTreatmentPoint.ItemIndex != -1)
                {
                    mTreatmentPointCode = cboTreatmentPoint.GetColumnValue("code").ToString().Trim();
                }

                //edit 
                pResult = pMdtMedicalStaffs.Edit(txtCode.Text, txtDescription.Text,
                    Convert.ToInt16(cboCategory.GetColumnValue("code").ToString().Substring(8)),
                    Program.gCurrentUser.Code, mTreatmentPointCode);
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

                DialogResult mResp = Program.Display_Question("Delete '"
                    + pSelectedRow["code"].ToString().Trim() + "'   '"
                    + pSelectedRow["description"].ToString().Trim() + "'", MessageBoxDefaultButton.Button2);

                if (mResp != DialogResult.Yes)
                {
                    return;
                }

                //add 
                pResult = pMdtMedicalStaffs.Delete(pSelectedRow["code"].ToString().Trim(), Program.gCurrentUser.Code);
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
    }
}