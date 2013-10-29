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
    public partial class frmOPDEditPatientDetails : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsRegistrations pMdtRegistrations;
        private AfyaPro_MT.clsTreatmentPoints pMdtTreatmentPoints;
        private AfyaPro_MT.clsCountries pMdtCountries;
        private AfyaPro_MT.clsRegions pMdtRegions;
        private AfyaPro_MT.clsDistricts pMdtDistricts;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_MT.clsPatientExtraFields pMdtPatientExtraFields;
        private AfyaPro_MT.clsReporter pMdtReporter;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();
        internal AfyaPro_Types.clsPatient gCurrentPatient;

        private Type pType;
        private string pClassName = "";
        private AfyaPro_Types.clsPatient pSelectedPatient = null;
        private string pCurrPatientId = "";
        private string pPrevPatientId = "";
        private bool pFirstTimeLoad = true;
        private List<Object> pObjectsList = new List<Object>();

        private int pFormWidth = 0;
        private int pFormHeight = 0;
        private bool pSearchingPatient = false;

        private DataTable pDtExtraFields = new DataTable("extrafields");

        #endregion

        #region frmOPDEditPatientDetails
        public frmOPDEditPatientDetails()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmOPDEditPatientDetails";
            this.Shown += new EventHandler(frmOPDEditPatientDetails_Shown);
            this.KeyDown += new KeyEventHandler(frmOPDEditPatientDetails_KeyDown);

            try
            {
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

                pMdtReporter = (AfyaPro_MT.clsReporter)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReporter),
                    Program.gMiddleTier + "clsReporter");

                cmdSave.ImageIndex = 0;

                this.Load_ExtraControls();

                this.Fill_LookupData();
                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.opdeditpatientdetails_customizelayout.ToString());

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
                pObjectsList.Add(cboGender);
                pObjectsList.Add(txbChronic);
                pObjectsList.Add(txbSevere);
                pObjectsList.Add(txbMajor);
                pObjectsList.Add(cmdSearch);
                pObjectsList.Add(cmdSave);
                pObjectsList.Add(cmdClose);

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
                DataTable mDtExtraFields = pMdtPatientExtraFields.View("filteronvaluefrom='" + mFilterOnValueFrom + "'", "", "", "");

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

        #region Apply_ControlProperties
        private void Apply_ControlProperties(Control mControl, DataView mDvProperties, int mInformationType)
        {
            //int mRowIndex = -1;
            //if (mControl is TextEdit)
            //{
            //    mRowIndex = mDvProperties.Find(mInformationType);
            //    if (mRowIndex >= 0)
            //    {
            //        switch (Convert.ToInt16(mDvProperties[mRowIndex]["charactercasingoption"]))
            //        {
            //            case 1: ((TextEdit)mControl).Properties.CharacterCasing = CharacterCasing.Upper; break;
            //            case 2: ((TextEdit)mControl).Properties.CharacterCasing = CharacterCasing.Lower; break;
            //            default: ((TextEdit)mControl).Properties.CharacterCasing = CharacterCasing.Normal; break;
            //        }
            //    }
            //}
            //else if (mControl is ComboBoxEdit)
            //{
            //    mRowIndex = mDvProperties.Find(mInformationType);
            //    if (mRowIndex >= 0)
            //    {
            //        switch (Convert.ToInt16(mDvProperties[mRowIndex]["charactercasingoption"]))
            //        {
            //            case 1: cboAddrCountry.Properties.CharacterCasing = CharacterCasing.Upper; break;
            //            case 2: cboAddrCountry.Properties.CharacterCasing = CharacterCasing.Lower; break;
            //            default: cboAddrCountry.Properties.CharacterCasing = CharacterCasing.Normal; break;
            //        }

            //        if (Convert.ToInt16(mDvProperties[mRowIndex]["freetext"]) == 1)
            //        {
            //            cboAddrCountry.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            //        }
            //        else
            //        {
            //            cboAddrCountry.Properties.TextEditStyle = TextEditStyles.Standard;
            //        }
            //    }
            //}
        }
        #endregion

        #region frmOPDEditPatientDetails_Load
        private void frmOPDEditPatientDetails_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Apply_Language("frmOPDRegistrations", pObjectsList);
            Program.Restore_FormSize(this);

            txbBirthDateFormat.Text = "(" + Program.gCulture.DateTimeFormat.ShortDatePattern + ")";

            this.pFormWidth = this.Width;
            this.pFormHeight = this.Height;

            Program.Center_Screen(this);

            this.Append_ShortcutKeys();
        }
        #endregion

        #region frmOPDEditPatientDetails_Shown
        void frmOPDEditPatientDetails_Shown(object sender, EventArgs e)
        {
            txtPatientId.Focus();
        }
        #endregion

        #region frmOPDEditPatientDetails_FormClosing
        private void frmOPDEditPatientDetails_FormClosing(object sender, FormClosingEventArgs e)
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

        #region Append_ShortcutKeys
        private void Append_ShortcutKeys()
        {
            cmdSearch.Text = cmdSearch.Text + " (" + Program.KeyCode_SeachPatient.ToString() + ")";
            cmdSave.Text = cmdSave.Text + " (" + Program.KeyCode_Save.ToString() + ")";
        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            string mFunctionName = "Fill_LookupData";

            try
            {
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
            cboGender.SelectedIndex = 0;
            txtBirthDate.Text = "";
            txtYears.Text = "";
            txtMonths.Text = "";
            txtChronic.Text = "";
            txtSevere.Text = "";
            txtMajor.Text = "";
            cboMaritalStatus.EditValue = null;
            txtAllergies.Text = "";

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

                        pCurrPatientId = mPatient.code;
                        pPrevPatientId = pCurrPatientId;

                        System.Media.SystemSounds.Beep.Play();
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
        }
        #endregion

        #region Data_Edit
        private void Data_Edit()
        {
            string mGender = "F";
            DateTime mBirthDate = new DateTime();
            DateTime mTransDate = new DateTime();
            AfyaPro_Types.clsPatient mPatient = new AfyaPro_Types.clsPatient();

            String mFunctionName = "Data_Edit";

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

                //Edit 
                mPatient = pMdtRegistrations.Edit_Patient(txtPatientId.Text, txtSurname.Text, txtFirstName.Text,
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

                Program.Display_Info(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientDetailsUpdated.ToString());
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

        #region cmdSave_Click
        private void cmdSave_Click(object sender, EventArgs e)
        {
            this.Data_Edit();
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region cmdSearch_Click
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            pSearchingPatient = true;

            frmSearchPatient mSearchPatient = new frmSearchPatient(txtPatientId);
            mSearchPatient.ShowDialog();

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

        #region frmOPDEditPatientDetails_KeyDown
        void frmOPDEditPatientDetails_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Program.KeyCode_Save:
                    {
                        this.Data_Edit();
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
            }
        }
        #endregion
    }
}