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
    public partial class frmRCHAntenatalCare : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsRCHDangerIndicators pMdtRCHDangerIndicators;
        private AfyaPro_MT.clsRCHClients pMdtRCHClients;
        private AfyaPro_MT.clsRCHAnteNatalCare pMdtRCHAnteNatalCare;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private AfyaPro_Types.clsRchClient pCurrentRchClient;

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private string pCurrRchClient = "";
        private string pPrevRchClient = "";
        private bool pSearchingRchClient = false;
        private string pCurrentBooking = "";

        private DataTable pDtDangerIndicators = new DataTable("dangerindicators");
        private DataTable pDtHistory = new DataTable("history");
        private DataTable pDtExtraColumns = new DataTable("extracolumns");

        internal bool gCalledFromClientsRegister = false;
        internal string gClientCode = "";
        internal bool gDataSaved = false;

        #endregion

        #region frmRCHAntenatalCare
        public frmRCHAntenatalCare()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmRCHAntenatalCare";

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                pMdtRCHDangerIndicators = (AfyaPro_MT.clsRCHDangerIndicators)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRCHDangerIndicators),
                    Program.gMiddleTier + "clsRCHDangerIndicators");

                pMdtRCHClients = (AfyaPro_MT.clsRCHClients)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRCHClients),
                    Program.gMiddleTier + "clsRCHClients");

                pMdtRCHAnteNatalCare = (AfyaPro_MT.clsRCHAnteNatalCare)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRCHAnteNatalCare),
                    Program.gMiddleTier + "clsRCHAnteNatalCare");

                //columns for methods used
                pDtDangerIndicators.Columns.Add("selected", typeof(System.Boolean));
                pDtDangerIndicators.Columns.Add("description", typeof(System.String));
                pDtDangerIndicators.Columns.Add("fieldvalue", typeof(System.Int16));
                pDtDangerIndicators.Columns.Add("fieldname", typeof(System.String));

                grdDangerIndicators.DataSource = pDtDangerIndicators;


                DataTable myTable = new DataTable();
                grdHistory.DataSource = myTable;

                //columns for history
                pDtExtraColumns = pMdtRCHAnteNatalCare.View_Archive("", "", txtClientId.Text.Trim());

                foreach (DataColumn mDataColumn in pDtExtraColumns.Columns)
                {
                    pDtHistory.Columns.Add(mDataColumn.ColumnName, mDataColumn.DataType);
                }

                //grdHistory.DataSource = pDtHistory;

                //foreach (DevExpress.XtraGrid.Columns.GridColumn mGridColumn in viewHistory.Columns)
                //{
                //    if (mGridColumn.FieldName.ToLower() != "bookdate"
                //        && mGridColumn.FieldName.ToLower() != "booking")
                //    {
                //        //viewHistory.Columns[mGridColumn.FieldName].Visible = false;
                //    }
                //}

                //fill methods
                //this.Fill_DangerIndicators();

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.rchantenatal_customizelayout.ToString());
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
            mObjectsList.Add(cmdSavePatient);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
            this.Data_Clear();
        }

        #endregion

        #region frmRCHAntenatalCare_Load
        private void frmRCHAntenatalCare_Load(object sender, EventArgs e)
        {
            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            this.Load_Controls();

            txtDate.EditValue = Program.gMdiForm.txtDate.EditValue;
            txbBirthDateFormat.Text = "(" + Program.gCulture.DateTimeFormat.ShortDatePattern + ")";

            this.pFormWidth = this.Width;
            this.pFormHeight = this.Height;
            this.txtEDD.Enabled = true;
            this.txtRegNo.Enabled = true;

            Program.Center_Screen(this);

            if (gCalledFromClientsRegister == true)
            {
                txtClientId.Text = gClientCode;
                txtRegNo.Text = gClientCode;
                this.Search_Client();
                this.Data_Display(pCurrentRchClient);
            }
            this.Append_ShortcutKeys();
        }
        #endregion

        #region frmRCHAntenatalCare_FormClosing
        private void frmRCHAntenatalCare_FormClosing(object sender, FormClosingEventArgs e)
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
            cmdSearch.Text = cmdSearch.Text + " (" + Program.KeyCode_RchSearchClient.ToString() + ")";
            cmdAdd.Text = cmdAdd.Text + " (" + Program.KeyCode_ItemAdd.ToString() + ")";
            cmdSavePatient.Text = cmdSavePatient.Text + " (" + Program.KeyCode_ItemRemove.ToString() + ")";
            cmdUpdate.Text = cmdUpdate.Text + " (" + Program.KeyCode_ItemUpdate.ToString() + ")";
            cmdVaccinations.Text = cmdVaccinations.Text + " (" + Program.KeyCode_RchVaccinations.ToString() + ")";
            cmdClear.Text = cmdClear.Text + " (" + Program.KeyCode_RchClear.ToString() + ")";
        }
        #endregion

        #region Fill_DangerIndicators
        private void Fill_DangerIndicators()
        {
            string mFunctionName = "Fill_DangerIndicators";

            try
            {
                pDtDangerIndicators.Rows.Clear();

                DataTable mDtMethods = pMdtRCHDangerIndicators.View("", "", Program.gLanguageName, "");

                foreach (DataRow mDataRow in mDtMethods.Rows)
                {
                    DataRow mNewRow = pDtDangerIndicators.NewRow();
                    mNewRow["selected"] = false;
                    mNewRow["description"] = mDataRow["description"];
                    mNewRow["fieldvalue"] = 1;
                    mNewRow["fieldname"] = mDataRow["code"];
                    pDtDangerIndicators.Rows.Add(mNewRow);
                    pDtDangerIndicators.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_History
        private void Fill_History()
        {
            string mFunctionName = "Fill_History";


           
            try
            {
                pDtHistory.Rows.Clear();
               
                DataTable mDtHistory = pMdtRCHAnteNatalCare.View_Archive(
                    "clientcode='" + txtClientId.Text.Trim() + "'", "bookdate desc,autocode desc", txtClientId.Text.Trim());

                foreach (DataRow mDataRow in mDtHistory.Rows)
                {
                    DataRow mNewRow = pDtHistory.NewRow();

                    foreach (DataColumn mDataColumn in pDtExtraColumns.Columns)
                    {
                        mNewRow[mDataColumn.ColumnName] = mDataRow[mDataColumn.ColumnName];
                    }
                    pDtHistory.Rows.Add(mNewRow);
                    pDtHistory.AcceptChanges();
                }

              

                grdHistory.DataSource = pDtHistory;
                // booking, bookdate, pregage,  weight, sp, fefo, syphilistest,  oncpt, onart
                viewHistory.Columns["booking"].Caption = "Booking";
                viewHistory.Columns["bookdate"].Caption = "Book Date";
                viewHistory.Columns["pregage"].Caption = "Pregnant Age";
                viewHistory.Columns["weight"].Caption = "Weight";
                viewHistory.Columns["sp"].Caption = "SP";
                viewHistory.Columns["fefo"].Caption = "FeFo";
                viewHistory.Columns["syphilistest"].Caption = "Syphilis Test";  
                viewHistory.Columns["oncpt"].Caption = "On CPT";
                viewHistory.Columns["onart"].Caption = "On ART";
                viewHistory.Columns["visitdate"].Caption = "Visit Date";
 
 


                 viewHistory.Columns[0].Visible = false;
                 viewHistory.Columns[1].Visible = false;
                 viewHistory.Columns[2].Visible = false;
                 viewHistory.Columns[4].Visible = false;
                 viewHistory.Columns[6].Visible = false;
                 viewHistory.Columns[7].Visible = false;
                 viewHistory.Columns[8].Visible = false;
                 viewHistory.Columns[9].Visible = false;
                 viewHistory.Columns[10].Visible = false;
                 viewHistory.Columns[11].Visible = false;
                 viewHistory.Columns[12].Visible = false;
                 viewHistory.Columns[13].Visible = false;
                 viewHistory.Columns[14].Visible = false;
                 viewHistory.Columns[15].Visible = false;
                 viewHistory.Columns[16].Visible = false;
                 viewHistory.Columns[17].Visible = false;
                 viewHistory.Columns[18].Visible = false;
                 viewHistory.Columns[19].Visible = false;
                 viewHistory.Columns[22].Visible = false;
                 viewHistory.Columns[23].Visible = false;
                 viewHistory.Columns[24].Visible = false;
                 viewHistory.Columns[27].Visible = false;
                 viewHistory.Columns[28].Visible = false;
                 viewHistory.Columns[29].Visible = false;
                 viewHistory.Columns[30].Visible = false;
                 viewHistory.Columns[31].Visible = false;
                 viewHistory.Columns[33].Visible = false;
                 viewHistory.Columns[35].Visible = false;
                 viewHistory.Columns[36].Visible = false;
                this.Details_Clear();
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
            pCurrentBooking = "";

            txtRegNo.Text = "";
            txtResidence.Text = "";
            txtGravida.Text = "";
            txtPara.Text = "";
            txtLMP.Text = "";
            txtEDD.Text = "";

            this.Details_Clear();

            pPrevRchClient = "";
            pCurrRchClient = pPrevRchClient;

            txtClientId.Focus();
            DataTable myTable = new DataTable();
            grdHistory.DataSource = myTable;
        }
        #endregion

        #region Details_Clear
        private void Details_Clear()
        {
            foreach (DataRow mDataRow in pDtDangerIndicators.Rows)
            {
                mDataRow.BeginEdit();
                mDataRow["selected"] = false;
                mDataRow["fieldvalue"] = 1;
                mDataRow.EndEdit();
            }
            chkFirstVisit.Checked = false;
            chkTTCard.Checked = false;
            chkDiscount.Checked = false;
            txtPregAge.Value = 0;
            txtNoOfPreg.Value = 0;
            txtHeight.Value = 0;
            txtLastBornYear.Text = "";
            cboLastBornStatus.SelectedIndex = -1;
            cboSyphilis.SelectedIndex = -1;
            cboPmtct.SelectedIndex = -1;
            cboReferedTo.Text = "";
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
                        string mFullName = mPatient.firstname;
                        if (mPatient.othernames.Trim() != "")
                        {
                            mFullName = mFullName + " " + mPatient.othernames;
                        }
                        mFullName = mFullName + " " + mPatient.surname;

                        txtClientId.Text = mPatient.code;
                        txtName.Text = mFullName;

                        txtRegNo.Text = mPatient.RegNo;
                        txtResidence.Text = mPatient.Residence;
                        txtGravida.Text = mPatient.Gravida;
                        txtPara.Text = mPatient.Para;
                        txtLMP.Text = mPatient.LMP;
                        txtEDD.Text = mPatient.EDD;

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

           
        //    #region danger indicators

            //DataTable mDtDangerIndicators = pMdtRCHAnteNatalCare.View_Indicators(
            //                        "clientcode='" + txtClientId.Text.Trim() + "' and booking='" + mSelectedRow["booking"].ToString().Trim() + "'",
            //                        "");
        //    DataView mDvDangerIndicators = new DataView();
        //    mDvDangerIndicators.Table = mDtDangerIndicators;
        //    mDvDangerIndicators.Sort = "methodcode";

        //    foreach (DataRow mDataRow in pDtDangerIndicators.Rows)
        //    {
        //        double mFieldValue = 0;

        //        int mRowIndex = mDvDangerIndicators.Find(mDataRow["fieldname"].ToString().Trim());

        //        if (mRowIndex >= 0)
        //        {
        //            mFieldValue = Convert.ToDouble(mDvDangerIndicators[mRowIndex]["quantity"]);
        //        }

        //        if (mFieldValue > 0)
        //        {
        //            mDataRow.BeginEdit();
        //            mDataRow["selected"] = true;
        //            mDataRow["fieldvalue"] = mFieldValue;
        //            mDataRow.EndEdit();
        //        }
        //        else
        //        {
        //            mDataRow.BeginEdit();
        //            mDataRow["selected"] = false;
        //            mDataRow["fieldvalue"] = 0;
        //            mDataRow.EndEdit();
        //        }
        //    }

        //    #endregion

        //    if (mSelectedRow["registrystatus"].ToString().Trim().ToLower() == "new")
        //    {
        //        chkFirstVisit.Checked = true;
        //    }
        //    else
        //    {
        //        chkFirstVisit.Checked = false;
        //    }

        //    //chkTTCard.Checked = Convert.ToBoolean(mSelectedRow["cardpresented"]);
        //    //chkDiscount.Checked = Convert.ToBoolean(mSelectedRow["discount"]);
        //    //txtPregAge.Value = Convert.ToInt32(mSelectedRow["pregage"]);
        //    //txtNoOfPreg.Value = Convert.ToInt32(mSelectedRow["noofpreg"]);
        //    //txtHeight.Value = Convert.ToInt32(mSelectedRow["height"]);
        //    //txtLastBornYear.Text = mSelectedRow["lastbirthyear"].ToString();
        //    //cboLastBornStatus.SelectedIndex = Convert.ToInt16(mSelectedRow["lastborndeath"]);
        //    //cboSyphilis.SelectedIndex = Convert.ToInt16(mSelectedRow["syphilistest"]);
        //    //cboPmtct.SelectedIndex = Convert.ToInt16(mSelectedRow["pmtcttest"]);
        //    //cboReferedTo.Text = mSelectedRow["referedto"].ToString().Trim();
        //    //txtDate.EditValue = Convert.ToDateTime(mSelectedRow["bookdate"]);
        //    //pCurrentBooking = mSelectedRow["booking"].ToString().Trim();
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

        #region viewHistory_RowClick
        private void viewHistory_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
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

        #region cmdAdd_Click
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdAdd_Attendance";
             
            try
            {
                AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();

                DataRow mSelectedDataRow = viewHistory.GetDataRow(viewHistory.FocusedRowHandle);

                frmRCHAntenatalVisitDetail mRCHAntenatalVisitDetail = new frmRCHAntenatalVisitDetail(txtClientId.Text, false, mSelectedDataRow, "");

                mRCHAntenatalVisitDetail.Show();

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



                DataTable mDtDangerIndicators = new DataTable("methods");
                mDtDangerIndicators.Columns.Add("fieldname", typeof(System.String));
                mDtDangerIndicators.Columns.Add("fieldvalue", typeof(System.Double));

                foreach (DataRow mDataRow in pDtDangerIndicators.Rows)
                {
                    double mFieldValue = 0;

                    if (Convert.ToBoolean(mDataRow["selected"]) == true)
                    {
                        mFieldValue = 1;
                    }

                    DataRow mNewRow = mDtDangerIndicators.NewRow();
                    mNewRow["fieldname"] = mDataRow["fieldname"];
                    mNewRow["fieldvalue"] = mFieldValue;
                    mDtDangerIndicators.Rows.Add(mNewRow);
                    mDtDangerIndicators.AcceptChanges();
                }

                int mSyphilisTest = 0;
                if (cboSyphilis.SelectedIndex == -1)
                {
                    mSyphilisTest = 0;
                }
                else
                {
                    mSyphilisTest = cboSyphilis.SelectedIndex;
                }

                int mPMTCT = 0;
                if (cboPmtct.SelectedIndex == -1)
                {
                    mPMTCT = 0;
                }
                else
                {
                    mPMTCT = cboPmtct.SelectedIndex;
                }

                int mLastBornYear = 0;
                if (Program.IsNumeric(txtLastBornYear.Text) == true)
                {
                    mLastBornYear = Convert.ToInt16(txtLastBornYear.Text);
                }

                int mLastBornDead = 0;
                if (cboLastBornStatus.Text.Trim().ToLower() == "dead")
                {
                    mLastBornDead = 1;
                }

                //edit


              
                try
                {
                   // AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
                    mSelectedRow = viewHistory.GetDataRow(viewHistory.FocusedRowHandle);

                    frmRCHAntenatalVisitDetail mRCHAntenatalVisitDetail = new frmRCHAntenatalVisitDetail(txtClientId.Text, true, mSelectedRow, mSelectedRow["autocode"].ToString());

                    mRCHAntenatalVisitDetail.Show();

                }
                catch (Exception ex)
                {
                    Program.Display_Error(pClassName, mFunctionName, ex.Message);
                    return;
                }

                //mResult = pMdtRCHAnteNatalCare.Edit(mTransDate, mSelectedRow["booking"].ToString(), txtClientId.Text, 
                //    Convert.ToInt16(chkTTCard.Checked), Convert.ToInt16(txtPregAge.Value), Convert.ToInt16(txtNoOfPreg.Value), 
                //    Convert.ToDouble(txtHeight.Value), Convert.ToInt16(chkDiscount.Checked), mSyphilisTest, 
                //    mLastBornYear, mLastBornDead, cboReferedTo.Text, mPMTCT, mDtDangerIndicators, 
                //    chkFirstVisit.Checked, Program.gCurrentUser.Code);

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
                pCurrentBooking = mResult.GeneratedCode;
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

        #region frmRCHAntenatalCare_KeyDown
        void frmRCHAntenatalCare_KeyDown(object sender, KeyEventArgs e)
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
                //case Program.KeyCode_ItemRemove:
                //    {
                //        this.cmdSavePatient_Click(cmdSavePatient, e);
                //    }
                //    break;
                case Program.KeyCode_ItemUpdate:
                    {
                        this.cmdUpdate_Click(cmdUpdate, e);
                    }
                    break;
                case Program.KeyCode_RchVaccinations:
                    {
                        this.cmdVaccinations_Click(cmdVaccinations, e);
                    }
                    break;
                case Program.KeyCode_RchClear:
                    {
                        //this.cmdClear_Click(cmdStatus, e);
                    }
                    break;
            }
        }
        #endregion

        #region cmdVaccinations_Click
        private void cmdVaccinations_Click(object sender, EventArgs e)
        {
            if (txtClientId.Text.Trim() == "")
            {
                return;
            }

            if (pCurrentBooking.Trim() == "")
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            frmRCHVaccinations mRCHVaccinations = new frmRCHVaccinations((int)AfyaPro_Types.clsEnums.RCHServices.antenatalcare, pCurrentBooking);
            mRCHVaccinations.gCalledFromClientsRegister = true;
            mRCHVaccinations.gClientCode = txtClientId.Text.Trim();
            mRCHVaccinations.ShowDialog();

            this.Cursor = Cursors.Default;
        }
        #endregion
        
        #region Save_Patient

        private void cmdSavePatient_Click_1(object sender, EventArgs e)
        {
            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();

            string mFunctionName = "cmdSavePatient_Click";

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

           

            if (txtRegNo.Text.Trim() == "")
            {
                MessageBox.Show("Please enter ANC registration number", "ANC Reg. No.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRegNo.Focus();
                return;
            }

            #endregion

            try
            {
                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

                //if (viewHistory.FocusedRowHandle <= -1)
                //{
                //    return;
                //}

                //DataRow mSelectedRow = viewHistory.GetDataRow(viewHistory.FocusedRowHandle);

                //DialogResult mResp = Program.Confirm_Deletion("'"
                //    + Convert.ToDateTime(mSelectedRow["bookdate"]).Date.ToString("d") + "'");

                //if (mResp != DialogResult.Yes)
                //{
                //    return;
                //}

                //delete
                mResult = pMdtRCHAnteNatalCare.Add_Patient(mTransDate, txtClientId.Text, Program.gCurrentUser.Code, txtRegNo.Text, txtResidence.Text, txtGravida.Text, txtPara.Text, txtLMP.Text, txtEDD.Text);

               
                if (mResult.Exe_Result == 0)
                {
                    MessageBox.Show("The ANC registration number is in use", "ANC Reg. No.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }
                if (mResult.Exe_Result == -1)
                {
                    Program.Display_Server_Error(mResult.Exe_Message);
                    return;
                }

                //refresh
                //this.Fill_History();
                this.Data_Clear();
                pCurrentBooking = "";

                MessageBox.Show("ANC Patient details successfully saved", "ANC Patient", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }

        #endregion

        #region Patient_Status
        private void cmdClear_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmd_Patient_Status";

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

            try
            {
                AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();

                frmRCHAntenatalPatientStatus mRCHAntenatalPatientStatus = new frmRCHAntenatalPatientStatus(pDtHistory);
                mRCHAntenatalPatientStatus.Text = "Status for [::" + txtClientId.Text + "::] " + txtName.Text;
               mRCHAntenatalPatientStatus.Show();

            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion


    }
}