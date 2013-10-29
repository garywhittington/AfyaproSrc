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
using System.Xml;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Text.RegularExpressions;

namespace AfyaPro_ServerAdmin
{
    public partial class frmInitialSetup : DevExpress.XtraEditors.XtraForm
    {
        private Type pType;
        private string pClassName = "";

        private OdbcConnection pConn = new OdbcConnection();
        private OdbcDataAdapter pDataAdapter = new OdbcDataAdapter();
        private OdbcTransaction pTrans = null;
        private OdbcCommand pCommand = new OdbcCommand();
        private String pDefaultDatabase = "";
        private String pConnectionString = "";
        private string pDbParameterEscapeCharacter = "@";

        private bool pErrorOnPage0 = false;
        private bool pErrorOnPage1 = false;
        private bool pErrorOnPage2 = false;

        private bool pIsNewDB = false;
        private string pDbDateQuote = "";

        #region frmInitialSetup
        public frmInitialSetup()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
        }
        #endregion

        #region frmInitialSetup_Load
        private void frmInitialSetup_Load(object sender, EventArgs e)
        {
            cboDBMSType.SelectedIndex = Program.gServerType;
            txtDBMSServer.Text = Program.gServerName;
            txtServerPort.Text = Program.gMiddleTierPort.ToString();
            txtDBMSPort.Text = Program.gServerPort;
            cboDBMSAuthentication.SelectedIndex = Program.gAuthentication;
            txtDBMSUser.Text = Program.gUserName;
            txtDBMSPassword.Text = Program.gPassword;
            cboDBMSDatabase.Text = Program.gDatabaseName;

            this.Try_LoadingDatabases();
        }
        #endregion

        #region wizardControl1_NextClick
        private void wizardControl1_NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            string mFunctionName = "wizardControl1_NextClick";

            this.Cursor = Cursors.WaitCursor;

