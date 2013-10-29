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
    public class clsSomTransferIns : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsSomTransferIns";

        #endregion

        #region View_TransferIns
        public DataTable View_TransferIns(bool mDateSpecified, DateTime mDateFrom, DateTime mDateTo, String mExtraFilter, string mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_TransferIns";

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

                string mCommandText = "select * from som_transferins";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mExtraFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("som_transferins");
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

        #region View_TransferInItems
        public DataTable View_TransferInItems(string mFilter, string mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_TransferInItems";

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
                string mCommandText = "select * from som_transferinitems";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("som_transferinitems");
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
                    mDataRow["amount"] = Convert.ToDouble(mDataRow["transferedqty"]) * Convert.ToDouble(mDataRow["transprice"]);
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

        #region View_Receipts
        public DataTable View_Receipts(bool mDateSpecified, DateTime mDateFrom, DateTime mDateTo, String mExtraFilter, string mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_Receipts";

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

                string mCommandText = "select * from som_stockreceipts";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mExtraFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("som_stockreceipts");
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

        #region View_ReceiptItems
        public DataTable View_ReceiptItems(string mFilter, string mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_ReceiptItems";

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
                string mCommandText = "select * from som_stockreceiptitems";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("som_stockreceiptitems");
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
                string mCommandText = "select itemtransferid, sum(receivedqty * piecesinpackage) receivedqty from som_transferinreceiveditems";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                mCommandText = mCommandText + " group by itemtransferid";

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;


                DataTable mDataTable = new DataTable("som_transferinreceiveditems");
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
        public AfyaPro_Types.clsResult Add(Int16 mGenerateCode, DateTime mTransDate, string mTransferInNo,
            string mTitle, string mToCode, string mToDescription, string mFromCode, string mFromDescription,
            string mTransferType, string mCurrencyCode, string mCurrencyDescription, double mExchangeRate, 
            string mCurrencySymbol, string mRemarks, DataTable mDtItems, string mUserId)
        {
            String mFunctionName = "Add";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            OdbcTransaction mTrans = null;
            int mRecsAffected = -1;
            string mTransferOutNo = "";

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivtransferins_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region auto generate transferin no, if option is on

            if (mGenerateCode == 1)
            {
                clsAutoCodes objAutoCodes = new clsAutoCodes();
                AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                mObjCode = objAutoCodes.Next_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.transferinno), "som_transferins", "transferno");
                if (mObjCode.Exe_Result == -1)
                {
                    mResult.Exe_Result = mObjCode.Exe_Result;
                    mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                    return mResult;
                }
                mTransferInNo = mObjCode.GeneratedCode;
            }

            #endregion

            #region check 4 duplicate
            try
            {
                mCommand.CommandText = "select transferno from som_transferins where transferno='"
                + mTransferInNo.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IV_TransferNoIsInUse.ToString();
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

            #region auto generate transferout no

            if (mTransferType.Trim().ToLower() ==
                AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString().Trim().ToLower())            
            {
                clsAutoCodes objAutoCodes = new clsAutoCodes();
                AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                mObjCode = objAutoCodes.Next_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.transferoutno), "som_transferouts", "transferno");
                if (mObjCode.Exe_Result == -1)
                {
                    mResult.Exe_Result = mObjCode.Exe_Result;
                    mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                    return mResult;
                }
                mTransferOutNo = mObjCode.GeneratedCode;
            }

            #endregion

            #region add
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                #region som_transferins

                mCommand.CommandText = "insert into som_transferins(sysdate,transdate,transferno,transferoutno,transfertitle,tocode,todescription,"
                + "fromcode,fromdescription,transfertype,currencycode,currencydescription,exchangerate,currencysymbol,remarks,"
                + "transferstatus,userid) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate)
                + ",'" + mTransferInNo.Trim() + "','" + mTransferOutNo + "','" + mTitle.Trim() + "','" + mToCode.Trim() + "','" + mToDescription.Trim() + "','"
                + mFromCode.Trim() + "','" + mFromDescription.Trim() + "','" + mTransferType + "','" + mCurrencyCode.Trim() + "','"
                + mCurrencyDescription.Trim() + "'," + mExchangeRate + ",'" + mCurrencySymbol.Trim() + "','" + mRemarks.Trim() + "','"
                + AfyaPro_Types.clsEnums.SomTransferStatus.Open.ToString() + "','" + mUserId.Trim() + "')";
                mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit som_transferins

                if (mRecsAffected > 0)
                {
                    string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_transferins";
                    string mAuditingFields = clsGlobal.AuditingFields();
                    string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                        AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                    mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,transferno,transferoutno,transfertitle,tocode,todescription,"
                    + "fromcode,fromdescription,transfertype,currencycode,currencydescription,exchangerate,currencysymbol,remarks,"
                    + "transferstatus,userid," + mAuditingFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                    + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mTransferInNo.Trim() + "','" + mTransferOutNo + "','" + mTitle.Trim() + "','"
                    + mToCode.Trim() + "','" + mToDescription.Trim() + "','" + mFromCode.Trim() + "','" + mFromDescription.Trim() + "','"
                    + mTransferType + "','" + mCurrencyCode.Trim() + "','" + mCurrencyDescription.Trim() + "'," + mExchangeRate + ",'"
                    + mCurrencySymbol.Trim() + "','" + mRemarks.Trim() + "','" + AfyaPro_Types.clsEnums.SomTransferStatus.Open.ToString()
                    + "','" + mUserId.Trim() + "'," + mAuditingValues + ")";
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #endregion

                #region som_transferouts

                if (mTransferType.Trim().ToLower() ==
                    AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString().Trim().ToLower())
                {
                    mCommand.CommandText = "insert into som_transferouts(sysdate,transdate,transferno,transferinno,transfertitle,tocode,todescription,"
                    + "fromcode,fromdescription,transfertype,currencycode,currencydescription,exchangerate,currencysymbol,remarks,"
                    + "transferstatus,userid) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate)
                    + ",'" + mTransferOutNo.Trim() + "','" + mTransferInNo + "','" + mTitle.Trim() + "','" + mToCode.Trim() + "','" + mToDescription.Trim() + "','"
                    + mFromCode.Trim() + "','" + mFromDescription.Trim() + "','" + mTransferType + "','" + mCurrencyCode.Trim() + "','"
                    + mCurrencyDescription.Trim() + "'," + mExchangeRate + ",'" + mCurrencySymbol.Trim() + "','" + mRemarks.Trim() + "','"
                    + AfyaPro_Types.clsEnums.SomTransferStatus.Open.ToString() + "','" + mUserId.Trim() + "')";
                    mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit som_transferouts

                    if (mRecsAffected > 0)
                    {
                        string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_transferouts";
                        string mAuditingFields = clsGlobal.AuditingFields();
                        string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                            AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                        mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,transferno,transferinno,transfertitle,tocode,todescription,"
                        + "fromcode,fromdescription,transfertype,currencycode,currencydescription,exchangerate,currencysymbol,remarks,"
                        + "transferstatus,userid," + mAuditingFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                        + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mTransferOutNo.Trim() + "','" + mTransferInNo + "','" + mTitle.Trim() + "','"
                        + mToCode.Trim() + "','" + mToDescription.Trim() + "','" + mFromCode.Trim() + "','" + mFromDescription.Trim() + "','"
                        + mTransferType + "','" + mCurrencyCode.Trim() + "','" + mCurrencyDescription.Trim() + "'," + mExchangeRate + ",'"
                        + mCurrencySymbol.Trim() + "','" + mRemarks.Trim() + "','" + AfyaPro_Types.clsEnums.SomTransferStatus.Open.ToString()
                        + "','" + mUserId.Trim() + "'," + mAuditingValues + ")";
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
                    string mProductCode = mDataRow["productcode"].ToString().Trim();
                    string mPackagingCode = mDataRow["packagingcode"].ToString().Trim();
                    string mPackagingDescription = mDataRow["packagingdescription"].ToString().Trim();
                    int mPiecesInPackage = Convert.ToInt32(mDataRow["piecesinpackage"]);
                    double mTransferedQty = Convert.ToDouble(mDataRow["transferedqty"]);
                    double mTransPrice = Convert.ToDouble(mDataRow["transprice"]);

                    string mExtraFields = clsGlobal.Build_FieldsForProductExtraInfos();
                    string mExtraFieldValues = clsGlobal.Build_FieldValuesForProductExtraInfos(mDvProducts, mProductCode);

                    string mItemTransferId = clsGlobal.Generate_AutoId() + mRowCount;

                    #region som_transferinitems

                    mCommand.CommandText = "insert into som_transferinitems(sysdate,transdate,transferno,transferoutno,itemtransferid,productcode,"
                    + "packagingcode,packagingdescription,piecesinpackage,transferedqty,transprice,userid," + mExtraFields + ") values("
                    + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mTransferInNo.Trim()
                    + "','" + mTransferOutNo + "','" + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode + "','" 
                    + mPackagingDescription + "'," + mPiecesInPackage + "," + mTransferedQty + "," + mTransPrice + ",'" + mUserId.Trim() + "'," 
                    + mExtraFieldValues + ")";
                    mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit som_transferinitems

                    if (mRecsAffected > 0)
                    {
                        string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_transferinitems";
                        string mAuditingFields = clsGlobal.AuditingFields();
                        string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                            AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                        mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,transferno,transferoutno,itemtransferid,"
                        + "productcode,packagingcode,packagingdescription,piecesinpackage,transferedqty,transprice,userid," + mExtraFields + ","
                        + mAuditingFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate)
                        + ",'" + mTransferInNo.Trim() + "','" + mTransferOutNo + "','" + mItemTransferId + "','" + mProductCode + "','"
                        + mPackagingCode + "','" + mPackagingDescription + "'," + mPiecesInPackage + "," + mTransferedQty + "," + mTransPrice
                        + ",'" + mUserId.Trim() + "'," + mExtraFieldValues + "," + mAuditingValues + ")";
                        mCommand.ExecuteNonQuery();
                    }

                    #endregion

                    #endregion

                    #region som_transferoutitems

                    mCommand.CommandText = "insert into som_transferoutitems(sysdate,transdate,transferno,transferinno,itemtransferid,productcode,"
                    + "packagingcode,packagingdescription,piecesinpackage,transferedqty,transprice,userid," + mExtraFields + ") values("
                    + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mTransferOutNo.Trim()
                    + "','" + mTransferInNo + "','" + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription 
                    + "'," + mPiecesInPackage + "," + mTransferedQty + "," + mTransPrice + ",'" + mUserId.Trim() + "'," + mExtraFieldValues + ")";
                    mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit som_transferoutitems

                    if (mRecsAffected > 0)
                    {
                        string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_transferoutitems";
                        string mAuditingFields = clsGlobal.AuditingFields();
                        string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                            AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                        mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,transferno,transferinno,itemtransferid,"
                        + "productcode,packagingcode,packagingdescription,piecesinpackage,transferedqty,transprice,userid," + mExtraFields + ","
                        + mAuditingFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate)
                        + ",'" + mTransferOutNo.Trim() + "','" + mTransferInNo + "','" + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode + "','"
                        + mPackagingDescription + "'," + mPiecesInPackage + "," + mTransferedQty + "," + mTransPrice + ",'" + mUserId.Trim() + "',"
                        + mExtraFieldValues + "," + mAuditingValues + ")";
                        mCommand.ExecuteNonQuery();
                    }

                    #endregion

                    #endregion

                    mRowCount++;
                }

                if (mGenerateCode == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.transferinno);
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
        public AfyaPro_Types.clsResult Edit(DateTime mTransDate, string mTransferInNo, string mTransferOutNo,
            string mTitle, string mToCode, string mToDescription, string mFromCode, string mFromDescription,
            string mTransferType, string mCurrencyCode, string mCurrencyDescription, double mExchangeRate, 
            string mCurrencySymbol, string mRemarks, DataTable mDtItems, string mUserId)
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivtransferins_edit.ToString(), mUserId);
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
                mCommand.CommandText = "select * from som_transferins where transferno='" + mTransferInNo.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IV_TransferNoDoesNotExist.ToString();
                    return mResult;
                }
                else
                {
                    if (mDataReader["transferstatus"].ToString().Trim().ToLower() ==
                        AfyaPro_Types.clsEnums.SomTransferStatus.Closed.ToString().ToLower())
                    {
                        mResult.Exe_Result = 0;
                        mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IV_ClosedTransferCannotbeEdited.ToString();
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

                #region som_transferins

                mCommand.CommandText = "update som_transferins set transdate=" + clsGlobal.Saving_DateValue(mTransDate)
                + ",transfertitle='" + mTitle.Trim() + "',tocode='" + mToCode.Trim()
                + "',todescription='" + mToDescription.Trim() + "',fromcode='" + mFromCode.Trim()
                + "',fromdescription='" + mFromDescription.Trim() + "',currencycode='" + mCurrencyCode.Trim()
                + "',currencydescription='" + mCurrencyDescription.Trim() + "',exchangerate=" + mExchangeRate
                + ",currencysymbol='" + mCurrencySymbol.Trim() + "',remarks='" + mRemarks.Trim()
                + "',transfertype='" + mTransferType + "' where transferno='" + mTransferInNo.Trim() + "'";
                mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit som_transferins
                if (mRecsAffected > 0)
                {
                    //get current details
                    DataTable mDtOrders = new DataTable("transferins");
                    mCommand.CommandText = "select * from som_transferins where transferno='" + mTransferInNo.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtOrders);

                    mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtOrders, "som_transferins",
                    mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                    mCommand.ExecuteNonQuery();
                }
                #endregion

                #endregion

                #region som_transferouts

                if (mTransferType.Trim().ToLower() ==
                    AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString().Trim().ToLower())
                {
                    mCommand.CommandText = "update som_transferouts set transdate=" + clsGlobal.Saving_DateValue(mTransDate)
                    + ",transfertitle='" + mTitle.Trim() + "',tocode='" + mToCode.Trim()
                    + "',todescription='" + mToDescription.Trim() + "',fromcode='" + mFromCode.Trim()
                    + "',fromdescription='" + mFromDescription.Trim() + "',currencycode='" + mCurrencyCode.Trim()
                    + "',currencydescription='" + mCurrencyDescription.Trim() + "',exchangerate=" + mExchangeRate
                    + ",currencysymbol='" + mCurrencySymbol.Trim() + "',remarks='" + mRemarks.Trim()
                    + "',transfertype='" + mTransferType + "' where transferno='" + mTransferOutNo.Trim() + "'";
                    mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit som_transferouts
                    if (mRecsAffected > 0)
                    {
                        //get current details
                        DataTable mDtOrders = new DataTable("transferouts");
                        mCommand.CommandText = "select * from som_transferouts where transferno='" + mTransferOutNo.Trim() + "'";
                        mDataAdapter.SelectCommand = mCommand;
                        mDataAdapter.Fill(mDtOrders);

                        mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtOrders, "som_transferouts",
                        mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                        mCommand.ExecuteNonQuery();
                    }
                    #endregion
                }

                #endregion

                #region som_transferinitems

                #region deleted items

                #region som_transferinitems

                DataTable mDtSavedInItems = new DataTable("transferinitems");
                mCommand.CommandText = "select * from som_transferinitems where transferno='" + mTransferInNo.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSavedInItems);

                DataView mDvInItems = new DataView();
                mDvInItems.Table = mDtItems;
                mDvInItems.Sort = "itemtransferid";
                foreach (DataRow mDataRow in mDtSavedInItems.Rows)
                {
                    string mItemTransferId = mDataRow["itemtransferid"].ToString().Trim();
                    int mRowIndex = mDvInItems.Find(mItemTransferId);
                    if (mRowIndex < 0)
                    {
                        mCommand.CommandText = "delete from som_transferinitems where itemtransferid='" + mItemTransferId + "'";
                        mRecsAffected = mCommand.ExecuteNonQuery();

                        #region audit som_transferinitems
                        if (mRecsAffected > 0)
                        {
                            mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtSavedInItems, mDataRow, "som_transferinitems",
                            mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Deleted.ToString());
                            mCommand.ExecuteNonQuery();
                        }
                        #endregion
                    }
                }

                #endregion

                #region som_transferoutitems

                if (mTransferType.Trim().ToLower() ==
                    AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString().Trim().ToLower())
                {
                    DataTable mDtSavedOutItems = new DataTable("som_transferoutitems");
                    mCommand.CommandText = "select * from som_transferoutitems where transferno='" + mTransferOutNo.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtSavedOutItems);

                    DataView mDvOutItems = new DataView();
                    mDvOutItems.Table = mDtItems;
                    mDvOutItems.Sort = "itemtransferid";
                    foreach (DataRow mDataRow in mDtSavedOutItems.Rows)
                    {
                        string mItemTransferId = mDataRow["itemtransferid"].ToString().Trim();
                        int mRowIndex = mDvOutItems.Find(mItemTransferId);
                        if (mRowIndex < 0)
                        {
                            mCommand.CommandText = "delete from som_transferoutitems where itemtransferid='" + mItemTransferId + "'";
                            mRecsAffected = mCommand.ExecuteNonQuery();

                            #region audit som_transferoutitems
                            if (mRecsAffected > 0)
                            {
                                mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtSavedOutItems, mDataRow, "som_transferoutitems",
                                mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Deleted.ToString());
                                mCommand.ExecuteNonQuery();
                            }
                            #endregion
                        }
                    }
                }

                #endregion

                #endregion

                #region updated items

                foreach (DataRow mDataRow in mDtItems.Rows)
                {
                    string mItemTransferId = mDataRow["itemtransferid"].ToString().Trim();
                    string mProductCode = mDataRow["productcode"].ToString().Trim();
                    string mPackagingCode = mDataRow["packagingcode"].ToString().Trim();
                    string mPackagingDescription = mDataRow["packagingdescription"].ToString().Trim();
                    int mPiecesInPackage = Convert.ToInt32(mDataRow["piecesinpackage"]);
                    double mTransferedQty = Convert.ToDouble(mDataRow["transferedqty"]);
                    double mTransPrice = Convert.ToDouble(mDataRow["transprice"]);

                    mCommand.CommandText = "update som_transferinitems set packagingcode='" + mPackagingCode + "',packagingdescription='"
                    + mPackagingDescription + "',piecesinpackage=" + mPiecesInPackage + ",transferedqty=" + mTransferedQty + ",transprice="
                    + mTransPrice + ",userid='" + mUserId.Trim() + "' where itemtransferid='" + mItemTransferId + "'";
                    mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit som_transferinitems

                    if (mRecsAffected > 0)
                    {
                        DataTable mDtUpdatedItem = new DataTable("updateditems");
                        mCommand.CommandText = "select * from som_transferinitems where itemtransferid='" + mItemTransferId + "'";
                        mDataAdapter.SelectCommand = mCommand;
                        mDataAdapter.Fill(mDtUpdatedItem);

                        mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtUpdatedItem, "som_transferinitems",
                        mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                        mCommand.ExecuteNonQuery();
                    }

                    #endregion

                    if (mTransferType.Trim().ToLower() ==
                        AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString().Trim().ToLower())
                    {
                        mCommand.CommandText = "update som_transferoutitems set packagingcode='" + mPackagingCode + "',packagingdescription='"
                        + mPackagingDescription + "',piecesinpackage=" + mPiecesInPackage + ",transferedqty=" + mTransferedQty + ",transprice="
                        + mTransPrice + ",userid='" + mUserId.Trim() + "' where itemtransferid='" + mItemTransferId + "'";
                        mRecsAffected = mCommand.ExecuteNonQuery();

                        #region audit som_transferoutitems

                        if (mRecsAffected > 0)
                        {
                            DataTable mDtUpdatedItem = new DataTable("updateditems");
                            mCommand.CommandText = "select * from som_transferoutitems where itemtransferid='" + mItemTransferId + "'";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(mDtUpdatedItem);

                            mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtUpdatedItem, "som_transferoutitems",
                            mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                            mCommand.ExecuteNonQuery();
                        }

                        #endregion
                    }
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
                mDvSavedItems.Table = mDtSavedInItems;
                mDvSavedItems.Sort = "itemtransferid";

                int mRowCount = 1;
                foreach (DataRow mDataRow in mDtItems.Rows)
                {
                    string mItemTransferId = mDataRow["itemtransferid"].ToString().Trim();

                    if (mDvSavedItems.Find(mItemTransferId) < 0)
                    {
                        string mProductCode = mDataRow["productcode"].ToString().Trim();
                        string mPackagingCode = mDataRow["packagingcode"].ToString().Trim();
                        string mPackagingDescription = mDataRow["packagingdescription"].ToString().Trim();
                        int mPiecesInPackage = Convert.ToInt32(mDataRow["piecesinpackage"]);
                        double mTransferedQty = Convert.ToDouble(mDataRow["transferedqty"]);
                        double mTransPrice = Convert.ToDouble(mDataRow["transprice"]);

                        string mExtraFields = clsGlobal.Build_FieldsForProductExtraInfos();
                        string mExtraFieldValues = clsGlobal.Build_FieldValuesForProductExtraInfos(mDvProducts, mProductCode);

                        mItemTransferId = clsGlobal.Generate_AutoId() + mRowCount;

                        //som_transferinitems
                        mCommand.CommandText = "insert into som_transferinitems(sysdate,transdate,transferno,transferoutno,itemtransferid,"
                        + "productcode,packagingcode,packagingdescription,piecesinpackage,transferedqty,transprice,userid," + mExtraFields
                        + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                        + mTransferInNo.Trim() + "','" + mTransferOutNo + "','" + mItemTransferId + "','" + mProductCode + "','"
                        + mPackagingCode + "','" + mPackagingDescription + "'," + mPiecesInPackage + "," + mTransferedQty + "," + mTransPrice
                        + ",'" + mUserId.Trim() + "'," + mExtraFieldValues + ")";
                        mRecsAffected = mCommand.ExecuteNonQuery();

                        #region audit som_transferinitems

                        if (mRecsAffected > 0)
                        {
                            string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_transferinitems";
                            string mAuditingFields = clsGlobal.AuditingFields();
                            string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                            mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,transferno,transferoutno,itemtransferid,"
                            + "productcode,packagingcode,packagingdescription,piecesinpackage,transferedqty,transprice,userid," + mExtraFields + ","
                            + mAuditingFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate)
                            + ",'" + mTransferInNo.Trim() + "','" + mTransferOutNo + "','" + mItemTransferId + "','" + mProductCode + "','"
                            + mPackagingCode + "','" + mPackagingDescription + "'," + mPiecesInPackage + "," + mTransferedQty + "," + mTransPrice
                            + ",'" + mUserId.Trim() + "'," + mExtraFieldValues + "," + mAuditingValues + ")";
                            mCommand.ExecuteNonQuery();
                        }

                        #endregion

                        if (mTransferType.Trim().ToLower() ==
                            AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString().Trim().ToLower())
                        {
                            //som_transferoutitems
                            mCommand.CommandText = "insert into som_transferoutitems(sysdate,transdate,transferno,transferinno,itemtransferid,"
                            + "productcode,packagingcode,packagingdescription,piecesinpackage,transferedqty,transprice,userid," + mExtraFields 
                            + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'" 
                            + mTransferOutNo.Trim() + "','" + mTransferInNo + "','" + mItemTransferId + "','" + mProductCode + "','" 
                            + mPackagingCode + "','" + mPackagingDescription + "'," + mPiecesInPackage + "," + mTransferedQty + "," 
                            + mTransPrice + ",'" + mUserId.Trim() + "'," + mExtraFieldValues + ")";
                            mRecsAffected = mCommand.ExecuteNonQuery();

                            #region audit som_transferoutitems

                            if (mRecsAffected > 0)
                            {
                                string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_transferoutitems";
                                string mAuditingFields = clsGlobal.AuditingFields();
                                string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                    AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                                mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,transferno,transferinno,itemtransferid,"
                                + "productcode,packagingcode,packagingdescription,piecesinpackage,transferedqty,transprice,userid," 
                                + mExtraFields + "," + mAuditingFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," 
                                + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mTransferOutNo.Trim() + "','" + mTransferInNo + "','" 
                                + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "'," 
                                + mPiecesInPackage + "," + mTransferedQty + "," + mTransPrice + ",'" + mUserId.Trim() + "'," 
                                + mExtraFieldValues + "," + mAuditingValues + ")";
                                mCommand.ExecuteNonQuery();
                            }

                            #endregion
                        }

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
        public AfyaPro_Types.clsResult Receive_Items(DateTime mTransDate, string mDeliveryNo, string mTransferInNo, string mTransferOutNo, 
            string mTitle, string mToCode, string mToDescription, string mFromCode, string mFromDescription, 
            string mTransferType, string mCurrencyCode, string mCurrencyDescription, double mExchangeRate, 
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivtransferins_receive.ToString(), mUserId);
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
                mCommand.CommandText = "select * from som_transferins where transferno='" + mTransferInNo.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IV_TransferNoDoesNotExist.ToString();
                    return mResult;
                }
                else
                {
                    if (mDataReader["transferstatus"].ToString().Trim().ToLower() ==
                        AfyaPro_Types.clsEnums.SomTransferStatus.Closed.ToString().ToLower())
                    {
                        mResult.Exe_Result = 0;
                        mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IV_ClosedTransferCannotbeReceived.ToString();
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
                    if (mDataReader["fromcode"].ToString().Trim().ToLower() != mFromCode.Trim().ToLower())
                    {
                        mResult.Exe_Result = 0;
                        mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IV_DeliveryNoIsInUseForAnotherSource.ToString();
                        return mResult;
                    }

                    if (mDataReader["transferno"].ToString().Trim().ToLower() != mTransferInNo.Trim().ToLower())
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
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

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

                string mFromStoreCode = "";
                string mToStoreCode = mToCode.Trim();

                if (mTransferType.Trim().ToLower() ==
                    AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString().Trim().ToLower())
                {
                    mFromStoreCode = mFromCode.Trim();
                }

                #region som_stockreceipts

                DataTable mDtReceipts = new DataTable("receipts");
                mCommand.CommandText = "select * from som_stockreceipts where deliveryno='" + mDeliveryNo.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtReceipts);

                if (mDtReceipts.Rows.Count == 0)
                {
                    mCommand.CommandText = "insert into som_stockreceipts(sysdate,transdate,deliveryno,transferno,transferoutno,"
                    + "transfertitle,tocode,todescription,fromcode,fromdescription,transfertype,currencycode,currencydescription,"
                    + "exchangerate,currencysymbol,remarks,transferstatus,userid) values(" + clsGlobal.Saving_DateValue(DateTime.Now)
                    + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mDeliveryNo.Trim() + "','" + mTransferInNo.Trim() + "','"
                    + mTransferOutNo + "','" + mTitle.Trim() + "','" + mToCode.Trim() + "','" + mToDescription.Trim() + "','"
                    + mFromCode.Trim() + "','" + mFromDescription.Trim() + "','" + mTransferType + "','" + mCurrencyCode.Trim() + "','"
                    + mCurrencyDescription.Trim() + "'," + mExchangeRate + ",'" + mCurrencySymbol.Trim() + "','" + mRemarks.Trim() + "','"
                    + AfyaPro_Types.clsEnums.SomTransferStatus.Open.ToString() + "','" + mUserId.Trim() + "')";
                    mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit som_stockreceipts

                    if (mRecsAffected > 0)
                    {
                        string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_stockreceipts";
                        string mAuditingFields = clsGlobal.AuditingFields();
                        string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                            AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                        mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,deliveryno,transferno,transferoutno,"
                        + "transfertitle,tocode,todescription,fromcode,fromdescription,transfertype,currencycode,currencydescription,"
                        + "exchangerate,currencysymbol,remarks,transferstatus,userid," + mAuditingFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now)
                        + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mDeliveryNo.Trim() + "','" + mTransferInNo.Trim() + "','"
                        + mTransferOutNo + "','" + mTitle.Trim() + "','" + mToCode.Trim() + "','" + mToDescription.Trim() + "','"
                        + mFromCode.Trim() + "','" + mFromDescription.Trim() + "','" + mTransferType + "','" + mCurrencyCode.Trim() + "','"
                        + mCurrencyDescription.Trim() + "'," + mExchangeRate + ",'" + mCurrencySymbol.Trim() + "','" + mRemarks.Trim() + "','"
                        + AfyaPro_Types.clsEnums.SomTransferStatus.Open.ToString() + "','" + mUserId.Trim() + "'," + mAuditingValues + ")";
                        mCommand.ExecuteNonQuery();
                    }

                    #endregion
                }
                else
                {
                    mCommand.CommandText = "update som_stockreceipts set transdate=" + clsGlobal.Saving_DateValue(mTransDate)
                    + ",transfertitle='" + mTitle.Trim() + "',tocode='" + mToCode.Trim()
                    + "',todescription='" + mToDescription.Trim() + "',fromcode='" + mFromCode.Trim()
                    + "',fromdescription='" + mFromDescription.Trim() + "',currencycode='" + mCurrencyCode.Trim()
                    + "',currencydescription='" + mCurrencyDescription.Trim() + "',exchangerate=" + mExchangeRate
                    + ",currencysymbol='" + mCurrencySymbol.Trim() + "',remarks='" + mRemarks.Trim()
                    + "',transfertype='" + mTransferType + "' where deliveryno='" + mDeliveryNo.Trim() + "'";
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

                foreach (DataRow mDataRow in mDtItems.Rows)
                {
                    string mItemTransferId = mDataRow["itemtransferid"].ToString().Trim();
                    string mProductCode = mDataRow["productcode"].ToString().Trim();
                    string mPackagingCode = mDataRow["packagingcode"].ToString().Trim();
                    string mPackagingDescription = mDataRow["packagingdescription"].ToString().Trim();
                    int mPiecesInPackage = Convert.ToInt32(mDataRow["piecesinpackage"]);
                    string mExpiryDate = clsGlobal.Saving_DateValueNullable(mDataRow["expirydate"]);
                    double mReceivedQty = Convert.ToDouble(mDataRow["receivedqty"]);
                    double mTransPrice = Convert.ToDouble(mDataRow["transprice"]);

                    double mProductQty = mReceivedQty * mPiecesInPackage;

                    if (mProductQty != 0)
                    {
                        string mExtraFields = clsGlobal.Build_FieldsForProductExtraInfos();
                        string mExtraFieldValues = clsGlobal.Build_FieldValuesForProductExtraInfos(mDvProducts, mProductCode);

                        #region add item to order if does not exist

                        if (mItemTransferId.Trim() == "")
                        {
                            mItemTransferId = clsGlobal.Generate_AutoId() + mRowCount;

                            //som_transferinitems
                            mCommand.CommandText = "insert into som_transferinitems(sysdate,transdate,transferno,transferoutno,itemtransferid,productcode,packagingcode,"
                            + "packagingdescription,piecesinpackage,transferedqty,transprice,userid," + mExtraFields + ") values("
                            + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                            + mTransferInNo.Trim() + "','" + mTransferOutNo.Trim() + "','" + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "',"
                            + mPiecesInPackage + "," + mReceivedQty + "," + mTransPrice + ",'" + mUserId.Trim() + "'," + mExtraFieldValues + ")";
                            mRecsAffected = mCommand.ExecuteNonQuery();

                            #region audit som_transferinitems

                            if (mRecsAffected > 0)
                            {
                                string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_transferinitems";
                                string mAuditingFields = clsGlobal.AuditingFields();
                                string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                    AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                                mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,transferno,transferoutno,itemtransferid,productcode,packagingcode,"
                                + "packagingdescription,piecesinpackage,transferedqty,transprice,userid," + mExtraFields + "," + mAuditingFields
                                + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                                + mTransferInNo.Trim() + "','" + mTransferOutNo.Trim() + "','" + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "',"
                                + mPiecesInPackage + "," + mReceivedQty + "," + mTransPrice + ",'" + mUserId.Trim() + "',"
                                + mExtraFieldValues + "," + mAuditingValues + ")";
                                mCommand.ExecuteNonQuery();
                            }

                            #endregion

                            mRowCount++;
                        }

                        #endregion

                        #region som_transferinreceiveditems

                        //som_transferinreceiveditems
                        mCommand.CommandText = "insert into som_transferinreceiveditems(sysdate,transdate,transferno,transferoutno,deliveryno,itemtransferid,"
                        + "productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,fromstorecode,tostorecode,receivedqty,"
                        + "transprice,userid," + mExtraFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                        + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mTransferInNo.Trim() + "','" + mTransferOutNo.Trim() + "','" + mDeliveryNo.Trim() + "','"
                        + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "',"
                        + mPiecesInPackage + "," + mExpiryDate + ",'" + mFromStoreCode + "','" + mToStoreCode + "'," + mReceivedQty + ","
                        + mTransPrice + ",'" + mUserId.Trim() + "'," + mExtraFieldValues + ")";
                        mRecsAffected = mCommand.ExecuteNonQuery();

                        #region audit som_transferinreceiveditems

                        if (mRecsAffected > 0)
                        {
                            string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_transferinreceiveditems";
                            string mAuditingFields = clsGlobal.AuditingFields();
                            string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                            mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,transferno,transferoutno,deliveryno,itemtransferid,"
                            + "productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,fromstorecode,tostorecode,receivedqty,"
                            + "transprice,userid," + mExtraFields + "," + mAuditingFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                            + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mTransferInNo.Trim() + "','" + mTransferOutNo.Trim() + "','" + mDeliveryNo.Trim() + "','"
                            + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "',"
                            + mPiecesInPackage + "," + mExpiryDate + ",'" + mFromStoreCode + "','" + mToStoreCode + "'," + mReceivedQty + ","
                            + mTransPrice + ",'" + mUserId.Trim() + "'," + mExtraFieldValues + "," + mAuditingValues + ")";
                            mCommand.ExecuteNonQuery();
                        }

                        #endregion

                        #endregion

                        #region som_stockreceiptitems

                        //som_stockreceiptitems
                        mCommand.CommandText = "insert into som_stockreceiptitems(sysdate,transdate,transferno,transferoutno,deliveryno,itemtransferid,"
                        + "productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,fromstorecode,tostorecode,receivedqty,"
                        + "transprice,userid," + mExtraFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                        + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mTransferInNo.Trim() + "','" + mTransferOutNo.Trim() + "','" + mDeliveryNo.Trim() + "','"
                        + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "',"
                        + mPiecesInPackage + "," + mExpiryDate + ",'" + mFromStoreCode + "','" + mToStoreCode + "'," + mReceivedQty + ","
                        + mTransPrice + ",'" + mUserId.Trim() + "'," + mExtraFieldValues + ")";
                        mRecsAffected = mCommand.ExecuteNonQuery();

                        #region audit som_stockreceiptitems

                        if (mRecsAffected > 0)
                        {
                            string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_stockreceiptitems";
                            string mAuditingFields = clsGlobal.AuditingFields();
                            string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                            mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,transferno,transferoutno,deliveryno,itemtransferid,"
                            + "productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,fromstorecode,tostorecode,receivedqty,"
                            + "transprice,userid," + mExtraFields + "," + mAuditingFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                            + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mTransferInNo.Trim() + "','" + mTransferOutNo.Trim() + "','" + mDeliveryNo.Trim() + "','"
                            + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "',"
                            + mPiecesInPackage + "," + mExpiryDate + ",'" + mFromStoreCode + "','" + mToStoreCode + "'," + mReceivedQty + ","
                            + mTransPrice + ",'" + mUserId.Trim() + "'," + mExtraFieldValues + "," + mAuditingValues + ")";
                            mCommand.ExecuteNonQuery();
                        }

                        #endregion

                        #endregion

                        #region productexpirydates

                        if (mExpiryDate.Trim().ToLower() != "null")
                        {
                            mCommand.CommandText = "update som_productexpirydates set quantity=quantity+" + mProductQty + " where storecode='"
                            + mToStoreCode.Trim() + "' and productcode='" + mProductCode + "' and expirydate=" + mExpiryDate;
                            mRecsAffected = mCommand.ExecuteNonQuery();
                            if (mRecsAffected == 0)
                            {
                                mCommand.CommandText = "insert into som_productexpirydates(storecode,productcode,expirydate,quantity) "
                                + "values('" + mToStoreCode.Trim() + "','" + mProductCode + "'," + mExpiryDate + "," + mProductQty + ")";
                                mCommand.ExecuteNonQuery();
                            }
                        }

                        #endregion

                        #region productcontrol

                        //som_productcontrol
                        mCommand.CommandText = "update som_productcontrol set qty_" + mToStoreCode + " = qty_" + mToStoreCode + " + "
                        + mProductQty + " where productcode='" + mProductCode + "'";
                        mRecsAffected = mCommand.ExecuteNonQuery();
                        if (mRecsAffected == 0)
                        {
                            mCommand.CommandText = "insert into som_productcontrol(productcode,qty_" + mToStoreCode + ") "
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
                        + "transdescription,userid,qtyin_" + mToStoreCode + ",transprice," + mExtraFields + ") values("
                        + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                        + mFromCode.Trim() + "','" + mFromDescription.Trim() + "','" + mProductCode + "','"
                        + mPackagingCode + "','" + mPackagingDescription + "'," + mPiecesInPackage + "," + mExpiryDate + ",'"
                        + mDeliveryNo + "','" + AfyaPro_Types.clsEnums.ProductTransTypes.Receive.ToString() + "','"
                        + mProductTransDescription + "','" + mUserId.Trim() + "'," + mProductQty + "," + mTransPrice + "," + mExtraFieldValues + ")";
                        mRecsAffected = mCommand.ExecuteNonQuery();

                        #region audit som_transferinreceiveditems

                        if (mRecsAffected > 0)
                        {
                            string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_producttransactions";
                            string mAuditingFields = clsGlobal.AuditingFields();
                            string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                            mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,sourcecode,sourcedescription,"
                            + "productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,reference,transtype,"
                            + "transdescription,userid,qtyin_" + mToStoreCode + ",transprice," + mExtraFields + "," + mAuditingFields + ") values("
                            + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                            + mFromCode.Trim() + "','" + mFromDescription.Trim() + "','" + mProductCode + "','"
                            + mPackagingCode + "','" + mPackagingDescription + "'," + mPiecesInPackage + "," + mExpiryDate + ",'"
                            + mDeliveryNo + "','" + AfyaPro_Types.clsEnums.ProductTransTypes.Receive.ToString() + "','"
                            + mProductTransDescription + "','" + mUserId.Trim() + "'," + mProductQty + "," + mTransPrice + "," + mExtraFieldValues
                            + "," + mAuditingValues + ")";
                            mCommand.ExecuteNonQuery();
                        }

                        #endregion

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
        public AfyaPro_Types.clsResult Close(DateTime mTransDate, string mTransferInNo, string mTransferOutNo, string mUserId)
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivtransferins_close.ToString(), mUserId);
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
                mCommand.CommandText = "select * from som_transferins where transferno='" + mTransferInNo.Trim() + "'";
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

                #region som_transferins

                mCommand.CommandText = "update som_transferins set transferstatus='"
                + AfyaPro_Types.clsEnums.SomTransferStatus.Closed.ToString()
                + "' where transferno='" + mTransferInNo.Trim() + "'";
                mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit som_transferins
                if (mRecsAffected > 0)
                {
                    //get current details
                    DataTable mDtOrders = new DataTable("transferins");
                    mCommand.CommandText = "select * from som_transferins where transferno='" + mTransferInNo.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtOrders);

                    mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtOrders, "som_transferins",
                    mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                    mCommand.ExecuteNonQuery();
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
    }
}
