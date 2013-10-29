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
    public class clsRegistrations : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsRegistrations";

        #endregion

        #region View_Patients
        public DataTable View_Patients(String mFilter, String mOrder)
        {
            String mFunctionName = "View_Patients";

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
                string mCommandText = "select * from patients";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("patients");
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

        #region View_PatientsQueue
        public DataTable View_PatientsQueue(string mPointCode, DateTime mTransDate, int mQueueType)
        {
            String mFunctionName = "View_PatientsQueue";

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
                mTransDate = mTransDate.Date;

                //columns from patients
                string mPatientColumns = clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code", "p", "patient");
                mPatientColumns = mPatientColumns + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "patientfullname");
                mPatientColumns = mPatientColumns + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "b.transdate", ""),"patientage");
                //columns from trans
                string mTransColumns = clsGlobal.Get_TableColumns(mCommand, "view_patientsqueue", "", "b", "");

                string mCommandText = "";

                if (mQueueType == (int)AfyaPro_Types.clsEnums.PatientsQueueTypes.Consultation
                    || mQueueType == (int)AfyaPro_Types.clsEnums.PatientsQueueTypes.Results)
                {
                    mCommandText = ""
                        + "SELECT "
                        + mTransColumns + ","
                        + mPatientColumns + " "
                        + "FROM view_patientsqueue AS b "
                        + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                        + "WHERE (b.queuetype=" + (int)AfyaPro_Types.clsEnums.PatientsQueueTypes.Consultation + " OR "
                        + "queuetype=" + (int)AfyaPro_Types.clsEnums.PatientsQueueTypes.Results + ") AND treatmentpointcode='" + mPointCode.Trim() + "' "
                        + "AND transdate=" + clsGlobal.Saving_DateValue(mTransDate) + " "
                        + "ORDER BY b.transdate, b.autocode";
                }
                else
                {
                    mCommandText = ""
                        + "SELECT "
                        + mTransColumns + ","
                        + mPatientColumns + " "
                        + "FROM view_patientsqueue AS b "
                        + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                        + "WHERE b.queuetype=" + mQueueType + " AND laboratorycode='" + mPointCode.Trim() + "' "
                        + "ORDER BY b.transdate, b.autocode";
                }

                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_patientsqueue");
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

        #region View_ActiveBookings
        public DataTable View_ActiveBookings(String mFilter, String mOrder)
        {
            String mFunctionName = "View_ActiveBookings";

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
                //columns from patients
                string mPatientColumns = clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "patient");
                mPatientColumns = mPatientColumns + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "patientfullname");
                mPatientColumns = mPatientColumns + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "b.bookdate", ""), "patientage");
                //columns from trans
                string mTransColumns = clsGlobal.Get_TableColumns(mCommand, "facilitybookings", "", "b", "");

                string mCommandText = ""
                    + "SELECT "
                    + mTransColumns + ","
                    + mPatientColumns + " "
                    + "FROM facilitybookings AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("facilitybookings");
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

        #region View_ArchiveBookings
        public DataTable View_ArchiveBookings(bool mDateSpecified, DateTime mDateFrom, DateTime mDateTo, String mExtraFilter, String mOrder, string mLanguageName, string mGridName)
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
                        mExtraFilter = "(bookdate between " + clsGlobal.Saving_DateValue(mDateFrom)
                            + " and " + clsGlobal.Saving_DateValue(mDateTo) + ")";
                    }
                    else
                    {
                        mExtraFilter = "(" + mExtraFilter + ") and (bookdate between " + clsGlobal.Saving_DateValue(mDateFrom)
                            + " and " + clsGlobal.Saving_DateValue(mDateTo) + ")";
                    }
                }
                #endregion

                //columns from patients
                string mPatientColumns = clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "patient");
                mPatientColumns = mPatientColumns + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "patientfullname");
                mPatientColumns = mPatientColumns + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "b.bookdate", ""), "patientage");
                //columns from trans
                string mTransColumns = clsGlobal.Get_TableColumns(mCommand, "facilitybookinglog", "", "b", "");

                string mCommandText = ""
                    + "SELECT "
                    + mTransColumns + ","
                    + mPatientColumns + " "
                    + "FROM facilitybookinglog AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mExtraFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("facilitybookinglog");
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

        #region View_CurrentAdmissions
        public DataTable View_CurrentAdmissions(bool mDateSpecified, DateTime mDateFrom, DateTime mDateTo, String mExtraFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_CurrentAdmissions";

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
                //columns from patients
                string mPatientColumns = clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "patient");
                mPatientColumns = mPatientColumns + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "patientfullname");
                mPatientColumns = mPatientColumns + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "ad.transdate", ""), "patientage");
                //columns from trans
                string mTransColumns = clsGlobal.Get_TableColumns(mCommand, "ipdadmissionslog", "", "ad", "");
                string mWard = "wd.description AS warddescription";
                string mRoom = "rm.description AS roomdescription";
                string mBed = "bd.description AS beddescription";
                string mTransfer = "tr.transferdate";            

                string mCommandText =""
                    + "SELECT "
                    + mTransColumns + ","
                    + mPatientColumns + ","
                    + mWard + ","
                    + mRoom + ","
                    + mBed + ","
                    + mTransfer + " "
                    + "FROM ipdadmissionslog AS ad "
                    + "LEFT OUTER JOIN patients AS p ON ad.patientcode=p.code "
                    + "LEFT OUTER JOIN ipddischargeslog AS dis ON "    
                    + "(ad.patientcode = dis.patientcode) and "
                    + "(ad.wardcode = dis.wardcode) and "
                    + "(ad.roomcode = dis.roomcode) and "
                    + "(ad.bedcode = dis.bedcode) "
                    + "LEFT OUTER JOIN ipdtransferslog AS tr ON "
                    + "(ad.autocode = tr.admissionid) "
                    + "LEFT OUTER JOIN facilitywards wd ON (wd.CODE =ad.wardcode) "
                    + "LEFT OUTER JOIN facilitywardrooms rm ON(rm.CODE =ad.roomcode) "
                    + "LEFT OUTER JOIN facilitywardroombeds bd ON(bd.CODE =ad.bedcode) "               
                    + " where (dis.dischargestatuscode='' OR dis.dischargestatuscode is null)";

                #region add dates to filter string

                if (mDateSpecified == true)
                {
                    mDateFrom = mDateFrom.Date;
                    mDateTo = mDateTo.Date;

                    if (mExtraFilter.Trim() == "")
                    {
                        mExtraFilter = "(ad.transdate between " + clsGlobal.Saving_DateValue(mDateFrom)
                            + " and " + clsGlobal.Saving_DateValue(mDateTo) + ")";
                    }
                    else
                    {
                        mExtraFilter = "(" + mExtraFilter + ") and (ad.transdate between " + clsGlobal.Saving_DateValue(mDateFrom)
                            + " and " + clsGlobal.Saving_DateValue(mDateTo) + ")";
                    }
                }
                #endregion

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("ipdadmissionslog");
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

        #region Get_Patient
        public AfyaPro_Types.clsPatient Get_Patient(string mPatientCode)
        {
            string mFunctionName = "Get_Patient";

            AfyaPro_Types.clsPatient mPatient = new AfyaPro_Types.clsPatient();
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
                DataTable mDtPatients = new DataTable("patients");
                mCommand.CommandText = "select * from patients where code='" + mPatientCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtPatients);

                if (mDtPatients.Rows.Count > 0)
                {
                    mPatient.Exist = true;
                    mPatient.code = mDtPatients.Rows[0]["code"].ToString();
                    mPatient.surname = mDtPatients.Rows[0]["surname"].ToString();
                    mPatient.firstname = mDtPatients.Rows[0]["firstname"].ToString();
                    mPatient.othernames = mDtPatients.Rows[0]["othernames"].ToString();
                    mPatient.gender = mDtPatients.Rows[0]["gender"].ToString();
                    mPatient.birthdate = Convert.ToDateTime(mDtPatients.Rows[0]["birthdate"]);
                    mPatient.chronic = mDtPatients.Rows[0]["chronic"].ToString();
                    mPatient.severe = mDtPatients.Rows[0]["severe"].ToString();
                    mPatient.operations = mDtPatients.Rows[0]["operations"].ToString();
                    mPatient.regdate = Convert.ToDateTime(mDtPatients.Rows[0]["regdate"]);
                    mPatient.maritalstatuscode = mDtPatients.Rows[0]["maritalstatuscode"].ToString();
                    mPatient.allergies = mDtPatients.Rows[0]["allergies"].ToString();
                }
                else
                {
                    mPatient.Exe_Result = 0;
                    mPatient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoDoesNotExist.ToString();
                    return mPatient;
                }

                return mPatient;
            }
            catch (OdbcException ex)
            {
                mPatient.Exe_Result = -1;
                mPatient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mPatient;
            }
        }
        #endregion

        #region Get_Booking
        public AfyaPro_Types.clsBooking Get_Booking(string mPatientCode)
        {
            string mFunctionName = "Get_Booking";

            AfyaPro_Types.clsBooking mBooking = new AfyaPro_Types.clsBooking();
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
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }

            #endregion

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

        #region Get_Admission
        public AfyaPro_Types.clsAdmission Get_Admission(string mBooking, string mPatientCode)
        {
            string mFunctionName = "Get_Admission";

            AfyaPro_Types.clsAdmission mAdmission = new AfyaPro_Types.clsAdmission();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;

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
                mAdmission.Exe_Result = -1;
                mAdmission.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }

            #endregion

            #region get admission details
            try
            {
                // old code
                //mCommand.CommandText = "select ad.* from view_ipdadmissionslog ad left outer join ipddischargeslog dis on "
                //    + "ad.autocode=dis.admissionid where ad.booking='" + mBooking.Trim() + "' and dis.admissionid is null";
                //mDataReader = mCommand.ExecuteReader();




                mCommand.CommandText = "select * from view_ipdadmissionslog "
                   + "where patientcode='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();

                if (mDataReader.Read() == true)
                {
                    mAdmission.IsAdmitted = true;
                    mAdmission.AdmissionId = Convert.ToInt32(mDataReader["autocode"]);
                    mAdmission.TransDate = Convert.ToDateTime(mDataReader["transdate"]);
                    mAdmission.PatientCode = mDataReader["patientcode"].ToString().Trim();
                    mAdmission.Booking = mDataReader["booking"].ToString();
                    mAdmission.WardCode = mDataReader["wardcode"].ToString().Trim();
                    mAdmission.WardDescription = mDataReader["warddescription"].ToString().Trim();
                    mAdmission.RoomCode = mDataReader["roomcode"].ToString();
                   mAdmission.RoomDescription = mDataReader["roomdescription"].ToString().Trim();
                    mAdmission.BedCode = mDataReader["bedcode"].ToString().Trim();
                    mAdmission.BedDescription = mDataReader["beddescription"].ToString().Trim();
                    mAdmission.PatientCondition = mDataReader["patientcondition"].ToString().Trim();
                    mAdmission.RegistryStatus = mDataReader["registrystatus"].ToString().Trim();
                }
                else
                {
                    mAdmission.IsAdmitted = false;
                    mAdmission.Exe_Result = 0;
                    mAdmission.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientIsNotAdmitted.ToString();
                    return mAdmission;
                }

                return mAdmission;
            }
            catch (OdbcException ex)
            {
                mAdmission.Exe_Result = -1;
                mAdmission.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion
        }
        #endregion

        #region Get_Discharge
        public AfyaPro_Types.clsDischarge Get_Discharge(string mBooking, string mPatientCode, 
            string mWardCode, string mRoomCode)
        {
            string mFunctionName = "Get_Discharge";

            AfyaPro_Types.clsDischarge mDischarge = new AfyaPro_Types.clsDischarge();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;

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
                mDischarge.Exe_Result = -1;
                mDischarge.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }

            #endregion

            #region get discharge details
            try
            {
                mCommand.CommandText = "select * from view_ipddischargeslog where booking='" + mBooking.Trim()
                + "' and patientcode='" + mPatientCode.Trim() + "' and wardcode='" + mWardCode.Trim()
                + "' and roomcode='" + mRoomCode.Trim() + "' and transcode<>'" + AfyaPro_Types.clsEnums.IPDTransTypes.Transfer.ToString() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mDischarge.IsDischarged = true;
                    mDischarge.TransDate = Convert.ToDateTime(mDataReader["transdate"]);
                    mDischarge.PatientCode = mDataReader["patientcode"].ToString().Trim();
                    mDischarge.Booking = mDataReader["booking"].ToString();
                    mDischarge.WardCode = mDataReader["wardcode"].ToString().Trim();
                    mDischarge.WardDescription = mDataReader["warddescription"].ToString().Trim();
                    mDischarge.RoomCode = mDataReader["roomcode"].ToString();
                    mDischarge.RoomDescription = mDataReader["roomdescription"].ToString().Trim();
                    mDischarge.BedCode = mDataReader["bedcode"].ToString().Trim();
                    mDischarge.BedDescription = mDataReader["beddescription"].ToString().Trim();
                    mDischarge.PatientCondition = mDataReader["patientcondition"].ToString().Trim();
                    mDischarge.StatusCode = mDataReader["dischargestatuscode"].ToString().Trim();
                    mDischarge.StatusDescription = mDataReader["dischargestatusdescription"].ToString().Trim();
                    mDischarge.Remarks = mDataReader["dischargeremarks"].ToString().Trim();
                }
                else
                {
                    mDischarge.IsDischarged = false;
                    mDischarge.Exe_Result = 0;
                    mDischarge.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientIsNotAdmitted.ToString();
                    return mDischarge;
                }

                return mDischarge;
            }
            catch (OdbcException ex)
            {
                mDischarge.Exe_Result = -1;
                mDischarge.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion
        }
        #endregion

        #region Add_Patient
        public AfyaPro_Types.clsPatient Add_Patient(Int16 mGenerateCode, string mCode, string mSurname, string mFirstName,
            string mOtherNames, string mGender, DateTime mBirthDate, string mChronic, string mSevere, string mOperations, 
            DateTime mRegDate, string mMaritalStatusCode, string mAllergies, DataTable mDtExtraDetails, DateTime mTransDate, 
            string mMachineName, string mMachineUser, string mUserId)
        {
            String mFunctionName = "Add_Patient";

            AfyaPro_Types.clsPatient mPatient = new AfyaPro_Types.clsPatient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            bool mIsReAttending = false;

            mTransDate = mTransDate.Date;
            mBirthDate = mBirthDate.Date;
            mRegDate = mRegDate.Date;

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
                mPatient.Exe_Result = -1;
                mPatient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mPatient;
            }

            #endregion

            #region auto generate code, if option is on

            if (mGenerateCode == 1)
            {
                clsAutoCodes objAutoCodes = new clsAutoCodes();
                AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                mObjCode = objAutoCodes.Next_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.patientno), "patients", "code");
                if (mObjCode.Exe_Result == -1)
                {
                    mPatient.Exe_Result = mObjCode.Exe_Result;
                    mPatient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                    return mPatient;
                }
                mCode = mObjCode.GeneratedCode;
            }
            else
            {
                //check if is trying to force re-attendance
                clsAutoCodes objAutoCodes = new clsAutoCodes();

                Int16 mAutoGenerate = objAutoCodes.Auto_Generate_Code(Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.patientno));
                if (mAutoGenerate == 1)
                {
                    mIsReAttending = true;
                }
            }

            #endregion

            #region check re-attendance number validity

            if (mIsReAttending == true)
            {
                try
                {
                    mCommand.CommandText = "select * from revisitingnumbers where patientcode='" + mCode.Trim() + "'";
                    mDataReader = mCommand.ExecuteReader();
                    if (mDataReader.Read() == false)
                    {
                        mPatient.Exe_Result = 0;
                        mPatient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoIsOutOfRange.ToString();
                        return mPatient;
                    }
                }
                catch (OdbcException ex)
                {
                    mPatient.Exe_Result = -1;
                    mPatient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                    return mPatient;
                }
                finally
                {
                    mDataReader.Close();
                }
            }

            #endregion

            #region check 4 duplicate
            try
            {
                mCommand.CommandText = "select * from patients where code='"
                + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mPatient.Exe_Result = 0;
                    mPatient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoIsInUse.ToString();
                    return mPatient;
                }
            }
            catch (OdbcException ex)
            {
                mPatient.Exe_Result = -1;
                mPatient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mPatient;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion

            #region add
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                string mExtraDetailFields = "";
                string mExtraDetailValues = "";

                #region prepare extra field details

                if (mDtExtraDetails.Rows.Count > 0)
                {
                    foreach (DataColumn mDataColumn in mDtExtraDetails.Columns)
                    {
                        string mFieldValue = "";

                        #region get field value
                        switch (mDataColumn.DataType.FullName.Trim().ToLower())
                        {
                            case "system.datetime":
                                {
                                    mFieldValue = clsGlobal.Saving_DateValueNullable(mDtExtraDetails.Rows[0][mDataColumn.ColumnName]);
                                }
                                break;
                            case "system.double":
                                {
                                    mFieldValue = mDtExtraDetails.Rows[0][mDataColumn.ColumnName].ToString();
                                }
                                break;
                            case "system.decimal":
                                {
                                    mFieldValue = mDtExtraDetails.Rows[0][mDataColumn.ColumnName].ToString();
                                }
                                break;
                            case "system.int32":
                                {
                                    mFieldValue = mDtExtraDetails.Rows[0][mDataColumn.ColumnName].ToString();
                                }
                                break;
                            default:
                                {
                                    mFieldValue = "'" + mDtExtraDetails.Rows[0][mDataColumn.ColumnName].ToString() + "'";
                                }
                                break;
                        }
                        #endregion

                        if (mExtraDetailFields.Trim() == "")
                        {
                            mExtraDetailFields = mDataColumn.ColumnName;
                            mExtraDetailValues = mFieldValue;
                        }
                        else
                        {
                            mExtraDetailFields = mExtraDetailFields + "," + mDataColumn.ColumnName;
                            mExtraDetailValues = mExtraDetailValues + "," + mFieldValue;
                        }
                    }
                }

                if (mExtraDetailFields.Trim() != "")
                {
                    mExtraDetailFields = "," + mExtraDetailFields;
                    mExtraDetailValues = "," + mExtraDetailValues;
                }

                #endregion

                //patients
                mCommand.CommandText = "insert into patients(code,surname,firstname,othernames,gender,birthdate,"
                + "chronic,severe,operations,regdate,maritalstatuscode,allergies" + mExtraDetailFields + ") values('" + mCode.Trim() + "','"
                + mSurname.Trim() + "','" + mFirstName.Trim() + "','" + mOtherNames.Trim() + "','" + mGender + "'," 
                + clsGlobal.Saving_DateValue(mBirthDate) + ",'" + mChronic.Trim() + "','" + mSevere.Trim() + "','"
                + mOperations.Trim() + "'," + clsGlobal.Saving_DateValue(mRegDate) + ",'" + mMaritalStatusCode 
                + "','" + mAllergies + "'" + mExtraDetailValues + ")";
                int mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit patients

                if (mRecsAffected > 0)
                {
                    string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "patients";
                    string mAuditingFields = clsGlobal.AuditingFields();
                    string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                        AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                    mCommand.CommandText = "insert into " + mAuditTableName + "(code,surname,firstname,othernames,gender,birthdate,"
                    + "chronic,severe,operations,regdate,maritalstatuscode,allergies" + mExtraDetailFields + "," + mAuditingFields + ") values('" + mCode.Trim() + "','"
                    + mSurname.Trim() + "','" + mFirstName.Trim() + "','" + mOtherNames.Trim() + "','" + mGender + "',"
                    + clsGlobal.Saving_DateValue(mBirthDate) + ",'" + mChronic.Trim() + "','" + mSevere.Trim() + "','"
                    + mOperations.Trim() + "'," + clsGlobal.Saving_DateValue(mRegDate) + ",'" + mMaritalStatusCode + "','" + mAllergies + "'" 
                    + mExtraDetailValues + "," + mAuditingValues + ")";
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region remember user entries for next time use

                DataTable mDtExtraFields = new DataTable("patientextrafields");
                mCommand.CommandText = "select * from patientextrafields";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtExtraFields);

                DataView mDvExtraFields = new DataView();
                mDvExtraFields.Table = mDtExtraFields;
                mDvExtraFields.Sort = "fieldname";

                if (mDtExtraDetails.Rows.Count > 0)
                {
                    foreach (DataColumn mDataColumn in mDtExtraDetails.Columns)
                    {
                        string mFieldName = mDataColumn.ColumnName;
                        string mFilterOnValueFrom = "";
                        string mFieldType = "";

                        int mRowIndex = mDvExtraFields.Find(mFieldName);
                        if (mRowIndex >= 0)
                        {
                            mFieldType = mDvExtraFields[mRowIndex]["fieldtype"].ToString().Trim();
                            mFilterOnValueFrom = mDvExtraFields[mRowIndex]["filteronvaluefrom"].ToString().Trim();
                        }

                        if (mFieldType.Trim().ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            string mFilterValue = "";
                            string mDescription = mDtExtraDetails.Rows[0][mFieldName].ToString();
                            if (mFilterOnValueFrom.Trim() != "")
                            {
                                mFilterValue = mDtExtraDetails.Rows[0][mFilterOnValueFrom].ToString().Trim();
                            }

                            DataTable mDtLookup = new DataTable("lookup");
                            mCommand.CommandText = "select * from patientextrafieldlookup where fieldname='"
                            + mFieldName + "' and description='" + mDescription + "' and filtervalue='" + mFilterValue + "'";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(mDtLookup);

                            if (mDtLookup.Rows.Count == 0)
                            {
                                mCommand.CommandText = "insert into patientextrafieldlookup(fieldname,description,"
                                + "filtervalue) values('" + mFieldName + "','" + mDescription + "','" + mFilterValue + "')";
                                mCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }

                //surname
                DataTable mDtSurname = new DataTable("surname");
                mCommand.CommandText = "select * from patientsurnames where description='" + mSurname.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSurname);
                if (mDtSurname.Rows.Count == 0)
                {
                    mCommand.CommandText = "insert into patientsurnames(description) values('" + mSurname.Trim() + "')";
                    mCommand.ExecuteNonQuery();
                }

                //firstname
                DataTable mDtFirstName = new DataTable("firstname");
                mCommand.CommandText = "select * from patientfirstnames where description='" + mFirstName.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFirstName);
                if (mDtFirstName.Rows.Count == 0)
                {
                    mCommand.CommandText = "insert into patientfirstnames(description) values('" + mFirstName.Trim() + "')";
                    mCommand.ExecuteNonQuery();
                }

                //othernames
                DataTable mDtOtherNames = new DataTable("othernames");
                mCommand.CommandText = "select * from patientothernames where description='" + mOtherNames.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtOtherNames);
                if (mDtOtherNames.Rows.Count == 0)
                {
                    mCommand.CommandText = "insert into patientothernames(description) values('" + mOtherNames.Trim() + "')";
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                if (mGenerateCode == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.patientno);
                    mCommand.ExecuteNonQuery();
                }

                //commit
                mTrans.Commit();

                //get patient
                mPatient = this.Get_Patient(mCode);

                //return
                return mPatient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mPatient.Exe_Result = -1;
                mPatient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mPatient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Edit_Patient
        public AfyaPro_Types.clsPatient Edit_Patient(string mCode, string mSurname, string mFirstName,
            string mOtherNames, string mGender, DateTime mBirthDate, string mChronic, string mSevere, string mOperations,
            DateTime mRegDate, string mMaritalStatusCode, string mAllergies, DataTable mDtExtraDetails, DateTime mTransDate, 
            string mMachineName, string mMachineUser, string mUserId)
        {
            String mFunctionName = "Edit_Patient";

            AfyaPro_Types.clsPatient mPatient = new AfyaPro_Types.clsPatient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;
            mBirthDate = mBirthDate.Date;
            mRegDate = mRegDate.Date;

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
                mPatient.Exe_Result = -1;
                mPatient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mPatient;
            }

            #endregion

            #region check 4 existance
            try
            {
                mCommand.CommandText = "select * from patients where code='"
                + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mPatient.Exe_Result = 0;
                    mPatient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoDoesNotExist.ToString();
                    return mPatient;
                }
            }
            catch (OdbcException ex)
            {
                mPatient.Exe_Result = -1;
                mPatient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mPatient;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion

            #region edit
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                string mUpdateExtraDetails = "";

                #region prepare extra field details

                if (mDtExtraDetails.Rows.Count > 0)
                {
                    foreach (DataColumn mDataColumn in mDtExtraDetails.Columns)
                    {
                        string mFieldValue = "";

                        #region get field value
                        switch (mDataColumn.DataType.FullName.Trim().ToLower())
                        {
                            case "system.datetime":
                                {
                                    mFieldValue = clsGlobal.Saving_DateValueNullable(mDtExtraDetails.Rows[0][mDataColumn.ColumnName]);
                                }
                                break;
                            case "system.double":
                                {
                                    mFieldValue = mDtExtraDetails.Rows[0][mDataColumn.ColumnName].ToString();
                                }
                                break;
                            case "system.decimal":
                                {
                                    mFieldValue = mDtExtraDetails.Rows[0][mDataColumn.ColumnName].ToString();
                                }
                                break;
                            case "system.int32":
                                {
                                    mFieldValue = mDtExtraDetails.Rows[0][mDataColumn.ColumnName].ToString();
                                }
                                break;
                            default:
                                {
                                    mFieldValue = "'" + mDtExtraDetails.Rows[0][mDataColumn.ColumnName].ToString() + "'";
                                }
                                break;
                        }
                        #endregion

                        if (mUpdateExtraDetails.Trim() == "")
                        {
                            mUpdateExtraDetails = mDataColumn.ColumnName + "=" + mFieldValue;
                        }
                        else
                        {
                            mUpdateExtraDetails = mUpdateExtraDetails + "," + mDataColumn.ColumnName + "=" + mFieldValue;
                        }
                    }
                }

                if (mUpdateExtraDetails.Trim() != "")
                {
                    mUpdateExtraDetails = "," + mUpdateExtraDetails;
                }

                #endregion

                //patients
                mCommand.CommandText = "update patients set surname='" + mSurname.Trim() + "',firstname='" + mFirstName.Trim()
                + "',othernames='" + mOtherNames.Trim() + "',gender='" + mGender + "',birthdate="
                + clsGlobal.Saving_DateValue(mBirthDate) + ",chronic='" + mChronic.Trim() + "',severe='" + mSevere.Trim()
                + "',operations='" + mOperations.Trim() + "',regdate=" + clsGlobal.Saving_DateValue(mRegDate)
                + ",maritalstatuscode='" + mMaritalStatusCode + "',allergies='" + mAllergies + "'"
                + mUpdateExtraDetails + " where code='" + mCode.Trim() + "'";
                int mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit patients

                if (mRecsAffected > 0)
                {
                    DataTable mDtPatients = new DataTable("patients");
                    mCommand.CommandText = "select * from patients where code='" + mCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtPatients);

                    mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtPatients, "patients",
                    mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region remember user entries for next time use

                DataTable mDtExtraFields = new DataTable("patientextrafields");
                mCommand.CommandText = "select * from patientextrafields";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtExtraFields);

                DataView mDvExtraFields = new DataView();
                mDvExtraFields.Table = mDtExtraFields;
                mDvExtraFields.Sort = "fieldname";

                if (mDtExtraDetails.Rows.Count > 0)
                {
                    foreach (DataColumn mDataColumn in mDtExtraDetails.Columns)
                    {
                        string mFieldName = mDataColumn.ColumnName;
                        string mFilterOnValueFrom = "";
                        string mFieldType = "";

                        int mRowIndex = mDvExtraFields.Find(mFieldName);
                        if (mRowIndex >= 0)
                        {
                            mFieldType = mDvExtraFields[mRowIndex]["fieldtype"].ToString().Trim();
                            mFilterOnValueFrom = mDvExtraFields[mRowIndex]["filteronvaluefrom"].ToString().Trim();
                        }

                        if (mFieldType.Trim().ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            string mFilterValue = "";
                            string mDescription = mDtExtraDetails.Rows[0][mFieldName].ToString();
                            if (mFilterOnValueFrom.Trim() != "")
                            {
                                mFilterValue = mDtExtraDetails.Rows[0][mFilterOnValueFrom].ToString().Trim();
                            }

                            DataTable mDtLookup = new DataTable("lookup");
                            mCommand.CommandText = "select * from patientextrafieldlookup where fieldname='"
                            + mFieldName + "' and description='" + mDescription + "' and filtervalue='" + mFilterValue + "'";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(mDtLookup);

                            if (mDtLookup.Rows.Count == 0)
                            {
                                mCommand.CommandText = "insert into patientextrafieldlookup(fieldname,description,"
                                + "filtervalue) values('" + mFieldName + "','" + mDescription + "','" + mFilterValue + "')";
                                mCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }

                //surname
                DataTable mDtSurname = new DataTable("surname");
                mCommand.CommandText = "select * from patientsurnames where description='" + mSurname.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSurname);
                if (mDtSurname.Rows.Count == 0)
                {
                    mCommand.CommandText = "insert into patientsurnames(description) values('" + mSurname.Trim() + "')";
                    mCommand.ExecuteNonQuery();
                }

                //firstname
                DataTable mDtFirstName = new DataTable("firstname");
                mCommand.CommandText = "select * from patientfirstnames where description='" + mFirstName.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFirstName);
                if (mDtFirstName.Rows.Count == 0)
                {
                    mCommand.CommandText = "insert into patientfirstnames(description) values('" + mFirstName.Trim() + "')";
                    mCommand.ExecuteNonQuery();
                }

                //othernames
                DataTable mDtOtherNames = new DataTable("othernames");
                mCommand.CommandText = "select * from patientothernames where description='" + mOtherNames.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtOtherNames);
                if (mDtOtherNames.Rows.Count == 0)
                {
                    mCommand.CommandText = "insert into patientothernames(description) values('" + mOtherNames.Trim() + "')";
                    mCommand.ExecuteNonQuery();
                }

                #endregion
 
                //commit
                mTrans.Commit();

                //get patient
                mPatient = this.Get_Patient(mCode);

                //return
                return mPatient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mPatient.Exe_Result = -1;
                mPatient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mPatient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Book
        public AfyaPro_Types.clsBooking Book(DateTime mTransDate, string mPatientCode, string mBillingGroupCode,
            string mBillingSubGroupCode, string mBillingGroupMembershipNo, string mTreatmentPointCode, string mPriceCategory, 
            string mTreatmentPoint, double mWeight, double mTemperature, string mUserId)
        {
            String mFunctionName = "Book";

            AfyaPro_Types.clsBooking mBooking = new AfyaPro_Types.clsBooking();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            DataTable mDtBookings = new DataTable("bookings");
            //string mSurname = "";
            //string mFirstName = "";
            //string mOtherNames = "";
            //string mGender = "";
            //DateTime mBirthDate = new DateTime();
            //DateTime mRegDate = new DateTime();

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
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
            }

            #endregion

            #region check for patient existance

            try
            {
                mCommand.CommandText = "select * from patients where code='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mBooking.Exe_Result = 0;
                    mBooking.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoDoesNotExist.ToString();
                    return mBooking;
                }
            }
            catch (OdbcException ex)
            {
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
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
                    false,
                    mUserId);

                if (mBooking.Exe_Result != 1)
                {
                    try { mTrans.Rollback(); }
                    catch { }
                    return mBooking;
                }

                mTrans.Commit();

                //return
                return mBooking;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
            }
            finally
            {
                mConn.Close();
            }
        }
        #endregion

        #region Change_Booking
        public AfyaPro_Types.clsBooking Change_Booking(DateTime mTransDate, string mPatientCode, string mBillingGroupCode,
            string mBillingSubGroupCode, string mBillingGroupMembershipNo, string mTreatmentPointCode, string mPriceCategory, string mUserId)
        {
            String mFunctionName = "Change_Booking";

            AfyaPro_Types.clsBooking mBooking = new AfyaPro_Types.clsBooking();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
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
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
            }

            #endregion

            #region check for patient existance

            try
            {
                mCommand.CommandText = "select * from patients where code='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mBooking.Exe_Result = 0;
                    mBooking.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoDoesNotExist.ToString();
                    return mBooking;
                }
            }
            catch (OdbcException ex)
            {
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
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

                mBooking = mWorkFlowAgent.Do_ChangeBooking(
                    mCommand,
                    mTransDate,
                    mPatientCode,
                    mBillingGroupCode,
                    mBillingSubGroupCode,
                    mBillingGroupMembershipNo,
                    mPriceCategory,
                    mUserId);

                if (mBooking.Exe_Result != 1)
                {
                    try { mTrans.Rollback(); }
                    catch { }
                    return mBooking;
                }

                mTrans.Commit();

                //return
                return mBooking;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
            }
            finally
            {
                mConn.Close();
            }
        }
        #endregion

        #region Admit
        public AfyaPro_Types.clsBooking Admit(int mAdmissionId, DateTime mTransDate, string mPatientCode, string mBillingGroupCode,
            string mBillingSubGroupCode, string mBillingGroupMembershipNo, string mPriceCategory, string mWardCode, string mRoomCode,
            string mRemarks, string mBedCode, double mWeight, double mTemperature, string mUserId)
        {
            String mFunctionName = "Admit";

            AfyaPro_Types.clsBooking mBooking = new AfyaPro_Types.clsBooking();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;

            string mNewBooking = "";
            DateTime mPrevBookDate = new DateTime();
            bool mIsReAttendance = false;
            string mSurname = "";
            string mFirstName = "";
            string mOtherNames = "";
            string mGender = "";
            string mBillingGroup = "";
            string mBillingSubGroup = "";
            DateTime mBirthDate = new DateTime();
            DateTime mRegDate = new DateTime();

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
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
            }

            #endregion

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

            #region check for patient existance

            try
            {
                mCommand.CommandText = "select * from patients where code='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mSurname = mDataReader["surname"].ToString();
                    mFirstName = mDataReader["firstname"].ToString();
                    mOtherNames = mDataReader["othernames"].ToString();
                    mGender = mDataReader["gender"].ToString();
                    mBirthDate = Convert.ToDateTime(mDataReader["birthdate"]);
                    mRegDate = Convert.ToDateTime(mDataReader["regdate"]);
                }
                else
                {
                    mBooking.Exe_Result = 0;
                    mBooking.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoDoesNotExist.ToString();
                    return mBooking;
                }
            }
            catch (OdbcException ex)
            {
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
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

                string mTreatmentPointCode = "IPD";
                string mTreatmentPoint = "IPD";

                string mDepartment = AfyaPro_Types.clsEnums.PatientCategories.IPD.ToString();
                clsWorkFlowAgent mWorkFlowAgent = new clsWorkFlowAgent();

                #region do booking
               // 
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
                    true,
                    mUserId);

                if (mBooking.Exe_Result != 1)
                {
                    try { mTrans.Rollback(); }
                    catch { }
                    return mBooking;
                }

                mNewBooking = mBooking.Booking;
                mPrevBookDate = mBooking.BookDate;
                mIsReAttendance = !mBooking.IsNewAttendance;
                mBillingGroup = mBooking.BillingGroupDescription;
                mBillingSubGroup = mBooking.BillingSubGroupDescription;

             

                #endregion

                #region admission
               
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
                    return mBooking;
                }

                #endregion

                mTrans.Commit();

                //get booking that has been done
                mBooking = this.Get_Booking(mPatientCode);

                if (mIsReAttendance == false)
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
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
            }
            finally
            {
                mConn.Close();
            }
        }
        #endregion

        #region IPD_Transfer
        public AfyaPro_Types.clsBooking IPD_Transfer(int mAdmissionId, DateTime mTransDate, string mPatientCode, string mFromWardCode, 
            string mFromRoomCode, string mFromBedCode, string mToWardCode, string mToRoomCode, string mToBedCode,
            string mPatientCondition, string mUserId)
        {
            String mFunctionName = "IPD_Transfer";

            AfyaPro_Types.clsBooking mBooking = new AfyaPro_Types.clsBooking();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;

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
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
            }

            #endregion

            #region check for patient existance

            try
            {
                mCommand.CommandText = "select * from patients where code='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mWeight = Convert.ToDouble(mDataReader["weight"]);
                    mTemperature = Convert.ToDouble(mDataReader["temperature"]);
                }
                else
                {
                    mBooking.Exe_Result = 0;
                    mBooking.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoDoesNotExist.ToString();
                    return mBooking;
                }
            }
            catch (OdbcException ex)
            {
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
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
                    if (mDataReader["department"].ToString().Trim().ToLower() ==
                        AfyaPro_Types.clsEnums.PatientCategories.OPD.ToString().Trim().ToLower())
                    {
                        mBooking.Exe_Result = 0;
                        mBooking.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientIsNotAdmitted.ToString();
                        return mBooking;
                    }
                }
            }
            catch (OdbcException ex)
            {
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
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

                mBooking = this.Get_Booking(mPatientCode);

                clsWorkFlowAgent mWorkFlowAgent = new clsWorkFlowAgent();

                AfyaPro_Types.clsBooking mTransfer = mWorkFlowAgent.Do_Transfering(
                                        mCommand,
                                        mAdmissionId,
                                        mTransDate,
                                        mPatientCode,
                                        mFromWardCode,
                                        mFromRoomCode,
                                        mFromBedCode,
                                        mToWardCode,
                                        mToRoomCode,
                                        mToBedCode,
                                        mPatientCondition,
                                        mWeight,
                                        mTemperature,
                                        mBooking,
                                        mUserId);

                if (mTransfer.Exe_Result != 1)
                {
                    try { mTrans.Rollback(); }
                    catch { }
                    return mBooking;
                }

                mTrans.Commit();

                //get booking that has been done
                mBooking = this.Get_Booking(mPatientCode);

                //return
                return mBooking;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
            }
            finally
            {
                mConn.Close();
            }
        }
        #endregion

        #region IPD_Discharge
        public AfyaPro_Types.clsBooking IPD_Discharge(int mAdmissionId, DateTime mTransDate, string mPatientCode,
            string mFromWardCode, string mFromRoomCode, string mFromBedCode, string mStatusCode, string mStatusDescription, string mRemarks, string mUserId)
        {
            String mFunctionName = "IPD_Discharge";

            AfyaPro_Types.clsBooking mBooking = new AfyaPro_Types.clsBooking();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;

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
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
            }

            #endregion

            #region check for patient existance

            try
            {
                mCommand.CommandText = "select * from patients where code='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mWeight = Convert.ToDouble(mDataReader["weight"]);
                    mTemperature = Convert.ToDouble(mDataReader["temperature"]);
                }
                else
                {
                    mBooking.Exe_Result = 0;
                    mBooking.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoDoesNotExist.ToString();
                    return mBooking;
                }
            }
            catch (OdbcException ex)
            {
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
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
                    if (mDataReader["department"].ToString().Trim().ToLower() ==
                        AfyaPro_Types.clsEnums.PatientCategories.OPD.ToString().Trim().ToLower())
                    {
                        mBooking.Exe_Result = 0;
                        mBooking.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientIsNotAdmitted.ToString();
                        return mBooking;
                    }
                }
            }
            catch (OdbcException ex)
            {
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
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

                mBooking = this.Get_Booking(mPatientCode);
                clsWorkFlowAgent mWorkFlowAgent = new clsWorkFlowAgent();
               
                AfyaPro_Types.clsBooking mDischarge = mWorkFlowAgent.Do_Discharging(
                                        mCommand,
                                        mAdmissionId,
                                        mTransDate,
                                        mPatientCode,
                                        mFromWardCode,
                                        mFromRoomCode,
                                        mFromBedCode,
                                        mStatusCode,
                                        mRemarks,
                                        mWeight,
                                        mTemperature,
                                        mBooking,
                                        mUserId);

                if (mDischarge.Exe_Result != 1)
                {
                    try { mTrans.Rollback(); }
                    catch { }
                    return mBooking;
                }

                mTrans.Commit();

                //get booking that has been done
                mBooking = this.Get_Booking(mPatientCode);

             

                //return
                return mBooking;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mBooking.Exe_Result = -1;
                mBooking.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBooking;
            }
            finally
            {
                mConn.Close();
            }
        }
        #endregion
    }
}
