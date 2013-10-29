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
using DevExpress.XtraLayout;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.IO;
using System.Xml.Serialization;

namespace AfyaPro_NextGen
{
    public partial class frmDXTPrescribeLabTests : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsLaboratories pMdtLaboratories;
        private AfyaPro_MT.clsLabTestGroups pMdtLabTestGroups;
        private AfyaPro_MT.clsLabTestSubGroups pMdtLabTestSubGroups;
        private AfyaPro_MT.clsLabTests pMdtLabTests;
        private AfyaPro_MT.clsPatientLabTests pMdtPatientLabTests;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private AfyaPro_Types.clsPatient pCurrentPatient;

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private DataTable pDtLaboratories = new DataTable("laboratories");
        private DataTable pDtGroups = new DataTable("labtestgroups");
        private DataTable pDtSubGroups = new DataTable("labtestsubgroups");
        private DataTable pDtLabTests = new DataTable("labtests");

        //private string pTreatmentPointCode = "";
        //internal string TreatmentPointCode
        //{
        //    set { pTreatmentPointCode = value; }
        //    get { return pTreatmentPointCode; }
        //}

        #endregion

        #region frmDXTPrescribeLabTests
        public frmDXTPrescribeLabTests(AfyaPro_Types.clsPatient mCurrentPatient)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmDXTPrescribeLabTests";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                this.pCurrentPatient = mCurrentPatient;

