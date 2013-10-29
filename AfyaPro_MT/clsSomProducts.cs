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
    public class clsSomProducts : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsSomProducts";

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
                string mCommandText = "select * from som_products";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("som_products");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                #region price category headers

                DataTable mDtPriceCategories = new DataTable("pricecategories");
                mCommand.CommandText = "select * from facilitypricecategories";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtPriceCategories);

                if (mDtPriceCategories.Rows.Count > 0)
                {
                    for (Int16 mIndex = 0; mIndex < 10; mIndex++)
                    {
                        mDataTable.Columns["price" + (mIndex + 1)].Caption = mDtPriceCategories.Rows[0]["price" + (mIndex + 1)].ToString().Trim();
                    }
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

        #region View_Stock
        public DataTable View_Stock(string mStoreCode, string mFilter, string mOrder, string mLanguageName, string mGridName)
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
                string mCommandText = "select * from som_products";
                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDtProducts = new DataTable("som_products");
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtProducts);

                #region price category headers

                DataTable mDtPriceCategories = new DataTable("pricecategories");
                mCommand.CommandText = "select * from facilitypricecategories";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtPriceCategories);

                if (mDtPriceCategories.Rows.Count > 0)
                {
                    for (Int16 mIndex = 0; mIndex < 10; mIndex++)
                    {
                        mDtProducts.Columns["price" + (mIndex + 1)].Caption = mDtPriceCategories.Rows[0]["price" + (mIndex + 1)].ToString().Trim();
                    }
                }
                #endregion

                DataTable mDataTable = mDtProducts.Copy();
                mDataTable.Columns.Add("qty", typeof(System.Double));
                mDataTable.Rows.Clear();

                #region stock

                clsSomStores mMdtSomStores = new clsSomStores();
                DataTable mDtStock = mMdtSomStores.Get_OnHandQuantities(mStoreCode, "", "");

                DataView mDvStock = new DataView();
                mDvStock.Table = mDtStock;
                mDvStock.Sort = "productcode";

                foreach (DataRow mDataRow in mDtProducts.Rows)
                {
                    DataRowView[] mDataRowsView = mDvStock.FindRows(mDataRow["code"].ToString().Trim());

                    bool mRecordFound = false;

                    foreach (DataRowView mDataRowView in mDataRowsView)
                    {
                        DataRow mNewRow = mDataTable.NewRow();

                        foreach (DataColumn mDataColumn in mDtProducts.Columns)
                        {
                            mNewRow[mDataColumn.ColumnName] = mDataRow[mDataColumn.ColumnName];
                        }

                        mNewRow["qty"] = mDataRowView["onhandqty"];
                        mDataTable.Rows.Add(mNewRow);
                        mDataTable.AcceptChanges();

                        mRecordFound = true;
                    }

                    if (mRecordFound == false)
                    {
                        DataRow mNewRow = mDataTable.NewRow();

                        foreach (DataColumn mDataColumn in mDtProducts.Columns)
                        {
                            mNewRow[mDataColumn.ColumnName] = mDataRow[mDataColumn.ColumnName];
                        }

                        mNewRow["qty"] = 0;
                        mDataTable.Rows.Add(mNewRow);
                        mDataTable.AcceptChanges();
                    }
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

        #region Add
        public AfyaPro_Types.clsResult Add(Int16 mGenerateCode, string mCode, string mDescription,
            string mOpmCode, string mOpmDescription, string mDisplayName, string mDeptCode, string mDeptDescription, 
            string mPackagingCode, string mPackagingDescription, int mPiecesInPackage, 
            int mHasExpiry, int mExpiryNotice, double mCostPrice,double mPrice1, double mPrice2, 
            double mPrice3, double mPrice4, double mPrice5, double mPrice6, double mPrice7,
            double mPrice8, double mPrice9, double mPrice10, double mMinLevel, double mOrderQty,
            DataTable mDtStores, string mUserId)
        {
            String mFunctionName = "Add";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivsproducts_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region auto generate code, if option is on

            if (mGenerateCode == 1)
            {
                clsAutoCodes objAutoCodes = new clsAutoCodes();
                AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                mObjCode = objAutoCodes.Next_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.productcode), "som_products", "code");
                if (mObjCode.Exe_Result == -1)
                {
                    mResult.Exe_Result = mObjCode.Exe_Result;
                    mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                    return mResult;
                }
                mCode = mObjCode.GeneratedCode;
            }

            #endregion

            #region check 4 duplicate
            try
            {
                mCommand.CommandText = "select * from som_products where code='"
                + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IVS_ProductCodeIsInUse.ToString();
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

                //som_products
                mCommand.CommandText = "insert into som_products(code,description,displayname,opmcode,opmdescription,"
                + "departmentcode,departmentdescription,packagingcode,packagingdescription,piecesinpackage,"
                + "hasexpiry,expirynotice,price1,price2,price3,price4,price5,price6,price7,"
                + "price8,price9,price10,minlevel,orderqty) values('" + mCode.Trim() + "','" + mDescription.Trim()
                + "','" + mOpmCode.Trim() + "','" + mOpmDescription.Trim() + "','" + mDisplayName.Trim() + "','" + mDeptCode.Trim() 
                + "','" + mDeptDescription.Trim() + "','" + mPackagingCode.Trim() + "','" 
                + mPackagingDescription.Trim() + "'," + mPiecesInPackage + "," + mHasExpiry + ","
                + mExpiryNotice + "," + mPrice1 + "," + mPrice2 + "," + mPrice3 + ","
                + mPrice4 + "," + mPrice5 + "," + mPrice6 + "," + mPrice7 + "," + mPrice8 + "," + mPrice9
                + "," + mPrice10 + "," + mMinLevel + "," + mOrderQty + ")";
                mCommand.ExecuteNonQuery();

                #region costing

                double mprevcost = mCostPrice;
                double mPreviousQty = 1;
                double mCurrentCost = mCostPrice;
                double mCurrentQty = 0;

                double mAverageCost = ((mprevcost * mPreviousQty) + (mCurrentCost * mCurrentQty)) / (mPreviousQty + mCurrentQty);

                mCommand.CommandText = "update som_products set costprice=" + mCurrentCost + ",prevcost="
                + mprevcost + ",averagecost=" + mAverageCost + " where code='" + mCode.Trim() + "'";
                int mRecsAffected = mCommand.ExecuteNonQuery();

                if (mRecsAffected > 0)
                {
                    mCommand.CommandText = "insert into som_productcostslog(sysdate,transdate,productcode,costprice) values("
                    + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(DateTime.Now.Date) + ",'"
                    + mCode.Trim() + "'," + mCurrentCost + ")";
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                if (mGenerateCode == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.productcode);
                    mCommand.ExecuteNonQuery();
                }

                #region visibility  in stores

                foreach (DataRow mDataRow in mDtStores.Rows)
                {
                    string mStoreCode = mDataRow["code"].ToString().Trim().ToLower();

                    if (Convert.ToBoolean(mDataRow["selected"]) == true)
                    {
                        mCommand.CommandText = "update som_products set visible_" + mStoreCode + "=1"
                        + " where code='" + mCode.Trim() + "'";
                        mCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        mCommand.CommandText = "update som_products set visible_" + mStoreCode + "=0"
                        + " where code='" + mCode.Trim() + "'";
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

        #region Edit
        public AfyaPro_Types.clsResult Edit(string mCode, string mDescription,
            string mOpmCode, string mOpmDescription, string mDisplayName, string mDeptCode, string mDeptDescription, 
            string mPackagingCode, string mPackagingDescription, int mPiecesInPackage, 
            int mHasExpiry, int mExpiryNotice, double mCostPrice,double mPrice1, double mPrice2, 
            double mPrice3, double mPrice4, double mPrice5, double mPrice6, double mPrice7,
            double mPrice8, double mPrice9, double mPrice10, double mMinLevel, double mOrderQty,
            DataTable mDtStores, string mUserId)
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivsproducts_edit.ToString(), mUserId);
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
                mCommand.CommandText = "select * from som_products where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IVS_ProductDoesNotExist.ToString();
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

                #region get previous costing

                DataTable mDtProducts = new DataTable("products");
                mCommand.CommandText = "select costprice from som_products where code='" + mCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtProducts);

                double mprevcost = 0;
                double mPreviousQty = 1;
                if (mDtProducts.Rows.Count > 0)
                {
                    mprevcost = Convert.ToDouble(mDtProducts.Rows[0]["costprice"]);
                    clsSomStores mSomStores = new clsSomStores();
                    DataTable mDtControl = mSomStores.Get_OnHandQuantities(mCommand, "productcode='" + mCode.Trim() + "'", "");
                    if (mDtControl != null)
                    {
                        if (mDtControl.Rows.Count > 0)
                        {
                            if (Convert.ToDouble(mDtControl.Rows[0]["onhandqty"]) > 0)
                            {
                                mPreviousQty = Convert.ToDouble(mDtControl.Rows[0]["onhandqty"]);
                            }
                        }
                    }
                }

                #endregion

                //som_products
                mCommand.CommandText = "update som_products set description = '" + mDescription.Trim() 
                + "',opmcode='" + mOpmCode.Trim() + "',opmdescription='" + mOpmDescription .Trim()
                + "',departmentcode='" + mDeptCode.Trim() + "',departmentdescription='" + mDeptDescription.Trim() 
                + "',packagingcode='" + mPackagingCode.Trim() + "',packagingdescription='" + mPackagingDescription.Trim()
                + "',piecesinpackage=" + mPiecesInPackage + ",hasexpiry=" + mHasExpiry + ",expirynotice=" + mExpiryNotice
                + ",price1=" + mPrice1 + ",price2=" + mPrice2 + ",price3=" + mPrice3 
                + ",price4=" + mPrice4 + ",price5=" + mPrice5 + ",price6=" + mPrice6 + ",price7=" + mPrice7 
                + ",price8=" + mPrice8 + ",price9=" + mPrice9 + ",price10=" + mPrice10 + ",minlevel=" + mMinLevel
                + ",orderqty=" + mOrderQty + ",displayname='" + mDisplayName.Trim() + "' where code='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                #region costing

                double mCurrentCost = mCostPrice;
                double mCurrentQty = 0;

                if (mprevcost != mCurrentCost)
                {
                    double mAverageCost = ((mprevcost * mPreviousQty) + (mCurrentCost * mCurrentQty)) / (mPreviousQty + mCurrentQty);

                    mCommand.CommandText = "update som_products set costprice=" + mCurrentCost + ",prevcost="
                    + mprevcost + ",averagecost=" + mAverageCost + " where code='" + mCode.Trim() + "'";
                    int mRecsAffected = mCommand.ExecuteNonQuery();

                    if (mRecsAffected > 0)
                    {
                        mCommand.CommandText = "insert into som_productcostslog(sysdate,transdate,productcode,costprice) values("
                        + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(DateTime.Now.Date) + ",'"
                        + mCode.Trim() + "'," + mCurrentCost + ")";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region visibility in stores

                foreach (DataRow mDataRow in mDtStores.Rows)
                {
                    string mStoreCode = mDataRow["code"].ToString().Trim().ToLower();

                    if (Convert.ToBoolean(mDataRow["selected"]) == true)
                    {
                        mCommand.CommandText = "update som_products set visible_" + mStoreCode + "=1"
                        + " where code='" + mCode.Trim() + "'";
                        mCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        mCommand.CommandText = "update som_products set visible_" + mStoreCode + "=0"
                        + " where code='" + mCode.Trim() + "'";
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

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

        #region Delete
        public AfyaPro_Types.clsResult Delete(String mCode, string mUserId)
        {
            String mFunctionName = "Delete";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.ivsproducts_delete.ToString(), mUserId);
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
                mCommand.CommandText = "select * from som_products where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.IVS_ProductDoesNotExist.ToString();
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

                mCommand.CommandText = "update som_products set display=0 where code='" + mCode.Trim() + "'";
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
    }
}
