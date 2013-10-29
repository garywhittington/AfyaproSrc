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
    public partial class frmIVTransferInsView : DevExpress.XtraEditors.XtraForm
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

        #region frmIVTransferInsView
        public frmIVTransferInsView()
        {
            InitializeComponent();

            String mFunctionName = "frmIVTransferInsView";

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
                cmdDesign.Enabled = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivtransferins_design.ToString());
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

        #region frmIVTransferInsView_Load
        private void frmIVTransferInsView_Load(object sender, EventArgs e)
        {
            radTransferStatus.SelectedIndex = 1;

            //Data_Fill
            cboStore.ItemIndex = Program.Get_LookupItemIndex(cboStore, "code", Program.gCurrentUser.StoreCode);
            this.Data_Fill();

            DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                (DevExpress.XtraGrid.Views.Grid.GridView)grdIVTransferInsView.MainView;
            mGridView.OptionsBehavior.Editable = false;
            mGridView.OptionsView.ShowGroupPanel = true;
            mGridView.OptionsView.ShowFooter = true;
            mGridView.OptionsCustomization.AllowGroup = true;

            grdIVTransferInsView.ForceInitialize();
            Program.Restore_GridLayout(grdIVTransferInsView, grdIVTransferInsView.Name);

            this.Load_Controls();
        }
        #endregion

        #region frmIVTransferInsView_FormClosing
        private void frmIVTransferInsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                MemoryStream mMemoryStream = new MemoryStream();
                grdIVTransferInsView.MainView.SaveLayoutToStream(mMemoryStream);

                Program.gMdtFacilitySetup.Save_GridLayout(grdIVTransferInsView.Name, Program.gCurrentUser.Code, mMemoryStream.ToArray());
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

            Program.Apply_Language("frmIVTransferInsView", mObjectsList);
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
                            mExtraFilter = "tocode='" + cboStore.GetColumnValue("code").ToString().Trim() + "'";
                        }
                        else
                        {
                            mExtraFilter = mExtraFilter + " and tocode='" + cboStore.GetColumnValue("code").ToString().Trim() + "'";
                        }
                    }
                }

                DataTable mDtOrders = pMdtSomTransferIns.View_TransferIns(mDateSpecified, mDateFrom, mDateTo,
                    mExtraFilter, "transdate desc,autocode desc", Program.gLanguageName, grdIVTransferInsView.Name);
                grdIVTransferInsView.DataSource = mDtOrders;

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
                                    (DevExpress.XtraGrid.Views.Grid.GridView)grdIVTransferInsView.MainView;

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
                //pResult = pMdtSomTransferIns.Delete(mTransDate,
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

        #region grdIVTransferInsView_DoubleClick
        private void grdIVTransferInsView_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)grdIVTransferInsView.MainView;

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

        #region grdIVTransferInsView_KeyDown
        private void grdIVTransferInsView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                        (DevExpress.XtraGrid.Views.Grid.GridView)grdIVTransferInsView.MainView;

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
                case "frmivtransferinsview":
                    {
                        mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)((frmIVTransferInsView)Program.gMdiForm.ActiveMdiChild).grdIVTransferInsView.MainView;
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

                frmIVTransferInsStore mIVTransferInsStore = new frmIVTransferInsStore(mStoreCode);
                mIVTransferInsStore.TransferOutNo = mSelectedRow["transferoutno"].ToString().Trim();
                mIVTransferInsStore.gDataState = "Edit";
                mIVTransferInsStore.Show();
            }
            else
            {
                string mStoreCode = "";
                if (cboStore.ItemIndex != -1)
                {
                    mStoreCode = cboStore.GetColumnValue("code").ToString();
                }

                frmIVTransferIns mIVTransferIns = new frmIVTransferIns(mStoreCode);
                mIVTransferIns.gDataState = "Edit";
                mIVTransferIns.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;
        }
        #endregion

        #region cmdReceive_Click
        private void cmdReceive_Click(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView mGridView;

            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            switch (Program.gMdiForm.ActiveMdiChild.Name.Trim().ToLower())
            {
                case "frmivtransferinsview":
                    {
                        mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)((frmIVTransferInsView)Program.gMdiForm.ActiveMdiChild).grdIVTransferInsView.MainView;
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

            frmIVTransferInsReceive mIVTransferInsReceive = new frmIVTransferInsReceive();
            mIVTransferInsReceive.gDataState = "Edit";
            mIVTransferInsReceive.Show();

            Program.gMdiForm.Cursor = Cursors.Default;
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
                DataTable mDtData = pMdtReporter.Get_DataFromDBView("view_transferindoc", null, mFilter, "");

                string mFileName = "IV_TransferInDoc";
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
                    (DevExpress.XtraGrid.Views.Grid.GridView)((frmIVTransferInsView)Program.gMdiForm.ActiveMdiChild).grdIVTransferInsView.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                string mFilter = "transferno='" + mSelectedRow["transferno"].ToString() + "'";
                string mFileName = "IV_TransferInDoc";
                DataTable mDtData = pMdtReporter.Get_DataFromDBView("view_transferindoc", null, mFilter, "");

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