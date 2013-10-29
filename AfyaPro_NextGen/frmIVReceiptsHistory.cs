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
    public partial class frmIVReceiptsHistory : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsSomStores pMdtSomStores;
        private AfyaPro_MT.clsSomTransferIns pMdtSomTransferIns;
        private AfyaPro_MT.clsReporter pMdtReporter;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private String pClassName = "";

        private DataTable pDtStores = new DataTable("stores");

        #endregion

        #region frmIVReceiptsHistory
        public frmIVReceiptsHistory()
        {
            InitializeComponent();

            String mFunctionName = "frmIVReceiptsHistory";

            try
            {
                pType = this.GetType();
                pClassName = pType.FullName;

                pMdtSomStores = (AfyaPro_MT.clsSomStores)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomStores),
                    Program.gMiddleTier + "clsSomStores");

                pMdtSomTransferIns = (AfyaPro_MT.clsSomTransferIns)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomTransferIns),
                    Program.gMiddleTier + "clsSomTransferIns");

                pMdtReporter = (AfyaPro_MT.clsReporter)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReporter),
                    Program.gMiddleTier + "clsReporter");

                #region stores
                pDtStores.Columns.Add("code", typeof(System.String));
                pDtStores.Columns.Add("description", typeof(System.String));
                cboStore.Properties.DataSource = pDtStores;
                cboStore.Properties.DisplayMember = "description";
                cboStore.Properties.ValueMember = "code";
                #endregion

                this.Fill_LookupData();

                txtDateFrom.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                txtDateTo.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);

                DateTime mCurrDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);
                txtDateFrom.EditValue = new DateTime(mCurrDate.Year, mCurrDate.Month, 1);
                txtDateTo.EditValue = mCurrDate;

                layoutControl1.AllowCustomizationMenu = false;
                cmdDesignOrder.Enabled = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivreceiptshistory_design.ToString());
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

        #region frmIVReceiptsHistory_Load
        private void frmIVReceiptsHistory_Load(object sender, EventArgs e)
        {
            //Data_Fill
            cboStore.ItemIndex = Program.Get_LookupItemIndex(cboStore, "code", Program.gCurrentUser.StoreCode);
            this.Data_Fill();

            DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                (DevExpress.XtraGrid.Views.Grid.GridView)grdIVReceiptsHistory.MainView;
            mGridView.OptionsBehavior.Editable = false;
            mGridView.OptionsView.ShowGroupPanel = true;
            mGridView.OptionsView.ShowFooter = true;
            mGridView.OptionsCustomization.AllowGroup = true;

            grdIVReceiptsHistory.ForceInitialize();
            Program.Restore_GridLayout(grdIVReceiptsHistory, grdIVReceiptsHistory.Name);

            this.Load_Controls();
        }
        #endregion

        #region frmIVReceiptsHistory_FormClosing
        private void frmIVReceiptsHistory_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                MemoryStream mMemoryStream = new MemoryStream();
                grdIVReceiptsHistory.MainView.SaveLayoutToStream(mMemoryStream);

                Program.gMdtFacilitySetup.Save_GridLayout(grdIVReceiptsHistory.Name, Program.gCurrentUser.Code, mMemoryStream.ToArray());
            }
            catch { }
        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            DataRow mNewRow;
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region stores

                pDtStores.Rows.Clear();
                DataTable mDtStores = pMdtSomStores.View("", "description", Program.gLanguageName, "grdIVSStores");

                DataView mDvUserStores = new DataView();
                mDvUserStores.Table = Program.gDtUserStores;
                mDvUserStores.Sort = "storecode";

                mNewRow = pDtStores.NewRow();
                mNewRow["code"] = "";
                mNewRow["description"] = "";
                pDtStores.Rows.Add(mNewRow);
                pDtStores.AcceptChanges();
                foreach (DataRow mDataRow in mDtStores.Rows)
                {
                    if (mDvUserStores.Find(mDataRow["code"].ToString().Trim()) >= 0)
                    {
                        mNewRow = pDtStores.NewRow();
                        mNewRow["code"] = mDataRow["code"].ToString();
                        mNewRow["description"] = mDataRow["description"].ToString();
                        pDtStores.Rows.Add(mNewRow);
                        pDtStores.AcceptChanges();
                    }
                }

                foreach (DataColumn mDataColumn in pDtStores.Columns)
                {
                    mDataColumn.Caption = mDtStores.Columns[mDataColumn.ColumnName].Caption;
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

        #region cboStore_EditValueChanged
        private void cboStore_EditValueChanged(object sender, EventArgs e)
        {
            string mFunctionName = "cboStore_EditValueChanged";

            try
            {
                this.Data_Fill();
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

            mObjectsList.Add(grpDate);
            mObjectsList.Add(txbDateFrom);
            mObjectsList.Add(txbDateTo);
            mObjectsList.Add(cmdRefresh);
            mObjectsList.Add(cmdPreview);

            Program.Apply_Language("frmIVReceiptsHistory", mObjectsList);
        }
        #endregion

        #region Data_Fill
        private delegate void Refresh_Grid();
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

                Program.gMdiForm.Cursor = Cursors.WaitCursor;

                string mExtraFilter = "";

                if (cboStore.ItemIndex != -1)
                {
                    if (cboStore.GetColumnValue("code").ToString().Trim() != "")
                    {
                        if (mExtraFilter.Trim() == "")
                        {
                            mExtraFilter = "tocode='" + cboStore.GetColumnValue("code").ToString().Trim() + "'";
                        }
                        else
                        {
                            mExtraFilter = mExtraFilter + " and tocode='" + cboStore.GetColumnValue("code").ToString().Trim() + "'";
                        }
                    }
                }

                DataTable mDtOrders = pMdtSomTransferIns.View_Receipts(mDateSpecified, mDateFrom, mDateTo,
                    mExtraFilter, "transdate desc,autocode desc", Program.gLanguageName, grdIVReceiptsHistory.Name);
                grdIVReceiptsHistory.DataSource = mDtOrders;

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

        #region cmdDesignOrder_Click
        private void cmdDesignOrder_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdDesignOrder_Click";

            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Create a design form and get its panel.
                DevExpress.XtraReports.UserDesigner.XRDesignFormEx mDesignForm =
                    new DevExpress.XtraReports.UserDesigner.XRDesignFormEx();
                DevExpress.XtraReports.UserDesigner.XRDesignPanel mDesignPanel = mDesignForm.DesignPanel;

                //retrieve data
                string mFilter = "1=2";
                DataTable mDtData = pMdtReporter.Get_DataFromDBView("view_ivreceiptdoc", null, mFilter, "");

                string mFileName = "IV_ReceiptDoc";
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

        #region cmdPreview_Click
        private void cmdPreview_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdPreview_Click";

            try
            {
                this.Cursor = Cursors.WaitCursor;

                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)((frmIVReceiptsHistory)Program.gMdiForm.ActiveMdiChild).grdIVReceiptsHistory.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                string mFilter = "deliveryno='" + mSelectedRow["deliveryno"].ToString() + "'";
                string mFileName = "IV_ReceiptDoc";
                DataTable mDtData = pMdtReporter.Get_DataFromDBView("view_ivreceiptdoc", null, mFilter, "");

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
    }
}