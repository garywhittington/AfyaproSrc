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
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Text.RegularExpressions;

namespace AfyaPro_MT
{
    internal class clsDatabaseUpdate
    {
        private string pClassName = "AfyaPro_MT.clsDatabaseUpdate";
        private string pDefaultDatabase = "";
        private DataTable pDtDatabases = new DataTable("databases");
        private string pConnString = "";

        #region clsDatabaseUpdate
        internal clsDatabaseUpdate()
        {
            this.Get_DefaultDatabase();
            pConnString = clsGlobal.Create_GenConStr(pDefaultDatabase);
            this.Get_DatabasesList();
        }
        #endregion

        #region Get_DefaultDatabase
        private bool Get_DefaultDatabase()
        {
            string mFunctionName = "Get_DefaultDatabase";

            try
            {
                switch (clsGlobal.gAfyaPro_ServerType)
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
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return false;
            }
        }
        #endregion

        #region Get_DatabasesList
        private bool Get_DatabasesList()
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            
            string mFunctionName = "Get_DatabasesList";

            #region connection
            try
            {
                mConn.ConnectionString = pConnString;
                mConn.Open();
                mCommand.Connection = mConn;
            }
            catch (OdbcException ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return false;
            }
            #endregion

            try
            {
                string mCommandText = "";

                switch (clsGlobal.gAfyaPro_ServerType)
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

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(pDtDatabases);

                return true;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return false;
            }
            finally
            {
                mConn.Close();
            }
        }
        #endregion

        #region Create_UpdaterDatabase
        internal bool Create_UpdaterDatabase()
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            string mFunctionName = "Create_UpdateDatabase";

            #region connection
            try
            {
                mConn.ConnectionString = pConnString;
                mConn.Open();
                mCommand.Connection = mConn;
            }
            catch (OdbcException ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return false;
            }
            #endregion

