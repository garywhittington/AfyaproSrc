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
using System.Data.Odbc;
using System.Data.OleDb;
using System.Xml.Serialization;

namespace AfyaPro_NextGen
{
    public partial class frmSMSPatientRegistration : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsSMS pMdtSMS;
        private AfyaPro_MT.clsRegistrations pMdtRegistrations;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_MT.clsPatientExtraFields pMdtPatientExtraFields;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();
       
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
        internal AfyaPro_Types.clsPatient gCurrentPatient;
        internal AfyaPro_Types.clsBooking gCurrentBooking = new AfyaPro_Types.clsBooking();

        private DataTable pDtAllGroups = new DataTable("groups");
        private DataTable pDtClientGroups = new DataTable("groups");
        private DataTable pDtSubGroups = new DataTable("subgroups");

        private Type pType;
        private string pClassName = "";
        private AfyaPro_Types.clsPatient pSelectedPatient = null;
        private string pCurrPatientId = "";
        private string pPrevPatientId = "";
        private string pPhoneNo = "";
        private bool pFirstTimeLoad = true;
        private List<Object> pObjectsList = new List<Object>();

        private bool pGroupHasId = false;
        private bool pGroupHasSubGroups = false;

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private int pStrictActivation = 0;

        private bool pSearchingPatient = false;
       
        private DataTable pDtExtraFields = new DataTable("extrafields");
        private DataTable pDtPriceCategories = new DataTable("pricecategories");

        #endregion

        #region frmSMSPatientRegistration
        public frmSMSPatientRegistration()
        {
            InitializeComponent();
            
            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmSMSPatientRegistration";
            this.Shown += new EventHandler(frmSMSPatientRegistration_Shown);
            this.KeyDown += new KeyEventHandler(frmSMSPatientRegistration_KeyDown);

            try
            {

                pMdtAutoCodes = (AfyaPro_MT.clsAutoCodes)Activator.GetObject(
                   typeof(AfyaPro_MT.clsAutoCodes),
                   Program.gMiddleTier + "clsAutoCodes");

                pMdtSMS = (AfyaPro_MT.clsSMS)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSMS),
                    Program.gMiddleTier + "clsSMS");

                pMdtRegistrations = (AfyaPro_MT.clsRegistrations)Activator.GetObject(
                   typeof(AfyaPro_MT.clsRegistrations),
                   Program.gMiddleTier + "clsRegistrations");

                pMdtPatientExtraFields = (AfyaPro_MT.clsPatientExtraFields)Activator.GetObject(
                   typeof(AfyaPro_MT.clsPatientExtraFields),
                   Program.gMiddleTier + "clsPatientExtraFields");

                    this.Load_ExtraControls();

                    pDtClientGroups.Columns.Add("Code", typeof(System.String));
                    pDtClientGroups.Columns.Add("Group", typeof(System.String));
                    pDtClientGroups.Columns.Add("Description", typeof(System.String));
                    cboGroup.Properties.DisplayMember = "Description";
                    cboGroup.Properties.ValueMember = "Code";
                    cboGroup.Properties.BestFitMode = BestFitMode.BestFit;
                    cboGroup.Properties.BestFit();

                
                

                this.Fill_LookupData();
                //layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                //    AfyaPro_Types.clsSystemFunctions.FunctionKeys.opdregistrations_customizelayout.ToString());

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
               // pObjectsList.Add(txbBillType);
                pObjectsList.Add(cmdSearch);
                pObjectsList.Add(cmdSave);
                //pObjectsList.Add(cmdNew);
                pObjectsList.Add(cmdClose);
                //pObjectsList.Add(cmdBook);

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
                            //mComboBoxEdit.EditValueChanged += new EventHandler(mComboBoxEdit_EditValueChanged);
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

