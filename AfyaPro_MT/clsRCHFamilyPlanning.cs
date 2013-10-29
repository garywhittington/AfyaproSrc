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

namespace AfyaPro_MT
{
    public class clsRCHFamilyPlanning : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsRCHFamilyPlanning";

        #endregion

        #region View_Active
        public DataTable View_Active(String mFilter, String mOrder)
        {
            String mFunctionName = "View_Active";

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
                //columns from clients
                string mClientColumns = clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "client");
                mClientColumns = mClientColumns + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "clientfullname");
                mClientColumns = mClientColumns + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "b.bookdate", ""),"clientage");
                //columns from trans
                string mTransColumns = clsGlobal.Get_TableColumns(mCommand, "rch_fplanattendances", "", "b", "");

                string mCommandText = ""
                    + "SELECT "
                    + mTransColumns + ","
                    + mClientColumns + " "
                    + "FROM rch_fplanattendances AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.clientcode=p.code";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("rch_fplanattendances");
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

        #region View_Archive
        public DataTable View_Archive(String mFilter, String mOrder)
        {
            String mFunctionName = "View_Archive";

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
                //columns from clients
                string mClientColumns = clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "client");
                mClientColumns = mClientColumns + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "clientfullname");
                mClientColumns = mClientColumns + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "b.bookdate",""), "clientage");
                //columns from trans
                string mTransColumns = clsGlobal.Get_TableColumns(mCommand, "rch_fplanattendancelog", "", "b", "");

                string mCommandText = ""
                    + "SELECT "
                    + mTransColumns + ","
                    + mClientColumns + " "
                    + "FROM rch_fplanattendancelog AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.clientcode=p.code";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("rch_fplanattendancelog");
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

        #region View_MethodsUsed
        public DataTable View_MethodsUsed(String mFilter, String mOrder)
        {
            String mFunctionName = "View_MethodsUsed";

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
                string mCommandText = "SELECT * FROM rch_fplanmethodslog";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("rch_fplanmethodslog");
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
        public AfyaPro_Types.clsResult Add(DateTime mTransDate, string mClientCode, DataTable mDtMethodsUsed,
            string mComplains, string mUserId)
        {
            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            bool mIsReAttendance = false;
            string mNewBooking = "";
            int mRecsAffected = -1;
            DataTable mDtAttendances = new DataTable("attendances");

            string mFunctionName = "Add";

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.rchfamilyplanning_add.ToString(), mUserId);
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
                mCommand.CommandText = "select * from patients where code='" + mClientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.RCH_ClientCodeDoesNotExist.ToString();
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

            #region check for reattendance

            try
            {
                mCommand.CommandText = "select * from rch_fplanattendances where clientcode='" + mClientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mIsReAttendance = true;
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

            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                int mYearPart = mTransDate.Year;
                int mMonthPart = mTransDate.Month;

                if (mIsReAttendance == true)
                {
                    //delete previous attendance
                    mCommand.CommandText = "delete from rch_fplanattendances where clientcode='" + mClientCode.Trim() + "'";
                    mCommand.ExecuteNonQuery();

                    //create new attendance
                    mCommand.CommandText = "insert into rch_fplanattendances(clientcode,bookdate,complains,userid) values('"
                    + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mComplains.Trim() + "','" 
                    + mUserId.Trim() + "')";
                    mCommand.ExecuteNonQuery();

                    //get attendance id created
                    mCommand.CommandText = "select autocode from rch_fplanattendances where clientcode='" + mClientCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAttendances);
                    if (mDtAttendances.Rows.Count > 0)
                    {
                        mNewBooking = mDtAttendances.Rows[0]["autocode"].ToString().Trim();
                    }
                    
                    //create a log for the new attendance
                    mCommand.CommandText = "insert into rch_fplanattendancelog(clientcode,bookdate,sysdate,booking,"
                    + "complains,userid,registrystatus,yearpart,monthpart) values('"
                    + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + clsGlobal.Saving_DateValue(DateTime.Now)
                    + ",'" + mNewBooking + "','" + mComplains.Trim() + "','" + mUserId.Trim() + "','"
                    + AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString() + "'," + mYearPart + "," + mMonthPart + ")";
                    mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit rch_fplanattendancelog

                    if (mRecsAffected > 0)
                    {
                        string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "rch_fplanattendancelog";
                        string mAuditingFields = clsGlobal.AuditingFields();
                        string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                            AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                        mCommand.CommandText = "insert into " + mAuditTableName + "(clientcode,bookdate,sysdate,booking,"
                        + "complains,userid,registrystatus,yearpart,monthpart," + mAuditingFields + ") values('"
                        + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + clsGlobal.Saving_DateValue(DateTime.Now)
                        + ",'" + mNewBooking + "','" + mComplains.Trim() + "','" + mUserId.Trim() + "','"
                        + AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString() + "'," + mYearPart + "," + mMonthPart + "," + mAuditingValues + ")";
                        mCommand.ExecuteNonQuery();
                    }

                    #endregion
                }
                else
                {
                    //delete previous attendance
                    mCommand.CommandText = "delete from rch_fplanattendances where clientcode='" + mClientCode.Trim() + "'";
                    mCommand.ExecuteNonQuery();

                    //create new attendance
                    mCommand.CommandText = "insert into rch_fplanattendances(clientcode,bookdate,complains,userid) values('"
                    + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mComplains.Trim() + "','" 
                    + mUserId.Trim() + "')";
                    mCommand.ExecuteNonQuery();

                    //get attendance id created
                    mCommand.CommandText = "select autocode from rch_fplanattendances where clientcode='" + mClientCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAttendances);
                    if (mDtAttendances.Rows.Count > 0)
                    {
                        mNewBooking = mDtAttendances.Rows[0]["autocode"].ToString().Trim();
                    }

                    //create a log for the new attendance
                    mCommand.CommandText = "insert into rch_fplanattendancelog(clientcode,bookdate,sysdate,booking,"
                    + "complains,userid,registrystatus,yearpart,monthpart) values('"
                    + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + clsGlobal.Saving_DateValue(DateTime.Now)
                    + ",'" + mNewBooking + "','" + mComplains.Trim() + "','" + mUserId.Trim() + "','"
                    + AfyaPro_Types.clsEnums.RegistryStatus.New.ToString() + "'," + mYearPart + "," + mMonthPart + ")";
                    mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit facilitybookinglog

                    if (mRecsAffected > 0)
                    {
                        string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "rch_fplanattendancelog";
                        string mAuditingFields = clsGlobal.AuditingFields();
                        string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                            AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                        mCommand.CommandText = "insert into " + mAuditTableName + "(clientcode,bookdate,sysdate,booking,"
                        + "complains,userid,registrystatus,yearpart,monthpart" + "," + mAuditingFields + ") values('"
                        + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + clsGlobal.Saving_DateValue(DateTime.Now)
                        + ",'" + mNewBooking + "','" + mComplains.Trim() + "','" + mUserId.Trim() + "','"
                        + AfyaPro_Types.clsEnums.RegistryStatus.New.ToString() + "'," + mYearPart + "," + mMonthPart + "," + mAuditingValues + ")";
                        mCommand.ExecuteNonQuery();
                    }

                    #endregion
                }

                #region methods used

                mCommand.CommandText = clsGlobal.Get_DeleteStatement(
                    "rch_fplanmethodslog", 
                    "clientcode='" + mClientCode.Trim() + "' and booking='" + mNewBooking + "'");
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtMethodsUsed.Rows)
                {
                    string mCode = mDataRow["fieldname"].ToString().Trim();
                    double mQuantity = Convert.ToDouble(mDataRow["fieldvalue"]);

                    if (mQuantity > 0)
                    {
                        List<clsDataField> mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mNewBooking));
                        mDataFields.Add(new clsDataField("clientcode", DataTypes.dbstring.ToString(), mClientCode));
                        mDataFields.Add(new clsDataField("methodcode", DataTypes.dbstring.ToString(), mCode));
                        mDataFields.Add(new clsDataField("quantity", DataTypes.dbdecimal.ToString(), mQuantity));

                        mCommand.CommandText = clsGlobal.Get_InsertStatement("rch_fplanmethodslog", mDataFields);
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region patient servicetype

                mCommand.CommandText = "update patients set servicetype=" + (int)AfyaPro_Types.clsEnums.RCHServices.familyplanning
                    + " where code='" + mClientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                #endregion

                //this patient has to be available when doing searching
                mCommand.CommandText = "update patients set fromrch=1 where code='" + mClientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                mTrans.Commit();

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

        #region Edit
        public AfyaPro_Types.clsResult Edit(DateTime mTransDate, string mBooking, string mClientCode, DataTable mDtMethodsUsed,
            string mComplains, string mUserId)
        {
            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            int mRecsAffected = -1;

            string mFunctionName = "Edit";

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.rchfamilyplanning_edit.ToString(), mUserId);
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
                mCommand.CommandText = "select * from patients where code='" + mClientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.RCH_ClientCodeDoesNotExist.ToString();
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

            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                int mYearPart = mTransDate.Year;
                int mMonthPart = mTransDate.Month;

                //rch_fplanattendances
                mCommand.CommandText = "update rch_fplanattendances set clientcode='" + mClientCode.Trim()
                + "',bookdate=" + clsGlobal.Saving_DateValue(mTransDate) + ",complains='" + mComplains.Trim() 
                + "',userid='" + mUserId.Trim() + "' where autocode=" + mBooking.Trim();
                mCommand.ExecuteNonQuery();

                //rch_fplanattendancelog
                mCommand.CommandText = "update rch_fplanattendancelog set clientcode='" + mClientCode.Trim()
                + "',bookdate=" + clsGlobal.Saving_DateValue(mTransDate) + ",sysdate=" + clsGlobal.Saving_DateValue(DateTime.Now)
                + ",complains='" + mComplains.Trim() + "',userid='" + mUserId.Trim() + "',yearpart=" 
                + mYearPart + ",monthpart=" + mMonthPart + " where booking='" + mBooking.Trim() + "'";
                mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit rch_fplanattendancelog
                if (mRecsAffected > 0)
                {
                    DataTable mDtBookingLog = new DataTable("rch_fplanattendancelog");
                    mCommand.CommandText = "select * from rch_fplanattendancelog where booking='" + mBooking.Trim()
                    + "' and clientcode='" + mClientCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtBookingLog);

                    mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtBookingLog, "rch_fplanattendancelog",
                    mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                    mCommand.ExecuteNonQuery();
                }
                #endregion

                #region methods used

                mCommand.CommandText = clsGlobal.Get_DeleteStatement(
                    "rch_fplanmethodslog",
                    "clientcode='" + mClientCode.Trim() + "' and booking='" + mBooking + "'");
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtMethodsUsed.Rows)
                {
                    string mCode = mDataRow["fieldname"].ToString().Trim();
                    double mQuantity = Convert.ToDouble(mDataRow["fieldvalue"]);

                    if (mQuantity > 0)
                    {
                        List<clsDataField> mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBooking));
                        mDataFields.Add(new clsDataField("clientcode", DataTypes.dbstring.ToString(), mClientCode));
                        mDataFields.Add(new clsDataField("methodcode", DataTypes.dbstring.ToString(), mCode));
                        mDataFields.Add(new clsDataField("quantity", DataTypes.dbdecimal.ToString(), mQuantity));

                        mCommand.CommandText = clsGlobal.Get_InsertStatement("rch_fplanmethodslog", mDataFields);
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                mTrans.Commit();

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

        #region Delete
        public AfyaPro_Types.clsResult Delete(DateTime mTransDate, string mBooking, string mClientCode, string mUserId)
        {
            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            int mRecsAffected = -1;

            string mFunctionName = "Delete";

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.rchfamilyplanning_delete.ToString(), mUserId);
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
                mCommand.CommandText = "select * from patients where code='" + mClientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.RCH_ClientCodeDoesNotExist.ToString();
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

            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                int mYearPart = mTransDate.Year;
                int mMonthPart = mTransDate.Month;

                DataTable mDtAttendanceLog = new DataTable("rch_fplanattendancelog");
                mCommand.CommandText = "select * from rch_fplanattendancelog where booking='" + mBooking.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtAttendanceLog);

                //rch_fplanattendancelog
                mCommand.CommandText = "delete from rch_fplanattendancelog where booking='" + mBooking.Trim() + "'";
                mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit rch_fplanattendancelog
                if (mRecsAffected > 0)
                {
                    mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtAttendanceLog, "rch_fplanattendancelog",
                    mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Deleted.ToString());
                    mCommand.ExecuteNonQuery();
                }
                #endregion

                //rch_fplanmethodslog
                mCommand.CommandText = "delete from rch_fplanmethodslog where booking='" + mBooking.Trim() + "' and clientcode='" + mClientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                mTrans.Commit();

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
