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
using DevExpress.XtraLayout;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.IO;
using System.Xml.Serialization;

namespace AfyaPro_NextGen
{
    public partial class frmOPDPatientDocuments : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsReporter pMdtReporter;
        private AfyaPro_MT.clsRegistrations pMdtRegistrations;
        private AfyaPro_MT.clsPatientDocuments pMdtPatientDocuments;
        private AfyaPro_MT.clsUserGroupPrinters pMdtUserGroupPrinters;
        private AfyaPro_MT.clsPatientExtraFields pMdtPatientExtraFields;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private AfyaPro_Types.clsPatient pCurrentPatient;

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private string pCurrPatientId = "";
        private string pPrevPatientId = "";
        private bool pSearchingPatient = false;

        private string pPrinterName = "";

        #endregion

        #region frmOPDPatientDocuments
        public frmOPDPatientDocuments()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmOPDPatientDocuments";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                this.KeyDown += new KeyEventHandler(frmOPDPatientDocuments_KeyDown);

                pMdtRegistrations = (AfyaPro_MT.clsRegistrations)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRegistrations),
                    Program.gMiddleTier + "clsRegistrations");

                pMdtReporter = (AfyaPro_MT.clsReporter)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReporter),
                    Program.gMiddleTier + "clsReporter");

                pMdtPatientDocuments = (AfyaPro_MT.clsPatientDocuments)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPatientDocuments),
                    Program.gMiddleTier + "clsPatientDocuments");

                pMdtUserGroupPrinters = (AfyaPro_MT.clsUserGroupPrinters)Activator.GetObject(
                    typeof(AfyaPro_MT.clsUserGroupPrinters),
                    Program.gMiddleTier + "clsUserGroupPrinters");

                pMdtPatientExtraFields = (AfyaPro_MT.clsPatientExtraFields)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPatientExtraFields),
                    Program.gMiddleTier + "clsPatientExtraFields");

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.opdpatientdocuments_customizelayout.ToString());

                grdOPDPatientDocuments.ForceInitialize();
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
            List<Object> mObjectsList = new List<object>();

            mObjectsList.Add(txbPatientId);
            mObjectsList.Add(txbName);
            mObjectsList.Add(txbYears);
            mObjectsList.Add(txbMonths);
            mObjectsList.Add(txbGender);
            mObjectsList.Add(txbGender);
            mObjectsList.Add(txbDocuments);
            mObjectsList.Add(cmdSearch);
            mObjectsList.Add(cmdPreview);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
            this.Data_Clear();
        }

        #endregion

        #region frmOPDPatientDocuments_Load
        private void frmOPDPatientDocuments_Load(object sender, EventArgs e)
        {
            this.Top = 0;

            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            this.Load_Controls();

            txbBirthDateFormat.Text = "(" + Program.gCulture.DateTimeFormat.ShortDatePattern + ")";

            this.Fill_Documents();

            this.pFormWidth = this.Width;
            this.pFormHeight = this.Height;

            Program.Center_Screen(this);

            this.Append_ShortcutKeys();
        }
        #endregion

        #region frmOPDPatientDocuments_FormClosing
        private void frmOPDPatientDocuments_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //layout
                if (layoutControl1.IsModified == true)
                {
                    Program.Save_FormLayout(this, layoutControl1, this.Name);
                }

                //grid
                Program.Save_GridLayout(grdOPDPatientDocuments, grdOPDPatientDocuments.Name);
            }
            catch { }
        }
        #endregion

        #region Append_ShortcutKeys
        private void Append_ShortcutKeys()
        {
            cmdSearch.Text = cmdSearch.Text + " (" + Program.KeyCode_SeachPatient.ToString() + ")";
            cmdPreview.Text = cmdPreview.Text + " (" + Program.KeyCode_Preview.ToString() + ")";
            cmdPrint.Text = cmdPrint.Text + " (" + Program.KeyCode_Print.ToString() + ")";
        }
        #endregion

        #region Data_Clear
        private void Data_Clear()
        {
            txtName.Text = "";
            txtGender.Text = "";
            txtBirthDate.Text = "";
            txtYears.Text = "";
            txtMonths.Text = "";

            pPrevPatientId = "";
            pCurrPatientId = pPrevPatientId;

            txtPatientId.Focus();
        }
        #endregion

        #region Data_Display
        internal void Data_Display(AfyaPro_Types.clsPatient mPatient)
        {
            String mFunctionName = "Data_Display";

            try
            {
                this.Data_Clear();

                if (mPatient != null)
                {
                    if (mPatient.Exist == true)
                    {
                        string mFullName = mPatient.firstname;
                        if (mPatient.othernames.Trim() != "")
                        {
                            mFullName = mFullName + " " + mPatient.othernames;
                        }
                        mFullName = mFullName + " " + mPatient.surname;

                        txtPatientId.Text = mPatient.code;
                        txtName.Text = mFullName;
                        if (mPatient.gender.Trim().ToLower() == "f")
                        {
                            txtGender.Text = "Female";
                        }
                        else
                        {
                            txtGender.Text = "Male";
                        }
                        txtBirthDate.Text = mPatient.birthdate.ToString("d");
                        int mDays = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue).Subtract(mPatient.birthdate).Days;
                        int mYears = (int)mDays / 365;
                        int mMonths = (int)(mDays % 365) / 30;

                        txtYears.Text = mYears.ToString();
                        txtMonths.Text = mMonths.ToString();

                        pCurrPatientId = mPatient.code;
                        pPrevPatientId = pCurrPatientId;

                        System.Media.SystemSounds.Beep.Play();
                    }
                    else
                    {
                        pCurrPatientId = txtPatientId.Text;
                        pPrevPatientId = pCurrPatientId;
                    }
                }
                else
                {
                    pCurrPatientId = txtPatientId.Text;
                    pPrevPatientId = pCurrPatientId;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_Documents
        internal void Fill_Documents()
        {
            string mFunctionName = "Fill_Documents";

            try
            {
                //load data
                DataTable mDtPatientDocuments = pMdtPatientDocuments.View("", "", Program.gLanguageName, "grdGENPatientDocuments");
                grdOPDPatientDocuments.DataSource = mDtPatientDocuments;

                Program.Restore_GridLayout(grdOPDPatientDocuments, grdOPDPatientDocuments.Name);
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Search_Patient
        private AfyaPro_Types.clsPatient Search_Patient()
        {
            string mFunctionName = "Search_Patient";

            try
            {
                pCurrentPatient = pMdtRegistrations.Get_Patient(txtPatientId.Text);
                return pCurrentPatient;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
        }
        #endregion

        #region txtPatientId_KeyDown
        private void txtPatientId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.pCurrentPatient = this.Search_Patient();
                this.Data_Display(pCurrentPatient);
            }
        }
        #endregion

        #region txtPatientId_Leave
        private void txtPatientId_Leave(object sender, EventArgs e)
        {
            pCurrPatientId = txtPatientId.Text;

            if (pCurrPatientId.Trim().ToLower() != pPrevPatientId.Trim().ToLower())
            {
                this.pCurrentPatient = this.Search_Patient();
                this.Data_Display(pCurrentPatient);
            }
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region cmdSearch_Click
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            pSearchingPatient = true;

            frmSearchPatient mSearchPatient = new frmSearchPatient(txtPatientId);
            mSearchPatient.ShowDialog();

            pSearchingPatient = false;
        }
        #endregion

        #region txtPatientId_EditValueChanged
        private void txtPatientId_EditValueChanged(object sender, EventArgs e)
        {
            if (pSearchingPatient == true)
            {
                this.pCurrentPatient = this.Search_Patient();
                this.Data_Display(pCurrentPatient);
            }
        }
        #endregion

        #region Preview
        private void Preview()
        {
            string mFunctionName = "Preview";

            #region validation

            if (txtPatientId.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoIsInvalid.ToString());
                txtPatientId.Focus();
                return;
            }

            AfyaPro_Types.clsPatient mPatient = pMdtRegistrations.Get_Patient(txtPatientId.Text);
            if (mPatient.Exist == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoDoesNotExist.ToString());
                txtPatientId.Focus();
                return;
            }

            #endregion

            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)grdOPDPatientDocuments.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                this.Cursor = Cursors.WaitCursor;

                string mFilter = "code='" + txtPatientId.Text.Trim() + "'";
                DataTable mDtData = pMdtReporter.REG_PatientDetails(null, mFilter, "");

                string mFileName = "PatientDocument_" + mSelectedRow["code"].ToString().Trim();
                byte[] mBytes = pMdtReporter.Load_ReportTemplate(mFileName, false);

                string mPrinterName = Program.Get_PrinterName(mSelectedRow["code"].ToString().Trim());
                if (mPrinterName.Trim() != "")
                {
                    pPrinterName = mPrinterName;
                }

                DevExpress.XtraReports.UI.XtraReport mReportDoc = Program.Load_ReportTemplate(mBytes, mDtData);
                mReportDoc.PrintingSystem.StartPrint += new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);

                if (mReportDoc == null)
                {
                    Program.gMdiForm.Cursor = Cursors.Default;
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
                this.Cursor = Cursors.Default;
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdPreview_Click
        private void cmdPreview_Click(object sender, EventArgs e)
        {
            this.Preview();
        }
        #endregion

        #region Print
        private void Print()
        {
            string mFunctionName = "Print";

            #region validation

            if (txtPatientId.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoIsInvalid.ToString());
                txtPatientId.Focus();
                return;
            }

            AfyaPro_Types.clsPatient mPatient = pMdtRegistrations.Get_Patient(txtPatientId.Text);
            if (mPatient.Exist == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoDoesNotExist.ToString());
                txtPatientId.Focus();
                return;
            }

            #endregion

            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)grdOPDPatientDocuments.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                this.Cursor = Cursors.WaitCursor;

                string mFilter = "code='" + txtPatientId.Text.Trim() + "'";
                DataTable mDtData = pMdtReporter.REG_PatientDetails(null, mFilter, "");

                string mFileName = "PatientDocument_" + mSelectedRow["code"].ToString().Trim();
                byte[] mBytes = pMdtReporter.Load_ReportTemplate(mFileName, false);

                DevExpress.XtraReports.UI.XtraReport mReportDoc = Program.Load_ReportTemplate(mBytes, mDtData);

                string mPrinterName = Program.Get_PrinterName(mSelectedRow["code"].ToString().Trim());

                if (mPrinterName.Trim() != "")
                {
                    pPrinterName = mPrinterName;
                    //mReportDoc.PaperKind = System.Drawing.Printing.PaperKind.Custom;
                    //System.Drawing.Printing.PaperSize mPaperSize = new System.Drawing.Printing.PaperSize("Label Size", 200, 100);
                    //mReportDoc.PaperName = mPaperSize.PaperName;
                    //mReportDoc.PageWidth = mPaperSize.Width;
                    //mReportDoc.PageHeight = mPaperSize.Height;
                }

                mReportDoc.PrintingSystem.StartPrint += new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);
                if (mReportDoc == null)
                {
                    Program.gMdiForm.Cursor = Cursors.Default;
                    return;
                }

                mReportDoc.Print();

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

        #region cmdPrint_Click
        private void cmdPrint_Click(object sender, EventArgs e)
        {
            this.Print();
        }
        #endregion

        #region AutoPrint_Documents
        internal void AutoPrint_Documents(string mPatientCode, Int16 mPrintedWhenFlag)
        {
            string mFunctionName = "AutoPrint_Documents";

            try
            {
                this.Cursor = Cursors.WaitCursor;

                string mFilter = "code='" + mPatientCode + "'";
                DataTable mDtData = pMdtReporter.REG_PatientDetails(null, mFilter, "");

                DataTable mDtPatientDocuments = pMdtPatientDocuments.View("", "printorder", "", "");
                DataTable mDtUserGroupPrinters = pMdtUserGroupPrinters.View(
                    "machinename='" + Program.gMachineName + "' and usergroupcode='" 
                    + Program.gCurrentUser.UserGroupCode + "' and autoprint=1 and printedwhen=" + mPrintedWhenFlag,
                    "", "", "");
                DataView mDvUserGroupPrinters = new DataView();
                mDvUserGroupPrinters.Table = mDtUserGroupPrinters;
                mDvUserGroupPrinters.Sort = "documenttypecode";

                foreach (DataRow mDataRow in mDtPatientDocuments.Rows)
                {
                    int mRowIndex = mDvUserGroupPrinters.Find(mDataRow["code"].ToString().Trim());

                    if (mRowIndex >= 0)
                    {
                        string mFileName = "PatientDocument_" + mDataRow["code"].ToString().Trim();
                        string mPrinterName = @mDvUserGroupPrinters[mRowIndex]["printername"].ToString().Trim();
                        byte[] mBytes = pMdtReporter.Load_ReportTemplate(mFileName, false);

                        DevExpress.XtraReports.UI.XtraReport mReportDoc = Program.Load_ReportTemplate(mBytes, mDtData);

                        if (mReportDoc == null)
                        {
                            this.Cursor = Cursors.Default;
                            return;
                        }

                        //set printer name if specified
                        if (mPrinterName.Trim() != "")
                        {
                            mReportDoc.PrinterName = mPrinterName;
                        }

                        try
                        {
                            bool mPrintToScreen = false;
                            try
                            {
                                mPrintToScreen = Convert.ToBoolean(mDtUserGroupPrinters.Rows[0]["printtoscreen"]);
                            }
                            catch { }

                            if (mPrintToScreen == true)
                            {
                                mReportDoc.ShowPreview();
                            }
                            else
                            {
                                mReportDoc.Print();
                            }
                        }
                        catch { }
                    }
                }

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

        #region PrintingSystem_StartPrint
        private void PrintingSystem_StartPrint(object sender, DevExpress.XtraPrinting.PrintDocumentEventArgs e)
        {
            // setting the specific printer's name before printing
            e.PrintDocument.PrinterSettings.PrinterName = pPrinterName;
        }
        #endregion

        #region AutoPrint_Receipt
        internal void AutoPrint_Receipt(string mReceiptNo)
        {
            string mFunctionName = "AutoPrint_Receipt";

            try
            {
                this.Cursor = Cursors.WaitCursor;

                string mFilter = "receiptno='" + mReceiptNo.Trim() + "'";
                DataTable mDtData = pMdtReporter.Get_DataFromDBView("view_billreceiptdoc", null, mFilter, "");

                DataTable mDtUserGroupPrinters = pMdtUserGroupPrinters.View(
                    "machinename='" + Program.gMachineName + "' and usergroupcode='"
                    + Program.gCurrentUser.UserGroupCode + "' and documenttypecode='receipts'", "", "", "");

                if (mDtUserGroupPrinters.Rows.Count > 0)
                {
                    string mFileName = "BILL_Receipt";
                    pPrinterName = mDtUserGroupPrinters.Rows[0]["printername"].ToString().Trim();
                    byte[] mBytes = pMdtReporter.Load_ReportTemplate(mFileName, false);

                    DevExpress.XtraReports.UI.XtraReport mReportDoc = Program.Load_ReportTemplate(mBytes, mDtData);
                    mReportDoc.PrintingSystem.StartPrint+=new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);

                    if (mReportDoc == null)
                    {
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    try
                    {
                        bool mPrintToScreen = false;
                        try
                        {
                            mPrintToScreen = Convert.ToBoolean(mDtUserGroupPrinters.Rows[0]["printtoscreen"]);
                        }
                        catch { }

                        if (mPrintToScreen == true)
                        {
                            mReportDoc.ShowPreview();
                        }
                        else
                        {
                            mReportDoc.Print();
                        }
                    }
                    catch { }
                }

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

        #region AutoPrint_Invoice
        internal void AutoPrint_Invoice(string mInvoiceNo)
        {
            string mFunctionName = "AutoPrint_Invoice";

            try
            {
                this.Cursor = Cursors.WaitCursor;

                string mFilter = "invoiceno='" + mInvoiceNo.Trim() + "'";
                DataTable mDtData = pMdtReporter.Get_DataFromDBView("view_billinvoicedoc", null, mFilter, "");

                DataTable mDtUserGroupPrinters = pMdtUserGroupPrinters.View(
                    "machinename='" + Program.gMachineName + "' and usergroupcode='"
                    + Program.gCurrentUser.UserGroupCode + "' and documenttypecode='invoices'", "", "", "");

                if (mDtUserGroupPrinters.Rows.Count > 0)
                {
                    string mFileName = "BILL_Invoice";
                    pPrinterName = mDtUserGroupPrinters.Rows[0]["printername"].ToString().Trim();
                    byte[] mBytes = pMdtReporter.Load_ReportTemplate(mFileName, false);

                    DevExpress.XtraReports.UI.XtraReport mReportDoc = Program.Load_ReportTemplate(mBytes, mDtData);
                    mReportDoc.PrintingSystem.StartPrint += new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);

                    if (mReportDoc == null)
                    {
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    try
                    {
                        bool mPrintToScreen = false;
                        try
                        {
                            mPrintToScreen = Convert.ToBoolean(mDtUserGroupPrinters.Rows[0]["printtoscreen"]);
                        }
                        catch { }

                        if (mPrintToScreen == true)
                        {
                            mReportDoc.ShowPreview();
                        }
                        else
                        {
                            mReportDoc.Print();
                        }
                    }
                    catch { }
                }

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

        #region AutoPrint_InvoicePayment
        internal void AutoPrint_InvoicePayment(string mReceiptNo)
        {
            string mFunctionName = "AutoPrint_InvoicePayment";

            try
            {
                this.Cursor = Cursors.WaitCursor;

                string mFilter = "receiptno='" + mReceiptNo.Trim() + "'";
                DataTable mDtData = pMdtReporter.Get_DataFromDBView("view_billinvoicepaymentdoc", null, mFilter, "");

                DataTable mDtUserGroupPrinters = pMdtUserGroupPrinters.View(
                    "machinename='" + Program.gMachineName + "' and usergroupcode='"
                    + Program.gCurrentUser.UserGroupCode + "' and documenttypecode='receipts'", "", "", "");

                if (mDtUserGroupPrinters.Rows.Count > 0)
                {
                    string mFileName = "BILL_InvoicePayment";
                    pPrinterName = mDtUserGroupPrinters.Rows[0]["printername"].ToString().Trim();
                    byte[] mBytes = pMdtReporter.Load_ReportTemplate(mFileName, false);

                    DevExpress.XtraReports.UI.XtraReport mReportDoc = Program.Load_ReportTemplate(mBytes, mDtData);
                    mReportDoc.PrintingSystem.StartPrint += new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);

                    if (mReportDoc == null)
                    {
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    try
                    {
                        bool mPrintToScreen = false;
                        try
                        {
                            mPrintToScreen = Convert.ToBoolean(mDtUserGroupPrinters.Rows[0]["printtoscreen"]);
                        }
                        catch { }

                        if (mPrintToScreen == true)
                        {
                            mReportDoc.ShowPreview();
                        }
                        else
                        {
                            mReportDoc.Print();
                        }
                    }
                    catch { }
                }

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

        #region AutoPrint_InvoicePaymentGroup
        internal void AutoPrint_InvoicePaymentGroup(string mReceiptNo)
        {
            string mFunctionName = "AutoPrint_InvoicePaymentGroup";

            try
            {
                this.Cursor = Cursors.WaitCursor;

                string mFilter = "receiptno='" + mReceiptNo.Trim() + "'";
                DataTable mDtData = pMdtReporter.Get_DataFromDBView("view_billinvoicepaymentdoc", null, mFilter, "");

                DataTable mDtUserGroupPrinters = pMdtUserGroupPrinters.View(
                    "machinename='" + Program.gMachineName + "' and usergroupcode='"
                    + Program.gCurrentUser.UserGroupCode + "' and documenttypecode='receipts'", "", "", "");

                if (mDtUserGroupPrinters.Rows.Count > 0)
                {
                    string mFileName = "BILL_InvoicePaymentGroup";
                    pPrinterName = mDtUserGroupPrinters.Rows[0]["printername"].ToString().Trim();
                    byte[] mBytes = pMdtReporter.Load_ReportTemplate(mFileName, false);

                    DevExpress.XtraReports.UI.XtraReport mReportDoc = Program.Load_ReportTemplate(mBytes, mDtData);
                    mReportDoc.PrintingSystem.StartPrint += new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);

                    if (mReportDoc == null)
                    {
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    try
                    {
                        bool mPrintToScreen = false;
                        try
                        {
                            mPrintToScreen = Convert.ToBoolean(mDtUserGroupPrinters.Rows[0]["printtoscreen"]);
                        }
                        catch { }

                        if (mPrintToScreen == true)
                        {
                            mReportDoc.ShowPreview();
                        }
                        else
                        {
                            mReportDoc.Print();
                        }
                    }
                    catch { }
                }

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

        #region frmOPDPatientDocuments_KeyDown
        void frmOPDPatientDocuments_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Program.KeyCode_Preview:
                    {
                        this.Preview();
                    }
                    break;
                case Program.KeyCode_SeachPatient:
                    {
                        pSearchingPatient = true;

                        frmSearchPatient mSearchPatient = new frmSearchPatient(txtPatientId);
                        mSearchPatient.ShowDialog();

                        pSearchingPatient = false;
                    }
                    break;
                case Program.KeyCode_Print:
                    {
                        this.Print();
                    }
                    break;
            }
        }
        #endregion
    }
}