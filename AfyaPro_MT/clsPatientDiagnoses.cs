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
    public class clsPatientDiagnoses : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsPatientDiagnoses";

        #endregion

        #region View_PatientsDiagnoses
        public DataTable View_PatientsDiagnoses(bool mDateSpecified, DateTime mDateFrom, DateTime mDateTo, String mExtraFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_ArchiveBookings";

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
                #region add dates to filter string

                if (mDateSpecified == true)
                {
                    mDateFrom = mDateFrom.Date;
                    mDateTo = mDateTo.Date;

                    if (mExtraFilter.Trim() == "")
                    {
                        mExtraFilter = "(transdate between " + clsGlobal.Saving_DateValue(mDateFrom)
                            + " and " + clsGlobal.Saving_DateValue(mDateTo) + ")";
                    }
                    else
                    {
                        mExtraFilter = "(" + mExtraFilter + ") and (transdate between " + clsGlobal.Saving_DateValue(mDateFrom)
                            + " and " + clsGlobal.Saving_DateValue(mDateTo) + ")";
                    }
                }
                #endregion

                //columns from patients
                string mPatientColumns = clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "patient");
                mPatientColumns = mPatientColumns + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "patientfullname");
                mPatientColumns = mPatientColumns + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "b.transdate", ""), "patientage");

                //columns from facility staffs
                string mStaffColumns = clsGlobal.Get_TableColumns(mCommand, "facilitystaffs", "autocode,code", "f", "staff");

                //columns from trans
                string mTransColumns = clsGlobal.Get_TableColumns(mCommand, "dxtpatientdiagnoseslog", "", "b", "");

                string mCommandText = ""
                    + "SELECT "
                    + mTransColumns + ","
                    + mPatientColumns + ", "
                    + mStaffColumns + " "
                    + "FROM dxtpatientdiagnoseslog AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                    + "LEFT OUTER JOIN facilitystaffs AS f ON (f.code = b.doctorcode) ";  

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mExtraFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("dxtpatientdiagnoseslog");
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

        #region View_Episodes
        public DataTable View_Episodes(String mFilter, String mOrder, string mGridName)
        {
            String mFunctionName = "View_Episodes";

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
                string mCommandText = "select * from view_dxtpatientdiagnoses";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_dxtpatientdiagnoses");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName.Trim());

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

        #region View_EpisodeVisits
        public DataTable View_EpisodeVisits(String mFilter, String mOrder, string mGridName)
        {
            String mFunctionName = "View_EpisodeVisits";

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
                string mCommandText = "select * from view_dxtpatientdiagnoseslog";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_dxtpatientdiagnoseslog");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName.Trim());

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

        #region View_Prescription
        public DataTable View_Prescription(string mPatientCode, DateTime mDate, string mGridName)
        {
            String mFunctionName = "View_Prescription";

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
                mDate = mDate.Date;

                string mCommandText = ""
                    + "SELECT "
                    + "* "
                    + "FROM view_dxtpatientprescriptions "
                    + "WHERE patientcode='" + mPatientCode.Trim() + "' and transdate=" + clsGlobal.Saving_DateValue(mDate);

                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_dxtpatientprescriptions");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName.Trim());

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
            string mPatientCode, 
            AfyaPro_Types.clsPatientDiagnosis mPatientDiagnosis,
            DataTable mDtAdmissionDetails, 
            DataTable mDtDischargeDetails, 
            string mUserId)
        {
            String mFunctionName = "Add";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            DataTable mDtBookings = new DataTable("bookings");
            DataTable mDtPatientCorporates = new DataTable("patientcorporates");
            string mNewBooking = "";
            DateTime mPrevBookDate = new DateTime();
            string mPrevDepartment = "";
            bool mIsReAttendance = false;
            string mBillingGroup = "";
            string mBillingSubGroup = "";

            string mWardCode = "";
            string mRemarks = "";
            string mBedCode = "";
            string mRoomCode = "";
            double mWeight = 0;
            double mTemperature = 0;
            string mBillingGroupCode = "";
            string mBillingSubGroupCode = "";
            string mBillingGroupMembershipNo = "";
            string mPriceCategory = "";
            string mTreatmentPointCode = "";
            string mTreatmentPoint = "";
            string mDischargeStatusCode = "";
            string mDischargeStatusDescription = "";
            int mAdmissionId = 0;

            mTransDate = mTransDate.Date;

            #region get admission details
            bool mAdmitting = false;
            if (mDtAdmissionDetails.Rows.Count > 0)
            {
                mAdmitting = true;
                mAdmissionId = Convert.ToInt32(mDtAdmissionDetails.Rows[0]["admissionid"]);
                mWardCode = mDtAdmissionDetails.Rows[0]["wardcode"].ToString().Trim();
                mRoomCode = mDtAdmissionDetails.Rows[0]["roomcode"].ToString().Trim();
                mRemarks = mDtAdmissionDetails.Rows[0]["remarks"].ToString().Trim();
                mBedCode = mDtAdmissionDetails.Rows[0]["bedno"].ToString().Trim();
                mWeight = Convert.ToDouble(mDtAdmissionDetails.Rows[0]["weight"]);
                mTemperature = Convert.ToDouble(mDtAdmissionDetails.Rows[0]["temperature"]);
                mBillingGroupCode = mDtAdmissionDetails.Rows[0]["billinggroupcode"].ToString().Trim();
                mBillingSubGroupCode = mDtAdmissionDetails.Rows[0]["billinggroupcode"].ToString().Trim();
                mBillingGroupMembershipNo = mDtAdmissionDetails.Rows[0]["billinggroupmembershipno"].ToString().Trim();
            }
            #endregion

            #region get discharge details
            bool mDischarging = false;
            if (mDtDischargeDetails.Rows.Count > 0)
            {
                mDischarging = true;
                mAdmissionId = Convert.ToInt32(mDtDischargeDetails.Rows[0]["admissionid"]);
                mWardCode = mDtDischargeDetails.Rows[0]["wardcode"].ToString().Trim();
                mRoomCode = mDtDischargeDetails.Rows[0]["roomcode"].ToString().Trim();
                mRemarks = mDtDischargeDetails.Rows[0]["remarks"].ToString().Trim();
                mBedCode = mDtDischargeDetails.Rows[0]["bedno"].ToString().Trim();
                mDischargeStatusCode = mDtDischargeDetails.Rows[0]["dischargestatuscode"].ToString().Trim();
                mDischargeStatusDescription = mDtDischargeDetails.Rows[0]["dischargestatusdescription"].ToString().Trim();
            }
            #endregion

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.dxtpatientdiagnoses_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region check for patient existance

            try
            {
                mCommand.CommandText = "select * from patients where code='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoDoesNotExist.ToString();
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

                clsWorkFlowAgent mWorkFlowAgent = new clsWorkFlowAgent();
                string mIPDDiagnosisType = "";

                #region do booking

                AfyaPro_Types.clsBooking mBooking;

                if (mAdmitting == true)
                {
                    mBooking = mWorkFlowAgent.Do_Booking(
                        mCommand,
                        mTransDate,
                        mPatientCode,
                        mBillingGroupCode,
                        mBillingSubGroupCode,
                        mBillingGroupMembershipNo,
                        mPriceCategory,
                        mTreatmentPointCode,
                        mTreatmentPoint,
                        mWeight,
                        mTemperature,
                        mAdmitting,
                        mUserId);
                }
                else
                {
                    mBooking = mWorkFlowAgent.Get_Booking(mCommand, mPatientCode);
                }

                if (mBooking.Exe_Result != 1)
                {
                    try { mTrans.Rollback(); }
                    catch { }
                    mResult.Exe_Result = mBooking.Exe_Result;
                    mResult.Exe_Message = mBooking.Exe_Message;
                    return mResult;
                }

                mNewBooking = mBooking.Booking;
                mPrevBookDate = mBooking.BookDate;
                mPrevDepartment = mBooking.Department;
                mIsReAttendance = !mBooking.IsNewAttendance;
                mBillingGroup = mBooking.BillingGroupDescription;
                mBillingSubGroup = mBooking.BillingSubGroupDescription;

                #endregion

                #region admission

                if (mAdmitting == true)
                {
                    AfyaPro_Types.clsBooking mAdmission = mWorkFlowAgent.Do_Admission(
                            mCommand,
                            mAdmissionId,
                            mTransDate,
                            mPatientCode,
                            mWardCode,
                            mRoomCode,
                            mBedCode,
                            mRemarks,
                            mWeight,
                            mTemperature,
                            mBooking,
                            mUserId);

                    if (mAdmission.Exe_Result != 1)
                    {
                        try { mTrans.Rollback(); }
                        catch { }
                        mResult.Exe_Result = mAdmission.Exe_Result;
                        mResult.Exe_Message = mAdmission.Exe_Message;
                        return mResult;
                    }

                    mIPDDiagnosisType = "Admission";
                }

                #endregion

                #region discharging

                if (mDischarging == true)
                {
                    AfyaPro_Types.clsBooking mDischarge = mWorkFlowAgent.Do_Discharging(
                            mCommand,
                            mAdmissionId,
                            mTransDate,
                            mPatientCode,
                            mWardCode,
                            mRoomCode,
                            mBedCode,
                            mDischargeStatusCode,
                            mRemarks,
                            mWeight,
                            mTemperature,
                            mBooking,
                            mUserId);

                    if (mDischarge.Exe_Result != 1)
                    {
                        try { mTrans.Rollback(); }
                        catch { }
                        mResult.Exe_Result = mDischarge.Exe_Result;
                        mResult.Exe_Message = mDischarge.Exe_Message;
                        return mResult;
                    }

                    mIPDDiagnosisType = "Discharge";
                }

                #endregion

                #region diagnoses

                AfyaPro_Types.clsResult mDiagnosesResult = mWorkFlowAgent.Add_Diagnoses(
                        mCommand,
                        mTransDate,
                        mPatientCode,
                        mPatientDiagnosis.diagnosiscode,
                        mPatientDiagnosis.episodecode,
                        mPatientDiagnosis.isprimary,
                        mIPDDiagnosisType,
                        mPatientDiagnosis.history,
                        mPatientDiagnosis.examination,
                        mPatientDiagnosis.investigation,
                        mPatientDiagnosis.treatments,
                        mPatientDiagnosis.referaldescription,
                        mPatientDiagnosis.doctorcode,
                        mWeight,
                        mTemperature,
                        mBooking,
                        mUserId);

                if (mDiagnosesResult.Exe_Result != 1)
                {
                    try { mTrans.Rollback(); }
                    catch { }
                    mResult.Exe_Result = mDiagnosesResult.Exe_Result;
                    mResult.Exe_Message = mDiagnosesResult.Exe_Message;
                    return mResult;
                }

                #endregion

                #region delete from queue

                mCommand.CommandText = "delete from patientsqueue where patientcode='" + mPatientCode.Trim()
                    + "' and (queuetype=" + (int)AfyaPro_Types.clsEnums.PatientsQueueTypes.Consultation
                    + " or queuetype=" + (int)AfyaPro_Types.clsEnums.PatientsQueueTypes.Results + ")";
                mCommand.ExecuteNonQuery();

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
        }
        #endregion

        #region Edit
        public AfyaPro_Types.clsResult Edit(
            int mAutoCode,
            DateTime mTransDate,
            AfyaPro_Types.clsPatientDiagnosis mPatientDiagnosis,
            string mUserId,string mPatientCode)
        {
            String mFunctionName = "Edit";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.dxtpatientdiagnoses_edit.ToString(), mUserId);
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

                clsWorkFlowAgent mWorkFlowAgent = new clsWorkFlowAgent();

                #region diagnoses

                AfyaPro_Types.clsResult mDiagnosesResult = mWorkFlowAgent.Edit_Diagnoses(
                        mCommand,
                        mAutoCode,
                        mTransDate,
                        mPatientDiagnosis.diagnosiscode,
                        mPatientDiagnosis.isprimary,
                        mPatientDiagnosis.history,
                        mPatientDiagnosis.examination,
                        mPatientDiagnosis.investigation,
                        mPatientDiagnosis.treatments,
                        mPatientDiagnosis.referaldescription,
                        mPatientDiagnosis.doctorcode,
                        mUserId);

                if (mDiagnosesResult.Exe_Result != 1)
                {
                    try { mTrans.Rollback(); }
                    catch { }
                    mResult.Exe_Result = mDiagnosesResult.Exe_Result;
                    mResult.Exe_Message = mDiagnosesResult.Exe_Message;
                    return mResult;
                }

                #endregion

                #region delete from queue

                mCommand.CommandText = "delete from patientsqueue where patientcode='" + mPatientCode.Trim()
                    + "' and (queuetype=" + (int)AfyaPro_Types.clsEnums.PatientsQueueTypes.Consultation
                    + " or queuetype=" + (int)AfyaPro_Types.clsEnums.PatientsQueueTypes.Results + ")";
                mCommand.ExecuteNonQuery();

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
        }
        #endregion

        #region Delete
        public AfyaPro_Types.clsResult Delete(int mAutoCode, string mUserId)
        {
            String mFunctionName = "Delete";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;
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
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.dxtpatientdiagnoses_delete.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region delete diagnoses

            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                string mPatientCode = "";
                string mEpisodeCode = "";

                //find patientcode and episodecode
                DataTable mDtLog = new DataTable("log");
                mCommand.CommandText = "select * from dxtpatientdiagnoseslog where autocode=" + mAutoCode;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtLog);

                if (mDtLog.Rows.Count > 0)
                {
                    mPatientCode = mDtLog.Rows[0]["patientcode"].ToString().Trim();
                    mEpisodeCode = mDtLog.Rows[0]["episodecode"].ToString().Trim();
                }

                //dxtpatientdiagnoseslog
                mCommand.CommandText = "delete from dxtpatientdiagnoseslog where autocode=" + mAutoCode;
                mCommand.ExecuteNonQuery();

                //check if all visits for episode has been deleted, if yes then delete episode also
                DataTable mDtVisits = new DataTable("episodevisits");
                mCommand.CommandText = "select * from dxtpatientdiagnoseslog where patientcode='" + mPatientCode + "' and episodecode='" + mEpisodeCode + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtVisits);

                if (mDtVisits.Rows.Count == 0)
                {
                    mCommand.CommandText = "delete from dxtpatientdiagnoses where patientcode='" + mPatientCode + "' and episodecode='" + mEpisodeCode + "'";
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

            #endregion
        }
        #endregion

        #region Treatment_Add
        public AfyaPro_Types.clsResult Treatment_Add(
            DateTime mTransDate,
            string mPatientCode,
            string mDiagnosisCode,
            string mDoctorCode,
            string mStoreCode,
            string mItemCode,
            decimal mQty,
            decimal mAmount,
            string mDosage,
            string mUserId)
        {
            String mFunctionName = "Treatment_Add";
            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;
            OdbcDataReader mDataReader = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            string mCurrentBooking = "";

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.dxtpatientdiagnoses_prescribe.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region check for patient existance

            try
            {
                mCommand.CommandText = "select * from patients where code='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoDoesNotExist.ToString();
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

            #region check for booking

            try
            {
                mCommand.CommandText = "select * from facilitybookings where patientcode='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mCurrentBooking = mDataReader["autocode"].ToString();
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
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                #region dxtpatientprescriptions

                mCommand.CommandText = clsGlobal.Get_DeleteStatement("dxtpatientprescriptions", 
                    "patientcode='" + mPatientCode.Trim() + "' and transdate=" + clsGlobal.Saving_DateValue(mTransDate))
                    + " and itemcode='" + mItemCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                List<clsDataField> mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mCurrentBooking.Trim()));
                mDataFields.Add(new clsDataField("diagnosiscode", DataTypes.dbstring.ToString(), mDiagnosisCode.Trim()));
                mDataFields.Add(new clsDataField("doctorcode", DataTypes.dbstring.ToString(), mDoctorCode.Trim()));
                mDataFields.Add(new clsDataField("storecode", DataTypes.dbstring.ToString(), mStoreCode.Trim()));
                mDataFields.Add(new clsDataField("itemcode", DataTypes.dbstring.ToString(), mItemCode.Trim()));
                mDataFields.Add(new clsDataField("qty", DataTypes.dbdecimal.ToString(), mQty));
                mDataFields.Add(new clsDataField("amount", DataTypes.dbdecimal.ToString(), mAmount));
                mDataFields.Add(new clsDataField("dosage", DataTypes.dbstring.ToString(), mDosage.Trim()));
                mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                mCommand.CommandText = clsGlobal.Get_InsertStatement("dxtpatientprescriptions", mDataFields);
                mCommand.ExecuteNonQuery();

                #endregion

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
        }
        #endregion

        #region Treatment_Delete
        public AfyaPro_Types.clsResult Treatment_Delete(int mAutoCode, string mUserId)
        {
            String mFunctionName = "Treatment_Delete";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;
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
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.dxtpatientdiagnoses_prescribe.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region delete prescription

            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //dxtpatientprescriptions
                mCommand.CommandText = "delete from dxtpatientprescriptions where autocode=" + mAutoCode;
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
    }
}
