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
    public partial class frmCTCPMTCTAntenatal : DevExpress.XtraEditors.XtraForm
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
        private bool pIsNewPreg = true;

        #endregion

        #region frmCTCPMTCTAntenatal
        public frmCTCPMTCTAntenatal()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCPMTCTAntenatal";
            this.KeyDown += new KeyEventHandler(frmCTCPMTCTAntenatal_KeyDown);

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

                pDtOtherChildren = pMdtCTCClients.View_PMTCTAntenatalOtherChildren("1=2", "", "grdPMTCTOtherChildren");

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
            mObjectsList.Add(cmdNewPreg);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region frmCTCPMTCTAntenatal_Load
        private void frmCTCPMTCTAntenatal_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCPMTCTAntenatal_Load";

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

        #region frmCTCPMTCTAntenatal_Shown
        private void frmCTCPMTCTAntenatal_Shown(object sender, EventArgs e)
        {
            if (pSelectedPatient != null)
            {
                this.Data_Display(pSelectedPatient);
            }
        }
        #endregion

        #region frmCTCPMTCTAntenatal_FormClosing
        private void frmCTCPMTCTAntenatal_FormClosing(object sender, FormClosingEventArgs e)
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

                pDtOtherChildren = pMdtCTCClients.View_PMTCTAntenatalOtherChildren("patientcode='" + txtPatientId.Text.Trim() + "'", "", "grdPMTCTOtherChildren");

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

                this.Display_CurrentPregnancy();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Display_CurrentPregnancy
        private void Display_CurrentPregnancy()
        {
            string mFunctionName = "Display_CurrentPregnancy";

            try
            {
                this.Data_ClearPreg();

                DataTable mDtHistory = pMdtCTCClients.View_PMTCTAntenatal(
                    "patientcode='" + txtPatientId.Text.Trim() + "'", "transdate desc limit 1");

                if (mDtHistory.Rows.Count > 0)
                {
                    pCurrentPregnancy = mDtHistory.Rows[0];

                    txtDate.EditValue = Convert.ToDateTime(pCurrentPregnancy["transdate"]);
                    cboHIVResult.SelectedIndex = pCurrentPregnancy["hivstatuscode"] == DBNull.Value ? 0 : Convert.ToInt32(pCurrentPregnancy["hivstatuscode"]);
                    chkSeenAtAnotherSite.Checked = Convert.ToBoolean(pCurrentPregnancy["seenatanothersite"]);
                    txtAnotherSiteName.Text = pCurrentPregnancy["anothersitename"].ToString();
                    txtGestationAge.Text = pCurrentPregnancy["gestationage"].ToString();
                    cboDisclosedTo.ItemIndex = Program.Get_LookupItemIndex(cboDisclosedTo, "code", pCurrentPregnancy["disclosedtocode"].ToString());
                    if (Program.IsNullDate(pCurrentPregnancy["cd4testdate"]) == false)
                    {
                        txtCD4TestDate.EditValue = Convert.ToDateTime(pCurrentPregnancy["cd4testdate"]);
                    }
                    txtCD4Count.Text = pCurrentPregnancy["cd4testresult"].ToString();
                    cboInfantFeedIntention.SelectedIndex = Convert.ToInt32(pCurrentPregnancy["infantfeedingintention"]);
                    chkOnHaart.Checked = Convert.ToBoolean(pCurrentPregnancy["onhaartforlife"]);
                    cboWhenOnHaart.SelectedIndex = Convert.ToInt32(pCurrentPregnancy["whenstartedhaartforlife"]);
                    chkPlanFacilityDelivery.Checked = Convert.ToBoolean(pCurrentPregnancy["plantodeliverinfacility"]);

                    this.pIsNewPreg = false;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Data_ClearPreg
        private void Data_ClearPreg()
        {
            pCurrentPregnancy = null;

            txtDate.EditValue = Program.gMdiForm.txtDate.EditValue;
            cboHIVResult.SelectedIndex = -1;
            chkSeenAtAnotherSite.Checked = false;
            txtAnotherSiteName.Text = "";
            txtGestationAge.Text = "";
            cboDisclosedTo.EditValue = null;
            txtCD4TestDate.EditValue = null;
            txtCD4Count.Text = "";
            cboInfantFeedIntention.SelectedIndex = -1;
            chkOnHaart.Checked = false;
            cboWhenOnHaart.SelectedIndex = -1;
            chkPlanFacilityDelivery.Checked = false;
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

        #region cmdNewPreg_Click
        private void cmdNewPreg_Click(object sender, EventArgs e)
        {
            this.Data_ClearPreg();
            this.pIsNewPreg = true;
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

            #endregion

            try
            {
                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

                DateTime? mCD4TestDate = null;
                if (Program.IsNullDate(txtCD4TestDate.EditValue) == false)
                {
                    mCD4TestDate = Convert.ToDateTime(txtCD4TestDate.EditValue);
                }

                double mCD4TestResult = Program.IsMoney(txtCD4Count.Text) == false ? 0 : Convert.ToDouble(txtCD4Count.Text);

                if (pIsNewPreg == true)
                {
                    AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Add_PMTCTAntenatal(
                                            mTransDate,
                                            txtPatientId.Text,
                                            cboHIVResult.SelectedIndex,
                                            Convert.ToInt32(chkSeenAtAnotherSite.Checked),
                                            txtAnotherSiteName.Text.Trim(),
                                            Convert.ToDouble(txtGestationAge.Text),
                                            cboDisclosedTo.GetColumnValue("code").ToString(),
                                            Convert.ToInt32(chkOnHaart.Checked),
                                            cboWhenOnHaart.SelectedIndex,
                                            cboInfantFeedIntention.SelectedIndex,
                                            Convert.ToInt32(chkPlanFacilityDelivery.Checked),
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

                    this.Display_CurrentPregnancy();
                    pDtOtherChildren = pMdtCTCClients.View_PMTCTAntenatalOtherChildren("patientcode='" + txtPatientId.Text.Trim() + "'", "", "grdPMTCTOtherChildren");
                }
                else
                {
                    AfyaPro_Types.clsCtcClient mCtcClient = pMdtCTCClients.Edit_PMTCTAntenatal(
                                            Convert.ToInt32(pCurrentPregnancy["autocode"]),
                                            mTransDate,
                                            txtPatientId.Text,
                                            cboHIVResult.SelectedIndex,
                                            Convert.ToInt32(chkSeenAtAnotherSite.Checked),
                                            txtAnotherSiteName.Text.Trim(),
                                            Convert.ToDouble(txtGestationAge.Text),
                                            cboDisclosedTo.GetColumnValue("code").ToString(),
                                            Convert.ToInt32(chkOnHaart.Checked),
                                            cboWhenOnHaart.SelectedIndex,
                                            cboInfantFeedIntention.SelectedIndex,
                                            Convert.ToInt32(chkPlanFacilityDelivery.Checked),
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

                    this.Display_CurrentPregnancy();
                    pDtOtherChildren = pMdtCTCClients.View_PMTCTAntenatalOtherChildren("patientcode='" + txtPatientId.Text.Trim() + "'", "", "grdPMTCTOtherChildren");
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

        #region frmCTCPMTCTAntenatal_KeyDown
        void frmCTCPMTCTAntenatal_KeyDown(object sender, KeyEventArgs e)
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

        #region chkOnHaart_CheckedChanged
        private void chkOnHaart_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOnHaart.Checked == true)
            {
                cboWhenOnHaart.Properties.ReadOnly = false;
            }
            else
            {
                cboWhenOnHaart.Properties.ReadOnly = true;
            }
        }
        #endregion

        #region cmdNewVisit_Click
        private void cmdNewVisit_Click(object sender, EventArgs e)
        {
            if (pCurrentPregnancy == null)
            {
                Program.Display_Error("No current pregnancy to serve");
                return;
            }

            frmCTCPMTCTAntenatalVisitDetails mCTCPMTCTAntenatalVisitDetails = new frmCTCPMTCTAntenatalVisitDetails();
            mCTCPMTCTAntenatalVisitDetails.SelectedPatient = pSelectedPatient;
            mCTCPMTCTAntenatalVisitDetails.CurrentPregRow = pCurrentPregnancy;
            mCTCPMTCTAntenatalVisitDetails.ShowDialog();

            if (mCTCPMTCTAntenatalVisitDetails.RecordSaved == true)
            {
                txtPatientId.Text = "";
                this.Data_ClearAll();
                this.Data_ClearPreg();
                txtPatientId.Focus();
            }
        }
        #endregion

        #region cmdHistory_Click
        private void cmdHistory_Click(object sender, EventArgs e)
        {
            if (pCurrentPregnancy == null)
            {
                Program.Display_Error("No current pregnancy to serve");
                return;
            }

            frmCTCPMTCTAntenatalVisits mCTCPMTCTAntenatalVisits = new frmCTCPMTCTAntenatalVisits();
            mCTCPMTCTAntenatalVisits.SelectedPatient = pSelectedPatient;
            mCTCPMTCTAntenatalVisits.CurrentPregRow = pCurrentPregnancy;
            mCTCPMTCTAntenatalVisits.ShowDialog();
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