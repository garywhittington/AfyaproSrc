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
using System.Xml;
using System.Runtime.InteropServices;

namespace AfyaPro_NextGen
{
    public partial class frmRCHMaternityDelivery : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsRCHMaternity pMdtRCHMaternity;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private ComboBoxItemCollection pStillBirths;
        private ComboBoxItemCollection pChildConditions;
        private DataTable pDtStillBirths = new DataTable("stillbirths");
        private DataTable pDtChildConditions = new DataTable("childconditions");
        private DataTable pDtChildren = new DataTable("children");
        private string pClientCode = "";
        private string pBooking = "";

        #endregion

        #region frmRCHMaternityDelivery
        public frmRCHMaternityDelivery(string mMotherCode, string mBooking, string mName, string mAgeYears, string mAgeMonths, string mAdmissionNo, DateTime mAdmissionDate)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmRCHMaternityDelivery";

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                this.pClientCode = mMotherCode;
                this.pBooking = mBooking;

                txtID.Text = mMotherCode.ToString();
                txtMotherName.Text = mName.ToString();
                txtAge.Text = mAgeYears.ToString();
                txtAdmissionDate.EditValue = mAdmissionDate.Date;
                txtAdmissionNo.Text = mAdmissionNo.ToString();
                txtAgeMonths.Text = mAgeMonths.ToString();

                
                //this.pDtChildren = mDtChildren;

