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
    public class clsRCHAnteNatalCare : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsRCHAnteNatalCare";
        
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
                string mTransColumns = clsGlobal.Get_TableColumns(mCommand, "rch_antenatalattendances", "", "b", "");

                string mCommandText = ""
                    + "SELECT "
                    + mTransColumns + ","
                    + mClientColumns + " "
                    + "FROM rch_antenatalattendances AS b "
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

                DataTable mDataTable = new DataTable("rch_antenatalattendances");
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
        public DataTable View_Archive(String mFilter, String mOrder, string mPCode)
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
                //string mClientColumns = clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "client");
                //mClientColumns = mClientColumns + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "clientfullname");
                //mClientColumns = mClientColumns + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "b.bookdate","") ,"clientage");
                ////columns from trans
                //string mTransColumns = clsGlobal.Get_TableColumns(mCommand, "rch_antenatalattendancelog", "", "b", "");

                //string mCommandText = "";
                //    + "SELECT "
                //    + mTransColumns + ","
                //    + mClientColumns + " "
                //    + "FROM rch_antenatalattendancelog AS b "
                //    + "LEFT OUTER JOIN patients AS p ON b.clientcode=p.code";

                //if (mFilter.Trim() != "")
                //{
                //    mCommandText = mCommandText + " where " + mFilter;
                //}

                //if (mOrder.Trim() != "")
                //{
                    //mCommandText = mCommandText + " order by " + mOrder;
                //}

               
                mCommand.CommandText = "select * from rch_antenatalattendancelog where clientcode ='" + mPCode + "'";

                DataTable mDataTable = new DataTable("rch_antenatalattendancelog");
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

        #region Load_Visit
        public DataTable Load_Visit(DataRow mSelectedRow)
        {
            String mFunctionName = "Load_Visit";

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
                //string mClientColumns = clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "client");
                //mClientColumns = mClientColumns + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "clientfullname");
                //mClientColumns = mClientColumns + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "b.bookdate","") ,"clientage");
                ////columns from trans
                //string mTransColumns = clsGlobal.Get_TableColumns(mCommand, "rch_antenatalattendancelog", "", "b", "");

                //string mCommandText = "";
                //    + "SELECT "
                //    + mTransColumns + ","
                //    + mClientColumns + " "
                //    + "FROM rch_antenatalattendancelog AS b "
                //    + "LEFT OUTER JOIN patients AS p ON b.clientcode=p.code";

                //if (mFilter.Trim() != "")
                //{
                //    mCommandText = mCommandText + " where " + mFilter;
                //}

                //if (mOrder.Trim() != "")
                //{
                //mCommandText = mCommandText + " order by " + mOrder;
                //}
               // mAutoCode = mSelectedRow["autocode"].ToString();
                mCommand.CommandText = "select * from rch_antenatalattendancelog where clientcode ='" + mSelectedRow["clientcode"].ToString() + "' and autocode= '" + mSelectedRow["autocode"].ToString() +"'";

                DataTable mDataTable = new DataTable("rch_antenatalattendancelog");
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

        #region View_Indicators
        public DataTable View_Indicators(String mFilter, String mOrder)
        {
            String mFunctionName = "View_Indicators";

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
                string mCommandText = "SELECT * FROM rch_dangerindicatorslog";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("rch_dangerindicatorslog");
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
      
        public AfyaPro_Types.clsResult Add_Patient(DateTime mTransDate, string mClientCode, string mUserId, string mRegistrationNo, string mResidence, string mGravida, string mPara, string mLMP, string mEDD)
        {
            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            AfyaPro_Types.clsRchClient mClient = new AfyaPro_Types.clsRchClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            //OdbcConnection mConn = new OdbcConnection();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            string mNewBooking = "";

            int mRecsAffected = -1;
            DataTable mDtAttendances = new DataTable("attendances");

            string mFunctionName = "Add_ANC Patient";

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.rchantenatal_add.ToString(), mUserId);
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

                mCommand.CommandText = "select * from rch_antenatalattendances where clientcode='" + mClientCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtAttendances);
                if (mDtAttendances.Rows.Count > 0)
                {
                    mCommand.CommandText = "update rch_antenatalattendances set userid = '" + mUserId.Trim() + "', regno = '" + mRegistrationNo + "', residence ='" + mResidence + "', gravida= '" + mGravida + "', para = '" + mPara + "', lmp ='" + mLMP + "', edd ='" + mEDD + "' where clientcode = '" + mClientCode.Trim() + "'";
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    mDtAttendances.Clear();
                    mCommand.CommandText = "select * from rch_antenatalattendances where regno='" + mRegistrationNo.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAttendances);

                    if (mDtAttendances.Rows.Count > 0)
                    {
                        mResult.Exe_Result = 0;
                        mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.RCH_ClientCodeIsInUse.ToString();
                        return mResult;
                    }

                    mCommand.CommandText = "insert into rch_antenatalattendances(clientcode, bookdate, userid, regno, residence, gravida, para, lmp, edd) values('" + mClientCode.Trim() + "',"
                    + clsGlobal.Saving_DateValue(mTransDate) + ", '" + mUserId.Trim() + "', '" + mRegistrationNo + "', '" + mResidence + "', '" + mGravida + "', '" + mPara + "', '" + mLMP + "', '" + mEDD + "')";
                    mCommand.ExecuteNonQuery();
                }
                    //get attendance id created
                    //mCommand.CommandText = "select autocode from rch_antenatalattendances where clientcode='" + mClientCode.Trim() + "'";
                    //mDataAdapter.SelectCommand = mCommand;
                    //mDataAdapter.Fill(mDtAttendances);
                    //if (mDtAttendances.Rows.Count > 0)
                    //{
                    //    mNewBooking = mDtAttendances.Rows[0]["autocode"].ToString().Trim();
                    //}

                    ////create a log for the new attendance
                    //mCommand.CommandText = "insert into rch_antenatalattendancelog(clientcode,bookdate,sysdate,booking,cardpresented,"
                    //+ "pregage,noofpreg,height,discount,syphilistest,lastbirthyear,lastborndeath,referedto,"
                    //+ "pmtcttest,userid,registrystatus,yearpart,monthpart) values('"
                    //+ mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + clsGlobal.Saving_DateValue(DateTime.Now) + ",'"
                    //+ mNewBooking + "'," + mCardPresented + "," + mPregAge + "," + mNoOfPreg + "," + mHeight + "," + mDiscount + "," + mSyphilisTest + ","
                    //+ mLastBirthYear + "," + mLastBornDeath + ",'" + mReferedTo.Trim() + "'," 
                    //+ mPMTCTTest + ",'" + mUserId.Trim() + "','" + AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString()
                    //+ "'," + mYearPart + "," + mMonthPart + ")";
                    //mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit rch_antenatalattendancelog

                    //if (mRecsAffected > 0)
                    //{
                    //    string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "rch_antenatalattendancelog";
                    //    string mAuditingFields = clsGlobal.AuditingFields();
                    //    string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                    //        AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                    //    mCommand.CommandText = "insert into " + mAuditTableName + "(clientcode,bookdate,sysdate,booking,cardpresented,"
                    //    + "pregage,noofpreg,height,discount,syphilistest,lastbirthyear,lastborndeath,referedto,"
                    //    + "pmtcttest,userid,registrystatus,yearpart,monthpart" + "," + mAuditingFields + ") values('"
                    //    + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + clsGlobal.Saving_DateValue(DateTime.Now) + ",'"
                    //    + mNewBooking + "'," + mCardPresented + "," + mPregAge + "," + mNoOfPreg + "," + mHeight + "," + mDiscount + "," + mSyphilisTest + ","
                    //    + mLastBirthYear + "," + mLastBornDeath + ",'" + mReferedTo.Trim() + "',"
                    //    + mPMTCTTest + ",'" + mUserId.Trim() + "','" + AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString()
                    //    + "'," + mYearPart + "," + mMonthPart + "," + mAuditingValues + ")";
                    //    mCommand.ExecuteNonQuery();
                    //}

                    //#endregion
                //}
                //else
                //{
                //    //delete previous attendance
                //    mCommand.CommandText = "delete from rch_antenatalattendances where clientcode='" + mClientCode.Trim() + "'";
                //    mCommand.ExecuteNonQuery();

                //    //create new attendance
                //    mCommand.CommandText = "insert into rch_antenatalattendances(clientcode,bookdate,cardpresented,pregage,"
                //    + "noofpreg,height,discount,syphilistest,lastbirthyear,lastborndeath,referedto,"
                //    + "pmtcttest,userid, regno, residence, gravida, para, lmp, edd) values('" + mClientCode.Trim() + "',"
                //    + clsGlobal.Saving_DateValue(mTransDate) + "," + mCardPresented + "," + mPregAge + "," + mNoOfPreg + ","
                //    + mHeight + "," + mDiscount + "," + mSyphilisTest
                //    + "," + mLastBirthYear + "," + mLastBornDeath + ",'" + mReferedTo.Trim() + "',"
                //    + mPMTCTTest + ",'" + mUserId.Trim() + "'," + mRegistrationNo + "," + mResidence + "," + mGravida + "," + mPara + "," + mLMP + "," + mEDD + ")";
                //    mCommand.ExecuteNonQuery();

                //    //get attendance id created
                //    mCommand.CommandText = "select autocode from rch_antenatalattendances where clientcode='" + mClientCode.Trim() + "'";
                //    mDataAdapter.SelectCommand = mCommand;
                //    mDataAdapter.Fill(mDtAttendances);
                //    if (mDtAttendances.Rows.Count > 0)
                //    {
                //        mNewBooking = mDtAttendances.Rows[0]["autocode"].ToString().Trim();
                //    }

                //    //create a log for the new attendance
                //    mCommand.CommandText = "insert into rch_antenatalattendancelog(clientcode,bookdate,sysdate,booking,cardpresented,"
                //    + "pregage,noofpreg,height,discount,syphilistest,lastbirthyear,lastborndeath,referedto,"
                //    + "pmtcttest,userid,registrystatus,yearpart,monthpart) values('"
                //    + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + clsGlobal.Saving_DateValue(DateTime.Now) + ",'"
                //    + mNewBooking + "'," + mCardPresented + "," + mPregAge + "," + mNoOfPreg + "," + mHeight + "," + mDiscount + "," + mSyphilisTest + ","
                //    + mLastBirthYear + "," + mLastBornDeath + ",'"
                //    + mReferedTo.Trim() + "',"
                //    + mPMTCTTest + ",'" + mUserId.Trim() + "','" + AfyaPro_Types.clsEnums.RegistryStatus.New.ToString()
                //    + "'," + mYearPart + "," + mMonthPart + ")";
                //    mRecsAffected = mCommand.ExecuteNonQuery();

                //    #region audit rch_antenatalattendancelog

                //    if (mRecsAffected > 0)
                //    {
                //        string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "rch_antenatalattendancelog";
                //        string mAuditingFields = clsGlobal.AuditingFields();
                //        string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                //            AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                //        mCommand.CommandText = "insert into " + mAuditTableName + "(clientcode,bookdate,sysdate,booking,cardpresented,"
                //        + "pregage,noofpreg,height,discount,syphilistest,lastbirthyear,lastborndeath,referedto,"
                //        + "pmtcttest,userid,registrystatus,yearpart,monthpart," + mAuditingFields + ") values('"
                //        + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + clsGlobal.Saving_DateValue(DateTime.Now) + ",'"
                //        + mNewBooking + "'," + mCardPresented + "," + mPregAge + "," + mNoOfPreg + "," + mHeight + "," + mDiscount + "," + mSyphilisTest + ","
                //        + mLastBirthYear + "," + mLastBornDeath + ",'"
                //        + mReferedTo.Trim() + "',"
                //        + mPMTCTTest + ",'" + mUserId.Trim() + "','" + AfyaPro_Types.clsEnums.RegistryStatus.New.ToString()
                //        + "'," + mYearPart + "," + mMonthPart + "," + mAuditingValues + ")";
                //        mCommand.ExecuteNonQuery();
                //    }

                //    #endregion
                //}

                //#region danger indicators

                //mCommand.CommandText = clsGlobal.Get_DeleteStatement(
                //    "rch_dangerindicatorslog",
                //    "clientcode='" + mClientCode.Trim() + "' and booking='" + mNewBooking + "'");
                //mCommand.ExecuteNonQuery();

                //foreach (DataRow mDataRow in mDtDangerIndicators.Rows)
                //{
                //    string mCode = mDataRow["fieldname"].ToString().Trim();
                //    double mQuantity = Convert.ToDouble(mDataRow["fieldvalue"]);

                //    if (mQuantity > 0)
                //    {
                //        List<clsDataField> mDataFields = new List<clsDataField>();
                //        mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mNewBooking));
                //        mDataFields.Add(new clsDataField("clientcode", DataTypes.dbstring.ToString(), mClientCode));
                //        mDataFields.Add(new clsDataField("methodcode", DataTypes.dbstring.ToString(), mCode));
                //        mDataFields.Add(new clsDataField("quantity", DataTypes.dbdecimal.ToString(), mQuantity));

                //        mCommand.CommandText = clsGlobal.Get_InsertStatement("rch_dangerindicatorslog", mDataFields);
                //        mCommand.ExecuteNonQuery();
                //    }
                //}

                 #endregion

                #region patient servicetype

                mCommand.CommandText = "update patients set servicetype=" + (int)AfyaPro_Types.clsEnums.RCHServices.antenatalcare
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

        #region Add_Visit
        public AfyaPro_Types.clsResult Add_Visit(string mUserId, string mClientCode, DateTime mTransDate, DateTime mVisitDate, string mPregage, string mFetalheart, string mWeight, string mSyphilisTest, string mBP, string mUrineProtein, string mTtvpreviousdosses, string mTtvnewdosses, string mSP, string mFeFo, string mAlbendazole, string mBednetgiven, string mHB, string mPrevioushivtestresult, string mNewhivtestresult, string mOnCPT, string mNvpsyrupdispensed, string mONART, string mComment, string mProvidername, Boolean isEditing, string mAutocode)
        {
            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            AfyaPro_Types.clsRchClient mClient = new AfyaPro_Types.clsRchClient();
           
            OdbcCommand mCommand = new OdbcCommand();
           // OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            OdbcConnection mConn = new OdbcConnection();
            string mNewBooking = "";

           // int mRecsAffected = -1;
            DataTable mDtAttendances = new DataTable("attendances");
            
            string mFunctionName = "Add_ANC Patient Visit";

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
            mFunctionName = "Add_ANC Patient Visit_security check";
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.rchantenatal_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

           
            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;
                mVisitDate = mVisitDate.Date;
                if (isEditing == false)
                {
                    mFunctionName = "Add_ANC Patient Visit_Saving";
                    mCommand.CommandText = "insert into rch_antenatalattendancelog(clientcode,bookdate,sysdate,booking,"
                    + "pregage,syphilistest,fetalheart,weight,bp,"
                    + "urineprotein,ttvpreviousdosses,ttvnewdosses,sp,fefo,albendazole,bednetgiven,hb,previoushivtestresult,newhivtestresult,oncpt,nvpsyrupdispensed,onart,comment,providername,visitdate) values('"
                    + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + clsGlobal.Saving_DateValue(DateTime.Now) + ",'"
                    + mNewBooking + "','" + mPregage + "','" + mSyphilisTest + "','" + mFetalheart + "','" + mWeight + "','" + mBP + "','" + mUrineProtein + "','"
                    + mTtvpreviousdosses + "','" + mTtvnewdosses + "','" + mSP + "','"
                    + mFeFo + "','" + mAlbendazole + "','" + mBednetgiven + "','" + mHB + "','" + mPrevioushivtestresult + "','" + mNewhivtestresult + "','" + mOnCPT + "','" + mNvpsyrupdispensed + "','" + mONART + "','" + mComment + "','" + mProvidername + "'," + clsGlobal.Saving_DateValue(mVisitDate) + ")";
                    mCommand.ExecuteNonQuery();
                }

                if (isEditing == true)
                {
                    mFunctionName = "Update_ANC Patient Visit";
                    mCommand.CommandText = "update rch_antenatalattendancelog set pregage ='" + mPregage + "', syphilistest ='" + mSyphilisTest + "', fetalheart ='" + mFetalheart + "', weight ='"  + mWeight + "', bp ='"
                        + mBP + "', urineprotein ='" + mUrineProtein + "', ttvpreviousdosses ='" + mTtvpreviousdosses + "', ttvnewdosses ='" + mTtvnewdosses + "', sp ='" + mSP + "', fefo ='" + mFeFo + "' ,albendazole ='" + mAlbendazole
                 
                        + "', bednetgiven ='" + mBednetgiven + "', hb ='" + mHB + "', previoushivtestresult ='" + mPrevioushivtestresult + "', newhivtestresult= '" + mNewhivtestresult + "', oncpt ='" + mOnCPT + "', nvpsyrupdispensed ='" + mNvpsyrupdispensed
                        + "', onart ='" + mONART + "', comment ='" + mComment + "', providername ='" + mProvidername + "', visitdate = " + clsGlobal.Saving_DateValue(mVisitDate)
                        + "where clientcode= '" + mClientCode + "' and autocode = '" + mAutocode + "'";
                    //  "','" '" "','",'"  + "'+ "','"
                    // + "','" '" + mSP + "','"
                    //+ mFeFo + "','" + mAlbendazole + "','" + mBednetgiven + "','" + mHB + "','" + mPrevioushivtestresult + "','" + mNewhivtestresult + "','" + mOnCPT + "','" + mNvpsyrupdispensed + "','" + mONART + "','" + mComment + "','" + mProvidername + "'," + clsGlobal.Saving_DateValue(mVisitDate) + ")";
                    mCommand.ExecuteNonQuery();

                    clsGlobal.Write_Error(pClassName, mFunctionName, mClientCode + "' and autocode = '" + mAutocode);
                }

                #region audit rch_antenatalattendancelog

                //if (mRecsAffected > 0)
                //{
                //    string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "rch_antenatalattendancelog";
                //    string mAuditingFields = clsGlobal.AuditingFields();
                //    string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                //        AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                //    mCommand.CommandText = "insert into " + mAuditTableName + "(clientcode,bookdate,sysdate,booking,cardpresented,"
                //    + "pregage,noofpreg,height,discount,syphilistest,lastbirthyear,lastborndeath,referedto,"
                //    + "pmtcttest,userid,registrystatus,yearpart,monthpart" + "," + mAuditingFields + ") values('"
                //    + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + clsGlobal.Saving_DateValue(DateTime.Now) + ",'"
                //    + mNewBooking + "'," + mCardPresented + "," + mPregAge + "," + mNoOfPreg + "," + mHeight + "," + mDiscount + "," + mSyphilisTest + ","
                //    + mLastBirthYear + "," + mLastBornDeath + ",'" + mReferedTo.Trim() + "',"
                //    + mPMTCTTest + ",'" + mUserId.Trim() + "','" + AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString()
                //    + "'," + mYearPart + "," + mMonthPart + "," + mAuditingValues + ")";
                //    mCommand.ExecuteNonQuery();
                //}

                //#endregion
                //}
                //else
                //{
                //    //delete previous attendance
                //    mCommand.CommandText = "delete from rch_antenatalattendances where clientcode='" + mClientCode.Trim() + "'";
                //    mCommand.ExecuteNonQuery();

                //    //create new attendance
                //    mCommand.CommandText = "insert into rch_antenatalattendances(clientcode,bookdate,cardpresented,pregage,"
                //    + "noofpreg,height,discount,syphilistest,lastbirthyear,lastborndeath,referedto,"
                //    + "pmtcttest,userid, regno, residence, gravida, para, lmp, edd) values('" + mClientCode.Trim() + "',"
                //    + clsGlobal.Saving_DateValue(mTransDate) + "," + mCardPresented + "," + mPregAge + "," + mNoOfPreg + ","
                //    + mHeight + "," + mDiscount + "," + mSyphilisTest
                //    + "," + mLastBirthYear + "," + mLastBornDeath + ",'" + mReferedTo.Trim() + "',"
                //    + mPMTCTTest + ",'" + mUserId.Trim() + "'," + mRegistrationNo + "," + mResidence + "," + mGravida + "," + mPara + "," + mLMP + "," + mEDD + ")";
                //    mCommand.ExecuteNonQuery();

                //    //get attendance id created
                //    mCommand.CommandText = "select autocode from rch_antenatalattendances where clientcode='" + mClientCode.Trim() + "'";
                //    mDataAdapter.SelectCommand = mCommand;
                //    mDataAdapter.Fill(mDtAttendances);
                //    if (mDtAttendances.Rows.Count > 0)
                //    {
                //        mNewBooking = mDtAttendances.Rows[0]["autocode"].ToString().Trim();
                //    }

                //    //create a log for the new attendance
                //    mCommand.CommandText = "insert into rch_antenatalattendancelog(clientcode,bookdate,sysdate,booking,cardpresented,"
                //    + "pregage,noofpreg,height,discount,syphilistest,lastbirthyear,lastborndeath,referedto,"
                //    + "pmtcttest,userid,registrystatus,yearpart,monthpart) values('"
                //    + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + clsGlobal.Saving_DateValue(DateTime.Now) + ",'"
                //    + mNewBooking + "'," + mCardPresented + "," + mPregAge + "," + mNoOfPreg + "," + mHeight + "," + mDiscount + "," + mSyphilisTest + ","
                //    + mLastBirthYear + "," + mLastBornDeath + ",'"
                //    + mReferedTo.Trim() + "',"
                //    + mPMTCTTest + ",'" + mUserId.Trim() + "','" + AfyaPro_Types.clsEnums.RegistryStatus.New.ToString()
                //    + "'," + mYearPart + "," + mMonthPart + ")";
                //    mRecsAffected = mCommand.ExecuteNonQuery();

                //    #region audit rch_antenatalattendancelog

                //    if (mRecsAffected > 0)
                //    {
                //        string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "rch_antenatalattendancelog";
                //        string mAuditingFields = clsGlobal.AuditingFields();
                //        string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                //            AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                //        mCommand.CommandText = "insert into " + mAuditTableName + "(clientcode,bookdate,sysdate,booking,cardpresented,"
                //        + "pregage,noofpreg,height,discount,syphilistest,lastbirthyear,lastborndeath,referedto,"
                //        + "pmtcttest,userid,registrystatus,yearpart,monthpart," + mAuditingFields + ") values('"
                //        + mClientCode.Trim() + "'," + clsGlobal.Saving_DateValue(mTransDate) + "," + clsGlobal.Saving_DateValue(DateTime.Now) + ",'"
                //        + mNewBooking + "'," + mCardPresented + "," + mPregAge + "," + mNoOfPreg + "," + mHeight + "," + mDiscount + "," + mSyphilisTest + ","
                //        + mLastBirthYear + "," + mLastBornDeath + ",'"
                //        + mReferedTo.Trim() + "',"
                //        + mPMTCTTest + ",'" + mUserId.Trim() + "','" + AfyaPro_Types.clsEnums.RegistryStatus.New.ToString()
                //        + "'," + mYearPart + "," + mMonthPart + "," + mAuditingValues + ")";
                //        mCommand.ExecuteNonQuery();
                //    }

                //    #endregion
                //}

                //#region danger indicators

                //mCommand.CommandText = clsGlobal.Get_DeleteStatement(
                //    "rch_dangerindicatorslog",
                //    "clientcode='" + mClientCode.Trim() + "' and booking='" + mNewBooking + "'");
                //mCommand.ExecuteNonQuery();

                //foreach (DataRow mDataRow in mDtDangerIndicators.Rows)
                //{
                //    string mCode = mDataRow["fieldname"].ToString().Trim();
                //    double mQuantity = Convert.ToDouble(mDataRow["fieldvalue"]);

                //    if (mQuantity > 0)
                //    {
                //        List<clsDataField> mDataFields = new List<clsDataField>();
                //        mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mNewBooking));
                //        mDataFields.Add(new clsDataField("clientcode", DataTypes.dbstring.ToString(), mClientCode));
                //        mDataFields.Add(new clsDataField("methodcode", DataTypes.dbstring.ToString(), mCode));
                //        mDataFields.Add(new clsDataField("quantity", DataTypes.dbdecimal.ToString(), mQuantity));

                //        mCommand.CommandText = clsGlobal.Get_InsertStatement("rch_dangerindicatorslog", mDataFields);
                //        mCommand.ExecuteNonQuery();
                //    }
                //}

                #endregion

                #region patient servicetype

                mCommand.CommandText = "update patients set servicetype=" + (int)AfyaPro_Types.clsEnums.RCHServices.antenatalcare
                    + " where code='" + mClientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                #endregion

                

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
        public AfyaPro_Types.clsResult Edit(DateTime mTransDate, string mBooking, string mClientCode, int mCardPresented, int mPregAge,
            int mNoOfPreg, double mHeight, int mDiscount, int mSyphilisTest, int mLastBirthYear,
            int mLastBornDeath, string mReferedTo, int mPMTCTTest, DataTable mDtDangerIndicators,
            bool mIsNewAttendance, string mUserId)
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.rchantenatal_edit.ToString(), mUserId);
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

                //rch_antenatalattendances
                mCommand.CommandText = "update rch_antenatalattendances set clientcode='" + mClientCode.Trim()
                + "',bookdate=" + clsGlobal.Saving_DateValue(mTransDate) + ",cardpresented=" + mCardPresented
                + ",pregage=" + mPregAge + ",noofpreg=" + mNoOfPreg + ",height=" + mHeight + ",discount=" + mDiscount
                + ",syphilistest=" + mSyphilisTest
                + ",lastbirthyear=" + mLastBirthYear + ",lastborndeath=" + mLastBornDeath + ",referedto='"
                + mReferedTo.Trim() + "',pmtcttest=" + mPMTCTTest
                + ",userid='" + mUserId.Trim() + "' where autocode=" + mBooking.Trim();
                mCommand.ExecuteNonQuery();

                //rch_antenatalattendancelog
                mCommand.CommandText = "update rch_antenatalattendancelog set clientcode='" + mClientCode.Trim()
                + "',bookdate=" + clsGlobal.Saving_DateValue(mTransDate) + ",sysdate=" + clsGlobal.Saving_DateValue(DateTime.Now) 
                + ",cardpresented=" + mCardPresented + ",pregage=" + mPregAge + ",noofpreg=" + mNoOfPreg + ",height=" 
                + mHeight + ",discount=" + mDiscount + ",syphilistest=" + mSyphilisTest
                + ",lastbirthyear=" + mLastBirthYear + ",lastborndeath=" + mLastBornDeath + ",referedto='"
                + mReferedTo.Trim() + "',pmtcttest=" + mPMTCTTest
                + ",userid='" + mUserId.Trim() + "',yearpart=" + mYearPart + ",monthpart=" + mMonthPart 
                + " where booking='" + mBooking.Trim() + "'";
                mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit rch_antenatalattendancelog
                if (mRecsAffected > 0)
                {
                    DataTable mDtBookingLog = new DataTable("rch_antenatalattendancelog");
                    mCommand.CommandText = "select * from rch_antenatalattendancelog where booking='" + mBooking.Trim()
                    + "' and clientcode='" + mClientCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtBookingLog);

                    mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtBookingLog, "rch_antenatalattendancelog",
                    mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                    mCommand.ExecuteNonQuery();
                }
                #endregion

                #region danger indicators

                mCommand.CommandText = clsGlobal.Get_DeleteStatement(
                    "rch_dangerindicatorslog",
                    "clientcode='" + mClientCode.Trim() + "' and booking='" + mBooking + "'");
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtDangerIndicators.Rows)
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

                        mCommand.CommandText = clsGlobal.Get_InsertStatement("rch_dangerindicatorslog", mDataFields);
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                mTrans.Commit();

                mResult.GeneratedCode = mBooking.Trim();
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.rchantenatal_delete.ToString(), mUserId);
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

                DataTable mDtAttendanceLog = new DataTable("rch_antenatalattendancelog");
                mCommand.CommandText = "select * from rch_antenatalattendancelog where booking='" + mBooking.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtAttendanceLog);

                //rch_antenatalattendancelog
                mCommand.CommandText = "delete from rch_antenatalattendancelog where booking='" + mBooking.Trim() + "'";
                mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit rch_antenatalattendancelog
                if (mRecsAffected > 0)
                {
                    mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtAttendanceLog, "rch_antenatalattendancelog",
                    mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Deleted.ToString());
                    mCommand.ExecuteNonQuery();
                }
                #endregion

                //rch_dangerindicatorslog
                mCommand.CommandText = "delete from rch_dangerindicatorslog where booking='" + mBooking.Trim() + "' and clientcode='" + mClientCode.Trim() + "'";
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