                pMdtLabTestGroups = (AfyaPro_MT.clsLabTestGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsLabTestGroups),
                    Program.gMiddleTier + "clsLabTestGroups");

                pMdtLabTestSubGroups = (AfyaPro_MT.clsLabTestSubGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsLabTestSubGroups),
                    Program.gMiddleTier + "clsLabTestSubGroups");

                pMdtLabTests = (AfyaPro_MT.clsLabTests)Activator.GetObject(
                    typeof(AfyaPro_MT.clsLabTests),
                    Program.gMiddleTier + "clsLabTests");

                pMdtLaboratories = (AfyaPro_MT.clsLaboratories)Activator.GetObject(
                    typeof(AfyaPro_MT.clsLaboratories),
                    Program.gMiddleTier + "clsLaboratories");

                pMdtPatientLabTests = (AfyaPro_MT.clsPatientLabTests)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPatientLabTests),
                    Program.gMiddleTier + "clsPatientLabTests");

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.labpatienttests_customizelayout.ToString());

                grdDXTPrescribeLabTests.ForceInitialize();

                pDtLabTests.Columns.Add("selected", typeof(System.Boolean));
                pDtLabTests.Columns.Add("code", typeof(System.String));
                pDtLabTests.Columns.Add("description", typeof(System.String));
                pDtLabTests.Columns.Add("displayname", typeof(System.String));
                pDtLabTests.Columns.Add("groupcode", typeof(System.String));
                pDtLabTests.Columns.Add("subgroupcode", typeof(System.String));

                grdDXTPrescribeLabTests.DataSource = pDtLabTests;

                viewDXTPrescribeLabTests.Columns["displayname"].Caption = "Test";

                foreach (DevExpress.XtraGrid.Columns.GridColumn mGridColumn in viewDXTPrescribeLabTests.Columns)
                {
                    if (mGridColumn.FieldName.ToLower() == "selected")
                    {
                        mGridColumn.OptionsColumn.ShowCaption = false;
                    }
                    else
                    {
                        mGridColumn.OptionsColumn.AllowEdit = false;
                        mGridColumn.OptionsColumn.AllowFocus = false;
                    }
                }

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

                pDtLaboratories.Columns.Add("code", typeof(System.String));
                pDtLaboratories.Columns.Add("description", typeof(System.String));
                cboLaboratory.Properties.DataSource = pDtLaboratories;
                cboLaboratory.Properties.DisplayMember = "description";
                cboLaboratory.Properties.ValueMember = "code";

                txtDate.EditValue = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue).Date;

                this.Fill_LookupData();
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
            List<Object> mObjectsList = new List<object>();

            mObjectsList.Add(txbPatientId);
            mObjectsList.Add(txbName);
            mObjectsList.Add(txbYears);
            mObjectsList.Add(txbMonths);
            mObjectsList.Add(txbGender);
            mObjectsList.Add(txbDocuments);

            Program.Apply_Language(this.Name, mObjectsList);
            this.Data_Clear();
        }

        #endregion

        #region frmDXTPrescribeLabTests_Load
        private void frmDXTPrescribeLabTests_Load(object sender, EventArgs e)
        {
            this.Top = 0;

            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            this.Load_Controls();

            txbBirthDateFormat.Text = "(" + Program.gCulture.DateTimeFormat.ShortDatePattern + ")";

            this.Fill_LabTests();

            Program.Restore_GridLayout(grdDXTPrescribeLabTests, grdDXTPrescribeLabTests.Name);

            this.pFormWidth = this.Width;
            this.pFormHeight = this.Height;

            Program.Center_Screen(this);

            this.Append_ShortcutKeys();

            this.Data_Display();
        }
        #endregion

        #region frmDXTPrescribeLabTests_Shown
        private void frmDXTPrescribeLabTests_Shown(object sender, EventArgs e)
        {
            //only one laboratory is registered
            if (pDtLaboratories.Rows.Count == 1)
            {
                cboLaboratory.ItemIndex = 0;
            }

            txtPatientId.Focus();
        }
        #endregion

        #region frmDXTPrescribeLabTests_FormClosing
        private void frmDXTPrescribeLabTests_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //layout
                if (layoutControl1.IsModified == true)
                {
                    Program.Save_FormLayout(this, layoutControl1, this.Name);
                }

                //grid
                Program.Save_GridLayout(grdDXTPrescribeLabTests, grdDXTPrescribeLabTests.Name);
            }
            catch { }
        }
        #endregion

        #region Append_ShortcutKeys
        private void Append_ShortcutKeys()
        {
            cmdSave.Text = cmdSave.Text + " (" + Program.KeyCode_Save.ToString() + ")";
        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            DataRow mNewRow;
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region labtestgroups

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

                #region laboratories

                pDtLaboratories.Rows.Clear();

                DataTable mDtLaboratories = pMdtLaboratories.View("", "", Program.gLanguageName, "grdLABLaboratories");
                foreach (DataRow mDataRow in mDtLaboratories.Rows)
                {
                    mNewRow = pDtLaboratories.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtLaboratories.Rows.Add(mNewRow);
                    pDtLaboratories.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtLaboratories.Columns)
                {
                    mDataColumn.Caption = mDtLaboratories.Columns[mDataColumn.ColumnName].Caption;
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

        #region Data_Clear
        private void Data_Clear()
        {
            txtName.Text = "";
            txtGender.Text = "";
            txtBirthDate.Text = "";
            txtYears.Text = "";
            txtMonths.Text = "";

            txtPatientId.Focus();
        }
        #endregion

        #region Data_Display
        internal void Data_Display()
        {
            if (pCurrentPatient.Exist == true)
            {
                string mFullName = pCurrentPatient.firstname;
                if (pCurrentPatient.othernames.Trim() != "")
                {
                    mFullName = mFullName + " " + pCurrentPatient.othernames;
                }
                mFullName = mFullName + " " + pCurrentPatient.surname;

                txtPatientId.Text = pCurrentPatient.code;
                txtName.Text = mFullName;
                if (pCurrentPatient.gender.Trim().ToLower() == "f")
                {
                    txtGender.Text = "Female";
                }
                else
                {
                    txtGender.Text = "Male";
                }
                txtBirthDate.Text = pCurrentPatient.birthdate.ToString("d");
                int mDays = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue).Subtract(pCurrentPatient.birthdate).Days;
                int mYears = (int)mDays / 365;
                int mMonths = (int)(mDays % 365) / 30;

                txtYears.Text = mYears.ToString();
                txtMonths.Text = mMonths.ToString();

                this.Fill_LabTests();

                System.Media.SystemSounds.Beep.Play();
            }
        }
        #endregion

        #region Fill_LabTests
        internal void Fill_LabTests()
        {
            string mFunctionName = "Fill_LabTests";

            if (Program.IsNullDate(txtDate.EditValue) == true)
            {
                Program.Display_Error("Invalid date entry", false);
                txtDate.Focus();
                return;
            }

            try
            {
                pDtLabTests.Rows.Clear();

                if (cboGroup.ItemIndex == -1)
                {
                    return;
                }

                string mSubGroupCode = "";
                if (cboSubGroup.ItemIndex != -1)
                {
                    mSubGroupCode = cboSubGroup.GetColumnValue("code").ToString().Trim();
                }

                string mFilter = "groupcode='" + cboGroup.GetColumnValue("code").ToString().Trim() + "' and subgroupcode='" + mSubGroupCode + "'"; ;

                int mYears = Program.IsNumeric(txtYears.Text) == false ? 0 : Convert.ToInt32(txtYears.Text);
                int mMonths = Program.IsNumeric(txtMonths.Text) == false ? 0 : Convert.ToInt32(txtMonths.Text);

                DataTable mDtLabTests = pMdtLabTests.View(
                    "groupcode='" + cboGroup.GetColumnValue("code").ToString().Trim() 
                    + "' and subgroupcode='" + mSubGroupCode + "'", "", "", "");
                DataTable mDtPatientTests = pMdtPatientLabTests.View_Prescription(txtPatientId.Text.Trim(), Convert.ToDateTime(txtDate.EditValue), grdDXTPrescribeLabTests.Name);

                DataView mDvPatientTests = new DataView();
                mDvPatientTests.Table = mDtPatientTests;
                mDvPatientTests.Sort = "labtesttypecode";

                foreach (DataRow mDataRow in mDtLabTests.Rows)
                {
                    bool mSelected = false;
                    if (mDvPatientTests.Find(mDataRow["code"].ToString().Trim()) >= 0)
                    {
                        mSelected = true;
                    }

                    DataRow mNewRow = pDtLabTests.NewRow();
                    mNewRow["selected"] = mSelected;
                    mNewRow["code"] = mDataRow["code"];
                    mNewRow["description"] = mDataRow["description"];
                    mNewRow["displayname"] = mDataRow["displayname"];
                    mNewRow["groupcode"] = mDataRow["groupcode"];
                    mNewRow["subgroupcode"] = mDataRow["subgroupcode"];
                    pDtLabTests.Rows.Add(mNewRow);
                    pDtLabTests.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_SubGroups
        private void Fill_SubGroups()
        {
            string mFunctionName = "Fill_SubGroups";

            try
            {
                pDtSubGroups.Rows.Clear();

                if (cboGroup.ItemIndex == -1)
                {
                    return;
                }

                DataTable mDtSubGroups = pMdtLabTestSubGroups.View("groupcode='" + cboGroup.GetColumnValue("code").ToString().Trim() + "'",
                    "", Program.gLanguageName, "grdLABLabTestSubGroups");

                DataRow mNewRow = pDtSubGroups.NewRow();
                mNewRow["code"] = "";
                mNewRow["description"] = "";
                pDtSubGroups.Rows.Add(mNewRow);
                pDtSubGroups.AcceptChanges();

                foreach (DataRow mDataRow in mDtSubGroups.Rows)
                {
                    mNewRow = pDtSubGroups.NewRow();
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

        #region cboGroup_EditValueChanged
        private void cboGroup_EditValueChanged(object sender, EventArgs e)
        {
            string mFunctionName = "cboGroup_EditValueChanged";

            try
            {
                this.Fill_SubGroups();
                this.Fill_LabTests();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cboSubGroup_EditValueChanged
        private void cboSubGroup_EditValueChanged(object sender, EventArgs e)
        {
            this.Fill_LabTests();
        }
        #endregion

        #region Save
        private void Save()
        {
            string mLaboratoryCode = "";
            string mClinicalOfficerCode = "";

            string mFunctionName = "Save";

            #region validation

            if (txtPatientId.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoIsInvalid.ToString());
                txtPatientId.Focus();
                return;
            }

            if (Program.IsNullDate(txtDate.EditValue) == true)
            {
                Program.Display_Error("Invalid date entry", false);
                txtDate.Focus();
                return;
            }

            //laboratory
            if (txbLaboratory.Visible == true)
            {
                if (cboLaboratory.ItemIndex == -1)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.LAB_LaboratorysDescriptionIsInvalid.ToString());
                    cboLaboratory.Focus();
                    return;
                }
            }

            //labtestgroup
            if (cboGroup.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.LAB_LabTestGroupDescriptionIsInvalid.ToString());
                cboGroup.Focus();
                return;
            }

            #endregion

            try
            {
                if (cboLaboratory.ItemIndex != -1)
                {
                    mLaboratoryCode = cboLaboratory.GetColumnValue("code").ToString().Trim();
                }

                string mSubGroupCode = cboSubGroup.ItemIndex == -1 ? "" : cboSubGroup.GetColumnValue("code").ToString().Trim();
                string mSubGroupDescription = cboSubGroup.ItemIndex == -1 ? "" : cboSubGroup.GetColumnValue("description").ToString().Trim();

                DataTable mDtLabTests = new DataTable("labtests");
                mDtLabTests.Columns.Add("labtestgroupcode", typeof(System.String));
                mDtLabTests.Columns.Add("labtestsubgroupcode", typeof(System.String));
                mDtLabTests.Columns.Add("labtesttypecode", typeof(System.String));
                mDtLabTests.Columns.Add("delete", typeof(System.Boolean));

                foreach (DataRow mDataRow in pDtLabTests.Rows)
                {
                    DataRow mNewRow = mDtLabTests.NewRow();
                    mNewRow["delete"] = !Convert.ToBoolean(mDataRow["selected"]);
                    mNewRow["labtestgroupcode"] = cboGroup.GetColumnValue("code").ToString().Trim();
                    mNewRow["labtestsubgroupcode"] = mSubGroupCode;
                    mNewRow["labtesttypecode"] = mDataRow["code"];
                    mDtLabTests.Rows.Add(mNewRow);
                    mDtLabTests.AcceptChanges();
                }

                pResult = pMdtPatientLabTests.Prescribe(
                                                    Convert.ToDateTime(txtDate.EditValue),
                                                    txtPatientId.Text,
                                                    mLaboratoryCode,
                                                    mClinicalOfficerCode,
                                                    mDtLabTests,
                                                    Program.gCurrentUser.Code);
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

                Program.Display_Info("Prescription saved successfully");
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdSave_Click
        private void cmdSave_Click(object sender, EventArgs e)
        {
            this.Save();
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region frmDXTPrescribeLabTests_KeyDown
        void frmDXTPrescribeLabTests_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Program.KeyCode_Save:
                    {
                        this.Save();
                    }
                    break;
            }
        }
        #endregion
    }
}