            try
            {
                switch (e.Page.Name.ToLower())
                {
                    case "page0":
                        {
                            pErrorOnPage0 = false;

                            if (Program.IsNumeric(txtServerPort.Text) == false)
                            {
                                pErrorOnPage0 = true;
                                Program.Display_Error("Invalid server port");
                                txtServerPort.Focus();
                                return;
                            }

                            Program.gMiddleTierPort = Convert.ToInt16(txtServerPort.Text);
                        }
                        break;
                    case "page1":
                        {
                            pErrorOnPage1 = false;

                            this.Cursor = Cursors.WaitCursor;

                            try
                            {
                                if (cboDBMSType.SelectedIndex == -1)
                                {
                                    pErrorOnPage1 = true;
                                    Program.Display_Error("Invalid DBMS type");
                                    cboDBMSType.Focus();
                                    return;
                                }

                                if (txtDBMSServer.Text.Trim() == "")
                                {
                                    pErrorOnPage1 = true;
                                    Program.Display_Error("Invalid server name");
                                    txtDBMSServer.Focus();
                                    return;
                                }

                                if (cboDBMSDatabase.Text.Trim() == "")
                                {
                                    pErrorOnPage1 = true;
                                    Program.Display_Error("Invalid database name");
                                    cboDBMSDatabase.Focus();
                                    return;
                                }

                                Program.gServerType = Convert.ToInt16(cboDBMSType.SelectedIndex);
                                Program.gServerName = txtDBMSServer.Text;
                                Program.gServerPort = txtDBMSPort.Text;
                                Program.gAuthentication = Convert.ToInt16(cboDBMSAuthentication.SelectedIndex);
                                Program.gUserName = txtDBMSUser.Text;
                                Program.gPassword = txtDBMSPassword.Text;
                                Program.gDatabaseName = cboDBMSDatabase.Text;

                                #region create database if not exist

                                Boolean mResult = this.Get_DefaultDatabase();
                                if (mResult == false)
                                {
                                    pErrorOnPage1 = true;
                                    return;
                                }

                                this.Create_ConnectionString(pDefaultDatabase);
                                pConn.ConnectionString = pConnectionString;
                                pCommand.Connection = pConn;

                                if (this.Test_Connection() == false)
                                {
                                    pErrorOnPage1 = true;
                                    return;
                                }

                                //create database if does not exist
                                DataTable mDtDatabases = this.Get_DbList_OnServer();
                                DataView mDvDatabases = new DataView();
                                mDvDatabases.Table = mDtDatabases;
                                mDvDatabases.Sort = mDtDatabases.Columns[0].ColumnName;

                                if (mDvDatabases.Find(Program.gDatabaseName) < 0)
                                {
                                    if (pConn.State != ConnectionState.Open)
                                    {
                                        pConn.Open();
                                    }

                                    //create working db
                                    pCommand.CommandText = "create database " + Program.gDatabaseName;
                                    pCommand.ExecuteNonQuery();

                                    //create audit db
                                    pCommand.CommandText = "create database " + Program.gDatabaseName + "_audit";
                                    pCommand.ExecuteNonQuery();

                                    //create tables
                                    switch (Program.gServerType)
                                    {
                                        case 0:
                                            {
                                                mResult = this.RunScript(Program.gDatabaseName, Resources.newdb_mysql.ToString(), Resources.views_mysql.ToString(), Resources.settings_mysql.ToString());
                                                pIsNewDB = true;
                                            }
                                            break;
                                        case 1:
                                            {
                                                mResult = this.RunScript(Program.gDatabaseName, Resources.newdb_mssqlserver.ToString(), Resources.views_mssqlserver.ToString(), Resources.settings_mysql.ToString());
                                                pIsNewDB = true;
                                            }
                                            break;
                                    }

                                    //load default settings
                                    this.Import_FromXml("sys_formsizes.xml", Program.gDatabaseName, "sys_formsizes");
                                    this.Import_FromXml("sys_gridlayouts.xml", Program.gDatabaseName, "sys_gridlayouts");
                                    this.Import_FromXml("sys_reportgroups.xml", Program.gDatabaseName, "sys_reportgroups");
                                    this.Import_FromXml("sys_reports.xml", Program.gDatabaseName, "sys_reports");
                                    this.Import_FromXml("sys_searchfields.xml", Program.gDatabaseName, "sys_searchfields");
                                    this.Import_FromXml("dxticddiagnoses.xml", Program.gDatabaseName, "dxticddiagnoses");

                                    if (mResult == false)
                                    {
                                        pErrorOnPage1 = true;
                                        return;
                                    }
                                }

                                #endregion

                                mResult = this.CheckConfiguration();
                                if (mResult == false)
                                {
                                    pErrorOnPage1 = true;
                                    return;
                                }
                                mResult = this.WriteIni();
                                if (mResult == false)
                                {
                                    pErrorOnPage1 = true;
                                    return;
                                }

                                #region restart service

                                System.ServiceProcess.ServiceController mServiceController =
                                    new System.ServiceProcess.ServiceController("AfyaPro_Service");

                                if (mServiceController.Status == System.ServiceProcess.ServiceControllerStatus.Running)
                                {
                                    mServiceController.Stop();
                                    mServiceController.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Stopped);
                                }

                                if (mServiceController.Status == System.ServiceProcess.ServiceControllerStatus.Stopped)
                                {
                                    mServiceController.Start();
                                    mServiceController.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Running);
                                }

                                #endregion
                            }
                            catch (Exception ex)
                            {
                                pErrorOnPage1 = true;
                                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                                return;
                            }
                            finally
                            {
                                this.Cursor = Cursors.Default;
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region wizardControl1_FinishClick
        private void wizardControl1_FinishClick(object sender, CancelEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region wizardControl1_SelectedPageChanging
        private void wizardControl1_SelectedPageChanging(object sender, DevExpress.XtraWizard.WizardPageChangingEventArgs e)
        {
            if (e.Direction == DevExpress.XtraWizard.Direction.Forward)
            {
                switch (e.PrevPage.Name.ToLower())
                {
                    case "page0":
                        {
                            if (pErrorOnPage0 == true)
                            {
                                e.Cancel = true;
                            }
                        }
                        break;
                    case "page1":
                        {
                            if (pErrorOnPage1 == true)
                            {
                                e.Cancel = true;
                            }
                        }
                        break;
                    case "page2":
                        {
                            if (pErrorOnPage2 == true)
                            {
                                e.Cancel = true;
                            }
                        }
                        break;
                }
            }
        }
        #endregion

        #region wizardControl1_CancelClick
        private void wizardControl1_CancelClick(object sender, CancelEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region cboDBMSType_SelectedIndexChanged
        private void cboDBMSType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDBMSType.SelectedIndex == 1)
            {
                cboDBMSAuthentication.Enabled = true;
                cboDBMSAuthentication.SelectedIndex = 0;
                txtDBMSUser.Enabled = false;
                txtDBMSPassword.Enabled = false;
                txtDBMSUser.Text = "";
                txtDBMSPassword.Text = "";
            }
            else
            {
                cboDBMSAuthentication.Enabled = false;
                cboDBMSAuthentication.SelectedIndex = -1;
                txtDBMSUser.Enabled = true;
                txtDBMSPassword.Enabled = true;
                txtDBMSUser.Text = "";
                txtDBMSPassword.Text = "";
            }
        }
        #endregion

        #region cboDBMSAuthentication_SelectedIndexChanged
        private void cboDBMSAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDBMSAuthentication.SelectedIndex == 0)
            {
                txtDBMSUser.Enabled = false;
                txtDBMSPassword.Enabled = false;
                txtDBMSUser.Text = "";
                txtDBMSPassword.Text = "";
            }
            else
            {
                txtDBMSUser.Enabled = true;
                txtDBMSPassword.Enabled = true;
                txtDBMSUser.Text = "";
                txtDBMSPassword.Text = "";
            }
        }
        #endregion

