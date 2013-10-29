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
    public partial class frmCTCPMTCTMotherChild : DevExpress.XtraEditors.XtraForm
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

        #region frmCTCPMTCTMotherChild
        public frmCTCPMTCTMotherChild()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCPMTCTMotherChild";
            this.KeyDown += new KeyEventHandler(frmCTCPMTCTMotherChild_KeyDown);

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
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpmtctmotherchild_customizelayout.ToString());
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

        #region frmCTCPMTCTMotherChild_Load
        private void frmCTCPMTCTMotherChild_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCPMTCTMotherChild_Load";

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

        #region frmCTCPMTCTMotherChild_Shown
        private void frmCTCPMTCTMotherChild_Shown(object sender, EventArgs e)
        {
            if (pSelectedPatient != null)
            {
                this.Data_Display(pSelectedPatient);
            }

            cboSearchBy.SelectedIndex = 0;
            txtSearchText.Focus();
        }
        #endregion

        #region frmCTCPMTCTMotherChild_FormClosing
        private void frmCTCPMTCTMotherChild_FormClosing(object sender, FormClosingEventArgs e)
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

                #region ctc_pmtctmotherchild

                DataTable mDtART = pMdtReporter.View_LookupData("ctc_pmtctmotherchild", "*", "patientcode='" + mPatient.code.Trim() + "'", "", "", "");

                if (mDtART.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtART.Rows[0];

                    cboARVMotherLabour.SelectedIndex = Convert.ToInt32(mDataRow["arvmotherlabour"]);
                    cboFamilyPlanMethod.SelectedIndex = Convert.ToInt32(mDataRow["familyplanmethod"]);
                    cboOutcomeAt6Months.SelectedIndex = Convert.ToInt32(mDataRow["outcomeat6months"]);
                    txtUnder5RegNo.Text = mDataRow["under5regno"] == DBNull.Value ? "" : mDataRow["under5regno"].ToString();
                    txtChildBirthDate.Text = mDataRow["childbirthdate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["childbirthdate"]).ToString("d");
                    txtChildBirthWeight.Text = mDataRow["childbirthweight"] == DBNull.Value ? "" : mDataRow["childbirthweight"].ToString();
                    cboInfantFeeding.SelectedIndex = Convert.ToInt32(mDataRow["infantfeeding"]);
                    cboInfantFeedingPractice.SelectedIndex = Convert.ToInt32(mDataRow["infantfeedingpractice"]);
                    cboOnCotrim.SelectedIndex = Convert.ToInt32(mDataRow["oncotrim"]);
                    txtCotrimDate.Text = mDataRow["cotrimdate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["cotrimdate"]).ToString("d");
                    cboInfantARV.SelectedIndex = Convert.ToInt32(mDataRow["infantarv"]);
                    txtFirstTestDate.Text = mDataRow["firsttestdate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["firsttestdate"]).ToString("d");
                    cboFirstPCRTest.SelectedIndex = Convert.ToInt32(mDataRow["firstpcrtest"]);
                    cboFirstTestResult.SelectedIndex = Convert.ToInt32(mDataRow["firsttestresult"]);

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
            cboARVMotherLabour.SelectedIndex = -1;
            cboFamilyPlanMethod.SelectedIndex = -1;
            cboOutcomeAt6Months.SelectedIndex = -1;
            txtUnder5RegNo.Text = "";
            txtChildBirthDate.EditValue = null;
            txtChildBirthWeight.Text = "";
            cboInfantFeeding.SelectedIndex = -1;
            cboInfantFeedingPractice.SelectedIndex = -1;
            cboOnCotrim.SelectedIndex = -1;
            txtCotrimDate.EditValue = null;
            cboInfantARV.SelectedIndex = -1;
            cboFirstPCRTest.SelectedIndex = -1;
            txtFirstTestDate.EditValue = null;
            cboFirstTestResult.SelectedIndex = -1;

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
                DateTime? mChildBirthDate = null;
                DateTime? mCotrimDate = null;
                DateTime? mFirstTestDate = null;
                double mChildBirthWeight = 0;

                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                if (Program.IsNullDate(txtChildBirthDate.EditValue) == false)
                {
                    mChildBirthDate = Convert.ToDateTime(txtChildBirthDate.EditValue);
                }

                if (Program.IsNullDate(txtCotrimDate.EditValue) == false)
                {
                    mCotrimDate = Convert.ToDateTime(txtCotrimDate.EditValue);
                }

                if (Program.IsNullDate(txtFirstTestDate.EditValue) == false)
                {
                    mFirstTestDate = Convert.ToDateTime(txtFirstTestDate.EditValue);
                }

                if (Program.IsMoney(txtChildBirthWeight.Text) == true)
                {
                    mChildBirthWeight = Convert.ToDouble(txtChildBirthWeight.Text);
                }

                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Enroll_MotherChild(
                    txtPatientId.Text,
                    mTransDate,
                    cboARVMotherLabour.SelectedIndex,
                    cboFamilyPlanMethod.SelectedIndex,
                    cboOutcomeAt6Months.SelectedIndex,
                    txtUnder5RegNo.Text,
                    mChildBirthDate,
                    mChildBirthWeight,
                    cboInfantFeeding.SelectedIndex,
                    cboInfantFeedingPractice.SelectedIndex,
                    cboOnCotrim.SelectedIndex,
                    mCotrimDate,
                    cboInfantARV.SelectedIndex,
                    mFirstTestDate,
                    cboFirstPCRTest.SelectedIndex,
                    cboFirstTestResult.SelectedIndex,
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

        #region frmCTCPMTCTMotherChild_KeyDown
        void frmCTCPMTCTMotherChild_KeyDown(object sender, KeyEventArgs e)
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

            frmCTCPMTCTMotherChildVisits mCTCARTPVisits = new frmCTCPMTCTMotherChildVisits();
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

            frmCTCPMTCTMotherChildVisitDetails mCTCPMTCTMotherChildVisitDetails = new frmCTCPMTCTMotherChildVisitDetails();
            mCTCPMTCTMotherChildVisitDetails.SelectedPatient = pSelectedPatient;
            mCTCPMTCTMotherChildVisitDetails.ShowDialog();

            if (mCTCPMTCTMotherChildVisitDetails.RecordSaved == true)
            {
                txtPatientId.Text = "";
                this.Data_Clear();
                txtPatientId.Focus();
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