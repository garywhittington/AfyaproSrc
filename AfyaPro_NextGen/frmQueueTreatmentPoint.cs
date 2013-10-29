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
    public partial class frmQueueTreatmentPoint : DevExpress.XtraEditors.XtraForm
    {
        private AfyaPro_MT.clsRegistrations pMdtRegistrations;
        private Type pType;
        private string pClassName = "";

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

        private string pTreatmentPointCode = "";
        internal string TreatmentPointCode
        {
            set { pTreatmentPointCode = value; }
            get { return pTreatmentPointCode; }
        }

        private DateTime pTransDate;
        internal DateTime TransDate
        {
            set { pTransDate = value; }
            get { return pTransDate; }
        }

        private int pQueueType;
        internal int QueueType
        {
            set { pQueueType = value; }
            get { return pQueueType; }
        }

        #region frmQueueTreatmentPoint
        public frmQueueTreatmentPoint()
        {
            InitializeComponent();

            string mFunctionName = "frmQueueTreatmentPoint";

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                pMdtRegistrations = (AfyaPro_MT.clsRegistrations)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRegistrations),
                    Program.gMiddleTier + "clsRegistrations");
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmQueueTreatmentPoint_Load
        private void frmQueueTreatmentPoint_Load(object sender, EventArgs e)
        {
            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            this.Fill_Queue();
            Program.Center_Screen(this);
            this.Append_ShortcutKeys();
        }
        #endregion

        #region frmQueueTreatmentPoint_FormClosing
        private void frmQueueTreatmentPoint_FormClosing(object sender, FormClosingEventArgs e)
        {
            //layout
            if (layoutControl1.IsModified == true)
            {
                Program.Save_FormLayout(this, layoutControl1, this.Name);
            }

            Program.Save_GridLayout(grdQueueTreatmentPoint, grdQueueTreatmentPoint.Name);
        }
        #endregion

        #region Append_ShortcutKeys
        private void Append_ShortcutKeys()
        {
            cmdRefresh.Text = cmdRefresh.Text + " (" + Program.KeyCode_Refresh.ToString() + ")";
            cmdClose.Text = cmdClose.Text + " (" + Program.KeyCode_Close.ToString() + ")";
        }
        #endregion

        #region Fill_Queue
        private void Fill_Queue()
        {
            string mFunctionName = "Fill_Queue";

            try
            {
                DataTable mDtPatientsQueue = pMdtRegistrations.View_PatientsQueue(pTreatmentPointCode, pTransDate, pQueueType);
                grdQueueTreatmentPoint.DataSource = mDtPatientsQueue;

                //restore current document layout
                Program.Restore_GridLayout(grdQueueTreatmentPoint, grdQueueTreatmentPoint.Name);
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
            this.Fill_Queue();
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
                if (viewQueueTreatmentPoint.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow mDataRow = viewQueueTreatmentPoint.GetDataRow(viewQueueTreatmentPoint.FocusedRowHandle);
                pPatientCode = mDataRow["patientcode"].ToString().Trim();
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

        #region frmQueueTreatmentPoint_KeyDown
        void frmQueueTreatmentPoint_KeyDown(object sender, KeyEventArgs e)
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