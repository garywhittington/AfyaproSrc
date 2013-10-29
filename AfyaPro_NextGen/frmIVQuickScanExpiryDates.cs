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
    public partial class frmIVQuickScanExpiryDates : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsSomProducts pMdtSomProducts;
        private AfyaPro_MT.clsSomStores pMdtSomStores;

        private Type pType;
        private string pClassName = "";

        private string pCurrItemCode = "";
        private string pPrevItemCode = "";
        private bool pSearchingItem = false;

        private XtraForm pSourceForm = null;

        private DataRow pSelectedRow;
        internal DataRow SelectedRow
        {
            get { return pSelectedRow; }
            set { pSelectedRow = value; }
        }

        private string pStoreCode = "";

        #endregion

        #region frmIVQuickScanExpiryDates
        public frmIVQuickScanExpiryDates(XtraForm mSourceForm, string mStoreCode)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;

            string mFunctionName = "frmIVQuickScanExpiryDates";

            try
            {
                this.pStoreCode = mStoreCode;
                this.pSourceForm = mSourceForm;

                pMdtSomProducts = (AfyaPro_MT.clsSomProducts)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomProducts),
                    Program.gMiddleTier + "clsSomProducts");

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

        #region frmIVQuickScanExpiryDates_Load
        private void frmIVQuickScanExpiryDates_Load(object sender, EventArgs e)
        {
            Program.Center_Screen(this);
            txtCode.Focus();
        }
        #endregion

        #region lookup item

        #region Display_Item
        private void Display_Item()
        {
            string mFunctionName = "Display_Item";

            try
            {
                DataTable mDtProducts = pMdtSomProducts.View(
                    "code='" + txtCode.Text.Trim() + "' and visible_" + pStoreCode.Trim() + "=1", "", "", "");

                if (mDtProducts.Rows.Count > 0)
                {
                    pSelectedRow = mDtProducts.Rows[0];

                    txtCode.Text = mDtProducts.Rows[0]["code"].ToString().Trim();
                    txtDescription.Text = mDtProducts.Rows[0]["description"].ToString().Trim();
                    txtQuantity.Text = "";

                    DataTable mDtExpiryDates = pMdtSomStores.Get_OnHandQuantitiesByExpiryDates(
                        pStoreCode, "productcode='" + txtCode.Text.Trim() + "'", "");
                    lstExpiryDates.Items.Clear();
                    lstExpiryDates.Items.Add(DBNull.Value);
                    foreach (DataRow mDataRow in mDtExpiryDates.Rows)
                    {
                        if (Program.IsNullDate(mDataRow["expirydate"]) == false)
                        {
                            lstExpiryDates.Items.Add(Convert.ToDateTime(mDataRow["expirydate"]).Date);
                        }
                    }

                    pCurrItemCode = txtCode.Text;
                    pPrevItemCode = pCurrItemCode;
                    txtQuantity.Focus();
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdSearch_Click
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdSearch_Click";

            try
            {
                pSearchingItem = true;

                frmSearchProduct mSearchProduct = new frmSearchProduct();
                mSearchProduct.ShowDialog();
                if (mSearchProduct.SearchingDone == true)
                {
                    pSelectedRow = mSearchProduct.SelectedRow;
                    txtCode.Text = pSelectedRow["code"].ToString().Trim();
                }

                pSearchingItem = false;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region txtCode_EditValueChanged
        private void txtCode_EditValueChanged(object sender, EventArgs e)
        {
            if (pSearchingItem == true)
            {
                this.Display_Item();
            }
        }
        #endregion

        #region txtCode_KeyDown
        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Display_Item();
            }
        }
        #endregion

        #region txtCode_Leave
        private void txtCode_Leave(object sender, EventArgs e)
        {
            pCurrItemCode = txtCode.Text;

            if (pCurrItemCode.Trim().ToLower() != pPrevItemCode.Trim().ToLower())
            {
                this.Display_Item();
            }
        }
        #endregion

        #endregion

        #region txtQuantity_Enter
        private void txtQuantity_Enter(object sender, EventArgs e)
        {
            txtQuantity.SelectAll();
        }
        #endregion

        #region Accept_Item
        private void Accept_Item()
        {
            string mFunctionName = "Accept_Item";

            try
            {
                DataTable mDtProducts = pMdtSomProducts.View(
                    "code='" + txtCode.Text.Trim() + "' and visible_" + pStoreCode.Trim() + "=1", "", "", "");

                if (mDtProducts.Rows.Count == 0)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_ProductCodeIsInvalid.ToString());
                    txtCode.Focus();
                    return;
                }

                if (Program.IsMoney(txtQuantity.Text) == false)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IV_OrderQtyIsInvalid.ToString());
                    txtQuantity.Focus();
                    return;
                }

                double mQuantity = Convert.ToDouble(txtQuantity.Text);

                switch (pSourceForm.Name.ToLower())
                {
                    case "frmivpurchaseorders": ((frmIVPurchaseOrders)pSourceForm).Add_Item(pSelectedRow, mQuantity); break;
                    case "frmivtransferins": ((frmIVTransferIns)pSourceForm).Add_Item(pSelectedRow, mQuantity); break;
                    case "frmivtransferinsstore": ((frmIVTransferInsStore)pSourceForm).Add_Item(pSelectedRow, mQuantity); break;
                    case "frmivtransferouts": ((frmIVTransferOuts)pSourceForm).Add_Item(pSelectedRow, mQuantity); break;
                    case "frmivtransferoutsstore": ((frmIVTransferOutsStore)pSourceForm).Add_Item(pSelectedRow, mQuantity); break;
                    case "frmivphysicalinventory":
                        {
                            object mExpiryDate = DBNull.Value;
                            if (Program.IsNullDate(txtExpiryDate.EditValue) == false)
                            {
                                mExpiryDate = Convert.ToDateTime(txtExpiryDate.EditValue);
                            }
                            ((frmIVPhysicalInventory)pSourceForm).Add_Item(pSelectedRow, mExpiryDate, mQuantity);
                        }
                        break;
                }

                txtCode.Text = "";
                txtDescription.Text = "";
                txtQuantity.Text = "";
                lstExpiryDates.Items.Clear();
                txtExpiryDate.EditValue = DBNull.Value;
                txtCode.Focus();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region txtQuantity_KeyDown
        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Accept_Item();
            }
        }
        #endregion

        #region cmdAccept_Click
        private void cmdAccept_Click(object sender, EventArgs e)
        {
            this.Accept_Item();
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region lstExpiryDates_SelectedIndexChanged
        private void lstExpiryDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstExpiryDates.Items.Count == 0)
            {
                return;
            }

            if (Program.IsNullDate(lstExpiryDates.Items[lstExpiryDates.SelectedIndex]) == true)
            {
                txtExpiryDate.EditValue = DBNull.Value;
            }
            else
            {
                txtExpiryDate.EditValue = Convert.ToDateTime(lstExpiryDates.Items[lstExpiryDates.SelectedIndex]);
            }
        }
        #endregion
    }
}