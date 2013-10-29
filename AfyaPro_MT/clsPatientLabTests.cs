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
    public class clsPatientLabTests : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsLabTest";

        #endregion

        #region View
        public DataTable View(bool mDateSpecified, DateTime mDateFrom, DateTime mDateTo, string mExtraFilter, 
            string mOrder, string mLanguageName, string mGridName)
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
                //columns from trans
                string mTransColumns = clsGlobal.Get_TableColumns(mCommand, "view_labpatienttests", "", "b", "");

                string mCommandText = ""
                    + "SELECT "
                    + mTransColumns + ","
                    + mPatientColumns + " "
                    + "FROM view_labpatienttests AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code";

                if (mDateSpecified == true)
                {
                    mCommandText = mCommandText + " where (b.transdate between " + clsGlobal.Saving_DateValue(mDateFrom)
                    + " and " + clsGlobal.Saving_DateValue(mDateTo) + ")";

                    if (mExtraFilter.Trim() != "")
                    {
                        mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                    }
                }
                else if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mExtraFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_labpatienttests");
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
                    + "FROM labpatientprescriptions "
                    + "WHERE patientcode='" + mPatientCode.Trim() + "' and transdate=" + clsGlobal.Saving_DateValue(mDate);

                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("labpatientprescriptions");
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

        #region View_ActivePatientDates
        public DataTable View_ActivePatientDates(bool mDateSpecified, DateTime mDateFrom, DateTime mDateTo, string mPatientCode)
        {
            String mFunctionName = "View_ActivePatientDates";

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
                string mCommandText = "";

                if (mDateSpecified == true)
                {
                    mCommandText = "SELECT DISTINCT(transdate) transdate FROM labpatienttests WHERE transdate between "
                    + clsGlobal.Saving_DateValue(mDateFrom) + " and " + clsGlobal.Saving_DateValue(mDateTo)
                    + " and patientcode='" + mPatientCode.Trim() + "' order by transdate desc";
                }
                else
                {
                    mCommandText = "SELECT DISTINCT(transdate) transdate FROM labpatienttests WHERE patientcode='"
                                       + mPatientCode.Trim() + "' order by transdate desc";
                }

                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("labtests");
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

        #region Save
        public AfyaPro_Types.clsResult Save(DateTime mTransDate, String mPatientCode,
            string mLaboratoryCode, string mDoctorCode, string mLabTechnicianCode, DataTable mDtLabTestResults, string mUserId)
        {
            String mFunctionName = "Save";
            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;
            OdbcDataReader mDataReader = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            string mCurrentBooking = "";
            string mDepartment = "";
            double mWeight = 0;
            double mTemperature = 0;

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.labpatienttests_add.ToString(), mUserId);
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
                    mDepartment = mDataReader["department"].ToString();
                    mWeight = Convert.ToDouble(mDataReader["patientweight"]);
                    mTemperature = Convert.ToDouble(mDataReader["patienttemperature"]);
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

                int mRecsAffected = 0;

                foreach (DataRow mDataRow in mDtLabTestResults.Rows)
                {
                    string mLabTestGroupCode = mDataRow["labtestgroupcode"].ToString().Trim();
                    string mLabTestSubGroupCode = mDataRow["labtestsubgroupcode"].ToString().Trim();
                    string mLabTestTypeCode = mDataRow["labtesttypecode"].ToString().Trim();
                    string mResultFigure = mDataRow["resultfigure"].ToString().Trim();
                    string mUnits = mDataRow["units"].ToString().Trim();
                    Int16 mOutOfRange_Normal = Convert.ToInt16(mDataRow["outofrange_normal"]);
                    Int16 mOutOfRange_Equipment = Convert.ToInt16(mDataRow["outofrange_equipment"]);
                    bool mDelete = Convert.ToBoolean(mDataRow["delete"]);
                    string mRemarks = mDataRow["remarks"].ToString().Trim();

                    string mResults = "";
                    if (mResultFigure.Trim() != "")
                    {
                        mResults = mResultFigure + " " + mUnits;
                    }

                    if (mDelete == true)
                    {
                        DataTable mDtPatientTests = new DataTable("labpatienttests");
                        mCommand.CommandText = "select * from labpatienttests where "
                        + "booking='" + mCurrentBooking + "' and patientcode='" + mPatientCode.Trim() + "' and labtesttypecode='" 
                        + mLabTestTypeCode + "' and transdate=" + clsGlobal.Saving_DateValue(mTransDate);
                        mDataAdapter.SelectCommand = mCommand;
                        mDataAdapter.Fill(mDtPatientTests);

                        #region labpatienttests

                        mCommand.CommandText = "delete from labpatienttests where "
                        + "booking='" + mCurrentBooking + "' and patientcode='" + mPatientCode.Trim() + "' and labtesttypecode='"
                        + mLabTestTypeCode + "' and transdate=" + clsGlobal.Saving_DateValue(mTransDate);
                        mRecsAffected = mCommand.ExecuteNonQuery();

                        #endregion

                        #region audit labpatienttests
                        if (mRecsAffected > 0)
                        {
                            mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtPatientTests, "labpatienttests",
                            mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Deleted.ToString());
                            mCommand.ExecuteNonQuery();
                        }
                        #endregion
                    }
                    else
                    {
                        DataTable mDtPatientTests = new DataTable("labpatienttests");
                        mCommand.CommandText = "select * from labpatienttests where "
                        + "booking='" + mCurrentBooking + "' and patientcode='" + mPatientCode.Trim() + "' and labtesttypecode='"
                        + mLabTestTypeCode + "' and transdate=" + clsGlobal.Saving_DateValue(mTransDate);
                        mDataAdapter.SelectCommand = mCommand;
                        mDataAdapter.Fill(mDtPatientTests);

                        if (mDtPatientTests.Rows.Count > 0)
                        {
                            #region labpatienttests

                            mCommand.CommandText = "update labpatienttests set "
                                + "results='" + mResults + "',"
                                + "resultfigure='" + mResultFigure + "',"
                                + "units='" + mUnits + "',"
                                + "remarks='" + mRemarks + "',"
                                + "outofrange_normal=" + mOutOfRange_Normal + ","
                                + "outofrange_equipment=" + mOutOfRange_Equipment + ","
                                + "patientweight=" + mWeight + ","
                                + "patienttemperature=" + mTemperature
                                + " where "
                                + "booking='" + mCurrentBooking + "' and patientcode='" + mPatientCode.Trim() + "' and labtesttypecode='"
                                + mLabTestTypeCode + "' and transdate=" + clsGlobal.Saving_DateValue(mTransDate);

                            mRecsAffected = mCommand.ExecuteNonQuery();

                            #endregion

                            #region audit labpatienttests

                            if (mRecsAffected > 0)
                            {
                                //get current details
                                mDtPatientTests = new DataTable("labpatienttests");
                                mCommand.CommandText = "select * from labpatienttests where "
                                + "booking='" + mCurrentBooking + "' and patientcode='" + mPatientCode.Trim() + "' and labtesttypecode='"
                                + mLabTestTypeCode + "' and transdate=" + clsGlobal.Saving_DateValue(mTransDate);
                                mDataAdapter.SelectCommand = mCommand;
                                mDataAdapter.Fill(mDtPatientTests);

                                mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtPatientTests, "labpatienttests",
                                mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                                mCommand.ExecuteNonQuery();
                            }

                            #endregion
                        }
                        else
                        {
                            #region labpatienttests

                            mCommand.CommandText = "insert into labpatienttests("
                                + "sysdate,"
                                + "transdate,"
                                + "patientcode,"
                                + "booking,"
                                + "laboratorycode,"
                                + "doctorcode,"
                                + "labtechniciancode,"
                                + "labtestgroupcode,"
                                + "labtestsubgroupcode,"
                                + "labtesttypecode,"
                                + "results,"
                                + "resultfigure,"
                                + "units,"
                                + "remarks,"
                                + "outofrange_normal,"
                                + "outofrange_equipment,"
                                + "department,"
                                + "userid,"
                                + "patientweight,"
                                + "patienttemperature"
                                + ") values("
                                + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                                + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                                + mPatientCode + "','"
                                + mCurrentBooking + "','"
                                + mLaboratoryCode.Trim() + "','"
                                + mDoctorCode.Trim() + "','"
                                + mLabTechnicianCode.Trim() + "','"
                                + mLabTestGroupCode.Trim() + "','"
                                + mLabTestSubGroupCode.Trim() + "','"
                                + mLabTestTypeCode + "','"
                                + mResults + "','"
                                + mResultFigure + "','"
                                + mUnits + "','"
                                + mRemarks + "',"
                                + mOutOfRange_Normal + ","
                                + mOutOfRange_Equipment + ",'"
                                + mDepartment.Trim() + "','"
                                + mUserId.Trim() + "',"
                                + mWeight + ","
                                + mTemperature + ")";

                            mRecsAffected = mCommand.ExecuteNonQuery();

                            #endregion

                            #region audit labpatienttests

                            if (mRecsAffected > 0)
                            {
                                string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "labpatienttests";
                                string mAuditingFields = clsGlobal.AuditingFields();
                                string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                    AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                                mCommand.CommandText = "insert into " + mAuditTableName + "("
                                    + "sysdate,"
                                    + "transdate,"
                                    + "patientcode,"
                                    + "booking,"
                                    + "laboratorycode,"
                                    + "doctorcode,"
                                    + "labtechniciancode,"
                                    + "labtestgroupcode,"
                                    + "labtestsubgroupcode,"
                                    + "labtesttypecode,"
                                    + "results,"
                                    + "resultfigure,"
                                    + "units,"
                                    + "remarks,"
                                    + "outofrange_normal,"
                                    + "outofrange_equipment,"
                                    + "department,"
                                    + "userid,"
                                    + "patientweight,"
                                    + "patienttemperature,"
                                    + mAuditingFields + ") values("
                                    + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                                    + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                                    + mPatientCode + "','"
                                    + mCurrentBooking + "','"
                                    + mLaboratoryCode.Trim() + "','"
                                    + mDoctorCode.Trim() + "','"
                                    + mLabTechnicianCode.Trim() + "','"
                                    + mLabTestGroupCode.Trim() + "','"
                                    + mLabTestSubGroupCode.Trim() + "','"
                                    + mLabTestTypeCode + "','"
                                    + mResults + "','"
                                    + mResultFigure + "','"
                                    + mUnits + "','"
                                    + mRemarks + "',"
                                    + mOutOfRange_Normal + ","
                                    + mOutOfRange_Equipment + ",'"
                                    + mDepartment.Trim() + "','"
                                    + mUserId.Trim() + "',"
                                    + mWeight + ","
                                    + mTemperature + ","
                                    + mAuditingValues + ")";

                                mCommand.ExecuteNonQuery();
                            }

                            #endregion
                        }
                    }
                }

                #region patientsqueue

                //patientsqueue
                mCommand.CommandText = "delete from patientsqueue where patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                //get current treatment point
                string mTreatmentPointCode = "";

                DataTable mDtTreatmentPoints = new DataTable("treatmentpoints");
                mCommand.CommandText = "select * from facilitybookings where patientcode='" + mPatientCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtTreatmentPoints);
                if (mDtTreatmentPoints.Rows.Count > 0)
                {
                    mTreatmentPointCode = mDtTreatmentPoints.Rows[0]["wheretakencode"].ToString().Trim();
                }

                mCommand.CommandText = "insert into patientsqueue(sysdate,transdate,treatmentpointcode,laboratorycode,patientcode,queuetype) "
                + "values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                + mTreatmentPointCode + "','" + mLaboratoryCode.Trim() + "','" + mPatientCode.Trim() + "'," + (int)AfyaPro_Types.clsEnums.PatientsQueueTypes.Results + ")";
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

        #region Prescribe
        public AfyaPro_Types.clsResult Prescribe(
            DateTime mTransDate, 
            String mPatientCode,
            string mLaboratoryCode, 
            string mDoctorCode, 
            DataTable mDtPrescriptions, 
            string mUserId)
        {
            String mFunctionName = "Prescribe";
            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;
            OdbcDataReader mDataReader = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.labpatienttests_add.ToString(), mUserId);
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
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                #region labpatientprescriptions

                foreach (DataRow mDataRow in mDtPrescriptions.Rows)
                {
                    string mLabTestGroupCode = mDataRow["labtestgroupcode"].ToString().Trim();
                    string mLabTestSubGroupCode = mDataRow["labtestsubgroupcode"].ToString().Trim();
                    string mLabTestTypeCode = mDataRow["labtesttypecode"].ToString().Trim();
                    bool mDelete = Convert.ToBoolean(mDataRow["delete"]);

                    if (mDelete == true)
                    {
                        mCommand.CommandText = clsGlobal.Get_DeleteStatement("labpatientprescriptions", "patientcode='" + mPatientCode.Trim() + "' and labtesttypecode='" + mLabTestTypeCode
                            + "' and transdate=" + clsGlobal.Saving_DateValue(mTransDate));
                        mCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        List<clsDataField> mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                        mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                        mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                        mDataFields.Add(new clsDataField("laboratorycode", DataTypes.dbstring.ToString(), mLaboratoryCode.Trim()));
                        mDataFields.Add(new clsDataField("doctorcode", DataTypes.dbstring.ToString(), mDoctorCode.Trim()));
                        mDataFields.Add(new clsDataField("labtestgroupcode", DataTypes.dbstring.ToString(), mLabTestGroupCode.Trim()));
                        mDataFields.Add(new clsDataField("labtestsubgroupcode", DataTypes.dbstring.ToString(), mLabTestSubGroupCode.Trim()));
                        mDataFields.Add(new clsDataField("labtesttypecode", DataTypes.dbstring.ToString(), mLabTestTypeCode.Trim()));
                        mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                        mCommand.CommandText = clsGlobal.Get_InsertStatement("labpatientprescriptions", mDataFields);
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region patientsqueue

                //patientsqueue
                mCommand.CommandText = "delete from patientsqueue where patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                //get current treatment point
                string mTreatmentPointCode = "";

                DataTable mDtTreatmentPoints = new DataTable("treatmentpoints");
                mCommand.CommandText = "select * from facilitybookings where patientcode='" + mPatientCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtTreatmentPoints);
                if (mDtTreatmentPoints.Rows.Count > 0)
                {
                    mTreatmentPointCode = mDtTreatmentPoints.Rows[0]["wheretakencode"].ToString().Trim();
                }

                mCommand.CommandText = "insert into patientsqueue(sysdate,transdate,treatmentpointcode,laboratorycode,patientcode,queuetype) "
                + "values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                + mTreatmentPointCode + "','" + mLaboratoryCode.Trim() + "','" + mPatientCode.Trim() + "'," + (int)AfyaPro_Types.clsEnums.PatientsQueueTypes.LabTests + ")";
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
    }
}
