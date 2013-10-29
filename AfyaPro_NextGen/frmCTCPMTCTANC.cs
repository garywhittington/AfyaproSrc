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
    public partial class frmCTCPMTCTANC : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_MT.clsCTCClients pMdtCTCClients;
        private AfyaPro_MT.clsReporter pMdtReporter;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

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

        #endregion

        #region frmCTCPMTCTANC
        public frmCTCPMTCTANC()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCPMTCTANC";
            this.KeyDown += new KeyEventHandler(frmCTCPMTCTANC_KeyDown);

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

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpmtctanc_customizelayout.ToString());
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

        #region frmCTCPMTCTANC_Load
        private void frmCTCPMTCTANC_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCPMTCTANC_Load";

            try
            {
                Program.Restore_FormLayout(layoutControl1, this.Name);
                Program.Restore_FormSize(this);

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

        #region frmCTCPMTCTANC_Shown
        private void frmCTCPMTCTANC_Shown(object sender, EventArgs e)
        {
            if (pSelectedPatient != null)
            {
                this.Data_Display(pSelectedPatient);
            }

            cboSearchBy.SelectedIndex = 0;
            txtSearchText.Focus();
        }
        #endregion

        #region frmCTCPMTCTANC_FormClosing
        private void frmCTCPMTCTANC_FormClosing(object sender, FormClosingEventArgs e)
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

                #region ctc_pmtct

                DataTable mDtART = pMdtReporter.View_LookupData("view_ctc_pmtct", "*", "patientcode='" + mPatient.code.Trim() + "'", "", "", "");

                if (mDtART.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtART.Rows[0];

                    txtStartDate.Text = mDataRow["startdate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["startdate"]).ToString("d");
                    txtANCCardNo.Text = mDataRow["anccardno"].ToString();
                    txtGestationAge.Text = mDataRow["gestationage"].ToString();
                    cboKnownHIVTestResult.SelectedIndex = Convert.ToInt32(mDataRow["knownhivtestresult"]);
                    txtPreCounsellingDate.Text = mDataRow["precounsellingdate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["precounsellingdate"]).ToString("d");
                    txtHIVTestDate.Text = mDataRow["hivtestdate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["hivtestdate"]).ToString("d");
                    cboHIVTestResult.SelectedIndex = Convert.ToInt32(mDataRow["hivtestresult"]);
                    txtPostCounsellingDate.Text = mDataRow["postcounsellingdate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["postcounsellingdate"]).ToString("d");
                    txtPartnerHIVTestDate.Text = mDataRow["partnerhivtestdate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["partnerhivtestdate"]).ToString("d");
                    cboPartnerHIVTestResult.SelectedIndex = Convert.ToInt32(mDataRow["partnerhivtestresult"]);
                    txtRemarks.Text = mDataRow["remarks"].ToString();

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
            txtStartDate.EditValue = null;
            txtANCCardNo.Text = "";
            txtGestationAge.Text = "";
            cboKnownHIVTestResult.SelectedIndex = 0;
            txtPreCounsellingDate.EditValue = null;
            txtHIVTestDate.EditValue = null;
            cboHIVTestResult.SelectedIndex = 0;
            txtPostCounsellingDate.EditValue = null;
            txtPartnerHIVTestDate.EditValue = null;
            cboPartnerHIVTestResult.SelectedIndex = 0;
            txtRemarks.Text = "";

            pPrevPatientId = "";
            pCurrPatientId = pPrevPatientId;
        }
        #endregion

        #region Ok
        private void Ok()
        {
            string mFunctionName = "Ok";

            try
            {
                DateTime? mStartDate = null;
                DateTime? mPreCounsellingDate = null;
                DateTime? mHIVTestDate = null;
                DateTime? mPostCounsellingDate = null;
                DateTime? mPartnerHIVTestDate = null;
                double mGestationAge = 0;
                int mKnownHIVResult = 0;
                int mHIVResult = 0;
                int mPartnerHIVResult = 0;

                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                if (Program.IsNullDate(txtStartDate.EditValue) == false)
                {
                    mStartDate = Convert.ToDateTime(txtStartDate.EditValue);
                }

                if (Program.IsNullDate(txtPreCounsellingDate.EditValue) == false)
                {
                    mPreCounsellingDate = Convert.ToDateTime(txtPreCounsellingDate.EditValue);
                }

                if (Program.IsNullDate(txtHIVTestDate.EditValue) == false)
                {
                    mHIVTestDate = Convert.ToDateTime(txtHIVTestDate.EditValue);
                }

                if (Program.IsNullDate(txtPostCounsellingDate.EditValue) == false)
                {
                    mPostCounsellingDate = Convert.ToDateTime(txtPostCounsellingDate.EditValue);
                }

                if (Program.IsDate(txtPartnerHIVTestDate.EditValue) == true)
                {
                    mPartnerHIVTestDate = Convert.ToDateTime(txtPartnerHIVTestDate.EditValue);
                }

                if (Program.IsMoney(txtGestationAge.Text) == true)
                {
                    mGestationAge = Convert.ToDouble(txtGestationAge.Text);
                }

                if (cboKnownHIVTestResult.SelectedIndex != -1)
                {
                    mKnownHIVResult = cboKnownHIVTestResult.SelectedIndex;
                }

                if (cboHIVTestResult.SelectedIndex != -1)
                {
                    mHIVResult = cboHIVTestResult.SelectedIndex;
                }

                if (cboPartnerHIVTestResult.SelectedIndex != -1)
                {
                    mPartnerHIVResult = cboPartnerHIVTestResult.SelectedIndex;
                }

                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Enroll_PMTCTANC(
                    txtPatientId.Text,
                    mTransDate,
                    mStartDate,
                    txtANCCardNo.Text,
                    mGestationAge,
                    mKnownHIVResult,
                    mPreCounsellingDate,
                    mHIVTestDate,
                    mHIVResult,
                    mPostCounsellingDate,
                    mPartnerHIVTestDate,
                    mPartnerHIVResult,
                    txtRemarks.Text,
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

                this.pSelectedPatient = this.Search_Patient("p.code", txtPatientId.Text);
                this.Data_Display(pSelectedPatient);
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

        #region frmCTCPMTCTANC_KeyDown
        void frmCTCPMTCTANC_KeyDown(object sender, KeyEventArgs e)
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

        #region cmdVisits_Click
        private void cmdVisits_Click(object sender, EventArgs e)
        {
            if (pSelectedPatient == null)
            {
                Program.Display_Error("Please specify client # and try again");
                txtPatientId.Focus();
                return;
            }

            frmCTCPMTCTANCVisits mCTCARTPVisits = new frmCTCPMTCTANCVisits();
            mCTCARTPVisits.SelectedPatient = pSelectedPatient;
            mCTCARTPVisits.ShowDialog();
        }
        #endregion

        #region cmdNewVisit_Click
        private void cmdNewVisit_Click(object sender, EventArgs e)
        {
            if (pSelectedPatient == null)
            {
                Program.Display_Error("Please specify client # and try again");
                txtPatientId.Focus();
                return;
            }

            bool mFirstVisit = false;
            //check if first visit
            DataTable mDtHistory = pMdtCTCClients.View_PMTCTCareVisits(
                "patientcode='" + pSelectedPatient.code.Trim() + "'", "transdate desc,autocode desc", "");
            if (mDtHistory.Rows.Count == 0)
            {
                mFirstVisit = true;
            }

            if (mFirstVisit == true)
            {
                frmCTCPMTCTANCVisitDetails1 mCTCPMTCTANCVisitDetails1 = new frmCTCPMTCTANCVisitDetails1();
                mCTCPMTCTANCVisitDetails1.SelectedPatient = pSelectedPatient;
                mCTCPMTCTANCVisitDetails1.ShowDialog();

                if (mCTCPMTCTANCVisitDetails1.RecordSaved == true)
                {
                    txtPatientId.Text = "";
                    this.Data_Clear();
                    txtPatientId.Focus();
                }
            }
            else
            {
                frmCTCPMTCTANCVisitDetails2 mCTCPMTCTANCVisitDetails2 = new frmCTCPMTCTANCVisitDetails2();
                mCTCPMTCTANCVisitDetails2.SelectedPatient = pSelectedPatient;
                mCTCPMTCTANCVisitDetails2.ShowDialog();

                if (mCTCPMTCTANCVisitDetails2.RecordSaved == true)
                {
                    txtPatientId.Text = "";
                    this.Data_Clear();
                    txtPatientId.Focus();
                }
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

        #region txtPatientId_Enter
        private void txtPatientId_Enter(object sender, EventArgs e)
        {
            txtPatientId.SelectAll();
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
    }
}