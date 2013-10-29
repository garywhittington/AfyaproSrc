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
using System.Text.RegularExpressions;
using DevExpress.XtraEditors.Controls;

namespace AfyaPro_NextGen
{
    public partial class frmSECUsers : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsUserGroups pMdtUserGroups;
        private AfyaPro_MT.clsClientGroups pMdtClientGroups;
        private AfyaPro_MT.clsClientSubGroups pMdtClientSubGroups;
        private AfyaPro_MT.clsLanguage pMdtLanguage;
        private AfyaPro_MT.clsPaymentTypes pMdtPaymentTypes;
        private AfyaPro_MT.clsPriceCategories pMdtPriceCategories;
        private AfyaPro_MT.clsSomStores pMdtSomStores;
        private AfyaPro_MT.clsUsers pMdtUsers;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private ComboBoxItemCollection pLanguageItems;
        private DataTable pDtUserGroups = new DataTable("usergroups");
        private DataTable pDtCustomerGroups = new DataTable("customergroups");
        private DataTable pDtCustomerSubGroups = new DataTable("customersubgroups");
        private DataTable pDtPaymentTypes = new DataTable("paymenttypes");
        private DataTable pDtPriceCategories = new DataTable("pricecategories");
        private DataTable pDtDefaultStores = new DataTable("defaultstores");
        private DataTable pDtStores = new DataTable("stores");

        private DataRow pSelectedRow = null;
        internal string gDataState = "";
        private bool pFirstTimeLoad = true;
        private bool pGroupHasSubGroups = false;

        #endregion

        #region frmSECUsers
        public frmSECUsers()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmSECUsers";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                
                this.CancelButton = cmdClose;

                pMdtUsers = (AfyaPro_MT.clsUsers)Activator.GetObject(
                    typeof(AfyaPro_MT.clsUsers),
                    Program.gMiddleTier + "clsUsers");

