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
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace AfyaPro_MT
{
    public class clsGridColumns : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsGridColumns";

        #endregion

        #region View_DisplayFields
        public DataTable View_DisplayFields(String mTableName)
        {
            String mFunctionName = "View_DisplayFields";

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
                DataTable mDataTable = new DataTable("sys_gridcolumns");
                mDataTable.RemotingFormat = SerializationFormat.Binary;

                mCommand.CommandText = "select * from sys_gridcolumns where tablename='" + mTableName.Trim() + "'";
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

        #region Get_GridColumns
        public DataTable Get_GridColumns()
        {
            String mFunctionName = "Get_GridColumns";

            try
            {
                DataTable mDtGridColumns = new DataTable("gridcolumns");

                mDtGridColumns.Columns.Add("tablename", typeof(System.String));
                mDtGridColumns.Columns.Add("fieldname", typeof(System.String));
                mDtGridColumns.Columns.Add("datatype", typeof(System.String));
                mDtGridColumns.Columns.Add("visibility", typeof(System.Boolean));

                DataRow mNewRow;

                #region Orders

                //productcode
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.Orders.ToString();
                mNewRow["fieldname"] = "productcode";
                mNewRow["datatype"] = "Text";
                mNewRow["visibility"] = true;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //productdescription
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.Orders.ToString();
                mNewRow["fieldname"] = "productdescription";
                mNewRow["datatype"] = "Text";
                mNewRow["visibility"] = true;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //orderedqty
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.Orders.ToString();
                mNewRow["fieldname"] = "orderedqty";
                mNewRow["datatype"] = "Number";
                mNewRow["visibility"] = true;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //costprice
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.Orders.ToString();
                mNewRow["fieldname"] = "costprice";
                mNewRow["datatype"] = "Currency";
                mNewRow["visibility"] = true;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //totalamount
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.Orders.ToString();
                mNewRow["fieldname"] = "totalamount";
                mNewRow["datatype"] = "Currency";
                mNewRow["visibility"] = true;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //productopmcode
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.Orders.ToString();
                mNewRow["fieldname"] = "productopmcode";
                mNewRow["datatype"] = "Text";
                mNewRow["visibility"] = false;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //productopmdescription
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.Orders.ToString();
                mNewRow["fieldname"] = "productopmdescription";
                mNewRow["datatype"] = "Text";
                mNewRow["visibility"] = false;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //packagingcode
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.Orders.ToString();
                mNewRow["fieldname"] = "packagingcode";
                mNewRow["datatype"] = "Text";
                mNewRow["visibility"] = false;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //packagingdescription
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.Orders.ToString();
                mNewRow["fieldname"] = "packagingdescription";
                mNewRow["datatype"] = "Text";
                mNewRow["visibility"] = false;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //piecesinpackage
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.Orders.ToString();
                mNewRow["fieldname"] = "piecesinpackage";
                mNewRow["datatype"] = "Number";
                mNewRow["visibility"] = false;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                #endregion

                #region ProductBalances

                //code
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.ProductBalances.ToString();
                mNewRow["fieldname"] = "code";
                mNewRow["datatype"] = "Text";
                mNewRow["visibility"] = true;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //description
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.ProductBalances.ToString();
                mNewRow["fieldname"] = "description";
                mNewRow["datatype"] = "Text";
                mNewRow["visibility"] = true;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //costprice
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.ProductBalances.ToString();
                mNewRow["fieldname"] = "costprice";
                mNewRow["datatype"] = "Currency";
                mNewRow["visibility"] = true;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //price1
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.ProductBalances.ToString();
                mNewRow["fieldname"] = "price1";
                mNewRow["datatype"] = "Currency";
                mNewRow["visibility"] = true;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //qty
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.ProductBalances.ToString();
                mNewRow["fieldname"] = "qty";
                mNewRow["datatype"] = "Number";
                mNewRow["visibility"] = true;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //expirydate
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.ProductBalances.ToString();
                mNewRow["fieldname"] = "expirydate";
                mNewRow["datatype"] = "Date";
                mNewRow["visibility"] = false;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //opmcode
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.ProductBalances.ToString();
                mNewRow["fieldname"] = "opmcode";
                mNewRow["datatype"] = "Text";
                mNewRow["visibility"] = false;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //opmdescription
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.ProductBalances.ToString();
                mNewRow["fieldname"] = "opmdescription";
                mNewRow["datatype"] = "Text";
                mNewRow["visibility"] = false;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //departmentcode
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.ProductBalances.ToString();
                mNewRow["fieldname"] = "departmentcode";
                mNewRow["datatype"] = "Text";
                mNewRow["visibility"] = false;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //departmentdescription
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.ProductBalances.ToString();
                mNewRow["fieldname"] = "departmentdescription";
                mNewRow["datatype"] = "Text";
                mNewRow["visibility"] = false;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //packagingcode
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.ProductBalances.ToString();
                mNewRow["fieldname"] = "packagingcode";
                mNewRow["datatype"] = "Text";
                mNewRow["visibility"] = false;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //packagingdescription
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.ProductBalances.ToString();
                mNewRow["fieldname"] = "packagingdescription";
                mNewRow["datatype"] = "Text";
                mNewRow["visibility"] = false;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //piecesinpackage
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.ProductBalances.ToString();
                mNewRow["fieldname"] = "piecesinpackage";
                mNewRow["datatype"] = "Number";
                mNewRow["visibility"] = false;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //costprice
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.ProductBalances.ToString();
                mNewRow["fieldname"] = "costprice";
                mNewRow["datatype"] = "Currency";
                mNewRow["visibility"] = false;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //price2
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.ProductBalances.ToString();
                mNewRow["fieldname"] = "price2";
                mNewRow["datatype"] = "Currency";
                mNewRow["visibility"] = false;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //price3
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.ProductBalances.ToString();
                mNewRow["fieldname"] = "price3";
                mNewRow["datatype"] = "Currency";
                mNewRow["visibility"] = false;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //price4
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.ProductBalances.ToString();
                mNewRow["fieldname"] = "price4";
                mNewRow["datatype"] = "Currency";
                mNewRow["visibility"] = false;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //price5
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.ProductBalances.ToString();
                mNewRow["fieldname"] = "price5";
                mNewRow["datatype"] = "Currency";
                mNewRow["visibility"] = false;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //price6
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.ProductBalances.ToString();
                mNewRow["fieldname"] = "price6";
                mNewRow["datatype"] = "Currency";
                mNewRow["visibility"] = false;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //price7
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.ProductBalances.ToString();
                mNewRow["fieldname"] = "price7";
                mNewRow["datatype"] = "Currency";
                mNewRow["visibility"] = false;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //price8
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.ProductBalances.ToString();
                mNewRow["fieldname"] = "price8";
                mNewRow["datatype"] = "Currency";
                mNewRow["visibility"] = false;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //price9
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.ProductBalances.ToString();
                mNewRow["fieldname"] = "price9";
                mNewRow["datatype"] = "Currency";
                mNewRow["visibility"] = false;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                //price10
                mNewRow = mDtGridColumns.NewRow();
                mNewRow["tablename"] = AfyaPro_Types.clsEnums.CartNames.ProductBalances.ToString();
                mNewRow["fieldname"] = "price10";
                mNewRow["datatype"] = "Currency";
                mNewRow["visibility"] = false;
                mDtGridColumns.Rows.Add(mNewRow);
                mDtGridColumns.AcceptChanges();

                #endregion

                return mDtGridColumns;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
        }
        #endregion

        #region Save_DisplayFields
        public AfyaPro_Types.clsResult Save_DisplayFields(string mSourceTableName, string mTableName, DataTable mDtFields)
        {
            String mFunctionName = "Save_DisplayFields";

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

            #region do save
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                DataTable mDtColumns = this.Get_GridColumns();
                DataView mDvColumns = new DataView();
                mDvColumns.Table = mDtColumns;
                mDvColumns.RowFilter = "tablename='" + mSourceTableName.Trim() + "'";
                mDvColumns.Sort = "fieldname";

                //execute statements
                mCommand.CommandText = "delete from sys_gridcolumns where tablename='" + mTableName.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtFields.Rows)
                {
                    int mRowIndex = mDvColumns.Find(mDataRow["fieldname"].ToString().Trim());
                    string mDataType = "Text";
                    if (mRowIndex >= 0)
                    {
                        mDataType = mDvColumns[mRowIndex]["datatype"].ToString().Trim();
                    }

                    mCommand.CommandText = "insert into sys_gridcolumns(tablename,fieldname,datatype) values('" 
                    + mTableName.Trim() + "','" + mDataRow["fieldname"].ToString().Trim() + "','" + mDataType + "')";
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

        #region Load_UserSettings
        public byte[] Load_UserSettings(string mUserId, string mGridName)
        {
            FileStream mFileStream = null;
            string mFunctionName = "Load_UserSettings";

            try
            {
                string mFileName = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"UserSettings\" + mUserId + @"\" + mGridName + ".xml";

                if (File.Exists(mFileName) == false)
                {
                    return null;
                }

                mFileStream = new FileStream(mFileName, FileMode.Open);
                MemoryStream mMemoryStream = new MemoryStream();

                mMemoryStream.SetLength(mFileStream.Length);
                mFileStream.Read(mMemoryStream.GetBuffer(), 0, (int)mFileStream.Length);
                mMemoryStream.Flush();

                return mMemoryStream.ToArray();
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
            finally
            {
                try
                {
                    mFileStream.Close();
                }
                catch { }
            }
        }
        #endregion

        #region Save_UserSettings
        public bool Save_UserSettings(string mUserId, string mGridName, byte[] mByte)
        {
            FileStream mFileStream = null;
            string mFunctionName = "Save_UserSettings";

            try
            {
                //root directory
                DirectoryInfo mDirectoryInfo = new DirectoryInfo(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"UserSettings");
                if (mDirectoryInfo.Exists == false)
                {
                    mDirectoryInfo.Create();
                }

                //user directory
                mDirectoryInfo = new DirectoryInfo(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"UserSettings\" + mUserId);
                if (mDirectoryInfo.Exists == false)
                {
                    mDirectoryInfo.Create();
                }

                //file name for grid
                string mFileName = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"UserSettings\" + mUserId + @"\" + mGridName + ".xml";

                //save settings
                mFileStream = new FileStream(mFileName, FileMode.Create);
                MemoryStream mMemoryStream = new MemoryStream(mByte);
                mMemoryStream.WriteTo(mFileStream);
                mFileStream.Flush();

                return true;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return false;
            }
            finally
            {
                try
                {
                    mFileStream.Close();
                }
                catch { }
            }
        }
        #endregion
    }
}
