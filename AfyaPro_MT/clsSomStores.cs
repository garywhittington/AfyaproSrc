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
    public class clsSomStores : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsSomStores";

        #endregion

        #region View
        public DataTable View(String mFilter, String mOrder, string mLanguageName, string mGridName)
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
                string mCommandText = "select * from som_stores";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("som_stores");
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

        #region Get_OnHandQuantities
        public DataTable Get_OnHandQuantities(string mFilter, string mOrder)
        {
            String mFunctionName = "Get_OnHandQuantities";

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
                DataTable mDtQtyColumns = new DataTable("qtycolumns");
                mCommand.CommandText = "select * from som_productcontrol where 1=2";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtQtyColumns);
                string mSumColumns = "";
                foreach (DataColumn mDataColumn in mDtQtyColumns.Columns)
                {
                    if (mDataColumn.ColumnName.ToLower().StartsWith("qty_") == true)
                    {
                        if (mSumColumns.Trim() == "")
                        {
                            mSumColumns = "sum(" + mDataColumn.ColumnName + ")";
                        }
                        else
                        {
                            mSumColumns = mSumColumns + " + sum(" + mDataColumn.ColumnName + ")";
                        }
                    }
                }

                mSumColumns = "(" + mSumColumns + ")";

                string mCommandText = "select productcode, " + mSumColumns + " onhandqty from som_productcontrol";
                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }
                mCommandText = mCommandText + " group by productcode";

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("som_productcontrol");
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

        #region Get_OnHandQuantities
        public DataTable Get_OnHandQuantities(string mStoreCode, string mFilter, string mOrder)
        {
            String mFunctionName = "Get_OnHandQuantities";

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
                string mCommandText = "select productcode, sum(qty_" + mStoreCode.Trim().ToLower() + ") onhandqty from som_productcontrol";
                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }
                mCommandText = mCommandText + " group by productcode";

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("som_productcontrol");
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

        #region Get_OnHandQuantities
        public DataTable Get_OnHandQuantities(OdbcCommand mCommand, string mFilter, string mOrder)
        {
            String mFunctionName = "Get_OnHandQuantities";

            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            try
            {
                DataTable mDtQtyColumns = new DataTable("qtycolumns");
                mCommand.CommandText = "select * from som_productcontrol where 1=2";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtQtyColumns);
                string mSumColumns = "";
                foreach (DataColumn mDataColumn in mDtQtyColumns.Columns)
                {
                    if (mDataColumn.ColumnName.ToLower().StartsWith("qty_") == true)
                    {
                        if (mSumColumns.Trim() == "")
                        {
                            mSumColumns = "sum(" + mDataColumn.ColumnName + ")";
                        }
                        else
                        {
                            mSumColumns = mSumColumns + " + sum(" + mDataColumn.ColumnName + ")";
                        }
                    }
                }

                mSumColumns = "(" + mSumColumns + ")";

                string mCommandText = "select productcode, " + mSumColumns + " onhandqty from som_productcontrol";
                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }
                mCommandText = mCommandText + " group by productcode";

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("som_productcontrol");
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
        }
        #endregion

        #region Get_OnHandQuantitiesByExpiryDates
        public DataTable Get_OnHandQuantitiesByExpiryDates(string mStoreCode, string mFilter, string mOrder)
        {
            String mFunctionName = "Get_OnHandQuantitiesByExpiryDates";

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
                string mCommandText = "select productcode,expirydate,quantity onhandqty from som_productexpirydates"
                    + " where storecode='" + mStoreCode + "'";
                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and " + mFilter;
                }
                mCommandText = mCommandText + " group by productcode,expirydate";

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("som_productexpirydates");
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
        public AfyaPro_Types.clsResult Add(String mCode, String mDescription, string mUserId)
        {
            String mFunctionName = "Add";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivsstores_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region check 4 duplicate
            try
            {
                mCommand.CommandText = "select * from som_stores where code='"
                + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreCodeIsInUse.ToString();
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

            #region store code should not be the same as supplier code
            try
            {
                mCommand.CommandText = "select * from som_suppliers where code='"
                + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreCodeSameAsSupplierCode.ToString();
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

                //som_stores
                mCommand.CommandText = "insert into som_stores(code,description) values('" + mCode.Trim() + "','" + mDescription.Trim() + "')";
                mCommand.ExecuteNonQuery();

                bool mColumnFound = false;

                #region products visibility column

                mColumnFound = false;
                DataTable mDtProductColumns = new DataTable("productcolumns");
                mCommand.CommandText = "select * from som_products where 1=2";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtProductColumns);

                
                string mProductVisibilityFieldName = "visible_" + mCode.Trim().ToLower();

                foreach (DataColumn mDataColumn in mDtProductColumns.Columns)
                {
                    if (mProductVisibilityFieldName == mDataColumn.ColumnName.Trim().ToLower())
                    {
                        mColumnFound = true;
                        break;
                    }
                }

                if (mColumnFound == false)
                {
                    switch (clsGlobal.gAfyaPro_ServerType)
                    {
                        case 0:
                            {
                                mCommand.CommandText = "ALTER TABLE som_products ADD "
                                + mProductVisibilityFieldName + " INTEGER(11) DEFAULT '1'";
                                mCommand.ExecuteNonQuery();
                            }
                            break;
                        case 1:
                            {
                                mCommand.CommandText = "ALTER TABLE som_products ADD "
                                 + mProductVisibilityFieldName + " INT DEFAULT ('1')";
                                mCommand.ExecuteNonQuery();
                            }
                            break;
                    }
                }

                #endregion

                #region customers visibility column

                DataTable mDtCustomerColumns = new DataTable("customercolumns");
                mCommand.CommandText = "select * from som_customers where 1=2";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtCustomerColumns);

                mColumnFound = false;
                string mCustomerVisibilityFieldName = "visible_" + mCode.Trim().ToLower();

                foreach (DataColumn mDataColumn in mDtCustomerColumns.Columns)
                {
                    if (mCustomerVisibilityFieldName == mDataColumn.ColumnName.Trim().ToLower())
                    {
                        mColumnFound = true;
                        break;
                    }
                }

                if (mColumnFound == false)
                {
                    switch (clsGlobal.gAfyaPro_ServerType)
                    {
                        case 0:
                            {
                                mCommand.CommandText = "ALTER TABLE som_customers ADD "
                                + mCustomerVisibilityFieldName + " INTEGER(11) DEFAULT '1'";
                                mCommand.ExecuteNonQuery();
                            }
                            break;
                        case 1:
                            {
                                mCommand.CommandText = "ALTER TABLE som_customers ADD "
                                 + mCustomerVisibilityFieldName + " INT DEFAULT ('1')";
                                mCommand.ExecuteNonQuery();
                            }
                            break;
                    }
                }

                #endregion

                #region productcontrol qty column

                mColumnFound = false;
                DataTable mDtProductControlColumns = new DataTable("productcontrolcolumns");
                mCommand.CommandText = "select * from som_productcontrol where 1=2";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtProductControlColumns);


                string mProductControlQtyFieldName = "qty_" + mCode.Trim().ToLower();

                foreach (DataColumn mDataColumn in mDtProductControlColumns.Columns)
                {
                    if (mProductControlQtyFieldName == mDataColumn.ColumnName.Trim().ToLower())
                    {
                        mColumnFound = true;
                        break;
                    }
                }

                if (mColumnFound == false)
                {
                    switch (clsGlobal.gAfyaPro_ServerType)
                    {
                        case 0:
                            {
                                mCommand.CommandText = "ALTER TABLE som_productcontrol ADD "
                                + mProductControlQtyFieldName + " DOUBLE(16,2) DEFAULT '0.00'";
                                mCommand.ExecuteNonQuery();
                            }
                            break;
                        case 1:
                            {
                                mCommand.CommandText = "ALTER TABLE som_productcontrol ADD "
                                 + mProductControlQtyFieldName + " DECIMAL(16,2) DEFAULT '0.00'";
                                mCommand.ExecuteNonQuery();
                            }
                            break;
                    }
                }

                #endregion

                #region producttransactions qty in/out columns

                mColumnFound = false;
                DataTable mDtProductTransactionColumns = new DataTable("producttransactioncolumns");
                mCommand.CommandText = "select * from som_producttransactions where 1=2";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtProductTransactionColumns);

                //qtyin
                string mProductTransactionsQtyFieldName = "qtyin_" + mCode.Trim().ToLower();

                foreach (DataColumn mDataColumn in mDtProductTransactionColumns.Columns)
                {
                    if (mProductTransactionsQtyFieldName == mDataColumn.ColumnName.Trim().ToLower())
                    {
                        mColumnFound = true;
                        break;
                    }
                }

                if (mColumnFound == false)
                {
                    switch (clsGlobal.gAfyaPro_ServerType)
                    {
                        case 0:
                            {
                                mCommand.CommandText = "ALTER TABLE som_producttransactions ADD "
                                + mProductTransactionsQtyFieldName + " DOUBLE(16,2) DEFAULT '0.00'";
                                mCommand.ExecuteNonQuery();
                            }
                            break;
                        case 1:
                            {
                                mCommand.CommandText = "ALTER TABLE som_producttransactions ADD "
                                 + mProductTransactionsQtyFieldName + " DECIMAL(16,2) DEFAULT '0.00'";
                                mCommand.ExecuteNonQuery();
                            }
                            break;
                    }
                }

                //qtyout
                mProductTransactionsQtyFieldName = "qtyout_" + mCode.Trim().ToLower();

                foreach (DataColumn mDataColumn in mDtProductTransactionColumns.Columns)
                {
                    if (mProductTransactionsQtyFieldName == mDataColumn.ColumnName.Trim().ToLower())
                    {
                        mColumnFound = true;
                        break;
                    }
                }

                if (mColumnFound == false)
                {
                    switch (clsGlobal.gAfyaPro_ServerType)
                    {
                        case 0:
                            {
                                mCommand.CommandText = "ALTER TABLE som_producttransactions ADD "
                                + mProductTransactionsQtyFieldName + " DOUBLE(16,2) DEFAULT '0.00'";
                                mCommand.ExecuteNonQuery();
                            }
                            break;
                        case 1:
                            {
                                mCommand.CommandText = "ALTER TABLE som_producttransactions ADD "
                                 + mProductTransactionsQtyFieldName + " DECIMAL(16,2) DEFAULT '0.00'";
                                mCommand.ExecuteNonQuery();
                            }
                            break;
                    }
                }

                #endregion

                #region physicalstockbalances qty column

                mColumnFound = false;
                DataTable mDtPhysicalStockColumns = new DataTable("som_physicalstockbalances");
                mCommand.CommandText = "select * from som_physicalstockbalances where 1=2";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtPhysicalStockColumns);


                string mPhysicalStockQtyFieldName = "qty_" + mCode.Trim().ToLower();

                foreach (DataColumn mDataColumn in mDtPhysicalStockColumns.Columns)
                {
                    if (mPhysicalStockQtyFieldName == mDataColumn.ColumnName.Trim().ToLower())
                    {
                        mColumnFound = true;
                        break;
                    }
                }

                if (mColumnFound == false)
                {
                    switch (clsGlobal.gAfyaPro_ServerType)
                    {
                        case 0:
                            {
                                mCommand.CommandText = "ALTER TABLE som_physicalstockbalances ADD "
                                + mPhysicalStockQtyFieldName + " DOUBLE(16,2) DEFAULT '0.00'";
                                mCommand.ExecuteNonQuery();
                            }
                            break;
                        case 1:
                            {
                                mCommand.CommandText = "ALTER TABLE som_physicalstockbalances ADD "
                                 + mPhysicalStockQtyFieldName + " DECIMAL(16,2) DEFAULT '0.00'";
                                mCommand.ExecuteNonQuery();
                            }
                            break;
                    }
                }

                #endregion

                //commit
                mTrans.Commit();

                Type mCodeKeys = typeof(AfyaPro_Types.clsEnums.AuditTables);
                foreach (string mTableName in Enum.GetNames(mCodeKeys))
                {
                    clsGlobal.Synchronize_AuditTable(mTableName);
                }

                //return
                return mResult;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }

                try
                {
                    mCommand.CommandText = "drop database " + clsGlobal.gInventoryDbName + mCode.Trim().ToLower();
                    mCommand.ExecuteNonQuery();
                }
                catch { }

                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }
            #endregion
        }
        #endregion

        #region Edit
        public AfyaPro_Types.clsResult Edit(String mCode, String mDescription, string mUserId)
        {
            String mFunctionName = "Edit";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivsstores_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region check for code existance

            try
            {
                mCommand.CommandText = "select * from som_stores where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreCodeDoesNotExist.ToString();
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

            #region do edit
            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //som_stores
                mCommand.CommandText = "update som_stores set description = '"
                + mDescription.Trim() + "' where code='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                bool mColumnFound = false;

                #region products visibility column

                mColumnFound = false;
                DataTable mDtProductColumns = new DataTable("productcolumns");
                mCommand.CommandText = "select * from som_products where 1=2";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtProductColumns);


                string mProductVisibilityFieldName = "visible_" + mCode.Trim().ToLower();

                foreach (DataColumn mDataColumn in mDtProductColumns.Columns)
                {
                    if (mProductVisibilityFieldName == mDataColumn.ColumnName.Trim().ToLower())
                    {
                        mColumnFound = true;
                        break;
                    }
                }

                if (mColumnFound == false)
                {
                    switch (clsGlobal.gAfyaPro_ServerType)
                    {
                        case 0:
                            {
                                mCommand.CommandText = "ALTER TABLE som_products ADD "
                                + mProductVisibilityFieldName + " INTEGER(11) DEFAULT '1'";
                                mCommand.ExecuteNonQuery();
                            }
                            break;
                        case 1:
                            {
                                mCommand.CommandText = "ALTER TABLE som_products ADD "
                                 + mProductVisibilityFieldName + " INT DEFAULT ('1')";
                                mCommand.ExecuteNonQuery();
                            }
                            break;
                    }
                }

                #endregion

                #region customers visibility column

                DataTable mDtCustomerColumns = new DataTable("customercolumns");
                mCommand.CommandText = "select * from som_customers where 1=2";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtCustomerColumns);

                mColumnFound = false;
                string mCustomerVisibilityFieldName = "visible_" + mCode.Trim().ToLower();

                foreach (DataColumn mDataColumn in mDtCustomerColumns.Columns)
                {
                    if (mCustomerVisibilityFieldName == mDataColumn.ColumnName.Trim().ToLower())
                    {
                        mColumnFound = true;
                        break;
                    }
                }

                if (mColumnFound == false)
                {
                    switch (clsGlobal.gAfyaPro_ServerType)
                    {
                        case 0:
                            {
                                mCommand.CommandText = "ALTER TABLE som_customers ADD "
                                + mCustomerVisibilityFieldName + " INTEGER(11) DEFAULT '1'";
                                mCommand.ExecuteNonQuery();
                            }
                            break;
                        case 1:
                            {
                                mCommand.CommandText = "ALTER TABLE som_customers ADD "
                                 + mCustomerVisibilityFieldName + " INT DEFAULT ('1')";
                                mCommand.ExecuteNonQuery();
                            }
                            break;
                    }
                }

                #endregion

                #region productcontrol qty column

                mColumnFound = false;
                DataTable mDtProductControlColumns = new DataTable("productcontrolcolumns");
                mCommand.CommandText = "select * from som_productcontrol where 1=2";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtProductControlColumns);


                string mProductControlQtyFieldName = "qty_" + mCode.Trim().ToLower();

                foreach (DataColumn mDataColumn in mDtProductControlColumns.Columns)
                {
                    if (mProductControlQtyFieldName == mDataColumn.ColumnName.Trim().ToLower())
                    {
                        mColumnFound = true;
                        break;
                    }
                }

                if (mColumnFound == false)
                {
                    switch (clsGlobal.gAfyaPro_ServerType)
                    {
                        case 0:
                            {
                                mCommand.CommandText = "ALTER TABLE som_productcontrol ADD "
                                + mProductControlQtyFieldName + " DOUBLE(16,2) DEFAULT '0.00'";
                                mCommand.ExecuteNonQuery();
                            }
                            break;
                        case 1:
                            {
                                mCommand.CommandText = "ALTER TABLE som_productcontrol ADD "
                                 + mProductControlQtyFieldName + " DECIMAL(16,2) DEFAULT '0.00'";
                                mCommand.ExecuteNonQuery();
                            }
                            break;
                    }
                }

                #endregion

                #region producttransactions qty in/out columns

                mColumnFound = false;
                DataTable mDtProductTransactionColumns = new DataTable("producttransactioncolumns");
                mCommand.CommandText = "select * from som_producttransactions where 1=2";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtProductTransactionColumns);

                //qtyin
                string mProductTransactionsQtyFieldName = "qtyin_" + mCode.Trim().ToLower();

                foreach (DataColumn mDataColumn in mDtProductTransactionColumns.Columns)
                {
                    if (mProductTransactionsQtyFieldName == mDataColumn.ColumnName.Trim().ToLower())
                    {
                        mColumnFound = true;
                        break;
                    }
                }

                if (mColumnFound == false)
                {
                    switch (clsGlobal.gAfyaPro_ServerType)
                    {
                        case 0:
                            {
                                mCommand.CommandText = "ALTER TABLE som_producttransactions ADD "
                                + mProductTransactionsQtyFieldName + " DOUBLE(16,2) DEFAULT '0.00'";
                                mCommand.ExecuteNonQuery();
                            }
                            break;
                        case 1:
                            {
                                mCommand.CommandText = "ALTER TABLE som_producttransactions ADD "
                                 + mProductTransactionsQtyFieldName + " DECIMAL(16,2) DEFAULT '0.00'";
                                mCommand.ExecuteNonQuery();
                            }
                            break;
                    }
                }

                //qtyout
                mProductTransactionsQtyFieldName = "qtyout_" + mCode.Trim().ToLower();

                foreach (DataColumn mDataColumn in mDtProductTransactionColumns.Columns)
                {
                    if (mProductTransactionsQtyFieldName == mDataColumn.ColumnName.Trim().ToLower())
                    {
                        mColumnFound = true;
                        break;
                    }
                }

                if (mColumnFound == false)
                {
                    switch (clsGlobal.gAfyaPro_ServerType)
                    {
                        case 0:
                            {
                                mCommand.CommandText = "ALTER TABLE som_producttransactions ADD "
                                + mProductTransactionsQtyFieldName + " DOUBLE(16,2) DEFAULT '0.00'";
                                mCommand.ExecuteNonQuery();
                            }
                            break;
                        case 1:
                            {
                                mCommand.CommandText = "ALTER TABLE som_producttransactions ADD "
                                 + mProductTransactionsQtyFieldName + " DECIMAL(16,2) DEFAULT '0.00'";
                                mCommand.ExecuteNonQuery();
                            }
                            break;
                    }
                }

                #endregion

                #region physicalstockbalances qty column

                mColumnFound = false;
                DataTable mDtPhysicalStockColumns = new DataTable("som_physicalstockbalances");
                mCommand.CommandText = "select * from som_physicalstockbalances where 1=2";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtPhysicalStockColumns);


                string mPhysicalStockQtyFieldName = "qty_" + mCode.Trim().ToLower();

                foreach (DataColumn mDataColumn in mDtPhysicalStockColumns.Columns)
                {
                    if (mPhysicalStockQtyFieldName == mDataColumn.ColumnName.Trim().ToLower())
                    {
                        mColumnFound = true;
                        break;
                    }
                }

                if (mColumnFound == false)
                {
                    switch (clsGlobal.gAfyaPro_ServerType)
                    {
                        case 0:
                            {
                                mCommand.CommandText = "ALTER TABLE som_physicalstockbalances ADD "
                                + mPhysicalStockQtyFieldName + " DOUBLE(16,2) DEFAULT '0.00'";
                                mCommand.ExecuteNonQuery();
                            }
                            break;
                        case 1:
                            {
                                mCommand.CommandText = "ALTER TABLE som_physicalstockbalances ADD "
                                 + mPhysicalStockQtyFieldName + " DECIMAL(16,2) DEFAULT '0.00'";
                                mCommand.ExecuteNonQuery();
                            }
                            break;
                    }
                }

                #endregion

                mTrans.Commit();

                Type mCodeKeys = typeof(AfyaPro_Types.clsEnums.AuditTables);
                foreach (string mTableName in Enum.GetNames(mCodeKeys))
                {
                    clsGlobal.Synchronize_AuditTable(mTableName);
                }

                //return
                return mResult;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }

                try
                {
                    mCommand.CommandText = "drop database " + clsGlobal.gInventoryDbName + mCode.Trim().ToLower();
                    mCommand.ExecuteNonQuery();
                }
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

        #region Delete
        public AfyaPro_Types.clsResult Delete(String mCode, string mUserId)
        {
            String mFunctionName = "Delete";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivsstores_delete.ToString(), mUserId);
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
                mCommand.CommandText = "select * from som_stores where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IVS_StoreCodeDoesNotExist.ToString();
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

            #region do delete
            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //som_stores
                mCommand.CommandText = "delete from som_stores where code='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                bool mColumnFound = false;

                #region products visibility column

                DataTable mDtProductColumns = new DataTable("productcolumns");
                mCommand.CommandText = "select * from som_products where 1=2";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtProductColumns);

                mColumnFound = false;
                string mProductVisibilityFieldName = "visible_" + mCode.Trim().ToLower();

                foreach (DataColumn mDataColumn in mDtProductColumns.Columns)
                {
                    if (mProductVisibilityFieldName == mDataColumn.ColumnName.Trim().ToLower())
                    {
                        mColumnFound = true;
                        break;
                    }
                }

                if (mColumnFound == true)
                {
                    mCommand.CommandText = "ALTER TABLE som_products DROP " + mProductVisibilityFieldName;
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region suppliers visibility column

                DataTable mDtSupplierColumns = new DataTable("suppliercolumns");
                mCommand.CommandText = "select * from som_suppliers where 1=2";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSupplierColumns);

                mColumnFound = false;
                string mSupplierVisibilityFieldName = "visible_" + mCode.Trim().ToLower();

                foreach (DataColumn mDataColumn in mDtSupplierColumns.Columns)
                {
                    if (mSupplierVisibilityFieldName == mDataColumn.ColumnName.Trim().ToLower())
                    {
                        mColumnFound = true;
                        break;
                    }
                }

                if (mColumnFound == true)
                {
                    mCommand.CommandText = "ALTER TABLE som_suppliers DROP " + mSupplierVisibilityFieldName;
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region customers visibility column

                DataTable mDtCustomerColumns = new DataTable("customercolumns");
                mCommand.CommandText = "select * from som_customers where 1=2";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtCustomerColumns);

                mColumnFound = false;
                string mCustomerVisibilityFieldName = "visible_" + mCode.Trim().ToLower();

                foreach (DataColumn mDataColumn in mDtCustomerColumns.Columns)
                {
                    if (mCustomerVisibilityFieldName == mDataColumn.ColumnName.Trim().ToLower())
                    {
                        mColumnFound = true;
                        break;
                    }
                }

                if (mColumnFound == true)
                {
                    mCommand.CommandText = "ALTER TABLE som_customers DROP " + mSupplierVisibilityFieldName;
                    mCommand.ExecuteNonQuery();
                }

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
            #endregion
        }
        #endregion
    }
}
