﻿/*
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
    public class clsRCHChildren : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsRCHChildren";

        #endregion

        #region Get_DPTs
        public DataTable Get_DPTs(string mLanguageName, string mGridName)
        {
            String mFunctionName = "Get_DPTs";

            try
            {
                DataTable mDataTable = new DataTable("dpts");
                mDataTable.Columns.Add("code", typeof(System.String));
                mDataTable.Columns.Add("description", typeof(System.String));
                mDataTable.Columns.Add("fieldname", typeof(System.String));
                mDataTable.RemotingFormat = SerializationFormat.Binary;

                DataRow mNewRow;

                #region load dpts

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "";
                mNewRow["description"] = "";
                mNewRow["fieldname"] = "";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "dpt1";
                mNewRow["description"] = "DPT 1";
                mNewRow["fieldname"] = AfyaPro_Types.clsEnums.RCHDPTs.dpt1.ToString();
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "dpt2";
                mNewRow["description"] = "DPT 2";
                mNewRow["fieldname"] = AfyaPro_Types.clsEnums.RCHDPTs.dpt2.ToString();
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "dpt3";
                mNewRow["description"] = "DPT 3";
                mNewRow["fieldname"] = AfyaPro_Types.clsEnums.RCHDPTs.dpt3.ToString();
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                #endregion

                #region interprete data

                if (mLanguageName.Trim() != "" && mGridName.Trim() != "")
                {
                    try
                    {
                        var mCurrLang = from lang in System.Xml.Linq.XElement.Load(
                            AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Lang\" + mLanguageName + ".xml").Elements(mGridName)
                                        select lang;

                        DataTable mDtLanguage = new DataTable("language");
                        mDtLanguage.Columns.Add("controlname");
                        mDtLanguage.Columns.Add("description");

                        foreach (var mElement in mCurrLang)
                        {
                            mNewRow = mDtLanguage.NewRow();
                            mNewRow["controlname"] = (string)mElement.Element("controlname").Value.Trim().ToLower();
                            mNewRow["description"] = (string)mElement.Element("description").Value.Trim();
                            mDtLanguage.Rows.Add(mNewRow);
                            mDtLanguage.AcceptChanges();
                        }

                        DataView mDvLanguage = new DataView();
                        mDvLanguage.Table = mDtLanguage;
                        mDvLanguage.Sort = "controlname";

                        foreach (DataRow mDataRow in mDataTable.Rows)
                        {
                            int mRowIndex = mDvLanguage.Find(mDataRow["code"].ToString().Trim());
                            if (mRowIndex >= 0)
                            {
                                mDataRow.BeginEdit();
                                mDataRow["description"] = mDvLanguage[mRowIndex]["description"].ToString().Trim();
                                mDataRow.EndEdit();
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
        }
        #endregion

        #region Get_OPVs
        public DataTable Get_OPVs(string mLanguageName, string mGridName)
        {
            String mFunctionName = "Get_OPVs";

            try
            {
                DataTable mDataTable = new DataTable("opvs");
                mDataTable.Columns.Add("code", typeof(System.String));
                mDataTable.Columns.Add("description", typeof(System.String));
                mDataTable.Columns.Add("fieldname", typeof(System.String));
                mDataTable.RemotingFormat = SerializationFormat.Binary;

                DataRow mNewRow;

                #region load opvs

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "";
                mNewRow["description"] = "";
                mNewRow["fieldname"] = "";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "opv0";
                mNewRow["description"] = "OPV 0";
                mNewRow["fieldname"] = AfyaPro_Types.clsEnums.RCHOPVs.opv0.ToString();
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "opv1";
                mNewRow["description"] = "OPV 1";
                mNewRow["fieldname"] = AfyaPro_Types.clsEnums.RCHOPVs.opv1.ToString();
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "opv2";
                mNewRow["description"] = "OPV 2";
                mNewRow["fieldname"] = AfyaPro_Types.clsEnums.RCHOPVs.opv2.ToString();
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "opv3";
                mNewRow["description"] = "OPV 3";
                mNewRow["fieldname"] = AfyaPro_Types.clsEnums.RCHOPVs.opv3.ToString();
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                #endregion

                #region interprete data

                if (mLanguageName.Trim() != "" && mGridName.Trim() != "")
                {
                    try
                    {
                        var mCurrLang = from lang in System.Xml.Linq.XElement.Load(
                            AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Lang\" + mLanguageName + ".xml").Elements(mGridName)
                                        select lang;

                        DataTable mDtLanguage = new DataTable("language");
                        mDtLanguage.Columns.Add("controlname");
                        mDtLanguage.Columns.Add("description");

                        foreach (var mElement in mCurrLang)
                        {
                            mNewRow = mDtLanguage.NewRow();
                            mNewRow["controlname"] = (string)mElement.Element("controlname").Value.Trim().ToLower();
                            mNewRow["description"] = (string)mElement.Element("description").Value.Trim();
                            mDtLanguage.Rows.Add(mNewRow);
                            mDtLanguage.AcceptChanges();
                        }

                        DataView mDvLanguage = new DataView();
                        mDvLanguage.Table = mDtLanguage;
                        mDvLanguage.Sort = "controlname";

                        foreach (DataRow mDataRow in mDataTable.Rows)
                        {
                            int mRowIndex = mDvLanguage.Find(mDataRow["code"].ToString().Trim());
                            if (mRowIndex >= 0)
                            {
                                mDataRow.BeginEdit();
                                mDataRow["description"] = mDvLanguage[mRowIndex]["description"].ToString().Trim();
                                mDataRow.EndEdit();
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
        }
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
                string mTransColumns = clsGlobal.Get_TableColumns(mCommand, "rch_childrenattendances", "", "b", "");

                string mCommandText = ""
                    + "SELECT "
                    + mTransColumns + ","
                    + mClientColumns + " "
                    + "FROM rch_childrenattendances AS b "
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

                DataTable mDataTable = new DataTable("rch_childrenattendances");
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
                string mTransColumns = clsGlobal.Get_TableColumns(mCommand, "rch_childrenattendancelog", "", "b", "");

                string mCommandText = ""
                    + "SELECT "
                    + mTransColumns + ","
                    + mClientColumns + " "
                    + "FROM rch_childrenattendancelog AS b "
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

                DataTable mDataTable = new DataTable("rch_childrenattendancelog");
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
        public AfyaPro_Types.clsResult Add(
            DateTime mTransDate, 
            string mClientCode, 
            string mMotherName, 
            int mMotherNNTVaccine, 
            double mWeight, 
            string mUserId)
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.rchchildren_add.ToString(), mUserId);
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
                mCommand.CommandText = "select * from rch_childrenattendances where clientcode='" + mClientCode.Trim() + "'";
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

                mCommand.CommandText = "update patients set mothername='" + mMotherName.Trim() 
                + "',mothernntvaccine=" + mMotherNNTVaccine + " where code='" + mClientCode.Trim() + "'";
                mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit patients

                if (mRecsAffected > 0)
                {
                    DataTable mDtClients = new DataTable("patients");
                    mCommand.CommandText = "select * from patients where code='" + mClientCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtClients);

                    mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtClients, "patients",
                    mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                if (mIsReAttendance == true)
                {
                    //delete previous attendance
                    mCommand.CommandText = "delete from rch_childrenattendances where clientcode='" + mClientCode.Trim() + "'";
                    mCommand.ExecuteNonQuery();

                    //create new attendance
                    mCommand.CommandText = "insert into rch_childrenattendances(clientcode,bookdate,weight,userid) values('" 
                    + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + mWeight + ",'" + mUserId.Trim() + "')";
                    mCommand.ExecuteNonQuery();

                    //get attendance id created
                    mCommand.CommandText = "select autocode from rch_childrenattendances where clientcode='" + mClientCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAttendances);
                    if (mDtAttendances.Rows.Count > 0)
                    {
                        mNewBooking = mDtAttendances.Rows[0]["autocode"].ToString().Trim();
                    }

                    //create a log for the new attendance
                    mCommand.CommandText = "insert into rch_childrenattendancelog(clientcode,bookdate,sysdate,booking,"
                    + "weight,userid,registrystatus,yearpart,monthpart" 
                    + ") values('" + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + ","
                    + clsGlobal.Saving_DateValue(DateTime.Now) + ",'" + mNewBooking + "',"
                    + mWeight + ",'" + mUserId.Trim() + "','"
                    + AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString() + "'," + mYearPart + "," + mMonthPart + ")";
                    mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit rch_childrenattendancelog

                    if (mRecsAffected > 0)
                    {
                        string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "rch_childrenattendancelog";
                        string mAuditingFields = clsGlobal.AuditingFields();
                        string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                            AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                        mCommand.CommandText = "insert into " + mAuditTableName + "(clientcode,bookdate,sysdate,booking,"
                        + "weight,userid,registrystatus,yearpart,monthpart,"
                        + mAuditingFields + ") values('" + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + ","
                        + clsGlobal.Saving_DateValue(DateTime.Now) + ",'" + mNewBooking + "',"
                        + mWeight + ",'" + mUserId.Trim() + "','"
                        + AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString() + "'," + mYearPart + "," + mMonthPart + "," + mAuditingValues + ")";
                        mCommand.ExecuteNonQuery();
                    }

                    #endregion
                }
                else
                {
                    //delete previous attendance
                    mCommand.CommandText = "delete from rch_childrenattendances where clientcode='" + mClientCode.Trim() + "'";
                    mCommand.ExecuteNonQuery();

                    //create new attendance
                    mCommand.CommandText = "insert into rch_childrenattendances(clientcode,bookdate,weight,userid) values('"
                    + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + mWeight + ",'" + mUserId.Trim() + "')";
                    mCommand.ExecuteNonQuery();

                    //get attendance id created
                    mCommand.CommandText = "select autocode from rch_childrenattendances where clientcode='" + mClientCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAttendances);
                    if (mDtAttendances.Rows.Count > 0)
                    {
                        mNewBooking = mDtAttendances.Rows[0]["autocode"].ToString().Trim();
                    }

                    //create a log for the new attendance
                    mCommand.CommandText = "insert into rch_childrenattendancelog(clientcode,bookdate,sysdate,booking,"
                    + "weight,userid,registrystatus,yearpart,monthpart"
                    + ") values('" + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + ","
                    + clsGlobal.Saving_DateValue(DateTime.Now) + ",'" + mNewBooking + "'," + mWeight + ",'" + mUserId.Trim() + "','"
                    + AfyaPro_Types.clsEnums.RegistryStatus.New.ToString() + "'," + mYearPart + "," + mMonthPart + ")";
                    mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit rch_childrenattendancelog

                    if (mRecsAffected > 0)
                    {
                        string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "rch_childrenattendancelog";
                        string mAuditingFields = clsGlobal.AuditingFields();
                        string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                            AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                        mCommand.CommandText = "insert into " + mAuditTableName + "(clientcode,bookdate,sysdate,booking,"
                        + "weight,userid,registrystatus,yearpart,monthpart,"
                        + mAuditingFields + ") values('" + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + ","
                        + clsGlobal.Saving_DateValue(DateTime.Now) + ",'" + mNewBooking + "'," + mWeight + ",'" + mUserId.Trim() + "','"
                        + AfyaPro_Types.clsEnums.RegistryStatus.New.ToString() + "'," + mYearPart + "," + mMonthPart + "," + mAuditingValues + ")";
                        mCommand.ExecuteNonQuery();
                    }

                    #endregion
                }

                #region patient servicetype

                mCommand.CommandText = "update patients set servicetype=" + (int)AfyaPro_Types.clsEnums.RCHServices.childrenhealth
                    + " where code='" + mClientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                #endregion

                //this patient has to be available when doing searching
                mCommand.CommandText = "update patients set fromrch=1 where code='" + mClientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                mTrans.Commit();

                mResult.GeneratedCode = mNewBooking.Trim();
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
        public AfyaPro_Types.clsResult Edit(
            DateTime mTransDate, 
            string mBooking, 
            string mClientCode, 
            string mMotherName, 
            int mMotherNNTVaccine, 
            double mWeight,
            string mUserId)
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.rchchildren_edit.ToString(), mUserId);
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

                mCommand.CommandText = "update patients set mothername='" + mMotherName.Trim()
                + "',mothernntvaccine=" + mMotherNNTVaccine + " where code='" + mClientCode.Trim() + "'";
                mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit patients

                if (mRecsAffected > 0)
                {
                    DataTable mDtClients = new DataTable("patients");
                    mCommand.CommandText = "select * from patients where code='" + mClientCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtClients);

                    mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtClients, "patients",
                    mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                //rch_childrenattendances
                mCommand.CommandText = "update rch_childrenattendances set clientcode='" + mClientCode.Trim()
                + "',bookdate=" + clsGlobal.Saving_DateValue(mTransDate)
                + ",weight=" + mWeight + ",userid='" + mUserId.Trim() 
                + "' where autocode=" + mBooking.Trim();
                mCommand.ExecuteNonQuery();

                //rch_childrenattendancelog
                mCommand.CommandText = "update rch_childrenattendancelog set clientcode='" + mClientCode.Trim()
                + "',bookdate=" + clsGlobal.Saving_DateValue(mTransDate) 
                + ",sysdate=" + clsGlobal.Saving_DateValue(DateTime.Now)
                + ",weight=" + mWeight + ",userid='" + mUserId.Trim()
                + "',yearpart=" + mYearPart + ",monthpart=" + mMonthPart
                + " where booking='" + mBooking.Trim() + "'";
                mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit rch_childrenattendancelog
                if (mRecsAffected > 0)
                {
                    DataTable mDtBookingLog = new DataTable("rch_childrenattendancelog");
                    mCommand.CommandText = "select * from rch_childrenattendancelog where booking='" + mBooking.Trim()
                    + "' and clientcode='" + mClientCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtBookingLog);

                    mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtBookingLog, "rch_childrenattendancelog",
                    mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                    mCommand.ExecuteNonQuery();
                }
                #endregion

                mTrans.Commit();

                mResult.GeneratedCode = mBooking.Trim();
                return mResult;
            }
            catch (OdbcException ex)
            {
                clsGlobal.Write_Error("","",mCommand.CommandText);
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.rchchildren_delete.ToString(), mUserId);
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

                DataTable mDtAttendanceLog = new DataTable("rch_childrenattendancelog");
                mCommand.CommandText = "select * from rch_childrenattendancelog where booking='" + mBooking.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtAttendanceLog);

                //rch_childrenattendances
                mCommand.CommandText = "delete from rch_childrenattendances where autocode=" + mBooking;
                mCommand.ExecuteNonQuery();

                //rch_childrenattendancelog
                mCommand.CommandText = "delete from rch_childrenattendancelog where booking='" + mBooking.Trim() + "'";
                mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit rch_childrenattendancelog
                if (mRecsAffected > 0)
                {
                    mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtAttendanceLog, "rch_childrenattendancelog",
                    mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Deleted.ToString());
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
    }
}
