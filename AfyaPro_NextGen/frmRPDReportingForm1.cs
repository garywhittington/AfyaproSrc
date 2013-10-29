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
using DevExpress.XtraLayout;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using System.IO;

namespace AfyaPro_NextGen
{
    public partial class frmRPDReportingForm1 : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsReports pMdtReports;
        private AfyaPro_MT.clsReporter pMdtReporter;
        private AfyaPro_MT.clsPatientExtraFields pMdtPatientExtraFields;

        private Type pType;
        private string pClassName = "";

        private int pFormWidth = 0;
        private int pFormHeight = 0;
        private bool pSaveReportingForm = false;

        private string pReportCode = "";
        private bool pSearching = false;
        private string pCurrSearchStr = "";
        private string pPrevSearchStr = "";

        private bool pCheckedCheckBox = false;
        private bool pCheckedGrid = false;

        private ComboBoxItemCollection pComboBoxItems1;
        private ComboBoxItemCollection pComboBoxItems2;
        private ComboBoxItemCollection pComboBoxItems3;
        private ComboBoxItemCollection pComboBoxItems4;

        #region controls definition

        private ComboBoxEdit comboBoxEdit1 = new ComboBoxEdit();
        private ComboBoxEdit comboBoxEdit2 = new ComboBoxEdit();
        private ComboBoxEdit comboBoxEdit3 = new ComboBoxEdit();
        private ComboBoxEdit comboBoxEdit4 = new ComboBoxEdit();

        private TextEdit textEdit1 = new TextEdit();
        private TextEdit textEdit2 = new TextEdit();
        private TextEdit textEdit3 = new TextEdit();
        private TextEdit textEdit4 = new TextEdit();
        private TextEdit textEditSearch = new TextEdit();

        private LookUpEdit lookUpEdit1 = new LookUpEdit();
        private LookUpEdit lookUpEdit2 = new LookUpEdit();
        private LookUpEdit lookUpEdit3 = new LookUpEdit();
        private LookUpEdit lookUpEdit4 = new LookUpEdit();

        private DateEdit dateEdit1 = new DateEdit();
        private DateEdit dateEdit2 = new DateEdit();
        private DateEdit dateEdit3 = new DateEdit();
        private DateEdit dateEdit4 = new DateEdit();
        private DateEdit dateEdit5 = new DateEdit();

        private CheckEdit checkEdit1 = new CheckEdit();
        private CheckEdit checkEdit2 = new CheckEdit();
        private CheckEdit checkEdit3 = new CheckEdit();
        private CheckEdit checkEdit4 = new CheckEdit();

        private SimpleButton simpleButton1 = new SimpleButton();
        private SimpleButton simpleButton2 = new SimpleButton();
        private SimpleButton simpleButton3 = new SimpleButton();
        private SimpleButton simpleButton4 = new SimpleButton();
        private SimpleButton simpleButtonSearch = new SimpleButton();

        private RadioGroup radioGroup1 = new RadioGroup();

        private GridControl gridControl1 = null;
        private GridControl gridControl2 = null;

        #endregion

        #endregion

        #region frmRPDReportingForm1
        public frmRPDReportingForm1(string mReportCode, bool mSaveReportingForm)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmRPDReportingForm1";

