using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;

namespace AfyaPro_NextGen
{
    public partial class frmCTCPMTCTPostnatalVisitDetails : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_MT.clsCTCClients pMdtCTCClients;
        private AfyaPro_MT.clsReporter pMdtReporter;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private AfyaPro_Types.ctcBooking pBooking = new AfyaPro_Types.ctcBooking();

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private AfyaPro_Types.clsCtcClient pSelectedPatient = null;
        internal AfyaPro_Types.clsCtcClient SelectedPatient
        {
            set { pSelectedPatient = value; }
            get { return pSelectedPatient; }
        }

        private DataRow pCurrentPregRow = null;
        internal DataRow CurrentPregRow
        {
            set { pCurrentPregRow = value; }
            get { return pCurrentPregRow; }
        }

        private bool pRecordSaved = false;
        internal bool RecordSaved
        {
            set { pRecordSaved = value; }
            get { return pRecordSaved; }
        }

        private DataRow pSelectedDataRow = null;

        #endregion

        #region frmCTCPMTCTPostnatalVisitDetails
        public frmCTCPMTCTPostnatalVisitDetails(DataRow mSelectedDataRow = null)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCPMTCTPostnatalVisitDetails";
            this.KeyDown += new KeyEventHandler(frmCTCPMTCTPostnatalVisitDetails_KeyDown);

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                this.pSelectedDataRow = mSelectedDataRow;

                this.CancelButton = cmdClose;

                pMdtAutoCodes = (AfyaPro_MT.clsAutoCodes)Activator.GetObject(
                    typeof(AfyaPro_MT.clsAutoCodes),
                    Program.gMiddleTier + "clsAutoCodes");

                pMdtCTCClients = (AfyaPro_MT.clsCTCClients)Activator.GetObject(
                    typeof(AfyaPro_MT.clsCTCClients),
                    Program.gMiddleTier + "clsCTCClients");

