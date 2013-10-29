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
using System.IO;
using System.Xml.Serialization;

namespace AfyaPro_NextGen
{
    public partial class frmBLSItems : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsBillItemGroups pMdtBillItemGroups;
        private AfyaPro_MT.clsBillItemSubGroups pMdtBillItemSubGroups;
        private AfyaPro_MT.clsBillItems pMdtBillItems;
        private AfyaPro_MT.clsPriceCategories pMdtPriceCategories;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataTable pDtGroups = new DataTable("groups");
        private DataTable pDtSubGroups = new DataTable("subgroups");
        private DataTable pDtActivePrices = new DataTable("pricecategories");
        private DataRow pSelectedRow = null;
        internal string gDataState = "";
        private bool pFirstTimeLoad = true;

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        #endregion

        #region frmBLSItems
        public frmBLSItems()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmBLSItems";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                this.CancelButton = cmdClose;

                pMdtBillItemGroups = (AfyaPro_MT.clsBillItemGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsBillItemGroups),
                    Program.gMiddleTier + "clsBillItemGroups");

                pMdtBillItemSubGroups = (AfyaPro_MT.clsBillItemSubGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsBillItemSubGroups),
                    Program.gMiddleTier + "clsBillItemSubGroups");

                pMdtBillItems = (AfyaPro_MT.clsBillItems)Activator.GetObject(
                    typeof(AfyaPro_MT.clsBillItems),
                    Program.gMiddleTier + "clsBillItems");

                pMdtPriceCategories = (AfyaPro_MT.clsPriceCategories)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPriceCategories),
                    Program.gMiddleTier + "clsPriceCategories");

                pMdtAutoCodes = (AfyaPro_MT.clsAutoCodes)Activator.GetObject(
                    typeof(AfyaPro_MT.clsAutoCodes),
                    Program.gMiddleTier + "clsAutoCodes");

                pDtGroups.Columns.Add("code", typeof(System.String));
                pDtGroups.Columns.Add("description", typeof(System.String));
                cboGroup.Properties.DataSource = pDtGroups;
                cboGroup.Properties.DisplayMember = "description";
                cboGroup.Properties.ValueMember = "code";

                pDtSubGroups.Columns.Add("code", typeof(System.String));
                pDtSubGroups.Columns.Add("description", typeof(System.String));
                cboSubGroup.Properties.DataSource = pDtSubGroups;
                cboSubGroup.Properties.DisplayMember = "description";
                cboSubGroup.Properties.ValueMember = "code";

                this.Fill_LookupData();



                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.blsitems_customizelayout.ToString());
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmBLSItems_Load
        private void frmBLSItems_Load(object sender, EventArgs e)
        {
            switch (gDataState.Trim().ToLower())
            {
                case "new": Mode_New(); break;
                case "edit": Mode_Edit(); break;
            }

            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);
            this.Load_Controls();
            this.Load_PriceCategories();

            this.pFormWidth = this.Width;
            this.pFormHeight = this.Height;

            Program.Center_Screen(this);
        }
        #endregion

        #region frmBLSItems_Activated
        private void frmBLSItems_Activated(object sender, EventArgs e)
        {
            if (pFirstTimeLoad == true)
            {
                if (gDataState.Trim().ToLower() == "new")
                {
                    if (txtCode.Text.Trim().ToLower() == "<<---new--->>")
                    {
                        txtDescription.Focus();
                    }
                    else
                    {
                        txtCode.Focus();
                    }
                }
                else
                {
                    txtDescription.Focus();
                }
                pFirstTimeLoad = false;
            }
        }
        #endregion

        #region frmBLSItems_FormClosing
        private void frmBLSItems_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //layout
                if (layoutControl1.IsModified == true)
                {
                    Program.Save_FormLayout(this, layoutControl1, this.Name);
                }
            }
            catch { }
        }
        #endregion

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(txbGroup);
            mObjectsList.Add(txbSubGroup);
            mObjectsList.Add(txbCode);
            mObjectsList.Add(txbDescription);
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
                pDtActivePrices = pMdtPriceCategories.View_Active(Program.gLanguageName, "frmBLSPriceCategories");
                DataView mDvActivePrices = new DataView();
                mDvActivePrices.Table = pDtActivePrices;
                mDvActivePrices.Sort = "pricename";

                for (int mPriceCount = 0; mPriceCount < 10; mPriceCount++)
                {
                    DevExpress.XtraEditors.TextEdit mTextBox =
                        (DevExpress.XtraEditors.TextEdit)layoutControl1.GetControlByName("txtprice" + (mPriceCount + 1));

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
                mGridView.OptionsView.ShowGroupPanel = true;
                mGridControl.MainView = mGridView;
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
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.districtcode));
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
                    cboGroup.ItemIndex = Program.Get_LookupItemIndex(cboGroup, "code", pSelectedRow["groupcode"].ToString());
                    cboSubGroup.ItemIndex = Program.Get_LookupItemIndex(cboSubGroup, "code", pSelectedRow["subgroupcode"].ToString());
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

                cboGroup.EditValue = null;
                cboSubGroup.EditValue = null;
                this.Data_Clear();
                pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                cboGroup.ItemIndex = Program.Get_LookupItemIndex(cboGroup, "code", pSelectedRow["groupcode"].ToString());
                cboSubGroup.ItemIndex = Program.Get_LookupItemIndex(cboSubGroup, "code", pSelectedRow["subgroupcode"].ToString());
                txtCode.Text = pSelectedRow["code"].ToString();
                txtDescription.Text = pSelectedRow["description"].ToString();
                txtDefaultQty.Text = pSelectedRow["defaultqty"].ToString();
                chkAddToCart.Checked = Convert.ToBoolean(pSelectedRow["addtocart"]);
                chkPrintBarcode.Checked = Convert.ToBoolean(pSelectedRow["printbarcode"]);
                chkForIpdAdmission.Checked = Convert.ToBoolean(pSelectedRow["foripdadmission"]);
                txtprice1.Text = pSelectedRow["price1"].ToString();
                txtprice2.Text = pSelectedRow["price2"].ToString();
                txtprice3.Text = pSelectedRow["price3"].ToString();
                txtprice4.Text = pSelectedRow["price4"].ToString();
                txtprice5.Text = pSelectedRow["price5"].ToString();
                txtprice6.Text = pSelectedRow["price6"].ToString();
                txtprice7.Text = pSelectedRow["price7"].ToString();
                txtprice8.Text = pSelectedRow["price8"].ToString();
                txtprice9.Text = pSelectedRow["price9"].ToString();
                txtprice10.Text = pSelectedRow["price10"].ToString();

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

            txtDefaultQty.Text = "";
            chkAddToCart.Checked = false;
            chkPrintBarcode.Checked = false;
            chkForIpdAdmission.Checked = false;

            txtprice1.Text = "0";
            txtprice2.Text = "0";
            txtprice3.Text = "0";
            txtprice4.Text = "0";
            txtprice5.Text = "0";
            txtprice6.Text = "0";
            txtprice7.Text = "0";
            txtprice8.Text = "0";
            txtprice9.Text = "0";
            txtprice10.Text = "0";
        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            DataRow mNewRow;
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region Groups

                pDtGroups.Rows.Clear();
                DataTable mDtGroups = pMdtBillItemGroups.View("", "code", Program.gLanguageName, "grdBLSItemGroups");
                foreach (DataRow mDataRow in mDtGroups.Rows)
                {
                    mNewRow = pDtGroups.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtGroups.Rows.Add(mNewRow);
                    pDtGroups.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtGroups.Columns)
                {
                    mDataColumn.Caption = mDtGroups.Columns[mDataColumn.ColumnName].Caption;
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

        #region cboGroup_EditValueChanged
        void cboGroup_EditValueChanged(object sender, EventArgs e)
        {
            string mFunctionName = "cboGroup_EditValueChanged";

            try
            {
                pDtSubGroups.Rows.Clear();
                if (cboGroup.ItemIndex == -1)
                {
                    return;
                }

                string mGroupCode = cboGroup.GetColumnValue("code").ToString().Trim();

                DataTable mDtSubGroups = pMdtBillItemSubGroups.View(
                    "groupcode='" + mGroupCode + "'", "code", Program.gLanguageName, "grdBLSItemSubGroups");
                foreach (DataRow mDataRow in mDtSubGroups.Rows)
                {
                    DataRow mNewRow = pDtSubGroups.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtSubGroups.Rows.Add(mNewRow);
                    pDtSubGroups.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtSubGroups.Columns)
                {
                    mDataColumn.Caption = mDtSubGroups.Columns[mDataColumn.ColumnName].Caption;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
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
                DataTable mDtDistricts = pMdtBillItems.View("", "", Program.gLanguageName, mGridControl.Name);
                mGridControl.DataSource = mDtDistricts;

                //expand all groups
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)mGridControl.MainView;
                mGridView.ExpandAllGroups();
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
            int mDefaultQty = 0;
            double mPrice1 = 0;
            double mPrice2 = 0;
            double mPrice3 = 0;
            double mPrice4 = 0;
            double mPrice5 = 0;
            double mPrice6 = 0;
            double mPrice7 = 0;
            double mPrice8 = 0;
            double mPrice9 = 0;
            double mPrice10 = 0;

            Int16 mGenerateCode = 0;
            String mFunctionName = "Data_New";

            #region validation
            if (cboGroup.ItemIndex == -1)
            {
                Program.Display_Error("Invalid item group");
                cboGroup.Focus();
                return;
            }

            if (cboSubGroup.ItemIndex == -1)
            {
                Program.Display_Error("Invalid item sub group");
                cboSubGroup.Focus();
                return;
            }

            if (txtCode.Text.Trim() == "" && txtCode.Text.Trim().ToLower() != "<<---new--->>")
            {
                Program.Display_Error("Invalid code");
                txtCode.Focus();
                return;
            }

            if (txtDescription.Text.Trim() == "")
            {
                Program.Display_Error("Invalid description");
                txtDescription.Focus();
                return;
            }

            #region prices

            DataView mDvActivePrices = new DataView();
            mDvActivePrices.Table = pDtActivePrices;
            mDvActivePrices.Sort = "pricename";
            if (mDvActivePrices.Find("price1") >= 0)
            {
                if (Program.IsMoney(txtprice1.Text) == false)
                {
                    Program.Display_Error("Invalid entry");
                    txtprice1.Focus();
                    return;
                }
                mPrice1 = Convert.ToDouble(txtprice1.Text);
            }

            if (mDvActivePrices.Find("price2") >= 0)
            {
                if (Program.IsMoney(txtprice2.Text) == false)
                {
                    Program.Display_Error("Invalid entry");
                    txtprice2.Focus();
                    return;
                }
                mPrice2 = Convert.ToDouble(txtprice2.Text);
            }

            if (mDvActivePrices.Find("price3") >= 0)
            {
                if (Program.IsMoney(txtprice3.Text) == false)
                {
                    Program.Display_Error("Invalid entry");
                    txtprice3.Focus();
                    return;
                }
                mPrice3 = Convert.ToDouble(txtprice3.Text);
            }

            if (mDvActivePrices.Find("price4") >= 0)
            {
                if (Program.IsMoney(txtprice4.Text) == false)
                {
                    Program.Display_Error("Invalid entry");
                    txtprice4.Focus();
                    return;
                }
                mPrice4 = Convert.ToDouble(txtprice4.Text);
            }

            if (mDvActivePrices.Find("price5") >= 0)
            {
                if (Program.IsMoney(txtprice5.Text) == false)
                {
                    Program.Display_Error("Invalid entry");
                    txtprice5.Focus();
                    return;
                }
                mPrice5 = Convert.ToDouble(txtprice5.Text);
            }

            if (mDvActivePrices.Find("price6") >= 0)
            {
                if (Program.IsMoney(txtprice6.Text) == false)
                {
                    Program.Display_Error("Invalid entry");
                    txtprice6.Focus();
                    return;
                }
                mPrice6 = Convert.ToDouble(txtprice6.Text);
            }

            if (mDvActivePrices.Find("price7") >= 0)
            {
                if (Program.IsMoney(txtprice7.Text) == false)
                {
                    Program.Display_Error("Invalid entry");
                    txtprice7.Focus();
                    return;
                }
                mPrice7 = Convert.ToDouble(txtprice7.Text);
            }

            if (mDvActivePrices.Find("price8") >= 0)
            {
                if (Program.IsMoney(txtprice8.Text) == false)
                {
                    Program.Display_Error("Invalid entry");
                    txtprice8.Focus();
                    return;
                }
                mPrice8 = Convert.ToDouble(txtprice8.Text);
            }

            if (mDvActivePrices.Find("price9") >= 0)
            {
                if (Program.IsMoney(txtprice9.Text) == false)
                {
                    Program.Display_Error("Invalid entry");
                    txtprice9.Focus();
                    return;
                }
                mPrice9 = Convert.ToDouble(txtprice9.Text);
            }

            if (mDvActivePrices.Find("price10") >= 0)
            {
                if (Program.IsMoney(txtprice10.Text) == false)
                {
                    Program.Display_Error("Invalid entry");
                    txtprice10.Focus();
                    return;
                }
                mPrice10 = Convert.ToDouble(txtprice10.Text);
            }

            #endregion

            #endregion

            try
            {
                if (txtCode.Text.Trim().ToLower() == "<<---new--->>")
                {
                    mGenerateCode = 1;
                }

                if (Program.IsNumeric(txtDefaultQty.Text) == true)
                {
                    mDefaultQty = Convert.ToInt32(txtDefaultQty.Text);
                }

                //add
                pResult = pMdtBillItems.Add(mGenerateCode, txtCode.Text, txtDescription.Text,
                    cboGroup.GetColumnValue("code").ToString(), cboSubGroup.GetColumnValue("code").ToString(),
                    mPrice1, mPrice2, mPrice3, mPrice4, mPrice5, mPrice6, mPrice7, mPrice8, mPrice9,
                    mPrice10, mDefaultQty, Convert.ToInt16(chkAddToCart.Checked),
                    Convert.ToInt16(chkPrintBarcode.Checked), Convert.ToInt16(chkForIpdAdmission.Checked), Program.gCurrentUser.Code);
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
            int mDefaultQty = 0;
            double mPrice1 = 0;
            double mPrice2 = 0;
            double mPrice3 = 0;
            double mPrice4 = 0;
            double mPrice5 = 0;
            double mPrice6 = 0;
            double mPrice7 = 0;
            double mPrice8 = 0;
            double mPrice9 = 0;
            double mPrice10 = 0;

            String mFunctionName = "Data_Edit";

            #region validation
            if (cboGroup.ItemIndex == -1)
            {
                Program.Display_Error("Invalid item group");
                cboGroup.Focus();
                return;
            }

            if (cboSubGroup.ItemIndex == -1)
            {
                Program.Display_Error("Invalid item sub group");
                cboSubGroup.Focus();
                return;
            }

            if (txtCode.Text.Trim() == "")
            {
                Program.Display_Error("Invalid code");
                txtCode.Focus();
                return;
            }

            if (txtDescription.Text.Trim() == "")
            {
                Program.Display_Error("Invalid description");
                txtDescription.Focus();
                return;
            }

            #region prices

            DataView mDvActivePrices = new DataView();
            mDvActivePrices.Table = pDtActivePrices;
            mDvActivePrices.Sort = "pricename";
            if (mDvActivePrices.Find("price1") >= 0)
            {
                if (Program.IsMoney(txtprice1.Text) == false)
                {
                    Program.Display_Error("Invalid entry");
                    txtprice1.Focus();
                    return;
                }
                mPrice1 = Convert.ToDouble(txtprice1.Text);
            }

            if (mDvActivePrices.Find("price2") >= 0)
            {
                if (Program.IsMoney(txtprice2.Text) == false)
                {
                    Program.Display_Error("Invalid entry");
                    txtprice2.Focus();
                    return;
                }
                mPrice2 = Convert.ToDouble(txtprice2.Text);
            }

            if (mDvActivePrices.Find("price3") >= 0)
            {
                if (Program.IsMoney(txtprice3.Text) == false)
                {
                    Program.Display_Error("Invalid entry");
                    txtprice3.Focus();
                    return;
                }
                mPrice3 = Convert.ToDouble(txtprice3.Text);
            }

            if (mDvActivePrices.Find("price4") >= 0)
            {
                if (Program.IsMoney(txtprice4.Text) == false)
                {
                    Program.Display_Error("Invalid entry");
                    txtprice4.Focus();
                    return;
                }
                mPrice4 = Convert.ToDouble(txtprice4.Text);
            }

            if (mDvActivePrices.Find("price5") >= 0)
            {
                if (Program.IsMoney(txtprice5.Text) == false)
                {
                    Program.Display_Error("Invalid entry");
                    txtprice5.Focus();
                    return;
                }
                mPrice5 = Convert.ToDouble(txtprice5.Text);
            }

            if (mDvActivePrices.Find("price6") >= 0)
            {
                if (Program.IsMoney(txtprice6.Text) == false)
                {
                    Program.Display_Error("Invalid entry");
                    txtprice6.Focus();
                    return;
                }
                mPrice6 = Convert.ToDouble(txtprice6.Text);
            }

            if (mDvActivePrices.Find("price7") >= 0)
            {
                if (Program.IsMoney(txtprice7.Text) == false)
                {
                    Program.Display_Error("Invalid entry");
                    txtprice7.Focus();
                    return;
                }
                mPrice7 = Convert.ToDouble(txtprice7.Text);
            }

            if (mDvActivePrices.Find("price8") >= 0)
            {
                if (Program.IsMoney(txtprice8.Text) == false)
                {
                    Program.Display_Error("Invalid entry");
                    txtprice8.Focus();
                    return;
                }
                mPrice8 = Convert.ToDouble(txtprice8.Text);
            }

            if (mDvActivePrices.Find("price9") >= 0)
            {
                if (Program.IsMoney(txtprice9.Text) == false)
                {
                    Program.Display_Error("Invalid entry");
                    txtprice9.Focus();
                    return;
                }
                mPrice9 = Convert.ToDouble(txtprice9.Text);
            }

            if (mDvActivePrices.Find("price10") >= 0)
            {
                if (Program.IsMoney(txtprice10.Text) == false)
                {
                    Program.Display_Error("Invalid entry");
                    txtprice10.Focus();
                    return;
                }
                mPrice10 = Convert.ToDouble(txtprice10.Text);
            }

            #endregion

            #endregion

            try
            {
                if (Program.IsNumeric(txtDefaultQty.Text) == true)
                {
                    mDefaultQty = Convert.ToInt32(txtDefaultQty.Text);
                }

                //edit 
                pResult = pMdtBillItems.Edit(txtCode.Text, txtDescription.Text,
                    mPrice1, mPrice2, mPrice3, mPrice4, mPrice5, mPrice6, mPrice7, mPrice8, mPrice9,
                    mPrice10, mDefaultQty, Convert.ToInt16(chkAddToCart.Checked),
                    Convert.ToInt16(chkPrintBarcode.Checked), Convert.ToInt16(chkForIpdAdmission.Checked), Program.gCurrentUser.Code);
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

                DialogResult mResp = Program.Display_Question("Delete '"
                    + pSelectedRow["code"].ToString().Trim() + "'   '"
                    + pSelectedRow["description"].ToString().Trim() + "'", MessageBoxDefaultButton.Button2);

                if (mResp != DialogResult.Yes)
                {
                    return;
                }

                //add 
                pResult = pMdtBillItems.Delete(pSelectedRow["code"].ToString().Trim(), Program.gCurrentUser.Code);
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
    }
}