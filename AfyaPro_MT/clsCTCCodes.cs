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
    public class clsCTCCodes : MarshalByRefObject
    {

        #region declaration

        private static String pClassName = "AfyaPro_MT.clsCTCCodes";

        #endregion

        #region View
        public DataTable View(String mFilter, String mOrder, int mCTCCode, string mLanguageName, string mGridName)
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
                string mTableName = "";

                #region determine table name

                if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.MaritalStatus)
                {
                    mTableName = "maritalstatus";
                }
                if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.FunctionalStatus)
                {
                    mTableName = "ctc_functionalstatus";
                }
                if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.TBStatus)
                {
                    mTableName = "ctc_tbstatus";
                }
                if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVStatus)
                {
                    mTableName = "ctc_arvstatus";
                }
                if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.AIDSIllness)
                {
                    mTableName = "ctc_aidsillness";
                }
                if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.MaritalStatus)
                {
                    mTableName = "ctc_maritalstatus";
                }
                if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVCombRegimens)
                {
                    mTableName = "ctc_arvcombregimens";
                }
                if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVAdherence)
                {
                    mTableName = "ctc_arvadherence";
                }
                if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVPoorAdherenceReasons)
                {
                    mTableName = "ctc_arvpooradherencereasons";
                }
                if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ReferedTo)
                {
                    mTableName = "ctc_referedto";
                }
                if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVReasons)
                {
                    mTableName = "ctc_arvreason";
                }
                if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.FollowUpStatus)
                {
                    mTableName = "ctc_followupstatus";
                }
                if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ReferedFrom)
                {
                    mTableName = "ctc_referedfrom";
                }
                if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.PriorARVExposure)
                {
                    mTableName = "ctc_priorarvexposure";
                }
                if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARTWhyEligible)
                {
                    mTableName = "ctc_artwhyeligible";
                }
                if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.RelevantComeds)
                {
                    mTableName = "ctc_relevantcomeds";
                }
                if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.abnormallabresults)
                {
                    mTableName = "ctc_abnormallabresults";
                }

                #endregion

                string mCommandText = "select * from " + mTableName;

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable(mTableName);
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
        public AfyaPro_Types.clsResult Add(String mCode, String mDescription, int mCTCCode, string mCategory, string mUserId)
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.lab_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            string mTableName = "";

            #region determine table name

            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.MaritalStatus)
            {
                mTableName = "maritalstatus";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.FunctionalStatus)
            {
                mTableName = "ctc_functionalstatus";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.TBStatus)
            {
                mTableName = "ctc_tbstatus";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.AIDSIllness)
            {
                mTableName = "ctc_aidsillness";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.MaritalStatus)
            {
                mTableName = "ctc_maritalstatus";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVCombRegimens)
            {
                mTableName = "ctc_arvcombregimens";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVAdherence)
            {
                mTableName = "ctc_arvadherence";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVPoorAdherenceReasons)
            {
                mTableName = "ctc_arvpooradherencereasons";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ReferedTo)
            {
                mTableName = "ctc_referedto";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVReasons)
            {
                mTableName = "ctc_arvreason";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.FollowUpStatus)
            {
                mTableName = "ctc_followupstatus";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ReferedFrom)
            {
                mTableName = "ctc_referedfrom";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.PriorARVExposure)
            {
                mTableName = "ctc_priorarvexposure";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARTWhyEligible)
            {
                mTableName = "ctc_artwhyeligible";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.RelevantComeds)
            {
                mTableName = "ctc_relevantcomeds";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.abnormallabresults)
            {
                mTableName = "ctc_abnormallabresults";
            }

            #endregion

            #region check 4 duplicate
            try
            {
                mCommand.CommandText = "select * from " + mTableName + " where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = "Duplicate code is not allowed";
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

                //facilitylaboratories
                List<clsDataField> mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("code", DataTypes.dbstring.ToString(), mCode.Trim()));
                mDataFields.Add(new clsDataField("description", DataTypes.dbstring.ToString(), mDescription.Trim()));
                if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVCombRegimens
                    || mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVReasons)
                {
                    mDataFields.Add(new clsDataField("category", DataTypes.dbstring.ToString(), mCategory.Trim()));
                }

                mCommand.CommandText = clsGlobal.Get_InsertStatement(mTableName, mDataFields);
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
        public AfyaPro_Types.clsResult Edit(String mCode, String mDescription, int mCTCCode, string mCategory, string mUserId)
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.lab_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            string mTableName = "";

            #region determine table name

            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.MaritalStatus)
            {
                mTableName = "maritalstatus";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.FunctionalStatus)
            {
                mTableName = "ctc_functionalstatus";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.TBStatus)
            {
                mTableName = "ctc_tbstatus";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.AIDSIllness)
            {
                mTableName = "ctc_aidsillness";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.MaritalStatus)
            {
                mTableName = "ctc_maritalstatus";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVCombRegimens)
            {
                mTableName = "ctc_arvcombregimens";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVAdherence)
            {
                mTableName = "ctc_arvadherence";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVPoorAdherenceReasons)
            {
                mTableName = "ctc_arvpooradherencereasons";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ReferedTo)
            {
                mTableName = "ctc_referedto";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVReasons)
            {
                mTableName = "ctc_arvreason";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.FollowUpStatus)
            {
                mTableName = "ctc_followupstatus";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ReferedFrom)
            {
                mTableName = "ctc_referedfrom";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.PriorARVExposure)
            {
                mTableName = "ctc_priorarvexposure";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARTWhyEligible)
            {
                mTableName = "ctc_artwhyeligible";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.RelevantComeds)
            {
                mTableName = "ctc_relevantcomeds";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.abnormallabresults)
            {
                mTableName = "ctc_abnormallabresults";
            }

            #endregion

            #region check for code existance

            try
            {
                mCommand.CommandText = "select * from " + mTableName + " where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = "Code does not exist";
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

                //facilitylaboratories
                List<clsDataField> mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("description", DataTypes.dbstring.ToString(), mDescription.Trim()));
                if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVCombRegimens
                    || mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVReasons)
                {
                    mDataFields.Add(new clsDataField("category", DataTypes.dbstring.ToString(), mCategory.Trim()));
                }

                mCommand.CommandText = clsGlobal.Get_UpdateStatement(mTableName, mDataFields, "code='" + mCode.Trim() + "'");
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
        public AfyaPro_Types.clsResult Delete(String mCode, int mCTCCode, string mUserId)
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.lab_delete.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            string mTableName = "";

            #region determine table name

            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.MaritalStatus)
            {
                mTableName = "maritalstatus";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.FunctionalStatus)
            {
                mTableName = "ctc_functionalstatus";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.TBStatus)
            {
                mTableName = "ctc_tbstatus";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.AIDSIllness)
            {
                mTableName = "ctc_aidsillness";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.MaritalStatus)
            {
                mTableName = "ctc_maritalstatus";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVCombRegimens)
            {
                mTableName = "ctc_arvcombregimens";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVAdherence)
            {
                mTableName = "ctc_arvadherence";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVPoorAdherenceReasons)
            {
                mTableName = "ctc_arvpooradherencereasons";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ReferedTo)
            {
                mTableName = "ctc_referedto";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVReasons)
            {
                mTableName = "ctc_arvreason";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.FollowUpStatus)
            {
                mTableName = "ctc_followupstatus";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ReferedFrom)
            {
                mTableName = "ctc_referedfrom";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.PriorARVExposure)
            {
                mTableName = "ctc_priorarvexposure";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARTWhyEligible)
            {
                mTableName = "ctc_artwhyeligible";
            }
            if (mCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.RelevantComeds)
            {
                mTableName = "ctc_relevantcomeds";
            }

            #endregion

            #region check 4 existance
            try
            {
                mCommand.CommandText = "select * from " + mTableName + " where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = "Code does not exist";
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

                mCommand.CommandText = clsGlobal.Get_DeleteStatement(mTableName, "code='" + mCode.Trim() + "'");
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

        #region MaritalStatus
        internal DataTable MaritalStatus()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "S";
            mNewRow["description"] = "SINGLE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "M";
            mNewRow["description"] = "MARRIED";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "CO";
            mNewRow["description"] = "COHABITING";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "D";
            mNewRow["description"] = "DIVORCED/DEPARATED";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "W";
            mNewRow["description"] = "WIDOW/WIDOWED";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region FunctionalStatus
        internal DataTable FunctionalStatus()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "W";
            mNewRow["description"] = "WORKING";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "A";
            mNewRow["description"] = "AMBULATORY";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "B";
            mNewRow["description"] = "BED RIDDEN";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region TBStatus
        internal DataTable TBStatus()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "NO";
            mNewRow["description"] = "Not suspected/No signs or symptoms";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "REFER";
            mNewRow["description"] = "TB suspected and referred for evaluation";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "SP";
            mNewRow["description"] = "TB suspected and spulums sample sent or results recorded";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "CONFIRM";
            mNewRow["description"] = "TB confirmed";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "INH";
            mNewRow["description"] = "Currently on INH prophylaxis (IPT)";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "TB RX";
            mNewRow["description"] = "Currently on TB";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "N";
            mNewRow["description"] = "TB Not suspected";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "Y";
            mNewRow["description"] = "TB suspected";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "C";
            mNewRow["description"] = "TB confirmed not (yet) on TB treatment";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "Rx";
            mNewRow["description"] = "TB confirmed and currently taking TB treatment";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region ARVStatus
        internal DataTable ARVStatus()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "1";
            mNewRow["description"] = "NO ARV";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "2";
            mNewRow["description"] = "START ARV";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "3";
            mNewRow["description"] = "CONTINUE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "4";
            mNewRow["description"] = "CHANGE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "5";
            mNewRow["description"] = "STOP";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "6";
            mNewRow["description"] = "RESTART";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region AIDSILLNESS
        internal DataTable AIDSILLNESS()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "AB";
            mNewRow["description"] = "ABDOMINAL PAIN";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "A";
            mNewRow["description"] = "ANAEMIA";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "BN";
            mNewRow["description"] = "BURNING, NUMB, TINGLING";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "CNS";
            mNewRow["description"] = "DIZZY, ANXIETY, NIGHTMARE, DEPRESSION";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "COUGH";
            mNewRow["description"] = "COUGH";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "DB";
            mNewRow["description"] = "DIFFICULT BREATHING";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "DE";
            mNewRow["description"] = "DEMENTIA";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "D";
            mNewRow["description"] = "DIARRHOEA";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "ENC";
            mNewRow["description"] = "HIV ENCEPHALOPATHY";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "FAT";
            mNewRow["description"] = "FAT CHANGES";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "F";
            mNewRow["description"] = "FATIGUE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "FEVER";
            mNewRow["description"] = "FEVER";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "GUD";
            mNewRow["description"] = "GENITAL ULCER DISEASE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "H";
            mNewRow["description"] = "HEADACHE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "HM";
            mNewRow["description"] = "HEMOPTYSIS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "IRIS";
            mNewRow["description"] = "IMM. RECONST. INFLAMM SYNDROME";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "ITC";
            mNewRow["description"] = "ITCHING";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "J";
            mNewRow["description"] = "JAUNDICE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "M";
            mNewRow["description"] = "MOLLUSCUM";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "NTSW";
            mNewRow["description"] = "NIGHT SWEATS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "N";
            mNewRow["description"] = "NAUSEA";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "PE";
            mNewRow["description"] = "PAROTID ENLARGEMENT";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "PID";
            mNewRow["description"] = "PELVIC INFLAMMATORY DISEASE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "P";
            mNewRow["description"] = "PNEUMONIA";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "PPE";
            mNewRow["description"] = "PAPULAR PRURITIC ERUPTIONS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "R";
            mNewRow["description"] = "RASH";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "THRUSH";
            mNewRow["description"] = "THRUSH ORAL/VAGINAL";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "UD";
            mNewRow["description"] = "URETHRAL DISCHARGE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "ULCERS";
            mNewRow["description"] = "ULCERS - MOUTH OR OTHER";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "W";
            mNewRow["description"] = "WEIGHT LOSS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "Z";
            mNewRow["description"] = "ZOSTER";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "CM";
            mNewRow["description"] = "CRYPTOCOCCAL MENINGITIS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "KS";
            mNewRow["description"] = "KAPOSIS SARCOMA";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "OC";
            mNewRow["description"] = "OESOPHAGEAL CANDIDIASIS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "PCP";
            mNewRow["description"] = "PNEUMOCYSTIS PNEUMONIA";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "OTHER";
            mNewRow["description"] = "OTHER";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region ARVCombRegimens
        internal DataTable ARVCombRegimens()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));
            mDtData.Columns.Add("category", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "1a (30)";
            mNewRow["description"] = "d4T, (30), 3TC, NVP";
            mNewRow["category"] = "1ST LINE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "1a (30) S";
            mNewRow["description"] = "d4T, (30), 3TC, NVP starting dose";
            mNewRow["category"] = "1ST LINE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "1a (40)";
            mNewRow["description"] = "d4T, (40), 3TC, NVP";
            mNewRow["category"] = "1ST LINE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "1a (40) S";
            mNewRow["description"] = "d4T, (40), 3TC, NVP starting dose";
            mNewRow["category"] = "1ST LINE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "1b";
            mNewRow["description"] = "ZDV, 3TC, NVP";
            mNewRow["category"] = "1ST LINE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "1c";
            mNewRow["description"] = "ZDV, 3TC, EFV";
            mNewRow["category"] = "1ST LINE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "1d (30)";
            mNewRow["description"] = "d4T (30), 3TC, EFV";
            mNewRow["category"] = "1ST LINE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "1d (40)";
            mNewRow["description"] = "d4T (40), 3TC, EFV";
            mNewRow["category"] = "1ST LINE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "2b";
            mNewRow["description"] = "ABC, ddl, SQV/r";
            mNewRow["category"] = "2ND LINE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "2c";
            mNewRow["description"] = "ABC, ddl, NFV";
            mNewRow["category"] = "2ND LINE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "99";
            mNewRow["description"] = "Other";
            mNewRow["category"] = "2ND LINE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "1P";
            mNewRow["description"] = "1P";
            mNewRow["category"] = "PAEDIATRIC";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "2P";
            mNewRow["description"] = "2P";
            mNewRow["category"] = "PAEDIATRIC";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "3P";
            mNewRow["description"] = "3P";
            mNewRow["category"] = "PAEDIATRIC";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "4P";
            mNewRow["description"] = "4P";
            mNewRow["category"] = "PAEDIATRIC";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "9P";
            mNewRow["description"] = "9P";
            mNewRow["category"] = "PAEDIATRIC";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "1A";
            mNewRow["description"] = "1A";
            mNewRow["category"] = "ADULT";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "2A";
            mNewRow["description"] = "2A";
            mNewRow["category"] = "ADULT";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "3A";
            mNewRow["description"] = "3A";
            mNewRow["category"] = "ADULT";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "4A";
            mNewRow["description"] = "4A";
            mNewRow["category"] = "ADULT";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "5A";
            mNewRow["description"] = "5A";
            mNewRow["category"] = "ADULT";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "6A";
            mNewRow["description"] = "6A";
            mNewRow["category"] = "ADULT";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "7A";
            mNewRow["description"] = "7A";
            mNewRow["category"] = "ADULT";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "8A";
            mNewRow["description"] = "8A";
            mNewRow["category"] = "ADULT";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "Oth";
            mNewRow["description"] = "Oth";
            mNewRow["category"] = "";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region ARVAdherence
        internal DataTable ARVAdherence()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "G (good)";
            mNewRow["description"] = "Fewer than 2 missed days";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "P (poor)";
            mNewRow["description"] = "2 or more missed days";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region ARVPoorAdherenceReasons
        internal DataTable ARVPoorAdherenceReasons()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "1";
            mNewRow["description"] = "TOXICITY";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "2";
            mNewRow["description"] = "SHARE WITH OTHERS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "3";
            mNewRow["description"] = "FORGOT";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "4";
            mNewRow["description"] = "FELT BETTER";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "5";
            mNewRow["description"] = "TOO ILL";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "6";
            mNewRow["description"] = "STIGMA";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "7";
            mNewRow["description"] = "PHARMACY DRUG STOCK OUT";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "8";
            mNewRow["description"] = "PATIENT LOST/RAN OUT OF PILLS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "9";
            mNewRow["description"] = "DELIVERY/TRAVEL PROBLEMS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "10";
            mNewRow["description"] = "INABILITY TO PAY";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "11";
            mNewRow["description"] = "ALCOHOL";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "12";
            mNewRow["description"] = "DEPRESSION";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "13";
            mNewRow["description"] = "OTHER";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region ReferredTo
        internal DataTable ReferredTo()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "1";
            mNewRow["description"] = "PMTCT";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "2";
            mNewRow["description"] = "HBC";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "3";
            mNewRow["description"] = "PLHIV SUPPORT GROUP/CLUB";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "4";
            mNewRow["description"] = "ORPHAN AND VULNERABLE CHILDREN GROUP";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "5";
            mNewRow["description"] = "MEDICAL SPECIALITY";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "6";
            mNewRow["description"] = "NUTRITIONAL SUPPORT";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "7";
            mNewRow["description"] = "LEGAL";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "TB";
            mNewRow["description"] = "TB CLINIC";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "FP";
            mNewRow["description"] = "FP SERVICES";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "ART";
            mNewRow["description"] = "ART";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "OTH";
            mNewRow["description"] = "OTHER";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region ARVReasons
        internal DataTable ARVReasons()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));
            mDtData.Columns.Add("category", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "51";
            mNewRow["description"] = "DOES NOT FULFILL CRITERIA";
            mNewRow["category"] = "NO START";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "52";
            mNewRow["description"] = "FULFILLS CRITERIA BUT COUNSELING FOR ARVS ONGOING";
            mNewRow["category"] = "NO START";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "53";
            mNewRow["description"] = "FULFILLS CRITERIA BUT NO ARVS AVAILABLE";
            mNewRow["category"] = "NO START";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "54";
            mNewRow["description"] = "FULFILLS CRITERIA BUT IS NOT WILLING";
            mNewRow["category"] = "NO START";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "55";
            mNewRow["description"] = "FULFILLS CRITERIA BUT IS ON TB RX";
            mNewRow["category"] = "NO START";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "57";
            mNewRow["description"] = "FULFILLS CRITERIA BUT AWAITS LAB RESULTS";
            mNewRow["category"] = "NO START";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "58";
            mNewRow["description"] = "FULFILLS CRITERIA BUT HAS OI AND IS TOO SICK TO START";
            mNewRow["category"] = "NO START";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "99";
            mNewRow["description"] = "FULFILLS CRITERIA BUT NO START - OTHER";
            mNewRow["category"] = "NO START";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "110";
            mNewRow["description"] = "START TB TREATMENT";
            mNewRow["category"] = "CHANGE OR STOP ARVS BECAUSE OF TB OR ADVERSE REACTIONS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "111";
            mNewRow["description"] = "NAUSEA/VOMITING";
            mNewRow["category"] = "CHANGE OR STOP ARVS BECAUSE OF TB OR ADVERSE REACTIONS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "112";
            mNewRow["description"] = "DIARRHOEA";
            mNewRow["category"] = "CHANGE OR STOP ARVS BECAUSE OF TB OR ADVERSE REACTIONS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "113";
            mNewRow["description"] = "HEADACHE";
            mNewRow["category"] = "CHANGE OR STOP ARVS BECAUSE OF TB OR ADVERSE REACTIONS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "114";
            mNewRow["description"] = "FEVER";
            mNewRow["category"] = "CHANGE OR STOP ARVS BECAUSE OF TB OR ADVERSE REACTIONS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "115";
            mNewRow["description"] = "RASH";
            mNewRow["category"] = "CHANGE OR STOP ARVS BECAUSE OF TB OR ADVERSE REACTIONS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "116";
            mNewRow["description"] = "PERIPHERAL NEUROPATHY";
            mNewRow["category"] = "CHANGE OR STOP ARVS BECAUSE OF TB OR ADVERSE REACTIONS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "117";
            mNewRow["description"] = "HEPATITIS";
            mNewRow["category"] = "CHANGE OR STOP ARVS BECAUSE OF TB OR ADVERSE REACTIONS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "118";
            mNewRow["description"] = "JAUNDICE";
            mNewRow["category"] = "CHANGE OR STOP ARVS BECAUSE OF TB OR ADVERSE REACTIONS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "119";
            mNewRow["description"] = "DEMENTIA";
            mNewRow["category"] = "CHANGE OR STOP ARVS BECAUSE OF TB OR ADVERSE REACTIONS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "120";
            mNewRow["description"] = "ANAEMIA";
            mNewRow["category"] = "CHANGE OR STOP ARVS BECAUSE OF TB OR ADVERSE REACTIONS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "121";
            mNewRow["description"] = "PANCREATITIS";
            mNewRow["category"] = "CHANGE OR STOP ARVS BECAUSE OF TB OR ADVERSE REACTIONS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "122";
            mNewRow["description"] = "CNS ADVERSE EVENT";
            mNewRow["category"] = "CHANGE OR STOP ARVS BECAUSE OF TB OR ADVERSE REACTIONS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "123";
            mNewRow["description"] = "OTHER ADVERSE EVENT";
            mNewRow["category"] = "CHANGE OR STOP ARVS BECAUSE OF TB OR ADVERSE REACTIONS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "131";
            mNewRow["description"] = "TREATMENT FAILURE, CLINICAL";
            mNewRow["category"] = "CHANGE OR STOP ARVS BECAUSE OF TREATMENT FAILURE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "132";
            mNewRow["description"] = "TREATMENT FAILURE, IMMUNOLOGICAL";
            mNewRow["category"] = "CHANGE OR STOP ARVS BECAUSE OF TREATMENT FAILURE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "141";
            mNewRow["description"] = "POOR ADHERENCE";
            mNewRow["category"] = "CHANGE OR STOP ARVS, OTHER REASON";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "142";
            mNewRow["description"] = "PATIENT DECISION";
            mNewRow["category"] = "CHANGE OR STOP ARVS, OTHER REASON";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "143";
            mNewRow["description"] = "PREGNANCY";
            mNewRow["category"] = "CHANGE OR STOP ARVS, OTHER REASON";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "144";
            mNewRow["description"] = "END OF PMTCT";
            mNewRow["category"] = "CHANGE OR STOP ARVS, OTHER REASON";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "148";
            mNewRow["description"] = "STOCK OUT";
            mNewRow["category"] = "CHANGE OR STOP ARVS, OTHER REASON";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "149";
            mNewRow["description"] = "OTHER";
            mNewRow["category"] = "CHANGE OR STOP ARVS, OTHER REASON";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "151";
            mNewRow["description"] = "RESTART ARV AFTER 3 OR MORE MONTHS NOT ON ARV";
            mNewRow["category"] = "RESTART ARV AFTER 3 OR MORE MONTHS NOT ON ARV";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region FollowUpStatus
        internal DataTable FollowUpStatus()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "MISSAP";
            mNewRow["description"] = "1 OR 2 MISSING APPOINTMENTS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "LTF";
            mNewRow["description"] = "LOST TO FOLLOW-UP";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "STOP";
            mNewRow["description"] = "PATIENT/PROVIDER DECISION TO STOP ART";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "TO";
            mNewRow["description"] = "TRANSFER OUT";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "DEAD";
            mNewRow["description"] = "DIED";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "RESTART";
            mNewRow["description"] = "Patient restart ARVs after interruption from STOP or LOST TO FOLLOW-UP";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "Con";
            mNewRow["description"] = "Continue pre-ART follow-up (given next appointment)";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "Dis";
            mNewRow["description"] = "Disch. uninfected";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "ART";
            mNewRow["description"] = "Stopped pre-ART follow-up and started ART";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "To";
            mNewRow["description"] = "Transferred to other HIV care clinic (including unofficial transfers)";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "Def";
            mNewRow["description"] = "Defaulted: More than 2 months overdue after next appointment";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "D";
            mNewRow["description"] = "Died before starting ART";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region ReferredFrom
        internal DataTable ReferredFrom()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "OIPD";
            mNewRow["description"] = "OPD/INPATIENT";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "STI";
            mNewRow["description"] = "STI";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "TBDOT";
            mNewRow["description"] = "TB DOTS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "MCPM";
            mNewRow["description"] = "RCH/PMTCT/EID";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "PLG";
            mNewRow["description"] = "PLHIV GROUP";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "SELREF";
            mNewRow["description"] = "SELF REFERRAL (incl. VCT)";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "HBC";
            mNewRow["description"] = "HOME BASED CARE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "OTH";
            mNewRow["description"] = "OTHER";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region PriorARVExposure
        internal DataTable PriorARVExposure()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "NONE";
            mNewRow["description"] = "NONE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "PRTH";
            mNewRow["description"] = "PRIOR THERAPY (Transfer in without records";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "PMMN";
            mNewRow["description"] = "PMTCT MONOTHERAPY";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "PMCT";
            mNewRow["description"] = "PMTCT COMBINATION THERAPY";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "TRIN";
            mNewRow["description"] = "TRANSFER IN (With records)";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region ARTWhyEligible
        internal DataTable ARTWhyEligible()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "CLON";
            mNewRow["description"] = "CLINICAL ONLY";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "CD4";
            mNewRow["description"] = "CD4 COUNT/%";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "TLC";
            mNewRow["description"] = "TLC";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region PMTCTComb
        internal DataTable PMTCTComb()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "None";
            mNewRow["description"] = "None";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "AZT";
            mNewRow["description"] = "AZT";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "AZT 0-3w";
            mNewRow["description"] = "AZT 0-3w";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "AZT 4+w";
            mNewRow["description"] = "AZT 4+w";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "AZT3TC";
            mNewRow["description"] = "AZT3TC";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "ART";
            mNewRow["description"] = "ART";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "ART 0-3w";
            mNewRow["description"] = "ART 0-3w";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "ART 4+w";
            mNewRow["description"] = "ART 4+w";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "NVP";
            mNewRow["description"] = "NVP";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "sdNVP";
            mNewRow["description"] = "sdNVP";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "NVP AZT3TC";
            mNewRow["description"] = "NVP AZT3TC";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "NVP AZT";
            mNewRow["description"] = "NVP AZT";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "Unk";
            mNewRow["description"] = "Unk";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region PMTCTDisclosedTo
        internal DataTable PMTCTDisclosedTo()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "PART";
            mNewRow["description"] = "Partner";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "CHLD";
            mNewRow["description"] = "Child(ren)";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "OTH";
            mNewRow["description"] = "Other";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "NYT";
            mNewRow["description"] = "Not Yet";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region WastingCodes
        internal DataTable WastingCodes()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "No";
            mNewRow["description"] = "No side effects";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "Mod";
            mNewRow["description"] = "Moderate (weight-for-height, BMI or MUAC)";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "Sev";
            mNewRow["description"] = "Severe (weight-for-height, BMI ot MUAC)";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region ARTOutcomes
        internal DataTable ARTOutcomes()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "D";
            mNewRow["description"] = "Died";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "Def";
            mNewRow["description"] = "Defaulted: More than 2 months overdue after expected to have run out of tablets, unknown survival and ART status";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "Stop";
            mNewRow["description"] = "Patient stopped taking ARVs (clinician`s or patient`s own decision)";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "TO";
            mNewRow["description"] = "Transferred to other ART clinic (including unofficial transfers)";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region HIVTestReasons
        internal DataTable HIVTestReasons()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "E";
            mNewRow["description"] = "Exposed";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "S";
            mNewRow["description"] = "Sick";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "I";
            mNewRow["description"] = "Indeterminate";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "C";
            mNewRow["description"] = "Confirmatory";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "B";
            mNewRow["description"] = "6 weeks since breast feeding cessation";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region BreastFeedings
        internal DataTable BreastFeedings()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "Exc";
            mNewRow["description"] = "Ongoing - Exclusive";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "M";
            mNewRow["description"] = "Ongoing - Mixed";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "C";
            mNewRow["description"] = "Ongoing - Complimentary";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "<6";
            mNewRow["description"] = "Stopped - In last 6 weeks";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "6+";
            mNewRow["description"] = "Stopped - Over 6 weeks age";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region MotherStatus
        internal DataTable MotherStatus()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "No_ART";
            mNewRow["description"] = "Alive Not on ART";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "On_ART";
            mNewRow["description"] = "Alive on ART";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "Died";
            mNewRow["description"] = "Died";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "Unk";
            mNewRow["description"] = "Unknown";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region HIVInfections
        internal DataTable HIVInfections()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "A";
            mNewRow["description"] = "Not Infected";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "B";
            mNewRow["description"] = "Infected";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "C";
            mNewRow["description"] = "Not ART eligible";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "D";
            mNewRow["description"] = "PSHD";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region ClinMonit
        internal DataTable ClinMonit()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "NAD";
            mNewRow["description"] = "Noth. abnorm detect";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "Sick";
            mNewRow["description"] = "Any sickness, specify";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region TransferIns
        internal DataTable TransferIns()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "WR";
            mNewRow["description"] = "WITH RECORDS (refferal and CTC 1 forms)";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "NR";
            mNewRow["description"] = "NO RECORDS AVAILABLE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "IC";
            mNewRow["description"] = "IN CARE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "OA";
            mNewRow["description"] = "ON ART";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region FPlanMethods
        internal DataTable FPlanMethods()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "O";
            mNewRow["description"] = "NOT USING";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "J";
            mNewRow["description"] = "DEPOINJECTION";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "Z";
            mNewRow["description"] = "STERILIZATION";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "T";
            mNewRow["description"] = "TRAD/WITHDRAWAL";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "P";
            mNewRow["description"] = "PILLS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "M";
            mNewRow["description"] = "IMPLANTS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "C";
            mNewRow["description"] = "CONDOM";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "L";
            mNewRow["description"] = "IUD";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region VisitTypes
        internal DataTable VisitTypes()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "S";
            mNewRow["description"] = "Scheduled visit at this clinic";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "US";
            mNewRow["description"] = "Unscheduled visit at this clinic";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "TK";
            mNewRow["description"] = "Traced back after LTFU";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "TS";
            mNewRow["description"] = "Treatment supports drug pick up";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "O";
            mNewRow["description"] = "Visit other clinic (refill/outreach or transit)";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "IP";
            mNewRow["description"] = "In-Patient Consultation";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region RelevantComedics
        internal DataTable RelevantComedics()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "1";
            mNewRow["description"] = "COTRIMOXAZOLE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "2";
            mNewRow["description"] = "FLUCONAZOLE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "3";
            mNewRow["description"] = "OTHER ANTIBIOTICS";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "4";
            mNewRow["description"] = "ANTIMALARIAL";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "5";
            mNewRow["description"] = "OTHER";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region TBScreening
        internal DataTable TBScreening()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "Screen -ve";
            mNewRow["description"] = "Answered NO to all 5 TB screening questions";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "TB Susp";
            mNewRow["description"] = "Answered Yes to 1 or more of TB screening questions";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "SS+";
            mNewRow["description"] = "Sputum Sample Positive";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "SS-";
            mNewRow["description"] = "Sputum Sample Negative";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "CXR+";
            mNewRow["description"] = "Chest X-Ray suggestive of TB";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "CXR-";
            mNewRow["description"] = "X-Ray NOT suggestive of TB";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region TBTreatment
        internal DataTable TBTreatment()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "START TB";
            mNewRow["description"] = "START TB Rx";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "CTN TB";
            mNewRow["description"] = "CONTINUE TB Rx";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "CPLT TB";
            mNewRow["description"] = "COMPLETE TB Rx";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "STOP TB";
            mNewRow["description"] = "STOPPED TB Rx";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "RES TB";
            mNewRow["description"] = "RESTART TB Rx";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "START IPT";
            mNewRow["description"] = "START IPT";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "CTN IPT";
            mNewRow["description"] = "CONTINUE IPT";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "CPLT IPT";
            mNewRow["description"] = "COMPLETE IPT";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "STOP IPT";
            mNewRow["description"] = "STOPPED IPT";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "RES IPT";
            mNewRow["description"] = "RESTART IPT";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region NutritionalStatus
        internal DataTable NutritionalStatus()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "OK";
            mNewRow["description"] = "NOT MALNOURISHED";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "MOD";
            mNewRow["description"] = "MODERATE MALNOURISHED";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "SEV";
            mNewRow["description"] = "SEVERELY MALNOURISHED";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region NutritionalSupp
        internal DataTable NutritionalSupp()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "TF";
            mNewRow["description"] = "THERAPEUTIC FOOD";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "SF";
            mNewRow["description"] = "SUPPLEMENTAL FOOD";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "NA";
            mNewRow["description"] = "NOT APPLICABLE";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region PMTCTReferedTo
        internal DataTable PMTCTReferedTo()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "CTC";
            mNewRow["description"] = "CTC";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "HBC";
            mNewRow["description"] = "HBC";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "HLF";
            mNewRow["description"] = "HLF";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "OTHER";
            mNewRow["description"] = "OTHER";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region PMTCTCombLabour
        internal DataTable PMTCTCombLabour()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "NVP";
            mNewRow["description"] = "NVP swallowed";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "AZT";
            mNewRow["description"] = "AZT swallowed";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "3TC";
            mNewRow["description"] = "3TC swallowed";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "ART";
            mNewRow["description"] = "Triple therapy ARV swallowed";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "None";
            mNewRow["description"] = "None";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "Tail";
            mNewRow["description"] = "AZT + 3TC dispensed during postpartum";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region PMTCTInfantDoseReceived
        internal DataTable PMTCTInfantDoseReceived()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "N";
            mNewRow["description"] = "Infant not received NVP";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "Y";
            mNewRow["description"] = "Infant received NVP";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region PMTCTInfantDoseDispensed
        internal DataTable PMTCTInfantDoseDispensed()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "1wk";
            mNewRow["description"] = "AZT for 1 week";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "4wks";
            mNewRow["description"] = "AZT for 4 weeks";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region PMTCTInfantFeeding
        internal DataTable PMTCTInfantFeeding()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "IFC";
            mNewRow["description"] = "Infant feeding counseling & support";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "EBF";
            mNewRow["description"] = "Exclusive breast feeding";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "RF";
            mNewRow["description"] = "Replacement Feeding";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion

        #region PMTCTHIVStatus
        internal DataTable PMTCTHIVStatus()
        {
            DataTable mDtData = new DataTable("data");
            mDtData.Columns.Add("code", typeof(System.String));
            mDtData.Columns.Add("description", typeof(System.String));

            DataRow mNewRow;

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "P";
            mNewRow["description"] = "Positive";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "N";
            mNewRow["description"] = "Negative";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            mNewRow = mDtData.NewRow();
            mNewRow["code"] = "U";
            mNewRow["description"] = "Unknown";
            mDtData.Rows.Add(mNewRow);
            mDtData.AcceptChanges();

            return mDtData;
        }
        #endregion
    }
}
