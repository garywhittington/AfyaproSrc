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
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Xml;
using DevExpress.XtraEditors;

namespace AfyaPro_NextGen
{
    static class Program
    {
        #region shortcut keys

        internal const Keys KeyCode_SeachPatient = Keys.F2;
        internal const Keys KeyCode_SearchBillingItem = Keys.F3;
        internal const Keys KeyCode_SearchDiagnosis = Keys.F3;
        internal const Keys KeyCode_Preview = Keys.F4;
        internal const Keys KeyCode_Save = Keys.F5;
        internal const Keys KeyCode_Ok = Keys.F5;
        internal const Keys KeyCode_Close = Keys.Escape;
        internal const Keys KeyCode_Refresh = Keys.F5;
        internal const Keys KeyCode_Book = Keys.F6;
        internal const Keys KeyCode_Admit = Keys.F6;
        internal const Keys KeyCode_Print = Keys.F6;
        internal const Keys KeyCode_ItemAdd = Keys.F12;
        internal const Keys KeyCode_ItemUpdate = Keys.F11;
        internal const Keys KeyCode_ItemRemove = Keys.F10;
        internal const Keys KeyCode_RchClear = Keys.F7;
        internal const Keys KeyCode_PrescribeLabTest = Keys.F8;
        internal const Keys KeyCode_ViewLabTestResults = Keys.F9;
        internal const Keys KeyCode_ViewPatientsQueue = Keys.F7;
        internal const Keys KeyCode_RchSearchClient = Keys.F2;
        internal const Keys KeyCode_RchGetService = Keys.F6;
        internal const Keys KeyCode_RchGetTreatment = Keys.F9;
        internal const Keys KeyCode_RchPostnatalChildren = Keys.F4;
        internal const Keys KeyCode_RchVaccinations = Keys.F8;

        #endregion

        #region RunTypes
        internal enum RunTypes
        {
            Trial = 0,
            Full,
            Expired,
            UnKnown,
        }

        #endregion

        internal static bool gDateChangeAtStartup = true;

        internal static DataTable gDtSearchPatients = null;
        internal static DataTable gDtSearchCTCPatients = null;

        #region declaration

        //internal
        internal static AfyaPro_MT.clsFacilitySetup gMdtFacilitySetup;
        internal static AfyaPro_MT.clsUserActions gMdtUserActions;

        internal static String gCurrentVersion = "";
        internal static String gApplicationName = "";
        internal static String gMiddleTier = "";
        internal static System.Globalization.CultureInfo gCulture;
        internal static DataTable gDtCompanySetup;
        internal static frmMain gMdiForm;
        internal static clsMainWindowCodes gMainWindowCodes = new clsMainWindowCodes();
        internal static DataTable gDtUserModuleItems;
        internal static DataTable gDtUserFunctionAccessKeys;
        internal static DataTable gDtModules;
        internal static DataTable gDtModuleItems;
        internal static DataTable gDtAccessFunctions;
        internal static String gMachineName = "";
        internal static String gMachineUser = "";
        internal static AfyaPro_Types.clsUser gCurrentUser = new AfyaPro_Types.clsUser();
        internal static DataTable gDtUserActions = new DataTable("useractions");
        internal static DataTable gDtUserStores = new DataTable("userstores");
        internal static DateTime gServerDate = new DateTime();
        internal static int gProcessId = 0;
        internal static string gIniFileName = "";
        internal static bool gWaitForInitialSetup = true;
        internal static bool gLoginOk = false;
        internal static DevExpress.LookAndFeel.DefaultLookAndFeel gDefaultLookAndFeel;
        internal static int gMainNavBarWidth = 211;
        internal static string gLanguageName = "";
        internal static string gDefaultLanguageName = "";
        internal static string gDefaultSkinName = "";
        internal static string gLocalCurrencyCode = "LocalCurr";
        internal static string gLocalCurrencyDesc = "";
        internal static string gLocalCurrencySymbol = "";
        internal static int gRoundingOption = 0;
        internal static int gRoundingFigure = 0;
        internal static int gRoundingDecimals = 0;
        internal static int gRoundingStrictness = 0;
        internal static int gRoundingMidpointOption = 0;
        internal static int gAffectStockAtCashier = 0;
        internal static int gDoubleEntryIssuing = 0;
        internal static int gTransferOutRefreshInterval = 1;
        internal static double gChildrenAgeLimit = 5;
        internal static DataTable gDtGridLang = new DataTable("gridlang");
        internal static DataTable gDtEditorsLang = new DataTable("editorslang");
        internal static DataTable gDtWizardLang = new DataTable("wizardlang");
        internal static AutoCompleteStringCollection gCommonSurnames = new AutoCompleteStringCollection();
        internal static AutoCompleteStringCollection gCommonFirstNames = new AutoCompleteStringCollection();
        internal static AutoCompleteStringCollection gCommonOtherNames = new AutoCompleteStringCollection();

        //private
        private static AfyaPro_MT.clsUsers pMdtUsers;
        private static AfyaPro_MT.clsCurrencies pMdtCurrencies;
        private static AfyaPro_MT.clsUserGroupPrinters pMdtUserGroupPrinters;
        private static AfyaPro_MT.clsLanguage pMdtLanguage;
        private static AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();
        private static AfyaPro_MT.clsPatientExtraFields pMdtPatientExtraFields;

        internal static DataTable gDtFormSizes = new DataTable("formsizes");
        private static DataTable pDtMessages = new DataTable("messages");
        private static DataTable pDtUserGroupPrinters = new DataTable("printers");
        private static string pServerName = "";
        private static Int16 pServerPort = 2008;
        private static string pClassName = "";
        internal static DataTable gDtActiveModules = new DataTable("activemodules");
        internal static DataTable gDtUserReports = new DataTable("userreports");

        #endregion

        #region Main
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.UserSkins.BonusSkins.Register();

            gDefaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            gDefaultLookAndFeel.LookAndFeel.SkinName = "Office 2007 Green";
            gDefaultLookAndFeel.LookAndFeel.UseDefaultLookAndFeel = false;
            gDefaultSkinName = gDefaultLookAndFeel.LookAndFeel.SkinName;

            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Startup_Code();
        }
        #endregion

        #region Startup_Code
        internal static void Startup_Code()
        {
            String mFunctionName = "Startup_Code";

            try
            {
                gMachineName = System.Windows.Forms.SystemInformation.ComputerName;
                gMachineUser = System.Windows.Forms.SystemInformation.UserName;

                
                //version information
                gCurrentVersion = Application.ProductVersion;
                gApplicationName = "AfyaPro " + gCurrentVersion;

                //full classname
                pClassName = "AfyaPro_Client.App";

                #region initial setup

                //get location of system directory
                //gIniFileName = System.Environment.SystemDirectory + "/afyaproclient.xml";
                gIniFileName = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/afyaproclient.xml";

                //prompty to do initial setup if not yet
                if (File.Exists(gIniFileName) == false)
                {
                    gWaitForInitialSetup = true;
                    frmSetup mSetup = new frmSetup();
                    mSetup.ShowDialog();
                }
                else
                {
                    //read config file and retrieve the settings
                    if (Read_Config_Settings() == false)
                    {
                        //do nothing if anything went wrong
                        return;
                    }
                }

                gMiddleTier = "tcp://" + pServerName + ":" + pServerPort + "/";

                #endregion

                pMdtUsers = (AfyaPro_MT.clsUsers)Activator.GetObject(
                    typeof(AfyaPro_MT.clsUsers),
                    gMiddleTier + "clsUsers");

                #region check for licence

                AfyaPro_Types.clsLicence mLicence = pMdtUsers.Check_Licence(DateTime.Now);

                if (mLicence.FullyActive == false)
                {
                    frmRCHAntenatalVisitDetails1 PassDialog = new frmRCHAntenatalVisitDetails1(mLicence.BaseString,
                                                                mLicence.TrialDays,
                                                                mLicence.InfoText,
                                                                mLicence.FullyActive);

                    DialogResult DR = PassDialog.ShowDialog();
                    RunTypes mRunType;

                    if (DR == System.Windows.Forms.DialogResult.OK)
                    {
                        mRunType = RunTypes.Full;
                    }
                    else if (DR == DialogResult.Retry)
                    {
                        mRunType = RunTypes.Trial;
                    }
                    else
                    {
                        mRunType = RunTypes.Expired;
                    }

                    if (mRunType != RunTypes.Full && mRunType != RunTypes.Trial)
                    {
                        return;
                    }
                }

                #region active modules

                gDtActiveModules.Columns.Add("activationkey", typeof(System.String));

                string[] mModuleKeys = mLicence.ModuleKeys.Split(',');

                foreach (string mModuleKey in mModuleKeys)
                {
                    if (mModuleKey.Trim() != "")
                    {
                        DataRow mNewRow = gDtActiveModules.NewRow();
                        mNewRow["activationkey"] = mModuleKey.Trim();
                        gDtActiveModules.Rows.Add(mNewRow);
                        gDtActiveModules.AcceptChanges();
                    }
                }

                #endregion

                #endregion

                #region these are needed by login screen

                gMdtFacilitySetup = (AfyaPro_MT.clsFacilitySetup)Activator.GetObject(
                    typeof(AfyaPro_MT.clsFacilitySetup),
                    gMiddleTier + "clsFacilitySetup");

                pMdtLanguage = (AfyaPro_MT.clsLanguage)Activator.GetObject(
                    typeof(AfyaPro_MT.clsLanguage),
                    gMiddleTier + "clsLanguage");

                //Get_UserSettings
                Get_UserSettings(false);

                #endregion

                //load login screen
                frmLogin mLogin = new frmLogin();
                mLogin.ShowDialog();
                if (gLoginOk == false)
                {
                    return;
                }

                #region now load application wide settings

                gMdtUserActions = (AfyaPro_MT.clsUserActions)Activator.GetObject(
                    typeof(AfyaPro_MT.clsUserActions),
                    gMiddleTier + "clsUserActions");

                pMdtCurrencies = (AfyaPro_MT.clsCurrencies)Activator.GetObject(
                    typeof(AfyaPro_MT.clsCurrencies),
                    Program.gMiddleTier + "clsCurrencies");

                pMdtUserGroupPrinters = (AfyaPro_MT.clsUserGroupPrinters)Activator.GetObject(
                    typeof(AfyaPro_MT.clsUserGroupPrinters),
                    gMiddleTier + "clsUserGroupPrinters");

                pMdtPatientExtraFields = (AfyaPro_MT.clsPatientExtraFields)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPatientExtraFields),
                    gMiddleTier + "clsPatientExtraFields");

