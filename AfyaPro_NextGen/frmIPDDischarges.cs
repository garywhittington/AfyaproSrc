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
using DevExpress.XtraLayout;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.IO;
using System.Xml.Serialization;

namespace AfyaPro_NextGen
{
    public partial class frmIPDDischarges : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsDischargeStatus pMdtDischargeStatus;
        private AfyaPro_MT.clsRegistrations pMdtRegistrations;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private AfyaPro_Types.clsPatient pCurrentPatient;
        private AfyaPro_Types.clsBooking pCurrentBooking = new AfyaPro_Types.clsBooking();
        private AfyaPro_Types.clsAdmission pCurrentAdmission = new AfyaPro_Types.clsAdmission();

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private bool pCheckedCheckBox = false;
        private bool pCheckedGrid = false;

        private string pCurrPatientId = "";
        private string pPrevPatientId = "";
        private bool pSearchingPatient = false;

        private DataTable pDtDischargeStatus = new DataTable("dischargestatus");

        internal int gAdmissionId = 0;
        internal string gWardCode = "";
        internal string gRoomCode = "";
        internal string gBedNo = "";
        internal string gPriceCategory = "";
        internal string gRemarks = "";
        internal bool gBillingOk = false;
        internal bool gDiagnosisOk = false;
        internal bool gDiagnosisDone = false;
        internal bool gBookingOk = false;
        internal bool gBillingDone = false;

        #endregion

