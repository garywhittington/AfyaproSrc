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
using System.Drawing;
using System.IO;

namespace AfyaPro_MT
{
    public class clsDataImportExport : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsDataImportExport";

        #endregion

        #region Get_DatabaseSchema
        public DataTable Get_DatabaseSchema()
        {
            String mFunctionName = "Get_DatabaseSchema";

            OdbcConnection mConn = new OdbcConnection();

            #region database connection

            try
            {
                mConn.ConnectionString = clsGlobal.gAfyaConStr;

                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }

            #endregion

            try
            {
                string[] mRestrictions = new string[3];
                mRestrictions[1] = "USER";// Owner

                DataTable mDataTable = new DataTable("databaseschema");
                mDataTable = mConn.GetSchema("Tables", mRestrictions);

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

        #region Get_TableSchema
        public DataTable Get_TableSchema(string mTableName)
        {
            String mFunctionName = "Get_TableSchema";

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
                mCommand.CommandText = "select * from " + mTableName + " where 1=2";

                DataTable mDataTable = new DataTable("tableschema");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.FillSchema(mDataTable, SchemaType.Source);

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

        #region Import
        public AfyaPro_Types.clsResult Import(string mTableName, Int16 mAutoGenerateCode, Int16 mCodeKey, string mCodeFieldName, DataTable mDtData)
        {
            String mFunctionName = "Import";

            clsAutoCodes objAutoCodes = new clsAutoCodes();
            AfyaPro_Types.clsCode mObjCode;
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

            #region do importation
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    string mCode = "";
                    bool mCodeFieldFound = false;
                    bool mCodeGenerated = false;

                    #region autogenerate code

                    if (mAutoGenerateCode == 1)
                    {
                        mObjCode = objAutoCodes.Next_Code(mCommand, mCodeKey, mTableName, mCodeFieldName);
                        if (mObjCode.Exe_Result == -1)
                        {
                            //rollback
                            try { mTrans.Rollback(); }
                            catch { }
                            mResult.Exe_Result = mObjCode.Exe_Result;
                            mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                            return mResult;
                        }
                        mCode = mObjCode.GeneratedCode;
                        mCodeGenerated = true;
                    }

                    #endregion

                    #region check if codefield is within the list

                    foreach (DataColumn mDataColumn in mDtData.Columns)
                    {
                        if (mCodeFieldName.Trim().ToLower() == mDataColumn.ColumnName.Trim().ToLower())
                        {
                            mCodeFieldFound = true;
                            break;
                        }
                    }

                    #endregion

                    string mFieldList = "";
                    string mFieldValues = "";

                    if (mCodeFieldFound == false)
                    {
                        mFieldList = mCodeFieldName;
                        mFieldValues = "'" + mCode + "'";
                    }

                    foreach (DataColumn mDataColumn in mDtData.Columns)
                    {
                        #region build field list
                        if (mFieldList.Trim() == "")
                        {
                            mFieldList = mDataColumn.ColumnName;
                        }
                        else
                        {
                            mFieldList = mFieldList + "," + mDataColumn.ColumnName;
                        }
                        #endregion

                        #region build field values

                        string mFieldValue = "";

                        #region type conversion

                        switch (mDataColumn.DataType.FullName.ToLower())
                        {
                            case "system.boolean":
                                {
                                    if (mDataColumn.AllowDBNull == false)
                                    {
                                        mFieldValue = Convert.ToBoolean(mDataRow[mDataColumn.ColumnName]).ToString();
                                    }
                                    else
                                    {
                                        try
                                        {
                                            mFieldValue = Convert.ToBoolean(mDataRow[mDataColumn.ColumnName]).ToString();
                                        }
                                        catch
                                        {
                                            mFieldValue = "Null";
                                        }
                                    }
                                }
                                break;
                            case "system.byte":
                                {
                                    if (mDataColumn.AllowDBNull == false)
                                    {
                                        mFieldValue = Convert.ToInt16(mDataRow[mDataColumn.ColumnName]).ToString();
                                    }
                                    else
                                    {
                                        try
                                        {
                                            mFieldValue = Convert.ToInt16(mDataRow[mDataColumn.ColumnName]).ToString();
                                        }
                                        catch
                                        {
                                            mFieldValue = "Null";
                                        }
                                    }
                                }
                                break;
                            case "system.datetime":
                                {
                                    if (mDataColumn.AllowDBNull == false)
                                    {
                                        mFieldValue = clsGlobal.Saving_DateValue(Convert.ToDateTime(mDataRow[mDataColumn.ColumnName]));
                                    }
                                    else
                                    {
                                        mFieldValue = clsGlobal.Saving_DateValueNullable(mDataRow[mDataColumn.ColumnName]);
                                    }
                                }
                                break;
                            case "system.decimal":
                                {
                                    if (mDataColumn.AllowDBNull == false)
                                    {
                                        mFieldValue = Convert.ToDouble(mDataRow[mDataColumn.ColumnName]).ToString();
                                    }
                                    else
                                    {
                                        try
                                        {
                                            mFieldValue = Convert.ToDouble(mDataRow[mDataColumn.ColumnName]).ToString();
                                        }
                                        catch
                                        {
                                            mFieldValue = "Null";
                                        }
                                    }
                                }
                                break;
                            case "system.double":
                                {
                                    if (mDataColumn.AllowDBNull == false)
                                    {
                                        mFieldValue = Convert.ToDouble(mDataRow[mDataColumn.ColumnName]).ToString();
                                    }
                                    else
                                    {
                                        try
                                        {
                                            mFieldValue = Convert.ToDouble(mDataRow[mDataColumn.ColumnName]).ToString();
                                        }
                                        catch
                                        {
                                            mFieldValue = "Null";
                                        }
                                    }
                                }
                                break;
                            case "system.int16":
                                {
                                    if (mDataColumn.AllowDBNull == false)
                                    {
                                        mFieldValue = Convert.ToInt16(mDataRow[mDataColumn.ColumnName]).ToString();
                                    }
                                    else
                                    {
                                        try
                                        {
                                            mFieldValue = Convert.ToInt16(mDataRow[mDataColumn.ColumnName]).ToString();
                                        }
                                        catch
                                        {
                                            mFieldValue = "Null";
                                        }
                                    }
                                }
                                break;
                            case "system.int32":
                                {
                                    if (mDataColumn.AllowDBNull == false)
                                    {
                                        mFieldValue = Convert.ToInt32(mDataRow[mDataColumn.ColumnName]).ToString();
                                    }
                                    else
                                    {
                                        try
                                        {
                                            mFieldValue = Convert.ToInt32(mDataRow[mDataColumn.ColumnName]).ToString();
                                        }
                                        catch
                                        {
                                            mFieldValue = "Null";
                                        }
                                    }
                                }
                                break;
                            case "system.int64":
                                {
                                    if (mDataColumn.AllowDBNull == false)
                                    {
                                        mFieldValue = Convert.ToInt64(mDataRow[mDataColumn.ColumnName]).ToString();
                                    }
                                    else
                                    {
                                        try
                                        {
                                            mFieldValue = Convert.ToInt64(mDataRow[mDataColumn.ColumnName]).ToString();
                                        }
                                        catch
                                        {
                                            mFieldValue = "Null";
                                        }
                                    }
                                }
                                break;
                            case "system.sbyte":
                                {
                                    if (mDataColumn.AllowDBNull == false)
                                    {
                                        mFieldValue = Convert.ToInt16(mDataRow[mDataColumn.ColumnName]).ToString();
                                    }
                                    else
                                    {
                                        try
                                        {
                                            mFieldValue = Convert.ToInt16(mDataRow[mDataColumn.ColumnName]).ToString();
                                        }
                                        catch
                                        {
                                            mFieldValue = "Null";
                                        }
                                    }
                                }
                                break;
                            case "system.single":
                                {
                                    if (mDataColumn.AllowDBNull == false)
                                    {
                                        mFieldValue = Convert.ToSingle(mDataRow[mDataColumn.ColumnName]).ToString();
                                    }
                                    else
                                    {
                                        try
                                        {
                                            mFieldValue = Convert.ToSingle(mDataRow[mDataColumn.ColumnName]).ToString();
                                        }
                                        catch
                                        {
                                            mFieldValue = "Null";
                                        }
                                    }
                                }
                                break;
                            case "system.unint16":
                                {
                                    if (mDataColumn.AllowDBNull == false)
                                    {
                                        mFieldValue = Convert.ToUInt16(mDataRow[mDataColumn.ColumnName]).ToString();
                                    }
                                    else
                                    {
                                        try
                                        {
                                            mFieldValue = Convert.ToUInt16(mDataRow[mDataColumn.ColumnName]).ToString();
                                        }
                                        catch
                                        {
                                            mFieldValue = "Null";
                                        }
                                    }
                                }
                                break;
                            case "system.unint32":
                                {
                                    if (mDataColumn.AllowDBNull == false)
                                    {
                                        mFieldValue = Convert.ToUInt32(mDataRow[mDataColumn.ColumnName]).ToString();
                                    }
                                    else
                                    {
                                        try
                                        {
                                            mFieldValue = Convert.ToUInt32(mDataRow[mDataColumn.ColumnName]).ToString();
                                        }
                                        catch
                                        {
                                            mFieldValue = "Null";
                                        }
                                    }
                                }
                                break;
                            case "system.unint64":
                                {
                                    if (mDataColumn.AllowDBNull == false)
                                    {
                                        mFieldValue = Convert.ToUInt64(mDataRow[mDataColumn.ColumnName]).ToString();
                                    }
                                    else
                                    {
                                        try
                                        {
                                            mFieldValue = Convert.ToUInt64(mDataRow[mDataColumn.ColumnName]).ToString();
                                        }
                                        catch
                                        {
                                            mFieldValue = "Null";
                                        }
                                    }
                                }
                                break;
                            default: mFieldValue = "'" + mDataRow[mDataColumn.ColumnName].ToString() + "'"; break;
                        }

                        #endregion

                        if (mCodeFieldName.Trim().ToLower() == mDataColumn.ColumnName.Trim().ToLower())
                        {
                            if (mAutoGenerateCode == 1 && mCodeFieldFound == true)
                            {
                                mFieldValue = "'" + mCode + "'";
                            }
                        }

                        if (mFieldValues.Trim() == "")
                        {
                            mFieldValues = mFieldValue;
                        }
                        else
                        {
                            mFieldValues = mFieldValues + "," + mFieldValue;
                        }

                        #endregion
                    }

                    mCommand.CommandText = "insert into " + mTableName + "(" + mFieldList + ") values(" + mFieldValues + ")";
                    mCommand.ExecuteNonQuery();

                    if (mCodeGenerated == true)
                    {
                        mCommand.CommandText = "update facilityautocodes set "
                        + "idcurrent=idcurrent+idincrement where codekey="
                        + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.clientgroupmembercode);
                        mCommand.ExecuteNonQuery();
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
