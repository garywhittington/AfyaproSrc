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
    public class clsSomTransferOuts : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsSomTransferOuts";

        #endregion

        #region View_TransferOuts
        public DataTable View_TransferOuts(bool mDateSpecified, DateTime mDateFrom, DateTime mDateTo, String mExtraFilter, string mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_TransferOuts";

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

                string mCommandText = "select * from som_transferouts";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mExtraFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("som_transferouts");
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

        #region View_TransferOutItems
        public DataTable View_TransferOutItems(string mFilter, string mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_TransferOutItems";

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
                string mCommandText = "select * from som_transferoutitems";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("som_transferoutitems");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                mDataTable.Columns.Add("onhandqty", typeof(System.Double));
                mDataTable.Columns.Add("issuedtodate", typeof(System.Double));
                mDataTable.Columns.Add("issuedqty", typeof(System.Double));
                mDataTable.Columns.Add("amount", typeof(System.Double));

                #region get onhandqty and issuedqty

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
                    mDataRow["issuedtodate"] = 0;
                    mDataRow["issuedqty"] = mReceivedQty;
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

        #region View_Issues
        public DataTable View_Issues(bool mDateSpecified, DateTime mDateFrom, DateTime mDateTo, String mExtraFilter, string mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_Issues";

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

                string mCommandText = "select * from som_stockissues";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mExtraFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("som_stockissues");
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

        #region View_IssueItems
        public DataTable View_IssueItems(string mFilter, string mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_IssueItems";

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
                string mCommandText = "select * from som_stockissueitems";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("som_stockissueitems");
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

        #region Get_IssuedToDate
        public DataTable Get_IssuedToDate(string mFilter, string mOrder)
        {
            String mFunctionName = "Get_IssuedToDate";

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
                string mCommandText = "select itemtransferid, sum(issuedqty * piecesinpackage) issuedqty from som_transferoutissueditems";

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


                DataTable mDataTable = new DataTable("som_transferoutissueditems");
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

        #region Get_IssuedToDateByExpiryDates
        public DataTable Get_IssuedToDateByExpiryDates(string mFilter, string mOrder)
        {
            String mFunctionName = "Get_IssuedToDateByExpiryDates";

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
                string mCommandText = "select productcode, itemtransferid, expirydate, piecesinpackage, sum(issuedqty * piecesinpackage) issuedqty from som_transferoutissueditems";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                mCommandText = mCommandText + " group by productcode, itemtransferid, expirydate, piecesinpackage";

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;


                DataTable mDataTable = new DataTable("som_transferoutissueditems");
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
        public AfyaPro_Types.clsResult Add(Int16 mGenerateCode, DateTime mTransDate, string mTransferOutNo,
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivtransferouts_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region auto generate transfer no, if option is on

            if (mGenerateCode == 1)
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

            #region check 4 duplicate
            try
            {
                mCommand.CommandText = "select transferno from som_transferouts where transferno='"
                + mTransferOutNo.Trim() + "'";
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

            #region add
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //som_transferouts
                mCommand.CommandText = "insert into som_transferouts(sysdate,transdate,transferno,transfertitle,tocode,todescription,"
                + "fromcode,fromdescription,transfertype,currencycode,currencydescription,exchangerate,currencysymbol,remarks,"
                + "transferstatus,userid) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate)
                + ",'" + mTransferOutNo.Trim() + "','" + mTitle.Trim() + "','" + mToCode.Trim() + "','" + mToDescription.Trim() + "','"
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

                    mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,transferno,transfertitle,tocode,todescription,"
                    + "fromcode,fromdescription,transfertype,currencycode,currencydescription,exchangerate,currencysymbol,remarks,"
                    + "transferstatus,userid," + mAuditingFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                    + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mTransferOutNo.Trim() + "','" + mTitle.Trim() + "','"
                    + mToCode.Trim() + "','" + mToDescription.Trim() + "','" + mFromCode.Trim() + "','" + mFromDescription.Trim() + "','"
                    + mTransferType + "','" + mCurrencyCode.Trim() + "','" + mCurrencyDescription.Trim() + "'," + mExchangeRate + ",'"
                    + mCurrencySymbol.Trim() + "','" + mRemarks.Trim() + "','" + AfyaPro_Types.clsEnums.SomTransferStatus.Open.ToString()
                    + "','" + mUserId.Trim() + "'," + mAuditingValues + ")";
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
                    double mTransferedQty = Convert.ToDouble(mDataRow["transferedqty"]);
                    double mTransPrice = Convert.ToDouble(mDataRow["transprice"]);

                    string mExtraFields = clsGlobal.Build_FieldsForProductExtraInfos();
                    string mExtraFieldValues = clsGlobal.Build_FieldValuesForProductExtraInfos(mDvProducts, mProductCode);

                    string mItemTransferId = clsGlobal.Generate_AutoId() + mRowCount;

                    //som_transferoutitems
                    mCommand.CommandText = "insert into som_transferoutitems(sysdate,transdate,transferno,itemtransferid,productcode,packagingcode,"
                    + "packagingdescription,piecesinpackage,transferedqty,transprice,userid," + mExtraFields + ") values("
                    + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mTransferOutNo.Trim()
                    + "','" + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "',"
                    + mPiecesInPackage + "," + mTransferedQty + "," + mTransPrice + ",'" + mUserId.Trim() + "'," + mExtraFieldValues + ")";
                    mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit som_transferoutitems

                    if (mRecsAffected > 0)
                    {
                        string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_transferoutitems";
                        string mAuditingFields = clsGlobal.AuditingFields();
                        string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                            AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                        mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,transferno,itemtransferid,productcode,"
                        + "packagingcode,packagingdescription,piecesinpackage,transferedqty,transprice,userid," + mExtraFields + ","
                        + mAuditingFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate)
                        + ",'" + mTransferOutNo.Trim() + "','" + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode + "','"
                        + mPackagingDescription + "'," + mPiecesInPackage + "," + mTransferedQty + "," + mTransPrice + ",'" + mUserId.Trim() + "',"
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
        public AfyaPro_Types.clsResult Edit(DateTime mTransDate, string mTransferOutNo, string mTransferInNo,
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivtransferouts_edit.ToString(), mUserId);
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
                mCommand.CommandText = "select * from som_transferouts where transferno='" + mTransferOutNo.Trim() + "'";
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

                #region som_transferouts

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

                #endregion

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

                #region items

                #region deleted items

                #region som_transferoutitems

                DataTable mDtSavedOutItems = new DataTable("transferoutitems");
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

                #endregion

                #region som_transferinitems

                if (mTransferType.Trim().ToLower() ==
                    AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString().Trim().ToLower())
                {
                    DataTable mDtSavedInItems = new DataTable("som_transferinitems");
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
                            if (mTransferInNo.Trim() != "")
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

                    if (mTransferType.Trim().ToLower() ==
                        AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString().Trim().ToLower())
                    {
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
                mDvSavedItems.Table = mDtSavedOutItems;
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

                        //som_transferoutitems
                        mCommand.CommandText = "insert into som_transferoutitems(sysdate,transdate,transferno,transferinno,itemtransferid,"
                        + "productcode,packagingcode,packagingdescription,piecesinpackage,transferedqty,transprice,userid," + mExtraFields
                        + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                        + mTransferOutNo.Trim() + "','" + mTransferInNo.Trim() + "','" + mItemTransferId + "','" + mProductCode + "','"
                        + mPackagingCode + "','" + mPackagingDescription + "'," + mPiecesInPackage + "," + mTransferedQty + "," + mTransPrice
                        + ",'" + mUserId.Trim() + "'," + mExtraFieldValues + ")";
                        mRecsAffected = mCommand.ExecuteNonQuery();

                        #region audit som_transferoutitems

                        if (mRecsAffected > 0)
                        {
                            string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_transferoutitems";
                            string mAuditingFields = clsGlobal.AuditingFields();
                            string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                            mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,transferno,transferinno,itemtransferid,"
                            + "productcode,packagingcode,packagingdescription,piecesinpackage,transferedqty,transprice,userid," + mExtraFields 
                            + "," + mAuditingFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                            + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mTransferOutNo.Trim() + "','" + mTransferInNo.Trim() + "','" 
                            + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "'," 
                            + mPiecesInPackage + "," + mTransferedQty + "," + mTransPrice + ",'" + mUserId.Trim() + "'," + mExtraFieldValues 
                            + "," + mAuditingValues + ")";
                            mCommand.ExecuteNonQuery();
                        }

                        #endregion

                        if (mTransferType.Trim().ToLower() ==
                            AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString().Trim().ToLower())
                        {
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

        #region Issue_Items
        public AfyaPro_Types.clsResult Issue_Items(Int16 mGenerateCode, DateTime mTransDate, string mDeliveryNo, string mTransferOutNo, 
            string mTransferInNo, string mTitle, string mToCode, string mToDescription, string mFromCode, 
            string mFromDescription, string mTransferType, bool mDoubleEntry, string mCurrencyCode, string mCurrencyDescription, 
            double mExchangeRate, string mCurrencySymbol, string mRemarks, DataTable mDtItems, string mUserId)
        {
            String mFunctionName = "Issue_Items";

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivtransferouts_receive.ToString(), mUserId);
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
                mCommand.CommandText = "select * from som_transferouts where transferno='" + mTransferOutNo.Trim() + "'";
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

            #region auto generate delivery no, if option is on

            if (mGenerateCode == 1)
            {
                clsAutoCodes objAutoCodes = new clsAutoCodes();
                AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                mObjCode = objAutoCodes.Next_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.deliveryno), "som_stockissues", "deliveryno");
                if (mObjCode.Exe_Result == -1)
                {
                    mResult.Exe_Result = mObjCode.Exe_Result;
                    mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                    return mResult;
                }
                mDeliveryNo = mObjCode.GeneratedCode;
            }

            #endregion

            #region check duplicate deliveryno for receipts - store to store option

            if (mTransferType.Trim().ToLower() ==
                AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString().Trim().ToLower())
            {
                if (mDoubleEntry == true && mTransferInNo.Trim() != "")
                {
                    if (mGenerateCode == 1)
                    {
                        clsAutoCodes objAutoCodes = new clsAutoCodes();
                        AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                        mObjCode = objAutoCodes.Next_Code(
                            Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.deliveryno), "som_stockreceipts", "deliveryno");
                        if (mObjCode.Exe_Result == -1)
                        {
                            mResult.Exe_Result = mObjCode.Exe_Result;
                            mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                            return mResult;
                        }
                        mDeliveryNo = mObjCode.GeneratedCode;
                    }
                }
            }

            #endregion

            #region validate deliveryno

            try
            {
                mCommand.CommandText = "select * from som_stockissues where deliveryno='" + mDeliveryNo.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    if (mDataReader["tocode"].ToString().Trim().ToLower() != mToCode.Trim().ToLower())
                    {
                        mResult.Exe_Result = 0;
                        mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IV_DeliveryNoIsInUseForAnotherSource.ToString();
                        return mResult;
                    }

                    if (mDataReader["transferno"].ToString().Trim().ToLower() != mTransferOutNo.Trim().ToLower())
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

            #region validate deliveryno - receiving store

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

            #region Issue
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

                string mFromStoreCode = mFromCode.Trim();
                string mToStoreCode = "";

                if (mTransferType.Trim().ToLower() ==
                    AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString().Trim().ToLower())
                {
                    mToStoreCode = mToCode.Trim();
                }

                #region som_stockissues

                DataTable mDtIssues = new DataTable("issues");
                mCommand.CommandText = "select * from som_stockissues where deliveryno='" + mDeliveryNo.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtIssues);

                if (mDtIssues.Rows.Count == 0)
                {
                    //som_stockissues
                    mCommand.CommandText = "insert into som_stockissues(sysdate,transdate,deliveryno,transferno,transferinno,"
                    + "transfertitle,tocode,todescription,fromcode,fromdescription,transfertype,currencycode,currencydescription,"
                    + "exchangerate,currencysymbol,remarks,transferstatus,userid) values(" + clsGlobal.Saving_DateValue(DateTime.Now)
                    + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mDeliveryNo.Trim() + "','" + mTransferOutNo.Trim() + "','"
                    + mTransferInNo.Trim() + "','" + mTitle.Trim() + "','" + mToCode.Trim() + "','" + mToDescription.Trim() + "','"
                    + mFromCode.Trim() + "','" + mFromDescription.Trim() + "','" + mTransferType + "','" + mCurrencyCode.Trim() + "','"
                    + mCurrencyDescription.Trim() + "'," + mExchangeRate + ",'" + mCurrencySymbol.Trim() + "','" + mRemarks.Trim() + "','"
                    + AfyaPro_Types.clsEnums.SomTransferStatus.Open.ToString() + "','" + mUserId.Trim() + "')";
                    mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit som_stockissues

                    if (mRecsAffected > 0)
                    {
                        string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_stockissues";
                        string mAuditingFields = clsGlobal.AuditingFields();
                        string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                            AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                        mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,deliveryno,transferno,transferinno,"
                        + "transfertitle,tocode,todescription,fromcode,fromdescription,transfertype,currencycode,currencydescription,"
                        + "exchangerate,currencysymbol,remarks,transferstatus,userid," + mAuditingFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now)
                        + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mDeliveryNo.Trim() + "','" + mTransferOutNo.Trim() + "','"
                        + mTransferInNo.Trim() + "','" + mTitle.Trim() + "','" + mToCode.Trim() + "','" + mToDescription.Trim() + "','"
                        + mFromCode.Trim() + "','" + mFromDescription.Trim() + "','" + mTransferType + "','" + mCurrencyCode.Trim() + "','"
                        + mCurrencyDescription.Trim() + "'," + mExchangeRate + ",'" + mCurrencySymbol.Trim() + "','" + mRemarks.Trim() + "','"
                        + AfyaPro_Types.clsEnums.SomTransferStatus.Open.ToString() + "','" + mUserId.Trim() + "'," + mAuditingValues + ")";
                        mCommand.ExecuteNonQuery();
                    }

                    #endregion
                }
                else
                {
                    mCommand.CommandText = "update som_stockissues set transdate=" + clsGlobal.Saving_DateValue(mTransDate)
                    + ",transfertitle='" + mTitle.Trim() + "',tocode='" + mToCode.Trim()
                    + "',todescription='" + mToDescription.Trim() + "',fromcode='" + mFromCode.Trim()
                    + "',fromdescription='" + mFromDescription.Trim() + "',currencycode='" + mCurrencyCode.Trim()
                    + "',currencydescription='" + mCurrencyDescription.Trim() + "',exchangerate=" + mExchangeRate
                    + ",currencysymbol='" + mCurrencySymbol.Trim() + "',remarks='" + mRemarks.Trim()
                    + "',transfertype='" + mTransferType + "' where deliveryno='" + mDeliveryNo.Trim() + "'";
                    mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit som_stockissues
                    if (mRecsAffected > 0)
                    {
                        //get current details
                        DataTable mDtOrders = new DataTable("som_stockissues");
                        mCommand.CommandText = "select * from som_stockissues where deliveryno='" + mDeliveryNo.Trim() + "'";
                        mDataAdapter.SelectCommand = mCommand;
                        mDataAdapter.Fill(mDtOrders);

                        mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtOrders, "som_stockissues",
                        mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                        mCommand.ExecuteNonQuery();
                    }
                    #endregion
                }

                #endregion

                #region som_stockreceipts

                if (mTransferType.Trim().ToLower() ==
                    AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString().Trim().ToLower())
                {
                    if (mDoubleEntry == true && mTransferInNo.Trim() != "")
                    {
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
                    }
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
                    double mIssuedQty = Convert.ToDouble(mDataRow["issuedqty"]);
                    double mTransPrice = Convert.ToDouble(mDataRow["transprice"]);

                    double mProductQty = mIssuedQty * mPiecesInPackage;

                    if (mProductQty != 0)
                    {
                        string mExtraFields = clsGlobal.Build_FieldsForProductExtraInfos();
                        string mExtraFieldValues = clsGlobal.Build_FieldValuesForProductExtraInfos(mDvProducts, mProductCode);

                        #region add item to order if does not exist

                        if (mItemTransferId.Trim() == "")
                        {
                            mItemTransferId = clsGlobal.Generate_AutoId() + mRowCount;

                            #region som_transferoutitems

                            mCommand.CommandText = "insert into som_transferoutitems(sysdate,transdate,transferno,transferinno,itemtransferid,"
                            + "productcode,packagingcode,packagingdescription,piecesinpackage,transferedqty,transprice,userid," + mExtraFields
                            + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                            + mTransferOutNo + "','" + mTransferInNo.Trim() + "','" + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode
                            + "','" + mPackagingDescription + "'," + mPiecesInPackage + "," + mIssuedQty + "," + mTransPrice + ",'"
                            + mUserId.Trim() + "'," + mExtraFieldValues + ")";
                            mRecsAffected = mCommand.ExecuteNonQuery();

                            #region audit som_transferoutitems

                            if (mRecsAffected > 0)
                            {
                                string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_transferoutitems";
                                string mAuditingFields = clsGlobal.AuditingFields();
                                string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                    AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                                mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,transferno,transferinno,itemtransferid,"
                                + "productcode,packagingcode,packagingdescription,piecesinpackage,transferedqty,transprice,userid," + mExtraFields
                                + "," + mAuditingFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                                + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mTransferOutNo.Trim() + "','" + mTransferInNo.Trim() + "','"
                                + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "',"
                                + mPiecesInPackage + "," + mIssuedQty + "," + mTransPrice + ",'" + mUserId.Trim() + "'," + mExtraFieldValues
                                + "," + mAuditingValues + ")";
                                mCommand.ExecuteNonQuery();
                            }

                            #endregion

                            #endregion

                            #region som_transferinitems

                            if (mTransferType.Trim().ToLower() ==
                                AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString().Trim().ToLower())
                            {
                                if (mDoubleEntry == true && mTransferOutNo.Trim() != "")
                                {
                                    mCommand.CommandText = "insert into som_transferinitems(sysdate,transdate,transferno,transferoutno,"
                                    + "itemtransferid,productcode,packagingcode,packagingdescription,piecesinpackage,transferedqty,transprice,"
                                    + "userid," + mExtraFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                                    + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mTransferInNo.Trim() + "','" + mTransferOutNo.Trim() + "','"
                                    + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "',"
                                    + mPiecesInPackage + "," + mIssuedQty + "," + mTransPrice + ",'" + mUserId.Trim() + "'," + mExtraFieldValues + ")";
                                    mRecsAffected = mCommand.ExecuteNonQuery();

                                    #region audit som_transferinitems

                                    if (mRecsAffected > 0)
                                    {
                                        string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_transferinitems";
                                        string mAuditingFields = clsGlobal.AuditingFields();
                                        string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                            AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                                        mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,transferno,transferoutno,"
                                        + "itemtransferid,productcode,packagingcode,packagingdescription,piecesinpackage,transferedqty,transprice,"
                                        + "userid," + mExtraFields + "," + mAuditingFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now)
                                        + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mTransferInNo.Trim() + "','" + mTransferOutNo.Trim()
                                        + "','" + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription
                                        + "'," + mPiecesInPackage + "," + mIssuedQty + "," + mTransPrice + ",'" + mUserId.Trim() + "',"
                                        + mExtraFieldValues + "," + mAuditingValues + ")";
                                        mCommand.ExecuteNonQuery();
                                    }

                                    #endregion
                                }
                            }

                            #endregion

                            mRowCount++;
                        }

                        #endregion

                        #region som_transferoutissueditems

                        mCommand.CommandText = "insert into som_transferoutissueditems(sysdate,transdate,transferno,transferinno,deliveryno,"
                        + "itemtransferid,productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,issuedqty,transprice,userid,"
                        + "fromstorecode,tostorecode," + mExtraFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                        + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mTransferOutNo.Trim() + "','" + mTransferInNo.Trim() + "','" 
                        + mDeliveryNo.Trim() + "','" + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode + "','" 
                        + mPackagingDescription + "'," + mPiecesInPackage + "," + mExpiryDate + "," + mIssuedQty + "," + mTransPrice + ",'" 
                        + mUserId.Trim() + "','" + mFromStoreCode + "','" + mToStoreCode + "'," + mExtraFieldValues + ")";
                        mRecsAffected = mCommand.ExecuteNonQuery();

                        #region audit som_transferoutissueditems

                        if (mRecsAffected > 0)
                        {
                            string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_transferoutissueditems";
                            string mAuditingFields = clsGlobal.AuditingFields();
                            string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                            mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,transferno,transferinno,deliveryno,"
                            + "itemtransferid,productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,issuedqty,transprice,userid,"
                            + "fromstorecode,tostorecode," + mExtraFields + "," + mAuditingFields + ") values("
                            + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mTransferOutNo.Trim()
                            + "','" + mTransferInNo.Trim() + "','" + mDeliveryNo.Trim() + "','" + mItemTransferId + "','" + mProductCode + "','" 
                            + mPackagingCode + "','" + mPackagingDescription + "'," + mPiecesInPackage + "," + mExpiryDate + "," + mIssuedQty + "," 
                            + mTransPrice + ",'" + mUserId.Trim() + "','" + mFromStoreCode + "','" + mToStoreCode + "'," + mExtraFieldValues + "," 
                            + mAuditingValues + ")";
                            mCommand.ExecuteNonQuery();
                        }

                        #endregion

                        #endregion

                        #region som_stockissueitems

                        mCommand.CommandText = "insert into som_stockissueitems(sysdate,transdate,transferno,transferinno,deliveryno,"
                        + "itemtransferid,productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,issuedqty,transprice,userid,"
                        + "fromstorecode,tostorecode," + mExtraFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                        + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mTransferOutNo.Trim() + "','" + mTransferInNo.Trim() + "','"
                        + mDeliveryNo.Trim() + "','" + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode + "','"
                        + mPackagingDescription + "'," + mPiecesInPackage + "," + mExpiryDate + "," + mIssuedQty + "," + mTransPrice + ",'"
                        + mUserId.Trim() + "','" + mFromStoreCode + "','" + mToStoreCode + "'," + mExtraFieldValues + ")";
                        mRecsAffected = mCommand.ExecuteNonQuery();

                        #region audit som_stockissueitems

                        if (mRecsAffected > 0)
                        {
                            string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_stockissueitems";
                            string mAuditingFields = clsGlobal.AuditingFields();
                            string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                            mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,transferno,transferinno,deliveryno,"
                            + "itemtransferid,productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,issuedqty,transprice,userid,"
                            + "fromstorecode,tostorecode," + mExtraFields + "," + mAuditingFields + ") values("
                            + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mTransferOutNo.Trim()
                            + "','" + mTransferInNo.Trim() + "','" + mDeliveryNo.Trim() + "','" + mItemTransferId + "','" + mProductCode + "','"
                            + mPackagingCode + "','" + mPackagingDescription + "'," + mPiecesInPackage + "," + mExpiryDate + "," + mIssuedQty + ","
                            + mTransPrice + ",'" + mUserId.Trim() + "','" + mFromStoreCode + "','" + mToStoreCode + "'," + mExtraFieldValues + ","
                            + mAuditingValues + ")";
                            mCommand.ExecuteNonQuery();
                        }

                        #endregion

                        #endregion

                        #region som_transferinreceiveditems

                        if (mTransferType.Trim().ToLower() ==
                            AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString().Trim().ToLower())
                        {
                            if (mDoubleEntry == true && mTransferInNo.Trim() != "")
                            {
                                mCommand.CommandText = "insert into som_transferinreceiveditems(sysdate,transdate,transferno,transferoutno,"
                                + "deliveryno,itemtransferid,productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,"
                                + "fromstorecode,tostorecode,receivedqty,transprice,userid," + mExtraFields + ") values("
                                + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                                + mTransferInNo.Trim() + "','" + mTransferOutNo.Trim() + "','" + mDeliveryNo.Trim() + "','"
                                + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "',"
                                + mPiecesInPackage + "," + mExpiryDate + ",'" + mFromStoreCode + "','" + mToStoreCode + "'," + mIssuedQty + ","
                                + mTransPrice + ",'" + mUserId.Trim() + "'," + mExtraFieldValues + ")";
                                mRecsAffected = mCommand.ExecuteNonQuery();

                                #region audit som_transferinreceiveditems

                                if (mRecsAffected > 0)
                                {
                                    string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_transferinreceiveditems";
                                    string mAuditingFields = clsGlobal.AuditingFields();
                                    string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                        AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                                    mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,transferno,transferoutno,"
                                    + "deliveryno,itemtransferid,productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,"
                                    + "fromstorecode,tostorecode,receivedqty,transprice,userid," + mExtraFields + "," + mAuditingFields
                                    + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate)
                                    + ",'" + mTransferInNo.Trim() + "','" + mTransferOutNo.Trim() + "','" + mDeliveryNo.Trim() + "','"
                                    + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "',"
                                    + mPiecesInPackage + "," + mExpiryDate + ",'" + mFromStoreCode + "','" + mToStoreCode + "'," + mIssuedQty + ","
                                    + mTransPrice + ",'" + mUserId.Trim() + "'," + mExtraFieldValues + "," + mAuditingValues + ")";
                                    mCommand.ExecuteNonQuery();
                                }

                                #endregion
                            }
                        }

                        #endregion

                        #region som_stockreceiptitems

                        if (mTransferType.Trim().ToLower() ==
                            AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString().Trim().ToLower())
                        {
                            if (mDoubleEntry == true && mTransferInNo.Trim() != "")
                            {
                                mCommand.CommandText = "insert into som_stockreceiptitems(sysdate,transdate,transferno,transferoutno,"
                                + "deliveryno,itemtransferid,productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,"
                                + "fromstorecode,tostorecode,receivedqty,transprice,userid," + mExtraFields + ") values("
                                + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                                + mTransferInNo.Trim() + "','" + mTransferOutNo.Trim() + "','" + mDeliveryNo.Trim() + "','"
                                + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "',"
                                + mPiecesInPackage + "," + mExpiryDate + ",'" + mFromStoreCode + "','" + mToStoreCode + "'," + mIssuedQty + ","
                                + mTransPrice + ",'" + mUserId.Trim() + "'," + mExtraFieldValues + ")";
                                mRecsAffected = mCommand.ExecuteNonQuery();

                                #region audit som_stockreceiptitems

                                if (mRecsAffected > 0)
                                {
                                    string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_stockreceiptitems";
                                    string mAuditingFields = clsGlobal.AuditingFields();
                                    string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                        AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                                    mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,transferno,transferoutno,"
                                    + "deliveryno,itemtransferid,productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,"
                                    + "fromstorecode,tostorecode,receivedqty,transprice,userid," + mExtraFields + "," + mAuditingFields
                                    + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate)
                                    + ",'" + mTransferInNo.Trim() + "','" + mTransferOutNo.Trim() + "','" + mDeliveryNo.Trim() + "','"
                                    + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "',"
                                    + mPiecesInPackage + "," + mExpiryDate + ",'" + mFromStoreCode + "','" + mToStoreCode + "'," + mIssuedQty + ","
                                    + mTransPrice + ",'" + mUserId.Trim() + "'," + mExtraFieldValues + "," + mAuditingValues + ")";
                                    mCommand.ExecuteNonQuery();
                                }

                                #endregion
                            }
                        }

                        #endregion

                        #region stock

                        string mProductTransDescription = "";

                        #region issuing part

                        mFromCode = mFromCode.Trim().ToLower();
                        mProductTransDescription = "Issue";

                        #region som_productexpirydates

                        //som_productexpirydates
                        mCommand.CommandText = "update som_productexpirydates set quantity=quantity-" + mProductQty + " where storecode='" 
                            + mFromStoreCode + "' and productcode='" + mProductCode + "' and expirydate=" + mExpiryDate;
                        mCommand.ExecuteNonQuery();

                        mCommand.CommandText = "delete from som_productexpirydates where storecode='" + mFromStoreCode
                            + "' and productcode='" + mProductCode + "' and expirydate=" + mExpiryDate + " and quantity=0";
                        mCommand.ExecuteNonQuery();

                        #endregion

                        #region productcontrol

                        //som_productcontrol
                        mCommand.CommandText = "update som_productcontrol set qty_" + mFromStoreCode + " = qty_" + mFromStoreCode + " - "
                        + mProductQty + " where productcode='" + mProductCode + "'";
                        mRecsAffected = mCommand.ExecuteNonQuery();
                        if (mRecsAffected == 0)
                        {
                            mCommand.CommandText = "insert into som_productcontrol(productcode,qty_" + mFromStoreCode + ") "
                            + "values('" + mProductCode + "'," + -mProductQty + ")";
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

                        //som_producttransactions
                        mCommand.CommandText = "insert into som_producttransactions(sysdate,transdate,sourcecode,sourcedescription,"
                        + "productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,reference,transtype,"
                        + "transdescription,userid,qtyout_" + mFromCode + ",transprice," + mExtraFields + ") values("
                        + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                        + mToCode.Trim() + "','" + mToDescription.Trim() + "','" + mProductCode + "','"
                        + mPackagingCode + "','" + mPackagingDescription + "'," + mPiecesInPackage + "," + mExpiryDate + ",'"
                        + mDeliveryNo + "','" + AfyaPro_Types.clsEnums.ProductTransTypes.Issue.ToString() + "','"
                        + mProductTransDescription + "','" + mUserId.Trim() + "'," + mProductQty + "," + mTransPrice + "," + mExtraFieldValues + ")";
                        mRecsAffected = mCommand.ExecuteNonQuery();

                        #region audit som_transferoutissueditems

                        if (mRecsAffected > 0)
                        {
                            string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_producttransactions";
                            string mAuditingFields = clsGlobal.AuditingFields();
                            string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                            mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,sourcecode,sourcedescription,"
                            + "productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,reference,transtype,"
                            + "transdescription,userid,qtyout_" + mFromCode + ",transprice," + mExtraFields + "," + mAuditingFields + ") values("
                            + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                            + mToCode.Trim() + "','" + mToDescription.Trim() + "','" + mProductCode + "','"
                            + mPackagingCode + "','" + mPackagingDescription + "'," + mPiecesInPackage + "," + mExpiryDate + ",'"
                            + mDeliveryNo + "','" + AfyaPro_Types.clsEnums.ProductTransTypes.Issue.ToString() + "','"
                            + mProductTransDescription + "','" + mUserId.Trim() + "'," + mProductQty + "," + mTransPrice + ","
                            + mExtraFieldValues + "," + mAuditingValues + ")";
                            mCommand.ExecuteNonQuery();
                        }

                        #endregion

                        #endregion

                        #endregion

                        #region receiving part

                        if (mTransferType.Trim().ToLower() ==
                            AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString().Trim().ToLower())
                        {
                            if (mDoubleEntry == true)
                            {
                                mToCode = mToCode.Trim().ToLower();
                                mProductTransDescription = "Receive";

                                #region som_productexpirydates

                                //som_productexpirydates
                                mCommand.CommandText = "update som_productexpirydates set quantity=quantity+" + mProductQty + " where storecode='"
                                + mToStoreCode.Trim() + "' and productcode='" + mProductCode + "' and expirydate=" + mExpiryDate;
                                mRecsAffected = mCommand.ExecuteNonQuery();
                                if (mRecsAffected == 0)
                                {
                                    mCommand.CommandText = "insert into som_productexpirydates(storecode,productcode,expirydate,quantity) "
                                    + "values('" + mToStoreCode.Trim() + "','" + mProductCode + "'," + mExpiryDate + "," + mProductQty + ")";
                                    mCommand.ExecuteNonQuery();
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

                                //som_producttransactions
                                mCommand.CommandText = "insert into som_producttransactions(sysdate,transdate,sourcecode,sourcedescription,"
                                + "productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,reference,transtype,"
                                + "transdescription,userid,qtyin_" + mToCode + ",transprice," + mExtraFields + ") values("
                                + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                                + mFromCode.Trim() + "','" + mFromDescription.Trim() + "','" + mProductCode + "','"
                                + mPackagingCode + "','" + mPackagingDescription + "'," + mPiecesInPackage + "," + mExpiryDate + ",'"
                                + mDeliveryNo + "','" + AfyaPro_Types.clsEnums.ProductTransTypes.Receive.ToString() + "','"
                                + mProductTransDescription + "','" + mUserId.Trim() + "'," + mProductQty + "," + mTransPrice + ","
                                + mExtraFieldValues + ")";
                                mRecsAffected = mCommand.ExecuteNonQuery();

                                #region audit som_transferoutissueditems

                                if (mRecsAffected > 0)
                                {
                                    string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_producttransactions";
                                    string mAuditingFields = clsGlobal.AuditingFields();
                                    string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                        AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                                    mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,sourcecode,sourcedescription,"
                                    + "productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,reference,transtype,"
                                    + "transdescription,userid,qtyin_" + mToCode + ",transprice," + mExtraFields + "," + mAuditingFields + ") values("
                                    + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                                    + mFromCode.Trim() + "','" + mFromDescription.Trim() + "','" + mProductCode + "','"
                                    + mPackagingCode + "','" + mPackagingDescription + "'," + mPiecesInPackage + "," + mExpiryDate + ",'"
                                    + mDeliveryNo + "','" + AfyaPro_Types.clsEnums.ProductTransTypes.Receive.ToString() + "','"
                                    + mProductTransDescription + "','" + mUserId.Trim() + "'," + mProductQty + "," + mTransPrice + ","
                                    + mExtraFieldValues + "," + mAuditingValues + ")";
                                    mCommand.ExecuteNonQuery();
                                }

                                #endregion

                                #endregion
                            }
                        }

                        #endregion

                        #endregion
                    }
                }

                if (mGenerateCode == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.deliveryno);
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

        #region Close
        public AfyaPro_Types.clsResult Close(DateTime mTransDate, string mTransferOutNo, string mTransferInNo,
            string mTransferType, bool mDoubleEntry, string mUserId)
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivtransferouts_close.ToString(), mUserId);
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
                mCommand.CommandText = "select * from som_transferouts where transferno='" + mTransferOutNo.Trim() + "'";
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

                #region som_transferouts

                mCommand.CommandText = "update som_transferouts set transferstatus='" 
                + AfyaPro_Types.clsEnums.SomTransferStatus.Closed.ToString()
                + "' where transferno='" + mTransferOutNo.Trim() + "'";
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

                #endregion

                if (mTransferType.Trim().ToLower() ==
                    AfyaPro_Types.clsEnums.SomTransferTypes.StoreToStore.ToString().Trim().ToLower())
                {
                    if (mDoubleEntry == true && mTransferInNo.Trim() != "")
                    {
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
    }
}