                pMdtRCHMaternity = (AfyaPro_MT.clsRCHMaternity)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRCHMaternity),
                    Program.gMiddleTier + "clsRCHMaternity");

                //columns for still births
                //pDtStillBirths.Columns.Add("description", typeof(System.String));
                //pDtStillBirths.Columns.Add("fieldname", typeof(System.String));
                //pDtStillBirths.Columns.Add("fieldindex", typeof(System.Int32));
                //pStillBirths = cboDeliveryPlace.Properties.Items;

                //columns for child conditions
                //pDtChildConditions.Columns.Add("description", typeof(System.String));
                //pDtChildConditions.Columns.Add("fieldname", typeof(System.String));
                //pDtChildConditions.Columns.Add("fieldindex", typeof(System.Int32));
                //pChildConditions = cboComplications.Properties.Items;


                DataTable mTable = new DataTable();

                mTable = pMdtRCHMaternity.View_Children("", "");

               
                //mTable.Columns.Add("Mother ID");
                //mTable.Columns.Add("Delivery Date");
                //mTable.Columns.Add("Delivery place");
                //mTable.Columns.Add("Delivery Mode");
                //mTable.Columns.Add("Gender");
                //mTable.Columns.Add("Weight");
                //mTable.Columns.Add("Complications");
                //mTable.Columns.Add("Baby status");
                //mTable.AcceptChanges();


                grdChildren.DataSource = mTable;

                //hide unneccessary columns
                foreach (DevExpress.XtraGrid.Columns.GridColumn mGridColumn in viewChildren.Columns)
                {
                    if (mGridColumn.FieldName.ToLower() == "autocode"
                        || mGridColumn.FieldName.ToLower() == "booking"
                        || mGridColumn.FieldName.ToLower() == "bookdate"
                        || mGridColumn.FieldName.ToLower() == "apgarscore"

                        || mGridColumn.FieldName.ToLower() == "freshbirth"
                        || mGridColumn.FieldName.ToLower() == "maceratedbirth"
                        || mGridColumn.FieldName.ToLower() == "childproblems"

                        || mGridColumn.FieldName.ToLower() == "deathbefore24"
                        || mGridColumn.FieldName.ToLower() == "deathafter24"
                        || mGridColumn.FieldName.ToLower() == "userid"

                        || mGridColumn.FieldName.ToLower() == "yearpart"
                        || mGridColumn.FieldName.ToLower() == "monthpart"
                        || mGridColumn.FieldName.ToLower() == "sysdate"

                        || mGridColumn.FieldName.ToLower() == "dischargedalive"
                        || mGridColumn.FieldName.ToLower() == "neonataldeath"
                        || mGridColumn.FieldName.ToLower() == "breastfeedinginitiated"

                        || mGridColumn.FieldName.ToLower() == "comments"
                          
                        || mGridColumn.FieldName.ToLower() == "tetracycline")
                    {
                        mGridColumn.Visible = false;
                    }
                    else
                    {
                        if (mGridColumn.FieldName.ToLower() == "clientcode")
                        {
                            mGridColumn.Caption = "Client Code";
                        }
                        if (mGridColumn.FieldName.ToLower() == "deliverydate")
                        {
                            mGridColumn.Caption = "Delivery Date";
                        }

                        if (mGridColumn.FieldName.ToLower() == "deliverymode")
                        {
                            mGridColumn.Caption = "Delivery Mode";
                        }

                        if (mGridColumn.FieldName.ToLower() == "deliveryplace")
                        {
                            mGridColumn.Caption = "Delivery Place";
                        }

                        if (mGridColumn.FieldName.ToLower() == "live")
                        {
                            mGridColumn.Caption = "Alive";
                        }

                        if (mGridColumn.FieldName.ToLower() == "childproblems")
                        {
                            mGridColumn.Caption = "Complications";
                        }

                        if (mGridColumn.FieldName.ToLower() == "weight")
                        {
                            mGridColumn.Caption = "Weight";
                        }

                        if (mGridColumn.FieldName.ToLower() == "gender")
                        {
                            mGridColumn.Caption = "Gender";
                        }

                        if (mGridColumn.FieldName.ToLower() == "apgarscore")
                        {
                            mGridColumn.Caption = "Apgar";
                        }
                         
                    }
                }

                //MessageBox.Show("Hallo");
                //fill lookup data
               // this.Fill_StillBirths();
               // this.Fill_ChildConditions();
               
                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.rchpostnatal_customizelayout.ToString());
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

            mObjectsList.Add(txbDeliveryDate);
            mObjectsList.Add(radGender.Properties.Items[0]);
            mObjectsList.Add(radGender.Properties.Items[1]);
            mObjectsList.Add(txbWeight);
            mObjectsList.Add(txbApgarScore);
            mObjectsList.Add(txbStillBirths);
            mObjectsList.Add(txbChildConditions);
            
            mObjectsList.Add(cmdAdd);
            mObjectsList.Add(cmdDelete);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }

        #endregion

        #region frmRCHPostnatalChildren_Load
        private void frmRCHPostnatalChildren_Load(object sender, EventArgs e)
        {
            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            this.Load_Controls();

            txtDeliveryDate.EditValue = Program.gMdiForm.txtDate.EditValue;

            this.pFormWidth = this.Width;
            this.pFormHeight = this.Height;

            Program.Center_Screen(this);

            this.Append_ShortcutKeys();
        }
        #endregion

        #region frmRCHPostnatalChildren_FormClosing
        private void frmRCHPostnatalChildren_FormClosing(object sender, FormClosingEventArgs e)
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
            cmdAdd.Text = cmdAdd.Text + " (" + Program.KeyCode_ItemAdd.ToString() + ")";
            cmdDelete.Text = cmdDelete.Text + " (" + Program.KeyCode_ItemRemove.ToString() + ")";
            cmdUpdate.Text = cmdUpdate.Text + " (" + Program.KeyCode_ItemUpdate.ToString() + ")";
            cmdClear.Text = cmdClear.Text + " (" + Program.KeyCode_RchClear.ToString() + ")";
        }
        #endregion

        #region viewChildren_RowClick
        private void viewChildren_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

        }
        #endregion

        #region viewChildren_GotFocus
        private void viewChildren_GotFocus(object sender, EventArgs e)
        {

        }
        #endregion

        #region Fill_StillBirths
        private void Fill_StillBirths()
        {
            //string mFunctionName = "Fill_StillBirths";

            //try
            //{
            //    pStillBirths.Clear();
            //    pDtStillBirths.Rows.Clear();

            //    DataTable mDtStillBirths = pMdtRCHPostNatalCare.Get_StillBirths(Program.gLanguageName, "grdRCHStillBirths");

            //    int mFieldIndex = 0;
            //    foreach (DataRow mDataRow in mDtStillBirths.Rows)
            //    {
            //        pStillBirths.Add(mDataRow["description"].ToString().Trim());

            //        DataRow mNewRow = pDtStillBirths.NewRow();
            //        mNewRow["description"] = mDataRow["description"];
            //        mNewRow["fieldname"] = mDataRow["fieldname"];
            //        mNewRow["fieldindex"] = mFieldIndex;
            //        pDtStillBirths.Rows.Add(mNewRow);
            //        pDtStillBirths.AcceptChanges();

            //        mFieldIndex++;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Program.Display_Error(pClassName, mFunctionName, ex.Message);
            //    return;
            //}
        }
        #endregion

        #region Fill_ChildConditions
        private void Fill_ChildConditions()
        {
            //string mFunctionName = "Fill_ChildConditions";

            //try
            //{
            //    pChildConditions.Clear();
            //    pDtChildConditions.Rows.Clear();

            //    DataTable mDtChildConditions = pMdtRCHPostNatalCare.Get_ChildConditions(Program.gLanguageName, "grdRCHChildConditions");

            //    int mFieldIndex = 0;
            //    foreach (DataRow mDataRow in mDtChildConditions.Rows)
            //    {
            //        pChildConditions.Add(mDataRow["description"].ToString().Trim());

            //        DataRow mNewRow = pDtChildConditions.NewRow();
            //        mNewRow["description"] = mDataRow["description"];
            //        mNewRow["fieldname"] = mDataRow["fieldname"];
            //        mNewRow["fieldindex"] = mFieldIndex;
            //        pDtChildConditions.Rows.Add(mNewRow);
            //        pDtChildConditions.AcceptChanges();

            //        mFieldIndex++;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Program.Display_Error(pClassName, mFunctionName, ex.Message);
            //    return;
            //}
        }
        #endregion

        #region Details_Clear
        private void Details_Clear()
        {
            txtDeliveryDate.EditValue = Program.gMdiForm.txtDate.EditValue;
            txtWeight.Text = "";
            txtApgarScore.Text = "";
            cboDeliveryPlace.Text = "";
            cboComplications.Text = "";

            cboDeliveryMode.Text = "";
            cboDeliveryPlace.Text = "";
            cboComplications.Text = "";
            txtGravida.Text = "";
            txtPara.Text = "";
            txtAdmissionDate.EditValue =null;
            cboMotherStatus.Text = "";


            txtGestationWeeks.Text = "";
            cboPreviousHIVTest.Text = "";
            cboNewHIVTest.Text = "";
            cboARTMother.Text = "";
            txtARTRegNo.Text = "";
            cboObstetricComplications.Text = "";
            cboEmegencyObstetricCare.Text = "";
            cboVitaminAGiven.Text = "";
            cboDischargedAlive.Text = "";

            txtAdmissionNo.Text = "";
            txtMotherName.Text = "";
            txtID.Text = "";
            txtAge.Text = "";
            txtAgeMonths.Text = "";
            txtChildrenNo.Text = "";


           
            radGender.SelectedIndex = -1;
        }
        #endregion

        #region Display_RowDetails
        private void Display_RowDetails(DataRow mSelectedRow)
        {
            txtDeliveryDate.EditValue = Convert.ToDateTime(mSelectedRow["deliverydate"]);
            txtWeight.Text = mSelectedRow["weight"].ToString().Trim();
            txtApgarScore.Text = mSelectedRow["apgarscore"].ToString().Trim();
            //txtChildProblems.Text = mSelectedRow["childproblems"].ToString().Trim();

            //gender
            if (mSelectedRow["gender"].ToString().Trim().ToLower() == "m")
            {
                radGender.SelectedIndex = 1;
            }
            else
            {
                radGender.SelectedIndex = 0;
            }

            //still birth
            cboDeliveryPlace.SelectedIndex = -1;
            if (mSelectedRow["stillbirth"].ToString().Trim().ToLower() == AfyaPro_Types.clsEnums.RCHStillBirths.freshbirth.ToString().ToLower())
            {
                cboDeliveryPlace.SelectedIndex = Convert.ToInt16(AfyaPro_Types.clsEnums.RCHStillBirths.freshbirth);
            }
            if (mSelectedRow["stillbirth"].ToString().Trim().ToLower() == AfyaPro_Types.clsEnums.RCHStillBirths.maceratedbirth.ToString().ToLower())
            {
                cboDeliveryPlace.SelectedIndex = Convert.ToInt16(AfyaPro_Types.clsEnums.RCHStillBirths.maceratedbirth);
            }

            //child condition
            cboComplications.SelectedIndex = -1;
            if (mSelectedRow["childcondition"].ToString().Trim().ToLower() == AfyaPro_Types.clsEnums.RCHChildConditions.live.ToString().ToLower())
            {
                cboComplications.SelectedIndex = Convert.ToInt16(AfyaPro_Types.clsEnums.RCHChildConditions.live);
            }
            if (mSelectedRow["childcondition"].ToString().Trim().ToLower() == AfyaPro_Types.clsEnums.RCHChildConditions.deathbefore24.ToString().ToLower())
            {
                cboComplications.SelectedIndex = Convert.ToInt16(AfyaPro_Types.clsEnums.RCHChildConditions.deathbefore24);
            }
            if (mSelectedRow["childcondition"].ToString().Trim().ToLower() == AfyaPro_Types.clsEnums.RCHChildConditions.deathafter24.ToString().ToLower())
            {
                cboComplications.SelectedIndex = Convert.ToInt16(AfyaPro_Types.clsEnums.RCHChildConditions.deathafter24);
            }
        }
        #endregion

        #region viewChildren_FocusedRowChanged
        private void viewChildren_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }
        #endregion

        #region cmdAdd_Click
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();

            string mFunctionName = "cmdAdd_Click";

            DateTime mTransDate = Convert.ToDateTime(Program.gServerDate);

            #region validation

            if (txtGestationWeeks.Text  == "")
            {
                Program.Display_Error("Invalid gestation weeks");
                txtGestationWeeks.Focus();
                return;
            }

            if (cboPreviousHIVTest.SelectedIndex == -1)
            {
                Program.Display_Error("Invalid previous HIV Test results");
                cboPreviousHIVTest.Focus();
                return;
            }

            if (cboNewHIVTest.SelectedIndex == -1)
            {
                Program.Display_Error("Invalid New HIV Test results");
                cboNewHIVTest.Focus();
                return;
            }

            if (cboARTMother.SelectedIndex == -1)
            {
                Program.Display_Error("Invalid Mother's ART Status");
                cboARTMother.Focus();
                return;
            }

            if (cboObstetricComplications.SelectedIndex == -1)
            {
                Program.Display_Error("Invalid selection on obstetric complications");
                cboObstetricComplications.Focus();
                return;
            }

            if (cboObstetricComplications.Text != "None" & cboEmegencyObstetricCare.SelectedIndex == -1 )
            {
                Program.Display_Error("Invalid selection on emmergency obstetric care");
                cboEmegencyObstetricCare.Focus();
                return;
            }

            if (cboVitaminAGiven.SelectedIndex == -1)
            {
                Program.Display_Error("Invalid selection on Vitamin A");
                cboVitaminAGiven.Focus();
                return;
            }

            if (cboMotherStatus.SelectedIndex == -1)
            {
                Program.Display_Error("Invalid selection on Mother status");
                cboMotherStatus.Focus();
                return;
            }
            
            if (Program.IsDate(txtDeliveryDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                txtDeliveryDate.Focus();
                return;
            }

            if (radGender.SelectedIndex == -1)
            {
                Program.Display_Error("Invalid gender");
                radGender.Focus();
                return;
            }

            if (txtChildrenNo.Text  == "")
            {
                Program.Display_Error("Invalid number of children born");
                txtChildrenNo.Focus();
                return;
            }

            if (Program.IsMoney(txtWeight.Text) == false)
            {
                Program.Display_Error("Invalid weight");
                txtWeight.Focus();
                return;
            }

            if (Program.IsMoney(txtApgarScore.Text) == false)
            {
                Program.Display_Error("Invalid apgar score");
                txtApgarScore.Focus();
                return;
            }
            
            if (cboDeliveryPlace.SelectedIndex == -1)
            {
                Program.Display_Error("Invalid Delivery Place");
                cboDeliveryPlace.Focus();
                return;
            }

            if (cboDeliveryMode.SelectedIndex == -1)
            {
                Program.Display_Error("Invalid Delivery Mode");
                cboDeliveryMode.Focus();
                return;
            }

            if (cboComplications.SelectedIndex == -1)
            {
                Program.Display_Error("Invalid selection on newborn complications");
                cboComplications.Focus();
                return;
            }

            if ((cboDischargedAlive.SelectedIndex == -1) && (cboStillBirth.SelectedIndex == -1) && (chkNeoNatal.Checked == false))
            {
                Program.Display_Error("Invalid selection on newborn survival / PMTCT Management");
                cboComplications.Focus();
                return;
            }

            #endregion

            try
            {
                DateTime mDeliveryDate = Convert.ToDateTime(txtDeliveryDate.EditValue);

                #region Disabled_Code

                ////still birth
                //string mStillBirth = "";
                //DataView mDvStillBirths = new DataView();
                //mDvStillBirths.Table = pDtStillBirths;
                //mDvStillBirths.Sort = "fieldindex";

                //int mRowIndex = mDvStillBirths.Find(cboStillBirths.SelectedIndex);
                //if (mRowIndex >= 0)
                //{
                //    mStillBirth = mDvStillBirths[mRowIndex]["fieldname"].ToString().Trim();
                //}

                ////child condition
                //string mChildCondition = "";
                //DataView mDvChildConditions = new DataView();
                //mDvChildConditions.Table = pDtChildConditions;
                //mDvChildConditions.Sort = "fieldindex";

                //mRowIndex = mDvChildConditions.Find(cboChildConditions.SelectedIndex);
                //if (mRowIndex >= 0)
                //{
                //    mChildCondition = mDvChildConditions[mRowIndex]["fieldname"].ToString().Trim();
                //}

                double mWeight = 0;
                if (Program.IsMoney(txtWeight.Text) == true)
                {
                    mWeight = Convert.ToDouble(txtWeight.Text);
                }

                double mApgarScore = 0;
                if (Program.IsMoney(txtApgarScore.Text) == true)
                {
                    mApgarScore = Convert.ToDouble(txtApgarScore.Text);
                }

                string mGender = "F";
                if (radGender.SelectedIndex == 1)
                {
                    mGender = "M";
                }

                string mDeliveryMode= cboDeliveryMode.Text ;
                string mDeliveryPlace = cboDeliveryPlace.Text;
                string mComplications = cboComplications.Text ;
                string  mGravida = txtGravida.Text;
                string  mPara =txtPara .Text ;
                DateTime mAdmissionDate = Convert.ToDateTime(txtAdmissionDate.EditValue);
                string mMotherStatus = cboMotherStatus.Text;
                string mUserId = "";

                string mGestationWeeks = txtGestationWeeks.Text ;
                    string mHIVTestPrevious =cboPreviousHIVTest.Text ;
                        string mHIVTestNew =cboNewHIVTest.Text;
                            string  mARTMother =cboARTMother.Text;
                                string mARTRegNo =txtARTRegNo.Text;
                                    string mObstetricComplications = cboObstetricComplications.Text;
                                        string mEmegencyObstetricCare = cboEmegencyObstetricCare .Text;
                                        int mVitaminAGiven = 0;
                                        string mDischargedAlive = cboDischargedAlive.Text;
                
               
                int mNeonatalDeath;
                int mBreastFeeding;
                int mTetracycline;

                if (cboVitaminAGiven.Text == "Yes")
                {

                    mVitaminAGiven = 1;
                }
                else
                {
                    mVitaminAGiven = 0;
                }

                                        if (chkNeoNatal.Checked == true)
                                        {
                                            mNeonatalDeath = 1;
                                        }
                                        else
                                        {
                                            mNeonatalDeath = 0;
                                        }

                                         
                                        if (chkBreastFeeding.Checked == true)
                                        {
                                            mBreastFeeding = 1;
                                        }
                                        else
                                        {
                                            mBreastFeeding = 0;
                                        }

                                        if (chkTetracycline.Checked == true)
                                        {
                                            mTetracycline = 1;
                                        }
                                        else
                                        {
                                            mTetracycline = 0;
                                        }

                                        int mNumberBorn = Convert.ToInt16(txtChildrenNo.Text);

                int mFreshBirth =0;
                int mMaceratedBirth =0;
                if (cboStillBirth.Text == "Fresh")
                {
                    mFreshBirth = 1;
                    mMaceratedBirth = 0;
                }

                if (cboStillBirth.Text == "Macerated")
                {
                    mFreshBirth = 0;
                    mMaceratedBirth = 1;
                }

                
                //DataRow mNewRow = pDtChildren.NewRow();
                //mNewRow["deliverydate"] = mDeliveryDate;
                //mNewRow["gender"] = mGender;
                //mNewRow["weight"] = mWeight;
                //mNewRow["apgarscore"] = mApgarScore;
                ////mNewRow["childproblems"] = txtChildProblems.Text.Trim();
                //mNewRow["stillbirth"] = mStillBirth;
                //mNewRow["childcondition"] = mChildCondition;
                //pDtChildren.Rows.Add(mNewRow);
                //pDtChildren.AcceptChanges();

                int mBabylive;
                //refresh
                if (chkNeoNatal.Checked == false)
                {
                    mBabylive = 1;
                }
                else { mBabylive = 0; }
                #endregion

                mResult = pMdtRCHMaternity.Add_Maternity_Delivery(mTransDate, txtID.Text, mDeliveryMode, mDeliveryPlace, mComplications, mGravida, mPara, mAdmissionDate, mMotherStatus, mUserId, mGestationWeeks, mHIVTestPrevious, mHIVTestNew, mARTMother, mARTRegNo, mObstetricComplications, mEmegencyObstetricCare, mVitaminAGiven, mBreastFeeding, mTetracycline, Convert.ToDateTime(txtDeliveryDate.EditValue), mNumberBorn, mGender, mWeight, mApgarScore, mFreshBirth, mMaceratedBirth, mBabylive, mDischargedAlive, txtComment.Text);
                

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
                //if (mResult.Exe_Result == 5)
                //{
                //    System.Windows.Forms.MessageBox.Show("This patient was already admitted in maternity", "Admission", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                //this.Admit();

                //if (gCalledFromClientsRegister == true)
                //{
                //    gDataSaved = true;
                //    this.Close();
                //}
                //else
                //{
                //    //refresh
                //    txtClientId.Text = "";
                //    this.Fill_History();
                //    this.Data_Clear();

                //}


                this.Details_Clear();
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
            string mFunctionName = "cmdUpdate_Click";

            if (viewChildren.FocusedRowHandle <= -1)
            {
                return;
            }

            DataRow mSelectedRow = viewChildren.GetDataRow(viewChildren.FocusedRowHandle);

            #region validation

            if (Program.IsDate(txtDeliveryDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                txtDeliveryDate.Focus();
                return;
            }

            if (radGender.SelectedIndex == -1)
            {
                Program.Display_Error("Invalid gender");
                radGender.Focus();
                return;
            }

            if (Program.IsMoney(txtWeight.Text) == false)
            {
                Program.Display_Error("Invalid weight");
                txtWeight.Focus();
                return;
            }

            if (Program.IsMoney(txtApgarScore.Text) == false)
            {
                Program.Display_Error("Invalid apgar score");
                txtApgarScore.Focus();
                return;
            }

            if (cboComplications.SelectedIndex == -1)
            {
                Program.Display_Error("Invalid discharge condition");
                cboComplications.Focus();
                return;
            }

            #endregion

            try
            {
                DateTime mDeliveryDate = Convert.ToDateTime(txtDeliveryDate.EditValue);

                //still birth
                string mStillBirth = "";
                DataView mDvStillBirths = new DataView();
                mDvStillBirths.Table = pDtStillBirths;
                mDvStillBirths.Sort = "fieldindex";

                int mRowIndex = mDvStillBirths.Find(cboDeliveryPlace.SelectedIndex);
                if (mRowIndex >= 0)
                {
                    mStillBirth = mDvStillBirths[mRowIndex]["fieldname"].ToString().Trim();
                }

                //child condition
                string mChildCondition = "";
                DataView mDvChildConditions = new DataView();
                mDvChildConditions.Table = pDtChildConditions;
                mDvChildConditions.Sort = "fieldindex";

                mRowIndex = mDvChildConditions.Find(cboComplications.SelectedIndex);
                if (mRowIndex >= 0)
                {
                    mChildCondition = mDvChildConditions[mRowIndex]["fieldname"].ToString().Trim();
                }

                double mWeight = 0;
                if (Program.IsMoney(txtWeight.Text) == true)
                {
                    mWeight = Convert.ToDouble(txtWeight.Text);
                }

                double mApgarScore = 0;
                if (Program.IsMoney(txtApgarScore.Text) == true)
                {
                    mApgarScore = Convert.ToDouble(txtApgarScore.Text);
                }

                string mGender = "F";
                if (radGender.SelectedIndex == 1)
                {
                    mGender = "M";
                }

                mSelectedRow.BeginEdit();

                mSelectedRow["deliverydate"] = mDeliveryDate;
                mSelectedRow["gender"] = mGender;
                mSelectedRow["weight"] = mWeight;
                mSelectedRow["apgarscore"] = mApgarScore;
               // mSelectedRow["childproblems"] = txtChildProblems.Text.Trim();
                mSelectedRow["stillbirth"] = mStillBirth;
                mSelectedRow["childcondition"] = mChildCondition;

                mSelectedRow.EndEdit();

                //refresh
                this.Details_Clear();
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
            if (viewChildren.FocusedRowHandle <= -1)
            {
                return;
            }

            DataRow mSelectedRow = viewChildren.GetDataRow(viewChildren.FocusedRowHandle);

            DialogResult mResp = Program.Confirm_Deletion("'"
                + Convert.ToDateTime(mSelectedRow["deliverydate"]).Date.ToString("d") + "'   '"
                + mSelectedRow["weight"].ToString().Trim() + "'");

            if (mResp != DialogResult.Yes)
            {
                return;
            }

            viewChildren.DeleteSelectedRows();
            pDtChildren.AcceptChanges();
        }
        #endregion

        #region cmdClear_Click
        private void cmdClear_Click(object sender, EventArgs e)
        {
            this.Details_Clear();
            radGender.Focus();
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region frmRCHPostnatalChildren_KeyDown
        void frmRCHPostnatalChildren_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
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
                        this.cmdUpdate_Click(cmdUpdate, e);
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