                pMdtReporter = (AfyaPro_MT.clsReporter)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReporter),
                    Program.gMiddleTier + "clsReporter");

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcartvisits_customizelayout.ToString());

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
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(txbPatientId);
            mObjectsList.Add(cmdOk);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region frmCTCPMTCTPostnatalVisitDetails_Load
        private void frmCTCPMTCTPostnatalVisitDetails_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCPMTCTPostnatalVisitDetails_Load";

            try
            {
                Program.Restore_FormLayout(layoutControl1, this.Name);
                Program.Restore_FormSize(this);

                this.pFormWidth = this.Width;
                this.pFormHeight = this.Height;

                Program.Center_Screen(this);

                this.Load_Controls();

                this.Append_ShortcutKeys();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmCTCPMTCTPostnatalVisitDetails_Shown
        private void frmCTCPMTCTPostnatalVisitDetails_Shown(object sender, EventArgs e)
        {
            txtDate.EditValue = DateTime.Now.Date;

            if (pSelectedPatient != null)
            {
                this.Display_Patient(pSelectedPatient);
            }
        }
        #endregion

        #region frmCTCPMTCTPostnatalVisitDetails_FormClosing
        private void frmCTCPMTCTPostnatalVisitDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            //layout
            if (layoutControl1.IsModified == true)
            {
                Program.Save_FormLayout(this, layoutControl1, this.Name);
            }
        }
        #endregion

        #region Append_ShortcutKeys
        private void Append_ShortcutKeys()
        {
            //cmdOk.Text = cmdOk.Text + " (" + Program.KeyCode_Ok.ToString() + ")";
        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region pmtct

                DataTable mDtPMTCT = pMdtReporter.View_LookupData("ctc_pmtctcomb", "code,description", "", "", Program.gLanguageName, "grdCTCPMTCTComb");

                cboMotherLabourPMTCT.Properties.DataSource = mDtPMTCT;
                cboMotherLabourPMTCT.Properties.DisplayMember = "description";
                cboMotherLabourPMTCT.Properties.ValueMember = "code";
                cboMotherLabourPMTCT.Properties.BestFit();

                cboMotherPostPMTCT.Properties.DataSource = mDtPMTCT;
                cboMotherPostPMTCT.Properties.DisplayMember = "description";
                cboMotherPostPMTCT.Properties.ValueMember = "code";
                cboMotherPostPMTCT.Properties.BestFit();

                cboBabyPMTCT.Properties.DataSource = mDtPMTCT;
                cboBabyPMTCT.Properties.DisplayMember = "description";
                cboBabyPMTCT.Properties.ValueMember = "code";
                cboBabyPMTCT.Properties.BestFit();

                #endregion
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Display_Patient
        private void Display_Patient(AfyaPro_Types.clsCtcClient mPatient)
        {
            string mFunctionName = "Display_Patient";

            try
            {
                if (mPatient.Exist == false)
                {
                    return;
                }

                #region patient info

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

                txtYears.Text = "";
                txtMonths.Text = "";
                if (Program.IsNullDate(mPatient.birthdate) == false)
                {
                    int mDays = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue).Subtract(mPatient.birthdate).Days;
                    int mYears = (int)mDays / 365;
                    int mMonths = (int)(mDays % 365) / 30;

                    txtYears.Text = mYears.ToString();
                    txtMonths.Text = mMonths.ToString();
                }

                #endregion

                #region ctc_clients

                //pBooking = pMdtCTCClients.Get_Booking(mPatient.code);

                DataTable mDtClients = pMdtCTCClients.View_CTCClients("patientcode='" + mPatient.code + "'", "");

                if (mDtClients.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtClients.Rows[0];

                    txtHIVNo.Text = mDataRow["hivno"].ToString();
                    txtCTCNo.Text = mDataRow["ctcno"].ToString();

                    cmdOk.Enabled = true;
                }
                else
                {
                    cmdOk.Enabled = false;
                }

                #endregion

                this.Display_Visit();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Display_Visit
        private void Display_Visit()
        {
            if (pSelectedDataRow != null)
            {
                txtDate.EditValue = Convert.ToDateTime(pSelectedDataRow["transdate"]);
                cboClinicalStage.Text = pSelectedDataRow["clinicalstage"] == DBNull.Value ? "" : pSelectedDataRow["clinicalstage"].ToString();
                cboMotherLabourPMTCT.ItemIndex = Program.Get_LookupItemIndex(cboMotherLabourPMTCT, "code", pSelectedDataRow["motherlabourcombcode"].ToString());
                cboMotherPostPMTCT.ItemIndex = Program.Get_LookupItemIndex(cboMotherPostPMTCT, "code", pSelectedDataRow["motherpostcombcode"].ToString());
                cboBabyPMTCT.ItemIndex = Program.Get_LookupItemIndex(cboBabyPMTCT, "code", pSelectedDataRow["babycombcode"].ToString());
                txtReferedTo.Text = pSelectedDataRow["referedto"] == DBNull.Value ? "" : pSelectedDataRow["referedto"].ToString();
                chkCameinCouple.Checked = Convert.ToBoolean(pSelectedDataRow["cameincouple"]);
                cboPartnerHIVResult.SelectedIndex = pSelectedDataRow["partnerresult"] == DBNull.Value ? 0 : Convert.ToInt32(pSelectedDataRow["partnerresult"]);
                chkBactrim.Checked = Convert.ToBoolean(pSelectedDataRow["babybactrim"]);
                cboFamilyPlanMethod.SelectedIndex = pSelectedDataRow["familyplanmethod"] == DBNull.Value ? -1 : Convert.ToInt32(pSelectedDataRow["familyplanmethod"]);
                cboInfantFeedingOption.SelectedIndex = pSelectedDataRow["infantfeedingoption"] == DBNull.Value ? -1 : Convert.ToInt32(pSelectedDataRow["infantfeedingoption"]);
                txtNextVisitDate.EditValue = null;
                if (pSelectedDataRow["nextvisitdate"] != DBNull.Value)
                {
                    txtNextVisitDate.EditValue = Convert.ToDateTime(pSelectedDataRow["nextvisitdate"]);
                }
            }
        }
        #endregion

        #region Data_New
        private void Data_New()
        {
            string mFunctionName = "Data_New";

            if (Program.IsDate(txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                txtDate.Focus();
                return;
            }

            try
            {
                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

                int mClinicalStage = 0;
                string mMotherLabourCombCode = "";
                string mMotherPostCombCode = "";
                string mBabyCombCode = "";
                string mReferredTo = txtReferedTo.Text.Trim();
                DateTime mNextVisitDate = new DateTime();

                if (Program.IsNumeric(cboClinicalStage.Text) == true)
                {
                    mClinicalStage = Convert.ToInt32(cboClinicalStage.Text);
                }

                if (Program.IsDate(txtNextVisitDate.EditValue) == true)
                {
                    mNextVisitDate = Convert.ToDateTime(txtNextVisitDate.EditValue);
                }

                if (cboMotherLabourPMTCT.ItemIndex >= 0)
                {
                    mMotherLabourCombCode = cboMotherLabourPMTCT.GetColumnValue("code").ToString();
                }

                if (cboMotherPostPMTCT.ItemIndex >= 0)
                {
                    mMotherPostCombCode = cboMotherPostPMTCT.GetColumnValue("code").ToString();
                }

                if (cboBabyPMTCT.ItemIndex >= 0)
                {
                    mBabyCombCode = cboBabyPMTCT.GetColumnValue("code").ToString();
                }

                AfyaPro_Types.clsResult mCtcClient = pMdtCTCClients.Add_PMTCTPostnatalVisit(
                    mTransDate,
                    txtPatientId.Text,
                    pCurrentPregRow["booking"].ToString(),
                    pCurrentPregRow["incidencecode"].ToString(),
                    mClinicalStage,
                    Convert.ToInt32(chkBactrim.Checked),
                    mMotherLabourCombCode,
                    mMotherPostCombCode,
                    mBabyCombCode,
                    Convert.ToInt32(chkCameinCouple.Checked),
                    cboPartnerHIVResult.SelectedIndex,
                    cboFamilyPlanMethod.SelectedIndex,
                    cboInfantFeedingOption.SelectedIndex,
                    mReferredTo,
                    mNextVisitDate,
                    Program.gMachineName,
                    Program.gMachineUser,
                    Program.gCurrentUser.Code);

                if (mCtcClient.Exe_Result == 0)
                {
                    Program.Display_Error(mCtcClient.Exe_Message);
                    return;
                }
                if (mCtcClient.Exe_Result == -1)
                {
                    Program.Display_Server_Error(mCtcClient.Exe_Message);
                    return;
                }

                Program.Display_Info("Record saved successfully");

                this.pRecordSaved = true;
                this.Close();
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
            string mFunctionName = "Data_Edit";

            if (Program.IsDate(Program.gMdiForm.txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                return;
            }

            try
            {
                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                int mClinicalStage = 0;
                string mMotherLabourCombCode = "";
                string mMotherPostCombCode = "";
                string mBabyCombCode = "";
                string mReferredTo = txtReferedTo.Text.Trim();
                DateTime mNextVisitDate = new DateTime();

                if (Program.IsNumeric(cboClinicalStage.Text) == true)
                {
                    mClinicalStage = Convert.ToInt32(cboClinicalStage.Text);
                }

                if (Program.IsDate(txtNextVisitDate.EditValue) == true)
                {
                    mNextVisitDate = Convert.ToDateTime(txtNextVisitDate.EditValue);
                }

                if (cboMotherLabourPMTCT.ItemIndex >= 0)
                {
                    mMotherLabourCombCode = cboMotherLabourPMTCT.GetColumnValue("code").ToString();
                }

                if (cboMotherPostPMTCT.ItemIndex >= 0)
                {
                    mMotherPostCombCode = cboMotherPostPMTCT.GetColumnValue("code").ToString();
                }

                if (cboBabyPMTCT.ItemIndex >= 0)
                {
                    mBabyCombCode = cboBabyPMTCT.GetColumnValue("code").ToString();
                }

                AfyaPro_Types.clsResult mCtcClient = pMdtCTCClients.Edit_PMTCTPostnatalVisit(
                    Convert.ToInt32(pSelectedDataRow["autocode"]),
                    mTransDate,
                    txtPatientId.Text,
                    mClinicalStage,
                    Convert.ToInt32(chkBactrim.Checked),
                    mMotherLabourCombCode,
                    mMotherPostCombCode,
                    mBabyCombCode,
                    Convert.ToInt32(chkCameinCouple.Checked),
                    cboPartnerHIVResult.SelectedIndex,
                    cboFamilyPlanMethod.SelectedIndex,
                    cboInfantFeedingOption.SelectedIndex,
                    mReferredTo,
                    mNextVisitDate,
                    Program.gMachineName,
                    Program.gMachineUser,
                    Program.gCurrentUser.Code);

                if (mCtcClient.Exe_Result == 0)
                {
                    Program.Display_Error(mCtcClient.Exe_Message);
                    return;
                }
                if (mCtcClient.Exe_Result == -1)
                {
                    Program.Display_Server_Error(mCtcClient.Exe_Message);
                    return;
                }

                Program.Display_Info("Record saved successfully");

                this.pRecordSaved = true;
                this.Close();
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
            if (pSelectedDataRow == null)
            {
                this.Data_New();
            }
            else
            {
                this.Data_Edit();
            }
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region frmCTCPMTCTPostnatalVisitDetails_KeyDown
        void frmCTCPMTCTPostnatalVisitDetails_KeyDown(object sender, KeyEventArgs e)
        {
            //switch (e.KeyCode)
            //{
            //    case Program.KeyCode_Ok:
            //        {
            //            this.Ok();
            //        }
            //        break;
            //}
        }
        #endregion

        #region chkCameinCouple_CheckedChanged
        private void chkCameinCouple_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCameinCouple.Checked == true)
            {
                cboPartnerHIVResult.Properties.ReadOnly = false;
            }
            else
            {
                cboPartnerHIVResult.Properties.ReadOnly = true;
            }
        }
        #endregion
    }
}