            try
            {
                pReportCode = mReportCode;
                pSaveReportingForm = mSaveReportingForm;

                pMdtReports = (AfyaPro_MT.clsReports)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReports),
                    Program.gMiddleTier + "clsReports");

                pMdtReporter = (AfyaPro_MT.clsReporter)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReporter),
                    Program.gMiddleTier + "clsReporter");

                pMdtPatientExtraFields = (AfyaPro_MT.clsPatientExtraFields)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPatientExtraFields),
                    Program.gMiddleTier + "clsPatientExtraFields");

                this.Initialize_Controls();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmRPDReportingForm1_Load
        private void frmRPDReportingForm1_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmRCHClients_Load";

            try
            {
                

                byte[] mByte = pMdtReporter.Load_ReportTemplate("frmReporter" + pReportCode, true);
                if (mByte != null)
                {
                    MemoryStream mMemoryStream = new MemoryStream(mByte);
                    layoutControl1.RestoreLayoutFromStream(mMemoryStream);
                    mMemoryStream.Close();
                }

                Program.Restore_FormSize("frmReporter" + pReportCode, this, true);

                this.pFormWidth = this.Width;
                this.pFormHeight = this.Height;

                Program.Center_Screen(this);

                this.Apply_Security();

                if (gridControl1 != null)
                {
                    Program.Restore_GridLayout(gridControl1, gridControl1.Name);
                }

                if (gridControl2 != null)
                {

                    Program.Restore_GridLayout(gridControl2, gridControl2.Name);
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Apply_Security
        private void Apply_Security()
        {
            DataView mDvUserReports = new DataView();
            mDvUserReports.Table = Program.gDtUserReports;
            mDvUserReports.Sort = "reportcode";

            int mRowIndex = mDvUserReports.Find(pReportCode);

            bool mReportView = false;
            bool mReportDesign = false;
            bool mFormCustomization = false;

            if (mRowIndex >= 0)
            {
                mReportView = Convert.ToBoolean(mDvUserReports[mRowIndex]["reportview"]);
                mReportDesign = Convert.ToBoolean(mDvUserReports[mRowIndex]["reportdesign"]);
                mFormCustomization = Convert.ToBoolean(mDvUserReports[mRowIndex]["reportformcustomization"]);
            }

            //view button
            if (mReportView == true)
            {
                txbView.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                txbView.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            //design button
            if (mReportDesign == true)
            {
                txbDesign.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.AttendanceCountAge)
                {
                    layoutControl1.GetItemByControl(simpleButton1).Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
            }
            else
            {
                txbDesign.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.AttendanceCountAge)
                {
                    layoutControl1.GetItemByControl(simpleButton1).Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
            }

            //form layout
            layoutControl1.AllowCustomizationMenu = mFormCustomization;
        }
        #endregion

        #region frmRPDReportingForm1_FormClosing
        private void frmRPDReportingForm1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string mFunctionName = "frmRPDReportingForm1_FormClosing";

            try
            {
                //layout
                if (layoutControl1.IsModified == true || pSaveReportingForm == true)
                {
                    MemoryStream mMemoryStream = new MemoryStream();
                    layoutControl1.SaveLayoutToStream(mMemoryStream);

                    pMdtReporter.Save_ReportTemplate("frmReporter" + pReportCode, mMemoryStream.ToArray(), this.Height, this.Width, true);
                }

                if (gridControl1 != null)
                {
                    Program.Save_GridLayout(gridControl1, gridControl1.Name);
                }

                if (gridControl2 != null)
                {
                    Program.Save_GridLayout(gridControl2, gridControl2.Name);
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Initialize_Controls
        private void Initialize_Controls()
        {
            LayoutControlItem mLayoutControlItem;

            #region DebtorStatement

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.DebtorStatement)
            {
                //radDebtorType
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "radDebtorType";
                mLayoutControlItem.CustomizationFormText = "";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                radioGroup1.Name = "radDebtorType";
                radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
                new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Individual"),
                new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Group")});
                radioGroup1.SelectedIndexChanged += new EventHandler(radioGroup1_SelectedIndexChanged);
                radioGroup1.SelectedIndex = 1;
                mLayoutControlItem.Control = radioGroup1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                //txtAccountCode
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAccountCode";
                mLayoutControlItem.CustomizationFormText = "Account Code";
                mLayoutControlItem.Text = "Account Code";
                textEditSearch.Name = "txtAccountCode";
                textEditSearch.KeyDown += new KeyEventHandler(textEditSearch_KeyDown);
                textEditSearch.Leave += new EventHandler(textEditSearch_Leave);
                textEditSearch.EditValueChanged += new EventHandler(textEditSearch_EditValueChanged);
                mLayoutControlItem.Control = textEditSearch;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                //cmdSearchAccount
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbSearchAccount";
                mLayoutControlItem.CustomizationFormText = "Search";
                mLayoutControlItem.Text = "Search";
                mLayoutControlItem.TextVisible = false;
                simpleButtonSearch.Text = "Search";
                simpleButtonSearch.Name = "cmdSearchAccount";
                simpleButtonSearch.Click += new EventHandler(simpleButtonSearch_Click);
                mLayoutControlItem.Control = simpleButtonSearch;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                //txtAccountDescription
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAccountDescription";
                mLayoutControlItem.CustomizationFormText = "Account Description";
                mLayoutControlItem.Text = "Account Description";
                textEdit1.Name = "txtAccountDescription";
                mLayoutControlItem.Control = textEdit1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                //txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                //txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                //chkAllTransactions
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAllTransactions";
                mLayoutControlItem.CustomizationFormText = "All Transactions";
                mLayoutControlItem.Text = "All Transactions";
                mLayoutControlItem.TextVisible = false;
                checkEdit1.Name = "chkAllTransactions";
                checkEdit1.Text = "All Transactions";
                checkEdit1.Checked = true;
                mLayoutControlItem.Control = checkEdit1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
            }

            #endregion

            #region registration

            #region AttendanceList

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.AttendanceList)
            {
                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region cboAgeCondition
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeCondition";
                mLayoutControlItem.CustomizationFormText = "Age Condition";
                mLayoutControlItem.Text = "AGE";
                comboBoxEdit1.Name = "cboAgeCondition";
                comboBoxEdit1.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit1.Properties.Items;
                pComboBoxItems1.AddRange(new string[] {"","=",">",">=","<","<=","<>"});
                #endregion

                #region txtAgeLimit
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeLimit";
                mLayoutControlItem.CustomizationFormText = "Age Limit";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                textEdit1.Name = "txtAgeLimit";
                mLayoutControlItem.Control = textEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region cboGender
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbGender";
                mLayoutControlItem.CustomizationFormText = "Gender";
                mLayoutControlItem.Text = "GENDER";
                comboBoxEdit2.Name = "cboGender";
                comboBoxEdit2.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems2 = comboBoxEdit2.Properties.Items;
                pComboBoxItems2.AddRange(new string[] { "", "M", "F"});
                #endregion

                #region cboWeight
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbWeight";
                mLayoutControlItem.CustomizationFormText = "Weight";
                mLayoutControlItem.Text = "WEIGHT";
                comboBoxEdit3.Name = "cboWeight";
                comboBoxEdit3.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit3;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems3 = comboBoxEdit3.Properties.Items;
                pComboBoxItems3.AddRange(new string[] { "", "=", ">", ">=", "<", "<=", "<>" });
                #endregion

                #region txtWeightLimit
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbWeightLimit";
                mLayoutControlItem.CustomizationFormText = "Weight Limit";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                textEdit2.Name = "txtWeightLimit";
                mLayoutControlItem.Control = textEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region cboTemperature
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbTemperature";
                mLayoutControlItem.CustomizationFormText = "Temperature";
                mLayoutControlItem.Text = "Temp (C)";
                comboBoxEdit4.Name = "cboTemperature";
                comboBoxEdit4.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit4;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems4 = comboBoxEdit4.Properties.Items;
                pComboBoxItems4.AddRange(new string[] { "", "=", ">", ">=", "<", "<=", "<>" });
                #endregion

                #region txtTemperatureLimit
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbTemperatureLimit";
                mLayoutControlItem.CustomizationFormText = "Temperature Limit";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                textEdit3.Name = "txtTemperatureLimit";
                mLayoutControlItem.Control = textEdit3;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region treatmentpoint
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbTreatmentPoint";
                mLayoutControlItem.CustomizationFormText = "Treatment Point";
                mLayoutControlItem.Text = "Treatment Point";
                lookUpEdit1.Name = "cboTreatmentPoint";
                lookUpEdit1.Properties.NullText = "";
                mLayoutControlItem.Control = lookUpEdit1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                DataTable mDtLookupData = pMdtReporter.View_LookupData("facilitytreatmentpoints",
                    "code,description", "", "description", Program.gLanguageName, "grdGENTreatmentPoints");
                DataTable mDtThisLookup = new DataTable("lookupdata");
                mDtThisLookup.Columns.Add("code", typeof(System.String));
                mDtThisLookup.Columns.Add("description", typeof(System.String));

                DataRow mNewRow = mDtThisLookup.NewRow();
                mNewRow["code"] = "";
                mNewRow["description"] = "";
                mDtThisLookup.Rows.Add(mNewRow);
                mDtThisLookup.AcceptChanges();
                foreach (DataRow mDataRow in mDtLookupData.Rows)
                {
                    mNewRow = mDtThisLookup.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    mDtThisLookup.Rows.Add(mNewRow);
                    mDtThisLookup.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in mDtThisLookup.Columns)
                {
                    mDataColumn.Caption = mDtLookupData.Columns[mDataColumn.ColumnName].Caption;
                }

                lookUpEdit1.Properties.DataSource = mDtThisLookup;
                lookUpEdit1.Properties.DisplayMember = "description";
                lookUpEdit1.Properties.ValueMember = "code";
                #endregion

                this.Load_ExtraPatientControls();
                this.Fill_LookupData();
            }

            #endregion

            #region AttendanceCountAge

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.AttendanceCountAge)
            {
                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region cboGender
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbGender";
                mLayoutControlItem.CustomizationFormText = "Gender";
                mLayoutControlItem.Text = "GENDER";
                comboBoxEdit2.Name = "cboGender";
                comboBoxEdit2.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems2 = comboBoxEdit2.Properties.Items;
                pComboBoxItems2.AddRange(new string[] { "", "M", "F" });
                #endregion

                #region cboWeight
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbWeight";
                mLayoutControlItem.CustomizationFormText = "Weight";
                mLayoutControlItem.Text = "WEIGHT";
                comboBoxEdit3.Name = "cboWeight";
                comboBoxEdit3.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit3;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems3 = comboBoxEdit3.Properties.Items;
                pComboBoxItems3.AddRange(new string[] { "", "=", ">", ">=", "<", "<=", "<>" });
                #endregion

                #region txtWeightLimit
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbWeightLimit";
                mLayoutControlItem.CustomizationFormText = "Weight Limit";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                textEdit2.Name = "txtWeightLimit";
                mLayoutControlItem.Control = textEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region cboTemperature
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbTemperature";
                mLayoutControlItem.CustomizationFormText = "Temperature";
                mLayoutControlItem.Text = "Temp (C)";
                comboBoxEdit4.Name = "cboTemperature";
                comboBoxEdit4.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit4;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems4 = comboBoxEdit4.Properties.Items;
                pComboBoxItems4.AddRange(new string[] { "", "=", ">", ">=", "<", "<=", "<>" });
                #endregion

                #region txtTemperatureLimit
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbTemperatureLimit";
                mLayoutControlItem.CustomizationFormText = "Temperature Limit";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                textEdit3.Name = "txtTemperatureLimit";
                mLayoutControlItem.Control = textEdit3;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region treatmentpoint
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbTreatmentPoint";
                mLayoutControlItem.CustomizationFormText = "Treatment Point";
                mLayoutControlItem.Text = "Treatment Point";
                lookUpEdit1.Name = "cboTreatmentPoint";
                lookUpEdit1.Properties.NullText = "";
                mLayoutControlItem.Control = lookUpEdit1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                DataTable mDtLookupData = pMdtReporter.View_LookupData("facilitytreatmentpoints",
                    "code,description", "", "description", Program.gLanguageName, "grdGENTreatmentPoints");
                DataTable mDtThisLookup = new DataTable("lookupdata");
                mDtThisLookup.Columns.Add("code", typeof(System.String));
                mDtThisLookup.Columns.Add("description", typeof(System.String));

                DataRow mNewRow = mDtThisLookup.NewRow();
                mNewRow["code"] = "";
                mNewRow["description"] = "";
                mDtThisLookup.Rows.Add(mNewRow);
                mDtThisLookup.AcceptChanges();
                foreach (DataRow mDataRow in mDtLookupData.Rows)
                {
                    mNewRow = mDtThisLookup.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    mDtThisLookup.Rows.Add(mNewRow);
                    mDtThisLookup.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in mDtThisLookup.Columns)
                {
                    mDataColumn.Caption = mDtLookupData.Columns[mDataColumn.ColumnName].Caption;
                }

                lookUpEdit1.Properties.DataSource = mDtThisLookup;
                lookUpEdit1.Properties.DisplayMember = "description";
                lookUpEdit1.Properties.ValueMember = "code";
                #endregion

                this.Load_ExtraPatientControls();
                this.Fill_LookupData();

                #region Age Groups

                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeGroups";
                mLayoutControlItem.CustomizationFormText = "Age Groups";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                simpleButton1.Text = "Age Groups";
                simpleButton1.Name = "cmdAgeGroups";
                simpleButton1.Click += new EventHandler(simpleButton_Click);
                mLayoutControlItem.Control = simpleButton1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                #endregion
            }

            #endregion

            #region AttendanceCountTreatmentPoints

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.AttendanceCountTreatmentPoints)
            {
                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region cboAgeCondition
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeCondition";
                mLayoutControlItem.CustomizationFormText = "Age Condition";
                mLayoutControlItem.Text = "AGE";
                comboBoxEdit1.Name = "cboAgeCondition";
                comboBoxEdit1.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit1.Properties.Items;
                pComboBoxItems1.AddRange(new string[] { "", "=", ">", ">=", "<", "<=", "<>" });
                #endregion

                #region txtAgeLimit
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeLimit";
                mLayoutControlItem.CustomizationFormText = "Age Limit";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                textEdit1.Name = "txtAgeLimit";
                mLayoutControlItem.Control = textEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region cboGender
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbGender";
                mLayoutControlItem.CustomizationFormText = "Gender";
                mLayoutControlItem.Text = "GENDER";
                comboBoxEdit2.Name = "cboGender";
                comboBoxEdit2.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems2 = comboBoxEdit2.Properties.Items;
                pComboBoxItems2.AddRange(new string[] { "", "M", "F" });
                #endregion

                #region cboWeight
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbWeight";
                mLayoutControlItem.CustomizationFormText = "Weight";
                mLayoutControlItem.Text = "WEIGHT";
                comboBoxEdit3.Name = "cboWeight";
                comboBoxEdit3.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit3;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems3 = comboBoxEdit3.Properties.Items;
                pComboBoxItems3.AddRange(new string[] { "", "=", ">", ">=", "<", "<=", "<>" });
                #endregion

                #region txtWeightLimit
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbWeightLimit";
                mLayoutControlItem.CustomizationFormText = "Weight Limit";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                textEdit2.Name = "txtWeightLimit";
                mLayoutControlItem.Control = textEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region cboTemperature
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbTemperature";
                mLayoutControlItem.CustomizationFormText = "Temperature";
                mLayoutControlItem.Text = "Temp (C)";
                comboBoxEdit4.Name = "cboTemperature";
                comboBoxEdit4.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit4;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems4 = comboBoxEdit4.Properties.Items;
                pComboBoxItems4.AddRange(new string[] { "", "=", ">", ">=", "<", "<=", "<>" });
                #endregion

                #region txtTemperatureLimit
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbTemperatureLimit";
                mLayoutControlItem.CustomizationFormText = "Temperature Limit";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                textEdit3.Name = "txtTemperatureLimit";
                mLayoutControlItem.Control = textEdit3;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                this.Load_ExtraPatientControls();
                this.Fill_LookupData();
            }

            #endregion

            #region AttendanceCountCustomerGroups

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.AttendanceCountCustomerGroups)
            {
                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region cboAgeCondition
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeCondition";
                mLayoutControlItem.CustomizationFormText = "Age Condition";
                mLayoutControlItem.Text = "AGE";
                comboBoxEdit1.Name = "cboAgeCondition";
                comboBoxEdit1.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit1.Properties.Items;
                pComboBoxItems1.AddRange(new string[] { "", "=", ">", ">=", "<", "<=", "<>" });
                #endregion

                #region txtAgeLimit
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeLimit";
                mLayoutControlItem.CustomizationFormText = "Age Limit";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                textEdit1.Name = "txtAgeLimit";
                mLayoutControlItem.Control = textEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region cboGender
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbGender";
                mLayoutControlItem.CustomizationFormText = "Gender";
                mLayoutControlItem.Text = "GENDER";
                comboBoxEdit2.Name = "cboGender";
                comboBoxEdit2.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems2 = comboBoxEdit2.Properties.Items;
                pComboBoxItems2.AddRange(new string[] { "", "M", "F" });
                #endregion

                #region cboWeight
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbWeight";
                mLayoutControlItem.CustomizationFormText = "Weight";
                mLayoutControlItem.Text = "WEIGHT";
                comboBoxEdit3.Name = "cboWeight";
                comboBoxEdit3.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit3;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems3 = comboBoxEdit3.Properties.Items;
                pComboBoxItems3.AddRange(new string[] { "", "=", ">", ">=", "<", "<=", "<>" });
                #endregion

                #region txtWeightLimit
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbWeightLimit";
                mLayoutControlItem.CustomizationFormText = "Weight Limit";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                textEdit2.Name = "txtWeightLimit";
                mLayoutControlItem.Control = textEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region cboTemperature
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbTemperature";
                mLayoutControlItem.CustomizationFormText = "Temperature";
                mLayoutControlItem.Text = "Temp (C)";
                comboBoxEdit4.Name = "cboTemperature";
                comboBoxEdit4.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit4;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems4 = comboBoxEdit4.Properties.Items;
                pComboBoxItems4.AddRange(new string[] { "", "=", ">", ">=", "<", "<=", "<>" });
                #endregion

                #region txtTemperatureLimit
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbTemperatureLimit";
                mLayoutControlItem.CustomizationFormText = "Temperature Limit";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                textEdit3.Name = "txtTemperatureLimit";
                mLayoutControlItem.Control = textEdit3;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region treatmentpoint
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbTreatmentPoint";
                mLayoutControlItem.CustomizationFormText = "Treatment Point";
                mLayoutControlItem.Text = "Treatment Point";
                lookUpEdit1.Name = "cboTreatmentPoint";
                lookUpEdit1.Properties.NullText = "";
                mLayoutControlItem.Control = lookUpEdit1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                DataTable mDtLookupData = pMdtReporter.View_LookupData("facilitytreatmentpoints",
                    "code,description", "", "description", Program.gLanguageName, "grdGENTreatmentPoints");
                DataTable mDtThisLookup = new DataTable("lookupdata");
                mDtThisLookup.Columns.Add("code", typeof(System.String));
                mDtThisLookup.Columns.Add("description", typeof(System.String));

                DataRow mNewRow = mDtThisLookup.NewRow();
                mNewRow["code"] = "";
                mNewRow["description"] = "";
                mDtThisLookup.Rows.Add(mNewRow);
                mDtThisLookup.AcceptChanges();
                foreach (DataRow mDataRow in mDtLookupData.Rows)
                {
                    mNewRow = mDtThisLookup.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    mDtThisLookup.Rows.Add(mNewRow);
                    mDtThisLookup.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in mDtThisLookup.Columns)
                {
                    mDataColumn.Caption = mDtLookupData.Columns[mDataColumn.ColumnName].Caption;
                }

                lookUpEdit1.Properties.DataSource = mDtThisLookup;
                lookUpEdit1.Properties.DisplayMember = "description";
                lookUpEdit1.Properties.ValueMember = "code";
                #endregion

                this.Load_ExtraPatientControls();
                this.Fill_LookupData();
            }

            #endregion

            #region IPDDailyCensusSummary

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.IPDDailyCensusSummary)
            {
                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion
            }

            #endregion

            #region IPDDailyCensusDetailed

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.IPDDailyCensusDetailed)
            {
                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region ward
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbWard";
                mLayoutControlItem.CustomizationFormText = "Ward";
                mLayoutControlItem.Text = "Ward";
                lookUpEdit1.Name = "cboWard";
                lookUpEdit1.Properties.NullText = "";
                mLayoutControlItem.Control = lookUpEdit1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                DataTable mDtLookupData = pMdtReporter.View_LookupData("facilitywards",
                    "code,description", "", "description", Program.gLanguageName, "grdIPDWards");
                DataTable mDtThisLookup = new DataTable("lookupdata");
                mDtThisLookup.Columns.Add("code", typeof(System.String));
                mDtThisLookup.Columns.Add("description", typeof(System.String));

                DataRow mNewRow = mDtThisLookup.NewRow();
                mNewRow["code"] = "";
                mNewRow["description"] = "";
                mDtThisLookup.Rows.Add(mNewRow);
                mDtThisLookup.AcceptChanges();
                foreach (DataRow mDataRow in mDtLookupData.Rows)
                {
                    mNewRow = mDtThisLookup.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    mDtThisLookup.Rows.Add(mNewRow);
                    mDtThisLookup.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in mDtThisLookup.Columns)
                {
                    mDataColumn.Caption = mDtLookupData.Columns[mDataColumn.ColumnName].Caption;
                }

                lookUpEdit1.Properties.DataSource = mDtThisLookup;
                lookUpEdit1.Properties.DisplayMember = "description";
                lookUpEdit1.Properties.ValueMember = "code";
                #endregion
            }

            #endregion

            #endregion

            #region diagnoses and treatments

            #region DXTDiagnosesSummary

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.DXTDiagnosesSummary)
            {
                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region cboAgeCondition
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeCondition";
                mLayoutControlItem.CustomizationFormText = "Age Condition";
                mLayoutControlItem.Text = "AGE";
                comboBoxEdit1.Name = "cboAgeCondition";
                comboBoxEdit1.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit1.Properties.Items;
                pComboBoxItems1.AddRange(new string[] { "", "=", ">", ">=", "<", "<=", "<>" });
                #endregion

                #region txtAgeLimit
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeLimit";
                mLayoutControlItem.CustomizationFormText = "Age Limit";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                textEdit1.Name = "txtAgeLimit";
                mLayoutControlItem.Control = textEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region chkPrimaryOnly

                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbPrimaryOnly";
                mLayoutControlItem.CustomizationFormText = "Report Primary Diagnoses only";
                mLayoutControlItem.Text = "Report Primary Diagnoses only";
                mLayoutControlItem.TextVisible = true;
                checkEdit1.Name = "chkPrimaryOnly";
                checkEdit1.Text = "Report Primary Diagnoses only";
                checkEdit1.Checked = false;
                mLayoutControlItem.Control = checkEdit1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                #endregion

                #region cboDepartment
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDepartment";
                mLayoutControlItem.CustomizationFormText = "Department";
                mLayoutControlItem.Text = "Department";
                comboBoxEdit2.Name = "cboDepartment";
                comboBoxEdit2.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems2 = comboBoxEdit2.Properties.Items;
                pComboBoxItems2.AddRange(new string[] { "Both Outpatient and Inpatient", "Outpatient", "Inpatient" });
                #endregion

                this.Load_ExtraPatientControls();
                this.Fill_LookupData();

                comboBoxEdit2.SelectedIndex = 0;
            }

            #endregion

            #region DXTDiagnosesListing

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.DXTDiagnosesListing)
            {
                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region cboAgeCondition
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeCondition";
                mLayoutControlItem.CustomizationFormText = "Age Condition";
                mLayoutControlItem.Text = "AGE";
                comboBoxEdit1.Name = "cboAgeCondition";
                comboBoxEdit1.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit1.Properties.Items;
                pComboBoxItems1.AddRange(new string[] { "", "=", ">", ">=", "<", "<=", "<>" });
                #endregion

                #region txtAgeLimit
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeLimit";
                mLayoutControlItem.CustomizationFormText = "Age Limit";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                textEdit1.Name = "txtAgeLimit";
                mLayoutControlItem.Control = textEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                this.Load_ExtraPatientControls();
                this.Fill_LookupData();
            }

            #endregion

            #region DXTPatientHistory

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.DXTPatientHistory)
            {
                //txtPatientId
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbPatientId";
                mLayoutControlItem.CustomizationFormText = "Patient #";
                mLayoutControlItem.Text = "Patient #";
                textEditSearch.Name = "txtPatientId";
                textEditSearch.KeyDown += new KeyEventHandler(textEditSearch_KeyDown);
                textEditSearch.Leave += new EventHandler(textEditSearch_Leave);
                textEditSearch.EditValueChanged += new EventHandler(textEditSearch_EditValueChanged);
                mLayoutControlItem.Control = textEditSearch;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                //cmdSearchPatient
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbSearchPatient";
                mLayoutControlItem.CustomizationFormText = "Search Patient";
                mLayoutControlItem.Text = "Search Patient";
                mLayoutControlItem.TextVisible = false;
                simpleButtonSearch.Text = "Search Patient";
                simpleButtonSearch.Name = "cmdSearchPatient";
                simpleButtonSearch.Click += new EventHandler(simpleButtonSearch_Click);
                mLayoutControlItem.Control = simpleButtonSearch;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                //txtPatientName
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbPatientName";
                mLayoutControlItem.CustomizationFormText = "Name";
                mLayoutControlItem.Text = "Name";
                textEdit1.Name = "txtPatientName";
                textEdit1.Properties.ReadOnly = true;
                mLayoutControlItem.Control = textEdit1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                //txtYears
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbYears";
                mLayoutControlItem.CustomizationFormText = "Age   Years";
                mLayoutControlItem.Text = "Age   Years";
                textEdit2.Name = "txtYears";
                textEdit2.Properties.ReadOnly = true;
                mLayoutControlItem.Control = textEdit2;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                //txtMonths
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbMonths";
                mLayoutControlItem.CustomizationFormText = "Months";
                mLayoutControlItem.Text = "Months";
                textEdit3.Name = "txtMonths";
                textEdit3.Properties.ReadOnly = true;
                mLayoutControlItem.Control = textEdit3;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                //txtGender
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbGender";
                mLayoutControlItem.CustomizationFormText = "Gender";
                mLayoutControlItem.Text = "Gender";
                textEdit4.Name = "txtGender";
                textEdit4.Properties.ReadOnly = true;
                mLayoutControlItem.Control = textEdit4;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
            }

            #endregion

            #endregion

            #region billing

            #region cashbox

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.CashBox)
            {
                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region User Id
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbUserId";
                mLayoutControlItem.CustomizationFormText = "User Id";
                mLayoutControlItem.Text = "User Id";
                lookUpEdit1.Name = "cboUserId";
                lookUpEdit1.Properties.NullText = "";
                mLayoutControlItem.Control = lookUpEdit1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                DataTable mDtLookupData = pMdtReporter.View_LookupData("sys_users",
                    "code,description", "", "code", Program.gLanguageName, "grdSECUsers");
                DataTable mDtThisLookup = new DataTable("lookupdata");
                mDtThisLookup.Columns.Add("code", typeof(System.String));
                mDtThisLookup.Columns.Add("description", typeof(System.String));

                DataRow mNewRow = mDtThisLookup.NewRow();
                mNewRow["code"] = "";
                mNewRow["description"] = "";
                mDtThisLookup.Rows.Add(mNewRow);
                mDtThisLookup.AcceptChanges();
                foreach (DataRow mDataRow in mDtLookupData.Rows)
                {
                    mNewRow = mDtThisLookup.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    mDtThisLookup.Rows.Add(mNewRow);
                    mDtThisLookup.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in mDtThisLookup.Columns)
                {
                    mDataColumn.Caption = mDtLookupData.Columns[mDataColumn.ColumnName].Caption;
                }
                lookUpEdit1.Properties.DataSource = mDtThisLookup;
                lookUpEdit1.Properties.DisplayMember = "description";
                lookUpEdit1.Properties.ValueMember = "code";
                lookUpEdit1.EditValue = lookUpEdit1.Properties.GetDataSourceRowByKeyValue("");
                #endregion
            }

            #endregion

            #region my cashbox

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.MyCashBox)
            {
                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

            }

            #endregion

            #region DebtorsList

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.BILDebtorsList)
            {
                #region cboBalanceCondition
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbBalanceCondition";
                mLayoutControlItem.CustomizationFormText = "Balance Condition";
                mLayoutControlItem.Text = "Balance";
                comboBoxEdit1.Name = "cboBalanceCondition";
                comboBoxEdit1.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit1.Properties.Items;
                pComboBoxItems1.AddRange(new string[] { "", "=", ">", ">=", "<", "<=", "<>" });
                #endregion

                #region txtBalanceLimit
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbBalanceLimit";
                mLayoutControlItem.CustomizationFormText = "Balance Limit";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                textEdit1.Name = "txtBalanceLimit";
                mLayoutControlItem.Control = textEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region cboAccountType
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAccountType";
                mLayoutControlItem.CustomizationFormText = "Account Type";
                mLayoutControlItem.Text = "Account Type";
                comboBoxEdit2.Name = "cboAccountType";
                comboBoxEdit2.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems2 = comboBoxEdit2.Properties.Items;
                pComboBoxItems2.AddRange(new string[] { "", "Groups", "Individuals" });
                #endregion

                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion
            }

            #endregion

            #region DebtorsList

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.BillDebtorsList1)
            {
                
              
                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion
            }

            #endregion

            #region dailysalessummary

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.BILDailySalesSummary)
            {
                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion
            }

            #endregion

            #region dailysalesdetails

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.BILDailySalesDetails)
            {
                #region billing groups
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbBillingGroup";
                mLayoutControlItem.CustomizationFormText = "Customer Group";
                mLayoutControlItem.Text = "Customer Group";
                lookUpEdit1.Name = "cboBillingGroup";
                lookUpEdit1.Properties.NullText = "";
                mLayoutControlItem.Control = lookUpEdit1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                DataTable mDtLookupData = pMdtReporter.View_LookupData("facilitycorporates",
                    "code,description", "", "description", Program.gLanguageName, "grdCUSCustomerGroups");
                DataTable mDtThisLookup = new DataTable("lookupdata");
                mDtThisLookup.Columns.Add("code", typeof(System.String));
                mDtThisLookup.Columns.Add("description", typeof(System.String));

                DataRow mNewRow = mDtThisLookup.NewRow();
                mNewRow["code"] = "";
                mNewRow["description"] = "";
                mDtThisLookup.Rows.Add(mNewRow);
                mDtThisLookup.AcceptChanges();
                mNewRow = mDtThisLookup.NewRow();
                mNewRow["code"] = "Individuals";
                mNewRow["description"] = "Individuals";
                mDtThisLookup.Rows.Add(mNewRow);
                mDtThisLookup.AcceptChanges();
                foreach (DataRow mDataRow in mDtLookupData.Rows)
                {
                    mNewRow = mDtThisLookup.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    mDtThisLookup.Rows.Add(mNewRow);
                    mDtThisLookup.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in mDtThisLookup.Columns)
                {
                    mDataColumn.Caption = mDtLookupData.Columns[mDataColumn.ColumnName].Caption;
                }

                lookUpEdit1.Properties.DataSource = mDtThisLookup;
                lookUpEdit1.Properties.DisplayMember = "description";
                lookUpEdit1.Properties.ValueMember = "code";
                lookUpEdit1.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEdit1.Properties.BestFit();
                #endregion

                #region item groups
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbItemGroup";
                mLayoutControlItem.CustomizationFormText = "Item Group";
                mLayoutControlItem.Text = "Item Group";
                lookUpEdit2.Name = "cboItemGroup";
                lookUpEdit2.Properties.NullText = "";
                mLayoutControlItem.Control = lookUpEdit2;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                DataTable mDtLookupData1 = pMdtReporter.View_LookupData("facilitybillinggroups",
                    "code,description", "", "description", Program.gLanguageName, "grdBLSItemGroups");
                DataTable mDtThisLookup1 = new DataTable("lookupdata");
                mDtThisLookup1.Columns.Add("code", typeof(System.String));
                mDtThisLookup1.Columns.Add("description", typeof(System.String));

                //none
                mNewRow = mDtThisLookup1.NewRow();
                mNewRow["code"] = "";
                mNewRow["description"] = "";
                mDtThisLookup1.Rows.Add(mNewRow);
                mDtThisLookup1.AcceptChanges();
                mNewRow = mDtThisLookup1.NewRow();
                //pharmacy
                mNewRow["code"] = "pharmacy";
                mNewRow["description"] = "Pharmacy";
                mDtThisLookup1.Rows.Add(mNewRow);
                mDtThisLookup1.AcceptChanges();
                foreach (DataRow mDataRow in mDtLookupData1.Rows)
                {
                    mNewRow = mDtThisLookup1.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    mDtThisLookup1.Rows.Add(mNewRow);
                    mDtThisLookup1.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in mDtThisLookup1.Columns)
                {
                    mDataColumn.Caption = mDtLookupData1.Columns[mDataColumn.ColumnName].Caption;
                }

                lookUpEdit2.Properties.DataSource = mDtThisLookup1;
                lookUpEdit2.Properties.DisplayMember = "description";
                lookUpEdit2.Properties.ValueMember = "code";
                lookUpEdit2.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEdit2.Properties.BestFit();
                #endregion

                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region ward
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbWard";
                mLayoutControlItem.CustomizationFormText = "Ward";
                mLayoutControlItem.Text = "Ward";
                lookUpEdit3.Name = "cboWard";
                lookUpEdit3.Properties.NullText = "";
                mLayoutControlItem.Control = lookUpEdit3;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                mDtLookupData = pMdtReporter.View_LookupData("facilitywards",
                    "code,description", "", "description", Program.gLanguageName, "grdIPDWards");
                mDtThisLookup = new DataTable("lookupdata");
                mDtThisLookup.Columns.Add("code", typeof(System.String));
                mDtThisLookup.Columns.Add("description", typeof(System.String));

                //all
                mNewRow = mDtThisLookup.NewRow();
                mNewRow["code"] = "";
                mNewRow["description"] = "All";
                mDtThisLookup.Rows.Add(mNewRow);
                mDtThisLookup.AcceptChanges();
                //OPD
                mNewRow = mDtThisLookup.NewRow();
                mNewRow["code"] = "OPD";
                mNewRow["description"] = "OPD";
                mDtThisLookup.Rows.Add(mNewRow);
                mDtThisLookup.AcceptChanges();
                foreach (DataRow mDataRow in mDtLookupData.Rows)
                {
                    mNewRow = mDtThisLookup.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    mDtThisLookup.Rows.Add(mNewRow);
                    mDtThisLookup.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in mDtThisLookup.Columns)
                {
                    mDataColumn.Caption = mDtLookupData.Columns[mDataColumn.ColumnName].Caption;
                }

                lookUpEdit3.Properties.DataSource = mDtThisLookup;
                lookUpEdit3.Properties.DisplayMember = "description";
                lookUpEdit3.Properties.ValueMember = "code";
                #endregion

                #region chkGroupByDept

                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbGroupByDept";
                mLayoutControlItem.CustomizationFormText = "Group by OPD and IPD";
                mLayoutControlItem.Text = "Group by OPD and IPD";
                mLayoutControlItem.TextVisible = false;
                checkEdit2.Name = "chkGroupByDept";
                checkEdit2.Text = "Group by OPD and IPD";
                checkEdit2.Checked = false;
                mLayoutControlItem.Control = checkEdit2;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                #endregion
            }

            #endregion

            #region BILGroupBillBreakDown

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.BILGroupBillBreakDown)
            {
                #region billing groups
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbBillingGroup";
                mLayoutControlItem.CustomizationFormText = "Customer Group";
                mLayoutControlItem.Text = "Customer Group";
                lookUpEdit1.Name = "cboBillingGroup";
                lookUpEdit1.Properties.NullText = "";
                mLayoutControlItem.Control = lookUpEdit1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                DataTable mDtLookupData = pMdtReporter.View_LookupData("facilitycorporates",
                    "code,description", "", "description", Program.gLanguageName, "grdCUSCustomerGroups");
                DataTable mDtThisLookup = new DataTable("lookupdata");
                mDtThisLookup.Columns.Add("code", typeof(System.String));
                mDtThisLookup.Columns.Add("description", typeof(System.String));

                DataRow mNewRow = mDtThisLookup.NewRow();
                mNewRow["code"] = "Individuals";
                mNewRow["description"] = "Individuals";
                mDtThisLookup.Rows.Add(mNewRow);
                mDtThisLookup.AcceptChanges();
                foreach (DataRow mDataRow in mDtLookupData.Rows)
                {
                    mNewRow = mDtThisLookup.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    mDtThisLookup.Rows.Add(mNewRow);
                    mDtThisLookup.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in mDtThisLookup.Columns)
                {
                    mDataColumn.Caption = mDtLookupData.Columns[mDataColumn.ColumnName].Caption;
                }

                lookUpEdit1.Properties.DataSource = mDtThisLookup;
                lookUpEdit1.Properties.DisplayMember = "description";
                lookUpEdit1.Properties.ValueMember = "code";
                lookUpEdit1.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEdit1.Properties.BestFit();
                #endregion

                #region cboDepartment
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDepartment";
                mLayoutControlItem.CustomizationFormText = "Department";
                mLayoutControlItem.Text = "Department";
                comboBoxEdit1.Name = "cboDepartment";
                comboBoxEdit1.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit1.Properties.Items;
                pComboBoxItems1.AddRange(new string[] { "Both Outpatient and Inpatient", "Outpatient", "Inpatient" });
                #endregion

                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = DateTime.Now;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region chkAllTransactions

                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAllTransactions";
                mLayoutControlItem.CustomizationFormText = "All Transactions";
                mLayoutControlItem.Text = "All Transactions";
                mLayoutControlItem.TextVisible = false;
                checkEdit1.Name = "chkAllTransactions";
                checkEdit1.Text = "All Transactions";
                checkEdit1.Checked = false;
                mLayoutControlItem.Control = checkEdit1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                mLayoutControlItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                #endregion

                #region chkOnlyUnpaid

                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbOnlyUnpaid";
                mLayoutControlItem.CustomizationFormText = "Get only unpaid amounts";
                mLayoutControlItem.Text = "Get only unpaid amounts";
                mLayoutControlItem.TextVisible = false;
                checkEdit2.Name = "chkOnlyUnpaid";
                checkEdit2.Text = "Get only unpaid amounts";
                checkEdit2.Checked = false;
                mLayoutControlItem.Control = checkEdit2;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                #endregion

                comboBoxEdit1.SelectedIndex = 0;
            }

            #endregion

            #region BILDebtorsListIndividual

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.BILDebtorsListIndividual)
            {
                #region cboBalanceCondition
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbBalanceCondition";
                mLayoutControlItem.CustomizationFormText = "Balance Condition";
                mLayoutControlItem.Text = "Balance";
                comboBoxEdit1.Name = "cboBalanceCondition";
                comboBoxEdit1.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit1.Properties.Items;
                pComboBoxItems1.AddRange(new string[] { "", "=", ">", ">=", "<", "<=", "<>" });
                #endregion

                #region txtBalanceLimit
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbBalanceLimit";
                mLayoutControlItem.CustomizationFormText = "Balance Limit";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                textEdit1.Name = "txtBalanceLimit";
                mLayoutControlItem.Control = textEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                this.Load_ExtraPatientControls();
                this.Fill_LookupData();
            }

            #endregion

            #region dailyincome

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.BILDailyIncome)
            {
                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion
            }

            #endregion

            #endregion

            #region inventory

            #region IVStockBalance

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.IVStockBalance)
            {
                #region txtDate
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDate";
                mLayoutControlItem.CustomizationFormText = "Date";
                mLayoutControlItem.Text = "Date";
                dateEdit1.Name = "txtDate";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                if (Program.IsNullDate(Program.gMdiForm.txtDate.EditValue) == false)
                {
                    dateEdit1.EditValue = Program.gMdiForm.txtDate.EditValue;
                }
                else
                {
                    dateEdit1.EditValue = DateTime.Now;
                }
                #endregion

                #region store
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbStore";
                mLayoutControlItem.CustomizationFormText = "Store";
                mLayoutControlItem.Text = "Store";
                lookUpEdit1.Name = "cboStore";
                lookUpEdit1.Properties.NullText = "";
                mLayoutControlItem.Control = lookUpEdit1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                DataTable mDtLookupData = pMdtReporter.View_LookupData("som_stores",
                    "code,description", "", "description", Program.gLanguageName, "grdIVSStores");
                DataTable mDtThisLookup = new DataTable("lookupdata");
                mDtThisLookup.Columns.Add("code", typeof(System.String));
                mDtThisLookup.Columns.Add("description", typeof(System.String));

                DataView mDvUserStores = new DataView();
                mDvUserStores.Table = Program.gDtUserStores;
                mDvUserStores.Sort = "storecode";

                DataRow mNewRow = mDtThisLookup.NewRow();
                if (mDtLookupData.Rows.Count > 0)
                {
                    mNewRow = mDtThisLookup.NewRow();
                    mNewRow["code"] = "";
                    mNewRow["description"] = "All Stores";
                    mDtThisLookup.Rows.Add(mNewRow);
                    mDtThisLookup.AcceptChanges();
                }

                foreach (DataRow mDataRow in mDtLookupData.Rows)
                {
                    if (mDvUserStores.Find(mDataRow["code"].ToString().Trim()) >= 0)
                    {
                        mNewRow = mDtThisLookup.NewRow();
                        mNewRow["code"] = mDataRow["code"].ToString();
                        mNewRow["description"] = mDataRow["description"].ToString();
                        mDtThisLookup.Rows.Add(mNewRow);
                        mDtThisLookup.AcceptChanges();
                    }
                }

                foreach (DataColumn mDataColumn in mDtThisLookup.Columns)
                {
                    mDataColumn.Caption = mDtLookupData.Columns[mDataColumn.ColumnName].Caption;
                }

                lookUpEdit1.Properties.DataSource = mDtThisLookup;
                lookUpEdit1.Properties.DisplayMember = "description";
                lookUpEdit1.Properties.ValueMember = "code";

                lookUpEdit1.ItemIndex = Program.Get_LookupItemIndex(lookUpEdit1, "code", Program.gCurrentUser.StoreCode);
                #endregion

                #region chkallclasses
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAllCategories";
                mLayoutControlItem.CustomizationFormText = "All Categories";
                mLayoutControlItem.Text = "All Categories";
                mLayoutControlItem.TextVisible = false;
                checkEdit1.Name = "chkAllCategories";
                checkEdit1.Text = "All Categories";
                mLayoutControlItem.Control = checkEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                checkEdit1.Enter += new EventHandler(checkEdit1_Enter);
                checkEdit1.CheckedChanged += new EventHandler(checkEdit1_CheckedChanged);
                #endregion

                #region categories grid
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbCategories";
                mLayoutControlItem.CustomizationFormText = "Categories";
                mLayoutControlItem.Text = "Categories";
                gridControl1 = new GridControl();
                gridControl1.Name = "grd" + this.Name.Substring(3);
                mLayoutControlItem.Control = gridControl1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                DataTable mDtGridData = pMdtReporter.View_LookupData("som_productcategories", "code,description", 
                    "", "description", Program.gLanguageName, "grdIVSProductCategories");
                mDtGridData.Columns.Add("selected", typeof(System.Boolean));
                foreach (DataRow mDataRow in mDtGridData.Rows)
                {
                    mDataRow.BeginEdit();
                    mDataRow["selected"] = false;
                    mDataRow.EndEdit();
                }

                DevExpress.XtraGrid.Views.Grid.GridView mGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
                gridControl1.MainView = mGridView;
                gridControl1.DataSource = mDtGridData;

                //grid properties
                mGridView.OptionsView.ShowFooter = false;
                mGridView.OptionsView.ShowGroupPanel = false;
                mGridView.OptionsView.ShowIndicator = false;

                foreach (DevExpress.XtraGrid.Columns.GridColumn mGridColumn in mGridView.Columns)
                {
                    if (mGridColumn.FieldName.Trim().ToLower() == "selected")
                    {
                        mGridColumn.OptionsColumn.ShowCaption = false;
                    }
                    else
                    {
                        mGridColumn.OptionsColumn.AllowEdit = false;
                    }
                }

                DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit mCheckEdit =
                     new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
                mGridView.Columns["selected"].ColumnEdit = mCheckEdit;
                mCheckEdit.CheckedChanged += new EventHandler(mCheckEdit_CheckedChanged);
                gridControl1.Enter += new EventHandler(gridControl1_Enter);
                #endregion

                #region cboBalanceCondition
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbBalanceCondition";
                mLayoutControlItem.CustomizationFormText = "Balance Condition";
                mLayoutControlItem.Text = "Balance";
                comboBoxEdit2.Name = "cboBalanceCondition";
                comboBoxEdit2.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems2 = comboBoxEdit2.Properties.Items;
                pComboBoxItems2.AddRange(new string[] { "", "=", ">", ">=", "<", "<=", "<>" });
                #endregion

                #region txtBalanceLimit
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbBalanceLimit";
                mLayoutControlItem.CustomizationFormText = "Balance Limit";
                mLayoutControlItem.Text = "0";
                mLayoutControlItem.TextVisible = false;
                textEdit1.Name = "txtBalanceLimit";
                mLayoutControlItem.Control = textEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion
            }
        
            #endregion

            #region IVGRN

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.IVGRN)
            {
                #region store
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbStore";
                mLayoutControlItem.CustomizationFormText = "Store";
                mLayoutControlItem.Text = "Store";
                lookUpEdit1.Name = "cboStore";
                lookUpEdit1.Properties.NullText = "";
                mLayoutControlItem.Control = lookUpEdit1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                DataTable mDtLookupData = pMdtReporter.View_LookupData("som_stores",
                    "code,description", "", "description", Program.gLanguageName, "grdIVSStores");
                DataTable mDtThisLookup = new DataTable("lookupdata");
                mDtThisLookup.Columns.Add("code", typeof(System.String));
                mDtThisLookup.Columns.Add("description", typeof(System.String));

                DataView mDvUserStores = new DataView();
                mDvUserStores.Table = Program.gDtUserStores;
                mDvUserStores.Sort = "storecode";

                DataRow mNewRow = mDtThisLookup.NewRow();
                foreach (DataRow mDataRow in mDtLookupData.Rows)
                {
                    if (mDvUserStores.Find(mDataRow["code"].ToString().Trim()) >= 0)
                    {
                        mNewRow = mDtThisLookup.NewRow();
                        mNewRow["code"] = mDataRow["code"].ToString();
                        mNewRow["description"] = mDataRow["description"].ToString();
                        mDtThisLookup.Rows.Add(mNewRow);
                        mDtThisLookup.AcceptChanges();
                    }
                }

                foreach (DataColumn mDataColumn in mDtThisLookup.Columns)
                {
                    mDataColumn.Caption = mDtLookupData.Columns[mDataColumn.ColumnName].Caption;
                }

                lookUpEdit1.Properties.DataSource = mDtThisLookup;
                lookUpEdit1.Properties.DisplayMember = "description";
                lookUpEdit1.Properties.ValueMember = "code";

                lookUpEdit1.ItemIndex = Program.Get_LookupItemIndex(lookUpEdit1, "code", Program.gCurrentUser.StoreCode);
                #endregion

                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region group by

                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbGroupBy";
                mLayoutControlItem.CustomizationFormText = "Group By";
                mLayoutControlItem.Text = "Group By";
                mLayoutControlItem.TextVisible = false;
                radioGroup1.Name = "radGroupBy";
                radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
                new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "None"),
                new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Supplier"),
                new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Delivery #")});
                radioGroup1.SelectedIndex = 0;
                mLayoutControlItem.Control = radioGroup1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                #endregion

                #region chkShowExpiryDates
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbShowExpiryDates";
                mLayoutControlItem.CustomizationFormText = "Show Expiry Dates";
                mLayoutControlItem.Text = "Show Expiry Dates";
                mLayoutControlItem.TextVisible = false;
                checkEdit3.Name = "chkShowExpiryDates";
                checkEdit3.Text = "Show Expiry Dates";
                checkEdit3.Checked = false;
                mLayoutControlItem.Control = checkEdit3;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion
            }

            #endregion

            #region IVGIN

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.IVGIN)
            {
                #region store
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbStore";
                mLayoutControlItem.CustomizationFormText = "Store";
                mLayoutControlItem.Text = "Store";
                lookUpEdit1.Name = "cboStore";
                lookUpEdit1.Properties.NullText = "";
                mLayoutControlItem.Control = lookUpEdit1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                DataTable mDtLookupData = pMdtReporter.View_LookupData("som_stores",
                    "code,description", "", "description", Program.gLanguageName, "grdIVSStores");
                DataTable mDtThisLookup = new DataTable("lookupdata");
                mDtThisLookup.Columns.Add("code", typeof(System.String));
                mDtThisLookup.Columns.Add("description", typeof(System.String));

                DataView mDvUserStores = new DataView();
                mDvUserStores.Table = Program.gDtUserStores;
                mDvUserStores.Sort = "storecode";

                DataRow mNewRow = mDtThisLookup.NewRow();
                foreach (DataRow mDataRow in mDtLookupData.Rows)
                {
                    if (mDvUserStores.Find(mDataRow["code"].ToString().Trim()) >= 0)
                    {
                        mNewRow = mDtThisLookup.NewRow();
                        mNewRow["code"] = mDataRow["code"].ToString();
                        mNewRow["description"] = mDataRow["description"].ToString();
                        mDtThisLookup.Rows.Add(mNewRow);
                        mDtThisLookup.AcceptChanges();
                    }
                }

                foreach (DataColumn mDataColumn in mDtThisLookup.Columns)
                {
                    mDataColumn.Caption = mDtLookupData.Columns[mDataColumn.ColumnName].Caption;
                }

                lookUpEdit1.Properties.DataSource = mDtThisLookup;
                lookUpEdit1.Properties.DisplayMember = "description";
                lookUpEdit1.Properties.ValueMember = "code";

                lookUpEdit1.ItemIndex = Program.Get_LookupItemIndex(lookUpEdit1, "code", Program.gCurrentUser.StoreCode);
                #endregion

                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region group by

                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbGroupBy";
                mLayoutControlItem.CustomizationFormText = "Group By";
                mLayoutControlItem.Text = "Group By";
                mLayoutControlItem.TextVisible = false;
                radioGroup1.Name = "radGroupBy";
                radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
                new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "None"),
                new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Customer"),
                new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Delivery #")});
                radioGroup1.SelectedIndex = 0;
                mLayoutControlItem.Control = radioGroup1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                #endregion

                #region chkShowExpiryDates
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbShowExpiryDates";
                mLayoutControlItem.CustomizationFormText = "Show Expiry Dates";
                mLayoutControlItem.Text = "Show Expiry Dates";
                mLayoutControlItem.TextVisible = false;
                checkEdit3.Name = "chkShowExpiryDates";
                checkEdit3.Text = "Show Expiry Dates";
                checkEdit3.Checked = false;
                mLayoutControlItem.Control = checkEdit3;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion
            }

            #endregion

            #region IVProductHistory

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.IVProductHistory)
            {
                #region store
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbStore";
                mLayoutControlItem.CustomizationFormText = "Store";
                mLayoutControlItem.Text = "Store";
                lookUpEdit1.Name = "cboStore";
                lookUpEdit1.Properties.NullText = "";
                mLayoutControlItem.Control = lookUpEdit1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                DataTable mDtLookupData = pMdtReporter.View_LookupData("som_stores",
                    "code,description", "", "description", Program.gLanguageName, "grdIVSStores");
                DataTable mDtThisLookup = new DataTable("lookupdata");
                mDtThisLookup.Columns.Add("code", typeof(System.String));
                mDtThisLookup.Columns.Add("description", typeof(System.String));

                DataView mDvUserStores = new DataView();
                mDvUserStores.Table = Program.gDtUserStores;
                mDvUserStores.Sort = "storecode";

                DataRow mNewRow = mDtThisLookup.NewRow();
                foreach (DataRow mDataRow in mDtLookupData.Rows)
                {
                    if (mDvUserStores.Find(mDataRow["code"].ToString().Trim()) >= 0)
                    {
                        mNewRow = mDtThisLookup.NewRow();
                        mNewRow["code"] = mDataRow["code"].ToString();
                        mNewRow["description"] = mDataRow["description"].ToString();
                        mDtThisLookup.Rows.Add(mNewRow);
                        mDtThisLookup.AcceptChanges();
                    }
                }

                foreach (DataColumn mDataColumn in mDtThisLookup.Columns)
                {
                    mDataColumn.Caption = mDtLookupData.Columns[mDataColumn.ColumnName].Caption;
                }

                lookUpEdit1.Properties.DataSource = mDtThisLookup;
                lookUpEdit1.Properties.DisplayMember = "description";
                lookUpEdit1.Properties.ValueMember = "code";

                lookUpEdit1.ItemIndex = Program.Get_LookupItemIndex(lookUpEdit1, "code", Program.gCurrentUser.StoreCode);
                #endregion

                //txtProductCode
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbProductCode";
                mLayoutControlItem.CustomizationFormText = "Product Code";
                mLayoutControlItem.Text = "Product Code";
                textEditSearch.Name = "txtProductCode";
                textEditSearch.KeyDown += new KeyEventHandler(textEditSearch_KeyDown);
                textEditSearch.Leave += new EventHandler(textEditSearch_Leave);
                textEditSearch.EditValueChanged += new EventHandler(textEditSearch_EditValueChanged);
                mLayoutControlItem.Control = textEditSearch;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                //cmdSearchProduct
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbSearchProduct";
                mLayoutControlItem.CustomizationFormText = "Search";
                mLayoutControlItem.Text = "Search";
                mLayoutControlItem.TextVisible = false;
                simpleButtonSearch.Text = "Search";
                simpleButtonSearch.Name = "cmdSearchProduct";
                simpleButtonSearch.Click += new EventHandler(simpleButtonSearch_Click);
                mLayoutControlItem.Control = simpleButtonSearch;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                //txtProductDescription
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbProductDescription";
                mLayoutControlItem.CustomizationFormText = "Product Description";
                mLayoutControlItem.Text = "Product Description";
                textEdit1.Name = "txtProductDescription";
                mLayoutControlItem.Control = textEdit1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                //txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                //txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                //chkAllTransactions
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAllTransactions";
                mLayoutControlItem.CustomizationFormText = "All Transactions";
                mLayoutControlItem.Text = "All Transactions";
                mLayoutControlItem.TextVisible = false;
                checkEdit1.Name = "chkAllTransactions";
                checkEdit1.Text = "All Transactions";
                checkEdit1.Checked = true;
                mLayoutControlItem.Control = checkEdit1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
            }

            #endregion

            #region IVPriceList

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.IVPriceList)
            {
                #region store
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbStore";
                mLayoutControlItem.CustomizationFormText = "Store";
                mLayoutControlItem.Text = "Store";
                lookUpEdit1.Name = "cboStore";
                lookUpEdit1.Properties.NullText = "";
                mLayoutControlItem.Control = lookUpEdit1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                DataTable mDtLookupData = pMdtReporter.View_LookupData("som_stores",
                    "code,description", "", "description", Program.gLanguageName, "grdIVSStores");
                DataTable mDtThisLookup = new DataTable("lookupdata");
                mDtThisLookup.Columns.Add("code", typeof(System.String));
                mDtThisLookup.Columns.Add("description", typeof(System.String));

                DataView mDvUserStores = new DataView();
                mDvUserStores.Table = Program.gDtUserStores;
                mDvUserStores.Sort = "storecode";

                DataRow mNewRow = mDtThisLookup.NewRow();
                if (mDtLookupData.Rows.Count > 0)
                {
                    mNewRow = mDtThisLookup.NewRow();
                    mNewRow["code"] = "";
                    mNewRow["description"] = "All Stores";
                    mDtThisLookup.Rows.Add(mNewRow);
                    mDtThisLookup.AcceptChanges();
                }

                foreach (DataRow mDataRow in mDtLookupData.Rows)
                {
                    if (mDvUserStores.Find(mDataRow["code"].ToString().Trim()) >= 0)
                    {
                        mNewRow = mDtThisLookup.NewRow();
                        mNewRow["code"] = mDataRow["code"].ToString();
                        mNewRow["description"] = mDataRow["description"].ToString();
                        mDtThisLookup.Rows.Add(mNewRow);
                        mDtThisLookup.AcceptChanges();
                    }
                }

                foreach (DataColumn mDataColumn in mDtThisLookup.Columns)
                {
                    mDataColumn.Caption = mDtLookupData.Columns[mDataColumn.ColumnName].Caption;
                }

                lookUpEdit1.Properties.DataSource = mDtThisLookup;
                lookUpEdit1.Properties.DisplayMember = "description";
                lookUpEdit1.Properties.ValueMember = "code";

                lookUpEdit1.ItemIndex = Program.Get_LookupItemIndex(lookUpEdit1, "code", Program.gCurrentUser.StoreCode);
                #endregion

                #region chkallcategories
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAllCategories";
                mLayoutControlItem.CustomizationFormText = "All Categories";
                mLayoutControlItem.Text = "All Categories";
                mLayoutControlItem.TextVisible = false;
                checkEdit1.Name = "chkAllCategories";
                checkEdit1.Text = "All Categories";
                mLayoutControlItem.Control = checkEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                checkEdit1.Enter += new EventHandler(checkEdit1_Enter);
                checkEdit1.CheckedChanged += new EventHandler(checkEdit1_CheckedChanged);
                #endregion

                #region categories grid
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbCategories";
                mLayoutControlItem.CustomizationFormText = "Categories";
                mLayoutControlItem.Text = "Categories";
                gridControl1 = new GridControl();
                gridControl1.Name = "grd" + this.Name.Substring(3);
                mLayoutControlItem.Control = gridControl1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                DataTable mDtGridData = pMdtReporter.View_LookupData("som_productcategories", "code,description",
                    "", "description", Program.gLanguageName, "grdIVSProductCategories");
                mDtGridData.Columns.Add("selected", typeof(System.Boolean));
                foreach (DataRow mDataRow in mDtGridData.Rows)
                {
                    mDataRow.BeginEdit();
                    mDataRow["selected"] = false;
                    mDataRow.EndEdit();
                }

                DevExpress.XtraGrid.Views.Grid.GridView mGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
                gridControl1.MainView = mGridView;
                gridControl1.DataSource = mDtGridData;

                //grid properties
                mGridView.OptionsView.ShowFooter = false;
                mGridView.OptionsView.ShowGroupPanel = false;
                mGridView.OptionsView.ShowIndicator = false;

                foreach (DevExpress.XtraGrid.Columns.GridColumn mGridColumn in mGridView.Columns)
                {
                    if (mGridColumn.FieldName.Trim().ToLower() == "selected")
                    {
                        mGridColumn.OptionsColumn.ShowCaption = false;
                    }
                    else
                    {
                        mGridColumn.OptionsColumn.AllowEdit = false;
                    }
                }

                DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit mCheckEdit =
                     new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
                mGridView.Columns["selected"].ColumnEdit = mCheckEdit;
                mCheckEdit.CheckedChanged += new EventHandler(mCheckEdit_CheckedChanged);
                gridControl1.Enter += new EventHandler(gridControl1_Enter);
                #endregion
            }

            #endregion

            #endregion

            #region laboratory

            #region LABPatientTestsCount

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.LABPatientTestsCount)
            {
                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion
                                             
            }

            #endregion

            #region LABCountByResult

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.LABCountByResult)
            {
                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region cboAgeCondition
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeCondition";
                mLayoutControlItem.CustomizationFormText = "Age Condition";
                mLayoutControlItem.Text = "AGE";
                comboBoxEdit1.Name = "cboAgeCondition";
                comboBoxEdit1.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit1.Properties.Items;
                pComboBoxItems1.AddRange(new string[] { "", "=", ">", ">=", "<", "<=", "<>" });
                #endregion

                #region txtAgeLimit
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeLimit";
                mLayoutControlItem.CustomizationFormText = "Age Limit";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                textEdit1.Name = "txtAgeLimit";
                mLayoutControlItem.Control = textEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                this.Load_ExtraPatientControls();
                this.Fill_LookupData();
            }

            #endregion

            #region LABListingByResult

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.LABListingByResult)
            {
                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region cboAgeCondition
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeCondition";
                mLayoutControlItem.CustomizationFormText = "Age Condition";
                mLayoutControlItem.Text = "AGE";
                comboBoxEdit1.Name = "cboAgeCondition";
                comboBoxEdit1.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit1.Properties.Items;
                pComboBoxItems1.AddRange(new string[] { "", "=", ">", ">=", "<", "<=", "<>" });
                #endregion

                #region txtAgeLimit
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeLimit";
                mLayoutControlItem.CustomizationFormText = "Age Limit";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                textEdit1.Name = "txtAgeLimit";
                mLayoutControlItem.Control = textEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                this.Load_ExtraPatientControls();
                this.Fill_LookupData();
            }

            #endregion

            #region LABCountMonthly

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.LABCountMonthly)
            {
                #region txtYear
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbYear";
                mLayoutControlItem.CustomizationFormText = "Year";
                mLayoutControlItem.Text = "Year";
                mLayoutControlItem.TextVisible = false;
                textEdit1.Name = "txtYear";
                mLayoutControlItem.Control = textEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region cboMonthFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbMonthFrom";
                mLayoutControlItem.CustomizationFormText = "From Month";
                mLayoutControlItem.Text = "From Month";
                comboBoxEdit1.Name = "cboMonthFrom";
                comboBoxEdit1.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit1.Properties.Items;
                pComboBoxItems1.AddRange(new string[] { "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGOST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" });
                #endregion

                #region cboMonthTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbMonthTo";
                mLayoutControlItem.CustomizationFormText = "To Month";
                mLayoutControlItem.Text = "To Month";
                comboBoxEdit2.Name = "cboMonthTo";
                comboBoxEdit2.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems2 = comboBoxEdit2.Properties.Items;
                pComboBoxItems2.AddRange(new string[] { "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGOST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" });
                #endregion

                #region txbAllTests
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAllTests";
                mLayoutControlItem.CustomizationFormText = "All Tests";
                mLayoutControlItem.Text = "All Tests";
                mLayoutControlItem.TextVisible = false;
                checkEdit1.Name = "chkAllTests";
                checkEdit1.Text = "All Tests";
                mLayoutControlItem.Control = checkEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                checkEdit1.Enter += new EventHandler(checkEdit1_Enter);
                checkEdit1.CheckedChanged += new EventHandler(checkEdit1_CheckedChanged);
                #endregion

                #region tests grid
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbTests";
                mLayoutControlItem.CustomizationFormText = "Tests";
                mLayoutControlItem.Text = "Tests";
                gridControl1 = new GridControl();
                gridControl1.Name = "grd" + this.Name.Substring(3);
                mLayoutControlItem.Control = gridControl1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                DataTable mDtGridData = pMdtReporter.View_LookupData("labtests", "code,description",
                    "", "description", Program.gLanguageName, "grdLABTests");
                mDtGridData.Columns.Add("selected", typeof(System.Boolean));
                foreach (DataRow mDataRow in mDtGridData.Rows)
                {
                    mDataRow.BeginEdit();
                    mDataRow["selected"] = false;
                    mDataRow.EndEdit();
                }

                DevExpress.XtraGrid.Views.Grid.GridView mGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
                gridControl1.MainView = mGridView;
                gridControl1.DataSource = mDtGridData;

                //grid properties
                mGridView.OptionsView.ShowFooter = false;
                mGridView.OptionsView.ShowGroupPanel = false;
                mGridView.OptionsView.ShowIndicator = false;

                foreach (DevExpress.XtraGrid.Columns.GridColumn mGridColumn in mGridView.Columns)
                {
                    if (mGridColumn.FieldName.Trim().ToLower() == "selected")
                    {
                        mGridColumn.OptionsColumn.ShowCaption = false;
                    }
                    else
                    {
                        mGridColumn.OptionsColumn.AllowEdit = false;
                    }
                }

                DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit mCheckEdit =
                     new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
                mGridView.Columns["selected"].ColumnEdit = mCheckEdit;
                mCheckEdit.CheckedChanged += new EventHandler(mCheckEdit_CheckedChanged);
                gridControl1.Enter += new EventHandler(gridControl1_Enter);
                #endregion
            }

            #endregion

            #region Lab patient count by technician

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.LABPatientCountByLabTechnician)
            {

                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region Lab Technician Id
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbLabTechnicianId";
                mLayoutControlItem.CustomizationFormText = "Lab Technician Id";
                mLayoutControlItem.Text = "Lab Technician Id";
                lookUpEdit1.Name = "cboLabTechnician";
                lookUpEdit1.Properties.NullText = "";
                mLayoutControlItem.Control = lookUpEdit1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                DataTable mDtLookupData = pMdtReporter.View_LookupData("facilitystaffs",
                    "code,description", "category="
                    + (int)AfyaPro_Types.clsEnums.StaffCategories.LabTechnicians, "code", Program.gLanguageName, "grdGENMedicalStaffs");
                DataTable mDtThisLookup = new DataTable("lookupdata");
                mDtThisLookup.Columns.Add("code", typeof(System.String));
                mDtThisLookup.Columns.Add("description", typeof(System.String));

                DataRow mNewRow = mDtThisLookup.NewRow();
                mNewRow["code"] = "";
                mNewRow["description"] = "";
                mDtThisLookup.Rows.Add(mNewRow);
                mDtThisLookup.AcceptChanges();
                foreach (DataRow mDataRow in mDtLookupData.Rows)
                {
                    mNewRow = mDtThisLookup.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    mDtThisLookup.Rows.Add(mNewRow);
                    mDtThisLookup.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in mDtThisLookup.Columns)
                {
                    mDataColumn.Caption = mDtLookupData.Columns[mDataColumn.ColumnName].Caption;
                }
                lookUpEdit1.Properties.DataSource = mDtThisLookup;
                lookUpEdit1.Properties.DisplayMember = "description";
                lookUpEdit1.Properties.ValueMember = "code";
                lookUpEdit1.EditValue = lookUpEdit1.Properties.GetDataSourceRowByKeyValue("");
                #endregion
            }

            #endregion

            #endregion

            #region rch

            #region AttendanceList

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.RCH_AttendanceList)
            {
                AfyaPro_MT.clsRCHClients mMdtRCHClients = (AfyaPro_MT.clsRCHClients)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRCHClients),
                    Program.gMiddleTier + "clsRCHClients");

                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region cboAgeCondition
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeCondition";
                mLayoutControlItem.CustomizationFormText = "Age Condition";
                mLayoutControlItem.Text = "AGE";
                comboBoxEdit1.Name = "cboAgeCondition";
                comboBoxEdit1.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit1.Properties.Items;
                pComboBoxItems1.AddRange(new string[] { "", "=", ">", ">=", "<", "<=", "<>" });
                #endregion

                #region txtAgeLimit
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeLimit";
                mLayoutControlItem.CustomizationFormText = "Age Limit";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                textEdit1.Name = "txtAgeLimit";
                mLayoutControlItem.Control = textEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region cboGender
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbGender";
                mLayoutControlItem.CustomizationFormText = "Gender";
                mLayoutControlItem.Text = "GENDER";
                comboBoxEdit2.Name = "cboGender";
                comboBoxEdit2.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems2 = comboBoxEdit2.Properties.Items;
                pComboBoxItems2.AddRange(new string[] { "", "M", "F" });
                #endregion

                #region servicetype
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbServiceType";
                mLayoutControlItem.CustomizationFormText = "Service Type";
                mLayoutControlItem.Text = "Service Type";
                lookUpEdit1.Name = "cboServiceType";
                lookUpEdit1.Properties.NullText = "";
                mLayoutControlItem.Control = lookUpEdit1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                DataTable mDtLookupData = mMdtRCHClients.Get_RCHServices(Program.gLanguageName, "grdRCHServices");
                DataTable mDtThisLookup = new DataTable("lookupdata");
                mDtThisLookup.Columns.Add("code", typeof(System.String));
                mDtThisLookup.Columns.Add("description", typeof(System.String));

                DataRow mNewRow = mDtThisLookup.NewRow();
                mNewRow["code"] = "";
                mNewRow["description"] = "";
                mDtThisLookup.Rows.Add(mNewRow);
                mDtThisLookup.AcceptChanges();
                foreach (DataRow mDataRow in mDtLookupData.Rows)
                {
                    mNewRow = mDtThisLookup.NewRow();
                    mNewRow["code"] = mDataRow["fieldname"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    mDtThisLookup.Rows.Add(mNewRow);
                    mDtThisLookup.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in mDtThisLookup.Columns)
                {
                    mDataColumn.Caption = mDtLookupData.Columns[mDataColumn.ColumnName].Caption;
                }

                lookUpEdit1.Properties.DataSource = mDtThisLookup;
                lookUpEdit1.Properties.DisplayMember = "description";
                lookUpEdit1.Properties.ValueMember = "code";
                #endregion

                this.Load_ExtraPatientControls();
                this.Fill_LookupData();
            }

            #endregion

            #region VaccinationsCount

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.RCH_VaccinationCount)
            {
                AfyaPro_MT.clsRCHClients mMdtRCHClients = (AfyaPro_MT.clsRCHClients)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRCHClients),
                    Program.gMiddleTier + "clsRCHClients");

                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region cboAgeCondition
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeCondition";
                mLayoutControlItem.CustomizationFormText = "Age Condition";
                mLayoutControlItem.Text = "AGE";
                comboBoxEdit1.Name = "cboAgeCondition";
                comboBoxEdit1.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit1.Properties.Items;
                pComboBoxItems1.AddRange(new string[] { "", "=", ">", ">=", "<", "<=", "<>" });
                #endregion

                #region txtAgeLimit
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeLimit";
                mLayoutControlItem.CustomizationFormText = "Years";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                textEdit1.Name = "txtAgeLimit";
                mLayoutControlItem.Control = textEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeMonths";
                mLayoutControlItem.CustomizationFormText = "Months";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                textEdit2.Name = "txtAgeMonths";
                mLayoutControlItem.Control = textEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region cboGender
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbGender";
                mLayoutControlItem.CustomizationFormText = "Gender";
                mLayoutControlItem.Text = "GENDER";
                comboBoxEdit2.Name = "cboGender";
                comboBoxEdit2.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems2 = comboBoxEdit2.Properties.Items;
                pComboBoxItems2.AddRange(new string[] { "", "M", "F" });
                #endregion

                #region servicetype
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbServiceType";
                mLayoutControlItem.CustomizationFormText = "Service Type";
                mLayoutControlItem.Text = "Service Type";
                lookUpEdit1.Name = "cboServiceType";
                lookUpEdit1.Properties.NullText = "";
                mLayoutControlItem.Control = lookUpEdit1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                DataTable mDtLookupData = mMdtRCHClients.Get_RCHServices(Program.gLanguageName, "grdRCHServices");
                DataTable mDtThisLookup = new DataTable("lookupdata");
                mDtThisLookup.Columns.Add("code", typeof(System.String));
                mDtThisLookup.Columns.Add("description", typeof(System.String));

                DataRow mNewRow = mDtThisLookup.NewRow();
                mNewRow["code"] = "";
                mNewRow["description"] = "";
                mDtThisLookup.Rows.Add(mNewRow);
                mDtThisLookup.AcceptChanges();
                foreach (DataRow mDataRow in mDtLookupData.Rows)
                {
                    mNewRow = mDtThisLookup.NewRow();
                    mNewRow["code"] = mDataRow["fieldname"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    mDtThisLookup.Rows.Add(mNewRow);
                    mDtThisLookup.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in mDtThisLookup.Columns)
                {
                    mDataColumn.Caption = mDtLookupData.Columns[mDataColumn.ColumnName].Caption;
                }

                lookUpEdit1.Properties.DataSource = mDtThisLookup;
                lookUpEdit1.Properties.DisplayMember = "description";
                lookUpEdit1.Properties.ValueMember = "code";
                #endregion

                this.Load_ExtraPatientControls();
                this.Fill_LookupData();
            }

            #endregion

            #region RCH_FPlanMethodsCount

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.RCH_FPlanMethodsCount)
            {
                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region cboAgeCondition
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeCondition";
                mLayoutControlItem.CustomizationFormText = "Age Condition";
                mLayoutControlItem.Text = "AGE";
                comboBoxEdit1.Name = "cboAgeCondition";
                comboBoxEdit1.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit1.Properties.Items;
                pComboBoxItems1.AddRange(new string[] { "", "=", ">", ">=", "<", "<=", "<>" });
                #endregion

                #region txtAgeLimit
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeLimit";
                mLayoutControlItem.CustomizationFormText = "Years";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                textEdit1.Name = "txtAgeLimit";
                mLayoutControlItem.Control = textEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeMonths";
                mLayoutControlItem.CustomizationFormText = "Months";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                textEdit2.Name = "txtAgeMonths";
                mLayoutControlItem.Control = textEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region cboGender
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbGender";
                mLayoutControlItem.CustomizationFormText = "Gender";
                mLayoutControlItem.Text = "GENDER";
                comboBoxEdit2.Name = "cboGender";
                comboBoxEdit2.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems2 = comboBoxEdit2.Properties.Items;
                pComboBoxItems2.AddRange(new string[] { "", "M", "F" });
                #endregion
            }

            #endregion

            #region RCH_AntenatalAtt

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.RCH_AntenatalAtt)
            {
                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion
            }

            #endregion

            #region RCH_PostnatalAtt

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.RCH_PostnatalAtt)
            {
                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion
            }

            #endregion

            #region RCHMaternityMonthlyReport

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.RCH_MaternityMonthlySummary)
            {
                #region txtYear
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbYear";
                mLayoutControlItem.CustomizationFormText = "Year";
                mLayoutControlItem.Text = "Year";
                mLayoutControlItem.TextVisible = false;
                comboBoxEdit2.Name = "txtYear";
                mLayoutControlItem.Control = comboBoxEdit2;
                comboBoxEdit2.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems2 = comboBoxEdit2.Properties.Items;
                pComboBoxItems2.AddRange(new string[] { "2009", "2010", "2011", "2012", "2013", "2014", "2015", "2016", "2017", "2018", "2019", "2020", "2021", "2021" });
                #endregion

                #region cboMonth
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbMonth";
                mLayoutControlItem.CustomizationFormText = "Month";
                mLayoutControlItem.Text = "Month";
                comboBoxEdit1.Name = "cboMonth";
                comboBoxEdit1.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit1.Properties.Items;
                pComboBoxItems1.AddRange(new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" });
                #endregion
            }

            #endregion

            #region RCH_ObstetricMonthlySummary

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.RCH_ObstetricMonthlyReport)
            {
                #region txtYear
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbYear";
                mLayoutControlItem.CustomizationFormText = "Year";
                mLayoutControlItem.Text = "Year";
                mLayoutControlItem.TextVisible = false;
                comboBoxEdit2.Name = "txtYear";
                mLayoutControlItem.Control = comboBoxEdit2;
                comboBoxEdit2.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems2 = comboBoxEdit2.Properties.Items;
                pComboBoxItems2.AddRange(new string[] { "2009", "2010", "2011", "2012", "2013", "2014", "2015", "2016", "2017", "2018", "2019", "2020", "2021", "2021" });
                #endregion

                #region cboMonth
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbMonth";
                mLayoutControlItem.CustomizationFormText = "Month";
                mLayoutControlItem.Text = "Month";
                comboBoxEdit1.Name = "cboMonth";
                comboBoxEdit1.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit1.Properties.Items;
                pComboBoxItems1.AddRange(new string[] { "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" });
                #endregion
            }

            #endregion

            #endregion

            #region ctc/art

            #region CTCAttendanceCountAge

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.CTCAttendanceCountAge)
            {
                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region Age Groups

                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeGroups";
                mLayoutControlItem.CustomizationFormText = "Age Groups";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                simpleButton1.Text = "Age Groups";
                simpleButton1.Name = "cmdAgeGroups";
                simpleButton1.Click += new EventHandler(simpleButton_Click);
                mLayoutControlItem.Control = simpleButton1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                #endregion
            }

            #endregion

            #region CTCAttendanceCD4T

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.CTCAttendanceCD4T)
            {
                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region Age Groups

                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbAgeGroups";
                mLayoutControlItem.CustomizationFormText = "Age Groups";
                mLayoutControlItem.Text = "";
                mLayoutControlItem.TextVisible = false;
                simpleButton1.Text = "Age Groups";
                simpleButton1.Name = "cmdAgeGroups";
                simpleButton1.Click += new EventHandler(simpleButton_Click);
                mLayoutControlItem.Control = simpleButton1;
                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                #endregion
            }

            #endregion

            #region CTCANCMonthlySummary

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.RCH_ANCMonthlySummary)
            {
                #region txtYear
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbYear";
                mLayoutControlItem.CustomizationFormText = "Year";
                mLayoutControlItem.Text = "Year";
                mLayoutControlItem.TextVisible = false;
                comboBoxEdit2.Name = "txtYear";
                mLayoutControlItem.Control = comboBoxEdit2;
                comboBoxEdit2.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems2 = comboBoxEdit2.Properties.Items;
                pComboBoxItems2.AddRange(new string[] { "2009", "2010", "2011", "2012", "2013", "2014", "2015", "2016", "2017", "2018", "2019", "2020", "2021", "2021" });
                #endregion

                #region cboMonth
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbMonth";
                mLayoutControlItem.CustomizationFormText = "Month";
                mLayoutControlItem.Text = "Month";
                comboBoxEdit1.Name = "cboMonth";
                comboBoxEdit1.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit1.Properties.Items;
                pComboBoxItems1.AddRange(new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" });
                #endregion
            }

            #endregion

            #region CTCTestingAndCounseling

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.CTCTestingAndCounseling)
            { 
                // Get year range
                int nMinYear = DateTime.MinValue.Year;
                int nMaxYear = DateTime.MaxValue.Year;
                int nCurrentYear = DateTime.Now.Year;

                #region cboYear
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbYear";
                mLayoutControlItem.CustomizationFormText = "Year";
                mLayoutControlItem.Text = "Year";
                comboBoxEdit1.Name = "cboYear";
                comboBoxEdit1.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit1.Properties.Items;
                
                for (int nIndex = nMinYear; nIndex <= nMaxYear; nIndex++)
                {
                    pComboBoxItems1.Add(nIndex);
                }
                comboBoxEdit1.SelectedItem = nCurrentYear;
                #endregion

                #region cboFromMonth
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbFromMonth";
                mLayoutControlItem.CustomizationFormText = "From Month";
                mLayoutControlItem.Text = "From Month";
                comboBoxEdit2.Name = "cboFromMonth";
                comboBoxEdit2.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit2.Properties.Items;
                foreach (string monthName in Enum.GetNames(typeof(AfyaPro_Types.clsEnums.MONTH)))
                {
                    pComboBoxItems1.Add(monthName);
                }
                #endregion
             
                #region cboToMonth
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbToMonth";
                mLayoutControlItem.CustomizationFormText = "To Month";
                mLayoutControlItem.Text = "To Month";
                comboBoxEdit3.Name = "cboToMonth";
                comboBoxEdit3.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit3;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit3.Properties.Items;
                foreach (string monthName in Enum.GetNames(typeof(AfyaPro_Types.clsEnums.MONTH)))
                {
                    pComboBoxItems1.Add(monthName);
                }
                #endregion
            }
            #endregion

            #region CTCExposedChildrenMonthlyReport

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.CTCExposedChildrenMonthlyReport)
            {
                // Get year range
                int nMinYear = DateTime.MinValue.Year;
                int nMaxYear = DateTime.MaxValue.Year;
                int nCurrentYear = DateTime.Now.Year;

                #region cboFromYear
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbYear";
                mLayoutControlItem.CustomizationFormText = "Year";
                mLayoutControlItem.Text = "Year";
                comboBoxEdit1.Name = "cboYear";
                comboBoxEdit1.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit1.Properties.Items;

                for (int nIndex = nMinYear; nIndex <= nMaxYear; nIndex++)
                {
                    pComboBoxItems1.Add(nIndex);
                }
                comboBoxEdit1.SelectedItem = nCurrentYear;
                #endregion

                #region cboFromMonth
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbFromMonth";
                mLayoutControlItem.CustomizationFormText = "From Month";
                mLayoutControlItem.Text = "From Month";
                comboBoxEdit2.Name = "cboFromMonth";
                comboBoxEdit2.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit2.Properties.Items;
                foreach (string monthName in Enum.GetNames(typeof(AfyaPro_Types.clsEnums.MONTH)))
                {
                    pComboBoxItems1.Add(monthName);
                }
                #endregion
                               
                #region cboToMonth
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbToMonth";
                mLayoutControlItem.CustomizationFormText = "To Month";
                mLayoutControlItem.Text = "To Month";
                comboBoxEdit3.Name = "cboToMonth";
                comboBoxEdit3.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit3;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit3.Properties.Items;
                foreach (string monthName in Enum.GetNames(typeof(AfyaPro_Types.clsEnums.MONTH)))
                {
                    pComboBoxItems1.Add(monthName);
                }
                #endregion
            }
            #endregion

            #region CTCDNAPCRMonthlyReport

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.CTCDNAPCRMonthlyReport)
            {
                #region cboYear
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbYear";
                mLayoutControlItem.CustomizationFormText = "Year";
                mLayoutControlItem.Text = "Year";
                comboBoxEdit1.Name = "cboYear";
                comboBoxEdit1.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit1.Properties.Items;

                int nMinYear = DateTime.MinValue.Year;
                int nMaxYear = DateTime.MaxValue.Year;
                int nCurrentYear = DateTime.Now.Year;
                for (int nIndex = nMinYear; nIndex <= nMaxYear; nIndex++)
                {
                    pComboBoxItems1.Add(nIndex);
                }
                comboBoxEdit1.SelectedItem = nCurrentYear;

                #endregion

                #region cboFromMonth
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbFromMonth";
                mLayoutControlItem.CustomizationFormText = "From Month";
                mLayoutControlItem.Text = "From Month";
                comboBoxEdit2.Name = "cboFromMonth";
                comboBoxEdit2.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit2.Properties.Items;
                foreach (string monthName in Enum.GetNames(typeof(AfyaPro_Types.clsEnums.MONTH)))
                {
                    pComboBoxItems1.Add(monthName);
                }
                #endregion

                #region cboToMonth
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbToMonth";
                mLayoutControlItem.CustomizationFormText = "To Month";
                mLayoutControlItem.Text = "To Month";
                comboBoxEdit3.Name = "cboToMonth";
                comboBoxEdit3.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit3;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit3.Properties.Items;
                foreach (string monthName in Enum.GetNames(typeof(AfyaPro_Types.clsEnums.MONTH)))
                {
                    pComboBoxItems1.Add(monthName);
                }
                #endregion

            }
            #endregion

            #region CTCARTClinicQuarterlySupervisionForm

            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.CTCARTClinicQuarterlySupervision)
            {
                #region cboYear
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbYear";
                mLayoutControlItem.CustomizationFormText = "Year";
                mLayoutControlItem.Text = "Year";
                comboBoxEdit1.Name = "cboYear";
                comboBoxEdit1.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit1.Properties.Items;

                int nMinYear = DateTime.MinValue.Year;
                int nMaxYear = DateTime.MaxValue.Year;
                int nCurrentYear = DateTime.Now.Year;
                for (int nIndex = nMinYear; nIndex <= nMaxYear; nIndex++)
                {
                    pComboBoxItems1.Add(nIndex);
                }
                comboBoxEdit1.SelectedItem = nCurrentYear;

                #region cboMonth
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbQuarter";
                mLayoutControlItem.CustomizationFormText = "Quarter";
                mLayoutControlItem.Text = "Quarter";
                comboBoxEdit2.Name = "cboQuarter";
                comboBoxEdit2.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                mLayoutControlItem.Control = comboBoxEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                pComboBoxItems1 = comboBoxEdit2.Properties.Items;
                foreach (string quarterName in Enum.GetNames(typeof(AfyaPro_Types.clsEnums.QUARTER)))
                {
                    pComboBoxItems1.Add(quarterName);
                }
                #endregion

                #endregion
            }
            #endregion
                                
            #endregion

            #region User Login Details
            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.UserLoginDetailReport)
            {
                #region txtDateFrom
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateFrom";
                mLayoutControlItem.CustomizationFormText = "FROM";
                mLayoutControlItem.Text = "FROM";
                dateEdit1.Name = "txtDateFrom";
                dateEdit1.EditValue = null;
                dateEdit1.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit1;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion

                #region txtDateTo
                mLayoutControlItem = new LayoutControlItem();
                mLayoutControlItem.Name = "txbDateTo";
                mLayoutControlItem.CustomizationFormText = "TO";
                mLayoutControlItem.Text = "TO";
                dateEdit2.Name = "txtDateTo";
                dateEdit2.EditValue = null;
                dateEdit2.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                mLayoutControlItem.Control = dateEdit2;
                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                #endregion
            }
            #endregion
        }
        #endregion

        #region Load_ExtraPatientControls
        private void Load_ExtraPatientControls()
        {
            string mFunctionName = "Load_ExtraPatientControls";

            LayoutControlItem mLayoutControlItem;

            try
            {
                DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");
                DataTable mDtExtraFilterFields = pMdtPatientExtraFields.View("filteronvaluefrom<>''", "", "", "");

                DataView mDvExtraFilterFields = new DataView();
                mDvExtraFilterFields.Table = mDtExtraFilterFields;
                mDvExtraFilterFields.Sort = "filteronvaluefrom";

                foreach (DataRow mDataRow in mDtExtraFields.Rows)
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

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            string mFunctionName = "Fill_LookupData";

            try
            {
                DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");

                foreach (DataRow mDataRow in mDtExtraFields.Rows)
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
                            mComboItems.Add("");
                            foreach (DataRow mLookupDataRow in mDtExtraFieldLookup.Rows)
                            {
                                mComboItems.Add(mLookupDataRow["description"].ToString().Trim());
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
                    mComboItems.Add("");
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

        #region mDateEdit_EditValueChanged
        void mDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            Program.AddTimeToDate((DateEdit)sender);
        }
        #endregion

        #region radioGroup1_SelectedIndexChanged
        void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.DebtorStatement)
            {
                textEditSearch.Text = "";
                textEdit1.Text = "";
            }
        }
        #endregion

        #region cmdView_Click
        internal void cmdView_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdView_Click";

            try
            {
                this.Cursor = Cursors.WaitCursor;

                DataTable mDtData = new DataTable();
                DataSet mDsData = new DataSet();
                bool mIsDataSet = false;

                string mReportCode = pReportCode;

                #region get reporting data

                #region debtorstatement

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.DebtorStatement)
                {
                    string mDebtorType = AfyaPro_Types.clsEnums.DebtorTypes.Individual.ToString().Trim();
                    if (radioGroup1.SelectedIndex == 1)
                    {
                        mDebtorType = AfyaPro_Types.clsEnums.DebtorTypes.Group.ToString().Trim();
                    }

                    mDtData = pMdtReporter.BIL_DebtorStatement(
                        textEditSearch.Text, textEdit1.Text, mDebtorType,
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), !checkEdit1.Checked);
                }

                #endregion

                #region billingitembarcodes

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.BillingItemBarcodes)
                {
                    mDtData = pMdtReporter.BIL_BillingItemsForBarcode();
                }

                #endregion

                #region registration

                #region attendancelist

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.AttendanceList)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    #region age
                    if (layoutControl1.GetItemByControl(comboBoxEdit1).Visible == true)
                    {
                        if (comboBoxEdit1.Text.Trim() != "" && Program.IsMoney(textEdit1.Text) != false)
                        {
                            string mPatientAge = pMdtReporter.Age_Formula("p.birthdate","bookdate", "");
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + comboBoxEdit1.Text + " " + textEdit1.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region gender
                    if (layoutControl1.GetItemByControl(comboBoxEdit2).Visible == true)
                    {
                        if (comboBoxEdit2.Text.Trim() != "")
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(p.gender='" + comboBoxEdit2.Text + "')";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (p.gender='" + comboBoxEdit2.Text + "')";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text + ": " + comboBoxEdit2.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region weight
                    if (layoutControl1.GetItemByControl(comboBoxEdit3).Visible == true)
                    {
                        if (comboBoxEdit3.Text.Trim() != "" && Program.IsMoney(textEdit2.Text) != false)
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(patientweight " + comboBoxEdit3.Text + " " + textEdit2.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (patientweight " + comboBoxEdit3.Text + " " + textEdit2.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit3).Text
                                + ": " + comboBoxEdit3.Text + " " + textEdit2.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit3).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region temperature
                    if (layoutControl1.GetItemByControl(comboBoxEdit4).Visible == true)
                    {
                        if (comboBoxEdit4.Text.Trim() != "" && Program.IsMoney(textEdit3.Text) != false)
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(patienttemperature " + comboBoxEdit4.Text + " " + textEdit3.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (patienttemperature " + comboBoxEdit4.Text + " " + textEdit3.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit4).Text
                                + ": " + comboBoxEdit4.Text + " " + textEdit3.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit4).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region treatmentpoint
                    if (layoutControl1.GetItemByControl(lookUpEdit1).Visible == true)
                    {
                        string mTreatmentPointCode = "";
                        string mTreatmentPointDescription = "";

                        if (lookUpEdit1.ItemIndex != -1)
                        {
                            mTreatmentPointCode = lookUpEdit1.GetColumnValue("code").ToString().Trim();
                            mTreatmentPointDescription = lookUpEdit1.GetColumnValue("description").ToString().Trim();
                        }

                        if (mTreatmentPointCode.Trim() != "")
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(wheretakencode='" + mTreatmentPointCode + "')";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (wheretakencode='" + mTreatmentPointCode + "')";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                + ": " + mTreatmentPointDescription;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region extra patient details

                    DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");
                    foreach (DataRow mDataRow in mDtExtraFields.Rows)
                    {
                        string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                        string mFieldCaption = mDataRow["fieldcaption"].ToString();
                        string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                        string mDataType = mDataRow["datatype"].ToString().Trim();

                        if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mComboBoxEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mComboBoxEdit.Text.Trim() != "")
                                {
                                    if (mExtraFilter.Trim() == "")
                                    {
                                        mExtraFilter = "(patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }
                                    else
                                    {
                                        mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }

                                    mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mComboBoxEdit.Text;
                                }
                                else
                                {
                                    mExtraParameters = mExtraParameters + Environment.NewLine
                                        + mLayoutControlItem.Text.ToUpper()
                                        + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                }
                            }
                            #endregion
                        }
                        else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().ToLower())
                        {
                            CheckEdit mCheckEdit = layoutControl1.GetControlByName("chk" + mFieldName) as CheckEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mCheckEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mExtraFilter.Trim() == "")
                                {
                                    mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }
                                else
                                {
                                    mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }

                                mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + Convert.ToInt16(mCheckEdit.Checked);
                            }
                            #endregion
                        }
                        else
                        {
                            if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
                            {
                                DateEdit mDateEdit = layoutControl1.GetControlByName("txt" + mFieldName) as DateEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mDateEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsNullDate(mDateEdit.EditValue) == false)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDateTime(mDateEdit.EditValue).ToString("d");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mTextEdit.Text.Trim();
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    #endregion

                    mDtData = pMdtReporter.REG_AttendanceList(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mExtraFilter, mExtraParameters);
                }

                #endregion

                #region attendancecountage

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.AttendanceCountAge)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    #region gender
                    if (layoutControl1.GetItemByControl(comboBoxEdit2).Visible == true)
                    {
                        if (comboBoxEdit2.Text.Trim() != "")
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(p.gender='" + comboBoxEdit2.Text + "')";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (p.gender='" + comboBoxEdit2.Text + "')";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text
                                + ": " + comboBoxEdit2.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region weight
                    if (layoutControl1.GetItemByControl(comboBoxEdit3).Visible == true)
                    {
                        if (comboBoxEdit3.Text.Trim() != "" && Program.IsMoney(textEdit2.Text) != false)
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(patientweight " + comboBoxEdit3.Text + " " + textEdit2.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (patientweight " + comboBoxEdit3.Text + " " + textEdit2.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit3).Text
                                + ": " + comboBoxEdit3.Text + " " + textEdit2.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit3).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region temperature
                    if (layoutControl1.GetItemByControl(comboBoxEdit4).Visible == true)
                    {
                        if (comboBoxEdit4.Text.Trim() != "" && Program.IsMoney(textEdit3.Text) != false)
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(patienttemperature " + comboBoxEdit4.Text + " " + textEdit3.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (patienttemperature " + comboBoxEdit4.Text + " " + textEdit3.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit4).Text
                                + ": " + comboBoxEdit4.Text + " " + textEdit3.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit4).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region treatmentpoint
                    if (layoutControl1.GetItemByControl(lookUpEdit1).Visible == true)
                    {
                        string mTreatmentPointCode = "";
                        string mTreatmentPointDescription = "";

                        if (lookUpEdit1.ItemIndex != -1)
                        {
                            mTreatmentPointCode = lookUpEdit1.GetColumnValue("code").ToString().Trim();
                            mTreatmentPointDescription = lookUpEdit1.GetColumnValue("description").ToString().Trim();
                        }

                        if (mTreatmentPointCode.Trim() != "")
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(wheretakencode='" + mTreatmentPointCode + "')";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (wheretakencode='" + mTreatmentPointCode + "')";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                + ": " + mTreatmentPointDescription;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region extra patient details

                    DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");
                    foreach (DataRow mDataRow in mDtExtraFields.Rows)
                    {
                        string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                        string mFieldCaption = mDataRow["fieldcaption"].ToString();
                        string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                        string mDataType = mDataRow["datatype"].ToString().Trim();

                        if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mComboBoxEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mComboBoxEdit.Text.Trim() != "")
                                {
                                    if (mExtraFilter.Trim() == "")
                                    {
                                        mExtraFilter = "(patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }
                                    else
                                    {
                                        mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }

                                    mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mComboBoxEdit.Text;
                                }
                                else
                                {
                                    mExtraParameters = mExtraParameters + Environment.NewLine
                                        + mLayoutControlItem.Text.ToUpper()
                                        + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                }
                            }
                            #endregion
                        }
                        else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().ToLower())
                        {
                            CheckEdit mCheckEdit = layoutControl1.GetControlByName("chk" + mFieldName) as CheckEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mCheckEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mExtraFilter.Trim() == "")
                                {
                                    mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }
                                else
                                {
                                    mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }

                                mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + Convert.ToInt16(mCheckEdit.Checked);
                            }
                            #endregion
                        }
                        else
                        {
                            if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
                            {
                                DateEdit mDateEdit = layoutControl1.GetControlByName("txt" + mFieldName) as DateEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mDateEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsNullDate(mDateEdit.EditValue) == false)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDateTime(mDateEdit.EditValue).ToString("d");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mTextEdit.Text.Trim();
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    #endregion

                    mDtData = pMdtReporter.REG_AttendanceCountAge(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mExtraFilter, mExtraParameters);
                }

                #endregion

                #region AttendanceCountTreatmentPoints

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.AttendanceCountTreatmentPoints)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    #region age
                    if (layoutControl1.GetItemByControl(comboBoxEdit1).Visible == true)
                    {
                        if (comboBoxEdit1.Text.Trim() != "" && Program.IsMoney(textEdit1.Text) != false)
                        {
                            string mPatientAge = pMdtReporter.Age_Formula("p.birthdate", "bookdate", "");
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + comboBoxEdit1.Text + " " + textEdit1.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region gender
                    if (layoutControl1.GetItemByControl(comboBoxEdit2).Visible == true)
                    {
                        if (comboBoxEdit2.Text.Trim() != "")
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(p.gender='" + comboBoxEdit2.Text + "')";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (p.gender='" + comboBoxEdit2.Text + "')";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text
                                + ": " + comboBoxEdit2.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region weight
                    if (layoutControl1.GetItemByControl(comboBoxEdit3).Visible == true)
                    {
                        if (comboBoxEdit3.Text.Trim() != "" && Program.IsMoney(textEdit2.Text) != false)
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(patientweight " + comboBoxEdit3.Text + " " + textEdit2.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (patientweight " + comboBoxEdit3.Text + " " + textEdit2.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit3).Text
                                + ": " + comboBoxEdit3.Text + " " + textEdit2.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit3).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region temperature
                    if (layoutControl1.GetItemByControl(comboBoxEdit4).Visible == true)
                    {
                        if (comboBoxEdit4.Text.Trim() != "" && Program.IsMoney(textEdit3.Text) != false)
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(patienttemperature " + comboBoxEdit4.Text + " " + textEdit3.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (patienttemperature " + comboBoxEdit4.Text + " " + textEdit3.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit4).Text
                                + ": " + comboBoxEdit4.Text + " " + textEdit3.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit4).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region extra patient details

                    DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");
                    foreach (DataRow mDataRow in mDtExtraFields.Rows)
                    {
                        string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                        string mFieldCaption = mDataRow["fieldcaption"].ToString();
                        string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                        string mDataType = mDataRow["datatype"].ToString().Trim();

                        if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mComboBoxEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mComboBoxEdit.Text.Trim() != "")
                                {
                                    if (mExtraFilter.Trim() == "")
                                    {
                                        mExtraFilter = "(patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }
                                    else
                                    {
                                        mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }

                                    mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mComboBoxEdit.Text;
                                }
                                else
                                {
                                    mExtraParameters = mExtraParameters + Environment.NewLine
                                        + mLayoutControlItem.Text.ToUpper()
                                        + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                }
                            }
                            #endregion
                        }
                        else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().ToLower())
                        {
                            CheckEdit mCheckEdit = layoutControl1.GetControlByName("chk" + mFieldName) as CheckEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mCheckEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mExtraFilter.Trim() == "")
                                {
                                    mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }
                                else
                                {
                                    mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }

                                mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + Convert.ToInt16(mCheckEdit.Checked);
                            }
                            #endregion
                        }
                        else
                        {
                            if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
                            {
                                DateEdit mDateEdit = layoutControl1.GetControlByName("txt" + mFieldName) as DateEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mDateEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsNullDate(mDateEdit.EditValue) == false)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDateTime(mDateEdit.EditValue).ToString("d");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mTextEdit.Text.Trim();
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    #endregion

                    mDsData = pMdtReporter.REG_AttendanceCountTreatmentPoints(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mExtraFilter, mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region AttendanceCountCustomerGroups

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.AttendanceCountCustomerGroups)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    #region age
                    if (layoutControl1.GetItemByControl(comboBoxEdit1).Visible == true)
                    {
                        if (comboBoxEdit1.Text.Trim() != "" && Program.IsMoney(textEdit1.Text) != false)
                        {
                            string mPatientAge = pMdtReporter.Age_Formula("p.birthdate", "bookdate", "");
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + comboBoxEdit1.Text + " " + textEdit1.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region gender
                    if (layoutControl1.GetItemByControl(comboBoxEdit2).Visible == true)
                    {
                        if (comboBoxEdit2.Text.Trim() != "")
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(p.gender='" + comboBoxEdit2.Text + "')";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (p.gender='" + comboBoxEdit2.Text + "')";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text
                                + ": " + comboBoxEdit2.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region weight
                    if (layoutControl1.GetItemByControl(comboBoxEdit3).Visible == true)
                    {
                        if (comboBoxEdit3.Text.Trim() != "" && Program.IsMoney(textEdit2.Text) != false)
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(patientweight " + comboBoxEdit3.Text + " " + textEdit2.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (patientweight " + comboBoxEdit3.Text + " " + textEdit2.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit3).Text
                                + ": " + comboBoxEdit3.Text + " " + textEdit2.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit3).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region temperature
                    if (layoutControl1.GetItemByControl(comboBoxEdit4).Visible == true)
                    {
                        if (comboBoxEdit4.Text.Trim() != "" && Program.IsMoney(textEdit3.Text) != false)
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(patienttemperature " + comboBoxEdit4.Text + " " + textEdit3.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (patienttemperature " + comboBoxEdit4.Text + " " + textEdit3.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit4).Text
                                + ": " + comboBoxEdit4.Text + " " + textEdit3.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit4).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region treatmentpoint
                    if (layoutControl1.GetItemByControl(lookUpEdit1).Visible == true)
                    {
                        string mTreatmentPointCode = "";
                        string mTreatmentPointDescription = "";

                        if (lookUpEdit1.ItemIndex != -1)
                        {
                            mTreatmentPointCode = lookUpEdit1.GetColumnValue("code").ToString().Trim();
                            mTreatmentPointDescription = lookUpEdit1.GetColumnValue("description").ToString().Trim();
                        }

                        if (mTreatmentPointCode.Trim() != "")
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(wheretakencode='" + mTreatmentPointCode + "')";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (wheretakencode='" + mTreatmentPointCode + "')";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                + ": " + mTreatmentPointDescription;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region extra patient details

                    DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");
                    foreach (DataRow mDataRow in mDtExtraFields.Rows)
                    {
                        string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                        string mFieldCaption = mDataRow["fieldcaption"].ToString();
                        string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                        string mDataType = mDataRow["datatype"].ToString().Trim();

                        if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mComboBoxEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mComboBoxEdit.Text.Trim() != "")
                                {
                                    if (mExtraFilter.Trim() == "")
                                    {
                                        mExtraFilter = "(patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }
                                    else
                                    {
                                        mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }

                                    mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mComboBoxEdit.Text;
                                }
                                else
                                {
                                    mExtraParameters = mExtraParameters + Environment.NewLine
                                        + mLayoutControlItem.Text.ToUpper()
                                        + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                }
                            }
                            #endregion
                        }
                        else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().ToLower())
                        {
                            CheckEdit mCheckEdit = layoutControl1.GetControlByName("chk" + mFieldName) as CheckEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mCheckEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mExtraFilter.Trim() == "")
                                {
                                    mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }
                                else
                                {
                                    mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }

                                mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + Convert.ToInt16(mCheckEdit.Checked);
                            }
                            #endregion
                        }
                        else
                        {
                            if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
                            {
                                DateEdit mDateEdit = layoutControl1.GetControlByName("txt" + mFieldName) as DateEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mDateEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsNullDate(mDateEdit.EditValue) == false)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDateTime(mDateEdit.EditValue).ToString("d");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mTextEdit.Text.Trim();
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    #endregion

                    mDsData = pMdtReporter.REG_AttendanceCountCustomerGroups(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mExtraFilter, mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region IPDDailyCensusSummary

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.IPDDailyCensusSummary)
                {
                    mDsData = pMdtReporter.REG_IPDDailyCensusSummaryWards(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, "", "");
                    mIsDataSet = true;
                }

                #endregion

                #region IPDDailyCensusDetailed

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.IPDDailyCensusDetailed)
                {
                    string mWardCode = "";
                    string mExtraParameters = "";

                    #region ward
                    if (layoutControl1.GetItemByControl(lookUpEdit1).Visible == true)
                    {
                        string mWardDescription = "";

                        if (lookUpEdit1.ItemIndex != -1)
                        {
                            mWardCode = lookUpEdit1.GetColumnValue("code").ToString().Trim();
                            mWardDescription = lookUpEdit1.GetColumnValue("description").ToString().Trim();
                        }

                        if (mWardCode.Trim() != "")
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                + ": " + mWardDescription;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    if (mWardCode.Trim() == "")
                    {
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    mDsData = pMdtReporter.REG_IPDDailyCensusDetailedWards(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mWardCode, "", mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #endregion

                #region diagnoses

                #region DXTDiagnosesSummary

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.DXTDiagnosesSummary)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    #region age
                    if (layoutControl1.GetItemByControl(comboBoxEdit1).Visible == true)
                    {
                        if (comboBoxEdit1.Text.Trim() != "" && Program.IsMoney(textEdit1.Text) != false)
                        {
                            string mPatientAge = pMdtReporter.Age_Formula("p.birthdate", "transdate", "");
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + comboBoxEdit1.Text + " " + textEdit1.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region extra patient details

                    DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");
                    foreach (DataRow mDataRow in mDtExtraFields.Rows)
                    {
                        string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                        string mFieldCaption = mDataRow["fieldcaption"].ToString();
                        string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                        string mDataType = mDataRow["datatype"].ToString().Trim();

                        if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mComboBoxEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mComboBoxEdit.Text.Trim() != "")
                                {
                                    if (mExtraFilter.Trim() == "")
                                    {
                                        mExtraFilter = "(patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }
                                    else
                                    {
                                        mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }

                                    mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mComboBoxEdit.Text;
                                }
                                else
                                {
                                    mExtraParameters = mExtraParameters + Environment.NewLine
                                        + mLayoutControlItem.Text.ToUpper()
                                        + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                }
                            }
                            #endregion
                        }
                        else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().ToLower())
                        {
                            CheckEdit mCheckEdit = layoutControl1.GetControlByName("chk" + mFieldName) as CheckEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mCheckEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mExtraFilter.Trim() == "")
                                {
                                    mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }
                                else
                                {
                                    mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }

                                mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + Convert.ToInt16(mCheckEdit.Checked);
                            }
                            #endregion
                        }
                        else
                        {
                            if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
                            {
                                DateEdit mDateEdit = layoutControl1.GetControlByName("txt" + mFieldName) as DateEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mDateEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsNullDate(mDateEdit.EditValue) == false)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDateTime(mDateEdit.EditValue).ToString("d");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mTextEdit.Text.Trim();
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    #endregion

                    if (checkEdit1.Checked == true)
                    {
                        mExtraParameters = mExtraParameters + Environment.NewLine
                            + "List Type"
                            + ": Primary Diagnoses Only";
                    }
                    else
                    {
                        mExtraParameters = mExtraParameters + Environment.NewLine
                            + "List Type"
                            + ": All Diagnoses";
                    }

                    switch (comboBoxEdit2.SelectedIndex)
                    {
                        case 1: mReportCode = mReportCode + "_opd"; break;
                        case 2: mReportCode = mReportCode + "_ipd"; break;
                    }

                    mExtraParameters = mExtraParameters + Environment.NewLine
                        + layoutControl1.GetItemByControl(comboBoxEdit2).Text
                        + ": " + comboBoxEdit2.Text;

                    mDsData = pMdtReporter.DXT_DiagnosesSummary(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, checkEdit1.Checked, comboBoxEdit2.SelectedIndex, mExtraFilter, mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region DXTDiagnosesListing

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.DXTDiagnosesListing)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    #region age
                    if (layoutControl1.GetItemByControl(comboBoxEdit1).Visible == true)
                    {
                        if (comboBoxEdit1.Text.Trim() != "" && Program.IsMoney(textEdit1.Text) != false)
                        {
                            string mPatientAge = pMdtReporter.Age_Formula("p.birthdate", "transdate", "");
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + comboBoxEdit1.Text + " " + textEdit1.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region extra patient details

                    DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");
                    foreach (DataRow mDataRow in mDtExtraFields.Rows)
                    {
                        string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                        string mFieldCaption = mDataRow["fieldcaption"].ToString();
                        string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                        string mDataType = mDataRow["datatype"].ToString().Trim();

                        if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mComboBoxEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mComboBoxEdit.Text.Trim() != "")
                                {
                                    if (mExtraFilter.Trim() == "")
                                    {
                                        mExtraFilter = "(patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }
                                    else
                                    {
                                        mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }

                                    mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mComboBoxEdit.Text;
                                }
                                else
                                {
                                    mExtraParameters = mExtraParameters + Environment.NewLine
                                        + mLayoutControlItem.Text.ToUpper()
                                        + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                }
                            }
                            #endregion
                        }
                        else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().ToLower())
                        {
                            CheckEdit mCheckEdit = layoutControl1.GetControlByName("chk" + mFieldName) as CheckEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mCheckEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mExtraFilter.Trim() == "")
                                {
                                    mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }
                                else
                                {
                                    mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }

                                mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + Convert.ToInt16(mCheckEdit.Checked);
                            }
                            #endregion
                        }
                        else
                        {
                            if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
                            {
                                DateEdit mDateEdit = layoutControl1.GetControlByName("txt" + mFieldName) as DateEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mDateEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsNullDate(mDateEdit.EditValue) == false)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDateTime(mDateEdit.EditValue).ToString("d");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mTextEdit.Text.Trim();
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    #endregion

                    mDtData = pMdtReporter.DXT_DiagnosesListing(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mExtraFilter, mExtraParameters);
                }

                #endregion

                #region DXTPatientHistory

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.DXTPatientHistory)
                {
                    TextEdit txtPatientId = (TextEdit)layoutControl1.GetControlByName("txtPatientId");
                    if (txtPatientId.Text.Trim() == "")
                    {
                        Program.Display_Error("Invalid Patient #");
                        txtPatientId.Focus();
                        return;
                    }

                    mDsData = pMdtReporter.DXT_PatientHistory(txtPatientId.Text);
                    mIsDataSet = true;
                }

                #endregion

                #endregion

                #region cashbox

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.CashBox)
                {
                    string mExtraFilter = "";

                    #region user id 
                    if (layoutControl1.GetItemByControl(lookUpEdit1).Visible == true)
                    {
                        string mUserCode = "";
                        string mUserDescription = "";

                        if (lookUpEdit1.ItemIndex != -1)
                        {
                            mUserCode = lookUpEdit1.GetColumnValue("code").ToString().Trim();
                            mUserDescription = lookUpEdit1.GetColumnValue("description").ToString().Trim();
                        }

                        if (mUserCode.Trim() != "")
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "userid='" + mUserCode + "' ";
                            }
                        }
                       
                    }
                    #endregion
                    
                    mDsData = pMdtReporter.BIL_CashBox(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mExtraFilter, "");

                    mIsDataSet = true;
                }

                #endregion

                #region my cashbox

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.MyCashBox)
                {
                    string mExtraFilter = "";
                    if (Program.gCurrentUser != null)
                    {
                        mExtraFilter = "userid='" + Program.gCurrentUser.Code + "' ";
                    }

                    mDsData = pMdtReporter.BIL_CashBox(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mExtraFilter, "");

                    mIsDataSet = true;
                }

                #endregion

                #region debtorslist

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.BILDebtorsList)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";
                    bool mGroups = true;
                    bool mIndividuals = true;
                    DateTime mFromDate = DateTime.Today;
                    DateTime mToDate = DateTime.Today;

                    #region balance
                    if (layoutControl1.GetItemByControl(comboBoxEdit1).Visible == true)
                    {
                        if (comboBoxEdit1.Text.Trim() != "" && Program.IsMoney(textEdit1.Text) != false)
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(balance " + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (balance " + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + comboBoxEdit1.Text + " " + textEdit1.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region accounttype
                    if (layoutControl1.GetItemByControl(comboBoxEdit2).Visible == true)
                    {
                        if (comboBoxEdit2.Text.Trim() != "")
                        {
                            mGroups = false;
                            mIndividuals = false;
                            if (comboBoxEdit2.Text.Trim().ToLower() == "groups")
                            {
                                mGroups = true;
                            }
                            else if (comboBoxEdit2.Text.Trim().ToLower() == "individuals")
                            {
                                mIndividuals = true;
                                //mReportCode = pReportCode + "_individual";
                            }
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text + ": " + comboBoxEdit2.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region FromDate
                    if (layoutControl1.GetItemByControl(dateEdit1).Visible == true)
                    {
                        if (dateEdit1.Text.Trim() != "")
                        {

                            mFromDate = Convert.ToDateTime(dateEdit1.EditValue);
                        }
                       
                    }
                    #endregion

                    #region ToDate
                    if (layoutControl1.GetItemByControl(dateEdit2).Visible == true)
                    {
                        if (dateEdit2.Text.Trim() != "")
                        {

                            mToDate = Convert.ToDateTime(dateEdit2.EditValue);
                        }

                    }
                    #endregion



                    mDsData = pMdtReporter.BIL_DebtorsList(
                        DateTime.Now, mGroups, mIndividuals, mExtraFilter, mExtraParameters, mFromDate, mToDate);
                    mIsDataSet = true;
                }

                #endregion

                #region debtors

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.BillDebtorsList1)
                {

                    bool mGroups = true;
                    bool mIndividuals = true;
                    DateTime mFromDate = DateTime.Today;
                    DateTime mToDate = DateTime.Today;



                    #region FromDate
                    if (layoutControl1.GetItemByControl(dateEdit1).Visible == true)
                    {
                        if (dateEdit1.Text.Trim() != "")
                        {

                            mFromDate = Convert.ToDateTime(dateEdit1.EditValue);
                        }
                        else
                        {
                            MessageBox.Show("Enter start date", "Start date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                    }
                    #endregion

                    #region ToDate
                    if (layoutControl1.GetItemByControl(dateEdit2).Visible == true)
                    {
                        if (dateEdit2.Text.Trim() != "")
                        {

                            mToDate = Convert.ToDateTime(dateEdit2.EditValue);
                        }
                        else
                        {
                            MessageBox.Show("Enter end date", "End date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                    }
                    #endregion



                    mDsData = pMdtReporter.BIL_Debtors(mFromDate, mToDate);
                    mIsDataSet = true;

                }

                #endregion

                #region dailysalessummary

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.BILDailySalesSummary)
                {
                    mDsData = pMdtReporter.BIL_DailySalesSummary(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, "", "");
                    mIsDataSet = true;
                }

                #endregion

                #region BILGroupBillBreakDown

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.BILGroupBillBreakDown)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    if (lookUpEdit1.ItemIndex == -1)
                    {
                        Program.Display_Error("Please select the group");
                        lookUpEdit1.Focus();
                        return;
                    }

                    #region customer group

                    if (lookUpEdit1.ItemIndex != -1)
                    {
                        string mBillingGroupCode = lookUpEdit1.GetColumnValue("code").ToString().Trim();
                        string mBillingGroupDescription = lookUpEdit1.GetColumnValue("description").ToString().Trim();

                        if (mBillingGroupCode.Trim().ToLower() == "individuals")
                        {
                            mExtraFilter = "(billinggroupcode='')";
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                + ": " + mBillingGroupDescription;
                        }
                        else if (mBillingGroupCode.Trim().ToLower() == "")
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                 + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                 + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                        else
                        {
                            mExtraFilter = "(billinggroupcode='" + mBillingGroupCode + "')";
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                + ": " + mBillingGroupDescription;
                        }
                    }
                    else
                    {
                        mExtraParameters = mExtraParameters + Environment.NewLine
                             + layoutControl1.GetItemByControl(lookUpEdit1).Text
                             + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                    }

                    #endregion

                    mExtraParameters = mExtraParameters + Environment.NewLine
                        + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                        + ": " + comboBoxEdit1.Text;

                    mDsData = pMdtReporter.BIL_GroupChargesBreakDown(!checkEdit1.Checked,
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), checkEdit2.Checked, comboBoxEdit1.SelectedIndex, mExtraFilter, mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region BILDebtorsListIndividual

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.BILDebtorsListIndividual)
                {
                    string mBalanceFilter = "";
                    string mExtraParameters = "";

                    #region balance
                    if (layoutControl1.GetItemByControl(comboBoxEdit1).Visible == true)
                    {
                        if (comboBoxEdit1.Text.Trim() != "" && Program.IsMoney(textEdit1.Text) != false)
                        {
                            if (mBalanceFilter.Trim() == "")
                            {
                                mBalanceFilter = "(balance " + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }
                            else
                            {
                                mBalanceFilter = mBalanceFilter + " and (balance " + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + comboBoxEdit1.Text + " " + textEdit1.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    ExtraPatientDetailsFilter mExtraPatientDetails = this.Prepare_ExtraPatientDetails("");

                    if (mExtraPatientDetails.ExtraParameters.Trim() != "")
                    {
                        if (mExtraParameters.Trim() == "")
                        {
                            mExtraParameters = mExtraPatientDetails.ExtraParameters;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + mExtraPatientDetails.ExtraParameters;
                        }
                    }

                    mDsData = pMdtReporter.BIL_DebtorsList(
                        DateTime.Now, mExtraPatientDetails.ExtraFilter, mBalanceFilter, mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region dailysalesdetails

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.BILDailySalesDetails)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    if (checkEdit2.Checked == true)
                    {
                        mReportCode = mReportCode + "_bydept";
                    }

                    #region customer group

                    if (lookUpEdit1.ItemIndex != -1)
                    {
                        string mBillingGroupCode = lookUpEdit1.GetColumnValue("code").ToString().Trim();
                        string mBillingGroupDescription = lookUpEdit1.GetColumnValue("description").ToString().Trim();

                        if (mBillingGroupCode.Trim().ToLower() == "individuals")
                        {
                            mExtraFilter = "(billinggroupcode='')";
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                + ": " + mBillingGroupDescription;
                        }
                        else if (mBillingGroupCode.Trim().ToLower() == "")
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                 + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                 + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                        else
                        {
                            mExtraFilter = "(billinggroupcode='" + mBillingGroupCode + "')";
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                + ": " + mBillingGroupDescription;
                        }
                    }
                    else
                    {
                        mExtraParameters = mExtraParameters + Environment.NewLine
                             + layoutControl1.GetItemByControl(lookUpEdit1).Text
                             + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                    }

                    #endregion

                    #region item group

                    if (lookUpEdit2.ItemIndex != -1)
                    {
                        string mItemGroupCode = lookUpEdit2.GetColumnValue("code").ToString().Trim();
                        string mItemGroupDescription = lookUpEdit2.GetColumnValue("description").ToString().Trim();

                        if (mItemGroupCode.Trim().ToLower() == "")
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                 + layoutControl1.GetItemByControl(lookUpEdit2).Text
                                 + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                        else
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(itemgroupcode='" + mItemGroupCode + "')";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (itemgroupcode='" + mItemGroupCode + "')";
                            }
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit2).Text
                                + ": " + mItemGroupDescription;
                        }
                    }
                    else
                    {
                        mExtraParameters = mExtraParameters + Environment.NewLine
                             + layoutControl1.GetItemByControl(lookUpEdit2).Text
                             + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                    }

                    #endregion

                    #region ward
                    if (layoutControl1.GetItemByControl(lookUpEdit3).Visible == true)
                    {
                        string mWardCode = "";
                        string mWardDescription = "";

                        if (lookUpEdit3.ItemIndex != -1)
                        {
                            mWardCode = lookUpEdit3.GetColumnValue("code").ToString().Trim();
                            mWardDescription = lookUpEdit3.GetColumnValue("description").ToString().Trim();
                        }

                        if (mWardCode.Trim() != "")
                        {
                            if (mWardCode.Trim().ToLower() == "opd")
                            {
                                mWardCode = "";
                            }

                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(wardcode='" + mWardCode + "')";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (wardcode='" + mWardCode + "')";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit3).Text
                                + ": " + mWardDescription;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit3).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    mDsData = pMdtReporter.BIL_DailySalesDetails(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, true, false, checkEdit2.Checked, mExtraFilter, mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region dailyincome

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.BILDailyIncome)
                {
                    mDsData = pMdtReporter.BIL_DailyIncome(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, "", "");
                    mIsDataSet = true;
                }

                #endregion

                #region inventory

                #region IVStockBalance

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.IVStockBalance)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    if (Program.IsNullDate(dateEdit1.EditValue) == true)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                        dateEdit1.Focus();
                        return;
                    }

                    if (lookUpEdit1.ItemIndex == -1)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreDescriptionIsInvalid.ToString());
                        lookUpEdit1.Focus();
                        return;
                    }

                    string mBalanceCondition = comboBoxEdit2.Text.Trim();
                    double mBalanceLimit = 0;

                    if (Program.IsMoney(textEdit1.Text) == false)
                    {
                        mBalanceCondition = "";
                        mBalanceLimit = 0;
                    }
                    else
                    {
                        mBalanceLimit = Convert.ToDouble(textEdit1.Text);
                    }

                    #region categories

                    string mCategories = "";
                    DataTable mDtCategories = (DataTable)gridControl1.DataSource;

                    foreach (DataRow mDataRow in mDtCategories.Rows)
                    {
                        if (Convert.ToBoolean(mDataRow["selected"]) == true)
                        {
                            if (mCategories.Trim() == "")
                            {
                                mCategories = "'" + mDataRow["code"].ToString().Trim() + "'";
                            }
                            else
                            {
                                mCategories = mCategories + ",'" + mDataRow["code"].ToString().Trim() + "'";
                            }
                        }
                    }

                    if (mCategories.Trim() == "")
                    {
                        mCategories = "1=2";
                    }
                    else
                    {
                        mCategories = "departmentcode in (" + mCategories + ")";
                    }

                    #endregion

                    mExtraFilter = mCategories;

                    mDtData = pMdtReporter.IV_StockBalance(lookUpEdit1.GetColumnValue("code").ToString(),
                        lookUpEdit1.GetColumnValue("description").ToString(), Convert.ToDateTime(dateEdit1.EditValue),
                        mBalanceCondition, mBalanceLimit, mExtraFilter, mExtraParameters);
                    mIsDataSet = false;
                }

                #endregion

                #region IVGRN

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.IVGRN)
                {
                    LookUpEdit cboStore = layoutControl1.GetControlByName("cboStore") as LookUpEdit;
                    DateEdit txtDateFrom = layoutControl1.GetControlByName("txtDateFrom") as DateEdit;
                    DateEdit txtDateTo = layoutControl1.GetControlByName("txtDateTo") as DateEdit;
                    RadioGroup radGroupBy = layoutControl1.GetControlByName("radGroupBy") as RadioGroup;
                    CheckEdit chkShowExpiryDates = layoutControl1.GetControlByName("chkShowExpiryDates") as CheckEdit;

                    //validate store
                    if (Program.IsNullDate(dateEdit1.EditValue) == true)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                        dateEdit1.Focus();
                        return;
                    }

                    if (lookUpEdit1.ItemIndex == -1)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreDescriptionIsInvalid.ToString());
                        lookUpEdit1.Focus();
                        return;
                    }

                    //validate dates
                    if (Program.IsNullDate(txtDateFrom.EditValue) == true)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                        txtDateFrom.Focus();
                        return;
                    }

                    if (Program.IsNullDate(txtDateTo.EditValue) == true)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                        txtDateTo.Focus();
                        return;
                    }

                    if (Convert.ToDateTime(txtDateTo.EditValue) < Convert.ToDateTime(txtDateFrom.EditValue))
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateRangeIsInvalid.ToString());
                        txtDateTo.Focus();
                        return;
                    }

                    bool mGroupBySupplier = false;
                    bool mGroupByDeliveryNo = false;

                    mReportCode = pReportCode;

                    switch (radGroupBy.SelectedIndex)
                    {
                        case 0:
                            {
                                if (Convert.ToBoolean(chkShowExpiryDates.Checked) == true)
                                {
                                    mReportCode = mReportCode + "_expirydates";
                                }
                            }
                            break;
                        case 1:
                            {
                                mReportCode = mReportCode + "_supplier";

                                if (Convert.ToBoolean(chkShowExpiryDates.Checked) == true)
                                {
                                    mReportCode = mReportCode + "expirydates";
                                }

                                mGroupBySupplier = true;
                            }
                            break;
                        case 2:
                            {
                                mReportCode = mReportCode + "_delivery";

                                if (Convert.ToBoolean(chkShowExpiryDates.Checked) == true)
                                {
                                    mReportCode = mReportCode + "expirydates";
                                }

                                mGroupByDeliveryNo = true;
                            }
                            break;
                    }

                    mDsData = pMdtReporter.IV_GRN(cboStore.GetColumnValue("code").ToString(),
                                                    cboStore.GetColumnValue("description").ToString(),
                                                    Convert.ToDateTime(dateEdit1.EditValue),
                                                    Convert.ToDateTime(dateEdit2.EditValue),
                                                    mGroupBySupplier,
                                                    mGroupByDeliveryNo,
                                                    Convert.ToBoolean(chkShowExpiryDates.Checked),
                                                    "",
                                                    "");
                    mIsDataSet = true;
                }

                #endregion

                #region IVGIN

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.IVGIN)
                {
                    LookUpEdit cboStore = layoutControl1.GetControlByName("cboStore") as LookUpEdit;
                    DateEdit txtDateFrom = layoutControl1.GetControlByName("txtDateFrom") as DateEdit;
                    DateEdit txtDateTo = layoutControl1.GetControlByName("txtDateTo") as DateEdit;
                    RadioGroup radGroupBy = layoutControl1.GetControlByName("radGroupBy") as RadioGroup;
                    CheckEdit chkShowExpiryDates = layoutControl1.GetControlByName("chkShowExpiryDates") as CheckEdit;

                    //validate store
                    if (Program.IsNullDate(dateEdit1.EditValue) == true)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                        dateEdit1.Focus();
                        return;
                    }

                    if (lookUpEdit1.ItemIndex == -1)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreDescriptionIsInvalid.ToString());
                        lookUpEdit1.Focus();
                        return;
                    }

                    //validate dates
                    if (Program.IsNullDate(txtDateFrom.EditValue) == true)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                        txtDateFrom.Focus();
                        return;
                    }

                    if (Program.IsNullDate(txtDateTo.EditValue) == true)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                        txtDateTo.Focus();
                        return;
                    }

                    if (Convert.ToDateTime(txtDateTo.EditValue) < Convert.ToDateTime(txtDateFrom.EditValue))
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateRangeIsInvalid.ToString());
                        txtDateTo.Focus();
                        return;
                    }

                    bool mGroupByCustomer = false;
                    bool mGroupByDeliveryNo = false;

                    mReportCode = pReportCode;

                    switch (radGroupBy.SelectedIndex)
                    {
                        case 0:
                            {
                                if (Convert.ToBoolean(chkShowExpiryDates.Checked) == true)
                                {
                                    mReportCode = mReportCode + "_expirydates";
                                }
                            }
                            break;
                        case 1:
                            {
                                mReportCode = mReportCode + "_customer";

                                if (Convert.ToBoolean(chkShowExpiryDates.Checked) == true)
                                {
                                    mReportCode = mReportCode + "expirydates";
                                }

                                mGroupByCustomer = true;
                            }
                            break;
                        case 2:
                            {
                                mReportCode = mReportCode + "_delivery";

                                if (Convert.ToBoolean(chkShowExpiryDates.Checked) == true)
                                {
                                    mReportCode = mReportCode + "expirydates";
                                }

                                mGroupByDeliveryNo = true;
                            }
                            break;
                    }

                    mDsData = pMdtReporter.IV_GIN(cboStore.GetColumnValue("code").ToString(),
                                                    cboStore.GetColumnValue("description").ToString(),
                                                    Convert.ToDateTime(dateEdit1.EditValue),
                                                    Convert.ToDateTime(dateEdit2.EditValue),
                                                    mGroupByCustomer,
                                                    mGroupByDeliveryNo,
                                                    Convert.ToBoolean(chkShowExpiryDates.Checked),
                                                    "",
                                                    "");
                    mIsDataSet = true;
                }

                #endregion

                #region producthistory

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.IVProductHistory)
                {
                    LookUpEdit cboStore = layoutControl1.GetControlByName("cboStore") as LookUpEdit;
                    DateEdit txtDateFrom = layoutControl1.GetControlByName("txtDateFrom") as DateEdit;
                    DateEdit txtDateTo = layoutControl1.GetControlByName("txtDateTo") as DateEdit;

                    if (cboStore.ItemIndex == -1)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreDescriptionIsInvalid.ToString());
                        cboStore.Focus();
                        return;
                    }

                    if (checkEdit1.Checked == false)
                    {
                        if (Program.IsNullDate(txtDateFrom.EditValue) == true)
                        {
                            Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                            txtDateFrom.Focus();
                            return;
                        }

                        if (Program.IsNullDate(txtDateTo.EditValue) == true)
                        {
                            Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                            txtDateTo.Focus();
                            return;
                        }

                        if (Convert.ToDateTime(txtDateTo.EditValue) < Convert.ToDateTime(txtDateFrom.EditValue))
                        {
                            Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateRangeIsInvalid.ToString());
                            txtDateTo.Focus();
                            return;
                        }
                    }

                    mDsData = pMdtReporter.IV_ProductHistory(
                        cboStore.GetColumnValue("code").ToString(),
                        cboStore.GetColumnValue("description").ToString(),
                        textEditSearch.Text,
                        Convert.ToDateTime(txtDateFrom.EditValue),
                        Convert.ToDateTime(txtDateTo.EditValue), 
                        !checkEdit1.Checked,
                        "","");

                    mIsDataSet = true;
                }

                #endregion

                #region IVPriceList

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.IVPriceList)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    if (lookUpEdit1.ItemIndex == -1)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreDescriptionIsInvalid.ToString());
                        lookUpEdit1.Focus();
                        return;
                    }

                    #region categories

                    string mCategories = "";
                    DataTable mDtCategories = (DataTable)gridControl1.DataSource;

                    foreach (DataRow mDataRow in mDtCategories.Rows)
                    {
                        if (Convert.ToBoolean(mDataRow["selected"]) == true)
                        {
                            if (mCategories.Trim() == "")
                            {
                                mCategories = "'" + mDataRow["code"].ToString().Trim() + "'";
                            }
                            else
                            {
                                mCategories = mCategories + ",'" + mDataRow["code"].ToString().Trim() + "'";
                            }
                        }
                    }

                    if (mCategories.Trim() == "")
                    {
                        mCategories = "1=2";
                    }
                    else
                    {
                        mCategories = "departmentcode in (" + mCategories + ")";
                    }

                    #endregion

                    mExtraFilter = mCategories;

                    mDtData = pMdtReporter.IV_PriceList(lookUpEdit1.GetColumnValue("code").ToString(),
                        lookUpEdit1.GetColumnValue("description").ToString(), mExtraFilter, mExtraParameters);
                    mIsDataSet = false;
                }

                #endregion

                #endregion

                #region laboratory

                #region LABPatientTestsCount

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.LABPatientTestsCount)
                {
                    
                    mDsData = pMdtReporter.LAB_PatientTestsCount(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, "", "");
                    mIsDataSet = true;
                }

                #endregion

                #region LABCountByResult

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.LABCountByResult)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    #region age
                    if (layoutControl1.GetItemByControl(comboBoxEdit1).Visible == true)
                    {
                        if (comboBoxEdit1.Text.Trim() != "" && Program.IsMoney(textEdit1.Text) != false)
                        {
                            string mPatientAge = pMdtReporter.Age_Formula("p.birthdate", "transdate", "");
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + comboBoxEdit1.Text + " " + textEdit1.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region extra patient details

                    DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");
                    foreach (DataRow mDataRow in mDtExtraFields.Rows)
                    {
                        string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                        string mFieldCaption = mDataRow["fieldcaption"].ToString();
                        string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                        string mDataType = mDataRow["datatype"].ToString().Trim();

                        if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mComboBoxEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mComboBoxEdit.Text.Trim() != "")
                                {
                                    if (mExtraFilter.Trim() == "")
                                    {
                                        mExtraFilter = "(patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }
                                    else
                                    {
                                        mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }

                                    mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mComboBoxEdit.Text;
                                }
                                else
                                {
                                    mExtraParameters = mExtraParameters + Environment.NewLine
                                        + mLayoutControlItem.Text.ToUpper()
                                        + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                }
                            }
                            #endregion
                        }
                        else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().ToLower())
                        {
                            CheckEdit mCheckEdit = layoutControl1.GetControlByName("chk" + mFieldName) as CheckEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mCheckEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mExtraFilter.Trim() == "")
                                {
                                    mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }
                                else
                                {
                                    mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }

                                mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + Convert.ToInt16(mCheckEdit.Checked);
                            }
                            #endregion
                        }
                        else
                        {
                            if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
                            {
                                DateEdit mDateEdit = layoutControl1.GetControlByName("txt" + mFieldName) as DateEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mDateEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsNullDate(mDateEdit.EditValue) == false)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDateTime(mDateEdit.EditValue).ToString("d");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mTextEdit.Text.Trim();
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    #endregion

                    mDsData = pMdtReporter.LAB_CountByResult(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mExtraFilter, mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region LABListingByResult

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.LABListingByResult)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    #region age
                    if (layoutControl1.GetItemByControl(comboBoxEdit1).Visible == true)
                    {
                        if (comboBoxEdit1.Text.Trim() != "" && Program.IsMoney(textEdit1.Text) != false)
                        {
                            string mPatientAge = pMdtReporter.Age_Formula("p.birthdate", "transdate", "");
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + comboBoxEdit1.Text + " " + textEdit1.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region extra patient details

                    DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");
                    foreach (DataRow mDataRow in mDtExtraFields.Rows)
                    {
                        string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                        string mFieldCaption = mDataRow["fieldcaption"].ToString();
                        string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                        string mDataType = mDataRow["datatype"].ToString().Trim();

                        if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mComboBoxEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mComboBoxEdit.Text.Trim() != "")
                                {
                                    if (mExtraFilter.Trim() == "")
                                    {
                                        mExtraFilter = "(patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }
                                    else
                                    {
                                        mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }

                                    mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mComboBoxEdit.Text;
                                }
                                else
                                {
                                    mExtraParameters = mExtraParameters + Environment.NewLine
                                        + mLayoutControlItem.Text.ToUpper()
                                        + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                }
                            }
                            #endregion
                        }
                        else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().ToLower())
                        {
                            CheckEdit mCheckEdit = layoutControl1.GetControlByName("chk" + mFieldName) as CheckEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mCheckEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mExtraFilter.Trim() == "")
                                {
                                    mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }
                                else
                                {
                                    mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }

                                mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + Convert.ToInt16(mCheckEdit.Checked);
                            }
                            #endregion
                        }
                        else
                        {
                            if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
                            {
                                DateEdit mDateEdit = layoutControl1.GetControlByName("txt" + mFieldName) as DateEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mDateEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsNullDate(mDateEdit.EditValue) == false)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDateTime(mDateEdit.EditValue).ToString("d");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mTextEdit.Text.Trim();
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    #endregion

                    mDtData = pMdtReporter.LAB_ListingByResult(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mExtraFilter, mExtraParameters);
                    mIsDataSet = false;
                }

                #endregion

                #region LABCountMonthly

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.LABCountMonthly)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    if (Program.IsNumeric(textEdit1.Text) == false)
                    {
                        Program.Display_Error("Invalid entry for Year");
                        textEdit1.Focus();
                        return;
                    }

                    if (comboBoxEdit1.SelectedIndex == -1)
                    {
                        Program.Display_Error("Invalid month selection");
                        comboBoxEdit1.Focus();
                        return;
                    }

                    if (comboBoxEdit2.SelectedIndex == -1)
                    {
                        Program.Display_Error("Invalid month selection");
                        comboBoxEdit2.Focus();
                        return;
                    }

                    #region lab tests

                    DataTable mDtTests = new DataTable("selectedtests");
                    mDtTests.Columns.Add("code", typeof(System.String));

                    foreach (DataRow mDataRow in ((DataTable)gridControl1.DataSource).Rows)
                    {
                        if (Convert.ToBoolean(mDataRow["selected"]) == true)
                        {
                            DataRow mNewRow = mDtTests.NewRow();
                            mNewRow["code"] = mDataRow["code"];
                            mDtTests.Rows.Add(mNewRow);
                            mDtTests.AcceptChanges();
                        }
                    }

                    if (mDtTests.Rows.Count == 0)
                    {
                        Program.Display_Error("Please select at least one Lab Test and try again", false);
                        return;
                    }

                    #endregion

                    mDsData = pMdtReporter.LAB_CountMonthly(
                        Convert.ToInt32(textEdit1.Text),
                        comboBoxEdit1.SelectedIndex + 1,
                        comboBoxEdit2.SelectedIndex + 1,
                        mDtTests,
                        mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region Lab test count by user

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.LABPatientCountByLabTechnician)
                {
                    string mExtraFilter = "";

                    #region Lab Technician Id
                    if (layoutControl1.GetItemByControl(lookUpEdit1).Visible == true)
                    {
                        string mLabTechnicianCode = "";
                        string mLabTechnicianDescription = "";

                        if (lookUpEdit1.ItemIndex != -1)
                        {
                            mLabTechnicianCode = lookUpEdit1.GetColumnValue("code").ToString().Trim();
                            mLabTechnicianDescription = lookUpEdit1.GetColumnValue("description").ToString().Trim();
                        }

                        if (mLabTechnicianCode.Trim() != "")
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "labtechniciancode='" + mLabTechnicianCode + "' ";
                            }
                        }

                    }
                    #endregion
                   
                    mDsData = pMdtReporter.LAB_CountByLabTechnician(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mExtraFilter, "");

                    mIsDataSet = true;
                }

                #endregion
                #endregion

                #region rch

                #region attendancelist

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.RCH_AttendanceList)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";
                    int mServiceType = 0;

                    #region service type

                    LookUpEdit cboServiceType = (LookUpEdit)layoutControl1.GetControlByName("cboServiceType");
                    if (cboServiceType.ItemIndex <= 0)
                    {
                        Program.Display_Error("Invalid service type selection");
                        cboServiceType.Focus();
                        return;
                    }

                    mExtraParameters = mExtraParameters + Environment.NewLine
                        + layoutControl1.GetItemByControl(cboServiceType).Text
                        + ": " + cboServiceType.GetColumnValue("description").ToString();

                    string mServiceTypeFieldName = cboServiceType.GetColumnValue("code").ToString();

                    if (mServiceTypeFieldName.ToLower() == AfyaPro_Types.clsEnums.RCHServices.familyplanning.ToString().ToLower())
                    {
                        mServiceType = (int)AfyaPro_Types.clsEnums.RCHServices.familyplanning;
                    }
                    else if (mServiceTypeFieldName.ToLower() == AfyaPro_Types.clsEnums.RCHServices.antenatalcare.ToString().ToLower())
                    {
                        mServiceType = (int)AfyaPro_Types.clsEnums.RCHServices.antenatalcare;
                    }
                    else if (mServiceTypeFieldName.ToLower() == AfyaPro_Types.clsEnums.RCHServices.postnatalcare.ToString().ToLower())
                    {
                        mServiceType = (int)AfyaPro_Types.clsEnums.RCHServices.postnatalcare;
                    }
                    else if (mServiceTypeFieldName.ToLower() == AfyaPro_Types.clsEnums.RCHServices.childrenhealth.ToString().ToLower())
                    {
                        mServiceType = (int)AfyaPro_Types.clsEnums.RCHServices.childrenhealth;
                    }
                    else if (mServiceTypeFieldName.ToLower() == AfyaPro_Types.clsEnums.RCHServices.vaccinations.ToString().ToLower())
                    {
                        mServiceType = (int)AfyaPro_Types.clsEnums.RCHServices.vaccinations;
                    }

                    #endregion

                    #region age
                    if (layoutControl1.GetItemByControl(comboBoxEdit1).Visible == true)
                    {
                        if (comboBoxEdit1.Text.Trim() != "" && Program.IsMoney(textEdit1.Text) != false)
                        {
                            string mPatientAge = pMdtReporter.Age_Formula("p.birthdate", "bookdate", "");
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + comboBoxEdit1.Text + " " + textEdit1.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region gender
                    if (layoutControl1.GetItemByControl(comboBoxEdit2).Visible == true)
                    {
                        if (comboBoxEdit2.Text.Trim() != "")
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(p.gender='" + comboBoxEdit2.Text + "')";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (p.gender='" + comboBoxEdit2.Text + "')";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text + ": " + comboBoxEdit2.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region extra patient details

                    DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");
                    foreach (DataRow mDataRow in mDtExtraFields.Rows)
                    {
                        string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                        string mFieldCaption = mDataRow["fieldcaption"].ToString();
                        string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                        string mDataType = mDataRow["datatype"].ToString().Trim();

                        if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mComboBoxEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mComboBoxEdit.Text.Trim() != "")
                                {
                                    if (mExtraFilter.Trim() == "")
                                    {
                                        mExtraFilter = "(patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }
                                    else
                                    {
                                        mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }

                                    mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mComboBoxEdit.Text;
                                }
                                else
                                {
                                    mExtraParameters = mExtraParameters + Environment.NewLine
                                        + mLayoutControlItem.Text.ToUpper()
                                        + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                }
                            }
                            #endregion
                        }
                        else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().ToLower())
                        {
                            CheckEdit mCheckEdit = layoutControl1.GetControlByName("chk" + mFieldName) as CheckEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mCheckEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mExtraFilter.Trim() == "")
                                {
                                    mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }
                                else
                                {
                                    mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }

                                mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + Convert.ToInt16(mCheckEdit.Checked);
                            }
                            #endregion
                        }
                        else
                        {
                            if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
                            {
                                DateEdit mDateEdit = layoutControl1.GetControlByName("txt" + mFieldName) as DateEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mDateEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsNullDate(mDateEdit.EditValue) == false)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDateTime(mDateEdit.EditValue).ToString("d");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mTextEdit.Text.Trim();
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    #endregion

                    mDtData = pMdtReporter.RCH_AttendanceList(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mServiceType, mExtraFilter, mExtraParameters);
                }

                #endregion

                #region vaccinationscount

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.RCH_VaccinationCount)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";
                    int mServiceType = -1;

                    #region service type

                    LookUpEdit cboServiceType = (LookUpEdit)layoutControl1.GetControlByName("cboServiceType");

                    string mServiceTypeFieldName = cboServiceType.ItemIndex <= 0 ? "" : cboServiceType.GetColumnValue("code").ToString();
                    string mServiceTypeDescription = cboServiceType.ItemIndex <= 0 ? "All Services" : cboServiceType.GetColumnValue("description").ToString();

                    mExtraParameters = mExtraParameters + Environment.NewLine
                        + layoutControl1.GetItemByControl(cboServiceType).Text
                        + ": " + mServiceTypeDescription;

                    if (mServiceTypeFieldName.ToLower() == AfyaPro_Types.clsEnums.RCHServices.familyplanning.ToString().ToLower())
                    {
                        mServiceType = (int)AfyaPro_Types.clsEnums.RCHServices.familyplanning;
                    }
                    else if (mServiceTypeFieldName.ToLower() == AfyaPro_Types.clsEnums.RCHServices.antenatalcare.ToString().ToLower())
                    {
                        mServiceType = (int)AfyaPro_Types.clsEnums.RCHServices.antenatalcare;
                    }
                    else if (mServiceTypeFieldName.ToLower() == AfyaPro_Types.clsEnums.RCHServices.postnatalcare.ToString().ToLower())
                    {
                        mServiceType = (int)AfyaPro_Types.clsEnums.RCHServices.postnatalcare;
                    }
                    else if (mServiceTypeFieldName.ToLower() == AfyaPro_Types.clsEnums.RCHServices.childrenhealth.ToString().ToLower())
                    {
                        mServiceType = (int)AfyaPro_Types.clsEnums.RCHServices.childrenhealth;
                    }
                    else if (mServiceTypeFieldName.ToLower() == AfyaPro_Types.clsEnums.RCHServices.vaccinations.ToString().ToLower())
                    {
                        mServiceType = (int)AfyaPro_Types.clsEnums.RCHServices.vaccinations;
                    }

                    #endregion

                    #region age
                    if (layoutControl1.GetItemByControl(comboBoxEdit1).Visible == true)
                    {
                        TextEdit txtAgeYears = (TextEdit)layoutControl1.GetControlByName("txtAgeLimit");
                        TextEdit txtAgeMonths = (TextEdit)layoutControl1.GetControlByName("txtAgeMonths");

                        decimal mAgeYears = 0;
                        decimal mAgeMonths = 0;
                        decimal mAge = 0;

                        if (Program.IsMoney(txtAgeYears.Text) == true)
                        {
                            mAgeYears = Convert.ToDecimal(txtAgeYears.Text);
                        }

                        if (Program.IsMoney(txtAgeMonths.Text) == true)
                        {
                            mAgeMonths = Convert.ToDecimal(txtAgeMonths.Text);
                        }

                        mAge = mAgeYears + (mAgeMonths / 12);

                        if (comboBoxEdit1.Text.Trim() != "" && Program.IsMoney(textEdit1.Text) != false)
                        {
                            string mPatientAge = pMdtReporter.Age_Formula("p.birthdate", "bookdate", "");
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(" + mPatientAge + comboBoxEdit1.Text + " " + mAge + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (" + mPatientAge + comboBoxEdit1.Text + " " + mAge + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + comboBoxEdit1.Text + " " + txtAgeYears.Text + " Years, " + txtAgeMonths.Text + " Months";
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region gender
                    if (layoutControl1.GetItemByControl(comboBoxEdit2).Visible == true)
                    {
                        if (comboBoxEdit2.Text.Trim() != "")
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(p.gender='" + comboBoxEdit2.Text + "')";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (p.gender='" + comboBoxEdit2.Text + "')";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text + ": " + comboBoxEdit2.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region extra patient details

                    DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");
                    foreach (DataRow mDataRow in mDtExtraFields.Rows)
                    {
                        string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                        string mFieldCaption = mDataRow["fieldcaption"].ToString();
                        string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                        string mDataType = mDataRow["datatype"].ToString().Trim();

                        if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mComboBoxEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mComboBoxEdit.Text.Trim() != "")
                                {
                                    if (mExtraFilter.Trim() == "")
                                    {
                                        mExtraFilter = "(patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }
                                    else
                                    {
                                        mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }

                                    mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mComboBoxEdit.Text;
                                }
                                else
                                {
                                    mExtraParameters = mExtraParameters + Environment.NewLine
                                        + mLayoutControlItem.Text.ToUpper()
                                        + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                }
                            }
                            #endregion
                        }
                        else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().ToLower())
                        {
                            CheckEdit mCheckEdit = layoutControl1.GetControlByName("chk" + mFieldName) as CheckEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mCheckEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mExtraFilter.Trim() == "")
                                {
                                    mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }
                                else
                                {
                                    mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }

                                mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + Convert.ToInt16(mCheckEdit.Checked);
                            }
                            #endregion
                        }
                        else
                        {
                            if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
                            {
                                DateEdit mDateEdit = layoutControl1.GetControlByName("txt" + mFieldName) as DateEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mDateEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsNullDate(mDateEdit.EditValue) == false)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDateTime(mDateEdit.EditValue).ToString("d");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mTextEdit.Text.Trim();
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    #endregion

                    mDtData = pMdtReporter.RCH_VaccinationsCount(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mServiceType, mExtraFilter, mExtraParameters);
                }

                #endregion

                #region RCH_FPlanMethodsCount

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.RCH_FPlanMethodsCount)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    #region age
                    if (layoutControl1.GetItemByControl(comboBoxEdit1).Visible == true)
                    {
                        TextEdit txtAgeYears = (TextEdit)layoutControl1.GetControlByName("txtAgeLimit");
                        TextEdit txtAgeMonths = (TextEdit)layoutControl1.GetControlByName("txtAgeMonths");

                        decimal mAgeYears = 0;
                        decimal mAgeMonths = 0;
                        decimal mAge = 0;

                        if (Program.IsMoney(txtAgeYears.Text) == true)
                        {
                            mAgeYears = Convert.ToDecimal(txtAgeYears.Text);
                        }

                        if (Program.IsMoney(txtAgeMonths.Text) == true)
                        {
                            mAgeMonths = Convert.ToDecimal(txtAgeMonths.Text);
                        }

                        mAge = mAgeYears + (mAgeMonths / 12);

                        if (comboBoxEdit1.Text.Trim() != "" && Program.IsMoney(textEdit1.Text) != false)
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(patientage " + comboBoxEdit1.Text + " " + mAge + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (patientage " + comboBoxEdit1.Text + " " + mAge + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + comboBoxEdit1.Text + " " + txtAgeYears.Text + " Years, " + txtAgeMonths.Text + " Months";
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region gender
                    if (layoutControl1.GetItemByControl(comboBoxEdit2).Visible == true)
                    {
                        if (comboBoxEdit2.Text.Trim() != "")
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(gender='" + comboBoxEdit2.Text + "')";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (gender='" + comboBoxEdit2.Text + "')";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text + ": " + comboBoxEdit2.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    mDtData = pMdtReporter.RCH_FPlanMethodsCount(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mExtraFilter, mExtraParameters);
                }

                #endregion

                #region RCH_AntenatalAtt

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.RCH_AntenatalAtt)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    mDsData = pMdtReporter.RCH_AntenatalAtt(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mExtraFilter, mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region RCH_PostnatalAtt

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.RCH_PostnatalAtt)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    mDsData = pMdtReporter.RCH_PostnatalAtt(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mExtraFilter, mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region RCHANCMonthlySummary

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.RCH_ANCMonthlySummary)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    if (comboBoxEdit2.SelectedIndex == -1)
                    {
                        Program.Display_Error("Invalid entry for Year");
                        comboBoxEdit2.Focus();
                        return;
                    }

                    if (comboBoxEdit1.SelectedIndex == -1)
                    {
                        Program.Display_Error("Invalid month selection");
                        comboBoxEdit1.Focus();
                        return;
                    }

                    mDsData = pMdtReporter.RCH_ANCMonthlySummary(
                        Convert.ToInt32(comboBoxEdit2.Text),
                        comboBoxEdit1.Text,
                        mExtraFilter,
                        mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region RCH_ObstetricMonthlySummary

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.RCH_ObstetricMonthlyReport)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    if (comboBoxEdit2.SelectedIndex == -1)
                    {
                        Program.Display_Error("Invalid entry for Year");
                        comboBoxEdit2.Focus();
                        return;
                    }

                    if (comboBoxEdit1.SelectedIndex == -1)
                    {
                        Program.Display_Error("Invalid month selection");
                        comboBoxEdit1.Focus();
                        return;
                    }

                    mDsData = pMdtReporter.RCH_ObstetricMonthlySummary(
                        Convert.ToInt32(comboBoxEdit2.Text),
                        comboBoxEdit1.Text,
                        mExtraFilter,
                        mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #endregion

                #region ctc/art

                #region CTCAttendanceCountAge

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.CTCAttendanceCountAge)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    DateEdit txtDateFrom = (DateEdit)layoutControl1.GetControlByName("txtDateFrom");
                    DateEdit txtDateTo = (DateEdit)layoutControl1.GetControlByName("txtDateTo");

                    mDsData = pMdtReporter.CTC_AttendanceCountAge(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), mExtraFilter, mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region CTCAttendanceCD4T

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.CTCAttendanceCD4T)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    DateEdit txtDateFrom = (DateEdit)layoutControl1.GetControlByName("txtDateFrom");
                    DateEdit txtDateTo = (DateEdit)layoutControl1.GetControlByName("txtDateTo");

                    mDsData = pMdtReporter.CTC_AttendanceCD4AgeT(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), mExtraFilter, mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                

                #region CTCTestingAndCounselingMonthlySummary

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.CTCTestingAndCounseling)
                {
                    if (fillCTCTestingAndCounselingMonthlySummary(ref mDsData) == false)
                        return;
                    mIsDataSet = true;
                }
                #endregion

                #region CTCExposedChildrenMonthlyReport

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.CTCExposedChildrenMonthlyReport)
                {
                    if (fillCTCExposedChildrenMonthlyReport(ref mDsData) == false)
                        return;
                    mIsDataSet = true;
                }
                #endregion

                #region CTCDNAPCRMonthlyReport

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.CTCDNAPCRMonthlyReport)
                {
                    if (fillCTCDNAPCRMonthlyReport(ref mDsData) == false)
                        return;
                    mIsDataSet = true;
                }

                #endregion

                #region CTC_QuarterlySupervision
                mFunctionName = mFunctionName + "  CTC_QuarterlySupervision";
                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.CTCARTClinicQuarterlySupervision)
                {

                    string mFromDate = "";
                    string mToDate = "";

                    string mFirstMonthInQuarter = "";
                    string mSecondMonthInQuarter = "";
                    string mThirdMonthInQuarter = "";

                    string mFirstMonth = "";
                    string mSecondMonth = "";
                    string mThirdMonth = "";

                    DateTime mQuarterStartDate = Convert.ToDateTime("01/01/1900");
                    DateTime mQuarterEndDate = Convert.ToDateTime("01/01/1900");

                    if (Program.IsNumeric(comboBoxEdit1.Text) == false)
                    {
                        Program.Display_Error("Invalid entry for year");
                        textEdit1.Focus();
                        return;
                    }

                    if (comboBoxEdit2.SelectedIndex == -1)
                    {
                        Program.Display_Error("Invalid Entry for Quarter");
                        comboBoxEdit1.Focus();
                        return;
                    }

                    if (comboBoxEdit2.Text == "Q1")
                    {
                        mFromDate = "01/01";
                        mToDate = "03/31";

                        mQuarterStartDate = Convert.ToDateTime(comboBoxEdit1.Text + "/01/01");
                        mQuarterEndDate = Convert.ToDateTime(comboBoxEdit1.Text + "/03/31");

                        mFirstMonthInQuarter = "January " + comboBoxEdit1.Text;
                        mSecondMonthInQuarter = "February " + comboBoxEdit1.Text;
                        mThirdMonthInQuarter = "March " + comboBoxEdit1.Text;

                        mFirstMonth = "January";
                        mSecondMonth = "February";
                        mThirdMonth = "March";
                    }

                    if (comboBoxEdit2.Text == "Q2")
                    {
                        mFromDate = "04/01";
                        mToDate = "06/30";

                        mQuarterStartDate = Convert.ToDateTime(comboBoxEdit1.Text + "/04/01");
                        mQuarterEndDate = Convert.ToDateTime(comboBoxEdit1.Text + "/06/30");

                        mFirstMonthInQuarter = "April " + comboBoxEdit1.Text;
                        mSecondMonthInQuarter = "May " + comboBoxEdit1.Text;
                        mThirdMonthInQuarter = "June " + comboBoxEdit1.Text;

                        mFirstMonth = "April";
                        mSecondMonth = "May";
                        mThirdMonth = "June";

                    }

                    if (comboBoxEdit2.Text == "Q3")
                    {
                        mFromDate = "07/01";
                        mToDate = "09/30";

                        mQuarterStartDate = Convert.ToDateTime(comboBoxEdit1.Text + "/07/01");
                        mQuarterEndDate = Convert.ToDateTime(comboBoxEdit1.Text + "/09/30");

                        mFirstMonthInQuarter = "July " + comboBoxEdit1.Text;
                        mSecondMonthInQuarter = "August " + comboBoxEdit1.Text;
                        mThirdMonthInQuarter = "September " + comboBoxEdit1.Text;

                        mFirstMonth = "July";
                        mSecondMonth = "August";
                        mThirdMonth = "September";

                    }

                    if (comboBoxEdit2.Text == "Q4")
                    {
                        mFromDate = "10/01";
                        mToDate = "12/31";

                        mQuarterStartDate = Convert.ToDateTime(comboBoxEdit1.Text + "/10/01");
                        mQuarterEndDate = Convert.ToDateTime(comboBoxEdit1.Text + "/12/31");

                        mFirstMonthInQuarter = "October " + comboBoxEdit1.Text;
                        mSecondMonthInQuarter = "November " + comboBoxEdit1.Text;
                        mThirdMonthInQuarter = "December " + comboBoxEdit1.Text;

                        mFirstMonth = "October";
                        mSecondMonth = "November";
                        mThirdMonth = "December";
                    }
                    mDsData = pMdtReporter.CTC_ARTCLinicQuarterlySupervisionForm(
                        Convert.ToInt32(comboBoxEdit1.Text),
                        comboBoxEdit2.Text,
                        mFromDate, mToDate, mQuarterStartDate, mQuarterEndDate, mFirstMonthInQuarter, mSecondMonthInQuarter, mThirdMonthInQuarter, mFirstMonth, mSecondMonth, mThirdMonth);
                    mIsDataSet = true;
                }

                #endregion

                #endregion

                #region User Login Details

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.UserLoginDetailReport)
                {
                    mDsData = pMdtReporter.USER_LoginDetails(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), "", "");
                    mIsDataSet = true;
                }

                #endregion

                #endregion

                string mFileName = mReportCode;
                byte[] mBytes = pMdtReporter.Load_ReportTemplate(mFileName, false);
                DevExpress.XtraReports.UI.XtraReport mReportDoc = null;

                if (mIsDataSet == true)
                {
                    mReportDoc = Program.Load_ReportTemplate(mBytes, mDsData);
                }
                else
                {
                    mReportDoc = Program.Load_ReportTemplate(mBytes, mDtData);
                }

                if (mReportDoc == null)
                {
                    Program.gMdiForm.Cursor = Cursors.Default;
                    return;
                }

                mReportDoc.CreateDocument();
                mReportDoc.ShowPreview();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Prepare_ExtraPatientDetails
        private ExtraPatientDetailsFilter Prepare_ExtraPatientDetails(string mFieldNamePrefix)
        {
            ExtraPatientDetailsFilter mExtraDetails = new ExtraPatientDetailsFilter();

            DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");
            foreach (DataRow mDataRow in mDtExtraFields.Rows)
            {
                string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                string mFieldCaption = mDataRow["fieldcaption"].ToString();
                string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                string mDataType = mDataRow["datatype"].ToString().Trim();

                if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                {
                    ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                    LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mComboBoxEdit);

                    #region build filter

                    if (mLayoutControlItem.Visible == true)
                    {
                        if (mComboBoxEdit.Text.Trim() != "")
                        {
                            if (mExtraDetails.ExtraFilter.Trim() == "")
                            {
                                mExtraDetails.ExtraFilter = "(" + mFieldNamePrefix + mFieldName + "='" + mComboBoxEdit.Text + "')";
                            }
                            else
                            {
                                mExtraDetails.ExtraFilter = mExtraDetails.ExtraFilter + " and (" + mFieldNamePrefix + mFieldName + "='" + mComboBoxEdit.Text + "')";
                            }

                            mExtraDetails.ExtraParameters = mExtraDetails.ExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mComboBoxEdit.Text;
                        }
                        else
                        {
                            mExtraDetails.ExtraParameters = mExtraDetails.ExtraParameters + Environment.NewLine
                                + mLayoutControlItem.Text.ToUpper()
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion
                }
                else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().ToLower())
                {
                    CheckEdit mCheckEdit = layoutControl1.GetControlByName("chk" + mFieldName) as CheckEdit;
                    LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mCheckEdit);

                    #region build filter

                    if (mLayoutControlItem.Visible == true)
                    {
                        if (mExtraDetails.ExtraFilter.Trim() == "")
                        {
                            mExtraDetails.ExtraFilter = "(" + mFieldNamePrefix + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                        }
                        else
                        {
                            mExtraDetails.ExtraFilter = mExtraDetails.ExtraFilter + " and (" + mFieldNamePrefix + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                        }

                        mExtraDetails.ExtraParameters = mExtraDetails.ExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + Convert.ToInt16(mCheckEdit.Checked);
                    }
                    #endregion
                }
                else
                {
                    if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
                    {
                        DateEdit mDateEdit = layoutControl1.GetControlByName("txt" + mFieldName) as DateEdit;
                        LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mDateEdit);

                        #region build filter

                        if (mLayoutControlItem.Visible == true)
                        {
                            if (Program.IsNullDate(mDateEdit.EditValue) == false)
                            {
                                if (mExtraDetails.ExtraFilter.Trim() == "")
                                {
                                    mExtraDetails.ExtraFilter = "(" + mFieldNamePrefix + mFieldName + "='"
                                        + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                }
                                else
                                {
                                    mExtraDetails.ExtraFilter = mExtraDetails.ExtraFilter + " and (" + mFieldNamePrefix + mFieldName + "='"
                                        + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                }

                                mExtraDetails.ExtraParameters = mExtraDetails.ExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                    + Convert.ToDateTime(mDateEdit.EditValue).ToString("d");
                            }
                            else
                            {
                                mExtraDetails.ExtraParameters = mExtraDetails.ExtraParameters + Environment.NewLine
                                    + mLayoutControlItem.Text.ToUpper()
                                    + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                            }
                        }
                        #endregion
                    }
                    else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().ToLower())
                    {
                        TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                        LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                        #region build filter

                        if (mLayoutControlItem.Visible == true)
                        {
                            if (Program.IsMoney(mTextEdit.Text) == true)
                            {
                                if (mExtraDetails.ExtraFilter.Trim() == "")
                                {
                                    mExtraDetails.ExtraFilter = "(" + mFieldNamePrefix + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                }
                                else
                                {
                                    mExtraDetails.ExtraFilter = mExtraDetails.ExtraFilter + " and (" + mFieldNamePrefix + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                }

                                mExtraDetails.ExtraParameters = mExtraDetails.ExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                    + Convert.ToDouble(mTextEdit.Text).ToString("c");
                            }
                            else
                            {
                                mExtraDetails.ExtraParameters = mExtraDetails.ExtraParameters + Environment.NewLine
                                    + mLayoutControlItem.Text.ToUpper()
                                    + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                            }
                        }
                        #endregion
                    }
                    else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().ToLower())
                    {
                        TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                        LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                        #region build filter

                        if (mLayoutControlItem.Visible == true)
                        {
                            if (Program.IsMoney(mTextEdit.Text) == true)
                            {
                                if (mExtraDetails.ExtraFilter.Trim() == "")
                                {
                                    mExtraDetails.ExtraFilter = "(" + mFieldNamePrefix + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                }
                                else
                                {
                                    mExtraDetails.ExtraFilter = mExtraDetails.ExtraFilter + " and (" + mFieldNamePrefix + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                }

                                mExtraDetails.ExtraParameters = mExtraDetails.ExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                    + Convert.ToDouble(mTextEdit.Text).ToString("c");
                            }
                            else
                            {
                                mExtraDetails.ExtraParameters = mExtraDetails.ExtraParameters + Environment.NewLine
                                    + mLayoutControlItem.Text.ToUpper()
                                    + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                        LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                        #region build filter

                        if (mLayoutControlItem.Visible == true)
                        {
                            if (Program.IsMoney(mTextEdit.Text) == true)
                            {
                                if (mExtraDetails.ExtraFilter.Trim() == "")
                                {
                                    mExtraDetails.ExtraFilter = "(" + mFieldNamePrefix + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                }
                                else
                                {
                                    mExtraDetails.ExtraFilter = mExtraDetails.ExtraFilter + " and (" + mFieldNamePrefix + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                }

                                mExtraDetails.ExtraParameters = mExtraDetails.ExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mTextEdit.Text.Trim();
                            }
                            else
                            {
                                mExtraDetails.ExtraParameters = mExtraDetails.ExtraParameters + Environment.NewLine
                                    + mLayoutControlItem.Text.ToUpper()
                                    + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                            }
                        }
                        #endregion
                    }
                }
            }

            return mExtraDetails;
        }
        #endregion

        #region cmdDesign_Click
        private void cmdDesign_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdDesign_Click";

            try
            {
                this.Cursor = Cursors.WaitCursor;

                DataTable mDtData = new DataTable();
                DataSet mDsData = new DataSet();
                bool mIsDataSet = false;

                // Create a design form and get its panel.
                DevExpress.XtraReports.UserDesigner.XRDesignFormEx mDesignForm =
                    new DevExpress.XtraReports.UserDesigner.XRDesignFormEx();
                DevExpress.XtraReports.UserDesigner.XRDesignPanel mDesignPanel = mDesignForm.DesignPanel;

                string mReportCode = pReportCode;

                #region get reporting data

                #region debtorstatement

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.DebtorStatement)
                {
                    mDtData = pMdtReporter.BIL_DebtorStatement(
                        textEditSearch.Text, textEdit1.Text, AfyaPro_Types.clsEnums.DebtorTypes.Group.ToString().Trim(),
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), !checkEdit1.Checked);
                }

                #endregion

                #region billingitembarcodes

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.BillingItemBarcodes)
                {
                    mDtData = pMdtReporter.BIL_BillingItemsForBarcode();
                }

                #endregion

                #region registration

                #region attendancelist

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.AttendanceList)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    #region age
                    if (layoutControl1.GetItemByControl(comboBoxEdit1).Visible == true)
                    {
                        if (comboBoxEdit1.Text.Trim() != "" && Program.IsMoney(textEdit1.Text) != false)
                        {
                            string mPatientAge = pMdtReporter.Age_Formula("p.birthdate", "bookdate", "");
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + comboBoxEdit1.Text + " " + textEdit1.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region gender
                    if (layoutControl1.GetItemByControl(comboBoxEdit2).Visible == true)
                    {
                        if (comboBoxEdit2.Text.Trim() != "")
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(p.gender='" + comboBoxEdit2.Text + "')";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (p.gender='" + comboBoxEdit2.Text + "')";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text + ": " + comboBoxEdit2.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region weight
                    if (layoutControl1.GetItemByControl(comboBoxEdit3).Visible == true)
                    {
                        if (comboBoxEdit3.Text.Trim() != "" && Program.IsMoney(textEdit2.Text) != false)
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(patientweight " + comboBoxEdit3.Text + " " + textEdit2.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (patientweight " + comboBoxEdit3.Text + " " + textEdit2.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit3).Text
                                + ": " + comboBoxEdit3.Text + " " + textEdit2.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit3).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region temperature
                    if (layoutControl1.GetItemByControl(comboBoxEdit4).Visible == true)
                    {
                        if (comboBoxEdit4.Text.Trim() != "" && Program.IsMoney(textEdit3.Text) != false)
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(patienttemperature " + comboBoxEdit4.Text + " " + textEdit3.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (patienttemperature " + comboBoxEdit4.Text + " " + textEdit3.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit4).Text
                                + ": " + comboBoxEdit4.Text + " " + textEdit3.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit4).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region treatmentpoint
                    if (layoutControl1.GetItemByControl(lookUpEdit1).Visible == true)
                    {
                        string mTreatmentPointCode = "";
                        string mTreatmentPointDescription = "";

                        if (lookUpEdit1.ItemIndex != -1)
                        {
                            mTreatmentPointCode = lookUpEdit1.GetColumnValue("code").ToString().Trim();
                            mTreatmentPointDescription = lookUpEdit1.GetColumnValue("description").ToString().Trim();
                        }

                        if (mTreatmentPointCode.Trim() != "")
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(wheretakencode='" + mTreatmentPointCode + "')";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (wheretakencode='" + mTreatmentPointCode + "')";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                + ": " + mTreatmentPointDescription;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region extra patient details

                    DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");
                    foreach (DataRow mDataRow in mDtExtraFields.Rows)
                    {
                        string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                        string mFieldCaption = mDataRow["fieldcaption"].ToString();
                        string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                        string mDataType = mDataRow["datatype"].ToString().Trim();

                        if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mComboBoxEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mComboBoxEdit.Text.Trim() != "")
                                {
                                    if (mExtraFilter.Trim() == "")
                                    {
                                        mExtraFilter = "(patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }
                                    else
                                    {
                                        mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }

                                    mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mComboBoxEdit.Text;
                                }
                                else
                                {
                                    mExtraParameters = mExtraParameters + Environment.NewLine
                                        + mLayoutControlItem.Text.ToUpper()
                                        + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                }
                            }
                            #endregion
                        }
                        else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().ToLower())
                        {
                            CheckEdit mCheckEdit = layoutControl1.GetControlByName("chk" + mFieldName) as CheckEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mCheckEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mExtraFilter.Trim() == "")
                                {
                                    mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }
                                else
                                {
                                    mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }

                                mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + Convert.ToInt16(mCheckEdit.Checked);
                            }
                            #endregion
                        }
                        else
                        {
                            if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
                            {
                                DateEdit mDateEdit = layoutControl1.GetControlByName("txt" + mFieldName) as DateEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mDateEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsNullDate(mDateEdit.EditValue) == false)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDateTime(mDateEdit.EditValue).ToString("d");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mTextEdit.Text.Trim();
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    #endregion

                    mDtData = pMdtReporter.REG_AttendanceList(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mExtraFilter, mExtraParameters);
                }

                #endregion

                #region attendancecountage

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.AttendanceCountAge)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    #region gender
                    if (layoutControl1.GetItemByControl(comboBoxEdit2).Visible == true)
                    {
                        if (comboBoxEdit2.Text.Trim() != "")
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(p.gender='" + comboBoxEdit2.Text + "')";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (p.gender='" + comboBoxEdit2.Text + "')";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text
                                + ": " + comboBoxEdit2.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region weight
                    if (layoutControl1.GetItemByControl(comboBoxEdit3).Visible == true)
                    {
                        if (comboBoxEdit3.Text.Trim() != "" && Program.IsMoney(textEdit2.Text) != false)
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(patientweight " + comboBoxEdit3.Text + " " + textEdit2.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (patientweight " + comboBoxEdit3.Text + " " + textEdit2.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit3).Text
                                + ": " + comboBoxEdit3.Text + " " + textEdit2.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit3).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region temperature
                    if (layoutControl1.GetItemByControl(comboBoxEdit4).Visible == true)
                    {
                        if (comboBoxEdit4.Text.Trim() != "" && Program.IsMoney(textEdit3.Text) != false)
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(patienttemperature " + comboBoxEdit4.Text + " " + textEdit3.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (patienttemperature " + comboBoxEdit4.Text + " " + textEdit3.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit4).Text
                                + ": " + comboBoxEdit4.Text + " " + textEdit3.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit4).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region treatmentpoint
                    if (layoutControl1.GetItemByControl(lookUpEdit1).Visible == true)
                    {
                        string mTreatmentPointCode = "";
                        string mTreatmentPointDescription = "";

                        if (lookUpEdit1.ItemIndex != -1)
                        {
                            mTreatmentPointCode = lookUpEdit1.GetColumnValue("code").ToString().Trim();
                            mTreatmentPointDescription = lookUpEdit1.GetColumnValue("description").ToString().Trim();
                        }

                        if (mTreatmentPointCode.Trim() != "")
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(wheretakencode='" + mTreatmentPointCode + "')";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (wheretakencode='" + mTreatmentPointCode + "')";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                + ": " + mTreatmentPointDescription;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region extra patient details

                    DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");
                    foreach (DataRow mDataRow in mDtExtraFields.Rows)
                    {
                        string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                        string mFieldCaption = mDataRow["fieldcaption"].ToString();
                        string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                        string mDataType = mDataRow["datatype"].ToString().Trim();

                        if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mComboBoxEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mComboBoxEdit.Text.Trim() != "")
                                {
                                    if (mExtraFilter.Trim() == "")
                                    {
                                        mExtraFilter = "(patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }
                                    else
                                    {
                                        mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }

                                    mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mComboBoxEdit.Text;
                                }
                                else
                                {
                                    mExtraParameters = mExtraParameters + Environment.NewLine
                                        + mLayoutControlItem.Text.ToUpper()
                                        + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                }
                            }
                            #endregion
                        }
                        else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().ToLower())
                        {
                            CheckEdit mCheckEdit = layoutControl1.GetControlByName("chk" + mFieldName) as CheckEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mCheckEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mExtraFilter.Trim() == "")
                                {
                                    mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }
                                else
                                {
                                    mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }

                                mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + Convert.ToInt16(mCheckEdit.Checked);
                            }
                            #endregion
                        }
                        else
                        {
                            if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
                            {
                                DateEdit mDateEdit = layoutControl1.GetControlByName("txt" + mFieldName) as DateEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mDateEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsNullDate(mDateEdit.EditValue) == false)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDateTime(mDateEdit.EditValue).ToString("d");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mTextEdit.Text.Trim();
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    #endregion

                    mDtData = pMdtReporter.REG_AttendanceCountAge(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mExtraFilter, mExtraParameters);
                }

                #endregion

                #region AttendanceCountTreatmentPoints

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.AttendanceCountTreatmentPoints)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    #region age
                    if (layoutControl1.GetItemByControl(comboBoxEdit1).Visible == true)
                    {
                        if (comboBoxEdit1.Text.Trim() != "" && Program.IsMoney(textEdit1.Text) != false)
                        {
                            string mPatientAge = pMdtReporter.Age_Formula("p.birthdate", "bookdate", "");
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + comboBoxEdit1.Text + " " + textEdit1.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region gender
                    if (layoutControl1.GetItemByControl(comboBoxEdit2).Visible == true)
                    {
                        if (comboBoxEdit2.Text.Trim() != "")
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(p.gender='" + comboBoxEdit2.Text + "')";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (p.gender='" + comboBoxEdit2.Text + "')";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text
                                + ": " + comboBoxEdit2.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region weight
                    if (layoutControl1.GetItemByControl(comboBoxEdit3).Visible == true)
                    {
                        if (comboBoxEdit3.Text.Trim() != "" && Program.IsMoney(textEdit2.Text) != false)
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(patientweight " + comboBoxEdit3.Text + " " + textEdit2.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (patientweight " + comboBoxEdit3.Text + " " + textEdit2.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit3).Text
                                + ": " + comboBoxEdit3.Text + " " + textEdit2.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit3).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region temperature
                    if (layoutControl1.GetItemByControl(comboBoxEdit4).Visible == true)
                    {
                        if (comboBoxEdit4.Text.Trim() != "" && Program.IsMoney(textEdit3.Text) != false)
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(patienttemperature " + comboBoxEdit4.Text + " " + textEdit3.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (patienttemperature " + comboBoxEdit4.Text + " " + textEdit3.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit4).Text
                                + ": " + comboBoxEdit4.Text + " " + textEdit3.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit4).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region extra patient details

                    DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");
                    foreach (DataRow mDataRow in mDtExtraFields.Rows)
                    {
                        string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                        string mFieldCaption = mDataRow["fieldcaption"].ToString();
                        string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                        string mDataType = mDataRow["datatype"].ToString().Trim();

                        if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mComboBoxEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mComboBoxEdit.Text.Trim() != "")
                                {
                                    if (mExtraFilter.Trim() == "")
                                    {
                                        mExtraFilter = "(patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }
                                    else
                                    {
                                        mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }

                                    mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mComboBoxEdit.Text;
                                }
                                else
                                {
                                    mExtraParameters = mExtraParameters + Environment.NewLine
                                        + mLayoutControlItem.Text.ToUpper()
                                        + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                }
                            }
                            #endregion
                        }
                        else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().ToLower())
                        {
                            CheckEdit mCheckEdit = layoutControl1.GetControlByName("chk" + mFieldName) as CheckEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mCheckEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mExtraFilter.Trim() == "")
                                {
                                    mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }
                                else
                                {
                                    mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }

                                mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + Convert.ToInt16(mCheckEdit.Checked);
                            }
                            #endregion
                        }
                        else
                        {
                            if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
                            {
                                DateEdit mDateEdit = layoutControl1.GetControlByName("txt" + mFieldName) as DateEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mDateEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsNullDate(mDateEdit.EditValue) == false)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDateTime(mDateEdit.EditValue).ToString("d");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mTextEdit.Text.Trim();
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    #endregion

                    mDsData = pMdtReporter.REG_AttendanceCountTreatmentPoints(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mExtraFilter, mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region AttendanceCountCustomerGroups

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.AttendanceCountCustomerGroups)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    #region age

                    string mAgeFormula = pMdtReporter.Age_Formula("p.birthdate", "b.bookdate", "");

                    if (layoutControl1.GetItemByControl(comboBoxEdit1).Visible == true)
                    {
                        if (comboBoxEdit1.Text.Trim() != "" && Program.IsMoney(textEdit1.Text) != false)
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(" + mAgeFormula + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (" + mAgeFormula + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + comboBoxEdit1.Text + " " + textEdit1.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region gender
                    if (layoutControl1.GetItemByControl(comboBoxEdit2).Visible == true)
                    {
                        if (comboBoxEdit2.Text.Trim() != "")
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(p.gender='" + comboBoxEdit2.Text + "')";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (p.gender='" + comboBoxEdit2.Text + "')";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text
                                + ": " + comboBoxEdit2.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region weight
                    if (layoutControl1.GetItemByControl(comboBoxEdit3).Visible == true)
                    {
                        if (comboBoxEdit3.Text.Trim() != "" && Program.IsMoney(textEdit2.Text) != false)
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(patientweight " + comboBoxEdit3.Text + " " + textEdit2.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (patientweight " + comboBoxEdit3.Text + " " + textEdit2.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit3).Text
                                + ": " + comboBoxEdit3.Text + " " + textEdit2.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit3).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region temperature
                    if (layoutControl1.GetItemByControl(comboBoxEdit4).Visible == true)
                    {
                        if (comboBoxEdit4.Text.Trim() != "" && Program.IsMoney(textEdit3.Text) != false)
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(patienttemperature " + comboBoxEdit4.Text + " " + textEdit3.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (patienttemperature " + comboBoxEdit4.Text + " " + textEdit3.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit4).Text
                                + ": " + comboBoxEdit4.Text + " " + textEdit3.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit4).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region treatmentpoint
                    if (layoutControl1.GetItemByControl(lookUpEdit1).Visible == true)
                    {
                        string mTreatmentPointCode = "";
                        string mTreatmentPointDescription = "";

                        if (lookUpEdit1.ItemIndex != -1)
                        {
                            mTreatmentPointCode = lookUpEdit1.GetColumnValue("code").ToString().Trim();
                            mTreatmentPointDescription = lookUpEdit1.GetColumnValue("description").ToString().Trim();
                        }

                        if (mTreatmentPointCode.Trim() != "")
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(wheretakencode='" + mTreatmentPointCode + "')";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (wheretakencode='" + mTreatmentPointCode + "')";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                + ": " + mTreatmentPointDescription;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region extra patient details

                    DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");
                    foreach (DataRow mDataRow in mDtExtraFields.Rows)
                    {
                        string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                        string mFieldCaption = mDataRow["fieldcaption"].ToString();
                        string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                        string mDataType = mDataRow["datatype"].ToString().Trim();

                        if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mComboBoxEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mComboBoxEdit.Text.Trim() != "")
                                {
                                    if (mExtraFilter.Trim() == "")
                                    {
                                        mExtraFilter = "(patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }
                                    else
                                    {
                                        mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }

                                    mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mComboBoxEdit.Text;
                                }
                                else
                                {
                                    mExtraParameters = mExtraParameters + Environment.NewLine
                                        + mLayoutControlItem.Text.ToUpper()
                                        + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                }
                            }
                            #endregion
                        }
                        else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().ToLower())
                        {
                            CheckEdit mCheckEdit = layoutControl1.GetControlByName("chk" + mFieldName) as CheckEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mCheckEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mExtraFilter.Trim() == "")
                                {
                                    mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }
                                else
                                {
                                    mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }

                                mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + Convert.ToInt16(mCheckEdit.Checked);
                            }
                            #endregion
                        }
                        else
                        {
                            if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
                            {
                                DateEdit mDateEdit = layoutControl1.GetControlByName("txt" + mFieldName) as DateEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mDateEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsNullDate(mDateEdit.EditValue) == false)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDateTime(mDateEdit.EditValue).ToString("d");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mTextEdit.Text.Trim();
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    #endregion

                    mDsData = pMdtReporter.REG_AttendanceCountCustomerGroups(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mExtraFilter, mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region IPDDailyCensusSummary

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.IPDDailyCensusSummary)
                {
                    mDsData = pMdtReporter.REG_IPDDailyCensusSummaryWards(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, "", "");
                    mIsDataSet = true;
                }

                #endregion

                #region IPDDailyCensusDetailed

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.IPDDailyCensusDetailed)
                {
                    string mWardCode = "";
                    string mExtraParameters = "";

                    #region ward
                    if (layoutControl1.GetItemByControl(lookUpEdit1).Visible == true)
                    {
                        string mWardDescription = "";

                        if (lookUpEdit1.ItemIndex != -1)
                        {
                            mWardCode = lookUpEdit1.GetColumnValue("code").ToString().Trim();
                            mWardDescription = lookUpEdit1.GetColumnValue("description").ToString().Trim();
                        }

                        if (mWardCode.Trim() != "")
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                + ": " + mWardDescription;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    if (mWardCode.Trim() == "")
                    {
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    mDsData = pMdtReporter.REG_IPDDailyCensusDetailedWards(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mWardCode, "", mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #endregion

                #region diagnoses

                #region DXTDiagnosesSummary

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.DXTDiagnosesSummary)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    #region age
                    if (layoutControl1.GetItemByControl(comboBoxEdit1).Visible == true)
                    {
                        if (comboBoxEdit1.Text.Trim() != "" && Program.IsMoney(textEdit1.Text) != false)
                        {
                            string mPatientAge = pMdtReporter.Age_Formula("p.birthdate", "transdate", "");
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + comboBoxEdit1.Text + " " + textEdit1.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region extra patient details

                    DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");
                    foreach (DataRow mDataRow in mDtExtraFields.Rows)
                    {
                        string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                        string mFieldCaption = mDataRow["fieldcaption"].ToString();
                        string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                        string mDataType = mDataRow["datatype"].ToString().Trim();

                        if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mComboBoxEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mComboBoxEdit.Text.Trim() != "")
                                {
                                    if (mExtraFilter.Trim() == "")
                                    {
                                        mExtraFilter = "(patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }
                                    else
                                    {
                                        mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }

                                    mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mComboBoxEdit.Text;
                                }
                                else
                                {
                                    mExtraParameters = mExtraParameters + Environment.NewLine
                                        + mLayoutControlItem.Text.ToUpper()
                                        + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                }
                            }
                            #endregion
                        }
                        else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().ToLower())
                        {
                            CheckEdit mCheckEdit = layoutControl1.GetControlByName("chk" + mFieldName) as CheckEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mCheckEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mExtraFilter.Trim() == "")
                                {
                                    mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }
                                else
                                {
                                    mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }

                                mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + Convert.ToInt16(mCheckEdit.Checked);
                            }
                            #endregion
                        }
                        else
                        {
                            if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
                            {
                                DateEdit mDateEdit = layoutControl1.GetControlByName("txt" + mFieldName) as DateEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mDateEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsNullDate(mDateEdit.EditValue) == false)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDateTime(mDateEdit.EditValue).ToString("d");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mTextEdit.Text.Trim();
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    #endregion

                    if (checkEdit1.Checked == true)
                    {
                        mExtraParameters = mExtraParameters + Environment.NewLine
                            + "List Type"
                            + ": Primary Diagnoses Only";
                    }
                    else
                    {
                        mExtraParameters = mExtraParameters + Environment.NewLine
                            + "List Type"
                            + ": All Diagnoses";
                    }

                    switch (comboBoxEdit2.SelectedIndex)
                    {
                        case 1: mReportCode = mReportCode + "_opd"; break;
                        case 2: mReportCode = mReportCode + "_ipd"; break;
                    }

                    mExtraParameters = mExtraParameters + Environment.NewLine
                        + layoutControl1.GetItemByControl(comboBoxEdit2).Text
                        + ": " + comboBoxEdit2.Text;

                    mDsData = pMdtReporter.DXT_DiagnosesSummary(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, checkEdit1.Checked, comboBoxEdit2.SelectedIndex, mExtraFilter, mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region DXTDiagnosesListing

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.DXTDiagnosesListing)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    #region age
                    if (layoutControl1.GetItemByControl(comboBoxEdit1).Visible == true)
                    {
                        if (comboBoxEdit1.Text.Trim() != "" && Program.IsMoney(textEdit1.Text) != false)
                        {
                            string mPatientAge = pMdtReporter.Age_Formula("p.birthdate", "transdate", "");
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + comboBoxEdit1.Text + " " + textEdit1.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region extra patient details

                    DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");
                    foreach (DataRow mDataRow in mDtExtraFields.Rows)
                    {
                        string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                        string mFieldCaption = mDataRow["fieldcaption"].ToString();
                        string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                        string mDataType = mDataRow["datatype"].ToString().Trim();

                        if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mComboBoxEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mComboBoxEdit.Text.Trim() != "")
                                {
                                    if (mExtraFilter.Trim() == "")
                                    {
                                        mExtraFilter = "(patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }
                                    else
                                    {
                                        mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }

                                    mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mComboBoxEdit.Text;
                                }
                                else
                                {
                                    mExtraParameters = mExtraParameters + Environment.NewLine
                                        + mLayoutControlItem.Text.ToUpper()
                                        + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                }
                            }
                            #endregion
                        }
                        else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().ToLower())
                        {
                            CheckEdit mCheckEdit = layoutControl1.GetControlByName("chk" + mFieldName) as CheckEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mCheckEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mExtraFilter.Trim() == "")
                                {
                                    mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }
                                else
                                {
                                    mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }

                                mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + Convert.ToInt16(mCheckEdit.Checked);
                            }
                            #endregion
                        }
                        else
                        {
                            if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
                            {
                                DateEdit mDateEdit = layoutControl1.GetControlByName("txt" + mFieldName) as DateEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mDateEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsNullDate(mDateEdit.EditValue) == false)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDateTime(mDateEdit.EditValue).ToString("d");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mTextEdit.Text.Trim();
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    #endregion

                    mDtData = pMdtReporter.DXT_DiagnosesListing(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mExtraFilter, mExtraParameters);
                }

                #endregion

                #region DXTPatientHistory

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.DXTPatientHistory)
                {
                    TextEdit txtPatientId = (TextEdit)layoutControl1.GetControlByName("txtPatientId");
                    if (txtPatientId.Text.Trim() == "")
                    {
                        Program.Display_Error("Invalid Patient #");
                        txtPatientId.Focus();
                        return;
                    }

                    mDsData = pMdtReporter.DXT_PatientHistory(txtPatientId.Text);
                    mIsDataSet = true;
                }

                #endregion

                #endregion

                #region cashbox

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.CashBox)
                {
                    string mExtraFilter = "";

                    #region user id
                    if (layoutControl1.GetItemByControl(lookUpEdit1).Visible == true)
                    {
                        string mUserCode = "";
                        string mUserDescription = "";

                        if (lookUpEdit1.ItemIndex != -1)
                        {
                            mUserCode = lookUpEdit1.GetColumnValue("code").ToString().Trim();
                            mUserDescription = lookUpEdit1.GetColumnValue("description").ToString().Trim();
                        }

                        if (mUserCode.Trim() != "")
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "userid='" + mUserCode + "' ";
                            }
                        }

                    }
                    #endregion

                    mDsData = pMdtReporter.BIL_CashBox(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true,"", "");
                    mIsDataSet = true;
                }

                #endregion

                #region my cashbox  

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.MyCashBox)
                {
                    string mExtraFilter = "";
                    if (Program.gCurrentUser != null)
                    {
                        mExtraFilter = " and userid= '" + Program.gCurrentUser.Code + "'  ";
                    }

                    mDsData = pMdtReporter.BIL_CashBox(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, "", "");
                    mIsDataSet = true;
                }

                #endregion

                #region debtorslist

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.BILDebtorsList)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";
                    bool mGroups = true;
                    bool mIndividuals = true;
                    DateTime mFromDate = DateTime.Today;
                    DateTime mToDate = DateTime.Today;

                    #region balance
                    if (layoutControl1.GetItemByControl(comboBoxEdit1).Visible == true)
                    {
                        if (comboBoxEdit1.Text.Trim() != "" && Program.IsMoney(textEdit1.Text) != false)
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(balance " + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (balance " + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + comboBoxEdit1.Text + " " + textEdit1.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region accounttype
                    if (layoutControl1.GetItemByControl(comboBoxEdit2).Visible == true)
                    {
                        if (comboBoxEdit2.Text.Trim() != "")
                        {
                            mGroups = false;
                            mIndividuals = false;
                            if (comboBoxEdit2.Text.Trim().ToLower() == "groups")
                            {
                                mGroups = true;
                            }
                            else if (comboBoxEdit2.Text.Trim().ToLower() == "individuals")
                            {
                                mIndividuals = true;
                                //mReportCode = pReportCode + "_individual";
                            }
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text + ": " + comboBoxEdit2.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region FromDate
                    if (layoutControl1.GetItemByControl(dateEdit1).Visible == true)
                    {
                        if (dateEdit1.Text.Trim() != "")
                        {

                            mFromDate = Convert.ToDateTime(dateEdit1.EditValue);
                        }

                    }
                    #endregion

                    #region ToDate
                    if (layoutControl1.GetItemByControl(dateEdit2).Visible == true)
                    {
                        if (dateEdit2.Text.Trim() != "")
                        {

                            mToDate = Convert.ToDateTime(dateEdit2.EditValue);
                        }

                    }
                    #endregion



                    mDsData = pMdtReporter.BIL_DebtorsList(
                        DateTime.Now, mGroups, mIndividuals, mExtraFilter, mExtraParameters, mFromDate, mToDate);
                    mIsDataSet = true;
                    mIsDataSet = true;
                }

                #endregion

                #region debtors

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.BillDebtorsList1)
                {
                    
                    bool mGroups = true;
                    bool mIndividuals = true;
                    DateTime mFromDate = DateTime.Today;
                    DateTime mToDate = DateTime.Today;

                                     

                    #region FromDate
                    if (layoutControl1.GetItemByControl(dateEdit1).Visible == true)
                    {
                        if (dateEdit1.Text.Trim() != "")
                        {

                            mFromDate = Convert.ToDateTime(dateEdit1.EditValue);
                        }
                        else
                        {
                            MessageBox.Show("Enter start date", "Start date",MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                    }
                    #endregion

                    #region ToDate
                    if (layoutControl1.GetItemByControl(dateEdit2).Visible == true)
                    {
                        if (dateEdit2.Text.Trim() != "")
                        {

                            mToDate = Convert.ToDateTime(dateEdit2.EditValue);
                        }
                        else
                        {
                            MessageBox.Show("Enter end date", "End date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                    }
                    #endregion



                    mDsData = pMdtReporter.BIL_Debtors(mFromDate, mToDate);
                    mIsDataSet = true;
                  
                }

                #endregion

                #region dailysalessummary

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.BILDailySalesSummary)
                {
                    mDsData = pMdtReporter.BIL_DailySalesSummary(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, "", "");
                    mIsDataSet = true;
                }

                #endregion

                #region dailysalesdetails

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.BILDailySalesDetails)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    if (checkEdit2.Checked == true)
                    {
                        mReportCode = mReportCode + "_bydept";
                    }

                    #region customer group

                    if (lookUpEdit1.ItemIndex != -1)
                    {
                        string mBillingGroupCode = lookUpEdit1.GetColumnValue("code").ToString().Trim();
                        string mBillingGroupDescription = lookUpEdit1.GetColumnValue("description").ToString().Trim();

                        if (mBillingGroupCode.Trim().ToLower() == "individuals")
                        {
                            mExtraFilter = "(billinggroupcode='')";
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                + ": " + mBillingGroupDescription;
                        }
                        else if (mBillingGroupCode.Trim().ToLower() == "")
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                 + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                 + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                        else
                        {
                            mExtraFilter = "(billinggroupcode='" + mBillingGroupCode + "')";
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                + ": " + mBillingGroupDescription;
                        }
                    }
                    else
                    {
                        mExtraParameters = mExtraParameters + Environment.NewLine
                             + layoutControl1.GetItemByControl(lookUpEdit1).Text
                             + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                    }

                    #endregion

                    #region item group

                    if (lookUpEdit2.ItemIndex != -1)
                    {
                        string mItemGroupCode = lookUpEdit2.GetColumnValue("code").ToString().Trim();
                        string mItemGroupDescription = lookUpEdit2.GetColumnValue("description").ToString().Trim();

                        if (mItemGroupCode.Trim().ToLower() == "")
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                 + layoutControl1.GetItemByControl(lookUpEdit2).Text
                                 + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                        else
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(itemgroupcode='" + mItemGroupCode + "')";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (itemgroupcode='" + mItemGroupCode + "')";
                            }
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit2).Text
                                + ": " + mItemGroupDescription;
                        }
                    }
                    else
                    {
                        mExtraParameters = mExtraParameters + Environment.NewLine
                             + layoutControl1.GetItemByControl(lookUpEdit2).Text
                             + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                    }

                    #endregion

                    mDsData = pMdtReporter.BIL_DailySalesDetails(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, true, false, checkEdit2.Checked, mExtraFilter, mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region BILGroupBillBreakDown

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.BILGroupBillBreakDown)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    if (lookUpEdit1.ItemIndex == -1)
                    {
                        Program.Display_Error("Please select the group");
                        lookUpEdit1.Focus();
                        return;
                    }

                    #region customer group

                    if (lookUpEdit1.ItemIndex != -1)
                    {
                        string mBillingGroupCode = lookUpEdit1.GetColumnValue("code").ToString().Trim();
                        string mBillingGroupDescription = lookUpEdit1.GetColumnValue("description").ToString().Trim();

                        if (mBillingGroupCode.Trim().ToLower() == "individuals")
                        {
                            mExtraFilter = "(billinggroupcode='')";
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                + ": " + mBillingGroupDescription;
                        }
                        else if (mBillingGroupCode.Trim().ToLower() == "")
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                 + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                 + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                        else
                        {
                            mExtraFilter = "(billinggroupcode='" + mBillingGroupCode + "')";
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(lookUpEdit1).Text
                                + ": " + mBillingGroupDescription;
                        }
                    }
                    else
                    {
                        mExtraParameters = mExtraParameters + Environment.NewLine
                             + layoutControl1.GetItemByControl(lookUpEdit1).Text
                             + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                    }

                    #endregion

                    mExtraParameters = mExtraParameters + Environment.NewLine
                        + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                        + ": " + comboBoxEdit1.Text;

                    mDsData = pMdtReporter.BIL_GroupChargesBreakDown(!checkEdit1.Checked,
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), checkEdit2.Checked, comboBoxEdit1.SelectedIndex, mExtraFilter, mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region BILDebtorsListIndividual

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.BILDebtorsListIndividual)
                {
                    string mBalanceFilter = "";
                    string mExtraParameters = "";

                    #region balance
                    if (layoutControl1.GetItemByControl(comboBoxEdit1).Visible == true)
                    {
                        if (comboBoxEdit1.Text.Trim() != "" && Program.IsMoney(textEdit1.Text) != false)
                        {
                            if (mBalanceFilter.Trim() == "")
                            {
                                mBalanceFilter = "(balance " + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }
                            else
                            {
                                mBalanceFilter = mBalanceFilter + " and (balance " + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + comboBoxEdit1.Text + " " + textEdit1.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    ExtraPatientDetailsFilter mExtraPatientDetails = this.Prepare_ExtraPatientDetails("");

                    if (mExtraPatientDetails.ExtraParameters.Trim() != "")
                    {
                        if (mExtraParameters.Trim() == "")
                        {
                            mExtraParameters = mExtraPatientDetails.ExtraParameters;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + mExtraPatientDetails.ExtraParameters;
                        }
                    }

                    mDsData = pMdtReporter.BIL_DebtorsList(
                        DateTime.Now, mExtraPatientDetails.ExtraFilter, mBalanceFilter, mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region dailyincome

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.BILDailyIncome)
                {
                    mDsData = pMdtReporter.BIL_DailyIncome(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, "", "");
                    mIsDataSet = true;
                }

                #endregion

                #region inventory

                #region IVStockBalance

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.IVStockBalance)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    if (Program.IsNullDate(dateEdit1.EditValue) == true)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                        dateEdit1.Focus();
                        return;
                    }

                    if (lookUpEdit1.ItemIndex == -1)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreDescriptionIsInvalid.ToString());
                        lookUpEdit1.Focus();
                        return;
                    }

                    string mBalanceCondition = comboBoxEdit2.Text.Trim();
                    double mBalanceLimit = 0;

                    if (Program.IsMoney(textEdit1.Text) == false)
                    {
                        mBalanceCondition = "";
                        mBalanceLimit = 0;
                    }
                    else
                    {
                        mBalanceLimit = Convert.ToDouble(textEdit1.Text);
                    }

                    #region categories

                    string mCategories = "";
                    DataTable mDtCategories = (DataTable)gridControl1.DataSource;

                    foreach (DataRow mDataRow in mDtCategories.Rows)
                    {
                        if (Convert.ToBoolean(mDataRow["selected"]) == true)
                        {
                            if (mCategories.Trim() == "")
                            {
                                mCategories = "'" + mDataRow["code"].ToString().Trim() + "'";
                            }
                            else
                            {
                                mCategories = mCategories + ",'" + mDataRow["code"].ToString().Trim() + "'";
                            }
                        }
                    }

                    if (mCategories.Trim() == "")
                    {
                        mCategories = "1=2";
                    }
                    else
                    {
                        mCategories = "departmentcode in (" + mCategories + ")";
                    }

                    #endregion

                    mExtraFilter = mCategories;

                    mDtData = pMdtReporter.IV_StockBalance(lookUpEdit1.GetColumnValue("code").ToString(),
                        lookUpEdit1.GetColumnValue("description").ToString(), Convert.ToDateTime(dateEdit1.EditValue),
                        mBalanceCondition, mBalanceLimit, mExtraFilter, mExtraParameters);
                    mIsDataSet = false;
                }

                #endregion

                #region IVGRN

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.IVGRN)
                {
                    LookUpEdit cboStore = layoutControl1.GetControlByName("cboStore") as LookUpEdit;
                    DateEdit txtDateFrom = layoutControl1.GetControlByName("txtDateFrom") as DateEdit;
                    DateEdit txtDateTo = layoutControl1.GetControlByName("txtDateTo") as DateEdit;
                    RadioGroup radGroupBy = layoutControl1.GetControlByName("radGroupBy") as RadioGroup;
                    CheckEdit chkShowExpiryDates = layoutControl1.GetControlByName("chkShowExpiryDates") as CheckEdit;

                    //validate store
                    if (Program.IsNullDate(dateEdit1.EditValue) == true)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                        dateEdit1.Focus();
                        return;
                    }

                    if (lookUpEdit1.ItemIndex == -1)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreDescriptionIsInvalid.ToString());
                        lookUpEdit1.Focus();
                        return;
                    }

                    //validate dates
                    if (Program.IsNullDate(txtDateFrom.EditValue) == true)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                        txtDateFrom.Focus();
                        return;
                    }

                    if (Program.IsNullDate(txtDateTo.EditValue) == true)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                        txtDateTo.Focus();
                        return;
                    }

                    if (Convert.ToDateTime(txtDateTo.EditValue) < Convert.ToDateTime(txtDateFrom.EditValue))
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateRangeIsInvalid.ToString());
                        txtDateTo.Focus();
                        return;
                    }

                    bool mGroupBySupplier = false;
                    bool mGroupByDeliveryNo = false;

                    mReportCode = pReportCode;

                    switch (radGroupBy.SelectedIndex)
                    {
                        case 0:
                            {
                                if (Convert.ToBoolean(chkShowExpiryDates.Checked) == true)
                                {
                                    mReportCode = mReportCode + "_expirydates";
                                }
                            }
                            break;
                        case 1:
                            {
                                mReportCode = mReportCode + "_supplier";

                                if (Convert.ToBoolean(chkShowExpiryDates.Checked) == true)
                                {
                                    mReportCode = mReportCode + "expirydates";
                                }

                                mGroupBySupplier = true;
                            }
                            break;
                        case 2:
                            {
                                mReportCode = mReportCode + "_delivery";

                                if (Convert.ToBoolean(chkShowExpiryDates.Checked) == true)
                                {
                                    mReportCode = mReportCode + "expirydates";
                                }

                                mGroupByDeliveryNo = true;
                            }
                            break;
                    }

                    mDsData = pMdtReporter.IV_GRN(cboStore.GetColumnValue("code").ToString(),
                                                    cboStore.GetColumnValue("description").ToString(),
                                                    Convert.ToDateTime(dateEdit1.EditValue),
                                                    Convert.ToDateTime(dateEdit2.EditValue),
                                                    mGroupBySupplier,
                                                    mGroupByDeliveryNo,
                                                    Convert.ToBoolean(chkShowExpiryDates.Checked),
                                                    "",
                                                    "");
                    mIsDataSet = true;
                }

                #endregion

                #region IVGIN

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.IVGIN)
                {
                    LookUpEdit cboStore = layoutControl1.GetControlByName("cboStore") as LookUpEdit;
                    DateEdit txtDateFrom = layoutControl1.GetControlByName("txtDateFrom") as DateEdit;
                    DateEdit txtDateTo = layoutControl1.GetControlByName("txtDateTo") as DateEdit;
                    RadioGroup radGroupBy = layoutControl1.GetControlByName("radGroupBy") as RadioGroup;
                    CheckEdit chkShowExpiryDates = layoutControl1.GetControlByName("chkShowExpiryDates") as CheckEdit;

                    //validate store
                    if (Program.IsNullDate(dateEdit1.EditValue) == true)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                        dateEdit1.Focus();
                        return;
                    }

                    if (lookUpEdit1.ItemIndex == -1)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreDescriptionIsInvalid.ToString());
                        lookUpEdit1.Focus();
                        return;
                    }

                    //validate dates
                    if (Program.IsNullDate(txtDateFrom.EditValue) == true)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                        txtDateFrom.Focus();
                        return;
                    }

                    if (Program.IsNullDate(txtDateTo.EditValue) == true)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                        txtDateTo.Focus();
                        return;
                    }

                    if (Convert.ToDateTime(txtDateTo.EditValue) < Convert.ToDateTime(txtDateFrom.EditValue))
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateRangeIsInvalid.ToString());
                        txtDateTo.Focus();
                        return;
                    }

                    bool mGroupByCustomer = false;
                    bool mGroupByDeliveryNo = false;

                    mReportCode = pReportCode;

                    switch (radGroupBy.SelectedIndex)
                    {
                        case 0:
                            {
                                if (Convert.ToBoolean(chkShowExpiryDates.Checked) == true)
                                {
                                    mReportCode = mReportCode + "_expirydates";
                                }
                            }
                            break;
                        case 1:
                            {
                                mReportCode = mReportCode + "_customer";

                                if (Convert.ToBoolean(chkShowExpiryDates.Checked) == true)
                                {
                                    mReportCode = mReportCode + "expirydates";
                                }

                                mGroupByCustomer = true;
                            }
                            break;
                        case 2:
                            {
                                mReportCode = mReportCode + "_delivery";

                                if (Convert.ToBoolean(chkShowExpiryDates.Checked) == true)
                                {
                                    mReportCode = mReportCode + "expirydates";
                                }

                                mGroupByDeliveryNo = true;
                            }
                            break;
                    }

                    mDsData = pMdtReporter.IV_GIN(cboStore.GetColumnValue("code").ToString(),
                                                    cboStore.GetColumnValue("description").ToString(),
                                                    Convert.ToDateTime(dateEdit1.EditValue),
                                                    Convert.ToDateTime(dateEdit2.EditValue),
                                                    mGroupByCustomer,
                                                    mGroupByDeliveryNo,
                                                    Convert.ToBoolean(chkShowExpiryDates.Checked),
                                                    "",
                                                    "");
                    mIsDataSet = true;
                }

                #endregion

                #region producthistory

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.IVProductHistory)
                {
                    LookUpEdit cboStore = layoutControl1.GetControlByName("cboStore") as LookUpEdit;
                    DateEdit txtDateFrom = layoutControl1.GetControlByName("txtDateFrom") as DateEdit;
                    DateEdit txtDateTo = layoutControl1.GetControlByName("txtDateTo") as DateEdit;

                    if (cboStore.ItemIndex == -1)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreDescriptionIsInvalid.ToString());
                        cboStore.Focus();
                        return;
                    }

                    if (checkEdit1.Checked == false)
                    {
                        if (Program.IsNullDate(txtDateFrom.EditValue) == true)
                        {
                            Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                            txtDateFrom.Focus();
                            return;
                        }

                        if (Program.IsNullDate(txtDateTo.EditValue) == true)
                        {
                            Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                            txtDateTo.Focus();
                            return;
                        }

                        if (Convert.ToDateTime(txtDateTo.EditValue) < Convert.ToDateTime(txtDateFrom.EditValue))
                        {
                            Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateRangeIsInvalid.ToString());
                            txtDateTo.Focus();
                            return;
                        }
                    }

                    mDsData = pMdtReporter.IV_ProductHistory(
                        cboStore.GetColumnValue("code").ToString(),
                        cboStore.GetColumnValue("description").ToString(),
                        textEditSearch.Text,
                        Convert.ToDateTime(txtDateFrom.EditValue),
                        Convert.ToDateTime(txtDateTo.EditValue),
                        !checkEdit1.Checked,
                        "", "");

                    mIsDataSet = true;
                }

                #endregion

                #region IVPriceList

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.IVPriceList)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    if (lookUpEdit1.ItemIndex == -1)
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreDescriptionIsInvalid.ToString());
                        lookUpEdit1.Focus();
                        return;
                    }

                    #region categories

                    string mCategories = "";
                    DataTable mDtCategories = (DataTable)gridControl1.DataSource;

                    foreach (DataRow mDataRow in mDtCategories.Rows)
                    {
                        if (Convert.ToBoolean(mDataRow["selected"]) == true)
                        {
                            if (mCategories.Trim() == "")
                            {
                                mCategories = "'" + mDataRow["code"].ToString().Trim() + "'";
                            }
                            else
                            {
                                mCategories = mCategories + ",'" + mDataRow["code"].ToString().Trim() + "'";
                            }
                        }
                    }

                    if (mCategories.Trim() == "")
                    {
                        mCategories = "1=2";
                    }
                    else
                    {
                        mCategories = "departmentcode in (" + mCategories + ")";
                    }

                    #endregion

                    mExtraFilter = mCategories;

                    mDtData = pMdtReporter.IV_PriceList(lookUpEdit1.GetColumnValue("code").ToString(),
                        lookUpEdit1.GetColumnValue("description").ToString(), mExtraFilter, mExtraParameters);
                    mIsDataSet = false;
                }

                #endregion

                #endregion

                #region laboratory

                #region LABPatientTestsCount

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.LABPatientTestsCount)
                {
                    mDsData = pMdtReporter.LAB_PatientTestsCount(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, "", "");
                    mIsDataSet = true;
                }

                #endregion

                #region LABCountByResult

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.LABCountByResult)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    #region age
                    if (layoutControl1.GetItemByControl(comboBoxEdit1).Visible == true)
                    {
                        if (comboBoxEdit1.Text.Trim() != "" && Program.IsMoney(textEdit1.Text) != false)
                        {
                            string mPatientAge = pMdtReporter.Age_Formula("p.birthdate", "transdate", "");
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + comboBoxEdit1.Text + " " + textEdit1.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region extra patient details

                    DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");
                    foreach (DataRow mDataRow in mDtExtraFields.Rows)
                    {
                        string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                        string mFieldCaption = mDataRow["fieldcaption"].ToString();
                        string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                        string mDataType = mDataRow["datatype"].ToString().Trim();

                        if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mComboBoxEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mComboBoxEdit.Text.Trim() != "")
                                {
                                    if (mExtraFilter.Trim() == "")
                                    {
                                        mExtraFilter = "(patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }
                                    else
                                    {
                                        mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }

                                    mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mComboBoxEdit.Text;
                                }
                                else
                                {
                                    mExtraParameters = mExtraParameters + Environment.NewLine
                                        + mLayoutControlItem.Text.ToUpper()
                                        + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                }
                            }
                            #endregion
                        }
                        else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().ToLower())
                        {
                            CheckEdit mCheckEdit = layoutControl1.GetControlByName("chk" + mFieldName) as CheckEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mCheckEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mExtraFilter.Trim() == "")
                                {
                                    mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }
                                else
                                {
                                    mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }

                                mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + Convert.ToInt16(mCheckEdit.Checked);
                            }
                            #endregion
                        }
                        else
                        {
                            if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
                            {
                                DateEdit mDateEdit = layoutControl1.GetControlByName("txt" + mFieldName) as DateEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mDateEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsNullDate(mDateEdit.EditValue) == false)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDateTime(mDateEdit.EditValue).ToString("d");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mTextEdit.Text.Trim();
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    #endregion

                    mDsData = pMdtReporter.LAB_CountByResult(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mExtraFilter, mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region LABListingByResult

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.LABListingByResult)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    #region age
                    if (layoutControl1.GetItemByControl(comboBoxEdit1).Visible == true)
                    {
                        if (comboBoxEdit1.Text.Trim() != "" && Program.IsMoney(textEdit1.Text) != false)
                        {
                            string mPatientAge = pMdtReporter.Age_Formula("p.birthdate", "transdate", "");
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + comboBoxEdit1.Text + " " + textEdit1.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region extra patient details

                    DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");
                    foreach (DataRow mDataRow in mDtExtraFields.Rows)
                    {
                        string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                        string mFieldCaption = mDataRow["fieldcaption"].ToString();
                        string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                        string mDataType = mDataRow["datatype"].ToString().Trim();

                        if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mComboBoxEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mComboBoxEdit.Text.Trim() != "")
                                {
                                    if (mExtraFilter.Trim() == "")
                                    {
                                        mExtraFilter = "(patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }
                                    else
                                    {
                                        mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }

                                    mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mComboBoxEdit.Text;
                                }
                                else
                                {
                                    mExtraParameters = mExtraParameters + Environment.NewLine
                                        + mLayoutControlItem.Text.ToUpper()
                                        + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                }
                            }
                            #endregion
                        }
                        else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().ToLower())
                        {
                            CheckEdit mCheckEdit = layoutControl1.GetControlByName("chk" + mFieldName) as CheckEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mCheckEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mExtraFilter.Trim() == "")
                                {
                                    mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }
                                else
                                {
                                    mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }

                                mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + Convert.ToInt16(mCheckEdit.Checked);
                            }
                            #endregion
                        }
                        else
                        {
                            if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
                            {
                                DateEdit mDateEdit = layoutControl1.GetControlByName("txt" + mFieldName) as DateEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mDateEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsNullDate(mDateEdit.EditValue) == false)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDateTime(mDateEdit.EditValue).ToString("d");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mTextEdit.Text.Trim();
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    #endregion

                    mDtData = pMdtReporter.LAB_ListingByResult(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mExtraFilter, mExtraParameters);
                    mIsDataSet = false;
                }

                #endregion

                #region LABCountMonthly

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.LABCountMonthly)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    if (Program.IsNumeric(textEdit1.Text) == false)
                    {
                        Program.Display_Error("Invalid entry for Year");
                        textEdit1.Focus();
                        return;
                    }

                    if (comboBoxEdit1.SelectedIndex == -1)
                    {
                        Program.Display_Error("Invalid month selection");
                        comboBoxEdit1.Focus();
                        return;
                    }

                    if (comboBoxEdit2.SelectedIndex == -1)
                    {
                        Program.Display_Error("Invalid month selection");
                        comboBoxEdit2.Focus();
                        return;
                    }

                    #region lab tests

                    DataTable mDtTests = new DataTable("selectedtests");
                    mDtTests.Columns.Add("code", typeof(System.String));

                    foreach (DataRow mDataRow in ((DataTable)gridControl1.DataSource).Rows)
                    {
                        if (Convert.ToBoolean(mDataRow["selected"]) == true)
                        {
                            DataRow mNewRow = mDtTests.NewRow();
                            mNewRow["code"] = mDataRow["code"];
                            mDtTests.Rows.Add(mNewRow);
                            mDtTests.AcceptChanges();
                        }
                    }

                    if (mDtTests.Rows.Count == 0)
                    {
                        Program.Display_Error("Please select at least one Lab Test and try again", false);
                        return;
                    }

                    #endregion

                    mDsData = pMdtReporter.LAB_CountMonthly(
                        Convert.ToInt32(textEdit1.Text),
                        comboBoxEdit1.SelectedIndex + 1,
                        comboBoxEdit2.SelectedIndex + 1,
                        mDtTests,
                        mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region Lab test count by user

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.LABPatientCountByLabTechnician)
                {
                    string mExtraFilter = "";

                    #region Lab Technician Id
                    if (layoutControl1.GetItemByControl(lookUpEdit1).Visible == true)
                    {
                        string mLabTechnicianCode = "";
                        string mLabTechnicianDescription = "";

                        if (lookUpEdit1.ItemIndex != -1)
                        {
                            mLabTechnicianCode = lookUpEdit1.GetColumnValue("code").ToString().Trim();
                            mLabTechnicianDescription = lookUpEdit1.GetColumnValue("description").ToString().Trim();
                        }

                        if (mLabTechnicianCode.Trim() != "")
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "labtechniciancode='" + mLabTechnicianCode + "' ";
                            }
                        }

                    }
                    #endregion

                    mDsData = pMdtReporter.LAB_CountByLabTechnician(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mExtraFilter, "");

                    mIsDataSet = true;
                }

                #endregion

                #endregion

                #region rch

                #region attendancelist

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.RCH_AttendanceList)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";
                    int mServiceType = 0;

                    #region service type

                    LookUpEdit cboServiceType = (LookUpEdit)layoutControl1.GetControlByName("cboServiceType");
                    if (cboServiceType.ItemIndex <= 0)
                    {
                        Program.Display_Error("Invalid service type selection");
                        cboServiceType.Focus();
                        return;
                    }

                    mExtraParameters = mExtraParameters + Environment.NewLine
                        + layoutControl1.GetItemByControl(cboServiceType).Text
                        + ": " + cboServiceType.GetColumnValue("description").ToString();

                    string mServiceTypeFieldName = cboServiceType.GetColumnValue("code").ToString();

                    if (mServiceTypeFieldName.ToLower() == AfyaPro_Types.clsEnums.RCHServices.familyplanning.ToString().ToLower())
                    {
                        mServiceType = (int)AfyaPro_Types.clsEnums.RCHServices.familyplanning;
                    }
                    else if (mServiceTypeFieldName.ToLower() == AfyaPro_Types.clsEnums.RCHServices.antenatalcare.ToString().ToLower())
                    {
                        mServiceType = (int)AfyaPro_Types.clsEnums.RCHServices.antenatalcare;
                    }
                    else if (mServiceTypeFieldName.ToLower() == AfyaPro_Types.clsEnums.RCHServices.postnatalcare.ToString().ToLower())
                    {
                        mServiceType = (int)AfyaPro_Types.clsEnums.RCHServices.postnatalcare;
                    }
                    else if (mServiceTypeFieldName.ToLower() == AfyaPro_Types.clsEnums.RCHServices.childrenhealth.ToString().ToLower())
                    {
                        mServiceType = (int)AfyaPro_Types.clsEnums.RCHServices.childrenhealth;
                    }
                    else if (mServiceTypeFieldName.ToLower() == AfyaPro_Types.clsEnums.RCHServices.vaccinations.ToString().ToLower())
                    {
                        mServiceType = (int)AfyaPro_Types.clsEnums.RCHServices.vaccinations;
                    }

                    #endregion

                    #region age
                    if (layoutControl1.GetItemByControl(comboBoxEdit1).Visible == true)
                    {
                        if (comboBoxEdit1.Text.Trim() != "" && Program.IsMoney(textEdit1.Text) != false)
                        {
                            string mPatientAge = pMdtReporter.Age_Formula("p.birthdate", "bookdate", "");
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (" + mPatientAge + comboBoxEdit1.Text + " " + textEdit1.Text + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + comboBoxEdit1.Text + " " + textEdit1.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region gender
                    if (layoutControl1.GetItemByControl(comboBoxEdit2).Visible == true)
                    {
                        if (comboBoxEdit2.Text.Trim() != "")
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(p.gender='" + comboBoxEdit2.Text + "')";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (p.gender='" + comboBoxEdit2.Text + "')";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text + ": " + comboBoxEdit2.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region extra patient details

                    DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");
                    foreach (DataRow mDataRow in mDtExtraFields.Rows)
                    {
                        string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                        string mFieldCaption = mDataRow["fieldcaption"].ToString();
                        string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                        string mDataType = mDataRow["datatype"].ToString().Trim();

                        if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mComboBoxEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mComboBoxEdit.Text.Trim() != "")
                                {
                                    if (mExtraFilter.Trim() == "")
                                    {
                                        mExtraFilter = "(patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }
                                    else
                                    {
                                        mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }

                                    mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mComboBoxEdit.Text;
                                }
                                else
                                {
                                    mExtraParameters = mExtraParameters + Environment.NewLine
                                        + mLayoutControlItem.Text.ToUpper()
                                        + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                }
                            }
                            #endregion
                        }
                        else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().ToLower())
                        {
                            CheckEdit mCheckEdit = layoutControl1.GetControlByName("chk" + mFieldName) as CheckEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mCheckEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mExtraFilter.Trim() == "")
                                {
                                    mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }
                                else
                                {
                                    mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }

                                mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + Convert.ToInt16(mCheckEdit.Checked);
                            }
                            #endregion
                        }
                        else
                        {
                            if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
                            {
                                DateEdit mDateEdit = layoutControl1.GetControlByName("txt" + mFieldName) as DateEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mDateEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsNullDate(mDateEdit.EditValue) == false)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDateTime(mDateEdit.EditValue).ToString("d");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mTextEdit.Text.Trim();
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    #endregion

                    mDtData = pMdtReporter.RCH_AttendanceList(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mServiceType, mExtraFilter, mExtraParameters);
                }

                #endregion

                #region vaccinationscount

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.RCH_VaccinationCount)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";
                    int mServiceType = -1;

                    #region service type

                    LookUpEdit cboServiceType = (LookUpEdit)layoutControl1.GetControlByName("cboServiceType");

                    string mServiceTypeFieldName = cboServiceType.ItemIndex <= 0 ? "" : cboServiceType.GetColumnValue("code").ToString();
                    string mServiceTypeDescription = cboServiceType.ItemIndex <= 0 ? "All Services" : cboServiceType.GetColumnValue("description").ToString();

                    mExtraParameters = mExtraParameters + Environment.NewLine
                        + layoutControl1.GetItemByControl(cboServiceType).Text
                        + ": " + mServiceTypeDescription;

                    if (mServiceTypeFieldName.ToLower() == AfyaPro_Types.clsEnums.RCHServices.familyplanning.ToString().ToLower())
                    {
                        mServiceType = (int)AfyaPro_Types.clsEnums.RCHServices.familyplanning;
                    }
                    else if (mServiceTypeFieldName.ToLower() == AfyaPro_Types.clsEnums.RCHServices.antenatalcare.ToString().ToLower())
                    {
                        mServiceType = (int)AfyaPro_Types.clsEnums.RCHServices.antenatalcare;
                    }
                    else if (mServiceTypeFieldName.ToLower() == AfyaPro_Types.clsEnums.RCHServices.postnatalcare.ToString().ToLower())
                    {
                        mServiceType = (int)AfyaPro_Types.clsEnums.RCHServices.postnatalcare;
                    }
                    else if (mServiceTypeFieldName.ToLower() == AfyaPro_Types.clsEnums.RCHServices.childrenhealth.ToString().ToLower())
                    {
                        mServiceType = (int)AfyaPro_Types.clsEnums.RCHServices.childrenhealth;
                    }
                    else if (mServiceTypeFieldName.ToLower() == AfyaPro_Types.clsEnums.RCHServices.vaccinations.ToString().ToLower())
                    {
                        mServiceType = (int)AfyaPro_Types.clsEnums.RCHServices.vaccinations;
                    }

                    #endregion

                    #region age
                    if (layoutControl1.GetItemByControl(comboBoxEdit1).Visible == true)
                    {
                        TextEdit txtAgeYears = (TextEdit)layoutControl1.GetControlByName("txtAgeLimit");
                        TextEdit txtAgeMonths = (TextEdit)layoutControl1.GetControlByName("txtAgeMonths");

                        decimal mAgeYears = 0;
                        decimal mAgeMonths = 0;
                        decimal mAge = 0;

                        if (Program.IsMoney(txtAgeYears.Text) == true)
                        {
                            mAgeYears = Convert.ToDecimal(txtAgeYears.Text);
                        }

                        if (Program.IsMoney(txtAgeMonths.Text) == true)
                        {
                            mAgeMonths = Convert.ToDecimal(txtAgeMonths.Text);
                        }

                        mAge = mAgeYears + (mAgeMonths / 12);

                        if (comboBoxEdit1.Text.Trim() != "" && Program.IsMoney(textEdit1.Text) != false)
                        {
                            string mPatientAge = pMdtReporter.Age_Formula("p.birthdate", "bookdate", "");
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(" + mPatientAge + comboBoxEdit1.Text + " " + mAge + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (" + mPatientAge + comboBoxEdit1.Text + " " + mAge + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + comboBoxEdit1.Text + " " + txtAgeYears.Text + " Years, " + txtAgeMonths.Text + " Months";
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region gender
                    if (layoutControl1.GetItemByControl(comboBoxEdit2).Visible == true)
                    {
                        if (comboBoxEdit2.Text.Trim() != "")
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(p.gender='" + comboBoxEdit2.Text + "')";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (p.gender='" + comboBoxEdit2.Text + "')";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text + ": " + comboBoxEdit2.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region extra patient details

                    DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");
                    foreach (DataRow mDataRow in mDtExtraFields.Rows)
                    {
                        string mFieldName = mDataRow["fieldname"].ToString().Trim().ToLower();
                        string mFieldCaption = mDataRow["fieldcaption"].ToString();
                        string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                        string mDataType = mDataRow["datatype"].ToString().Trim();

                        if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            ComboBoxEdit mComboBoxEdit = layoutControl1.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mComboBoxEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mComboBoxEdit.Text.Trim() != "")
                                {
                                    if (mExtraFilter.Trim() == "")
                                    {
                                        mExtraFilter = "(patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }
                                    else
                                    {
                                        mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mComboBoxEdit.Text + "')";
                                    }

                                    mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mComboBoxEdit.Text;
                                }
                                else
                                {
                                    mExtraParameters = mExtraParameters + Environment.NewLine
                                        + mLayoutControlItem.Text.ToUpper()
                                        + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                }
                            }
                            #endregion
                        }
                        else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().ToLower())
                        {
                            CheckEdit mCheckEdit = layoutControl1.GetControlByName("chk" + mFieldName) as CheckEdit;
                            LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mCheckEdit);

                            #region build filter

                            if (mLayoutControlItem.Visible == true)
                            {
                                if (mExtraFilter.Trim() == "")
                                {
                                    mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }
                                else
                                {
                                    mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToInt16(mCheckEdit.Checked) + ")";
                                }

                                mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + Convert.ToInt16(mCheckEdit.Checked);
                            }
                            #endregion
                        }
                        else
                        {
                            if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
                            {
                                DateEdit mDateEdit = layoutControl1.GetControlByName("txt" + mFieldName) as DateEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mDateEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsNullDate(mDateEdit.EditValue) == false)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='"
                                                + Convert.ToDateTime(mDateEdit.EditValue).ToString("d") + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDateTime(mDateEdit.EditValue).ToString("d");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().ToLower())
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "=" + Convert.ToDouble(mTextEdit.Text) + ")";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": "
                                            + Convert.ToDouble(mTextEdit.Text).ToString("c");
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                TextEdit mTextEdit = layoutControl1.GetControlByName("txt" + mFieldName) as TextEdit;
                                LayoutControlItem mLayoutControlItem = layoutControl1.GetItemByControl(mTextEdit);

                                #region build filter

                                if (mLayoutControlItem.Visible == true)
                                {
                                    if (Program.IsMoney(mTextEdit.Text) == true)
                                    {
                                        if (mExtraFilter.Trim() == "")
                                        {
                                            mExtraFilter = "(patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }
                                        else
                                        {
                                            mExtraFilter = mExtraFilter + " and (patient" + mFieldName + "='" + mTextEdit.Text.Trim() + "')";
                                        }

                                        mExtraParameters = mExtraParameters + Environment.NewLine + mLayoutControlItem.Text.ToUpper() + ": " + mTextEdit.Text.Trim();
                                    }
                                    else
                                    {
                                        mExtraParameters = mExtraParameters + Environment.NewLine
                                            + mLayoutControlItem.Text.ToUpper()
                                            + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    #endregion

                    mDtData = pMdtReporter.RCH_VaccinationsCount(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mServiceType, mExtraFilter, mExtraParameters);
                }

                #endregion

                #region RCH_FPlanMethodsCount

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.RCH_FPlanMethodsCount)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    #region age
                    if (layoutControl1.GetItemByControl(comboBoxEdit1).Visible == true)
                    {
                        TextEdit txtAgeYears = (TextEdit)layoutControl1.GetControlByName("txtAgeLimit");
                        TextEdit txtAgeMonths = (TextEdit)layoutControl1.GetControlByName("txtAgeMonths");

                        decimal mAgeYears = 0;
                        decimal mAgeMonths = 0;
                        decimal mAge = 0;

                        if (Program.IsMoney(txtAgeYears.Text) == true)
                        {
                            mAgeYears = Convert.ToDecimal(txtAgeYears.Text);
                        }

                        if (Program.IsMoney(txtAgeMonths.Text) == true)
                        {
                            mAgeMonths = Convert.ToDecimal(txtAgeMonths.Text);
                        }

                        mAge = mAgeYears + (mAgeMonths / 12);

                        if (comboBoxEdit1.Text.Trim() != "" && Program.IsMoney(textEdit1.Text) != false)
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(patientage " + comboBoxEdit1.Text + " " + mAge + ")";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (patientage " + comboBoxEdit1.Text + " " + mAge + ")";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + comboBoxEdit1.Text + " " + txtAgeYears.Text + " Years, " + txtAgeMonths.Text + " Months";
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit1).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    #region gender
                    if (layoutControl1.GetItemByControl(comboBoxEdit2).Visible == true)
                    {
                        if (comboBoxEdit2.Text.Trim() != "")
                        {
                            if (mExtraFilter.Trim() == "")
                            {
                                mExtraFilter = "(gender='" + comboBoxEdit2.Text + "')";
                            }
                            else
                            {
                                mExtraFilter = mExtraFilter + " and (gender='" + comboBoxEdit2.Text + "')";
                            }

                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text + ": " + comboBoxEdit2.Text;
                        }
                        else
                        {
                            mExtraParameters = mExtraParameters + Environment.NewLine
                                + layoutControl1.GetItemByControl(comboBoxEdit2).Text
                                + ": " + Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SystemFilterNone.ToString());
                        }
                    }
                    #endregion

                    mDtData = pMdtReporter.RCH_FPlanMethodsCount(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mExtraFilter, mExtraParameters);
                }

                #endregion

                #region RCH_AntenatalAtt

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.RCH_AntenatalAtt)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    mDsData = pMdtReporter.RCH_AntenatalAtt(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mExtraFilter, mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region RCH_PostnatalAtt

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.RCH_PostnatalAtt)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    mDsData = pMdtReporter.RCH_PostnatalAtt(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), true, mExtraFilter, mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region RCH_ANCMonthlySummary

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.RCH_ANCMonthlySummary)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    if (comboBoxEdit2.SelectedIndex == -1)
                    {
                        Program.Display_Error("Invalid entry for Year");
                        comboBoxEdit2.Focus();
                        return;
                    }

                    if (comboBoxEdit1.SelectedIndex == -1)
                    {
                        Program.Display_Error("Invalid month selection");
                        comboBoxEdit1.Focus();
                        return;
                    }

                    mDsData = pMdtReporter.RCH_ANCMonthlySummary(
                        Convert.ToInt32(comboBoxEdit2.Text),
                        comboBoxEdit1.Text,
                        mExtraFilter,
                        mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region RCH_MaternityCMonthlySummary

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.RCH_MaternityMonthlySummary)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    if (comboBoxEdit2.SelectedIndex == -1)
                    {
                        Program.Display_Error("Invalid entry for Year");
                        comboBoxEdit2.Focus();
                        return;
                    }

                    if (comboBoxEdit1.SelectedIndex == -1)
                    {
                        Program.Display_Error("Invalid month selection");
                        comboBoxEdit1.Focus();
                        return;
                    }

                    mDsData = pMdtReporter.RCH_MaternityMonthlySummary(
                        Convert.ToInt32(comboBoxEdit2.Text),
                        comboBoxEdit1.Text,
                        mExtraFilter,
                        mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region RCH_ObstetricMonthlySummary

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.RCH_ObstetricMonthlyReport)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    if (comboBoxEdit2.SelectedIndex == -1)
                    {
                        Program.Display_Error("Invalid entry for Year");
                        comboBoxEdit2.Focus();
                        return;
                    }

                    if (comboBoxEdit1.SelectedIndex == -1)
                    {
                        Program.Display_Error("Invalid month selection");
                        comboBoxEdit1.Focus();
                        return;
                    }

                    mDsData = pMdtReporter.RCH_ObstetricMonthlySummary(
                        Convert.ToInt32(comboBoxEdit2.Text),
                        comboBoxEdit1.Text,
                        mExtraFilter,
                        mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #endregion

                #region ctc/art

                #region CTCAttendanceCountAge

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.CTCAttendanceCountAge)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    DateEdit txtDateFrom = (DateEdit)layoutControl1.GetControlByName("txtDateFrom");
                    DateEdit txtDateTo = (DateEdit)layoutControl1.GetControlByName("txtDateTo");

                    mDsData = pMdtReporter.CTC_AttendanceCountAge(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), mExtraFilter, mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion

                #region CTCAttendanceCD4T

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.CTCAttendanceCD4T)
                {
                    string mExtraFilter = "";
                    string mExtraParameters = "";

                    DateEdit txtDateFrom = (DateEdit)layoutControl1.GetControlByName("txtDateFrom");
                    DateEdit txtDateTo = (DateEdit)layoutControl1.GetControlByName("txtDateTo");

                    mDsData = pMdtReporter.CTC_AttendanceCD4AgeT(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), mExtraFilter, mExtraParameters);
                    mIsDataSet = true;
                }

                #endregion


                #region CTCTestingAndCounselingMonthlySummary

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.CTCTestingAndCounseling)
                {
                    if (fillCTCTestingAndCounselingMonthlySummary(ref mDsData) == false)
                        return;
                    mIsDataSet = true;
                }

                #endregion

                #region CTCExposedChildrenMonthlyReport

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.CTCExposedChildrenMonthlyReport)
                {
                    if (fillCTCExposedChildrenMonthlyReport(ref mDsData) == false)
                        return;
                    mIsDataSet = true;
                }
                #endregion

                #region CTCDNAPCRMonthlyReport

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.CTCDNAPCRMonthlyReport)
                {
                    if (fillCTCDNAPCRMonthlyReport(ref mDsData) == false)
                        return;
                    mIsDataSet = true;
                }

                #endregion

                #region CTC_QuarterlySupervision
                mFunctionName = mFunctionName + "  CTC_QuarterlySupervision";
                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.CTCARTClinicQuarterlySupervision)
                {

                    string mFromDate = "";
                    string mToDate = "";

                    string mFirstMonthInQuarter = "";
                    string mSecondMonthInQuarter = "";
                    string mThirdMonthInQuarter = "";

                    string mFirstMonth = "";
                    string mSecondMonth = "";
                    string mThirdMonth = "";

                    DateTime mQuarterStartDate = Convert.ToDateTime("01/01/1900");
                    DateTime mQuarterEndDate = Convert.ToDateTime("01/01/1900");

                    if (Program.IsNumeric(comboBoxEdit1.Text) == false)
                    {
                        Program.Display_Error("Invalid entry for year");
                        textEdit1.Focus();
                        return;
                    }

                    if (comboBoxEdit2.SelectedIndex == -1)
                    {
                        Program.Display_Error("Invalid Entry for Quarter");
                        comboBoxEdit1.Focus();
                        return;
                    }

                    if (comboBoxEdit2.Text == "Q1")
                    {
                        mFromDate = "01/01";
                        mToDate = "03/31";

                        mQuarterStartDate = Convert.ToDateTime(comboBoxEdit1.Text + "/01/01");
                        mQuarterEndDate = Convert.ToDateTime(comboBoxEdit1.Text + "/03/31");

                        mFirstMonthInQuarter = "January " + comboBoxEdit1.Text;
                        mSecondMonthInQuarter = "February " + comboBoxEdit1.Text;
                        mThirdMonthInQuarter = "March " + comboBoxEdit1.Text;

                        mFirstMonth = "January";
                        mSecondMonth = "February";
                        mThirdMonth = "March";
                    }

                    if (comboBoxEdit2.Text == "Q2")
                    {
                        mFromDate = "04/01";
                        mToDate = "06/30";

                        mQuarterStartDate = Convert.ToDateTime(comboBoxEdit1.Text + "/04/01");
                        mQuarterEndDate = Convert.ToDateTime(comboBoxEdit1.Text + "/06/30");

                        mFirstMonthInQuarter = "April " + comboBoxEdit1.Text;
                        mSecondMonthInQuarter = "May " + comboBoxEdit1.Text;
                        mThirdMonthInQuarter = "June " + comboBoxEdit1.Text;

                        mFirstMonth = "April";
                        mSecondMonth = "May";
                        mThirdMonth = "June";

                    }

                    if (comboBoxEdit2.Text == "Q3")
                    {
                        mFromDate = "07/01";
                        mToDate = "09/30";

                        mQuarterStartDate = Convert.ToDateTime(comboBoxEdit1.Text + "/07/01");
                        mQuarterEndDate = Convert.ToDateTime(comboBoxEdit1.Text + "/09/30");

                        mFirstMonthInQuarter = "July " + comboBoxEdit1.Text;
                        mSecondMonthInQuarter = "August " + comboBoxEdit1.Text;
                        mThirdMonthInQuarter = "September " + comboBoxEdit1.Text;

                        mFirstMonth = "July";
                        mSecondMonth = "August";
                        mThirdMonth = "September";

                    }

                    if (comboBoxEdit2.Text == "Q4")
                    {
                        mFromDate = "10/01";
                        mToDate = "12/31";

                        mQuarterStartDate = Convert.ToDateTime(comboBoxEdit1.Text + "/10/01");
                        mQuarterEndDate = Convert.ToDateTime(comboBoxEdit1.Text + "/12/31");

                        mFirstMonthInQuarter = "October " + comboBoxEdit1.Text;
                        mSecondMonthInQuarter = "November " + comboBoxEdit1.Text;
                        mThirdMonthInQuarter = "December " + comboBoxEdit1.Text;

                        mFirstMonth = "October";
                        mSecondMonth = "November";
                        mThirdMonth = "December";
                    }
                    mDsData = pMdtReporter.CTC_ARTCLinicQuarterlySupervisionForm(
                        Convert.ToInt32(comboBoxEdit1.Text),
                        comboBoxEdit2.Text,
                        mFromDate, mToDate, mQuarterStartDate, mQuarterEndDate, mFirstMonthInQuarter, mSecondMonthInQuarter, mThirdMonthInQuarter, mFirstMonth, mSecondMonth, mThirdMonth);
                    mIsDataSet = true;
                }

                #endregion

                #endregion

                #region User Login Details

                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.UserLoginDetailReport)
                {
                    mDsData = pMdtReporter.USER_LoginDetails(
                        Convert.ToDateTime(dateEdit1.EditValue),
                        Convert.ToDateTime(dateEdit2.EditValue), "", "");
                    mIsDataSet = true;
                }

                #endregion

                #endregion

                string mFileName = mReportCode;
                byte[] mBytes = pMdtReporter.Load_ReportTemplate(mFileName, false);
                DevExpress.XtraReports.UI.XtraReport mReportDoc = null;

                if (mIsDataSet == true)
                {
                    mReportDoc = Program.Load_ReportTemplate(mBytes, mDsData);
                }
                else
                {
                    mReportDoc = Program.Load_ReportTemplate(mBytes, mDtData);
                }

                if (mReportDoc == null)
                {
                    Program.gMdiForm.Cursor = Cursors.Default;
                    return;
                }

                // Add a new command handler which saves a report in a custom way.
                mDesignPanel.AddCommandHandler(new clsReportDesignerSaveCommandHandler(mDesignPanel, mFileName));

                // Load a report into the design form and show the form.
                mDesignPanel.OpenReport(mReportDoc);
                mDesignForm.ShowDialog();
                mDesignPanel.CloseReport();

                this.Cursor = Cursors.Default;
            }

            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region searchfacility

        #region Display_SearchData
        private void Display_SearchData()
        {
            string mFunctionName = "Display_SearchData";

            try
            {
                DataTable mDtData = new DataTable();

                #region DebtorStatement
                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.DebtorStatement)
                {
                    string mDebtorType = AfyaPro_Types.clsEnums.DebtorTypes.Individual.ToString().Trim();
                    if (radioGroup1.SelectedIndex == 1)
                    {
                        mDebtorType = AfyaPro_Types.clsEnums.DebtorTypes.Group.ToString().Trim();
                    }

                    mDtData = pMdtReporter.View_Data("billdebtors",
                        "accountcode='" + textEditSearch.Text.Trim() + "' and debtortype='" + mDebtorType + "'", "", "", "");
                    if (mDtData.Rows.Count > 0)
                    {
                        textEditSearch.Text = mDtData.Rows[0]["accountcode"].ToString();
                        textEdit1.Text = mDtData.Rows[0]["accountdescription"].ToString();
                    }
                }
                #endregion

                #region IVProductHistory
                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.IVProductHistory)
                {
                    mDtData = pMdtReporter.View_Data("som_products",
                        "code='" + textEditSearch.Text.Trim() + "'", "", "", "");
                    if (mDtData.Rows.Count > 0)
                    {
                        textEditSearch.Text = mDtData.Rows[0]["code"].ToString();
                        textEdit1.Text = mDtData.Rows[0]["description"].ToString();
                    }
                }
                #endregion

                #region DXTPatientHistory
                if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.DXTPatientHistory)
                {
                    mDtData = pMdtReporter.View_Data("patients", "code='" + textEditSearch.Text.Trim() + "'", "", "", "");
                    if (mDtData.Rows.Count > 0)
                    {
                        TextEdit txtPatientId = (TextEdit)layoutControl1.GetControlByName("txtPatientId");
                        TextEdit txtName = (TextEdit)layoutControl1.GetControlByName("txtPatientName");
                        TextEdit txtYears = (TextEdit)layoutControl1.GetControlByName("txtYears");
                        TextEdit txtMonths = (TextEdit)layoutControl1.GetControlByName("txtMonths");
                        TextEdit txtGender = (TextEdit)layoutControl1.GetControlByName("txtGender");

                        txtPatientId.Text = mDtData.Rows[0]["code"].ToString();
                        string mFullName = mDtData.Rows[0]["firstname"].ToString();
                        if (mDtData.Rows[0]["othernames"].ToString().Trim() != "")
                        {
                            mFullName = mFullName + " " + mDtData.Rows[0]["othernames"].ToString();
                        }
                        mFullName = mFullName + " " + mDtData.Rows[0]["surname"].ToString();
                        txtName.Text = mFullName;
                        if (mDtData.Rows[0]["gender"].ToString().Trim().ToLower() == "f")
                        {
                            txtGender.Text = "Female";
                        }
                        else
                        {
                            txtGender.Text = "Male";
                        }
                        DateTime mBirthDate = Convert.ToDateTime(mDtData.Rows[0]["birthdate"]);
                        int mDays = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue).Subtract(mBirthDate).Days;
                        int mYears = (int)mDays / 365;
                        int mMonths = (int)(mDays % 365) / 30;

                        txtYears.Text = mYears.ToString();
                        txtMonths.Text = mMonths.ToString();
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

        private void simpleButtonSearch_Click(object sender, EventArgs e)
        {
            pSearching = true;

            #region Debtor
            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.DebtorStatement)
            {
                frmSearchDebtor mSearchDebtor = new frmSearchDebtor(textEditSearch);
                if (radioGroup1.SelectedIndex == 0)
                {
                    mSearchDebtor.DebtorType = AfyaPro_Types.clsEnums.DebtorTypes.Individual.ToString();
                }
                else
                {
                    mSearchDebtor.DebtorType = AfyaPro_Types.clsEnums.DebtorTypes.Group.ToString();
                }
                mSearchDebtor.ShowDialog();
            }
            #endregion

            #region Product
            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.IVProductHistory)
            {
                pSearching = true;

                frmSearchProduct mSearchProduct = new frmSearchProduct();
                mSearchProduct.ShowDialog();
                if (mSearchProduct.SearchingDone == true)
                {
                    TextEdit txtProductCode = layoutControl1.GetControlByName("txtProductCode") as TextEdit;
                    txtProductCode.Text = mSearchProduct.SelectedRow["code"].ToString().Trim();
                }

                pSearching = false;
            }
            #endregion

            #region DXTPatientHistory
            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.DXTPatientHistory)
            {
                TextEdit txtPatientId = (TextEdit)layoutControl1.GetControlByName("txtPatientId");
                frmSearchPatient mSearchPatient = new frmSearchPatient(txtPatientId);
                mSearchPatient.ShowDialog();
            }
            #endregion

            pSearching = false;
        }

        private void textEditSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Display_SearchData();
            }
        }

        private void textEditSearch_Leave(object sender, EventArgs e)
        {
            pCurrSearchStr = textEditSearch.Text;

            if (pCurrSearchStr.Trim().ToLower() != pPrevSearchStr.Trim().ToLower())
            {
                this.Display_SearchData();
            }
        }

        private void textEditSearch_EditValueChanged(object sender, EventArgs e)
        { 
            if (pSearching == true)
            {
                this.Display_SearchData();
            }
        }

        #endregion

        #region simpleButton_Click
        private void simpleButton_Click(object sender, EventArgs e)
        {
            pSearching = true;

            #region attendancecountage
            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.AttendanceCountAge)
            {
                frmRPDAgeGroups mRPDAgeGroups = new frmRPDAgeGroups("REP" + Convert.ToInt16(AfyaPro_Types.clsEnums.BuiltInReports.AttendanceCountAge));
                mRPDAgeGroups.ShowDialog();
            }
            #endregion

            #region CTCAttendanceCountAge
            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.CTCAttendanceCountAge)
            {
                frmRPDAgeGroups mRPDAgeGroups = new frmRPDAgeGroups("REP" + Convert.ToInt16(AfyaPro_Types.clsEnums.BuiltInReports.CTCAttendanceCountAge));
                mRPDAgeGroups.ShowDialog();
            }
            #endregion

            #region CTCAttendanceCD4T
            if (pReportCode.Trim().ToLower() == "rep" + (int)AfyaPro_Types.clsEnums.BuiltInReports.CTCAttendanceCD4T)
            {
                frmRPDAgeGroups mRPDAgeGroups = new frmRPDAgeGroups("REP" + Convert.ToInt16(AfyaPro_Types.clsEnums.BuiltInReports.CTCAttendanceCD4T));
                mRPDAgeGroups.ShowDialog();
            }
            #endregion

            pSearching = false;
        }
        #endregion

        #region checkedit and grid checking

        #region checkEdit1_CheckedChanged
        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (pCheckedCheckBox == true)
            {
                DataTable mDataTable = (DataTable)gridControl1.DataSource;
                foreach (DataRow mDataRow in mDataTable.Rows)
                {
                    mDataRow.BeginEdit();
                    mDataRow["selected"] = checkEdit1.Checked;
                    mDataRow.EndEdit();
                }
            }
        }
        #endregion

        #region mCheckEdit_CheckedChanged
        void mCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (pCheckedGrid == true)
            {
                gridControl1.FocusedView.PostEditor();

                int mChecked = 0;
                int mUnChecked = 0;

                DataTable mDataTable = (DataTable)gridControl1.DataSource;
                foreach (DataRow mDataRow in mDataTable.Rows)
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

                if (mChecked == mDataTable.Rows.Count)
                {
                    checkEdit1.CheckState = CheckState.Checked;
                }
                else if (mUnChecked == mDataTable.Rows.Count)
                {
                    checkEdit1.CheckState = CheckState.Unchecked;
                }
                else
                {
                    checkEdit1.CheckState = CheckState.Indeterminate;
                }
            }
        }
        #endregion

        #region checkEdit1_Enter
        private void checkEdit1_Enter(object sender, EventArgs e)
        {
            pCheckedGrid = false;
            pCheckedCheckBox = true;
        }
        #endregion

        #region gridControl1_Enter
        private void gridControl1_Enter(object sender, EventArgs e)
        {
            pCheckedCheckBox = false;
            pCheckedGrid = true;
        }
        #endregion

        #endregion

        #region fillCTCTestingAndCounselingMonthlySummary
        /**
         * This method populates data for CTCTestingAndCounselingMonthlySummary from database.
         **/
        private bool fillCTCTestingAndCounselingMonthlySummary(ref DataSet mDsData)
        {
            string mExtraParameters = "";
            int fromYear = 0;
            int fromMonth = 0;
            int toMonth = 0;

            if (comboBoxEdit1.SelectedIndex == -1)
            {
                Program.Display_Error("Please select a year");
                comboBoxEdit1.Focus();
                return false;
            }

            if (comboBoxEdit2.SelectedIndex == -1)
            {
                Program.Display_Error("Please select a month from FROM field");
                comboBoxEdit2.Focus();
                return false;
            }
            if (comboBoxEdit3.SelectedIndex == -1)
            {
                Program.Display_Error("Please select a month from TO field");
                comboBoxEdit3.Focus();
                return false;
            }

            fromYear = comboBoxEdit1.SelectedIndex + 1;
            fromMonth = comboBoxEdit2.SelectedIndex + 1;
            toMonth = comboBoxEdit3.SelectedIndex + 1;

            if (toMonth < fromMonth)
            {
                Program.Display_Error("Please enter a month which is greater than or equal to From Month");
                comboBoxEdit4.Focus();
                return false;
            }          


            mDsData = pMdtReporter.CTC_TestingAndCounselingMonthlySummary(
                comboBoxEdit1.SelectedIndex + 1,
                comboBoxEdit2.SelectedIndex + 1,
                comboBoxEdit3.SelectedIndex + 1,
                mExtraParameters);
           
            return true;            
        }
        #endregion

        #region fillCTCExposedChildrenMonthlyReport
        /**
         * This method populates data for CTCTestingAndCounselingMonthlySummary from database.
         **/
        private bool fillCTCExposedChildrenMonthlyReport(ref DataSet mDsData)
        {
            string mExtraParameters = "";
            int fromYear = 0;
            int fromMonth = 0;
            int toMonth = 0;

            if (comboBoxEdit1.SelectedIndex == -1)
            {
                Program.Display_Error("Please select a year");
                comboBoxEdit1.Focus();
                return false;
            }

            if (comboBoxEdit2.SelectedIndex == -1)
            {
                Program.Display_Error("Please select a month from FROM field");
                comboBoxEdit2.Focus();
                return false;
            }
            if (comboBoxEdit3.SelectedIndex == -1)
            {
                Program.Display_Error("Please select a month from TO field");
                comboBoxEdit3.Focus();
                return false;
            }
            
            fromYear = comboBoxEdit1.SelectedIndex + 1;
            fromMonth = comboBoxEdit2.SelectedIndex + 1;
            toMonth = comboBoxEdit3.SelectedIndex + 1;
           
            if (toMonth < fromMonth)
            {
                Program.Display_Error("Please enter a month which is greater than or equal to From Month");
                comboBoxEdit4.Focus();
                return false;
            }          


            mDsData = pMdtReporter.CTC_ExposedChildrenMonthlyReport(
                comboBoxEdit1.SelectedIndex + 1,
                comboBoxEdit2.SelectedIndex + 1,
                comboBoxEdit3.SelectedIndex + 1,
                mExtraParameters);

            return true;
        }
        #endregion

        #region fillCTCDNAPCRLogBook
        /**
        * This method populates data for CTCDNAPCRMonthlyReport from database.
        **/
        private bool fillCTCDNAPCRMonthlyReport(ref DataSet mDsData)
        {
            string mExtraParameters = "";
            int fromYear = 0;
            int fromMonth = 0;
            int toMonth = 0;

            if (comboBoxEdit1.SelectedIndex == -1)
            {
                Program.Display_Error("Please select a year");
                comboBoxEdit1.Focus();
                return false;
            }

            if (comboBoxEdit2.SelectedIndex == -1)
            {
                Program.Display_Error("Please select a month from FROM field");
                comboBoxEdit2.Focus();
                return false;
            }
            if (comboBoxEdit3.SelectedIndex == -1)
            {
                Program.Display_Error("Please select a month from TO field");
                comboBoxEdit3.Focus();
                return false;
            }

            fromYear = comboBoxEdit1.SelectedIndex + 1;
            fromMonth = comboBoxEdit2.SelectedIndex + 1;
            toMonth = comboBoxEdit3.SelectedIndex + 1;

            if (toMonth < fromMonth)
            {
                Program.Display_Error("Please enter a month which is greater than or equal to From Month");
                comboBoxEdit4.Focus();
                return false;
            }


            mDsData = pMdtReporter.CTC_DNAPCRMonthlyReport(
                comboBoxEdit1.SelectedIndex + 1,
                comboBoxEdit2.SelectedIndex + 1,
                comboBoxEdit3.SelectedIndex + 1,
                mExtraParameters);

            return true;
       
        }
        #endregion

      
    }
    
    internal class ExtraPatientDetailsFilter
    {
        internal string ExtraFilter = "";
        internal string ExtraParameters = "";

        internal ExtraPatientDetailsFilter()
        {
            ExtraFilter = "";
            ExtraParameters = "";
        }
    }
   
}