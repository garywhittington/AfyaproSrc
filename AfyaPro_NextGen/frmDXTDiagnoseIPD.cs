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
    public partial class frmDXTDiagnoseIPD : DevExpress.XtraEditors.XtraForm
    {
        internal object gIpdAdmission = null;
        internal object gIpdDischarge = null;

        #region declaration

        private AfyaPro_MT.clsMedicalStaffs pMdtMedicalStaffs;
        private AfyaPro_MT.clsDiagnoses pMdtDiagnoses;
        private AfyaPro_MT.clsRegistrations pMdtRegistrations;
        private AfyaPro_MT.clsPatientDiagnoses pMdtPatientDiagnoses;
        private AfyaPro_MT.clsTreatmentPoints pMdtTreatmentPoints;
        private AfyaPro_MT.clsClientGroups pMdtClientGroups;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        internal AfyaPro_Types.clsPatient gCurrentPatient;
        internal AfyaPro_Types.clsBooking gCurrentBooking = new AfyaPro_Types.clsBooking();
        internal bool gBillingOk = false;
        internal string gDoctorCode = "";
        internal string gDoctorDescription = "";
        internal string gHistory = "";
        internal string gExamination = "";
        internal string gInvestigation = "";
        internal string gTreatments = "";
        internal DataTable gDtDiagnoses = new DataTable("patientdiagnoses");
        internal Int16 gDeathStatus = 0;
        internal bool gCharge = false;
        internal DataTable gDtDiagnosesReferalInfo = new DataTable("diagnosesreferalinfo");
        internal DataTable gDtDiagnosesPrescriptions = new DataTable("diagnosesprescriptions");
        private DataTable pDtTreatmentPoints = new DataTable("treatmentpoints");

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private string pCurrPatientId = "";
        private string pPrevPatientId = "";
        private string pCurrDiagnosis = "";
        private string pPrevDiagnosis = "";
        private bool pSearchingPatient = false;
        private bool pSearchingDiagnosis = false;
        private Int16 pDiagnosesSensitive = 0;
        private string pEpisodeCode = "";

        private DataTable pDtMedicalStaffs = new DataTable("medicalstaffs");
        private DataTable pDtEpisodes = new DataTable("dxepisodes");

        #endregion

        #region frmDXTDiagnoseIPD
        public frmDXTDiagnoseIPD()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmDXTDiagnoseIPD";

            try
            {
                this.Icon = Program.gMdiForm.Icon;

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

                pMdtTreatmentPoints = (AfyaPro_MT.clsTreatmentPoints)Activator.GetObject(
                    typeof(AfyaPro_MT.clsTreatmentPoints),
                    Program.gMiddleTier + "clsTreatmentPoints");

                pDtMedicalStaffs.Columns.Add("code", typeof(System.String));
                pDtMedicalStaffs.Columns.Add("description", typeof(System.String));
                pDtMedicalStaffs.Columns.Add("treatmentpointcode", typeof(System.String));
                cboDoctor.Properties.DataSource = pDtMedicalStaffs;
                cboDoctor.Properties.DisplayMember = "description";
                cboDoctor.Properties.ValueMember = "code";

                gDtDiagnoses.Columns.Add("diagnosiscode", typeof(System.String));
                gDtDiagnoses.Columns.Add("diagnosisdescription", typeof(System.String));
                gDtDiagnoses.Columns.Add("diagnosiscategory", typeof(System.String));
                gDtDiagnoses.Columns.Add("isprimary", typeof(System.Int16));
                gDtDiagnoses.Columns.Add("reasonforencounter", typeof(System.String));
                gDtDiagnoses.Columns.Add("treatments", typeof(System.String));
                gDtDiagnoses.Columns.Add("episodecode", typeof(System.String));
                grdDiagnoses.DataSource = gDtDiagnoses;

                pDtEpisodes.Columns.Add("diagnosiscode", typeof(System.String));
                pDtEpisodes.Columns.Add("diagnosisdescription", typeof(System.String));
                pDtEpisodes.Columns.Add("firstencounterdate", typeof(System.DateTime));
                pDtEpisodes.Columns.Add("lastencounterdate", typeof(System.DateTime));
                pDtEpisodes.Columns.Add("episodecode", typeof(System.String));
                grdPatientHistory.DataSource = pDtEpisodes;

                pDtTreatmentPoints.Columns.Add("code", typeof(System.String));
                pDtTreatmentPoints.Columns.Add("description", typeof(System.String));
                cboTreatmentPoint.Properties.DataSource = pDtTreatmentPoints;
                cboTreatmentPoint.Properties.DisplayMember = "description";
                cboTreatmentPoint.Properties.ValueMember = "code";

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.dxtpatientdiagnoses_customizelayout.ToString());

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
            mObjectsList.Add(txbDoctor);
            mObjectsList.Add(cmdSearch);
            mObjectsList.Add(cmdOk);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
            this.Data_Clear();
        }

        #endregion

        #region frmBILPayBillsPatients_Load
        private void frmBILPayBillsPatients_Load(object sender, EventArgs e)
        {
            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            this.Load_Controls();

            txbBirthDateFormat.Text = "(" + Program.gCulture.DateTimeFormat.ShortDatePattern + ")";

            this.pFormWidth = this.Width;
            this.pFormHeight = this.Height;

            Program.Center_Screen(this);

            this.Data_Display(gCurrentPatient);

            if (gIpdAdmission != null)
            {
                txbPrimary.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;
                viewDiagnoses.Columns["diagnosiscategory"].Visible = false;
            }
            else if (gIpdDischarge != null)
            {
                txbPrimary.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }

            this.Append_ShortcutKeys();
        }
        #endregion

        #region frmDXTDiagnoseIPD_Shown
        private void frmDXTDiagnoseIPD_Shown(object sender, EventArgs e)
        {
            //set clinical officer to user
            cboDoctor.ItemIndex = Program.Get_LookupItemIndex(cboDoctor, "code", Program.gCurrentUser.Code);
            txtPatientId.Focus();
        }
        #endregion

        #region frmBILPayBillsPatients_FormClosing
        private void frmBILPayBillsPatients_FormClosing(object sender, FormClosingEventArgs e)
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
            cmdSearch.Text = cmdSearch.Text + " (" + Program.KeyCode_SeachPatient.ToString() + ")";
            cmdOk.Text = cmdOk.Text + " (" + Program.KeyCode_Save.ToString() + ")";
            cmdPatientsQueue.Text = cmdPatientsQueue.Text + " (" + Program.KeyCode_ViewPatientsQueue.ToString() + ")";
            cmdLabTestResults.Text = cmdLabTestResults.Text + " (" + Program.KeyCode_ViewLabTestResults.ToString() + ")";
            cmdSearchDx.Text = cmdSearchDx.Text + " (" + Program.KeyCode_SearchDiagnosis.ToString() + ")";
            cmdAdd.Text = cmdAdd.Text + " (" + Program.KeyCode_ItemAdd.ToString() + ")";
            cmdRemove.Text = cmdRemove.Text + " (" + Program.KeyCode_ItemRemove.ToString() + ")";

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

        #region Data_Clear
        private void Data_Clear()
        {
            txtName.Text = "";
            txtGender.Text = "";
            txtBirthDate.Text = "";
            txtYears.Text = "";
            txtMonths.Text = "";

            txtDxCode.Text = "";
            txtDxDescription.Text = "";
            txtWard.Text = "";
            txtRoom.Text = "";
            txtBedNo.Text = "";
            gDtDiagnoses.Rows.Clear();
            pDtEpisodes.Rows.Clear();
            pDiagnosesSensitive = 0;
            pEpisodeCode = "";
            txtHistory.Text = "";
            txtExamination.Text = "";
            txtInvestigation.Text = "";
            txtTreatments.Text = "";
            gHistory = "";
            gExamination = "";
            gInvestigation = "";
            gTreatments = "";

            this.Clear_Diagnosis();

            pPrevPatientId = "";
            pCurrPatientId = pPrevPatientId;

            txtPatientId.Focus();
        }
        #endregion

        #region Clear_Diagnosis
        private void Clear_Diagnosis()
        {
            txtDxCode.Text = "";
            txtDxDescription.Text = "";
            pEpisodeCode = "";
            pCurrDiagnosis = txtDxCode.Text;
            pPrevDiagnosis = pCurrDiagnosis;

            viewPatientHistory.ClearSelection();
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
                            DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

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

                        AfyaPro_Types.clsAdmission mAdmission = pMdtRegistrations.Get_Admission(gCurrentBooking.Booking, txtPatientId.Text);
                        if (mAdmission != null)
                        {
                            if (mAdmission.IsAdmitted == true)
                            {
                                txtWard.Text = mAdmission.WardDescription;
                                txtRoom.Text = mAdmission.RoomDescription;
                                txtBedNo.Text = mAdmission.BedDescription;
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

                        this.Fill_PatientHistory();

                        this.Clear_Diagnosis();

                        pCurrPatientId = mPatient.code;
                        pPrevPatientId = pCurrPatientId;

                        System.Media.SystemSounds.Beep.Play();

                        cboDoctor.Focus();
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

        #region Fill_PatientHistory
        private void Fill_PatientHistory()
        {
            string mFunctionName = "Fill_PatientHistory";

            try
            {
                pDtEpisodes.Rows.Clear();

                DataTable mDtEpisodes = pMdtPatientDiagnoses.View_Episodes("patientcode='" + txtPatientId.Text.Trim() + "'", "", grdDiagnoses.Name);

                foreach (DataRow mDataRow in mDtEpisodes.Rows)
                {
                    DataRow mNewRow = pDtEpisodes.NewRow();
                    mNewRow["diagnosiscode"] = mDataRow["diagnosiscode"];
                    mNewRow["diagnosisdescription"] = mDataRow["diagnosisdescription"];
                    mNewRow["firstencounterdate"] = mDataRow["firstencounterdate"];
                    mNewRow["lastencounterdate"] = mDataRow["lastencounterdate"];
                    mNewRow["episodecode"] = mDataRow["episodecode"];
                    pDtEpisodes.Rows.Add(mNewRow);
                    pDtEpisodes.AcceptChanges();
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
                gCurrentPatient = pMdtRegistrations.Get_Patient(txtPatientId.Text);
                return gCurrentPatient;
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
                this.gCurrentPatient = this.Search_Patient();
                this.Data_Display(gCurrentPatient);
            }
        }
        #endregion

        #region txtPatientId_Leave
        private void txtPatientId_Leave(object sender, EventArgs e)
        {
            pCurrPatientId = txtPatientId.Text;

            if (pCurrPatientId.Trim().ToLower() != pPrevPatientId.Trim().ToLower())
            {
                this.gCurrentPatient = this.Search_Patient();
                this.Data_Display(gCurrentPatient);
            }
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Save
        private void Save()
        {
            AfyaPro_Types.clsBill mBill = new AfyaPro_Types.clsBill();
            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();

            string mFunctionName = "Save";

            #region validation

            if (Program.IsDate(Program.gMdiForm.txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
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

            if (gDtDiagnoses.Rows.Count == 0)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DxIsMissing.ToString());
                grdDiagnoses.Focus();
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
                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

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

            #endregion

            try
            {
                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                gBillingOk = true;

                gDoctorCode = cboDoctor.GetColumnValue("code").ToString().Trim();
                gDoctorDescription = cboDoctor.GetColumnValue("description").ToString().Trim();
                gHistory = txtHistory.Text.Trim();
                gExamination = txtExamination.Text.Trim();
                gInvestigation = txtInvestigation.Text.Trim();
                gTreatments = txtTreatments.Text.Trim();

                if (gCharge == true)
                {
                    gBillingOk = false;

                    #region billing + admission + diagnoses

                    frmBILBillPosting mBILBillPosting = new frmBILBillPosting();
                    mBILBillPosting.gIpdPatientDiagnoses = this;
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
                    mBILBillPosting.gCurrentBooking.BookDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);
                    mBILBillPosting.gCurrentPatient = gCurrentPatient;

                    mBILBillPosting.gCurrentBooking.PriceName = Program.gCurrentUser.DefaultPriceCategoryCode;

                    mBILBillPosting.ShowDialog();

                    #endregion

                    if (gBillingOk == false)
                    {
                        return;
                    }

                    txtPatientId.Text = "";
                    this.Data_Clear();
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
                    mResult = pMdtPatientDiagnoses.Diagnose(mTransDate, txtPatientId.Text, gDoctorCode, gDoctorDescription,
                        txtHistory.Text, txtExamination.Text, txtInvestigation.Text,
                        gDtDiagnoses, txtTreatments.Text, mDtAdmissionDetails, mDtDischargeDetails, Program.gCurrentUser.Code);

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
                        this.Close();
                    }
                    else if (gIpdDischarge != null)
                    {
                        frmIPDDischarges mIPDDischarges = (frmIPDDischarges)gIpdDischarge;
                        mIPDDischarges.gDiagnosisOk = true;
                        this.Close();
                    }
                    else
                    {
                        txtPatientId.Text = "";
                        this.Data_Clear();
                        txtPatientId.Focus();
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

        #region cmdSave_Click
        private void cmdSave_Click(object sender, EventArgs e)
        {
            this.Save();
        }
        #endregion

        #region cmdSearch_Click
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            pSearchingPatient = true;

            frmSearchPatient mSearchPatient = new frmSearchPatient(txtPatientId);
            mSearchPatient.ShowDialog();
            if (mSearchPatient.SearchingDone == true)
            {
                cboDoctor.Focus();
            }

            pSearchingPatient = false;
        }
        #endregion

        #region txtPatientId_EditValueChanged
        private void txtPatientId_EditValueChanged(object sender, EventArgs e)
        {
            if (pSearchingPatient == true)
            {
                this.gCurrentPatient = this.Search_Patient();
                this.Data_Display(gCurrentPatient);
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

                    if (gIpdAdmission != null)
                    {

                    }
                    else
                    {
                        cmdAdd.Focus();
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
                if (mSearchDiagnosis.SearchingDone == true)
                {
                    cmdAdd.Focus();
                }

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

        #region Add_Diagnosis
        private void Add_Diagnosis()
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

            //restrict to one entry in admission
            if (gIpdAdmission != null)
            {
                if (gDtDiagnoses.Rows.Count > 0)
                {
                    Program.Display_Error("Only one admission diagnosis is allowed");
                    return;
                }
            }

            string mCategory = "Primary";
            Int16 mPrimary = 1;
            if (txbPrimary.Visible == true)
            {
                if (radPrimary.SelectedIndex == 1)
                {
                    mPrimary = 0;
                    mCategory = "Secondary";
                }
            }

            //restrict to only one primary diagnosis
            int mPrimaryCount = 0;
            foreach (DataRow mDataRow in gDtDiagnoses.Rows)
            {
                if (Convert.ToInt16(mDataRow["isprimary"]) == 1)
                {
                    mPrimaryCount++;
                }
            }

            if (mPrimaryCount > 0 && mPrimary == 1)
            {
                Program.Display_Error("Only one primary diagnosis is allowed");
                return;
            }

            DataRow mNewRow = gDtDiagnoses.NewRow();
            mNewRow["diagnosiscode"] = txtDxCode.Text.Trim();
            mNewRow["diagnosisdescription"] = txtDxDescription.Text.Trim();
            mNewRow["diagnosiscategory"] = mCategory;
            mNewRow["isprimary"] = mPrimary;
            mNewRow["reasonforencounter"] = "";
            mNewRow["treatments"] = "";
            mNewRow["episodecode"] = pEpisodeCode;
            gDtDiagnoses.Rows.Add(mNewRow);
            gDtDiagnoses.AcceptChanges();

            txtDxCode.Text = "";
            txtDxDescription.Text = "";
            pEpisodeCode = "";
            pCurrDiagnosis = txtDxCode.Text;
            pPrevDiagnosis = pCurrDiagnosis;

            viewPatientHistory.ClearSelection();
        }
        #endregion

        #region Remove_Diagnosis
        private void Remove_Diagnosis()
        {
            DevExpress.XtraGrid.Views.Grid.GridView viewPatientHistory =
            (DevExpress.XtraGrid.Views.Grid.GridView)grdDiagnoses.MainView;

            if (viewPatientHistory.FocusedRowHandle < 0)
            {
                return;
            }

            viewPatientHistory.DeleteSelectedRows();
            gDtDiagnoses.AcceptChanges();
            txtDxCode.Focus();
        }
        #endregion

        #region cmdAdd_Click
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            this.Add_Diagnosis();
        }
        #endregion

        #region cmdRemove_Click
        private void cmdRemove_Click(object sender, EventArgs e)
        {
            this.Remove_Diagnosis();
        }
        #endregion

        #region Display_RowDetails
        private void Display_RowDetails(DataRow mSelectedRow)
        {
            txtDxCode.Text = mSelectedRow["diagnosiscode"].ToString().Trim();
            txtDxDescription.Text = mSelectedRow["diagnosisdescription"].ToString().Trim();
            pEpisodeCode = mSelectedRow["episodecode"].ToString().Trim();
        }
        #endregion

        #region viewPatientHistory_FocusedRowChanged
        void viewPatientHistory_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle <= -1)
            {
                return;
            }

            this.Display_RowDetails(viewPatientHistory.GetDataRow(e.FocusedRowHandle));
        }
        #endregion

        #region viewPatientHistory_RowClick
        private void viewPatientHistory_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle <= -1)
            {
                return;
            }

            this.Display_RowDetails(viewPatientHistory.GetDataRow(e.RowHandle));
        }
        #endregion

        #region viewPatientHistory_GotFocus
        private void viewPatientHistory_GotFocus(object sender, EventArgs e)
        {
            if (viewPatientHistory.FocusedRowHandle <= -1)
            {
                return;
            }

            this.Display_RowDetails(viewPatientHistory.GetDataRow(viewPatientHistory.FocusedRowHandle));
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

        #region frmDXTDiagnoseIPD_KeyDown
        void frmDXTDiagnoseIPD_KeyDown(object sender, KeyEventArgs e)
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
                            cboDoctor.Focus();
                        }

                        pSearchingPatient = false;
                    }
                    break;
                case Program.KeyCode_ViewLabTestResults:
                    {
                        this.View_LabTestResults();
                    }
                    break;
                case Program.KeyCode_SearchDiagnosis:
                    {
                        this.Search_Diagnoses();
                    }
                    break;
                case Program.KeyCode_ItemAdd:
                    {
                        this.Add_Diagnosis();
                    }
                    break;
                case Program.KeyCode_ItemRemove:
                    {
                        this.Remove_Diagnosis();
                    }
                    break;
                case Program.KeyCode_ViewPatientsQueue:
                    {
                        this.cmdPatientsQueue_Click(cmdPatientsQueue, e);
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
            if (cboTreatmentPoint.ItemIndex == -1)
            {
                return;
            }

            frmQueueTreatmentPoint mQueueTreatmentPoint = new frmQueueTreatmentPoint();
            mQueueTreatmentPoint.TreatmentPointCode = cboTreatmentPoint.GetColumnValue("code").ToString().Trim();
            mQueueTreatmentPoint.TransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);
            mQueueTreatmentPoint.QueueType = (int)AfyaPro_Types.clsEnums.PatientsQueueTypes.Consultation;
            mQueueTreatmentPoint.ShowDialog();

            if (mQueueTreatmentPoint.PatientSelected == true)
            {
                txtPatientId.Text = mQueueTreatmentPoint.PatientCode;
                this.gCurrentPatient = this.Search_Patient();
                this.Data_Display(gCurrentPatient);
            }
        }
        #endregion

        #region cboDoctor_EditValueChanged
        private void cboDoctor_EditValueChanged(object sender, EventArgs e)
        {
            if (cboDoctor.ItemIndex == -1)
            {
                return;
            }

            string mTreatmentPointCode = cboDoctor.GetColumnValue("treatmentpointcode").ToString();
            if (mTreatmentPointCode.Trim() != "")
            {
                cboTreatmentPoint.ItemIndex = Program.Get_LookupItemIndex(cboTreatmentPoint, "code", mTreatmentPointCode);
            }
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
            if (cboTreatmentPoint.ItemIndex == -1)
            {
                return;
            }

            AfyaPro_Types.clsPatient mPatient = pMdtRegistrations.Get_Patient(txtPatientId.Text);
            if (mPatient.Exist == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoDoesNotExist.ToString());
                txtPatientId.Focus();
                return;
            }

            int mYears = Program.IsNumeric(txtYears.Text) == false ? 0 : Convert.ToInt32(txtYears.Text);
            int mMonths = Program.IsNumeric(txtMonths.Text) == false ? 0 : Convert.ToInt32(txtMonths.Text);

            frmDXTPrescribeLabTests mDXTPrescribeLabTests = new frmDXTPrescribeLabTests(mPatient);
            mDXTPrescribeLabTests.TreatmentPointCode = cboTreatmentPoint.GetColumnValue("code").ToString().Trim();
            mDXTPrescribeLabTests.ShowDialog();
        }
        #endregion

        #region cmdViewPatientHistory_Click
        private void cmdViewPatientHistory_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdViewPatientHistory_Click";

            this.Cursor = Cursors.WaitCursor;

            try
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

                frmRPDReportingForm1 mReportingForm = new frmRPDReportingForm1("rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.DXTPatientHistory, false);
                TextEdit txtTargetPatientId = (TextEdit)mReportingForm.layoutControl1.GetControlByName("txtPatientId");
                txtTargetPatientId.Text = txtPatientId.Text.Trim();
                mReportingForm.cmdView_Click(mReportingForm.cmdView, e);
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion
    }
}