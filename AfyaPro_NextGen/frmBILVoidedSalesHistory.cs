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
using System.IO;
using System.Xml;

namespace AfyaPro_NextGen
{
    public partial class frmBILVoidedSalesHistory : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsBilling pMdtBilling;
        private AfyaPro_MT.clsReporter pMdtReporter;

        private Type pType;
        private String pClassName = "";

        #endregion

        #region frmBILVoidedSales
        public frmBILVoidedSalesHistory()
        {
            InitializeComponent();

            String mFunctionName = "frmBILVoidedSales";

            try
            {
                pType = this.GetType();
                pClassName = pType.FullName;

                pMdtBilling = (AfyaPro_MT.clsBilling)Activator.GetObject(
                    typeof(AfyaPro_MT.clsBilling),
                    Program.gMiddleTier + "clsBilling");

                pMdtReporter = (AfyaPro_MT.clsReporter)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReporter),
                    Program.gMiddleTier + "clsReporter");

                txtDateFrom.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                txtDateTo.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                txtDateFrom.EditValue = Program.gMdiForm.txtDate.EditValue;
                txtDateTo.EditValue = Program.gMdiForm.txtDate.EditValue;

                //Data_Fill
                this.Data_Fill();

                //access to design receipt
                cmdDesignReceipt.Enabled = Program.GrantDeny_FunctionAccess(
                      AfyaPro_Types.clsSystemFunctions.FunctionKeys.billvoidedsaleshistory_designreceipt.ToString());

                //access to design receipt
                cmdDesignRefund.Enabled = Program.GrantDeny_FunctionAccess(
                      AfyaPro_Types.clsSystemFunctions.FunctionKeys.billvoidedsaleshistory_designrefund.ToString());

