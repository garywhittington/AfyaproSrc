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
    public partial class frmIPDAdmit : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsWards pMdtWards;
        private AfyaPro_MT.clsWardRooms pMdtWardRooms;
        private AfyaPro_MT.clsWardRoomBeds pMdtWardRoomBeds;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";
        private DataTable pDtWards = new DataTable("wards");
        private DataTable pDtRooms = new DataTable("rooms");
        private DataTable pDtBeds = new DataTable("beds");

        internal bool gBillingDone = false;
        internal bool gDiagnosisDone = false;
        private DevExpress.XtraEditors.XtraForm pSourceForm;

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        #endregion

        #region frmIPDAdmit
        public frmIPDAdmit(DevExpress.XtraEditors.XtraForm mSourceForm)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmIPDAdmit";
            this.KeyDown += new KeyEventHandler(frmIPDAdmit_KeyDown);

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                
                this.CancelButton = cmdClose;
                this.pSourceForm = mSourceForm;

                pMdtWards = (AfyaPro_MT.clsWards)Activator.GetObject(
                    typeof(AfyaPro_MT.clsWards),
                    Program.gMiddleTier + "clsWards");

                pMdtWardRooms = (AfyaPro_MT.clsWardRooms)Activator.GetObject(
                    typeof(AfyaPro_MT.clsWardRooms),
                    Program.gMiddleTier + "clsWardRooms");

                pMdtWardRoomBeds = (AfyaPro_MT.clsWardRoomBeds)Activator.GetObject(
                    typeof(AfyaPro_MT.clsWardRoomBeds),
                    Program.gMiddleTier + "clsWardRoomBeds");

                pDtWards.Columns.Add("code", typeof(System.String));
                pDtWards.Columns.Add("description", typeof(System.String));
                cboWard.Properties.DataSource = pDtWards;
                cboWard.Properties.DisplayMember = "description";
                cboWard.Properties.ValueMember = "code";

                pDtRooms.Columns.Add("code", typeof(System.String));
                pDtRooms.Columns.Add("description", typeof(System.String));
                cboRoom.Properties.DataSource = pDtRooms;
                cboRoom.Properties.DisplayMember = "description";
                cboRoom.Properties.ValueMember = "code";

                pDtBeds.Columns.Add("code", typeof(System.String));
                pDtBeds.Columns.Add("description", typeof(System.String));
                pDtBeds.Columns.Add("bedstatus", typeof(System.String));
                pDtBeds.Columns.Add("patients", typeof(System.Int32));
                cboBed.Properties.DataSource = pDtBeds;
                cboBed.Properties.DisplayMember = "description";
                cboBed.Properties.ValueMember = "code";

                cboBed.Properties.BestFitMode = BestFitMode.BestFit;
                cboBed.Properties.BestFit();

                this.Fill_LookupData();

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ipdregistrations_customizeadmissionform.ToString());

                chkCharge.Checked = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ipdregistrations_promptforbilling.ToString());

                chkDiagnosis.Checked = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ipdregistrations_promptfordiagnosis.ToString());

                chkCharge.Properties.ReadOnly = !Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ipdregistrations_changechargestatus.ToString());

                chkDiagnosis.Properties.ReadOnly = !Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ipdregistrations_changepromptfordiagnosis.ToString());
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

            mObjectsList.Add(txbWard);
            mObjectsList.Add(txbRoom);
            mObjectsList.Add(txbBedNo);
            mObjectsList.Add(txbRemarks);
            mObjectsList.Add(txbWeight);
            mObjectsList.Add(txbTemperature);
            mObjectsList.Add(cmdOk);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region frmIPDAdmit_Load
        private void frmIPDAdmit_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmIPDAdmit_Load";

            try
            {
                frmIPDRegistrations mIPDRegistrations = (frmIPDRegistrations)this.pSourceForm;

                txtLastVisitDate.EditValue = mIPDRegistrations.gLastAttendanceDate;
                cboWard.ItemIndex = Program.Get_LookupItemIndex(cboWard, "code", mIPDRegistrations.gWardCode);
                cboRoom.ItemIndex = Program.Get_LookupItemIndex(cboRoom, "code", mIPDRegistrations.gRoomCode);
                cboBed.ItemIndex = Program.Get_LookupItemIndex(cboBed, "code", mIPDRegistrations.gBedNo);
                txtWeight.Text = mIPDRegistrations.gWeight.ToString();
                txtTemperature.Text = mIPDRegistrations.gTemperature.ToString();
                txtRemarks.Text = mIPDRegistrations.gRemarks;

                Program.Restore_FormLayout(layoutControl1, this.Name);
                Program.Restore_FormSize(this);

                this.pFormWidth = this.Width;
                this.pFormHeight = this.Height;

                this.Load_Controls();

                Program.Center_Screen(this);

                this.Append_ShortcutKeys();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmIPDAdmit_FormClosing
        private void frmIPDAdmit_FormClosing(object sender, FormClosingEventArgs e)
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
            cmdOk.Text = cmdOk.Text + " (" + Program.KeyCode_Ok.ToString() + ")";
        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            DataRow mNewRow;
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region Wards

                pDtWards.Rows.Clear();
                DataTable mDtWards = pMdtWards.View("", "code", Program.gLanguageName, "grdIPDWards");
                foreach (DataRow mDataRow in mDtWards.Rows)
                {
                    mNewRow = pDtWards.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtWards.Rows.Add(mNewRow);
                    pDtWards.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtWards.Columns)
                {
                    mDataColumn.Caption = mDtWards.Columns[mDataColumn.ColumnName].Caption;
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

        #region cboWard_EditValueChanged
        void cboWard_EditValueChanged(object sender, EventArgs e)
        {
            string mFunctionName = "cboWard_EditValueChanged";

            try
            {
                pDtRooms.Rows.Clear();
                pDtBeds.Rows.Clear();
                if (cboWard.ItemIndex == -1)
                {
                    return;
                }

                string mWardCode = cboWard.GetColumnValue("code").ToString().Trim();

                DataTable mDtRooms = pMdtWardRooms.View(
                    "wardcode='" + mWardCode + "'", "code", Program.gLanguageName, "grdIPDWardRooms");
                foreach (DataRow mDataRow in mDtRooms.Rows)
                {
                    DataRow mNewRow = pDtRooms.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtRooms.Rows.Add(mNewRow);
                    pDtRooms.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtRooms.Columns)
                {
                    mDataColumn.Caption = mDtRooms.Columns[mDataColumn.ColumnName].Caption;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cboRoom_EditValueChanged
        void cboRoom_EditValueChanged(object sender, EventArgs e)
        {
            string mFunctionName = "cboRoom_EditValueChanged";

            try
            {
                pDtBeds.Rows.Clear();
                if (cboWard.ItemIndex == -1 || cboRoom.ItemIndex == -1)
                {
                    return;
                }

                string mWardCode = cboWard.GetColumnValue("code").ToString().Trim();
                string mRoomCode = cboRoom.GetColumnValue("code").ToString().Trim();

                DataTable mDtBeds = pMdtWardRoomBeds.View(
                    "wardcode='" + mWardCode + "' and roomcode='" + mRoomCode + "'", "patients,code", Program.gLanguageName, "grdIPDWardRooms");
                foreach (DataRow mDataRow in mDtBeds.Rows)
                {
                    DataRow mNewRow = pDtBeds.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    mNewRow["bedstatus"] = mDataRow["bedstatus"].ToString();
                    mNewRow["patients"] = mDataRow["patients"];
                    pDtBeds.Rows.Add(mNewRow);
                    pDtBeds.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtBeds.Columns)
                {
                    mDataColumn.Caption = mDtBeds.Columns[mDataColumn.ColumnName].Caption;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Ok
        private void Ok()
        {
            string mFunctionName = "Ok";

            #region validation

            if (cboWard.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_WardDescriptionIsInvalid.ToString());
                cboWard.Focus();
                return;
            }

            if (cboRoom.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_WardRoomDescriptionIsInvalid.ToString());
                cboRoom.Focus();
                return;
            }

            #endregion

            try
            {
                frmIPDRegistrations mIPDRegistrations = (frmIPDRegistrations)pSourceForm;

                mIPDRegistrations.gBedNo = "";

                if (cboBed.ItemIndex != -1)
                {
                    mIPDRegistrations.gBedNo = cboBed.GetColumnValue("code").ToString().Trim();
                }

                mIPDRegistrations.gWardCode = cboWard.GetColumnValue("code").ToString().Trim();
                mIPDRegistrations.gRoomCode = cboRoom.GetColumnValue("code").ToString().Trim();
                mIPDRegistrations.gWeight = 0;
                mIPDRegistrations.gTemperature = 0;

                if (Program.IsMoney(txtWeight.Text) == true)
                {
                    mIPDRegistrations.gWeight = Convert.ToDouble(txtWeight.Text);
                }

                if (Program.IsMoney(txtTemperature.Text) == true)
                {
                    mIPDRegistrations.gTemperature = Convert.ToDouble(txtTemperature.Text);
                }

                gDiagnosisDone = false;
                gBillingDone = false;

                mIPDRegistrations.gBillingOk = true;

                #region This will do diagnosis + admission

                if (chkDiagnosis.Checked == true && chkCharge.Checked == false)
                {
                    mIPDRegistrations.gDiagnosisOk = false;

                    #region diagnosis and admission together

                    frmDXTEpisodeVisitDetails mDXTEpisodeVisitDetails = new frmDXTEpisodeVisitDetails();
                    mDXTEpisodeVisitDetails.gIpdAdmission = mIPDRegistrations;

                    mDXTEpisodeVisitDetails.gCurrentBooking.BillingGroupCode = mIPDRegistrations.gBillingGroupCode;
                    mDXTEpisodeVisitDetails.gCurrentBooking.BillingGroupDescription = mIPDRegistrations.gBillingGroupDescription;
                    mDXTEpisodeVisitDetails.gCurrentBooking.BillingSubGroupCode = mIPDRegistrations.gBillingSubGroupCode;
                    mDXTEpisodeVisitDetails.gCurrentBooking.BillingSubGroupDescription = mIPDRegistrations.gBillingSubGroupDescription;
                    mDXTEpisodeVisitDetails.gCurrentBooking.BillingGroupMembershipNo = mIPDRegistrations.gBillingGroupMembershipNo;
                    mDXTEpisodeVisitDetails.gCurrentBooking.PatientCode = mIPDRegistrations.txtPatientId.Text.Trim();
                    mDXTEpisodeVisitDetails.gCurrentBooking.IsBooked = true;
                    mDXTEpisodeVisitDetails.gCurrentBooking.BookDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);
                    mDXTEpisodeVisitDetails.gCharge = false;
                    mDXTEpisodeVisitDetails.gCurrentPatient = mIPDRegistrations.gCurrentPatient;

                    mDXTEpisodeVisitDetails.ShowDialog();

                    #endregion

                    gDiagnosisDone = true;

                    if (mIPDRegistrations.gDiagnosisOk == false)
                    {
                        return;
                    }

                    mIPDRegistrations.gBookingOk = true;

                    this.Close();
                }

                #endregion

                #region This will do admission + billing

                else if (chkDiagnosis.Checked == false && chkCharge.Checked == true)
                {
                    if (chkCharge.Checked == true)
                    {
                        mIPDRegistrations.gBillingOk = false;

                        #region billing and admission together

                        frmBILBillPosting mBILBillPosting = new frmBILBillPosting();
                        mBILBillPosting.gIpdAdmission = mIPDRegistrations;

                        mBILBillPosting.gCurrentBooking.BillingGroupCode = mIPDRegistrations.gBillingGroupCode;
                        mBILBillPosting.gCurrentBooking.BillingGroupDescription = mIPDRegistrations.gBillingGroupDescription;
                        mBILBillPosting.gCurrentBooking.BillingSubGroupCode = mIPDRegistrations.gBillingSubGroupCode;
                        mBILBillPosting.gCurrentBooking.BillingSubGroupDescription = mIPDRegistrations.gBillingSubGroupDescription;
                        mBILBillPosting.gCurrentBooking.BillingGroupMembershipNo = mIPDRegistrations.gBillingGroupMembershipNo;
                        mBILBillPosting.gCurrentBooking.PatientCode = mIPDRegistrations.txtPatientId.Text.Trim();
                        mBILBillPosting.gCurrentBooking.IsBooked = true;
                        mBILBillPosting.gCurrentBooking.BookDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);
                        mBILBillPosting.gCurrentPatient = mIPDRegistrations.gCurrentPatient;

                        mBILBillPosting.gCurrentBooking.PriceName = Program.gCurrentUser.DefaultPriceCategoryCode;

                        mBILBillPosting.ShowDialog();

                        #endregion

                        gBillingDone = true;
                    }

                    if (mIPDRegistrations.gBillingOk == false)
                    {
                        return;
                    }

                    mIPDRegistrations.gBookingOk = true;

                    this.Close();
                }

                #endregion

                #region This will do diagnosis + admission + billing

                else if (chkDiagnosis.Checked == true && chkCharge.Checked == true)
                {
                    if (chkCharge.Checked == true)
                    {
                        mIPDRegistrations.gBillingOk = false;

                        #region billing + diagnosis + admission

                        frmDXTEpisodeVisitDetails mDXTEpisodeVisitDetails = new frmDXTEpisodeVisitDetails();
                        mDXTEpisodeVisitDetails.gIpdAdmission = mIPDRegistrations;

                        mDXTEpisodeVisitDetails.gCurrentBooking.BillingGroupCode = mIPDRegistrations.gBillingGroupCode;
                        mDXTEpisodeVisitDetails.gCurrentBooking.BillingGroupDescription = mIPDRegistrations.gBillingGroupDescription;
                        mDXTEpisodeVisitDetails.gCurrentBooking.BillingSubGroupCode = mIPDRegistrations.gBillingSubGroupCode;
                        mDXTEpisodeVisitDetails.gCurrentBooking.BillingSubGroupDescription = mIPDRegistrations.gBillingSubGroupDescription;
                        mDXTEpisodeVisitDetails.gCurrentBooking.BillingGroupMembershipNo = mIPDRegistrations.gBillingGroupMembershipNo;
                        mDXTEpisodeVisitDetails.gCurrentBooking.PatientCode = mIPDRegistrations.txtPatientId.Text.Trim();
                        mDXTEpisodeVisitDetails.gCurrentBooking.IsBooked = true;
                        mDXTEpisodeVisitDetails.gCurrentBooking.BookDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);
                        mDXTEpisodeVisitDetails.gCharge = true;
                        mDXTEpisodeVisitDetails.gCurrentPatient = mIPDRegistrations.gCurrentPatient;

                        mDXTEpisodeVisitDetails.ShowDialog();

                        #endregion

                        gBillingDone = true;
                        gDiagnosisDone = true;
                    }

                    if (mIPDRegistrations.gBillingOk == false)
                    {
                        return;
                    }

                    mIPDRegistrations.gBookingOk = true;

                    this.Close();
                }

                #endregion

                mIPDRegistrations.gBookingOk = true;

               
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
            this.Ok();
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region frmIPDAdmit_KeyDown
        void frmIPDAdmit_KeyDown(object sender, KeyEventArgs e)
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
    }
}