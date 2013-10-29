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
using System.Windows;

namespace AfyaPro_NextGen
{
    public partial class frmRCHMaternity1 : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsRCHBirthMethods pMdtRCHBirthMethods;
        internal AfyaPro_Types.clsPatient gCurrentPatient;
        private AfyaPro_MT.clsRegistrations pMdtRegistrations;
        private AfyaPro_MT.clsRCHBirthComplications pMdtRCHBirthComplications;
        internal AfyaPro_Types.clsBooking gCurrentBooking = new AfyaPro_Types.clsBooking();
        private AfyaPro_MT.clsRCHClients pMdtRCHClients;
        private AfyaPro_MT.clsRCHMaternity pMdtRCHMaternity;
           
        private AfyaPro_MT.clsRCHPostNatalCare pMdtRCHPostnatal;
        private AfyaPro_MT.clsMedicalStaffs pMdtMedicalStaffs;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private AfyaPro_Types.clsRchClient pCurrentRchClient;

        private int pFormWidth = 0;
        private int pFormHeight = 0;
        private bool pSearching = false;

        private string pCurrRchClient = "";
        private string pPrevRchClient = "";
        private bool pSearchingRchClient = false;
        internal string gBillingGroupCode = "";
        internal string gBillingGroupDescription = "";
        internal string gBillingSubGroupCode = "";
        internal string gBillingSubGroupDescription = "";
        internal string gBillingGroupMembershipNo = "";
        internal string gPriceCategory = "";
        internal double gWeight = 0;
        internal double gTemperature = 0;
        internal int gAdmissionId = 0;
        internal string gWardCode = "";
        internal string gRoomCode = "";
        internal string gBedNo = "";
        internal string gRemarks = "";
        internal object gLastAttendanceDate = null;
        DataTable mDtBeds;

        private DataTable pDtBirthMethods = new DataTable("birthmethods");
        private DataTable pDtComplications = new DataTable("complications");
        private DataTable pDtHistory = new DataTable("history");
        private DataTable pDtExtraColumns = new DataTable("extracolumns");
        private DataTable pDtAttendants = new DataTable("attendants");
        private DataTable pDtChildren = new DataTable("children");
        private DataTable pDtMaternityPatient = new DataTable("maternitypatient");

        internal bool gCalledFromClientsRegister = false;
        internal string gClientCode = "";
        internal bool gDataSaved = false;

        #endregion

