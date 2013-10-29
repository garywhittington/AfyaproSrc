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
    public partial class frmRCHPostnatalCare : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsRCHBirthMethods pMdtRCHBirthMethods;
        private AfyaPro_MT.clsRCHBirthComplications pMdtRCHBirthComplications;
        private AfyaPro_MT.clsRCHClients pMdtRCHClients;
        private AfyaPro_MT.clsRCHPostNatalCare pMdtRCHPostNatalCare;
        private AfyaPro_MT.clsMedicalStaffs pMdtMedicalStaffs;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private AfyaPro_Types.clsRchClient pCurrentRchClient;

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private string pCurrRchClient = "";
        private string pPrevRchClient = "";
        private bool pSearchingRchClient = false;

        private DataTable pDtBirthMethods = new DataTable("birthmethods");
        private DataTable pDtComplications = new DataTable("complications");
        private DataTable pDtHistory = new DataTable("history");
        private DataTable pDtExtraColumns = new DataTable("extracolumns");
        private DataTable pDtAttendants = new DataTable("attendants");
        private DataTable pDtChildren = new DataTable("children");

        internal bool gCalledFromClientsRegister = false;
        internal string gClientCode = "";
        internal bool gDataSaved = false;

        #endregion

        #region frmRCHPostnatalCare
        public frmRCHPostnatalCare()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmRCHPostnatalCare";

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                pMdtRCHBirthMethods = (AfyaPro_MT.clsRCHBirthMethods)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRCHBirthMethods),
                    Program.gMiddleTier + "clsRCHBirthMethods");

                pMdtRCHBirthComplications = (AfyaPro_MT.clsRCHBirthComplications)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRCHBirthComplications),
                    Program.gMiddleTier + "clsRCHBirthComplications");

                pMdtRCHClients = (AfyaPro_MT.clsRCHClients)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRCHClients),
                    Program.gMiddleTier + "clsRCHClients");

                pMdtMedicalStaffs = (AfyaPro_MT.clsMedicalStaffs)Activator.GetObject(
                    typeof(AfyaPro_MT.clsMedicalStaffs),
                    Program.gMiddleTier + "clsMedicalStaffs");

                pMdtRCHPostNatalCare = (AfyaPro_MT.clsRCHPostNatalCare)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRCHPostNatalCare),
                    Program.gMiddleTier + "clsRCHPostNatalCare");

                pDtAttendants.Columns.Add("code", typeof(System.String));
                pDtAttendants.Columns.Add("description", typeof(System.String));
                cboAttendant.Properties.DataSource = pDtAttendants;
                cboAttendant.Properties.DisplayMember = "description";
                cboAttendant.Properties.ValueMember = "code";
                cboAttendant.Properties.BestFitMode = BestFitMode.BestFit;
                cboAttendant.Properties.BestFit();

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
                cboBirthMethod.Properties.DataSource = pDtBirthMethods;
                cboBirthMethod.Properties.DisplayMember = "description";
                cboBirthMethod.Properties.ValueMember = "code";
                cboBirthMethod.Properties.BestFitMode = BestFitMode.BestFit;
                cboBirthMethod.Properties.BestFit();

                //columns for complications
                pDtComplications.Columns.Add("selected", typeof(System.Boolean));
                pDtComplications.Columns.Add("description", typeof(System.String));
                pDtComplications.Columns.Add("fieldvalue", typeof(System.Int16));
                pDtComplications.Columns.Add("fieldname", typeof(System.String));
                pDtComplications.Columns.Add("quantityentry", typeof(System.Int16));

                grdComplications.DataSource = pDtComplications;

                //columns for history
                pDtExtraColumns = pMdtRCHPostNatalCare.View_Archive("1=2", "");

                pDtHistory.Columns.Add("birthmethoddescription", typeof(System.String));
                foreach (DataColumn mDataColumn in pDtExtraColumns.Columns)
                {
                    pDtHistory.Columns.Add(mDataColumn.ColumnName, mDataColumn.DataType);
                }

                grdHistory.DataSource = pDtHistory;

                //hide unneccessary columns
                foreach (DevExpress.XtraGrid.Columns.GridColumn mGridColumn in viewHistory.Columns)
                {
                    if (mGridColumn.FieldName.ToLower() != "bookdate"
                        && mGridColumn.FieldName.ToLower() != "birthmethoddescription"
                        && mGridColumn.FieldName.ToLower() != "gravida"
                        && mGridColumn.FieldName.ToLower() != "para"
                        && mGridColumn.FieldName.ToLower() != "booking")
                    {
                        mGridColumn.Visible = false;
                    }
                }

                //fill lookup data
                this.Fill_BirthMethods();
                this.Fill_Complications();
                this.Fill_Attendants();

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

        #region frmRCHPostnatalCare_Load
        private void frmRCHPostnatalCare_Load(object sender, EventArgs e)
        {
            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            this.Load_Controls();

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

        #region frmRCHPostnatalCare_FormClosing
        private void frmRCHPostnatalCare_FormClosing(object sender, FormClosingEventArgs e)
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

        #region Fill_Children
        private void Fill_Children(string mBooking)
        {
            string mFunctionName = "Fill_Children";

            try
            {
                pDtChildren.Rows.Clear();

                string mFilter = "clientcode='" + txtClientId.Text.Trim() + "' and booking='" + mBooking.Trim() + "'";

                DataTable mDtChildren = pMdtRCHPostNatalCare.View_Children(mFilter, "deliverydate desc,autocode desc");

                foreach (DataRow mDataRow in mDtChildren.Rows)
                {
                    string mStillBirth = "";
                    if (Convert.ToInt16(mDataRow["freshbirth"]) == 1)
                    {
                        mStillBirth = AfyaPro_Types.clsEnums.RCHStillBirths.freshbirth.ToString().Trim();
                    }
                    if (Convert.ToInt16(mDataRow["maceratedbirth"]) == 1)
                    {
                        mStillBirth = AfyaPro_Types.clsEnums.RCHStillBirths.maceratedbirth.ToString().Trim();
                    }

                    string mChildCondition = "";
                    if (Convert.ToInt16(mDataRow["live"]) == 1)
                    {
                        mChildCondition = AfyaPro_Types.clsEnums.RCHChildConditions.live.ToString().Trim();
                    }
                    if (Convert.ToInt16(mDataRow["deathbefore24"]) == 1)
                    {
                        mChildCondition = AfyaPro_Types.clsEnums.RCHChildConditions.deathbefore24.ToString().Trim();
                    }
                    if (Convert.ToInt16(mDataRow["deathafter24"]) == 1)
                    {
                        mChildCondition = AfyaPro_Types.clsEnums.RCHChildConditions.deathafter24.ToString().Trim();
                    }

                    DataRow mNewRow = pDtChildren.NewRow();
                    mNewRow["deliverydate"] = mDataRow["deliverydate"];
                    mNewRow["gender"] = mDataRow["gender"];
                    mNewRow["weight"] = mDataRow["weight"];
                    mNewRow["apgarscore"] = mDataRow["apgarscore"];
                    mNewRow["childproblems"] = mDataRow["childproblems"];
                    mNewRow["stillbirth"] = mStillBirth;
                    mNewRow["childcondition"] = mChildCondition;
                    pDtChildren.Rows.Add(mNewRow);
                    pDtChildren.AcceptChanges();
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

                DataTable mDtHistory = pMdtRCHPostNatalCare.View_Archive(
                    "clientcode='" + txtClientId.Text.Trim() + "'", "bookdate desc,autocode desc");

                DataView mDvComplications = new DataView();
                mDvComplications.Table = pDtComplications;
                mDvComplications.Sort = "fieldname";

                DataView mDvBirthMethods = new DataView();
                mDvBirthMethods.Table = pDtBirthMethods;
                mDvBirthMethods.Sort = "code";

                foreach (DataRow mDataRow in mDtHistory.Rows)
                {
                    string mBirthMethodDescription = "";

                    DataRow mNewRow = pDtHistory.NewRow();

                    foreach (DataColumn mDataColumn in pDtExtraColumns.Columns)
                    {
                        mNewRow[mDataColumn.ColumnName] = mDataRow[mDataColumn.ColumnName];
                    }

                    int mMethodRowIndex = mDvBirthMethods.Find(mDataRow["birthmethod"].ToString().Trim());
                    if (mMethodRowIndex >= 0)
                    {
                        mBirthMethodDescription = mDvBirthMethods[mMethodRowIndex]["description"].ToString().Trim();
                    }

                    mNewRow["birthmethoddescription"] = mBirthMethodDescription;
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
            foreach (DataRow mDataRow in pDtComplications.Rows)
            {
                mDataRow.BeginEdit();
                mDataRow["selected"] = false;
                mDataRow["fieldvalue"] = 1;
                mDataRow.EndEdit();
            }

            txtAdmissionDate.EditValue = txtDate.EditValue;
            cboBirthMethod.EditValue = null;
            txtGravida.Text = "";
            txtPara.Text = "";
            cboDischargeStatus.Text = "";
            cboAttendant.EditValue = null;
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
            int mRowIndex;

            #region danger indicators

            DataTable mDtComplications = pMdtRCHPostNatalCare.View_Complications(
                                    "clientcode='" + txtClientId.Text.Trim() + "' and booking='" + mSelectedRow["booking"].ToString().Trim() + "'",
                                    "");
            DataView mDvComplications = new DataView();
            mDvComplications.Table = mDtComplications;
            mDvComplications.Sort = "methodcode";

            foreach (DataRow mDataRow in pDtComplications.Rows)
            {
                double mFieldValue = 0;

                mRowIndex = mDvComplications.Find(mDataRow["fieldname"].ToString().Trim());

                if (mRowIndex >= 0)
                {
                    mFieldValue = Convert.ToDouble(mDvComplications[mRowIndex]["quantity"]);
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

            txtDate.EditValue = Convert.ToDateTime(mSelectedRow["bookdate"]);
            if (Program.IsNullDate(mSelectedRow["admissiondate"]) == false)
            {
                txtAdmissionDate.EditValue = Convert.ToDateTime(mSelectedRow["admissiondate"]);
            }
            else
            {
                txtAdmissionDate.EditValue = DBNull.Value;
            }

            cboBirthMethod.ItemIndex = Program.Get_LookupItemIndex(cboBirthMethod, "code", mSelectedRow["birthmethod"].ToString());
            txtGravida.Text = mSelectedRow["gravida"].ToString().Trim();
            txtPara.Text = mSelectedRow["para"].ToString().Trim();
            cboDischargeStatus.Text = mSelectedRow["dischargestatus"].ToString().Trim();
            cboAttendant.ItemIndex = Program.Get_LookupItemIndex(cboAttendant, "code", mSelectedRow["attendantid"].ToString());

            this.Fill_Children(mSelectedRow["booking"].ToString().Trim());
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

        #region cmdChildren_Click
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

            string mBooking = "";

            if (viewHistory.FocusedRowHandle >= 0)
            {
                DataRow mSelectedRow = viewHistory.GetDataRow(viewHistory.FocusedRowHandle);
                mBooking = mSelectedRow["booking"].ToString().Trim();
            }

            frmRCHPostnatalChildren mRCHPostnatalChildren = new frmRCHPostnatalChildren(txtClientId.Text, mBooking, pDtChildren);
            mRCHPostnatalChildren.ShowDialog();
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
                AfyaPro_Types.clsEnums.RCHServices.postnatalcare.ToString(), txtClientId.Text);
            if (mValidClient == false)
            {
                return;
            }

            #endregion

            try
            {
                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

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
                if (cboBirthMethod.ItemIndex == -1)
                {
                    Program.Display_Error("Invalid birth method");
                    cboBirthMethod.Focus();
                    return;
                }

                string mBirthMethod = cboBirthMethod.GetColumnValue("code").ToString().Trim();

                int mGravida = 0;
                if (Program.IsNumeric(txtGravida.Text) == true)
                {
                    mGravida = Convert.ToInt32(txtGravida.Text);
                }

                int mPara = 0;
                if (Program.IsNumeric(txtPara.Text) == true)
                {
                    mPara = Convert.ToInt32(txtPara.Text);
                }

                DateTime mAdmissionDate = new DateTime();
                if (Program.IsNullDate(txtAdmissionDate.EditValue) == false)
                {
                    mAdmissionDate = Convert.ToDateTime(txtAdmissionDate.EditValue);
                }

                if (cboAttendant.ItemIndex == -1)
                {
                    Program.Display_Error("Invalid attendant");
                    cboAttendant.Focus();
                    return;
                }

                string mAttendantId = cboAttendant.GetColumnValue("code").ToString().Trim();
                string mAttendantName = cboAttendant.GetColumnValue("description").ToString().Trim();

                //add
                mResult = pMdtRCHPostNatalCare.Add(mTransDate, txtClientId.Text, mBirthMethod, mDtComplications,
                mGravida, mPara, mAdmissionDate, cboDischargeStatus.Text, mAttendantId, mAttendantName, 0, 
                pDtChildren, Program.gCurrentUser.Code);

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
                if (cboBirthMethod.ItemIndex == -1)
                {
                    Program.Display_Error("Invalid birth method");
                    cboBirthMethod.Focus();
                    return;
                }

                string mBirthMethod = cboBirthMethod.GetColumnValue("code").ToString().Trim();

                int mGravida = 0;
                if (Program.IsNumeric(txtGravida.Text) == true)
                {
                    mGravida = Convert.ToInt32(txtGravida.Text);
                }

                int mPara = 0;
                if (Program.IsNumeric(txtPara.Text) == true)
                {
                    mPara = Convert.ToInt32(txtPara.Text);
                }

                DateTime mAdmissionDate = new DateTime();
                if (Program.IsNullDate(txtAdmissionDate.EditValue) == false)
                {
                    mAdmissionDate = Convert.ToDateTime(txtAdmissionDate.EditValue);
                }

                if (cboAttendant.ItemIndex == -1)
                {
                    Program.Display_Error("Invalid attendant");
                    cboAttendant.Focus();
                    return;
                }

                string mAttendantId = cboAttendant.GetColumnValue("code").ToString().Trim();
                string mAttendantName = cboAttendant.GetColumnValue("description").ToString().Trim();

                //edit
                mResult = pMdtRCHPostNatalCare.Edit(mTransDate, mSelectedRow["booking"].ToString(), 
                txtClientId.Text, mBirthMethod, mDtComplications, mGravida, mPara, mAdmissionDate, 
                cboDischargeStatus.Text, mAttendantId, mAttendantName, 0, pDtChildren, Program.gCurrentUser.Code);

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
                mResult = pMdtRCHPostNatalCare.Delete(mTransDate, mSelectedRow["booking"].ToString(),
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

            grdComplications.Focus();
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
                        this.cmdUpdate_Click(cmdUpdate, e);
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