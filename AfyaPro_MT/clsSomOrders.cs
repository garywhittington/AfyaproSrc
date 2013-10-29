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
    public class clsSomOrders : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsSomOrders";

        #endregion

        #region View_Orders
        public DataTable View_Orders(bool mDateSpecified, DateTime mDateFrom, DateTime mDateTo, String mExtraFilter, string mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_Orders";

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

                string mCommandText = "select * from som_orders";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mExtraFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("som_orders");
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

        #region View_OrderItems
        public DataTable View_OrderItems(string mFilter, string mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_OrderItems";

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
                string mCommandText = "select * from som_orderitems";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("som_orderitems");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                mDataTable.Columns.Add("onhandqty", typeof(System.Double));
                mDataTable.Columns.Add("receivedtodate", typeof(System.Double));
                mDataTable.Columns.Add("receivedqty", typeof(System.Double));
                mDataTable.Columns.Add("amount", typeof(System.Double));

                #region get onhandqty and receivedqty

                //onhand
                clsSomStores mSomStores = new clsSomStores();
                DataTable mDtOnHand = mSomStores.Get_OnHandQuantities("", "");
                DataView mDvOnHand = new DataView();
                mDvOnHand.Table = mDtOnHand;
                mDvOnHand.Sort = "productcode";

                foreach (DataRow mDataRow in mDataTable.Rows)
                {
                    double mOnHandQty = 0;
                    double mReceivedQty = 0;
                    int mPiecesInPackage = Convert.ToInt32(mDataRow["piecesinpackage"]);

                    int mRowIndex = mDvOnHand.Find(mDataRow["productcode"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mOnHandQty = Convert.ToDouble(mDvOnHand[mRowIndex]["onhandqty"]) / mPiecesInPackage;
                    }

                    mDataRow.BeginEdit();
                    mDataRow["onhandqty"] = mOnHandQty;
                    mDataRow["receivedtodate"] = 0;
                    mDataRow["receivedqty"] = mReceivedQty;
                    mDataRow["amount"] = Convert.ToDouble(mDataRow["orderedqty"]) * Convert.ToDouble(mDataRow["transprice"]);
                    mDataRow.EndEdit();
                    mDataTable.AcceptChanges();
                }

                #endregion

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

        #region Get_ReceivedToDate
        public DataTable Get_ReceivedToDate(string mFilter, string mOrder)
        {
            String mFunctionName = "Get_ReceivedToDate";

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
                string mCommandText = "select itemorderid, sum(receivedqty * piecesinpackage) receivedqty from som_orderreceiveditems";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                mCommandText = mCommandText + " group by itemorderid";

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;


                DataTable mDataTable = new DataTable("som_orderreceiveditems");
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
        public AfyaPro_Types.clsResult Add(Int16 mGenerateCode, DateTime mTransDate, string mOrderNo, 
            string mTitle, string mShipToDescription, string mSupplierCode, string mSupplierDescription, string mCurrencyCode, 
            string mCurrencyDescription, double mExchangeRate, string mCurrencySymbol, string mRemarks, DataTable mDtItems, string mUserId)
        {
            String mFunctionName = "Add";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            OdbcTransaction mTrans = null;
            int mRecsAffected = -1;

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivorders_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region auto generate order no, if option is on

            if (mGenerateCode == 1)
            {
                clsAutoCodes objAutoCodes = new clsAutoCodes();
                AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                mObjCode = objAutoCodes.Next_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.orderno), "som_orders", "orderno");
                if (mObjCode.Exe_Result == -1)
                {
                    mResult.Exe_Result = mObjCode.Exe_Result;
                    mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                    return mResult;
                }
                mOrderNo = mObjCode.GeneratedCode;
            }

            #endregion

            #region check 4 duplicate
            try
            {
                mCommand.CommandText = "select orderno from som_orders where orderno='"
                + mOrderNo.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IV_OrderNoIsInUse.ToString();
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

                //som_orders
                mCommand.CommandText = "insert into som_orders(sysdate,transdate,orderno,ordertitle,shiptodescription,suppliercode,"
                + "supplierdescription,currencycode,currencydescription,exchangerate,currencysymbol,remarks,orderstatus,userid) values(" 
                + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mOrderNo.Trim() 
                + "','" + mTitle.Trim() + "','" + mShipToDescription.Trim() + "','" + mSupplierCode.Trim() + "','" + mSupplierDescription.Trim() 
                + "','" + mCurrencyCode.Trim() + "','" + mCurrencyDescription.Trim() + "'," + mExchangeRate + ",'" + mCurrencySymbol.Trim() 
                + "','" + mRemarks.Trim() + "','" + AfyaPro_Types.clsEnums.SomOrderStatus.Open.ToString() + "','" + mUserId.Trim() + "')";
                mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit som_orders

                if (mRecsAffected > 0)
                {
                    string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_orders";
                    string mAuditingFields = clsGlobal.AuditingFields();
                    string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                        AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                    mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,orderno,ordertitle,shiptodescription,suppliercode,"
                    + "supplierdescription,currencycode,currencydescription,exchangerate,currencysymbol,remarks,orderstatus,userid," + mAuditingFields 
                    + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mOrderNo.Trim()
                    + "','" + mTitle.Trim() + "','" + mShipToDescription.Trim() + "','" + mSupplierCode.Trim() + "','" + mSupplierDescription.Trim()
                    + "','" + mCurrencyCode.Trim() + "','" + mCurrencyDescription.Trim() + "'," + mExchangeRate + ",'" + mCurrencySymbol.Trim()
                    + "','" + mRemarks.Trim() + "','" + AfyaPro_Types.clsEnums.SomOrderStatus.Open.ToString() + "','" + mUserId.Trim() + "'," 
                    + mAuditingValues + ")";
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                string mProductCodesList = "";
                foreach (DataRow mDataRow in mDtItems.Rows)
                {
                    if (mProductCodesList.Trim() == "")
                    {
                        mProductCodesList = "'" + mDataRow["productcode"].ToString().Trim() + "'";
                    }
                    else
                    {
                        mProductCodesList = mProductCodesList + ",'" + mDataRow["productcode"].ToString().Trim() + "'";
                    }
                }

                DataTable mDtProducts = new DataTable("products");
                if (mProductCodesList.Trim() != "")
                {
                    mCommand.CommandText = "select * from som_products where code in (" + mProductCodesList + ")";
                }
                else
                {
                    mCommand.CommandText = "select * from som_products where 1=2";
                }
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtProducts);
                DataView mDvProducts = new DataView();
                mDvProducts.Table = mDtProducts;
                mDvProducts.Sort = "code";

                int mRowCount = 1;
                foreach (DataRow mDataRow in mDtItems.Rows)
                {
                    string mProductCode = mDataRow["productcode"].ToString().Trim();
                    string mPackagingCode = mDataRow["packagingcode"].ToString().Trim();
                    string mPackagingDescription = mDataRow["packagingdescription"].ToString().Trim();
                    int mPiecesInPackage = Convert.ToInt32(mDataRow["piecesinpackage"]);
                    double mOrderedQty = Convert.ToDouble(mDataRow["orderedqty"]);
                    double mTransPrice = Convert.ToDouble(mDataRow["transprice"]);

                    string mExtraFields = clsGlobal.Build_FieldsForProductExtraInfos();
                    string mExtraFieldValues = clsGlobal.Build_FieldValuesForProductExtraInfos(mDvProducts, mProductCode);

                    string mItemOrderId = clsGlobal.Generate_AutoId() + mRowCount;

                    //som_orderitems
                    mCommand.CommandText = "insert into som_orderitems(sysdate,transdate,orderno,itemorderid,productcode,packagingcode,"
                    + "packagingdescription,piecesinpackage,orderedqty,transprice,userid," + mExtraFields + ") values("
                    + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                    + mOrderNo.Trim() + "','" + mItemOrderId  + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "',"
                    + mPiecesInPackage + "," + mOrderedQty + "," + mTransPrice + ",'" + mUserId.Trim() + "'," + mExtraFieldValues + ")";
                    mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit som_orderitems

                    if (mRecsAffected > 0)
                    {
                        string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_orderitems";
                        string mAuditingFields = clsGlobal.AuditingFields();
                        string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                            AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                        mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,orderno,itemorderid,productcode,packagingcode,"
                        + "packagingdescription,piecesinpackage,orderedqty,transprice,userid," + mExtraFields + "," + mAuditingFields
                        + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                        + mOrderNo.Trim() + "','" + mItemOrderId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "',"
                        + mPiecesInPackage + "," + mOrderedQty + "," + mTransPrice + ",'" + mUserId.Trim() + "',"
                        + mExtraFieldValues + "," + mAuditingValues + ")";
                        mCommand.ExecuteNonQuery();
                    }

                    #endregion

                    mRowCount++;
                }

                if (mGenerateCode == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.orderno);
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

        #region Edit
        public AfyaPro_Types.clsResult Edit(DateTime mTransDate, string mOrderNo, 
            string mTitle, string mShipToDescription, string mSupplierCode, string mSupplierDescription, string mCurrencyCode, 
            string mCurrencyDescription, double mExchangeRate, string mCurrencySymbol, string mRemarks, DataTable mDtItems, string mUserId)
        {
            String mFunctionName = "Edit";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            int mRecsAffected = -1;

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivorders_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region check 4 order existance
            try
            {
                mCommand.CommandText = "select * from som_orders where orderno='" + mOrderNo.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IV_OrderNoDoesNotExist.ToString();
                    return mResult;
                }
                else
                {
                    if (mDataReader["orderstatus"].ToString().Trim().ToLower() ==
                        AfyaPro_Types.clsEnums.SomOrderStatus.Closed.ToString().ToLower())
                    {
                        mResult.Exe_Result = 0;
                        mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IV_ClosedOrderCannotbeEdited.ToString();
                        return mResult;
                    }
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

            #region Edit
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //som_orders
                mCommand.CommandText = "update som_orders set transdate=" + clsGlobal.Saving_DateValue(mTransDate) 
                + ",ordertitle='" + mTitle.Trim()
                + "',shiptodescription='" + mShipToDescription.Trim() + "', suppliercode='" + mSupplierCode.Trim()
                + "',supplierdescription='" + mSupplierDescription.Trim() + "',currencycode='" + mCurrencyCode.Trim()
                + "',currencydescription='" + mCurrencyDescription.Trim() + "',exchangerate=" + mExchangeRate
                + ",currencysymbol='" + mCurrencySymbol.Trim() + "',remarks='" + mRemarks.Trim()
                + "' where orderno='" + mOrderNo.Trim() + "'";
                mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit som_orders
                if (mRecsAffected > 0)
                {
                    //get current details
                    DataTable mDtOrders = new DataTable("orders");
                    mCommand.CommandText = "select * from som_orders where orderno='" + mOrderNo.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtOrders);

                    mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtOrders, "som_orders",
                    mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                    mCommand.ExecuteNonQuery();
                }
                #endregion

                #region som_orderitems
                
                #region deleted items

                DataTable mDtSavedItems = new DataTable("orderitems");
                mCommand.CommandText = "select * from som_orderitems where orderno='" + mOrderNo.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSavedItems);

                DataView mDvItems = new DataView();
                mDvItems.Table = mDtItems;
                mDvItems.Sort = "itemorderid";
                foreach (DataRow mDataRow in mDtSavedItems.Rows)
                {
                    string mItemOrderId = mDataRow["itemorderid"].ToString().Trim();
                    int mRowIndex = mDvItems.Find(mItemOrderId);
                    if (mRowIndex < 0)
                    {
                        mCommand.CommandText = "delete from som_orderitems where itemorderid='" + mItemOrderId + "'";
                        mRecsAffected = mCommand.ExecuteNonQuery();

                        #region audit som_orderitems
                        if (mRecsAffected > 0)
                        {
                            mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtSavedItems, mDataRow, "som_orderitems",
                            mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Deleted.ToString());
                            mCommand.ExecuteNonQuery();
                        }
                        #endregion
                    }
                }

                #endregion

                #region updated items

                foreach (DataRow mDataRow in mDtItems.Rows)
                {
                    string mItemOrderId = mDataRow["itemorderid"].ToString().Trim();
                    string mProductCode = mDataRow["productcode"].ToString().Trim();
                    string mPackagingCode = mDataRow["packagingcode"].ToString().Trim();
                    string mPackagingDescription = mDataRow["packagingdescription"].ToString().Trim();
                    int mPiecesInPackage = Convert.ToInt32(mDataRow["piecesinpackage"]);
                    double mOrderedQty = Convert.ToDouble(mDataRow["orderedqty"]);
                    double mTransPrice = Convert.ToDouble(mDataRow["transprice"]);

                    mCommand.CommandText = "update som_orderitems set packagingcode='" + mPackagingCode + "',packagingdescription='"
                    + mPackagingDescription + "',piecesinpackage=" + mPiecesInPackage + ",orderedqty=" + mOrderedQty + ",transprice="
                    + mTransPrice + ",userid='" + mUserId.Trim() + "' where itemorderid='" + mItemOrderId + "'";
                    mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit som_orderitems

                    if (mRecsAffected > 0)
                    {
                        clsGlobal.Write_Error(pClassName, mFunctionName, mCommand.CommandText);
                        clsGlobal.Write_Error(pClassName, mFunctionName, mRecsAffected.ToString());

                        DataTable mDtUpdatedItem = new DataTable("updateditems");
                        mCommand.CommandText = "select * from som_orderitems where itemorderid='" + mItemOrderId + "'";
                        mDataAdapter.SelectCommand = mCommand;
                        mDataAdapter.Fill(mDtUpdatedItem);

                        mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtUpdatedItem, "som_orderitems",
                        mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                        mCommand.ExecuteNonQuery();
                    }

                    #endregion
                }

                #endregion

                #region inserted items

                string mProductCodesList = "";
                foreach (DataRow mDataRow in mDtItems.Rows)
                {
                    if (mProductCodesList.Trim() == "")
                    {
                        mProductCodesList = "'" + mDataRow["productcode"].ToString().Trim() + "'";
                    }
                    else
                    {
                        mProductCodesList = mProductCodesList + ",'" + mDataRow["productcode"].ToString().Trim() + "'";
                    }
                }

                DataTable mDtProducts = new DataTable("products");
                if (mProductCodesList.Trim() != "")
                {
                    mCommand.CommandText = "select * from som_products where code in (" + mProductCodesList + ")";
                }
                else
                {
                    mCommand.CommandText = "select * from som_products where 1=2";
                }
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtProducts);
                DataView mDvProducts = new DataView();
                mDvProducts.Table = mDtProducts;
                mDvProducts.Sort = "code";

                DataView mDvSavedItems = new DataView();
                mDvSavedItems.Table = mDtSavedItems;
                mDvSavedItems.Sort = "itemorderid";

                int mRowCount = 1;
                foreach (DataRow mDataRow in mDtItems.Rows)
                {
                    string mItemOrderId = mDataRow["itemorderid"].ToString().Trim();

                    if (mDvSavedItems.Find(mItemOrderId) < 0)
                    {
                        string mProductCode = mDataRow["productcode"].ToString().Trim();
                        string mPackagingCode = mDataRow["packagingcode"].ToString().Trim();
                        string mPackagingDescription = mDataRow["packagingdescription"].ToString().Trim();
                        int mPiecesInPackage = Convert.ToInt32(mDataRow["piecesinpackage"]);
                        double mOrderedQty = Convert.ToDouble(mDataRow["orderedqty"]);
                        double mTransPrice = Convert.ToDouble(mDataRow["transprice"]);

                        string mExtraFields = clsGlobal.Build_FieldsForProductExtraInfos();
                        string mExtraFieldValues = clsGlobal.Build_FieldValuesForProductExtraInfos(mDvProducts, mProductCode);

                        mItemOrderId = clsGlobal.Generate_AutoId() + mRowCount;

                        //som_orderitems
                        mCommand.CommandText = "insert into som_orderitems(sysdate,transdate,orderno,itemorderid,productcode,packagingcode,"
                        + "packagingdescription,piecesinpackage,orderedqty,transprice,userid," + mExtraFields + ") values("
                        + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mOrderNo.Trim() 
                        + "','" + mItemOrderId  + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "',"
                        + mPiecesInPackage + "," + mOrderedQty + "," + mTransPrice + ",'" + mUserId.Trim() + "'," + mExtraFieldValues + ")";
                        mRecsAffected = mCommand.ExecuteNonQuery();

                        #region audit som_orderitems

                        if (mRecsAffected > 0)
                        {
                            string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_orderitems";
                            string mAuditingFields = clsGlobal.AuditingFields();
                            string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                            mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,orderno,itemorderid,productcode,packagingcode,"
                            + "packagingdescription,piecesinpackage,orderedqty,transprice,userid," + mExtraFields + "," + mAuditingFields
                            + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                            + mOrderNo.Trim() + "','" + mItemOrderId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "',"
                            + mPiecesInPackage + "," + mOrderedQty + "," + mTransPrice + ",'" + mUserId.Trim() + "',"
                            + mExtraFieldValues + "," + mAuditingValues + ")";
                            mCommand.ExecuteNonQuery();
                        }

                        #endregion

                        mRowCount++;
                    }
                }

                #endregion

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

        #region Receive_Items
        public AfyaPro_Types.clsResult Receive_Items(DateTime mTransDate, string mDeliveryNo,string mOrderNo, 
            string mStoreCode, string mStoreDescription, string mSupplierCode, string mSupplierDescription, 
            string mTitle, string mCurrencyCode, string mCurrencyDescription, double mExchangeRate, 
            string mCurrencySymbol, string mRemarks, DataTable mDtItems, string mUserId)
        {
            String mFunctionName = "Receive_Items";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            int mRecsAffected = -1;

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivorders_receive.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region check 4 order existance
            try
            {
                mCommand.CommandText = "select * from som_orders where orderno='" + mOrderNo.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IV_OrderNoDoesNotExist.ToString();
                    return mResult;
                }
                else
                {
                    if (mDataReader["orderstatus"].ToString().Trim().ToLower() ==
                        AfyaPro_Types.clsEnums.SomOrderStatus.Closed.ToString().ToLower())
                    {
                        mResult.Exe_Result = 0;
                        mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IV_ClosedOrderCannotbeReceived.ToString();
                        return mResult;
                    }
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

            #region validate deliveryno

            try
            {
                mCommand.CommandText = "select * from som_stockreceipts where deliveryno='" + mDeliveryNo.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    if (mDataReader["fromcode"].ToString().Trim().ToLower() != mSupplierCode.Trim().ToLower())
                    {
                        mResult.Exe_Result = 0;
                        mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IV_DeliveryNoIsInUseForAnotherSource.ToString();
                        return mResult;
                    }

                    if (mDataReader["orderno"].ToString().Trim().ToLower() != mOrderNo.Trim().ToLower())
                    {
                        mResult.Exe_Result = 0;
                        mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IV_DeliveryNoIsInUseForAnotherOrder.ToString();
                        return mResult;
                    }
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

            #region Receive
            try
            {
                mStoreCode = mStoreCode.Trim().ToLower();

                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                #region som_stockreceipts

                DataTable mDtReceipts = new DataTable("receipts");
                mCommand.CommandText = "select * from som_stockreceipts where deliveryno='" + mDeliveryNo.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtReceipts);

                if (mDtReceipts.Rows.Count == 0)
                {
                    mCommand.CommandText = "insert into som_stockreceipts(sysdate,transdate,deliveryno,orderno,"
                    + "transfertitle,tocode,todescription,fromcode,fromdescription,transfertype,currencycode,currencydescription,"
                    + "exchangerate,currencysymbol,remarks,transferstatus,userid) values(" + clsGlobal.Saving_DateValue(DateTime.Now)
                    + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mDeliveryNo.Trim() + "','" + mOrderNo.Trim() + "','"
                    + mTitle + "','" + mStoreCode.Trim() + "','" + mStoreDescription.Trim() + "','" + mSupplierCode.Trim() + "','" 
                    + mSupplierDescription.Trim() + "','" + AfyaPro_Types.clsEnums.SomTransferTypes.Normal.ToString() + "','" 
                    + mCurrencyCode.Trim() + "','" + mCurrencyDescription.Trim() + "'," + mExchangeRate + ",'" + mCurrencySymbol.Trim() 
                    + "','" + mRemarks.Trim() + "','" + AfyaPro_Types.clsEnums.SomTransferStatus.Open.ToString() + "','" 
                    + mUserId.Trim() + "')";
                    mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit som_stockreceipts

                    if (mRecsAffected > 0)
                    {
                        string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_stockreceipts";
                        string mAuditingFields = clsGlobal.AuditingFields();
                        string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                            AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                        mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,deliveryno,orderno,"
                        + "transfertitle,tocode,todescription,fromcode,fromdescription,transfertype,currencycode,currencydescription,"
                        + "exchangerate,currencysymbol,remarks,transferstatus,userid," + mAuditingFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now)
                        + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mDeliveryNo.Trim() + "','" + mOrderNo.Trim() + "','"
                        + mTitle + "','" + mStoreCode.Trim() + "','" + mStoreDescription.Trim() + "','" + mSupplierCode.Trim() + "','"
                        + mSupplierDescription.Trim() + "','" + AfyaPro_Types.clsEnums.SomTransferTypes.Normal.ToString() + "','"
                        + mCurrencyCode.Trim() + "','" + mCurrencyDescription.Trim() + "'," + mExchangeRate + ",'" + mCurrencySymbol.Trim()
                        + "','" + mRemarks.Trim() + "','" + AfyaPro_Types.clsEnums.SomTransferStatus.Open.ToString() + "','"
                        + mUserId.Trim() + "'," + mAuditingValues + ")";
                        mCommand.ExecuteNonQuery();
                    }

                    #endregion
                }
                else
                {
                    mCommand.CommandText = "update som_stockreceipts set transdate=" + clsGlobal.Saving_DateValue(mTransDate)
                    + ",transfertitle='" + mTitle.Trim() + "',tocode='" + mStoreCode.Trim()
                    + "',todescription='" + mStoreDescription.Trim() + "',fromcode='" + mSupplierCode.Trim()
                    + "',fromdescription='" + mSupplierDescription.Trim() + "',currencycode='" + mCurrencyCode.Trim()
                    + "',currencydescription='" + mCurrencyDescription.Trim() + "',exchangerate=" + mExchangeRate
                    + ",currencysymbol='" + mCurrencySymbol.Trim() + "',remarks='" + mRemarks.Trim()
                    + "',transfertype='" + AfyaPro_Types.clsEnums.SomTransferTypes.Normal.ToString() 
                    + "' where deliveryno='" + mDeliveryNo.Trim() + "'";
                    mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit som_stockreceipts
                    if (mRecsAffected > 0)
                    {
                        //get current details
                        DataTable mDtOrders = new DataTable("som_stockreceipts");
                        mCommand.CommandText = "select * from som_stockreceipts where deliveryno='" + mDeliveryNo.Trim() + "'";
                        mDataAdapter.SelectCommand = mCommand;
                        mDataAdapter.Fill(mDtOrders);

                        mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtOrders, "som_stockreceipts",
                        mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                        mCommand.ExecuteNonQuery();
                    }
                    #endregion
                }

                #endregion

                string mProductCodesList = "";
                foreach (DataRow mDataRow in mDtItems.Rows)
                {
                    if (mProductCodesList.Trim() == "")
                    {
                        mProductCodesList = "'" + mDataRow["productcode"].ToString().Trim() + "'";
                    }
                    else
                    {
                        mProductCodesList = mProductCodesList + ",'" + mDataRow["productcode"].ToString().Trim() + "'";
                    }
                }

                DataTable mDtProducts = new DataTable("products");
                if (mProductCodesList.Trim() != "")
                {
                    mCommand.CommandText = "select * from som_products where code in (" + mProductCodesList + ")";
                }
                else
                {
                    mCommand.CommandText = "select * from som_products where 1=2";
                }
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtProducts);
                DataView mDvProducts = new DataView();
                mDvProducts.Table = mDtProducts;
                mDvProducts.Sort = "code";

                int mRowCount = 1;

                foreach (DataRow mDataRow in mDtItems.Rows)
                {
                    string mItemOrderId = mDataRow["itemorderid"].ToString().Trim();
                    string mProductCode = mDataRow["productcode"].ToString().Trim();
                    string mPackagingCode = mDataRow["packagingcode"].ToString().Trim();
                    string mPackagingDescription = mDataRow["packagingdescription"].ToString().Trim();
                    double mPiecesInPackage = Convert.ToDouble(mDataRow["piecesinpackage"]);
                    string mExpiryDate = clsGlobal.Saving_DateValueNullable(mDataRow["expirydate"]);
                    double mReceivedQty = Convert.ToDouble(mDataRow["receivedqty"]);
                    double mTransPrice = Convert.ToDouble(mDataRow["transprice"]);

                    double mProductQty = mReceivedQty * mPiecesInPackage;
                    double mPrevCost = 0;
                    double mPreviousQty = 0;

                    if (mProductQty != 0)
                    {
                        string mExtraFields = clsGlobal.Build_FieldsForProductExtraInfos();
                        string mExtraFieldValues = clsGlobal.Build_FieldValuesForProductExtraInfos(mDvProducts, mProductCode);

                        #region get previous costing

                        DataTable mDtProductCost = new DataTable("products");
                        mCommand.CommandText = "select costprice from som_products where code='" + mProductCode.Trim() + "'";
                        mDataAdapter.SelectCommand = mCommand;
                        mDataAdapter.Fill(mDtProductCost);

                        if (mDtProductCost.Rows.Count > 0)
                        {
                            mPrevCost = Convert.ToDouble(mDtProductCost.Rows[0]["costprice"]);
                            clsSomStores mSomStores = new clsSomStores();
                            DataTable mDtControl = mSomStores.Get_OnHandQuantities(mCommand, "productcode='" + mProductCode.Trim() + "'", "");
                            if (mDtControl != null)
                            {
                                if (mDtControl.Rows.Count > 0)
                                {
                                    mPreviousQty = Convert.ToDouble(mDtControl.Rows[0]["onhandqty"]);
                                }
                            }
                        }

                        #endregion

                        #region add item to order if does not exist

                        if (mItemOrderId.Trim() == "")
                        {
                            mItemOrderId = clsGlobal.Generate_AutoId() + mRowCount;

                            //som_orderitems
                            mCommand.CommandText = "insert into som_orderitems(sysdate,transdate,orderno,itemorderid,productcode,packagingcode,"
                            + "packagingdescription,piecesinpackage,orderedqty,transprice,userid," + mExtraFields + ") values("
                            + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                            + mOrderNo.Trim() + "','" + mItemOrderId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "',"
                            + mPiecesInPackage + "," + mReceivedQty + "," + mTransPrice + ",'" + mUserId.Trim() + "'," + mExtraFieldValues + ")";
                            mRecsAffected = mCommand.ExecuteNonQuery();

                            #region audit som_orderitems

                            if (mRecsAffected > 0)
                            {
                                string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_orderitems";
                                string mAuditingFields = clsGlobal.AuditingFields();
                                string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                    AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                                mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,orderno,itemorderid,productcode,packagingcode,"
                                + "packagingdescription,piecesinpackage,orderedqty,transprice,userid," + mExtraFields + "," + mAuditingFields
                                + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                                + mOrderNo.Trim() + "','" + mItemOrderId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "',"
                                + mPiecesInPackage + "," + mReceivedQty + "," + mTransPrice + ",'" + mUserId.Trim() + "',"
                                + mExtraFieldValues + "," + mAuditingValues + ")";
                                mCommand.ExecuteNonQuery();
                            }

                            #endregion

                            mRowCount++;
                        }

                        #endregion

                        #region som_orderreceiveditems

                        //som_orderreceiveditems
                        mCommand.CommandText = "insert into som_orderreceiveditems(sysdate,transdate,orderno,deliveryno,itemorderid,"
                        + "productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,storecode,receivedqty,transprice,userid," 
                        + mExtraFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," 
                        + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mOrderNo.Trim() + "','" + mDeliveryNo.Trim() + "','"
                        + mItemOrderId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "'," 
                        + mPiecesInPackage + "," + mExpiryDate + ",'" + mStoreCode + "'," + mReceivedQty + "," + mTransPrice + ",'" + mUserId.Trim() 
                        + "'," + mExtraFieldValues + ")";
                        mRecsAffected = mCommand.ExecuteNonQuery();

                        #region audit som_orderreceiveditems

                        if (mRecsAffected > 0)
                        {
                            string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_orderreceiveditems";
                            string mAuditingFields = clsGlobal.AuditingFields();
                            string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                            mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,orderno,deliveryno,itemorderid,"
                            + "productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,storecode,receivedqty,transprice,userid,"
                            + mExtraFields + "," + mAuditingFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                            + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mOrderNo.Trim() + "','" + mDeliveryNo.Trim() + "','"
                            + mItemOrderId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "',"
                            + mPiecesInPackage + "," + mExpiryDate + ",'" + mStoreCode + "'," + mReceivedQty + "," + mTransPrice + ",'" + mUserId.Trim()
                            + "'," + mExtraFieldValues + "," + mAuditingValues + ")";
                            mCommand.ExecuteNonQuery();
                        }

                        #endregion

                        #endregion

                        #region som_stockreceiptitems

                        //som_stockreceiptitems
                        mCommand.CommandText = "insert into som_stockreceiptitems(sysdate,transdate,orderno,deliveryno,itemtransferid,"
                        + "productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,tostorecode,receivedqty,"
                        + "transprice,userid," + mExtraFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                        + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mOrderNo.Trim() + "','" + mDeliveryNo.Trim() + "','"
                        + mItemOrderId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "',"
                        + mPiecesInPackage + "," + mExpiryDate + ",'" + mStoreCode + "'," + mReceivedQty + "," + mTransPrice 
                        + ",'" + mUserId.Trim() + "'," + mExtraFieldValues + ")";
                        mRecsAffected = mCommand.ExecuteNonQuery();

                        #region audit som_stockreceiptitems

                        if (mRecsAffected > 0)
                        {
                            string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_stockreceiptitems";
                            string mAuditingFields = clsGlobal.AuditingFields();
                            string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                            mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,orderno,deliveryno,itemtransferid,"
                             + "productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,tostorecode,receivedqty,"
                             + "transprice,userid," + mExtraFields + "," + mAuditingFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                             + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mOrderNo.Trim() + "','" + mDeliveryNo.Trim() + "','"
                             + mItemOrderId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "',"
                             + mPiecesInPackage + "," + mExpiryDate + ",'" + mStoreCode + "'," + mReceivedQty + "," + mTransPrice
                             + ",'" + mUserId.Trim() + "'," + mExtraFieldValues + "," + mAuditingValues + ")";
                            mCommand.ExecuteNonQuery();
                        }

                        #endregion

                        #endregion

                        #region productexpirydates

                        if (mExpiryDate.Trim().ToLower() != "null")
                        {
                            mCommand.CommandText = "update som_productexpirydates set quantity=quantity+" + mProductQty + " where storecode='"
                            + mStoreCode.Trim() + "' and productcode='" + mProductCode + "' and expirydate=" + mExpiryDate;
                            mRecsAffected = mCommand.ExecuteNonQuery();
                            if (mRecsAffected == 0)
                            {
                                mCommand.CommandText = "insert into som_productexpirydates(storecode,productcode,expirydate,quantity) "
                                + "values('" + mStoreCode.Trim() + "','" + mProductCode + "'," + mExpiryDate + "," + mProductQty + ")";
                                mCommand.ExecuteNonQuery();
                            }
                        }

                        #endregion

                        #region productcontrol

                        //som_productcontrol
                        mCommand.CommandText = "update som_productcontrol set qty_" + mStoreCode + " = qty_" + mStoreCode + " + "
                        + mProductQty + " where productcode='" + mProductCode + "'";
                        mRecsAffected = mCommand.ExecuteNonQuery();
                        if (mRecsAffected == 0)
                        {
                            mCommand.CommandText = "insert into som_productcontrol(productcode,qty_" + mStoreCode + ") "
                            + "values('" + mProductCode + "'," + mProductQty + ")";
                            mRecsAffected = mCommand.ExecuteNonQuery();
                        }

                        #region audit som_productcontrol
                        if (mRecsAffected > 0)
                        {
                            //get current details
                            DataTable mDtProductControl = new DataTable("productcontrol");
                            mCommand.CommandText = "select * from som_productcontrol where productcode='" + mProductCode + "'";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(mDtProductControl);

                            mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtProductControl, "som_productcontrol",
                            mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                            mCommand.ExecuteNonQuery();
                        }
                        #endregion

                        #endregion

                        #region producttransactions

                        string mProductTransDescription = "Receive Order";

                        //som_producttransactions
                        mCommand.CommandText = "insert into som_producttransactions(sysdate,transdate,sourcecode,sourcedescription,"
                        + "productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,reference,transtype,"
                        + "transdescription,userid,qtyin_" + mStoreCode + ",transprice," + mExtraFields + ") values(" 
                        + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                        + mSupplierCode.Trim() + "','" + mSupplierDescription.Trim() + "','" + mProductCode + "','"
                        + mPackagingCode + "','" + mPackagingDescription + "'," + mPiecesInPackage + "," + mExpiryDate + ",'"
                        + mDeliveryNo + "','" + AfyaPro_Types.clsEnums.ProductTransTypes.Receive.ToString() + "','"
                        + mProductTransDescription + "','" + mUserId.Trim() + "'," + mProductQty + "," + mTransPrice + "," + mExtraFieldValues + ")";
                        mRecsAffected = mCommand.ExecuteNonQuery();

                        #region audit som_orderreceiveditems

                        if (mRecsAffected > 0)
                        {
                            string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_producttransactions";
                            string mAuditingFields = clsGlobal.AuditingFields();
                            string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                            mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,sourcecode,sourcedescription,"
                            + "productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,reference,transtype,"
                            + "transdescription,userid,qtyin_" + mStoreCode + ",transprice," + mExtraFields + "," + mAuditingFields + ") values("
                            + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                            + mSupplierCode.Trim() + "','" + mSupplierDescription.Trim() + "','" + mProductCode + "','"
                            + mPackagingCode + "','" + mPackagingDescription + "'," + mPiecesInPackage + "," + mExpiryDate + ",'"
                            + mDeliveryNo + "','" + AfyaPro_Types.clsEnums.ProductTransTypes.Receive.ToString() + "','"
                            + mProductTransDescription + "','" + mUserId.Trim() + "'," + mProductQty + "," + mTransPrice + "," + mExtraFieldValues
                            + "," + mAuditingValues + ")";
                            mCommand.ExecuteNonQuery();
                        }

                        #endregion

                        #endregion

                        #region costing

                        double mCurrentCost = mTransPrice / mPiecesInPackage;
                        double mCurrentQty = mProductQty;

                        if (mPrevCost != mCurrentCost)
                        {
                            double mAverageCost = ((mPrevCost * mPreviousQty) + (mCurrentCost * mCurrentQty)) / (mPreviousQty + mCurrentQty);

                            mCommand.CommandText = "update som_products set costprice=" + mCurrentCost + ",prevcost="
                            + mPrevCost + ",averagecost=" + mAverageCost + " where code='" + mProductCode.Trim() + "'";
                            mRecsAffected = mCommand.ExecuteNonQuery();

                            if (mRecsAffected > 0)
                            {
                                mCommand.CommandText = "insert into som_productcostslog(sysdate,transdate,productcode,costprice) values("
                                + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(DateTime.Now.Date) + ",'"
                                + mProductCode.Trim() + "'," + mCurrentCost + ")";
                                mCommand.ExecuteNonQuery();
                            }
                        }

                        #endregion
                    }
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

        #region Close
        public AfyaPro_Types.clsResult Close(DateTime mTransDate, string mOrderNo, string mUserId)
        {
            String mFunctionName = "Close";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            int mRecsAffected = -1;

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivorders_close.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region check 4 transfer existance
            try
            {
                mCommand.CommandText = "select * from som_orders where orderno='" + mOrderNo.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IV_TransferNoDoesNotExist.ToString();
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

            #region Close
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //som_orders
                mCommand.CommandText = "update som_orders set orderstatus='"
                + AfyaPro_Types.clsEnums.SomOrderStatus.Closed.ToString()
                + "' where orderno='" + mOrderNo.Trim() + "'";
                mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit som_orders
                if (mRecsAffected > 0)
                {
                    //get current details
                    DataTable mDtOrders = new DataTable("som_orders");
                    mCommand.CommandText = "select * from som_orders where orderno='" + mOrderNo.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtOrders);

                    mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtOrders, "som_orders",
                    mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
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
