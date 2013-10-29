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

namespace AfyaPro_MT
{
    public class clsUserActions : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsUserActions";

        #endregion

        #region View_Searching
        public DataTable View_Searching(string mUserId)
        {
            String mFunctionName = "View_Searching";

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
                mCommand.CommandText = "select * from sys_useractions where userid='" + mUserId.Trim() + "'"; ;

                DataTable mDataTable = new DataTable("sys_useractions");
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

        #region Save_ProductSearching
        public AfyaPro_Types.clsResult Save_ProductSearching(string mUserId, 
            int mSearchWhileTyping, int mSearchOption, string mFieldName)
        {
            String mFunctionName = "Save_ProductSearching";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            bool mExist = false;

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

            #region check 4 existance
            try
            {
                mCommand.CommandText = "select * from sys_useractions where userid='" + mUserId.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mExist = true;
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

            #region save
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                if (mExist == false)
                {
                    mCommand.CommandText = "insert into sys_useractions(userid,searchproductswhiletyping,"
                    + "searchproductsoption,searchproductfieldname) values('"
                    + mUserId.Trim() + "'," + mSearchWhileTyping + "," + mSearchOption + ",'" + mFieldName + "')";
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    mCommand.CommandText = "update sys_useractions set searchproductswhiletyping ="
                    + mSearchWhileTyping + ",searchproductsoption=" + mSearchOption + ",searchproductfieldname='"
                    + mFieldName + "' where userid='" + mUserId.Trim() + "'";
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

        #region Save_PatientSearching
        public AfyaPro_Types.clsResult Save_PatientSearching(string mUserId,
            int mSearchWhileTyping, int mSearchOption, string mFieldName)
        {
            String mFunctionName = "Save_PatientSearching";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            bool mExist = false;

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

            #region check 4 existance
            try
            {
                mCommand.CommandText = "select * from sys_useractions where userid='" + mUserId.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mExist = true;
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

            #region save
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                if (mExist == false)
                {
                    mCommand.CommandText = "insert into sys_useractions(userid,searchpatientswhiletyping,"
                    + "searchpatientsoption,searchpatientfieldname) values('"
                    + mUserId.Trim() + "'," + mSearchWhileTyping + "," + mSearchOption + ",'" + mFieldName + "')";
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    mCommand.CommandText = "update sys_useractions set searchpatientswhiletyping ="
                    + mSearchWhileTyping + ",searchpatientsoption=" + mSearchOption + ",searchpatientfieldname='"
                    + mFieldName + "' where userid='" + mUserId.Trim() + "'";
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

        #region Save_DebtorSearching
        public AfyaPro_Types.clsResult Save_DebtorSearching(string mUserId,
            int mSearchWhileTyping, int mSearchOption, string mFieldName)
        {
            String mFunctionName = "Save_DebtorSearching";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            bool mExist = false;

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

            #region check 4 existance
            try
            {
                mCommand.CommandText = "select * from sys_useractions where userid='" + mUserId.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mExist = true;
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

            #region save
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                if (mExist == false)
                {
                    mCommand.CommandText = "insert into sys_useractions(userid,searchdebtorswhiletyping,"
                    + "searchdebtorsoption,searchdebtorfieldname) values('"
                    + mUserId.Trim() + "'," + mSearchWhileTyping + "," + mSearchOption + ",'" + mFieldName + "')";
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    mCommand.CommandText = "update sys_useractions set searchdebtorswhiletyping ="
                    + mSearchWhileTyping + ",searchdebtorsoption=" + mSearchOption + ",searchdebtorfieldname='"
                    + mFieldName + "' where userid='" + mUserId.Trim() + "'";
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

        #region Save_ClientGroupSearching
        public AfyaPro_Types.clsResult Save_ClientGroupSearching(string mUserId,
            int mSearchWhileTyping, int mSearchOption, string mFieldName)
        {
            String mFunctionName = "Save_ClientGroup";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            bool mExist = false;

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

            #region check 4 existance
            try
            {
                mCommand.CommandText = "select * from sys_useractions where userid='" + mUserId.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mExist = true;
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

            #region save
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                if (mExist == false)
                {
                    mCommand.CommandText = "insert into sys_useractions(userid,searchclientgroupswhiletyping,"
                    + "searchclientgroupsoption,searchclientgroupfieldname) values('"
                    + mUserId.Trim() + "'," + mSearchWhileTyping + "," + mSearchOption + ",'" + mFieldName + "')";
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    mCommand.CommandText = "update sys_useractions set searchclientgroupswhiletyping ="
                    + mSearchWhileTyping + ",searchclientgroupsoption=" + mSearchOption + ",searchclientgroupfieldname='"
                    + mFieldName + "' where userid='" + mUserId.Trim() + "'";
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

        #region Save_ClientGroupMemberSearching
        public AfyaPro_Types.clsResult Save_ClientGroupMemberSearching(string mUserId,
            int mSearchWhileTyping, int mSearchOption, string mFieldName)
        {
            String mFunctionName = "Save_ClientGroupMemberSearching";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            bool mExist = false;

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

            #region check 4 existance
            try
            {
                mCommand.CommandText = "select * from sys_useractions where userid='" + mUserId.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mExist = true;
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

            #region save
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                if (mExist == false)
                {
                    mCommand.CommandText = "insert into sys_useractions(userid,searchclientgroupmemberswhiletyping,"
                    + "searchclientgroupmembersoption,searchclientgroupmemberfieldname) values('"
                    + mUserId.Trim() + "'," + mSearchWhileTyping + "," + mSearchOption + ",'" + mFieldName + "')";
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    mCommand.CommandText = "update sys_useractions set searchclientgroupmemberswhiletyping ="
                    + mSearchWhileTyping + ",searchclientgroupmembersoption=" + mSearchOption + ",searchclientgroupmemberfieldname='"
                    + mFieldName + "' where userid='" + mUserId.Trim() + "'";
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

        #region Save_BillingItemGroupSearching
        public AfyaPro_Types.clsResult Save_BillingItemGroupSearching(string mUserId,
            int mSearchWhileTyping, int mSearchOption, string mFieldName)
        {
            String mFunctionName = "Save_BillingItemGroupSearching";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            bool mExist = false;

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

            #region check 4 existance
            try
            {
                mCommand.CommandText = "select * from sys_useractions where userid='" + mUserId.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mExist = true;
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

            #region save
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                if (mExist == false)
                {
                    mCommand.CommandText = "insert into sys_useractions(userid,searchbillingitemgroupswhiletyping,"
                    + "searchbillingitemgroupsoption,searchbillingitemgroupfieldname) values('"
                    + mUserId.Trim() + "'," + mSearchWhileTyping + "," + mSearchOption + ",'" + mFieldName + "')";
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    mCommand.CommandText = "update sys_useractions set searchbillingitemgroupswhiletyping ="
                    + mSearchWhileTyping + ",searchbillingitemgroupsoption=" + mSearchOption + ",searchbillingitemgroupfieldname='"
                    + mFieldName + "' where userid='" + mUserId.Trim() + "'";
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

        #region Save_DiagnosisSearching
        public AfyaPro_Types.clsResult Save_DiagnosisSearching(string mUserId,
            int mSearchWhileTyping, int mSearchOption, string mFieldName)
        {
            String mFunctionName = "Save_DiagnosisSearching";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            bool mExist = false;

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

            #region check 4 existance
            try
            {
                mCommand.CommandText = "select * from sys_useractions where userid='" + mUserId.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mExist = true;
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

            #region save
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                if (mExist == false)
                {
                    mCommand.CommandText = "insert into sys_useractions(userid,searchdiagnoseswhiletyping,"
                    + "searchdiagnosesoption,searchdiagnosisfieldname) values('"
                    + mUserId.Trim() + "'," + mSearchWhileTyping + "," + mSearchOption + ",'" + mFieldName + "')";
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    mCommand.CommandText = "update sys_useractions set searchdiagnoseswhiletyping ="
                    + mSearchWhileTyping + ",searchdiagnosesoption=" + mSearchOption + ",searchdiagnosisfieldname='"
                    + mFieldName + "' where userid='" + mUserId.Trim() + "'";
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

        #region Save_MtuhaDiagnosisSearching
        public AfyaPro_Types.clsResult Save_MtuhaDiagnosisSearching(string mUserId,
            int mSearchWhileTyping, int mSearchOption, string mFieldName)
        {
            String mFunctionName = "Save_MtuhaDiagnosisSearching";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            bool mExist = false;

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

            #region check 4 existance
            try
            {
                mCommand.CommandText = "select * from sys_useractions where userid='" + mUserId.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mExist = true;
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

            #region save
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                if (mExist == false)
                {
                    mCommand.CommandText = "insert into sys_useractions(userid,searchmtuhadiagnoseswhiletyping,"
                    + "searchmtuhadiagnosesoption,searchmtuhadiagnosisfieldname) values('"
                    + mUserId.Trim() + "'," + mSearchWhileTyping + "," + mSearchOption + ",'" + mFieldName + "')";
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    mCommand.CommandText = "update sys_useractions set searchmtuhadiagnoseswhiletyping ="
                    + mSearchWhileTyping + ",searchmtuhadiagnosesoption=" + mSearchOption + ",searchmtuhadiagnosisfieldname='"
                    + mFieldName + "' where userid='" + mUserId.Trim() + "'";
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

        #region Save_SupplierSearching
        public AfyaPro_Types.clsResult Save_SupplierSearching(string mUserId,
            int mSearchWhileTyping, int mSearchOption, string mFieldName)
        {
            String mFunctionName = "Save_SupplierSearching";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            bool mExist = false;

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

            #region check 4 existance
            try
            {
                mCommand.CommandText = "select * from sys_useractions where userid='" + mUserId.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mExist = true;
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

            #region save
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                if (mExist == false)
                {
                    mCommand.CommandText = "insert into sys_useractions(userid,searchsupplierswhiletyping,"
                    + "searchsuppliersoption,searchsupplierfieldname) values('"
                    + mUserId.Trim() + "'," + mSearchWhileTyping + "," + mSearchOption + ",'" + mFieldName + "')";
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    mCommand.CommandText = "update sys_useractions set searchsupplierswhiletyping ="
                    + mSearchWhileTyping + ",searchsuppliersoption=" + mSearchOption + ",searchsupplierfieldname='"
                    + mFieldName + "' where userid='" + mUserId.Trim() + "'";
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

        #region Save_StoreSearching
        public AfyaPro_Types.clsResult Save_StoreSearching(string mUserId,
            int mSearchWhileTyping, int mSearchOption, string mFieldName)
        {
            String mFunctionName = "Save_StoreSearching";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            bool mExist = false;

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

            #region check 4 existance
            try
            {
                mCommand.CommandText = "select * from sys_useractions where userid='" + mUserId.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mExist = true;
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

            #region save
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                if (mExist == false)
                {
                    mCommand.CommandText = "insert into sys_useractions(userid,searchstoreswhiletyping,"
                    + "searchstoresoption,searchstorefieldname) values('"
                    + mUserId.Trim() + "'," + mSearchWhileTyping + "," + mSearchOption + ",'" + mFieldName + "')";
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    mCommand.CommandText = "update sys_useractions set searchstoreswhiletyping ="
                    + mSearchWhileTyping + ",searchstoresoption=" + mSearchOption + ",searchstorefieldname='"
                    + mFieldName + "' where userid='" + mUserId.Trim() + "'";
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

        #region Save_RCHClientSearching
        public AfyaPro_Types.clsResult Save_RCHClientSearching(string mUserId,
            int mSearchWhileTyping, int mSearchOption, string mFieldName)
        {
            String mFunctionName = "Save_RCHClientSearching";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            bool mExist = false;

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

            #region check 4 existance
            try
            {
                mCommand.CommandText = "select * from sys_useractions where userid='" + mUserId.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mExist = true;
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

            #region save
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                if (mExist == false)
                {
                    mCommand.CommandText = "insert into sys_useractions(userid,searchrchclientswhiletyping,"
                    + "searchrchclientsoption,searchrchclientfieldname) values('"
                    + mUserId.Trim() + "'," + mSearchWhileTyping + "," + mSearchOption + ",'" + mFieldName + "')";
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    mCommand.CommandText = "update sys_useractions set searchrchclientswhiletyping ="
                    + mSearchWhileTyping + ",searchrchclientsoption=" + mSearchOption + ",searchrchclientfieldname='"
                    + mFieldName + "' where userid='" + mUserId.Trim() + "'";
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

        #region Save_IndicatorSearching
        public AfyaPro_Types.clsResult Save_IndicatorSearching(string mUserId,
            int mSearchWhileTyping, int mSearchOption, string mFieldName)
        {
            String mFunctionName = "Save_IndicatorSearching";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            bool mExist = false;

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

            #region check 4 existance
            try
            {
                mCommand.CommandText = "select * from sys_useractions where userid='" + mUserId.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mExist = true;
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

            #region save
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                if (mExist == false)
                {
                    mCommand.CommandText = "insert into sys_useractions(userid,searchindicatorswhiletyping,"
                    + "searchindicatorsoption,searchindicatorfieldname) values('"
                    + mUserId.Trim() + "'," + mSearchWhileTyping + "," + mSearchOption + ",'" + mFieldName + "')";
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    mCommand.CommandText = "update sys_useractions set searchindicatorswhiletyping ="
                    + mSearchWhileTyping + ",searchindicatorsoption=" + mSearchOption + ",searchindicatorfieldname='"
                    + mFieldName + "' where userid='" + mUserId.Trim() + "'";
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