        #region frmRCHMaternity1()
        public frmRCHMaternity1()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmRCHMaternity1";

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                pMdtRegistrations = (AfyaPro_MT.clsRegistrations)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRegistrations),
                    Program.gMiddleTier + "clsRegistrations");

                //pMdtRCHBirthComplications = (AfyaPro_MT.clsRCHBirthComplications)Activator.GetObject(
                //    typeof(AfyaPro_MT.clsRCHBirthComplications),
                //    Program.gMiddleTier + "clsRCHBirthComplications");

                pMdtRCHClients = (AfyaPro_MT.clsRCHClients)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRCHClients),
                    Program.gMiddleTier + "clsRCHClients");

                //pMdtMedicalStaffs = (AfyaPro_MT.clsMedicalStaffs)Activator.GetObject(
                //    typeof(AfyaPro_MT.clsMedicalStaffs),
                //    Program.gMiddleTier + "clsMedicalStaffs");

                pMdtRCHMaternity = (AfyaPro_MT.clsRCHMaternity)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRCHMaternity),
                    Program.gMiddleTier + "clsRCHMaternity");

                pDtAttendants.Columns.Add("code", typeof(System.String));
                pDtAttendants.Columns.Add("description", typeof(System.String));
                
                //children
                pDtChildren.Columns.Add("deliverydate", typeof(System.DateTime));
                pDtChildren.Columns.Add("gender", typeof(System.String));
                pDtChildren.Columns.Add("weight", typeof(System.Double));
                pDtChildren.Columns.Add("apgarscore", typeof(System.Double));
                pDtChildren.Columns.Add("childproblems", typeof(System.String));
                pDtChildren.Columns.Add("stillbirth", typeof(System.String));
                pDtChildren.Columns.Add("childcondition", typeof(System.String));

                //columns for birth methods
                pDtBirthMethods.Columns.Add("code", typeof(System.String));
                pDtBirthMethods.Columns.Add("description", typeof(System.String));

                //columns for complications
                pDtComplications.Columns.Add("selected", typeof(System.Boolean));
                pDtComplications.Columns.Add("description", typeof(System.String));
                pDtComplications.Columns.Add("fieldvalue", typeof(System.Int16));
                pDtComplications.Columns.Add("fieldname", typeof(System.String));
                pDtComplications.Columns.Add("quantityentry", typeof(System.Int16));

               // grdComplications.DataSource = pDtComplications;

                //columns for history
                //pDtExtraColumns = pMdtRCHMaternity.View_Archive("1=2", "");

                //pDtHistory.Columns.Add("AdmissionDate", typeof(System.DateTime));
                foreach (DataColumn mDataColumn in pDtExtraColumns.Columns)
                {
                    pDtHistory.Columns.Add(mDataColumn.ColumnName, mDataColumn.DataType);
                }

                grdHistory.DataSource = pDtHistory;

                //hide unneccessary columns
                //foreach (DevExpress.XtraGrid.Columns.GridColumn mGridColumn in viewHistory.Columns)
                //{
                //    if (mGridColumn.FieldName.ToLower() != "bookdate"
                //        && mGridColumn.FieldName.ToLower() != "birthmethoddescription"
                //        && mGridColumn.FieldName.ToLower() != "gravida"
                //        && mGridColumn.FieldName.ToLower() != "para"
                //        && mGridColumn.FieldName.ToLower() != "booking")
                //    {
                //        mGridColumn.Visible = false;
                //    }
                //}

                //fill lookup data
                //this.Fill_BirthMethods();
                //this.Fill_Complications();
                //this.Fill_Attendants();

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.rchfamilyplanning_customizelayout.ToString());
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
            mObjectsList.Add(cmdSearch);
            mObjectsList.Add(cmdAdd);
            mObjectsList.Add(cmdDelete);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
            this.Data_Clear();
        }

        #endregion

        #region frmRCHMaternity1_Load
        private void frmRCHMaternity1_Load(object sender, EventArgs e)
        {
            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            this.Load_Controls();
            this.Fill_History();
                       

            txtDate.EditValue = Program.gMdiForm.txtDate.EditValue;
            txtAdmissionDate.EditValue = txtDate.EditValue;
            txbBirthDateFormat.Text = "(" + Program.gCulture.DateTimeFormat.ShortDatePattern + ")";

            this.pFormWidth = this.Width;
            this.pFormHeight = this.Height;

            Program.Center_Screen(this);

            if (gCalledFromClientsRegister == true)
            {
                txtClientId.Text = gClientCode;
                this.Search_Client();
                this.Data_Display(pCurrentRchClient);
            }

            this.Append_ShortcutKeys();
        }
        #endregion

        #region frmRCHMaternity1_FormClosing
        private void frmRCHMaternity1_FormClosing(object sender, FormClosingEventArgs e)
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

        #region LoadVacantWardBeds

        private void LoadVacantWardBeds()

        {
            try
            {
                

                DataTable mtBeds = pMdtRCHMaternity.LoadvacantBeds();
                cboBedNo.Properties.Items.Clear();
                mDtBeds = mtBeds;
                foreach (DataRow mRow in mDtBeds.Rows)
                {


                    cboBedNo.Properties.Items.Add(mRow["description"].ToString());
                }
            }
            catch { } 
        }
        #endregion

        #region Append_ShortcutKeys
        private void Append_ShortcutKeys()
        {
            cmdSearch.Text = cmdSearch.Text + " (" + Program.KeyCode_RchSearchClient.ToString() + ")";
            cmdAdd.Text = cmdAdd.Text + " (" + Program.KeyCode_ItemAdd.ToString() + ")";
            cmdDelete.Text = cmdDelete.Text + " (" + Program.KeyCode_ItemRemove.ToString() + ")";
            //cmdUpdate.Text = cmdUpdate.Text + " (" + Program.KeyCode_ItemUpdate.ToString() + ")";
            cmdClear.Text = cmdClear.Text + " (" + Program.KeyCode_RchClear.ToString() + ")";
        }
        #endregion

        #region Admit
        private void Admit()
        {
            string mGroupCode = "";
            string mSubGroupCode = "";
            string mFunctionName = "Admit";


            gBedNo = cboBedNo.Text;
           
            try
            {
                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                foreach (DataRow mRow in mDtBeds.Rows)
                {
                    if (cboBedNo.Text == mRow["description"].ToString())
                    {
                        gBedNo = mRow["CODE"].ToString();
                        gRoomCode = mRow["roomcode"].ToString();
                        gWardCode = mRow["wardcode"].ToString();
                        //System.Windows.Forms.MessageBox.Show(gBedNo.ToString());
                    }
                }

                if (txtClientId.Text.Trim() != "" && txtClientId.Text.Trim().ToLower() != "<<---new--->>")
                {
                                                         

                    #region get current admission details

                    AfyaPro_Types.clsBooking mLastBooking = pMdtRegistrations.Get_Booking(txtClientId.Text);

                    if (mLastBooking != null)
                    {
                        AfyaPro_Types.clsAdmission mLastAdmission = pMdtRegistrations.Get_Admission(mLastBooking.Booking, txtClientId.Text);

                        if (mLastAdmission != null)
                        {
                            AfyaPro_Types.clsDischarge mDischargeDetails = pMdtRegistrations.Get_Discharge(
                                mLastBooking.Booking, txtClientId.Text, mLastAdmission.WardCode,
                                mLastAdmission.RoomCode);

                            if (mDischargeDetails.IsDischarged == false)
                            {
                                gAdmissionId = mLastAdmission.AdmissionId;
                                //gWardCode = mLastAdmission.WardCode;
                                //gRoomCode = mLastAdmission.RoomCode;
                                //gBedNo = mLastAdmission.BedCode;
                                gRemarks = mLastAdmission.PatientCondition;

                                if (mTransDate.Date == mLastBooking.BookDate.Date)
                                {
                                    gWeight = mLastBooking.Weight;
                                    gTemperature = mLastBooking.Temperature;
                                }
                            }
                        }

                    }

                    else
                    {
                        

                    }
 

                    #endregion

                   
                        gCurrentBooking = pMdtRegistrations.Admit(gAdmissionId, mTransDate,
                            txtClientId.Text, gBillingGroupCode, gBillingSubGroupCode, gBillingGroupMembershipNo, gPriceCategory,
                            gWardCode, gRoomCode, gRemarks, gBedNo, gWeight, gTemperature, Program.gCurrentUser.Code);

                        if (gCurrentBooking.Exe_Result == 0)
                        {
                            //MessageBox.Show("Yeas");
                            Program.Display_Error(gCurrentBooking.Exe_Message);
                            return;
                        }
                        if (gCurrentBooking.Exe_Result == -1)
                        {
                            Program.Display_Server_Error(gCurrentBooking.Exe_Message);
                            return;
                        }
                  

                    #region documents printing

                    frmOPDPatientDocuments mOPDPatientDocuments;

                    if (gCurrentBooking.IsNewAttendance == true)
                    {
                        //new patient is registered
                        mOPDPatientDocuments = new frmOPDPatientDocuments();
                        mOPDPatientDocuments.AutoPrint_Documents(txtClientId.Text,
                            Convert.ToInt16(AfyaPro_Types.clsEnums.PrintedWhens.NewPatientIsRegistered));
                    }
                    else
                    {
                        //patient is reattending
                        mOPDPatientDocuments = new frmOPDPatientDocuments();
                        mOPDPatientDocuments.AutoPrint_Documents(txtClientId.Text,
                            Convert.ToInt16(AfyaPro_Types.clsEnums.PrintedWhens.PatientIsReAttending));
                    }

                    if (gCurrentBooking.IsNewAttendance == true)
                    {
                        //patient is admitted
                        mOPDPatientDocuments = new frmOPDPatientDocuments();
                        mOPDPatientDocuments.AutoPrint_Documents(txtClientId.Text,
                            Convert.ToInt16(AfyaPro_Types.clsEnums.PrintedWhens.PatientIsAdmitted));
                    }

                    #endregion

                    if (Program.GrantDeny_FunctionAccess(AfyaPro_Types.clsSystemFunctions.FunctionKeys.ipdregistrations_showsuccessmessage.ToString()) == true)
                    {
                        Program.Display_Info(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_AdmissionSuccess.ToString());
                    }

                    if (Program.GrantDeny_FunctionAccess(AfyaPro_Types.clsSystemFunctions.FunctionKeys.ipdregistrations_closeafterregistration.ToString()) == true)
                    {
                        this.Close();
                    }
                    else
                    {
                       // this.Mode_New();
                    }
                }
                else
                {
                    Program.Display_Info(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_OnlyRegisteredCanBeAdmitted.ToString());
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region viewHistory_RowClick
        private void viewHistory_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            pSearching = false;
            if (e.RowHandle <= -1)
            {
                return;
            }

            this.Display_RowDetails(viewHistory.GetDataRow(e.RowHandle));
        }
        #endregion

        #region viewHistory_GotFocus
        private void viewHistory_GotFocus(object sender, EventArgs e)
        {

            if (viewHistory.FocusedRowHandle <= -1)
            {
                return;
            }

            this.Display_RowDetails(viewHistory.GetDataRow(viewHistory.FocusedRowHandle));
        }
        #endregion

        #region Fill_Complications
        private void Fill_Complications()
        {
            string mFunctionName = "Fill_Complications";

            try
            {
                pDtComplications.Rows.Clear();

                DataTable mDtComplications = pMdtRCHBirthComplications.View("", "", Program.gLanguageName, "");

                foreach (DataRow mDataRow in mDtComplications.Rows)
                {
                    DataRow mNewRow = pDtComplications.NewRow();
                    mNewRow["selected"] = false;
                    mNewRow["description"] = mDataRow["description"];
                    mNewRow["fieldvalue"] = 1;
                    mNewRow["fieldname"] = mDataRow["code"];
                    pDtComplications.Rows.Add(mNewRow);
                    pDtComplications.AcceptChanges(); 
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_BirthMethods
        private void Fill_BirthMethods()
        {
            string mFunctionName = "Fill_BirthMethods";

            try
            {
                pDtBirthMethods.Rows.Clear();

                DataTable mDtBirthMethods = pMdtRCHBirthMethods.View("", "", Program.gLanguageName, "");

                foreach (DataRow mDataRow in mDtBirthMethods.Rows)
                {
                    DataRow mNewRow = pDtBirthMethods.NewRow();
                    mNewRow["description"] = mDataRow["description"];
                    mNewRow["code"] = mDataRow["code"];
                    pDtBirthMethods.Rows.Add(mNewRow);
                    pDtBirthMethods.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_Attendants
        private void Fill_Attendants()
        {
            string mFunctionName = "Fill_Attendants";

            try
            {
                pDtAttendants.Rows.Clear();

                DataTable mDtAttendants = pMdtMedicalStaffs.View(
                    "category=" + Convert.ToInt16(AfyaPro_Types.clsEnums.StaffCategories.BirthAttendants),
                    "code", Program.gLanguageName, "grdGENMedicalStaffs");
                foreach (DataRow mDataRow in mDtAttendants.Rows)
                {
                    DataRow mNewRow = pDtAttendants.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtAttendants.Rows.Add(mNewRow);
                    pDtAttendants.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtAttendants.Columns)
                {
                    mDataColumn.Caption = mDtAttendants.Columns[mDataColumn.ColumnName].Caption;
                }
                
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        //#region Fill_Children
        //private void Fill_Children(string mBooking)
        //{
        //    string mFunctionName = "Fill_Children";

        //    try
        //    {
        //        pDtChildren.Rows.Clear();

        //        string mFilter = "clientcode='" + txtClientId.Text.Trim() + "' and booking='" + mBooking.Trim() + "'";

        //        DataTable mDtChildren = pMdtRCHPostNatalCare.View_Children(mFilter, "deliverydate desc,autocode desc");

        //        foreach (DataRow mDataRow in mDtChildren.Rows)
        //        {
        //            string mStillBirth = "";
        //            if (Convert.ToInt16(mDataRow["freshbirth"]) == 1)
        //            {
        //                mStillBirth = AfyaPro_Types.clsEnums.RCHStillBirths.freshbirth.ToString().Trim();
        //            }
        //            if (Convert.ToInt16(mDataRow["maceratedbirth"]) == 1)
        //            {
        //                mStillBirth = AfyaPro_Types.clsEnums.RCHStillBirths.maceratedbirth.ToString().Trim();
        //            }

        //            string mChildCondition = "";
        //            if (Convert.ToInt16(mDataRow["live"]) == 1)
        //            {
        //                mChildCondition = AfyaPro_Types.clsEnums.RCHChildConditions.live.ToString().Trim();
        //            }
        //            if (Convert.ToInt16(mDataRow["deathbefore24"]) == 1)
        //            {
        //                mChildCondition = AfyaPro_Types.clsEnums.RCHChildConditions.deathbefore24.ToString().Trim();
        //            }
        //            if (Convert.ToInt16(mDataRow["deathafter24"]) == 1)
        //            {
        //                mChildCondition = AfyaPro_Types.clsEnums.RCHChildConditions.deathafter24.ToString().Trim();
        //            }

        //            DataRow mNewRow = pDtChildren.NewRow();
        //            mNewRow["deliverydate"] = mDataRow["deliverydate"];
        //            mNewRow["gender"] = mDataRow["gender"];
        //            mNewRow["weight"] = mDataRow["weight"];
        //            mNewRow["apgarscore"] = mDataRow["apgarscore"];
        //            mNewRow["childproblems"] = mDataRow["childproblems"];
        //            mNewRow["stillbirth"] = mStillBirth;
        //            mNewRow["childcondition"] = mChildCondition;
        //            pDtChildren.Rows.Add(mNewRow);
        //            pDtChildren.AcceptChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Program.Display_Error(pClassName, mFunctionName, ex.Message);
        //        return;
        //    }
        //}
        //#endregion

        #region Fill_History
        private void Fill_History()
        {
            string mFunctionName = "Fill_History";

            try
            {
                pDtHistory.Rows.Clear();

                pDtHistory = pMdtRCHMaternity.View_Archive();

                pDtHistory.Columns["patientcode"].Caption = "Patient Code";
                pDtHistory.Columns["firstname"].Caption = "FName";
                pDtHistory.Columns["surname"].Caption = "Surname";
                pDtHistory.Columns["admissionnumber"].Caption = "Admission No.";
                pDtHistory.Columns["bednumber"].Caption = "Bed No.";
                pDtHistory.Columns["referringfacility"].Caption = "Ref. Facility";
                pDtHistory.Columns["admissionreasons"].Caption = "Why Admitted";
                pDtHistory.Columns["admissiondate"].Caption = "Date Admitted";
                pDtHistory.Columns["patientstatus"].Caption = "Status";
                pDtHistory.Columns["birthdate"].Caption = "DOB";

               
                pDtHistory.AcceptChanges();


                grdHistory.DataSource = pDtHistory;

                this.LoadVacantWardBeds();
                
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
            

            this.Details_Clear();

            pPrevRchClient = "";
            pCurrRchClient = pPrevRchClient;

            txtClientId.Focus();
        }
        #endregion

        #region Details_Clear
        private void Details_Clear()
        {
            foreach (DataRow mDataRow in pDtComplications.Rows)
            {
                mDataRow.BeginEdit();
                mDataRow["selected"] = false;
                mDataRow["fieldvalue"] = 1;
                mDataRow.EndEdit();
            }

            txtAdmissionDate.EditValue = txtDate.EditValue;
            //cboBirthMethod.EditValue = null;
            txtAdmissionNo.Text = "";
            cboBedNo.Text = "";

            txtName.ResetText();
            txtGender.ResetText();
            txtBirthDate.ResetText();
            txtYears.ResetText();
            txtMonths.ResetText();
            txtAdmissionDate.ResetText();
            txtAdmissionNo.ResetText();
            txtAdmissionReason.ResetText();
            cboBedNo.ResetText();
            txtClientId.ResetText();
            txtFacility.ResetText();
            //cboDischargeStatus.Text = "";
            //cboAttendant.EditValue = null;
        }
        #endregion

        #region Data_Display
        internal void Data_Display(AfyaPro_Types.clsRchClient mPatient)
        {
            String mFunctionName = "Data_Display";
            pSearching = true;
            try
            {
                
                this.Data_Clear();
                this.LoadVacantWardBeds();

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

                        txtClientId.Text = mPatient.code;
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

                        this.Fill_History();

                        pCurrRchClient = mPatient.code;
                        pPrevRchClient = pCurrRchClient;
                        txtDate.EditValue =   Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);
                        txtAdmissionDate.ResetText();
                        txtAdmissionNo.ResetText();
                        txtAdmissionReason.ResetText();
                        cboBedNo.ResetText();
                        txtFacility.ResetText();


                        foreach (DataRow mRow in pDtHistory.Rows)
                        {

                            if (txtClientId.Text == mRow["patientcode"].ToString().Trim())
                            {
                                txtDate.EditValue = Convert.ToDateTime(mRow["admissiondate"]);
                                if (Program.IsNullDate(mRow["admissiondate"]) == false)
                                {
                                    txtAdmissionDate.EditValue = Convert.ToDateTime(mRow["admissiondate"]);
                                }
                                else
                                {
                                    txtAdmissionDate.EditValue = DBNull.Value;
                                }

                                //cboBirthMethod.ItemIndex = Program.Get_LookupItemIndex(cboBirthMethod, "code", mSelectedRow["birthmethod"].ToString());
                                txtAdmissionNo.Text = mRow["admissionnumber"].ToString().Trim();
                                cboBedNo.Text = mRow["bednumber"].ToString().Trim();
                                txtFacility.Text = mRow["referringfacility"].ToString();
                                txtAdmissionReason.Text = mRow["admissionreasons"].ToString().Trim();
                                txtClientId.Text = mRow["patientcode"].ToString().Trim();
                                txtName.Text = mRow["firstname"].ToString().Trim() + " " + mRow["surname"].ToString().Trim();
                                txtGender.Text = "Female";
                            }

                        }

                        System.Media.SystemSounds.Beep.Play();
                        
                    }
                    else
                    {
                        pCurrRchClient = txtClientId.Text;
                        pPrevRchClient = pCurrRchClient;
                    }
                }
                else
                {
                    pCurrRchClient = txtClientId.Text;
                    pPrevRchClient = pCurrRchClient;
                }
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

            if (pSearching == true)
            {
                return;
            }
            int mRowIndex;

          
           

            txtDate.EditValue = Convert.ToDateTime(mSelectedRow["admissiondate"]);
            if (Program.IsNullDate(mSelectedRow["admissiondate"]) == false)
            {
                txtAdmissionDate.EditValue = Convert.ToDateTime(mSelectedRow["admissiondate"]);
            }
            else
            {
                txtAdmissionDate.EditValue = DBNull.Value;
            }

            //cboBirthMethod.ItemIndex = Program.Get_LookupItemIndex(cboBirthMethod, "code", mSelectedRow["birthmethod"].ToString());
            txtAdmissionNo.Text = mSelectedRow["admissionnumber"].ToString().Trim();
            cboBedNo.Text = mSelectedRow["bednumber"].ToString().Trim();
            txtFacility.Text = mSelectedRow["referringfacility"].ToString();
            txtAdmissionReason.Text = mSelectedRow["admissionreasons"].ToString().Trim();
            txtClientId.Text = mSelectedRow["patientcode"].ToString().Trim();
            txtName.Text = mSelectedRow["firstname"].ToString().Trim() + " "  +  mSelectedRow["surname"].ToString().Trim();
            txtGender.Text = "Female";

            DateTime mBirthDate = Convert.ToDateTime(mSelectedRow["birthdate"]);
            int mDays = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue).Subtract(mBirthDate).Days;
            int mYears = (int)mDays / 365;
            int mMonths = (int)(mDays % 365) / 30;

            txtYears.Text = mYears.ToString();
            txtMonths.Text = mMonths.ToString();


            //this.Fill_Children(mSelectedRow["booking"].ToString().Trim());
        }
        #endregion

        #region viewHistory_FocusedRowChanged
        private void viewHistory_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle <= -1)
            {
                return;
            }

            this.Display_RowDetails(viewHistory.GetDataRow(e.FocusedRowHandle));
        }
        #endregion

        #region Search_Client
        private AfyaPro_Types.clsRchClient Search_Client()
        {
            string mFunctionName = "Search_Client";

            try
            {
               
                pCurrentRchClient = pMdtRCHClients.Get_Client(txtClientId.Text);
                return pCurrentRchClient;
                                               
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
                this.pCurrentRchClient = this.Search_Client();
                this.Data_Display(pCurrentRchClient);
            }
        }
        #endregion

        #region txtClientId_Leave
        private void txtClientId_Leave(object sender, EventArgs e)
        {
            pCurrRchClient = txtClientId.Text;

            if (pCurrRchClient.Trim().ToLower() != pPrevRchClient.Trim().ToLower())
            {
                this.pCurrentRchClient = this.Search_Client();
                this.Data_Display(pCurrentRchClient);
            }
        }
        #endregion

        #region cmdSearch_Click
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            pSearchingRchClient = true;

            frmSearchRCHClient mSearchRCHClient = new frmSearchRCHClient(txtClientId);
            mSearchRCHClient.ShowDialog();

            pSearchingRchClient = false;
        }
        #endregion

        #region txtClientId_EditValueChanged
        private void txtClientId_EditValueChanged(object sender, EventArgs e)
        {
            if (pSearchingRchClient == true)
            {
                this.pCurrentRchClient = this.Search_Client();
                this.Data_Display(pCurrentRchClient);
            }
        }
        #endregion

        #region cmdDelivery_Click
        private void cmdChildren_Click(object sender, EventArgs e)
        {
            #region validation

            if (txtClientId.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RCH_ClientCodeIsInvalid.ToString());
                txtClientId.Focus();
                return;
            }

            AfyaPro_Types.clsRchClient mRchClient = pMdtRCHClients.Get_Client(txtClientId.Text);
            if (mRchClient.Exist == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RCH_ClientCodeDoesNotExist.ToString());
                txtClientId.Focus();
                return;
            }

            #endregion
            string mBooking ="";

            frmRCHMaternityDelivery mRCHMaternityDelivery = new frmRCHMaternityDelivery(txtClientId.Text, mBooking, txtName.Text, txtYears.Text, txtMonths.Text, txtAdmissionNo.Text , Convert.ToDateTime(txtAdmissionDate.EditValue));
            mRCHMaternityDelivery.ShowDialog();
        }
        #endregion

        #region cmdAdd_Click
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();

            string mFunctionName = "cmdAdd_Click";

            #region validation

            if (Program.IsDate(txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                txtDate.Focus();
                return;
            }

            if (txtClientId.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RCH_ClientCodeIsInvalid.ToString());
                txtClientId.Focus();
                return;
            }

            AfyaPro_Types.clsRchClient mRchClient = pMdtRCHClients.Get_Client(txtClientId.Text);
            if (mRchClient.Exist == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RCH_ClientCodeDoesNotExist.ToString());
                txtClientId.Focus();
                return;
            }

            bool mValidClient = Program.Validate_RchClient(
                AfyaPro_Types.clsEnums.RCHServices.maternity.ToString(), txtClientId.Text);
            if (mValidClient == false)
            {
                return;
            }

            #endregion

            try
            {
                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

               

                
                string mAdmissionNo = "";
                if (Program.IsNumeric(txtAdmissionNo.Text) == true)
                {
                    mAdmissionNo =  txtAdmissionNo.Text;
                }

                string mBedNo = "";
                if (Program.IsNumeric(cboBedNo.Text) == true)
                {
                    mBedNo =  cboBedNo.Text;
                }

                DateTime mAdmissionDate = new DateTime();
                if (Program.IsNullDate(txtAdmissionDate.EditValue) == false)
                {
                    mAdmissionDate = Convert.ToDateTime(txtAdmissionDate.EditValue);
                }

                string mAdmissionReason = txtAdmissionReason.Text;

                string mWard ="";
                string mFacility = txtFacility .Text ;

                foreach (DataRow mRow in pDtHistory.Rows)
                {

                    if (txtAdmissionNo.Text == mRow["admissionnumber"].ToString().Trim())
                    {

                        if (txtClientId.Text == mRow["patientcode"].ToString().Trim())
                        {
                            System.Windows.Forms.MessageBox.Show("This admission was already recorded", "Admission", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("This admission was recorded for [" + mRow["patientcode"] + ": " + mRow["firstname"].ToString() + " " + mRow["surname"].ToString() + "]", "Admission", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        //cboBirthMethod.ItemIndex = Program.Get_LookupItemIndex(cboBirthMethod, "code", mSelectedRow["birthmethod"].ToString());
                        txtAdmissionNo.Text = mRow["admissionnumber"].ToString().Trim();
                        cboBedNo.Text = mRow["bednumber"].ToString().Trim();
                        txtFacility.Text = mRow["referringfacility"].ToString();
                        txtAdmissionReason.Text = mRow["admissionreasons"].ToString().Trim();
                        txtClientId.Text = mRow["patientcode"].ToString().Trim();
                        txtName.Text = mRow["firstname"].ToString().Trim() + " " + mRow["surname"].ToString().Trim();
                        txtGender.Text = "Female";
                    }

                }

              
                mResult = pMdtRCHMaternity.Add(txtClientId.Text, mAdmissionDate, mAdmissionNo, mBedNo, mWard,
                mFacility, mAdmissionReason,  Program.gCurrentUser.Code, mTransDate);



               // MessageBox.Show("Hallo");

                if (mResult.Exe_Result == 0)
                {
                    Program.Display_Error(mResult.Exe_Message);
                    return;
                }
                if (mResult.Exe_Result == -1)
                {
                    Program.Display_Server_Error(mResult.Exe_Message);
                    return;
                }
                if (mResult.Exe_Result == 5)
                {
                   System.Windows.Forms.MessageBox.Show("This patient was already admitted in maternity", "Admission", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   return;
                }

                this.Admit();

                if (gCalledFromClientsRegister == true)
                {
                    gDataSaved = true;
                    this.Close();
                }
                else
                {
                    //refresh
                    txtClientId.Text = "";
                    this.Fill_History();
                    this.Data_Clear();

                }

                 
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdUpdate_Click
        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();

            string mFunctionName = "cmdUpdate_Click";

            #region validation

            if (Program.IsDate(txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                txtDate.Focus();
                return;
            }

            if (txtClientId.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RCH_ClientCodeIsInvalid.ToString());
                txtClientId.Focus();
                return;
            }

            AfyaPro_Types.clsRchClient mRchClient = pMdtRCHClients.Get_Client(txtClientId.Text);
            if (mRchClient.Exist == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RCH_ClientCodeDoesNotExist.ToString());
                txtClientId.Focus();
                return;
            }

            #endregion

            try
            {
                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

                if (viewHistory.FocusedRowHandle <= -1)
                {
                    return;
                }

                DataRow mSelectedRow = viewHistory.GetDataRow(viewHistory.FocusedRowHandle);

                //birth complications
                DataTable mDtComplications = new DataTable("complications");
                mDtComplications.Columns.Add("fieldname", typeof(System.String));
                mDtComplications.Columns.Add("fieldvalue", typeof(System.Double));

                foreach (DataRow mDataRow in pDtComplications.Rows)
                {
                    double mFieldValue = 0;

                    if (Convert.ToBoolean(mDataRow["selected"]) == true)
                    {
                        mFieldValue = 1;
                    }

                    DataRow mNewRow = mDtComplications.NewRow();
                    mNewRow["fieldname"] = mDataRow["fieldname"];
                    mNewRow["fieldvalue"] = mFieldValue;
                    mDtComplications.Rows.Add(mNewRow);
                    mDtComplications.AcceptChanges();
                }

                //birth method
                //if (cboBirthMethod.ItemIndex == -1)
                //{
                //    Program.Display_Error("Invalid birth method");
                //    cboBirthMethod.Focus();
                //    return;
                //}

                //string mBirthMethod = cboBirthMethod.GetColumnValue("code").ToString().Trim();

                  string mAdmissionNo = "";
                if (Program.IsNumeric(txtAdmissionNo.Text) == true)
                {
                    mAdmissionNo =  txtAdmissionNo.Text;
                }

                string mBedNo = "";
                if (Program.IsNumeric(cboBedNo.Text) == true)
                {
                    mBedNo =  cboBedNo.Text;
                }

                DateTime mAdmissionDate = new DateTime();
                if (Program.IsNullDate(txtAdmissionDate.EditValue) == false)
                {
                    mAdmissionDate = Convert.ToDateTime(txtAdmissionDate.EditValue);
                }

                string mAdmissionReason = txtAdmissionReason.Text;

                string mWard ="";
                string mFacility = txtFacility .Text ;

                //if (cboAttendant.ItemIndex == -1)
                //{
                //    Program.Display_Error("Invalid attendant");
                //    cboAttendant.Focus();
                //    return;
                //}

                //string mAttendantId = cboAttendant.GetColumnValue("code").ToString().Trim();
                //string mAttendantName = cboAttendant.GetColumnValue("description").ToString().Trim();

                //edit
                mResult = pMdtRCHMaternity.Edit(txtClientId.Text, mAdmissionDate, mAdmissionNo, mBedNo, mWard,
                mFacility, mAdmissionReason, Program.gCurrentUser.Code, mTransDate);
                //cboDischargeStatus.Text, mAttendantId, mAttendantName, 0, pDtChildren, Program.gCurrentUser.Code);

                if (mResult.Exe_Result == 0)
                {
                    Program.Display_Error(mResult.Exe_Message);
                    return;
                }
                if (mResult.Exe_Result == -1)
                {
                    Program.Display_Server_Error(mResult.Exe_Message);
                    return;
                }

                //refresh
                this.Fill_History();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdDelete_Click
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();

            string mFunctionName = "cmdDelete_Click";

            #region validation

            if (Program.IsDate(txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                txtDate.Focus();
                return;
            }

            if (txtClientId.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RCH_ClientCodeIsInvalid.ToString());
                txtClientId.Focus();
                return;
            }

            AfyaPro_Types.clsRchClient mRchClient = pMdtRCHClients.Get_Client(txtClientId.Text);
            if (mRchClient.Exist == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RCH_ClientCodeDoesNotExist.ToString());
                txtClientId.Focus();
                return;
            }

            #endregion

            try
            {
                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

                if (txtClientId.Text =="")
                {
                    return;
                }

               // DataRow mSelectedRow = viewHistory.GetDataRow(viewHistory.FocusedRowHandle);

                DialogResult mResp = Program.Confirm_Deletion("'"
                    +  (txtClientId.Text.ToString() + "'"));

                if (mResp != DialogResult.Yes)
                {
                    return;
                }

                //delete
                mResult = pMdtRCHMaternity.Delete(mTransDate, txtAdmissionNo.Text.ToString(),
                txtClientId.Text, Program.gCurrentUser.Code);

                if (mResult.Exe_Result == 0)
                {
                    Program.Display_Error(mResult.Exe_Message);
                    return;
                }
                if (mResult.Exe_Result == -1)
                {
                    Program.Display_Server_Error(mResult.Exe_Message);
                    return;
                }

                //refresh
                this.Fill_History();
                this.Data_Clear();
                this.Details_Clear();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdClear_Click
        private void cmdClear_Click(object sender, EventArgs e)
        {
            this.Details_Clear();

            //grdComplications.Focus();
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region frmRCHPostnatalCare_KeyDown
        void frmRCHPostnatalCare_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Program.KeyCode_RchSearchClient:
                    {
                        pSearchingRchClient = true;

                        frmSearchRCHClient mSearchRCHClient = new frmSearchRCHClient(txtClientId);
                        mSearchRCHClient.ShowDialog();

                        pSearchingRchClient = false;
                    }
                    break;
                case Program.KeyCode_ItemAdd:
                    {
                        this.cmdAdd_Click(cmdAdd, e);
                    }
                    break;
                case Program.KeyCode_ItemRemove:
                    {
                        this.cmdDelete_Click(cmdDelete, e);
                    }
                    break;
                case Program.KeyCode_ItemUpdate:
                    {
                        //this.cmdUpdate_Click(cmdUpdate, e);
                    }
                    break;
                case Program.KeyCode_RchPostnatalChildren:
                    {
                        this.cmdChildren_Click(cmdChildren, e);
                    }
                    break;
                case Program.KeyCode_RchClear:
                    {
                        this.cmdClear_Click(cmdClear, e);
                    }
                    break;
            }
        }
        #endregion
    }
}