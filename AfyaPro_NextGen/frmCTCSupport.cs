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

namespace AfyaPro_NextGen
{
    public partial class frmCTCSupport : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsReporter pMdtReporter;
        private AfyaPro_MT.clsCTCClients pMdtCTCClients;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private DataTable pDtHistory = new DataTable("history");

        private Type pType;
        private string pClassName = "";
        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private int mAutoCode = 0;

        private AfyaPro_Types.clsCtcClient pSelectedPatient = null;
        internal AfyaPro_Types.clsCtcClient SelectedPatient
        {
            set { pSelectedPatient = value; }
            get { return pSelectedPatient; }
        }

        private string mCounsellorCode = "";
        internal string SelectedCounsellor
        {
            set { mCounsellorCode = value; }
            get { return mCounsellorCode; }
        }       

        private bool pRecordSaved = false;
        internal bool RecordSaved
        {
            set { pRecordSaved = value; }
            get { return pRecordSaved; }
        }

        bool mEdit = false;
        #endregion

        #region frmCTCSupport
        public frmCTCSupport()
        {
            InitializeComponent();
           
            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCSupport";
            this.KeyDown += new KeyEventHandler(frmCTCSupport_KeyDown);
            try
            {
                this.Icon = Program.gMdiForm.Icon;

                pMdtReporter = (AfyaPro_MT.clsReporter)Activator.GetObject(
                   typeof(AfyaPro_MT.clsReporter),
                   Program.gMiddleTier + "clsReporter");

                pMdtAutoCodes = (AfyaPro_MT.clsAutoCodes)Activator.GetObject(
                    typeof(AfyaPro_MT.clsAutoCodes),
                    Program.gMiddleTier + "clsAutoCodes");

                pMdtCTCClients = (AfyaPro_MT.clsCTCClients)Activator.GetObject(
                    typeof(AfyaPro_MT.clsCTCClients),
                    Program.gMiddleTier + "clsCTCClients");

                pDtHistory.Columns.Add("autocode", typeof(System.Int32));
                pDtHistory.Columns.Add("booking", typeof(System.String));
                pDtHistory.Columns.Add("transdate", typeof(System.DateTime));
                pDtHistory.Columns.Add("counsellorcode", typeof(System.String));
                pDtHistory.Columns.Add("counsellordescription", typeof(System.String));
                pDtHistory.Columns.Add("problem", typeof(System.String));
                pDtHistory.Columns.Add("reply", typeof(System.String));            

                viewHistory.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;

                grdHistory.ForceInitialize();

                Fill_LookupData();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }

            layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                   AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctchivtests_customizelayout.ToString());
        }
        #endregion

        #region frmCTCSupport_Load
        private void frmCTCSupport_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCSupport_Load";
            try
            {
                Program.Restore_FormLayout(layoutControl1, this.Name);
                Program.Restore_FormSize(this);

                this.pFormWidth = this.Width;
                this.pFormHeight = this.Height;

                Program.Center_Screen(this);

                DevExpress.XtraGrid.Views.Grid.GridView mHistoryGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
                mHistoryGridView.OptionsBehavior.Editable = false;
                mHistoryGridView.OptionsView.ShowGroupPanel = false;
                mHistoryGridView.OptionsView.ShowFooter = false;
                mHistoryGridView.OptionsView.ShowIndicator = false;
                grdHistory.MainView = mHistoryGridView;
                grdHistory.DataSource = pDtHistory;
                grdHistory.ForceInitialize();

                Program.Restore_GridLayout(grdHistory, grdHistory.Name);

                this.Load_Controls();

                this.Append_ShortcutKeys();
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

            mObjectsList.Add(txbPatientId);
            mObjectsList.Add(cmdSave);
            mObjectsList.Add(cmdClose);

            DevExpress.XtraGrid.Views.Grid.GridView mHistoryGridView = (DevExpress.XtraGrid.Views.Grid.GridView)grdHistory.MainView;
            foreach (DevExpress.XtraGrid.Columns.GridColumn mGridColumn in mHistoryGridView.Columns)
            {
                mObjectsList.Add(mGridColumn);
            }

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region counsellors

                DataTable mDtCounsellors = pMdtReporter.View_LookupData("facilitystaffs", "code,description,treatmentpointcode",
                    "category=" + Convert.ToInt16(AfyaPro_Types.clsEnums.StaffCategories.Counsellors),
                    "description", Program.gLanguageName, "grdGENMedicalStaffs");

                cboCounsellor.Properties.DataSource = mDtCounsellors;
                cboCounsellor.Properties.DisplayMember = "description";
                cboCounsellor.Properties.ValueMember = "code";

                #endregion

                dtDate.EditValue = DateTime.Now.Date;

            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion        

        #region frmCTCSupport_Shown
        private void frmCTCSupport_Shown(object sender, EventArgs e)
        {
            this.Data_Clear();

            if (pSelectedPatient != null)
            {
                this.Display_Patient(pSelectedPatient);
            }
        }
        #endregion

        #region Data_Clear
        private void Data_Clear()
        {
            txtProblem.Text = "";
            txtReply.Text = "";
            mAutoCode = 0;
            cboCounsellor.EditValue = mCounsellorCode;
            dtDate.EditValue = DateTime.Now.Date;
            mEdit = false;
        }
        #endregion

        #region Data_ClearAll
        private void Data_ClearAll()
        {
            txtPatientId.Text = "";
            txtHIVNo.Text = "";
            txtArvNo.Text = "";
            txtCTCNo.Text = "";
            txtName.Text = "";
            txtYears.Text = "";
            txtMonths.Text = "";
            txtGender.Text = "";
            dtDate.EditValue = DateTime.Now.Date;
            txtMaritalStatus.Text = "";
            this.Data_Clear();
        }
        #endregion

        #region Display_Patient
        private void Display_Patient(AfyaPro_Types.clsCtcClient mPatient)
        {
            string mFunctionName = "Display_Patient";

            try
            {
                if (mPatient.Exist == false)
                {
                    return;
                }

                #region patient info

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

                txtYears.Text = "";
                txtMonths.Text = "";
                if (Program.IsNullDate(mPatient.birthdate) == false)
                {
                    int mDays = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue).Subtract(mPatient.birthdate).Days;
                    int mYears = (int)mDays / 365;
                    int mMonths = (int)(mDays % 365) / 30;

                    txtYears.Text = mYears.ToString();
                    txtMonths.Text = mMonths.ToString();
                }

                DataTable mDtMaritalstatus = pMdtReporter.View_LookupData("maritalstatus", "code,description",
                  "code = '" + mPatient.maritalstatuscode + "'", "description", Program.gLanguageName, "");

                if (mDtMaritalstatus.Rows.Count > 0)
                {
                    DataRow mDataRow =  mDtMaritalstatus.Rows[0];
                    txtMaritalStatus.Text = mDataRow["description"].ToString().Trim();
                }
                #endregion

                #region ctc_clients

                //pBooking = pMdtCTCClients.Get_Booking(mPatient.code);

                DataTable mDtClients = pMdtCTCClients.View_CTCClients("patientcode='" + mPatient.code + "'", "");

                if (mDtClients.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtClients.Rows[0];

                    txtHIVNo.Text = mDataRow["hivno"].ToString();
                    txtArvNo.Text = mDataRow["arvno"].ToString();
                    txtCTCNo.Text = mDataRow["ctcno"].ToString();
                }

                #endregion

                #region HIVno

                if (txtHIVNo.Text.Trim() == "")
                {
                    Int16 mGenerateCode = pMdtAutoCodes.Auto_Generate_Code(
                        Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.hivnumbers));
                    if (mGenerateCode == -1)
                    {
                        Program.Display_Server_Error("");
                        return;
                    }

                    if (mGenerateCode == 1)
                    {
                        txtHIVNo.Text = "<<---New--->>";
                    }
                }

                #endregion
                
                cboCounsellor.EditValue = mCounsellorCode;

                #region History Grid
                DataTable mDtHistory = pMdtReporter.View_Data("view_ctcsupportivecounselling",
                              "patientcode='" + mPatient.code.Trim() + "'", "transdate desc", Program.gLanguageName, "grdHistory");
                if (mDtHistory.Rows.Count > 0)
                {
                    this.Fill_History(mDtHistory);
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

        #region Fill_History
        private void Fill_History(DataTable mDtHistory)
        {
            string mFunctioName = "Fill_History";

            try
            {
                pDtHistory.Rows.Clear();

                foreach (DataRow mDataRow in mDtHistory.Rows)
                {
                    DataRow mNewRow = pDtHistory.NewRow();
                    mNewRow["autocode"] = mDataRow["autocode"];
                    mNewRow["booking"] = mDataRow["booking"];
                    mNewRow["transdate"] = mDataRow["transdate"];
                    mNewRow["problem"] = mDataRow["problem"];
                    mNewRow["reply"] = mDataRow["reply"];
                    mNewRow["counsellorcode"] = mDataRow["counsellorcode"];
                    mNewRow["counsellordescription"] = mDataRow["counsellordescription"];                    
                    
                    pDtHistory.Rows.Add(mNewRow);
                    pDtHistory.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtHistory.Columns)
                {
                    mDataColumn.Caption = mDtHistory.Columns[mDataColumn.ColumnName].Caption;
                }

                DevExpress.XtraGrid.Views.Grid.GridView mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)grdHistory.Views[0];
                mGridView.ExpandAllGroups();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctioName, ex.Message);
                return;
            }

        }
        #endregion

        #region frmCTCSupport_FormClosing
        private void frmCTCSupport_FormClosing(object sender, FormClosingEventArgs e)
        {
            //layout
            if (layoutControl1.IsModified == true)
            {
                Program.Save_FormLayout(this, layoutControl1, this.Name);
            }

            Program.Save_GridLayout(grdHistory, grdHistory.Name);
        }
        #endregion


        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Append_ShortcutKeys
        private void Append_ShortcutKeys()
        {
            //cmdOk.Text = cmdOk.Text + " (" + Program.KeyCode_Ok.ToString() + ")";
        }
        #endregion

        #region frmCTCSupport_KeyDown
        void frmCTCSupport_KeyDown(object sender, KeyEventArgs e)
        {
            //switch (e.KeyCode)
            //{
            //    case Program.KeyCode_Ok:
            //        {
            //            this.Ok();
            //        }
            //        break;
            //}
        }
        #endregion

        #region cmdSave_Click
        private void cmdSave_Click(object sender, EventArgs e)
        {
           
            if (mEdit)
            {
                Data_Edit();
            }
            else
            {
                Data_New();
            }
                        
            
        }
        #endregion

        #region Data_New
        private void Data_New()
        {
            string mFunctionName = "Data_New";

            if (Program.IsDate(dtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                dtDate.Focus();
                return;
            }
            try
            {
                DateTime? mTransDate = null;

                string mCounsellorCode = cboCounsellor.GetColumnValue("code").ToString();

                mTransDate = Convert.ToDateTime(dtDate.EditValue);

                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Add_SupportiveCounselling(
                    pSelectedPatient.code,
                    mTransDate,
                    mCounsellorCode, 
                    txtProblem.Text, 
                    txtReply.Text,
                    Program.gCurrentUser.Code);

                if (mCtcClient.Exe_Result == 0)
                {
                    Program.Display_Error(mCtcClient.Exe_Message);
                    return;
                }
                if (mCtcClient.Exe_Result == -1)
                {
                    Program.Display_Server_Error(mCtcClient.Exe_Message);
                    return;
                }

                Program.Display_Info("Record added successfully");

                this.pRecordSaved = true;
                this.Close();
            }
            catch(Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Data_Edit
        private void Data_Edit()
        {
            string mFunctionName = "Data_Edit";

            if (Program.IsDate(dtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                dtDate.Focus();
                return;
            }
            try
            {
                DateTime? mTransDate = null;

                string mCounsellorCode = cboCounsellor.GetColumnValue("code").ToString();

                mTransDate = Convert.ToDateTime(dtDate.EditValue);

                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Edit_SupportiveCounselling(
                    mAutoCode,
                    pSelectedPatient.code,
                    mTransDate,
                    mCounsellorCode,
                    txtProblem.Text,
                    txtReply.Text,
                    Program.gCurrentUser.Code);

                if (mCtcClient.Exe_Result == 0)
                {
                    Program.Display_Error(mCtcClient.Exe_Message);
                    return;
                }
                if (mCtcClient.Exe_Result == -1)
                {
                    Program.Display_Server_Error(mCtcClient.Exe_Message);
                    return;
                }

                Program.Display_Info("Record edited successfully");

                this.pRecordSaved = true;
                this.Close();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdEdit_Click
        private void cmdEdit_Click(object sender, EventArgs e)
        {

            string mFunctionName = "cmdEdit_Click";

            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)grdHistory.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    Program.Display_Error("Please select a session and try again");
                    grdHistory.Focus();
                    return;
                }

                DataRow mSelectedDataRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                if (mSelectedDataRow != null)
                {
                    mAutoCode = System.Convert.ToInt32(mSelectedDataRow["autocode"].ToString());

                    txtProblem.Text = mSelectedDataRow["problem"].ToString();
                    txtReply.Text = mSelectedDataRow["reply"].ToString();

                    if (mSelectedDataRow["transdate"] != DBNull.Value)
                    {
                        dtDate.EditValue = Convert.ToDateTime(mSelectedDataRow["transdate"]);
                    }

                    cboCounsellor.EditValue = mSelectedDataRow["counsellorcode"].ToString().Trim();

                    mEdit = true;
                }
            }
            catch(Exception Ex)
            {
                Program.Display_Error(pClassName, mFunctionName, Ex.Message);
                return;
            }
                       
        }
        #endregion

        #region cmdClear_Click
        private void cmdClear_Click(object sender, EventArgs e)
        {
         
            Data_Clear();
        }
        #endregion
    }
}