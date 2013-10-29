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
    public partial class frmCTCARTVisitDetails : DevExpress.XtraEditors.XtraForm
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

        #region frmCTCARTVisitDetails
        public frmCTCARTVisitDetails(DataRow mSelectedDataRow = null)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCARTVisitDetails";
            this.KeyDown += new KeyEventHandler(frmCTCARTVisitDetails_KeyDown);

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

        #region frmCTCARTVisitDetails_Load
        private void frmCTCARTVisitDetails_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCARTVisitDetails_Load";

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

        #region frmCTCARTVisitDetails_Shown
        private void frmCTCARTVisitDetails_Shown(object sender, EventArgs e)
        {
            txtDate.EditValue = DateTime.Now.Date;

            if (pSelectedPatient != null)
            {
                this.Display_Patient(pSelectedPatient);
            }
        }
        #endregion

        #region frmCTCARTVisitDetails_FormClosing
        private void frmCTCARTVisitDetails_FormClosing(object sender, FormClosingEventArgs e)
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
                #region artoutcome

                DataTable mDtARTOutcomes = pMdtReporter.View_LookupData("ctc_artoutcomes", "code,description", "active=1", "", Program.gLanguageName, "grdCTCARTOutcomes");
                cboAdverseOutcome.Properties.DataSource = mDtARTOutcomes;
                cboAdverseOutcome.Properties.DisplayMember = "description";
                cboAdverseOutcome.Properties.ValueMember = "code";

                cboAdverseOutcome.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboAdverseOutcome.Properties.BestFit();

                #endregion

                #region arvregimen

                DataTable mDtARVRegimens = pMdtReporter.View_LookupData("ctc_arvcombregimens", "category,code,description", "active=1", "category,code", Program.gLanguageName, "grdCTCARVCombRegimens");
                cboARTRegimen.Properties.DataSource = mDtARVRegimens;
                cboARTRegimen.Properties.DisplayMember = "description";
                cboARTRegimen.Properties.ValueMember = "code";

                cboARTRegimen.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboARTRegimen.Properties.BestFit();

                #endregion

                #region TB Status

                DataTable mDtTBStatus = pMdtReporter.View_LookupData("ctc_tbstatus", "code,description", "active=1", "", Program.gLanguageName, "grdCTCTBStatus");
                cboTBStatus.Properties.DataSource = mDtTBStatus;
                cboTBStatus.Properties.DisplayMember = "description";
                cboTBStatus.Properties.ValueMember = "code";

                cboTBStatus.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboTBStatus.Properties.BestFit();

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
                cboAdverseOutcome.ItemIndex = Program.Get_LookupItemIndex(cboAdverseOutcome, "code", pSelectedDataRow["outcomecode"].ToString());
                txtOutcomeDate.EditValue = null;
                if (pSelectedDataRow["outcomedate"] != DBNull.Value)
                {
                    txtOutcomeDate.EditValue = Convert.ToDateTime(pSelectedDataRow["outcomedate"]);
                }
                cboARTRegimen.ItemIndex = Program.Get_LookupItemIndex(cboARTRegimen, "code", pSelectedDataRow["artregimencode"].ToString());
                cboTBStatus.ItemIndex = Program.Get_LookupItemIndex(cboTBStatus, "code", pSelectedDataRow["tbstatuscode"].ToString());
                txtPillCount.Text = pSelectedDataRow["pillcount"] == DBNull.Value ? "" : pSelectedDataRow["pillcount"].ToString();
                txtDosesMissed.Text = pSelectedDataRow["dosesmissed"] == DBNull.Value ? "" : pSelectedDataRow["dosesmissed"].ToString();
                txtARVTablets.Text = pSelectedDataRow["arvtablets"] == DBNull.Value ? "" : pSelectedDataRow["arvtablets"].ToString();
                cboARVTo.SelectedIndex = Convert.ToInt32(pSelectedDataRow["arvto"]);
                txtCPT.Text = pSelectedDataRow["cpttablets"] == DBNull.Value ? "" : pSelectedDataRow["cpttablets"].ToString();
                chkDepoGiven.Checked = Convert.ToBoolean(pSelectedDataRow["depogiven"]);
                txtCondoms.Text = pSelectedDataRow["condoms"] == DBNull.Value ? "" : pSelectedDataRow["condoms"].ToString();
                txtViralLoadDate.EditValue = null;
                if (pSelectedDataRow["viralloaddate"] != DBNull.Value)
                {
                    txtViralLoadDate.EditValue = Convert.ToDateTime(pSelectedDataRow["viralloaddate"]);
                }
                txtViralLoadResult.Text = pSelectedDataRow["viralloadresult"] == DBNull.Value ? "" : pSelectedDataRow["viralloadresult"].ToString();
                txtNextVisitDate.EditValue = null;
                if (pSelectedDataRow["nextvisitdate"] != DBNull.Value)
                {
                    txtNextVisitDate.EditValue = Convert.ToDateTime(pSelectedDataRow["nextvisitdate"]);
                }
                cboClinician.ItemIndex = Program.Get_LookupItemIndex(cboClinician, "code", pSelectedDataRow["cliniciancode"].ToString());
                txtNotes.Text = pSelectedDataRow["notes"] == DBNull.Value ? "" : pSelectedDataRow["notes"].ToString();

                //months on ART
                DataTable mDtVisits = pMdtReporter.View_LookupData("view_ctc_firstartvisits", "*",
                    "patientcode='" + txtPatientId.Text.Trim() + "'", "", "", "");
                if (mDtVisits.Rows.Count > 0)
                {
                    DateTime mFirstVisitDate = Convert.ToDateTime(mDtVisits.Rows[0]["transdate"]);
                    DateTime mCurrentDate = DateTime.Now.Date;
                    if (Program.IsNullDate(txtDate.EditValue) == false)
                    {
                        mCurrentDate = Convert.ToDateTime(txtDate.EditValue);
                    }

                    if (mCurrentDate > mFirstVisitDate)
                    {
                        TimeSpan mTimeSpan = mCurrentDate.Subtract(mFirstVisitDate);

                        int mDays = mTimeSpan.Days;
                        int mMonths = (int)mDays / 30;
                        int mWeeks = (int)(mDays % 30) / 7;

                        if (mMonths > 0)
                        {
                            txtARTMonths.Text = mMonths + " Months";
                            if (mWeeks > 0)
                            {
                                txtARTMonths.Text = txtARTMonths.Text + ", " + mWeeks + " Weeks";
                            }
                        }
                        else
                        {
                            txtARTMonths.Text = mWeeks + " Weeks";
                        }
                    }
                }

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

                string mOutcomeCode = "";
                DateTime? mOutcomeDate = null;
                string mARVRegimenCode = "";
                string mTBStatusCode = "";
                string mClinicianCode = "";
                double mPillCount = 0;
                double mDosesMissed = 0;
                double mARVTablets = 0;
                double mCPT = 0;
                int mCondoms = 0;
                DateTime? mViralLoadDate = null;
                DateTime? mNextVisitDate = null;

                if (cboAdverseOutcome.ItemIndex >= 0)
                {
                    mOutcomeCode = cboAdverseOutcome.GetColumnValue("code").ToString();
                }

                if (Program.IsDate(txtOutcomeDate.EditValue) == true)
                {
                    mOutcomeDate = Convert.ToDateTime(txtOutcomeDate.EditValue);
                }

                if (cboARTRegimen.ItemIndex >= 0)
                {
                    mARVRegimenCode = cboARTRegimen.GetColumnValue("code").ToString();
                }

                if (cboTBStatus.ItemIndex >= 0)
                {
                    mTBStatusCode = cboTBStatus.GetColumnValue("code").ToString();
                }

                if (Program.IsMoney(txtPillCount.Text) == true)
                {
                    mPillCount = Convert.ToDouble(txtPillCount.Text);
                }

                if (Program.IsMoney(txtDosesMissed.Text) == true)
                {
                    mDosesMissed = Convert.ToDouble(txtDosesMissed.Text);
                }

                if (Program.IsMoney(txtARVTablets.Text) == true)
                {
                    mARVTablets = Convert.ToDouble(txtARVTablets.Text);
                }

                if (Program.IsMoney(txtCPT.Text) == true)
                {
                    mCPT = Convert.ToDouble(txtCPT.Text);
                }

                if (Program.IsNumeric(txtCondoms.Text) == true)
                {
                    mCondoms = Convert.ToInt32(txtCondoms.Text);
                }

                if (Program.IsDate(txtViralLoadDate.EditValue) == true)
                {
                    mViralLoadDate = Convert.ToDateTime(txtViralLoadDate.EditValue);
                }

                if (Program.IsDate(txtNextVisitDate.EditValue) == true)
                {
                    mNextVisitDate = Convert.ToDateTime(txtNextVisitDate.EditValue);
                }

                if (cboClinician.ItemIndex >= 0)
                {
                    mClinicianCode = cboClinician.GetColumnValue("code").ToString();
                }

                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Add_ART(
                    txtPatientId.Text,
                    mTransDate,
                    mOutcomeCode,
                    mOutcomeDate,
                    mARVRegimenCode,
                    mTBStatusCode,
                    mPillCount,
                    mDosesMissed,
                    mARVTablets,
                    cboARVTo.SelectedIndex,
                    mCPT,
                    Convert.ToInt32(chkDepoGiven.Checked),
                    mCondoms,
                    mViralLoadDate,
                    txtViralLoadResult.Text,
                    mNextVisitDate,
                    mClinicianCode,
                    txtNotes.Text,
                    pDtSideEffects,
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

                string mOutcomeCode = "";
                DateTime? mOutcomeDate = null;
                string mARVRegimenCode = "";
                string mTBStatusCode = "";
                string mClinicianCode = "";
                double mPillCount = 0;
                double mDosesMissed = 0;
                double mARVTablets = 0;
                double mCPT = 0;
                int mCondoms = 0;
                DateTime? mViralLoadDate = null;
                DateTime? mNextVisitDate = null;

                if (cboAdverseOutcome.ItemIndex >= 0)
                {
                    mOutcomeCode = cboAdverseOutcome.GetColumnValue("code").ToString();
                }

                if (Program.IsDate(txtOutcomeDate.EditValue) == true)
                {
                    mOutcomeDate = Convert.ToDateTime(txtOutcomeDate.EditValue);
                }

                if (cboARTRegimen.ItemIndex >= 0)
                {
                    mARVRegimenCode = cboARTRegimen.GetColumnValue("code").ToString();
                }

                if (cboTBStatus.ItemIndex >= 0)
                {
                    mTBStatusCode = cboTBStatus.GetColumnValue("code").ToString();
                }

                if (Program.IsMoney(txtPillCount.Text) == true)
                {
                    mPillCount = Convert.ToDouble(txtPillCount.Text);
                }

                if (Program.IsMoney(txtDosesMissed.Text) == true)
                {
                    mDosesMissed = Convert.ToDouble(txtDosesMissed.Text);
                }

                if (Program.IsMoney(txtARVTablets.Text) == true)
                {
                    mARVTablets = Convert.ToDouble(txtARVTablets.Text);
                }

                if (Program.IsMoney(txtCPT.Text) == true)
                {
                    mCPT = Convert.ToDouble(txtCPT.Text);
                }

                if (Program.IsNumeric(txtCondoms.Text) == true)
                {
                    mCondoms = Convert.ToInt32(txtCondoms.Text);
                }

                if (Program.IsDate(txtViralLoadDate.EditValue) == true)
                {
                    mViralLoadDate = Convert.ToDateTime(txtViralLoadDate.EditValue);
                }

                if (Program.IsDate(txtNextVisitDate.EditValue) == true)
                {
                    mNextVisitDate = Convert.ToDateTime(txtNextVisitDate.EditValue);
                }

                if (cboClinician.ItemIndex >= 0)
                {
                    mClinicianCode = cboClinician.GetColumnValue("code").ToString();
                }

                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Edit_ART(
                    Convert.ToInt32(pSelectedDataRow["autocode"]),
                    mTransDate,
                    mOutcomeCode,
                    mOutcomeDate,
                    mARVRegimenCode,
                    mTBStatusCode,
                    mPillCount,
                    mDosesMissed,
                    mARVTablets,
                    cboARVTo.SelectedIndex,
                    mCPT,
                    Convert.ToInt32(chkDepoGiven.Checked),
                    mCondoms,
                    mViralLoadDate,
                    txtViralLoadResult.Text,
                    mNextVisitDate,
                    mClinicianCode,
                    txtNotes.Text,
                    pDtSideEffects,
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

        #region frmCTCARTVisitDetails_KeyDown
        void frmCTCARTVisitDetails_KeyDown(object sender, KeyEventArgs e)
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
    }
}