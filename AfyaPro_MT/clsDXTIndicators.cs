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
    public class clsDXTIndicators : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsDXTIndicators";

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
                string mCommandText = "select * from dxtindicators";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("dxtindicators");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                #region add group description info

                mDataTable.Columns.Add("groupdescription", typeof(System.String));

                DataTable mDtGroups = new DataTable("dxtindicatorgroups");
                mCommand.CommandText = "select * from dxtindicatorgroups";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtGroups);
                DataView mDvGroups = new DataView();
                mDvGroups.Table = mDtGroups;
                mDvGroups.Sort = "code";

                foreach (DataRow mDataRow in mDataTable.Rows)
                {
                    string mGroupDescription = "";

                    int mRowIndex = mDvGroups.Find(mDataRow["groupcode"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mGroupDescription = mDvGroups[mRowIndex]["description"].ToString().Trim();
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

        #region View_Mapping
        public DataTable View_Mapping(String mFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_Mapping";

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
                string mCommandText = "select * from dxtindicatordiagnoses";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("dxtindicatordiagnoses");
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

        #region Add
        public AfyaPro_Types.clsResult Add(String mCode, String mDescription, String mGroupCode, string mUserId)
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.dxtindicators_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region check 4 duplicate
            try
            {
                mCommand.CommandText = "select * from dxtindicators where code='"
                + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DxIndicatorCodeIsInUse.ToString();
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

                //execute statements
                mCommand.CommandText = "insert into dxtindicators(code,description,groupcode) values('"
                + mCode.Trim() + "','" + mDescription.Trim() + "','" + mGroupCode.Trim() + "')";
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

        #region Edit
        public AfyaPro_Types.clsResult Edit(String mCode, String mDescription, string mGroupCode, string mUserId)
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.dxtindicators_edit.ToString(), mUserId);
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
                mCommand.CommandText = "select * from dxtindicators where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DxIndicatorCodeDoesNotExist.ToString();
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

                mCommand.CommandText = "update dxtindicators set description = '"
                + mDescription.Trim() + "',groupcode='" + mGroupCode.Trim() + "' where code='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.dxtindicators_delete.ToString(), mUserId);
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
                mCommand.CommandText = "select * from dxtindicators where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DxIndicatorCodeDoesNotExist.ToString();
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

                mCommand.CommandText = "delete from dxtindicators where code='" + mCode.Trim() + "'";
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

        #region Diagnoses_Mapping
        public AfyaPro_Types.clsResult Diagnoses_Mapping(string mIndicatorCode, DataTable mDtDiagnoses, string mUserId)
        {
            String mFunctionName = "Diagnoses_Mapping";

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

            #region check for code existance

            try
            {
                mCommand.CommandText = "select * from dxtindicators where code='" + mIndicatorCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DxIndicatorCodeDoesNotExist.ToString();
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

            #region do stuff
            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                mCommand.CommandText = "delete from dxtindicatordiagnoses where indicatorcode='" + mIndicatorCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtDiagnoses.Rows)
                {
                    mCommand.CommandText = "insert into dxtindicatordiagnoses(indicatorcode,diagnosiscode) "
                    + "values('" + mIndicatorCode.Trim() + "','" + mDataRow["code"].ToString().Trim() + "')";
                    mCommand.ExecuteNonQuery();
                }

                #region synchronize

                mCommand.CommandText = "update dxtpatientdiagnoseslog set indicatorcode='' where indicatorcode='" + mIndicatorCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtDiagnoses.Rows)
                {
                    mCommand.CommandText = "update dxtpatientdiagnoseslog set indicatorcode='"
                    + mIndicatorCode.Trim() + "' where diagnosiscode='" + mDataRow["code"].ToString().Trim() + "'";
                    mCommand.ExecuteNonQuery();
                }

                #endregion

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

        #region Synchronize_Mappings
        public AfyaPro_Types.clsResult Synchronize_Mappings(string mUserId)
        {
            String mFunctionName = "Synchronize_Mappings";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
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

            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                DataTable mDtIndicatorDiagnoses = new DataTable("indicatordiagnoses");
                mCommand.CommandText = "select * from dxtindicatordiagnoses";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtIndicatorDiagnoses);

                foreach (DataRow mDataRow in mDtIndicatorDiagnoses.Rows)
                {
                    string mIndicatorCode = mDataRow["indicatorcode"].ToString().Trim();
                    string mDiagnosisCode = mDataRow["diagnosiscode"].ToString().Trim();

                    mCommand.CommandText = "update dxtpatientdiagnoseslog set indicatorcode='" 
                    + mIndicatorCode.Trim() + "' where diagnosiscode='" + mDiagnosisCode + "'";
                    mCommand.ExecuteNonQuery();
                }

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
        }
        #endregion
    }
}
