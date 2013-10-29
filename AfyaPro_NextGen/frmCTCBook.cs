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
    public partial class frmCTCBook : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsTreatmentPoints pMdtTreatmentPoints;
        private AfyaPro_MT.clsCTCClients pMdtCTCClients;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";
        private DataTable pDtTreatmentPoints = new DataTable("treatmentpoints");

        private AfyaPro_Types.ctcBooking pBooking = new AfyaPro_Types.ctcBooking();

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private int pApptCode = 0;

        private bool pBookingDone = false;
        internal bool BookingDone
        {
            set { pBookingDone = value; }
            get { return pBookingDone; }
        }

        private AfyaPro_Types.clsCtcClient pPatient = new AfyaPro_Types.clsCtcClient();
        internal AfyaPro_Types.clsCtcClient Patient
        {
            set { pPatient = value; }
            get { return pPatient; }
        }

        #endregion

        #region frmCTCBook
        public frmCTCBook(int mApptCode)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCBook";
            this.KeyDown += new KeyEventHandler(frmCTCBook_KeyDown);

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                this.CancelButton = cmdClose;
                this.pApptCode = mApptCode;

                pMdtTreatmentPoints = (AfyaPro_MT.clsTreatmentPoints)Activator.GetObject(
                    typeof(AfyaPro_MT.clsTreatmentPoints),
                    Program.gMiddleTier + "clsTreatmentPoints");

                pMdtCTCClients = (AfyaPro_MT.clsCTCClients)Activator.GetObject(
                    typeof(AfyaPro_MT.clsCTCClients),
                    Program.gMiddleTier + "clsCTCClients");

                #region TreatmentPoints
                pDtTreatmentPoints.Columns.Add("code", typeof(System.String));
                pDtTreatmentPoints.Columns.Add("description", typeof(System.String));
                cboTreatmentPoint.Properties.DataSource = pDtTreatmentPoints;
                cboTreatmentPoint.Properties.DisplayMember = "description";
                cboTreatmentPoint.Properties.ValueMember = "code";
                #endregion

                this.Fill_LookupData();

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.opdregistrations_customizebookingform.ToString());
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

            mObjectsList.Add(txbTreatmentPoint);
            mObjectsList.Add(txbPatientId);
            mObjectsList.Add(cmdOk);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region frmCTCBook_Load
        private void frmCTCBook_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCBook_Load";

            try
            {
                Program.Restore_FormLayout(layoutControl1, this.Name);
                Program.Restore_FormSize(this);

                this.pFormWidth = this.Width;
                this.pFormHeight = this.Height;

                Program.Center_Screen(this);

                this.Load_Controls();

                this.Append_ShortcutKeys();

                if (pPatient != null)
                {
                    if (pPatient.Exist == true)
                    {
                        string mFullName = pPatient.firstname;
                        if (pPatient.othernames.Trim() != "")
                        {
                            mFullName = mFullName + " " + pPatient.othernames;
                        }
                        mFullName = mFullName + " " + pPatient.surname;

                        txtPatientId.Text = pPatient.code;
                        txtName.Text = mFullName;
                        if (pPatient.gender.Trim().ToLower() == "f")
                        {
                            txtGender.Text = "Female";
                        }
                        else
                        {
                            txtGender.Text = "Male";
                        }
                        int mDays = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue).Subtract(pPatient.birthdate).Days;
                        int mYears = (int)mDays / 365;
                        int mMonths = (int)(mDays % 365) / 30;

                        txtYears.Text = mYears.ToString();
                        txtMonths.Text = mMonths.ToString();
                    }
                }

                AfyaPro_Types.ctcBooking mBooking = pMdtCTCClients.Get_Booking(pPatient.code.Trim());
                if (mBooking != null)
                {
                    if (mBooking.IsBooked == true)
                    {
                        txtLastVisitDate.EditValue = mBooking.BookDate;
                        cboTreatmentPoint.ItemIndex = Program.Get_LookupItemIndex(cboTreatmentPoint, "code", mBooking.WhereTakenCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmCTCBook_Shown
        private void frmCTCBook_Shown(object sender, EventArgs e)
        {
            this.cboTreatmentPoint.Focus();
        }
        #endregion

        #region frmCTCBook_FormClosing
        private void frmCTCBook_FormClosing(object sender, FormClosingEventArgs e)
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
            cmdOk.Text = cmdOk.Text + " (" + Program.KeyCode_Ok.ToString() + ")";
        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            DataRow mNewRow;
            string mFunctionName = "Fill_LookupData";

            try
            {
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

        #region Ok
        private void Ok()
        {
            string mFunctionName = "Ok";

            #region validation

            if (Program.IsDate(Program.gMdiForm.txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                return;
            }
            if (cboTreatmentPoint.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_TreatmentPointDescriptionIsInvalid.ToString());
                cboTreatmentPoint.Focus();
                return;
            }

            #endregion

            try
            {
                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                pBooking = pMdtCTCClients.Booking(
                    mTransDate,
                    pPatient.code,
                    pApptCode,
                    cboTreatmentPoint.GetColumnValue("code").ToString(),
                    Program.gCurrentUser.Code);

                if (pBooking.Exe_Result == 0)
                {
                    Program.Display_Error(pBooking.Exe_Message);
                    return;
                }
                if (pBooking.Exe_Result == -1)
                {
                    Program.Display_Server_Error(pBooking.Exe_Message);
                    return;
                }

                pBookingDone = true;
                this.Close();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdOk_Click
        private void cmdOk_Click(object sender, EventArgs e)
        {
            this.Ok();
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region frmCTCBook_KeyDown
        void frmCTCBook_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Program.KeyCode_Ok:
                    {
                        this.Ok();
                    }
                    break;
            }
        }
        #endregion
    }
}