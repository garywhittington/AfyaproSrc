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
using DevExpress.XtraLayout;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.IO;
using System.Xml.Serialization;

namespace AfyaPro_NextGen
{
    public partial class frmBILBillPosting : DevExpress.XtraEditors.XtraForm
    {
        internal object gOpdRegistration = null;
        internal object gIpdAdmission = null;
        internal object gIpdDischarge = null;
        internal object gPatientDiagnosisForm = null;

        #region declaration

        internal AfyaPro_Types.clsBooking gCurrentBooking = new AfyaPro_Types.clsBooking();
        internal AfyaPro_Types.clsPatient gCurrentPatient = null;

        private AfyaPro_MT.clsClientGroups pMdtClientGroups;
        private AfyaPro_MT.clsBillItemGroups pMdtBillItemGroups;
        private AfyaPro_MT.clsBillItemSubGroups pMdtBillItemSubGroups;
        private AfyaPro_MT.clsBillItems pMdtBillItems;
        private AfyaPro_MT.clsBilling pMdtBilling;
        private AfyaPro_MT.clsRegistrations pMdtRegistrations;
        private AfyaPro_MT.clsPriceCategories pMdtPriceCategories;
        private AfyaPro_MT.clsSomProducts pMdtSomProducts;
        private AfyaPro_MT.clsSomStores pMdtSomStores;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private string pBillingGroupCode = "";
        private string pBillingSubGroupCode = "";
        private string pTreatmentPointCode = "";
        private string pTreatmentPoint = "";
        private DataTable pDtPriceCategories = new DataTable("pricecategories");
        private DataTable pDtCartItems = new DataTable("cartitems");
        private DataTable pDtBilledItems = new DataTable("billeditems");

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private bool pQuantityChanging = false;
        private bool pPriceChanging = false;
        private bool pAmountChanging = false;

        private double pTotalPaid = 0;
        private double pDiscount = 0;
        private bool pPaymentsDone = false;
        private string pReceiptNo = "";
        private DataTable pDtPayments = new DataTable("payments");

        private string pCurrPatientId = "";
        private string pPrevPatientId = "";
        private string pCurrItemCode = "";
        private string pPrevItemCode = "";
        private string pCurrPharmCode = "";
        private string pPrevPharmCode = "";

        private bool pSearchingPatient = false;
        private bool pSearchingBillingItem = false;
        private bool pSearchingPharmacyItem = false;
        private Int16 pPromptPayment = 0;
        private Int16 pItemsSensitive = 0;

        private LabelControl txbAffectBillsDirectOn = new LabelControl();
        private LabelControl txbAffectBillsDirectOff = new LabelControl();

        private bool pAffectBillsDirect = true;
        private bool pProcessIncomingBill = false;

        private DataTable pDtStores = new DataTable("stores");

        #endregion

        #region frmBILBillPosting
        public frmBILBillPosting()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmBILBillPosting";
            this.KeyDown += new KeyEventHandler(frmBILBillPosting_KeyDown);

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                pMdtClientGroups = (AfyaPro_MT.clsClientGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsClientGroups),
                    Program.gMiddleTier + "clsClientGroups");

                pMdtAutoCodes = (AfyaPro_MT.clsAutoCodes)Activator.GetObject(
                    typeof(AfyaPro_MT.clsAutoCodes),
                    Program.gMiddleTier + "clsAutoCodes");

