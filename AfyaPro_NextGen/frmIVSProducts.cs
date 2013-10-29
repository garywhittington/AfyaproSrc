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

namespace AfyaPro_NextGen
{
    public partial class frmIVSProducts : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsPriceCategories pMdtPriceCategories;
        private AfyaPro_MT.clsSomStores pMdtSomStores;
        private AfyaPro_MT.clsSomSuppliers pMdtSomSuppliers;
        private AfyaPro_MT.clsSomProductCategories pMdtSomProductCategories;
        private AfyaPro_MT.clsSomPackagings pMdtSomPackagings;
        private AfyaPro_MT.clsSomProducts pMdtSomProducts;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataRow pSelectedRow = null;
        internal string gDataState = "";

        private DataTable pDtStores = new DataTable("stores");
        private DataTable pDtPackagings = new DataTable("packagings");
        private DataTable pDtDepartments = new DataTable("departments");

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private bool pStoreCheckedCheckBox = false;
        private bool pStoreCheckedGrid = false;

        #endregion

        #region frmIVSProducts
        public frmIVSProducts()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmIVSProducts";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                this.CancelButton = cmdClose;

                pMdtPriceCategories = (AfyaPro_MT.clsPriceCategories)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPriceCategories),
                    Program.gMiddleTier + "clsPriceCategories");

                pMdtSomStores = (AfyaPro_MT.clsSomStores)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomStores),
                    Program.gMiddleTier + "clsSomStores");

                pMdtSomSuppliers = (AfyaPro_MT.clsSomSuppliers)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomSuppliers),
                    Program.gMiddleTier + "clsSomSuppliers");

                pMdtSomPackagings = (AfyaPro_MT.clsSomPackagings)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomPackagings),
                    Program.gMiddleTier + "clsSomPackagings");

                pMdtSomProductCategories = (AfyaPro_MT.clsSomProductCategories)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomProductCategories),
                    Program.gMiddleTier + "clsSomProductCategories");

                pMdtSomProducts = (AfyaPro_MT.clsSomProducts)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomProducts),
                    Program.gMiddleTier + "clsSomProducts");

                pMdtAutoCodes = (AfyaPro_MT.clsAutoCodes)Activator.GetObject(
                    typeof(AfyaPro_MT.clsAutoCodes),
                    Program.gMiddleTier + "clsAutoCodes");

                //stores
                pDtStores.Columns.Add("selected", typeof(System.Boolean));
                pDtStores.Columns.Add("code", typeof(System.String));
                pDtStores.Columns.Add("description", typeof(System.String));

                grdIVSProductStores.DataSource = pDtStores;

                pDtPackagings.Columns.Add("code", typeof(System.String));
                pDtPackagings.Columns.Add("description", typeof(System.String));
                pDtPackagings.Columns.Add("pieces", typeof(System.Int32));
                cboPackaging.Properties.DataSource = pDtPackagings;
                cboPackaging.Properties.DisplayMember = "description";
                cboPackaging.Properties.ValueMember = "code";

                pDtDepartments.Columns.Add("code", typeof(System.String));
                pDtDepartments.Columns.Add("description", typeof(System.String));
                cboDepartment.Properties.DataSource = pDtDepartments;
                cboDepartment.Properties.DisplayMember = "description";
                cboDepartment.Properties.ValueMember = "code";

                grdIVSProductStores.ForceInitialize();

                this.Fill_LookupData();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmIVSProducts_Load
        private void frmIVSProducts_Load(object sender, EventArgs e)
        {
            switch (gDataState.Trim().ToLower())
            {
                case "new": Mode_New(); break;
                case "edit": Mode_Edit(); break;
            }

            this.Load_Controls();

            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);
            Program.Restore_GridLayout(grdIVSProductStores, grdIVSProductStores.Name);

            this.Load_PriceCategories();

            this.pFormWidth = this.Width;
            this.pFormHeight = this.Height;

            Program.Center_Screen(this);
        }
        #endregion

        #region frmIVSProducts_FormClosing
        private void frmIVSProducts_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //layout
                if (layoutControl1.IsModified == true)
                {
                    Program.Save_FormLayout(this, layoutControl1, this.Name);
                }

                //grid
                Program.Save_GridLayout(grdIVSProductStores, grdIVSProductStores.Name);
            }
            catch { }
        }
        #endregion

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(txbCategory);
            mObjectsList.Add(txbCode);
            mObjectsList.Add(txbDescription);
            mObjectsList.Add(txbOPMCode);
            mObjectsList.Add(txbOPMDescription);
            mObjectsList.Add(txbPackaging);
            mObjectsList.Add(txbCostPrice);
            mObjectsList.Add(txbPrice1);
            mObjectsList.Add(txbPrice2);
            mObjectsList.Add(txbPrice3);
            mObjectsList.Add(txbPrice4);
            mObjectsList.Add(txbPrice5);
            mObjectsList.Add(txbPrice6);
            mObjectsList.Add(txbPrice7);
            mObjectsList.Add(txbPrice8);
            mObjectsList.Add(txbPrice9);
            mObjectsList.Add(txbPrice10);
            mObjectsList.Add(code);
            mObjectsList.Add(description);
            mObjectsList.Add(cmdOk);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region Load_PriceCategories
        private void Load_PriceCategories()
        {
            string mFunctionName = "Load_PriceCategories";

            try
            {
                DataTable mDtActivePrices = pMdtPriceCategories.View_Active(Program.gLanguageName, "frmBLSPriceCategories");
                DataView mDvActivePrices = new DataView();
                mDvActivePrices.Table = mDtActivePrices;
                mDvActivePrices.Sort = "pricename";

                for (int mPriceCount = 0; mPriceCount < 10; mPriceCount++)
                {
                    DevExpress.XtraEditors.TextEdit mTextBox =
                        (DevExpress.XtraEditors.TextEdit)layoutControl1.GetControlByName("txtPrice" + (mPriceCount + 1));

                    int mRowIndex = mDvActivePrices.Find("price" + (mPriceCount + 1));
                    if (mRowIndex >= 0)
                    {
                        DevExpress.XtraLayout.LayoutControlItem mLayoutControlItem =
                            (DevExpress.XtraLayout.LayoutControlItem)layoutControl1.GetItemByControl(mTextBox);
                        mLayoutControlItem.Text = mDvActivePrices[mRowIndex]["pricedescription"].ToString().Trim();
                        mLayoutControlItem.CustomizationFormText = mDvActivePrices[mRowIndex]["pricedescription"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Grid_Settings
        internal void Grid_Settings(GridControl mGridControl)
        {
            string mFunctionName = "Grid_Settings";

            try
            {
                if (mGridControl.Visible == false)
                {
                    mGridControl.Visible = true;
                }

                mGridControl.DataSource = null;

                //prepare grid view
                DevExpress.XtraGrid.Views.Grid.GridView mGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
                mGridView.OptionsBehavior.Editable = false;
                mGridView.OptionsView.ShowGroupPanel = false;
                mGridControl.MainView = mGridView;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region Departments
                pDtDepartments.Rows.Clear();
                DataTable mDtDepartments = pMdtSomProductCategories.View("", "description", Program.gLanguageName, "grdIVSProductCategories");
                foreach (DataRow mDataRow in mDtDepartments.Rows)
                {
                    DataRow mNewRow = pDtDepartments.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtDepartments.Rows.Add(mNewRow);
                    pDtDepartments.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtDepartments.Columns)
                {
                    mDataColumn.Caption = mDtDepartments.Columns[mDataColumn.ColumnName].Caption;
                }

                #endregion

                #region Packagings
                pDtPackagings.Rows.Clear();
                DataTable mDtPackagings = pMdtSomPackagings.View("", "description", Program.gLanguageName, "grdIVSPackagings");
                foreach (DataRow mDataRow in mDtPackagings.Rows)
                {
                    DataRow mNewRow = pDtPackagings.NewRow();
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

                DataTable mDtStores = pMdtSomStores.View("", "description", "", "");

                pDtStores.Rows.Clear();
                foreach (DataRow mDataRow in mDtStores.Rows)
                {
                    DataRow mNewRow = pDtStores.NewRow();
                    mNewRow["selected"] = false;
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtStores.Rows.Add(mNewRow);
                    pDtStores.AcceptChanges();
                }

                DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit mStoreCheckEdit =
                    new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
                mStoreCheckEdit.CheckedChanged += new EventHandler(mStoreCheckEdit_CheckedChanged);
                viewStores.Columns["selected"].ColumnEdit = mStoreCheckEdit;

                if (pDtStores.Rows.Count > 0)
                {
                    chkAllStores.Checked = true;
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

        #region Mode_New
        private void Mode_New()
        {
            String mFunctionName = "Mode_New";

            try
            {
                Int16 mGenerateCode = pMdtAutoCodes.Auto_Generate_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.suppliercode));
                if (mGenerateCode == -1)
                {
                    Program.Display_Server_Error("");
                    return;
                }

                txtCode.Text = "";
                this.Data_Clear();

                #region display owning groups if any
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain.MainView;

                if (mGridView.FocusedRowHandle >= 0)
                {
                    pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);
                    cboDepartment.ItemIndex = Program.Get_LookupItemIndex(cboDepartment, "code", pSelectedRow["departmentcode"].ToString());
                }
                #endregion

                if (mGenerateCode == 1)
                {
                    txtCode.Text = "<<---New--->>";
                    txtCode.Enabled = false;
                    txtDescription.Focus();
                }
                else
                {
                    txtCode.Enabled = true;
                    txtCode.Focus();
                }

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
                    (DevExpress.XtraGrid.Views.Grid.GridView)((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                this.Data_Clear();
                pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                cboDepartment.ItemIndex = Program.Get_LookupItemIndex(cboDepartment, "code", pSelectedRow["departmentcode"].ToString());
                txtCode.Text = pSelectedRow["code"].ToString();
                txtDescription.Text = pSelectedRow["description"].ToString();
                txtDisplayName.Text = pSelectedRow["displayname"].ToString();
                txtOPMCode.Text = pSelectedRow["opmcode"].ToString();
                txtOPMDescription.Text = pSelectedRow["opmdescription"].ToString();
                cboPackaging.ItemIndex = Program.Get_LookupItemIndex(cboPackaging, "code", pSelectedRow["packagingcode"].ToString());
                chkHasExpiry.Checked = Convert.ToBoolean(pSelectedRow["hasexpiry"]);
                txtExpiryNotify.Text = pSelectedRow["expirynotice"].ToString();
                txtMinLevel.Text = pSelectedRow["minlevel"].ToString();
                txtOrderQty.Text = pSelectedRow["orderqty"].ToString();
                txtCostPrice.Text = pSelectedRow["costprice"].ToString();
                txtPrice1.Text = pSelectedRow["price1"].ToString();
                txtPrice2.Text = pSelectedRow["price2"].ToString();
                txtPrice3.Text = pSelectedRow["price3"].ToString();
                txtPrice4.Text = pSelectedRow["price4"].ToString();
                txtPrice5.Text = pSelectedRow["price5"].ToString();
                txtPrice6.Text = pSelectedRow["price6"].ToString();
                txtPrice7.Text = pSelectedRow["price7"].ToString();
                txtPrice8.Text = pSelectedRow["price8"].ToString();
                txtPrice9.Text = pSelectedRow["price9"].ToString();
                txtPrice10.Text = pSelectedRow["price10"].ToString();

                foreach (DataRow mDataRow in pDtStores.Rows)
                {
                    mDataRow.BeginEdit();
                    if (Convert.ToInt16(pSelectedRow["visible_" + mDataRow["code"].ToString().Trim()]) == 1)
                    {
                        mDataRow["selected"] = true;
                    }
                    else
                    {
                        mDataRow["selected"] = false;
                    }
                    mDataRow.EndEdit();
                    pDtStores.AcceptChanges();
                }

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
            txtDescription.Text = "";
            txtDisplayName.Text = "";
            txtOPMCode.Text = "";
            txtOPMDescription.Text = "";
            cboPackaging.EditValue = null;
            chkHasExpiry.Checked = false;
            txtExpiryNotify.Text = "";
            txtCostPrice.Text = "";
            txtPrice1.Text = "";
            txtPrice2.Text = "";
            txtPrice3.Text = "";
            txtPrice4.Text = "";
            txtPrice5.Text = "";
            txtPrice6.Text = "";
            txtPrice7.Text = "";
            txtPrice8.Text = "";
            txtPrice9.Text = "";
            txtPrice10.Text = "";
            txtMinLevel.Text = "";
            txtOrderQty.Text = "";

            foreach (DataRow mDataRow in pDtStores.Rows)
            {
                mDataRow.BeginEdit();
                mDataRow["selected"] = true;
                mDataRow.EndEdit();
                pDtStores.AcceptChanges();
            }
        }
        #endregion

        #region Data_Fill
        internal void Data_Fill(GridControl mGridControl)
        {
            string mFunctionName = "Data_Fill";

            try
            {
                //load data
                DataTable mDtSuppliers = pMdtSomProducts.View("", "", Program.gLanguageName, mGridControl.Name);
                mGridControl.DataSource = mDtSuppliers;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Data_New
        private void Data_New()
        {
            Int16 mGenerateCode = 0;

            int mExpiryNotice = 0;
            double mCostPrice = 0;
            double[] mPrices = new double[10];

            String mFunctionName = "Data_New";

            #region validation
            if (cboDepartment.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_ProductCategoryDescriptionIsInvalid.ToString());
                cboDepartment.Focus();
                return;
            }
            if (txtCode.Text.Trim() == "" && txtCode.Text.Trim().ToLower() != "<<---new--->>")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_ProductCodeIsInvalid.ToString());
                txtCode.Focus();
                return;
            }

            if (txtDescription.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_ProductDescriptionIsInvalid.ToString());
                txtDescription.Focus();
                return;
            }
            if (cboPackaging.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_PackagingDescriptionIsInvalid.ToString());
                cboPackaging.Focus();
                return;
            }

            if (txtCostPrice.Visible == true)
            {
                if (Program.IsMoney(txtCostPrice.Text) == false)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_EntryIsInvalid.ToString());
                    txtCostPrice.Focus();
                    return;
                }
                mCostPrice = Convert.ToDouble(txtCostPrice.Text);
            }

            //prices 1 to 10
            for (int mPriceCount = 0; mPriceCount < 10; mPriceCount++)
            {
                DevExpress.XtraEditors.TextEdit mTextBox =
                    (DevExpress.XtraEditors.TextEdit)layoutControl1.GetControlByName("txtPrice" + (mPriceCount + 1));

                if (mTextBox.Visible == true)
                {
                    if (Program.IsMoney(mTextBox.Text) == false)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_EntryIsInvalid.ToString());
                        mTextBox.Focus();
                        return;
                    }
                    mPrices[mPriceCount] = Convert.ToDouble(mTextBox.Text);
                }
            }

            #endregion

            try
            {
                if (txtCode.Text.Trim().ToLower() == "<<---new--->>")
                {
                    mGenerateCode = 1;
                }

                if (Program.IsMoney(txtExpiryNotify.Text) == true)
                {
                    mExpiryNotice = Convert.ToInt32(txtExpiryNotify.Text);
                }

                double mMinLevel = 0;
                double mOrderQty = 0;

                if (Program.IsMoney(txtMinLevel.Text) == true)
                {
                    mMinLevel = Convert.ToDouble(txtMinLevel.Text);
                }
                if (Program.IsMoney(txtOrderQty.Text) == true)
                {
                    mOrderQty = Convert.ToDouble(txtOrderQty.Text);
                }

                //add
                pResult = pMdtSomProducts.Add(mGenerateCode, txtCode.Text, txtDescription.Text, txtOPMCode.Text, txtOPMDescription.Text,
                    txtDisplayName.Text,
                     cboDepartment.GetColumnValue("code").ToString(),
                     cboDepartment.GetColumnValue("description").ToString(),
                     cboPackaging.GetColumnValue("code").ToString(),
                     cboPackaging.GetColumnValue("description").ToString(),
                     Convert.ToInt32(cboPackaging.GetColumnValue("pieces")),
                     Convert.ToInt16(chkHasExpiry.Checked), mExpiryNotice,
                     mCostPrice, mPrices[0], mPrices[1], mPrices[2], mPrices[3],
                     mPrices[4], mPrices[5], mPrices[6], mPrices[7], mPrices[8],
                     mPrices[9], mMinLevel, mOrderQty, pDtStores, Program.gCurrentUser.Code);
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
                this.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
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
            int mExpiryNotice = 0;
            double mCostPrice = 0;
            double[] mPrices = new double[10];

            String mFunctionName = "Data_Edit";

            #region validation
            if (cboDepartment.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_ProductCategoryDescriptionIsInvalid.ToString());
                cboDepartment.Focus();
                return;
            }
            if (txtCode.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_ProductCodeIsInvalid.ToString());
                txtCode.Focus();
                return;
            }

            if (txtDescription.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_ProductDescriptionIsInvalid.ToString());
                txtDescription.Focus();
                return;
            }

            if (cboPackaging.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_PackagingDescriptionIsInvalid.ToString());
                cboPackaging.Focus();
                return;
            }

            if (txtCostPrice.Visible == true)
            {
                if (Program.IsMoney(txtCostPrice.Text) == false)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_EntryIsInvalid.ToString());
                    txtCostPrice.Focus();
                    return;
                }
                mCostPrice = Convert.ToDouble(txtCostPrice.Text);
            }

            //prices 1 to 10
            for (int mPriceCount = 0; mPriceCount < 10; mPriceCount++)
            {
                DevExpress.XtraEditors.TextEdit mTextBox =
                    (DevExpress.XtraEditors.TextEdit)layoutControl1.GetControlByName("txtPrice" + (mPriceCount + 1));

                if (mTextBox.Visible == true)
                {
                    if (Program.IsMoney(mTextBox.Text) == false)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_EntryIsInvalid.ToString());
                        mTextBox.Focus();
                        return;
                    }
                    mPrices[mPriceCount] = Convert.ToDouble(mTextBox.Text);
                }
            }
            #endregion

            try
            {
                if (Program.IsMoney(txtExpiryNotify.Text) == true)
                {
                    mExpiryNotice = Convert.ToInt32(txtExpiryNotify.Text);
                }

                double mMinLevel = 0;
                double mOrderQty = 0;

                if (Program.IsMoney(txtMinLevel.Text) == true)
                {
                    mMinLevel = Convert.ToDouble(txtMinLevel.Text);
                }
                if (Program.IsMoney(txtOrderQty.Text) == true)
                {
                    mOrderQty = Convert.ToDouble(txtOrderQty.Text);
                }

                //edit
                pResult = pMdtSomProducts.Edit(txtCode.Text, txtDescription.Text, txtOPMCode.Text, txtOPMDescription.Text,
                    txtDisplayName.Text,
                     cboDepartment.GetColumnValue("code").ToString(),
                     cboDepartment.GetColumnValue("description").ToString(),
                     cboPackaging.GetColumnValue("code").ToString(),
                     cboPackaging.GetColumnValue("description").ToString(),
                     Convert.ToInt32(cboPackaging.GetColumnValue("pieces")),
                     Convert.ToInt16(chkHasExpiry.Checked), mExpiryNotice,
                     mCostPrice, mPrices[0], mPrices[1], mPrices[2], mPrices[3],
                     mPrices[4], mPrices[5], mPrices[6], mPrices[7], mPrices[8],
                     mPrices[9], mMinLevel, mOrderQty, pDtStores, Program.gCurrentUser.Code);
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
                Program.Get_LocalCurrency();
                this.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                this.Close();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Data_Delete
        internal void Data_Delete()
        {
            String mFunctionName = "Data_Delete";

            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                DialogResult mResp = Program.Confirm_Deletion("'"
                    + pSelectedRow["code"].ToString().Trim() + "'   '"
                    + pSelectedRow["description"].ToString().Trim() + "'");

                if (mResp != DialogResult.Yes)
                {
                    return;
                }

                //delete 
                pResult = pMdtSomProducts.Delete(pSelectedRow["code"].ToString().Trim(), Program.gCurrentUser.Code);
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
                this.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
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

        #region chkAllStores_CheckedChanged
        private void chkAllStores_CheckedChanged(object sender, EventArgs e)
        {
            if (pStoreCheckedCheckBox == true)
            {
                foreach (DataRow mDataRow in pDtStores.Rows)
                {
                    mDataRow["selected"] = chkAllStores.Checked;
                }
            }
        }
        #endregion

        #region mStoreCheckEdit_CheckedChanged
        void mStoreCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (pStoreCheckedGrid == true)
            {
                grdIVSProductStores.FocusedView.PostEditor();

                int mChecked = 0;
                int mUnChecked = 0;

                foreach (DataRow mDataRow in pDtStores.Rows)
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

                if (mChecked == pDtStores.Rows.Count)
                {
                    chkAllStores.CheckState = CheckState.Checked;
                }
                else if (mUnChecked == pDtStores.Rows.Count)
                {
                    chkAllStores.CheckState = CheckState.Unchecked;
                }
                else
                {
                    chkAllStores.CheckState = CheckState.Indeterminate;
                }
            }
        }
        #endregion

        #region chkAllStores_Enter
        private void chkAllStores_Enter(object sender, EventArgs e)
        {
            pStoreCheckedGrid = false;
            pStoreCheckedCheckBox = true;
        }
        #endregion

        #region grdIVSProductStores_Enter
        private void grdIVSProductStores_Enter(object sender, EventArgs e)
        {
            pStoreCheckedCheckBox = false;
            pStoreCheckedGrid = true;
        }
        #endregion
    }
}