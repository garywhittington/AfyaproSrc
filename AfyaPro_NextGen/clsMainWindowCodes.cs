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
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraNavBar;

namespace AfyaPro_NextGen
{
    class clsMainWindowCodes
    {

        private static string pClassName = "AfyaPro_NextGen.clsMainWindowCodes";

        private AfyaPro_MT.clsSMS pMdtSMS;

        

        #region Fill_Modules
        internal void Fill_Modules(NavBarControl mNavBarControl)
        {
            string mFunctionName = "Fill_Modules";

            pMdtSMS = (AfyaPro_MT.clsSMS)Activator.GetObject(
                  typeof(AfyaPro_MT.clsSMS),
                  Program.gMiddleTier + "clsSMS");
            

            try
            {
                #region register channel
                AfyaPro_MT.clsUserGroups mMdtUserGroups = (AfyaPro_MT.clsUserGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsUserGroups),
                    Program.gMiddleTier + "clsUserGroups");
                #endregion

                mNavBarControl.Groups.Clear();

                DataTable mDtModules = mMdtUserGroups.Get_Modules(Program.gLanguageName, "grdModules");
                DataTable mDtModuleItems = mMdtUserGroups.Get_ModuleItems(Program.gLanguageName, "grdModuleItems");
                DataTable mDtGroupsAccess = mMdtUserGroups.Get_UserModuleItems(
                    "usergroupcode='" + Program.gCurrentUser.UserGroupCode.Trim() + "'", "");

                DataView mDvModuleItems = new DataView();
                mDvModuleItems.Table = mDtModuleItems;

                DataView mDvGroupsAccess = new DataView();
                mDvGroupsAccess.Table = mDtGroupsAccess;

                DataView mDvActiveModules = new DataView();
                mDvActiveModules.Table = Program.gDtActiveModules;
                mDvActiveModules.Sort = "activationkey";

                NavBarControl mNavBarAdministrativeTools = new NavBarControl();
                mNavBarAdministrativeTools.LinkClicked += new NavBarLinkEventHandler(mNavBarAdministrativeTools_LinkClicked);
                bool mAdministrativeToolsFound = false;

                foreach (DataRow mModuleRow in mDtModules.Rows)
                {
                    string mModuleKey = mModuleRow["modulekey"].ToString().Trim();
                    string mModuleText = mModuleRow["moduletext"].ToString().Trim();
                    string mActivationKey = mModuleRow["activationkey"].ToString().Trim();
                    Int16 mLargeImageIndex = Convert.ToInt16(mModuleRow["iconindex"]);

                    if (mDvActiveModules.Find(mActivationKey) >= 0)
                    {
                        if (mModuleKey.Trim().ToLower() == "reports")
                        {
                            Fill_Reports(mNavBarControl, mLargeImageIndex);
                        }
                        else if (mModuleKey.Trim().ToLower() == "billingsetup"
                            || mModuleKey.Trim().ToLower() == "inventorysetup"
                            || mModuleKey.Trim().ToLower() == "reportdesigner"
                            || mModuleKey.Trim().ToLower() == "generalsetup"
                            || mModuleKey.Trim().ToLower() == "ctcsetup"
                            || mModuleKey.Trim().ToLower() == "securitysetup")
                        {
                            #region add administrative tools if at least one item is found
                            if (mAdministrativeToolsFound == false)
                            {
                                mDvGroupsAccess.RowFilter = "moduleitemkey like 'billingsetup%'"
                                    + " or moduleitemkey like 'inventorysetup%'"
                                    + " or moduleitemkey like 'reportdesigner%'"
                                    + " or moduleitemkey like 'generalsetup%'"
                                    + " or moduleitemkey like 'ctcsetup%'"
                                    + " or moduleitemkey like 'securitysetup%'";

                                if (mDvGroupsAccess.Count > 0)
                                {
                                    NavBarGroupControlContainer mNavBarGroupControlContainer = new NavBarGroupControlContainer();

                                    NavBarGroup mNavBarGroup = mNavBarControl.Groups.Add();
                                    mNavBarGroup.Name = "administrativetools";
                                    mNavBarGroup.Caption = "Administrative tools";
                                    mNavBarGroup.LargeImageIndex = mLargeImageIndex;
                                    mNavBarAdministrativeTools.SmallImages = Program.gMdiForm.imageList1;

                                    mNavBarGroupControlContainer.Controls.Add(mNavBarAdministrativeTools);
                                    mNavBarAdministrativeTools.Dock = System.Windows.Forms.DockStyle.Fill;
                                    mNavBarGroup.ControlContainer = mNavBarGroupControlContainer;
                                    mNavBarGroup.GroupClientHeight = 80;
                                    mNavBarGroup.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;

                                    mAdministrativeToolsFound = true;
                                }
                            }
                            #endregion

                            if (mAdministrativeToolsFound == true)
                            {
                                #region fill groups to administrative tools

                                mDvGroupsAccess.RowFilter = "moduleitemkey like '" + mModuleKey + "%'";
                                if (mDvGroupsAccess.Count > 0)
                                {
                                    NavBarGroup mNavBarGroup = mNavBarAdministrativeTools.Groups.Add();
                                    mNavBarGroup.Name = mModuleKey;
                                    mNavBarGroup.Caption = mModuleText;
                                    mNavBarGroup.LargeImageIndex = mLargeImageIndex;

                                    mDvGroupsAccess.Sort = "moduleitemkey";
                                    mDvModuleItems.RowFilter = "modulekey='" + mModuleKey.Trim() + "'";

                                    foreach (DataRowView mModuleItemRow in mDvModuleItems)
                                    {
                                        if (mDvGroupsAccess.Find(mModuleItemRow["moduleitemkey"].ToString().Trim()) >= 0)
                                        {
                                            NavBarItem mNavBarItem = new NavBarItem();
                                            mNavBarItem.Name = mModuleItemRow["moduleitemkey"].ToString().Trim();
                                            mNavBarItem.Caption = mModuleItemRow["moduleitemtext"].ToString().Trim();
                                            mNavBarItem.LargeImageIndex = mNavBarItem.SmallImageIndex = Convert.ToInt16(mModuleItemRow["iconindex"]);
                                            mNavBarGroup.ItemLinks.Add(mNavBarItem);
                                        }
                                    }
                                }

                                #endregion
                            }
                        }
                        else
                        {
                            #region data entry modules
                            mDvGroupsAccess.RowFilter = "moduleitemkey like '" + mModuleKey + "%'";

                            if (mDvGroupsAccess.Count > 0)
                            {
                                NavBarGroup mNavBarGroup = mNavBarControl.Groups.Add();
                                mNavBarGroup.Name = mModuleKey;
                                mNavBarGroup.Caption = mModuleText;
                                mNavBarGroup.LargeImageIndex = mLargeImageIndex;

                                mDvGroupsAccess.Sort = "moduleitemkey";
                                mDvModuleItems.RowFilter = "modulekey='" + mModuleKey.Trim() + "'";

                                foreach (DataRowView mModuleItemRow in mDvModuleItems)
                                {
                                    if (mDvGroupsAccess.Find(mModuleItemRow["moduleitemkey"].ToString().Trim()) >= 0)
                                    {
                                        NavBarItem mNavBarItem = new NavBarItem();
                                        mNavBarItem.Name = mModuleItemRow["moduleitemkey"].ToString().Trim();
                                        mNavBarItem.Caption = mModuleItemRow["moduleitemtext"].ToString().Trim();
                                        mNavBarItem.LargeImageIndex = mNavBarItem.SmallImageIndex = Convert.ToInt16(mModuleItemRow["iconindex"]);
                                        mNavBarGroup.ItemLinks.Add(mNavBarItem);
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }

        void mNavBarAdministrativeTools_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            mNavBarAdministrativeTools_ItemClicked(e.Link);
        }

        #endregion

        #region Fill_Reports
        internal void Fill_Reports(NavBarControl mNavBarControlMain, int mLargeImageIndex)
        {
            string mFunctionName = "Fill_Reports";

            try
            {
                #region register channel

                AfyaPro_MT.clsReportGroups mMdtReportGroups;
                mMdtReportGroups = (AfyaPro_MT.clsReportGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReportGroups),
                    Program.gMiddleTier + "clsReportGroups");

                AfyaPro_MT.clsReports mMdtReports;
                mMdtReports = (AfyaPro_MT.clsReports)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReports),
                    Program.gMiddleTier + "clsReports");

                AfyaPro_MT.clsUserGroups mMdtUserGroups;
                mMdtUserGroups = (AfyaPro_MT.clsUserGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsUserGroups),
                    Program.gMiddleTier + "clsUserGroups");

                #endregion

                NavBarGroupControlContainer mNavBarGroupControlContainer = new NavBarGroupControlContainer();
                NavBarControl mNavBarControl = new NavBarControl();
                mNavBarControl.LinkClicked += new NavBarLinkEventHandler(mNavBarControl_LinkClicked);

                NavBarGroup mNavBarGroup = mNavBarControlMain.Groups.Add();
                mNavBarGroup.Name = "reports";
                mNavBarGroup.Caption = "Reports";
                mNavBarGroup.LargeImageIndex = mLargeImageIndex;
                mNavBarControl.SmallImages = Program.gMdiForm.imgReports;

                mNavBarGroupControlContainer.Controls.Add(mNavBarControl);
                mNavBarControl.Dock = System.Windows.Forms.DockStyle.Fill;
                mNavBarGroup.ControlContainer = mNavBarGroupControlContainer;
                mNavBarGroup.GroupClientHeight = 80;
                mNavBarGroup.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;

                DataTable mDtGroupReports = mMdtUserGroups.Get_UserReports(
                    "usergroupcode='" + Program.gCurrentUser.UserGroupCode.Trim() + "'", "");

                DataTable mDtReportGroups = mMdtReportGroups.View("", "", "", "");
                DataTable mDtReports = mMdtReports.View("", "", "", "");

                DataView mDvReports = new DataView();
                mDvReports.Table = mDtReports;

                DataView mDvGroupReports = new DataView();
                mDvGroupReports.Table = mDtGroupReports;
                mDvGroupReports.Sort = "reportcode";

                foreach (DataRow mReportGroupRow in mDtReportGroups.Rows)
                {
                    string mGroupCode = mReportGroupRow["code"].ToString().Trim();
                    string mGroupDescription = mReportGroupRow["description"].ToString().Trim();

                    mDvReports.RowFilter = "groupcode='" + mGroupCode + "'";

                    if (mDvReports.Count > 0)
                    {
                        bool mHasAccess = false;
                        foreach (DataRowView mReportRow in mDvReports)
                        {
                            if (mDvGroupReports.Find(mReportRow["code"].ToString().Trim()) >= 0)
                            {
                                mHasAccess = true;
                                break;
                            }
                        }

                        if (mHasAccess == true)
                        {
                            NavBarGroup mCurrNavBarGroup = mNavBarControl.Groups.Add();
                            mCurrNavBarGroup.Name = mGroupCode;
                            mCurrNavBarGroup.Caption = mGroupDescription;

                            foreach (DataRowView mReportRow in mDvReports)
                            {
                                if (mDvGroupReports.Find(mReportRow["code"].ToString().Trim()) >= 0)
                                {
                                    NavBarItem mNavBarItem = new NavBarItem();
                                    mNavBarItem.Name = "report_" + mReportRow["code"].ToString().Trim();
                                    mNavBarItem.Caption = mReportRow["description"].ToString().Trim();
                                    mNavBarItem.LargeImageIndex = mNavBarItem.SmallImageIndex = 0;
                                    mCurrNavBarGroup.ItemLinks.Add(mNavBarItem);

                                    mHasAccess = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }

        void mNavBarControl_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            NavBarReports_ItemClicked(e.Link);
        }

        #endregion

        #region mNavBar_LinkClicked
        internal void mNavBar_LinkClicked(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            string mFunctionName = "mNavBar_LinkClicked";

            try
            {
                switch (mItemLink.ItemName.Trim().ToLower())
                {
                    case "outpatientdept0": this.Open_OPDRegistrations(mItemLink); break;
                    case "outpatientdept1": this.Open_OPDEditPatientDetails(mItemLink); break;
                    case "outpatientdept2": this.Open_OPDPatientDocuments(mItemLink); break;
                    case "outpatientdept3": this.Open_OPDPatientListing(mItemLink); break;

                    case "inpatientdept0": this.Open_IPDWards(mItemLink); break;
                    case "inpatientdept1": this.Open_IPDWardRooms(mItemLink); break;
                    case "inpatientdept2": this.Open_IPDWardRoomBeds(mItemLink); break;
                    case "inpatientdept3": this.Open_IPDDischargeStatus(mItemLink); break;
                    case "inpatientdept4": this.Open_IPDRegistrations(mItemLink); break;
                    case "inpatientdept5": this.Open_IPDEditPatientDetails(mItemLink); break;
                    case "inpatientdept6": this.Open_OPDPatientDocuments(mItemLink); break;
                    case "inpatientdept7": this.Open_IPDPatientListing(mItemLink); break;
                    case "inpatientdept8": this.Open_IPDTransfers(mItemLink); break;
                    case "inpatientdept9": this.Open_IPDDischarges(mItemLink); break;

                    case "diagnosesandtreatments0": this.Open_DXTGroups(mItemLink); break;
                    case "diagnosesandtreatments1": this.Open_DXTSubGroups(mItemLink); break;
                    case "diagnosesandtreatments2": this.Open_DXTDiagnoses(mItemLink); break;
                    case "diagnosesandtreatments3": this.Open_DXTEpisodes(mItemLink); break;
                    case "diagnosesandtreatments4": this.Open_DXTListing(mItemLink); break;
                    case "diagnosesandtreatments8": this.Open_DXTPrintPrescriptions(mItemLink); break;
                    //case "diagnosesandtreatments6": this.Open_DXTIndicators(mItemLink); break;
                    //case "diagnosesandtreatments7": this.Open_DXTIndicatorDiagnoses(mItemLink); break;

                    //Lab events
                    case "laboratory0": this.Open_LABLaboratories(mItemLink); break;
                    case "laboratory1": this.Open_LABLabTestGroups(mItemLink); break;
                    case "laboratory2": this.Open_LABLabTestSubGroups(mItemLink); break;
                    case "laboratory3": this.Open_LABLabTests(mItemLink); break;   
                    case "laboratory4": this.Open_LABRemarks(mItemLink); break;   
                    case "laboratory5": this.Open_LABvisit(mItemLink); break;    
                    case "laboratory6": this.Open_LABListing(mItemLink); break;

                    case "rch2": this.Open_RCHBirthMethods(mItemLink); break;
                    case "rch5": this.Open_RCHClients(mItemLink); break;
                    case "rch7": this.Open_RCHFamilyPlanning(mItemLink); break;
                    case "rch8": this.Open_RCHAntenatalCare(mItemLink); break;
                    case "rch9": this.Open_RCHPostnatalCare(mItemLink); break;
                    case "rch10": this.Open_RCHChildren(mItemLink); break;
                    case "rch11": this.Open_RCHVaccinations(mItemLink); break;
                    case "rch12": this.Open_RCHMaternity(mItemLink); break;

                    case "customers0": this.Open_CUSCustomerGroups(mItemLink); break;
                    case "customers1": this.Open_CUSCustomerSubGroups(mItemLink); break;
                    case "customers2": this.Open_CUSMembers(mItemLink); break;
                    case "customers3": this.Open_CUSImportMembers(mItemLink); break;
                    case "customers4": this.Open_CUSActivateDeActivate(mItemLink); break;

                    case "inventory0": this.Open_IVPurchaseOrders(mItemLink); break;
                    case "inventory1": this.Open_IVTransferIns(mItemLink); break;
                    case "inventory2": this.Open_IVTransferOuts(mItemLink); break;
                    case "inventory3": this.Open_IVPhysicalInventory(mItemLink); break;
                    case "inventory4": this.Open_IVIssuesHistory(mItemLink); break;
                    case "inventory5": this.Open_IVReceiptsHistory(mItemLink); break;

                    case "billing0": this.Open_BILDepositAccounts(mItemLink); break;
                    case "billing1": this.Open_BILDepositAccountMembers(mItemLink); break;
                    case "billing2": this.Open_BILBillPosting(mItemLink); break;
                    case "billing3": this.Open_BILPayBillsPatients(mItemLink); break;
                    case "billing4": this.Open_BILPayBillsGroups(mItemLink); break;
                    case "billing5": this.Open_BILSalesHistory(mItemLink); break;
                    case "billing6": this.Open_BILReceiptsHistory(mItemLink); break;
                    case "billing7": this.Open_BILInvoicesHistory(mItemLink); break;
                    case "billing8": this.Open_BILInvoicePaymentsHistory(mItemLink); break;
                    case "billing9": this.Open_BILVoidedSalesHistory(mItemLink); break;
                    case "billing10": this.Open_BILDebtReliefRequests(mItemLink); break;
                    case "billing11": this.Open_BILDebtReliefRequestsView(mItemLink); break;
                    case "billing12": this.Open_OPDChangeBillingGroup(mItemLink); break;

                    case "mtuha0": this.Open_MTUDiagnoses(mItemLink); break;
                    case "mtuha1": this.Open_MTUDiagnosesMapping(mItemLink); break;
                    case "mtuha2": this.Open_MTUDHISExport(mItemLink); break;

                    case "ctc0": this.Open_CTCRegistrations(mItemLink); break;
                    case "ctc1": this.Open_CTCEditPatientDetails(mItemLink); break;
                    case "ctc2": this.Open_CTCAppointments(mItemLink); break;
                    case "ctc3": this.Open_CTCMeasurements(mItemLink); break;
                    case "ctc4": this.Open_CTCHTC(mItemLink); break;
                    case "ctc5": this.Open_CTCPCR(mItemLink); break;
                    case "ctc6": this.Open_CTCCD4Tests(mItemLink); break;
                    case "ctc7": this.Open_PreART(mItemLink); break;
                    case "ctc8": this.Open_CTCART(mItemLink); break;
                    case "ctc9": this.Open_CTCARTP(mItemLink); break;
                    case "ctc10": this.Open_CTCExposed(mItemLink); break;
                    case "ctc11": this.Open_CTCEnrollInCare(mItemLink); break;
                    case "ctc12": this.Open_CTCEnrollInPMTCTANC(mItemLink); break;
                    case "ctc13": this.Open_CTCPMTCTLabourAndDelivery(mItemLink); break;
                    case "ctc14": this.Open_CTCPMTCTMotherChild(mItemLink); break;
                    case "ctc15": this.Open_CTCContactableClinicStaff(mItemLink); break;

                    case "mobilehealth0": this.Open_MessageTemplates(mItemLink); break;
                    case "mobilehealth1": this.Open_PatientCategories(mItemLink); break;
                    case "mobilehealth2": this.Open_SentMessage(mItemLink); break;
                    case "mobilehealth3": this.Open_ReceivedMessage(mItemLink); break;
                    case "mobilehealth4": this.Open_RegisteredPatients(mItemLink); break;
                    case "mobilehealth5": this.Open_ActivePatients(mItemLink); break;
                    case "mobilehealth6": this.Open_SMSAgents(mItemLink); break;
                    case "mobilehealth7": this.Open_Trash(mItemLink); break;
                    case "mobilehealth8": this.Open_SendSMS(mItemLink); break;
                    case "mobilehealth9": this.Open_ModuleSettings(mItemLink); break;
                    case "mobilehealth10": this.Open_PatientsList(mItemLink); break;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }

        #endregion

        #region mNavBarAdministrativeTools_ItemClicked
        private void mNavBarAdministrativeTools_ItemClicked(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            string mFunctionName = "mNavBarAdministrativeTools_ItemClicked";

            try
            {
                switch (mItemLink.ItemName.Trim().ToLower())
                {
                    case "generalsetup0": this.Open_GENFacilitySetup(mItemLink); break;
                    case "generalsetup1": this.Open_GENAutoCodes(mItemLink); break;
                    case "generalsetup2": this.Open_GENCountries(mItemLink); break;
                    case "generalsetup3": this.Open_GENRegions(mItemLink); break;
                    case "generalsetup4": this.Open_GENDistricts(mItemLink); break;
                    case "generalsetup5": this.Open_GENMedicalStaffs(mItemLink); break;
                    case "generalsetup6": this.Open_GENTreatmentPoints(mItemLink); break;
                    case "generalsetup7": this.Open_GENSearchEngine(mItemLink); break;
                    case "generalsetup8": this.Open_GENPatientDocuments(mItemLink); break;
                    case "generalsetup9": this.Open_GENPatientExtraFields(mItemLink); break;

                    case "inventorysetup0": this.Open_IVSStores(mItemLink); break;
                    case "inventorysetup1": this.Open_IVSProductCategories(mItemLink); break;
                    case "inventorysetup2": this.Open_IVSPackagings(mItemLink); break;
                    case "inventorysetup3": this.Open_IVSSourcesOfStock(mItemLink); break;
                    case "inventorysetup4": this.Open_IVSCustomers(mItemLink); break;
                    case "inventorysetup5": this.Open_IVSProducts(mItemLink); break;

                    case "billingsetup0": this.Open_BLSCurrencies(mItemLink); break;
                    case "billingsetup1": this.Open_BLSPaymentTypes(mItemLink); break;
                    case "billingsetup2": this.Open_BLSPriceCategories(mItemLink); break;
                    case "billingsetup3": this.Open_BLSItemGroups(mItemLink); break;
                    case "billingsetup4": this.Open_BLSItemSubGroups(mItemLink); break;
                    case "billingsetup5": this.Open_BLSItems(mItemLink); break;

                    case "securitysetup0": this.Open_SECUserGroups(mItemLink); break;
                    case "securitysetup1": this.Open_SECGroupsAccess(mItemLink); break;
                    case "securitysetup2": this.Open_SECReportsAccess(mItemLink); break;
                    case "securitysetup3": this.Open_SECUserGroupPrinters(mItemLink); break;
                    case "securitysetup4": this.Open_SECUsers(mItemLink); break;

                    case "reportdesigner0": this.Open_RPDReportGroups(mItemLink); break;
                    case "reportdesigner1": this.Open_RPDReports(mItemLink); break;

                    case "ctcsetup0": this.Open_CTCMaritalStatus(mItemLink); break;
                    case "ctcsetup1": this.Open_CTCFunctionalStatus(mItemLink); break;
                    case "ctcsetup2": this.Open_CTCTBStatus(mItemLink); break;
                    case "ctcsetup3": this.Open_CTCARVStatus(mItemLink); break;
                    case "ctcsetup4": this.Open_CTCAIDSIllness(mItemLink); break;
                    case "ctcsetup5": this.Open_CTCARVCombRegimens(mItemLink); break;
                    case "ctcsetup6": this.Open_CTCARVAdherence(mItemLink); break;
                    case "ctcsetup7": this.Open_CTCARVPoorAdherenceReasons(mItemLink); break;
                    case "ctcsetup8": this.Open_CTCReferedTo(mItemLink); break;
                    case "ctcsetup9": this.Open_CTCARVReasons(mItemLink); break;
                    case "ctcsetup10": this.Open_CTCFollowUpStatus(mItemLink); break;
                    case "ctcsetup11": this.Open_CTCReferedFrom(mItemLink); break;
                    case "ctcsetup12": this.Open_CTCPriorARVExposure(mItemLink); break;
                    case "ctcsetup13": this.Open_CTCARTWhyEligible(mItemLink); break;
                    case "ctcsetup14": this.Open_CTCRelevantComeds(mItemLink); break;
                    case "ctcsetup15": this.Open_CTCAbnormalLabResults(mItemLink); break;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region NavBarReports_ItemClicked
        private void NavBarReports_ItemClicked(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            string mFunctionName = "NavBarReports_ItemClicked";

            try
            {
                Open_ReportingForm(mItemLink);
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region New_Clicked
        internal static void New_Clicked()
        {
            if (Program.gMdiForm.ActiveMdiChild == null)
            {
                return;
            }

            switch (Program.gMdiForm.ActiveMdiChild.Name.Trim().ToLower())
            {
                #region generalsetup

                case "frmgencountries":
                    {
                        frmGENCountries mGENCountries = new frmGENCountries();
                        mGENCountries.gDataState = "New";
                        mGENCountries.ShowDialog();
                    }
                    break;
                case "frmgenregions":
                    {
                        frmGENRegions mGENRegions = new frmGENRegions();
                        mGENRegions.gDataState = "New";
                        mGENRegions.ShowDialog();
                    }
                    break;
                case "frmgendistricts":
                    {
                        frmGENDistricts mGENDistricts = new frmGENDistricts();
                        mGENDistricts.gDataState = "New";
                        mGENDistricts.ShowDialog();
                    }
                    break;
                case "frmgenmedicalstaffs":
                    {
                        frmGENMedicalStaffs mGENMedicalStaffs = new frmGENMedicalStaffs();
                        mGENMedicalStaffs.gDataState = "New";
                        mGENMedicalStaffs.ShowDialog();
                    }
                    break;
                case "frmgentreatmentpoints":
                    {
                        frmGENTreatmentPoints mGENTreatmentPoints = new frmGENTreatmentPoints();
                        mGENTreatmentPoints.gDataState = "New";
                        mGENTreatmentPoints.ShowDialog();
                    }
                    break;
                case "frmgenpatientdocuments":
                    {
                        frmGENPatientDocuments mGENPatientDocuments = new frmGENPatientDocuments();
                        mGENPatientDocuments.gDataState = "New";
                        mGENPatientDocuments.ShowDialog();
                    }
                    break;
                case "frmgenpatientfields":
                    {
                        frmGENPatientFields mGENPatientFields = new frmGENPatientFields();
                        mGENPatientFields.gDataState = "New";
                        mGENPatientFields.ShowDialog();
                    }
                    break;

                #endregion

                #region inventory

                case "frmivpurchaseordersview":
                    {
                        frmIVPurchaseOrders mIVPurchaseOrders = new frmIVPurchaseOrders();
                        mIVPurchaseOrders.gDataState = "New";
                        mIVPurchaseOrders.Show();
                    }
                    break;

                case "frmivtransferinsview":
                    {
                        //frmIVTransferInsView mIVTransferInsView = (frmIVTransferInsView)Program.gMdiForm.ActiveMdiChild;

                        //string mStoreCode = "";
                        //if (mIVTransferInsView.cboStore.ItemIndex != -1)
                        //{
                        //    mStoreCode = mIVTransferInsView.cboStore.GetColumnValue("code").ToString();
                        //}

                        //frmIVTransferInsStore mIVTransferInsStore = new frmIVTransferInsStore(mStoreCode);
                        //mIVTransferInsStore.gDataState = "New";
                        //mIVTransferInsStore.Show();
                        frmIVTransferInsSwitch mIVTransferInsSwitch = new frmIVTransferInsSwitch();
                        mIVTransferInsSwitch.ShowDialog();
                    }
                    break;

                case "frmivtransferoutsview":
                    {
                        //frmIVTransferOutsView mIVTransferOutsView = (frmIVTransferOutsView)Program.gMdiForm.ActiveMdiChild;

                        //string mStoreCode = "";
                        //if (mIVTransferOutsView.cboStore.ItemIndex != -1)
                        //{
                        //    mStoreCode = mIVTransferOutsView.cboStore.GetColumnValue("code").ToString();
                        //}

                        //frmIVTransferOutsStore mIVTransferOutsStore = new frmIVTransferOutsStore(mStoreCode);
                        //mIVTransferOutsStore.gDataState = "New";
                        //mIVTransferOutsStore.Show();
                        frmIVTransferOutsSwitch mIVTransferOutsSwitch = new frmIVTransferOutsSwitch();
                        mIVTransferOutsSwitch.ShowDialog();
                    }
                    break;

                case "frmivphysicalinventoryview":
                    {
                        frmIVPhysicalInventoryView mIVPhysicalInventoryView = (frmIVPhysicalInventoryView)Program.gMdiForm.ActiveMdiChild;

                        if (mIVPhysicalInventoryView.cboStore.ItemIndex == -1)
                        {
                            Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreDescriptionIsInvalid.ToString());
                            return;
                        }

                        frmIVPhysicalInventory mIVPhysicalInventory = 
                            new frmIVPhysicalInventory(mIVPhysicalInventoryView.cboStore.GetColumnValue("code").ToString());
                        mIVPhysicalInventory.gDataState = "New";
                        mIVPhysicalInventory.Show();
                    }
                    break;

                #endregion

                #region inventorysetup

                case "frmivsstores":
                    {
                        frmIVSStores mIVSStores = new frmIVSStores();
                        mIVSStores.gDataState = "New";
                        mIVSStores.ShowDialog();
                    }
                    break;
                case "frmivsproductcategories":
                    {
                        frmIVSProductCategories mIVSProductCategories = new frmIVSProductCategories();
                        mIVSProductCategories.gDataState = "New";
                        mIVSProductCategories.ShowDialog();
                    }
                    break;
                case "frmivspackagings":
                    {
                        frmIVSPackagings mIVSPackagings = new frmIVSPackagings();
                        mIVSPackagings.gDataState = "New";
                        mIVSPackagings.ShowDialog();
                    }
                    break;
                case "frmivssourcesofstock":
                    {
                        frmIVSSourcesOfStock mIVSSourcesOfStock = new frmIVSSourcesOfStock();
                        mIVSSourcesOfStock.gDataState = "New";
                        mIVSSourcesOfStock.ShowDialog();
                    }
                    break;
                case "frmivscustomers":
                    {
                        frmIVSCustomers mIVSCustomers = new frmIVSCustomers();
                        mIVSCustomers.gDataState = "New";
                        mIVSCustomers.ShowDialog();
                    }
                    break;
                case "frmivsproducts":
                    {
                        frmIVSProducts mIVSProducts = new frmIVSProducts();
                        mIVSProducts.gDataState = "New";
                        mIVSProducts.ShowDialog();
                    }
                    break;

                #endregion

                #region billing
                case "frmbildepositaccounts":
                    {
                        frmBILDepositAccounts mBILDepositAccounts = new frmBILDepositAccounts();
                        mBILDepositAccounts.gDataState = "New";
                        mBILDepositAccounts.ShowDialog();
                    }
                    break;
                case "frmbildepositaccountmembers":
                    {
                        frmBILDepositAccountMembers mBILDepositAccountMembers = new frmBILDepositAccountMembers();
                        mBILDepositAccountMembers.gDataState = "New";
                        mBILDepositAccountMembers.ShowDialog();
                    }
                    break;
                #endregion

                #region mtuha

                case "frmmtudiagnoses":
                    {
                        frmMTUDiagnoses mMTUDiagnoses = new frmMTUDiagnoses();
                        mMTUDiagnoses.gDataState = "New";
                        mMTUDiagnoses.ShowDialog();
                    }
                    break;

                #endregion

                #region billingsetup

                case "frmblscurrencies":
                    {
                        frmBLSCurrencies mBLSCurrencies = new frmBLSCurrencies();
                        mBLSCurrencies.gDataState = "New";
                        mBLSCurrencies.ShowDialog();
                    }
                    break;
                case "frmblspaymenttypes":
                    {
                        frmBLSPaymentTypes mBLSPaymentTypes = new frmBLSPaymentTypes();
                        mBLSPaymentTypes.gDataState = "New";
                        mBLSPaymentTypes.ShowDialog();
                    }
                    break;
                case "frmblsitemgroups":
                    {
                        frmBLSItemGroups mBLSItemGroups = new frmBLSItemGroups();
                        mBLSItemGroups.gDataState = "New";
                        mBLSItemGroups.ShowDialog();
                    }
                    break;
                case "frmblsitemsubgroups":
                    {
                        frmBLSItemSubGroups mBLSItemSubGroups = new frmBLSItemSubGroups();
                        mBLSItemSubGroups.gDataState = "New";
                        mBLSItemSubGroups.ShowDialog();
                    }
                    break;
                case "frmblsitems":
                    {
                        frmBLSItems mBLSItems = new frmBLSItems();
                        mBLSItems.gDataState = "New";
                        mBLSItems.ShowDialog();
                    }
                    break;

                #endregion

                #region customers

                case "frmcuscustomergroups":
                    {
                        frmCUSCustomerGroups mBLSCustomerGroups = new frmCUSCustomerGroups();
                        mBLSCustomerGroups.gDataState = "New";
                        mBLSCustomerGroups.ShowDialog();
                    }
                    break;
                case "frmcuscustomersubgroups":
                    {
                        frmCUSCustomerSubGroups mBLSCustomerSubGroups = new frmCUSCustomerSubGroups();
                        mBLSCustomerSubGroups.gDataState = "New";
                        mBLSCustomerSubGroups.ShowDialog();
                    }
                    break;
                case "frmcusmembers":
                    {
                        frmCUSMembers mCUSMembers = new frmCUSMembers();
                        mCUSMembers.gDataState = "New";
                        mCUSMembers.ShowDialog();
                    }
                    break;

                #endregion

                #region inpatientdept

                case "frmipdwards":
                    {
                        frmIPDWards mIPDWards = new frmIPDWards();
                        mIPDWards.gDataState = "New";
                        mIPDWards.ShowDialog();
                    }
                    break;
                case "frmipdwardrooms":
                    {
                        frmIPDWardRooms mIPDWardRooms = new frmIPDWardRooms();
                        mIPDWardRooms.gDataState = "New";
                        mIPDWardRooms.ShowDialog();
                    }
                    break;
                case "frmipdwardroombeds":
                    {
                        frmIPDWardRoomBeds mIPDWardRoomBeds = new frmIPDWardRoomBeds();
                        mIPDWardRoomBeds.gDataState = "New";
                        mIPDWardRoomBeds.ShowDialog();
                    }
                    break;
                case "frmipddischargestatus":
                    {
                        frmIPDDischargeStatus mIPDDischargeStatus = new frmIPDDischargeStatus();
                        mIPDDischargeStatus.gDataState = "New";
                        mIPDDischargeStatus.ShowDialog();
                    }
                    break;

                #endregion

                #region securitysetup

                case "frmsecusergroups":
                    {
                        frmSECUserGroups mSECUserGroups = new frmSECUserGroups();
                        mSECUserGroups.gDataState = "New";
                        mSECUserGroups.ShowDialog();
                    }
                    break;
                case "frmsecusers":
                    {
                        frmSECUsers mSECUsers = new frmSECUsers();
                        mSECUsers.gDataState = "New";
                        mSECUsers.ShowDialog();
                    }
                    break;

                #endregion

                #region reportdesigner

                case "frmrpdreportgroups":
                    {
                        frmRPDReportGroups mRPDReportGroups = new frmRPDReportGroups();
                        mRPDReportGroups.gDataState = "New";
                        mRPDReportGroups.ShowDialog();
                    }
                    break;
                case "frmrpdreports":
                    {
                        frmRPDReports mRPDReports = new frmRPDReports();
                        mRPDReports.gDataState = "New";
                        mRPDReports.ShowDialog();
                    }
                    break;

                #endregion

                #region diagnosesandtreatments

                case "frmdxtgroups":
                    {
                        frmDXTGroups mDXTGroups = new frmDXTGroups();
                        mDXTGroups.gDataState = "New";
                        mDXTGroups.ShowDialog();
                    }
                    break;
                case "frmdxtsubgroups":
                    {
                        frmDXTSubGroups mDXTSubGroups = new frmDXTSubGroups();
                        mDXTSubGroups.gDataState = "New";
                        mDXTSubGroups.ShowDialog();
                    }
                    break;
                case "frmdxtdiagnoses":
                    {
                        frmDXTDiagnoses mDXTDiagnoses = new frmDXTDiagnoses();
                        mDXTDiagnoses.gDataState = "New";
                        mDXTDiagnoses.ShowDialog();
                    }
                    break;
                case "frmdxtindicatorgroups":
                    {
                        frmDXTIndicatorGroups mDXTIndicatorGroups = new frmDXTIndicatorGroups();
                        mDXTIndicatorGroups.gDataState = "New";
                        mDXTIndicatorGroups.ShowDialog();
                    }
                    break;
                case "frmdxtindicators":
                    {
                        frmDXTIndicators mDXTIndicators = new frmDXTIndicators();
                        mDXTIndicators.gDataState = "New";
                        mDXTIndicators.ShowDialog();
                    }
                    break;

                #endregion

                #region laboratory
                case "frmlabpatienttests":
                    {
                        frmLABPatientTests mLabs = new frmLABPatientTests();
                        //mLabs.gDataState = "New";
                        mLabs.Show();

                    }
                    break;
                case "frmlablaboratories":
                    {
                        frmLABLaboratories mLABLaboratories = new frmLABLaboratories();
                        mLABLaboratories.gDataState = "New";
                        mLABLaboratories.ShowDialog();

                    }

                    break;

                case "frmlablabtestgroups":
                    {
                        frmLABLabTestGroups mLabtestgroups = new frmLABLabTestGroups();
                        mLabtestgroups.gDataState = "New";
                        mLabtestgroups.ShowDialog();
                    }
                    break;

                case "frmlablabtestsubgroups":
                    {
                        frmLABLabTestSubGroups mLABLabTestSubGroups = new frmLABLabTestSubGroups();
                        mLABLabTestSubGroups.gDataState = "New";
                        mLABLabTestSubGroups.ShowDialog();
                    }
                    break;

                case "frmlablabtests":
                    {
                        frmLABLabTests mLABtests = new frmLABLabTests();
                        mLABtests.gDataState = "New";
                        mLABtests.ShowDialog();
                    }
                    break;

                case "frmlabremarks":
                    {
                        frmLABRemarks mLABRemarks = new frmLABRemarks();
                        mLABRemarks.gDataState = "New";
                        mLABRemarks.ShowDialog();
                    }
                    break;
                #endregion

                #region ctc

                case "frmctcmaritalstatus":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.MaritalStatus);
                        mCTCCodes.gDataState = "New";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcfunctionalstatus":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.FunctionalStatus);
                        mCTCCodes.gDataState = "New";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctctbstatus":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.TBStatus);
                        mCTCCodes.gDataState = "New";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcarvstatus":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVStatus);
                        mCTCCodes.gDataState = "New";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcaidsillness":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.AIDSIllness);
                        mCTCCodes.gDataState = "New";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcarvcombregimens":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVCombRegimens);
                        mCTCCodes.gDataState = "New";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcarvadherence":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVAdherence);
                        mCTCCodes.gDataState = "New";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcarvpooradherencereasons":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVPoorAdherenceReasons);
                        mCTCCodes.gDataState = "New";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcreferedto":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ReferedTo);
                        mCTCCodes.gDataState = "New";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcarvreasons":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVReasons);
                        mCTCCodes.gDataState = "New";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcfollowupstatus":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.FollowUpStatus);
                        mCTCCodes.gDataState = "New";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcreferedfrom":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ReferedFrom);
                        mCTCCodes.gDataState = "New";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcpriorarvexposure":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.PriorARVExposure);
                        mCTCCodes.gDataState = "New";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcartwhyeligible":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARTWhyEligible);
                        mCTCCodes.gDataState = "New";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcrelevantcomeds":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.RelevantComeds);
                        mCTCCodes.gDataState = "New";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcabnormallabresults":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.abnormallabresults);
                        mCTCCodes.gDataState = "New";
                        mCTCCodes.ShowDialog();
                    }
                    break;

                #endregion

                #region rch

                case "frmrchbirthmethods":
                    {
                        frmRCHBirthMethods mRCHBirthMethods = new frmRCHBirthMethods();
                        mRCHBirthMethods.gDataState = "New";
                        mRCHBirthMethods.ShowDialog();
                    }
                   
                    break;

                #endregion

                   
                #region sms

                case "frmmessagetemplates":
                    {
                        DataGridViewRow mRow = new DataGridViewRow();

                        frmSMSMessageTemplates mSMSMessageTemplates = new frmSMSMessageTemplates(mRow, "New");
                        mSMSMessageTemplates.gDataState = "New";
                        mSMSMessageTemplates.ShowDialog();
                    }

                    break;
                case "frmsmsclientgroups":
                    {
                        DataGridViewRow mRow = new DataGridViewRow();

                        frmSMSGroups mSMSMessageTemplates = new frmSMSGroups(mRow, "");
                        mSMSMessageTemplates.gDataState = "New";
                        mSMSMessageTemplates.ShowDialog();
                    }
                    break;
                case "frmsmsmobilepatientslist":
                    {
                        DataGridViewRow mRow = new DataGridViewRow();

                        frmSMSPatientRegistration mSMSPatientsRegistrations = new frmSMSPatientRegistration();
                        //mSMSPatientsRegistrations.gDataState = "New";
                        mSMSPatientsRegistrations.ShowDialog();
                    }

                    break;
                case "frmsmsagents":
                    {
                        
                        DataGridViewRow mRow = new DataGridViewRow();

                        frmSMSAddAgent mSMSAddAgent= new frmSMSAddAgent(mRow, "New");
                        
                        mSMSAddAgent.ShowDialog();
                    }

                    break;

                #endregion

            }
        }
        #endregion
                
        #region Edit_Clicked
        internal static void Edit_Clicked()
        {
            if (Program.gMdiForm.ActiveMdiChild == null)
            {
                return;
            }

            DevExpress.XtraGrid.Views.Grid.GridView mGridView;
           

            if (Program.gMdiForm.ActiveMdiChild.Name.Trim().ToLower().ToString() == "frmmessagetemplates")
            {
                if (frmMessageTemplates.pRow == null)
                {
                    return;
                }

                frmSMSMessageTemplates mTpt = new frmSMSMessageTemplates(frmMessageTemplates.pRow, "Edit");
                mTpt.ShowDialog();

                return;
            }

            if (Program.gMdiForm.ActiveMdiChild.Name.Trim().ToLower().ToString() == "frmsmsclientgroups")
            {

                if (frmSMSClientGroups.pRow == null)
                {
                     
                    return;
                }

                frmSMSGroups mTpt = new frmSMSGroups(frmSMSClientGroups.pRow, "Edit");
                mTpt.ShowDialog();

                return;
            }

            if (Program.gMdiForm.ActiveMdiChild.Name.Trim().ToLower().ToString() == "frmsmsagents")
            {
                
                if (frmSMSAgents.pRow == null)
                {

                    return;
                }

                //MessageBox.Show(frmSMSAgents.pRow.Cells.Count.ToString());

                frmSMSAddAgent mTpt = new frmSMSAddAgent(frmSMSAgents.pRow, "Edit");
                mTpt.ShowDialog();

                return;
            }

            try
            {
                switch (Program.gMdiForm.ActiveMdiChild.Name.Trim().ToLower())
                {
                    case "frmivpurchaseordersview":
                        {
                            mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)((frmIVPurchaseOrdersView)Program.gMdiForm.ActiveMdiChild).grdIVPurchaseOrdersView.MainView;
                        }
                        break;
                    case "frmivtransferinsview":
                        {
                            mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)((frmIVTransferInsView)Program.gMdiForm.ActiveMdiChild).grdIVTransferInsView.MainView;
                        }
                        break;
                    case "frmivtransferoutsview":
                        {
                            mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)((frmIVTransferOutsView)Program.gMdiForm.ActiveMdiChild).grdIVTransferOutsView.MainView;
                        }
                        break;
                    case "frmivphysicalinventoryview":
                        {
                            mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)((frmIVPhysicalInventoryView)Program.gMdiForm.ActiveMdiChild).grdIVPhysicalInventoryView.MainView;
                        }
                        break;
                    
                    default:
                        {
                            mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain.MainView;
                        }
                        break;
                }

                if (mGridView.FocusedRowHandle < 0)
                {
                    Program.gMdiForm.Cursor = Cursors.Default;
                    return;
                }
            }
            catch
            {
                Program.gMdiForm.Cursor = Cursors.Default;
                return;
            }

            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            switch (Program.gMdiForm.ActiveMdiChild.Name.Trim().ToLower())
            {
                #region generalsetup

                case "frmgencountries":
                    {
                        frmGENCountries mGENCountries = new frmGENCountries();
                        mGENCountries.gDataState = "Edit";
                        mGENCountries.ShowDialog();
                    }
                    break;
                case "frmgenregions":
                    {
                        frmGENRegions mGENRegions = new frmGENRegions();
                        mGENRegions.gDataState = "Edit";
                        mGENRegions.ShowDialog();
                    }
                    break;
                case "frmgendistricts":
                    {
                        frmGENDistricts mGENDistricts = new frmGENDistricts();
                        mGENDistricts.gDataState = "Edit";
                        mGENDistricts.ShowDialog();
                    }
                    break;
                case "frmgenmedicalstaffs":
                    {
                        frmGENMedicalStaffs mGENMedicalStaffs = new frmGENMedicalStaffs();
                        mGENMedicalStaffs.gDataState = "Edit";
                        mGENMedicalStaffs.ShowDialog();
                    }
                    break;
                case "frmgentreatmentpoints":
                    {
                        frmGENTreatmentPoints mGENTreatmentPoints = new frmGENTreatmentPoints();
                        mGENTreatmentPoints.gDataState = "Edit";
                        mGENTreatmentPoints.ShowDialog();
                    }
                    break;
                case "frmgenautocodes":
                    {
                        frmGENAutoCodes mGENAutoCodes = new frmGENAutoCodes();
                        mGENAutoCodes.Mode_Edit();
                        mGENAutoCodes.ShowDialog();
                    }
                    break;
                case "frmgenpatientdocuments":
                    {
                        frmGENPatientDocuments mGENPatientDocuments = new frmGENPatientDocuments();
                        mGENPatientDocuments.gDataState = "Edit";
                        mGENPatientDocuments.ShowDialog();
                    }
                    break;
                case "frmgenpatientfields":
                    {
                        frmGENPatientFields mGENPatientFields = new frmGENPatientFields();
                        mGENPatientFields.gDataState = "Edit";
                        mGENPatientFields.ShowDialog();
                    }
                    break;

                #endregion

                #region inventorysetup

                case "frmivsstores":
                    {
                        frmIVSStores mIVSStores = new frmIVSStores();
                        mIVSStores.gDataState = "Edit";
                        mIVSStores.ShowDialog();
                    }
                    break;
                case "frmivsproductcategories":
                    {
                        frmIVSProductCategories mIVSProductCategories = new frmIVSProductCategories();
                        mIVSProductCategories.gDataState = "Edit";
                        mIVSProductCategories.ShowDialog();
                    }
                    break;
                case "frmivspackagings":
                    {
                        frmIVSPackagings mIVSPackagings = new frmIVSPackagings();
                        mIVSPackagings.gDataState = "Edit";
                        mIVSPackagings.ShowDialog();
                    }
                    break;
                case "frmivssourcesofstock":
                    {
                        frmIVSSourcesOfStock mIVSSourcesOfStock = new frmIVSSourcesOfStock();
                        mIVSSourcesOfStock.gDataState = "Edit";
                        mIVSSourcesOfStock.ShowDialog();
                    }
                    break;
                case "frmivscustomers":
                    {
                        frmIVSCustomers mIVSCustomers = new frmIVSCustomers();
                        mIVSCustomers.gDataState = "Edit";
                        mIVSCustomers.ShowDialog();
                    }
                    break;
                case "frmivsproducts":
                    {
                        frmIVSProducts mIVSProducts = new frmIVSProducts();
                        mIVSProducts.gDataState = "Edit";
                        mIVSProducts.ShowDialog();
                    }
                    break;

                #endregion

                #region inventory

                case "frmivpurchaseordersview":
                    {
                        frmIVPurchaseOrders mIVPurchaseOrders = new frmIVPurchaseOrders();
                        mIVPurchaseOrders.gDataState = "Edit";
                        mIVPurchaseOrders.Show();
                    }
                    break;

                case "frmivtransferinsview":
                    {
                        DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                        if (mSelectedRow["transfertype"].ToString().Trim().ToLower() ==
                            AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString().Trim().ToLower())
                        {
                            frmIVTransferInsView mIVTransferInsView = (frmIVTransferInsView)Program.gMdiForm.ActiveMdiChild;

                            string mStoreCode = "";
                            if (mIVTransferInsView.cboStore.ItemIndex != -1)
                            {
                                mStoreCode = mIVTransferInsView.cboStore.GetColumnValue("code").ToString();
                            }

                            frmIVTransferInsStore mIVTransferInsStore = new frmIVTransferInsStore(mStoreCode);
                            mIVTransferInsStore.TransferOutNo = mSelectedRow["transferoutno"].ToString().Trim();
                            mIVTransferInsStore.gDataState = "Edit";
                            mIVTransferInsStore.Show();
                        }
                        else
                        {
                            frmIVTransferInsView mIVTransferInsView = (frmIVTransferInsView)Program.gMdiForm.ActiveMdiChild;

                            string mStoreCode = "";
                            if (mIVTransferInsView.cboStore.ItemIndex != -1)
                            {
                                mStoreCode = mIVTransferInsView.cboStore.GetColumnValue("code").ToString();
                            }

                            frmIVTransferIns mIVTransferIns = new frmIVTransferIns(mStoreCode);
                            mIVTransferIns.gDataState = "Edit";
                            mIVTransferIns.Show();
                        }
                    }
                    break;

                case "frmivtransferoutsview":
                    {
                        DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                        if (mSelectedRow["transfertype"].ToString().Trim().ToLower() ==
                            AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString().Trim().ToLower())
                        {
                            frmIVTransferOutsView mIVTransferOutsView = (frmIVTransferOutsView)Program.gMdiForm.ActiveMdiChild;

                            string mStoreCode = "";
                            if (mIVTransferOutsView.cboStore.ItemIndex != -1)
                            {
                                mStoreCode = mIVTransferOutsView.cboStore.GetColumnValue("code").ToString();
                            }

                            frmIVTransferOutsStore mIVTransferOutsStore = new frmIVTransferOutsStore(mStoreCode);
                            mIVTransferOutsStore.TransferInNo = mSelectedRow["transferinno"].ToString().Trim();
                            mIVTransferOutsStore.gDataState = "Edit";
                            mIVTransferOutsStore.Show();
                        }
                        else
                        {
                            frmIVTransferOutsView mIVTransferOutsView = (frmIVTransferOutsView)Program.gMdiForm.ActiveMdiChild;

                            string mStoreCode = "";
                            if (mIVTransferOutsView.cboStore.ItemIndex != -1)
                            {
                                mStoreCode = mIVTransferOutsView.cboStore.GetColumnValue("code").ToString();
                            }

                            frmIVTransferOuts mIVTransferOuts = new frmIVTransferOuts(mStoreCode);
                            mIVTransferOuts.gDataState = "Edit";
                            mIVTransferOuts.Show();
                        }
                    }
                    break;

                case "frmivphysicalinventoryview":
                    {
                        frmIVPhysicalInventoryView mIVPhysicalInventoryView = (frmIVPhysicalInventoryView)Program.gMdiForm.ActiveMdiChild;

                        string mStoreCode = "";
                        if (mIVPhysicalInventoryView.cboStore.ItemIndex != -1)
                        {
                            mStoreCode = mIVPhysicalInventoryView.cboStore.GetColumnValue("code").ToString();
                        }
                        frmIVPhysicalInventory mIVPhysicalInventory = new frmIVPhysicalInventory(mStoreCode);
                        mIVPhysicalInventory.gDataState = "Edit";
                        mIVPhysicalInventory.Show();
                    }
                    break;

                #endregion

                #region billing
                case "frmbildepositaccounts":
                    {
                        frmBILDepositAccounts mBILDepositAccounts = new frmBILDepositAccounts();
                        mBILDepositAccounts.gDataState = "Edit";
                        mBILDepositAccounts.ShowDialog();
                    }
                    break;
                #endregion

                #region mtuha

                case "frmmtudiagnoses":
                    {
                        frmMTUDiagnoses mMTUDiagnoses = new frmMTUDiagnoses();
                        mMTUDiagnoses.gDataState = "Edit";
                        mMTUDiagnoses.ShowDialog();
                    }
                    break;
                case "frmmtudiagnosesmapping":
                    {
                        frmMTUDiagnosesMapping mMTUDiagnosesMapping = new frmMTUDiagnosesMapping();
                        mMTUDiagnosesMapping.ShowDialog();
                    }
                    break;

                #endregion

                #region billingsetup

                case "frmblscurrencies":
                    {
                        frmBLSCurrencies mBLSCurrencies = new frmBLSCurrencies();
                        mBLSCurrencies.gDataState = "Edit";
                        mBLSCurrencies.ShowDialog();
                    }
                    break;
                case "frmblspaymenttypes":
                    {
                        frmBLSPaymentTypes mBLSPaymentTypes = new frmBLSPaymentTypes();
                        mBLSPaymentTypes.gDataState = "Edit";
                        mBLSPaymentTypes.ShowDialog();
                    }
                    break;
                case "frmblsitemgroups":
                    {
                        frmBLSItemGroups mBLSItemGroups = new frmBLSItemGroups();
                        mBLSItemGroups.gDataState = "Edit";
                        mBLSItemGroups.ShowDialog();
                    }
                    break;
                case "frmblsitemsubgroups":
                    {
                        frmBLSItemSubGroups mBLSItemSubGroups = new frmBLSItemSubGroups();
                        mBLSItemSubGroups.gDataState = "Edit";
                        mBLSItemSubGroups.ShowDialog();
                    }
                    break;
                case "frmblsitems":
                    {
                        frmBLSItems mBLSItems = new frmBLSItems();
                        mBLSItems.gDataState = "Edit";
                        mBLSItems.ShowDialog();
                    }
                    break;

                #endregion

                #region customers

                case "frmcuscustomergroups":
                    {
                        frmCUSCustomerGroups mBLSCustomerGroups = new frmCUSCustomerGroups();
                        mBLSCustomerGroups.gDataState = "Edit";
                        mBLSCustomerGroups.ShowDialog();
                    }
                    break;
                case "frmcuscustomersubgroups":
                    {
                        frmCUSCustomerSubGroups mBLSCustomerSubGroups = new frmCUSCustomerSubGroups();
                        mBLSCustomerSubGroups.gDataState = "Edit";
                        mBLSCustomerSubGroups.ShowDialog();
                    }
                    break;
                case "frmcusmembers":
                    {
                        frmCUSMembers mCUSMembers = new frmCUSMembers();
                        mCUSMembers.gDataState = "Edit";
                        mCUSMembers.ShowDialog();
                    }
                    break;

                #endregion

                #region inpatientdept

                case "frmipdwards":
                    {
                        frmIPDWards mIPDWards = new frmIPDWards();
                        mIPDWards.gDataState = "Edit";
                        mIPDWards.ShowDialog();
                    }
                    break;
                case "frmipdwardrooms":
                    {
                        frmIPDWardRooms mIPDWardRooms = new frmIPDWardRooms();
                        mIPDWardRooms.gDataState = "Edit";
                        mIPDWardRooms.ShowDialog();
                    }
                    break;
                case "frmipdwardroombeds":
                    {
                        frmIPDWardRoomBeds mIPDWardRoomBeds = new frmIPDWardRoomBeds();
                        mIPDWardRoomBeds.gDataState = "Edit";
                        mIPDWardRoomBeds.ShowDialog();
                    }
                    break;
                case "frmipddischargestatus":
                    {
                        frmIPDDischargeStatus mIPDDischargeStatus = new frmIPDDischargeStatus();
                        mIPDDischargeStatus.gDataState = "Edit";
                        mIPDDischargeStatus.ShowDialog();
                    }
                    break;

                #endregion

                #region securitysetup

                case "frmsecusergroups":
                    {
                        frmSECUserGroups mSECUserGroups = new frmSECUserGroups();
                        mSECUserGroups.gDataState = "Edit";
                        mSECUserGroups.ShowDialog();
                    }
                    break;
                case "frmsecusers":
                    {
                        frmSECUsers mSECUsers = new frmSECUsers();
                        mSECUsers.gDataState = "Edit";
                        mSECUsers.ShowDialog();
                    }
                    break;

                #endregion

                #region reportdesigner

                case "frmrpdreportgroups":
                    {
                        frmRPDReportGroups mRPDReportGroups = new frmRPDReportGroups();
                        mRPDReportGroups.gDataState = "Edit";
                        mRPDReportGroups.ShowDialog();
                    }
                    break;
                case "frmrpdreports":
                    {
                        mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain.MainView;

                        if (mGridView.FocusedRowHandle < 0)
                        {
                            return;
                        }

                        DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);
                        Type mReports = typeof(AfyaPro_Types.clsEnums.BuiltInReports);
                        bool mIsBuiltIn = false;
                        foreach (int mReportCode in Enum.GetValues(mReports))
                        {
                            if (mSelectedRow["code"].ToString().Trim().ToLower() == "rep" + mReportCode)
                            {
                                mIsBuiltIn = true;
                                break;
                            }
                        }

                        if (mIsBuiltIn == true)
                        {
                            frmRPDEditReport mRPDEditReport = new frmRPDEditReport();
                            mRPDEditReport.ShowDialog();
                        }
                        else
                        {
                            frmRPDReports mRPDReports = new frmRPDReports();
                            mRPDReports.gDataState = "Edit";
                            mRPDReports.ShowDialog();
                        }
                    }
                    break;

                #endregion

                #region diagnosesandtreatments

                case "frmdxtgroups":
                    {
                        frmDXTGroups mDXTGroups = new frmDXTGroups();
                        mDXTGroups.gDataState = "Edit";
                        mDXTGroups.ShowDialog();
                    }
                    break;
                case "frmdxtsubgroups":
                    {
                        frmDXTSubGroups mDXTSubGroups = new frmDXTSubGroups();
                        mDXTSubGroups.gDataState = "Edit";
                        mDXTSubGroups.ShowDialog();
                    }
                    break;
                case "frmdxtdiagnoses":
                    {
                        frmDXTDiagnoses mDXTDiagnoses = new frmDXTDiagnoses();
                        mDXTDiagnoses.gDataState = "Edit";
                        mDXTDiagnoses.ShowDialog();
                    }
                    break;
                case "frmdxtindicatorgroups":
                    {
                        frmDXTIndicatorGroups mDXTIndicatorGroups = new frmDXTIndicatorGroups();
                        mDXTIndicatorGroups.gDataState = "Edit";
                        mDXTIndicatorGroups.ShowDialog();
                    }
                    break;
                case "frmdxtindicators":
                    {
                        frmDXTIndicators mDXTIndicators = new frmDXTIndicators();
                        mDXTIndicators.gDataState = "Edit";
                        mDXTIndicators.ShowDialog();
                    }
                    break;

                #endregion

                #region laboratory

                case "frmlablaboratories":
                    {
                        frmLABLaboratories mLABs = new frmLABLaboratories();
                        mLABs.gDataState = "Edit";
                        mLABs.ShowDialog();

                    }

                    break;

                case "frmlablabtestgroups":
                    {
                        frmLABLabTestGroups mLABtestgroups = new frmLABLabTestGroups();
                        mLABtestgroups.gDataState = "Edit";
                        mLABtestgroups.ShowDialog();

                    }
                    break;

                case "frmlablabtestsubgroups":
                    {
                        frmLABLabTestSubGroups mLABLabTestSubGroups = new frmLABLabTestSubGroups();
                        mLABLabTestSubGroups.gDataState = "Edit";
                        mLABLabTestSubGroups.ShowDialog();
                    }
                    break;

                case "frmlablabtests":
                    {
                        frmLABLabTests mLABtests = new frmLABLabTests();
                        mLABtests.gDataState = "Edit";
                        mLABtests.ShowDialog();
                    }
                    break;

                #endregion

                #region ctc

                case "frmctcmaritalstatus":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.MaritalStatus);
                        mCTCCodes.gDataState = "Edit";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcfunctionalstatus":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.FunctionalStatus);
                        mCTCCodes.gDataState = "Edit";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctctbstatus":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.TBStatus);
                        mCTCCodes.gDataState = "Edit";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcarvstatus":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVStatus);
                        mCTCCodes.gDataState = "Edit";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcaidsillness":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.AIDSIllness);
                        mCTCCodes.gDataState = "Edit";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcarvcombregimens":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVCombRegimens);
                        mCTCCodes.gDataState = "Edit";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcarvadherence":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVAdherence);
                        mCTCCodes.gDataState = "Edit";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcarvpooradherencereasons":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVPoorAdherenceReasons);
                        mCTCCodes.gDataState = "Edit";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcreferedto":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ReferedTo);
                        mCTCCodes.gDataState = "Edit";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcarvreasons":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVReasons);
                        mCTCCodes.gDataState = "Edit";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcfollowupstatus":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.FollowUpStatus);
                        mCTCCodes.gDataState = "Edit";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcreferedfrom":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ReferedFrom);
                        mCTCCodes.gDataState = "Edit";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcpriorarvexposure":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.PriorARVExposure);
                        mCTCCodes.gDataState = "Edit";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcartwhyeligible":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARTWhyEligible);
                        mCTCCodes.gDataState = "Edit";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcrelevantcomeds":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.RelevantComeds);
                        mCTCCodes.gDataState = "Edit";
                        mCTCCodes.ShowDialog();
                    }
                    break;
                case "frmctcabnormallabresults":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.abnormallabresults);
                        mCTCCodes.gDataState = "Edit";
                        mCTCCodes.ShowDialog();
                    }
                    break;

                #endregion

                #region sms

                

                #endregion
            }

            Program.gMdiForm.Cursor = Cursors.Default;
        }
        #endregion

        #region Delete_Clicked
        internal static void Delete_Clicked()
        {
           
            if (Program.gMdiForm.ActiveMdiChild == null)
            {
                return;
            }
            
            try
            {
                if ((Program.gMdiForm.ActiveMdiChild.Name.Trim().ToLower().ToString()) == "frmmessagetemplates")
                {

                    if (frmMessageTemplates.pRow ==null)
                    {
                        return;
                    }
                    frmSMSMessageTemplates mTpt = new frmSMSMessageTemplates(frmMessageTemplates.pRow, "");
                    mTpt.Data_Delete(frmMessageTemplates.pRow);
                    return;
                }
            }
            catch
            {
            }

            try
            {
                if ((Program.gMdiForm.ActiveMdiChild.Name.Trim().ToLower().ToString()) == "frmsmsclientgroups")
                {

                    if (frmSMSClientGroups.pRow ==null)
                    {
                        return;
                    }
                    frmSMSGroups mTmpt = new frmSMSGroups(frmSMSClientGroups.pRow, "");
                    mTmpt.Data_Delete(frmSMSClientGroups.pRow);
                    return;
                }
            }
            catch
            {
            }

            try
            {
                if ((Program.gMdiForm.ActiveMdiChild.Name.Trim().ToLower().ToString()) == "frmsentmessages")
                {


                    if (frmSMSSentMessages.pRow == null)
                    {
                        return;
                    }
                    frmSMSSentMessages mTmpt = new frmSMSSentMessages();
                    mTmpt.Data_Delete(frmSMSSentMessages.pRow);
                    return;
                }
            }
            catch
            {
            }

            try
            {
                if ((Program.gMdiForm.ActiveMdiChild.Name.Trim().ToLower().ToString()) == "frmreceivedmessages")
                {


                    if (frmSMSReceivedMessages.pRow == null)
                    {
                        return;
                    }
                    frmSMSReceivedMessages mTmpt = new frmSMSReceivedMessages();
                    mTmpt.Data_Delete(frmSMSReceivedMessages.pRow);
                    return;
                }
            }
            catch
            {
            }

            try
            {
                if ((Program.gMdiForm.ActiveMdiChild.Name.Trim().ToLower().ToString()) == "frmsmsagents")
                {


                    if (frmSMSAgents.pRow == null)
                    {
                        return;
                    }
                    frmSMSAgents mTmpt = new frmSMSAgents();
                    mTmpt.Data_Delete(frmSMSAgents.pRow.Cells[0].Value.ToString());
                    return;
                }
            }
            catch
            {
            }

            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    Program.gMdiForm.Cursor = Cursors.Default;
                    return;
                }
            }
            catch
            {
                Program.gMdiForm.Cursor = Cursors.Default;
                return;
            }


           
               
               

