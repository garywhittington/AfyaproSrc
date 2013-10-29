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
    public partial class frmDXTEpisodeVisits : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsRegistrations pMdtRegistrations;
        private AfyaPro_MT.clsPatientDiagnoses pMdtPatientDiagnoses;

        private Type pType;
        private string pClassName = "";

        private AfyaPro_Types.clsPatient pSelectedPatient = null;
        internal AfyaPro_Types.clsPatient SelectedPatient
        {
            set { pSelectedPatient = value; }
            get { return pSelectedPatient; }
        }

        private DataRow pEpisodeRow = null;
        internal DataRow EpisodeRow
        {
            set { pEpisodeRow = value; }
            get { return pEpisodeRow; }
        }

        private bool pRecordSaved = false;
        internal bool RecordSaved
        {
            set { pRecordSaved = value; }
            get { return pRecordSaved; }
        }

        private DataTable pDtVisits = new DataTable("patientcasevisits");

        #endregion

        #region frmDXTEpisodeVisits
        public frmDXTEpisodeVisits()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmDXTEpisodeVisits";

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                this.CancelButton = cmdClose;

                pMdtRegistrations = (AfyaPro_MT.clsRegistrations)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRegistrations),
                    Program.gMiddleTier + "clsRegistrations");

                pMdtPatientDiagnoses = (AfyaPro_MT.clsPatientDiagnoses)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPatientDiagnoses),
                    Program.gMiddleTier + "clsPatientDiagnoses");

                viewDXTEpisodeVisits.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;

                //visits
                pDtVisits = pMdtPatientDiagnoses.View_EpisodeVisits("1=2", "transdate desc,autocode desc", grdDXTEpisodeVisits.Name);

                grdDXTEpisodeVisits.DataSource = pDtVisits;

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.dxtpatientdiagnoses_customizelayout.ToString());
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

            mObjectsList.Add(cmdNew);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region frmCTCAppointmentVisits_Load
        private void frmCTCAppointmentVisits_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCAppointmentVisits_Load";

            try
            {
                Program.Restore_FormLayout(layoutControl1, this.Name);
                Program.Restore_FormSize(this);
                Program.Restore_GridLayout(grdDXTEpisodeVisits, grdDXTEpisodeVisits.Name);


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

        #region frmCTCAppointmentVisits_Shown
        private void frmCTCAppointmentVisits_Shown(object sender, EventArgs e)
        {
            if (pSelectedPatient != null)
            {
                this.Fill_Visits();
            }
        }
        #endregion

        #region frmCTCAppointmentVisits_FormClosing
        private void frmCTCAppointmentVisits_FormClosing(object sender, FormClosingEventArgs e)
        {
            //layout
            if (layoutControl1.IsModified == true)
            {
                Program.Save_FormLayout(this, layoutControl1, this.Name);
            }

            Program.Save_GridLayout(grdDXTEpisodeVisits, grdDXTEpisodeVisits.Name);
        }
        #endregion

        #region Append_ShortcutKeys
        private void Append_ShortcutKeys()
        {
            //cmdOk.Text = cmdOk.Text + " (" + Program.KeyCode_Ok.ToString() + ")";
        }
        #endregion

        #region Fill_Visits
        private void Fill_Visits()
        {
            string mFunctionName = "Fill_Visits";

            try
            {
                pDtVisits.Rows.Clear();

                DataTable mDtHistory = pMdtPatientDiagnoses.View_EpisodeVisits(
                    "episodecode='" + pEpisodeRow["episodecode"].ToString().Trim() + "'", "transdate desc,autocode desc", grdDXTEpisodeVisits.Name);

                foreach (DataRow mDataRow in mDtHistory.Rows)
                {
                    DataRow mNewRow = pDtVisits.NewRow();

                    foreach (DataColumn mDataColumn in pDtVisits.Columns)
                    {
                        mNewRow[mDataColumn.ColumnName] = mDataRow[mDataColumn.ColumnName];
                    }

                    pDtVisits.Rows.Add(mNewRow);
                    pDtVisits.AcceptChanges();
                }

                viewDXTEpisodeVisits.BestFitColumns();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
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

           
            #region validate current booking

            DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

            AfyaPro_Types.clsBooking mCurrentBooking = pMdtRegistrations.Get_Booking(pSelectedPatient.code);

            if (mCurrentBooking.BookDate.Date != mTransDate.Date)
            {
                //retry getting booking
                mCurrentBooking = pMdtRegistrations.Get_Booking(pSelectedPatient.code);
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
            mDXTEpisodeVisitDetails.gCurrentPatient = pSelectedPatient;
            mDXTEpisodeVisitDetails.EpisodeCode = pEpisodeRow["episodecode"].ToString().Trim();
            mDXTEpisodeVisitDetails.txtDxCode.Text = pEpisodeRow["diagnosiscode"].ToString().Trim();
            mDXTEpisodeVisitDetails.txtDxDescription.Text = pEpisodeRow["diagnosisdescription"].ToString().Trim();
            mDXTEpisodeVisitDetails.radPrimary.SelectedIndex = Convert.ToInt32(pEpisodeRow["isprimary"]) == 1 ? 0 : 1;
            mDXTEpisodeVisitDetails.txtDxCode.Properties.ReadOnly = true;
            mDXTEpisodeVisitDetails.radPrimary.Properties.ReadOnly = true;
            mDXTEpisodeVisitDetails.cmdSearchDx.Enabled = false;
            mDXTEpisodeVisitDetails.ShowDialog();

            if (mDXTEpisodeVisitDetails.RecordSaved == true)
            {
                pRecordSaved = mDXTEpisodeVisitDetails.RecordSaved;
                this.Close();
            }
        }
        #endregion

        #region cmdEdit_Click
        private void cmdEdit_Click(object sender, EventArgs e)
        {
            if (viewDXTEpisodeVisits.FocusedRowHandle < 0)
            {
                Program.Display_Error("Please select a visit and try again");
                grdDXTEpisodeVisits.Focus();
                return;
            }

            DataRow mSelectedDataRow = viewDXTEpisodeVisits.GetDataRow(viewDXTEpisodeVisits.FocusedRowHandle);

            frmDXTEpisodeVisitDetails mDXTEpisodeVisitDetails = new frmDXTEpisodeVisitDetails("Edit");
            mDXTEpisodeVisitDetails.gCurrentPatient = pSelectedPatient;
            mDXTEpisodeVisitDetails.SelectedAutoCode = Convert.ToInt32(mSelectedDataRow["autocode"]);
            mDXTEpisodeVisitDetails.EpisodeCode = mSelectedDataRow["episodecode"].ToString().Trim();
            mDXTEpisodeVisitDetails.txtDate.EditValue = Convert.ToDateTime(mSelectedDataRow["transdate"]);
            mDXTEpisodeVisitDetails.DoctorCode = mSelectedDataRow["doctorcode"].ToString().Trim();
            mDXTEpisodeVisitDetails.txtDxCode.Text = mSelectedDataRow["diagnosiscode"].ToString().Trim();
            mDXTEpisodeVisitDetails.txtDxDescription.Text = mSelectedDataRow["diagnosisdescription"].ToString().Trim();
            mDXTEpisodeVisitDetails.radPrimary.SelectedIndex = Convert.ToInt32(mSelectedDataRow["isprimary"]) == 1 ? 0 : 1;
            mDXTEpisodeVisitDetails.txtHistory.Text = mSelectedDataRow["history"].ToString().Trim();
            mDXTEpisodeVisitDetails.txtExamination.Text = mSelectedDataRow["examination"].ToString().Trim();
            mDXTEpisodeVisitDetails.txtInvestigation.Text = mSelectedDataRow["investigation"].ToString().Trim();
            mDXTEpisodeVisitDetails.txtTreatments.Text = mSelectedDataRow["treatments"].ToString().Trim();
            mDXTEpisodeVisitDetails.ShowDialog();

            if (mDXTEpisodeVisitDetails.RecordSaved == true)
            {
                pRecordSaved = mDXTEpisodeVisitDetails.RecordSaved;
                this.Close();
            }
        }
        #endregion

        #region cmdDelete_Click
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdDelete_Click";

            if (viewDXTEpisodeVisits.FocusedRowHandle < 0)
            {
                Program.Display_Error("Please select a visit and try again");
                grdDXTEpisodeVisits.Focus();
                return;
            }

            DataRow mSelectedDataRow = viewDXTEpisodeVisits.GetDataRow(viewDXTEpisodeVisits.FocusedRowHandle);

            DialogResult mResult = Program.Display_Question("Delete selected record", MessageBoxDefaultButton.Button2);
            if (mResult != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }

            try
            {
                AfyaPro_Types.clsResult mCtcClient = pMdtPatientDiagnoses.Delete(
                    Convert.ToInt32(mSelectedDataRow["autocode"]),
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

                pRecordSaved = true;
                this.Fill_Visits();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region frmDXTEpisodeVisits_KeyDown
        void frmDXTEpisodeVisits_KeyDown(object sender, KeyEventArgs e)
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