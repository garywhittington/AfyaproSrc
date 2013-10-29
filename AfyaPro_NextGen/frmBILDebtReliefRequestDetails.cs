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
    public partial class frmBILDebtReliefRequestDetails : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_MT.clsBillDebtReliefs pMdtBillDebtReliefs;
        private AfyaPro_MT.clsBilling pMdtBilling;
        private AfyaPro_MT.clsReporter pMdtReporter;
        private AfyaPro_MT.clsRegistrations pMdtRegistrations;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataTable pDtInvoices = new DataTable("invoices");

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private int pRequestStatus = -1;

        #endregion

        #region frmBILDebtReliefRequestDetails
        public frmBILDebtReliefRequestDetails(int mRequestStatus)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmBILDebtReliefRequestDetails";

            try
            {
                this.pRequestStatus = mRequestStatus;
                this.Icon = Program.gMdiForm.Icon;
                this.CancelButton = cmdClose;

                pMdtBillDebtReliefs = (AfyaPro_MT.clsBillDebtReliefs)Activator.GetObject(
                    typeof(AfyaPro_MT.clsBillDebtReliefs),
                    Program.gMiddleTier + "clsBillDebtReliefs");

                pMdtBilling = (AfyaPro_MT.clsBilling)Activator.GetObject(
                    typeof(AfyaPro_MT.clsBilling),
                    Program.gMiddleTier + "clsBilling");

                pMdtAutoCodes = (AfyaPro_MT.clsAutoCodes)Activator.GetObject(
                    typeof(AfyaPro_MT.clsAutoCodes),
                    Program.gMiddleTier + "clsAutoCodes");

                pMdtReporter = (AfyaPro_MT.clsReporter)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReporter),
                    Program.gMiddleTier + "clsReporter");

                pMdtRegistrations = (AfyaPro_MT.clsRegistrations)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRegistrations),
                    Program.gMiddleTier + "clsRegistrations");

                pDtInvoices.Columns.Add("transdate", typeof(System.DateTime));
                pDtInvoices.Columns.Add("invoiceno", typeof(System.String));
                pDtInvoices.Columns.Add("balancedue", typeof(System.Double));
                pDtInvoices.Columns.Add("requestedamount", typeof(System.Double));
                pDtInvoices.Columns.Add("preview", typeof(System.String));
                pDtInvoices.Columns.Add("booking", typeof(System.String));
                grdInvoices.DataSource = pDtInvoices;

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.bilpostbills_customizefinalpayment.ToString());
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmBILDebtReliefRequestDetails_Load
        private void frmBILDebtReliefRequestDetails_Load(object sender, EventArgs e)
        {
            this.Top = 0;

            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            int mGenerateRefNo = pMdtAutoCodes.Auto_Generate_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.receiptno));
            if (mGenerateRefNo == -1)
            {
                Program.Display_Server_Error("");
                return;
            }
            txtReceiptNo.Text = "";
            if (mGenerateRefNo == 1)
            {
                txtReceiptNo.Text = "<<---New--->>";
                txtReceiptNo.Properties.ReadOnly = true;
            }
            else
            {
                txtReceiptNo.Properties.ReadOnly = false;
            }

            this.Load_Controls();

            this.pFormWidth = this.Width;
            this.pFormHeight = this.Height;

            Program.Center_Screen(this);

            this.Fill_Invoices();
        }
        #endregion

        #region frmBILDebtReliefRequestDetails_Shown
        private void frmBILDebtReliefRequestDetails_Shown(object sender, EventArgs e)
        {
            txtRemarks.Focus();
        }
        #endregion

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(txbBalance);
            mObjectsList.Add(cmdApprove);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region Fill_Invoices
        private void Fill_Invoices()
        {
            string mFunctionName = "Fill_Invoices";

            try
            {
                pDtInvoices.Rows.Clear();

                DataTable mDtInvoices = pMdtBillDebtReliefs.View_Invoices("requestno='" + txtRequestNo.Text.Trim() + "'", "", "", "");

                double mTotalRequestedAmount = 0;
                foreach (DataRow mDataRow in mDtInvoices.Rows)
                {
                    DataRow mNewRow = pDtInvoices.NewRow();
                    mNewRow["transdate"] = mDataRow["transdate"];
                    mNewRow["invoiceno"] = mDataRow["invoiceno"];
                    mNewRow["balancedue"] = mDataRow["balancedue"];
                    mNewRow["requestedamount"] = mDataRow["requestedamount"];
                    mNewRow["preview"] = "Preview Original Invoice";
                    mNewRow["booking"] = mDataRow["booking"];
                    pDtInvoices.Rows.Add(mNewRow);
                    pDtInvoices.AcceptChanges();

                    mTotalRequestedAmount = mTotalRequestedAmount + Convert.ToDouble(mDataRow["requestedamount"]);
                }

                txtRequestedAmount.Text = mTotalRequestedAmount.ToString("c");
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

        #region cmdApprove_Click
        private void cmdApprove_Click(object sender, EventArgs e)
        {
            AfyaPro_Types.clsBill mBill = new AfyaPro_Types.clsBill();
            Int16 mGenerateReceiptNo = 0;

            string mFunctionName = "cmdApprove_Click";

            #region validation

            if (Program.IsDate(Program.gMdiForm.txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                return;
            }

            if (txtAccountNo.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoIsInvalid.ToString());
                txtAccountNo.Focus();
                return;
            }

            AfyaPro_Types.clsPatient mPatient = pMdtRegistrations.Get_Patient(txtAccountNo.Text);
            if (mPatient.Exist == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoDoesNotExist.ToString());
                txtAccountNo.Focus();
                return;
            }

            if (pRequestStatus != Convert.ToInt16(AfyaPro_Types.clsEnums.DebtReliefRequestStatus.Open))
            {
                Program.Display_Info("Sorry, this request can not be approved. Either it has already been approved or rejected");
                return;
            }

            #endregion

            try
            {
                this.Cursor = Cursors.WaitCursor;

                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                DialogResult mDlg = Program.Display_Question("Are you sure you want to approve this request", MessageBoxDefaultButton.Button2);
                if (mDlg != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }

                DataTable mDtInvoices = new DataTable("invoices");
                mDtInvoices.Columns.Add("invoiceno", typeof(System.String));
                mDtInvoices.Columns.Add("balancedue", typeof(System.Double));
                mDtInvoices.Columns.Add("paidforbill", typeof(System.Double));
                mDtInvoices.Columns.Add("billinggroupcode", typeof(System.String));
                mDtInvoices.Columns.Add("billinggroupdescription", typeof(System.String));
                mDtInvoices.Columns.Add("billingsubgroupcode", typeof(System.String));
                mDtInvoices.Columns.Add("billingsubgroupdescription", typeof(System.String));
                mDtInvoices.Columns.Add("billinggroupmembershipno", typeof(System.String));
                mDtInvoices.Columns.Add("booking", typeof(System.String));

                double mTotalPaid = 0;
                foreach (DataRow mDataRow in pDtInvoices.Rows)
                {
                    double mInvoiceBalanceDue = Convert.ToDouble(mDataRow["balancedue"]);
                    double mPaidForBill = Convert.ToDouble(mDataRow["requestedamount"]);

                    DataRow mNewRow = mDtInvoices.NewRow();
                    mNewRow["invoiceno"] = mDataRow["invoiceno"].ToString().Trim();
                    mNewRow["balancedue"] = mInvoiceBalanceDue;
                    mNewRow["paidforbill"] = mPaidForBill;
                    mNewRow["billinggroupcode"] = "";
                    mNewRow["billinggroupdescription"] = "";
                    mNewRow["billingsubgroupcode"] = "";
                    mNewRow["billingsubgroupdescription"] = "";
                    mNewRow["billinggroupmembershipno"] = "";
                    mNewRow["booking"] = mDataRow["booking"].ToString().Trim();
                    mDtInvoices.Rows.Add(mNewRow);
                    mDtInvoices.AcceptChanges();

                    mTotalPaid = mTotalPaid + mPaidForBill;
                }

                DataTable mDtDebtRelief = new DataTable("debtrelief");
                mDtDebtRelief.Columns.Add("approvalremarks", typeof(System.String));
                mDtDebtRelief.Columns.Add("requestno", typeof(System.String));

                DataRow mNewReliefRow = mDtDebtRelief.NewRow();
                mNewReliefRow["approvalremarks"] = txtRemarks.Text;
                mNewReliefRow["requestno"] = txtRequestNo.Text;
                mDtDebtRelief.Rows.Add(mNewReliefRow);
                mDtDebtRelief.AcceptChanges();

                if (txtReceiptNo.Text.Trim().ToLower() == "<<---new--->>")
                {
                    mGenerateReceiptNo = 1;
                }

                mBill = pMdtBilling.Pay_FromPatient(mGenerateReceiptNo, txtReceiptNo.Text, mTransDate, txtAccountNo.Text,
                    "dbtrel", "", "", "", "", mTotalPaid, 0, "", "", mDtInvoices, Program.gLocalCurrencyCode, Program.gLocalCurrencyDesc,
                    Program.gLocalCurrencySymbol, mDtDebtRelief, Program.gCurrentUser.Code);
                if (mBill.Exe_Result == 0)
                {
                    Program.Display_Error(mBill.Exe_Message);
                    return;
                }
                if (mBill.Exe_Result == -1)
                {
                    Program.Display_Server_Error(mBill.Exe_Message);
                    return;
                }

                Program.Display_Info("Debt relief request has been approved successfully");

                //#region receipt printing

                //if (mBill.ReceiptNo.Trim() != "")
                //{
                //    frmOPDPatientDocuments mOPDPatientDocuments = new frmOPDPatientDocuments();
                //    mOPDPatientDocuments.AutoPrint_InvoicePayment(mBill.ReceiptNo);
                //}

                //#endregion

                this.Close();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        #endregion

        #region cmdReject_Click
        private void cmdReject_Click(object sender, EventArgs e)
        {
            AfyaPro_Types.clsBill mBill = new AfyaPro_Types.clsBill();

            string mFunctionName = "cmdReject_Click";

            #region validation

            if (Program.IsDate(Program.gMdiForm.txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                return;
            }

            if (txtAccountNo.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoIsInvalid.ToString());
                txtAccountNo.Focus();
                return;
            }

            AfyaPro_Types.clsPatient mPatient = pMdtRegistrations.Get_Patient(txtAccountNo.Text);
            if (mPatient.Exist == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoDoesNotExist.ToString());
                txtAccountNo.Focus();
                return;
            }

            if (pRequestStatus != Convert.ToInt16(AfyaPro_Types.clsEnums.DebtReliefRequestStatus.Open))
            {
                Program.Display_Info("Sorry, this request can not be rejected. Either it has already been approved or rejected");
                return;
            }

            #endregion

            try
            {
                this.Cursor = Cursors.WaitCursor;

                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                DialogResult mDlg = Program.Display_Question("Are you sure you want to reject this request", MessageBoxDefaultButton.Button2);
                if (mDlg != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }

                mBill = pMdtBillDebtReliefs.Reject(txtRequestNo.Text, mTransDate, txtAccountNo.Text, txtRemarks.Text, Program.gCurrentUser.Code);
                if (mBill.Exe_Result == 0)
                {
                    Program.Display_Error(mBill.Exe_Message);
                    return;
                }
                if (mBill.Exe_Result == -1)
                {
                    Program.Display_Server_Error(mBill.Exe_Message);
                    return;
                }

                Program.Display_Info("Debt relief request has been rejected successfully");

                this.Close();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region linkPreview_Click
        private void linkPreview_Click(object sender, EventArgs e)
        {
            string mFunctionName = "linkPreview_Click";

            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (viewInvoices.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow mSelectedRow = viewInvoices.GetDataRow(viewInvoices.FocusedRowHandle);

                string mFilter = "invoiceno='" + mSelectedRow["invoiceno"].ToString() + "'";
                DataTable mDtData = pMdtReporter.Get_DataFromDBView("view_billinvoicedoc", null, mFilter, "");

                string mFileName = "BILL_Invoice";
                byte[] mBytes = pMdtReporter.Load_ReportTemplate(mFileName, false);

                DevExpress.XtraReports.UI.XtraReport mReportDoc = Program.Load_ReportTemplate(mBytes, mDtData);

                if (mReportDoc == null)
                {
                    return;
                }

                frmReportViewer mReportViewer = new frmReportViewer();
                mReportViewer.printControl1.PrintingSystem = mReportDoc.PrintingSystem;
                mReportDoc.CreateDocument();
                mReportViewer.Show();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion
    }
}