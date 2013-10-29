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
    public partial class frmLABPatientTests : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsRegistrations pMdtRegistrations;
        private AfyaPro_MT.clsLaboratories pMdtLaboratories;
        private AfyaPro_MT.clsMedicalStaffs pMdtMedicalStaffs;
        private AfyaPro_MT.clsLabTestGroups pMdtLabTestGroups;
        private AfyaPro_MT.clsLabTestSubGroups pMdtLabTestSubGroups;
        private AfyaPro_MT.clsLabTests pMdtLabTests;
        private AfyaPro_MT.clsLabRemarks pMdtLabRemarks;
        private AfyaPro_MT.clsPatientLabTests pMdtPatientLabTests;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private AfyaPro_Types.clsPatient pCurrentPatient;
        private AfyaPro_Types.clsBooking pCurrentBooking = new AfyaPro_Types.clsBooking();
        private AfyaPro_Types.clsAdmission pCurrentAdmission = new AfyaPro_Types.clsAdmission();

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private string pCurrPatientId = "";
        private string pPrevPatientId = "";
        private bool pSearchingPatient = false;

        private DataTable pDtLaboratories = new DataTable("laboratories");
        private DataTable pDtClinicalOfficers = new DataTable("clinicalofficers");
        private DataTable pDtLabTechnician = new DataTable("labtechnicians");
        private DataTable pDtGroups = new DataTable("labtestgroups");
        private DataTable pDtSubGroups = new DataTable("labtestsubgroups");
        private DataTable pDtLabTests = new DataTable("labtests");
        private DataTable pDtDropDownValues = new DataTable("dropdownvalues");
        private DataView pDvDropDownValues = new DataView();
        private ComboBoxItemCollection pRemarks;

        #endregion

        #region frmLABPatientTests
        public frmLABPatientTests()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmLABPatientTests";

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                pMdtRegistrations = (AfyaPro_MT.clsRegistrations)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRegistrations),
                    Program.gMiddleTier + "clsRegistrations");

                pMdtLabTestGroups = (AfyaPro_MT.clsLabTestGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsLabTestGroups),
                    Program.gMiddleTier + "clsLabTestGroups");

                pMdtLabTestSubGroups = (AfyaPro_MT.clsLabTestSubGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsLabTestSubGroups),
                    Program.gMiddleTier + "clsLabTestSubGroups");

                pMdtLabTests = (AfyaPro_MT.clsLabTests)Activator.GetObject(
                    typeof(AfyaPro_MT.clsLabTests),
                    Program.gMiddleTier + "clsLabTests");

                pMdtLabRemarks = (AfyaPro_MT.clsLabRemarks)Activator.GetObject(
                    typeof(AfyaPro_MT.clsLabRemarks),
                    Program.gMiddleTier + "clsLabRemarks");

                pMdtLaboratories = (AfyaPro_MT.clsLaboratories)Activator.GetObject(
                    typeof(AfyaPro_MT.clsLaboratories),
                    Program.gMiddleTier + "clsLaboratories");

                pMdtMedicalStaffs = (AfyaPro_MT.clsMedicalStaffs)Activator.GetObject(
                    typeof(AfyaPro_MT.clsMedicalStaffs),
                    Program.gMiddleTier + "clsMedicalStaffs");

                pMdtPatientLabTests = (AfyaPro_MT.clsPatientLabTests)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPatientLabTests),
                    Program.gMiddleTier + "clsPatientLabTests");

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.labpatienttests_customizelayout.ToString());

                viewLABPatientTests.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(viewLABPatientTests_CustomRowCellEdit);
                viewLABPatientTests.ValidatingEditor += new BaseContainerValidateEditorEventHandler(viewLABPatientTests_ValidatingEditor);
                viewLABPatientTests.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(viewLABPatientTests_RowCellStyle);

                grdLABPatientTests.ForceInitialize();

                pDtLabTests.Columns.Add("code", typeof(System.String));
                pDtLabTests.Columns.Add("description", typeof(System.String));
                pDtLabTests.Columns.Add("displayname", typeof(System.String));
                pDtLabTests.Columns.Add("groupcode", typeof(System.String));
                pDtLabTests.Columns.Add("groupdescription", typeof(System.String));
                pDtLabTests.Columns.Add("subgroupcode", typeof(System.String));
                pDtLabTests.Columns.Add("subgroupdescription", typeof(System.String));
                pDtLabTests.Columns.Add("resulttype", typeof(System.Int16));
                pDtLabTests.Columns.Add("restricttodropdownlist", typeof(System.Int16));
                pDtLabTests.Columns.Add("equipment_lowerrange", typeof(System.Double));
                pDtLabTests.Columns.Add("equipment_upperrange", typeof(System.Double));
                pDtLabTests.Columns.Add("normal_lowerrange", typeof(System.Double));
                pDtLabTests.Columns.Add("normal_upperrange", typeof(System.Double));
                pDtLabTests.Columns.Add("rejectoutofrange", typeof(System.Int16));
                pDtLabTests.Columns.Add("resultfigure", typeof(System.Object));
                pDtLabTests.Columns.Add("units", typeof(System.String));
                pDtLabTests.Columns.Add("remarks", typeof(System.String));
                pDtLabTests.Columns.Add("normalrange", typeof(System.String));
                pDtLabTests.Columns.Add("equipmentrange", typeof(System.String));
                pDtLabTests.Columns.Add("hasnormalrange", typeof(System.Boolean));
                pDtLabTests.Columns.Add("hasequipmentrange", typeof(System.Boolean));
                pDtLabTests.Columns.Add("outofrange", typeof(System.Int16));
                pDtLabTests.Columns.Add("outofrange_normal", typeof(System.Int16));
                pDtLabTests.Columns.Add("outofrange_equipment", typeof(System.Int16));

                grdLABPatientTests.DataSource = pDtLabTests;

                viewLABPatientTests.Columns["displayname"].Caption = "Test";
                viewLABPatientTests.Columns["resultfigure"].Caption = "Result";
                viewLABPatientTests.Columns["units"].Caption = "Unit";
                viewLABPatientTests.Columns["remarks"].Caption = "Remarks";
                viewLABPatientTests.Columns["normalrange"].Caption = "Normal Range";
                viewLABPatientTests.Columns["equipmentrange"].Caption = "Equipment Range";

                foreach (DevExpress.XtraGrid.Columns.GridColumn mGridColumn in viewLABPatientTests.Columns)
                {
                    if (mGridColumn.FieldName.ToLower() != "resultfigure" && mGridColumn.FieldName.ToLower() != "remarks")
                    {
                        mGridColumn.OptionsColumn.AllowEdit = false;
                        mGridColumn.OptionsColumn.AllowFocus = false;
                    }
                }

                pDtDropDownValues = pMdtLabTests.View_DropDownValues("", "", "", "");
                pDvDropDownValues.Table = pDtDropDownValues;
                pDvDropDownValues.Sort = "labtesttypecode";

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

                pDtClinicalOfficers.Columns.Add("code", typeof(System.String));
                pDtClinicalOfficers.Columns.Add("description", typeof(System.String));
                cboClinicalOfficer.Properties.DataSource = pDtClinicalOfficers;
                cboClinicalOfficer.Properties.DisplayMember = "description";
                cboClinicalOfficer.Properties.ValueMember = "code";

                pDtLabTechnician.Columns.Add("code", typeof(System.String));
                pDtLabTechnician.Columns.Add("description", typeof(System.String));
                cboLabTechnician.Properties.DataSource = pDtLabTechnician;
                cboLabTechnician.Properties.DisplayMember = "description";
                cboLabTechnician.Properties.ValueMember = "code";

                pRemarks = cboRemarks.Items;

                DataTable mDtRemarks = pMdtLabRemarks.View("", "", "", "");
                pRemarks.Add("");
                foreach (DataRow mDataRow in mDtRemarks.Rows)
                {
                    pRemarks.Add(mDataRow["description"].ToString().Trim());
                }
                viewLABPatientTests.Columns["remarks"].ColumnEdit = cboRemarks;

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
            mObjectsList.Add(cmdSearch);

            Program.Apply_Language(this.Name, mObjectsList);
            this.Data_Clear();
        }

        #endregion

        #region frmLABPatientTests_Load
        private void frmLABPatientTests_Load(object sender, EventArgs e)
        {
            this.Top = 0;

            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            this.Load_Controls();

            txbBirthDateFormat.Text = "(" + Program.gCulture.DateTimeFormat.ShortDatePattern + ")";

            //this.Fill_LabTests();

            Program.Restore_GridLayout(grdLABPatientTests, grdLABPatientTests.Name);

            this.pFormWidth = this.Width;
            this.pFormHeight = this.Height;

            Program.Center_Screen(this);

            txtDate.EditValue = Program.gMdiForm.txtDate.EditValue;

            this.Append_ShortcutKeys();
        }
        #endregion

        #region frmLABPatientTests_Shown
        private void frmLABPatientTests_Shown(object sender, EventArgs e)
        {
            //only one laboratory is registered
            if (pDtLaboratories.Rows.Count == 1)
            {
                cboLaboratory.ItemIndex = 0;
            }

            //set to default user
            cboLabTechnician.ItemIndex = Program.Get_LookupItemIndex(cboLabTechnician, "code", Program.gCurrentUser.Code);

            txtPatientId.Focus();
        }
        #endregion

        #region frmLABPatientTests_FormClosing
        private void frmLABPatientTests_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //layout
                if (layoutControl1.IsModified == true)
                {
                    Program.Save_FormLayout(this, layoutControl1, this.Name);
                }

                //grid
                Program.Save_GridLayout(grdLABPatientTests, grdLABPatientTests.Name);
            }
            catch { }
        }
        #endregion

        #region Append_ShortcutKeys
        private void Append_ShortcutKeys()
        {
            cmdSearch.Text = cmdSearch.Text + " (" + Program.KeyCode_SeachPatient.ToString() + ")";
            cmdSave.Text = cmdSave.Text + " (" + Program.KeyCode_Save.ToString() + ")";
            cmdPatientHistory.Text = cmdPatientHistory.Text + " (" + Program.KeyCode_ViewLabTestResults.ToString() + ")";
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

                #region clinicalofficers

                pDtClinicalOfficers.Rows.Clear();

                DataTable mDtClinicalOfficers = pMdtMedicalStaffs.View("category="
                    + (int)AfyaPro_Types.clsEnums.StaffCategories.MedicalDoctors, "", Program.gLanguageName, "grdGENMedicalStaffs");
                foreach (DataRow mDataRow in mDtClinicalOfficers.Rows)
                {
                    mNewRow = pDtClinicalOfficers.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtClinicalOfficers.Rows.Add(mNewRow);
                    pDtClinicalOfficers.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtClinicalOfficers.Columns)
                {
                    mDataColumn.Caption = mDtClinicalOfficers.Columns[mDataColumn.ColumnName].Caption;
                }

                #endregion

                #region labtechnicians

                pDtLabTechnician.Rows.Clear();

                DataTable mDtLabTechnician = pMdtMedicalStaffs.View("category="
                    + (int)AfyaPro_Types.clsEnums.StaffCategories.LabTechnicians, "", Program.gLanguageName, "grdGENMedicalStaffs");
                foreach (DataRow mDataRow in mDtLabTechnician.Rows)
                {
                    mNewRow = pDtLabTechnician.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtLabTechnician.Rows.Add(mNewRow);
                    pDtLabTechnician.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtLabTechnician.Columns)
                {
                    mDataColumn.Caption = mDtLabTechnician.Columns[mDataColumn.ColumnName].Caption;
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
            txtWard.Text = "";
            txtRoom.Text = "";
            txtBed.Text = "";

            pPrevPatientId = "";
            pCurrPatientId = pPrevPatientId;

            txtPatientId.Focus();
        }
        #endregion

        #region Data_Display
        internal void Data_Display(AfyaPro_Types.clsPatient mPatient)
        {
            String mFunctionName = "Data_Display";

            try
            {
                this.Data_Clear();

                if (mPatient != null)
                {
                    if (mPatient.Exist == true)
                    {
                        string mFullName = mPatient.firstname;
                        if (mPatient.othernames.Trim() != "")
                        {
                            mFullName = mFullName + " " + mPatient.othernames;
                        }
                        mFullName = mFullName + " " + mPatient.surname;

                        txtPatientId.Text = mPatient.code;
                        txtName.Text = mFullName;
                        if (mPatient.gender.Trim().ToLower() == "f")
                        {
                            txtGender.Text = "Female";
                        }
                        else
                        {
                            txtGender.Text = "Male";
                        }
                        txtBirthDate.Text = mPatient.birthdate.ToString("d");
                        int mDays = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue).Subtract(mPatient.birthdate).Days;
                        int mYears = (int)mDays / 365;
                        int mMonths = (int)(mDays % 365) / 30;

                        txtYears.Text = mYears.ToString();
                        txtMonths.Text = mMonths.ToString();

                        pCurrentBooking = pMdtRegistrations.Get_Booking(txtPatientId.Text);
                        pCurrentAdmission = pMdtRegistrations.Get_Admission(pCurrentBooking.Booking, txtPatientId.Text);

                        if (pCurrentAdmission != null)
                        {
                            if (pCurrentAdmission.IsAdmitted == true)
                            {
                                txtWard.Text = pCurrentAdmission.WardDescription;
                                txtRoom.Text = pCurrentAdmission.RoomDescription;
                                txtBed.Text = pCurrentAdmission.BedDescription;
                            }
                        }

                        pCurrPatientId = mPatient.code;
                        pPrevPatientId = pCurrPatientId;

                        this.Fill_LabTests();

                        System.Media.SystemSounds.Beep.Play();
                    }
                    else
                    {
                        pCurrPatientId = txtPatientId.Text;
                        pPrevPatientId = pCurrPatientId;
                    }
                }
                else
                {
                    pCurrPatientId = txtPatientId.Text;
                    pPrevPatientId = pCurrPatientId;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_LabTests
        internal void Fill_LabTests()
        {
            string mFunctionName = "Fill_LabTests";

            try
            {
                pDtLabTests.Rows.Clear();

                if (Program.IsNullDate(txtDate.EditValue) == true)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                    txtDate.Focus();
                    return;
                }

                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

                string mBooking = pCurrentBooking.Booking;

                string mFilter = "";

                #region prepare filter

                bool mProcessPrescription = false;

                DataTable mDtPrescriptions = pMdtPatientLabTests.View_Prescription(txtPatientId.Text.Trim(), mTransDate, "");
                if (mDtPrescriptions.Rows.Count > 0)
                {
                    DialogResult mAnswer = Program.Display_Question("There is a prescription associated with this Patient for the selected date"
                        + Environment.NewLine + "Do you want to process it", MessageBoxDefaultButton.Button1);

                    if (mAnswer == System.Windows.Forms.DialogResult.Yes)
                    {
                        foreach (DataRow mDataRow in mDtPrescriptions.Rows)
                        {
                            if (mFilter.Trim() == "")
                            {
                                mFilter = "'" + mDataRow["labtesttypecode"].ToString().Trim() + "'";
                            }
                            else
                            {
                                mFilter = mFilter + ",'" + mDataRow["labtesttypecode"].ToString().Trim() + "'";
                            }
                        }

                        if (mFilter.Trim() != "")
                        {
                            mFilter = " code in (" + mFilter + ")";
                        }

                        mProcessPrescription = true;
                    }
                }

                if (mProcessPrescription == false)
                {
                    if (cboGroup.ItemIndex == -1)
                    {
                        return;
                    }

                    string mSubGroupCode = "";
                    if (cboSubGroup.ItemIndex != -1)
                    {
                        mSubGroupCode = cboSubGroup.GetColumnValue("code").ToString().Trim();
                    }

                    mFilter = "groupcode='" + cboGroup.GetColumnValue("code").ToString().Trim() + "' and subgroupcode='" + mSubGroupCode + "'";
                }

                #endregion

                int mYears = Program.IsNumeric(txtYears.Text) == false ? 0 : Convert.ToInt32(txtYears.Text);
                int mMonths = Program.IsNumeric(txtMonths.Text) == false ? 0 : Convert.ToInt32(txtMonths.Text);

                DataTable mDtLabTests = pMdtLabTests.View_Tests(mFilter, mYears, mMonths);
                DataTable mDtPatientTests = pMdtPatientLabTests.View(true, mTransDate, mTransDate,
                    "patientcode='" + txtPatientId.Text.Trim() + "' and booking='" + mBooking + "'", "", "", "");

                DataView mDvPatientTests = new DataView();
                mDvPatientTests.Table = mDtPatientTests;
                mDvPatientTests.Sort = "labtesttypecode";

                foreach (DataRow mDataRow in mDtLabTests.Rows)
                {
                    string mNormalRange = "";
                    string mEquipmentRange = "";
                    double mEquipmentLowerRange = 0;
                    double mEquipmentUpperRange = 0;
                    double mNormalLowerRange = 0;
                    double mNormalUpperRange = 0;
                    string mResultFigure = "";
                    Int16 mOutOfRange_Normal = 0;
                    Int16 mOutOfRange_Equipment = 0;

                    Int16 mResultType = Convert.ToInt16(mDataRow["resulttype"]);
                    Int16 mRestrictToDropDownList = Convert.ToInt16(mDataRow["restricttodropdownlist"]);
                    string mUnits = mDataRow["units"].ToString();
                    string mRemarks = "";
                    bool mHasNormalRange = false;
                    bool mHasEquipmentRange = false;

                    int mRowIndex = mDvPatientTests.Find(mDataRow["code"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mResultFigure = mDvPatientTests[mRowIndex]["resultfigure"].ToString().Trim();
                        mRemarks = mDvPatientTests[mRowIndex]["remarks"].ToString().Trim();
                        mOutOfRange_Normal = Convert.ToInt16(mDvPatientTests[mRowIndex]["outofrange_normal"]);
                        mOutOfRange_Equipment = Convert.ToInt16(mDvPatientTests[mRowIndex]["outofrange_equipment"]);
                    }

                    if (mResultType == (int)AfyaPro_Types.clsEnums.LabTestResultTypes.number)
                    {
                        switch (txtGender.Text.Trim().ToLower())
                        {
                            case "female":
                                {
                                    mEquipmentLowerRange = Convert.ToDouble(mDataRow["equipment_femalelowerrange"]);
                                    mEquipmentUpperRange = Convert.ToDouble(mDataRow["equipment_femaleupperrange"]);
                                    mNormalLowerRange = Convert.ToDouble(mDataRow["normal_femalelowerrange"]);
                                    mNormalUpperRange = Convert.ToDouble(mDataRow["normal_femaleupperrange"]);
                                }
                                break;
                            case "male":
                                {
                                    mEquipmentLowerRange = Convert.ToDouble(mDataRow["equipment_malelowerrange"]);
                                    mEquipmentUpperRange = Convert.ToDouble(mDataRow["equipment_maleupperrange"]);
                                    mNormalLowerRange = Convert.ToDouble(mDataRow["normal_malelowerrange"]);
                                    mNormalUpperRange = Convert.ToDouble(mDataRow["normal_maleupperrange"]);
                                }
                                break;
                        }

                        if (mEquipmentLowerRange != 0 || mEquipmentUpperRange != 0)
                        {
                            mEquipmentRange = mEquipmentLowerRange + " - " + mEquipmentUpperRange + " " + mUnits;
                            mHasEquipmentRange = true;
                        }

                        if (mNormalLowerRange != 0 || mNormalUpperRange != 0)
                        {
                            mNormalRange = mNormalLowerRange + " - " + mNormalUpperRange + " " + mUnits;
                            mHasNormalRange = true;
                        }
                    }

                    DataRow mNewRow = pDtLabTests.NewRow();
                    mNewRow["code"] = mDataRow["code"];
                    mNewRow["description"] = mDataRow["description"];
                    mNewRow["displayname"] = mDataRow["displayname"];
                    mNewRow["groupcode"] = mDataRow["groupcode"];
                    mNewRow["groupdescription"] = mDataRow["groupdescription"];
                    mNewRow["subgroupcode"] = mDataRow["subgroupcode"];
                    mNewRow["subgroupdescription"] = mDataRow["subgroupdescription"];
                    mNewRow["resulttype"] = mResultType;
                    mNewRow["restricttodropdownlist"] = mRestrictToDropDownList;
                    mNewRow["equipment_lowerrange"] = mEquipmentLowerRange;
                    mNewRow["equipment_upperrange"] = mEquipmentUpperRange;
                    mNewRow["normal_lowerrange"] = mNormalLowerRange;
                    mNewRow["normal_upperrange"] = mNormalUpperRange;
                    mNewRow["rejectoutofrange"] = mDataRow["rejectoutofrange"];
                    mNewRow["resultfigure"] = mResultFigure;
                    mNewRow["units"] = mUnits;
                    mNewRow["remarks"] = mRemarks;
                    mNewRow["normalrange"] = mNormalRange;
                    mNewRow["equipmentrange"] = mEquipmentRange;
                    mNewRow["hasnormalrange"] = mHasNormalRange;
                    mNewRow["hasequipmentrange"] = mHasEquipmentRange;
                    mNewRow["outofrange_normal"] = mOutOfRange_Normal;
                    mNewRow["outofrange_equipment"] = mOutOfRange_Equipment;
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

        #region Search_Patient
        private AfyaPro_Types.clsPatient Search_Patient()
        {
            string mFunctionName = "Search_Patient";

            try
            {
                pCurrentPatient = pMdtRegistrations.Get_Patient(txtPatientId.Text);
                return pCurrentPatient;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
        }
        #endregion

        #region txtPatientId_KeyDown
        private void txtPatientId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.pCurrentPatient = this.Search_Patient();
                this.Data_Display(pCurrentPatient);
            }
        }
        #endregion

        #region txtPatientId_Leave
        private void txtPatientId_Leave(object sender, EventArgs e)
        {
            pCurrPatientId = txtPatientId.Text;

            if (pCurrPatientId.Trim().ToLower() != pPrevPatientId.Trim().ToLower())
            {
                this.pCurrentPatient = this.Search_Patient();
                this.Data_Display(pCurrentPatient);
            }
        }
        #endregion

        #region cmdSearch_Click
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            pSearchingPatient = true;

            frmSearchPatient mSearchPatient = new frmSearchPatient(txtPatientId);
            mSearchPatient.ShowDialog();

            pSearchingPatient = false;
        }
        #endregion

        #region txtPatientId_EditValueChanged
        private void txtPatientId_EditValueChanged(object sender, EventArgs e)
        {
            if (pSearchingPatient == true)
            {
                this.pCurrentPatient = this.Search_Patient();
                this.Data_Display(pCurrentPatient);
            }
        }
        #endregion

        #region viewLABPatientTests_CustomRowCellEdit
        void viewLABPatientTests_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName.ToLower() != "resultfigure")
            {
                return;
            }

            if (e.RowHandle < 0)
            {
                return;
            }

            viewLABPatientTests.PostEditor();

            Int16 mResultType = Convert.ToInt16(viewLABPatientTests.GetRowCellValue(e.RowHandle, viewLABPatientTests.Columns["resulttype"]));
            string mTestTypeCode = viewLABPatientTests.GetRowCellValue(e.RowHandle, viewLABPatientTests.Columns["code"]).ToString().Trim();
            Int16 mRestrictToDropDownList = Convert.ToInt16(viewLABPatientTests.GetRowCellValue(e.RowHandle, viewLABPatientTests.Columns["restricttodropdownlist"]));

            switch (mResultType)
            {
                case 0://negative or positive
                    {
                        e.RepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();

                        DevExpress.XtraEditors.Repository.RepositoryItemComboBox mComboBoxEdit = (DevExpress.XtraEditors.Repository.RepositoryItemComboBox)e.RepositoryItem;
                        mComboBoxEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
                        ComboBoxItemCollection mDropDownItems = mComboBoxEdit.Items;

                        mDropDownItems.Clear();
                        mDropDownItems.Add("Negative");
                        mDropDownItems.Add("Positive");
                    }
                    break;
                case 1://number
                    {
                        e.RepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
                    }
                    break;
                case 2://free text
                    {
                        e.RepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
                    }
                    break;
                case 3://dropdown
                    {
                        e.RepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();

                        DevExpress.XtraEditors.Repository.RepositoryItemComboBox mComboBoxEdit = (DevExpress.XtraEditors.Repository.RepositoryItemComboBox)e.RepositoryItem;

                        if (mRestrictToDropDownList == 1)
                        {
                            mComboBoxEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
                        }
                        ComboBoxItemCollection mDropDownItems = mComboBoxEdit.Items;

                        mDropDownItems.Clear();

                        DataRowView[] mDropDownValues = pDvDropDownValues.FindRows(mTestTypeCode);

                        mDropDownItems.Add("");
                        foreach (DataRowView mDataRowView in mDropDownValues)
                        {
                            mDropDownItems.Add(mDataRowView["description"].ToString().Trim());
                        }
                    }
                    break;
            }
        }
        #endregion

        #region viewLABPatientTests_ValidatingEditor
        private void viewLABPatientTests_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            if (viewLABPatientTests.FocusedColumn.FieldName.Trim().ToLower() == "resultfigure")
            {
                DataRow mDataRow = viewLABPatientTests.GetDataRow(viewLABPatientTests.FocusedRowHandle);
                Int16 mResultType = Convert.ToInt16(mDataRow["resulttype"]);
                string mResult = e.Value.ToString();

                if (e.Value != null && mResult != "")
                {
                    switch (mResultType)
                    {
                        case 1:
                            {
                                //validate input
                                if (Program.IsMoney(e.Value.ToString()) == false)
                                {
                                    e.Valid = false;
                                    e.ErrorText = "Specified result is not valid. The result should be a number";
                                    return;
                                }

                                //out of range determination
                                double mValue = Convert.ToDouble(e.Value);
                                double mNormal_LowerRange = Convert.ToDouble(mDataRow["normal_lowerrange"]);
                                double mNormal_UpperRange = Convert.ToDouble(mDataRow["normal_upperrange"]);
                                double Equipment_LowerRange = Convert.ToDouble(mDataRow["equipment_lowerrange"]);
                                double Equipment_UpperRange = Convert.ToDouble(mDataRow["equipment_upperrange"]);

                                mDataRow.BeginEdit();

                                //outside of normal range
                                if (mValue < mNormal_LowerRange || mValue > mNormal_UpperRange)
                                {
                                    mDataRow["outofrange_normal"] = 1;
                                }
                                else
                                {
                                    mDataRow["outofrange_normal"] = 0;
                                }

                                //outside of equipment range
                                if (mValue < Equipment_LowerRange || mValue > Equipment_UpperRange)
                                {
                                    mDataRow["outofrange_equipment"] = 1;
                                }
                                else
                                {
                                    mDataRow["outofrange_equipment"] = 0;
                                }

                                mDataRow.EndEdit();
                            }
                            break;
                    }
                }
            }
        }
        #endregion

        #region viewLABPatientTests_RowCellStyle
        void viewLABPatientTests_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName.Trim().ToLower() == "resultfigure")
                {
                    DataRow mDataRow = viewLABPatientTests.GetDataRow(e.RowHandle);

                    Int16 mResultType = Convert.ToInt16(mDataRow["resulttype"]);

                    switch (mResultType)
                    {
                        case 1:
                            {
                                try
                                {
                                    double mValue = Convert.ToDouble(e.CellValue);
                                    double mNormal_LowerRange = Convert.ToDouble(mDataRow["normal_lowerrange"]);
                                    double mNormal_UpperRange = Convert.ToDouble(mDataRow["normal_upperrange"]);
                                    double Equipment_LowerRange = Convert.ToDouble(mDataRow["equipment_lowerrange"]);
                                    double Equipment_UpperRange = Convert.ToDouble(mDataRow["equipment_upperrange"]);

                                    //outside of normal range
                                    if ((mValue < mNormal_LowerRange || mValue > mNormal_UpperRange)
                                        && (mNormal_LowerRange != 0 && mNormal_UpperRange != 0))
                                    {
                                        e.Appearance.BackColor = txtNormalRangeColor.BackColor;
                                    }

                                    //outside of equipment range
                                    if ((mValue < Equipment_LowerRange || mValue > Equipment_UpperRange)
                                        && (Equipment_LowerRange != 0 && Equipment_UpperRange != 0))
                                    {
                                        e.Appearance.BackColor = txtEquipmentRangeColor.BackColor;
                                    }
                                }
                                catch { }
                            }
                            break;
                    }
                }
            }
        }
        #endregion

        #region cboGroup_EditValueChanged
        private void cboGroup_EditValueChanged(object sender, EventArgs e)
        {
            string mFunctionName = "cboGroup_EditValueChanged";

            try
            {
                this.Fill_LabTests();
                this.Fill_SubGroups();
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
            string mLaboratoryDescription = "";
            string mClinicalOfficerCode = "";
            string mClinicalOfficerDescription = "";
            string mLabTechnicianCode = "";
            string mLabTechnicianDescription = "";

            string mFunctionName = "Save";

            #region validation

            if (Program.IsDate(txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                txtDate.Focus();
                return;
            }

            if (txtPatientId.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoIsInvalid.ToString());
                txtPatientId.Focus();
                return;
            }

            AfyaPro_Types.clsPatient mPatient = pMdtRegistrations.Get_Patient(txtPatientId.Text);
            if (mPatient.Exist == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoDoesNotExist.ToString());
                txtPatientId.Focus();
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

            //clinicalofficer
            if (txbClinicalOfficer.Visible == true)
            {
                if (cboClinicalOfficer.ItemIndex == -1)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.LAB_ClinicalOfficerIsInvalid.ToString());
                    cboClinicalOfficer.Focus();
                    return;
                }

                mClinicalOfficerCode = cboClinicalOfficer.GetColumnValue("code").ToString().Trim();
                mClinicalOfficerDescription = cboClinicalOfficer.GetColumnValue("description").ToString().Trim();
            }

            //labtechnician
            if (txbLabTechnician.Visible == true)
            {
                if (cboLabTechnician.ItemIndex == -1)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.LAB_LabTechnicianIsInvalid.ToString());
                    cboLabTechnician.Focus();
                    return;
                }

                mLabTechnicianCode = cboLabTechnician.GetColumnValue("code").ToString().Trim();
                mLabTechnicianDescription = cboLabTechnician.GetColumnValue("description").ToString().Trim();
            }

            //labtests
            if (pDtLabTests.Rows.Count == 0)
            {
                Program.Display_Error("Please enter at least one lab test");
                grdLABPatientTests.Focus();
                return;
            }

            #endregion

            try
            {
                if (cboLaboratory.ItemIndex != -1)
                {
                    mLaboratoryCode = cboLaboratory.GetColumnValue("code").ToString().Trim();
                    mLaboratoryDescription = cboLaboratory.GetColumnValue("description").ToString().Trim();
                }

                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

                DataTable mDtLabTests = new DataTable("labtests");
                mDtLabTests.Columns.Add("labtestgroupcode", typeof(System.String));
                mDtLabTests.Columns.Add("labtestgroupdescription", typeof(System.String));
                mDtLabTests.Columns.Add("labtestsubgroupcode", typeof(System.String));
                mDtLabTests.Columns.Add("labtestsubgroupdescription", typeof(System.String));
                mDtLabTests.Columns.Add("labtesttypecode", typeof(System.String));
                mDtLabTests.Columns.Add("labtesttypedescription", typeof(System.String));
                mDtLabTests.Columns.Add("resultfigure", typeof(System.String));
                mDtLabTests.Columns.Add("units", typeof(System.String));
                mDtLabTests.Columns.Add("remarks", typeof(System.String));
                mDtLabTests.Columns.Add("outofrange_normal", typeof(System.Int16));
                mDtLabTests.Columns.Add("outofrange_equipment", typeof(System.Int16));
                mDtLabTests.Columns.Add("delete", typeof(System.Boolean));

                foreach (DataRow mDataRow in pDtLabTests.Rows)
                {
                    bool mDelete = false;

                    if (mDataRow["resultfigure"].ToString().Trim() == "" && mDataRow["remarks"].ToString().Trim() == "")
                    {
                        mDelete = true;
                    }

                    DataRow mNewRow = mDtLabTests.NewRow();
                    mNewRow["labtestgroupcode"] = mDataRow["groupcode"];
                    mNewRow["labtestgroupdescription"] = mDataRow["groupdescription"];
                    mNewRow["labtestsubgroupcode"] = mDataRow["subgroupcode"];
                    mNewRow["labtestsubgroupdescription"] = mDataRow["subgroupdescription"];
                    mNewRow["labtesttypecode"] = mDataRow["code"];
                    mNewRow["labtesttypedescription"] = mDataRow["description"];
                    mNewRow["resultfigure"] = mDataRow["resultfigure"];
                    mNewRow["units"] = mDataRow["units"];
                    mNewRow["remarks"] = mDataRow["remarks"];
                    mNewRow["outofrange_normal"] = mDataRow["outofrange_normal"];
                    mNewRow["outofrange_equipment"] = mDataRow["outofrange_equipment"];
                    mNewRow["delete"] = mDelete;
                    mDtLabTests.Rows.Add(mNewRow);
                    mDtLabTests.AcceptChanges();
                }

                pResult = pMdtPatientLabTests.Save(mTransDate,
                                                    txtPatientId.Text,
                                                    mLaboratoryCode,
                                                    mClinicalOfficerCode,
                                                    mLabTechnicianCode,
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

                Program.Display_Info(AfyaPro_Types.clsSystemMessages.MessageIds.LAB_PatientLabTestsSaved.ToString());
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

        #region View_LabTestResults
        private void View_LabTestResults()
        {
            if (txtPatientId.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoIsInvalid.ToString());
                txtPatientId.Focus();
                return;
            }

            AfyaPro_Types.clsPatient mPatient = pMdtRegistrations.Get_Patient(txtPatientId.Text);
            if (mPatient.Exist == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoDoesNotExist.ToString());
                txtPatientId.Focus();
                return;
            }

            string mGender = "f";
            if (mPatient.gender.Trim().ToLower() == "male")
            {
                mGender = "m";
            }

            int mYears = Program.IsNumeric(txtYears.Text) == false ? 0 : Convert.ToInt32(txtYears.Text);
            int mMonths = Program.IsNumeric(txtMonths.Text) == false ? 0 : Convert.ToInt32(txtMonths.Text);

            frmLABPatientHistory mLABPatientHistory = new frmLABPatientHistory(txtPatientId.Text, mGender, mYears, mMonths);
            mLABPatientHistory.ShowDialog();
        }
        #endregion

        #region cmdPatientHistory_Click
        private void cmdPatientHistory_Click(object sender, EventArgs e)
        {
            this.View_LabTestResults();
        }
        #endregion

        #region txtDate_EditValueChanged
        private void txtDate_EditValueChanged(object sender, EventArgs e)
        {
            this.Fill_LabTests();
        }
        #endregion

        #region frmLABPatientTests_KeyDown
        void frmLABPatientTests_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Program.KeyCode_Save:
                    {
                        this.Save();
                    }
                    break;
                case Program.KeyCode_SeachPatient:
                    {
                        pSearchingPatient = true;

                        frmSearchPatient mSearchPatient = new frmSearchPatient(txtPatientId);
                        mSearchPatient.ShowDialog();
                        if (mSearchPatient.SearchingDone == true)
                        {
                            cboGroup.Focus();
                        }

                        pSearchingPatient = false;
                    }
                    break;
                case Program.KeyCode_ViewLabTestResults:
                    {
                        this.View_LabTestResults();
                    }
                    break;
            }
        }
        #endregion

        #region cmdPatientsQueue_Click
        private void cmdPatientsQueue_Click(object sender, EventArgs e)
        {
            if (Program.IsDate(Program.gMdiForm.txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                return;
            }
            if (cboLaboratory.ItemIndex == -1)
            {
                return;
            }

            frmQueueTreatmentPoint mQueueTreatmentPoint = new frmQueueTreatmentPoint();
            mQueueTreatmentPoint.TreatmentPointCode = cboLaboratory.GetColumnValue("code").ToString().Trim();
            mQueueTreatmentPoint.TransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);
            mQueueTreatmentPoint.QueueType = (int)AfyaPro_Types.clsEnums.PatientsQueueTypes.LabTests;
            mQueueTreatmentPoint.ShowDialog();

            if (mQueueTreatmentPoint.PatientSelected == true)
            {
                txtPatientId.Text = mQueueTreatmentPoint.PatientCode;
                this.pCurrentPatient = this.Search_Patient();
                this.Data_Display(pCurrentPatient);
                this.Fill_LabTests();
            }
        }
        #endregion
    }
}