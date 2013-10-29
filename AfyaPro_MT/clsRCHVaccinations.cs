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
    public class clsRCHVaccinations : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsRCHVaccinations";

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
                mClientColumns = mClientColumns + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "b.bookdate",""), "clientage");
                //columns from trans
                string mTransColumns = clsGlobal.Get_TableColumns(mCommand, "rch_vaccinations", "", "b", "");

                string mCommandText = ""
                    + "SELECT "
                    + mTransColumns + ","
                    + mClientColumns + " "
                    + "FROM rch_vaccinations AS b "
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

                DataTable mDataTable = new DataTable("rch_vaccinations");
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
                mClientColumns = mClientColumns + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "b.bookdate", ""),"clientage");
                //columns from trans
                string mTransColumns = clsGlobal.Get_TableColumns(mCommand, "rch_vaccinationslog", "", "b", "");

                string mCommandText = ""
                    + "SELECT "
                    + mTransColumns + ","
                    + mClientColumns + " "
                    + "FROM rch_vaccinationslog AS b "
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

                DataTable mDataTable = new DataTable("rch_vaccinationslog");
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

        #region View_VaccinesUsed
        public DataTable View_VaccinesUsed(String mFilter, String mOrder)
        {
            String mFunctionName = "View_VaccinesUsed";

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
                string mCommandText = "SELECT * FROM rch_vaccineslog";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("rch_vaccineslog");
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
        public AfyaPro_Types.clsResult Add(DateTime mTransDate, string mClientCode, DataTable mDtVaccines, string mUserId, int mServiceType = -1,string mForeignBooking = "")
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.rchvaccinations_add.ToString(), mUserId);
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
                if (mDataReader.Read() == true)
                {
                    if (mServiceType == -1)
                    {
                        mServiceType = mDataReader["servicetype"] == null ? -1 : Convert.ToInt16(mDataReader["servicetype"]);
                    }
                }
                else
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
                mCommand.CommandText = "select * from rch_vaccinations where clientcode='" + mClientCode.Trim() + "'";
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

            #region check for foreign booking

            if (mServiceType != -1 && mForeignBooking.Trim() == "")
            {
                try
                {
                    if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.familyplanning)
                    {
                        mCommand.CommandText = "select * from rch_fplanattendances where clientcode='" + mClientCode.Trim() + "'";
                        mDataReader = mCommand.ExecuteReader();
                        if (mDataReader.Read() == true)
                        {
                            mForeignBooking = mDataReader["autocode"].ToString().Trim();
                        }
                    }
                    else if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.antenatalcare)
                    {
                        mCommand.CommandText = "select * from rch_antenatalattendances where clientcode='" + mClientCode.Trim() + "'";
                        mDataReader = mCommand.ExecuteReader();
                        if (mDataReader.Read() == true)
                        {
                            mForeignBooking = mDataReader["autocode"].ToString().Trim();
                        }
                    }
                    else if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.postnatalcare)
                    {
                        mCommand.CommandText = "select * from rch_postnatalattendances where clientcode='" + mClientCode.Trim() + "'";
                        mDataReader = mCommand.ExecuteReader();
                        if (mDataReader.Read() == true)
                        {
                            mForeignBooking = mDataReader["autocode"].ToString().Trim();
                        }
                    }
                    else if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.childrenhealth)
                    {
                        mCommand.CommandText = "select * from rch_childrenattendances where clientcode='" + mClientCode.Trim() + "'";
                        mDataReader = mCommand.ExecuteReader();
                        if (mDataReader.Read() == true)
                        {
                            mForeignBooking = mDataReader["autocode"].ToString().Trim();
                        }
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

            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                int mYearPart = mTransDate.Year;
                int mMonthPart = mTransDate.Month;

                string mBookingField = "";
                string mBookingFieldValue = "";

                if (mServiceType == -1)
                {
                    mServiceType = (int)AfyaPro_Types.clsEnums.RCHServices.vaccinations;
                }

                if (mServiceType != -1 && mForeignBooking.Trim() != "")
                {
                    if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.familyplanning)
                    {
                        mBookingField = ",fplanbooking";
                        mBookingFieldValue = ",'" + mForeignBooking + "'";
                    }
                    else if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.antenatalcare)
                    {
                        mBookingField = ",antenatalbooking";
                        mBookingFieldValue = ",'" + mForeignBooking + "'";
                    }
                    else if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.postnatalcare)
                    {
                        mBookingField = ",postnatalbooking";
                        mBookingFieldValue = ",'" + mForeignBooking + "'";
                    }
                    else if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.childrenhealth)
                    {
                        mBookingField = ",childrenbooking";
                        mBookingFieldValue = ",'" + mForeignBooking + "'";
                    }
                }

                if (mIsReAttendance == true)
                {
                    //delete previous attendance
                    mCommand.CommandText = "delete from rch_vaccinations where clientcode='" + mClientCode.Trim() + "'";
                    mCommand.ExecuteNonQuery();

                    //create new attendance
                    mCommand.CommandText = "insert into rch_vaccinations(clientcode,bookdate,servicetype,userid) values('"
                    + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + mServiceType + ",'" + mUserId.Trim() + "')";
                    mCommand.ExecuteNonQuery();

                    //get attendance id created
                    mCommand.CommandText = "select autocode from rch_vaccinations where clientcode='" + mClientCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAttendances);
                    if (mDtAttendances.Rows.Count > 0)
                    {
                        mNewBooking = mDtAttendances.Rows[0]["autocode"].ToString().Trim();
                    }

                    //create a log for the new attendance
                    mCommand.CommandText = "insert into rch_vaccinationslog(clientcode,bookdate,sysdate,booking,"
                    + "servicetype,userid,registrystatus,yearpart,monthpart" + mBookingField + ") values('"
                    + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + clsGlobal.Saving_DateValue(DateTime.Now)
                    + ",'" + mNewBooking + "'," + mServiceType + ",'" + mUserId.Trim() + "','"
                    + AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString() + "'," + mYearPart + "," + mMonthPart + mBookingFieldValue + ")";
                    mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit rch_vaccinationslog

                    if (mRecsAffected > 0)
                    {
                        string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "rch_vaccinationslog";
                        string mAuditingFields = clsGlobal.AuditingFields();
                        string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                            AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                        mCommand.CommandText = "insert into " + mAuditTableName + "(clientcode,bookdate,sysdate,booking,"
                        + "servicetype,userid,registrystatus,yearpart,monthpart," + mAuditingFields + mBookingField + ") values('"
                        + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + clsGlobal.Saving_DateValue(DateTime.Now)
                        + ",'" + mNewBooking + "'," + mServiceType + ",'" + mUserId.Trim() + "','"
                        + AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString() + "'," + mYearPart + "," + mMonthPart + ","
                        + mAuditingValues + mBookingFieldValue + ")";
                        mCommand.ExecuteNonQuery();
                    }

                    #endregion
                }
                else
                {
                    //delete previous attendance
                    mCommand.CommandText = "delete from rch_vaccinations where clientcode='" + mClientCode.Trim() + "'";
                    mCommand.ExecuteNonQuery();

                    //create new attendance
                    mCommand.CommandText = "insert into rch_vaccinations(clientcode,bookdate,servicetype,userid) values('"
                    + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + mServiceType + ",'"
                    + mUserId.Trim() + "')";
                    mCommand.ExecuteNonQuery();

                    //get attendance id created
                    mCommand.CommandText = "select autocode from rch_vaccinations where clientcode='" + mClientCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAttendances);
                    if (mDtAttendances.Rows.Count > 0)
                    {
                        mNewBooking = mDtAttendances.Rows[0]["autocode"].ToString().Trim();
                    }

                    //create a log for the new attendance
                    mCommand.CommandText = "insert into rch_vaccinationslog(clientcode,bookdate,sysdate,booking,"
                    + "servicetype,userid,registrystatus,yearpart,monthpart" + mBookingField + ") values('"
                    + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + clsGlobal.Saving_DateValue(DateTime.Now)
                    + ",'" + mNewBooking + "'," + mServiceType + ",'" + mUserId.Trim() + "','"
                    + AfyaPro_Types.clsEnums.RegistryStatus.New.ToString() + "'," + mYearPart + "," + mMonthPart + mBookingFieldValue+ ")";
                    mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit facilitybookinglog

                    if (mRecsAffected > 0)
                    {
                        string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "rch_vaccinationslog";
                        string mAuditingFields = clsGlobal.AuditingFields();
                        string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                            AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                        mCommand.CommandText = "insert into " + mAuditTableName + "(clientcode,bookdate,sysdate,booking,"
                        + "servicetype,userid,registrystatus,yearpart,monthpart" + "," + mAuditingFields + mBookingField + ") values('"
                        + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + clsGlobal.Saving_DateValue(DateTime.Now)
                        + ",'" + mNewBooking + "'," + mServiceType + ",'" + mUserId.Trim() + "','"
                        + AfyaPro_Types.clsEnums.RegistryStatus.New.ToString() + "'," + mYearPart + "," + mMonthPart + "," 
                        + mAuditingValues + mBookingFieldValue+ ")";
                        mCommand.ExecuteNonQuery();
                    }

                    #endregion
                }

                #region vaccines used

                mCommand.CommandText = clsGlobal.Get_DeleteStatement(
                    "rch_vaccineslog",
                    "clientcode='" + mClientCode.Trim() + "' and booking='" + mNewBooking + "'");
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtVaccines.Rows)
                {
                    string mCode = mDataRow["fieldname"].ToString().Trim();
                    string mRemarks = mDataRow["remarks"].ToString().Trim();

                    List<clsDataField> mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mNewBooking));
                    mDataFields.Add(new clsDataField("clientcode", DataTypes.dbstring.ToString(), mClientCode));
                    mDataFields.Add(new clsDataField("vaccinecode", DataTypes.dbstring.ToString(), mCode));
                    mDataFields.Add(new clsDataField("remarks", DataTypes.dbstring.ToString(), mRemarks));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("rch_vaccineslog", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

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
        public AfyaPro_Types.clsResult Edit(DateTime mTransDate, string mBooking, string mClientCode, DataTable mDtVaccines,
            string mUserId, int mServiceType = -1, string mForeignBooking = "")
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.rchvaccinations_edit.ToString(), mUserId);
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
                if (mDataReader.Read() == true)
                {
                    if (mServiceType == -1)
                    {
                        mServiceType = mDataReader["servicetype"] == null ? -1 : Convert.ToInt16(mDataReader["servicetype"]);
                    }
                }
                else
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

            #region check for foreign booking

            if (mServiceType != -1 && mForeignBooking.Trim() == "")
            {
                try
                {
                    if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.familyplanning)
                    {
                        mCommand.CommandText = "select * from rch_fplanattendances where clientcode='" + mClientCode.Trim() + "'";
                        mDataReader = mCommand.ExecuteReader();
                        if (mDataReader.Read() == true)
                        {
                            mForeignBooking = mDataReader["autocode"].ToString().Trim();
                        }
                    }
                    else if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.antenatalcare)
                    {
                        mCommand.CommandText = "select * from rch_antenatalattendances where clientcode='" + mClientCode.Trim() + "'";
                        mDataReader = mCommand.ExecuteReader();
                        if (mDataReader.Read() == true)
                        {
                            mForeignBooking = mDataReader["autocode"].ToString().Trim();
                        }
                    }
                    else if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.postnatalcare)
                    {
                        mCommand.CommandText = "select * from rch_postnatalattendances where clientcode='" + mClientCode.Trim() + "'";
                        mDataReader = mCommand.ExecuteReader();
                        if (mDataReader.Read() == true)
                        {
                            mForeignBooking = mDataReader["autocode"].ToString().Trim();
                        }
                    }
                    else if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.childrenhealth)
                    {
                        mCommand.CommandText = "select * from rch_childrenattendances where clientcode='" + mClientCode.Trim() + "'";
                        mDataReader = mCommand.ExecuteReader();
                        if (mDataReader.Read() == true)
                        {
                            mForeignBooking = mDataReader["autocode"].ToString().Trim();
                        }
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

            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                int mYearPart = mTransDate.Year;
                int mMonthPart = mTransDate.Month;

                if (mServiceType == -1)
                {
                    mServiceType = (int)AfyaPro_Types.clsEnums.RCHServices.vaccinations;
                }

                string mBookingFieldValue = "";

                if (mServiceType != -1 && mForeignBooking.Trim() != "")
                {
                    if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.familyplanning)
                    {
                        mBookingFieldValue = ",fplanbooking='" + mForeignBooking + "'";
                    }
                    else if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.antenatalcare)
                    {
                        mBookingFieldValue = ",antenatalbooking='" + mForeignBooking + "'";
                    }
                    else if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.postnatalcare)
                    {
                        mBookingFieldValue = ",postnatalbooking='" + mForeignBooking + "'";
                    }
                    else if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.childrenhealth)
                    {
                        mBookingFieldValue = ",childrenbooking='" + mForeignBooking + "'";
                    }
                }

                //rch_vaccinations
                mCommand.CommandText = "update rch_vaccinations set clientcode='" + mClientCode.Trim()
                + "',bookdate=" + clsGlobal.Saving_DateValue(mTransDate) + ",servicetype=" + mServiceType
                + ",userid='" + mUserId.Trim() + "' where autocode=" + mBooking.Trim();
                mCommand.ExecuteNonQuery();

                //rch_vaccinationslog
                mCommand.CommandText = "update rch_vaccinationslog set clientcode='" + mClientCode.Trim()
                + "',bookdate=" + clsGlobal.Saving_DateValue(mTransDate) + ",sysdate=" + clsGlobal.Saving_DateValue(DateTime.Now)
                + ",servicetype=" + mServiceType + ",userid='" + mUserId.Trim() + "',yearpart="
                + mYearPart + ",monthpart=" + mMonthPart + mBookingFieldValue + " where booking='" + mBooking.Trim() + "'";
                mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit rch_vaccinationslog
                if (mRecsAffected > 0)
                {
                    DataTable mDtBookingLog = new DataTable("rch_vaccinationslog");
                    mCommand.CommandText = "select * from rch_vaccinationslog where booking='" + mBooking.Trim()
                    + "' and clientcode='" + mClientCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtBookingLog);

                    mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtBookingLog, "rch_vaccinationslog",
                    mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                    mCommand.ExecuteNonQuery();
                }
                #endregion

                #region vaccines used

                mCommand.CommandText = clsGlobal.Get_DeleteStatement(
                    "rch_vaccineslog",
                    "clientcode='" + mClientCode.Trim() + "' and booking='" + mBooking + "'");
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtVaccines.Rows)
                {
                    string mCode = mDataRow["fieldname"].ToString().Trim();
                    string mRemarks = mDataRow["remarks"].ToString().Trim();

                    List<clsDataField> mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBooking));
                    mDataFields.Add(new clsDataField("clientcode", DataTypes.dbstring.ToString(), mClientCode));
                    mDataFields.Add(new clsDataField("vaccinecode", DataTypes.dbstring.ToString(), mCode));
                    mDataFields.Add(new clsDataField("remarks", DataTypes.dbstring.ToString(), mRemarks));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("rch_vaccineslog", mDataFields);
                    mCommand.ExecuteNonQuery();
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.rchvaccinations_delete.ToString(), mUserId);
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

                DataTable mDtAttendanceLog = new DataTable("rch_vaccinationslog");
                mCommand.CommandText = "select * from rch_vaccinationslog where booking='" + mBooking.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtAttendanceLog);

                //rch_vaccinationslog
                mCommand.CommandText = "delete from rch_vaccinationslog where booking='" + mBooking.Trim() + "'";
                mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit rch_vaccinationslog
                if (mRecsAffected > 0)
                {
                    mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtAttendanceLog, "rch_vaccinationslog",
                    mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Deleted.ToString());
                    mCommand.ExecuteNonQuery();
                }
                #endregion

                //rch_vaccineslog
                mCommand.CommandText = "delete from rch_vaccineslog where booking='" + mBooking.Trim() + "' and clientcode='" + mClientCode.Trim() + "'";
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
