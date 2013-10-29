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
    public class clsPriceCategories : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsPriceCategories";

        #endregion

        #region View
        public DataTable View(String mFilter, String mOrder)
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
                string mCommandText = "select * from facilitypricecategories";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("facilitypricecategories");
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

        #region View_Active
        public DataTable View_Active(string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_Active";

            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            DataTable mDtPriceCategories = new DataTable("pricecategories");
            DataTable mDtSavedData = new DataTable("pricecategories");

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
                string mCommandText = "select * from facilitypricecategories";
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSavedData);

                mDtPriceCategories.Columns.Add("pricename");
                mDtPriceCategories.Columns.Add("pricedescription");
                mDtPriceCategories.RemotingFormat = SerializationFormat.Binary;

                if (mDtSavedData.Rows.Count > 0)
                {
                    for (Int16 mIndex = 0; mIndex < 10; mIndex++)
                    {
                        if (Convert.ToInt16(mDtSavedData.Rows[0]["useprice" + (mIndex + 1)]) == 1)
                        {
                            DataRow mNewRow = mDtPriceCategories.NewRow();
                            mNewRow["pricename"] = "price" + (mIndex + 1);
                            mNewRow["pricedescription"] = mDtSavedData.Rows[0]["price" + (mIndex + 1)].ToString().Trim();
                            mDtPriceCategories.Rows.Add(mNewRow);
                            mDtPriceCategories.AcceptChanges();
                        }
                    }
                }

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
                            if (mDtPriceCategories.Columns.Contains((string)mElement.Element("controlname").Value.Trim().Substring(3)) == true)
                            {
                                mDtPriceCategories.Columns[(string)mElement.Element("controlname").Value.Trim().Substring(3)].Caption =
                                    (string)mElement.Element("description").Value.Trim();
                            }
                        }
                    }
                    catch { }
                }

                #endregion

                return mDtPriceCategories;
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

        #region Edit
        public AfyaPro_Types.clsResult Edit(Int16 mUserPrice1, Int16 mUserPrice2, Int16 mUserPrice3, Int16 mUserPrice4,
            Int16 mUserPrice5, Int16 mUserPrice6, Int16 mUserPrice7, Int16 mUserPrice8, Int16 mUserPrice9, Int16 mUserPrice10,
            String mPrice1, String mPrice2, String mPrice3, String mPrice4, String mPrice5, String mPrice6, String mPrice7,
            String mPrice8, String mPrice9, String mPrice10, string mUserId)
        {
            String mFunctionName = "Edit";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            Boolean mFound = false;

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.blspricecategories_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region check for existance

            try
            {
                mCommand.CommandText = "select * from facilitypricecategories";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mFound = true;
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

            #region do edit
            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                if (mFound == false)
                {
                    mCommand.CommandText = "insert into facilitypricecategories(useprice1,price1) values(1,'Price 1')";
                    mCommand.ExecuteNonQuery();
                }

                mCommand.CommandText = "update facilitypricecategories set useprice1=" + mUserPrice1 + ",useprice2="
                + mUserPrice2 + ",useprice3=" + mUserPrice3 + ",useprice4=" + mUserPrice4 + ",useprice5=" + mUserPrice5
                + ",useprice6=" + mUserPrice6 + ",useprice7=" + mUserPrice7 + ",useprice8=" + mUserPrice8 + ",useprice9="
                + mUserPrice9 + ",useprice10=" + mUserPrice10 + ",price1='" + mPrice1.Trim() + "',price2='" + mPrice2.Trim()
                + "',price3='" + mPrice3.Trim() + "',price4='" + mPrice4.Trim() + "',price5='" + mPrice5.Trim()
                + "',price6='" + mPrice6.Trim() + "',price7='" + mPrice7.Trim() + "',price8='" + mPrice8.Trim()
                + "',price9='" + mPrice9.Trim() + "',price10='" + mPrice10.Trim() + "'";
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
