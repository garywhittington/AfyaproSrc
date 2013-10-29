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
    public partial class frmDXTPrintPrescriptions : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsRegistrations pMdtRegistrations;
        private AfyaPro_MT.clsUserGroupPrinters pMdtUserGroupPrinters;
        private AfyaPro_MT.clsReporter pMdtReporter;

        private Type pType;
        private String pClassName = "";

        private string pPrinterName = "";
        private bool pCheckedCheckBox = false;
        private bool pCheckedGrid = false;

        private AfyaPro_Types.clsPatient pCurrentPatient;
        private string pCurrPatientId = "";
        private string pPrevPatientId = "";
        private bool pSearchingPatient = false;

        #endregion

        #region frmDXTPrintPrescriptions
        public frmDXTPrintPrescriptions()
        {
            InitializeComponent();

            String mFunctionName = "frmDXTPrintPrescriptions";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                pType = this.GetType();
                pClassName = pType.FullName;

                pMdtReporter = (AfyaPro_MT.clsReporter)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReporter),
                    Program.gMiddleTier + "clsReporter");

                pMdtUserGroupPrinters = (AfyaPro_MT.clsUserGroupPrinters)Activator.GetObject(
                    typeof(AfyaPro_MT.clsUserGroupPrinters),
                    Program.gMiddleTier + "clsUserGroupPrinters");

                pMdtRegistrations = (AfyaPro_MT.clsRegistrations)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRegistrations),
                    Program.gMiddleTier + "clsRegistrations");

                txtDateFrom.EditValue = Program.gMdiForm.txtDate.EditValue;
                txtDateTo.EditValue = Program.gMdiForm.txtDate.EditValue;

                //Data_Fill
                this.Data_Fill("");

                //access to design receipt
                cmdDesign.Enabled = Program.GrantDeny_FunctionAccess(
                      AfyaPro_Types.clsSystemFunctions.FunctionKeys.dxtpatientdiagnoses_customizelayout.ToString());
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmDXTPrintPrescriptions_Load
        private void frmDXTPrintPrescriptions_Load(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                (DevExpress.XtraGrid.Views.Grid.GridView)grdDXTPrintPrescriptions.MainView;
            mGridView.OptionsBehavior.Editable = true;
            mGridView.OptionsView.ShowGroupPanel = true;
            mGridView.OptionsView.ShowFooter = true;
            mGridView.OptionsCustomization.AllowGroup = true;

            grdDXTPrintPrescriptions.ForceInitialize();
            Program.Restore_GridLayout(grdDXTPrintPrescriptions, grdDXTPrintPrescriptions.Name);

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
                if (mGridView.Columns[mIndex].FieldName.ToLower() != "selected")
                {
                    mGridView.Columns[mIndex].OptionsColumn.AllowEdit = false;
                }
            }

            mGridView.Columns["selected"].ColumnEdit = selected;
            mGridView.Columns["selected"].OptionsColumn.ShowCaption = false;
            selected.CheckedChanged += new EventHandler(selected_CheckedChanged);

            this.Load_Controls();

            Program.Center_Screen(this);

            cboPrintStatus.SelectedIndex = 0;
        }
        #endregion

        #region frmDXTPrintPrescriptions_FormClosing
        private void frmDXTPrintPrescriptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                MemoryStream mMemoryStream = new MemoryStream();
                grdDXTPrintPrescriptions.MainView.SaveLayoutToStream(mMemoryStream);

                Program.gMdtFacilitySetup.Save_GridLayout(grdDXTPrintPrescriptions.Name, Program.gCurrentUser.Code, mMemoryStream.ToArray());
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

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region Data_Fill
        internal void Data_Fill(string mPatientCode)
        {
            String mFunctionName = "Data_Fill";

            #region validation

            if (Program.IsNullDate(txtDateFrom.EditValue) == true)
            {
                //Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                //txtDateFrom.Focus();
                return;
            }
            if (Program.IsNullDate(txtDateTo.EditValue) == true)
            {
                //Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                //txtDateTo.Focus();
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
                DateTime mDateFrom = Convert.ToDateTime(txtDateFrom.EditValue);
                DateTime mDateTo = Convert.ToDateTime(txtDateTo.EditValue);

                Program.gMdiForm.Cursor = Cursors.WaitCursor;

                Int16 mPrintStatus = Convert.ToInt16(cboPrintStatus.SelectedIndex);

                DataTable mDtPatients = pMdtReporter.Get_PrescribedPatients(mDateFrom, mDateTo, mPrintStatus, mPatientCode, grdDXTPrintPrescriptions.Name);
                grdDXTPrintPrescriptions.DataSource = mDtPatients;

                Program.gMdiForm.Cursor = Cursors.Default;

                chkAll.Checked = false;
            }
            catch (Exception ex)
            {
                Program.gMdiForm.Cursor = Cursors.Default;
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cboPrintStatus_SelectedIndexChanged
        private void cboPrintStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPrintStatus.SelectedIndex != -1)
            {
                this.Data_Fill(txtPatientId.Text.Trim());
            }
        }
        #endregion

        #region cmdRefresh_Click
        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            this.Data_Fill(txtPatientId.Text.Trim());
        }
        #endregion

        #region cmdPrint_Click
        private void cmdPrint_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdPreview_Click";

            this.Cursor = Cursors.WaitCursor;

            try
            {
                bool mSelected = false;
                DataTable mDataTable = (DataTable)grdDXTPrintPrescriptions.DataSource;

                DataTable mDtPrinted = new DataTable("printed");
                mDtPrinted.Columns.Add("patientcode", typeof(System.String));
                mDtPrinted.Columns.Add("transdate", typeof(System.DateTime));

                foreach (DataRow mDataRow in mDataTable.Rows)
                {
                    if (Convert.ToBoolean(mDataRow["selected"]) == true)
                    {
                        mSelected = true;
                        DataRow mNewRow = mDtPrinted.NewRow();
                        mNewRow["patientcode"] = mDataRow["patientcode"];
                        mNewRow["transdate"] = mDataRow["transdate"];
                        mDtPrinted.Rows.Add(mNewRow);
                        mDtPrinted.AcceptChanges();
                    }
                }

                if (mSelected == false)
                {
                    Program.Display_Info("Please select at least one prescription");
                    return;
                }

                DataTable mDtUserGroupPrinters = pMdtUserGroupPrinters.View(
                    "machinename='" + Program.gMachineName + "' and usergroupcode='"
                    + Program.gCurrentUser.UserGroupCode + "' and (documenttypecode='receipts' or documenttypecode='invoices')", "", "", "");
                if (mDtUserGroupPrinters.Rows.Count > 0)
                {
                    pPrinterName = mDtUserGroupPrinters.Rows[0]["printername"].ToString().Trim();
                }

                string mFileName = "Pre_Treat";

                foreach (DataRow mDataRow in mDataTable.Rows)
                {
                    if (Convert.ToBoolean(mDataRow["selected"]) == true)
                    {
                        string mFilter = "patientcode='" + mDataRow["patientcode"].ToString().Trim() + "' and transdate='" + Convert.ToDateTime(mDataRow["transdate"]).ToString("yyyy-MM-dd") + "'";

                        DataTable mDtData = pMdtReporter.Get_DataFromDBView("view_dxtpatientprescriptions", null, mFilter, "");

                        byte[] mBytes = pMdtReporter.Load_ReportTemplate(mFileName, false);

                        DevExpress.XtraReports.UI.XtraReport mReportDoc = Program.Load_ReportTemplate(mBytes, mDtData);
                        mReportDoc.PrintingSystem.StartPrint += new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);

                        if (mReportDoc == null)
                        {
                            return;
                        }

                        mReportDoc.Print();
                    }
                }

                //update those printed
                AfyaPro_Types.clsResult mResult = pMdtReporter.Update_PrintedPrescription(mDtPrinted, Program.gCurrentUser.Code);
                if (mResult.Exe_Result == 0)
                {
                    Program.Display_Error(mResult.Exe_Message);
                    return;
                }
                if (mResult.Exe_Result == -1)
                {
                    Program.Display_Server_Error(mResult.Exe_Message);
                    return;
                }

                this.Data_Fill(txtPatientId.Text.Trim());
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

        #region cmdDesign_Click
        private void cmdDesign_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdDesign_Click";

            this.Cursor = Cursors.WaitCursor;

            try
            {
                bool mSelected = false;
                DataTable mDataTable = (DataTable)grdDXTPrintPrescriptions.DataSource;
                foreach (DataRow mDataRow in mDataTable.Rows)
                {
                    if (Convert.ToBoolean(mDataRow["selected"]) == true)
                    {
                        mSelected = true;
                        break;
                    }
                }

                if (mSelected == false)
                {
                    Program.Display_Info("Please select at least one prescription");
                    return;
                }

                // Create a design form and get its panel.
                DevExpress.XtraReports.UserDesigner.XRDesignFormEx mDesignForm =
                    new DevExpress.XtraReports.UserDesigner.XRDesignFormEx();
                DevExpress.XtraReports.UserDesigner.XRDesignPanel mDesignPanel = mDesignForm.DesignPanel;

                //retrieve data
                string mFileName = "Pre_Treat";

                string mPatientCode = "";
                DateTime mTransDate = new DateTime();
                foreach (DataRow mDataRow in mDataTable.Rows)
                {
                    mPatientCode = mDataRow["patientcode"].ToString().Trim();
                    mTransDate = Convert.ToDateTime(mDataRow["transdate"]);
                    break;
                }

                string mFilter = "patientcode='" + mPatientCode + "' and transdate='" + mTransDate.ToString("yyyy-MM-dd") + "'";

                DataTable mDtData = pMdtReporter.Get_DataFromDBView("view_dxtpatientprescriptions", null, mFilter, "");

                byte[] mBytes = pMdtReporter.Load_ReportTemplate(mFileName, false);

                DevExpress.XtraReports.UI.XtraReport mReportDoc = Program.Load_ReportTemplate(mBytes, mDtData);

                // Add a new command handler which saves a report in a custom way.
                mDesignPanel.AddCommandHandler(new clsReportDesignerSaveCommandHandler(mDesignPanel, mFileName));

                // Load a report into the design form and show the form.
                mDesignPanel.OpenReport(mReportDoc);
                mDesignForm.ShowDialog();
                mDesignPanel.CloseReport();
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

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region checkedit and grid checking

        #region chkAll_CheckedChanged
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (pCheckedCheckBox == true)
            {
                DataTable mDataTable = (DataTable)grdDXTPrintPrescriptions.DataSource;
                foreach (DataRow mDataRow in mDataTable.Rows)
                {
                    mDataRow.BeginEdit();
                    mDataRow["selected"] = chkAll.Checked;
                    mDataRow.EndEdit();
                }
            }
        }
        #endregion

        #region selected_CheckedChanged
        void selected_CheckedChanged(object sender, EventArgs e)
        {
            if (pCheckedGrid == true)
            {
                grdDXTPrintPrescriptions.FocusedView.PostEditor();

                int mChecked = 0;
                int mUnChecked = 0;

                DataTable mDataTable = (DataTable)grdDXTPrintPrescriptions.DataSource;
                foreach (DataRow mDataRow in mDataTable.Rows)
                {
                    if (Convert.ToBoolean(mDataRow["selected"]) == true)
                    {
                        mChecked++;
                    }
                    else
                    {
                        mUnChecked++;
                    }
                }

                if (mChecked == mDataTable.Rows.Count)
                {
                    chkAll.CheckState = CheckState.Checked;
                }
                else if (mUnChecked == mDataTable.Rows.Count)
                {
                    chkAll.CheckState = CheckState.Unchecked;
                }
                else
                {
                    chkAll.CheckState = CheckState.Indeterminate;
                }
            }
        }
        #endregion

        #region chkAll_Enter
        private void chkAll_Enter(object sender, EventArgs e)
        {
            pCheckedGrid = false;
            pCheckedCheckBox = true;
        }
        #endregion

        #region grdDXTPrintPrescriptions_Enter
        private void grdDXTPrintPrescriptions_Enter(object sender, EventArgs e)
        {
            pCheckedCheckBox = false;
            pCheckedGrid = true;
        }
        #endregion

        #endregion

        #region PrintingSystem_StartPrint
        private void PrintingSystem_StartPrint(object sender, DevExpress.XtraPrinting.PrintDocumentEventArgs e)
        {
            // setting the specific printer's name before printing
            if (pPrinterName.Trim() != "")
            {
                e.PrintDocument.PrinterSettings.PrinterName = pPrinterName;
            }
        }
        #endregion

        #region cmdPreview_Click
        private void cmdPreview_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdPreview_Click";

            this.Cursor = Cursors.WaitCursor;

            try
            {
                bool mSelected = false;
                DataTable mDataTable = (DataTable)grdDXTPrintPrescriptions.DataSource;

                DataTable mDtPrinted = new DataTable("printed");
                mDtPrinted.Columns.Add("patientcode", typeof(System.String));
                mDtPrinted.Columns.Add("transdate", typeof(System.DateTime));

                foreach (DataRow mDataRow in mDataTable.Rows)
                {
                    if (Convert.ToBoolean(mDataRow["selected"]) == true)
                    {
                        mSelected = true;
                        DataRow mNewRow = mDtPrinted.NewRow();
                        mNewRow["patientcode"] = mDataRow["patientcode"];
                        mNewRow["transdate"] = mDataRow["transdate"];
                        mDtPrinted.Rows.Add(mNewRow);
                        mDtPrinted.AcceptChanges();
                    }
                }

                if (mSelected == false)
                {
                    Program.Display_Info("Please select at least one prescription");
                    return;
                }

                DataTable mDtUserGroupPrinters = pMdtUserGroupPrinters.View(
                    "machinename='" + Program.gMachineName + "' and usergroupcode='"
                    + Program.gCurrentUser.UserGroupCode + "' and (documenttypecode='receipts' or documenttypecode='invoices')", "", "", "");
                if (mDtUserGroupPrinters.Rows.Count > 0)
                {
                    pPrinterName = mDtUserGroupPrinters.Rows[0]["printername"].ToString().Trim();
                }

                string mFileName = "Pre_Treat";

                foreach (DataRow mDataRow in mDataTable.Rows)
                {
                    if (Convert.ToBoolean(mDataRow["selected"]) == true)
                    {
                        string mFilter = "patientcode='" + mDataRow["patientcode"].ToString().Trim() + "' and transdate='" + Convert.ToDateTime(mDataRow["transdate"]).ToString("yyyy-MM-dd") + "'";

                        DataTable mDtData = pMdtReporter.Get_DataFromDBView("view_dxtpatientprescriptions", null, mFilter, "");

                        byte[] mBytes = pMdtReporter.Load_ReportTemplate(mFileName, false);

                        DevExpress.XtraReports.UI.XtraReport mReportDoc = Program.Load_ReportTemplate(mBytes, mDtData);
                        mReportDoc.PrintingSystem.StartPrint += new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);

                        if (mReportDoc == null)
                        {
                            return;
                        }

                        frmReportViewer mReportViewer = new frmReportViewer();
                        mReportViewer.printControl1.PrintingSystem = mReportDoc.PrintingSystem;
                        mReportDoc.CreateDocument();
                        mReportViewer.Show();
                    }
                }
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

        #region searching logic

        #region Data_Clear
        private void Data_Clear()
        {
            txtName.Text = "";
            txtGender.Text = "";
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
                        int mDays = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue).Subtract(mPatient.birthdate).Days;
                        int mYears = (int)mDays / 365;
                        int mMonths = (int)(mDays % 365) / 30;

                        txtYears.Text = mYears.ToString();
                        txtMonths.Text = mMonths.ToString();

                        pCurrPatientId = mPatient.code;
                        pPrevPatientId = pCurrPatientId;

                        this.Data_Fill(txtPatientId.Text);

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

        #endregion

        private void txtDateFrom_EditValueChanged(object sender, EventArgs e)
        {
            this.Data_Fill(txtPatientId.Text);
        }

        private void txtDateTo_EditValueChanged(object sender, EventArgs e)
        {
            this.Data_Fill(txtPatientId.Text);
        }
    }
}