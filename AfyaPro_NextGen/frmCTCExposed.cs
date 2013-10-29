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
    public partial class frmCTCExposed : DevExpress.XtraEditors.XtraForm
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

        #endregion

        #region frmCTCExposed
        public frmCTCExposed()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCExposed";
            this.KeyDown += new KeyEventHandler(frmCTCExposed_KeyDown);

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

                this.Fill_LookupData();

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcexposed_customizelayout.ToString());
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

        #region frmCTCExposed_Load
        private void frmCTCExposed_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCExposed_Load";

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

        #region frmCTCExposed_Shown
        private void frmCTCExposed_Shown(object sender, EventArgs e)
        {
            this.Data_Clear();

            if (pSelectedPatient != null)
            {
                this.Data_Display(pSelectedPatient);
            }
        }
        #endregion

        #region frmCTCExposed_FormClosing
        private void frmCTCExposed_FormClosing(object sender, FormClosingEventArgs e)
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
                #region arv comb

                DataTable mDtARVComb = pMdtReporter.View_LookupData("ctc_pmtctcomb", "code,description", "active=1", "autocode", Program.gLanguageName, "grdCTCPMTCTComb");

                #region arvpreg

                cboARVPreg.Properties.DataSource = mDtARVComb;
                cboARVPreg.Properties.DisplayMember = "description";
                cboARVPreg.Properties.ValueMember = "code";

                cboARVPreg.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboARVPreg.Properties.BestFit();

                #endregion

                #region arvlabour

                cboARVLabour.Properties.DataSource = mDtARVComb;
                cboARVLabour.Properties.DisplayMember = "description";
                cboARVLabour.Properties.ValueMember = "code";

                cboARVLabour.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboARVLabour.Properties.BestFit();

                #endregion

                #region arvbabybirth

                cboARVBabyBirth.Properties.DataSource = mDtARVComb;
                cboARVBabyBirth.Properties.DisplayMember = "description";
                cboARVBabyBirth.Properties.ValueMember = "code";

                cboARVBabyBirth.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboARVBabyBirth.Properties.BestFit();

                #endregion

                #region arvbabycont

                cboARVBabyCont.Properties.DataSource = mDtARVComb;
                cboARVBabyCont.Properties.DisplayMember = "description";
                cboARVBabyCont.Properties.ValueMember = "code";

                cboARVBabyCont.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboARVBabyCont.Properties.BestFit();

                #endregion

                #endregion

                #region motherstatus

                DataTable mDtMotherStatus = pMdtReporter.View_LookupData("ctc_motherstatus", "code,description", "active=1", "", Program.gLanguageName, "grdCTCMotherStatus");
                cboMotherStatus.Properties.DataSource = mDtMotherStatus;
                cboMotherStatus.Properties.DisplayMember = "description";
                cboMotherStatus.Properties.ValueMember = "code";

                cboMotherStatus.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboMotherStatus.Properties.BestFit();

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

                    txtHIVNo.Text = mDataRow["hivno"].ToString();
                    txtHTCNo.Text = mDataRow["htcno"].ToString();

                    #region logic new/update

                    if (mDataRow["ctcno"].ToString().Trim() == "")
                    {
                        Int16 mGenerateCode = pMdtAutoCodes.Auto_Generate_Code(
                            Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcnumbers));
                        if (mGenerateCode == -1)
                        {
                            Program.Display_Server_Error("");
                            return;
                        }

                        txtARTNo.Text = "";
                        if (mGenerateCode == 1)
                        {
                            txtARTNo.Text = "<<---New--->>";
                        }
                    }
                    else
                    {
                        txtARTNo.Text = mDataRow["ctcno"].ToString();
                    }

                    #endregion
                }

                #endregion

                #region ctc_exposed

                DataTable mDtART = pMdtReporter.View_LookupData("view_ctc_exposed", "*", "patientcode='" + mPatient.code.Trim() + "'", "", "", "");

                if (mDtART.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtART.Rows[0];

                    txtBirthWeight.Text = mDataRow["weight"].ToString();
                    txtDate.Text = mDataRow["enrolmentdate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["enrolmentdate"]).ToString("d");
                    txtTransferInDate.Text = mDataRow["transferindate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["transferindate"]).ToString("d");
                    cboARVPreg.ItemIndex = Program.Get_LookupItemIndex(cboARVPreg, "code", mDataRow["arvpregcode"].ToString());
                    cboARVLabour.ItemIndex = Program.Get_LookupItemIndex(cboARVLabour, "code", mDataRow["arvlabourcode"].ToString());
                    cboARVBabyBirth.ItemIndex = Program.Get_LookupItemIndex(cboARVBabyBirth, "code", mDataRow["arvbabybirthcode"].ToString());
                    cboARVBabyCont.ItemIndex = Program.Get_LookupItemIndex(cboARVBabyCont, "code", mDataRow["arvbabycontcode"].ToString());
                    txtARVBabyAdhere.Text = mDataRow["arvbabyadhere"].ToString();
                    txtDeliveryPlace.Text = mDataRow["deliveryplace"].ToString();
                    cboHIVTestRapid.SelectedIndex = Convert.ToInt32(mDataRow["hivtestrapid"]);
                    txtHIVTestRapidAge.Text = mDataRow["hivtestrapidage"].ToString();
                    cboHIVTestPCR.SelectedIndex = Convert.ToInt32(mDataRow["hivtestpcr"]);
                    txtHIVTestPCRAge.Text = mDataRow["hivtestpcrage"].ToString();
                    cboConfirmedHIV.SelectedIndex = Convert.ToInt32(mDataRow["confirmedhiv"]);
                    cboMotherStatus.ItemIndex = Program.Get_LookupItemIndex(cboMotherStatus, "code", mDataRow["motherstatuscode"].ToString());
                    txtMotherARTNo.Text = mDataRow["motherartno"].ToString();
                    txtBirthCohortMonth.Text = mDataRow["birthcohortmonth"].ToString();
                    txtBirthCohortYear.Text = mDataRow["birthcohortyear"].ToString();
                    cboModeofEntry.Text = mDataRow["modeofentry"].ToString();
                    if (Program.IsNullDate(mPatient.birthdate) == false && Program.IsNullDate(txtDate.EditValue) == false)
                    {
                        int mDays = Convert.ToDateTime(txtDate.EditValue).Subtract(mPatient.birthdate).Days;
                        int mYears = (int)mDays / 365;
                        int mMonths = (int)(mDays % 365) / 30;

                        txtAgeAtStart.Text = mYears + " Yrs," + mMonths + " Mths";
                    }

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
            txtHIVNo.Text = "";
            txtHTCNo.Text = "";
            txtARTNo.Text = "";
            txtBirthWeight.Text = "";
            txtTransferInDate.EditValue = null;
            cboARVPreg.EditValue = null;
            cboARVLabour.EditValue = null;
            cboARVBabyBirth.EditValue = null;
            cboARVBabyCont.EditValue = null;
            txtARVBabyAdhere.Text = "";
            txtDeliveryPlace.Text = "";
            cboHIVTestPCR.SelectedIndex = -1;
            cboHIVTestRapid.SelectedIndex = -1;
            txtHIVTestPCRAge.Text = "";
            txtHIVTestRapidAge.Text = "";
            cboConfirmedHIV.SelectedIndex = -1;
            cboMotherStatus.EditValue = null;
            txtMotherARTNo.Text = "";
            txtBirthCohortMonth.Text = "";
            txtBirthCohortYear.Text = "";
            cboModeofEntry.SelectedIndex = -1;
           

            pPrevPatientId = "";
            pCurrPatientId = pPrevPatientId;

            txtDate.EditValue = Program.gMdiForm.txtDate.EditValue;
        }
        #endregion

        #region Ok
        private void Ok()
        {
            string mFunctionName = "Ok";

            if (Program.IsNullDate(txtDate.EditValue) == true)
            {
                Program.Display_Error("Invalid date of enrolment");
                txtDate.Focus();
                return;
            }

            try
            {
                DateTime? mTransferInDate = null;
                double mBirthWeight = 0;
                string mARVPregCode = "";
                string mARVLabourCode = "";
                string mARVBabyBirthCode = "";
                string mARVBabyContCode = "";
                double mARVBabyAdhere = 0;
                string mHIVTestRapidAge = txtHIVTestRapidAge.Text;
                string mHIVTestPCRAge = txtHIVTestPCRAge.Text;
                string mMotherStatusCode = "";
                int mBirthCohortYear = 0;
                int mBirthCohortMonth = 0;
                string mEntryMode = cboModeofEntry.Text;

                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

                if (Program.IsNullDate(txtTransferInDate.EditValue) == false)
                {
                    mTransferInDate = Convert.ToDateTime(txtTransferInDate.EditValue);
                }


                if (Program.IsMoney(txtBirthWeight.Text) == true)
                {
                    mBirthWeight = Convert.ToDouble(txtBirthWeight.Text);
                }

                if (Program.IsMoney(txtARVBabyAdhere.Text) == true)
                {
                    mARVBabyAdhere = Convert.ToDouble(txtARVBabyAdhere.Text);
                }

                if (Program.IsNumeric(txtBirthCohortYear.Text) == true)
                {
                    mBirthCohortYear = Convert.ToInt32(txtBirthCohortYear.Text);
                }

                if (Program.IsNumeric(txtBirthCohortMonth.Text) == true)
                {
                    mBirthCohortMonth = Convert.ToInt32(txtBirthCohortMonth.Text);
                }

                if (cboARVPreg.ItemIndex != -1)
                {
                    mARVPregCode = cboARVPreg.GetColumnValue("code").ToString().Trim();
                }

                if (cboARVLabour.ItemIndex != -1)
                {
                    mARVLabourCode = cboARVLabour.GetColumnValue("code").ToString().Trim();
                }

                if (cboARVBabyBirth.ItemIndex != -1)
                {
                    mARVBabyBirthCode = cboARVBabyBirth.GetColumnValue("code").ToString().Trim();
                }

                if (cboARVBabyCont.ItemIndex != -1)
                {
                    mARVBabyContCode = cboARVBabyCont.GetColumnValue("code").ToString().Trim();
                }

                if (cboMotherStatus.ItemIndex != -1)
                {
                    mMotherStatusCode = cboMotherStatus.GetColumnValue("code").ToString().Trim();
                }

                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Enroll_Exposed(
                    txtPatientId.Text,
                    mTransDate,
                    mBirthWeight,
                    mTransferInDate,
                    mARVPregCode,
                    mARVLabourCode,
                    mARVBabyBirthCode,
                    mARVBabyContCode,
                    mARVBabyAdhere,
                    txtDeliveryPlace.Text,
                    cboHIVTestRapid.SelectedIndex,
                    mHIVTestRapidAge,
                    cboHIVTestPCR.SelectedIndex,
                    mHIVTestPCRAge,
                    cboConfirmedHIV.SelectedIndex,
                    mMotherStatusCode,
                    txtMotherARTNo.Text,
                    mBirthCohortYear,
                    mBirthCohortMonth,
                    mEntryMode,
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

        #region frmCTCExposed_KeyDown
        void frmCTCExposed_KeyDown(object sender, KeyEventArgs e)
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

        #region txtPatientId_KeyDown
        private void txtPatientId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.pSelectedPatient = this.Search_Patient("p.code", txtPatientId.Text);
                this.Data_Display(pSelectedPatient);
            }
        }
        #endregion

        #region txtPatientId_Leave
        private void txtPatientId_Leave(object sender, EventArgs e)
        {
            pCurrPatientId = txtPatientId.Text;

            if (pCurrPatientId.Trim().ToLower() != pPrevPatientId.Trim().ToLower())
            {
                this.pSelectedPatient = this.Search_Patient("p.code", txtPatientId.Text);
                this.Data_Display(pSelectedPatient);
            }
        }
        #endregion

        #endregion

        #region search by hivno

        #region txtHIVNo_EditValueChanged
        private void txtHIVNo_EditValueChanged(object sender, EventArgs e)
        {
            if (pSearchingPatient == true)
            {
                this.pSelectedPatient = this.Search_Patient("c.hivno", txtHIVNo.Text);
                this.Data_Display(pSelectedPatient);
            }
        }
        #endregion

        #region txtHIVNo_Enter
        private void txtHIVNo_Enter(object sender, EventArgs e)
        {
            txtHIVNo.SelectAll();
        }
        #endregion

        #region txtHIVNo_KeyDown
        private void txtHIVNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.pSelectedPatient = this.Search_Patient("c.hivno", txtHIVNo.Text);
                this.Data_Display(pSelectedPatient);
            }
        }
        #endregion

        #region txtHIVNo_Leave
        private void txtHIVNo_Leave(object sender, EventArgs e)
        {
            pCurrHIVNo = txtHIVNo.Text;

            if (pCurrHIVNo.Trim().ToLower() != pPrevHIVNo.Trim().ToLower())
            {
                this.pSelectedPatient = this.Search_Patient("c.hivno", txtHIVNo.Text);
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
                Program.Display_Error("Please specify client # and try again");
                txtPatientId.Focus();
                return;
            }

            frmCTCExposedVisits mCTCARTPVisits = new frmCTCExposedVisits();
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

            frmCTCExposedVisitDetails mCTCPreARTPVisitDetails = new frmCTCExposedVisitDetails();
            mCTCPreARTPVisitDetails.SelectedPatient = pSelectedPatient;
            mCTCPreARTPVisitDetails.ShowDialog();

            if (mCTCPreARTPVisitDetails.RecordSaved == true)
            {
                txtPatientId.Text = "";
                this.Data_Clear();
                txtPatientId.Focus();
            }
        }
        #endregion

        #region txtCTCNo_Leave
        private void txtCTCNo_Leave(object sender, EventArgs e)
        {
            if (txtARTNo.Text.Trim() == "")
            {
                Int16 mGenerateCode = pMdtAutoCodes.Auto_Generate_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcnumbers));
                if (mGenerateCode == -1)
                {
                    return;
                }
                if (mGenerateCode == 1)
                {
                    txtARTNo.Text = "<<---New--->>";
                }
            }
        }
        #endregion

        #region txtCTCNo_Enter
        private void txtCTCNo_Enter(object sender, EventArgs e)
        {
            if (txtARTNo.Text.Trim().ToLower() == "<<---new--->>")
            {
                txtARTNo.Text = "";
            }
            else
            {
                txtARTNo.SelectAll();
            }
        }
        #endregion
    }
}