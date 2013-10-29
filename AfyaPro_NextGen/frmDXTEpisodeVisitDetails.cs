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
    public partial class frmDXTEpisodeVisitDetails : DevExpress.XtraEditors.XtraForm
    {
        internal object gIpdAdmission = null;
        internal object gIpdDischarge = null;
        private string pDataState = "New";

        #region declaration

        private AfyaPro_MT.clsMedicalStaffs pMdtMedicalStaffs;
        private AfyaPro_MT.clsDiagnoses pMdtDiagnoses;
        private AfyaPro_MT.clsRegistrations pMdtRegistrations;
        private AfyaPro_MT.clsPatientDiagnoses pMdtPatientDiagnoses;
        private AfyaPro_MT.clsClientGroups pMdtClientGroups;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        internal AfyaPro_Types.clsPatient gCurrentPatient;
        internal AfyaPro_Types.clsBooking gCurrentBooking = new AfyaPro_Types.clsBooking();
        internal bool gBillingOk = false;
        internal bool gCharge = false;
        internal AfyaPro_Types.clsPatientDiagnosis gPatientDiagnosis = null;

        private string pEpisodeCode = "";
        internal string EpisodeCode
        {
            set { pEpisodeCode = value; }
            get { return pEpisodeCode; }
        }
        internal DataTable gDtDiagnosesPrescriptions = new DataTable("diagnosesprescriptions");

        private bool pRecordSaved = false;
        internal bool RecordSaved
        {
            set { pRecordSaved = value; }
            get { return pRecordSaved; }
        }

        private string pDoctorCode = "";
        internal string DoctorCode
        {
            set { pDoctorCode = value; }
            get { return pDoctorCode; }
        }

        private int pSelectedAutoCode = 0;
        internal int SelectedAutoCode
        {
            set { pSelectedAutoCode = value; }
            get { return pSelectedAutoCode; }
        }

        private string pCurrDiagnosis = "";
        private string pPrevDiagnosis = "";
        private bool pSearchingDiagnosis = false;
        private Int16 pDiagnosesSensitive = 0;

        private DataTable pDtMedicalStaffs = new DataTable("medicalstaffs");

        #endregion

        #region frmDXTEpisodeVisitDetails
        public frmDXTEpisodeVisitDetails(string mDataState = "New")
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmDXTEpisodeVisitDetails";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                pDataState = mDataState;

                pMdtRegistrations = (AfyaPro_MT.clsRegistrations)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRegistrations),
                    Program.gMiddleTier + "clsRegistrations");

                pMdtMedicalStaffs = (AfyaPro_MT.clsMedicalStaffs)Activator.GetObject(
                    typeof(AfyaPro_MT.clsMedicalStaffs),
                    Program.gMiddleTier + "clsMedicalStaffs");

                pMdtDiagnoses = (AfyaPro_MT.clsDiagnoses)Activator.GetObject(
                    typeof(AfyaPro_MT.clsDiagnoses),
                    Program.gMiddleTier + "clsDiagnoses");

                pMdtPatientDiagnoses = (AfyaPro_MT.clsPatientDiagnoses)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPatientDiagnoses),
                    Program.gMiddleTier + "clsPatientDiagnoses");

                pMdtClientGroups = (AfyaPro_MT.clsClientGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsClientGroups),
                    Program.gMiddleTier + "clsClientGroups");

                pDtMedicalStaffs.Columns.Add("code", typeof(System.String));
                pDtMedicalStaffs.Columns.Add("description", typeof(System.String));
                pDtMedicalStaffs.Columns.Add("treatmentpointcode", typeof(System.String));
                cboDoctor.Properties.DataSource = pDtMedicalStaffs;
                cboDoctor.Properties.DisplayMember = "description";
                cboDoctor.Properties.ValueMember = "code";

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.dxtpatientdiagnoses_customizelayout.ToString());

                this.Fill_LookupData();

                if (Program.IsNullDate(txtDate.EditValue) == true)
                {
                    txtDate.EditValue = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue).Date;
                }

                radPrimary.SelectedIndex = -1;
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
            mObjectsList.Add(txbDoctor);
            mObjectsList.Add(cmdOk);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }

        #endregion

        #region frmDXTEpisodeVisitDetails_Load
        private void frmDXTEpisodeVisitDetails_Load(object sender, EventArgs e)
        {
            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            this.Load_Controls();

            txbBirthDateFormat.Text = "(" + Program.gCulture.DateTimeFormat.ShortDatePattern + ")";

            Program.Center_Screen(this);

            this.Data_Display(gCurrentPatient);

            if (gIpdAdmission != null)
            {
                //txbPrimary.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;
            }
            else if (gIpdDischarge != null)
            {
                txbPrimary.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }

            this.Append_ShortcutKeys();
        }
        #endregion

        #region frmDXTEpisodeVisitDetails_Shown
        private void frmDXTEpisodeVisitDetails_Shown(object sender, EventArgs e)
        {
            //set clinical officer to user
            if (pDoctorCode.Trim() != "")
            {
                cboDoctor.ItemIndex = Program.Get_LookupItemIndex(cboDoctor, "code", pDoctorCode);
            }
            else
            {
                cboDoctor.ItemIndex = Program.Get_LookupItemIndex(cboDoctor, "code", Program.gCurrentUser.Code);
            }

            txtDxCode.Focus();
        }
        #endregion

        #region frmDXTEpisodeVisitDetails_FormClosing
        private void frmDXTEpisodeVisitDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //layout
                if (layoutControl1.IsModified == true)
                {
                    Program.Save_FormLayout(this, layoutControl1, this.Name);
                }
            }
            catch { }
        }
        #endregion

        #region Append_ShortcutKeys
        private void Append_ShortcutKeys()
        {
            //cmdOk.Text = cmdOk.Text + " (" + Program.KeyCode_Save.ToString() + ")";
            //cmdLabTestResults.Text = cmdLabTestResults.Text + " (" + Program.KeyCode_ViewLabTestResults.ToString() + ")";
            //cmdSearchDx.Text = cmdSearchDx.Text + " (" + Program.KeyCode_SearchDiagnosis.ToString() + ")";
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

                pDtMedicalStaffs.Rows.Clear();

                DataTable mDtMedicalStaffs = pMdtMedicalStaffs.View(
                    "category=" + Convert.ToInt16(AfyaPro_Types.clsEnums.StaffCategories.MedicalDoctors)
                    + " or category=" + Convert.ToInt16(AfyaPro_Types.clsEnums.StaffCategories.Nurses),
                    "description", Program.gLanguageName, "grdGENMedicalStaffs");

                mNewRow = pDtMedicalStaffs.NewRow();
                mNewRow["code"] = "";
                mNewRow["description"] = "Unknown Clinician";
                mNewRow["treatmentpointcode"] = "";
                pDtMedicalStaffs.Rows.Add(mNewRow);
                pDtMedicalStaffs.AcceptChanges();
                foreach (DataRow mDataRow in mDtMedicalStaffs.Rows)
                {
                    mNewRow = pDtMedicalStaffs.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    mNewRow["treatmentpointcode"] = mDataRow["treatmentpointcode"].ToString();
                    pDtMedicalStaffs.Rows.Add(mNewRow);
                    pDtMedicalStaffs.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtMedicalStaffs.Columns)
                {
                    mDataColumn.Caption = mDtMedicalStaffs.Columns[mDataColumn.ColumnName].Caption;
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

        #region Data_Display
        internal void Data_Display(AfyaPro_Types.clsPatient mPatient)
        {
            String mFunctionName = "Data_Display";

            try
            {
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
                        int mDays = Convert.ToDateTime(txtDate.EditValue).Subtract(mPatient.birthdate).Days;
                        int mYears = (int)mDays / 365;
                        int mMonths = (int)(mDays % 365) / 30;

                        txtYears.Text = mYears.ToString();
                        txtMonths.Text = mMonths.ToString();

                        if (pDataState.Trim().ToLower() == "new")
                        {
                            #region get current booking

                            if (gIpdAdmission == null && gIpdDischarge == null)
                            {
                                gCurrentBooking = pMdtRegistrations.Get_Booking(txtPatientId.Text);
                            }

                            if (gCurrentBooking.IsBooked == false)
                            {
                                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientIsNotBooked.ToString());
                                return;
                            }
                            else
                            {
                                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

                                if (gCurrentBooking.BookDate.Date != mTransDate.Date)
                                {
                                    //retry getting booking
                                    gCurrentBooking = pMdtRegistrations.Get_Booking(txtPatientId.Text);
                                    if (gCurrentBooking.IsBooked == false)
                                    {
                                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientIsNotBooked.ToString());
                                        return;
                                    }
                                    else
                                    {
                                        if (gCurrentBooking.BookDate.Date != mTransDate.Date)
                                        {
                                            if (gCurrentBooking.Department.Trim().ToLower() !=
                                                AfyaPro_Types.clsEnums.PatientCategories.IPD.ToString().ToLower())
                                            {
                                                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientIsNotBooked.ToString());
                                                return;
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion

                            #region group is sensitive to diagnoses ?

                            DataTable mDtCustomerGroups = pMdtClientGroups.View("code='" + gCurrentBooking.BillingGroupCode + "'", "", "", "");

                            if (mDtCustomerGroups.Rows.Count > 0)
                            {
                                pDiagnosesSensitive = Convert.ToInt16(mDtCustomerGroups.Rows[0]["diagnosessensitive"]);
                            }

                            #endregion
                        }

                        System.Media.SystemSounds.Beep.Play();

                        cboDoctor.Focus();
                    }
                }
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

        #region Data_New
        private void Data_New()
        {
            AfyaPro_Types.clsBill mBill = new AfyaPro_Types.clsBill();
            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            string mDiagnosisCode = "";
            int mIsPrimary = 0;

            string mFunctionName = "Data_New";

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

            if (cboDoctor.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DoctorIsInvalid.ToString());
                cboDoctor.Focus();
                return;
            }

            #region get current booking

            if (gCurrentBooking.IsBooked == false)
            {
                gCurrentBooking = pMdtRegistrations.Get_Booking(txtPatientId.Text);
            }

            if (gCurrentBooking.IsBooked == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientIsNotBooked.ToString());
                return;
            }
            else
            {
                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

                if (gCurrentBooking.BookDate.Date != mTransDate.Date)
                {
                    //retry getting booking
                    gCurrentBooking = pMdtRegistrations.Get_Booking(txtPatientId.Text);
                    if (gCurrentBooking.IsBooked == false)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientIsNotBooked.ToString());
                        return;
                    }
                    else
                    {
                        if (gCurrentBooking.BookDate.Date != mTransDate.Date)
                        {
                            if (gCurrentBooking.Department.Trim().ToLower() !=
                                AfyaPro_Types.clsEnums.PatientCategories.IPD.ToString().ToLower())
                            {
                                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientIsNotBooked.ToString());
                                return;
                            }
                        }
                    }
                }
            }

            #endregion

            #region diagnosis

            if (txtDxCode.Text.Trim() != "")
            {
                DataTable mDtDiagnoses = pMdtDiagnoses.View("code='" + txtDxCode.Text.Trim() + "'", "", "", "", true);
                if (mDtDiagnoses.Rows.Count == 0)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DiagnosisCodeIsInvalid.ToString());
                    txtDxCode.Focus();
                    return;
                }

                if (gCurrentBooking.BillingGroupCode.Trim() != "")
                {
                    DataTable mDtGroupDiagnoses = pMdtClientGroups.View_Diagnoses(
                        "groupcode='" + gCurrentBooking.BillingGroupCode.Trim()
                        + "' and diagnosiscode='" + txtDxCode.Text.Trim() + "'", "", "", "");
                    if (mDtGroupDiagnoses.Rows.Count == 0 && pDiagnosesSensitive == 1)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DiagnosisIsNotApplicableToCurrentGroup.ToString());
                        txtDxCode.Focus();
                        return;
                    }
                }

                mDiagnosisCode = txtDxCode.Text.Trim();

                if (radPrimary.SelectedIndex == -1)
                {
                    Program.Display_Error("Please select whether this diagnosis is Primary or Secondary");
                    radPrimary.Focus();
                    return;
                }

                //determine primary/secondary
                mIsPrimary = 1;
                if (txbPrimary.Visible == true)
                {
                    if (radPrimary.SelectedIndex == 1)
                    {
                        mIsPrimary = 0;
                    }
                }
            }

            #endregion

            #endregion

            try
            {
                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

                //capture diagnosis details
                gPatientDiagnosis = new AfyaPro_Types.clsPatientDiagnosis();
                gPatientDiagnosis.diagnosiscode = mDiagnosisCode;
                gPatientDiagnosis.isprimary = mIsPrimary;
                gPatientDiagnosis.doctorcode = cboDoctor.GetColumnValue("code").ToString().Trim();
                gPatientDiagnosis.history = txtHistory.Text.Trim();
                gPatientDiagnosis.examination = txtExamination.Text.Trim();
                gPatientDiagnosis.investigation = txtInvestigation.Text.Trim();
                gPatientDiagnosis.treatments = txtTreatments.Text.Trim();
                gPatientDiagnosis.episodecode = pEpisodeCode;

                //billing
                gBillingOk = true;

                if (gCharge == true)
                {
                    gBillingOk = false;

                    #region billing + admission + diagnoses

                    frmBILBillPosting mBILBillPosting = new frmBILBillPosting();
                    mBILBillPosting.gPatientDiagnosisForm = this;
                    mBILBillPosting.gIpdAdmission = gIpdAdmission;
                    mBILBillPosting.gIpdDischarge = gIpdDischarge;

                    mBILBillPosting.gCurrentBooking.BillingGroupCode = gCurrentBooking.BillingGroupCode;
                    mBILBillPosting.gCurrentBooking.BillingGroupDescription = gCurrentBooking.BillingGroupDescription;
                    mBILBillPosting.gCurrentBooking.BillingSubGroupCode = gCurrentBooking.BillingSubGroupCode;
                    mBILBillPosting.gCurrentBooking.BillingSubGroupDescription = gCurrentBooking.BillingSubGroupDescription;
                    mBILBillPosting.gCurrentBooking.BillingGroupMembershipNo = gCurrentBooking.BillingGroupMembershipNo;
                    mBILBillPosting.gCurrentBooking.WhereTakenCode = "IPD";
                    mBILBillPosting.gCurrentBooking.WhereTaken = "IPD";
                    mBILBillPosting.gCurrentBooking.PatientCode = txtPatientId.Text.Trim();
                    mBILBillPosting.gCurrentBooking.IsBooked = true;
                    mBILBillPosting.gCurrentBooking.BookDate = Convert.ToDateTime(txtDate.EditValue);
                    mBILBillPosting.gCurrentPatient = gCurrentPatient;

                    mBILBillPosting.gCurrentBooking.PriceName = Program.gCurrentUser.DefaultPriceCategoryCode;

                    mBILBillPosting.ShowDialog();

                    #endregion

                    if (gBillingOk == false)
                    {
                        return;
                    }

                    txtPatientId.Text = "";
                    pRecordSaved = true;
                    this.Close();
                }

                else
                {
                    DataTable mDtAdmissionDetails = new DataTable("admissiondetails");
                    DataTable mDtDischargeDetails = new DataTable("dischargedetails");

                    #region initiated from IPD admission

                    if (gIpdAdmission != null)
                    {
                        mDtAdmissionDetails.Columns.Add("admissionid", typeof(System.Int32));
                        mDtAdmissionDetails.Columns.Add("wardcode", typeof(System.String));
                        mDtAdmissionDetails.Columns.Add("roomcode", typeof(System.String));
                        mDtAdmissionDetails.Columns.Add("bedno", typeof(System.String));
                        mDtAdmissionDetails.Columns.Add("remarks", typeof(System.String));
                        mDtAdmissionDetails.Columns.Add("weight", typeof(System.Double));
                        mDtAdmissionDetails.Columns.Add("temperature", typeof(System.Double));
                        mDtAdmissionDetails.Columns.Add("billinggroupcode", typeof(System.String));
                        mDtAdmissionDetails.Columns.Add("billingsubgroupcode", typeof(System.String));
                        mDtAdmissionDetails.Columns.Add("billinggroupmembershipno", typeof(System.String));

                        frmIPDRegistrations mIPDRegistrations = (frmIPDRegistrations)gIpdAdmission;
                        DataRow mNewRow = mDtAdmissionDetails.NewRow();
                        mNewRow["admissionid"] = mIPDRegistrations.gAdmissionId;
                        mNewRow["wardcode"] = mIPDRegistrations.gWardCode;
                        mNewRow["roomcode"] = mIPDRegistrations.gRoomCode;
                        mNewRow["bedno"] = mIPDRegistrations.gBedNo;
                        mNewRow["remarks"] = mIPDRegistrations.gRemarks;
                        mNewRow["weight"] = mIPDRegistrations.gWeight;
                        mNewRow["temperature"] = mIPDRegistrations.gTemperature;
                        mNewRow["billinggroupcode"] = mIPDRegistrations.gBillingGroupCode;
                        mNewRow["billingsubgroupcode"] = mIPDRegistrations.gBillingSubGroupCode;
                        mNewRow["billinggroupmembershipno"] = mIPDRegistrations.gBillingGroupMembershipNo;
                        mDtAdmissionDetails.Rows.Add(mNewRow);
                        mDtAdmissionDetails.AcceptChanges();
                    }

                    #endregion

                    #region initiated from IPD discharge

                    if (gIpdDischarge != null)
                    {
                        mDtDischargeDetails.Columns.Add("admissionid", typeof(System.Int32));
                        mDtDischargeDetails.Columns.Add("wardcode", typeof(System.String));
                        mDtDischargeDetails.Columns.Add("roomcode", typeof(System.String));
                        mDtDischargeDetails.Columns.Add("bedno", typeof(System.String));
                        mDtDischargeDetails.Columns.Add("dischargestatuscode", typeof(System.String));
                        mDtDischargeDetails.Columns.Add("dischargestatusdescription", typeof(System.String));
                        mDtDischargeDetails.Columns.Add("remarks", typeof(System.String));

                        frmIPDDischarges mIPDDischarges = (frmIPDDischarges)gIpdDischarge;
                        DataRow mNewRow = mDtDischargeDetails.NewRow();
                        mNewRow["admissionid"] = mIPDDischarges.gAdmissionId;
                        mNewRow["wardcode"] = mIPDDischarges.gWardCode;
                        mNewRow["roomcode"] = mIPDDischarges.gRoomCode;
                        mNewRow["bedno"] = mIPDDischarges.gBedNo;
                        mNewRow["dischargestatuscode"] = mIPDDischarges.cboDischargeStatus.GetColumnValue("code").ToString();
                        mNewRow["dischargestatusdescription"] = mIPDDischarges.cboDischargeStatus.GetColumnValue("description").ToString();
                        mNewRow["remarks"] = mIPDDischarges.gRemarks;
                        mDtDischargeDetails.Rows.Add(mNewRow);
                        mDtDischargeDetails.AcceptChanges();
                    }

                    #endregion

                    //diagnose & treat 
                    mResult = pMdtPatientDiagnoses.Add(
                        mTransDate,
                        txtPatientId.Text,
                        gPatientDiagnosis,
                        mDtAdmissionDetails,
                        mDtDischargeDetails,
                        Program.gCurrentUser.Code);

                    if (mResult.Exe_Result == 0)
                    {
                        Program.Display_Error(mResult.Exe_Message);
                        return;
                    }
                    if (mResult.Exe_Result == -1)
                    {
                        Program.Display_Server_Error(mResult.Exe_Message);
                        return;
                    }

                    if (gIpdAdmission != null)
                    {
                        frmIPDRegistrations mIPDRegistrations = (frmIPDRegistrations)gIpdAdmission;
                        mIPDRegistrations.gCurrentBooking.IsNewAttendance = mBill.IsNewAttendance;
                        mIPDRegistrations.gDiagnosisOk = true;
                        pRecordSaved = true;
                        this.Close();
                    }
                    else if (gIpdDischarge != null)
                    {
                        frmIPDDischarges mIPDDischarges = (frmIPDDischarges)gIpdDischarge;
                        mIPDDischarges.gDiagnosisOk = true;
                        pRecordSaved = true;
                        this.Close();
                    }
                    else
                    {
                        txtPatientId.Text = "";
                        txtPatientId.Focus();
                        pRecordSaved = true;
                        this.Close();
                    }
                }
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
            AfyaPro_Types.clsBill mBill = new AfyaPro_Types.clsBill();
            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            string mDiagnosisCode = "";
            int mIsPrimary = 0;

            string mFunctionName = "Data_Edit";

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

            if (cboDoctor.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DoctorIsInvalid.ToString());
                cboDoctor.Focus();
                return;
            }

            #region diagnosis

            if (txtDxCode.Text.Trim() != "")
            {
                DataTable mDtDiagnoses = pMdtDiagnoses.View("code='" + txtDxCode.Text.Trim() + "'", "", "", "", true);
                if (mDtDiagnoses.Rows.Count == 0)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DiagnosisCodeIsInvalid.ToString());
                    txtDxCode.Focus();
                    return;
                }

                mDiagnosisCode = txtDxCode.Text.Trim();

                if (radPrimary.SelectedIndex == -1)
                {
                    Program.Display_Error("Please select whether this diagnosis is Primary or Secondary");
                    radPrimary.Focus();
                    return;
                }

                //determine primary/secondary
                mIsPrimary = 1;
                if (txbPrimary.Visible == true)
                {
                    if (radPrimary.SelectedIndex == 1)
                    {
                        mIsPrimary = 0;
                    }
                }
            }

            #endregion

            #endregion

            try
            {
                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

                //capture diagnosis details
                gPatientDiagnosis = new AfyaPro_Types.clsPatientDiagnosis();
                gPatientDiagnosis.diagnosiscode = mDiagnosisCode;
                gPatientDiagnosis.isprimary = mIsPrimary;
                gPatientDiagnosis.doctorcode = cboDoctor.GetColumnValue("code").ToString().Trim();
                gPatientDiagnosis.history = txtHistory.Text.Trim();
                gPatientDiagnosis.examination = txtExamination.Text.Trim();
                gPatientDiagnosis.investigation = txtInvestigation.Text.Trim();
                gPatientDiagnosis.treatments = txtTreatments.Text.Trim();
                gPatientDiagnosis.episodecode = pEpisodeCode;

                //diagnose & treat 
                mResult = pMdtPatientDiagnoses.Edit(
                    pSelectedAutoCode,
                    mTransDate,
                    gPatientDiagnosis,
                    Program.gCurrentUser.Code, txtPatientId.Text);

                if (mResult.Exe_Result == 0)
                {
                    Program.Display_Error(mResult.Exe_Message);
                    return;
                }
                if (mResult.Exe_Result == -1)
                {
                    Program.Display_Server_Error(mResult.Exe_Message);
                    return;
                }

                txtPatientId.Text = "";
                txtPatientId.Focus();
                pRecordSaved = true;
                this.Close();
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
            switch (pDataState.Trim().ToLower())
            {
                case "new": this.Data_New(); break;
                case "edit": this.Data_Edit(); break;
            }
        }
        #endregion

        #region Display_Diagnosis
        private void Display_Diagnosis()
        {
            string mFunctionName = "Display_Diagnosis";

            try
            {
                if (gCurrentBooking.BillingGroupCode.Trim() != "")
                {
                    DataTable mDtGroupDiagnoses = pMdtClientGroups.View_Diagnoses(
                        "groupcode='" + gCurrentBooking.BillingGroupCode.Trim()
                        + "' and diagnosiscode='" + txtDxCode.Text.Trim() + "'", "", "", "");
                    if (mDtGroupDiagnoses.Rows.Count == 0)
                    {
                        if (pDiagnosesSensitive == 1)
                        {
                            Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DiagnosisIsNotApplicableToCurrentGroup.ToString());
                            txtDxCode.Focus();
                            return;
                        }
                    }
                }

                DataTable mDtDiagnoses = pMdtDiagnoses.View(
                    "code='" + txtDxCode.Text.Trim() + "'", "", "", "", true);

                if (mDtDiagnoses.Rows.Count > 0)
                {
                    txtDxCode.Text = mDtDiagnoses.Rows[0]["code"].ToString().Trim();
                    txtDxDescription.Text = mDtDiagnoses.Rows[0]["description"].ToString().Trim();

                    pCurrDiagnosis = txtDxCode.Text;
                    pPrevDiagnosis = pCurrDiagnosis;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Search_Diagnoses
        private void Search_Diagnoses()
        {
            string mFunctionName = "Search_Diagnoses";

            try
            {
                pSearchingDiagnosis = true;

                bool mDiagnosesSensitive = false;

                DataTable mDtGroups = pMdtClientGroups.View("code='" + gCurrentBooking.BillingGroupCode + "'", "", "", "");
                if (mDtGroups.Rows.Count > 0)
                {
                    mDiagnosesSensitive = Convert.ToBoolean(mDtGroups.Rows[0]["diagnosessensitive"]);
                }

                DataTable mDtGroupDiagnoses = null;

                if (mDiagnosesSensitive == true)
                {
                    mDtGroupDiagnoses = pMdtClientGroups.View_Diagnoses("groupcode='" + gCurrentBooking.BillingGroupCode.Trim() + "'", "", "", "");
                }

                frmSearchDiagnosis mSearchDiagnosis = new frmSearchDiagnosis(txtDxCode, mDiagnosesSensitive, mDtGroupDiagnoses);
                mSearchDiagnosis.ShowDialog();

                pSearchingDiagnosis = false;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdSearchDx_Click
        private void cmdSearchDx_Click(object sender, EventArgs e)
        {
            this.Search_Diagnoses();
        }
        #endregion

        #region txtDxCode_EditValueChanged
        private void txtDxCode_EditValueChanged(object sender, EventArgs e)
        {
            if (pSearchingDiagnosis == true)
            {
                this.Display_Diagnosis();
            }
        }
        #endregion

        #region txtDxCode_KeyDown
        private void txtDxCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Display_Diagnosis();
            }
        }
        #endregion

        #region txtDxCode_Leave
        private void txtDxCode_Leave(object sender, EventArgs e)
        {
            pCurrDiagnosis = txtDxCode.Text;

            if (pCurrDiagnosis.Trim().ToLower() != pPrevDiagnosis.Trim().ToLower())
            {
                this.Display_Diagnosis();
            }
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

        #region cmdLabTestResults_Click
        private void cmdLabTestResults_Click(object sender, EventArgs e)
        {
            this.View_LabTestResults();
        }
        #endregion

        #region frmDXTEpisodeVisitDetails_KeyDown
        void frmDXTEpisodeVisitDetails_KeyDown(object sender, KeyEventArgs e)
        {
            //switch (e.KeyCode)
            //{
            //    case Program.KeyCode_Save:
            //        {
            //            this.Save();
            //        }
            //        break;
            //    case Program.KeyCode_ViewLabTestResults:
            //        {
            //            this.View_LabTestResults();
            //        }
            //        break;
            //    case Program.KeyCode_SearchDiagnosis:
            //        {
            //            this.Search_Diagnoses();
            //        }
            //        break;
            //}
        }
        #endregion

        #region cmdPrescribeLabTests_Click
        private void cmdPrescribeLabTests_Click(object sender, EventArgs e)
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

            frmDXTPrescribeLabTests mDXTPrescribeLabTests = new frmDXTPrescribeLabTests(mPatient);
            mDXTPrescribeLabTests.ShowDialog();
        }
        #endregion

        #region cmdPrescribeTreatments_Click
        private void cmdPrescribeTreatments_Click(object sender, EventArgs e)
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

            if (txtDxCode.Text.Trim() == "")
            {
                DialogResult mAnswer = Program.Display_Question("You are about to prescribe treatments without diagnosis"
                    + Environment.NewLine + "Are you sure you want to proceed with Prescription", MessageBoxDefaultButton.Button2);
                if (mAnswer != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }
            }

            frmDXTPrescribeTreatments mDXTPrescribeTreatments = new frmDXTPrescribeTreatments(mPatient);
            mDXTPrescribeTreatments.DiagnosisCode = txtDxCode.Text;
            
            if (cboDoctor.ItemIndex == -1)
            {
                Program.Display_Error("Please select a clinical officer and try again");
                cboDoctor.Focus();
                return;
            }
            
            mDXTPrescribeTreatments.DoctorCode = cboDoctor.GetColumnValue("code").ToString().Trim();

            mDXTPrescribeTreatments.ShowDialog();

            txtTreatments.Text = mDXTPrescribeTreatments.Treatments;
        }
        #endregion
    }
}