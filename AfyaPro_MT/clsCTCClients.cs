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
    public class clsCTCClients : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsCTCClients";

        #endregion

        #region View_Clients
        public DataTable View_Clients(String mFilter, String mOrder)
        {
            String mFunctionName = "View_Clients";

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

        #region View_Appointments
        public DataTable View_Appointments(String mFilter, String mOrder, string mGridName)
        {
            String mFunctionName = "View_Appointments";

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
                string mCommandText = "select * from view_ctc_appointments";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_ctc_appointments");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

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

        #region View_Appointments
        public DataTable View_Appointments(DateTime mStartDate, DateTime mEndDate, int mApptStatus, String mOrder, string mGridName)
        {
            String mFunctionName = "View_Appointments";

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
                mStartDate = mStartDate.Date;
                mEndDate = mEndDate.Date;

                //columns from patients
                string mPatientColumns = clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "patient");
                mPatientColumns = mPatientColumns + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "patientfullname");
                mPatientColumns = mPatientColumns + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "b.transdate", ""), "patientage");
                //columns from trans
                string mTransColumns = clsGlobal.Get_TableColumns(mCommand, "view_ctc_appointments", "", "b", "");

                string mCommandText = ""
                    + "SELECT "
                    + mTransColumns + ","
                    + mPatientColumns + " "
                    + "FROM view_ctc_appointments AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code WHERE "
                    + "b.apptdate BETWEEN " + clsGlobal.Saving_DateValue(mStartDate) + " AND " + clsGlobal.Saving_DateValue(mEndDate);

                if (mApptStatus != -1)
                {
                    mCommandText = mCommandText + " AND b.apptstatus=" + mApptStatus;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("ctc_appointments");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

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

        #region View_Measurements
        public DataTable View_Measurements(String mFilter, String mOrder, string mGridName)
        {
            String mFunctionName = "View_Measurements";

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
                string mCommandText = "select * from view_ctc_triage";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_ctc_triage");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

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

        #region View_CTCClients
        public DataTable View_CTCClients(String mFilter, String mOrder)
        {
            String mFunctionName = "View_CTCClients";

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
                string mCommandText = "select * from view_ctc_patients";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_ctc_patients");
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

        #region View_HIVTests
        public DataTable View_HIVTests(String mFilter, String mOrder, string mGridName)
        {
            String mFunctionName = "View_HIVTests";

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
                string mCommandText = "select * from view_ctc_hivtests";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_ctc_hivtests");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

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

        #region View_PCRTests
        public DataTable View_PCRTests(String mFilter, String mOrder, string mGridName)
        {
            String mFunctionName = "View_PCRTests";

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
                string mCommandText = "select * from view_ctc_pcrtests";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_ctc_pcrtests");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

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

        #region View_CD4Tests
        public DataTable View_CD4Tests(String mFilter, String mOrder, string mGridName)
        {
            String mFunctionName = "View_CD4Tests";

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
                string mCommandText = "select * from view_ctc_cd4tests";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_ctc_cd4tests");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

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

        #region View_PreventiveTherapies
        public DataTable View_PreventiveTherapies(String mFilter, String mOrder, string mGridName)
        {
            String mFunctionName = "View_PreventiveTherapies";

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
                string mCommandText = "select * from view_ctc_preventivetherapies";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_ctc_preventivetherapies");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

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

        //#region View_PMTCTAntenatal
        //public DataTable View_PMTCTAntenatal(String mFilter, String mOrder)
        //{
        //    String mFunctionName = "View_PMTCTAntenatal";

        //    OdbcConnection mConn = new OdbcConnection();
        //    OdbcCommand mCommand = new OdbcCommand();
        //    OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

        //    #region database connection

        //    try
        //    {
        //        mConn.ConnectionString = clsGlobal.gAfyaConStr;

        //        if (mConn.State != ConnectionState.Open)
        //        {
        //            mConn.Open();
        //        }

        //        mCommand.Connection = mConn;
        //    }
        //    catch (Exception ex)
        //    {
        //        clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
        //        return null;
        //    }

        //    #endregion

        //    try
        //    {
        //        string mCommandText = "select * from view_ctc_pmtctantenatal";

        //        if (mFilter.Trim() != "")
        //        {
        //            mCommandText = mCommandText + " where " + mFilter;
        //        }

        //        if (mOrder.Trim() != "")
        //        {
        //            mCommandText = mCommandText + " order by " + mOrder;
        //        }
        //        mCommand.CommandText = mCommandText;

        //        DataTable mDataTable = new DataTable("view_ctc_pmtctantenatal");
        //        mDataTable.RemotingFormat = SerializationFormat.Binary;
        //        mDataAdapter.SelectCommand = mCommand;
        //        mDataAdapter.Fill(mDataTable);

        //        return mDataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
        //        return null;
        //    }
        //    finally
        //    {
        //        mConn.Close();
        //    }
        //}
        //#endregion

        //#region View_PMTCTAntenatalVisits
        //public DataTable View_PMTCTAntenatalVisits(String mFilter, String mOrder, string mGridName)
        //{
        //    String mFunctionName = "View_PMTCTAntenatalVisits";

        //    OdbcConnection mConn = new OdbcConnection();
        //    OdbcCommand mCommand = new OdbcCommand();
        //    OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

        //    #region database connection

        //    try
        //    {
        //        mConn.ConnectionString = clsGlobal.gAfyaConStr;

        //        if (mConn.State != ConnectionState.Open)
        //        {
        //            mConn.Open();
        //        }

        //        mCommand.Connection = mConn;
        //    }
        //    catch (Exception ex)
        //    {
        //        clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
        //        return null;
        //    }

        //    #endregion

        //    try
        //    {
        //        string mCommandText = "select * from view_ctc_pmtctantenatallog";

        //        if (mFilter.Trim() != "")
        //        {
        //            mCommandText = mCommandText + " where " + mFilter;
        //        }

        //        if (mOrder.Trim() != "")
        //        {
        //            mCommandText = mCommandText + " order by " + mOrder;
        //        }
        //        mCommand.CommandText = mCommandText;

        //        DataTable mDataTable = new DataTable("view_ctc_pmtctantenatallog");
        //        mDataTable.RemotingFormat = SerializationFormat.Binary;
        //        mDataAdapter.SelectCommand = mCommand;
        //        mDataAdapter.Fill(mDataTable);

        //        //columnheaders
        //        clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

        //        return mDataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
        //        return null;
        //    }
        //    finally
        //    {
        //        mConn.Close();
        //    }
        //}
        //#endregion

        //#region View_PMTCTOtherChildren
        //public DataTable View_PMTCTOtherChildren(String mFilter, String mOrder, string mGridName)
        //{
        //    String mFunctionName = "View_PMTCTOtherChildren";

        //    OdbcConnection mConn = new OdbcConnection();
        //    OdbcCommand mCommand = new OdbcCommand();
        //    OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

        //    #region database connection

        //    try
        //    {
        //        mConn.ConnectionString = clsGlobal.gAfyaConStr;

        //        if (mConn.State != ConnectionState.Open)
        //        {
        //            mConn.Open();
        //        }

        //        mCommand.Connection = mConn;
        //    }
        //    catch (Exception ex)
        //    {
        //        clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
        //        return null;
        //    }

        //    #endregion

        //    try
        //    {
        //        string mCommandText = "select * from view_ctc_pmtctotherchildren";

        //        if (mFilter.Trim() != "")
        //        {
        //            mCommandText = mCommandText + " where " + mFilter;
        //        }

        //        if (mOrder.Trim() != "")
        //        {
        //            mCommandText = mCommandText + " order by " + mOrder;
        //        }
        //        mCommand.CommandText = mCommandText;

        //        DataTable mDataTable = new DataTable("view_ctc_pmtctotherchildren");
        //        mDataTable.RemotingFormat = SerializationFormat.Binary;
        //        mDataAdapter.SelectCommand = mCommand;
        //        mDataAdapter.Fill(mDataTable);

        //        //columnheaders
        //        clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

        //        return mDataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
        //        return null;
        //    }
        //    finally
        //    {
        //        mConn.Close();
        //    }
        //}
        //#endregion

        //#region View_PMTCTAntenatalOtherChildren
        //public DataTable View_PMTCTAntenatalOtherChildren(String mFilter, String mOrder, string mGridName)
        //{
        //    String mFunctionName = "View_PMTCTAntenatalOtherChildren";

        //    OdbcConnection mConn = new OdbcConnection();
        //    OdbcCommand mCommand = new OdbcCommand();
        //    OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

        //    #region database connection

        //    try
        //    {
        //        mConn.ConnectionString = clsGlobal.gAfyaConStr;

        //        if (mConn.State != ConnectionState.Open)
        //        {
        //            mConn.Open();
        //        }

        //        mCommand.Connection = mConn;
        //    }
        //    catch (Exception ex)
        //    {
        //        clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
        //        return null;
        //    }

        //    #endregion

        //    try
        //    {
        //        string mCommandText = "select * from view_ctc_pmtctantenatalotherchildren";

        //        if (mFilter.Trim() != "")
        //        {
        //            mCommandText = mCommandText + " where " + mFilter;
        //        }

        //        if (mOrder.Trim() != "")
        //        {
        //            mCommandText = mCommandText + " order by " + mOrder;
        //        }
        //        mCommand.CommandText = mCommandText;

        //        DataTable mDataTable = new DataTable("view_ctc_pmtctantenatalotherchildren");
        //        mDataTable.RemotingFormat = SerializationFormat.Binary;
        //        mDataAdapter.SelectCommand = mCommand;
        //        mDataAdapter.Fill(mDataTable);

        //        //columnheaders
        //        clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

        //        return mDataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
        //        return null;
        //    }
        //    finally
        //    {
        //        mConn.Close();
        //    }
        //}
        //#endregion

        //#region View_PMTCTPostnatalOtherChildren
        //public DataTable View_PMTCTPostnatalOtherChildren(String mFilter, String mOrder, string mGridName)
        //{
        //    String mFunctionName = "View_PMTCTPostnatalOtherChildren";

        //    OdbcConnection mConn = new OdbcConnection();
        //    OdbcCommand mCommand = new OdbcCommand();
        //    OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

        //    #region database connection

        //    try
        //    {
        //        mConn.ConnectionString = clsGlobal.gAfyaConStr;

        //        if (mConn.State != ConnectionState.Open)
        //        {
        //            mConn.Open();
        //        }

        //        mCommand.Connection = mConn;
        //    }
        //    catch (Exception ex)
        //    {
        //        clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
        //        return null;
        //    }

        //    #endregion

        //    try
        //    {
        //        string mCommandText = "select * from view_ctc_pmtctpostnatalotherchildren";

        //        if (mFilter.Trim() != "")
        //        {
        //            mCommandText = mCommandText + " where " + mFilter;
        //        }

        //        if (mOrder.Trim() != "")
        //        {
        //            mCommandText = mCommandText + " order by " + mOrder;
        //        }
        //        mCommand.CommandText = mCommandText;

        //        DataTable mDataTable = new DataTable("view_ctc_pmtctpostnatalotherchildren");
        //        mDataTable.RemotingFormat = SerializationFormat.Binary;
        //        mDataAdapter.SelectCommand = mCommand;
        //        mDataAdapter.Fill(mDataTable);

        //        //columnheaders
        //        clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

        //        return mDataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
        //        return null;
        //    }
        //    finally
        //    {
        //        mConn.Close();
        //    }
        //}
        //#endregion

        //#region View_PMTCTPostnatal
        //public DataTable View_PMTCTPostnatal(String mFilter, String mOrder)
        //{
        //    String mFunctionName = "View_PMTCTPostnatal";

        //    OdbcConnection mConn = new OdbcConnection();
        //    OdbcCommand mCommand = new OdbcCommand();
        //    OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

        //    #region database connection

        //    try
        //    {
        //        mConn.ConnectionString = clsGlobal.gAfyaConStr;

        //        if (mConn.State != ConnectionState.Open)
        //        {
        //            mConn.Open();
        //        }

        //        mCommand.Connection = mConn;
        //    }
        //    catch (Exception ex)
        //    {
        //        clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
        //        return null;
        //    }

        //    #endregion

        //    try
        //    {
        //        string mCommandText = "select * from view_ctc_pmtctpostnatal";

        //        if (mFilter.Trim() != "")
        //        {
        //            mCommandText = mCommandText + " where " + mFilter;
        //        }

        //        if (mOrder.Trim() != "")
        //        {
        //            mCommandText = mCommandText + " order by " + mOrder;
        //        }
        //        mCommand.CommandText = mCommandText;

        //        DataTable mDataTable = new DataTable("view_ctc_pmtctpostnatal");
        //        mDataTable.RemotingFormat = SerializationFormat.Binary;
        //        mDataAdapter.SelectCommand = mCommand;
        //        mDataAdapter.Fill(mDataTable);

        //        return mDataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
        //        return null;
        //    }
        //    finally
        //    {
        //        mConn.Close();
        //    }
        //}
        //#endregion

        //#region View_PMTCTPostnatalVisits
        //public DataTable View_PMTCTPostnatalVisits(String mFilter, String mOrder, string mGridName)
        //{
        //    String mFunctionName = "View_PMTCTPostnatalVisits";

        //    OdbcConnection mConn = new OdbcConnection();
        //    OdbcCommand mCommand = new OdbcCommand();
        //    OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

        //    #region database connection

        //    try
        //    {
        //        mConn.ConnectionString = clsGlobal.gAfyaConStr;

        //        if (mConn.State != ConnectionState.Open)
        //        {
        //            mConn.Open();
        //        }

        //        mCommand.Connection = mConn;
        //    }
        //    catch (Exception ex)
        //    {
        //        clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
        //        return null;
        //    }

        //    #endregion

        //    try
        //    {
        //        string mCommandText = "select * from view_ctc_pmtctpostnatallog";

        //        if (mFilter.Trim() != "")
        //        {
        //            mCommandText = mCommandText + " where " + mFilter;
        //        }

        //        if (mOrder.Trim() != "")
        //        {
        //            mCommandText = mCommandText + " order by " + mOrder;
        //        }
        //        mCommand.CommandText = mCommandText;

        //        DataTable mDataTable = new DataTable("view_ctc_pmtctpostnatallog");
        //        mDataTable.RemotingFormat = SerializationFormat.Binary;
        //        mDataAdapter.SelectCommand = mCommand;
        //        mDataAdapter.Fill(mDataTable);

        //        //columnheaders
        //        clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

        //        return mDataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
        //        return null;
        //    }
        //    finally
        //    {
        //        mConn.Close();
        //    }
        //}
        //#endregion

        #region View_PreART
        public DataTable View_PreART(string mFilter, string mOrder, string mGridName)
        {
            String mFunctionName = "View_PreART";

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
                string mCommandText = "select * from view_ctc_preart";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_ctc_preart");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

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

        #region View_PreARTVisits
        public DataTable View_PreARTVisits(string mFilter, string mOrder, string mGridName)
        {
            String mFunctionName = "View_PreARTVisits";

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
                string mCommandText = "select * from view_ctc_preartlog";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_ctc_preartlog");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

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

        #region View_ART
        public DataTable View_ART(string mFilter, string mOrder, string mGridName)
        {
            String mFunctionName = "View_ART";

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
                string mCommandText = "select * from view_ctc_art";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_ctc_art");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

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
               
        #region View_ARTVisits
        public DataTable View_ARTVisits(string mFilter, string mOrder, string mGridName)
        {
            String mFunctionName = "View_ARTVisits";

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
                string mCommandText = "select * from view_ctc_artlog";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_ctc_artlog");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

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

        #region View_ARTT
        public DataTable View_ARTT(string mFilter, string mOrder, string mGridName)
        {
            String mFunctionName = "View_ARTT";

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
                string mCommandText = "select * from view_ctc_artt";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_ctc_artt");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

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

        #region View_ARTTVisits
        public DataTable View_ARTTVisits(string mFilter, string mOrder, string mGridName)
        {
            String mFunctionName = "View_ARTTVisits";

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
                string mCommandText = "select * from view_ctc_arttlog";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_ctc_arttlog");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

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

        #region View_ARTP
        public DataTable View_ARTP(string mFilter, string mOrder, string mGridName)
        {
            String mFunctionName = "View_ARTP";

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
                string mCommandText = "select * from view_ctc_artp";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_ctc_artp");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

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

        #region View_ARTPVisits
        public DataTable View_ARTPVisits(string mFilter, string mOrder, string mGridName)
        {
            String mFunctionName = "View_ARTPVisits";

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
                string mCommandText = "select * from view_ctc_artplog";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_ctc_artplog");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

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

        #region View_Exposed
        public DataTable View_Exposed(string mFilter, string mOrder, string mGridName)
        {
            String mFunctionName = "View_Exposed";

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
                string mCommandText = "select * from view_ctc_exposed";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_ctc_exposed");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

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

        #region View_ExposedVisits
        public DataTable View_ExposedVisits(string mFilter, string mOrder, string mGridName)
        {
            String mFunctionName = "View_ExposedVisits";

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
                string mCommandText = "select * from view_ctc_exposedlog";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_ctc_exposedlog");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

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

        #region View_PMTCT
        public DataTable View_PMTCT(string mFilter, string mOrder, string mGridName)
        {
            String mFunctionName = "View_PMTCT";

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
                string mCommandText = "select * from view_ctc_pmtct";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_ctc_pmtct");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

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

        #region View_PMTCTCareVisits
        public DataTable View_PMTCTCareVisits(string mFilter, string mOrder, string mGridName)
        {
            String mFunctionName = "View_PMTCTCareVisits";

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
                string mCommandText = "select * from view_ctc_pmtctcarevisits";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_ctc_pmtctcarevisits");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

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

        #region View_PMTCTLabourAndDelivery
        public DataTable View_PMTCTLabourAndDelivery(string mFilter, string mOrder, string mGridName)
        {
            String mFunctionName = "View_PMTCTLabourAndDelivery";

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
                string mCommandText = "select * from view_ctc_pmtctlabouranddelivery";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_ctc_pmtctlabouranddelivery");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

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

        #region View_PMTCTMotherChildVisits
        public DataTable View_PMTCTMotherChildVisits(string mFilter, string mOrder, string mGridName)
        {
            String mFunctionName = "View_PMTCTMotherChildVisits";

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
                string mCommandText = "select * from view_ctc_pmtctmotherchildvisits";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_ctc_pmtctmotherchildvisits");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName);

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

        #region Get_Booking
        public AfyaPro_Types.ctcBooking Get_Booking(OdbcCommand mCommand, string mPatientCode)
        {
            string mFunctionName = "Get_Booking";

            AfyaPro_Types.ctcBooking mBooking = new AfyaPro_Types.ctcBooking();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            #region get booking details
            try
            {
                DataTable mDtBookings = new DataTable("bookings");
                mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtBookings);
                if (mDtBookings.Rows.Count > 0)
                {
                    mBooking.IsBooked = true;
                    mBooking.PatientCode = mDtBookings.Rows[0]["patientcode"].ToString().Trim();
                    mBooking.WhereTakenCode = mDtBookings.Rows[0]["wheretakencode"].ToString().Trim();
                    mBooking.BookDate = Convert.ToDateTime(mDtBookings.Rows[0]["bookdate"]);
                    mBooking.Booking = mDtBookings.Rows[0]["autocode"].ToString();
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

        #region Get_Booking
        public AfyaPro_Types.ctcBooking Get_Booking(string mPatientCode)
        {
            string mFunctionName = "Get_Booking";

            AfyaPro_Types.clsCtcClient mClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            AfyaPro_Types.ctcBooking mBooking = new AfyaPro_Types.ctcBooking();

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

            #region get booking details
            try
            {
                DataTable mDtBookings = new DataTable("bookings");
                mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtBookings);
                if (mDtBookings.Rows.Count > 0)
                {
                    mBooking.IsBooked = true;
                    mBooking.PatientCode = mDtBookings.Rows[0]["patientcode"].ToString().Trim();
                    mBooking.WhereTakenCode = mDtBookings.Rows[0]["wheretakencode"].ToString().Trim();
                    mBooking.BookDate = Convert.ToDateTime(mDtBookings.Rows[0]["bookdate"]);
                    mBooking.Booking = mDtBookings.Rows[0]["autocode"].ToString();
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

        #region Get_Client
        public AfyaPro_Types.clsCtcClient Get_Client(string mFieldName, string mFieldValue)
        {
            string mFunctionName = "Get_Client";

            AfyaPro_Types.clsCtcClient mClient = new AfyaPro_Types.clsCtcClient();
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
                DataTable mDtClients = new DataTable("patients");
                mCommand.CommandText = "select p.*,c.hivno,c.ctcno,c.hivtestno,c.arvno,"
                + "c.guardianname,c.patientphone,c.guardianphone,c.guardianrelation,c.agreetofup from patients p "
                + "left outer join view_ctc_patients c on p.code=c.patientcode where " 
                + mFieldName.Trim() + "='" + mFieldValue.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtClients);

                if (mDtClients.Rows.Count > 0)
                {
                    mClient.Exist = true;
                    mClient.code = mDtClients.Rows[0]["code"].ToString();
                    mClient.surname = mDtClients.Rows[0]["surname"].ToString();
                    mClient.firstname = mDtClients.Rows[0]["firstname"].ToString();
                    mClient.othernames = mDtClients.Rows[0]["othernames"].ToString();
                    mClient.gender = mDtClients.Rows[0]["gender"].ToString();
                    mClient.birthdate = mDtClients.Rows[0]["birthdate"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(mDtClients.Rows[0]["birthdate"]);
                    mClient.regdate = Convert.ToDateTime(mDtClients.Rows[0]["regdate"]);
                    mClient.maritalstatuscode = mDtClients.Rows[0]["maritalstatuscode"].ToString();
                    mClient.allergies = mDtClients.Rows[0]["allergies"].ToString();

                    mClient.guardianname = mDtClients.Rows[0]["guardianname"] == DBNull.Value ? "" : mDtClients.Rows[0]["guardianname"].ToString().Trim();
                    mClient.patientphone = mDtClients.Rows[0]["patientphone"] == DBNull.Value ? "" : mDtClients.Rows[0]["patientphone"].ToString().Trim();
                    mClient.guardianphone = mDtClients.Rows[0]["guardianphone"] == DBNull.Value ? "" : mDtClients.Rows[0]["guardianphone"].ToString().Trim();
                    mClient.guardianrelation = mDtClients.Rows[0]["guardianrelation"] == DBNull.Value ? "" : mDtClients.Rows[0]["guardianrelation"].ToString().Trim();
                    mClient.agreetofup = mDtClients.Rows[0]["agreetofup"] == DBNull.Value ? 0 : Convert.ToInt32(mDtClients.Rows[0]["agreetofup"]);

                    mClient.hivno = mDtClients.Rows[0]["hivno"] == DBNull.Value ? "" : mDtClients.Rows[0]["hivno"].ToString().Trim();
                    mClient.ctcno = mDtClients.Rows[0]["ctcno"] == DBNull.Value ? "" : mDtClients.Rows[0]["ctcno"].ToString().Trim();
                    mClient.hivtestno = mDtClients.Rows[0]["hivtestno"] == DBNull.Value ? "" : mDtClients.Rows[0]["hivtestno"].ToString().Trim();
                    mClient.arvno = mDtClients.Rows[0]["arvno"] == DBNull.Value ? "" : mDtClients.Rows[0]["arvno"].ToString().Trim();
                }
                else
                {
                    mClient.Exe_Result = 0;
                    mClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.CTC_ClientCodeDoesNotExist.ToString();
                    return mClient;
                }

                return mClient;
            }
            catch (OdbcException ex)
            {
                mClient.Exe_Result = -1;
                mClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mClient;
            }
        }
        #endregion

        #region Add_Client
        public AfyaPro_Types.clsCtcClient Add_Client(
            Int16 mGenerateCode, string mCode,
            Int16 mGenerateHIVTestCode, string mHIVTestCode,
            string mSurname, string mFirstName,
            string mOtherNames, string mGender, DateTime mBirthDate, DateTime mRegDate, string mMaritalStatusCode, 
            string mAllergies, DataTable mDtExtraDetails, DateTime mTransDate, string mGuardianName,
            string mPatientPhone, string mGuardianPhone, string mGuardianRelation, int mAgreeToFUP,
            string mMachineName, string mMachineUser, string mUserId)
        {
            String mFunctionName = "Add_Client";

            AfyaPro_Types.clsCtcClient mClient = new AfyaPro_Types.clsCtcClient();
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
                mClient.Exe_Result = -1;
                mClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcclients_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mClient.Exe_Result = 0;
                mClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mClient;
            }
            #endregion

            #region client #

            #region auto generate code, if option is on

            if (mGenerateCode == 1)
            {
                clsAutoCodes objAutoCodes = new clsAutoCodes();
                AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                mObjCode = objAutoCodes.Next_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.vctnumbers), "patients", "code");
                if (mObjCode.Exe_Result == -1)
                {
                    mClient.Exe_Result = mObjCode.Exe_Result;
                    mClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                    return mClient;
                }
                mCode = mObjCode.GeneratedCode;
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
                    mClient.Exe_Result = 0;
                    mClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.CTC_ClientCodeIsInUse.ToString();
                    return mClient;
                }
            }
            catch (OdbcException ex)
            {
                mClient.Exe_Result = -1;
                mClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mClient;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion

            #endregion

            #region hivtest #

            #region auto generate code, if option is on

            if (mGenerateHIVTestCode == 1)
            {
                clsAutoCodes objAutoCodes = new clsAutoCodes();
                AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                mObjCode = objAutoCodes.Next_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctchivtestnumber), "ctc_patients", "hivtestno");
                if (mObjCode.Exe_Result == -1)
                {
                    mClient.Exe_Result = mObjCode.Exe_Result;
                    mClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                    return mClient;
                }
                mHIVTestCode = mObjCode.GeneratedCode;
            }

            #endregion

            #region check 4 duplicate

            if (mHIVTestCode.Trim() != "")
            {
                try
                {
                    mCommand.CommandText = "select * from ctc_patients where hivtestno='"
                    + mHIVTestCode.Trim() + "'";
                    mDataReader = mCommand.ExecuteReader();
                    if (mDataReader.Read() == true)
                    {
                        mClient.Exe_Result = 0;
                        mClient.Exe_Message = "Duplicate HIV Test Serial # is not allowed";
                        return mClient;
                    }
                }
                catch (OdbcException ex)
                {
                    mClient.Exe_Result = -1;
                    mClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                    return mClient;
                }
                finally
                {
                    mDataReader.Close();
                }
            }

            #endregion

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

                #region patients

                mCommand.CommandText = "insert into patients(code,surname,firstname,othernames,gender,birthdate"
                + ",regdate,maritalstatuscode,allergies" + mExtraDetailFields + ") values('" + mCode.Trim() + "','"
                + mSurname.Trim() + "','" + mFirstName.Trim() + "','" + mOtherNames.Trim() + "','" + mGender + "',"
                + clsGlobal.Saving_DateValueNullable(mBirthDate) + "," + clsGlobal.Saving_DateValue(mRegDate) 
                + ",'" + mMaritalStatusCode.Trim() + "','" + mAllergies + "'" + mExtraDetailValues + ")";
                mCommand.ExecuteNonQuery();

                #endregion

                #region ctc_patients

                DataTable mDtPatients = new DataTable("patients");
                mCommand.CommandText = "select * from ctc_patients where patientcode='" + mCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtPatients);

                if (mDtPatients.Rows.Count > 0)
                {
                    List<clsDataField> mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("hivtestno", DataTypes.dbstring.ToString(), mHIVTestCode.Trim()));
                    mDataFields.Add(new clsDataField("guardianname", DataTypes.dbstring.ToString(), mGuardianName.Trim()));
                    mDataFields.Add(new clsDataField("patientphone", DataTypes.dbstring.ToString(), mPatientPhone.Trim()));
                    mDataFields.Add(new clsDataField("guardianphone", DataTypes.dbstring.ToString(), mGuardianPhone.Trim()));
                    mDataFields.Add(new clsDataField("guardianrelation", DataTypes.dbstring.ToString(), mGuardianRelation.Trim()));
                    mDataFields.Add(new clsDataField("agreetofup", DataTypes.dbnumber.ToString(), mAgreeToFUP));

                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_patients", mDataFields, "patientcode='" + mCode.Trim() + "'");
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    List<clsDataField> mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mCode.Trim()));
                    mDataFields.Add(new clsDataField("hivtestno", DataTypes.dbstring.ToString(), mHIVTestCode.Trim()));
                    mDataFields.Add(new clsDataField("guardianname", DataTypes.dbstring.ToString(), mGuardianName.Trim()));
                    mDataFields.Add(new clsDataField("patientphone", DataTypes.dbstring.ToString(), mPatientPhone.Trim()));
                    mDataFields.Add(new clsDataField("guardianphone", DataTypes.dbstring.ToString(), mGuardianPhone.Trim()));
                    mDataFields.Add(new clsDataField("guardianrelation", DataTypes.dbstring.ToString(), mGuardianRelation.Trim()));
                    mDataFields.Add(new clsDataField("agreetofup", DataTypes.dbnumber.ToString(), mAgreeToFUP));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patients", mDataFields);
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

                //guardianrelation
                if (mGuardianRelation.Trim() != "")
                {
                    DataTable mDtGuardianRelations = new DataTable("guardianrelation");
                    mCommand.CommandText = "select * from ctc_guardianrelations where description='" + mGuardianRelation.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtGuardianRelations);
                    if (mDtGuardianRelations.Rows.Count == 0)
                    {
                        mCommand.CommandText = "insert into ctc_guardianrelations(description) values('" + mGuardianRelation.Trim() + "')";
                        mCommand.ExecuteNonQuery();
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

                //this patient has to be available when doing searching
                mCommand.CommandText = "update patients set fromctc=1 where code='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                if (mGenerateCode == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.vctnumbers);
                    mCommand.ExecuteNonQuery();
                }

                if (mGenerateHIVTestCode == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctchivtestnumber);
                    mCommand.ExecuteNonQuery();
                }

                //commit
                mTrans.Commit();

                //get patient
                mClient = this.Get_Client("code", mCode);

                //return
                return mClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mClient.Exe_Result = -1;
                mClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Edit_Client
        public AfyaPro_Types.clsCtcClient Edit_Client(string mCode, string mSurname, string mFirstName,
            string mOtherNames, string mGender, DateTime mBirthDate, DateTime mRegDate, string mMaritalStatusCode, 
            string mAllergies, DataTable mDtExtraDetails, DateTime mTransDate, string mGuardianName,
            string mPatientPhone, string mGuardianPhone, string mGuardianRelation, int mAgreeToFUP,
            string mHIVSerialNo, string mHIVNo, string mARVNo, string mCTCNo,
            string mMachineName, string mMachineUser, string mUserId)
        {
            String mFunctionName = "Edit_Client";

            AfyaPro_Types.clsCtcClient mClient = new AfyaPro_Types.clsCtcClient();
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
                mClient.Exe_Result = -1;
                mClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcclients_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mClient.Exe_Result = 0;
                mClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mClient;
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
                    mClient.Exe_Result = 0;
                    mClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.CTC_ClientCodeDoesNotExist.ToString();
                    return mClient;
                }
            }
            catch (OdbcException ex)
            {
                mClient.Exe_Result = -1;
                mClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mClient;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion

            #region edit
            try
            {

                #region avoid duplicate numbers

                DataTable mDtTesting = null;

                #region hivtestno

                if (mHIVSerialNo.Trim() != "")
                {
                    mDtTesting = new DataTable("testing");
                    mCommand.CommandText = "select * from ctc_patients where hivtestno='" + mHIVSerialNo.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtTesting);

                    if (mDtTesting.Rows.Count > 0)
                    {
                        if (mDtTesting.Rows[0]["patientcode"].ToString().Trim().ToLower() != mCode.Trim().ToLower())
                        {
                            mClient.Exe_Result = 0;
                            mClient.Exe_Message = "HIV Test Serial # is in use by another client";
                            return mClient;
                        }
                    }
                }

                #endregion

                #region hivno

                if (mHIVNo.Trim() != "")
                {
                    mDtTesting = new DataTable("testing");
                    mCommand.CommandText = "select * from ctc_patients where hivno='" + mHIVNo.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtTesting);

                    if (mDtTesting.Rows.Count > 0)
                    {
                        if (mDtTesting.Rows[0]["patientcode"].ToString().Trim().ToLower() != mCode.Trim().ToLower())
                        {
                            mClient.Exe_Result = 0;
                            mClient.Exe_Message = "HIV # is in use by another client";
                            return mClient;
                        }
                    }
                }

                #endregion

                #region arvno

                if (mARVNo.Trim() != "")
                {
                    mDtTesting = new DataTable("testing");
                    mCommand.CommandText = "select * from ctc_patients where arvno='" + mARVNo.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtTesting);

                    if (mDtTesting.Rows.Count > 0)
                    {
                        if (mDtTesting.Rows[0]["patientcode"].ToString().Trim().ToLower() != mCode.Trim().ToLower())
                        {
                            mClient.Exe_Result = 0;
                            mClient.Exe_Message = "ARV Test Serial # is in use by another client";
                            return mClient;
                        }
                    }
                }

                #endregion

                #region ctcno

                if (mCTCNo.Trim() != "")
                {
                    mDtTesting = new DataTable("testing");
                    mCommand.CommandText = "select * from ctc_patients where ctcno='" + mCTCNo.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtTesting);

                    if (mDtTesting.Rows.Count > 0)
                    {
                        if (mDtTesting.Rows[0]["patientcode"].ToString().Trim().ToLower() != mCode.Trim().ToLower())
                        {
                            mClient.Exe_Result = 0;
                            mClient.Exe_Message = "CTC/ART # is in use by another client";
                            return mClient;
                        }
                    }
                }

                #endregion

                #endregion

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

                #region patients

                mCommand.CommandText = "update patients set surname='" + mSurname.Trim() + "',firstname='" + mFirstName.Trim()
                + "',othernames='" + mOtherNames.Trim() + "',gender='" + mGender + "',birthdate="
                + clsGlobal.Saving_DateValueNullable(mBirthDate) + ",regdate=" + clsGlobal.Saving_DateValue(mRegDate)
                + ",maritalstatuscode='" + mMaritalStatusCode.Trim() + "',allergies='" + mAllergies + "'"
                + mUpdateExtraDetails + " where code='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                #endregion

                #region ctc_patients

                DataTable mDtPatients = new DataTable("patients");
                mCommand.CommandText = "select * from ctc_patients where patientcode='" + mCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtPatients);

                if (mDtPatients.Rows.Count > 0)
                {
                    List<clsDataField> mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("guardianname", DataTypes.dbstring.ToString(), mGuardianName.Trim()));
                    mDataFields.Add(new clsDataField("patientphone", DataTypes.dbstring.ToString(), mPatientPhone.Trim()));
                    mDataFields.Add(new clsDataField("guardianphone", DataTypes.dbstring.ToString(), mGuardianPhone.Trim()));
                    mDataFields.Add(new clsDataField("guardianrelation", DataTypes.dbstring.ToString(), mGuardianRelation.Trim()));
                    mDataFields.Add(new clsDataField("agreetofup", DataTypes.dbnumber.ToString(), mAgreeToFUP));
                    mDataFields.Add(new clsDataField("hivtestno", DataTypes.dbstring.ToString(), mHIVSerialNo.Trim()));
                    mDataFields.Add(new clsDataField("hivno", DataTypes.dbstring.ToString(), mHIVNo.Trim()));
                    mDataFields.Add(new clsDataField("arvno", DataTypes.dbstring.ToString(), mARVNo.Trim()));
                    mDataFields.Add(new clsDataField("ctcno", DataTypes.dbstring.ToString(), mCTCNo.Trim()));

                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_patients", mDataFields, "patientcode='" + mCode.Trim() + "'");
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    List<clsDataField> mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mCode.Trim()));
                    mDataFields.Add(new clsDataField("guardianname", DataTypes.dbstring.ToString(), mGuardianName.Trim()));
                    mDataFields.Add(new clsDataField("patientphone", DataTypes.dbstring.ToString(), mPatientPhone.Trim()));
                    mDataFields.Add(new clsDataField("guardianphone", DataTypes.dbstring.ToString(), mGuardianPhone.Trim()));
                    mDataFields.Add(new clsDataField("guardianrelation", DataTypes.dbstring.ToString(), mGuardianRelation.Trim()));
                    mDataFields.Add(new clsDataField("agreetofup", DataTypes.dbnumber.ToString(), mAgreeToFUP));
                    mDataFields.Add(new clsDataField("hivtestno", DataTypes.dbstring.ToString(), mHIVSerialNo.Trim()));
                    mDataFields.Add(new clsDataField("hivno", DataTypes.dbstring.ToString(), mHIVNo.Trim()));
                    mDataFields.Add(new clsDataField("arvno", DataTypes.dbstring.ToString(), mARVNo.Trim()));
                    mDataFields.Add(new clsDataField("ctcno", DataTypes.dbstring.ToString(), mCTCNo.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patients", mDataFields);
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

                //guardianrelation
                if (mGuardianRelation.Trim() != "")
                {
                    DataTable mDtGuardianRelations = new DataTable("guardianrelation");
                    mCommand.CommandText = "select * from ctc_guardianrelations where description='" + mGuardianRelation.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtGuardianRelations);
                    if (mDtGuardianRelations.Rows.Count == 0)
                    {
                        mCommand.CommandText = "insert into ctc_guardianrelations(description) values('" + mGuardianRelation.Trim() + "')";
                        mCommand.ExecuteNonQuery();
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

                //this patient has to be available when doing searching
                mCommand.CommandText = "update patients set fromctc=1 where code='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                //commit
                mTrans.Commit();

                //get patient
                mClient = this.Get_Client("code", mCode);

                //return
                return mClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mClient.Exe_Result = -1;
                mClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Booking
        public AfyaPro_Types.ctcBooking Booking(DateTime mTransDate, string mPatientCode,
            int mApptCode, string mWhereTakenCode, string mUserId)
        {
            String mFunctionName = "Booking";

            AfyaPro_Types.ctcBooking mBooking = new AfyaPro_Types.ctcBooking();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            DataTable mDtBookings = new DataTable("bookings");
            string mNewBooking = "";
            string mPrevBooking = "";
            DateTime mPrevBookDate = new DateTime();
            bool mIsReAttendance = false;
            bool mIsForcedReAttendance = false;

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

            #region check for reattendance

            try
            {
                mDtBookings = new DataTable("bookings");
                mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtBookings);

                if (mDtBookings.Rows.Count > 0)
                {
                    mPrevBooking = mDtBookings.Rows[0]["autocode"].ToString();
                    mPrevBookDate = Convert.ToDateTime(mDtBookings.Rows[0]["bookdate"]);
                    mIsReAttendance = true;
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
                    mCommand.CommandText = "select * from ctc_revisitingnumbers where patientcode='" + mPatientCode.Trim() + "'";
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

            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                if (mApptCode == 0)
                {
                    DataTable mDtAppts = new DataTable("appts");
                    mCommand.CommandText = "select * from ctc_appointments where patientcode='" + mPatientCode + "' "
                        + "and apptdate=" + clsGlobal.Saving_DateValue(mTransDate) + " and apptstatus=" + (int)AfyaPro_Types.clsEnums.CTC_ApptStatus.New;
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAppts);

                    if (mDtAppts.Rows.Count > 0)
                    {
                        mApptCode = Convert.ToInt32(mDtAppts.Rows[0]["autocode"]);
                    }
                }

                if (mIsReAttendance == true || mIsForcedReAttendance == true)
                {
                    if (mPrevBookDate == mTransDate)
                    {
                        #region booking update

                        //ctc_bookings
                        mCommand.CommandText = "update ctc_bookings set wheretakencode='" + mWhereTakenCode.Trim() 
                        + "' where autocode='" + mPrevBooking.Trim() + "'";
                        mCommand.ExecuteNonQuery();

                        //ctc_bookinglog
                        mCommand.CommandText = "update ctc_bookinglog set wheretakencode='" + mWhereTakenCode.Trim() 
                        + "' where booking='" + mPrevBooking.Trim() + "'";
                        mCommand.ExecuteNonQuery();

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
                                Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcbookingno), "ctc_bookinglog", "booking");
                            if (mObjCode.Exe_Result == -1)
                            {
                                mBooking.Exe_Result = 0;
                                mBooking.Exe_Message = "Duplicate booking number detected";
                                return mBooking;
                            }
                            mNewBooking = mObjCode.GeneratedCode;

                            //ctc_bookings
                            mCommand.CommandText = "insert into ctc_bookings(autocode,sysdate,bookdate,patientcode,wheretakencode,"
                            + "userid) values('" + mNewBooking + "'," + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                            + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mPatientCode.Trim() + "','" + mWhereTakenCode.Trim() + "','"
                            + mUserId.Trim() + "')";
                            mCommand.ExecuteNonQuery();

                            //ctc_bookinglog
                            mCommand.CommandText = "insert into ctc_bookinglog(sysdate,bookdate,patientcode,booking,wheretakencode,"
                            + "registrystatus,userid) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                            + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mPatientCode.Trim() + "','" + mNewBooking + "','"
                            + mWhereTakenCode.Trim() + "','" + AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString() + "','"
                            + mUserId.Trim() + "')";
                            mCommand.ExecuteNonQuery();

                            //prepare next booking #
                            mCommand.CommandText = "update facilityautocodes set "
                            + "idcurrent=idcurrent+idincrement where codekey="
                            + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcbookingno);
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
                                Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcbookingno), "ctc_bookinglog", "booking");
                            if (mObjCode.Exe_Result == -1)
                            {
                                mBooking.Exe_Result = 0;
                                mBooking.Exe_Message = "Duplicate booking number detected";
                                return mBooking;
                            }
                            mNewBooking = mObjCode.GeneratedCode;

                            //delete previous booking
                            mCommand.CommandText = "delete from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                            mCommand.ExecuteNonQuery();

                            //ctc_bookings
                            mCommand.CommandText = "insert into ctc_bookings(autocode,sysdate,bookdate,patientcode,wheretakencode,"
                            + "userid) values('" + mNewBooking + "'," + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                            + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mPatientCode.Trim() + "','" + mWhereTakenCode.Trim() + "','"
                            + mUserId.Trim() + "')";
                            mCommand.ExecuteNonQuery();

                            //ctc_bookinglog
                            mCommand.CommandText = "insert into ctc_bookinglog(sysdate,bookdate,patientcode,booking,wheretakencode,"
                            + "registrystatus,userid) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                            + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mPatientCode.Trim() + "','" + mNewBooking + "','"
                            + mWhereTakenCode.Trim() + "','" + AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString() + "','"
                            + mUserId.Trim() + "')";
                            mCommand.ExecuteNonQuery();

                            //prepare next booking #
                            mCommand.CommandText = "update facilityautocodes set "
                            + "idcurrent=idcurrent+idincrement where codekey="
                            + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcbookingno);
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
                        Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcbookingno), "ctc_bookinglog", "booking");
                    if (mObjCode.Exe_Result == -1)
                    {
                        mBooking.Exe_Result = 0;
                        mBooking.Exe_Message = "Duplicate booking number detected";
                        return mBooking;
                    }
                    mNewBooking = mObjCode.GeneratedCode;

                    //ctc_bookings
                    mCommand.CommandText = "insert into ctc_bookings(autocode,sysdate,bookdate,patientcode,wheretakencode,"
                    + "userid) values('" + mNewBooking + "'," + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                    + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mPatientCode.Trim() + "','" + mWhereTakenCode.Trim() + "','"
                    + mUserId.Trim() + "')";
                    mCommand.ExecuteNonQuery();

                    //ctc_bookinglog
                    mCommand.CommandText = "insert into ctc_bookinglog(sysdate,bookdate,patientcode,booking,wheretakencode,"
                    + "registrystatus,userid) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                    + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mPatientCode.Trim() + "','" + mNewBooking + "','"
                    + mWhereTakenCode.Trim() + "','" + AfyaPro_Types.clsEnums.RegistryStatus.New.ToString() + "','"
                    + mUserId.Trim() + "')";
                    mCommand.ExecuteNonQuery();

                    //prepare next booking #
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcbookingno);
                    mCommand.ExecuteNonQuery();

                    #endregion
                }

                #region book to hospital services also

                string mWhereTaken = "";
                DataTable mDtTreatmentPoints = new DataTable("treatmentpoints");
                mCommand.CommandText = "select * from facilitytreatmentpoints where code='" + mWhereTakenCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtTreatmentPoints);
                if (mDtTreatmentPoints.Rows.Count > 0)
                {
                    mWhereTaken = mDtTreatmentPoints.Rows[0]["description"].ToString().Trim();
                }

                clsWorkFlowAgent mWorkFlowAgent = new clsWorkFlowAgent();

                AfyaPro_Types.clsBooking mCtcBooking = mWorkFlowAgent.Do_Booking(
                    mCommand,
                    mTransDate,
                    mPatientCode,
                    "",
                    "",
                    "",
                    "price1",
                    mWhereTakenCode.Trim(),
                    mWhereTaken,
                    0,
                    0,
                    false,
                    mUserId);

                #endregion

                //update appointment status if any
                if (mApptCode > 0)
                {
                    mCommand.CommandText = "update ctc_appointments set apptstatus=" + (int)AfyaPro_Types.clsEnums.CTC_ApptStatus.Met
                        + ", attdate=" + clsGlobal.Saving_DateValue(mTransDate) + " where autocode=" + mApptCode;
                    mCommand.ExecuteNonQuery();
                }

                //this patient has to be available when doing searching
                mCommand.CommandText = "update patients set fromctc=1 where code='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                //commit
                mTrans.Commit();

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

        #region appointments

        #region Add_Appointment
        public AfyaPro_Types.clsResult Add_Appointment(
            string mPatientCode,
            DateTime mTransDate,
            int mApptType,
            string mReason,
            string mWhereTakenCode,
            DateTime mApptDate,
            string mUserId)
        {
            string mFunctionName = "Add_Appointment";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;
            mApptDate = mApptDate.Date;

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

            string mBookingNo = "";

            #region check for bookingno
            try
            {
                mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["autocode"].ToString().Trim();

                    if (Convert.ToDateTime(mDataReader["bookdate"]) != mTransDate)
                    {
                        mResult.Exe_Result = 0;
                        mResult.Exe_Message = "Client is not booked for services";
                        return mResult;
                    }
                }
                else
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = "Client is not booked for services";
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

            #region add
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                List<clsDataField> mDataFields;

                DataTable mDtData = new DataTable("data");
                mCommand.CommandText = "select * from ctc_appointments where patientcode='" + mPatientCode.Trim() + "' and booking='" 
                    + mBookingNo + "' and appttype=" + mApptType;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);

                if (mDtData.Rows.Count == 0)
                {
                    //ctc_appointments
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo));
                    mDataFields.Add(new clsDataField("appttype", DataTypes.dbnumber.ToString(), mApptType));
                    mDataFields.Add(new clsDataField("reason", DataTypes.dbstring.ToString(), mReason.Trim()));
                    mDataFields.Add(new clsDataField("wheretakencode", DataTypes.dbstring.ToString(), mWhereTakenCode.Trim()));
                    mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mApptDate));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_appointments", mDataFields);
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    int mAutoCode = Convert.ToInt32(mDtData.Rows[0]["autocode"]);
                    //ctc_appointments
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("appttype", DataTypes.dbnumber.ToString(), mApptType));
                    mDataFields.Add(new clsDataField("reason", DataTypes.dbstring.ToString(), mReason.Trim()));
                    mDataFields.Add(new clsDataField("wheretakencode", DataTypes.dbstring.ToString(), mWhereTakenCode.Trim()));
                    mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mApptDate));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_appointments", mDataFields, "autocode=" + mAutoCode);
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

        #region Edit_Appointment
        public AfyaPro_Types.clsResult Edit_Appointment(
            int mAutoCode,
            string mPatientCode,
            DateTime mTransDate,
            int mApptType,
            string mReason,
            string mWhereTakenCode,
            DateTime mApptDate,
            string mUserId)
        {
            string mFunctionName = "Edit_Appointment";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;
            mApptDate = mApptDate.Date;

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

            #region edit
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //ctc_appointments
                List<clsDataField> mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("appttype", DataTypes.dbnumber.ToString(), mApptType));
                mDataFields.Add(new clsDataField("reason", DataTypes.dbstring.ToString(), mReason.Trim()));
                mDataFields.Add(new clsDataField("wheretakencode", DataTypes.dbstring.ToString(), mWhereTakenCode.Trim()));
                mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mApptDate));
                mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_appointments", mDataFields, "autocode=" + mAutoCode);
                mCommand.ExecuteNonQuery();

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

        #region Delete_Appointment
        public AfyaPro_Types.clsResult Delete_Appointment(
            int mAutoCode,
            string mUserId)
        {
            string mFunctionName = "Delete_Appointment";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;

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

            #region delete
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //this patient has to be available when doing searching
                mCommand.CommandText = "delete from ctc_appointments where autocode=" + mAutoCode;
                mCommand.ExecuteNonQuery();

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

        #endregion

        #region Triage

        #region Add_Triage
        public AfyaPro_Types.clsResult Add_Triage(
            string mPatientCode,
            DateTime mTransDate,
            double mWeight,
            double mHeight,
            double mPulse,
            string mBloodPressure,
            double mRespRate,
            double mTemperature,
            string mUserId)
        {
            string mFunctionName = "Add_Triage";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
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
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcmeasurements_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            string mBookingNo = "";

            #region check for bookingno
            try
            {
                mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["autocode"].ToString().Trim();

                    if (Convert.ToDateTime(mDataReader["bookdate"]) != mTransDate)
                    {
                        mResult.Exe_Result = 0;
                        mResult.Exe_Message = "Client is not booked for services";
                        return mResult;
                    }
                }
                else
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = "Client is not booked for services";
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

            #region add
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                List<clsDataField> mDataFields;

                DataTable mDtData = new DataTable("data");
                mCommand.CommandText = "select * from ctc_triage where patientcode='" + mPatientCode.Trim() + "' and booking='" + mBookingNo + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);

                if (mDtData.Rows.Count == 0)
                {
                    //ctc_triage
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo));
                    mDataFields.Add(new clsDataField("weight", DataTypes.dbdecimal.ToString(), mWeight));
                    mDataFields.Add(new clsDataField("height", DataTypes.dbdecimal.ToString(), mHeight));
                    mDataFields.Add(new clsDataField("pulse", DataTypes.dbdecimal.ToString(), mPulse));
                    mDataFields.Add(new clsDataField("bloodpressure", DataTypes.dbstring.ToString(), mBloodPressure.Trim()));
                    mDataFields.Add(new clsDataField("resprate", DataTypes.dbdecimal.ToString(), mRespRate));
                    mDataFields.Add(new clsDataField("temperature", DataTypes.dbdecimal.ToString(), mTemperature));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_triage", mDataFields);
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    int mAutoCode = Convert.ToInt32(mDtData.Rows[0]["autocode"]);

                    //ctc_triage
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("weight", DataTypes.dbdecimal.ToString(), mWeight));
                    mDataFields.Add(new clsDataField("height", DataTypes.dbdecimal.ToString(), mHeight));
                    mDataFields.Add(new clsDataField("pulse", DataTypes.dbdecimal.ToString(), mPulse));
                    mDataFields.Add(new clsDataField("bloodpressure", DataTypes.dbstring.ToString(), mBloodPressure.Trim()));
                    mDataFields.Add(new clsDataField("resprate", DataTypes.dbdecimal.ToString(), mRespRate));
                    mDataFields.Add(new clsDataField("temperature", DataTypes.dbdecimal.ToString(), mTemperature));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_triage", mDataFields, "autocode=" + mAutoCode);
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

        #region Edit_Triage
        public AfyaPro_Types.clsResult Edit_Triage(
            int mAutoCode,
            string mPatientCode,
            DateTime mTransDate,
            double mWeight,
            double mHeight,
            double mPulse,
            string mBloodPressure,
            double mRespRate,
            double mTemperature,
            string mUserId)
        {
            string mFunctionName = "Edit_Triage";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
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
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcmeasurements_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region edit
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //ctc_triage
                List<clsDataField> mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("weight", DataTypes.dbdecimal.ToString(), mWeight));
                mDataFields.Add(new clsDataField("height", DataTypes.dbdecimal.ToString(), mHeight));
                mDataFields.Add(new clsDataField("pulse", DataTypes.dbdecimal.ToString(), mPulse));
                mDataFields.Add(new clsDataField("bloodpressure", DataTypes.dbstring.ToString(), mBloodPressure.Trim()));
                mDataFields.Add(new clsDataField("resprate", DataTypes.dbdecimal.ToString(), mRespRate));
                mDataFields.Add(new clsDataField("temperature", DataTypes.dbdecimal.ToString(), mTemperature));
                mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_triage", mDataFields, "autocode=" + mAutoCode);
                mCommand.ExecuteNonQuery();

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

        #region Delete_Triage
        public AfyaPro_Types.clsResult Delete_Triage(
            int mAutoCode,
            string mUserId)
        {
            string mFunctionName = "Delete_Triage";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcmeasurements_delete.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region delete
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //ctc_triage
                mCommand.CommandText = "delete from ctc_triage where autocode=" + mAutoCode;
                mCommand.ExecuteNonQuery();

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

        #endregion

        #region HTC

        #region Add_HTC
        public AfyaPro_Types.clsCtcClient Add_HTC(
            string mPatientCode,
            Int16 mGenerateHIVNo,
            string mHIVNo,
            DateTime? mTransDate,
            int mPregnant,
            int mEverTested,
            int mWithPartner,
            int mTestResult1,
            int mTestResult2,
            int mTestResult3,
            int mTestResultGiven,
            DateTime? mTestResultGivenDate,
            string mComments,
            string mCounselorCode,
            DateTime? mNextApptDate,
            DataTable mDtReferals,
            string mMachineName,
            string mMachineUser,
            string mUserId,
            DataTable mDtReason,
            string mProblemnotes,
            string mObservation,
            string mMedicalsymptoms,
            string mReactionPositive,
            string mReactionNegative,
            string mShareResultWith,
            string mClientAssessmentNotes,
            int mClientDecision,
            string mPreInterventionNotes,
            int mWillingToTest,
            int mUnderstoodInformation,
            DateTime? mResultCollectionDate,
            DataTable mDtEducation,
            DataTable mDtSupport,
            int mAccompaniedBy,
            string mPostProblemNotes,            
            int mWillingToRcvResult,
            string mPostObservation,
            string mPostObservationNotes,
            int mPostIntervention,
            string mPostInterventionNotes,
            string mAppointmentReason)
        {
            string mFunctionName = "Add_HTC";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Value.Date;

            if (mNextApptDate != null)
            {
                mNextApptDate = mNextApptDate.Value.Date;
            }

            if (mTestResultGivenDate != null)
            {
                mTestResultGivenDate = mTestResultGivenDate.Value.Date;
            }

            if (mResultCollectionDate != null)
            {
                mResultCollectionDate = mResultCollectionDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctchivtests_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            bool mAlreadyHasHIVNo = false;
            bool mRecordFound = false;
            bool mHIVNoGenerated = false;
            string mBookingNo = "";

            #region check for bookingno
            try
            {
                mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["autocode"].ToString().Trim();

                    if (Convert.ToDateTime(mDataReader["bookdate"]) != mTransDate)
                    {
                        mCtcClient.Exe_Result = 0;
                        mCtcClient.Exe_Message = "Client is not booked for services";
                        return mCtcClient;
                    }
                }
                else
                {
                    mCtcClient.Exe_Result = 0;
                    mCtcClient.Exe_Message = "Client is not booked for services";
                    return mCtcClient;
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion

            #region check if already has HIV no
            try
            {
                mCommand.CommandText = "select * from ctc_patients where patientcode='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mRecordFound = true;
                    if (mDataReader["hivno"].ToString().Trim() != "")
                    {
                        mAlreadyHasHIVNo = true;
                    }
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion

            #region HIV no

            if (mAlreadyHasHIVNo == false)
            {
                if (mTestResultGiven == (int)AfyaPro_Types.clsEnums.CTC_HIVTestResultsGiven.Positive)
                {
                    if (mGenerateHIVNo == 1)
                    {
                        clsAutoCodes objAutoCodes = new clsAutoCodes();
                        AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                        mObjCode = objAutoCodes.Next_Code(
                            Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.hivnumbers), "ctc_patients", "hivno");
                        if (mObjCode.Exe_Result == -1)
                        {
                            mCtcClient.Exe_Result = mObjCode.Exe_Result;
                            mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                            return mCtcClient;
                        }
                        mHIVNo = mObjCode.GeneratedCode;
                        mHIVNoGenerated = true;
                    }
                    else
                    {
                        if (mHIVNo.Trim() == "")
                        {
                            mCtcClient.Exe_Result = 0;
                            mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.CTC_HIVNoIsInvalid.ToString();
                            return mCtcClient;
                        }
                    }
                }
                else
                {
                    mHIVNo = "";
                }
            }

            #endregion

            #region check 4 duplicate hivno
            try
            {
                mCommand.CommandText = "select * from ctc_patients where hivno='" + mHIVNo.Trim() + "' and patientcode<>'" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    if (mDataReader["hivno"].ToString().Trim() != "")
                    {
                        mCtcClient.Exe_Result = 0;
                        mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.CTC_HIVNoIsInUse.ToString();
                        return mCtcClient;
                    }
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
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

                List<clsDataField> mDataFields;

                if (mRecordFound == false)
                {
                    //ctc_patients
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patients", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                //ctc_patients
                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("hivno", DataTypes.dbstring.ToString(), mHIVNo.Trim()));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_patients", mDataFields, "patientcode='" + mPatientCode.Trim() + "'");
                int mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit ctc_patients

                if (mRecsAffected > 0)
                {
                    DataTable mDtTests = new DataTable("ctc_patients");
                    mCommand.CommandText = "select * from ctc_patients where patientcode='" + mPatientCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtTests);

                    mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtTests, "ctc_patients",
                    DateTime.Now.Date, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region ctc_hivtests

                DataTable mDtData = new DataTable("data");
                mCommand.CommandText = "select * from ctc_hivtests where patientcode='" + mPatientCode.Trim() + "' and booking='" + mBookingNo + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);

                if (mDtData.Rows.Count == 0)
                {
                    //ctc_hivtests
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("pregnant", DataTypes.dbnumber.ToString(), mPregnant));
                    mDataFields.Add(new clsDataField("evertested", DataTypes.dbnumber.ToString(), mEverTested));
                    mDataFields.Add(new clsDataField("withpartner", DataTypes.dbnumber.ToString(), mWithPartner));
                    mDataFields.Add(new clsDataField("testresult1", DataTypes.dbnumber.ToString(), mTestResult1));
                    mDataFields.Add(new clsDataField("testresult2", DataTypes.dbnumber.ToString(), mTestResult2));
                    mDataFields.Add(new clsDataField("testresult3", DataTypes.dbnumber.ToString(), mTestResult3));
                    mDataFields.Add(new clsDataField("testresultgiven", DataTypes.dbnumber.ToString(), mTestResultGiven));
                    mDataFields.Add(new clsDataField("testresultgivendate", DataTypes.dbdatetime.ToString(), mTestResultGivenDate));
                    mDataFields.Add(new clsDataField("comments", DataTypes.dbstring.ToString(), mComments.Trim()));
                    mDataFields.Add(new clsDataField("counsellorcode", DataTypes.dbstring.ToString(), mCounselorCode.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_hivtests", mDataFields);
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    int mAutoCode = Convert.ToInt32(mDtData.Rows[0]["autocode"]);

                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("pregnant", DataTypes.dbnumber.ToString(), mPregnant));
                    mDataFields.Add(new clsDataField("evertested", DataTypes.dbnumber.ToString(), mEverTested));
                    mDataFields.Add(new clsDataField("withpartner", DataTypes.dbnumber.ToString(), mWithPartner));
                    mDataFields.Add(new clsDataField("testresult1", DataTypes.dbnumber.ToString(), mTestResult1));
                    mDataFields.Add(new clsDataField("testresult2", DataTypes.dbnumber.ToString(), mTestResult2));
                    mDataFields.Add(new clsDataField("testresult3", DataTypes.dbnumber.ToString(), mTestResult3));
                    mDataFields.Add(new clsDataField("testresultgiven", DataTypes.dbnumber.ToString(), mTestResultGiven));
                    mDataFields.Add(new clsDataField("testresultgivendate", DataTypes.dbdatetime.ToString(), mTestResultGivenDate));
                    mDataFields.Add(new clsDataField("comments", DataTypes.dbstring.ToString(), mComments.Trim()));
                    mDataFields.Add(new clsDataField("counsellorcode", DataTypes.dbstring.ToString(), mCounselorCode.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_hivtests", mDataFields, "autocode=" + mAutoCode);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region ctc_hivcounselling

                DataTable mDtDataCounselling = new DataTable("data");
                mCommand.CommandText = "select * from ctc_hivcounselling where patientcode='" + mPatientCode.Trim() + "' and booking='" + mBookingNo + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtDataCounselling);

                if (mDtDataCounselling.Rows.Count == 0)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("problemnotes", DataTypes.dbstring.ToString(), mProblemnotes.Trim()));
                    mDataFields.Add(new clsDataField("observation", DataTypes.dbstring.ToString(), mObservation.Trim()));
                    mDataFields.Add(new clsDataField("medicalsymptoms", DataTypes.dbstring.ToString(), mMedicalsymptoms.Trim()));
                    mDataFields.Add(new clsDataField("reactionpositive", DataTypes.dbstring.ToString(), mReactionPositive.Trim()));
                    mDataFields.Add(new clsDataField("reactionnegative", DataTypes.dbstring.ToString(), mReactionNegative.Trim()));
                    mDataFields.Add(new clsDataField("shareresultwith", DataTypes.dbstring.ToString(), mShareResultWith.Trim()));
                    mDataFields.Add(new clsDataField("preclientassessmentnotes", DataTypes.dbstring.ToString(), mClientAssessmentNotes));
                    mDataFields.Add(new clsDataField("clientdecision", DataTypes.dbnumber.ToString(), mClientDecision));
                    mDataFields.Add(new clsDataField("preinterventionnotes", DataTypes.dbstring.ToString(), mPreInterventionNotes));
                    mDataFields.Add(new clsDataField("willingtoaccept", DataTypes.dbnumber.ToString(), mWillingToTest));
                    mDataFields.Add(new clsDataField("understoodinformation", DataTypes.dbnumber.ToString(), mUnderstoodInformation));
                    mDataFields.Add(new clsDataField("resultcollectiondate", DataTypes.dbdatetime.ToString(), mResultCollectionDate));
                    mDataFields.Add(new clsDataField("accompaniedby", DataTypes.dbnumber.ToString(), mAccompaniedBy));
                    mDataFields.Add(new clsDataField("postproblemnotes", DataTypes.dbstring.ToString(), mPostProblemNotes.Trim()));
                    mDataFields.Add(new clsDataField("willingtoreceiveresult", DataTypes.dbnumber.ToString(), mWillingToRcvResult));
                    mDataFields.Add(new clsDataField("postobservation", DataTypes.dbstring.ToString(), mPostObservation));
                    mDataFields.Add(new clsDataField("postobservationnotes", DataTypes.dbstring.ToString(), mPostObservationNotes.Trim()));
                    mDataFields.Add(new clsDataField("postintervention", DataTypes.dbnumber.ToString(), mPostIntervention));                              
                    mDataFields.Add(new clsDataField("postintervetionnotes", DataTypes.dbstring.ToString(), mPostInterventionNotes.Trim()));
                    mDataFields.Add(new clsDataField("appointmentreasoncode", DataTypes.dbstring.ToString(), mAppointmentReason.Trim()));
                  
                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_hivcounselling", mDataFields);
                    mCommand.ExecuteNonQuery();
                   
                }
                else
                {

                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("problemnotes", DataTypes.dbstring.ToString(), mProblemnotes.Trim()));
                    mDataFields.Add(new clsDataField("observation", DataTypes.dbstring.ToString(), mObservation.Trim()));
                    mDataFields.Add(new clsDataField("medicalsymptoms", DataTypes.dbstring.ToString(), mMedicalsymptoms.Trim()));
                    mDataFields.Add(new clsDataField("reactionpositive", DataTypes.dbstring.ToString(), mReactionPositive.Trim()));
                    mDataFields.Add(new clsDataField("reactionnegative", DataTypes.dbstring.ToString(), mReactionNegative.Trim()));
                    mDataFields.Add(new clsDataField("shareresultwith", DataTypes.dbstring.ToString(), mShareResultWith.Trim()));
                    mDataFields.Add(new clsDataField("preclientassessmentnotes", DataTypes.dbstring.ToString(), mClientAssessmentNotes));
                    mDataFields.Add(new clsDataField("clientdecision", DataTypes.dbnumber.ToString(), mClientDecision));
                    mDataFields.Add(new clsDataField("preinterventionnotes", DataTypes.dbstring.ToString(), mPreInterventionNotes));
                    mDataFields.Add(new clsDataField("willingtoaccept", DataTypes.dbnumber.ToString(), mWillingToTest));
                    mDataFields.Add(new clsDataField("understoodinformation", DataTypes.dbnumber.ToString(), mUnderstoodInformation));
                    mDataFields.Add(new clsDataField("resultcollectiondate", DataTypes.dbdatetime.ToString(), mResultCollectionDate));
                    mDataFields.Add(new clsDataField("accompaniedby", DataTypes.dbnumber.ToString(), mAccompaniedBy));
                    mDataFields.Add(new clsDataField("postproblemnotes", DataTypes.dbstring.ToString(), mPostProblemNotes.Trim()));
                    mDataFields.Add(new clsDataField("willingtoreceiveresult", DataTypes.dbnumber.ToString(), mWillingToRcvResult));
                    mDataFields.Add(new clsDataField("postobservation", DataTypes.dbstring.ToString(), mPostObservation));
                    mDataFields.Add(new clsDataField("postobservationnotes", DataTypes.dbstring.ToString(), mPostObservationNotes.Trim()));
                    mDataFields.Add(new clsDataField("postintervention", DataTypes.dbnumber.ToString(), mPostIntervention));                              
                    mDataFields.Add(new clsDataField("postintervetionnotes", DataTypes.dbstring.ToString(), mPostInterventionNotes.Trim()));
                    mDataFields.Add(new clsDataField("appointmentreasoncode", DataTypes.dbstring.ToString(), mAppointmentReason.Trim()));
                                  
                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_hivcounselling", mDataFields, "patientcode='" + mPatientCode.Trim() + "' and booking='" + mBookingNo + "'");
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region referals

                mCommand.CommandText = "delete from ctc_hivtestreferals where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtReferals.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("referalcode", DataTypes.dbstring.ToString(), mDataRow["code"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_hivtestreferals", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region Testing Reason

                mCommand.CommandText = "delete from ctc_hivcounsellingreasonlog where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtReason.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("reasoncode", DataTypes.dbstring.ToString(), mDataRow["reasoncode"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_hivcounsellingreasonlog", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region HIV Educatoion

                mCommand.CommandText = "delete from ctc_hiveducationlog where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtEducation.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("hiveducationcode", DataTypes.dbstring.ToString(), mDataRow["hiveducationcode"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_hiveducationlog", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion
             
                #region Support Systems

                mCommand.CommandText = "delete from ctc_hivclientsupportlog where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtSupport.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("supportcode", DataTypes.dbstring.ToString(), mDataRow["supportcode"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_hivclientsupportlog", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion
                
                #region ctc_appointments

                if (clsGlobal.IsNullDate(mNextApptDate) == false)
                {
                    DataTable mDtAppts = new DataTable("appointments");
                    mCommand.CommandText = "select * from ctc_appointments where patientcode='" + mPatientCode
                        + "' and booking='" + mBookingNo + "' and appttype=" + (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.HIVTest;
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAppts);

                    if (mDtAppts.Rows.Count == 0)
                    {
                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                        mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                        mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                        mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                        mDataFields.Add(new clsDataField("appttype", DataTypes.dbnumber.ToString(), (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.HIVTest));
                        mDataFields.Add(new clsDataField("reason", DataTypes.dbstring.ToString(), "HIV Test"));
                        mDataFields.Add(new clsDataField("wheretakencode", DataTypes.dbstring.ToString(), null));
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextApptDate));
                        mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                        mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_appointments", mDataFields);
                        mCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        int mApptAutoCode = Convert.ToInt32(mDtAppts.Rows[0]["autocode"]);

                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextApptDate));

                        mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_appointments", mDataFields, "autocode=" + mApptAutoCode);
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                //increment hivno
                if (mHIVNoGenerated == true)
                {
                    mCommand.CommandText = "update facilityautocodes set idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.hivnumbers);
                    mCommand.ExecuteNonQuery();
                }

                //this patient has to be available when doing searching
                mCommand.CommandText = "update patients set fromctc=1 where code='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                #region delete from queue

                mCommand.CommandText = "delete from patientsqueue where patientcode='" + mPatientCode.Trim()
                    + "' and (queuetype=" + (int)AfyaPro_Types.clsEnums.PatientsQueueTypes.Consultation
                    + " or queuetype=" + (int)AfyaPro_Types.clsEnums.PatientsQueueTypes.Results + ")";
                mCommand.ExecuteNonQuery();

                #endregion

                //commit
                mTrans.Commit();

                //get patient
                mCtcClient = this.Get_Client("code", mPatientCode);

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Edit_HTC
        public AfyaPro_Types.clsCtcClient Edit_HTC(
            int mAutoCode,
            string mPatientCode,
            Int16 mGenerateHIVNo,
            string mHIVNo,
            DateTime? mTransDate,
            int mPregnant,
            int mEverTested,
            int mWithPartner,
            int mTestResult1,
            int mTestResult2,
            int mTestResult3,
            int mTestResultGiven,
            DateTime? mTestResultGivenDate,
            string mComments,
            string mCounselorCode,
            DateTime? mNextApptDate,
            DataTable mDtReferals,
            string mMachineName,
            string mMachineUser,
            string mUserId,
            DataTable mDtReason,
            string mProblemnotes,
            string mObservation,
            string mMedicalsymptoms,
            string mReactionPositive,
            string mReactionNegative,
            string mShareResultWith,
            string mClientAssessmentNotes,
            int mClientDecision,
            string mPreInterventionNotes,
            int mWillingToTest,
            int mUnderstoodInformation,
            DateTime? mResultCollectionDate,
            DataTable mDtEducation,
            DataTable mDtSupport,
            int mAccompaniedBy,
            string mPostProblemNotes, 
            int mWillingToRcvResult,
            string mPostObservation,
            string mPostObservationNotes,
            int mPostIntervention,
            string mPostInterventionNotes,
            string mAppointmentReason)
        {
            string mFunctionName = "Edit_HTC";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Value.Date;

            if (mNextApptDate != null)
            {
                mNextApptDate = mNextApptDate.Value.Date;
            }

            if (mTestResultGivenDate != null)
            {
                mTestResultGivenDate = mTestResultGivenDate.Value.Date;
            }

            if (mResultCollectionDate != null)
            {
                mResultCollectionDate = mResultCollectionDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctchivtests_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            bool mAlreadyHasHIVNo = false;
            bool mHIVNoGenerated = false;
            string mBookingNo = "";

            #region check for bookingno
            try
            {
                mCommand.CommandText = "select * from ctc_hivtests where autocode=" + mAutoCode;
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["booking"].ToString().Trim();
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion

            #region check if already has HIV no
            try
            {
                mCommand.CommandText = "select * from ctc_patients where patientcode='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    if (mDataReader["hivno"].ToString().Trim() != "")
                    {
                        mAlreadyHasHIVNo = true;
                    }
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion

            #region HIV no

            if (mAlreadyHasHIVNo == false)
            {
                if (mTestResultGiven == (int)AfyaPro_Types.clsEnums.CTC_HIVTestResultsGiven.Positive)
                {
                    if (mGenerateHIVNo == 1)
                    {
                        clsAutoCodes objAutoCodes = new clsAutoCodes();
                        AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                        mObjCode = objAutoCodes.Next_Code(
                            Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.hivnumbers), "ctc_patients", "hivno");
                        if (mObjCode.Exe_Result == -1)
                        {
                            mCtcClient.Exe_Result = mObjCode.Exe_Result;
                            mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                            return mCtcClient;
                        }
                        mHIVNo = mObjCode.GeneratedCode;
                        mHIVNoGenerated = true;
                    }
                    else
                    {
                        if (mHIVNo.Trim() == "")
                        {
                            mCtcClient.Exe_Result = 0;
                            mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.CTC_HIVNoIsInvalid.ToString();
                            return mCtcClient;
                        }
                    }
                }
                else
                {
                    mHIVNo = "";
                }
            }

            #endregion

            #region check 4 duplicate hivno
            try
            {
                mCommand.CommandText = "select * from ctc_patients where hivno='" + mHIVNo.Trim() + "' and patientcode<>'" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    if (mDataReader["hivno"].ToString().Trim() != "")
                    {
                        mCtcClient.Exe_Result = 0;
                        mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.CTC_HIVNoIsInUse.ToString();
                        return mCtcClient;
                    }
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
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

                List<clsDataField> mDataFields;

                //ctc_patients
                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("hivno", DataTypes.dbstring.ToString(), mHIVNo.Trim()));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_patients", mDataFields, "patientcode='" + mPatientCode.Trim() + "'");
                mCommand.ExecuteNonQuery();

                //ctc_hivtests
                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("pregnant", DataTypes.dbnumber.ToString(), mPregnant));
                mDataFields.Add(new clsDataField("evertested", DataTypes.dbnumber.ToString(), mEverTested));
                mDataFields.Add(new clsDataField("withpartner", DataTypes.dbnumber.ToString(), mWithPartner));
                mDataFields.Add(new clsDataField("testresult1", DataTypes.dbnumber.ToString(), mTestResult1));
                mDataFields.Add(new clsDataField("testresult2", DataTypes.dbnumber.ToString(), mTestResult2));
                mDataFields.Add(new clsDataField("testresult3", DataTypes.dbnumber.ToString(), mTestResult3));
                mDataFields.Add(new clsDataField("testresultgiven", DataTypes.dbnumber.ToString(), mTestResultGiven));
                mDataFields.Add(new clsDataField("testresultgivendate", DataTypes.dbdatetime.ToString(), mTestResultGivenDate));
                mDataFields.Add(new clsDataField("comments", DataTypes.dbstring.ToString(), mComments.Trim()));
                mDataFields.Add(new clsDataField("counsellorcode", DataTypes.dbstring.ToString(), mCounselorCode.Trim()));
                mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_hivtests", mDataFields, "autocode=" + mAutoCode);
                mCommand.ExecuteNonQuery();

                #region ctc_hivcounselling

                DataTable mDtDataCounselling = new DataTable("data");
                mCommand.CommandText = "select * from ctc_hivcounselling where patientcode='" + mPatientCode.Trim() + "' and booking='" + mBookingNo + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtDataCounselling);              

                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("problemnotes", DataTypes.dbstring.ToString(), mProblemnotes.Trim()));
                mDataFields.Add(new clsDataField("observation", DataTypes.dbstring.ToString(), mObservation.Trim()));
                mDataFields.Add(new clsDataField("medicalsymptoms", DataTypes.dbstring.ToString(), mMedicalsymptoms.Trim()));
                mDataFields.Add(new clsDataField("reactionpositive", DataTypes.dbstring.ToString(), mReactionPositive.Trim()));
                mDataFields.Add(new clsDataField("reactionnegative", DataTypes.dbstring.ToString(), mReactionNegative.Trim()));
                mDataFields.Add(new clsDataField("shareresultwith", DataTypes.dbstring.ToString(), mShareResultWith.Trim()));
                mDataFields.Add(new clsDataField("preclientassessmentnotes", DataTypes.dbstring.ToString(), mClientAssessmentNotes));
                mDataFields.Add(new clsDataField("clientdecision", DataTypes.dbnumber.ToString(), mClientDecision));               
                mDataFields.Add(new clsDataField("preinterventionnotes", DataTypes.dbstring.ToString(), mPreInterventionNotes));                                       
                mDataFields.Add(new clsDataField("willingtoaccept", DataTypes.dbnumber.ToString(), mWillingToTest));
                mDataFields.Add(new clsDataField("understoodinformation", DataTypes.dbnumber.ToString(), mUnderstoodInformation));
                mDataFields.Add(new clsDataField("resultcollectiondate", DataTypes.dbdatetime.ToString(), mResultCollectionDate));
                mDataFields.Add(new clsDataField("accompaniedby", DataTypes.dbnumber.ToString(), mAccompaniedBy));
                mDataFields.Add(new clsDataField("postproblemnotes", DataTypes.dbstring.ToString(), mPostProblemNotes.Trim()));
                mDataFields.Add(new clsDataField("willingtoreceiveresult", DataTypes.dbnumber.ToString(), mWillingToRcvResult));
                mDataFields.Add(new clsDataField("postobservation", DataTypes.dbstring.ToString(), mPostObservation));                    
                mDataFields.Add(new clsDataField("postobservationnotes", DataTypes.dbstring.ToString(), mPostObservationNotes.Trim()));
                mDataFields.Add(new clsDataField("postintervention", DataTypes.dbnumber.ToString(), mPostIntervention));                              
                mDataFields.Add(new clsDataField("postintervetionnotes", DataTypes.dbstring.ToString(), mPostInterventionNotes.Trim()));
                mDataFields.Add(new clsDataField("appointmentreasoncode", DataTypes.dbstring.ToString(), mAppointmentReason.Trim()));
                                 
                
                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_hivcounselling", mDataFields, "patientcode='" + mPatientCode.Trim() + "' and booking='" + mBookingNo + "'");
                mCommand.ExecuteNonQuery();
              
                #endregion

                #region referals

                mCommand.CommandText = "delete from ctc_hivtestreferals where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtReferals.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("referalcode", DataTypes.dbstring.ToString(), mDataRow["code"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_hivtestreferals", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region Testing Reason

                mCommand.CommandText = "delete from ctc_hivcounsellingreasonlog where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtReason.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("reasoncode", DataTypes.dbstring.ToString(), mDataRow["reasoncode"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_hivcounsellingreasonlog", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region HIV Educatoion

                mCommand.CommandText = "delete from ctc_hiveducationlog where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtEducation.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("hiveducationcode", DataTypes.dbstring.ToString(), mDataRow["hiveducationcode"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_hiveducationlog", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region Support Systems

                mCommand.CommandText = "delete from ctc_hivclientsupportlog where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtSupport.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("supportcode", DataTypes.dbstring.ToString(), mDataRow["supportcode"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_hivclientsupportlog", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                //ctc_appointments
                if (clsGlobal.IsNullDate(mNextApptDate) == false)
                {
                    DataTable mDtAppts = new DataTable("appointments");
                    mCommand.CommandText = "select * from ctc_appointments where patientcode='" + mPatientCode
                        + "' and booking='" + mBookingNo + "' and appttype=" + (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.HIVTest;
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAppts);

                    if (mDtAppts.Rows.Count == 0)
                    {
                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                        mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                        mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                        mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                        mDataFields.Add(new clsDataField("appttype", DataTypes.dbnumber.ToString(), (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.HIVTest));
                        mDataFields.Add(new clsDataField("reason", DataTypes.dbstring.ToString(), "HIV Test"));
                        mDataFields.Add(new clsDataField("wheretakencode", DataTypes.dbstring.ToString(), null));
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextApptDate));
                        mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                        mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_appointments", mDataFields);
                        mCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        int mApptAutoCode = Convert.ToInt32(mDtAppts.Rows[0]["autocode"]);

                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextApptDate));

                        mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_appointments", mDataFields, "autocode=" + mApptAutoCode);
                        mCommand.ExecuteNonQuery();
                    }
                }
                else
                {
                    mCommand.CommandText = "delete from ctc_appointments where patientcode='" + mPatientCode
                        + "' and booking='" + mBookingNo + "' and appttype=" + (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.HIVTest;
                    mCommand.ExecuteNonQuery();
                }

                //increment hivno
                if (mHIVNoGenerated == true)
                {
                    mCommand.CommandText = "update facilityautocodes set idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.hivnumbers);
                    mCommand.ExecuteNonQuery();
                }

                #region delete from queue

                mCommand.CommandText = "delete from patientsqueue where patientcode='" + mPatientCode.Trim()
                    + "' and (queuetype=" + (int)AfyaPro_Types.clsEnums.PatientsQueueTypes.Consultation
                    + " or queuetype=" + (int)AfyaPro_Types.clsEnums.PatientsQueueTypes.Results + ")";
                mCommand.ExecuteNonQuery();

                #endregion

                //commit
                mTrans.Commit();

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Delete_HTC
        public AfyaPro_Types.clsCtcClient Delete_HTC(
            int mAutoCode,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Delete_HTC";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctchivtests_delete.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            #region delete
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //this patient has to be available when doing searching
                mCommand.CommandText = "delete from ctc_hivtests where autocode=" + mAutoCode;
                mCommand.ExecuteNonQuery();

                //commit
                mTrans.Commit();

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #endregion
    
        #region Supportive Counselling

        #region Add_SupportiveCounselling
        public AfyaPro_Types.clsCtcClient Add_SupportiveCounselling( string mPatientCode,           
            DateTime? mTransDate,
            string mCounselorCode,
            string mProblem,
            string mReply,
            string mUserId)
        {
            string mFunctionName = "Add_SupportiveCounselling";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctchivtests_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            #region check for bookingno
            try
            {

                string mBookingNo = "";

                mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["autocode"].ToString().Trim();

                    if (Convert.ToDateTime(mDataReader["bookdate"]) != mTransDate)
                    {
                        mCtcClient.Exe_Result = 0;
                        mCtcClient.Exe_Message = "Client is not booked for services";
                        return mCtcClient;
                    }
                }
                else
                {
                    mCtcClient.Exe_Result = 0;
                    mCtcClient.Exe_Message = "Client is not booked for services";
                    return mCtcClient;
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
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
                string mBookingNo = "";

                #region check for bookingno
                try
                {
                    mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                    mDataReader = mCommand.ExecuteReader();
                    if (mDataReader.Read() == true)
                    {
                        mBookingNo = mDataReader["autocode"].ToString().Trim();

                        if (Convert.ToDateTime(mDataReader["bookdate"]) != mTransDate)
                        {
                            mCtcClient.Exe_Result = 0;
                            mCtcClient.Exe_Message = "Client is not booked for services";
                            return mCtcClient;
                        }
                    }
                    else
                    {
                        mCtcClient.Exe_Result = 0;
                        mCtcClient.Exe_Message = "Client is not booked for services";
                        return mCtcClient;
                    }
                }
                catch (OdbcException ex)
                {
                    mCtcClient.Exe_Result = -1;
                    mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                    return mCtcClient;
                }
                finally
                {
                    mDataReader.Close();
                }
                #endregion

                #region Add supportive counselling

                List<clsDataField> mDataFields;

                DataTable mDtData = new DataTable("data");
                mCommand.CommandText = "select * from ctc_supportivecounselling where patientcode='" + mPatientCode.Trim() + "' and booking='" + mBookingNo + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);

                if (mDtData.Rows.Count == 0)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));                   
                    mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("problem", DataTypes.dbstring.ToString(), mProblem.Trim()));
                    mDataFields.Add(new clsDataField("reply", DataTypes.dbstring.ToString(), mReply.Trim()));
                    mDataFields.Add(new clsDataField("counsellorcode", DataTypes.dbstring.ToString(), mCounselorCode.Trim()));                    
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_supportivecounselling", mDataFields);
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    int mAutoCode = Convert.ToInt32(mDtData.Rows[0]["autocode"]);

                    mDataFields = new List<clsDataField>();
                   
                    mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                    mDataFields.Add(new clsDataField("problem", DataTypes.dbstring.ToString(), mProblem.Trim()));
                    mDataFields.Add(new clsDataField("reply", DataTypes.dbstring.ToString(), mReply.Trim()));
                    mDataFields.Add(new clsDataField("counsellorcode", DataTypes.dbstring.ToString(), mCounselorCode.Trim()));                    
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_supportivecounselling", mDataFields, "autocode=" + mAutoCode);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region delete from queue

                mCommand.CommandText = "delete from patientsqueue where patientcode='" + mPatientCode.Trim()
                    + "' and (queuetype=" + (int)AfyaPro_Types.clsEnums.PatientsQueueTypes.Consultation
                    + " or queuetype=" + (int)AfyaPro_Types.clsEnums.PatientsQueueTypes.Results + ")";
                mCommand.ExecuteNonQuery();

                #endregion

                //commit
                mTrans.Commit();

                //get patient
                mCtcClient = this.Get_Client("code", mPatientCode);

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Edit_SupportiveCounselling
        public AfyaPro_Types.clsCtcClient Edit_SupportiveCounselling(int mAutoCode,
            string mPatientCode,
            DateTime? mTransDate,
            string mCounselorCode,
            string mProblem,
            string mReply,
            string mUserId)
        {
            string mFunctionName = "Edit_SupportiveCounselling";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Value.Date;

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctchivtests_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

             string mBookingNo = "";

            #region check for bookingno
            try
            {
                mCommand.CommandText = "select * from ctc_hivtests where autocode=" + mAutoCode;
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["booking"].ToString().Trim();
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
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

                List<clsDataField> mDataFields;

                //ctc_hivtests
                mDataFields = new List<clsDataField>();

                mDataFields.Add(new clsDataField("problem", DataTypes.dbstring.ToString(), mProblem.Trim()));
                mDataFields.Add(new clsDataField("reply", DataTypes.dbstring.ToString(), mReply.Trim()));
                mDataFields.Add(new clsDataField("counsellorcode", DataTypes.dbstring.ToString(), mCounselorCode.Trim()));
                mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));


                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_supportivecounselling", mDataFields, "autocode=" + mAutoCode);
                mCommand.ExecuteNonQuery();  
                
                #region delete from queue
                mCommand.CommandText = "delete from patientsqueue where patientcode='" + mPatientCode.Trim()
                    + "' and (queuetype=" + (int)AfyaPro_Types.clsEnums.PatientsQueueTypes.Consultation
                    + " or queuetype=" + (int)AfyaPro_Types.clsEnums.PatientsQueueTypes.Results + ")";
                mCommand.ExecuteNonQuery();

                #endregion

                //commit
                mTrans.Commit();

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion       
            
        }
        #endregion

        #endregion

        #region PCR

        #region Add_PCR
        public AfyaPro_Types.clsCtcClient Add_PCR(
            string mPatientCode,
            DateTime? mTransDate,
            DateTime? mSampleDate,
            string mReasonCode,
            int mTestResult,
            DateTime? mTestResultDate,
            DateTime? mTestResultReceviedDate,
            int mGivenCPT,
            string mMotherClientCode,
            string mComments,
            string mCounselorCode,
            int mBreastFeeding,
            DateTime? mNextApptDate,
            DataTable mDtReferals,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Add_PCR";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Value.Date;

            if (mNextApptDate != null)
            {
                mNextApptDate = mNextApptDate.Value.Date;
            }

            if (mTestResultDate != null)
            {
                mTestResultDate = mTestResultDate.Value.Date;
            }

            if (mSampleDate != null)
            {
                mSampleDate = mSampleDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpcrtests_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            bool mRecordFound = false;
            string mBookingNo = "";

            #region check for bookingno
            try
            {
                mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["autocode"].ToString().Trim();

                    if (Convert.ToDateTime(mDataReader["bookdate"]) != mTransDate)
                    {
                        mCtcClient.Exe_Result = 0;
                        mCtcClient.Exe_Message = "Client is not booked for services";
                        return mCtcClient;
                    }
                }
                else
                {
                    mCtcClient.Exe_Result = 0;
                    mCtcClient.Exe_Message = "Client is not booked for services";
                    return mCtcClient;
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion

            #region check if exist
            try
            {
                mCommand.CommandText = "select * from ctc_patients where patientcode='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mRecordFound = true;
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
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

                List<clsDataField> mDataFields;

                if (mRecordFound == false)
                {
                    //ctc_patients
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("motherclientno", DataTypes.dbstring.ToString(), mMotherClientCode.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patients", mDataFields);
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("motherclientno", DataTypes.dbstring.ToString(), mMotherClientCode.Trim()));
                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_patients", mDataFields, "patientcode=" + mPatientCode.Trim());
                    mCommand.ExecuteNonQuery();

                }
                #region ctc_pcrtests

                DataTable mDtData = new DataTable("data");
                mCommand.CommandText = "select * from ctc_pcrtests where patientcode='" + mPatientCode.Trim() + "' and booking='" + mBookingNo + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);

                if (mDtData.Rows.Count == 0)
                {
                    //ctc_pcrtests
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("sampledate", DataTypes.dbdatetime.ToString(), mSampleDate));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("reasoncode", DataTypes.dbstring.ToString(), mReasonCode.Trim()));
                    mDataFields.Add(new clsDataField("breastfeeding", DataTypes.dbnumber.ToString(), mBreastFeeding));
                    mDataFields.Add(new clsDataField("testresult", DataTypes.dbnumber.ToString(), mTestResult));
                    mDataFields.Add(new clsDataField("testresultdate", DataTypes.dbdatetime.ToString(), mTestResultDate));
                    mDataFields.Add(new clsDataField("comments", DataTypes.dbstring.ToString(), mComments.Trim()));
                    mDataFields.Add(new clsDataField("counsellorcode", DataTypes.dbstring.ToString(), mCounselorCode.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));
                    mDataFields.Add(new clsDataField("testresultrcvddate", DataTypes.dbdatetime.ToString(), mTestResultReceviedDate));
                    mDataFields.Add(new clsDataField("cptgiven", DataTypes.dbnumber.ToString(), mGivenCPT));
                   
                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_pcrtests", mDataFields);
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    int mAutoCode = Convert.ToInt32(mDtData.Rows[0]["autocode"]);

                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("sampledate", DataTypes.dbdatetime.ToString(), mSampleDate));
                    mDataFields.Add(new clsDataField("reasoncode", DataTypes.dbstring.ToString(), mReasonCode.Trim()));
                    mDataFields.Add(new clsDataField("breastfeeding", DataTypes.dbnumber.ToString(), mBreastFeeding));
                    mDataFields.Add(new clsDataField("testresult", DataTypes.dbnumber.ToString(), mTestResult));
                    mDataFields.Add(new clsDataField("testresultdate", DataTypes.dbdatetime.ToString(), mTestResultDate));
                    mDataFields.Add(new clsDataField("comments", DataTypes.dbstring.ToString(), mComments.Trim()));
                    mDataFields.Add(new clsDataField("counsellorcode", DataTypes.dbstring.ToString(), mCounselorCode.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));
                    mDataFields.Add(new clsDataField("testresultrcvddate", DataTypes.dbdatetime.ToString(), mTestResultReceviedDate));
                    mDataFields.Add(new clsDataField("cptgiven", DataTypes.dbnumber.ToString(), mGivenCPT));
                   
                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_pcrtests", mDataFields, "autocode=" + mAutoCode);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region referals

                mCommand.CommandText = "delete from ctc_hivtestreferals where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtReferals.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("referalcode", DataTypes.dbstring.ToString(), mDataRow["code"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_hivtestreferals", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region ctc_appointments

                if (clsGlobal.IsNullDate(mNextApptDate) == false)
                {
                    DataTable mDtAppts = new DataTable("appointments");
                    mCommand.CommandText = "select * from ctc_appointments where patientcode='" + mPatientCode
                        + "' and booking='" + mBookingNo + "' and appttype=" + (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.HIVTest;
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAppts);

                    if (mDtAppts.Rows.Count == 0)
                    {
                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                        mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                        mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                        mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                        mDataFields.Add(new clsDataField("appttype", DataTypes.dbnumber.ToString(), (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.HIVTest));
                        mDataFields.Add(new clsDataField("reason", DataTypes.dbstring.ToString(), "HIV Test"));
                        mDataFields.Add(new clsDataField("wheretakencode", DataTypes.dbstring.ToString(), null));
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextApptDate));
                        mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                        mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_appointments", mDataFields);
                        mCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        int mApptAutoCode = Convert.ToInt32(mDtAppts.Rows[0]["autocode"]);

                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextApptDate));

                        mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_appointments", mDataFields, "autocode=" + mApptAutoCode);
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                //this patient has to be available when doing searching
                mCommand.CommandText = "update patients set fromctc=1 where code='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                //commit
                mTrans.Commit();

                //get patient
                mCtcClient = this.Get_Client("code", mPatientCode);

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Edit_PCR
        public AfyaPro_Types.clsCtcClient Edit_PCR(
            int mAutoCode,
            string mPatientCode,
            DateTime? mTransDate,
            DateTime? mSampleDate,
            string mReasonCode,
            int mTestResult,
            DateTime? mTestResultDate,
            DateTime? mTestResultReceviedDate,
            int mGivenCPT,
            string mMotherClientCode,
            string mComments,
            string mCounselorCode,
            int mBreastFeeding,
            DateTime? mNextApptDate,
            DataTable mDtReferals,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Edit_PCR";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Value.Date;

            if (mNextApptDate != null)
            {
                mNextApptDate = mNextApptDate.Value.Date;
            }

            if (mTestResultDate != null)
            {
                mTestResultDate = mTestResultDate.Value.Date;
            }
            if (mTestResultReceviedDate != null)
            {
                mTestResultReceviedDate = mTestResultReceviedDate.Value.Date;
            }

            if (mSampleDate != null)
            {
                mSampleDate = mSampleDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpcrtests_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            string mBookingNo = "";

            #region check for bookingno
            try
            {
                mCommand.CommandText = "select * from ctc_pcrtests where autocode=" + mAutoCode;
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["booking"].ToString().Trim();
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
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

                #region Update Mother client id

                List<clsDataField> mPatientDataFields;
                mPatientDataFields = new List<clsDataField>();
                mPatientDataFields.Add(new clsDataField("motherclientno", DataTypes.dbstring.ToString(), mMotherClientCode.Trim()));
                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_patients", mPatientDataFields, "patientcode=" + mPatientCode.Trim());
                mCommand.ExecuteNonQuery();

                #endregion

                List<clsDataField> mDataFields;

                //ctc_pcrtests
                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("sampledate", DataTypes.dbdatetime.ToString(), mSampleDate));
                mDataFields.Add(new clsDataField("reasoncode", DataTypes.dbstring.ToString(), mReasonCode.Trim()));
                mDataFields.Add(new clsDataField("breastfeeding", DataTypes.dbnumber.ToString(), mBreastFeeding));
                mDataFields.Add(new clsDataField("testresult", DataTypes.dbnumber.ToString(), mTestResult));
                mDataFields.Add(new clsDataField("testresultdate", DataTypes.dbdatetime.ToString(), mTestResultDate));
                mDataFields.Add(new clsDataField("testresultrcvddate", DataTypes.dbdatetime.ToString(), mTestResultReceviedDate));
                mDataFields.Add(new clsDataField("cptgiven", DataTypes.dbnumber.ToString(), mGivenCPT));
                mDataFields.Add(new clsDataField("comments", DataTypes.dbstring.ToString(), mComments.Trim()));
                mDataFields.Add(new clsDataField("counsellorcode", DataTypes.dbstring.ToString(), mCounselorCode.Trim()));
                mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_pcrtests", mDataFields, "autocode=" + mAutoCode);
                mCommand.ExecuteNonQuery();

                #region referals

                mCommand.CommandText = "delete from ctc_hivtestreferals where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtReferals.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("referalcode", DataTypes.dbstring.ToString(), mDataRow["code"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_hivtestreferals", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                //ctc_appointments
                if (clsGlobal.IsNullDate(mNextApptDate) == false)
                {
                    DataTable mDtAppts = new DataTable("appointments");
                    mCommand.CommandText = "select * from ctc_appointments where patientcode='" + mPatientCode
                        + "' and booking='" + mBookingNo + "' and appttype=" + (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.HIVTest;
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAppts);

                    if (mDtAppts.Rows.Count == 0)
                    {
                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                        mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                        mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                        mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                        mDataFields.Add(new clsDataField("appttype", DataTypes.dbnumber.ToString(), (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.HIVTest));
                        mDataFields.Add(new clsDataField("reason", DataTypes.dbstring.ToString(), "HIV Test"));
                        mDataFields.Add(new clsDataField("wheretakencode", DataTypes.dbstring.ToString(), null));
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextApptDate));
                        mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                        mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_appointments", mDataFields);
                        mCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        int mApptAutoCode = Convert.ToInt32(mDtAppts.Rows[0]["autocode"]);

                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextApptDate));

                        mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_appointments", mDataFields, "autocode=" + mApptAutoCode);
                        mCommand.ExecuteNonQuery();
                    }
                }
                else
                {
                    mCommand.CommandText = "delete from ctc_appointments where patientcode='" + mPatientCode
                        + "' and booking='" + mBookingNo + "' and appttype=" + (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.HIVTest;
                    mCommand.ExecuteNonQuery();
                }

                //commit
                mTrans.Commit();

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Delete_PCR
        public AfyaPro_Types.clsCtcClient Delete_PCR(
            int mAutoCode,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Delete_PCR";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpcrtests_delete.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            #region delete
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //this patient has to be available when doing searching
                mCommand.CommandText = "delete from ctc_hivtests where autocode=" + mAutoCode;
                mCommand.ExecuteNonQuery();

                //commit
                mTrans.Commit();

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #endregion

        #region CD4 Testing

        #region Add_CD4Test
        public AfyaPro_Types.clsCtcClient Add_CD4Test(
            string mPatientCode,
            Int16 mGenerateSampleId,
            string mSampleId,
            DateTime mTransDate,
            double mTestResult,
            double mTestResultPercent,
            DateTime? mTestResultDate,
            string mComments,
            string mLabTechnicianCode,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Add_CD4Test";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;

            if (mTestResultDate != null)
            {
                mTestResultDate = mTestResultDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctccd4tests_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            string mBookingNo = "";

            #region check for bookingno
            try
            {
                mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["autocode"].ToString().Trim();

                    if (Convert.ToDateTime(mDataReader["bookdate"]) != mTransDate)
                    {
                        mCtcClient.Exe_Result = 0;
                        mCtcClient.Exe_Message = "Client is not booked for services";
                        return mCtcClient;
                    }
                }
                else
                {
                    mCtcClient.Exe_Result = 0;
                    mCtcClient.Exe_Message = "Client is not booked for services";
                    return mCtcClient;
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion

            #region generate sample id

            if (mGenerateSampleId == 1)
            {
                clsAutoCodes objAutoCodes = new clsAutoCodes();
                AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                mObjCode = objAutoCodes.Next_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcsampleid), "ctc_cd4tests", "sampleid");
                if (mObjCode.Exe_Result == -1)
                {
                    mCtcClient.Exe_Result = mObjCode.Exe_Result;
                    mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                    return mCtcClient;
                }
                mSampleId = mObjCode.GeneratedCode;
            }

            #endregion

            if (mSampleId.Trim() == "")
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.CTC_CD4SampleIdIsInvalid.ToString();
                return mCtcClient;
            }

            #region check 4 duplicate sample id
            try
            {
                mCommand.CommandText = "select * from ctc_cd4tests where sampleid='" + mSampleId.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    if (mDataReader["sampleid"].ToString().Trim() != "")
                    {
                        mCtcClient.Exe_Result = 0;
                        mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.CTC_CD4SampleIdIsInUse.ToString();
                        return mCtcClient;
                    }
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
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

                DataTable mDtData = new DataTable("data");
                mCommand.CommandText = "select * from ctc_cd4tests where patientcode='" + mPatientCode.Trim() + "' and booking='" + mBookingNo + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);

                //check if on arv on not
                DataTable mDtARTT = new DataTable("artt");
                mCommand.CommandText = "select * from ctc_artt where patientcode='" + mPatientCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtARTT);

                int mOnARV = 0;
                if (mDtARTT.Rows.Count > 0)
                {
                    mOnARV = mDtARTT.Rows[0]["onarv"] == DBNull.Value ? 0 : Convert.ToInt32(mDtARTT.Rows[0]["onarv"]);
                }

                if (mDtData.Rows.Count == 0)
                {
                    //ctc_cd4tests
                    List<clsDataField> mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("sampleid", DataTypes.dbstring.ToString(), mSampleId.Trim()));
                    mDataFields.Add(new clsDataField("onarv", DataTypes.dbnumber.ToString(), mOnARV));
                    mDataFields.Add(new clsDataField("testresult", DataTypes.dbdecimal.ToString(), mTestResult));
                    mDataFields.Add(new clsDataField("testresultpercent", DataTypes.dbdecimal.ToString(), mTestResultPercent));
                    mDataFields.Add(new clsDataField("testresultdate", DataTypes.dbdatetime.ToString(), mTestResultDate));
                    mDataFields.Add(new clsDataField("comments", DataTypes.dbstring.ToString(), mComments.Trim()));
                    mDataFields.Add(new clsDataField("labtechniciancode", DataTypes.dbstring.ToString(), mLabTechnicianCode.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_cd4tests", mDataFields);
                    mCommand.ExecuteNonQuery();

                    //increment sampleid
                    if (mGenerateSampleId == 1)
                    {
                        mCommand.CommandText = "update facilityautocodes set "
                        + "idcurrent=idcurrent+idincrement where codekey="
                        + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcsampleid);
                        mCommand.ExecuteNonQuery();
                    }
                }
                else
                {
                    int mAutoCode = Convert.ToInt32(mDtData.Rows[0]["autocode"]);

                    //ctc_cd4tests
                    List<clsDataField> mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("onarv", DataTypes.dbnumber.ToString(), mOnARV));
                    mDataFields.Add(new clsDataField("testresult", DataTypes.dbdecimal.ToString(), mTestResult));
                    mDataFields.Add(new clsDataField("testresultpercent", DataTypes.dbdecimal.ToString(), mTestResultPercent));
                    mDataFields.Add(new clsDataField("testresultdate", DataTypes.dbdatetime.ToString(), mTestResultDate));
                    mDataFields.Add(new clsDataField("comments", DataTypes.dbstring.ToString(), mComments.Trim()));
                    mDataFields.Add(new clsDataField("labtechniciancode", DataTypes.dbstring.ToString(), mLabTechnicianCode.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_cd4tests", mDataFields, "autocode=" + mAutoCode);
                    mCommand.ExecuteNonQuery();
                }

                //this patient has to be available when doing searching
                mCommand.CommandText = "update patients set fromctc=1 where code='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                //commit
                mTrans.Commit();

                //get patient
                mCtcClient = this.Get_Client("code", mPatientCode);

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Edit_CD4Test
        public AfyaPro_Types.clsCtcClient Edit_CD4Test(
            int mAutoCode,
            DateTime mTransDate,
            double mTestResult,
            double mTestResultPercent,
            DateTime? mTestResultDate,
            string mComments,
            string mLabTechnicianCode,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Edit_CD4Test";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;

            if (mTestResultDate != null)
            {
                mTestResultDate = mTestResultDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctccd4tests_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            #region edit
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //ctc_hivtests
                List<clsDataField> mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("testresult", DataTypes.dbdecimal.ToString(), mTestResult));
                mDataFields.Add(new clsDataField("testresultpercent", DataTypes.dbdecimal.ToString(), mTestResultPercent));
                mDataFields.Add(new clsDataField("testresultdate", DataTypes.dbdatetime.ToString(), mTestResultDate));
                mDataFields.Add(new clsDataField("comments", DataTypes.dbstring.ToString(), mComments.Trim()));
                mDataFields.Add(new clsDataField("labtechniciancode", DataTypes.dbstring.ToString(), mLabTechnicianCode.Trim()));
                mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_cd4tests", mDataFields, "autocode=" + mAutoCode);
                mCommand.ExecuteNonQuery();

                //commit
                mTrans.Commit();

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Delete_CD4Test
        public AfyaPro_Types.clsCtcClient Delete_CD4Test(
            int mAutoCode,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Delete_CD4Test";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctccd4tests_delete.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            #region delete
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //this patient has to be available when doing searching
                mCommand.CommandText = "delete from ctc_cd4tests where autocode=" + mAutoCode;
                mCommand.ExecuteNonQuery();

                //commit
                mTrans.Commit();

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #endregion

        #region PreART

        #region Enroll_PreART
        public AfyaPro_Types.clsCtcClient Enroll_PreART(
            string mPatientCode,
            Int16 mGenerateHTCNo,
            string mHTCNo,
            DateTime mTransDate,
            string mReferedFrom,
            DateTime? mTransferInDate,
            string mHIVCareClinicNo,
            string mConfirmSite,
            DateTime? mConfirmDate,
            int mConfirmTestType,
            int mClinicalStage,
            string mClinicalConditions,
            int mTakenIPT,
            DateTime? mIPTDate,
            int mTakenTBTreatment,
            DateTime? mTBTreatmentDate,
            string mTBRegNo,
            int mTakenARV,
            DateTime? mARVDate,
            string mARVType,
            string mEntryMode,
            string mLatestOutcome,
            DateTime? mDateofInitiation,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Enroll_PreART";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;

            if (mTransferInDate != null)
            {
                mTransferInDate = mTransferInDate.Value.Date;
            }

            if (mConfirmDate != null)
            {
                mConfirmDate = mConfirmDate.Value.Date;
            }

            if (mIPTDate != null)
            {
                mIPTDate = mIPTDate.Value.Date;
            }

            if (mTBTreatmentDate != null)
            {
                mTBTreatmentDate = mTBTreatmentDate.Value.Date;
            }

            if (mARVDate != null)
            {
                mARVDate = mARVDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpreart_enroll.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            DataTable mDtPreART = new DataTable("preart");
            mCommand.CommandText = "select * from ctc_preart where patientcode='" + mPatientCode.Trim() + "'";
            mDataAdapter.SelectCommand = mCommand;
            mDataAdapter.Fill(mDtPreART);

            string mBookingNo = "";

            if (mDtPreART.Rows.Count == 0)
            {
                #region check for bookingno
                try
                {
                    mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                    mDataReader = mCommand.ExecuteReader();
                    if (mDataReader.Read() == true)
                    {
                        mBookingNo = mDataReader["autocode"].ToString().Trim();

                        if (Convert.ToDateTime(mDataReader["bookdate"]) != mTransDate)
                        {
                            mCtcClient.Exe_Result = 0;
                            mCtcClient.Exe_Message = "Client is not booked for services";
                            return mCtcClient;
                        }
                    }
                    else
                    {
                        mCtcClient.Exe_Result = 0;
                        mCtcClient.Exe_Message = "Client is not booked for services";
                        return mCtcClient;
                    }
                }
                catch (OdbcException ex)
                {
                    mCtcClient.Exe_Result = -1;
                    mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                    return mCtcClient;
                }
                finally
                {
                    mDataReader.Close();
                }
                #endregion

                #region htc #

                #region auto generate htc #, if option is on

                if (mGenerateHTCNo == 1)
                {
                    clsAutoCodes objAutoCodes = new clsAutoCodes();
                    AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                    mObjCode = objAutoCodes.Next_Code(
                        Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.vctnumbers), "ctc_patients", "htcno");
                    if (mObjCode.Exe_Result == -1)
                    {
                        mCtcClient.Exe_Result = mObjCode.Exe_Result;
                        mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                        return mCtcClient;
                    }
                    mHTCNo = mObjCode.GeneratedCode;
                }

                #endregion

                #region check 4 duplicate htcno
                try
                {
                    mCommand.CommandText = "select * from ctc_patients where htcno='" + mHTCNo.Trim() + "' and patientcode<>'" + mPatientCode.Trim() + "'";
                    mDataReader = mCommand.ExecuteReader();
                    if (mDataReader.Read() == true)
                    {
                        if (mDataReader["htcno"].ToString().Trim() != "")
                        {
                            mCtcClient.Exe_Result = 0;
                            mCtcClient.Exe_Message = "HTC # is in use by another client";
                            return mCtcClient;
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    mCtcClient.Exe_Result = -1;
                    mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                    return mCtcClient;
                }
                finally
                {
                    mDataReader.Close();
                }
                #endregion

                #endregion
            }

            #region add/edit
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                List<clsDataField> mDataFields;

                #region ctc_patients

                DataTable mDtClients = new DataTable("clients");
                mCommand.CommandText = "select * from ctc_patients where patientcode='" + mPatientCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtClients);

                if (mDtClients.Rows.Count == 0)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("htcno", DataTypes.dbstring.ToString(), mHTCNo.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patients", mDataFields);
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("htcno", DataTypes.dbstring.ToString(), mHTCNo.Trim()));

                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_patients", mDataFields, "patientcode='" + mPatientCode.Trim() + "'");
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region ctc_preart

                if (mDtPreART.Rows.Count == 0)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_preart", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("hivcareclinicno", DataTypes.dbstring.ToString(), mHIVCareClinicNo));
                mDataFields.Add(new clsDataField("confirmsite", DataTypes.dbstring.ToString(), mConfirmSite));
                mDataFields.Add(new clsDataField("referedfrom", DataTypes.dbstring.ToString(), mReferedFrom.Trim()));
                mDataFields.Add(new clsDataField("transferindate", DataTypes.dbdatetime.ToString(), mTransferInDate));
                mDataFields.Add(new clsDataField("confirmdate", DataTypes.dbdatetime.ToString(), mConfirmDate));
                mDataFields.Add(new clsDataField("confirmtesttype", DataTypes.dbnumber.ToString(), mConfirmTestType));
                mDataFields.Add(new clsDataField("clinicalstage", DataTypes.dbdecimal.ToString(), mClinicalStage));
                mDataFields.Add(new clsDataField("clinicalconditions", DataTypes.dbstring.ToString(), mClinicalConditions));
                mDataFields.Add(new clsDataField("takenipt", DataTypes.dbnumber.ToString(), mTakenIPT));
                mDataFields.Add(new clsDataField("iptdate", DataTypes.dbdatetime.ToString(), mIPTDate));
                mDataFields.Add(new clsDataField("takentbtreatment", DataTypes.dbnumber.ToString(), mTakenTBTreatment));
                mDataFields.Add(new clsDataField("tbtreatmentdate", DataTypes.dbdatetime.ToString(), mTBTreatmentDate));
                mDataFields.Add(new clsDataField("tbregno", DataTypes.dbstring.ToString(), mTBRegNo));
                mDataFields.Add(new clsDataField("takenarv", DataTypes.dbnumber.ToString(), mTakenARV));
                mDataFields.Add(new clsDataField("arvdate", DataTypes.dbdatetime.ToString(), mARVDate));
                mDataFields.Add(new clsDataField("arvtype", DataTypes.dbstring.ToString(), mARVType));
                mDataFields.Add(new clsDataField("modeofentry", DataTypes.dbstring.ToString(), mEntryMode));
                mDataFields.Add(new clsDataField("latestoutcome", DataTypes.dbstring.ToString(), mLatestOutcome));
                mDataFields.Add(new clsDataField("dateofinitiation", DataTypes.dbdatetime.ToString(), mDateofInitiation));
                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_preart", mDataFields, "patientcode='" + mPatientCode.Trim() + "'");
                mCommand.ExecuteNonQuery();

                #endregion

                if (mGenerateHTCNo == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.vctnumbers);
                    mCommand.ExecuteNonQuery();
                }

                //commit
                mTrans.Commit();

                //get patient
                mCtcClient = this.Get_Client("code", mPatientCode);

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, mCommand.CommandText);
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Add_PreART
        public AfyaPro_Types.clsCtcClient Add_PreART(
            string mPatientCode,
            DateTime mTransDate,
            string mWastingCode,
            string mTBStatusCode,
            int mClinicalStage,
            int mPregnant,
            double mIPTTablets,
            double mCPTTablets,
            int mDepoGiven,
            int mCondoms,
            string mFollowUpStatusCode,
            DateTime? mNextVisitDate,
            DateTime? mOutcomeDate,
            string mClinicianCode,
            string mNotes,
            DataTable mDtSideEffects,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Add_PreART";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;
            if (clsGlobal.IsNullDate(mNextVisitDate) == false)
            {
                mNextVisitDate = mNextVisitDate.Value.Date;
            }
            if (clsGlobal.IsNullDate(mOutcomeDate) == false)
            {
                mOutcomeDate = mOutcomeDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpreart_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            string mBookingNo = "";

            #region check for bookingno
            try
            {
                mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["autocode"].ToString().Trim();

                    if (Convert.ToDateTime(mDataReader["bookdate"]) != mTransDate)
                    {
                        mCtcClient.Exe_Result = 0;
                        mCtcClient.Exe_Message = "Client is not booked for services";
                        return mCtcClient;
                    }
                }
                else
                {
                    mCtcClient.Exe_Result = 0;
                    mCtcClient.Exe_Message = "Client is not booked for services";
                    return mCtcClient;
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
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

                List<clsDataField> mDataFields;

                #region ctc_preartlog

                DataTable mDtData = new DataTable("data");
                mCommand.CommandText = "select * from ctc_preartlog where patientcode='" + mPatientCode.Trim() + "' and booking='" + mBookingNo + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);

                if (mDtData.Rows.Count == 0)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("wastingcode", DataTypes.dbstring.ToString(), mWastingCode.Trim()));
                    mDataFields.Add(new clsDataField("tbstatuscode", DataTypes.dbstring.ToString(), mTBStatusCode.Trim()));
                    mDataFields.Add(new clsDataField("clinicalstage", DataTypes.dbnumber.ToString(), mClinicalStage));
                    mDataFields.Add(new clsDataField("pregnant", DataTypes.dbnumber.ToString(), mPregnant));
                    mDataFields.Add(new clsDataField("ipttablets", DataTypes.dbdecimal.ToString(), mIPTTablets));
                    mDataFields.Add(new clsDataField("cpttablets", DataTypes.dbdecimal.ToString(), mCPTTablets));
                    mDataFields.Add(new clsDataField("depogiven", DataTypes.dbnumber.ToString(), mDepoGiven));
                    mDataFields.Add(new clsDataField("condoms", DataTypes.dbnumber.ToString(), mCondoms));
                    mDataFields.Add(new clsDataField("followupstatuscode", DataTypes.dbstring.ToString(), mFollowUpStatusCode));
                    mDataFields.Add(new clsDataField("outcomedate", DataTypes.dbdatetime.ToString(), mOutcomeDate));
                    mDataFields.Add(new clsDataField("cliniciancode", DataTypes.dbstring.ToString(), mClinicianCode));
                    mDataFields.Add(new clsDataField("notes", DataTypes.dbstring.ToString(), mNotes.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_preartlog", mDataFields);
                    mCommand.ExecuteNonQuery();

                   
                }
                else
                {
                    int mAutoCode = Convert.ToInt32(mDtData.Rows[0]["autocode"]);

                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("wastingcode", DataTypes.dbstring.ToString(), mWastingCode.Trim()));
                    mDataFields.Add(new clsDataField("tbstatuscode", DataTypes.dbstring.ToString(), mTBStatusCode.Trim()));
                    mDataFields.Add(new clsDataField("clinicalstage", DataTypes.dbnumber.ToString(), mClinicalStage));
                    mDataFields.Add(new clsDataField("pregnant", DataTypes.dbnumber.ToString(), mPregnant));
                    mDataFields.Add(new clsDataField("ipttablets", DataTypes.dbdecimal.ToString(), mIPTTablets));
                    mDataFields.Add(new clsDataField("cpttablets", DataTypes.dbdecimal.ToString(), mCPTTablets));
                    mDataFields.Add(new clsDataField("depogiven", DataTypes.dbnumber.ToString(), mDepoGiven));
                    mDataFields.Add(new clsDataField("condoms", DataTypes.dbnumber.ToString(), mCondoms));
                    mDataFields.Add(new clsDataField("followupstatuscode", DataTypes.dbstring.ToString(), mFollowUpStatusCode));
                    mDataFields.Add(new clsDataField("outcomedate", DataTypes.dbdatetime.ToString(), mOutcomeDate));
                    mDataFields.Add(new clsDataField("cliniciancode", DataTypes.dbstring.ToString(), mClinicianCode));
                    mDataFields.Add(new clsDataField("notes", DataTypes.dbstring.ToString(), mNotes.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));
                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_preartlog", mDataFields, "autocode=" + mAutoCode);
                    mCommand.ExecuteNonQuery();

                  
                }

                //this patient has to be available when doing searching
                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("fromctc", DataTypes.dbnumber.ToString(), 1));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("patients", mDataFields, "code='" + mPatientCode.Trim() + "'");
                mCommand.ExecuteNonQuery();

                OdbcCommand cmdPUpdate = new OdbcCommand();


                cmdPUpdate.Connection = mConn;
                cmdPUpdate.Transaction = mTrans;
              
                cmdPUpdate.CommandText = "update ctc_preart set latestoutcome ='" + mFollowUpStatusCode + "' where patientcode ='" + mPatientCode.Trim() + "'";
                cmdPUpdate.ExecuteNonQuery();
               // clsGlobal.Write_Error(pClassName, mFunctionName, "update ctc_preart set latestoutcome ='" + mFollowUpStatusCode + "' where patientcode ='" + mPatientCode.Trim() + "'");
               
                #endregion

                #region side effects

                mCommand.CommandText = "delete from ctc_patientaidsillness where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtSideEffects.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("illnesscode", DataTypes.dbstring.ToString(), mDataRow["code"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patientaidsillness", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region ctc_appointments

                if (clsGlobal.IsNullDate(mNextVisitDate) == false)
                {
                    DataTable mDtAppts = new DataTable("appointments");
                    mCommand.CommandText = "select * from ctc_appointments where patientcode='" + mPatientCode
                        + "' and booking='" + mBookingNo + "' and appttype=" + (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit;
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAppts);

                    if (mDtAppts.Rows.Count == 0)
                    {
                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                        mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                        mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                        mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                        mDataFields.Add(new clsDataField("appttype", DataTypes.dbnumber.ToString(), (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit));
                        mDataFields.Add(new clsDataField("reason", DataTypes.dbstring.ToString(), "Pre-ART Visit"));
                        mDataFields.Add(new clsDataField("wheretakencode", DataTypes.dbstring.ToString(), null));
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextVisitDate));
                        mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                        mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_appointments", mDataFields);
                        mCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        int mApptAutoCode = Convert.ToInt32(mDtAppts.Rows[0]["autocode"]);

                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextVisitDate));

                        mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_appointments", mDataFields, "autocode=" + mApptAutoCode);
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                //commit
                mTrans.Commit();

                //get patient
                mCtcClient = this.Get_Client("code", mPatientCode);

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Edit_PreART
        public AfyaPro_Types.clsCtcClient Edit_PreART(
            int mAutoCode,
            DateTime mTransDate,
            string mWastingCode,
            string mTBStatusCode,
            int mClinicalStage,
            int mPregnant,
            double mIPTTablets,
            double mCPTTablets,
            int mDepoGiven,
            int mCondoms,
            string mFollowUpStatusCode,
            DateTime? mNextVisitDate,
            DateTime? mOutcomeDate,
            string mClinicianCode,
            string mNotes,
            DataTable mDtSideEffects,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Edit_PreART";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;
            if (clsGlobal.IsNullDate(mNextVisitDate) == false)
            {
                mNextVisitDate = mNextVisitDate.Value.Date;
            }
            if (clsGlobal.IsNullDate(mOutcomeDate) == false)
            {
                mOutcomeDate = mOutcomeDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpreart_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            string mBookingNo = "";
            string mPatientCode = "";

            #region check for bookingno
            try
            {
                mCommand.CommandText = "select * from ctc_preartlog where autocode=" + mAutoCode;
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["booking"].ToString().Trim();
                    mPatientCode = mDataReader["patientcode"].ToString().Trim();
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
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

                #region ctc_preartlog

                List<clsDataField> mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                mDataFields.Add(new clsDataField("wastingcode", DataTypes.dbstring.ToString(), mWastingCode.Trim()));
                mDataFields.Add(new clsDataField("tbstatuscode", DataTypes.dbstring.ToString(), mTBStatusCode.Trim()));
                mDataFields.Add(new clsDataField("clinicalstage", DataTypes.dbnumber.ToString(), mClinicalStage));
                mDataFields.Add(new clsDataField("pregnant", DataTypes.dbnumber.ToString(), mPregnant));
                mDataFields.Add(new clsDataField("ipttablets", DataTypes.dbdecimal.ToString(), mIPTTablets));
                mDataFields.Add(new clsDataField("cpttablets", DataTypes.dbdecimal.ToString(), mCPTTablets));
                mDataFields.Add(new clsDataField("depogiven", DataTypes.dbnumber.ToString(), mDepoGiven));
                mDataFields.Add(new clsDataField("condoms", DataTypes.dbnumber.ToString(), mCondoms));
                mDataFields.Add(new clsDataField("followupstatuscode", DataTypes.dbstring.ToString(), mFollowUpStatusCode));
                mDataFields.Add(new clsDataField("outcomedate", DataTypes.dbdatetime.ToString(), mOutcomeDate));
                mDataFields.Add(new clsDataField("cliniciancode", DataTypes.dbstring.ToString(), mClinicianCode));
                mDataFields.Add(new clsDataField("notes", DataTypes.dbstring.ToString(), mNotes.Trim()));
                mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_preartlog", mDataFields, "autocode=" + mAutoCode);
                mCommand.ExecuteNonQuery();

                OdbcCommand cmdPUpdate = new OdbcCommand();

                cmdPUpdate.Connection = mConn;
                cmdPUpdate.Transaction = mTrans;

                cmdPUpdate.Connection = mConn;
                cmdPUpdate.CommandText = "update ctc_preart set latestoutcome ='" + mFollowUpStatusCode + "' where patientcode ='" + mPatientCode.Trim() + "'";
                cmdPUpdate.ExecuteNonQuery();
                //clsGlobal.Write_Error(pClassName, mFunctionName, "update ctc_preart set latestoutcome ='" + mFollowUpStatusCode + "' where patientcode ='" + mPatientCode.Trim() + "'");

                #endregion

                #region side effects

                mCommand.CommandText = "delete from ctc_patientaidsillness where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtSideEffects.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("illnesscode", DataTypes.dbstring.ToString(), mDataRow["code"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patientaidsillness", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region ctc_appointments

                if (clsGlobal.IsNullDate(mNextVisitDate) == false)
                {
                    DataTable mDtAppts = new DataTable("appointments");
                    mCommand.CommandText = "select * from ctc_appointments where patientcode='" + mPatientCode
                        + "' and booking='" + mBookingNo + "' and appttype=" + (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit;
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAppts);

                    if (mDtAppts.Rows.Count == 0)
                    {
                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                        mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                        mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                        mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                        mDataFields.Add(new clsDataField("appttype", DataTypes.dbnumber.ToString(), (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit));
                        mDataFields.Add(new clsDataField("reason", DataTypes.dbstring.ToString(), "Pre-ART Visit"));
                        mDataFields.Add(new clsDataField("wheretakencode", DataTypes.dbstring.ToString(), null));
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextVisitDate));
                        mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                        mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_appointments", mDataFields);
                        mCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        int mApptAutoCode = Convert.ToInt32(mDtAppts.Rows[0]["autocode"]);

                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextVisitDate));

                        mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_appointments", mDataFields, "autocode=" + mApptAutoCode);
                        mCommand.ExecuteNonQuery();
                    }
                }
                else
                {
                    mCommand.CommandText = "delete from ctc_appointments where patientcode='" + mPatientCode
                        + "' and booking='" + mBookingNo + "' and appttype=" + (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit;
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                //commit
                mTrans.Commit();

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Delete_PreART
        public AfyaPro_Types.clsCtcClient Delete_PreART(
            int mAutoCode,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Delete_PreART";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpreart_delete.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            #region delete
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //ctc_preartlog
                mCommand.CommandText = "delete from ctc_preartlog where autocode=" + mAutoCode;
                mCommand.ExecuteNonQuery();

                //commit
                mTrans.Commit();

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #endregion
        
        #region ART

        #region Enroll_ART
        public AfyaPro_Types.clsCtcClient Enroll_ART(
            string mPatientCode,
            Int16 mGenerateHTCNo,
            string mCTCNo,
            DateTime mTransDate,
            DateTime? mTransferInDate,
            string mHIVTestSite,
            DateTime? mHIVTestDate,
            int mHIVTestType,
            int mClinicalStage,
            //string mClinicalConditions,
            double mCD4Count,
            double mCD4CountPercent,
            DateTime? mCD4Date,
            double mWeight,
            double mHeight,
            int mTBInitialStatus,
            DateTime? mTBTreatmentDate,
            string mTBRegNo,
            int mARTEdu,
            DateTime? mARTEduDate,
            int mKS,
            int mPregnant,
            int mTakenARV,
            DateTime? mARVDate,
            //string mARVType,
            string mARTRegimenCode1,
            DateTime? mARTRegimenDate1,
            string mARTRegimenCode2,
            DateTime? mARTRegimenDate2,
            DateTime? mARTinitiationDate,
            string mARTEntryMode,
            string mLatestOutcome,
            string mArtStartReason,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Enroll_ART";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;

            if (mTransferInDate != null)
            {
                mTransferInDate = mTransferInDate.Value.Date;
            }

            if (mHIVTestDate != null)
            {
                mHIVTestDate = mHIVTestDate.Value.Date;
            }

            if (mARTEduDate != null)
            {
                mARTEduDate = mARTEduDate.Value.Date;
            }

            if (mTBTreatmentDate != null)
            {
                mTBTreatmentDate = mTBTreatmentDate.Value.Date;
            }

            if (mARVDate != null)
            {
                mARVDate = mARVDate.Value.Date;
            }

            if (mARTRegimenDate1 != null)
            {
                mARTRegimenDate1 = mARTRegimenDate1.Value.Date;
            }

            if (mARTRegimenDate2 != null)
            {
                mARTRegimenDate2 = mARTRegimenDate2.Value.Date;
            }

            if (mCD4Date != null)
            {
                mCD4Date = mCD4Date.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcart_enroll.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            DataTable mDtPreART = new DataTable("art");
            mCommand.CommandText = "select * from ctc_art where patientcode='" + mPatientCode.Trim() + "'";
            mDataAdapter.SelectCommand = mCommand;
            mDataAdapter.Fill(mDtPreART);

            string mBookingNo = "";

            if (mDtPreART.Rows.Count == 0)
            {
                #region check for bookingno
                try
                {
                    mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                    mDataReader = mCommand.ExecuteReader();
                    if (mDataReader.Read() == true)
                    {
                        mBookingNo = mDataReader["autocode"].ToString().Trim();

                        if (Convert.ToDateTime(mDataReader["bookdate"]) != mTransDate)
                        {
                            mCtcClient.Exe_Result = 0;
                            mCtcClient.Exe_Message = "Client is not booked for services";
                            return mCtcClient;
                        }
                    }
                    else
                    {
                        mCtcClient.Exe_Result = 0;
                        mCtcClient.Exe_Message = "Client is not booked for services";
                        return mCtcClient;
                    }
                }
                catch (OdbcException ex)
                {
                    mCtcClient.Exe_Result = -1;
                    mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                    return mCtcClient;
                }
                finally
                {
                    mDataReader.Close();
                }
                #endregion

                #region auto generate ctc #, if option is on

                if (mGenerateHTCNo == 1)
                {
                    clsAutoCodes objAutoCodes = new clsAutoCodes();
                    AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                    mObjCode = objAutoCodes.Next_Code(
                        Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcnumbers), "ctc_patients", "ctcno");
                    if (mObjCode.Exe_Result == -1)
                    {
                        mCtcClient.Exe_Result = mObjCode.Exe_Result;
                        mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                        return mCtcClient;
                    }
                    mCTCNo = mObjCode.GeneratedCode;
                }

                #endregion

                #region check 4 duplicate ctcno
                try
                {
                    mCommand.CommandText = "select * from ctc_patients where ctcno='" + mCTCNo.Trim() + "' and patientcode<>'" + mPatientCode.Trim() + "'";
                    mDataReader = mCommand.ExecuteReader();
                    if (mDataReader.Read() == true)
                    {
                        if (mDataReader["ctcno"].ToString().Trim() != "")
                        {
                            mCtcClient.Exe_Result = 0;
                            mCtcClient.Exe_Message = "ART Registration # is in use by another client";
                            return mCtcClient;
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    mCtcClient.Exe_Result = -1;
                    mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                    return mCtcClient;
                }
                finally
                {
                    mDataReader.Close();
                }
                #endregion
            }

            #region add/edit
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                List<clsDataField> mDataFields;

                #region ctc_patients

                DataTable mDtClients = new DataTable("clients");
                mCommand.CommandText = "select * from ctc_patients where patientcode='" + mPatientCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtClients);

                if (mDtClients.Rows.Count == 0)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("ctcno", DataTypes.dbstring.ToString(), mCTCNo.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patients", mDataFields);
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("ctcno", DataTypes.dbstring.ToString(), mCTCNo.Trim()));

                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_patients", mDataFields, "patientcode='" + mPatientCode.Trim() + "'");
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region ctc_art

                if (mDtPreART.Rows.Count == 0)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_art", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("transferindate", DataTypes.dbdatetime.ToString(), mTransferInDate));
                mDataFields.Add(new clsDataField("hivtestsite", DataTypes.dbstring.ToString(), mHIVTestSite));
                mDataFields.Add(new clsDataField("hivtestdate", DataTypes.dbdatetime.ToString(), mHIVTestDate));
                mDataFields.Add(new clsDataField("hivtesttype", DataTypes.dbnumber.ToString(), mHIVTestType));
                mDataFields.Add(new clsDataField("clinicalstage", DataTypes.dbdecimal.ToString(), mClinicalStage));
                //mDataFields.Add(new clsDataField("clinicalconditions", DataTypes.dbstring.ToString(), mClinicalConditions));
                mDataFields.Add(new clsDataField("cd4count", DataTypes.dbdecimal.ToString(), mCD4Count));
                mDataFields.Add(new clsDataField("cd4countpercent", DataTypes.dbdecimal.ToString(), mCD4CountPercent));
                mDataFields.Add(new clsDataField("cd4date", DataTypes.dbdatetime.ToString(), mCD4Date));
                mDataFields.Add(new clsDataField("weight", DataTypes.dbdecimal.ToString(), mWeight));
                mDataFields.Add(new clsDataField("height", DataTypes.dbdecimal.ToString(), mHeight));
                mDataFields.Add(new clsDataField("tbinitialstatus", DataTypes.dbnumber.ToString(), mTBInitialStatus));
                mDataFields.Add(new clsDataField("tbtreatmentdate", DataTypes.dbdatetime.ToString(), mTBTreatmentDate));
                mDataFields.Add(new clsDataField("tbregno", DataTypes.dbstring.ToString(), mTBRegNo));
                mDataFields.Add(new clsDataField("artedu", DataTypes.dbnumber.ToString(), mARTEdu));
                mDataFields.Add(new clsDataField("artedudate", DataTypes.dbdatetime.ToString(), mARTEduDate));
                mDataFields.Add(new clsDataField("ks", DataTypes.dbnumber.ToString(), mKS));
                mDataFields.Add(new clsDataField("pregnant", DataTypes.dbnumber.ToString(), mPregnant));
                mDataFields.Add(new clsDataField("takenarv", DataTypes.dbnumber.ToString(), mTakenARV));
                mDataFields.Add(new clsDataField("arvdate", DataTypes.dbdatetime.ToString(), mARVDate));
                //mDataFields.Add(new clsDataField("arvtype", DataTypes.dbstring.ToString(), mARVType));
                mDataFields.Add(new clsDataField("artregimencode1", DataTypes.dbstring.ToString(), mARTRegimenCode1));
                mDataFields.Add(new clsDataField("artregimendate1", DataTypes.dbdatetime.ToString(), mARTRegimenDate1));
                mDataFields.Add(new clsDataField("artregimencode2", DataTypes.dbstring.ToString(), mARTRegimenCode2));
                mDataFields.Add(new clsDataField("artregimendate2", DataTypes.dbdatetime.ToString(), mARTRegimenDate2));
                mDataFields.Add(new clsDataField("artinitiationdate", DataTypes.dbdatetime.ToString(), mARTinitiationDate));
                mDataFields.Add(new clsDataField("arventrymode", DataTypes.dbstring.ToString(), mARTEntryMode));
                mDataFields.Add(new clsDataField("latestoutcome", DataTypes.dbstring.ToString(), mLatestOutcome));
                mDataFields.Add(new clsDataField("arvstartreason", DataTypes.dbstring.ToString(), mArtStartReason));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_art", mDataFields, "patientcode='" + mPatientCode.Trim() + "'");
                mCommand.ExecuteNonQuery();

                #endregion

                if (mGenerateHTCNo == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcnumbers);
                    mCommand.ExecuteNonQuery();
                }

                //commit
                mTrans.Commit();

                //get patient
                mCtcClient = this.Get_Client("code", mPatientCode);

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, mCommand.CommandText);
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Add_ART
        public AfyaPro_Types.clsCtcClient Add_ART(
            string mPatientCode,
            DateTime mTransDate,
            string mOutcomeCode,
            DateTime? mOutcomeDate,
            string mARTRegimenCode,
            string mTBStatusCode,
            double mPillCount,
            double mDosesMissed,
            double mARVTablets,
            int mARVTo,
            double mCPTTablets,
            int mDepoGiven,
            int mCondoms,
            DateTime? mViralLoadDate,
            string mViralLoadResult,
            DateTime? mNextVisitDate,
            string mClinicianCode,
            string mNotes,
            DataTable mDtSideEffects,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Add_ART";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;
            if (clsGlobal.IsNullDate(mNextVisitDate) == false)
            {
                mNextVisitDate = mNextVisitDate.Value.Date;
            }
            if (clsGlobal.IsNullDate(mViralLoadDate) == false)
            {
                mViralLoadDate = mViralLoadDate.Value.Date;
            }
            if (clsGlobal.IsNullDate(mOutcomeDate) == false)
            {
                mOutcomeDate = mOutcomeDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcart_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            string mBookingNo = "";

            #region check for bookingno
            try
            {
                mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["autocode"].ToString().Trim();

                    if (Convert.ToDateTime(mDataReader["bookdate"]) != mTransDate)
                    {
                        mCtcClient.Exe_Result = 0;
                        mCtcClient.Exe_Message = "Client is not booked for services";
                        return mCtcClient;
                    }
                }
                else
                {
                    mCtcClient.Exe_Result = 0;
                    mCtcClient.Exe_Message = "Client is not booked for services";
                    return mCtcClient;
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
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

                List<clsDataField> mDataFields;

                #region ctc_artlog

                DataTable mDtData = new DataTable("data");
                mCommand.CommandText = "select * from ctc_artlog where patientcode='" + mPatientCode.Trim() + "' and booking='" + mBookingNo + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);

                if (mDtData.Rows.Count == 0)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("outcomecode", DataTypes.dbstring.ToString(), mOutcomeCode.Trim()));
                    mDataFields.Add(new clsDataField("outcomedate", DataTypes.dbdatetime.ToString(), mOutcomeDate));
                    mDataFields.Add(new clsDataField("artregimencode", DataTypes.dbstring.ToString(), mARTRegimenCode.Trim()));
                    mDataFields.Add(new clsDataField("tbstatuscode", DataTypes.dbstring.ToString(), mTBStatusCode.Trim()));
                    mDataFields.Add(new clsDataField("pillcount", DataTypes.dbdecimal.ToString(), mPillCount));
                    mDataFields.Add(new clsDataField("dosesmissed", DataTypes.dbdecimal.ToString(), mDosesMissed));
                    mDataFields.Add(new clsDataField("arvtablets", DataTypes.dbdecimal.ToString(), mARVTablets));
                    mDataFields.Add(new clsDataField("arvto", DataTypes.dbnumber.ToString(), mARVTo));
                    mDataFields.Add(new clsDataField("cpttablets", DataTypes.dbdecimal.ToString(), mCPTTablets));
                    mDataFields.Add(new clsDataField("depogiven", DataTypes.dbnumber.ToString(), mDepoGiven));
                    mDataFields.Add(new clsDataField("condoms", DataTypes.dbnumber.ToString(), mCondoms));
                    mDataFields.Add(new clsDataField("viralloaddate", DataTypes.dbdatetime.ToString(), mViralLoadDate));
                    mDataFields.Add(new clsDataField("viralloadresult", DataTypes.dbstring.ToString(), mViralLoadResult));
                    mDataFields.Add(new clsDataField("cliniciancode", DataTypes.dbstring.ToString(), mClinicianCode));
                    mDataFields.Add(new clsDataField("notes", DataTypes.dbstring.ToString(), mNotes.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_artlog", mDataFields);
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    int mAutoCode = Convert.ToInt32(mDtData.Rows[0]["autocode"]);

                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("outcomecode", DataTypes.dbstring.ToString(), mOutcomeCode.Trim()));
                    mDataFields.Add(new clsDataField("outcomedate", DataTypes.dbdatetime.ToString(), mOutcomeDate));
                    mDataFields.Add(new clsDataField("artregimencode", DataTypes.dbstring.ToString(), mARTRegimenCode.Trim()));
                    mDataFields.Add(new clsDataField("tbstatuscode", DataTypes.dbstring.ToString(), mTBStatusCode.Trim()));
                    mDataFields.Add(new clsDataField("pillcount", DataTypes.dbdecimal.ToString(), mPillCount));
                    mDataFields.Add(new clsDataField("dosesmissed", DataTypes.dbdecimal.ToString(), mDosesMissed));
                    mDataFields.Add(new clsDataField("arvtablets", DataTypes.dbdecimal.ToString(), mARVTablets));
                    mDataFields.Add(new clsDataField("arvto", DataTypes.dbnumber.ToString(), mARVTo));
                    mDataFields.Add(new clsDataField("cpttablets", DataTypes.dbdecimal.ToString(), mCPTTablets));
                    mDataFields.Add(new clsDataField("depogiven", DataTypes.dbnumber.ToString(), mDepoGiven));
                    mDataFields.Add(new clsDataField("condoms", DataTypes.dbnumber.ToString(), mCondoms));
                    mDataFields.Add(new clsDataField("viralloaddate", DataTypes.dbdatetime.ToString(), mViralLoadDate));
                    mDataFields.Add(new clsDataField("viralloadresult", DataTypes.dbstring.ToString(), mViralLoadResult));
                    mDataFields.Add(new clsDataField("cliniciancode", DataTypes.dbstring.ToString(), mClinicianCode));
                    mDataFields.Add(new clsDataField("notes", DataTypes.dbstring.ToString(), mNotes.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_artlog", mDataFields, "autocode=" + mAutoCode);
                    mCommand.ExecuteNonQuery();
                }

                //this patient has to be available when doing searching
                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("fromctc", DataTypes.dbnumber.ToString(), 1));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("patients", mDataFields, "code='" + mPatientCode.Trim() + "'");
                mCommand.ExecuteNonQuery();

                OdbcCommand cmdPUpdate = new OdbcCommand();

                cmdPUpdate.Connection = mConn;
                cmdPUpdate.Transaction = mTrans;

                cmdPUpdate.Connection = mConn;
                cmdPUpdate.CommandText = "update ctc_art set latestoutcome ='" + mOutcomeCode + "' where patientcode ='" + mPatientCode.Trim() + "'";
                cmdPUpdate.ExecuteNonQuery();

                #endregion

                #region side effects

                mCommand.CommandText = "delete from ctc_patientaidsillness where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtSideEffects.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("illnesscode", DataTypes.dbstring.ToString(), mDataRow["code"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patientaidsillness", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region ctc_appointments

                if (clsGlobal.IsNullDate(mNextVisitDate) == false)
                {
                    DataTable mDtAppts = new DataTable("appointments");
                    mCommand.CommandText = "select * from ctc_appointments where patientcode='" + mPatientCode
                        + "' and booking='" + mBookingNo + "' and appttype=" + (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit;
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAppts);

                    if (mDtAppts.Rows.Count == 0)
                    {
                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                        mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                        mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                        mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                        mDataFields.Add(new clsDataField("appttype", DataTypes.dbnumber.ToString(), (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit));
                        mDataFields.Add(new clsDataField("reason", DataTypes.dbstring.ToString(), "Pre-ART Visit"));
                        mDataFields.Add(new clsDataField("wheretakencode", DataTypes.dbstring.ToString(), null));
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextVisitDate));
                        mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                        mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_appointments", mDataFields);
                        mCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        int mApptAutoCode = Convert.ToInt32(mDtAppts.Rows[0]["autocode"]);

                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextVisitDate));

                        mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_appointments", mDataFields, "autocode=" + mApptAutoCode);
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                //commit
                mTrans.Commit();

                //get patient
                mCtcClient = this.Get_Client("code", mPatientCode);

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Edit_ART
        public AfyaPro_Types.clsCtcClient Edit_ART(
            int mAutoCode,
            DateTime mTransDate,
            string mOutcomeCode,
            DateTime? mOutcomeDate,
            string mARTRegimenCode,
            string mTBStatusCode,
            double mPillCount,
            double mDosesMissed,
            double mARVTablets,
            int mARVTo,
            double mCPTTablets,
            int mDepoGiven,
            int mCondoms,
            DateTime? mViralLoadDate,
            string mViralLoadResult,
            DateTime? mNextVisitDate,
            string mClinicianCode,
            string mNotes,
            DataTable mDtSideEffects,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Edit_ART";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;
            if (clsGlobal.IsNullDate(mNextVisitDate) == false)
            {
                mNextVisitDate = mNextVisitDate.Value.Date;
            }
            if (clsGlobal.IsNullDate(mOutcomeDate) == false)
            {
                mOutcomeDate = mOutcomeDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcart_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            string mBookingNo = "";
            string mPatientCode = "";

            #region check for bookingno
            try
            {
                mCommand.CommandText = "select * from ctc_artlog where autocode=" + mAutoCode;
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["booking"].ToString().Trim();
                    mPatientCode = mDataReader["patientcode"].ToString().Trim();
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
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

                #region ctc_artlog

                List<clsDataField> mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                mDataFields.Add(new clsDataField("outcomecode", DataTypes.dbstring.ToString(), mOutcomeCode.Trim()));
                mDataFields.Add(new clsDataField("outcomedate", DataTypes.dbdatetime.ToString(), mOutcomeDate));
                mDataFields.Add(new clsDataField("artregimencode", DataTypes.dbstring.ToString(), mARTRegimenCode.Trim()));
                mDataFields.Add(new clsDataField("tbstatuscode", DataTypes.dbstring.ToString(), mTBStatusCode.Trim()));
                mDataFields.Add(new clsDataField("pillcount", DataTypes.dbdecimal.ToString(), mPillCount));
                mDataFields.Add(new clsDataField("dosesmissed", DataTypes.dbdecimal.ToString(), mDosesMissed));
                mDataFields.Add(new clsDataField("arvtablets", DataTypes.dbdecimal.ToString(), mARVTablets));
                mDataFields.Add(new clsDataField("arvto", DataTypes.dbnumber.ToString(), mARVTo));
                mDataFields.Add(new clsDataField("cpttablets", DataTypes.dbdecimal.ToString(), mCPTTablets));
                mDataFields.Add(new clsDataField("depogiven", DataTypes.dbnumber.ToString(), mDepoGiven));
                mDataFields.Add(new clsDataField("condoms", DataTypes.dbnumber.ToString(), mCondoms));
                mDataFields.Add(new clsDataField("viralloaddate", DataTypes.dbdatetime.ToString(), mViralLoadDate));
                mDataFields.Add(new clsDataField("viralloadresult", DataTypes.dbstring.ToString(), mViralLoadResult));
                mDataFields.Add(new clsDataField("cliniciancode", DataTypes.dbstring.ToString(), mClinicianCode));
                mDataFields.Add(new clsDataField("notes", DataTypes.dbstring.ToString(), mNotes.Trim()));
                mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_artlog", mDataFields, "autocode=" + mAutoCode);
                mCommand.ExecuteNonQuery();

                OdbcCommand cmdPUpdate = new OdbcCommand();

                cmdPUpdate.Connection = mConn;
                cmdPUpdate.Transaction = mTrans;

                cmdPUpdate.Connection = mConn;
                cmdPUpdate.CommandText = "update ctc_art set latestoutcome ='" + mOutcomeCode + "' where patientcode ='" + mPatientCode.Trim() + "'";
                cmdPUpdate.ExecuteNonQuery();

                #endregion

                #region side effects

                mCommand.CommandText = "delete from ctc_patientaidsillness where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtSideEffects.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("illnesscode", DataTypes.dbstring.ToString(), mDataRow["code"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patientaidsillness", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region ctc_appointments

                if (clsGlobal.IsNullDate(mNextVisitDate) == false)
                {
                    DataTable mDtAppts = new DataTable("appointments");
                    mCommand.CommandText = "select * from ctc_appointments where patientcode='" + mPatientCode
                        + "' and booking='" + mBookingNo + "' and appttype=" + (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit;
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAppts);

                    if (mDtAppts.Rows.Count == 0)
                    {
                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                        mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                        mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                        mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                        mDataFields.Add(new clsDataField("appttype", DataTypes.dbnumber.ToString(), (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit));
                        mDataFields.Add(new clsDataField("reason", DataTypes.dbstring.ToString(), "Pre-ART Visit"));
                        mDataFields.Add(new clsDataField("wheretakencode", DataTypes.dbstring.ToString(), null));
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextVisitDate));
                        mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                        mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_appointments", mDataFields);
                        mCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        int mApptAutoCode = Convert.ToInt32(mDtAppts.Rows[0]["autocode"]);

                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextVisitDate));

                        mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_appointments", mDataFields, "autocode=" + mApptAutoCode);
                        mCommand.ExecuteNonQuery();
                    }
                }
                else
                {
                    mCommand.CommandText = "delete from ctc_appointments where patientcode='" + mPatientCode
                        + "' and booking='" + mBookingNo + "' and appttype=" + (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit;
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                //commit
                mTrans.Commit();

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Delete_ART
        public AfyaPro_Types.clsCtcClient Delete_ART(
            int mAutoCode,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Delete_ART";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcartp_delete.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            #region delete
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //this patient has to be available when doing searching
                mCommand.CommandText = "delete from ctc_artlog where autocode=" + mAutoCode;
                mCommand.ExecuteNonQuery();

                //commit
                mTrans.Commit();

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #endregion

        #region ARTT

        #region Enroll_ARTT
        public AfyaPro_Types.clsCtcClient Enroll_ARTT(
            string mPatientCode,
            Int16 mGenerateARVNo,
            string mARVNo,
            Int16 mGenerateCTCNo,
            string mCTCNo,
            string mTBRegNo,
            string mHBCNo,
            DateTime mTransDate,
            int mOnARV,
            string mReferedFromCode,
            string mReferedFromOther,
            DataTable mDtTransferIns,
            string mPriorArvExposureCode,
            DateTime? mConfirmedHivDate,
            DateTime? mEnrolledInCareDate,
            DateTime? mMedEligibleDate,
            DateTime? mEligibleAndReadyDate,
            DateTime? mStartedARTDate,
            int mWhyEligibleClinicalStage,
            double mWhyEligibleCD4Count,
            double mWhyEligibleCD4CountPercent,
            int mClinicalStage,
            decimal mCD4Count,
            decimal mCD4CountPercent,
            string mFunctionalStatusCode,
            decimal mWeight,
            int mJoinedSupport,
            string mSupporterName,
            string mSupporterAddress,
            string mSupporterTelephone,
            string mSupporterCommunity,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Enroll_ARTT";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcartt_enroll.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            DataTable mDtART = new DataTable("art");
            mCommand.CommandText = "select * from ctc_artt where patientcode='" + mPatientCode.Trim() + "'";
            mDataAdapter.SelectCommand = mCommand;
            mDataAdapter.Fill(mDtART);

            string mBookingNo = "";

            if (mDtART.Rows.Count == 0)
            {
                #region check for bookingno
                try
                {
                    mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                    mDataReader = mCommand.ExecuteReader();
                    if (mDataReader.Read() == true)
                    {
                        mBookingNo = mDataReader["autocode"].ToString().Trim();

                        if (Convert.ToDateTime(mDataReader["bookdate"]) != mTransDate)
                        {
                            mCtcClient.Exe_Result = 0;
                            mCtcClient.Exe_Message = "Client is not booked for services";
                            return mCtcClient;
                        }
                    }
                    else
                    {
                        mCtcClient.Exe_Result = 0;
                        mCtcClient.Exe_Message = "Client is not booked for services";
                        return mCtcClient;
                    }
                }
                catch (OdbcException ex)
                {
                    mCtcClient.Exe_Result = -1;
                    mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                    return mCtcClient;
                }
                finally
                {
                    mDataReader.Close();
                }
                #endregion

                #region ctc #

                #region auto generate ctc #, if option is on

                if (mGenerateCTCNo == 1)
                {
                    clsAutoCodes objAutoCodes = new clsAutoCodes();
                    AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                    mObjCode = objAutoCodes.Next_Code(
                        Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcnumbers), "ctc_patients", "ctcno");
                    if (mObjCode.Exe_Result == -1)
                    {
                        mCtcClient.Exe_Result = mObjCode.Exe_Result;
                        mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                        return mCtcClient;
                    }
                    mCTCNo = mObjCode.GeneratedCode;
                }

                #endregion

                #region check 4 duplicate ctcno
                try
                {
                    mCommand.CommandText = "select * from ctc_patients where ctcno='" + mCTCNo.Trim() + "' and patientcode<>'" + mPatientCode.Trim() + "'";
                    mDataReader = mCommand.ExecuteReader();
                    if (mDataReader.Read() == true)
                    {
                        if (mDataReader["ctcno"].ToString().Trim() != "")
                        {
                            mCtcClient.Exe_Result = 0;
                            mCtcClient.Exe_Message = "CTC # is in use by another client";
                            return mCtcClient;
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    mCtcClient.Exe_Result = -1;
                    mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                    return mCtcClient;
                }
                finally
                {
                    mDataReader.Close();
                }
                #endregion

                #endregion
            }

            #region arvno

            #region auto generate ctc #, if option is on

            if (mOnARV == 1)
            {
                if (mGenerateARVNo == 1)
                {
                    clsAutoCodes objAutoCodes = new clsAutoCodes();
                    AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                    mObjCode = objAutoCodes.Next_Code(
                        Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcarvnumber), "ctc_patients", "arvno");
                    if (mObjCode.Exe_Result == -1)
                    {
                        mCtcClient.Exe_Result = mObjCode.Exe_Result;
                        mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                        return mCtcClient;
                    }
                    mARVNo = mObjCode.GeneratedCode;
                }
            }
            else
            {
                if (mARVNo.Trim().ToLower() == "<<---new--->>")
                {
                    mARVNo = "";
                }
            }

            #endregion

            #region check 4 duplicate arvno
            try
            {
                mCommand.CommandText = "select * from ctc_patients where arvno='" + mARVNo.Trim() + "' and patientcode<>'" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    if (mDataReader["arvno"].ToString().Trim() != "")
                    {
                        mCtcClient.Exe_Result = 0;
                        mCtcClient.Exe_Message = "ARV # is in use by another client";
                        return mCtcClient;
                    }
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion

            #endregion

            #region add/edit
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                List<clsDataField> mDataFields;

                #region ctc_patients

                DataTable mDtClients = new DataTable("clients");
                mCommand.CommandText = "select * from ctc_patients where patientcode='" + mPatientCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtClients);

                if (mDtClients.Rows.Count == 0)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("arvno", DataTypes.dbstring.ToString(), mARVNo.Trim()));
                    mDataFields.Add(new clsDataField("ctcno", DataTypes.dbstring.ToString(), mCTCNo.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patients", mDataFields);
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("arvno", DataTypes.dbstring.ToString(), mARVNo.Trim()));
                    mDataFields.Add(new clsDataField("ctcno", DataTypes.dbstring.ToString(), mCTCNo.Trim()));

                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_patients", mDataFields, "patientcode='" + mPatientCode.Trim() + "'");
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region ctc_artt

                if (mDtART.Rows.Count == 0)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_artt", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("onarv", DataTypes.dbnumber.ToString(), mOnARV));
                mDataFields.Add(new clsDataField("referredfromcode", DataTypes.dbstring.ToString(), mReferedFromCode));
                mDataFields.Add(new clsDataField("referredfromother", DataTypes.dbstring.ToString(), mReferedFromOther));
                mDataFields.Add(new clsDataField("joinedsupport", DataTypes.dbnumber.ToString(), mJoinedSupport));
                mDataFields.Add(new clsDataField("supportername", DataTypes.dbstring.ToString(), mSupporterName));
                mDataFields.Add(new clsDataField("supporteraddress", DataTypes.dbstring.ToString(), mSupporterAddress));
                mDataFields.Add(new clsDataField("supportertelephone", DataTypes.dbstring.ToString(), mSupporterTelephone));
                mDataFields.Add(new clsDataField("supportercommunity", DataTypes.dbstring.ToString(), mSupporterCommunity));
                mDataFields.Add(new clsDataField("priorarvexposurecode", DataTypes.dbstring.ToString(), mPriorArvExposureCode));
                mDataFields.Add(new clsDataField("tbregno", DataTypes.dbstring.ToString(), mTBRegNo));
                mDataFields.Add(new clsDataField("hbcno", DataTypes.dbstring.ToString(), mHBCNo));
                mDataFields.Add(new clsDataField("confirmedhivdate", DataTypes.dbdatetime.ToString(), mConfirmedHivDate));
                mDataFields.Add(new clsDataField("enrolledincaredate", DataTypes.dbdatetime.ToString(), mEnrolledInCareDate));
                mDataFields.Add(new clsDataField("medeligibledate", DataTypes.dbdatetime.ToString(), mMedEligibleDate));
                mDataFields.Add(new clsDataField("eligibleandreadydate", DataTypes.dbdatetime.ToString(), mEligibleAndReadyDate));
                mDataFields.Add(new clsDataField("startedartdate", DataTypes.dbdatetime.ToString(), mStartedARTDate));
                mDataFields.Add(new clsDataField("whyeligibleclinicalstage", DataTypes.dbnumber.ToString(), mWhyEligibleClinicalStage));
                mDataFields.Add(new clsDataField("whyeligiblecd4count", DataTypes.dbdecimal.ToString(), mWhyEligibleCD4Count));
                mDataFields.Add(new clsDataField("whyeligiblecd4countpercent", DataTypes.dbdecimal.ToString(), mWhyEligibleCD4CountPercent));
                mDataFields.Add(new clsDataField("startclinicalstage", DataTypes.dbnumber.ToString(), mClinicalStage));
                mDataFields.Add(new clsDataField("startcd4count", DataTypes.dbdecimal.ToString(), mCD4Count));
                mDataFields.Add(new clsDataField("startcd4countpercent", DataTypes.dbdecimal.ToString(), mCD4CountPercent));
                mDataFields.Add(new clsDataField("startfunctionalstatuscode", DataTypes.dbstring.ToString(), mFunctionalStatusCode.Trim()));
                mDataFields.Add(new clsDataField("startweight", DataTypes.dbdecimal.ToString(), mWeight));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_artt", mDataFields, "patientcode='" + mPatientCode.Trim() + "'");
                mCommand.ExecuteNonQuery();

                #endregion

                #region transferins

                mCommand.CommandText = "delete from ctc_patienttransferin where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtTransferIns.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("transferincode", DataTypes.dbstring.ToString(), mDataRow["code"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patienttransferin", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                if (mGenerateARVNo == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcarvnumber);
                    mCommand.ExecuteNonQuery();
                }

                if (mGenerateCTCNo == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcnumbers);
                    mCommand.ExecuteNonQuery();
                }

                //commit
                mTrans.Commit();

                //get patient
                mCtcClient = this.Get_Client("code", mPatientCode);

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, mCommand.CommandText);
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Add_ARTT
        public AfyaPro_Types.clsCtcClient Add_ARTT(
            DateTime mTransDate,
            string mPatientCode,
            string mVisitTypeCode,
            int mClinicalStage,
            int mPregnant,
            DateTime? mEDDate,
            string mANCNo,
            string mFunctionalStatusCode,
            string mTBScreeningCode,
            string mTBRxCode,
            string mARVStatusCode,
            string mARVReasonCode,
            string mARVCombRegimenCode,
            int mARVCombRegimenDays,
            string mARVAdherenceCode,
            string mARVPoorAdherenceReasonCode,
            double mHB,
            double mALT,
            string mNutritionalStatusCode,
            string mNutritionalSupplementCode,
            DateTime? mNextVisitDate,
            string mFollowUpStatusCode,
            string mClinicianCode,
            DataTable mDtSideEffects,
            DataTable mDtRelevantComedics,
            DataTable mDtAbnormalResults,
            DataTable mDtFPlanMethods,
            DataTable mDtReferrals,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Add_ARTT";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;
            if (clsGlobal.IsNullDate(mNextVisitDate) == false)
            {
                mNextVisitDate = mNextVisitDate.Value.Date;
            }

            if (clsGlobal.IsNullDate(mEDDate) == false)
            {
                mEDDate = mEDDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcart_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            string mBookingNo = "";

            #region check for bookingno
            try
            {
                mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["autocode"].ToString().Trim();

                    if (Convert.ToDateTime(mDataReader["bookdate"]) != mTransDate)
                    {
                        mCtcClient.Exe_Result = 0;
                        mCtcClient.Exe_Message = "Client is not booked for services";
                        return mCtcClient;
                    }
                }
                else
                {
                    mCtcClient.Exe_Result = 0;
                    mCtcClient.Exe_Message = "Client is not booked for services";
                    return mCtcClient;
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
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

                List<clsDataField> mDataFields;

                DataTable mDtARTT = new DataTable("artt");
                mCommand.CommandText = "select * from ctc_artt where patientcode='" + mPatientCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtARTT);

                int mOnARV = 0;
                if (mDtARTT.Rows.Count > 0)
                {
                    mOnARV = mDtARTT.Rows[0]["onarv"] == DBNull.Value ? 0 : Convert.ToInt32(mDtARTT.Rows[0]["onarv"]);
                }

                #region ctc_arttlog

                DataTable mDtData = new DataTable("data");
                mCommand.CommandText = "select * from ctc_arttlog where patientcode='" + mPatientCode.Trim() + "' and booking='" + mBookingNo + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);

                if (mDtData.Rows.Count == 0)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("visittypecode", DataTypes.dbstring.ToString(), mVisitTypeCode.Trim()));
                    mDataFields.Add(new clsDataField("onarv", DataTypes.dbnumber.ToString(), mOnARV));
                    mDataFields.Add(new clsDataField("clinicalstage", DataTypes.dbnumber.ToString(), mClinicalStage));
                    mDataFields.Add(new clsDataField("pregnant", DataTypes.dbnumber.ToString(), mPregnant));
                    mDataFields.Add(new clsDataField("edddate", DataTypes.dbdatetime.ToString(), mEDDate));
                    mDataFields.Add(new clsDataField("ancno", DataTypes.dbstring.ToString(), mANCNo.Trim()));
                    mDataFields.Add(new clsDataField("tbscreeningcode", DataTypes.dbstring.ToString(), mTBScreeningCode.Trim()));
                    mDataFields.Add(new clsDataField("tbrxcode", DataTypes.dbstring.ToString(), mTBRxCode.Trim()));
                    mDataFields.Add(new clsDataField("functionalstatuscode", DataTypes.dbstring.ToString(), mFunctionalStatusCode.Trim()));
                    mDataFields.Add(new clsDataField("arvstatuscode", DataTypes.dbstring.ToString(), mARVStatusCode.Trim()));
                    mDataFields.Add(new clsDataField("arvreasoncode", DataTypes.dbstring.ToString(), mARVReasonCode.Trim()));
                    mDataFields.Add(new clsDataField("arvcombregimencode", DataTypes.dbstring.ToString(), mARVCombRegimenCode.Trim()));
                    mDataFields.Add(new clsDataField("arvcombregimendays", DataTypes.dbnumber.ToString(), mARVCombRegimenDays));
                    mDataFields.Add(new clsDataField("arvadherencecode", DataTypes.dbstring.ToString(), mARVAdherenceCode.Trim()));
                    mDataFields.Add(new clsDataField("arvpooradherencereasoncode", DataTypes.dbstring.ToString(), mARVPoorAdherenceReasonCode.Trim()));
                    mDataFields.Add(new clsDataField("hb", DataTypes.dbdecimal.ToString(), mHB));
                    mDataFields.Add(new clsDataField("alt", DataTypes.dbdecimal.ToString(), mALT));
                    mDataFields.Add(new clsDataField("nutritionalstatuscode", DataTypes.dbstring.ToString(), mNutritionalStatusCode));
                    mDataFields.Add(new clsDataField("nutritionalsuppcode", DataTypes.dbstring.ToString(), mNutritionalSupplementCode));
                    mDataFields.Add(new clsDataField("followupstatuscode", DataTypes.dbstring.ToString(), mFollowUpStatusCode));
                    mDataFields.Add(new clsDataField("cliniciancode", DataTypes.dbstring.ToString(), mClinicianCode));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_arttlog", mDataFields);
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    int mAutoCode = Convert.ToInt32(mDtData.Rows[0]["autocode"]);

                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("visittypecode", DataTypes.dbstring.ToString(), mVisitTypeCode.Trim()));
                    mDataFields.Add(new clsDataField("onarv", DataTypes.dbnumber.ToString(), mOnARV));
                    mDataFields.Add(new clsDataField("clinicalstage", DataTypes.dbnumber.ToString(), mClinicalStage));
                    mDataFields.Add(new clsDataField("pregnant", DataTypes.dbnumber.ToString(), mPregnant));
                    mDataFields.Add(new clsDataField("edddate", DataTypes.dbdatetime.ToString(), mEDDate));
                    mDataFields.Add(new clsDataField("ancno", DataTypes.dbstring.ToString(), mANCNo.Trim()));
                    mDataFields.Add(new clsDataField("tbscreeningcode", DataTypes.dbstring.ToString(), mTBScreeningCode.Trim()));
                    mDataFields.Add(new clsDataField("tbrxcode", DataTypes.dbstring.ToString(), mTBRxCode.Trim()));
                    mDataFields.Add(new clsDataField("functionalstatuscode", DataTypes.dbstring.ToString(), mFunctionalStatusCode.Trim()));
                    mDataFields.Add(new clsDataField("arvstatuscode", DataTypes.dbstring.ToString(), mARVStatusCode.Trim()));
                    mDataFields.Add(new clsDataField("arvreasoncode", DataTypes.dbstring.ToString(), mARVReasonCode.Trim()));
                    mDataFields.Add(new clsDataField("arvcombregimencode", DataTypes.dbstring.ToString(), mARVCombRegimenCode.Trim()));
                    mDataFields.Add(new clsDataField("arvcombregimendays", DataTypes.dbnumber.ToString(), mARVCombRegimenDays));
                    mDataFields.Add(new clsDataField("arvadherencecode", DataTypes.dbstring.ToString(), mARVAdherenceCode.Trim()));
                    mDataFields.Add(new clsDataField("arvpooradherencereasoncode", DataTypes.dbstring.ToString(), mARVPoorAdherenceReasonCode.Trim()));
                    mDataFields.Add(new clsDataField("hb", DataTypes.dbdecimal.ToString(), mHB));
                    mDataFields.Add(new clsDataField("alt", DataTypes.dbdecimal.ToString(), mALT));
                    mDataFields.Add(new clsDataField("nutritionalstatuscode", DataTypes.dbstring.ToString(), mNutritionalStatusCode));
                    mDataFields.Add(new clsDataField("nutritionalsuppcode", DataTypes.dbstring.ToString(), mNutritionalSupplementCode));
                    mDataFields.Add(new clsDataField("followupstatuscode", DataTypes.dbstring.ToString(), mFollowUpStatusCode));
                    mDataFields.Add(new clsDataField("cliniciancode", DataTypes.dbstring.ToString(), mClinicianCode));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_arttlog", mDataFields, "autocode=" + mAutoCode);
                    mCommand.ExecuteNonQuery();
                }

                //this patient has to be available when doing searching
                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("fromctc", DataTypes.dbnumber.ToString(), 1));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("patients", mDataFields, "code='" + mPatientCode.Trim() + "'");
                mCommand.ExecuteNonQuery();

                #endregion

                #region ctc_patientaidsillness

                mCommand.CommandText = "delete from ctc_patientaidsillness where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtSideEffects.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("illnesscode", DataTypes.dbstring.ToString(), mDataRow["code"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patientaidsillness", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region ctc_patientfplanmethods

                mCommand.CommandText = "delete from ctc_patientfplanmethods where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtFPlanMethods.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("methodcode", DataTypes.dbstring.ToString(), mDataRow["code"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patientfplanmethods", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region ctc_patientcomeds

                mCommand.CommandText = "delete from ctc_patientcomeds where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtRelevantComedics.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("medcode", DataTypes.dbstring.ToString(), mDataRow["code"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patientcomeds", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region ctc_patientabnormallabresults

                mCommand.CommandText = "delete from ctc_patientabnormallabresults where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtAbnormalResults.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("resultcode", DataTypes.dbstring.ToString(), mDataRow["code"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patientabnormallabresults", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region ctc_patientreferrals

                mCommand.CommandText = "delete from ctc_patientreferrals where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtReferrals.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("referedtocode", DataTypes.dbstring.ToString(), mDataRow["code"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patientreferrals", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region ctc_appointments

                if (clsGlobal.IsNullDate(mNextVisitDate) == false)
                {
                    DataTable mDtAppts = new DataTable("appointments");
                    mCommand.CommandText = "select * from ctc_appointments where patientcode='" + mPatientCode
                        + "' and booking='" + mBookingNo + "' and appttype=" + (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit;
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAppts);

                    if (mDtAppts.Rows.Count == 0)
                    {
                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                        mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                        mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                        mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                        mDataFields.Add(new clsDataField("appttype", DataTypes.dbnumber.ToString(), (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit));
                        mDataFields.Add(new clsDataField("reason", DataTypes.dbstring.ToString(), "Pre-ART Visit"));
                        mDataFields.Add(new clsDataField("wheretakencode", DataTypes.dbstring.ToString(), null));
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextVisitDate));
                        mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                        mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_appointments", mDataFields);
                        mCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        int mApptAutoCode = Convert.ToInt32(mDtAppts.Rows[0]["autocode"]);

                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextVisitDate));

                        mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_appointments", mDataFields, "autocode=" + mApptAutoCode);
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                //commit
                mTrans.Commit();

                //get patient
                mCtcClient = this.Get_Client("code", mPatientCode);

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Edit_ARTT
        public AfyaPro_Types.clsCtcClient Edit_ARTT(
            int mAutoCode,
            DateTime mTransDate,
            string mVisitTypeCode,
            int mClinicalStage,
            int mPregnant,
            DateTime? mEDDate,
            string mANCNo,
            string mFunctionalStatusCode,
            string mTBScreeningCode,
            string mTBRxCode,
            string mARVStatusCode,
            string mARVReasonCode,
            string mARVCombRegimenCode,
            int mARVCombRegimenDays,
            string mARVAdherenceCode,
            string mARVPoorAdherenceReasonCode,
            double mHB,
            double mALT,
            string mNutritionalStatusCode,
            string mNutritionalSupplementCode,
            DateTime? mNextVisitDate,
            string mFollowUpStatusCode,
            string mClinicianCode,
            DataTable mDtSideEffects,
            DataTable mDtRelevantComedics,
            DataTable mDtAbnormalResults,
            DataTable mDtFPlanMethods,
            DataTable mDtReferrals,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Edit_ARTT";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;
            if (clsGlobal.IsNullDate(mNextVisitDate) == false)
            {
                mNextVisitDate = mNextVisitDate.Value.Date;
            }

            if (clsGlobal.IsNullDate(mEDDate) == false)
            {
                mEDDate = mEDDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcart_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            string mBookingNo = "";
            string mPatientCode = "";

            #region check for bookingno
            try
            {
                mCommand.CommandText = "select * from ctc_arttlog where autocode=" + mAutoCode;
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["booking"].ToString().Trim();
                    mPatientCode = mDataReader["patientcode"].ToString().Trim();
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
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

                #region ctc_arttlog

                List<clsDataField> mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                mDataFields.Add(new clsDataField("visittypecode", DataTypes.dbstring.ToString(), mVisitTypeCode.Trim()));
                mDataFields.Add(new clsDataField("clinicalstage", DataTypes.dbnumber.ToString(), mClinicalStage));
                mDataFields.Add(new clsDataField("pregnant", DataTypes.dbnumber.ToString(), mPregnant));
                mDataFields.Add(new clsDataField("edddate", DataTypes.dbdatetime.ToString(), mEDDate));
                mDataFields.Add(new clsDataField("ancno", DataTypes.dbstring.ToString(), mANCNo.Trim()));
                mDataFields.Add(new clsDataField("tbscreeningcode", DataTypes.dbstring.ToString(), mTBScreeningCode.Trim()));
                mDataFields.Add(new clsDataField("tbrxcode", DataTypes.dbstring.ToString(), mTBRxCode.Trim()));
                mDataFields.Add(new clsDataField("functionalstatuscode", DataTypes.dbstring.ToString(), mFunctionalStatusCode.Trim()));
                mDataFields.Add(new clsDataField("arvstatuscode", DataTypes.dbstring.ToString(), mARVStatusCode.Trim()));
                mDataFields.Add(new clsDataField("arvreasoncode", DataTypes.dbstring.ToString(), mARVReasonCode.Trim()));
                mDataFields.Add(new clsDataField("arvcombregimencode", DataTypes.dbstring.ToString(), mARVCombRegimenCode.Trim()));
                mDataFields.Add(new clsDataField("arvcombregimendays", DataTypes.dbnumber.ToString(), mARVCombRegimenDays));
                mDataFields.Add(new clsDataField("arvadherencecode", DataTypes.dbstring.ToString(), mARVAdherenceCode.Trim()));
                mDataFields.Add(new clsDataField("arvpooradherencereasoncode", DataTypes.dbstring.ToString(), mARVPoorAdherenceReasonCode.Trim()));
                mDataFields.Add(new clsDataField("hb", DataTypes.dbdecimal.ToString(), mHB));
                mDataFields.Add(new clsDataField("alt", DataTypes.dbdecimal.ToString(), mALT));
                mDataFields.Add(new clsDataField("nutritionalstatuscode", DataTypes.dbstring.ToString(), mNutritionalStatusCode));
                mDataFields.Add(new clsDataField("nutritionalsuppcode", DataTypes.dbstring.ToString(), mNutritionalSupplementCode));
                mDataFields.Add(new clsDataField("followupstatuscode", DataTypes.dbstring.ToString(), mFollowUpStatusCode));
                mDataFields.Add(new clsDataField("cliniciancode", DataTypes.dbstring.ToString(), mClinicianCode));
                mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_arttlog", mDataFields, "autocode=" + mAutoCode);
                mCommand.ExecuteNonQuery();

                #endregion

                #region ctc_patientaidsillness

                mCommand.CommandText = "delete from ctc_patientaidsillness where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtSideEffects.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("illnesscode", DataTypes.dbstring.ToString(), mDataRow["code"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patientaidsillness", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region ctc_patientfplanmethods

                mCommand.CommandText = "delete from ctc_patientfplanmethods where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtFPlanMethods.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("methodcode", DataTypes.dbstring.ToString(), mDataRow["code"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patientfplanmethods", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region ctc_patientcomeds

                mCommand.CommandText = "delete from ctc_patientcomeds where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtRelevantComedics.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("medcode", DataTypes.dbstring.ToString(), mDataRow["code"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patientcomeds", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region ctc_patientabnormallabresults

                mCommand.CommandText = "delete from ctc_patientabnormallabresults where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtAbnormalResults.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("resultcode", DataTypes.dbstring.ToString(), mDataRow["code"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patientabnormallabresults", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region ctc_patientreferrals

                mCommand.CommandText = "delete from ctc_patientreferrals where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtReferrals.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("referedtocode", DataTypes.dbstring.ToString(), mDataRow["code"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patientreferrals", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region ctc_appointments

                if (clsGlobal.IsNullDate(mNextVisitDate) == false)
                {
                    DataTable mDtAppts = new DataTable("appointments");
                    mCommand.CommandText = "select * from ctc_appointments where patientcode='" + mPatientCode
                        + "' and booking='" + mBookingNo + "' and appttype=" + (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit;
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAppts);

                    if (mDtAppts.Rows.Count == 0)
                    {
                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                        mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                        mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                        mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                        mDataFields.Add(new clsDataField("appttype", DataTypes.dbnumber.ToString(), (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit));
                        mDataFields.Add(new clsDataField("reason", DataTypes.dbstring.ToString(), "Pre-ART Visit"));
                        mDataFields.Add(new clsDataField("wheretakencode", DataTypes.dbstring.ToString(), null));
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextVisitDate));
                        mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                        mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_appointments", mDataFields);
                        mCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        int mApptAutoCode = Convert.ToInt32(mDtAppts.Rows[0]["autocode"]);

                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextVisitDate));

                        mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_appointments", mDataFields, "autocode=" + mApptAutoCode);
                        mCommand.ExecuteNonQuery();
                    }
                }
                else
                {
                    mCommand.CommandText = "delete from ctc_appointments where patientcode='" + mPatientCode
                        + "' and booking='" + mBookingNo + "' and appttype=" + (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit;
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                //commit
                mTrans.Commit();

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Delete_ARTT
        public AfyaPro_Types.clsCtcClient Delete_ARTT(
            int mAutoCode,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Delete_ARTT";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcartp_delete.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            #region delete
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //this patient has to be available when doing searching
                mCommand.CommandText = "delete from ctc_arttlog where autocode=" + mAutoCode;
                mCommand.ExecuteNonQuery();

                //commit
                mTrans.Commit();

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #endregion

        #region ARTP

        #region Enroll_ARTP
        public AfyaPro_Types.clsCtcClient Enroll_ARTP(
            string mPatientCode,
            Int16 mGenerateHTCNo,
            string mCTCNo,
            DateTime mTransDate,
            DateTime? mTransferInDate,
            string mHIVTestSite1,
            DateTime? mHIVTestDate1,
            int mHIVTestType1,
            string mHIVTestSite2,
            DateTime? mHIVTestDate2,
            int mHIVTestType2,
            string mHIVTestSite3,
            DateTime? mHIVTestDate3,
            int mHIVTestType3,
            int mClinicalStage,
            //string mClinicalConditions,
            double mCD4Count,
            double mCD4CountPercent,
            DateTime? mCD4Date,
            double mWeight,
            double mHeight,
            int mTBInitialStatus,
            DateTime? mTBTreatmentDate,
            string mTBRegNo,
            int mARTEdu,
            DateTime? mARTEduDate,
            int mKS,            
            string mARTRegimenCode1,
            DateTime? mARTRegimenDate1,           
            DateTime? mInitiationDate,
            string mModeOfEntry,
            String mOutcome,
            string mStartReason,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Enroll_ARTP";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;

            if (mTransferInDate != null)
            {
                mTransferInDate = mTransferInDate.Value.Date;
            }

            if (mHIVTestDate1 != null)
            {
                mHIVTestDate1 = mHIVTestDate1.Value.Date;
            }

            if (mHIVTestDate2 != null)
            {
                mHIVTestDate2 = mHIVTestDate2.Value.Date;
            }

            if (mHIVTestDate3 != null)
            {
                mHIVTestDate3 = mHIVTestDate3.Value.Date;
            }

            if (mARTEduDate != null)
            {
                mARTEduDate = mARTEduDate.Value.Date;
            }

            if (mTBTreatmentDate != null)
            {
                mTBTreatmentDate = mTBTreatmentDate.Value.Date;
            }

          
            if (mARTRegimenDate1 != null)
            {
                mARTRegimenDate1 = mARTRegimenDate1.Value.Date;
            }

            //if (mARTRegimenDate2 != null)
            //{
            //    mARTRegimenDate2 = mARTRegimenDate2.Value.Date;
            //}

            if (mCD4Date != null)
            {
                mCD4Date = mCD4Date.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcartp_enroll.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            DataTable mDtPreART = new DataTable("art");
            mCommand.CommandText = "select * from ctc_artp where patientcode='" + mPatientCode.Trim() + "'";
            mDataAdapter.SelectCommand = mCommand;
            mDataAdapter.Fill(mDtPreART);

            string mBookingNo = "";

            if (mDtPreART.Rows.Count == 0)
            {
                #region check for bookingno
                try
                {
                    mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                    mDataReader = mCommand.ExecuteReader();
                    if (mDataReader.Read() == true)
                    {
                        mBookingNo = mDataReader["autocode"].ToString().Trim();

                        if (Convert.ToDateTime(mDataReader["bookdate"]) != mTransDate)
                        {
                            mCtcClient.Exe_Result = 0;
                            mCtcClient.Exe_Message = "Client is not booked for services";
                            return mCtcClient;
                        }
                    }
                    else
                    {
                        mCtcClient.Exe_Result = 0;
                        mCtcClient.Exe_Message = "Client is not booked for services";
                        return mCtcClient;
                    }
                }
                catch (OdbcException ex)
                {
                    mCtcClient.Exe_Result = -1;
                    mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                    return mCtcClient;
                }
                finally
                {
                    mDataReader.Close();
                }
                #endregion

                #region auto generate ctc #, if option is on

                if (mGenerateHTCNo == 1)
                {
                    clsAutoCodes objAutoCodes = new clsAutoCodes();
                    AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                    mObjCode = objAutoCodes.Next_Code(
                        Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcnumbers), "ctc_patients", "ctcno");
                    if (mObjCode.Exe_Result == -1)
                    {
                        mCtcClient.Exe_Result = mObjCode.Exe_Result;
                        mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                        return mCtcClient;
                    }
                    mCTCNo = mObjCode.GeneratedCode;
                }

                #endregion

                #region check 4 duplicate ctcno
                try
                {
                    mCommand.CommandText = "select * from ctc_patients where ctcno='" + mCTCNo.Trim() + "' and patientcode<>'" + mPatientCode.Trim() + "'";
                    mDataReader = mCommand.ExecuteReader();
                    if (mDataReader.Read() == true)
                    {
                        if (mDataReader["ctcno"].ToString().Trim() != "")
                        {
                            mCtcClient.Exe_Result = 0;
                            mCtcClient.Exe_Message = "ART Registration # is in use by another client";
                            return mCtcClient;
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    mCtcClient.Exe_Result = -1;
                    mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                    return mCtcClient;
                }
                finally
                {
                    mDataReader.Close();
                }
                #endregion
            }

            #region add/edit
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                List<clsDataField> mDataFields;

                #region ctc_patients

                DataTable mDtClients = new DataTable("clients");
                mCommand.CommandText = "select * from ctc_patients where patientcode='" + mPatientCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtClients);

                if (mDtClients.Rows.Count == 0)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("ctcno", DataTypes.dbstring.ToString(), mCTCNo.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patients", mDataFields);
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("ctcno", DataTypes.dbstring.ToString(), mCTCNo.Trim()));

                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_patients", mDataFields, "patientcode='" + mPatientCode.Trim() + "'");
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region ctc_artp

                if (mDtPreART.Rows.Count == 0)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_artp", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("transferindate", DataTypes.dbdatetime.ToString(), mTransferInDate));
                mDataFields.Add(new clsDataField("hivtestsite1", DataTypes.dbstring.ToString(), mHIVTestSite1));
                mDataFields.Add(new clsDataField("hivtestdate1", DataTypes.dbdatetime.ToString(), mHIVTestDate1));
                mDataFields.Add(new clsDataField("hivtesttype1", DataTypes.dbnumber.ToString(), mHIVTestType1));
                mDataFields.Add(new clsDataField("hivtestsite2", DataTypes.dbstring.ToString(), mHIVTestSite2));
                mDataFields.Add(new clsDataField("hivtestdate2", DataTypes.dbdatetime.ToString(), mHIVTestDate2));
                mDataFields.Add(new clsDataField("hivtesttype2", DataTypes.dbnumber.ToString(), mHIVTestType2));
                mDataFields.Add(new clsDataField("hivtestsite3", DataTypes.dbstring.ToString(), mHIVTestSite3));
                mDataFields.Add(new clsDataField("hivtestdate3", DataTypes.dbdatetime.ToString(), mHIVTestDate3));
                mDataFields.Add(new clsDataField("hivtesttype3", DataTypes.dbnumber.ToString(), mHIVTestType3));
                mDataFields.Add(new clsDataField("clinicalstage", DataTypes.dbdecimal.ToString(), mClinicalStage));
                mDataFields.Add(new clsDataField("cd4count", DataTypes.dbdecimal.ToString(), mCD4Count));
                mDataFields.Add(new clsDataField("cd4countpercent", DataTypes.dbdecimal.ToString(), mCD4CountPercent));
                mDataFields.Add(new clsDataField("cd4date", DataTypes.dbdatetime.ToString(), mCD4Date));
                mDataFields.Add(new clsDataField("weight", DataTypes.dbdecimal.ToString(), mWeight));
                mDataFields.Add(new clsDataField("height", DataTypes.dbdecimal.ToString(), mHeight));
                mDataFields.Add(new clsDataField("tbinitialstatus", DataTypes.dbnumber.ToString(), mTBInitialStatus));
                mDataFields.Add(new clsDataField("tbtreatmentdate", DataTypes.dbdatetime.ToString(), mTBTreatmentDate));
                mDataFields.Add(new clsDataField("tbregno", DataTypes.dbstring.ToString(), mTBRegNo));
                mDataFields.Add(new clsDataField("artedu", DataTypes.dbnumber.ToString(), mARTEdu));
                mDataFields.Add(new clsDataField("artedudate", DataTypes.dbdatetime.ToString(), mARTEduDate));
                mDataFields.Add(new clsDataField("ks", DataTypes.dbnumber.ToString(), mKS));
                mDataFields.Add(new clsDataField("artregimencode1", DataTypes.dbstring.ToString(), mARTRegimenCode1));
                mDataFields.Add(new clsDataField("artregimendate1", DataTypes.dbdatetime.ToString(), mARTRegimenDate1));
                mDataFields.Add(new clsDataField("artinitiationdate", DataTypes.dbdatetime.ToString(), mInitiationDate));
                mDataFields.Add(new clsDataField("arventrymode", DataTypes.dbstring.ToString(), mModeOfEntry));
                mDataFields.Add(new clsDataField("latestoutcome", DataTypes.dbstring.ToString(), mOutcome));
                mDataFields.Add(new clsDataField("arvstartreason", DataTypes.dbstring.ToString(), mStartReason));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_artp", mDataFields, "patientcode='" + mPatientCode.Trim() + "'");
                mCommand.ExecuteNonQuery();

                #endregion

                if (mGenerateHTCNo == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcnumbers);
                    mCommand.ExecuteNonQuery();
                }

                //commit
                mTrans.Commit();

                //get patient
                mCtcClient = this.Get_Client("code", mPatientCode);

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, mCommand.CommandText);
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Add_ARTP
        public AfyaPro_Types.clsCtcClient Add_ARTP(
            string mPatientCode,
            DateTime mTransDate,
            string mOutcomeCode,
            DateTime? mOutcomeDate,
            string mARTRegimenCode,
            string mTBStatusCode,
            double mPillCount,
            double mDosesMissed,
            double mARVTablets,
            int mARVTo,
            double mCPTTablets,
            DateTime? mViralLoadDate,
            string mViralLoadResult,
            DateTime? mNextVisitDate,
            string mClinicianCode,
            string mNotes,
            DataTable mDtSideEffects,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Add_ARTP";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;
            if (clsGlobal.IsNullDate(mNextVisitDate) == false)
            {
                mNextVisitDate = mNextVisitDate.Value.Date;
            }
            if (clsGlobal.IsNullDate(mViralLoadDate) == false)
            {
                mViralLoadDate = mViralLoadDate.Value.Date;
            }
            if (clsGlobal.IsNullDate(mOutcomeDate) == false)
            {
                mOutcomeDate = mOutcomeDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcartp_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            string mBookingNo = "";

            #region check for bookingno
            try
            {
                mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["autocode"].ToString().Trim();

                    if (Convert.ToDateTime(mDataReader["bookdate"]) != mTransDate)
                    {
                        mCtcClient.Exe_Result = 0;
                        mCtcClient.Exe_Message = "Client is not booked for services";
                        return mCtcClient;
                    }
                }
                else
                {
                    mCtcClient.Exe_Result = 0;
                    mCtcClient.Exe_Message = "Client is not booked for services";
                    return mCtcClient;
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
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

                List<clsDataField> mDataFields;

                #region ctc_artplog

                DataTable mDtData = new DataTable("data");
                mCommand.CommandText = "select * from ctc_artplog where patientcode='" + mPatientCode.Trim() + "' and booking='" + mBookingNo + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);

                if (mDtData.Rows.Count == 0)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("outcomecode", DataTypes.dbstring.ToString(), mOutcomeCode.Trim()));
                    mDataFields.Add(new clsDataField("outcomedate", DataTypes.dbdatetime.ToString(), mOutcomeDate));
                    mDataFields.Add(new clsDataField("artregimencode", DataTypes.dbstring.ToString(), mARTRegimenCode.Trim()));
                    mDataFields.Add(new clsDataField("tbstatuscode", DataTypes.dbstring.ToString(), mTBStatusCode.Trim()));
                    mDataFields.Add(new clsDataField("pillcount", DataTypes.dbdecimal.ToString(), mPillCount));
                    mDataFields.Add(new clsDataField("dosesmissed", DataTypes.dbdecimal.ToString(), mDosesMissed));
                    mDataFields.Add(new clsDataField("arvtablets", DataTypes.dbdecimal.ToString(), mARVTablets));
                    mDataFields.Add(new clsDataField("arvto", DataTypes.dbnumber.ToString(), mARVTo));
                    mDataFields.Add(new clsDataField("cpttablets", DataTypes.dbdecimal.ToString(), mCPTTablets));
                    mDataFields.Add(new clsDataField("viralloaddate", DataTypes.dbdatetime.ToString(), mViralLoadDate));
                    mDataFields.Add(new clsDataField("viralloadresult", DataTypes.dbstring.ToString(), mViralLoadResult));
                    mDataFields.Add(new clsDataField("cliniciancode", DataTypes.dbstring.ToString(), mClinicianCode));
                    mDataFields.Add(new clsDataField("notes", DataTypes.dbstring.ToString(), mNotes.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_artplog", mDataFields);
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    int mAutoCode = Convert.ToInt32(mDtData.Rows[0]["autocode"]);

                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("outcomecode", DataTypes.dbstring.ToString(), mOutcomeCode.Trim()));
                    mDataFields.Add(new clsDataField("outcomedate", DataTypes.dbdatetime.ToString(), mOutcomeDate));
                    mDataFields.Add(new clsDataField("artregimencode", DataTypes.dbstring.ToString(), mARTRegimenCode.Trim()));
                    mDataFields.Add(new clsDataField("tbstatuscode", DataTypes.dbstring.ToString(), mTBStatusCode.Trim()));
                    mDataFields.Add(new clsDataField("pillcount", DataTypes.dbdecimal.ToString(), mPillCount));
                    mDataFields.Add(new clsDataField("dosesmissed", DataTypes.dbdecimal.ToString(), mDosesMissed));
                    mDataFields.Add(new clsDataField("arvtablets", DataTypes.dbdecimal.ToString(), mARVTablets));
                    mDataFields.Add(new clsDataField("arvto", DataTypes.dbnumber.ToString(), mARVTo));
                    mDataFields.Add(new clsDataField("cpttablets", DataTypes.dbdecimal.ToString(), mCPTTablets));
                    mDataFields.Add(new clsDataField("viralloaddate", DataTypes.dbdatetime.ToString(), mViralLoadDate));
                    mDataFields.Add(new clsDataField("viralloadresult", DataTypes.dbstring.ToString(), mViralLoadResult));
                    mDataFields.Add(new clsDataField("cliniciancode", DataTypes.dbstring.ToString(), mClinicianCode));
                    mDataFields.Add(new clsDataField("notes", DataTypes.dbstring.ToString(), mNotes.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_artplog", mDataFields, "autocode=" + mAutoCode);
                    mCommand.ExecuteNonQuery();
                }

                //this patient has to be available when doing searching
                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("fromctc", DataTypes.dbnumber.ToString(), 1));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("patients", mDataFields, "code='" + mPatientCode.Trim() + "'");
                mCommand.ExecuteNonQuery();

                OdbcCommand cmdPUpdate = new OdbcCommand();

                cmdPUpdate.Connection = mConn;
                cmdPUpdate.Transaction = mTrans;

                cmdPUpdate.Connection = mConn;
                cmdPUpdate.CommandText = "update ctc_artp set latestoutcome ='" + mOutcomeCode + "' where patientcode ='" + mPatientCode.Trim() + "'";
                cmdPUpdate.ExecuteNonQuery();

                #endregion

                #region side effects

                mCommand.CommandText = "delete from ctc_patientaidsillness where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtSideEffects.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("illnesscode", DataTypes.dbstring.ToString(), mDataRow["code"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patientaidsillness", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region ctc_appointments

                if (clsGlobal.IsNullDate(mNextVisitDate) == false)
                {
                    DataTable mDtAppts = new DataTable("appointments");
                    mCommand.CommandText = "select * from ctc_appointments where patientcode='" + mPatientCode
                        + "' and booking='" + mBookingNo + "' and appttype=" + (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit;
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAppts);

                    if (mDtAppts.Rows.Count == 0)
                    {
                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                        mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                        mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                        mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                        mDataFields.Add(new clsDataField("appttype", DataTypes.dbnumber.ToString(), (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit));
                        mDataFields.Add(new clsDataField("reason", DataTypes.dbstring.ToString(), "Pre-ART Visit"));
                        mDataFields.Add(new clsDataField("wheretakencode", DataTypes.dbstring.ToString(), null));
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextVisitDate));
                        mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                        mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_appointments", mDataFields);
                        mCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        int mApptAutoCode = Convert.ToInt32(mDtAppts.Rows[0]["autocode"]);

                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextVisitDate));

                        mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_appointments", mDataFields, "autocode=" + mApptAutoCode);
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                //commit
                mTrans.Commit();

                //get patient
                mCtcClient = this.Get_Client("code", mPatientCode);

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Edit_ARTP
        public AfyaPro_Types.clsCtcClient Edit_ARTP(
            int mAutoCode,
            DateTime mTransDate,
            string mOutcomeCode,
            DateTime? mOutcomeDate,
            string mARTRegimenCode,
            string mTBStatusCode,
            double mPillCount,
            double mDosesMissed,
            double mARVTablets,
            int mARVTo,
            double mCPTTablets,
            DateTime? mViralLoadDate,
            string mViralLoadResult,
            DateTime? mNextVisitDate,
            string mClinicianCode,
            string mNotes,
            DataTable mDtSideEffects,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Edit_ARTP";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;
            if (clsGlobal.IsNullDate(mNextVisitDate) == false)
            {
                mNextVisitDate = mNextVisitDate.Value.Date;
            }
            if (clsGlobal.IsNullDate(mOutcomeDate) == false)
            {
                mOutcomeDate = mOutcomeDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcartp_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            string mBookingNo = "";
            string mPatientCode = "";

            #region check for bookingno
            try
            {
                mCommand.CommandText = "select * from ctc_artplog where autocode=" + mAutoCode;
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["booking"].ToString().Trim();
                    mPatientCode = mDataReader["patientcode"].ToString().Trim();
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
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

                #region ctc_artplog

                List<clsDataField> mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                mDataFields.Add(new clsDataField("outcomecode", DataTypes.dbstring.ToString(), mOutcomeCode.Trim()));
                mDataFields.Add(new clsDataField("outcomedate", DataTypes.dbdatetime.ToString(), mOutcomeDate));
                mDataFields.Add(new clsDataField("artregimencode", DataTypes.dbstring.ToString(), mARTRegimenCode.Trim()));
                mDataFields.Add(new clsDataField("tbstatuscode", DataTypes.dbstring.ToString(), mTBStatusCode.Trim()));
                mDataFields.Add(new clsDataField("pillcount", DataTypes.dbdecimal.ToString(), mPillCount));
                mDataFields.Add(new clsDataField("dosesmissed", DataTypes.dbdecimal.ToString(), mDosesMissed));
                mDataFields.Add(new clsDataField("arvtablets", DataTypes.dbdecimal.ToString(), mARVTablets));
                mDataFields.Add(new clsDataField("arvto", DataTypes.dbnumber.ToString(), mARVTo));
                mDataFields.Add(new clsDataField("cpttablets", DataTypes.dbdecimal.ToString(), mCPTTablets));
                mDataFields.Add(new clsDataField("viralloaddate", DataTypes.dbdatetime.ToString(), mViralLoadDate));
                mDataFields.Add(new clsDataField("viralloadresult", DataTypes.dbstring.ToString(), mViralLoadResult));
                mDataFields.Add(new clsDataField("cliniciancode", DataTypes.dbstring.ToString(), mClinicianCode));
                mDataFields.Add(new clsDataField("notes", DataTypes.dbstring.ToString(), mNotes.Trim()));
                mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_artplog", mDataFields, "autocode=" + mAutoCode);
                mCommand.ExecuteNonQuery();

                OdbcCommand cmdPUpdate = new OdbcCommand();

                cmdPUpdate.Connection = mConn;
                cmdPUpdate.Transaction = mTrans;

                cmdPUpdate.Connection = mConn;
                cmdPUpdate.CommandText = "update ctc_artp set latestoutcome ='" + mOutcomeCode + "' where patientcode ='" + mPatientCode.Trim() + "'";
                cmdPUpdate.ExecuteNonQuery();

                #endregion

                #region side effects

                mCommand.CommandText = "delete from ctc_patientaidsillness where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtSideEffects.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("illnesscode", DataTypes.dbstring.ToString(), mDataRow["code"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patientaidsillness", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region ctc_appointments

                if (clsGlobal.IsNullDate(mNextVisitDate) == false)
                {
                    DataTable mDtAppts = new DataTable("appointments");
                    mCommand.CommandText = "select * from ctc_appointments where patientcode='" + mPatientCode
                        + "' and booking='" + mBookingNo + "' and appttype=" + (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit;
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAppts);

                    if (mDtAppts.Rows.Count == 0)
                    {
                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                        mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                        mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                        mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                        mDataFields.Add(new clsDataField("appttype", DataTypes.dbnumber.ToString(), (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit));
                        mDataFields.Add(new clsDataField("reason", DataTypes.dbstring.ToString(), "Pre-ART Visit"));
                        mDataFields.Add(new clsDataField("wheretakencode", DataTypes.dbstring.ToString(), null));
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextVisitDate));
                        mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                        mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_appointments", mDataFields);
                        mCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        int mApptAutoCode = Convert.ToInt32(mDtAppts.Rows[0]["autocode"]);

                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextVisitDate));

                        mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_appointments", mDataFields, "autocode=" + mApptAutoCode);
                        mCommand.ExecuteNonQuery();
                    }
                }
                else
                {
                    mCommand.CommandText = "delete from ctc_appointments where patientcode='" + mPatientCode
                        + "' and booking='" + mBookingNo + "' and appttype=" + (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit;
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                //commit
                mTrans.Commit();

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Delete_ARTP
        public AfyaPro_Types.clsCtcClient Delete_ARTP(
            int mAutoCode,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Delete_ARTP";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcartp_delete.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            #region delete
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //this patient has to be available when doing searching
                mCommand.CommandText = "delete from ctc_artplog where autocode=" + mAutoCode;
                mCommand.ExecuteNonQuery();

                //commit
                mTrans.Commit();

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #endregion

        #region Exposed

        #region Enroll_Exposed
        public AfyaPro_Types.clsCtcClient Enroll_Exposed(
            string mPatientCode,
            DateTime mTransDate,
            double mBirthWeight,
            DateTime? mTransferInDate,
            string mARVPregCode,
            string mARVLabourCode,
            string mARVBabyBirthCode,
            string mARVBabyContCode,
            double mARVBabyAdhere,
            string mDeliveryPlace,
            int mHIVTestRapid,
            string mHIVTestRapidAge,
            int mHIVTestPCR,
            string mHIVTestPCRAge,
            int mConfirmedHIV,
            string mMotherStatusCode,
            string mMotherARTNo,
            int mBirthCohortYear,
            int mBirthCohortMonth,
            string mEntryMode,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Enroll_Exposed";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;

            if (mTransferInDate != null)
            {
                mTransferInDate = mTransferInDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcexposed_enroll.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            DataTable mDtPreART = new DataTable("art");
            mCommand.CommandText = "select * from ctc_exposed where patientcode='" + mPatientCode.Trim() + "'";
            mDataAdapter.SelectCommand = mCommand;
            mDataAdapter.Fill(mDtPreART);

            string mBookingNo = "";

            if (mDtPreART.Rows.Count == 0)
            {
                #region check for bookingno
                try
                {
                    mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                    mDataReader = mCommand.ExecuteReader();
                    if (mDataReader.Read() == true)
                    {
                        mBookingNo = mDataReader["autocode"].ToString().Trim();

                        if (Convert.ToDateTime(mDataReader["bookdate"]) != mTransDate)
                        {
                            mCtcClient.Exe_Result = 0;
                            mCtcClient.Exe_Message = "Client is not booked for services";
                            return mCtcClient;
                        }
                    }
                    else
                    {
                        mCtcClient.Exe_Result = 0;
                        mCtcClient.Exe_Message = "Client is not booked for services";
                        return mCtcClient;
                    }
                }
                catch (OdbcException ex)
                {
                    mCtcClient.Exe_Result = -1;
                    mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                    return mCtcClient;
                }
                finally
                {
                    mDataReader.Close();
                }
                #endregion
            }

            #region add/edit
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                List<clsDataField> mDataFields;

                #region ctc_patients

                DataTable mDtClients = new DataTable("clients");
                mCommand.CommandText = "select * from ctc_patients where patientcode='" + mPatientCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtClients);

                if (mDtClients.Rows.Count == 0)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patients", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region ctc_exposed

                if (mDtPreART.Rows.Count == 0)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_exposed", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("weight", DataTypes.dbdecimal.ToString(), mBirthWeight));
                mDataFields.Add(new clsDataField("enrolmentdate", DataTypes.dbdatetime.ToString(), mTransDate));
                mDataFields.Add(new clsDataField("transferindate", DataTypes.dbdatetime.ToString(), mTransferInDate));
                mDataFields.Add(new clsDataField("arvpregcode", DataTypes.dbstring.ToString(), mARVPregCode));
                mDataFields.Add(new clsDataField("arvlabourcode", DataTypes.dbstring.ToString(), mARVLabourCode));
                mDataFields.Add(new clsDataField("arvbabybirthcode", DataTypes.dbstring.ToString(), mARVBabyBirthCode));
                mDataFields.Add(new clsDataField("arvbabycontcode", DataTypes.dbstring.ToString(), mARVBabyContCode));
                mDataFields.Add(new clsDataField("arvbabyadhere", DataTypes.dbdecimal.ToString(), mARVBabyAdhere));
                mDataFields.Add(new clsDataField("deliveryplace", DataTypes.dbstring.ToString(), mDeliveryPlace));
                mDataFields.Add(new clsDataField("hivtestrapid", DataTypes.dbnumber.ToString(), mHIVTestRapid));
                mDataFields.Add(new clsDataField("hivtestrapidage", DataTypes.dbstring.ToString(), mHIVTestRapidAge));
                mDataFields.Add(new clsDataField("hivtestpcr", DataTypes.dbnumber.ToString(), mHIVTestPCR));
                mDataFields.Add(new clsDataField("hivtestpcrage", DataTypes.dbstring.ToString(), mHIVTestPCRAge));
                mDataFields.Add(new clsDataField("confirmedhiv", DataTypes.dbnumber.ToString(), mConfirmedHIV));
                mDataFields.Add(new clsDataField("motherstatuscode", DataTypes.dbstring.ToString(), mMotherStatusCode));
                mDataFields.Add(new clsDataField("motherartno", DataTypes.dbstring.ToString(), mMotherARTNo));
                mDataFields.Add(new clsDataField("birthcohortyear", DataTypes.dbnumber.ToString(), mBirthCohortYear));
                mDataFields.Add(new clsDataField("birthcohortmonth", DataTypes.dbnumber.ToString(), mBirthCohortMonth));
                mDataFields.Add(new clsDataField("modeofentry", DataTypes.dbstring.ToString(), mEntryMode));


                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_exposed", mDataFields, "patientcode='" + mPatientCode.Trim() + "'");
                mCommand.ExecuteNonQuery();

                #endregion

                //commit
                mTrans.Commit();

                //get patient
                mCtcClient = this.Get_Client("code", mPatientCode);

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, mCommand.CommandText);
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Add_Exposed
        public AfyaPro_Types.clsCtcClient Add_Exposed(
            string mPatientCode,
            DateTime mTransDate,
            double mMuac,
            string mWastingCode,
            string mBreastFeedingCode,
            string mMotherStatusCode,
            string mTBStatusCode,
            string mClinMonit,
            string mHIVInfectionCode,
            double mCPTMills,
            double mCPTTablets,
            string mOutcomeCode,
            DateTime? mOutcomeDate,
            DateTime? mNextVisitDate,
            string mClinicianCode,
            string mNotes,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Add_Exposed";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;
            if (clsGlobal.IsNullDate(mNextVisitDate) == false)
            {
                mNextVisitDate = mNextVisitDate.Value.Date;
            }

            if (clsGlobal.IsNullDate(mOutcomeDate) == false)
            {
                mOutcomeDate = mOutcomeDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcexposed_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            string mBookingNo = "";

            #region check for bookingno
            try
            {
                mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["autocode"].ToString().Trim();

                    if (Convert.ToDateTime(mDataReader["bookdate"]) != mTransDate)
                    {
                        mCtcClient.Exe_Result = 0;
                        mCtcClient.Exe_Message = "Client is not booked for services";
                        return mCtcClient;
                    }
                }
                else
                {
                    mCtcClient.Exe_Result = 0;
                    mCtcClient.Exe_Message = "Client is not booked for services";
                    return mCtcClient;
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
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

                List<clsDataField> mDataFields;

                #region ctc_exposedlog

                DataTable mDtData = new DataTable("data");
                mCommand.CommandText = "select * from ctc_exposedlog where patientcode='" + mPatientCode.Trim() + "' and booking='" + mBookingNo + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);

                if (mDtData.Rows.Count == 0)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("muac", DataTypes.dbdecimal.ToString(), mMuac));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("wastingcode", DataTypes.dbstring.ToString(), mWastingCode.Trim()));
                    mDataFields.Add(new clsDataField("breastfeedingcode", DataTypes.dbstring.ToString(), mBreastFeedingCode.Trim()));
                    mDataFields.Add(new clsDataField("motherstatuscode", DataTypes.dbstring.ToString(), mMotherStatusCode.Trim()));
                    mDataFields.Add(new clsDataField("tbstatuscode", DataTypes.dbstring.ToString(), mTBStatusCode.Trim()));
                    mDataFields.Add(new clsDataField("clinmonit", DataTypes.dbstring.ToString(), mClinMonit.Trim()));
                    mDataFields.Add(new clsDataField("hivinfectioncode", DataTypes.dbstring.ToString(), mHIVInfectionCode.Trim()));
                    mDataFields.Add(new clsDataField("cptmills", DataTypes.dbdecimal.ToString(), mCPTMills));
                    mDataFields.Add(new clsDataField("cpttablets", DataTypes.dbdecimal.ToString(), mCPTTablets));
                    mDataFields.Add(new clsDataField("outcomecode", DataTypes.dbstring.ToString(), mOutcomeCode.Trim()));
                    mDataFields.Add(new clsDataField("outcomedate", DataTypes.dbdatetime.ToString(), mOutcomeDate));
                    mDataFields.Add(new clsDataField("cliniciancode", DataTypes.dbstring.ToString(), mClinicianCode));
                    mDataFields.Add(new clsDataField("notes", DataTypes.dbstring.ToString(), mNotes.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_exposedlog", mDataFields);
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    int mAutoCode = Convert.ToInt32(mDtData.Rows[0]["autocode"]);

                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("muac", DataTypes.dbdecimal.ToString(), mMuac));
                    mDataFields.Add(new clsDataField("wastingcode", DataTypes.dbstring.ToString(), mWastingCode.Trim()));
                    mDataFields.Add(new clsDataField("breastfeedingcode", DataTypes.dbstring.ToString(), mBreastFeedingCode.Trim()));
                    mDataFields.Add(new clsDataField("motherstatuscode", DataTypes.dbstring.ToString(), mMotherStatusCode.Trim()));
                    mDataFields.Add(new clsDataField("tbstatuscode", DataTypes.dbstring.ToString(), mTBStatusCode.Trim()));
                    mDataFields.Add(new clsDataField("clinmonit", DataTypes.dbstring.ToString(), mClinMonit.Trim()));
                    mDataFields.Add(new clsDataField("hivinfectioncode", DataTypes.dbstring.ToString(), mHIVInfectionCode.Trim()));
                    mDataFields.Add(new clsDataField("cptmills", DataTypes.dbdecimal.ToString(), mCPTMills));
                    mDataFields.Add(new clsDataField("cpttablets", DataTypes.dbdecimal.ToString(), mCPTTablets));
                    mDataFields.Add(new clsDataField("outcomecode", DataTypes.dbstring.ToString(), mOutcomeCode.Trim()));
                    mDataFields.Add(new clsDataField("outcomedate", DataTypes.dbdatetime.ToString(), mOutcomeDate));
                    mDataFields.Add(new clsDataField("cliniciancode", DataTypes.dbstring.ToString(), mClinicianCode));
                    mDataFields.Add(new clsDataField("notes", DataTypes.dbstring.ToString(), mNotes.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_exposedlog", mDataFields, "autocode=" + mAutoCode);
                    mCommand.ExecuteNonQuery();
                }

                //this patient has to be available when doing searching
                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("fromctc", DataTypes.dbnumber.ToString(), 1));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("patients", mDataFields, "code='" + mPatientCode.Trim() + "'");
                mCommand.ExecuteNonQuery();

                OdbcCommand cmdPUpdate = new OdbcCommand();

                cmdPUpdate.Connection = mConn;
                cmdPUpdate.Transaction = mTrans;

                cmdPUpdate.Connection = mConn;
                cmdPUpdate.CommandText = "update ctc_exposed set latestoutcome ='" + mOutcomeCode + "' where patientcode ='" + mPatientCode.Trim() + "'";
                cmdPUpdate.ExecuteNonQuery();

                #endregion

                #region ctc_appointments

                if (clsGlobal.IsNullDate(mNextVisitDate) == false)
                {
                    DataTable mDtAppts = new DataTable("appointments");
                    mCommand.CommandText = "select * from ctc_appointments where patientcode='" + mPatientCode
                        + "' and booking='" + mBookingNo + "' and appttype=" + (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit;
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAppts);

                    if (mDtAppts.Rows.Count == 0)
                    {
                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                        mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                        mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                        mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                        mDataFields.Add(new clsDataField("appttype", DataTypes.dbnumber.ToString(), (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit));
                        mDataFields.Add(new clsDataField("reason", DataTypes.dbstring.ToString(), "Pre-ART Visit"));
                        mDataFields.Add(new clsDataField("wheretakencode", DataTypes.dbstring.ToString(), null));
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextVisitDate));
                        mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                        mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_appointments", mDataFields);
                        mCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        int mApptAutoCode = Convert.ToInt32(mDtAppts.Rows[0]["autocode"]);

                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextVisitDate));

                        mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_appointments", mDataFields, "autocode=" + mApptAutoCode);
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                //commit
                mTrans.Commit();

                //get patient
                mCtcClient = this.Get_Client("code", mPatientCode);

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Edit_Exposed
        public AfyaPro_Types.clsCtcClient Edit_Exposed(
            int mAutoCode,
            DateTime mTransDate,
            double mMuac,
            string mWastingCode,
            string mBreastFeedingCode,
            string mMotherStatusCode,
            string mTBStatusCode,
            string mClinMonit,
            string mHIVInfectionCode,
            double mCPTMills,
            double mCPTTablets,
            string mOutcomeCode,
            DateTime? mOutcomeDate,
            DateTime? mNextVisitDate,
            string mClinicianCode,
            string mNotes,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Edit_Exposed";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;
            if (clsGlobal.IsNullDate(mNextVisitDate) == false)
            {
                mNextVisitDate = mNextVisitDate.Value.Date;
            }
            if (clsGlobal.IsNullDate(mOutcomeDate) == false)
            {
                mOutcomeDate = mOutcomeDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcexposed_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            string mBookingNo = "";
            string mPatientCode = "";

            #region check for bookingno
            try
            {
                mCommand.CommandText = "select * from ctc_exposedlog where autocode=" + mAutoCode;
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["booking"].ToString().Trim();
                    mPatientCode = mDataReader["patientcode"].ToString().Trim();
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
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

                #region ctc_exposedlog

                List<clsDataField> mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                mDataFields.Add(new clsDataField("muac", DataTypes.dbdecimal.ToString(), mMuac));
                mDataFields.Add(new clsDataField("wastingcode", DataTypes.dbstring.ToString(), mWastingCode.Trim()));
                mDataFields.Add(new clsDataField("breastfeedingcode", DataTypes.dbstring.ToString(), mBreastFeedingCode.Trim()));
                mDataFields.Add(new clsDataField("motherstatuscode", DataTypes.dbstring.ToString(), mMotherStatusCode.Trim()));
                mDataFields.Add(new clsDataField("tbstatuscode", DataTypes.dbstring.ToString(), mTBStatusCode.Trim()));
                mDataFields.Add(new clsDataField("clinmonit", DataTypes.dbstring.ToString(), mClinMonit.Trim()));
                mDataFields.Add(new clsDataField("hivinfectioncode", DataTypes.dbstring.ToString(), mHIVInfectionCode.Trim()));
                mDataFields.Add(new clsDataField("cptmills", DataTypes.dbdecimal.ToString(), mCPTMills));
                mDataFields.Add(new clsDataField("cpttablets", DataTypes.dbdecimal.ToString(), mCPTTablets));
                mDataFields.Add(new clsDataField("outcomecode", DataTypes.dbstring.ToString(), mOutcomeCode.Trim()));
                mDataFields.Add(new clsDataField("outcomedate", DataTypes.dbdatetime.ToString(), mOutcomeDate));
                mDataFields.Add(new clsDataField("cliniciancode", DataTypes.dbstring.ToString(), mClinicianCode));
                mDataFields.Add(new clsDataField("notes", DataTypes.dbstring.ToString(), mNotes.Trim()));
                mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_exposedlog", mDataFields, "autocode=" + mAutoCode);
                mCommand.ExecuteNonQuery();

                OdbcCommand cmdPUpdate = new OdbcCommand();

                cmdPUpdate.Connection = mConn;
                cmdPUpdate.Transaction = mTrans;

                cmdPUpdate.Connection = mConn;
                cmdPUpdate.CommandText = "update ctc_exposed set latestoutcome ='" + mOutcomeCode + "' where patientcode ='" + mPatientCode.Trim() + "'";
                cmdPUpdate.ExecuteNonQuery();

                #endregion

                #region ctc_appointments

                if (clsGlobal.IsNullDate(mNextVisitDate) == false)
                {
                    DataTable mDtAppts = new DataTable("appointments");
                    mCommand.CommandText = "select * from ctc_appointments where patientcode='" + mPatientCode
                        + "' and booking='" + mBookingNo + "' and appttype=" + (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit;
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAppts);

                    if (mDtAppts.Rows.Count == 0)
                    {
                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                        mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                        mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                        mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                        mDataFields.Add(new clsDataField("appttype", DataTypes.dbnumber.ToString(), (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit));
                        mDataFields.Add(new clsDataField("reason", DataTypes.dbstring.ToString(), "Pre-ART Visit"));
                        mDataFields.Add(new clsDataField("wheretakencode", DataTypes.dbstring.ToString(), null));
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextVisitDate));
                        mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                        mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_appointments", mDataFields);
                        mCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        int mApptAutoCode = Convert.ToInt32(mDtAppts.Rows[0]["autocode"]);

                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("apptdate", DataTypes.dbdatetime.ToString(), mNextVisitDate));

                        mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_appointments", mDataFields, "autocode=" + mApptAutoCode);
                        mCommand.ExecuteNonQuery();
                    }
                }
                else
                {
                    mCommand.CommandText = "delete from ctc_appointments where patientcode='" + mPatientCode
                        + "' and booking='" + mBookingNo + "' and appttype=" + (int)AfyaPro_Types.clsEnums.CTC_ApptTypes.ARTVisit;
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                //commit
                mTrans.Commit();

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Delete_Exposed
        public AfyaPro_Types.clsCtcClient Delete_Exposed(
            int mAutoCode,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Delete_Exposed";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcexposed_delete.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            #region delete
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //this patient has to be available when doing searching
                mCommand.CommandText = "delete from ctc_exposedlog where autocode=" + mAutoCode;
                mCommand.ExecuteNonQuery();

                //commit
                mTrans.Commit();

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #endregion

        #region PMTCT ANC Register

        #region Enroll_PMTCTANC
        public AfyaPro_Types.clsCtcClient Enroll_PMTCTANC(
            string mPatientCode,
            DateTime mTransDate,
            DateTime? mStartDate,
            string mANCCardNo,
            double mGestationAge,
            int mKnownHIVTestResult,
            DateTime? mPreCounsellingTestDate,
            DateTime? mHIVTestDate,
            int mHIVTestResult,
            DateTime? mPostCounsellingTestDate,
            DateTime? mPartnerHIVTestDate,
            int mPartnerHIVTestResult,
            string mRemarks,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Enroll_PMTCTANC";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;

            if (mStartDate != null)
            {
                mStartDate = mStartDate.Value.Date;
            }

            if (mPreCounsellingTestDate != null)
            {
                mPreCounsellingTestDate = mPreCounsellingTestDate.Value.Date;
            }

            if (mHIVTestDate != null)
            {
                mHIVTestDate = mHIVTestDate.Value.Date;
            }

            if (mPostCounsellingTestDate != null)
            {
                mPostCounsellingTestDate = mPostCounsellingTestDate.Value.Date;
            }

            if (mPartnerHIVTestDate != null)
            {
                mPartnerHIVTestDate = mPartnerHIVTestDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpmtctanc_enroll.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            DataTable mDtPMTCT = new DataTable("ctc_pmtct");
            mCommand.CommandText = "select * from ctc_pmtct where patientcode='" + mPatientCode.Trim() + "'";
            mDataAdapter.SelectCommand = mCommand;
            mDataAdapter.Fill(mDtPMTCT);

            string mBookingNo = "";

            if (mDtPMTCT.Rows.Count == 0)
            {
                #region check for bookingno
                try
                {
                    mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                    mDataReader = mCommand.ExecuteReader();
                    if (mDataReader.Read() == true)
                    {
                        mBookingNo = mDataReader["autocode"].ToString().Trim();

                        if (Convert.ToDateTime(mDataReader["bookdate"]) != mTransDate)
                        {
                            mCtcClient.Exe_Result = 0;
                            mCtcClient.Exe_Message = "Client is not booked for services";
                            return mCtcClient;
                        }
                    }
                    else
                    {
                        mCtcClient.Exe_Result = 0;
                        mCtcClient.Exe_Message = "Client is not booked for services";
                        return mCtcClient;
                    }
                }
                catch (OdbcException ex)
                {
                    mCtcClient.Exe_Result = -1;
                    mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                    return mCtcClient;
                }
                finally
                {
                    mDataReader.Close();
                }
                #endregion
            }

            #region add/edit
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                List<clsDataField> mDataFields;

                #region ctc_pmtct

                if (mDtPMTCT.Rows.Count == 0)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_pmtct", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("startdate", DataTypes.dbdatetime.ToString(), mStartDate));
                mDataFields.Add(new clsDataField("anccardno", DataTypes.dbstring.ToString(), mANCCardNo.Trim()));
                mDataFields.Add(new clsDataField("gestationage", DataTypes.dbdecimal.ToString(), mGestationAge));
                mDataFields.Add(new clsDataField("knownhivtestresult", DataTypes.dbnumber.ToString(), mKnownHIVTestResult));
                mDataFields.Add(new clsDataField("precounsellingdate", DataTypes.dbdatetime.ToString(), mPreCounsellingTestDate));
                mDataFields.Add(new clsDataField("hivtestdate", DataTypes.dbdatetime.ToString(), mHIVTestDate));
                mDataFields.Add(new clsDataField("hivtestresult", DataTypes.dbnumber.ToString(), mHIVTestResult));
                mDataFields.Add(new clsDataField("postcounsellingdate", DataTypes.dbdatetime.ToString(), mPostCounsellingTestDate));
                mDataFields.Add(new clsDataField("partnerhivtestdate", DataTypes.dbdatetime.ToString(), mPartnerHIVTestDate));
                mDataFields.Add(new clsDataField("partnerhivtestresult", DataTypes.dbnumber.ToString(), mPartnerHIVTestResult));
                mDataFields.Add(new clsDataField("remarks", DataTypes.dbstring.ToString(), mRemarks.Trim()));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_pmtct", mDataFields, "patientcode='" + mPatientCode.Trim() + "'");
                mCommand.ExecuteNonQuery();

                #endregion

                //commit
                mTrans.Commit();

                //get patient
                mCtcClient = this.Get_Client("code", mPatientCode);

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, mCommand.CommandText);
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region PMTCT Care Register

        #region Add_PMTCTANCVisit
        public AfyaPro_Types.clsCtcClient Add_PMTCTANCVisit(
            string mPatientCode,
            DateTime mTransDate,
            DateTime? mTransferInDate,
            double mGestationAge,
            int mInfantFeedingChoice,
            string mARTRegimenCode,
            int mAdherenceAndDisclosure,
            DateTime? mTBRXStartDate,
            DateTime? mTBRXStopDate,
            DateTime? mCTXStartDate,
            DateTime? mCTXStopDate,
            int mClinicalStage,
            DateTime? mClinicalStageDate,
            int mWhyEligible,
            double mCD4Count,
            int mAdherence,
            int mInfantFeedCounseling,
            string mRemarks,
            DataTable mDtReferrals,
            int mFirstVisit,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Add_PMTCTANCVisit";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;
            if (clsGlobal.IsNullDate(mTransferInDate) == false)
            {
                mTransferInDate = mTransferInDate.Value.Date;
            }
            if (clsGlobal.IsNullDate(mTBRXStartDate) == false)
            {
                mTBRXStartDate = mTBRXStartDate.Value.Date;
            }
            if (clsGlobal.IsNullDate(mTBRXStopDate) == false)
            {
                mTBRXStopDate = mTBRXStopDate.Value.Date;
            }
            if (clsGlobal.IsNullDate(mCTXStartDate) == false)
            {
                mCTXStartDate = mCTXStartDate.Value.Date;
            }
            if (clsGlobal.IsNullDate(mCTXStopDate) == false)
            {
                mCTXStopDate = mCTXStopDate.Value.Date;
            }
            if (clsGlobal.IsNullDate(mClinicalStageDate) == false)
            {
                mClinicalStageDate = mClinicalStageDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpmtctanc_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            string mBookingNo = "";

            #region check for bookingno
            try
            {
                mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["autocode"].ToString().Trim();

                    if (Convert.ToDateTime(mDataReader["bookdate"]) != mTransDate)
                    {
                        mCtcClient.Exe_Result = 0;
                        mCtcClient.Exe_Message = "Client is not booked for services";
                        return mCtcClient;
                    }
                }
                else
                {
                    mCtcClient.Exe_Result = 0;
                    mCtcClient.Exe_Message = "Client is not booked for services";
                    return mCtcClient;
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
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

                List<clsDataField> mDataFields;

                #region ctc_pmtctcarevisits

                DataTable mDtData = new DataTable("data");
                mCommand.CommandText = "select * from ctc_pmtctcarevisits where patientcode='" + mPatientCode.Trim() + "' and booking='" + mBookingNo + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);

                if (mDtData.Rows.Count == 0)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("isfirstvisit", DataTypes.dbnumber.ToString(), mFirstVisit));
                    mDataFields.Add(new clsDataField("transferindate", DataTypes.dbdatetime.ToString(), mTransferInDate));
                    mDataFields.Add(new clsDataField("gestationage", DataTypes.dbdecimal.ToString(), mGestationAge));
                    mDataFields.Add(new clsDataField("infantfeedchoice", DataTypes.dbnumber.ToString(), mInfantFeedingChoice));
                    mDataFields.Add(new clsDataField("artregimencode", DataTypes.dbstring.ToString(), mARTRegimenCode.Trim()));
                    mDataFields.Add(new clsDataField("adherenceanddisclosure", DataTypes.dbnumber.ToString(), mAdherenceAndDisclosure));
                    mDataFields.Add(new clsDataField("tbrxstartdate", DataTypes.dbdatetime.ToString(), mTBRXStartDate));
                    mDataFields.Add(new clsDataField("tbrxstopdate", DataTypes.dbdatetime.ToString(), mTBRXStopDate));
                    mDataFields.Add(new clsDataField("ctxstartdate", DataTypes.dbdatetime.ToString(), mCTXStartDate));
                    mDataFields.Add(new clsDataField("ctxstopdate", DataTypes.dbdatetime.ToString(), mCTXStopDate));
                    mDataFields.Add(new clsDataField("clinicalstage", DataTypes.dbnumber.ToString(), mClinicalStage));
                    mDataFields.Add(new clsDataField("clinicalstagedate", DataTypes.dbdatetime.ToString(), mClinicalStageDate));
                    mDataFields.Add(new clsDataField("cd4count", DataTypes.dbdecimal.ToString(), mCD4Count));
                    mDataFields.Add(new clsDataField("whyeligible", DataTypes.dbnumber.ToString(), mWhyEligible));
                    mDataFields.Add(new clsDataField("adherence", DataTypes.dbnumber.ToString(), mAdherence));
                    mDataFields.Add(new clsDataField("infantfeedcounseling", DataTypes.dbnumber.ToString(), mInfantFeedCounseling));
                    mDataFields.Add(new clsDataField("remarks", DataTypes.dbstring.ToString(), mRemarks.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_pmtctcarevisits", mDataFields);
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    int mAutoCode = Convert.ToInt32(mDtData.Rows[0]["autocode"]);

                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("isfirstvisit", DataTypes.dbnumber.ToString(), mFirstVisit));
                    mDataFields.Add(new clsDataField("transferindate", DataTypes.dbdatetime.ToString(), mTransferInDate));
                    mDataFields.Add(new clsDataField("gestationage", DataTypes.dbdecimal.ToString(), mGestationAge));
                    mDataFields.Add(new clsDataField("infantfeedchoice", DataTypes.dbnumber.ToString(), mInfantFeedingChoice));
                    mDataFields.Add(new clsDataField("artregimencode", DataTypes.dbstring.ToString(), mARTRegimenCode.Trim()));
                    mDataFields.Add(new clsDataField("adherenceanddisclosure", DataTypes.dbnumber.ToString(), mAdherenceAndDisclosure));
                    mDataFields.Add(new clsDataField("tbrxstartdate", DataTypes.dbdatetime.ToString(), mTBRXStartDate));
                    mDataFields.Add(new clsDataField("tbrxstopdate", DataTypes.dbdatetime.ToString(), mTBRXStopDate));
                    mDataFields.Add(new clsDataField("ctxstartdate", DataTypes.dbdatetime.ToString(), mCTXStartDate));
                    mDataFields.Add(new clsDataField("ctxstopdate", DataTypes.dbdatetime.ToString(), mCTXStopDate));
                    mDataFields.Add(new clsDataField("clinicalstage", DataTypes.dbnumber.ToString(), mClinicalStage));
                    mDataFields.Add(new clsDataField("clinicalstagedate", DataTypes.dbdatetime.ToString(), mClinicalStageDate));
                    mDataFields.Add(new clsDataField("cd4count", DataTypes.dbdecimal.ToString(), mCD4Count));
                    mDataFields.Add(new clsDataField("whyeligible", DataTypes.dbnumber.ToString(), mWhyEligible));
                    mDataFields.Add(new clsDataField("adherence", DataTypes.dbnumber.ToString(), mAdherence));
                    mDataFields.Add(new clsDataField("infantfeedcounseling", DataTypes.dbnumber.ToString(), mInfantFeedCounseling));
                    mDataFields.Add(new clsDataField("remarks", DataTypes.dbstring.ToString(), mRemarks.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_pmtctcarevisits", mDataFields, "autocode=" + mAutoCode);
                    mCommand.ExecuteNonQuery();
                }

                //this patient has to be available when doing searching
                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("fromctc", DataTypes.dbnumber.ToString(), 1));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("patients", mDataFields, "code='" + mPatientCode.Trim() + "'");
                mCommand.ExecuteNonQuery();

                #endregion

                #region ctc_patientreferrals

                mCommand.CommandText = "delete from ctc_patientreferrals where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtReferrals.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("referedtocode", DataTypes.dbstring.ToString(), mDataRow["code"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patientreferrals", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                //commit
                mTrans.Commit();

                //get patient
                mCtcClient = this.Get_Client("code", mPatientCode);

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Edit_PMTCTANCVisit
        public AfyaPro_Types.clsCtcClient Edit_PMTCTANCVisit(
            int mAutoCode,
            DateTime mTransDate,
            DateTime? mTransferInDate,
            double mGestationAge,
            int mInfantFeedingChoice,
            string mARTRegimenCode,
            int mAdherenceAndDisclosure,
            DateTime? mTBRXStartDate,
            DateTime? mTBRXStopDate,
            DateTime? mCTXStartDate,
            DateTime? mCTXStopDate,
            int mClinicalStage,
            DateTime? mClinicalStageDate,
            int mWhyEligible,
            double mCD4Count,
            int mAdherence,
            int mInfantFeedCounseling,
            string mRemarks,
            DataTable mDtReferrals,
            int mFirstVisit,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Edit_PMTCTANCVisit";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;
            if (clsGlobal.IsNullDate(mTransferInDate) == false)
            {
                mTransferInDate = mTransferInDate.Value.Date;
            }
            if (clsGlobal.IsNullDate(mTBRXStartDate) == false)
            {
                mTBRXStartDate = mTBRXStartDate.Value.Date;
            }
            if (clsGlobal.IsNullDate(mTBRXStopDate) == false)
            {
                mTBRXStopDate = mTBRXStopDate.Value.Date;
            }
            if (clsGlobal.IsNullDate(mCTXStartDate) == false)
            {
                mCTXStartDate = mCTXStartDate.Value.Date;
            }
            if (clsGlobal.IsNullDate(mCTXStopDate) == false)
            {
                mCTXStopDate = mCTXStopDate.Value.Date;
            }
            if (clsGlobal.IsNullDate(mClinicalStageDate) == false)
            {
                mClinicalStageDate = mClinicalStageDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpmtctanc_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            string mBookingNo = "";
            string mPatientCode = "";

            #region check for bookingno
            try
            {
                mCommand.CommandText = "select * from ctc_pmtctcarevisits where autocode=" + mAutoCode;
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["booking"].ToString().Trim();
                    mPatientCode = mDataReader["patientcode"].ToString().Trim();
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
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

                #region ctc_pmtctcarevisits

                List<clsDataField> mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                mDataFields.Add(new clsDataField("isfirstvisit", DataTypes.dbnumber.ToString(), mFirstVisit));
                mDataFields.Add(new clsDataField("transferindate", DataTypes.dbdatetime.ToString(), mTransferInDate));
                mDataFields.Add(new clsDataField("gestationage", DataTypes.dbdecimal.ToString(), mGestationAge));
                mDataFields.Add(new clsDataField("infantfeedchoice", DataTypes.dbnumber.ToString(), mInfantFeedingChoice));
                mDataFields.Add(new clsDataField("artregimencode", DataTypes.dbstring.ToString(), mARTRegimenCode.Trim()));
                mDataFields.Add(new clsDataField("adherenceanddisclosure", DataTypes.dbnumber.ToString(), mAdherenceAndDisclosure));
                mDataFields.Add(new clsDataField("tbrxstartdate", DataTypes.dbdatetime.ToString(), mTBRXStartDate));
                mDataFields.Add(new clsDataField("tbrxstopdate", DataTypes.dbdatetime.ToString(), mTBRXStopDate));
                mDataFields.Add(new clsDataField("ctxstartdate", DataTypes.dbdatetime.ToString(), mCTXStartDate));
                mDataFields.Add(new clsDataField("ctxstopdate", DataTypes.dbdatetime.ToString(), mCTXStopDate));
                mDataFields.Add(new clsDataField("clinicalstage", DataTypes.dbnumber.ToString(), mClinicalStage));
                mDataFields.Add(new clsDataField("clinicalstagedate", DataTypes.dbdatetime.ToString(), mClinicalStageDate));
                mDataFields.Add(new clsDataField("cd4count", DataTypes.dbdecimal.ToString(), mCD4Count));
                mDataFields.Add(new clsDataField("whyeligible", DataTypes.dbnumber.ToString(), mWhyEligible));
                mDataFields.Add(new clsDataField("adherence", DataTypes.dbnumber.ToString(), mAdherence));
                mDataFields.Add(new clsDataField("infantfeedcounseling", DataTypes.dbnumber.ToString(), mInfantFeedCounseling));
                mDataFields.Add(new clsDataField("remarks", DataTypes.dbstring.ToString(), mRemarks.Trim()));
                mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_pmtctcarevisits", mDataFields, "autocode=" + mAutoCode);
                mCommand.ExecuteNonQuery();

                #endregion

                #region ctc_patientreferrals

                mCommand.CommandText = "delete from ctc_patientreferrals where booking='"
                + mBookingNo + "' and patientcode='" + mPatientCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtReferrals.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("referedtocode", DataTypes.dbstring.ToString(), mDataRow["code"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_patientreferrals", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                //commit
                mTrans.Commit();

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Delete_PMTCTANCVisit
        public AfyaPro_Types.clsCtcClient Delete_PMTCTANCVisit(
            int mAutoCode,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Delete_PMTCTANCVisit";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpmtctanc_delete.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            #region delete
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //ctc_preartlog
                mCommand.CommandText = "delete from ctc_pmtctcarevisits where autocode=" + mAutoCode;
                mCommand.ExecuteNonQuery();

                //commit
                mTrans.Commit();

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #endregion

        #region Labour and Delivery Register

        #region Add_PMTCTLabourAndDelivery
        public AfyaPro_Types.clsCtcClient Add_PMTCTLabourAndDelivery(
            string mPatientCode,
            DateTime mTransDate,
            DateTime? mAdmissionDate,
            string mHIVStatusFromANC,
            string mHIVResultAtLnD,
            string mARVDuringANC,
            string mARVDuringLabour,
            string mInfantDoseReceived,
            string mInfantDoseDispensed,
            string mInfantFeeding,
            int mLinkage,
            string mRemarks,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Add_PMTCTLabourAndDelivery";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;
            if (clsGlobal.IsNullDate(mAdmissionDate) == false)
            {
                mAdmissionDate = mAdmissionDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpmtctdelivery_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            string mBookingNo = "";

            #region check for bookingno
            try
            {
                mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["autocode"].ToString().Trim();

                    if (Convert.ToDateTime(mDataReader["bookdate"]) != mTransDate)
                    {
                        mCtcClient.Exe_Result = 0;
                        mCtcClient.Exe_Message = "Client is not booked for services";
                        return mCtcClient;
                    }
                }
                else
                {
                    mCtcClient.Exe_Result = 0;
                    mCtcClient.Exe_Message = "Client is not booked for services";
                    return mCtcClient;
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
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

                List<clsDataField> mDataFields;

                #region ctc_pmtctlabouranddelivery

                DataTable mDtData = new DataTable("data");
                mCommand.CommandText = "select * from ctc_pmtctlabouranddelivery where patientcode='" + mPatientCode.Trim() + "' and booking='" + mBookingNo + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);

                if (mDtData.Rows.Count == 0)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("admissiondate", DataTypes.dbdatetime.ToString(), mAdmissionDate));
                    mDataFields.Add(new clsDataField("hivstatusfromanc", DataTypes.dbstring.ToString(), mHIVStatusFromANC.Trim()));
                    mDataFields.Add(new clsDataField("hivresultlnd", DataTypes.dbstring.ToString(), mHIVResultAtLnD.Trim()));
                    mDataFields.Add(new clsDataField("arvduringanc", DataTypes.dbstring.ToString(), mARVDuringANC.Trim()));
                    mDataFields.Add(new clsDataField("arvduringlabour", DataTypes.dbstring.ToString(), mARVDuringLabour.Trim()));
                    mDataFields.Add(new clsDataField("infantdosereceived", DataTypes.dbstring.ToString(), mInfantDoseReceived.Trim()));
                    mDataFields.Add(new clsDataField("infantdosedispensed", DataTypes.dbstring.ToString(), mInfantDoseDispensed.ToString()));
                    mDataFields.Add(new clsDataField("infantfeeding", DataTypes.dbstring.ToString(), mInfantFeeding.ToString()));
                    mDataFields.Add(new clsDataField("linkage", DataTypes.dbnumber.ToString(), mLinkage));
                    mDataFields.Add(new clsDataField("remarks", DataTypes.dbstring.ToString(), mRemarks.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_pmtctlabouranddelivery", mDataFields);
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    int mAutoCode = Convert.ToInt32(mDtData.Rows[0]["autocode"]);

                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("admissiondate", DataTypes.dbdatetime.ToString(), mAdmissionDate));
                    mDataFields.Add(new clsDataField("hivstatusfromanc", DataTypes.dbstring.ToString(), mHIVStatusFromANC.Trim()));
                    mDataFields.Add(new clsDataField("hivresultlnd", DataTypes.dbstring.ToString(), mHIVResultAtLnD.Trim()));
                    mDataFields.Add(new clsDataField("arvduringanc", DataTypes.dbstring.ToString(), mARVDuringANC.Trim()));
                    mDataFields.Add(new clsDataField("arvduringlabour", DataTypes.dbstring.ToString(), mARVDuringLabour.Trim()));
                    mDataFields.Add(new clsDataField("infantdosereceived", DataTypes.dbstring.ToString(), mInfantDoseReceived.Trim()));
                    mDataFields.Add(new clsDataField("infantdosedispensed", DataTypes.dbstring.ToString(), mInfantDoseDispensed.ToString()));
                    mDataFields.Add(new clsDataField("infantfeeding", DataTypes.dbstring.ToString(), mInfantFeeding.ToString()));
                    mDataFields.Add(new clsDataField("linkage", DataTypes.dbnumber.ToString(), mLinkage));
                    mDataFields.Add(new clsDataField("remarks", DataTypes.dbstring.ToString(), mRemarks.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_pmtctlabouranddelivery", mDataFields, "autocode=" + mAutoCode);
                    mCommand.ExecuteNonQuery();
                }

                //this patient has to be available when doing searching
                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("fromctc", DataTypes.dbnumber.ToString(), 1));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("patients", mDataFields, "code='" + mPatientCode.Trim() + "'");
                mCommand.ExecuteNonQuery();

                #endregion

                //commit
                mTrans.Commit();

                //get patient
                mCtcClient = this.Get_Client("code", mPatientCode);

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Edit_PMTCTLabourAndDelivery
        public AfyaPro_Types.clsCtcClient Edit_PMTCTLabourAndDelivery(
            int mAutoCode,
            DateTime mTransDate,
            DateTime? mAdmissionDate,
            string mHIVStatusFromANC,
            string mHIVResultAtLnD,
            string mARVDuringANC,
            string mARVDuringLabour,
            string mInfantDoseReceived,
            string mInfantDoseDispensed,
            string mInfantFeeding,
            int mLinkage,
            string mRemarks,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Edit_PMTCTLabourAndDelivery";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;
            if (clsGlobal.IsNullDate(mAdmissionDate) == false)
            {
                mAdmissionDate = mAdmissionDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpmtctdelivery_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            string mBookingNo = "";
            string mPatientCode = "";

            #region check for bookingno
            try
            {
                mCommand.CommandText = "select * from ctc_pmtctlabouranddelivery where autocode=" + mAutoCode;
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["booking"].ToString().Trim();
                    mPatientCode = mDataReader["patientcode"].ToString().Trim();
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
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

                #region ctc_pmtctlabouranddelivery

                List<clsDataField> mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("admissiondate", DataTypes.dbdatetime.ToString(), mAdmissionDate));
                    mDataFields.Add(new clsDataField("hivstatusfromanc", DataTypes.dbstring.ToString(), mHIVStatusFromANC.Trim()));
                    mDataFields.Add(new clsDataField("hivresultlnd", DataTypes.dbstring.ToString(), mHIVResultAtLnD.Trim()));
                    mDataFields.Add(new clsDataField("arvduringanc", DataTypes.dbstring.ToString(), mARVDuringANC.Trim()));
                    mDataFields.Add(new clsDataField("arvduringlabour", DataTypes.dbstring.ToString(), mARVDuringLabour.Trim()));
                    mDataFields.Add(new clsDataField("infantdosereceived", DataTypes.dbstring.ToString(), mInfantDoseReceived.Trim()));
                    mDataFields.Add(new clsDataField("infantdosedispensed", DataTypes.dbstring.ToString(), mInfantDoseDispensed.ToString()));
                    mDataFields.Add(new clsDataField("infantfeeding", DataTypes.dbstring.ToString(), mInfantFeeding.ToString()));
                    mDataFields.Add(new clsDataField("linkage", DataTypes.dbnumber.ToString(), mLinkage));
                    mDataFields.Add(new clsDataField("remarks", DataTypes.dbstring.ToString(), mRemarks.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_pmtctlabouranddelivery", mDataFields, "autocode=" + mAutoCode);
                mCommand.ExecuteNonQuery();

                #endregion

                //commit
                mTrans.Commit();

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Delete_PMTCTLabourAndDelivery
        public AfyaPro_Types.clsCtcClient Delete_PMTCTLabourAndDelivery(
            int mAutoCode,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Delete_PMTCTLabourAndDelivery";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpmtctdelivery_delete.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            #region delete
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //ctc_pmtctlabouranddelivery
                mCommand.CommandText = "delete from ctc_pmtctlabouranddelivery where autocode=" + mAutoCode;
                mCommand.ExecuteNonQuery();

                //commit
                mTrans.Commit();

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #endregion

        #endregion

        #region PMTCT MotherChild

        #region Enroll_MotherChild
        public AfyaPro_Types.clsCtcClient Enroll_MotherChild(
            string mPatientCode,
            DateTime mTransDate,
            int mARVMotherLabour,
            int mFamilyPlanMethod,
            int mOutcomeAt6Months,
            string mUnder5RegNo,
            DateTime? mChildBirthDate,
            double mChildBirthWeight,
            int mInfantFeeding,
            int mInfantFeedingPractice,
            int mOnCotrim,
            DateTime? mCotrimDate,
            int mInfantARV,
            DateTime? mFirstTestDate,
            int mFirstPCRTest,
            int mFirstTestResult,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Enroll_MotherChild";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;

            if (mChildBirthDate != null)
            {
                mChildBirthDate = mChildBirthDate.Value.Date;
            }

            if (mCotrimDate != null)
            {
                mCotrimDate = mCotrimDate.Value.Date;
            }

            if (mFirstTestDate != null)
            {
                mFirstTestDate = mFirstTestDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpmtctmotherchild_enroll.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            DataTable mDtPMTCT = new DataTable("ctc_pmtctmotherchild");
            mCommand.CommandText = "select * from ctc_pmtctmotherchild where patientcode='" + mPatientCode.Trim() + "'";
            mDataAdapter.SelectCommand = mCommand;
            mDataAdapter.Fill(mDtPMTCT);

            string mBookingNo = "";

            if (mDtPMTCT.Rows.Count == 0)
            {
                #region check for bookingno
                try
                {
                    mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                    mDataReader = mCommand.ExecuteReader();
                    if (mDataReader.Read() == true)
                    {
                        mBookingNo = mDataReader["autocode"].ToString().Trim();

                        if (Convert.ToDateTime(mDataReader["bookdate"]) != mTransDate)
                        {
                            mCtcClient.Exe_Result = 0;
                            mCtcClient.Exe_Message = "Client is not booked for services";
                            return mCtcClient;
                        }
                    }
                    else
                    {
                        mCtcClient.Exe_Result = 0;
                        mCtcClient.Exe_Message = "Client is not booked for services";
                        return mCtcClient;
                    }
                }
                catch (OdbcException ex)
                {
                    mCtcClient.Exe_Result = -1;
                    mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                    return mCtcClient;
                }
                finally
                {
                    mDataReader.Close();
                }
                #endregion
            }

            #region add/edit
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                List<clsDataField> mDataFields;

                #region ctc_pmtctmotherchild

                if (mDtPMTCT.Rows.Count == 0)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_pmtctmotherchild", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("cotrimdate", DataTypes.dbdatetime.ToString(), mCotrimDate));
                mDataFields.Add(new clsDataField("arvmotherlabour", DataTypes.dbnumber.ToString(), mARVMotherLabour));
                mDataFields.Add(new clsDataField("familyplanmethod", DataTypes.dbnumber.ToString(), mFamilyPlanMethod));
                mDataFields.Add(new clsDataField("outcomeat6months", DataTypes.dbnumber.ToString(), mOutcomeAt6Months));
                mDataFields.Add(new clsDataField("under5regno", DataTypes.dbstring.ToString(), mUnder5RegNo.Trim()));
                mDataFields.Add(new clsDataField("childbirthdate", DataTypes.dbdatetime.ToString(), mChildBirthDate));
                mDataFields.Add(new clsDataField("childbirthweight", DataTypes.dbdecimal.ToString(), mChildBirthWeight));
                mDataFields.Add(new clsDataField("infantfeeding", DataTypes.dbnumber.ToString(), mInfantFeeding));
                mDataFields.Add(new clsDataField("infantfeedingpractice", DataTypes.dbnumber.ToString(), mInfantFeedingPractice));
                mDataFields.Add(new clsDataField("oncotrim", DataTypes.dbnumber.ToString(), mOnCotrim));
                mDataFields.Add(new clsDataField("cotrimdate", DataTypes.dbdatetime.ToString(), mCotrimDate));
                mDataFields.Add(new clsDataField("infantarv", DataTypes.dbnumber.ToString(), mInfantARV));
                mDataFields.Add(new clsDataField("firsttestdate", DataTypes.dbdatetime.ToString(), mFirstTestDate));
                mDataFields.Add(new clsDataField("firstPCRTest", DataTypes.dbnumber.ToString(), mFirstPCRTest));
                mDataFields.Add(new clsDataField("firsttestresult", DataTypes.dbnumber.ToString(), mFirstTestResult));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_pmtctmotherchild", mDataFields, "patientcode='" + mPatientCode.Trim() + "'");
                mCommand.ExecuteNonQuery();

                #endregion

                //commit
                mTrans.Commit();

                //get patient
                mCtcClient = this.Get_Client("code", mPatientCode);

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, mCommand.CommandText);
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Add_MotherChild
        public AfyaPro_Types.clsCtcClient Add_MotherChild(
            string mPatientCode,
            DateTime mTransDate,
            double mWeight,
            int mInfantFeeding,
            int mOnCotrim,
            int mHIVTestType,
            int mHIVTest,
            int mHIVTestResult,
            DateTime? mHIVTestDate,
            int mReferedToCTC,
            string mRemarks,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Add_MotherChild";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;
            if (clsGlobal.IsNullDate(mHIVTestDate) == false)
            {
                mHIVTestDate = mHIVTestDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpmtctmotherchild_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            string mBookingNo = "";

            #region check for bookingno
            try
            {
                mCommand.CommandText = "select * from ctc_bookings where patientcode='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["autocode"].ToString().Trim();

                    if (Convert.ToDateTime(mDataReader["bookdate"]) != mTransDate)
                    {
                        mCtcClient.Exe_Result = 0;
                        mCtcClient.Exe_Message = "Client is not booked for services";
                        return mCtcClient;
                    }
                }
                else
                {
                    mCtcClient.Exe_Result = 0;
                    mCtcClient.Exe_Message = "Client is not booked for services";
                    return mCtcClient;
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
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

                List<clsDataField> mDataFields;

                #region ctc_pmtctmotherchildvisits

                DataTable mDtData = new DataTable("data");
                mCommand.CommandText = "select * from ctc_pmtctmotherchildvisits where patientcode='" + mPatientCode.Trim() + "' and booking='" + mBookingNo + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);

                if (mDtData.Rows.Count == 0)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("weight", DataTypes.dbdecimal.ToString(), mWeight));
                    mDataFields.Add(new clsDataField("infantfeeding", DataTypes.dbnumber.ToString(), mInfantFeeding));
                    mDataFields.Add(new clsDataField("oncotrim", DataTypes.dbnumber.ToString(), mOnCotrim));
                    mDataFields.Add(new clsDataField("hivtesttype", DataTypes.dbnumber.ToString(), mHIVTestType));
                    mDataFields.Add(new clsDataField("hivtest", DataTypes.dbnumber.ToString(), mHIVTest));
                    mDataFields.Add(new clsDataField("hivtestresult", DataTypes.dbnumber.ToString(), mHIVTestResult));
                    mDataFields.Add(new clsDataField("hivtestdate", DataTypes.dbdatetime.ToString(), mHIVTestDate));
                    mDataFields.Add(new clsDataField("referedtoctc", DataTypes.dbnumber.ToString(), mReferedToCTC));
                    mDataFields.Add(new clsDataField("remarks", DataTypes.dbstring.ToString(), mRemarks.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("ctc_pmtctmotherchildvisits", mDataFields);
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    int mAutoCode = Convert.ToInt32(mDtData.Rows[0]["autocode"]);

                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                    mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                    mDataFields.Add(new clsDataField("weight", DataTypes.dbdecimal.ToString(), mWeight));
                    mDataFields.Add(new clsDataField("infantfeeding", DataTypes.dbnumber.ToString(), mInfantFeeding));
                    mDataFields.Add(new clsDataField("oncotrim", DataTypes.dbnumber.ToString(), mOnCotrim));
                    mDataFields.Add(new clsDataField("hivtesttype", DataTypes.dbnumber.ToString(), mHIVTestType));
                    mDataFields.Add(new clsDataField("hivtest", DataTypes.dbnumber.ToString(), mHIVTest));
                    mDataFields.Add(new clsDataField("hivtestresult", DataTypes.dbnumber.ToString(), mHIVTestResult));
                    mDataFields.Add(new clsDataField("hivtestdate", DataTypes.dbdatetime.ToString(), mHIVTestDate));
                    mDataFields.Add(new clsDataField("referedtoctc", DataTypes.dbnumber.ToString(), mReferedToCTC));
                    mDataFields.Add(new clsDataField("remarks", DataTypes.dbstring.ToString(), mRemarks.Trim()));
                    mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_pmtctmotherchildvisits", mDataFields, "autocode=" + mAutoCode);
                    mCommand.ExecuteNonQuery();
                }

                //this patient has to be available when doing searching
                mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("fromctc", DataTypes.dbnumber.ToString(), 1));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("patients", mDataFields, "code='" + mPatientCode.Trim() + "'");
                mCommand.ExecuteNonQuery();

                #endregion

                //commit
                mTrans.Commit();

                //get patient
                mCtcClient = this.Get_Client("code", mPatientCode);

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Edit_MotherChild
        public AfyaPro_Types.clsCtcClient Edit_MotherChild(
            int mAutoCode,
            DateTime mTransDate,
            double mWeight,
            int mInfantFeeding,
            int mOnCotrim,
            int mHIVTestType,
            int mHIVTest,
            int mHIVTestResult,
            DateTime? mHIVTestDate,
            int mReferedToCTC,
            string mRemarks,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Edit_MotherChild";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;
            if (clsGlobal.IsNullDate(mHIVTestDate) == false)
            {
                mHIVTestDate = mHIVTestDate.Value.Date;
            }

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpmtctmotherchild_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            string mBookingNo = "";
            string mPatientCode = "";

            #region check for bookingno
            try
            {
                mCommand.CommandText = "select * from ctc_pmtctmotherchildvisits where autocode=" + mAutoCode;
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBookingNo = mDataReader["booking"].ToString().Trim();
                    mPatientCode = mDataReader["patientcode"].ToString().Trim();
                }
            }
            catch (OdbcException ex)
            {
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
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

                #region ctc_pmtctmotherchildvisits

                List<clsDataField> mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBookingNo.Trim()));
                mDataFields.Add(new clsDataField("patientcode", DataTypes.dbstring.ToString(), mPatientCode.Trim()));
                mDataFields.Add(new clsDataField("weight", DataTypes.dbdecimal.ToString(), mWeight));
                mDataFields.Add(new clsDataField("infantfeeding", DataTypes.dbnumber.ToString(), mInfantFeeding));
                mDataFields.Add(new clsDataField("oncotrim", DataTypes.dbnumber.ToString(), mOnCotrim));
                mDataFields.Add(new clsDataField("hivtesttype", DataTypes.dbnumber.ToString(), mHIVTestType));
                mDataFields.Add(new clsDataField("hivtest", DataTypes.dbnumber.ToString(), mHIVTest));
                mDataFields.Add(new clsDataField("hivtestresult", DataTypes.dbnumber.ToString(), mHIVTestResult));
                mDataFields.Add(new clsDataField("hivtestdate", DataTypes.dbdatetime.ToString(), mHIVTestDate));
                mDataFields.Add(new clsDataField("referedtoctc", DataTypes.dbnumber.ToString(), mReferedToCTC));
                mDataFields.Add(new clsDataField("remarks", DataTypes.dbstring.ToString(), mRemarks.Trim()));
                mDataFields.Add(new clsDataField("userid", DataTypes.dbstring.ToString(), mUserId.Trim()));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("ctc_pmtctmotherchildvisits", mDataFields, "autocode=" + mAutoCode);
                mCommand.ExecuteNonQuery();

                #endregion

                //commit
                mTrans.Commit();

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Delete_MotherChild
        public AfyaPro_Types.clsCtcClient Delete_MotherChild(
            int mAutoCode,
            string mMachineName,
            string mMachineUser,
            string mUserId)
        {
            string mFunctionName = "Delete_MotherChild";

            AfyaPro_Types.clsCtcClient mCtcClient = new AfyaPro_Types.clsCtcClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;

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
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpmtctmotherchild_delete.ToString(), mUserId);
            if (mGranted == false)
            {
                mCtcClient.Exe_Result = 0;
                mCtcClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mCtcClient;
            }
            #endregion

            #region delete
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //ctc_preartlog
                mCommand.CommandText = "delete from ctc_pmtctmotherchildvisits where autocode=" + mAutoCode;
                mCommand.ExecuteNonQuery();

                //commit
                mTrans.Commit();

                //return
                return mCtcClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mCtcClient.Exe_Result = -1;
                mCtcClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mCtcClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #endregion
    }
}
