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
    public partial class frmIVPhysicalInventory : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsSomPhysicalInventory pMdtSomPhysicalInventory;
        private AfyaPro_MT.clsSomPackagings pMdtSomPackagings;
        private AfyaPro_MT.clsSomProducts pMdtSomProducts;
        private AfyaPro_MT.clsSomStores pMdtSomStores;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataTable pDtItems = new DataTable("items");
        private DataTable pDtExpected = new DataTable("expected");
        private DataRow pSelectedRow = null;
        internal string gDataState = "";

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private DataTable pDtPackagings = new DataTable("packagings");
        private DataTable pDtStores = new DataTable("stores");

        private string pCurrentGridMode = "open";
        private string pStoreCode = "";

        #endregion

        #region frmIVPhysicalInventory
        public frmIVPhysicalInventory(string mStoreCode)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmIVPurchaseOrder";

            try
            {
                this.pStoreCode = mStoreCode;
                this.Icon = Program.gMdiForm.Icon;
                this.CancelButton = cmdClose;

                pMdtSomPhysicalInventory = (AfyaPro_MT.clsSomPhysicalInventory)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomPhysicalInventory),
                    Program.gMiddleTier + "clsSomPhysicalInventory");

                pMdtSomPackagings = (AfyaPro_MT.clsSomPackagings)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomPackagings),
                    Program.gMiddleTier + "clsSomPackagings");

                pMdtSomProducts = (AfyaPro_MT.clsSomProducts)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomProducts),
                    Program.gMiddleTier + "clsSomProducts");

                pMdtSomStores = (AfyaPro_MT.clsSomStores)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomStores),
                    Program.gMiddleTier + "clsSomStores");

                pMdtAutoCodes = (AfyaPro_MT.clsAutoCodes)Activator.GetObject(
                    typeof(AfyaPro_MT.clsAutoCodes),
                    Program.gMiddleTier + "clsAutoCodes");

                #region packaging lookupedit
                pDtPackagings.Columns.Add("code", typeof(System.String));
                pDtPackagings.Columns.Add("description", typeof(System.String));
                pDtPackagings.Columns.Add("pieces", typeof(System.Int32));
                packagingeditor.DataSource = pDtPackagings;
                #endregion

                #region stores lookupedit
                pDtStores.Columns.Add("code", typeof(System.String));
                pDtStores.Columns.Add("description", typeof(System.String));
                cboStore.Properties.DataSource = pDtStores;
                cboStore.Properties.DisplayMember = "description";
                cboStore.Properties.ValueMember = "code";
                #endregion

                this.Fill_LookupData();

                pDtItems.Columns.Add("productcode", typeof(System.String));
                pDtItems.Columns.Add("productdescription", typeof(System.String));
                pDtItems.Columns.Add("productopmcode", typeof(System.String));
                pDtItems.Columns.Add("productopmdescription", typeof(System.String));
                pDtItems.Columns.Add("productdepartmentcode", typeof(System.String));
                pDtItems.Columns.Add("productdepartmentdescription", typeof(System.String));
                pDtItems.Columns.Add("packagingcode", typeof(System.String));
                pDtItems.Columns.Add("packagingdescription", typeof(System.String));
                pDtItems.Columns.Add("piecesinpackage", typeof(System.Int32));
                pDtItems.Columns.Add("expirydate", typeof(System.DateTime));
                pDtItems.Columns.Add("countedqty", typeof(System.Double));
                pDtItems.Columns.Add("expectedqty", typeof(System.Double));
                pDtItems.Columns.Add("deltaqty", typeof(System.Double));
                pDtItems.Columns.Add("deltapercent", typeof(System.Int16));
                pDtItems.Columns.Add("transprice", typeof(System.Double));
                pDtItems.Columns.Add("amount", typeof(System.Double));

                grdIVPhysicalInventoryItems.DataSource = pDtItems;

                foreach (DevExpress.XtraGrid.Columns.GridColumn mGridColumn in viewIVPhysicalInventoryItems.Columns)
                {
                    mGridColumn.OptionsColumn.AllowEdit = false;
                }

                viewIVPhysicalInventoryItems.Columns["packagingdescription"].ColumnEdit = packagingeditor;
                viewIVPhysicalInventoryItems.Columns["countedqty"].ColumnEdit = quantityeditor;

                txtOpenedDate.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                txtOpenedDate.EditValue = Program.gMdiForm.txtDate.EditValue;

                layoutControl1.AllowCustomizationMenu = false;
                cmdCalculate.Enabled = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivphysicalinventory_calculate.ToString());
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

        #region Grid_Flexibility
        private void Grid_Flexibility()
        {
            switch (pCurrentGridMode.Trim().ToLower())
            {
                case "open":
                    {
                        Program.Restore_GridLayout(grdIVPhysicalInventoryItems, "grdIVPhysicalInventoryItems_Open");

                        viewIVPhysicalInventoryItems.Columns["packagingdescription"].OptionsColumn.AllowEdit = true;
                        viewIVPhysicalInventoryItems.Columns["expirydate"].OptionsColumn.AllowEdit = true;
                        viewIVPhysicalInventoryItems.Columns["countedqty"].OptionsColumn.AllowEdit = true;
                        viewIVPhysicalInventoryItems.Columns["expectedqty"].Visible = false;
                        viewIVPhysicalInventoryItems.Columns["deltaqty"].Visible = false;
                        viewIVPhysicalInventoryItems.Columns["deltapercent"].Visible = false;

                        cmdItemQuickScan.Enabled = true;
                        cmdItemDelete.Enabled = true;
                    }
                    break;
                case "calculated":
                    {
                        Program.Restore_GridLayout(grdIVPhysicalInventoryItems, "grdIVPhysicalInventoryItems_Calculated");

                        viewIVPhysicalInventoryItems.Columns["packagingdescription"].OptionsColumn.AllowEdit = false;
                        viewIVPhysicalInventoryItems.Columns["expirydate"].OptionsColumn.AllowEdit = false;
                        viewIVPhysicalInventoryItems.Columns["countedqty"].OptionsColumn.AllowEdit = false;
                        viewIVPhysicalInventoryItems.Columns["expectedqty"].Visible = true;
                        viewIVPhysicalInventoryItems.Columns["deltaqty"].Visible = true;
                        viewIVPhysicalInventoryItems.Columns["deltapercent"].Visible = true;

                        cmdItemQuickScan.Enabled = false;
                        cmdItemDelete.Enabled = false;
                    }
                    break;
                case "closed":
                    {
                        Program.Restore_GridLayout(grdIVPhysicalInventoryItems, "grdIVPhysicalInventoryItems_Calculated");

                        viewIVPhysicalInventoryItems.Columns["packagingdescription"].OptionsColumn.AllowEdit = false;
                        viewIVPhysicalInventoryItems.Columns["expirydate"].OptionsColumn.AllowEdit = false;
                        viewIVPhysicalInventoryItems.Columns["countedqty"].OptionsColumn.AllowEdit = false;
                        viewIVPhysicalInventoryItems.Columns["expectedqty"].Visible = true;
                        viewIVPhysicalInventoryItems.Columns["deltaqty"].Visible = true;
                        viewIVPhysicalInventoryItems.Columns["deltapercent"].Visible = true;

                        cmdItemQuickScan.Enabled = false;
                        cmdItemDelete.Enabled = false;
                    }
                    break;
            }
        }
        #endregion

        #region packagingeditor_EditValueChanged
        private void packagingeditor_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.LookUpEdit mLookupEdit = sender as DevExpress.XtraEditors.LookUpEdit;

            if (viewIVPhysicalInventoryItems.FocusedRowHandle < 0)
            {
                return;
            }

            DataRow mSelectedRow = viewIVPhysicalInventoryItems.GetDataRow(viewIVPhysicalInventoryItems.FocusedRowHandle);

            //get previous cost and onhandqty
            double mUnitPrice = Convert.ToDouble(mSelectedRow["transprice"]) / Convert.ToDouble(mSelectedRow["piecesinpackage"]);
            double mCountedQty = Convert.ToDouble(mSelectedRow["countedqty"]) * Convert.ToDouble(mSelectedRow["piecesinpackage"]);
            double mExpectedQty = Convert.ToDouble(mSelectedRow["expectedqty"]) * Convert.ToDouble(mSelectedRow["piecesinpackage"]);
            double mDeltaQty = Convert.ToDouble(mSelectedRow["deltaqty"]) * Convert.ToDouble(mSelectedRow["piecesinpackage"]);

            double mTransPrice = mUnitPrice * Convert.ToInt32(mLookupEdit.GetColumnValue("pieces"));

            mSelectedRow.BeginEdit();

            mSelectedRow["packagingcode"] = mLookupEdit.GetColumnValue("code").ToString();
            mSelectedRow["packagingdescription"] = mLookupEdit.GetColumnValue("description").ToString();
            mSelectedRow["piecesinpackage"] = mLookupEdit.GetColumnValue("pieces").ToString();
            mSelectedRow["transprice"] = mUnitPrice * Convert.ToInt32(mLookupEdit.GetColumnValue("pieces"));
            mSelectedRow["amount"] = mTransPrice * Convert.ToDouble(mSelectedRow["countedqty"]);

            mSelectedRow.EndEdit();
        }
        #endregion

        #region viewIVPhysicalInventoryItems_ValidatingEditor
        private void viewIVPhysicalInventoryItems_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            switch (viewIVPhysicalInventoryItems.FocusedColumn.FieldName.ToLower())
            {
                case "countedqty":
                    {
                        if (Program.IsMoney(e.Value.ToString()) == false)
                        {
                            e.ErrorText = Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_EntryIsInvalid.ToString());
                            e.Valid = false;
                            return;
                        }

                        DataRow mSelectedRow = viewIVPhysicalInventoryItems.GetDataRow(viewIVPhysicalInventoryItems.FocusedRowHandle);

                        #region compute amount

                        mSelectedRow.BeginEdit();

                        double mOrderedQty = Convert.ToDouble(e.Value);
                        double mTransPrice = Convert.ToDouble(mSelectedRow["transprice"]);
                        mSelectedRow["amount"] = mTransPrice * mOrderedQty;

                        mSelectedRow.EndEdit();

                        #endregion
                    }
                    break;
            }
        }
        #endregion

        #region frmIVPhysicalInventory_Load
        private void frmIVPhysicalInventory_Load(object sender, EventArgs e)
        {
            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            switch (gDataState.Trim().ToLower())
            {
                case "new": Mode_New(); break;
                case "edit": Mode_Edit(); break;
            }

            this.Grid_Flexibility();
            this.Load_Controls();

            for (int mIndex = 0; mIndex < viewIVPhysicalInventoryItems.Columns.Count; mIndex++)
            {
                switch (viewIVPhysicalInventoryItems.Columns[mIndex].ColumnType.ToString().ToLower())
                {
                    case "system.double":
                        {
                            viewIVPhysicalInventoryItems.Columns[mIndex].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            viewIVPhysicalInventoryItems.Columns[mIndex].DisplayFormat.FormatString = "{0:c}";
                        }
                        break;
                }
            }

            this.pFormWidth = this.Width;
            this.pFormHeight = this.Height;

            cboStore.ItemIndex = Program.Get_LookupItemIndex(cboStore, "code", pStoreCode);

            if (pStoreCode.Trim() != "")
            {
                DataTable mDtItems = pMdtSomPhysicalInventory.View_PhysicalInventoryItems(
                    cboStore.GetColumnValue("code").ToString().Trim(), "1=2", "autocode",
                    Program.gLanguageName, grdIVPhysicalInventoryItems.Name);

                foreach (DataColumn mDataColumn in pDtItems.Columns)
                {
                    mDataColumn.Caption = mDtItems.Columns[mDataColumn.ColumnName].Caption;
                }
            }

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

        #region frmIVPhysicalInventory_FormClosing
        private void frmIVPhysicalInventory_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //layout
                if (layoutControl1.IsModified == true)
                {
                    Program.Save_FormLayout(this, layoutControl1, this.Name);
                }

                //grid
                switch (pCurrentGridMode.Trim().ToLower())
                {
                    case "open":
                        {
                            Program.Save_GridLayout(grdIVPhysicalInventoryItems, "grdIVPhysicalInventoryItems_Open");
                        }
                        break;
                    case "calculated":
                        {
                            Program.Save_GridLayout(grdIVPhysicalInventoryItems, "grdIVPhysicalInventoryItems_Calculated");
                        }
                        break;
                    case "closed":
                        {
                            Program.Save_GridLayout(grdIVPhysicalInventoryItems, "grdIVPhysicalInventoryItems_Calculated");
                        }
                        break;
                }
            }
            catch { }
        }
        #endregion

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(txbReferenceNo);
            mObjectsList.Add(txbDescription);
            mObjectsList.Add(txbStore);
            mObjectsList.Add(txbOpenedDate);
            mObjectsList.Add(txbCalculatedDate);
            mObjectsList.Add(txbClosedDate);
            mObjectsList.Add(radInventoryStatus.Properties.Items[0]);
            mObjectsList.Add(radInventoryStatus.Properties.Items[1]);
            mObjectsList.Add(radInventoryStatus.Properties.Items[2]);
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
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.stocktakingno));
                if (mGenerateCode == -1)
                {
                    Program.Display_Server_Error("");
                    return;
                }

                txtReferenceNo.Text = "";

                if (mGenerateCode == 1)
                {
                    txtReferenceNo.Text = "<<---New--->>";
                    txtReferenceNo.Enabled = false;
                    txtDescription.Focus();
                }
                else
                {
                    txtReferenceNo.Enabled = true;
                    txtReferenceNo.Focus();
                }

                cmdOk.Enabled = true;
                cmdRecount.Enabled = false;
                cmdCommit.Enabled = false;
                cmdCalculate.Enabled = false;
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
                    (DevExpress.XtraGrid.Views.Grid.GridView)((frmIVPhysicalInventoryView)Program.gMdiForm.ActiveMdiChild).grdIVPhysicalInventoryView.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                txtReferenceNo.Text = pSelectedRow["referenceno"].ToString();
                txtDescription.Text = pSelectedRow["description"].ToString();
                cboStore.ItemIndex = Program.Get_LookupItemIndex(cboStore, "code", pSelectedRow["storecode"].ToString());
                txtOpenedDate.EditValue = Convert.ToDateTime(pSelectedRow["transdate"]);
                if (Program.IsNullDate(pSelectedRow["calculateddate"]) == false)
                {
                    txtCalculatedDate.EditValue = Convert.ToDateTime(pSelectedRow["calculateddate"]);
                }
                else
                {
                    txtCalculatedDate.EditValue = DBNull.Value;
                }
                if (Program.IsNullDate(pSelectedRow["closeddate"]) == false)
                {
                    txtClosedDate.EditValue = Convert.ToDateTime(pSelectedRow["closeddate"]);
                }
                else
                {
                    txtClosedDate.EditValue = DBNull.Value;
                }

                if (pSelectedRow["inventorystatus"].ToString().Trim().ToLower() ==
                    AfyaPro_Types.clsEnums.SomInventoryStatus.Open.ToString().ToLower())
                {
                    radInventoryStatus.SelectedIndex = 0;
                    pCurrentGridMode = "open";

                    cmdOk.Enabled = true;
                    cmdRecount.Enabled = false;
                    cmdCommit.Enabled = false;
                    cmdCalculate.Enabled = true;
                }
                if (pSelectedRow["inventorystatus"].ToString().Trim().ToLower() ==
                    AfyaPro_Types.clsEnums.SomInventoryStatus.Calculated.ToString().ToLower())
                {
                    radInventoryStatus.SelectedIndex = 1;
                    pCurrentGridMode = "calculated";

                    cmdOk.Enabled = true;
                    cmdRecount.Enabled = true;
                    cmdCommit.Enabled = true;
                    cmdCalculate.Enabled = true;
                }
                if (pSelectedRow["inventorystatus"].ToString().Trim().ToLower() ==
                    AfyaPro_Types.clsEnums.SomInventoryStatus.Closed.ToString().ToLower())
                {
                    radInventoryStatus.SelectedIndex = 2;
                    pCurrentGridMode = "closed";

                    cmdOk.Enabled = false;
                    cmdRecount.Enabled = false;
                    cmdCommit.Enabled = false;
                    cmdCalculate.Enabled = false;
                }

                this.Grid_Flexibility();
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

        #region Fill_Contents
        private void Fill_Contents()
        {
            string mFunctionName = "Fill_Contents";

            try
            {
                if (cboStore.ItemIndex == -1)
                {
                    return;
                }

                DataTable mDtItems = pMdtSomPhysicalInventory.View_PhysicalInventoryItems(
                    cboStore.GetColumnValue("code").ToString().Trim(),
                    "referenceno='" + txtReferenceNo.Text.Trim() + "'", "autocode",
                    Program.gLanguageName, grdIVPhysicalInventoryItems.Name);

                pDtItems.Rows.Clear();

                foreach (DataRow mDataRow in mDtItems.Rows)
                {
                    double mDeltaQty = Convert.ToDouble(mDataRow["countedqty"]) - Convert.ToDouble(mDataRow["expectedqty"]);
                    Int16 mDeltaPercent = 0;
                    if (Convert.ToDouble(mDataRow["expectedqty"]) != 0)
                    {
                        mDeltaPercent = Convert.ToInt16(mDeltaQty * 100 / Convert.ToDouble(mDataRow["expectedqty"]));
                    }

                    DataRow mNewRow = pDtItems.NewRow();
                    mNewRow["productcode"] = mDataRow["productcode"].ToString().Trim();
                    mNewRow["productdescription"] = mDataRow["productdescription"].ToString().Trim();
                    mNewRow["productopmcode"] = mDataRow["productopmcode"].ToString().Trim();
                    mNewRow["productopmdescription"] = mDataRow["productopmdescription"].ToString().Trim();
                    mNewRow["productdepartmentcode"] = mDataRow["productdepartmentcode"].ToString().Trim();
                    mNewRow["productdepartmentdescription"] = mDataRow["productdepartmentdescription"].ToString().Trim();
                    mNewRow["packagingcode"] = mDataRow["packagingcode"].ToString().Trim();
                    mNewRow["packagingdescription"] = mDataRow["packagingdescription"].ToString().Trim();
                    mNewRow["piecesinpackage"] = Convert.ToInt32(mDataRow["piecesinpackage"]);
                    if (Program.IsNullDate(mDataRow["expirydate"]) == true)
                    {
                        mNewRow["expirydate"] = DBNull.Value;
                    }
                    else
                    {
                        mNewRow["expirydate"] = mDataRow["expirydate"];
                    }
                    mNewRow["countedqty"] = Convert.ToDouble(mDataRow["countedqty"]);
                    mNewRow["expectedqty"] = Convert.ToDouble(mDataRow["expectedqty"]);
                    mNewRow["deltaqty"] = mDeltaQty;
                    mNewRow["deltapercent"] = mDeltaPercent;
                    mNewRow["transprice"] = Convert.ToDouble(mDataRow["transprice"]);
                    mNewRow["amount"] = Convert.ToDouble(mDataRow["countedqty"]) * Convert.ToDouble(mDataRow["transprice"]);
                    pDtItems.Rows.Add(mNewRow);
                    pDtItems.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtItems.Columns)
                {
                    mDataColumn.Caption = mDtItems.Columns[mDataColumn.ColumnName].Caption;
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
        internal void Add_Item(DataRow mDataRow, object mExpiryDate, double mQuantity)
        {
            string mFunctionName = "Add_Item";

            try
            {
                int mRowIndex = -1;
                for (int mRowCount = 0; mRowCount < pDtItems.Rows.Count; mRowCount++)
                {
                    DataRow mItemDataRow = pDtItems.Rows[mRowCount];

                    if (mItemDataRow["productcode"].ToString().Trim().ToLower() == mDataRow["code"].ToString().Trim().ToLower()
                        && mItemDataRow["expirydate"].Equals(mExpiryDate))
                    {
                        mRowIndex = mRowCount;
                        break;
                    }
                }

                if (mRowIndex >= 0)
                {
                    DataRow mEditDataRow = pDtItems.Rows[mRowIndex];

                    mEditDataRow.BeginEdit();

                    mEditDataRow["expirydate"] = mExpiryDate;
                    mEditDataRow["countedqty"] = Convert.ToDouble(mQuantity);
                    mEditDataRow["amount"] = Convert.ToDouble(mEditDataRow["transprice"]) * mQuantity;

                    mEditDataRow.EndEdit();
                }
                else
                {
                    DataRow mNewRow = pDtItems.NewRow();
                    mNewRow["productcode"] = mDataRow["code"].ToString().Trim();
                    mNewRow["productdescription"] = mDataRow["description"].ToString().Trim();
                    mNewRow["productopmcode"] = mDataRow["opmcode"].ToString().Trim();
                    mNewRow["productopmdescription"] = mDataRow["opmdescription"].ToString().Trim();
                    mNewRow["productdepartmentcode"] = mDataRow["departmentcode"].ToString().Trim();
                    mNewRow["productdepartmentdescription"] = mDataRow["departmentdescription"].ToString().Trim();
                    mNewRow["packagingcode"] = mDataRow["packagingcode"].ToString().Trim();
                    mNewRow["packagingdescription"] = mDataRow["packagingdescription"].ToString().Trim();
                    mNewRow["piecesinpackage"] = Convert.ToInt32(mDataRow["piecesinpackage"]);
                    mNewRow["expirydate"] = mExpiryDate;
                    mNewRow["countedqty"] = mQuantity;
                    mNewRow["expectedqty"] = 0;
                    mNewRow["deltaqty"] = 0;
                    mNewRow["deltapercent"] = 0;
                    mNewRow["transprice"] = Convert.ToDouble(mDataRow["costprice"]) * Convert.ToDouble(mNewRow["piecesinpackage"]);
                    mNewRow["amount"] = Convert.ToDouble(mNewRow["transprice"]) * mQuantity;
                    pDtItems.Rows.Add(mNewRow);
                    pDtItems.AcceptChanges();
                }
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
            //frmSearchProduct mSearchProduct = new frmSearchProduct();
            //mSearchProduct.ShowDialog();

            //if (mSearchProduct.SearchingDone == true)
            //{
            //    pSelectedRow = mSearchProduct.SelectedRow;
            //    if (pSelectedRow != null)
            //    {
            //        this.Add_Item(pSelectedRow, 1);
            //    }
            //}
        }
        #endregion

        #region cmdItemQuickScan_Click
        private void cmdItemQuickScan_Click(object sender, EventArgs e)
        {
            if (cboStore.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreDescriptionIsInvalid.ToString());
                cboStore.Focus();
                return;
            }

            frmIVQuickScanExpiryDates mIVQuickScanExpiryDates = new frmIVQuickScanExpiryDates(this, cboStore.GetColumnValue("code").ToString().Trim());
            mIVQuickScanExpiryDates.ShowDialog();
        }
        #endregion

        #region cmdItemDelete_Click
        private void cmdItemDelete_Click(object sender, EventArgs e)
        {
            viewIVPhysicalInventoryItems.DeleteSelectedRows();
            pDtItems.AcceptChanges();
        }
        #endregion

        #region Data_New
        private void Data_New()
        {
            Int16 mGenerateNumber = 0;
            string mFunctionName = "Data_New";

            #region validation

            if (txtReferenceNo.Text.Trim() == "" && txtReferenceNo.Text.Trim().ToLower() != "<<---new--->>")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IV_StockTakingNoIsInvalid.ToString());
                txtReferenceNo.Focus();
                return;
            }

            if (Program.IsDate(txtOpenedDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                txtOpenedDate.Focus();
                return;
            }

            if (cboStore.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreDescriptionIsInvalid.ToString());
                cboStore.Focus();
                return;
            }

            #endregion

            try
            {
                if (txtReferenceNo.Text.Trim().ToLower() == "<<---new--->>")
                {
                    mGenerateNumber = 1;
                }

                DateTime mTransDate = Convert.ToDateTime(txtOpenedDate.EditValue);

                viewIVPhysicalInventoryItems.PostEditor();            

                //add
                pResult = pMdtSomPhysicalInventory.Add(mGenerateNumber, mTransDate, cboStore.GetColumnValue("code").ToString(),
                    cboStore.GetColumnValue("description").ToString(), txtReferenceNo.Text, txtDescription.Text, pDtItems, Program.gCurrentUser.Code);
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
                    frmIVPhysicalInventoryView mIVPurchaseOrdersView = (frmIVPhysicalInventoryView)Program.gMdiForm.ActiveMdiChild;
                    mIVPurchaseOrdersView.Data_Fill();
                }
                catch { }
                this.Mode_Edit();
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

            if (txtReferenceNo.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IV_StockTakingNoIsInvalid.ToString());
                txtReferenceNo.Focus();
                return;
            }

            if (Program.IsDate(txtOpenedDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                txtOpenedDate.Focus();
                return;
            }

            if (cboStore.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreDescriptionIsInvalid.ToString());
                cboStore.Focus();
                return;
            }

            #endregion

            try
            {
                DateTime mTransDate = Convert.ToDateTime(txtOpenedDate.EditValue);

                viewIVPhysicalInventoryItems.PostEditor();

                string mInventoryStatus = "";
                switch (pCurrentGridMode.Trim().ToLower())
                {
                    case "open":
                        {
                            mInventoryStatus = AfyaPro_Types.clsEnums.SomInventoryStatus.Open.ToString();
                        }
                        break;
                    case "calculated":
                        {
                            mInventoryStatus = AfyaPro_Types.clsEnums.SomInventoryStatus.Calculated.ToString();
                        }
                        break;
                    case "closed":
                        {
                            mInventoryStatus = AfyaPro_Types.clsEnums.SomInventoryStatus.Closed.ToString();
                        }
                        break;
                }

                DateTime mCalculatedDate = new DateTime();
                if (Program.IsDate(txtCalculatedDate.EditValue) == true)
                {
                    mCalculatedDate = Convert.ToDateTime(txtCalculatedDate.EditValue);
                }

                //edit
                pResult = pMdtSomPhysicalInventory.Edit(mTransDate, mCalculatedDate, cboStore.GetColumnValue("code").ToString(),
                    cboStore.GetColumnValue("description").ToString(), txtReferenceNo.Text, txtDescription.Text, pDtItems, 
                    mInventoryStatus, Program.gCurrentUser.Code);

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
                    frmIVPhysicalInventoryView mIVPhysicalInventoryView = (frmIVPhysicalInventoryView)Program.gMdiForm.ActiveMdiChild;
                    mIVPhysicalInventoryView.Data_Fill();
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

        #region Get_ExpectedQuantities
        private void Get_ExpectedQuantities()
        {
            string mFunctionName = "Get_ExpectedQuantities";

            if (cboStore.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreDescriptionIsInvalid.ToString());
                cboStore.Focus();
                return;
            }

            try
            {
                string mFilter = "";
                foreach (DataRow mDataRow in pDtItems.Rows)
                {
                    if (mFilter.Trim() == "")
                    {
                        mFilter = "'" + mDataRow["productcode"].ToString().Trim() + "'";
                    }
                    else
                    {
                        mFilter = mFilter + ",'" + mDataRow["productcode"].ToString().Trim() + "'";
                    }
                }
                if (mFilter.Trim() == "")
                {
                    mFilter = "1=2";
                }
                else
                {
                    mFilter = "productcode in (" + mFilter + ")";
                }

                pDtExpected = pMdtSomPhysicalInventory.Get_ExpectedQuantities(
                    cboStore.GetColumnValue("code").ToString(), mFilter, "");
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdRecount_Click
        private void cmdRecount_Click(object sender, EventArgs e)
        {
            pCurrentGridMode = "open";
            this.Grid_Flexibility();

            txtCalculatedDate.EditValue = DBNull.Value;
            cmdRecount.Enabled = false;
            cmdCommit.Enabled = false;
        }
        #endregion

        #region cmdCommit_Click
        private void cmdCommit_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdCommit_Click";

            if (Program.IsDate(Program.gMdiForm.txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                return;
            }

            try
            {
                DialogResult mDialogResult = Program.Display_Question(
                    AfyaPro_Types.clsSystemMessages.MessageIds.IV_StockTakingConfirmCommiting.ToString(), MessageBoxDefaultButton.Button2);

                if (mDialogResult != DialogResult.Yes)
                {
                    return;
                }

                DateTime mTransDate = Convert.ToDateTime(txtOpenedDate.EditValue);

                DateTime mCalculatedDate = Convert.ToDateTime(txtCalculatedDate.EditValue);

                string mStoreCode = cboStore.GetColumnValue("code").ToString();

                string mInventoryStatus = "";
                switch (pCurrentGridMode.Trim().ToLower())
                {
                    case "open":
                        {
                            mInventoryStatus = AfyaPro_Types.clsEnums.SomInventoryStatus.Open.ToString();
                        }
                        break;
                    case "calculated":
                        {
                            mInventoryStatus = AfyaPro_Types.clsEnums.SomInventoryStatus.Calculated.ToString();
                        }
                        break;
                    case "closed":
                        {
                            mInventoryStatus = AfyaPro_Types.clsEnums.SomInventoryStatus.Closed.ToString();
                        }
                        break;
                }

                //edit
                pResult = pMdtSomPhysicalInventory.Edit(mTransDate, mCalculatedDate, cboStore.GetColumnValue("code").ToString(),
                    cboStore.GetColumnValue("description").ToString(), txtReferenceNo.Text, txtDescription.Text, pDtItems, mInventoryStatus, Program.gCurrentUser.Code);

                //commit
                pResult = pMdtSomPhysicalInventory.Commit(mTransDate, mCalculatedDate, mStoreCode,
                    txtReferenceNo.Text, txtDescription.Text, pDtItems, Program.gCurrentUser.Code);

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
                    frmIVPhysicalInventoryView mIVPhysicalInventoryView = (frmIVPhysicalInventoryView)Program.gMdiForm.ActiveMdiChild;
                    mIVPhysicalInventoryView.Data_Fill();
                }
                catch { }

                this.Close();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdCalculate_Click
        private void cmdCalculate_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdCalculate_Click";

            if (cboStore.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreDescriptionIsInvalid.ToString());
                cboStore.Focus();
                return;
            }

            try
            {
                Program.Display_Info(AfyaPro_Types.clsSystemMessages.MessageIds.IV_StockTakingConfirmCalculate.ToString());

                pCurrentGridMode = "calculated";
                this.Grid_Flexibility();
                this.Get_ExpectedQuantities();

                foreach (DataRow mDataRow in pDtItems.Rows)
                {
                    double mCountedQty = Convert.ToDouble(mDataRow["countedqty"]);
                    double mExpectedQty = 0;

                    if (Convert.IsDBNull(mDataRow["expirydate"]) == true)
                    {
                        object mExpiryDate = DBNull.Value;
                        foreach (DataRow mExpectedRow in pDtExpected.Rows)
                        {
                            if (mDataRow["productcode"].ToString().Trim().ToLower() == mExpectedRow["productcode"].ToString().Trim().ToLower()
                                && Program.IsNullDate(mExpectedRow["expirydate"]) == true)
                            {
                                mExpectedQty = Convert.ToDouble(mExpectedRow["expectedqty"]);
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (DataRow mExpectedRow in pDtExpected.Rows)
                        {
                            if (mDataRow["productcode"].ToString().Trim().ToLower() == mExpectedRow["productcode"].ToString().Trim().ToLower()
                                && mExpectedRow["expirydate"].Equals(mDataRow["expirydate"]))
                            {
                                mExpectedQty = Convert.ToDouble(mExpectedRow["expectedqty"]);
                                break;
                            }
                        }
                    }

                    mExpectedQty = mExpectedQty / Convert.ToInt32(mDataRow["piecesinpackage"]);

                    double mDeltaQty = mCountedQty - mExpectedQty;
                    double mDeltaPercent = 0;
                    if (mExpectedQty != 0)
                    {
                        mDeltaPercent = (mDeltaQty / mExpectedQty) * 100;
                    }

                    mDataRow.BeginEdit();
                    mDataRow["expectedqty"] = mExpectedQty;
                    mDataRow["deltaqty"] = mDeltaQty;
                    mDataRow["deltapercent"] = mDeltaPercent;
                    mDataRow.EndEdit();
                }

                txtCalculatedDate.EditValue = Program.gMdiForm.txtDate.EditValue;
                cmdRecount.Enabled = true;
                cmdCommit.Enabled = true;
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
    }
}