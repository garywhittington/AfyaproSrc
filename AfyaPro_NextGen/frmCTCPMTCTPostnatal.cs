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
    public partial class frmCTCPMTCTPostnatal : DevExpress.XtraEditors.XtraForm
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

        private DataTable pDtOtherChildren = new DataTable("otherchildren");

        private DataRow pCurrentPregnancy = null;
        private bool pIsNewBirth = true;

        #endregion

        #region frmCTCPMTCTPostnatal
        public frmCTCPMTCTPostnatal()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCPMTCTPostnatal";
            this.KeyDown += new KeyEventHandler(frmCTCPMTCTPostnatal_KeyDown);

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

                pDtOtherChildren = pMdtCTCClients.View_PMTCTPostnatalOtherChildren("1=2", "", "grdPMTCTOtherChildren");

                this.Fill_LookupData();

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpmtctantenatal_customizelayout.ToString());
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
            mObjectsList.Add(cmdNewBirth);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region frmCTCPMTCTPostnatal_Load
        private void frmCTCPMTCTPostnatal_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCPMTCTPostnatal_Load";

            try
            {
                Program.Restore_FormLayout(layoutControl1, this.Name);
                Program.Restore_FormSize(this);

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

        #region frmCTCPMTCTPostnatal_Shown
        private void frmCTCPMTCTPostnatal_Shown(object sender, EventArgs e)
        {
            if (pSelectedPatient != null)
            {
                this.Data_Display(pSelectedPatient);
            }
        }
        #endregion

        #region frmCTCPMTCTPostnatal_FormClosing
        private void frmCTCPMTCTPostnatal_FormClosing(object sender, FormClosingEventArgs e)
        {
            //layout
            if (layoutControl1.IsModified == true)
            {
                Program.Save_FormLayout(this, layoutControl1, this.Name);
            }
        }
        #endregion

        #region Append_ShortcutKeys
        private void Append_ShortcutKeys()
        {
            //cmdOk.Text = cmdOk.Text + " (" + Program.KeyCode_Ok.ToString() + ")";
        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region disclosed to

                DataTable mDtReferedFrom = pMdtReporter.View_LookupData("ctc_pmtctdisclosedto", "code,description", "", "", Program.gLanguageName, "grdPMTCTDisclosedTo");
                cboDisclosedTo.Properties.DataSource = mDtReferedFrom;
                cboDisclosedTo.Properties.DisplayMember = "description";
                cboDisclosedTo.Properties.ValueMember = "code";
                cboDisclosedTo.Properties.BestFit();

                #endregion
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
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

                pDtOtherChildren = pMdtCTCClients.View_PMTCTPostnatalOtherChildren("patientcode='" + txtPatientId.Text.Trim() + "'", "", "grdPMTCTOtherChildren");

                #endregion

                #region last date booked

                AfyaPro_Types.ctcBooking mBooking = pMdtCTCClients.Get_Booking(txtPatientId.Text.Trim());
                if (mBooking != null)
                {
                    if (mBooking.IsBooked == true)
                    {
                        txtLastVisitDate.EditValue = mBooking.BookDate;
                    }
                }

                #endregion

                this.Display_CurrentBirth();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Display_CurrentBirth
        private void Display_CurrentBirth()
        {
            string mFunctionName = "Display_CurrentBirth";

            try
            {
                this.Data_ClearBirth();

                DataTable mDtHistory = pMdtCTCClients.View_PMTCTPostnatal(
                    "patientcode='" + txtPatientId.Text.Trim() + "'", "transdate desc limit 1");

                if (mDtHistory.Rows.Count > 0)
                {
                    pCurrentPregnancy = mDtHistory.Rows[0];

                    txtDate.EditValue = Convert.ToDateTime(pCurrentPregnancy["transdate"]);
                    cboHIVResult.SelectedIndex = pCurrentPregnancy["hivstatuscode"] == DBNull.Value ? 0 : Convert.ToInt32(pCurrentPregnancy["hivstatuscode"]);
                    chkSeenAtAnotherSite.Checked = Convert.ToBoolean(pCurrentPregnancy["seenatanothersite"]);
                    txtAnotherSiteName.Text = pCurrentPregnancy["anothersitename"].ToString();
                    cboDisclosedTo.ItemIndex = Program.Get_LookupItemIndex(cboDisclosedTo, "code", pCurrentPregnancy["disclosedtocode"].ToString());
                    if (Program.IsNullDate(pCurrentPregnancy["cd4testdate"]) == false)
                    {
                        txtCD4TestDate.EditValue = Convert.ToDateTime(pCurrentPregnancy["cd4testdate"]);
                    }
                    txtCD4Count.Text = pCurrentPregnancy["cd4testresult"].ToString();
                    chkMotherOnHaart.Checked = Convert.ToBoolean(pCurrentPregnancy["motheronhaartforlife"]);
                    chkBabyOnHaart.Checked = Convert.ToBoolean(pCurrentPregnancy["babyonhaartforlife"]);
                    if (Program.IsNullDate(pCurrentPregnancy["babybirthdate"]) == false)
                    {
                        txtBabyBirthDate.EditValue = Convert.ToDateTime(pCurrentPregnancy["babybirthdate"]);
                    }
                    if (Program.IsNullDate(pCurrentPregnancy["babyhivdate"]) == false)
                    {
                        txtBabyHivDate.EditValue = Convert.ToDateTime(pCurrentPregnancy["babyhivdate"]);
                    }
                    cboBabyHivStatus.SelectedIndex = Convert.ToInt32(pCurrentPregnancy["babyhivresult"]);
                    chkFacilityDelivery.Checked = Convert.ToBoolean(pCurrentPregnancy["deliverinfacility"]);

                    this.pIsNewBirth = false;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Data_ClearBirth
        private void Data_ClearBirth()
        {
            pCurrentPregnancy = null;

            txtDate.EditValue = Program.gMdiForm.txtDate.EditValue;
            cboHIVResult.SelectedIndex = -1;
            chkSeenAtAnotherSite.Checked = false;
            txtAnotherSiteName.Text = "";
            cboDisclosedTo.EditValue = null;
            txtCD4TestDate.EditValue = null;
            txtCD4Count.Text = "";
            chkMotherOnHaart.Checked = false;
            chkBabyOnHaart.Checked = false;
            txtBabyBirthDate.EditValue = null;
            txtBabyHivDate.EditValue = null;
            cboBabyHivStatus.SelectedIndex = -1;
            chkFacilityDelivery.Checked = false;
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
            txtLastVisitDate.EditValue = null;

            pDtOtherChildren.Rows.Clear();
        }
        #endregion

        #region cmdNewBirth_Click
        private void cmdNewBirth_Click(object sender, EventArgs e)
        {
            this.Data_ClearBirth();
            this.pIsNewBirth = true;
        }
        #endregion

        #region cmdSave_Click
        private void cmdSave_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdSave_Click";

            #region validation

            if (Program.IsNullDate(txtDate.EditValue) == true)
            {
                Program.Display_Error("Invalid date entry", false);
                txtDate.Focus();
                return;
            }

            if (cboHIVResult.SelectedIndex == -1)
            {
                Program.Display_Error("Invalid HIV Status", false);
                cboHIVResult.Focus();
                return;
            }

            if (cboDisclosedTo.ItemIndex == -1)
            {
                Program.Display_Error("Invalid entry for Disclosed To", false);
                cboDisclosedTo.Focus();
                return;
            }

            if (Program.IsNullDate(txtBabyBirthDate.EditValue) == true)
            {
                Program.Display_Error("Invalid entry for date of birth", false);
                txtBabyBirthDate.Focus();
                return;
            }

            #endregion

            try
            {
                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

                DateTime? mCD4TestDate = null;
                if (Program.IsNullDate(txtCD4TestDate.EditValue) == false)
                {
                    mCD4TestDate = Convert.ToDateTime(txtCD4TestDate.EditValue);
                }

                DateTime? mBabyHIVDate = null;
                if (Program.IsNullDate(txtBabyHivDate.EditValue) == false)
                {
                    mBabyHIVDate = Convert.ToDateTime(txtBabyHivDate.EditValue);
                }

                double mCD4TestResult = Program.IsMoney(txtCD4Count.Text) == false ? 0 : Convert.ToDouble(txtCD4Count.Text);

                if (pIsNewBirth == true)
                {
                    AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Add_PMTCTPostnatal(
                                            mTransDate,
                                            txtPatientId.Text,
                                            cboHIVResult.SelectedIndex,
                                            Convert.ToInt32(chkSeenAtAnotherSite.Checked),
                                            txtAnotherSiteName.Text.Trim(),
                                            Convert.ToDateTime(txtBabyBirthDate.EditValue),
                                            cboDisclosedTo.GetColumnValue("code").ToString(),
                                            Convert.ToInt32(chkMotherOnHaart.Checked),
                                            Convert.ToInt32(chkBabyOnHaart.Checked),
                                            Convert.ToInt32(chkFacilityDelivery.Checked),
                                            mBabyHIVDate,
                                            cboBabyHivStatus.SelectedIndex,
                                            pDtOtherChildren,
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

                    this.Display_CurrentBirth();
                    pDtOtherChildren = pMdtCTCClients.View_PMTCTPostnatalOtherChildren("patientcode='" + txtPatientId.Text.Trim() + "'", "", "grdPMTCTOtherChildren");
                }
                else
                {
                    AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Edit_PMTCTPostnatal(
                                            Convert.ToInt32(pCurrentPregnancy["autocode"]),
                                            mTransDate,
                                            txtPatientId.Text,
                                            cboHIVResult.SelectedIndex,
                                            Convert.ToInt32(chkSeenAtAnotherSite.Checked),
                                            txtAnotherSiteName.Text.Trim(),
                                            Convert.ToDateTime(txtBabyBirthDate.EditValue),
                                            cboDisclosedTo.GetColumnValue("code").ToString(),
                                            Convert.ToInt32(chkMotherOnHaart.Checked),
                                            Convert.ToInt32(chkBabyOnHaart.Checked),
                                            Convert.ToInt32(chkFacilityDelivery.Checked),
                                            mBabyHIVDate,
                                            cboBabyHivStatus.SelectedIndex,
                                            pDtOtherChildren,
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

                    this.Display_CurrentBirth();
                    pDtOtherChildren = pMdtCTCClients.View_PMTCTPostnatalOtherChildren("patientcode='" + txtPatientId.Text.Trim() + "'", "", "grdPMTCTOtherChildren");
                }
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

        #region frmCTCPMTCTPostnatal_KeyDown
        void frmCTCPMTCTPostnatal_KeyDown(object sender, KeyEventArgs e)
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
            if (txtHIVNo.Text.Trim().ToLower() == "<<---new--->>")
            {
                txtHIVNo.Text = "";
            }
            else
            {
                txtHIVNo.SelectAll();
            }
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

            if (txtHIVNo.Text.Trim() == "")
            {
                Int16 mGenerateCode = pMdtAutoCodes.Auto_Generate_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.hivnumbers));
                if (mGenerateCode == -1)
                {
                    return;
                }
                if (mGenerateCode == 1)
                {
                    txtHIVNo.Text = "<<---New--->>";
                }
                else
                {
                    txtHIVNo.Focus();
                }
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

        #region chkSeenAtAnotherSite_CheckedChanged
        private void chkSeenAtAnotherSite_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeenAtAnotherSite.Checked == true)
            {
                txtAnotherSiteName.Properties.ReadOnly = false;
            }
            else
            {
                txtAnotherSiteName.Properties.ReadOnly = true;
            }
        }
        #endregion

        #region cmdNewVisit_Click
        private void cmdNewVisit_Click(object sender, EventArgs e)
        {
            if (pCurrentPregnancy == null)
            {
                Program.Display_Error("No current birth to serve");
                return;
            }

            frmCTCPMTCTPostnatalVisitDetails mCTCPMTCTPostnatalVisitDetails = new frmCTCPMTCTPostnatalVisitDetails();
            mCTCPMTCTPostnatalVisitDetails.SelectedPatient = pSelectedPatient;
            mCTCPMTCTPostnatalVisitDetails.CurrentPregRow = pCurrentPregnancy;
            mCTCPMTCTPostnatalVisitDetails.ShowDialog();
        }
        #endregion

        #region cmdHistory_Click
        private void cmdHistory_Click(object sender, EventArgs e)
        {
            if (pCurrentPregnancy == null)
            {
                Program.Display_Error("No current birth to serve");
                return;
            }

            frmCTCPMTCTPostnatalVisits mCTCPMTCTPostnatalVisits = new frmCTCPMTCTPostnatalVisits();
            mCTCPMTCTPostnatalVisits.SelectedPatient = pSelectedPatient;
            mCTCPMTCTPostnatalVisits.CurrentPregRow = pCurrentPregnancy;
            mCTCPMTCTPostnatalVisits.ShowDialog();
        }
        #endregion

        #region cmdOtherChildren_Click
        private void cmdOtherChildren_Click(object sender, EventArgs e)
        {
            if (pSelectedPatient.Exist == false)
            {
                Program.Display_Error("Please enter a valid patient number and try again");
                txtPatientId.Focus();
                return;
            }

            frmCTCPMTCTOtherChildren mCTCPMTCTOtherChildren = new frmCTCPMTCTOtherChildren(pDtOtherChildren);
            mCTCPMTCTOtherChildren.ShowDialog();
        }
        #endregion
    }
}