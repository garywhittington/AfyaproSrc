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
    public partial class frmOPDRegistrations : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsRegistrations pMdtRegistrations;
        private AfyaPro_MT.clsTreatmentPoints pMdtTreatmentPoints;
        private AfyaPro_MT.clsCountries pMdtCountries;
        private AfyaPro_MT.clsRegions pMdtRegions;
        private AfyaPro_MT.clsDistricts pMdtDistricts;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_MT.clsPatientExtraFields pMdtPatientExtraFields;
        private AfyaPro_MT.clsClientGroups pMdtClientGroups;
        private AfyaPro_MT.clsClientSubGroups pMdtClientSubGroups;
        private AfyaPro_MT.clsClientGroupMembers pMdtClientGroupMembers;
        private AfyaPro_MT.clsPriceCategories pMdtPriceCategories;
        private AfyaPro_MT.clsReporter pMdtReporter;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();
        internal bool gBillingOk = false;
        internal bool gBookingOk = false;
        internal string gTreatmentPointCode = "";
        internal string gTreatmentPointDescription = "";
        internal string gBillingGroupCode = "";
        internal string gBillingGroupDescription = "";
        internal string gBillingSubGroupCode = "";
        internal string gBillingSubGroupDescription = "";
        internal string gBillingGroupMembershipNo = "";
        internal string gPriceCategory = "";
        internal double gWeight = 0;
        internal double gTemperature = 0;
        internal object gLastAttendanceDate = null;
        bool mAllowCharging = false;
        internal AfyaPro_Types.clsPatient gCurrentPatient;
        internal AfyaPro_Types.clsBooking gCurrentBooking = new AfyaPro_Types.clsBooking();

        private DataTable pDtAllGroups = new DataTable("groups");
        private DataTable pDtGroups = new DataTable("groups");
        private DataTable pDtSubGroups = new DataTable("subgroups");

        private Type pType;
        private string pClassName = "";
        private AfyaPro_Types.clsPatient pSelectedPatient = null;
        private string pCurrPatientId = "";
        private string pPrevPatientId = "";
        private bool pFirstTimeLoad = true;
        private List<Object> pObjectsList = new List<Object>();

        private bool pGroupHasId = false;
        private bool pGroupHasSubGroups = false;

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private int pStrictActivation = 0;

        private bool pSearchingPatient = false;
        private string pPrevMemberId = "";
        private string pCurrMemberId = "";

        private DataTable pDtExtraFields = new DataTable("extrafields");
        private DataTable pDtPriceCategories = new DataTable("pricecategories");

        #endregion

        #region frmOPDRegistrations
        public frmOPDRegistrations()
        {
            InitializeComponent();
            
            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmOPDRegistrations";
            this.Shown += new EventHandler(frmOPDRegistrations_Shown);
            this.KeyDown += new KeyEventHandler(frmOPDRegistrations_KeyDown);

            try
            {
                cboBillType.EditValueChanged += new EventHandler(cboBillType_EditValueChanged);
                this.Icon = Program.gMdiForm.Icon;

                pMdtTreatmentPoints = (AfyaPro_MT.clsTreatmentPoints)Activator.GetObject(
                    typeof(AfyaPro_MT.clsTreatmentPoints),
                    Program.gMiddleTier + "clsTreatmentPoints");

                pMdtAutoCodes = (AfyaPro_MT.clsAutoCodes)Activator.GetObject(
                    typeof(AfyaPro_MT.clsAutoCodes),
                    Program.gMiddleTier + "clsAutoCodes");

                pMdtRegistrations = (AfyaPro_MT.clsRegistrations)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRegistrations),
                    Program.gMiddleTier + "clsRegistrations");

                pMdtCountries = (AfyaPro_MT.clsCountries)Activator.GetObject(
                    typeof(AfyaPro_MT.clsCountries),
                    Program.gMiddleTier + "clsCountries");

                pMdtRegions = (AfyaPro_MT.clsRegions)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRegions),
                    Program.gMiddleTier + "clsRegions");

                pMdtDistricts = (AfyaPro_MT.clsDistricts)Activator.GetObject(
                    typeof(AfyaPro_MT.clsDistricts),
                    Program.gMiddleTier + "clsDistricts");

                pMdtPatientExtraFields = (AfyaPro_MT.clsPatientExtraFields)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPatientExtraFields),
                    Program.gMiddleTier + "clsPatientExtraFields");

                pMdtClientGroups = (AfyaPro_MT.clsClientGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsClientGroups),
                    Program.gMiddleTier + "clsClientGroups");

                pMdtClientSubGroups = (AfyaPro_MT.clsClientSubGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsClientSubGroups),
                    Program.gMiddleTier + "clsClientSubGroups");

                pMdtClientGroupMembers = (AfyaPro_MT.clsClientGroupMembers)Activator.GetObject(
                    typeof(AfyaPro_MT.clsClientGroupMembers),
                    Program.gMiddleTier + "clsClientGroupMembers");

                pMdtPriceCategories = (AfyaPro_MT.clsPriceCategories)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPriceCategories),
                    Program.gMiddleTier + "clsPriceCategories");

                pMdtReporter = (AfyaPro_MT.clsReporter)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReporter),
                    Program.gMiddleTier + "clsReporter");

                cmdSave.ImageIndex = 0;
                cmdNew.ImageIndex = 2;

                this.Load_ExtraControls();

                pDtGroups.Columns.Add("code", typeof(System.String));
                pDtGroups.Columns.Add("description", typeof(System.String));
                cboGroup.Properties.DataSource = pDtGroups;
                cboGroup.Properties.DisplayMember = "description";
                cboGroup.Properties.ValueMember = "code";
                cboGroup.Properties.BestFit();

                pDtSubGroups.Columns.Add("code", typeof(System.String));
                pDtSubGroups.Columns.Add("description", typeof(System.String));
                cboSubGroup.Properties.DataSource = pDtSubGroups;
                cboSubGroup.Properties.DisplayMember = "description";
                cboSubGroup.Properties.ValueMember = "code";
                cboSubGroup.Properties.BestFit();

                pDtPriceCategories.Columns.Add("pricename", typeof(System.String));
                pDtPriceCategories.Columns.Add("pricedescription", typeof(System.String));
                cboPriceCategory.Properties.DataSource = pDtPriceCategories;
                cboPriceCategory.Properties.DisplayMember = "pricedescription";
                cboPriceCategory.Properties.ValueMember = "pricename";

                this.Fill_LookupData();
                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.opdregistrations_customizelayout.ToString());

                #region character casing

                if (Program.gDtCompanySetup.Rows.Count > 0)
                {
                    switch (Convert.ToInt16(Program.gDtCompanySetup.Rows[0]["charactercasingoptionpatientnames"]))
                    {
                        case 0:
                            {
                                txtSurname.Properties.CharacterCasing = CharacterCasing.Normal;
                                txtFirstName.Properties.CharacterCasing = CharacterCasing.Normal;
                                txtOtherNames.Properties.CharacterCasing = CharacterCasing.Normal;
                            }
                            break;
                        case 1:
                            {
                                txtSurname.Properties.CharacterCasing = CharacterCasing.Upper;
                                txtFirstName.Properties.CharacterCasing = CharacterCasing.Upper;
                                txtOtherNames.Properties.CharacterCasing = CharacterCasing.Upper;
                            }
                            break;
                        case 2:
                            {
                                txtSurname.Properties.CharacterCasing = CharacterCasing.Lower;
                                txtFirstName.Properties.CharacterCasing = CharacterCasing.Lower;
                                txtOtherNames.Properties.CharacterCasing = CharacterCasing.Lower;
                            }
                            break;
                    }
                }
                #endregion

                txtSurname.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtSurname.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtSurname.MaskBox.AutoCompleteCustomSource = Program.gCommonSurnames;
                
                txtFirstName.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtFirstName.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtFirstName.MaskBox.AutoCompleteCustomSource = Program.gCommonFirstNames;

                txtOtherNames.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtOtherNames.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtOtherNames.MaskBox.AutoCompleteCustomSource = Program.gCommonOtherNames;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }

        #endregion

        #region Load_ExtraControls
        private void Load_ExtraControls()
        {
            string mFunctionName = "Load_ExtraControls";

            LayoutControlItem mLayoutControlItem;

            try
            {
                pObjectsList.Add(txbPatientId);
                pObjectsList.Add(txbSurname);
                pObjectsList.Add(txbFirstName);
                pObjectsList.Add(txbOtherNames);
                pObjectsList.Add(txbBirthDate);
                pObjectsList.Add(txbYears);
                pObjectsList.Add(txbMonths);
                pObjectsList.Add(txbGender);
                pObjectsList.Add(txbChronic);
                pObjectsList.Add(txbSevere);
                pObjectsList.Add(txbMajor);
                pObjectsList.Add(txbBillType);
                pObjectsList.Add(cmdSearch);
                pObjectsList.Add(cmdSave);
                pObjectsList.Add(cmdNew);
                pObjectsList.Add(cmdClose);
                pObjectsList.Add(cmdBook);

                pDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");
                DataTable mDtExtraFilterFields = pMdtPatientExtraFields.View("filteronvaluefrom<>''", "", "", "");

                DataView mDvExtraFilterFields = new DataView();
                mDvExtraFilterFields.Table = mDtExtraFilterFields;
                mDvExtraFilterFields.Sort = "filteronvaluefrom";

                foreach (DataRow mDataRow in pDtExtraFields.Rows)
                {
                    string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                    string mFieldCaption = mDataRow["fieldcaption"].ToString();
                    string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                    string mDataType = mDataRow["datatype"].ToString().Trim();
                    string mFilterOnValueFrom = mDataRow["filteronvaluefrom"].ToString().Trim();
                    int mAllowInput = 0;
                    int mRestrictToDropDownList = 0;
                    int mRememberEntries = 0;

                    try
                    {
                        mAllowInput = Convert.ToInt16(mDataRow["allowinput"]);
                    }
                    catch { }

                    try
                    {
                        mRestrictToDropDownList = Convert.ToInt16(mDataRow["restricttodropdownlist"]);
                    }
                    catch { }

                    try
                    {
                        mRememberEntries = Convert.ToInt16(mDataRow["rememberentries"]);
                    }
                    catch { }

                    if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                    {
                        mLayoutControlItem = new LayoutControlItem();
                        mLayoutControlItem.Name = "txb" + mFieldName;
                        mLayoutControlItem.CustomizationFormText = mFieldCaption;
                        mLayoutControlItem.Text = mFieldCaption;
                        ComboBoxEdit mComboBoxEdit = new ComboBoxEdit();
                        mComboBoxEdit.Name = "cbo" + mFieldName;
                        mLayoutControlItem.Control = mComboBoxEdit;
                        layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                        #region character casing

                        switch (Convert.ToInt16(mDataRow["charactercasingoption"]))
                        {
                            case 0:
                                {
                                    mComboBoxEdit.Properties.CharacterCasing = CharacterCasing.Normal;
                                }
                                break;
                            case 1:
                                {
                                    mComboBoxEdit.Properties.CharacterCasing = CharacterCasing.Upper;
                                }
                                break;
                            case 2:
                                {
                                    mComboBoxEdit.Properties.CharacterCasing = CharacterCasing.Lower;
                                }
                                break;
                        }

                        #endregion

                        //if this will be used to filter other values, add the event to do the job
                        if (mDvExtraFilterFields.Find(mFieldName) >= 0)
                        {
                            mComboBoxEdit.EditValueChanged += new EventHandler(mComboBoxEdit_EditValueChanged);
                        }

                        //is it editable
                        if (mAllowInput == 0)
                        {
                            mComboBoxEdit.Properties.ReadOnly = true;
                        }

                        //is it disable texteditor
                        if (mRestrictToDropDownList == 1)
                        {
                            mComboBoxEdit.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                        }
                    }
                    else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().ToLower())
                    {
                        mLayoutControlItem = new LayoutControlItem();
                        mLayoutControlItem.Name = "txb" + mFieldName;
                        mLayoutControlItem.CustomizationFormText = mFieldCaption;
                        mLayoutControlItem.Text = mFieldCaption;
                        CheckEdit mCheckEdit = new CheckEdit();
                        mCheckEdit.Name = "chk" + mFieldName;
                        mCheckEdit.Checked = false;
                        mCheckEdit.Text = mFieldCaption;
                        mLayoutControlItem.Control = mCheckEdit;
                        layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                        //is it editable
                        if (mAllowInput == 0)
                        {
                            mCheckEdit.Properties.ReadOnly = true;
                        }
                    }
                    else
                    {
                        if (mDataType.Trim().ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
                        {
                            mLayoutControlItem = new LayoutControlItem();
                            mLayoutControlItem.Name = "txb" + mFieldName;
                            mLayoutControlItem.CustomizationFormText = mFieldCaption;
                            mLayoutControlItem.Text = mFieldCaption;
                            DateEdit mDateEdit = new DateEdit();
                            mDateEdit.Name = "txt" + mFieldName;
                            mDateEdit.Properties.NullText = "";
                            mLayoutControlItem.Control = mDateEdit;
                            layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                            //is it editable
                            if (mAllowInput == 0)
                            {
                                mDateEdit.Properties.ReadOnly = true;
                            }
                        }
                        else
                        {
                            mLayoutControlItem = new LayoutControlItem();
                            mLayoutControlItem.Name = "txb" + mFieldName;
                            mLayoutControlItem.CustomizationFormText = mFieldCaption;
                            mLayoutControlItem.Text = mFieldCaption;
                            TextEdit mTextEdit = new TextEdit();
                            mTextEdit.Name = "txt" + mFieldName;
                            mLayoutControlItem.Control = mTextEdit;
                            layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                            #region character casing

                            switch (Convert.ToInt16(mDataRow["charactercasingoption"]))
                            {
                                case 0:
                                    {
                                        mTextEdit.Properties.CharacterCasing = CharacterCasing.Normal;
                                    }
                                    break;
                                case 1:
                                    {
                                        mTextEdit.Properties.CharacterCasing = CharacterCasing.Upper;
                                    }
                                    break;
                                case 2:
                                    {
                                        mTextEdit.Properties.CharacterCasing = CharacterCasing.Lower;
                                    }
                                    break;
                            }

                            #endregion

                            //is it editable
                            if (mAllowInput == 0)
                            {
                                mTextEdit.Properties.ReadOnly = true;
                            }
                        }
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

        #region mComboBoxEdit_EditValueChanged
        void mComboBoxEdit_EditValueChanged(object sender, EventArgs e)
        {
            string mFunctionName = "mComboBoxEdit_EditValueChanged";

            try
            {
                ComboBoxEdit mComboBoxEdit = (ComboBoxEdit)sender;

                string mFilterOnValueFrom = mComboBoxEdit.Name.Substring(3);
                string mFilterValue = mComboBoxEdit.Text.Trim();

                //get all fields filtered from this field
                DataTable mDtExtraFields = pMdtPatientExtraFields.View("filteronvaluefrom='" +mFilterOnValueFrom + "'", "", "", "");

                //fill the filter values to each child controls
                foreach (DataRow mDataRow in mDtExtraFields.Rows)
                {
                    string mFieldName = mDataRow["fieldname"].ToString().ToLower().Trim();
                    ComboBoxEdit mComboToFill = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                    ComboBoxItemCollection mComboItems = mComboToFill.Properties.Items;

                    DataTable mDtExtraFieldLookup = pMdtPatientExtraFields.View_Lookup(
                        "fieldname='" + mFieldName + "' and filtervalue='" + mFilterValue + "'", "", "", "");

                    mComboToFill.Text = "";
                    mComboItems.Clear();
                    foreach (DataRow mLookupDataRow in mDtExtraFieldLookup.Rows)
                    {
                        mComboItems.Add(mLookupDataRow["description"].ToString().Trim());
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

        #region frmOPDRegistrations_Load
        private void frmOPDRegistrations_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmOPDRegistrations_Load";

            try
            {
                Program.Restore_FormLayout(layoutControl1, this.Name);
                Program.Apply_Language(this.Name, pObjectsList);
                Program.Restore_FormSize(this);

                cboGroup.Properties.ReadOnly = !Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.opdregistrations_changecustomergroup.ToString());
                cboSubGroup.Properties.ReadOnly = !Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.opdregistrations_changecustomersubgroup.ToString());

                txbBirthDateFormat.Text = "(" + Program.gCulture.DateTimeFormat.ShortDatePattern + ")";

                this.pFormWidth = this.Width;
                this.pFormHeight = this.Height;

                if (Program.gCurrentUser.DefaultPriceCategoryCode.Trim() != "")
                {
                    cboPriceCategory.ItemIndex = Program.Get_LookupItemIndex(cboPriceCategory, "pricename", Program.gCurrentUser.DefaultPriceCategoryCode);
                }
                cboPriceCategory.Properties.ReadOnly = !Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.bilpostbills_changepricecategory.ToString());

                Program.Center_Screen(this);
                this.Append_ShortcutKeys();
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
            cmdSearch.Text = cmdSearch.Text + " (" + Program.KeyCode_SeachPatient.ToString() + ")";
            cmdSave.Text = cmdSave.Text + " (" + Program.KeyCode_Save.ToString() + ")";
            cmdBook.Text = cmdBook.Text + " (" + Program.KeyCode_Book.ToString() + ")";
        }
        #endregion

        #region frmOPDRegistrations_Shown
        void frmOPDRegistrations_Shown(object sender, EventArgs e)
        {
            this.Mode_New();
        }
        #endregion

        #region frmOPDRegistrations_FormClosing
        private void frmOPDRegistrations_FormClosing(object sender, FormClosingEventArgs e)
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

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            DataRow mNewRow;
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region Groups

                pDtGroups.Rows.Clear();
                pDtAllGroups = pMdtClientGroups.View("inactive=0", "code", Program.gLanguageName, "grdCUSCustomerGroups");

                mNewRow = pDtGroups.NewRow();
                mNewRow["code"] = "";
                mNewRow["description"] = "";
                pDtGroups.Rows.Add(mNewRow);
                pDtGroups.AcceptChanges();

                foreach (DataRow mDataRow in pDtAllGroups.Rows)
                {
                    mNewRow = pDtGroups.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtGroups.Rows.Add(mNewRow);
                    pDtGroups.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtGroups.Columns)
                {
                    mDataColumn.Caption = pDtAllGroups.Columns[mDataColumn.ColumnName].Caption;
                }

                #endregion

                #region fill other lookups

                foreach (DataRow mDataRow in pDtExtraFields.Rows)
                {
                    string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                    string mFieldCaption = mDataRow["fieldcaption"].ToString();
                    string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                    string mDataType = mDataRow["datatype"].ToString().Trim();
                    string mFilterOnValueFrom = mDataRow["filteronvaluefrom"].ToString().Trim();

                    if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                    {
                        ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;

                        //if no filter, fill values
                        if (mFilterOnValueFrom.Trim() == "")
                        {
                            ComboBoxItemCollection mComboItems = mComboBoxEdit.Properties.Items;

                            DataTable mDtExtraFieldLookup = pMdtPatientExtraFields.View_Lookup("fieldname='" + mFieldName + "'", "", "", "");

                            mComboItems.Clear();
                            foreach (DataRow mLookupDataRow in mDtExtraFieldLookup.Rows)
                            {
                                mComboItems.Add(mLookupDataRow["description"].ToString().Trim());
                            }
                        }
                    }
                }

                #endregion

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

                #region Marital Status

                DataTable mDtMaritalStatus = pMdtReporter.View_LookupData("maritalstatus", "code,description", "", "", Program.gLanguageName, "grdGENMaritalStatus");
                cboMaritalStatus.Properties.DataSource = mDtMaritalStatus;
                cboMaritalStatus.Properties.DisplayMember = "description";
                cboMaritalStatus.Properties.ValueMember = "code";
                cboMaritalStatus.Properties.BestFit();

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
            txtSurname.Text = "";
            txtFirstName.Text = "";
            txtOtherNames.Text = "";
            cboBillType.SelectedIndex = 0;
            cboGender.SelectedIndex = 0;
            txtBirthDate.Text = "";
            txtYears.Text = "";
            txtMonths.Text = "";
            txtChronic.Text = "";
            txtSevere.Text = "";
            txtMajor.Text = "";
            cboGroup.EditValue = null;
            cboSubGroup.EditValue = null;
            cboMaritalStatus.EditValue = null;
            txtAllergies.Text = "";

            gTreatmentPointCode = "";
            gTreatmentPointDescription = "";
            gBillingGroupCode = "";
            gBillingSubGroupCode = "";
            gBillingGroupMembershipNo = "";
            gBookingOk = false;
            gLastAttendanceDate = null;
            gWeight = 0;
            gTemperature = 0;

            pStrictActivation = 0;

            if (Program.gCurrentUser.DefaultCustomerGroupCode.Trim() != "")
            {
                cboGroup.ItemIndex = Program.Get_LookupItemIndex(cboGroup, "code", Program.gCurrentUser.DefaultCustomerGroupCode);
            }
            if (Program.gCurrentUser.DefaultCustomerSubGroupCode.Trim() != "")
            {
                cboSubGroup.ItemIndex = Program.Get_LookupItemIndex(cboSubGroup, "code", Program.gCurrentUser.DefaultCustomerSubGroupCode);
            }
            txtMembershipNo.Text = "";
            picPatient.Image = null;
            txbPicture.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;

            #region clear extra fields

            foreach (DataRow mDataRow in pDtExtraFields.Rows)
            {
                string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                string mDataType = mDataRow["datatype"].ToString().Trim();
                string mDefaultValue = mDataRow["defaultvalue"].ToString().Trim();

                if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                {
                    ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                    mComboBoxEdit.Text = mDefaultValue;
                }
                else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().ToLower())
                {
                    CheckEdit mCheckEdit = layoutControl1.GetControlByName("chk" + mFieldName) as CheckEdit;
                    mCheckEdit.Checked = Convert.ToBoolean(mDefaultValue);
                }
                else
                {
                    if (mDataType.Trim().ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
                    {
                        DateEdit mDateEdit = layoutControl1.GetControlByName("txt" + mFieldName) as DateEdit;
                        mDateEdit.EditValue = DBNull.Value;

                        if (Program.IsNullDate(mDefaultValue) == false)
                        {
                            mDateEdit.EditValue = Convert.ToDateTime(mDefaultValue);
                        }
                    }
                    else
                    {
                        TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                        mTextEdit.Text = mDefaultValue;
                    }
                }
            }

            #endregion

            pPrevPatientId = "";
            pCurrPatientId = pPrevPatientId;
        }
        #endregion

        #region Data_Display
        internal void Data_Display(AfyaPro_Types.clsPatient mPatient)
        {
            String mFunctionName = "Data_Display";

            try
            {
                this.Data_Clear();

                if (mPatient != null)
                {
                    if (mPatient.Exist == true)
                    {
                        txtPatientId.Text = mPatient.code;
                        txtSurname.Text = mPatient.surname;
                        txtFirstName.Text = mPatient.firstname;
                        txtOtherNames.Text = mPatient.othernames;
                        if (mPatient.gender.Trim().ToLower() == "f")
                        {
                            cboGender.SelectedIndex = 1;
                        }
                        else
                        {
                            cboGender.SelectedIndex = 2;
                        }
                        txtBirthDate.Text = mPatient.birthdate.ToString("d");
                        int mDays = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue).Subtract(mPatient.birthdate).Days;
                        int mYears = (int)mDays / 365;
                        int mMonths = (int)(mDays % 365) / 30;

                        txtYears.Text = mYears.ToString();
                        txtMonths.Text = mMonths.ToString();

                        txtChronic.Text = mPatient.chronic;
                        txtSevere.Text = mPatient.severe;
                        txtMajor.Text = mPatient.operations;
                        cboMaritalStatus.ItemIndex = Program.Get_LookupItemIndex(cboMaritalStatus, "code", mPatient.maritalstatuscode);
                        txtAllergies.Text = mPatient.allergies;

                        this.Display_ExtraDetails();

                        #region get last attendance details

                        AfyaPro_Types.clsBooking mLastBooking = pMdtRegistrations.Get_Booking(txtPatientId.Text);

                        cboBillType.SelectedIndex = 0;

                        if (mLastBooking != null)
                        {
                            if (mLastBooking.Booking.Trim() != "")
                            {
                                gBillingGroupCode = mLastBooking.BillingGroupCode;
                                gBillingGroupDescription = mLastBooking.BillingGroupDescription;
                                gBillingSubGroupCode = mLastBooking.BillingSubGroupCode;
                                gBillingSubGroupDescription = mLastBooking.BillingSubGroupDescription;
                                gBillingGroupMembershipNo = mLastBooking.BillingGroupMembershipNo;
                                gPriceCategory = mLastBooking.PriceName;

                                if (Program.IsNullDate(mLastBooking.BookDate) == false)
                                {
                                    gLastAttendanceDate = Convert.ToDateTime(mLastBooking.BookDate);
                                }

                                if (gBillingGroupCode.Trim() == "")
                                {
                                    cboBillType.SelectedIndex = 1;
                                    cboPriceCategory.ItemIndex = Program.Get_LookupItemIndex(cboPriceCategory, "pricename", gPriceCategory);
                                }
                                else
                                {
                                    cboBillType.SelectedIndex = 2;
                                    cboGroup.ItemIndex = Program.Get_LookupItemIndex(cboGroup, "code", gBillingGroupCode);
                                    cboSubGroup.ItemIndex = Program.Get_LookupItemIndex(cboSubGroup, "code", gBillingSubGroupCode);
                                    txtMembershipNo.Text = gBillingGroupMembershipNo;
                                }
                            }
                        }

                        #endregion

                        
                        //customising the system to skip billing features
                        cboPriceCategory.Visible = true;

                        grpBilling.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        mAllowCharging = true;
                        
                        //end of customing

                        pCurrPatientId = mPatient.code;
                        pPrevPatientId = pCurrPatientId;

                        // grpBilling.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        txbBook.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        System.Media.SystemSounds.Beep.Play();

                        cboBillType.Focus();
                    }
                    else
                    {
                        pCurrPatientId = txtPatientId.Text;
                        pPrevPatientId = pCurrPatientId;
                    }
                }
                else
                {
                    picPatient.Image = null;
                    txbPicture.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;

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

        #region Display_ExtraDetails
        internal void Display_ExtraDetails()
        {
            string mFunctionName = "Display_ExtraDetails";

            try
            {
                DataTable mDtPatients = pMdtRegistrations.View_Patients("code='" + txtPatientId.Text.Trim() + "'", "");

                if (mDtPatients.Rows.Count > 0)
                {
                    foreach (DataRow mDataRow in pDtExtraFields.Rows)
                    {
                        string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                        string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                        string mDataType = mDataRow["datatype"].ToString().Trim();
                        object mFieldValue = mDtPatients.Rows[0][mFieldName];

                        if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                            mComboBoxEdit.Text = mFieldValue.ToString();
                        }
                        else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().ToLower())
                        {
                            CheckEdit mCheckEdit = layoutControl1.GetControlByName("chk" + mFieldName) as CheckEdit;
                            mCheckEdit.Checked = Convert.ToBoolean(mFieldValue);
                        }
                        else
                        {
                            if (mDataType.Trim().ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
                            {
                                DateEdit mDateEdit = layoutControl1.GetControlByName("txt" + mFieldName) as DateEdit;
                                mDateEdit.EditValue = DBNull.Value;

                                if (Program.IsNullDate(mFieldValue) == false)
                                {
                                    mDateEdit.EditValue = Convert.ToDateTime(mFieldValue);
                                }
                            }
                            else
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                mTextEdit.Text = mFieldValue.ToString();
                            }
                        }
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

        #region Display_GroupMember
        private void Display_GroupMember()
        {
            Byte[] mBytes = null;
            string mBillingGroupCode = "";
            string mBillingGroupMembershipNo = "";
            string mFunctionName = "Display_GroupMember";

            try
            {
                if (cboGroup.ItemIndex != -1)
                {
                    mBillingGroupCode = cboGroup.GetColumnValue("code").ToString().Trim();
                }

                mBillingGroupMembershipNo = txtMembershipNo.Text.Trim();

                DataView mDvGroups = new DataView();
                mDvGroups.Table = pDtAllGroups;
                mDvGroups.Sort = "code";
                int mRowIndex = mDvGroups.Find(mBillingGroupCode);
                if (mRowIndex >= 0)
                {
                    pStrictActivation = Convert.ToInt16(mDvGroups[mRowIndex]["strictactivation"]);
                }

                DataTable mDtMembers = pMdtClientGroupMembers.View("billinggroupmembershipno='" + mBillingGroupMembershipNo
                + "' and billinggroupcode='" + mBillingGroupCode + "' and membershipstatus=1", "", "", "");
                if (mDtMembers.Rows.Count > 0)
                {
                    cboGroup.ItemIndex = Program.Get_LookupItemIndex(cboGroup, "code", mDtMembers.Rows[0]["billinggroupcode"].ToString().Trim());
                    cboSubGroup.ItemIndex = Program.Get_LookupItemIndex(cboSubGroup, "code", mDtMembers.Rows[0]["billingsubgroupcode"].ToString().Trim());
                    txtMembershipNo.Text = mDtMembers.Rows[0]["billinggroupmembershipno"].ToString().Trim();

                    if (cboGroup.ItemIndex != -1)
                    {
                        gBillingGroupCode = cboGroup.GetColumnValue("code").ToString();
                    }
                    if (cboSubGroup.ItemIndex != -1)
                    {
                        gBillingSubGroupCode = cboSubGroup.GetColumnValue("code").ToString();
                    }
                    gBillingGroupMembershipNo = txtMembershipNo.Text;

                    //if (mDtMembers.Rows[0]["picturename"].ToString().Trim() != "")
                    //{
                        //try
                        //{
                        //    mBytes = pMdtClientGroupMembers.Load_Picture(mDtMembers.Rows[0]["picturename"].ToString().Trim());
                        //}
                        //catch { }

                        //if (mBytes != null)
                        //{
                        //    MemoryStream mStream = new MemoryStream(mBytes);
                        //    Image mImage = Image.FromStream(mStream);
                        //    picPatient.Image = mImage;
                        //    txbPicture.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        //}
                        //else
                        //{
                        //    picPatient.Image = null;
                        //    txbPicture.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;
                        //}
                    //}
                    //else
                    //{
                    //    picPatient.Image = null;
                    //    txbPicture.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;
                    //}

                    if (Convert.ToInt16(mDtMembers.Rows[0]["membershipstatus"]) == 0)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_NoServiceMembershipTerminated.ToString());
                        return;
                    }

                    if (pStrictActivation == 1)
                    {
                        if (Convert.ToInt16(mDtMembers.Rows[0]["inactive"]) == 1)
                        {
                            Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_NoServiceMemberIsNotActivated.ToString());
                            return;
                        }
                    }
                }
                else
                {
                    picPatient.Image = null;
                    txbPicture.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;
                    if (gCurrentPatient != null)
                    {
                        if (gCurrentPatient.Exist == true && pStrictActivation == 1)
                        {
                            Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_NoServiceInvalidGroupOrMembershipId.ToString());
                            return;
                        }
                    }
                }

                pCurrMemberId = txtMembershipNo.Text;
                pPrevMemberId = pCurrMemberId;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region txtMembershipNo_KeyDown
        private void txtMembershipNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Display_GroupMember();
            }
        }
        #endregion

        #region txtMembershipNo_Leave
        private void txtMembershipNo_Leave(object sender, EventArgs e)
        {
            pCurrMemberId = txtMembershipNo.Text;

            if (pCurrMemberId.Trim().ToLower() != pPrevMemberId.Trim().ToLower())
            {
                this.Display_GroupMember();

                pCurrMemberId = txtMembershipNo.Text;
                pPrevMemberId = pCurrMemberId;
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
                this.pSelectedPatient = this.Search_Patient();
                this.Data_Display(pSelectedPatient);
            }
        }
        #endregion

        #region txtPatientId_Leave
        private void txtPatientId_Leave(object sender, EventArgs e)
        {
            pCurrPatientId = txtPatientId.Text;

            if (pCurrPatientId.Trim().ToLower() != pPrevPatientId.Trim().ToLower())
            {
                this.pSelectedPatient = this.Search_Patient();
                this.Data_Display(pSelectedPatient);
            }

            if (txtPatientId.Text.Trim() == "")
            {
                Int16 mGenerateCode = pMdtAutoCodes.Auto_Generate_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.patientno));
                if (mGenerateCode == -1)
                {
                    return;
                }
                if (mGenerateCode == 1)
                {
                    txtPatientId.Text = "<<---New--->>";
                }
                else
                {
                    txtPatientId.Focus();
                }
            }
        }
        #endregion

        #region cboGroup_EditValueChanged
        void cboGroup_EditValueChanged(object sender, EventArgs e)
        {
            DataRow mNewRow;
            string mFunctionName = "cboGroup_EditValueChanged";

            try
            {
                pDtSubGroups.Rows.Clear();
                if (cboGroup.ItemIndex == -1)
                {
                    return;
                }

                pGroupHasId = false;
                pGroupHasSubGroups = false;
                pStrictActivation = 0;
                cboSubGroup.EditValue = null;
                txtMembershipNo.Text = "";

                string mGroupCode = cboGroup.GetColumnValue("code").ToString().Trim();

                DataTable mDtGroups = pMdtClientGroups.View("code='" + mGroupCode + "'", "", Program.gLanguageName, "grdCUSCustomerGroups");
                if (mDtGroups.Rows.Count > 0)
                {
                    pGroupHasId = Convert.ToBoolean(mDtGroups.Rows[0]["hasid"]);
                    pGroupHasSubGroups = Convert.ToBoolean(mDtGroups.Rows[0]["hassubgroups"]);
                    pStrictActivation = Convert.ToInt16(mDtGroups.Rows[0]["strictactivation"]);
                    cboPriceCategory.Properties.ReadOnly = false;
                    int mPriceIndex = Program.Get_LookupItemIndex(cboPriceCategory, "pricename", mDtGroups.Rows[0]["pricecategory"].ToString().Trim());
                    cboPriceCategory.ItemIndex = mPriceIndex;
                    cboPriceCategory.Properties.ReadOnly = true;
                    gPriceCategory = mDtGroups.Rows[0]["pricecategory"].ToString().Trim();
                }

                if (pGroupHasSubGroups == true)
                {
                    DataTable mDtSubGroups = pMdtClientSubGroups.View("groupcode='" + mGroupCode + "'", "code", Program.gLanguageName, "grdCUSCustomerSubGroups");

                    mNewRow = pDtSubGroups.NewRow();
                    mNewRow["code"] = "";
                    mNewRow["description"] = "";
                    pDtSubGroups.Rows.Add(mNewRow);
                    pDtSubGroups.AcceptChanges();

                    foreach (DataRow mDataRow in mDtSubGroups.Rows)
                    {
                        mNewRow = pDtSubGroups.NewRow();
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
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.patientno));
                if (mGenerateCode == -1)
                {
                    Program.Display_Server_Error("");
                    return;
                }

                txtPatientId.Text = "";
                this.Data_Clear();

                if (mGenerateCode == 1)
                {
                    txtPatientId.Text = "<<---New--->>";
                    txtSurname.Focus();
                }
                else
                {
                    txtPatientId.Focus();
                }

                grpBilling.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;
                txbBook.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;
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
            string mGender = "F";
            DateTime mBirthDate = new DateTime();
            DateTime mTransDate = new DateTime();
            AfyaPro_Types.clsPatient mPatient = new AfyaPro_Types.clsPatient();

            String mFunctionName = "Data_New";

            #region validation

            if (Program.IsDate(Program.gMdiForm.txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                return;
            }

            if (txtPatientId.Text.Trim() == "" && txtPatientId.Text.Trim().ToLower() != "<<---new--->>")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoIsInvalid.ToString());
                txtPatientId.Focus();
                return;
            }

            if (txtSurname.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_SurnameIsInvalid.ToString());
                txtSurname.Focus();
                return;
            }

            if (txtFirstName.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_FirstNameIsInvalid.ToString());
                txtFirstName.Focus();
                return;
            }

            #region age stuff

            if (txtBirthDate.Text.Trim() != "")
            {
                if (Program.IsNumeric(txtBirthDate.Text) == true)
                {
                    if (txtBirthDate.Text.Length < 4)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_BirthDateIsInvalid.ToString());
                        txtBirthDate.Focus();
                        return;
                    }
                    else if (txtBirthDate.Text.Length == 4)
                    {
                        if (Convert.ToInt32(txtBirthDate.Text) < 1900)
                        {
                            Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_BirthDateIsInvalid.ToString());
                            txtBirthDate.Focus();
                            return;
                        }
                        mBirthDate = Convert.ToDateTime("01/01/" + txtBirthDate.Text);
                    }
                    else
                    {
                        try
                        {
                            mBirthDate = Convert.ToDateTime(txtBirthDate.Text);
                        }
                        catch
                        {
                            Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_BirthDateIsInvalid.ToString());
                            txtBirthDate.Focus();
                            return;
                        }
                    }
                }
                else
                {
                    if (Program.IsDate(txtBirthDate.Text) == false)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_BirthDateIsInvalid.ToString());
                        txtBirthDate.Focus();
                        return;
                    }
                    mBirthDate = Convert.ToDateTime(txtBirthDate.Text);
                    if (mBirthDate.Year < 1900)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_BirthDateIsInvalid.ToString());
                        txtBirthDate.Focus();
                        return;
                    }
                }
            }
            else
            {
                int mYears = 0;
                int mMonths = 0;

                if (txtYears.Text.Trim() != "")
                {
                    if (Program.IsNumeric(txtYears.Text) == false)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_AgeIsInvalid.ToString());
                        txtYears.Focus();
                        return;
                    }
                    mYears = Convert.ToInt32(txtYears.Text);
                }
                if (txtMonths.Text.Trim() != "")
                {
                    if (Program.IsNumeric(txtMonths.Text) == false)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_AgeIsInvalid.ToString());
                        txtMonths.Focus();
                        return;
                    }
                    mMonths = Convert.ToInt32(txtMonths.Text);
                }

                if (mYears == 0 && mMonths == 0)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_AgeIsInvalid.ToString());
                    return;
                }
                string mBirthDateStr = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue).AddYears(-mYears).AddMonths(-mMonths).ToString("d");
                mBirthDate = Convert.ToDateTime(mBirthDateStr);
            }

            #endregion

            
            if (cboGender.SelectedIndex <= 0)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_GenderIsInvalid.ToString());
                return;
            }

            //extra patient details
            string mErrorMessage = Program.Validate_ExtraPatientDetails(layoutControl1);
            if (mErrorMessage.Trim() != "")
            {
                Program.Display_Error(mErrorMessage, false);
                return;
            }

            #endregion

            try
            {
                if (txtPatientId.Text.Trim().ToLower() == "<<---new--->>")
                {
                    mGenerateCode = 1;
                }

                switch (cboGender.SelectedIndex)
                {
                    case 1: mGender = "F"; break;
                    case 2: mGender = "M"; break;
                }

                mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                mBirthDate = mBirthDate.Date;
                mBirthDate = mBirthDate.AddHours(DateTime.Now.Hour);
                mBirthDate = mBirthDate.AddMinutes(DateTime.Now.Minute);

                #region build extra details for the patient

                DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");
                DataTable mDtExtraDetails = new DataTable("patientextradetails");
                foreach (DataRow mDataRow in mDtExtraFields.Rows)
                {
                    string mFieldName = mDataRow["fieldname"].ToString().ToLower().Trim();
                    string mDataType = mDataRow["datatype"].ToString().Trim();

                    if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().Trim().ToLower())
                    {
                        mDtExtraDetails.Columns.Add(mFieldName, typeof(System.DateTime));
                    }
                    else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().Trim().ToLower())
                    {
                        mDtExtraDetails.Columns.Add(mFieldName, typeof(System.Double));
                    }
                    else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().Trim().ToLower())
                    {
                        mDtExtraDetails.Columns.Add(mFieldName, typeof(System.Int32));
                    }
                    else
                    {
                        mDtExtraDetails.Columns.Add(mFieldName, typeof(System.String));
                    }
                }

                if (mDtExtraDetails.Columns.Count > 0)
                {
                    DataRow mNewRow = mDtExtraDetails.NewRow();

                    foreach (DataRow mDataRow in mDtExtraFields.Rows)
                    {
                        string mFieldName = mDataRow["fieldname"].ToString().ToLower().Trim();
                        string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                        string mDataType = mDataRow["datatype"].ToString().Trim();
                        object mFieldValue = DBNull.Value;

                        #region get user input value

                        if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().Trim().ToLower())
                        {
                            ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                            mFieldValue = mComboBoxEdit.Text;
                        }
                        else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().Trim().ToLower())
                        {
                            CheckEdit mCheckEdit = layoutControl1.GetControlByName("chk" + mFieldName) as CheckEdit;
                            mFieldValue = Convert.ToInt16(mCheckEdit.Checked);
                        }
                        else
                        {
                            if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().Trim().ToLower())
                            {
                                DateEdit mDateEdit = layoutControl1.GetControlByName("txt" + mFieldName) as DateEdit;
                                if (Program.IsNullDate(mDateEdit.EditValue) == false)
                                {
                                    mFieldValue = Convert.ToDateTime(mDateEdit.EditValue).Date;
                                }
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().Trim().ToLower())
                            {
                                Control mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as Control;
                                if (Program.IsMoney(mTextEdit.Text) == true)
                                {
                                    mFieldValue = Convert.ToDouble(mTextEdit.Text);
                                }
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().Trim().ToLower())
                            {
                                Control mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as Control;
                                if (Program.IsMoney(mTextEdit.Text) == true)
                                {
                                    mFieldValue = Convert.ToInt32(mTextEdit.Text);
                                }
                            }
                            else
                            {
                                Control mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as Control;
                                mFieldValue = mTextEdit.Text;
                            }
                        }

                        #endregion

                        mNewRow[mFieldName] = mFieldValue;
                    }

                    mDtExtraDetails.Rows.Add(mNewRow);
                    mDtExtraDetails.AcceptChanges();
                }

                #endregion

                string mMaritalStatusCode = "";
                if (cboMaritalStatus.ItemIndex != -1)
                {
                    mMaritalStatusCode = cboMaritalStatus.GetColumnValue("code").ToString();
                }

                //add 
                mPatient = pMdtRegistrations.Add_Patient(mGenerateCode, txtPatientId.Text, txtSurname.Text, txtFirstName.Text,
                    txtOtherNames.Text, mGender, mBirthDate, txtChronic.Text, txtSevere.Text, txtMajor.Text,
                    mTransDate, mMaritalStatusCode, txtAllergies.Text.Trim(), mDtExtraDetails, mTransDate, Program.gMachineName, Program.gMachineUser, Program.gCurrentUser.Code);
                if (mPatient.Exe_Result == 0)
                {
                    Program.Display_Error(mPatient.Exe_Message);
                    return;
                }
                if (mPatient.Exe_Result == -1)
                {
                    Program.Display_Server_Error(mPatient.Exe_Message);
                    return;
                }

                //refresh
                this.Fill_LookupData();
                this.Data_Display(mPatient);
                Program.Update_CommonNames(txtSurname.Text, txtFirstName.Text, txtOtherNames.Text);
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdNew_Click
        private void cmdNew_Click(object sender, EventArgs e)
        {
            this.Mode_New();
        }
        #endregion

        #region cmdSave_Click
        private void cmdSave_Click(object sender, EventArgs e)
        {
            this.Data_New();
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Book
        private void Book()
        {
            string mGroupCode = "";
            string mSubGroupCode = "";
            string mFunctionName = "Book";

            #region validation

            if (Program.IsDate(Program.gMdiForm.txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                return;
            }

            if (cboBillType.SelectedIndex <= 0)
            {
                //Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_BillTypeIsInvalid.ToString());
                //cboBillType.Focus();
                //return;
            }

            if (cboBillType.SelectedIndex == 2 && cboGroup.ItemIndex == -1)
            {
                //Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_CustomerGroupIsInvalid.ToString());
                //cboGroup.Focus();
                //return;
            }

            if (cboGroup.ItemIndex != -1)
            {
                gBillingGroupCode = cboGroup.GetColumnValue("code").ToString().Trim();
                gBillingGroupDescription = cboGroup.GetColumnValue("description").ToString().Trim();
                gBillingGroupMembershipNo = txtMembershipNo.Text.Trim();
                mGroupCode = cboGroup.GetColumnValue("code").ToString().Trim();
            }

            if (pGroupHasSubGroups == true && cboGroup.ItemIndex != -1)
            {
                if (cboSubGroup.ItemIndex == -1)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_CustomerSubGroupIsInvalid.ToString());
                    cboSubGroup.Focus();
                    return;
                }
                mSubGroupCode = cboSubGroup.GetColumnValue("code").ToString().Trim();
                gBillingSubGroupDescription = cboSubGroup.GetColumnValue("description").ToString().Trim();
            }

            if (pGroupHasId == true && cboGroup.ItemIndex != -1)
            {
                if (txtMembershipNo.Text.Trim() == "")
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.CUS_MembershipIdIsInvalid.ToString());
                    txtMembershipNo.Focus();
                    return;
                }
            }

            #endregion

            try
            {
                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                if (txtPatientId.Text.Trim() != "" && txtPatientId.Text.Trim().ToLower() != "<<---new--->>")
                {
                    #region validate a patient

                    gCurrentPatient = pMdtRegistrations.Get_Patient(txtPatientId.Text);
                    if (gCurrentPatient.Exist == false)
                    {
                        Program.Display_Error(gCurrentPatient.Exe_Message);
                        txtPatientId.Focus();
                        return;
                    }

                    #endregion

                    #region validate billing group

                    if (mGroupCode.Trim() != "")
                    {
                        DataTable mDtMembers = pMdtClientGroupMembers.View("billinggroupmembershipno='" + gBillingGroupMembershipNo
                        + "' and billinggroupcode='" + gBillingGroupCode + "'", "", "", "");
                        if (mDtMembers.Rows.Count > 0)
                        {
                            //if (mDtMembers.Rows[0]["patientcode"].ToString().Trim() != "")
                            //{
                            //    if (mDtMembers.Rows[0]["patientcode"].ToString().Trim().ToLower() != txtPatientId.Text.Trim().ToLower())
                            //    {
                            //        Program.Display_Error("Service denied. Specified membership # is not for this patient");
                            //        return;
                            //    }
                            //}

                            if (Convert.ToInt16(mDtMembers.Rows[0]["membershipstatus"]) == 0)
                            {
                                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_NoServiceMembershipTerminated.ToString());
                                return;
                            }

                            if (pStrictActivation == 1)
                            {
                                if (Convert.ToInt16(mDtMembers.Rows[0]["inactive"]) == 1)
                                {
                                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_NoServiceMemberIsNotActivated.ToString());
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (pStrictActivation == 1)
                            {
                                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_NoServiceInvalidGroupOrMembershipId.ToString());
                                return;
                            }
                        }
                    }

                    #endregion

                    #region validate price category

                    if (cboPriceCategory.ItemIndex == -1)
                    {
                        //Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_PriceCategoryIsInvalid.ToString());
                        //return;
                    }

                    #endregion

                    AfyaPro_Types.clsBooking mLastBooking = pMdtRegistrations.Get_Booking(txtPatientId.Text);

                    if (mLastBooking != null)
                    {
                        gTreatmentPointCode = mLastBooking.WhereTakenCode;

                        if (mTransDate.Date == mLastBooking.BookDate.Date)
                        {
                            gWeight = mLastBooking.Weight;
                            gTemperature = mLastBooking.Temperature;
                        }
                    }

                    gBookingOk = false;

                    this.gBillingGroupCode = mGroupCode;
                    this.gBillingSubGroupCode = mSubGroupCode;
                    this.gBillingGroupMembershipNo = txtMembershipNo.Text.Trim();
                    this.gPriceCategory = cboPriceCategory.GetColumnValue("pricename").ToString().Trim();

                    frmOPDBook mOPDBook = new frmOPDBook(this, mAllowCharging);
                    
                    mOPDBook.ShowDialog();

                    if (gBookingOk == false)
                    {
                        return;
                    }

                    if (mOPDBook.gBillingDone == false)
                    {
                        gCurrentBooking = pMdtRegistrations.Book(mTransDate,
                            txtPatientId.Text, gBillingGroupCode, gBillingSubGroupCode, gBillingGroupMembershipNo,
                            gTreatmentPointCode, gPriceCategory, gTreatmentPointDescription, gWeight, gTemperature, Program.gCurrentUser.Code);

                        if (gCurrentBooking.Exe_Result == 0)
                        {
                            Program.Display_Error(gCurrentBooking.Exe_Message);
                            return;
                        }
                        if (gCurrentBooking.Exe_Result == -1)
                        {
                            Program.Display_Server_Error(gCurrentBooking.Exe_Message);
                            return;
                        }
                    }

                    #region documents printing

                    frmOPDPatientDocuments mOPDPatientDocuments;

                    if (gCurrentBooking.IsNewAttendance == true)
                    {
                        //new patient is registered
                        mOPDPatientDocuments = new frmOPDPatientDocuments();
                        mOPDPatientDocuments.AutoPrint_Documents(txtPatientId.Text,
                            Convert.ToInt16(AfyaPro_Types.clsEnums.PrintedWhens.NewPatientIsRegistered));
                    }
                    else
                    {
                        //patient is reattending
                        mOPDPatientDocuments = new frmOPDPatientDocuments();
                        mOPDPatientDocuments.AutoPrint_Documents(txtPatientId.Text,
                            Convert.ToInt16(AfyaPro_Types.clsEnums.PrintedWhens.PatientIsReAttending));
                    }

                    #endregion

                    if (Program.GrantDeny_FunctionAccess(AfyaPro_Types.clsSystemFunctions.FunctionKeys.opdregistrations_showsuccessmessage.ToString()) == true)
                    {
                        Program.Display_Info(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_BookingSuccess.ToString());
                    }

                    if (Program.GrantDeny_FunctionAccess(AfyaPro_Types.clsSystemFunctions.FunctionKeys.opdregistrations_closeafterregistration.ToString()) == true)
                    {
                        this.Close();
                    }
                    else
                    {
                        this.Mode_New();
                    }
                }
                else
                {
                    Program.Display_Info(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_OnlyRegisteredCanBeBooked.ToString());
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdBook_Click
        private void cmdBook_Click(object sender, EventArgs e)
        {
            this.Book();
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
                cboBillType.Focus();
            }

            pSearchingPatient = false;
        }
        #endregion

        #region txtPatientId_EditValueChanged
        private void txtPatientId_EditValueChanged(object sender, EventArgs e)
        {
            if (pSearchingPatient == true)
            {
                this.pSelectedPatient = this.Search_Patient();
                this.Data_Display(pSelectedPatient);
            }
        }
        #endregion

        #region txtPatientId_Enter
        private void txtPatientId_Enter(object sender, EventArgs e)
        {
            if (txtPatientId.Text.Trim().ToLower() == "<<---new--->>")
            {
                txtPatientId.Text = "";
            }
            else
            {
                txtPatientId.SelectAll();
            }
        }
        #endregion

        #region cboBillType_EditValueChanged
        private void cboBillType_EditValueChanged(object sender, EventArgs e)
        {
            switch (cboBillType.SelectedIndex)
            {
                case 0:
                    {
                        cboGroup.EditValue = null;
                        cboSubGroup.EditValue = null;
                        txtMembershipNo.Text = "";
                        picPatient.Image = null;

                        cboGroup.Properties.ReadOnly = true;
                        cboSubGroup.Properties.ReadOnly = true;
                        txtMembershipNo.Properties.ReadOnly = true;
                        cboPriceCategory.Properties.ReadOnly = false;
                    }
                    break;
                case 1:
                    {
                        cboGroup.EditValue = null;
                        cboSubGroup.EditValue = null;
                        txtMembershipNo.Text = "";
                        picPatient.Image = null;

                        cboGroup.Properties.ReadOnly = true;
                        cboSubGroup.Properties.ReadOnly = true;
                        txtMembershipNo.Properties.ReadOnly = true;
                        cboPriceCategory.Properties.ReadOnly = false;
                    }
                    break;
                case 2:
                    {
                        cboGroup.Properties.ReadOnly = false;
                        cboSubGroup.Properties.ReadOnly = false;
                        txtMembershipNo.Properties.ReadOnly = false;
                        cboPriceCategory.Properties.ReadOnly = true;
                        picPatient.Image = null;
                    }
                    break;
            }
        }
        #endregion

        #region frmOPDRegistrations_KeyDown
        void frmOPDRegistrations_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Program.KeyCode_Save:
                    {
                        this.Data_New();
                    }
                    break;
                case Program.KeyCode_SeachPatient:
                    {
                        pSearchingPatient = true;

                        frmSearchPatient mSearchPatient = new frmSearchPatient(txtPatientId);
                        mSearchPatient.ShowDialog();

                        pSearchingPatient = false;
                    }
                    break;
                case Program.KeyCode_Book:
                    {
                        this.Book();
                    }
                    break;
            }
        }
        #endregion
    }
}