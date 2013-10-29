using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;

namespace AfyaPro_NextGen
{
    public partial class frmCTCPreventiveTherapies : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsReporter pMdtReporter;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_MT.clsCTCClients pMdtCTCClients;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private AfyaPro_Types.ctcBooking pBooking = new AfyaPro_Types.ctcBooking();

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private AfyaPro_Types.clsCtcClient pSelectedPatient = null;
        internal AfyaPro_Types.clsCtcClient SelectedPatient
        {
            set { pSelectedPatient = value; }
            get { return pSelectedPatient; }
        }
        private string pCurrPatientId = "";
        private string pPrevPatientId = "";
        private string pCurrHIVNo = "";
        private string pPrevHIVNo = "";
        private string pCurrCTCNo = "";
        private string pPrevCTCNo = "";
        private bool pSearchingPatient = false;

        private DataTable pDtHistory = new DataTable("history");

        #endregion

        #region frmCTCPreventiveTherapies
        public frmCTCPreventiveTherapies()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCPreventiveTherapies";
            this.KeyDown += new KeyEventHandler(frmCTCPreventiveTherapies_KeyDown);

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                this.CancelButton = cmdClose;

                pMdtAutoCodes = (AfyaPro_MT.clsAutoCodes)Activator.GetObject(
                    typeof(AfyaPro_MT.clsAutoCodes),
                    Program.gMiddleTier + "clsAutoCodes");

                pMdtCTCClients = (AfyaPro_MT.clsCTCClients)Activator.GetObject(
                    typeof(AfyaPro_MT.clsCTCClients),
                    Program.gMiddleTier + "clsCTCClients");