        #region Try_LoadingDatabases
        private void Try_LoadingDatabases()
        {
            try
            {
                Program.gServerType = Convert.ToInt16(cboDBMSType.SelectedIndex);
                Program.gServerName = txtDBMSServer.Text;
                Program.gServerPort = txtDBMSPort.Text;
                Program.gAuthentication = Convert.ToInt16(cboDBMSAuthentication.SelectedIndex);
                Program.gUserName = txtDBMSUser.Text;
                Program.gPassword = txtDBMSPassword.Text;

                if (Program.gServerType == -1)
                {
                    return;
                }
                if (Program.gServerName.Trim() == "")
                {
                    return;
                }
                if (Program.gUserName.Trim() == "")
                {
                    return;
                }
                if (Program.gPassword.Trim() == "")
                {
                    return;
                }

                this.Get_DefaultDatabase();
                this.Create_ConnectionString(pDefaultDatabase);
                cboDBMSDatabase.Properties.Items.Clear();

                //if connection is closed, open it
                if (pConn.State != ConnectionState.Open)
                {
                    pConn.Open();
                }

                DataTable mDtDatabases = this.Get_DbList_OnServer();

                for (Int32 mRow = 0; mRow < mDtDatabases.Rows.Count; mRow++)
                {
                    cboDBMSDatabase.Properties.Items.Add(mDtDatabases.Rows[mRow][0].ToString());
                }
            }
            catch 
            { 
            }
            finally 
            {
                try
                {
                    pConn.Close();
                }
                catch { }
            }
        }
        #endregion

        #region check configuration file
        private bool CheckConfiguration()
        {
            String mFunctionName = "CheckConfiguration";

            try
            {
                if (System.IO.File.Exists(Program.pIniFileName) == true)
                {
                    System.IO.File.Delete(Program.pIniFileName);
                }
                return true;
            }
            catch (Exception e)
            {
                Program.Display_Error(pClassName, mFunctionName, e.Message);
                return false;
            }
        }
        #endregion