                pMdtRegistrations = (AfyaPro_MT.clsRegistrations)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRegistrations),
                    Program.gMiddleTier + "clsRegistrations");

                pMdtBilling = (AfyaPro_MT.clsBilling)Activator.GetObject(
                    typeof(AfyaPro_MT.clsBilling),
                    Program.gMiddleTier + "clsBilling");

                pMdtBillItemGroups = (AfyaPro_MT.clsBillItemGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsBillItemGroups),
                    Program.gMiddleTier + "clsBillItemGroups");

                pMdtBillItemSubGroups = (AfyaPro_MT.clsBillItemSubGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsBillItemSubGroups),
                    Program.gMiddleTier + "clsBillItemSubGroups");

                pMdtBillItems = (AfyaPro_MT.clsBillItems)Activator.GetObject(
                    typeof(AfyaPro_MT.clsBillItems),
                    Program.gMiddleTier + "clsBillItems");

                pMdtSomProducts = (AfyaPro_MT.clsSomProducts)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomProducts),
                    Program.gMiddleTier + "clsSomProducts");

                pMdtSomStores = (AfyaPro_MT.clsSomStores)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomStores),
                    Program.gMiddleTier + "clsSomStores");

                pMdtPriceCategories = (AfyaPro_MT.clsPriceCategories)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPriceCategories),
                    Program.gMiddleTier + "clsPriceCategories");

                pDtPriceCategories.Columns.Add("pricename", typeof(System.String));
                pDtPriceCategories.Columns.Add("pricedescription", typeof(System.String));
                cboPriceCategory.Properties.DataSource = pDtPriceCategories;
                cboPriceCategory.Properties.DisplayMember = "pricedescription";
                cboPriceCategory.Properties.ValueMember = "pricename";

                pDtCartItems.Columns.Add("itemgroupcode", typeof(System.String));
                pDtCartItems.Columns.Add("itemgroupdescription", typeof(System.String));
                pDtCartItems.Columns.Add("itemsubgroupcode", typeof(System.String));
                pDtCartItems.Columns.Add("itemsubgroupdescription", typeof(System.String));
                pDtCartItems.Columns.Add("itemcode", typeof(System.String));
                pDtCartItems.Columns.Add("itemdescription", typeof(System.String));
                pDtCartItems.Columns.Add("expirydate", typeof(System.DateTime));
                pDtCartItems.Columns.Add("storecode", typeof(System.String));
                pDtCartItems.Columns.Add("qty", typeof(System.Double));
                pDtCartItems.Columns.Add("unitprice", typeof(System.Double));
                pDtCartItems.Columns.Add("actualamount", typeof(System.Double));
                pDtCartItems.Columns.Add("amount", typeof(System.Double));
                pDtCartItems.Columns.Add("storedescription", typeof(System.String));
                pDtCartItems.Columns.Add("itemopmcode", typeof(System.String));
                pDtCartItems.Columns.Add("itemopmdescription", typeof(System.String));
                pDtCartItems.Columns.Add("packagingcode", typeof(System.String));
                pDtCartItems.Columns.Add("packagingdescription", typeof(System.String));
                pDtCartItems.Columns.Add("piecesinpackage", typeof(System.Int32));
                pDtCartItems.Columns.Add("autocode", typeof(System.Int32));

                pDtBilledItems.Columns.Add("itemgroupcode", typeof(System.String));
                pDtBilledItems.Columns.Add("itemgroupdescription", typeof(System.String));
                pDtBilledItems.Columns.Add("itemsubgroupcode", typeof(System.String));
                pDtBilledItems.Columns.Add("itemsubgroupdescription", typeof(System.String));
                pDtBilledItems.Columns.Add("itemcode", typeof(System.String));
                pDtBilledItems.Columns.Add("itemdescription", typeof(System.String));
                pDtBilledItems.Columns.Add("qty", typeof(System.Double));
                pDtBilledItems.Columns.Add("unitprice", typeof(System.Double));
                pDtBilledItems.Columns.Add("actualamount", typeof(System.Double));
                pDtBilledItems.Columns.Add("amount", typeof(System.Double));
                pDtBilledItems.Columns.Add("autocode", typeof(System.Int32));
                //pDtBilledItems.Columns.Add("number", typeof(System.Int32));

                pDtPayments.Columns.Add("paymenttypecode", typeof(System.String));
                pDtPayments.Columns.Add("paymenttypedescription", typeof(System.String));
                pDtPayments.Columns.Add("amtpaid", typeof(System.Double));
                pDtPayments.Columns.Add("chequeno", typeof(System.String));
                pDtPayments.Columns.Add("bank", typeof(System.String));
                pDtPayments.Columns.Add("branch", typeof(System.String));
                pDtPayments.Columns.Add("holder", typeof(System.String));
                pDtPayments.Columns.Add("paidforbill", typeof(System.String));
                pDtPayments.Columns.Add("checkdepositbalance", typeof(System.Int16));
                pDtPayments.Columns.Add("accountcode", typeof(System.String));
                pDtPayments.Columns.Add("accountdescription", typeof(System.String));

                #region stores
                pDtStores.Columns.Add("code", typeof(System.String));
                pDtStores.Columns.Add("description", typeof(System.String));
                cboStore.Properties.DataSource = pDtStores;
                cboStore.Properties.DisplayMember = "description";
                cboStore.Properties.ValueMember = "code";
                #endregion

                pDtPayments.Rows.Clear(); 

                grdBILBillPosting.ForceInitialize();
              
                grdBILBilledItems.ForceInitialize();

                this.Fill_LookupData();
                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.bilpostbills_customizelayout.ToString());
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
            List<Object> mObjectsList = new List<object>();

            txbAffectBillsDirectOn.Name = "txbAffectBillsDirectOn";
            txbAffectBillsDirectOn.Text = "Post Bills";
            txbAffectBillsDirectOff.Name = "txbAffectBillsDirectOff";
            txbAffectBillsDirectOff.Text = "Prepare Outgoing Bills";

            mObjectsList.Add(txbAffectBillsDirectOn);
            mObjectsList.Add(txbAffectBillsDirectOff);
            mObjectsList.Add(txbPatientId);
            mObjectsList.Add(cmdSearch);
            mObjectsList.Add(txbName);
            mObjectsList.Add(txbBirthDate);
            mObjectsList.Add(txbYears);
            mObjectsList.Add(txbMonths);
            mObjectsList.Add(txbGender);
            mObjectsList.Add(txbGender);
            mObjectsList.Add(txbInvoiceNo);
            mObjectsList.Add(txbBalanceDue);
            mObjectsList.Add(txbDepositBalance);
            mObjectsList.Add(txbCustomerGroup);
            mObjectsList.Add(txbCustomerSubGroup);
            mObjectsList.Add(txbMembershipNo);
            mObjectsList.Add(txbPriceCategory);
            mObjectsList.Add(grpGeneral);
            mObjectsList.Add(grpPharmacy);
            mObjectsList.Add(txbItemCode);
            mObjectsList.Add(txbItemDescription);
            mObjectsList.Add(cmdSearchItem);
            mObjectsList.Add(txbItemQuantity);
            mObjectsList.Add(txbItemPrice);
            mObjectsList.Add(txbItemAmount);
            mObjectsList.Add(cmdAdd);
            mObjectsList.Add(cmdRemove);
            mObjectsList.Add(cmdSave);
            mObjectsList.Add(cmdNew);
            mObjectsList.Add(cmdClose);

            DevExpress.XtraGrid.Views.Grid.GridView mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)grdBILBillPosting.MainView;
            foreach (DevExpress.XtraGrid.Columns.GridColumn mGridColumn in mGridView.Columns)
            {
                mObjectsList.Add(mGridColumn);
            }

            DevExpress.XtraGrid.Views.Grid.GridView mBilledItemGridView = (DevExpress.XtraGrid.Views.Grid.GridView)grdBILBilledItems.MainView;
            foreach (DevExpress.XtraGrid.Columns.GridColumn mGridColumn in mBilledItemGridView.Columns)
            {
                mObjectsList.Add(mGridColumn);
            }

            Program.Apply_Language(this.Name, mObjectsList);
            this.Data_Clear();
        }

        #endregion

        #region frmBILBillPosting_Load
        private void frmBILBillPosting_Load(object sender, EventArgs e)
        {
            this.Top = 0;

            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            DevExpress.XtraGrid.Views.Grid.GridView mGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            mGridView.OptionsBehavior.Editable = false;
            mGridView.OptionsView.ShowGroupPanel = true;
            mGridView.OptionsView.ShowFooter = false;
            mGridView.OptionsCustomization.AllowGroup = false;
            mGridView.OptionsView.ShowIndicator = false;
            grdBILBillPosting.MainView = mGridView;
            grdBILBillPosting.DataSource = pDtCartItems;
            mGridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(mGridView_FocusedRowChanged);
            mGridView.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(mGridView_RowClick);
            mGridView.GotFocus += new EventHandler(mGridView_GotFocus);
            mGridView.Columns["itemdescription"].Group();
            mGridView.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "qty", null, ": Quantity={0:c}");
           
            for (int mIndex = 0; mIndex < mGridView.Columns.Count; mIndex++)
            {
                switch (mGridView.Columns[mIndex].ColumnType.ToString().ToLower())
                {
                    case "system.double":
                        {
                            mGridView.Columns[mIndex].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            mGridView.Columns[mIndex].DisplayFormat.FormatString = "{0:c}";
                        }
                        break;
                }
            }

            grdBILBillPosting.DataSource = pDtCartItems;
            grdBILBillPosting.ForceInitialize();
            Program.Restore_GridLayout(grdBILBillPosting, grdBILBillPosting.Name);

            DevExpress.XtraGrid.Views.Grid.GridView mBilledGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            mBilledGridView.OptionsBehavior.Editable = false;
            mBilledGridView.OptionsView.ShowGroupPanel = false;
            mBilledGridView.OptionsView.ShowFooter = false;
            mBilledGridView.OptionsCustomization.AllowGroup = false;
            mBilledGridView.OptionsView.ShowIndicator = false;
            grdBILBilledItems.MainView = mBilledGridView;
            grdBILBilledItems.DataSource = pDtBilledItems;
            grdBILBilledItems.ForceInitialize();
            Program.Restore_GridLayout(grdBILBilledItems, grdBILBilledItems.Name);

            this.Load_Controls();
            this.Mode_New();

            cboPriceCategory.Enabled = Program.GrantDeny_FunctionAccess(
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.bilpostbills_changepricecategory.ToString());
            txtItemPrice.Enabled = Program.GrantDeny_FunctionAccess(
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.bilpostbills_changeprice.ToString());
            txtItemAmount.Enabled = txtItemPrice.Enabled;

            txbBirthDateFormat.Text = "(" + Program.gCulture.DateTimeFormat.ShortDatePattern + ")";

            this.pFormWidth = this.Width;
            this.pFormHeight = this.Height;

            Program.Center_Screen(this);

            pAffectBillsDirect = Program.GrantDeny_FunctionAccess(
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.bilpostbills_affectbillsdirect.ToString());

            if (pAffectBillsDirect == true)
            {
                this.Text = txbAffectBillsDirectOn.Text;
            }
            else
            {
                this.Text = txbAffectBillsDirectOff.Text;
            }

            this.Data_Display(gCurrentPatient);

            cboStore.ItemIndex = Program.Get_LookupItemIndex(cboStore, "code", Program.gCurrentUser.StoreCode);

            if (gOpdRegistration != null || gIpdAdmission != null || gIpdDischarge != null || gPatientDiagnosisForm != null)
            {
                txtItemCode.Focus();
            }
            else
            {
                txtPatientId.Focus();
            }

            this.Append_ShortcutKeys();
        }
        #endregion

        #region frmBILBillPosting_FormClosing
        private void frmBILBillPosting_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //layout
                if (layoutControl1.IsModified == true)
                {
                    Program.Save_FormLayout(this, layoutControl1, this.Name);
                }

                //grid
                Program.Save_GridLayout(grdBILBillPosting, grdBILBillPosting.Name);

                Program.Save_GridLayout(grdBILBilledItems, grdBILBilledItems.Name);
            }
            catch { }
        }
        #endregion

        #region Append_ShortcutKeys
        private void Append_ShortcutKeys()
        {
            cmdSearch.Text = cmdSearch.Text + " (" + Program.KeyCode_SeachPatient.ToString() + ")";
            cmdSave.Text = cmdSave.Text + " (" + Program.KeyCode_Save.ToString() + ")";
            cmdSearchItem.Text = cmdSearchItem.Text + " (" + Program.KeyCode_SearchBillingItem.ToString() + ")";
            cmdPharmSearchItem.Text = cmdPharmSearchItem.Text + " (" + Program.KeyCode_SearchBillingItem.ToString() + ")";
            cmdAdd.Text = cmdAdd.Text + " (" + Program.KeyCode_ItemAdd.ToString() + ")";
            cmdRemove.Text = cmdRemove.Text + " (" + Program.KeyCode_ItemRemove.ToString() + ")";
            cmdPharmAdd.Text = cmdPharmAdd.Text + " (" + Program.KeyCode_ItemAdd.ToString() + ")";
            cmdPharmDelete.Text = cmdPharmDelete.Text + " (" + Program.KeyCode_ItemRemove.ToString() + ")";

        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            DataRow mNewRow;
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region Price Categories

                pDtPriceCategories.Rows.Clear();
                DataTable mDtPriceCategories = pMdtPriceCategories.View_Active(Program.gLanguageName, "frmBLSPriceCategories");
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

        #region Mode_New
        internal void Mode_New()
        {
            string mFunctionName = "Mode_New";

            try
            {
                txtPatientId.Text = "";
                this.Data_Clear();

                int mGenerateRefNo = pMdtAutoCodes.Auto_Generate_Code(
                        Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.invoiceno));
                if (mGenerateRefNo == -1)
                {
                    Program.Display_Server_Error("");
                    return;
                }
                txtInvoiceNo.Text = "";
                if (mGenerateRefNo == 1)
                {
                    txtInvoiceNo.Text = "<<---New--->>";
                    txtInvoiceNo.Properties.ReadOnly = true;
                }
                else
                {
                    txtInvoiceNo.Properties.ReadOnly = false;
                }

                pCurrPatientId = txtPatientId.Text;
                pPrevPatientId = pCurrPatientId;

                if (gOpdRegistration != null || gIpdAdmission != null || gIpdDischarge != null || gPatientDiagnosisForm != null)
                {
                    txtItemCode.Focus();
                }
                else
                {
                    txtPatientId.Focus();
                }
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
            txtName.Text = "";
            txtGender.Text = "";
            txtBirthDate.Text = "";
            txtYears.Text = "";
            txtMonths.Text = "";

            txtDepositAmt.Text = "";
            txtBalanceDue.Text = "";
            txtGroup.Text = "";
            txtSubGroup.Text = "";
            txtMembershipNo.Text = "";
            pBillingGroupCode = "";
            pBillingSubGroupCode = "";
            pTreatmentPointCode = "";
            pTreatmentPoint = "";

            txtItemCode.Text = "";
            txtItemDescription.Text = "";
            txtItemQuantity.Text = "";
            txtItemPrice.Text = "";
            txtItemAmount.Text = "";

            pTotalPaid = 0;
            pDtPayments.Rows.Clear();
            pDtCartItems.Rows.Clear();

            pDtBilledItems.Rows.Clear();

            pProcessIncomingBill = false;
            pPromptPayment = 0;
            pItemsSensitive = 0;

            if (gOpdRegistration != null)
            {
                frmOPDRegistrations mOPDRegistration = (frmOPDRegistrations)gOpdRegistration;
                cboPriceCategory.ItemIndex = Program.Get_LookupItemIndex(cboPriceCategory, "pricename", mOPDRegistration.gPriceCategory);
            }
            else if (gIpdAdmission != null)
            {
                frmIPDRegistrations mIPDRegistration = (frmIPDRegistrations)gIpdAdmission;
                cboPriceCategory.ItemIndex = Program.Get_LookupItemIndex(cboPriceCategory, "pricename", mIPDRegistration.gPriceCategory);
            }
            else if (Program.gCurrentUser.DefaultPriceCategoryCode.Trim() != "")
            {
                cboPriceCategory.ItemIndex = Program.Get_LookupItemIndex(cboPriceCategory, "pricename", Program.gCurrentUser.DefaultPriceCategoryCode);
            }

            txbTotal.Text = "TOTAL: " + Convert.ToDouble("0").ToString("c");

            #region toggle between affect bills direct

            pAffectBillsDirect = Program.GrantDeny_FunctionAccess(
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.bilpostbills_affectbillsdirect.ToString());

            if (pAffectBillsDirect == true)
            {
                this.Text = txbAffectBillsDirectOn.Text;
            }
            else
            {
                this.Text = txbAffectBillsDirectOff.Text;
            }

            #endregion

            pPrevPatientId = txtPatientId.Text;
            pCurrPatientId = pPrevPatientId;

            pCurrItemCode = "";
            pPrevItemCode = pCurrItemCode;
        }
        #endregion

        #region Clear_Item
        private void Clear_Item()
        {
            txtItemCode.Text = "";
            txtItemDescription.Text = "";
            txtItemQuantity.Text = "";
            txtItemPrice.Text = "";
            txtItemAmount.Text = "";

            pCurrItemCode = txtItemCode.Text;
            pPrevItemCode = pCurrItemCode;
        }
        #endregion

        #region Clear_Pharmacy
        private void Clear_Pharmacy()
        {
            txtPharmCode.Text = "";
            txtPharmDescription.Text = "";
            txtPharmQuantity.Text = "";
            txtPharmPrice.Text = "";
            txtPharmAmount.Text = "";

            pCurrItemCode = txtItemCode.Text;
            pPrevItemCode = pCurrItemCode;
        }
        #endregion

        #region Data_Display
        private void Data_Display(AfyaPro_Types.clsPatient mPatient)
        {
            String mFunctionName = "Data_Display";

            try
            {
                this.Data_Clear();

                if (mPatient != null)
                {
                    if (mPatient.Exist == true)
                    {
                        gCurrentPatient = mPatient;

                        string mFullName = mPatient.firstname;
                        if (mPatient.othernames.Trim() != "")
                        {
                            mFullName = mFullName + " " + mPatient.othernames;
                        }
                        mFullName = mFullName + " " + mPatient.surname;

                        txtPatientId.Text = mPatient.code;
                        txtName.Text = mFullName;
                        if (mPatient.gender.Trim().ToLower() == "f")
                        {
                            txtGender.Text = "Female";
                        }
                        else
                        {
                            txtGender.Text = "Male";
                        }
                        txtBirthDate.Text = mPatient.birthdate.ToString("d");
                        int mDays = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue).Subtract(mPatient.birthdate).Days;
                        int mYears = (int)mDays / 365;
                        int mMonths = (int)(mDays % 365) / 30;

                        txtYears.Text = mYears.ToString();
                        txtMonths.Text = mMonths.ToString();

                        #region summary

                        //if (gOpdRegistration == null && gIpdAdmission == null && gPatientDiagnosisForm == null && gIpdDischarge == null)
                        if (gOpdRegistration == null && gIpdAdmission == null)
                        {
                            gCurrentBooking = pMdtRegistrations.Get_Booking(txtPatientId.Text);
                        }

                        if (gCurrentBooking != null)
                        {
                            txtGroup.Text = gCurrentBooking.BillingGroupDescription;
                            txtSubGroup.Text = gCurrentBooking.BillingSubGroupDescription;
                            txtMembershipNo.Text = gCurrentBooking.BillingGroupMembershipNo;
                            cboPriceCategory.ItemIndex = Program.Get_LookupItemIndex(cboPriceCategory, "pricename", gCurrentBooking.PriceName);

                            pBillingGroupCode = gCurrentBooking.BillingGroupCode;
                            pBillingSubGroupCode = gCurrentBooking.BillingSubGroupCode;
                            pTreatmentPointCode = gCurrentBooking.WhereTakenCode;
                            pTreatmentPoint = gCurrentBooking.WhereTaken;
                        }

                        #region price is specified from previous screens

                        if (gOpdRegistration != null)
                        {
                            frmOPDRegistrations mOPDRegistration = (frmOPDRegistrations)gOpdRegistration;
                            cboPriceCategory.ItemIndex = Program.Get_LookupItemIndex(cboPriceCategory, "pricename", mOPDRegistration.gPriceCategory);
                        }
                        else if (gIpdAdmission != null)
                        {
                            frmIPDRegistrations mIPDRegistration = (frmIPDRegistrations)gIpdAdmission;
                            cboPriceCategory.ItemIndex = Program.Get_LookupItemIndex(cboPriceCategory, "pricename", mIPDRegistration.gPriceCategory);
                        }

                        #endregion

                        //change price category from default to group price
                        if (pBillingGroupCode.Trim() != "")
                        {
                            DataTable mDtCustomerGroups = pMdtClientGroups.View("code='" + pBillingGroupCode.Trim() + "'", "", "", "");
                            if (mDtCustomerGroups.Rows.Count > 0)
                            {
                                cboPriceCategory.ItemIndex =
                                    Program.Get_LookupItemIndex(cboPriceCategory, "pricename", mDtCustomerGroups.Rows[0]["pricecategory"].ToString());
                                pPromptPayment = Convert.ToInt16(mDtCustomerGroups.Rows[0]["promptpayment"]);
                                pItemsSensitive = Convert.ToInt16(mDtCustomerGroups.Rows[0]["itemssensitive"]);

                                #region for some customer groups, an invoice has to be generated even if a user is not allowed to affect bills

                                if (Convert.ToInt16(mDtCustomerGroups.Rows[0]["generateinvoicewhenpreparingbill"]) == 1)
                                {
                                    pAffectBillsDirect = Convert.ToBoolean(mDtCustomerGroups.Rows[0]["generateinvoicewhenpreparingbill"]);
                                }

                                if (pAffectBillsDirect == true)
                                {
                                    this.Text = txbAffectBillsDirectOn.Text;
                                }
                                else
                                {
                                    this.Text = txbAffectBillsDirectOff.Text;
                                }

                                #endregion
                            }
                        }

                        //balance
                        DataTable mDtBalances = pMdtBilling.Get_DebtorBalances(
                        "accountcode='" + txtPatientId.Text.Trim() + "' and debtortype='"
                        + AfyaPro_Types.clsEnums.DebtorTypes.Individual.ToString().Trim() + "'", "", "", "");
                        double mBalanceDue = 0;
                        if (mDtBalances.Rows.Count > 0)
                        {
                            mBalanceDue = Convert.ToDouble(mDtBalances.Rows[0]["balance"]);
                        }
                        txtBalanceDue.Text = mBalanceDue.ToString("c");

                        //account balance
                        DataTable mDtAccountBalances = pMdtBilling.Get_PatientAccountBalances(txtPatientId.Text, "", "");
                        double mAccountBalance = 0;
                        foreach (DataRow mDataRow in mDtAccountBalances.Rows)
                        {
                            mAccountBalance = mAccountBalance + Convert.ToDouble(mDataRow["balance"]);
                        }
                        txtDepositAmt.Text = mAccountBalance.ToString("c");

                        #endregion

                        #region check for incoming bill

                        if (pAffectBillsDirect == true)
                        {
                            DataTable mDtIncomingItems = pMdtBilling.View_IncomingItems(
                                "patientcode='" + mPatient.code.Trim() + "' and display=1", "", "", "");
                            if (mDtIncomingItems.Rows.Count > 0)
                            {
                                DialogResult mDialogResult = Program.Display_Question(
                                    AfyaPro_Types.clsSystemMessages.MessageIds.BIL_ConfirmIncomingBillProcess.ToString(),
                                    MessageBoxDefaultButton.Button1);

                                if (mDialogResult == DialogResult.Yes)
                                {
                                    this.Fill_IncomingBill(mDtIncomingItems);
                                    pProcessIncomingBill = true;
                                }
                            }
                        }
                        else
                        {
                            DataTable mDtIncomingItems = pMdtBilling.View_IncomingItems(
                                "patientcode='" + mPatient.code.Trim() + "' and display=1", "", "", "");
                            if (mDtIncomingItems.Rows.Count > 0)
                            {
                                this.Fill_IncomingBill(mDtIncomingItems);
                                pProcessIncomingBill = true;
                            }
                        }
                        
                        #endregion

                        // Retrieve patient billed items for the day
                        DataTable mDtBilledItems = pMdtBilling.View_BilledItems(
                                "patientcode='" + mPatient.code.Trim() + "' and display=1", "", "", "");
                        if (mDtBilledItems.Rows.Count > 0)
                        {
                            this.Fill_BilledItems(mDtBilledItems);
                        }

                        pCurrPatientId = mPatient.code; 
                        pPrevPatientId = pCurrPatientId;

                        this.Clear_Item();
                        this.Clear_Pharmacy();

                        System.Media.SystemSounds.Beep.Play();

                        txtItemCode.Focus();
                    }
                    else
                    {
                        pCurrPatientId = txtPatientId.Text;
                        pPrevPatientId = pCurrPatientId;
                    }
                }
                else
                {
                    pCurrPatientId = txtPatientId.Text;
                    pPrevPatientId = pCurrPatientId;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Search_Patient
        private AfyaPro_Types.clsPatient Search_Patient()
        {
            string mFunctionName = "Search_Patient";

            try
            {
                gCurrentPatient = pMdtRegistrations.Get_Patient(txtPatientId.Text);
                return gCurrentPatient;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
        }
        #endregion

        #region txtPatientId_KeyDown
        private void txtPatientId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.gCurrentPatient = this.Search_Patient();
                this.Data_Display(gCurrentPatient);
            }
        }
        #endregion

        #region txtPatientId_Leave
        private void txtPatientId_Leave(object sender, EventArgs e)
        {
            pCurrPatientId = txtPatientId.Text;

            if (pCurrPatientId.Trim().ToLower() != pPrevPatientId.Trim().ToLower())
            {
                this.gCurrentPatient = this.Search_Patient();
                this.Data_Display(gCurrentPatient);
            }
        }
        #endregion

        #region cmdNew_Click
        private void cmdNew_Click(object sender, EventArgs e)
        {
            this.Mode_New();
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Fill_IncomingBill
        private void Fill_IncomingBill(DataTable mDtIncomingItems)
        {
            string mFunctionName = "Fill_IncomingBill";

            try
            {
                pDtCartItems.Rows.Clear();

                foreach (DataRow mDataRow in mDtIncomingItems.Rows)
                {
                    DataRow mNewRow = pDtCartItems.NewRow();
                    mNewRow["itemgroupcode"] = mDataRow["itemgroupcode"];
                    mNewRow["itemgroupdescription"] = mDataRow["itemgroupdescription"];
                    mNewRow["itemsubgroupcode"] = mDataRow["itemsubgroupcode"];
                    mNewRow["itemsubgroupdescription"] = mDataRow["itemsubgroupdescription"];
                    mNewRow["itemcode"] = mDataRow["itemcode"];
                    mNewRow["itemdescription"] = mDataRow["itemdescription"];
                    mNewRow["expirydate"] = DBNull.Value;
                    if (Program.IsNullDate(mDataRow["expirydate"]) == false)
                    {
                        mNewRow["expirydate"] = Convert.ToDateTime(mDataRow["expirydate"]);
                    }
                    mNewRow["storecode"] = mDataRow["storecode"];
                    mNewRow["qty"] = Convert.ToDouble(mDataRow["qty"]);
                    mNewRow["unitprice"] = 0;
                    if (Convert.ToDouble(mDataRow["qty"]) != 0)
                    {
                        mNewRow["unitprice"] = Convert.ToDouble(mDataRow["amount"]) / Convert.ToDouble(mDataRow["qty"]);
                    }
                    mNewRow["actualamount"] = Convert.ToDouble(mDataRow["actualamount"]);
                    mNewRow["amount"] = Convert.ToDouble(mDataRow["amount"]);
                    mNewRow["storedescription"] = mDataRow["storedescription"];
                    mNewRow["itemopmcode"] = mDataRow["itemopmcode"];
                    mNewRow["itemopmdescription"] = mDataRow["itemopmdescription"];
                    mNewRow["packagingcode"] = mDataRow["packagingcode"];
                    mNewRow["packagingdescription"] = mDataRow["packagingdescription"];
                    mNewRow["piecesinpackage"] = mDataRow["piecesinpackage"];
                    mNewRow["autocode"] = mDataRow["autocode"];

                    pDtCartItems.Rows.Add(mNewRow);
                    pDtCartItems.AcceptChanges();
                }

                pCurrPatientId = txtPatientId.Text;
                pPrevPatientId = pCurrPatientId;

                DevExpress.XtraGrid.Views.Grid.GridView mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)grdBILBillPosting.Views[0];
                mGridView.ExpandAllGroups();
                
                this.Get_Total();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_BilledItems
        private void Fill_BilledItems(DataTable mDtBillingItems)
        {
            string mFunctionName = "Fill_BilledItems";

            try
            {
                pDtBilledItems.Rows.Clear();

                foreach (DataRow mDataRow in mDtBillingItems.Rows)
                {
                    DataRow mNewRow = pDtBilledItems.NewRow();
                    mNewRow["itemgroupcode"] = mDataRow["itemgroupcode"];
                    mNewRow["itemgroupdescription"] = mDataRow["itemgroupdescription"];
                    mNewRow["itemsubgroupcode"] = mDataRow["itemsubgroupcode"];
                    mNewRow["itemsubgroupdescription"] = mDataRow["itemsubgroupdescription"];
                    mNewRow["itemcode"] = mDataRow["itemcode"];
                    mNewRow["itemdescription"] = mDataRow["itemdescription"];
                    mNewRow["qty"] = Convert.ToDouble(mDataRow["qty"]);
                    mNewRow["unitprice"] = 0;
                    if (Convert.ToDouble(mDataRow["qty"]) != 0)
                    {
                        mNewRow["unitprice"] = Convert.ToDouble(mDataRow["amount"]) / Convert.ToDouble(mDataRow["qty"]);
                    }
                    mNewRow["actualamount"] = Convert.ToDouble(mDataRow["actualamount"]);
                    mNewRow["amount"] = Convert.ToDouble(mDataRow["amount"]);
                    mNewRow["autocode"] = mDataRow["autocode"];

                    pDtBilledItems.Rows.Add(mNewRow);
                    pDtBilledItems.AcceptChanges();
                }

                pCurrPatientId = txtPatientId.Text;
                pPrevPatientId = pCurrPatientId;

                DevExpress.XtraGrid.Views.Grid.GridView mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)grdBILBillPosting.Views[0];
                mGridView.ExpandAllGroups();

                this.Get_Total();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion


        #region Display_RowDetails
        private void Display_RowDetails(DataRow mSelectedRow)
        {
            txtItemCode.Text = "";
            txtItemDescription.Text = "";
            txtItemQuantity.Text = "";
            txtItemPrice.Text = "";
            txtItemAmount.Text = "";

            txtPharmCode.Text = "";
            txtPharmDescription.Text = "";
            txtPharmQuantity.Text = "";
            txtPharmPrice.Text = "";
            txtPharmAmount.Text = "";

            if (mSelectedRow["storecode"].ToString().Trim() == "")
            {
                txtItemCode.Text = mSelectedRow["itemcode"].ToString().Trim();
                txtItemDescription.Text = mSelectedRow["itemdescription"].ToString().Trim();
                txtItemQuantity.Text = Convert.ToDouble(mSelectedRow["qty"]).ToString("c");
                txtItemPrice.Text = Convert.ToDouble(mSelectedRow["unitprice"]).ToString("c");
                txtItemAmount.Text = Convert.ToDouble(mSelectedRow["amount"]).ToString("c");
            }
            else
            {
                txtPharmCode.Text = mSelectedRow["itemcode"].ToString().Trim();
                txtPharmDescription.Text = mSelectedRow["itemdescription"].ToString().Trim();
                txtPharmQuantity.Text = Convert.ToDouble(mSelectedRow["qty"]).ToString("c");
                txtPharmPrice.Text = Convert.ToDouble(mSelectedRow["unitprice"]).ToString("c");
                txtPharmAmount.Text = Convert.ToDouble(mSelectedRow["amount"]).ToString("c");
            }
        }
        #endregion

        #region mGridView_FocusedRowChanged
        void mGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle <= -1)
            {
                return;
            }

            DevExpress.XtraGrid.Views.Grid.GridView mGridView =
            (DevExpress.XtraGrid.Views.Grid.GridView)grdBILBillPosting.MainView;

            this.Display_RowDetails(mGridView.GetDataRow(e.FocusedRowHandle));
        }
        #endregion

        #region mGridView_RowClick
        private void mGridView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle <= -1)
            {
                return;
            }

            DevExpress.XtraGrid.Views.Grid.GridView mGridView =
            (DevExpress.XtraGrid.Views.Grid.GridView)grdBILBillPosting.MainView;

            this.Display_RowDetails(mGridView.GetDataRow(e.RowHandle));
        }
        #endregion

        #region mGridView_GotFocus
        private void mGridView_GotFocus(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView mGridView =
            (DevExpress.XtraGrid.Views.Grid.GridView)grdBILBillPosting.MainView;

            if (mGridView.FocusedRowHandle <= -1)
            {
                return;
            }

            this.Display_RowDetails(mGridView.GetDataRow(mGridView.FocusedRowHandle));
        }
        #endregion

        #region Get_Total
        private void Get_Total()
        {
            double mAmount = 0;
            foreach (DataRow mDataRow in pDtCartItems.Rows)
            {
                mAmount = mAmount + Convert.ToDouble(mDataRow["amount"]);
            }

            txbTotal.Text = "TOTAL: " + Program.Round_Number(mAmount).ToString("c");
        }
        #endregion

        #region Save_Bill
        private void Save_Bill()
        {
            Int16 mGenerateInvoiceNo = 0;
            Int16 mGenerateReceiptNo = 0;
            AfyaPro_Types.clsBill mBill = new AfyaPro_Types.clsBill();

            string mFunctionName = "Save_Bill";

            #region validation

            if (Program.IsDate(Program.gMdiForm.txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                return;
            }

            if (txtPatientId.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoIsInvalid.ToString());
                txtPatientId.Focus();
                return;
            }

            AfyaPro_Types.clsPatient mPatient = pMdtRegistrations.Get_Patient(txtPatientId.Text);
            if (mPatient.Exist == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoDoesNotExist.ToString());
                txtPatientId.Focus();
                return;
            }

            if (pDtCartItems.Rows.Count == 0)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_BillingItemsToChargeAreMissing.ToString());
                grdBILBillPosting.Focus();
                return;
            }

            #endregion

            try
            {
                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                double mTotalDue = 0;
                foreach (DataRow mDataRow in pDtCartItems.Rows)
                {
                    mTotalDue = mTotalDue + Convert.ToDouble(mDataRow["amount"]);
                }

                double mUnRounded = mTotalDue;
                mTotalDue = Program.Round_Number(mTotalDue);

                if (mTotalDue < 0)
                {
                    return;
                }

                bool mPromptForPayment = true;

                #region control flow depending on customer group

                DataTable mDtClientGroups = pMdtClientGroups.View("code='" + pBillingGroupCode.Trim() + "'", "", "", "");

                if (mDtClientGroups.Rows.Count > 0)
                {
                    if (Convert.ToInt16(mDtClientGroups.Rows[0]["promptpayment"]) == 0)
                    {
                        mPromptForPayment = false;
                    }
                }

                #endregion

                if (pAffectBillsDirect == false)
                {
                    mPromptForPayment = false;
                }

                #region do payments

                pPaymentsDone = true;

                if (mPromptForPayment == true)
                {
                    pPaymentsDone = false;

                    frmBILFinalPayment mBILFinalPayment = new frmBILFinalPayment(mTotalDue, true);
                    mBILFinalPayment.PatientId = txtPatientId.Text.Trim();
                    mBILFinalPayment.ReceiptNo = pReceiptNo;
                    mBILFinalPayment.TotalPaid = pTotalPaid;
                    mBILFinalPayment.DtPayments = pDtPayments;
                    mBILFinalPayment.PaymentsDone = pPaymentsDone;
                    mBILFinalPayment.PostingBill = true;

                    mBILFinalPayment.ShowDialog();

                    pReceiptNo = mBILFinalPayment.ReceiptNo;
                    pTotalPaid = mBILFinalPayment.TotalPaid;
                    pDtPayments = mBILFinalPayment.DtPayments;
                    pPaymentsDone = mBILFinalPayment.PaymentsDone;
                }

                #endregion

                if (mTotalDue > pTotalPaid)
                {
                    if (txtInvoiceNo.Text.Trim() == "" && txtInvoiceNo.Text.Trim().ToLower() != "<<---new--->>")
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_InvoiceNoIsInvalid.ToString());
                        txtInvoiceNo.Focus();
                        return;
                    }
                }

                if (txtInvoiceNo.Text.Trim().ToLower() == "<<---new--->>")
                {
                    mGenerateInvoiceNo = 1;
                }

                if (pReceiptNo.Trim().ToLower() == "<<---new--->>")
                {
                    mGenerateReceiptNo = 1;
                }

                if (pPaymentsDone == true)
                {
                    #region company description

                    string mCompanyDescription = "";

                    if (Program.gDtCompanySetup.Rows.Count > 0)
                    {
                        mCompanyDescription = Program.gDtCompanySetup.Rows[0]["facilitydescription"].ToString().Trim();
                        if (Program.gDtCompanySetup.Rows[0]["box"].ToString().Trim() != "")
                        {
                            mCompanyDescription = mCompanyDescription + Environment.NewLine +
                                Program.gDtCompanySetup.Rows[0]["box"].ToString().Trim();
                        }
                        if (Program.gDtCompanySetup.Rows[0]["street"].ToString().Trim() != "")
                        {
                            mCompanyDescription = mCompanyDescription + Environment.NewLine +
                                Program.gDtCompanySetup.Rows[0]["street"].ToString().Trim();
                        }
                    }

                    #endregion

                    DataTable mDtBookingDetails = new DataTable("bookingdetails");
                    DataTable mDtAdmissionDetails = new DataTable("admissiondetails");
                    DataTable mDtDischargeDetails = new DataTable("dischargedetails");
                    DataTable mDtDiagnosesPrescriptions = new DataTable("diagnosisprescriptions");
                    DataTable mDtDiagnosesReferalInfo = new DataTable("diagnosisreferalinfo");
                    AfyaPro_Types.clsPatientDiagnosis mPatientDiagnosis = null;

                    #region initiated from IPD admission

                    if (gIpdAdmission != null)
                    {
                        mDtAdmissionDetails.Columns.Add("admissionid", typeof(System.Int32));
                        mDtAdmissionDetails.Columns.Add("wardcode", typeof(System.String));
                        mDtAdmissionDetails.Columns.Add("roomcode", typeof(System.String));
                        mDtAdmissionDetails.Columns.Add("bedno", typeof(System.String));
                        mDtAdmissionDetails.Columns.Add("remarks", typeof(System.String));
                        mDtAdmissionDetails.Columns.Add("weight", typeof(System.Double));
                        mDtAdmissionDetails.Columns.Add("temperature", typeof(System.Double));

                        frmIPDRegistrations mIPDRegistrations = (frmIPDRegistrations)gIpdAdmission;
                        DataRow mNewRow = mDtAdmissionDetails.NewRow();
                        mNewRow["admissionid"] = mIPDRegistrations.gAdmissionId;
                        mNewRow["wardcode"] = mIPDRegistrations.gWardCode;
                        mNewRow["roomcode"] = mIPDRegistrations.gRoomCode;
                        mNewRow["bedno"] = mIPDRegistrations.gBedNo;
                        mNewRow["remarks"] = mIPDRegistrations.gRemarks;
                        mNewRow["weight"] = mIPDRegistrations.gWeight;
                        mNewRow["temperature"] = mIPDRegistrations.gTemperature;
                        mDtAdmissionDetails.Rows.Add(mNewRow);
                        mDtAdmissionDetails.AcceptChanges();
                    }

                    #endregion

                    #region initiated from IPD discharge

                    if (gIpdDischarge != null)
                    {
                        mDtDischargeDetails.Columns.Add("admissionid", typeof(System.Int32));
                        mDtDischargeDetails.Columns.Add("wardcode", typeof(System.String));
                        mDtDischargeDetails.Columns.Add("roomcode", typeof(System.String));
                        mDtDischargeDetails.Columns.Add("bedno", typeof(System.String));
                        mDtDischargeDetails.Columns.Add("dischargestatuscode", typeof(System.String));
                        mDtDischargeDetails.Columns.Add("dischargestatusdescription", typeof(System.String));
                        mDtDischargeDetails.Columns.Add("remarks", typeof(System.String));

                        frmIPDDischarges mIPDDischarges = (frmIPDDischarges)gIpdDischarge;
                        DataRow mNewRow = mDtDischargeDetails.NewRow();
                        mNewRow["admissionid"] = mIPDDischarges.gAdmissionId;
                        mNewRow["wardcode"] = mIPDDischarges.gWardCode;
                        mNewRow["roomcode"] = mIPDDischarges.gRoomCode;
                        mNewRow["bedno"] = mIPDDischarges.gBedNo;
                        mNewRow["dischargestatuscode"] = mIPDDischarges.cboDischargeStatus.GetColumnValue("code").ToString();
                        mNewRow["dischargestatusdescription"] = mIPDDischarges.cboDischargeStatus.GetColumnValue("description").ToString();
                        mNewRow["remarks"] = mIPDDischarges.gRemarks;
                        mDtDischargeDetails.Rows.Add(mNewRow);
                        mDtDischargeDetails.AcceptChanges();
                    }

                    #endregion

                    #region initiated from OPD

                    if (gOpdRegistration != null)
                    {
                        mDtBookingDetails.Columns.Add("treatmentpointcode", typeof(System.String));
                        mDtBookingDetails.Columns.Add("treatmentpoint", typeof(System.String));
                        mDtBookingDetails.Columns.Add("weight", typeof(System.Double));
                        mDtBookingDetails.Columns.Add("temperature", typeof(System.Double));

                        frmOPDRegistrations mOPDRegistrations = (frmOPDRegistrations)gOpdRegistration;
                        DataRow mNewRow = mDtBookingDetails.NewRow();
                        mNewRow["treatmentpointcode"] = pTreatmentPointCode;
                        mNewRow["treatmentpoint"] = pTreatmentPoint;
                        mNewRow["weight"] = mOPDRegistrations.gWeight;
                        mNewRow["temperature"] = mOPDRegistrations.gTemperature;
                        mDtBookingDetails.Rows.Add(mNewRow);
                        mDtBookingDetails.AcceptChanges();
                    }

                    #endregion

                    #region initiated from IPD DiagnosesAndTreatments

                    if (gPatientDiagnosisForm != null)
                    {
                        frmDXTEpisodeVisitDetails mDXTEpisodeVisitDetails = (frmDXTEpisodeVisitDetails)gPatientDiagnosisForm;
                        mPatientDiagnosis = new AfyaPro_Types.clsPatientDiagnosis();

                        mPatientDiagnosis.diagnosiscode = mDXTEpisodeVisitDetails.gPatientDiagnosis.diagnosiscode;
                        mPatientDiagnosis.isprimary = mDXTEpisodeVisitDetails.gPatientDiagnosis.isprimary;
                        mPatientDiagnosis.doctorcode = mDXTEpisodeVisitDetails.gPatientDiagnosis.doctorcode;
                        mPatientDiagnosis.history = mDXTEpisodeVisitDetails.gPatientDiagnosis.history;
                        mPatientDiagnosis.examination = mDXTEpisodeVisitDetails.gPatientDiagnosis.examination;
                        mPatientDiagnosis.investigation = mDXTEpisodeVisitDetails.gPatientDiagnosis.investigation;
                        mPatientDiagnosis.treatments = mDXTEpisodeVisitDetails.gPatientDiagnosis.treatments;
                        mPatientDiagnosis.referaldescription = mDXTEpisodeVisitDetails.gPatientDiagnosis.referaldescription;
                        mPatientDiagnosis.episodecode = mDXTEpisodeVisitDetails.gPatientDiagnosis.episodecode;
                    }

                    #endregion

                    if (mUnRounded != 0)
                    {
                        foreach (DataRow mDataRow in pDtCartItems.Rows)
                        {
                            double mNewAmount = (Convert.ToDouble(mDataRow["amount"]) / mUnRounded) * mTotalDue;
                            if (Program.gRoundingDecimals == 0)
                            {
                                mNewAmount = Math.Round(mNewAmount, Program.gRoundingDecimals);
                            }

                            mDataRow.BeginEdit();
                            mDataRow["amount"] = mNewAmount;
                            mDataRow.EndEdit();
                            pDtCartItems.AcceptChanges();
                        }
                    }

                    mBill = pMdtBilling.Bill_Patient(mGenerateInvoiceNo, mGenerateReceiptNo, txtInvoiceNo.Text, pReceiptNo,
                        mTransDate, txtPatientId.Text, pBillingGroupCode, pBillingSubGroupCode, txtMembershipNo.Text,
                        cboPriceCategory.GetColumnValue("pricename").ToString(),
                        mTotalDue, pDiscount, pTotalPaid, Program.gLocalCurrencyCode, Program.gLocalCurrencyDesc,
                        Program.gLocalCurrencySymbol, mCompanyDescription, pDtPayments, pDtCartItems,
                        Convert.ToBoolean(Program.gAffectStockAtCashier),
                        pProcessIncomingBill, pAffectBillsDirect, mDtBookingDetails, mDtAdmissionDetails,
                        mDtDischargeDetails, mPatientDiagnosis, Program.gCurrentUser.Code, Program.gCurrentUser.Description);
                    if (mBill.Exe_Result == 0)
                    {
                        Program.Display_Error(mBill.Exe_Message);
                        return;
                    }
                    if (mBill.Exe_Result == -1)
                    {
                        Program.Display_Server_Error(mBill.Exe_Message);
                        return;
                    }

                    if (gOpdRegistration != null)
                    {
                        frmOPDRegistrations mOPDRegistrations = (frmOPDRegistrations)gOpdRegistration;
                        mOPDRegistrations.gCurrentBooking.IsNewAttendance = mBill.IsNewAttendance;
                        mOPDRegistrations.gBillingOk = true;
                        this.Close();
                    }
                    if (gIpdAdmission != null)
                    {
                        frmIPDRegistrations mIPDRegistrations = (frmIPDRegistrations)gIpdAdmission;
                        mIPDRegistrations.gCurrentBooking.IsNewAttendance = mBill.IsNewAttendance;
                        mIPDRegistrations.gBillingOk = true;
                        this.Close();
                    }
                    if (gIpdDischarge != null)
                    {
                        frmIPDDischarges mIPDDischarges = (frmIPDDischarges)gIpdDischarge;
                        mIPDDischarges.gBillingOk = true;
                        this.Close();
                    }
                    if (gPatientDiagnosisForm != null)
                    {
                        frmDXTEpisodeVisitDetails mDXTEpisodeVisitDetails = (frmDXTEpisodeVisitDetails)gPatientDiagnosisForm;
                        mDXTEpisodeVisitDetails.gBillingOk = true;
                        this.Close();
                    }
                    else
                    {
                        txtPatientId.Text = "";
                        this.Data_Clear();
                        txtPatientId.Focus();
                    }
                }

                #region receipt/invoice printing

                if (mBill.InvoiceNo.Trim() != "")
                {
                    frmOPDPatientDocuments mOPDPatientDocuments = new frmOPDPatientDocuments();
                    mOPDPatientDocuments.AutoPrint_Invoice(mBill.InvoiceNo);
                }
                else if (mBill.InvoiceNo.Trim() == "" && mBill.ReceiptNo.Trim() != "")
                {
                    frmOPDPatientDocuments mOPDPatientDocuments = new frmOPDPatientDocuments();
                    mOPDPatientDocuments.AutoPrint_Receipt(mBill.ReceiptNo);
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

        #region cmdSave_Click
        private void cmdSave_Click(object sender, EventArgs e)
        {
            this.Save_Bill();
        }
        #endregion

        #region cmdSearch_Click
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            pSearchingPatient = true;

            frmSearchPatient mSearchPatient = new frmSearchPatient(txtPatientId);
            mSearchPatient.ShowDialog();
            if (mSearchPatient.SearchingDone == true)
            {
                txtItemCode.Focus();
            }

            pSearchingPatient = false;
        }
        #endregion

        #region txtPatientId_EditValueChanged
        private void txtPatientId_EditValueChanged(object sender, EventArgs e)
        {
            if (pSearchingPatient == true)
            {
                this.gCurrentPatient = this.Search_Patient();
                this.Data_Display(gCurrentPatient);
            }
        }
        #endregion

        #region billing item

        #region qty, price, amount changes

        private void txtQuantity_EditValueChanged(object sender, EventArgs e)
        {
            double mQuantity = 0;
            double mPrice = 0;

            if (pQuantityChanging == true)
            {
                if (Program.IsMoney(txtItemQuantity.Text) == true)
                {
                    mQuantity = Convert.ToDouble(txtItemQuantity.Text);
                }

                if (Program.IsMoney(txtItemPrice.Text) == true)
                {
                    mPrice = Convert.ToDouble(txtItemPrice.Text);
                }

                double mAmount = mQuantity * mPrice;

                txtItemAmount.Text = mAmount.ToString("c");
            }
        }

        private void txtPrice_EditValueChanged(object sender, EventArgs e)
        {
            double mQuantity = 0;
            double mPrice = 0;

            if (pPriceChanging == true)
            {
                if (Program.IsMoney(txtItemQuantity.Text) == true)
                {
                    mQuantity = Convert.ToDouble(txtItemQuantity.Text);
                }

                if (Program.IsMoney(txtItemPrice.Text) == true)
                {
                    mPrice = Convert.ToDouble(txtItemPrice.Text);
                }

                double mAmount = mQuantity * mPrice;

                txtItemAmount.Text = mAmount.ToString("c");
            }
        }

        private void txtAmount_EditValueChanged(object sender, EventArgs e)
        {
            double mQuantity = 0;
            double mAmount = 0;

            if (pAmountChanging == true)
            {
                if (Program.IsMoney(txtItemQuantity.Text) == true)
                {
                    mQuantity = Convert.ToDouble(txtItemQuantity.Text);
                }

                if (Program.IsMoney(txtItemAmount.Text) == true)
                {
                    mAmount = Convert.ToDouble(txtItemAmount.Text);
                }

                if (mQuantity == 0)
                {
                    txtItemPrice.Text = Convert.ToDouble("0").ToString("c");
                    return;
                }

                double mPrice = mAmount / mQuantity;
                txtItemPrice.Text = mPrice.ToString("c");
            }
        }

        private void txtQuantity_Enter(object sender, EventArgs e)
        {
            pPriceChanging = false;
            pAmountChanging = false;
            pQuantityChanging = true;
        }

        private void txtPrice_Enter(object sender, EventArgs e)
        {
            pQuantityChanging = false;
            pAmountChanging = false;
            pPriceChanging = true;
        }

        private void txtAmount_Enter(object sender, EventArgs e)
        {
            pPriceChanging = false;
            pQuantityChanging = false;
            pAmountChanging = true;
        }

        #endregion

        #region Display_BillingItem
        private void Display_BillingItem()
        {
            string mFunctionName = "Display_BillingItem";

            try
            {
                if (cboPriceCategory.ItemIndex == -1)
                {
                    return;
                }

                string mPriceName = cboPriceCategory.GetColumnValue("pricename").ToString().Trim();

                DataTable mDtItems = pMdtBillItems.View("code='" + txtItemCode.Text.Trim() + "'", "", "", "");
                if (mDtItems.Rows.Count == 0)
                {
                    return;
                }

                double mPricingPercent = 100;
                if (pBillingGroupCode.Trim() != "")
                {
                    DataTable mDtGroupItems = pMdtClientGroups.View_Items(
                        "groupcode='" + pBillingGroupCode.Trim()
                        + "' and itemcode='" + txtItemCode.Text.Trim() + "'", "", "", "");
                    if (mDtGroupItems.Rows.Count > 0)
                    {
                        mPricingPercent = Convert.ToDouble(mDtGroupItems.Rows[0]["pricingpercent"]);
                    }
                    else
                    {
                        if (pItemsSensitive == 1)
                        {
                            Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_ItemIsNotApplicableToCurrentGroup.ToString());
                            txtItemCode.Focus();
                            return;
                        }
                    }
                }

                //get price
                double mPrice = Convert.ToDouble(mDtItems.Rows[0][mPriceName]);

                //get price as percentage
                mPrice = (mPricingPercent / 100) * mPrice;

                double mQuantity = 0;

                if (Convert.ToDouble(mDtItems.Rows[0]["defaultqty"]) > 0)
                {
                    mQuantity = Convert.ToDouble(mDtItems.Rows[0]["defaultqty"]);
                }

                #region get # of days if item is for IPD admission

                if (Convert.ToInt16(mDtItems.Rows[0]["foripdadmission"]) == 1)
                {
                    AfyaPro_Types.clsBooking mCurrentBooking = pMdtRegistrations.Get_Booking(txtPatientId.Text);

                    if (mCurrentBooking != null)
                    {
                        if (mCurrentBooking.IsBooked == true)
                        {
                            AfyaPro_Types.clsAdmission mCurrentAdmission = pMdtRegistrations.Get_Admission(
                                mCurrentBooking.Booking, txtPatientId.Text);

                            if (mCurrentAdmission != null)
                            {
                                if (mCurrentAdmission.IsAdmitted == true)
                                {
                                    if (mCurrentAdmission.TransDate.Date == DateTime.Now.Date)
                                    {
                                        mQuantity = 1;
                                    }
                                    else
                                    {
                                        TimeSpan mTimeSpan = DateTime.Now.Date.Subtract(mCurrentAdmission.TransDate);
                                        mQuantity = mTimeSpan.Days;
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                txtItemQuantity.Text = mQuantity.ToString();
                txtItemCode.Text = mDtItems.Rows[0]["code"].ToString();
                txtItemDescription.Text = mDtItems.Rows[0]["description"].ToString();
                txtItemPrice.Text = mPrice.ToString();

                double mAmount = mPrice * mQuantity;
                txtItemQuantity.Text = mQuantity.ToString();
                txtItemAmount.Text = mAmount.ToString();

                if (Convert.ToInt16(mDtItems.Rows[0]["addtocart"]) == 1)
                {
                    this.Add_ToCart();
                }
                else
                {
                    txtItemQuantity.SelectAll();
                    txtItemQuantity.Focus();
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Search_BillingItem
        private void Search_BillingItem()
        {
            string mFunctionName = "Search_BillingItem";

            try
            {
                if (cboPriceCategory.ItemIndex == -1)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_PriceCategoryIsInvalid.ToString());
                    cboPriceCategory.Focus();
                    return;
                }

                pSearchingBillingItem = true;

                bool mItemsSensitive = false;
                DataTable mDtGroupItems = null;

                if (pItemsSensitive == 1)
                {
                    mItemsSensitive = true;

                    mDtGroupItems = pMdtClientGroups.View_Items("groupcode='" + pBillingGroupCode.Trim() + "'", "", "", "");
                }

                frmSearchBillingItem mSearchBillingItem = new frmSearchBillingItem(
                    txtItemCode, mItemsSensitive, mDtGroupItems, cboPriceCategory.GetColumnValue("pricename").ToString());
                mSearchBillingItem.ShowDialog();
                if (mSearchBillingItem.SearchingDone == true)
                {
                    txtItemQuantity.Focus();
                }

                pSearchingBillingItem = false;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdSearchItem_Click
        private void cmdSearchItem_Click(object sender, EventArgs e)
        {
            this.Search_BillingItem();
        }
        #endregion

        #region txtItemCode_EditValueChanged
        private void txtItemCode_EditValueChanged(object sender, EventArgs e)
        {
            if (pSearchingBillingItem == true)
            {
                this.Display_BillingItem();
            }
        }
        #endregion

        #region txtItemCode_KeyDown
        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Display_BillingItem();
            }
        }
        #endregion

        #region txtItemCode_Leave
        private void txtItemCode_Leave(object sender, EventArgs e)
        {
            pCurrItemCode = txtItemCode.Text;

            if (pCurrItemCode.Trim().ToLower() != pPrevItemCode.Trim().ToLower())
            {
                this.Display_BillingItem();
            }
        }
        #endregion

        #region txtItemQuantity_KeyDown
        private void txtItemQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Add_ToCart();
            }
        }
        #endregion

        #region txtItemPrice_KeyDown
        private void txtItemPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Add_ToCart();
            }
        }
        #endregion

        #region txtItemAmount_KeyDown
        private void txtItemAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Add_ToCart();
            }
        }
        #endregion

        #region Add_ToCart
        private void Add_ToCart()
        {
            DataView mDvAllItems = new DataView();
            string mFunctionName = "Add_ToCart";

            if (pProcessIncomingBill == true)
            {
                bool mCanAccess = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.bilpostbills_incomingbilladd.ToString());

                if (mCanAccess == false)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString());
                    return;
                }
            }

            #region validation

            #region transaction date

            if (Program.IsDate(Program.gMdiForm.txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                return;
            }

            #endregion

            #region patient

            if (txtPatientId.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoIsInvalid.ToString());
                txtPatientId.Focus();
                return;
            }

            AfyaPro_Types.clsPatient mPatient = pMdtRegistrations.Get_Patient(txtPatientId.Text);
            if (mPatient.Exist == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoDoesNotExist.ToString());
                txtPatientId.Focus();
                return;
            }

            #endregion

            #region booking

            if (gCurrentBooking.IsBooked == false)
            {
                gCurrentBooking = pMdtRegistrations.Get_Booking(txtPatientId.Text);
            }

            if (gCurrentBooking.IsBooked == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientIsNotBooked.ToString());
                return;
            }
            else
            {
                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                if (gCurrentBooking.BookDate.Date != mTransDate.Date)
                {
                    //retry getting booking
                    gCurrentBooking = pMdtRegistrations.Get_Booking(txtPatientId.Text);
                    if (gCurrentBooking.IsBooked == false)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientIsNotBooked.ToString());
                        return;
                    }
                    else
                    {
                        if (Program.GrantDeny_FunctionAccess(
                            AfyaPro_Types.clsSystemFunctions.FunctionKeys.bilpostbills_billunbooked.ToString()) == false)
                        {
                            if (gCurrentBooking.BookDate.Date != mTransDate.Date)
                            {
                                if (gCurrentBooking.Department.Trim().ToLower() !=
                                    AfyaPro_Types.clsEnums.PatientCategories.IPD.ToString().ToLower())
                                {
                                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientIsNotBooked.ToString());
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            #endregion

            #region item

            if (cboPriceCategory.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_PriceCategoryIsInvalid.ToString());
                cboPriceCategory.Focus();
                return;
            }

            if (Program.IsMoney(txtItemQuantity.Text) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_QuantityIsInvalid.ToString());
                txtItemQuantity.Focus();
                return;
            }

            if (Program.IsMoney(txtItemPrice.Text) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_UnitPriceIsInvalid.ToString());
                txtItemPrice.Focus();
                return;
            }

            if (Program.IsMoney(txtItemAmount.Text) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_AmountIsInvalid.ToString());
                txtItemAmount.Focus();
                return;
            }

            #endregion

            #endregion

            try
            {
                DataTable mDtItems = pMdtBillItems.View("code='" + txtItemCode.Text.Trim() + "'", "", "", "");
                if (mDtItems.Rows.Count == 0)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BLS_BillingItemCodeIsInvalid.ToString());
                    txtItemCode.Focus();
                    return;
                }

                if (pBillingGroupCode.Trim() != "")
                {
                    DataTable mDtGroupItems = pMdtClientGroups.View_Items(
                        "groupcode='" + pBillingGroupCode.Trim()
                        + "' and itemcode='" + txtItemCode.Text.Trim() + "'", "", "", "");
                    if (mDtGroupItems.Rows.Count == 0 && pItemsSensitive == 1)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_ItemIsNotApplicableToCurrentGroup.ToString());
                        txtItemCode.Focus();
                        return;
                    }
                }

                string mItemGroupCode = mDtItems.Rows[0]["groupcode"].ToString().Trim();
                string mItemSubGroupCode = mDtItems.Rows[0]["subgroupcode"].ToString().Trim();
                string mItemGroupDescription = "";
                string mItemSubGroupDescription = "";

                DataTable mDtGroups = pMdtBillItemGroups.View("code='" + mItemGroupCode + "'", "", "", "");
                if (mDtGroups.Rows.Count > 0)
                {
                    mItemGroupDescription = mDtGroups.Rows[0]["description"].ToString().Trim();
                }

                DataTable mDtSubGroups = pMdtBillItemSubGroups.View("code='" + mItemGroupCode + "'", "", "", "");
                if (mDtSubGroups.Rows.Count > 0)
                {
                    mItemSubGroupDescription = mDtSubGroups.Rows[0]["description"].ToString().Trim();
                }

                DataRow mNewRow = pDtCartItems.NewRow();
                mNewRow["itemgroupcode"] = mItemGroupCode;
                mNewRow["itemgroupdescription"] = mItemGroupDescription;
                mNewRow["itemsubgroupcode"] = mItemSubGroupCode;
                mNewRow["itemsubgroupdescription"] = mItemSubGroupDescription;
                mNewRow["itemcode"] = mDtItems.Rows[0]["code"].ToString().Trim();
                mNewRow["itemdescription"] = mDtItems.Rows[0]["description"].ToString().Trim();
                mNewRow["expirydate"] = DBNull.Value;
                mNewRow["storecode"] = "";
                mNewRow["qty"] = Convert.ToDouble(txtItemQuantity.Text);
                mNewRow["unitprice"] = Convert.ToDouble(txtItemPrice.Text);
                mNewRow["actualamount"] = Convert.ToDouble(txtItemAmount.Text);
                mNewRow["amount"] = Convert.ToDouble(txtItemAmount.Text);
                mNewRow["storedescription"] = "";
                mNewRow["itemopmcode"] = "";
                mNewRow["itemopmdescription"] = "";
                mNewRow["packagingcode"] = "";
                mNewRow["packagingdescription"] = "";
                mNewRow["piecesinpackage"] = 1;
                mNewRow["autocode"] = 0;

                pDtCartItems.Rows.Add(mNewRow);
                pDtCartItems.AcceptChanges();

                this.Clear_Item();
                this.Get_Total();

                DevExpress.XtraGrid.Views.Grid.GridView mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)grdBILBillPosting.Views[0];
                mGridView.ExpandAllGroups();

                txtItemCode.Focus();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdAdd_Click
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            this.Add_ToCart();
        }
        #endregion

        #region cmdUpdate_Click
        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView mGridView =
            (DevExpress.XtraGrid.Views.Grid.GridView)grdBILBillPosting.MainView;

            if (mGridView.FocusedRowHandle < 0)
            {
                return;
            }

            DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

            string mAutoCode = mSelectedRow["autocode"].ToString();

            if (Convert.ToInt32(mSelectedRow["autocode"]) > 0)
            {
                //bool mCanAccess = Program.GrantDeny_FunctionAccess(
                //    AfyaPro_Types.clsSystemFunctions.FunctionKeys.bilpostbills_incomingbilledit.ToString());

                //if (mCanAccess == false)
                //{
                //    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString());
                //    return;
                //}
            }

            mSelectedRow.BeginEdit();
            mSelectedRow["qty"] = Convert.ToDouble(txtItemQuantity.Text);
            mSelectedRow["unitprice"] = Convert.ToDouble(txtItemPrice.Text);
            mSelectedRow["actualamount"] = Convert.ToDouble(txtItemAmount.Text);
            mSelectedRow["amount"] = Convert.ToDouble(txtItemAmount.Text);
            mSelectedRow.EndEdit();
            pDtCartItems.AcceptChanges();

            this.Clear_Item();
            this.Clear_Pharmacy();
            this.Get_Total();

            txtItemCode.Focus();
        }
        #endregion

        #region Remove_FromCart
        private void Remove_FromCart()
        {
            string mFunctionName = "Remove_FromCart";

            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                (DevExpress.XtraGrid.Views.Grid.GridView)grdBILBillPosting.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);
                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                if (Convert.ToInt32(mSelectedRow["autocode"]) > 0)
                {
                    bool mCanAccess = Program.GrantDeny_FunctionAccess(
                        AfyaPro_Types.clsSystemFunctions.FunctionKeys.bilpostbills_incomingbilldelete.ToString());

                    if (mCanAccess == false)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString());
                        return;
                    }

                    DialogResult mDialogResult = Program.Display_Question("Are you sure you want to remove the selected item", MessageBoxDefaultButton.Button2);
                    if (mDialogResult != System.Windows.Forms.DialogResult.Yes)
                    {
                        return;
                    }

                    AfyaPro_Types.clsBill mBill = pMdtBilling.Remove_ItemFromCart(
                        mTransDate, Convert.ToInt32(mSelectedRow["autocode"]), Program.gCurrentUser.Code);
                    if (mBill.Exe_Result == 0)
                    {
                        Program.Display_Error(mBill.Exe_Message);
                        return;
                    }
                    if (mBill.Exe_Result == -1)
                    {
                        Program.Display_Server_Error(mBill.Exe_Message);
                        return;
                    }
                }

                mGridView.DeleteSelectedRows();
                pDtCartItems.AcceptChanges();
                this.Clear_Item();
                this.Clear_Pharmacy();
                this.Get_Total();

                txtItemCode.Focus();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdRemove_Click
        private void cmdRemove_Click(object sender, EventArgs e)
        {
            this.Remove_FromCart();
        }
        #endregion

        #endregion

        #region pharmacy item

        #region qty, price, amount changes

        private void txtPharmQuantity_EditValueChanged(object sender, EventArgs e)
        {
            double mQuantity = 0;
            double mPrice = 0;

            if (pQuantityChanging == true)
            {
                if (Program.IsMoney(txtPharmQuantity.Text) == true)
                {
                    mQuantity = Convert.ToDouble(txtPharmQuantity.Text);
                }

                if (Program.IsMoney(txtPharmPrice.Text) == true)
                {
                    mPrice = Convert.ToDouble(txtPharmPrice.Text);
                }

                double mAmount = mQuantity * mPrice;

                txtPharmAmount.Text = mAmount.ToString("c");
            }
        }

        private void txtPharmPrice_EditValueChanged(object sender, EventArgs e)
        {
            double mQuantity = 0;
            double mPrice = 0;

            if (pPriceChanging == true)
            {
                if (Program.IsMoney(txtPharmQuantity.Text) == true)
                {
                    mQuantity = Convert.ToDouble(txtPharmQuantity.Text);
                }

                if (Program.IsMoney(txtPharmPrice.Text) == true)
                {
                    mPrice = Convert.ToDouble(txtPharmPrice.Text);
                }

                double mAmount = mQuantity * mPrice;

                txtPharmAmount.Text = mAmount.ToString("c");
            }
        }

        private void txtPharmAmount_EditValueChanged(object sender, EventArgs e)
        {
            double mQuantity = 0;
            double mAmount = 0;

            if (pAmountChanging == true)
            {
                if (Program.IsMoney(txtPharmQuantity.Text) == true)
                {
                    mQuantity = Convert.ToDouble(txtPharmQuantity.Text);
                }

                if (Program.IsMoney(txtPharmAmount.Text) == true)
                {
                    mAmount = Convert.ToDouble(txtPharmAmount.Text);
                }

                if (mQuantity == 0)
                {
                    txtPharmPrice.Text = Convert.ToDouble("0").ToString("c");
                    return;
                }

                double mPrice = mAmount / mQuantity;
                txtPharmPrice.Text = mPrice.ToString("c");
            }
        }

        private void txtPharmQuantity_Enter(object sender, EventArgs e)
        {
            pPriceChanging = false;
            pAmountChanging = false;
            pQuantityChanging = true;
        }

        private void txtPharmPrice_Enter(object sender, EventArgs e)
        {
            pQuantityChanging = false;
            pAmountChanging = false;
            pPriceChanging = true;
        }

        private void txtPharmAmount_Enter(object sender, EventArgs e)
        {
            pPriceChanging = false;
            pQuantityChanging = false;
            pAmountChanging = true;
        }

        #endregion

        #region Display_PharmacyItem
        private void Display_PharmacyItem()
        {
            string mFunctionName = "Display_PharmacyItem";

            try
            {
                if (cboPriceCategory.ItemIndex == -1)
                {
                    return;
                }

                string mPriceName = cboPriceCategory.GetColumnValue("pricename").ToString().Trim();

                DataTable mDtItems = pMdtSomProducts.View("code='" + txtPharmCode.Text.Trim() + "'", "", "", "");
                if (mDtItems.Rows.Count == 0)
                {
                    return;
                }

                //get price
                double mPrice = Convert.ToDouble(mDtItems.Rows[0][mPriceName]);
                double mQuantity = 0;

                if (Convert.ToDouble(mDtItems.Rows[0]["defaultqty"]) > 0)
                {
                    mQuantity = Convert.ToDouble(mDtItems.Rows[0]["defaultqty"]);
                }

                txtPharmQuantity.Text = mQuantity.ToString();
                txtPharmCode.Text = mDtItems.Rows[0]["code"].ToString();
                txtPharmDescription.Text = mDtItems.Rows[0]["description"].ToString();
                txtPharmPrice.Text = mPrice.ToString();
                
                double mAmount = mPrice * mQuantity;
                txtPharmQuantity.Text = mQuantity.ToString();
                txtPharmAmount.Text = mAmount.ToString();

                txtPharmQuantity.SelectAll();
                txtPharmQuantity.Focus();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Search_PharmacyItem
        private void Search_PharmacyItem()
        {
            string mFunctionName = "Search_PharmacyItem";

            try
            {
                if (cboStore.ItemIndex == -1)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreDescriptionIsInvalid.ToString());
                    cboStore.Focus();
                    return;
                }

                string mStoreCode = cboStore.GetColumnValue("code").ToString().Trim();

                if (mStoreCode == "")
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreDescriptionIsInvalid.ToString());
                    cboStore.Focus();
                    return;
                }

                pSearchingPharmacyItem = true;

                frmSearchProductStock mSearchProductStock = new frmSearchProductStock(mStoreCode);
                mSearchProductStock.ShowDialog();
                if (mSearchProductStock.SearchingDone == true)
                {
                    txtPharmCode.Text = mSearchProductStock.SelectedRow["code"].ToString().Trim();
                    txtPharmQuantity.Focus();
                }

                pSearchingPharmacyItem = false;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdPharmSearchItem_Click
        private void cmdPharmSearchItem_Click(object sender, EventArgs e)
        {
            this.Search_PharmacyItem();
        }
        #endregion

        #region txtPharmCode_EditValueChanged
        private void txtPharmCode_EditValueChanged(object sender, EventArgs e)
        {
            if (pSearchingPharmacyItem == true)
            {
                this.Display_PharmacyItem();
            }
        }
        #endregion

        #region txtPharmCode_KeyDown
        private void txtPharmCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Display_PharmacyItem();
            }
        }
        #endregion

        #region txtPharmCode_Leave
        private void txtPharmCode_Leave(object sender, EventArgs e)
        {
            pCurrPharmCode = txtPharmCode.Text;

            if (pCurrPharmCode.Trim().ToLower() != pPrevPharmCode.Trim().ToLower())
            {
                this.Display_PharmacyItem();
            }
        }
        #endregion

        #region txtPharmQuantity_KeyDown
        private void txtPharmQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Add_PharmToCart();
            }
        }
        #endregion

        #region txtPharmPrice_KeyDown
        private void txtPharmPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Add_PharmToCart();
            }
        }
        #endregion

        #region txtPharmAmount_KeyDown
        private void txtPharmAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Add_PharmToCart();
            }
        }
        #endregion

        #region Add_PharmToCart
        private void Add_PharmToCart()
        {
            DataView mDvAllItems = new DataView();
            string mFunctionName = "Add_PharmToCart";

            if (pProcessIncomingBill == true)
            {
                bool mCanAccess = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.bilpostbills_incomingbilladd.ToString());

                if (mCanAccess == false)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString());
                    return;
                }
            }

            #region validation

            #region transaction date

            if (Program.IsDate(Program.gMdiForm.txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                return;
            }

            #endregion

            #region patient

            if (txtPatientId.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoIsInvalid.ToString());
                txtPatientId.Focus();
                return;
            }

            AfyaPro_Types.clsPatient mPatient = pMdtRegistrations.Get_Patient(txtPatientId.Text);
            if (mPatient.Exist == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoDoesNotExist.ToString());
                txtPatientId.Focus();
                return;
            }

            #endregion

            #region booking

            if (gCurrentBooking.IsBooked == false)
            {
                gCurrentBooking = pMdtRegistrations.Get_Booking(txtPatientId.Text);
            }

            if (gCurrentBooking.IsBooked == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientIsNotBooked.ToString());
                return;
            }
            else
            {
                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                if (gCurrentBooking.BookDate.Date != mTransDate.Date)
                {
                    //retry getting booking
                    gCurrentBooking = pMdtRegistrations.Get_Booking(txtPatientId.Text);
                    if (gCurrentBooking.IsBooked == false)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientIsNotBooked.ToString());
                        return;
                    }
                    else
                    {
                        if (Program.GrantDeny_FunctionAccess(
                            AfyaPro_Types.clsSystemFunctions.FunctionKeys.bilpostbills_billunbooked.ToString()) == false)
                        {
                            if (gCurrentBooking.BookDate.Date != mTransDate.Date)
                            {
                                if (gCurrentBooking.Department.Trim().ToLower() !=
                                    AfyaPro_Types.clsEnums.PatientCategories.IPD.ToString().ToLower())
                                {
                                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientIsNotBooked.ToString());
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            #endregion

            #region item

            if (cboPriceCategory.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_PriceCategoryIsInvalid.ToString());
                cboPriceCategory.Focus();
                return;
            }

            if (Program.IsMoney(txtPharmQuantity.Text) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_QuantityIsInvalid.ToString());
                txtPharmQuantity.Focus();
                return;
            }

            if (Program.IsMoney(txtPharmPrice.Text) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_UnitPriceIsInvalid.ToString());
                txtPharmPrice.Focus();
                return;
            }

            if (Program.IsMoney(txtPharmAmount.Text) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_AmountIsInvalid.ToString());
                txtPharmAmount.Focus();
                return;
            }

            #endregion

            #endregion

            try
            {
                if (cboStore.ItemIndex == -1)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreDescriptionIsInvalid.ToString());
                    cboStore.Focus();
                    return;
                }

                string mStoreCode = cboStore.GetColumnValue("code").ToString().Trim();
                string mStoreDescription = cboStore.GetColumnValue("description").ToString().Trim();

                if (mStoreCode == "")
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreDescriptionIsInvalid.ToString());
                    cboStore.Focus();
                    return;
                }

                DataTable mDtItems = pMdtSomProducts.View("code='" + txtPharmCode.Text.Trim() + "'", "", "", "");
                if (mDtItems.Rows.Count == 0)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BLS_BillingItemCodeIsInvalid.ToString());
                    txtPharmCode.Focus();
                    return;
                }

                if (pBillingGroupCode.Trim() != "")
                {
                    DataTable mDtGroupItems = pMdtClientGroups.View_Items(
                        "groupcode='" + pBillingGroupCode.Trim()
                        + "' and itemcode='" + txtPharmCode.Text.Trim() + "'", "", "", "");
                    if (mDtGroupItems.Rows.Count == 0 && pItemsSensitive == 1)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_ItemIsNotApplicableToCurrentGroup.ToString());
                        txtItemCode.Focus();
                        return;
                    }
                }

                if (Convert.ToInt16(mDtItems.Rows[0]["hasexpiry"]) == 1 && Program.gAffectStockAtCashier == 1)
                {
                    #region expiry dates data entry

                    string mItemTransferId = "";
                    string mProductCode = mDtItems.Rows[0]["code"].ToString().Trim();
                    string mProductDescription = mDtItems.Rows[0]["description"].ToString().Trim();
                    string mPackagingDescription = "Each";
                    int mPiecesInPackage = 1;

                    DataTable mDtExpiryDates = new DataTable("expirydates");
                    mDtExpiryDates.Columns.Add("itemtransferid", typeof(System.String));
                    mDtExpiryDates.Columns.Add("productcode", typeof(System.String));
                    mDtExpiryDates.Columns.Add("expirydate", typeof(System.DateTime));
                    mDtExpiryDates.Columns.Add("balance", typeof(System.Double));
                    mDtExpiryDates.Columns.Add("quantity", typeof(System.Double));
                    mDtExpiryDates.Columns.Add("packaging", typeof(System.String));

                    double mTotalToIssue = Convert.ToDouble(txtPharmQuantity.Text);

                    #region prepare suggested stock

                    DataTable mDtSuggestStock = pMdtSomStores.Get_OnHandQuantitiesByExpiryDates(
                        mStoreCode, "productcode='" + mProductCode + "'", "productcode,expirydate");

                    double mPendingQty = mTotalToIssue;
                    foreach (DataRow mDataRow in mDtSuggestStock.Rows)
                    {
                        if (Program.IsNullDate(mDataRow["expirydate"]) == false)
                        {
                            double mToIssue = mPendingQty;
                            double mOnHandQty = Convert.ToDouble(mDataRow["onhandqty"]) / mPiecesInPackage;

                            if (mOnHandQty < mToIssue)
                            {
                                mToIssue = mOnHandQty;
                            }

                            DataRow mNewRow = mDtExpiryDates.NewRow();
                            mNewRow["itemtransferid"] = mItemTransferId;
                            mNewRow["productcode"] = mProductCode;
                            mNewRow["expirydate"] = Convert.ToDateTime(mDataRow["expirydate"]);
                            mNewRow["packaging"] = mPackagingDescription;
                            mNewRow["balance"] = mOnHandQty;
                            mNewRow["quantity"] = mToIssue;
                            mDtExpiryDates.Rows.Add(mNewRow);
                            mDtExpiryDates.AcceptChanges();

                            mPendingQty = mPendingQty - mToIssue;
                        }
                    }

                    #endregion

                    frmIVSuggestStock mIVSuggestStock = new frmIVSuggestStock(mDtExpiryDates);
                    mIVSuggestStock.StoreCode = mStoreCode;
                    mIVSuggestStock.ItemTransferId = mItemTransferId;
                    mIVSuggestStock.PackagingDescription = mPackagingDescription;
                    mIVSuggestStock.PiecesInPackage = mPiecesInPackage;
                    mIVSuggestStock.txtItemCode.Text = mProductCode;
                    mIVSuggestStock.txtItemDescription.Text = mProductDescription;
                    mIVSuggestStock.TotalToIssue = mTotalToIssue;
                    mIVSuggestStock.ShowDialog();

                    #endregion

                    if (mIVSuggestStock.SuggestionAccepted == true)
                    {
                        foreach (DataRow mDataRow in mDtExpiryDates.Rows)
                        {
                            if (Convert.ToDouble(mDataRow["quantity"]) > 0)
                            {
                                DataRow mNewRow = pDtCartItems.NewRow();
                                mNewRow["itemgroupcode"] = "pharmacy";
                                mNewRow["itemgroupdescription"] = "Pharmacy";
                                mNewRow["itemsubgroupcode"] = mDtItems.Rows[0]["departmentcode"].ToString().Trim();
                                mNewRow["itemsubgroupdescription"] = mDtItems.Rows[0]["departmentdescription"].ToString().Trim();
                                mNewRow["itemcode"] = mDtItems.Rows[0]["code"].ToString().Trim();
                                mNewRow["itemdescription"] = mDtItems.Rows[0]["description"].ToString().Trim();
                                mNewRow["storecode"] = mStoreCode;
                                if (Program.IsNullDate(mDataRow["expirydate"]) == true)
                                {
                                    mNewRow["expirydate"] = DBNull.Value;
                                }
                                else
                                {
                                    mNewRow["expirydate"] = Convert.ToDateTime(mDataRow["expirydate"]);
                                }
                                mNewRow["qty"] = Convert.ToDouble(mDataRow["quantity"]);
                                mNewRow["unitprice"] = Convert.ToDouble(txtPharmPrice.Text);
                                mNewRow["actualamount"] = Convert.ToDouble(mNewRow["qty"]) * Convert.ToDouble(mNewRow["unitprice"]);
                                mNewRow["amount"] = Convert.ToDouble(mNewRow["qty"]) * Convert.ToDouble(mNewRow["unitprice"]);
                                mNewRow["storedescription"] = mStoreDescription;
                                mNewRow["itemopmcode"] = mDtItems.Rows[0]["opmcode"].ToString().Trim();
                                mNewRow["itemopmdescription"] = mDtItems.Rows[0]["opmdescription"].ToString().Trim();
                                mNewRow["packagingcode"] = "Each";
                                mNewRow["packagingdescription"] = "Each";
                                mNewRow["piecesinpackage"] = 1;
                                mNewRow["autocode"] = 0;

                                pDtCartItems.Rows.Add(mNewRow);
                                pDtCartItems.AcceptChanges();
                            }
                        }
                    }
                }
                else
                {
                    DataRow mNewRow = pDtCartItems.NewRow();
                    mNewRow["itemgroupcode"] = "pharmacy";
                    mNewRow["itemgroupdescription"] = "Pharmacy";
                    mNewRow["itemsubgroupcode"] = mDtItems.Rows[0]["departmentcode"].ToString().Trim();
                    mNewRow["itemsubgroupdescription"] = mDtItems.Rows[0]["departmentdescription"].ToString().Trim();
                    mNewRow["itemcode"] = mDtItems.Rows[0]["code"].ToString().Trim();
                    mNewRow["itemdescription"] = mDtItems.Rows[0]["description"].ToString().Trim();
                    mNewRow["storecode"] = cboStore.GetColumnValue("code").ToString().Trim();
                    mNewRow["expirydate"] = DBNull.Value;
                    mNewRow["qty"] = Convert.ToDouble(txtPharmQuantity.Text);
                    mNewRow["unitprice"] = Convert.ToDouble(txtPharmPrice.Text);
                    mNewRow["actualamount"] = Convert.ToDouble(txtPharmAmount.Text);
                    mNewRow["amount"] = Convert.ToDouble(txtPharmAmount.Text);
                    mNewRow["storedescription"] = mStoreDescription;
                    mNewRow["itemopmcode"] = mDtItems.Rows[0]["opmcode"].ToString().Trim();
                    mNewRow["itemopmdescription"] = mDtItems.Rows[0]["opmdescription"].ToString().Trim();
                    mNewRow["packagingcode"] = "Each";
                    mNewRow["packagingdescription"] = "Each";
                    mNewRow["piecesinpackage"] = 1;
                    mNewRow["autocode"] = 0;

                    pDtCartItems.Rows.Add(mNewRow);
                    pDtCartItems.AcceptChanges();
                }

                this.Clear_Pharmacy();
                this.Get_Total();

                DevExpress.XtraGrid.Views.Grid.GridView mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)grdBILBillPosting.Views[0];
                mGridView.ExpandAllGroups();

                txtPharmCode.Focus();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdPharmAdd_Click
        private void cmdPharmAdd_Click(object sender, EventArgs e)
        {
            this.Add_PharmToCart();
        }
        #endregion

        #region cmdPharmUpdate_Click
        private void cmdPharmUpdate_Click(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView mGridView =
            (DevExpress.XtraGrid.Views.Grid.GridView)grdBILBillPosting.MainView;

            if (mGridView.FocusedRowHandle < 0)
            {
                return;
            }

            DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

            if (Convert.ToInt32(mSelectedRow["autocode"]) > 0)
            {
                //bool mCanAccess = Program.GrantDeny_FunctionAccess(
                //    AfyaPro_Types.clsSystemFunctions.FunctionKeys.bilpostbills_incomingbilledit.ToString());

                //if (mCanAccess == false)
                //{
                //    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString());
                //    return;
                //}
            }

            mSelectedRow.BeginEdit();
            mSelectedRow["qty"] = Convert.ToDouble(txtPharmQuantity.Text);
            mSelectedRow["unitprice"] = Convert.ToDouble(txtPharmPrice.Text);
            mSelectedRow["actualamount"] = Convert.ToDouble(txtPharmAmount.Text);
            mSelectedRow["amount"] = Convert.ToDouble(txtPharmAmount.Text);
            mSelectedRow.EndEdit();
            pDtCartItems.AcceptChanges();

            this.Clear_Item();
            this.Clear_Pharmacy();
            this.Get_Total();

            txtPharmCode.Focus();
        }
        #endregion

        #region Remove_PharmacyItemFromCart
        private void Remove_PharmacyItemFromCart()
        {
            string mFunctionName = "Remove_PharmacyItemFromCart";

            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                (DevExpress.XtraGrid.Views.Grid.GridView)grdBILBillPosting.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);
                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                if (Convert.ToInt32(mSelectedRow["autocode"]) > 0)
                {
                    bool mCanAccess = Program.GrantDeny_FunctionAccess(
                        AfyaPro_Types.clsSystemFunctions.FunctionKeys.bilpostbills_incomingbilldelete.ToString());

                    if (mCanAccess == false)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString());
                        return;
                    }

                    DialogResult mDialogResult = Program.Display_Question("Are you sure you want to remove the selected item", MessageBoxDefaultButton.Button2);
                    if (mDialogResult != System.Windows.Forms.DialogResult.Yes)
                    {
                        return;
                    }

                    AfyaPro_Types.clsBill mBill = pMdtBilling.Remove_ItemFromCart(
                        mTransDate, Convert.ToInt32(mSelectedRow["autocode"]), Program.gCurrentUser.Code);
                    if (mBill.Exe_Result == 0)
                    {
                        Program.Display_Error(mBill.Exe_Message);
                        return;
                    }
                    if (mBill.Exe_Result == -1)
                    {
                        Program.Display_Server_Error(mBill.Exe_Message);
                        return;
                    }
                }

                mGridView.DeleteSelectedRows();
                pDtCartItems.AcceptChanges();
                this.Clear_Item();
                this.Clear_Pharmacy();
                this.Get_Total();

                txtPharmCode.Focus();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdPharmRemove_Click
        private void cmdPharmRemove_Click(object sender, EventArgs e)
        {
            this.Remove_PharmacyItemFromCart();
        }
        #endregion

        #endregion

        #region cboPriceCategory_EditValueChanged
        private void cboPriceCategory_EditValueChanged(object sender, EventArgs e)
        {
            this.Display_BillingItem();
        }
        #endregion

        #region frmBILBillPosting_Shown
        private void frmBILBillPosting_Shown(object sender, EventArgs e)
        {
            if (gOpdRegistration != null || gIpdAdmission != null || gIpdDischarge != null || gPatientDiagnosisForm != null)
            {
                txtItemCode.Focus();
            }
            else
            {
                txtPatientId.Focus();
            }
        }
        #endregion

        #region frmBILBillPosting_KeyDown
        void frmBILBillPosting_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Program.KeyCode_Save:
                    {
                        this.Save_Bill();
                    }
                    break;
                case Program.KeyCode_SeachPatient:
                    {
                        pSearchingPatient = true;

                        frmSearchPatient mSearchPatient = new frmSearchPatient(txtPatientId);
                        mSearchPatient.ShowDialog();
                        if (mSearchPatient.SearchingDone == true)
                        {
                            txtItemCode.Focus();
                        }

                        pSearchingPatient = false;
                    }
                    break;
                case Program.KeyCode_SearchBillingItem:
                    {
                        if (txtItemCode.CanFocus == true)
                        {
                            this.Search_BillingItem();
                        }
                        else if (txtPharmCode.CanFocus == true)
                        {
                            this.Search_PharmacyItem();
                        }
                    }
                    break;
                case Program.KeyCode_ItemAdd:
                    {
                        if (txtItemCode.CanFocus == true)
                        {
                            this.Add_ToCart();
                        }
                        else if (txtPharmCode.CanFocus == true)
                        {
                            this.Add_PharmToCart();
                        }
                    }
                    break;
                case Program.KeyCode_ItemRemove:
                    {
                        if (txtItemCode.CanFocus == true)
                        {
                            this.Remove_FromCart();
                        }
                        else if (txtPharmCode.CanFocus == true)
                        {
                            this.Remove_PharmacyItemFromCart();
                        }
                    }
                    break;
            }
        }
        #endregion
    }
}