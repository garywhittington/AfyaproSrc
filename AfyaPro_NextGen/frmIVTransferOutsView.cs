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
    public partial class frmIVTransferOutsView : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsSomStores pMdtSomStores;
        private AfyaPro_MT.clsSomTransferOuts pMdtSomTransferOuts;
        private AfyaPro_MT.clsReporter pMdtReporter;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private String pClassName = "";

        private DataTable pDtStores = new DataTable("stores");

        private bool pDoubleEntry = false;

        #endregion

        #region frmIVTransferOutsView
        public frmIVTransferOutsView()
        {
            InitializeComponent();

            String mFunctionName = "frmIVTransferOutsView";

            try
            {
                pType = this.GetType();
                pClassName = pType.FullName;

                pMdtSomStores = (AfyaPro_MT.clsSomStores)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomStores),
                    Program.gMiddleTier + "clsSomStores");

                pMdtSomTransferOuts = (AfyaPro_MT.clsSomTransferOuts)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomTransferOuts),
                    Program.gMiddleTier + "clsSomTransferOuts");

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
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivtransferouts_design.ToString());
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

        #region frmIVTransferOutsView_Load
        private void frmIVTransferOutsView_Load(object sender, EventArgs e)
        {
            radTransferStatus.SelectedIndex = 1;

            //Data_Fill
            cboStore.ItemIndex = Program.Get_LookupItemIndex(cboStore, "code", Program.gCurrentUser.StoreCode);
            this.Data_Fill();

            DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                (DevExpress.XtraGrid.Views.Grid.GridView)grdIVTransferOutsView.MainView;
            mGridView.OptionsBehavior.Editable = false;
            mGridView.OptionsView.ShowGroupPanel = true;
            mGridView.OptionsView.ShowFooter = true;
            mGridView.OptionsCustomization.AllowGroup = true;

            grdIVTransferOutsView.ForceInitialize();
            Program.Restore_GridLayout(grdIVTransferOutsView, grdIVTransferOutsView.Name);

            this.Load_Controls();

            timer1.Interval = Program.gTransferOutRefreshInterval * 10000;
            timer1.Enabled = true;
            timer1.Start();
        }
        #endregion

        #region frmIVTransferOutsView_FormClosing
        private void frmIVTransferOutsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                MemoryStream mMemoryStream = new MemoryStream();
                grdIVTransferOutsView.MainView.SaveLayoutToStream(mMemoryStream);

                Program.gMdtFacilitySetup.Save_GridLayout(grdIVTransferOutsView.Name, Program.gCurrentUser.Code, mMemoryStream.ToArray());
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
            mObjectsList.Add(radTransferStatus.Properties.Items[0]);
            mObjectsList.Add(radTransferStatus.Properties.Items[1]);
            mObjectsList.Add(radTransferStatus.Properties.Items[2]);
            mObjectsList.Add(cmdRefresh);
            mObjectsList.Add(cmdPreview);

            Program.Apply_Language("frmIVTransferOutsView", mObjectsList);
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

                switch (radTransferStatus.SelectedIndex)
                {
                    case 1:
                        {
                            mExtraFilter = "transferstatus='" + AfyaPro_Types.clsEnums.SomOrderStatus.Open.ToString() + "'";
                        }
                        break;
                    case 2:
                        {
                            mExtraFilter = "transferstatus='" + AfyaPro_Types.clsEnums.SomOrderStatus.Closed.ToString() + "'";
                        }
                        break;
                    case 3:
                        {
                            mExtraFilter = "transferstatus='" + AfyaPro_Types.clsEnums.SomOrderStatus.Partial.ToString() + "'";
                        }
                        break;
                }

                if (cboStore.ItemIndex != -1)
                {
                    if (cboStore.GetColumnValue("code").ToString().Trim() != "")
                    {
                        if (mExtraFilter.Trim() == "")
                        {
                            mExtraFilter = "fromcode='" + cboStore.GetColumnValue("code").ToString().Trim() + "'";
                        }
                        else
                        {
                            mExtraFilter = mExtraFilter + " and fromcode='" + cboStore.GetColumnValue("code").ToString().Trim() + "'";
                        }
                    }
                }

                DataTable mDtOrders = pMdtSomTransferOuts.View_TransferOuts(mDateSpecified, mDateFrom, mDateTo,
                    mExtraFilter, "transdate desc,autocode desc", Program.gLanguageName, grdIVTransferOutsView.Name);
                grdIVTransferOutsView.DataSource = mDtOrders;

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

        #region cmdDelete_Click
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdDelete_Click";

            if (Program.IsDate(Program.gMdiForm.txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                return;
            }

            try
            {
                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                                    (DevExpress.XtraGrid.Views.Grid.GridView)grdIVTransferOutsView.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                DialogResult mResp = Program.Confirm_Deletion("'"
                    + mSelectedRow["diagnosiscode"].ToString().Trim() + "'   '"
                    + mSelectedRow["diagnosisdescription"].ToString().Trim() + "'");

                if (mResp != DialogResult.Yes)
                {
                    return;
                }

                ////add 
                //pResult = pMdtSomTransferOuts.Delete(mTransDate,
                //    Convert.ToInt32(mSelectedRow["autocode"]), Program.gCurrentUser.Code);
                //if (pResult.Exe_Result == 0)
                //{
                //    Program.Display_Error(pResult.Exe_Message);
                //    return;
                //}
                //if (pResult.Exe_Result == -1)
                //{
                //    Program.Display_Server_Error(pResult.Exe_Message);
                //    return;
                //}

                //refresh
                this.Data_Fill();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region grdIVTransferOutsView_DoubleClick
        private void grdIVTransferOutsView_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)grdIVTransferOutsView.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                clsMainWindowCodes.Edit_Clicked();
            }
            catch
            {
                return;
            }
        }
        #endregion

        #region grdIVTransferOutsView_KeyDown
        private void grdIVTransferOutsView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                        (DevExpress.XtraGrid.Views.Grid.GridView)grdIVTransferOutsView.MainView;

                    if (mGridView.FocusedRowHandle < 0)
                    {
                        return;
                    }

                    clsMainWindowCodes.Delete_Clicked();
                }
                catch
                {
                    return;
                }
            }
        }
        #endregion

        #region cmdEdit_Click
        private void cmdEdit_Click(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView mGridView;

            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            switch (Program.gMdiForm.ActiveMdiChild.Name.Trim().ToLower())
            {
                case "frmivtransferoutsview":
                    {
                        mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)((frmIVTransferOutsView)Program.gMdiForm.ActiveMdiChild).grdIVTransferOutsView.MainView;
                    }
                    break;
                default:
                    {
                        mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain.MainView;
                    }
                    break;
            }

            if (mGridView.FocusedRowHandle < 0)
            {
                Program.gMdiForm.Cursor = Cursors.Default;
                return;
            }

            DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

            if (mSelectedRow["transfertype"].ToString().Trim().ToLower() ==
                AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString().Trim().ToLower())
            {
                string mStoreCode = "";
                if (cboStore.ItemIndex != -1)
                {
                    mStoreCode = cboStore.GetColumnValue("code").ToString();
                }

                frmIVTransferOutsStore mIVTransferOutsStore = new frmIVTransferOutsStore(mStoreCode);
                mIVTransferOutsStore.TransferInNo = mSelectedRow["transferinno"].ToString().Trim();
                mIVTransferOutsStore.gDataState = "Edit";
                mIVTransferOutsStore.Show();
            }
            else
            {
                string mStoreCode = "";
                if (cboStore.ItemIndex != -1)
                {
                    mStoreCode = cboStore.GetColumnValue("code").ToString();
                }

                frmIVTransferOuts mIVTransferOuts = new frmIVTransferOuts(mStoreCode);
                mIVTransferOuts.gDataState = "Edit";
                mIVTransferOuts.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;
        }
        #endregion

        #region cmdIssue_Click
        private void cmdIssue_Click(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView mGridView;

            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            switch (Program.gMdiForm.ActiveMdiChild.Name.Trim().ToLower())
            {
                case "frmivtransferoutsview":
                    {
                        mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)((frmIVTransferOutsView)Program.gMdiForm.ActiveMdiChild).grdIVTransferOutsView.MainView;
                    }
                    break;
                default:
                    {
                        mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain.MainView;
                    }
                    break;
            }

            if (mGridView.FocusedRowHandle < 0)
            {
                Program.gMdiForm.Cursor = Cursors.Default;
                return;
            }

            frmIVTransferOutsIssue mIVTransferOutsIssue = new frmIVTransferOutsIssue();
            mIVTransferOutsIssue.gDataState = "Edit";
            mIVTransferOutsIssue.Show();

            Program.gMdiForm.Cursor = Cursors.Default;
        }
        #endregion

        #region timer1_Tick

        private void timer1_Tick(object sender, EventArgs e)
        {
            //System.Threading.Thread PBThread = new System.Threading.Thread(new System.Threading.ThreadStart(DoWork));
            //PBThread.Start();
        }

        public void DoWork()
        {
            this.Invoke(new Refresh_Grid(Data_Fill));
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
                DataTable mDtData = pMdtReporter.Get_DataFromDBView("view_transferoutdoc", null, mFilter, "");

                string mFileName = "IV_TransferOutDoc";
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
                    (DevExpress.XtraGrid.Views.Grid.GridView)((frmIVTransferOutsView)Program.gMdiForm.ActiveMdiChild).grdIVTransferOutsView.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                string mFilter = "transferno='" + mSelectedRow["transferno"].ToString() + "'";
                string mFileName = "IV_TransferOutDoc";
                DataTable mDtData = pMdtReporter.Get_DataFromDBView("view_transferoutdoc", null, mFilter, "");

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