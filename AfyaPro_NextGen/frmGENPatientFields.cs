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
using DevExpress.XtraEditors.Controls;

namespace AfyaPro_NextGen
{
    public partial class frmGENPatientFields : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsPatientExtraFields pMdtPatientExtraFields;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataRow pSelectedRow = null;
        internal string gDataState = "";

        private ComboBoxItemCollection pFilterValuesFrom;
        private DataTable pDtDropDownValues = new DataTable("dropdownvalues");

        #endregion

        #region frmGENPatientFields
        public frmGENPatientFields()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmGENPatientFields";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                this.CancelButton = cmdClose;

                pMdtPatientExtraFields = (AfyaPro_MT.clsPatientExtraFields)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPatientExtraFields),
                    Program.gMiddleTier + "clsPatientExtraFields");

                pFilterValuesFrom = cboFilterValueFrom.Properties.Items;

                this.Fill_FilterOn();

                pDtDropDownValues.Columns.Add("description", typeof(System.String));
                pDtDropDownValues.Columns.Add("filtervalue", typeof(System.String));
                grdDropDownValues.DataSource = pDtDropDownValues;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmGENPatientFields_Load
        private void frmGENPatientFields_Load(object sender, EventArgs e)
        {
            switch (gDataState.Trim().ToLower())
            {
                case "new": Mode_New(); break;
                case "edit": Mode_Edit(); break;
            }

            chkAllowInput.Checked = true;
            chkRememberEntries.Checked = false;

            this.Load_Controls();
        }
        #endregion

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(txbFieldName);
            mObjectsList.Add(txbCaption);
            mObjectsList.Add(txbFieldType);
            mObjectsList.Add(txbDataType);
            mObjectsList.Add(txbDefaultValue);
            mObjectsList.Add(txbFilterValueOn);
            mObjectsList.Add(chkRememberEntries);
            mObjectsList.Add(chkAllowInput);
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
                txtFieldName.Text = "";
                txtFieldName.Focus();
                this.Data_Clear();

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

                txtFieldName.Text = pSelectedRow["fieldname"].ToString();
                txtCaption.Text = pSelectedRow["fieldcaption"].ToString();
                cboFieldType.Text = pSelectedRow["fieldtype"].ToString();
                cboDataType.Text = pSelectedRow["datatype"].ToString();
                txtDefaultValue.Text = pSelectedRow["defaultvalue"].ToString();
                cboFilterValueFrom.Text = pSelectedRow["filteronvaluefrom"].ToString();
                chkAllowInput.Checked = Convert.ToBoolean(pSelectedRow["allowinput"]);
                chkRestrictToDropDownList.Checked = Convert.ToBoolean(pSelectedRow["restricttodropdownlist"]);
                chkRememberEntries.Checked = Convert.ToBoolean(pSelectedRow["rememberentries"]);
                chkCompulsory.Checked = Convert.ToBoolean(pSelectedRow["compulsory"]);
                txtErrorOnEmpty.Text = pSelectedRow["erroronempty"].ToString();
                txtErrorOnInvalidInput.Text = pSelectedRow["erroroninvalidinput"].ToString();
                radCharacterCasingOption.SelectedIndex = Convert.ToInt16(pSelectedRow["charactercasingoption"]);

                this.Fill_DropDownValues();

                if (cboFieldType.Text.Trim().ToLower() == "dropdown")
                {
                    grpDropDownOptions.Enabled = true;
                }

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
            txtCaption.Text = "";
            cboFieldType.Text = "";
            cboDataType.Text = "";
            txtDefaultValue.Text = "";
            cboFilterValueFrom.Text = "";
            chkAllowInput.Checked = true;
            chkRestrictToDropDownList.Checked = false;
            chkRememberEntries.Checked = false;
            chkCompulsory.Checked = false;
            txtErrorOnEmpty.Text = "";
            txtErrorOnInvalidInput.Text = "";
            radCharacterCasingOption.SelectedIndex = 0;
            pDtDropDownValues.Rows.Clear();
        }
        #endregion

        #region Fill_DropDownValues
        private void Fill_DropDownValues()
        {
            string mFunctionName = "Fill_DropDownValues";

            try
            {
                pDtDropDownValues.Clear();

                DataTable mDtDropDownValues = pMdtPatientExtraFields.View_Lookup("fieldname='" + txtFieldName.Text.Trim() + "'", "filtervalue,description", Program.gLanguageName, "");
                foreach (DataRow mDataRow in mDtDropDownValues.Rows)
                {
                    DataRow mNewRow = pDtDropDownValues.NewRow();
                    mNewRow["description"] = mDataRow["description"];
                    mNewRow["filtervalue"] = mDataRow["filtervalue"];
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

        #region Fill_FilterOn
        private void Fill_FilterOn()
        {
            string mFunctionName = "Fill_FilterOn";

            try
            {
                pFilterValuesFrom.Clear();

                DataTable mDtPatientExtraFields = pMdtPatientExtraFields.View("", "fieldname", Program.gLanguageName, "");
                foreach (DataRow mDataRow in mDtPatientExtraFields.Rows)
                {
                    pFilterValuesFrom.Add(mDataRow["fieldname"].ToString());
                }
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
                DataTable mDtPatientExtraFields = pMdtPatientExtraFields.View("", "", Program.gLanguageName, mGridControl.Name);
                mGridControl.DataSource = mDtPatientExtraFields;
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
            if (txtFieldName.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GEN_PatientFieldNameIsInvalid.ToString());
                txtFieldName.Focus();
                return;
            }

            if (txtCaption.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GEN_PatientFieldCaptionIsInvalid.ToString());
                txtCaption.Focus();
                return;
            }
            #endregion

            try
            {
                string mErrorOnEmpty = txtErrorOnEmpty.Text.Trim();
                if (mErrorOnEmpty.Trim() == "")
                {
                    mErrorOnEmpty = "Please enter " + txtCaption.Text.Trim();
                }

                //viewDropDownValues.PostEditor();

                //add 
                pResult = pMdtPatientExtraFields.Add(txtFieldName.Text, txtCaption.Text, cboFieldType.Text,
                    cboDataType.Text, txtDefaultValue.Text, cboFilterValueFrom.Text,
                    Convert.ToInt16(chkAllowInput.Checked), Convert.ToInt16(chkRestrictToDropDownList.Checked), 
                    Convert.ToInt16(chkRememberEntries.Checked), radCharacterCasingOption.SelectedIndex, 
                    Convert.ToInt16(chkCompulsory.Checked), mErrorOnEmpty, txtErrorOnInvalidInput.Text, 
                    pDtDropDownValues, Program.gCurrentUser.Code);
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
                Program.Display_Info(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SettingsSavedSuccessfully.ToString());
                this.Fill_FilterOn();
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
            if (txtFieldName.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GEN_PatientFieldNameIsInvalid.ToString());
                txtFieldName.Focus();
                return;
            }

            if (txtCaption.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GEN_PatientFieldCaptionIsInvalid.ToString());
                txtCaption.Focus();
                return;
            }
            #endregion

            try
            {
                string mErrorOnEmpty = txtErrorOnEmpty.Text.Trim();
                if (mErrorOnEmpty.Trim() == "")
                {
                    mErrorOnEmpty = "Please enter " + txtCaption.Text.Trim();
                }

                //viewDropDownValues.PostEditor();

                //edit
                pResult = pMdtPatientExtraFields.Edit(txtFieldName.Text, txtCaption.Text, cboFieldType.Text,
                    cboDataType.Text, txtDefaultValue.Text, cboFilterValueFrom.Text,
                    Convert.ToInt16(chkAllowInput.Checked), Convert.ToInt16(chkRestrictToDropDownList.Checked),
                    Convert.ToInt16(chkRememberEntries.Checked), radCharacterCasingOption.SelectedIndex,
                    Convert.ToInt16(chkCompulsory.Checked), mErrorOnEmpty, txtErrorOnInvalidInput.Text,
                    pDtDropDownValues, Program.gCurrentUser.Code);
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
                Program.Display_Info(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SettingsSavedSuccessfully.ToString());
                this.Fill_FilterOn();
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
                    pSelectedRow["fieldname"].ToString().Trim() + "'   '"
                    + pSelectedRow["fieldcaption"].ToString().Trim());

                if (mResp != DialogResult.Yes)
                {
                    return;
                }

                //delete 
                pResult = pMdtPatientExtraFields.Delete(pSelectedRow["fieldname"].ToString().Trim(), Program.gCurrentUser.Code);
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

        #region cmdNew_Click
        private void cmdNew_Click(object sender, EventArgs e)
        {
            this.Mode_New();
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

        #region cboFieldType_SelectedIndexChanged
        private void cboFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFieldType.Text.Trim().ToLower() == "dropdown")
            {
                grpDropDownOptions.Enabled = false;
            }
            else
            {
                grpDropDownOptions.Enabled = true;
            }
        }
        #endregion
    }
}