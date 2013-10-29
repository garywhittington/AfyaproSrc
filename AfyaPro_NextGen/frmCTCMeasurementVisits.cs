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
    public partial class frmCTCMeasurementVisits : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        //private AfyaPro_MT.clsReporter pMdtReporter;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_MT.clsCTCClients pMdtCTCClients;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        //private AfyaPro_Types.ctcBooking pBooking = new AfyaPro_Types.ctcBooking();

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private AfyaPro_Types.clsCtcClient pSelectedPatient = null;
        internal AfyaPro_Types.clsCtcClient SelectedPatient
        {
            set { pSelectedPatient = value; }
            get { return pSelectedPatient; }
        }

        private DataRow pCurrentPregRow = null;
        internal DataRow CurrentPregRow
        {
            set { pCurrentPregRow = value; }
            get { return pCurrentPregRow; }
        }

        private DataTable pDtVisits = new DataTable("pmtctantenatalvisits");

        #endregion

        #region frmCTCMeasurementVisits
        public frmCTCMeasurementVisits()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCMeasurementVisits";
            this.KeyDown += new KeyEventHandler(frmCTCMeasurementVisits_KeyDown);

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

                viewCTCMeasurements.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;

                //visits
                pDtVisits = pMdtCTCClients.View_Measurements("1=2", "transdate desc,autocode desc", grdCTCMeasurements.Name);

                grdCTCMeasurements.DataSource = pDtVisits;

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcmeasurements_customizelayout.ToString());
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

        #region frmCTCMeasurementVisits_Load
        private void frmCTCMeasurementVisits_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCMeasurementVisits_Load";

            try
            {
                Program.Restore_FormLayout(layoutControl1, this.Name);
                Program.Restore_FormSize(this);
                Program.Restore_GridLayout(grdCTCMeasurements, grdCTCMeasurements.Name);

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

        #region frmCTCMeasurementVisits_Shown
        private void frmCTCMeasurementVisits_Shown(object sender, EventArgs e)
        {
            if (pSelectedPatient != null)
            {
                this.Fill_Visits();
            }
        }
        #endregion

        #region frmCTCMeasurementVisits_FormClosing
        private void frmCTCMeasurementVisits_FormClosing(object sender, FormClosingEventArgs e)
        {
            //layout
            if (layoutControl1.IsModified == true)
            {
                Program.Save_FormLayout(this, layoutControl1, this.Name);
            }

            Program.Save_GridLayout(grdCTCMeasurements, grdCTCMeasurements.Name);
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

                DataTable mDtHistory = pMdtCTCClients.View_Measurements(
                    "patientcode='" + pSelectedPatient.code.Trim() + "'", "transdate desc,autocode desc", grdCTCMeasurements.Name);

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

                viewCTCMeasurements.BestFitColumns();
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
            frmCTCMeasurementDetails mCTCMeasurementDetails = new frmCTCMeasurementDetails();
            mCTCMeasurementDetails.SelectedPatient = pSelectedPatient;
            mCTCMeasurementDetails.ShowDialog();

            if (mCTCMeasurementDetails.RecordSaved == true)
            {
                this.Fill_Visits();
            }
        }
        #endregion

        #region cmdEdit_Click
        private void cmdEdit_Click(object sender, EventArgs e)
        {
            if (viewCTCMeasurements.FocusedRowHandle < 0)
            {
                Program.Display_Error("Please select a visit and try again");
                grdCTCMeasurements.Focus();
                return;
            }

            DataRow mSelectedDataRow = viewCTCMeasurements.GetDataRow(viewCTCMeasurements.FocusedRowHandle);

            frmCTCMeasurementDetails mCTCMeasurementDetails = new frmCTCMeasurementDetails(mSelectedDataRow);
            mCTCMeasurementDetails.SelectedPatient = pSelectedPatient;
            mCTCMeasurementDetails.ShowDialog();

            if (mCTCMeasurementDetails.RecordSaved == true)
            {
                this.Fill_Visits();
            }
        }
        #endregion

        #region cmdDelete_Click
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdDelete_Click";

            if (viewCTCMeasurements.FocusedRowHandle < 0)
            {
                Program.Display_Error("Please select a visit and try again");
                grdCTCMeasurements.Focus();
                return;
            }

            DataRow mSelectedDataRow = viewCTCMeasurements.GetDataRow(viewCTCMeasurements.FocusedRowHandle);

            DialogResult mResult = Program.Display_Question("Delete selected record", MessageBoxDefaultButton.Button2);
            if (mResult != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }

            try
            {
                AfyaPro_Types.clsResult mCtcClient = pMdtCTCClients.Delete_Triage(
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

        #region frmCTCMeasurementVisits_KeyDown
        void frmCTCMeasurementVisits_KeyDown(object sender, KeyEventArgs e)
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