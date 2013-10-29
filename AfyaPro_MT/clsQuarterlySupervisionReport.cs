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
    public class clsQuarterlySupervisionReport : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsContactableStaffs";

        #endregion

       
      

        #region View_ContactableClinicStaff
        public DataTable View_ContactableClinicStaff()
        {
            String mFunctionName = "View_ContactableClinicStaff";

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
                string mCommandText = "select Staffname, Position, Phone from ctc_contactablestaff";


                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("ContactableClinicStaff");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders


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

        #region Add_Staff
        public AfyaPro_Types.clsContactableStaff Add_Staff(
            string mStaffName,
            string mPosition,
            string mPhone)
        {
            String mFunctionName = "Add_Staff";

            AfyaPro_Types.clsContactableStaff mStaff = new AfyaPro_Types.clsContactableStaff();
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
                mStaff.Exe_Result = -1;
                mStaff.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mStaff;
            }

            #endregion
                                           
           
            #region add
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;
                               

               
                #region patients

                mCommand.CommandText = "insert into ctc_contactablestaff(staffname, phone, position) values('" + mStaffName.Trim() + "', '" + mPhone.Trim() + "', '" + mPosition.Trim() + "')";
                mCommand.ExecuteNonQuery();

                #endregion

               
              
                mTrans.Commit();

              
                return mStaff;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mStaff.Exe_Result = -1;
                mStaff.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mStaff;
            }
            finally
            {
                mConn.Close();
            }
            #endregion


        }
        #endregion

        #region Remove_Staff
        public AfyaPro_Types.clsContactableStaff Remove_Staff(string mStaffName)
                       
        {
            String mFunctionName = "Remove_Staff";

            AfyaPro_Types.clsContactableStaff mStaff = new AfyaPro_Types.clsContactableStaff();
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
                mStaff.Exe_Result = -1;
                mStaff.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mStaff;
            }

            #endregion


            #region Remove
            try
            {
                
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;



                #region patients

                mCommand.CommandText = "delete from ctc_contactablestaff where staffname ='" + mStaffName.Trim() + "'";
                mCommand.ExecuteNonQuery();

                #endregion



                mTrans.Commit();


                return mStaff;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mStaff.Exe_Result = -1;
                mStaff.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mStaff;
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
