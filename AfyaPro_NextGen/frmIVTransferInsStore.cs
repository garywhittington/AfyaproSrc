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
    public partial class frmIVTransferInsStore : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsSomTransferIns pMdtSomTransferIns;
        private AfyaPro_MT.clsSomPackagings pMdtSomPackagings;
        private AfyaPro_MT.clsCurrencies pMdtCurrencies;
        private AfyaPro_MT.clsSomProducts pMdtSomProducts;
        private AfyaPro_MT.clsSomStores pMdtSomStores;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataTable pDtItems = new DataTable("items");
        private DataRow pSelectedRow = null;
        internal string gDataState = "";

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private DataTable pDtPackagings = new DataTable("packagings");
        private DataTable pDtCurrencies = new DataTable("currencies");

        private string pCurrFromCode = "";
        private string pPrevFromCode = "";
        private bool pSearchingFrom = false;

        private string pCurrToCode = "";
        private string pPrevToCode = "";
        private bool pSearchingTo = false;

        private string pTransferOutNo = "";
        internal string TransferOutNo
        {
            set { pTransferOutNo = value; }
            get { return pTransferOutNo; }
        }

        private string pStoreCode = "";

        #endregion

        #region frmIVTransferInsStore
        public frmIVTransferInsStore(string mStoreCode)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;

            string mFunctionName = "frmIVTransferInsStore";

            try
            {
                this.pStoreCode = mStoreCode;

                this.Icon = Program.gMdiForm.Icon;
                this.CancelButton = cmdClose;

                pMdtSomTransferIns = (AfyaPro_MT.clsSomTransferIns)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomTransferIns),
                    Program.gMiddleTier + "clsSomTransferIns");

                pMdtSomPackagings = (AfyaPro_MT.clsSomPackagings)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomPackagings),
                    Program.gMiddleTier + "clsSomPackagings");

                pMdtCurrencies = (AfyaPro_MT.clsCurrencies)Activator.GetObject(
                    typeof(AfyaPro_MT.clsCurrencies),
                    Program.gMiddleTier + "clsCurrencies");

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

                grdIVTransferInItems.DataSource = pDtItems;

                foreach (DevExpress.XtraGrid.Columns.GridColumn mGridColumn in viewIVTransferInItems.Columns)
                {
                    if (mGridColumn.FieldName.ToLower() != "packagingdescription"
                        && mGridColumn.FieldName.ToLower() != "transferedqty"
                        && mGridColumn.FieldName.ToLower() != "transprice")
                    {
                        mGridColumn.OptionsColumn.AllowEdit = false;
                    }

                    if (mGridColumn.FieldName.ToLower() == "autocode"
                        || mGridColumn.FieldName.ToLower() == "receivedtodate")
                    {
                        mGridColumn.OptionsColumn.AllowEdit = false;
                        mGridColumn.Visible = false;
                    }
                }

                viewIVTransferInItems.Columns["packagingdescription"].ColumnEdit = packagingeditor;
                viewIVTransferInItems.Columns["transferedqty"].ColumnEdit = transferedqtyeditor;
                viewIVTransferInItems.Columns["transprice"].ColumnEdit = transpriceeditor;

                txtDate.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                txtDate.EditValue = Program.gMdiForm.txtDate.EditValue;
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

            if (viewIVTransferInItems.FocusedRowHandle < 0)
            {
                return;
            }

            DataRow mSelectedRow = viewIVTransferInItems.GetDataRow(viewIVTransferInItems.FocusedRowHandle);

            //get previous cost and onhandqty
            double mUnitPrice = Convert.ToDouble(mSelectedRow["transprice"]) / Convert.ToDouble(mSelectedRow["piecesinpackage"]);
            double mOnHandQty = Convert.ToDouble(mSelectedRow["onhandqty"]) * Convert.ToDouble(mSelectedRow["piecesinpackage"]);
            double mReceivedToDate = Convert.ToDouble(mSelectedRow["receivedtodate"]) * Convert.ToDouble(mSelectedRow["piecesinpackage"]);

            double mOrderedQty = Convert.ToDouble(mSelectedRow["transferedqty"]);
            double mTransPrice = mUnitPrice * Convert.ToInt32(mLookupEdit.GetColumnValue("pieces"));

            mSelectedRow.BeginEdit();

            mSelectedRow["packagingcode"] = mLookupEdit.GetColumnValue("code").ToString();
            mSelectedRow["packagingdescription"] = mLookupEdit.GetColumnValue("description").ToString();
            mSelectedRow["piecesinpackage"] = mLookupEdit.GetColumnValue("pieces").ToString();
            mSelectedRow["onhandqty"] = mOnHandQty / Convert.ToInt32(mLookupEdit.GetColumnValue("pieces"));
            mSelectedRow["receivedtodate"] = mReceivedToDate / Convert.ToInt32(mLookupEdit.GetColumnValue("pieces"));
            mSelectedRow["transprice"] = mUnitPrice * Convert.ToInt32(mLookupEdit.GetColumnValue("pieces"));
            mSelectedRow["amount"] = mTransPrice * mOrderedQty;

            mSelectedRow.EndEdit();

            this.Get_Total();
        }
        #endregion

        #region viewIVTransferInItems_ValidatingEditor
        private void viewIVTransferInItems_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            switch (viewIVTransferInItems.FocusedColumn.FieldName.ToLower())
            {
                case "transferedqty":
                    {
                        if (Program.IsMoney(e.Value.ToString()) == false)
                        {
                            e.ErrorText = Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_EntryIsInvalid.ToString());
                            e.Valid = false;
                            return;
                        }

                        DataRow mSelectedRow = viewIVTransferInItems.GetDataRow(viewIVTransferInItems.FocusedRowHandle);

                        #region compute amount

                        mSelectedRow.BeginEdit();

                        double mOrderedQty = Convert.ToDouble(e.Value);
                        double mTransPrice = Convert.ToDouble(mSelectedRow["transprice"]);
                        mSelectedRow["amount"] = mTransPrice * mOrderedQty;

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

                        DataRow mSelectedRow = viewIVTransferInItems.GetDataRow(viewIVTransferInItems.FocusedRowHandle);

                        #region compute amount

                        mSelectedRow.BeginEdit();

                        double mReceivedQty = Convert.ToDouble(mSelectedRow["transferedqty"]);
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

        #region Get_Total
        private void Get_Total()
        {
            string mFunctionName = "Get_Total";

            try
            {
                double mTotal = 0;
                foreach (DataRow mDataRow in pDtItems.Rows)
                {
                    mTotal = mTotal + Convert.ToDouble(mDataRow["amount"]);
                }

                txtTotal.Text = mTotal.ToString("c");
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmIVTransferInsStore_Load
        private void frmIVTransferInsStore_Load(object sender, EventArgs e)
        {
            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            tabbedControlGroup1.SelectedTabPage = grpHeader;

            switch (gDataState.Trim().ToLower())
            {
                case "new": Mode_New(); break;
                case "edit": Mode_Edit(); break;
            }

            this.Load_Controls();

            Program.Restore_GridLayout(grdIVTransferInItems, grdIVTransferInItems.Name);

            for (int mIndex = 0; mIndex < viewIVTransferInItems.Columns.Count; mIndex++)
            {
                switch (viewIVTransferInItems.Columns[mIndex].ColumnType.ToString().ToLower())
                {
                    case "system.double":
                        {
                            viewIVTransferInItems.Columns[mIndex].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            viewIVTransferInItems.Columns[mIndex].DisplayFormat.FormatString = "{0:c}";
                        }
                        break;
                }
            }

            this.pFormWidth = this.Width;
            this.pFormHeight = this.Height;

            Program.Center_Screen(this);
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

        #region frmIVTransferInsStore_FormClosing
        private void frmIVTransferInsStore_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //layout
                if (layoutControl1.IsModified == true)
                {
                    Program.Save_FormLayout(this, layoutControl1, this.Name);
                }

                //grid
                Program.Save_GridLayout(grdIVTransferInItems, grdIVTransferInItems.Name);
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
            mObjectsList.Add(radOrderStatus.Properties.Items[0]);
            mObjectsList.Add(radOrderStatus.Properties.Items[1]);
            mObjectsList.Add(radOrderStatus.Properties.Items[2]);
            mObjectsList.Add(txbTitle);
            mObjectsList.Add(grpFrom);
            mObjectsList.Add(cmdSearchFrom);
            mObjectsList.Add(grpTo);
            mObjectsList.Add(txbCurrency);
            mObjectsList.Add(txbExchangeRate);
            mObjectsList.Add(txbRemarks);
            mObjectsList.Add(txbTotal);
            mObjectsList.Add(cmdItemQuickScan);
            mObjectsList.Add(cmdItemDelete);
            mObjectsList.Add(cmdOk);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region Mode_New
        private void Mode_New()
        {
            String mFunctionName = "Mode_New";

            try
            {
                Int16 mGenerateCode = pMdtAutoCodes.Auto_Generate_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.transferinno));
                if (mGenerateCode == -1)
                {
                    Program.Display_Server_Error("");
                    return;
                }

                txtTransferNo.Text = "";
                this.Data_Clear();

                if (mGenerateCode == 1)
                {
                    txtTransferNo.Text = "<<---New--->>";
                    txtTransferNo.Enabled = false;
                    txtTitle.Focus();
                }
                else
                {
                    txtTransferNo.Enabled = true;
                    txtTransferNo.Focus();
                }

                DataTable mDtItems = pMdtSomTransferIns.View_TransferInItems("1=2", "autocode",
                    Program.gLanguageName, grdIVTransferInItems.Name);

                foreach (DataColumn mDataColumn in pDtItems.Columns)
                {
                    mDataColumn.Caption = mDtItems.Columns[mDataColumn.ColumnName].Caption;
                }

                this.Get_Total();

                pSearchingTo = true;
                txtToCode.Text = pStoreCode;
                pSearchingTo = false;

                gDataState = "New";
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
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
                txtToDesciption.Text = pSelectedRow["todescription"].ToString();
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
            radOrderStatus.SelectedIndex = 0;
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
                DataTable mDtItems = pMdtSomTransferIns.View_TransferInItems(
                    "transferno='" + txtTransferNo.Text.Trim() + "'", "autocode",
                    Program.gLanguageName, grdIVTransferInItems.Name);

                pDtItems.Rows.Clear();

                foreach (DataRow mDataRow in mDtItems.Rows)
                {
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
                    mNewRow["receivedtodate"] = Convert.ToDouble(mDataRow["receivedtodate"]);
                    mNewRow["receivedqty"] = Convert.ToDouble(mDataRow["receivedqty"]);
                    mNewRow["transprice"] = Convert.ToDouble(mDataRow["transprice"]);
                    mNewRow["amount"] = Convert.ToDouble(mDataRow["transferedqty"]) * Convert.ToDouble(mDataRow["transprice"]);
                    pDtItems.Rows.Add(mNewRow);
                    pDtItems.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtItems.Columns)
                {
                    mDataColumn.Caption = mDtItems.Columns[mDataColumn.ColumnName].Caption;
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

        #region Add_Item
        internal void Add_Item(DataRow mDataRow, double mQuantity)
        {
            string mFunctionName = "Add_Item";

            try
            {
                if (Program.IsMoney(txtExchangeRate.Text) == false)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_ExchangeRateIsInvalid.ToString());
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
                mNewRow["onhandqty"] = 0;
                mNewRow["transferedqty"] = mQuantity;
                mNewRow["onhandqty"] = mOnHand;
                mNewRow["receivedqty"] = 0;
                mNewRow["transprice"] = mForeignPrice;
                mNewRow["amount"] = mForeignPrice * Convert.ToDouble(mNewRow["transferedqty"]);
                pDtItems.Rows.Add(mNewRow);
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
                    this.Add_Item(pSelectedRow, 1);
                }
            }
        }
        #endregion

        #region cmdItemQuickScan_Click
        private void cmdItemQuickScan_Click(object sender, EventArgs e)
        {
            frmIVQuickScan mIVQuickScan = new frmIVQuickScan(this, txtToCode.Text);
            mIVQuickScan.ShowDialog();
        }
        #endregion

        #region cmdItemDelete_Click
        private void cmdItemDelete_Click(object sender, EventArgs e)
        {
            viewIVTransferInItems.DeleteSelectedRows();
            pDtItems.AcceptChanges();

            this.Get_Total();
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

        #region lookup from

        #region Display_From
        private void Display_From()
        {
            string mFunctionName = "Display_From";

            try
            {
                DataTable mDtStores = pMdtSomStores.View("code='" + txtFromCode.Text.Trim() + "'", "", "", "");

                DataView mDvUserStores = new DataView();
                mDvUserStores.Table = Program.gDtUserStores;
                mDvUserStores.Sort = "storecode";

                if (mDtStores.Rows.Count > 0)
                {
                    if (mDvUserStores.Find(mDtStores.Rows[0]["code"].ToString().Trim()) >= 0)
                    {
                        txtFromCode.Text = mDtStores.Rows[0]["code"].ToString().Trim();
                        txtFromDescription.Text = mDtStores.Rows[0]["description"].ToString().Trim();
                    }

                    pCurrFromCode = txtFromCode.Text;
                    pPrevFromCode = pCurrFromCode;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdSearchFrom_Click
        private void cmdSearchFrom_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdSearchFrom_Click";

            try
            {
                pSearchingFrom = true;

                frmSearchStore mSearchStore = new frmSearchStore(txtFromCode);
                mSearchStore.ShowDialog();

                pSearchingFrom = false;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region txtFromCode_EditValueChanged
        private void txtFromCode_EditValueChanged(object sender, EventArgs e)
        {
            if (pSearchingFrom == true)
            {
                this.Display_From();
            }
        }
        #endregion

        #region txtFromCode_KeyDown
        private void txtFromCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Display_From();
            }
        }
        #endregion

        #region txtFromCode_Leave
        private void txtFromCode_Leave(object sender, EventArgs e)
        {
            pCurrFromCode = txtFromCode.Text;

            if (pCurrFromCode.Trim().ToLower() != pPrevFromCode.Trim().ToLower())
            {
                this.Display_From();
            }
        }
        #endregion

        #endregion

        #region lookup to

        #region Display_To
        private void Display_To()
        {
            string mFunctionName = "Display_To";

            try
            {
                DataTable mDtStores = pMdtSomStores.View("code='" + txtToCode.Text.Trim() + "'", "", "", "");

                DataView mDvUserStores = new DataView();
                mDvUserStores.Table = Program.gDtUserStores;
                mDvUserStores.Sort = "storecode";

                if (mDtStores.Rows.Count > 0)
                {
                    if (mDvUserStores.Find(mDtStores.Rows[0]["code"].ToString().Trim()) >= 0)
                    {
                        txtToCode.Text = mDtStores.Rows[0]["code"].ToString().Trim();
                        txtToDesciption.Text = mDtStores.Rows[0]["description"].ToString().Trim();
                    }

                    pCurrToCode = txtToCode.Text;
                    pPrevToCode = pCurrToCode;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdSearchTo_Click
        private void cmdSearchTo_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdSearchTo_Click";

            try
            {
                pSearchingTo = true;

                frmSearchStore mSearchStore = new frmSearchStore(txtToCode);
                mSearchStore.ShowDialog();

                pSearchingTo = false;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region txtToCode_EditValueChanged
        private void txtToCode_EditValueChanged(object sender, EventArgs e)
        {
            if (pSearchingTo == true)
            {
                this.Display_To();
            }
        }
        #endregion

        #region txtToCode_KeyDown
        private void txtToCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Display_To();
            }
        }
        #endregion

        #region txtToCode_Leave
        private void txtToCode_Leave(object sender, EventArgs e)
        {
            pCurrToCode = txtToCode.Text;

            if (pCurrToCode.Trim().ToLower() != pPrevToCode.Trim().ToLower())
            {
                this.Display_To();
            }
        }
        #endregion

        #endregion

        #region Data_New
        private void Data_New()
        {
            Int16 mGeneratePONumber = 0;
            string mFunctionName = "Data_New";

            #region validation

            if (txtTransferNo.Text.Trim() == "" && txtTransferNo.Text.Trim().ToLower() != "<<---new--->>")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IV_TransferNoIsInvalid.ToString());
                txtTransferNo.Focus();
                return;
            }

            if (Program.IsDate(txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                txtDate.Focus();
                return;
            }

            DataTable mDtSomFromStores = pMdtSomStores.View("code='" + txtFromCode.Text.Trim() + "'", "", "", "");
            if (mDtSomFromStores.Rows.Count == 0)
            {
                DataView mDvUserStores = new DataView();
                mDvUserStores.Table = Program.gDtUserActions;
                mDvUserStores.Sort = "storecode";

                if (mDvUserStores.Find(mDtSomFromStores.Rows[0]["code"].ToString().Trim()) == -1)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreDescriptionIsInvalid.ToString());
                    txtFromCode.Focus();
                    return;
                }
            }

            DataTable mDtSomToStores = pMdtSomStores.View("code='" + txtToCode.Text.Trim() + "'", "", "", "");
            if (mDtSomToStores.Rows.Count == 0)
            {
                DataView mDvUserStores = new DataView();
                mDvUserStores.Table = Program.gDtUserActions;
                mDvUserStores.Sort = "storecode";

                if (mDvUserStores.Find(mDtSomToStores.Rows[0]["code"].ToString().Trim()) == -1)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreDescriptionIsInvalid.ToString());
                    txtToCode.Focus();
                    return;
                }
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
                if (txtTransferNo.Text.Trim().ToLower() == "<<---new--->>")
                {
                    mGeneratePONumber = 1;
                }

                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

                viewIVTransferInItems.PostEditor();

                //add
                pResult = pMdtSomTransferIns.Add(mGeneratePONumber, mTransDate, txtTransferNo.Text, txtTitle.Text,
                    txtToCode.Text, txtToDesciption.Text, txtFromCode.Text, txtFromDescription.Text,
                    AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString(), cboCurrency.GetColumnValue("code").ToString(),
                    cboCurrency.GetColumnValue("description").ToString(), Convert.ToDouble(txtExchangeRate.Text),
                    cboCurrency.GetColumnValue("currencysymbol").ToString(), txtRemarks.Text, pDtItems, Program.gCurrentUser.Code);
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

                //refresh
                try
                {
                    frmIVTransferInsView mIVTransferInsView = (frmIVTransferInsView)Program.gMdiForm.ActiveMdiChild;
                    mIVTransferInsView.Data_Fill();
                }
                catch { }
                this.Mode_New();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Data_Edit
        private void Data_Edit()
        {
            string mFunctionName = "Data_Edit";

            #region validation

            if (txtTransferNo.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IV_TransferNoIsInvalid.ToString());
                txtTransferNo.Focus();
                return;
            }

            if (Program.IsDate(txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                txtDate.Focus();
                return;
            }

            DataTable mDtSomFromStores = pMdtSomStores.View("code='" + txtFromCode.Text.Trim() + "'", "", "", "");
            if (mDtSomFromStores.Rows.Count == 0)
            {
                DataView mDvUserStores = new DataView();
                mDvUserStores.Table = Program.gDtUserActions;
                mDvUserStores.Sort = "storecode";

                if (mDvUserStores.Find(mDtSomFromStores.Rows[0]["code"].ToString().Trim()) == -1)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreDescriptionIsInvalid.ToString());
                    txtFromCode.Focus();
                    return;
                }
            }

            DataTable mDtSomToStores = pMdtSomStores.View("code='" + txtToCode.Text.Trim() + "'", "", "", "");
            if (mDtSomToStores.Rows.Count == 0)
            {
                DataView mDvUserStores = new DataView();
                mDvUserStores.Table = Program.gDtUserActions;
                mDvUserStores.Sort = "storecode";

                if (mDvUserStores.Find(mDtSomToStores.Rows[0]["code"].ToString().Trim()) == -1)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreDescriptionIsInvalid.ToString());
                    txtToCode.Focus();
                    return;
                }
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
                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

                viewIVTransferInItems.PostEditor();

                //edit
                pResult = pMdtSomTransferIns.Edit(mTransDate, txtTransferNo.Text, pTransferOutNo, txtTitle.Text, txtToCode.Text, txtToDesciption.Text,
                    txtFromCode.Text, txtFromDescription.Text, AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString(),
                    cboCurrency.GetColumnValue("code").ToString(), cboCurrency.GetColumnValue("description").ToString(),
                    Convert.ToDouble(txtExchangeRate.Text), cboCurrency.GetColumnValue("currencysymbol").ToString(), txtRemarks.Text,
                    pDtItems, Program.gCurrentUser.Code);
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

                //refresh
                try
                {
                    frmIVTransferInsView mIVPurchaseOrdersView = (frmIVTransferInsView)Program.gMdiForm.ActiveMdiChild;
                    mIVPurchaseOrdersView.Data_Fill();
                }
                catch { }

                Program.Display_Info(AfyaPro_Types.clsSystemMessages.MessageIds.IV_OrderSaved.ToString());
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
            switch (gDataState.Trim().ToLower())
            {
                case "new": this.Data_New(); break;
                case "edit": this.Data_Edit(); break;
            }
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}