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
    public partial class frmCTCMeasurementDetails : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsReporter pMdtReporter;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_MT.clsCTCClients pMdtCTCClients;
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

        #endregion

        #region frmCTCMeasurementDetails
        public frmCTCMeasurementDetails(DataRow mSelectedDataRow = null)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCMeasurementDetails";
            this.KeyDown += new KeyEventHandler(frmCTCMeasurementDetails_KeyDown);

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
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctchivtests_customizelayout.ToString());

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

        #region frmCTCMeasurementDetails_Load
        private void frmCTCMeasurementDetails_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCMeasurementDetails_Load";

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

        #region frmCTCMeasurementDetails_Shown
        private void frmCTCMeasurementDetails_Shown(object sender, EventArgs e)
        {
            this.Data_Clear();

            if (pSelectedPatient != null)
            {
                this.Display_Patient(pSelectedPatient);
            }
        }
        #endregion

        #region frmCTCMeasurementDetails_FormClosing
        private void frmCTCMeasurementDetails_FormClosing(object sender, FormClosingEventArgs e)
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
                txtWeight.Text = pSelectedDataRow["weight"].ToString();
                txtHeight.Text = pSelectedDataRow["height"].ToString();
                txtPulse.Text = pSelectedDataRow["pulse"].ToString();
                txtBloodPressure.Text = pSelectedDataRow["bloodpressure"].ToString();
                txtRespRate.Text = pSelectedDataRow["resprate"].ToString();
                txtTemperature.Text = pSelectedDataRow["temperature"].ToString();
            }
        }
        #endregion

        #region Data_Clear
        private void Data_Clear()
        {
            txtWeight.Text = "";
            txtHeight.Text = "";
            txtPulse.Text = "";
            txtBloodPressure.Text = "";
            txtRespRate.Text = "";
            txtTemperature.Text = "";

            txtDate.EditValue = Program.gMdiForm.txtDate.EditValue;
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

            this.Data_Clear();
        }
        #endregion

        #region Data_New
        private void Data_New()
        {
            string mFunctionName = "Data_New";

            try
            {
                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);
                double mWeight = Program.IsMoney(txtWeight.Text) == false ? 0 : Convert.ToDouble(txtWeight.Text);
                double mHeight = Program.IsMoney(txtHeight.Text) == false ? 0 : Convert.ToDouble(txtHeight.Text);
                double mPulse = Program.IsMoney(txtPulse.Text) == false ? 0 : Convert.ToDouble(txtPulse.Text);
                double mRespRate = Program.IsMoney(txtRespRate.Text) == false ? 0 : Convert.ToDouble(txtRespRate.Text);
                double mTemperature = Program.IsMoney(txtTemperature.Text) == false ? 0 : Convert.ToDouble(txtTemperature.Text);

                AfyaPro_Types.clsResult mCtcClient = pMdtCTCClients.Add_Triage(
                    pSelectedPatient.code,
                    mTransDate,
                    mWeight,
                    mHeight,
                    mPulse,
                    txtBloodPressure.Text,
                    mRespRate,
                    mTemperature,
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

            try
            {
                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);
                double mWeight = Program.IsMoney(txtWeight.Text) == false ? 0 : Convert.ToDouble(txtWeight.Text);
                double mHeight = Program.IsMoney(txtHeight.Text) == false ? 0 : Convert.ToDouble(txtHeight.Text);
                double mPulse = Program.IsMoney(txtPulse.Text) == false ? 0 : Convert.ToDouble(txtPulse.Text);
                double mRespRate = Program.IsMoney(txtRespRate.Text) == false ? 0 : Convert.ToDouble(txtRespRate.Text);
                double mTemperature = Program.IsMoney(txtTemperature.Text) == false ? 0 : Convert.ToDouble(txtTemperature.Text);

                AfyaPro_Types.clsResult mCtcClient = pMdtCTCClients.Edit_Triage(
                    Convert.ToInt32(pSelectedDataRow["autocode"]),
                    pSelectedPatient.code,
                    mTransDate,
                    mWeight,
                    mHeight,
                    mPulse,
                    txtBloodPressure.Text,
                    mRespRate,
                    mTemperature,
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

        #region frmCTCMeasurementDetails_KeyDown
        void frmCTCMeasurementDetails_KeyDown(object sender, KeyEventArgs e)
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