                pMdtReporter = (AfyaPro_MT.clsReporter)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReporter),
                    Program.gMiddleTier + "clsReporter");

                //history
                pDtHistory = pMdtCTCClients.View_PreventiveTherapies("1=2", "transdate desc,autocode desc", grdCTCPreventiveTherapies.Name);

                pDtHistory.Columns["transdate"].Caption = "Date";
                pDtHistory.Columns["therapydescription"].Caption = "Therapy";

                grdCTCPreventiveTherapies.DataSource = pDtHistory;

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpreventivetherapies_customizelayout.ToString());

                this.Fill_LookupData();
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
            mObjectsList.Add(cmdAdd);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region frmCTCPreventiveTherapies_Load
        private void frmCTCPreventiveTherapies_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCPreventiveTherapies_Load";

            try
            {
                Program.Restore_FormLayout(layoutControl1, this.Name);
                Program.Restore_FormSize(this);
                Program.Restore_GridLayout(grdCTCPreventiveTherapies, grdCTCPreventiveTherapies.Name);

                this.pFormWidth = this.Width;
                this.pFormHeight = this.Height;

                Program.Center_Screen(this);

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

        #region frmCTCPreventiveTherapies_Shown
        private void frmCTCPreventiveTherapies_Shown(object sender, EventArgs e)
        {
            this.Data_Clear();

            if (pSelectedPatient != null)
            {
                this.Data_Display(pSelectedPatient);
            }

            txtDate.EditValue = DateTime.Now.Date;
        }
        #endregion

        #region frmCTCPreventiveTherapies_FormClosing
        private void frmCTCPreventiveTherapies_FormClosing(object sender, FormClosingEventArgs e)
        {
            //layout
            if (layoutControl1.IsModified == true)
            {
                Program.Save_FormLayout(this, layoutControl1, this.Name);
            }

            Program.Save_GridLayout(grdCTCPreventiveTherapies, grdCTCPreventiveTherapies.Name);
        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            string mFunctionName = "Fill_LookupData";

            try
            {
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
            //cmdOk.Text = cmdOk.Text + " (" + Program.KeyCode_Ok.ToString() + ")";
        }
        #endregion

        #region Data_Display
        private void Data_Display(AfyaPro_Types.clsCtcClient mPatient)
        {
            string mFunctionName = "Data_Display";

            try
            {
                this.pSearchingPatient = false;

                this.Data_ClearAll();

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
                txtHIVNo.Text = mPatient.hivno;
                txtCTCNo.Text = mPatient.ctcno;
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

                #endregion

                #region last test date

                DataTable mDtHIVTests = pMdtCTCClients.View_PreventiveTherapies("patientcode='" + mPatient.code.Trim() + "'", "transdate desc, autocode desc", grdCTCPreventiveTherapies.Name);

                if (mDtHIVTests.Rows.Count > 0)
                {
                    txtLastVisitDate.EditValue = Convert.ToDateTime(mDtHIVTests.Rows[0]["transdate"]);
                }
                else
                {
                    txtLastVisitDate.EditValue = null;
                }

                #endregion

                this.Fill_History();

                txtDate.EditValue = Program.gMdiForm.txtDate.EditValue;
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

                DataTable mDtHistory = pMdtCTCClients.View_PreventiveTherapies(
                    "patientcode='" + txtPatientId.Text.Trim() + "'", "transdate desc,autocode desc", grdCTCPreventiveTherapies.Name);

                foreach (DataRow mDataRow in mDtHistory.Rows)
                {
                    DataRow mNewRow = pDtHistory.NewRow();

                    foreach (DataColumn mDataColumn in pDtHistory.Columns)
                    {
                        mNewRow[mDataColumn.ColumnName] = mDataRow[mDataColumn.ColumnName];
                    }

                    pDtHistory.Rows.Add(mNewRow);
                    pDtHistory.AcceptChanges();
                }

                this.Data_Clear();
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
            cboTherapy.SelectedIndex = -1;
        }
        #endregion

        #region Data_ClearAll
        private void Data_ClearAll()
        {
            txtPatientId.Text = "";
            txtHIVNo.Text = "";
            txtCTCNo.Text = "";
            txtName.Text = "";
            txtYears.Text = "";
            txtMonths.Text = "";
            txtGender.Text = "";
            txtDate.EditValue = DateTime.Now.Date;
            txtLastVisitDate.EditValue = null;

            this.Data_Clear();
            pDtHistory.Rows.Clear();
        }
        #endregion

        #region cmdAdd_Click
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdAdd_Click";

            if (Program.IsDate(txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                txtDate.Focus();
                return;
            }
            if (cboTherapy.SelectedIndex == -1)
            {
                Program.Display_Error("Invalid Therapy selection");
                cboTherapy.Focus();
                return;
            }

            try
            {
                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Add_PreventiveTherapy(
                    pSelectedPatient.code,
                    mTransDate,
                    cboTherapy.SelectedIndex,
                    Program.gMachineName,
                    Program.gMachineUser,
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

        #region cmdUpdate_Click
        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdUpdate_Click";

            if (viewCTCPreventiveTherapies.FocusedRowHandle == -1)
            {
                Program.Display_Error("Please select a record to edit and try again", false);
                grdCTCPreventiveTherapies.Focus();
                return;
            }
            if (Program.IsDate(txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                txtDate.Focus();
                return;
            }
            if (cboTherapy.SelectedIndex == -1)
            {
                Program.Display_Error("Invalid Therapy selection");
                cboTherapy.Focus();
                return;
            }

            try
            {
                DataRow mSelectedRow = viewCTCPreventiveTherapies.GetDataRow(viewCTCPreventiveTherapies.FocusedRowHandle);
                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

                AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Edit_PreventiveTherapy(
                    Convert.ToInt32(mSelectedRow["autocode"]),
                    mTransDate,
                    cboTherapy.SelectedIndex,
                    Program.gMachineName,
                    Program.gMachineUser,
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

                Program.Display_Info("Record updated successfully");

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

        #region cmdDelete_Click
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            AfyaPro_Types.clsCtcClient mResult = new AfyaPro_Types.clsCtcClient();

            string mFunctionName = "cmdDelete_Click";

            try
            {
                if (viewCTCPreventiveTherapies.FocusedRowHandle <= -1)
                {
                    return;
                }

                DataRow mSelectedRow = viewCTCPreventiveTherapies.GetDataRow(viewCTCPreventiveTherapies.FocusedRowHandle);

                DialogResult mResp = Program.Confirm_Deletion("'"
                    + Convert.ToDateTime(mSelectedRow["transdate"]).Date.ToString("d") + "'     '" + mSelectedRow["therapydescription"].ToString() + "'");

                if (mResp != DialogResult.Yes)
                {
                    return;
                }

                //delete
                mResult = pMdtCTCClients.Delete_PreventiveTherapy(Convert.ToInt32(mSelectedRow["autocode"]),
                    Program.gMachineName,
                    Program.gMachineUser,
                    Program.gCurrentUser.Code);

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

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region cmdClear_Click
        private void cmdClear_Click(object sender, EventArgs e)
        {
            this.Data_ClearAll();
        }
        #endregion

        #region frmCTCPreventiveTherapies_KeyDown
        void frmCTCPreventiveTherapies_KeyDown(object sender, KeyEventArgs e)
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

        #region Search_Patient
        private AfyaPro_Types.clsCtcClient Search_Patient(string mFieldName, string mFieldValue)
        {
            string mFunctionName = "Search_Patient";

            try
            {
                if (mFieldValue.Trim() == "")
                {
                    return null;
                }

                pSelectedPatient = pMdtCTCClients.Get_Client(mFieldName, mFieldValue);
                return pSelectedPatient;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
        }
        #endregion

        #region cmdSearch_Click
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            pSearchingPatient = true;

            frmSearchCTCClient mSearchPatient = new frmSearchCTCClient(txtPatientId);
            mSearchPatient.ShowDialog();

            pSearchingPatient = false;
        }
        #endregion

        #region search by patientcode

        #region txtPatientId_EditValueChanged
        private void txtPatientId_EditValueChanged(object sender, EventArgs e)
        {
            if (pSearchingPatient == true)
            {
                this.pSelectedPatient = this.Search_Patient("p.code", txtPatientId.Text);
                this.Data_Display(pSelectedPatient);
            }
        }
        #endregion

        #region txtPatientId_Enter
        private void txtPatientId_Enter(object sender, EventArgs e)
        {
            txtPatientId.SelectAll();
        }
        #endregion

        #region txtPatientId_KeyDown
        private void txtPatientId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.pSelectedPatient = this.Search_Patient("p.code", txtPatientId.Text);
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
                this.pSelectedPatient = this.Search_Patient("p.code", txtPatientId.Text);
                this.Data_Display(pSelectedPatient);
            }
        }
        #endregion

        #endregion

        #region search by hivno

        #region txtHIVNo_EditValueChanged
        private void txtHIVNo_EditValueChanged(object sender, EventArgs e)
        {
            if (pSearchingPatient == true)
            {
                this.pSelectedPatient = this.Search_Patient("c.hivno", txtHIVNo.Text);
                this.Data_Display(pSelectedPatient);
            }
        }
        #endregion

        #region txtHIVNo_Enter
        private void txtHIVNo_Enter(object sender, EventArgs e)
        {
            txtHIVNo.SelectAll();
        }
        #endregion

        #region txtHIVNo_KeyDown
        private void txtHIVNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.pSelectedPatient = this.Search_Patient("c.hivno", txtHIVNo.Text);
                this.Data_Display(pSelectedPatient);
            }
        }
        #endregion

        #region txtHIVNo_Leave
        private void txtHIVNo_Leave(object sender, EventArgs e)
        {
            pCurrHIVNo = txtHIVNo.Text;

            if (pCurrHIVNo.Trim().ToLower() != pPrevHIVNo.Trim().ToLower())
            {
                this.pSelectedPatient = this.Search_Patient("c.hivno", txtHIVNo.Text);
                this.Data_Display(pSelectedPatient);
            }
        }
        #endregion

        #endregion

        #region search by ctcno

        #region txtCTCNo_EditValueChanged
        private void txtCTCNo_EditValueChanged(object sender, EventArgs e)
        {
            if (pSearchingPatient == true)
            {
                this.pSelectedPatient = this.Search_Patient("c.ctcno", txtCTCNo.Text);
                this.Data_Display(pSelectedPatient);
            }
        }
        #endregion

        #region txtCTCNo_Enter
        private void txtCTCNo_Enter(object sender, EventArgs e)
        {
            txtCTCNo.SelectAll();
        }
        #endregion

        #region txtCTCNo_KeyDown
        private void txtCTCNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.pSelectedPatient = this.Search_Patient("c.ctcno", txtCTCNo.Text);
                this.Data_Display(pSelectedPatient);
            }
        }
        #endregion

        #region txtCTCNo_Leave
        private void txtCTCNo_Leave(object sender, EventArgs e)
        {
            pCurrHIVNo = txtCTCNo.Text;

            if (pCurrHIVNo.Trim().ToLower() != pPrevHIVNo.Trim().ToLower())
            {
                this.pSelectedPatient = this.Search_Patient("c.ctcno", txtCTCNo.Text);
                this.Data_Display(pSelectedPatient);
            }
        }
        #endregion

        #endregion

        #region handle grid selection

        #region Display_RowDetails
        private void Display_RowDetails(DataRow mSelectedRow)
        {
            if (mSelectedRow == null)
            {
                return;
            }

            txtDate.EditValue = Convert.ToDateTime(mSelectedRow["transdate"]);
            cboTherapy.SelectedIndex = (int)mSelectedRow["therapyid"];
        }
        #endregion

        #region viewCTCPreventiveTherapies_FocusedRowChanged
        private void viewCTCPreventiveTherapies_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle <= -1)
            {
                return;
            }

            this.Display_RowDetails(viewCTCPreventiveTherapies.GetDataRow(e.FocusedRowHandle));
        }
        #endregion

        #region viewCTCPreventiveTherapies_RowClick
        private void viewCTCPreventiveTherapies_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle <= -1)
            {
                return;
            }

            this.Display_RowDetails(viewCTCPreventiveTherapies.GetDataRow(e.RowHandle));
        }
        #endregion

        #region viewCTCPreventiveTherapies_GotFocus
        private void viewCTCPreventiveTherapies_GotFocus(object sender, EventArgs e)
        {
            if (viewCTCPreventiveTherapies.FocusedRowHandle <= -1)
            {
                return;
            }

            this.Display_RowDetails(viewCTCPreventiveTherapies.GetDataRow(viewCTCPreventiveTherapies.FocusedRowHandle));
        }
        #endregion

        #endregion
    }
}