                //access to refund
                cmdRefund.Enabled = Program.GrantDeny_FunctionAccess(
                      AfyaPro_Types.clsSystemFunctions.FunctionKeys.billvoidedsaleshistory_refund.ToString());
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region mDateEdit_EditValueChanged
        void mDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            Program.AddTimeToDate((DateEdit)sender);
        }
        #endregion

        #region frmBILVoidedSales_Load
        private void frmBILVoidedSales_Load(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                (DevExpress.XtraGrid.Views.Grid.GridView)grdVoidedSalesHistory.MainView;
            mGridView.OptionsBehavior.Editable = false;
            mGridView.OptionsView.ShowGroupPanel = true;
            mGridView.OptionsView.ShowFooter = true;
            mGridView.OptionsCustomization.AllowGroup = true;

            grdVoidedSalesHistory.ForceInitialize();
            Program.Restore_GridLayout(grdVoidedSalesHistory, grdVoidedSalesHistory.Name);

            for (int mIndex = 0; mIndex < mGridView.Columns.Count; mIndex++)
            {
                switch (mGridView.Columns[mIndex].ColumnType.ToString().ToLower())
                {
                    case "system.double":
                        {
                            mGridView.Columns[mIndex].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            mGridView.Columns[mIndex].DisplayFormat.FormatString = "{0:c}";
                        }
                        break;
                }
            }

            this.Load_Controls();
        }
        #endregion

        #region frmBILVoidedSales_FormClosing
        private void frmBILVoidedSales_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                MemoryStream mMemoryStream = new MemoryStream();
                grdVoidedSalesHistory.MainView.SaveLayoutToStream(mMemoryStream);

                Program.gMdtFacilitySetup.Save_GridLayout(grdVoidedSalesHistory.Name, Program.gCurrentUser.Code, mMemoryStream.ToArray());
            }
            catch { }
        }
        #endregion

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(grpDate);
            mObjectsList.Add(txbDateFrom);
            mObjectsList.Add(txbDateTo);
            mObjectsList.Add(cmdRefresh);
            mObjectsList.Add(cmdPreview);
            mObjectsList.Add(cmdDesignReceipt);
            mObjectsList.Add(cmdDesignRefund);
            mObjectsList.Add(cmdRefund);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region Data_Fill
        internal void Data_Fill()
        {
            String mFunctionName = "Data_Fill";

            #region validation
            if (Program.IsDate(txtDateFrom.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                txtDateFrom.Focus();
                return;
            }
            if (Program.IsDate(txtDateTo.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                txtDateTo.Focus();
                return;
            }
            if (Convert.ToDateTime(txtDateTo.Text) < Convert.ToDateTime(txtDateFrom.Text))
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateRangeIsInvalid.ToString());
                return;
            }
            #endregion

            try
            {
                //date range
                bool mDateSpecified = true;
                DateTime mDateFrom = Convert.ToDateTime(txtDateFrom.EditValue);
                DateTime mDateTo = Convert.ToDateTime(txtDateTo.EditValue);

                string mExtraFilter = "(voidsource=" + Convert.ToInt16(AfyaPro_Types.clsEnums.VoidSources.CashSale)
                    + " or voidsource=" + Convert.ToInt16(AfyaPro_Types.clsEnums.VoidSources.InvoicePayment)
                    + " or voidsource=" + Convert.ToInt16(AfyaPro_Types.clsEnums.VoidSources.InvoiceSale) + ")";

                Program.gMdiForm.Cursor = Cursors.WaitCursor;

                DataTable mDtPatientListing = pMdtBilling.View_VoidedSales(mDateSpecified, mDateFrom, mDateTo,
                    mExtraFilter, "transdate desc,autocode desc", Program.gLanguageName, grdVoidedSalesHistory.Name);
                grdVoidedSalesHistory.DataSource = mDtPatientListing;

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

        #region cmdRefresh_Click
        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            this.Data_Fill();
        }
        #endregion

        #region cmdPreview_Click
        private void cmdPreview_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdPreview_Click";

            try
            {
                Program.gMdiForm.Cursor = Cursors.WaitCursor;

                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)((frmBILVoidedSalesHistory)Program.gMdiForm.ActiveMdiChild).grdVoidedSalesHistory.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                string mFilter = "invoiceno='" + mSelectedRow["invoiceno"].ToString() + "'";
                DataTable mDtData = pMdtReporter.Get_DataFromDBView("view_billinvoicedoc", null, mFilter, "");

                string mFileName = "BILL_Invoice";
                byte[] mBytes = pMdtReporter.Load_ReportTemplate(mFileName, false);

                DevExpress.XtraReports.UI.XtraReport mReportDoc = Program.Load_ReportTemplate(mBytes, mDtData);

                if (mReportDoc == null)
                {
                    Program.gMdiForm.Cursor = Cursors.Default;
                    return;
                }

                frmReportViewer mReportViewer = new frmReportViewer();
                mReportViewer.printControl1.PrintingSystem = mReportDoc.PrintingSystem;
                mReportDoc.CreateDocument();
                mReportViewer.Show();

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

        #region cmdPreviewList_Click
        private void cmdPreviewList_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdPreview_Click";

            try
            {
                Program.gMdiForm.Cursor = Cursors.WaitCursor;

                grdVoidedSalesHistory.ShowPrintPreview();

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

        #region cmdDesign_Click
        private void cmdDesign_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdDesign_Click";

            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Create a design form and get its panel.
                DevExpress.XtraReports.UserDesigner.XRDesignFormEx mDesignForm =
                    new DevExpress.XtraReports.UserDesigner.XRDesignFormEx();
                DevExpress.XtraReports.UserDesigner.XRDesignPanel mDesignPanel = mDesignForm.DesignPanel;

                //retrieve data
                string mFilter = "1=2";
                DataTable mDtData = pMdtReporter.Get_DataFromDBView("view_billinvoicedoc", null, mFilter, "");

                string mFileName = "BILL_Invoice";
                byte[] mBytes = pMdtReporter.Load_ReportTemplate(mFileName, false);

                DevExpress.XtraReports.UI.XtraReport mReportDoc = Program.Load_ReportTemplate(mBytes, mDtData);

                // Add a new command handler which saves a report in a custom way.
                mDesignPanel.AddCommandHandler(new clsReportDesignerSaveCommandHandler(mDesignPanel, mFileName));

                // Load a report into the design form and show the form.
                mDesignPanel.OpenReport(mReportDoc);
                mDesignForm.ShowDialog();
                mDesignPanel.CloseReport();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdRefund_Click
        private void cmdRefund_Click(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                (DevExpress.XtraGrid.Views.Grid.GridView)((frmBILVoidedSalesHistory)Program.gMdiForm.ActiveMdiChild).grdVoidedSalesHistory.MainView;

            if (mGridView.FocusedRowHandle < 0)
            {
                return;
            }

            DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

            string mReceiptNo = mSelectedRow["receiptno"].ToString().Trim();
            Int16 mPaymentSource = Convert.ToInt16(AfyaPro_Types.clsEnums.PaymentSources.CashSale);
            if (Convert.ToInt16(mSelectedRow["voidsource"]) ==
                Convert.ToInt16(AfyaPro_Types.clsEnums.VoidSources.InvoicePayment))
            {
                mPaymentSource = Convert.ToInt16(AfyaPro_Types.clsEnums.PaymentSources.InvoicePayment);
            }
            frmBILRefund mBILRefund = new frmBILRefund(mPaymentSource);
            mBILRefund.txtReceiptNo.Text = mReceiptNo;
            mBILRefund.ShowDialog();
        }
        #endregion
    }
}