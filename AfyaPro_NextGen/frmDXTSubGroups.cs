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
    public partial class frmDXTSubGroups : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsDiagnosesGroups pMdtDiagnosesGroups;
        private AfyaPro_MT.clsDiagnosesSubGroups pMdtDiagnosesSubGroups;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataTable pDtGroups = new DataTable("groups");
        private DataRow pSelectedRow = null;
        internal string gDataState = "";

        #endregion

        #region frmDXTSubGroups
        public frmDXTSubGroups()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmDXTSubGroups";

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                this.CancelButton = cmdClose;

                pMdtDiagnosesGroups = (AfyaPro_MT.clsDiagnosesGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsDiagnosesGroups),
                    Program.gMiddleTier + "clsDiagnosesGroups");

                pMdtDiagnosesSubGroups = (AfyaPro_MT.clsDiagnosesSubGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsDiagnosesSubGroups),
                    Program.gMiddleTier + "clsDiagnosesSubGroups");

                pDtGroups.Columns.Add("code", typeof(System.String));
                pDtGroups.Columns.Add("description", typeof(System.String));
                cboGroup.Properties.DataSource = pDtGroups;
                cboGroup.Properties.DisplayMember = "description";
                cboGroup.Properties.ValueMember = "code";

                this.Fill_LookupData();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmDXTSubGroups_Load
        private void frmDXTSubGroups_Load(object sender, EventArgs e)
        {
            switch (gDataState.Trim().ToLower())
            {
                case "new": Mode_New(); break;
                case "edit": Mode_Edit(); break;
            }

            this.Load_Controls();
        }
        #endregion

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(txbGroup);
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
                txtCode.Text = "";
                this.Data_Clear();

                #region display owning groups if any
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain.MainView;

                if (mGridView.FocusedRowHandle >= 0)
                {
                    pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);
                    cboGroup.ItemIndex = Program.Get_LookupItemIndex(cboGroup, "code", pSelectedRow["groupcode"].ToString());
                }
                #endregion

                txtCode.Focus();

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

                cboGroup.EditValue = null;
                this.Data_Clear();
                pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                cboGroup.ItemIndex = Program.Get_LookupItemIndex(cboGroup, "code", pSelectedRow["groupcode"].ToString());
                txtCode.Text = pSelectedRow["code"].ToString();
                txtDescription.Text = pSelectedRow["description"].ToString();

                txtDescription.Focus();

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
        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            DataRow mNewRow;
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region groups

                pDtGroups.Rows.Clear();
                DataTable mDtGroups = pMdtDiagnosesGroups.View("", "code", Program.gLanguageName, "grdDXTGroups");
                foreach (DataRow mDataRow in mDtGroups.Rows)
                {
                    mNewRow = pDtGroups.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtGroups.Rows.Add(mNewRow);
                    pDtGroups.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtGroups.Columns)
                {
                    mDataColumn.Caption = mDtGroups.Columns[mDataColumn.ColumnName].Caption;
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
                DataTable mDtRegions = pMdtDiagnosesSubGroups.View("", "", Program.gLanguageName, mGridControl.Name);
                mGridControl.DataSource = mDtRegions;

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
            String mFunctionName = "Data_New";

            #region validation
            if (cboGroup.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DxGroupDescriptionIsInvalid.ToString());
                cboGroup.Focus();
                return;
            }

            if (txtCode.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DxCodeIsInvalid.ToString());
                txtCode.Focus();
                return;
            }

            if (txtDescription.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DxDescriptionIsInvalid.ToString());
                txtDescription.Focus();
                return;
            }
            #endregion

            try
            {
                //add 
                pResult = pMdtDiagnosesSubGroups.Add(txtCode.Text, txtDescription.Text,
                    cboGroup.GetColumnValue("code").ToString(), Program.gCurrentUser.Code);
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
            if (cboGroup.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DxGroupDescriptionIsInvalid.ToString());
                cboGroup.Focus();
                return;
            }

            if (txtCode.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DxCodeIsInvalid.ToString());
                txtCode.Focus();
                return;
            }

            if (txtDescription.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DxDescriptionIsInvalid.ToString());
                txtDescription.Focus();
                return;
            }
            #endregion

            try
            {
                //add 
                pResult = pMdtDiagnosesSubGroups.Edit(txtCode.Text, txtDescription.Text,
                    cboGroup.GetColumnValue("code").ToString(), Program.gCurrentUser.Code);
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

                DialogResult mResp = Program.Confirm_Deletion(
                    pSelectedRow["code"].ToString().Trim() + "'   '"
                    + pSelectedRow["description"].ToString().Trim());

                if (mResp != DialogResult.Yes)
                {
                    return;
                }

                //delete 
                pResult = pMdtDiagnosesSubGroups.Delete(pSelectedRow["code"].ToString().Trim(), Program.gCurrentUser.Code);
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