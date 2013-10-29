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
    public partial class frmCTCPCRTestDetails : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsReporter pMdtReporter;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_MT.clsCTCClients pMdtCTCClients;
        private AfyaPro_MT.clsRegistrations pMdtRegistrations;
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

        private bool pRecordSaved = false;
        internal bool RecordSaved
        {
            set { pRecordSaved = value; }
            get { return pRecordSaved; }
        }

        private DataRow pSelectedDataRow = null;

        private DataTable pDtHIVTeferals = new DataTable("referals");

        #endregion

        #region frmCTCPCRTestDetails
        public frmCTCPCRTestDetails(DataRow mSelectedDataRow = null)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCPCRTestDetails";
            this.KeyDown += new KeyEventHandler(frmCTCPCRTestDetails_KeyDown);

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

                pMdtRegistrations = (AfyaPro_MT.clsRegistrations)Activator.GetObject(
                   typeof(AfyaPro_MT.clsRegistrations),
                   Program.gMiddleTier + "clsRegistrations");

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpcrtests_customizelayout.ToString());

                pDtHIVTeferals.Columns.Add("code", typeof(System.String));

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

        #region frmCTCPCRTestDetails_Load
        private void frmCTCPCRTestDetails_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCPCRTestDetails_Load";

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

        #region frmCTCPCRTestDetails_Shown
        private void frmCTCPCRTestDetails_Shown(object sender, EventArgs e)
        {
            this.Data_Clear();

            if (pSelectedPatient != null)
            {
                this.Display_Patient(pSelectedPatient);
            }
        }
        #endregion

        #region frmCTCPCRTestDetails_FormClosing
        private void frmCTCPCRTestDetails_FormClosing(object sender, FormClosingEventArgs e)
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
                #region reasons

                DataTable mDtReasons = pMdtReporter.View_LookupData("ctc_pcrtestreasons", "code,description", "active=1", "", Program.gLanguageName, "grdCTCHIVTestReasons");
                cboReason.Properties.DataSource = mDtReasons;
                cboReason.Properties.DisplayMember = "description";
                cboReason.Properties.ValueMember = "code";

                cboReason.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboReason.Properties.BestFit();

                #endregion

                #region counsellors

                DataTable mDtCounsellors = pMdtReporter.View_LookupData("facilitystaffs", "code,description,treatmentpointcode",
                    "category=" + Convert.ToInt16(AfyaPro_Types.clsEnums.StaffCategories.Counsellors),
                    "description", Program.gLanguageName, "grdGENMedicalStaffs");

                cboCounsellor.Properties.DataSource = mDtCounsellors;
                cboCounsellor.Properties.DisplayMember = "description";
                cboCounsellor.Properties.ValueMember = "code";

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
                    txtMotherClientNo.Text = mDataRow["motherclientno"].ToString();
                }
                #endregion

                cboCounsellor.EditValue = null;
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
                if (Program.IsNullDate(pSelectedDataRow["sampledate"]) == false)
                {
                    txtSampleDate.EditValue = Convert.ToDateTime(pSelectedDataRow["sampledate"]);
                }
                chkBreastFeeding.Checked = Convert.ToBoolean(pSelectedDataRow["breastfeeding"]);
                cboResultGiven.SelectedIndex = Convert.ToInt32(pSelectedDataRow["testresult"]);
                if (pSelectedDataRow["testresultdate"] != DBNull.Value)
                {
                    txtResultGivenDate.EditValue = Convert.ToDateTime(pSelectedDataRow["testresultdate"]);
                }
                if (pSelectedDataRow["testresultrcvddate"] != DBNull.Value)
                {
                    txtResultReceivedDate.EditValue = Convert.ToDateTime(pSelectedDataRow["testresultrcvddate"]);
                }
                chkGivenCPT.Checked = Convert.ToBoolean(pSelectedDataRow["cptgiven"]);
                txtComments.Text = pSelectedDataRow["comments"].ToString().Trim();
                cboReason.ItemIndex = Program.Get_LookupItemIndex(cboReason, "code", pSelectedDataRow["reasoncode"].ToString().Trim());
                cboCounsellor.ItemIndex = Program.Get_LookupItemIndex(cboCounsellor, "code", pSelectedDataRow["counsellorcode"].ToString().Trim());
                txtNextApptDate.EditValue = "";
                if (pSelectedDataRow["nextapptdate"] != DBNull.Value)
                {
                    txtNextApptDate.EditValue = Convert.ToDateTime(pSelectedDataRow["nextapptdate"]);
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
            }
        }
        #endregion

        #region Data_Clear
        private void Data_Clear()
        {
            txtSampleDate.EditValue = null;
            chkBreastFeeding.Checked = false;
            cboReason.EditValue = null;
            cboResultGiven.SelectedIndex = 0;
            txtResultGivenDate.EditValue = null;
            txtComments.Text = "";
            txtNextApptDate.EditValue = null;
            chkGivenCPT.Checked = false;
            txtResultReceivedDate.EditValue = null;
            txtMotherClientNo.Text = "";
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

            this.Data_Clear();
        }
        #endregion

        #region Data_New
        private void Data_New()
        {
            string mFunctionName = "Data_New";

            if (Program.IsDate(txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                txtDate.Focus();
                return;
            }
            if (Program.IsNullDate(txtSampleDate.EditValue) == true)
            {
                Program.Display_Error("Invalid date for specimen collection");
                txtSampleDate.Focus();
                return;
            }
            if (cboReason.ItemIndex == -1)
            {
                Program.Display_Error("Invalid reason selection", false);
                cboReason.Focus();
                return;
            }
            if (cboCounsellor.ItemIndex == -1)
            {
                Program.Display_Error("Invalid counsellor selection", false);
                cboCounsellor.Focus();
                return;
            }

            try
            {
                DateTime? mTransDate = null;
                DateTime? mNextApptDate = null;
                DateTime? mResultGivenDate = null;
                DateTime? mResultReceivedDate = null;
                
                string mReasonCode = cboReason.GetColumnValue("code").ToString();
                string mCounsellorCode = cboCounsellor.GetColumnValue("code").ToString();

                mTransDate = Convert.ToDateTime(txtDate.EditValue);

                if (Program.IsDate(txtNextApptDate.EditValue) == true)
                {
                    mNextApptDate = Convert.ToDateTime(txtNextApptDate.EditValue);
                }

                if (Program.IsDate(txtResultGivenDate.EditValue) == true)
                {
                    mResultGivenDate = Convert.ToDateTime(txtResultGivenDate.EditValue);
                }
                if (Program.IsDate(txtResultReceivedDate.EditValue) == true)
                {
                    mResultReceivedDate = Convert.ToDateTime(txtResultReceivedDate.EditValue);
                }

                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Add_PCR(
                    pSelectedPatient.code,
                    mTransDate,
                    Convert.ToDateTime(txtSampleDate.EditValue),
                    mReasonCode,
                    cboResultGiven.SelectedIndex,
                    mResultGivenDate,
                    mResultReceivedDate,
                    Convert.ToInt32(chkGivenCPT.Checked),
                    txtMotherClientNo.Text,
                    txtComments.Text,
                    mCounsellorCode,
                    Convert.ToInt32(chkBreastFeeding.Checked),
                    mNextApptDate,
                    pDtHIVTeferals,
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
            string mFunctionName = "Data_Edit";

            if (Program.IsDate(txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                txtDate.Focus();
                return;
            }
            if (Program.IsNullDate(txtSampleDate.EditValue) == true)
            {
                Program.Display_Error("Invalid date for specimen collection");
                txtSampleDate.Focus();
                return;
            }
            if (cboReason.ItemIndex == -1)
            {
                Program.Display_Error("Invalid reason selection", false);
                cboReason.Focus();
                return;
            }
            if (cboCounsellor.ItemIndex == -1)
            {
                Program.Display_Error("Invalid counsellor selection", false);
                cboCounsellor.Focus();
                return;
            }

            try
            {
                DateTime? mTransDate = null;
                DateTime? mNextApptDate = null;
                DateTime? mResultGivenDate = null;
                DateTime? mResultReceivedDate = null;
                
                string mReasonCode = cboReason.GetColumnValue("code").ToString();
                string mCounsellorCode = cboCounsellor.GetColumnValue("code").ToString();

                mTransDate = Convert.ToDateTime(txtDate.EditValue);

                if (Program.IsDate(txtNextApptDate.EditValue) == true)
                {
                    mNextApptDate = Convert.ToDateTime(txtNextApptDate.EditValue);
                }

                if (Program.IsDate(txtResultGivenDate.EditValue) == true)
                {
                    mResultGivenDate = Convert.ToDateTime(txtResultGivenDate.EditValue);
                }
                if (Program.IsDate(txtResultReceivedDate.EditValue) == true)
                {
                   mResultReceivedDate = Convert.ToDateTime(txtResultReceivedDate.EditValue);
                }
                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Edit_PCR(
                    Convert.ToInt32(pSelectedDataRow["autocode"]),
                    pSelectedPatient.code,
                    mTransDate,
                    Convert.ToDateTime(txtSampleDate.EditValue),
                    mReasonCode,
                    cboResultGiven.SelectedIndex,
                    mResultGivenDate,
                    mResultReceivedDate,
                    Convert.ToInt32(chkGivenCPT.Checked),
                    txtMotherClientNo.Text,
                    txtComments.Text,
                    mCounsellorCode,
                    Convert.ToInt32(chkBreastFeeding.Checked),
                    mNextApptDate,
                    pDtHIVTeferals,
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

        #region frmCTCPCRTestDetails_KeyDown
        void frmCTCPCRTestDetails_KeyDown(object sender, KeyEventArgs e)
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

        #region txtMotherClientNo_Validating
        private void txtMotherClientNo_Validating(object sender, System.ComponentModel.CancelEventArgs  e)
        {
            string mFunctionName = "txtMotherClientNo_Validating";
            AfyaPro_Types.clsPatient mClientMother;

            if (txtMotherClientNo.Text != "")
            {
                try
                {
                    mClientMother = pMdtRegistrations.Get_Patient(txtMotherClientNo.Text);
                    if (mClientMother.Exist == false)
                    {
                        Program.Display_Error(mClientMother.Exe_Message);
                        txtMotherClientNo.Focus();
                        return;
                    }
                    if (mClientMother.gender != "F")
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoIsInvalid.ToString());
                        txtMotherClientNo.Focus();
                        return;                       
                    }
                   
                }
                catch (Exception ex)
                {
                    Program.Display_Error(pClassName, mFunctionName, ex.Message);
                    
                }
            }
        }
        #endregion

        #region cboResultGiven_SelectedIndexChanged
        private void cboResultGiven_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboResultGiven.SelectedIndex == (int)AfyaPro_Types.clsEnums.CTC_PCRTestResultsGiven.Negative)
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

    }
}