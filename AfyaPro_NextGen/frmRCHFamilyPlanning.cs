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
    public partial class frmRCHFamilyPlanning : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsRCHFPlanMethods pMdtRCHFPlanMethods;
        private AfyaPro_MT.clsRCHClients pMdtRCHClients;
        private AfyaPro_MT.clsRCHFamilyPlanning pMdtRCHFamilyPlanning;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private AfyaPro_Types.clsRchClient pCurrentRchClient;

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private string pCurrRchClient = "";
        private string pPrevRchClient = "";
        private bool pSearchingRchClient = false;

        private DataTable pDtMethods = new DataTable("methods");
        private DataTable pDtHistory = new DataTable("history");
        private DataTable pDtExtraColumns = new DataTable("extracolumns");

        internal bool gCalledFromClientsRegister = false;
        internal string gClientCode = "";
        internal bool gDataSaved = false;

        #endregion

        #region frmRCHFamilyPlanning
        public frmRCHFamilyPlanning()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmRCHFamilyPlanning";

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                pMdtRCHFPlanMethods = (AfyaPro_MT.clsRCHFPlanMethods)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRCHFPlanMethods),
                    Program.gMiddleTier + "clsRCHFPlanMethods");

                pMdtRCHClients = (AfyaPro_MT.clsRCHClients)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRCHClients),
                    Program.gMiddleTier + "clsRCHClients");

                pMdtRCHFamilyPlanning = (AfyaPro_MT.clsRCHFamilyPlanning)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRCHFamilyPlanning),
                    Program.gMiddleTier + "clsRCHFamilyPlanning");

                //columns for methods used
                pDtMethods.Columns.Add("selected", typeof(System.Boolean));
                pDtMethods.Columns.Add("description", typeof(System.String));
                pDtMethods.Columns.Add("fieldvalue", typeof(System.Int16));
                pDtMethods.Columns.Add("fieldname", typeof(System.String));
                pDtMethods.Columns.Add("quantityentry", typeof(System.Int16));

                grdMethods.DataSource = pDtMethods;

                //columns for history
                pDtExtraColumns = pMdtRCHFamilyPlanning.View_Archive("1=2", "");

                foreach (DataColumn mDataColumn in pDtExtraColumns.Columns)
                {
                    pDtHistory.Columns.Add(mDataColumn.ColumnName, mDataColumn.DataType);
                }

                grdHistory.DataSource = pDtHistory;

                //hide unneccessary columns
                foreach (DevExpress.XtraGrid.Columns.GridColumn mGridColumn in viewHistory.Columns)
                {
                    if (mGridColumn.FieldName.ToLower() != "bookdate"
                        && mGridColumn.FieldName.ToLower() != "complains"
                        && mGridColumn.FieldName.ToLower() != "booking")
                    {
                        mGridColumn.Visible = false;
                    }
                }

                //fill methods
                this.Fill_Methods();

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

        #region frmRCHFamilyPlanning_Load
        private void frmRCHFamilyPlanning_Load(object sender, EventArgs e)
        {
            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            this.Load_Controls();

            txtDate.EditValue = Program.gMdiForm.txtDate.EditValue;
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

        #region frmRCHFamilyPlanning_Shown
        private void frmRCHFamilyPlanning_Shown(object sender, EventArgs e)
        {

        }
        #endregion

        #region frmRCHFamilyPlanning_FormClosing
        private void frmRCHFamilyPlanning_FormClosing(object sender, FormClosingEventArgs e)
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
            cmdDelete.Text = cmdDelete.Text + " (" + Program.KeyCode_ItemRemove.ToString() + ")";
            cmdUpdate.Text = cmdUpdate.Text + " (" + Program.KeyCode_ItemUpdate.ToString() + ")";
            cmdClear.Text = cmdClear.Text + " (" + Program.KeyCode_RchClear.ToString() + ")";
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

        #region Fill_Methods
        private void Fill_Methods()
        {
            string mFunctionName = "Fill_Methods";

            try
            {
                pDtMethods.Rows.Clear();

                DataTable mDtMethods = pMdtRCHFPlanMethods.View("", "", Program.gLanguageName, "");

                foreach (DataRow mDataRow in mDtMethods.Rows)
                {
                    DataRow mNewRow = pDtMethods.NewRow();
                    mNewRow["selected"] = false;
                    mNewRow["description"] = mDataRow["description"];
                    mNewRow["fieldvalue"] = 1;
                    mNewRow["fieldname"] = mDataRow["code"];
                    mNewRow["quantityentry"] = mDataRow["quantityentry"];
                    pDtMethods.Rows.Add(mNewRow);
                    pDtMethods.AcceptChanges();
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

                DataTable mDtHistory = pMdtRCHFamilyPlanning.View_Archive(
                    "clientcode='" + txtClientId.Text.Trim() + "'", "bookdate desc,autocode desc");

                Type mFields = typeof(AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods);

                DataView mDvMethods = new DataView();
                mDvMethods.Table = pDtMethods;
                mDvMethods.Sort = "fieldname";

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

            this.Details_Clear();

            pPrevRchClient = "";
            pCurrRchClient = pPrevRchClient;

            txtClientId.Focus();
        }
        #endregion

        #region Details_Clear
        private void Details_Clear()
        {
            foreach (DataRow mDataRow in pDtMethods.Rows)
            {
                mDataRow.BeginEdit();
                mDataRow["selected"] = false;
                mDataRow["fieldvalue"] = 1;
                mDataRow.EndEdit();
            }
            cboComplains.Text = "";
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
            #region methods used

            DataTable mDtMethodsUsed = pMdtRCHFamilyPlanning.View_MethodsUsed(
                                    "clientcode='" + txtClientId.Text.Trim() + "' and booking='" + mSelectedRow["booking"].ToString().Trim() + "'",
                                    "");
            DataView mDvMethodsUsed = new DataView();
            mDvMethodsUsed.Table = mDtMethodsUsed;
            mDvMethodsUsed.Sort = "methodcode";

            foreach (DataRow mDataRow in pDtMethods.Rows)
            {
                double mFieldValue = 0;

                int mRowIndex = mDvMethodsUsed.Find(mDataRow["fieldname"].ToString().Trim());

                if (mRowIndex >= 0)
                {
                    mFieldValue = Convert.ToDouble(mDvMethodsUsed[mRowIndex]["quantity"]);
                }

                if (mFieldValue > 0)
                {
                    mDataRow.BeginEdit();
                    mDataRow["selected"] = true;
                    mDataRow["fieldvalue"] = mFieldValue;
                    mDataRow.EndEdit();
                }
                else
                {
                    mDataRow.BeginEdit();
                    mDataRow["selected"] = false;
                    mDataRow["fieldvalue"] = 0;
                    mDataRow.EndEdit();
                }
            }

            #endregion

            cboComplains.Text = mSelectedRow["complains"].ToString().Trim();
            txtDate.EditValue = Convert.ToDateTime(mSelectedRow["bookdate"]);
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
                AfyaPro_Types.clsEnums.RCHServices.familyplanning.ToString(), txtClientId.Text);
            if (mValidClient == false)
            {
                return;
            }

            #endregion

            try
            {
                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

                DataTable mDtMethods = new DataTable("methods");
                mDtMethods.Columns.Add("fieldname", typeof(System.String));
                mDtMethods.Columns.Add("fieldvalue", typeof(System.Double));

                foreach (DataRow mDataRow in pDtMethods.Rows)
                {
                    double mFieldValue = 0;

                    if (Convert.ToBoolean(mDataRow["selected"]) == true)
                    {
                        mFieldValue = Convert.ToDouble(mDataRow["fieldvalue"]);
                    }

                    DataRow mNewRow = mDtMethods.NewRow();
                    mNewRow["fieldname"] = mDataRow["fieldname"];
                    mNewRow["fieldvalue"] = mFieldValue;
                    mDtMethods.Rows.Add(mNewRow);
                    mDtMethods.AcceptChanges();
                }

                //add
                mResult = pMdtRCHFamilyPlanning.Add(mTransDate, txtClientId.Text, mDtMethods,
                cboComplains.Text, Program.gCurrentUser.Code);

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

                DataTable mDtMethods = new DataTable("methods");
                mDtMethods.Columns.Add("fieldname", typeof(System.String));
                mDtMethods.Columns.Add("fieldvalue", typeof(System.Double));

                foreach (DataRow mDataRow in pDtMethods.Rows)
                {
                    double mFieldValue = 0;

                    if (Convert.ToBoolean(mDataRow["selected"]) == true)
                    {
                        mFieldValue = Convert.ToDouble(mDataRow["fieldvalue"]);
                    }

                    DataRow mNewRow = mDtMethods.NewRow();
                    mNewRow["fieldname"] = mDataRow["fieldname"];
                    mNewRow["fieldvalue"] = mFieldValue;
                    mDtMethods.Rows.Add(mNewRow);
                    mDtMethods.AcceptChanges();
                }

                //edit
                mResult = pMdtRCHFamilyPlanning.Edit(mTransDate, mSelectedRow["booking"].ToString(),
                txtClientId.Text, mDtMethods, cboComplains.Text, Program.gCurrentUser.Code);

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

                if (viewHistory.FocusedRowHandle <= -1)
                {
                    return;
                }

                DataRow mSelectedRow = viewHistory.GetDataRow(viewHistory.FocusedRowHandle);

                DialogResult mResp = Program.Confirm_Deletion("'"
                    + Convert.ToDateTime(mSelectedRow["bookdate"]).Date.ToString("d") + "'");

                if (mResp != DialogResult.Yes)
                {
                    return;
                }

                //delete
                mResult = pMdtRCHFamilyPlanning.Delete(mTransDate, mSelectedRow["booking"].ToString(),
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

            grdMethods.Focus();
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region frmRCHFamilyPlanning_KeyDown
        void frmRCHFamilyPlanning_KeyDown(object sender, KeyEventArgs e)
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