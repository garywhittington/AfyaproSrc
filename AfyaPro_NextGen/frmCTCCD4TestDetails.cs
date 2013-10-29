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
    public partial class frmCTCCD4TestDetails : DevExpress.XtraEditors.XtraForm
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

        #region frmCTCCD4TestDetails
        public frmCTCCD4TestDetails(DataRow mSelectedDataRow = null)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCCD4TestDetails";
            this.KeyDown += new KeyEventHandler(frmCTCCD4TestDetails_KeyDown);

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
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctccd4tests_customizelayout.ToString());

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

        #region frmCTCCD4TestDetails_Load
        private void frmCTCCD4TestDetails_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCCD4TestDetails_Load";

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

        #region frmCTCCD4TestDetails_Shown
        private void frmCTCCD4TestDetails_Shown(object sender, EventArgs e)
        {
            this.Data_Clear();

            if (pSelectedPatient != null)
            {
                this.Display_Patient(pSelectedPatient);
            }

            txtDate.EditValue = DateTime.Now.Date;
        }
        #endregion

        #region frmCTCCD4TestDetails_FormClosing
        private void frmCTCCD4TestDetails_FormClosing(object sender, FormClosingEventArgs e)
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
                #region labtechnicians

                DataTable mDtCounsellors = pMdtReporter.View_LookupData("facilitystaffs", "code,description,treatmentpointcode",
                    "category=" + Convert.ToInt16(AfyaPro_Types.clsEnums.StaffCategories.LabTechnicians),
                    "description", Program.gLanguageName, "grdGENMedicalStaffs");

                cboLabTechnician.Properties.DataSource = mDtCounsellors;
                cboLabTechnician.Properties.DisplayMember = "description";
                cboLabTechnician.Properties.ValueMember = "code";

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
                cboLabTechnician.ItemIndex = Program.Get_LookupItemIndex(cboLabTechnician, "code", pSelectedDataRow["labtechniciancode"].ToString().Trim());
                txtSampleId.Text = pSelectedDataRow["sampleid"].ToString();
                txtDate.EditValue = Convert.ToDateTime(pSelectedDataRow["transdate"]);
                txtResult.Text = Convert.ToDouble(pSelectedDataRow["testresult"]) == 0 ? "" : pSelectedDataRow["testresult"].ToString();
                txtResultPercent.Text = Convert.ToDouble(pSelectedDataRow["testresultpercent"]) == 0 ? "" : pSelectedDataRow["testresultpercent"].ToString();
                txtResultDate.EditValue = pSelectedDataRow["testresultdate"] == DBNull.Value ? "" : Convert.ToDateTime(pSelectedDataRow["testresultdate"]).ToString("d");
                txtComments.Text = pSelectedDataRow["comments"].ToString();
            }
        }
        #endregion

        #region Toggle_SampleId

        private void Toggle_SampleId()
        {
            if (txtSampleId.Text.Trim() == "")
            {
                Int16 mGenerateCode = pMdtAutoCodes.Auto_Generate_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcsampleid));
                if (mGenerateCode == -1)
                {
                    Program.Display_Server_Error("");
                    return;
                }

                if (mGenerateCode == 1)
                {
                    txtSampleId.Text = "<<---New--->>";
                }
            }
        }

        #endregion

        #region Data_Clear
        private void Data_Clear()
        {
            txtSampleId.Text = "";
            txtDate.EditValue = null;
            txtResult.Text = "";
            txtResultDate.EditValue = null;
            txtComments.Text = "";

            this.Toggle_SampleId();
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
            Int16 mGenerateSampleId = 0;
            double mResult = 0;
            double mResultPercent = 0;
            DateTime? mResultDate = null;

            string mFunctionName = "Data_New";

            if (txtSampleId.Text.Trim() == "" && txtSampleId.Text.Trim().ToLower() != "<<---new--->>")
            {
                Program.Display_Error("Invalid sample Id");
                txtSampleId.Focus();
                return;
            }
            if (Program.IsDate(txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                txtDate.Focus();
                return;
            }
            if (cboLabTechnician.ItemIndex == -1)
            {
                Program.Display_Error("Invalid lab technician selection", false);
                cboLabTechnician.Focus();
                return;
            }
            if (txtResult.Text.Trim() != "")
            {
                if (Program.IsMoney(txtResult.Text) == false)
                {
                    Program.Display_Error("Invalid entry for result", false);
                    txtResult.Focus();
                    return;
                }

                mResult = Convert.ToDouble(txtResult.Text);
            }
            if (txtResultPercent.Text.Trim() != "")
            {
                if (Program.IsMoney(txtResultPercent.Text) == false)
                {
                    Program.Display_Error("Invalid entry for result", false);
                    txtResultPercent.Focus();
                    return;
                }

                mResultPercent = Convert.ToDouble(txtResultPercent.Text);
            }

            if (txtResultDate.Text.Trim() != "")
            {
                if (Program.IsDate(txtResultDate.EditValue) == false)
                {
                    Program.Display_Error("Invalid entry for date result given", false);
                    txtResultDate.Focus();
                    return;
                }

                mResultDate = Convert.ToDateTime(txtResultDate.EditValue);
            }

            try
            {
                string mLabTechnicianCode = cboLabTechnician.GetColumnValue("code").ToString();

                if (txtSampleId.Text.Trim().ToLower() == "<<---new--->>")
                {
                    mGenerateSampleId = 1;
                }

                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Add_CD4Test(
                    pSelectedPatient.code,
                    mGenerateSampleId,
                    txtSampleId.Text,
                    mTransDate,
                    mResult,
                    mResultPercent,
                    mResultDate,
                    txtComments.Text,
                    mLabTechnicianCode,
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
            double mResult = 0;
            double mResultPercent = 0;
            DateTime? mResultDate = null;

            string mFunctionName = "Data_Edit";
            if (Program.IsDate(txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                txtDate.Focus();
                return;
            }
            if (cboLabTechnician.ItemIndex == -1)
            {
                Program.Display_Error("Invalid lab technician selection", false);
                cboLabTechnician.Focus();
                return;
            }
            if (txtResult.Text.Trim() != "")
            {
                if (Program.IsMoney(txtResult.Text) == false)
                {
                    Program.Display_Error("Invalid entry for result", false);
                    txtResult.Focus();
                    return;
                }

                mResult = Convert.ToDouble(txtResult.Text);
            }
            if (txtResultPercent.Text.Trim() != "")
            {
                if (Program.IsMoney(txtResultPercent.Text) == false)
                {
                    Program.Display_Error("Invalid entry for result", false);
                    txtResultPercent.Focus();
                    return;
                }

                mResultPercent = Convert.ToDouble(txtResultPercent.Text);
            }

            if (txtResultDate.Text.Trim() != "")
            {
                if (Program.IsDate(txtResultDate.EditValue) == false)
                {
                    Program.Display_Error("Invalid entry for date result given", false);
                    txtResultDate.Focus();
                    return;
                }

                mResultDate = Convert.ToDateTime(txtResultDate.EditValue);
            }

            try
            {
                string mLabTechnicianCode = cboLabTechnician.GetColumnValue("code").ToString();

                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Edit_CD4Test(
                    Convert.ToInt32(pSelectedDataRow["autocode"]),
                    mTransDate,
                    mResult,
                    mResultPercent,
                    mResultDate,
                    txtComments.Text,
                    mLabTechnicianCode,
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

        #region frmCTCCD4TestDetails_KeyDown
        void frmCTCCD4TestDetails_KeyDown(object sender, KeyEventArgs e)
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

        #region sample id events

        #region txtSampleId_Enter
        private void txtSampleId_Enter(object sender, EventArgs e)
        {
            if (txtSampleId.Text.Trim().ToLower() == "<<---new--->>")
            {
                txtSampleId.Text = "";
            }
            else
            {
                txtSampleId.SelectAll();
            }
        }
        #endregion

        #region txtSampleId_Leave
        private void txtSampleId_Leave(object sender, EventArgs e)
        {
            if (txtSampleId.Text.Trim() == "")
            {
                Int16 mGenerateCode = pMdtAutoCodes.Auto_Generate_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcsampleid));
                if (mGenerateCode == -1)
                {
                    return;
                }
                if (mGenerateCode == 1)
                {
                    txtSampleId.Text = "<<---New--->>";
                }
                else
                {
                    txtSampleId.Focus();
                }
            }
        }
        #endregion

        #endregion
    }
}