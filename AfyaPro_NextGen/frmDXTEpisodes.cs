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
    public partial class frmDXTEpisodes : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsMedicalStaffs pMdtMedicalStaffs;
        private AfyaPro_MT.clsRegistrations pMdtRegistrations;
        private AfyaPro_MT.clsPatientDiagnoses pMdtPatientDiagnoses;
        private AfyaPro_MT.clsTreatmentPoints pMdtTreatmentPoints;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();
        internal object gLastAttendanceDate = null;

        private Type pType;
        private string pClassName = "";

        internal AfyaPro_Types.clsPatient gCurrentPatient;
        internal string gDoctorCode = "";
        internal string gDoctorDescription = "";

        private string pCurrPatientId = "";
        private string pPrevPatientId = "";
        private bool pSearchingPatient = false;

        private DataTable pDtMedicalStaffs = new DataTable("medicalstaffs");
        private DataTable pDtTreatmentPoints = new DataTable("treatmentpoints");
        private DataTable pDtEpisodes = new DataTable("dxepisodes");

        #endregion

        #region frmDXTEpisodes
        public frmDXTEpisodes()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmDXTEpisodes";

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                pMdtRegistrations = (AfyaPro_MT.clsRegistrations)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRegistrations),
                    Program.gMiddleTier + "clsRegistrations");

                pMdtMedicalStaffs = (AfyaPro_MT.clsMedicalStaffs)Activator.GetObject(
                    typeof(AfyaPro_MT.clsMedicalStaffs),
                    Program.gMiddleTier + "clsMedicalStaffs");

                pMdtPatientDiagnoses = (AfyaPro_MT.clsPatientDiagnoses)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPatientDiagnoses),
                    Program.gMiddleTier + "clsPatientDiagnoses");

                pMdtTreatmentPoints = (AfyaPro_MT.clsTreatmentPoints)Activator.GetObject(
                    typeof(AfyaPro_MT.clsTreatmentPoints),
                    Program.gMiddleTier + "clsTreatmentPoints");

                pDtMedicalStaffs.Columns.Add("code", typeof(System.String));
                pDtMedicalStaffs.Columns.Add("description", typeof(System.String));
                pDtMedicalStaffs.Columns.Add("treatmentpointcode", typeof(System.String));
                cboDoctor.Properties.DataSource = pDtMedicalStaffs;
                cboDoctor.Properties.DisplayMember = "description";
                cboDoctor.Properties.ValueMember = "code";

                pDtEpisodes.Columns.Add("diagnosiscode", typeof(System.String));
                pDtEpisodes.Columns.Add("diagnosisdescription", typeof(System.String));
                pDtEpisodes.Columns.Add("firstencounterdate", typeof(System.DateTime));
                pDtEpisodes.Columns.Add("lastencounterdate", typeof(System.DateTime));
                pDtEpisodes.Columns.Add("episodecode", typeof(System.String));

                pDtEpisodes = pMdtPatientDiagnoses.View_Episodes("1=2", "", grdDXTEpisodes.Name);
                grdDXTEpisodes.DataSource = pDtEpisodes;

                pDtTreatmentPoints.Columns.Add("code", typeof(System.String));
                pDtTreatmentPoints.Columns.Add("description", typeof(System.String));
                cboTreatmentPoint.Properties.DataSource = pDtTreatmentPoints;
                cboTreatmentPoint.Properties.DisplayMember = "description";
                cboTreatmentPoint.Properties.ValueMember = "code";

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.dxtpatientdiagnoses_customizelayout.ToString());

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
            mObjectsList.Add(txbYears);
            mObjectsList.Add(txbMonths);
            mObjectsList.Add(txbGender);
            mObjectsList.Add(txbDoctor);
            mObjectsList.Add(cmdSearch);
            mObjectsList.Add(cmdNew);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
            this.Data_Clear();
        }

        #endregion

        #region frmDXTEpisodes_Load
        private void frmDXTEpisodes_Load(object sender, EventArgs e)
        {
            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);
            Program.Restore_GridLayout(grdDXTEpisodes, grdDXTEpisodes.Name);

            this.Load_Controls();

            txbBirthDateFormat.Text = "(" + Program.gCulture.DateTimeFormat.ShortDatePattern + ")";

            Program.Center_Screen(this);

            this.Data_Display(gCurrentPatient);

            this.Append_ShortcutKeys();
        }
        #endregion

        #region frmDXTEpisodes_Shown
        private void frmDXTEpisodes_Shown(object sender, EventArgs e)
        {
            //set clinical officer to user
            cboDoctor.ItemIndex = Program.Get_LookupItemIndex(cboDoctor, "code", Program.gCurrentUser.Code);
            txtPatientId.Focus();
        }
        #endregion

        #region frmDXTEpisodes_FormClosing
        private void frmDXTEpisodes_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //layout
                if (layoutControl1.IsModified == true)
                {
                    Program.Save_FormLayout(this, layoutControl1, this.Name);
                }

                Program.Save_GridLayout(grdDXTEpisodes, grdDXTEpisodes.Name);
            }
            catch { }
        }
        #endregion

        #region Append_ShortcutKeys
        private void Append_ShortcutKeys()
        {
            //cmdSearch.Text = cmdSearch.Text + " (" + Program.KeyCode_SeachPatient.ToString() + ")";
            //cmdOk.Text = cmdOk.Text + " (" + Program.KeyCode_Save.ToString() + ")";
            //cmdPatientsQueue.Text = cmdPatientsQueue.Text + " (" + Program.KeyCode_ViewPatientsQueue.ToString() + ")";
            //cmdLabTestResults.Text = cmdLabTestResults.Text + " (" + Program.KeyCode_ViewLabTestResults.ToString() + ")";
            //cmdSearchDx.Text = cmdSearchDx.Text + " (" + Program.KeyCode_SearchDiagnosis.ToString() + ")";
            //cmdAdd.Text = cmdAdd.Text + " (" + Program.KeyCode_ItemAdd.ToString() + ")";
            //cmdRemove.Text = cmdRemove.Text + " (" + Program.KeyCode_ItemRemove.ToString() + ")";
        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            DataRow mNewRow;
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region medical doctors

                pDtMedicalStaffs.Rows.Clear();

                DataTable mDtMedicalStaffs = pMdtMedicalStaffs.View(
                    "category=" + Convert.ToInt16(AfyaPro_Types.clsEnums.StaffCategories.MedicalDoctors)
                    + " or category=" + Convert.ToInt16(AfyaPro_Types.clsEnums.StaffCategories.Nurses),
                    "description", Program.gLanguageName, "grdGENMedicalStaffs");

                mNewRow = pDtMedicalStaffs.NewRow();
                mNewRow["code"] = "";
                mNewRow["description"] = "Unknown Clinician";
                mNewRow["treatmentpointcode"] = "";
                pDtMedicalStaffs.Rows.Add(mNewRow);
                pDtMedicalStaffs.AcceptChanges();
                foreach (DataRow mDataRow in mDtMedicalStaffs.Rows)
                {
                    mNewRow = pDtMedicalStaffs.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    mNewRow["treatmentpointcode"] = mDataRow["treatmentpointcode"].ToString();
                    pDtMedicalStaffs.Rows.Add(mNewRow);
                    pDtMedicalStaffs.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtMedicalStaffs.Columns)
                {
                    mDataColumn.Caption = mDtMedicalStaffs.Columns[mDataColumn.ColumnName].Caption;
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

        #region Data_Clear
        private void Data_Clear()
        {
            txtName.Text = "";
            txtGender.Text = "";
            txtBirthDate.Text = "";
            txtYears.Text = "";
            txtMonths.Text = "";

            txtWard.Text = "";
            txtRoom.Text = "";
            txtBedNo.Text = "";
            pDtEpisodes.Rows.Clear();

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

                       
                        #region get current booking

                        AfyaPro_Types.clsBooking mCurrentBooking = pMdtRegistrations.Get_Booking(txtPatientId.Text);

                        if (mCurrentBooking.IsBooked == true)
                        {
                            AfyaPro_Types.clsAdmission mAdmission = pMdtRegistrations.Get_Admission(mCurrentBooking.Booking, txtPatientId.Text);
                            if (mAdmission != null)
                            {
                                
                                if (mAdmission.IsAdmitted == true)
                                {
                                    txtWard.Text = mAdmission.WardDescription;
                                    txtRoom.Text = mAdmission.RoomDescription;
                                    txtBedNo.Text = mAdmission.BedDescription;
                                }
                            }
                        }

                        #endregion

                        #region get last visit/attendance
                        AfyaPro_Types.clsBooking mLastBooking = pMdtRegistrations.Get_Booking(txtPatientId.Text);

                        
                        if (mLastBooking != null)
                        {
                            if (mLastBooking.Booking.Trim() != "")
                            {
                               
                                if (Program.IsNullDate(mLastBooking.BookDate) == false)
                                {
                                    gLastAttendanceDate = Convert.ToDateTime(mLastBooking.BookDate);
                                }
                            }
                        }

                        TimeSpan Ts = (Convert.ToDateTime(Program.gServerDate) - Convert.ToDateTime(gLastAttendanceDate));
                        Double mdays =Ts.Days;
                        if (mdays < 32)
                        {
                            lblDisplay.Text = "PATIENT'S LAST VISIT IS LESS THAN ONE MONTH";
                            lblDisplay.ForeColor = Color.Red;

                        }
                        else
                        {
                            lblDisplay.ResetText();
                        }
                        #endregion




                        this.Fill_Episodes();

                        pCurrPatientId = mPatient.code;
                        pPrevPatientId = pCurrPatientId;

                        System.Media.SystemSounds.Beep.Play();

                        cboDoctor.Focus();
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

        #region Fill_Episodes
        private void Fill_Episodes()
        {
            string mFunctionName = "Fill_Episodes";

            try
            {
                pDtEpisodes.Rows.Clear();

                DataTable mDtEpisodes = pMdtPatientDiagnoses.View_Episodes("patientcode='" + txtPatientId.Text.Trim() + "'", "lastencounterdate desc", grdDXTEpisodes.Name);

                foreach (DataRow mDataRow in mDtEpisodes.Rows)
                {
                    DataRow mNewRow = pDtEpisodes.NewRow();

                    foreach (DataColumn mDataColumn in pDtEpisodes.Columns)
                    {
                        mNewRow[mDataColumn.ColumnName] = mDataRow[mDataColumn.ColumnName];
                    }

                    pDtEpisodes.Rows.Add(mNewRow);
                    pDtEpisodes.AcceptChanges();
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
                gCurrentPatient = pMdtRegistrations.Get_Patient(txtPatientId.Text);
                return gCurrentPatient;
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
                this.gCurrentPatient = this.Search_Patient();
                this.Data_Display(gCurrentPatient);
            }
        }
        #endregion

        #region txtPatientId_Leave
        private void txtPatientId_Leave(object sender, EventArgs e)
        {
            pCurrPatientId = txtPatientId.Text;

            if (pCurrPatientId.Trim().ToLower() != pPrevPatientId.Trim().ToLower())
            {
                this.gCurrentPatient = this.Search_Patient();
                this.Data_Display(gCurrentPatient);
            }
        }
        #endregion

        #region cmdSearch_Click
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            pSearchingPatient = true;

            frmSearchPatient mSearchPatient = new frmSearchPatient(txtPatientId);
            mSearchPatient.ShowDialog();
            if (mSearchPatient.SearchingDone == true)
            {
                cboDoctor.Focus();
            }

            pSearchingPatient = false;
        }
        #endregion

        #region txtPatientId_EditValueChanged
        private void txtPatientId_EditValueChanged(object sender, EventArgs e)
        {
            if (pSearchingPatient == true)
            {
                this.gCurrentPatient = this.Search_Patient();
                this.Data_Display(gCurrentPatient);
            }
        }
        #endregion

        #region frmDXTEpisodes_KeyDown
        void frmDXTEpisodes_KeyDown(object sender, KeyEventArgs e)
        {
            //switch (e.KeyCode)
            //{
            //    case Program.KeyCode_SeachPatient:
            //        {
            //            pSearchingPatient = true;

            //            frmSearchPatient mSearchPatient = new frmSearchPatient(txtPatientId);
            //            mSearchPatient.ShowDialog();
            //            if (mSearchPatient.SearchingDone == true)
            //            {
            //                cboDoctor.Focus();
            //            }

            //            pSearchingPatient = false;
            //        }
            //        break;
            //    case Program.KeyCode_ViewLabTestResults:
            //        {
            //            this.View_LabTestResults();
            //        }
            //        break;
            //    case Program.KeyCode_ViewPatientsQueue:
            //        {
            //            this.cmdPatientsQueue_Click(cmdPatientsQueue, e);
            //        }
            //        break;
            //}
        }
        #endregion

        #region cboDoctor_EditValueChanged
        private void cboDoctor_EditValueChanged(object sender, EventArgs e)
        {
            if (cboDoctor.ItemIndex == -1)
            {
                return;
            }

            string mTreatmentPointCode = cboDoctor.GetColumnValue("treatmentpointcode").ToString();
            if (mTreatmentPointCode.Trim() != "")
            {
                cboTreatmentPoint.ItemIndex = Program.Get_LookupItemIndex(cboTreatmentPoint, "code", mTreatmentPointCode);
            }
        }
        #endregion

        #region cmdNew_Click
        private void cmdNew_Click(object sender, EventArgs e)
        {
            if (gCurrentPatient == null)
            {
                Program.Display_Error("Please specify a valid Patient # and try again");
                return;
            }
            if (cboDoctor.ItemIndex == -1)
            {
                Program.Display_Error("Please select a clinical officer and try again");
                cboDoctor.Focus();
                return;
            }

            #region validate current booking

            DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

            AfyaPro_Types.clsBooking mCurrentBooking = pMdtRegistrations.Get_Booking(txtPatientId.Text);

            if (mCurrentBooking.BookDate.Date != mTransDate.Date)
            {
                //retry getting booking
                mCurrentBooking = pMdtRegistrations.Get_Booking(txtPatientId.Text);
                if (mCurrentBooking.IsBooked == false)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientIsNotBooked.ToString());
                    return;
                }
                else
                {
                    if (mCurrentBooking.BookDate.Date != mTransDate.Date)
                    {
                        if (mCurrentBooking.Department.Trim().ToLower() !=
                            AfyaPro_Types.clsEnums.PatientCategories.IPD.ToString().ToLower())
                        {
                            Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientIsNotBooked.ToString());
                            return;
                        }
                    }
                }
            }

            #endregion

            frmDXTEpisodeVisitDetails mDXTEpisodeVisitDetails = new frmDXTEpisodeVisitDetails();
            mDXTEpisodeVisitDetails.gCurrentPatient = gCurrentPatient;
            mDXTEpisodeVisitDetails.DoctorCode = cboDoctor.ItemIndex == -1 ? "" : cboDoctor.GetColumnValue("code").ToString().Trim();
            mDXTEpisodeVisitDetails.EpisodeCode = "";
            mDXTEpisodeVisitDetails.ShowDialog();

            if (mDXTEpisodeVisitDetails.RecordSaved == true)
            {
                txtPatientId.Text = "";
                this.Data_Clear();
                txtPatientId.Focus();
            }
        }
        #endregion

        #region cmdDetails_Click
        private void cmdDetails_Click(object sender, EventArgs e)
        {
            if (gCurrentPatient == null)
            {
                Program.Display_Error("Please specify a valid Patient # and try again");
                return;
            }

            if (viewDXTEpisodes.FocusedRowHandle < 0)
            {
                Program.Display_Error("Please select a case and try again");
                return;
            }

            frmDXTEpisodeVisits mDXTEpisodeVisits = new frmDXTEpisodeVisits();
            mDXTEpisodeVisits.SelectedPatient = gCurrentPatient;
            mDXTEpisodeVisits.EpisodeRow = viewDXTEpisodes.GetDataRow(viewDXTEpisodes.FocusedRowHandle);
            mDXTEpisodeVisits.ShowDialog();

            if (mDXTEpisodeVisits.RecordSaved == true)
            {
                txtPatientId.Text = "";
                this.Data_Clear();
                txtPatientId.Focus();

            }
        }
        #endregion

        #region cmdViewPatientHistory_Click
        private void cmdViewPatientHistory_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region cmdPatientsQueue_Click
        private void cmdPatientsQueue_Click(object sender, EventArgs e)
        {
            if (Program.IsDate(Program.gMdiForm.txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                return;
            }
            if (cboTreatmentPoint.ItemIndex == -1)
            {
                Program.Display_Error("Please select the Treatment Point and Try again");
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
                this.gCurrentPatient = this.Search_Patient();
                this.Data_Display(gCurrentPatient);
            }
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}