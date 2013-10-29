using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using System.IO;

namespace AfyaPro_NextGen
{
    internal partial class frmLABvisit : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsRegistrations pMdtRegistrations;

        private AfyaPro_MT.clsMedicalStaffs pMdtMedicalStaffs;          //holding all the staff
        private AfyaPro_MT.clsLaboratories pMdtLaboratories;            //holding all the labs
        private AfyaPro_MT.clsLabTestGroups pMdtLabtestgroups;          //holding all the lab test groups
        private AfyaPro_MT.clsLabTests pMdtLabtests;                    //holding all the lab tests
        private AfyaPro_MT.clsPatientLabTests pMdtPatientLabTests;       //holding all the lab tests
        private AfyaPro_MT.clsControlInformation pMdtControlInformation;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();
        private AfyaPro_Types.clsPatient currentpatient;

        internal AfyaPro_Types.clsPatient gCurrentPatient
        {
            get
            {           
                return currentpatient;
            }
            set
            {
                currentpatient = value;
                if (value.Exist)
                {
                    Fill_HistoryData();         //very time this changes
                }
            }
        }

        private DataTable pDtProperties = new DataTable("properties");
        private DataTable pDtMedicalStaffs = new DataTable("medicalstaffs");
        private DataTable pDtTechnicians = new DataTable("lab technicians");
        private DataTable pDtLabs = new DataTable("labs");
        private DataTable pDtLabTestGroups = new DataTable("lab test groups");
        private DataTable pDtLabTests = new DataTable("lab tests");
        private DataTable pDtPatientLabTests = new DataTable("Patient Lab Tests");


        private string pClassName = "frmLABvisit";
        private AfyaPro_Types.clsPatient pSelectedPatient = null;
        private string pCurrPatientId = "";
        private string pPrevPatientId = "";

        private List<Object> pObjectsList = new List<Object>();


        /// <summary>
        /// The Highlighted Row
        /// </summary>
        private DataRow pSelectedRow = null;

        private string pdatastate = "";

        internal string gDataState //= "";
        {
            get
            {
                return pdatastate;
            }
            set
            {
                this.Text = "LAB - " + value;
                pdatastate = value;
            }
        }

        //keep track of changes
        private bool pSearchingPatient = false;
        private bool bfirstnamechanged = false;
        private bool bsurnamechanged = false;
        private bool bgenderchanged = false;

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        #endregion

