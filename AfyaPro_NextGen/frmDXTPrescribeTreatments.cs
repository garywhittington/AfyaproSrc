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
    public partial class frmDXTPrescribeTreatments : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        internal AfyaPro_Types.clsBooking gCurrentBooking = new AfyaPro_Types.clsBooking();

        private AfyaPro_MT.clsMedicalStaffs pMdtMedicalStaffs;
        private AfyaPro_MT.clsPatientDiagnoses pMdtPatientDiagnoses;
        private AfyaPro_MT.clsRegistrations pMdtRegistrations;
        private AfyaPro_MT.clsPriceCategories pMdtPriceCategories;
        private AfyaPro_MT.clsSomProducts pMdtSomProducts;
        private AfyaPro_MT.clsSomStores pMdtSomStores;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private bool pQuantityChanging = false;
        private bool pPriceChanging = false;
        private bool pAmountChanging = false;

        private string pCurrPatientId = "";
        private string pPrevPatientId = "";
        private string pCurrPharmCode = "";
        private string pPrevPharmCode = "";
        private AfyaPro_Types.clsPatient pCurrentPatient;

        private bool pSearchingPharmacyItem = false;

        private DataTable pDtPriceCategories = new DataTable("pricecategories");
        private DataTable pDtStores = new DataTable("stores");
        private DataTable pDtMedicalStaffs = new DataTable("medicalstaffs");
        private DataTable pDtVisits = new DataTable("patientcasevisits");

        private string pDiagnosisCode = "";
        internal string DiagnosisCode
        {
            set { pDiagnosisCode = value; }
            get { return pDiagnosisCode; }
        }

        private string pTreatments = "";
        internal string Treatments
        {
            set { pTreatments = value; }
            get { return pTreatments; }
        }
        private string pDoctorCode = "";
        internal string DoctorCode
        {
            set { pDoctorCode = value; }
            get { return pDoctorCode; }
        }
        #endregion

        #region frmDXTPrescribeTreatments
        public frmDXTPrescribeTreatments(AfyaPro_Types.clsPatient mCurrentPatient)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmDXTPrescribeTreatments";
            this.KeyDown += new KeyEventHandler(frmDXTPrescribeTreatments_KeyDown);

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                this.pCurrentPatient = mCurrentPatient;

                pMdtSomProducts = (AfyaPro_MT.clsSomProducts)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomProducts),
                    Program.gMiddleTier + "clsSomProducts");

                pMdtSomStores = (AfyaPro_MT.clsSomStores)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSomStores),
                    Program.gMiddleTier + "clsSomStores");

                pMdtPriceCategories = (AfyaPro_MT.clsPriceCategories)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPriceCategories),
                    Program.gMiddleTier + "clsPriceCategories");

                pMdtRegistrations = (AfyaPro_MT.clsRegistrations)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRegistrations),
                    Program.gMiddleTier + "clsRegistrations");

                pMdtPatientDiagnoses = (AfyaPro_MT.clsPatientDiagnoses)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPatientDiagnoses),
                    Program.gMiddleTier + "clsPatientDiagnoses");

                pMdtMedicalStaffs = (AfyaPro_MT.clsMedicalStaffs)Activator.GetObject(
                    typeof(AfyaPro_MT.clsMedicalStaffs),
                    Program.gMiddleTier + "clsMedicalStaffs");

                pDtPriceCategories.Columns.Add("pricename", typeof(System.String));
                pDtPriceCategories.Columns.Add("pricedescription", typeof(System.String));
                cboPriceCategory.Properties.DataSource = pDtPriceCategories;
                cboPriceCategory.Properties.DisplayMember = "pricedescription";
                cboPriceCategory.Properties.ValueMember = "pricename";

                #region stores
                pDtStores.Columns.Add("code", typeof(System.String));
                pDtStores.Columns.Add("description", typeof(System.String));
                cboStore.Properties.DataSource = pDtStores;
                cboStore.Properties.DisplayMember = "description";
                cboStore.Properties.ValueMember = "code";
                #endregion

                pDtMedicalStaffs.Columns.Add("code", typeof(System.String));
                pDtMedicalStaffs.Columns.Add("description", typeof(System.String));
                pDtMedicalStaffs.Columns.Add("treatmentpointcode", typeof(System.String));
                cboDoctor.Properties.DataSource = pDtMedicalStaffs;
                cboDoctor.Properties.DisplayMember = "description";
                cboDoctor.Properties.ValueMember = "code";

                //visits
                pDtVisits = pMdtPatientDiagnoses.View_Prescription("&&&&&&&&&&********&&&&&&&&&&&&&", DateTime.Now, grdDXTTreatments.Name);
                grdDXTTreatments.DataSource = pDtVisits;

                this.Fill_LookupData();

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.dxtpatientdiagnoses_customizelayout.ToString());

                if (Program.IsNullDate(txtDate.EditValue) == true)
                {
                    txtDate.EditValue = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue).Date;
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
            List<Object> mObjectsList = new List<object>();

            mObjectsList.Add(txbPatientId);
            mObjectsList.Add(txbName);
            mObjectsList.Add(txbBirthDate);
            mObjectsList.Add(txbYears);
            mObjectsList.Add(txbMonths);
            mObjectsList.Add(txbGender);
            mObjectsList.Add(txbGender);
            mObjectsList.Add(txbPriceCategory);
            mObjectsList.Add(grpPharmacy);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
            this.Data_Clear();
        }

        #endregion

        #region frmDXTPrescribeTreatments_Load
        private void frmDXTPrescribeTreatments_Load(object sender, EventArgs e)
        {
            this.Top = 0;

            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);
            Program.Restore_GridLayout(grdDXTTreatments, grdDXTTreatments.Name);
            
            viewDXTTreatments.OptionsBehavior.Editable = false;
            viewDXTTreatments.OptionsView.ShowGroupPanel = false;
            viewDXTTreatments.OptionsView.ShowFooter = false;
            viewDXTTreatments.OptionsCustomization.AllowGroup = true;
            viewDXTTreatments.OptionsView.ShowIndicator = false;
            grdDXTTreatments.MainView = viewDXTTreatments;
            viewDXTTreatments.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(viewDXTTreatments_FocusedRowChanged);
            viewDXTTreatments.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(viewDXTTreatments_RowClick);
            viewDXTTreatments.GotFocus += new EventHandler(viewDXTTreatments_GotFocus);

            this.Load_Controls();

            cboPriceCategory.Enabled = Program.GrantDeny_FunctionAccess(
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.bilpostbills_changepricecategory.ToString());

            txbBirthDateFormat.Text = "(" + Program.gCulture.DateTimeFormat.ShortDatePattern + ")";

            Program.Center_Screen(this);

            this.Data_Display(pCurrentPatient);

            cboStore.ItemIndex = Program.Get_LookupItemIndex(cboStore, "code", Program.gCurrentUser.StoreCode);

            txtPatientId.Focus();

            this.Append_ShortcutKeys();
        }
        #endregion

        #region frmDXTPrescribeTreatments_Shown
        private void frmDXTPrescribeTreatments_Shown(object sender, EventArgs e)
        {
            cboDoctor.ItemIndex = Program.Get_LookupItemIndex(cboDoctor, "code", pDoctorCode);
            this.Fill_Visits();
            txtPatientId.Focus();
        }
        #endregion

        #region frmDXTPrescribeTreatments_FormClosing
        private void frmDXTPrescribeTreatments_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //layout
                if (layoutControl1.IsModified == true)
                {
                    Program.Save_FormLayout(this, layoutControl1, this.Name);
                }

                //grid
                Program.Save_GridLayout(grdDXTTreatments, grdDXTTreatments.Name);

                #region update treatments

                string mProductCodes = "";
                foreach (DataRow mDataRow in pDtVisits.Rows)
                {
                    if (mProductCodes.Trim() == "")
                    {
                        mProductCodes = "'" + mDataRow["itemcode"].ToString().Trim() + "'";
                    }
                    else
                    {
                        mProductCodes = mProductCodes + ",'" + mDataRow["itemcode"].ToString().Trim() + "'";
                    }
                }

                string mFilter = "";
                if (mProductCodes.Trim() == "")
                {
                    mFilter = "1=2";
                }
                else
                {
                    mFilter = "code in (" + mProductCodes + ")";
                }

                DataTable mDtProducts = pMdtSomProducts.View(mFilter, "", "", "");
                DataView mDvProducts = new DataView();
                mDvProducts.Table = mDtProducts;
                mDvProducts.Sort = "code";

                pTreatments = "";
                foreach (DataRow mDataRow in pDtVisits.Rows)
                {
                    string mDisplayName = "";
                    int mRowIndex = mDvProducts.Find(mDataRow["itemcode"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mDisplayName = mDvProducts[mRowIndex]["displayname"].ToString().Trim() != "" ?
                            mDvProducts[mRowIndex]["displayname"].ToString().Trim() :
                            mDvProducts[mRowIndex]["description"].ToString().Trim();
                    }

                    if (pTreatments.Trim() == "")
                    {
                        pTreatments = mDisplayName + ": " + mDataRow["dosage"].ToString().Trim();
                    }
                    else
                    {
                        pTreatments = pTreatments + Environment.NewLine
                            + mDisplayName + ": " + mDataRow["dosage"].ToString().Trim();
                    }
                }

                #endregion
            }
            catch { }
        }
        #endregion

        #region Fill_Visits
        private void Fill_Visits()
        {
            string mFunctionName = "Fill_Visits";

            try
            {
                pDtVisits.Rows.Clear();

                if (Program.IsNullDate(txtDate.EditValue) == true)
                {
                    Program.Display_Error("Invalid date entry");
                    txtDate.Focus();
                    return;
                }

                DataTable mDtHistory = pMdtPatientDiagnoses.View_Prescription(txtPatientId.Text, Convert.ToDateTime(txtDate.EditValue), grdDXTTreatments.Name);

                foreach (DataRow mDataRow in mDtHistory.Rows)
                {
                    DataRow mNewRow = pDtVisits.NewRow();

                    foreach (DataColumn mDataColumn in pDtVisits.Columns)
                    {
                        mNewRow[mDataColumn.ColumnName] = mDataRow[mDataColumn.ColumnName];
                    }

                    pDtVisits.Rows.Add(mNewRow);
                    pDtVisits.AcceptChanges();
                }

                viewDXTTreatments.BestFitColumns();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Append_ShortcutKeys
        private void Append_ShortcutKeys()
        {
            //cmdPharmSearchItem.Text = cmdPharmSearchItem.Text + " (" + Program.KeyCode_SearchBillingItem.ToString() + ")";
            //cmdPharmAdd.Text = cmdPharmAdd.Text + " (" + Program.KeyCode_ItemAdd.ToString() + ")";
            //cmdPharmDelete.Text = cmdPharmDelete.Text + " (" + Program.KeyCode_ItemRemove.ToString() + ")";
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

                #region medical doctors

                pDtMedicalStaffs.Rows.Clear();

                DataTable mDtMedicalStaffs = pMdtMedicalStaffs.View(
                    "category=" + Convert.ToInt16(AfyaPro_Types.clsEnums.StaffCategories.MedicalDoctors)
                    + " or category=" + Convert.ToInt16(AfyaPro_Types.clsEnums.StaffCategories.Nurses),
                    "description", Program.gLanguageName, "grdGENMedicalStaffs");

                mNewRow = pDtMedicalStaffs.NewRow();
                mNewRow["code"] = "";
                mNewRow["description"] = "Unknown Clinician";
                mNewRow["treatmentpointcode"] = "";
                pDtMedicalStaffs.Rows.Add(mNewRow);
                pDtMedicalStaffs.AcceptChanges();
                foreach (DataRow mDataRow in mDtMedicalStaffs.Rows)
                {
                    mNewRow = pDtMedicalStaffs.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    mNewRow["treatmentpointcode"] = mDataRow["treatmentpointcode"].ToString();
                    pDtMedicalStaffs.Rows.Add(mNewRow);
                    pDtMedicalStaffs.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtMedicalStaffs.Columns)
                {
                    mDataColumn.Caption = mDtMedicalStaffs.Columns[mDataColumn.ColumnName].Caption;
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

        #region Data_Clear
        private void Data_Clear()
        {
            txtName.Text = "";
            txtGender.Text = "";
            txtBirthDate.Text = "";
            txtYears.Text = "";
            txtMonths.Text = "";

            if (Program.gCurrentUser.DefaultPriceCategoryCode.Trim() != "")
            {
                cboPriceCategory.ItemIndex = Program.Get_LookupItemIndex(cboPriceCategory, "pricename", Program.gCurrentUser.DefaultPriceCategoryCode);
            }

            pPrevPatientId = txtPatientId.Text;
            pCurrPatientId = pPrevPatientId;
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
            txtDosage.Text = "";

            pCurrPharmCode = txtPharmCode.Text;
            pPrevPharmCode = pCurrPharmCode;
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
                        pCurrentPatient = mPatient;

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

                        gCurrentBooking = pMdtRegistrations.Get_Booking(txtPatientId.Text);

                        pCurrPatientId = mPatient.code;
                        pPrevPatientId = pCurrPatientId;

                        this.Clear_Pharmacy();

                        System.Media.SystemSounds.Beep.Play();

                        txtPharmCode.Focus();
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

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Display_RowDetails
        private void Display_RowDetails(DataRow mSelectedRow)
        {
            cboStore.ItemIndex = Program.Get_LookupItemIndex(cboStore, "code", mSelectedRow["storecode"].ToString().Trim());
            cboDoctor.ItemIndex = Program.Get_LookupItemIndex(cboDoctor, "code", mSelectedRow["doctorcode"].ToString().Trim());
            txtPharmCode.Text = mSelectedRow["itemcode"].ToString().Trim();
            txtPharmDescription.Text = mSelectedRow["itemdescription"].ToString().Trim();
            txtPharmQuantity.Text = Convert.ToDouble(mSelectedRow["qty"]).ToString("c");
            txtPharmPrice.Text = Convert.ToDouble(mSelectedRow["qty"])==0? Convert.ToDouble(mSelectedRow["amount"]).ToString("c"): (Convert.ToDouble(mSelectedRow["amount"]) / Convert.ToDouble(mSelectedRow["qty"])).ToString("c");
            txtPharmAmount.Text = Convert.ToDouble(mSelectedRow["amount"]).ToString("c");
            txtDosage.Text = mSelectedRow["dosage"].ToString().Trim();
        }
        #endregion

        #region viewDXTTreatments_FocusedRowChanged
        void viewDXTTreatments_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle <= -1)
            {
                return;
            }

            DevExpress.XtraGrid.Views.Grid.GridView viewDXTTreatments =
            (DevExpress.XtraGrid.Views.Grid.GridView)grdDXTTreatments.MainView;

            this.Display_RowDetails(viewDXTTreatments.GetDataRow(e.FocusedRowHandle));
        }
        #endregion

        #region viewDXTTreatments_RowClick
        private void viewDXTTreatments_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle <= -1)
            {
                return;
            }

            DevExpress.XtraGrid.Views.Grid.GridView viewDXTTreatments =
            (DevExpress.XtraGrid.Views.Grid.GridView)grdDXTTreatments.MainView;

            this.Display_RowDetails(viewDXTTreatments.GetDataRow(e.RowHandle));
        }
        #endregion

        #region viewDXTTreatments_GotFocus
        private void viewDXTTreatments_GotFocus(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView viewDXTTreatments =
            (DevExpress.XtraGrid.Views.Grid.GridView)grdDXTTreatments.MainView;

            if (viewDXTTreatments.FocusedRowHandle <= -1)
            {
                return;
            }

            this.Display_RowDetails(viewDXTTreatments.GetDataRow(viewDXTTreatments.FocusedRowHandle));
        }
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
                this.Add();
            }
        }
        #endregion

        #region txtPharmPrice_KeyDown
        private void txtPharmPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Add();
            }
        }
        #endregion

        #region txtPharmAmount_KeyDown
        private void txtPharmAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Add();
            }
        }
        #endregion

        #region Add
        private void Add()
        {
            DataView mDvAllItems = new DataView();
            string mFunctionName = "Add";

            #region validation

            #region transaction date

            if (Program.IsDate(txtDate.EditValue) == false)
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

            #region clinicalofficer

            if (cboDoctor.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DoctorIsInvalid.ToString());
                cboDoctor.Focus();
                return;
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
                string mDoctorCode = cboDoctor.GetColumnValue("code").ToString().Trim();

                DataTable mDtItems = pMdtSomProducts.View("code='" + txtPharmCode.Text.Trim() + "'", "", "", "");
                if (mDtItems.Rows.Count == 0)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BLS_BillingItemCodeIsInvalid.ToString());
                    txtPharmCode.Focus();
                    return;
                }

                pResult = pMdtPatientDiagnoses.Treatment_Add(
                                                    Convert.ToDateTime(txtDate.EditValue),
                                                    txtPatientId.Text,
                                                    pDiagnosisCode,
                                                    mDoctorCode,
                                                    mStoreCode,
                                                    txtPharmCode.Text,
                                                    Convert.ToDecimal(txtPharmQuantity.Text),
                                                    Convert.ToDecimal(txtPharmAmount.Text),
                                                    txtDosage.Text,
                                                    Program.gCurrentUser.Code);
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

                this.Fill_Visits();
                this.Clear_Pharmacy();

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
            this.Add();
        }
        #endregion

        #region Remove
        private void Remove()
        {
            string mFunctionName = "Remove";

            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView viewDXTTreatments =
                (DevExpress.XtraGrid.Views.Grid.GridView)grdDXTTreatments.MainView;

                if (viewDXTTreatments.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow mSelectedRow = viewDXTTreatments.GetDataRow(viewDXTTreatments.FocusedRowHandle);

                DialogResult mResult = Program.Display_Question("Delete selected record", MessageBoxDefaultButton.Button2);
                if (mResult != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }

                pResult = pMdtPatientDiagnoses.Treatment_Delete(
                                                    Convert.ToInt32(mSelectedRow["autocode"]),
                                                    Program.gCurrentUser.Code);
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

                this.Fill_Visits();
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
            this.Remove();
        }
        #endregion

        #endregion

        #region frmDXTPrescribeTreatments_KeyDown
        void frmDXTPrescribeTreatments_KeyDown(object sender, KeyEventArgs e)
        {

        }
        #endregion
    }
}