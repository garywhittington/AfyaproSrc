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
using System.Data.Odbc;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace AfyaPro_ServerAdmin
{
    class Program
    {
        #region declaration

        private static EventLog pEventLog;

        internal static string pIniFileName;
        internal static Int16 gMiddleTierPort = 1416;
        internal static String gPicturesFolderPath = "";
        internal static Int16 gServerType = 1;
        internal static String gServerName = "";
        internal static String gServerPort = "";
        internal static Int16 gAuthentication = 0;
        internal static String gUserName = "";
        internal static String gPassword = "";
        internal static String gDatabaseName = "";

        internal static OdbcConnection gConn = new OdbcConnection();
        internal static OdbcDataAdapter gDataAdapter = new OdbcDataAdapter();
        internal static OdbcTransaction gTrans = null;
        internal static OdbcCommand gCommand = new OdbcCommand();
        internal static String gConnectionString = "";
        internal static String gDbDateQuote = "";
        internal static String gSavingDateFormat = "";

        private static string pClassName = "AfyaPro_ServerAdmin.Program";

        public static string gCryptoKey = "123789";
        internal static DevExpress.LookAndFeel.DefaultLookAndFeel gDefaultLookAndFeel;

        #endregion

        #region Program
        public Program()
        {
            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.UserSkins.BonusSkins.Register();
            gDefaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            gDefaultLookAndFeel.LookAndFeel.SkinName = "Office 2007 Green";
            gDefaultLookAndFeel.LookAndFeel.UseDefaultLookAndFeel = false;

            //config file path
            pIniFileName = System.Environment.SystemDirectory + @"/afyaproserver.xml";

            if (File.Exists(pIniFileName) == false)
            {
                frmInitialSetup mInitialSetup = new frmInitialSetup();
                mInitialSetup.ShowDialog();
            }
            else
            {
                ReadIni();
            }

            gConnectionString = Get_ConnString(gDatabaseName);
            gConn = new OdbcConnection();
            gConn.ConnectionString = gConnectionString;

            frmMain mMain = new frmMain();
            mMain.ShowDialog();
        }
        #endregion

        #region Main
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            new Program();
        }
        #endregion

        #region Create_ConnectionString
        internal static String Create_ConnectionString(String mDatabaseName)
        {
            String mServerDriver = "";

            String mFunctionName = "Create_ConnectionString";

            try
            {
                if (Program.gConn.State != ConnectionState.Closed)
                {
                    Program.gConn.Close();
                }

                //determine server type
                switch (gServerType)
                {
                    case 0:
                        {
                            mServerDriver = "MySQL ODBC 5.1 Driver";
                            Program.gDbDateQuote = "'";
                            gSavingDateFormat = "yyyy-MM-dd";

                            //connectionstring
                            Program.gConnectionString = "DRIVER={" + mServerDriver
                                + "};SERVER=" + gServerName
                                + ";UID=" + gUserName
                                + ";PWD=" + gPassword
                                + ";DATABASE=" + mDatabaseName;
                        }
                        break;
                    case 1:
                        {
                            mServerDriver = "SQL Server";
                            Program.gDbDateQuote = "'";
                            gSavingDateFormat = "MM-dd-yyyy";

                            //connectionstring
                            if (gAuthentication == 0)
                            {
                                Program.gConnectionString = "DRIVER={" + mServerDriver
                                    + "};SERVER=" + gServerName
                                    + ";Trusted_Connection=yes"
                                    + ";DATABASE=" + mDatabaseName;
                            }
                            else
                            {
                                Program.gConnectionString = "DRIVER={" + mServerDriver
                                   + "};SERVER=" + gServerName
                                   + ";UID=" + gUserName
                                   + ";PWD=" + gPassword
                                   + ";DATABASE=" + mDatabaseName;
                            }
                        }
                        break;
                }

                Program.gConn.ConnectionString = Program.gConnectionString;
                return Program.gConnectionString;
            }
            catch (Exception ex)
            {
                Program.Write_Error(pClassName, mFunctionName, ex.Message);
                return "";
            }
        }
        #endregion

        #region Get_ConnString
        internal static string Get_ConnString(string mDatabaseName)
        {
            string mConnStr = "";
            string mServerDriver = "";

            string mFunctionName = "Get_ConnString";

            try
            {
                //determine server type
                switch (gServerType)
                {
                    case 0:
                        {
                            mServerDriver = "MySQL ODBC 5.1 Driver";
                            Program.gDbDateQuote = "'";
                            gSavingDateFormat = "yyyy-MM-dd";

                            //mConnStr
                            mConnStr = "DRIVER={" + mServerDriver
                                + "};SERVER=" + gServerName
                                + ";UID=" + gUserName
                                + ";PWD=" + gPassword
                                + ";DATABASE=" + mDatabaseName;
                        }
                        break;
                    case 1:
                        {
                            mServerDriver = "SQL Server";
                            Program.gDbDateQuote = "'";
                            gSavingDateFormat = "MM-dd-yyyy";

                            //mConnStr
                            if (gAuthentication == 0)
                            {
                                mConnStr = "DRIVER={" + mServerDriver
                                    + "};SERVER=" + gServerName
                                    + ";Trusted_Connection=yes"
                                    + ";DATABASE=" + mDatabaseName;
                            }
                            else
                            {
                                mConnStr = "DRIVER={" + mServerDriver
                                   + "};SERVER=" + gServerName
                                   + ";UID=" + gUserName
                                   + ";PWD=" + gPassword
                                   + ";DATABASE=" + mDatabaseName;
                            }
                        }
                        break;
                }

                return mConnStr;
            }
            catch (Exception ex)
            {
                Program.Write_Error(pClassName, mFunctionName, ex.Message);
                return "";
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
                mFileSystemIn = new FileStream(Program.pIniFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                mStreamReader = new StreamReader(mFileSystemIn);
                mXmlTextReader = new XmlTextReader(mStreamReader);

                while (mXmlTextReader.Read())
                {
                    //middle tier port
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "middletierport")
                    {
                        gMiddleTierPort = System.Convert.ToInt16(mXmlTextReader.ReadElementString("middletierport"));
                    }

                    //server type
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "afyapro_servertype")
                    {
                        try
                        {
                            gServerType = Convert.ToInt16(mXmlTextReader.ReadElementString("afyapro_servertype"));
                        }
                        catch { }
                    }

                    //servername
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "afyapro_servername")
                    {
                        gServerName = mXmlTextReader.ReadElementString("afyapro_servername");
                    }

                    //serverport
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "afyapro_serverport")
                    {
                        gServerPort = mXmlTextReader.ReadElementString("afyapro_serverport");
                    }

                    //authentication
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "afyapro_authentication")
                    {
                        gAuthentication = Convert.ToInt16(mXmlTextReader.ReadElementString("afyapro_authentication"));
                    }

                    //serveruser
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "afyapro_serveruser")
                    {
                        ////decrypt password
                        //clsCryptoService mCryptoService = new clsCryptoService(clsCryptoService.SymmProvEnum.Rijndael);
                        string mPassword = mXmlTextReader.ReadElementString("afyapro_serveruser");
                        //mPassword = mCryptoService.Decrypt(mPassword, gCryptoKey);

                        gUserName = mPassword;
                    }

                    //serverpassword
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "afyapro_serverpassword")
                    {
                        gPassword = mXmlTextReader.ReadElementString("afyapro_serverpassword");
                    }

                    //databasename
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "afyapro_databasename")
                    {
                        gDatabaseName = mXmlTextReader.ReadElementString("afyapro_databasename");
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

        #region Display_Error
        public static void Display_Error(String mMessage)
        {
            DevExpress.XtraEditors.XtraMessageBox.Show(mMessage, 
                "AfyaPro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        #region Display_Error
        public static void Display_Error(String mClassName, String mFunctionName, String mMessage)
        {
            DevExpress.XtraEditors.XtraMessageBox.Show(
            "Error occured" + System.Environment.NewLine
            + "CLASS: " + mClassName + System.Environment.NewLine
            + "FUNCTION: " + mFunctionName + System.Environment.NewLine
            + "ERROR_MESSAGE: " + mMessage + System.Environment.NewLine,
            "AfyaPro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        #region Display_Question
        public static DialogResult Display_Question(String mMessage)
        {
            DialogResult mResult;
            mResult = DevExpress.XtraEditors.XtraMessageBox.Show(
                mMessage + "?", "AfyaPro", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            return mResult;

        }
        #endregion

        #region Display_Info
        public static void Display_Info(String mMessage)
        {
            DevExpress.XtraEditors.XtraMessageBox.Show(
                mMessage, "AfyaPro", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        //**********************************************************
        #region IsMoney
        public static Boolean IsMoney(String mValue)
        {
            Boolean mValid = true;

            if (System.Text.RegularExpressions.Regex.IsMatch(mValue,
                @"^\$?(?:\d+|\d{1,3}(?:,\d{3})*)(?:\.\d{1,2}){0,1}$") == false)
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

        #region IsNumeric
        public static Boolean IsNumeric(char mValue)
        {
            Boolean mValid = false;
            if (char.IsDigit(mValue) || char.IsControl(mValue))
            {
                mValid = true;
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
        public static Boolean IsDate(String mValue)
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

        #region DateValueNullable
        public static String DateValueNullable(Object mDateValue)
        {
            String mDateString = "";

            try
            {
                DateTime mDate = Convert.ToDateTime(mDateValue);
                mDateString = mDate.ToString(gSavingDateFormat);
                mDateString = gDbDateQuote + mDateString + gDbDateQuote;
            }
            catch
            {
                mDateString = "Null";
            }

            return mDateString;
        }
        #endregion

        #region DbValue_ToInt16
        public static Int16 DbValue_ToInt16(Object mDateValue)
        {
            try
            {
                return Convert.ToInt16(mDateValue);
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region DbValue_ToInt32
        public static Int32 DbValue_ToInt32(Object mDateValue)
        {
            try
            {
                return Convert.ToInt32(mDateValue);
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region DbValue_ToDouble
        public static double DbValue_ToDouble(Object mDateValue)
        {
            try
            {
                return Convert.ToDouble(mDateValue);
            }
            catch
            {
                return 0;
            }
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

                    //server type
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "afyapro_servertype")
                    {
                        gServerType = Convert.ToInt16(mXmlTextReader.ReadElementString("afyapro_servertype"));
                    }
                    //servername
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "afyapro_servername")
                    {
                        gServerName = mXmlTextReader.ReadElementString("afyapro_servername");
                    }
                    //serverport
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "afyapro_serverport")
                    {
                        gServerPort = mXmlTextReader.ReadElementString("afyapro_serverport");
                    }
                    //authentication
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "afyapro_authentication")
                    {
                        gAuthentication = Convert.ToInt16(mXmlTextReader.ReadElementString("afyapro_authentication"));
                    }
                    //serveruser
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "afyapro_serveruser")
                    {
                        gUserName = mXmlTextReader.ReadElementString("afyapro_serveruser");
                    }
                    //serverpassword
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "afyapro_serverpassword")
                    {
                        gPassword = mXmlTextReader.ReadElementString("afyapro_serverpassword");
                    }
                    //databasename
                    if (mXmlTextReader.NodeType == XmlNodeType.Element & mXmlTextReader.Name == "afyapro_databasename")
                    {
                        gDatabaseName = mXmlTextReader.ReadElementString("afyapro_databasename");
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
    }
}
