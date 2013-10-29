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
    public partial class frmIPDTransfers : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsRegistrations pMdtRegistrations;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private AfyaPro_MT.clsWards pMdtWards;
        private AfyaPro_MT.clsWardRooms pMdtWardRooms;
        private AfyaPro_MT.clsWardRoomBeds pMdtWardRoomBeds;
        private AfyaPro_Types.clsPatient pCurrentPatient;
        private AfyaPro_Types.clsBooking pCurrentBooking = new AfyaPro_Types.clsBooking();
        private AfyaPro_Types.clsAdmission pCurrentAdmission = new AfyaPro_Types.clsAdmission();

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private bool pCheckedCheckBox = false;
        private bool pCheckedGrid = false;

        private int pCurrAdmissionId = 0;
        private string pCurrPatientId = "";
        private string pPrevPatientId = "";
        private bool pSearchingPatient = false;

        private string pPrevWardCode = "";
        private string pPrevRoomCode = "";
        private string pPrevBedCode = "";

        private DataTable pDtWards = new DataTable("wards");
        private DataTable pDtRooms = new DataTable("rooms");
        private DataTable pDtBeds = new DataTable("beds");

        #endregion

        #region frmIPDTransfers
        public frmIPDTransfers()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmIPDTransfers";

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                pMdtRegistrations = (AfyaPro_MT.clsRegistrations)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRegistrations),
                    Program.gMiddleTier + "clsRegistrations");

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
                cboBedNo.Properties.DataSource = pDtBeds;
                cboBedNo.Properties.DisplayMember = "description";
                cboBedNo.Properties.ValueMember = "code";

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ipdtransfers_customizelayout.ToString());

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
            mObjectsList.Add(grpTransfer);
            mObjectsList.Add(txbWard);
            mObjectsList.Add(txbRoom);
            mObjectsList.Add(txbBedNo);
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
            cboWard.EditValue = null;
            cboRoom.EditValue = null;
            cboBedNo.EditValue = null;
            txtRemarks.Text = "";

            pCurrAdmissionId = 0;
            pPrevWardCode = "";
            pPrevRoomCode = "";
            pPrevBedCode = "";

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
                                pCurrentAdmission = pMdtRegistrations.Get_Admission(pCurrentBooking.Booking, txtPatientId.Text);

                                if (pCurrentAdmission != null)
                                {
                                    AfyaPro_Types.clsDischarge mDischargeDetails = pMdtRegistrations.Get_Discharge(
                                        pCurrentBooking.Booking, txtPatientId.Text, pCurrentAdmission.WardCode,
                                        pCurrentAdmission.RoomCode);

                                    if (pCurrentAdmission.IsAdmitted == true && mDischargeDetails.IsDischarged == false)
                                    {
                                        pCurrAdmissionId = pCurrentAdmission.AdmissionId;
                                        txtCurrWard.Text = pCurrentAdmission.WardDescription;
                                        txtCurrRoom.Text = pCurrentAdmission.RoomDescription;
                                        txtCurrBedNo.Text = pCurrentAdmission.BedDescription;

                                        pPrevWardCode = pCurrentAdmission.WardCode;
                                        pPrevRoomCode = pCurrentAdmission.RoomCode;
                                        pPrevBedCode = pCurrentAdmission.BedCode;
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
            string mWardCode = "";
            string mRoomCode = "";

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

            mWardCode = cboWard.GetColumnValue("code").ToString().Trim();
            mRoomCode = cboRoom.GetColumnValue("code").ToString().Trim();

            #endregion

            try
            {
                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                string mBedCode = "";

                if (cboBedNo.ItemIndex != -1)
                {
                    mBedCode = cboBedNo.GetColumnValue("code").ToString().Trim();
                }

                //transfer 
                pCurrentBooking = pMdtRegistrations.IPD_Transfer(pCurrAdmissionId, mTransDate, txtPatientId.Text,
                pPrevWardCode, pPrevRoomCode, pPrevBedCode,
                mWardCode,
                mRoomCode,
                mBedCode,
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

                //refresh
                txtPatientId.Text = "";
                Program.Display_Info(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_TransferSuccess.ToString());
                

                if (Program.GrantDeny_FunctionAccess(AfyaPro_Types.clsSystemFunctions.FunctionKeys.ipdtransfers_showsuccessmessage.ToString()) == true)
                {
                    Program.Display_Info(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_TransferSuccess.ToString());
                }

                if (Program.GrantDeny_FunctionAccess(AfyaPro_Types.clsSystemFunctions.FunctionKeys.ipdtransfers_closeaftertransfer.ToString()) == true)
                {
                    this.Close();
                }
                else
                {
                    this.Data_Clear();
                    txtPatientId.Focus();
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