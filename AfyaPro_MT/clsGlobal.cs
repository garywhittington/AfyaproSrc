/*
Copyright (C) 2013 AfyaPro Foundation

This file is part of AfyaPro.

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
using System.Text;
using System.Xml;
using System.IO;
using System.Data;
using System.Data.Odbc;
using System.Diagnostics;
using Microsoft.VisualBasic;

namespace AfyaPro_MT
{
    public class clsGlobal
    {
        //versioning
        internal static Int16 gVersionMajor = 3;
        internal static Int16 gVersionMinor = 0;
        internal static Int16 gVersionBuild = 57;
        internal static Int16 gVersionRevision = 15;

        #region Declaration

        private System.Timers.Timer pTimer = new System.Timers.Timer();
        private int pNumberOfTrials = 0;
        private OdbcConnection pConn = new OdbcConnection();

        //afyacon
        internal static Int16 gAfyaPro_Authentication = 0;
        internal static Int32 gAfyaPro_ServerType;
        private static String pAfyaPro_ServerName;
        private static String pAfyaPro_ServerUser;
        private static String pAfyaPro_ServerPwd;
        private static String pAfyaPro_ServerPort;
        private static String pAfyaPro_DbaseName;
        internal static String gAfyaConStr = "";
        internal static String gInventoryDbName = "";
        internal static String gAfyaProDbName = "";
        internal static String gAfyaProAuditDbName = "";

        private static String pIniFileName;
        private static String pFunctionName;
        private static String pClassName = "AfyaPro_MT.clsGlobal";

        public static String gDateFormatWithoutTime = "";
        public static String gDateFormatDisplay = "";
        public static String gUserId = "";

        public static Int16 gMiddleTierPort = 2008;
        public static String gCryptoKey = "123789";
        public static AfyaPro_Types.clsCode[] gCodes;

        public static String gDbDateQuote = "'";
        public static String gDbCurrentDate = "";
        public static String gDbParameterEscapeCharacter = "@";
        public static String gDbNameTableNameSep = "";

        //log
        private static EventLog pEventLog;

        public static System.Globalization.CultureInfo gCulture;
        public static String gPicturesPath = "";

        internal static Int16 gDbVersionMajor = 1;
        internal static Int16 gDbVersionMinor = 1;
        internal static Int16 gDbVersionBuild = 1;
        internal static Int16 gDbVersionRevision = 1;

        #endregion

        #region clsGlobal
        public clsGlobal()
        {
            this.ReadIni();

            Create_AfyaConStr(pAfyaPro_DbaseName);

            pTimer.Interval = 10000;
            pTimer.Elapsed += new System.Timers.ElapsedEventHandler(pTimer_Elapsed);
            pTimer.Enabled = true;

            gPicturesPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Pictures";
        }
        #endregion

        #region pTimer_Elapsed
        void pTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                pConn.ConnectionString = gAfyaConStr;
                pConn.Open();

                if (pConn.State == ConnectionState.Open)
                {
                    pTimer.Enabled = false;

                    this.SetDateFormat();

                    this.Update_Database();
                    this.Synchronize_FacilityPaymentTypes();
                    this.Synchronize_Packagings();
                    this.Synchronize_Codes();
                    this.Synchronize_Admin();
                    this.Synchronize_PaymentTypes();
                    this.Synchronize_DischargeStatus();
                    this.Synchronize_Currencies();
                    this.Synchronize_PriceCategories();
                    this.Synchronize_EnglishLanguage();
                    this.Create_DynamicViews();
                    this.Synchronize_BuiltInReports();
                    Synchronize_FamilyPlanningMethods();
                    Synchronize_DangerIndicators();
                    Synchronize_BirthComplications();
                    Synchronize_BirthMethods();
                    Synchronize_Vaccines();
                    Synchronize_ExtraProductInfos();
                    Synchronize_CTCCodes();
                    this.Synchronize_MTUHA_Diagnoses();
                    this.Synchronize_UsersWithStaffs();
                    this.Synchronize_VoidedSalesTable("billinvoices", "billvoidedsales");
                    this.Synchronize_VoidedSalesTable("billreceipts", "billvoidedsales");
                    this.Synchronize_VoidedSalesTable("billinvoiceitems", "billvoidedsalesitems");
                    this.Synchronize_VoidedSalesTable("billreceiptitems", "billvoidedsalesitems");
                    this.Synchronize_VoidedSalesTable("billinvoicepayments", "billvoidedsales");
                    this.Synchronize_VoidedSalesTable("billcollections", "billrefunds");

                    Type mCodeKeys = typeof(AfyaPro_Types.clsEnums.AuditTables);
                    foreach (string mTableName in Enum.GetNames(mCodeKeys))
                    {
                        Synchronize_AuditTable(mTableName);
                    }
                }
                else if (pNumberOfTrials >= 6)
                {
                    pTimer.Interval = 30000;
                }

                pNumberOfTrials++;
            }
            catch { }
        }
        #endregion

        #region Update_Database
        private void Update_Database()
        {
            //OdbcConnection mConn = new OdbcConnection();
            //OdbcCommand mCommand = new OdbcCommand();
            //OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            //string mFunctionName = "Update_Database";

            //try
            //{
            //    mConn.ConnectionString = gAfyaConStr;
            //    if (mConn.State != ConnectionState.Open)
            //    {
            //        mConn.Open();
            //    }
            //    mCommand.Connection = mConn;

            //    bool mIsNewVersion = false;

            //    #region check for database version

            //    DataTable mDtVersionControl = new DataTable("sys_versioncontrol");

            //    try
            //    {

            //        mCommand.CommandText = "select * from sys_versioncontrol";
            //        mDataAdapter.SelectCommand = mCommand;
            //        mDataAdapter.Fill(mDtVersionControl);

            //        if (mDtVersionControl.Rows.Count == 0)
            //        {
            //            mCommand.CommandText = "insert into sys_versioncontrol(majornumber,minornumber,"
            //                + "buildnumber,revisionnumber) values(1,1,1,1)";
            //            mCommand.ExecuteNonQuery();

            //            mCommand.CommandText = "select * from sys_versioncontrol";
            //            mDataAdapter.SelectCommand = mCommand;
            //            mDataAdapter.Fill(mDtVersionControl);
            //        }
            //    }
            //    catch
            //    {
            //        mCommand.CommandText = ""
            //            + "CREATE TABLE sys_versioncontrol (autocode INT(11) NOT NULL AUTO_INCREMENT,"
            //            + "majornumber INT DEFAULT ('1'),"
            //            + "minornumber INT DEFAULT ('1'),"
            //            + "buildnumber INT DEFAULT ('1'),"
            //            + "revisionnumber INT DEFAULT ('1'),"
            //            + "PRIMARY KEY (autocode));";
            //        mCommand.ExecuteNonQuery();

            //        mCommand.CommandText = "insert into sys_versioncontrol(majornumber,minornumber,"
            //            + "buildnumber,revisionnumber) values(1,1,1,1)";
            //        mCommand.ExecuteNonQuery();

            //        mCommand.CommandText = "select * from sys_versioncontrol";
            //        mDataAdapter.SelectCommand = mCommand;
            //        mDataAdapter.Fill(mDtVersionControl);
            //    }

            //    if (mDtVersionControl.Rows.Count > 0)
            //    {
            //        gDbVersionMajor = Convert.ToInt16(mDtVersionControl.Rows[0]["majornumber"]);
            //        gDbVersionMinor = Convert.ToInt16(mDtVersionControl.Rows[0]["minornumber"]);
            //        gDbVersionBuild = Convert.ToInt16(mDtVersionControl.Rows[0]["buildnumber"]);
            //        gDbVersionRevision = Convert.ToInt16(mDtVersionControl.Rows[0]["revisionnumber"]);

            //        if (gDbVersionBuild < gVersionBuild)
            //        {
            //            mIsNewVersion = true;
            //        }
            //    }
            //    else
            //    {
            //        mIsNewVersion = true;
            //    }

            //    #endregion

            //    if (mIsNewVersion == true)
            //    {
            //        clsDatabaseUpdate mDatabaseUpdate = new clsDatabaseUpdate();
            //        bool mResult = mDatabaseUpdate.Create_UpdaterDatabase();

            //        if (mResult == true)
            //        {
            //            mResult = mDatabaseUpdate.Update_Database(gAfyaProDbName);
            //        }

            //        if (mResult == true)
            //        {
            //            mCommand.CommandText = "update sys_versioncontrol set majornumber="
            //            + gVersionMajor + ",minornumber=" + gVersionMinor + ",buildnumber="
            //            + gVersionBuild + ",revisionnumber=" + gVersionRevision;
            //            mCommand.ExecuteNonQuery();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
            //    return;
            //}
            //finally
            //{
            //    try
            //    {
            //        mConn.Close();
            //    }
            //    catch { }
            //}
        }
        #endregion

        #region Get_DatabaseList
        //internal static DataTable Get_DatabaseList(OdbcConnection mConn, OdbcTransaction mTrans)
        //{
        //    OdbcCommand mCommand = new OdbcCommand();
        //    OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
        //    DataTable mDtDatabases = new DataTable("databases");
        //    String mCommandText = "";
        //    String mFunctionName = "Get_DatabaseList";

        //    try
        //    {
        //        switch (gAfyaPro_ServerType)
        //        {
        //            case 0:
        //                {
        //                    mCommandText = "show databases";
        //                }
        //                break;
        //            case 1:
        //                {
        //                    mCommandText = "SELECT name FROM master.dbo.sysdatabases";
        //                }
        //                break;
        //        }

        //        mCommand.Connection = mConn;
        //        if (mTrans != null)
        //        {
        //            mCommand.Transaction = mTrans;
        //        }
                
        //        mCommand.CommandText = mCommandText;
        //        mDataAdapter.SelectCommand = mCommand;
        //        mDataAdapter.Fill(mDtDatabases);

        //        return mDtDatabases;
        //    }
        //    catch (Exception ex)
        //    {
        //        Write_Error(pClassName, mFunctionName, ex.Message);
        //        return null;
        //    }
        //}
        #endregion

        #region Create_GenConStr
        internal static string Create_GenConStr(string mDatabaseName)
        {
            string mConnStr = "";
            string mServerDriver = "";

            pFunctionName = "Create_GenConStr";

            try
            {
                //determine server type
                switch (gAfyaPro_ServerType)
                {
                    case 0:
                        {
                            mServerDriver = "MySQL ODBC 5.1 Driver";
                            gDbDateQuote = "'";
                            gDbCurrentDate = "Now()";
                            gDbParameterEscapeCharacter = "?";
                            gDbNameTableNameSep = ".";

                            //connectionstring
                            mConnStr = "DRIVER={" + mServerDriver
                                + "};SERVER=" + pAfyaPro_ServerName
                                + ";UID=" + pAfyaPro_ServerUser
                                + ";PWD=" + pAfyaPro_ServerPwd
                                + ";DATABASE=" + mDatabaseName;
                                //+ ";Max Pool Size=50"
                                //+ ";Min Pool Size=5"
                                //+ "Pooling=True"
                                //+ ";OPTION=3";
                        }
                        break;
                    case 1:
                        {
                            mServerDriver = "SQL Server";
                            gDbDateQuote = "'";
                            gDbCurrentDate = "GetDate()";
                            gDbParameterEscapeCharacter = "@";
                            gDbNameTableNameSep = ".dbo.";

                            //connectionstring
                            if (gAfyaPro_Authentication == 0)
                            {
                                mConnStr = "DRIVER={" + mServerDriver
                                    + "};SERVER=" + pAfyaPro_ServerName
                                    + ";Trusted_Connection=yes"
                                    + ";DATABASE=" + mDatabaseName;
                                    //+ ";Max Pool Size=50"
                                    //+ ";Min Pool Size=5"
                                    //+ "Pooling=True"
                                    //+ ";OPTION=3";
                            }
                            else
                            {
                                mConnStr = "DRIVER={" + mServerDriver
                                    + "};SERVER=" + pAfyaPro_ServerName
                                    + ";UID=" + pAfyaPro_ServerUser
                                    + ";PWD=" + pAfyaPro_ServerPwd
                                    + ";DATABASE=" + mDatabaseName;
                                    //+ ";Max Pool Size=50"
                                    //+ ";Min Pool Size=5"
                                    //+ "Pooling=True"
                                    //+ ";OPTION=3";
                            }
                        }
                        break;
                }

                return mConnStr;
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, pFunctionName, ex.Message);
                return "";
            }
        }
        #endregion

        #region Create_AfyaConStr
        internal static string Create_AfyaConStr(string mDatabaseName)
        {
            String mServerDriver = "";

            pFunctionName = "Create_AfyaConStr";

            try
            {
                //determine server type
                switch (gAfyaPro_ServerType)
                {
                    case 0:
                        {
                            mServerDriver = "MySQL ODBC 5.1 Driver";
                            gDbDateQuote = "'";
                            gDbCurrentDate = "Now()";
                            gDbParameterEscapeCharacter = "?";
                            gDbNameTableNameSep = ".";

                            //connectionstring
                            gAfyaConStr = "DRIVER={" + mServerDriver
                                + "};SERVER=" + pAfyaPro_ServerName
                                + ";UID=" + pAfyaPro_ServerUser
                                + ";PWD=" + pAfyaPro_ServerPwd
                                + ";DATABASE=" + mDatabaseName;
                                //+ ";Max Pool Size=50"
                                //+ ";Min Pool Size=5"
                                //+ "Pooling=True"
                                //+ ";OPTION=3";
                        }
                        break;
                    case 1:
                        {
                            mServerDriver = "SQL Server";
                            gDbDateQuote = "'";
                            gDbCurrentDate = "GetDate()";
                            gDbParameterEscapeCharacter = "@";
                            gDbNameTableNameSep = ".dbo.";

                            //connectionstring
                            if (gAfyaPro_Authentication == 0)
                            {
                                gAfyaConStr = "DRIVER={" + mServerDriver
                                    + "};SERVER=" + pAfyaPro_ServerName
                                    + ";Trusted_Connection=yes"
                                    + ";DATABASE=" + mDatabaseName;
                                    //+ ";Max Pool Size=50"
                                    //+ ";Min Pool Size=5"
                                    //+ "Pooling=True"
                                    //+ ";OPTION=3";
                            }
                            else
                            {
                                gAfyaConStr = "DRIVER={" + mServerDriver
                                    + "};SERVER=" + pAfyaPro_ServerName
                                    + ";UID=" + pAfyaPro_ServerUser
                                    + ";PWD=" + pAfyaPro_ServerPwd
                                    + ";DATABASE=" + mDatabaseName;
                                    //+ ";Max Pool Size=50"
                                    //+ ";Min Pool Size=5"
                                    //+ "Pooling=True"
                                    //+ ";OPTION=3";
                            }
                        }
                        break;
                }

                return gAfyaConStr;
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, pFunctionName, ex.Message);
                return "";
            }
        }
        #endregion

        #region Concat_Fields
        public static string Concat_Fields(string mCommaSepStrings, string mResultFieldName)
        {
            string mResult = "";

            string[] mStrings = mCommaSepStrings.Split(',');

            foreach (string mString in mStrings)
            {
                switch (gAfyaPro_ServerType)
                {
                    case 0:
                        {
                            if (mResult.Trim() == "")
                            {
                                mResult = mString;
                            }
                            else
                            {
                                mResult = mResult + "," + mString;
                            }
                        }
                        break;
                    case 1:
                        {
                            if (mResult.Trim() == "")
                            {
                                mResult = mString;
                            }
                            else
                            {
                                mResult = mResult + "+" + mString;
                            }
                        }
                        break;
                }
            }

            switch (gAfyaPro_ServerType)
            {
                case 0: mResult = "CONCAT(" + mResult + ") "+ mResultFieldName; break;
                case 1: mResult = "(" + mResult + ") " + mResultFieldName; break;
            }

            return mResult;
        }
        #endregion

        #region Age_Formula
        public static string Age_Formula(string mBirthDateField, string mEventDateField, string mResultFieldName)
        {
            string mAge_Formula = "";

            switch (gAfyaPro_ServerType)
            {
                case 0:
                    {
                        mAge_Formula = "DATEDIFF(" + mEventDateField + "," + mBirthDateField + ")/365.25";
                    }
                    break;
                case 1:
                    {
                        mAge_Formula = "DATEDIFF(day, " + mBirthDateField + ", " + mEventDateField + ")/365.25";
                    }
                    break;
            }

            return "(" + mAge_Formula + ") " + mResultFieldName;
        }
        #endregion

        #region Age_Display
        public static string Age_Display(string mAgeFormula, string mResultFieldName)
        {
            string mAgeDisplay = "";

            switch (gAfyaPro_ServerType)
            {
                case 0:
                    {
                        mAgeDisplay = "CAST(CONCAT(CAST(" + mAgeFormula + " - (" + mAgeFormula + " MOD 1) AS SIGNED), ' yrs ', CAST((" + mAgeFormula + " MOD 1) * 12 - ((" + mAgeFormula + " MOD 1) * 12 MOD 1) AS SIGNED), ' mts') AS CHAR)";
                    }
                    break;
                case 1:
                    {
                        mAgeDisplay = "CAST(CAST(" + mAgeFormula + " - (" + mAgeFormula + " % 1) AS INTEGER) AS VARCHAR) + ' yrs ' + CAST(CAST((" + mAgeFormula + " % 1) * 12 - ((" + mAgeFormula + " % 1) * 12 % 1) AS INTEGER) AS VARCHAR) + ' mts'";
                    }
                    break;
            }

            return "(" + mAgeDisplay + ") " + mResultFieldName;
        }
        #endregion

        #region DateDiff_InDays
        public static string DateDiff_InDays(string mStartDateField, string mEndDateField)
        {
            string mDateDiff = "";

            switch (gAfyaPro_ServerType)
            {
                case 0:
                    {
                        mDateDiff = "datediff(" + mEndDateField + "," + mStartDateField + ")";
                    }
                    break;
                case 1:
                    {
                        mDateDiff = "datediff(day," + mStartDateField + "," + mEndDateField + ")";
                    }
                    break;
            }

            return mDateDiff;
        }
        #endregion

        #region read xml configuration file
        private bool ReadIni()
        {
            XmlTextReader mXmlTextReader;
            FileStream mFileSystemIn;
            StreamReader mStreamReader;

            try
            {
                pIniFileName = System.Environment.SystemDirectory + "/afyaproserver.xml";

                mFileSystemIn = new FileStream(pIniFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                mStreamReader = new StreamReader(mFileSystemIn);
                mXmlTextReader = new XmlTextReader(mStreamReader);

                while (mXmlTextReader.Read())
                {

                    //middletierport
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "middletierport")
                    {
                        gMiddleTierPort = Convert.ToInt16(mXmlTextReader.ReadElementString("middletierport"));
                    }

                    #region afyapro

                    //server type
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "afyapro_servertype")
                    {
                        gAfyaPro_ServerType = Convert.ToInt16(mXmlTextReader.ReadElementString("afyapro_servertype"));
                    }
                    //servername
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "afyapro_servername")
                    {
                        pAfyaPro_ServerName = mXmlTextReader.ReadElementString("afyapro_servername");
                    }
                    //serverport
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "afyapro_serverport")
                    {
                        pAfyaPro_ServerPort = mXmlTextReader.ReadElementString("afyapro_serverport");
                    }
                    //authentication
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "afyapro_authentication")
                    {
                        gAfyaPro_Authentication = Convert.ToInt16(mXmlTextReader.ReadElementString("afyapro_authentication"));
                    }
                    //serveruser
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "afyapro_serveruser")
                    {
                        pAfyaPro_ServerUser = mXmlTextReader.ReadElementString("afyapro_serveruser");
                    }
                    //serverpassword
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "afyapro_serverpassword")
                    {
                        pAfyaPro_ServerPwd = mXmlTextReader.ReadElementString("afyapro_serverpassword");
                    }
                    //databasename
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "afyapro_databasename")
                    {
                        pAfyaPro_DbaseName = mXmlTextReader.ReadElementString("afyapro_databasename");
                        gAfyaProDbName = pAfyaPro_DbaseName;
                        gInventoryDbName = pAfyaPro_DbaseName + "_inventory_";
                        gAfyaProAuditDbName = pAfyaPro_DbaseName + "_audit";
                    }

                    #endregion
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

        #region SetDateFormat
        private void SetDateFormat()
        {
            pFunctionName = "SetDateFormat";

            try
            { 
                //set culture
                gCulture = new System.Globalization.CultureInfo("en-US");
                System.Threading.Thread.CurrentThread.CurrentCulture = gCulture;

                //curreny
                gCulture.NumberFormat.CurrencySymbol = "";
                gCulture.NumberFormat.CurrencyGroupSeparator = ",";
                gCulture.NumberFormat.CurrencyDecimalDigits = 2;

                switch (gAfyaPro_ServerType)
                {
                    case 0:
                        {
                            gCulture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd HH:mm";
                            gDateFormatWithoutTime = "yyyy-MM-dd";
                            gDateFormatDisplay = "dd MMM yyyy";
                            gCulture.DateTimeFormat.LongDatePattern = "yyyy-MMMM-dd HH:mm:ss";
                        }
                        break;
                    case 1:
                        {
                            gCulture.DateTimeFormat.ShortDatePattern = "MM-dd-yyyy HH:mm";
                            gDateFormatWithoutTime = "MM-dd-yyyy";
                            gDateFormatDisplay = "dd MMM yyyy";
                            gCulture.DateTimeFormat.LongDatePattern = "MM-dd-yyyy HH:mm:ss";
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, pFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Synchronize_Packagings

        private void Synchronize_Packagings()
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            DataTable mDataTable = new DataTable("som_packagings");
            DataView mDataView = new DataView();

            String mFunctionName = "Synchronize_Packagings";

            try
            {
                mConn.ConnectionString = gAfyaConStr;
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }
                mCommand.Connection = mConn;

                //load saved data
                mCommand.CommandText = "select * from som_packagings where code='each'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                if (mDataTable.Rows.Count == 0)
                {
                    mCommand.CommandText = "insert into som_packagings(code,description,pieces) "
                    + "values('Each','Each',1)";
                    mCommand.ExecuteNonQuery();
                }

                //return
                return;
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
            finally
            {
                mConn.Close();
            }
        }

        #endregion

        #region Synchronize_Codes

        private void Synchronize_Codes()
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            DataTable mDataTable = new DataTable("facilityautocodes");
            DataView mDataView = new DataView();

            String mFunctionName = "Synchronize_Codes";

            try
            {
                mConn.ConnectionString = gAfyaConStr;
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }
                mCommand.Connection = mConn;

                //load saved data
                mCommand.CommandText = "select * from facilityautocodes";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);
                mDataView.Table = mDataTable;
                mDataView.Sort = "codekey";

                Type mCodeKeys = typeof(AfyaPro_Types.clsEnums.SystemGeneratedCodes);
                foreach (int mCodeKey in Enum.GetValues(mCodeKeys))
                {
                    if (mDataView.Find(mCodeKey) < 0)
                    {
                        mCommand.CommandText = "insert into facilityautocodes(codekey) values(" + mCodeKey + ")";
                        mCommand.ExecuteNonQuery();
                    }
                }

                //return
                return;
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
            finally
            {
                mConn.Close();
            }
        }

        #endregion

        #region Synchronize_CTCCodes

        private void Synchronize_CTCCodes()
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            String mFunctionName = "Synchronize_CTCCodes";

            try
            {
                mConn.ConnectionString = gAfyaConStr;
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }
                mCommand.Connection = mConn;

                DataTable mDtData;
                DataTable mDtSaved;
                DataView mDvSaved;
                string mTableName = "";

                clsCTCCodes mCTCCodes = new clsCTCCodes();

                #region MaritalStatus

                mDtData = mCTCCodes.MaritalStatus();
                mTableName = "maritalstatus";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region FunctionalStatus

                mDtData = mCTCCodes.FunctionalStatus();
                mTableName = "ctc_functionalstatus";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region TBStatus

                mDtData = mCTCCodes.TBStatus();
                mTableName = "ctc_tbstatus";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region ARVStatus

                mDtData = mCTCCodes.ARVStatus();
                mTableName = "ctc_arvstatus";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region AIDSILLNESS

                mDtData = mCTCCodes.AIDSILLNESS();
                mTableName = "ctc_aidsillness";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region ARVCombRegimens

                mDtData = mCTCCodes.ARVCombRegimens();
                mTableName = "ctc_arvcombregimens";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description,category) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "','"
                        + mDataRow["category"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region ARVAdherence

                mDtData = mCTCCodes.ARVAdherence();
                mTableName = "ctc_arvadherence";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region ARVPoorAdherenceReasons

                mDtData = mCTCCodes.ARVPoorAdherenceReasons();
                mTableName = "ctc_arvpooradherencereasons";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region ReferredTo

                mDtData = mCTCCodes.ReferredTo();
                mTableName = "ctc_referedto";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region ARVReasons

                mDtData = mCTCCodes.ARVReasons();
                mTableName = "ctc_arvreason";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description,category) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "','"
                        + mDataRow["category"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region FollowUpStatus

                mDtData = mCTCCodes.FollowUpStatus();
                mTableName = "ctc_followupstatus";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region ReferredFrom

                mDtData = mCTCCodes.ReferredFrom();
                mTableName = "ctc_referedfrom";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region PriorARVExposure

                mDtData = mCTCCodes.PriorARVExposure();
                mTableName = "ctc_priorarvexposure";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region ARTWhyEligible

                mDtData = mCTCCodes.ARTWhyEligible();
                mTableName = "ctc_artwhyeligible";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region PMTCTComb

                mDtData = mCTCCodes.PMTCTComb();
                mTableName = "ctc_pmtctcomb";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region PMTCTDisclosedTo

                mDtData = mCTCCodes.PMTCTDisclosedTo();
                mTableName = "ctc_pmtctdisclosedto";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region WastingCodes

                mDtData = mCTCCodes.WastingCodes();
                mTableName = "ctc_wastingcodes";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region ARTOutcomes

                mDtData = mCTCCodes.ARTOutcomes();
                mTableName = "ctc_artoutcomes";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region HIVTestReasons

                mDtData = mCTCCodes.HIVTestReasons();
                mTableName = "ctc_hivtestreasons";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region BreastFeedings

                mDtData = mCTCCodes.BreastFeedings();
                mTableName = "ctc_breastfeedings";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region MotherStatus

                mDtData = mCTCCodes.MotherStatus();
                mTableName = "ctc_motherstatus";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region HIVInfections

                mDtData = mCTCCodes.HIVInfections();
                mTableName = "ctc_hivinfections";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region ClinMonit

                mDtData = mCTCCodes.ClinMonit();
                mTableName = "ctc_clinmonit";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region TransferIns

                mDtData = mCTCCodes.TransferIns();
                mTableName = "ctc_transferins";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region FPlanMethods

                mDtData = mCTCCodes.FPlanMethods();
                mTableName = "ctc_fplanmethods";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region VisitTypes

                mDtData = mCTCCodes.VisitTypes();
                mTableName = "ctc_visittypes";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region RelevantComedics

                mDtData = mCTCCodes.RelevantComedics();
                mTableName = "ctc_relevantcomeds";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region TBScreening

                mDtData = mCTCCodes.TBScreening();
                mTableName = "ctc_tbscreening";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region TBTreatment

                mDtData = mCTCCodes.TBTreatment();
                mTableName = "ctc_tbtreatments";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region NutritionalStatus

                mDtData = mCTCCodes.NutritionalStatus();
                mTableName = "ctc_nutritionalstatus";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region NutritionalSupp

                mDtData = mCTCCodes.NutritionalSupp();
                mTableName = "ctc_nutritionalsupp";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region PMTCTReferedTo

                mDtData = mCTCCodes.PMTCTReferedTo();
                mTableName = "ctc_pmtctreferedto";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region PMTCTCombLabour

                mDtData = mCTCCodes.PMTCTCombLabour();
                mTableName = "ctc_pmtctcomblabour";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region PMTCTInfantDoseReceived

                mDtData = mCTCCodes.PMTCTInfantDoseReceived();
                mTableName = "ctc_pmtctinfantdosereceived";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region PMTCTInfantDoseDispensed

                mDtData = mCTCCodes.PMTCTInfantDoseDispensed();
                mTableName = "ctc_pmtctinfantdosedispensed";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region PMTCTInfantFeeding

                mDtData = mCTCCodes.PMTCTInfantFeeding();
                mTableName = "ctc_pmtctinfantfeeding";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region PMTCTHIVStatus

                mDtData = mCTCCodes.PMTCTHIVStatus();
                mTableName = "ctc_pmtcthivstatus";
                mDtSaved = new DataTable("saved");
                mDvSaved = new DataView();

                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSaved);
                mDvSaved.Table = mDtSaved;
                mDvSaved.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    if (mDvSaved.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into " + mTableName + "(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                //return
                return;
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
            finally
            {
                mConn.Close();
            }
        }

        #endregion

        #region Synchronize_UsersWithStaffs

        private void Synchronize_UsersWithStaffs()
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            String mFunctionName = "Synchronize_UsersWithStaffs";

            try
            {
                mConn.ConnectionString = gAfyaConStr;
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }
                mCommand.Connection = mConn;

                //load saved data
                DataTable mDtStaffs = new DataTable("staffs");
                mCommand.CommandText = "select * from facilitystaffs";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtStaffs);

                DataTable mDtUsers = new DataTable("users");
                mCommand.CommandText = "select * from sys_users";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtUsers);

                DataTable mDtUserGroups = new DataTable("usergroups");
                mCommand.CommandText = "select * from sys_usergroups";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtUserGroups);

                DataView mDvStaffs = new DataView();
                mDvStaffs.Table = mDtStaffs;
                mDvStaffs.Sort = "category,code";

                DataView mDvUsers = new DataView();
                mDvUsers.Table = mDtUsers;
                mDvUsers.Sort = "usergroupcode";

                foreach (DataRow mDataRow in mDtUserGroups.Rows)
                {
                    string mUserGroupCode = mDataRow["code"].ToString().Trim();
                    int mStaffCategoryCode = -1;
                    try
                    {
                        mStaffCategoryCode = Convert.ToInt16(mDataRow["staffcategorycode"]);
                    }
                    catch { }

                    if (mStaffCategoryCode != -1)
                    {
                        DataRowView[] mUsersView = mDvUsers.FindRows(mUserGroupCode);

                        foreach (DataRowView mDataRowView in mUsersView)
                        {
                            string mUserId = mDataRowView["code"].ToString().Trim();
                            string mUserName = mDataRowView["description"].ToString().Trim();

                            object[] mStaff = new object[2];
                            mStaff[0] = mStaffCategoryCode;
                            mStaff[1] = mUserId;

                            int mRowIndex = mDvStaffs.Find(mStaff);
                            if (mRowIndex < 0)
                            {
                                List<clsDataField> mDataFields = new List<clsDataField>();
                                mDataFields.Add(new clsDataField("code", DataTypes.dbstring.ToString(), mUserId));
                                mDataFields.Add(new clsDataField("description", DataTypes.dbstring.ToString(), mUserName));
                                mDataFields.Add(new clsDataField("category", DataTypes.dbnumber.ToString(), mStaffCategoryCode));

                                mCommand.CommandText = clsGlobal.Get_InsertStatement("facilitystaffs", mDataFields);
                                mCommand.ExecuteNonQuery();
                            }
                            else
                            {
                                if (Convert.ToInt16(mDataRow["synchronizewithstaffs"]) == 1)
                                {
                                    List<clsDataField> mDataFields = new List<clsDataField>();
                                    mDataFields.Add(new clsDataField("description", DataTypes.dbstring.ToString(), mUserName));

                                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("facilitystaffs", mDataFields,
                                        "category=" + mStaffCategoryCode + " and code='" + mUserId + "'");
                                    mCommand.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }

                return;
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
            finally
            {
                mConn.Close();
            }
        }

        #endregion

        #region Synchronize_FacilityPaymentTypes

        private void Synchronize_FacilityPaymentTypes()
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            DataTable mDataTable = new DataTable("facilitypaymenttypes");

            String mFunctionName = "Synchronize_FacilityPaymentTypes";

            try
            {
                mConn.ConnectionString = gAfyaConStr;
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }
                mCommand.Connection = mConn;

                //load saved data
                mCommand.CommandText = "select * from facilitypaymenttypes where code='dbtrel'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                if (mDataTable.Rows.Count == 0)
                {
                    mCommand.CommandText = "insert into facilitypaymenttypes(code,description,preventoverpay,visibilitysales,visibilitydebtorpayments) "
                    + "values('dbtrel','Debt Relief',1,0,1)";
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    mCommand.CommandText = "update facilitypaymenttypes set preventoverpay=1,visibilitysales=0,visibilitydebtorpayments=1 where code='dbtrel'";
                    mCommand.ExecuteNonQuery();
                }

                //return
                return;
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
            finally
            {
                mConn.Close();
            }
        }

        #endregion

        #region Synchronize_MTUHA_Diagnoses

        private void Synchronize_MTUHA_Diagnoses()
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            DataTable mDataTable = new DataTable("dxtmtuhadiagnoses");
            DataView mDataView = new DataView();

            String mFunctionName = "Synchronize_MTUHA_Diagnoses";

            try
            {
                mConn.ConnectionString = gAfyaConStr;
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }
                mCommand.Connection = mConn;

                //load saved data
                mCommand.CommandText = "select * from dxtmtuhadiagnoses";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);
                mDataView.Table = mDataTable;
                mDataView.Sort = "code";

                clsDiagnoses mDiagnoses = new clsDiagnoses();
                DataTable mDtDiagnoses = mDiagnoses.Get_MTUHA_Diagnoses();

                foreach (DataRow mDataRow in mDtDiagnoses.Rows)
                {
                    if (mDataView.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into dxtmtuhadiagnoses(code,description) values('"
                        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                //return
                return;
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
            finally
            {
                mConn.Close();
            }
        }

        #endregion

        #region Synchronize_EnglishLanguage
        private void Synchronize_EnglishLanguage()
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            DataTable mDataTable = new DataTable("englishlanguage");
            DataSet mDataSet = new DataSet();
            DataView mDataView = new DataView();

            String mFunctionName = "Synchronize_EnglishLanguage";

            try
            {
                mConn.ConnectionString = gAfyaConStr;
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }
                mCommand.Connection = mConn;

                #region englishlanguagemessages

                mDataTable = new DataTable("ClientMessages");
                mCommand.CommandText = "select * from englishlanguagemessages";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);
                mDataView.Table = mDataTable;
                mDataView.Sort = "controlname";

                Type mMessagesIds = typeof(AfyaPro_Types.clsSystemMessages.MessageIds);
                foreach (string mMessageId in Enum.GetNames(mMessagesIds))
                {
                    if (mDataView.Find(mMessageId) < 0)
                    {
                        mCommand.CommandText = "insert into englishlanguagemessages(objecttype,controlname,description) "
                        + "values('ClientMessages','" + mMessageId + "','" + mMessageId + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                mDataTable = new DataTable("ClientMessages");
                mCommand.CommandText = "select controlname,description from englishlanguagemessages";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);
                mDataSet.Tables.Add(mDataTable);

                #endregion

                #region englishlanguageaccessfunctions

                mDataTable = new DataTable("AccessFunctions");
                mCommand.CommandText = "select * from englishlanguageaccessfunctions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);
                mDataView.Table = mDataTable;
                mDataView.Sort = "controlname";

                Type mFunctionKeys = typeof(AfyaPro_Types.clsSystemFunctions.FunctionKeys);
                foreach (string mFunctionKey in Enum.GetNames(mFunctionKeys))
                {
                    if (mDataView.Find(mFunctionKey) < 0)
                    {
                        mCommand.CommandText = "insert into englishlanguageaccessfunctions(objecttype,controlname,description) "
                        + "values('AccessFunctions','" + mFunctionKey + "','" + mFunctionKey + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                mDataTable = new DataTable("AccessFunctions");
                mCommand.CommandText = "select controlname,description from englishlanguageaccessfunctions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);
                mDataSet.Tables.Add(mDataTable);

                #endregion

                #region englishlanguage

                mDataTable = new DataTable("XtraEditor");
                mCommand.CommandText = "select controlname,description from englishlanguage where objecttype='XtraEditor'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);
                mDataSet.Tables.Add(mDataTable);

                mDataTable = new DataTable("XtraGrid");
                mCommand.CommandText = "select controlname,description from englishlanguage where objecttype='XtraGrid'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);
                mDataSet.Tables.Add(mDataTable);

                mDataTable = new DataTable("XtraWizard");
                mCommand.CommandText = "select controlname,description from englishlanguage where objecttype='XtraWizard'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);
                mDataSet.Tables.Add(mDataTable);

                #endregion

                mDataSet.WriteXml(@"c:\englishlanguage.xml");
                
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Synchronize_Admin
        private void Synchronize_Admin()
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            DataTable mDtUsers = new DataTable("sys_users");
            DataTable mDtModuleItems = new DataTable("sys_usergroupmoduleitems");
            DataView mDvModuleItems = new DataView();
            DataTable mDtFunctionAccessKeys = new DataTable("sys_usergroupfunctions");
            DataView mDvFunctionAccessKeys = new DataView();
            DataTable mDtUserGroups = new DataTable("sys_usergroups");

            String mFunctionName = "Synchronize_Admin";

            try
            {
                mConn.ConnectionString = gAfyaConStr;
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }
                mCommand.Connection = mConn;

                //usergroups
                mCommand.CommandText = "select * from sys_usergroups where code='admin'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtUserGroups);
                if (mDtUserGroups.Rows.Count <= 0)
                {
                    mCommand.CommandText = "insert into sys_usergroups(code,description,staffcategorycode,synchronizewithstaffs) "
                        + "values('admin','Administrators', -1, 0)";
                    mCommand.ExecuteNonQuery();
                }

                //moduleitems
                mCommand.CommandText = "select * from sys_usergroupmoduleitems where usergroupcode='admin'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtModuleItems);
                mDvModuleItems.Table = mDtModuleItems;
                mDvModuleItems.Sort = "moduleitemkey";

                clsUserGroups mUserGroups = new clsUserGroups();
                DataTable mDtModules = mUserGroups.Get_ModuleItems("", "");
                for (Int16 mCount = 0; mCount < mDtModules.Rows.Count; mCount++)
                {
                    if (mDvModuleItems.Find(mDtModules.Rows[mCount]["moduleitemkey"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into sys_usergroupmoduleitems(usergroupcode,moduleitemkey) "
                            + "values('admin','" + mDtModules.Rows[mCount]["moduleitemkey"].ToString().Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                //reports
                DataTable mDtUserReports = new DataTable("sys_usergroupreports");
                mCommand.CommandText = "select * from sys_usergroupreports where usergroupcode='admin'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtUserReports);
                DataView mDvUserReports = new DataView();
                mDvUserReports.Table = mDtUserReports;
                mDvUserReports.Sort = "reportcode";

                DataTable mDtReports = new DataTable("sys_reports");
                mCommand.CommandText = "select * from sys_reports";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtReports);

                for (Int16 mCount = 0; mCount < mDtReports.Rows.Count; mCount++)
                {
                    if (mDvUserReports.Find(mDtReports.Rows[mCount]["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into sys_usergroupreports(usergroupcode,reportcode,reportview,"
                        + "reportdesign,reportformcustomization) values('admin','" + mDtReports.Rows[mCount]["code"].ToString().Trim() 
                        + "',1,1,1)";
                        mCommand.ExecuteNonQuery();
                    }
                }

                //accessfunctions
                mCommand.CommandText = "select * from sys_usergroupfunctions where usergroupcode='admin'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFunctionAccessKeys);
                mDvFunctionAccessKeys.Table = mDtFunctionAccessKeys;
                mDvFunctionAccessKeys.Sort = "functionaccesskey";

                DataTable mDtAccessFunctions = mUserGroups.Get_AccessFunctions("", "");
                for (Int16 mCount = 0; mCount < mDtAccessFunctions.Rows.Count; mCount++)
                {
                    if (mDtAccessFunctions.Rows[mCount]["functionaccesskey"].ToString().Trim().ToLower() !=
                        AfyaPro_Types.clsSystemFunctions.FunctionKeys.opdregistrations_promptforbilling.ToString().ToLower()
                        && mDtAccessFunctions.Rows[mCount]["functionaccesskey"].ToString().Trim().ToLower() !=
                        AfyaPro_Types.clsSystemFunctions.FunctionKeys.opdregistrations_changechargestatus.ToString().ToLower()
                        && mDtAccessFunctions.Rows[mCount]["functionaccesskey"].ToString().Trim().ToLower() !=
                        AfyaPro_Types.clsSystemFunctions.FunctionKeys.ipdregistrations_promptforbilling.ToString().ToLower()
                        && mDtAccessFunctions.Rows[mCount]["functionaccesskey"].ToString().Trim().ToLower() !=
                        AfyaPro_Types.clsSystemFunctions.FunctionKeys.ipdregistrations_changechargestatus.ToString().ToLower()
                        && mDtAccessFunctions.Rows[mCount]["functionaccesskey"].ToString().Trim().ToLower() !=
                        AfyaPro_Types.clsSystemFunctions.FunctionKeys.bilpostbills_affectbillsdirect.ToString().ToLower()
                        && mDtAccessFunctions.Rows[mCount]["functionaccesskey"].ToString().Trim().ToLower() !=
                        AfyaPro_Types.clsSystemFunctions.FunctionKeys.bilpostbills_incomingbilladd.ToString().ToLower()
                        && mDtAccessFunctions.Rows[mCount]["functionaccesskey"].ToString().Trim().ToLower() !=
                        AfyaPro_Types.clsSystemFunctions.FunctionKeys.bilpostbills_incomingbilldelete.ToString().ToLower()
                        && mDtAccessFunctions.Rows[mCount]["functionaccesskey"].ToString().Trim().ToLower() !=
                        AfyaPro_Types.clsSystemFunctions.FunctionKeys.bilpostbills_billunbooked.ToString().ToLower())
                    {
                        if (mDvFunctionAccessKeys.Find(mDtAccessFunctions.Rows[mCount]["functionaccesskey"].ToString().Trim()) < 0)
                        {
                            mCommand.CommandText = "insert into sys_usergroupfunctions(usergroupcode,functionaccesskey) "
                                + "values('admin','" + mDtAccessFunctions.Rows[mCount]["functionaccesskey"].ToString().Trim() + "')";
                            mCommand.ExecuteNonQuery();
                        }
                    }
                }

                //users
                mCommand.CommandText = "select * from sys_users where code='admin'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtUsers);
                if (mDtUsers.Rows.Count <= 0)
                {
                    //encrypt password
                    clsCryptoService mCryptoService = new clsCryptoService(clsCryptoService.SymmProvEnum.Rijndael);
                    String mUserPwd = mCryptoService.Encrypt("admin", gCryptoKey);

                    mCommand.CommandText = "insert into sys_users(code,description,password,"
                    + "usergroupcode) values('admin','Administrator','" + mUserPwd + "','admin')";
                    mCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Synchronize_DischargeStatus
        private void Synchronize_DischargeStatus()
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            String mFunctionName = "Synchronize_DischargeStatus";

            try
            {
                mConn.ConnectionString = gAfyaConStr;
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }
                mCommand.Connection = mConn;

                DataTable mDtDischargeStatus = new DataTable("facilitydischargestatus");
                mCommand.CommandText = "select * from facilitydischargestatus";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtDischargeStatus);

                DataView mDvDischargeStatus = new DataView();
                mDvDischargeStatus.Table = mDtDischargeStatus;
                mDvDischargeStatus.Sort = "code";

                //death status
                if (mDvDischargeStatus.Find(AfyaPro_Types.clsEnums.IPDDischargeStatus.Death.ToString()) < 0)
                {
                    mCommand.CommandText = "insert into facilitydischargestatus(code, description) values('"
                    + AfyaPro_Types.clsEnums.IPDDischargeStatus.Death.ToString() + "','Death')";
                    mCommand.ExecuteNonQuery();
                }

                //abscondee status
                if (mDvDischargeStatus.Find(AfyaPro_Types.clsEnums.IPDDischargeStatus.Abscondee.ToString()) < 0)
                {
                    mCommand.CommandText = "insert into facilitydischargestatus(code, description) values('"
                    + AfyaPro_Types.clsEnums.IPDDischargeStatus.Abscondee.ToString() + "','Abscondee')";
                    mCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Synchronize_BuiltInReports
        private void Synchronize_BuiltInReports()
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            String mFunctionName = "Synchronize_ReportGroups";

            try
            {
                mConn.ConnectionString = gAfyaConStr;
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }
                mCommand.Connection = mConn;

                #region reportgroups

                DataView mDvReportGroups = new DataView();
                DataTable mDtReportGroups = new DataTable("sys_reportgroups");
                mCommand.CommandText = "select * from sys_reportgroups where code='BTIN'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtReportGroups);

                if (mDtReportGroups.Rows.Count == 0)
                {
                    mCommand.CommandText = "insert into sys_reportgroups(code,description) values("
                    + "'BTIN','BUILT IN REPORTS')";
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region reports

                DataView mDvReports = new DataView();
                DataTable mDtReports = new DataTable("sys_reports");
                mCommand.CommandText = "select * from sys_reports";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtReports);
                mDvReports.Table = mDtReports;
                mDvReports.Sort = "code";

                Type mReports = typeof(AfyaPro_Types.clsEnums.BuiltInReports);
                foreach (int mReportCode in Enum.GetValues(mReports))
                {
                    if (mDvReports.Find("REP" + mReportCode) < 0)
                    {
                        mCommand.CommandText = "insert into sys_reports(groupcode,code,description) values("
                        + "'BTIN','REP" + mReportCode + "','" + Enum.GetName(mReports, mReportCode) + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region rch extra fields

        #region Synchronize_FamilyPlanningMethods
        private void Synchronize_FamilyPlanningMethods()
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            String mFunctionName = "Synchronize_FamilyPlanningMethods";

            try
            {
                mConn.ConnectionString = gAfyaConStr;
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }
                mCommand.Connection = mConn;

                DataTable mDataTable = new DataTable("planningmethods");
                mDataTable.Columns.Add("code", typeof(System.String));
                mDataTable.Columns.Add("description", typeof(System.String));
                mDataTable.Columns.Add("quantityentry", typeof(System.Int16));

                DataRow mNewRow;

                #region load planning methods

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "fpmethod" + (int)AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods.implanon;
                mNewRow["description"] = "Implanon";
                mNewRow["quantityentry"] = 0;
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "fpmethod" + (int)AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods.oralpills;
                mNewRow["description"] = "Oral pills";
                mNewRow["quantityentry"] = 1;
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "fpmethod" + (int)AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods.injection;
                mNewRow["description"] = "Injection";
                mNewRow["quantityentry"] = 1;
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "fpmethod" + (int)AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods.diaphragm;
                mNewRow["description"] = "Diaphragm";
                mNewRow["quantityentry"] = 0;
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "fpmethod" + (int)AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods.iucd;
                mNewRow["description"] = "IUCD";
                mNewRow["quantityentry"] = 0;
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "fpmethod" + (int)AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods.condoms;
                mNewRow["description"] = "Condoms";
                mNewRow["quantityentry"] = 1;
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "fpmethod" + (int)AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods.foamingtablets;
                mNewRow["description"] = "Foaming tablets";
                mNewRow["quantityentry"] = 1;
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "fpmethod" + (int)AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods.bilateraltubeligation;
                mNewRow["description"] = "Bilateral tubeligation";
                mNewRow["quantityentry"] = 0;
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "fpmethod" + (int)AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods.naturalmethods;
                mNewRow["description"] = "Natural methods";
                mNewRow["quantityentry"] = 0;
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                #endregion

                DataTable mDtMethods = new DataTable("rch_fplanmethods");
                mCommand.CommandText = "select * from rch_fplanmethods";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtMethods);

                DataView mDvMethods = new DataView();
                mDvMethods.Table = mDtMethods;
                mDvMethods.Sort = "code";

                foreach (DataRow mDataRow in mDataTable.Rows)
                {
                    if (mDvMethods.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into rch_fplanmethods(code,description,quantityentry) values('"
                        + mDataRow["code"] + "','" + mDataRow["description"] + "'," + mDataRow["quantityentry"] + ")";
                        mCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Synchronize_DangerIndicators
        private void Synchronize_DangerIndicators()
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            String mFunctionName = "Synchronize_DangerIndicators";

            try
            {
                mConn.ConnectionString = gAfyaConStr;
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }
                mCommand.Connection = mConn;

                DataTable mDataTable = new DataTable("dangerindicators");
                mDataTable.Columns.Add("code", typeof(System.String));
                mDataTable.Columns.Add("description", typeof(System.String));
                mDataTable.Columns.Add("quantityentry", typeof(System.Int16));

                DataRow mNewRow;

                #region load danger indicators

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "danger" + (int)AfyaPro_Types.clsEnums.RCHDangerIndicators.abortion;
                mNewRow["description"] = "Abortion";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "danger" + (int)AfyaPro_Types.clsEnums.RCHDangerIndicators.caesariansection;
                mNewRow["description"] = "Caesarian Section";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "danger" + (int)AfyaPro_Types.clsEnums.RCHDangerIndicators.anaemia;
                mNewRow["description"] = "Anaemia";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "danger" + (int)AfyaPro_Types.clsEnums.RCHDangerIndicators.oedema;
                mNewRow["description"] = "Oedema";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "danger" + (int)AfyaPro_Types.clsEnums.RCHDangerIndicators.protenuria;
                mNewRow["description"] = "Protenuria";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "danger" + (int)AfyaPro_Types.clsEnums.RCHDangerIndicators.highbloodpressure;
                mNewRow["description"] = "High blood pressure";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "danger" + (int)AfyaPro_Types.clsEnums.RCHDangerIndicators.retardedweight;
                mNewRow["description"] = "Retarded weight";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "danger" + (int)AfyaPro_Types.clsEnums.RCHDangerIndicators.bleeding;
                mNewRow["description"] = "Bleeding";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "danger" + (int)AfyaPro_Types.clsEnums.RCHDangerIndicators.badbabyposition;
                mNewRow["description"] = "Bad baby position";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                #endregion

                DataTable mDtMethods = new DataTable("rch_dangerindicators");
                mCommand.CommandText = "select * from rch_dangerindicators";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtMethods);

                DataView mDvMethods = new DataView();
                mDvMethods.Table = mDtMethods;
                mDvMethods.Sort = "code";

                foreach (DataRow mDataRow in mDataTable.Rows)
                {
                    if (mDvMethods.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into rch_dangerindicators(code,description) values('"
                        + mDataRow["code"] + "','" + mDataRow["description"] + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Synchronize_BirthComplications
        internal static void Synchronize_BirthComplications()
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            String mFunctionName = "Synchronize_BirthComplications";

            try
            {
                mConn.ConnectionString = gAfyaConStr;
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }
                mCommand.Connection = mConn;

                DataTable mDataTable = new DataTable("birthcomplications");
                mDataTable.Columns.Add("code", typeof(System.String));
                mDataTable.Columns.Add("description", typeof(System.String));

                DataRow mNewRow;

                #region load birth complications

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "compl" + (int)AfyaPro_Types.clsEnums.RCHBirthComplication.postpartumhaemorrg;
                mNewRow["description"] = "Post-Partum Haemorrg";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "compl" + (int)AfyaPro_Types.clsEnums.RCHBirthComplication.retainedplacenta;
                mNewRow["description"] = "Retained placenta";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "compl" + (int)AfyaPro_Types.clsEnums.RCHBirthComplication.thirddegreetear;
                mNewRow["description"] = "Third degree tear";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "compl" + (int)AfyaPro_Types.clsEnums.RCHBirthComplication.death;
                mNewRow["description"] = "Death";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                #endregion

                DataTable mDtMethods = new DataTable("rch_birthcomplications");
                mCommand.CommandText = "select * from rch_birthcomplications";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtMethods);

                DataView mDvMethods = new DataView();
                mDvMethods.Table = mDtMethods;
                mDvMethods.Sort = "code";

                foreach (DataRow mDataRow in mDataTable.Rows)
                {
                    if (mDvMethods.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into rch_birthcomplications(code,description) values('"
                        + mDataRow["code"] + "','" + mDataRow["description"] + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Synchronize_BirthMethods
        internal static void Synchronize_BirthMethods()
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            String mFunctionName = "Synchronize_BirthMethods";

            try
            {
                mConn.ConnectionString = gAfyaConStr;
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }
                mCommand.Connection = mConn;

                DataTable mDataTable = new DataTable("birthmethods");
                mDataTable.Columns.Add("code", typeof(System.String));
                mDataTable.Columns.Add("description", typeof(System.String));

                DataRow mNewRow;

                #region load birth methods

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "method" + (int)AfyaPro_Types.clsEnums.RCHBirthMethods.bba;
                mNewRow["description"] = "Birth Before Arrival (BBA)";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "method" + (int)AfyaPro_Types.clsEnums.RCHBirthMethods.normaldelivery;
                mNewRow["description"] = "Normal delivery";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "method" + (int)AfyaPro_Types.clsEnums.RCHBirthMethods.vacuum;
                mNewRow["description"] = "Vacuum";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "method" + (int)AfyaPro_Types.clsEnums.RCHBirthMethods.caesariansection;
                mNewRow["description"] = "Caesarian section";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "method" + (int)AfyaPro_Types.clsEnums.RCHBirthMethods.abortion;
                mNewRow["description"] = "Abortion";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                #endregion

                DataTable mDtMethods = new DataTable("rch_birthmethods");
                mCommand.CommandText = "select * from rch_birthmethods";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtMethods);

                DataView mDvMethods = new DataView();
                mDvMethods.Table = mDtMethods;
                mDvMethods.Sort = "code";

                foreach (DataRow mDataRow in mDataTable.Rows)
                {
                    if (mDvMethods.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into rch_birthmethods(code,description) values('"
                        + mDataRow["code"] + "','" + mDataRow["description"] + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Synchronize_Vaccines
        internal static void Synchronize_Vaccines()
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            String mFunctionName = "Synchronize_Vaccines";

            try
            {
                mConn.ConnectionString = gAfyaConStr;
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }
                mCommand.Connection = mConn;

                DataTable mDataTable = new DataTable("vaccines");
                mDataTable.Columns.Add("code", typeof(System.String));
                mDataTable.Columns.Add("description", typeof(System.String));

                DataRow mNewRow;

                #region load birth methods

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "vacc" + (int)AfyaPro_Types.clsEnums.RCHVaccines.tt;
                mNewRow["description"] = "TT";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "vacc" + (int)AfyaPro_Types.clsEnums.RCHVaccines.bcg;
                mNewRow["description"] = "BCG";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "vacc" + (int)AfyaPro_Types.clsEnums.RCHVaccines.sp1;
                mNewRow["description"] = "SP 1";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "vacc" + (int)AfyaPro_Types.clsEnums.RCHVaccines.sp2;
                mNewRow["description"] = "SP 2";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "vacc" + (int)AfyaPro_Types.clsEnums.RCHVaccines.dpt1;
                mNewRow["description"] = "DPT 1";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "vacc" + (int)AfyaPro_Types.clsEnums.RCHVaccines.dpt2;
                mNewRow["description"] = "DPT 2";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "vacc" + (int)AfyaPro_Types.clsEnums.RCHVaccines.dpt3;
                mNewRow["description"] = "DPT 3";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "vacc" + (int)AfyaPro_Types.clsEnums.RCHVaccines.opv0;
                mNewRow["description"] = "OPV 0";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "vacc" + (int)AfyaPro_Types.clsEnums.RCHVaccines.opv1;
                mNewRow["description"] = "OPV 1";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "vacc" + (int)AfyaPro_Types.clsEnums.RCHVaccines.opv2;
                mNewRow["description"] = "OPV 2";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "vacc" + (int)AfyaPro_Types.clsEnums.RCHVaccines.opv3;
                mNewRow["description"] = "OPV 3";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "vacc" + (int)AfyaPro_Types.clsEnums.RCHVaccines.measles;
                mNewRow["description"] = "Measles";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "vacc" + (int)AfyaPro_Types.clsEnums.RCHVaccines.vitasupp;
                mNewRow["description"] = "Vit.A suppliment";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                #endregion

                DataTable mDtMethods = new DataTable("rch_vaccines");
                mCommand.CommandText = "select * from rch_vaccines";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtMethods);

                DataView mDvMethods = new DataView();
                mDvMethods.Table = mDtMethods;
                mDvMethods.Sort = "code";

                foreach (DataRow mDataRow in mDataTable.Rows)
                {
                    if (mDvMethods.Find(mDataRow["code"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into rch_vaccines(code,description) values('"
                        + mDataRow["code"] + "','" + mDataRow["description"] + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #endregion

        #region product extra fields

        #region Synchronize_ExtraProductInfos
        private void Synchronize_ExtraProductInfos()
        {
            string mAlterTableStatement = "";
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            String mFunctionName = "Synchronize_ExtraProductInfos";

            try
            {
                mConn.ConnectionString = gAfyaConStr;
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }
                mCommand.Connection = mConn;

                //som_orderitems
                mAlterTableStatement = this.Build_AlterTableForProductExtraInfos("som_orderitems");
                if (mAlterTableStatement.Trim() != "")
                {
                    mCommand.CommandText = mAlterTableStatement;
                    mCommand.ExecuteNonQuery();
                }

                //som_orderreceiveditems
                mAlterTableStatement = this.Build_AlterTableForProductExtraInfos("som_orderreceiveditems");
                if (mAlterTableStatement.Trim() != "")
                {
                    mCommand.CommandText = mAlterTableStatement;
                    mCommand.ExecuteNonQuery();
                }

                //som_transferinitems
                mAlterTableStatement = this.Build_AlterTableForProductExtraInfos("som_transferinitems");
                if (mAlterTableStatement.Trim() != "")
                {
                    mCommand.CommandText = mAlterTableStatement;
                    mCommand.ExecuteNonQuery();
                }

                //som_transferinreceiveditems
                mAlterTableStatement = this.Build_AlterTableForProductExtraInfos("som_transferinreceiveditems");
                if (mAlterTableStatement.Trim() != "")
                {
                    mCommand.CommandText = mAlterTableStatement;
                    mCommand.ExecuteNonQuery();
                }

                //som_physicalinventoryitems
                mAlterTableStatement = this.Build_AlterTableForProductExtraInfos("som_physicalinventoryitems");
                if (mAlterTableStatement.Trim() != "")
                {
                    mCommand.CommandText = mAlterTableStatement;
                    mCommand.ExecuteNonQuery();
                }

                //som_producttransactions
                mAlterTableStatement = this.Build_AlterTableForProductExtraInfos("som_producttransactions");
                if (mAlterTableStatement.Trim() != "")
                {
                    mCommand.CommandText = mAlterTableStatement;
                    mCommand.ExecuteNonQuery();
                }

                //som_stockreceiptitems
                mAlterTableStatement = this.Build_AlterTableForProductExtraInfos("som_stockreceiptitems");
                if (mAlterTableStatement.Trim() != "")
                {
                    mCommand.CommandText = mAlterTableStatement;
                    mCommand.ExecuteNonQuery();
                }

                //som_stockissueitems
                mAlterTableStatement = this.Build_AlterTableForProductExtraInfos("som_stockissueitems");
                if (mAlterTableStatement.Trim() != "")
                {
                    mCommand.CommandText = mAlterTableStatement;
                    mCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Build_AlterTableForProductExtraInfos
        private string Build_AlterTableForProductExtraInfos(string mTableName)
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            string mAlterTableStr = "";

            string mFunctionName = "Build_AlterTableForProductExtraInfos";

            try
            {
                mConn.ConnectionString = gAfyaConStr;
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }
                mCommand.Connection = mConn;

                DataTable mDtColumns;
                Type mProductExtraInfos = typeof(AfyaPro_Types.clsEnums.ProductExtraInfos);

                mDtColumns = new DataTable("columns");
                mCommand.CommandText = "select * from " + mTableName + " where 1=2";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.FillSchema(mDtColumns, SchemaType.Source);

                foreach (string mProductInfo in Enum.GetNames(mProductExtraInfos))
                {
                    if (mDtColumns.Columns.Contains("product" + mProductInfo.Trim().ToLower()) == false)
                    {
                        #region mysql
                        if (gAfyaPro_ServerType == 0)
                        {
                            if (mAlterTableStr.Trim() == "")
                            {
                                mAlterTableStr = "ALTER TABLE " + mTableName + " ADD product" + mProductInfo.Trim().ToLower() + " VARCHAR(255) DEFAULT ''";
                            }
                            else
                            {
                                mAlterTableStr = mAlterTableStr + ", ADD product" + mProductInfo.Trim().ToLower() + " VARCHAR(255) DEFAULT ''";
                            }
                        }
                        #endregion

                        #region sql server
                        if (gAfyaPro_ServerType == 1)
                        {
                            if (mAlterTableStr.Trim() == "")
                            {
                                mAlterTableStr = "ALTER TABLE " + mTableName + " ADD product" + mProductInfo.Trim().ToLower() + " VARCHAR(255) DEFAULT ''";
                            }
                            else
                            {
                                mAlterTableStr = mAlterTableStr + ", ADD product" + mProductInfo.Trim().ToLower() + " VARCHAR(255) DEFAULT ''";
                            }
                        }
                        #endregion
                    }
                }

                return mAlterTableStr;
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return "";
            }
        }
        #endregion

        #region Build_FieldsForProductExtraInfos
        internal static string Build_FieldsForProductExtraInfos()
        {
            string mProductFields = "";
            string mFunctionName = "Build_FieldsForProductExtraInfos";

            try
            {
                Type mProductExtraInfos = typeof(AfyaPro_Types.clsEnums.ProductExtraInfos);
                foreach (string mProductInfo in Enum.GetNames(mProductExtraInfos))
                {
                    if (mProductFields.Trim() == "")
                    {
                        mProductFields = "product" + mProductInfo.Trim().ToLower();
                    }
                    else
                    {
                        mProductFields = mProductFields + ",product" + mProductInfo.Trim().ToLower();
                    }
                }

                return mProductFields;
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return "";
            }
        }
        #endregion

        #region Build_FieldValuesForProductExtraInfos
        internal static string Build_FieldValuesForProductExtraInfos(DataView mDataView, string mValue)
        {
            string mProductFieldValues = "";
            string mFunctionName = "Build_FieldValuesForProductExtraInfos";

            try
            {
                int mRowIndex = mDataView.Find(mValue.Trim());

                if (mRowIndex >= 0)
                {
                    DataRowView mDataRowView = mDataView[mRowIndex];

                    Type mProductExtraInfos = typeof(AfyaPro_Types.clsEnums.ProductExtraInfos);
                    foreach (string mProductInfo in Enum.GetNames(mProductExtraInfos))
                    {
                        string mFieldValue = "'" + mDataRowView[mProductInfo.Trim().ToLower()].ToString().Trim() + "'";

                        if (mProductFieldValues.Trim() == "")
                        {
                            mProductFieldValues = mFieldValue;
                        }
                        else
                        {
                            mProductFieldValues = mProductFieldValues + "," + mFieldValue;
                        }
                    }
                }

                return mProductFieldValues;
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return "";
            }
        }
        #endregion

        #region Get_ProductExtraFields
        internal static List<clsDataField> Get_ProductExtraFields()
        {
            string mFunctionName = "Get_ProductExtraFields";

            try
            {
                List<clsDataField> mDataFields = new List<clsDataField>();

                Type mProductExtraInfos = typeof(AfyaPro_Types.clsEnums.ProductExtraInfos);
                foreach (string mProductInfo in Enum.GetNames(mProductExtraInfos))
                {
                    mDataFields.Add(new clsDataField(mProductInfo.Trim().ToLower(), DataTypes.dbstring.ToString(), ""));
                }

                return mDataFields;
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
        }
        #endregion

        #endregion

        #region Synchronize_VoidedSalesTable
        private void Synchronize_VoidedSalesTable(string mSourceTableName, string mTargetTableName)
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            String mFunctionName = "Synchronize_VoidedSalesTable";

            try
            {
                mConn.ConnectionString = gAfyaConStr;
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }
                mCommand.Connection = mConn;

                String[] mRestrictions = new String[3];
                mRestrictions[0] = null;
                mRestrictions[1] = null;
                mRestrictions[2] = mSourceTableName;

                //get source columns
                DataTable mDtSource = mConn.GetSchema("Columns", mRestrictions);

                //get target columns
                mRestrictions[2] = mTargetTableName;
                DataTable mDtDestination = mConn.GetSchema("Columns", mRestrictions);

                DataView mDvDestination = new DataView();
                mDvDestination.Table = mDtDestination;
                mDvDestination.Sort = "COLUMN_NAME";

                string mTableSql = "ALTER TABLE " + mTargetTableName + " ";
                bool mFieldFound = false;
                bool mFirstField = true;

                foreach (DataRow mDataRow in mDtSource.Rows)
                {
                    String mFieldName = mDataRow["COLUMN_NAME"].ToString();
                    Int16 mColumnType = Convert.ToInt16(mDataRow["DATA_TYPE"]);
                    Int32 mFieldLength = Convert.ToInt32(mDataRow["COLUMN_SIZE"]);
                    Int16 mNullable = Convert.ToInt16(mDataRow["NULLABLE"]);
                    String mDefaultValue = "";

                    String mDataType = "";
                    String mColumnStr = "";
                    String mAllowNull = "";
                    String mIdentity = "";

                    if (mDvDestination.Find(mFieldName) < 0)
                    {
                        #region MySql Server

                        if (gAfyaPro_ServerType == 0)
                        {
                            switch (mColumnType)
                            {
                                case 12://varchar
                                    {
                                        mDataType = " VARCHAR(" + mFieldLength.ToString() + ") ";
                                        mDefaultValue = "DEFAULT ''";
                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "NOT NULL ";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull + mDefaultValue;
                                    }
                                    break;

                                case 4://integer
                                    {
                                        mDataType = " INTEGER(11) ";
                                        if (mFieldName.ToLower() == "autocode")
                                        {
                                            mDefaultValue = "";
                                            mIdentity = "AUTO_INCREMENT ";
                                        }
                                        else
                                        {
                                            mDefaultValue = "DEFAULT '0'";
                                        }

                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "NOT NULL ";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull + mIdentity + mDefaultValue;
                                    }
                                    break;

                                case 93://datetime
                                    {
                                        mDataType = " DATETIME ";

                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "NOT NULL ";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull;
                                    }
                                    break;

                                case 3://decimal
                                    {
                                        mDataType = " DECIMAL(16,2) ";
                                        mDefaultValue = "DEFAULT '0.00'";

                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "NOT NULL ";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull + mDefaultValue;
                                    }
                                    break;

                                case 8://double
                                    {
                                        mDataType = " DOUBLE(16,2) ";
                                        mDefaultValue = "DEFAULT '0.00'";

                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "NOT NULL ";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull + mDefaultValue;
                                    }
                                    break;

                                case 2://numeric
                                    {
                                        mDataType = " NUMERIC(16,2) ";
                                        mDefaultValue = "DEFAULT '0.00'";

                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "NOT NULL ";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull + mDefaultValue;
                                    }
                                    break;

                                default:
                                    {
                                        mColumnStr = mColumnType.ToString();
                                    }
                                    break;
                            }
                        }

                        #endregion

                        #region MS SQL Server

                        if (gAfyaPro_ServerType == 1)
                        {
                            switch (mColumnType)
                            {
                                case 12://varchar
                                    {
                                        mDataType = " VARCHAR(" + mFieldLength.ToString() + ") ";
                                        mDefaultValue = "DEFAULT ''";

                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "NOT NULL ";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull + mDefaultValue;
                                    }
                                    break;

                                case 4://integer
                                    {
                                        mDataType = " INT ";
                                        if (mFieldName.ToLower() == "autocode")
                                        {
                                            mDefaultValue = "";
                                            mIdentity = "IDENTITY ";
                                        }
                                        else
                                        {
                                            mDefaultValue = "DEFAULT '0'";
                                        }

                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "NOT NULL ";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull + mIdentity + mDefaultValue;
                                    }
                                    break;

                                case 93://datetime
                                    {
                                        mDataType = " DATETIME ";

                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "NOT NULL ";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull;
                                    }
                                    break;

                                case 3://decimal
                                    {
                                        mDataType = " DECIMAL(16,2) ";
                                        mDefaultValue = "DEFAULT '0.00'";

                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "NOT NULL ";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull + mDefaultValue;
                                    }
                                    break;

                                case 8://double
                                    {
                                        mDataType = " DECIMAL(16,2) ";
                                        mDefaultValue = "DEFAULT '0.00'";

                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "NOT NULL ";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull + mDefaultValue;
                                    }
                                    break;

                                case 2://numeric
                                    {
                                        mDataType = " NUMERIC(16,2) ";
                                        mDefaultValue = "DEFAULT '0.00'";

                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "NOT NULL ";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull + mDefaultValue;
                                    }
                                    break;

                                default:
                                    {
                                        mColumnStr = mColumnType.ToString();
                                    }
                                    break;
                            }
                        }

                        #endregion

                        if (mFirstField == true)
                        {
                            mTableSql += "ADD " + mColumnStr;
                            mFirstField = false;
                        }
                        else
                        {
                            mTableSql += "," + Environment.NewLine + "ADD " + mColumnStr;
                        }

                        mFieldFound = true;
                    }
                }

                if (mFieldFound == true)
                {
                    mCommand.CommandText = mTableSql;
                    mCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Build_ColumnForAlter
        internal static string Build_ColumnForAlter(string mFieldName, string mDataType)
        {
            string mAlterStatement = "";

            if (mDataType.Trim().ToLower() == AfyaPro_Types.clsEnums.DataTypes.datetime.ToString().ToLower())
            {
                mAlterStatement = mFieldName + "  DATETIME";
            }
            else if (mDataType.Trim().ToLower() == AfyaPro_Types.clsEnums.DataTypes.number.ToString().ToLower())
            {
                if (clsGlobal.gAfyaPro_ServerType == 0)
                {
                    mAlterStatement = mFieldName + "  INT(11) DEFAULT '0'";
                }
                else
                {
                    mAlterStatement = mFieldName + "  INT DEFAULT '0'";
                }
            }
            else if (mDataType.Trim().ToLower() == AfyaPro_Types.clsEnums.DataTypes.money.ToString().ToLower())
            {
                if (clsGlobal.gAfyaPro_ServerType == 0)
                {
                    mAlterStatement = mFieldName + "  DOUBLE(16,2) DEFAULT '0.00'";
                }
                else
                {
                    mAlterStatement = mFieldName + "  DECIMAL(16,2) DEFAULT '0.00'";
                }
            }
            else
            {
                mAlterStatement = mFieldName + "  VARCHAR(255) DEFAULT ''";
            }

            return mAlterStatement;
        }
        #endregion

        #region Build_InsertToTable
        internal static string Build_InsertToTable(DataRow mDataRow, DataColumnCollection mDataColumns, string mTargetTableName)
        {
            string mFieldList = "";
            string mValueList = "";

            foreach (DataColumn mDataColumn in mDataColumns)
            {
                if (mDataColumn.ColumnName.ToLower() != "autocode")
                {
                    string mFieldValue = "";

                    switch (mDataColumn.DataType.FullName.ToLower())
                    {
                        case "system.decimal":
                            {
                                mFieldValue = Convert.ToDouble(mDataRow[mDataColumn.ColumnName]).ToString();
                            }
                            break;
                        case "system.double":
                            {
                                mFieldValue = Convert.ToDouble(mDataRow[mDataColumn.ColumnName]).ToString();
                            }
                            break;
                        case "system.int16":
                            {
                                mFieldValue = Convert.ToInt16(mDataRow[mDataColumn.ColumnName]).ToString();
                            }
                            break;
                        case "system.int32":
                            {
                                mFieldValue = Convert.ToInt32(mDataRow[mDataColumn.ColumnName]).ToString();
                            }
                            break;
                        case "system.int64":
                            {
                                mFieldValue = Convert.ToInt64(mDataRow[mDataColumn.ColumnName]).ToString();
                            }
                            break;
                        case "system.single":
                            {
                                mFieldValue = Convert.ToSingle(mDataRow[mDataColumn.ColumnName]).ToString();
                            }
                            break;
                        case "system.datetime":
                            {
                                mFieldValue = Saving_DateValueNullable(mDataRow[mDataColumn.ColumnName]);
                            }
                            break;
                        case "system.string":
                            {
                                mFieldValue = "'" + mDataRow[mDataColumn.ColumnName].ToString() + "'";
                            }
                            break;
                    }

                    if (mFieldList.Trim() == "")
                    {
                        mFieldList = mDataColumn.ColumnName;
                        mValueList = mFieldValue;
                    }
                    else
                    {
                        mFieldList = mFieldList + "," + mDataColumn.ColumnName;
                        mValueList = mValueList + "," + mFieldValue;
                    }
                }
            }

            string mFullSQL = "insert into " + mTargetTableName + "(" + mFieldList + ") values(" + mValueList + ")";
            return mFullSQL;
        }
        #endregion

        #region AuditingFields
        internal static string AuditingFields()
        {
            return "changeuserid,changesysdate,changetransdate,changetransdatetime,auditchangetype";
        }
        #endregion

        #region AuditingValues
        internal static string AuditingValues(DateTime mTransDate, string mUserId, string mChangeType)
        {
            string mAuditingValues = "";
            DateTime mTransDateTime =
                mTransDate.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);

            mAuditingValues = "'" + mUserId.Trim() + "'," + clsGlobal.Saving_DateValue(DateTime.Now)
            + "," + clsGlobal.Saving_DateValue(mTransDate) + "," + clsGlobal.Saving_DateValue(mTransDateTime)
            + ",'" + AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString() + "'";

            return mAuditingValues;
        }
        #endregion

        #region Audit_ThisRecord
        internal static string Audit_ThisRecord(DataTable mDataTable, string mTableName, 
            DateTime mTransDate, string mUserId, string mChangeType)
        {
            string mFieldList = "";
            string mValueList = "";

            foreach (DataColumn mDataColumn in mDataTable.Columns)
            {
                if (mDataColumn.ColumnName.ToLower() != "autocode")
                {
                    string mFieldValue = "";
                    DataRow mDataRow = mDataTable.Rows[0];

                    switch (mDataColumn.DataType.FullName.ToLower())
                    {
                        case "system.decimal":
                            {
                                mFieldValue = Convert.ToDouble(mDataRow[mDataColumn.ColumnName]).ToString();
                            }
                            break;
                        case "system.double":
                            {
                                mFieldValue = Convert.ToDouble(mDataRow[mDataColumn.ColumnName]).ToString();
                            }
                            break;
                        case "system.int16":
                            {
                                mFieldValue = Convert.ToInt16(mDataRow[mDataColumn.ColumnName]).ToString();
                            }
                            break;
                        case "system.int32":
                            {
                                mFieldValue = Convert.ToInt32(mDataRow[mDataColumn.ColumnName]).ToString();
                            }
                            break;
                        case "system.int64":
                            {
                                mFieldValue = Convert.ToInt64(mDataRow[mDataColumn.ColumnName]).ToString();
                            }
                            break;
                        case "system.single":
                            {
                                mFieldValue = Convert.ToSingle(mDataRow[mDataColumn.ColumnName]).ToString();
                            }
                            break;
                        case "system.datetime":
                            {
                                mFieldValue = Saving_DateValueNullable(mDataRow[mDataColumn.ColumnName]);
                            }
                            break;
                        case "system.string":
                            {
                                mFieldValue = "'" + mDataRow[mDataColumn.ColumnName].ToString() + "'";
                            }
                            break;
                    }

                    if (mFieldList.Trim() == "")
                    {
                        mFieldList = mDataColumn.ColumnName;
                        mValueList = mFieldValue;
                    }
                    else
                    {
                        mFieldList = mFieldList + "," + mDataColumn.ColumnName;
                        mValueList = mValueList + "," + mFieldValue;
                    }
                }
            }

            if (mFieldList.Trim() == "")
            {
                DateTime mTransDateTime = 
                    mTransDate.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
                mFieldList = "changeuserid,changesysdate,changetransdate,changetransdatetime,auditchangetype";
                mValueList = "'" + mUserId.Trim() + "'," + Saving_DateValue(DateTime.Now)
                + "," + Saving_DateValue(mTransDate) + "," + Saving_DateValue(mTransDateTime) + ",'" + mChangeType + "'";
            }
            else
            {
                DateTime mTransDateTime =
                    mTransDate.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
                mFieldList = mFieldList + ",changeuserid,changesysdate,changetransdate,changetransdatetime,auditchangetype";
                mValueList = mValueList + ",'" + mUserId.Trim() + "'," + Saving_DateValue(DateTime.Now)
                + "," + Saving_DateValue(mTransDate) + "," + Saving_DateValue(mTransDateTime) + ",'" + mChangeType + "'";
            }

            string mFullSQL = "insert into " + gAfyaProAuditDbName + gDbNameTableNameSep + mTableName 
            + "(" + mFieldList + ") values(" + mValueList + ")";
            return mFullSQL;
        }
        #endregion

        #region Audit_ThisRecord
        internal static string Audit_ThisRecord(DataTable mDataTable, DataRow mDataRow, string mTableName,
            DateTime mTransDate, string mUserId, string mChangeType)
        {
            string mFieldList = "";
            string mValueList = "";

            foreach (DataColumn mDataColumn in mDataTable.Columns)
            {
                if (mDataColumn.ColumnName.ToLower() != "autocode")
                {
                    string mFieldValue = "";

                    switch (mDataColumn.DataType.FullName.ToLower())
                    {
                        case "system.decimal":
                            {
                                mFieldValue = Convert.ToDouble(mDataRow[mDataColumn.ColumnName]).ToString();
                            }
                            break;
                        case "system.double":
                            {
                                mFieldValue = Convert.ToDouble(mDataRow[mDataColumn.ColumnName]).ToString();
                            }
                            break;
                        case "system.int16":
                            {
                                mFieldValue = Convert.ToInt16(mDataRow[mDataColumn.ColumnName]).ToString();
                            }
                            break;
                        case "system.int32":
                            {
                                mFieldValue = Convert.ToInt32(mDataRow[mDataColumn.ColumnName]).ToString();
                            }
                            break;
                        case "system.int64":
                            {
                                mFieldValue = Convert.ToInt64(mDataRow[mDataColumn.ColumnName]).ToString();
                            }
                            break;
                        case "system.single":
                            {
                                mFieldValue = Convert.ToSingle(mDataRow[mDataColumn.ColumnName]).ToString();
                            }
                            break;
                        case "system.datetime":
                            {
                                mFieldValue = Saving_DateValueNullable(mDataRow[mDataColumn.ColumnName]);
                            }
                            break;
                        case "system.string":
                            {
                                mFieldValue = "'" + mDataRow[mDataColumn.ColumnName].ToString() + "'";
                            }
                            break;
                    }

                    if (mFieldList.Trim() == "")
                    {
                        mFieldList = mDataColumn.ColumnName;
                        mValueList = mFieldValue;
                    }
                    else
                    {
                        mFieldList = mFieldList + "," + mDataColumn.ColumnName;
                        mValueList = mValueList + "," + mFieldValue;
                    }
                }
            }

            if (mFieldList.Trim() == "")
            {
                DateTime mTransDateTime =
                    mTransDate.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
                mFieldList = "changeuserid,changesysdate,changetransdate,changetransdatetime,auditchangetype";
                mValueList = "'" + mUserId.Trim() + "'," + Saving_DateValue(DateTime.Now)
                + "," + Saving_DateValue(mTransDate) + "," + Saving_DateValue(mTransDateTime) + ",'" + mChangeType + "'";
            }
            else
            {
                DateTime mTransDateTime =
                    mTransDate.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
                mFieldList = mFieldList + ",changeuserid,changesysdate,changetransdate,changetransdatetime,auditchangetype";
                mValueList = mValueList + ",'" + mUserId.Trim() + "'," + Saving_DateValue(DateTime.Now)
                + "," + Saving_DateValue(mTransDate) + "," + Saving_DateValue(mTransDateTime) + ",'" + mChangeType + "'";
            }

            string mFullSQL = "insert into " + gAfyaProAuditDbName + gDbNameTableNameSep + mTableName
            + "(" + mFieldList + ") values(" + mValueList + ")";
            return mFullSQL;
        }
        #endregion

        #region Synchronize_PaymentTypes
        private void Synchronize_PaymentTypes()
        {
            DataTable mDtColumns;
            string mAlterTableStr = "";
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            String mFunctionName = "Synchronize_PaymentTypes";

            try
            {
                mConn.ConnectionString = gAfyaConStr;
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }
                mCommand.Connection = mConn;

                DataTable mDtPaymentTypes = new DataTable("facilitypaymenttypes");
                mCommand.CommandText = "select * from facilitypaymenttypes";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtPaymentTypes);

                #region billcollections

                mCommand.CommandText = "select * from billcollections where 1=2";
                mDtColumns = new DataTable("columns");
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.FillSchema(mDtColumns, SchemaType.Source);

                mAlterTableStr = Build_AlterTableForPaymentTypes("billcollections", mDtColumns, mDtPaymentTypes);
                if (mAlterTableStr.Trim() != "")
                {
                    mCommand.CommandText = mAlterTableStr;
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region billreceipts

                mCommand.CommandText = "select * from billreceipts where 1=2";
                mDtColumns = new DataTable("columns");
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.FillSchema(mDtColumns, SchemaType.Source);

                mAlterTableStr = Build_AlterTableForPaymentTypes("billreceipts", mDtColumns, mDtPaymentTypes);
                if (mAlterTableStr.Trim() != "")
                {
                    mCommand.CommandText = mAlterTableStr;
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region billinvoicepayments

                mCommand.CommandText = "select * from billinvoicepayments where 1=2";
                mDtColumns = new DataTable("columns");
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.FillSchema(mDtColumns, SchemaType.Source);

                mAlterTableStr = Build_AlterTableForPaymentTypes("billinvoicepayments", mDtColumns, mDtPaymentTypes);
                if (mAlterTableStr.Trim() != "")
                {
                    mCommand.CommandText = mAlterTableStr;
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                //null values would create problems
                foreach (DataRow mDataRow in mDtPaymentTypes.Rows)
                {
                    string mFieldName = "paytype" + mDataRow["code"].ToString().Trim().ToLower();

                    //billcollections
                    mCommand.CommandText = "update billcollections set " + mFieldName + "=0 where " + mFieldName + " is null";
                    mCommand.ExecuteNonQuery();

                    //billreceipts
                    mCommand.CommandText = "update billreceipts set " + mFieldName + "=0 where " + mFieldName + " is null";
                    mCommand.ExecuteNonQuery();

                    //billinvoicepayments
                    mCommand.CommandText = "update billinvoicepayments set " + mFieldName + "=0 where " + mFieldName + " is null";
                    mCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Create_DynamicViews
        private void Create_DynamicViews()
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            String mFunctionName = "Create_DynamicViews";

            try
            {
                mConn.ConnectionString = gAfyaConStr;
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }
                mCommand.Connection = mConn;

                string mViewName = "";
                string mFieldsList = "";
                string mCommandText = "";
                DataTable mDtColumns;

                #region billcollections

                mDtColumns = new DataTable("tableschema");
                mCommand.CommandText = "select * from billcollections where 1=2";
                mDtColumns.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.FillSchema(mDtColumns, SchemaType.Source);

                mFieldsList = "";
                mViewName = "view_billcollections";
                foreach (DataColumn mDataColumn in mDtColumns.Columns)
                {
                    if (mFieldsList.Trim() == "")
                    {
                        mFieldsList = mDataColumn.ColumnName;
                    }
                    else
                    {
                        mFieldsList = mFieldsList + ", " + mDataColumn.ColumnName;
                    }
                }

                switch (gAfyaPro_ServerType)
                {
                    case 0:
                        {
                            mCommandText = "DROP VIEW IF EXISTS " + mViewName;
                            mCommand.CommandText = mCommandText;
                            mCommand.ExecuteNonQuery();
                        }
                        break;
                    case 1:
                        {
                            mCommandText = "IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = '" 
                                + mViewName + "') DROP VIEW " + mViewName;
                            mCommand.CommandText = mCommandText;
                            mCommand.ExecuteNonQuery();
                        }
                        break;
                }

                mCommandText = "CREATE VIEW " + mViewName + " AS SELECT "
                    + mFieldsList + " FROM billcollections";
                mCommand.CommandText = mCommandText;
                mCommand.ExecuteNonQuery();

                #endregion

                #region billreceipts

                mDtColumns = new DataTable("tableschema");
                mCommand.CommandText = "select * from billreceipts where 1=2";
                mDtColumns.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.FillSchema(mDtColumns, SchemaType.Source);

                mFieldsList = "";
                mViewName = "view_billreceipts";
                foreach (DataColumn mDataColumn in mDtColumns.Columns)
                {
                    if (mFieldsList.Trim() == "")
                    {
                        mFieldsList = mDataColumn.ColumnName;
                    }
                    else
                    {
                        mFieldsList = mFieldsList + ", " + mDataColumn.ColumnName;
                    }
                }

                switch (gAfyaPro_ServerType)
                {
                    case 0:
                        {
                            mCommandText = "DROP VIEW IF EXISTS " + mViewName;
                            mCommand.CommandText = mCommandText;
                            mCommand.ExecuteNonQuery();
                        }
                        break;
                    case 1:
                        {
                            mCommandText = "IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = '"
                                + mViewName + "') DROP VIEW " + mViewName;
                            mCommand.CommandText = mCommandText;
                            mCommand.ExecuteNonQuery();
                        }
                        break;
                }

                mCommandText = "CREATE VIEW " + mViewName + " AS SELECT "
                    + mFieldsList + " FROM billreceipts";
                mCommand.CommandText = mCommandText;
                mCommand.ExecuteNonQuery();

                #endregion

                #region billinvoicepayments

                mDtColumns = new DataTable("tableschema");
                mCommand.CommandText = "select * from billinvoicepayments where 1=2";
                mDtColumns.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.FillSchema(mDtColumns, SchemaType.Source);

                mFieldsList = "";
                mViewName = "view_billinvoicepayments";
                foreach (DataColumn mDataColumn in mDtColumns.Columns)
                {
                    if (mFieldsList.Trim() == "")
                    {
                        mFieldsList = mDataColumn.ColumnName;
                    }
                    else
                    {
                        mFieldsList = mFieldsList + ", " + mDataColumn.ColumnName;
                    }
                }

                switch (gAfyaPro_ServerType)
                {
                    case 0:
                        {
                            mCommandText = "DROP VIEW IF EXISTS " + mViewName;
                            mCommand.CommandText = mCommandText;
                            mCommand.ExecuteNonQuery();
                        }
                        break;
                    case 1:
                        {
                            mCommandText = "IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = '"
                                + mViewName + "') DROP VIEW " + mViewName;
                            mCommand.CommandText = mCommandText;
                            mCommand.ExecuteNonQuery();
                        }
                        break;
                }

                mCommandText = "CREATE VIEW " + mViewName + " AS SELECT "
                    + mFieldsList + " FROM billinvoicepayments";
                mCommand.CommandText = mCommandText;
                mCommand.ExecuteNonQuery();

                #endregion
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Synchronize_AuditTable
        internal static void Synchronize_AuditTable(string mTableName)
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            String mFunctionName = "Synchronize_AuditTable";

            try
            {
                mConn.ConnectionString = gAfyaConStr;
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }
                mCommand.Connection = mConn;

                #region create destinationtable if not exist
                if (gAfyaPro_ServerType == 0)
                {
                    try
                    {
                        mCommand.CommandText = "create table " + gAfyaProAuditDbName + gDbNameTableNameSep + mTableName
                        + " (autocode integer(11) not null auto_increment,"
                        + "changeuserid varchar(50) default '',"
                        + "changesysdate datetime,"
                        + "changetransdate datetime,"
                        + "changetransdatetime datetime,"
                        + "auditchangetype varchar(50) default '',"
                        + "primary key (autocode)) ENGINE=InnoDB DEFAULT CHARSET=latin1";
                        mCommand.ExecuteNonQuery();
                    }
                    catch { }
                }
                if (gAfyaPro_ServerType == 1)
                {
                    try
                    {
                        mCommand.CommandText = "create table " + gAfyaProAuditDbName + gDbNameTableNameSep + mTableName
                        + " (autocode int identity, primary key (autocode))";
                        mCommand.ExecuteNonQuery();
                        mCommand.CommandText = "create table " + gAfyaProAuditDbName + gDbNameTableNameSep + mTableName
                        + " (autocode int identity,"
                        + "changeuserid varchar(50) default '',"
                        + "changesysdate datetime,"
                        + "changetransdate datetime,"
                        + "changetransdatetime datetime,"
                        + "auditchangetype varchar(50) default '',"
                        + "primary key (autocode))";
                        mCommand.ExecuteNonQuery();
                    }
                    catch { }
                }
                #endregion

                DataTable mDtSourceTable = new DataTable("sourcetable");
                DataTable mDtDestinationTable = new DataTable("destinationtable");
                String[] mRestrictions = new String[3];

                #region get source table
                mRestrictions[0] = gAfyaProDbName;
                mRestrictions[1] = null;
                mRestrictions[2] = mTableName;

                mDtSourceTable = mConn.GetSchema("Columns", mRestrictions);
                #endregion

                #region get destination table
                mRestrictions[0] = gAfyaProAuditDbName;
                mRestrictions[1] = null;
                mRestrictions[2] = mTableName;

                mDtDestinationTable = mConn.GetSchema("Columns", mRestrictions);
                DataView mDvDestinationTable = new DataView();
                mDvDestinationTable.Table = mDtDestinationTable;
                mDvDestinationTable.Sort = "COLUMN_NAME";
                #endregion

                string mTableSql = "ALTER TABLE " + gAfyaProAuditDbName + gDbNameTableNameSep + mTableName + " ";
                string mUpdateStatements = "";
                bool mFieldFound = false;
                bool mFirstField = true;

                foreach (DataRow mDataRow in mDtSourceTable.Rows)
                {
                    String mFieldName = mDataRow["COLUMN_NAME"].ToString();

                    if (mDvDestinationTable.Find(mFieldName) < 0)
                    {
                        Int16 mColumnType = Convert.ToInt16(mDataRow["DATA_TYPE"]);
                        Int32 mFieldLength = Convert.ToInt32(mDataRow["COLUMN_SIZE"]);
                        Int16 mNullable = Convert.ToInt16(mDataRow["NULLABLE"]);
                        string mDefaultValue = "";

                        String mDataType = "";
                        String mColumnStr = "";
                        String mAllowNull = "";
                        String mIdentity = "";

                        #region MySql Server

                        if (gAfyaPro_ServerType == 0)
                        {
                            switch (mColumnType)
                            {
                                case 12://varchar
                                    {
                                        mDataType = " VARCHAR(" + mFieldLength.ToString() + ") ";
                                        mDefaultValue = "DEFAULT ''";
                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "NOT NULL ";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull + mDefaultValue;
                                    }
                                    break;

                                case 4://integer
                                    {
                                        mDataType = " INTEGER(11) ";
                                        if (mFieldName.ToLower() == "autocode")
                                        {
                                            mDefaultValue = "";
                                            mIdentity = "AUTO_INCREMENT ";
                                        }
                                        else
                                        {
                                            mDefaultValue = "DEFAULT '0'";
                                        }

                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "NOT NULL ";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull + mIdentity + mDefaultValue;
                                    }
                                    break;

                                case 93://datetime
                                    {
                                        mDataType = " DATETIME ";

                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "NOT NULL ";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull;
                                    }
                                    break;

                                case 3://decimal
                                    {
                                        mDataType = " DECIMAL(16,2) ";
                                        mDefaultValue = "DEFAULT '0.00'";

                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "NOT NULL ";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull + mDefaultValue;
                                    }
                                    break;

                                case 8://double
                                    {
                                        mDataType = " DOUBLE(16,2) ";
                                        mDefaultValue = "DEFAULT '0.00'";

                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "NOT NULL ";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull + mDefaultValue;
                                    }
                                    break;

                                case 2://numeric
                                    {
                                        mDataType = " NUMERIC(16,2) ";
                                        mDefaultValue = "DEFAULT '0.00'";

                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "NOT NULL ";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull + mDefaultValue;
                                    }
                                    break;

                                case -4://blob
                                    {
                                        mDataType = " LONGBLOB ";
                                        mDefaultValue = "";

                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull + mDefaultValue;
                                    }
                                    break;

                                default:
                                    {
                                        mColumnStr = mColumnType.ToString();
                                    }
                                    break;
                            }
                        }

                        #endregion

                        #region MS SQL Server

                        if (gAfyaPro_ServerType == 1)
                        {
                            switch (mColumnType)
                            {
                                case 12://varchar
                                    {
                                        mDataType = " VARCHAR(" + mFieldLength.ToString() + ") ";
                                        mDefaultValue = "DEFAULT ''";

                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "NOT NULL ";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull + mDefaultValue;
                                    }
                                    break;

                                case 4://integer
                                    {
                                        mDataType = " INT ";
                                        if (mFieldName.ToLower() == "autocode")
                                        {
                                            mDefaultValue = "";
                                            mIdentity = "IDENTITY ";
                                        }
                                        else
                                        {
                                            mDefaultValue = "DEFAULT '0'";
                                        }

                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "NOT NULL ";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull + mIdentity + mDefaultValue;
                                        mUpdateStatements = mUpdateStatements + "UPDATE " + mTableName + " SET " + mFieldName
                                            + "=0 WHERE " + mFieldName + " IS NULL;";
                                    }
                                    break;

                                case 93://datetime
                                    {
                                        mDataType = " DATETIME ";

                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "NOT NULL ";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull;
                                    }
                                    break;

                                case 3://decimal
                                    {
                                        mDataType = " DECIMAL(16,2) ";
                                        mDefaultValue = "DEFAULT '0.00'";

                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "NOT NULL ";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull + mDefaultValue;
                                        mUpdateStatements = mUpdateStatements + "UPDATE " + mTableName + " SET " + mFieldName
                                            + "=0 WHERE " + mFieldName + " IS NULL;";
                                    }
                                    break;

                                case 8://double
                                    {
                                        mDataType = " DECIMAL(16,2) ";
                                        mDefaultValue = "DEFAULT '0.00'";

                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "NOT NULL ";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull + mDefaultValue;
                                        mUpdateStatements = mUpdateStatements + "UPDATE " + mTableName + " SET " + mFieldName
                                            + "=0 WHERE " + mFieldName + " IS NULL;";
                                    }
                                    break;

                                case 2://numeric
                                    {
                                        mDataType = " NUMERIC(16,2) ";
                                        mDefaultValue = "DEFAULT '0.00'";

                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "NOT NULL ";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull + mDefaultValue;
                                        mUpdateStatements = mUpdateStatements + "UPDATE " + mTableName + " SET " + mFieldName
                                            + "=0 WHERE " + mFieldName + " IS NULL;";
                                    }
                                    break;

                                case -4://blob
                                    {
                                        mDataType = " IMAGE ";
                                        mDefaultValue = "";

                                        if (mNullable == 0)
                                        {
                                            mAllowNull = "";
                                        }

                                        mColumnStr = mFieldName + mDataType + mAllowNull + mDefaultValue;
                                    }
                                    break;

                                default:
                                    {
                                        mColumnStr = mColumnType.ToString();
                                    }
                                    break;
                            }
                        }

                        #endregion

                        if (mFirstField == true)
                        {
                            mTableSql += "ADD " + mColumnStr;
                            mFirstField = false;
                        }
                        else
                        {
                            mTableSql += "," + Environment.NewLine + "ADD " + mColumnStr;
                        }

                        mFieldFound = true;
                    }
                }

                if (mFieldFound == true)
                {
                    mCommand.CommandText = mTableSql;
                    mCommand.ExecuteNonQuery();

                    if (mUpdateStatements.Trim() != "")
                    {
                        mCommand.CommandText = mUpdateStatements;
                        mCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Build_AlterTableForPaymentTypes
        internal static string Build_AlterTableForPaymentTypes(string mTableName, DataTable mDtColumns, DataTable mDtPaymentTypes)
        {
            bool mColumnFound = false;

            string mAlterTableStr = "";

            foreach (DataRow mDataRow in mDtPaymentTypes.Rows)
            {
                string mFieldName = "paytype" + mDataRow["code"].ToString().Trim().ToLower();

                mColumnFound = false;
                foreach (DataColumn mDataColumn in mDtColumns.Columns)
                {
                    if (mFieldName == mDataColumn.ColumnName.Trim().ToLower())
                    {
                        mColumnFound = true;
                        break;
                    }
                }

                if (mColumnFound == false)
                {
                    if (mAlterTableStr.Trim() == "")
                    {
                        switch (gAfyaPro_ServerType)
                        {
                            case 0:
                                {
                                    mAlterTableStr = "ALTER TABLE " + mTableName + " ADD " + mFieldName + " DOUBLE(16,2) DEFAULT '0.00'";
                                }
                                break;
                            case 1:
                                {
                                    mAlterTableStr = "ALTER TABLE " + mTableName + " ADD " + mFieldName + " DECIMAL(16,2) DEFAULT '0.00'";
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (gAfyaPro_ServerType)
                        {
                            case 0:
                                {
                                    mAlterTableStr = mAlterTableStr + ", ADD " + mFieldName + " DOUBLE(16,2) DEFAULT '0.00'";
                                }
                                break;
                            case 1:
                                {
                                    mAlterTableStr = mAlterTableStr + ", ADD " + mFieldName + " DECIMAL(16,2) DEFAULT '0.00'";
                                }
                                break;
                        }
                    }
                }
            }

            return mAlterTableStr;
        }
        #endregion

        #region Build_AlterTableForPaymentTypes
        internal static string Build_AlterTableForPaymentTypes(string mTableName, string mPaymentTypeCode, DataTable mDtColumns)
        {
            bool mColumnFound = false;

            string mAlterTableStr = "";

            string mFieldName = "paytype" + mPaymentTypeCode.Trim().ToLower();

            mColumnFound = false;
            foreach (DataColumn mDataColumn in mDtColumns.Columns)
            {
                if (mFieldName == mDataColumn.ColumnName.Trim().ToLower())
                {
                    mColumnFound = true;
                    break;
                }
            }

            if (mColumnFound == false)
            {
                switch (gAfyaPro_ServerType)
                {
                    case 0:
                        {
                            mAlterTableStr = "ALTER TABLE " + mTableName + " ADD " + mFieldName + " DOUBLE(16,2) DEFAULT '0.00'";
                        }
                        break;
                    case 1:
                        {
                            mAlterTableStr = "ALTER TABLE " + mTableName + " ADD " + mFieldName + " DECIMAL(16,2) DEFAULT '0.00'";
                        }
                        break;
                }
            }

            return mAlterTableStr;
        }
        #endregion

        #region Synchronize_Currencies
        private void Synchronize_Currencies()
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            String mFunctionName = "Synchronize_Currencies";

            try
            {
                mConn.ConnectionString = gAfyaConStr;
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }
                mCommand.Connection = mConn;

                DataTable mDtCurrencies = new DataTable("facilitycurrencies");
                mCommand.CommandText = "select * from facilitycurrencies where code='localcurr'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtCurrencies);

                if (mDtCurrencies.Rows.Count == 0)
                {
                    mCommand.CommandText = "insert into facilitycurrencies(code, description,"
                    + "exchangerate,currencysymbol) values('LocalCurr','USD',1,'$')";
                    mCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Synchronize_PriceCategories
        private void Synchronize_PriceCategories()
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            String mFunctionName = "Synchronize_PriceCategories";

            try
            {
                mConn.ConnectionString = gAfyaConStr;
                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }
                mCommand.Connection = mConn;

                DataTable mDtPriceCategories = new DataTable("facilitypricecategories");
                mCommand.CommandText = "select * from facilitypricecategories";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtPriceCategories);

                if (mDtPriceCategories.Rows.Count == 0)
                {
                    mCommand.CommandText = "insert into facilitypricecategories(useprice1, price1) values(1,'Price')";
                    mCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Write_Error
        public static String Write_Error(String mClassName, String mFunctionName, String mError)
        {

            String mErrorMessage = "";

            //create log if does not exist
            String mLogSource = "AfyaPro";
            String mLogName = "AfyaPro";
            if (EventLog.SourceExists(mLogSource) == false)
            {
                EventLog.CreateEventSource(mLogSource, mLogName);
            }

            // Create an EventLog instance and assign its source.
            pEventLog = new EventLog();
            pEventLog.Source = mLogSource;

            //buit error message
            mErrorMessage = "Error occured in the following module" + System.Environment.NewLine
            + "Class Name: " + mClassName + System.Environment.NewLine
            + "Function Name: " + mFunctionName + System.Environment.NewLine
            + "Error Message: " + mError;

            pEventLog.WriteEntry(mErrorMessage, EventLogEntryType.Error);

            return mErrorMessage;

        }
        #endregion

        #region Saving_DateValue
        public static String Saving_DateValue(DateTime mDateValue)
        {
            String mDateString = "";

            switch (gAfyaPro_ServerType)
            {
                case 0: mDateString = mDateValue.ToString("yyyy-MM-dd HH:mm:ss"); break;
                case 1: mDateString = mDateValue.ToString("MM-dd-yyyy HH:mm:ss"); break;
            }

            mDateString = gDbDateQuote + mDateString + gDbDateQuote;

            return mDateString;
        }
        #endregion

        #region Saving_DateValueNullable
        public static String Saving_DateValueNullable(object mDateValue)
        {
            String mDateString = "";
            DateTime mNullDate = new DateTime(1900, 1, 1);

            if (mDateValue == null)
            {
                return "Null";
            }

            DateTime mDateTime = new DateTime();

            try
            {
                mDateTime = Convert.ToDateTime(mDateValue);
            }
            catch 
            {
            }

            if (mDateTime <= mNullDate)
            {
                mDateString = "Null";
            }
            else
            {
                switch (gAfyaPro_ServerType)
                {
                    case 0: mDateString = mDateTime.ToString("yyyy-MM-dd HH:mm:ss"); break;
                    case 1: mDateString = mDateTime.ToString("MM-dd-yyyy HH:mm:ss"); break;
                }
                
                mDateString = gDbDateQuote + mDateString + gDbDateQuote;
            }

            return mDateString;
        }
        #endregion

        #region DateValueNullable
        public static DateTime DateValueNullable(object mDateValue)
        {
            DateTime mDateTime = new DateTime();

            try
            {
                mDateTime = Convert.ToDateTime(mDateValue);
            }
            catch
            {
            }

            return mDateTime;
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

        #region GrantDeny_FunctionAccess
        internal static bool GrantDeny_FunctionAccess(OdbcCommand mCommand, string mFunctionAccessKey, string mUserId)
        {
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            try
            {
                DataTable mDtUsers = new DataTable("users");
                mCommand.CommandText = "select usergroupcode from sys_users where code='" + mUserId.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtUsers);

                string mUserGroupCode = "";
                if (mDtUsers.Rows.Count > 0)
                {
                    mUserGroupCode = mDtUsers.Rows[0]["usergroupcode"].ToString().Trim();
                }

                if (mUserGroupCode.Trim() == "")
                {
                    return false;
                }

                DataTable mDtFunctions = new DataTable("functions");
                mCommand.CommandText = "select * from sys_usergroupfunctions where usergroupcode='"
                + mUserGroupCode.Trim() + "' and functionaccesskey='" + mFunctionAccessKey.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFunctions);

                if (mDtFunctions.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region RandomString
        /// <summary>
        /// Generates a random string with the given length
        /// </summary>
        /// <param name="size">Size of the string</param>
        /// <param name="lowerCase">If true, generate lowercase string</param>
        /// <returns>Random string</returns>
        internal static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        #endregion

        #region Generate_AutoId
        internal static string Generate_AutoId()
        {
            int mLowerBound = 100000;
            int mUpperBound = 999999;

            VBMath.Randomize();
            int mRandomNumber = (int)((int)((mUpperBound - mLowerBound + 1) * VBMath.Rnd() + mLowerBound));

            string mTimeStamp = "" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +
                    DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;

            return mRandomNumber + mTimeStamp;
        }
        #endregion

        #region Get_InsertStatement
        internal static string Get_InsertStatement(string mTableName, List<clsDataField> mDataFields)
        {
            string mFieldNames = "";
            string mFieldValues = "";

            foreach (clsDataField mDataField in mDataFields)
            {
                string mFieldValue = "Null";

                //build value depending on the type of field
                if (mDataField.FieldType.Trim().ToLower() == DataTypes.dbstring.ToString().ToLower())
                {
                    if (mDataField.FieldValue != null)
                    {
                        mFieldValue = "'" + mDataField.FieldValue.ToString().Trim() + "'";
                    }
                }
                else if (mDataField.FieldType.Trim().ToLower() == DataTypes.dbdatetime.ToString().ToLower())
                {
                    if (mDataField.FieldValue != null)
                    {
                        mFieldValue = Saving_DateValueNullable(mDataField.FieldValue);
                    }
                }
                else if (mDataField.FieldType.Trim().ToLower() == DataTypes.dbdecimal.ToString().ToLower())
                {
                    if (mDataField.FieldValue != null)
                    {
                        mFieldValue = mDataField.FieldValue.ToString();
                    }
                }
                else if (mDataField.FieldType.Trim().ToLower() == DataTypes.dbnumber.ToString().ToLower())
                {
                    if (mDataField.FieldValue != null)
                    {
                        mFieldValue = mDataField.FieldValue.ToString();
                    }
                }

                //build fieldnames and fieldvalues
                if (mFieldNames.Trim() == "")
                {
                    mFieldNames = mDataField.FieldName;
                    mFieldValues = mFieldValue;
                }
                else
                {
                    mFieldNames += "," + mDataField.FieldName;
                    mFieldValues += "," + mFieldValue;
                }
            }

            return "INSERT INTO " + mTableName + "(" + mFieldNames + ") values(" + mFieldValues + ")";
        }
        #endregion

        #region Get_UpdateStatement
        internal static string Get_UpdateStatement(string mTableName, List<clsDataField> mDataFields, string mCondition)
        {
            string mUpdateFields = "";

            foreach (clsDataField mDataField in mDataFields)
            {
                string mFieldValue = "Null";

                //build value depending on the type of field
                if (mDataField.FieldType.Trim().ToLower() == DataTypes.dbstring.ToString().ToLower())
                {
                    if (mDataField.FieldValue != null)
                    {
                        mFieldValue = "'" + mDataField.FieldValue.ToString().Trim() + "'";
                    }
                }
                else if (mDataField.FieldType.Trim().ToLower() == DataTypes.dbdatetime.ToString().ToLower())
                {
                    if (mDataField.FieldValue != null)
                    {
                        mFieldValue = Saving_DateValueNullable(mDataField.FieldValue);
                    }
                }
                else if (mDataField.FieldType.Trim().ToLower() == DataTypes.dbdecimal.ToString().ToLower())
                {
                    if (mDataField.FieldValue != null)
                    {
                        mFieldValue = mDataField.FieldValue.ToString();
                    }
                }
                else if (mDataField.FieldType.Trim().ToLower() == DataTypes.dbnumber.ToString().ToLower())
                {
                    if (mDataField.FieldValue != null)
                    {
                        mFieldValue = mDataField.FieldValue.ToString();
                    }
                }

                //build fieldnames and fieldvalues
                if (mUpdateFields.Trim() == "")
                {
                    mUpdateFields = mDataField.FieldName + "=" + mFieldValue;
                }
                else
                {
                    mUpdateFields += "," + mDataField.FieldName + "=" + mFieldValue;
                }
            }

            return "UPDATE " + mTableName + " SET " + mUpdateFields + " WHERE " + mCondition;
        }
        #endregion

        #region Get_DeleteStatement
        internal static string Get_DeleteStatement(string mTableName, string mCondition)
        {
            return "DELETE FROM " + mTableName + " WHERE " + mCondition;
        }
        #endregion

        #region Get_DeleteStatement
        internal static string Get_DeleteStatement(string mTableName)
        {
            return "DELETE FROM " + mTableName;
        }
        #endregion

        #region Get_StockDate
        internal static object Get_StockDate(OdbcCommand mCommand)
        {
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            try
            {
                switch (gAfyaPro_ServerType)
                {
                    case 0:
                        {
                            mCommand.CommandText = "SELECT transdate FROM som_physicalstockbalances LIMIT 1";
                        }
                        break;
                    case 1:
                        {
                            mCommand.CommandText = "SELECT TOP 1 transdate FROM som_physicalstockbalances";
                        }
                        break;
                }

                DataTable mDtStockBalances = new DataTable("stockbalances");
                if (mCommand.CommandText.Trim() != "")
                {
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtStockBalances);
                }

                if (mDtStockBalances.Rows.Count > 0)
                {
                    return Convert.ToDateTime(mDtStockBalances.Rows[0]["transdate"]);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Get_TableColumns

        internal static string Get_TableColumns(OdbcCommand mCommand, string mTableName, string mExcludedColumns, string mTableAlias, string mColumnAliasPrefix)
        {
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            string[] mColumnNames = mExcludedColumns.ToLower().Split(',');

            string mSelectedColumns = "";
            DataTable mDtColumns = new DataTable(mTableName);
            mCommand.CommandText = "select * from " + mTableName + " where 1=2";
            mDataAdapter.SelectCommand = mCommand;
            mDataAdapter.Fill(mDtColumns);
            foreach (DataColumn mDataColumn in mDtColumns.Columns)
            {
                bool mFound = false;
                foreach (string mColumnName in mColumnNames)
                {
                    if (mColumnName.ToLower().Trim() == mDataColumn.ColumnName.ToLower().Trim())
                    {
                        mFound = true;
                        break;
                    }
                }

                if (mFound == false)
                {
                    if (mSelectedColumns.Trim() == "")
                    {
                        mSelectedColumns = mTableAlias + "." + mDataColumn.ColumnName + " AS " + mColumnAliasPrefix + mDataColumn.ColumnName;
                    }
                    else
                    {
                        mSelectedColumns = mSelectedColumns + "," + mTableAlias + "." + mDataColumn.ColumnName + " AS " + mColumnAliasPrefix + mDataColumn.ColumnName;
                    }
                }
            }

            return mSelectedColumns;
        }

        #endregion

        #region Get_ProductColumns

        internal static string Get_ProductColumns(OdbcCommand mCommand, string mTableName, string mExcludedColumns, string mTableAlias, string mColumnAliasPrefix)
        {
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            string[] mColumnNames = mExcludedColumns.ToLower().Split(',');

            string mSelectedColumns = "";
            DataTable mDtColumns = new DataTable(mTableName);
            mCommand.CommandText = "select * from " + mTableName + " where 1=2";
            mDataAdapter.SelectCommand = mCommand;
            mDataAdapter.Fill(mDtColumns);
            foreach (DataColumn mDataColumn in mDtColumns.Columns)
            {
                bool mFound = true;
                if (mDataColumn.ColumnName.ToLower().StartsWith("visible_") == false)
                {
                    mFound = false;
                    foreach (string mColumnName in mColumnNames)
                    {
                        if (mColumnName.ToLower().Trim() == mDataColumn.ColumnName.ToLower().Trim())
                        {
                            mFound = true;
                            break;
                        }
                    }
                }

                if (mFound == false)
                {
                    if (mSelectedColumns.Trim() == "")
                    {
                        mSelectedColumns = mTableAlias + "." + mDataColumn.ColumnName + " AS " + mColumnAliasPrefix + mDataColumn.ColumnName;
                    }
                    else
                    {
                        mSelectedColumns = mSelectedColumns + "," + mTableAlias + "." + mDataColumn.ColumnName + " AS " + mColumnAliasPrefix + mDataColumn.ColumnName;
                    }
                }
            }

            return mSelectedColumns;
        }

        #endregion

        #region Scan_ColumnsForLanguage
        internal static void Scan_ColumnsForLanguage(OdbcCommand mCommand, DataTable mDataTable, string mGridName)
        {
            if (mGridName.Trim() == "")
            {
                return;
            }

            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            DataTable mDtColumns = new DataTable("columns");
            mCommand.CommandText = "select * from sys_langcolumnheaders where gridname='" + mGridName + "'";
            mDataAdapter.SelectCommand = mCommand;
            mDataAdapter.Fill(mDtColumns);

            //interprete
            foreach (DataRow mDataRow in mDtColumns.Rows)
            {
                if (mDataTable.Columns.Contains(mDataRow["columnname"].ToString().Trim()) == true)
                {
                    mDataTable.Columns[mDataRow["columnname"].ToString().Trim()].Caption = mDataRow["english"].ToString().Trim();
                }
            }

            //add missing columns
            DataView mDvColumns = new DataView();
            mDvColumns.Table = mDtColumns;
            mDvColumns.Sort = "columnname";

            foreach (DataColumn mDataColumn in mDataTable.Columns)
            {
                if (mDvColumns.Find(mDataColumn.ColumnName) < 0)
                {
                    List<clsDataField> mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("gridname", DataTypes.dbstring.ToString(), mGridName));
                    mDataFields.Add(new clsDataField("columnname", DataTypes.dbstring.ToString(), mDataColumn.ColumnName));
                    mDataFields.Add(new clsDataField("english", DataTypes.dbstring.ToString(), mDataColumn.ColumnName));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("sys_langcolumnheaders", mDataFields);
                    mCommand.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region Get_MonthName
        internal static string Get_MonthName(int mMonth)
        {
            String mFunctionName = "Get_MonthName";
            string monthName = "";

            monthName = Enum.GetName(typeof(AfyaPro_Types.clsEnums.MONTH), mMonth);

            return monthName;
        }
        #endregion

        #region Get_QuatrerStartMonth
        internal static int Get_QuatrerStartMonth(string mstrQuarter)
        {
            String mFunctionName = "Get_QuatrerStartMonth";
            try
            {
                AfyaPro_Types.clsEnums.QUARTER mQuarter = (AfyaPro_Types.clsEnums.QUARTER)Enum.Parse(typeof(AfyaPro_Types.clsEnums.QUARTER), mstrQuarter);

                int startMonth = (int)mQuarter;

                return startMonth;
            }
            catch (ArgumentException e)
            {
                Write_Error(pClassName, mFunctionName,e.Message);
                return 0;
            }
        }
        #endregion

    }
  }
