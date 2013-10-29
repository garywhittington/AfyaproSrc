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
    public class clsRCHPostNatalCare : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsRCHPostNatalCare";

        #endregion

        #region Get_StillBirths
        public DataTable Get_StillBirths(string mLanguageName, string mGridName)
        {
            String mFunctionName = "Get_StillBirths";

            try
            {
                DataTable mDataTable = new DataTable("stillbirths");
                mDataTable.Columns.Add("code", typeof(System.String));
                mDataTable.Columns.Add("description", typeof(System.String));
                mDataTable.Columns.Add("fieldname", typeof(System.String));
                mDataTable.RemotingFormat = SerializationFormat.Binary;

                DataRow mNewRow;

                #region load still births

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "stillbirths0";
                mNewRow["description"] = "Fresh birth (FSB)";
                mNewRow["fieldname"] = AfyaPro_Types.clsEnums.RCHStillBirths.freshbirth.ToString();
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "stillbirths1";
                mNewRow["description"] = "Macerated (MSB)";
                mNewRow["fieldname"] = AfyaPro_Types.clsEnums.RCHStillBirths.maceratedbirth.ToString();
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

        #region Get_ChildConditions
        public DataTable Get_ChildConditions(string mLanguageName, string mGridName)
        {
            String mFunctionName = "Get_ChildConditions";

            try
            {
                DataTable mDataTable = new DataTable("childconditions");
                mDataTable.Columns.Add("code", typeof(System.String));
                mDataTable.Columns.Add("description", typeof(System.String));
                mDataTable.Columns.Add("fieldname", typeof(System.String));
                mDataTable.RemotingFormat = SerializationFormat.Binary;

                DataRow mNewRow;

                #region load child conditions

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "childcondition0";
                mNewRow["description"] = "Alive";
                mNewRow["fieldname"] = AfyaPro_Types.clsEnums.RCHChildConditions.live.ToString();
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "childcondition1";
                mNewRow["description"] = "Died before 24 hours";
                mNewRow["fieldname"] = AfyaPro_Types.clsEnums.RCHChildConditions.deathbefore24.ToString();
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "childcondition2";
                mNewRow["description"] = "Died after 24 hours";
                mNewRow["fieldname"] = AfyaPro_Types.clsEnums.RCHChildConditions.deathafter24.ToString();
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
                mClientColumns = mClientColumns + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "b.bookdate", ""),"clientage");
                //columns from trans
                string mTransColumns = clsGlobal.Get_TableColumns(mCommand, "rch_postnatalattendances", "", "b", "");

                string mCommandText = ""
                    + "SELECT "
                    + mTransColumns + ","
                    + mClientColumns + " "
                    + "FROM rch_postnatalattendances AS b "
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

                DataTable mDataTable = new DataTable("rch_postnatalattendances");
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
                string mTransColumns = clsGlobal.Get_TableColumns(mCommand, "rch_postnatalattendancelog", "", "b", "");

                string mCommandText = ""
                    + "SELECT "
                    + mTransColumns + ","
                    + mClientColumns + " "
                    + "FROM rch_postnatalattendancelog AS b "
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

                DataTable mDataTable = new DataTable("rch_postnatalattendancelog");
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

        #region View_Children
        public DataTable View_Children(String mFilter, String mOrder)
        {
            String mFunctionName = "View_Children";

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
                string mCommandText = "select * from rch_postnatalchildren";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("rch_postnatalchildren");
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

        #region View_Complications
        public DataTable View_Complications(String mFilter, String mOrder)
        {
            String mFunctionName = "View_Complications";

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
                string mCommandText = "SELECT * FROM rch_birthcomplicationslog";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("rch_birthcomplicationslog");
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
        public AfyaPro_Types.clsResult Add(DateTime mTransDate, string mClientCode, string mBirthMethod, 
            DataTable mDtComplications, int mGravida, int mPara, DateTime mAdmissionDate, string mDischargeStatus,
            string mAttendantId, string mAttendantName, int mFromAntenatal, DataTable mDtChildren, string mUserId)
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.rchpostnatal_add.ToString(), mUserId);
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
                mCommand.CommandText = "select * from rch_postnatalattendances where clientcode='" + mClientCode.Trim() + "'";
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
                    mCommand.CommandText = "delete from rch_postnatalattendances where clientcode='" + mClientCode.Trim() + "'";
                    mCommand.ExecuteNonQuery();

                    //create new attendance
                    mCommand.CommandText = "insert into rch_postnatalattendances(clientcode,bookdate,gravida,para,"
                    + "admissiondate,noofchildren,dischargestatus,attendantid,attendantname,birthmethod,fromantenatal,userid"
                    + ") values('" + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate)
                    + "," + mGravida + "," + mPara + "," + clsGlobal.Saving_DateValueNullable(mAdmissionDate) + ","
                    + mDtChildren.Rows.Count + ",'" + mDischargeStatus.Trim() + "','" + mAttendantId.Trim() + "','"
                    + mAttendantName.Trim() + "','" + mBirthMethod.Trim() + "'," + mFromAntenatal + ",'" + mUserId + "')";
                    mCommand.ExecuteNonQuery();

                    //get attendance id created
                    mCommand.CommandText = "select autocode from rch_postnatalattendances where clientcode='" + mClientCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAttendances);
                    if (mDtAttendances.Rows.Count > 0)
                    {
                        mNewBooking = mDtAttendances.Rows[0]["autocode"].ToString().Trim();
                    }

                    //create a log for the new attendance
                    mCommand.CommandText = "insert into rch_postnatalattendancelog(clientcode,booking,bookdate,sysdate,gravida,para,"
                    + "admissiondate,noofchildren,dischargestatus,attendantid,attendantname,birthmethod,fromantenatal,userid,"
                    + "registrystatus,yearpart,monthpart) values('" + mClientCode.Trim() + "','"
                    + mNewBooking + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + clsGlobal.Saving_DateValue(DateTime.Now)
                    + "," + mGravida + "," + mPara + "," + clsGlobal.Saving_DateValueNullable(mAdmissionDate) + ","
                    + mDtChildren.Rows.Count + ",'" + mDischargeStatus.Trim() + "','" + mAttendantId.Trim() + "','"
                    + mAttendantName.Trim() + "','" + mBirthMethod.Trim() + "'," + mFromAntenatal + ",'" + mUserId
                    + "','" + AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString() + "'," + mYearPart + "," 
                    + mMonthPart + ")";
                    mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit rch_postnatalattendancelog

                    if (mRecsAffected > 0)
                    {
                        string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "rch_postnatalattendancelog";
                        string mAuditingFields = clsGlobal.AuditingFields();
                        string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                            AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                        mCommand.CommandText = "insert into " + mAuditTableName + "(clientcode,booking,bookdate,sysdate,gravida,para,"
                        + "admissiondate,noofchildren,dischargestatus,attendantid,attendantname,birthmethod,fromantenatal,userid,"
                        + "registrystatus,yearpart,monthpart," + mAuditingFields + ") values('" 
                        + mClientCode.Trim() + "','" + mNewBooking + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," 
                        + clsGlobal.Saving_DateValue(DateTime.Now) + "," + mGravida + "," + mPara + "," + clsGlobal.Saving_DateValueNullable(mAdmissionDate) 
                        + "," + mDtChildren.Rows.Count + ",'" + mDischargeStatus.Trim() + "','" + mAttendantId.Trim() + "','"
                        + mAttendantName.Trim() + "','" + mBirthMethod.Trim() + "'," + mFromAntenatal + ",'" + mUserId
                        + "','" + AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString() + "'," + mYearPart + ","
                        + mMonthPart + "," + mAuditingValues + ")";
                        mCommand.ExecuteNonQuery();
                    }

                    #endregion

                    #region children

                    mCommand.CommandText = "delete from rch_postnatalchildren where clientcode='" + mClientCode.Trim() 
                    + "' and booking='" + mNewBooking + "'";
                    mCommand.ExecuteNonQuery();

                    foreach (DataRow mDataRow in mDtChildren.Rows)
                    {
                        string mDeliveryDate = clsGlobal.Saving_DateValueNullable(mDataRow["deliverydate"]);
                        string mGender = mDataRow["gender"].ToString().Trim();
                        double mWeight = Convert.ToDouble(mDataRow["weight"]);
                        double mApgarScore = Convert.ToDouble(mDataRow["apgarscore"]);
                        string mProblems = mDataRow["childproblems"].ToString().Trim();

                        int mFreshBirth = 0;
                        int mMaceratedBirth = 0;

                        if (mDataRow["stillbirth"].ToString().Trim().ToLower() ==
                            AfyaPro_Types.clsEnums.RCHStillBirths.freshbirth.ToString().ToLower())
                        {
                            mFreshBirth = 1;
                        }
                        else if (mDataRow["stillbirth"].ToString().Trim().ToLower() ==
                            AfyaPro_Types.clsEnums.RCHStillBirths.maceratedbirth.ToString().ToLower())
                        {
                            mMaceratedBirth = 1;
                        }

                        int mlive = 0;
                        int mDeathBefore24 = 0;
                        int mDeathAfter24 = 0;

                        if (mDataRow["childcondition"].ToString().Trim().ToLower() ==
                            AfyaPro_Types.clsEnums.RCHChildConditions.live.ToString().ToLower())
                        {
                            mlive = 1;
                        }
                        else if (mDataRow["childcondition"].ToString().Trim().ToLower() ==
                            AfyaPro_Types.clsEnums.RCHChildConditions.deathbefore24.ToString().ToLower())
                        {
                            mDeathBefore24 = 1;
                        }
                        else if (mDataRow["childcondition"].ToString().Trim().ToLower() ==
                            AfyaPro_Types.clsEnums.RCHChildConditions.deathafter24.ToString().ToLower())
                        {
                            mDeathAfter24 = 1;
                        }

                        mCommand.CommandText = "insert into rch_postnatalchildren(clientcode,booking,bookdate,sysdate,"
                        + "deliverydate,gender,weight,apgarscore,freshbirth,maceratedbirth,childproblems,live,"
                        + "deathbefore24,deathafter24,userid,yearpart,monthpart) values('" + mClientCode.Trim() + "','"
                        + mNewBooking + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + clsGlobal.Saving_DateValue(DateTime.Now)
                        + "," + mDeliveryDate + ",'" + mGender + "'," + mWeight + "," + mApgarScore + ","
                        + mFreshBirth + "," + mMaceratedBirth + ",'" + mProblems + "'," + mlive + "," + mDeathBefore24 + "," 
                        + mDeathAfter24 + ",'" + mUserId + "'," + mYearPart + "," + mMonthPart + ")";
                        mRecsAffected = mCommand.ExecuteNonQuery();
                    }

                    #endregion
                }
                else
                {
                    //delete previous attendance
                    mCommand.CommandText = "delete from rch_postnatalattendances where clientcode='" + mClientCode.Trim() + "'";
                    mCommand.ExecuteNonQuery();

                    //create new attendance
                    mCommand.CommandText = "insert into rch_postnatalattendances(clientcode,bookdate,gravida,para,"
                    + "admissiondate,noofchildren,dischargestatus,attendantid,attendantname,birthmethod,fromantenatal,userid"
                    + ") values('" + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate)
                    + "," + mGravida + "," + mPara + "," + clsGlobal.Saving_DateValueNullable(mAdmissionDate) + ","
                    + mDtChildren.Rows.Count + ",'" + mDischargeStatus.Trim() + "','" + mAttendantId.Trim() + "','"
                    + mAttendantName.Trim() + "','" + mBirthMethod.Trim() + "'," + mFromAntenatal + ",'" + mUserId
                    + "')";
                    mCommand.ExecuteNonQuery();

                    //get attendance id created
                    mCommand.CommandText = "select autocode from rch_postnatalattendances where clientcode='" + mClientCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAttendances);
                    if (mDtAttendances.Rows.Count > 0)
                    {
                        mNewBooking = mDtAttendances.Rows[0]["autocode"].ToString().Trim();
                    }

                    //create a log for the new attendance
                    mCommand.CommandText = "insert into rch_postnatalattendancelog(clientcode,booking,bookdate,sysdate,gravida,para,"
                    + "admissiondate,noofchildren,dischargestatus,attendantid,attendantname,birthmethod,fromantenatal,userid,"
                    + "registrystatus,yearpart,monthpart) values('" + mClientCode.Trim() + "','"
                    + mNewBooking + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + clsGlobal.Saving_DateValue(DateTime.Now)
                    + "," + mGravida + "," + mPara + "," + clsGlobal.Saving_DateValueNullable(mAdmissionDate) + ","
                    + mDtChildren.Rows.Count + ",'" + mDischargeStatus.Trim() + "','" + mAttendantId.Trim() + "','"
                    + mAttendantName.Trim() + "','" + mBirthMethod.Trim() + "'," + mFromAntenatal + ",'" + mUserId
                    + "','" + AfyaPro_Types.clsEnums.RegistryStatus.New.ToString() + "'," + mYearPart + ","
                    + mMonthPart + ")";
                    mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit rch_postnatalattendancelog

                    if (mRecsAffected > 0)
                    {
                        string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "rch_postnatalattendancelog";
                        string mAuditingFields = clsGlobal.AuditingFields();
                        string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                            AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                        mCommand.CommandText = "insert into " + mAuditTableName + "(clientcode,booking,bookdate,sysdate,gravida,para,"
                        + "admissiondate,noofchildren,dischargestatus,attendantid,attendantname,birthmethod,fromantenatal,userid,"
                        + "registrystatus,yearpart,monthpart," + mAuditingFields + ") values('"
                        + mClientCode.Trim() + "','" + mNewBooking + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + clsGlobal.Saving_DateValue(DateTime.Now)
                        + "," + mGravida + "," + mPara + "," + clsGlobal.Saving_DateValueNullable(mAdmissionDate) + ","
                        + mDtChildren.Rows.Count + ",'" + mDischargeStatus.Trim() + "','" + mAttendantId.Trim() + "','"
                        + mAttendantName.Trim() + "','" + mBirthMethod.Trim() + "'," + mFromAntenatal + ",'" + mUserId
                        + "','" + AfyaPro_Types.clsEnums.RegistryStatus.New.ToString() + "'," + mYearPart + ","
                        + mMonthPart + "," + mAuditingValues + ")";
                        mCommand.ExecuteNonQuery();
                    }

                    #endregion

                    #region children

                    mCommand.CommandText = "delete from rch_postnatalchildren where clientcode='" + mClientCode.Trim()
                    + "' and booking='" + mNewBooking + "'";
                    mCommand.ExecuteNonQuery();

                    foreach (DataRow mDataRow in mDtChildren.Rows)
                    {
                        string mDeliveryDate = clsGlobal.Saving_DateValueNullable(mDataRow["deliverydate"]);
                        string mGender = mDataRow["gender"].ToString().Trim();
                        double mWeight = Convert.ToDouble(mDataRow["weight"]);
                        double mApgarScore = Convert.ToDouble(mDataRow["apgarscore"]);
                        string mProblems = mDataRow["childproblems"].ToString().Trim();

                        int mFreshBirth = 0;
                        int mMaceratedBirth = 0;

                        if (mDataRow["stillbirth"].ToString().Trim().ToLower() ==
                            AfyaPro_Types.clsEnums.RCHStillBirths.freshbirth.ToString().ToLower())
                        {
                            mFreshBirth = 1;
                        }
                        else if (mDataRow["stillbirth"].ToString().Trim().ToLower() ==
                            AfyaPro_Types.clsEnums.RCHStillBirths.maceratedbirth.ToString().ToLower())
                        {
                            mMaceratedBirth = 1;
                        }

                        int mlive = 0;
                        int mDeathBefore24 = 0;
                        int mDeathAfter24 = 0;

                        if (mDataRow["childcondition"].ToString().Trim().ToLower() ==
                            AfyaPro_Types.clsEnums.RCHChildConditions.live.ToString().ToLower())
                        {
                            mlive = 1;
                        }
                        else if (mDataRow["childcondition"].ToString().Trim().ToLower() ==
                            AfyaPro_Types.clsEnums.RCHChildConditions.deathbefore24.ToString().ToLower())
                        {
                            mDeathBefore24 = 1;
                        }
                        else if (mDataRow["childcondition"].ToString().Trim().ToLower() ==
                            AfyaPro_Types.clsEnums.RCHChildConditions.deathafter24.ToString().ToLower())
                        {
                            mDeathAfter24 = 1;
                        }

                        mCommand.CommandText = "insert into rch_postnatalchildren(clientcode,booking,bookdate,sysdate,"
                        + "deliverydate,gender,weight,apgarscore,freshbirth,maceratedbirth,childproblems,live,"
                        + "deathbefore24,deathafter24,userid,yearpart,monthpart) values('" + mClientCode.Trim() + "','"
                        + mNewBooking + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + clsGlobal.Saving_DateValue(DateTime.Now)
                        + "," + mDeliveryDate + ",'" + mGender + "'," + mWeight + "," + mApgarScore + ","
                        + mFreshBirth + "," + mMaceratedBirth + ",'" + mProblems + "'," + mlive + "," + mDeathBefore24 + ","
                        + mDeathAfter24 + ",'" + mUserId + "'," + mYearPart + "," + mMonthPart + ")";
                        mRecsAffected = mCommand.ExecuteNonQuery();
                    }

                    #endregion
                }

                #region danger indicators

                mCommand.CommandText = clsGlobal.Get_DeleteStatement(
                    "rch_birthcomplicationslog",
                    "clientcode='" + mClientCode.Trim() + "' and booking='" + mNewBooking + "'");
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtComplications.Rows)
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

                        mCommand.CommandText = clsGlobal.Get_InsertStatement("rch_birthcomplicationslog", mDataFields);
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region patient servicetype

                mCommand.CommandText = "update patients set servicetype=" + (int)AfyaPro_Types.clsEnums.RCHServices.postnatalcare
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
        public AfyaPro_Types.clsResult Edit(DateTime mTransDate, string mBooking, string mClientCode, string mBirthMethod, 
            DataTable mDtComplications, int mGravida, int mPara, DateTime mAdmissionDate, string mDischargeStatus,
            string mAttendantId, string mAttendantName, int mFromAntenatal, DataTable mDtChildren, string mUserId)
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.rchpostnatal_edit.ToString(), mUserId);
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

                //rch_postnatalattendances
                mCommand.CommandText = "update rch_postnatalattendances set clientcode='" + mClientCode.Trim()
                + "',bookdate=" + clsGlobal.Saving_DateValue(mTransDate) + ",gravida=" + mGravida
                + ",para=" + mPara + ",admissiondate=" + clsGlobal.Saving_DateValueNullable(mAdmissionDate)
                + ",noofchildren=" + mDtChildren.Rows.Count + ",dischargestatus='" + mDischargeStatus.Trim()
                + "',attendantid='" + mAttendantId.Trim() + "',attendantname='" + mAttendantName.Trim()
                + "',birthmethod='" + mBirthMethod.Trim() + "',fromantenatal=" + mFromAntenatal + ",userid='" 
                + mUserId.Trim() + "' where autocode=" + mBooking.Trim();
                mCommand.ExecuteNonQuery();

                //rch_postnatalattendancelog
                mCommand.CommandText = "update rch_postnatalattendancelog set clientcode='" + mClientCode.Trim()
                + "',bookdate=" + clsGlobal.Saving_DateValue(mTransDate) + ",gravida=" + mGravida
                + ",para=" + mPara + ",admissiondate=" + clsGlobal.Saving_DateValueNullable(mAdmissionDate)
                + ",sysdate=" + clsGlobal.Saving_DateValue(DateTime.Now)
                + ",noofchildren=" + mDtChildren.Rows.Count + ",dischargestatus='" + mDischargeStatus.Trim()
                + "',attendantid='" + mAttendantId.Trim() + "',attendantname='" + mAttendantName.Trim()
                + "',birthmethod='" + mBirthMethod.Trim() + "',fromantenatal=" + mFromAntenatal + ",userid='"
                + mUserId.Trim() + "',yearpart=" + mYearPart + ",monthpart=" + mMonthPart 
                + " where booking='" + mBooking.Trim() + "'";
                mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit rch_postnatalattendancelog
                if (mRecsAffected > 0)
                {
                    DataTable mDtBookingLog = new DataTable("rch_postnatalattendancelog");
                    mCommand.CommandText = "select * from rch_postnatalattendancelog where booking='" + mBooking.Trim()
                    + "' and clientcode='" + mClientCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtBookingLog);

                    mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtBookingLog, "rch_postnatalattendancelog",
                    mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                    mCommand.ExecuteNonQuery();
                }
                #endregion

                #region children

                mCommand.CommandText = "delete from rch_postnatalchildren where clientcode='" + mClientCode.Trim()
                + "' and booking='" + mBooking + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtChildren.Rows)
                {
                    string mDeliveryDate = clsGlobal.Saving_DateValueNullable(mDataRow["deliverydate"]);
                    string mGender = mDataRow["gender"].ToString().Trim();
                    double mWeight = Convert.ToDouble(mDataRow["weight"]);
                    double mApgarScore = Convert.ToDouble(mDataRow["apgarscore"]);
                    string mProblems = mDataRow["childproblems"].ToString().Trim();

                    int mFreshBirth = 0;
                    int mMaceratedBirth = 0;

                    if (mDataRow["stillbirth"].ToString().Trim().ToLower() ==
                        AfyaPro_Types.clsEnums.RCHStillBirths.freshbirth.ToString().ToLower())
                    {
                        mFreshBirth = 1;
                    }
                    else if (mDataRow["stillbirth"].ToString().Trim().ToLower() ==
                        AfyaPro_Types.clsEnums.RCHStillBirths.maceratedbirth.ToString().ToLower())
                    {
                        mMaceratedBirth = 1;
                    }

                    int mlive = 0;
                    int mDeathBefore24 = 0;
                    int mDeathAfter24 = 0;

                    if (mDataRow["childcondition"].ToString().Trim().ToLower() ==
                        AfyaPro_Types.clsEnums.RCHChildConditions.live.ToString().ToLower())
                    {
                        mlive = 1;
                    }
                    else if (mDataRow["childcondition"].ToString().Trim().ToLower() ==
                        AfyaPro_Types.clsEnums.RCHChildConditions.deathbefore24.ToString().ToLower())
                    {
                        mDeathBefore24 = 1;
                    }
                    else if (mDataRow["childcondition"].ToString().Trim().ToLower() ==
                        AfyaPro_Types.clsEnums.RCHChildConditions.deathafter24.ToString().ToLower())
                    {
                        mDeathAfter24 = 1;
                    }

                    mCommand.CommandText = "insert into rch_postnatalchildren(clientcode,booking,bookdate,sysdate,"
                    + "deliverydate,gender,weight,apgarscore,freshbirth,maceratedbirth,childproblems,live,"
                    + "deathbefore24,deathafter24,userid,yearpart,monthpart) values('" + mClientCode.Trim() + "','"
                    + mBooking + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + clsGlobal.Saving_DateValue(DateTime.Now)
                    + "," + mDeliveryDate + ",'" + mGender + "'," + mWeight + "," + mApgarScore + ","
                    + mFreshBirth + "," + mMaceratedBirth + ",'" + mProblems + "'," + mlive + "," + mDeathBefore24 + ","
                    + mDeathAfter24 + ",'" + mUserId + "'," + mYearPart + "," + mMonthPart + ")";
                    mRecsAffected = mCommand.ExecuteNonQuery();
                }

                #endregion

                #region danger indicators

                mCommand.CommandText = clsGlobal.Get_DeleteStatement(
                    "rch_birthcomplicationslog",
                    "clientcode='" + mClientCode.Trim() + "' and booking='" + mBooking + "'");
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtComplications.Rows)
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

                        mCommand.CommandText = clsGlobal.Get_InsertStatement("rch_birthcomplicationslog", mDataFields);
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.rchpostnatal_delete.ToString(), mUserId);
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

                DataTable mDtAttendanceLog = new DataTable("rch_postnatalattendancelog");
                mCommand.CommandText = "select * from rch_postnatalattendancelog where booking='" + mBooking.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtAttendanceLog);

                //rch_postnatalattendances
                mCommand.CommandText = "delete from rch_postnatalattendances where autocode=" + mBooking;
                mCommand.ExecuteNonQuery();

                //rch_postnatalattendancelog
                mCommand.CommandText = "delete from rch_postnatalattendancelog where booking='" + mBooking.Trim() + "'";
                mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit rch_postnatalattendancelog
                if (mRecsAffected > 0)
                {
                    mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtAttendanceLog, "rch_postnatalattendancelog",
                    mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Deleted.ToString());
                    mCommand.ExecuteNonQuery();
                }
                #endregion

                //rch_postnatalchildren
                mCommand.CommandText = "delete from rch_postnatalchildren where clientcode='" + mClientCode.Trim() + "' and booking='" + mBooking.Trim() + "'";
                mCommand.ExecuteNonQuery();

                //rch_birthcomplicationslog
                mCommand.CommandText = "delete from rch_birthcomplicationslog where booking='" + mBooking.Trim() + "' and clientcode='" + mClientCode.Trim() + "'";
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