        #region write xml configuration file
        private bool WriteIni()
        {
            try
            {
                //create xml doc and specify formating informations
                XmlTextWriter mXmlTextWriter;
                mXmlTextWriter = new XmlTextWriter(Program.pIniFileName, new System.Text.ASCIIEncoding());
                mXmlTextWriter.Formatting = Formatting.Indented;
                mXmlTextWriter.Indentation = 4;

                //write xml declaration with version 1.0 and a comments
                mXmlTextWriter.WriteStartDocument();
                mXmlTextWriter.WriteComment("Configurations Settings for AfyaPro Server");

                //xml body
                mXmlTextWriter.WriteStartElement("AfyaPro_Server_Configuration");
                //Connections Settings
                mXmlTextWriter.WriteStartElement("Connection_Settings");
                //middler tier port
                mXmlTextWriter.WriteStartElement("Middle_Tier_Port");
                mXmlTextWriter.WriteElementString("middletierport", Program.gMiddleTierPort.ToString());
                mXmlTextWriter.WriteEndElement();

                //server type
                mXmlTextWriter.WriteStartElement("afyapro_Server_Type");
                mXmlTextWriter.WriteElementString("afyapro_servertype", Program.gServerType.ToString());
                mXmlTextWriter.WriteEndElement();
                //server name
                mXmlTextWriter.WriteStartElement("afyapro_Server_Name");
                mXmlTextWriter.WriteElementString("afyapro_servername", Program.gServerName);
                mXmlTextWriter.WriteEndElement();
                //server port
                mXmlTextWriter.WriteStartElement("afyapro_Server_Port");
                mXmlTextWriter.WriteElementString("afyapro_serverport", Program.gServerPort);
                mXmlTextWriter.WriteEndElement();
                //authentication
                mXmlTextWriter.WriteStartElement("afyapro_Authentication_Type");
                mXmlTextWriter.WriteElementString("afyapro_authentication", Program.gAuthentication.ToString());
                mXmlTextWriter.WriteEndElement();
                //server user
                mXmlTextWriter.WriteStartElement("afyapro_Server_User");
                mXmlTextWriter.WriteElementString("afyapro_serveruser", Program.gUserName);
                mXmlTextWriter.WriteEndElement();
                //server password
                mXmlTextWriter.WriteStartElement("afyapro_Server_Password");
                mXmlTextWriter.WriteElementString("afyapro_serverpassword", Program.gPassword);
                mXmlTextWriter.WriteEndElement();
                //database name
                mXmlTextWriter.WriteStartElement("afyapro_Database_Name");
                mXmlTextWriter.WriteElementString("afyapro_databasename", Program.gDatabaseName);
                mXmlTextWriter.WriteEndElement();

                mXmlTextWriter.WriteEndElement();
                mXmlTextWriter.WriteEndElement();
                //Write End of document
                mXmlTextWriter.WriteEndDocument();

                //Close any open elements
                mXmlTextWriter.Flush();
                mXmlTextWriter.Close();
                Program.Read_Config_Settings();

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
        #endregion

        #region Get_DefaultDatabase
        public bool Get_DefaultDatabase()
        {
            pType = this.GetType();
            String pClassName = pType.FullName;

            String mFunctionName = "Get_DefaultDatabase";

            try
            {
                switch (cboDBMSType.SelectedIndex)
                {
                    case 0:
                        {
                            pDefaultDatabase = "mysql";
                        }
                        break;
                    case 1:
                        {
                            pDefaultDatabase = "master";
                        }
                        break;
                }

                return true;
            }
            catch (Exception ex)
            {
                Program.Write_Error(pClassName, mFunctionName, ex.Message);
                return false;
            }
        }
        #endregion

        #region Create_ConnectionString
        internal String Create_ConnectionString(String mDatabaseName)
        {
            String mServerDriver = "";

            String mFunctionName = "Create_ConnectionString";

            try
            {
                //determine server type
                switch (cboDBMSType.SelectedIndex)
                {
                    case 0:
                        {
                            pDbParameterEscapeCharacter = "?";

                            mServerDriver = "MySQL ODBC 5.1 Driver";
                            pDbDateQuote = "'";
                            //connectionstring
                            pConnectionString = "DRIVER={" + mServerDriver
                                + "};SERVER=" + txtDBMSServer.Text
                                + ";UID=" + txtDBMSUser.Text
                                + ";PWD=" + txtDBMSPassword.Text
                                + ";DATABASE=" + mDatabaseName
                                + ";Max Pool Size=50"
                                + ";Min Pool Size=5"
                                + "Pooling=True"
                                + ";OPTION=3";
                        }
                        break;
                    case 1:
                        {
                            pDbParameterEscapeCharacter = "@";

                            mServerDriver = "SQL Server";
                            pDbDateQuote = "'";
                            //connectionstring
                            if (cboDBMSAuthentication.SelectedIndex == 0)
                            {
                                pConnectionString = "DRIVER={" + mServerDriver
                                    + "};SERVER=" + txtDBMSServer.Text
                                    + ";Trusted_Connection=yes"
                                    + ";DATABASE=" + mDatabaseName
                                    + ";Max Pool Size=50"
                                    + ";Min Pool Size=5"
                                    + "Pooling=True"
                                    + ";OPTION=3";
                            }
                            else
                            {
                                pConnectionString = "DRIVER={" + mServerDriver
                                   + "};SERVER=" + txtDBMSServer.Text
                                   + ";UID=" + txtDBMSUser.Text
                                   + ";PWD=" + txtDBMSPassword.Text
                                   + ";DATABASE=" + mDatabaseName
                                   + ";Max Pool Size=50"
                                   + ";Min Pool Size=5"
                                   + "Pooling=True"
                                   + ";OPTION=3";
                            }
                        }
                        break;
                }

                pConn.ConnectionString = pConnectionString;
                return pConnectionString;
            }
            catch (Exception ex)
            {
                Program.Write_Error(pClassName, mFunctionName, ex.Message);
                return "";
            }
        }
        #endregion

        #region Test_Connection
        private Boolean Test_Connection()
        {
            String mFunctionName = "Test_Connection";

            try
            {
                pConn.Open();

                return true;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return false;
            }
            finally
            {
                pConn.Close();
            }
        }
        #endregion

        #region Get_DbList_OnServer
        private DataTable Get_DbList_OnServer()
        {
            DataTable mDtDatabases = new DataTable("databases");
            String mCommandText = "";
            String mFunctionName = "Get_DbList_OnServer";

            try
            {
                this.Get_DefaultDatabase();
                this.Create_ConnectionString(pDefaultDatabase);

                //if connection is closed, open it
                if (pConn.State != ConnectionState.Open)
                {
                    pConn.Open();
                }

                switch (Program.gServerType)
                {
                    case 0:
                        {
                            mCommandText = "show databases";
                        }
                        break;
                    case 1:
                        {
                            mCommandText = "SELECT name FROM master.dbo.sysdatabases";
                        }
                        break;
                }

                pCommand.Connection = pConn;
                pCommand.CommandText = mCommandText;
                pDataAdapter.SelectCommand = pCommand;
                pDataAdapter.Fill(mDtDatabases);

                return mDtDatabases;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
            finally
            {
                pConn.Close();
            }
        }
        #endregion

        #region RunScript
        public Boolean RunScript(String mDataBaseName, string mTablesFile, string mViewsFile, string mSettingsFile)
        {
            String mFunctionName = "RunScript";

            try
            {
                pConn.ChangeDatabase(mDataBaseName);

                //if connection is closed, open it
                if (pConn.State != ConnectionState.Open)
                {
                    pConn.Open();
                }

                pCommand.Connection = pConn;
                pTrans = pConn.BeginTransaction();
                pCommand.Transaction = pTrans;

                //TABLES
                String[] mCommandsTables = ParseScriptToCommands(mTablesFile);
                foreach (String mCommand in mCommandsTables)
                {
                    if (mCommand.Length > 0 && mCommand.Trim() != "")
                    {
                        pCommand.CommandText = mCommand;
                        pCommand.ExecuteNonQuery();
                    }
                }

                //commit to create tables
                pTrans.Commit();

                //VIEWS
                pTrans = pConn.BeginTransaction();
                pCommand.Transaction = pTrans;

                String[] mCommandsViews = ParseScriptToCommands(mViewsFile);
                foreach (String mCommand in mCommandsViews)
                {
                    if (mCommand.Length > 0 && mCommand.Trim() != "")
                    {
                        pCommand.CommandText = mCommand;
                        pCommand.ExecuteNonQuery();
                    }
                }

                //commit to create views
                pTrans.Commit();

                ////SETTINGS
                //pTrans = pConn.BeginTransaction();
                //pCommand.Transaction = pTrans;

                //String[] mCommandsSettings = ParseScriptToCommands(mSettingsFile);
                //foreach (String mCommand in mCommandsSettings)
                //{
                //    if (mCommand.Length > 0 && mCommand.Trim() != "")
                //    {
                //        pCommand.CommandText = mCommand;
                //        pCommand.ExecuteNonQuery();
                //    }
                //}

                ////commit to create views
                //pTrans.Commit();

                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    pTrans.Rollback();
                }
                catch { }
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return false;
            }
            finally
            {
                pConn.Close();
            }
        }
        #endregion

        #region ParseScriptToCommands
        public String[] ParseScriptToCommands(String mScript)
        {
            String[] mCommands;
            mCommands = Regex.Split(mScript, ";", RegexOptions.IgnoreCase);
            return mCommands;
        }
        #endregion

        #region loading databases

        private void cboDBMSType_Leave(object sender, EventArgs e)
        {
            this.Try_LoadingDatabases();
        }

        private void txtDBMSServer_Leave(object sender, EventArgs e)
        {
            this.Try_LoadingDatabases();
        }

        private void txtDBMSPort_Leave(object sender, EventArgs e)
        {
            this.Try_LoadingDatabases();
        }

        private void cboDBMSAuthentication_Leave(object sender, EventArgs e)
        {
            this.Try_LoadingDatabases();
        }

        private void txtDBMSUser_Leave(object sender, EventArgs e)
        {
            this.Try_LoadingDatabases();
        }

        private void txtDBMSPassword_Leave(object sender, EventArgs e)
        {
            this.Try_LoadingDatabases();
        }

        #endregion

        #region cmdExport_Click
        private void cmdExport_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            this.Export_ToXml("sys_formsizes.xml", "sys_formsizes");
            this.Export_ToXml("sys_gridlayouts.xml", "sys_gridlayouts");
            this.Export_ToXml("sys_reportgroups.xml", "sys_reportgroups");
            this.Export_ToXml("sys_reports.xml", "sys_reports");
            this.Export_ToXml("sys_searchfields.xml", "sys_searchfields");
            this.Export_ToXml("dxticddiagnoses.xml", "dxticddiagnoses");

            this.Cursor = Cursors.Default;
        }
        #endregion

        #region cmdImport_Click
        private void cmdImport_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdImport_Click";

            this.Cursor = Cursors.Default;

            try
            {
                this.Import_FromXml("sys_formsizes.xml", Program.gDatabaseName, "sys_formsizes");
                this.Import_FromXml("sys_gridlayouts.xml", Program.gDatabaseName, "sys_gridlayouts");
                this.Import_FromXml("sys_reportgroups.xml", Program.gDatabaseName, "sys_reportgroups");
                this.Import_FromXml("sys_reports.xml", Program.gDatabaseName, "sys_reports");
                this.Import_FromXml("sys_searchfields.xml", Program.gDatabaseName, "sys_searchfields");
                this.Import_FromXml("dxticddiagnoses.xml", Program.gDatabaseName, "dxticddiagnoses");
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Import_FromXml
        private void Import_FromXml(string mFileName, string mDatabaseName, string mTableName)
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            string mFunctionName = "Import_FromXml";

            try
            {
                mConn.ConnectionString = Create_ConnectionString(mDatabaseName);
                mConn.Open();
                mCommand.Connection = mConn;

                DataTable mDtXmlData = new DataTable();
                mDtXmlData.ReadXml(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + mFileName);

                string mColumnsList = "";
                string mParametersList = "";
                foreach (DataColumn mDataColumn in mDtXmlData.Columns)
                {
                    if (mDataColumn.ColumnName.ToLower() != "autocode")
                    {
                        if (mColumnsList.Trim() == "")
                        {
                            mColumnsList = mDataColumn.ColumnName;
                            //mParametersList = pDbParameterEscapeCharacter + "para_" + mDataColumn.ColumnName;
                            mParametersList = pDbParameterEscapeCharacter;
                        }
                        else
                        {
                            mColumnsList = mColumnsList + "," + mDataColumn.ColumnName;
                            //mParametersList = mParametersList + "," + pDbParameterEscapeCharacter + "para_" + mDataColumn.ColumnName;
                            mParametersList = mParametersList + "," + pDbParameterEscapeCharacter;
                        }
                    }
                }

                mCommand.CommandText = "delete from " + mTableName;
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtXmlData.Rows)
                {
                    mCommand.CommandText = "insert into " + mTableName + "(" + mColumnsList + ") values(" + mParametersList + ")";

                    foreach (DataColumn mDataColumn in mDtXmlData.Columns)
                    {
                        string mColumnName = mDataColumn.ColumnName;
                        string mDataType = mDataColumn.DataType.FullName;

                        if (mColumnName.ToLower() != "autocode")
                        {
                            #region parameter values

                            switch (mDataType.ToLower())
                            {
                                case "system.char":
                                    {
                                        OdbcParameter mOdbcParameter = mCommand.Parameters.Add(pDbParameterEscapeCharacter + "para_" + mColumnName, OdbcType.Char);
                                        mOdbcParameter.Value = mDataRow[mDataColumn.ColumnName].ToString();
                                    }
                                    break;
                                case "system.string":
                                    {
                                        OdbcParameter mOdbcParameter = mCommand.Parameters.Add(pDbParameterEscapeCharacter + "para_" + mColumnName, OdbcType.VarChar);
                                        mOdbcParameter.Value = mDataRow[mDataColumn.ColumnName].ToString();
                                    }
                                    break;
                                case "system.decimal":
                                    {
                                        OdbcParameter mOdbcParameter = mCommand.Parameters.Add(pDbParameterEscapeCharacter + "para_" + mColumnName, OdbcType.Double);
                                        mOdbcParameter.Value = mDataRow[mDataColumn.ColumnName];
                                    }
                                    break;
                                case "system.double":
                                    {
                                        OdbcParameter mOdbcParameter = mCommand.Parameters.Add(pDbParameterEscapeCharacter + "para_" + mColumnName, OdbcType.Double);
                                        mOdbcParameter.Value = mDataRow[mDataColumn.ColumnName];
                                    }
                                    break;
                                case "system.int16":
                                    {
                                        OdbcParameter mOdbcParameter = mCommand.Parameters.Add(pDbParameterEscapeCharacter + "para_" + mColumnName, OdbcType.Int);
                                        mOdbcParameter.Value = mDataRow[mDataColumn.ColumnName];
                                    }
                                    break;
                                case "system.int32":
                                    {
                                        OdbcParameter mOdbcParameter = mCommand.Parameters.Add(pDbParameterEscapeCharacter + "para_" + mColumnName, OdbcType.Int);
                                        mOdbcParameter.Value = mDataRow[mDataColumn.ColumnName];
                                    }
                                    break;
                                case "system.int64":
                                    {
                                        OdbcParameter mOdbcParameter = mCommand.Parameters.Add(pDbParameterEscapeCharacter + "para_" + mColumnName, OdbcType.Int);
                                        mOdbcParameter.Value = mDataRow[mDataColumn.ColumnName];
                                    }
                                    break;
                                case "system.datetime":
                                    {
                                        OdbcParameter mOdbcParameter = mCommand.Parameters.Add(pDbParameterEscapeCharacter + "para_" + mColumnName, OdbcType.DateTime);
                                        mOdbcParameter.Value = mDataRow[mDataColumn.ColumnName];
                                    }
                                    break;
                                case "system.byte[]":
                                    {
                                        OdbcParameter mOdbcParameter = mCommand.Parameters.Add(pDbParameterEscapeCharacter + "para_" + mColumnName, OdbcType.Binary);
                                        mOdbcParameter.Value = mDataRow[mDataColumn.ColumnName];
                                    }
                                    break;
                            }

                            #endregion
                        }
                    }

                    mCommand.ExecuteNonQuery();
                    mCommand.Parameters.Clear();
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
            }
            finally
            {
                try { mConn.Close(); }
                catch { }
            }
        }
        #endregion

        #region Export_ToXml

        private void Export_ToXml(string mFileName, string mTableName)
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            string mFunctionName = "Export_ToXml";

            try
            {
                mConn.ConnectionString = Create_ConnectionString(Program.gDatabaseName);
                mConn.Open();
                mCommand.Connection = mConn;

                DataTable mDtFormLayouts = new DataTable(mTableName);
                mCommand.CommandText = "select * from " + mTableName;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFormLayouts);

                mDtFormLayouts.WriteXml(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + mFileName, XmlWriteMode.WriteSchema);
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
            finally
            {
                try { mConn.Close(); }
                catch { }
            }
        }

        #endregion
    }
}