                pMdtUsers = (AfyaPro_MT.clsUsers)Activator.GetObject(
                    typeof(AfyaPro_MT.clsUsers),
                    gMiddleTier + "clsUsers");

                //Get_FacilitySetup
                Get_FacilitySetup();

                //Set_Culture
                Set_Culture();

                //Server Date
                gServerDate = gMdtFacilitySetup.Get_ServerDate();

                //Get_FormSizes
                Get_FormSizes();

                //Get_UserGroupPrinters
                Get_UserGroupPrinters();

                //Get_UserActions
                Get_UserActions();

                //Get_LocalCurrency
                Get_LocalCurrency();

                //Load_CommonNames
                Load_CommonNames();

                //Add User Login Information
                Add_UserLogin();

                #endregion

                //load main application
                gMdiForm = new frmMain();
                gMdiForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Display_Error(pClassName, mFunctionName, ex.Message);
                Environment.Exit(1);
            }
        }
        #endregion

        #region Load_CommonNames
        private static void Load_CommonNames()
        {
            string mFunctionName = "Load_CommonNames";

            try
            {
                //surnames
                DataTable mDtSurnames = pMdtPatientExtraFields.View_Surnames("", "description");
                gCommonSurnames.Clear();
                foreach (DataRow mDataRow in mDtSurnames.Rows)
                {
                    gCommonSurnames.Add(mDataRow["description"].ToString().Trim());
                }

                //firstnames
                DataTable mDtFirstnames = pMdtPatientExtraFields.View_Firstnames("", "description");
                gCommonFirstNames.Clear();
                foreach (DataRow mDataRow in mDtFirstnames.Rows)
                {
                    gCommonFirstNames.Add(mDataRow["description"].ToString().Trim());
                }

                //othernames
                DataTable mDtOthernames = pMdtPatientExtraFields.View_Othernames("", "description");
                gCommonOtherNames.Clear();
                foreach (DataRow mDataRow in mDtOthernames.Rows)
                {
                    gCommonOtherNames.Add(mDataRow["description"].ToString().Trim());
                }

                ////load patients
                //AfyaPro_MT.clsSearchEngine mSearchEngine = (AfyaPro_MT.clsSearchEngine)Activator.GetObject(
                //    typeof(AfyaPro_MT.clsSearchEngine),
                //    gMiddleTier + "clsSearchEngine");

                //gDtSearchPatients = mSearchEngine.Get_Patients("regdate<'" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "'", "");
                //gDtSearchCTCPatients = mSearchEngine.Get_CTCClients("regdate<'" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "'", "");
            }
            catch (Exception ex)
            {
                Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Update_CommonNames
        internal static void Update_CommonNames(string mSurname, string mFirstName, string mOtherNames)
        {
            string mFunctionName = "Update_CommonNames";

            try
            {
                //surname
                if (gCommonSurnames.Contains(mSurname.Trim()) == false)
                {
                    gCommonSurnames.Add(mSurname);
                }
                //firstname
                if (gCommonFirstNames.Contains(mFirstName.Trim()) == false)
                {
                    gCommonFirstNames.Add(mFirstName);
                }
                //othernames
                if (mOtherNames.Trim() != "")
                {
                    if (gCommonOtherNames.Contains(mOtherNames.Trim()) == false)
                    {
                        gCommonOtherNames.Add(mOtherNames);
                    }
                }
            }
            catch (Exception ex)
            {
                Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Set_Culture
        private static void Set_Culture()
        {
            String mFunctionName = "Set_Culture";

            try
            {
                //set culture
                gCulture = new System.Globalization.CultureInfo("en-GB");
                System.Threading.Thread.CurrentThread.CurrentCulture = gCulture;

                //curreny
                gCulture.NumberFormat.CurrencySymbol = "";
                gCulture.NumberFormat.CurrencyGroupSeparator = ",";
                gCulture.NumberFormat.CurrencyDecimalDigits = 2;
                gCulture.NumberFormat.CurrencyDecimalSeparator = ".";

                //date
                gCulture.DateTimeFormat.LongDatePattern = "dd/MM/yyyy hh:mm tt";
                gCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
                gCulture.DateTimeFormat.ShortTimePattern = "hh:mm tt";
                gCulture.DateTimeFormat.AMDesignator = "AM";
                gCulture.DateTimeFormat.PMDesignator = "PM";
            }
            catch (Exception ex)
            {
                Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Read_Config_Settings

        internal static bool Read_Config_Settings()
        {
            XmlTextReader mXmlTextReader;
            FileStream mFileSystemIn;
            StreamReader mStreamReader;

            try
            {
                mFileSystemIn = new FileStream(gIniFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                mStreamReader = new StreamReader(mFileSystemIn);
                mXmlTextReader = new XmlTextReader(mStreamReader);

                while (mXmlTextReader.Read())
                {
                    //servername
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "servername")
                    {
                        pServerName = mXmlTextReader.ReadElementString("servername");
                    }

                    //serverport
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "serverport")
                    {
                        pServerPort = System.Convert.ToInt16(mXmlTextReader.ReadElementString("serverport"));
                    }
                }

                mXmlTextReader.Close();
                mStreamReader.Close();
                mFileSystemIn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Get_FacilitySetup
        internal static void Get_FacilitySetup()
        {
            string mFunctionName = "Get_FacilitySetup";

            try
            {
                gDtCompanySetup = gMdtFacilitySetup.View("", "");
                gServerDate = gMdtFacilitySetup.Get_ServerDate();

                if (gDtCompanySetup.Rows.Count > 0)
                {
                    gRoundingOption = Convert.ToInt16(gDtCompanySetup.Rows[0]["roundingoption"]);
                    gRoundingFigure = Convert.ToInt16(gDtCompanySetup.Rows[0]["roundingfigure"]);
                    gRoundingDecimals = Convert.ToInt16(gDtCompanySetup.Rows[0]["roundingdecimals"]);
                    gRoundingStrictness = Convert.ToInt16(gDtCompanySetup.Rows[0]["roundingstrictness"]);
                    gRoundingMidpointOption = Convert.ToInt16(gDtCompanySetup.Rows[0]["roundingmidpointoption"]);
                    gAffectStockAtCashier = Convert.ToInt16(gDtCompanySetup.Rows[0]["affectstockatcashier"]);
                    gDoubleEntryIssuing = Convert.ToInt16(gDtCompanySetup.Rows[0]["doubleentryissuing"]);
                    gTransferOutRefreshInterval = Convert.ToInt16(gDtCompanySetup.Rows[0]["transferoutrefreshinterval"]);
                }
            }
            catch (Exception ex)
            {
                Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Round_Number
        internal static double Round_Number(double mNumber)
        {
            switch (gRoundingOption)
            {
                case 1://up
                    {
                        #region rounding up codes

                        if (gRoundingFigure > 0)
                        {
                            double mNewNumber = mNumber;

                            if (gRoundingStrictness == 1)
                            {
                                mNewNumber = mNewNumber + ((gRoundingFigure / 10) * 5);
                            }

                            if (gRoundingMidpointOption == 1)
                            {
                                mNewNumber = Math.Round(mNewNumber / gRoundingFigure, 0, MidpointRounding.ToEven) * gRoundingFigure;
                            }
                            else
                            {
                                mNewNumber = Math.Round(mNewNumber / gRoundingFigure, 0, MidpointRounding.AwayFromZero) * gRoundingFigure;
                            }

                            if (mNewNumber - mNumber != gRoundingFigure)
                            {
                                mNumber = mNewNumber;
                            }
                        }
                        else
                        {
                            if (gRoundingStrictness == 1)
                            {
                                string mFractionStr = "0.".PadRight(gRoundingDecimals + 2, '0') + "5";
                                mNumber = mNumber + Convert.ToDouble(mFractionStr);
                            }

                            if (gRoundingMidpointOption == 1)
                            {
                                mNumber = Math.Round(mNumber, gRoundingDecimals, MidpointRounding.ToEven);
                            }
                            else
                            {
                                mNumber = Math.Round(mNumber, gRoundingDecimals, MidpointRounding.AwayFromZero);
                            }
                        }

                        #endregion
                    }
                    break;
                case 2://down
                    {
                        #region rounding down codes

                        double mNewNumber = mNumber;

                        if (gRoundingFigure > 0)
                        {
                            mNewNumber = Math.Floor(mNewNumber / gRoundingFigure) * gRoundingFigure;

                            if (mNewNumber - mNumber != gRoundingFigure)
                            {
                                mNumber = mNewNumber;
                            }
                        }

                        #endregion
                    }
                    break;
            }

            return mNumber;
        }
        #endregion

        #region Get_LocalCurrency
        internal static void Get_LocalCurrency()
        {
            string mFunctionName = "Get_LocalCurrency";

            try
            {
                DataTable mDtCurrencies = pMdtCurrencies.View("code='"
                    + Program.gLocalCurrencyCode.Trim() + "'", "", "", "");
                if (mDtCurrencies.Rows.Count > 0)
                {
                    Program.gLocalCurrencyDesc = mDtCurrencies.Rows[0]["description"].ToString().Trim();
                    Program.gLocalCurrencySymbol = mDtCurrencies.Rows[0]["currencysymbol"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Get_FormSizes
        private static void Get_FormSizes()
        {
            try
            {
                gDtFormSizes = gMdtFacilitySetup.Get_FormSizes("layoutname='" 
                    + gCurrentUser.UserGroupFormsLayoutTemplateName.Trim() + "'", "");
            }
            catch { }
        }
        #endregion

        #region Get_UserSettings
        internal static void Get_UserSettings(bool mUserSpecific)
        {
            string mFunctionName = "Get_UserSettings";

            try
            {
                DataTable mDtSettings = gMdtFacilitySetup.Get_UserSettings(gMachineName, gCurrentUser.Code);

                if (mDtSettings.Rows.Count > 0)
                {
                    if (mUserSpecific == true)
                    {
                        gMainNavBarWidth = Convert.ToInt32(mDtSettings.Rows[0]["usermainnavbarwidth"]);
                        gDefaultSkinName = mDtSettings.Rows[0]["userdefaultskinname"].ToString().Trim();
                        gDefaultLanguageName = mDtSettings.Rows[0]["userdefaultlanguage"].ToString().Trim();
                        gLanguageName = mDtSettings.Rows[0]["userdefaultlanguage"].ToString().Trim();
                    }
                    else
                    {
                        gDefaultSkinName = mDtSettings.Rows[0]["defaultskinname"].ToString().Trim();
                        gDefaultLanguageName = mDtSettings.Rows[0]["defaultlanguage"].ToString().Trim();
                        gLanguageName = mDtSettings.Rows[0]["defaultlanguage"].ToString().Trim();
                    }
                }

                if (gDefaultSkinName.Trim() != "")
                {
                    gDefaultLookAndFeel.LookAndFeel.SkinName = gDefaultSkinName;
                }

                Get_CurrentLanguage();
            }
            catch (Exception ex)
            {
                Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Save_UserSettings
        internal static void Save_UserSettings(int mNavBarWidth)
        {
            string mFunctionName = "Save_UserSettings";

            try
            {
                gMdtFacilitySetup.Save_UserSettings(
                    Program.gMachineName, mNavBarWidth, Program.gDefaultSkinName,
                    Program.gDefaultLanguageName, Program.gCurrentUser.Code);
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Add_UserLogin
        internal static void Add_UserLogin()
        {
            string mFunctionName = "Add_UserLogin";

            //Get process Id 
            gProcessId = System.Diagnostics.Process.GetCurrentProcess().Id;
          
            try
            {
                gMdtFacilitySetup.Add_UserLogin(
                    Program.gMachineName, Program.gCurrentUser.Code, gServerDate, gProcessId);
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Update_UserLogin
        internal static void Update_UserLogin()
        {
            string mFunctionName = "Update_UserLogin";

            try
            {
                gMdtFacilitySetup.Edit_UserLogin(
                    Program.gMachineName, Program.gCurrentUser.Code, gServerDate, gProcessId);
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Get_CurrentLanguage
        internal static void Get_CurrentLanguage()
        {
            string mFunctionName = "Get_CurrentLanguage";

            try
            {
                pDtMessages = pMdtLanguage.Get_Language(Program.gLanguageName, "ClientMessages");
                gDtGridLang = pMdtLanguage.Get_Language(Program.gLanguageName, "XtraGrid");
                gDtEditorsLang = pMdtLanguage.Get_Language(Program.gLanguageName, "XtraEditor");
                gDtWizardLang = pMdtLanguage.Get_Language(Program.gLanguageName, "XtraWizard");

                DevExpress.XtraGrid.Localization.GridLocalizer.Active = new clsGridLocalizer();
                DevExpress.XtraEditors.Controls.Localizer.Active = new clsEditorsLocalizer();
                DevExpress.XtraWizard.Localization.WizardLocalizer.Active = new clsWizardLocalizer();
            }
            catch (Exception ex)
            {
                Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Get_UserActions
        internal static void Get_UserActions()
        {
            string mFunctionName = "Get_UserActions";

            try
            {
                gDtUserActions = gMdtUserActions.View_Searching(gCurrentUser.Code.Trim());

                gDtUserStores = pMdtUsers.View_StoreUsers("usercode='" + gCurrentUser.Code.Trim() + "'", "");
            }
            catch (Exception ex)
            {
                Display_Error("AfyaPro_Client.App", mFunctionName, ex.Message);
            }
        }
        #endregion

        #region Get_UserGroupPrinters
        internal static void Get_UserGroupPrinters()
        {
            string mFunctionName = "Get_UserGroupPrinters";

            try
            {
                pDtUserGroupPrinters = pMdtUserGroupPrinters.View(
                    "machinename='" + gMachineName + "' and usergroupcode='" + gCurrentUser.UserGroupCode + "'", "", "", "");
            }
            catch (Exception ex)
            {
                Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Get_PrinterName
        internal static string Get_PrinterName(string mDocumentTypeCode)
        {
            string mFunctionName = "Get_PrinterName";

            try
            {
                DataView mDvPrinters = new DataView();
                mDvPrinters.Table = pDtUserGroupPrinters;
                mDvPrinters.Sort = "documenttypecode";

                int mRowIndex = mDvPrinters.Find(mDocumentTypeCode.Trim());

                if (mRowIndex >= 0)
                {
                    return @mDvPrinters[mRowIndex]["printername"].ToString().Trim();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                Display_Error(pClassName, mFunctionName, ex.Message);
                return "";
            }
        }
        #endregion

        #region TransLate
        public static string TransLate(string mMessage)
        {
            string mTranslated = mMessage;

            if (pDtMessages == null)
            {
                return mTranslated;
            }

            DataView mDvMessages = new DataView();
            mDvMessages.Table = pDtMessages;
            mDvMessages.Sort = "controlname";

            Int32 mRowIndex = mDvMessages.Find(mMessage);
            if (mRowIndex >= 0)
            {
                mTranslated = mDvMessages[mRowIndex]["description"].ToString().Trim();
            }

            return mTranslated;
        }
        #endregion

        #region Apply_Language
        internal static void Apply_Language(string mObjectName, List<Object> mObjects)
        {
            Int32 mRowIndex = -1;

            try
            {
                DataTable mDtPageLang = pMdtLanguage.Get_Language(Program.gLanguageName, mObjectName);
                if (mDtPageLang == null)
                {
                    return;
                }

                DataView mDvPageLang = new DataView();
                mDvPageLang.Table = mDtPageLang;
                mDvPageLang.Sort = "controlname";

                int mRadioGroupItemCount = 0;
                int mWizardPageCount = 0;
                for (Int32 mRow = 0; mRow < mObjects.Count; mRow++)
                {
                    #region LabelControl
                    if (mObjects[mRow] is LabelControl)
                    {
                        LabelControl mLabelControl = (LabelControl)mObjects[mRow];
                        mRowIndex = mDvPageLang.Find(mLabelControl.Name.Trim());
                        if (mRowIndex >= 0)
                        {
                            mLabelControl.Text = mDvPageLang[mRowIndex]["description"].ToString().Trim();
                        }
                    }
                    #endregion

                    #region SimpleButton
                    else if (mObjects[mRow] is SimpleButton)
                    {
                        SimpleButton mSimpleButton = (SimpleButton)mObjects[mRow];
                        mRowIndex = mDvPageLang.Find(mSimpleButton.Name.Trim());
                        if (mRowIndex >= 0)
                        {
                            mSimpleButton.Text = mDvPageLang[mRowIndex]["description"].ToString().Trim();
                        }
                    }
                    #endregion

                    #region GroupControl
                    else if (mObjects[mRow] is GroupControl)
                    {
                        GroupControl mGroupControl = (GroupControl)mObjects[mRow];
                        mRowIndex = mDvPageLang.Find(mGroupControl.Name.Trim());
                        if (mRowIndex >= 0)
                        {
                            mGroupControl.Text = mDvPageLang[mRowIndex]["description"].ToString().Trim();
                        }
                    }
                    #endregion

                    #region CheckEdit
                    else if (mObjects[mRow] is CheckEdit)
                    {
                        CheckEdit mCheckEdit = (CheckEdit)mObjects[mRow];
                        mRowIndex = mDvPageLang.Find(mCheckEdit.Name.Trim());
                        if (mRowIndex >= 0)
                        {
                            mCheckEdit.Text = mDvPageLang[mRowIndex]["description"].ToString().Trim();
                        }
                    }
                    #endregion

                    #region DevExpress.XtraBars.Ribbon.RibbonPage
                    else if (mObjects[mRow] is DevExpress.XtraBars.Ribbon.RibbonPage)
                    {
                        DevExpress.XtraBars.Ribbon.RibbonPage mRibbonPage = (DevExpress.XtraBars.Ribbon.RibbonPage)mObjects[mRow];
                        mRowIndex = mDvPageLang.Find(mRibbonPage.Name.Trim());
                        if (mRowIndex >= 0)
                        {
                            mRibbonPage.Text = mDvPageLang[mRowIndex]["description"].ToString().Trim();
                        }
                    }
                    #endregion

                    #region DevExpress.XtraBars.BarEditItem
                    else if (mObjects[mRow] is DevExpress.XtraBars.BarEditItem)
                    {
                        DevExpress.XtraBars.BarEditItem mBarEditItem = (DevExpress.XtraBars.BarEditItem)mObjects[mRow];
                        mRowIndex = mDvPageLang.Find(mBarEditItem.Name.Trim());
                        if (mRowIndex >= 0)
                        {
                            mBarEditItem.Caption = mDvPageLang[mRowIndex]["description"].ToString().Trim();
                        }
                    }
                    #endregion

                    #region DevExpress.XtraBars.BarButtonItem
                    else if (mObjects[mRow] is DevExpress.XtraBars.BarButtonItem)
                    {
                        DevExpress.XtraBars.BarButtonItem mBarButtonItem = (DevExpress.XtraBars.BarButtonItem)mObjects[mRow];
                        mRowIndex = mDvPageLang.Find(mBarButtonItem.Name.Trim());
                        if (mRowIndex >= 0)
                        {
                            mBarButtonItem.Caption = mDvPageLang[mRowIndex]["description"].ToString().Trim();
                        }
                    }
                    #endregion

                    #region DevExpress.XtraNavBar.NavBarGroup
                    else if (mObjects[mRow] is DevExpress.XtraNavBar.NavBarGroup)
                    {
                        DevExpress.XtraNavBar.NavBarGroup mNavBarGroup = (DevExpress.XtraNavBar.NavBarGroup)mObjects[mRow];
                        mRowIndex = mDvPageLang.Find(mNavBarGroup.Name.Trim());
                        if (mRowIndex >= 0)
                        {
                            mNavBarGroup.Caption = mDvPageLang[mRowIndex]["description"].ToString().Trim();
                        }
                    }
                    #endregion

                    #region DevExpress.XtraNavBar.NavBarItem
                    else if (mObjects[mRow] is DevExpress.XtraNavBar.NavBarItem)
                    {
                        DevExpress.XtraNavBar.NavBarItem mNavBarItem = (DevExpress.XtraNavBar.NavBarItem)mObjects[mRow];
                        mRowIndex = mDvPageLang.Find(mNavBarItem.Name.Trim());
                        if (mRowIndex >= 0)
                        {
                            mNavBarItem.Caption = mDvPageLang[mRowIndex]["description"].ToString().Trim();
                        }
                    }
                    #endregion

                    #region DevExpress.XtraEditors.Controls.RadioGroupItem
                    else if (mObjects[mRow] is DevExpress.XtraEditors.Controls.RadioGroupItem)
                    {
                        DevExpress.XtraEditors.Controls.RadioGroupItem mRadioGroupItem =
                            (DevExpress.XtraEditors.Controls.RadioGroupItem)mObjects[mRow];
                        mRowIndex = mDvPageLang.Find("radioGroupItem" + mRadioGroupItemCount);
                        if (mRowIndex >= 0)
                        {
                            mRadioGroupItem.Description = mDvPageLang[mRowIndex]["description"].ToString().Trim();
                        }

                        mRadioGroupItemCount++;
                    }
                    #endregion

                    #region DevExpress.XtraLayout.LayoutControlItem
                    else if (mObjects[mRow] is DevExpress.XtraLayout.LayoutControlItem)
                    {
                        DevExpress.XtraLayout.LayoutControlItem mLayoutControlItem =
                            (DevExpress.XtraLayout.LayoutControlItem)mObjects[mRow];
                        mRowIndex = mDvPageLang.Find(mLayoutControlItem.Name.Trim());
                        if (mRowIndex >= 0)
                        {
                            mLayoutControlItem.Text = mDvPageLang[mRowIndex]["description"].ToString().Trim();
                            mLayoutControlItem.CustomizationFormText = mDvPageLang[mRowIndex]["description"].ToString().Trim();
                        }
                    }
                    #endregion

                    #region DevExpress.XtraLayout.LayoutControlGroup
                    else if (mObjects[mRow] is DevExpress.XtraLayout.LayoutControlGroup)
                    {
                        DevExpress.XtraLayout.LayoutControlGroup mLayoutControlGroup =
                            (DevExpress.XtraLayout.LayoutControlGroup)mObjects[mRow];
                        mRowIndex = mDvPageLang.Find(mLayoutControlGroup.Name.Trim());
                        if (mRowIndex >= 0)
                        {
                            mLayoutControlGroup.Text = mDvPageLang[mRowIndex]["description"].ToString().Trim();
                            mLayoutControlGroup.CustomizationFormText = mDvPageLang[mRowIndex]["description"].ToString().Trim();
                        }
                    }
                    #endregion

                    #region DevExpress.XtraWizard.WelcomeWizardPage
                    else if (mObjects[mRow] is DevExpress.XtraWizard.WelcomeWizardPage)
                    {
                        DevExpress.XtraWizard.WelcomeWizardPage mWelcomeWizardPage =
                            (DevExpress.XtraWizard.WelcomeWizardPage)mObjects[mRow];

                        //Text
                        mRowIndex = mDvPageLang.Find("pagewelcometext");
                        if (mRowIndex >= 0)
                        {
                            mWelcomeWizardPage.Text = mDvPageLang[mRowIndex]["description"].ToString().Trim();
                        }

                        //IntroductionText
                        mRowIndex = mDvPageLang.Find("pagewelcomeintroductiontext");
                        if (mRowIndex >= 0)
                        {
                            mWelcomeWizardPage.IntroductionText = mDvPageLang[mRowIndex]["description"].ToString().Trim();
                        }

                        //ProceedText
                        mRowIndex = mDvPageLang.Find("pagewelcomeproceedtext");
                        if (mRowIndex >= 0)
                        {
                            mWelcomeWizardPage.ProceedText = mDvPageLang[mRowIndex]["description"].ToString().Trim();
                        }
                    }
                    #endregion

                    #region DevExpress.XtraWizard.WizardPage
                    else if (mObjects[mRow] is DevExpress.XtraWizard.WizardPage)
                    {
                        DevExpress.XtraWizard.WizardPage mWizardPage =
                            (DevExpress.XtraWizard.WizardPage)mObjects[mRow];

                        //Text
                        mRowIndex = mDvPageLang.Find("page" + mWizardPageCount + "text");
                        if (mRowIndex >= 0)
                        {
                            mWizardPage.Text = mDvPageLang[mRowIndex]["description"].ToString().Trim();
                        }

                        //DescriptionText
                        mRowIndex = mDvPageLang.Find("page" + mWizardPageCount + "descriptiontext");
                        if (mRowIndex >= 0)
                        {
                            mWizardPage.DescriptionText = mDvPageLang[mRowIndex]["description"].ToString().Trim();
                        }

                        mWizardPageCount++;
                    }
                    #endregion

                    #region DevExpress.XtraWizard.CompletionWizardPage
                    else if (mObjects[mRow] is DevExpress.XtraWizard.CompletionWizardPage)
                    {
                        DevExpress.XtraWizard.CompletionWizardPage mCompletionWizardPage =
                            (DevExpress.XtraWizard.CompletionWizardPage)mObjects[mRow];

                        //Text
                        mRowIndex = mDvPageLang.Find("pagefinishtext");
                        if (mRowIndex >= 0)
                        {
                            mCompletionWizardPage.Text = mDvPageLang[mRowIndex]["description"].ToString().Trim();
                        }

                        //FinishText
                        mRowIndex = mDvPageLang.Find("pagefinishfinishtext");
                        if (mRowIndex >= 0)
                        {
                            mCompletionWizardPage.FinishText = mDvPageLang[mRowIndex]["description"].ToString().Trim();
                        }

                        //ProceedText
                        mRowIndex = mDvPageLang.Find("pagefinishproceedtext");
                        if (mRowIndex >= 0)
                        {
                            mCompletionWizardPage.ProceedText = mDvPageLang[mRowIndex]["description"].ToString().Trim();
                        }
                    }
                    #endregion

                    #region DevExpress.XtraGrid.Columns.GridColumn
                    else if (mObjects[mRow] is DevExpress.XtraGrid.Columns.GridColumn)
                    {
                        DevExpress.XtraGrid.Columns.GridColumn mGridColumn =
                            (DevExpress.XtraGrid.Columns.GridColumn)mObjects[mRow];
                        mRowIndex = mDvPageLang.Find("col" + mGridColumn.FieldName.Trim());
                        if (mRowIndex >= 0)
                        {
                            mGridColumn.Caption = mDvPageLang[mRowIndex]["description"].ToString().Trim();
                        }
                    }
                    #endregion
                }
            }
            catch
            {
            }
        }
        #endregion

        #region Apply_Language
        internal static void Apply_Language(DataTable mDtLanguage, List<Object> mObjects)
        {
            Int32 mRowIndex = -1;

            try
            {
                DataView mDvLanguage = new DataView();
                if (mDtLanguage == null)
                {
                    return;
                }

                mDvLanguage.Table = mDtLanguage;
                mDvLanguage.Sort = "controlname";

                for (Int32 mRow = 0; mRow < mObjects.Count; mRow++)
                {
                    if (mObjects[mRow] is LabelControl)
                    {
                        LabelControl mLabelControl = (LabelControl)mObjects[mRow];
                        mRowIndex = mDvLanguage.Find(mLabelControl.Name.Trim());
                        if (mRowIndex >= 0)
                        {
                            mLabelControl.Text = mDvLanguage[mRowIndex]["description"].ToString().Trim();
                        }
                    }
                    else if (mObjects[mRow] is SimpleButton)
                    {
                        SimpleButton mSimpleButton = (SimpleButton)mObjects[mRow];
                        mRowIndex = mDvLanguage.Find(mSimpleButton.Name.Trim());
                        if (mRowIndex >= 0)
                        {
                            mSimpleButton.Text = mDvLanguage[mRowIndex]["description"].ToString().Trim();
                        }
                    }
                }
            }
            catch
            {
            }
        }
        #endregion

        #region Save_FormLayout
        internal static void Save_FormLayout(DevExpress.XtraEditors.XtraForm mXtraForm,
            DevExpress.XtraLayout.LayoutControl mLayoutControl, string mFormName)
        {
            try
            {
                if (mLayoutControl.IsModified == true)
                {
                    MemoryStream mMemoryStream = new MemoryStream();
                    mLayoutControl.SaveLayoutToStream(mMemoryStream);

                    frmGENSaveFormLayout mGENSaveFormLayout = new frmGENSaveFormLayout(
                        mXtraForm, mFormName, mMemoryStream.ToArray());

                    mGENSaveFormLayout.ShowDialog();
                }
            }
            catch { }
        }
        #endregion

        #region Save_GridLayout
        internal static void Save_GridLayout(DevExpress.XtraGrid.GridControl mGridControl, string mGridName)
        {
            try
            {
                MemoryStream mMemoryStream = new MemoryStream();
                mGridControl.MainView.SaveLayoutToStream(mMemoryStream);

                Program.gMdtFacilitySetup.Save_GridLayout(mGridName, Program.gCurrentUser.Code, mMemoryStream.ToArray());
            }
            catch { }
        }
        #endregion

        #region Save_FormSize
        internal static void Save_FormSize(string mFormName, DevExpress.XtraEditors.XtraForm mForm)
        {
            try
            {
                DataView mDvFormSizes = new DataView();
                mDvFormSizes.Table = Program.gDtFormSizes;
                mDvFormSizes.Sort = "formname";

                int mRowIndex = mDvFormSizes.Find(mFormName);
                if (mRowIndex >= 0)
                {
                    mDvFormSizes.BeginInit();

                    mDvFormSizes[mRowIndex]["formwidth"] = mForm.Width;
                    mDvFormSizes[mRowIndex]["formheight"] = mForm.Height;

                    mDvFormSizes.EndInit();
                    gDtFormSizes.AcceptChanges();
                }
                else
                {
                    DataRow mNewRow = gDtFormSizes.NewRow();
                    mNewRow["formname"] = mFormName;
                    mNewRow["formwidth"] = mForm.Width;
                    mNewRow["formheight"] = mForm.Height;
                    gDtFormSizes.Rows.Add(mNewRow);
                    gDtFormSizes.AcceptChanges();
                }
            }
            catch { }
        }
        #endregion

        #region Restore_FormLayout
        internal static void Restore_FormLayout(DevExpress.XtraLayout.LayoutControl mLayoutControl, string mFormName)
        {
            try
            {
                byte[] mByte = Program.gMdtFacilitySetup.Load_FormLayout(mFormName, 
                    Program.gCurrentUser.UserGroupFormsLayoutTemplateName);

                if (mByte != null)
                {
                    MemoryStream mMemoryStream = new MemoryStream(mByte);
                    mLayoutControl.RestoreLayoutFromStream(mMemoryStream);
                    mMemoryStream.Close();
                }
            }
            catch { }
        }
        #endregion

        #region Restore_GridLayout
        internal static void Restore_GridLayout(DevExpress.XtraGrid.GridControl mGridControl, string mGridName)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)mGridControl.MainView;
                foreach (DevExpress.XtraGrid.Columns.GridColumn mGridColumn in mGridView.Columns)
                {
                    mGridColumn.Visible = false;
                }

                byte[] mByte = Program.gMdtFacilitySetup.Load_GridLayout(mGridName, Program.gCurrentUser.Code);

                if (mByte != null)
                {
                    MemoryStream mMemoryStream = new MemoryStream(mByte);
                    mGridControl.MainView.RestoreLayoutFromStream(mMemoryStream);
                    mMemoryStream.Close();
                }
            }
            catch { }
        }
        #endregion

        #region Restore_FormSize
        internal static void Restore_FormSize(DevExpress.XtraEditors.XtraForm mForm)
        {
            try
            {
                DataView mDvFormSizes = new DataView();
                mDvFormSizes.Table = gDtFormSizes;
                mDvFormSizes.Sort = "formname";

                int mRowIndex = mDvFormSizes.Find(mForm.Name.Trim());
                if (mRowIndex >= 0)
                {
                    if (Convert.ToInt16(mDvFormSizes[mRowIndex]["formwidth"]) > 0)
                    {
                        mForm.Width = Convert.ToInt16(mDvFormSizes[mRowIndex]["formwidth"]);
                    }

                    if (Convert.ToInt16(mDvFormSizes[mRowIndex]["formheight"]) > 0)
                    {
                        mForm.Height = Convert.ToInt16(mDvFormSizes[mRowIndex]["formheight"]);
                    }
                }
            }
            catch { }
        }
        #endregion

        #region Restore_FormSize
        internal static void Restore_FormSize(string mFormName, DevExpress.XtraEditors.XtraForm mForm, bool mReportingForm)
        {
            try
            {
                if (mReportingForm == true)
                {
                    DataTable mDtFormSizes = gMdtFacilitySetup.Get_FormSizes(
                        "layoutname='defaultsettings' and formname='" + mFormName.Trim() + "'", "");
                    if (mDtFormSizes.Rows.Count > 0)
                    {
                        if (Convert.ToInt16(mDtFormSizes.Rows[0]["formwidth"]) > 0)
                        {
                            mForm.Width = Convert.ToInt16(mDtFormSizes.Rows[0]["formwidth"]);
                        }

                        if (Convert.ToInt16(mDtFormSizes.Rows[0]["formheight"]) > 0)
                        {
                            mForm.Height = Convert.ToInt16(mDtFormSizes.Rows[0]["formheight"]);
                        }
                    }
                }
                else
                {
                    DataView mDvFormSizes = new DataView();
                    mDvFormSizes.Table = gDtFormSizes;
                    mDvFormSizes.Sort = "formname";

                    int mRowIndex = mDvFormSizes.Find(mFormName.Trim());
                    if (mRowIndex >= 0)
                    {
                        if (Convert.ToInt16(mDvFormSizes[mRowIndex]["formwidth"]) > 0)
                        {
                            mForm.Width = Convert.ToInt16(mDvFormSizes[mRowIndex]["formwidth"]);
                        }

                        if (Convert.ToInt16(mDvFormSizes[mRowIndex]["formheight"]) > 0)
                        {
                            mForm.Height = Convert.ToInt16(mDvFormSizes[mRowIndex]["formheight"]);
                        }
                    }
                }
            }
            catch { }
        }
        #endregion

        #region Display_Error
        public static void Display_Error(String mClassName, String mFunctionName, String mMessage)
        {
            DevExpress.XtraEditors.XtraMessageBox.Show("Error occured" + System.Environment.NewLine
            + "CLASS: " + mClassName + System.Environment.NewLine
            + "FUNCTION: " + mFunctionName + System.Environment.NewLine
            + "ERROR_MESSAGE: " + mMessage + System.Environment.NewLine,
            gApplicationName,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
        }
        #endregion

        #region Display_Error
        public static void Display_Error(String mMessage)
        {
            //translate the message to user language
            mMessage = TransLate(mMessage);

            //display message
            DevExpress.XtraEditors.XtraMessageBox.Show(mMessage,
                gApplicationName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        #endregion

        #region Display_Error
        public static void Display_Error(String mMessage, bool mTranslate)
        {
            if (mTranslate == true)
            {
                //translate the message to user language
                mMessage = TransLate(mMessage);
            }

            //display message
            DevExpress.XtraEditors.XtraMessageBox.Show(mMessage,
                gApplicationName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        #endregion

        #region Display_Server_Error
        public static void Display_Server_Error(String mMessage)
        {
            DevExpress.XtraEditors.XtraMessageBox.Show(mMessage,
                gApplicationName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        #endregion

        #region Display_Question
        public static DialogResult Display_Question(String mMessage, MessageBoxDefaultButton mDefaultButton)
        {
            mMessage = TransLate(mMessage);

            DialogResult mResult = DevExpress.XtraEditors.XtraMessageBox.Show(mMessage + "?",
                Program.gApplicationName,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                mDefaultButton);

            return mResult;

        }
        #endregion

        #region Confirm_Deletion
        public static DialogResult Confirm_Deletion(String mItem)
        {
            string mDelete = TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DeleteRecordQuestion.ToString());

            DialogResult mResult = DevExpress.XtraEditors.XtraMessageBox.Show(mDelete + " " + mItem + "?",
                Program.gApplicationName,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            return mResult;

        }
        #endregion

        #region Confirm_VoidingSale
        public static DialogResult Confirm_VoidingSale(String mItem)
        {
            string mVoid = TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_ConfirmVoidSale.ToString());

            DialogResult mResult = DevExpress.XtraEditors.XtraMessageBox.Show(mVoid + " " + mItem + "?",
                Program.gApplicationName,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            return mResult;

        }
        #endregion

        #region Confirm_Refund
        public static DialogResult Confirm_Refund(String mItem)
        {
            string mVoid = TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_ConfirmRefund.ToString());

            DialogResult mResult = DevExpress.XtraEditors.XtraMessageBox.Show(mVoid + " " + mItem + "?",
                Program.gApplicationName,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            return mResult;

        }
        #endregion

        #region Display_Info
        public static void Display_Info(String mMessage)
        {
            //translate the message to user language
            mMessage = TransLate(mMessage);

            DevExpress.XtraEditors.XtraMessageBox.Show(mMessage,
                gApplicationName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        #endregion

        #region Display_Info
        public static void Display_Info(String mMessage, bool mTranslate)
        {
            //translate the message to user language
            if (mTranslate == true)
            {
                mMessage = TransLate(mMessage);
            }

            DevExpress.XtraEditors.XtraMessageBox.Show(mMessage,
                gApplicationName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        #endregion

        #region DateValueNullable
        public static string DateValueNullable(object mDateValue)
        {
            String mDateString = "";
            DateTime mNullDate = new DateTime(1900, 1, 1);

            DateTime mDateTime = new DateTime();

            try
            {
                mDateTime = Convert.ToDateTime(mDateValue);
            }
            catch { }

            if (mDateTime > mNullDate)
            {
                mDateString = mDateTime.ToString("d");
            }

            return mDateString;
        }
        #endregion

        #region IsMoney
        public static Boolean IsMoney(String mValue)
        {
            Boolean mValid = true;

            //if (System.Text.RegularExpressions.Regex.IsMatch(mValue,
            //    @"^\$?(?:\d+|\d{1,3}(?:,\d{3})*)(?:\.\d{1,2}){0,1}$") == false)
            //{
            //    mValid = false;
            //}

            try
            {
                Convert.ToDouble(mValue);
            }
            catch
            {
                mValid = false;
            }

            return mValid;
        }
        #endregion

        #region IsNumeric
        public static Boolean IsNumeric(String mValue)
        {
            Boolean mValid = true;

            try
            {
                Convert.ToInt32(mValue);
            }
            catch
            {
                mValid = false;
            }

            return mValid;
        }
        #endregion

        #region IsEmail
        public static Boolean IsEmail(String mValue)
        {
            Boolean mValid = true;
            if (System.Text.RegularExpressions.Regex.IsMatch(mValue,
                @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$") == false)
            {
                mValid = false;
            }

            return mValid;
        }
        #endregion

        #region IsAlphaNumeric
        public static Boolean IsAlphaNumeric(String mValue)
        {
            Boolean mValid = true;
            if (System.Text.RegularExpressions.Regex.IsMatch(mValue, @"^[a-zA-Z0-9]*$") == false)
            {
                mValid = false;
            }
            return mValid;
        }
        #endregion

        #region IsDate
        public static Boolean IsDate(Object mValue)
        {
            Boolean mValid = true;
            try
            {
                Convert.ToDateTime(mValue);
            }
            catch
            {
                mValid = false;
            }

            return mValid;
        }
        #endregion

        #region IsNullDate
        public static Boolean IsNullDate(Object mValue)
        {
            DateTime mNullDate = new DateTime(1900, 1, 1);
            DateTime mDateValue;
            Boolean mIsNullDate = false;

            try
            {
                mDateValue = Convert.ToDateTime(mValue);

                if (mDateValue <= mNullDate)
                {
                    mIsNullDate = true;
                }
            }
            catch
            {
                mIsNullDate = true;
            }

            return mIsNullDate;
        }
        #endregion

        #region Data Functions

        #region CSV_To_DataSet
        internal static DataSet CSV_To_DataSet(DevExpress.XtraEditors.XtraForm mForm, string mFileName, string mSepChar)
        {
            StreamReader mStreamReader = null;
            DataSet mDsData = new DataSet();
            bool mColumnAdded = false;

            string mFunctionName = "CSV_To_DataSet";

            try
            {
                mForm.Cursor = Cursors.WaitCursor;

                mStreamReader = new StreamReader(mFileName);

                mDsData.Tables.Add(new DataTable("data"));

                while (mStreamReader.Peek() >= 0)
                {
                    string[] mTokens = System.Text.RegularExpressions.Regex.Split(
                        mStreamReader.ReadLine(), mSepChar);

                    if (mColumnAdded == false)
                    {
                        foreach (string mToken in mTokens)
                        {
                            mDsData.Tables["data"].Columns.Add(mToken);
                        }
                        mColumnAdded = true;
                    }
                    else
                    {
                        DataRow mNewRow = mDsData.Tables["data"].NewRow();
                        for (int mColumnCount = 0; mColumnCount < mDsData.Tables["data"].Columns.Count; mColumnCount++)
                        {
                            mNewRow[mColumnCount] = mTokens[mColumnCount];
                        }
                        mDsData.Tables["data"].Rows.Add(mNewRow);
                        mDsData.Tables["data"].AcceptChanges();
                    }
                }

                mForm.Cursor = Cursors.Default;
                return mDsData;
            }
            catch (Exception ex)
            {
                mForm.Cursor = Cursors.Default;
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
            finally
            {
                if (mStreamReader != null)
                {
                    mStreamReader.Close();
                }
            }
        }
        #endregion

        #region Excel_To_DataSet
        internal static DataSet Excel_To_DataSet(DevExpress.XtraEditors.XtraForm mForm, string mFileName)
        {
            DataSet mDsData = new DataSet();
            string mConnStr = "";
            System.Data.OleDb.OleDbConnection mConn = new System.Data.OleDb.OleDbConnection();
            string mFunctionName = "Excel_To_DataSet";

            try
            {
                mForm.Cursor = Cursors.WaitCursor;

                //You should consider using "HDR=NO", to get numbered ColumnNames in your DataSet.
                mConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Persist Security Info=False;"
                    + "Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;\"";
                mConn.ConnectionString = string.Format(mConnStr, mFileName);
                mConn.Open();

                //Get all Table-Names from the workbook
                DataTable mDataTable = mConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, 
                    new object[] { null, null, null, "TABLE" });

                foreach (DataRow row in mDataTable.Rows)
                {
                    string mTableName = row[2].ToString();

                    System.Data.OleDb.OleDbCommand mCommand = 
                        new System.Data.OleDb.OleDbCommand("SELECT * FROM [" + mTableName + "]", mConn);
                    System.Data.OleDb.OleDbDataAdapter mDataAdapter =
                        new System.Data.OleDb.OleDbDataAdapter(mCommand);

                    mDataAdapter.Fill(mDsData, mTableName);
                }

                mForm.Cursor = Cursors.Default;
                return mDsData;
            }
            catch (Exception ex)
            {
                mForm.Cursor = Cursors.Default;
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
            finally
            {
                try
                {
                    mConn.Close();
                }
                catch { }
            }
        }
        #endregion

        #region XML_To_DataSet
        internal static DataSet XML_To_DataSet(DevExpress.XtraEditors.XtraForm mForm, string mFileName)
        {
            DataSet mDsData = new DataSet();
            string mFunctionName = "XML_To_DataSet";

            try
            {
                mForm.Cursor = Cursors.WaitCursor;

                foreach (DataTable dataTable in mDsData.Tables)
                    dataTable.BeginLoadData();

                mDsData.ReadXml(mFileName);

                foreach (DataTable dataTable in mDsData.Tables)
                    dataTable.EndLoadData();


                mForm.Cursor = Cursors.Default;
                return mDsData;
            }
            catch (Exception ex)
            {
                mForm.Cursor = Cursors.Default;
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
        }
        #endregion

        #endregion

        #region Get_LookupItemIndex
        internal static int Get_LookupItemIndex(DevExpress.XtraEditors.LookUpEdit mLookupEdit, string mFieldName, string mFieldValue)
        {
            return mLookupEdit.Properties.GetDataSourceRowIndex(mFieldName, mFieldValue);
        }
        #endregion

        #region Load_ReportTemplate
        public static DevExpress.XtraReports.UI.XtraReport Load_ReportTemplate(byte[] mBytes, DataTable mDataTable)
        {
            DevExpress.XtraReports.UI.XtraReport mReportDoc = new DevExpress.XtraReports.UI.XtraReport();
            DevExpress.XtraReports.UI.DetailBand mDetail = new DevExpress.XtraReports.UI.DetailBand();
            DevExpress.XtraReports.UI.PageHeaderBand mPageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            DevExpress.XtraReports.UI.PageFooterBand mPageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            DevExpress.XtraReports.UI.ReportHeaderBand mReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();

            string mFunctionName = "Create_Report";

            try
            {
                if (mBytes != null)
                {
                    MemoryStream mMemoryStream = new MemoryStream(mBytes);
                    mReportDoc.LoadLayout(mMemoryStream);
                }
                else
                {
                    mDetail.Name = "Detail";
                    mPageHeader.Name = "PageHeader";
                    mPageFooter.Name = "PageFooter";
                    mReportHeader.Name = "ReportHeader";

                    mReportDoc.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
                    mDetail,
                    mPageHeader,
                    mPageFooter,
                    mReportHeader});
                    mReportDoc.Version = "8.2";
                }

                mReportDoc.DataSource = mDataTable;
                mReportDoc.DataMember = mDataTable.TableName;

                return mReportDoc;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
        }
        #endregion

        #region Load_ReportTemplate
        public static DevExpress.XtraReports.UI.XtraReport Load_ReportTemplate(byte[] mBytes, DataSet mDataSet)
        {
            DevExpress.XtraReports.UI.XtraReport mReportDoc = new DevExpress.XtraReports.UI.XtraReport();
            DevExpress.XtraReports.UI.DetailBand mDetail = new DevExpress.XtraReports.UI.DetailBand();
            DevExpress.XtraReports.UI.PageHeaderBand mPageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            DevExpress.XtraReports.UI.PageFooterBand mPageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            DevExpress.XtraReports.UI.ReportHeaderBand mReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();

            string mFunctionName = "Create_Report";

            try
            {
                if (mBytes != null)
                {
                    MemoryStream mMemoryStream = new MemoryStream(mBytes);
                    mReportDoc.LoadLayout(mMemoryStream);
                }
                else
                {
                    mDetail.Name = "Detail";
                    mPageHeader.Name = "PageHeader";
                    mPageFooter.Name = "PageFooter";
                    mReportHeader.Name = "ReportHeader";

                    mReportDoc.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
                    mDetail,
                    mPageHeader,
                    mPageFooter,
                    mReportHeader});
                    mReportDoc.Version = "8.2";
                }

                mReportDoc.DataSource = mDataSet;
                //mReportDoc.DataMember = mDataSet.Namespace;

                return mReportDoc;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
        }
        #endregion

        #region GrantDeny_FunctionAccess
        internal static bool GrantDeny_FunctionAccess(string mFunctionAccessKey)
        {
            DataView mDvUserFunctionAccessKeys = new DataView();
            mDvUserFunctionAccessKeys.Table = Program.gDtUserFunctionAccessKeys;
            mDvUserFunctionAccessKeys.Sort = "functionaccesskey";

            if (mDvUserFunctionAccessKeys.Find(mFunctionAccessKey.Trim()) >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Center_Screen
        internal static void Center_Screen(XtraForm mForm)
        {
            System.Drawing.Point mPoint = new System.Drawing.Point();
            mPoint.X = (Screen.PrimaryScreen.WorkingArea.Width / 2) - (mForm.Width / 2);
            mPoint.Y = (Screen.PrimaryScreen.WorkingArea.Height / 2) - (mForm.Height / 2);
            mForm.Location = mPoint;
            //mForm.MaximizeBox = false;
        }
        #endregion

        #region AddTimeToDate
        internal static void AddTimeToDate(DevExpress.XtraEditors.DateEdit mDateEdit)
        {
            if (Program.IsDate(mDateEdit.EditValue) == true)
            {
                DateTime mDateValue = Convert.ToDateTime(mDateEdit.EditValue).Date;
                mDateValue = mDateValue.AddHours(DateTime.Now.Hour);
                mDateValue = mDateValue.AddMinutes(DateTime.Now.Minute);

                mDateEdit.EditValue = mDateValue;
            }
        }
        #endregion

        #region Validate_RchClient
        internal static bool Validate_RchClient(string mServiceType, string mClientCode)
        {
            string mFunctionName = "Validate_RchClient";

            try
            {
                DateTime mBirthDate = new DateTime();
                string mGender = "";
                double mAge = 0;

                AfyaPro_MT.clsRCHClients mMdtRCHClients = (AfyaPro_MT.clsRCHClients)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRCHClients),
                    Program.gMiddleTier + "clsRCHClients");

                AfyaPro_Types.clsRchClient mRchClient = mMdtRCHClients.Get_Client(mClientCode.Trim());

                if (mRchClient != null)
                {
                    if (mRchClient.Exist == false)
                    {
                        Program.Display_Error("Client with code '" + mClientCode + "' does not exist");
                        return false;
                    }

                    mBirthDate = mRchClient.birthdate;
                    mGender = mRchClient.gender;

                    int mDays = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue).Subtract(mBirthDate).Days;
                    int mYears = (int)mDays / 365;
                    int mMonths = (int)(mDays % 365) / 30;

                    mAge = mYears + (mMonths / 12);
                }

                if (mServiceType.Trim().ToLower() ==
                    AfyaPro_Types.clsEnums.RCHServices.familyplanning.ToString().ToLower())
                {
                    if (mAge <= gChildrenAgeLimit)
                    {
                        Program.Display_Error("Age restriction. Not a valid client for family planning service");
                        return false;
                    }
                }

                if (mServiceType.Trim().ToLower() ==
                    AfyaPro_Types.clsEnums.RCHServices.antenatalcare.ToString().ToLower())
                {
                    if (mAge <= gChildrenAgeLimit)
                    {
                        Program.Display_Error("Age restriction. Not a valid client for antenatal care service");
                        return false;
                    }

                    if (mGender.Trim().ToLower() != "f")
                    {
                        Program.Display_Error("Gender restriction. Not a valid client for antenatal care service");
                        return false;
                    }
                }

                if (mServiceType.Trim().ToLower() ==
                     AfyaPro_Types.clsEnums.RCHServices.postnatalcare.ToString().ToLower())
                {
                    if (mAge <= gChildrenAgeLimit)
                    {
                        Program.Display_Error("Age restriction. Not a valid client for postnatal care service");
                        return false;
                    }

                    if (mGender.Trim().ToLower() != "f")
                    {
                        Program.Display_Error("Gender restriction. Not a valid client for postnatal care service");
                        return false;
                    }
                }

                if (mServiceType.Trim().ToLower() ==
                    AfyaPro_Types.clsEnums.RCHServices.maternity.ToString().ToLower())
                {
                    if (mAge <= gChildrenAgeLimit)
                    {
                        Program.Display_Error("Age restriction. Not a valid client for maternity care service");
                        return false;
                    }

                    if (mGender.Trim().ToLower() != "f")
                    {
                        Program.Display_Error("Gender restriction. Not a valid client for maternity care service");
                        return false;
                    }
                }

                if (mServiceType.Trim().ToLower() ==
                     AfyaPro_Types.clsEnums.RCHServices.childrenhealth.ToString().ToLower())
                {
                    if (mAge > gChildrenAgeLimit)
                    {
                        Program.Display_Error("Age restriction. Not a valid client for children health service");
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return false;
            }
        }
        #endregion

        #region UppercaseFirst
        internal static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            s = s.ToLower();

            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
        #endregion

        #region Validate_ExtraPatientDetails
        internal static string Validate_ExtraPatientDetails(DevExpress.XtraLayout.LayoutControl mLayoutControl)
        {
            string mErrorMessage = "";
            string mErrorFieldName = "";

            try
            {
                DataTable mDtExtraFields = pMdtPatientExtraFields.View("", "", "", "");

                foreach (DataRow mDataRow in mDtExtraFields.Rows)
                {
                    string mFieldName = mDataRow["fieldname"].ToString().ToLower().Trim();
                    string mFieldType = mDataRow["fieldtype"].ToString().Trim();
                    string mDataType = mDataRow["datatype"].ToString().Trim();
                    Int16 mCompulsory = Convert.ToInt16(mDataRow["compulsory"]);
                    string mErrorOnEmpty = mDataRow["erroronempty"].ToString().Trim();
                    object mFieldValue = DBNull.Value;

                    mErrorFieldName = mFieldName;

                    if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().Trim().ToLower())
                    {
                        ComboBoxEdit mComboBoxEdit = mLayoutControl.GetControlByName("cbo" + mFieldName) as ComboBoxEdit;
                        mFieldValue = mComboBoxEdit.Text;

                        if (mLayoutControl.GetItemByControl(mComboBoxEdit).Visible == true)
                        {
                            if (mCompulsory == 1 && mComboBoxEdit.Text.Trim() == "")
                            {
                                mErrorMessage = mErrorOnEmpty;
                                mComboBoxEdit.Focus();
                                break;
                            }
                        }
                    }
                    else if (mFieldType.ToLower() == AfyaPro_Types.clsEnums.FieldTypes.checkbox.ToString().Trim().ToLower())
                    {
                        CheckEdit mCheckEdit = mLayoutControl.GetControlByName("chk" + mFieldName) as CheckEdit;
                        mFieldValue = Convert.ToInt16(mCheckEdit.Checked);
                    }
                    else
                    {
                        if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().Trim().ToLower())
                        {
                            DateEdit mDateEdit = mLayoutControl.GetControlByName("txt" + mFieldName) as DateEdit;
                            if (Program.IsNullDate(mDateEdit.EditValue) == false)
                            {
                                mFieldValue = Convert.ToDateTime(mDateEdit.EditValue).Date;
                            }
                            else
                            {
                                if (mLayoutControl.GetItemByControl(mDateEdit).Visible == true)
                                {
                                    if (mCompulsory == 1)
                                    {
                                        mErrorMessage = mErrorOnEmpty;
                                        mDateEdit.Focus();
                                        break;
                                    }
                                }
                            }
                        }
                        else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().Trim().ToLower())
                        {
                            Control mTextEdit = mLayoutControl.GetControlByName("txt" + mFieldName) as Control;
                            if (Program.IsMoney(mTextEdit.Text) == true)
                            {
                                mFieldValue = Convert.ToDouble(mTextEdit.Text);
                            }
                            else
                            {
                                if (mLayoutControl.GetItemByControl(mTextEdit).Visible == true)
                                {
                                    if (mCompulsory == 1)
                                    {
                                        mErrorMessage = mErrorOnEmpty;
                                        mTextEdit.Focus();
                                        break;
                                    }
                                }
                            }
                        }
                        else if (mDataType.ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().Trim().ToLower())
                        {
                            Control mTextEdit = mLayoutControl.GetControlByName("txt" + mFieldName) as Control;
                            if (Program.IsMoney(mTextEdit.Text) == true)
                            {
                                mFieldValue = Convert.ToInt32(mTextEdit.Text);
                            }
                            else
                            {
                                if (mLayoutControl.GetItemByControl(mTextEdit).Visible == true)
                                {
                                    if (mCompulsory == 1)
                                    {
                                        mErrorMessage = mErrorOnEmpty;
                                        mTextEdit.Focus();
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Control mTextEdit = mLayoutControl.GetControlByName("txt" + mFieldName) as Control;
                            mFieldValue = mTextEdit.Text;

                            if (mLayoutControl.GetItemByControl(mTextEdit).Visible == true)
                            {
                                if (mCompulsory == 1)
                                {
                                    mErrorMessage = mErrorOnEmpty;
                                    mTextEdit.Focus();
                                    break;
                                }
                            }
                        }
                    }
                }

                return mErrorMessage;
            }
            catch (Exception ex)
            {
                return ex.Message + " Error on Field " + mErrorFieldName;
            }
        }
        #endregion
    }
}
