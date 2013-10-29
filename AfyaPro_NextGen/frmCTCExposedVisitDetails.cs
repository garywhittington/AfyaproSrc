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
using DevExpress.XtraEditors.Controls;

namespace AfyaPro_NextGen
{
    public partial class frmCTCExposedVisitDetails : DevExpress.XtraEditors.XtraForm
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

        private DataRow pSelectedDataRow = null;
        private DataTable pDtSideEffects = new DataTable("sideeffects");

        private bool pRecordSaved = false;
        internal bool RecordSaved
        {
            set { pRecordSaved = value; }
            get { return pRecordSaved; }
        }

        #endregion

        #region frmCTCExposedVisitDetails
        public frmCTCExposedVisitDetails(DataRow mSelectedDataRow = null)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCExposedVisitDetails";
            this.KeyDown += new KeyEventHandler(frmCTCExposedVisitDetails_KeyDown);

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
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcart_customizelayout.ToString());

                pDtSideEffects.Columns.Add("code", typeof(System.String));

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

        #region frmCTCExposedVisitDetails_Load
        private void frmCTCExposedVisitDetails_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCExposedVisitDetails_Load";

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

        #region frmCTCExposedVisitDetails_Shown
        private void frmCTCExposedVisitDetails_Shown(object sender, EventArgs e)
        {
            txtDate.EditValue = DateTime.Now.Date;

            if (pSelectedPatient != null)
            {
                this.Display_Patient(pSelectedPatient);
            }
        }
        #endregion

        #region frmCTCExposedVisitDetails_FormClosing
        private void frmCTCExposedVisitDetails_FormClosing(object sender, FormClosingEventArgs e)
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
                #region wasting

                DataTable mDtWasting = pMdtReporter.View_LookupData("ctc_wastingcodes", "code,description", "active=1", "autocode", Program.gLanguageName, "grdCTCARTOutcomes");
                cboWasting.Properties.DataSource = mDtWasting;
                cboWasting.Properties.DisplayMember = "description";
                cboWasting.Properties.ValueMember = "code";

                cboWasting.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboWasting.Properties.BestFit();

                #endregion

                #region BreastFeeding

                DataTable mDtBreastFeeding = pMdtReporter.View_LookupData("ctc_breastfeedings", "code,description", "active=1", "autocode", Program.gLanguageName, "grdCTCARVCombRegimens");
                cboBreastFeeding.Properties.DataSource = mDtBreastFeeding;
                cboBreastFeeding.Properties.DisplayMember = "description";
                cboBreastFeeding.Properties.ValueMember = "code";

                cboBreastFeeding.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboBreastFeeding.Properties.BestFit();

                #endregion

                #region MotherStatus

                DataTable mDtMotherStatus = pMdtReporter.View_LookupData("ctc_motherstatus", "code,description", "active=1", "autocode", Program.gLanguageName, "grdCTCARVCombRegimens");
                cboMotherStatus.Properties.DataSource = mDtMotherStatus;
                cboMotherStatus.Properties.DisplayMember = "description";
                cboMotherStatus.Properties.ValueMember = "code";

                cboMotherStatus.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboMotherStatus.Properties.BestFit();

                #endregion

                #region TB Status

                DataTable mDtTBStatus = pMdtReporter.View_LookupData("ctc_tbstatus", "code,description", "active=1", "autocode", Program.gLanguageName, "grdCTCTBStatus");
                cboTBStatus.Properties.DataSource = mDtTBStatus;
                cboTBStatus.Properties.DisplayMember = "description";
                cboTBStatus.Properties.ValueMember = "code";

                cboTBStatus.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboTBStatus.Properties.BestFit();

                #endregion

                #region ClinMonit

                DataTable mDTClinMonit = pMdtReporter.View_LookupData("ctc_clinmonit", "code,description", "active=1", "autocode", Program.gLanguageName, "grdCTCTBStatus");
                cboClinMonit.Properties.DataSource = mDTClinMonit;
                cboClinMonit.Properties.DisplayMember = "description";
                cboClinMonit.Properties.ValueMember = "code";

                cboClinMonit.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboClinMonit.Properties.BestFit();

                #endregion

                #region follow-up

                DataTable mDtARTOutcomes = pMdtReporter.View_LookupData("ctc_followupstatus", "code,description", "active=1", "autocode", Program.gLanguageName, "grdCTCARTOutcomes");
                cboOutcome.Properties.DataSource = mDtARTOutcomes;
                cboOutcome.Properties.DisplayMember = "description";
                cboOutcome.Properties.ValueMember = "code";

                cboOutcome.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboOutcome.Properties.BestFit();

                #endregion

                #region HIVInfection

                DataTable mDtHIVInfections = pMdtReporter.View_LookupData("ctc_hivinfections", "code,description", "active=1", "autocode", Program.gLanguageName, "grdCTCARVCombRegimens");
                cboHIVInfection.Properties.DataSource = mDtHIVInfections;
                cboHIVInfection.Properties.DisplayMember = "description";
                cboHIVInfection.Properties.ValueMember = "code";

                cboHIVInfection.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboHIVInfection.Properties.BestFit();

                #endregion

                #region clinicians

                DataTable mDtClinicians = pMdtReporter.View_LookupData("facilitystaffs", "code,description,treatmentpointcode",
                    "category=" + Convert.ToInt16(AfyaPro_Types.clsEnums.StaffCategories.MedicalDoctors),
                    "description", Program.gLanguageName, "grdGENMedicalStaffs");

                cboClinician.Properties.DataSource = mDtClinicians;
                cboClinician.Properties.DisplayMember = "description";
                cboClinician.Properties.ValueMember = "code";

                cboClinician.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboClinician.Properties.BestFit();

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
                    txtHTCNo.Text = mDataRow["htcno"].ToString();
                    txtCTCNo.Text = mDataRow["ctcno"].ToString();

                    cmdOk.Enabled = true;
                }
                else
                {
                    cmdOk.Enabled = false;
                }

                #endregion

                pDtSideEffects.Rows.Clear();

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
                txtWeight.Text = pSelectedDataRow["patientweight"] == DBNull.Value ? "" : pSelectedDataRow["patientweight"].ToString();
                txtHeight.Text = pSelectedDataRow["patientheight"] == DBNull.Value ? "" : pSelectedDataRow["patientheight"].ToString();
                txtMUAC.Text = pSelectedDataRow["muac"] == DBNull.Value ? "" : pSelectedDataRow["muac"].ToString();
                cboWasting.ItemIndex = Program.Get_LookupItemIndex(cboWasting, "code", pSelectedDataRow["wastingcode"].ToString());
                cboBreastFeeding.ItemIndex = Program.Get_LookupItemIndex(cboBreastFeeding, "code", pSelectedDataRow["breastfeedingcode"].ToString());
                cboMotherStatus.ItemIndex = Program.Get_LookupItemIndex(cboMotherStatus, "code", pSelectedDataRow["motherstatuscode"].ToString());
                cboTBStatus.ItemIndex = Program.Get_LookupItemIndex(cboTBStatus, "code", pSelectedDataRow["tbstatuscode"].ToString());
                cboClinMonit.ItemIndex = Program.Get_LookupItemIndex(cboClinMonit, "code", pSelectedDataRow["clinmonit"].ToString());
                cboHIVInfection.ItemIndex = Program.Get_LookupItemIndex(cboHIVInfection, "code", pSelectedDataRow["hivinfectioncode"].ToString());
                txtCPTMills.Text = pSelectedDataRow["cptmills"].ToString();
                txtCPTtablets.Text = pSelectedDataRow["cpttablets"].ToString();
                cboOutcome.ItemIndex = Program.Get_LookupItemIndex(cboOutcome, "code", pSelectedDataRow["outcomecode"].ToString());
                txtOutcomeDate.EditValue = null;
                if (pSelectedDataRow["outcomedate"] != DBNull.Value)
                {
                    txtOutcomeDate.EditValue = Convert.ToDateTime(pSelectedDataRow["outcomedate"]);
                }
                txtNextVisitDate.EditValue = null;
                if (pSelectedDataRow["nextvisitdate"] != DBNull.Value)
                {
                    txtNextVisitDate.EditValue = Convert.ToDateTime(pSelectedDataRow["nextvisitdate"]);
                }
                cboClinician.ItemIndex = Program.Get_LookupItemIndex(cboClinician, "code", pSelectedDataRow["cliniciancode"].ToString());
                txtNotes.Text = pSelectedDataRow["notes"] == DBNull.Value ? "" : pSelectedDataRow["notes"].ToString();
            }
            else
            {
                //current weight measurements if any
                DataTable mDtTriage = pMdtReporter.View_LookupData("view_ctc_lasttriage", "*",
                    "patientcode='" + txtPatientId.Text.Trim() + "'", "", "", "");
                if (mDtTriage.Rows.Count > 0)
                {
                    txtWeight.Text = mDtTriage.Rows[0]["weight"].ToString();
                    txtHeight.Text = mDtTriage.Rows[0]["height"].ToString();
                }
                else
                {
                    txtWeight.Text = "";
                    txtHeight.Text = "";
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
                return;
            }

            try
            {
                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

                double mMuac = 0;
                string mWastingCode = "";
                string mBreastFeedingCode = "";
                string mMotherStatusCode = "";
                string mTBStatusCode = "";
                string mClinMonit = "";
                string mHIVInfectionCode = "";
                double mCPTMills = 0;
                double mCPTTablets = 0;
                string mOutcomeCode = "";
                DateTime? mOutcomeDate = null;
                DateTime? mNextVisitDate = null;
                string mClinicianCode = "";

                if (Program.IsMoney(txtMUAC.Text) == true)
                {
                    mMuac = Convert.ToDouble(txtMUAC.Text);
                }

                if (cboWasting.ItemIndex >= 0)
                {
                    mWastingCode = cboWasting.GetColumnValue("code").ToString();
                }

                if (cboBreastFeeding.ItemIndex >= 0)
                {
                    mBreastFeedingCode = cboBreastFeeding.GetColumnValue("code").ToString();
                }

                if (cboMotherStatus.ItemIndex >= 0)
                {
                    mMotherStatusCode = cboMotherStatus.GetColumnValue("code").ToString();
                }

                if (cboTBStatus.ItemIndex >= 0)
                {
                    mTBStatusCode = cboTBStatus.GetColumnValue("code").ToString();
                }

                if (cboClinMonit.ItemIndex >= 0)
                {
                    mClinMonit = cboClinMonit.GetColumnValue("code").ToString();
                }

                if (cboHIVInfection.ItemIndex >= 0)
                {
                    mHIVInfectionCode = cboHIVInfection.GetColumnValue("code").ToString();
                }

                if (cboOutcome.ItemIndex >= 0)
                {
                    mOutcomeCode = cboOutcome.GetColumnValue("code").ToString();
                }

                if (Program.IsDate(txtOutcomeDate.EditValue) == true)
                {
                    mOutcomeDate = Convert.ToDateTime(txtOutcomeDate.EditValue);
                }

                if (Program.IsDate(txtNextVisitDate.EditValue) == true)
                {
                    mNextVisitDate = Convert.ToDateTime(txtNextVisitDate.EditValue);
                }

                if (Program.IsMoney(txtCPTMills.Text) == true)
                {
                    mCPTMills = Convert.ToDouble(txtCPTMills.Text);
                }

                if (Program.IsMoney(txtCPTtablets.Text) == true)
                {
                    mCPTTablets = Convert.ToDouble(txtCPTtablets.Text);
                }

                if (cboClinician.ItemIndex >= 0)
                {
                    mClinicianCode = cboClinician.GetColumnValue("code").ToString();
                }

                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Add_Exposed(
                    txtPatientId.Text,
                    mTransDate,
                    mMuac,
                    mWastingCode,
                    mBreastFeedingCode,
                    mMotherStatusCode,
                    mTBStatusCode,
                    mClinMonit,
                    mHIVInfectionCode,
                    mCPTMills,
                    mCPTTablets,
                    mOutcomeCode,
                    mOutcomeDate,
                    mNextVisitDate,
                    mClinicianCode,
                    txtNotes.Text,
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

            if (Program.IsDate(txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                return;
            }

            try
            {
                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

                double mMuac = 0;
                string mWastingCode = "";
                string mBreastFeedingCode = "";
                string mMotherStatusCode = "";
                string mTBStatusCode = "";
                string mClinMonit = "";
                string mHIVInfectionCode = "";
                double mCPTMills = 0;
                double mCPTTablets = 0;
                string mOutcomeCode = "";
                DateTime? mOutcomeDate = null;
                DateTime? mNextVisitDate = null;
                string mClinicianCode = "";

                if (Program.IsMoney(txtMUAC.Text) == true)
                {
                    mMuac = Convert.ToDouble(txtMUAC.Text);
                }

                if (cboWasting.ItemIndex >= 0)
                {
                    mWastingCode = cboWasting.GetColumnValue("code").ToString();
                }

                if (cboBreastFeeding.ItemIndex >= 0)
                {
                    mBreastFeedingCode = cboBreastFeeding.GetColumnValue("code").ToString();
                }

                if (cboMotherStatus.ItemIndex >= 0)
                {
                    mMotherStatusCode = cboMotherStatus.GetColumnValue("code").ToString();
                }

                if (cboTBStatus.ItemIndex >= 0)
                {
                    mTBStatusCode = cboTBStatus.GetColumnValue("code").ToString();
                }

                if (cboClinMonit.ItemIndex >= 0)
                {
                    mClinMonit = cboClinMonit.GetColumnValue("code").ToString();
                }

                if (cboHIVInfection.ItemIndex >= 0)
                {
                    mHIVInfectionCode = cboHIVInfection.GetColumnValue("code").ToString();
                }

                if (cboOutcome.ItemIndex >= 0)
                {
                    mOutcomeCode = cboOutcome.GetColumnValue("code").ToString();
                }

                if (Program.IsDate(txtOutcomeDate.EditValue) == true)
                {
                    mOutcomeDate = Convert.ToDateTime(txtOutcomeDate.EditValue);
                }

                if (Program.IsDate(txtNextVisitDate.EditValue) == true)
                {
                    mNextVisitDate = Convert.ToDateTime(txtNextVisitDate.EditValue);
                }

                if (Program.IsMoney(txtCPTMills.Text) == true)
                {
                    mCPTMills = Convert.ToDouble(txtCPTMills.Text);
                }

                if (Program.IsMoney(txtCPTtablets.Text) == true)
                {
                    mCPTTablets = Convert.ToDouble(txtCPTtablets.Text);
                }

                if (cboClinician.ItemIndex >= 0)
                {
                    mClinicianCode = cboClinician.GetColumnValue("code").ToString();
                }

                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Edit_Exposed(
                    Convert.ToInt32(pSelectedDataRow["autocode"]),
                    mTransDate,
                    mMuac,
                    mWastingCode,
                    mBreastFeedingCode,
                    mMotherStatusCode,
                    mTBStatusCode,
                    mClinMonit,
                    mHIVInfectionCode,
                    mCPTMills,
                    mCPTTablets,
                    mOutcomeCode,
                    mOutcomeDate,
                    mNextVisitDate,
                    mClinicianCode,
                    txtNotes.Text,
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

        #region frmCTCExposedVisitDetails_KeyDown
        void frmCTCExposedVisitDetails_KeyDown(object sender, KeyEventArgs e)
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
    }
}