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

namespace AfyaPro_NextGen
{
    public partial class frmIVSuggestStock : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private Type pType;
        private string pClassName = "";

        private DataTable pDtExpiryDates;

        private string pStoreCode = "";
        internal string StoreCode
        {
            set { pStoreCode = value; }
            get { return pStoreCode; }
        }

        private string pItemTransferId = "";
        internal string ItemTransferId
        {
            set { pItemTransferId = value; }
            get { return pItemTransferId; }
        }

        private string pPackagingDescription = "";
        internal string PackagingDescription
        {
            set { pPackagingDescription = value; }
            get { return pPackagingDescription; }
        }

        private double pPiecesInPackage = 0;
        internal double PiecesInPackage
        {
            set { pPiecesInPackage = value; }
            get { return pPiecesInPackage; }
        }

        private double pTotalToIssue = 0;
        internal double TotalToIssue
        {
            set { pTotalToIssue = value; }
            get { return pTotalToIssue; }
        }

        private bool pSuggestionAccepted = false;
        internal bool SuggestionAccepted
        {
            set { pSuggestionAccepted = value; }
            get { return pSuggestionAccepted; }
        }

        private AfyaPro_MT.clsSomStores pMdtSomStores;

        #endregion

        #region frmIVSuggestStock
        public frmIVSuggestStock(DataTable mDtExpiryDates)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmIVSuggestStock";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                this.CancelButton = cmdClose;
                this.pDtExpiryDates = mDtExpiryDates;

                pMdtSomStores = (AfyaPro_MT.clsSomStores)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomStores),
                    Program.gMiddleTier + "clsSomStores");
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmIVSuggestStock_Load
        private void frmIVSuggestStock_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmIVSuggestStock_Load";

            try
            {
                DataView mDvExpiryDates = new DataView();
                mDvExpiryDates.Table = pDtExpiryDates;
                mDvExpiryDates.RowFilter = "productcode='" + txtItemCode.Text.Trim() + "' and itemtransferid='" + pItemTransferId + "'";

                grdExpiryDates.DataSource = mDvExpiryDates;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdAccept_Click
        private void cmdAccept_Click(object sender, EventArgs e)
        {
            viewExpiryDates.PostEditor();
            pSuggestionAccepted = true;
            this.Close();
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            pSuggestionAccepted = false;
            this.Close();
        }
        #endregion

        #region Suggest
        internal void Suggest()
        {
            DataTable mDtSuggestStock = pMdtSomStores.Get_OnHandQuantitiesByExpiryDates(
                                pStoreCode, "productcode='" + txtItemCode.Text.Trim() + "'", "productcode,expirydate");

            DataView mDvExpiryDates = new DataView();
            mDvExpiryDates.Table = pDtExpiryDates;
            mDvExpiryDates.RowFilter = "productcode='" + txtItemCode.Text.Trim() + "' and itemtransferid='" + pItemTransferId + "'";
            foreach (DataRowView mDataRowView in mDvExpiryDates)
            {
                mDataRowView.Delete();
            }

            double mPendingQty = pTotalToIssue;
            foreach (DataRow mDataRow in mDtSuggestStock.Rows)
            {
                if (Program.IsNullDate(mDataRow["expirydate"]) == false)
                {
                    double mToIssue = mPendingQty;
                    double mOnHandQty = Convert.ToDouble(mDataRow["onhandqty"]) / pPiecesInPackage;

                    if (mOnHandQty < mToIssue)
                    {
                        mToIssue = mOnHandQty;
                    }

                    DataRow mNewRow = pDtExpiryDates.NewRow();
                    mNewRow["itemtransferid"] = pItemTransferId;
                    mNewRow["productcode"] = txtItemCode.Text.Trim();
                    mNewRow["expirydate"] = Convert.ToDateTime(mDataRow["expirydate"]);
                    mNewRow["packaging"] = pPackagingDescription;
                    mNewRow["balance"] = mOnHandQty;
                    mNewRow["quantity"] = mToIssue;
                    pDtExpiryDates.Rows.Add(mNewRow);
                    pDtExpiryDates.AcceptChanges();

                    mPendingQty = mPendingQty - mToIssue;
                }
            }

            mDvExpiryDates = new DataView();
            mDvExpiryDates.Table = pDtExpiryDates;
            mDvExpiryDates.RowFilter = "productcode='" + txtItemCode.Text.Trim() + "' and itemtransferid='" + pItemTransferId + "'";

            grdExpiryDates.DataSource = mDvExpiryDates;
        }
        #endregion

        #region txtQuantity_KeyDown
        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Add();
            }
        }
        #endregion

        #region cmdSuggest_Click
        private void cmdSuggest_Click(object sender, EventArgs e)
        {
            this.Suggest();
        }
        #endregion

        #region viewExpiryDates_ValidatingEditor
        private void viewExpiryDates_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)grdExpiryDates.MainView;

            if (mGridView.FocusedRowHandle < 0)
            {
                return;
            }

            switch (mGridView.FocusedColumn.FieldName.ToLower())
            {
                case "quantity":
                    {
                        if (Program.IsMoney(e.Value.ToString()) == false)
                        {
                            e.ErrorText = Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_EntryIsInvalid.ToString());
                            e.Valid = false;
                            return;
                        }
                    }
                    break;
            }
        }
        #endregion

        #region cmdAdd_Click
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            this.Add();
        }

        private void Add()
        {
            if (Program.IsMoney(txtQuantity.Text) == false)
            {
                Program.Display_Error("Invalid quantity");
                txtQuantity.Focus();
                return;
            }

            DateTime mExpiryDate = new DateTime();
            if (Program.IsDate(txtDate.Text) == true)
            {
                mExpiryDate = Convert.ToDateTime(txtDate.Text);
            }

            DataView mDvExpiryDates = (DataView)grdExpiryDates.DataSource;

            bool mFound = false;
            foreach (DataRowView mDataRowView in mDvExpiryDates)
            {
                if (mDataRowView["expirydate"].Equals(mExpiryDate) == true)
                {
                    mFound = true;
                    break;
                }
            }

            if (mFound == false)
            {
                DataRowView mDataRowView = mDvExpiryDates.AddNew();
                mDataRowView.BeginEdit();

                mDataRowView["itemtransferid"] = pItemTransferId;
                mDataRowView["productcode"] = txtItemCode.Text;
                if (Program.IsNullDate(mExpiryDate) == false)
                {
                    mDataRowView["expirydate"] = mExpiryDate;
                }
                mDataRowView["balance"] = 0;
                mDataRowView["quantity"] = Convert.ToDouble(txtQuantity.Text);
                mDataRowView["packaging"] = pPackagingDescription;

                mDataRowView.EndEdit();
                pDtExpiryDates.AcceptChanges();

                txtDate.Text = "";
                txtQuantity.Text = "";
            }
        }
        #endregion

        #region cmdRemove_Click
        private void cmdRemove_Click(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)grdExpiryDates.MainView;

            if (mGridView.FocusedRowHandle < 0)
            {
                return;
            }

            DataView mDvExpiryDates = (DataView)grdExpiryDates.DataSource;
            mGridView.DeleteRow(mGridView.FocusedRowHandle);
            mGridView.PostEditor();
            pDtExpiryDates.AcceptChanges();
        }
        #endregion
    }
}