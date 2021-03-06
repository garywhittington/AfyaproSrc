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
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace AfyaPro_NextGen
{
    public partial class frmIVTransferInsReceive : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsSomTransferIns pMdtSomTransferIns;
        private AfyaPro_MT.clsSomTransferOuts pMdtSomTransferOuts;
        private AfyaPro_MT.clsSomPackagings pMdtSomPackagings;
        private AfyaPro_MT.clsCurrencies pMdtCurrencies;
        private AfyaPro_MT.clsSomSuppliers pMdtSomSuppliers;
        private AfyaPro_MT.clsSomProducts pMdtSomProducts;
        private AfyaPro_MT.clsSomStores pMdtSomStores;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataTable pDtItems = new DataTable("items");
        private DataTable pDtExpiryDates = new DataTable("expirydates");
        private DataRow pSelectedRow = null;
        internal string gDataState = "";

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private DataTable pDtPackagings = new DataTable("packagings");
        private DataTable pDtCurrencies = new DataTable("currencies");
        private DataTable pDtReceived = new DataTable("received");

        #endregion

        #region frmIVTransferInsReceive
        public frmIVTransferInsReceive()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmIVPurchaseOrder";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                this.CancelButton = cmdClose;

                pMdtSomTransferIns = (AfyaPro_MT.clsSomTransferIns)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomTransferIns),
                    Program.gMiddleTier + "clsSomTransferIns");

                pMdtSomTransferOuts = (AfyaPro_MT.clsSomTransferOuts)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomTransferOuts),
                    Program.gMiddleTier + "clsSomTransferOuts");

                pMdtSomPackagings = (AfyaPro_MT.clsSomPackagings)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomPackagings),
                    Program.gMiddleTier + "clsSomPackagings");

                pMdtCurrencies = (AfyaPro_MT.clsCurrencies)Activator.GetObject(
                    typeof(AfyaPro_MT.clsCurrencies),
                    Program.gMiddleTier + "clsCurrencies");

                pMdtSomSuppliers = (AfyaPro_MT.clsSomSuppliers)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomSuppliers),
                    Program.gMiddleTier + "clsSomSuppliers");

                pMdtSomProducts = (AfyaPro_MT.clsSomProducts)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomProducts),
                    Program.gMiddleTier + "clsSomProducts");

                pMdtSomStores = (AfyaPro_MT.clsSomStores)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomStores),
                    Program.gMiddleTier + "clsSomStores");

                pMdtAutoCodes = (AfyaPro_MT.clsAutoCodes)Activator.GetObject(
                    typeof(AfyaPro_MT.clsAutoCodes),
                    Program.gMiddleTier + "clsAutoCodes");

                #region currencies
                pDtCurrencies.Columns.Add("code", typeof(System.String));
                pDtCurrencies.Columns.Add("description", typeof(System.String));
                pDtCurrencies.Columns.Add("exchangerate", typeof(System.Double));
                pDtCurrencies.Columns.Add("currencysymbol", typeof(System.String));
                cboCurrency.Properties.DataSource = pDtCurrencies;
                cboCurrency.Properties.DisplayMember = "description";
                cboCurrency.Properties.ValueMember = "code";
                #endregion

                #region packaging lookupedit
                pDtPackagings.Columns.Add("code", typeof(System.String));
                pDtPackagings.Columns.Add("description", typeof(System.String));
                pDtPackagings.Columns.Add("pieces", typeof(System.Int32));
                packagingeditor.DataSource = pDtPackagings;
                #endregion

                this.Fill_LookupData();

                pDtItems.Columns.Add("itemtransferid", typeof(System.String));
                pDtItems.Columns.Add("productcode", typeof(System.String));
                pDtItems.Columns.Add("productdescription", typeof(System.String));
                pDtItems.Columns.Add("productopmcode", typeof(System.String));
                pDtItems.Columns.Add("productopmdescription", typeof(System.String));
                pDtItems.Columns.Add("productdepartmentcode", typeof(System.String));
                pDtItems.Columns.Add("productdepartmentdescription", typeof(System.String));
                pDtItems.Columns.Add("packagingcode", typeof(System.String));
                pDtItems.Columns.Add("packagingdescription", typeof(System.String));
                pDtItems.Columns.Add("piecesinpackage", typeof(System.Int32));
                pDtItems.Columns.Add("onhandqty", typeof(System.Double));
                pDtItems.Columns.Add("transferedqty", typeof(System.Double));
                pDtItems.Columns.Add("receivedtodate", typeof(System.Double));
                pDtItems.Columns.Add("receivedqty", typeof(System.Double));
                pDtItems.Columns.Add("transprice", typeof(System.Double));
                pDtItems.Columns.Add("amount", typeof(System.Double));
                pDtItems.Columns.Add("hasexpiry", typeof(System.Boolean));

                pDtExpiryDates.Columns.Add("itemtransferid", typeof(System.String));
                pDtExpiryDates.Columns.Add("productcode", typeof(System.String));
                pDtExpiryDates.Columns.Add("expirydate", typeof(System.DateTime));
                pDtExpiryDates.Columns.Add("quantity", typeof(System.Double));

                grdIVTransferInReceiveItems.DataSource = pDtItems;

                foreach (DevExpress.XtraGrid.Columns.GridColumn mGridColumn in viewIVPurchaseOrderItems.Columns)
                {
                    if (mGridColumn.FieldName.ToLower() != "packagingdescription"
                        && mGridColumn.FieldName.ToLower() != "receivedqty"
                        && mGridColumn.FieldName.ToLower() != "transprice")
                    {
                        mGridColumn.OptionsColumn.AllowEdit = false;
                    }
                }

                viewIVPurchaseOrderItems.Columns["packagingdescription"].ColumnEdit = packagingeditor;

                txtDate.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                txtDeliveryDate.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                txtDate.EditValue = Program.gMdiForm.txtDate.EditValue;
                txtDeliveryDate.EditValue = Program.gMdiForm.txtDate.EditValue;
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

        #region packagingeditor_EditValueChanged
        private void packagingeditor_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.LookUpEdit mLookupEdit = sender as DevExpress.XtraEditors.LookUpEdit;

            if (viewIVPurchaseOrderItems.FocusedRowHandle < 0)
            {
                return;
            }

            DataRow mSelectedRow = viewIVPurchaseOrderItems.GetDataRow(viewIVPurchaseOrderItems.FocusedRowHandle);

            //get previous cost and onhandqty
            double mUnitPrice = Convert.ToDouble(mSelectedRow["transprice"]) / Convert.ToDouble(mSelectedRow["piecesinpackage"]);
            double mOnHandQty = Convert.ToDouble(mSelectedRow["onhandqty"]) * Convert.ToDouble(mSelectedRow["piecesinpackage"]);
            double mTransferedQty = Convert.ToDouble(mSelectedRow["transferedqty"]) * Convert.ToDouble(mSelectedRow["piecesinpackage"]);
            double mReceivedToDate = Convert.ToDouble(mSelectedRow["receivedtodate"]) * Convert.ToDouble(mSelectedRow["piecesinpackage"]);

            double mReceivedQty = Convert.ToDouble(mSelectedRow["receivedqty"]);
            double mTransPrice = mUnitPrice * Convert.ToInt32(mLookupEdit.GetColumnValue("pieces"));

            mSelectedRow.BeginEdit();

            mSelectedRow["packagingcode"] = mLookupEdit.GetColumnValue("code").ToString();
            mSelectedRow["packagingdescription"] = mLookupEdit.GetColumnValue("description").ToString();
            mSelectedRow["piecesinpackage"] = Convert.ToInt32(mLookupEdit.GetColumnValue("pieces"));
            mSelectedRow["onhandqty"] = mOnHandQty / Convert.ToInt32(mLookupEdit.GetColumnValue("pieces"));
            mSelectedRow["transferedqty"] = mTransferedQty / Convert.ToInt32(mLookupEdit.GetColumnValue("pieces"));
            mSelectedRow["receivedtodate"] = mReceivedToDate / Convert.ToInt32(mLookupEdit.GetColumnValue("pieces"));
            mSelectedRow["transprice"] = mUnitPrice * Convert.ToInt32(mLookupEdit.GetColumnValue("pieces"));
            mSelectedRow["amount"] = mTransPrice * mReceivedQty;

            mSelectedRow.EndEdit();

            this.Get_Total();
        }
        #endregion

        #region viewIVPurchaseOrderItems_ValidatingEditor
        private void viewIVPurchaseOrderItems_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            switch (viewIVPurchaseOrderItems.FocusedColumn.FieldName.ToLower())
            {
                case "receivedqty":
                    {
                        if (Program.IsMoney(e.Value.ToString()) == false)
                        {
                            e.ErrorText = Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_EntryIsInvalid.ToString());
                            e.Valid = false;
                            return;
                        }

                        DataRow mSelectedRow = viewIVPurchaseOrderItems.GetDataRow(viewIVPurchaseOrderItems.FocusedRowHandle);

                        #region compute amount

                        mSelectedRow.BeginEdit();

                        double mReceivedQty = Convert.ToDouble(e.Value);
                        double mTransPrice = Convert.ToDouble(mSelectedRow["transprice"]);
                        mSelectedRow["receivedqty"] = e.Value;
                        mSelectedRow["amount"] = mTransPrice * mReceivedQty;

                        if (mSelectedRow["itemtransferid"].ToString().Trim() == "")
                        {
                            mSelectedRow["transferedqty"] = e.Value;
                        }

                        mSelectedRow.EndEdit();

                        #endregion

                        this.Get_Total();
                    }
                    break;
                case "transprice":
                    {
                        if (Program.IsMoney(e.Value.ToString()) == false)
                        {
                            e.ErrorText = Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_EntryIsInvalid.ToString());
                            e.Valid = false;
                            return;
                        }

                        DataRow mSelectedRow = viewIVPurchaseOrderItems.GetDataRow(viewIVPurchaseOrderItems.FocusedRowHandle);

                        #region compute amount

                        mSelectedRow.BeginEdit();

                        double mReceivedQty = Convert.ToDouble(mSelectedRow["receivedqty"]);
                        double mTransPrice = Convert.ToDouble(e.Value);
                        mSelectedRow["amount"] = mTransPrice * mReceivedQty;

                        mSelectedRow.EndEdit();

                        #endregion

                        this.Get_Total();
                    }
                    break;
            }
        }
        #endregion

        #region viewIVPurchaseOrderItems_CustomRowCellEdit
        private void viewIVPurchaseOrderItems_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName.Trim().ToLower() == "receivedqty")
            {
                GridView mGridView = sender as GridView;
                bool mHasExpiry = Convert.ToBoolean(mGridView.GetRowCellValue(e.RowHandle, "hasexpiry"));
                if (mHasExpiry == true)
                {
                    e.RepositoryItem = expiryeditor;
                }
                else
                {
                    e.RepositoryItem = quantityeditor;
                }
            }
        }
        #endregion

        #region Get_Total
        private void Get_Total()
        {
            string mFunctionName = "Get_Total";

            try
            {
                double mTotalReceived = 0;
                double mTotalToDate = 0;
                foreach (DataRow mDataRow in pDtItems.Rows)
                {
                    mTotalReceived = mTotalReceived + Convert.ToDouble(mDataRow["amount"]);
                    mTotalToDate = mTotalToDate + (Convert.ToDouble(mDataRow["receivedtodate"]) * Convert.ToDouble(mDataRow["transprice"]));
                }

                txtTotalReceived.Text = mTotalReceived.ToString("c");
                txtTotalToDate.Text = mTotalToDate.ToString("c");
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmIVTransferInsReceive_Load
        private void frmIVTransferInsReceive_Load(object sender, EventArgs e)
        {
            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            tabbedControlGroup1.SelectedTabPage = grpHeader;

            switch (gDataState.Trim().ToLower())
            {
                case "edit": Mode_Edit(); break;
            }

            this.Load_Controls();

            Program.Restore_GridLayout(grdIVTransferInReceiveItems, grdIVTransferInReceiveItems.Name);

            for (int mIndex = 0; mIndex < viewIVPurchaseOrderItems.Columns.Count; mIndex++)
            {
                switch (viewIVPurchaseOrderItems.Columns[mIndex].ColumnType.ToString().ToLower())
                {
                    case "system.double":
                        {
                            viewIVPurchaseOrderItems.Columns[mIndex].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            viewIVPurchaseOrderItems.Columns[mIndex].DisplayFormat.FormatString = "{0:c}";
                        }
                        break;
                }
            }

            this.pFormWidth = this.Width;
            this.pFormHeight = this.Height;

            Program.Center_Screen(this);

            tabbedControlGroup1.SelectedTabPage = grpContents;
        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            DataRow mNewRow;
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region Packagings

                pDtPackagings.Rows.Clear();
                DataTable mDtPackagings = pMdtSomPackagings.View("", "code", Program.gLanguageName, "grdIVSPackagings");
                foreach (DataRow mDataRow in mDtPackagings.Rows)
                {
                    mNewRow = pDtPackagings.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    mNewRow["pieces"] = Convert.ToInt32(mDataRow["pieces"]);
                    pDtPackagings.Rows.Add(mNewRow);
                    pDtPackagings.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtPackagings.Columns)
                {
                    mDataColumn.Caption = mDtPackagings.Columns[mDataColumn.ColumnName].Caption;
                }

                #endregion

                #region Currencies

                pDtCurrencies.Rows.Clear();
                DataTable mDtCurrencies = pMdtCurrencies.View("", "code", Program.gLanguageName, "grdBLSCurrencies");

                foreach (DataRow mDataRow in mDtCurrencies.Rows)
                {
                    mNewRow = pDtCurrencies.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    mNewRow["exchangerate"] = Convert.ToDouble(mDataRow["exchangerate"]);
                    mNewRow["currencysymbol"] = mDataRow["currencysymbol"].ToString();
                    pDtCurrencies.Rows.Add(mNewRow);
                    pDtCurrencies.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtCurrencies.Columns)
                {
                    mDataColumn.Caption = mDtCurrencies.Columns[mDataColumn.ColumnName].Caption;
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

        #region frmIVTransferInsReceive_FormClosing
        private void frmIVTransferInsReceive_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //layout
                if (layoutControl1.IsModified == true)
                {
                    Program.Save_FormLayout(this, layoutControl1, this.Name);
                }

                //grid
                Program.Save_GridLayout(grdIVTransferInReceiveItems, grdIVTransferInReceiveItems.Name);
            }
            catch { }
        }
        #endregion

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(grpHeader);
            mObjectsList.Add(grpContents);
            mObjectsList.Add(txbTransferNo);
            mObjectsList.Add(txbDate);
            mObjectsList.Add(radTransferStatus.Properties.Items[0]);
            mObjectsList.Add(radTransferStatus.Properties.Items[1]);
            mObjectsList.Add(radTransferStatus.Properties.Items[2]);
            mObjectsList.Add(txbTitle);
            mObjectsList.Add(grpFrom);
            mObjectsList.Add(cmdSearchFrom);
            mObjectsList.Add(grpTo);
            mObjectsList.Add(txbCurrency);
            mObjectsList.Add(txbExchangeRate);
            mObjectsList.Add(txbRemarks);
            mObjectsList.Add(txbTotalReceived);
            mObjectsList.Add(txbTotalToDate);
            mObjectsList.Add(cmdItemQuickScan);
            mObjectsList.Add(cmdOk);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region Mode_Edit
        private void Mode_Edit()
        {
            string mFunctionName = "Mode_Edit";

            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)((frmIVTransferInsView)Program.gMdiForm.ActiveMdiChild).grdIVTransferInsView.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                this.Data_Clear();
                pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                txtTransferNo.Text = pSelectedRow["transferno"].ToString();
                txtDate.EditValue = Convert.ToDateTime(pSelectedRow["transdate"]);
                txtTitle.Text = pSelectedRow["transfertitle"].ToString();
                txtToCode.Text = pSelectedRow["tocode"].ToString();
                txtToDescription.Text = pSelectedRow["todescription"].ToString();
                txtFromCode.Text = pSelectedRow["fromcode"].ToString();
                txtFromDescription.Text = pSelectedRow["fromdescription"].ToString();
                cboCurrency.ItemIndex = Program.Get_LookupItemIndex(cboCurrency, "code", pSelectedRow["currencycode"].ToString());
                txtExchangeRate.Text = pSelectedRow["exchangerate"].ToString();
                txtRemarks.Text = pSelectedRow["remarks"].ToString();

                this.Fill_Contents();

                gDataState = "Edit";
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Data_Clear
        private void Data_Clear()
        {
            radTransferStatus.SelectedIndex = 0;
            txtTitle.Text = "";
            txtFromCode.Text = "";
            txtFromDescription.Text = "";
            txtRemarks.Text = "";
            pDtItems.Rows.Clear();

            cboCurrency.ItemIndex = Program.Get_LookupItemIndex(cboCurrency, "code", Program.gLocalCurrencyCode);
        }
        #endregion

        #region Fill_Contents
        private void Fill_Contents()
        {
            string mFunctionName = "Fill_Contents";

            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)((frmIVTransferInsView)Program.gMdiForm.ActiveMdiChild).grdIVTransferInsView.MainView;
                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }
                pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                string mTransferType = pSelectedRow["transfertype"].ToString().Trim();
                string mTransferOutNo = pSelectedRow["transferoutno"].ToString().Trim();

                DataTable mDtItems = pMdtSomTransferIns.View_TransferInItems(
                    "transferno='" + txtTransferNo.Text.Trim() + "'", "autocode",
                    Program.gLanguageName, "grdIVTransferInItems");

                DataTable mDtIssuedToDate = null;
                DataView mDvIssuedToDate = null;
                DataTable mDtReceivedToDate = null;
                DataView mDvReceivedToDate = null;

                #region issued todate for this transfer
                if (mTransferType.Trim().ToLower() == AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString().Trim().ToLower())
                {
                    if (mTransferOutNo.Trim() != "")
                    {
                        mDtIssuedToDate = pMdtSomTransferOuts.Get_IssuedToDate(
                            "transferno='" + mTransferOutNo + "'", "itemtransferid");

                        mDvIssuedToDate = new DataView();
                        mDvIssuedToDate.Table = mDtIssuedToDate;
                        mDvIssuedToDate.Sort = "itemtransferid";

                        DataTable mDtIssuedToDateExpiryDates = pMdtSomTransferOuts.Get_IssuedToDateByExpiryDates(
                            "transferno='" + mTransferOutNo + "'", "itemtransferid");

                        pDtExpiryDates.Rows.Clear();
                        foreach (DataRow mDataRow in mDtIssuedToDateExpiryDates.Rows)
                        {
                            DataRow mNewRow = pDtExpiryDates.NewRow();
                            mNewRow["itemtransferid"] = mDataRow["itemtransferid"];
                            mNewRow["productcode"] = mDataRow["productcode"];
                            mNewRow["expirydate"] = mDataRow["expirydate"];
                            mNewRow["quantity"] = Convert.ToDouble(mDataRow["issuedqty"]) / Convert.ToInt32(mDataRow["piecesinpackage"]);
                            pDtExpiryDates.Rows.Add(mNewRow);
                            pDtExpiryDates.AcceptChanges();
                        }
                    }
                }
                #endregion

                #region received todate for this transfer
                if (mTransferType.Trim().ToLower() == AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString().Trim().ToLower())
                {
                    if (mTransferOutNo.Trim() != "")
                    {
                        mDtReceivedToDate = pMdtSomTransferIns.Get_ReceivedToDate(
                            "transferno='" + txtTransferNo.Text.Trim() + "'", "itemtransferid");

                        mDvReceivedToDate = new DataView();
                        mDvReceivedToDate.Table = mDtReceivedToDate;
                        mDvReceivedToDate.Sort = "itemtransferid";
                    }
                }
                if (mTransferType.Trim().ToLower() == AfyaPro_Types.clsEnums.SomTransferTypes.Normal.ToString().Trim().ToLower())
                {
                    mDtReceivedToDate = pMdtSomTransferIns.Get_ReceivedToDate(
                        "transferno='" + txtTransferNo.Text.Trim() + "'", "itemtransferid");

                    mDvReceivedToDate = new DataView();
                    mDvReceivedToDate.Table = mDtReceivedToDate;
                    mDvReceivedToDate.Sort = "itemtransferid";
                }
                #endregion

                DataTable mDtProducts = pMdtSomProducts.View("", "", "", "");
                DataView mDvProducts = new DataView();
                mDvProducts.Table = mDtProducts;
                mDvProducts.Sort = "code";

                pDtItems.Rows.Clear();

                foreach (DataRow mDataRow in mDtItems.Rows)
                {
                    int mRowIndex = mDvProducts.Find(mDataRow["productcode"].ToString().Trim());

                    bool mHasExpiry = false;
                    double mReceivedToDate = 0;
                    double mIssuedToDate = 0;
                    double mToReceiveQty = 0;

                    if (mRowIndex >= 0)
                    {
                        if (Convert.ToInt16(mDvProducts[mRowIndex]["hasexpiry"]) == 1)
                        {
                            mHasExpiry = true;
                        }
                    }

                    if (mDvIssuedToDate != null)
                    {
                        mRowIndex = mDvIssuedToDate.Find(mDataRow["itemtransferid"].ToString().Trim());
                        if (mRowIndex >= 0)
                        {
                            mIssuedToDate = Convert.ToDouble(mDvIssuedToDate[mRowIndex]["issuedqty"]);
                        }
                    }

                    if (mDvReceivedToDate != null)
                    {
                        mRowIndex = mDvReceivedToDate.Find(mDataRow["itemtransferid"].ToString().Trim());
                        if (mRowIndex >= 0)
                        {
                            mReceivedToDate = Convert.ToDouble(mDvReceivedToDate[mRowIndex]["receivedqty"]);
                        }
                    }

                    if (mTransferType.Trim().ToLower() == AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString().Trim().ToLower())
                    {
                        if (mReceivedToDate < mIssuedToDate)
                        {
                            mToReceiveQty = mIssuedToDate - mReceivedToDate;
                        }
                    }
                    else
                    {
                        mToReceiveQty = 0;
                    }

                    DataRow mNewRow = pDtItems.NewRow();
                    mNewRow["itemtransferid"] = mDataRow["itemtransferid"].ToString().Trim();
                    mNewRow["productcode"] = mDataRow["productcode"].ToString().Trim();
                    mNewRow["productdescription"] = mDataRow["productdescription"].ToString().Trim();
                    mNewRow["productopmcode"] = mDataRow["productopmcode"].ToString().Trim();
                    mNewRow["productopmdescription"] = mDataRow["productopmdescription"].ToString().Trim();
                    mNewRow["productdepartmentcode"] = mDataRow["productdepartmentcode"].ToString().Trim();
                    mNewRow["productdepartmentdescription"] = mDataRow["productdepartmentdescription"].ToString().Trim();
                    mNewRow["packagingcode"] = mDataRow["packagingcode"].ToString().Trim();
                    mNewRow["packagingdescription"] = mDataRow["packagingdescription"].ToString().Trim();
                    mNewRow["piecesinpackage"] = Convert.ToInt32(mDataRow["piecesinpackage"]);
                    mNewRow["onhandqty"] = Convert.ToDouble(mDataRow["onhandqty"]);
                    mNewRow["transferedqty"] = Convert.ToDouble(mDataRow["transferedqty"]);
                    mNewRow["receivedtodate"] = mReceivedToDate / Convert.ToInt32(mDataRow["piecesinpackage"]);
                    mNewRow["receivedqty"] = mToReceiveQty / Convert.ToInt32(mDataRow["piecesinpackage"]);
                    mNewRow["transprice"] = Convert.ToDouble(mDataRow["transprice"]);
                    mNewRow["amount"] = Convert.ToDouble(mNewRow["receivedqty"]) * Convert.ToDouble(mNewRow["transprice"]);
                    mNewRow["hasexpiry"] = mHasExpiry;
                    pDtItems.Rows.Add(mNewRow);
                    pDtItems.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtItems.Columns)
                {
                    if (mDataColumn.ColumnName.ToLower() != "hasexpiry")
                    {
                        mDataColumn.Caption = mDtItems.Columns[mDataColumn.ColumnName].Caption;
                    }
                }

                this.Get_Total();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Get_ReceivedToDate
        private void Get_ReceivedToDate()
        {
            string mFunctionName = "Get_ReceivedQuantities";

            try
            {
                DataTable mDtReceived = pMdtSomTransferIns.Get_ReceivedToDate(
                    "transferno='" + txtTransferNo.Text.Trim() + "'", "");

                DataView mDvReceived = new DataView();
                mDvReceived.Table = mDtReceived;
                mDvReceived.Sort = "itemtransferid";

                foreach (DataRow mDataRow in pDtItems.Rows)
                {
                    int mRowIndex = mDvReceived.Find(mDataRow["itemtransferid"].ToString().Trim());
                    double mReceivedToDate = 0;
                    int mPiecesInPackage = Convert.ToInt32(mDataRow["piecesinpackage"]);
                    if (mRowIndex >= 0)
                    {
                        mReceivedToDate = Convert.ToDouble(mDvReceived[mRowIndex]["receivedqty"]) / mPiecesInPackage;
                    }

                    mDataRow.BeginEdit();
                    mDataRow["receivedtodate"] = mReceivedToDate;
                    mDataRow.EndEdit();
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Add_Item
        private void Add_Item(DataRow mDataRow)
        {
            if (Program.IsMoney(txtExchangeRate.Text) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_ExchangeRateIsInvalid.ToString());
                return;
            }

            DialogResult mDialogResult = Program.Display_Question(
                AfyaPro_Types.clsSystemMessages.MessageIds.IV_OrderConfirmAddItemToOrder.ToString(), MessageBoxDefaultButton.Button1);
            if (mDialogResult != DialogResult.Yes)
            {
                return;
            }

            double mLocalPrice = Convert.ToDouble(mDataRow["costprice"]) * Convert.ToInt32(mDataRow["piecesinpackage"]);
            double mExchangeRate = Convert.ToDouble(txtExchangeRate.Text);
            double mForeignPrice = mLocalPrice / mExchangeRate;
            double mOnHand = 0;

            DataTable mDtOnHand = pMdtSomStores.Get_OnHandQuantities("productcode='" + mDataRow["code"].ToString().Trim() + "'", "");
            if (mDtOnHand.Rows.Count > 0)
            {
                mOnHand = Convert.ToDouble(mDtOnHand.Rows[0]["onhandqty"]) / Convert.ToInt32(mDataRow["piecesinpackage"]);
            }

            DataRow mNewRow = pDtItems.NewRow();
            mNewRow["itemtransferid"] = "";
            mNewRow["productcode"] = mDataRow["code"].ToString().Trim();
            mNewRow["productdescription"] = mDataRow["description"].ToString().Trim();
            mNewRow["productopmcode"] = mDataRow["opmcode"].ToString().Trim();
            mNewRow["productopmdescription"] = mDataRow["opmdescription"].ToString().Trim();
            mNewRow["productdepartmentcode"] = mDataRow["departmentcode"].ToString().Trim();
            mNewRow["productdepartmentdescription"] = mDataRow["departmentdescription"].ToString().Trim();
            mNewRow["packagingcode"] = mDataRow["packagingcode"].ToString().Trim();
            mNewRow["packagingdescription"] = mDataRow["packagingdescription"].ToString().Trim();
            mNewRow["piecesinpackage"] = Convert.ToInt32(mDataRow["piecesinpackage"]);
            mNewRow["onhandqty"] = mOnHand;
            mNewRow["transferedqty"] = 1;
            mNewRow["receivedtodate"] = 0;
            mNewRow["receivedqty"] = 1;
            mNewRow["transprice"] = mForeignPrice;
            mNewRow["amount"] = mForeignPrice * Convert.ToDouble(mNewRow["transferedqty"]);
            mNewRow["hasexpiry"] = Convert.ToBoolean(mDataRow["hasexpiry"]);
            pDtItems.Rows.Add(mNewRow);
            pDtItems.AcceptChanges();

            this.Get_Total();
        }
        #endregion

        #region cmdItemNew_Click
        private void cmdItemNew_Click(object sender, EventArgs e)
        {
            frmSearchProduct mSearchProduct = new frmSearchProduct();
            mSearchProduct.ShowDialog();
            if (mSearchProduct.SearchingDone == true)
            {
                pSelectedRow = mSearchProduct.SelectedRow;
                if (pSelectedRow != null)
                {
                    this.Add_Item(pSelectedRow);
                }
            }
        }
        #endregion

        #region cmdItemQuickScan_Click
        private void cmdItemQuickScan_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region cboCurrency_EditValueChanged
        private void cboCurrency_EditValueChanged(object sender, EventArgs e)
        {
            string mFunctionName = "cboCurrency_EditValueChanged";

            try
            {
                if (cboCurrency.ItemIndex == -1)
                {
                    return;
                }

                txtExchangeRate.Text = cboCurrency.GetColumnValue("exchangerate").ToString();

                if (cboCurrency.GetColumnValue("code").ToString().Trim().ToLower() ==
                    Program.gLocalCurrencyCode.Trim().ToLower())
                {
                    txtExchangeRate.Enabled = false;
                }
                else
                {
                    txtExchangeRate.Enabled = true;
                    txtExchangeRate.Focus();
                }
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
            string mFunctionName = "cmdOk_Click";

            #region validation

            if (txtDeliveryNo.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IV_DeliveryNoIsInvalid.ToString());
                txtDeliveryNo.Focus();
                return;
            }

            if (txtTransferNo.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IV_TransferNoIsInvalid.ToString());
                txtTransferNo.Focus();
                return;
            }

            if (Program.IsDate(txtDeliveryDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                txtDeliveryDate.Focus();
                return;
            }

            if (cboCurrency.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_CurrencyIsInvalid.ToString());
                return;
            }

            if (Program.IsMoney(txtExchangeRate.Text) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_ExchangeRateIsInvalid.ToString());
                return;
            }

            #endregion

            try
            {
                DateTime mTransDate = Convert.ToDateTime(txtDeliveryDate.EditValue);

                viewIVPurchaseOrderItems.PostEditor();

                DataTable mDtItems = new DataTable("items");
                mDtItems.Columns.Add("itemtransferid", typeof(System.String));
                mDtItems.Columns.Add("productcode", typeof(System.String));
                mDtItems.Columns.Add("packagingcode", typeof(System.String));
                mDtItems.Columns.Add("packagingdescription", typeof(System.String));
                mDtItems.Columns.Add("piecesinpackage", typeof(System.Int32));
                mDtItems.Columns.Add("receivedqty", typeof(System.Double));
                mDtItems.Columns.Add("expirydate", typeof(System.DateTime));
                mDtItems.Columns.Add("transprice", typeof(System.Double));

                DataView mDvExpiryDates = new DataView();
                mDvExpiryDates.Table = pDtExpiryDates;

                foreach (DataRow mDataRow in pDtItems.Rows)
                {
                    mDvExpiryDates.RowFilter = "itemtransferid='" + mDataRow["itemtransferid"].ToString().Trim() + "'";

                    if (mDvExpiryDates.Count > 0)
                    {
                        foreach (DataRowView mDataRowView in mDvExpiryDates)
                        {
                            DateTime mExpiryDate = new DateTime();
                            double mReceivedQty = Convert.ToDouble(mDataRowView["quantity"]); ;

                            if (Program.IsNullDate(mDataRowView["expirydate"]) == false)
                            {
                                mExpiryDate = Convert.ToDateTime(mDataRowView["expirydate"]);
                            }

                            DataRow mNewRow = mDtItems.NewRow();
                            mNewRow["itemtransferid"] = mDataRow["itemtransferid"].ToString().Trim();
                            mNewRow["productcode"] = mDataRow["productcode"].ToString().Trim();
                            mNewRow["packagingcode"] = mDataRow["packagingcode"].ToString().Trim();
                            mNewRow["packagingdescription"] = mDataRow["packagingdescription"].ToString().Trim();
                            mNewRow["piecesinpackage"] = Convert.ToInt32(mDataRow["piecesinpackage"]);
                            mNewRow["receivedqty"] = mReceivedQty;
                            mNewRow["expirydate"] = mExpiryDate;
                            mNewRow["transprice"] = Convert.ToDouble(mDataRow["transprice"]);
                            mDtItems.Rows.Add(mNewRow);
                            mDtItems.AcceptChanges();
                        }
                    }
                    else
                    {
                        DataRow mNewRow = mDtItems.NewRow();
                        mNewRow["itemtransferid"] = mDataRow["itemtransferid"].ToString().Trim();
                        mNewRow["productcode"] = mDataRow["productcode"].ToString().Trim();
                        mNewRow["packagingcode"] = mDataRow["packagingcode"].ToString().Trim();
                        mNewRow["packagingdescription"] = mDataRow["packagingdescription"].ToString().Trim();
                        mNewRow["piecesinpackage"] = Convert.ToInt32(mDataRow["piecesinpackage"]);
                        mNewRow["receivedqty"] = Convert.ToDouble(mDataRow["receivedqty"]);
                        mNewRow["expirydate"] = new DateTime();
                        mNewRow["transprice"] = Convert.ToDouble(mDataRow["transprice"]);
                        mDtItems.Rows.Add(mNewRow);
                        mDtItems.AcceptChanges();
                    }
                }

                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)((frmIVTransferInsView)Program.gMdiForm.ActiveMdiChild).grdIVTransferInsView.MainView;
                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }
                pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                string mTransferType = pSelectedRow["transfertype"].ToString().Trim();
                string mTransferOutNo = pSelectedRow["transferoutno"].ToString().Trim();

                //receive
                pResult = pMdtSomTransferIns.Receive_Items(mTransDate, txtDeliveryNo.Text, txtTransferNo.Text, mTransferOutNo, 
                    txtTitle.Text, txtToCode.Text, txtToDescription.Text, txtFromCode.Text, txtFromDescription.Text, 
                    mTransferType, cboCurrency.GetColumnValue("code").ToString(), 
                    cboCurrency.GetColumnValue("description").ToString(), Convert.ToDouble(txtExchangeRate.Text),
                    cboCurrency.GetColumnValue("currencysymbol").ToString(), txtRemarks.Text, mDtItems, Program.gCurrentUser.Code);
                if (pResult.Exe_Result == 0)
                {
                    Program.Display_Error(pResult.Exe_Message);
                    return;
                }
                if (pResult.Exe_Result == -1)
                {
                    Program.Display_Server_Error(pResult.Exe_Message);
                    return;
                }

                this.Fill_Contents();
                this.Get_Total();

                #region check if all items received

                bool mAllReceived = true;
                foreach (DataRow mDataRow in pDtItems.Rows)
                {
                    double mToReceiveQty = Convert.ToDouble(mDataRow["transferedqty"]) - Convert.ToDouble(mDataRow["receivedtodate"]);
                    if (mToReceiveQty > 0)
                    {
                        mAllReceived = false;
                    }
                }

                if (mAllReceived == true)
                {
                    DialogResult mDialogResult = Program.Display_Question(
                        AfyaPro_Types.clsSystemMessages.MessageIds.IV_CloseTransferConfirmation.ToString(), MessageBoxDefaultButton.Button1);

                    if (mDialogResult != DialogResult.Yes)
                    {
                        return;
                    }

                    //close
                    pResult = pMdtSomTransferIns.Close(mTransDate, txtTransferNo.Text, mTransferOutNo, Program.gCurrentUser.Code);
                    if (pResult.Exe_Result == 0)
                    {
                        Program.Display_Error(pResult.Exe_Message);
                        return;
                    }
                    if (pResult.Exe_Result == -1)
                    {
                        Program.Display_Server_Error(pResult.Exe_Message);
                        return;
                    }
                }

                #endregion

                //refresh
                try
                {
                    frmIVTransferInsView mIVTransferInsView = (frmIVTransferInsView)Program.gMdiForm.ActiveMdiChild;
                    mIVTransferInsView.Data_Fill();
                    this.Close();
                }
                catch { }
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

        #region cmdReceiveAll_Click
        private void cmdReceiveAll_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdReceiveAll_Click";

            try
            {
                DataView mDvExpiryDates = new DataView();
                mDvExpiryDates.Table = pDtExpiryDates;

                foreach (DataRow mDataRow in pDtItems.Rows)
                {
                    double mToReceiveQty = Convert.ToDouble(mDataRow["transferedqty"]) - Convert.ToDouble(mDataRow["receivedtodate"]);

                    if (mToReceiveQty != 0)
                    {
                        if (Convert.ToBoolean(mDataRow["hasexpiry"]) == false)
                        {
                            mDataRow.BeginEdit();
                            mDataRow["receivedqty"] = mToReceiveQty;
                            mDataRow["amount"] = Convert.ToDouble(mDataRow["transprice"]) * mToReceiveQty;
                            mDataRow.EndEdit();
                        }
                    }
                }

                pDtItems.AcceptChanges();

                this.Get_Total();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region expiryeditor_ButtonClick
        private void expiryeditor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow mSelectedRow = viewIVPurchaseOrderItems.GetDataRow(viewIVPurchaseOrderItems.FocusedRowHandle);

            #region prompt for expiry dates

            if (Convert.ToBoolean(mSelectedRow["hasexpiry"]) == true)
            {
                frmIVExpiryDates mIVExpiryDates = new frmIVExpiryDates();
                mIVExpiryDates.LookupField = "itemtransferid";
                mIVExpiryDates.LookupValue = mSelectedRow["itemtransferid"].ToString().Trim();
                mIVExpiryDates.Quantity = Convert.ToDouble(mSelectedRow["receivedqty"]);
                mIVExpiryDates.DtExpiryDates = pDtExpiryDates;
                mIVExpiryDates.txtItemCode.Text = mSelectedRow["productcode"].ToString().Trim();
                mIVExpiryDates.txtItemDescription.Text = mSelectedRow["productdescription"].ToString().Trim();
                mIVExpiryDates.ShowDialog();

                mSelectedRow.BeginEdit();

                ButtonEdit mButtonEdit = sender as ButtonEdit;

                mButtonEdit.EditValue = mIVExpiryDates.Quantity;
                mSelectedRow["receivedqty"] = mIVExpiryDates.Quantity;
                mSelectedRow["amount"] = Convert.ToDouble(mSelectedRow["transprice"]) * mIVExpiryDates.Quantity;

                mSelectedRow.EndEdit();

                this.Get_Total();
            }

            #endregion
        }
        #endregion
    }
}