                pMdtUserGroups = (AfyaPro_MT.clsUserGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsUserGroups),
                    Program.gMiddleTier + "clsUserGroups");

                pMdtClientGroups = (AfyaPro_MT.clsClientGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsClientGroups),
                    Program.gMiddleTier + "clsClientGroups");

                pMdtClientSubGroups = (AfyaPro_MT.clsClientSubGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsClientSubGroups),
                    Program.gMiddleTier + "clsClientSubGroups");

                pMdtLanguage = (AfyaPro_MT.clsLanguage)Activator.GetObject(
                    typeof(AfyaPro_MT.clsLanguage),
                    Program.gMiddleTier + "clsLanguage");

                pMdtPaymentTypes = (AfyaPro_MT.clsPaymentTypes)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPaymentTypes),
                    Program.gMiddleTier + "clsPaymentTypes");

                pMdtPriceCategories = (AfyaPro_MT.clsPriceCategories)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPriceCategories),
                    Program.gMiddleTier + "clsPriceCategories");

                pMdtSomStores = (AfyaPro_MT.clsSomStores)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomStores),
                    Program.gMiddleTier + "clsSomStores");

                pDtUserGroups.Columns.Add("code", typeof(System.String));
                pDtUserGroups.Columns.Add("description", typeof(System.String));
                cboUserGroup.Properties.DataSource = pDtUserGroups;
                cboUserGroup.Properties.DisplayMember = "description";
                cboUserGroup.Properties.ValueMember = "code";

                pDtCustomerGroups.Columns.Add("code", typeof(System.String));
                pDtCustomerGroups.Columns.Add("description", typeof(System.String));
                cboCustomerGroup.Properties.DataSource = pDtCustomerGroups;
                cboCustomerGroup.Properties.DisplayMember = "description";
                cboCustomerGroup.Properties.ValueMember = "code";

                pDtCustomerSubGroups.Columns.Add("code", typeof(System.String));
                pDtCustomerSubGroups.Columns.Add("description", typeof(System.String));
                cboCustomerSubGroup.Properties.DataSource = pDtCustomerSubGroups;
                cboCustomerSubGroup.Properties.DisplayMember = "description";
                cboCustomerSubGroup.Properties.ValueMember = "code";

                pDtPaymentTypes.Columns.Add("code", typeof(System.String));
                pDtPaymentTypes.Columns.Add("description", typeof(System.String));
                cboPaymentType.Properties.DataSource = pDtPaymentTypes;
                cboPaymentType.Properties.DisplayMember = "description";
                cboPaymentType.Properties.ValueMember = "code";

                pDtPriceCategories.Columns.Add("pricename", typeof(System.String));
                pDtPriceCategories.Columns.Add("pricedescription", typeof(System.String));
                cboPriceCategory.Properties.DataSource = pDtPriceCategories;
                cboPriceCategory.Properties.DisplayMember = "pricedescription";
                cboPriceCategory.Properties.ValueMember = "pricename";

                pDtDefaultStores.Columns.Add("code", typeof(System.String));
                pDtDefaultStores.Columns.Add("description", typeof(System.String));
                cboStore.Properties.DataSource = pDtDefaultStores;
                cboStore.Properties.DisplayMember = "description";
                cboStore.Properties.ValueMember = "code";

                pLanguageItems = cboLanguage.Properties.Items;

                pDtStores.Columns.Add("selected", typeof(System.Boolean));
                pDtStores.Columns.Add("code", typeof(System.String));
                pDtStores.Columns.Add("description", typeof(System.String));

                grdStores.DataSource = pDtStores;

                this.Fill_LookupData();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmSECUsers_Load
        private void frmSECUsers_Load(object sender, EventArgs e)
        {
            switch (gDataState.Trim().ToLower())
            {
                case "new": Mode_New(); break;
                case "edit": Mode_Edit(); break;
            }

            this.Load_Controls();
        }
        #endregion

        #region frmSECUsers_Activated
        private void frmSECUsers_Activated(object sender, EventArgs e)
        {
            if (pFirstTimeLoad == true)
            {
                txtCode.Focus();
                pFirstTimeLoad = false;
            }
        }
        #endregion

        #region cboCustomerGroup_EditValueChanged
        void cboCustomerGroup_EditValueChanged(object sender, EventArgs e)
        {
            DataRow mNewRow;
            string mFunctionName = "cboCustomerGroup_EditValueChanged";

            try
            {
                pDtCustomerSubGroups.Rows.Clear();
                if (cboCustomerGroup.ItemIndex == -1)
                {
                    return;
                }

                pGroupHasSubGroups = false;
                cboCustomerSubGroup.EditValue = null;

                string mGroupCode = cboCustomerGroup.GetColumnValue("code").ToString().Trim();

                DataTable mDtGroups = pMdtClientGroups.View("code='" + mGroupCode + "'", "", Program.gLanguageName, "grdCUSCustomerGroups");
                if (mDtGroups.Rows.Count > 0)
                {
                    pGroupHasSubGroups = Convert.ToBoolean(mDtGroups.Rows[0]["hassubgroups"]);
                }

                if (pGroupHasSubGroups == true)
                {
                    DataTable mDtSubGroups = pMdtClientSubGroups.View("groupcode='" + mGroupCode + "'", "code", Program.gLanguageName, "grdCUSCustomerSubGroups");

                    mNewRow = pDtCustomerSubGroups.NewRow();
                    mNewRow["code"] = "";
                    mNewRow["description"] = "";
                    pDtCustomerSubGroups.Rows.Add(mNewRow);
                    pDtCustomerSubGroups.AcceptChanges();
                    foreach (DataRow mDataRow in mDtSubGroups.Rows)
                    {
                        mNewRow = pDtCustomerSubGroups.NewRow();
                        mNewRow["code"] = mDataRow["code"].ToString();
                        mNewRow["description"] = mDataRow["description"].ToString();
                        pDtCustomerSubGroups.Rows.Add(mNewRow);
                        pDtCustomerSubGroups.AcceptChanges();
                    }

                    foreach (DataColumn mDataColumn in pDtCustomerSubGroups.Columns)
                    {
                        mDataColumn.Caption = mDtSubGroups.Columns[mDataColumn.ColumnName].Caption;
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

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            DataRow mNewRow;
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region usergroups

                pDtUserGroups.Rows.Clear();
                DataTable mDtUserGroups = pMdtUserGroups.View("", "code", Program.gLanguageName, "grdSECUserGroups");
                foreach (DataRow mDataRow in mDtUserGroups.Rows)
                {
                    mNewRow = pDtUserGroups.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtUserGroups.Rows.Add(mNewRow);
                    pDtUserGroups.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtUserGroups.Columns)
                {
                    mDataColumn.Caption = mDtUserGroups.Columns[mDataColumn.ColumnName].Caption;
                }

                #endregion

                #region languages

                DataTable mDtLanguages = pMdtLanguage.Get_DefinedLanguages();
                pLanguageItems.Clear();
                foreach (DataRow mDataRow in mDtLanguages.Rows)
                {
                    pLanguageItems.Add(mDataRow["description"].ToString());
                }

                #endregion

                #region customergroups

                pDtCustomerGroups.Rows.Clear();
                DataTable mDtCustomerGroups = pMdtClientGroups.View("", "code", Program.gLanguageName, "grdCUSCustomerGroups");

                mNewRow = pDtCustomerGroups.NewRow();
                mNewRow["code"] = "";
                mNewRow["description"] = "";
                pDtCustomerGroups.Rows.Add(mNewRow);
                pDtCustomerGroups.AcceptChanges();

                foreach (DataRow mDataRow in mDtCustomerGroups.Rows)
                {
                    mNewRow = pDtCustomerGroups.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtCustomerGroups.Rows.Add(mNewRow);
                    pDtCustomerGroups.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtCustomerGroups.Columns)
                {
                    mDataColumn.Caption = mDtCustomerGroups.Columns[mDataColumn.ColumnName].Caption;
                }

                #endregion

                #region PaymentTypes

                pDtPaymentTypes.Rows.Clear();
                DataTable mDtPaymentTypes = pMdtPaymentTypes.View("", "code", Program.gLanguageName, "grdBLSPaymentTypes");

                mNewRow = pDtPaymentTypes.NewRow();
                mNewRow["code"] = "";
                mNewRow["description"] = "";
                pDtPaymentTypes.Rows.Add(mNewRow);
                pDtPaymentTypes.AcceptChanges();

                foreach (DataRow mDataRow in mDtPaymentTypes.Rows)
                {
                    mNewRow = pDtPaymentTypes.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtPaymentTypes.Rows.Add(mNewRow);
                    pDtPaymentTypes.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtPaymentTypes.Columns)
                {
                    mDataColumn.Caption = mDtPaymentTypes.Columns[mDataColumn.ColumnName].Caption;
                }

                #endregion

                #region Price Categories

                pDtPriceCategories.Rows.Clear();
                DataTable mDtPriceCategories = pMdtPriceCategories.View_Active(Program.gLanguageName, "frmBLSPriceCategories");

                mNewRow = pDtPriceCategories.NewRow();
                mNewRow["pricename"] = "";
                mNewRow["pricedescription"] = "";
                pDtPriceCategories.Rows.Add(mNewRow);
                pDtPriceCategories.AcceptChanges();

                foreach (DataRow mDataRow in mDtPriceCategories.Rows)
                {
                    mNewRow = pDtPriceCategories.NewRow();
                    mNewRow["pricename"] = mDataRow["pricename"].ToString();
                    mNewRow["pricedescription"] = mDataRow["pricedescription"].ToString();
                    pDtPriceCategories.Rows.Add(mNewRow);
                    pDtPriceCategories.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtPriceCategories.Columns)
                {
                    mDataColumn.Caption = mDtPriceCategories.Columns[mDataColumn.ColumnName].Caption;
                }

                #endregion

                #region DefaultStores

                pDtDefaultStores.Rows.Clear();
                DataTable mDtStores = pMdtSomStores.View("", "", Program.gLanguageName, "grdIVSStores");

                mNewRow = pDtDefaultStores.NewRow();
                mNewRow["code"] = "";
                mNewRow["description"] = "";
                pDtDefaultStores.Rows.Add(mNewRow);
                pDtDefaultStores.AcceptChanges();

                foreach (DataRow mDataRow in mDtStores.Rows)
                {
                    mNewRow = pDtDefaultStores.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtDefaultStores.Rows.Add(mNewRow);
                    pDtDefaultStores.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtDefaultStores.Columns)
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

        #region Fill_Stores
        private void Fill_Stores()
        {
            string mFunctionName = "Fill_Stores";
            try
            {
                pDtStores.Rows.Clear();

                DataTable mDtStores = pMdtSomStores.View("", "", Program.gLanguageName, "");
                DataTable mDtUserStores = pMdtUsers.View_StoreUsers("usercode='" + txtCode.Text.Trim() + "'", "");
                DataView mDvUserStores = new DataView();
                mDvUserStores.Table = mDtUserStores;
                mDvUserStores.Sort = "storecode";

                foreach (DataRow mDataRow in mDtStores.Rows)
                {
                    bool mGranted = false;
                    if (mDvUserStores.Find(mDataRow["code"].ToString().Trim()) >= 0)
                    {
                        mGranted = true;
                    }

                    DataRow mNewRow = pDtStores.NewRow();
                    mNewRow["selected"] = mGranted;
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtStores.Rows.Add(mNewRow);
                    pDtStores.AcceptChanges();
                }
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

            mObjectsList.Add(txbCode);
            mObjectsList.Add(txbDescription);
            mObjectsList.Add(txbUserGroup);
            mObjectsList.Add(txbAddress);
            mObjectsList.Add(txbPhone);
            mObjectsList.Add(txbOccupation);
            mObjectsList.Add(txbPassword);
            mObjectsList.Add(txbConfirmPassword);
            mObjectsList.Add(grpDefaultSettings);
            mObjectsList.Add(txbLanguage);
            mObjectsList.Add(txbCustomerGroup);
            mObjectsList.Add(txbCustomerSubGroup);
            mObjectsList.Add(txbPaymentType);
            mObjectsList.Add(txbPriceCategory);
            mObjectsList.Add(grpStores);
            mObjectsList.Add(txbStore);
            mObjectsList.Add(code);
            mObjectsList.Add(description);
            mObjectsList.Add(cmdOk);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
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

        #region Mode_New
        private void Mode_New()
        {
            String mFunctionName = "Mode_New";

            try
            {
                txtCode.Text = "";
                this.Data_Clear();
                this.Fill_Stores();

                //Disable Restpassword button for new user..
                cmdResetPassword.Enabled = false;

                txtCode.Focus();

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
                
                // Disable password field during edit operation.Password can be changed from change password screen only.
                txtPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;

                pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                txtCode.Text = pSelectedRow["code"].ToString();
                txtDescription.Text = pSelectedRow["description"].ToString();
                cboUserGroup.ItemIndex = Program.Get_LookupItemIndex(cboUserGroup, "code", pSelectedRow["usergroupcode"].ToString());
                txtAddress.Text = pSelectedRow["address"].ToString();
                txtPhone.Text = pSelectedRow["phone"].ToString();
                txtOccupation.Text = pSelectedRow["occupation"].ToString();
                cboLanguage.Text = pSelectedRow["defaultlanguage"].ToString();
                cboCustomerGroup.ItemIndex = 
                    Program.Get_LookupItemIndex(cboCustomerGroup, "code", pSelectedRow["defaultclientgroupcode"].ToString());
                cboCustomerSubGroup.ItemIndex =
                    Program.Get_LookupItemIndex(cboCustomerSubGroup, "code", pSelectedRow["defaultclientsubgroupcode"].ToString());
                cboPaymentType.ItemIndex =
                    Program.Get_LookupItemIndex(cboPaymentType, "code", pSelectedRow["defaultpaymenttypecode"].ToString());
                cboPriceCategory.ItemIndex =
                    Program.Get_LookupItemIndex(cboPriceCategory, "pricename", pSelectedRow["defaultpricecategorycode"].ToString());
                cboStore.ItemIndex =
                    Program.Get_LookupItemIndex(cboStore, "code", pSelectedRow["storecode"].ToString());
                chkAllowChangingTransDate.Checked = Convert.ToBoolean(pSelectedRow["allowchangingtransdate"]);

                this.Fill_Stores();

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
            cboUserGroup.EditValue = null;
            txtAddress.Text = "";
            txtPhone.Text = "";
            txtOccupation.Text = "";

            cboCustomerGroup.EditValue = null;
            cboCustomerSubGroup.EditValue = null;
            cboPaymentType.EditValue = null;
            cboPriceCategory.EditValue = null;
            cboStore.EditValue = null;
            chkAllowChangingTransDate.Checked = false;

            //Reset password field
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";

            pDtCustomerSubGroups.Rows.Clear();
        }
        #endregion

        #region Data_Fill
        internal void Data_Fill(GridControl mGridControl)
        {
            string mFunctionName = "Data_Fill";

            try
            {
                //load data
                DataTable mDtUsers = pMdtUsers.View("", "", Program.gLanguageName, mGridControl.Name);
                mGridControl.DataSource = mDtUsers;
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
            string mUserGroupCode = "";
            string mLanguage = "";
            string mCustomerGroupCode = "";
            string mCustomerSubGroupCode = "";
            string mPaymentTypeCode = "";
            string mPriceCategoryCode = "";
            string mStoreCode = "";

            String mFunctionName = "Data_New";

            #region validation
            if (txtCode.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.SEC_UserIdIsInvalid.ToString());
                txtCode.Focus();
                return;
            }

            if (Regex.IsMatch(txtCode.Text, @"^[a-zA-Z0-9]*$") == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.SEC_UnsupportedCharactersForUserId.ToString());
                txtCode.Focus();
                return;
            }

            if (txtDescription.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.SEC_UserFullNameIsInvalid.ToString());
                txtDescription.Focus();
                return;
            }

            if (cboUserGroup.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.SEC_UserGroupDescriptionIsInvalid.ToString());
                cboUserGroup.Focus();
                return;
            }

            if (txtPassword.Text == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.SEC_PasswordIsInvalid.ToString());
                txtPassword.Focus();
                return;
            }

            if (Regex.IsMatch(txtPassword.Text, @"^[a-zA-Z0-9]{3,}$") == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.SEC_PasswordLettersOrNumbersOnly.ToString());
                txtPassword.Focus();
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.SEC_PasswordConfirmedDoNotMatch.ToString());
                txtPassword.Focus();
                return;
            }
            #endregion

            try
            {
                mLanguage = cboLanguage.Text;

                if (cboUserGroup.ItemIndex != -1)
                {
                    mUserGroupCode = cboUserGroup.GetColumnValue("code").ToString();
                }
                if (cboCustomerGroup.ItemIndex != -1)
                {
                    mCustomerGroupCode = cboCustomerGroup.GetColumnValue("code").ToString();
                }
                if (cboCustomerSubGroup.ItemIndex != -1)
                {
                    mCustomerSubGroupCode = cboCustomerSubGroup.GetColumnValue("code").ToString();
                }
                if (cboPaymentType.ItemIndex != -1)
                {
                    mPaymentTypeCode = cboPaymentType.GetColumnValue("code").ToString();
                }
                if (cboPriceCategory.ItemIndex != -1)
                {
                    mPriceCategoryCode = cboPriceCategory.GetColumnValue("pricename").ToString();
                }
                if (cboStore.ItemIndex != -1)
                {
                    mStoreCode = cboStore.GetColumnValue("code").ToString();
                }

                DataTable mDtStores = new DataTable("stores");
                mDtStores.Columns.Add("storecode", typeof(System.String));

                foreach (DataRow mDataRow in pDtStores.Rows)
                {
                    if (Convert.ToBoolean(mDataRow["selected"]) == true)
                    {
                        DataRow mNewRow = mDtStores.NewRow();
                        mNewRow["storecode"] = mDataRow["code"].ToString().Trim();
                        mDtStores.Rows.Add(mNewRow);
                        mDtStores.AcceptChanges();
                    }
                }

                //add 
                pResult = pMdtUsers.Add(txtCode.Text, txtPassword.Text, txtDescription.Text, mUserGroupCode,
                txtOccupation.Text, txtAddress.Text, txtPhone.Text, mLanguage, mCustomerGroupCode, mCustomerSubGroupCode,
                mPaymentTypeCode, mPriceCategoryCode, mStoreCode, Convert.ToInt32(chkAllowChangingTransDate.Checked), mDtStores, Program.gCurrentUser.Code);
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
                Program.Get_UserActions();
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
            string mUserGroupCode = "";
            string mLanguage = "";
            string mCustomerGroupCode = "";
            string mCustomerSubGroupCode = "";
            string mPaymentTypeCode = "";
            string mPriceCategoryCode = "";
            string mStoreCode = "";

            String mFunctionName = "Data_Edit";

            #region validation
            if (txtCode.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.SEC_UserIdIsInvalid.ToString());
                txtCode.Focus();
                return;
            }

            if (Regex.IsMatch(txtCode.Text, @"^[a-zA-Z0-9]*$") == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.SEC_UnsupportedCharactersForUserId.ToString());
                txtCode.Focus();
                return;
            }

            if (txtDescription.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.SEC_UserFullNameIsInvalid.ToString());
                txtDescription.Focus();
                return;
            }

            if (cboUserGroup.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.SEC_UserGroupDescriptionIsInvalid.ToString());
                cboUserGroup.Focus();
                return;
            }
            #endregion

            try
            {
                mLanguage = cboLanguage.Text;

                if (cboUserGroup.ItemIndex != -1)
                {
                    mUserGroupCode = cboUserGroup.GetColumnValue("code").ToString();
                }
                if (cboCustomerGroup.ItemIndex != -1)
                {
                    mCustomerGroupCode = cboCustomerGroup.GetColumnValue("code").ToString();
                }
                if (cboCustomerSubGroup.ItemIndex != -1)
                {
                    mCustomerSubGroupCode = cboCustomerSubGroup.GetColumnValue("code").ToString();
                }
                if (cboPaymentType.ItemIndex != -1)
                {
                    mPaymentTypeCode = cboPaymentType.GetColumnValue("code").ToString();
                }
                if (cboPriceCategory.ItemIndex != -1)
                {
                    mPriceCategoryCode = cboPriceCategory.GetColumnValue("pricename").ToString();
                }
                if (cboStore.ItemIndex != -1)
                {
                    mStoreCode = cboStore.GetColumnValue("code").ToString();
                }

                DataTable mDtStores = new DataTable("stores");
                mDtStores.Columns.Add("storecode", typeof(System.String));

                foreach (DataRow mDataRow in pDtStores.Rows)
                {
                    if (Convert.ToBoolean(mDataRow["selected"]) == true)
                    {
                        DataRow mNewRow = mDtStores.NewRow();
                        mNewRow["storecode"] = mDataRow["code"].ToString().Trim();
                        mDtStores.Rows.Add(mNewRow);
                        mDtStores.AcceptChanges();
                    }
                }

                //Edit 
                pResult = pMdtUsers.Edit(txtCode.Text, txtDescription.Text, mUserGroupCode,
                txtOccupation.Text, txtAddress.Text, txtPhone.Text, mLanguage, mCustomerGroupCode, mCustomerSubGroupCode,
                mPaymentTypeCode, mPriceCategoryCode, mStoreCode, Convert.ToInt32(chkAllowChangingTransDate.Checked), mDtStores, Program.gCurrentUser.Code);
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
                Program.Get_UserActions();
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

                DialogResult mResp = Program.Confirm_Deletion(
                    pSelectedRow["code"].ToString().Trim() + "'   '"
                    + pSelectedRow["description"].ToString().Trim());

                if (mResp != DialogResult.Yes)
                {
                    return;
                }

                //add 
                pResult = pMdtUsers.Delete(pSelectedRow["code"].ToString().Trim(), Program.gCurrentUser.Code);
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

        #region cmdResetPassword_Click
        private void cmdResetPassword_Click(object sender, EventArgs e)
        {
            txtPassword.Enabled = false;
            txtConfirmPassword.Enabled = false;
            frmChangePassword mChangePassword = new frmChangePassword(txtCode.Text, true);
            mChangePassword.ShowDialog();
        }
        #endregion
    }
}