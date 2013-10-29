﻿using System;
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
    public partial class frmCTCPMTCTPostnatalVisits : DevExpress.XtraEditors.XtraForm
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

        private DataTable pDtPMTCTVisits = new DataTable("pmtctantenatalvisits");

        #endregion

        #region frmCTCPMTCTPostnatalVisits
        public frmCTCPMTCTPostnatalVisits()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCPMTCTPostnatalVisits";
            this.KeyDown += new KeyEventHandler(frmCTCPMTCTPostnatalVisits_KeyDown);

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

                viewCTCPMTCTPostnatalVisits.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;

                //visits
                pDtPMTCTVisits = pMdtCTCClients.View_PMTCTPostnatalVisits("1=2", "transdate desc,autocode desc", grdCTCPMTCTPostnatalVisits.Name);

                grdCTCPMTCTPostnatalVisits.DataSource = pDtPMTCTVisits;

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpmtctantenatal_customizelayout.ToString());
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

        #region frmCTCPMTCTPostnatalVisits_Load
        private void frmCTCPMTCTPostnatalVisits_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCPMTCTPostnatalVisits_Load";

            try
            {
                Program.Restore_FormLayout(layoutControl1, this.Name);
                Program.Restore_FormSize(this);
                Program.Restore_GridLayout(grdCTCPMTCTPostnatalVisits, grdCTCPMTCTPostnatalVisits.Name);

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

        #region frmCTCPMTCTPostnatalVisits_Shown
        private void frmCTCPMTCTPostnatalVisits_Shown(object sender, EventArgs e)
        {
            if (pSelectedPatient != null)
            {
                this.Fill_Visits();
            }
        }
        #endregion

        #region frmCTCPMTCTPostnatalVisits_FormClosing
        private void frmCTCPMTCTPostnatalVisits_FormClosing(object sender, FormClosingEventArgs e)
        {
            //layout
            if (layoutControl1.IsModified == true)
            {
                Program.Save_FormLayout(this, layoutControl1, this.Name);
            }

            Program.Save_GridLayout(grdCTCPMTCTPostnatalVisits, grdCTCPMTCTPostnatalVisits.Name);
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
                pDtPMTCTVisits.Rows.Clear();

                DataTable mDtHistory = pMdtCTCClients.View_PMTCTPostnatalVisits(
                    "patientcode='" + pSelectedPatient.code.Trim() + "' and incidencecode='" +
                    pCurrentPregRow["incidencecode"] + "'", "transdate desc,autocode desc", grdCTCPMTCTPostnatalVisits.Name);

                foreach (DataRow mDataRow in mDtHistory.Rows)
                {
                    DataRow mNewRow = pDtPMTCTVisits.NewRow();

                    foreach (DataColumn mDataColumn in pDtPMTCTVisits.Columns)
                    {
                        mNewRow[mDataColumn.ColumnName] = mDataRow[mDataColumn.ColumnName];
                    }

                    pDtPMTCTVisits.Rows.Add(mNewRow);
                    pDtPMTCTVisits.AcceptChanges();
                }

                viewCTCPMTCTPostnatalVisits.BestFitColumns();
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
            frmCTCPMTCTAntenatalVisitDetails mCTCPMTCTAntenatalVisitDetails = new frmCTCPMTCTAntenatalVisitDetails();
            mCTCPMTCTAntenatalVisitDetails.SelectedPatient = pSelectedPatient;
            mCTCPMTCTAntenatalVisitDetails.ShowDialog();

            if (mCTCPMTCTAntenatalVisitDetails.RecordSaved == true)
            {
                this.Fill_Visits();
            }
        }
        #endregion

        #region cmdEdit_Click
        private void cmdEdit_Click(object sender, EventArgs e)
        {
            if (viewCTCPMTCTPostnatalVisits.FocusedRowHandle < 0)
            {
                Program.Display_Error("Please select a visit and try again");
                grdCTCPMTCTPostnatalVisits.Focus();
                return;
            }

            DataRow mSelectedDataRow = viewCTCPMTCTPostnatalVisits.GetDataRow(viewCTCPMTCTPostnatalVisits.FocusedRowHandle);

            frmCTCPMTCTPostnatalVisitDetails mCTCPMTCTPostnatalVisitDetails = new frmCTCPMTCTPostnatalVisitDetails(mSelectedDataRow);
            mCTCPMTCTPostnatalVisitDetails.SelectedPatient = pSelectedPatient;
            mCTCPMTCTPostnatalVisitDetails.ShowDialog();

            if (mCTCPMTCTPostnatalVisitDetails.RecordSaved == true)
            {
                this.Fill_Visits();
            }
        }
        #endregion

        #region cmdDelete_Click
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdDelete_Click";

            if (viewCTCPMTCTPostnatalVisits.FocusedRowHandle < 0)
            {
                Program.Display_Error("Please select a visit and try again");
                grdCTCPMTCTPostnatalVisits.Focus();
                return;
            }

            DataRow mSelectedDataRow = viewCTCPMTCTPostnatalVisits.GetDataRow(viewCTCPMTCTPostnatalVisits.FocusedRowHandle);

            DialogResult mResult = Program.Display_Question("Delete selected record", MessageBoxDefaultButton.Button2);
            if (mResult != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }

            try
            {
                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Delete_PMTCTPostnatalVisit(
                    Convert.ToInt32(mSelectedDataRow["autocode"]),
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

        #region frmCTCPMTCTPostnatalVisits_KeyDown
        void frmCTCPMTCTPostnatalVisits_KeyDown(object sender, KeyEventArgs e)
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