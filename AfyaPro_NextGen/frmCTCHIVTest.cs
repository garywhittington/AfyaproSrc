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
    public partial class frmCTCHIVTest : DevExpress.XtraEditors.XtraForm
    {
        #region declaration
        
        private AfyaPro_MT.clsMedicalStaffs pMdtCounsellors;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_MT.clsCTCClients pMdtCTCClients;
        private AfyaPro_MT.clsTreatmentPoints pMdtTreatmentPoints;
        private AfyaPro_MT.clsReporter pMdtReporter;
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
        private string pCurrPatientId = "";
        private string pPrevPatientId = "";
        private string pCurrHIVNo = "";
        private string pPrevHIVNo = "";
        private string pCurrCTCNo = "";
        private string pPrevCTCNo = "";
        private bool pSearchingPatient = false;

        private DataTable pDtCounsellors = new DataTable("medicalstaffs");
        private DataTable pDtTreatmentPoints = new DataTable("treatmentpoints");

        #endregion

        #region frmCTCHIVTest
        public frmCTCHIVTest()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCHIVTest";
            this.KeyDown += new KeyEventHandler(frmCTCHIVTest_KeyDown);

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

                pMdtCounsellors = (AfyaPro_MT.clsMedicalStaffs)Activator.GetObject(
                    typeof(AfyaPro_MT.clsMedicalStaffs),
                    Program.gMiddleTier + "clsMedicalStaffs");

                pMdtTreatmentPoints = (AfyaPro_MT.clsTreatmentPoints)Activator.GetObject(
                   typeof(AfyaPro_MT.clsTreatmentPoints),
                   Program.gMiddleTier + "clsTreatmentPoints");

                pMdtReporter = (AfyaPro_MT.clsReporter)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReporter),
                    Program.gMiddleTier + "clsReporter");

                pDtCounsellors.Columns.Add("code", typeof(System.String));
                pDtCounsellors.Columns.Add("Description", typeof(System.String));
                pDtCounsellors.Columns.Add("treatmentpointcode", typeof(System.String));
                cboCounsellor.Properties.DataSource = pDtCounsellors;
                cboCounsellor.Properties.PopulateColumns();
                cboCounsellor.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("code"));
                cboCounsellor.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Description"));
                cboCounsellor.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("treatmentpointcode"));
                cboCounsellor.Properties.Columns["code"].Visible = false;
                cboCounsellor.Properties.Columns["treatmentpointcode"].Visible = false;
                cboCounsellor.Properties.DisplayMember = "Description";
                cboCounsellor.Properties.ValueMember = "code";

                pDtTreatmentPoints.Columns.Add("code", typeof(System.String));
                pDtTreatmentPoints.Columns.Add("Description", typeof(System.String));
                cboTreatmentPoint.Properties.DataSource = pDtTreatmentPoints;
                cboTreatmentPoint.Properties.PopulateColumns();
                cboTreatmentPoint.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("code"));
                cboTreatmentPoint.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Description"));
                cboTreatmentPoint.Properties.Columns["code"].Visible = false;
                cboTreatmentPoint.Properties.DisplayMember = "Description";
                cboTreatmentPoint.Properties.ValueMember = "code";

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctchivtests_customizelayout.ToString());

                Fill_LookupData();

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
            mObjectsList.Add(cmdNew);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region frmCTCHIVTest_Load
        private void frmCTCHIVTest_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCHIVTest_Load";

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

        #region frmCTCHIVTest_Shown
        private void frmCTCHIVTest_Shown(object sender, EventArgs e)
        {
            //set clinical officer to user
            cboCounsellor.ItemIndex = Program.Get_LookupItemIndex(cboCounsellor, "code", Program.gCurrentUser.Code);
           
            if (pSelectedPatient != null)
            {
                this.Data_Display(pSelectedPatient);
            }

            cboSearchBy.SelectedIndex = 0;
            txtSearchText.Focus();
        }
        #endregion

        #region frmCTCHIVTest_FormClosing
        private void frmCTCHIVTest_FormClosing(object sender, FormClosingEventArgs e)
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

            DataRow mNewRow;
            try
            {
                #region Counsellors

                pDtCounsellors.Rows.Clear();

                DataTable mDtCounsellors = pMdtCounsellors.View(
                    "category=" +  Convert.ToInt16(AfyaPro_Types.clsEnums.StaffCategories.Counsellors),
                    "description", Program.gLanguageName, "grdGENMedicalStaffs");

                mNewRow = pDtCounsellors.NewRow();
                mNewRow["code"] = "";
                mNewRow["description"] = "Unknown Clinician";
                mNewRow["treatmentpointcode"] = "";
                pDtCounsellors.Rows.Add(mNewRow);
                pDtCounsellors.AcceptChanges();
                foreach (DataRow mDataRow in mDtCounsellors.Rows)
                {
                    mNewRow = pDtCounsellors.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    mNewRow["treatmentpointcode"] = mDataRow["treatmentpointcode"].ToString();
                    pDtCounsellors.Rows.Add(mNewRow);
                    pDtCounsellors.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtCounsellors.Columns)
                {
                    mDataColumn.Caption = mDtCounsellors.Columns[mDataColumn.ColumnName].Caption;
                }

                #endregion

                #region TreatmentPoints

                pDtTreatmentPoints.Rows.Clear();
                DataTable mDtTreatmentPoints = pMdtTreatmentPoints.View("", "code", Program.gLanguageName, "grdGENTreatmentPoints");
                foreach (DataRow mDataRow in mDtTreatmentPoints.Rows)
                {
                    mNewRow = pDtTreatmentPoints.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtTreatmentPoints.Rows.Add(mNewRow);
                    pDtTreatmentPoints.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtTreatmentPoints.Columns)
                {
                    mDataColumn.Caption = mDtTreatmentPoints.Columns[mDataColumn.ColumnName].Caption;
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

                txtSearchText.Text = "";
                switch (cboSearchBy.SelectedIndex)
                {
                    case 0: txtSearchText.Text = mPatient.code; break;
                    case 1: txtSearchText.Text = mPatient.hivtestno; break;
                    case 2: txtSearchText.Text = mPatient.hivno; break;
                    case 3: txtSearchText.Text = mPatient.arvno; break;
                    case 4: txtSearchText.Text = mPatient.ctcno; break;
                    default: txtSearchText.Text = ""; break;
                }

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

                #region last test date

                DataTable mDtVisits = pMdtReporter.View_LookupData("view_ctc_lasthivtests", "*", "patientcode='" + mPatient.code.Trim() + "'", "", "", "");

                if (mDtVisits.Rows.Count > 0)
                {
                    txtLastVisitDate.EditValue = Convert.ToDateTime(mDtVisits.Rows[0]["transdate"]);
                }
                else
                {
                    txtLastVisitDate.EditValue = null;
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
            txtPatientId.Text = "";
            txtName.Text = "";
            txtYears.Text = "";
            txtMonths.Text = "";
            txtGender.Text = "";
            txtLastVisitDate.EditValue = null;
            txtSearchText.Text = "";
        }
        #endregion

        #region cmdNew_Click
        private void cmdNew_Click(object sender, EventArgs e)
        {
            if (pSelectedPatient == null)
            {
                Program.Display_Error("Please specify a valid Patient # and try again");
                return;
            }
            if (cboCounsellor.ItemIndex == -1)
            {
                Program.Display_Error("Please select a counsellor", false);
                cboCounsellor.Focus();
                return;
            }

            frmCTCHIVTestDetails mCTCAppointmentDetails = new frmCTCHIVTestDetails();
            mCTCAppointmentDetails.SelectedPatient = pSelectedPatient;
            mCTCAppointmentDetails.SelectedCounsellor = cboCounsellor.GetColumnValue("code").ToString();
            mCTCAppointmentDetails.ShowDialog();

            if (mCTCAppointmentDetails.RecordSaved == true)
            {
                txtPatientId.Text = "";
                this.Data_Clear();
                this.pSelectedPatient = null;
                txtSearchText.Focus();
            }
        }
        #endregion

        #region cmdHistory_Click
        private void cmdHistory_Click(object sender, EventArgs e)
        {
            if (pSelectedPatient == null)
            {
                Program.Display_Error("Please specify a valid Patient # and try again");
                return;
            }
            if (cboCounsellor.ItemIndex == -1)
            {
                Program.Display_Error("Please select a counsellor", false);
                cboCounsellor.Focus();
                return;
            }

            frmCTCHIVTestVisits mCTCHIVTestVisits = new frmCTCHIVTestVisits();
            mCTCHIVTestVisits.SelectedPatient = pSelectedPatient;
            mCTCHIVTestVisits.SelectedCounsellor = cboCounsellor.GetColumnValue("code").ToString();
            mCTCHIVTestVisits.ShowDialog();
            if (mCTCHIVTestVisits.RecordSaved == true)
            {
                txtPatientId.Text = "";
                this.Data_Clear();
                this.pSelectedPatient = null;
            }
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region frmCTCHIVTest_KeyDown
        void frmCTCHIVTest_KeyDown(object sender, KeyEventArgs e)
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

        #region Search_Patient by field
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

            this.pSelectedPatient = this.Search_Patient(mFieldName, txtSearchText.Text);

            if (pSelectedPatient != null)
            {
                if (pSelectedPatient.Exist == false)
                {
                    Program.Display_Info("No records found");
                }
            }
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

                this.Data_Display(pSelectedPatient);
            }
        }
        #endregion

        private void cmdViewQueue_Click(object sender, EventArgs e)
        {
            if (cboTreatmentPoint.ItemIndex == -1)
            {
                Program.Display_Error("Please select the HTC Point and Try again", false);
                cboTreatmentPoint.Focus();
                return;
            }
            frmQueueTreatmentPoint mQueueTreatmentPoint = new frmQueueTreatmentPoint();
            mQueueTreatmentPoint.TreatmentPointCode = cboTreatmentPoint.GetColumnValue("code").ToString().Trim();
            mQueueTreatmentPoint.TransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);
            mQueueTreatmentPoint.QueueType = (int)AfyaPro_Types.clsEnums.PatientsQueueTypes.Consultation;
            mQueueTreatmentPoint.ShowDialog();
            if (mQueueTreatmentPoint.PatientSelected == true)
            {
                txtPatientId.Text = mQueueTreatmentPoint.PatientCode;
                this.pSelectedPatient = this.Search_Patient("p.code", txtPatientId.Text);
                this.Data_Display(pSelectedPatient);
            }

        }

        #endregion

        #region cboCounsellor_EditValueChanged
        private void cboCounsellor_EditValueChanged(object sender, EventArgs e)
        {
            if (cboCounsellor.ItemIndex == -1)
            {
                return;
            }

            string mTreatmentPointCode = cboCounsellor.GetColumnValue("treatmentpointcode").ToString();
            if (mTreatmentPointCode.Trim() != "")
            {
                cboTreatmentPoint.ItemIndex = Program.Get_LookupItemIndex(cboTreatmentPoint, "code", mTreatmentPointCode);
            }
        }
        #endregion

        private void cmdSupport_Click(object sender, EventArgs e)
        {
            if (pSelectedPatient == null)
            {
                Program.Display_Error("Please specify a valid Patient # and try again");
                return;
            }
            if (cboCounsellor.ItemIndex == -1)
            {
                Program.Display_Error("Please select a counsellor", false);
                cboCounsellor.Focus();
                return;
            }

            frmCTCSupport mSupportiveForm = new frmCTCSupport();
            mSupportiveForm.SelectedPatient = pSelectedPatient;
            mSupportiveForm.SelectedCounsellor = cboCounsellor.GetColumnValue("code").ToString();
            mSupportiveForm.ShowDialog();

            if (mSupportiveForm.RecordSaved == true)
            {
                txtPatientId.Text = "";
                this.Data_Clear();
                this.pSelectedPatient = null;
                txtSearchText.Focus();
            }
        }
      }
}