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
    public partial class frmRCHClients : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsRCHClients pMdtRCHClients;
        private AfyaPro_MT.clsTreatmentPoints pMdtTreatmentPoints;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_MT.clsPatientExtraFields pMdtPatientExtraFields;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();
        internal AfyaPro_Types.clsRchClient gCurrentClient;

        private Type pType;
        private string pClassName = "";
        private AfyaPro_Types.clsRchClient pSelectedClient = null;
        private string pCurrClientId = "";
        private string pPrevClientId = "";
        private List<Object> pObjectsList = new List<Object>();

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private bool pSearchingClient = false;

        private ComboBoxItemCollection pRchServices;
        private DataTable pDtRchServices = new DataTable("rchservices");
        private DataTable pDtExtraFields = new DataTable("extrafields");

        #endregion

        #region frmRCHClients
        public frmRCHClients()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmRCHClients";

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                pMdtTreatmentPoints = (AfyaPro_MT.clsTreatmentPoints)Activator.GetObject(
                    typeof(AfyaPro_MT.clsTreatmentPoints),
                    Program.gMiddleTier + "clsTreatmentPoints");

                pMdtAutoCodes = (AfyaPro_MT.clsAutoCodes)Activator.GetObject(
                    typeof(AfyaPro_MT.clsAutoCodes),
                    Program.gMiddleTier + "clsAutoCodes");

                pMdtRCHClients = (AfyaPro_MT.clsRCHClients)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRCHClients),
                    Program.gMiddleTier + "clsRCHClients");

                pMdtPatientExtraFields = (AfyaPro_MT.clsPatientExtraFields)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPatientExtraFields),
                    Program.gMiddleTier + "clsPatientExtraFields");

                //columns for birth methods
                pDtRchServices.Columns.Add("description", typeof(System.String));
                pDtRchServices.Columns.Add("fieldname", typeof(System.String));
                pDtRchServices.Columns.Add("fieldindex", typeof(System.Int32));
                pRchServices = cboService.Properties.Items;

                cmdSave.ImageIndex = 0;
                cmdNew.ImageIndex = 2;

                this.Load_ExtraControls();

                this.Fill_RchServices();
                this.Fill_LookupData();

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.opdregistrations_customizelayout.ToString());

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
                pObjectsList.Add(txbMajor);
                pObjectsList.Add(cmdSave);
                pObjectsList.Add(cmdNew);
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

                    if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                    {
                        mLayoutControlItem = new LayoutControlItem();
                        mLayoutControlItem.Name = "txb" + mFieldName;
                        mLayoutControlItem.CustomizationFormText = mFieldCaption;
                        mLayoutControlItem.Text = mFieldCaption;
                        ComboBoxEdit mComboBoxEdit = new ComboBoxEdit();
                        mComboBoxEdit.Name = "cbo" + mFieldName;
                        mLayoutControlItem.Control = mComboBoxEdit;
                        pObjectsList.Add(mLayoutControlItem);
                        layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                        //if this will be used to filter other values, add the event to do the job
                        if (mDvExtraFilterFields.Find(mFieldName) >= 0)
                        {
                            mComboBoxEdit.EditValueChanged += new EventHandler(mComboBoxEdit_EditValueChanged);
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
                        pObjectsList.Add(mLayoutControlItem);
                        layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
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
                            pObjectsList.Add(mLayoutControlItem);
                            layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
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
                            pObjectsList.Add(mLayoutControlItem);
                            layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
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

        #region frmRCHClients_Load
        private void frmRCHClients_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmRCHClients_Load";

            try
            {
                Program.Restore_FormLayout(layoutControl1, this.Name);
                Program.Apply_Language(this.Name, pObjectsList);
                Program.Restore_FormSize(this);

                txbBirthDateFormat.Text = "(" + Program.gCulture.DateTimeFormat.ShortDatePattern + ")";

                this.pFormWidth = this.Width;
                this.pFormHeight = this.Height;

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
            cmdSearch.Text = cmdSearch.Text + " (" + Program.KeyCode_RchSearchClient.ToString() + ")";
            cmdSave.Text = cmdSave.Text + " (" + Program.KeyCode_Save.ToString() + ")";
            cmdService.Text = cmdService.Text + " (" + Program.KeyCode_RchGetService.ToString() + ")";
        }
        #endregion

        #region frmRCHClients_Shown
        private void frmRCHClients_Shown(object sender, EventArgs e)
        {
            this.Mode_New();
        }
        #endregion

        #region frmRCHClients_FormClosing
        private void frmRCHClients_FormClosing(object sender, FormClosingEventArgs e)
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

        #region Fill_RchServices
        private void Fill_RchServices()
        {
            string mFunctionName = "Fill_RchServices";

            try
            {
                pRchServices.Clear();
                pDtRchServices.Rows.Clear();

                DataTable mDtRchServices = pMdtRCHClients.Get_RCHServices(Program.gLanguageName, "grdRCHServices");

                int mFieldIndex = 0;
                foreach (DataRow mDataRow in mDtRchServices.Rows)
                {
                    pRchServices.Add(mDataRow["description"].ToString().Trim());

                    DataRow mNewRow = pDtRchServices.NewRow();
                    mNewRow["description"] = mDataRow["description"];
                    mNewRow["fieldname"] = mDataRow["fieldname"];
                    mNewRow["fieldindex"] = mFieldIndex;
                    pDtRchServices.Rows.Add(mNewRow);
                    pDtRchServices.AcceptChanges();

                    mFieldIndex++;
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

            pPrevClientId = "";
            pCurrClientId = pPrevClientId;
        }
        #endregion

        #region Data_Display
        internal void Data_Display(AfyaPro_Types.clsRchClient mPatient)
        {
            String mFunctionName = "Data_Display";

            try
            {
                this.Data_Clear();

                if (mPatient != null)
                {
                    if (mPatient.Exist == true)
                    {
                        txtClientId.Text = mPatient.code;
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

                        this.Display_ExtraDetails();

                        pCurrClientId = mPatient.code;
                        pPrevClientId = pCurrClientId;

                        System.Media.SystemSounds.Beep.Play();
                    }
                    else
                    {
                        pCurrClientId = txtClientId.Text;
                        pPrevClientId = pCurrClientId;
                    }
                }
                else
                {
                    pCurrClientId = txtClientId.Text;
                    pPrevClientId = pCurrClientId;
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
                DataTable mDtPatients = pMdtRCHClients.View_Clients("code='" + txtClientId.Text.Trim() + "'", "");

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

        #region Search_Client
        private AfyaPro_Types.clsRchClient Search_Client()
        {
            string mFunctionName = "Search_Client";

            try
            {
                gCurrentClient = pMdtRCHClients.Get_Client(txtClientId.Text);
                return gCurrentClient;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
        }
        #endregion

        #region txtClientId_KeyDown
        private void txtClientId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.pSelectedClient = this.Search_Client();
                this.Data_Display(pSelectedClient);
            }
        }
        #endregion

        #region txtClientId_Leave
        private void txtClientId_Leave(object sender, EventArgs e)
        {
            pCurrClientId = txtClientId.Text;

            if (pCurrClientId.Trim().ToLower() != pPrevClientId.Trim().ToLower())
            {
                this.pSelectedClient = this.Search_Client();
                this.Data_Display(pSelectedClient);
            }

            if (txtClientId.Text.Trim() == "")
            {
                Int16 mGenerateCode = pMdtAutoCodes.Auto_Generate_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.rchcustomerno));
                if (mGenerateCode == -1)
                {
                    return;
                }
                if (mGenerateCode == 1)
                {
                    txtClientId.Text = "<<---New--->>";
                }
                else
                {
                    txtClientId.Focus();
                }
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
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.rchcustomerno));
                if (mGenerateCode == -1)
                {
                    Program.Display_Server_Error("");
                    return;
                }

                txtClientId.Text = "";
                this.Data_Clear();

                if (mGenerateCode == 1)
                {
                    txtClientId.Text = "<<---New--->>";
                    txtSurname.Focus();
                }
                else
                {
                    txtClientId.Focus();
                }
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
            AfyaPro_Types.clsRchClient mRchClient = new AfyaPro_Types.clsRchClient();

            String mFunctionName = "Data_New";

            #region validation

            if (Program.IsDate(Program.gMdiForm.txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                return;
            }

            if (txtClientId.Text.Trim() == "" && txtClientId.Text.Trim().ToLower() != "<<---new--->>")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RCH_ClientCodeIsInvalid.ToString());
                txtClientId.Focus();
                return;
            }

            if (txtSurname.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RCH_SurnameIsInvalid.ToString());
                txtSurname.Focus();
                return;
            }

            if (txtFirstName.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RCH_FirstNameIsInvalid.ToString());
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
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RCH_BirthDateIsInvalid.ToString());
                        txtBirthDate.Focus();
                        return;
                    }
                    else if (txtBirthDate.Text.Length == 4)
                    {
                        if (Convert.ToInt32(txtBirthDate.Text) < 1900)
                        {
                            Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RCH_BirthDateIsInvalid.ToString());
                            txtBirthDate.Focus();
                            return;
                        }
                        mBirthDate = Convert.ToDateTime("01/01/" + txtBirthDate.Text);
                    }
                }
                else
                {
                    if (Program.IsDate(txtBirthDate.Text) == false)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RCH_BirthDateIsInvalid.ToString());
                        txtBirthDate.Focus();
                        return;
                    }
                    mBirthDate = Convert.ToDateTime(txtBirthDate.Text);
                    if (mBirthDate.Year < 1900)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RCH_BirthDateIsInvalid.ToString());
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
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RCH_AgeIsInvalid.ToString());
                        txtYears.Focus();
                        return;
                    }
                    mYears = Convert.ToInt32(txtYears.Text);
                }
                if (txtMonths.Text.Trim() != "")
                {
                    if (Program.IsNumeric(txtMonths.Text) == false)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RCH_AgeIsInvalid.ToString());
                        txtMonths.Focus();
                        return;
                    }
                    mMonths = Convert.ToInt32(txtMonths.Text);
                }

                if (mYears == 0 && mMonths == 0)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RCH_AgeIsInvalid.ToString());
                    return;
                }
                string mBirthDateStr = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue).AddYears(-mYears).AddMonths(-mMonths).ToString("d");
                mBirthDate = Convert.ToDateTime(mBirthDateStr);
            }

            #endregion


            if (cboGender.SelectedIndex <= 0)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RCH_GenderIsInvalid.ToString());
                return;
            }

            #endregion

           // MessageBox.Show("Hallo");
            try
            {
                if (txtClientId.Text.Trim().ToLower() == "<<---new--->>")
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

                //add 
                mRchClient = pMdtRCHClients.Add_Client(mGenerateCode, txtClientId.Text, txtSurname.Text, txtFirstName.Text,
                    txtOtherNames.Text, mGender, mBirthDate, mTransDate, mDtExtraDetails, mTransDate, Program.gMachineName, 
                    Program.gMachineUser, Program.gCurrentUser.Code);

              
                if (mRchClient.Exe_Result == 0)
                {
                    Program.Display_Error(mRchClient.Exe_Message);
                    return;
                }

                MessageBox.Show("Hallo");
                if (mRchClient.Exe_Result == -1)
                {
                    Program.Display_Server_Error(mRchClient.Exe_Message);
                    return;
                }

                //refresh
                this.Fill_LookupData();
                this.Data_Display(mRchClient);
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

        #region cmdSearch_Click
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            pSearchingClient = true;

            frmSearchRCHClient mSearchRCHClient = new frmSearchRCHClient(txtClientId);
            mSearchRCHClient.ShowDialog();

            pSearchingClient = false;
        }
        #endregion

        #region txtClientId_EditValueChanged
        private void txtClientId_EditValueChanged(object sender, EventArgs e)
        {
            if (pSearchingClient == true)
            {
                this.pSelectedClient = this.Search_Client();
                this.Data_Display(pSelectedClient);
            }
        }
        #endregion

        #region txtClientId_Enter
        private void txtClientId_Enter(object sender, EventArgs e)
        {
            if (txtClientId.Text.Trim().ToLower() == "<<---new--->>")
            {
                txtClientId.Text = "";
            }
            else
            {
                txtClientId.SelectAll();
            }
        }
        #endregion

        #region Get_Service
        private void Get_Service()
        {
            DataView mDvRchServices = new DataView();
            mDvRchServices.Table = pDtRchServices;
            mDvRchServices.Sort = "fieldindex";

            int mRowIndex = mDvRchServices.Find(cboService.SelectedIndex);

            if (mRowIndex < 0)
            {
                Program.Display_Error("Invalid service selection");
                return;
            }

            string mServiceType = mDvRchServices[mRowIndex]["fieldname"].ToString().Trim();

            bool mValidClient = Program.Validate_RchClient(mServiceType, txtClientId.Text);

            this.Cursor = Cursors.WaitCursor;

            if (mValidClient == true)
            {
                //familyplanning
                if (mServiceType.ToLower() == AfyaPro_Types.clsEnums.RCHServices.familyplanning.ToString().ToLower())
                {
                    frmRCHFamilyPlanning mRCHFamilyPlanning = new frmRCHFamilyPlanning();
                    mRCHFamilyPlanning.gCalledFromClientsRegister = true;
                    mRCHFamilyPlanning.gClientCode = txtClientId.Text.Trim();
                    mRCHFamilyPlanning.ShowDialog();

                    if (mRCHFamilyPlanning.gDataSaved == true)
                    {
                        this.Mode_New();
                    }
                }

                //antenatalcare
                if (mServiceType.ToLower() == AfyaPro_Types.clsEnums.RCHServices.antenatalcare.ToString().ToLower())
                {
                    frmRCHAntenatalCare mRCHAntenatalCare = new frmRCHAntenatalCare();
                    mRCHAntenatalCare.gCalledFromClientsRegister = true;
                    mRCHAntenatalCare.gClientCode = txtClientId.Text.Trim();
                    mRCHAntenatalCare.ShowDialog();

                    if (mRCHAntenatalCare.gDataSaved == true)
                    {
                        this.Mode_New();
                    }
                }

                //postnatalcare
                if (mServiceType.ToLower() == AfyaPro_Types.clsEnums.RCHServices.postnatalcare.ToString().ToLower())
                {
                    frmRCHPostnatalCare mRCHPostnatalCare = new frmRCHPostnatalCare();
                    mRCHPostnatalCare.gCalledFromClientsRegister = true;
                    mRCHPostnatalCare.gClientCode = txtClientId.Text.Trim();
                    mRCHPostnatalCare.ShowDialog();

                    if (mRCHPostnatalCare.gDataSaved == true)
                    {
                        this.Mode_New();
                    }
                }

                //childrenhealth
                if (mServiceType.ToLower() == AfyaPro_Types.clsEnums.RCHServices.childrenhealth.ToString().ToLower())
                {
                    frmRCHChildren mRCHChildren = new frmRCHChildren();
                    mRCHChildren.gCalledFromClientsRegister = true;
                    mRCHChildren.gClientCode = txtClientId.Text.Trim();
                    mRCHChildren.ShowDialog();

                    if (mRCHChildren.gDataSaved == true)
                    {
                        this.Mode_New();
                    }
                }

                //vaccinations
                if (mServiceType.ToLower() == AfyaPro_Types.clsEnums.RCHServices.vaccinations.ToString().ToLower())
                {
                    frmRCHVaccinations mRCHVaccinations = new frmRCHVaccinations(-1, "");
                    mRCHVaccinations.gCalledFromClientsRegister = true;
                    mRCHVaccinations.gClientCode = txtClientId.Text.Trim();
                    mRCHVaccinations.ShowDialog();

                    if (mRCHVaccinations.gDataSaved == true)
                    {
                        this.Mode_New();
                    }
                }
            }

            this.Cursor = Cursors.Default;
        }
        #endregion

        #region cmdService_Click
        private void cmdService_Click(object sender, EventArgs e)
        {
            this.Get_Service();
        }
        #endregion

        #region frmRCHClients_KeyDown
        void frmRCHClients_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Program.KeyCode_Save:
                    {
                        this.Data_New();
                    }
                    break;
                case Program.KeyCode_RchSearchClient:
                    {
                        pSearchingClient = true;

                        frmSearchRCHClient mSearchRCHClient = new frmSearchRCHClient(txtClientId);
                        mSearchRCHClient.ShowDialog();

                        pSearchingClient = false;
                    }
                    break;
                case Program.KeyCode_RchGetService:
                    {
                        this.Get_Service();
                    }
                    break;
            }
        }
        #endregion
    }
}