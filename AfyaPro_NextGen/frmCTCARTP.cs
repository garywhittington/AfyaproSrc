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
    public partial class frmCTCARTP : DevExpress.XtraEditors.XtraForm
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

        #region frmCTCARTP
        public frmCTCARTP()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCARTP";
            this.KeyDown += new KeyEventHandler(frmCTCARTP_KeyDown);

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
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcartp_customizelayout.ToString());
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

        #region frmCTCARTP_Load
        private void frmCTCARTP_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCARTP_Load";

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

        #region frmCTCARTP_Shown
        private void frmCTCARTP_Shown(object sender, EventArgs e)
        {
            this.Data_Clear();

            if (pSelectedPatient != null)
            {
                this.Data_Display(pSelectedPatient);
            }
        }
        #endregion

        #region frmCTCARTP_FormClosing
        private void frmCTCARTP_FormClosing(object sender, FormClosingEventArgs e)
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
                #region artregimens

                DataTable mDtARTRegimens = pMdtReporter.View_LookupData("ctc_arvcombregimens", "code,description", "active=1", "", Program.gLanguageName, "grdCTCARVCombRegimens");
                cboARTRegimenCode1.Properties.DataSource = mDtARTRegimens;
                cboARTRegimenCode1.Properties.DisplayMember = "description";
                cboARTRegimenCode1.Properties.ValueMember = "code";

                cboARTRegimenCode1.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboARTRegimenCode1.Properties.BestFit();

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

                #region ctc_artp

                DataTable mDtART = pMdtReporter.View_LookupData("view_ctc_artp", "*", "patientcode='" + mPatient.code.Trim() + "'", "", "", "");

                if (mDtART.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtART.Rows[0];

                    txtTransferInDate.Text = mDataRow["transferindate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["transferindate"]).ToString("d");
                    txtHIVTestSite.Text = mDataRow["hivtestsite1"].ToString();
                    txtHIVTestDate.Text = mDataRow["hivtestdate1"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["hivtestdate1"]).ToString("d");
                    cboHIVTestType.SelectedIndex = Convert.ToInt32(mDataRow["hivtesttype1"]);
                    txtHIVTestSite2.Text = mDataRow["hivtestsite2"].ToString();
                    txtHIVTestDate2.Text = mDataRow["hivtestdate2"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["hivtestdate2"]).ToString("d");
                    cboHIVTestType2.SelectedIndex = Convert.ToInt32(mDataRow["hivtesttype2"]);
                    txtHIVTestSite3.Text = mDataRow["hivtestsite3"].ToString();
                    txtHIVTestDate3.Text = mDataRow["hivtestdate3"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["hivtestdate3"]).ToString("d");
                    cboHIVTestType3.SelectedIndex = Convert.ToInt32(mDataRow["hivtesttype3"]);
                    cboClinicalStage.Text = mDataRow["clinicalstage"].ToString();
                  
                    cboTBStatus.SelectedIndex = (int)mDataRow["tbinitialstatus"];
                    txtTBTreatmentDate.Text = mDataRow["tbtreatmentdate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["tbtreatmentdate"]).ToString("d");
                    txtTBRegNo.Text = mDataRow["tbregno"].ToString();
                    chkARTEdu.Checked = Convert.ToBoolean(mDataRow["artedu"]);
                    txtARTEduDate.Text = mDataRow["artedudate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["artedudate"]).ToString("d");
                    chkKS.Checked = Convert.ToBoolean(mDataRow["ks"]);
                    cboARTRegimenCode1.ItemIndex = Program.Get_LookupItemIndex(cboARTRegimenCode1, "code", mDataRow["artregimencode1"].ToString());
                    txtARTRegimenDate1.Text = mDataRow["artregimendate1"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["artregimendate1"]).ToString("d");
                    txtCD4Count.Text = mDataRow["cd4count"].ToString();
                    txtCD4CountPercent.Text = mDataRow["cd4countpercent"].ToString();
                    txtCD4TestDate.Text = mDataRow["cd4date"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["cd4date"]).ToString("d");
                    txtWeight.Text = mDataRow["weight"].ToString();
                    txtHeight.Text = mDataRow["height"].ToString();
                    cboModeOfEntry.Text = mDataRow["arventrymode"].ToString();
                    txtInitiationDate.Text = mDataRow["artinitiationdate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["artinitiationdate"]).ToString("d");
                    //txtTransferInDate.Text = mDataRow["transferindate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["transferindate"]).ToString("d");
                    cboStartReason.Text = mDataRow["arvstartreason"].ToString();

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
            txtTransferInDate.EditValue = null;
            txtHIVTestSite.Text = "";
            txtHIVTestDate.EditValue = null;
            cboHIVTestType.SelectedIndex = -1;
            txtHIVTestSite2.Text = "";
            txtHIVTestDate2.EditValue = null;
            cboHIVTestType2.SelectedIndex = -1;
            txtHIVTestSite3.Text = "";
            txtHIVTestDate3.EditValue = null;
            cboHIVTestType3.SelectedIndex = -1;
            cboClinicalStage.SelectedIndex = -1;
            //txtClinicalConditions.Text = "";
            cboTBStatus.SelectedIndex = -1;
            txtTBTreatmentDate.EditValue = null;
            txtTBRegNo.Text = "";
            chkARTEdu.Checked = false;
            txtARTEduDate.EditValue = null;
            chkKS.Checked = false;
            //chkTakenARV.Checked = false;
            //txtARVDate.EditValue = null;
            //txtARVType.Text = "";
            cboARTRegimenCode1.EditValue = null;
            txtARTRegimenDate1.EditValue = null;
            cboStartReason.SelectedIndex = -1;
            cboModeOfEntry.SelectedIndex = -1;
            txtInitiationDate.EditValue = null;
            txtCD4Count.ResetText();
            txtCD4CountPercent.ResetText();
            

            pPrevPatientId = "";
            pCurrPatientId = pPrevPatientId;
        }
        #endregion

        #region Ok
        private void Ok()
        {
            Int16 mGenerateCTCNo = 0;

            string mFunctionName = "Ok";

            if (txtARTNo.Text.Trim() == "" && txtARTNo.Text.Trim().ToLower() != "<<---new--->>")
            {
                Program.Display_Error("Please enter ART number");
                txtHTCNo.Focus();
                return;
            }


            if (cboModeOfEntry.SelectedIndex == -1)
            {
                Program.Display_Error("Please select Mode of Entry", false);
                cboModeOfEntry.Focus();
                return;
            }

            if ((cboModeOfEntry.Text =="TI") && (txtTransferInDate.Text == ""))
            {
                Program.Display_Error("Please enter transferin date for the patient", false);
                txtTransferInDate.Focus();
                return;
            }

            if ((cboModeOfEntry.Text != "TI") && (txtInitiationDate.Text == ""))
            {
                Program.Display_Error("Please enter ART initiation date for the patient", false);
                txtInitiationDate.Focus();
                return;
            }

            if (cboStartReason.SelectedIndex == -1)
            {
                Program.Display_Error("Please select ART start reason", false);
                cboStartReason.Focus();
                return;
            }

           
            if (cboClinicalStage.SelectedIndex == -1)
            {
                Program.Display_Error("Please select clinical stage", false);
                cboClinicalStage.Focus();
                return;
            }

            if (cboTBStatus.SelectedIndex == -1)
            {
                Program.Display_Error("Please select TB Status at Initiation", false);
                cboTBStatus.Focus();
                return;
            }

            if (cboARTRegimenCode1.Text == "")
            {
                Program.Display_Error("Please select ART Regimen", false);
                cboARTRegimenCode1.Focus();
                return;
            }

            try
            {
                DateTime? mTransferInDate = null;
                DateTime? mHIVTestDate1 = null;
                DateTime? mHIVTestDate2 = null;
                DateTime? mHIVTestDate3 = null;
                DateTime? mTBTreatmentDate = null;
                DateTime? mARVDate = null;
                DateTime? mARTEduDate = null;
                DateTime? mARTRegimenDate1 = null;
                string mARTRegimenCode1 = "";
                double mCD4Count = 0;
                double mCD4CountPercent = 0;
                DateTime? mCD4Date = null;
                double mWeight = 0;
                double mHeight = 0;

                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                if (txtARTNo.Text.Trim().ToLower() == "<<---new--->>")
                {
                    mGenerateCTCNo = 1;
                }

                if (Program.IsNullDate(txtTransferInDate.EditValue) == false)
                {
                    mTransferInDate = Convert.ToDateTime(txtTransferInDate.EditValue);
                }

                if (Program.IsNullDate(txtHIVTestDate.EditValue) == false)
                {
                    mHIVTestDate1 = Convert.ToDateTime(txtHIVTestDate.EditValue);
                }

                if (Program.IsNullDate(txtHIVTestDate2.EditValue) == false)
                {
                    mHIVTestDate2 = Convert.ToDateTime(txtHIVTestDate2.EditValue);
                }

                if (Program.IsNullDate(txtHIVTestDate3.EditValue) == false)
                {
                    mHIVTestDate3 = Convert.ToDateTime(txtHIVTestDate3.EditValue);
                }

                if (Program.IsDate(txtTBTreatmentDate.EditValue) == true)
                {
                    mTBTreatmentDate = Convert.ToDateTime(txtTBTreatmentDate.EditValue);
                }

                //if (Program.IsDate(txtARVDate.EditValue) == true)
                //{
                //    mARVDate = Convert.ToDateTime(txtARVDate.EditValue);
                //}

                if (Program.IsDate(txtARTEduDate.EditValue) == true)
                {
                    mARTEduDate = Convert.ToDateTime(txtARTEduDate.EditValue);
                }

                if (Program.IsDate(txtARTRegimenDate1.EditValue) == true)
                {
                    mARTRegimenDate1 = Convert.ToDateTime(txtARTRegimenDate1.EditValue);
                }

                if (cboARTRegimenCode1.ItemIndex != -1)
                {
                    mARTRegimenCode1 = cboARTRegimenCode1.GetColumnValue("code").ToString().Trim();
                }

                if (Program.IsMoney(txtCD4Count.Text) == true)
                {
                    mCD4Count = Convert.ToDouble(txtCD4Count.Text);
                }

                if (Program.IsMoney(txtCD4CountPercent.Text) == true)
                {
                    mCD4CountPercent = Convert.ToDouble(txtCD4CountPercent.Text);
                }

                if (Program.IsDate(txtCD4TestDate.EditValue) == true)
                {
                    mCD4Date = Convert.ToDateTime(txtCD4TestDate.EditValue);
                }

                if (Program.IsMoney(txtWeight.Text) == true)
                {
                    mWeight = Convert.ToDouble(txtWeight.Text);
                }

                if (Program.IsMoney(txtHeight.Text) == true)
                {
                    mHeight = Convert.ToDouble(txtHeight.Text);
                }

                string mOutcome = "Alive";
                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Enroll_ARTP(
                    txtPatientId.Text,
                    mGenerateCTCNo,
                    txtARTNo.Text,
                    mTransDate,
                    mTransferInDate,
                    txtHIVTestSite.Text,
                    mHIVTestDate1,
                    cboHIVTestType.SelectedIndex,
                    txtHIVTestSite2.Text,
                    mHIVTestDate2,
                    cboHIVTestType2.SelectedIndex,
                    txtHIVTestSite3.Text,
                    mHIVTestDate3,
                    cboHIVTestType3.SelectedIndex,
                    Convert.ToInt32(cboClinicalStage.Text),
                    mCD4Count,
                    mCD4CountPercent,
                    mCD4Date,
                    mWeight,
                    mHeight,
                    cboTBStatus.SelectedIndex,
                    mTBTreatmentDate,
                    txtTBRegNo.Text,
                    Convert.ToInt32(chkARTEdu.Checked),
                    mARTEduDate,
                    Convert.ToInt32(chkKS.Checked),
                    mARTRegimenCode1,
                    mARTRegimenDate1,
                    Convert.ToDateTime(txtInitiationDate.Text),
                    cboModeOfEntry.Text,
                    mOutcome,
                    cboStartReason.Text,
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

                //MessageBox.Show(cboModeOfEntry.Text + " " + cboStartReason.Text);
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

        #region frmCTCARTP_KeyDown
        void frmCTCARTP_KeyDown(object sender, KeyEventArgs e)
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

            frmCTCARTPVisits mCTCARTPVisits = new frmCTCARTPVisits();
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

            frmCTCARTPVisitDetails mCTCPreARTPVisitDetails = new frmCTCARTPVisitDetails();
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