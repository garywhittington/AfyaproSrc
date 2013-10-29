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
    public partial class frmCTCPreART : DevExpress.XtraEditors.XtraForm
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

        #region frmCTCPreART
        public frmCTCPreART()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCPreART";
            this.KeyDown += new KeyEventHandler(frmCTCPreART_KeyDown);

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
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpreart_customizelayout.ToString());
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

            txtIPTDate.Enabled = false;
            txtTBTreatmentDate.Enabled = false;
            txtTBRegNo.Enabled = false;
            txtARVDate.Enabled = false;
            txtARVType.Enabled = false;

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region frmCTCPreART_Load
        private void frmCTCPreART_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCPreART_Load";

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

        #region frmCTCPreART_Shown
        private void frmCTCPreART_Shown(object sender, EventArgs e)
        {
            this.Data_Clear();

            if (pSelectedPatient != null)
            {
                this.Data_Display(pSelectedPatient);
            }

            cboSearchBy.SelectedIndex = 0;
        }
        #endregion

        #region frmCTCPreART_FormClosing
        private void frmCTCPreART_FormClosing(object sender, FormClosingEventArgs e)
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
                #region refered from

                DataTable mDtReferedFrom = pMdtReporter.View_LookupData("ctc_referedfrom", "code,description", "", "", Program.gLanguageName, "");

                ComboBoxItemCollection mItems = cboReferedFrom.Properties.Items;

                foreach (DataRow mDataRow in mDtReferedFrom.Rows)
                {
                    mItems.Add(mDataRow["description"].ToString());
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

        #region Data_Display
        private void Data_Display(AfyaPro_Types.clsCtcClient mPatient)
        {
            string mFunctionName = "Data_Display";

            try
            {
                this.pSearchingPatient = false;

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

                    #region logic new/update

                    //htc#
                    if (mDataRow["htcno"].ToString().Trim() == "")
                    {
                        Int16 mGenerateCode = pMdtAutoCodes.Auto_Generate_Code(
                            Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.vctnumbers));
                        if (mGenerateCode == -1)
                        {
                            Program.Display_Server_Error("");
                            return;
                        }

                        txtHTCNo.Text = "";
                        if (mGenerateCode == 1)
                        {
                            txtHTCNo.Text = "<<---New--->>";
                        }
                    }
                    else
                    {
                        txtHTCNo.Text = mDataRow["htcno"].ToString();
                    }

                    #endregion
                }

                #endregion

                #region ctc_preart

                DataTable mDtPreART = pMdtReporter.View_LookupData("view_ctc_preart", "*", "patientcode='" + mPatient.code.Trim() + "'", "", "", "");

                if (mDtPreART.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtPreART.Rows[0];

                    txtTransferInDate.Text = mDataRow["transferindate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["transferindate"]).ToString("d");
                    txtHIVCareClinicNo.Text = mDataRow["hivcareclinicno"].ToString();
                    txtConfirmSite.Text = mDataRow["confirmsite"].ToString();
                    txtConfirmDate.Text = mDataRow["confirmdate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["confirmdate"]).ToString("d");
                    cboConfirmTestType.SelectedIndex = Convert.ToInt32(mDataRow["confirmtesttype"]);
                    cboClinicalStage.Text = mDataRow["clinicalstage"].ToString();
                    txtClinicalConditions.Text = mDataRow["clinicalconditions"].ToString();
                    txtDateOfInitiation.Text = mDataRow["dateofinitiation"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["dateofinitiation"]).ToString("d");
                    chkTakenIPT.Checked = Convert.ToBoolean(mDataRow["takenipt"]);
                    cboModeOfEntry.Text = mDataRow["modeofentry"].ToString();

                    if (chkTakenIPT.Checked)
                    {
                        txtIPTDate.Text = mDataRow["iptdate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["iptdate"]).ToString("d");
                        txtIPTDate.Enabled = true;
                    }
                    else
                    {
                        txtIPTDate.Text = "";
                        txtIPTDate.Enabled = false;
                    }
                    chkTakenTBTreatment.Checked = Convert.ToBoolean(mDataRow["takentbtreatment"]);
                    if (chkTakenTBTreatment.Checked)
                    {
                        txtTBTreatmentDate.Text = mDataRow["tbtreatmentdate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["tbtreatmentdate"]).ToString("d");
                        txtTBRegNo.Text = mDataRow["tbregno"].ToString();
                    }
                    else
                    {
                        txtTBTreatmentDate.Text =  "";
                        txtTBRegNo.Text = "";
                        txtTBTreatmentDate.Enabled = false;
                        txtTBRegNo.Enabled = false;
                    }
                    chkTakenARV.Checked = Convert.ToBoolean(mDataRow["takenarv"]);
                    if (chkTakenARV.Checked)
                    {
                        txtARVDate.Text = mDataRow["arvdate"] == DBNull.Value ? "" : Convert.ToDateTime(mDataRow["arvdate"]).ToString("d");
                        txtARVType.Text = mDataRow["arvtype"].ToString();
                        txtARVDate.Enabled = true;
                        txtARVType.Enabled = true;
                    }
                    else
                    {
                         txtARVDate.Text = "";
                         txtARVType.Text = "";
                         txtARVDate.Enabled = false;
                         txtARVType.Enabled = false;
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
            txtHTCNo.Text = "";
            txtTransferInDate.EditValue = null;
            txtHIVCareClinicNo.Text = "";
            txtConfirmSite.Text = "";
            txtConfirmDate.EditValue = null;
            cboConfirmTestType.SelectedIndex = -1;
            cboClinicalStage.SelectedIndex = -1;
            txtClinicalConditions.Text = "";
            chkTakenIPT.Checked = false;
            txtIPTDate.EditValue = null;
            chkTakenTBTreatment.Checked = false;
            txtTBTreatmentDate.EditValue = null;
            txtTBRegNo.Text = "";
            chkTakenARV.Checked = false;
            txtARVDate.EditValue = null;
            txtARVType.Text = "";
            
            pPrevPatientId = "";
            pCurrPatientId = pPrevPatientId;

            txtPatientId.Text = "";
            txtGender.Text = "";
            txtMonths.Text = "";
            txtYears.Text  ="";
            txtName.Text ="";
            txtDateOfInitiation.EditValue = null;
            cboModeOfEntry.SelectedIndex = -1;

        }
        #endregion

        #region Ok
        private void Ok()
        {
            Int16 mGenerateHTCNo = 0;

            string mFunctionName = "Ok";

            if (txtHTCNo.Text.Trim() == "" && txtHTCNo.Text.Trim().ToLower() != "<<---new--->>")
            {
                Program.Display_Error("Invalid HTC Serial #");
                txtHTCNo.Focus();
                return;
            }

            if (cboModeOfEntry.SelectedIndex == -1)
            {
                Program.Display_Error("Please select mode of entry", false);
                cboModeOfEntry.Focus();
                return;
            }

            if ((cboModeOfEntry.Text == "TI") && (txtTransferInDate.EditValue == null))
            {
                Program.Display_Error("Please enter transfer in date", false);
                txtTransferInDate.Focus();
                return;
            }

            if ((cboModeOfEntry.Text != "TI") && (txtDateOfInitiation.EditValue == null))
            {
                Program.Display_Error("Please enter date of initiation", false);
                txtDateOfInitiation.Focus();
                return;
            }

            if (cboClinicalStage.SelectedIndex == -1)
            {
                Program.Display_Error("Invalid clinical stage", false);
                cboClinicalStage.Focus();
                return;
            }

            try
            {
                string mModeOfEntry = cboModeOfEntry.Text;

                DateTime? mTransferInDate= null;
                DateTime? mConfirmDate = null;
                DateTime? mIPTDate = null;
                DateTime? mTBTreatmentDate = null;
                DateTime? mARVDate = null;
                DateTime? mInitiationDate = null;

                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                if (txtHTCNo.Text.Trim().ToLower() == "<<---new--->>")
                {
                    mGenerateHTCNo = 1;
                }

                if (Program.IsNullDate(txtTransferInDate.EditValue) == false)
                {
                    mTransferInDate = Convert.ToDateTime(txtTransferInDate.EditValue);
                }

                if (Program.IsNullDate(txtDateOfInitiation.EditValue) == false)
                {
                    mInitiationDate = Convert.ToDateTime(txtDateOfInitiation.EditValue);
                }

                if (Program.IsNullDate(txtConfirmDate.EditValue) == false)
                {
                    mConfirmDate = Convert.ToDateTime(txtConfirmDate.EditValue);
                }

                if (Program.IsDate(txtIPTDate.EditValue) == true)
                {
                    mIPTDate = Convert.ToDateTime(txtIPTDate.EditValue);
                }

                if (Program.IsDate(txtTBTreatmentDate.EditValue) == true)
                {
                    mTBTreatmentDate = Convert.ToDateTime(txtTBTreatmentDate.EditValue);
                }

                if (Program.IsDate(txtARVDate.EditValue) == true)
                {
                    mARVDate = Convert.ToDateTime(txtARVDate.EditValue);
                }

                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Enroll_PreART(
                    txtPatientId.Text,
                    mGenerateHTCNo,
                    txtHTCNo.Text,
                    mTransDate,
                    cboReferedFrom.Text,
                    mTransferInDate,
                    txtHIVCareClinicNo.Text,
                    txtConfirmSite.Text,
                    mConfirmDate,
                    cboConfirmTestType.SelectedIndex,
                    Convert.ToInt32(cboClinicalStage.Text),
                    txtClinicalConditions.Text,
                    Convert.ToInt32(chkTakenIPT.Checked),
                    mIPTDate,
                    Convert.ToInt32(chkTakenTBTreatment.Checked),
                    mTBTreatmentDate,
                    txtTBRegNo.Text,
                    Convert.ToInt32(chkTakenARV.Checked),
                    mARVDate,
                    txtARVType.Text,
                    mModeOfEntry,
                    "Con",
                    mInitiationDate,
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

        #region chkTakenIPTChecked
        private void chkTakenIPTChecked(object sender, EventArgs e)
        {
            if (chkTakenIPT.Checked)
            {
                txtIPTDate.Enabled = true;
            }
            else
            {
                txtIPTDate.Enabled = false;
                txtIPTDate.Text = "";
            }
        }
        #endregion

        #region chkTakenTBTreatmentChecked
        private void chkTakenTBTreatmentChecked(object sender, EventArgs e)
        {
            if (chkTakenTBTreatment.Checked)
            {
                txtTBTreatmentDate.Enabled = true;
                txtTBRegNo.Enabled = true;
            }
            else
            {
               txtTBTreatmentDate.Enabled = false;
               txtTBRegNo.Enabled = false;
               txtTBTreatmentDate.Text = "";
               txtTBRegNo.Text = "";
            }
        }
        #endregion

        #region chkTakenARVChecked
        private void chkTakenARVChecked(object sender, EventArgs e)
        {
            if (chkTakenARV.Checked)
            {
                 txtARVDate.Enabled = true;
                 txtARVType.Enabled = true;
            }
            else
            {
                 txtARVDate.Enabled = false;
                 txtARVType.Enabled = false;
                 txtARVDate.Text = "";
                 txtARVType.Text = "";
            }
        }
        #endregion

        #region dateChange_Click
        private void dateChange_Click(object sender, EventArgs e)
        {
            DateTime mDate = (sender as DevExpress.XtraEditors.DateEdit).DateTime;

            if (mDate > DateTime.Today)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_Invalid_Date_Greater.ToString());
                mDate = DateTime.Today;
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

        #region frmCTCPreART_KeyDown
        void frmCTCPreART_KeyDown(object sender, KeyEventArgs e)
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
                this.Data_Clear();
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

            if (txtSearchText.Text == "")
            {
                Program.Display_Error("Please enter a value for " + cboSearchBy.SelectedItem.ToString()+ "."); 
                return;
            }

            this.pSelectedPatient = this.Search_Patient(mFieldName, txtSearchText.Text);

            if (pSelectedPatient != null)
            {
                if (pSelectedPatient.Exist == false)
                {
                    Program.Display_Info("No records found");
                }
            }
            this.Data_Clear();
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
                this.Data_Clear();
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

            frmCTCPreARTVisits mCTCPreARTVisits = new frmCTCPreARTVisits();
            mCTCPreARTVisits.SelectedPatient = pSelectedPatient;
            mCTCPreARTVisits.ShowDialog();
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

            frmCTCPreARTVisitDetails mCTCPreARTVisitDetails = new frmCTCPreARTVisitDetails();
            mCTCPreARTVisitDetails.SelectedPatient = pSelectedPatient;
            mCTCPreARTVisitDetails.ShowDialog();

            if (mCTCPreARTVisitDetails.RecordSaved == true)
            {
                txtSearchText.Text = "";
                this.Data_Clear();
                txtPatientId.Focus();
            }
        }
        #endregion

        #region txtHTCNo_Leave
        private void txtHTCNo_Leave(object sender, EventArgs e)
        {
            if (txtHTCNo.Text.Trim() == "")
            {
                Int16 mGenerateCode = pMdtAutoCodes.Auto_Generate_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.vctnumbers));
                if (mGenerateCode == -1)
                {
                    return;
                }
                if (mGenerateCode == 1)
                {
                    txtHTCNo.Text = "<<---New--->>";
                }
                else
                {
                    txtHTCNo.Focus();
                }
            }
        }
        #endregion

        #region txtHTCNo_Enter
        private void txtHTCNo_Enter(object sender, EventArgs e)
        {
            if (txtHTCNo.Text.Trim().ToLower() == "<<---new--->>")
            {
                txtHTCNo.Text = "";
            }
            else
            {
                txtHTCNo.SelectAll();
            }
        }
        #endregion
    }
}