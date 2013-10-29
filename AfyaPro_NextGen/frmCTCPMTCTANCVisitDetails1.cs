﻿/*
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
    public partial class frmCTCPMTCTANCVisitDetails1 : DevExpress.XtraEditors.XtraForm
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
        private DataTable pDtReferrals = new DataTable("referrals");

        private bool pRecordSaved = false;
        internal bool RecordSaved
        {
            set { pRecordSaved = value; }
            get { return pRecordSaved; }
        }

        #endregion

        #region frmCTCPMTCTANCVisitDetails1
        public frmCTCPMTCTANCVisitDetails1(DataRow mSelectedDataRow = null)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCPMTCTANCVisitDetails1";
            this.KeyDown += new KeyEventHandler(frmCTCPMTCTANCVisitDetails1_KeyDown);

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
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpmtctanc_customizelayout.ToString());

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

        #region frmCTCPMTCTANCVisitDetails1_Load
        private void frmCTCPMTCTANCVisitDetails1_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCPMTCTANCVisitDetails1_Load";

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

        #region frmCTCPMTCTANCVisitDetails1_Shown
        private void frmCTCPMTCTANCVisitDetails1_Shown(object sender, EventArgs e)
        {
            txtDate.EditValue = DateTime.Now.Date;

            if (pSelectedPatient != null)
            {
                this.Display_Patient(pSelectedPatient);
            }
        }
        #endregion

        #region frmCTCPMTCTANCVisitDetails1_FormClosing
        private void frmCTCPMTCTANCVisitDetails1_FormClosing(object sender, FormClosingEventArgs e)
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
                #region arvregimen

                DataTable mDtARVRegimens = pMdtReporter.View_LookupData("ctc_pmtctcomb", "code,description", "active=1", "autocode", Program.gLanguageName, "grdCTCARVCombRegimens");
                cboARTRegimen.Properties.DataSource = mDtARVRegimens;
                cboARTRegimen.Properties.DisplayMember = "description";
                cboARTRegimen.Properties.ValueMember = "code";

                cboARTRegimen.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboARTRegimen.Properties.BestFit();

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

                pDtReferrals.Rows.Clear();

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
                txtGestationAge.Text = pSelectedDataRow["gestationage"] == DBNull.Value ? "" : pSelectedDataRow["gestationage"].ToString();
                cboInfantFeedChoice.SelectedIndex = Convert.ToInt32(pSelectedDataRow["infantfeedchoice"]);
                cboARTRegimen.ItemIndex = Program.Get_LookupItemIndex(cboARTRegimen, "code", pSelectedDataRow["artregimencode"].ToString());
                cboAdherenceAndDisclosure.SelectedIndex = Convert.ToInt32(pSelectedDataRow["adherenceanddisclosure"]);
                txtTBRxStartDate.EditValue = null;
                if (pSelectedDataRow["tbrxstartdate"] != DBNull.Value)
                {
                    txtTBRxStartDate.EditValue = Convert.ToDateTime(pSelectedDataRow["tbrxstartdate"]);
                }
                txtTBRxStopDate.EditValue = null;
                if (pSelectedDataRow["tbrxstopdate"] != DBNull.Value)
                {
                    txtTBRxStopDate.EditValue = Convert.ToDateTime(pSelectedDataRow["tbrxstopdate"]);
                }
                txtCTXStartDate.EditValue = null;
                if (pSelectedDataRow["ctxstartdate"] != DBNull.Value)
                {
                    txtCTXStartDate.EditValue = Convert.ToDateTime(pSelectedDataRow["ctxstartdate"]);
                }
                txtCTXStopDate.EditValue = null;
                if (pSelectedDataRow["ctxstopdate"] != DBNull.Value)
                {
                    txtCTXStopDate.EditValue = Convert.ToDateTime(pSelectedDataRow["ctxstopdate"]);
                }
                cboClinicalStage.Text = pSelectedDataRow["clinicalstage"] == DBNull.Value ? "" : pSelectedDataRow["clinicalstage"].ToString();
                txtClinicalStageDate.EditValue = null;
                if (pSelectedDataRow["clinicalstagedate"] != DBNull.Value)
                {
                    txtClinicalStageDate.EditValue = Convert.ToDateTime(pSelectedDataRow["clinicalstagedate"]);
                }
                cboWhyEligible.SelectedIndex = Convert.ToInt32(pSelectedDataRow["whyeligible"]);
                txtCD4Counts.Text = pSelectedDataRow["cd4count"] == DBNull.Value ? "" : pSelectedDataRow["cd4count"].ToString();
                txtRemarks.Text = pSelectedDataRow["remarks"] == DBNull.Value ? "" : pSelectedDataRow["remarks"].ToString();

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

                DateTime? mTBRxStartDate = null;
                DateTime? mTBRxStopDate = null;
                DateTime? mCTXStartDate = null;
                DateTime? mCTXStopDate = null;
                DateTime? mClinicalStageDate = null;
                string mARVRegimenCode = "";
                double mGestationAge = 0;
                double mCD4Count = 0;

                if (Program.IsDate(txtTBRxStartDate.EditValue) == true)
                {
                    mTBRxStartDate = Convert.ToDateTime(txtTBRxStartDate.EditValue);
                }

                if (Program.IsDate(txtTBRxStopDate.EditValue) == true)
                {
                    mTBRxStopDate = Convert.ToDateTime(txtTBRxStopDate.EditValue);
                }

                if (Program.IsDate(txtCTXStartDate.EditValue) == true)
                {
                    mCTXStartDate = Convert.ToDateTime(txtCTXStartDate.EditValue);
                }

                if (Program.IsDate(txtCTXStopDate.EditValue) == true)
                {
                    mCTXStopDate = Convert.ToDateTime(txtCTXStopDate.EditValue);
                }

                if (Program.IsDate(txtClinicalStageDate.EditValue) == true)
                {
                    mClinicalStageDate = Convert.ToDateTime(txtClinicalStageDate.EditValue);
                }

                if (cboARTRegimen.ItemIndex >= 0)
                {
                    mARVRegimenCode = cboARTRegimen.GetColumnValue("code").ToString();
                }

                if (Program.IsMoney(txtGestationAge.Text) == true)
                {
                    mGestationAge = Convert.ToDouble(txtGestationAge.Text);
                }

                if (Program.IsMoney(txtCD4Counts.Text) == true)
                {
                    mCD4Count = Convert.ToDouble(txtCD4Counts.Text);
                }

                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Add_PMTCTANCVisit(
                    txtPatientId.Text,
                    mTransDate,
                    null,
                    mGestationAge,
                    cboInfantFeedChoice.SelectedIndex,
                    mARVRegimenCode,
                    cboAdherenceAndDisclosure.SelectedIndex,
                    mTBRxStartDate,
                    mTBRxStopDate,
                    mCTXStartDate,
                    mCTXStopDate,
                    Convert.ToInt32(cboClinicalStage.Text),
                    mClinicalStageDate,
                    cboWhyEligible.SelectedIndex,
                    mCD4Count,
                    -1,
                    -1,
                    txtRemarks.Text,
                    pDtReferrals,
                    1,
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

                DateTime? mTBRxStartDate = null;
                DateTime? mTBRxStopDate = null;
                DateTime? mCTXStartDate = null;
                DateTime? mCTXStopDate = null;
                DateTime? mClinicalStageDate = null;
                string mARVRegimenCode = "";
                double mGestationAge = 0;
                double mCD4Count = 0;

                if (Program.IsDate(txtTBRxStartDate.EditValue) == true)
                {
                    mTBRxStartDate = Convert.ToDateTime(txtTBRxStartDate.EditValue);
                }

                if (Program.IsDate(txtTBRxStopDate.EditValue) == true)
                {
                    mTBRxStopDate = Convert.ToDateTime(txtTBRxStopDate.EditValue);
                }

                if (Program.IsDate(txtCTXStartDate.EditValue) == true)
                {
                    mCTXStartDate = Convert.ToDateTime(txtCTXStartDate.EditValue);
                }

                if (Program.IsDate(txtCTXStopDate.EditValue) == true)
                {
                    mCTXStopDate = Convert.ToDateTime(txtCTXStopDate.EditValue);
                }

                if (Program.IsDate(txtClinicalStageDate.EditValue) == true)
                {
                    mClinicalStageDate = Convert.ToDateTime(txtClinicalStageDate.EditValue);
                }

                if (cboARTRegimen.ItemIndex >= 0)
                {
                    mARVRegimenCode = cboARTRegimen.GetColumnValue("code").ToString();
                }

                if (Program.IsMoney(txtGestationAge.Text) == true)
                {
                    mGestationAge = Convert.ToDouble(txtGestationAge.Text);
                }

                if (Program.IsMoney(txtCD4Counts.Text) == true)
                {
                    mCD4Count = Convert.ToDouble(txtCD4Counts.Text);
                }                

                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Edit_PMTCTANCVisit(
                    Convert.ToInt32(pSelectedDataRow["autocode"]),
                    mTransDate,
                    null,
                    mGestationAge,
                    cboInfantFeedChoice.SelectedIndex,
                    mARVRegimenCode,
                    cboAdherenceAndDisclosure.SelectedIndex,
                    mTBRxStartDate,
                    mTBRxStopDate,
                    mCTXStartDate,
                    mCTXStopDate,
                    Convert.ToInt32(cboClinicalStage.Text),
                    mClinicalStageDate,
                    cboWhyEligible.SelectedIndex,
                    mCD4Count,
                    -1,
                    -1,
                    txtRemarks.Text,
                    pDtReferrals,
                    1,
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

        #region frmCTCPMTCTANCVisitDetails1_KeyDown
        void frmCTCPMTCTANCVisitDetails1_KeyDown(object sender, KeyEventArgs e)
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

        #region cmdReferedTo_ButtonClick
        private void cmdReferedTo_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            frmCTCReferrals mCTCReferrals = new frmCTCReferrals(pDtReferrals, "ctc_pmtctreferedto");
            mCTCReferrals.ShowDialog();

            cmdReferedTo.Text = mCTCReferrals.SelectedCodes;
        }
        #endregion
    }
}