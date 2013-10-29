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
    public partial class frmCTCARTTVisitDetails : DevExpress.XtraEditors.XtraForm
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
        private DataTable pDtComeds = new DataTable("comeds");
        private DataTable pDtSideEffects = new DataTable("sideeffects");
        private DataTable pDtAbnormalResults = new DataTable("abnormalresults");
        private DataTable pDtReferrals = new DataTable("referrals");
        private DataTable pDtFPlanMethods = new DataTable("fplanmethods");

        private bool pRecordSaved = false;
        internal bool RecordSaved
        {
            set { pRecordSaved = value; }
            get { return pRecordSaved; }
        }

        #endregion

        #region frmCTCARTTVisitDetails
        public frmCTCARTTVisitDetails(DataRow mSelectedDataRow = null)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCARTTVisitDetails";
            this.KeyDown += new KeyEventHandler(frmCTCARTTVisitDetails_KeyDown);

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

                pDtSideEffects.Columns.Add("code", typeof(System.String));

                pDtComeds.Columns.Add("code", typeof(System.String));

                pDtAbnormalResults.Columns.Add("code", typeof(System.String));

                pDtFPlanMethods.Columns.Add("code", typeof(System.String));

                pDtReferrals.Columns.Add("code", typeof(System.String));

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

        #region frmCTCARTTVisitDetails_Load
        private void frmCTCARTTVisitDetails_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCARTTVisitDetails_Load";

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

        #region frmCTCARTTVisitDetails_Shown
        private void frmCTCARTTVisitDetails_Shown(object sender, EventArgs e)
        {
            txtDate.EditValue = DateTime.Now.Date;

            if (pSelectedPatient != null)
            {
                this.Display_Patient(pSelectedPatient);
            }
        }
        #endregion

        #region frmCTCARTTVisitDetails_FormClosing
        private void frmCTCARTTVisitDetails_FormClosing(object sender, FormClosingEventArgs e)
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
                #region visittypes

                DataTable mDtVisitTypes = pMdtReporter.View_LookupData("ctc_visittypes", "code,description", "", "", Program.gLanguageName, "grdCTCVisitTypes");
                cboVisitType.Properties.DataSource = mDtVisitTypes;
                cboVisitType.Properties.DisplayMember = "description";
                cboVisitType.Properties.ValueMember = "code";
                cboVisitType.Properties.BestFit();

                #endregion

                #region functional status

                DataTable mDtFunctionalStatus = pMdtReporter.View_LookupData("ctc_functionalstatus", "code,description", "", "", Program.gLanguageName, "grdCTCFunctionalStatus");
                cboFunctionalStatus.Properties.DataSource = mDtFunctionalStatus;
                cboFunctionalStatus.Properties.DisplayMember = "description";
                cboFunctionalStatus.Properties.ValueMember = "code";
                cboFunctionalStatus.Properties.BestFit();

                #endregion

                #region TB Screening

                DataTable mDtTBScreening = pMdtReporter.View_LookupData("ctc_tbscreening", "code,description", "", "", Program.gLanguageName, "grdCTCTBScreening");
                cboTBScreening.Properties.DataSource = mDtTBScreening;
                cboTBScreening.Properties.DisplayMember = "description";
                cboTBScreening.Properties.ValueMember = "code";
                cboTBScreening.Properties.BestFit();

                #endregion

                #region TB Treatments

                DataTable mDtTBRx = pMdtReporter.View_LookupData("ctc_tbtreatments", "code,description", "", "", Program.gLanguageName, "grdCTCTBTreatments");
                cboTBRx.Properties.DataSource = mDtTBRx;
                cboTBRx.Properties.DisplayMember = "description";
                cboTBRx.Properties.ValueMember = "code";
                cboTBRx.Properties.BestFit();

                #endregion

                #region arv status

                DataTable mDtARVStatus = pMdtReporter.View_LookupData("ctc_arvstatus", "code,description", "", "", Program.gLanguageName, "grdCTCARVStatus");
                cboARVStatus.Properties.DataSource = mDtARVStatus;
                cboARVStatus.Properties.DisplayMember = "description";
                cboARVStatus.Properties.ValueMember = "code";
                cboARVStatus.Properties.BestFit();

                #endregion

                #region arv reason

                DataTable mDtARVReason = pMdtReporter.View_LookupData("ctc_arvreason", "code,description", "", "", Program.gLanguageName, "grdCTCARVReason");
                cboARVReason.Properties.DataSource = mDtARVReason;
                cboARVReason.Properties.DisplayMember = "description";
                cboARVReason.Properties.ValueMember = "code";
                cboARVReason.Properties.BestFit();

                #endregion

                #region arv comb regimens

                DataTable mDtARVCombRegimens = pMdtReporter.View_LookupData("ctc_arvcombregimens", "code,description", "", "", Program.gLanguageName, "grdCTCARVCombRegimens");
                cboARVComb.Properties.DataSource = mDtARVCombRegimens;
                cboARVComb.Properties.DisplayMember = "description";
                cboARVComb.Properties.ValueMember = "code";
                cboARVComb.Properties.BestFit();

                #endregion

                #region arv adherence

                DataTable mDtARVAdherence = pMdtReporter.View_LookupData("ctc_arvadherence", "code,description", "", "", Program.gLanguageName, "grdCTCARVAdherence");
                cboARVAdherence.Properties.DataSource = mDtARVAdherence;
                cboARVAdherence.Properties.DisplayMember = "description";
                cboARVAdherence.Properties.ValueMember = "code";
                cboARVAdherence.Properties.BestFit();

                #endregion

                #region arv poor adherence reasons

                DataTable mDtARVPoorAdherenceReasons = pMdtReporter.View_LookupData("ctc_arvpooradherencereasons", "code,description", "", "", Program.gLanguageName, "grdCTCARVPoorAdherenceReasons");
                cboARVPoorAdherenceReason.Properties.DataSource = mDtARVPoorAdherenceReasons;
                cboARVPoorAdherenceReason.Properties.DisplayMember = "description";
                cboARVPoorAdherenceReason.Properties.ValueMember = "code";
                cboARVPoorAdherenceReason.Properties.BestFit();

                #endregion

                #region nutritionalstatus

                DataTable mDtNutritionalStatus = pMdtReporter.View_LookupData("ctc_nutritionalstatus", "code,description", "", "", Program.gLanguageName, "grdCTCReferredTo");
                cboNutritionalStatus.Properties.DataSource = mDtNutritionalStatus;
                cboNutritionalStatus.Properties.DisplayMember = "description";
                cboNutritionalStatus.Properties.ValueMember = "code";
                cboNutritionalStatus.Properties.BestFit();

                #endregion

                #region nutritionalsupp

                DataTable mDtNutritionalSupp = pMdtReporter.View_LookupData("ctc_nutritionalsupp", "code,description", "", "", Program.gLanguageName, "grdCTCReferredTo");
                cboNutritionalSupp.Properties.DataSource = mDtNutritionalSupp;
                cboNutritionalSupp.Properties.DisplayMember = "description";
                cboNutritionalSupp.Properties.ValueMember = "code";
                cboNutritionalSupp.Properties.BestFit();

                #endregion

                #region follow up status

                DataTable mDtFollowUpStatus = pMdtReporter.View_LookupData("ctc_followupstatus", "code,description", "", "", Program.gLanguageName, "grdCTCFollowUpStatus");
                cboFollowUpStatus.Properties.DataSource = mDtFollowUpStatus;
                cboFollowUpStatus.Properties.DisplayMember = "description";
                cboFollowUpStatus.Properties.ValueMember = "code";
                cboFollowUpStatus.Properties.BestFit();

                #endregion

                #region clinicians

                DataTable mDtClinicians = pMdtReporter.View_LookupData("facilitystaffs", "code,description,treatmentpointcode",
                    "category=" + Convert.ToInt16(AfyaPro_Types.clsEnums.StaffCategories.MedicalDoctors),
                    "description", Program.gLanguageName, "grdGENMedicalStaffs");

                cboClinician.Properties.DataSource = mDtClinicians;
                cboClinician.Properties.DisplayMember = "description";
                cboClinician.Properties.ValueMember = "code";

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

                pDtSideEffects.Rows.Clear();
                pDtComeds.Rows.Clear();
                pDtAbnormalResults.Rows.Clear();

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
                cboVisitType.ItemIndex = Program.Get_LookupItemIndex(cboVisitType, "code", pSelectedDataRow["visittypecode"].ToString());
                txtWeight.Text = pSelectedDataRow["patientweight"] == DBNull.Value ? "" : pSelectedDataRow["patientweight"].ToString();
                txtHeight.Text = pSelectedDataRow["patientheight"] == DBNull.Value ? "" : pSelectedDataRow["patientheight"].ToString();
                txtCD4.Text = pSelectedDataRow["cd4count"] == DBNull.Value ? "" : pSelectedDataRow["cd4count"].ToString();
                txtCD4CountPercent.Text = pSelectedDataRow["cd4countpercent"] == DBNull.Value ? "" : pSelectedDataRow["cd4countpercent"].ToString();
                cboClinicalStage.Text = pSelectedDataRow["clinicalstage"] == DBNull.Value ? "" : pSelectedDataRow["clinicalstage"].ToString();
                cboFunctionalStatus.ItemIndex = Program.Get_LookupItemIndex(cboFunctionalStatus, "code", pSelectedDataRow["functionalstatuscode"].ToString());
                chkPregnant.Checked = Convert.ToBoolean(pSelectedDataRow["pregnant"]);
                txtEDDate.EditValue = null;
                if (pSelectedDataRow["edddate"] != DBNull.Value)
                {
                    txtEDDate.EditValue = Convert.ToDateTime(pSelectedDataRow["edddate"]);
                }
                txtANCNo.Text = pSelectedDataRow["ancno"].ToString();
                cboTBScreening.ItemIndex = Program.Get_LookupItemIndex(cboTBScreening, "code", pSelectedDataRow["tbscreeningcode"].ToString());
                cboTBRx.ItemIndex = Program.Get_LookupItemIndex(cboTBRx, "code", pSelectedDataRow["tbrxcode"].ToString());
                cboARVStatus.ItemIndex = Program.Get_LookupItemIndex(cboARVStatus, "code", pSelectedDataRow["arvstatuscode"].ToString());
                cboARVReason.ItemIndex = Program.Get_LookupItemIndex(cboARVReason, "code", pSelectedDataRow["arvreasoncode"].ToString());
                cboARVComb.ItemIndex = Program.Get_LookupItemIndex(cboARVComb, "code", pSelectedDataRow["arvcombregimencode"].ToString());
                txtARVCombDays.Text = pSelectedDataRow["arvcombregimendays"].ToString();
                cboARVAdherence.ItemIndex = Program.Get_LookupItemIndex(cboARVAdherence, "code", pSelectedDataRow["arvadherencecode"].ToString());
                cboARVPoorAdherenceReason.ItemIndex = Program.Get_LookupItemIndex(cboARVPoorAdherenceReason, "code", pSelectedDataRow["arvpooradherencereasoncode"].ToString());
                txtHB.Text = pSelectedDataRow["hb"] == DBNull.Value ? "" : pSelectedDataRow["hb"].ToString();
                txtALT.Text = pSelectedDataRow["alt"] == DBNull.Value ? "" : pSelectedDataRow["alt"].ToString();
                cboNutritionalStatus.ItemIndex = Program.Get_LookupItemIndex(cboNutritionalStatus, "code", pSelectedDataRow["nutritionalstatuscode"].ToString());
                cboNutritionalSupp.ItemIndex = Program.Get_LookupItemIndex(cboNutritionalSupp, "code", pSelectedDataRow["nutritionalsuppcode"].ToString());
                txtNextVisitDate.EditValue = null;
                if (pSelectedDataRow["nextvisitdate"] != DBNull.Value)
                {
                    txtNextVisitDate.EditValue = Convert.ToDateTime(pSelectedDataRow["nextvisitdate"]);
                }
                cboFollowUpStatus.ItemIndex = Program.Get_LookupItemIndex(cboFollowUpStatus, "code", pSelectedDataRow["followupstatuscode"].ToString());
                cboClinician.ItemIndex = Program.Get_LookupItemIndex(cboClinician, "code", pSelectedDataRow["cliniciancode"].ToString());

                //sideeffects
                pDtSideEffects = pMdtReporter.View_LookupData("ctc_patientaidsillness", "illnesscode AS code",
                    "booking='" + pSelectedDataRow["booking"] + "' and patientcode='" + txtPatientId.Text.Trim() + "'", "", "", "");
                string mSideEffects = "";
                foreach (DataRow mDataRow in pDtSideEffects.Rows)
                {
                    if (mSideEffects.Trim() == "")
                    {
                        mSideEffects = mDataRow["code"].ToString().Trim();
                    }
                    else
                    {
                        mSideEffects = mSideEffects + "; " + mDataRow["code"].ToString().Trim();
                    }
                }
                cmdIllness.Text = mSideEffects;

                //comeds
                pDtComeds = pMdtReporter.View_LookupData("ctc_patientcomeds", "medcode AS code",
                    "booking='" + pSelectedDataRow["booking"] + "' and patientcode='" + txtPatientId.Text.Trim() + "'", "", "", "");
                string mComeds = "";
                foreach (DataRow mDataRow in pDtComeds.Rows)
                {
                    if (mComeds.Trim() == "")
                    {
                        mComeds = mDataRow["code"].ToString().Trim();
                    }
                    else
                    {
                        mComeds = mComeds + "; " + mDataRow["code"].ToString().Trim();
                    }
                }
                cmdRelevantComeds.Text = mComeds;

                //abnormal lab result
                pDtAbnormalResults = pMdtReporter.View_LookupData("ctc_patientabnormallabresults", "resultcode AS code",
                    "booking='" + pSelectedDataRow["booking"] + "' and patientcode='" + txtPatientId.Text.Trim() + "'", "", "", "");
                string mLabResults = "";
                foreach (DataRow mDataRow in pDtAbnormalResults.Rows)
                {
                    if (mLabResults.Trim() == "")
                    {
                        mLabResults = mDataRow["code"].ToString().Trim();
                    }
                    else
                    {
                        mLabResults = mLabResults + "; " + mDataRow["code"].ToString().Trim();
                    }
                }
                cmdAbnormalLab.Text = mLabResults;

                //referrals
                pDtReferrals = pMdtReporter.View_LookupData("ctc_patientreferrals", "referedtocode AS code",
                    "booking='" + pSelectedDataRow["booking"] + "' and patientcode='" + txtPatientId.Text.Trim() + "'", "", "", "");
                string mReferrals = "";
                foreach (DataRow mDataRow in pDtReferrals.Rows)
                {
                    if (mReferrals.Trim() == "")
                    {
                        mReferrals = mDataRow["code"].ToString().Trim();
                    }
                    else
                    {
                        mReferrals = mReferrals + "; " + mDataRow["code"].ToString().Trim();
                    }
                }
                cmdReferedTo.Text = mReferrals;

                //familyplanning
                pDtFPlanMethods = pMdtReporter.View_LookupData("ctc_patientfplanmethods", "methodcode AS code",
                    "booking='" + pSelectedDataRow["booking"] + "' and patientcode='" + txtPatientId.Text.Trim() + "'", "", "", "");
                string mFPlanMethods = "";
                foreach (DataRow mDataRow in pDtFPlanMethods.Rows)
                {
                    if (mFPlanMethods.Trim() == "")
                    {
                        mFPlanMethods = mDataRow["code"].ToString().Trim();
                    }
                    else
                    {
                        mFPlanMethods = mFPlanMethods + "; " + mDataRow["code"].ToString().Trim();
                    }
                }
                cmdFPlanMethods.Text = mFPlanMethods;
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

                //current cd4 count if any
                DataTable mDtCD4Tests = pMdtReporter.View_LookupData("view_ctc_lastcd4tests", "*",
                    "patientcode='" + txtPatientId.Text.Trim() + "'", "", "", "");
                if (mDtCD4Tests.Rows.Count > 0)
                {
                    txtCD4.Text = mDtCD4Tests.Rows[0]["testresult"].ToString();
                    txtCD4CountPercent.Text = mDtCD4Tests.Rows[0]["testresultpercent"].ToString();
                }
                else
                {
                    txtCD4.Text = "";
                    txtCD4CountPercent.Text = "";
                }
            }
        }
        #endregion

        #region Data_New
        private void Data_New()
        {
            string mFunctionName = "Data_New";

            if (txtCTCNo.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.CTC_CTCNoIsInvalid.ToString());
                txtCTCNo.Focus();
                return;
            }

            if (Program.IsDate(Program.gMdiForm.txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                return;
            }

            try
            {
                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                string mVisitTypeCode = "";
                int mClinicalStage = 0;
                string mFunctionalStatusCode = "";
                string mARVStatusCode = "";
                string mARVReasonCode = "";
                string mARVCombRegimenCode = "";
                string mARVAdherenceCode = "";
                string mARVPoorAdherenceReasonCode = "";
                string mReferredToCode = "";
                string mFollowUpStatusCode = "";
                double mHB = 0;
                double mALT = 0;
                DateTime mNextVisitDate = new DateTime();
                DateTime? mEDDDate = null;
                string mClinicianCode = "";
                string mTBScreeningCode = "";
                string mTBRxCode = "";
                int mARVCombRegimenDays = 0;
                string mNutritionalStatusCode = "";
                string mNutritionalSuppCode = "";

                if (cboVisitType.ItemIndex >= 0)
                {
                    mVisitTypeCode = cboVisitType.GetColumnValue("code").ToString();
                }

                if (Program.IsNumeric(cboClinicalStage.Text) == true)
                {
                    mClinicalStage = Convert.ToInt32(cboClinicalStage.Text);
                }

                if (Program.IsDate(txtEDDate.EditValue) == true)
                {
                    mEDDDate = Convert.ToDateTime(txtEDDate.EditValue);
                }

                if (Program.IsMoney(txtHB.Text) == true)
                {
                    mHB = Convert.ToDouble(txtHB.Text);
                }

                if (Program.IsMoney(txtALT.Text) == true)
                {
                    mALT = Convert.ToDouble(txtALT.Text);
                }

                if (Program.IsDate(txtNextVisitDate.EditValue) == true)
                {
                    mNextVisitDate = Convert.ToDateTime(txtNextVisitDate.EditValue);
                }

                if (cboFunctionalStatus.ItemIndex >= 0)
                {
                    mFunctionalStatusCode = cboFunctionalStatus.GetColumnValue("code").ToString();
                }

                if (cboTBScreening.ItemIndex >= 0)
                {
                    mTBScreeningCode = cboTBScreening.GetColumnValue("code").ToString();
                }

                if (cboTBRx.ItemIndex >= 0)
                {
                    mTBRxCode = cboTBRx.GetColumnValue("code").ToString();
                }

                if (cboARVStatus.ItemIndex >= 0)
                {
                    mARVStatusCode = cboARVStatus.GetColumnValue("code").ToString();
                }

                if (cboARVReason.ItemIndex >= 0)
                {
                    mARVReasonCode = cboARVReason.GetColumnValue("code").ToString();
                }

                if (cboARVComb.ItemIndex >= 0)
                {
                    mARVCombRegimenCode = cboARVComb.GetColumnValue("code").ToString();
                }

                if (Program.IsMoney(txtARVCombDays.Text) == true)
                {
                    mARVCombRegimenDays = Convert.ToInt32(txtARVCombDays.Text);
                }

                if (cboARVAdherence.ItemIndex >= 0)
                {
                    mARVAdherenceCode = cboARVAdherence.GetColumnValue("code").ToString();
                }

                if (cboARVPoorAdherenceReason.ItemIndex >= 0)
                {
                    mARVPoorAdherenceReasonCode = cboARVPoorAdherenceReason.GetColumnValue("code").ToString();
                }

                if (cboNutritionalStatus.ItemIndex >= 0)
                {
                    mNutritionalStatusCode = cboNutritionalStatus.GetColumnValue("code").ToString();
                }

                if (cboNutritionalSupp.ItemIndex >= 0)
                {
                    mNutritionalSuppCode = cboNutritionalSupp.GetColumnValue("code").ToString();
                }

                if (cboFollowUpStatus.ItemIndex >= 0)
                {
                    mFollowUpStatusCode = cboFollowUpStatus.GetColumnValue("code").ToString();
                }

                if (cboClinician.ItemIndex >= 0)
                {
                    mClinicianCode = cboClinician.GetColumnValue("code").ToString();
                }

                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Add_ARTT(
                    mTransDate,
                    txtPatientId.Text,
                    mVisitTypeCode,
                    mClinicalStage,
                    Convert.ToInt32(chkPregnant.Checked),
                    mEDDDate,
                    txtANCNo.Text,
                    mFunctionalStatusCode,
                    mTBScreeningCode,
                    mTBRxCode,
                    mARVStatusCode,
                    mARVReasonCode,
                    mARVCombRegimenCode,
                    mARVCombRegimenDays,
                    mARVAdherenceCode,
                    mARVPoorAdherenceReasonCode,
                    mHB,
                    mALT,
                    mNutritionalStatusCode,
                    mNutritionalSuppCode,
                    mNextVisitDate,
                    mFollowUpStatusCode,
                    mClinicianCode,
                    pDtSideEffects,
                    pDtComeds,
                    pDtAbnormalResults,
                    pDtFPlanMethods,
                    pDtReferrals,
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

            if (txtCTCNo.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.CTC_CTCNoIsInvalid.ToString());
                txtCTCNo.Focus();
                return;
            }

            if (Program.IsDate(Program.gMdiForm.txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                return;
            }

            try
            {
                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                string mVisitTypeCode = "";
                int mClinicalStage = 0;
                string mFunctionalStatusCode = "";
                string mARVStatusCode = "";
                string mARVReasonCode = "";
                string mARVCombRegimenCode = "";
                string mARVAdherenceCode = "";
                string mARVPoorAdherenceReasonCode = "";
                string mFollowUpStatusCode = "";
                double mHB = 0;
                double mALT = 0;
                DateTime mNextVisitDate = new DateTime();
                DateTime? mEDDDate = null;
                string mClinicianCode = "";
                string mTBScreeningCode = "";
                string mTBRxCode = "";
                int mARVCombRegimenDays = 0;
                string mNutritionalStatusCode = "";
                string mNutritionalSuppCode = "";

                if (cboVisitType.ItemIndex >= 0)
                {
                    mVisitTypeCode = cboVisitType.GetColumnValue("code").ToString();
                }

                if (Program.IsNumeric(cboClinicalStage.Text) == true)
                {
                    mClinicalStage = Convert.ToInt32(cboClinicalStage.Text);
                }

                if (Program.IsDate(txtEDDate.EditValue) == true)
                {
                    mEDDDate = Convert.ToDateTime(txtEDDate.EditValue);
                }

                if (Program.IsMoney(txtHB.Text) == true)
                {
                    mHB = Convert.ToDouble(txtHB.Text);
                }

                if (Program.IsMoney(txtALT.Text) == true)
                {
                    mALT = Convert.ToDouble(txtALT.Text);
                }

                if (Program.IsDate(txtNextVisitDate.EditValue) == true)
                {
                    mNextVisitDate = Convert.ToDateTime(txtNextVisitDate.EditValue);
                }

                if (cboFunctionalStatus.ItemIndex >= 0)
                {
                    mFunctionalStatusCode = cboFunctionalStatus.GetColumnValue("code").ToString();
                }

                if (cboTBScreening.ItemIndex >= 0)
                {
                    mTBScreeningCode = cboTBScreening.GetColumnValue("code").ToString();
                }

                if (cboTBRx.ItemIndex >= 0)
                {
                    mTBRxCode = cboTBRx.GetColumnValue("code").ToString();
                }

                if (cboARVStatus.ItemIndex >= 0)
                {
                    mARVStatusCode = cboARVStatus.GetColumnValue("code").ToString();
                }

                if (cboARVReason.ItemIndex >= 0)
                {
                    mARVReasonCode = cboARVReason.GetColumnValue("code").ToString();
                }

                if (cboARVComb.ItemIndex >= 0)
                {
                    mARVCombRegimenCode = cboARVComb.GetColumnValue("code").ToString();
                }

                if (Program.IsMoney(txtARVCombDays.Text) == true)
                {
                    mARVCombRegimenDays = Convert.ToInt32(txtARVCombDays.Text);
                }

                if (cboARVAdherence.ItemIndex >= 0)
                {
                    mARVAdherenceCode = cboARVAdherence.GetColumnValue("code").ToString();
                }

                if (cboARVPoorAdherenceReason.ItemIndex >= 0)
                {
                    mARVPoorAdherenceReasonCode = cboARVPoorAdherenceReason.GetColumnValue("code").ToString();
                }

                if (cboNutritionalStatus.ItemIndex >= 0)
                {
                    mNutritionalStatusCode = cboNutritionalStatus.GetColumnValue("code").ToString();
                }

                if (cboNutritionalSupp.ItemIndex >= 0)
                {
                    mNutritionalSuppCode = cboNutritionalSupp.GetColumnValue("code").ToString();
                }

                if (cboFollowUpStatus.ItemIndex >= 0)
                {
                    mFollowUpStatusCode = cboFollowUpStatus.GetColumnValue("code").ToString();
                }

                if (cboClinician.ItemIndex >= 0)
                {
                    mClinicianCode = cboClinician.GetColumnValue("code").ToString();
                }

                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Edit_ARTT(
                    Convert.ToInt32(pSelectedDataRow["autocode"]),
                    mTransDate,
                    mVisitTypeCode,
                    mClinicalStage,
                    Convert.ToInt32(chkPregnant.Checked),
                    mEDDDate,
                    txtANCNo.Text,
                    mFunctionalStatusCode,
                    mTBScreeningCode,
                    mTBRxCode,
                    mARVStatusCode,
                    mARVReasonCode,
                    mARVCombRegimenCode,
                    mARVCombRegimenDays,
                    mARVAdherenceCode,
                    mARVPoorAdherenceReasonCode,
                    mHB,
                    mALT,
                    mNutritionalStatusCode,
                    mNutritionalSuppCode,
                    mNextVisitDate,
                    mFollowUpStatusCode,
                    mClinicianCode,
                    pDtSideEffects,
                    pDtComeds,
                    pDtAbnormalResults,
                    pDtFPlanMethods,
                    pDtReferrals,
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

        #region frmCTCARTTVisitDetails_KeyDown
        void frmCTCARTTVisitDetails_KeyDown(object sender, KeyEventArgs e)
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

        #region cmdIllness_ButtonClick
        private void cmdIllness_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            frmCTCSideEffects mCTCSideEffects = new frmCTCSideEffects(pDtSideEffects);
            mCTCSideEffects.ShowDialog();

            cmdIllness.Text = mCTCSideEffects.SelectedCodes;
        }
        #endregion

        #region cmdRelevantComeds_ButtonClick
        private void cmdRelevantComeds_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            frmCTCComeds mCTCComeds = new frmCTCComeds(pDtComeds);
            mCTCComeds.ShowDialog();

            cmdRelevantComeds.Text = mCTCComeds.SelectedCodes;
        }
        #endregion

        #region cmdAbnormalLab_ButtonClick
        private void cmdAbnormalLab_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            frmCTCAbnormalLabResults mCTCAbnormalLabResults = new frmCTCAbnormalLabResults(pDtAbnormalResults);
            mCTCAbnormalLabResults.ShowDialog();

            cmdAbnormalLab.Text = mCTCAbnormalLabResults.SelectedCodes;
        }
        #endregion

        #region cmdFPlanMethods_ButtonClick
        private void cmdFPlanMethods_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            frmCTCFPlanMethods mCTCFPlanMethods = new frmCTCFPlanMethods(pDtFPlanMethods);
            mCTCFPlanMethods.ShowDialog();

            cmdFPlanMethods.Text = mCTCFPlanMethods.SelectedCodes;
        }
        #endregion

        #region cmdReferedTo_ButtonClick
        private void cmdReferedTo_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            frmCTCReferrals mCTCReferrals = new frmCTCReferrals(pDtReferrals, "ctc_referedto");
            mCTCReferrals.ShowDialog();

            cmdReferedTo.Text = mCTCReferrals.SelectedCodes;
        }
        #endregion
    }
}