            switch (Program.gMdiForm.ActiveMdiChild.Name.Trim().ToLower())
            {
                #region generalsetup

                case "frmgencountries":
                    {
                        frmGENCountries mGENCountries = new frmGENCountries();
                        mGENCountries.Data_Delete();
                    }
                    break;
                case "frmgenregions":
                    {
                        frmGENRegions mGENRegions = new frmGENRegions();
                        mGENRegions.Data_Delete();
                    }
                    break;
                case "frmgendistricts":
                    {
                        frmGENDistricts mGENDistricts = new frmGENDistricts();
                        mGENDistricts.Data_Delete();
                    }
                    break;
                case "frmgenmedicalstaffs":
                    {
                        frmGENMedicalStaffs mGENMedicalStaffs = new frmGENMedicalStaffs();
                        mGENMedicalStaffs.Data_Delete();
                    }
                    break;
                case "frmgentreatmentpoints":
                    {
                        frmGENTreatmentPoints mGENTreatmentPoints = new frmGENTreatmentPoints();
                        mGENTreatmentPoints.Data_Delete();
                    }
                    break;
                case "frmgenpatientdocuments":
                    {
                        frmGENPatientDocuments mGENPatientDocuments = new frmGENPatientDocuments();
                        mGENPatientDocuments.Data_Delete();
                    }
                    break;
                case "frmgenpatientfields":
                    {
                        frmGENPatientFields mGENPatientFields = new frmGENPatientFields();
                        mGENPatientFields.Data_Delete();
                    }
                    break;

                #endregion

                #region inventorysetup

                case "frmivsstores":
                    {
                        frmIVSStores mIVSStores = new frmIVSStores();
                        mIVSStores.Data_Delete();
                    }
                    break;
                case "frmivsproductcategories":
                    {
                        frmIVSProductCategories mIVSProductCategories = new frmIVSProductCategories();
                        mIVSProductCategories.Data_Delete();
                    }
                    break;
                case "frmivspackagings":
                    {
                        frmIVSPackagings mIVSPackagings = new frmIVSPackagings();
                        mIVSPackagings.Data_Delete();
                    }
                    break;
                case "frmivssourcesofstock":
                    {
                        frmIVSSourcesOfStock mIVSSourcesOfStock = new frmIVSSourcesOfStock();
                        mIVSSourcesOfStock.Data_Delete();
                    }
                    break;
                case "frmivscustomers":
                    {
                        frmIVSCustomers mIVSCustomers = new frmIVSCustomers();
                        mIVSCustomers.Data_Delete();
                    }
                    break;
                case "frmivsproducts":
                    {
                        frmIVSProducts mIVSProducts = new frmIVSProducts();
                        mIVSProducts.Data_Delete();
                    }
                    break;

                #endregion

                #region billing
                case "frmbildepositaccounts":
                    {
                        /********Uncomment the lines below to allow deletion of account*******/
                        //frmBILDepositAccounts mBILDepositAccounts = new frmBILDepositAccounts();
                        //mBILDepositAccounts.Data_Delete();
                    }
                    break;
                case "frmbildepositaccountmembers":
                    {
                        frmBILDepositAccountMembers mBILDepositAccountMembers = new frmBILDepositAccountMembers();
                        mBILDepositAccountMembers.Data_Delete();
                    }
                    break;
                #endregion

                #region billing

                case "frmmtudiagnoses":
                    {
                        frmMTUDiagnoses mMTUDiagnoses = new frmMTUDiagnoses();
                        mMTUDiagnoses.Data_Delete();
                    }
                    break;

                #endregion

                #region billingsetup

                case "frmblscurrencies":
                    {
                        frmBLSCurrencies mBLSCurrencies = new frmBLSCurrencies();
                        mBLSCurrencies.Data_Delete();
                    }
                    break;
                case "frmblspaymenttypes":
                    {
                        frmBLSPaymentTypes mBLSPaymentTypes = new frmBLSPaymentTypes();
                        mBLSPaymentTypes.Data_Delete();
                    }
                    break;
                case "frmblsitemgroups":
                    {
                        frmBLSItemGroups mBLSItemGroups = new frmBLSItemGroups();
                        mBLSItemGroups.Data_Delete();
                    }
                    break;
                case "frmblsitemsubgroups":
                    {
                        frmBLSItemSubGroups mBLSItemSubGroups = new frmBLSItemSubGroups();
                        mBLSItemSubGroups.Data_Delete();
                    }
                    break;
                case "frmblsitems":
                    {
                        frmBLSItems mBLSItems = new frmBLSItems();
                        mBLSItems.Data_Delete();
                    }
                    break;

                #endregion

                #region customers

                case "frmcuscustomergroups":
                    {
                        frmCUSCustomerGroups mBLSCustomerGroups = new frmCUSCustomerGroups();
                        mBLSCustomerGroups.Data_Delete();
                    }
                    break;
                case "frmcuscustomersubgroups":
                    {
                        frmCUSCustomerSubGroups mBLSCustomerSubGroups = new frmCUSCustomerSubGroups();
                        mBLSCustomerSubGroups.Data_Delete();
                    }
                    break;
                case "frmcusmembers":
                    {
                        frmCUSMembers mCUSMembers = new frmCUSMembers();
                        mCUSMembers.Data_Delete();
                    }
                    break;

                #endregion

                #region inpatientdept

                case "frmipdwards":
                    {
                        frmIPDWards mIPDWards = new frmIPDWards();
                        mIPDWards.Data_Delete();
                    }
                    break;
                case "frmipdwardrooms":
                    {
                        frmIPDWardRooms mIPDWardRooms = new frmIPDWardRooms();
                        mIPDWardRooms.Data_Delete();
                    }
                    break;
                case "frmipdwardroombeds":
                    {
                        frmIPDWardRoomBeds mIPDWardRoomBeds = new frmIPDWardRoomBeds();
                        mIPDWardRoomBeds.Data_Delete();
                    }
                    break;
                case "frmipddischargestatus":
                    {
                        frmIPDDischargeStatus mIPDDischargeStatus = new frmIPDDischargeStatus();
                        mIPDDischargeStatus.Data_Delete();
                    }
                    break;

                #endregion

                #region securitysetup

                case "frmsecusergroups":
                    {
                        frmSECUserGroups mSECUserGroups = new frmSECUserGroups();
                        mSECUserGroups.Data_Delete();
                    }
                    break;
                case "frmsecusers":
                    {
                        frmSECUsers mSECUsers = new frmSECUsers();
                        mSECUsers.Data_Delete();
                    }
                    break;

                #endregion

                #region reportdesigner

                case "frmrpdreportgroups":
                    {
                        frmRPDReportGroups mRPDReportGroups = new frmRPDReportGroups();
                        mRPDReportGroups.Data_Delete();
                    }
                    break;
                case "frmrpdreports":
                    {
                        frmRPDReports mRPDReports = new frmRPDReports();
                        mRPDReports.Data_Delete();
                    }
                    break;

                #endregion

                #region diagnosesandtreatments

                case "frmdxtgroups":
                    {
                        frmDXTGroups mDXTGroups = new frmDXTGroups();
                        mDXTGroups.Data_Delete();
                    }
                    break;
                case "frmdxtsubgroups":
                    {
                        frmDXTSubGroups mDXTSubGroups = new frmDXTSubGroups();
                        mDXTSubGroups.Data_Delete();
                    }
                    break;
                case "frmdxtdiagnoses":
                    {
                        frmDXTDiagnoses mDXTDiagnoses = new frmDXTDiagnoses();
                        mDXTDiagnoses.Data_Delete();
                    }
                    break;
                case "frmdxtindicatorgroups":
                    {
                        frmDXTIndicatorGroups mDXTIndicatorGroups = new frmDXTIndicatorGroups();
                        mDXTIndicatorGroups.Data_Delete();
                    }
                    break;
                case "frmdxtindicators":
                    {
                        frmDXTIndicators mDXTIndicators = new frmDXTIndicators();
                        mDXTIndicators.Data_Delete();
                    }
                    break;

                #endregion

                #region laboritory
                case "frmlablaboratories":
                    {
                        frmLABLaboratories mLabs = new frmLABLaboratories();
                        mLabs.Data_Delete();
                    }

                    break;

                case "frmlablabtestgroups":
                    {
                        frmLABLabTestGroups mLABtestgroup = new frmLABLabTestGroups();
                        mLABtestgroup.Data_Delete();
                    }
                    break;

                case "frmlablabtestsubgroups":
                    {
                        frmLABLabTestSubGroups mLABLabTestSubGroups = new frmLABLabTestSubGroups();
                        mLABLabTestSubGroups.Data_Delete();
                    }
                    break;

                case "frmlablabtests":
                    {
                        frmLABLabTests mLABtests = new frmLABLabTests();
                        mLABtests.Data_Delete();
                    }
                    break;

                case "frmlabremarks":
                    {
                        frmLABRemarks mLABRemarks = new frmLABRemarks();
                        mLABRemarks.Data_Delete();
                    }
                    break;
                #endregion

                #region ctc

                case "frmctcmaritalstatus":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.MaritalStatus);
                        mCTCCodes.Data_Delete();
                    }
                    break;
                case "frmctcfunctionalstatus":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.FunctionalStatus);
                        mCTCCodes.Data_Delete();
                    }
                    break;
                case "frmctctbstatus":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.TBStatus);
                        mCTCCodes.Data_Delete();
                    }
                    break;
                case "frmctcarvstatus":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVStatus);
                        mCTCCodes.Data_Delete();
                    }
                    break;
                case "frmctcaidsillness":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.AIDSIllness);
                        mCTCCodes.Data_Delete();
                    }
                    break;
                case "frmctcarvcombregimens":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVCombRegimens);
                        mCTCCodes.Data_Delete();
                    }
                    break;
                case "frmctcarvadherence":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVAdherence);
                        mCTCCodes.Data_Delete();
                    }
                    break;
                case "frmctcarvpooradherencereasons":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVPoorAdherenceReasons);
                        mCTCCodes.Data_Delete();
                    }
                    break;
                case "frmctcreferedto":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ReferedTo);
                        mCTCCodes.Data_Delete();
                    }
                    break;
                case "frmctcarvreasons":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVReasons);
                        mCTCCodes.Data_Delete();
                    }
                    break;
                case "frmctcfollowupstatus":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.FollowUpStatus);
                        mCTCCodes.Data_Delete();
                    }
                    break;
                case "frmctcreferedfrom":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ReferedFrom);
                        mCTCCodes.Data_Delete();
                    }
                    break;
                case "frmctcpriorarvexposure":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.PriorARVExposure);
                        mCTCCodes.Data_Delete();
                    }
                    break;
                case "frmctcartwhyeligible":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARTWhyEligible);
                        mCTCCodes.Data_Delete();
                    }
                    break;
                case "frmctcrelevantcomeds":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.RelevantComeds);
                        mCTCCodes.Data_Delete();
                    }
                    break;
                case "frmctcabnormallabresults":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.abnormallabresults);
                        mCTCCodes.Data_Delete();
                    }
                    break;

                #endregion

               

            }
        }
        #endregion

        #region Refresh_Clicked
        internal static void Refresh_Clicked()
        {
            if (Program.gMdiForm.ActiveMdiChild == null)
            {
                return;
            }

            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            switch (Program.gMdiForm.ActiveMdiChild.Name.Trim().ToLower())
            {
                #region generalsetup

                case "frmgencountries":
                    {
                        frmGENCountries mGENCountries = new frmGENCountries();
                        mGENCountries.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmgenregions":
                    {
                        frmGENRegions mGENRegions = new frmGENRegions();
                        mGENRegions.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmgendistricts":
                    {
                        frmGENDistricts mGENDistricts = new frmGENDistricts();
                        mGENDistricts.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmgenmedicalstaffs":
                    {
                        frmGENMedicalStaffs mGENMedicalStaffs = new frmGENMedicalStaffs();
                        mGENMedicalStaffs.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmgentreatmentpoints":
                    {
                        frmGENTreatmentPoints mGENTreatmentPoints = new frmGENTreatmentPoints();
                        mGENTreatmentPoints.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmgenpatientdocuments":
                    {
                        frmGENPatientDocuments mGENPatientDocuments = new frmGENPatientDocuments();
                        mGENPatientDocuments.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;

                #endregion

                #region inventorysetup

                case "frmivsstores":
                    {
                        frmIVSStores mIVSStores = new frmIVSStores();
                        mIVSStores.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmivsproductcategories":
                    {
                        frmIVSProductCategories mIVSProductCategories = new frmIVSProductCategories();
                        mIVSProductCategories.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmivspackagings":
                    {
                        frmIVSPackagings mIVSPackagings = new frmIVSPackagings();
                        mIVSPackagings.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmivssourcesofstock":
                    {
                        frmIVSSourcesOfStock mIVSSourcesOfStock = new frmIVSSourcesOfStock();
                        mIVSSourcesOfStock.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmivscustomers":
                    {
                        frmIVSCustomers mIVSCustomers = new frmIVSCustomers();
                        mIVSCustomers.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmivsproducts":
                    {
                        frmIVSProducts mIVSProducts = new frmIVSProducts();
                        mIVSProducts.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;

                #endregion

                #region billing
                case "frmbildepositaccounts":
                    {
                        frmBILDepositAccounts mBILDepositAccounts = new frmBILDepositAccounts();
                        mBILDepositAccounts.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmbildepositaccountmembers":
                    {
                        frmBILDepositAccountMembers mBILDepositAccountMembers = new frmBILDepositAccountMembers();
                        mBILDepositAccountMembers.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                #endregion

                #region billing

                case "frmmtudiagnoses":
                    {
                        frmMTUDiagnoses mMTUDiagnoses = new frmMTUDiagnoses();
                        mMTUDiagnoses.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;

                #endregion

                #region billingsetup

                case "frmblscurrencies":
                    {
                        frmBLSCurrencies mBLSCurrencies = new frmBLSCurrencies();
                        mBLSCurrencies.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmblspaymenttypes":
                    {
                        frmBLSPaymentTypes mBLSPaymentTypes = new frmBLSPaymentTypes();
                        mBLSPaymentTypes.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmblsitemgroups":
                    {
                        frmBLSItemGroups mBLSItemGroups = new frmBLSItemGroups();
                        mBLSItemGroups.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmblsitemsubgroups":
                    {
                        frmBLSItemSubGroups mBLSItemSubGroups = new frmBLSItemSubGroups();
                        mBLSItemSubGroups.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmblsitems":
                    {
                        frmBLSItems mBLSItems = new frmBLSItems();
                        mBLSItems.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;

                #endregion

                #region customers

                case "frmcuscustomergroups":
                    {
                        frmCUSCustomerGroups mBLSCustomerGroups = new frmCUSCustomerGroups();
                        mBLSCustomerGroups.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmcuscustomersubgroups":
                    {
                        frmCUSCustomerSubGroups mBLSCustomerSubGroups = new frmCUSCustomerSubGroups();
                        mBLSCustomerSubGroups.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmcusmembers":
                    {
                        frmCUSMembers mCUSMembers = new frmCUSMembers();
                        mCUSMembers.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;

                #endregion

                #region outpatientdept

                case "frmopdpatientlisting":
                    {
                        frmOPDPatientListing mOPDPatientListing = (frmOPDPatientListing)Program.gMdiForm.ActiveMdiChild;
                        mOPDPatientListing.Data_Fill();
                    }
                    break;

                #endregion

                #region billing

                case "frmbilsaleshistory":
                    {
                        frmBILSalesHistory mBILSalesHistory = (frmBILSalesHistory)Program.gMdiForm.ActiveMdiChild;
                        mBILSalesHistory.Data_Fill();
                    }
                    break;
                case "frmbilreceiptshistory":
                    {
                        frmBILReceiptsHistory mBILReceiptsHistory = (frmBILReceiptsHistory)Program.gMdiForm.ActiveMdiChild;
                        mBILReceiptsHistory.Data_Fill();
                    }
                    break;
                case "frmbilinvoiceshistory":
                    {
                        frmBILInvoicesHistory mBILInvoicesHistory = (frmBILInvoicesHistory)Program.gMdiForm.ActiveMdiChild;
                        mBILInvoicesHistory.Data_Fill();
                    }
                    break;
                case "frmbilvoidedsaleshistory":
                    {
                        frmBILVoidedSalesHistory mBILVoidedSalesHistory = (frmBILVoidedSalesHistory)Program.gMdiForm.ActiveMdiChild;
                        mBILVoidedSalesHistory.Data_Fill();
                    }
                    break;
                case "frmbilinvoicepaymentshistory":
                    {
                        frmBILInvoicePaymentsHistory mBILInvoicePaymentsHistory = (frmBILInvoicePaymentsHistory)Program.gMdiForm.ActiveMdiChild;
                        mBILInvoicePaymentsHistory.Data_Fill();
                    }
                    break;

                #endregion

                #region inpatientdept

                case "frmipdwards":
                    {
                        frmIPDWards mIPDWards = new frmIPDWards();
                        mIPDWards.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmipdwardrooms":
                    {
                        frmIPDWardRooms mIPDWardRooms = new frmIPDWardRooms();
                        mIPDWardRooms.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmipdwardroombeds":
                    {
                        frmIPDWardRoomBeds mIPDWardRoomBeds = new frmIPDWardRoomBeds();
                        mIPDWardRoomBeds.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmipddischargestatus":
                    {
                        frmIPDDischargeStatus mIPDDischargeStatus = new frmIPDDischargeStatus();
                        mIPDDischargeStatus.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;

                #endregion

                #region securitysetup

                case "frmsecusergroups":
                    {
                        frmSECUserGroups mSECUserGroups = new frmSECUserGroups();
                        mSECUserGroups.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmsecuserS":
                    {
                        frmSECUsers mSECUsers = new frmSECUsers();
                        mSECUsers.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;

                #endregion

                #region reportdesigner

                case "frmrpdreportgroups":
                    {
                        frmRPDReportGroups mRPDReportGroups = new frmRPDReportGroups();
                        mRPDReportGroups.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmrpdreports":
                    {
                        frmRPDReports mRPDReports = new frmRPDReports();
                        mRPDReports.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;

                #endregion

                #region diagnosesandtreatments

                case "frmdxtgroups":
                    {
                        frmDXTGroups mDXTGroups = new frmDXTGroups();
                        mDXTGroups.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmdxtsubgroups":
                    {
                        frmDXTSubGroups mDXTSubGroups = new frmDXTSubGroups();
                        mDXTSubGroups.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmdxtdiagnoses":
                    {
                        frmDXTDiagnoses mDXTDiagnoses = new frmDXTDiagnoses();
                        mDXTDiagnoses.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmdxtlisting":
                    {
                        frmDXTListing mDXTListing = (frmDXTListing)Program.gMdiForm.ActiveMdiChild;
                        mDXTListing.Data_Fill();
                    }
                    break;
                case "frmdxtindicatorgroups":
                    {
                        frmDXTIndicatorGroups mDXTIndicatorGroups = new frmDXTIndicatorGroups();
                        mDXTIndicatorGroups.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmdxtindicators":
                    {
                        frmDXTIndicators mDXTIndicators = new frmDXTIndicators();
                        mDXTIndicators.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;

                #endregion

                #region laboritory

                case "frmlablaboratories":
                    {
                        frmLABLaboratories mLABs = new frmLABLaboratories();
                        mLABs.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);

                    }
                    break;
                case "frmlablabtestgroups":
                    {
                        frmLABLabTestGroups mLABtestgroups = new frmLABLabTestGroups();
                        mLABtestgroups.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmlablabtestsubgroups":
                    {
                        frmLABLabTestSubGroups mLABLabTestSubGroups = new frmLABLabTestSubGroups();
                        mLABLabTestSubGroups.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmlablabtests":
                    {
                        frmLABLabTests mLABtests = new frmLABLabTests();
                        mLABtests.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmlablisting":
                    {
                        frmLABListing mLABListing = new frmLABListing();
                        mLABListing.Data_Fill();
                    }
                    break;

                #endregion

                #region ctc

                case "frmctcmaritalstatus":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.MaritalStatus);
                        mCTCCodes.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmctcfunctionalstatus":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.FunctionalStatus);
                        mCTCCodes.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmctctbstatus":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.TBStatus);
                        mCTCCodes.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmctcarvstatus":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVStatus);
                        mCTCCodes.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmctcaidsillness":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.AIDSIllness);
                        mCTCCodes.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmctcarvcombregimens":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVCombRegimens);
                        mCTCCodes.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmctcarvadherence":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVAdherence);
                        mCTCCodes.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmctcarvpooradherencereasons":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVPoorAdherenceReasons);
                        mCTCCodes.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmctcreferedto":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ReferedTo);
                        mCTCCodes.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmctcarvreasons":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVReasons);
                        mCTCCodes.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmctcfollowupstatus":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.FollowUpStatus);
                        mCTCCodes.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmctcreferedfrom":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ReferedFrom);
                        mCTCCodes.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmctcpriorarvexposure":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.PriorARVExposure);
                        mCTCCodes.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmctcartwhyeligible":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARTWhyEligible);
                        mCTCCodes.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmctcrelevantcomeds":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.RelevantComeds);
                        mCTCCodes.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;
                case "frmctcabnormallabresults":
                    {
                        frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.abnormallabresults);
                        mCTCCodes.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                    }
                    break;

                #endregion
            }

            Program.gMdiForm.Cursor = Cursors.Default;
        }
        #endregion

        #region Find_MdiChild
        private bool Find_MdiChild(string mFormName)
        {
            bool mFound = false;

            foreach (DevExpress.XtraEditors.XtraForm mForm in Program.gMdiForm.MdiChildren)
            {
                if (mForm.Name.ToLower() == mFormName.Trim().ToLower())
                {
                    mForm.BringToFront();
                    mFound = true;
                    break;
                }
            }

            return mFound;
        }
        #endregion

        #region outpatientdept

        #region Open_OPDRegistrations
        private void Open_OPDRegistrations(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmOPDRegistrations mOPDRegistrations = new frmOPDRegistrations();
            mOPDRegistrations.Name = "frmOPDRegistrations";
            mOPDRegistrations.Text = mItemLink.Caption;
            mOPDRegistrations.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_OPDEditPatientDetails
        private void Open_OPDEditPatientDetails(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmOPDEditPatientDetails mOPDEditPatientDetails = new frmOPDEditPatientDetails();
            mOPDEditPatientDetails.Name = "frmOPDEditPatientDetails";
            mOPDEditPatientDetails.Text = mItemLink.Caption;
            mOPDEditPatientDetails.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_OPDPatientDocuments
        private void Open_OPDPatientDocuments(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmOPDPatientDocuments mOPDPatientDocuments = new frmOPDPatientDocuments();
            mOPDPatientDocuments.Text = mItemLink.Caption;
            mOPDPatientDocuments.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_OPDPatientListing
        private void Open_OPDPatientListing(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmOPDPatientListing") == false)
            {
                frmOPDPatientListing mOPDPatientListing = new frmOPDPatientListing();
                mOPDPatientListing.MdiParent = Program.gMdiForm;
                mOPDPatientListing.Name = "frmOPDPatientListing";
                mOPDPatientListing.Text = mItemLink.Caption;
                mOPDPatientListing.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #endregion

        #region inpatientdept

        #region Open_IPDWards
        private void Open_IPDWards(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmIPDWards") == false)
            {
                frmIPDWards mIPDWards = new frmIPDWards();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdIPDWards";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mIPDWards.Name;
                mMainGrid.Text = mItemLink.Caption;

                mIPDWards.Grid_Settings(mMainGrid.grdMain);
                mIPDWards.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_IPDWardRooms
        private void Open_IPDWardRooms(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmIPDWardRooms") == false)
            {
                frmIPDWardRooms mIPDWardRooms = new frmIPDWardRooms();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdIPDWardRooms";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mIPDWardRooms.Name;
                mMainGrid.Text = mItemLink.Caption;

                mIPDWardRooms.Grid_Settings(mMainGrid.grdMain);
                mIPDWardRooms.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_IPDWardRoomBeds
        private void Open_IPDWardRoomBeds(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmIPDWardRoomBeds") == false)
            {
                frmIPDWardRoomBeds mIPDWardRoomBeds = new frmIPDWardRoomBeds();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdIPDWardRoomBeds";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mIPDWardRoomBeds.Name;
                mMainGrid.Text = mItemLink.Caption;

                mIPDWardRoomBeds.Grid_Settings(mMainGrid.grdMain);
                mIPDWardRoomBeds.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_IPDDischargeStatus
        private void Open_IPDDischargeStatus(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmIPDDischargeStatus") == false)
            {
                frmIPDDischargeStatus mIPDDischargeStatus = new frmIPDDischargeStatus();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdIPDDischargeStatus";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mIPDDischargeStatus.Name;
                mMainGrid.Text = mItemLink.Caption;

                mIPDDischargeStatus.Grid_Settings(mMainGrid.grdMain);
                mIPDDischargeStatus.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_IPDRegistrations
        private void Open_IPDRegistrations(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmIPDRegistrations mIPDRegistrations = new frmIPDRegistrations();
            mIPDRegistrations.Name = "frmIPDRegistrations";
            mIPDRegistrations.Text = mItemLink.Caption;
            mIPDRegistrations.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_IPDEditPatientDetails
        private void Open_IPDEditPatientDetails(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmOPDEditPatientDetails mOPDEditPatientDetails = new frmOPDEditPatientDetails();
            mOPDEditPatientDetails.Name = "frmOPDEditPatientDetails";
            mOPDEditPatientDetails.Text = mItemLink.Caption;
            mOPDEditPatientDetails.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_IPDPatientListing
        private void Open_IPDPatientListing(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmIPDPatientListing") == false)
            {
                frmIPDPatientListing mIPDPatientListing = new frmIPDPatientListing();
                mIPDPatientListing.MdiParent = Program.gMdiForm;
                mIPDPatientListing.Name = "frmIPDPatientListing";
                mIPDPatientListing.Text = mItemLink.Caption;
                mIPDPatientListing.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_IPDTransfers
        private void Open_IPDTransfers(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmIPDTransfers mIPDTransfers = new frmIPDTransfers();
            mIPDTransfers.Name = "frmIPDTransfers";
            mIPDTransfers.Text = mItemLink.Caption;
            mIPDTransfers.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_IPDDischarges
        private void Open_IPDDischarges(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmIPDDischarges mIPDDischarges = new frmIPDDischarges();
            mIPDDischarges.Name = "frmIPDDischarges";
            mIPDDischarges.Text = mItemLink.Caption;
            mIPDDischarges.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #endregion

        #region generalsetup

        #region Open_GENFacilitySetup
        private void Open_GENFacilitySetup(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmGENFacilitySetup mGENFacilitySetup = new frmGENFacilitySetup();
            mGENFacilitySetup.Text = mItemLink.Caption;
            mGENFacilitySetup.ShowDialog();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_GENAutoCodes
        private void Open_GENAutoCodes(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmGENAutoCodes") == false)
            {
                frmGENAutoCodes mGENAutoCodes = new frmGENAutoCodes();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdGENAutoCodes";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mGENAutoCodes.Name;
                mMainGrid.Text = mItemLink.Caption;

                mGENAutoCodes.Grid_Settings(mMainGrid.grdMain);
                mGENAutoCodes.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_GENCountries
        private void Open_GENCountries(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmGENCountries") == false)
            {
                frmGENCountries mGENCountries = new frmGENCountries();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdGENCountries";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mGENCountries.Name;
                mMainGrid.Text = mItemLink.Caption;

                mGENCountries.Grid_Settings(mMainGrid.grdMain);
                mGENCountries.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_GENRegions
        private void Open_GENRegions(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmGENRegions") == false)
            {
                frmGENRegions mGENRegions = new frmGENRegions();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdGENRegions";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mGENRegions.Name;
                mMainGrid.Text = mItemLink.Caption;

                mGENRegions.Grid_Settings(mMainGrid.grdMain);
                mGENRegions.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_GENDistricts
        private void Open_GENDistricts(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmGENDistricts") == false)
            {
                frmGENDistricts mGENDistricts = new frmGENDistricts();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdGENDistricts";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mGENDistricts.Name;
                mMainGrid.Text = mItemLink.Caption;

                mGENDistricts.Grid_Settings(mMainGrid.grdMain);
                mGENDistricts.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_GENMedicalStaffs
        private void Open_GENMedicalStaffs(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmGENMedicalStaffs") == false)
            {
                frmGENMedicalStaffs mGENMedicalStaffs = new frmGENMedicalStaffs();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdGENMedicalStaffs";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mGENMedicalStaffs.Name;
                mMainGrid.Text = mItemLink.Caption;

                mGENMedicalStaffs.Grid_Settings(mMainGrid.grdMain);
                mGENMedicalStaffs.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_GENTreatmentPoints
        private void Open_GENTreatmentPoints(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmGENTreatmentPoints") == false)
            {
                frmGENTreatmentPoints mGENTreatmentPoints = new frmGENTreatmentPoints();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdGENTreatmentPoints";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mGENTreatmentPoints.Name;
                mMainGrid.Text = mItemLink.Caption;

                mGENTreatmentPoints.Grid_Settings(mMainGrid.grdMain);
                mGENTreatmentPoints.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_GENPatientDocuments
        private void Open_GENPatientDocuments(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmGENPatientDocuments") == false)
            {
                frmGENPatientDocuments mGENPatientDocuments = new frmGENPatientDocuments();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdGENPatientDocuments";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mGENPatientDocuments.Name;
                mMainGrid.Text = mItemLink.Caption;

                mGENPatientDocuments.Grid_Settings(mMainGrid.grdMain);
                mGENPatientDocuments.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_GENSearchEngine
        private void Open_GENSearchEngine(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmGENSearchEngine mGENSearchEngine = new frmGENSearchEngine();
            mGENSearchEngine.Text = mItemLink.Caption;
            mGENSearchEngine.ShowDialog();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_GENPatientExtraFields
        private void Open_GENPatientExtraFields(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmGENPatientFields") == false)
            {
                frmGENPatientFields mGENPatientFields = new frmGENPatientFields();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdGENPatientFields";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mGENPatientFields.Name;
                mMainGrid.Text = mItemLink.Caption;

                mGENPatientFields.Grid_Settings(mMainGrid.grdMain);
                mGENPatientFields.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #endregion

        #region diagnosesandtreatments

        #region Open_DXTGroups
        private void Open_DXTGroups(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmDXTGroups") == false)
            {
                frmDXTGroups mDXTGroups = new frmDXTGroups();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdDXTGroups";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mDXTGroups.Name;
                mMainGrid.Text = mItemLink.Caption;

                mDXTGroups.Grid_Settings(mMainGrid.grdMain);
                mDXTGroups.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_DXTSubGroups
        private void Open_DXTSubGroups(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmDXTSubGroups") == false)
            {
                frmDXTSubGroups mDXTSubGroups = new frmDXTSubGroups();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdDXTSubGroups";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mDXTSubGroups.Name;
                mMainGrid.Text = mItemLink.Caption;

                mDXTSubGroups.Grid_Settings(mMainGrid.grdMain);
                mDXTSubGroups.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_DXTDiagnoses
        private void Open_DXTDiagnoses(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmDXTDiagnoses") == false)
            {
                frmDXTDiagnoses mDXTDiagnoses = new frmDXTDiagnoses();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdDXTDiagnoses";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mDXTDiagnoses.Name;
                mMainGrid.Text = mItemLink.Caption;

                mDXTDiagnoses.Grid_Settings(mMainGrid.grdMain);
                mDXTDiagnoses.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_DXTEpisodes
        private void Open_DXTEpisodes(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmDXTEpisodes mDXTEpisodes = new frmDXTEpisodes();
            mDXTEpisodes.Name = "frmDXTEpisodes";
            mDXTEpisodes.Text = mItemLink.Caption;
            mDXTEpisodes.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_DXTListing
        private void Open_DXTListing(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmDXTListing") == false)
            {
                frmDXTListing mDXTListing = new frmDXTListing();
                mDXTListing.MdiParent = Program.gMdiForm;
                mDXTListing.Name = "frmDXTListing";
                mDXTListing.Text = mItemLink.Caption;
                mDXTListing.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_DXTPrintPrescriptions
        private void Open_DXTPrintPrescriptions(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmDXTPrintPrescriptions mDXTPrintPrescriptions = new frmDXTPrintPrescriptions();
            mDXTPrintPrescriptions.Name = "frmDXTPrintPrescriptions";
            mDXTPrintPrescriptions.Text = mItemLink.Caption;
            mDXTPrintPrescriptions.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_DXTIndicatorGroups
        private void Open_DXTIndicatorGroups(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmDXTIndicatorGroups") == false)
            {
                frmDXTIndicatorGroups mDXTIndicatorGroups = new frmDXTIndicatorGroups();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdDXTIndicatorGroups";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mDXTIndicatorGroups.Name;
                mMainGrid.Text = mItemLink.Caption;

                mDXTIndicatorGroups.Grid_Settings(mMainGrid.grdMain);
                mDXTIndicatorGroups.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_DXTIndicators
        private void Open_DXTIndicators(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmDXTIndicators") == false)
            {
                frmDXTIndicators mDXTIndicators = new frmDXTIndicators();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdDXTIndicators";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mDXTIndicators.Name;
                mMainGrid.Text = mItemLink.Caption;

                mDXTIndicators.Grid_Settings(mMainGrid.grdMain);
                mDXTIndicators.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_DXTIndicatorDiagnoses
        private void Open_DXTIndicatorDiagnoses(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmDXTIndicatorDiagnoses mDXTIndicatorDiagnoses = new frmDXTIndicatorDiagnoses();
            mDXTIndicatorDiagnoses.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #endregion

        #region laboratory

        #region Open_LABLaboratories
        private void Open_LABLaboratories(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmLABLaboratories") == false)
            {
                frmLABLaboratories mLABs = new frmLABLaboratories();
                mLABs.Name = "frmLABLaboratories";
                mLABs.Text = mItemLink.Caption;

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdLABLaboratories";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mLABs.Name;;
                mMainGrid.Text = mItemLink.Caption;

                mLABs.Grid_Settings(mMainGrid.grdMain);
                mLABs.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;
        }
        #endregion

        #region Open_LABLabTestGroups
        private void Open_LABLabTestGroups(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmLABLabTestGroups") == false)
            {
                frmLABLabTestGroups mLABs = new frmLABLabTestGroups();
                mLABs.Name = "frmLABLabTestGroups";
                mLABs.Text = mItemLink.Caption;

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdLABLabTestGroups";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mLABs.Name;
                mMainGrid.Text = mItemLink.Caption;

                mLABs.Grid_Settings(mMainGrid.grdMain);
                mLABs.Data_Fill(mMainGrid.grdMain);
                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;
        }
        #endregion

        #region Open_LABLabTestSubGroups
        private void Open_LABLabTestSubGroups(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmLABLabTestSubGroups") == false)
            {
                frmLABLabTestSubGroups mLABLabTestSubGroups = new frmLABLabTestSubGroups();
                mLABLabTestSubGroups.Name = "frmLABLabTestSubGroups";
                mLABLabTestSubGroups.Text = mItemLink.Caption;

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdLABLabTestSubGroups";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mLABLabTestSubGroups.Name;
                mMainGrid.Text = mItemLink.Caption;

                mLABLabTestSubGroups.Grid_Settings(mMainGrid.grdMain);
                mLABLabTestSubGroups.Data_Fill(mMainGrid.grdMain);
                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;
        }
        #endregion

        #region Open_LABLabTests
        private void Open_LABLabTests(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmLABLabTests") == false)
            {
                frmLABLabTests mLABtests = new frmLABLabTests();
                mLABtests.Name = "frmLABLabTests";
                mLABtests.Text = mItemLink.Caption;

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdLABLabTests";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mLABtests.Name;
                mMainGrid.Text = mItemLink.Caption;

                mLABtests.Grid_Settings(mMainGrid.grdMain);
                mLABtests.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;
        }
        #endregion

        #region Open_LABRemarks
        private void Open_LABRemarks(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmLABRemarks") == false)
            {
                frmLABRemarks mLABRemarks = new frmLABRemarks();
                mLABRemarks.Name = "frmLABRemarks";
                mLABRemarks.Text = mItemLink.Caption;

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdLABRemarks";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mLABRemarks.Name;
                mMainGrid.Text = mItemLink.Caption;

                mLABRemarks.Grid_Settings(mMainGrid.grdMain);
                mLABRemarks.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;
        }
        #endregion

        #region Open_LABvisit
        private void Open_LABvisit(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmLABPatientTests mLABvisit = new frmLABPatientTests();
            mLABvisit.Name = "frmLABPatientTests";
            mLABvisit.Text = mItemLink.Caption;
            mLABvisit.Show();

            Program.gMdiForm.Cursor = Cursors.Default;
        }
        #endregion

        #region Open_LABListing
        private void Open_LABListing(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmLABListing") == false)
            {
                frmLABListing mLABListing = new frmLABListing();
                mLABListing.MdiParent = Program.gMdiForm;
                mLABListing.Name = "frmLABListing";
                mLABListing.Text = mItemLink.Caption;
                mLABListing.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;
        }
        #endregion

        #endregion

        #region RCH

        #region Open_RCHClients
        private void Open_RCHClients(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmRCHClients mRCHClients = new frmRCHClients();
            mRCHClients.Name = "frmRCHClients";
            mRCHClients.Text = mItemLink.Caption;
            mRCHClients.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_RCHFamilyPlanning
        private void Open_RCHFamilyPlanning(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmRCHFamilyPlanning mRCHFamilyPlanning = new frmRCHFamilyPlanning();
            mRCHFamilyPlanning.Name = "frmRCHFamilyPlanning";
            mRCHFamilyPlanning.Text = mItemLink.Caption;
            mRCHFamilyPlanning.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_RCHAntenatalCare
        private void Open_RCHAntenatalCare(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmRCHAntenatalCare mRCHAntenatalCare = new frmRCHAntenatalCare();
            mRCHAntenatalCare.Name = "frmRCHAntenatalCare";
            mRCHAntenatalCare.Text = mItemLink.Caption;
            mRCHAntenatalCare.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_RCHMaternity
        private void Open_RCHMaternity(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmRCHMaternity1 mRCHMaternity = new frmRCHMaternity1();
            mRCHMaternity.Name = "frmmRCHMaternity";
            mRCHMaternity.Text = mItemLink.Caption;
            mRCHMaternity.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_RCHPostnatalCare
        private void Open_RCHPostnatalCare(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmRCHPostnatalCare mRCHPostnatalCare = new frmRCHPostnatalCare();
            mRCHPostnatalCare.Name = "frmRCHPostnatalCare";
            mRCHPostnatalCare.Text = mItemLink.Caption;
            mRCHPostnatalCare.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_RCHChildren
        private void Open_RCHChildren(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmRCHChildren mRCHChildren = new frmRCHChildren();
            mRCHChildren.Name = "frmRCHChildren";
            mRCHChildren.Text = mItemLink.Caption;
            mRCHChildren.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_RCHVaccinations
        private void Open_RCHVaccinations(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmRCHVaccinations mRCHVaccinations = new frmRCHVaccinations(-1, "");
            mRCHVaccinations.Name = "frmRCHVaccinations";
            mRCHVaccinations.Text = mItemLink.Caption;
            mRCHVaccinations.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_RCHBirthMethods
        private void Open_RCHBirthMethods(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmRCHBirthMethods") == false)
            {
                frmDXTDiagnoses mRCHBirthMethods = new frmDXTDiagnoses();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdRCHBirthMethods";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mRCHBirthMethods.Name;
                mMainGrid.Text = mItemLink.Caption;

                mRCHBirthMethods.Grid_Settings(mMainGrid.grdMain);
                mRCHBirthMethods.Data_Fill(mMainGrid.grdMain);
               

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

            //Program.gMdiForm.Cursor = Cursors.WaitCursor;

            //if (this.Find_MdiChild("frmDXTDiagnoses") == false)
            //{
            //    frmDXTDiagnoses mDXTDiagnoses = new frmDXTDiagnoses();

            //    frmMainGrid mMainGrid = new frmMainGrid();
            //    mMainGrid.grdMain.Name = "grdDXTDiagnoses";
            //    mMainGrid.MdiParent = Program.gMdiForm;
            //    mMainGrid.Name = mDXTDiagnoses.Name;
            //    mMainGrid.Text = mItemLink.Caption;

            //    mDXTDiagnoses.Grid_Settings(mMainGrid.grdMain);
            //    mDXTDiagnoses.Data_Fill(mMainGrid.grdMain);

            //    mMainGrid.Show();
            //}

            //Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #endregion

        #region customers

        #region Open_CUSCustomerGroups
        private void Open_CUSCustomerGroups(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmCUSCustomerGroups") == false)
            {
                frmCUSCustomerGroups mBLSCustomerGroups = new frmCUSCustomerGroups();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdCUSCustomerGroups";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mBLSCustomerGroups.Name;
                mMainGrid.Text = mItemLink.Caption;

                mBLSCustomerGroups.Grid_Settings(mMainGrid.grdMain);
                mBLSCustomerGroups.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CUSCustomerSubGroups
        private void Open_CUSCustomerSubGroups(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmCUSCustomerSubGroups") == false)
            {
                frmCUSCustomerSubGroups mBLSCustomerSubGroups = new frmCUSCustomerSubGroups();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdCUSCustomerSubGroups";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mBLSCustomerSubGroups.Name;
                mMainGrid.Text = mItemLink.Caption;

                mBLSCustomerSubGroups.Grid_Settings(mMainGrid.grdMain);
                mBLSCustomerSubGroups.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CUSMembers
        private void Open_CUSMembers(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmCUSMembers") == false)
            {
                frmCUSMembers mCUSMembers = new frmCUSMembers();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdCUSMembers";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mCUSMembers.Name;
                mMainGrid.Text = mItemLink.Caption;

                mCUSMembers.Grid_Settings(mMainGrid.grdMain);
                mCUSMembers.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CUSImportMembers
        private void Open_CUSImportMembers(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            frmTOOImportation mTOOImportation = new frmTOOImportation("facilitycorporatemembers");
            mTOOImportation.Text = mItemLink.Caption;
            mTOOImportation.ShowDialog();
        }
        #endregion

        #region Open_CUSActivateDeActivate
        private void Open_CUSActivateDeActivate(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            frmCUSActivateDeActivate mCUSActivateDeActivate = new frmCUSActivateDeActivate();
            mCUSActivateDeActivate.Text = mItemLink.Caption;
            mCUSActivateDeActivate.Show();
        }
        #endregion

        #endregion

        #region billing

        #region Open_BILDepositAccounts
        private void Open_BILDepositAccounts(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmBILDepositAccounts") == false)
            {
                frmBILDepositAccounts mBILDepositAccounts = new frmBILDepositAccounts();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdBILDepositAccounts";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mBILDepositAccounts.Name;
                mMainGrid.Text = mItemLink.Caption;

                mBILDepositAccounts.Grid_Settings(mMainGrid.grdMain);
                mBILDepositAccounts.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_BILDepositAccountMembers
        private void Open_BILDepositAccountMembers(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmBILDepositAccountMembers") == false)
            {
                frmBILDepositAccountMembers mBILDepositAccountMembers = new frmBILDepositAccountMembers();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdBILDepositAccountMembers";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mBILDepositAccountMembers.Name;
                mMainGrid.Text = mItemLink.Caption;

                mBILDepositAccountMembers.Grid_Settings(mMainGrid.grdMain);
                mBILDepositAccountMembers.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_BILBillPosting
        private void Open_BILBillPosting(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmBILBillPosting mBILBillPosting = new frmBILBillPosting();
            mBILBillPosting.Name = "frmBILBillPosting";
            mBILBillPosting.Text = mItemLink.Caption;
            mBILBillPosting.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_BILPayBillsPatients
        private void Open_BILPayBillsPatients(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmBILPayBillsPatients mBILPayBillsPatients = new frmBILPayBillsPatients();
            mBILPayBillsPatients.Text = mItemLink.Caption;
            mBILPayBillsPatients.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_BILPayBillsGroups
        private void Open_BILPayBillsGroups(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmBILPayBillsGroups mBILPayBillsGroups = new frmBILPayBillsGroups();
            mBILPayBillsGroups.Text = mItemLink.Caption;
            mBILPayBillsGroups.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_BILSalesHistory
        private void Open_BILSalesHistory(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmBILSalesHistory") == false)
            {
                frmBILSalesHistory mBILSalesHistory = new frmBILSalesHistory();
                mBILSalesHistory.MdiParent = Program.gMdiForm;
                mBILSalesHistory.Name = "frmBILSalesHistory";
                mBILSalesHistory.Text = mItemLink.Caption;
                mBILSalesHistory.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_BILReceiptsHistory
        private void Open_BILReceiptsHistory(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmBILReceiptsHistory") == false)
            {
                frmBILReceiptsHistory mBILReceiptsHistory = new frmBILReceiptsHistory();
                mBILReceiptsHistory.MdiParent = Program.gMdiForm;
                mBILReceiptsHistory.Name = "frmBILReceiptsHistory";
                mBILReceiptsHistory.Text = mItemLink.Caption;
                mBILReceiptsHistory.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_BILInvoicesHistory
        private void Open_BILInvoicesHistory(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmBILInvoicesHistory") == false)
            {
                frmBILInvoicesHistory mBILInvoicesHistory = new frmBILInvoicesHistory();
                mBILInvoicesHistory.MdiParent = Program.gMdiForm;
                mBILInvoicesHistory.Name = "frmBILInvoicesHistory";
                mBILInvoicesHistory.Text = mItemLink.Caption;
                mBILInvoicesHistory.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_BILInvoicePaymentsHistory
        private void Open_BILInvoicePaymentsHistory(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmBILInvoicePaymentsHistory") == false)
            {
                frmBILInvoicePaymentsHistory mBILInvoicePaymentsHistory = new frmBILInvoicePaymentsHistory();
                mBILInvoicePaymentsHistory.MdiParent = Program.gMdiForm;
                mBILInvoicePaymentsHistory.Name = "frmBILInvoicePaymentsHistory";
                mBILInvoicePaymentsHistory.Text = mItemLink.Caption;
                mBILInvoicePaymentsHistory.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_BILVoidedSalesHistory
        private void Open_BILVoidedSalesHistory(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmBILVoidedSalesHistory") == false)
            {
                frmBILVoidedSalesHistory mBILVoidedSalesHistory = new frmBILVoidedSalesHistory();
                mBILVoidedSalesHistory.MdiParent = Program.gMdiForm;
                mBILVoidedSalesHistory.Name = "frmBILVoidedSalesHistory";
                mBILVoidedSalesHistory.Text = mItemLink.Caption;
                mBILVoidedSalesHistory.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_BILDebtReliefRequests
        private void Open_BILDebtReliefRequests(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmBILDebtReliefRequests mBILDebtReliefRequests = new frmBILDebtReliefRequests();
            mBILDebtReliefRequests.Text = mItemLink.Caption;
            mBILDebtReliefRequests.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_BILDebtReliefRequestsView
        private void Open_BILDebtReliefRequestsView(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmBILDebtReliefRequestsView") == false)
            {
                frmBILDebtReliefRequestsView mBILDebtReliefRequestsView = new frmBILDebtReliefRequestsView();
                mBILDebtReliefRequestsView.MdiParent = Program.gMdiForm;
                mBILDebtReliefRequestsView.Name = "mBILDebtReliefRequestsView";
                mBILDebtReliefRequestsView.Text = mItemLink.Caption;
                mBILDebtReliefRequestsView.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_OPDChangeBillingGroup
        private void Open_OPDChangeBillingGroup(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmOPDChangeBillingGroup mOPDChangeBillingGroup = new frmOPDChangeBillingGroup();
            mOPDChangeBillingGroup.Name = "frmOPDChangeBillingGroup";
            mOPDChangeBillingGroup.Text = mItemLink.Caption;
            mOPDChangeBillingGroup.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #endregion

        #region inventory

        #region Open_IVPurchaseOrders
        private void Open_IVPurchaseOrders(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmIVPurchaseOrdersView") == false)
            {
                frmIVPurchaseOrdersView mIVPurchaseOrdersView = new frmIVPurchaseOrdersView();
                mIVPurchaseOrdersView.MdiParent = Program.gMdiForm;
                mIVPurchaseOrdersView.Name = "frmIVPurchaseOrdersView";
                mIVPurchaseOrdersView.Text = mItemLink.Caption;
                mIVPurchaseOrdersView.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_IVTransferIns
        private void Open_IVTransferIns(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmIVTransferInsView") == false)
            {
                frmIVTransferInsView mIVTransferInsView = new frmIVTransferInsView();
                mIVTransferInsView.MdiParent = Program.gMdiForm;
                mIVTransferInsView.Name = "frmIVTransferInsView";
                mIVTransferInsView.Text = mItemLink.Caption;
                mIVTransferInsView.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_IVTransferOuts
        private void Open_IVTransferOuts(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmIVTransferOutsView") == false)
            {
                frmIVTransferOutsView mIVTransferOutsView = new frmIVTransferOutsView();
                mIVTransferOutsView.MdiParent = Program.gMdiForm;
                mIVTransferOutsView.Name = "frmIVTransferOutsView";
                mIVTransferOutsView.Text = mItemLink.Caption;
                mIVTransferOutsView.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_IVPhysicalInventory
        private void Open_IVPhysicalInventory(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmIVPhysicalInventoryView") == false)
            {
                frmIVPhysicalInventoryView mIVPhysicalInventoryView = new frmIVPhysicalInventoryView();
                mIVPhysicalInventoryView.MdiParent = Program.gMdiForm;
                mIVPhysicalInventoryView.Name = "frmIVPhysicalInventoryView";
                mIVPhysicalInventoryView.Text = mItemLink.Caption;
                mIVPhysicalInventoryView.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_IVIssuesHistory
        private void Open_IVIssuesHistory(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmIVIssuesHistory") == false)
            {
                frmIVIssuesHistory mIVIssuesHistory = new frmIVIssuesHistory();
                mIVIssuesHistory.MdiParent = Program.gMdiForm;
                mIVIssuesHistory.Name = "frmIVIssuesHistory";
                mIVIssuesHistory.Text = mItemLink.Caption;
                mIVIssuesHistory.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_IVReceiptsHistory
        private void Open_IVReceiptsHistory(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmIVReceiptsHistory") == false)
            {
                frmIVReceiptsHistory mIVReceiptsHistory = new frmIVReceiptsHistory();
                mIVReceiptsHistory.MdiParent = Program.gMdiForm;
                mIVReceiptsHistory.Name = "frmIVReceiptsHistory";
                mIVReceiptsHistory.Text = mItemLink.Caption;
                mIVReceiptsHistory.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #endregion

        #region MTUHA

        #region Open_MTUDiagnoses
        private void Open_MTUDiagnoses(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmMTUDiagnoses") == false)
            {
                frmMTUDiagnoses mMTUDiagnoses = new frmMTUDiagnoses();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdMTUDiagnoses";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mMTUDiagnoses.Name;
                mMainGrid.Text = mItemLink.Caption;

                mMTUDiagnoses.Grid_Settings(mMainGrid.grdMain);
                mMTUDiagnoses.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_MTUDiagnosesMapping
        private void Open_MTUDiagnosesMapping(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmMTUDiagnosesMapping") == false)
            {
                frmMTUDiagnosesMapping mMTUDiagnosesMapping = new frmMTUDiagnosesMapping();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdMTUDiagnosesMapping";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mMTUDiagnosesMapping.Name;
                mMainGrid.Text = mItemLink.Caption;

                mMTUDiagnosesMapping.Grid_Settings(mMainGrid.grdMain);
                mMTUDiagnosesMapping.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_MTUDHISExport
        private void Open_MTUDHISExport(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmMTUDHISExport mMTUDHISExport = new frmMTUDHISExport();
            mMTUDHISExport.ShowDialog();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #endregion

        #region inventorysetup

        #region Open_IVSStores
        private void Open_IVSStores(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmIVSStores") == false)
            {
                frmIVSStores mIVSStores = new frmIVSStores();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdIVSStores";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mIVSStores.Name;
                mMainGrid.Text = mItemLink.Caption;

                mIVSStores.Grid_Settings(mMainGrid.grdMain);
                mIVSStores.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;
        }
        #endregion

        #region Open_IVSProductCategories
        private void Open_IVSProductCategories(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmIVSProductCategories") == false)
            {
                frmIVSProductCategories mIVSProductCategories = new frmIVSProductCategories();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdIVSProductCategories";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mIVSProductCategories.Name;
                mMainGrid.Text = mItemLink.Caption;

                mIVSProductCategories.Grid_Settings(mMainGrid.grdMain);
                mIVSProductCategories.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;
        }
        #endregion

        #region Open_IVSPackagings
        private void Open_IVSPackagings(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmIVSPackagings") == false)
            {
                frmIVSPackagings mIVSPackagings = new frmIVSPackagings();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdIVSPackagings";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mIVSPackagings.Name;
                mMainGrid.Text = mItemLink.Caption;

                mIVSPackagings.Grid_Settings(mMainGrid.grdMain);
                mIVSPackagings.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;
        }
        #endregion

        #region Open_IVSSourcesOfStock
        private void Open_IVSSourcesOfStock(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmIVSSourcesOfStock") == false)
            {
                frmIVSSourcesOfStock mIVSSourcesOfStock = new frmIVSSourcesOfStock();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdIVSSourcesOfStock";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mIVSSourcesOfStock.Name;
                mMainGrid.Text = mItemLink.Caption;

                mIVSSourcesOfStock.Grid_Settings(mMainGrid.grdMain);
                mIVSSourcesOfStock.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;
        }
        #endregion

        #region Open_IVSCustomers
        private void Open_IVSCustomers(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmIVSCustomers") == false)
            {
                frmIVSCustomers mIVSCustomers = new frmIVSCustomers();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdIVSCustomers";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mIVSCustomers.Name;
                mMainGrid.Text = mItemLink.Caption;

                mIVSCustomers.Grid_Settings(mMainGrid.grdMain);
                mIVSCustomers.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;
        }
        #endregion

        #region Open_IVSProducts
        private void Open_IVSProducts(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmIVSProducts") == false)
            {
                frmIVSProducts mIVSProducts = new frmIVSProducts();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdIVSProducts";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mIVSProducts.Name;
                mMainGrid.Text = mItemLink.Caption;

                mIVSProducts.Grid_Settings(mMainGrid.grdMain);
                mIVSProducts.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;
        }
        #endregion

        #endregion

        #region billingsetup

        #region Open_BLSCurrencies
        private void Open_BLSCurrencies(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmBLSCurrencies") == false)
            {
                frmBLSCurrencies mBLSCurrencies = new frmBLSCurrencies();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdBLSCurrencies";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mBLSCurrencies.Name;
                mMainGrid.Text = mItemLink.Caption;

                mBLSCurrencies.Grid_Settings(mMainGrid.grdMain);
                mBLSCurrencies.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_BLSPaymentTypes
        private void Open_BLSPaymentTypes(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmBLSPaymentTypes") == false)
            {
                frmBLSPaymentTypes mBLSPaymentTypes = new frmBLSPaymentTypes();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdBLSPaymentTypes";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mBLSPaymentTypes.Name;
                mMainGrid.Text = mItemLink.Caption;

                mBLSPaymentTypes.Grid_Settings(mMainGrid.grdMain);
                mBLSPaymentTypes.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_BLSPriceCategories
        private void Open_BLSPriceCategories(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmBLSPriceCategories mBLSPriceCategories = new frmBLSPriceCategories();
            mBLSPriceCategories.Text = mItemLink.Caption;
            mBLSPriceCategories.ShowDialog();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_BLSItemGroups
        private void Open_BLSItemGroups(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmBLSItemGroups") == false)
            {
                frmBLSItemGroups mBLSItemGroups = new frmBLSItemGroups();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdBLSItemGroups";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mBLSItemGroups.Name;
                mMainGrid.Text = mItemLink.Caption;

                mBLSItemGroups.Grid_Settings(mMainGrid.grdMain);
                mBLSItemGroups.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_BLSItemSubGroups
        private void Open_BLSItemSubGroups(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmBLSItemSubGroups") == false)
            {
                frmBLSItemSubGroups mBLSItemSubGroups = new frmBLSItemSubGroups();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdBLSItemSubGroups";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mBLSItemSubGroups.Name;
                mMainGrid.Text = mItemLink.Caption;

                mBLSItemSubGroups.Grid_Settings(mMainGrid.grdMain);
                mBLSItemSubGroups.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_BLSItems
        private void Open_BLSItems(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmBLSItems") == false)
            {
                frmBLSItems mBLSItems = new frmBLSItems();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdBLSItems";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mBLSItems.Name;
                mMainGrid.Text = mItemLink.Caption;

                mBLSItems.Grid_Settings(mMainGrid.grdMain);
                mBLSItems.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #endregion

        #region reports

        #region Open_ReportingForm
        private void Open_ReportingForm(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mReportCode =
                mItemLink.ItemName.Substring("report_".Length);

            if (this.Find_MdiChild("frmReporter" + mReportCode) == false)
            {
                Type mReports = typeof(AfyaPro_Types.clsEnums.BuiltInReports);
                bool mIsBuiltIn = false;
                foreach (int mReportCodeInt in Enum.GetValues(mReports))
                {
                    if (mReportCode.Trim().ToLower() == "rep" + mReportCodeInt)
                    {
                        mIsBuiltIn = true;
                        break;
                    }
                }

                DataView mDvUserReports = new DataView();
                mDvUserReports.Table = Program.gDtUserReports;
                mDvUserReports.Sort = "reportcode";

                int mRowIndex = mDvUserReports.Find(mReportCode);

                bool mReportView = false;
                bool mReportDesign = false;
                bool mFormCustomization = false;

                if (mRowIndex >= 0)
                {
                    mReportView = Convert.ToBoolean(mDvUserReports[mRowIndex]["reportview"]);
                    mReportDesign = Convert.ToBoolean(mDvUserReports[mRowIndex]["reportdesign"]);
                    mFormCustomization = Convert.ToBoolean(mDvUserReports[mRowIndex]["reportformcustomization"]);
                }

                if (mIsBuiltIn == true)
                {
                    frmRPDReportingForm1 mRPDReportingForm1 = new frmRPDReportingForm1(mReportCode, false);
                    mRPDReportingForm1.Text = mItemLink.Caption;
                    mRPDReportingForm1.cmdView.Enabled = mReportView;
                    mRPDReportingForm1.cmdDesign.Enabled = mReportDesign;
                    mRPDReportingForm1.layoutControl1.AllowCustomizationMenu = mFormCustomization;
                    mRPDReportingForm1.Name = "frmReporter" + mReportCode;

                    mRPDReportingForm1.ShowDialog();
                }
                else
                {
                    frmRPDReportingForm mRPDReportingForm = new frmRPDReportingForm(mReportCode, false);
                    mRPDReportingForm.Text = mItemLink.Caption;
                    mRPDReportingForm.cmdView.Enabled = mReportView;
                    mRPDReportingForm.cmdDesign.Enabled = mReportDesign;
                    mRPDReportingForm.layoutControl1.AllowCustomizationMenu = mFormCustomization;
                    mRPDReportingForm.Name = "frmReporter" + mReportCode;

                    mRPDReportingForm.ShowDialog();
                }
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #endregion

        #region securitysetup

        #region Open_SECUserGroups
        private void Open_SECUserGroups(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmSECUserGroups") == false)
            {
                frmSECUserGroups mSECUserGroups = new frmSECUserGroups();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdSECUserGroups";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mSECUserGroups.Name;
                mMainGrid.Text = mItemLink.Caption;

                mSECUserGroups.Grid_Settings(mMainGrid.grdMain);
                mSECUserGroups.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_SECGroupsAccess
        private void Open_SECGroupsAccess(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmSECGroupsAccess") == false)
            {
                frmSECGroupsAccess mSECGroupsAccess = new frmSECGroupsAccess();
                mSECGroupsAccess.MdiParent = Program.gMdiForm;
                mSECGroupsAccess.Name = "frmSECGroupsAccess";
                mSECGroupsAccess.Text = mItemLink.Caption;
                mSECGroupsAccess.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_SECReportsAccess
        private void Open_SECReportsAccess(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmSECReportsAccess") == false)
            {
                frmSECReportsAccess mSECReportsAccess = new frmSECReportsAccess();
                mSECReportsAccess.MdiParent = Program.gMdiForm;
                mSECReportsAccess.Name = "frmSECReportsAccess";
                mSECReportsAccess.Text = mItemLink.Caption;
                mSECReportsAccess.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_SECUserGroupPrinters
        private void Open_SECUserGroupPrinters(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmSECUserGroupPrinters mSECUserGroupPrinters = new frmSECUserGroupPrinters();
            mSECUserGroupPrinters.Text = mItemLink.Caption;
            mSECUserGroupPrinters.ShowDialog();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_SECUsers
        private void Open_SECUsers(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmSECUsers") == false)
            {
                frmSECUsers mSECUsers = new frmSECUsers();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdSECUsers";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mSECUsers.Name;
                mMainGrid.Text = mItemLink.Caption;

                mSECUsers.Grid_Settings(mMainGrid.grdMain);
                mSECUsers.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #endregion

        #region reportdesigner

        #region Open_RPDReportGroups
        private void Open_RPDReportGroups(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmRPDReportGroups") == false)
            {
                frmRPDReportGroups mRPDReportGroups = new frmRPDReportGroups();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdRPDReportGroups";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mRPDReportGroups.Name;
                mMainGrid.Text = mItemLink.Caption;

                mRPDReportGroups.Grid_Settings(mMainGrid.grdMain);
                mRPDReportGroups.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_RPDReports
        private void Open_RPDReports(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            if (this.Find_MdiChild("frmRPDReports") == false)
            {
                frmRPDReports mRPDReports = new frmRPDReports();

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = "grdRPDReports";
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mRPDReports.Name;
                mMainGrid.Text = mItemLink.Caption;

                mRPDReports.Grid_Settings(mMainGrid.grdMain);
                mRPDReports.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #endregion

        #region ctc

        #region Open_CTCRegistrations
        private void Open_CTCRegistrations(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmCTCRegistration mCTCRegistration = new frmCTCRegistration();
            mCTCRegistration.Name = "frmCTCRegistration";
            mCTCRegistration.Text = mItemLink.Caption;
            mCTCRegistration.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCEditPatientDetails
        private void Open_CTCEditPatientDetails(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmCTCEditPatientDetails mCTCEditPatientDetails = new frmCTCEditPatientDetails();
            mCTCEditPatientDetails.Name = "frmCTCEditPatientDetails";
            mCTCEditPatientDetails.Text = mItemLink.Caption;
            mCTCEditPatientDetails.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCAppointments
        private void Open_CTCAppointments(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmCTCAppointment mCTCAppointment = new frmCTCAppointment();
            mCTCAppointment.Name = "frmCTCAppointment";
            mCTCAppointment.Text = mItemLink.Caption;
            mCTCAppointment.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCMeasurements
        private void Open_CTCMeasurements(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmCTCMeasurement mCTCMeasurement = new frmCTCMeasurement();
            mCTCMeasurement.Name = "frmCTCMeasurement";
            mCTCMeasurement.Text = mItemLink.Caption;
            mCTCMeasurement.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCHTC
        private void Open_CTCHTC(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmCTCHIVTest mCTCHIVTest = new frmCTCHIVTest();
            mCTCHIVTest.Name = "frmCTCHIVTest";
            mCTCHIVTest.Text = mItemLink.Caption;
            mCTCHIVTest.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCPCR
        private void Open_CTCPCR(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmCTCPCRTest mCTCHIVTest = new frmCTCPCRTest();
            mCTCHIVTest.Name = "frmCTCPCRTest";
            mCTCHIVTest.Text = mItemLink.Caption;
            mCTCHIVTest.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCCD4Tests
        private void Open_CTCCD4Tests(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmCTCCD4Test mCTCCD4Test = new frmCTCCD4Test();
            mCTCCD4Test.Name = "frmCTCCD4Test";
            mCTCCD4Test.Text = mItemLink.Caption;
            mCTCCD4Test.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_PreART
        private void Open_PreART(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmCTCPreART mCTCPreART = new frmCTCPreART();
            mCTCPreART.Name = "frmCTCPreART";
            mCTCPreART.Text = mItemLink.Caption;
            mCTCPreART.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCART
        private void Open_CTCART(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmCTCART mCTCART = new frmCTCART();
            mCTCART.Name = "frmCTCART";
            mCTCART.Text = mItemLink.Caption;
            mCTCART.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCARTP
        private void Open_CTCARTP(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmCTCARTP mCTCARTP = new frmCTCARTP();
            mCTCARTP.Name = "frmCTCARTP";
            mCTCARTP.Text = mItemLink.Caption;
            mCTCARTP.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCExposed
        private void Open_CTCExposed(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmCTCExposed mCTCExposed = new frmCTCExposed();
            mCTCExposed.Name = "frmCTCExposed";
            mCTCExposed.Text = mItemLink.Caption;
            mCTCExposed.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCEnrollInCare
        private void Open_CTCEnrollInCare(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmCTCARTT mCTCEnrollInCare = new frmCTCARTT();
            mCTCEnrollInCare.Name = "frmCTCEnrollInCare";
            mCTCEnrollInCare.Text = mItemLink.Caption;
            mCTCEnrollInCare.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCEnrollInPMTCTANC
        private void Open_CTCEnrollInPMTCTANC(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmCTCPMTCTANC mCTCPMTCTANC = new frmCTCPMTCTANC();
            mCTCPMTCTANC.Name = "frmCTCPMTCTANC";
            mCTCPMTCTANC.Text = mItemLink.Caption;
            mCTCPMTCTANC.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCPMTCTLabourAndDelivery
        private void Open_CTCPMTCTLabourAndDelivery(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmCTCPMTCTLabourAndDelivery mCTCPMTCTLabourAndDelivery = new frmCTCPMTCTLabourAndDelivery();
            mCTCPMTCTLabourAndDelivery.Name = "frmCTCPMTCTLabourAndDelivery";
            mCTCPMTCTLabourAndDelivery.Text = mItemLink.Caption;
            mCTCPMTCTLabourAndDelivery.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCPMTCTMotherChild
        private void Open_CTCPMTCTMotherChild(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmCTCPMTCTMotherChild mCTCPMTCTMotherChild = new frmCTCPMTCTMotherChild();
            mCTCPMTCTMotherChild.Name = "frmCTCPMTCTMotherChild";
            mCTCPMTCTMotherChild.Text = mItemLink.Caption;
            mCTCPMTCTMotherChild.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCContactableClinicStaff
        private void Open_CTCContactableClinicStaff(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            frmCTCContactableClinicStaff mCTCClinicStaff = new frmCTCContactableClinicStaff();
            mCTCClinicStaff.Name = "frmCTCContactableClinicStaff";
            mCTCClinicStaff.Text = mItemLink.Caption;
            mCTCClinicStaff.Show();

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #endregion

        #region ctcsetup

        #region Open_CTCMaritalStatus
        private void Open_CTCMaritalStatus(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmCTCMaritalStatus";
            string mGridName = "grdCTCMaritalStatus";

            if (this.Find_MdiChild(mFormName) == false)
            {
                frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.MaritalStatus);

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = mGridName;
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;

                mCTCCodes.Grid_Settings(mMainGrid.grdMain);
                mCTCCodes.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCFunctionalStatus
        private void Open_CTCFunctionalStatus(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmCTCFunctionalStatus";
            string mGridName = "grdCTCFunctionalStatus";

            if (this.Find_MdiChild(mFormName) == false)
            {
                frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.FunctionalStatus);

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = mGridName;
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;

                mCTCCodes.Grid_Settings(mMainGrid.grdMain);
                mCTCCodes.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCTBStatus
        private void Open_CTCTBStatus(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmCTCTBStatus";
            string mGridName = "grdCTCTBStatus";

            if (this.Find_MdiChild(mFormName) == false)
            {
                frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.TBStatus);

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = mGridName;
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;

                mCTCCodes.Grid_Settings(mMainGrid.grdMain);
                mCTCCodes.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCARVStatus
        private void Open_CTCARVStatus(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmCTCARVStatus";
            string mGridName = "grdCTCRVStatus";

            if (this.Find_MdiChild(mFormName) == false)
            {
                frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVStatus);

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = mGridName;
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;

                mCTCCodes.Grid_Settings(mMainGrid.grdMain);
                mCTCCodes.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCAIDSIllness
        private void Open_CTCAIDSIllness(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmCTCAIDSIllness";
            string mGridName = "grdCTCAIDSIllness";

            if (this.Find_MdiChild(mFormName) == false)
            {
                frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.AIDSIllness);

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = mGridName;
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;

                mCTCCodes.Grid_Settings(mMainGrid.grdMain);
                mCTCCodes.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCARVCombRegimens
        private void Open_CTCARVCombRegimens(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmCTCARVCombRegimens";
            string mGridName = "grdCTCARVCombRegimens";

            if (this.Find_MdiChild(mFormName) == false)
            {
                frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVCombRegimens);

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = mGridName;
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;

                mCTCCodes.Grid_Settings(mMainGrid.grdMain);
                mCTCCodes.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCARVAdherence
        private void Open_CTCARVAdherence(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmCTCARVAdherence";
            string mGridName = "grdCTCARVAdherence";

            if (this.Find_MdiChild(mFormName) == false)
            {
                frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVAdherence);

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = mGridName;
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;

                mCTCCodes.Grid_Settings(mMainGrid.grdMain);
                mCTCCodes.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCARVPoorAdherenceReasons
        private void Open_CTCARVPoorAdherenceReasons(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmCTCARVPoorAdherenceReasons";
            string mGridName = "grdCTCARVPoorAdherenceReasons";

            if (this.Find_MdiChild(mFormName) == false)
            {
                frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVPoorAdherenceReasons);

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = mGridName;
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;

                mCTCCodes.Grid_Settings(mMainGrid.grdMain);
                mCTCCodes.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCReferedTo
        private void Open_CTCReferedTo(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmCTCReferedTo";
            string mGridName = "grdCTCReferedTo";

            if (this.Find_MdiChild(mFormName) == false)
            {
                frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ReferedTo);

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = mGridName;
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;

                mCTCCodes.Grid_Settings(mMainGrid.grdMain);
                mCTCCodes.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCARVReasons
        private void Open_CTCARVReasons(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmCTCARVReasons";
            string mGridName = "grdCTCARVReasons";

            if (this.Find_MdiChild(mFormName) == false)
            {
                frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARVReasons);

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = mGridName;
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;

                mCTCCodes.Grid_Settings(mMainGrid.grdMain);
                mCTCCodes.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCFollowUpStatus
        private void Open_CTCFollowUpStatus(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmCTCFollowUpStatus";
            string mGridName = "grdCTCFollowUpStatus";

            if (this.Find_MdiChild(mFormName) == false)
            {
                frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.FollowUpStatus);

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = mGridName;
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;

                mCTCCodes.Grid_Settings(mMainGrid.grdMain);
                mCTCCodes.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCReferedFrom
        private void Open_CTCReferedFrom(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmCTCReferedFrom";
            string mGridName = "grdCTCReferedFrom";

            if (this.Find_MdiChild(mFormName) == false)
            {
                frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ReferedFrom);

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = mGridName;
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;

                mCTCCodes.Grid_Settings(mMainGrid.grdMain);
                mCTCCodes.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCPriorARVExposure
        private void Open_CTCPriorARVExposure(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmCTCPriorARVExposure";
            string mGridName = "grdCTCPriorARVExposure";

            if (this.Find_MdiChild(mFormName) == false)
            {
                frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.PriorARVExposure);

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = mGridName;
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;

                mCTCCodes.Grid_Settings(mMainGrid.grdMain);
                mCTCCodes.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCARTWhyEligible
        private void Open_CTCARTWhyEligible(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmCTCARTWhyEligible";
            string mGridName = "grdCTCARTWhyEligible";

            if (this.Find_MdiChild(mFormName) == false)
            {
                frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.ARTWhyEligible);

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = mGridName;
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;

                mCTCCodes.Grid_Settings(mMainGrid.grdMain);
                mCTCCodes.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCRelevantComeds
        private void Open_CTCRelevantComeds(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmCTCRelevantComeds";
            string mGridName = "grdCTCRelevantComeds";

            if (this.Find_MdiChild(mFormName) == false)
            {
                frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.RelevantComeds);

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = mGridName;
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;

                mCTCCodes.Grid_Settings(mMainGrid.grdMain);
                mCTCCodes.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_CTCAbnormalLabResults
        private void Open_CTCAbnormalLabResults(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmCTCAbnormalLabResults";
            string mGridName = "grdCTCAbnormalLabResults";

            if (this.Find_MdiChild(mFormName) == false)
            {
                frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.abnormallabresults);

                frmMainGrid mMainGrid = new frmMainGrid();
                mMainGrid.grdMain.Name = mGridName;
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;

                mCTCCodes.Grid_Settings(mMainGrid.grdMain);
                mCTCCodes.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #endregion

        #region sms

        #region Open_MessageTemplates
        private void Open_MessageTemplates(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmMessageTemplates";
           // string mGridName = "grdSMSMessageTemplates";

            if (this.Find_MdiChild(mFormName) == false)
            {
            frmMessageTemplates mSMSMessageTemplates = new frmMessageTemplates();
            mSMSMessageTemplates.MdiParent = Program.gMdiForm;;
            mSMSMessageTemplates.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

            
        }
        #endregion

        #region Open_SentMessage
        private void Open_SentMessage(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmSentMessages";
            

            if (this.Find_MdiChild(mFormName) == false)
            {


                frmSMSSentMessages mMainGrid = new frmSMSSentMessages();
                //mMainGrid.grdMain.Name = mGridName;
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;

               

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_ReceivedMessage
        private void Open_ReceivedMessage(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmReceivedMessages";
            string mGridName = "grdReceivedMessages";

            if (this.Find_MdiChild(mFormName) == false)
            {
                frmSMSReceivedMessages mMainGrid = new frmSMSReceivedMessages();
                //mMainGrid.grdMain.Name = mGridName;
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;

                //mCTCCodes.Grid_Settings(mMainGrid.grdMain);
                // mCTCCodes.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_PatientCategories
        private void Open_PatientCategories(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmSMSClientGroups";


            if (this.Find_MdiChild(mFormName) == false)
            {
                if (this.Find_MdiChild(mFormName) == false)
                {
                    frmSMSClientGroups mSMSClientGroups = new frmSMSClientGroups();
                    mSMSClientGroups.MdiParent = Program.gMdiForm;
                    mSMSClientGroups.Show();
                }
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_RegisteredPatients
        private void Open_RegisteredPatients(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmRegisteredPatients";
            string mGridName = "grdRegisteredPatients";

            if (this.Find_MdiChild(mFormName) == false)
            {
                //frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.MaritalStatus);

                frmSMSMobileRegistrations mMainGrid = new frmSMSMobileRegistrations();
                //mMainGrid.grdMain.Name = mGridName;
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;

                //mCTCCodes.Grid_Settings(mMainGrid.grdMain);
                // mCTCCodes.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_ActivePatients
        private void Open_ActivePatients(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmSMSRegisteredPatients";
            //string mGridName = "grdActivePatients";

            if (this.Find_MdiChild(mFormName) == false)
            {
                //frmCTCCodes mCTCCodes = new frmCTCCodes((int)AfyaPro_Types.clsEnums.CTCCodes.MaritalStatus);

                frmSMSRegisteredPatients mMainGrid = new frmSMSRegisteredPatients();
                //mMainGrid.grdMain.Name = mGridName;
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;

                //mCTCCodes.Grid_Settings(mMainGrid.grdMain);
                // mCTCCodes.Data_Fill(mMainGrid.grdMain);

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_SMSAgents
        private void Open_SMSAgents(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmSMSAgents";
           
            if (this.Find_MdiChild(mFormName) == false)
            {
                frmSMSAgents mMainGrid = new frmSMSAgents();
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;

             

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_Trash
        private void Open_Trash(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmTrashMessages";
           

            if (this.Find_MdiChild(mFormName) == false)
            {
                
                frmSMSTrashMessages mMainGrid = new frmSMSTrashMessages();
               
                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;
                                
                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_SendSMS
        private void Open_SendSMS(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmSMSSendMessage";


            if (this.Find_MdiChild(mFormName) == false)
            {

                frmSMSSendMessage mMainGrid = new frmSMSSendMessage();

                //mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_PatientsList
        private void Open_PatientsList(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmSMSMobilePatientsList";


            if (this.Find_MdiChild(mFormName) == false)
            {

                frmSMSMobilePatientsList mMainGrid = new frmSMSMobilePatientsList();

                mMainGrid.MdiParent = Program.gMdiForm;
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #region Open_ModuleSettings
        private void Open_ModuleSettings(DevExpress.XtraNavBar.NavBarItemLink mItemLink)
        {
            Program.gMdiForm.Cursor = Cursors.WaitCursor;

            string mFormName = "frmSMSModuleSettings";


            if (this.Find_MdiChild(mFormName) == false)
            {

                frmSMSModuleSettings mMainGrid = new frmSMSModuleSettings();
                mMainGrid.Name = mFormName;
                mMainGrid.Text = mItemLink.Caption;

                mMainGrid.Show();
            }

            Program.gMdiForm.Cursor = Cursors.Default;

        }
        #endregion

        #endregion
    }
}
