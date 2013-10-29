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
    public partial class frmBILRefund : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsBilling pMdtBilling;
        private AfyaPro_MT.clsReporter pMdtReporter;

        private Type pType;
        private String pClassName = "";

        private Int16 pPaymentSource = 0;

        private DataTable pDtPaymentsMade = new DataTable("paymentsmade");

        #endregion

        #region frmBILRefund
        public frmBILRefund(Int16 mPaymentSource)
        {
            InitializeComponent();

            String mFunctionName = "frmBILRefund";

            try
            {
                pType = this.GetType();
                pClassName = pType.FullName;
                pPaymentSource = mPaymentSource;

                pMdtBilling = (AfyaPro_MT.clsBilling)Activator.GetObject(
                    typeof(AfyaPro_MT.clsBilling),
                    Program.gMiddleTier + "clsBilling");

                pMdtReporter = (AfyaPro_MT.clsReporter)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReporter),
                    Program.gMiddleTier + "clsReporter");

                pDtPaymentsMade.Columns.Add("paytypecode", typeof(System.String));
                pDtPaymentsMade.Columns.Add("paytypedescription", typeof(System.String));
                pDtPaymentsMade.Columns.Add("allowrefund", typeof(System.Int16));
                pDtPaymentsMade.Columns.Add("bank", typeof(System.String));
                pDtPaymentsMade.Columns.Add("branch", typeof(System.String));
                pDtPaymentsMade.Columns.Add("holder", typeof(System.String));
                pDtPaymentsMade.Columns.Add("chequeno", typeof(System.String));
                pDtPaymentsMade.Columns.Add("paidamount", typeof(System.Double));
                pDtPaymentsMade.Columns.Add("refundedamount", typeof(System.Double));

                grdBILRefund.DataSource = pDtPaymentsMade;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmBILRefund_Load
        private void frmBILRefund_Load(object sender, EventArgs e)
        {
            this.Data_Fill();

            for (int mIndex = 0; mIndex < viewBILRefund.Columns.Count; mIndex++)
            {
                switch (viewBILRefund.Columns[mIndex].ColumnType.ToString().ToLower())
                {
                    case "system.double":
                        {
                            viewBILRefund.Columns[mIndex].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            viewBILRefund.Columns[mIndex].DisplayFormat.FormatString = "{0:c}";
                        }
                        break;
                }
            }
        }
        #endregion

        #region Data_Fill
        internal void Data_Fill()
        {
            String mFunctionName = "Data_Fill";

            try
            {
                pDtPaymentsMade.Rows.Clear();

                DataTable mDtPaymentsMade = pMdtBilling.View_CollectionByReceipt(txtReceiptNo.Text, pPaymentSource, "", "");
                foreach (DataRow mDataRow in mDtPaymentsMade.Rows)
                {
                    DataRow mNewRow = pDtPaymentsMade.NewRow();
                    mNewRow["paytypecode"] = mDataRow["paytypecode"];
                    mNewRow["paytypedescription"] = mDataRow["paytypedescription"];
                    mNewRow["allowrefund"] = mDataRow["allowrefund"];
                    mNewRow["bank"] = mDataRow["bank"];
                    mNewRow["branch"] = mDataRow["branch"];
                    mNewRow["holder"] = mDataRow["holder"];
                    mNewRow["chequeno"] = mDataRow["chequeno"];
                    mNewRow["paidamount"] = mDataRow["paidamount"];
                    mNewRow["refundedamount"] = mDataRow["refundedamount"];
                    pDtPaymentsMade.Rows.Add(mNewRow);
                    pDtPaymentsMade.AcceptChanges();
                }

                Program.gMdiForm.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Program.gMdiForm.Cursor = Cursors.Default;
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdRefund_Click
        private void cmdRefund_Click(object sender, EventArgs e)
        {
            AfyaPro_Types.clsBill mBill = new AfyaPro_Types.clsBill();

            string mFunctionName = "cmdRefund_Click";

            if (Program.IsDate(Program.gMdiForm.txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                return;
            }

            try
            {
                viewBILRefund.PostEditor();
                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                double mTotalRefunded = 0;
                foreach (DataRow mDataRow in pDtPaymentsMade.Rows)
                {
                    mTotalRefunded = mTotalRefunded + Convert.ToDouble(mDataRow["refundedamount"]);
                }

                if (mTotalRefunded == 0)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_RefundAmountIsInvalid.ToString());
                    return;
                }

                DialogResult mDialogResult = Program.Confirm_Refund(txtReceiptNo.Text);
                if (mDialogResult != DialogResult.Yes)
                {
                    return;
                }

                mBill = pMdtBilling.Refund(txtReceiptNo.Text, pPaymentSource, mTransDate,
                    pDtPaymentsMade, Program.gCurrentUser.Code, Program.gCurrentUser.Description);
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

                Program.Display_Info(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_PaymentRefundComplete.ToString());

                this.Close();
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

        #region viewBILRefund_ValidatingEditor
        private void viewBILRefund_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (viewBILRefund.FocusedColumn.Name.Trim().ToLower() == "refundedamount")
            {
                if (Program.IsMoney(e.Value.ToString()) == false)
                {
                    e.Valid = false;
                    e.ErrorText = Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_RefundAmountIsInvalid.ToString());
                    return;
                }

                DataRow mSelectedRow = viewBILRefund.GetDataRow(viewBILRefund.FocusedRowHandle);
                if (Convert.ToInt16(mSelectedRow["allowrefund"]) == 0)
                {
                    e.Valid = false;
                    e.ErrorText = Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_PaymentTypeDoesNotAllowRefund.ToString());
                    return;
                }

                if (Convert.ToDouble(e.Value) > Convert.ToDouble(mSelectedRow["paidamount"]))
                {
                    e.Valid = false;
                    e.ErrorText = Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_RefundAmountIsInvalid.ToString());
                    return;
                }
            }
        }
        #endregion
    }
}