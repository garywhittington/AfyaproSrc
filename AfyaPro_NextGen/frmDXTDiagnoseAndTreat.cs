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
    public partial class frmDXTDiagnoseAndTreat : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsMedicalStaffs pMdtMedicalStaffs;
        private AfyaPro_MT.clsDiagnoses pMdtDiagnoses;
        private AfyaPro_MT.clsRegistrations pMdtRegistrations;
        private AfyaPro_MT.clsPatientDiagnoses pMdtPatientDiagnoses;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private AfyaPro_Types.clsPatient pCurrentPatient;
        internal AfyaPro_Types.clsBooking gCurrentBooking = new AfyaPro_Types.clsBooking();
        internal bool gBillingOk = false;
        internal string gDoctorCode = "";
        internal string gDoctorDescription = "";
        internal DataTable gDtDiagnoses = new DataTable("patientdiagnoses");
        internal Int16 gDeathStatus = 0;
        internal string gHistory = "";
        internal string gExamination = "";
        internal string gInvestigation = "";
        internal string gTreatments = "";
        internal DataTable gDtDiagnosesReferalInfo = new DataTable("diagnosesreferalinfo");
        internal DataTable gDtDiagnosesPrescriptions = new DataTable("diagnosesprescriptions");

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private string pCurrPatientId = "";
        private string pPrevPatientId = "";
        private string pCurrDiagnosis = "";
        private string pPrevDiagnosis = "";
        private bool pSearchingPatient = false;
        private bool pSearchingDiagnosis = false;

        private DataTable pDtMedicalStaffs = new DataTable("medicalstaffs");

        #endregion

        #region frmDXTDiagnoseAndTreat
        public frmDXTDiagnoseAndTreat()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmDXTDiagnoseAndTreat";

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

                pDtMedicalStaffs.Columns.Add("code", typeof(System.String));
                pDtMedicalStaffs.Columns.Add("description", typeof(System.String));
                cboDoctor.Properties.DataSource = pDtMedicalStaffs;
                cboDoctor.Properties.DisplayMember = "description";
                cboDoctor.Properties.ValueMember = "code";

                gDtDiagnoses.Columns.Add("diagnosiscode", typeof(System.String));
                gDtDiagnoses.Columns.Add("diagnosisdescription", typeof(System.String));
                gDtDiagnoses.Columns.Add("followup", typeof(System.Boolean));
                grdDiagnoses.DataSource = gDtDiagnoses;

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
            mObjectsList.Add(txbStreet);
            mObjectsList.Add(txbBirthDate);
            mObjectsList.Add(txbYears);
            mObjectsList.Add(txbMonths);
            mObjectsList.Add(txbGender);
            mObjectsList.Add(radGender.Properties.Items[0]);
            mObjectsList.Add(radGender.Properties.Items[1]);
            mObjectsList.Add(txbDoctor);
            mObjectsList.Add(cmdSearch);
            mObjectsList.Add(cmdNew);
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
                    "category=" + Convert.ToInt16(AfyaPro_Types.clsEnums.StaffCategories.MedicalDoctors),
                    "code", Program.gLanguageName, "grdGENMedicalStaffs");
                foreach (DataRow mDataRow in mDtMedicalStaffs.Rows)
                {
                    mNewRow = pDtMedicalStaffs.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
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

        #region Data_Clear
        private void Data_Clear()
        {
            txtName.Text = "";
            txtStreet.Text = "";
            radGender.SelectedIndex = -1;
            txtBirthDate.Text = "";
            txtYears.Text = "";
            txtMonths.Text = "";

            radDxStatus.SelectedIndex = -1;
            txtDxCode.Text = "";
            txtDxDescription.Text = "";
            chkDeathStatus.Checked = false;
            radDxStatus.Enabled = true;
            txtHistory.Text = "";
            txtExamination.Text = "";
            txtInvestigation.Text = "";
            txtTreatments.Text = "";
            gDtDiagnoses.Rows.Clear();

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
                            radGender.SelectedIndex = 0;
                        }
                        else
                        {
                            radGender.SelectedIndex = 1;
                        }
                        txtBirthDate.Text = mPatient.birthdate.ToString("d");
                        int mDays = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue).Subtract(mPatient.birthdate).Days;
                        int mYears = (int)mDays / 365;
                        int mMonths = (int)(mDays % 365) / 30;

                        txtYears.Text = mYears.ToString();
                        txtMonths.Text = mMonths.ToString();

                        #region get current booking

                        gCurrentBooking = pMdtRegistrations.Get_Booking(txtPatientId.Text);

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

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region cmdSave_Click
        private void cmdSave_Click(object sender, EventArgs e)
        {
            AfyaPro_Types.clsBill mBill = new AfyaPro_Types.clsBill();
            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();

            string mFunctionName = "cmdSave_Click";

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
                gDeathStatus = Convert.ToInt16(chkDeathStatus.Checked);

                DataTable mDtAdmissionDetails = new DataTable("admissiondetails");
                DataTable mDtDischargeDetails = new DataTable("dischargedetails");

                //diagnose & treat 
                mResult = pMdtPatientDiagnoses.Diagnose(mTransDate, txtPatientId.Text,
                gDoctorCode, gDoctorDescription, gHistory, gExamination, gInvestigation,
                gTreatments, gDtDiagnoses, mDtAdmissionDetails, mDtDischargeDetails, Program.gCurrentUser.Code);

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

                //refresh
                txtPatientId.Text = "";
                this.Data_Clear();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
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
                this.pCurrentPatient = this.Search_Patient();
                this.Data_Display(pCurrentPatient);
            }
        }
        #endregion

        #region Display_Diagnosis
        private void Display_Diagnosis()
        {
            string mFunctionName = "Display_Diagnosis";

            try
            {
                DataTable mDtDiagnoses = pMdtDiagnoses.View(
                    "code='" + txtDxCode.Text.Trim() + "'", "", "", "");

                if (mDtDiagnoses.Rows.Count > 0)
                {
                    txtDxCode.Text = mDtDiagnoses.Rows[0]["code"].ToString().Trim();
                    txtDxDescription.Text = mDtDiagnoses.Rows[0]["description"].ToString().Trim();

                    pCurrDiagnosis = txtDxCode.Text;
                    pPrevDiagnosis = pCurrDiagnosis;

                    cmdAdd.Focus();
                }
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
            string mFunctionName = "cmdSearchItem_Click";

            try
            {
                pSearchingDiagnosis = true;

                frmSearchDiagnosis mSearchDiagnosis = new frmSearchDiagnosis(txtDxCode);
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

        #region cmdAdd_Click
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (radDxStatus.SelectedIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DiagnosisStatusIsInvalid.ToString());
                cboDoctor.Focus();
                return;
            }

            DataTable mDtDiagnoses = pMdtDiagnoses.View("code='" + txtDxCode.Text.Trim() + "'", "", "", "");
            if (mDtDiagnoses.Rows.Count == 0)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DiagnosisCodeIsInvalid.ToString());
                txtDxCode.Focus();
                return;
            }

            Int16 mFollowUp = 0;
            if (radDxStatus.SelectedIndex == 1)
            {
                mFollowUp = 1;
            }

            DataRow mNewRow = gDtDiagnoses.NewRow();
            mNewRow["diagnosiscode"] = txtDxCode.Text.Trim();
            mNewRow["diagnosisdescription"] = txtDxDescription.Text.Trim();
            mNewRow["followup"] = mFollowUp;
            gDtDiagnoses.Rows.Add(mNewRow);
            gDtDiagnoses.AcceptChanges();

            radDxStatus.SelectedIndex = -1;
            txtDxCode.Text = "";
            txtDxDescription.Text = "";
            pCurrDiagnosis = txtDxCode.Text;
            pPrevDiagnosis = pCurrDiagnosis;
            txtTreatments.Focus();
        }
        #endregion

        #region cmdRemove_Click
        private void cmdRemove_Click(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView mGridView =
            (DevExpress.XtraGrid.Views.Grid.GridView)grdDiagnoses.MainView;

            if (mGridView.FocusedRowHandle < 0)
            {
                return;
            }

            mGridView.DeleteSelectedRows();
            gDtDiagnoses.AcceptChanges();
            txtDxCode.Focus();
        }
        #endregion
    }
}