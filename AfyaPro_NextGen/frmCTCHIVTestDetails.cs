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
    public partial class frmCTCHIVTestDetails : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsReporter pMdtReporter;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_MT.clsCTCClients pMdtCTCClients;
        private AfyaPro_MT.clsRegistrations pMdtRegistrations;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private DataTable pDtAllReasonData = new DataTable("allreason");
        private DataTable pDtReasonData = new DataTable("reason");

        private DataTable pDtAllEducationData = new DataTable("alleducation");
        private DataTable pDtEducationData = new DataTable("education");

        private DataTable pDtAllSupportData = new DataTable("allsupport");
        private DataTable pDtSupportData = new DataTable("support");

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

        private string mCounsellorCode = "";
        internal string SelectedCounsellor
        {
            set { mCounsellorCode = value; }
            get { return mCounsellorCode; }
        }

        private bool pRecordSaved = false;
        internal bool RecordSaved
        {
            set { pRecordSaved = value; }
            get { return pRecordSaved; }
        }

        private DataRow pSelectedDataRow = null;

        private DataTable pDtHIVTeferals = new DataTable("referals");


        #endregion

        #region frmCTCHIVTestDetails
        public frmCTCHIVTestDetails(DataRow mSelectedDataRow = null)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCHIVTestDetails";
            this.KeyDown += new KeyEventHandler(frmCTCHIVTestDetails_KeyDown);

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                this.pSelectedDataRow = mSelectedDataRow;

                this.CancelButton = cmdClose;

                pMdtRegistrations = (AfyaPro_MT.clsRegistrations)Activator.GetObject(
                   typeof(AfyaPro_MT.clsRegistrations),
                   Program.gMiddleTier + "clsRegistrations");

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
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctchivtests_customizelayout.ToString());

                pDtHIVTeferals.Columns.Add("code", typeof(System.String));

                pDtReasonData.Columns.Add("reasoncode", typeof(System.String));

                pDtAllReasonData.Columns.Add("code", typeof(System.String));
                pDtAllReasonData.Columns.Add("description", typeof(System.String));               

                chklstReason.DataSource = pDtAllReasonData;
                chklstReason.DisplayMember = "description";
                chklstReason.ValueMember = "code";

                pDtAllEducationData.Columns.Add("code", typeof(System.String));
                pDtAllEducationData.Columns.Add("description", typeof(System.String));

                chklstEducation.DataSource = pDtAllEducationData;
                chklstEducation.DisplayMember = "description";
                chklstEducation.ValueMember = "code";

                pDtEducationData.Columns.Add("hiveducationcode", typeof(System.String));

                pDtAllSupportData.Columns.Add("code", typeof(System.String));
                pDtAllSupportData.Columns.Add("description", typeof(System.String));

                chklstSupportSystems.DataSource = pDtAllSupportData;
                chklstSupportSystems.DisplayMember = "description";
                chklstSupportSystems.ValueMember = "code";

                pDtSupportData.Columns.Add("supportcode", typeof(System.String));
                
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
            mObjectsList.Add(cmdSave);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region frmCTCHIVTestDetails_Load
        private void frmCTCHIVTestDetails_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCHIVTestDetails_Load";

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

        #region frmCTCHIVTestDetails_Shown
        private void frmCTCHIVTestDetails_Shown(object sender, EventArgs e)
        {
            this.Data_Clear();

            if (pSelectedPatient != null)
            {
                this.Display_Patient(pSelectedPatient);
            }
        }
        #endregion

        #region frmCTCHIVTestDetails_FormClosing
        private void frmCTCHIVTestDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            //layout
            if (layoutControl1.IsModified == true)
            {
                Program.Save_FormLayout(this, layoutControl1, this.Name);
            }
        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region counsellors

                DataTable mDtCounsellors = pMdtReporter.View_LookupData("facilitystaffs", "code,description,treatmentpointcode",
                    "category=" + Convert.ToInt16(AfyaPro_Types.clsEnums.StaffCategories.Counsellors),
                    "description", Program.gLanguageName, "grdGENMedicalStaffs");

                cboCounsellor.Properties.DataSource = mDtCounsellors;
                cboCounsellor.Properties.DisplayMember = "description";
                cboCounsellor.Properties.ValueMember = "code";

                #endregion

                #region hiv testing reason

                DataTable mDtData = pMdtReporter.View_LookupData("ctc_hivcounsellingreason", "code,description", "", "", Program.gLanguageName, "");

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    DataRow mNewRow = pDtAllReasonData.NewRow();
                    mNewRow["code"] = mDataRow["code"];
                    mNewRow["description"] = mDataRow["description"];
                    pDtAllReasonData.Rows.Add(mNewRow);
                    pDtAllReasonData.AcceptChanges();
                }
                #endregion                           

                #region hiv education

                DataTable mDtEducationData = pMdtReporter.View_LookupData("ctc_hiveducation", "code,description", "", "", Program.gLanguageName, "");

                foreach (DataRow mDataRow in mDtEducationData.Rows)
                {
                    DataRow mNewRow = pDtAllEducationData.NewRow();
                    mNewRow["code"] = mDataRow["code"];
                    mNewRow["description"] = mDataRow["description"];
                    pDtAllEducationData.Rows.Add(mNewRow);
                    pDtAllEducationData.AcceptChanges();
                }
                #endregion

                #region support systems

                DataTable mDtSupportData = pMdtReporter.View_LookupData("ctc_hivclientsupport", "code,description", "", "", Program.gLanguageName, "");

                foreach (DataRow mDataRow in mDtSupportData.Rows)
                {
                    DataRow mNewRow = pDtAllSupportData.NewRow();
                    mNewRow["code"] = mDataRow["code"];
                    mNewRow["description"] = mDataRow["description"];
                    pDtAllSupportData.Rows.Add(mNewRow);
                    pDtAllSupportData.AcceptChanges();
                }
                #endregion

                #region Counsellor Observaton

                DataTable mDtCounsellorObsn = pMdtReporter.View_LookupData("ctc_hivcounsellorobservation", "code,description",
                    "",
                    "description", Program.gLanguageName, "");

                cboPostCounsellorObsn.Properties.DataSource = mDtCounsellorObsn;
                cboPostCounsellorObsn.Properties.DisplayMember = "description";
                cboPostCounsellorObsn.Properties.ValueMember = "code";
               
                #endregion

                #region Appointment Reason

                DataTable mDtAppntReason = pMdtReporter.View_LookupData("ctc_hivappointmentreason", "code,description",
                    "",
                    "description", Program.gLanguageName, "");

                cboAppntReason.Properties.DataSource = mDtAppntReason;
                cboAppntReason.Properties.DisplayMember = "description";
                cboAppntReason.Properties.ValueMember = "code";
            
                #endregion

            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Append_ShortcutKeys
        private void Append_ShortcutKeys()
        {
            //cmdOk.Text = cmdOk.Text + " (" + Program.KeyCode_Ok.ToString() + ")";
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

                DataTable mDtMaritalstatus = pMdtReporter.View_LookupData("maritalstatus", "code,description",
                  "code = '" + mPatient.maritalstatuscode + "'", "description", Program.gLanguageName, "");

                if (mDtMaritalstatus.Rows.Count > 0)
                {
                    DataRow mDataRow =  mDtMaritalstatus.Rows[0];
                    txtMaritalStatus.Text = mDataRow["description"].ToString().Trim();
                }
                #endregion

                #region ctc_clients

                //pBooking = pMdtCTCClients.Get_Booking(mPatient.code);

                DataTable mDtClients = pMdtCTCClients.View_CTCClients("patientcode='" + mPatient.code + "'", "");

                if (mDtClients.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtClients.Rows[0];

                    txtHIVNo.Text = mDataRow["hivno"].ToString();
                    txtArvNo.Text = mDataRow["arvno"].ToString();
                    txtCTCNo.Text = mDataRow["ctcno"].ToString();
                }

                #endregion

                #region HIVno

                if (txtHIVNo.Text.Trim() == "")
                {
                    Int16 mGenerateCode = pMdtAutoCodes.Auto_Generate_Code(
                        Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.hivnumbers));
                    if (mGenerateCode == -1)
                    {
                        Program.Display_Server_Error("");
                        return;
                    }

                    if (mGenerateCode == 1)
                    {
                        txtHIVNo.Text = "<<---New--->>";
                    }
                }

                #endregion

                cboCounsellor.EditValue = mCounsellorCode;
                txtDate.EditValue = Program.gMdiForm.txtDate.EditValue;
                txtNextApptDate.EditValue = null;

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
                chkPregnant.Checked = Convert.ToBoolean(pSelectedDataRow["pregnant"]);
                chkEverTested.Checked = Convert.ToBoolean(pSelectedDataRow["evertested"]);
                chkWithPartner.Checked = Convert.ToBoolean(pSelectedDataRow["withpartner"]);
                cboResult1.SelectedIndex = Convert.ToInt32(pSelectedDataRow["testresult1"]);
                cboResult2.SelectedIndex = Convert.ToInt32(pSelectedDataRow["testresult2"]);
                cboResult3.SelectedIndex = Convert.ToInt32(pSelectedDataRow["testresult3"]);
                cboResultGiven.SelectedIndex = Convert.ToInt32(pSelectedDataRow["testresultgiven"]);
                
                txtResultGivenDate.EditValue = "";
                if (pSelectedDataRow["testresultgivendate"] != DBNull.Value)
                {
                    txtResultGivenDate.EditValue = Convert.ToDateTime(pSelectedDataRow["testresultgivendate"]);
                }
                txtComments.Text = pSelectedDataRow["comments"].ToString().Trim();
                cboCounsellor.ItemIndex = Program.Get_LookupItemIndex(cboCounsellor, "code", pSelectedDataRow["counsellorcode"].ToString().Trim());
                txtNextApptDate.EditValue = "";
                if (pSelectedDataRow["nextapptdate"] != DBNull.Value)
                {
                    txtNextApptDate.EditValue = Convert.ToDateTime(pSelectedDataRow["nextapptdate"]);
                }

                //Counselling data
                DataTable pDtCounsellingData = pMdtReporter.View_LookupData("ctc_hivcounselling", "problemnotes,observation,medicalsymptoms,reactionpositive,"
                + "reactionnegative,shareresultwith,preclientassessmentnotes,clientdecision,preinterventionnotes,"
                + "willingtoaccept,understoodinformation,resultcollectiondate,accompaniedby,postproblemnotes,willingtoreceiveresult,"
                + "postobservation,postobservationnotes,postintervention,postintervetionnotes,appointmentreasoncode",
                "booking='" + pSelectedDataRow["booking"] + "' and patientcode='" + txtPatientId.Text.Trim() + "'", "", "", "");

                if (pDtCounsellingData.Rows.Count > 0)
                {
                    DataRow mCounsellingDataRow = pDtCounsellingData.Rows[0];
                    txtPreProblemNotes.Text = mCounsellingDataRow["problemnotes"].ToString().Trim();
                    txtCounsellorObservation.Text = mCounsellingDataRow["observation"].ToString().Trim();
                    txtMedicalSymptoms.Text = mCounsellingDataRow["medicalsymptoms"].ToString().Trim();
                    txtReactionPositive.Text = mCounsellingDataRow["reactionpositive"].ToString().Trim();
                    txtReactionNegative.Text = mCounsellingDataRow["reactionnegative"].ToString().Trim();
                    txtShareresultwith.Text = mCounsellingDataRow["shareresultwith"].ToString().Trim();
                    txPreClientAssessmentNotes.Text = mCounsellingDataRow["preclientassessmentnotes"].ToString().Trim(); ;
                    radDecision.SelectedIndex = Convert.ToInt32(mCounsellingDataRow["clientdecision"]);
                    txtPreInterventionNotes.Text = mCounsellingDataRow["preinterventionnotes"].ToString().Trim();
                    chkWillingtoTest.Checked = Convert.ToBoolean(mCounsellingDataRow["willingtoaccept"]); ;
                    chkUnderstoodInformation.Checked = Convert.ToBoolean(mCounsellingDataRow["understoodinformation"]);
                    radAccompaniedBy.SelectedIndex = Convert.ToInt32(mCounsellingDataRow["accompaniedby"]);
                    txtPostProblemNotes.Text = mCounsellingDataRow["postproblemnotes"].ToString().Trim();
                    cboWillingToRcvResult.SelectedIndex = Convert.ToInt32(mCounsellingDataRow["willingtoreceiveresult"]);
                    cboPostCounsellorObsn.EditValue = mCounsellingDataRow["postobservation"].ToString().Trim();
                    txtPostObservationNotes.Text = mCounsellingDataRow["postobservationnotes"].ToString().Trim();
                    radPostIntervention.SelectedIndex = Convert.ToInt32(mCounsellingDataRow["postintervention"]);
                    txtPostInterventionNotes.Text = mCounsellingDataRow["postintervetionnotes"].ToString().Trim();
                    cboAppntReason.EditValue = mCounsellingDataRow["appointmentreasoncode"].ToString().Trim();

                    dtResultCollection.EditValue = "";
                    if (mCounsellingDataRow["resultcollectiondate"] != DBNull.Value)
                    {
                        dtResultCollection.EditValue = Convert.ToDateTime(mCounsellingDataRow["resultcollectiondate"]);
                    }
                }

                //referals
                pDtHIVTeferals = pMdtReporter.View_LookupData("ctc_hivtestreferals", "referalcode AS code",
                    "booking='" + pSelectedDataRow["booking"] + "' and patientcode='" + txtPatientId.Text.Trim() + "'", "", "", "");
                
                string mReferals = "";
                
                foreach (DataRow mDataRow in pDtHIVTeferals.Rows)
                {
                    if (mReferals.Trim() == "")
                    {
                        mReferals = mDataRow["code"].ToString().Trim();
                    }
                    else
                    {
                        mReferals = mReferals + "; " + mDataRow["code"].ToString().Trim();
                    }
                }
                cmdReferals.Text = mReferals;

                //Reason for testing
                pDtReasonData = pMdtReporter.View_LookupData("ctc_hivcounsellingreasonlog", "reasoncode",
                    "booking='" + pSelectedDataRow["booking"] + "' and patientcode='" + txtPatientId.Text.Trim() + "'", "", "", "");
             
                DataView mDvData = new DataView();
                mDvData.Table = pDtReasonData;
                mDvData.Sort = "reasoncode";

                for (int i = 0; i < chklstReason.ItemCount; i++)
                {
                    int mRowIndex = mDvData.Find(chklstReason.GetItemValue(i));

                    if (mRowIndex >= 0)
                    {
                        chklstReason.SetItemChecked(i, true);
                    }
                }

                //HIV Education
                pDtEducationData = pMdtReporter.View_LookupData("ctc_hiveducationlog", "hiveducationcode",
                    "booking='" + pSelectedDataRow["booking"] + "' and patientcode='" + txtPatientId.Text.Trim() + "'", "", "", "");

                DataView mDvEducationData = new DataView();
                mDvEducationData.Table = pDtEducationData;
                mDvEducationData.Sort = "hiveducationcode";

                for (int i = 0; i < chklstEducation.ItemCount; i++)
                {
                    int mRowIndex = mDvEducationData.Find(chklstEducation.GetItemValue(i));

                    if (mRowIndex >= 0)
                    {
                        chklstEducation.SetItemChecked(i, true);
                    }
                }

                //Client Support Systems
                pDtSupportData = pMdtReporter.View_LookupData("ctc_hivclientsupportlog", "supportcode",
                    "booking='" + pSelectedDataRow["booking"] + "' and patientcode='" + txtPatientId.Text.Trim() + "'", "", "", "");

                DataView mDvSupportData = new DataView();
                mDvSupportData.Table = pDtSupportData;
                mDvSupportData.Sort = "supportcode";

                for (int i = 0; i < chklstSupportSystems.ItemCount; i++)
                {
                    int mRowIndex = mDvSupportData.Find(chklstSupportSystems.GetItemValue(i));

                    if (mRowIndex >= 0)
                    {
                        chklstSupportSystems.SetItemChecked(i, true);
                    }
                }
            }
        }
        #endregion

        #region Data_Clear
        private void Data_Clear()
        {
            chkPregnant.Checked = false;
            chkEverTested.Checked = false;
            chkWithPartner.Checked = false;
            cboResult1.SelectedIndex = 0;
            cboResult2.SelectedIndex = 0;
            cboResult3.SelectedIndex = 0;
            cboResultGiven.SelectedIndex = 0;
            txtResultGivenDate.EditValue = null;
            txtComments.Text = "";
            txtNextApptDate.EditValue = null;
            txtPreProblemNotes.Text = "";
            txtCounsellorObservation.Text = "";
            txtMedicalSymptoms.Text = "";
            txtReactionPositive.Text = "";
            txtReactionNegative.Text = "";
            txtShareresultwith.Text = "";
            txPreClientAssessmentNotes.Text = "";
            radDecision.SelectedIndex = 0;
            txtPreInterventionNotes.Text = "";
            chkWillingtoTest.Checked = false;
            chkUnderstoodInformation.Checked = false;
            dtResultCollection.EditValue = null;
            radAccompaniedBy.SelectedIndex = 0;
            txtPostProblemNotes.Text = "";
            cboPostCounsellorObsn.EditValue = "";
            txtPostObservationNotes.Text = "";
            radPostIntervention.SelectedIndex = 0;
            txtPostInterventionNotes.Text = "";
            cboAppntReason.EditValue = "";            

            for (int i = 0; i < chklstReason.ItemCount; i++)
            {
                chklstReason.SetItemChecked(i, false);
            }

            for (int i = 0; i < chklstEducation.ItemCount; i++)
            {
                chklstEducation.SetItemChecked(i, false);
            }

            for (int i = 0; i < chklstSupportSystems.ItemCount; i++)
            {
                chklstSupportSystems.SetItemChecked(i, false);
            }
         }
        #endregion

        #region Data_ClearAll
        private void Data_ClearAll()
        {
            txtPatientId.Text = "";
            txtHIVNo.Text = "";
            txtArvNo.Text = "";
            txtCTCNo.Text = "";
            txtName.Text = "";
            txtYears.Text = "";
            txtMonths.Text = "";
            txtGender.Text = "";
            txtDate.EditValue = DateTime.Now.Date;
            txtMaritalStatus.Text = "";
            this.Data_Clear();
        }
        #endregion

        #region Data_New
        private void Data_New()
        {
            Int16 mGenerateHIVNo = 0;

            string mFunctionName = "Data_New";

            if (txtHIVNo.Text.Trim() == "" && txtHIVNo.Text.Trim().ToLower() != "<<---new--->>" && (cboResultGiven.SelectedIndex == (int)AfyaPro_Types.clsEnums.CTC_HIVTestResults.Positive))
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.CTC_HIVNoIsInvalid.ToString());
                txtHIVNo.Focus();
                return;
            }
            if (Program.IsDate(txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                txtDate.Focus();
                return;
            }
            if (cboResult1.SelectedIndex == -1)
            {
                Program.Display_Error("Invalid result selection", false);
                cboResult1.Focus();
                return;
            }

            try
            {
                DateTime? mTransDate = null;
                DateTime? mNextApptDate = null;
                DateTime? mResultGivenDate = null;
                DateTime? mResultCollectionDate = null;

                mCounsellorCode = cboCounsellor.GetColumnValue("code").ToString();
                
                if (txtHIVNo.Text.Trim().ToLower() == "<<---new--->>" && cboResultGiven.SelectedIndex == (int)AfyaPro_Types.clsEnums.CTC_HIVTestResults.Positive)
                {
                    mGenerateHIVNo = 1;
                }

                mTransDate = Convert.ToDateTime(txtDate.EditValue);

                if (Program.IsDate(txtNextApptDate.EditValue) == true)
                {
                    mNextApptDate = Convert.ToDateTime(txtNextApptDate.EditValue);
                }

                if (Program.IsDate(txtResultGivenDate.EditValue) == true)
                {
                    mResultGivenDate = Convert.ToDateTime(txtResultGivenDate.EditValue);
                }

                if(Program.IsDate(dtResultCollection.EditValue) == true)
                {
                    mResultCollectionDate = Convert.ToDateTime(dtResultCollection.EditValue);

                }

                for (int i = 0; i < chklstReason.CheckedItems.Count; i++)
                {
                    DataRow mNewRow = pDtReasonData.NewRow();
                    mNewRow["reasoncode"] = chklstReason.CheckedItems[i].ToString();
                    pDtReasonData.Rows.Add(mNewRow);
                    pDtReasonData.AcceptChanges();
                }

                for (int i = 0; i < chklstEducation.CheckedItems.Count; i++)
                {
                    DataRow mNewRow = pDtEducationData.NewRow();
                    mNewRow["hiveducationcode"] = chklstEducation.CheckedItems[i].ToString();
                    pDtEducationData.Rows.Add(mNewRow);
                    pDtEducationData.AcceptChanges();
                }
                
                for (int i = 0; i < chklstSupportSystems.CheckedItems.Count; i++)
                {
                    DataRow mNewRow = pDtSupportData.NewRow();
                    mNewRow["supportcode"] = chklstSupportSystems.CheckedItems[i].ToString();
                    pDtSupportData.Rows.Add(mNewRow);
                    pDtSupportData.AcceptChanges();
                }

                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Add_HTC(
                    pSelectedPatient.code,
                    mGenerateHIVNo,
                    txtHIVNo.Text,
                    mTransDate,
                    Convert.ToInt32(chkPregnant.Checked),
                    Convert.ToInt32(chkEverTested.Checked),
                    Convert.ToInt32(chkWithPartner.Checked),
                    cboResult1.SelectedIndex,
                    cboResult2.SelectedIndex,
                    cboResult3.SelectedIndex,
                    cboResultGiven.SelectedIndex,
                    mResultGivenDate,
                    txtComments.Text,
                    mCounsellorCode,
                    mNextApptDate,
                    pDtHIVTeferals,
                    Program.gMachineName,
                    Program.gMachineUser,
                    Program.gCurrentUser.Code,
                    pDtReasonData,
                    txtPreProblemNotes.Text,
                    txtCounsellorObservation.Text,
                    txtMedicalSymptoms.Text,
                    txtReactionPositive.Text,
                    txtReactionNegative.Text,
                    txtShareresultwith.Text,
                    txPreClientAssessmentNotes.Text,
                    radDecision.SelectedIndex,
                    txtPreInterventionNotes.Text,
                    Convert.ToInt32(chkWillingtoTest.Checked),
                    Convert.ToInt32(chkUnderstoodInformation.Checked),
                    mResultCollectionDate,
                    pDtEducationData,
                    pDtSupportData,
                    radAccompaniedBy.SelectedIndex,
                    txtPostProblemNotes.Text,
                    cboWillingToRcvResult.SelectedIndex,
                    cboPostCounsellorObsn.GetColumnValue("code").ToString(),
                    txtPostObservationNotes.Text,
                    radPostIntervention.SelectedIndex,
                    txtPostInterventionNotes.Text,
                    cboAppntReason.GetColumnValue("code").ToString());
                

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

                Program.Display_Info("Record added successfully");

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
            Int16 mGenerateHIVNo = 0;

            string mFunctionName = "Data_Edit";

            if (txtHIVNo.Text.Trim() == "" && txtHIVNo.Text.Trim().ToLower() != "<<---new--->>" && (cboResultGiven.SelectedIndex == (int)AfyaPro_Types.clsEnums.CTC_HIVTestResults.Positive))
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.CTC_HIVNoIsInvalid.ToString());
                txtHIVNo.Focus();
                return;
            }
            if (Program.IsDate(txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                txtDate.Focus();
                return;
            }
            if (cboCounsellor.ItemIndex == -1)
            {
                Program.Display_Error("Invalid counsellor selection", false);
                cboCounsellor.Focus();
                return;
            }
            if (cboResult1.SelectedIndex == -1)
            {
                Program.Display_Error("Invalid result selection", false);
                cboResult1.Focus();
                return;
            }

            try
            {
                DateTime? mTransDate = null;
                DateTime? mNextApptDate = null;
                DateTime? mResultGivenDate = null;
                DateTime? mResultCollectionDate = null;
                
                string mCounsellorCode = cboCounsellor.GetColumnValue("code").ToString();
               
                if (txtHIVNo.Text.Trim().ToLower() == "<<---new--->>" && cboResultGiven.SelectedIndex == (int)AfyaPro_Types.clsEnums.CTC_HIVTestResults.Positive)
                {
                    mGenerateHIVNo = 1;
                }

                mTransDate = Convert.ToDateTime(txtDate.EditValue);

                if (Program.IsDate(txtNextApptDate.EditValue) == true)
                {
                    mNextApptDate = Convert.ToDateTime(txtNextApptDate.EditValue);
                }

                if (Program.IsDate(txtResultGivenDate.EditValue) == true)
                {
                    mResultGivenDate = Convert.ToDateTime(txtResultGivenDate.EditValue);
                }

                if (Program.IsDate(dtResultCollection.EditValue) == true)
                {
                    mResultCollectionDate = Convert.ToDateTime(dtResultCollection.EditValue);
                }

                for (int i = 0; i < chklstReason.CheckedItems.Count; i++)
                {
                    DataRow mNewRow = pDtReasonData.NewRow();
                    mNewRow["reasoncode"] = chklstReason.CheckedItems[i].ToString();
                    pDtReasonData.Rows.Add(mNewRow);
                    pDtReasonData.AcceptChanges();
                }

                for (int i = 0; i < chklstEducation.CheckedItems.Count; i++)
                {
                    DataRow mNewRow = pDtEducationData.NewRow();
                    mNewRow["hiveducationcode"] = chklstEducation.CheckedItems[i].ToString();
                    pDtEducationData.Rows.Add(mNewRow);
                    pDtEducationData.AcceptChanges();
                }

                for (int i = 0; i < chklstSupportSystems.CheckedItems.Count; i++)
                {
                    DataRow mNewRow = pDtSupportData.NewRow();
                    mNewRow["supportcode"] = chklstSupportSystems.CheckedItems[i].ToString();
                    pDtSupportData.Rows.Add(mNewRow);
                    pDtSupportData.AcceptChanges();
                }
                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Edit_HTC(
                    Convert.ToInt32(pSelectedDataRow["autocode"]),
                    pSelectedPatient.code,
                    mGenerateHIVNo,
                    txtHIVNo.Text,
                    mTransDate,
                    Convert.ToInt32(chkPregnant.Checked),
                    Convert.ToInt32(chkEverTested.Checked),
                    Convert.ToInt32(chkWithPartner.Checked),
                    cboResult1.SelectedIndex,
                    cboResult2.SelectedIndex,
                    cboResult3.SelectedIndex,
                    cboResultGiven.SelectedIndex,
                    mResultGivenDate,
                    txtComments.Text,
                    mCounsellorCode,
                    mNextApptDate,
                    pDtHIVTeferals,
                    Program.gMachineName,
                    Program.gMachineUser,
                    Program.gCurrentUser.Code,
                    pDtReasonData,
                    txtPreProblemNotes.Text,
                    txtCounsellorObservation.Text,
                    txtMedicalSymptoms.Text,
                    txtReactionPositive.Text,
                    txtReactionNegative.Text,
                    txtShareresultwith.Text,
                    txPreClientAssessmentNotes.Text,
                    radDecision.SelectedIndex,
                    txtPreInterventionNotes.Text,
                    Convert.ToInt32(chkWillingtoTest.Checked),
                    Convert.ToInt32(chkUnderstoodInformation.Checked),
                    mResultCollectionDate,
                    pDtEducationData,
                    pDtSupportData,
                    radAccompaniedBy.SelectedIndex,
                    txtPostProblemNotes.Text,
                    cboWillingToRcvResult.SelectedIndex,
                    cboPostCounsellorObsn.GetColumnValue("code").ToString(),
                    txtPostObservationNotes.Text,
                    radPostIntervention.SelectedIndex,
                    txtPostInterventionNotes.Text,
                    cboAppntReason.GetColumnValue("code").ToString());

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

                Program.Display_Info("Record updated successfully");

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

        #region cmdSave_Click
        private void cmdSave_Click(object sender, EventArgs e)
        {
            pDtReasonData.Rows.Clear();

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

        #region cmdClear_Click
        private void cmdClear_Click(object sender, EventArgs e)
        {
            this.Data_ClearAll();
        }
        #endregion

        #region frmCTCHIVTestDetails_KeyDown
        void frmCTCHIVTestDetails_KeyDown(object sender, KeyEventArgs e)
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

        #region txtDate_EditValueChanged
        private void txtDate_EditValueChanged(object sender, EventArgs e)
        {
            //compute the next date for testing

            if (Program.IsNullDate(txtDate.EditValue) == false)
            {
                txtNextApptDate.EditValue = Convert.ToDateTime(txtDate.EditValue).AddMonths(3);
            }
        }
        #endregion

        #region cboResultGiven_SelectedIndexChanged
        private void cboResultGiven_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboResultGiven.SelectedIndex == (int)AfyaPro_Types.clsEnums.CTC_HIVTestResults.Negative)
            {
                if (Program.IsNullDate(txtDate.EditValue) == false)
                {
                    txtNextApptDate.EditValue = Convert.ToDateTime(txtDate.EditValue).AddMonths(3);
                }
            }
            else
            {
                txtNextApptDate.EditValue = null;
            }
        }
        #endregion

        #region cmdReferals_ButtonClick
        private void cmdReferals_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            frmCTCHIVTestReferals mCTCHIVTestReferals = new frmCTCHIVTestReferals(pDtHIVTeferals);
            mCTCHIVTestReferals.ShowDialog();

            cmdReferals.Text = mCTCHIVTestReferals.SelectedCodes;
        }
        #endregion

        #region cmdRequestLabTest_Click
        private void cmdRequestLabTest_Click(object sender, EventArgs e)
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

        #region View_LabTestResults
        private void cmdViewLabResult_Click(object sender, EventArgs e)
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
                    
    }
}