        #region frmSMSPatientRegistration_Load
        private void frmSMSPatientRegistration_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmSMSPatientRegistration_Load";

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
            cmdSearch.Text = cmdSearch.Text + " (" + Program.KeyCode_SeachPatient.ToString() + ")";
            cmdSave.Text = cmdSave.Text + " (" + Program.KeyCode_Save.ToString() + ")";
           
        }
        #endregion

        #region frmSMSPatientRegistration_Shown
        void frmSMSPatientRegistration_Shown(object sender, EventArgs e)
        {
            this.Mode_New();
        }
        #endregion

        #region frmSMSPatientRegistration_FormClosing
        private void frmSMSPatientRegistration_FormClosing(object sender, FormClosingEventArgs e)
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

                pDtClientGroups.Rows.Clear();
                DataTable mDtGroups = pMdtSMS.Get_PatientGroups();

                foreach (DataRow mDataRow in mDtGroups.Rows)
                {
                    mNewRow = pDtClientGroups.NewRow();
                    mNewRow["Code"] = mDataRow["code"].ToString();
                    mNewRow["Group"] = mDataRow["colgroup"].ToString();
                    mNewRow["Description"] = mDataRow["description"].ToString();
                    pDtClientGroups.Rows.Add(mNewRow);
                    pDtClientGroups.AcceptChanges();

                }


                cboGroup.Properties.DataSource = pDtClientGroups;

                //DataTable mDtPatients = new DataTable();
                //mDtPatients = pMdtSMS.Get_SMSPatientsList();
                //// MessageBox.Show("Hallo");
                //foreach (DataColumn mCol in mDtPatients.Columns)
                //{

                //    if (mCol.ColumnName == "code")
                //    {
                //        mDtPatients.Columns["code"].ColumnName = "Patient Code";

                //    }
                //    if (mCol.ColumnName == "fullname")
                //    {

                //        mDtPatients.Columns["fullname"].ColumnName = "Patient Name";

                //    }
                //    if (mCol.ColumnName == "phonenumber")
                //    {
                //        mDtPatients.Columns["phonenumber"].ColumnName = "Phone";

                //    }
                //    if (mCol.ColumnName == "colgroup")
                //    {
                //        mDtPatients.Columns["colgroup"].ColumnName = "Group";

                //    }




                //}

                //mDtPatients.AcceptChanges();
                //grdMain.DataSource = mDtPatients;

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
            cboGroup.EditValue = null;
            cboGroup.EditValue = null;
            cboMaritalStatus.EditValue = null;
            txtPhoneNo.Text = "";

           
            gWeight = 0;
            gTemperature = 0;

            pStrictActivation = 0;

                      

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
                        txtPhoneNo.Text = pPhoneNo;
                        cboMaritalStatus.ItemIndex = Program.Get_LookupItemIndex(cboMaritalStatus, "code", mPatient.maritalstatuscode);
                        

                        this.Display_ExtraDetails();

                        #region get last attendance details

                        //AfyaPro_Types.clsBooking mLastBooking = pMdtRegistrations.Get_Booking(txtPatientId.Text);

                        //cboBillType.SelectedIndex = 0;

                        //if (mLastBooking != null)
                        //{
                        //    if (mLastBooking.Booking.Trim() != "")
                        //    {
                        //        gBillingGroupCode = mLastBooking.BillingGroupCode;
                        //        gBillingGroupDescription = mLastBooking.BillingGroupDescription;
                        //        gBillingSubGroupCode = mLastBooking.BillingSubGroupCode;
                        //        gBillingSubGroupDescription = mLastBooking.BillingSubGroupDescription;
                        //        gBillingGroupMembershipNo = mLastBooking.BillingGroupMembershipNo;
                        //        gPriceCategory = mLastBooking.PriceName;

                        //        if (Program.IsNullDate(mLastBooking.BookDate) == false)
                        //        {
                        //            gLastAttendanceDate = Convert.ToDateTime(mLastBooking.BookDate);
                        //        }

                        //        if (gBillingGroupCode.Trim() == "")
                        //        {
                        //            cboBillType.SelectedIndex = 1;
                        //            cboPriceCategory.ItemIndex = Program.Get_LookupItemIndex(cboPriceCategory, "pricename", gPriceCategory);
                        //        }
                        //        else
                        //        {
                        //            cboBillType.SelectedIndex = 2;
                        //            cboGroup.ItemIndex = Program.Get_LookupItemIndex(cboGroup, "code", gBillingGroupCode);
                        //            cboSubGroup.ItemIndex = Program.Get_LookupItemIndex(cboSubGroup, "code", gBillingSubGroupCode);
                        //            txtMembershipNo.Text = gBillingGroupMembershipNo;
                        //        }
                        //    }
                        //}

                        #endregion

                        pCurrPatientId = mPatient.code;
                        pPrevPatientId = pCurrPatientId;
                                               
                       // System.Media.SystemSounds.Beep.Play();

                        
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

            //try
            //{
            //    pDtSubGroups.Rows.Clear();
            //    if (cboGroup.ItemIndex == -1)
            //    {
            //        return;
            //    }

            //    pGroupHasId = false;
            //    pGroupHasSubGroups = false;
            //    pStrictActivation = 0;
            //    cboSubGroup.EditValue = null;
            //    txtMembershipNo.Text = "";

            //    string mGroupCode = cboGroup.GetColumnValue("code").ToString().Trim();

            //    DataTable mDtGroups = pMdtClientGroups.View("code='" + mGroupCode + "'", "", Program.gLanguageName, "grdCUSCustomerGroups");
            //    if (mDtGroups.Rows.Count > 0)
            //    {
            //        pGroupHasId = Convert.ToBoolean(mDtGroups.Rows[0]["hasid"]);
            //        pGroupHasSubGroups = Convert.ToBoolean(mDtGroups.Rows[0]["hassubgroups"]);
            //        pStrictActivation = Convert.ToInt16(mDtGroups.Rows[0]["strictactivation"]);
            //        cboPriceCategory.Properties.ReadOnly = false;
            //        int mPriceIndex = Program.Get_LookupItemIndex(cboPriceCategory, "pricename", mDtGroups.Rows[0]["pricecategory"].ToString().Trim());
            //        cboPriceCategory.ItemIndex = mPriceIndex;
            //        cboPriceCategory.Properties.ReadOnly = true;
            //        gPriceCategory = mDtGroups.Rows[0]["pricecategory"].ToString().Trim();
            //    }

            //    if (pGroupHasSubGroups == true)
            //    {
            //        DataTable mDtSubGroups = pMdtClientSubGroups.View("groupcode='" + mGroupCode + "'", "code", Program.gLanguageName, "grdCUSCustomerSubGroups");

            //        mNewRow = pDtSubGroups.NewRow();
            //        mNewRow["code"] = "";
            //        mNewRow["description"] = "";
            //        pDtSubGroups.Rows.Add(mNewRow);
            //        pDtSubGroups.AcceptChanges();

            //        foreach (DataRow mDataRow in mDtSubGroups.Rows)
            //        {
            //            mNewRow = pDtSubGroups.NewRow();
            //            mNewRow["code"] = mDataRow["code"].ToString();
            //            mNewRow["description"] = mDataRow["description"].ToString();
            //            pDtSubGroups.Rows.Add(mNewRow);
            //            pDtSubGroups.AcceptChanges();
            //        }

            //        foreach (DataColumn mDataColumn in pDtSubGroups.Columns)
            //        {
            //            mDataColumn.Caption = mDtSubGroups.Columns[mDataColumn.ColumnName].Caption;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Program.Display_Error(pClassName, mFunctionName, ex.Message);
            //    return;
            //}
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

                //grpBilling.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;
                //txbBook.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.OnlyInCustomization;
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
            AfyaPro_Types.clsPatient mPatientCheck = new AfyaPro_Types.clsPatient();
            
            String mFunctionName = "Data_New";

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

            if (grdMain.Rows.Count > 0)
            {
                int count =0;
                int PatientsSaved = 0;
                DataTable mFakeTable = new DataTable();
                DialogResult rst = MessageBox.Show("Do you want to save the imported patients?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rst == DialogResult.No)
                {
                   
                    grdMain.DataSource = mFakeTable;
                    return;
                }
                else
                {
                   
                    foreach (DataGridViewRow mRow in grdMain.Rows)
                    {
                        mPatientCheck = null;
                       
                        string mPatientId = mRow.Cells[0].Value.ToString();

                        if (mRow.Cells[4].Value.ToString() != "")
                        {


                            if (mPatientId != "")
                            {
                                mPatientCheck = pMdtRegistrations.Get_Patient(mPatientId);
                            }
                            else
                            {
                                mPatient = pMdtRegistrations.Add_Patient(1, "", mRow.Cells[2].Value.ToString(), mRow.Cells[1].Value.ToString(),
                                mRow.Cells[3].Value.ToString(), mGender, mBirthDate, txtChronic.Text, txtSevere.Text, txtMajor.Text,
                                mTransDate, "", "", mDtExtraDetails, mTransDate, Program.gMachineName, Program.gMachineUser, Program.gCurrentUser.Code);

                            }
                            if (mPatientCheck != null)
                            {

                                mPatient = mPatientCheck;
                            }

                            pPhoneNo = mRow.Cells[4].Value.ToString();
                            //this.Fill_LookupData();
                            this.Data_Display(mPatient);

                            pMdtSMS.Add_SMSPatient(txtPatientId.Text, txtFirstName.Text + " " + txtSurname.Text, pPhoneNo, mRow.Cells[5].Value.ToString());
                            PatientsSaved += 1;
                        }
                        else
                        {

                            count += 1;
                        }
                        }
                    
                }

                txtPatientId.Text = "<<---new--->>";
                ((frmSMSMobilePatientsList)Program.gMdiForm.ActiveMdiChild).Close();

                frmSMSMobilePatientsList mTpt = new frmSMSMobilePatientsList();
                mTpt.MdiParent = Program.gMdiForm;
                mTpt.Show();
                
                MessageBox.Show("[" + PatientsSaved + "] patients successfully added to the list", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (count > 0)
                {
                    MessageBox.Show(count + " patient(s) were not saved because phone number was missing", "Phone number", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                count = 0;
                PatientsSaved = 0;
                grdMain.DataSource = mFakeTable;
                return;
            }

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

            string mGroupCode = cboGroup.GetColumnValue("Code").ToString().Trim();

            if (txtFirstName.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_FirstNameIsInvalid.ToString());
                txtFirstName.Focus();
                return;
            }

            if (txtPhoneNo.Text.Trim() == "")
            {
                Program.Display_Error("Please enter phone number");
                txtPhoneNo.Focus();
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
                pPhoneNo = txtPhoneNo.Text;
                mBirthDate = mBirthDate.Date;
                mBirthDate = mBirthDate.AddHours(DateTime.Now.Hour);
                mBirthDate = mBirthDate.AddMinutes(DateTime.Now.Minute);

               

                string mMaritalStatusCode = "";
                if (cboMaritalStatus.ItemIndex != -1)
                {
                    mMaritalStatusCode = cboMaritalStatus.GetColumnValue("code").ToString();
                }


              
                //add 
                mPatientCheck = null;
                
                //check if patient exist
                if (mGenerateCode == 0)
                {
                    mPatientCheck = pMdtRegistrations.Get_Patient(txtPatientId.Text);
                }
                else
                {
                  mPatient = pMdtRegistrations.Add_Patient(mGenerateCode, txtPatientId.Text, txtSurname.Text, txtFirstName.Text,
                  txtOtherNames.Text, mGender, mBirthDate, txtChronic.Text, txtSevere.Text, txtMajor.Text,
                  mTransDate, mMaritalStatusCode, "", mDtExtraDetails, mTransDate, Program.gMachineName, Program.gMachineUser, Program.gCurrentUser.Code);

                }
                if (mPatientCheck != null)
                {
                  
                    mPatient = mPatientCheck;
                }
               
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
                pMdtSMS.Add_SMSPatient(txtPatientId.Text, txtFirstName.Text + " " + txtSurname.Text, pPhoneNo, mGroupCode);
                Program.Update_CommonNames(txtSurname.Text, txtFirstName.Text, txtOtherNames.Text);

                this.Data_Clear();
                txtPatientId.Text = "<<---new--->>";
                ((frmSMSMobilePatientsList)Program.gMdiForm.ActiveMdiChild).Close();

                frmSMSMobilePatientsList mTpt = new frmSMSMobilePatientsList();
                mTpt.MdiParent = Program.gMdiForm;
                mTpt.Show();
                MessageBox.Show("Mobile patient successfully added to the list", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);

               
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
            pSearchingPatient = true;

            frmSearchPatient mSearchPatient = new frmSearchPatient(txtPatientId);
            mSearchPatient.ShowDialog();

            if (mSearchPatient.SearchingDone == true)
            {
                //cboBillType.Focus();
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

        #region frmSMSPatientRegistration_KeyDown
        void frmSMSPatientRegistration_KeyDown(object sender, KeyEventArgs e)
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
                        //this.Book();
                    }
                    break;
            }
        }
        #endregion

        #region btnImport_Click
        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog mOfDlg = new OpenFileDialog();
                mOfDlg.Filter = "Excel files |*.xls";
                mOfDlg.ShowDialog();

                string mFileName = mOfDlg.FileName.ToString();
                string Cn ="";

                Cn = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + mFileName + ";" + "Extended Properties=Excel 8.0;";

               OleDbConnection mConn = new OleDbConnection(Cn);
               mConn.ConnectionString = Cn;
               mConn.Open();
               OleDbDataAdapter mAdp = new OleDbDataAdapter();

               OleDbCommand mCommand = new OleDbCommand();
               DataTable  mDs = new DataTable();
               mCommand.Connection =mConn;
               mCommand.CommandText = "select * from [Sheet1$]";

               mAdp.SelectCommand = mCommand;
               mAdp.Fill(mDs);
               grdMain.DataSource = mDs;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}