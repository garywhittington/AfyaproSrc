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
    internal class clsWorkFlowAgent
    {
        string pClassName = "AfyaPro_MT.clsWorkFlowAgent";

        #region Get_Booking
        public AfyaPro_Types.clsBooking Get_Booking(OdbcCommand mCommand, string mPatientCode)
        {
            string mFunctionName = "Get_Booking";

            AfyaPro_Types.clsBooking mBooking = new AfyaPro_Types.clsBooking();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            #region get booking details
            try
            {
                DataTable mDtBookings = new DataTable("bookings");
                mCommand.CommandText = "select * from facilitybookings where patientcode='" + mPatientCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtBookings);
                if (mDtBookings.Rows.Count > 0)
                {
                    mBooking.IsBooked = true;
                    mBooking.PatientCode = mDtBookings.Rows[0]["patientcode"].ToString().Trim();
                    mBooking.WhereTakenCode = mDtBookings.Rows[0]["wheretakencode"].ToString().Trim();
                    mBooking.WhereTaken = mDtBookings.Rows[0]["wheretaken"].ToString().Trim();
                    mBooking.Department = mDtBookings.Rows[0]["department"].ToString();
                    mBooking.BookDate = Convert.ToDateTime(mDtBookings.Rows[0]["bookdate"]);
                    mBooking.Booking = mDtBookings.Rows[0]["autocode"].ToString();
                    mBooking.BillingGroupCode = mDtBookings.Rows[0]["billinggroupcode"].ToString().Trim();
                    mBooking.BillingSubGroupCode = mDtBookings.Rows[0]["billingsubgroupcode"].ToString().Trim();
                    mBooking.BillingGroupMembershipNo = mDtBookings.Rows[0]["billinggroupmembershipno"].ToString().Trim();
                    mBooking.Refered = Convert.ToInt16(mDtBookings.Rows[0]["refered"]);
                    mBooking.ReferedFacility = mDtBookings.Rows[0]["referedfacility"].ToString().Trim();
                    mBooking.Weight = Convert.ToDouble(mDtBookings.Rows[0]["patientweight"]);
                    mBooking.Temperature = Convert.ToDouble(mDtBookings.Rows[0]["patienttemperature"]);

                    //BillingGroupDescription, PriceName
                    DataTable mDtGroups = new DataTable("groups");
                    mCommand.CommandText = "select * from facilitycorporates where code='" + mBooking.BillingGroupCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtGroups);
                    if (mDtGroups.Rows.Count > 0)
                    {
                        mBooking.BillingGroupDescription = mDtGroups.Rows[0]["description"].ToString().Trim();
                        mBooking.PriceName = mDtGroups.Rows[0]["pricecategory"].ToString().Trim();
                    }
                    else
                    {
                        mBooking.PriceName = mDtBookings.Rows[0]["pricecategory"].ToString().Trim();
                    }

                    //PriceDescription
                    DataTable mDtPriceCategories = new DataTable("pricecategories");
                    mCommand.CommandText = "select * from facilitypricecategories";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtPriceCategories);
                    if (mBooking.PriceName.Trim() != "")
                    {
                        if (mDtPriceCategories.Rows.Count > 0)
                        {
                            mBooking.PriceDescription = mDtPriceCategories.Rows[0][mBooking.PriceName].ToString().Trim();
                        }
                    }

                    //BillingSubGroupDescription
                    DataTable mDtSubGroups = new DataTable("subgroups");
                    mCommand.CommandText = "select * from facilitycorporatesubgroups where code='" + mBooking.BillingSubGroupCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtSubGroups);
                    if (mDtSubGroups.Rows.Count > 0)
                    {
                        mBooking.BillingSubGroupDescription = mDtSubGroups.Rows[0]["description"].ToString().Trim();
                    }

                    //BalanceDue
                    DataTable mDtDebtors = new DataTable("debtors");
                    mCommand.CommandText = "select accountcode,sum(balance) balance from billdebtors where accountcode='"
                    + mPatientCode.Trim() + "' and debtortype='" + AfyaPro_Types.clsEnums.DebtorTypes.Individual.ToString().Trim()
                    + "' group by accountcode";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtDebtors);
                    if (mDtDebtors.Rows.Count > 0)
                    {
                        mBooking.BalanceDue = Convert.ToDouble(mDtDebtors.Rows[0]["balance"]);
                    }
                }
                else
                {
                    mBooking.IsBooked = false;
                    mBooking.Exe_Result = 0;
                    mBooking.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientIsNotBooked.ToString();
                    return mBooking;
                }

                return mBooking;
            }
            catch (OdbcException ex)
            {
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
            #endregion
        }
        #endregion

        #region Do_Booking
        public AfyaPro_Types.clsBooking Do_Booking(OdbcCommand mCommand, DateTime mTransDate, string mPatientCode, 
            string mBillingGroupCode, string mBillingSubGroupCode, string mBillingGroupMembershipNo, 
            string mPriceCategory, string mTreatmentPointCode, string mTreatmentPoint, double mWeight, double mTemperature, 
            bool mAdmitting, string mUserId)
        {
            String mFunctionName = "Do_Booking";

            AfyaPro_Types.clsBooking mBooking = new AfyaPro_Types.clsBooking();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            DataTable mDtBookings = new DataTable("bookings");
            DataTable mDtPatientCorporates = new DataTable("patientcorporates");
            string mNewBooking = "";
            string mPrevBooking = "";
            DateTime mPrevBookDate = new DateTime();
            bool mIsReAttendance = false;
            bool mIsForcedReAttendance = false;
            string mBillingGroup = "";
            string mBillingSubGroup = "";

            string mDepartment = AfyaPro_Types.clsEnums.PatientCategories.OPD.ToString();

            mTransDate = mTransDate.Date;

            #region this will update weight & temperature for this visit
            try
            {
                mCommand.CommandText = "update patients set weight=" + mWeight + ",temperature=" + mTemperature
                + " where code='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

               
            }
            catch (OdbcException ex)
            {
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
            }
            #endregion

            #region check for reattendance

            try
            {
                mDtBookings = new DataTable("bookings");
                mCommand.CommandText = "select * from facilitybookings where patientcode='" + mPatientCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtBookings);

                if (mDtBookings.Rows.Count > 0)
                {
                    mPrevBooking = mDtBookings.Rows[0]["autocode"].ToString();
                    mPrevBookDate = Convert.ToDateTime(mDtBookings.Rows[0]["bookdate"]);
                    mIsReAttendance = true;

                    if (mPriceCategory.Trim() == "")
                    {
                        mPriceCategory = mDtBookings.Rows[0]["pricecategory"].ToString();
                    }

                    mDepartment = mDtBookings.Rows[0]["department"].ToString().Trim();

                    if (mDepartment.ToLower() == AfyaPro_Types.clsEnums.PatientCategories.IPD.ToString().Trim().ToLower() && mAdmitting == true)
                    {
                        //mBooking.Exe_Result = 0;
                        //mBooking.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientHasNotBeenDischarged.ToString();
                        //return mBooking;

                        mCommand.CommandText = "update facilitybookings set department = 'OPD' where patientcode='" + mPatientCode.Trim() + "'";
                    }
                }
            }
            catch (OdbcException ex)
            {
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
            }

            #endregion

            #region check for forced reattendance

            if (mIsReAttendance == false)
            {
                try
                {
                    DataTable mDtRevisitingNumbers = new DataTable("revisitingnumbers");
                    mCommand.CommandText = "select * from revisitingnumbers where patientcode='" + mPatientCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtRevisitingNumbers);

                    if (mDtRevisitingNumbers.Rows.Count > 0)
                    {
                        mIsForcedReAttendance = true;
                    }
                }
                catch (OdbcException ex)
                {
                    mBooking.Exe_Result = -1;
                    mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                    return mBooking;
                }
            }

            #endregion

            //

            try
            {
                int mYearPart = mTransDate.Year;
                int mMonthPart = mTransDate.Month;
                int mRecsAffected = 0;

                if (mAdmitting == true)
                {
                    mDepartment = AfyaPro_Types.clsEnums.PatientCategories.IPD.ToString();
                }

                mCommand.CommandText = "select * from facilitycorporates where code='" + mBillingGroupCode.Trim() + "'";
                DataTable mDtBillingGroups = new DataTable("facilitycorporates");
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtBillingGroups);
                if (mDtBillingGroups.Rows.Count > 0)
                {
                    mBillingGroup = mDtBillingGroups.Rows[0]["description"].ToString().Trim();
                }

                mCommand.CommandText = "select * from facilitycorporatesubgroups where code='" + mBillingSubGroupCode.Trim() + "'";
                DataTable mDtBillingSubGroups = new DataTable("facilitycorporatesubgroups");
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtBillingSubGroups);
                if (mDtBillingSubGroups.Rows.Count > 0)
                {
                    mBillingSubGroup = mDtBillingSubGroups.Rows[0]["description"].ToString().Trim();
                }

                if (mIsReAttendance == true || mIsForcedReAttendance == true)
                {
                    if (mPrevBookDate == mTransDate)
                    {
                        #region booking update

                        //facilitybookings
                        mCommand.CommandText = "update facilitybookings set patientcode='" + mPatientCode.Trim()
                        + "',wheretakencode='" + mTreatmentPointCode.Trim() + "',department='"
                        + mDepartment + "',wheretaken='" + mTreatmentPoint.Trim()
                        + "',billinggroupcode='" + mBillingGroupCode.Trim() + "',billingsubgroupcode='"
                        + mBillingSubGroupCode.Trim() + "',billinggroupmembershipno='" + mBillingGroupMembershipNo.Trim()
                        + "',pricecategory='" + mPriceCategory.Trim() + "',userid='" + mUserId.Trim()
                        + "',patientweight=" + mWeight + ",patienttemperature=" + mTemperature
                        + " where autocode=" + mPrevBooking.Trim();
                        mCommand.ExecuteNonQuery();

                        //facilitybookinglog
                        mCommand.CommandText = "update facilitybookinglog set patientcode='" + mPatientCode.Trim()
                        + "',department='" + mDepartment + "',wheretakencode='"
                        + mTreatmentPointCode.Trim() + "',wheretaken='" + mTreatmentPoint.Trim()
                        + "',billinggroupcode='" + mBillingGroupCode.Trim() + "',billinggroup='" + mBillingGroup
                        + "',billingsubgroupcode='" + mBillingSubGroupCode.Trim() + "',billingsubgroup='"
                        + mBillingSubGroup + "',billinggroupmembershipno='" + mBillingGroupMembershipNo.Trim()
                        + "',userid='" + mUserId.Trim() + "',patientweight=" + mWeight + ",patienttemperature=" + mTemperature
                        + " where booking='" + mPrevBooking.Trim() + "' and patientcode='" + mPatientCode.Trim() + "'";
                        mRecsAffected = mCommand.ExecuteNonQuery();

                        #region audit facilitybookinglog
                        if (mRecsAffected > 0)
                        {
                            DataTable mDtBookingLog = new DataTable("facilitybookinglog");
                            mCommand.CommandText = "select * from facilitybookinglog where booking='" + mPrevBooking
                            + "' and patientcode='" + mPatientCode.Trim() + "'";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(mDtBookingLog);

                            mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtBookingLog, "facilitybookinglog",
                            mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                            mCommand.ExecuteNonQuery();
                        }
                        #endregion

                        #endregion
                    }
                    else
                    {
                        if (mIsForcedReAttendance == true)
                        {
                            #region forced re-attendance

                            //generate booking id
                            clsAutoCodes objAutoCodes = new clsAutoCodes();
                            AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                            mObjCode = objAutoCodes.Next_Code(mCommand,
                                Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.bookingno), "facilitybookinglog", "booking");
                            if (mObjCode.Exe_Result == -1)
                            {
                                mBooking.Exe_Result = 0;
                                mBooking.Exe_Message = "Duplicate booking number detected";
                                return mBooking;
                            }
                            mNewBooking = mObjCode.GeneratedCode;

                            //create new booking
                            mCommand.CommandText = "insert into facilitybookings(autocode,patientcode,bookdate,department,"
                            + "wheretakencode,wheretaken,billinggroupcode,billingsubgroupcode,billinggroupmembershipno,"
                            + "yearpart,monthpart,sysdate,patientweight,patienttemperature,userid,pricecategory) values('"
                            + mNewBooking + "','" + mPatientCode.Trim() + "',"
                            + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mDepartment
                            + "','" + mTreatmentPointCode.Trim() + "','" + mTreatmentPoint.Trim() + "','"
                            + mBillingGroupCode.Trim() + "','" + mBillingSubGroupCode.Trim()
                            + "','" + mBillingGroupMembershipNo.Trim() + "'," + mYearPart + ","
                            + mMonthPart + "," + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                            + mWeight + "," + mTemperature + ",'" + mUserId.Trim() + "','" + mPriceCategory.Trim() + "')";
                            mCommand.ExecuteNonQuery();

                            //create a log for the new booking
                            mCommand.CommandText = "insert into facilitybookinglog(patientcode,booking,bookdate,department,"
                            + "wheretakencode,wheretaken,billinggroupcode,billinggroup,billingsubgroupcode,billingsubgroup,"
                            + "billinggroupmembershipno,registrystatus,yearpart,monthpart,sysdate,patientweight,patienttemperature,userid) values('" 
                            + mPatientCode.Trim() + "','" + mNewBooking + "',"
                            + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mDepartment
                            + "','" + mTreatmentPointCode.Trim() + "','" + mTreatmentPoint.Trim() + "','"
                            + mBillingGroupCode.Trim() + "','" + mBillingGroup + "','" + mBillingSubGroupCode.Trim() + "','"
                            + mBillingSubGroup + "','" + mBillingGroupMembershipNo.Trim() + "','"
                            + AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString() + "'," + mYearPart + ","
                            + mMonthPart + "," + clsGlobal.Saving_DateValue(DateTime.Now) + "," + mWeight + "," + mTemperature
                            + ",'" + mUserId.Trim() + "')";
                            mRecsAffected = mCommand.ExecuteNonQuery();

                            #region audit facilitybookinglog

                            if (mRecsAffected > 0)
                            {
                                string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "facilitybookinglog";
                                string mAuditingFields = clsGlobal.AuditingFields();
                                string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                    AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                                mCommand.CommandText = "insert into " + mAuditTableName + "(patientcode,booking,bookdate,department,"
                                + "wheretakencode,wheretaken,billinggroupcode,billinggroup,billingsubgroupcode,billingsubgroup,"
                                + "billinggroupmembershipno,registrystatus,yearpart,monthpart,sysdate,patientweight,patienttemperature,userid,"
                                + mAuditingFields + ") values('" + mPatientCode.Trim() + "','" + mNewBooking + "',"
                                + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mDepartment
                                + "','" + mTreatmentPointCode.Trim() + "','" + mTreatmentPoint.Trim() + "','"
                                + mBillingGroupCode.Trim() + "','" + mBillingGroup + "','" + mBillingSubGroupCode.Trim() + "','"
                                + mBillingSubGroup + "','" + mBillingGroupMembershipNo.Trim() + "','"
                                + AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString() + "'," + mYearPart + ","
                                + mMonthPart + "," + clsGlobal.Saving_DateValue(DateTime.Now) + "," + mWeight + "," + mTemperature
                                + ",'" + mUserId.Trim() + "'," + mAuditingValues + ")";
                                mCommand.ExecuteNonQuery();
                            }

                            #endregion

                            //facilitycorporatemembers
                            if (mBillingGroupCode.Trim() != "" && mBillingGroupMembershipNo.Trim() != "")
                            {
                                mCommand.CommandText = "update facilitycorporatemembers set inactive=1, patientcode='" + mPatientCode.Trim() 
                                + "' where billinggroupcode='" + mBillingGroupCode.Trim() + "' and billinggroupmembershipno='" + mBillingGroupMembershipNo.Trim()
                                + "' and membershipstatus=1";
                                mCommand.ExecuteNonQuery();
                            }

                            //patientsqueue
                            mCommand.CommandText = "delete from patientsqueue where patientcode='" + mPatientCode.Trim() + "'";
                            mCommand.ExecuteNonQuery();

                            mCommand.CommandText = "insert into patientsqueue(sysdate,transdate,treatmentpointcode,patientcode,queuetype) "
                            + "values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                            + mTreatmentPointCode.Trim() + "','" + mPatientCode.Trim() + "'," + (int)AfyaPro_Types.clsEnums.PatientsQueueTypes.Consultation + ")";
                            mCommand.ExecuteNonQuery();

                            //delete reattendance numbers
                            mCommand.CommandText = "delete from revisitingnumbers where patientcode='" + mPatientCode.Trim() + "'";
                            mCommand.ExecuteNonQuery();

                            //prepare next booking #
                            mCommand.CommandText = "update facilityautocodes set "
                            + "idcurrent=idcurrent+idincrement where codekey="
                            + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.bookingno);
                            mCommand.ExecuteNonQuery();

                            #endregion
                        }
                        else
                        {
                            #region normal re-attendance

                            //generate booking id
                            clsAutoCodes objAutoCodes = new clsAutoCodes();
                            AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                            mObjCode = objAutoCodes.Next_Code(mCommand,
                                Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.bookingno), "facilitybookinglog", "booking");
                            if (mObjCode.Exe_Result == -1)
                            {
                                mBooking.Exe_Result = 0;
                                mBooking.Exe_Message = "Duplicate booking number detected";
                                return mBooking;
                            }
                            mNewBooking = mObjCode.GeneratedCode;

                            //delete previous booking
                            mCommand.CommandText = "delete from facilitybookings where patientcode='" + mPatientCode.Trim() + "'";
                            mCommand.ExecuteNonQuery();

                            //create new booking
                            mCommand.CommandText = "insert into facilitybookings(autocode,patientcode,bookdate,department,"
                            + "wheretakencode,wheretaken,billinggroupcode,billingsubgroupcode,billinggroupmembershipno,"
                            + "yearpart,monthpart,sysdate,patientweight,patienttemperature,userid,pricecategory) values('" 
                            + mNewBooking + "','" + mPatientCode.Trim() + "',"
                            + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mDepartment
                            + "','" + mTreatmentPointCode.Trim() + "','" + mTreatmentPoint.Trim() + "','"
                            + mBillingGroupCode.Trim() + "','" + mBillingSubGroupCode.Trim() + "','"
                            + mBillingGroupMembershipNo.Trim() + "'," + mYearPart + "," + mMonthPart + ","
                            + clsGlobal.Saving_DateValue(DateTime.Now) + "," + mWeight + "," + mTemperature + ",'" + mUserId.Trim()
                            + "','" + mPriceCategory.Trim() + "')";
                            mCommand.ExecuteNonQuery();

                            //create a log for the new booking
                            mCommand.CommandText = "insert into facilitybookinglog(patientcode,booking,bookdate,department,"
                            + "wheretakencode,wheretaken,billinggroupcode,billinggroup,billingsubgroupcode,billingsubgroup,"
                            + "billinggroupmembershipno,registrystatus,yearpart,monthpart,sysdate,patientweight,patienttemperature,userid) values('" 
                            + mPatientCode.Trim() + "','" + mNewBooking + "',"
                            + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mDepartment
                            + "','" + mTreatmentPointCode.Trim() + "','" + mTreatmentPoint.Trim() + "','"
                            + mBillingGroupCode.Trim() + "','" + mBillingGroup + "','" + mBillingSubGroupCode.Trim() + "','"
                            + mBillingSubGroup + "','" + mBillingGroupMembershipNo.Trim() + "','"
                            + AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString() + "'," + mYearPart + "," + mMonthPart
                            + "," + clsGlobal.Saving_DateValue(DateTime.Now) + "," + mWeight + "," + mTemperature + ",'" + mUserId.Trim()
                            + "')";
                            mRecsAffected = mCommand.ExecuteNonQuery();

                            #region audit facilitybookinglog

                            if (mRecsAffected > 0)
                            {
                                string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "facilitybookinglog";
                                string mAuditingFields = clsGlobal.AuditingFields();
                                string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                    AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                                mCommand.CommandText = "insert into " + mAuditTableName + "(patientcode,booking,bookdate,department,"
                                + "wheretakencode,wheretaken,billinggroupcode,billinggroup,billingsubgroupcode,billingsubgroup,"
                                + "billinggroupmembershipno,registrystatus,yearpart,monthpart,sysdate,patientweight,patienttemperature,userid,"
                                + mAuditingFields + ") values('" + mPatientCode.Trim() + "','" + mNewBooking + "',"
                                + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mDepartment
                                + "','" + mTreatmentPointCode.Trim() + "','" + mTreatmentPoint.Trim() + "','"
                                + mBillingGroupCode.Trim() + "','" + mBillingGroup + "','" + mBillingSubGroupCode.Trim() + "','"
                                + mBillingSubGroup + "','" + mBillingGroupMembershipNo.Trim() + "','"
                                + AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString() + "'," + mYearPart + "," + mMonthPart
                                + "," + clsGlobal.Saving_DateValue(DateTime.Now) + "," + mWeight + "," + mTemperature + ",'" + mUserId.Trim()
                                + "'," + mAuditingValues + ")";
                                mCommand.ExecuteNonQuery();
                            }

                            #endregion

                            //facilitycorporatemembers
                            if (mBillingGroupCode.Trim() != "" && mBillingGroupMembershipNo.Trim() != "")
                            {
                                mCommand.CommandText = "update facilitycorporatemembers set inactive=1,patientcode='" + mPatientCode.Trim() + "' where billinggroupcode='"
                                + mBillingGroupCode.Trim() + "' and billinggroupmembershipno='" + mBillingGroupMembershipNo.Trim()
                                + "' and membershipstatus=1";
                                mCommand.ExecuteNonQuery();
                            }

                            //patientsqueue
                            mCommand.CommandText = "delete from patientsqueue where patientcode='" + mPatientCode.Trim() + "'";
                            mCommand.ExecuteNonQuery();

                            mCommand.CommandText = "insert into patientsqueue(sysdate,transdate,treatmentpointcode,patientcode,queuetype) "
                            + "values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                            + mTreatmentPointCode.Trim() + "','" + mPatientCode.Trim() + "'," + (int)AfyaPro_Types.clsEnums.PatientsQueueTypes.Consultation + ")";
                            mCommand.ExecuteNonQuery();

                            //prepare next booking #
                            mCommand.CommandText = "update facilityautocodes set "
                            + "idcurrent=idcurrent+idincrement where codekey="
                            + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.bookingno);
                            mCommand.ExecuteNonQuery();

                            #endregion
                        }
                    }
                }
                else
                {
                    #region new attendance

                    //generate booking id
                    clsAutoCodes objAutoCodes = new clsAutoCodes();
                    AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                    mObjCode = objAutoCodes.Next_Code(mCommand,
                        Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.bookingno), "facilitybookinglog", "booking");
                    if (mObjCode.Exe_Result == -1)
                    {
                        mBooking.Exe_Result = 0;
                        mBooking.Exe_Message = "Duplicate booking number detected";
                        return mBooking;
                    }
                    mNewBooking = mObjCode.GeneratedCode;

                    //create new booking
                    mCommand.CommandText = "insert into facilitybookings(autocode,patientcode,bookdate,department,"
                    + "wheretakencode,wheretaken,billinggroupcode,billingsubgroupcode,billinggroupmembershipno,"
                    + "yearpart,monthpart,sysdate,patientweight,patienttemperature,userid,pricecategory) values('" 
                    + mNewBooking + "','" + mPatientCode.Trim() + "',"
                    + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mDepartment
                    + "','" + mTreatmentPointCode.Trim() + "','" + mTreatmentPoint.Trim() + "','"
                    + mBillingGroupCode.Trim() + "','" + mBillingSubGroupCode.Trim() + "','"
                    + mBillingGroupMembershipNo.Trim() + "'," + mYearPart + "," + mMonthPart + ","
                    + clsGlobal.Saving_DateValue(DateTime.Now) + "," + mWeight + "," + mTemperature + ",'" + mUserId.Trim()
                    + "','" + mPriceCategory.Trim() + "')";
                    mCommand.ExecuteNonQuery();

                    //create a log for the new booking
                    mCommand.CommandText = "insert into facilitybookinglog(patientcode,booking,bookdate,department,"
                    + "wheretakencode,wheretaken,billinggroupcode,billinggroup,billingsubgroupcode,billingsubgroup,billinggroupmembershipno,"
                    + "registrystatus,yearpart,monthpart,sysdate,patientweight,patienttemperature,userid) values('"
                    + mPatientCode.Trim() + "','" + mNewBooking + "'," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                    + mDepartment + "','" + mTreatmentPointCode.Trim() + "','"
                    + mTreatmentPoint.Trim() + "','" + mBillingGroupCode.Trim() + "','" + mBillingGroup + "','" + mBillingSubGroupCode.Trim() + "','"
                    + mBillingSubGroup + "','" + mBillingGroupMembershipNo.Trim() + "','"
                    + AfyaPro_Types.clsEnums.RegistryStatus.New.ToString() + "'," + mYearPart + "," + mMonthPart
                    + "," + clsGlobal.Saving_DateValue(DateTime.Now) + "," + mWeight + "," + mTemperature + ",'" + mUserId.Trim() + "')";
                    mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit facilitybookinglog

                    if (mRecsAffected > 0)
                    {
                        string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "facilitybookinglog";
                        string mAuditingFields = clsGlobal.AuditingFields();
                        string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                            AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                        mCommand.CommandText = "insert into " + mAuditTableName + "(patientcode,booking,bookdate,department,"
                        + "wheretakencode,wheretaken,billinggroupcode,billinggroup,billingsubgroupcode,billingsubgroup,billinggroupmembershipno,"
                        + "registrystatus,yearpart,monthpart,sysdate,patientweight,patienttemperature,userid," + mAuditingFields + ") values('"
                        + mPatientCode.Trim() + "','" + mNewBooking + "'," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                        + mDepartment + "','" + mTreatmentPointCode.Trim() + "','"
                        + mTreatmentPoint.Trim() + "','" + mBillingGroupCode.Trim() + "','" + mBillingGroup + "','" + mBillingSubGroupCode.Trim() + "','"
                        + mBillingSubGroup + "','" + mBillingGroupMembershipNo.Trim() + "','"
                        + AfyaPro_Types.clsEnums.RegistryStatus.New.ToString() + "'," + mYearPart + "," + mMonthPart
                        + "," + clsGlobal.Saving_DateValue(DateTime.Now) + "," + mWeight + "," + mTemperature + ",'" + mUserId.Trim()
                        + "'," + mAuditingValues + ")";
                        mCommand.ExecuteNonQuery();
                    }

                    #endregion

                    //facilitycorporatemembers
                    if (mBillingGroupCode.Trim() != "" && mBillingGroupMembershipNo.Trim() != "")
                    {
                        mCommand.CommandText = "update facilitycorporatemembers set inactive=1,patientcode='" + mPatientCode.Trim() + "' where billinggroupcode='"
                        + mBillingGroupCode.Trim() + "' and billinggroupmembershipno='" + mBillingGroupMembershipNo.Trim()
                        + "' and membershipstatus=1";
                        mCommand.ExecuteNonQuery();
                    }

                    //patientsqueue
                    mCommand.CommandText = "delete from patientsqueue where patientcode='" + mPatientCode.Trim() + "'";
                    mCommand.ExecuteNonQuery();

                    mCommand.CommandText = "insert into patientsqueue(sysdate,transdate,treatmentpointcode,patientcode,queuetype) "
                    + "values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                    + mTreatmentPointCode.Trim() + "','" + mPatientCode.Trim() + "'," + (int)AfyaPro_Types.clsEnums.PatientsQueueTypes.Consultation + ")";
                    mCommand.ExecuteNonQuery();

                    //prepare next booking #
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.bookingno);
                    mCommand.ExecuteNonQuery();

                    #endregion
                }

                //this patient has to be available when doing searching
                mCommand.CommandText = "update patients set fromopd=1 where code='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                //get booking that has been done
                mBooking = this.Get_Booking(mCommand, mPatientCode);

                if (mIsReAttendance == false && mIsForcedReAttendance == false)
                {
                    mBooking.IsNewAttendance = true;
                }
                else
                {
                    mBooking.IsNewAttendance = false;
                }

                //return
                return mBooking;
            }
            catch (OdbcException ex)
            {
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
            }
        }
        #endregion

        #region Do_ChangeBooking
        public AfyaPro_Types.clsBooking Do_ChangeBooking(OdbcCommand mCommand, DateTime mTransDate, string mPatientCode,
            string mBillingGroupCode, string mBillingSubGroupCode, string mBillingGroupMembershipNo,
            string mPriceCategory, string mUserId)
        {
            String mFunctionName = "Do_ChangeBooking";

            AfyaPro_Types.clsBooking mBooking = new AfyaPro_Types.clsBooking();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            DataTable mDtBookings = new DataTable("bookings");
            DataTable mDtPatientCorporates = new DataTable("patientcorporates");
            string mPrevBooking = "";
            string mBillingGroup = "";
            string mBillingSubGroup = "";

            mTransDate = mTransDate.Date;

            string mDepartment = AfyaPro_Types.clsEnums.PatientCategories.OPD.ToString();

            #region get current booking

            try
            {
                mDtBookings = new DataTable("bookings");
                mCommand.CommandText = "select * from facilitybookings where patientcode='" + mPatientCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtBookings);

                if (mDtBookings.Rows.Count > 0)
                {
                    mPrevBooking = mDtBookings.Rows[0]["autocode"].ToString().Trim();
                }
            }
            catch (OdbcException ex)
            {
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
            }

            #endregion

            try
            {

                mCommand.CommandText = "select * from facilitycorporates where code='" + mBillingGroupCode.Trim() + "'";
                DataTable mDtBillingGroups = new DataTable("facilitycorporates");
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtBillingGroups);
                if (mDtBillingGroups.Rows.Count > 0)
                {
                    mBillingGroup = mDtBillingGroups.Rows[0]["description"].ToString().Trim();
                }

                mCommand.CommandText = "select * from facilitycorporatesubgroups where code='" + mBillingSubGroupCode.Trim() + "'";
                DataTable mDtBillingSubGroups = new DataTable("facilitycorporatesubgroups");
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtBillingSubGroups);
                if (mDtBillingSubGroups.Rows.Count > 0)
                {
                    mBillingSubGroup = mDtBillingSubGroups.Rows[0]["description"].ToString().Trim();
                }

                //facilitybookings
                mCommand.CommandText = "update facilitybookings set patientcode='" + mPatientCode.Trim()
                + "',billinggroupcode='" + mBillingGroupCode.Trim() + "',billingsubgroupcode='"
                + mBillingSubGroupCode.Trim() + "',billinggroupmembershipno='" + mBillingGroupMembershipNo.Trim()
                + "',pricecategory='" + mPriceCategory.Trim() + "' where autocode=" + mPrevBooking.Trim();
                mCommand.ExecuteNonQuery();

                //facilitybookinglog
                mCommand.CommandText = "update facilitybookinglog set patientcode='" + mPatientCode.Trim()
                + "',billinggroupcode='" + mBillingGroupCode.Trim() + "',billinggroup='" + mBillingGroup
                + "',billingsubgroupcode='" + mBillingSubGroupCode.Trim() + "',billingsubgroup='"
                + mBillingSubGroup + "',billinggroupmembershipno='" + mBillingGroupMembershipNo.Trim()
                + "' where booking='" + mPrevBooking.Trim() + "' and patientcode='" + mPatientCode.Trim() + "'";
                int mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit facilitybookinglog
                if (mRecsAffected > 0)
                {
                    DataTable mDtBookingLog = new DataTable("facilitybookinglog");
                    mCommand.CommandText = "select * from facilitybookinglog where booking='" + mPrevBooking
                    + "' and patientcode='" + mPatientCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtBookingLog);

                    mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtBookingLog, "facilitybookinglog",
                    mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                    mCommand.ExecuteNonQuery();
                }
                #endregion

                //get booking that has been done
                mBooking = this.Get_Booking(mCommand, mPatientCode);

                //return
                return mBooking;
            }
            catch (OdbcException ex)
            {
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
            }
        }
        #endregion

        #region Do_Admission
        public AfyaPro_Types.clsBooking Do_Admission(OdbcCommand mCommand, int mAdmissionId, DateTime mTransDate, string mPatientCode, 
            string mWardCode, string mRoomCode, string mBedCode, string mRemarks, 
            double mWeight, double mTemperature, AfyaPro_Types.clsBooking mCurrentBooking, string mUserId)
        {
            String mFunctionName = "Do_Admission";
           
            AfyaPro_Types.clsBooking mBooking = new AfyaPro_Types.clsBooking();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            DataTable mDtBookings = new DataTable("bookings");
            DataTable mDtPatientCorporates = new DataTable("patientcorporates");

            mTransDate = mTransDate.Date;

            try
            {
                string mNewBooking = mCurrentBooking.Booking;
                string mRegistryStatus = AfyaPro_Types.clsEnums.RegistryStatus.New.ToString();
                if (mCurrentBooking.IsNewAttendance == false)
                {
                    mRegistryStatus = AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString();
                }

                DataTable mDtAdmissions = new DataTable("admissions");
                //old code
                //mCommand.CommandText = "select * from ipdadmissionslog where autocode=" + mAdmissionId;
                mCommand.CommandText = "select * from ipdadmissionslog where patientcode= '" + mPatientCode + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtAdmissions);

                if (mDtAdmissions.Rows.Count > 0)
                {
                    //update previous admission
                    List<clsDataField> mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("wardcode", DataTypes.dbstring.ToString(), mWardCode.Trim()));
                    mDataFields.Add(new clsDataField("roomcode", DataTypes.dbstring.ToString(), mRoomCode.Trim()));
                    mDataFields.Add(new clsDataField("bedcode", DataTypes.dbstring.ToString(), mBedCode.Trim() == "" ? null : mBedCode.Trim()));
                    mDataFields.Add(new clsDataField("patientcondition", DataTypes.dbstring.ToString(), mRemarks.Trim()));

                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("ipdadmissionslog", mDataFields, "autocode=" + mAdmissionId);
                    mCommand.ExecuteNonQuery();

                    //delete previous discharge
                    mCommand.CommandText = clsGlobal.Get_DeleteStatement("ipddischargeslog", "admissionid=" + mAdmissionId);
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    List<clsDataField> mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mNewBooking.Trim()));
                    mDataFields.Add(new clsDataField("wardcode", DataTypes.dbstring.ToString(), mWardCode.Trim()));
                    mDataFields.Add(new clsDataField("roomcode", DataTypes.dbstring.ToString(), mRoomCode.Trim()));
                    mDataFields.Add(new clsDataField("bedcode", DataTypes.dbstring.ToString(), mBedCode.Trim() == "" ? null : mBedCode.Trim()));
                    mDataFields.Add(new clsDataField("patientcondition", DataTypes.dbstring.ToString(), mRemarks.Trim()));
                    mDataFields.Add(new clsDataField("registrystatus", DataTypes.dbstring.ToString(), mRegistryStatus));
                    mDataFields.Add(new clsDataField("transcode", DataTypes.dbstring.ToString(), AfyaPro_Types.clsEnums.IPDTransTypes.Admission.ToString()));
                    mDataFields.Add(new clsDataField("patientweight", DataTypes.dbdecimal.ToString(), mWeight));
                    mDataFields.Add(new clsDataField("patienttemperature", DataTypes.dbdecimal.ToString(), mTemperature));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ipdadmissionslog", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                //update bed status
                mCommand.CommandText = "update facilitywardroombeds set lastadmissiondate="
                + clsGlobal.Saving_DateValue(mTransDate) + ",bedstatus='"
                + AfyaPro_Types.clsEnums.IPDBedStatus.Occupied.ToString()
                + "',patients=patients+1 where code='" + mBedCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                //get booking that has been done
                mBooking = this.Get_Booking(mCommand, mPatientCode);
                mBooking.IsNewAttendance = mCurrentBooking.IsNewAttendance;

                //return
                return mBooking;
            }
            catch (OdbcException ex)
            {
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
            }
        }
        #endregion

        #region Do_Discharging
        public AfyaPro_Types.clsBooking Do_Discharging(OdbcCommand mCommand, int mAdmissionId, DateTime mTransDate, string mPatientCode,
            string mFromWardCode, string mFromRoomCode, string mFromBedCode, string mStatusCode, string mRemarks,
            double mWeight, double mTemperature, AfyaPro_Types.clsBooking mAdmissionBooking, string mUserId)
        {
            String mFunctionName = "Do_Discharging";

            AfyaPro_Types.clsBooking mBooking = new AfyaPro_Types.clsBooking();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;

            try
            {
                int mRecsAffected = 0;

                string mCurrentBooking = mAdmissionBooking.Booking;

                DataTable mDtAdmissions = new DataTable("admissions");
                mCommand.CommandText = "select * from ipdadmissionslog where autocode=" + mAdmissionId;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtAdmissions);

                string mRegistryStatus = "";

                if (mDtAdmissions.Rows.Count > 0)
                {
                    mRegistryStatus = mDtAdmissions.Rows[0]["registrystatus"].ToString();
                }

                //get discharge diagnosis
                string mDiagnosisCode = "";
                DataTable mDtDiagnoses = new DataTable("patientdiagnoses");
                mCommand.CommandText = "select diagnosiscode from dxtpatientdiagnoseslog where patientcode='" + mPatientCode.Trim()
                    + "' and booking='" + mCurrentBooking + "' and ipddiagnosistype<>'Admission' and isprimary=1 order by autocode desc limit 1";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtDiagnoses);
                if (mDtDiagnoses.Rows.Count > 0)
                {
                    mDiagnosisCode = mDtDiagnoses.Rows[0]["diagnosiscode"].ToString().Trim();
                }

                //ipddischargeslog
                List<clsDataField> mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                mDataFields.Add(new clsDataField("admissionid", DataTypes.dbnumber.ToString(), mAdmissionId));
                mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mCurrentBooking.Trim()));
                mDataFields.Add(new clsDataField("wardcode", DataTypes.dbstring.ToString(), mFromWardCode.Trim()));
                mDataFields.Add(new clsDataField("roomcode", DataTypes.dbstring.ToString(), mFromRoomCode.Trim()));
                mDataFields.Add(new clsDataField("bedcode", DataTypes.dbstring.ToString(), mFromBedCode.Trim() == "" ? null : mFromBedCode.Trim()));
                mDataFields.Add(new clsDataField("patientcondition", DataTypes.dbstring.ToString(), mRemarks.Trim()));
                mDataFields.Add(new clsDataField("dischargestatuscode", DataTypes.dbstring.ToString(), mStatusCode.Trim()));
                mDataFields.Add(new clsDataField("dischargeremarks", DataTypes.dbstring.ToString(), mRemarks.Trim()));
                mDataFields.Add(new clsDataField("registrystatus", DataTypes.dbstring.ToString(), mRegistryStatus));
                mDataFields.Add(new clsDataField("diagnosiscode", DataTypes.dbstring.ToString(), mDiagnosisCode.Trim() == "" ? null : mDiagnosisCode));
                mDataFields.Add(new clsDataField("transcode", DataTypes.dbstring.ToString(), AfyaPro_Types.clsEnums.IPDTransTypes.Discharge.ToString()));
                mDataFields.Add(new clsDataField("patientweight", DataTypes.dbdecimal.ToString(), mWeight));
                mDataFields.Add(new clsDataField("patienttemperature", DataTypes.dbdecimal.ToString(), mTemperature));
                mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                mCommand.CommandText = clsGlobal.Get_InsertStatement("ipddischargeslog", mDataFields);
                mCommand.ExecuteNonQuery();

                //facilitybookings
                mCommand.CommandText = "update facilitybookings set department='"
                + AfyaPro_Types.clsEnums.PatientCategories.OPD.ToString() + "',ipdstop="
                + clsGlobal.Saving_DateValue(mTransDate) + " where autocode='" + mCurrentBooking
                + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                //facilitybookinglog
                mCommand.CommandText = "update facilitybookinglog set ipdstop="
                + clsGlobal.Saving_DateValue(mTransDate) + ",ipddischargestatus='" + mStatusCode
                + "' where booking='" + mCurrentBooking + "' and patientcode='" + mPatientCode.Trim() + "'";
                mRecsAffected = mCommand.ExecuteNonQuery();

                //update bed status
                mCommand.CommandText = "update facilitywardroombeds set patients=patients-1 where code='" + mFromBedCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                mCommand.CommandText = "update facilitywardroombeds set bedstatus='"
                + AfyaPro_Types.clsEnums.IPDBedStatus.Vacant.ToString()
                + "' where code='" + mFromBedCode.Trim() + "' and patients=0";
                mCommand.ExecuteNonQuery();

                //get booking that has been done
                mBooking = this.Get_Booking(mCommand, mPatientCode);

                //return
                return mBooking;
            }
            catch (OdbcException ex)
            {
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
            }
        }
        #endregion

        #region Do_Transfering
        public AfyaPro_Types.clsBooking Do_Transfering(OdbcCommand mCommand, int mAdmissionId, DateTime mTransDate, string mPatientCode,
            string mFromWardCode, string mFromRoomCode, string mFromBedCode, string mToWardCode, string mToRoomCode, 
            string mToBedCode, string mPatientCondition,
            double mWeight, double mTemperature, AfyaPro_Types.clsBooking mAdmissionBooking, string mUserId)
        {
            String mFunctionName = "Do_Transfering";

            AfyaPro_Types.clsBooking mBooking = new AfyaPro_Types.clsBooking();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;

            if (mFromWardCode.Trim().ToLower() == mToWardCode.Trim().ToLower()
                && mFromRoomCode.Trim().ToLower() == mToRoomCode.Trim().ToLower())
            {
                mBooking.Exe_Result = 0;
                mBooking.Exe_Message = "Transfering within the same ward and room is not supported";
                return mBooking;
            }

            try
            {
                string mCurrentBooking = mAdmissionBooking.Booking;

                DataTable mDtAdmissions = new DataTable("admissions");
                mCommand.CommandText = "select * from ipdadmissionslog where autocode=" + mAdmissionId;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtAdmissions);

                string mRegistryStatus = "";

                if (mDtAdmissions.Rows.Count > 0)
                {
                    mRegistryStatus = mDtAdmissions.Rows[0]["registrystatus"].ToString();
                }

                List<clsDataField> mDataFields;

                //get discharge diagnosis
                string mDiagnosisCode = "";
                DataTable mDtDiagnoses = new DataTable("patientdiagnoses");
                mCommand.CommandText = "select diagnosiscode from dxtpatientdiagnoseslog where patientcode='" + mPatientCode.Trim()
                    + "' and booking='" + mCurrentBooking + "' and ipddiagnosistype<>'Admission' and isprimary=1 order by autocode desc limit 1";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtDiagnoses);
                if (mDtDiagnoses.Rows.Count > 0)
                {
                    mDiagnosisCode = mDtDiagnoses.Rows[0]["diagnosiscode"].ToString().Trim();
                }

                //ipdtransferslog
                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                mDataFields.Add(new clsDataField("transferdate", DataTypes.dbdatetime.ToString(), mTransDate));
                mDataFields.Add(new clsDataField("admissionid", DataTypes.dbnumber.ToString(), mAdmissionId));
                mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mCurrentBooking.Trim()));
                mDataFields.Add(new clsDataField("wardfromcode", DataTypes.dbstring.ToString(), mFromWardCode.Trim()));
                mDataFields.Add(new clsDataField("roomfromcode", DataTypes.dbstring.ToString(), mFromRoomCode.Trim()));
                mDataFields.Add(new clsDataField("bedfromcode", DataTypes.dbstring.ToString(), mFromBedCode.Trim() == "" ? null : mFromBedCode.Trim()));
                mDataFields.Add(new clsDataField("wardtocode", DataTypes.dbstring.ToString(), mToWardCode.Trim()));
                mDataFields.Add(new clsDataField("roomtocode", DataTypes.dbstring.ToString(), mToRoomCode.Trim()));
                mDataFields.Add(new clsDataField("bedtocode", DataTypes.dbstring.ToString(), mToBedCode.Trim() == "" ? null : mToBedCode.Trim()));
                mDataFields.Add(new clsDataField("patientcondition", DataTypes.dbstring.ToString(), mPatientCondition.Trim()));
                mDataFields.Add(new clsDataField("registrystatus", DataTypes.dbstring.ToString(), mRegistryStatus));
                mDataFields.Add(new clsDataField("patientweight", DataTypes.dbdecimal.ToString(), mWeight));
                mDataFields.Add(new clsDataField("patienttemperature", DataTypes.dbdecimal.ToString(), mTemperature));
                mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                mCommand.CommandText = clsGlobal.Get_InsertStatement("ipdtransferslog", mDataFields);
                mCommand.ExecuteNonQuery();

                //ipddischargeslog
                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                mDataFields.Add(new clsDataField("admissionid", DataTypes.dbnumber.ToString(), mAdmissionId));
                mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mCurrentBooking.Trim()));
                mDataFields.Add(new clsDataField("wardcode", DataTypes.dbstring.ToString(), mFromWardCode.Trim()));
                mDataFields.Add(new clsDataField("roomcode", DataTypes.dbstring.ToString(), mFromRoomCode.Trim()));
                mDataFields.Add(new clsDataField("bedcode", DataTypes.dbstring.ToString(), mFromBedCode.Trim() == "" ? null : mFromBedCode.Trim()));
                mDataFields.Add(new clsDataField("patientcondition", DataTypes.dbstring.ToString(), mPatientCondition.Trim()));
                mDataFields.Add(new clsDataField("dischargestatuscode", DataTypes.dbstring.ToString(), null));
                mDataFields.Add(new clsDataField("dischargeremarks", DataTypes.dbstring.ToString(), ""));
                mDataFields.Add(new clsDataField("registrystatus", DataTypes.dbstring.ToString(), mRegistryStatus));
                mDataFields.Add(new clsDataField("transcode", DataTypes.dbstring.ToString(), AfyaPro_Types.clsEnums.IPDTransTypes.Transfer.ToString()));
                mDataFields.Add(new clsDataField("diagnosiscode", DataTypes.dbstring.ToString(), mDiagnosisCode.Trim() == "" ? null : mDiagnosisCode));
                mDataFields.Add(new clsDataField("patientweight", DataTypes.dbdecimal.ToString(), mWeight));
                mDataFields.Add(new clsDataField("patienttemperature", DataTypes.dbdecimal.ToString(), mTemperature));
                mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                mCommand.CommandText = clsGlobal.Get_InsertStatement("ipddischargeslog", mDataFields);
                mCommand.ExecuteNonQuery();

                //update bed status
                mCommand.CommandText = "update facilitywardroombeds set patients=patients-1 where code='" + mFromBedCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                mCommand.CommandText = "update facilitywardroombeds set bedstatus='"
                + AfyaPro_Types.clsEnums.IPDBedStatus.Vacant.ToString()
                + "' where code='" + mFromBedCode.Trim() + "' and patients=0";
                mCommand.ExecuteNonQuery();

                #region ipdadmissionslog - destination ward/room/bed

                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mCurrentBooking.Trim()));
                mDataFields.Add(new clsDataField("wardcode", DataTypes.dbstring.ToString(), mToWardCode.Trim()));
                mDataFields.Add(new clsDataField("roomcode", DataTypes.dbstring.ToString(), mToRoomCode.Trim()));
                mDataFields.Add(new clsDataField("bedcode", DataTypes.dbstring.ToString(), mToBedCode.Trim() == "" ? null : mToBedCode.Trim()));
                mDataFields.Add(new clsDataField("patientcondition", DataTypes.dbstring.ToString(), mPatientCondition.Trim()));
                mDataFields.Add(new clsDataField("registrystatus", DataTypes.dbstring.ToString(), mRegistryStatus));
                mDataFields.Add(new clsDataField("transcode", DataTypes.dbstring.ToString(), AfyaPro_Types.clsEnums.IPDTransTypes.Transfer.ToString()));
                mDataFields.Add(new clsDataField("patientweight", DataTypes.dbdecimal.ToString(), mWeight));
                mDataFields.Add(new clsDataField("patienttemperature", DataTypes.dbdecimal.ToString(), mTemperature));
                mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                mCommand.CommandText = clsGlobal.Get_InsertStatement("ipdadmissionslog", mDataFields);
                mCommand.ExecuteNonQuery();

                //update bed status
                mCommand.CommandText = "update facilitywardroombeds set bedstatus='"
                + AfyaPro_Types.clsEnums.IPDBedStatus.Occupied.ToString()
                + "',patients=patients+1 where code='" + mToBedCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                #endregion

                //get booking that has been done
                mBooking = this.Get_Booking(mCommand, mPatientCode);

                //return
                return mBooking;
            }
            catch (OdbcException ex)
            {
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
            }
        }
        #endregion

        #region Add_Diagnoses
        public AfyaPro_Types.clsResult Add_Diagnoses(
            OdbcCommand mCommand, 
            DateTime mTransDate, 
            string mPatientCode,
            string mDiagnosisCode,
            string mEpisodeCode,
            int mIsPrimary,
            string mIPDDiagnosisType,
            string mHistory,
            string mExamination,
            string mInvestigation, 
            string mTreatments,
            string mReferalDescription,
            string mDoctorCode, 
            double mWeight, 
            double mTemperature, 
            AfyaPro_Types.clsBooking mDiagnosisBooking, 
            string mUserId)
        {
            String mFunctionName = "Add_Diagnoses";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            DataTable mDtBookings = new DataTable("bookings");
            DataTable mDtPatientCorporates = new DataTable("patientcorporates");

            mTransDate = mTransDate.Date;

            try
            {
                string mDepartment = mDiagnosisBooking.Department;
                string mNewBooking = mDiagnosisBooking.Booking;
                string mBillingGroupCode = mDiagnosisBooking.BillingGroupCode;
                string mBillingSubGroupCode = mDiagnosisBooking.BillingSubGroupCode;

                string mWardCode = "";
                string mRoomCode = "";
                string mBedCode = "";

                clsRegistrations mRegistrations = new clsRegistrations();
                AfyaPro_Types.clsAdmission mCurrentAdmission = mRegistrations.Get_Admission(mNewBooking, mPatientCode.Trim());
                if (mCurrentAdmission != null)
                {
                    if (mCurrentAdmission.IsAdmitted == true)
                    {
                        mWardCode = mCurrentAdmission.WardCode;
                        mRoomCode = mCurrentAdmission.RoomCode;
                        mBedCode = mCurrentAdmission.BedCode;
                    }
                }

                string mIndicatorCode = "";

                DataTable mDtEpisodes = new DataTable("dxtpatientdiagnoses");
                mCommand.CommandText = "select * from dxtpatientdiagnoses where patientcode='"
                + mPatientCode.Trim() + "' and episodecode='" + mEpisodeCode + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtEpisodes);

                List<clsDataField> mDataFields;

                if (mDtEpisodes.Rows.Count == 0)
                {
                    #region find indicator code

                    DataTable mDtIndicators = new DataTable("indicators");
                    mCommand.CommandText = "select * from dxtindicatordiagnoses where diagnosiscode='" + mDiagnosisCode + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtIndicators);

                    if (mDtIndicators.Rows.Count > 0)
                    {
                        mIndicatorCode = mDtIndicators.Rows[0]["indicatorcode"].ToString().Trim();
                    }

                    #endregion

                    #region generate unique key for this episode

                    clsAutoCodes objAutoCodes = new clsAutoCodes();
                    AfyaPro_Types.clsCode mObjCode = objAutoCodes.Next_Code(mCommand,
                        Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.dxincidencekey), "dxtpatientdiagnoses", "episodecode");

                    if (mObjCode.Exe_Result == -1)
                    {
                        mResult.Exe_Result = mObjCode.Exe_Result;
                        mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                        return mResult;
                    }
                    mEpisodeCode = mObjCode.GeneratedCode;

                    #endregion

                    #region dxtpatientdiagnoses

                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("episodecode", DataTypes.dbstring.ToString(), mEpisodeCode.Trim()));
                    mDataFields.Add(new clsDataField("isprimary", DataTypes.dbnumber.ToString(), mIsPrimary));
                    mDataFields.Add(new clsDataField("ipddiagnosistype", DataTypes.dbstring.ToString(), mIPDDiagnosisType.Trim()));
                    mDataFields.Add(new clsDataField("diagnosiscode", DataTypes.dbstring.ToString(), mDiagnosisCode.Trim() == "" ? null : mDiagnosisCode.Trim()));
                    mDataFields.Add(new clsDataField("indicatorcode", DataTypes.dbstring.ToString(), mIndicatorCode.Trim()));
                    mDataFields.Add(new clsDataField("firstencounterdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("firstencounterbooking", DataTypes.dbstring.ToString(), mNewBooking.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("dxtpatientdiagnoses", mDataFields);
                    mCommand.ExecuteNonQuery();

                    #endregion

                    #region dxtpatientdiagnoseslog

                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mNewBooking.Trim()));
                    mDataFields.Add(new clsDataField("episodecode", DataTypes.dbstring.ToString(), mEpisodeCode.Trim()));
                    mDataFields.Add(new clsDataField("isprimary", DataTypes.dbnumber.ToString(), mIsPrimary));
                    mDataFields.Add(new clsDataField("ipddiagnosistype", DataTypes.dbstring.ToString(), mIPDDiagnosisType.Trim()));
                    mDataFields.Add(new clsDataField("diagnosiscode", DataTypes.dbstring.ToString(), mDiagnosisCode.Trim() == "" ? null : mDiagnosisCode.Trim()));
                    mDataFields.Add(new clsDataField("indicatorcode", DataTypes.dbstring.ToString(), mIndicatorCode.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("billinggroupcode", DataTypes.dbstring.ToString(), mBillingGroupCode.Trim()));
                    mDataFields.Add(new clsDataField("billingsubgroupcode", DataTypes.dbstring.ToString(), mBillingSubGroupCode.Trim()));
                    mDataFields.Add(new clsDataField("history", DataTypes.dbstring.ToString(), mHistory.Trim()));
                    mDataFields.Add(new clsDataField("examination", DataTypes.dbstring.ToString(), mExamination.Trim()));
                    mDataFields.Add(new clsDataField("investigation", DataTypes.dbstring.ToString(), mInvestigation.Trim()));
                    mDataFields.Add(new clsDataField("treatments", DataTypes.dbstring.ToString(), mTreatments.Trim()));
                    mDataFields.Add(new clsDataField("doctorcode", DataTypes.dbstring.ToString(), mDoctorCode.Trim()));
                    mDataFields.Add(new clsDataField("referaldescription", DataTypes.dbstring.ToString(), mReferalDescription.Trim()));
                    mDataFields.Add(new clsDataField("department", DataTypes.dbstring.ToString(), mDepartment.Trim()));
                    mDataFields.Add(new clsDataField("patientweight", DataTypes.dbdecimal.ToString(), mWeight));
                    mDataFields.Add(new clsDataField("patienttemperature", DataTypes.dbdecimal.ToString(), mTemperature));
                    mDataFields.Add(new clsDataField("wardcode", DataTypes.dbstring.ToString(), mWardCode.Trim()));
                    mDataFields.Add(new clsDataField("roomcode", DataTypes.dbstring.ToString(), mRoomCode.Trim()));
                    mDataFields.Add(new clsDataField("bedcode", DataTypes.dbstring.ToString(), mBedCode.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("dxtpatientdiagnoseslog", mDataFields);
                    mCommand.ExecuteNonQuery();

                    #endregion

                    #region add diagnosis to quick list

                    DataTable mDtQuickList = new DataTable("quicklist");
                    mCommand.CommandText = "select code from dxtdiagnoses where code='" + mDiagnosisCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtQuickList);

                    if (mDtQuickList.Rows.Count == 0)
                    {
                        DataTable mDtICD = new DataTable("icd");
                        mCommand.CommandText = "select * from dxticddiagnoses where code='" + mDiagnosisCode.Trim() + "'";
                        mDataAdapter.SelectCommand = mCommand;
                        mDataAdapter.Fill(mDtICD);

                        if (mDtICD.Rows.Count > 0)
                        {
                            mCommand.CommandText = "insert into dxtdiagnoses(code,description,groupcode,subgroupcode) values('"
                            + mDtICD.Rows[0]["code"] + "','" + mDtICD.Rows[0]["description"] + "','"
                            + mDtICD.Rows[0]["groupcode"] + "','" + mDtICD.Rows[0]["subgroupcode"] + "')";
                            mCommand.ExecuteNonQuery();
                        }
                    }

                    #endregion

                    #region update first and last encounter

                    DataTable mDtFirstEncounters = new DataTable("firstencounters");
                    mCommand.CommandText = "SELECT booking,MIN(transdate) transdate FROM dxtpatientdiagnoseslog WHERE patientcode='" 
                        + mPatientCode.Trim() + "' AND episodecode='" + mEpisodeCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtFirstEncounters);

                    DataTable mDtLastEncounters = new DataTable("firstencounters");
                    mCommand.CommandText = "SELECT booking,MAX(transdate) transdate FROM dxtpatientdiagnoseslog WHERE patientcode='"
                        + mPatientCode.Trim() + "' AND episodecode='" + mEpisodeCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtLastEncounters);

                    mDataFields = new List<clsDataField>();

                    if (mDtFirstEncounters.Rows.Count > 0)
                    {
                        mDataFields.Add(new clsDataField("firstencounterdate", DataTypes.dbdatetime.ToString(), mDtFirstEncounters.Rows[0]["transdate"]));
                        mDataFields.Add(new clsDataField("firstencounterbooking", DataTypes.dbstring.ToString(), mDtFirstEncounters.Rows[0]["booking"]));
                    }
                    if (mDtLastEncounters.Rows.Count > 0)
                    {
                        mDataFields.Add(new clsDataField("lastencounterdate", DataTypes.dbdatetime.ToString(), mDtLastEncounters.Rows[0]["transdate"]));
                        mDataFields.Add(new clsDataField("lastencounterbooking", DataTypes.dbstring.ToString(), mDtLastEncounters.Rows[0]["booking"]));
                    }

                    if (mDtFirstEncounters.Rows.Count > 0
                        || mDtLastEncounters.Rows.Count > 0)
                    {
                        mCommand.CommandText = clsGlobal.Get_UpdateStatement("dxtpatientdiagnoses", mDataFields,
                            "patientcode='" + mPatientCode.Trim() + "' and episodecode='" + mEpisodeCode.Trim() + "'");
                        mCommand.ExecuteNonQuery();
                    }

                    #endregion
                }
                else
                {
                    #region dxtpatientdiagnoses

                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("isprimary", DataTypes.dbnumber.ToString(), mIsPrimary));
                    mDataFields.Add(new clsDataField("ipddiagnosistype", DataTypes.dbstring.ToString(), mIPDDiagnosisType.Trim()));
                    mDataFields.Add(new clsDataField("diagnosiscode", DataTypes.dbstring.ToString(), mDiagnosisCode.Trim() == "" ? null : mDiagnosisCode.Trim()));
                    mDataFields.Add(new clsDataField("indicatorcode", DataTypes.dbstring.ToString(), mIndicatorCode.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("dxtpatientdiagnoses", mDataFields,
                        "patientcode='" + mPatientCode.Trim() + "' and episodecode='" + mEpisodeCode + "'");
                    mCommand.ExecuteNonQuery();

                    #endregion

                    #region dxtpatientdiagnoseslog

                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mNewBooking.Trim()));
                    mDataFields.Add(new clsDataField("episodecode", DataTypes.dbstring.ToString(), mEpisodeCode.Trim()));
                    mDataFields.Add(new clsDataField("isprimary", DataTypes.dbnumber.ToString(), mIsPrimary));
                    mDataFields.Add(new clsDataField("ipddiagnosistype", DataTypes.dbstring.ToString(), mIPDDiagnosisType.Trim()));
                    mDataFields.Add(new clsDataField("diagnosiscode", DataTypes.dbstring.ToString(), mDiagnosisCode.Trim() == "" ? null : mDiagnosisCode.Trim()));
                    mDataFields.Add(new clsDataField("indicatorcode", DataTypes.dbstring.ToString(), mIndicatorCode.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("billinggroupcode", DataTypes.dbstring.ToString(), mBillingGroupCode.Trim()));
                    mDataFields.Add(new clsDataField("billingsubgroupcode", DataTypes.dbstring.ToString(), mBillingSubGroupCode.Trim()));
                    mDataFields.Add(new clsDataField("history", DataTypes.dbstring.ToString(), mHistory.Trim()));
                    mDataFields.Add(new clsDataField("examination", DataTypes.dbstring.ToString(), mExamination.Trim()));
                    mDataFields.Add(new clsDataField("investigation", DataTypes.dbstring.ToString(), mInvestigation.Trim()));
                    mDataFields.Add(new clsDataField("treatments", DataTypes.dbstring.ToString(), mTreatments.Trim()));
                    mDataFields.Add(new clsDataField("doctorcode", DataTypes.dbstring.ToString(), mDoctorCode.Trim()));
                    mDataFields.Add(new clsDataField("referaldescription", DataTypes.dbstring.ToString(), mReferalDescription.Trim()));
                    mDataFields.Add(new clsDataField("department", DataTypes.dbstring.ToString(), mDepartment.Trim()));
                    mDataFields.Add(new clsDataField("patientweight", DataTypes.dbdecimal.ToString(), mWeight));
                    mDataFields.Add(new clsDataField("patienttemperature", DataTypes.dbdecimal.ToString(), mTemperature));
                    mDataFields.Add(new clsDataField("wardcode", DataTypes.dbstring.ToString(), mWardCode.Trim()));
                    mDataFields.Add(new clsDataField("roomcode", DataTypes.dbstring.ToString(), mRoomCode.Trim()));
                    mDataFields.Add(new clsDataField("bedcode", DataTypes.dbstring.ToString(), mBedCode.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("dxtpatientdiagnoseslog", mDataFields);
                    mCommand.ExecuteNonQuery();

                    #endregion

                    #region update first and last encounter

                    DataTable mDtFirstEncounters = new DataTable("firstencounters");
                    mCommand.CommandText = "SELECT booking,MIN(transdate) transdate FROM dxtpatientdiagnoseslog WHERE patientcode='"
                        + mPatientCode.Trim() + "' AND episodecode='" + mEpisodeCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtFirstEncounters);

                    DataTable mDtLastEncounters = new DataTable("firstencounters");
                    mCommand.CommandText = "SELECT booking,MAX(transdate) transdate FROM dxtpatientdiagnoseslog WHERE patientcode='"
                        + mPatientCode.Trim() + "' AND episodecode='" + mEpisodeCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtLastEncounters);

                    mDataFields = new List<clsDataField>();

                    if (mDtFirstEncounters.Rows.Count > 0)
                    {
                        mDataFields.Add(new clsDataField("firstencounterdate", DataTypes.dbdatetime.ToString(), mDtFirstEncounters.Rows[0]["transdate"]));
                        mDataFields.Add(new clsDataField("firstencounterbooking", DataTypes.dbstring.ToString(), mDtFirstEncounters.Rows[0]["booking"]));
                    }
                    if (mDtLastEncounters.Rows.Count > 0)
                    {
                        mDataFields.Add(new clsDataField("lastencounterdate", DataTypes.dbdatetime.ToString(), mDtLastEncounters.Rows[0]["transdate"]));
                        mDataFields.Add(new clsDataField("lastencounterbooking", DataTypes.dbstring.ToString(), mDtLastEncounters.Rows[0]["booking"]));
                    }

                    if (mDtFirstEncounters.Rows.Count > 0
                        || mDtLastEncounters.Rows.Count > 0)
                    {
                        mCommand.CommandText = clsGlobal.Get_UpdateStatement("dxtpatientdiagnoses", mDataFields,
                            "patientcode='" + mPatientCode.Trim() + "' and episodecode='" + mEpisodeCode.Trim() + "'");
                        mCommand.ExecuteNonQuery();
                    }

                    #endregion
                }

                //return
                return mResult;
            }
            catch (OdbcException ex)
            {
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }
        }
        #endregion

        #region Edit_Diagnoses
        public AfyaPro_Types.clsResult Edit_Diagnoses(
            OdbcCommand mCommand,
            int mAutoCode,
            DateTime mTransDate,
            string mDiagnosisCode,
            int mIsPrimary,
            string mHistory,
            string mExamination,
            string mInvestigation,
            string mTreatments,
            string mReferalDescription,
            string mDoctorCode,
            string mUserId)
        {
            String mFunctionName = "Edit_Diagnoses";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            DataTable mDtBookings = new DataTable("bookings");
            DataTable mDtPatientCorporates = new DataTable("patientcorporates");

            mTransDate = mTransDate.Date;

            try
            {
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

                //find indicatorcode
                string mIndicatorCode = "";

                #region find indicator code

                DataTable mDtIndicators = new DataTable("indicators");
                mCommand.CommandText = "select * from dxtindicatordiagnoses where diagnosiscode='" + mDiagnosisCode + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtIndicators);

                if (mDtIndicators.Rows.Count > 0)
                {
                    mIndicatorCode = mDtIndicators.Rows[0]["indicatorcode"].ToString().Trim();
                }

                #endregion

                List<clsDataField> mDataFields;

                #region dxtpatientdiagnoses

                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("isprimary", DataTypes.dbnumber.ToString(), mIsPrimary));
                mDataFields.Add(new clsDataField("diagnosiscode", DataTypes.dbstring.ToString(), mDiagnosisCode.Trim() == "" ? null : mDiagnosisCode.Trim()));
                mDataFields.Add(new clsDataField("indicatorcode", DataTypes.dbstring.ToString(), mIndicatorCode.Trim()));
                mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("dxtpatientdiagnoses", mDataFields,
                    "patientcode='" + mPatientCode.Trim() + "' and episodecode='" + mEpisodeCode + "'");
                mCommand.ExecuteNonQuery();

                #endregion

                #region dxtpatientdiagnoseslog

                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                mDataFields.Add(new clsDataField("history", DataTypes.dbstring.ToString(), mHistory.Trim()));
                mDataFields.Add(new clsDataField("examination", DataTypes.dbstring.ToString(), mExamination.Trim()));
                mDataFields.Add(new clsDataField("investigation", DataTypes.dbstring.ToString(), mInvestigation.Trim()));
                mDataFields.Add(new clsDataField("treatments", DataTypes.dbstring.ToString(), mTreatments.Trim()));
                mDataFields.Add(new clsDataField("doctorcode", DataTypes.dbstring.ToString(), mDoctorCode.Trim()));
                mDataFields.Add(new clsDataField("referaldescription", DataTypes.dbstring.ToString(), mReferalDescription.Trim()));
                mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("dxtpatientdiagnoseslog", mDataFields, "autocode=" + mAutoCode);
                mCommand.ExecuteNonQuery();

                #endregion

                #region update first and last encounter

                DataTable mDtFirstEncounters = new DataTable("firstencounters");
                mCommand.CommandText = "SELECT booking,MIN(transdate) transdate FROM dxtpatientdiagnoseslog WHERE patientcode='"
                    + mPatientCode.Trim() + "' AND episodecode='" + mEpisodeCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFirstEncounters);

                DataTable mDtLastEncounters = new DataTable("firstencounters");
                mCommand.CommandText = "SELECT booking,MAX(transdate) transdate FROM dxtpatientdiagnoseslog WHERE patientcode='"
                    + mPatientCode.Trim() + "' AND episodecode='" + mEpisodeCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtLastEncounters);

                mDataFields = new List<clsDataField>();

                if (mDtFirstEncounters.Rows.Count > 0)
                {
                    mDataFields.Add(new clsDataField("firstencounterdate", DataTypes.dbdatetime.ToString(), mDtFirstEncounters.Rows[0]["transdate"]));
                    mDataFields.Add(new clsDataField("firstencounterbooking", DataTypes.dbstring.ToString(), mDtFirstEncounters.Rows[0]["booking"]));
                }
                if (mDtLastEncounters.Rows.Count > 0)
                {
                    mDataFields.Add(new clsDataField("lastencounterdate", DataTypes.dbdatetime.ToString(), mDtLastEncounters.Rows[0]["transdate"]));
                    mDataFields.Add(new clsDataField("lastencounterbooking", DataTypes.dbstring.ToString(), mDtLastEncounters.Rows[0]["booking"]));
                }

                if (mDtFirstEncounters.Rows.Count > 0
                    || mDtLastEncounters.Rows.Count > 0)
                {
                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("dxtpatientdiagnoses", mDataFields,
                        "patientcode='" + mPatientCode.Trim() + "' and episodecode='" + mEpisodeCode.Trim() + "'");
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                //return
                return mResult;
            }
            catch (OdbcException ex)
            {
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }
        }
        #endregion

        #region Do_Diagnoses
        //public AfyaPro_Types.clsResult Do_Diagnoses(OdbcCommand mCommand, DateTime mTransDate, string mPatientCode,
        //    string mHistory, string mExamination, string mInvestigation, DataTable mDtDiagnoses, string mTreatments, string mDoctorCode,
        //    string mDoctorDescription, Int16 mDeathStatus,
        //    double mWeight, double mTemperature, AfyaPro_Types.clsBooking mDiagnosisBooking, string mIPDDiagnosisType, string mUserId)
        //{
        //    String mFunctionName = "Do_Diagnoses";

        //    AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
        //    OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

        //    DataTable mDtBookings = new DataTable("bookings");
        //    DataTable mDtPatientCorporates = new DataTable("patientcorporates");

        //    mTransDate = mTransDate.Date;

        //    try
        //    {
        //        int mYearPart = mTransDate.Year;
        //        int mMonthPart = mTransDate.Month;

        //        string mDepartment = mDiagnosisBooking.Department;
        //        string mNewBooking = mDiagnosisBooking.Booking;
        //        string mBillingGroupCode = mDiagnosisBooking.BillingGroupCode;
        //        string mBillingSubGroupCode = mDiagnosisBooking.BillingSubGroupCode;

        //        string mWardCode = "";
        //        string mRoomCode = "";
        //        string mBedCode = "";

        //        clsRegistrations mRegistrations = new clsRegistrations();
        //        AfyaPro_Types.clsAdmission mCurrentAdmission = mRegistrations.Get_Admission(mNewBooking, mPatientCode.Trim());
        //        if (mCurrentAdmission != null)
        //        {
        //            if (mCurrentAdmission.IsAdmitted == true)
        //            {
        //                mWardCode = mCurrentAdmission.WardCode;
        //                mRoomCode = mCurrentAdmission.RoomCode;
        //                mBedCode = mCurrentAdmission.BedCode;
        //            }
        //        }

        //        foreach (DataRow mDataRow in mDtDiagnoses.Rows)
        //        {
        //            string mDxCode = mDataRow["diagnosiscode"].ToString().Trim();
        //            string mDxDescription = mDataRow["diagnosisdescription"].ToString().Trim();
        //            Int16 mIsPrimary = Convert.ToInt16(mDataRow["isprimary"]);
        //            string mReasonForEncounter = mDataRow["reasonforencounter"].ToString().Trim();
        //            string mEpisodeCode = mDataRow["episodecode"].ToString().Trim();
        //            string mIndicatorCode = "";

        //            DataTable mDtEpisodes = new DataTable("dxtpatientdiagnoseslog");
        //            mCommand.CommandText = "select * from dxtpatientdiagnoseslog where patientcode='"
        //            + mPatientCode.Trim() + "' and diagnosiscode='" + mDxCode + "' and episodecode='" + mEpisodeCode + "'";
        //            mDataAdapter.SelectCommand = mCommand;
        //            mDataAdapter.Fill(mDtEpisodes);

        //            Int16 mCurrentDeathStatus = 0;
        //            if (mIsPrimary == 1)
        //            {
        //                mCurrentDeathStatus = mDeathStatus;
        //            }

        //            if (mDtEpisodes.Rows.Count == 0)
        //            {
        //                #region find indicator code

        //                DataTable mDtIndicators = new DataTable("indicators");
        //                mCommand.CommandText = "select * from dxtindicatordiagnoses where diagnosiscode='" + mDxCode + "'";
        //                mDataAdapter.SelectCommand = mCommand;
        //                mDataAdapter.Fill(mDtIndicators);

        //                if (mDtIndicators.Rows.Count > 0)
        //                {
        //                    mIndicatorCode = mDtIndicators.Rows[0]["indicatorcode"].ToString().Trim();
        //                }

        //                #endregion

        //                #region generate unique key for this episode

        //                clsAutoCodes objAutoCodes = new clsAutoCodes();
        //                AfyaPro_Types.clsCode mObjCode = objAutoCodes.Next_Code(mCommand,
        //                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.dxincidencekey), "dxtpatientdiagnoseslog", "episodecode");

        //                if (mObjCode.Exe_Result == -1)
        //                {
        //                    mResult.Exe_Result = mObjCode.Exe_Result;
        //                    mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
        //                    return mResult;
        //                }
        //                mEpisodeCode = mObjCode.GeneratedCode;

        //                #endregion

        //                #region dxtpatientdiagnoseslog

        //                mCommand.CommandText = "insert into dxtpatientdiagnoseslog("
        //                    + "sysdate,"
        //                    + "transdate,"
        //                    + "booking,"
        //                    + "patientcode,"
        //                    + "billinggroupcode,"
        //                    + "billingsubgroupcode,"
        //                    + "episodecode,"
        //                    + "isprimary,"
        //                    + "reasonforencounter,"
        //                    + "ipddiagnosistype,"
        //                    + "history,"
        //                    + "examination,"
        //                    + "investigation,"
        //                    + "diagnosiscode,"
        //                    + "indicatorcode,"
        //                    + "diagnosisdescription,"
        //                    + "firstencounterdate,"
        //                    + "lastencounterdate,"
        //                    + "doctorcode,"
        //                    + "doctordescription,"
        //                    + "deathstatus,"
        //                    + "department,"
        //                    + "treatments,"
        //                    + "yearpart,"
        //                    + "monthpart,"
        //                    + "userid,"
        //                    + "wardcode,"
        //                    + "roomcode,"
        //                    + "bedcode,"
        //                    + "patientweight,"
        //                    + "patienttemperature"
        //                    + ") values("
        //                    + clsGlobal.Saving_DateValue(DateTime.Now) + ","
        //                    + clsGlobal.Saving_DateValue(mTransDate) + ",'"
        //                    + mNewBooking + "','"
        //                    + mPatientCode.Trim() + "','"
        //                    + mBillingGroupCode.Trim() + "','"
        //                    + mBillingSubGroupCode.Trim() + "','"
        //                    + mEpisodeCode.Trim() + "',"
        //                    + mIsPrimary + ",'"
        //                    + mReasonForEncounter + "','"
        //                    + mIPDDiagnosisType.Trim() + "','"
        //                    + mHistory.Trim() + "','"
        //                    + mExamination.Trim() + "','"
        //                    + mInvestigation.Trim() + "','"
        //                    + mDxCode.Trim() + "','"
        //                    + mIndicatorCode.Trim() + "','"
        //                    + mDxDescription.Trim() + "',"
        //                    + clsGlobal.Saving_DateValue(mTransDate) + ","
        //                    + clsGlobal.Saving_DateValue(mTransDate) + ",'"
        //                    + mDoctorCode.Trim() + "','"
        //                    + mDoctorDescription.Trim() + "',"
        //                    + mCurrentDeathStatus + ",'"
        //                    + mDepartment + "','"
        //                    + mTreatments.Trim() + "',"
        //                    + mYearPart + ","
        //                    + mMonthPart + ",'"
        //                    + mUserId.Trim() + "','"
        //                    + mWardCode + "','"
        //                    + mRoomCode + "','"
        //                    + mBedCode + "',"
        //                    + mWeight + ","
        //                    + mTemperature + ")";

        //                mCommand.ExecuteNonQuery();

        //                #endregion

        //                #region dxtpatientdiagnosesfollowup

        //                mCommand.CommandText = "insert into dxtpatientdiagnosesfollowup("
        //                    + "sysdate,"
        //                    + "transdate,"
        //                    + "booking,"
        //                    + "patientcode,"
        //                    + "billinggroupcode,"
        //                    + "billingsubgroupcode,"
        //                    + "episodecode,"
        //                    + "isprimary,"
        //                    + "reasonforencounter,"
        //                    + "ipddiagnosistype,"
        //                    + "diagnosiscode,"
        //                    + "diagnosisdescription,"
        //                    + "doctorcode,"
        //                    + "doctordescription,"
        //                    + "deathstatus,"
        //                    + "department,"
        //                    + "treatments,"
        //                    + "yearpart,"
        //                    + "monthpart,"
        //                    + "userid,"
        //                    + "wardcode,"
        //                    + "roomcode,"
        //                    + "bedcode,"
        //                    + "patientweight,"
        //                    + "patienttemperature"
        //                    + ") values("
        //                    + clsGlobal.Saving_DateValue(DateTime.Now) + ","
        //                    + clsGlobal.Saving_DateValue(mTransDate) + ",'"
        //                    + mNewBooking + "','"
        //                    + mPatientCode.Trim() + "','"
        //                    + mBillingGroupCode.Trim() + "','"
        //                    + mBillingSubGroupCode.Trim() + "','"
        //                    + mEpisodeCode.Trim() + "',"
        //                    + mIsPrimary + ",'"
        //                    + mReasonForEncounter + "','"
        //                    + mIPDDiagnosisType.Trim() + "','"
        //                    + mDxCode.Trim() + "','"
        //                    + mDxDescription.Trim() + "','"
        //                    + mDoctorCode.Trim() + "','"
        //                    + mDoctorDescription.Trim() + "',"
        //                    + mCurrentDeathStatus + ",'"
        //                    + mDepartment + "','"
        //                    + mTreatments.Trim() + "',"
        //                    + mYearPart + ","
        //                    + mMonthPart + ",'"
        //                    + mUserId.Trim() + "','"
        //                    + mWardCode + "','"
        //                    + mRoomCode + "','"
        //                    + mBedCode + "',"
        //                    + mWeight + ","
        //                    + mTemperature + ")";

        //                mCommand.ExecuteNonQuery();

        //                #endregion
        //            }
        //            else
        //            {
        //                mCommand.CommandText = "update dxtpatientdiagnoseslog set "
        //                    + "lastencounterdate=" + clsGlobal.Saving_DateValue(mTransDate)
        //                    + " where patientcode='" + mPatientCode.Trim() + "' and diagnosiscode='"
        //                    + mDxCode + "' and episodecode='" + mEpisodeCode + "'";
        //                mCommand.ExecuteNonQuery();

        //                #region dxtpatientdiagnosesfollowup

        //                mCommand.CommandText = "insert into dxtpatientdiagnosesfollowup("
        //                    + "sysdate,"
        //                    + "transdate,"
        //                    + "booking,"
        //                    + "patientcode,"
        //                    + "billinggroupcode,"
        //                    + "billingsubgroupcode,"
        //                    + "episodecode,"
        //                    + "isprimary,"
        //                    + "reasonforencounter,"
        //                    + "ipddiagnosistype,"
        //                    + "diagnosiscode,"
        //                    + "diagnosisdescription,"
        //                    + "doctorcode,"
        //                    + "doctordescription,"
        //                    + "deathstatus,"
        //                    + "department,"
        //                    + "treatments,"
        //                    + "yearpart,"
        //                    + "monthpart,"
        //                    + "userid,"
        //                    + "wardcode,"
        //                    + "roomcode,"
        //                    + "bedcode,"
        //                    + "patientweight,"
        //                    + "patienttemperature"
        //                    + ") values("
        //                    + clsGlobal.Saving_DateValue(DateTime.Now) + ","
        //                    + clsGlobal.Saving_DateValue(mTransDate) + ",'"
        //                    + mNewBooking + "','"
        //                    + mPatientCode.Trim() + "','"
        //                    + mBillingGroupCode.Trim() + "','"
        //                    + mBillingSubGroupCode.Trim() + "','"
        //                    + mEpisodeCode.Trim() + "',"
        //                    + mIsPrimary + ",'"
        //                    + mReasonForEncounter + "','"
        //                    + mIPDDiagnosisType.Trim() + "','"
        //                    + mDxCode.Trim() + "','"
        //                    + mDxDescription.Trim() + "','"
        //                    + mDoctorCode.Trim() + "','"
        //                    + mDoctorDescription.Trim() + "',"
        //                    + mCurrentDeathStatus + ",'"
        //                    + mDepartment + "','"
        //                    + mTreatments.Trim() + "',"
        //                    + mYearPart + ","
        //                    + mMonthPart + ",'"
        //                    + mUserId.Trim() + "','"
        //                    + mWardCode + "','"
        //                    + mRoomCode + "','"
        //                    + mBedCode + "',"
        //                    + mWeight + ","
        //                    + mTemperature + ")";

        //                mCommand.ExecuteNonQuery();

        //                #endregion
        //            }

        //            #region add diagnosis to quick list

        //            DataTable mDtQuickList = new DataTable("quicklist");
        //            mCommand.CommandText = "select code from dxtdiagnoses where code='" + mDxCode.Trim() + "'";
        //            mDataAdapter.SelectCommand = mCommand;
        //            mDataAdapter.Fill(mDtQuickList);

        //            if (mDtQuickList.Rows.Count == 0)
        //            {
        //                DataTable mDtICD = new DataTable("icd");
        //                mCommand.CommandText = "select * from dxticddiagnoses where code='" + mDxCode.Trim() + "'";
        //                mDataAdapter.SelectCommand = mCommand;
        //                mDataAdapter.Fill(mDtICD);

        //                if (mDtICD.Rows.Count > 0)
        //                {
        //                    mCommand.CommandText = "insert into dxtdiagnoses(code,description,groupcode,subgroupcode) values('"
        //                    + mDtICD.Rows[0]["code"] + "','" + mDtICD.Rows[0]["description"] + "','"
        //                    + mDtICD.Rows[0]["groupcode"] + "','" + mDtICD.Rows[0]["subgroupcode"] + "')";
        //                    mCommand.ExecuteNonQuery();
        //                }
        //            }

        //            #endregion
        //        }

        //        //return
        //        return mResult;
        //    }
        //    catch (OdbcException ex)
        //    {
        //        mResult.Exe_Result = -1;
        //        mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
        //        return mResult;
        //    }
        //}
        #endregion
    }
}
