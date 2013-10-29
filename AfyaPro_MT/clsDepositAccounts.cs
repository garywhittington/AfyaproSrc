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
    public class clsDepositAccounts : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsDepositAccounts";

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
                string mCommandText = "select * from billaccounts";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("billaccounts");
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

        #region Get_FromToWhom
        public DataTable Get_FromToWhom(String mFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "Get_FromToWhom";

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
                string mCommandText = "select * from billaccountfromtowhom";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("billaccountfromtowhom");
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

        #region View_Transactions
        public DataTable View_Transactions(String mFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_Transactions";

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
                string mCommandText = "select * from billaccountslog";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("billaccountslog");
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

        #region View_Members
        public DataTable View_Members(String mFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_Members";

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
                string mCommandText = "SELECT au.accountcode,ac.description accountname,au.membercode,"
                + "pa.firstname,pa.othernames,pa.surname,pa.gender FROM billaccountusers au"
                + " INNER JOIN patients pa ON au.membercode=pa.code"
                + " INNER JOIN billaccounts ac ON au.accountcode=ac.code";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("billaccountusers");
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
        public AfyaPro_Types.clsResult Add(DateTime mTransDate, Int16 mGenerateCode, String mAccountCode, 
            String mMemberCode, int mAllowOverDraft, int mInActive, string mUserId)
        {
            String mFunctionName = "Add";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;

            mTransDate = mTransDate.Date;

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.bildepositaccounts_add.ToString(), mUserId);
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
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.depositaccountcode), "billaccounts", "code");
                if (mObjCode.Exe_Result == -1)
                {
                    mResult.Exe_Result = mObjCode.Exe_Result;
                    mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                    return mResult;
                }
                mAccountCode = mObjCode.GeneratedCode;
            }

            #endregion

            #region check 4 duplicate
            try
            {
                mCommand.CommandText = "select * from billaccounts where code='" + mAccountCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DepositAccountCodeIsInUse.ToString();
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

            #region do add
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                mCommand.CommandText = "insert into billaccounts(sysdate,transdate,code,description,balance,"
                    + "allowoverdraft,inactive) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," 
                    + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mAccountCode.Trim() + "','" + mMemberCode.Trim() 
                    + "',0," + mAllowOverDraft + "," + mInActive + ")";
                mCommand.ExecuteNonQuery();

                if (mGenerateCode == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.depositaccountcode);
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
        public AfyaPro_Types.clsResult Edit(String mAccountCode, 
            String mMemberCode, int mAllowOverDraft, int mInActive, string mUserId)
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.bildepositaccounts_edit.ToString(), mUserId);
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
                mCommand.CommandText = "select * from billaccounts where code='" + mAccountCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DepositAccountCodeDoesNotExist.ToString();
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
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                mCommand.CommandText = "update billaccounts set description = '" + mMemberCode.Trim()
                + "',allowoverdraft=" + mAllowOverDraft + ",inactive=" + mInActive + " where code='" + mAccountCode.Trim() + "'";
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
        public AfyaPro_Types.clsResult Delete(String mAccountCode, string mUserId)
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.bildepositaccounts_delete.ToString(), mUserId);
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
                mCommand.CommandText = "select * from billaccounts where code='" + mAccountCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DepositAccountCodeDoesNotExist.ToString();
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

                //execute statements
                mCommand.CommandText = "delete from billaccounts where code='" + mAccountCode.Trim() + "'";
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

        #region Add_Member
        public AfyaPro_Types.clsResult Add_Member(string mAccountCode, string mMemberCode, string mUserId)
        {
            String mFunctionName = "Add_Member";

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

            #region check 4 account existance
            try
            {
                mCommand.CommandText = "select * from billaccounts where code='" + mAccountCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DepositAccountCodeDoesNotExist.ToString();
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

            #region do add
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //billaccountusers
                mCommand.CommandText = "insert into billaccountusers(accountcode,membercode) values('"
                    + mAccountCode.Trim() + "','" + mMemberCode.Trim() + "')";
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

        #region Delete_Member
        public AfyaPro_Types.clsResult Delete_Member(string mAccountCode, string mMemberCode, string mUserId)
        {
            String mFunctionName = "Delete_Member";

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

            #region check 4 account existance
            try
            {
                mCommand.CommandText = "select * from billaccounts where code='" + mAccountCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DepositAccountCodeDoesNotExist.ToString();
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
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //billaccountusers
                mCommand.CommandText = "delete from billaccountusers where accountcode='"
                    + mAccountCode.Trim() + "' and membercode='" + mMemberCode.Trim() + "'";
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

        #region Transact
        public AfyaPro_Types.clsResult Transact(DateTime mTransDate, Int16 mGenerateDepositNo, String mDepositTransactionId,
            string mAccountCode, string mAccountDescription, string mFromWhomToWhomAccountCode, string mFromWhomToWhom, double mTransAmount, string mMemo, 
            string mEntrySide, string mUserId)
        {
            String mFunctionName = "Transact";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            OdbcTransaction mTrans = null;

            Int16 mEntrySideInt = 0;
            int mAllowOverDraft = 0;
            int mInActive = 0;
            int mDestinationInActive = 0;
            double mCurrentBalance = 0;

            mTransDate = mTransDate.Date;

            switch (mEntrySide.Trim().ToLower())
            {
                case "debit": mEntrySideInt = (int)AfyaPro_Types.clsEnums.AccountEntrySides.Debit; break;
                case "credit": mEntrySideInt = (int)AfyaPro_Types.clsEnums.AccountEntrySides.Credit; break;
                case "transfer": mEntrySideInt = (int)AfyaPro_Types.clsEnums.AccountEntrySides.Transfer; break;
            }

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.bildepositaccounts_transact.ToString(), mUserId);
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
                mCommand.CommandText = "select * from billaccounts where code='" + mAccountCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DepositAccountCodeDoesNotExist.ToString();
                    return mResult;
                }
                else
                {
                    mAllowOverDraft = Convert.ToInt16(mDataReader["allowoverdraft"]);
                    mInActive = Convert.ToInt16(mDataReader["inactive"]);
                    mCurrentBalance = Convert.ToDouble(mDataReader["balance"]);
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

            #region check 4 destination account existance

            if (mEntrySideInt == 3)
            {
                try
                {
                    mCommand.CommandText = "select * from billaccounts where code='" + mFromWhomToWhomAccountCode.Trim() + "'";
                    mDataReader = mCommand.ExecuteReader();
                    if (mDataReader.Read() == false)
                    {
                        mResult.Exe_Result = 0;
                        mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DestinationAccountDoesNotExist.ToString();
                        return mResult;
                    }
                    else
                    {
                        mDestinationInActive = Convert.ToInt16(mDataReader["inactive"]);
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
            }
            #endregion

            #region check for accounts restrictions

            if (mInActive == 1)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_AccountIsMarkedInactive.ToString();
                return mResult;
            }

            if (mDestinationInActive == 1 && mEntrySideInt == 3)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DestinationAccountIsMarkedInactive.ToString();
                return mResult;
            }

            if (mEntrySideInt == 2 || mEntrySideInt == 3)
            {
                if (mTransAmount > mCurrentBalance && mAllowOverDraft == 0)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_AccountDoesNotAllowOverDraft.ToString();
                    return mResult;
                }
            }

            #endregion

            #region auto generate transactionid, if option is on

            if (mGenerateDepositNo == 1)
            {
                clsAutoCodes objAutoCodes = new clsAutoCodes();
                AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                mObjCode = objAutoCodes.Next_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.deposittransactionid), "billaccounttransactions", "transactionid");
                if (mObjCode.Exe_Result == -1)
                {
                    mResult.Exe_Result = mObjCode.Exe_Result;
                    mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                    return mResult;
                }
                mDepositTransactionId = mObjCode.GeneratedCode;
            }

            #endregion

            #region check 4 duplicate transactionid
            try
            {
                mCommand.CommandText = "select * from billaccounttransactions where transactionid='" + mDepositTransactionId.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DuplicateDepositNoDetected.ToString();
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

            #region do transaction
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                double mCreditAmount = 0;
                double mDebitAmount = 0;

                switch (mEntrySideInt)
                {
                    case 1:
                        {
                            mDebitAmount = mTransAmount;
                            mCommand.CommandText = "update billaccounts set balance=balance+" + mDebitAmount
                                + " where code='" + mAccountCode.Trim() + "'";
                            mCommand.ExecuteNonQuery();
                        }
                        break;
                    case 2:
                        {
                            mCreditAmount = mTransAmount;
                            mCommand.CommandText = "update billaccounts set balance=balance-" + mCreditAmount
                                + " where code='" + mAccountCode.Trim() + "'";
                            mCommand.ExecuteNonQuery();
                        }
                        break;
                    case 3:
                        {
                            mCreditAmount = mTransAmount;
                            //source
                            mCommand.CommandText = "update billaccounts set balance=balance-" + mCreditAmount
                                + " where code='" + mAccountCode.Trim() + "'";
                            mCommand.ExecuteNonQuery();
                            //destination
                            mCommand.CommandText = "update billaccounts set balance=balance+" + mCreditAmount
                                + " where code='" + mFromWhomToWhomAccountCode.Trim() + "'";
                            mCommand.ExecuteNonQuery();
                        }
                        break;
                }

                //billaccountdeposits
                mCommand.CommandText = "insert into billaccounttransactions(sysdate,transdate,transactionid,"
                    + "accountcode,accountdescription,fromwhomtowhomcode,fromwhomtowhom,entryside,transdescription,"
                    + "transamount,userid) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) 
                    + ",'" + mDepositTransactionId.Trim() + "','" + mAccountCode.Trim() + "','" + mAccountDescription.Trim() 
                    + "','" + mFromWhomToWhomAccountCode.Trim() + "','" + mFromWhomToWhom.Trim() + "'," + mEntrySideInt + ",'" + mMemo.Trim() + "'," 
                    + mTransAmount + ",'" + mUserId.Trim() + "')";
                mCommand.ExecuteNonQuery();
                if (mEntrySideInt == 3)
                {
                    mCommand.CommandText = "insert into billaccounttransactions(sysdate,transdate,transactionid,"
                        + "accountcode,accountdescription,fromwhomtowhomcode,fromwhomtowhom,entryside,transdescription,"
                        + "transamount,userid) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate)
                        + ",'" + mDepositTransactionId.Trim() + "','" + mFromWhomToWhomAccountCode.Trim() + "','" + mFromWhomToWhom.Trim()
                        + "','" + mAccountCode.Trim() + "','" + mAccountDescription.Trim() + "'," + mEntrySideInt + ",'" + mMemo.Trim() + "',"
                        + mTransAmount + ",'" + mUserId.Trim() + "')";
                    mCommand.ExecuteNonQuery();
                }

                //billaccountslog
                mCommand.CommandText = "insert into billaccountslog(sysdate,transdate,reference,"
                    + "accountcode,accountdescription,fromwhomtowhomcode,fromwhomtowhom,entryside,transdescription,"
                    + "creditamount,debitamount,userid) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," 
                    + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mDepositTransactionId.Trim() + "','" 
                    + mAccountCode.Trim() + "','" + mAccountDescription.Trim() + "','" + mFromWhomToWhomAccountCode.Trim() 
                    + "','" + mFromWhomToWhom.Trim() + "'," + mEntrySideInt + ",'" + mMemo.Trim() + "'," + mCreditAmount 
                    + "," + mDebitAmount + ",'" + mUserId.Trim() + "')";
                mCommand.ExecuteNonQuery();
                if (mEntrySideInt == 3)
                {
                    mCommand.CommandText = "insert into billaccountslog(sysdate,transdate,reference,"
                        + "accountcode,accountdescription,fromwhomtowhomcode,fromwhomtowhom,entryside,transdescription,"
                        + "creditamount,debitamount,userid) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                        + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mDepositTransactionId.Trim() + "','"
                        + mFromWhomToWhomAccountCode.Trim() + "','" + mFromWhomToWhom.Trim() + "','" + mAccountCode.Trim()
                        + "','" + mAccountDescription.Trim() + "'," + mEntrySideInt + ",'" + mMemo.Trim() + "'," + mDebitAmount
                        + "," + mCreditAmount + ",'" + mUserId.Trim() + "')";
                    mCommand.ExecuteNonQuery();
                }

                //remember from/to whom
                if (mEntrySideInt != 3)
                {
                    DataTable mDtFromToWhom = new DataTable("fromtowhom");
                    mCommand.CommandText = "select * from billaccountfromtowhom where description='" + mFromWhomToWhom.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtFromToWhom);

                    if (mDtFromToWhom.Rows.Count == 0)
                    {
                        mCommand.CommandText = "insert into billaccountfromtowhom(description) values('" + mFromWhomToWhom.Trim() + "')";
                        mCommand.ExecuteNonQuery();
                    }
                }

                if (mGenerateDepositNo == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.deposittransactionid);
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
    }
}