            try
            {
                #region create empty database

                DataView mDvDatabases = new DataView();
                mDvDatabases.Table = pDtDatabases;
                mDvDatabases.Sort = pDtDatabases.Columns[0].ColumnName;

                if (mDvDatabases.Find("temp_afyaproupdater") >= 0)
                {
                    mCommand.CommandText = "drop database temp_afyaproupdater";
                    mCommand.ExecuteNonQuery();
                }

                mCommand.CommandText = "create database temp_afyaproupdater";
                mCommand.ExecuteNonQuery();

                #endregion

                #region tables

                mConn.ChangeDatabase("temp_afyaproupdater");
                mCommand.Connection = mConn;

                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                string[] mCommands = ParseScriptToCommands(resScripts.newdb_mysql.ToString());
                foreach (string mCommandText in mCommands)
                {
                    if (mCommandText.Length > 0 && mCommandText.Trim() != "")
                    {
                        mCommand.CommandText = mCommandText;
                        mCommand.ExecuteNonQuery();
                    }
                }

                mTrans.Commit();

                #endregion

                return true;
            }
            catch (OdbcException ex)
            {
                //delete temp database
                try
                {
                    mConn.ChangeDatabase(pDefaultDatabase);
                    mCommand.Connection = mConn;

                    mCommand.CommandText = "drop database temp_afyaproupdater";
                    mCommand.ExecuteNonQuery();
                }
                catch { }

                //rollback
                try { mTrans.Rollback(); }
                catch { }
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return false;
            }
            finally
            {
                try { mConn.Close(); }
                catch { }
            }
        }
        #endregion

        #region ParseScriptToCommands
        public string[] ParseScriptToCommands(string mScript)
        {
            string[] mCommands;
            mCommands = Regex.Split(mScript, ";", RegexOptions.IgnoreCase);
            return mCommands;
        }
        #endregion

        #region Update_Database
        internal bool Update_Database(string mDatabaseName)
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();

            string mFunctionName = "Update_Database";

            #region connection
            try
            {
                mConn.ConnectionString = pConnString;
                mConn.Open();
                mConn.ChangeDatabase(mDatabaseName);
                mCommand.Connection = mConn;
            }
            catch (OdbcException ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return false;
            }
            #endregion

            try
            {
                #region facilitybookings autocode has changed to varchar(50) from database version 3.1.2.5 and above

                bool mIsVersion3_1_2_5Plus = true;

                if (clsGlobal.gDbVersionMajor < 3)
                {
                    mIsVersion3_1_2_5Plus = false;
                }
                else if (clsGlobal.gDbVersionMinor < 1)
                {
                    mIsVersion3_1_2_5Plus = false;
                }
                else if (clsGlobal.gDbVersionBuild < 2)
                {
                    mIsVersion3_1_2_5Plus = false;
                }
                else if (clsGlobal.gDbVersionRevision < 5)
                {
                    mIsVersion3_1_2_5Plus = false;
                }

                if (mIsVersion3_1_2_5Plus == false)
                {
                    mCommand.CommandText = "ALTER TABLE facilitybookings CHANGE autocode autocode VARCHAR(50)";
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region full text search has been added for patients table from database version 

                bool mIsVersion3_1_3_3Plus = true;

                if (clsGlobal.gDbVersionMajor < 3)
                {
                    mIsVersion3_1_3_3Plus = false;
                }
                else if (clsGlobal.gDbVersionMinor < 1)
                {
                    mIsVersion3_1_3_3Plus = false;
                }
                else if (clsGlobal.gDbVersionBuild < 3)
                {
                    mIsVersion3_1_3_3Plus = false;
                }
                else if (clsGlobal.gDbVersionRevision < 3)
                {
                    mIsVersion3_1_3_3Plus = false;
                }

                if (mIsVersion3_1_3_3Plus == false)
                {
                    switch (clsGlobal.gAfyaPro_ServerType)
                    {
                        case 0:
                            {
                                ////change table engine
                                //mCommand.CommandText = "ALTER TABLE patients TYPE = MYISAM";
                                //mCommand.ExecuteNonQuery();

                                ////add full text columns
                                //mCommand.CommandText = "ALTER TABLE patients ADD FULLTEXT(firstname, othernames, surname)";
                                //mCommand.ExecuteNonQuery();
                            }
                            break;
                    }
                }

                #endregion

                if (clsGlobal.gDbVersionBuild < 40)
                {
                    this.Drop_PatientFields(mDatabaseName);
                }

                string mUpdateScript = this.Build_UpdateScript(mDatabaseName);

                //tables
                if (mUpdateScript.Trim() != "")
                {
                    string[] mCommands = ParseScriptToCommands(mUpdateScript);
                    foreach (string mCommandText in mCommands)
                    {
                        if (mCommandText.Length > 0 && mCommandText.Trim() != "")
                        {
                            mCommand.CommandText = mCommandText;
                            mCommand.ExecuteNonQuery();
                        }
                    }
                }

                //views
                string[] mViews = ParseScriptToCommands(resScripts.views_mysql.ToString());
                foreach (string mCommandText in mViews)
                {
                    if (mCommandText.Length > 0 && mCommandText.Trim() != "")
                    {
                        try
                        {
                            mCommand.CommandText = mCommandText;
                            mCommand.ExecuteNonQuery();
                        }
                        catch { }
                    }
                }

                //default settings


                mCommand.CommandText = "drop database temp_afyaproupdater";
                mCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return false;
            }
        }
        #endregion

        #region Build_UpdateScript
        private string Build_UpdateScript(string mDatabaseName)
        {
            OdbcConnection mConnSource = new OdbcConnection();
            OdbcConnection mConnDestination = new OdbcConnection();
            OdbcCommand mCommandSource = new OdbcCommand();
            OdbcCommand mCommandDestination = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            string mFunctionName = "Build_UpdateScript";

            #region connection
            try
            {
                //source
                mConnSource.ConnectionString = pConnString;
                mConnSource.Open();
                mConnSource.ChangeDatabase("temp_afyaproupdater");
                mCommandSource.Connection = mConnSource;

                //destination
                mConnDestination.ConnectionString = pConnString;
                mConnDestination.Open();
                mConnDestination.ChangeDatabase(mDatabaseName);
                mCommandDestination.Connection = mConnDestination;
            }
            catch (OdbcException ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return "";
            }
            #endregion

            try
            {
                string mUpdateScript = "";

                //string[] mRestrictions = new string[4];
                //mRestrictions[1] = "dbo";

                DataTable mDtSource = mConnSource.GetSchema("Tables");
                DataTable mDtDestination = mConnDestination.GetSchema("Tables");

                DataView mDvDestinationTables = new DataView();
                mDvDestinationTables.Table = mDtDestination;
                mDvDestinationTables.Sort = "table_name";

                #region new tables

                foreach (DataRow mDataRowTable in mDtSource.Rows)
                {
                    string mTableName = mDataRowTable["table_name"].ToString();

                    if (mDvDestinationTables.Find(mTableName) < 0)
                    {
                        string mScriptCreateTable = "CREATE TABLE " + mTableName + "(" + Environment.NewLine;

                        DataTable mDtColumnsSource = new DataTable("columnssource");
                        mCommandSource.CommandText = "select * from " + mTableName + " where 1=2";
                        mDataAdapter.SelectCommand = mCommandSource;
                        mDataAdapter.FillSchema(mDtColumnsSource, SchemaType.Source);

                        string mColumnsString = "";
                        string mPrimaryKey = "";
                        foreach (DataColumn mDataColumn in mDtColumnsSource.Columns)
                        {
                            string mColumnString = "";

                            #region columns

                            string mColumnName = mDataColumn.ColumnName;
                            string mDataType = mDataColumn.DataType.FullName;
                            Int32 mCharacter_MaxLength = mDataColumn.MaxLength;
                            string mDefaultValue = "";
                            bool mIsNullable = mDataColumn.AllowDBNull;

                            if (mColumnName.ToLower() == "autocode")
                            {
                                mColumnString = mColumnName + " INT(11) NOT NULL auto_increment UNIQUE";
                            }
                            else
                            {
                                switch (mDataType.ToLower())
                                {
                                    case "system.char":
                                        {
                                            mColumnString = mColumnName + " VARCHAR(" + (mCharacter_MaxLength - 1) + ")";
                                            if (mIsNullable == true)
                                            {
                                                mColumnString = mColumnString + " NULL";
                                            }
                                            else
                                            {
                                                mColumnString = mColumnString + " NOT NULL";
                                            }

                                            mColumnString = mColumnString + " DEFAULT '" + mDefaultValue + "'";
                                        }
                                        break;
                                    case "system.string":
                                        {
                                            mColumnString = mColumnName + " VARCHAR(" + (mCharacter_MaxLength - 1) + ")";
                                            if (mIsNullable == true)
                                            {
                                                mColumnString = mColumnString + " NULL";
                                            }
                                            else
                                            {
                                                mColumnString = mColumnString + " NOT NULL";
                                            }

                                            mColumnString = mColumnString + " DEFAULT '" + mDefaultValue + "'";
                                        }
                                        break;
                                    case "system.decimal":
                                        {
                                            mColumnString = mColumnName + " DOUBLE(16,2)";
                                            if (mIsNullable == true)
                                            {
                                                mColumnString = mColumnString + " NULL";
                                            }
                                            else
                                            {
                                                mColumnString = mColumnString + " NOT NULL";
                                            }

                                            mColumnString = mColumnString + " DEFAULT '0.00'";
                                        }
                                        break;
                                    case "system.double":
                                        {
                                            mColumnString = mColumnName + " DOUBLE(16,2)";
                                            if (mIsNullable == true)
                                            {
                                                mColumnString = mColumnString + " NULL";
                                            }
                                            else
                                            {
                                                mColumnString = mColumnString + " NOT NULL";
                                            }

                                            mColumnString = mColumnString + " DEFAULT '0.00'";
                                        }
                                        break;
                                    case "system.int16":
                                        {
                                            mColumnString = mColumnName + " INT(11)";
                                            if (mIsNullable == true)
                                            {
                                                mColumnString = mColumnString + " NULL";
                                            }
                                            else
                                            {
                                                mColumnString = mColumnString + " NOT NULL";
                                            }

                                            mColumnString = mColumnString + " DEFAULT '0'";
                                        }
                                        break;
                                    case "system.int32":
                                        {
                                            mColumnString = mColumnName + " INT(11)";
                                            if (mIsNullable == true)
                                            {
                                                mColumnString = mColumnString + " NULL";
                                            }
                                            else
                                            {
                                                mColumnString = mColumnString + " NOT NULL";
                                            }

                                            mColumnString = mColumnString + " DEFAULT '0'";
                                        }
                                        break;
                                    case "system.int64":
                                        {
                                            mColumnString = mColumnName + " INT(11)";
                                            if (mIsNullable == true)
                                            {
                                                mColumnString = mColumnString + " NULL";
                                            }
                                            else
                                            {
                                                mColumnString = mColumnString + " NOT NULL";
                                            }

                                            mColumnString = mColumnString + " DEFAULT '0'";
                                        }
                                        break;
                                    case "system.datetime":
                                        {
                                            mColumnString = mColumnName + " DATETIME";
                                            if (mIsNullable == true)
                                            {
                                                mColumnString = mColumnString + " NULL";
                                            }
                                            else
                                            {
                                                mColumnString = mColumnString + " NOT NULL";
                                            }
                                        }
                                        break;
                                    case "system.byte[]":
                                        {
                                            mColumnString = mColumnName + " LONGBLOB";
                                            if (mIsNullable == true)
                                            {
                                                mColumnString = mColumnString + " NULL";
                                            }
                                            else
                                            {
                                                mColumnString = mColumnString + " NOT NULL";
                                            }
                                        }
                                        break;
                                    default:
                                        {
                                            mColumnString = mColumnString + mDataType;
                                        }
                                        break;
                                }
                            }

                            #endregion

                            if (mColumnsString.Trim() == "")
                            {
                                mColumnsString = mColumnString;
                            }
                            else
                            {
                                mColumnsString = mColumnsString + "," + Environment.NewLine + mColumnString;
                            }

                            if (mColumnName.ToLower() == "autocode")
                            {
                                if (mPrimaryKey.Trim() == "")
                                {
                                    mPrimaryKey = mColumnName;
                                }
                                else
                                {
                                    mPrimaryKey += "," + mColumnName;
                                }
                            }
                        }

                        if (mColumnsString.Trim() != "")
                        {
                            mScriptCreateTable = mScriptCreateTable + mColumnsString;

                            if (mPrimaryKey.Trim() != "")
                            {
                                mScriptCreateTable += "," + Environment.NewLine
                                    + "PRIMARY KEY (" + mPrimaryKey + ")) ENGINE=InnoDB DEFAULT CHARSET=latin1;" + Environment.NewLine;
                            }
                            else
                            {
                                mScriptCreateTable += ") ENGINE=InnoDB DEFAULT CHARSET=latin1;" + Environment.NewLine;
                            }
                            
                            mUpdateScript = mUpdateScript + mScriptCreateTable + Environment.NewLine;
                        }
                    }
                }

                #endregion

                #region altered tables

                foreach (DataRow mDataRowTable in mDtSource.Rows)
                {
                    string mTableName = mDataRowTable["table_name"].ToString();

                    if (mDvDestinationTables.Find(mTableName) >= 0)
                    {
                        DataTable mDtColumnsSource = new DataTable("columnssource");
                        mCommandSource.CommandText = "select * from " + mTableName + " where 1=2";
                        mDataAdapter.SelectCommand = mCommandSource;
                        mDataAdapter.FillSchema(mDtColumnsSource, SchemaType.Source);

                        DataTable mDtColumnsDestination = new DataTable("columnsdestination");
                        mCommandDestination.CommandText = "select * from " + mTableName + " where 1=2";
                        mDataAdapter.SelectCommand = mCommandDestination;
                        mDataAdapter.FillSchema(mDtColumnsDestination, SchemaType.Source);

                        string mScriptAlterTable = "ALTER TABLE " + mTableName + Environment.NewLine;

                        string mColumnsString = "";
                        foreach (DataColumn mDataColumn in mDtColumnsSource.Columns)
                        {
                            if (mDtColumnsDestination.Columns.Contains(mDataColumn.ColumnName) == false)
                            {
                                string mColumnString = "";

                                #region columns

                                string mColumnName = mDataColumn.ColumnName;
                                string mDataType = mDataColumn.DataType.FullName;
                                Int32 mCharacter_MaxLength = mDataColumn.MaxLength;
                                string mDefaultValue = "";
                                bool mIsNullable = mDataColumn.AllowDBNull;

                                if (mColumnName.ToLower() == "autocode")
                                {
                                    mColumnString = mColumnName + " INT(11) NOT NULL auto_increment";
                                }
                                else
                                {
                                    switch (mDataType.ToLower())
                                    {
                                        case "system.char":
                                            {
                                                mColumnString = mColumnName + " VARCHAR(" + (mCharacter_MaxLength - 1) + ")";
                                                if (mIsNullable == true)
                                                {
                                                    mColumnString = mColumnString + " NULL";
                                                }
                                                else
                                                {
                                                    mColumnString = mColumnString + " NOT NULL";
                                                }

                                                mColumnString = mColumnString + " DEFAULT '" + mDefaultValue + "'";
                                            }
                                            break;
                                        case "system.string":
                                            {
                                                mColumnString = mColumnName + " VARCHAR(" + (mCharacter_MaxLength - 1) + ")";
                                                if (mIsNullable == true)
                                                {
                                                    mColumnString = mColumnString + " NULL";
                                                }
                                                else
                                                {
                                                    mColumnString = mColumnString + " NOT NULL";
                                                }

                                                mColumnString = mColumnString + " DEFAULT '" + mDefaultValue + "'";
                                            }
                                            break;
                                        case "system.decimal":
                                            {
                                                mColumnString = mColumnName + " DOUBLE(16,2)";
                                                if (mIsNullable == true)
                                                {
                                                    mColumnString = mColumnString + " NULL";
                                                }
                                                else
                                                {
                                                    mColumnString = mColumnString + " NOT NULL";
                                                }

                                                mColumnString = mColumnString + " DEFAULT '0.00'";
                                            }
                                            break;
                                        case "system.double":
                                            {
                                                mColumnString = mColumnName + " DOUBLE(16,2)";
                                                if (mIsNullable == true)
                                                {
                                                    mColumnString = mColumnString + " NULL";
                                                }
                                                else
                                                {
                                                    mColumnString = mColumnString + " NOT NULL";
                                                }

                                                mColumnString = mColumnString + " DEFAULT '0.00'";
                                            }
                                            break;
                                        case "system.int16":
                                            {
                                                mColumnString = mColumnName + " INT(11)";
                                                if (mIsNullable == true)
                                                {
                                                    mColumnString = mColumnString + " NULL";
                                                }
                                                else
                                                {
                                                    mColumnString = mColumnString + " NOT NULL";
                                                }

                                                mColumnString = mColumnString + " DEFAULT '0'";
                                            }
                                            break;
                                        case "system.int32":
                                            {
                                                mColumnString = mColumnName + " INT(11)";
                                                if (mIsNullable == true)
                                                {
                                                    mColumnString = mColumnString + " NULL";
                                                }
                                                else
                                                {
                                                    mColumnString = mColumnString + " NOT NULL";
                                                }

                                                mColumnString = mColumnString + " DEFAULT '0'";
                                            }
                                            break;
                                        case "system.int64":
                                            {
                                                mColumnString = mColumnName + " INT(11)";
                                                if (mIsNullable == true)
                                                {
                                                    mColumnString = mColumnString + " NULL";
                                                }
                                                else
                                                {
                                                    mColumnString = mColumnString + " NOT NULL";
                                                }

                                                mColumnString = mColumnString + " DEFAULT '0'";
                                            }
                                            break;
                                        case "system.datetime":
                                            {
                                                mColumnString = mColumnName + " DATETIME";
                                                if (mIsNullable == true)
                                                {
                                                    mColumnString = mColumnString + " NULL";
                                                }
                                                else
                                                {
                                                    mColumnString = mColumnString + " NOT NULL";
                                                }
                                            }
                                            break;

                                        case "system.byte[]":
                                            {
                                                mColumnString = mColumnName + " LONGBLOB";
                                                if (mIsNullable == true)
                                                {
                                                    mColumnString = mColumnString + " NULL";
                                                }
                                                else
                                                {
                                                    mColumnString = mColumnString + " NOT NULL";
                                                }
                                            }
                                            break;

                                        //default:
                                        //    {
                                        //        mColumnString = mColumnString + mDataType;
                                        //    }
                                        //    break;
                                    }
                                }

                                #endregion

                                if (mColumnsString.Trim() == "")
                                {
                                    mColumnsString = "ADD " + mColumnString;
                                }
                                else
                                {
                                    mColumnsString = mColumnsString + "," + Environment.NewLine + "ADD " + mColumnString;
                                }
                            }
                        }

                        if (mColumnsString.Trim() != "")
                        {
                            mScriptAlterTable = mScriptAlterTable + mColumnsString + ";" + Environment.NewLine;
                            mUpdateScript = mUpdateScript + mScriptAlterTable + Environment.NewLine;
                        }
                    }
                }

                #endregion

                //clsGlobal.Write_Error("", "", mUpdateScript);

                return mUpdateScript;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return "";
            }
            finally
            {
                mConnSource.ChangeDatabase(pDefaultDatabase);
                mConnDestination.ChangeDatabase(pDefaultDatabase);
                mConnSource.Close();
                mConnDestination.Close();
            }
        }
        #endregion

        #region Drop_PatientFields
        private void Drop_PatientFields(string mDatabaseName)
        {
            OdbcConnection mConnDestination = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            string mFunctionName = "Drop_PatientFields";

            #region connection
            try
            {
                mConnDestination.ConnectionString = pConnString;
                mConnDestination.Open();
                mConnDestination.ChangeDatabase(mDatabaseName);
                mCommand.Connection = mConnDestination;
            }
            catch (OdbcException ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
            #endregion

            try
            {
                DataTable mDtFields = new DataTable("fields");
                mCommand.CommandText = "select * from patientextrafields";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFields);

                DataTable mDtTables = mConnDestination.GetSchema("Tables");

                foreach (DataRow mTableRow in mDtTables.Rows)
                {
                    string mTableName = mTableRow["table_name"].ToString().Trim();
                    string mFieldName = "";

                    DataTable mDtColumns = new DataTable("columnssource");
                    mCommand.CommandText = "select * from " + mTableName + " where 1=2";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtColumns);

                    #region patients

                    //patientsurname
                    mFieldName = "patientsurname";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //patientothernames
                    mFieldName = "patientothernames";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //patientfirstname
                    mFieldName = "patientfirstname";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //patientgender
                    mFieldName = "patientgender";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //patientbirthdate
                    mFieldName = "patientbirthdate";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //patientregdate
                    mFieldName = "patientregdate";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //patientregion
                    mFieldName = "patientregion";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //patientdistrict
                    mFieldName = "patientdistrict";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //patientward
                    mFieldName = "patientward";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //patientwardhead
                    mFieldName = "patientwardhead";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //patientvillage
                    mFieldName = "patientvillage";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //patientvillagehead
                    mFieldName = "patientvillagehead";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //patientstreet
                    mFieldName = "patientstreet";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //patientethnicity
                    mFieldName = "patientethnicity";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //patientreligion
                    mFieldName = "patientreligion";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //patientoccupation
                    mFieldName = "patientoccupation";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //patientnextofkin
                    mFieldName = "patientnextofkin";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //patientteleno
                    mFieldName = "patientteleno";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    mDtColumns = new DataTable("columnssource");
                    mCommand.CommandText = "select * from " + mTableName + " where 1=2";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtColumns);
                    foreach (DataRow mFieldRow in mDtFields.Rows)
                    {
                        mFieldName = "patient" + mFieldRow["fieldname"].ToString();
                        if (mDtColumns.Columns.Contains(mFieldName) == true)
                        {
                            mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                            mCommand.ExecuteNonQuery();
                        }
                    }

                    #endregion

                    #region rch clients

                    //clientsurname
                    mFieldName = "clientsurname";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //clientothernames
                    mFieldName = "clientothernames";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //clientfirstname
                    mFieldName = "clientfirstname";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //clientgender
                    mFieldName = "clientgender";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //clientbirthdate
                    mFieldName = "clientbirthdate";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //clientregdate
                    mFieldName = "clientregdate";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //clientregion
                    mFieldName = "clientregion";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //clientdistrict
                    mFieldName = "clientdistrict";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //clientward
                    mFieldName = "clientward";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //clientwardhead
                    mFieldName = "clientwardhead";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //clientvillage
                    mFieldName = "clientvillage";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //clientvillagehead
                    mFieldName = "clientvillagehead";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //clientstreet
                    mFieldName = "clientstreet";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //clientethnicity
                    mFieldName = "clientethnicity";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //clientreligion
                    mFieldName = "clientreligion";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //clientoccupation
                    mFieldName = "clientoccupation";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //clientnextofkin
                    mFieldName = "clientnextofkin";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    //clientteleno
                    mFieldName = "clientteleno";
                    if (mDtColumns.Columns.Contains(mFieldName) == true)
                    {
                        mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                        mCommand.ExecuteNonQuery();
                    }

                    mDtColumns = new DataTable("columnssource");
                    mCommand.CommandText = "select * from " + mTableName + " where 1=2";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtColumns);
                    foreach (DataRow mFieldRow in mDtFields.Rows)
                    {
                        mFieldName = "client" + mFieldRow["fieldname"].ToString();
                        if (mDtColumns.Columns.Contains(mFieldName) == true)
                        {
                            mCommand.CommandText = "alter table " + mTableName + " drop " + mFieldName;
                            mCommand.ExecuteNonQuery();
                        }
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, mCommand.CommandText);
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion
    }
}
