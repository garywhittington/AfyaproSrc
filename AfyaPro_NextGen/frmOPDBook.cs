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
using GsmComm.PduConverter;
using GsmComm.GsmCommunication;

namespace AfyaPro_NextGen
{
    public partial class frmOPDBook : DevExpress.XtraEditors.XtraForm    
    {

        #region declaration

        private AfyaPro_MT.clsTreatmentPoints pMdtTreatmentPoints;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";
        private DataTable pDtTreatmentPoints = new DataTable("treatmentpoints");

        internal bool gBillingDone = false;
        private DevExpress.XtraEditors.XtraForm pSourceForm;

        private int pFormWidth = 0;
        private int pFormHeight = 0;
        
        #endregion

        #region frmOPDBook
        public frmOPDBook(DevExpress.XtraEditors.XtraForm mSourceForm, bool mAllowCharging)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmOPDBook";
            this.KeyDown += new KeyEventHandler(frmOPDBook_KeyDown);

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                
                this.CancelButton = cmdClose;
                this.pSourceForm = mSourceForm;

                pMdtTreatmentPoints = (AfyaPro_MT.clsTreatmentPoints)Activator.GetObject(
                    typeof(AfyaPro_MT.clsTreatmentPoints),
                    Program.gMiddleTier + "clsTreatmentPoints");

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

                //this section is for disabling the billing featues

                //chkCharge.Checked = Program.GrantDeny_FunctionAccess(
                //       AfyaPro_Types.clsSystemFunctions.FunctionKeys.opdregistrations_promptforbilling.ToString());

                //chkCharge.Properties.ReadOnly = !Program.GrantDeny_FunctionAccess(
                //    AfyaPro_Types.clsSystemFunctions.FunctionKeys.opdregistrations_changechargestatus.ToString());

                if (mAllowCharging != false)
                {

                    chkCharge.Checked = Program.GrantDeny_FunctionAccess(
                        AfyaPro_Types.clsSystemFunctions.FunctionKeys.opdregistrations_promptforbilling.ToString());

                    chkCharge.Properties.ReadOnly = !Program.GrantDeny_FunctionAccess(
                        AfyaPro_Types.clsSystemFunctions.FunctionKeys.opdregistrations_changechargestatus.ToString());
                }
                else
                {
                    chkCharge.Visible = false;
                }


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
            mObjectsList.Add(txbWeight);
            mObjectsList.Add(txbTemperature);
            mObjectsList.Add(cmdOk);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region frmOPDBook_Load
        private void frmOPDBook_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmOPDBook_Load";

            try
            {
                frmOPDRegistrations mOPDRegistrations = (frmOPDRegistrations)this.pSourceForm;

                txtLastVisitDate.EditValue = mOPDRegistrations.gLastAttendanceDate;
                cboTreatmentPoint.ItemIndex = Program.Get_LookupItemIndex(cboTreatmentPoint, "code", mOPDRegistrations.gTreatmentPointCode);
                txtWeight.Text = mOPDRegistrations.gWeight.ToString();
                txtTemperature.Text = mOPDRegistrations.gTemperature.ToString();

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

        #region frmOPDBook_FormClosing
        private void frmOPDBook_FormClosing(object sender, FormClosingEventArgs e)
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

            if (cboTreatmentPoint.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_TreatmentPointDescriptionIsInvalid.ToString());
                cboTreatmentPoint.Focus();
                return;
            }

            #endregion

            try
            {
                frmOPDRegistrations mOPDRegistrations = (frmOPDRegistrations)pSourceForm;

                mOPDRegistrations.gTreatmentPointCode = cboTreatmentPoint.GetColumnValue("code").ToString().Trim();
                mOPDRegistrations.gTreatmentPointDescription = cboTreatmentPoint.GetColumnValue("description").ToString().Trim();
                mOPDRegistrations.gWeight = 0;
                mOPDRegistrations.gTemperature = 0;

                if (Program.IsMoney(txtWeight.Text) == true)
                {
                    mOPDRegistrations.gWeight = Convert.ToDouble(txtWeight.Text);
                }

                if (Program.IsMoney(txtTemperature.Text) == true)
                {
                    mOPDRegistrations.gTemperature = Convert.ToDouble(txtTemperature.Text);
                }

                mOPDRegistrations.gBillingOk = true;

                if (chkCharge.Checked == true)
                {
                    mOPDRegistrations.gBillingOk = false;

                    #region billing and booking together

                    frmBILBillPosting mBILBillPosting = new frmBILBillPosting();
                    mBILBillPosting.gOpdRegistration = mOPDRegistrations;

                    mBILBillPosting.gCurrentBooking.BillingGroupCode = mOPDRegistrations.gBillingGroupCode;
                    mBILBillPosting.gCurrentBooking.BillingGroupDescription = mOPDRegistrations.gBillingGroupDescription;
                    mBILBillPosting.gCurrentBooking.BillingSubGroupCode = mOPDRegistrations.gBillingSubGroupCode;
                    mBILBillPosting.gCurrentBooking.BillingSubGroupDescription = mOPDRegistrations.gBillingSubGroupDescription;
                    mBILBillPosting.gCurrentBooking.BillingGroupMembershipNo = mOPDRegistrations.gBillingGroupMembershipNo;
                    mBILBillPosting.gCurrentBooking.WhereTakenCode = mOPDRegistrations.gTreatmentPointCode;
                    mBILBillPosting.gCurrentBooking.WhereTaken = mOPDRegistrations.gTreatmentPointDescription;
                    mBILBillPosting.gCurrentBooking.PatientCode = mOPDRegistrations.txtPatientId.Text.Trim();
                    mBILBillPosting.gCurrentBooking.IsBooked = true;
                    mBILBillPosting.gCurrentBooking.BookDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);
                    mBILBillPosting.gCurrentPatient = mOPDRegistrations.gCurrentPatient;

                    mBILBillPosting.gCurrentBooking.PriceName = Program.gCurrentUser.DefaultPriceCategoryCode;

                    mBILBillPosting.ShowDialog();

                    #endregion

                    gBillingDone = true;
                }

                if (mOPDRegistrations.gBillingOk == false)
                {
                    return;
                }

                mOPDRegistrations.gBookingOk = true;

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

        #region frmOPDBook_KeyDown
        void frmOPDBook_KeyDown(object sender, KeyEventArgs e)
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