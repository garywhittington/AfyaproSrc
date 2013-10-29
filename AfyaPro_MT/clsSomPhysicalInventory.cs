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
    public class clsSomPhysicalInventory : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsSomPhysicalInventory";

        #endregion

        #region View_PhysicalInventory
        public DataTable View_PhysicalInventory(bool mDateSpecified, DateTime mDateFrom, DateTime mDateTo, String mExtraFilter, string mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_PhysicalInventory";

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

                string mCommandText = "select * from som_physicalinventory";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mExtraFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("som_physicalinventory");
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

        #region View_PhysicalInventoryItems
        public DataTable View_PhysicalInventoryItems(string mStoreCode, string mFilter, string mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_PhysicalInventoryItems";

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
                string mCommandText = "select * from som_physicalinventoryitems where storecode='" + mStoreCode.Trim() + "'";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mFilter + ")";
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("som_physicalinventoryitems");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                mDataTable.Columns.Add("deltaqty", typeof(System.Double));
                mDataTable.Columns.Add("deltapercent", typeof(System.Int32));
                mDataTable.Columns.Add("amount", typeof(System.Double));

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

        #region Get_ExpectedQuantities
        public DataTable Get_ExpectedQuantities(string mStoreCode, string mFilter, string mOrder)
        {
            String mFunctionName = "Get_ExpectedQuantities";

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
                string mQtyInField = "qtyin_" + mStoreCode.Trim().ToLower();
                string mQtyOutField = "qtyout_" + mStoreCode.Trim().ToLower();

                string mCommandText = "select productcode,expirydate,(sum(" + mQtyInField
                + ")-sum(" + mQtyOutField + ")) expectedqty from som_producttransactions";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                mCommandText = mCommandText + " group by productcode,expirydate";

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;


                DataTable mDataTable = new DataTable("som_producttransactions");
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
        public AfyaPro_Types.clsResult Add(Int16 mGenerateCode, DateTime mTransDate, string mStoreCode, 
            string mStoreDescription, string mReferenceNo, string mDescription, DataTable mDtItems, string mUserId)
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivphysicalinventory_count.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region auto generate reference no, if option is on

            if (mGenerateCode == 1)
            {
                clsAutoCodes objAutoCodes = new clsAutoCodes();
                AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                mObjCode = objAutoCodes.Next_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.stocktakingno), "som_physicalinventory", "referenceno");
                if (mObjCode.Exe_Result == -1)
                {
                    mResult.Exe_Result = mObjCode.Exe_Result;
                    mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                    return mResult;
                }
                mReferenceNo = mObjCode.GeneratedCode;
            }

            #endregion

            #region check 4 duplicate
            try
            {
                mCommand.CommandText = "select referenceno from som_physicalinventory where referenceno='"
                + mReferenceNo.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IV_StockTakingNoIsInUse.ToString();
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

                //som_physicalinventory
                mCommand.CommandText = "insert into som_physicalinventory(sysdate,transdate,storecode,storedescription,referenceno,"
                + "description,inventorystatus,userid) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," 
                + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mStoreCode.Trim() + "','" + mStoreDescription.Trim() + "','" + mReferenceNo.Trim()
                + "','" + mDescription.Trim() + "','" + AfyaPro_Types.clsEnums.SomInventoryStatus.Open.ToString() 
                + "','" + mUserId.Trim() + "')";
                mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit som_physicalinventory

                if (mRecsAffected > 0)
                {
                    string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_physicalinventory";
                    string mAuditingFields = clsGlobal.AuditingFields();
                    string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                        AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                    mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,storecode,storedescription,referenceno,"
                    + "description,inventorystatus,userid," + mAuditingFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                    + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mStoreCode.Trim() + "','" + mStoreDescription.Trim() + "','" + mReferenceNo.Trim()
                    + "','" + mDescription.Trim() + "','" + AfyaPro_Types.clsEnums.SomInventoryStatus.Open.ToString()
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

                foreach (DataRow mDataRow in mDtItems.Rows)
                {
                    string mProductCode = mDataRow["productcode"].ToString().Trim();
                    string mPackagingCode = mDataRow["packagingcode"].ToString().Trim();
                    string mPackagingDescription = mDataRow["packagingdescription"].ToString().Trim();
                    int mPiecesInPackage = Convert.ToInt32(mDataRow["piecesinpackage"]);
                    string mExpiryDate = clsGlobal.Saving_DateValueNullable(mDataRow["expirydate"]);
                    double mCountedQty = Convert.ToDouble(mDataRow["countedqty"]);
                    double mExpectedQty = Convert.ToDouble(mDataRow["expectedqty"]);
                    double mTransPrice = Convert.ToDouble(mDataRow["transprice"]);

                    string mExtraFields = clsGlobal.Build_FieldsForProductExtraInfos();
                    string mExtraFieldValues = clsGlobal.Build_FieldValuesForProductExtraInfos(mDvProducts, mProductCode);

                    //som_physicalinventoryitems
                    mCommand.CommandText = "insert into som_physicalinventoryitems(sysdate,transdate,storecode,referenceno,"
                    + "productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,countedqty,expectedqty,"
                    + "transprice,userid," + mExtraFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                    + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mStoreCode.Trim() + "','" + mReferenceNo.Trim() + "','"
                    + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "'," + mPiecesInPackage + ","
                    + mExpiryDate + "," + mCountedQty + "," + mExpectedQty + "," + mTransPrice + ",'" + mUserId.Trim() + "',"
                    + mExtraFieldValues + ")";
                    mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit som_physicalinventoryitems

                    if (mRecsAffected > 0)
                    {
                        string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_physicalinventoryitems";
                        string mAuditingFields = clsGlobal.AuditingFields();
                        string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                            AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                        mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,storecode,referenceno,"
                        + "productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,countedqty,expectedqty,"
                        + "transprice,userid," + mExtraFields + "," + mAuditingFields + ") values("
                        + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                        + mStoreCode.Trim() + "','" + mReferenceNo.Trim() + "','" + mProductCode + "','" + mPackagingCode + "','"
                        + mPackagingDescription + "'," + mPiecesInPackage + "," + mExpiryDate + "," + mCountedQty + "," + mExpectedQty
                        + "," + mTransPrice + ",'" + mUserId.Trim() + "'," + mExtraFieldValues + "," + mAuditingValues + ")";
                        mCommand.ExecuteNonQuery();
                    }

                    #endregion
                }

                if (mGenerateCode == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.stocktakingno);
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
        public AfyaPro_Types.clsResult Edit(DateTime mTransDate, DateTime mCalculatedDate, string mStoreCode,
            string mStoreDescription, string mReferenceNo, string mDescription, DataTable mDtItems, string mInventoryStatus, string mUserId)
        {
            String mFunctionName = "Edit";

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivphysicalinventory_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region check 4 referenceno existance
            try
            {
                mCommand.CommandText = "select * from som_physicalinventory where referenceno='" + mReferenceNo.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IV_StockTakingNoDoesNotExist.ToString();
                    return mResult;
                }
                else
                {
                    if (mDataReader["inventorystatus"].ToString().Trim().ToLower() ==
                        AfyaPro_Types.clsEnums.SomInventoryStatus.Closed.ToString().ToLower())
                    {
                        mResult.Exe_Result = 0;
                        mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IV_ClosedStockTakingCannotbeEdited.ToString();
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

            #region edit
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                string mCalculatedDateString = clsGlobal.Saving_DateValueNullable(mCalculatedDate);

                //som_physicalinventory
                mCommand.CommandText = "update som_physicalinventory set transdate=" + clsGlobal.Saving_DateValue(mTransDate)
                + ",calculateddate=" + clsGlobal.Saving_DateValueNullable(mCalculatedDate) + ",storecode='" + mStoreCode.Trim()
                + "',storedescription='" + mStoreDescription.Trim() + "', description='" + mDescription.Trim() 
                + "',inventorystatus='" + mInventoryStatus.Trim() + "',userid='" + mUserId.Trim() + "' where referenceno='" 
                + mReferenceNo.Trim() + "'";
                mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit som_physicalinventory

                if (mRecsAffected > 0)
                {
                    //get current details
                    DataTable mDtPhysicalInventory = new DataTable("physicalinventory");
                    mCommand.CommandText = "select * from som_physicalinventory where referenceno='" + mReferenceNo.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtPhysicalInventory);

                    mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtPhysicalInventory, "som_physicalinventory",
                    mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region som_physicalinventoryitems

                DataTable mDtSavedItems = new DataTable("physicalinventoryitems");
                mCommand.CommandText = "select * from som_physicalinventoryitems where referenceno='" + mReferenceNo.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSavedItems);

                #region deleted items

                DataView mDvItems = new DataView();
                mDvItems.Table = mDtItems;

                foreach (DataRow mDataRow in mDtSavedItems.Rows)
                {
                    string mProductCode = mDataRow["productcode"].ToString().Trim();
                    string mExpiryDate = clsGlobal.Saving_DateValueNullable(mDataRow["expirydate"]);

                    string mWhereProduct = "";
                    if (mExpiryDate.Trim().ToLower() == "null")
                    {
                        mDvItems.RowFilter = "productcode='" + mProductCode + "' and expirydate is null";
                        mWhereProduct = " where (productcode='" + mProductCode + "') and (expirydate=Null or expirydate is Null)";
                    }
                    else
                    {
                        mDvItems.RowFilter = "productcode='" + mProductCode 
                            + "' and expirydate=#" + Convert.ToDateTime(mDataRow["expirydate"]).ToString() + "#";
                        mWhereProduct = " where productcode='" + mProductCode + "' and expirydate=" + mExpiryDate;
                    }

                    if (mDvItems.Count == 0)
                    {
                        mCommand.CommandText = "delete from som_physicalinventoryitems" + mWhereProduct;
                        mRecsAffected = mCommand.ExecuteNonQuery();

                        #region audit som_orderitems
                        if (mRecsAffected > 0)
                        {
                            mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtSavedItems, mDataRow, "som_physicalinventoryitems",
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
                    string mProductCode = mDataRow["productcode"].ToString().Trim();
                    string mPackagingCode = mDataRow["packagingcode"].ToString().Trim();
                    string mPackagingDescription = mDataRow["packagingdescription"].ToString().Trim();
                    int mPiecesInPackage = Convert.ToInt32(mDataRow["piecesinpackage"]);
                    string mExpiryDate = clsGlobal.Saving_DateValueNullable(mDataRow["expirydate"]);
                    double mCountedQty = Convert.ToDouble(mDataRow["countedqty"]);
                    double mExpectedQty = Convert.ToDouble(mDataRow["expectedqty"]);
                    double mTransPrice = Convert.ToDouble(mDataRow["transprice"]);

                    string mWhereProduct = "";
                    if (mExpiryDate.Trim().ToLower() == "null")
                    {
                        mWhereProduct = " where (productcode='" + mProductCode + "') and (expirydate=Null or expirydate is Null)";
                    }
                    else
                    {
                        mWhereProduct = " where productcode='" + mProductCode + "' and expirydate=" + mExpiryDate;
                    }

                    mCommand.CommandText = "update som_physicalinventoryitems set packagingcode='" + mPackagingCode + "',packagingdescription='"
                    + mPackagingDescription + "',piecesinpackage=" + mPiecesInPackage + ",countedqty=" + mCountedQty
                    + ",expectedqty=" + mExpectedQty + ",transprice=" + mTransPrice + ",userid='" + mUserId.Trim() + "'" + mWhereProduct;
                    mRecsAffected = mCommand.ExecuteNonQuery();

                    #region audit som_physicalinventoryitems

                    if (mRecsAffected > 0)
                    {
                        DataTable mDtUpdatedItem = new DataTable("updateditems");
                        mCommand.CommandText = "select * from som_physicalinventoryitems" + mWhereProduct;
                        mDataAdapter.SelectCommand = mCommand;
                        mDataAdapter.Fill(mDtUpdatedItem);

                        mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtUpdatedItem, "som_physicalinventoryitems",
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

                foreach (DataRow mDataRow in mDtItems.Rows)
                {
                    string mProductCode = mDataRow["productcode"].ToString().Trim();
                    string mExpiryDate = clsGlobal.Saving_DateValueNullable(mDataRow["expirydate"]);

                    string mWhereProduct = "";
                    if (mExpiryDate.Trim().ToLower() == "null")
                    {
                        mDvSavedItems.RowFilter = "productcode='" + mProductCode + "' and expirydate is null";
                        mWhereProduct = " where (productcode='" + mProductCode + "') and (expirydate=Null or expirydate is Null)";
                    }
                    else
                    {
                        mDvSavedItems.RowFilter = "productcode='" + mProductCode
                            + "' and expirydate=#" + Convert.ToDateTime(mDataRow["expirydate"]).ToString() + "#";
                        mWhereProduct = " where productcode='" + mProductCode + "' and expirydate=" + mExpiryDate;
                    }

                    if (mDvSavedItems.Count == 0)
                    {
                        string mPackagingCode = mDataRow["packagingcode"].ToString().Trim();
                        string mPackagingDescription = mDataRow["packagingdescription"].ToString().Trim();
                        int mPiecesInPackage = Convert.ToInt32(mDataRow["piecesinpackage"]);
                        double mCountedQty = Convert.ToDouble(mDataRow["countedqty"]);
                        double mExpectedQty = Convert.ToDouble(mDataRow["expectedqty"]);
                        double mTransPrice = Convert.ToDouble(mDataRow["transprice"]);

                        string mExtraFields = clsGlobal.Build_FieldsForProductExtraInfos();
                        string mExtraFieldValues = clsGlobal.Build_FieldValuesForProductExtraInfos(mDvProducts, mProductCode);

                        //som_physicalinventoryitems
                        mCommand.CommandText = "insert into som_physicalinventoryitems(sysdate,transdate,storecode,referenceno,"
                        + "productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,countedqty,expectedqty,"
                        + "transprice,userid," + mExtraFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                        + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mStoreCode.Trim() + "','" + mReferenceNo.Trim() + "','"
                        + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "'," + mPiecesInPackage + ","
                        + mExpiryDate + "," + mCountedQty + "," + mExpectedQty + "," + mTransPrice + ",'" + mUserId.Trim() + "'," 
                        + mExtraFieldValues + ")";
                        mRecsAffected = mCommand.ExecuteNonQuery();

                        #region audit som_physicalinventoryitems

                        if (mRecsAffected > 0)
                        {
                            string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_physicalinventoryitems";
                            string mAuditingFields = clsGlobal.AuditingFields();
                            string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                            mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,storecode,referenceno,"
                            + "productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,countedqty,expectedqty,"
                            + "transprice,userid," + mExtraFields + "," + mAuditingFields + ") values("
                            + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                            + mStoreCode.Trim() + "','" + mReferenceNo.Trim() + "','" + mProductCode + "','" + mPackagingCode + "','"
                            + mPackagingDescription + "'," + mPiecesInPackage + "," + mExpiryDate + "," + mCountedQty + "," + mExpectedQty
                            + "," + mTransPrice + ",'" + mUserId.Trim() + "'," + mExtraFieldValues + "," + mAuditingValues + ")";
                            mCommand.ExecuteNonQuery();
                        }

                        #endregion
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

        #region Commit
        public AfyaPro_Types.clsResult Commit(DateTime mTransDate, DateTime mCalculatedDate, string mStoreCode,
            string mReferenceNo, string mDescription, DataTable mDtItems, string mUserId)
        {
            String mFunctionName = "Commit";

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivphysicalinventory_commit.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region check 4 referenceno existance
            try
            {
                mCommand.CommandText = "select * from som_physicalinventory where referenceno='" + mReferenceNo.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IV_StockTakingNoDoesNotExist.ToString();
                    return mResult;
                }
                else
                {
                    if (mDataReader["inventorystatus"].ToString().Trim().ToLower() ==
                        AfyaPro_Types.clsEnums.SomInventoryStatus.Closed.ToString().ToLower())
                    {
                        mResult.Exe_Result = 0;
                        mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IV_ClosedStockTakingCannotbeEdited.ToString();
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

            #region commit
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //som_physicalinventory
                mCommand.CommandText = "update som_physicalinventory set closeddate="
                + clsGlobal.Saving_DateValue(mTransDate) + ",calculateddate="
                + clsGlobal.Saving_DateValue(mCalculatedDate) + ",inventorystatus='"
                + AfyaPro_Types.clsEnums.SomInventoryStatus.Closed.ToString()
                + "' where referenceno='" + mReferenceNo.Trim() + "'";
                mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit som_physicalinventory

                if (mRecsAffected > 0)
                {
                    //get current details
                    DataTable mDtPhysicalInventory = new DataTable("physicalinventory");
                    mCommand.CommandText = "select * from som_physicalinventory where referenceno='" + mReferenceNo.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtPhysicalInventory);

                    mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtPhysicalInventory, "som_physicalinventory",
                    mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region som_physicalinventoryitems

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

                mCommand.CommandText = "delete from som_productexpirydates where storecode='"
                    + mStoreCode.Trim() + "' and productcode in (" + mProductCodesList + ")";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtItems.Rows)
                {
                    string mProductCode = mDataRow["productcode"].ToString().Trim();
                    string mPackagingCode = mDataRow["packagingcode"].ToString().Trim();
                    string mPackagingDescription = mDataRow["packagingdescription"].ToString().Trim();
                    int mPiecesInPackage = Convert.ToInt32(mDataRow["piecesinpackage"]);
                    string mExpiryDate = clsGlobal.Saving_DateValueNullable(mDataRow["expirydate"]);
                    double mCountedQty = Convert.ToDouble(mDataRow["countedqty"]);
                    double mExpectedQty = Convert.ToDouble(mDataRow["expectedqty"]);
                    double mTransPrice = Convert.ToDouble(mDataRow["transprice"]);
                    double mDeltaQty = (mCountedQty - mExpectedQty) * mPiecesInPackage;

                    #region som_productexpirydates

                    if (mExpiryDate.Trim().ToLower() != "null")
                    {
                        mCommand.CommandText = "insert into som_productexpirydates(storecode,productcode,expirydate,quantity) "
                        + "values('" + mStoreCode.Trim() + "','" + mProductCode + "'," + mExpiryDate + "," + mCountedQty + ")";
                        mCommand.ExecuteNonQuery();

                        mCommand.CommandText = "delete from som_productexpirydates where storecode='" + mStoreCode.Trim()
                            + "' and productcode='" + mProductCode + "' and expirydate=" + mExpiryDate + " and quantity=0";
                        mCommand.ExecuteNonQuery();
                    }

                    #endregion

                    if (mDeltaQty != 0)
                    {
                        string mExtraFields = clsGlobal.Build_FieldsForProductExtraInfos();
                        string mExtraFieldValues = clsGlobal.Build_FieldValuesForProductExtraInfos(mDvProducts, mProductCode);

                        #region productcontrol

                        //som_productcontrol
                        mCommand.CommandText = "update som_productcontrol set qty_" + mStoreCode + " = qty_" + mStoreCode + " + "
                        + mDeltaQty + " where productcode='" + mProductCode + "'";
                        mRecsAffected = mCommand.ExecuteNonQuery();
                        if (mRecsAffected == 0)
                        {
                            mCommand.CommandText = "insert into som_productcontrol(productcode,qty_" + mStoreCode + ") "
                            + "values('" + mProductCode + "'," + mDeltaQty + ")";
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

                        string mProductTransDescription = "Adjustment";

                        double mQtyIn = 0;
                        double mQtyOut = 0;
                        if (mDeltaQty > 0)
                        {
                            mQtyIn = mDeltaQty;
                        }
                        else
                        {
                            mQtyOut = -mDeltaQty;
                        }

                        //som_producttransactions
                        mCommand.CommandText = "insert into som_producttransactions(sysdate,transdate,sourcedescription,"
                        + "productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,reference,transtype,"
                        + "transdescription,userid,qtyin_" + mStoreCode + ",qtyout_" + mStoreCode + ",transprice," + mExtraFields
                        + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                        + mDescription.Trim() + "','" + mProductCode + "','" + mPackagingCode + "','" + mPackagingDescription + "',"
                        + mPiecesInPackage + "," + mExpiryDate + ",'" + mReferenceNo + "','"
                        + AfyaPro_Types.clsEnums.ProductTransTypes.SystemAdjustment.ToString() + "','" + mProductTransDescription + "','"
                        + mUserId.Trim() + "'," + mQtyIn + "," + mQtyOut + "," + mTransPrice + "," + mExtraFieldValues + ")";
                        mRecsAffected = mCommand.ExecuteNonQuery();

                        #region audit som_producttransactions

                        if (mRecsAffected > 0)
                        {
                            string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_producttransactions";
                            string mAuditingFields = clsGlobal.AuditingFields();
                            string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                            mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,sourcedescription,"
                            + "productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,reference,transtype,"
                            + "transdescription,userid,qtyin_" + mStoreCode + ",qtyout_" + mStoreCode + ",transprice," + mExtraFields
                            + "," + mAuditingFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                            + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mDescription.Trim() + "','" + mProductCode + "','"
                            + mPackagingCode + "','" + mPackagingDescription + "'," + mPiecesInPackage + "," + mExpiryDate + ",'"
                            + mReferenceNo + "','" + AfyaPro_Types.clsEnums.ProductTransTypes.SystemAdjustment.ToString() + "','"
                            + mProductTransDescription + "','" + mUserId.Trim() + "'," + mQtyIn + "," + mQtyOut + "," + mTransPrice
                            + "," + mExtraFieldValues + "," + mAuditingValues + ")";
                            mCommand.ExecuteNonQuery();
                        }

                        #endregion

                        #endregion
                    }
                }

                #endregion

                #region som_physicalstockbalances

                mCommand.CommandText = "delete from som_physicalstockbalances"
                    + " where storecode='" + mStoreCode.Trim()
                    + "' and transdate=" + clsGlobal.Saving_DateValue(mTransDate)
                    + " and productcode in (" + mProductCodesList + ")";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtItems.Rows)
                {
                    string mProductCode = mDataRow["productcode"].ToString().Trim();
                    int mPiecesInPackage = Convert.ToInt32(mDataRow["piecesinpackage"]);
                    double mCountedQty = Convert.ToDouble(mDataRow["countedqty"]) * mPiecesInPackage;

                    DataTable mDtBalances = new DataTable("balances");
                    mCommand.CommandText = "select * from som_physicalstockbalances"
                        + " where storecode='" + mStoreCode.Trim() + "' and productcode='" + mProductCode
                        + "' and transdate=" + clsGlobal.Saving_DateValue(mTransDate);
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtBalances);

                    if (mDtBalances.Rows.Count > 0)
                    {
                        mCommand.CommandText = "update som_physicalstockbalances set quantity=quantity+" + mCountedQty
                            + " where storecode='" + mStoreCode.Trim() + "' and productcode='" + mProductCode
                            + "' and transdate=" + clsGlobal.Saving_DateValue(mTransDate);
                        mCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        mCommand.CommandText = "insert into som_physicalstockbalances(transdate,storecode,productcode,quantity) "
                        + "values(" + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mStoreCode.Trim() + "','" 
                        + mProductCode + "'," + mCountedQty + ")";
                        mCommand.ExecuteNonQuery();
                    }
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
