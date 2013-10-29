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
    public partial class frmLABLabTests : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsLabTests pMdtLabTests;
        private AfyaPro_MT.clsLabTestGroups pMdtLabTestGroups;
        private AfyaPro_MT.clsLabTestSubGroups pMdtLabTestSubGroups;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataRow pSelectedRow = null;
        internal string gDataState = "";

        private DataTable pDtDropDownValues = new DataTable("dropdownvalues");
        private DataTable pDtGroups = new DataTable("labtestgroups");
        private DataTable pDtSubGroups = new DataTable("labtestsubgroups");
        private DataTable pDtRanges = new DataTable("labtestranges");

        #endregion

        #region frmLABLabTests
        public frmLABLabTests()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmLABLabTests";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                this.CancelButton = cmdClose;

                pMdtLabTests = (AfyaPro_MT.clsLabTests)Activator.GetObject(
                    typeof(AfyaPro_MT.clsLabTests),
                    Program.gMiddleTier + "clsLabTests");

                pMdtLabTestGroups = (AfyaPro_MT.clsLabTestGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsLabTestGroups),
                    Program.gMiddleTier + "clsLabTestGroups");

                pMdtLabTestSubGroups = (AfyaPro_MT.clsLabTestSubGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsLabTestSubGroups),
                    Program.gMiddleTier + "clsLabTestSubGroups");

                pMdtAutoCodes = (AfyaPro_MT.clsAutoCodes)Activator.GetObject(
                    typeof(AfyaPro_MT.clsAutoCodes),
                    Program.gMiddleTier + "clsAutoCodes");

                layoutControl1.AllowCustomizationMenu = false;

                pDtDropDownValues.Columns.Add("description", typeof(System.String));
                grdDropDownValues.DataSource = pDtDropDownValues;

                pDtGroups.Columns.Add("code", typeof(System.String));
                pDtGroups.Columns.Add("description", typeof(System.String));
                cboGroup.Properties.DataSource = pDtGroups;
                cboGroup.Properties.DisplayMember = "description";
                cboGroup.Properties.ValueMember = "code";

                pDtSubGroups.Columns.Add("code", typeof(System.String));
                pDtSubGroups.Columns.Add("description", typeof(System.String));
                cboSubGroup.Properties.DataSource = pDtSubGroups;
                cboSubGroup.Properties.DisplayMember = "description";
                cboSubGroup.Properties.ValueMember = "code";

                pDtRanges.Columns.Add("age_loweryears", typeof(System.Int32));
                pDtRanges.Columns.Add("age_upperyears", typeof(System.Int32));
                pDtRanges.Columns.Add("age_lowermonths", typeof(System.Int32));
                pDtRanges.Columns.Add("age_uppermonths", typeof(System.Int32));
                pDtRanges.Columns.Add("equipment_malelowerrange", typeof(System.Double));
                pDtRanges.Columns.Add("equipment_maleupperrange", typeof(System.Double));
                pDtRanges.Columns.Add("equipment_femalelowerrange", typeof(System.Double));
                pDtRanges.Columns.Add("equipment_femaleupperrange", typeof(System.Double));
                pDtRanges.Columns.Add("normal_malelowerrange", typeof(System.Double));
                pDtRanges.Columns.Add("normal_maleupperrange", typeof(System.Double));
                pDtRanges.Columns.Add("normal_femalelowerrange", typeof(System.Double));
                pDtRanges.Columns.Add("normal_femaleupperrange", typeof(System.Double));

                this.Fill_LookupData();

                grpPossibleValues.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;
                cmdRanges.Enabled = false;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmLABLabTests_Load
        private void frmLABLabTests_Load(object sender, EventArgs e)
        {
            switch (gDataState.Trim().ToLower())
            {
                case "new": Mode_New(); break;
                case "edit": Mode_Edit(); break;
            }

            this.Load_Controls();
        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            DataRow mNewRow;
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region medical doctors

                pDtGroups.Rows.Clear();

                DataTable mDtGroups = pMdtLabTestGroups.View("", "", Program.gLanguageName, "grdLABLabTestGroups");
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

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

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
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.labtestcode));
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
                    txtCode.Properties.ReadOnly = true;
                    txtDescription.Focus();
                }
                else
                {
                    txtCode.Properties.ReadOnly = false;
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

                cboGroup.ItemIndex = Program.Get_LookupItemIndex(cboGroup, "code", pSelectedRow["groupcode"].ToString().Trim());
                cboSubGroup.ItemIndex = Program.Get_LookupItemIndex(cboSubGroup, "code", pSelectedRow["subgroupcode"].ToString().Trim());
                txtCode.Text = pSelectedRow["code"].ToString();
                txtDescription.Text = pSelectedRow["description"].ToString();
                txtDisplayName.Text = pSelectedRow["displayname"].ToString();
                radResultType.SelectedIndex = Convert.ToInt16(pSelectedRow["resulttype"]);
                txtUnits.Text = pSelectedRow["units"].ToString();
                chkRejectOutOfRange.Checked = Convert.ToBoolean(pSelectedRow["rejectoutofrange"]);
                chkRestrictToDropDownList.Checked = Convert.ToBoolean(pSelectedRow["restricttodropdownlist"]);
                chkAdditionalInfo.Checked = Convert.ToBoolean(pSelectedRow["additionalinfo"]);

                this.Fill_DropDownValues();
                this.Fill_Ranges();

                gDataState = "Edit";
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_DropDownValues
        private void Fill_DropDownValues()
        {
            string mFunctionName = "Fill_DropDownValues";

            try
            {
                pDtDropDownValues.Clear();

                DataTable mDtDropDownValues = pMdtLabTests.View_DropDownValues("labtesttypecode='" + txtCode.Text.Trim() + "'", "description", Program.gLanguageName, "");
                foreach (DataRow mDataRow in mDtDropDownValues.Rows)
                {
                    DataRow mNewRow = pDtDropDownValues.NewRow();
                    mNewRow["description"] = mDataRow["description"];
                    pDtDropDownValues.Rows.Add(mNewRow);
                    pDtDropDownValues.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_Ranges
        private void Fill_Ranges()
        {
            string mFunctionName = "Fill_Ranges";

            try
            {
                pDtRanges.Clear();

                DataTable mDtRanges = pMdtLabTests.View_Ranges("labtesttypecode='" + txtCode.Text.Trim() + "'", "age_loweryears,age_lowermonths", "", "");
                foreach (DataRow mDataRow in mDtRanges.Rows)
                {
                    DataRow mNewRow = pDtRanges.NewRow();
                    mNewRow["age_loweryears"] = mDataRow["age_loweryears"];
                    mNewRow["age_upperyears"] = mDataRow["age_upperyears"];
                    mNewRow["age_lowermonths"] = mDataRow["age_lowermonths"];
                    mNewRow["age_uppermonths"] = mDataRow["age_uppermonths"];
                    mNewRow["equipment_malelowerrange"] = mDataRow["equipment_malelowerrange"];
                    mNewRow["equipment_maleupperrange"] = mDataRow["equipment_maleupperrange"];
                    mNewRow["equipment_femalelowerrange"] = mDataRow["equipment_femalelowerrange"];
                    mNewRow["equipment_femaleupperrange"] = mDataRow["equipment_femaleupperrange"];
                    mNewRow["normal_malelowerrange"] = mDataRow["normal_malelowerrange"];
                    mNewRow["normal_maleupperrange"] = mDataRow["normal_maleupperrange"];
                    mNewRow["normal_femalelowerrange"] = mDataRow["normal_femalelowerrange"];
                    mNewRow["normal_femaleupperrange"] = mDataRow["normal_femaleupperrange"];
                    pDtRanges.Rows.Add(mNewRow);
                    pDtRanges.AcceptChanges();
                }
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
            txtDisplayName.Text = "";
            txtUnits.Text = "";
            pDtDropDownValues.Rows.Clear();
            pDtRanges.Rows.Clear();
            chkAdditionalInfo.Checked = false;
        }
        #endregion

        #region Data_Fill
        internal void Data_Fill(GridControl mGridControl)
        {
            string mFunctionName = "Data_Fill";

            try
            {
                //load data
                DataTable mDtData = pMdtLabTests.View("", "", Program.gLanguageName, mGridControl.Name);
                mGridControl.DataSource = mDtData;
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
            if (txtCode.Text.Trim() == "" && txtCode.Text.Trim().ToLower() != "<<---new--->>")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.LAB_LabTestCodeIsInvalid.ToString());
                txtCode.Focus();
                return;
            }

            if (txtDescription.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.LAB_LabTestDescriptionIsInvalid.ToString());
                txtDescription.Focus();
                return;
            }

            if (cboGroup.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.LAB_LabTestGroupDescriptionIsInvalid.ToString());
                cboGroup.Focus();
                return;
            }
            #endregion

            try
            {
                if (txtCode.Text.Trim().ToLower() == "<<---new--->>")
                {
                    mGenerateCode = 1;
                }

                viewDropDownValues.PostEditor();

                string mSubGroupCode = "";
                if (cboSubGroup.ItemIndex != -1)
                {
                    mSubGroupCode = cboSubGroup.GetColumnValue("code").ToString().Trim();
                }

                //add 
                pResult = pMdtLabTests.Add(mGenerateCode, txtCode.Text, txtDescription.Text, txtDisplayName.Text,
                    cboGroup.GetColumnValue("code").ToString(), mSubGroupCode, radResultType.SelectedIndex, 
                    Convert.ToInt16(chkRejectOutOfRange.Checked), txtUnits.Text, pDtDropDownValues, pDtRanges, 
                    Convert.ToInt16(chkRestrictToDropDownList.Checked), Convert.ToInt32(chkAdditionalInfo.Checked), Program.gCurrentUser.Code);
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
            if (txtCode.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.LAB_LabTestCodeIsInvalid.ToString());
                txtCode.Focus();
                return;
            }

            if (txtDescription.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.LAB_LabTestDescriptionIsInvalid.ToString());
                txtDescription.Focus();
                return;
            }
            if (cboGroup.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.LAB_LabTestGroupDescriptionIsInvalid.ToString());
                cboGroup.Focus();
                return;
            }
            #endregion

            try
            {
                viewDropDownValues.PostEditor();

                string mSubGroupCode = "";
                if (cboSubGroup.ItemIndex != -1)
                {
                    mSubGroupCode = cboSubGroup.GetColumnValue("code").ToString().Trim();
                }

                //edit 
                pResult = pMdtLabTests.Edit(txtCode.Text, txtDescription.Text, txtDisplayName.Text,
                    cboGroup.GetColumnValue("code").ToString(), mSubGroupCode, radResultType.SelectedIndex,
                    Convert.ToInt16(chkRejectOutOfRange.Checked), txtUnits.Text, pDtDropDownValues, pDtRanges,
                    Convert.ToInt16(chkRestrictToDropDownList.Checked), Convert.ToInt32(chkAdditionalInfo.Checked), Program.gCurrentUser.Code);

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

                //add 
                pResult = pMdtLabTests.Delete(pSelectedRow["code"].ToString().Trim(), Program.gCurrentUser.Code);
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

        #region radResultType_SelectedIndexChanged
        private void radResultType_SelectedIndexChanged(object sender, EventArgs e)
        {
            grpPossibleValues.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;
            cmdRanges.Enabled = false;
            txbRestrictToDropDownList.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;

            switch (radResultType.SelectedIndex)
            {
                case 1: cmdRanges.Enabled = true; break;
                case 3:
                    {
                        grpPossibleValues.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        txbRestrictToDropDownList.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    }
                    break;
            }
        }
        #endregion

        #region cmdAdd_Click
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            viewDropDownValues.AddNewRow();
        }
        #endregion

        #region cmdRemove_Click
        private void cmdRemove_Click(object sender, EventArgs e)
        {
            viewDropDownValues.DeleteSelectedRows();
            pDtDropDownValues.AcceptChanges();
        }
        #endregion

        #region cboGroup_EditValueChanged
        private void cboGroup_EditValueChanged(object sender, EventArgs e)
        {
            string mFunctionName = "cboGroup_EditValueChanged";

            try
            {
                pDtSubGroups.Rows.Clear();

                if (cboGroup.ItemIndex == -1)
                {
                    return;
                }

                DataTable mDtSubGroups = pMdtLabTestSubGroups.View("groupcode='" + cboGroup.GetColumnValue("code").ToString().Trim() + "'",
                    "", Program.gLanguageName, "grdLABLabTestSubGroups");

                foreach (DataRow mDataRow in mDtSubGroups.Rows)
                {
                    DataRow mNewRow = pDtSubGroups.NewRow();
                    mNewRow["code"] = mDataRow["code"];
                    mNewRow["description"] = mDataRow["description"];
                    pDtSubGroups.Rows.Add(mNewRow);
                    pDtSubGroups.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtSubGroups.Columns)
                {
                    mDataColumn.Caption = mDtSubGroups.Columns[mDataColumn.ColumnName].Caption;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdRanges_Click
        private void cmdRanges_Click(object sender, EventArgs e)
        {
            frmLABTestRanges mLABTestRanges = new frmLABTestRanges(pDtRanges);
            mLABTestRanges.ShowDialog();
        }
        #endregion
    }
}