        #region frmIPDDischarges
        public frmIPDDischarges()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmIPDDischarges";

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                pMdtRegistrations = (AfyaPro_MT.clsRegistrations)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRegistrations),
                    Program.gMiddleTier + "clsRegistrations");

                pMdtDischargeStatus = (AfyaPro_MT.clsDischargeStatus)Activator.GetObject(
                    typeof(AfyaPro_MT.clsDischargeStatus),
                    Program.gMiddleTier + "clsDischargeStatus");

                pDtDischargeStatus.Columns.Add("code", typeof(System.String));
                pDtDischargeStatus.Columns.Add("description", typeof(System.String));
                cboDischargeStatus.Properties.DataSource = pDtDischargeStatus;
                cboDischargeStatus.Properties.DisplayMember = "description";
                cboDischargeStatus.Properties.ValueMember = "code";

                this.Fill_LookupData();

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ipddischarges_customizelayout.ToString());

                chkCharge.Checked = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ipddischarges_promptforbilling.ToString());

                chkDiagnosis.Checked = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ipddischarges_promptfordiagnosis.ToString());

                chkCharge.Properties.ReadOnly = !Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ipddischarges_changechargestatus.ToString());

                chkDiagnosis.Properties.ReadOnly = !Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ipddischarges_changepromptfordiagnosis.ToString());
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
            List<Object> mObjectsList = new List<object>();

            mObjectsList.Add(txbPatientId);
            mObjectsList.Add(txbName);
            mObjectsList.Add(txbBirthDate);
            mObjectsList.Add(txbYears);
            mObjectsList.Add(txbMonths);
            mObjectsList.Add(txbGender);
            mObjectsList.Add(txbGender);
            mObjectsList.Add(grpCurrent);
            mObjectsList.Add(txbCurrWard);
            mObjectsList.Add(txbCurrRoom);
            mObjectsList.Add(txbCurrBedNo);
            mObjectsList.Add(txbDischargeStatus);
            mObjectsList.Add(txbRemarks);
            mObjectsList.Add(cmdSearch);
            mObjectsList.Add(cmdOk);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
            this.Data_Clear();
        }

        #endregion

        #region frmBILPayBillsPatients_Load
        private void frmBILPayBillsPatients_Load(object sender, EventArgs e)
        {
            this.Top = 0;

            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            this.Load_Controls();

            txbBirthDateFormat.Text = "(" + Program.gCulture.DateTimeFormat.ShortDatePattern + ")";

            this.pFormWidth = this.Width;
            this.pFormHeight = this.Height;

            Program.Center_Screen(this);
        }
        #endregion

        #region frmBILPayBillsPatients_FormClosing
        private void frmBILPayBillsPatients_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //layout
                if (layoutControl1.IsModified == true)
                {
                    Program.Save_FormLayout(this, layoutControl1, this.Name);
                }
            }
            catch { }
        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            DataRow mNewRow;
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region DischargeStatus

                pDtDischargeStatus.Rows.Clear();
                DataTable mDtDischargeStatus = pMdtDischargeStatus.View("", "code", Program.gLanguageName, "grdIPDDischargeStatus");
                foreach (DataRow mDataRow in mDtDischargeStatus.Rows)
                {
                    mNewRow = pDtDischargeStatus.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtDischargeStatus.Rows.Add(mNewRow);
                    pDtDischargeStatus.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtDischargeStatus.Columns)
                {
                    mDataColumn.Caption = mDtDischargeStatus.Columns[mDataColumn.ColumnName].Caption;
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
            txtName.Text = "";
            txtGender.Text = "";
            txtBirthDate.Text = "";
            txtYears.Text = "";
            txtMonths.Text = "";

            txtCurrWard.Text = "";
            txtCurrRoom.Text = "";
            txtCurrBedNo.Text = "";
            cboDischargeStatus.EditValue = null;
            txtRemarks.Text = "";

            gAdmissionId = 0;
            gWardCode = "";
            gRoomCode = "";

            pPrevPatientId = "";
            pCurrPatientId = pPrevPatientId;

            txtPatientId.Focus();
        }
        #endregion

        #region Data_Display
        internal void Data_Display(AfyaPro_Types.clsPatient mPatient)
        {
            String mFunctionName = "Data_Display";

            try
            {
                this.Data_Clear();

                if (mPatient != null)
                {
                    if (mPatient.Exist == true)
                    {
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
                        txtBirthDate.Text = mPatient.birthdate.ToString("d");
                        int mDays = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue).Subtract(mPatient.birthdate).Days;
                        int mYears = (int)mDays / 365;
                        int mMonths = (int)(mDays % 365) / 30;

                        txtYears.Text = mYears.ToString();
                        txtMonths.Text = mMonths.ToString();

                        #region get current admission

                        pCurrentBooking = pMdtRegistrations.Get_Booking(txtPatientId.Text);

                        if (pCurrentBooking != null)
                        {
                            if (pCurrentBooking.IsBooked == true)
                            {
                                gPriceCategory = pCurrentBooking.PriceName;

                                pCurrentAdmission = pMdtRegistrations.Get_Admission(
                                    pCurrentBooking.Booking, txtPatientId.Text);

                                if (pCurrentAdmission != null)
                                {
                                    AfyaPro_Types.clsDischarge mDischargeDetails = pMdtRegistrations.Get_Discharge(
                                        pCurrentBooking.Booking, txtPatientId.Text, pCurrentAdmission.WardCode,
                                        pCurrentAdmission.RoomCode);

                                    if (pCurrentAdmission.IsAdmitted == true && mDischargeDetails.IsDischarged == false)
                                    {
                                        txtCurrWard.Text = pCurrentAdmission.WardDescription;
                                        txtCurrRoom.Text = pCurrentAdmission.RoomDescription;
                                        txtCurrBedNo.Text = pCurrentAdmission.BedDescription;

                                        gAdmissionId = pCurrentAdmission.AdmissionId;
                                        gWardCode = pCurrentAdmission.WardCode;
                                        gRoomCode = pCurrentAdmission.RoomCode;
                                        gBedNo = pCurrentAdmission.BedCode;
                                    }
                                }
                            }
                        }

                        #endregion

                        pCurrPatientId = mPatient.code;
                        pPrevPatientId = pCurrPatientId;

                        System.Media.SystemSounds.Beep.Play();
                    }
                    else
                    {
                        pCurrPatientId = txtPatientId.Text;
                        pPrevPatientId = pCurrPatientId;
                    }
                }
                else
                {
                    pCurrPatientId = txtPatientId.Text;
                    pPrevPatientId = pCurrPatientId;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Search_Patient
        private AfyaPro_Types.clsPatient Search_Patient()
        {
            string mFunctionName = "Search_Patient";

            try
            {
                pCurrentPatient = pMdtRegistrations.Get_Patient(txtPatientId.Text);
                return pCurrentPatient;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
        }
        #endregion

        #region txtPatientId_KeyDown
        private void txtPatientId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.pCurrentPatient = this.Search_Patient();
                this.Data_Display(pCurrentPatient);
            }
        }
        #endregion

        #region txtPatientId_Leave
        private void txtPatientId_Leave(object sender, EventArgs e)
        {
            pCurrPatientId = txtPatientId.Text;

            if (pCurrPatientId.Trim().ToLower() != pPrevPatientId.Trim().ToLower())
            {
                this.pCurrentPatient = this.Search_Patient();
                this.Data_Display(pCurrentPatient);
            }
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region cmdSave_Click
        private void cmdSave_Click(object sender, EventArgs e)
        {
            AfyaPro_Types.clsBill mBill = new AfyaPro_Types.clsBill();

            string mFunctionName = "cmdSave_Click";

            #region validation

            if (Program.IsDate(Program.gMdiForm.txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                return;
            }

            if (txtPatientId.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoIsInvalid.ToString());
                txtPatientId.Focus();
                return;
            }

            AfyaPro_Types.clsPatient mPatient = pMdtRegistrations.Get_Patient(txtPatientId.Text);
            if (mPatient.Exist == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoDoesNotExist.ToString());
                txtPatientId.Focus();
                return;
            }

            if (pCurrentAdmission.IsAdmitted == false)
            {
                 
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientIsNotAdmitted.ToString());
                return;
            }
            else
            {
                AfyaPro_Types.clsDischarge mDischargeDetails = pMdtRegistrations.Get_Discharge(
                    pCurrentBooking.Booking, txtPatientId.Text, pCurrentAdmission.WardCode,
                    pCurrentAdmission.RoomCode);

                if (mDischargeDetails.IsDischarged == true)
                {
                   
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientIsNotAdmitted.ToString());
                    return;
                }
            }

            if (cboDischargeStatus.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_DischargeStatusDescriptionIsInvalid.ToString());
                cboDischargeStatus.Focus();
                return;
            }

            #endregion
          
            try
            {
                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                gBillingOk = true;
                bool mDischargeIsDone = false;

                #region This will do diagnosis + discharge

                if (chkDiagnosis.Checked == true && chkCharge.Checked == false)
                {
                    gDiagnosisOk = false;

                    #region diagnosis and discharge together

                    frmDXTEpisodeVisitDetails mDXTEpisodeVisitDetails = new frmDXTEpisodeVisitDetails();
                    mDXTEpisodeVisitDetails.gIpdDischarge = this;

                    mDXTEpisodeVisitDetails.gCurrentBooking.BillingGroupCode = pCurrentBooking.BillingGroupCode;
                    mDXTEpisodeVisitDetails.gCurrentBooking.BillingGroupDescription = pCurrentBooking.BillingGroupDescription;
                    mDXTEpisodeVisitDetails.gCurrentBooking.BillingSubGroupCode = pCurrentBooking.BillingSubGroupCode;
                    mDXTEpisodeVisitDetails.gCurrentBooking.BillingSubGroupDescription = pCurrentBooking.BillingSubGroupDescription;
                    mDXTEpisodeVisitDetails.gCurrentBooking.BillingGroupMembershipNo = pCurrentBooking.BillingGroupMembershipNo;
                    mDXTEpisodeVisitDetails.gCurrentBooking.PatientCode = txtPatientId.Text.Trim();
                    mDXTEpisodeVisitDetails.gCurrentBooking.IsBooked = true;
                    mDXTEpisodeVisitDetails.gCurrentBooking.BookDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);
                    mDXTEpisodeVisitDetails.gCharge = false;
                    mDXTEpisodeVisitDetails.gCurrentPatient = pCurrentPatient;

                    mDXTEpisodeVisitDetails.ShowDialog();

                    #endregion

                    gDiagnosisDone = true;

                    if (gDiagnosisOk == false)
                    {
                        return;
                    }

                    gBookingOk = true;

                    mDischargeIsDone = true;
                }

                #endregion

                #region This will do discharge + billing

                else if (chkDiagnosis.Checked == false && chkCharge.Checked == true)
                {
                    if (chkCharge.Checked == true)
                    {
                        gBillingOk = false;

                        #region billing and discharge together

                        frmBILBillPosting mBILBillPosting = new frmBILBillPosting();
                        mBILBillPosting.gIpdDischarge = this;

                        mBILBillPosting.gCurrentBooking.BillingGroupCode = pCurrentBooking.BillingGroupCode;
                        mBILBillPosting.gCurrentBooking.BillingGroupDescription = pCurrentBooking.BillingGroupDescription;
                        mBILBillPosting.gCurrentBooking.BillingSubGroupCode = pCurrentBooking.BillingSubGroupCode;
                        mBILBillPosting.gCurrentBooking.BillingSubGroupDescription = pCurrentBooking.BillingSubGroupDescription;
                        mBILBillPosting.gCurrentBooking.BillingGroupMembershipNo = pCurrentBooking.BillingGroupMembershipNo;
                        mBILBillPosting.gCurrentBooking.PatientCode = txtPatientId.Text.Trim();
                        mBILBillPosting.gCurrentBooking.IsBooked = true;
                        mBILBillPosting.gCurrentBooking.BookDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);
                        mBILBillPosting.gCurrentPatient = pCurrentPatient;

                        mBILBillPosting.gCurrentBooking.PriceName = Program.gCurrentUser.DefaultPriceCategoryCode;

                        mBILBillPosting.ShowDialog();

                        #endregion

                        gBillingDone = true;
                    }

                    if (gBillingOk == false)
                    {
                        return;
                    }

                    gBookingOk = true;

                    mDischargeIsDone = true;
                }

                #endregion

                #region This will do diagnosis + discharge + billing

                else if (chkDiagnosis.Checked == true && chkCharge.Checked == true)
                {
                    if (chkCharge.Checked == true)
                    {
                        gBillingOk = false;

                        #region billing + diagnosis + discharge

                        frmDXTEpisodeVisitDetails mDXTEpisodeVisitDetails = new frmDXTEpisodeVisitDetails();
                        mDXTEpisodeVisitDetails.gIpdDischarge = this;

                        mDXTEpisodeVisitDetails.gCurrentBooking.BillingGroupCode = pCurrentBooking.BillingGroupCode;
                        mDXTEpisodeVisitDetails.gCurrentBooking.BillingGroupDescription = pCurrentBooking.BillingGroupDescription;
                        mDXTEpisodeVisitDetails.gCurrentBooking.BillingSubGroupCode = pCurrentBooking.BillingSubGroupCode;
                        mDXTEpisodeVisitDetails.gCurrentBooking.BillingSubGroupDescription = pCurrentBooking.BillingSubGroupDescription;
                        mDXTEpisodeVisitDetails.gCurrentBooking.BillingGroupMembershipNo = pCurrentBooking.BillingGroupMembershipNo;
                        mDXTEpisodeVisitDetails.gCurrentBooking.PatientCode = txtPatientId.Text.Trim();
                        mDXTEpisodeVisitDetails.gCurrentBooking.IsBooked = true;
                        mDXTEpisodeVisitDetails.gCurrentBooking.BookDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);
                        mDXTEpisodeVisitDetails.gCharge = true;
                        mDXTEpisodeVisitDetails.gCurrentPatient = pCurrentPatient;

                        mDXTEpisodeVisitDetails.ShowDialog();

                        #endregion

                        gBillingDone = true;
                    }

                    if (gBillingOk == false)
                    {
                        return;
                    }

                    gBookingOk = true;

                    mDischargeIsDone = true;
                }

                #endregion

                if (mDischargeIsDone == false)
                {
                    pCurrentBooking = pMdtRegistrations.IPD_Discharge(gAdmissionId, mTransDate, txtPatientId.Text,
                                    pCurrentAdmission.WardCode, pCurrentAdmission.RoomCode, pCurrentAdmission.BedCode,
                                    cboDischargeStatus.GetColumnValue("code").ToString().Trim(),
                                    cboDischargeStatus.GetColumnValue("description").ToString().Trim(),
                                    txtRemarks.Text, Program.gCurrentUser.Code);
                    if (pCurrentBooking.Exe_Result == 0)
                    {
                        Program.Display_Error(pCurrentBooking.Exe_Message);
                        return;
                    }
                    if (pCurrentBooking.Exe_Result == -1)
                    {
                        Program.Display_Server_Error(pCurrentBooking.Exe_Message);
                        return;
                    }

                    mDischargeIsDone = true;
                }

                if (mDischargeIsDone == true)
                {
                    if (Program.GrantDeny_FunctionAccess(AfyaPro_Types.clsSystemFunctions.FunctionKeys.ipddischarges_showsuccessmessage.ToString()) == true)
                    {
                        Program.Display_Info(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_DischargeSuccess.ToString());
                    }

                    if (Program.GrantDeny_FunctionAccess(AfyaPro_Types.clsSystemFunctions.FunctionKeys.ipddischarges_closeafterdischarge.ToString()) == true)
                    {
                        this.Close();
                    }
                    else
                    {
                        txtPatientId.Text = "";
                        this.Data_Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdSearch_Click
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            pSearchingPatient = true;

            frmSearchPatient mSearchPatient = new frmSearchPatient(txtPatientId);
            mSearchPatient.ShowDialog();

            pSearchingPatient = false;
        }
        #endregion

        #region txtPatientId_EditValueChanged
        private void txtPatientId_EditValueChanged(object sender, EventArgs e)
        {
            if (pSearchingPatient == true)
            {
                this.pCurrentPatient = this.Search_Patient();
                this.Data_Display(pCurrentPatient);
            }
        }
        #endregion
    }
}