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
    public partial class frmIVPurchaseOrdersView : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsSomOrders pMdtSomOrders;
        private AfyaPro_MT.clsReporter pMdtReporter;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private String pClassName = "";

        #endregion

        #region frmIVPurchaseOrdersView
        public frmIVPurchaseOrdersView()
        {
            InitializeComponent();

            String mFunctionName = "frmIVPurchaseOrdersView";

            try
            {
                pType = this.GetType();
                pClassName = pType.FullName;

                pMdtSomOrders = (AfyaPro_MT.clsSomOrders)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomOrders),
                    Program.gMiddleTier + "clsSomOrders");

                pMdtReporter = (AfyaPro_MT.clsReporter)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReporter),
                    Program.gMiddleTier + "clsReporter");

                txtDateFrom.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                txtDateTo.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);

                DateTime mCurrDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);
                txtDateFrom.EditValue = new DateTime(mCurrDate.Year, mCurrDate.Month, 1);
                txtDateTo.EditValue = mCurrDate;

                layoutControl1.AllowCustomizationMenu = false;
                cmdDesign.Enabled = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivorders_design.ToString());
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

        #region frmIVPurchaseOrdersView_Load
        private void frmIVPurchaseOrdersView_Load(object sender, EventArgs e)
        {
            radOrderStatus.SelectedIndex = 1;

            //Data_Fill
            this.Data_Fill();

            DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                (DevExpress.XtraGrid.Views.Grid.GridView)grdIVPurchaseOrdersView.MainView;
            mGridView.OptionsBehavior.Editable = false;
            mGridView.OptionsView.ShowGroupPanel = true;
            mGridView.OptionsView.ShowFooter = true;
            mGridView.OptionsView.RowAutoHeight = true;
            mGridView.OptionsCustomization.AllowGroup = true;
            mGridView.Columns["supplierdescription"].ColumnEdit = supplierdescription;

            grdIVPurchaseOrdersView.ForceInitialize();
            Program.Restore_GridLayout(grdIVPurchaseOrdersView, grdIVPurchaseOrdersView.Name);

            this.Load_Controls();
        }
        #endregion

        #region frmIVPurchaseOrdersView_FormClosing
        private void frmIVPurchaseOrdersView_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                MemoryStream mMemoryStream = new MemoryStream();
                grdIVPurchaseOrdersView.MainView.SaveLayoutToStream(mMemoryStream);

                Program.gMdtFacilitySetup.Save_GridLayout(grdIVPurchaseOrdersView.Name, Program.gCurrentUser.Code, mMemoryStream.ToArray());
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
            mObjectsList.Add(radOrderStatus.Properties.Items[0]);
            mObjectsList.Add(radOrderStatus.Properties.Items[1]);
            mObjectsList.Add(radOrderStatus.Properties.Items[2]);
            mObjectsList.Add(cmdRefresh);
            mObjectsList.Add(cmdPreview);

            Program.Apply_Language("frmIVPurchaseOrdersView", mObjectsList);
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

                switch (radOrderStatus.SelectedIndex)
                {
                    case 1:
                        {
                            mExtraFilter = "orderstatus='" + AfyaPro_Types.clsEnums.SomOrderStatus.Open.ToString() + "'";
                        }
                        break;
                    case 2:
                        {
                            mExtraFilter = "orderstatus='" + AfyaPro_Types.clsEnums.SomOrderStatus.Closed.ToString() + "'";
                        }
                        break;
                    case 3:
                        {
                            mExtraFilter = "orderstatus='" + AfyaPro_Types.clsEnums.SomOrderStatus.Partial.ToString() + "'";
                        }
                        break;
                }


                DataTable mDtOrders = pMdtSomOrders.View_Orders(mDateSpecified, mDateFrom, mDateTo,
                    mExtraFilter, "transdate desc,autocode desc", Program.gLanguageName, grdIVPurchaseOrdersView.Name);
                grdIVPurchaseOrdersView.DataSource = mDtOrders;

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
                                    (DevExpress.XtraGrid.Views.Grid.GridView)grdIVPurchaseOrdersView.MainView;

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
                //pResult = pMdtSomOrders.Delete(mTransDate,
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

        #region grdIVPurchaseOrdersView_DoubleClick
        private void grdIVPurchaseOrdersView_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)grdIVPurchaseOrdersView.MainView;

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

        #region grdIVPurchaseOrdersView_KeyDown
        private void grdIVPurchaseOrdersView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                        (DevExpress.XtraGrid.Views.Grid.GridView)grdIVPurchaseOrdersView.MainView;

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
                case "frmivpurchaseordersview":
                    {
                        mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)((frmIVPurchaseOrdersView)Program.gMdiForm.ActiveMdiChild).grdIVPurchaseOrdersView.MainView;
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

            frmIVPurchaseOrders mIVPurchaseOrders = new frmIVPurchaseOrders();
            mIVPurchaseOrders.gDataState = "Edit";
            mIVPurchaseOrders.Show();

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
                case "frmivpurchaseordersview":
                    {
                        mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)((frmIVPurchaseOrdersView)Program.gMdiForm.ActiveMdiChild).grdIVPurchaseOrdersView.MainView;
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

            frmIVPurchaseOrdersReceive mIVPurchaseOrdersReceive = new frmIVPurchaseOrdersReceive();
            mIVPurchaseOrdersReceive.gDataState = "Edit";
            mIVPurchaseOrdersReceive.Show();

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
                DataTable mDtData = pMdtReporter.Get_DataFromDBView("view_purchaseorderdoc", null, mFilter, "");

                string mFileName = "IV_PurchaseOrderDoc";
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
                    (DevExpress.XtraGrid.Views.Grid.GridView)((frmIVPurchaseOrdersView)Program.gMdiForm.ActiveMdiChild).grdIVPurchaseOrdersView.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                string mFilter = "orderno='" + mSelectedRow["orderno"].ToString() + "'";
                string mFileName = "IV_PurchaseOrderDoc";
                DataTable mDtData = pMdtReporter.Get_DataFromDBView("view_purchaseorderdoc", null, mFilter, "");

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