        #region frmLABvisit
        /// <summary>
        /// Initialize the Form Object
        /// </summary>
        public frmLABvisit()
        {
            string mFunctionName = "frmLABvisit()";
            InitializeComponent();
            
            this.Icon = Program.gMdiForm.Icon;
            try
            {   //try to get all the objects ready that we will use
                this.Icon = Program.gMdiForm.Icon;

                //get all the registered users from the system
                pMdtRegistrations = (AfyaPro_MT.clsRegistrations)Activator.GetObject(
                  typeof(AfyaPro_MT.clsRegistrations),
                  Program.gMiddleTier + "clsRegistrations");

                //retreave a link to all medical staffs we have in the system
                pMdtMedicalStaffs = (AfyaPro_MT.clsMedicalStaffs)Activator.GetObject(
                    typeof(AfyaPro_MT.clsMedicalStaffs), 
                    Program.gMiddleTier + "clsMedicalStaffs");
              
                //retreave a link to all labs that we have in the system
                pMdtLaboratories = (AfyaPro_MT.clsLaboratories)Activator.GetObject(
                    typeof(AfyaPro_MT.clsLaboratories), 
                    Program.gMiddleTier + "clsLaboratories");

                //retreave a link to all lab test groups from the system
                pMdtLabtestgroups = (AfyaPro_MT.clsLabTestGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsLabTestGroups),
                    Program.gMiddleTier + "clsLabTestGroups");

                //retreave a link to all lab tests from the system
                pMdtLabtests = (AfyaPro_MT.clsLabTests)Activator.GetObject(
                    typeof(AfyaPro_MT.clsLabTests),
                    Program.gMiddleTier + "clsLabTests");

                //retreave a link to all the patient lab tests
                pMdtPatientLabTests = (AfyaPro_MT.clsPatientLabTests)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPatientLabTests),
                    Program.gMiddleTier + "clsPatientLabTests");


                pMdtControlInformation = (AfyaPro_MT.clsControlInformation)Activator.GetObject(
                    typeof(AfyaPro_MT.clsControlInformation),
                    Program.gMiddleTier + "clsControlInformation");

                pDtProperties = pMdtControlInformation.View_Properties("", "", "", "");

                //prepare columns for patient history
                DataTable mDtColumns = pMdtPatientLabTests.View(false, new DateTime(), new DateTime(), "1=2", "", "", "");
                foreach (DataColumn mDataColumn in mDtColumns.Columns)
                {
                    pDtPatientLabTests.Columns.Add(mDataColumn.ColumnName, mDataColumn.DataType);
                }
                grdLABPatientHistory.DataSource = pDtPatientLabTests;

                ////set the tab order durring runtime
                //(new clsTabOrderMan(this)).SetTabOrder(clsTabOrderMan.TabScheme.AcrossFirst);

                Data_Clear();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmLABvisit_Load
        private void frmLABvisit_Load(object sender, EventArgs e)
        {
            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            Program.Restore_GridLayout(grdLABPatientHistory, grdLABPatientHistory.Name);

            txbBirthDateFormat.Text = "(" + Program.gCulture.DateTimeFormat.ShortDatePattern + ")";

            this.pFormWidth = this.Width;
            this.pFormHeight = this.Height;

            Program.Center_Screen(this);
        }
        #endregion

        #region frmLABvisit_FormClosing
        private void frmLABvisit_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //layout
                if (layoutControl1.IsModified == true)
                {
                    Program.Save_FormLayout(this, layoutControl1, this.Name);
                }

                //grid
                Program.Save_GridLayout(grdLABPatientHistory, grdLABPatientHistory.Name);
            }
            catch { }
        }
        #endregion

        #region Fill_HistoryData
        public void Fill_HistoryData()
        {
            grpHsty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            pDtPatientLabTests.Rows.Clear();

            #region ready pDtPatientLabTests
            //if (pDtPatientLabTests.Columns.Count == 0)
            //{
                
            //    //pDtPatientLabTests.Columns.Add("Patient Code", typeof(String));
            //    pDtPatientLabTests.Columns.Add("Test Date", typeof(string));
            //    pDtPatientLabTests.Columns.Add("Lab Code", typeof(string));
            //    pDtPatientLabTests.Columns.Add("Lab Description", typeof(string));

            //    pDtPatientLabTests.Columns.Add("Doctor Description", typeof(string));

            //    pDtPatientLabTests.Columns.Add("TestGroup", typeof(string));
            //    pDtPatientLabTests.Columns.Add("TestGroup Description", typeof(string));

            //    pDtPatientLabTests.Columns.Add("Test Code", typeof(string));
            //    pDtPatientLabTests.Columns.Add("Test Description", typeof(string));

               
            //    pDtPatientLabTests.Columns.Add("results", typeof(string));
            //    pDtPatientLabTests.Columns.Add("First Name", typeof(string));
            //    pDtPatientLabTests.Columns.Add("Sur Name", typeof(string));

               
            //}
            #endregion 


            DataTable mDtPatientLabTests = pMdtPatientLabTests.View(false,new DateTime(),new DateTime(),
                "patientcode= '" + gCurrentPatient.code + "'", "transdate desc, autocode desc", Program.gLanguageName, "grdLABPatientHistory");

            foreach (DataRow mDataRow in mDtPatientLabTests.Rows)
            {
                //put all the info about that row into the c
                DataRow mNewRow = pDtPatientLabTests.NewRow();
                foreach (DataColumn mDataColumn in mDtPatientLabTests.Columns)
                {
                    mNewRow[mDataColumn.ColumnName] = mDataRow[mDataColumn.ColumnName];
                }

                pDtPatientLabTests.Rows.Add(mNewRow);
                pDtPatientLabTests.AcceptChanges();
            }
        }
        #endregion

        #region HistoryData_Clear

        public void Clear_HistoryData()
        {
            pDtPatientLabTests.Rows.Clear();

            txtPatientId.Focus();
        }

        #endregion

        #region Data_Lookup
        internal void Fill_LookupData(DevExpress.XtraEditors.LookUpEdit mLookupEdit)
        {
            string mFunctionName = "Fill_LookupData";
            DataRow mNewRow;
            try
            {
                switch (mLookupEdit.Name)
                {
                    case "cboClinicalOfficer":
                        {

                            #region retreave doctors

                            pDtMedicalStaffs.Rows.Clear();


                            if (pDtMedicalStaffs.Columns.Count == 0)
                            {
                                pDtMedicalStaffs.Columns.Add("code", typeof(String));
                                pDtMedicalStaffs.Columns.Add("description", typeof(String));
                            }
                            //add columns to medicalstaff datatable
                            cboClinicalOfficer.Properties.DataSource = pDtMedicalStaffs;
                            cboClinicalOfficer.Properties.DisplayMember = "description";
                            cboClinicalOfficer.Properties.ValueMember = "code";

                            DataTable mDtMedicalStaffs = pMdtMedicalStaffs.View(
                                "category=" + Convert.ToInt16(AfyaPro_Types.clsEnums.StaffCategories.MedicalDoctors),
                                "code", Program.gLanguageName, "grdGENMedicalStaffs");
                            foreach (DataRow mDataRow in mDtMedicalStaffs.Rows)
                            {
                                if ((string)mDataRow[4] == "CLINICAL OFFICERS")                     //the Fith object holds the discription   
                                {
                                    mNewRow = pDtMedicalStaffs.NewRow();
                                    mNewRow["code"] = mDataRow["code"].ToString();
                                    mNewRow["description"] = mDataRow["description"].ToString();
                                    pDtMedicalStaffs.Rows.Add(mNewRow);
                                    pDtMedicalStaffs.AcceptChanges();
                                }
                            }

                            if (pDtMedicalStaffs.DefaultView.Count == 0)
                            {
                                //user needs to add a technician into the system

                                cboClinicalOfficer.Properties.NullText = "[EMPTY]";
                                cboClinicalOfficer.Enabled = false;
                            }
                            else
                            {
                                foreach (DataColumn mDataColumn in pDtMedicalStaffs.Columns)
                                {
                                    mDataColumn.Caption = mDtMedicalStaffs.Columns[mDataColumn.ColumnName].Caption;
                                }
                            }

                            #endregion
                        }
                        break;
                    case "cboTechnicianName":
                        {

                            #region retreave LAB TECHNICIANS
                            pDtTechnicians.Rows.Clear();

                            //check if the colums dont' already exists
                            if (pDtTechnicians.Columns.Count == 0)
                            {
                                //add columns to medicalstaff datatable
                                pDtTechnicians.Columns.Add("code", typeof(String));
                                pDtTechnicians.Columns.Add("description", typeof(String));
                            }
                            cboTechnicianName.Properties.DataSource = pDtTechnicians;
                            cboTechnicianName.Properties.DisplayMember = "description";
                            cboTechnicianName.Properties.ValueMember = "code";

                            DataTable mDtLabTechnicians = pMdtMedicalStaffs.View(
                                "category=" + Convert.ToInt16(AfyaPro_Types.clsEnums.StaffCategories.MedicalDoctors),
                                "code", Program.gLanguageName, "grdGENMedicalStaffs");
                            foreach (DataRow mDataRow in mDtLabTechnicians.Rows)
                            {
                                if ((string)mDataRow[4] == "LAB TECHNICIANS")                     //the Fith object holds the discription   
                                {
                                    mNewRow = pDtTechnicians.NewRow();
                                    mNewRow["code"] = mDataRow["code"].ToString();
                                    mNewRow["description"] = mDataRow["description"].ToString();
                                    pDtTechnicians.Rows.Add(mNewRow);
                                    pDtTechnicians.AcceptChanges();
                                }
                            }

                            if (pDtTechnicians.DefaultView.Count == 0)
                            {
                                //user needs to add a technician into the system

                                cboTechnicianName.Properties.NullText = "[Not Registered]";
                                cboTechnicianName.Enabled = false;
                            }
                            else
                            {
                                foreach (DataColumn mDataColumn in pDtTechnicians.Columns)
                                {
                                    mDataColumn.Caption = mDtLabTechnicians.Columns[mDataColumn.ColumnName].Caption;
                                }
                            }

                            #endregion

                        }
                        break;
                    case "cboLabName":
                        {

                            #region retreave LABS

                            pDtLabs.Rows.Clear();

                            //add columns to medicalstaff datatable
                            if (pDtLabs.Columns.Count == 0)
                            {
                                pDtLabs.Columns.Add("code", typeof(String));
                                pDtLabs.Columns.Add("description", typeof(String));
                            }
                            cboLabName.Properties.DataSource = pDtLabs;
                            cboLabName.Properties.DisplayMember = "description";
                            cboLabName.Properties.ValueMember = "code";

                            // DataTable mDtLabs = pMdtLaboratories.View("", "", Program.gLanguageName, "grdLABs");
                            DataTable mDtLabs = pMdtLaboratories.View("", "", "", "");



                            foreach (DataRow mDataRow in mDtLabs.Rows)
                            {

                                mNewRow = pDtLabs.NewRow();
                                mNewRow["code"] = mDataRow["code"].ToString();
                                mNewRow["description"] = mDataRow["description"].ToString();
                                pDtLabs.Rows.Add(mNewRow);
                                pDtLabs.AcceptChanges();

                            }

                            if (pDtLabs.DefaultView.Count == 0)
                            {
                                //user needs to add a lab into the system
                                cboLabName.Enabled = false;
                            }
                            else
                            {
                                foreach (DataColumn mDataColumn in pDtLabs.Columns)
                                {
                                    mDataColumn.Caption = mDtLabs.Columns[mDataColumn.ColumnName].Caption;
                                }
                            }


                            #endregion

                        }
                        break;
                    case "cboLabTestGroup":

                        #region retreave LAB TEST GROUPS
                        pDtLabTestGroups.Rows.Clear();

                        //check if the colums dont' already exists
                        if (pDtLabTestGroups.Columns.Count == 0)
                        {
                            //add columns to medicalstaff datatable
                            pDtLabTestGroups.Columns.Add("code", typeof(String));
                            pDtLabTestGroups.Columns.Add("description", typeof(String));
                        }
                        cboLabTestGroup.Properties.DataSource = pDtLabTestGroups;
                        cboLabTestGroup.Properties.DisplayMember = "description";
                        cboLabTestGroup.Properties.ValueMember = "code";

                        DataTable mDtLabTestGroups = pMdtLabtestgroups.View("", "", Program.gLanguageName, "grdLABtestgroups");

                        foreach (DataRow mDataRow in mDtLabTestGroups.Rows)
                        {

                            mNewRow = pDtLabTestGroups.NewRow();
                            mNewRow["code"] = mDataRow["code"].ToString();
                            mNewRow["description"] = mDataRow["description"].ToString();
                            pDtLabTestGroups.Rows.Add(mNewRow);
                            pDtLabTestGroups.AcceptChanges();
                        }

                        if (pDtLabTestGroups.DefaultView.Count == 0)
                        {
                            //user needs to add a lab into the system
                            cboLabTestGroup.Properties.NullText = "[Not Registered]";
                            cboLabTestGroup.Enabled = false;
                        }
                        else
                        {
                            foreach (DataColumn mDataColumn in pDtLabTestGroups.Columns)
                            {
                                mDataColumn.Caption = mDtLabTestGroups.Columns[mDataColumn.ColumnName].Caption;
                            }
                        }

                        #endregion

                        break;
                    case "cboLabTest":
                        {

                            #region retreave LAB TESTS


                            pDtLabTests.Rows.Clear();

                            //add columns to pDtLabTests datatable
                            if (pDtLabTests.Columns.Count == 0)
                            {
                                pDtLabTests.Columns.Add("code", typeof(String));
                                pDtLabTests.Columns.Add("description", typeof(String));
                                pDtLabTests.Columns.Add("groupcode", typeof(String));
                                pDtLabTests.Columns.Add("resulttype", typeof(String));
                                pDtLabTests.Columns.Add("units", typeof(String));
                            }


                            cboLabTest.Properties.DataSource = pDtLabTests;
                            cboLabTest.Properties.DisplayMember = "description";
                            cboLabTest.Properties.ValueMember = "code";

                            DataTable mDtLabTests = pMdtLabtests.View("", "", Program.gLanguageName, "grdLABtests");



                            foreach (DataRow mDataRow in mDtLabTests.Rows)
                            {

                                mNewRow = pDtLabTests.NewRow();


                                mNewRow["code"] = mDataRow["code"].ToString();
                                mNewRow["description"] = mDataRow["description"].ToString();
                                mNewRow["groupcode"] = mDataRow["groupcode"].ToString();
                                mNewRow["resulttype"] = mDataRow["resulttype"].ToString();
                                mNewRow["units"] = mDataRow["units"].ToString();

                                if (mNewRow["groupcode"].ToString() == get_LabTestGroup()[0].ToString())
                                {
                                    pDtLabTests.Rows.Add(mNewRow);
                                }

                                pDtLabTests.AcceptChanges();

                            }

                            if (pDtLabTests.DefaultView.Count == 0)
                            {
                                //user needs to add a few tests into the system
                                cboLabTest.Properties.NullText = "[Not Registered]";
                                cboLabTest.Enabled = false;
                            }
                            else
                            {
                                foreach (DataColumn mDataColumn in pDtLabTests.Columns)
                                {
                                    mDataColumn.Caption = mDtLabTests.Columns[mDataColumn.ColumnName].Caption;
                                }
                            }


                            #endregion
                        }
                        break;

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
            txtBirthDate.Text = "";
            txtYears.Text = "";
            txtMonths.Text = "";
            txtChronic.Text = "";
            txtPatientId.Text = "";

            txeResultNumber.Text = "";

            txeResultsFree.Text = "";

            pPrevPatientId = "";
            pCurrPatientId = pPrevPatientId;

            //memotext
            memoEdit1.Text = string.Empty;
            cmdSave.Enabled = false;
            radGender.SelectedIndex = -1;
            radResults.SelectedIndex = -1;


            //set the change properties back to default
            pSearchingPatient = false;
            bfirstnamechanged = false;
            bsurnamechanged = false;
            bgenderchanged = false;


           
            //labinfo
            cboClinicalOfficer.Properties.NullText = "";
            cboClinicalOfficer.EditValue = null;

            cboLabName.Properties.NullText = "";
            cboLabName.EditValue = null;

            cboTechnicianName.Properties.NullText = "";
            cboTechnicianName.EditValue = null;

            cboLabTestGroup.Properties.NullText = "";
            cboLabTestGroup.EditValue = null;

            cboLabTest.Properties.NullText = "";
            cboLabTest.EditValue = null;

            cboClinicalOfficer.Enabled = false;
            cboLabName.Enabled = false;
            cboTechnicianName.Enabled = false;
            
            cboLabTestGroup.Enabled = false;
            
            cboLabTest.Enabled = false;

            this.cmdSave.Enabled = false;

            grpHsty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            //clear the history
            Clear_HistoryData();
            Init_Results();             //try to re_initiate things
        

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

        #region Data_Display
        internal void Data_Display(AfyaPro_Types.clsPatient mPatient)
        {
            String mFunctionName = "Data_Display";

            try
            {

            //    this.Data_Clear();

                if (mPatient != null)
                {
                    if (mPatient.Exist == true)
                    {
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
                            radGender.SelectedIndex = 0;
                        }
                        else
                        {
                            radGender.SelectedIndex = 1;
                        }
                        txtBirthDate.Text = mPatient.birthdate.ToString("d");
                        int mDays = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue).Subtract(mPatient.birthdate).Days;
                        int mYears = (int)mDays / 365;
                        int mMonths = (int)(mDays % 365) / 30;

                        txtYears.Text = mYears.ToString();
                        txtMonths.Text = mMonths.ToString();

                        pCurrPatientId = mPatient.code;
                        pPrevPatientId = pCurrPatientId;

                        this.Fill_HistoryData();
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

        #region txtPatientId Events
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
        #region txtPatientId_EditValueChanged
        private void txtPatientId_EditValueChanged(object sender, EventArgs e)
        {
            if (pSearchingPatient == true)
            {
                this.pSelectedPatient = this.Search_Patient();
                this.Data_Display(pSelectedPatient);
               
            }
            if (bfirstnamechanged && bsurnamechanged)
            {
                //set the labinfo ready for edit
                cboClinicalOfficer.Enabled = true;
            }
        }
        #endregion

        
        #endregion

        #region Methods for retreaving LookupEdit
        /// <summary>
        /// Returns whats inside cboLabName LookupEdit
        /// </summary>
        /// <returns></returns>
        public Object[] get_LabName()
        {
            Object[] ret = new string[] { "", "" };

            foreach (DataRow row in this.pDtLabs.Rows)
            {

                if (row.ItemArray[0] == this.cboLabName.EditValue)			//the labtest IDnumber
                {
                    ret = row.ItemArray;								// 3 where the result code is kept
                    break;
                }

            }

            return ret;

        }
        /// <summary>
        /// Returns whats inside cboClinicalOfficer LookupEdit
        /// </summary>
        /// <returns></returns>
        public Object[] get_Officer()
        {
            Object[] ret = new string[] { "", "" };

            foreach (DataRow row in this.pDtMedicalStaffs.Rows)
            {

                if (row.ItemArray[0] == this.cboClinicalOfficer.EditValue)			//the labtest IDnumber
                {
                    ret = row.ItemArray;								// 3 where the result code is kept
                    break;
                }

            }

            return ret;
        }
        /// <summary>
        /// Returns whats inside cboTechnicianName LookupEdit
        /// </summary>
        /// <returns></returns>
        public Object[] get_Technician()
        {
            Object[] ret = new string[] {"",""};

            foreach (DataRow row in this.pDtTechnicians.Rows)
            {

                if (row.ItemArray[0] == this.cboTechnicianName.EditValue)			//the labtest IDnumber
                {
                    ret = row.ItemArray;								// 3 where the result code is kept
                    break;                                              //get out of the loop
                }

            }

            return ret;

        }
        /// <summary>
        /// Returns whats inside cboLabTest LookupEdit
        /// </summary>
        /// <returns></returns>
        public Object[] get_LabTest()
        {
            Object[] ret = new string[] { "", "" };

            foreach (DataRow row in pDtLabTests.Rows)
            {

                if (row.ItemArray[0] == cboLabTest.EditValue)			//the labtest IDnumber
                {
                    ret = row.ItemArray;								// 3 where the result code is kept
                    break;                                              //get out of the loop
                }

            }

            return ret;

        }

        /// <summary>
        /// Returns whats inside cboLabTestGroup LookupEdit
        /// </summary>
        /// <returns></returns>
        public Object[] get_LabTestGroup()
        {
            Object[] ret = new string[] { "", "" };


            foreach (DataRow row in this.pDtLabTestGroups.Rows)
            {

                if (row.ItemArray[0] == cboLabTestGroup.EditValue)			//the labtest IDnumber
                {
                    ret = row.ItemArray;								// 3 where the result code is kept
                    break;                                              //get out of the loop
                }

            }

            return ret;

        }
        #endregion

        #region Methods for handling Results field
        /// <summary>
        /// Returns the results that were filled by the user
        /// pertaining to the labtest that was selected
        /// </summary>
        /// <returns></returns>
        private string Ret_Results()
        {
            string ret = string.Empty;
            string resultcode = string.Empty;
            foreach (DataRow row in pDtLabTests.Rows)
            {

                if (row.ItemArray[0] == cboLabTest.EditValue)			//the labtest IDnumber
                {
                    resultcode = (string)row.ItemArray[3];								// 3 where the result code is kept
                }

            }


            switch (resultcode)
            {
                #region as Pos/Neg
                //return positive or negative
                case "1":
                    if (radResults.SelectedIndex == 0)
                        ret = "positive";
                    else
                        ret = "negative";
                    break;
                #endregion

                #region as String
                //returns a text string
                case "2":
                    ret = txeResultsFree.Text;
                    break;
                #endregion

                #region as Number
                //returns a number
                case "3":
                    ret = txeResultNumber.Text;
                    break;
                #endregion


            }
            return ret;         //return whats there
        }
        /// <summary>
        /// Shows and hides the result portion of the lab visit form
        /// depending on type of labtest was chosen
        /// </summary>
        private void Init_Results()
        {
            //take the result code.

            string resultcode = string.Empty;
            foreach (DataRow row in pDtLabTests.Rows)
            {
                if (row.ItemArray[0] == cboLabTest.EditValue)			//the labtest IDnumber
                {
                    resultcode = (string)row.ItemArray[3];								// 3 where the result code is kept
                    break;
                }
            }
			

            switch (resultcode)
            {
                #region as Pos/Neg
                //return positive or negative 
                case "1":
                    veResults.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    //hide everything else
                    reNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    reText.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    break;
                #endregion

                #region as String
                //returns a text string
                case "2":
                    this.reText.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    //hide everything else
                    veResults.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    reNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    break;
                #endregion

                #region as Number
                //returns a number
                case "3":
                    this.reNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    //hide everything else
                    veResults.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    reText.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    
                    break;
                #endregion

                #region as Nothing
                case "":
                    //hide everything else
                    veResults.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    reText.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    reNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    break;

                #endregion
            }

        }

        /// <summary>
        /// Event: Click for cmdStart button: Process all the fields and Submit
        ///                                   the new lab visit into the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            //register all the information and send it.
            Int16 mGenerateCode = 1;                //make it autogen

            //validate input
            if ((this.txtName.Text == "") || (this.cboClinicalOfficer.Text == cboClinicalOfficer.Properties.NullText) || (this.cboLabTest.Enabled == false))
            {
                //this button should have been fulsilized
                this.cmdSave.Enabled = false;
                
            }
            else
            {

                string labcode = get_LabName()[0].ToString();
                string labdesc = get_LabName()[1].ToString();
                string results = Ret_Results();             //return the results

                try
                {
                    DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);
                    
                    this.pMdtPatientLabTests.Add(mGenerateCode, mTransDate, txtPatientId.Text, get_LabName()[0].ToString(), 
                        get_LabName()[1].ToString(), get_Officer()[0].ToString(), get_Officer()[1].ToString(), 
                        get_Technician()[0].ToString(), get_Technician()[1].ToString(), get_LabTestGroup()[0].ToString(),
                        get_LabTestGroup()[1].ToString(), get_LabTest()[0].ToString(), get_LabTest()[1].ToString(),
                        results, memoEdit1.Text, Program.gCurrentUser.Code);


                }
                catch (Exception ex)
                {
                    if (ex.Message == "String was not recognized as a valid DateTime.")
                    {
                        //user didn't enter the date correctly
                        MessageBox.Show("Must Enter Birth Date Correctly!");
                        return;
                    }

                }
               
                MessageBox.Show("Added Successfully");
            }
            this.Data_Clear();
        }
        #endregion

        #region Events
        /// <summary>
        /// Event: MouseDoubleClick for radGender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void radGender_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //throw new System.NotImplementedException();
         
            bgenderclicked = true;
        }
        /// <summary>
        /// Event: make sure that the radGender change was not cause by a mouseclick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void radGender_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            //make sure the changed wasn't done by users mouse or keyboard
            if (bgenderclicked)
            {
                bgenderclicked = false;
                e.Handled = true;
                radGender.SelectedIndex = radGender.SelectedIndex;
             //   radGender.SelectedIndex = -1;

            }

        }
        /// <summary>
        /// Event: MouseClick for radGender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void radGender_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //throw new System.NotImplementedException();
            bgenderclicked = true;
        }

        /// <summary>
        /// Event: TextChanged for txtSurname
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtSurname_TextChanged(object sender, EventArgs e)
        {
            //throw new System.NotImplementedException();
            bsurnamechanged = true;
            if (bfirstnamechanged && bgenderchanged)
                cboClinicalOfficer.Enabled = true;
        }

        /// <summary>
        /// Event: TextChanged for txtFirstName
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtFirstName_TextChanged(object sender, EventArgs e)
        {

            bfirstnamechanged = true;
            if (bsurnamechanged && bgenderchanged)
                cboClinicalOfficer.Enabled = true;
        }


        /// <summary>
        /// Event: cboLabTestGroup EnabledChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cboLabTestGroup_EnabledChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.LookUpEdit s = (DevExpress.XtraEditors.LookUpEdit)sender;
            if (s.Enabled == true)          //if its being enabled
                Fill_LookupData(cboLabTestGroup);
            else
                cboLabTestGroup.EditValue = null;
        }

        /// <summary>
        /// Event: cboLabTest EnabledChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cboLabTest_EnabledChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.LookUpEdit s = (DevExpress.XtraEditors.LookUpEdit)sender;
            if (s.Enabled == true)          //if its being enabled
                Fill_LookupData(cboLabTest);
            else
                cboLabTest.EditValue = null;
        }

        /// <summary>
        /// Event: EnabledChanged for cboLabName
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cboLabName_EnabledChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.LookUpEdit s = (DevExpress.XtraEditors.LookUpEdit)sender;
            if (s.Enabled == true)          //if its being enabled
                Fill_LookupData(cboLabName);
            else
                cboLabName.EditValue = null;
        }
        /// <summary>
        /// Event: EnabledChanged for cboTechnicianName
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cboTechnicianName_EnabledChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.LookUpEdit s = (DevExpress.XtraEditors.LookUpEdit)sender;
            if (s.Enabled == true)          //if its being enabled
                Fill_LookupData(cboTechnicianName);
            else
                cboTechnicianName.EditValue = null;
        }

        /// <summary>
        /// Event: EnabledChanged cboClinicalOfficer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cboClinicalOfficer_EnabledChanged(object sender, EventArgs e)
        {
            //
            DevExpress.XtraEditors.LookUpEdit s = (DevExpress.XtraEditors.LookUpEdit)sender;
            if (s.Enabled == true)          //if its being enabled
                Fill_LookupData(cboClinicalOfficer);
            else
                cboClinicalOfficer.EditValue = null;
        }
        /// <summary>
        /// Event: EditValueChanged for cboLabTest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cboLabTest_EditValueChanged(object sender, EventArgs e)
        {

            DevExpress.XtraEditors.LookUpEdit d = (DevExpress.XtraEditors.LookUpEdit)sender;
            if (d.EditValue == null)
            {
                //disable the control
                cboLabTest.Enabled = false;
            }
            else
            {
                //now we enable the results portion
                Init_Results();

               
            }

        }
        /// <summary>
        /// Event: EditValueChanged for cboLabTestGroup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cboLabTestGroup_EditValueChanged(object sender, EventArgs e)
        {
            //release the cboLabTests field       
            if (cboLabTest.Enabled == false)
                cboLabTest.Enabled = true;
            else
                Fill_LookupData(cboLabTest);            //refill the data in labtest

        }
        /// <summary>
        /// Event: EditValueChanged for cboLabName
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cboLabName_EditValueChanged(object sender, EventArgs e)
        {
            //release the cboLabTestGroup field      
            cboLabTestGroup.Enabled = true;
        }
        /// <summary>
        /// Event: EditValueChanged for cboTechnicianName
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cboTechnicianName_EditValueChanged(object sender, EventArgs e)
        {
            //release cboLabName field
            cboLabName.Enabled = true;

        }
        /// <summary>
        /// Event: EditValueChanged cboClinicalOfficer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cboClinicalOfficer_EditValueChanged(object sender, EventArgs e)
        {
            //release the cboLabName field
            cboTechnicianName.Enabled = true;
            cboLabName.Enabled = true;

        }

        /// <summary>
        /// Event: KeyPres for txeResultNumber (makes sure only numbers are entered)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txeResultNumber_KeyPress(object sender, KeyPressEventArgs e)
        {

            //only numbers allowed
            int isNumber = 0;

            e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
        }
        /// <summary>
        /// Event: SelectedIndexChanged for radGender (alerts that the gender status was changed)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            //release the lab portion
            bgenderchanged = true;

            //don't process the editable
            radGender.Enabled = false;              //disable it
            cboClinicalOfficer.Enabled = true;
        }
        /// <summary>
        /// Event: Clears all the Data in this Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdClear_Click(object sender, EventArgs e)
        {
            //clears the date
            Data_Clear();
        }
        /// <summary>
        /// Event: KeyPress for txtSurName (makes sure no keyboard edits this field)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void txtSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            //no typing allowed
            e.Handled = true;
        }

        /// <summary>
        /// Event: KeyPress for txtFirstName (makes sure no keyboard edits this field)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFirstName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //no typing allowed
            e.Handled = true;

        }
        /// <summary>
        /// Event: KeyPress for txtOtherNames (makes sure no keyboard edits this field)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOtherNames_KeyPress(object sender, KeyPressEventArgs e)
        {
            //no typing allowed
            e.Handled = true;
        }

        /// <summary>
        /// Event: KeyPress for txtBirthDate (makes sure no keyboard edits this field)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBirthDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            //no typing allowed
            e.Handled = true;
        }
        /// <summary>
        /// Event: KeyPress for txtPatientId (makes sure no keyboard edits this field)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPatientId_KeyPress(object sender, KeyPressEventArgs e)
        {
            ////no typing allowed
            //e.Handled = true;
        }
        /// <summary>
        /// Event: Click for cmdSearch (starts the searchPatient form)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            pSearchingPatient = true;

            frmSearchPatient mSearchPatient = new frmSearchPatient(txtPatientId);
            mSearchPatient.ShowDialog();

            pSearchingPatient = false;
        }

        #endregion

        #region txeResultNumber_EditValueChanged
        private void txeResultNumber_EditValueChanged(object sender, EventArgs e)
        {
            this.cmdSave.Enabled = true;
        }
        #endregion

        #region txeResultsFree_EditValueChanged
        private void txeResultsFree_EditValueChanged(object sender, EventArgs e)
        {
            this.cmdSave.Enabled = true;
        }
        #endregion

        #region radResults_SelectedIndexChanged
        private void radResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmdSave.Enabled = true;
        }
        #endregion
    }
}
