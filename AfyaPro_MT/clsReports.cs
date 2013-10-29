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
using System.Data;
using System.Data.Odbc;
using System.Linq;

namespace AfyaPro_MT
{
    public class clsReports : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsReports";

        #endregion

        #region View
        public DataTable View(String mFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View";

            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            #region database connection

            try
            {
                mConn.ConnectionString = clsGlobal.gAfyaConStr;

                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                mCommand.Connection = mConn;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }

            #endregion

            try
            {
                string mCommandText = "select * from sys_reports";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("sys_reports");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                #region add group description info

                mDataTable.Columns.Add("groupdescription", typeof(System.String));

                DataTable mDtReportGroups = new DataTable("sys_reportgroups");
                mCommand.CommandText = "select * from sys_reportgroups";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtReportGroups);
                DataView mDvReportGroups = new DataView();
                mDvReportGroups.Table = mDtReportGroups;
                mDvReportGroups.Sort = "code";

                foreach (DataRow mDataRow in mDataTable.Rows)
                {
                    string mGroupDescription = "";

                    int mRowIndex = mDvReportGroups.Find(mDataRow["groupcode"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mGroupDescription = mDvReportGroups[mRowIndex]["description"].ToString().Trim();
                    }

                    mDataRow["groupdescription"] = mGroupDescription;
                    mDataRow.EndEdit();
                    mDataTable.AcceptChanges();
                }

                #endregion

                #region column headers

                if (mLanguageName.Trim() != "" && mGridName.Trim() != "")
                {
                    try
                    {
                        var mCurrLang = from lang in System.Xml.Linq.XElement.Load(
                            AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Lang\" + mLanguageName + ".xml").Elements(mGridName)
                                        select lang;

                        foreach (var mElement in mCurrLang)
                        {
                            if (mDataTable.Columns.Contains((string)mElement.Element("controlname").Value.Trim().Substring(3)) == true)
                            {
                                mDataTable.Columns[(string)mElement.Element("controlname").Value.Trim().Substring(3)].Caption =
                                    (string)mElement.Element("description").Value.Trim();
                            }
                        }
                    }
                    catch { }
                }

                #endregion

                return mDataTable;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
            finally
            {
                mConn.Close();
            }
        }
        #endregion

        #region View_Parameters
        public DataTable View_Parameters(String mFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_Parameters";

            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            #region database connection

            try
            {
                mConn.ConnectionString = clsGlobal.gAfyaConStr;

                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                mCommand.Connection = mConn;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }

            #endregion

            try
            {
                string mCommandText = "select * from sys_reportparameters";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("sys_reportparameters");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                #region column headers

                if (mLanguageName.Trim() != "" && mGridName.Trim() != "")
                {
                    try
                    {
                        var mCurrLang = from lang in System.Xml.Linq.XElement.Load(
                            AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Lang\" + mLanguageName + ".xml").Elements(mGridName)
                                        select lang;

                        foreach (var mElement in mCurrLang)
                        {
                            if (mDataTable.Columns.Contains((string)mElement.Element("controlname").Value.Trim().Substring(3)) == true)
                            {
                                mDataTable.Columns[(string)mElement.Element("controlname").Value.Trim().Substring(3)].Caption =
                                    (string)mElement.Element("description").Value.Trim();
                            }
                        }
                    }
                    catch { }
                }

                #endregion

                return mDataTable;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
            finally
            {
                mConn.Close();
            }
        }
        #endregion

        #region View_FilterConditions
        public DataTable View_FilterConditions(String mFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_FilterConditions";

            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            #region database connection

            try
            {
                mConn.ConnectionString = clsGlobal.gAfyaConStr;

                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                mCommand.Connection = mConn;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }

            #endregion

            try
            {
                string mCommandText = "select * from sys_reportfilterconditions";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("sys_reportfilterconditions");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                #region column headers

                if (mLanguageName.Trim() != "" && mGridName.Trim() != "")
                {
                    try
                    {
                        var mCurrLang = from lang in System.Xml.Linq.XElement.Load(
                            AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Lang\" + mLanguageName + ".xml").Elements(mGridName)
                                        select lang;

                        foreach (var mElement in mCurrLang)
                        {
                            if (mDataTable.Columns.Contains((string)mElement.Element("controlname").Value.Trim().Substring(3)) == true)
                            {
                                mDataTable.Columns[(string)mElement.Element("controlname").Value.Trim().Substring(3)].Caption =
                                    (string)mElement.Element("description").Value.Trim();
                            }
                        }
                    }
                    catch { }
                }

                #endregion

                return mDataTable;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
            finally
            {
                mConn.Close();
            }
        }
        #endregion

        #region Get_LookupTables
        public DataTable Get_LookupTables()
        {
            String mFunctionName = "Get_LookupTables";

            try
            {
                DataTable mDataTable = new DataTable("lookuptables");
                mDataTable.Columns.Add("description", typeof(System.String));

                DataRow mNewRow = mDataTable.NewRow();
                mNewRow["description"] = "complexions";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["description"] = "bloodgroups";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["description"] = "eyecolors";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["description"] = "ethnicities";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["description"] = "religions";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["description"] = "hair";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["description"] = "maritalstatus";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["description"] = "idtypes";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["description"] = "countries";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["description"] = "regions";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["description"] = "districts";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["description"] = "wards";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["description"] = "wardheads";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["description"] = "villages";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["description"] = "villageheads";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["description"] = "streets";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["description"] = "facilitycorporates";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["description"] = "facilitycorporatesubgroups";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["description"] = "facilitydischargestatus";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["description"] = "facilitytreatmentpoints";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["description"] = "facilitywards";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["description"] = "facilitywardrooms";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mDataTable.RemotingFormat = SerializationFormat.Binary;

                return mDataTable;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
        }
        #endregion

        #region Get_LookupTableColumns
        public DataTable Get_LookupTableColumns(string mTableName)
        {
            String mFunctionName = "Get_LookupTableColumns";

            try
            {
                DataTable mDataTable = new DataTable("lookupcolumns");
                mDataTable.Columns.Add("description", typeof(System.String));

                DataRow mNewRow;

                switch (mTableName.Trim().ToLower())
                {
                    case "complexions":
                        {
                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "description";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();
                        }
                        break;
                    case "bloodgroups":
                        {
                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "description";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();
                        }
                        break;
                    case "eyecolors":
                        {
                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "description";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();
                        }
                        break;
                    case "ethnicities":
                        {
                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "description";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();
                        }
                        break;
                    case "religions":
                        {
                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "description";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();
                        }
                        break;
                    case "hair":
                        {
                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "description";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();
                        }
                        break;
                    case "maritalstatus":
                        {
                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "description";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();
                        }
                        break;
                    case "streets":
                        {
                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "description";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();
                        }
                        break;
                    case "idtypes":
                        {
                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "description";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();
                        }
                        break;
                    case "countries":
                        {
                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "code";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();

                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "description";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();
                        }
                        break;
                    case "regions":
                        {
                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "code";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();

                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "description";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();
                        }
                        break;
                    case "districts":
                        {
                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "code";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();

                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "description";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();
                        }
                        break;
                    case "facilitycorporates":
                        {
                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "code";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();

                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "description";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();
                        }
                        break;
                    case "facilitycorporatesubgroups":
                        {
                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "code";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();

                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "description";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();
                        }
                        break;
                    case "facilitydischargestatus":
                        {
                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "code";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();

                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "description";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();
                        }
                        break;
                    case "facilitytreatmentpoints":
                        {
                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "code";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();

                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "description";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();
                        }
                        break;
                    case "facilitywards":
                        {
                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "code";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();

                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "description";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();
                        }
                        break;
                    case "facilitywardrooms":
                        {
                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "code";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();

                            mNewRow = mDataTable.NewRow();
                            mNewRow["description"] = "description";
                            mDataTable.Rows.Add(mNewRow);
                            mDataTable.AcceptChanges();
                        }
                        break;
                }


                mDataTable.RemotingFormat = SerializationFormat.Binary;

                return mDataTable;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
        }
        #endregion

        #region Edit
        public AfyaPro_Types.clsResult Edit(String mCode, String mDescription, String mGroupCode, string mUserId)
        {
            String mFunctionName = "Edit";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;

            #region database connection

            try
            {
                mConn.ConnectionString = clsGlobal.gAfyaConStr;

                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                mCommand.Connection = mConn;
            }
            catch (Exception ex)
            {
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.rpdreports_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region check for code existance

            try
            {
                mCommand.CommandText = "select * from sys_reports where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.RPD_ReportCodeDoesNotExist.ToString();
                    return mResult;
                }
            }
            catch (OdbcException ex)
            {
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }
            finally
            {
                mDataReader.Close();
            }

            #endregion

            #region do edit
            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //sys_reports
                mCommand.CommandText = "update sys_reports set description = '" + mDescription.Trim() 
                + "',groupcode='" + mGroupCode.Trim() + "' where code='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                mTrans.Commit();

                mResult.GeneratedCode = mCode;

                //return
                return mResult;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Add
        public AfyaPro_Types.clsResult Add(Int16 mGenerateCode, String mCode, String mDescription, 
            String mGroupCode, DataTable mDtParameters, DataTable mDtFilterConditions, string mTableName,
            string mCommandText, string mFilterString, string mGroupByFields, string mUserId)
        {
            String mFunctionName = "Add";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;

            #region database connection

            try
            {
                mConn.ConnectionString = clsGlobal.gAfyaConStr;

                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                mCommand.Connection = mConn;
            }
            catch (Exception ex)
            {
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.rpdreports_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region auto generate code, if option is on

            if (mGenerateCode == 1)
            {
                clsAutoCodes objAutoCodes = new clsAutoCodes();
                AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                mObjCode = objAutoCodes.Next_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.reportcode), "sys_reports", "code");
                if (mObjCode.Exe_Result == -1)
                {
                    mResult.Exe_Result = mObjCode.Exe_Result;
                    mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                    return mResult;
                }
                mCode = mObjCode.GeneratedCode;
            }

            #endregion

            #region check 4 duplicate
            try
            {
                mCommand.CommandText = "select * from sys_reports where code='"
                + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.RPD_ReportCodeIsInUse.ToString();
                    return mResult;
                }
            }
            catch (OdbcException ex)
            {
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion

            #region add
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //sys_reports
                mCommand.CommandText = "insert into sys_reports(code,description,groupcode,commandtext,"
                + "filterstring,tablename,groupbyfields) values('"
                + mCode.Trim() + "','" + mDescription.Trim() + "','" + mGroupCode.Trim() + "','" + mCommandText.Trim()
                + "','" + mFilterString.Trim() + "','" + mTableName.Trim() + "','" + mGroupByFields.Trim() + "')";
                mCommand.ExecuteNonQuery();

                //sys_reportparameters
                mCommand.CommandText = "delete from sys_reportparameters where reportcode='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();
                foreach (DataRow mDataRow in mDtParameters.Rows)
                {
                    string mParameterCode = mDataRow["parametercode"].ToString().Trim();
                    string mParameterDescription = mDataRow["parameterdescription"].ToString().Trim();
                    string mParameterType = mDataRow["parametertype"].ToString().Trim();
                    string mParameterControl = mDataRow["parametercontrol"].ToString().Trim();
                    int mParameterSaveValue = Convert.ToInt16(mDataRow["parametersavevalue"]);
                    string mLookupTableName = mDataRow["lookuptablename"].ToString().Trim();
                    string mValueString = mDataRow["valuestring"].ToString().Trim();
                    string mValueDateTime =clsGlobal.Saving_DateValueNullable(mDataRow["valuedatetime"]);
                    int mValueInt = Convert.ToInt32(mDataRow["valueint"]);
                    double mValueDouble = Convert.ToDouble(mDataRow["valuedouble"]);

                    mCommand.CommandText = "insert into sys_reportparameters(reportcode,parametercode,parameterdescription,"
                    + "parametertype,parametercontrol,parametersavevalue,lookuptablename,"
                    + "valuestring,valuedatetime,valueint,valuedouble) values('" + mCode.Trim() + "','" + mParameterCode 
                    + "','" + mParameterDescription + "','" + mParameterType + "','" + mParameterControl + "',"
                    + mParameterSaveValue + ",'" + mLookupTableName + "','"
                    + mValueString + "'," + mValueDateTime + "," + mValueInt + "," + mValueDouble + ")";
                    mCommand.ExecuteNonQuery();
                }

                //sys_reportfilterconditions
                mCommand.CommandText = "delete from sys_reportfilterconditions where reportcode='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();
                foreach (DataRow mDataRow in mDtFilterConditions.Rows)
                {
                    string mConditionFieldName = mDataRow["conditionfieldname"].ToString().Trim();
                    string mConditionOperator = mDataRow["conditionoperator"].ToString().Trim();
                    string mConditionValue1 = mDataRow["conditionvalue1"].ToString().Trim();
                    string mConditionValue2 = mDataRow["conditionvalue2"].ToString().Trim();

                    mCommand.CommandText = "insert into sys_reportfilterconditions(reportcode,conditionfieldname,"
                    + "conditionoperator,conditionvalue1,conditionvalue2) values('" + mCode.Trim() + "','"
                    + mConditionFieldName + "','" + mConditionOperator + "','" + mConditionValue1 + "','" + mConditionValue2 + "')";
                    mCommand.ExecuteNonQuery();
                }

                if (mGenerateCode == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.reportcode);
                    mCommand.ExecuteNonQuery();
                }

                //commit
                mTrans.Commit();

                mResult.GeneratedCode = mCode;

                //return
                return mResult;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Edit
        public AfyaPro_Types.clsResult Edit(String mCode, String mDescription, 
            String mGroupCode, DataTable mDtParameters, DataTable mDtFilterConditions, string mTableName,
            string mCommandText, string mFilterString, string mGroupByFields, string mUserId)
        {
            String mFunctionName = "Edit";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;

            #region database connection

            try
            {
                mConn.ConnectionString = clsGlobal.gAfyaConStr;

                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                mCommand.Connection = mConn;
            }
            catch (Exception ex)
            {
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.rpdreports_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region check for code existance

            try
            {
                mCommand.CommandText = "select * from sys_reports where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.RPD_ReportCodeDoesNotExist.ToString();
                    return mResult;
                }
            }
            catch (OdbcException ex)
            {
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }
            finally
            {
                mDataReader.Close();
            }

            #endregion

            #region do edit
            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //sys_reports
                mCommand.CommandText = "update sys_reports set description = '"
                + mDescription.Trim() + "',groupcode='" + mGroupCode.Trim() + "',commandtext='" + mCommandText.Trim() + "',filterstring='"
                + mFilterString.Trim() + "',tablename='" + mTableName.Trim() + "',groupbyfields='"
                + mGroupByFields.Trim() + "' where code='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                //sys_reportparameters
                mCommand.CommandText = "delete from sys_reportparameters where reportcode='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();
                foreach (DataRow mDataRow in mDtParameters.Rows)
                {
                    string mParameterCode = mDataRow["parametercode"].ToString().Trim();
                    string mParameterDescription = mDataRow["parameterdescription"].ToString().Trim();
                    string mParameterType = mDataRow["parametertype"].ToString().Trim();
                    string mParameterControl = mDataRow["parametercontrol"].ToString().Trim();
                    int mParameterSaveValue = Convert.ToInt16(mDataRow["parametersavevalue"]);
                    string mLookupTableName = mDataRow["lookuptablename"].ToString().Trim();
                    string mValueString = mDataRow["valuestring"].ToString().Trim();
                    string mValueDateTime = clsGlobal.Saving_DateValueNullable(mDataRow["valuedatetime"]);
                    int mValueInt = Convert.ToInt32(mDataRow["valueint"]);
                    double mValueDouble = Convert.ToDouble(mDataRow["valuedouble"]);

                    mCommand.CommandText = "insert into sys_reportparameters(reportcode,parametercode,parameterdescription,"
                    + "parametertype,parametercontrol,parametersavevalue,lookuptablename,"
                    + "valuestring,valuedatetime,valueint,valuedouble) values('" + mCode.Trim() + "','" + mParameterCode
                    + "','" + mParameterDescription + "','" + mParameterType + "','" + mParameterControl + "',"
                    + mParameterSaveValue + ",'" + mLookupTableName + "','"
                    + mValueString + "'," + mValueDateTime + "," + mValueInt + "," + mValueDouble + ")";
                    mCommand.ExecuteNonQuery();
                }

                //sys_reportfilterconditions
                mCommand.CommandText = "delete from sys_reportfilterconditions where reportcode='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();
                foreach (DataRow mDataRow in mDtFilterConditions.Rows)
                {
                    string mConditionFieldName = mDataRow["conditionfieldname"].ToString().Trim();
                    string mConditionOperator = mDataRow["conditionoperator"].ToString().Trim();
                    string mConditionValue1 = mDataRow["conditionvalue1"].ToString().Trim();
                    string mConditionValue2 = mDataRow["conditionvalue2"].ToString().Trim();

                    mCommand.CommandText = "insert into sys_reportfilterconditions(reportcode,conditionfieldname,"
                    + "conditionoperator,conditionvalue1,conditionvalue2) values('" + mCode.Trim() + "','"
                    + mConditionFieldName + "','" + mConditionOperator + "','" + mConditionValue1 + "','" + mConditionValue2 + "')";
                    mCommand.ExecuteNonQuery();
                }

                mTrans.Commit();

                mResult.GeneratedCode = mCode;

                //return
                return mResult;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Delete
        public AfyaPro_Types.clsResult Delete(String mCode, string mUserId)
        {
            String mFunctionName = "Delete";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;

            #region database connection

            try
            {
                mConn.ConnectionString = clsGlobal.gAfyaConStr;

                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                mCommand.Connection = mConn;
            }
            catch (Exception ex)
            {
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.rpdreports_delete.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region check 4 existance
            try
            {
                mCommand.CommandText = "select * from sys_reports where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.RPD_ReportCodeDoesNotExist.ToString();
                    return mResult;
                }
            }
            catch (OdbcException ex)
            {
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion

            #region do delete
            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                mCommand.CommandText = "delete from sys_reports where code='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                mCommand.CommandText = "delete from sys_reportparameters where reportcode='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                mCommand.CommandText = "delete from sys_reportfilterconditions where reportcode='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                //commit
                mTrans.Commit();

                //return
                return mResult;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion
    }
}
