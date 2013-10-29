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
    public partial class frmCTCPMTCTANCVisits : DevExpress.XtraEditors.XtraForm
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

        private DataTable pDtVisits = new DataTable("artvisits");

        #endregion

        #region frmCTCPMTCTANCVisits
        public frmCTCPMTCTANCVisits()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCPMTCTANCVisits";
            this.KeyDown += new KeyEventHandler(frmCTCPMTCTANCVisits_KeyDown);

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

                viewCTCPMTCTANCVisits.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;

                //visits
                pDtVisits = pMdtCTCClients.View_PMTCTCareVisits("1=2", "transdate desc,autocode desc", grdCTCPMTCTANCVisits.Name);

                grdCTCPMTCTANCVisits.DataSource = pDtVisits;

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpmtctanc_customizelayout.ToString());
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

        #region frmCTCPMTCTANCVisits_Load
        private void frmCTCPMTCTANCVisits_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCPMTCTANCVisits_Load";

            try
            {
                Program.Restore_FormLayout(layoutControl1, this.Name);
                Program.Restore_FormSize(this);
                Program.Restore_GridLayout(grdCTCPMTCTANCVisits, grdCTCPMTCTANCVisits.Name);

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

        #region frmCTCPMTCTANCVisits_Shown
        private void frmCTCPMTCTANCVisits_Shown(object sender, EventArgs e)
        {
            if (pSelectedPatient != null)
            {
                this.Fill_Visits();
            }
        }
        #endregion

        #region frmCTCPMTCTANCVisits_FormClosing
        private void frmCTCPMTCTANCVisits_FormClosing(object sender, FormClosingEventArgs e)
        {
            //layout
            if (layoutControl1.IsModified == true)
            {
                Program.Save_FormLayout(this, layoutControl1, this.Name);
            }

            Program.Save_GridLayout(grdCTCPMTCTANCVisits, grdCTCPMTCTANCVisits.Name);
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

                DataTable mDtHistory = pMdtCTCClients.View_PMTCTCareVisits(
                    "patientcode='" + pSelectedPatient.code.Trim() + "'", "transdate desc,autocode desc", grdCTCPMTCTANCVisits.Name);

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

                viewCTCPMTCTANCVisits.BestFitColumns();
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
            bool mFirstVisit = false;
            //check if first visit
            DataTable mDtHistory = pMdtCTCClients.View_PMTCTCareVisits(
                "patientcode='" + pSelectedPatient.code.Trim() + "'", "transdate desc,autocode desc", "");
            if (mDtHistory.Rows.Count == 0)
            {
                mFirstVisit = true;
            }

            if (mFirstVisit == true)
            {
                frmCTCPMTCTANCVisitDetails1 mCTCPMTCTANCVisitDetails1 = new frmCTCPMTCTANCVisitDetails1();
                mCTCPMTCTANCVisitDetails1.SelectedPatient = pSelectedPatient;
                mCTCPMTCTANCVisitDetails1.ShowDialog();

                if (mCTCPMTCTANCVisitDetails1.RecordSaved == true)
                {
                    this.Fill_Visits();
                }
            }
            else
            {
                frmCTCPMTCTANCVisitDetails2 mCTCPMTCTANCVisitDetails2 = new frmCTCPMTCTANCVisitDetails2();
                mCTCPMTCTANCVisitDetails2.SelectedPatient = pSelectedPatient;
                mCTCPMTCTANCVisitDetails2.ShowDialog();

                if (mCTCPMTCTANCVisitDetails2.RecordSaved == true)
                {
                    this.Fill_Visits();
                }
            }
        }
        #endregion

        #region cmdEdit_Click
        private void cmdEdit_Click(object sender, EventArgs e)
        {
            if (viewCTCPMTCTANCVisits.FocusedRowHandle < 0)
            {
                Program.Display_Error("Please select a visit and try again");
                grdCTCPMTCTANCVisits.Focus();
                return;
            }

            DataRow mSelectedDataRow = viewCTCPMTCTANCVisits.GetDataRow(viewCTCPMTCTANCVisits.FocusedRowHandle);

            if (Convert.ToInt32(mSelectedDataRow["isfirstvisit"]) == 1)
            {
                frmCTCPMTCTANCVisitDetails1 mCTCPMTCTANCVisitDetails1 = new frmCTCPMTCTANCVisitDetails1(mSelectedDataRow);
                mCTCPMTCTANCVisitDetails1.SelectedPatient = pSelectedPatient;
                mCTCPMTCTANCVisitDetails1.ShowDialog();

                if (mCTCPMTCTANCVisitDetails1.RecordSaved == true)
                {
                    this.Fill_Visits();
                }
            }
            else
            {
                frmCTCPMTCTANCVisitDetails2 mCTCPMTCTANCVisitDetails2 = new frmCTCPMTCTANCVisitDetails2(mSelectedDataRow);
                mCTCPMTCTANCVisitDetails2.SelectedPatient = pSelectedPatient;
                mCTCPMTCTANCVisitDetails2.ShowDialog();

                if (mCTCPMTCTANCVisitDetails2.RecordSaved == true)
                {
                    this.Fill_Visits();
                }
            }
        }
        #endregion

        #region cmdDelete_Click
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdDelete_Click";

            if (viewCTCPMTCTANCVisits.FocusedRowHandle < 0)
            {
                Program.Display_Error("Please select a visit and try again");
                grdCTCPMTCTANCVisits.Focus();
                return;
            }

            DataRow mSelectedDataRow = viewCTCPMTCTANCVisits.GetDataRow(viewCTCPMTCTANCVisits.FocusedRowHandle);

            DialogResult mResult = Program.Display_Question("Delete selected record", MessageBoxDefaultButton.Button2);
            if (mResult != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }

            try
            {
                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Delete_PMTCTANCVisit(
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

        #region frmCTCPMTCTANCVisits_KeyDown
        void frmCTCPMTCTANCVisits_KeyDown(object sender, KeyEventArgs e)
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