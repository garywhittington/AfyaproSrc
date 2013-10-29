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
    public partial class frmCTCPMTCTLabourAndDeliveryVisitDetails : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_MT.clsCTCClients pMdtCTCClients;
        private AfyaPro_MT.clsReporter pMdtReporter;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private AfyaPro_Types.ctcBooking pBooking = new AfyaPro_Types.ctcBooking();

        private AfyaPro_Types.clsCtcClient pSelectedPatient = null;
        internal AfyaPro_Types.clsCtcClient SelectedPatient
        {
            set { pSelectedPatient = value; }
            get { return pSelectedPatient; }
        }

        private DataRow pSelectedDataRow = null;

        private bool pRecordSaved = false;
        internal bool RecordSaved
        {
            set { pRecordSaved = value; }
            get { return pRecordSaved; }
        }

        #endregion

        #region frmCTCPMTCTLabourAndDeliveryVisitDetails
        public frmCTCPMTCTLabourAndDeliveryVisitDetails(DataRow mSelectedDataRow = null)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCPMTCTLabourAndDeliveryVisitDetails";
            this.KeyDown += new KeyEventHandler(frmCTCPMTCTLabourAndDeliveryVisitDetails_KeyDown);

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

        #region frmCTCPMTCTLabourAndDeliveryVisitDetails_Load
        private void frmCTCPMTCTLabourAndDeliveryVisitDetails_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCPMTCTLabourAndDeliveryVisitDetails_Load";

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

        #region frmCTCPMTCTLabourAndDeliveryVisitDetails_Shown
        private void frmCTCPMTCTLabourAndDeliveryVisitDetails_Shown(object sender, EventArgs e)
        {
            txtDate.EditValue = DateTime.Now.Date;

            if (pSelectedPatient != null)
            {
                this.Display_Patient(pSelectedPatient);
            }
        }
        #endregion

        #region frmCTCPMTCTLabourAndDeliveryVisitDetails_FormClosing
        private void frmCTCPMTCTLabourAndDeliveryVisitDetails_FormClosing(object sender, FormClosingEventArgs e)
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
                #region ctc_pmtcthivstatus ANC

                DataTable ctc_pmtcthivstatus1 = pMdtReporter.View_LookupData("ctc_pmtcthivstatus", "code,description", "active=1", "autocode", Program.gLanguageName, "grdCTCPMTCTComb");
                cboHIVStatusANC.Properties.DataSource = ctc_pmtcthivstatus1;
                cboHIVStatusANC.Properties.DisplayMember = "description";
                cboHIVStatusANC.Properties.ValueMember = "code";

                cboHIVStatusANC.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboHIVStatusANC.Properties.BestFit();

                #endregion

                #region ctc_pmtcthivstatus LnD

                DataTable ctc_pmtcthivstatus2 = pMdtReporter.View_LookupData("ctc_pmtcthivstatus", "code,description", "active=1", "autocode", Program.gLanguageName, "grdCTCPMTCTCombLabour");
                cboHIVLnD.Properties.DataSource = ctc_pmtcthivstatus2;
                cboHIVLnD.Properties.DisplayMember = "description";
                cboHIVLnD.Properties.ValueMember = "code";

                cboHIVLnD.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboHIVLnD.Properties.BestFit();

                #endregion

                #region ctc_pmtctcomb

                DataTable ctc_pmtctcomb = pMdtReporter.View_LookupData("ctc_pmtctcomb", "code,description", "active=1", "autocode", Program.gLanguageName, "grdCTCPMTCTComb");
                cboARVANC.Properties.DataSource = ctc_pmtctcomb;
                cboARVANC.Properties.DisplayMember = "description";
                cboARVANC.Properties.ValueMember = "code";

                cboARVANC.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboARVANC.Properties.BestFit();

                #endregion

                #region ctc_pmtctcomblabour

                DataTable ctc_pmtctcomblabour = pMdtReporter.View_LookupData("ctc_pmtctcomblabour", "code,description", "active=1", "autocode", Program.gLanguageName, "grdCTCPMTCTCombLabour");
                cboARVLabour.Properties.DataSource = ctc_pmtctcomblabour;
                cboARVLabour.Properties.DisplayMember = "description";
                cboARVLabour.Properties.ValueMember = "code";

                cboARVLabour.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboARVLabour.Properties.BestFit();

                #endregion

                #region ctc_pmtctinfantdosereceived

                DataTable ctc_pmtctinfantdosereceived = pMdtReporter.View_LookupData("ctc_pmtctinfantdosereceived", "code,description", "active=1", "autocode", Program.gLanguageName, "grdCTCPMTCTCombLabour");
                cboInfantReceived.Properties.DataSource = ctc_pmtctinfantdosereceived;
                cboInfantReceived.Properties.DisplayMember = "description";
                cboInfantReceived.Properties.ValueMember = "code";

                cboInfantReceived.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboInfantReceived.Properties.BestFit();

                #endregion

                #region ctc_pmtctinfantdosedispensed

                DataTable ctc_pmtctinfantdosedispensed = pMdtReporter.View_LookupData("ctc_pmtctinfantdosedispensed", "code,description", "active=1", "autocode", Program.gLanguageName, "grdCTCPMTCTCombLabour");
                cboInfantDispensed.Properties.DataSource = ctc_pmtctinfantdosedispensed;
                cboInfantDispensed.Properties.DisplayMember = "description";
                cboInfantDispensed.Properties.ValueMember = "code";

                cboInfantDispensed.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboInfantDispensed.Properties.BestFit();

                #endregion

                #region ctc_pmtctinfantfeeding

                DataTable ctc_pmtctinfantfeeding = pMdtReporter.View_LookupData("ctc_pmtctinfantfeeding", "code,description", "active=1", "autocode", Program.gLanguageName, "grdCTCPMTCTCombLabour");
                cboInfantFeeding.Properties.DataSource = ctc_pmtctinfantfeeding;
                cboInfantFeeding.Properties.DisplayMember = "description";
                cboInfantFeeding.Properties.ValueMember = "code";

                cboInfantFeeding.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                cboInfantFeeding.Properties.BestFit();

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
                    //cmdOk.Enabled = false;
                }

                #endregion

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
                txtAdmissionDate.EditValue = null;
                if (pSelectedDataRow["admissiondate"] != DBNull.Value)
                {
                    txtAdmissionDate.EditValue = Convert.ToDateTime(pSelectedDataRow["admissiondate"]);
                }
                cboHIVStatusANC.ItemIndex = Program.Get_LookupItemIndex(cboHIVStatusANC, "code", pSelectedDataRow["hivstatusfromanc"].ToString());
                cboHIVLnD.ItemIndex = Program.Get_LookupItemIndex(cboHIVLnD, "code", pSelectedDataRow["hivresultlnd"].ToString());
                cboARVANC.ItemIndex = Program.Get_LookupItemIndex(cboARVANC, "code", pSelectedDataRow["arvduringanc"].ToString());
                cboARVLabour.ItemIndex = Program.Get_LookupItemIndex(cboARVLabour, "code", pSelectedDataRow["arvduringlabour"].ToString());
                cboInfantReceived.ItemIndex = Program.Get_LookupItemIndex(cboInfantReceived, "code", pSelectedDataRow["infantdosereceived"].ToString());
                cboInfantDispensed.ItemIndex = Program.Get_LookupItemIndex(cboInfantDispensed, "code", pSelectedDataRow["infantdosedispensed"].ToString());
                cboInfantFeeding.ItemIndex = Program.Get_LookupItemIndex(cboInfantFeeding, "code", pSelectedDataRow["infantfeeding"].ToString());
                cboLinkage.SelectedIndex = Convert.ToInt32(pSelectedDataRow["linkage"]);
                txtRemarks.Text = pSelectedDataRow["remarks"] == DBNull.Value ? "" : pSelectedDataRow["remarks"].ToString();
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

                DateTime? mAdmissionDate = null;
                string mHIVANC = "";
                string mHIVLnD = "";
                string mARVANC = "";
                string mARVLabour = "";
                string mInfantReceived = "";
                string mInfantDispensed = "";
                string mInfantFeeding = "";

                if (Program.IsDate(txtAdmissionDate.EditValue) == true)
                {
                    mAdmissionDate = Convert.ToDateTime(txtAdmissionDate.EditValue);
                }

                if (cboHIVStatusANC.ItemIndex >= 0)
                {
                    mHIVANC = cboHIVStatusANC.GetColumnValue("code").ToString();
                }

                if (cboHIVLnD.ItemIndex >= 0)
                {
                    mHIVLnD = cboHIVLnD.GetColumnValue("code").ToString();
                }

                if (cboARVANC.ItemIndex >= 0)
                {
                    mARVANC = cboARVANC.GetColumnValue("code").ToString();
                }

                if (cboARVLabour.ItemIndex >= 0)
                {
                    mARVLabour = cboARVLabour.GetColumnValue("code").ToString();
                }

                if (cboInfantReceived.ItemIndex >= 0)
                {
                    mInfantReceived = cboInfantReceived.GetColumnValue("code").ToString();
                }

                if (cboInfantDispensed.ItemIndex >= 0)
                {
                    mInfantDispensed = cboInfantDispensed.GetColumnValue("code").ToString();
                }

                if (cboInfantFeeding.ItemIndex >= 0)
                {
                    mInfantFeeding = cboInfantFeeding.GetColumnValue("code").ToString();
                }

                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Add_PMTCTLabourAndDelivery(
                    txtPatientId.Text,
                    mTransDate,
                    mAdmissionDate,
                    mHIVANC,
                    mHIVLnD,
                    mARVANC,
                    mARVLabour,
                    mInfantReceived,
                    mInfantDispensed,
                    mInfantFeeding,
                    cboLinkage.SelectedIndex,
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

                DateTime? mAdmissionDate = null;
                string mHIVANC = "";
                string mHIVLnD = "";
                string mARVANC = "";
                string mARVLabour = "";
                string mInfantReceived = "";
                string mInfantDispensed = "";
                string mInfantFeeding = "";

                if (Program.IsDate(txtAdmissionDate.EditValue) == true)
                {
                    mAdmissionDate = Convert.ToDateTime(txtAdmissionDate.EditValue);
                }

                if (cboHIVStatusANC.ItemIndex >= 0)
                {
                    mHIVANC = cboHIVStatusANC.GetColumnValue("code").ToString();
                }

                if (cboHIVLnD.ItemIndex >= 0)
                {
                    mHIVLnD = cboHIVLnD.GetColumnValue("code").ToString();
                }

                if (cboARVANC.ItemIndex >= 0)
                {
                    mARVANC = cboARVANC.GetColumnValue("code").ToString();
                }

                if (cboARVLabour.ItemIndex >= 0)
                {
                    mARVLabour = cboARVLabour.GetColumnValue("code").ToString();
                }

                if (cboInfantReceived.ItemIndex >= 0)
                {
                    mInfantReceived = cboInfantReceived.GetColumnValue("code").ToString();
                }

                if (cboInfantDispensed.ItemIndex >= 0)
                {
                    mInfantDispensed = cboInfantDispensed.GetColumnValue("code").ToString();
                }

                if (cboInfantFeeding.ItemIndex >= 0)
                {
                    mInfantFeeding = cboInfantFeeding.GetColumnValue("code").ToString();
                }

                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Edit_PMTCTLabourAndDelivery(
                    Convert.ToInt32(pSelectedDataRow["autocode"]),
                    mTransDate,
                    mAdmissionDate,
                    mHIVANC,
                    mHIVLnD,
                    mARVANC,
                    mARVLabour,
                    mInfantReceived,
                    mInfantDispensed,
                    mInfantFeeding,
                    cboLinkage.SelectedIndex,
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

        #region frmCTCPMTCTLabourAndDeliveryVisitDetails_KeyDown
        void frmCTCPMTCTLabourAndDeliveryVisitDetails_KeyDown(object sender, KeyEventArgs e)
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
    }
}