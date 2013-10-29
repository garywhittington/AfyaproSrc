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
    public class clsPatientExtraFields : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsPatientExtraFields";

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
                string mCommandText = "select * from patientextrafields";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("patientextrafields");
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
                                    (string)mElement.Element("fieldcaption").Value.Trim();
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

        #region View_Lookup
        public DataTable View_Lookup(String mFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_Lookup";

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
                string mCommandText = "select * from patientextrafieldlookup";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("patientextrafieldlookup");
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
                                    (string)mElement.Element("fieldcaption").Value.Trim();
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

        #region View_Surnames
        public DataTable View_Surnames(String mFilter, String mOrder)
        {
            String mFunctionName = "View_Surnames";

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
                string mCommandText = "select * from patientsurnames";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("patientsurnames");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

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

        #region View_Firstnames
        public DataTable View_Firstnames(String mFilter, String mOrder)
        {
            String mFunctionName = "View_Firstnames";

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
                string mCommandText = "select * from patientfirstnames";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("patientfirstnames");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

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

        #region View_Othernames
        public DataTable View_Othernames(String mFilter, String mOrder)
        {
            String mFunctionName = "View_Othernames";

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
                string mCommandText = "select * from patientothernames";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("patientothernames");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

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
        public AfyaPro_Types.clsResult Add(string mFieldName, string mFieldCaption, string mFieldType, 
            string mDataType, string mDefaultValue, string mFilterOnValueFrom, int mAllowInput, int mRestrictDropDownList, int mRememberEntries,
            int mCharacterCasingOption, int mCompulsory, string mErrorOnEmpty, string mErrorOnInvalidInput, DataTable mDtDropDownValues, string mUserId)
        {
            String mFunctionName = "Add";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
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

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.genpatientextrafields_add.ToString(), mUserId);
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
                mCommand.CommandText = "select * from patientextrafields where fieldname='"
                + mFieldName.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GEN_PatientFieldNameIsInUse.ToString();
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

                //patientextrafields
                List<clsDataField> mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("fieldname", DataTypes.dbstring.ToString(), mFieldName.Trim()));
                mDataFields.Add(new clsDataField("fieldcaption", DataTypes.dbstring.ToString(), mFieldCaption.Trim()));
                mDataFields.Add(new clsDataField("fieldtype", DataTypes.dbstring.ToString(), mFieldType.Trim()));
                mDataFields.Add(new clsDataField("datatype", DataTypes.dbstring.ToString(), mDataType.Trim()));
                mDataFields.Add(new clsDataField("defaultvalue", DataTypes.dbstring.ToString(), mDefaultValue.Trim()));
                mDataFields.Add(new clsDataField("filteronvaluefrom", DataTypes.dbstring.ToString(), mFilterOnValueFrom.Trim()));
                mDataFields.Add(new clsDataField("allowinput", DataTypes.dbnumber.ToString(), mAllowInput));
                mDataFields.Add(new clsDataField("rememberentries", DataTypes.dbnumber.ToString(), mRememberEntries));
                mDataFields.Add(new clsDataField("charactercasingoption", DataTypes.dbnumber.ToString(), mCharacterCasingOption));
                mDataFields.Add(new clsDataField("compulsory", DataTypes.dbnumber.ToString(), mCompulsory));
                mDataFields.Add(new clsDataField("erroronempty", DataTypes.dbstring.ToString(), mErrorOnEmpty));
                mDataFields.Add(new clsDataField("restricttodropdownlist", DataTypes.dbnumber.ToString(), mRestrictDropDownList));
                mDataFields.Add(new clsDataField("erroroninvalidinput", DataTypes.dbstring.ToString(), mErrorOnInvalidInput));

                mCommand.CommandText = clsGlobal.Get_InsertStatement("patientextrafields", mDataFields);
                mCommand.ExecuteNonQuery();

                #region dropdown list

                if (mFieldType.Trim().ToLower() == "dropdown")
                {
                    mCommand.CommandText = clsGlobal.Get_DeleteStatement("patientextrafieldlookup", "fieldname='" + mFieldName.Trim() + "'");
                    mCommand.ExecuteNonQuery();

                    foreach (DataRow mDataRow in mDtDropDownValues.Rows)
                    {
                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("fieldname", DataTypes.dbstring.ToString(), mFieldName.Trim()));
                        mDataFields.Add(new clsDataField("description", DataTypes.dbstring.ToString(), mDataRow["description"].ToString().Trim()));
                        mDataFields.Add(new clsDataField("filtervalue", DataTypes.dbstring.ToString(), mDataRow["filtervalue"].ToString().Trim()));

                        mCommand.CommandText = clsGlobal.Get_InsertStatement("patientextrafieldlookup", mDataFields);
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                //patients
                DataTable mDtColumns = new DataTable("columns");
                mCommand.CommandText = "select * from patients where 1=2";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtColumns);

                if (mDtColumns.Columns.Contains(mFieldName) == false)
                {
                    mCommand.CommandText = "ALTER TABLE patients ADD " + clsGlobal.Build_ColumnForAlter(mFieldName, mDataType);
                    mCommand.ExecuteNonQuery();
                }

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
        public AfyaPro_Types.clsResult Edit(string mFieldName, string mFieldCaption, string mFieldType,
            string mDataType, string mDefaultValue, string mFilterOnValueFrom, int mAllowInput, int mRestrictDropDownList, int mRememberEntries,
            int mCharacterCasingOption, int mCompulsory, string mErrorOnEmpty, string mErrorOnInvalidInput, DataTable mDtDropDownValues, string mUserId)
        {
            String mFunctionName = "Edit";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
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

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.genpatientextrafields_edit.ToString(), mUserId);
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
                mCommand.CommandText = "select * from patientextrafields where fieldname='"
                + mFieldName.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GEN_PatientFieldNameDoesNotExist.ToString();
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

            #region edit
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //patientextrafields
                List<clsDataField> mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("fieldcaption", DataTypes.dbstring.ToString(), mFieldCaption.Trim()));
                mDataFields.Add(new clsDataField("fieldtype", DataTypes.dbstring.ToString(), mFieldType.Trim()));
                mDataFields.Add(new clsDataField("datatype", DataTypes.dbstring.ToString(), mDataType.Trim()));
                mDataFields.Add(new clsDataField("defaultvalue", DataTypes.dbstring.ToString(), mDefaultValue.Trim()));
                mDataFields.Add(new clsDataField("filteronvaluefrom", DataTypes.dbstring.ToString(), mFilterOnValueFrom.Trim()));
                mDataFields.Add(new clsDataField("allowinput", DataTypes.dbnumber.ToString(), mAllowInput));
                mDataFields.Add(new clsDataField("rememberentries", DataTypes.dbnumber.ToString(), mRememberEntries));
                mDataFields.Add(new clsDataField("charactercasingoption", DataTypes.dbnumber.ToString(), mCharacterCasingOption));
                mDataFields.Add(new clsDataField("compulsory", DataTypes.dbnumber.ToString(), mCompulsory));
                mDataFields.Add(new clsDataField("erroronempty", DataTypes.dbstring.ToString(), mErrorOnEmpty));
                mDataFields.Add(new clsDataField("restricttodropdownlist", DataTypes.dbnumber.ToString(), mRestrictDropDownList));
                mDataFields.Add(new clsDataField("erroroninvalidinput", DataTypes.dbstring.ToString(), mErrorOnInvalidInput));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("patientextrafields", mDataFields, "fieldname='" + mFieldName.Trim() + "'");
                mCommand.ExecuteNonQuery();

                #region dropdown list

                if (mFieldType.Trim().ToLower() == "dropdown")
                {
                    mCommand.CommandText = clsGlobal.Get_DeleteStatement("patientextrafieldlookup", "fieldname='" + mFieldName.Trim() + "'");
                    mCommand.ExecuteNonQuery();

                    foreach (DataRow mDataRow in mDtDropDownValues.Rows)
                    {
                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("fieldname", DataTypes.dbstring.ToString(), mFieldName.Trim()));
                        mDataFields.Add(new clsDataField("description", DataTypes.dbstring.ToString(), mDataRow["description"].ToString().Trim()));
                        mDataFields.Add(new clsDataField("filtervalue", DataTypes.dbstring.ToString(), mDataRow["filtervalue"].ToString().Trim()));

                        mCommand.CommandText = clsGlobal.Get_InsertStatement("patientextrafieldlookup", mDataFields);
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                //patients
                DataTable mDtColumns = new DataTable("columns");
                mCommand.CommandText = "select * from patients where 1=2";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtColumns);

                if (mDtColumns.Columns.Contains(mFieldName) == false)
                {
                    mCommand.CommandText = "ALTER TABLE patients ADD " + clsGlobal.Build_ColumnForAlter(mFieldName, mDataType);
                    mCommand.ExecuteNonQuery();
                }

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

        #region Delete
        public AfyaPro_Types.clsResult Delete(String mFieldName, string mUserId)
        {
            String mFunctionName = "Delete";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
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

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.genpatientextrafields_delete.ToString(), mUserId);
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
                mCommand.CommandText = "select * from patientextrafields where fieldname='" + mFieldName.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GEN_PatientFieldNameDoesNotExist.ToString();
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

                mCommand.CommandText = "delete from patientextrafields where fieldname='" + mFieldName.Trim() + "'";
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
