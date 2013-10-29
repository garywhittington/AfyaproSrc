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
using DevExpress.XtraEditors;

namespace AfyaPro_NextGen
{
    public partial class frmCTCPendingAppt : DevExpress.XtraEditors.XtraForm
    {
        private AfyaPro_MT.clsCTCClients pMdtCTCClients;
        private Type pType;
        private string pClassName = "";

        private int pApptCode = 0;
        internal int ApptCode
        {
            set { pApptCode = value; }
            get { return pApptCode; }
        }

        private string pPatientCode = "";
        internal string PatientCode
        {
            set { pPatientCode = value; }
            get { return pPatientCode; }
        }

        private bool pPatientSelected = false;
        internal bool PatientSelected
        {
            set { pPatientSelected = value; }
            get { return pPatientSelected; }
        }

        #region frmCTCPendingAppt
        public frmCTCPendingAppt()
        {
            InitializeComponent();

            string mFunctionName = "frmCTCPendingAppt";

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                pMdtCTCClients = (AfyaPro_MT.clsCTCClients)Activator.GetObject(
                    typeof(AfyaPro_MT.clsCTCClients),
                    Program.gMiddleTier + "clsCTCClients");

                txtStartDate.EditValue = Program.IsNullDate(Program.gMdiForm.txtDate.EditValue) == true ? DateTime.Today.Date : Program.gMdiForm.txtDate.EditValue;
                txtEndDate.EditValue = Program.IsNullDate(Program.gMdiForm.txtDate.EditValue) == true ? DateTime.Today.Date : Program.gMdiForm.txtDate.EditValue;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmCTCPendingAppt_Load
        private void frmCTCPendingAppt_Load(object sender, EventArgs e)
        {
            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            this.Fill_Appointments();
            Program.Center_Screen(this);
            this.Append_ShortcutKeys();
        }
        #endregion

        #region frmCTCPendingAppt_FormClosing
        private void frmCTCPendingAppt_FormClosing(object sender, FormClosingEventArgs e)
        {
            //layout
            if (layoutControl1.IsModified == true)
            {
                Program.Save_FormLayout(this, layoutControl1, this.Name);
            }

            Program.Save_GridLayout(grdCTCPendingAppt, grdCTCPendingAppt.Name);
        }
        #endregion

        #region Append_ShortcutKeys
        private void Append_ShortcutKeys()
        {
            cmdRefresh.Text = cmdRefresh.Text + " (" + Program.KeyCode_Refresh.ToString() + ")";
            cmdClose.Text = cmdClose.Text + " (" + Program.KeyCode_Close.ToString() + ")";
        }
        #endregion

        #region Fill_Appointments
        private void Fill_Appointments()
        {
            string mFunctionName = "Fill_Appointments";

            try
            {
                int mApptStatus = -1;

                if (chkAll.Checked == true)
                {
                    mApptStatus = (int)AfyaPro_Types.clsEnums.CTC_ApptStatus.New;
                }

                DataTable mDtPatientsQueue = pMdtCTCClients.View_Appointments(
                    Convert.ToDateTime(txtStartDate.EditValue), Convert.ToDateTime(txtEndDate.EditValue),
                    mApptStatus, "apptdate,booking", grdCTCPendingAppt.Name);
                grdCTCPendingAppt.DataSource = mDtPatientsQueue;

                //restore current document layout
                Program.Restore_GridLayout(grdCTCPendingAppt, grdCTCPendingAppt.Name);
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdRefresh_Click
        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            this.Fill_Appointments();
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Get_SelectedItem
        private void Get_SelectedItem()
        {
            string mFunctionName = "Get_SelectedItem";

            try
            {
                if (viewCTCPendingAppt.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow mDataRow = viewCTCPendingAppt.GetDataRow(viewCTCPendingAppt.FocusedRowHandle);
                pPatientCode = mDataRow["patientcode"].ToString().Trim();
                pApptCode = Convert.ToInt32(mDataRow["autocode"]);
                pPatientSelected = true;

                this.Close();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region grdQueueTreatmentPoint_ProcessGridKey
        private void grdQueueTreatmentPoint_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Get_SelectedItem();
                e.Handled = true;
            }
        }
        #endregion

        #region frmCTCPendingAppt_KeyDown
        void frmCTCPendingAppt_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Program.KeyCode_Refresh:
                    {
                        cmdRefresh_Click(cmdRefresh, e);
                    }
                    break;
                case Program.KeyCode_Close:
                    {
                        cmdClose_Click(cmdClose, e);
                    }
                    break;
            }
        }
        #endregion

        #region grdQueueTreatmentPoint_DoubleClick
        private void grdQueueTreatmentPoint_DoubleClick(object sender, EventArgs e)
        {
            this.Get_SelectedItem();
        }
        #endregion
    }
}