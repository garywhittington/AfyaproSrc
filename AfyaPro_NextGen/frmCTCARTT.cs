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
    public partial class frmCTCARTT : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_MT.clsCTCClients pMdtCTCClients;
        private AfyaPro_MT.clsReporter pMdtReporter;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        //private AfyaPro_Types.ctcBooking pBooking = new AfyaPro_Types.ctcBooking();

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private AfyaPro_Types.clsCtcClient pSelectedPatient = null;
        internal AfyaPro_Types.clsCtcClient SelectedPatient
        {
            set { pSelectedPatient = value; }
            get { return pSelectedPatient; }
        }
        private string pCurrPatientId = "";
        private string pPrevPatientId = "";
        private string pCurrHIVNo = "";
        private string pPrevHIVNo = "";
        private string pCurrCTCNo = "";
        private string pPrevCTCNo = "";
        private bool pSearchingPatient = false;

        private DataTable pDtTransferIns = new DataTable("transferins");

        #endregion

        #region frmCTCARTT
        public frmCTCARTT()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCARTT";
            this.KeyDown += new KeyEventHandler(frmCTCARTT_KeyDown);

            try
            {
                this.Icon = Program.gMdiForm.Icon;

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

                pDtTransferIns.Columns.Add("code", typeof(System.String));

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcartt_customizelayout.ToString());

                cmdNewVisit.Enabled = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcartt_newvisit.ToString());

                cmdVisits.Enabled = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcartt_history.ToString());

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

        #region frmCTCARTT_Load
        private void frmCTCARTT_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCARTT_Load";

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

        #region frmCTCARTT_Shown
        private void frmCTCARTT_Shown(object sender, EventArgs e)
        {
            this.Data_Clear();

            if (pSelectedPatient != null)
            {
                this.Data_Display(pSelectedPatient);
            }

            cboSearchBy.SelectedIndex = 0;
        }
        #endregion

        #region frmCTCARTT_FormClosing
        private void frmCTCARTT_FormClosing(object sender, FormClosingEventArgs e)
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
                #region referred from

                DataTable mDtReferedFrom = pMdtReporter.View_LookupData("ctc_referedfrom", "code,description", "", "", Program.gLanguageName, "grdCTCReferedFrom");
                cboReferedFrom.Properties.DataSource = mDtReferedFrom;
                cboReferedFrom.Properties.DisplayMember = "description";
                cboReferedFrom.Properties.ValueMember = "code";
                cboReferedFrom.Properties.BestFit();

                #endregion

                #region prio arv exposure

                DataTable mDtPriorArvExposure = pMdtReporter.View_LookupData("ctc_priorarvexposure", "code,description", "", "", Program.gLanguageName, "grdCTCPriorARVExposure");
                cboPriorARVExposure.Properties.DataSource = mDtPriorArvExposure;
                cboPriorARVExposure.Properties.DisplayMember = "description";
                cboPriorARVExposure.Properties.ValueMember = "code";
                cboPriorARVExposure.Properties.BestFit();

                #endregion

                #region functionalstatus

                DataTable mDtFunctionalStatus = pMdtReporter.View_LookupData("ctc_functionalstatus", "code,description", "", "", Program.gLanguageName, "grdCTCFunctionalStatus");
                cboStartFunctionalStatus.Properties.DataSource = mDtFunctionalStatus;
                cboStartFunctionalStatus.Properties.DisplayMember = "description";
                cboStartFunctionalStatus.Properties.ValueMember = "code";
                cboStartFunctionalStatus.Properties.BestFit();

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
        private void Data_Display(AfyaPro_Types.clsCtcClient mPatient)
        {
            string mFunctionName = "Data_Display";

            try
            {
                this.pSearchingPatient = false;

                this.Data_Clear();

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

                pCurrPatientId = txtPatientId.Text;
                pPrevPatientId = pCurrPatientId;

                #endregion

                #region ctc_clients

                DataTable mDtClients = pMdtCTCClients.View_CTCClients("patientcode='" + mPatient.code + "'", "");

                if (mDtClients.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtClients.Rows[0];

                    txtARVNo.Text = mDataRow["arvno"].ToString();
                    txtCTCNo.Text = mDataRow["ctcno"].ToString();

                    #region logic new/update

                    //arv#
                    if (mDataRow["arvno"].ToString().Trim() == "")
                    {
                        Int16 mGenerateCode = pMdtAutoCodes.Auto_Generate_Code(
                            Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcarvnumber));
                        if (mGenerateCode == -1)
                        {
                            Program.Display_Server_Error("");
                            return;
                        }

                        txtARVNo.Text = "";
                        if (mGenerateCode == 1)
                        {
                            txtARVNo.Text = "<<---New--->>";
                        }
                    }
                    else
                    {
                        txtARVNo.Text = mDataRow["arvno"].ToString();
                    }

                    //ctc#
                    if (mDataRow["ctcno"].ToString().Trim() == "")
                    {
                        Int16 mGenerateCode = pMdtAutoCodes.Auto_Generate_Code(
                            Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcnumbers));
                        if (mGenerateCode == -1)
                        {
                            Program.Display_Server_Error("");
                            return;
                        }

                        txtCTCNo.Text = "";
                        if (mGenerateCode == 1)
                        {
                            txtCTCNo.Text = "<<---New--->>";
                        }
                    }
                    else
                    {
                        txtCTCNo.Text = mDataRow["ctcno"].ToString();
                    }

                    #endregion
                }

                #endregion

                #region ctc_artt

                DataTable mDtPreART = pMdtReporter.View_LookupData("view_ctc_artt", "*", "patientcode='" + mPatient.code.Trim() + "'", "", "", "");

                if (mDtPreART.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtPreART.Rows[0];

                    chkOnARV.Checked = Convert.ToBoolean(mDataRow["onarv"]);
                    cboReferedFrom.ItemIndex = Program.Get_LookupItemIndex(cboReferedFrom, "code", mDataRow["referredfromcode"].ToString());
                    txtReferedFromOther.Text = mDataRow["referredfromother"].ToString();
                    cboPriorARVExposure.ItemIndex = Program.Get_LookupItemIndex(cboPriorARVExposure, "code", mDataRow["priorarvexposurecode"].ToString());
                    txtDateConfirmedHiv.Text = mDataRow["confirmedhivdate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["confirmedhivdate"]).ToString("d");
                    txtDateEnrolled.Text = mDataRow["enrolledincaredate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["enrolledincaredate"]).ToString("d");
                    txtDateMedEligible.Text = mDataRow["medeligibledate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["medeligibledate"]).ToString("d");
                    txtDateEligibleAndReady.Text = mDataRow["eligibleandreadydate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["eligibleandreadydate"]).ToString("d");
                    txtDateStartedART.Text = mDataRow["startedartdate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["startedartdate"]).ToString("d");
                    cboWhyEligibleClinicalStage.Text = mDataRow["whyeligibleclinicalstage"].ToString();
                    txtWhyEligibleCD4Count.Text = mDataRow["whyeligiblecd4count"].ToString();
                    txtWhyEligibleCD4CountPercent.Text = mDataRow["whyeligiblecd4countpercent"].ToString();
                    chkJoinedSupport.Checked = mDataRow["joinedsupport"] != DBNull.Value ? Convert.ToBoolean(mDataRow["joinedsupport"]) : false;
                    txtSupporterName.Text = mDataRow["supportername"].ToString();
                    txtSupporterAddress.Text = mDataRow["supporteraddress"].ToString();
                    txtSupporterTelephone.Text = mDataRow["supportertelephone"].ToString();
                    txtSupporterCommunity.Text = mDataRow["supportercommunity"].ToString();
                    txtTBRegNo.Text = mDataRow["tbregno"].ToString();
                    txtHBCNo.Text = mDataRow["hbcno"].ToString();
                    cboStartClinicalStage.Text = mDataRow["startclinicalstage"].ToString();
                    txtStartCD4Count.Text = mDataRow["startcd4count"].ToString();
                    txtStartCD4CountPercent.Text = mDataRow["startcd4countpercent"].ToString();
                    cboStartFunctionalStatus.ItemIndex = Program.Get_LookupItemIndex(cboStartFunctionalStatus, "code", mDataRow["startfunctionalstatuscode"].ToString());
                    txtStartWeight.Text = mDataRow["startweight"].ToString();

                    //transferin
                    pDtTransferIns = pMdtReporter.View_LookupData("ctc_patienttransferin", "transferincode as code",
                        "booking='" + mDataRow["booking"] + "' and patientcode='" + txtPatientId.Text.Trim() + "'", "", "", "");
                    string mSideEffects = "";
                    foreach (DataRow mCurrDataRow in pDtTransferIns.Rows)
                    {
                        if (mSideEffects.Trim() == "")
                        {
                            mSideEffects = mCurrDataRow["code"].ToString().Trim();
                        }
                        else
                        {
                            mSideEffects = mSideEffects + "; " + mCurrDataRow["code"].ToString().Trim();
                        }
                    }
                    cmdTransferIn.Text = mSideEffects;

                    cmdVisits.Enabled = true;
                }
                else
                {
                    cmdVisits.Enabled = false;
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
            txtARVNo.Text = "";
            txtCTCNo.Text = "";
            chkOnARV.Checked = false;
            cboReferedFrom.EditValue = null;
            txtReferedFromOther.Text = "";
            cboPriorARVExposure.EditValue = null;
            txtDateConfirmedHiv.EditValue = null;
            txtDateEnrolled.EditValue = null;
            txtDateMedEligible.EditValue = null;
            txtDateEligibleAndReady.EditValue = null;
            txtDateStartedART.EditValue = null;
            txtWhyEligibleCD4Count.Text = "";
            txtWhyEligibleCD4CountPercent.Text = "";
            cboWhyEligibleClinicalStage.Text = "";
            cboStartClinicalStage.Text = "";
            txtStartCD4Count.Text = "";
            txtStartCD4CountPercent.Text = "";
            txtStartWeight.Text = "";
            cboStartFunctionalStatus.EditValue = null;
            txtTBRegNo.Text = "";
            txtHBCNo.Text = "";
            chkJoinedSupport.Checked = false;
            txtSupporterName.Text = "";
            txtSupporterAddress.Text = "";
            txtSupporterTelephone.Text = "";
            txtSupporterCommunity.Text = "";
            cmdTransferIn.Text = "";

            pPrevPatientId = "";
            pCurrPatientId = pPrevPatientId;
        }
        #endregion

        #region Ok
        private void Ok()
        {
            Int16 mGenerateARVNo = 0;
            Int16 mGenerateCTCNo = 0;

            string mFunctionName = "Ok";

            if (chkOnARV.Checked == true)
            {
                if (txtARVNo.Text.Trim() == "" && txtARVNo.Text.Trim().ToLower() != "<<---new--->>")
                {
                    Program.Display_Error("Invalid ARV #");
                    txtARVNo.Focus();
                    return;
                }
            }

            if (txtCTCNo.Text.Trim() == "" && txtCTCNo.Text.Trim().ToLower() != "<<---new--->>")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.CTC_CTCNoIsInvalid.ToString());
                txtCTCNo.Focus();
                return;
            }

            try
            {
                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                string mReferedFromCode = "";
                string mPriorArvExposureCode = "";
                DateTime? mConfirmedHivDate = null;
                DateTime? mEnroledInCareDate = null;
                DateTime? mMedEligibleDate = null;
                DateTime? mEligibleAndReadyDate = null;
                DateTime? mStartedArtDate = null;
                int mWhyEligibleClinicalStage = 0;
                double mWhyEligibleCD4 = 0;
                double mWhyEligibleCD4Percent = 0;
                int mStartClinicalStage = 0;
                decimal mStartCD4Count = 0;
                decimal mStartCD4CountPercent = 0;
                string mStartFunctionalStatusCode = "";
                decimal mStartWeight = 0;

                if (txtARVNo.Text.Trim().ToLower() == "<<---new--->>")
                {
                    mGenerateARVNo = 1;
                }

                if (txtCTCNo.Text.Trim().ToLower() == "<<---new--->>")
                {
                    mGenerateCTCNo = 1;
                }

                if (cboReferedFrom.ItemIndex != -1)
                {
                    mReferedFromCode = cboReferedFrom.GetColumnValue("code").ToString();
                }

                if (cboPriorARVExposure.ItemIndex != -1)
                {
                    mPriorArvExposureCode = cboPriorARVExposure.GetColumnValue("code").ToString();
                }

                if (Program.IsDate(txtDateConfirmedHiv.EditValue) == true)
                {
                    mConfirmedHivDate = Convert.ToDateTime(txtDateConfirmedHiv.EditValue);
                }

                if (Program.IsDate(txtDateEnrolled.EditValue) == true)
                {
                    mEnroledInCareDate = Convert.ToDateTime(txtDateEnrolled.EditValue);
                }

                if (Program.IsDate(txtDateMedEligible.EditValue) == true)
                {
                    mMedEligibleDate = Convert.ToDateTime(txtDateMedEligible.EditValue);
                }

                if (Program.IsDate(txtDateEligibleAndReady.EditValue) == true)
                {
                    mEligibleAndReadyDate = Convert.ToDateTime(txtDateEligibleAndReady.EditValue);
                }

                if (Program.IsDate(txtDateStartedART.EditValue) == true)
                {
                    mStartedArtDate = Convert.ToDateTime(txtDateStartedART.EditValue);
                }

                if (cboWhyEligibleClinicalStage.SelectedIndex != -1)
                {
                    mWhyEligibleClinicalStage = Convert.ToInt16(cboWhyEligibleClinicalStage.Text);
                }

                if (Program.IsMoney(txtWhyEligibleCD4Count.Text) == true)
                {
                    mWhyEligibleCD4 = Convert.ToDouble(txtWhyEligibleCD4Count.Text);
                }

                if (Program.IsMoney(txtWhyEligibleCD4CountPercent.Text) == true)
                {
                    mWhyEligibleCD4Percent = Convert.ToDouble(txtWhyEligibleCD4CountPercent.Text);
                }

                if (cboStartClinicalStage.SelectedIndex != -1)
                {
                    mStartClinicalStage = Convert.ToInt32(cboStartClinicalStage.Text);
                }

                if (Program.IsMoney(txtStartCD4Count.Text) == true)
                {
                    mStartCD4Count = Convert.ToDecimal(txtStartCD4Count.Text);
                }

                if (Program.IsMoney(txtStartCD4CountPercent.Text) == true)
                {
                    mStartCD4CountPercent = Convert.ToDecimal(txtStartCD4CountPercent.Text);
                }

                if (cboStartFunctionalStatus.ItemIndex != -1)
                {
                    mStartFunctionalStatusCode = cboStartFunctionalStatus.GetColumnValue("code").ToString();
                }

                if ((Program.IsMoney(txtStartWeight.Text) == true))
                {
                    mStartWeight = Convert.ToDecimal(txtStartWeight.Text);
                }

                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Enroll_ARTT(
                    txtPatientId.Text,
                    mGenerateARVNo,
                    txtARVNo.Text,
                    mGenerateCTCNo,
                    txtCTCNo.Text,
                    txtTBRegNo.Text,
                    txtHBCNo.Text,
                    mTransDate,
                    Convert.ToInt32(chkOnARV.Checked),
                    mReferedFromCode,
                    txtReferedFromOther.Text,
                    pDtTransferIns,
                    mPriorArvExposureCode,
                    mConfirmedHivDate,
                    mEnroledInCareDate,
                    mMedEligibleDate,
                    mEligibleAndReadyDate,
                    mStartedArtDate,
                    mWhyEligibleClinicalStage,
                    mWhyEligibleCD4,
                    mWhyEligibleCD4Percent,
                    mStartClinicalStage,
                    mStartCD4Count,
                    mStartCD4CountPercent,
                    mStartFunctionalStatusCode,
                    mStartWeight,
                    Convert.ToInt32(chkJoinedSupport.Checked),
                    txtSupporterName.Text,
                    txtSupporterAddress.Text,
                    txtSupporterTelephone.Text,
                    txtSupporterCommunity.Text,
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
            this.Ok();
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region frmCTCARTT_KeyDown
        void frmCTCARTT_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Program.KeyCode_Ok:
                    {
                        this.Ok();
                    }
                    break;
            }
        }
        #endregion

        #region cboReferedFrom_EditValueChanged
        private void cboReferedFrom_EditValueChanged(object sender, EventArgs e)
        {
            if (cboReferedFrom.ItemIndex == -1)
            {
                return;
            }

            if (cboReferedFrom.GetColumnValue("code").ToString().Trim().ToLower() == "oth")
            {
                txtReferedFromOther.Properties.ReadOnly = false;
                txtReferedFromOther.Focus();
            }
            else
            {
                txtReferedFromOther.Text = "";
                txtReferedFromOther.Properties.ReadOnly = true;
            }
        }
        #endregion

        #region Search_Patient
        private AfyaPro_Types.clsCtcClient Search_Patient(string mFieldName, string mFieldValue)
        {
            string mFunctionName = "Search_Patient";

            try
            {
                if (mFieldValue.Trim() == "")
                {
                    return null;
                }

                pSelectedPatient = pMdtCTCClients.Get_Client(mFieldName, mFieldValue);
                return pSelectedPatient;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
        }
        #endregion

        #region cmdSearch_Click
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            pSearchingPatient = true;

            frmSearchCTCClient mSearchPatient = new frmSearchCTCClient(txtPatientId);
            mSearchPatient.ShowDialog();

            pSearchingPatient = false;
        }
        #endregion

        #region search by patientcode

        #region txtPatientId_EditValueChanged
        private void txtPatientId_EditValueChanged(object sender, EventArgs e)
        {
            if (pSearchingPatient == true)
            {
                this.pSelectedPatient = this.Search_Patient("p.code", txtPatientId.Text);
                this.Data_Display(pSelectedPatient);
            }
        }
        #endregion

        #endregion

        #region quick search of client

        #region cmdFindWhat_Click
        private void cmdFindWhat_Click(object sender, EventArgs e)
        {
            string mFieldName = "";
            switch (cboSearchBy.SelectedIndex)
            {
                case 0: mFieldName = "p.code"; break;
                case 1: mFieldName = "c.hivtestno"; break;
                case 2: mFieldName = "c.hivno"; break;
                case 3: mFieldName = "c.arvno"; break;
                case 4: mFieldName = "c.ctcno"; break;
                default: Program.Display_Error("Please specified the field to search in 'Look In' and try again"); break;
            }

            this.pSelectedPatient = this.Search_Patient(mFieldName, txtSearchText.Text);

            if (pSelectedPatient != null)
            {
                if (pSelectedPatient.Exist == false)
                {
                    Program.Display_Info("No records found");
                }
            }
            this.Data_Display(pSelectedPatient);
        }
        #endregion

        #region txtSearchText_Enter
        private void txtSearchText_Enter(object sender, EventArgs e)
        {
            txtSearchText.SelectAll();
        }
        #endregion

        #region txtSearchText_KeyDown
        private void txtSearchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string mFieldName = "";
                switch (cboSearchBy.SelectedIndex)
                {
                    case 0: mFieldName = "p.code"; break;
                    case 1: mFieldName = "c.hivtestno"; break;
                    case 2: mFieldName = "c.hivno"; break;
                    case 3: mFieldName = "c.arvno"; break;
                    case 4: mFieldName = "c.ctcno"; break;
                    default: Program.Display_Error("Please specified the field to search in 'Look In' and try again"); break;
                }

                this.pSelectedPatient = this.Search_Patient(mFieldName, txtSearchText.Text);

                if (pSelectedPatient != null)
                {
                    if (pSelectedPatient.Exist == false)
                    {
                        Program.Display_Info("No records found");
                    }
                }

                this.Data_Display(pSelectedPatient);
            }
        }
        #endregion

        #endregion

        #region cmdVisits_Click
        private void cmdVisits_Click(object sender, EventArgs e)
        {
            if (pSelectedPatient == null)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.CTC_CTCNoIsInvalid.ToString());
                txtCTCNo.Focus();
                return;
            }

            if (Program.GrantDeny_FunctionAccess(AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcartt_history.ToString()) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString());
                return;
            }

            frmCTCARTTVisits mCTCARTTVisits = new frmCTCARTTVisits();
            mCTCARTTVisits.SelectedPatient = pSelectedPatient;
            mCTCARTTVisits.ShowDialog();
        }
        #endregion

        #region cmdNewVisit_Click
        private void cmdNewVisit_Click(object sender, EventArgs e)
        {
            if (pSelectedPatient == null)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.CTC_CTCNoIsInvalid.ToString());
                txtCTCNo.Focus();
                return;
            }

            if (Program.GrantDeny_FunctionAccess(AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcartt_newvisit.ToString()) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString());
                return;
            }

            frmCTCARTTVisitDetails mCTCARTTVisitDetails = new frmCTCARTTVisitDetails();
            mCTCARTTVisitDetails.SelectedPatient = pSelectedPatient;
            mCTCARTTVisitDetails.ShowDialog();

            if (mCTCARTTVisitDetails.RecordSaved == true)
            {
                txtPatientId.Text = "";
                this.Data_Clear();
                txtPatientId.Focus();
            }
        }
        #endregion

        #region cmdTransferIn_ButtonClick
        private void cmdTransferIn_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            frmCTCTransferIns mCTCTransferIns = new frmCTCTransferIns(pDtTransferIns);
            mCTCTransferIns.ShowDialog();

            cmdTransferIn.Text = mCTCTransferIns.SelectedCodes;
        }
        #endregion
    }
}