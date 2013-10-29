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
using System.Linq;

namespace AfyaPro_MT
{
    public class clsReporter : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsReporter";
 

        #endregion

        #region Age_Formula
        public string Age_Formula(string mBirthDateField, string mEventDateField, string mResultFieldName)
        {
            return clsGlobal.Age_Formula(mBirthDateField, mEventDateField, mResultFieldName);
        }
        #endregion

        #region View_Data
        public DataTable View_Data(string mTableName, string mFilter, string mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_Data";

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
                string mCommandText = "select * from " + mTableName;

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("data");
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

        #region View_ReportCharts
        public DataTable View_ReportCharts(string mFilter, string mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_ReportCharts";

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
                string mCommandText = "select * from sys_reportcharts";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("data");
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

        #region View_LookupData
        public DataTable View_LookupData(string mTableName, string mFieldList, string mFilter, string mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_LookupData";

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
                string mCommandText = "select " + mFieldList + " from " + mTableName;

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("data");
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

        #region Get_ConditionOperators
        public DataTable Get_ConditionOperators(string mLanguageName, string mGridName)
        {
            DataRow mNewRow;
            
            String mFunctionName = "Get_ConditionOperators";

            try
            {
                DataTable mDataTable = new DataTable("conditionoperators");
                mDataTable.Columns.Add("code", typeof(System.String));
                mDataTable.Columns.Add("description", typeof(System.String));
                mDataTable.RemotingFormat = SerializationFormat.Binary;

                DataTable mDtLanguage = new DataTable("language");
                mDtLanguage.Columns.Add("controlname");
                mDtLanguage.Columns.Add("description");

                #region get language

                if (mLanguageName.Trim() != "" && mGridName.Trim() != "")
                {
                    try
                    {
                        var mCurrLang = from lang in System.Xml.Linq.XElement.Load(
                            AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Lang\" + mLanguageName + ".xml").Elements(mGridName)
                                        select lang;

                        foreach (var mElement in mCurrLang)
                        {
                            mNewRow = mDtLanguage.NewRow();
                            mNewRow["controlname"] = (string)mElement.Element("controlname").Value.Trim().ToLower();
                            mNewRow["description"] = (string)mElement.Element("description").Value.Trim();
                            mDtLanguage.Rows.Add(mNewRow);
                            mDtLanguage.AcceptChanges();
                        }
                    }
                    catch { }
                }

                #endregion

                DataView mDvLanguage = new DataView();
                mDvLanguage.Table = mDtLanguage;
                mDvLanguage.Sort = "controlname";

                #region load condition operators

                int mRowIndex = -1;
                string mDescription = "";

                #region Equals

                mDescription = "Equals";
                mRowIndex = mDvLanguage.Find("operator" + (int)AfyaPro_Types.clsEnums.ConditionOperators.Equals);
                if (mRowIndex >= 0)
                {
                    mDescription = mDvLanguage[mRowIndex]["description"].ToString().Trim();
                }
                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "=";
                mNewRow["description"] = mDescription;
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                #endregion

                #region DoesNotEqual

                mDescription = "Does not equal";
                mRowIndex = mDvLanguage.Find("operator" + (int)AfyaPro_Types.clsEnums.ConditionOperators.DoesNotEqual);
                if (mRowIndex >= 0)
                {
                    mDescription = mDvLanguage[mRowIndex]["description"].ToString().Trim();
                }
                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "!=";
                mNewRow["description"] = mDescription;
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                #endregion

                #region IsGreaterThan

                mDescription = "Is greater than";
                mRowIndex = mDvLanguage.Find("operator" + (int)AfyaPro_Types.clsEnums.ConditionOperators.IsGreaterThan);
                if (mRowIndex >= 0)
                {
                    mDescription = mDvLanguage[mRowIndex]["description"].ToString().Trim();
                }
                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = ">";
                mNewRow["description"] = mDescription;
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                #endregion

                #region IsGreaterThanOrEqual

                mDescription = "Is greater than or equal to";
                mRowIndex = mDvLanguage.Find("operator" + (int)AfyaPro_Types.clsEnums.ConditionOperators.IsGreaterThanOrEqual);
                if (mRowIndex >= 0)
                {
                    mDescription = mDvLanguage[mRowIndex]["description"].ToString().Trim();
                }
                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = ">=";
                mNewRow["description"] = mDescription;
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                #endregion

                #region IsLessThan

                mDescription = "Is less than";
                mRowIndex = mDvLanguage.Find("operator" + (int)AfyaPro_Types.clsEnums.ConditionOperators.IsLessThan);
                if (mRowIndex >= 0)
                {
                    mDescription = mDvLanguage[mRowIndex]["description"].ToString().Trim();
                }
                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "<";
                mNewRow["description"] = mDescription;
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                #endregion

                #region IsLessThanOrEqual

                mDescription = "Is less than or equal to";
                mRowIndex = mDvLanguage.Find("operator" + (int)AfyaPro_Types.clsEnums.ConditionOperators.IsLessThanOrEqual);
                if (mRowIndex >= 0)
                {
                    mDescription = mDvLanguage[mRowIndex]["description"].ToString().Trim();
                }
                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "<=";
                mNewRow["description"] = mDescription;
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                #endregion

                #region IsBetween

                mDescription = "Is between";
                mRowIndex = mDvLanguage.Find("operator" + (int)AfyaPro_Types.clsEnums.ConditionOperators.IsBetween);
                if (mRowIndex >= 0)
                {
                    mDescription = mDvLanguage[mRowIndex]["description"].ToString().Trim();
                }
                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "AxB";
                mNewRow["description"] = mDescription;
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                #endregion

                #region IsNotBetween

                mDescription = "Is not between";
                mRowIndex = mDvLanguage.Find("operator" + (int)AfyaPro_Types.clsEnums.ConditionOperators.IsNotBetween);
                if (mRowIndex >= 0)
                {
                    mDescription = mDvLanguage[mRowIndex]["description"].ToString().Trim();
                }
                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "!AxB";
                mNewRow["description"] = mDescription;
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                #endregion

                #region Contains

                mDescription = "Contains";
                mRowIndex = mDvLanguage.Find("operator" + (int)AfyaPro_Types.clsEnums.ConditionOperators.Contains);
                if (mRowIndex >= 0)
                {
                    mDescription = mDvLanguage[mRowIndex]["description"].ToString().Trim();
                }
                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "%x%";
                mNewRow["description"] = mDescription;
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                #endregion

                #region DoesNotContain

                mDescription = "Does not contain";
                mRowIndex = mDvLanguage.Find("operator" + (int)AfyaPro_Types.clsEnums.ConditionOperators.DoesNotContain);
                if (mRowIndex >= 0)
                {
                    mDescription = mDvLanguage[mRowIndex]["description"].ToString().Trim();
                }
                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "!%x%";
                mNewRow["description"] = mDescription;
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                #endregion

                #region BeginsWith

                mDescription = "Begins with";
                mRowIndex = mDvLanguage.Find("operator" + (int)AfyaPro_Types.clsEnums.ConditionOperators.BeginsWith);
                if (mRowIndex >= 0)
                {
                    mDescription = mDvLanguage[mRowIndex]["description"].ToString().Trim();
                }
                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "x%";
                mNewRow["description"] = mDescription;
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                #endregion

                #region EndsWith

                mDescription = "Ends with";
                mRowIndex = mDvLanguage.Find("operator" + (int)AfyaPro_Types.clsEnums.ConditionOperators.EndsWith);
                if (mRowIndex >= 0)
                {
                    mDescription = mDvLanguage[mRowIndex]["description"].ToString().Trim();
                }
                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "%x";
                mNewRow["description"] = mDescription;
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                #endregion

                #endregion

                return mDataTable;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
        }
        #endregion

        #region Get_TableFields
        public DataTable Get_TableFields(string mTableName)
        {
            String mFunctionName = "Get_TableFields";

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

        #region Get_ViewsList
        public DataTable Get_ViewsList()
        {
            String mFunctionName = "Get_ViewsList";

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

                DataTable mDataTable = new DataTable("database_views");
                mDataTable = mConn.GetSchema("Views", mRestrictions);

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

        #region Get_TablesList
        public DataTable Get_TablesList()
        {
            String mFunctionName = "Get_TablesList";

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

                DataTable mDataTable = new DataTable("database_tables");
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

        #region Get_DataFromDBView
        public DataTable Get_DataFromDBView(string mViewName, DataTable mDtStaticInfo, string mFilter, string mOrder)
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            string mFunctionName = "Get_DataFromDBView";

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
                DataTable mDtData = new DataTable("data");

                string mFields = "";

                #region add static info

                if (mDtStaticInfo != null)
                {
                    foreach (DataRow mDataRow in mDtStaticInfo.Rows)
                    {
                        string mFieldName = "ctr_" + mDataRow["fieldname"].ToString().Trim().ToLower();
                        string mFieldValue = "";

                        #region build field value

                        switch (mDataRow["datatype"].ToString().Trim().ToLower())
                        {
                            case "system.decimal":
                                {
                                    mFieldValue = Convert.ToDouble(mDataRow["fieldvalue"]).ToString();
                                    mDtData.Columns.Add(mFieldName, typeof(System.Double));
                                }
                                break;
                            case "system.double":
                                {
                                    mFieldValue = Convert.ToDouble(mDataRow["fieldvalue"]).ToString();
                                    mDtData.Columns.Add(mFieldName, typeof(System.Double));
                                }
                                break;
                            case "system.int16":
                                {
                                    mFieldValue = Convert.ToInt16(mDataRow["fieldvalue"]).ToString();
                                    mDtData.Columns.Add(mFieldName, typeof(System.Int16));
                                }
                                break;
                            case "system.int32":
                                {
                                    mFieldValue = Convert.ToInt32(mDataRow["fieldvalue"]).ToString();
                                    mDtData.Columns.Add(mFieldName, typeof(System.Int32));
                                }
                                break;
                            case "system.int64":
                                {
                                    mFieldValue = Convert.ToInt64(mDataRow["fieldvalue"]).ToString();
                                    mDtData.Columns.Add(mFieldName, typeof(System.Int64));
                                }
                                break;
                            case "system.single":
                                {
                                    mFieldValue = Convert.ToSingle(mDataRow["fieldvalue"]).ToString();
                                    mDtData.Columns.Add(mFieldName, typeof(System.Single));
                                }
                                break;
                            case "system.datetime":
                                {
                                    mFieldValue = "'" + Convert.ToSingle(mDataRow["fieldvalue"]).ToString() + "'";
                                    mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                                }
                                break;
                            default:
                                {
                                    mFieldValue = "'" + mDataRow["fieldvalue"].ToString() + "'";
                                    mDtData.Columns.Add(mFieldName, typeof(System.String));
                                }
                                break;
                        }

                        #endregion

                        if (mFields.Trim() == "")
                        {
                            mFields = mFieldValue + " " + mFieldName;
                        }
                        else
                        {
                            mFields = mFields + "," + mFieldValue + " " + mFieldName;
                        }
                    }
                }

                #endregion

                #region add columns from the view

                mCommand.CommandText = "select * from " + mViewName + " where 1=2";
                DataTable mDtColumns = new DataTable("tableschema");
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.FillSchema(mDtColumns, SchemaType.Source);

                foreach (DataColumn mDataColumn in mDtColumns.Columns)
                {
                    if (mFields.Trim() == "")
                    {
                        mFields = mDataColumn.ColumnName;
                    }
                    else
                    {
                        mFields = mFields + "," + mDataColumn.ColumnName;
                    }
                }

                #endregion

                string mCommandText = "select " + mFields + " from " + mViewName;
                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where (" + mFilter + ")";
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);
                mDtData.RemotingFormat = SerializationFormat.Binary;

                return mDtData;
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

        #region Get_GeneratedSQL
        public string Get_GeneratedSQL(string mCommandText, string mFilterString, string mGroupByFields, DataTable mDtParameters)
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            string mFunctionName = "Get_GeneratedSQL";

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
                string mNewCommandText = "";
                string mNewFilterString = "";

                #region set values to filterstring

                DataView mDvParameters = new DataView();
                mDvParameters.Table = mDtParameters;
                mDvParameters.Sort = "parametercode";

                int mIndex = 0;

                while (mIndex < mFilterString.Length)
                {
                    string mParameterCode = "";
                    int mTotalChars = 1;
                    string mCurrChar = mFilterString.Substring(mIndex, 1);

                    bool mWildCardStart = false;
                    bool mWildCardEnd = false;

                    switch (mCurrChar.ToLower())
                    {
                        case "#":
                            {
                                string mStartParameterCode = mFilterString.Substring(mIndex + 1);
                                mParameterCode = mStartParameterCode.Substring(0, mStartParameterCode.IndexOf("#"));
                                mTotalChars = mParameterCode.Length + 2;

                                if (mIndex > 1)
                                {
                                    try
                                    {
                                        if (mFilterString.Substring(mIndex - 1, 2) == "%#")
                                        {
                                            mWildCardStart = true;
                                        }
                                    }
                                    catch { }
                                }
                                if (mIndex + 1 < mFilterString.Length)
                                {
                                    try
                                    {
                                        if (mStartParameterCode.Substring(mStartParameterCode.IndexOf("#"), 2) == "#%")
                                        {
                                            mWildCardEnd = true;
                                        }
                                    }
                                    catch { }
                                }

                                string mParameterType = "";
                                string mParameterValue = "";
                                bool mParameterSaveValue = false;

                                int mRowIndex = mDvParameters.Find(mParameterCode);

                                #region get input value

                                if (mRowIndex >= 0)
                                {
                                    mParameterType = mDvParameters[mRowIndex]["parametertype"].ToString().Trim();
                                    mParameterSaveValue = Convert.ToBoolean(mDvParameters[mRowIndex]["parametersavevalue"]);

                                    switch (mParameterType.ToLower())
                                    {
                                        case "date":
                                            {
                                                if (mParameterSaveValue == true)
                                                {
                                                    mParameterValue = clsGlobal.Saving_DateValue(
                                                        Convert.ToDateTime(mDvParameters[mRowIndex]["valuedatetime"]).Date);
                                                }
                                                else
                                                {
                                                    mParameterValue = clsGlobal.gDbDateQuote + "#" + mParameterCode + "#" + clsGlobal.gDbDateQuote;
                                                }
                                            }
                                            break;
                                        case "int":
                                            {
                                                if (mParameterSaveValue==true)
                                                {
                                                    mParameterValue = mDvParameters[mRowIndex]["valueint"].ToString();
                                                }
                                                else
                                                {
                                                    mParameterValue = "#" + mParameterCode + "#";
                                                }
                                            }
                                            break;
                                        case "double":
                                            {
                                                if (mParameterSaveValue == true)
                                                {
                                                    mParameterValue = mDvParameters[mRowIndex]["valuedouble"].ToString();
                                                }
                                                else
                                                {
                                                    mParameterValue = "#" + mParameterCode + "#";
                                                }
                                            }
                                            break;
                                        case "boolean":
                                            {
                                                if (mParameterSaveValue == true)
                                                {
                                                    mParameterValue = mDvParameters[mRowIndex]["valueint"].ToString();
                                                }
                                                else
                                                {
                                                    mParameterValue = "#" + mParameterCode + "#";
                                                }
                                            }
                                            break;
                                        default:
                                            {
                                                if (mParameterSaveValue == true)
                                                {
                                                    if (mWildCardStart == true && mWildCardEnd==true)
                                                    {
                                                        mParameterValue = "'%" + mDvParameters[mRowIndex]["valuestring"].ToString() + "%'";
                                                    }
                                                    else if (mWildCardStart == true && mWildCardEnd == false)
                                                    {
                                                        mParameterValue = "'%" + mDvParameters[mRowIndex]["valuestring"].ToString() + "'";
                                                    }
                                                    else if (mWildCardStart == false && mWildCardEnd == true)
                                                    {
                                                        mParameterValue = "'" + mDvParameters[mRowIndex]["valuestring"].ToString() + "%'";
                                                    }
                                                    else
                                                    {
                                                        mParameterValue = "'" + mDvParameters[mRowIndex]["valuestring"].ToString() + "'";
                                                    }
                                                }
                                                else
                                                {
                                                    if (mWildCardStart == true && mWildCardEnd == true)
                                                    {
                                                        mParameterValue = "'%#" + mParameterCode + "#%'";
                                                    }
                                                    else if (mWildCardStart == true && mWildCardEnd == false)
                                                    {
                                                        mParameterValue = "'%#" + mParameterCode + "#'";
                                                    }
                                                    else if (mWildCardStart == false && mWildCardEnd == true)
                                                    {
                                                        mParameterValue = "'#" + mParameterCode + "#%'";
                                                    }
                                                    else
                                                    {
                                                        mParameterValue = "'#" + mParameterCode + "#'";
                                                    }
                                                }
                                            }
                                            break;
                                    }
                                }
                                #endregion

                                mNewFilterString = mNewFilterString + mParameterValue;
                            }
                            break;
                        default:
                            {
                                if (mCurrChar.ToLower()!="%")
                                {
                                    mNewFilterString = mNewFilterString + mCurrChar;
                                }
                            }
                            break;
                    }

                    mIndex = mIndex + mTotalChars;
                }

                #endregion

                string mFields = "";

                #region add parametervalues

                if (mDtParameters != null)
                {
                    foreach (DataRow mDataRow in mDtParameters.Rows)
                    {
                        string mParameterCode = mDataRow["parametercode"].ToString().Trim().ToLower();
                        string mFieldName = "para_" + mParameterCode;
                        string mParameterType = mDataRow["parametertype"].ToString().Trim();
                        bool mParameterSaveValue = Convert.ToBoolean(mDataRow["parametersavevalue"]);
                        string mFieldValue = mDataRow["parametervalue"].ToString().Trim();;

                        #region build field value

                        switch (mParameterType.ToLower())
                        {
                            case "double":
                                {
                                    if (mParameterSaveValue == true)
                                    {
                                        mFieldValue = Convert.ToDouble(mDataRow["valuedouble"]).ToString();
                                    }
                                    else
                                    {
                                        mFieldValue = "#" + mParameterCode + "#";
                                    }
                                }
                                break;
                            case "int":
                                {
                                    if (mParameterSaveValue == true)
                                    {
                                        mFieldValue = Convert.ToInt32(mDataRow["valueint"]).ToString();
                                    }
                                    else
                                    {
                                        mFieldValue = "#" + mParameterCode + "#";
                                    }
                                }
                                break;
                            case "boolean":
                                {
                                    if (mParameterSaveValue == true)
                                    {
                                        mFieldValue = Convert.ToInt32(mDataRow["valueint"]).ToString();
                                    }
                                    else
                                    {
                                        mFieldValue = "#" + mParameterCode + "#";
                                    }
                                }
                                break;
                            case "date":
                                {
                                    if (mParameterSaveValue == true)
                                    {
                                        mFieldValue = clsGlobal.gDbDateQuote 
                                            + Convert.ToDateTime(mDataRow["valuedatetime"]).Date.ToString() + clsGlobal.gDbDateQuote;
                                    }
                                    else
                                    {
                                        mFieldValue = clsGlobal.gDbDateQuote + "#" + mParameterCode + "#" + clsGlobal.gDbDateQuote;
                                    }
                                }
                                break;
                            default:
                                {
                                    if (mParameterSaveValue == true)
                                    {
                                        mFieldValue = "'" + mDataRow["valuestring"].ToString() + "'";
                                    }
                                    else
                                    {
                                        mFieldValue = "'#" + mParameterCode + "#'";
                                    }
                                }
                                break;
                        }

                        #endregion

                        if (mFields.Trim() == "")
                        {
                            mFields = Environment.NewLine + " " + mFieldValue + " " + mFieldName;
                        }
                        else
                        {
                            mFields = mFields + Environment.NewLine  + ", " + mFieldValue + " " + mFieldName;
                        }
                    }
                }

                #endregion

                string mAfterSelectStr = mCommandText.Substring(mCommandText.ToLower().IndexOf("select ") + "select ".Length);
                mNewCommandText = "SELECT " + mFields + ", " + mAfterSelectStr;
                if (mNewFilterString.Trim() != "")
                {
                    mNewCommandText = mNewCommandText + " " + Environment.NewLine + "WHERE " + mNewFilterString;
                }
                if (mGroupByFields.Trim() != "")
                {
                    mNewCommandText = mNewCommandText + " " + Environment.NewLine + "GROUP BY " + mGroupByFields;
                }

                return mNewCommandText;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return "";
            }
            finally
            {
                mConn.Close();
            }
        }
        #endregion

        #region Get_PrescribedPatients
        public DataTable Get_PrescribedPatients(DateTime mDateFrom, DateTime mDateTo, Int16 mPrintStatus, string mPatientCode, string mGridName)
        {
            String mFunctionName = "Get_PrescribedPatients";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                string mCommandText = ""
                    + "SELECT b.* FROM view_dxtprescribedpatients AS b WHERE "
                    + "transdate between " + clsGlobal.Saving_DateValue(mDateFrom) + " and " + clsGlobal.Saving_DateValue(mDateTo)
                    + " AND printed=" + mPrintStatus;

                if (mPatientCode.Trim() != "")
                {
                    mCommandText = mCommandText + " and patientcode='" + mPatientCode.Trim() + "'";
                }

                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_dxtprescribedpatients");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                mDataTable.Columns.Add("selected", typeof(System.Boolean));
                foreach (DataRow mDataRow in mDataTable.Rows)
                {
                    mDataRow.BeginEdit();
                    mDataRow["selected"] = false;
                    mDataRow.EndEdit();
                }

                mDataTable.AcceptChanges();

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mGridName.Trim());

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

        #region Update_PrintedPrescription
        public AfyaPro_Types.clsResult Update_PrintedPrescription(
            DataTable mDtPrinted,
            string mUserId)
        {
            String mFunctionName = "Update_PrintedPrescription";
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

            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                #region dxtpatientprescriptions

                foreach (DataRow mDataRow in mDtPrinted.Rows)
                {
                    List<clsDataField> mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("printed", DataTypes.dbnumber.ToString(), 1));

                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("dxtpatientprescriptions", mDataFields,
                        "patientcode='" + mDataRow["patientcode"].ToString().Trim() + "' and transdate="
                        + clsGlobal.Saving_DateValue(Convert.ToDateTime(mDataRow["transdate"])));
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
        }
        #endregion

        #region registrations

        #region REG_PatientDetails
        public DataTable REG_PatientDetails(DataTable mDtStaticInfo, string mFilter, string mOrder)
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            string mFunctionName = "REG_PatientDetails";

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
                DataTable mDtData = new DataTable("data");

                string mFields = "";

                #region add static info

                if (mDtStaticInfo != null)
                {
                    foreach (DataRow mDataRow in mDtStaticInfo.Rows)
                    {
                        string mFieldName = "ctr_" + mDataRow["fieldname"].ToString().Trim().ToLower();
                        string mFieldValue = "";

                        #region build field value

                        switch (mDataRow["datatype"].ToString().Trim().ToLower())
                        {
                            case "system.decimal":
                                {
                                    mFieldValue = Convert.ToDouble(mDataRow["fieldvalue"]).ToString();
                                    mDtData.Columns.Add(mFieldName, typeof(System.Double));
                                }
                                break;
                            case "system.double":
                                {
                                    mFieldValue = Convert.ToDouble(mDataRow["fieldvalue"]).ToString();
                                    mDtData.Columns.Add(mFieldName, typeof(System.Double));
                                }
                                break;
                            case "system.int16":
                                {
                                    mFieldValue = Convert.ToInt16(mDataRow["fieldvalue"]).ToString();
                                    mDtData.Columns.Add(mFieldName, typeof(System.Int16));
                                }
                                break;
                            case "system.int32":
                                {
                                    mFieldValue = Convert.ToInt32(mDataRow["fieldvalue"]).ToString();
                                    mDtData.Columns.Add(mFieldName, typeof(System.Int32));
                                }
                                break;
                            case "system.int64":
                                {
                                    mFieldValue = Convert.ToInt64(mDataRow["fieldvalue"]).ToString();
                                    mDtData.Columns.Add(mFieldName, typeof(System.Int64));
                                }
                                break;
                            case "system.single":
                                {
                                    mFieldValue = Convert.ToSingle(mDataRow["fieldvalue"]).ToString();
                                    mDtData.Columns.Add(mFieldName, typeof(System.Single));
                                }
                                break;
                            case "system.datetime":
                                {
                                    mFieldValue = "'" + Convert.ToSingle(mDataRow["fieldvalue"]).ToString() + "'";
                                    mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                                }
                                break;
                            default:
                                {
                                    mFieldValue = "'" + mDataRow["fieldvalue"].ToString() + "'";
                                    mDtData.Columns.Add(mFieldName, typeof(System.String));
                                }
                                break;
                        }

                        #endregion

                        if (mFields.Trim() == "")
                        {
                            mFields = mFieldValue + " " + mFieldName;
                        }
                        else
                        {
                            mFields = mFields + "," + mFieldValue + " " + mFieldName;
                        }
                    }
                }

                #endregion

                #region add columns from the view

                mCommand.CommandText = "select * from patients where 1=2";
                DataTable mDtColumns = new DataTable("tableschema");
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.FillSchema(mDtColumns, SchemaType.Source);

                foreach (DataColumn mDataColumn in mDtColumns.Columns)
                {
                    if (mDataColumn.ColumnName.ToLower() != "autocode")
                    {
                        if (mFields.Trim() == "")
                        {
                            mFields = mDataColumn.ColumnName;
                        }
                        else
                        {
                            mFields = mFields + "," + mDataColumn.ColumnName;
                        }
                    }
                }

                //add fullname
                mFields = mFields + "," + clsGlobal.Concat_Fields("firstname,' ',othernames,' ',surname", "fullname");

                //add patientage
                mFields = mFields + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("birthdate", "regdate", ""),"patientage");

                #endregion

                string mCommandText = "select " + mFields + " from patients";
                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where (" + mFilter + ")";
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);
                mDtData.RemotingFormat = SerializationFormat.Binary;

                return mDtData;
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

        #region REG_AttendanceList
        public DataTable REG_AttendanceList(DateTime mDateFrom, DateTime mDateTo, bool mDateBased, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "REG_AttendanceList";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                string mFieldName = "";
                string mFieldValue = "";
                string mCommandText = "";
                DataTable mDtData = new DataTable("data");

                string mFields = "";

                #region facilitysetup

                DataTable mDtFacilityOptions = new DataTable("facilityoptions");
                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilityOptions);

                if (mDtFacilityOptions.Rows.Count > 0)
                {
                    foreach (DataColumn mDataColumn in mDtFacilityOptions.Columns)
                    {
                        if (mDataColumn.ColumnName.ToLower() != "autocode")
                        {
                            mFieldName = "facility_" + mDataColumn.ColumnName;
                            mFieldValue = "";

                            #region build fieldvalue

                            switch (mDataColumn.DataType.FullName.Trim().ToLower())
                            {
                                case "system.decimal":
                                    {
                                        mFieldValue = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Double));
                                    }
                                    break;
                                case "system.double":
                                    {
                                        mFieldValue = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Double));
                                    }
                                    break;
                                case "system.int16":
                                    {
                                        mFieldValue = Convert.ToInt16(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int16));
                                    }
                                    break;
                                case "system.int32":
                                    {
                                        mFieldValue = Convert.ToInt32(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int32));
                                    }
                                    break;
                                case "system.int64":
                                    {
                                        mFieldValue = Convert.ToInt64(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int64));
                                    }
                                    break;
                                case "system.single":
                                    {
                                        mFieldValue = Convert.ToSingle(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Single));
                                    }
                                    break;
                                case "system.datetime":
                                    {
                                        mFieldValue = clsGlobal.Saving_DateValue(Convert.ToDateTime(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]));
                                        mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                                    }
                                    break;
                                default:
                                    {
                                        mFieldValue = "'" + mDtFacilityOptions.Rows[0][mDataColumn.ColumnName].ToString() + "'";
                                        mDtData.Columns.Add(mFieldName, typeof(System.String));
                                    }
                                    break;
                            }
                            #endregion

                            if (mFields.Trim() == "")
                            {
                                mFields = Environment.NewLine + " " + mFieldValue + " " + mFieldName;
                            }
                            else
                            {
                                mFields = mFields + Environment.NewLine + ", " + mFieldValue + " " + mFieldName;
                            }
                        }
                    }
                }

                #endregion

                #region add parameters used

                #region datefrom
                mFieldName = "para_datefrom";
                mFieldValue = clsGlobal.Saving_DateValue(mDateFrom);
                mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                if (mFields.Trim() == "")
                {
                    mFields = mFieldValue + " " + mFieldName;
                }
                else
                {
                    mFields = mFields + "," + mFieldValue + " " + mFieldName;
                }
                #endregion

                #region dateto
                mFieldName = "para_dateto";
                mFieldValue = clsGlobal.Saving_DateValue(mDateTo);
                mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                if (mFields.Trim() == "")
                {
                    mFields = mFieldValue + " " + mFieldName;
                }
                else
                {
                    mFields = mFields + "," + mFieldValue + " " + mFieldName;
                }
                #endregion

                #region extra parameters
                mFieldName = "para_otherparameters";
                mFieldValue = "'" + mExtraParameters + "'";
                mDtData.Columns.Add(mFieldName, typeof(System.String));
                if (mFields.Trim() == "")
                {
                    mFields = mFieldValue + " " + mFieldName;
                }
                else
                {
                    mFields = mFields + "," + mFieldValue + " " + mFieldName;
                }
                #endregion

                #endregion

                //columns from patients
                mFields = mFields + "," + clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "patient");
                mFields = mFields + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "patientfullname");
                mFields = mFields + "," + clsGlobal.Age_Formula("p.birthdate", "b.bookdate", "patientage");
                mFields = mFields + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "b.bookdate", ""), "patientagedisplay");
                //columns from trans
                mFields = mFields + "," + clsGlobal.Get_TableColumns(mCommand, "view_facilitybookinglog", "", "b", "");

                mCommandText = ""
                    + "SELECT "
                    + mFields + " "
                    + "FROM view_facilitybookinglog AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                    + "WHERE (b.bookdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ")";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " AND (" + mExtraFilter + ")";
                }

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);
                mDtData.RemotingFormat = SerializationFormat.Binary;

                return mDtData;
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

        #region REG_AttendanceCountAge
        public DataTable REG_AttendanceCountAge(DateTime mDateFrom, DateTime mDateTo, bool mDateBased, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "REG_AttendanceCountAge";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                string mFieldName = "";
                string mCommandText = "";
                DataTable mDtData = new DataTable("data");

                #region facilitysetup columns

                DataTable mDtFacilityOptions = new DataTable("facilityoptions");
                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilityOptions);

                if (mDtFacilityOptions.Rows.Count > 0)
                {
                    foreach (DataColumn mDataColumn in mDtFacilityOptions.Columns)
                    {
                        if (mDataColumn.ColumnName.ToLower() != "autocode")
                        {
                            mFieldName = "facility_" + mDataColumn.ColumnName;

                            #region add column names

                            switch (mDataColumn.DataType.FullName.Trim().ToLower())
                            {
                                case "system.decimal":
                                    {
                                        mDtData.Columns.Add(mFieldName, typeof(System.Double));
                                    }
                                    break;
                                case "system.double":
                                    {
                                        mDtData.Columns.Add(mFieldName, typeof(System.Double));
                                    }
                                    break;
                                case "system.int16":
                                    {
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int16));
                                    }
                                    break;
                                case "system.int32":
                                    {
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int32));
                                    }
                                    break;
                                case "system.int64":
                                    {
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int64));
                                    }
                                    break;
                                case "system.single":
                                    {
                                        mDtData.Columns.Add(mFieldName, typeof(System.Single));
                                    }
                                    break;
                                case "system.datetime":
                                    {
                                        mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                                    }
                                    break;
                                default:
                                    {
                                        mDtData.Columns.Add(mFieldName, typeof(System.String));
                                    }
                                    break;
                            }
                            #endregion
                        }
                    }
                }

                #endregion

                #region add parameters columns

                #region datefrom
                mFieldName = "para_datefrom";
                mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                #endregion

                #region dateto
                mFieldName = "para_dateto";
                mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                #endregion

                #region extra parameters
                mFieldName = "para_otherparameters";
                mDtData.Columns.Add(mFieldName, typeof(System.String));
                #endregion

                #endregion

                mDtData.Columns.Add("bardescription", typeof(System.String));
                mDtData.Columns.Add("newfemales", typeof(System.Double));
                mDtData.Columns.Add("newmales", typeof(System.Double));
                mDtData.Columns.Add("reattfemales", typeof(System.Double));
                mDtData.Columns.Add("reattmales", typeof(System.Double));

                #region generate data for the chart

                string mPatientAge = clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "b.bookdate",""), "patientage");

                mCommandText = ""
                    + "SELECT "
                    + "b.registrystatus,"
                    + mPatientAge + ","
                    + "p.gender AS patientgender "
                    + "FROM view_facilitybookinglog AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                    + "WHERE (b.bookdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ")";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }
                DataTable mDtBooking = new DataTable("bookinglog");
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtBooking);

                DataView mDvBooking = new DataView();
                mDvBooking.Table = mDtBooking;

                mCommand.CommandText = "select * from sys_reportcharts where reportcode='"
                + "REP" + Convert.ToInt16(AfyaPro_Types.clsEnums.BuiltInReports.AttendanceCountAge) + "' order by autocode";
                DataTable mDtControlData = new DataTable("controldata");
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtControlData);

                foreach (DataRow mDataRow in mDtControlData.Rows)
                {
                    int mNewFemales = 0;
                    int mNewMales = 0;
                    int mReAttFemales = 0;
                    int mReAttMales = 0;

                    if (Convert.ToDouble(mDataRow["value2"]) != -1)
                    {
                        #region new
                        //females
                        mDvBooking.RowFilter = "patientage>=" + Convert.ToDouble(mDataRow["value1"])
                        + " and patientage<" + Convert.ToDouble(mDataRow["value2"]) + " and patientgender='f' and registrystatus='new'";
                        mNewFemales = mDvBooking.Count;
                        //males
                        mDvBooking.RowFilter = "patientage>=" + Convert.ToDouble(mDataRow["value1"])
                        + " and patientage<" + Convert.ToDouble(mDataRow["value2"]) + " and patientgender='m' and registrystatus='new'";
                        mNewMales = mDvBooking.Count;
                        #endregion

                        #region re_visiting
                        //females
                        mDvBooking.RowFilter = "patientage>=" + Convert.ToDouble(mDataRow["value1"])
                        + " and patientage<" + Convert.ToDouble(mDataRow["value2"]) + " and patientgender='f' and registrystatus='re_visiting'";
                        mReAttFemales = mDvBooking.Count;
                        //males
                        mDvBooking.RowFilter = "patientage>=" + Convert.ToDouble(mDataRow["value1"])
                        + " and patientage<" + Convert.ToDouble(mDataRow["value2"]) + " and patientgender='m' and registrystatus='re_visiting'";
                        mReAttMales = mDvBooking.Count;
                        #endregion
                    }
                    else
                    {
                        #region new
                        //females
                        mDvBooking.RowFilter = "patientage>=" + Convert.ToDouble(mDataRow["value1"]) + " and patientgender='f' and registrystatus='new'";
                        mNewFemales = mDvBooking.Count;
                        //males
                        mDvBooking.RowFilter = "patientage>=" + Convert.ToDouble(mDataRow["value1"]) + " and patientgender='m' and registrystatus='new'";
                        mNewMales = mDvBooking.Count;
                        #endregion

                        #region re_visiting
                        //females
                        mDvBooking.RowFilter = "patientage>=" + Convert.ToDouble(mDataRow["value1"]) + " and patientgender='f' and registrystatus='re_visiting'";
                        mReAttFemales = mDvBooking.Count;
                        //males
                        mDvBooking.RowFilter = "patientage>=" + Convert.ToDouble(mDataRow["value1"]) + " and patientgender='m' and registrystatus='re_visiting'";
                        mReAttMales = mDvBooking.Count;
                        #endregion
                    }

                    DataRow mNewRow = mDtData.NewRow();

                    #region facilitysetup

                    if (mDtFacilityOptions.Rows.Count > 0)
                    {
                        foreach (DataColumn mDataColumn in mDtFacilityOptions.Columns)
                        {
                            if (mDataColumn.ColumnName.ToLower() != "autocode")
                            {
                                mFieldName = "facility_" + mDataColumn.ColumnName;

                                switch (mDataColumn.DataType.FullName.Trim().ToLower())
                                {
                                    case "system.decimal":
                                        {
                                            mNewRow[mFieldName] = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        }
                                        break;
                                    case "system.double":
                                        {
                                            mNewRow[mFieldName] = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        }
                                        break;
                                    case "system.int16":
                                        {
                                            mNewRow[mFieldName] = Convert.ToInt16(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        }
                                        break;
                                    case "system.int32":
                                        {
                                            mNewRow[mFieldName] = Convert.ToInt32(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        }
                                        break;
                                    case "system.int64":
                                        {
                                            mNewRow[mFieldName] = Convert.ToInt64(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        }
                                        break;
                                    case "system.single":
                                        {
                                            mNewRow[mFieldName] = Convert.ToSingle(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        }
                                        break;
                                    case "system.datetime":
                                        {
                                            mNewRow[mFieldName] = Convert.ToDateTime(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).Date;
                                        }
                                        break;
                                    default:
                                        {
                                            mNewRow[mFieldName] = mDtFacilityOptions.Rows[0][mDataColumn.ColumnName].ToString();
                                        }
                                        break;
                                }
                            }
                        }
                    }

                    #endregion

                    mNewRow["para_datefrom"] = mDateFrom;
                    mNewRow["para_dateto"] = mDateTo;
                    mNewRow["para_otherparameters"] = mExtraParameters;
                    mNewRow["bardescription"] = mDataRow["description"].ToString().Trim();
                    mNewRow["newfemales"] = mNewFemales;
                    mNewRow["newmales"] = mNewMales;
                    mNewRow["reattfemales"] = mReAttFemales;
                    mNewRow["reattmales"] = mReAttMales;
                    mDtData.Rows.Add(mNewRow);
                    mDtData.AcceptChanges();
                }

                #endregion

                mDtData.RemotingFormat = SerializationFormat.Binary;

                return mDtData;
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

        #region REG_AttendanceCountTreatmentPoints
        public DataSet REG_AttendanceCountTreatmentPoints(DateTime mDateFrom, DateTime mDateTo, bool mDateBased, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "REG_AttendanceCountTreatmentPoints";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                string mCommandText = "";
                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtBooking = new DataTable("bookinglog");

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_datefrom", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_dateto", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));
                mDtFacilitySetup.Columns.Add("keyvalue", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_datefrom"] = mDateFrom;
                    mDataRow["para_dateto"] = mDateTo;
                    mDataRow["para_otherparameters"] = mExtraParameters;
                    mDataRow["keyvalue"] = "parentlink";

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region generate data

                mCommandText = "SELECT "
                    + "'parentlink' keyvalue,b.wheretakencode,b.wheretaken,"
                    + "sum(case when p.gender='m' and b.registrystatus='new' then 1 else 0 end) newmales,"
                    + "sum(case when p.gender='f' and b.registrystatus='new' then 1 else 0 end) newfemales,"
                    + "sum(case when p.gender='m' and b.registrystatus='re_visiting' then 1 else 0 end) reattmales,"
                    + "sum(case when p.gender='f' and b.registrystatus='re_visiting' then 1 else 0 end) reattfemales "
                    + "FROM view_facilitybookinglog AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                    + "WHERE (b.bookdate between " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ")";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }

                mCommandText = mCommandText + " group by b.wheretakencode,b.wheretaken";
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtBooking);

                #endregion

                DataSet mDsData = new DataSet("cashbox");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtBooking);
                mDsData.Relations.Add("childrelationship", mDtFacilitySetup.Columns["keyvalue"], mDtBooking.Columns["keyvalue"]);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #region REG_AttendanceCountCustomerGroups
        public DataSet REG_AttendanceCountCustomerGroups(DateTime mDateFrom, DateTime mDateTo, bool mDateBased, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "REG_AttendanceCountCustomerGroups";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                string mCommandText = "";
                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtBooking = new DataTable("bookinglog");

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_datefrom", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_dateto", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));
                mDtFacilitySetup.Columns.Add("keyvalue", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_datefrom"] = mDateFrom;
                    mDataRow["para_dateto"] = mDateTo;
                    mDataRow["para_otherparameters"] = mExtraParameters;
                    mDataRow["keyvalue"] = "parentlink";

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region generate data

                mCommandText = "SELECT "
                    + "'parentlink' keyvalue,b.billinggroupcode,b.billinggroup,"
                    + "sum(case when p.gender='m' and b.registrystatus='new' then 1 else 0 end) newmales,"
                    + "sum(case when p.gender='f' and b.registrystatus='new' then 1 else 0 end) newfemales,"
                    + "sum(case when p.gender='m' and b.registrystatus='re_visiting' then 1 else 0 end) reattmales,"
                    + "sum(case when p.gender='f' and b.registrystatus='re_visiting' then 1 else 0 end) reattfemales "
                    + "FROM view_facilitybookinglog AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                    + "WHERE (b.bookdate between " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ")";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }

                mCommandText = mCommandText + " group by billinggroupcode,billinggroup";
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtBooking);

                #endregion

                DataSet mDsData = new DataSet("cashbox");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtBooking);
                mDsData.Relations.Add("childrelationship", mDtFacilitySetup.Columns["keyvalue"], mDtBooking.Columns["keyvalue"]);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #region REG_IPDDailyCensusSummaryWards
        public DataSet REG_IPDDailyCensusSummaryWards(DateTime mDateFrom, DateTime mDateTo, bool mDateBased, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "REG_IPDDailyCensusSummaryWards";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                string mCommandText = "";
                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtSummary = new DataTable("summary");
                DataTable mDtWards = new DataTable("wards");
                DataTable mDtAdmissions = new DataTable("admissions");
                DataTable mDtDischarges = new DataTable("discharges");
                DataTable mDtTransIn = new DataTable("transin");
                DataTable mDtTransOut = new DataTable("transout");
                DataTable mDtDeaths = new DataTable("deaths");
                DataTable mDtAbs = new DataTable("abs");
                DataTable mDtDays = new DataTable("days");
                DataTable mDtNoOfBeds = new DataTable("noofbeds");

                mDtSummary.Columns.Add("keyvalue", typeof(System.String));
                mDtSummary.Columns.Add("wardcode", typeof(System.String));
                mDtSummary.Columns.Add("warddescription", typeof(System.String));
                mDtSummary.Columns.Add("adminmale", typeof(System.Int32));
                mDtSummary.Columns.Add("adminfemale", typeof(System.Int32));
                mDtSummary.Columns.Add("dischmale", typeof(System.Int32));
                mDtSummary.Columns.Add("dischfemale", typeof(System.Int32));
                mDtSummary.Columns.Add("transinmale", typeof(System.Int32));
                mDtSummary.Columns.Add("transinfemale", typeof(System.Int32));
                mDtSummary.Columns.Add("transoutmale", typeof(System.Int32));
                mDtSummary.Columns.Add("transoutfemale", typeof(System.Int32));
                mDtSummary.Columns.Add("deathmale", typeof(System.Int32));
                mDtSummary.Columns.Add("deathfemale", typeof(System.Int32));
                mDtSummary.Columns.Add("abscmale", typeof(System.Int32));
                mDtSummary.Columns.Add("abscfemale", typeof(System.Int32));
                mDtSummary.Columns.Add("daysmale", typeof(System.Double));
                mDtSummary.Columns.Add("daysfemale", typeof(System.Double));
                mDtSummary.Columns.Add("beds", typeof(System.Double));

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_datefrom", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_dateto", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));
                mDtFacilitySetup.Columns.Add("keyvalue", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_datefrom"] = mDateFrom;
                    mDataRow["para_dateto"] = mDateTo;
                    mDataRow["para_otherparameters"] = mExtraParameters;
                    mDataRow["keyvalue"] = "parentlink";

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region admissions

                mCommandText = "SELECT "
                    + "wardcode,"
                    + "sum(case when p.gender='m' then 1 else 0 end) males,"
                    + "sum(case when p.gender='f' then 1 else 0 end) females "
                    + "FROM ipdadmissionslog AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                    + "WHERE (b.transdate between " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                    + "AND b.transcode='" + AfyaPro_Types.clsEnums.IPDTransTypes.Admission.ToString() + "'";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }

                mCommandText = mCommandText + " group by b.wardcode";
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtAdmissions);

                #endregion

                #region discharges

                mCommandText = "SELECT "
                    + "wardcode,"
                    + "sum(case when p.gender='m' then 1 else 0 end) males,"
                    + "sum(case when p.gender='f' then 1 else 0 end) females "
                    + "FROM ipddischargeslog AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                    + "WHERE (transdate between " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                    + "AND dischargestatuscode<>'" + AfyaPro_Types.clsEnums.IPDDischargeStatus.Death.ToString() + "' "
                    + "AND dischargestatuscode<>'" + AfyaPro_Types.clsEnums.IPDDischargeStatus.Abscondee.ToString() + "' "
                    + "AND dischargestatuscode<>'' "
                    + "AND transcode<>'" + AfyaPro_Types.clsEnums.IPDTransTypes.Transfer.ToString() + "'";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }

                mCommandText = mCommandText + " group by wardcode";
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtDischarges);

                #endregion

                #region transfersin

                mCommandText = "SELECT "
                    + "wardtocode,"
                    + "sum(case when p.gender='m' then 1 else 0 end) males,"
                    + "sum(case when p.gender='f' then 1 else 0 end) females "
                    + "FROM ipdtransferslog AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                    + "WHERE (transferdate between " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ")";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }

                mCommandText = mCommandText + " group by wardtocode";
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtTransIn);

                #endregion

                #region transfersout

                mCommandText = "SELECT "
                    + "wardfromcode,"
                    + "sum(case when p.gender='m' then 1 else 0 end) males,"
                    + "sum(case when p.gender='f' then 1 else 0 end) females "
                    + "FROM ipdtransferslog AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                    + "WHERE (transferdate between " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ")";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }

                mCommandText = mCommandText + " group by wardfromcode";
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtTransOut);

                #endregion

                #region deaths

                mCommandText = "SELECT "
                    + "wardcode,"
                    + "sum(case when p.gender='m' then 1 else 0 end) males,"
                    + "sum(case when p.gender='f' then 1 else 0 end) females "
                    + "FROM ipddischargeslog AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                    + "WHERE (transdate between " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                    + "AND dischargestatuscode='" + AfyaPro_Types.clsEnums.IPDDischargeStatus.Death.ToString() + "' "
                    + "AND transcode<>'" + AfyaPro_Types.clsEnums.IPDTransTypes.Transfer.ToString() + "'";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }

                mCommandText = mCommandText + " group by wardcode";
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtDeaths);

                #endregion

                #region abscondee

                mCommandText = "SELECT "
                    + "wardcode,"
                    + "sum(case when p.gender='m' then 1 else 0 end) males,"
                    + "sum(case when p.gender='f' then 1 else 0 end) females "
                    + "FROM ipddischargeslog AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                    + "WHERE (transdate between " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                    + "AND dischargestatuscode='" + AfyaPro_Types.clsEnums.IPDDischargeStatus.Abscondee.ToString() + "' "
                    + "AND transcode<>'" + AfyaPro_Types.clsEnums.IPDTransTypes.Transfer.ToString() + "'";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }

                mCommandText = mCommandText + " group by wardcode";
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtAbs);

                #endregion

                #region patientdays and bed occupancy

                mCommandText = "SELECT "
                    + "ds.wardcode,"
                    + "ad.transdate admissionstart,"
                    + "ds.transdate admissionstop,"
                    + "sum(case when p.gender='m' then 1 else 0 end) males,"
                    + "sum(case when p.gender='f' then 1 else 0 end) females "
                    + "FROM ipddischargeslog ds "
                    + "LEFT OUTER JOIN ipdadmissionslog ad ON ds.admissionid=ad.autocode "
                    + "LEFT OUTER JOIN patients AS p ON ds.patientcode=p.code "
                    + "WHERE (ds.transdate between " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                    + "AND ad.transdate IS NOT NULL AND ds.transdate IS NOT NULL";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }

                mCommandText = mCommandText + " group by ds.wardcode,ad.transdate,ds.transdate";
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtDays);

                #endregion

                #region number of beds

                mCommandText = "SELECT wardcode,COUNT(CODE) beds FROM facilitywardroombeds WHERE regdate<="
                + clsGlobal.Saving_DateValue(mDateTo) + " GROUP BY wardcode";

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtNoOfBeds);

                #endregion

                mCommand.CommandText = "select * from facilitywards";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtWards);

                foreach (DataRow mDataRow in mDtWards.Rows)
                {
                    int mRowIndex = -1;
                    int mAdminMale = 0;
                    int mAdminFemale = 0;
                    int mDischMale = 0;
                    int mDischFemale = 0;
                    int mTransInMale = 0;
                    int mTransInFemale = 0;
                    int mTransOutMale = 0;
                    int mTransOutFemale = 0;
                    int mDeathMale = 0;
                    int mDeathFemale = 0;
                    int mAbscMale = 0;
                    int mAbscFemale = 0;
                    double mDaysMale = 0;
                    double mDaysFemale = 0;
                    int mBeds = 1;

                    DataView mDataView;

                    #region admissions
                    mDataView = new DataView();
                    mDataView.Table = mDtAdmissions;
                    mDataView.Sort = "wardcode";
                    mRowIndex = mDataView.Find(mDataRow["code"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mAdminMale = mDataView[mRowIndex]["males"] == null ? 0 : Convert.ToInt32(mDataView[mRowIndex]["males"]);
                        mAdminFemale = mDataView[mRowIndex]["females"] == null ? 0 : Convert.ToInt32(mDataView[mRowIndex]["females"]);
                    }
                    #endregion

                    #region discharges
                    mDataView = new DataView();
                    mDataView.Table = mDtDischarges;
                    mDataView.Sort = "wardcode";
                    mRowIndex = mDataView.Find(mDataRow["code"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mDischMale = mDataView[mRowIndex]["males"] == null ? 0 : Convert.ToInt32(mDataView[mRowIndex]["males"]);
                        mDischFemale = mDataView[mRowIndex]["females"] == null ? 0 : Convert.ToInt32(mDataView[mRowIndex]["females"]);
                    }
                    #endregion

                    #region transfersin
                    mDataView = new DataView();
                    mDataView.Table = mDtTransIn;
                    mDataView.Sort = "wardtocode";
                    mRowIndex = mDataView.Find(mDataRow["code"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mTransInMale = mDataView[mRowIndex]["males"] == null ? 0 : Convert.ToInt32(mDataView[mRowIndex]["males"]);
                        mTransInFemale = mDataView[mRowIndex]["females"] == null ? 0 : Convert.ToInt32(mDataView[mRowIndex]["females"]);
                    }
                    #endregion

                    #region transfersout
                    mDataView = new DataView();
                    mDataView.Table = mDtTransOut;
                    mDataView.Sort = "wardfromcode";
                    mRowIndex = mDataView.Find(mDataRow["code"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mTransOutMale = mDataView[mRowIndex]["males"] == null ? 0 : Convert.ToInt32(mDataView[mRowIndex]["males"]);
                        mTransOutFemale = mDataView[mRowIndex]["females"] == null ? 0 : Convert.ToInt32(mDataView[mRowIndex]["females"]);
                    }
                    #endregion

                    #region deaths
                    mDataView = new DataView();
                    mDataView.Table = mDtDeaths;
                    mDataView.Sort = "wardcode";
                    mRowIndex = mDataView.Find(mDataRow["code"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mDeathMale = mDataView[mRowIndex]["males"] == null ? 0 : Convert.ToInt32(mDataView[mRowIndex]["males"]);
                        mDeathFemale = mDataView[mRowIndex]["females"] == null ? 0 : Convert.ToInt32(mDataView[mRowIndex]["females"]);
                    }
                    #endregion

                    #region abscondee
                    mDataView = new DataView();
                    mDataView.Table = mDtAbs;
                    mDataView.Sort = "wardcode";
                    mRowIndex = mDataView.Find(mDataRow["code"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mAbscMale = mDataView[mRowIndex]["males"] == null ? 0 : Convert.ToInt32(mDataView[mRowIndex]["males"]);
                        mAbscFemale = mDataView[mRowIndex]["females"] == null ? 0 : Convert.ToInt32(mDataView[mRowIndex]["females"]);
                    }
                    #endregion

                    #region patientdays
                    mDataView = new DataView();
                    mDataView.Table = mDtDays;
                    mDataView.RowFilter = "wardcode='" + mDataRow["code"].ToString().Trim() + "'";
                    foreach (DataRowView mDataRowView in mDataView)
                    {
                        int mCurrDaysMale = 0;
                        int mCurrDaysFemale = 0;

                        if (Convert.ToDateTime(mDataRowView["admissionstart"]).Date ==
                            Convert.ToDateTime(mDataRowView["admissionstop"]).Date)
                        {
                            mCurrDaysMale = mDataRowView["males"] == null ? 0 : Convert.ToInt32(mDataRowView["males"]);
                            mCurrDaysFemale = mDataRowView["females"] == null ? 0 : Convert.ToInt32(mDataRowView["females"]);
                        }
                        else
                        {
                            TimeSpan mTimeSpan = Convert.ToDateTime(mDataRowView["admissionstop"]).Subtract(
                                Convert.ToDateTime(mDataRowView["admissionstart"]));

                            int mMales = mDataRowView["males"] == null ? 0 : Convert.ToInt32(mDataRowView["males"]);
                            int mFeMales = mDataRowView["females"] == null ? 0 : Convert.ToInt32(mDataRowView["females"]);

                            mCurrDaysMale = mTimeSpan.Days * mMales;
                            mCurrDaysFemale = mTimeSpan.Days * mFeMales;
                        }

                        mDaysMale = mDaysMale + mCurrDaysMale;
                        mDaysFemale = mDaysFemale + mCurrDaysFemale;
                    }
                    #endregion

                    #region beds
                    mDataView = new DataView();
                    mDataView.Table = mDtNoOfBeds;
                    mDataView.Sort = "wardcode";
                    mRowIndex = mDataView.Find(mDataRow["code"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mBeds = mDataView[mRowIndex]["beds"] == null ? 0 : Convert.ToInt32(mDataView[mRowIndex]["beds"]);
                    }

                    #endregion

                    DataRow mNewRow = mDtSummary.NewRow();
                    mNewRow["keyvalue"] = "parentlink";
                    mNewRow["wardcode"] = mDataRow["code"].ToString().Trim();
                    mNewRow["warddescription"] = mDataRow["description"].ToString().Trim();
                    mNewRow["adminmale"] = mAdminMale;
                    mNewRow["adminfemale"] = mAdminFemale;
                    mNewRow["dischmale"] = mDischMale;
                    mNewRow["dischfemale"] = mDischFemale;
                    mNewRow["transinmale"] = mTransInMale;
                    mNewRow["transinfemale"] = mTransInFemale;
                    mNewRow["transoutmale"] = mTransOutMale;
                    mNewRow["transoutfemale"] = mTransOutFemale;
                    mNewRow["deathmale"] = mDeathMale;
                    mNewRow["deathfemale"] = mDeathFemale;
                    mNewRow["abscmale"] = mAbscMale;
                    mNewRow["abscfemale"] = mAbscFemale;
                    mNewRow["daysmale"] = mDaysMale;
                    mNewRow["daysfemale"] = mDaysFemale;
                    mNewRow["beds"] = mBeds;
                    mDtSummary.Rows.Add(mNewRow);
                    mDtSummary.AcceptChanges();
                }

                DataSet mDsData = new DataSet("data");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtSummary);
                mDsData.Relations.Add("childrelationship", mDtFacilitySetup.Columns["keyvalue"], mDtSummary.Columns["keyvalue"]);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #region REG_IPDDailyCensusDetailedWards
        public DataSet REG_IPDDailyCensusDetailedWards(DateTime mDateFrom, DateTime mDateTo, bool mDateBased, string mWardCode,
            string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "REG_IPDDailyCensusDetailedWards";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                string mFields = "";
                string mCommandText = "";
                DataTable mDtColumns = new DataTable("tableschema");
                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtSummary = new DataTable("summary");
                DataTable mDtWards = new DataTable("wards");
                DataTable mDtAdmissions = new DataTable("admissions");
                DataTable mDtDischarges = new DataTable("discharges");
                DataTable mDtTransIn = new DataTable("transin");
                DataTable mDtTransOut = new DataTable("transout");
                DataTable mDtDeaths = new DataTable("deaths");
                DataTable mDtAbs = new DataTable("abs");
                DataTable mDtBF = new DataTable("bf");

                mDtSummary.Columns.Add("keyvalue", typeof(System.String));
                mDtSummary.Columns.Add("adminmale", typeof(System.Int32));
                mDtSummary.Columns.Add("adminfemale", typeof(System.Int32));
                mDtSummary.Columns.Add("dischmale", typeof(System.Int32));
                mDtSummary.Columns.Add("dischfemale", typeof(System.Int32));
                mDtSummary.Columns.Add("transinmale", typeof(System.Int32));
                mDtSummary.Columns.Add("transinfemale", typeof(System.Int32));
                mDtSummary.Columns.Add("transoutmale", typeof(System.Int32));
                mDtSummary.Columns.Add("transoutfemale", typeof(System.Int32));
                mDtSummary.Columns.Add("deathmale", typeof(System.Int32));
                mDtSummary.Columns.Add("deathfemale", typeof(System.Int32));
                mDtSummary.Columns.Add("abscmale", typeof(System.Int32));
                mDtSummary.Columns.Add("abscfemale", typeof(System.Int32));
                mDtSummary.Columns.Add("bfmale", typeof(System.Int32));
                mDtSummary.Columns.Add("bffemale", typeof(System.Int32));

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_datefrom", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_dateto", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));
                mDtFacilitySetup.Columns.Add("keyvalue", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_datefrom"] = mDateFrom;
                    mDataRow["para_dateto"] = mDateTo;
                    mDataRow["para_otherparameters"] = mExtraParameters;
                    mDataRow["keyvalue"] = "parentlink";

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region admissions

                mFields = "";

                //columns from patients
                mFields = clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "patient");
                mFields = mFields + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "patientfullname");
                mFields = mFields + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "b.transdate",""), "patientage");
                //columns from trans
                mFields = mFields + "," + clsGlobal.Get_TableColumns(mCommand, "view_ipdadmissionslog", "", "b", "");
                
                mCommandText = ""
                    + "SELECT "
                    + "'parentlink' keyvalue,"
                    + mFields + ","
                    + "(case when p.gender='m' then 1 else 0 end) male,"
                    + "(case when p.gender='f' then 1 else 0 end) female "
                    + "FROM view_ipdadmissionslog AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                    + "WHERE (b.transdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                    + "AND transcode='" + AfyaPro_Types.clsEnums.IPDTransTypes.Admission.ToString() + "' "
                    + "AND wardcode='" + mWardCode.Trim() + "'";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtAdmissions);

                #endregion

                #region discharges

                mFields = "";

                //columns from patients
                mFields = clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "patient");
                mFields = mFields + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "patientfullname");
                mFields = mFields + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "b.transdate",""), "patientage");
                //columns from trans
                mFields = mFields + "," + clsGlobal.Get_TableColumns(mCommand, "view_ipddischargeslog", "", "b", "");

                mCommandText = ""
                    + "SELECT "
                    + "'parentlink' keyvalue,"
                    + mFields + ","
                    + "(case when p.gender='m' then 1 else 0 end) male,"
                    + "(case when p.gender='f' then 1 else 0 end) female "
                    + "FROM view_ipddischargeslog AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                    + "WHERE (transdate between " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                    + "AND dischargestatuscode<>'" + AfyaPro_Types.clsEnums.IPDDischargeStatus.Death.ToString() + "' "
                    + "AND dischargestatuscode<>'" + AfyaPro_Types.clsEnums.IPDDischargeStatus.Abscondee.ToString() + "' "
                    + "AND dischargestatuscode<>'' "
                    + "AND wardcode='" + mWardCode.Trim() + "' "
                    + "AND transcode<>'" + AfyaPro_Types.clsEnums.IPDTransTypes.Transfer.ToString() + "'";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtDischarges);

                #endregion

                #region transfersin

                mFields = "";

                //columns from patients
                mFields = clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "patient");
                mFields = mFields + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "patientfullname");
                mFields = mFields + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "b.transferdate",""), "patientage");
                //columns from trans
                mFields = mFields + "," + clsGlobal.Get_TableColumns(mCommand, "view_ipdtransferslog", "", "b", "");

                mCommandText = ""
                    + "SELECT "
                    + "'parentlink' keyvalue,"
                    + mFields + ","
                    + "(case when p.gender='m' then 1 else 0 end) male,"
                    + "(case when p.gender='f' then 1 else 0 end) female "
                    + "FROM view_ipdtransferslog AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                    + "WHERE (b.transferdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                    + "AND wardtocode='" + mWardCode.Trim() + "'";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtTransIn);

                #endregion

                #region transfersout

                mFields = "";

                //columns from patients
                mFields = clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "patient");
                mFields = mFields + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "patientfullname");
                mFields = mFields + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "b.transferdate",""), "patientage");
                //columns from trans
                mFields = mFields + "," + clsGlobal.Get_TableColumns(mCommand, "view_ipdtransferslog", "", "b", "");

                mCommandText = ""
                    + "SELECT "
                    + "'parentlink' keyvalue,"
                    + mFields + ","
                    + "(case when p.gender='m' then 1 else 0 end) male,"
                    + "(case when p.gender='f' then 1 else 0 end) female "
                    + "FROM view_ipdtransferslog AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                    + "WHERE (b.transferdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                    + "AND wardfromcode='" + mWardCode.Trim() + "'";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtTransOut);

                #endregion

                #region deaths

                mFields = "";

                //columns from patients
                mFields = clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "patient");
                mFields = mFields + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "patientfullname");
                mFields = mFields + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "b.transdate",""), "patientage");
                //columns from trans
                mFields = mFields + "," + clsGlobal.Get_TableColumns(mCommand, "view_ipddischargeslog", "", "b", "");

                mCommandText = ""
                    + "SELECT "
                    + "'parentlink' keyvalue,"
                    + mFields + ","
                    + "(case when p.gender='m' then 1 else 0 end) male,"
                    + "(case when p.gender='f' then 1 else 0 end) female "
                    + "FROM view_ipddischargeslog AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                    + "WHERE (b.transdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                    + "AND dischargestatuscode='" + AfyaPro_Types.clsEnums.IPDDischargeStatus.Death.ToString() + "' " 
                    + "AND wardcode='" + mWardCode.Trim() + "'";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtDeaths);

                #endregion

                #region abscondee

                mFields = "";

                //columns from patients
                mFields = clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "patient");
                mFields = mFields + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "patientfullname");
                mFields = mFields + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "b.transdate",""), "patientage");
                //columns from trans
                mFields = mFields + "," + clsGlobal.Get_TableColumns(mCommand, "view_ipddischargeslog", "", "b", "");

                mCommandText = ""
                    + "SELECT "
                    + "'parentlink' keyvalue,"
                    + mFields + ","
                    + "(case when p.gender='m' then 1 else 0 end) male,"
                    + "(case when p.gender='f' then 1 else 0 end) female "
                    + "FROM view_ipddischargeslog AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                    + "WHERE (b.transdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                    + "AND dischargestatuscode='" + AfyaPro_Types.clsEnums.IPDDischargeStatus.Abscondee.ToString() + "' "
                    + "AND wardcode='" + mWardCode.Trim() + "'";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtAbs);

                #endregion

                #region summary

                int mAdminMale = 0;
                int mAdminFemale = 0;
                int mDischMale = 0;
                int mDischFemale = 0;
                int mTransInMale = 0;
                int mTransInFemale = 0;
                int mTransOutMale = 0;
                int mTransOutFemale = 0;
                int mDeathMale = 0;
                int mDeathFemale = 0;
                int mAbscMale = 0;
                int mAbscFemale = 0;
                int mBFMale = 0;
                int mBFFemale = 0;

                DataView mDataView = new DataView();

                #region admissions
                mDataView.Table = mDtAdmissions;
                mDataView.RowFilter = "male=1";
                mAdminMale = mDataView.Count;
                mDataView.RowFilter = "female=1";
                mAdminFemale = mDataView.Count;
                #endregion

                #region discharges
                mDataView.Table = mDtDischarges;
                mDataView.RowFilter = "male=1";
                mDischMale = mDataView.Count;
                mDataView.RowFilter = "female=1";
                mDischFemale = mDataView.Count;
                #endregion

                #region transfersin
                mDataView.Table = mDtTransIn;
                mDataView.RowFilter = "male=1";
                mTransInMale = mDataView.Count;
                mDataView.RowFilter = "female=1";
                mTransInFemale = mDataView.Count;
                #endregion

                #region transfersout
                mDataView.Table = mDtTransOut;
                mDataView.RowFilter = "male=1";
                mTransOutMale = mDataView.Count;
                mDataView.RowFilter = "female=1";
                mTransOutFemale = mDataView.Count;
                #endregion

                #region deaths
                mDataView.Table = mDtDeaths;
                mDataView.RowFilter = "male=1";
                mDeathMale = mDataView.Count;
                mDataView.RowFilter = "female=1";
                mDeathFemale = mDataView.Count;
                #endregion

                #region abscondee
                mDataView.Table = mDtAbs;
                mDataView.RowFilter = "male=1";
                mAbscMale = mDataView.Count;
                mDataView.RowFilter = "female=1";
                mAbscFemale = mDataView.Count;
                #endregion

                DataRow mNewRow = mDtSummary.NewRow();
                mNewRow["keyvalue"] = "parentlink";
                mNewRow["adminmale"] = mAdminMale;
                mNewRow["adminfemale"] = mAdminFemale;
                mNewRow["dischmale"] = mDischMale;
                mNewRow["dischfemale"] = mDischFemale;
                mNewRow["transinmale"] = mTransInMale;
                mNewRow["transinfemale"] = mTransInFemale;
                mNewRow["transoutmale"] = mTransOutMale;
                mNewRow["transoutfemale"] = mTransOutFemale;
                mNewRow["deathmale"] = mDeathMale;
                mNewRow["deathfemale"] = mDeathFemale;
                mNewRow["abscmale"] = mAbscMale;
                mNewRow["abscfemale"] = mAbscFemale;
                mNewRow["bfmale"] = mBFMale;
                mNewRow["bffemale"] = mBFFemale;
                mDtSummary.Rows.Add(mNewRow);
                mDtSummary.AcceptChanges();

                #endregion

                DataSet mDsData = new DataSet("data");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtAdmissions);
                mDsData.Tables.Add(mDtDischarges);
                mDsData.Tables.Add(mDtTransIn);
                mDsData.Tables.Add(mDtTransOut);
                mDsData.Tables.Add(mDtDeaths);
                mDsData.Tables.Add(mDtAbs);
                mDsData.Tables.Add(mDtSummary);
                mDsData.Relations.Add("admissionsrelationship", mDtFacilitySetup.Columns["keyvalue"], mDtAdmissions.Columns["keyvalue"]);
                mDsData.Relations.Add("dischargesrelationship", mDtFacilitySetup.Columns["keyvalue"], mDtDischarges.Columns["keyvalue"]);
                mDsData.Relations.Add("transfersinrelationship", mDtFacilitySetup.Columns["keyvalue"], mDtTransIn.Columns["keyvalue"]);
                mDsData.Relations.Add("transfersoutrelationship", mDtFacilitySetup.Columns["keyvalue"], mDtTransOut.Columns["keyvalue"]);
                mDsData.Relations.Add("deathsrelationship", mDtFacilitySetup.Columns["keyvalue"], mDtDeaths.Columns["keyvalue"]);
                mDsData.Relations.Add("abscondeerelationship", mDtFacilitySetup.Columns["keyvalue"], mDtAbs.Columns["keyvalue"]);
                mDsData.Relations.Add("summaryrelationship", mDtFacilitySetup.Columns["keyvalue"], mDtSummary.Columns["keyvalue"]);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #endregion

        #region billing

        #region BIL_DebtorStatement
        public DataTable BIL_DebtorStatement(string mAccountCode, string mAccountDescription, 
            string mDebtorType, DateTime mDateFrom, DateTime mDateTo, bool mDateBased)
        {
            string mFunctionName = "BIL_DebtorStatement";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                string mCommandText = "";
                DataTable mDtStatement = new DataTable("debtorstatement");

                #region add facilitysetup columns

                DataTable mDtFacilityOptions = new DataTable("facilityoptions");
                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilityOptions);

                foreach (DataColumn mDataColumn in mDtFacilityOptions.Columns)
                {
                    if (mDataColumn.ColumnName.ToLower() != "autocode")
                    {
                        string mFieldName = "facility_" + mDataColumn.ColumnName;

                        switch (mDataColumn.DataType.FullName.Trim().ToLower())
                        {
                            case "system.decimal":
                                {
                                    mDtStatement.Columns.Add(mFieldName, typeof(System.Double));
                                }
                                break;
                            case "system.double":
                                {
                                    mDtStatement.Columns.Add(mFieldName, typeof(System.Double));
                                }
                                break;
                            case "system.int16":
                                {
                                    mDtStatement.Columns.Add(mFieldName, typeof(System.Int16));
                                }
                                break;
                            case "system.int32":
                                {
                                    mDtStatement.Columns.Add(mFieldName, typeof(System.Int32));
                                }
                                break;
                            case "system.int64":
                                {
                                    mDtStatement.Columns.Add(mFieldName, typeof(System.Int64));
                                }
                                break;
                            case "system.single":
                                {
                                    mDtStatement.Columns.Add(mFieldName, typeof(System.Single));
                                }
                                break;
                            case "system.datetime":
                                {
                                    mDtStatement.Columns.Add(mFieldName, typeof(System.DateTime));
                                }
                                break;
                            default:
                                {
                                    mDtStatement.Columns.Add(mFieldName, typeof(System.String));
                                }
                                break;
                        }
                    }
                }

                #endregion

                mDtStatement.Columns.Add("datefrom", typeof(System.DateTime));
                mDtStatement.Columns.Add("dateto", typeof(System.DateTime));
                mDtStatement.Columns.Add("accountcode", typeof(System.String));
                mDtStatement.Columns.Add("accountdescription", typeof(System.String));
                mDtStatement.Columns.Add("transdate", typeof(System.DateTime));
                mDtStatement.Columns.Add("reference", typeof(System.String));
                mDtStatement.Columns.Add("transdescription", typeof(System.String));
                mDtStatement.Columns.Add("fromwhomtowhomcode", typeof(System.String));
                mDtStatement.Columns.Add("fromwhomtowhom", typeof(System.String));
                mDtStatement.Columns.Add("debitamount", typeof(System.Double));
                mDtStatement.Columns.Add("creditamount", typeof(System.Double));
                mDtStatement.Columns.Add("balance", typeof(System.Double));
                mDtStatement.Columns.Add("userid", typeof(System.String));

                if (mDateBased == true)
                {
                    mCommandText = "select * from billdebtorslog "
                    + "where transdate between " + clsGlobal.Saving_DateValue(mDateFrom) + " and " 
                    + clsGlobal.Saving_DateValue(mDateTo) + " and accountcode='" + mAccountCode.Trim() 
                    + "' and debtortype='" + mDebtorType.Trim() + "' order by transdate, autocode";
                }
                else
                {
                    mCommandText = "select * from billdebtorslog where accountcode='" + mAccountCode.Trim()
                    + "' and debtortype='" + mDebtorType.Trim() + "' order by transdate, autocode";
                }

                DataTable mDtData = new DataTable("reportingdata");
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);

                DataRow mNewRow;

                #region get reporting data

                if (mDateBased == true)
                {
                    double mBalance = 0;

                    #region opening balance

                    DataTable mDtBalance = new DataTable("balance");
                    mCommand.CommandText = "select accountcode,sum(debitamount)-sum(creditamount) balance from billdebtorslog "
                    + "where accountcode='" + mAccountCode.Trim() + "' and transdate<" + clsGlobal.Saving_DateValue(mDateFrom)
                    + " and debtortype='" + mDebtorType.Trim() + "' group by accountcode";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtBalance);
                    if (mDtBalance.Rows.Count > 0)
                    {
                        mBalance = Convert.ToDouble(mDtBalance.Rows[0]["balance"]);
                    }

                    mNewRow = mDtStatement.NewRow();

                    #region facilitysetup

                    if (mDtFacilityOptions.Rows.Count > 0)
                    {
                        foreach (DataColumn mDataColumn in mDtFacilityOptions.Columns)
                        {
                            if (mDataColumn.ColumnName.ToLower() != "autocode")
                            {
                                string mFieldName = "facility_" + mDataColumn.ColumnName;

                                switch (mDataColumn.DataType.FullName.Trim().ToLower())
                                {
                                    case "system.decimal":
                                        {
                                            mNewRow[mFieldName] = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]);
                                        }
                                        break;
                                    case "system.double":
                                        {
                                            mNewRow[mFieldName] = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]);
                                        }
                                        break;
                                    case "system.int16":
                                        {
                                            mNewRow[mFieldName] = Convert.ToInt16(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]);
                                        }
                                        break;
                                    case "system.int32":
                                        {
                                            mNewRow[mFieldName] = Convert.ToInt32(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]);
                                        }
                                        break;
                                    case "system.int64":
                                        {
                                            mNewRow[mFieldName] = Convert.ToInt64(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]);
                                        }
                                        break;
                                    case "system.single":
                                        {
                                            mNewRow[mFieldName] = Convert.ToSingle(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]);
                                        }
                                        break;
                                    case "system.datetime":
                                        {
                                            mNewRow[mFieldName] = Convert.ToDateTime(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]);
                                        }
                                        break;
                                    default:
                                        {
                                            mNewRow[mFieldName] = mDtFacilityOptions.Rows[0][mDataColumn.ColumnName].ToString();
                                        }
                                        break;
                                }
                            }
                        }
                    }

                    #endregion

                    mNewRow["datefrom"] = mDateFrom;
                    mNewRow["dateto"] = mDateTo;
                    mNewRow["accountcode"] = mAccountCode.Trim();
                    mNewRow["accountdescription"] = mAccountDescription.Trim();
                    mNewRow["transdate"] = mDateFrom;
                    mNewRow["reference"] = "B/F";
                    mNewRow["transdescription"] = "B/F";
                    mNewRow["fromwhomtowhomcode"] = "";
                    mNewRow["fromwhomtowhom"] = "";
                    mNewRow["debitamount"] = 0;
                    mNewRow["creditamount"] = 0;
                    mNewRow["balance"] = mBalance;
                    mNewRow["userid"] = "";
                    mDtStatement.Rows.Add(mNewRow);
                    mDtStatement.AcceptChanges();

                    #endregion

                    #region transactions

                    foreach (DataRow mDataRow in mDtData.Rows)
                    {
                        mBalance = mBalance + Convert.ToDouble(mDataRow["debitamount"]) - Convert.ToDouble(mDataRow["creditamount"]);

                        mNewRow = mDtStatement.NewRow();

                        #region facilitysetup

                        if (mDtFacilityOptions.Rows.Count > 0)
                        {
                            foreach (DataColumn mDataColumn in mDtFacilityOptions.Columns)
                            {
                                if (mDataColumn.ColumnName.ToLower() != "autocode")
                                {
                                    string mFieldName = "facility_" + mDataColumn.ColumnName;

                                    switch (mDataColumn.DataType.FullName.Trim().ToLower())
                                    {
                                        case "system.decimal":
                                            {
                                                mNewRow[mFieldName] = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                            }
                                            break;
                                        case "system.double":
                                            {
                                                mNewRow[mFieldName] = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                            }
                                            break;
                                        case "system.int16":
                                            {
                                                mNewRow[mFieldName] = Convert.ToInt16(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                            }
                                            break;
                                        case "system.int32":
                                            {
                                                mNewRow[mFieldName] = Convert.ToInt32(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                            }
                                            break;
                                        case "system.int64":
                                            {
                                                mNewRow[mFieldName] = Convert.ToInt64(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                            }
                                            break;
                                        case "system.single":
                                            {
                                                mNewRow[mFieldName] = Convert.ToSingle(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                            }
                                            break;
                                        case "system.datetime":
                                            {
                                                mNewRow[mFieldName] = Convert.ToDateTime(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).Date;
                                            }
                                            break;
                                        default:
                                            {
                                                mNewRow[mFieldName] = mDtFacilityOptions.Rows[0][mDataColumn.ColumnName].ToString();
                                            }
                                            break;
                                    }
                                }
                            }
                        }

                        #endregion

                        mNewRow["datefrom"] = mDateFrom;
                        mNewRow["dateto"] = mDateTo;
                        mNewRow["accountcode"] = mAccountCode.Trim();
                        mNewRow["accountdescription"] = mAccountDescription.Trim();
                        mNewRow["transdate"] = Convert.ToDateTime(mDataRow["transdate"]); ;
                        mNewRow["reference"] = mDataRow["reference"].ToString();
                        mNewRow["transdescription"] = mDataRow["transdescription"].ToString();
                        mNewRow["fromwhomtowhomcode"] = mDataRow["fromwhomtowhomcode"].ToString();
                        mNewRow["fromwhomtowhom"] = mDataRow["fromwhomtowhom"].ToString();
                        mNewRow["debitamount"] = Convert.ToDouble(mDataRow["debitamount"]);
                        mNewRow["creditamount"] = Convert.ToDouble(mDataRow["creditamount"]);
                        mNewRow["balance"] = mBalance;
                        mNewRow["userid"] = mDataRow["userid"].ToString();
                        mDtStatement.Rows.Add(mNewRow);
                        mDtStatement.AcceptChanges();
                    }

                    #endregion
                }
                else
                {
                    double mBalance = 0;

                    #region transactions

                    foreach (DataRow mDataRow in mDtData.Rows)
                    {
                        mBalance = mBalance + Convert.ToDouble(mDataRow["debitamount"]) - Convert.ToDouble(mDataRow["creditamount"]);

                        mNewRow = mDtStatement.NewRow();

                        #region facilitysetup

                        if (mDtFacilityOptions.Rows.Count > 0)
                        {
                            foreach (DataColumn mDataColumn in mDtFacilityOptions.Columns)
                            {
                                if (mDataColumn.ColumnName.ToLower() != "autocode")
                                {
                                    string mFieldName = "facility_" + mDataColumn.ColumnName;

                                    switch (mDataColumn.DataType.FullName.Trim().ToLower())
                                    {
                                        case "system.decimal":
                                            {
                                                mNewRow[mFieldName] = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                            }
                                            break;
                                        case "system.double":
                                            {
                                                mNewRow[mFieldName] = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                            }
                                            break;
                                        case "system.int16":
                                            {
                                                mNewRow[mFieldName] = Convert.ToInt16(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                            }
                                            break;
                                        case "system.int32":
                                            {
                                                mNewRow[mFieldName] = Convert.ToInt32(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                            }
                                            break;
                                        case "system.int64":
                                            {
                                                mNewRow[mFieldName] = Convert.ToInt64(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                            }
                                            break;
                                        case "system.single":
                                            {
                                                mNewRow[mFieldName] = Convert.ToSingle(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                            }
                                            break;
                                        case "system.datetime":
                                            {
                                                mNewRow[mFieldName] = Convert.ToDateTime(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]);
                                            }
                                            break;
                                        default:
                                            {
                                                mNewRow[mFieldName] = mDtFacilityOptions.Rows[0][mDataColumn.ColumnName].ToString();
                                            }
                                            break;
                                    }
                                }
                            }
                        }

                        #endregion

                        mNewRow["datefrom"] = DBNull.Value;
                        mNewRow["dateto"] = DBNull.Value;
                        mNewRow["accountcode"] = mAccountCode.Trim();
                        mNewRow["accountdescription"] = mAccountDescription.Trim();
                        mNewRow["transdate"] = Convert.ToDateTime(mDataRow["transdate"]); ;
                        mNewRow["reference"] = mDataRow["reference"].ToString();
                        mNewRow["transdescription"] = mDataRow["transdescription"].ToString();
                        mNewRow["fromwhomtowhomcode"] = mDataRow["fromwhomtowhomcode"].ToString();
                        mNewRow["fromwhomtowhom"] = mDataRow["fromwhomtowhom"].ToString();
                        mNewRow["debitamount"] = Convert.ToDouble(mDataRow["debitamount"]);
                        mNewRow["creditamount"] = Convert.ToDouble(mDataRow["creditamount"]);
                        mNewRow["balance"] = mBalance;
                        mNewRow["userid"] = mDataRow["userid"].ToString();
                        mDtStatement.Rows.Add(mNewRow);
                        mDtStatement.AcceptChanges();
                    }

                    #endregion
                }

                #endregion

                return mDtStatement;
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

        #region BIL_CashBox
        public DataSet BIL_CashBox(DateTime mDateFrom, DateTime mDateTo, bool mDateBased, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "BIL_CashBox";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                string mSumColumns = "";
                DataRow mDataRow;
                
                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtParent = new DataTable("parent");
                DataTable mDtTotals = new DataTable("totals");
                DataTable mDtRefunds = new DataTable("refunds");
                DataTable mDtBalances = new DataTable("balances");
                DataTable mDtTotalsD = new DataTable("totalsd");
                DataTable mDtRefundsD = new DataTable("refundsd");
                DataTable mDtBalancesD = new DataTable("balancesd");
                DataTable mDtTotalsG = new DataTable("totalsg");
                DataTable mDtRefundsG = new DataTable("refundsg");
                DataTable mDtBalancesG = new DataTable("balancesg");

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_datefrom", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_dateto", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_datefrom"] = mDateFrom;
                    mDataRow["para_dateto"] = mDateTo;
                    mDataRow["para_otherparameters"] = mExtraParameters;

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region prepare parent datatable

                mDtParent.Columns.Add("keyvalue", typeof(System.String));

                mDataRow = mDtParent.NewRow();
                mDataRow["keyvalue"] = "totals";
                mDtParent.Rows.Add(mDataRow);
                mDtParent.AcceptChanges();

                mDataRow = mDtParent.NewRow();
                mDataRow["keyvalue"] = "refunds";
                mDtParent.Rows.Add(mDataRow);
                mDtParent.AcceptChanges();

                mDataRow = mDtParent.NewRow();
                mDataRow["keyvalue"] = "balances";
                mDtParent.Rows.Add(mDataRow);
                mDtParent.AcceptChanges();

                mDataRow = mDtParent.NewRow();
                mDataRow["keyvalue"] = "totalsd";
                mDtParent.Rows.Add(mDataRow);
                mDtParent.AcceptChanges();

                mDataRow = mDtParent.NewRow();
                mDataRow["keyvalue"] = "refundsd";
                mDtParent.Rows.Add(mDataRow);
                mDtParent.AcceptChanges();

                mDataRow = mDtParent.NewRow();
                mDataRow["keyvalue"] = "balancesd";
                mDtParent.Rows.Add(mDataRow);
                mDtParent.AcceptChanges();

                mDataRow = mDtParent.NewRow();
                mDataRow["keyvalue"] = "totalsg";
                mDtParent.Rows.Add(mDataRow);
                mDtParent.AcceptChanges();

                mDataRow = mDtParent.NewRow();
                mDataRow["keyvalue"] = "refundsg";
                mDtParent.Rows.Add(mDataRow);
                mDtParent.AcceptChanges();

                mDataRow = mDtParent.NewRow();
                mDataRow["keyvalue"] = "balancesg";
                mDtParent.Rows.Add(mDataRow);
                mDtParent.AcceptChanges();

                #endregion

                #region build paymenttypes sum strings

                DataTable mDtCollections = new DataTable("billcollections");
                mCommand.CommandText = "select * from billcollections where 1=2";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtCollections);

                foreach (DataColumn mDataColumn in mDtCollections.Columns)
                {
                    if (mDataColumn.ColumnName.ToLower().StartsWith("paytype") == true)
                    {
                        if (mSumColumns.Trim() == "")
                        {
                            mSumColumns = "sum(" + mDataColumn.ColumnName + ") " + mDataColumn.ColumnName;
                        }
                        else
                        {
                            mSumColumns = mSumColumns + ",sum(" + mDataColumn.ColumnName + ") " + mDataColumn.ColumnName;
                        }

                        mDtFacilitySetup.Columns.Add(mDataColumn.ColumnName, typeof(System.Double));
                        mDtRefunds.Columns.Add(mDataColumn.ColumnName, typeof(System.Double));
                        mDtBalances.Columns.Add(mDataColumn.ColumnName, typeof(System.Double));
                    }
                }

                #endregion

                if (mExtraFilter.Trim() != "")
                {
                    mExtraFilter = " and " + mExtraFilter;
                }

                #region from sales

                #region totals


                mCommand.CommandText = "select 'totals' keyvalue,userid," + mSumColumns + " from billcollections where (transdate between "
                + clsGlobal.Saving_DateValue(mDateFrom) + " and " + clsGlobal.Saving_DateValue(mDateTo)
                + ") and refunded=0 and paymentsource=" + (int)AfyaPro_Types.clsEnums.PaymentSources.CashSale + mExtraFilter + " group by userid";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtTotals);

                #endregion

                #region refunds

                mCommand.CommandText = "select 'refunds' keyvalue,userid," + mSumColumns + " from billcollections where (transdate between "
                + clsGlobal.Saving_DateValue(mDateFrom) + " and " + clsGlobal.Saving_DateValue(mDateTo)
                + ") and refunded=1 and paymentsource=" + (int)AfyaPro_Types.clsEnums.PaymentSources.CashSale + mExtraFilter + " group by userid";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtRefunds);

                #endregion

                #region balances

                mCommand.CommandText = "select 'balances' keyvalue,userid," + mSumColumns + " from billcollections where (transdate between "
                + clsGlobal.Saving_DateValue(mDateFrom) + " and " + clsGlobal.Saving_DateValue(mDateTo)
                + ") and paymentsource=" + (int)AfyaPro_Types.clsEnums.PaymentSources.CashSale + mExtraFilter + " group by userid";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtBalances);

                #endregion

                #endregion

                #region from debtors

                #region totalsd

                mCommand.CommandText = "select 'totalsd' keyvalue,userid," + mSumColumns + " from billcollections where (transdate between "
                + clsGlobal.Saving_DateValue(mDateFrom) + " and " + clsGlobal.Saving_DateValue(mDateTo)
                + ") and refunded=0 and paymentsource=" + (int)AfyaPro_Types.clsEnums.PaymentSources.InvoicePayment + mExtraFilter + " group by userid";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtTotalsD);

                #endregion

                #region refundsd

                mCommand.CommandText = "select 'refundsd' keyvalue,userid," + mSumColumns + " from billcollections where (transdate between "
                + clsGlobal.Saving_DateValue(mDateFrom) + " and " + clsGlobal.Saving_DateValue(mDateTo)
                + ") and refunded=1 and paymentsource=" + (int)AfyaPro_Types.clsEnums.PaymentSources.InvoicePayment + mExtraFilter + " group by userid";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtRefundsD);

                #endregion

                #region balancesd

                mCommand.CommandText = "select 'balancesd' keyvalue,userid," + mSumColumns + " from billcollections where (transdate between "
                + clsGlobal.Saving_DateValue(mDateFrom) + " and " + clsGlobal.Saving_DateValue(mDateTo)
                + ") and paymentsource=" + (int)AfyaPro_Types.clsEnums.PaymentSources.InvoicePayment + mExtraFilter + " group by userid";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtBalancesD);

                #endregion

                #endregion

                #region  grand balances

                #region totalsg

                mCommand.CommandText = "select 'totalsg' keyvalue,userid," + mSumColumns + " from billcollections where (transdate between "
                + clsGlobal.Saving_DateValue(mDateFrom) + " and " + clsGlobal.Saving_DateValue(mDateTo)
                + ") and refunded=0 "  + mExtraFilter + " group by userid";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtTotalsG);

                #endregion

                #region refundsg

                mCommand.CommandText = "select 'refundsg' keyvalue,userid," + mSumColumns + " from billcollections where (transdate between "
                + clsGlobal.Saving_DateValue(mDateFrom) + " and " + clsGlobal.Saving_DateValue(mDateTo)
                + ") and refunded=1 " + mExtraFilter + "group by userid";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtRefundsG);

                #endregion

                #region balancesg

                mCommand.CommandText = "select 'balancesg' keyvalue,userid," + mSumColumns + " from billcollections where (transdate between "
                + clsGlobal.Saving_DateValue(mDateFrom) + " and " + clsGlobal.Saving_DateValue(mDateTo) + ") " + mExtraFilter + "group by userid";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtBalancesG);

                #endregion

                #endregion

                DataSet mDsData = new DataSet("cashbox");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtParent);
                mDsData.Tables.Add(mDtTotals);
                mDsData.Tables.Add(mDtRefunds);
                mDsData.Tables.Add(mDtBalances);
                mDsData.Tables.Add(mDtTotalsD);
                mDsData.Tables.Add(mDtRefundsD);
                mDsData.Tables.Add(mDtBalancesD);
                mDsData.Tables.Add(mDtTotalsG);
                mDsData.Tables.Add(mDtRefundsG);
                mDsData.Tables.Add(mDtBalancesG);

                #region build relationships
                
                mDsData.Relations.Add("totalsrelationship", mDtParent.Columns["keyvalue"], mDtTotals.Columns["keyvalue"]);
                mDsData.Relations.Add("refundsrelationship", mDtParent.Columns["keyvalue"], mDtRefunds.Columns["keyvalue"]);
                mDsData.Relations.Add("balancesrelationship", mDtParent.Columns["keyvalue"], mDtBalances.Columns["keyvalue"]);
                mDsData.Relations.Add("totalsdrelationship", mDtParent.Columns["keyvalue"], mDtTotalsD.Columns["keyvalue"]);
                mDsData.Relations.Add("refundsdrelationship", mDtParent.Columns["keyvalue"], mDtRefundsD.Columns["keyvalue"]);
                mDsData.Relations.Add("balancesdrelationship", mDtParent.Columns["keyvalue"], mDtBalancesD.Columns["keyvalue"]);
                mDsData.Relations.Add("totalsgrelationship", mDtParent.Columns["keyvalue"], mDtTotalsG.Columns["keyvalue"]);
                mDsData.Relations.Add("refundsgrelationship", mDtParent.Columns["keyvalue"], mDtRefundsG.Columns["keyvalue"]);
                mDsData.Relations.Add("balancesgrelationship", mDtParent.Columns["keyvalue"], mDtBalancesG.Columns["keyvalue"]);

                #endregion

                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #region BIL_BillingItemsForBarcode
        public DataTable BIL_BillingItemsForBarcode()
        {
            String mFunctionName = "BIL_BillingItemsForBarcode";

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
                DataTable mDataTable = new DataTable("data");
                mCommand.CommandText = "select * from facilitybillingitems where printbarcode=1";
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

        #region BIL_DebtorsList
        public DataSet BIL_DebtorsList(DateTime mTransDate, bool mGroups, bool mIndividuals, string mExtraFilter, string mExtraParameters, DateTime mFromDate, DateTime mToDate)
        {
            string mFunctionName = "BIL_DebtorsList";

            int mRowIndex = -1;
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
                string mCommandText = "";
                mTransDate = mTransDate.Date;

                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtDebtorsList = new DataTable("debtorslist");
                DataTable mDtCorporates = new DataTable("corporates");
                DataTable mDtPatients = new DataTable("patients");
                DataTable mDt0to30 = new DataTable("0to30");
                DataTable mDt31to60 = new DataTable("31to60");
                DataTable mDt61to90 = new DataTable("61to90");
                DataTable mDt91andAbove = new DataTable("91andAbove");

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_transdate", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));
                mDtFacilitySetup.Columns.Add("keyvalue", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_transdate"] = mTransDate;
                    mDataRow["para_otherparameters"] = mExtraParameters;
                    mDataRow["keyvalue"] = "parentlink";

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                string mStartDateStr = "transdate";
                string mEndDateStr = clsGlobal.Saving_DateValue(mTransDate);

                //string mStartDateStr =  "transdate";
                //string mEndDateStr = clsGlobal.Saving_DateValue(mToDate);

                mCommand.CommandText = "select * from facilitycorporates";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtCorporates);

                mCommand.CommandText = "select * from patients";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtPatients);

                DataView mDvCorporates = new DataView();
                mDvCorporates.Table = mDtCorporates;
                mDvCorporates.Sort = "code";

                DataView mDvPatients = new DataView();
                mDvPatients.Table = mDtPatients;
                mDvPatients.Sort = "code";

                DataView mDv0to30 = new DataView();
                DataView mDv31to60 = new DataView();
                DataView mDv61to90 = new DataView();
                DataView mDv91andAbove = new DataView();

                mDtDebtorsList.Columns.Add("debtortype", typeof(System.String));
                mDtDebtorsList.Columns.Add("accountcode", typeof(System.String));
                mDtDebtorsList.Columns.Add("accountdescription", typeof(System.String));
                mDtDebtorsList.Columns.Add("0to30", typeof(System.Double));
                mDtDebtorsList.Columns.Add("31to60", typeof(System.Double));
                mDtDebtorsList.Columns.Add("61to90", typeof(System.Double));
                mDtDebtorsList.Columns.Add("91andAbove", typeof(System.Double));

                string mPatientColumns = "";
                foreach (DataColumn mDataColumn in mDtPatients.Columns)
                {
                    if (mDataColumn.ColumnName.ToLower() != "autocode")
                    {
                        mDtDebtorsList.Columns.Add(mDataColumn.ColumnName, mDataColumn.DataType);

                        if (mPatientColumns.Trim() == "")
                        {
                            mPatientColumns = "pt." + mDataColumn.ColumnName;
                        }
                        else
                        {
                            mPatientColumns = mPatientColumns + ",pt." + mDataColumn.ColumnName;
                        }
                    }
                }

                #region generate data

                #region medical schemes

                if (mGroups == true)
                {
                    #region 0 to 30

                    mCommand.CommandText = "select billinggroupcode,sum(balancedue) balance from billinvoices "
                    + "where (" + clsGlobal.DateDiff_InDays(mStartDateStr, mEndDateStr) + " between 0 and 30) "
                    + "and billinggroupcode<>'' and voided<>1 group by billinggroupcode";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDt0to30);

                    #endregion

                    #region 0 to 30

                    mCommand.CommandText = "select billinggroupcode,sum(balancedue) balance from billinvoices "
                    + "where (" + clsGlobal.DateDiff_InDays(mStartDateStr, mEndDateStr) + " between 0 and 30) "
                    + "and billinggroupcode<>'' and voided<>1 group by billinggroupcode";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDt0to30);

                    #endregion

                    #region 31 to 60

                    mCommand.CommandText = "select billinggroupcode,sum(balancedue) balance from billinvoices "
                    + "where (" + clsGlobal.DateDiff_InDays(mStartDateStr, mEndDateStr) + " between 31 and 60) "
                    + "and billinggroupcode<>'' and voided<>1 group by billinggroupcode";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDt31to60);

                    #endregion

                    #region 61 to 90

                    mCommand.CommandText = "select billinggroupcode,sum(balancedue) balance from billinvoices "
                    + "where (" + clsGlobal.DateDiff_InDays(mStartDateStr, mEndDateStr) + " between 61 and 90) "
                    + "and billinggroupcode<>'' and voided<>1 group by billinggroupcode";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDt61to90);

                    #endregion

                    #region 91 and Above

                    mCommand.CommandText = "select billinggroupcode,sum(balancedue) balance from billinvoices "
                    + "where (" + clsGlobal.DateDiff_InDays(mStartDateStr, mEndDateStr) + " >= 91) "
                    + "and billinggroupcode<>'' and voided<>1 group by billinggroupcode";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDt91andAbove);

                    #endregion

                    DataTable mDtDebtorsGroup = new DataTable("debtors");
                    mCommandText = "select * from billdebtors where (debtortype='group')";
                    if (mExtraFilter.Trim() != "")
                    {
                        mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                    }

                    mCommand.CommandText = mCommandText;
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtDebtorsGroup);

                    mDv0to30.Table = mDt0to30;
                    mDv0to30.Sort = "billinggroupcode";

                    mDv31to60.Table = mDt31to60;
                    mDv31to60.Sort = "billinggroupcode";

                    mDv61to90.Table = mDt61to90;
                    mDv61to90.Sort = "billinggroupcode";

                    mDv91andAbove.Table = mDt91andAbove;
                    mDv91andAbove.Sort = "billinggroupcode";

                    foreach (DataRow mDataRow in mDtDebtorsGroup.Rows)
                    {
                        string mAccountCode = mDataRow["accountcode"].ToString().Trim();
                        string mAccountDescription = mDataRow["accountdescription"].ToString().Trim();
                        string mDebtorType = "GROUPS";
                        double m0to30 = 0;
                        double m31to60 = 0;
                        double m61to90 = 0;
                        double m91andAbove = 0;

                        mRowIndex = mDvCorporates.Find(mAccountCode);
                        if (mRowIndex >= 0)
                        {
                            mAccountDescription = mDvCorporates[mRowIndex]["description"].ToString().Trim();
                        }

                        mRowIndex = mDv0to30.Find(mAccountCode);
                        if (mRowIndex >= 0)
                        {
                            m0to30 = Convert.ToDouble(mDv0to30[mRowIndex]["balance"]);
                        }

                        mRowIndex = mDv31to60.Find(mAccountCode);
                        if (mRowIndex >= 0)
                        {
                            m31to60 = Convert.ToDouble(mDv31to60[mRowIndex]["balance"]);
                        }

                        mRowIndex = mDv61to90.Find(mAccountCode);
                        if (mRowIndex >= 0)
                        {
                            m61to90 = Convert.ToDouble(mDv61to90[mRowIndex]["balance"]);
                        }

                        mRowIndex = mDv91andAbove.Find(mAccountCode);
                        if (mRowIndex >= 0)
                        {
                            m91andAbove = Convert.ToDouble(mDv91andAbove[mRowIndex]["balance"]);
                        }

                        DataRow mNewRow = mDtDebtorsList.NewRow();
                        mNewRow["debtortype"] = mDebtorType;
                        mNewRow["accountcode"] = mAccountCode;
                        mNewRow["accountdescription"] = mAccountDescription;
                        mNewRow["0to30"] = m0to30;
                        mNewRow["31to60"] = m31to60;
                        mNewRow["61to90"] = m61to90;
                        mNewRow["91andAbove"] = m91andAbove;

                        mDtDebtorsList.Rows.Add(mNewRow);
                        mDtDebtorsList.AcceptChanges();
                    }
                }

                #endregion

                #region individuals

                if (mIndividuals == true)
                {
                    #region 0 to 30

                    mCommand.CommandText = "select patientcode,sum(balancedue) balance from billinvoices "
                    + "where (" + clsGlobal.DateDiff_InDays(mStartDateStr, mEndDateStr) + " between 0 and 30) "
                    + "and billinggroupcode='' and voided<>1 group by patientcode";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDt0to30);

                    #endregion

                    #region 0 to 30

                    mCommand.CommandText = "select patientcode,sum(balancedue) balance from billinvoices "
                    + "where (" + clsGlobal.DateDiff_InDays(mStartDateStr, mEndDateStr) + " between 0 and 30) "
                    + "and billinggroupcode='' and voided<>1 group by patientcode";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDt0to30);

                    #endregion

                    #region 31 to 60

                    mCommand.CommandText = "select patientcode,sum(balancedue) balance from billinvoices "
                    + "where (" + clsGlobal.DateDiff_InDays(mStartDateStr, mEndDateStr) + " between 31 and 60) "
                    + "and billinggroupcode='' and voided<>1 group by patientcode";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDt31to60);

                    #endregion

                    #region 61 to 90

                    mCommand.CommandText = "select patientcode,sum(balancedue) balance from billinvoices "
                    + "where (" + clsGlobal.DateDiff_InDays(mStartDateStr, mEndDateStr) + " between 61 and 90) "
                    + "and billinggroupcode='' and voided<>1 group by patientcode";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDt61to90);

                    #endregion

                    #region 91 and Above

                    mCommand.CommandText = "select patientcode,sum(balancedue) balance from billinvoices "
                    + "where (" + clsGlobal.DateDiff_InDays(mStartDateStr, mEndDateStr) + " >= 91) "
                    + "and billinggroupcode='' and voided<>1 group by patientcode";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDt91andAbove);

                    #endregion

                    DataTable mDtDebtorsIndividual = new DataTable("debtors");
                    mCommandText = "select * from billdebtors where (debtortype='individual')";
                    if (mExtraFilter.Trim() != "")
                    {
                        mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                    }

                    mCommand.CommandText = mCommandText;
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtDebtorsIndividual);

                    mDv0to30.Table = mDt0to30;
                    mDv0to30.Sort = "patientcode";

                    mDv31to60.Table = mDt31to60;
                    mDv31to60.Sort = "patientcode";

                    mDv61to90.Table = mDt61to90;
                    mDv61to90.Sort = "patientcode";

                    mDv91andAbove.Table = mDt91andAbove;
                    mDv91andAbove.Sort = "patientcode";

                    foreach (DataRow mDataRow in mDtDebtorsIndividual.Rows)
                    {
                        string mAccountCode = mDataRow["accountcode"].ToString().Trim();
                        string mAccountDescription = mDataRow["accountdescription"].ToString().Trim();
                        string mDebtorType = "INDIVIDUALS";
                        double m0to30 = 0;
                        double m31to60 = 0;
                        double m61to90 = 0;
                        double m91andAbove = 0;

                        mRowIndex = mDv0to30.Find(mAccountCode);
                        if (mRowIndex >= 0)
                        {
                            m0to30 = Convert.ToDouble(mDv0to30[mRowIndex]["balance"]);
                        }

                        mRowIndex = mDv31to60.Find(mAccountCode);
                        if (mRowIndex >= 0)
                        {
                            m31to60 = Convert.ToDouble(mDv31to60[mRowIndex]["balance"]);
                        }

                        mRowIndex = mDv61to90.Find(mAccountCode);
                        if (mRowIndex >= 0)
                        {
                            m61to90 = Convert.ToDouble(mDv61to90[mRowIndex]["balance"]);
                        }

                        mRowIndex = mDv91andAbove.Find(mAccountCode);
                        if (mRowIndex >= 0)
                        {
                            m91andAbove = Convert.ToDouble(mDv91andAbove[mRowIndex]["balance"]);
                        }

                        DataRow mNewRow = mDtDebtorsList.NewRow();


                        mRowIndex = mDvPatients.Find(mAccountCode);
                        if (mRowIndex >= 0)
                        {
                            mAccountDescription = mDvPatients[mRowIndex]["firstname"].ToString().Trim();
                            if (mDvPatients[mRowIndex]["othernames"].ToString().Trim() != "")
                            {
                                mAccountDescription = mAccountDescription + " " + mDvPatients[mRowIndex]["othernames"].ToString().Trim();
                            }
                            mAccountDescription = mAccountDescription + " " + mDvPatients[mRowIndex]["surname"].ToString().Trim();

                            foreach (DataColumn mDataColumn in mDtPatients.Columns)
                            {
                                if (mDataColumn.ColumnName.ToLower() != "autocode")
                                {
                                    mNewRow[mDataColumn.ColumnName] = mDvPatients[mRowIndex][mDataColumn.ColumnName];
                                }
                            }
                        }

                        mNewRow["debtortype"] = mDebtorType;
                        mNewRow["accountcode"] = mAccountCode;
                        mNewRow["accountdescription"] = mAccountDescription;
                        mNewRow["0to30"] = m0to30;
                        mNewRow["31to60"] = m31to60;
                        mNewRow["61to90"] = m61to90;
                        mNewRow["91andAbove"] = m91andAbove;
                        mDtDebtorsList.Rows.Add(mNewRow);
                        mDtDebtorsList.AcceptChanges();
                    }
                }

                #endregion

                #endregion

                DataSet mDsData = new DataSet("summary");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtDebtorsList);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #region BIL_Debtors
        public DataSet BIL_Debtors(DateTime mFromDate, DateTime mToDate)
        {
            string mFunctionName = "BIL_Debtors";

            int mRowIndex = -1;
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
                string mCommandText = "";
                
                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtDebtorsList = new DataTable("debtorslist");
                DataTable mDtCorporates = new DataTable("corporates");
                DataTable mDtPatients = new DataTable("patients");
                DataTable mDtAllGropus = new DataTable("mDtAllGropus");
                

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_transdate", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));
                mDtFacilitySetup.Columns.Add("keyvalue", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();
                                     
                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                string mStartDateStr = clsGlobal.Saving_DateValue(mFromDate);
                string mEndDateStr = clsGlobal.Saving_DateValue(mToDate);

                //string mStartDateStr =  "transdate";
                //string mEndDateStr = clsGlobal.Saving_DateValue(mToDate);

                mCommand.CommandText = "select * from facilitycorporates";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtCorporates);

                mCommand.CommandText = "select * from patients";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtPatients);

                DataView mDvCorporates = new DataView();
                mDvCorporates.Table = mDtCorporates;
                mDvCorporates.Sort = "code";

                DataView mDvPatients = new DataView();
                mDvPatients.Table = mDtPatients;
                mDvPatients.Sort = "code";

                DataView mDvAllGroups = new DataView();
                DataView mDv31to60 = new DataView();
                DataView mDv61to90 = new DataView();
                DataView mDv91andAbove = new DataView();

                mDtDebtorsList.Columns.Add("debtortype", typeof(System.String));
                mDtDebtorsList.Columns.Add("accountcode", typeof(System.String));
                mDtDebtorsList.Columns.Add("accountdescription", typeof(System.String));
                mDtDebtorsList.Columns.Add("balance", typeof(System.Double));
                mDtDebtorsList.Columns.Add("GVH", typeof(System.String));
                mDtDebtorsList.Columns.Add("DateFrom", typeof(System.DateTime));
                mDtDebtorsList.Columns.Add("DateTo", typeof(System.DateTime));
               

                string mPatientColumns = "";
                foreach (DataColumn mDataColumn in mDtPatients.Columns)
                {
                    if (mDataColumn.ColumnName.ToLower() != "autocode")
                    {
                        mDtDebtorsList.Columns.Add(mDataColumn.ColumnName, mDataColumn.DataType);

                        if (mPatientColumns.Trim() == "")
                        {
                            mPatientColumns = "pt." + mDataColumn.ColumnName;
                        }
                        else
                        {
                            mPatientColumns = mPatientColumns + ",pt." + mDataColumn.ColumnName;
                        }
                    }
                }

                #region generate data

                #region medical schemes

               
                    #region allgroups

                    mCommand.CommandText = "select billinggroupcode,sum(balancedue) balance from billinvoices "
                    + "where transdate between '" + mFromDate + "' and '"  + mToDate + "'"
                    + "and billinggroupcode<>'' and voided<>1 group by billinggroupcode";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAllGropus);

                    #endregion


                    DataTable mDtDebtorsGroup = new DataTable("debtors");
                    mCommandText = "select * from billdebtors where (debtortype='group')";
                   

                    mCommand.CommandText = mCommandText;
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtDebtorsGroup);

                    mDvAllGroups.Table = mDtAllGropus;
                    mDvAllGroups.Sort = "billinggroupcode";

                    

                    foreach (DataRow mDataRow in mDtDebtorsGroup.Rows)
                    {
                        string mAccountCode = mDataRow["accountcode"].ToString().Trim();
                        string mAccountDescription = mDataRow["accountdescription"].ToString().Trim();
                        string mDebtorType = "GROUPS";
                        double mBalance = 0;
                       

                       // mRowIndex = mDvCorporates.Find(mAccountCode);



                        mBalance = Convert.ToDouble(mDataRow["balance"]);
                        

                        

                        DataRow mNewRow = mDtDebtorsList.NewRow();
                        mNewRow["debtortype"] = mDebtorType;
                        mNewRow["accountcode"] = mAccountCode;
                        mNewRow["accountdescription"] = mAccountDescription;
                        mNewRow["balance"] = mBalance;
                        mNewRow["GVH"] = "";
                        mNewRow["DateFrom"] = Convert.ToDateTime(mFromDate);
                        mNewRow["DateTo"] = Convert.ToDateTime(mToDate);


                        if (mBalance > 0)
                        {
                            mDtDebtorsList.Rows.Add(mNewRow);
                        }
                        mDtDebtorsList.AcceptChanges();
                    }
                

                #endregion

                #region individuals
                                
                  
                    DataTable mDtDebtorsIndividual = new DataTable("debtors");
                  
                    mCommandText = "select * from billdebtors where transdate between " + clsGlobal.Saving_DateValue(mFromDate) + " and " + clsGlobal.Saving_DateValue(mToDate) + " and (debtortype='Individual')";
                    

                    mCommand.CommandText = mCommandText;
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtDebtorsIndividual);

                    
                    foreach (DataRow mDataRow in mDtDebtorsIndividual.Rows)
                    {

                        string mAccountCode = mDataRow["accountcode"].ToString().Trim();
                        string mAccountDescription = mDataRow["accountdescription"].ToString().Trim();
                        string mDebtorType = "INDIVIDUALS";
                        double mBalance = 0;


                       // mRowIndex = mDvCorporates.Find(mAccountCode);
                        mBalance = Convert.ToDouble(mDataRow["balance"]);
                        

                       
                        DataRow mNewRow = mDtDebtorsList.NewRow();

                        string mVillage = "";
                        OdbcCommand mCmd = new OdbcCommand();
                        mCmd.Connection = mConn;
                        mCommandText ="select * from patients where code ='" + mAccountCode + "'";
                        mCmd.CommandText = mCommandText;
                        DataSet mDS = new DataSet();
                        mDS.Clear();
                        OdbcDataAdapter mAdpt = new OdbcDataAdapter();
                        mAdpt.SelectCommand = mCmd;
                        mAdpt.Fill(mDS);

                        if (mDS.Tables[0].Rows .Count  > 0)
                        {
                            mAccountDescription = mDS.Tables[0].Rows[0]["firstname"].ToString().Trim();


                            if (mDS.Tables[0].Rows[0]["village"].ToString() != "")
                            {
                                mVillage = mDS.Tables[0].Rows[0]["village"].ToString();
                            }
                            mAccountDescription = mAccountDescription + " " + mDS.Tables[0].Rows[0]["surname"].ToString().Trim();

                            //foreach (DataColumn mDataColumn in mDtPatients.Columns)
                            //{
                            //    if (mDataColumn.ColumnName.ToLower() != "autocode")
                            //    {
                            //        mNewRow[mDataColumn.ColumnName] = mDvPatients[mRowIndex][mDataColumn.ColumnName];
                            //    }
                            //}
                        }

                        mNewRow["debtortype"] = mDebtorType;
                        mNewRow["accountcode"] = mAccountCode;
                        mNewRow["accountdescription"] = mAccountDescription;
                        mNewRow["balance"] = mBalance;
                        mNewRow["GVH"] = mVillage.ToString();
                        mNewRow["DateFrom"] = Convert.ToDateTime(mFromDate);
                        mNewRow["DateTo"] = Convert.ToDateTime(mToDate);


   
                        if (mBalance > 0)
                        {
                            mDtDebtorsList.Rows.Add(mNewRow);
                        }
                       
                        mDtDebtorsList.AcceptChanges();
                    }
                

                #endregion

                #endregion

                DataSet mDsData = new DataSet("summary");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtDebtorsList);
                mDsData.RemotingFormat = SerializationFormat.Binary;
                mConn.Close();
                return mDsData;
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

        #region BIL_DailySalesSummary
        public DataSet BIL_DailySalesSummary(DateTime mDateFrom, DateTime mDateTo, bool mDateBased, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "BIL_DailySalesSummary";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                DataRow mDataRow;
                int mRowIndex = -1;
                double mAmount = 0;
                double mPaid = 0;
                string mCommandText = "";

                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtParent = new DataTable("parent");
                DataTable mDtCash = new DataTable("cash");
                DataTable mDtInvoices = new DataTable("invoices");

                mDtCash.Columns.Add("keyvalue", typeof(System.String));
                mDtCash.Columns.Add("groupcode", typeof(System.String));
                mDtCash.Columns.Add("groupdescription", typeof(System.String));
                mDtCash.Columns.Add("amount", typeof(System.Double));

                mDtInvoices.Columns.Add("keyvalue", typeof(System.String));
                mDtInvoices.Columns.Add("groupcode", typeof(System.String));
                mDtInvoices.Columns.Add("groupdescription", typeof(System.String));
                mDtInvoices.Columns.Add("amount", typeof(System.Double));
                mDtInvoices.Columns.Add("paid", typeof(System.Double));

                DataTable mDtGroups = new DataTable("billinggroups");
                mCommand.CommandText = "select * from facilitycorporates";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtGroups);

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_datefrom", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_dateto", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_datefrom"] = mDateFrom;
                    mDataRow["para_dateto"] = mDateTo;
                    mDataRow["para_otherparameters"] = mExtraParameters;

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region prepare parent datatable

                mDtParent.Columns.Add("keyvalue", typeof(System.String));

                mDataRow = mDtParent.NewRow();
                mDataRow["keyvalue"] = "cash";
                mDtParent.Rows.Add(mDataRow);
                mDtParent.AcceptChanges();

                mDataRow = mDtParent.NewRow();
                mDataRow["keyvalue"] = "invoices";
                mDtParent.Rows.Add(mDataRow);
                mDtParent.AcceptChanges();

                #endregion

                #region cash

                DataTable mDtCashSales = new DataTable("cashsales");
                mCommandText = "select billinggroupcode,sum(amount) amount from billsalesitems "
                + "where (transdate between " + clsGlobal.Saving_DateValue(mDateFrom) + " and " + clsGlobal.Saving_DateValue(mDateTo)
                + ") and invoiceno=''";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }
                mCommandText = mCommandText + " group by billinggroupcode";

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtCashSales);
                DataView mDvCashSales = new DataView();
                mDvCashSales.Table = mDtCashSales;
                mDvCashSales.Sort = "billinggroupcode";

                //individuals
                mAmount = 0;
                mRowIndex = mDvCashSales.Find("");
                if (mRowIndex >= 0)
                {
                    mAmount = Convert.ToDouble(mDvCashSales[mRowIndex]["amount"]);
                }
                mDataRow = mDtCash.NewRow();
                mDataRow["keyvalue"] = "cash";
                mDataRow["groupcode"] = DBNull.Value;
                mDataRow["groupdescription"] = DBNull.Value;
                mDataRow["amount"] = mAmount;
                mDtCash.Rows.Add(mDataRow);
                mDtCash.AcceptChanges();

                foreach (DataRow mGroupDataRow in mDtGroups.Rows)
                {
                    mAmount = 0;
                    mRowIndex = mDvCashSales.Find(mGroupDataRow["code"]);
                    if (mRowIndex >= 0)
                    {
                        mAmount = Convert.ToDouble(mDvCashSales[mRowIndex]["amount"]);
                    }

                    mDataRow = mDtCash.NewRow();
                    mDataRow["keyvalue"] = "cash";
                    mDataRow["groupcode"] = mGroupDataRow["code"];
                    mDataRow["groupdescription"] = mGroupDataRow["description"];
                    mDataRow["amount"] = mAmount;
                    mDtCash.Rows.Add(mDataRow);
                    mDtCash.AcceptChanges();
                }

                #endregion

                #region invoices

                #region amount

                DataTable mDtInvoiceSales = new DataTable("cashsales");
                mCommandText = "select billinggroupcode,sum(amount) amount from billsalesitems "
                + "where (transdate between " + clsGlobal.Saving_DateValue(mDateFrom) + " and " + clsGlobal.Saving_DateValue(mDateTo)
                + ") and invoiceno<>''";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }
                mCommandText = mCommandText + " group by billinggroupcode";

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtInvoiceSales);
                DataView mDvInvoiceSales = new DataView();
                mDvInvoiceSales.Table = mDtInvoiceSales;
                mDvInvoiceSales.Sort = "billinggroupcode";

                //individuals
                mAmount = 0;
                mRowIndex = mDvInvoiceSales.Find("");
                if (mRowIndex >= 0)
                {
                    mAmount = Convert.ToDouble(mDvInvoiceSales[mRowIndex]["amount"]);
                }
                mDataRow = mDtInvoices.NewRow();
                mDataRow["keyvalue"] = "invoices";
                mDataRow["groupcode"] = DBNull.Value;
                mDataRow["groupdescription"] = DBNull.Value;
                mDataRow["amount"] = mAmount;
                mDataRow["paid"] = 0;
                mDtInvoices.Rows.Add(mDataRow);
                mDtInvoices.AcceptChanges();

                foreach (DataRow mGroupDataRow in mDtGroups.Rows)
                {
                    mAmount = 0;
                    mPaid = 0;
                    mRowIndex = mDvInvoiceSales.Find(mGroupDataRow["code"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mAmount = Convert.ToDouble(mDvInvoiceSales[mRowIndex]["amount"]);
                    }

                    mDataRow = mDtInvoices.NewRow();
                    mDataRow["keyvalue"] = "invoices";
                    mDataRow["groupcode"] = mGroupDataRow["code"];
                    mDataRow["groupdescription"] = mGroupDataRow["description"];
                    mDataRow["amount"] = mAmount;
                    mDataRow["paid"] = mPaid;
                    mDtInvoices.Rows.Add(mDataRow);
                    mDtInvoices.AcceptChanges();
                }

                #endregion

                #region paid

                //get receipts involved in invoices
                DataTable mDtReceipts = new DataTable("receipts");
                mCommandText = "select distinct(receiptno) receiptno from billsalesitems "
                + "where (transdate between " + clsGlobal.Saving_DateValue(mDateFrom) + " and " + clsGlobal.Saving_DateValue(mDateTo)
                + ") and invoiceno<>'' and receiptno<>''";
                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtReceipts);

                string mReceipts = "";
                foreach (DataRow mReceiptRow in mDtReceipts.Rows)
                {
                    if (mReceipts.Trim() == "")
                    {
                        mReceipts = "'" + mReceiptRow["receiptno"].ToString().Trim() + "'";
                    }
                    else
                    {
                        mReceipts = mReceipts + ",'" + mReceiptRow["receiptno"].ToString().Trim() + "'";
                    }
                }

                if (mReceipts.Trim() != "")
                {
                    string mPaymentTypes = "";
                    DataTable mDtColumns = new DataTable("columns");
                    mCommand.CommandText = "select * from billcollections where 1=2";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtColumns);

                    foreach (DataColumn mDataColumn in mDtColumns.Columns)
                    {
                        if (mDataColumn.ColumnName.ToLower().StartsWith("paytype") == true)
                        {
                            if (mPaymentTypes.Trim() == "")
                            {
                                mPaymentTypes = mDataColumn.ColumnName;
                            }
                            else
                            {
                                mPaymentTypes = mPaymentTypes + "+" + mDataColumn.ColumnName;
                            }
                        }
                    }

                    DataTable mDtInvoicePayments = new DataTable("invoicepayments");
                    mCommandText = "select billinggroupcode, sum(" + mPaymentTypes 
                    + ") paid from billcollections where (transdate between " + clsGlobal.Saving_DateValue(mDateFrom) 
                    + " and " + clsGlobal.Saving_DateValue(mDateTo) + ") and receiptno in (" + mReceipts + ")";
                    if (mExtraFilter.Trim() != "")
                    {
                        mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                    }
                    mCommandText = mCommandText + " group by billinggroupcode";
                    mCommand.CommandText = mCommandText;
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtInvoicePayments);
                    DataView mDvInvoicePayments = new DataView();
                    mDvInvoicePayments.Table = mDtInvoicePayments;
                    mDvInvoicePayments.Sort = "billinggroupcode";

                    foreach (DataRow mInvoiceRow in mDtInvoices.Rows)
                    {
                        mRowIndex = mDvInvoicePayments.Find(mInvoiceRow["groupcode"]);
                        if (mRowIndex >= 0)
                        {
                            mInvoiceRow.BeginEdit();
                            mInvoiceRow["paid"] = Convert.ToDouble(mDvInvoicePayments[mRowIndex]["paid"]);
                            mInvoiceRow.EndEdit();
                        }
                    }

                    mDtInvoices.AcceptChanges();
                }

                #endregion

                #endregion

                DataSet mDsData = new DataSet("dailysalessummary");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtParent);
                mDsData.Tables.Add(mDtCash);
                mDsData.Tables.Add(mDtInvoices);

                #region build relationships

                mDsData.Relations.Add("cashrelationship", mDtParent.Columns["keyvalue"], mDtCash.Columns["keyvalue"]);
                mDsData.Relations.Add("invoicesrelationship", mDtParent.Columns["keyvalue"], mDtInvoices.Columns["keyvalue"]);

                #endregion

                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #region BIL_DailySalesDetails
        public DataSet BIL_DailySalesDetails(DateTime mDateFrom, DateTime mDateTo, bool mDateBased, 
            bool mGroupSimilarItems, bool mCustomerGroupsAndItemGroups, bool mGroupByDept, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "BIL_DailySalesDetails";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                string mCommandText = "";
                DataRow mDataRow = null;

                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtParent = new DataTable("parent");
                DataTable mDtSalesDetails = new DataTable("salesdetails");

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_datefrom", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_dateto", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_datefrom"] = mDateFrom;
                    mDataRow["para_dateto"] = mDateTo;
                    mDataRow["para_otherparameters"] = mExtraParameters;

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region prepare parent datatable

                mDtParent.Columns.Add("keyvalue", typeof(System.String));

                mDataRow = mDtParent.NewRow();
                mDataRow["keyvalue"] = "salesdetails";
                mDtParent.Rows.Add(mDataRow);
                mDtParent.AcceptChanges();

                #endregion

                #region sales details

                mDtSalesDetails.Columns.Add("keyvalue", typeof(System.String));
                mDtSalesDetails.Columns.Add("billinggroupcode", typeof(System.String));
                mDtSalesDetails.Columns.Add("billinggroupdescription", typeof(System.String));
                mDtSalesDetails.Columns.Add("itemgroupcode", typeof(System.String));
                mDtSalesDetails.Columns.Add("itemgroupdescription", typeof(System.String));
                mDtSalesDetails.Columns.Add("itemcode", typeof(System.String));
                mDtSalesDetails.Columns.Add("itemdescription", typeof(System.String));
                mDtSalesDetails.Columns.Add("departmentdescription", typeof(System.String));
                mDtSalesDetails.Columns.Add("wardcode", typeof(System.String));
                mDtSalesDetails.Columns.Add("warddescription", typeof(System.String));
                mDtSalesDetails.Columns.Add("unitprice", typeof(System.Double));
                mDtSalesDetails.Columns.Add("qty", typeof(System.Double));
                mDtSalesDetails.Columns.Add("amount", typeof(System.Double));

                //corporates for lookup
                DataTable mDtGroups = new DataTable("billinggroups");
                mCommand.CommandText = "select * from facilitycorporates";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtGroups);
                DataView mDvGroups = new DataView();
                mDvGroups.Table = mDtGroups;
                mDvGroups.Sort = "code";

                //wards for lookup
                DataTable mDtWards = new DataTable("wards");
                mCommand.CommandText = "select * from facilitywards";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtWards);
                DataView mDvWards = new DataView();
                mDvWards.Table = mDtWards;
                mDvWards.Sort = "code";

                //itemgroups for lookup
                DataTable mDtItemGroups = new DataTable("itemgroups");
                mCommand.CommandText = "select * from facilitybillinggroups";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtItemGroups);
                DataView mDvItemGroups = new DataView();
                mDvItemGroups.Table = mDtItemGroups;
                mDvItemGroups.Sort = "code";

                //items for lookup
                DataTable mDtItems = new DataTable("itemgroups");
                mCommand.CommandText = "select * from facilitybillingitems";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtItems);
                DataView mDvItems = new DataView();
                mDvItems.Table = mDtItems;
                mDvItems.Sort = "code";

                //som_products for lookup
                DataTable mDtProducts = new DataTable("som_products");
                mCommand.CommandText = "select * from som_products";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtProducts);
                DataView mDvProducts = new DataView();
                mDvProducts.Table = mDtProducts;
                mDvProducts.Sort = "code";

                if (mCustomerGroupsAndItemGroups == true)
                {
                    if (mGroupByDept == true)
                    {
                        mCommandText = "select billinggroupcode,wardcode,itemgroupcode,'' itemcode,sum(qty) qty,"
                        + "sum(amount) amount from billsalesitems where (transdate between " + clsGlobal.Saving_DateValue(mDateFrom)
                        + " and " + clsGlobal.Saving_DateValue(mDateTo) + ")";
                        if (mExtraFilter.Trim() != "")
                        {
                            mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                        }
                        mCommandText = mCommandText + " group by billinggroupcode,wardcode,itemgroupcode";
                    }
                    else
                    {
                        mCommandText = "select billinggroupcode,'' wardcode,itemgroupcode,'' itemcode,sum(qty) qty,"
                        + "sum(amount) amount from billsalesitems where (transdate between " + clsGlobal.Saving_DateValue(mDateFrom)
                        + " and " + clsGlobal.Saving_DateValue(mDateTo) + ")";
                        if (mExtraFilter.Trim() != "")
                        {
                            mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                        }
                        mCommandText = mCommandText + " group by billinggroupcode,itemgroupcode";
                    }
                }
                else
                {
                    if (mGroupSimilarItems == true)
                    {
                        if (mGroupByDept == true)
                        {
                            mCommandText = "select '' billinggroupcode,wardcode,itemgroupcode,itemcode,sum(qty) qty,"
                            + "sum(amount) amount from billsalesitems where (transdate between " + clsGlobal.Saving_DateValue(mDateFrom)
                            + " and " + clsGlobal.Saving_DateValue(mDateTo) + ")";
                            if (mExtraFilter.Trim() != "")
                            {
                                mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                            }
                            mCommandText = mCommandText + " group by wardcode,itemgroupcode,itemcode";
                        }
                        else
                        {
                            mCommandText = "select '' billinggroupcode,'' wardcode,itemgroupcode,itemcode,sum(qty) qty,"
                            + "sum(amount) amount from billsalesitems where (transdate between " + clsGlobal.Saving_DateValue(mDateFrom)
                            + " and " + clsGlobal.Saving_DateValue(mDateTo) + ")";
                            if (mExtraFilter.Trim() != "")
                            {
                                mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                            }
                            mCommandText = mCommandText + " group by itemgroupcode,itemcode";
                        }
                    }
                    else
                    {
                        if (mGroupByDept == true)
                        {
                            mCommandText = "select '' billinggroupcode,wardcode,itemgroupcode,itemcode,amount/qty unitprice,sum(qty) qty,"
                            + "sum(amount) amount from billsalesitems where (transdate between " + clsGlobal.Saving_DateValue(mDateFrom)
                            + " and " + clsGlobal.Saving_DateValue(mDateTo) + ")";
                            if (mExtraFilter.Trim() != "")
                            {
                                mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                            }
                            mCommandText = mCommandText + " group by wardcode,itemgroupcode,itemcode,amount/qty";
                        }
                        else
                        {
                            mCommandText = "select '' billinggroupcode,'' wardcode,itemgroupcode,itemcode,amount/qty unitprice,sum(qty) qty,"
                            + "sum(amount) amount from billsalesitems where (transdate between " + clsGlobal.Saving_DateValue(mDateFrom)
                            + " and " + clsGlobal.Saving_DateValue(mDateTo) + ")";
                            if (mExtraFilter.Trim() != "")
                            {
                                mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                            }
                            mCommandText = mCommandText + " group by itemgroupcode,itemcode,amount/qty";
                        }
                    }
                }

                DataTable mDtSalesItems = new DataTable("salesitems");
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSalesItems);

                foreach (DataRow mItemDataRow in mDtSalesItems.Rows)
                {
                    string mBillingGroupCode = mItemDataRow["billinggroupcode"].ToString().Trim();
                    string mBillingGroupDescription = "";
                    string mItemGroupCode = mItemDataRow["itemgroupcode"].ToString().Trim();
                    string mItemGroupDescription = "";
                    string mItemCode = mItemDataRow["itemcode"].ToString().Trim();
                    string mItemDescription = "";
                    string mDepartmentDescription = mItemDataRow["wardcode"].ToString().Trim() == "" ? "OPD" : "IPD";
                    string mWardCode = mItemDataRow["wardcode"].ToString().Trim();
                    string mWardDescription = "";
                    double mQty = Convert.ToDouble(mItemDataRow["qty"]);
                    double mAmount = Convert.ToDouble(mItemDataRow["amount"]);
                    double mUnitPrice = mAmount / mQty;
                    int mRowIndex = -1;

                    //billinggroup description
                    if (mCustomerGroupsAndItemGroups == true)
                    {
                        mRowIndex = mDvGroups.Find(mBillingGroupCode);
                        if (mRowIndex >= 0)
                        {
                            mBillingGroupDescription = mDvGroups[mRowIndex]["description"].ToString().Trim();
                        }
                    }

                    //ward description
                    mRowIndex = mDvWards.Find(mWardCode);
                    if (mRowIndex >= 0)
                    {
                        mWardDescription = mDvWards[mRowIndex]["description"].ToString().Trim();
                    }

                    
                    if (mItemGroupCode.ToLower() == "pharmacy")
                    {
                        //itemgroup description
                        mItemGroupDescription = "Pharmacy";

                        //item description
                        mRowIndex = mDvProducts.Find(mItemCode);
                        if (mRowIndex >= 0)
                        {
                            mItemDescription = mDvProducts[mRowIndex]["description"].ToString().Trim();
                        }
                    }
                    else
                    {
                        //itemgroup description
                        mRowIndex = mDvItemGroups.Find(mItemGroupCode);
                        if (mRowIndex >= 0)
                        {
                            mItemGroupDescription = mDvItemGroups[mRowIndex]["description"].ToString().Trim();
                        }

                        //item description
                        mRowIndex = mDvItems.Find(mItemCode);
                        if (mRowIndex >= 0)
                        {
                            mItemDescription = mDvItems[mRowIndex]["description"].ToString().Trim();
                        }
                    }

                    DataRow mNewRow = mDtSalesDetails.NewRow();
                    mNewRow["keyvalue"] = "salesdetails";
                    mNewRow["billinggroupcode"] = mBillingGroupCode;
                    mNewRow["billinggroupdescription"] = mBillingGroupDescription;
                    mNewRow["departmentdescription"] = mDepartmentDescription;
                    mNewRow["wardcode"] = mWardCode;
                    mNewRow["warddescription"] = mWardDescription;
                    mNewRow["itemgroupcode"] = mItemGroupCode;
                    mNewRow["itemgroupdescription"] = mItemGroupDescription;
                    mNewRow["itemcode"] = mItemCode;
                    mNewRow["itemdescription"] = mItemDescription;
                    mNewRow["unitprice"] = mUnitPrice;
                    mNewRow["qty"] = mQty;
                    mNewRow["amount"] = mAmount;
                    mDtSalesDetails.Rows.Add(mNewRow);
                    mDtSalesDetails.AcceptChanges();
                }

                #endregion

                DataSet mDsData = new DataSet("dailysalesdetails");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtParent);
                mDsData.Tables.Add(mDtSalesDetails);

                #region build relationships

                mDsData.Relations.Add("salesdetailsrelationship", mDtParent.Columns["keyvalue"], mDtSalesDetails.Columns["keyvalue"]);

                #endregion

                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #region BIL_GroupChargesBreakDown
        public DataSet BIL_GroupChargesBreakDown(bool mDateSpecified, DateTime mDateFrom, DateTime mDateTo, 
            bool mGetUnpaidBalance, int mDepartment, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "BIL_GroupChargesBreakDown";

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
                string mCommandText = "";
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtSummary = new DataTable("debtorslist");
                DataTable mDtPatients = new DataTable("patients");

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_datefrom", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_dateto", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_datefrom"] = mDateFrom;
                    mDataRow["para_dateto"] = mDateTo;
                    mDataRow["para_otherparameters"] = mExtraParameters;

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                mCommand.CommandText = "select * from patients";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtPatients);

                DataView mDvPatients = new DataView();
                mDvPatients.Table = mDtPatients;
                mDvPatients.Sort = "code";

                #region create columns

                foreach (DataColumn mDataColumn in mDtPatients.Columns)
                {
                    if (mDataColumn.ColumnName.ToLower() != "autocode" && mDataColumn.ColumnName.ToLower() != "code")
                    {
                        mDtSummary.Columns.Add(mDataColumn.ColumnName, mDataColumn.DataType);
                    }
                }

                mDtSummary.Columns.Add("transdate", typeof(System.DateTime));
                mDtSummary.Columns.Add("diagnosesdescription", typeof(System.String));
                mDtSummary.Columns.Add("billamount", typeof(System.Double));
                mDtSummary.Columns.Add("patientcode", typeof(System.String));
                mDtSummary.Columns.Add("billinggroupcode", typeof(System.String));
                mDtSummary.Columns.Add("billinggroupdescription", typeof(System.String));

                #endregion

                #region generate data

                DataTable mDtInvoices = new DataTable("invoices");
                DataTable mDtDiagnoses = new DataTable("diagnoses");

                #region get invoices

                mCommandText = "SELECT "
                                + "inv.billinggroupcode"
                                + ",fc.description billinggroupdescription"
                                + ",inv.transdate"
                                + ",inv.patientcode"
                                + ",SUM(inv.totaldue) totaldue"
                                + ",SUM(inv.totalpaid) totalpaid"
                                + ",SUM(inv.balancedue) balancedue"
                                + ",SUM(inv.paidforinvoice) paidforinvoice "
                                + "FROM "
                                + "billinvoices inv "
                                + "LEFT JOIN "
                                + "facilitycorporates fc ON inv.billinggroupcode=fc.code";

                if (mDateSpecified == true)
                {
                    mCommandText = mCommandText + " WHERE (transdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND "
                        + clsGlobal.Saving_DateValue(mDateTo) + ") and voided<>1";

                    switch (mDepartment)
                    {
                        case 1:
                            {
                                mCommandText = mCommandText + " AND (wardcode IS NULL OR wardcode='')";
                            }
                            break;
                        case 2:
                            {
                                mCommandText = mCommandText + " AND (wardcode IS NOT NULL AND wardcode<>'')";
                            }
                            break;
                    }

                    if (mExtraFilter.Trim() != "")
                    {
                        mCommandText = mCommandText + " AND (" + mExtraFilter + ")";
                    }
                }
                else
                {
                    switch (mDepartment)
                    {
                        case 1:
                            {
                                mCommandText = mCommandText + " WHERE (wardcode IS NULL OR wardcode='')";

                                if (mExtraFilter.Trim() != "")
                                {
                                    mCommandText = mCommandText + " AND (" + mExtraFilter + ") and voided<>1";
                                }
                            }
                            break;
                        case 2:
                            {
                                mCommandText = mCommandText + " WHERE (wardcode IS NOT NULL AND wardcode<>'')";

                                if (mExtraFilter.Trim() != "")
                                {
                                    mCommandText = mCommandText + " AND (" + mExtraFilter + ") and voided<>1";
                                }
                            }
                            break;
                        default:
                            {
                                if (mExtraFilter.Trim() != "")
                                {
                                    mCommandText = mCommandText + " WHERE (" + mExtraFilter + ") and voided<>1";
                                }
                            }
                            break;
                    }
                }

                mCommandText = mCommandText + " GROUP BY inv.billinggroupcode,inv.transdate,inv.patientcode";

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtInvoices);

                #endregion

                #region get diagnoses

                mCommandText = "SELECT "
                                + "dx.billinggroupcode"
                                + ",dx.transdate"
                                + ",dx.patientcode"
                                + ",dx.diagnosiscode"
                                + ",dg.description diagnosisdescription "
                                + "FROM "
                                + "view_dxtpatientdiagnoseslog dx "
                                + "LEFT JOIN "
                                + "dxtdiagnoses dg ON dx.diagnosiscode=dg.code";

                if (mDateSpecified == true)
                {
                    mCommandText = mCommandText + " WHERE (transdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ")";

                    switch (mDepartment)
                    {
                        case 1:
                            {
                                mCommandText = mCommandText + " AND (wardcode IS NULL OR wardcode='')";
                            }
                            break;
                        case 2:
                            {
                                mCommandText = mCommandText + " AND (wardcode IS NOT NULL AND wardcode<>'')";
                            }
                            break;
                    }

                    if (mExtraFilter.Trim() != "")
                    {
                        mCommandText = mCommandText + " AND (" + mExtraFilter + ")";
                    }
                }
                else
                {
                    switch (mDepartment)
                    {
                        case 1:
                            {
                                mCommandText = mCommandText + " WHERE (wardcode IS NULL OR wardcode='')";

                                if (mExtraFilter.Trim() != "")
                                {
                                    mCommandText = mCommandText + " AND (" + mExtraFilter + ")";
                                }
                            }
                            break;
                        case 2:
                            {
                                mCommandText = mCommandText + " WHERE (wardcode IS NOT NULL AND wardcode<>'')";

                                if (mExtraFilter.Trim() != "")
                                {
                                    mCommandText = mCommandText + " AND (" + mExtraFilter + ")";
                                }
                            }
                            break;
                        default:
                            {
                                if (mExtraFilter.Trim() != "")
                                {
                                    mCommandText = mCommandText + " WHERE (" + mExtraFilter + ")";
                                }
                            }
                            break;
                    }
                }

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtDiagnoses);

                #endregion

                #region build report data

                DataView mDvDiagnoses = new DataView();
                mDvDiagnoses.Table = mDtDiagnoses;

                foreach (DataRow mInvoiceRow in mDtInvoices.Rows)
                {
                    string mBillingGroupCode = mInvoiceRow["billinggroupcode"].ToString().Trim();
                    string mBillingGroupDescription = mInvoiceRow["billinggroupdescription"].ToString().Trim();
                    DateTime mTransDate = Convert.ToDateTime(mInvoiceRow["transdate"]);
                    string mPatientCode = mInvoiceRow["patientcode"].ToString().Trim();
                    double mBillAmount = Convert.ToDouble(mInvoiceRow["totaldue"]) - Convert.ToDouble(mInvoiceRow["paidforinvoice"]);
                    string mDiagnoses = "";

                    if (mGetUnpaidBalance == true)
                    {
                        mBillAmount = Convert.ToDouble(mInvoiceRow["balancedue"]);
                    }

                    #region find the diagnoses

                    mDvDiagnoses.RowFilter = "patientcode='" + mPatientCode + "' and billinggroupcode='" + mBillingGroupCode
                        + "' and transdate=" + clsGlobal.Saving_DateValue(mTransDate);

                    foreach (DataRowView mDiagnosesRow in mDvDiagnoses)
                    {
                        if (mDiagnoses.Trim() == "")
                        {
                            mDiagnoses = mDiagnosesRow["diagnosisdescription"].ToString().Trim();
                        }
                        else
                        {
                            mDiagnoses = mDiagnoses + " + " + mDiagnosesRow["diagnosisdescription"].ToString().Trim();
                        }
                    }

                    #endregion

                    DataRow mNewRow = mDtSummary.NewRow();
                    mNewRow["transdate"] = mTransDate;
                    mNewRow["diagnosesdescription"] = mDiagnoses;
                    mNewRow["billinggroupcode"] = mBillingGroupCode;
                    mNewRow["billinggroupdescription"] = mBillingGroupDescription;
                    mNewRow["patientcode"] = mPatientCode;
                    mNewRow["billamount"] = mBillAmount;

                    int mRowIndex = mDvPatients.Find(mPatientCode);

                    if (mRowIndex >= 0)
                    {
                        foreach (DataColumn mDataColumn in mDtPatients.Columns)
                        {
                            if (mDataColumn.ColumnName.ToLower() != "autocode" && mDataColumn.ColumnName.ToLower() != "code")
                            {
                                mNewRow[mDataColumn.ColumnName] = mDvPatients[mRowIndex][mDataColumn.ColumnName];
                            }
                        }
                    }

                    mDtSummary.Rows.Add(mNewRow);
                    mDtSummary.AcceptChanges();
                }

                #endregion

                #endregion

                DataSet mDsData = new DataSet("data");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtSummary);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #region BIL_DebtorsList
        public DataSet BIL_DebtorsList(DateTime mTransDate, string mExtraFilter, string mBalanceFilter, string mExtraParameters)
        {
            string mFunctionName = "BIL_DebtorsList";

            int mRowIndex = -1;
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
                string mCommandText = "";
                mTransDate = mTransDate.Date;

                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtDebtorsList = new DataTable("debtorslist");
                DataTable mDtCorporates = new DataTable("corporates");
                DataTable mDtPatients = new DataTable("patients");
                DataTable mDt0to30 = new DataTable("0to30");
                DataTable mDt31to60 = new DataTable("31to60");
                DataTable mDt61to90 = new DataTable("61to90");
                DataTable mDt91andAbove = new DataTable("91andAbove");

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_transdate", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));
                mDtFacilitySetup.Columns.Add("keyvalue", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_transdate"] = mTransDate;
                    mDataRow["para_otherparameters"] = mExtraParameters;
                    mDataRow["keyvalue"] = "parentlink";

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                string mStartDateStr = "transdate";
                string mEndDateStr = clsGlobal.Saving_DateValue(mTransDate);

                mCommand.CommandText = "select * from patients";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtPatients);

                DataView mDvPatients = new DataView();
                mDvPatients.Table = mDtPatients;
                mDvPatients.Sort = "code";

                DataView mDv0to30 = new DataView();
                DataView mDv31to60 = new DataView();
                DataView mDv61to90 = new DataView();
                DataView mDv91andAbove = new DataView();

                mDtDebtorsList.Columns.Add("debtortype", typeof(System.String));
                mDtDebtorsList.Columns.Add("accountcode", typeof(System.String));
                mDtDebtorsList.Columns.Add("accountdescription", typeof(System.String));
                mDtDebtorsList.Columns.Add("0to30", typeof(System.Double));
                mDtDebtorsList.Columns.Add("31to60", typeof(System.Double));
                mDtDebtorsList.Columns.Add("61to90", typeof(System.Double));
                mDtDebtorsList.Columns.Add("91andAbove", typeof(System.Double));

                string mPatientColumns = "";
                foreach (DataColumn mDataColumn in mDtPatients.Columns)
                {
                    if (mDataColumn.ColumnName.ToLower() != "autocode")
                    {
                        mDtDebtorsList.Columns.Add(mDataColumn.ColumnName, mDataColumn.DataType);

                        if (mPatientColumns.Trim() == "")
                        {
                            mPatientColumns = "pt." + mDataColumn.ColumnName;
                        }
                        else
                        {
                            mPatientColumns = mPatientColumns + ",pt." + mDataColumn.ColumnName;
                        }
                    }
                }

                #region generate data

                #region 0 to 30

                mCommand.CommandText = "select patientcode,sum(balancedue) balance from billinvoices "
                + "where (" + clsGlobal.DateDiff_InDays(mStartDateStr, mEndDateStr) + " between 0 and 30) and billinggroupcode='' and voided<>1";

                mCommand.CommandText = mCommand.CommandText + " group by patientcode";

                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDt0to30);

                #endregion

                #region 0 to 30

                mCommand.CommandText = "select patientcode,sum(balancedue) balance from billinvoices "
                + "where (" + clsGlobal.DateDiff_InDays(mStartDateStr, mEndDateStr) + " between 0 and 30) and billinggroupcode='' and voided<>1";

                mCommand.CommandText = mCommand.CommandText + " group by patientcode";

                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDt0to30);

                #endregion

                #region 31 to 60

                mCommand.CommandText = "select patientcode,sum(balancedue) balance from billinvoices "
                + "where (" + clsGlobal.DateDiff_InDays(mStartDateStr, mEndDateStr) + " between 31 and 60) and billinggroupcode='' and voided<>1";

                mCommand.CommandText = mCommand.CommandText + " group by patientcode";

                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDt31to60);

                #endregion

                #region 61 to 90

                mCommand.CommandText = "select patientcode,sum(balancedue) balance from billinvoices "
                + "where (" + clsGlobal.DateDiff_InDays(mStartDateStr, mEndDateStr) + " between 61 and 90) and billinggroupcode='' and voided<>1";

                mCommand.CommandText = mCommand.CommandText + " group by patientcode";

                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDt61to90);

                #endregion

                #region 91 and Above

                mCommand.CommandText = "select patientcode,sum(balancedue) balance from billinvoices "
                + "where (" + clsGlobal.DateDiff_InDays(mStartDateStr, mEndDateStr) + " >= 91) and billinggroupcode='' and voided<>1";

                mCommand.CommandText = mCommand.CommandText + " group by patientcode";

                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDt91andAbove);

                #endregion

                DataTable mDtDebtorsIndividual = new DataTable("debtors");
                mCommandText = "select * from billdebtors where (debtortype='individual')";
                if (mBalanceFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mBalanceFilter + ")";
                }

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtDebtorsIndividual);

                mDv0to30.Table = mDt0to30;
                mDv0to30.Sort = "patientcode";

                mDv31to60.Table = mDt31to60;
                mDv31to60.Sort = "patientcode";

                mDv61to90.Table = mDt61to90;
                mDv61to90.Sort = "patientcode";

                mDv91andAbove.Table = mDt91andAbove;
                mDv91andAbove.Sort = "patientcode";

                foreach (DataRow mDataRow in mDtDebtorsIndividual.Rows)
                {
                    string mAccountCode = mDataRow["accountcode"].ToString().Trim();
                    string mAccountDescription = mDataRow["accountdescription"].ToString().Trim();
                    string mDebtorType = "INDIVIDUALS";
                    double m0to30 = 0;
                    double m31to60 = 0;
                    double m61to90 = 0;
                    double m91andAbove = 0;

                    mRowIndex = mDv0to30.Find(mAccountCode);
                    if (mRowIndex >= 0)
                    {
                        m0to30 = Convert.ToDouble(mDv0to30[mRowIndex]["balance"]);
                    }

                    mRowIndex = mDv31to60.Find(mAccountCode);
                    if (mRowIndex >= 0)
                    {
                        m31to60 = Convert.ToDouble(mDv31to60[mRowIndex]["balance"]);
                    }

                    mRowIndex = mDv61to90.Find(mAccountCode);
                    if (mRowIndex >= 0)
                    {
                        m61to90 = Convert.ToDouble(mDv61to90[mRowIndex]["balance"]);
                    }

                    mRowIndex = mDv91andAbove.Find(mAccountCode);
                    if (mRowIndex >= 0)
                    {
                        m91andAbove = Convert.ToDouble(mDv91andAbove[mRowIndex]["balance"]);
                    }

                    DataRow mNewRow = mDtDebtorsList.NewRow();


                    mRowIndex = mDvPatients.Find(mAccountCode);
                    if (mRowIndex >= 0)
                    {
                        mAccountDescription = mDvPatients[mRowIndex]["firstname"].ToString().Trim();
                        if (mDvPatients[mRowIndex]["othernames"].ToString().Trim() != "")
                        {
                            mAccountDescription = mAccountDescription + " " + mDvPatients[mRowIndex]["othernames"].ToString().Trim();
                        }
                        mAccountDescription = mAccountDescription + " " + mDvPatients[mRowIndex]["surname"].ToString().Trim();

                        foreach (DataColumn mDataColumn in mDtPatients.Columns)
                        {
                            if (mDataColumn.ColumnName.ToLower() != "autocode")
                            {
                                mNewRow[mDataColumn.ColumnName] = mDvPatients[mRowIndex][mDataColumn.ColumnName];
                            }
                        }
                    }

                    mNewRow["debtortype"] = mDebtorType;
                    mNewRow["accountcode"] = mAccountCode;
                    mNewRow["accountdescription"] = mAccountDescription;
                    mNewRow["0to30"] = m0to30;
                    mNewRow["31to60"] = m31to60;
                    mNewRow["61to90"] = m61to90;
                    mNewRow["91andAbove"] = m91andAbove;
                    mDtDebtorsList.Rows.Add(mNewRow);
                    mDtDebtorsList.AcceptChanges();
                }

                #endregion

                DataView mDvDebtorsList = new DataView();
                mDvDebtorsList.Table = mDtDebtorsList;
                mDvDebtorsList.RowFilter = mExtraFilter;

                DataTable mDtDebtorsListFiltered = mDvDebtorsList.ToTable().Copy();

                DataSet mDsData = new DataSet("summary");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtDebtorsListFiltered);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #region BIL_DailyIncome
        public DataSet BIL_DailyIncome(DateTime mDateFrom, DateTime mDateTo, bool mDateBased, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "BIL_DailyIncome";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                DataRow mDataRow;
                int mRowIndex = -1;
                double mAmount = 0;
                string mCommandText = "";

                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtParent = new DataTable("parent");
                DataTable mDtCash = new DataTable("cash");
                DataTable mDtInvoices = new DataTable("invoices");
                DataTable mDtPayments = new DataTable("payments");

                mDtCash.Columns.Add("keyvalue", typeof(System.String));
                mDtCash.Columns.Add("groupcode", typeof(System.String));
                mDtCash.Columns.Add("groupdescription", typeof(System.String));
                mDtCash.Columns.Add("amount", typeof(System.Double));

                mDtInvoices.Columns.Add("keyvalue", typeof(System.String));
                mDtInvoices.Columns.Add("groupcode", typeof(System.String));
                mDtInvoices.Columns.Add("groupdescription", typeof(System.String));
                mDtInvoices.Columns.Add("amount", typeof(System.Double));

                mDtPayments.Columns.Add("keyvalue", typeof(System.String));
                mDtPayments.Columns.Add("groupcode", typeof(System.String));
                mDtPayments.Columns.Add("groupdescription", typeof(System.String));
                mDtPayments.Columns.Add("amount", typeof(System.Double));

                DataTable mDtGroups = new DataTable("billinggroups");
                mCommand.CommandText = "select * from facilitybillinggroups";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtGroups);

                DataRow mNewRow = mDtGroups.NewRow();
                mNewRow["code"] = "pharmacy";
                mNewRow["description"] = "Pharmacy";
                mDtGroups.Rows.Add(mNewRow);
                mDtGroups.AcceptChanges();

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_datefrom", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_dateto", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_datefrom"] = mDateFrom;
                    mDataRow["para_dateto"] = mDateTo;
                    mDataRow["para_otherparameters"] = mExtraParameters;

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region prepare parent datatable

                mDtParent.Columns.Add("keyvalue", typeof(System.String));

                mDataRow = mDtParent.NewRow();
                mDataRow["keyvalue"] = "cash";
                mDtParent.Rows.Add(mDataRow);
                mDtParent.AcceptChanges();

                mDataRow = mDtParent.NewRow();
                mDataRow["keyvalue"] = "invoices";
                mDtParent.Rows.Add(mDataRow);
                mDtParent.AcceptChanges();

                mDataRow = mDtParent.NewRow();
                mDataRow["keyvalue"] = "payments";
                mDtParent.Rows.Add(mDataRow);
                mDtParent.AcceptChanges();

                #endregion

                #region cash

                DataTable mDtCashSales = new DataTable("cashsales");
                mCommandText = "select itemgroupcode,sum(amount) amount from billsalesitems "
                + "where (transdate between " + clsGlobal.Saving_DateValue(mDateFrom) + " and " + clsGlobal.Saving_DateValue(mDateTo)
                + ") and invoiceno=''";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }
                mCommandText = mCommandText + " group by itemgroupcode";

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtCashSales);
                DataView mDvCashSales = new DataView();
                mDvCashSales.Table = mDtCashSales;
                mDvCashSales.Sort = "itemgroupcode";

                foreach (DataRow mGroupDataRow in mDtGroups.Rows)
                {
                    mAmount = 0;
                    mRowIndex = mDvCashSales.Find(mGroupDataRow["code"]);
                    if (mRowIndex >= 0)
                    {
                        mAmount = Convert.ToDouble(mDvCashSales[mRowIndex]["amount"]);
                    }

                    mDataRow = mDtCash.NewRow();
                    mDataRow["keyvalue"] = "cash";
                    mDataRow["groupcode"] = mGroupDataRow["code"];
                    mDataRow["groupdescription"] = mGroupDataRow["description"];
                    mDataRow["amount"] = mAmount;
                    mDtCash.Rows.Add(mDataRow);
                    mDtCash.AcceptChanges();
                }

                #endregion

                #region invoiced

                DataTable mDtInvoiceSales = new DataTable("cashsales");
                mCommandText = "select itemgroupcode,sum(amount) amount from billsalesitems "
                + "where (transdate between " + clsGlobal.Saving_DateValue(mDateFrom) + " and " + clsGlobal.Saving_DateValue(mDateTo)
                + ") and invoiceno<>''";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }
                mCommandText = mCommandText + " group by itemgroupcode";

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtInvoiceSales);
                DataView mDvInvoiceSales = new DataView();
                mDvInvoiceSales.Table = mDtInvoiceSales;
                mDvInvoiceSales.Sort = "itemgroupcode";

                foreach (DataRow mGroupDataRow in mDtGroups.Rows)
                {
                    mAmount = 0;
                    mRowIndex = mDvInvoiceSales.Find(mGroupDataRow["code"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mAmount = Convert.ToDouble(mDvInvoiceSales[mRowIndex]["amount"]);
                    }

                    mDataRow = mDtInvoices.NewRow();
                    mDataRow["keyvalue"] = "invoices";
                    mDataRow["groupcode"] = mGroupDataRow["code"];
                    mDataRow["groupdescription"] = mGroupDataRow["description"];
                    mDataRow["amount"] = mAmount;
                    mDtInvoices.Rows.Add(mDataRow);
                    mDtInvoices.AcceptChanges();
                }

                #endregion

                #region receipts from debtors

                string mPaymentTypes = "";
                DataTable mDtColumns = new DataTable("columns");
                mCommand.CommandText = "select * from billinvoicepayments where 1=2";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtColumns);

                foreach (DataColumn mDataColumn in mDtColumns.Columns)
                {
                    if (mDataColumn.ColumnName.ToLower() != "paytypedbtrel")
                    {
                        if (mDataColumn.ColumnName.ToLower().StartsWith("paytype") == true)
                        {
                            if (mPaymentTypes.Trim() == "")
                            {
                                mPaymentTypes = mDataColumn.ColumnName;
                            }
                            else
                            {
                                mPaymentTypes = mPaymentTypes + "+" + mDataColumn.ColumnName;
                            }
                        }
                    }
                }

                DataTable mDtInvoicePayments = new DataTable("invoicepayments");
                mCommandText = "select bc.billinggroupcode,fc.description billinggroupdescription, sum(" + mPaymentTypes
                + ") amount from billinvoicepayments bc left join facilitycorporates fc on bc.billinggroupcode=fc.code "
                + "where (bc.transdate between " + clsGlobal.Saving_DateValue(mDateFrom) + " and " + clsGlobal.Saving_DateValue(mDateTo) + ")";
                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }
                mCommandText = mCommandText + " group by bc.billinggroupcode";
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtInvoicePayments);

                foreach (DataRow mPaymentRow in mDtInvoicePayments.Rows)
                {
                    string mGroupCode = "";
                    string mGroupDescription = "";
                    if (mPaymentRow["billinggroupcode"].ToString().Trim() == "")
                    {
                        mGroupCode = "Individuals";
                        mGroupDescription = "Individuals";
                    }
                    else
                    {
                        mGroupCode = mPaymentRow["billinggroupcode"].ToString();
                        mGroupDescription = mPaymentRow["billinggroupdescription"].ToString();
                    }
                    mDataRow = mDtPayments.NewRow();
                    mDataRow["keyvalue"] = "payments";
                    mDataRow["groupcode"] = mGroupCode;
                    mDataRow["groupdescription"] = mGroupDescription;
                    mDataRow["amount"] = Convert.ToDouble(mPaymentRow["amount"]);
                    mDtPayments.Rows.Add(mDataRow);
                    mDtPayments.AcceptChanges();
                }

                #endregion

                DataSet mDsData = new DataSet("dailysalessummary");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtParent);
                mDsData.Tables.Add(mDtCash);
                mDsData.Tables.Add(mDtInvoices);
                mDsData.Tables.Add(mDtPayments);

                #region build relationships

                mDsData.Relations.Add("cashrelationship", mDtParent.Columns["keyvalue"], mDtCash.Columns["keyvalue"]);
                mDsData.Relations.Add("invoicesrelationship", mDtParent.Columns["keyvalue"], mDtInvoices.Columns["keyvalue"]);
                mDsData.Relations.Add("paymentsrelationship", mDtParent.Columns["keyvalue"], mDtPayments.Columns["keyvalue"]);

                #endregion

                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #endregion

        #region laboratory

        #region LAB_PatientTestsCount
        public DataSet LAB_PatientTestsCount(DateTime mDateFrom, DateTime mDateTo, bool mDateBased, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "LAB_PatientTestsCount";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                string mCommandText = "";
                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtSummary = new DataTable("summary");

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_datefrom", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_dateto", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_datefrom"] = mDateFrom;
                    mDataRow["para_dateto"] = mDateTo;
                    mDataRow["para_otherparameters"] = mExtraParameters;

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region labtests

                mCommandText = "SELECT "
                    + "pt.labtestgroupcode,"
                    + "gp.description labtestgroupdescription,"
                    + "pt.labtesttypecode,"
                    + "lt.description labtesttypedescription,"
                    + "lt.displayname,"
                    + "lt.units,"
                    + "SUM(pt.males) males,"
                    + "SUM(pt.females) females "
                    + "FROM view_labpatienttestscount  pt "
                    + "LEFT JOIN labtestgroups gp ON pt.labtestgroupcode=gp.code "
                    + "LEFT JOIN labtests lt ON pt.labtesttypecode=lt.code "
                    + "WHERE (transdate between " + clsGlobal.Saving_DateValue(mDateFrom) + " and " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                    + "AND lt.additionalinfo<>1";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }

                mCommandText = mCommandText + " GROUP BY pt.labtestgroupcode,pt.labtesttypecode";

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSummary);

                #endregion

                DataSet mDsData = new DataSet("data");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtSummary);

                return mDsData;
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

        #region LAB_CountByUser
        public DataSet LAB_CountByLabTechnician(DateTime mDateFrom, DateTime mDateTo, bool mDateBased, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "LAB_CountByLabTechnician";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                string mCommandText = "";
                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtDiagnosesSummary = new DataTable("summary");

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_datefrom", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_dateto", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));
                mDtFacilitySetup.Columns.Add("keyvalue", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_datefrom"] = mDateFrom;
                    mDataRow["para_dateto"] = mDateTo;
                    mDataRow["para_otherparameters"] = mExtraParameters;
                    mDataRow["keyvalue"] = "parentlink";

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region generate data

                mCommandText = "SELECT "
                    + "'parentlink' keyvalue,"
                    + "labtechniciancode,"
                    + "labtechniciandescription,"
                    + "case when p.gender='m' then 1 else 0 end male,"
                    + "case when p.gender='f' then 1 else 0 end female "
                    + "FROM view_labpatienttests b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                    + "WHERE (transdate between " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                    + "AND b.additionalinfo<>1";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }

                mCommandText = mCommandText + " group by labtechniciancode,labtechniciandescription,patientcode";
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtDiagnosesSummary);

                #endregion

                DataSet mDsData = new DataSet("summary");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtDiagnosesSummary);
                mDsData.Relations.Add("childrelationship", mDtFacilitySetup.Columns["keyvalue"], mDtDiagnosesSummary.Columns["keyvalue"]);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #region LAB_CountByUser
        public DataSet LAB_CountByResult(DateTime mDateFrom, DateTime mDateTo, bool mDateBased, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "LAB_CountByUser";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                string mCommandText = "";
                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtDiagnosesSummary = new DataTable("summary");

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_datefrom", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_dateto", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));
                mDtFacilitySetup.Columns.Add("keyvalue", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_datefrom"] = mDateFrom;
                    mDataRow["para_dateto"] = mDateTo;
                    mDataRow["para_otherparameters"] = mExtraParameters;
                    mDataRow["keyvalue"] = "parentlink";

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region generate data

                mCommandText = "SELECT "
                    + "'parentlink' keyvalue,"
                    + "labtestgroupcode,"
                    + "labtestgroupdescription,"
                    + "labtestsubgroupcode,"
                    + "labtestsubgroupdescription,"
                    + "labtesttypecode,"
                    + "labtesttypedescription,"
                    + "b.results,"
                    + "sum(case when p.gender='m' then 1 else 0 end) males,"
                    + "sum(case when p.gender='f' then 1 else 0 end) females "
                    + "FROM view_labpatienttests b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                    + "WHERE (transdate between " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                    + "AND b.additionalinfo<>1";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }

                mCommandText = mCommandText + " group by labtestgroupcode,labtestgroupdescription,labtestsubgroupcode,labtestsubgroupdescription,labtesttypecode,labtesttypedescription,b.results";
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtDiagnosesSummary);

                #endregion

                DataSet mDsData = new DataSet("summary");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtDiagnosesSummary);
                mDsData.Relations.Add("childrelationship", mDtFacilitySetup.Columns["keyvalue"], mDtDiagnosesSummary.Columns["keyvalue"]);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #region LAB_ListingByResult
        public DataTable LAB_ListingByResult(DateTime mDateFrom, DateTime mDateTo, bool mDateBased, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "LAB_ListingByResult";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                string mFieldName = "";
                string mFieldValue = "";
                string mCommandText = "";
                DataTable mDtData = new DataTable("data");

                string mFields = "";

                #region facilitysetup

                DataTable mDtFacilityOptions = new DataTable("facilityoptions");
                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilityOptions);

                if (mDtFacilityOptions.Rows.Count > 0)
                {
                    foreach (DataColumn mDataColumn in mDtFacilityOptions.Columns)
                    {
                        if (mDataColumn.ColumnName.ToLower() != "autocode")
                        {
                            mFieldName = "facility_" + mDataColumn.ColumnName;
                            mFieldValue = "";

                            #region build fieldvalue

                            switch (mDataColumn.DataType.FullName.Trim().ToLower())
                            {
                                case "system.decimal":
                                    {
                                        mFieldValue = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Double));
                                    }
                                    break;
                                case "system.double":
                                    {
                                        mFieldValue = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Double));
                                    }
                                    break;
                                case "system.int16":
                                    {
                                        mFieldValue = Convert.ToInt16(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int16));
                                    }
                                    break;
                                case "system.int32":
                                    {
                                        mFieldValue = Convert.ToInt32(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int32));
                                    }
                                    break;
                                case "system.int64":
                                    {
                                        mFieldValue = Convert.ToInt64(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int64));
                                    }
                                    break;
                                case "system.single":
                                    {
                                        mFieldValue = Convert.ToSingle(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Single));
                                    }
                                    break;
                                case "system.datetime":
                                    {
                                        mFieldValue = clsGlobal.Saving_DateValue(Convert.ToDateTime(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]));
                                        mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                                    }
                                    break;
                                default:
                                    {
                                        mFieldValue = "'" + mDtFacilityOptions.Rows[0][mDataColumn.ColumnName].ToString() + "'";
                                        mDtData.Columns.Add(mFieldName, typeof(System.String));
                                    }
                                    break;
                            }
                            #endregion

                            if (mFields.Trim() == "")
                            {
                                mFields = Environment.NewLine + " " + mFieldValue + " " + mFieldName;
                            }
                            else
                            {
                                mFields = mFields + Environment.NewLine + ", " + mFieldValue + " " + mFieldName;
                            }
                        }
                    }
                }

                #endregion

                #region add parameters used

                #region datefrom
                mFieldName = "para_datefrom";
                mFieldValue = clsGlobal.Saving_DateValue(mDateFrom);
                mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                if (mFields.Trim() == "")
                {
                    mFields = mFieldValue + " " + mFieldName;
                }
                else
                {
                    mFields = mFields + "," + mFieldValue + " " + mFieldName;
                }
                #endregion

                #region dateto
                mFieldName = "para_dateto";
                mFieldValue = clsGlobal.Saving_DateValue(mDateTo);
                mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                if (mFields.Trim() == "")
                {
                    mFields = mFieldValue + " " + mFieldName;
                }
                else
                {
                    mFields = mFields + "," + mFieldValue + " " + mFieldName;
                }
                #endregion

                #region extra parameters
                mFieldName = "para_otherparameters";
                mFieldValue = "'" + mExtraParameters + "'";
                mDtData.Columns.Add(mFieldName, typeof(System.String));
                if (mFields.Trim() == "")
                {
                    mFields = mFieldValue + " " + mFieldName;
                }
                else
                {
                    mFields = mFields + "," + mFieldValue + " " + mFieldName;
                }
                #endregion

                #endregion

                //columns from patients
                mFields = mFields + "," + clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "patient");
                mFields = mFields + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "patientfullname");
                mFields = mFields + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "b.transdate",""), "patientage");
                //columns from trans
                mFields = mFields + "," + clsGlobal.Get_TableColumns(mCommand, "view_labpatienttests", "", "b", "");

                mCommandText = ""
                    + "SELECT "
                    + mFields + " "
                    + "FROM view_labpatienttests AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                    + "WHERE (b.transdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                    + "AND b.additionalinfo<>1";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);
                mDtData.RemotingFormat = SerializationFormat.Binary;

                return mDtData;
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

        #region LAB_CountMonthly
        public DataSet LAB_CountMonthly(int mYear, int mMonthFrom, int mMonthTo, DataTable mDtSelected, string mExtraParameters)
        {
            string mFunctionName = "LAB_CountMonthly";

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
                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtSummary = new DataTable("summary");

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_monthfrom", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_monthto", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));
                mDtFacilitySetup.Columns.Add("keyvalue", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    string mMonthFromName = clsGlobal.Get_MonthName(mMonthFrom);
                    string mMonthToName = clsGlobal.Get_MonthName(mMonthTo);

                    mDataRow["para_monthfrom"] = mMonthFromName;
                    mDataRow["para_monthto"] = mMonthToName;
                    mDataRow["para_otherparameters"] = mExtraParameters;
                    mDataRow["keyvalue"] = "parentlink";

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region generate data

                string mSelectedTests = "";
                foreach (DataRow mDataRow in mDtSelected.Rows)
                {
                    if (mSelectedTests.Trim() == "")
                    {
                        mSelectedTests = "'" + mDataRow["code"].ToString().Trim() + "'";
                    }
                    else
                    {
                        mSelectedTests = mSelectedTests + ",'" + mDataRow["code"].ToString().Trim() + "'";
                    }
                }

                if (mSelectedTests.Trim() == "")
                {
                    mSelectedTests = "1=2";
                }
                else
                {
                    mSelectedTests = "code in (" + mSelectedTests + ")";
                }

                //tests
                DataTable mDtLabTests = new DataTable("labtests");
                mCommand.CommandText = "SELECT * FROM labtests where " + mSelectedTests;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtLabTests);

                //distinct test results
                DataTable mDtTests = new DataTable("results");
                mCommand.CommandText = "SELECT * FROM view_labresultslist ORDER BY labtesttypecode, displayorder, description";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtTests);

                //patient tests
                DataTable mDtResults = new DataTable("tests");
                mCommand.CommandText = "SELECT "
                    + "'parentlink' keyvalue,b.* "
                    + "FROM view_labmonthlyresultscount b "
                    + "WHERE yearpart=" + mYear + " and monthpart between " + mMonthFrom + " and " + mMonthTo;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtResults);

                foreach (DataColumn mDataColumn in mDtResults.Columns)
                {
                    mDtSummary.Columns.Add(mDataColumn.ColumnName, mDataColumn.DataType);
                }

                DataView mDvResults = new DataView();
                mDvResults.Table = mDtResults;
                mDvResults.Sort = "monthpart,labtesttypecode,results";

                DataView mDvTests = new DataView();
                mDvTests.Table = mDtTests;
                mDvTests.Sort = "labtesttypecode";

                //generate reporting data
                for (int mMonth = mMonthFrom; mMonth <= mMonthTo; mMonth++)
                {
                    foreach (DataRow mTestRow in mDtLabTests.Rows)
                    {
                        object[] mTest = new object[1];
                        mTest[0] = mTestRow["code"].ToString().Trim();

                        DataRowView[] mTestRows = mDvTests.FindRows(mTest);

                        foreach (DataRowView mDataRow in mTestRows)
                        {
                            int myearpart = mYear;
                            int mmonthpart = mMonth;
                            string mmonthpartname = clsGlobal.Get_MonthName(mMonth);
                            string mlabtesttypecode = mDataRow["labtesttypecode"].ToString().Trim();
                            string mlabtesttypedescription = mDataRow["labtesttypedescription"].ToString().Trim();
                            string mresults = mDataRow["description"].ToString().Trim();
                            int mmaleu18 = 0;
                            int mfemaleu18 = 0;
                            int mtotalu18 = 0;
                            int mmale18 = 0;
                            int mfemale18 = 0;
                            int mtotal18 = 0;
                            int mtotalmale = 0;
                            int mtotalfemale = 0;
                            int mtotal = 0;

                            object[] mData = new object[3];
                            mData[0] = mMonth;
                            mData[1] = mlabtesttypecode;
                            mData[2] = mresults;

                            int mRowIndex = mDvResults.Find(mData);
                            if (mRowIndex >= 0)
                            {
                                mmaleu18 = Convert.ToInt32(mDvResults[mRowIndex]["maleu18"]);
                                mfemaleu18 = Convert.ToInt32(mDvResults[mRowIndex]["femaleu18"]);
                                mtotalu18 = Convert.ToInt32(mDvResults[mRowIndex]["totalu18"]);
                                mmale18 = Convert.ToInt32(mDvResults[mRowIndex]["male18"]);
                                mfemale18 = Convert.ToInt32(mDvResults[mRowIndex]["female18"]);
                                mtotal18 = Convert.ToInt32(mDvResults[mRowIndex]["total18"]);
                                mtotalmale = Convert.ToInt32(mDvResults[mRowIndex]["totalmale"]);
                                mtotalfemale = Convert.ToInt32(mDvResults[mRowIndex]["totalfemale"]);
                                mtotal = Convert.ToInt32(mDvResults[mRowIndex]["total"]);
                            }

                            DataRow mNewRow = mDtSummary.NewRow();
                            mNewRow["keyvalue"] = "parentlink";
                            mNewRow["yearpart"] = myearpart;
                            mNewRow["monthpart"] = mmonthpart;
                            mNewRow["monthpartname"] = mmonthpartname;
                            mNewRow["labtesttypecode"] = mlabtesttypecode;
                            mNewRow["labtesttypedescription"] = mlabtesttypedescription;
                            mNewRow["results"] = mresults;
                            mNewRow["maleu18"] = mmaleu18;
                            mNewRow["femaleu18"] = mfemaleu18;
                            mNewRow["totalu18"] = mtotalu18;
                            mNewRow["male18"] = mmale18;
                            mNewRow["female18"] = mfemale18;
                            mNewRow["total18"] = mtotal18;
                            mNewRow["totalmale"] = mtotalmale;
                            mNewRow["totalfemale"] = mtotalfemale;
                            mNewRow["total"] = mtotal;
                            mDtSummary.Rows.Add(mNewRow);
                            mDtSummary.AcceptChanges();
                        }
                    }
                }

                #endregion

                DataSet mDsData = new DataSet("summary");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtSummary);
                mDsData.Relations.Add("childrelationship", mDtFacilitySetup.Columns["keyvalue"], mDtSummary.Columns["keyvalue"]);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #endregion

        #region diagnoses and treatments

        #region DXT_DiagnosesSummary
        public DataSet DXT_DiagnosesSummary(DateTime mDateFrom, DateTime mDateTo, bool mDateBased, bool mPrimaryOnly, int mDepartment, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "DXT_DiagnosesSummary";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                string mCommandText = "";
                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtDiagnosesSummary = new DataTable("diagnosessummary");

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_datefrom", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_dateto", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));
                mDtFacilitySetup.Columns.Add("keyvalue", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_datefrom"] = mDateFrom;
                    mDataRow["para_dateto"] = mDateTo;
                    mDataRow["para_otherparameters"] = mExtraParameters;
                    mDataRow["keyvalue"] = "parentlink";

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region generate data

                switch (mDepartment)
                {
                    case 1:
                        {
                            mCommandText = "SELECT "
                                        + "'parentlink' keyvalue,"
                                        + "b.diagnosiscode,"
                                        + "dx.description AS diagnosisdescription,"
                                        + "sum(case when p.gender='m' then 1 else 0 end) males,"
                                        + "sum(case when p.gender='f' then 1 else 0 end) females "
                                        + "FROM view_dxtfirstencounters b "
                                        + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                                        + "LEFT OUTER JOIN dxticddiagnoses AS dx ON b.diagnosiscode=dx.code "
                                        + "WHERE (transdate between " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                                        + "AND (b.wardcode IS NULL OR b.wardcode='') AND (ipddiagnosistype<>'admission')";

                            if (mPrimaryOnly == true)
                            {
                                mCommandText = mCommandText + " and (isprimary=1)";
                            }

                            if (mExtraFilter.Trim() != "")
                            {
                                mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                            }

                            mCommandText = mCommandText + " group by diagnosiscode";
                        }
                        break;
                    case 2:
                        {
                            mCommandText = "SELECT "
                                        + "'parentlink' keyvalue,"
                                        + "b.diagnosiscode,"
                                        + "dx.description AS diagnosisdescription,"
                                        + "b.wardcode,"
                                        + "CASE WHEN b.wardcode IS NULL OR b.wardcode='' THEN 'OUTPATIENT' ELSE w.description END AS warddescription,"
                                        + "roomcode,"
                                        + "CASE WHEN roomcode IS NULL OR roomcode='' THEN 'OUTPATIENT' ELSE wr.description END AS roomdescription,"
                                        + "sum(case when p.gender='m' then 1 else 0 end) males,"
                                        + "sum(case when p.gender='f' then 1 else 0 end) females "
                                        + "FROM view_dxtfirstencounters b "
                                        + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                                        + "LEFT OUTER JOIN facilitywards AS w ON b.wardcode=w.code "
                                        + "LEFT OUTER JOIN facilitywardrooms AS wr ON b.roomcode=wr.code "
                                        + "LEFT OUTER JOIN dxticddiagnoses AS dx ON b.diagnosiscode=dx.code "
                                        + "WHERE (transdate between " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                                        + "AND (b.wardcode IS NOT NULL AND b.wardcode<>'') AND (ipddiagnosistype<>'admission')";

                            if (mPrimaryOnly == true)
                            {
                                mCommandText = mCommandText + " and (isprimary=1)";
                            }

                            if (mExtraFilter.Trim() != "")
                            {
                                mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                            }

                            mCommandText = mCommandText + " group by diagnosiscode,b.wardcode,roomcode";
                        }
                        break;
                    default:
                        {
                            mCommandText = "SELECT "
                                        + "'parentlink' keyvalue,"
                                        + "b.diagnosiscode,"
                                        + "dx.description AS diagnosisdescription,"
                                        + "sum(case when p.gender='m' then 1 else 0 end) males,"
                                        + "sum(case when p.gender='f' then 1 else 0 end) females "
                                        + "FROM view_dxtfirstencounters b "
                                        + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                                        + "LEFT OUTER JOIN dxticddiagnoses AS dx ON b.diagnosiscode=dx.code "
                                        + "WHERE (transdate between " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                                        + "AND (ipddiagnosistype<>'admission')";

                            if (mPrimaryOnly == true)
                            {
                                mCommandText = mCommandText + " and (isprimary=1)";
                            }

                            if (mExtraFilter.Trim() != "")
                            {
                                mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                            }

                            mCommandText = mCommandText + " group by diagnosiscode";
                        }
                        break;
                }

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtDiagnosesSummary);

                #endregion

                DataSet mDsData = new DataSet("summary");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtDiagnosesSummary);
                mDsData.Relations.Add("childrelationship", mDtFacilitySetup.Columns["keyvalue"], mDtDiagnosesSummary.Columns["keyvalue"]);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #region DXT_DiagnosesListing
        public DataTable DXT_DiagnosesListing(DateTime mDateFrom, DateTime mDateTo, bool mDateBased, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "DXT_DiagnosesListing";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                string mFieldName = "";
                string mFieldValue = "";
                string mCommandText = "";
                DataTable mDtData = new DataTable("data");

                string mFields = "";

                #region facilitysetup

                DataTable mDtFacilityOptions = new DataTable("facilityoptions");
                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilityOptions);

                if (mDtFacilityOptions.Rows.Count > 0)
                {
                    foreach (DataColumn mDataColumn in mDtFacilityOptions.Columns)
                    {
                        if (mDataColumn.ColumnName.ToLower() != "autocode")
                        {
                            mFieldName = "facility_" + mDataColumn.ColumnName;
                            mFieldValue = "";

                            #region build fieldvalue

                            switch (mDataColumn.DataType.FullName.Trim().ToLower())
                            {
                                case "system.decimal":
                                    {
                                        mFieldValue = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Double));
                                    }
                                    break;
                                case "system.double":
                                    {
                                        mFieldValue = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Double));
                                    }
                                    break;
                                case "system.int16":
                                    {
                                        mFieldValue = Convert.ToInt16(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int16));
                                    }
                                    break;
                                case "system.int32":
                                    {
                                        mFieldValue = Convert.ToInt32(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int32));
                                    }
                                    break;
                                case "system.int64":
                                    {
                                        mFieldValue = Convert.ToInt64(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int64));
                                    }
                                    break;
                                case "system.single":
                                    {
                                        mFieldValue = Convert.ToSingle(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Single));
                                    }
                                    break;
                                case "system.datetime":
                                    {
                                        mFieldValue = clsGlobal.Saving_DateValue(Convert.ToDateTime(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]));
                                        mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                                    }
                                    break;
                                default:
                                    {
                                        mFieldValue = "'" + mDtFacilityOptions.Rows[0][mDataColumn.ColumnName].ToString() + "'";
                                        mDtData.Columns.Add(mFieldName, typeof(System.String));
                                    }
                                    break;
                            }
                            #endregion

                            if (mFields.Trim() == "")
                            {
                                mFields = Environment.NewLine + " " + mFieldValue + " " + mFieldName;
                            }
                            else
                            {
                                mFields = mFields + Environment.NewLine + ", " + mFieldValue + " " + mFieldName;
                            }
                        }
                    }
                }

                #endregion

                #region add parameters used

                #region datefrom
                mFieldName = "para_datefrom";
                mFieldValue = clsGlobal.Saving_DateValue(mDateFrom);
                mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                if (mFields.Trim() == "")
                {
                    mFields = mFieldValue + " " + mFieldName;
                }
                else
                {
                    mFields = mFields + "," + mFieldValue + " " + mFieldName;
                }
                #endregion

                #region dateto
                mFieldName = "para_dateto";
                mFieldValue = clsGlobal.Saving_DateValue(mDateTo);
                mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                if (mFields.Trim() == "")
                {
                    mFields = mFieldValue + " " + mFieldName;
                }
                else
                {
                    mFields = mFields + "," + mFieldValue + " " + mFieldName;
                }
                #endregion

                #region extra parameters
                mFieldName = "para_otherparameters";
                mFieldValue = "'" + mExtraParameters + "'";
                mDtData.Columns.Add(mFieldName, typeof(System.String));
                if (mFields.Trim() == "")
                {
                    mFields = mFieldValue + " " + mFieldName;
                }
                else
                {
                    mFields = mFields + "," + mFieldValue + " " + mFieldName;
                }
                #endregion

                #endregion

                //columns from patients
                mFields = mFields + "," + clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "patient");
                mFields = mFields + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "patientfullname");
                mFields = mFields + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "b.transdate",""), "patientage");
                //columns from trans
                mFields = mFields + "," + clsGlobal.Get_TableColumns(mCommand, "view_dxtfirstencounters", "", "b", "");

                mCommandText = ""
                    + "SELECT "
                    + mFields + " "
                    + "FROM view_dxtfirstencounters AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code "
                    + "WHERE (b.transdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                    + "AND ipddiagnosistype<>'admission'";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);
                mDtData.RemotingFormat = SerializationFormat.Binary;

                return mDtData;
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

        #region DXT_PatientHistory
        public DataSet DXT_PatientHistory(string mPatientCode)
        {
            string mFunctionName = "DXT_PatientHistory";

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
                string mCommandText = "";
                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtDiagnosesSummary = new DataTable("diagnosessummary");

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                //columns from patients
                string mPatientColumns = clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,weight,temperature", "p", "patient");
                mPatientColumns = mPatientColumns + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "patientfullname");

                DataTable mDtPatients = new DataTable("patients");
                mCommand.CommandText = "select " + mPatientColumns + " from patients as p where code='" + mPatientCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtPatients);

                mDtFacilitySetup.Columns.Add("keyvalue", typeof(System.String));
                foreach (DataColumn mDataColumn in mDtPatients.Columns)
                {
                    mDtFacilitySetup.Columns.Add(mDataColumn.ColumnName, mDataColumn.DataType);
                }

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["keyvalue"] = "parentlink";

                    if (mDtPatients.Rows.Count > 0)
                    {
                        foreach (DataColumn mDataColumn in mDtPatients.Columns)
                        {
                            mDataRow[mDataColumn.ColumnName] = mDtPatients.Rows[0][mDataColumn.ColumnName];
                        }
                    }

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region generate data

                mDtDiagnosesSummary.Columns.Add("keyvalue", typeof(System.String));
                mDtDiagnosesSummary.Columns.Add("transdate", typeof(System.DateTime));
                mDtDiagnosesSummary.Columns.Add("history", typeof(System.String));
                mDtDiagnosesSummary.Columns.Add("examination", typeof(System.String));
                mDtDiagnosesSummary.Columns.Add("diagnosiscode", typeof(System.String));
                mDtDiagnosesSummary.Columns.Add("diagnosisdescription", typeof(System.String));
                mDtDiagnosesSummary.Columns.Add("details", typeof(System.String));
                mDtDiagnosesSummary.Columns.Add("investigation", typeof(System.String));
                mDtDiagnosesSummary.Columns.Add("treatments", typeof(System.String));

                mCommandText = "SELECT "
                    + "dx.transdate,"
                    + "dx.patientcode,"
                    + "dx.history,"
                    + "dx.examination,"
                    + "dx.diagnosiscode,"
                    + "d.description AS diagnosisdescription,"
                    + "dx.investigation,"
                    + "dx.treatments "
                    + "FROM view_dxtpatientdiagnoseslog dx "
                    + "LEFT OUTER JOIN dxticddiagnoses AS d ON dx.diagnosiscode=d.code "
                    + "WHERE dx.patientcode='" + mPatientCode.Trim() + "' "
                    + "ORDER BY dx.transdate desc, dx.autocode desc";

                DataTable mDtDiagnoses = new DataTable("diagnoses");
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtDiagnoses);

                foreach (DataRow mDataRow in mDtDiagnoses.Rows)
                {
                    string mDetails = "";
                    if (mDataRow["history"].ToString().Trim() != "")
                    {
                        mDetails += mDataRow["history"].ToString().Trim() + Environment.NewLine;
                    }
                    if (mDataRow["examination"].ToString().Trim() != "")
                    {
                        mDetails += mDataRow["examination"].ToString().Trim() + Environment.NewLine;
                    }
                    if (mDataRow["diagnosisdescription"].ToString().Trim() != "")
                    {
                        mDetails += mDataRow["diagnosisdescription"].ToString().Trim() + Environment.NewLine;
                    }

                    DataRow mNewRow = mDtDiagnosesSummary.NewRow();
                    mNewRow["keyvalue"] = "parentlink";
                    mNewRow["transdate"] = mDataRow["transdate"];
                    mNewRow["history"] = mDataRow["history"];
                    mNewRow["examination"] = mDataRow["examination"];
                    mNewRow["diagnosiscode"] = mDataRow["diagnosiscode"];
                    mNewRow["diagnosisdescription"] = mDataRow["diagnosisdescription"];
                    mNewRow["details"] = mDetails;
                    mNewRow["investigation"] = mDataRow["investigation"];
                    mNewRow["treatments"] = mDataRow["treatments"];
                    mDtDiagnosesSummary.Rows.Add(mNewRow);
                    mDtDiagnosesSummary.AcceptChanges();
                }

                #endregion

                DataSet mDsData = new DataSet("summary");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtDiagnosesSummary);
                mDsData.Relations.Add("childrelationship", mDtFacilitySetup.Columns["keyvalue"], mDtDiagnosesSummary.Columns["keyvalue"]);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #endregion

        #region inventory

        #region IV_StockBalance
        public DataTable IV_StockBalance(string mStoreCode, string mStoreDescription, DateTime mTransDate,
            string mBalanceCondition, double mBalanceLimit, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "IV_StockBalance";

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
                mTransDate = mTransDate.Date;

                string mFieldName = "";
                string mFieldValue = "";
                string mCommandText = "";
                DataTable mDtData = new DataTable("data");
                DataTable mDtProducts = new DataTable("products");
                DataTable mDtStock = new DataTable("stock");

                string mFields = "";

                #region facilitysetup

                DataTable mDtFacilityOptions = new DataTable("facilityoptions");
                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilityOptions);

                if (mDtFacilityOptions.Rows.Count > 0)
                {
                    foreach (DataColumn mDataColumn in mDtFacilityOptions.Columns)
                    {
                        if (mDataColumn.ColumnName.ToLower() != "autocode")
                        {
                            mFieldName = "facility_" + mDataColumn.ColumnName;
                            mFieldValue = "";

                            #region build fieldvalue

                            switch (mDataColumn.DataType.FullName.Trim().ToLower())
                            {
                                case "system.decimal":
                                    {
                                        mFieldValue = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Double));
                                    }
                                    break;
                                case "system.double":
                                    {
                                        mFieldValue = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Double));
                                    }
                                    break;
                                case "system.int16":
                                    {
                                        mFieldValue = Convert.ToInt16(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int16));
                                    }
                                    break;
                                case "system.int32":
                                    {
                                        mFieldValue = Convert.ToInt32(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int32));
                                    }
                                    break;
                                case "system.int64":
                                    {
                                        mFieldValue = Convert.ToInt64(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int64));
                                    }
                                    break;
                                case "system.single":
                                    {
                                        mFieldValue = Convert.ToSingle(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Single));
                                    }
                                    break;
                                case "system.datetime":
                                    {
                                        mFieldValue = clsGlobal.Saving_DateValue(Convert.ToDateTime(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]));
                                        mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                                    }
                                    break;
                                default:
                                    {
                                        mFieldValue = "'" + mDtFacilityOptions.Rows[0][mDataColumn.ColumnName].ToString() + "'";
                                        mDtData.Columns.Add(mFieldName, typeof(System.String));
                                    }
                                    break;
                            }
                            #endregion

                            if (mFields.Trim() == "")
                            {
                                mFields = Environment.NewLine + " " + mFieldValue + " " + mFieldName;
                            }
                            else
                            {
                                mFields = mFields + Environment.NewLine + ", " + mFieldValue + " " + mFieldName;
                            }
                        }
                    }
                }

                #endregion

                #region add parameters used

                #region transdate
                mFieldName = "para_transdate";
                mFieldValue = clsGlobal.Saving_DateValue(mTransDate);
                mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                if (mFields.Trim() == "")
                {
                    mFields = mFieldValue + " " + mFieldName;
                }
                else
                {
                    mFields = mFields + "," + mFieldValue + " " + mFieldName;
                }
                #endregion

                #region storedescription
                mFieldName = "para_storedescription";
                mFieldValue = "'" + mStoreDescription + "'";
                mDtData.Columns.Add(mFieldName, typeof(System.String));
                if (mFields.Trim() == "")
                {
                    mFields = mFieldValue + " " + mFieldName;
                }
                else
                {
                    mFields = mFields + "," + mFieldValue + " " + mFieldName;
                }
                #endregion

                #region extra parameters
                mFieldName = "para_otherparameters";
                mFieldValue = "'" + mExtraParameters + "'";
                mDtData.Columns.Add(mFieldName, typeof(System.String));
                if (mFields.Trim() == "")
                {
                    mFields = mFieldValue + " " + mFieldName;
                }
                else
                {
                    mFields = mFields + "," + mFieldValue + " " + mFieldName;
                }
                #endregion

                #endregion

                #region add columns from the table/view

                mCommand.CommandText = "select * from som_products where 1=2";
                DataTable mDtColumns = new DataTable("tableschema");
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.FillSchema(mDtColumns, SchemaType.Source);

                foreach (DataColumn mDataColumn in mDtColumns.Columns)
                {
                    if (mFields.Trim() == "")
                    {
                        mFields = mDataColumn.ColumnName;
                    }
                    else
                    {
                        mFields = mFields + "," + mDataColumn.ColumnName;
                    }
                }

                #endregion

                #region generate data

                #region details of products

                if (mStoreCode.Trim() != "")
                {
                    mCommandText = "select " + mFields + " from som_products where (display=1 and visible_" + mStoreCode.Trim() + "=1)";
                }
                else
                {
                    mCommandText = "select " + mFields + " from som_products where (display=1)";
                }
                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtProducts);

                #endregion

                #region stock details

                string mProductsFilter = "";

                #region products filter

                foreach (DataRow mDataRow in mDtProducts.Rows)
                {
                    if (mProductsFilter.Trim() == "")
                    {
                        mProductsFilter = "'" + mDataRow["code"].ToString().Trim() + "'";
                    }
                    else
                    {
                        mProductsFilter = mProductsFilter + ",'" + mDataRow["code"].ToString().Trim() + "'";
                    }
                }

                if (mProductsFilter.Trim() == "")
                {
                    mProductsFilter = "1=2";
                }
                else
                {
                    mProductsFilter = "productcode in (" + mProductsFilter + ")";
                }

                #endregion

                if (mStoreCode.Trim() != "")
                {
                    string mQtyInField = "qtyin_" + mStoreCode.Trim().ToLower();
                    string mQtyOutField = "qtyout_" + mStoreCode.Trim().ToLower();

                    mCommandText = "select productcode,(sum(" + mQtyInField + ")-sum(" + mQtyOutField + ")) "
                    + "onhandqty from som_producttransactions where (" + mProductsFilter + ") and transdate<="
                    + clsGlobal.Saving_DateValue(mTransDate) + " group by productcode";
                }
                else
                {
                    DataTable mDtStores = new DataTable("stores");
                    mCommand.CommandText = "select code from som_stores";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtStores);

                    string mOnHandQty = "";
                    foreach (DataRow mDataRow in mDtStores.Rows)
                    {
                        if (mOnHandQty.Trim() == "")
                        {
                            mOnHandQty = "(sum(qtyin_" + mDataRow["code"].ToString().Trim() + ")-sum(qtyout_" + mDataRow["code"].ToString().Trim() + "))";
                        }
                        else
                        {
                            mOnHandQty = mOnHandQty + "+(sum(qtyin_" + mDataRow["code"].ToString().Trim() + ")-sum(qtyout_" + mDataRow["code"].ToString().Trim() + "))";
                        }
                    }

                    mCommandText = "select productcode," + mOnHandQty
                    + " onhandqty from som_producttransactions where (" + mProductsFilter + ") and transdate<="
                    + clsGlobal.Saving_DateValue(mTransDate) + " group by productcode";
                }

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtStock);

                #endregion

                mDtData = mDtProducts.Copy();
                mDtData.Rows.Clear();
                mDtData.Columns.Add("onhandqty", typeof(System.Double));

                DataView mDvStock = new DataView();
                mDvStock.Table = mDtStock;
                mDvStock.Sort = "productcode";

                foreach (DataRow mDataRow in mDtProducts.Rows)
                {
                    int mRowIndex = mDvStock.Find(mDataRow["code"].ToString().Trim());

                    double mQty = 0;
                    if (mRowIndex >= 0)
                    {
                        mQty = Convert.ToDouble(mDvStock[mRowIndex]["onhandqty"]);
                    }

                    bool mConditionPassed = true;

                    if (mBalanceCondition.Trim() != "")
                    {
                        mConditionPassed = false;

                        switch (mBalanceCondition.Trim().ToLower())
                        {
                            case "=": if (mQty == mBalanceLimit) { mConditionPassed = true; } break;
                            case ">": if (mQty > mBalanceLimit) { mConditionPassed = true; } break;
                            case ">=": if (mQty >= mBalanceLimit) { mConditionPassed = true; } break;
                            case "<": if (mQty < mBalanceLimit) { mConditionPassed = true; } break;
                            case "<=": if (mQty <= mBalanceLimit) { mConditionPassed = true; } break;
                            case "<>": if (mQty != mBalanceLimit) { mConditionPassed = true; } break;
                        }
                    }

                    if (mConditionPassed == true)
                    {
                        DataRow mNewRow = mDtData.NewRow();
                        foreach (DataColumn mDataColumn in mDtProducts.Columns)
                        {
                            mNewRow[mDataColumn.ColumnName] = mDataRow[mDataColumn.ColumnName];
                        }
                        mNewRow["onhandqty"] = mQty;
                        mDtData.Rows.Add(mNewRow);
                        mDtData.AcceptChanges();
                    }
                }

                #endregion

                mDtData.RemotingFormat = SerializationFormat.Binary;

                return mDtData;
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

        #region IV_GRN
        public DataSet IV_GRN(string mStoreCode, string mStoreDescription, DateTime mDateFrom, DateTime mDateTo,
            bool mGroupBySupplier, bool mGroupByDeliveries, bool mShowExpiryDates, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "IV_GRN";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                string mCommandText = "";

                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtData = new DataTable("data");

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_datefrom", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_dateto", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_storecode", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_storedescription", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_datefrom"] = mDateFrom;
                    mDataRow["para_dateto"] = mDateTo;
                    mDataRow["para_storecode"] = mStoreCode;
                    mDataRow["para_storedescription"] = mStoreDescription;
                    mDataRow["para_otherparameters"] = mExtraParameters;

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                string mProductFields = "";

                //no group
                mProductFields = "rti.productcode"
                                + ",rti.packagingcode"
                                + ",rti.packagingdescription"
                                + ",rti.piecesinpackage"
                                + ",rti.transprice";

                //group by supplier
                if (mGroupBySupplier == true)
                {
                    mProductFields += ",rt.fromcode"
                                    + ",rt.fromdescription";
                }

                //group by deliveries
                if (mGroupByDeliveries == true)
                {
                    mProductFields += ",rt.deliveryno";
                }

                //show expiry dates
                if (mShowExpiryDates == true)
                {
                    mProductFields += ",rti.expirydate";
                }

                //get product extra fields
                List<clsDataField> mDataFields = clsGlobal.Get_ProductExtraFields();
                foreach (clsDataField mDataField in mDataFields)
                {
                    if (mProductFields.Trim() == "")
                    {
                        mProductFields = "rti.product" + mDataField.FieldName.ToLower();
                    }
                    else
                    {
                        mProductFields = mProductFields + ",rti.product" + mDataField.FieldName.ToLower();
                    }
                }

                string mGroupByFields = mProductFields;

                mProductFields += ",sum(rti.receivedqty) receivedqty";

                mCommandText = "select " + mProductFields + " from som_stockreceiptitems rti "
                                + "left join som_stockreceipts rt on rti.deliveryno=rt.deliveryno "
                                + "where tostorecode='" + mStoreCode.Trim() + "' and rti.transdate between "
                                + clsGlobal.Saving_DateValue(mDateFrom) + " and " + clsGlobal.Saving_DateValue(mDateTo);

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText += " and (" + mExtraFilter + ")";
                }

                mCommandText += " group by " + mGroupByFields;

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);

                //build dataset and return
                DataSet mDsData = new DataSet("dataset");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtData);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #region IV_GIN
        public DataSet IV_GIN(string mStoreCode, string mStoreDescription, DateTime mDateFrom, DateTime mDateTo,
            bool mGroupByCustomer, bool mGroupByReceipt, bool mShowExpiryDates, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "IV_GIN";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                string mCommandText = "";

                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtData = new DataTable("data");

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_datefrom", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_dateto", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_storecode", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_storedescription", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_datefrom"] = mDateFrom;
                    mDataRow["para_dateto"] = mDateTo;
                    mDataRow["para_storecode"] = mStoreCode;
                    mDataRow["para_storedescription"] = mStoreDescription;
                    mDataRow["para_otherparameters"] = mExtraParameters;

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                string mProductFields = "";

                //no group
                mProductFields = "rti.productcode"
                                + ",rti.packagingcode"
                                + ",rti.packagingdescription"
                                + ",rti.piecesinpackage"
                                + ",rti.transprice";

                //group by customer
                if (mGroupByCustomer == true)
                {
                    mProductFields += ",rt.tocode"
                                    + ",rt.todescription";
                }

                //group by deliveries
                if (mGroupByReceipt == true)
                {
                    mProductFields += ",rt.deliveryno";
                }

                //show expiry dates
                if (mShowExpiryDates == true)
                {
                    mProductFields += ",rti.expirydate";
                }

                //get product extra fields
                List<clsDataField> mDataFields = clsGlobal.Get_ProductExtraFields();
                foreach (clsDataField mDataField in mDataFields)
                {
                    if (mProductFields.Trim() == "")
                    {
                        mProductFields = "rti.product" + mDataField.FieldName.ToLower();
                    }
                    else
                    {
                        mProductFields = mProductFields + ",rti.product" + mDataField.FieldName.ToLower();
                    }
                }

                string mGroupByFields = mProductFields;

                mProductFields += ",sum(rti.issuedqty) issuedqty";

                mCommandText = "select " + mProductFields + " from som_stockissueitems rti "
                                + "left join som_stockissues rt on rti.deliveryno=rt.deliveryno "
                                + "where fromstorecode='" + mStoreCode.Trim() + "' and rti.transdate between "
                                + clsGlobal.Saving_DateValue(mDateFrom) + " and " + clsGlobal.Saving_DateValue(mDateTo);

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText += " and (" + mExtraFilter + ")";
                }

                mCommandText += " group by " + mGroupByFields;

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);

                //build dataset and return
                DataSet mDsData = new DataSet("dataset");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtData);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #region IV_ProductHistory
        public DataSet IV_ProductHistory(string mStoreCode, string mStoreDescription, string mProductCode, DateTime mDateFrom, DateTime mDateTo,
            bool mDateBased, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "IV_ProductHistory";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtData = new DataTable("data");
                mDtData.Columns.Add("transdate", typeof(System.DateTime));
                mDtData.Columns.Add("referenceno", typeof(System.String));
                mDtData.Columns.Add("transdescription", typeof(System.String));
                mDtData.Columns.Add("sourcecode", typeof(System.String));
                mDtData.Columns.Add("sourcedescription", typeof(System.String));
                mDtData.Columns.Add("qty_in", typeof(System.Double));
                mDtData.Columns.Add("qty_out", typeof(System.Double));
                mDtData.Columns.Add("balance", typeof(System.Double));

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_datefrom", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_dateto", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_storecode", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_storedescription", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    if (mDateBased == true)
                    {
                        mDataRow["para_datefrom"] = mDateFrom;
                        mDataRow["para_dateto"] = mDateTo;
                    }
                    else
                    {
                        mDataRow["para_datefrom"] = DBNull.Value;
                        mDataRow["para_dateto"] = DBNull.Value;
                    }
                    mDataRow["para_storecode"] = mStoreCode;
                    mDataRow["para_storedescription"] = mStoreDescription;
                    mDataRow["para_otherparameters"] = mExtraParameters;

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                //add product details
                DataTable mDtProducts = new DataTable("products");
                mCommand.CommandText = "select * from som_products where code='" + mProductCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtProducts);

                if (mDtProducts.Rows.Count > 0)
                {
                    foreach (DataColumn mDataColumn in mDtProducts.Columns)
                    {
                        mDtFacilitySetup.Columns.Add("prod_" + mDataColumn.ColumnName, mDataColumn.DataType);
                    }

                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();
                    foreach (DataColumn mDataColumn in mDtProducts.Columns)
                    {
                        mDataRow["prod_" + mDataColumn.ColumnName] = mDtProducts.Rows[0][mDataColumn.ColumnName];
                    }
                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                double mBalance = 0;

                #region balance

                if (mDateBased == true)
                {
                    DataTable mDtBalance = new DataTable("balance");
                    mCommand.CommandText = "select productcode,(sum(qtyin_" + mStoreCode + ")-sum(qtyout_" + mStoreCode + ")) balance "
                        + "from som_producttransactions where productcode='" + mProductCode.Trim() + "' and transdate<" 
                        + clsGlobal.Saving_DateValue(mDateFrom) + " group by productcode";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtBalance);

                    if (mDtBalance.Rows.Count > 0)
                    {
                        mBalance = Convert.ToDouble(mDtBalance.Rows[0]["balance"]);

                        DataRow mNewRow = mDtData.NewRow();
                        mNewRow["transdate"] = mDateFrom;
                        mNewRow["referenceno"] = "";
                        mNewRow["transdescription"] = "B/F";
                        mNewRow["sourcecode"] = "";
                        mNewRow["sourcedescription"] = "";
                        mNewRow["balance"] = mBalance;
                        mDtData.Rows.Add(mNewRow);
                        mDtData.AcceptChanges();
                    }
                }

                #endregion

                #region transactions

                if (mDateBased == true)
                {
                    DataTable mDtTransactions = new DataTable("transactions");
                    mCommand.CommandText = "select transdate,reference,transdescription,sourcecode,sourcedescription,"
                        + "qtyin_" + mStoreCode + " qty_in,qtyout_" + mStoreCode + " qty_out "
                        + "from som_producttransactions where productcode='" + mProductCode.Trim() + "' and transdate between "
                        + clsGlobal.Saving_DateValue(mDateFrom) + " and " + clsGlobal.Saving_DateValue(mDateTo)
                        + " and (qtyin_" + mStoreCode + "<>0 or qtyout_" + mStoreCode + "<>0) order by transdate, autocode";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtTransactions);

                    foreach (DataRow mDataRow in mDtTransactions.Rows)
                    {
                        mBalance = mBalance + Convert.ToDouble(mDataRow["qty_in"]) - Convert.ToDouble(mDataRow["qty_out"]);

                        DataRow mNewRow = mDtData.NewRow();
                        mNewRow["transdate"] = mDataRow["transdate"];
                        mNewRow["referenceno"] = mDataRow["reference"];
                        mNewRow["transdescription"] = mDataRow["transdescription"];
                        mNewRow["sourcecode"] = mDataRow["sourcecode"];
                        mNewRow["sourcedescription"] = mDataRow["sourcedescription"];
                        mNewRow["qty_in"] = Convert.ToDouble(mDataRow["qty_in"]);
                        mNewRow["qty_out"] = Convert.ToDouble(mDataRow["qty_out"]);
                        mNewRow["balance"] = mBalance;
                        mDtData.Rows.Add(mNewRow);
                        mDtData.AcceptChanges();
                    }
                }
                else
                {
                    DataTable mDtTransactions = new DataTable("transactions");
                    mCommand.CommandText = "select transdate,reference,transdescription,sourcecode,sourcedescription,"
                        + "qtyin_" + mStoreCode + " qty_in,qtyout_" + mStoreCode + " qty_out "
                        + "from som_producttransactions where productcode='" + mProductCode.Trim()
                        + "' and (qtyin_" + mStoreCode + "<>0 or qtyout_" + mStoreCode + "<>0) order by transdate, autocode";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtTransactions);

                    foreach (DataRow mDataRow in mDtTransactions.Rows)
                    {
                        mBalance = mBalance + Convert.ToDouble(mDataRow["qty_in"]) - Convert.ToDouble(mDataRow["qty_out"]);

                        DataRow mNewRow = mDtData.NewRow();
                        mNewRow["transdate"] = mDataRow["transdate"];
                        mNewRow["referenceno"] = mDataRow["reference"];
                        mNewRow["transdescription"] = mDataRow["transdescription"];
                        mNewRow["sourcecode"] = mDataRow["sourcecode"];
                        mNewRow["sourcedescription"] = mDataRow["sourcedescription"];
                        mNewRow["qty_in"] = Convert.ToDouble(mDataRow["qty_in"]);
                        mNewRow["qty_out"] = Convert.ToDouble(mDataRow["qty_out"]);
                        mNewRow["balance"] = mBalance;
                        mDtData.Rows.Add(mNewRow);
                        mDtData.AcceptChanges();
                    }
                }

                #endregion

                //build dataset and return
                DataSet mDsData = new DataSet("dataset");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtData);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #region IV_PriceList
        public DataTable IV_PriceList(string mStoreCode, string mStoreDescription, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "IV_PriceList";

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
                string mFieldName = "";
                string mFieldValue = "";
                string mCommandText = "";
                DataTable mDtProducts = new DataTable("products");

                string mFields = "";

                #region facilitysetup

                DataTable mDtFacilityOptions = new DataTable("facilityoptions");
                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilityOptions);

                if (mDtFacilityOptions.Rows.Count > 0)
                {
                    foreach (DataColumn mDataColumn in mDtFacilityOptions.Columns)
                    {
                        if (mDataColumn.ColumnName.ToLower() != "autocode")
                        {
                            mFieldName = "facility_" + mDataColumn.ColumnName;
                            mFieldValue = "";

                            #region build fieldvalue

                            switch (mDataColumn.DataType.FullName.Trim().ToLower())
                            {
                                case "system.decimal":
                                    {
                                        mFieldValue = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                    }
                                    break;
                                case "system.double":
                                    {
                                        mFieldValue = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                    }
                                    break;
                                case "system.int16":
                                    {
                                        mFieldValue = Convert.ToInt16(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                    }
                                    break;
                                case "system.int32":
                                    {
                                        mFieldValue = Convert.ToInt32(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                    }
                                    break;
                                case "system.int64":
                                    {
                                        mFieldValue = Convert.ToInt64(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                    }
                                    break;
                                case "system.single":
                                    {
                                        mFieldValue = Convert.ToSingle(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                    }
                                    break;
                                case "system.datetime":
                                    {
                                        mFieldValue = clsGlobal.Saving_DateValue(Convert.ToDateTime(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]));
                                    }
                                    break;
                                default:
                                    {
                                        mFieldValue = "'" + mDtFacilityOptions.Rows[0][mDataColumn.ColumnName].ToString() + "'";
                                    }
                                    break;
                            }
                            #endregion

                            if (mFields.Trim() == "")
                            {
                                mFields = Environment.NewLine + " " + mFieldValue + " " + mFieldName;
                            }
                            else
                            {
                                mFields = mFields + Environment.NewLine + ", " + mFieldValue + " " + mFieldName;
                            }
                        }
                    }
                }

                #endregion

                #region add parameters used

                #region storedescription
                mFieldName = "para_storedescription";
                mFieldValue = "'" + mStoreDescription + "'";
                if (mFields.Trim() == "")
                {
                    mFields = mFieldValue + " " + mFieldName;
                }
                else
                {
                    mFields = mFields + "," + mFieldValue + " " + mFieldName;
                }
                #endregion

                #region extra parameters
                mFieldName = "para_otherparameters";
                mFieldValue = "'" + mExtraParameters + "'";
                if (mFields.Trim() == "")
                {
                    mFields = mFieldValue + " " + mFieldName;
                }
                else
                {
                    mFields = mFields + "," + mFieldValue + " " + mFieldName;
                }
                #endregion

                #endregion

                #region add columns from the table/view

                mCommand.CommandText = "select * from som_products where 1=2";
                DataTable mDtColumns = new DataTable("tableschema");
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.FillSchema(mDtColumns, SchemaType.Source);

                foreach (DataColumn mDataColumn in mDtColumns.Columns)
                {
                    if (mFields.Trim() == "")
                    {
                        mFields = mDataColumn.ColumnName;
                    }
                    else
                    {
                        mFields = mFields + "," + mDataColumn.ColumnName;
                    }
                }

                #endregion

                //generate data for report
                if (mStoreCode.Trim() != "")
                {
                    mCommandText = "select " + mFields + " from som_products where (display=1 and visible_" + mStoreCode.Trim() + "=1)";
                }
                else
                {
                    mCommandText = "select " + mFields + " from som_products where (display=1)";
                }
                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mExtraFilter + ")";
                }
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtProducts);

                mDtProducts.RemotingFormat = SerializationFormat.Binary;

                return mDtProducts;
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

        #endregion

        #region RCH

        #region RCH_AttendanceList
        public DataTable RCH_AttendanceList(DateTime mDateFrom, DateTime mDateTo, bool mDateBased, int mServiceType, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "RCH_AttendanceList";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                string mFieldName = "";
                string mFieldValue = "";
                string mCommandText = "";
                DataTable mDtData = new DataTable("data");

                string mFields = "";

                #region facilitysetup

                DataTable mDtFacilityOptions = new DataTable("facilityoptions");
                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilityOptions);

                if (mDtFacilityOptions.Rows.Count > 0)
                {
                    foreach (DataColumn mDataColumn in mDtFacilityOptions.Columns)
                    {
                        if (mDataColumn.ColumnName.ToLower() != "autocode")
                        {
                            mFieldName = "facility_" + mDataColumn.ColumnName;
                            mFieldValue = "";

                            #region build fieldvalue

                            switch (mDataColumn.DataType.FullName.Trim().ToLower())
                            {
                                case "system.decimal":
                                    {
                                        mFieldValue = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Double));
                                    }
                                    break;
                                case "system.double":
                                    {
                                        mFieldValue = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Double));
                                    }
                                    break;
                                case "system.int16":
                                    {
                                        mFieldValue = Convert.ToInt16(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int16));
                                    }
                                    break;
                                case "system.int32":
                                    {
                                        mFieldValue = Convert.ToInt32(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int32));
                                    }
                                    break;
                                case "system.int64":
                                    {
                                        mFieldValue = Convert.ToInt64(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int64));
                                    }
                                    break;
                                case "system.single":
                                    {
                                        mFieldValue = Convert.ToSingle(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Single));
                                    }
                                    break;
                                case "system.datetime":
                                    {
                                        mFieldValue = clsGlobal.Saving_DateValue(Convert.ToDateTime(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]));
                                        mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                                    }
                                    break;
                                default:
                                    {
                                        mFieldValue = "'" + mDtFacilityOptions.Rows[0][mDataColumn.ColumnName].ToString() + "'";
                                        mDtData.Columns.Add(mFieldName, typeof(System.String));
                                    }
                                    break;
                            }
                            #endregion

                            if (mFields.Trim() == "")
                            {
                                mFields = Environment.NewLine + " " + mFieldValue + " " + mFieldName;
                            }
                            else
                            {
                                mFields = mFields + Environment.NewLine + ", " + mFieldValue + " " + mFieldName;
                            }
                        }
                    }
                }

                #endregion

                #region add parameters used

                #region datefrom
                mFieldName = "para_datefrom";
                mFieldValue = clsGlobal.Saving_DateValue(mDateFrom);
                mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                if (mFields.Trim() == "")
                {
                    mFields = mFieldValue + " " + mFieldName;
                }
                else
                {
                    mFields = mFields + "," + mFieldValue + " " + mFieldName;
                }
                #endregion

                #region dateto
                mFieldName = "para_dateto";
                mFieldValue = clsGlobal.Saving_DateValue(mDateTo);
                mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                if (mFields.Trim() == "")
                {
                    mFields = mFieldValue + " " + mFieldName;
                }
                else
                {
                    mFields = mFields + "," + mFieldValue + " " + mFieldName;
                }
                #endregion

                #region extra parameters
                mFieldName = "para_otherparameters";
                mFieldValue = "'" + mExtraParameters + "'";
                mDtData.Columns.Add(mFieldName, typeof(System.String));
                if (mFields.Trim() == "")
                {
                    mFields = mFieldValue + " " + mFieldName;
                }
                else
                {
                    mFields = mFields + "," + mFieldValue + " " + mFieldName;
                }
                #endregion

                #endregion

                //columns from patients
                mFields = mFields + "," + clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "patient");
                mFields = mFields + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "patientfullname");
                mFields = mFields + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "b.bookdate",""), "patientage");

                if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.antenatalcare)
                {
                    mCommandText = ""
                        + "SELECT "
                        + "b.bookdate,"
                        + "b.clientcode,"
                        + "b.registrystatus,"
                        + mFields + " "
                        + "FROM rch_antenatalattendancelog AS b "
                        + "LEFT OUTER JOIN patients AS p ON b.clientcode=p.code "
                        + "WHERE (b.bookdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ")";
                }
                else if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.childrenhealth)
                {
                    mCommandText = ""
                        + "SELECT "
                        + "b.bookdate,"
                        + "b.clientcode,"
                        + "b.registrystatus,"
                        + mFields + " "
                        + "FROM rch_childrenattendancelog AS b "
                        + "LEFT OUTER JOIN patients AS p ON b.clientcode=p.code "
                        + "WHERE (b.bookdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ")";
                }
                else if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.familyplanning)
                {
                    mCommandText = ""
                        + "SELECT "
                        + "b.bookdate,"
                        + "b.clientcode,"
                        + "b.registrystatus,"
                        + mFields + " "
                        + "FROM rch_fplanattendancelog AS b "
                        + "LEFT OUTER JOIN patients AS p ON b.clientcode=p.code "
                        + "WHERE (b.bookdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ")";
                }
                else if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.postnatalcare)
                {
                    mCommandText = ""
                        + "SELECT "
                        + "b.bookdate,"
                        + "b.clientcode,"
                        + "b.registrystatus,"
                        + mFields + " "
                        + "FROM rch_postnatalattendancelog AS b "
                        + "LEFT OUTER JOIN patients AS p ON b.clientcode=p.code "
                        + "WHERE (b.bookdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ")";
                }
                else if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.vaccinations)
                {
                    //columns from patients
                    mFields = mFields + "," + clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "patient");
                    mFields = mFields + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "patientfullname");
                    mFields = mFields + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "b.bookdate",""), "patientage");

                    mCommandText = ""
                        + "SELECT "
                        + "b.bookdate,"
                        + "b.clientcode,"
                        + "b.registrystatus,"
                        + mFields + " "
                        + "FROM rch_vaccinationslog AS b "
                        + "LEFT OUTER JOIN patients AS p ON b.clientcode=p.code "
                        + "WHERE (b.bookdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ")";
                }

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " AND (" + mExtraFilter + ")";
                }

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);
                mDtData.RemotingFormat = SerializationFormat.Binary;

                return mDtData;
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

        #region RCH_VaccinationsCount
        public DataTable RCH_VaccinationsCount(DateTime mDateFrom, DateTime mDateTo, bool mDateBased, int mServiceType, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "RCH_VaccinationsCount";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                string mFieldName = "";
                string mFieldValue = "";
                string mCommandText = "";
                DataTable mDtData = new DataTable("data");

                string mFields = "";

                #region facilitysetup

                DataTable mDtFacilityOptions = new DataTable("facilityoptions");
                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilityOptions);

                if (mDtFacilityOptions.Rows.Count > 0)
                {
                    foreach (DataColumn mDataColumn in mDtFacilityOptions.Columns)
                    {
                        if (mDataColumn.ColumnName.ToLower() != "autocode")
                        {
                            mFieldName = "facility_" + mDataColumn.ColumnName;
                            mFieldValue = "";

                            #region build fieldvalue

                            switch (mDataColumn.DataType.FullName.Trim().ToLower())
                            {
                                case "system.decimal":
                                    {
                                        mFieldValue = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Double));
                                    }
                                    break;
                                case "system.double":
                                    {
                                        mFieldValue = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Double));
                                    }
                                    break;
                                case "system.int16":
                                    {
                                        mFieldValue = Convert.ToInt16(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int16));
                                    }
                                    break;
                                case "system.int32":
                                    {
                                        mFieldValue = Convert.ToInt32(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int32));
                                    }
                                    break;
                                case "system.int64":
                                    {
                                        mFieldValue = Convert.ToInt64(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int64));
                                    }
                                    break;
                                case "system.single":
                                    {
                                        mFieldValue = Convert.ToSingle(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Single));
                                    }
                                    break;
                                case "system.datetime":
                                    {
                                        mFieldValue = clsGlobal.Saving_DateValue(Convert.ToDateTime(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]));
                                        mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                                    }
                                    break;
                                default:
                                    {
                                        mFieldValue = "'" + mDtFacilityOptions.Rows[0][mDataColumn.ColumnName].ToString() + "'";
                                        mDtData.Columns.Add(mFieldName, typeof(System.String));
                                    }
                                    break;
                            }
                            #endregion

                            if (mFields.Trim() == "")
                            {
                                mFields = Environment.NewLine + " " + mFieldValue + " " + mFieldName;
                            }
                            else
                            {
                                mFields = mFields + Environment.NewLine + ", " + mFieldValue + " " + mFieldName;
                            }
                        }
                    }
                }

                #endregion

                #region add parameters used

                #region datefrom
                mFieldName = "para_datefrom";
                mFieldValue = clsGlobal.Saving_DateValue(mDateFrom);
                mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                if (mFields.Trim() == "")
                {
                    mFields = mFieldValue + " " + mFieldName;
                }
                else
                {
                    mFields = mFields + "," + mFieldValue + " " + mFieldName;
                }
                #endregion

                #region dateto
                mFieldName = "para_dateto";
                mFieldValue = clsGlobal.Saving_DateValue(mDateTo);
                mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                if (mFields.Trim() == "")
                {
                    mFields = mFieldValue + " " + mFieldName;
                }
                else
                {
                    mFields = mFields + "," + mFieldValue + " " + mFieldName;
                }
                #endregion

                #region extra parameters
                mFieldName = "para_otherparameters";
                mFieldValue = "'" + mExtraParameters + "'";
                mDtData.Columns.Add(mFieldName, typeof(System.String));
                if (mFields.Trim() == "")
                {
                    mFields = mFieldValue + " " + mFieldName;
                }
                else
                {
                    mFields = mFields + "," + mFieldValue + " " + mFieldName;
                }
                #endregion

                #endregion

                if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.antenatalcare)
                {
                    mCommandText = ""
                        + "SELECT "
                        + mFields + ","
                        + "b.vaccinecode,"
                        + "b.vaccinedescription,"
                        + "sum(case when p.gender='m' then 1 else 0 end) males,"
                        + "sum(case when p.gender='f' then 1 else 0 end) females "
                        + "FROM view_vaccineslog AS b "
                        + "LEFT OUTER JOIN patients AS p ON b.clientcode=p.code "
                        + "WHERE (b.bookdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                        + "AND b.servicetype=" + (int)AfyaPro_Types.clsEnums.RCHServices.antenatalcare;
                }
                else if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.childrenhealth)
                {
                    mCommandText = ""
                        + "SELECT "
                        + mFields + ","
                        + "b.vaccinecode,"
                        + "b.vaccinedescription,"
                        + "sum(case when p.gender='m' then 1 else 0 end) males,"
                        + "sum(case when p.gender='f' then 1 else 0 end) females "
                        + "FROM view_vaccineslog AS b "
                        + "LEFT OUTER JOIN patients AS p ON b.clientcode=p.code "
                        + "WHERE (b.bookdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                        + "AND b.servicetype=" + (int)AfyaPro_Types.clsEnums.RCHServices.childrenhealth;
                }
                else if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.familyplanning)
                {
                    mCommandText = ""
                        + "SELECT "
                        + mFields + ","
                        + "b.vaccinecode,"
                        + "b.vaccinedescription,"
                        + "sum(case when p.gender='m' then 1 else 0 end) males,"
                        + "sum(case when p.gender='f' then 1 else 0 end) females "
                        + "FROM view_vaccineslog AS b "
                        + "LEFT OUTER JOIN patients AS p ON b.clientcode=p.code "
                        + "WHERE (b.bookdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                        + "AND b.servicetype=" + (int)AfyaPro_Types.clsEnums.RCHServices.familyplanning;
                }
                else if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.postnatalcare)
                {
                    mCommandText = ""
                        + "SELECT "
                        + mFields + ","
                        + "b.vaccinecode,"
                        + "b.vaccinedescription,"
                        + "sum(case when p.gender='m' then 1 else 0 end) males,"
                        + "sum(case when p.gender='f' then 1 else 0 end) females "
                        + "FROM view_vaccineslog AS b "
                        + "LEFT OUTER JOIN patients AS p ON b.clientcode=p.code "
                        + "WHERE (b.bookdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                        + "AND b.servicetype=" + (int)AfyaPro_Types.clsEnums.RCHServices.postnatalcare;
                }
                else if (mServiceType == (int)AfyaPro_Types.clsEnums.RCHServices.vaccinations)
                {
                    mCommandText = ""
                        + "SELECT "
                        + mFields + ","
                        + "b.vaccinecode,"
                        + "b.vaccinedescription,"
                        + "sum(case when p.gender='m' then 1 else 0 end) males,"
                        + "sum(case when p.gender='f' then 1 else 0 end) females "
                        + "FROM view_vaccineslog AS b "
                        + "LEFT OUTER JOIN patients AS p ON b.clientcode=p.code "
                        + "WHERE (b.bookdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                        + "AND b.servicetype=" + (int)AfyaPro_Types.clsEnums.RCHServices.vaccinations;
                }
                else
                {
                    mCommandText = ""
                        + "SELECT "
                        + mFields + ","
                        + "b.vaccinecode,"
                        + "b.vaccinedescription,"
                        + "sum(case when p.gender='m' then 1 else 0 end) males,"
                        + "sum(case when p.gender='f' then 1 else 0 end) females "
                        + "FROM view_vaccineslog AS b "
                        + "LEFT OUTER JOIN patients AS p ON b.clientcode=p.code "
                        + "WHERE (b.bookdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ")";
                }

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " AND (" + mExtraFilter + ")";
                }

                mCommandText = mCommandText + " GROUP BY b.vaccinecode,b.vaccinedescription";

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);
                mDtData.RemotingFormat = SerializationFormat.Binary;

                return mDtData;
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

        #region RCH_FPlanMethodsCount
        public DataTable RCH_FPlanMethodsCount(DateTime mDateFrom, DateTime mDateTo, bool mDateBased, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "RCH_FPlanMethodsCount";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                string mFieldName = "";
                string mFieldValue = "";
                string mCommandText = "";
                DataTable mDtData = new DataTable("data");

                string mFields = "";

                #region facilitysetup

                DataTable mDtFacilityOptions = new DataTable("facilityoptions");
                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilityOptions);

                if (mDtFacilityOptions.Rows.Count > 0)
                {
                    foreach (DataColumn mDataColumn in mDtFacilityOptions.Columns)
                    {
                        if (mDataColumn.ColumnName.ToLower() != "autocode")
                        {
                            mFieldName = "facility_" + mDataColumn.ColumnName;
                            mFieldValue = "";

                            #region build fieldvalue

                            switch (mDataColumn.DataType.FullName.Trim().ToLower())
                            {
                                case "system.decimal":
                                    {
                                        mFieldValue = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Double));
                                    }
                                    break;
                                case "system.double":
                                    {
                                        mFieldValue = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Double));
                                    }
                                    break;
                                case "system.int16":
                                    {
                                        mFieldValue = Convert.ToInt16(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int16));
                                    }
                                    break;
                                case "system.int32":
                                    {
                                        mFieldValue = Convert.ToInt32(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int32));
                                    }
                                    break;
                                case "system.int64":
                                    {
                                        mFieldValue = Convert.ToInt64(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int64));
                                    }
                                    break;
                                case "system.single":
                                    {
                                        mFieldValue = Convert.ToSingle(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Single));
                                    }
                                    break;
                                case "system.datetime":
                                    {
                                        mFieldValue = clsGlobal.Saving_DateValue(Convert.ToDateTime(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]));
                                        mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                                    }
                                    break;
                                default:
                                    {
                                        mFieldValue = "'" + mDtFacilityOptions.Rows[0][mDataColumn.ColumnName].ToString() + "'";
                                        mDtData.Columns.Add(mFieldName, typeof(System.String));
                                    }
                                    break;
                            }
                            #endregion

                            if (mFields.Trim() == "")
                            {
                                mFields = Environment.NewLine + " " + mFieldValue + " " + mFieldName;
                            }
                            else
                            {
                                mFields = mFields + Environment.NewLine + ", " + mFieldValue + " " + mFieldName;
                            }
                        }
                    }
                }

                #endregion

                #region add parameters used

                #region datefrom
                mFieldName = "para_datefrom";
                mFieldValue = clsGlobal.Saving_DateValue(mDateFrom);
                mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                if (mFields.Trim() == "")
                {
                    mFields = mFieldValue + " " + mFieldName;
                }
                else
                {
                    mFields = mFields + "," + mFieldValue + " " + mFieldName;
                }
                #endregion

                #region dateto
                mFieldName = "para_dateto";
                mFieldValue = clsGlobal.Saving_DateValue(mDateTo);
                mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                if (mFields.Trim() == "")
                {
                    mFields = mFieldValue + " " + mFieldName;
                }
                else
                {
                    mFields = mFields + "," + mFieldValue + " " + mFieldName;
                }
                #endregion

                #region extra parameters
                mFieldName = "para_otherparameters";
                mFieldValue = "'" + mExtraParameters + "'";
                mDtData.Columns.Add(mFieldName, typeof(System.String));
                if (mFields.Trim() == "")
                {
                    mFields = mFieldValue + " " + mFieldName;
                }
                else
                {
                    mFields = mFields + "," + mFieldValue + " " + mFieldName;
                }
                #endregion

                #endregion

                mCommandText = ""
                    + "SELECT "
                    + mFields + ","
                    + "methodcode,"
                    + "methoddescription,"
                    + "sum(case when gender='m' then quantity else 0 end) males,"
                    + "sum(case when gender='f' then quantity else 0 end) females "
                    + "FROM view_rch_fplanmethodslog "
                    + "WHERE (bookdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ")";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " AND (" + mExtraFilter + ")";
                }

                mCommandText = mCommandText + " GROUP BY methodcode,methoddescription";

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);
                mDtData.RemotingFormat = SerializationFormat.Binary;

                return mDtData;
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

        #region RCH_AntenatalAtt
        public DataSet RCH_AntenatalAtt(DateTime mDateFrom, DateTime mDateTo, bool mDateBased, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "RCH_AntenatalAtt";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                string mCommandText = "";
                DataRow mDataRow;

                DataTable mDtData;
                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtParent = new DataTable("parent");
                DataTable mDtSummaryA = new DataTable("summarya");
                DataTable mDtSummaryB = new DataTable("summaryb");

                mDtSummaryA.Columns.Add("keyvalue", typeof(System.String));
                mDtSummaryA.Columns.Add("newatt", typeof(System.Int32));
                mDtSummaryA.Columns.Add("withtt", typeof(System.Int32));
                mDtSummaryA.Columns.Add("under20", typeof(System.Int32));
                mDtSummaryA.Columns.Add("indicators", typeof(System.Int32));
                mDtSummaryA.Columns.Add("syphilispos", typeof(System.Int32));
                mDtSummaryA.Columns.Add("syphilisneg", typeof(System.Int32));

                mDtSummaryB.Columns.Add("keyvalue", typeof(System.String));
                mDtSummaryB.Columns.Add("reatt", typeof(System.Int32));
                mDtSummaryB.Columns.Add("lastbornalive", typeof(System.Int32));
                mDtSummaryB.Columns.Add("lastborndead", typeof(System.Int32));
                mDtSummaryB.Columns.Add("referal", typeof(System.Int32));

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_datefrom", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_dateto", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_datefrom"] = mDateFrom;
                    mDataRow["para_dateto"] = mDateTo;
                    mDataRow["para_otherparameters"] = mExtraParameters;

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region prepare parent datatable

                mDtParent.Columns.Add("keyvalue", typeof(System.String));

                mDataRow = mDtParent.NewRow();
                mDataRow["keyvalue"] = "summarya";
                mDtParent.Rows.Add(mDataRow);
                mDtParent.AcceptChanges();

                mDataRow = mDtParent.NewRow();
                mDataRow["keyvalue"] = "summaryb";
                mDtParent.Rows.Add(mDataRow);
                mDtParent.AcceptChanges();

                #endregion

                #region summarya

                mCommandText = ""
                    + "SELECT "
	                    + "COUNT(b.clientcode) newatt,"
	                    + "SUM(CASE WHEN b.cardpresented = 1 THEN 1 ELSE 0 END) withtt,"
	                    + "SUM(CASE WHEN b.pregage < 20 THEN 1 ELSE 0 END) under20,"
	                    + "SUM(CASE WHEN b.syphilistest = 2 THEN 1 ELSE 0 END) syphilispos,"
	                    + "SUM(CASE WHEN b.syphilistest = 1 THEN 1 ELSE 0 END) syphilisneg "
                    + "FROM rch_antenatalattendancelog b "
                    + "WHERE (b.bookdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                    + "AND b.registrystatus='New'";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " AND (" + mExtraFilter + ")";
                }
                mDtData = new DataTable("data");
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);

                mCommandText = "SELECT "
                    + "COUNT(DISTINCT(clientcode)) indicators "
                    + "FROM view_rch_dangerindicatorslog "
                    + "WHERE (bookdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ")";
                DataTable mDtIndicators = new DataTable("indicators");
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtIndicators);

                int mNewAtt = 0;
                int mWithTT = 0;
                int mUnder20 = 0;
                int mIndicators = 0;
                int mSyphilisPos = 0;
                int mSyphilisNeg = 0;

                if (mDtData.Rows.Count > 0)
                {
                    mNewAtt = mDtData.Rows[0]["newatt"] == DBNull.Value ? 0 : Convert.ToInt32(mDtData.Rows[0]["newatt"]);
                    mWithTT = mDtData.Rows[0]["withtt"] == DBNull.Value ? 0 : Convert.ToInt32(mDtData.Rows[0]["withtt"]);
                    mUnder20 = mDtData.Rows[0]["under20"] == DBNull.Value ? 0 : Convert.ToInt32(mDtData.Rows[0]["under20"]);
                    mSyphilisPos = mDtData.Rows[0]["syphilispos"] == DBNull.Value ? 0 : Convert.ToInt32(mDtData.Rows[0]["syphilispos"]);
                    mSyphilisNeg = mDtData.Rows[0]["syphilisneg"] == DBNull.Value ? 0 : Convert.ToInt32(mDtData.Rows[0]["syphilisneg"]);
                }
                if (mDtIndicators.Rows.Count > 0)
                {
                    mIndicators = mDtIndicators.Rows[0]["indicators"] == DBNull.Value ? 0 : Convert.ToInt32(mDtIndicators.Rows[0]["indicators"]);
                }

                DataRow mNewRow = mDtSummaryA.NewRow();
                mNewRow["keyvalue"] = "summarya";
                mNewRow["newatt"] = mNewAtt;
                mNewRow["withtt"] = mWithTT;
                mNewRow["under20"] = mUnder20;
                mNewRow["indicators"] = mIndicators;
                mNewRow["syphilispos"] = mSyphilisPos;
                mNewRow["syphilisneg"] = mSyphilisNeg;
                mDtSummaryA.Rows.Add(mNewRow);
                mDtSummaryA.AcceptChanges();

                #endregion

                #region summaryb

                mCommandText = ""
                    + "SELECT "
                        + "SUM(CASE WHEN b.registrystatus<>'New' THEN 1 ELSE 0 END) reatt,"
                        + "SUM(CASE WHEN b.lastborndeath = 0 THEN 1 ELSE 0 END) lastbornalive,"
                        + "SUM(CASE WHEN b.lastborndeath = 1 THEN 1 ELSE 0 END) lastborndead,"
                        + "SUM(CASE WHEN b.referedto <> '' OR b.referedto IS NOT NULL THEN 1 ELSE 0 END) referal "
                    + "FROM rch_antenatalattendancelog b "
                    + "WHERE (b.bookdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ")";
                mDtData = new DataTable("data");
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);

                int mReAtt = 0;
                int mLastBornAlive = 0;
                int mLastBornDead = 0;
                int mReferal = 0;

                if (mDtData.Rows.Count > 0)
                {
                    mReAtt = mDtData.Rows[0]["reatt"] == DBNull.Value ? 0 : Convert.ToInt32(mDtData.Rows[0]["reatt"]);
                    mLastBornAlive = mDtData.Rows[0]["lastbornalive"] == DBNull.Value ? 0 : Convert.ToInt32(mDtData.Rows[0]["lastbornalive"]);
                    mLastBornDead = mDtData.Rows[0]["lastborndead"] == DBNull.Value ? 0 : Convert.ToInt32(mDtData.Rows[0]["lastborndead"]);
                    mReferal = mDtData.Rows[0]["referal"] == DBNull.Value ? 0 : Convert.ToInt32(mDtData.Rows[0]["referal"]);
                }

                mNewRow = mDtSummaryB.NewRow();
                mNewRow["keyvalue"] = "summaryb";
                mNewRow["reatt"] = mReAtt;
                mNewRow["lastbornalive"] = mLastBornAlive;
                mNewRow["lastborndead"] = mLastBornDead;
                mNewRow["referal"] = mReferal;
                mDtSummaryB.Rows.Add(mNewRow);
                mDtSummaryB.AcceptChanges();

                #endregion

                DataSet mDsData = new DataSet("data");
                mDsData.RemotingFormat = SerializationFormat.Binary;

                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtParent);
                mDsData.Tables.Add(mDtSummaryA);
                mDsData.Tables.Add(mDtSummaryB);

                mDsData.Relations.Add("summarya", mDtParent.Columns["keyvalue"], mDtSummaryA.Columns["keyvalue"]);
                mDsData.Relations.Add("summaryb", mDtParent.Columns["keyvalue"], mDtSummaryB.Columns["keyvalue"]);

                return mDsData;
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

        #region RCH_PostnatalAtt
        public DataSet RCH_PostnatalAtt(DateTime mDateFrom, DateTime mDateTo, bool mDateBased, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "RCH_PostnatalAtt";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                string mCommandText = "";
                DataRow mDataRow;

                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtParent = new DataTable("parent");
                DataTable mDtSummaryA = new DataTable("summarya");
                DataTable mDtSummaryB = new DataTable("summaryb");
                DataTable mDtSummaryC = new DataTable("summaryc");
                DataTable mDtSummaryD = new DataTable("summaryd");

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_datefrom", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_dateto", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_datefrom"] = mDateFrom;
                    mDataRow["para_dateto"] = mDateTo;
                    mDataRow["para_otherparameters"] = mExtraParameters;

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region prepare parent datatable

                mDtParent.Columns.Add("keyvalue", typeof(System.String));

                mDataRow = mDtParent.NewRow();
                mDataRow["keyvalue"] = "summarya";
                mDtParent.Rows.Add(mDataRow);
                mDtParent.AcceptChanges();

                mDataRow = mDtParent.NewRow();
                mDataRow["keyvalue"] = "summaryb";
                mDtParent.Rows.Add(mDataRow);
                mDtParent.AcceptChanges();

                mDataRow = mDtParent.NewRow();
                mDataRow["keyvalue"] = "summaryc";
                mDtParent.Rows.Add(mDataRow);
                mDtParent.AcceptChanges();

                mDataRow = mDtParent.NewRow();
                mDataRow["keyvalue"] = "summaryd";
                mDtParent.Rows.Add(mDataRow);
                mDtParent.AcceptChanges();

                #endregion

                #region summarya

                mCommandText = ""
                    + "SELECT "
                        + "'summarya' keyvalue,"
                        + "b.birthmethod code,"
                        + "bm.description description,"
                        + "COUNT(b.clientcode) total "
                    + "FROM rch_postnatalattendancelog b "
                    + "LEFT OUTER JOIN rch_birthmethods bm ON b.birthmethod = bm.code "
                    + "WHERE (b.bookdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ")";
                mCommandText = mCommandText + " GROUP BY b.birthmethod,bm.description";

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSummaryA);

                #endregion

                #region summaryb

                mCommandText = ""
                    + "SELECT "
                        + "'summaryb' keyvalue,"
                        + "methodcode code,"
                        + "methoddescription description,"
                        + "COUNT(clientcode) total "
                    + "FROM view_rch_birthcomplicationslog "
                    + "WHERE (bookdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ")";
                mCommandText = mCommandText + " GROUP BY methodcode,methoddescription";

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSummaryB);

                #endregion

                #region summaryc

                mCommandText = ""
                    + "SELECT "
                        + "'summaryc' keyvalue,"
                        + "SUM(CASE WHEN live=1 THEN 1 ELSE 0 END) alives,"
                        + "SUM(CASE WHEN weight<>0 THEN 1 ELSE 0 END) weighed,"
                        + "SUM(CASE WHEN weight<2.5 THEN 1 ELSE 0 END) under25,"
                        + "SUM(CASE WHEN maceratedbirth=1 THEN 1 ELSE 0 END) macerated,"
                        + "SUM(CASE WHEN freshbirth=1 THEN 1 ELSE 0 END) fresh,"
                        + "SUM(CASE WHEN deathbefore24=1 THEN 1 ELSE 0 END) under24,"
                        + "SUM(CASE WHEN deathafter24=1 THEN 1 ELSE 0 END) 24plus "
                    + "FROM view_rch_postnatalchildren "
                    + "WHERE (bookdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                    + "AND noofchildren=1";

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSummaryC);

                #endregion

                #region summaryd

                mCommandText = ""
                    + "SELECT "
                        + "'summaryd' keyvalue,"
                        + "SUM(CASE WHEN live=1 THEN 1 ELSE 0 END) alives,"
                        + "SUM(CASE WHEN maceratedbirth=1 THEN 1 ELSE 0 END) macerated,"
                        + "SUM(CASE WHEN freshbirth=1 THEN 1 ELSE 0 END) fresh,"
                        + "SUM(CASE WHEN deathbefore24=1 THEN 1 ELSE 0 END) under24,"
                        + "SUM(CASE WHEN deathafter24=1 THEN 1 ELSE 0 END) 24plus "
                    + "FROM view_rch_postnatalchildren "
                    + "WHERE (bookdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                    + "AND noofchildren>1";

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSummaryD);

                #endregion

                DataSet mDsData = new DataSet("data");
                mDsData.RemotingFormat = SerializationFormat.Binary;

                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtParent);
                mDsData.Tables.Add(mDtSummaryA);
                mDsData.Tables.Add(mDtSummaryB);
                mDsData.Tables.Add(mDtSummaryC);
                mDsData.Tables.Add(mDtSummaryD);

                mDsData.Relations.Add("summarya", mDtParent.Columns["keyvalue"], mDtSummaryA.Columns["keyvalue"]);
                mDsData.Relations.Add("summaryb", mDtParent.Columns["keyvalue"], mDtSummaryB.Columns["keyvalue"]);
                mDsData.Relations.Add("summaryc", mDtParent.Columns["keyvalue"], mDtSummaryC.Columns["keyvalue"]);
                mDsData.Relations.Add("summaryd", mDtParent.Columns["keyvalue"], mDtSummaryD.Columns["keyvalue"]);

                return mDsData;
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

        #region RCH_ANCMonthlySummary
        public DataSet RCH_ANCMonthlySummary(int mYearPart, string mMonthPart, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "RCH_ANCMonthlySummary";

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
                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtSummary = new DataTable("summary");

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_year", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_month", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));
                mDtFacilitySetup.Columns.Add("keyvalue", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_year"] = mYearPart.ToString();
                    mDataRow["para_month"] =  mMonthPart.ToString();
                    mDataRow["para_otherparameters"] = mExtraParameters;
                    mDataRow["keyvalue"] = "parentlink";

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region generate data

                
                DateTime mDstartDate = Convert.ToDateTime("01 " + mMonthPart + " " + mYearPart);

                int mDaysInMonth = DateTime.DaysInMonth(mDstartDate.Year, mDstartDate.Month);
                DateTime mDEndDate = Convert.ToDateTime(mDaysInMonth + " " +  mMonthPart + " " + mYearPart);
              
                mCommand.CommandText = "SELECT * from  rch_antenatalattendances where bookdate between " + clsGlobal.Saving_DateValue(mDstartDate) + " and " + clsGlobal.Saving_DateValue(mDEndDate);

                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSummary);
               // clsGlobal.Write_Error(pClassName, mFunctionName, Convert.ToString(mYearPart + "," + mMonthPart));

                DataTable mDAntenatal = new DataTable();

                int mSyphilisNegative = 0, mSyphilisPostive = 0, mSyphilisUnknown = 0, mOneVisit = 0, mTwoVisits = 0, mThreeVisits = 0, mFourVisits = 0, mFiveVisits = 0, mFirstVisit_0_12_Weeks = 0, mFirstVisit13Weeks = 0, mNoPreEclampsia = 0, mYesPreEclampsia = 0, mTTVDoses01 = 0, mTTVDoses2 = 0;
                int mSpTabletsYes = 0, mSpTabletsNo = 0, mFeFoTablets0119 = 0, mFeFoTablets120 = 0, mHIVPreviousNegative = 0, mHIVPreviousPositive = 0, mHIVNewNegative = 0, mHIVNewPositive = 0, mNotDone = 0, mspdoses0 =0, mspdoses1x3=0, mspdoses2x3=0, mAlbendazole1Dose=0;
                int mTotalWomenHIV = 0, mONARTYes = 0, mONARTNo = 0, mUnknownARTStatus = 0, mONCPTYes = 0, mONCPTNo = 0, mNVPSyrupYes = 0, NVPSyrupNo = 0, mTotalReceivedITN = 0, mTotalOnART = 0, mTotalAlreadyOnART =0, mTotalStartedARTat0_27wksofPreg =0, mTotalStartedARTat28wksofPreg =0;
                mDAntenatal.Columns.Add("keyvalue", typeof(System.String));
                mDAntenatal.Columns.Add("ReportingYear", typeof(System.String));
                mDAntenatal.Columns.Add("ReportingMonth", typeof(System.String));
                mDAntenatal.Columns.Add("NewWomenRegisterd", typeof(System.String));
               
                mDAntenatal.Columns.Add("SyphilisNegative", typeof(System.String));
                mDAntenatal.Columns.Add("SyphilisPostive", typeof(System.String));
                mDAntenatal.Columns.Add("SyphilisUnknown", typeof(System.String));
                mDAntenatal.Columns.Add("OneVisit", typeof(System.String));
                mDAntenatal.Columns.Add("TwoVisits", typeof(System.String));

                mDAntenatal.Columns.Add("ThreeVisits", typeof(System.String));
                mDAntenatal.Columns.Add("FourVisits", typeof(System.String));
                mDAntenatal.Columns.Add("FiveVisits", typeof(System.String));
                mDAntenatal.Columns.Add("FirstVisit_0_12_Weeks", typeof(System.String));
                mDAntenatal.Columns.Add("FirstVisit_13+_Weeks", typeof(System.String));
                mDAntenatal.Columns.Add("NoPreEclampsia", typeof(System.String));
                mDAntenatal.Columns.Add("YesPreEclampsia", typeof(System.String));
                mDAntenatal.Columns.Add("TTVDoses_0_1", typeof(System.String));
                mDAntenatal.Columns.Add("TTVDoses_2+", typeof(System.String));
               
                mDAntenatal.Columns.Add("SpTabletsYes", typeof(System.String));
                mDAntenatal.Columns.Add("SpTabletsNo", typeof(System.String));
                mDAntenatal.Columns.Add("FeFoTablets__0_119", typeof(System.String));
                mDAntenatal.Columns.Add("FeFoTablets__120", typeof(System.String));

                mDAntenatal.Columns.Add("HIVPreviousNegative", typeof(System.String));
                mDAntenatal.Columns.Add("HIVPreviousPositive", typeof(System.String));
                mDAntenatal.Columns.Add("HIVNewNegative", typeof(System.String));
                mDAntenatal.Columns.Add("HIVNewPositive", typeof(System.String));
                mDAntenatal.Columns.Add("NotDone", typeof(System.String));
                mDAntenatal.Columns.Add("TotalWomenHIV", typeof(System.String));

                mDAntenatal.Columns.Add("ONARTYes", typeof(System.String));
                mDAntenatal.Columns.Add("ONARTNo", typeof(System.String));
                mDAntenatal.Columns.Add("UnknownARTStatus", typeof(System.String));
                mDAntenatal.Columns.Add("ONCPTYes", typeof(System.String));
                mDAntenatal.Columns.Add("ONCPTNo", typeof(System.String));
                mDAntenatal.Columns.Add("NVPSyrupYes", typeof(System.String));
                mDAntenatal.Columns.Add("NVPSyrupNo", typeof(System.String));
                mDAntenatal.Columns.Add("StartDate", typeof(System.DateTime));
                mDAntenatal.Columns.Add("spdoses0", typeof(System.String));
                mDAntenatal.Columns.Add("spdoses1x3", typeof(System.String));
                mDAntenatal.Columns.Add("spdoses2x3", typeof(System.String));
                mDAntenatal.Columns.Add("albendazole1dose", typeof(System.String));
                mDAntenatal.Columns.Add("TotalReceivedITN", typeof(System.String));

                mDAntenatal.Columns.Add("TotalOnART", typeof(System.String));
                mDAntenatal.Columns.Add("TotalAlreadyOnART", typeof(System.String));
                mDAntenatal.Columns.Add("TotalStartedARTat0-27wksofPreg", typeof(System.String));
                mDAntenatal.Columns.Add("TotalStartedARTat28+wksofPreg", typeof(System.String));


               
                foreach (DataRow myRow in mDtSummary.Rows)
                {
                    int mTTV = 0;
                    DataRow mRwFirstVist = myRow;
                    mCommand.CommandText = "select * from rch_antenatalattendancelog where clientcode =" + myRow["clientcode"].ToString().Trim();
                    DataTable mDs = new DataTable();
                    OdbcDataAdapter mDp = new OdbcDataAdapter();
                    mDs.Clear();
                    mDp.SelectCommand = mCommand;
                    mDp.Fill(mDs);
                                       

                    if (mDs.Rows.Count == 1)
                    {
                        mOneVisit += 1;
                    }
                    if (mDs.Rows.Count == 2)
                    {
                        mTwoVisits += 1;
                    }
                    if (mDs.Rows.Count == 3)
                    {
                        mThreeVisits += 1;
                    }
                    if (mDs.Rows.Count == 4)
                    {
                        mFourVisits += 1;
                    }
                    if (mDs.Rows.Count >= 5)
                    {
                        mFiveVisits += 1;
                    }

                    int mAutocode = 0;
                    mAutocode = 0;
                    string mSyphilis = "";
                    string  mSpTb = "";
                    int mFeFo =0;
                    string mPreviousHIV = "", mNewHIV = "";
                    string mCPT = "";
                    int mAbendazole = 0;
                    string mART = "";
                    string mNVP = "";
                    string mITN = "";

                    Boolean mOnART = false;
                    Boolean mAlreadyOnART = false;
                    Boolean mStartedARTat0_27wksofPreg = false;
                    Boolean mStartedARTat28wksofPreg = false;
                
                    foreach (DataRow myRw in mDs.Rows)
                    {
                        if (Convert.ToUInt16(myRw["autocode"]) < mAutocode)
                        {
                            mAutocode = Convert.ToUInt16(myRow["autocode"]);
                            mRwFirstVist = myRw;
 
                        }

                        if (myRw["ttvnewdosses"].ToString() != "" || myRw["ttvnewdosses"].ToString() != "0")
                        {
                            mTTV += 1;
                        }

                        if (myRw["sp"].ToString() != "" || myRw["sp"].ToString() != "0")
                        {
                            mSpTb += 1;
                        }

                        if (myRw["fefo"].ToString() != "" || myRw["fefo"].ToString() != "0")
                        {
                            mFeFo += Convert.ToInt16(myRw["fefo"].ToString().Trim());

                        }

                        if (myRw["syphilistest"].ToString() != "" || myRw["syphilistest"].ToString() != "0")
                        {
                           mSyphilis =  myRw["syphilistest"].ToString().Trim();

                        }

                        if (myRw["previoushivtestresult"].ToString() != "" || myRw["previoushivtestresult"].ToString() != "0")
                        {
                           mPreviousHIV = myRw["previoushivtestresult"].ToString().Trim();

                        }


                        if (myRw["newhivtestresult"].ToString() != "" || myRw["newhivtestresult"].ToString() != "0")
                        {
                            mNewHIV = myRw["newhivtestresult"].ToString().Trim();

                        }

                        if (myRw["newhivtestresult"].ToString() == "Postive" || myRw["previoushivtestresult"].ToString() == "Postive")
                        {
                            mOnART=true;

                        }

                        if (myRw["previoushivtestresult"].ToString() == "Postive")
                        {
                            mAlreadyOnART=true;

                        }
                        if (mAlreadyOnART == false)
                        {
                            if (myRw["newhivtestresult"].ToString() == "Postive" && Convert.ToInt16(myRw["pregage"]) <= 27)
                            {
                                mStartedARTat0_27wksofPreg = true;
                            }

                            if (myRw["newhivtestresult"].ToString() == "Postive" && Convert.ToInt16(myRw["pregage"]) >= 28)
                            {
                                mStartedARTat28wksofPreg = true;
                            }
                        }

                        if (myRw["oncpt"].ToString() != "" || myRw["oncpt"].ToString() != "0")
                        {
                            mCPT = myRw["oncpt"].ToString().Trim();

                        }

                      
                        if (myRw["nvpsyrupdispensed"].ToString().Trim() != "")
                        {
                           mNVP = "Yes";

                        }

                       
                        if (myRw["onart"].ToString() != "" || myRw["onart"].ToString() != "0")
                        {
                           mART = myRw["onart"].ToString().Trim();

                        }

                       
                        if (myRw["bednetgiven"].ToString().Trim() == "Yes")
                        {
                            mITN = "Yes";

                        }

                        if (myRw["albendazole"].ToString() != "" || myRw["albendazole"].ToString() != "0")
                        {
                           mAbendazole += 1;

                        }
                    }


                    if (mOnART == true)
                    {
                        mTotalOnART += 1;
                    }

                    if (mAlreadyOnART == true)
                    {
                       mTotalAlreadyOnART += 1;
                    }

                    if (mStartedARTat0_27wksofPreg == true)
                    {
                        mTotalStartedARTat0_27wksofPreg += 1;
                    }

                    if (mStartedARTat28wksofPreg == true)
                    {
                        mTotalStartedARTat28wksofPreg += 1;
                    }

                    if (mITN != "")
                    {
                       mTotalReceivedITN += 1;
                    }

                    mITN = "";

                    if (mNVP != "")
                    {
                        mNVPSyrupYes += 1;
                    }
                    if (mNVP != "Yes")
                    {
                        NVPSyrupNo += 1;
                    }

                    mNVP = "";


                    if (mART == "Yes")
                    {
                        mONARTYes += 1;
                    }
                    else if (mART == "No")
                    {
                        mONARTNo += 1;
                    }
                    else
                    { mUnknownARTStatus += 1; }

                    mART = "";
                    if (mCPT == "Yes")
                    {
                        mONCPTYes += 1;
                    }
                    else if (mCPT == "No")
                    {
                        mONCPTNo += 1;
                    }

                    if (mNewHIV == "Postive")
                    {
                        mHIVNewPositive += 1;
                    }

                    if (mNewHIV == "Negative")
                    {
                        mHIVNewNegative += 1;
                    }

                    if (mNewHIV == "Postive")
                    {
                        mHIVNewPositive += 1;
                    }

                    if (mNewHIV == "ND")
                    {
                        mNotDone += 1;
                    }


                    if (mPreviousHIV == "Negative")
                    {
                      mHIVPreviousNegative += 1;
                    }

                    if (mPreviousHIV == "Postive")
                    {
                      mHIVPreviousPositive += 1;
                    }

                    if ((mPreviousHIV == "Postive") || (mNewHIV == "Postive"))
                    {
                       mTotalWomenHIV += 1;
                    }

                    if (mSyphilis == "Postive")
                    {
                        mSyphilisPostive += 1;
                    }

                    else if (mSyphilis == "Negative")
                    {
                        mSyphilisNegative += 1;
                    }

                    else
                    {
                        mSyphilisUnknown += 1;
                    }

                    if (mSpTb =="0" || mSpTb == "")
                    {
                        mspdoses0 += 1;
                    }
                    //if (mSpTb != 0)
                    //{
                    //    mSpTabletsYes += 1;
                    //}
                    if (mTTV < 2)
                    {
                        mTTVDoses01 += 1;
                    }

                    if (mTTV >= 2)
                    {
                        mTTVDoses2 += 1;
                    }

                    if (mFeFo < 120)
                    {
                        mFeFoTablets0119 += 1;
                    }
                    else if (mFeFo >=120)
                    {
                        mFeFoTablets120 += 1;
                    }

                    if (mAbendazole == 1)
                    {
                        mAlbendazole1Dose += 1;
                    }

                    if (mDs.Rows.Count != 0 && mDs.Rows.Count == 1)
                    {
                        mRwFirstVist = mDs.Rows[0];
                        if (mRwFirstVist["pregage"].ToString() != "")
                        {
                            if (Convert.ToInt16(mRwFirstVist["pregage"]) < 13)
                            {
                                mFirstVisit_0_12_Weeks += 1;
                            }
                            if (Convert.ToInt16(mRwFirstVist["pregage"]) >= 13)
                            {
                                mFirstVisit13Weeks += 1;
                            }

                        }

                       
                    }

                    if (mDs.Rows.Count != 0 && mDs.Rows.Count > 1)
                    {
                        if (mRwFirstVist["pregage"].ToString() != "")
                        {
                            if (Convert.ToInt16(mRwFirstVist["pregage"]) < 13)
                            {
                                mFirstVisit_0_12_Weeks += 1;
                            }
                            if (Convert.ToInt16(mRwFirstVist["pregage"]) >= 13)
                            {
                                mFirstVisit13Weeks += 1;
                            }
                        }
                                               
                    }
                    
                }

                
                mDAntenatal.AcceptChanges();

                mDAntenatal.Rows.Add("parentlink", mYearPart, mMonthPart, mDtSummary.Rows.Count.ToString(), mSyphilisNegative, mSyphilisPostive, mSyphilisUnknown, mOneVisit, mTwoVisits, mThreeVisits, mFourVisits, mFiveVisits, mFirstVisit_0_12_Weeks, mFirstVisit13Weeks, mNoPreEclampsia, mYesPreEclampsia, mTTVDoses01, mTTVDoses2, mSpTabletsYes, mSpTabletsNo, mFeFoTablets0119, mFeFoTablets120, mHIVPreviousNegative, mHIVPreviousPositive, mHIVNewNegative, mHIVNewPositive, mNotDone, mTotalWomenHIV, mONARTYes, mONARTNo, mUnknownARTStatus, mONCPTYes, mONCPTNo, mNVPSyrupYes, NVPSyrupNo, mDstartDate, mspdoses0, mspdoses1x3, mspdoses2x3, mAlbendazole1Dose, mTotalReceivedITN, mTotalOnART, mTotalAlreadyOnART, mTotalStartedARTat0_27wksofPreg, mTotalStartedARTat28wksofPreg);
                mDAntenatal.AcceptChanges();

                #endregion

                DataSet mDsData = new DataSet("summary");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDAntenatal);
                mDsData.Relations.Add("childrelationship", mDtFacilitySetup.Columns["keyvalue"], mDAntenatal.Columns["keyvalue"]);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #region RCH_MaternityMonthlySummary
        public DataSet RCH_MaternityMonthlySummary(int mYearPart, string mMonthPart, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "RCH_MaternityMonthlySummary";

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
                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtSummary = new DataTable("summary");

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_year", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_month", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));
                mDtFacilitySetup.Columns.Add("keyvalue", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_year"] = mYearPart.ToString();
                    mDataRow["para_month"] = mMonthPart.ToString();
                    mDataRow["para_otherparameters"] = mExtraParameters;
                    mDataRow["keyvalue"] = "parentlink";

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region generate data


                DateTime mDstartDate = Convert.ToDateTime("01 " + mMonthPart + " " + mYearPart);

                int mDaysInMonth = DateTime.DaysInMonth(mDstartDate.Year, mDstartDate.Month);
                DateTime mDEndDate = Convert.ToDateTime(mDaysInMonth + " " + mMonthPart + " " + mYearPart);

                mCommand.CommandText = "SELECT * from  rch_antenatalattendances where bookdate between " + clsGlobal.Saving_DateValue(mDstartDate) + " and " + clsGlobal.Saving_DateValue(mDEndDate);

                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSummary);
                // clsGlobal.Write_Error(pClassName, mFunctionName, Convert.ToString(mYearPart + "," + mMonthPart));

                DataTable mDAntenatal = new DataTable();

                int mSyphilisNegative = 0, mSyphilisPostive = 0, mSyphilisUnknown = 0, mOneVisit = 0, mTwoVisits = 0, mThreeVisits = 0, mFourVisits = 0, mFiveVisits = 0, mFirstVisit_0_12_Weeks = 0, mFirstVisit13Weeks = 0, mNoPreEclampsia = 0, mYesPreEclampsia = 0, mTTVDoses01 = 0, mTTVDoses2 = 0;
                int mSpTabletsYes = 0, mSpTabletsNo = 0, mFeFoTablets0119 = 0, mFeFoTablets120 = 0, mHIVPreviousNegative = 0, mHIVPreviousPositive = 0, mHIVNewNegative = 0, mHIVNewPositive = 0, mNotDone = 0, mspdoses0 = 0, mspdoses1x3 = 0, mspdoses2x3 = 0, mAlbendazole1Dose = 0;
                int mTotalWomenHIV = 0, mONARTYes = 0, mONARTNo = 0, mUnknownARTStatus = 0, mONCPTYes = 0, mONCPTNo = 0, mNVPSyrupYes = 0, NVPSyrupNo = 0, mTotalReceivedITN = 0, mTotalOnART = 0, mTotalAlreadyOnART = 0, mTotalStartedARTat0_27wksofPreg = 0, mTotalStartedARTat28wksofPreg = 0;
                mDAntenatal.Columns.Add("keyvalue", typeof(System.String));
                mDAntenatal.Columns.Add("ReportingYear", typeof(System.String));
                mDAntenatal.Columns.Add("ReportingMonth", typeof(System.String));
                mDAntenatal.Columns.Add("NewWomenRegisterd", typeof(System.String));

                mDAntenatal.Columns.Add("SyphilisNegative", typeof(System.String));
                mDAntenatal.Columns.Add("SyphilisPostive", typeof(System.String));
                mDAntenatal.Columns.Add("SyphilisUnknown", typeof(System.String));
                mDAntenatal.Columns.Add("OneVisit", typeof(System.String));
                mDAntenatal.Columns.Add("TwoVisits", typeof(System.String));

                mDAntenatal.Columns.Add("ThreeVisits", typeof(System.String));
                mDAntenatal.Columns.Add("FourVisits", typeof(System.String));
                mDAntenatal.Columns.Add("FiveVisits", typeof(System.String));
                mDAntenatal.Columns.Add("FirstVisit_0_12_Weeks", typeof(System.String));
                mDAntenatal.Columns.Add("FirstVisit_13+_Weeks", typeof(System.String));
                mDAntenatal.Columns.Add("NoPreEclampsia", typeof(System.String));
                mDAntenatal.Columns.Add("YesPreEclampsia", typeof(System.String));
                mDAntenatal.Columns.Add("TTVDoses_0_1", typeof(System.String));
                mDAntenatal.Columns.Add("TTVDoses_2+", typeof(System.String));

                mDAntenatal.Columns.Add("SpTabletsYes", typeof(System.String));
                mDAntenatal.Columns.Add("SpTabletsNo", typeof(System.String));
                mDAntenatal.Columns.Add("FeFoTablets__0_119", typeof(System.String));
                mDAntenatal.Columns.Add("FeFoTablets__120", typeof(System.String));

                mDAntenatal.Columns.Add("HIVPreviousNegative", typeof(System.String));
                mDAntenatal.Columns.Add("HIVPreviousPositive", typeof(System.String));
                mDAntenatal.Columns.Add("HIVNewNegative", typeof(System.String));
                mDAntenatal.Columns.Add("HIVNewPositive", typeof(System.String));
                mDAntenatal.Columns.Add("NotDone", typeof(System.String));
                mDAntenatal.Columns.Add("TotalWomenHIV", typeof(System.String));

                mDAntenatal.Columns.Add("ONARTYes", typeof(System.String));
                mDAntenatal.Columns.Add("ONARTNo", typeof(System.String));
                mDAntenatal.Columns.Add("UnknownARTStatus", typeof(System.String));
                mDAntenatal.Columns.Add("ONCPTYes", typeof(System.String));
                mDAntenatal.Columns.Add("ONCPTNo", typeof(System.String));
                mDAntenatal.Columns.Add("NVPSyrupYes", typeof(System.String));
                mDAntenatal.Columns.Add("NVPSyrupNo", typeof(System.String));
                mDAntenatal.Columns.Add("StartDate", typeof(System.DateTime));
                mDAntenatal.Columns.Add("spdoses0", typeof(System.String));
                mDAntenatal.Columns.Add("spdoses1x3", typeof(System.String));
                mDAntenatal.Columns.Add("spdoses2x3", typeof(System.String));
                mDAntenatal.Columns.Add("albendazole1dose", typeof(System.String));
                mDAntenatal.Columns.Add("TotalReceivedITN", typeof(System.String));

                mDAntenatal.Columns.Add("TotalOnART", typeof(System.String));
                mDAntenatal.Columns.Add("TotalAlreadyOnART", typeof(System.String));
                mDAntenatal.Columns.Add("TotalStartedARTat0-27wksofPreg", typeof(System.String));
                mDAntenatal.Columns.Add("TotalStartedARTat28+wksofPreg", typeof(System.String));



                foreach (DataRow myRow in mDtSummary.Rows)
                {
                    int mTTV = 0;
                    DataRow mRwFirstVist = myRow;
                    mCommand.CommandText = "select * from rch_antenatalattendancelog where clientcode =" + myRow["clientcode"].ToString().Trim();
                    DataTable mDs = new DataTable();
                    OdbcDataAdapter mDp = new OdbcDataAdapter();
                    mDs.Clear();
                    mDp.SelectCommand = mCommand;
                    mDp.Fill(mDs);


                    if (mDs.Rows.Count == 1)
                    {
                        mOneVisit += 1;
                    }
                    if (mDs.Rows.Count == 2)
                    {
                        mTwoVisits += 1;
                    }
                    if (mDs.Rows.Count == 3)
                    {
                        mThreeVisits += 1;
                    }
                    if (mDs.Rows.Count == 4)
                    {
                        mFourVisits += 1;
                    }
                    if (mDs.Rows.Count >= 5)
                    {
                        mFiveVisits += 1;
                    }

                    int mAutocode = 0;
                    mAutocode = 0;
                    string mSyphilis = "";
                    string mSpTb = "";
                    int mFeFo = 0;
                    string mPreviousHIV = "", mNewHIV = "";
                    string mCPT = "";
                    int mAbendazole = 0;
                    string mART = "";
                    string mNVP = "";
                    string mITN = "";

                    Boolean mOnART = false;
                    Boolean mAlreadyOnART = false;
                    Boolean mStartedARTat0_27wksofPreg = false;
                    Boolean mStartedARTat28wksofPreg = false;

                    foreach (DataRow myRw in mDs.Rows)
                    {
                        if (Convert.ToUInt16(myRw["autocode"]) < mAutocode)
                        {
                            mAutocode = Convert.ToUInt16(myRow["autocode"]);
                            mRwFirstVist = myRw;

                        }

                        if (myRw["ttvnewdosses"].ToString() != "" || myRw["ttvnewdosses"].ToString() != "0")
                        {
                            mTTV += 1;
                        }

                        if (myRw["sp"].ToString() != "" || myRw["sp"].ToString() != "0")
                        {
                            mSpTb += 1;
                        }

                        if (myRw["fefo"].ToString() != "" || myRw["fefo"].ToString() != "0")
                        {
                            mFeFo += Convert.ToInt16(myRw["fefo"].ToString().Trim());

                        }

                        if (myRw["syphilistest"].ToString() != "" || myRw["syphilistest"].ToString() != "0")
                        {
                            mSyphilis = myRw["syphilistest"].ToString().Trim();

                        }

                        if (myRw["previoushivtestresult"].ToString() != "" || myRw["previoushivtestresult"].ToString() != "0")
                        {
                            mPreviousHIV = myRw["previoushivtestresult"].ToString().Trim();

                        }


                        if (myRw["newhivtestresult"].ToString() != "" || myRw["newhivtestresult"].ToString() != "0")
                        {
                            mNewHIV = myRw["newhivtestresult"].ToString().Trim();

                        }

                        if (myRw["newhivtestresult"].ToString() == "Postive" || myRw["previoushivtestresult"].ToString() == "Postive")
                        {
                            mOnART = true;

                        }

                        if (myRw["previoushivtestresult"].ToString() == "Postive")
                        {
                            mAlreadyOnART = true;

                        }
                        if (mAlreadyOnART == false)
                        {
                            if (myRw["newhivtestresult"].ToString() == "Postive" && Convert.ToInt16(myRw["pregage"]) <= 27)
                            {
                                mStartedARTat0_27wksofPreg = true;
                            }

                            if (myRw["newhivtestresult"].ToString() == "Postive" && Convert.ToInt16(myRw["pregage"]) >= 28)
                            {
                                mStartedARTat28wksofPreg = true;
                            }
                        }

                        if (myRw["oncpt"].ToString() != "" || myRw["oncpt"].ToString() != "0")
                        {
                            mCPT = myRw["oncpt"].ToString().Trim();

                        }


                        if (myRw["nvpsyrupdispensed"].ToString().Trim() != "")
                        {
                            mNVP = "Yes";

                        }


                        if (myRw["onart"].ToString() != "" || myRw["onart"].ToString() != "0")
                        {
                            mART = myRw["onart"].ToString().Trim();

                        }


                        if (myRw["bednetgiven"].ToString().Trim() == "Yes")
                        {
                            mITN = "Yes";

                        }

                        if (myRw["albendazole"].ToString() != "" || myRw["albendazole"].ToString() != "0")
                        {
                            mAbendazole += 1;

                        }
                    }


                    if (mOnART == true)
                    {
                        mTotalOnART += 1;
                    }

                    if (mAlreadyOnART == true)
                    {
                        mTotalAlreadyOnART += 1;
                    }

                    if (mStartedARTat0_27wksofPreg == true)
                    {
                        mTotalStartedARTat0_27wksofPreg += 1;
                    }

                    if (mStartedARTat28wksofPreg == true)
                    {
                        mTotalStartedARTat28wksofPreg += 1;
                    }

                    if (mITN != "")
                    {
                        mTotalReceivedITN += 1;
                    }

                    mITN = "";

                    if (mNVP != "")
                    {
                        mNVPSyrupYes += 1;
                    }
                    if (mNVP != "Yes")
                    {
                        NVPSyrupNo += 1;
                    }

                    mNVP = "";


                    if (mART == "Yes")
                    {
                        mONARTYes += 1;
                    }
                    else if (mART == "No")
                    {
                        mONARTNo += 1;
                    }
                    else
                    { mUnknownARTStatus += 1; }

                    mART = "";
                    if (mCPT == "Yes")
                    {
                        mONCPTYes += 1;
                    }
                    else if (mCPT == "No")
                    {
                        mONCPTNo += 1;
                    }

                    if (mNewHIV == "Postive")
                    {
                        mHIVNewPositive += 1;
                    }

                    if (mNewHIV == "Negative")
                    {
                        mHIVNewNegative += 1;
                    }

                    if (mNewHIV == "Postive")
                    {
                        mHIVNewPositive += 1;
                    }

                    if (mNewHIV == "ND")
                    {
                        mNotDone += 1;
                    }


                    if (mPreviousHIV == "Negative")
                    {
                        mHIVPreviousNegative += 1;
                    }

                    if (mPreviousHIV == "Postive")
                    {
                        mHIVPreviousPositive += 1;
                    }

                    if ((mPreviousHIV == "Postive") || (mNewHIV == "Postive"))
                    {
                        mTotalWomenHIV += 1;
                    }

                    if (mSyphilis == "Postive")
                    {
                        mSyphilisPostive += 1;
                    }

                    else if (mSyphilis == "Negative")
                    {
                        mSyphilisNegative += 1;
                    }

                    else
                    {
                        mSyphilisUnknown += 1;
                    }

                    if (mSpTb == "0" || mSpTb == "")
                    {
                        mspdoses0 += 1;
                    }
                    //if (mSpTb != 0)
                    //{
                    //    mSpTabletsYes += 1;
                    //}
                    if (mTTV < 2)
                    {
                        mTTVDoses01 += 1;
                    }

                    if (mTTV >= 2)
                    {
                        mTTVDoses2 += 1;
                    }

                    if (mFeFo < 120)
                    {
                        mFeFoTablets0119 += 1;
                    }
                    else if (mFeFo >= 120)
                    {
                        mFeFoTablets120 += 1;
                    }

                    if (mAbendazole == 1)
                    {
                        mAlbendazole1Dose += 1;
                    }

                    if (mDs.Rows.Count != 0 && mDs.Rows.Count == 1)
                    {
                        mRwFirstVist = mDs.Rows[0];
                        if (mRwFirstVist["pregage"].ToString() != "")
                        {
                            if (Convert.ToInt16(mRwFirstVist["pregage"]) < 13)
                            {
                                mFirstVisit_0_12_Weeks += 1;
                            }
                            if (Convert.ToInt16(mRwFirstVist["pregage"]) >= 13)
                            {
                                mFirstVisit13Weeks += 1;
                            }

                        }


                    }

                    if (mDs.Rows.Count != 0 && mDs.Rows.Count > 1)
                    {
                        if (mRwFirstVist["pregage"].ToString() != "")
                        {
                            if (Convert.ToInt16(mRwFirstVist["pregage"]) < 13)
                            {
                                mFirstVisit_0_12_Weeks += 1;
                            }
                            if (Convert.ToInt16(mRwFirstVist["pregage"]) >= 13)
                            {
                                mFirstVisit13Weeks += 1;
                            }
                        }

                    }

                }


                mDAntenatal.AcceptChanges();

                mDAntenatal.Rows.Add("parentlink", mYearPart, mMonthPart, mDtSummary.Rows.Count.ToString(), mSyphilisNegative, mSyphilisPostive, mSyphilisUnknown, mOneVisit, mTwoVisits, mThreeVisits, mFourVisits, mFiveVisits, mFirstVisit_0_12_Weeks, mFirstVisit13Weeks, mNoPreEclampsia, mYesPreEclampsia, mTTVDoses01, mTTVDoses2, mSpTabletsYes, mSpTabletsNo, mFeFoTablets0119, mFeFoTablets120, mHIVPreviousNegative, mHIVPreviousPositive, mHIVNewNegative, mHIVNewPositive, mNotDone, mTotalWomenHIV, mONARTYes, mONARTNo, mUnknownARTStatus, mONCPTYes, mONCPTNo, mNVPSyrupYes, NVPSyrupNo, mDstartDate, mspdoses0, mspdoses1x3, mspdoses2x3, mAlbendazole1Dose, mTotalReceivedITN, mTotalOnART, mTotalAlreadyOnART, mTotalStartedARTat0_27wksofPreg, mTotalStartedARTat28wksofPreg);
                mDAntenatal.AcceptChanges();

                #endregion

                DataSet mDsData = new DataSet("summary");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDAntenatal);
                mDsData.Relations.Add("childrelationship", mDtFacilitySetup.Columns["keyvalue"], mDAntenatal.Columns["keyvalue"]);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #region RCH_ObstetricMonthlySummary
        public DataSet RCH_ObstetricMonthlySummary(int mYearPart, string mMonthPart, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "RCH_ObstetricMonthlySummary";

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
                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtSummary = new DataTable("summary");

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_year", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_month", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));
                mDtFacilitySetup.Columns.Add("keyvalue", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_year"] = mYearPart.ToString();
                    mDataRow["para_month"] = mMonthPart.ToString();
                    mDataRow["para_otherparameters"] = mExtraParameters;
                    mDataRow["keyvalue"] = "parentlink";

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region generate data


                DateTime mDstartDate = Convert.ToDateTime("01 " + mMonthPart + " " + mYearPart);

                int mDaysInMonth = DateTime.DaysInMonth(mDstartDate.Year, mDstartDate.Month);
                DateTime mDEndDate = Convert.ToDateTime(mDaysInMonth + " " + mMonthPart + " " + mYearPart);

                mCommand.CommandText = "SELECT * from  rch_antenatalattendances where bookdate between " + clsGlobal.Saving_DateValue(mDstartDate) + " and " + clsGlobal.Saving_DateValue(mDEndDate);

                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSummary);
                // clsGlobal.Write_Error(pClassName, mFunctionName, Convert.ToString(mYearPart + "," + mMonthPart));

                DataTable mDAntenatal = new DataTable();

                int mSyphilisNegative = 0, mSyphilisPostive = 0, mSyphilisUnknown = 0, mOneVisit = 0, mTwoVisits = 0, mThreeVisits = 0, mFourVisits = 0, mFiveVisits = 0, mFirstVisit_0_12_Weeks = 0, mFirstVisit13Weeks = 0, mNoPreEclampsia = 0, mYesPreEclampsia = 0, mTTVDoses01 = 0, mTTVDoses2 = 0;
                int mSpTabletsYes = 0, mSpTabletsNo = 0, mFeFoTablets0119 = 0, mFeFoTablets120 = 0, mHIVPreviousNegative = 0, mHIVPreviousPositive = 0, mHIVNewNegative = 0, mHIVNewPositive = 0, mNotDone = 0, mspdoses0 = 0, mspdoses1x3 = 0, mspdoses2x3 = 0, mAlbendazole1Dose = 0;
                int mTotalWomenHIV = 0, mONARTYes = 0, mONARTNo = 0, mUnknownARTStatus = 0, mONCPTYes = 0, mONCPTNo = 0, mNVPSyrupYes = 0, NVPSyrupNo = 0, mTotalReceivedITN = 0, mTotalOnART = 0, mTotalAlreadyOnART = 0, mTotalStartedARTat0_27wksofPreg = 0, mTotalStartedARTat28wksofPreg = 0;
                mDAntenatal.Columns.Add("keyvalue", typeof(System.String));
                mDAntenatal.Columns.Add("ReportingYear", typeof(System.String));
                mDAntenatal.Columns.Add("ReportingMonth", typeof(System.String));
                mDAntenatal.Columns.Add("NewWomenRegisterd", typeof(System.String));

                mDAntenatal.Columns.Add("SyphilisNegative", typeof(System.String));
                mDAntenatal.Columns.Add("SyphilisPostive", typeof(System.String));
                mDAntenatal.Columns.Add("SyphilisUnknown", typeof(System.String));
                mDAntenatal.Columns.Add("OneVisit", typeof(System.String));
                mDAntenatal.Columns.Add("TwoVisits", typeof(System.String));

                mDAntenatal.Columns.Add("ThreeVisits", typeof(System.String));
                mDAntenatal.Columns.Add("FourVisits", typeof(System.String));
                mDAntenatal.Columns.Add("FiveVisits", typeof(System.String));
                mDAntenatal.Columns.Add("FirstVisit_0_12_Weeks", typeof(System.String));
                mDAntenatal.Columns.Add("FirstVisit_13+_Weeks", typeof(System.String));
                mDAntenatal.Columns.Add("NoPreEclampsia", typeof(System.String));
                mDAntenatal.Columns.Add("YesPreEclampsia", typeof(System.String));
                mDAntenatal.Columns.Add("TTVDoses_0_1", typeof(System.String));
                mDAntenatal.Columns.Add("TTVDoses_2+", typeof(System.String));

                mDAntenatal.Columns.Add("SpTabletsYes", typeof(System.String));
                mDAntenatal.Columns.Add("SpTabletsNo", typeof(System.String));
                mDAntenatal.Columns.Add("FeFoTablets__0_119", typeof(System.String));
                mDAntenatal.Columns.Add("FeFoTablets__120", typeof(System.String));

                mDAntenatal.Columns.Add("HIVPreviousNegative", typeof(System.String));
                mDAntenatal.Columns.Add("HIVPreviousPositive", typeof(System.String));
                mDAntenatal.Columns.Add("HIVNewNegative", typeof(System.String));
                mDAntenatal.Columns.Add("HIVNewPositive", typeof(System.String));
                mDAntenatal.Columns.Add("NotDone", typeof(System.String));
                mDAntenatal.Columns.Add("TotalWomenHIV", typeof(System.String));

                mDAntenatal.Columns.Add("ONARTYes", typeof(System.String));
                mDAntenatal.Columns.Add("ONARTNo", typeof(System.String));
                mDAntenatal.Columns.Add("UnknownARTStatus", typeof(System.String));
                mDAntenatal.Columns.Add("ONCPTYes", typeof(System.String));
                mDAntenatal.Columns.Add("ONCPTNo", typeof(System.String));
                mDAntenatal.Columns.Add("NVPSyrupYes", typeof(System.String));
                mDAntenatal.Columns.Add("NVPSyrupNo", typeof(System.String));
                mDAntenatal.Columns.Add("StartDate", typeof(System.DateTime));
                mDAntenatal.Columns.Add("spdoses0", typeof(System.String));
                mDAntenatal.Columns.Add("spdoses1x3", typeof(System.String));
                mDAntenatal.Columns.Add("spdoses2x3", typeof(System.String));
                mDAntenatal.Columns.Add("albendazole1dose", typeof(System.String));
                mDAntenatal.Columns.Add("TotalReceivedITN", typeof(System.String));

                mDAntenatal.Columns.Add("TotalOnART", typeof(System.String));
                mDAntenatal.Columns.Add("TotalAlreadyOnART", typeof(System.String));
                mDAntenatal.Columns.Add("TotalStartedARTat0-27wksofPreg", typeof(System.String));
                mDAntenatal.Columns.Add("TotalStartedARTat28+wksofPreg", typeof(System.String));



                foreach (DataRow myRow in mDtSummary.Rows)
                {
                    int mTTV = 0;
                    DataRow mRwFirstVist = myRow;
                    mCommand.CommandText = "select * from rch_antenatalattendancelog where clientcode =" + myRow["clientcode"].ToString().Trim();
                    DataTable mDs = new DataTable();
                    OdbcDataAdapter mDp = new OdbcDataAdapter();
                    mDs.Clear();
                    mDp.SelectCommand = mCommand;
                    mDp.Fill(mDs);


                    if (mDs.Rows.Count == 1)
                    {
                        mOneVisit += 1;
                    }
                    if (mDs.Rows.Count == 2)
                    {
                        mTwoVisits += 1;
                    }
                    if (mDs.Rows.Count == 3)
                    {
                        mThreeVisits += 1;
                    }
                    if (mDs.Rows.Count == 4)
                    {
                        mFourVisits += 1;
                    }
                    if (mDs.Rows.Count >= 5)
                    {
                        mFiveVisits += 1;
                    }

                    int mAutocode = 0;
                    mAutocode = 0;
                    string mSyphilis = "";
                    string mSpTb = "";
                    int mFeFo = 0;
                    string mPreviousHIV = "", mNewHIV = "";
                    string mCPT = "";
                    int mAbendazole = 0;
                    string mART = "";
                    string mNVP = "";
                    string mITN = "";

                    Boolean mOnART = false;
                    Boolean mAlreadyOnART = false;
                    Boolean mStartedARTat0_27wksofPreg = false;
                    Boolean mStartedARTat28wksofPreg = false;

                    foreach (DataRow myRw in mDs.Rows)
                    {
                        if (Convert.ToUInt16(myRw["autocode"]) < mAutocode)
                        {
                            mAutocode = Convert.ToUInt16(myRow["autocode"]);
                            mRwFirstVist = myRw;

                        }

                        if (myRw["ttvnewdosses"].ToString() != "" || myRw["ttvnewdosses"].ToString() != "0")
                        {
                            mTTV += 1;
                        }

                        if (myRw["sp"].ToString() != "" || myRw["sp"].ToString() != "0")
                        {
                            mSpTb += 1;
                        }

                        if (myRw["fefo"].ToString() != "" || myRw["fefo"].ToString() != "0")
                        {
                            mFeFo += Convert.ToInt16(myRw["fefo"].ToString().Trim());

                        }

                        if (myRw["syphilistest"].ToString() != "" || myRw["syphilistest"].ToString() != "0")
                        {
                            mSyphilis = myRw["syphilistest"].ToString().Trim();

                        }

                        if (myRw["previoushivtestresult"].ToString() != "" || myRw["previoushivtestresult"].ToString() != "0")
                        {
                            mPreviousHIV = myRw["previoushivtestresult"].ToString().Trim();

                        }


                        if (myRw["newhivtestresult"].ToString() != "" || myRw["newhivtestresult"].ToString() != "0")
                        {
                            mNewHIV = myRw["newhivtestresult"].ToString().Trim();

                        }

                        if (myRw["newhivtestresult"].ToString() == "Postive" || myRw["previoushivtestresult"].ToString() == "Postive")
                        {
                            mOnART = true;

                        }

                        if (myRw["previoushivtestresult"].ToString() == "Postive")
                        {
                            mAlreadyOnART = true;

                        }
                        if (mAlreadyOnART == false)
                        {
                            if (myRw["newhivtestresult"].ToString() == "Postive" && Convert.ToInt16(myRw["pregage"]) <= 27)
                            {
                                mStartedARTat0_27wksofPreg = true;
                            }

                            if (myRw["newhivtestresult"].ToString() == "Postive" && Convert.ToInt16(myRw["pregage"]) >= 28)
                            {
                                mStartedARTat28wksofPreg = true;
                            }
                        }

                        if (myRw["oncpt"].ToString() != "" || myRw["oncpt"].ToString() != "0")
                        {
                            mCPT = myRw["oncpt"].ToString().Trim();

                        }


                        if (myRw["nvpsyrupdispensed"].ToString().Trim() != "")
                        {
                            mNVP = "Yes";

                        }


                        if (myRw["onart"].ToString() != "" || myRw["onart"].ToString() != "0")
                        {
                            mART = myRw["onart"].ToString().Trim();

                        }


                        if (myRw["bednetgiven"].ToString().Trim() == "Yes")
                        {
                            mITN = "Yes";

                        }

                        if (myRw["albendazole"].ToString() != "" || myRw["albendazole"].ToString() != "0")
                        {
                            mAbendazole += 1;

                        }
                    }


                    if (mOnART == true)
                    {
                        mTotalOnART += 1;
                    }

                    if (mAlreadyOnART == true)
                    {
                        mTotalAlreadyOnART += 1;
                    }

                    if (mStartedARTat0_27wksofPreg == true)
                    {
                        mTotalStartedARTat0_27wksofPreg += 1;
                    }

                    if (mStartedARTat28wksofPreg == true)
                    {
                        mTotalStartedARTat28wksofPreg += 1;
                    }

                    if (mITN != "")
                    {
                        mTotalReceivedITN += 1;
                    }

                    mITN = "";

                    if (mNVP != "")
                    {
                        mNVPSyrupYes += 1;
                    }
                    if (mNVP != "Yes")
                    {
                        NVPSyrupNo += 1;
                    }

                    mNVP = "";


                    if (mART == "Yes")
                    {
                        mONARTYes += 1;
                    }
                    else if (mART == "No")
                    {
                        mONARTNo += 1;
                    }
                    else
                    { mUnknownARTStatus += 1; }

                    mART = "";
                    if (mCPT == "Yes")
                    {
                        mONCPTYes += 1;
                    }
                    else if (mCPT == "No")
                    {
                        mONCPTNo += 1;
                    }

                    if (mNewHIV == "Postive")
                    {
                        mHIVNewPositive += 1;
                    }

                    if (mNewHIV == "Negative")
                    {
                        mHIVNewNegative += 1;
                    }

                    if (mNewHIV == "Postive")
                    {
                        mHIVNewPositive += 1;
                    }

                    if (mNewHIV == "ND")
                    {
                        mNotDone += 1;
                    }


                    if (mPreviousHIV == "Negative")
                    {
                        mHIVPreviousNegative += 1;
                    }

                    if (mPreviousHIV == "Postive")
                    {
                        mHIVPreviousPositive += 1;
                    }

                    if ((mPreviousHIV == "Postive") || (mNewHIV == "Postive"))
                    {
                        mTotalWomenHIV += 1;
                    }

                    if (mSyphilis == "Postive")
                    {
                        mSyphilisPostive += 1;
                    }

                    else if (mSyphilis == "Negative")
                    {
                        mSyphilisNegative += 1;
                    }

                    else
                    {
                        mSyphilisUnknown += 1;
                    }

                    if (mSpTb == "0" || mSpTb == "")
                    {
                        mspdoses0 += 1;
                    }
                    //if (mSpTb != 0)
                    //{
                    //    mSpTabletsYes += 1;
                    //}
                    if (mTTV < 2)
                    {
                        mTTVDoses01 += 1;
                    }

                    if (mTTV >= 2)
                    {
                        mTTVDoses2 += 1;
                    }

                    if (mFeFo < 120)
                    {
                        mFeFoTablets0119 += 1;
                    }
                    else if (mFeFo >= 120)
                    {
                        mFeFoTablets120 += 1;
                    }

                    if (mAbendazole == 1)
                    {
                        mAlbendazole1Dose += 1;
                    }

                    if (mDs.Rows.Count != 0 && mDs.Rows.Count == 1)
                    {
                        mRwFirstVist = mDs.Rows[0];
                        if (mRwFirstVist["pregage"].ToString() != "")
                        {
                            if (Convert.ToInt16(mRwFirstVist["pregage"]) < 13)
                            {
                                mFirstVisit_0_12_Weeks += 1;
                            }
                            if (Convert.ToInt16(mRwFirstVist["pregage"]) >= 13)
                            {
                                mFirstVisit13Weeks += 1;
                            }

                        }


                    }

                    if (mDs.Rows.Count != 0 && mDs.Rows.Count > 1)
                    {
                        if (mRwFirstVist["pregage"].ToString() != "")
                        {
                            if (Convert.ToInt16(mRwFirstVist["pregage"]) < 13)
                            {
                                mFirstVisit_0_12_Weeks += 1;
                            }
                            if (Convert.ToInt16(mRwFirstVist["pregage"]) >= 13)
                            {
                                mFirstVisit13Weeks += 1;
                            }
                        }

                    }

                }


                mDAntenatal.AcceptChanges();

                mDAntenatal.Rows.Add("parentlink", mYearPart, mMonthPart, mDtSummary.Rows.Count.ToString(), mSyphilisNegative, mSyphilisPostive, mSyphilisUnknown, mOneVisit, mTwoVisits, mThreeVisits, mFourVisits, mFiveVisits, mFirstVisit_0_12_Weeks, mFirstVisit13Weeks, mNoPreEclampsia, mYesPreEclampsia, mTTVDoses01, mTTVDoses2, mSpTabletsYes, mSpTabletsNo, mFeFoTablets0119, mFeFoTablets120, mHIVPreviousNegative, mHIVPreviousPositive, mHIVNewNegative, mHIVNewPositive, mNotDone, mTotalWomenHIV, mONARTYes, mONARTNo, mUnknownARTStatus, mONCPTYes, mONCPTNo, mNVPSyrupYes, NVPSyrupNo, mDstartDate, mspdoses0, mspdoses1x3, mspdoses2x3, mAlbendazole1Dose, mTotalReceivedITN, mTotalOnART, mTotalAlreadyOnART, mTotalStartedARTat0_27wksofPreg, mTotalStartedARTat28wksofPreg);
                mDAntenatal.AcceptChanges();

                #endregion

                DataSet mDsData = new DataSet("summary");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDAntenatal);
                mDsData.Relations.Add("childrelationship", mDtFacilitySetup.Columns["keyvalue"], mDAntenatal.Columns["keyvalue"]);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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
        
        #endregion

        #region CTC/ART

        #region CTC_AttendanceCountAge
        public DataSet CTC_AttendanceCountAge(DateTime mDateFrom, DateTime mDateTo, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "CTC_AttendanceCountAge";

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
                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtSummary = new DataTable("summary");
                DataTable mDtSummary2 = new DataTable("summary2");

                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_datefrom", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_dateto", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));
                mDtFacilitySetup.Columns.Add("keyvalue", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_datefrom"] = mDateFrom;
                    mDataRow["para_dateto"] = mDateTo;
                    mDataRow["para_otherparameters"] = mExtraParameters;
                    mDataRow["keyvalue"] = "parentlink";

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region generate data

                #region new

                mCommand.CommandText = ""
                    + "SELECT "
                        + "'parentlink' AS keyvalue, "
                        + "1 AS groupindex, "
                        + "'NEW' AS visittype, "
                        + "rc.value1, "
                        + "rc.value2, "
                        + "rc.description, "
                        + "SUM(CASE WHEN ac.pmale IS NULL THEN 0 ELSE ac.pmale END) AS pmale, "
                        + "SUM(CASE WHEN ac.pfemale IS NULL THEN 0 ELSE ac.pfemale END) AS pfemale, "
                        + "SUM(CASE WHEN ac.ptotal IS NULL THEN 0 ELSE ac.ptotal END) AS ptotal, "
                        + "SUM(CASE WHEN ac.nmale IS NULL THEN 0 ELSE ac.nmale END) AS nmale, "
                        + "SUM(CASE WHEN ac.nfemale IS NULL THEN 0 ELSE ac.nfemale END) AS nfemale, "
                        + "SUM(CASE WHEN ac.ntotal IS NULL THEN 0 ELSE ac.ntotal END) AS ntotal, "
                        + "SUM(CASE WHEN ac.totalmale IS NULL THEN 0 ELSE ac.totalmale END) AS totalmale, "
                        + "SUM(CASE WHEN ac.totalfemale IS NULL THEN 0 ELSE ac.totalfemale END) AS totalfemale, "
                        + "SUM(CASE WHEN ac.total IS NULL THEN 0 ELSE ac.total END) AS total "
                    + "FROM sys_reportcharts AS rc "
                    + "LEFT OUTER JOIN view_ctc_attendancecount AS ac ON "
                    + "ac.patientage>=rc.value1 AND ac.patientage<rc.value2 "
                    + "AND transdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + " "
                    + "AND visittype='NEW' "
                    + "WHERE reportcode='REP" + Convert.ToInt16(AfyaPro_Types.clsEnums.BuiltInReports.CTCAttendanceCountAge) + "' "
                    + "GROUP BY "
                        + "rc.value1, "
                        + "rc.value2, "
                        + "rc.description";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSummary);

                #endregion

                #region re-attendance

                mCommand.CommandText = ""
                    + "SELECT "
                        + "'parentlink' AS keyvalue, "
                        + "1 AS groupindex, "
                        + "'RE-ATTENDANCE' AS visittype, "
                        + "rc.value1, "
                        + "rc.value2, "
                        + "rc.description, "
                        + "SUM(CASE WHEN ac.pmale IS NULL THEN 0 ELSE ac.pmale END) AS pmale, "
                        + "SUM(CASE WHEN ac.pfemale IS NULL THEN 0 ELSE ac.pfemale END) AS pfemale, "
                        + "SUM(CASE WHEN ac.ptotal IS NULL THEN 0 ELSE ac.ptotal END) AS ptotal, "
                        + "SUM(CASE WHEN ac.nmale IS NULL THEN 0 ELSE ac.nmale END) AS nmale, "
                        + "SUM(CASE WHEN ac.nfemale IS NULL THEN 0 ELSE ac.nfemale END) AS nfemale, "
                        + "SUM(CASE WHEN ac.ntotal IS NULL THEN 0 ELSE ac.ntotal END) AS ntotal, "
                        + "SUM(CASE WHEN ac.totalmale IS NULL THEN 0 ELSE ac.totalmale END) AS totalmale, "
                        + "SUM(CASE WHEN ac.totalfemale IS NULL THEN 0 ELSE ac.totalfemale END) AS totalfemale, "
                        + "SUM(CASE WHEN ac.total IS NULL THEN 0 ELSE ac.total END) AS total "
                    + "FROM sys_reportcharts AS rc "
                    + "LEFT OUTER JOIN view_ctc_attendancecount AS ac ON "
                    + "ac.patientage>=rc.value1 AND ac.patientage<rc.value2 "
                    + "AND transdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + " "
                    + "AND visittype='RE-ATTENDANCE' "
                    + "WHERE reportcode='REP" + Convert.ToInt16(AfyaPro_Types.clsEnums.BuiltInReports.CTCAttendanceCountAge) + "' "
                    + "GROUP BY "
                        + "rc.value1, "
                        + "rc.value2, "
                        + "rc.description ";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSummary2);

                #endregion

                #endregion

                DataSet mDsData = new DataSet("summary");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtSummary);
                mDsData.Tables.Add(mDtSummary2);
                mDsData.Relations.Add("childrelationship", mDtFacilitySetup.Columns["keyvalue"], mDtSummary.Columns["keyvalue"]);
                mDsData.Relations.Add("childrelationship2", mDtFacilitySetup.Columns["keyvalue"], mDtSummary2.Columns["keyvalue"]);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #region CTC_AttendanceCD4AgeT
        public DataSet CTC_AttendanceCD4AgeT(DateTime mDateFrom, DateTime mDateTo, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "CTC_AttendanceCD4AgeT";

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
                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtSummary = new DataTable("summary");

                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_datefrom", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_dateto", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));
                mDtFacilitySetup.Columns.Add("keyvalue", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_datefrom"] = mDateFrom;
                    mDataRow["para_dateto"] = mDateTo;
                    mDataRow["para_otherparameters"] = mExtraParameters;
                    mDataRow["keyvalue"] = "parentlink";

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region generate data

                mCommand.CommandText = ""
                    + "SELECT "
                        + "'parentlink' AS keyvalue, "
                        + "1 AS groupindex, "
                        + "rc.value1, "
                        + "rc.value2, "
                        + "rc.description, "
                        + "SUM(CASE WHEN ac.pmale IS NULL THEN 0 ELSE ac.pmale END) AS pmale, "
                        + "SUM(CASE WHEN ac.pfemale IS NULL THEN 0 ELSE ac.pfemale END) AS pfemale, "
                        + "SUM(CASE WHEN ac.ptotal IS NULL THEN 0 ELSE ac.ptotal END) AS ptotal, "
                        + "SUM(CASE WHEN ac.nmale IS NULL THEN 0 ELSE ac.nmale END) AS nmale, "
                        + "SUM(CASE WHEN ac.nfemale IS NULL THEN 0 ELSE ac.nfemale END) AS nfemale, "
                        + "SUM(CASE WHEN ac.ntotal IS NULL THEN 0 ELSE ac.ntotal END) AS ntotal, "
                        + "SUM(CASE WHEN ac.totalmale IS NULL THEN 0 ELSE ac.totalmale END) AS totalmale, "
                        + "SUM(CASE WHEN ac.totalfemale IS NULL THEN 0 ELSE ac.totalfemale END) AS totalfemale, "
                        + "SUM(CASE WHEN ac.total IS NULL THEN 0 ELSE ac.total END) AS total "
                    + "FROM sys_reportcharts AS rc "
                    + "LEFT OUTER JOIN view_ctc_attendancecd4t AS ac ON "
                    + "ac.patientage>=rc.value1 AND ac.patientage<rc.value2 "
                    + "AND transdate BETWEEN " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + " "
                    + "WHERE reportcode='REP" + Convert.ToInt16(AfyaPro_Types.clsEnums.BuiltInReports.CTCAttendanceCD4T) + "' "
                    + "GROUP BY "
                        + "rc.value1, "
                        + "rc.value2, "
                        + "rc.description ";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSummary);

                #endregion

                DataSet mDsData = new DataSet("summary");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtSummary);
                mDsData.Relations.Add("childrelationship", mDtFacilitySetup.Columns["keyvalue"], mDtSummary.Columns["keyvalue"]);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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


        #region CTC_PMTCTCareMonthlySummary
        public DataSet CTC_PMTCTCareMonthlySummary(int mYearPart, int mMonthPart, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "CTC_PMTCTCareMonthlySummary";

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
                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtSummary = new DataTable("summary");

                #region facilitysetup

                mCommand.CommandText = "select * from view_facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_year", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_month", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));
                mDtFacilitySetup.Columns.Add("keyvalue", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_year"] = mYearPart.ToString();
                    mDataRow["para_month"] = clsGlobal.Get_MonthName(mMonthPart);
                    mDataRow["para_otherparameters"] = mExtraParameters;
                    mDataRow["keyvalue"] = "parentlink";

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region generate data

                mCommand.CommandText = "SELECT "
                    + "'parentlink' AS keyvalue, "
                    + "YEAR(v.startdate) AS yearpart, "
                    + "MONTH(v.startdate) AS monthpart, "
                    + "SUM(CASE WHEN bl.registrystatus='new' THEN 1 ELSE 0 END) AS new_clients, "
                    + "SUM(CASE WHEN v.knownhivtestresult=1 THEN 1 ELSE 0 END) AS known_hiv_pos, "
                    + "SUM(CASE WHEN YEAR(v.hivtestdate)=" + mYearPart + " AND MONTH(v.hivtestdate)=" + mMonthPart + " AND bl.registrystatus='new' THEN 1 ELSE 0 END) AS new_tested, "
                    + "SUM(CASE WHEN YEAR(v.hivtestdate)=" + mYearPart + " AND MONTH(v.hivtestdate)=" + mMonthPart + " THEN 1 ELSE 0 END) AS total_tested, "
                    + "SUM(CASE WHEN YEAR(v.hivtestdate)=" + mYearPart + " AND MONTH(v.hivtestdate)=" + mMonthPart + " AND v.hivtestresult=1 THEN 1 ELSE 0 END) AS tested_pos, "
                    + "SUM(CASE WHEN YEAR(v.postcounsellingdate)=" + mYearPart + " AND MONTH(v.postcounsellingdate)=" + mMonthPart + " THEN 1 ELSE 0 END) AS post_counselled, "
                    + "SUM(CASE WHEN YEAR(v.partnerhivtestdate)=" + mYearPart + " AND MONTH(v.partnerhivtestdate)=" + mMonthPart + " THEN 1 ELSE 0 END) AS partner_tested, "
                    + "SUM(CASE WHEN YEAR(v.partnerhivtestdate)=" + mYearPart + " AND MONTH(v.partnerhivtestdate)=" + mMonthPart + " AND v.partnerhivtestresult=1 THEN 1 ELSE 0 END) AS partner_tested_pos "
                + "FROM ctc_pmtct v "
                + "LEFT OUTER JOIN ctc_bookinglog bl ON v.patientcode=bl.patientcode AND v.booking=bl.booking "
                + "WHERE YEAR(v.startdate)=" + mYearPart + " AND MONTH(v.startdate)=" + mMonthPart + " "
                + "GROUP BY "
                    + "YEAR(v.startdate), "
                    + "MONTH(v.startdate) ";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSummary);

                #endregion

                DataSet mDsData = new DataSet("summary");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtSummary);
                mDsData.Relations.Add("childrelationship", mDtFacilitySetup.Columns["keyvalue"], mDtSummary.Columns["keyvalue"]);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #region CTC_TestingAndCounselingMonthlySummary
        public DataSet CTC_TestingAndCounselingMonthlySummary(int mYearPart, int mFromMonthPart,  int mToMonthPart,string mExtraParameters)
        {
            string mFunctionName = "CTC_TestingAndCounselingMonthlySummary";

            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            int mHivTestSummaryRows = 0;
            string mExtraFilter = "";
            string mSelect = "";

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
                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtSummary = new DataTable("summary");

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_year", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_frommonth", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_tomonth", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));
                mDtFacilitySetup.Columns.Add("keyvalue", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_year"] = mYearPart.ToString();
                    mDataRow["para_frommonth"] = clsGlobal.Get_MonthName(mFromMonthPart);
                    mDataRow["para_tomonth"] = clsGlobal.Get_MonthName(mToMonthPart);
                    mDataRow["para_otherparameters"] = mExtraParameters;
                    mDataRow["keyvalue"] = "parentlink";

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region generate data

                #region HIV Test summary

                mSelect = "SELECT "
                    + "'parentlink' AS keyvalue, "
                    + "ch.* FROM view_ctc_hivtests_summary ch ";

                //Populate filter string
                mExtraFilter = " ch.trans_year = " + mYearPart + " AND ch.trans_month >= "
                                    + mFromMonthPart + " AND ch.trans_month <=" + mToMonthPart;
                
                mCommand.CommandText = mSelect + "WHERE" + mExtraFilter;
                
                mDataAdapter.SelectCommand = mCommand;
                mHivTestSummaryRows = mDataAdapter.Fill(mDtSummary);

                #endregion

               
                #endregion

                //Poupulate 0 if no rows are returned from database
                if (mHivTestSummaryRows == 0)
                    mDtSummary.Rows.Add("parentlink",0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0);                   
                                            
                DataSet mDsData = new DataSet("summary");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtSummary);
                mDsData.Relations.Add("childrelationship", mDtFacilitySetup.Columns["keyvalue"], mDtSummary.Columns["keyvalue"]);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #region CTC_ExposedChildrenMonthlyReport
        public DataSet CTC_ExposedChildrenMonthlyReport(int mYearPart, int mFromMonthPart, int mToMonthPart, string mExtraParameters)
        {
            string mFunctionName = "CTC_ExposedChildrenMonthlyReport";

            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            int mExposedChildrenSummaryRows = 0;
            string mSelect = "";
            string mExtraFilter = "";

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
                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtSummary = new DataTable("summary");

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_year", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_frommonth", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_tomonth", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));
                mDtFacilitySetup.Columns.Add("keyvalue", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_year"] = mYearPart.ToString();
                    mDataRow["para_frommonth"] = clsGlobal.Get_MonthName(mFromMonthPart);
                    mDataRow["para_tomonth"] = clsGlobal.Get_MonthName(mToMonthPart);
                    mDataRow["para_otherparameters"] = mExtraParameters;
                    mDataRow["keyvalue"] = "parentlink";

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region generate data

                #region Exposed Children
                
                mSelect = "SELECT "
                  + "'parentlink' AS keyvalue, "
                  + "ex.* FROM view_ctc_exposedchildren_summary ex ";

                //Populate filter string
                mExtraFilter = " ex.trans_year = " + mYearPart + 
                    " AND ex.trans_month >= " + mFromMonthPart + " AND ex.trans_month <=" + mToMonthPart;
              
                mCommand.CommandText = mSelect + "WHERE" + mExtraFilter;

                mDataAdapter.SelectCommand = mCommand;
                mExposedChildrenSummaryRows = mDataAdapter.Fill(mDtSummary);          

                #endregion
               
                #endregion

                //Poupulate 0 if no rows are returned from database
                if (mExposedChildrenSummaryRows == 0)
                    mDtSummary.Rows.Add("parentlink", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

                DataSet mDsData = new DataSet("summary");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtSummary);
                mDsData.Relations.Add("childrelationship", mDtFacilitySetup.Columns["keyvalue"], mDtSummary.Columns["keyvalue"]);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
                 
              
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
        
        #region CTC_DNAPCRMonthlyReport
        public DataSet CTC_DNAPCRMonthlyReport(int mYearPart, int mFromMonthPart, int mToMonthPart, string mExtraParameters)
        {
            string mFunctionName = "CTC_DNAPCRMonthlyReport";

            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            int mPCRSummaryRows = 0;
            string mSelect = "";
            string mExtraFilter = "";
           
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
                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtDNAPCR = new DataTable("dnapcr");
              
                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_year", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_frommonth", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_tomonth", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));
                mDtFacilitySetup.Columns.Add("keyvalue", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_year"] = mYearPart.ToString();
                    mDataRow["para_frommonth"] = clsGlobal.Get_MonthName(mFromMonthPart);
                    mDataRow["para_tomonth"] = clsGlobal.Get_MonthName(mToMonthPart);
                    mDataRow["para_otherparameters"] = mExtraParameters;
                    mDataRow["keyvalue"] = "parentlink";

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region generate data

                #region DNA PCR Monthly Report
                mSelect = "SELECT "
                  + "'parentlink' AS keyvalue, "
                  + "pcr.* FROM view_ctc_dnapcrtests_summary pcr ";

                //Populate filter string
                mExtraFilter = " pcr.trans_year = " + mYearPart +
                    " AND pcr.trans_month >= " + mFromMonthPart + " AND pcr.trans_month <=" + mToMonthPart;

                mCommand.CommandText = mSelect + "WHERE" + mExtraFilter;

                mDataAdapter.SelectCommand = mCommand;
                mPCRSummaryRows = mDataAdapter.Fill(mDtDNAPCR);

                //Poupulate 0 if no rows are returned from database
                if (mPCRSummaryRows == 0)
                    mDtDNAPCR.Rows.Add("parentlink", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);                   
                
                #endregion

                #endregion

                DataSet mDsData = new DataSet("dnapcr");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtDNAPCR);
                mDsData.Relations.Add("childrelationship", mDtFacilitySetup.Columns["keyvalue"], mDtDNAPCR.Columns["keyvalue"]);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #region CTC_ARTCLinicQuarterlySupervisionForm
        public DataSet CTC_ARTCLinicQuarterlySupervisionForm(int mYearPart, string mQuarter, string mFromDate, string mToDate, DateTime mQuarterStartDate, DateTime mQuarterEndDate, string mFirstMonthInQuarter, string mSecondMonthInQuarter, string mThirdMonthInQuarter, string mFirstMonth, string mSecondMonth, string mThirdMonth)
        {
            string mFunctionName = "CTC_ARTCLinicQuarterlySupervisionForm";
           
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcCommand mFirstPatientOnARTCommand = new OdbcCommand();
            OdbcCommand mCohortSurvivalAnalysisCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

                  

            int startMonth = 0;
            int endMonth = 0;
       
            #region database connection

            try
            {
                mConn.ConnectionString = clsGlobal.gAfyaConStr;

                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                mCommand.Connection = mConn;
                mFirstPatientOnARTCommand.Connection = mConn;
             
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }

            #endregion

            try
            {
                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtClinicStaff = new DataTable("ctc_contactablestaff");
                DataTable mDtFistPatientOnART = new DataTable("ctc_FirstPatientOnART");
                DataTable mDtCohortSurvivalAnalysis = new DataTable("ctc_CohortSurvivalAnalysis");
                DataTable mDtHIVCareClinic = new DataTable("ctc_HIVCareClinic");
                DataTable mDtExposedChildUnder24Months = new DataTable("ctc_ExposedChildReportingMonth1");
                DataTable mDtExposedChildUnder24MonthsM2 = new DataTable("ctc_ExposedChildReportingMonth2");
                DataTable mDtExposedChildUnder24MonthsM3 = new DataTable("ctc_ExposedChildReportingMonth3");
                DataTable mDtARTClinic = new DataTable("ctc_ARTClinic");
                DataTable mDtPrimaryOutcomesEndOfQuarter = new DataTable("ctc_PrimaryOutcomesEndOfQuarterEvaluated");
              
              
                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_year", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_quarter", typeof(System.String));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));
                mDtFacilitySetup.Columns.Add("keyvalue", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_year"] = mYearPart.ToString();
                    mDataRow["para_quarter"] = mQuarter;
                   // mDataRow["para_otherparameters"] = mExtraParameters;
                    mDataRow["keyvalue"] = "parentlink";

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region Quarterly supervision report data

                #region ContactableClinicStaff
                mFunctionName = "ContactableClinicStaff";
                    
                    mCommand.CommandText = "SELECT * FROM ctc_contactablestaff";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtClinicStaff);
                    mDtClinicStaff.Columns.Add("keyvalue", typeof(System.String));
                    DataRow mRow = mDtClinicStaff.Rows[0];
                   mRow["keyvalue"] = "parentlink";

                   mRow.EndEdit();
                    mDtClinicStaff.AcceptChanges();
                #endregion

                #region First ART Patient Registered

                    mFunctionName = "First ART Patient Registered";        
                    mFirstPatientOnARTCommand.CommandText = "SELECT Min(transferindate) AS TransferIndate FROM ctc_art";
                    mDataAdapter.SelectCommand = mFirstPatientOnARTCommand;
                    mDataAdapter.Fill(mDtFistPatientOnART);
                    mDtFistPatientOnART.Columns.Add("keyvalue", typeof(System.String));
                    //DataRow mRow1 = mDtClinicStaff.Rows[0];
                    //mRow["keyvalue"] = "parentlink";

                    //mRow.EndEdit();
                    mDtFistPatientOnART.AcceptChanges();
                    #endregion

                #region CohortSurvivalAnanlysis
                    mFunctionName = "Cohort survival analysis";

                    #region Variable Declaration

                    int index = 0;
                    int mMonthsInterval, mTotalRegistered, mAlive, mDied, mDefaulted, mStopped, mTransferOut, mChildren, mCurrentYear;

                    mCurrentYear = mYearPart;
                    string mAgeGroup, mFirstYear, mCohort;
                    mMonthsInterval = 0;
                    mTotalRegistered = 0;
                    mMonthsInterval = 0;
                    mAgeGroup = "All Ages";
                    mAlive = 0;
                    mDied = 0;
                    mDefaulted = 0;
                    mStopped = 0;
                    mTransferOut = 0;
                    mChildren = 0;
                    mCohort = "";


                  
                    mCurrentYear = Convert.ToInt32(mCurrentYear - 1);
                    int mYear = mCurrentYear;
                    mFirstYear = mCurrentYear + " " + mQuarter;

                    #endregion

                  
                    DataTable mTransPatients = new DataTable();

                    mDtCohortSurvivalAnalysis.Columns.Add("keyvalue", typeof(System.String));
                    mDtCohortSurvivalAnalysis.Columns.Add("Cohort", typeof(System.String));
                    mDtCohortSurvivalAnalysis.Columns.Add("IntervalMonths", typeof(System.String));
                    mDtCohortSurvivalAnalysis.Columns.Add("AgeGroup", typeof(System.String));
                    mDtCohortSurvivalAnalysis.Columns.Add("TotalReg", typeof(System.String));
                    mDtCohortSurvivalAnalysis.Columns.Add("Alive", typeof(System.String));
                    mDtCohortSurvivalAnalysis.Columns.Add("Died", typeof(System.String));
                    mDtCohortSurvivalAnalysis.Columns.Add("Defaulted", typeof(System.String));
                    mDtCohortSurvivalAnalysis.Columns.Add("Stopped", typeof(System.String));
                    mDtCohortSurvivalAnalysis.Columns.Add("TransferOut", typeof(System.String));

                                  
               
                    for (index = 0; index <= 8; index++)
                    {

                        OdbcCommand mTransferInARTPatients = new OdbcCommand();
                        OdbcCommand mOtherARTPatients = new OdbcCommand();

                        OdbcCommand mTransferInARTPatientsARTP = new OdbcCommand();
                        OdbcCommand mOtherARTPatientsARTP = new OdbcCommand();

                        DataTable  mDtARTPPatients = new DataTable();
                         
                        mTransferInARTPatients.Connection = mConn;
                        mOtherARTPatients.Connection = mConn;

                        mTransferInARTPatientsARTP.Connection = mConn;
                        mOtherARTPatientsARTP.Connection = mConn;

                        mCohort = mCurrentYear + " " + mQuarter;

                        mOtherARTPatients.CommandText = "select * from ctc_art where artinitiationdate between '" + mCurrentYear + "/" + mFromDate + "' and '" + mCurrentYear + "/" + mToDate + "' AND arventrymode <> 'TI'";
               
                        mOtherARTPatients.ExecuteNonQuery();
                        

                        mTransferInARTPatients.CommandText = "select * from ctc_art where transferindate between '" + mCurrentYear + "/" + mFromDate + "' and '" + mCurrentYear + "/" + mToDate + "' AND arventrymode = 'TI'";

                        mTransferInARTPatients.ExecuteNonQuery();
                        mTransPatients.Clear();

                        mOtherARTPatientsARTP.CommandText = "select * from ctc_artp where artinitiationdate between '" + mCurrentYear + "/" + mFromDate + "' and '" + mCurrentYear + "/" + mToDate + "' AND arventrymode <> 'TI'";

                        mOtherARTPatientsARTP.ExecuteNonQuery();


                        mTransferInARTPatientsARTP.CommandText = "select * from ctc_artp where transferindate between '" + mCurrentYear + "/" + mFromDate + "' and '" + mCurrentYear + "/" + mToDate + "' AND arventrymode = 'TI'";

                        mTransferInARTPatientsARTP.ExecuteNonQuery();
                        mDtARTPPatients.Clear();
                      
                        OdbcDataAdapter mTransAdapter = new OdbcDataAdapter();
                        
                        mTransAdapter.SelectCommand = mOtherARTPatients;
                        mTransAdapter.Fill(mTransPatients);
                                               
                        OdbcDataAdapter mArtAdapter = new OdbcDataAdapter();
                        mArtAdapter.SelectCommand = mTransferInARTPatients;
                        mArtAdapter.Fill(mTransPatients);

                        OdbcDataAdapter mArtPAdapterOther = new OdbcDataAdapter();
                        mArtPAdapterOther.SelectCommand = mOtherARTPatientsARTP;
                        mArtPAdapterOther.Fill(mDtARTPPatients);

                        OdbcDataAdapter mArtPAdapterTI = new OdbcDataAdapter();
                        mArtPAdapterTI.SelectCommand = mTransferInARTPatientsARTP;
                        mArtPAdapterTI.Fill(mDtARTPPatients);

                        mAlive = 0;
                        mDied = 0;
                        mDefaulted = 0;
                        mStopped = 0;
                        mTransferOut = 0;

                         

                        foreach (DataRow myRow in mTransPatients.Rows)
                        {
                            String Outcome = myRow["latestoutcome"].ToString();
                            string mPatientCode = myRow["patientcode"].ToString();
                            DateTime mFromBeginning = Convert.ToDateTime("1900/01/01");
                            OdbcCommand cmdARTLog = new OdbcCommand();
                            cmdARTLog.Connection = mConn;
                            cmdARTLog.CommandText = "select * from ctc_artlog where patientcode ='" + mPatientCode + "' and transdate between '" + mFromBeginning + "' AND '" + mCurrentYear + "/" + mToDate + "'";
                            cmdARTLog.ExecuteNonQuery();

                            OdbcDataAdapter adARTLog = new OdbcDataAdapter();
                            DataTable tbARTLog = new DataTable();
                            
                            adARTLog.SelectCommand = cmdARTLog;
                            adARTLog.Fill(tbARTLog);

                            Int64 mAutoCode = 0;
                            if (tbARTLog.Rows.Count != 0)
                            {
                                foreach (DataRow myARTLogRow in tbARTLog.Rows)
                                {
                                    if (mAutoCode < Convert.ToInt64(myARTLogRow["autocode"]))
                                    {
                                        Outcome = Convert.ToString(myARTLogRow["outcomecode"]);
                                        mAutoCode = Convert.ToInt64(myARTLogRow["autocode"]);
                                    }
                                }
                            }

                           

                            if   (Outcome == "D")
                            {
                                mDied += 1;
                            }

                            if (Outcome == "TO")
                            {
                                mTransferOut += 1;
                            }

                            if (Outcome == "Def")
                            {
                             mDefaulted    += 1;
                            }

                            if (Outcome == "Alive")
                            {
                                mAlive += 1;
                            }

                            if (Outcome == "Stop")
                            {
                                mStopped += 1;
                            }
                        }

                        foreach (DataRow myRow in mDtARTPPatients.Rows)
                        {
                            String Outcome = myRow["latestoutcome"].ToString();

                            string mPatientCode = myRow["patientcode"].ToString();
                            DateTime mFromBeginning = Convert.ToDateTime("1900/01/01");
                            OdbcCommand cmdARTLog = new OdbcCommand();
                            cmdARTLog.Connection = mConn;
                            cmdARTLog.CommandText = "select * from ctc_artplog where patientcode ='" + mPatientCode + "' and transdate between '" + mFromBeginning + "' AND '" + mCurrentYear + "/" + mToDate + "'";
                            cmdARTLog.ExecuteNonQuery();

                            OdbcDataAdapter adARTLog = new OdbcDataAdapter();
                            DataTable tbARTLog = new DataTable();

                            adARTLog.SelectCommand = cmdARTLog;
                            adARTLog.Fill(tbARTLog);

                            Int64 mAutoCode = 0;
                            if (tbARTLog.Rows.Count != 0)
                            {
                                foreach (DataRow myARTLogRow in tbARTLog.Rows)
                                {
                                    if (mAutoCode < Convert.ToInt64(myARTLogRow["autocode"]))
                                    {
                                        Outcome = Convert.ToString(myARTLogRow["outcomecode"]);
                                        mAutoCode = Convert.ToInt64(myARTLogRow["autocode"]);
                                    }
                                }
                            }


                            if (Outcome == "D")
                            {
                                mDied += 1;
                            }

                            if (Outcome == "TO")
                            {
                                mTransferOut += 1;
                            }

                            if (Outcome == "Def")
                            {
                                mDefaulted += 1;
                            }

                            if (Outcome == "Alive")
                            {
                                mAlive += 1;
                            }

                            if (Outcome == "Stop")
                            {
                                mStopped += 1;
                            }
                        }

                        mMonthsInterval += 12;


                        mDtCohortSurvivalAnalysis.Rows.Add("parentlink", mCohort, mMonthsInterval, mAgeGroup, mTransPatients.Rows.Count + mDtARTPPatients.Rows.Count, mAlive, mDied, mDefaulted, mStopped, mTransferOut);

                        mDtCohortSurvivalAnalysis.AcceptChanges();

                       
                        
                        if (index == 0)
                        {
                            mAlive = 0;
                            mDied = 0;
                            mDefaulted = 0;
                            mStopped = 0;
                            mTransferOut = 0;
                            mTotalRegistered = 0;
                            foreach (DataRow myRow1 in mTransPatients.Rows)
                            {
                                String Outcome = myRow1["latestoutcome"].ToString();
                                string mPatientCode = myRow1["patientcode"].ToString();
                                DateTime mFromBeginning = Convert.ToDateTime("1900/01/01");
                                OdbcCommand cmdARTLog = new OdbcCommand();
                                cmdARTLog.Connection = mConn;
                                cmdARTLog.CommandText = "select * from ctc_artlog where patientcode ='" + mPatientCode + "' and transdate between '" + mFromBeginning + "' AND '" + mCurrentYear + "/" + mToDate + "'";
                                cmdARTLog.ExecuteNonQuery();

                                OdbcDataAdapter adARTLog = new OdbcDataAdapter();
                                DataTable tbARTLog = new DataTable();

                                adARTLog.SelectCommand = cmdARTLog;
                                adARTLog.Fill(tbARTLog);

                                Int64 mAutoCode = 0;
                                if (tbARTLog.Rows.Count != 0)
                                {
                                    foreach (DataRow myARTLogRow in tbARTLog.Rows)
                                    {
                                        if (mAutoCode < Convert.ToInt64(myARTLogRow["autocode"]))
                                        {
                                            Outcome = Convert.ToString(myARTLogRow["outcomecode"]);
                                            mAutoCode = Convert.ToInt64(myARTLogRow["autocode"]);
                                        }
                                    }
                                }

                                String mArvStartReason = myRow1["arvstartreason"].ToString();
                                if (mArvStartReason == "BF")
                                 {
                                    mTotalRegistered += 1;
                                    if (Outcome == "D")
                                    {
                                    mDied += 1;
                                    }

                                    if (Outcome == "TO")
                                    {
                                    mTransferOut += 1;
                                    }

                                    if (Outcome == "Def")
                                    {
                                    mDefaulted += 1;
                                    }

                                    if (Outcome == "Alive")
                                    {
                                    mAlive += 1;
                                    }

                                    if (Outcome == "Stop")
                                    {
                                    mStopped += 1;
                                    }
                                }

                                if (mArvStartReason == "Preg")
                                {
                                    mTotalRegistered += 1;
                                    if (Outcome == "D")
                                    {
                                        mDied += 1;
                                    }

                                    if (Outcome == "TO")
                                    {
                                        mTransferOut += 1;
                                    }

                                    if (Outcome == "Def")
                                    {
                                        mDefaulted += 1;
                                    }

                                    if (Outcome == "Alive")
                                    {
                                        mAlive += 1;
                                    }

                                    if (Outcome == "Stop")
                                    {
                                        mStopped += 1;
                                    }
                                }
                                                                
                            }


                            foreach (DataRow myRow1 in mDtARTPPatients.Rows)
                            {
                                String Outcome = myRow1["latestoutcome"].ToString();
                                String mArvStartReason = myRow1["arvstartreason"].ToString();

                                string mPatientCode = myRow1["patientcode"].ToString();
                                DateTime mFromBeginning = Convert.ToDateTime("1900/01/01");
                                OdbcCommand cmdARTLog = new OdbcCommand();
                                cmdARTLog.Connection = mConn;
                                cmdARTLog.CommandText = "select * from ctc_artplog where patientcode ='" + mPatientCode + "' and transdate between '" + mFromBeginning + "' AND '" + mCurrentYear + "/" + mToDate + "'";
                                cmdARTLog.ExecuteNonQuery();

                                OdbcDataAdapter adARTLog = new OdbcDataAdapter();
                                DataTable tbARTLog = new DataTable();

                                adARTLog.SelectCommand = cmdARTLog;
                                adARTLog.Fill(tbARTLog);

                                Int64 mAutoCode = 0;
                                if (tbARTLog.Rows.Count != 0)
                                {
                                    foreach (DataRow myARTLogRow in tbARTLog.Rows)
                                    {
                                        if (mAutoCode < Convert.ToInt64(myARTLogRow["autocode"]))
                                        {
                                            Outcome = Convert.ToString(myARTLogRow["outcomecode"]);
                                            mAutoCode = Convert.ToInt64(myARTLogRow["autocode"]);
                                        }
                                    }
                                }


                                if (mArvStartReason == "BF")
                                {
                                    mTotalRegistered += 1;
                                    if (Outcome == "D")
                                    {
                                        mDied += 1;
                                    }

                                    if (Outcome == "TO")
                                    {
                                        mTransferOut += 1;
                                    }

                                    if (Outcome == "Def")
                                    {
                                        mDefaulted += 1;
                                    }

                                    if (Outcome == "Alive")
                                    {
                                        mAlive += 1;
                                    }

                                    if (Outcome == "Stop")
                                    {
                                        mStopped += 1;
                                    }
                                }

                                if (mArvStartReason == "Preg")
                                {
                                    mTotalRegistered += 1;
                                    if (Outcome == "D")
                                    {
                                        mDied += 1;
                                    }

                                    if (Outcome == "TO")
                                    {
                                        mTransferOut += 1;
                                    }

                                    if (Outcome == "Def")
                                    {
                                        mDefaulted += 1;
                                    }

                                    if (Outcome == "Alive")
                                    {
                                        mAlive += 1;
                                    }

                                    if (Outcome == "Stop")
                                    {
                                        mStopped += 1;
                                    }
                                }

                            }

                            mDtCohortSurvivalAnalysis.Rows.Add("parentlink", mCohort, mMonthsInterval, "OptionB+", mTotalRegistered, mAlive, mDied, mDefaulted, mStopped, mTransferOut);

                            mDtCohortSurvivalAnalysis.AcceptChanges();

                            int mDaysInLastMonthOfQuarter = DateTime.DaysInMonth(Convert.ToDateTime(mQuarterEndDate).Year, Convert.ToDateTime(mQuarterEndDate).Month);
                           // clsGlobal.Write_Error(pClassName, mFunctionName, "Hallo yadutsapo");
                            DateTime mSixMonthsBefore = (Convert.ToDateTime(mQuarterEndDate).AddMonths(-6));

                            int mDaysInSisMonthsBefre = DateTime.DaysInMonth(mSixMonthsBefore.Year, mSixMonthsBefore.Month);

                            OdbcCommand mCmdSixMonthsTI = new OdbcCommand();
                            OdbcCommand mCmdSixMonthsOther = new OdbcCommand();

                            OdbcCommand mCmdSixMonthsTIARTP = new OdbcCommand();
                            OdbcCommand mCmdSixMonthsOtherARTP = new OdbcCommand();



                            mCmdSixMonthsTI.Connection = mConn;
                            mCmdSixMonthsTI.CommandText = "select * from ctc_art where transferindate between '" + Convert.ToDateTime(mQuarterStartDate).AddMonths(-6) + "' and '" + Convert.ToDateTime(Convert.ToDateTime(mQuarterEndDate).AddMonths(-6).Year + "/" + Convert.ToDateTime(mQuarterEndDate).AddMonths(-6).Month + "/" + mDaysInSisMonthsBefre) + "' AND arventrymode = 'TI'";
                            mCmdSixMonthsTI.ExecuteNonQuery();

                            mCmdSixMonthsOther.Connection = mConn;
                            mCmdSixMonthsOther.CommandText = "select * from ctc_art where artinitiationdate between '" + Convert.ToDateTime(mQuarterStartDate).AddMonths(-6) + "' and '" + Convert.ToDateTime(Convert.ToDateTime(mQuarterEndDate).AddMonths(-6).Year + "/" + Convert.ToDateTime(mQuarterEndDate).AddMonths(-6).Month + "/" + mDaysInSisMonthsBefre) + "' AND arventrymode <> 'TI'";
                            mCmdSixMonthsOther.ExecuteNonQuery();

                            mCmdSixMonthsTIARTP.Connection = mConn;
                            mCmdSixMonthsTIARTP.CommandText = "select * from ctc_artp where transferindate between '" + Convert.ToDateTime(mQuarterStartDate).AddMonths(-6) + "' and '" + Convert.ToDateTime(Convert.ToDateTime(mQuarterEndDate).AddMonths(-6).Year + "/" + Convert.ToDateTime(mQuarterEndDate).AddMonths(-6).Month + "/" + mDaysInSisMonthsBefre) + "' AND arventrymode = 'TI'";
                            mCmdSixMonthsTIARTP.ExecuteNonQuery();

                            mCmdSixMonthsOtherARTP.Connection = mConn;
                            mCmdSixMonthsOtherARTP.CommandText = "select * from ctc_artp where artinitiationdate between '" + Convert.ToDateTime(mQuarterStartDate).AddMonths(-6) + "' and '" + Convert.ToDateTime(Convert.ToDateTime(mQuarterEndDate).AddMonths(-6).Year + "/" + Convert.ToDateTime(mQuarterEndDate).AddMonths(-6).Month + "/" + mDaysInSisMonthsBefre) + "' AND arventrymode <> 'TI'";
                            mCmdSixMonthsOtherARTP.ExecuteNonQuery();

                            mTransPatients.Clear();
                            mDtARTPPatients.Clear();

                            OdbcDataAdapter mSixMonths = new OdbcDataAdapter();
                            mTransAdapter.SelectCommand = mCmdSixMonthsOther;
                            mTransAdapter.Fill(mTransPatients);

                            mTransAdapter.SelectCommand = mCmdSixMonthsTI;
                            mTransAdapter.Fill(mTransPatients);

                            //OdbcDataAdapter mSixMonths = new OdbcDataAdapter();
                            mArtPAdapterOther.SelectCommand = mCmdSixMonthsOtherARTP;
                            mArtPAdapterOther.Fill(mDtARTPPatients);

                            mArtPAdapterTI.SelectCommand = mCmdSixMonthsTIARTP;
                            mArtPAdapterTI.Fill(mDtARTPPatients);

                            mAlive = 0;
                            mDied = 0;
                            mDefaulted = 0;
                            mStopped = 0;
                            mTransferOut = 0;
                            mTotalRegistered = 0;

                            foreach (DataRow myRow2 in mTransPatients.Rows)
                            {
                                String Outcome = myRow2["latestoutcome"].ToString();
                                String mArvStartReason = myRow2["arvstartreason"].ToString();

                                string mPatientCode = myRow2["patientcode"].ToString();
                                DateTime mFromBeginning = Convert.ToDateTime("1900/01/01");
                                OdbcCommand cmdARTLog = new OdbcCommand();
                                cmdARTLog.Connection = mConn;
                                cmdARTLog.CommandText = "select * from ctc_artlog where patientcode ='" + mPatientCode + "' and transdate between '" + mFromBeginning + "' AND '" + mCurrentYear + "/" + mToDate + "'";
                                cmdARTLog.ExecuteNonQuery();

                                OdbcDataAdapter adARTLog = new OdbcDataAdapter();
                                DataTable tbARTLog = new DataTable();

                                adARTLog.SelectCommand = cmdARTLog;
                                adARTLog.Fill(tbARTLog);

                                Int64 mAutoCode = 0;
                                if (tbARTLog.Rows.Count != 0)
                                {
                                    foreach (DataRow myARTLogRow in tbARTLog.Rows)
                                    {
                                        if (mAutoCode < Convert.ToInt64(myARTLogRow["autocode"]))
                                        {
                                            Outcome = Convert.ToString(myARTLogRow["outcomecode"]);
                                            mAutoCode = Convert.ToInt64(myARTLogRow["autocode"]);
                                        }
                                    }
                                }


                                if (mArvStartReason == "BF")
                                {
                                    mTotalRegistered += 1;
                                    if (Outcome == "D")
                                    {
                                        mDied += 1;
                                    }

                                    if (Outcome == "TO")
                                    {
                                        mTransferOut += 1;
                                    }

                                    if (Outcome == "Def")
                                    {
                                        mDefaulted += 1;
                                    }

                                    if (Outcome == "Alive")
                                    {
                                        mAlive += 1;
                                    }

                                    if (Outcome == "Stop")
                                    {
                                        mStopped += 1;
                                    }
                                }

                                if (mArvStartReason == "Preg")
                                {
                                    mTotalRegistered += 1;
                                    if (Outcome == "D")
                                    {
                                        mDied += 1;
                                    }

                                    if (Outcome == "TO")
                                    {
                                        mTransferOut += 1;
                                    }

                                    if (Outcome == "Def")
                                    {
                                        mDefaulted += 1;
                                    }

                                    if (Outcome == "Alive")
                                    {
                                        mAlive += 1;
                                    }

                                    if (Outcome == "Stop")
                                    {
                                        mStopped += 1;
                                    }
                                }


                            }

                            foreach (DataRow myRow2 in mDtARTPPatients.Rows)
                            {
                                String Outcome = myRow2["latestoutcome"].ToString();
                                String mArvStartReason = myRow2["arvstartreason"].ToString();
                                string mPatientCode = myRow2["patientcode"].ToString();
                                DateTime mFromBeginning = Convert.ToDateTime("1900/01/01");
                                OdbcCommand cmdARTLog = new OdbcCommand();
                                cmdARTLog.Connection = mConn;
                                cmdARTLog.CommandText = "select * from ctc_artplog where patientcode ='" + mPatientCode + "' and transdate between '" + mFromBeginning + "' AND '" + mCurrentYear + "/" + mToDate + "'";
                                cmdARTLog.ExecuteNonQuery();

                                OdbcDataAdapter adARTLog = new OdbcDataAdapter();
                                DataTable tbARTLog = new DataTable();

                                adARTLog.SelectCommand = cmdARTLog;
                                adARTLog.Fill(tbARTLog);

                                Int64 mAutoCode = 0;
                                if (tbARTLog.Rows.Count != 0)
                                {
                                    foreach (DataRow myARTLogRow in tbARTLog.Rows)
                                    {
                                        if (mAutoCode < Convert.ToInt64(myARTLogRow["autocode"]))
                                        {
                                            Outcome = Convert.ToString(myARTLogRow["outcomecode"]);
                                            mAutoCode = Convert.ToInt64(myARTLogRow["autocode"]);
                                        }
                                    }
                                }

                                if (mArvStartReason == "BF")
                                {
                                    mTotalRegistered += 1;
                                    if (Outcome == "D")
                                    {
                                        mDied += 1;
                                    }

                                    if (Outcome == "TO")
                                    {
                                        mTransferOut += 1;
                                    }

                                    if (Outcome == "Def")
                                    {
                                        mDefaulted += 1;
                                    }

                                    if (Outcome == "Alive")
                                    {
                                        mAlive += 1;
                                    }

                                    if (Outcome == "Stop")
                                    {
                                        mStopped += 1;
                                    }
                                }

                                if (mArvStartReason == "Preg")
                                {
                                    mTotalRegistered += 1;
                                    if (Outcome == "D")
                                    {
                                        mDied += 1;
                                    }

                                    if (Outcome == "TO")
                                    {
                                        mTransferOut += 1;
                                    }

                                    if (Outcome == "Def")
                                    {
                                        mDefaulted += 1;
                                    }

                                    if (Outcome == "Alive")
                                    {
                                        mAlive += 1;
                                    }

                                    if (Outcome == "Stop")
                                    {
                                        mStopped += 1;
                                    }
                                }


                            }

                            mCohort = "";
                            if (mQuarter == "Q1")
                            {
                                mCohort = Convert.ToDateTime(Convert.ToDateTime(mQuarterEndDate).AddMonths(-6).Year + "/" + Convert.ToDateTime(mQuarterEndDate).AddMonths(-6).Month + "/" + mDaysInSisMonthsBefre).Year + " Q3";
                               
                            }

                            if (mQuarter == "Q2")
                            {
                                mCohort = Convert.ToDateTime(Convert.ToDateTime(mQuarterEndDate).AddMonths(-6).Year + "/" + Convert.ToDateTime(mQuarterEndDate).AddMonths(-6).Month + "/" + mDaysInSisMonthsBefre).Year + " Q4";

                            }

                            if (mQuarter == "Q3")
                            {
                                mCohort = Convert.ToDateTime(Convert.ToDateTime(mQuarterEndDate).AddMonths(-6).Year + "/" + Convert.ToDateTime(mQuarterEndDate).AddMonths(-6).Month + "/" + mDaysInSisMonthsBefre).Year + " Q1";

                            }

                            if (mQuarter == "Q4")
                            {
                                mCohort = Convert.ToDateTime(Convert.ToDateTime(mQuarterEndDate).AddMonths(-6).Year + "/" + Convert.ToDateTime(mQuarterEndDate).AddMonths(-6).Month + "/" + mDaysInSisMonthsBefre).Year + " Q2";

                            }

                            mDtCohortSurvivalAnalysis.Rows.Add("parentlink", mCohort, 6, "OptionB+", mTotalRegistered, mAlive, mDied, mDefaulted, mStopped, mTransferOut);

                            mDtCohortSurvivalAnalysis.AcceptChanges();
                        }


                        if (index == 1)
                        {
                            mAlive = 0;
                            mDied = 0;
                            mDefaulted = 0;
                            mStopped = 0;
                            mTransferOut = 0;
                            mTotalRegistered = 0;
                            foreach (DataRow myRow1 in mTransPatients.Rows)
                            {
                                String Outcome = myRow1["latestoutcome"].ToString();
                                String mArvStartReason = myRow1["arvstartreason"].ToString();

                                string mPatientCode = myRow1["patientcode"].ToString();
                                DateTime mFromBeginning = Convert.ToDateTime("1900/01/01");
                                OdbcCommand cmdARTLog = new OdbcCommand();
                                cmdARTLog.Connection = mConn;
                                cmdARTLog.CommandText = "select * from ctc_artlog where patientcode ='" + mPatientCode + "' and transdate between '" + mFromBeginning + "' AND '" + mCurrentYear + "/" + mToDate + "'";
                                cmdARTLog.ExecuteNonQuery();

                                OdbcDataAdapter adARTLog = new OdbcDataAdapter();
                                DataTable tbARTLog = new DataTable();

                                adARTLog.SelectCommand = cmdARTLog;
                                adARTLog.Fill(tbARTLog);

                                Int64 mAutoCode = 0;
                                if (tbARTLog.Rows.Count != 0)
                                {
                                    foreach (DataRow myARTLogRow in tbARTLog.Rows)
                                    {
                                        if (mAutoCode < Convert.ToInt64(myARTLogRow["autocode"]))
                                        {
                                            Outcome = Convert.ToString(myARTLogRow["outcomecode"]);
                                            mAutoCode = Convert.ToInt64(myARTLogRow["autocode"]);
                                        }
                                    }
                                }


                                if (mArvStartReason == "BF")
                                {
                                    mTotalRegistered += 1;
                                    if (Outcome == "D")
                                    {
                                        mDied += 1;
                                    }

                                    if (Outcome == "TO")
                                    {
                                        mTransferOut += 1;
                                    }

                                    if (Outcome == "Def")
                                    {
                                        mDefaulted += 1;
                                    }

                                    if (Outcome == "Alive")
                                    {
                                        mAlive += 1;
                                    }

                                    if (Outcome == "Stop")
                                    {
                                        mStopped += 1;
                                    }
                                }

                                if (mArvStartReason == "Preg")
                                {
                                    mTotalRegistered += 1;
                                    if (Outcome == "D")
                                    {
                                        mDied += 1;
                                    }

                                    if (Outcome == "TO")
                                    {
                                        mTransferOut += 1;
                                    }

                                    if (Outcome == "Def")
                                    {
                                        mDefaulted += 1;
                                    }

                                    if (Outcome == "Alive")
                                    {
                                        mAlive += 1;
                                    }

                                    if (Outcome == "Stop")
                                    {
                                        mStopped += 1;
                                    }
                                }
                            }

                            foreach (DataRow myRow1 in mDtARTPPatients.Rows)
                            {
                                String Outcome = myRow1["latestoutcome"].ToString();
                                String mArvStartReason = myRow1["arvstartreason"].ToString();

                                string mPatientCode = myRow1["patientcode"].ToString();
                                DateTime mFromBeginning = Convert.ToDateTime("1900/01/01");
                                OdbcCommand cmdARTLog = new OdbcCommand();
                                cmdARTLog.Connection = mConn;
                                cmdARTLog.CommandText = "select * from ctc_artplog where patientcode ='" + mPatientCode + "' and transdate between '" + mFromBeginning + "' AND '" + mCurrentYear + "/" + mToDate + "'";
                                cmdARTLog.ExecuteNonQuery();

                                OdbcDataAdapter adARTLog = new OdbcDataAdapter();
                                DataTable tbARTLog = new DataTable();

                                adARTLog.SelectCommand = cmdARTLog;
                                adARTLog.Fill(tbARTLog);

                                Int64 mAutoCode = 0;
                                if (tbARTLog.Rows.Count != 0)
                                {
                                    foreach (DataRow myARTLogRow in tbARTLog.Rows)
                                    {
                                        if (mAutoCode < Convert.ToInt64(myARTLogRow["autocode"]))
                                        {
                                            Outcome = Convert.ToString(myARTLogRow["outcomecode"]);
                                            mAutoCode = Convert.ToInt64(myARTLogRow["autocode"]);
                                        }
                                    }
                                }


                                if (mArvStartReason == "BF")
                                {
                                    mTotalRegistered += 1;
                                    if (Outcome == "D")
                                    {
                                        mDied += 1;
                                    }

                                    if (Outcome == "TO")
                                    {
                                        mTransferOut += 1;
                                    }

                                    if (Outcome == "Def")
                                    {
                                        mDefaulted += 1;
                                    }

                                    if (Outcome == "Alive")
                                    {
                                        mAlive += 1;
                                    }

                                    if (Outcome == "Stop")
                                    {
                                        mStopped += 1;
                                    }
                                }

                                if (mArvStartReason == "Preg")
                                {
                                    mTotalRegistered += 1;
                                    if (Outcome == "D")
                                    {
                                        mDied += 1;
                                    }

                                    if (Outcome == "TO")
                                    {
                                        mTransferOut += 1;
                                    }

                                    if (Outcome == "Def")
                                    {
                                        mDefaulted += 1;
                                    }

                                    if (Outcome == "Alive")
                                    {
                                        mAlive += 1;
                                    }

                                    if (Outcome == "Stop")
                                    {
                                        mStopped += 1;
                                    }
                                }
                            }
                            mDtCohortSurvivalAnalysis.Rows.Add("parentlink", mCohort, mMonthsInterval, "OptionB+", mTotalRegistered, mAlive, mDied, mDefaulted, mStopped, mTransferOut);

                            mDtCohortSurvivalAnalysis.AcceptChanges();
                        }

                       

                        mCurrentYear = mCurrentYear - 1;
                    }

                    mTransPatients.Clear();
                    OdbcCommand mCmdARTPTI = new OdbcCommand();
                    OdbcCommand mCmdARTPOther = new OdbcCommand();

                    mCmdARTPOther.Connection = mConn;

                    mCmdARTPOther.CommandText = "select p.code, p.firstname, p.surname, p.birthdate, a.artinitiationdate, a.transferindate, a.latestoutcome, DATEDIFF('" + Convert.ToDateTime(mYear + "/" + mFromDate) + "', p.birthdate) AS AgeInDays FROM patients p, ctc_artp a WHERE p.code = a.patientcode AND a.artinitiationdate BETWEEN '" + mYear + "/" + mFromDate + "' AND '" + mYear + "/" + mToDate + "' AND a.arventrymode <> 'TI'"; 

                    mCmdARTPOther.ExecuteNonQuery();


                    mCmdARTPTI.Connection = mConn;

                    mCmdARTPTI.CommandText = "select p.code, p.firstname, p.surname, p.birthdate, a.artinitiationdate, a.transferindate, a.latestoutcome, DATEDIFF('" + Convert.ToDateTime(mYear + "/" + mFromDate) + "', p.birthdate) AS AgeInDays FROM patients p, ctc_artp a WHERE p.code = a.patientcode AND a.transferindate BETWEEN '" + mYear + "/" + mFromDate + "' AND '" + mYear + "/" + mToDate + "' AND a.arventrymode = 'TI'"; 

                    mCmdARTPTI.ExecuteNonQuery();

                    //clsGlobal.Write_Error(pClassName, mFunctionName, "a.artinitiationdate BETWEEN '" + Convert.ToDateTime(mQuarterStartDate) + "' AND '" + Convert.ToDateTime(mQuarterEndDate));
                   
                    OdbcDataAdapter mAdChildAdapter =new OdbcDataAdapter();
                    mAdChildAdapter.SelectCommand = mCmdARTPOther;
                    mAdChildAdapter.Fill(mTransPatients);

                
                    mAdChildAdapter.SelectCommand = mCmdARTPTI;
                    mAdChildAdapter.Fill(mTransPatients);

                            mAlive = 0;
                            mDied = 0;
                            mDefaulted = 0;
                            mStopped = 0;
                            mTransferOut = 0;
                            mTotalRegistered = 0;
                            foreach (DataRow myRow3 in mTransPatients.Rows)
                            {
                                String Outcome = myRow3["latestoutcome"].ToString();
                                TimeSpan Ts = (Convert.ToDateTime(mYear + "/" + mToDate) - Convert.ToDateTime(myRow3["birthdate"]));

                                string mPatientCode = myRow3["code"].ToString();
                                DateTime mFromBeginning = Convert.ToDateTime("1900/01/01");
                                OdbcCommand cmdARTLog = new OdbcCommand();
                                cmdARTLog.Connection = mConn;
                                cmdARTLog.CommandText = "select * from ctc_artplog where patientcode ='" + mPatientCode + "' and transdate between '" + mFromBeginning + "' AND '" + mYear + "/" + mToDate + "'";
                                cmdARTLog.ExecuteNonQuery();

                                OdbcDataAdapter adARTLog = new OdbcDataAdapter();
                                DataTable tbARTLog = new DataTable();

                                tbARTLog.Clear();
                                adARTLog.SelectCommand = cmdARTLog;
                                adARTLog.Fill(tbARTLog);

                                Int64 myAutoCode = 0;

                               // clsGlobal.Write_Error(pClassName, mFunctionName,  Outcome + " " + mPatientCode);
                                if (tbARTLog.Rows.Count != 0)
                                {
                                    foreach (DataRow myARTLogRow in tbARTLog.Rows)
                                    {
                                        if (myAutoCode < Convert.ToInt64(myARTLogRow["autocode"]))
                                        {
                                            Outcome = Convert.ToString(myARTLogRow["outcomecode"]);
                                            myAutoCode = Convert.ToInt64(myARTLogRow["autocode"]);
                                           
                                        }
                                    }
                                }


                                double mAgeInDays = Ts.Days;

                                if ((mAgeInDays < 5463.75))
                                    mTotalRegistered += 1;
                                {
                                    if (Outcome == "D")
                                    {
                                        mDied += 1;
                                    }

                                    if (Outcome == "TO")
                                    {
                                        mTransferOut += 1;
                                    }

                                    if (Outcome == "Def")
                                    {
                                        mDefaulted += 1;
                                    }

                                    if (Outcome == "Alive")
                                    {
                                        mAlive += 1;
                                    }

                                    if (Outcome == "Stop")
                                    {
                                        mStopped += 1;
                                    }

                                }
                            }
                            mDtCohortSurvivalAnalysis.Rows.Add("parentlink", mFirstYear, 12, "Children", mTotalRegistered, mAlive, mDied, mDefaulted, mStopped, mTransferOut);

                            mDtCohortSurvivalAnalysis.AcceptChanges();        

                #endregion

                #region HIV Care Clinic
                mFunctionName = "HIV Care Clinic";

                #region Variable declaration
                Int64 mTotalPatientsRegisteredInQuarter, mTotalPatientsRegisteredEver, mFTPatientsInQuarter, mFTPatientsEverRegistered, mREPatientsInQuarter, mREPatientsEverRegistered, mTIPatientsInQuarter, mTIPatientsEverRegistered, mMalePatientsInQuarter, mMalePatientsEverRegistered, mFNPPatientsInQuarter, mFNPPatientsEverRegistered, mFPPatientsInQuarter, mFPPatientsEverRegistered, mA1InfantsBelow2MonthsInQuarter, mA1InfantsBelow2MonthsInRegisteredEver, mA2Children2toBelow24MonthsInQuarter, mA2Children2toBelow24MonthsRegisteredEver, mBChildren24MonthsTo14YearsATEnrolmentInQuarter, mBChildren24MonthsTo14YearsATEnrolmentRegisteredEver, mAdults15YrsOrOlderAtEnrolmentInQuarter, mAdults15YrsOrOlderAtEnrolmentEverRegistered, mExposedInfantsInQuarter, mExposedInfantsEverRegistered, mConfirmedInfectedPatientsInQuarter, mConfirmedInfectedPatientsEverRegistered, mARTLess24Months;
                mFTPatientsEverRegistered = 0;
                mTotalPatientsRegisteredInQuarter = 0;
                mTIPatientsInQuarter = 0;
                mTotalPatientsRegisteredEver = 0;
                mFTPatientsInQuarter = 0;
                mREPatientsEverRegistered = 0;
                mTIPatientsEverRegistered = 0;
                mREPatientsInQuarter = 0;
                mMalePatientsInQuarter = 0;
                mMalePatientsEverRegistered = 0;
                mFNPPatientsInQuarter = 0;
                mFNPPatientsEverRegistered = 0;
                mFPPatientsInQuarter = 0;
                mFPPatientsEverRegistered = 0;
                mA1InfantsBelow2MonthsInQuarter = 0;
                mA1InfantsBelow2MonthsInRegisteredEver = 0;
                mA2Children2toBelow24MonthsInQuarter = 0;
                mA2Children2toBelow24MonthsRegisteredEver = 0;
                mBChildren24MonthsTo14YearsATEnrolmentInQuarter = 0;
                mBChildren24MonthsTo14YearsATEnrolmentRegisteredEver = 0;
                mAdults15YrsOrOlderAtEnrolmentInQuarter = 0;
                mAdults15YrsOrOlderAtEnrolmentEverRegistered = 0;
                mExposedInfantsInQuarter = 0;
                mExposedInfantsEverRegistered = 0;
                mExposedInfantsEverRegistered = 0;
                mConfirmedInfectedPatientsInQuarter = 0;
                mConfirmedInfectedPatientsEverRegistered = 0;
                mARTLess24Months =0;

                Int16 mTotalDefaulted =0, mTotalTransferOut =0, mTotalDied =0, mTotalStartedART =0;
                double mTotalRetainedAlive = 0;

                #endregion

                #region PreARTPatients

                mFunctionName = "HIV Care Clinic - Pre ART Patients";

                OdbcCommand mCmdHIVCareClinicQuarterOther = new OdbcCommand();
                OdbcCommand mCmdHIVCareClinicEverOther = new OdbcCommand();

                OdbcCommand mCmdHIVCareClinicQuarterTI = new OdbcCommand();
                OdbcCommand mCmdHIVCareClinicEverTI = new OdbcCommand();

                OdbcDataAdapter mAdpHIVCareClinicQuarter = new OdbcDataAdapter();
                OdbcDataAdapter mAdpHIVCareClinicEver = new OdbcDataAdapter();

                DataTable  mDsHIVCareClinicQuarter = new DataTable();
                DataTable mDsHIVCareClinicEver = new DataTable();

                mCmdHIVCareClinicQuarterOther.Connection = mConn;
                mCmdHIVCareClinicQuarterOther.CommandText = "Select a.patientcode, a.transferindate, a.dateofinitiation, a.confirmdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_preart a, patients p where a.patientcode = p.code and a.modeofentry <> 'TI' and a.dateofinitiation between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mYearPart + "/" + mFromDate)) + " AND " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                mCmdHIVCareClinicQuarterOther.ExecuteNonQuery();

                mCmdHIVCareClinicEverOther.Connection = mConn;
                mCmdHIVCareClinicEverOther.CommandText = "Select a.patientcode, a.transferindate, a.dateofinitiation, a.confirmdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_preart a, patients p where a.patientcode = p.code and a.modeofentry <> 'TI' and a.dateofinitiation between " + clsGlobal.Saving_DateValue(Convert.ToDateTime("1001/01/01")) + " AND " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                mCmdHIVCareClinicEverOther.ExecuteNonQuery();

                mAdpHIVCareClinicQuarter.SelectCommand = mCmdHIVCareClinicQuarterOther;
                mAdpHIVCareClinicQuarter.Fill(mDsHIVCareClinicQuarter);

                mAdpHIVCareClinicEver.SelectCommand = mCmdHIVCareClinicEverOther;
                mAdpHIVCareClinicEver.Fill(mDsHIVCareClinicEver);

                mCmdHIVCareClinicQuarterTI.Connection = mConn;
                mCmdHIVCareClinicQuarterTI.CommandText = "Select a.patientcode, a.transferindate, a.dateofinitiation, a.confirmdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_preart a, patients p where a.patientcode = p.code and a.modeofentry = 'TI' and a.transferindate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mYearPart + "/" + mFromDate)) + " AND " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                mCmdHIVCareClinicQuarterTI.ExecuteNonQuery();

                mCmdHIVCareClinicEverTI.Connection = mConn;
                mCmdHIVCareClinicEverTI.CommandText = "Select a.patientcode, a.transferindate, a.dateofinitiation, a.confirmdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_preart a, patients p where a.patientcode = p.code and a.modeofentry = 'TI' and a.transferindate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime("1001/01/01")) + " AND " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                mCmdHIVCareClinicEverTI.ExecuteNonQuery();

                mAdpHIVCareClinicQuarter.SelectCommand = mCmdHIVCareClinicQuarterTI;
                mAdpHIVCareClinicQuarter.Fill(mDsHIVCareClinicQuarter);

                mAdpHIVCareClinicEver.SelectCommand = mCmdHIVCareClinicEverTI;
                mAdpHIVCareClinicEver.Fill(mDsHIVCareClinicEver);

              //  clsGlobal.Write_Error(pClassName, mFunctionName, Convert.ToDateTime(mYearPart + "/" + mFromDate) + "' AND '" + Convert.ToDateTime(mQuarterEndDate));
                mTotalPatientsRegisteredInQuarter += mDsHIVCareClinicQuarter.Rows.Count;
                mTotalPatientsRegisteredEver += mDsHIVCareClinicEver.Rows.Count;

                foreach (DataRow myRow in mDsHIVCareClinicQuarter.Rows)
                {
                    
                    String Outcome = myRow["latestoutcome"].ToString();
                    String EntryMode = myRow["modeofentry"].ToString();
                    String Gender = myRow["gender"].ToString();
                    TimeSpan Ts = (Convert.ToDateTime(mQuarterEndDate) - Convert.ToDateTime(myRow["birthdate"]));
                    double AgeInDays = Ts.Days;
                    Int16 mPregnant = 0;

                    string mPatientCode = myRow["patientcode"].ToString();
                    DateTime mFromBeginning = Convert.ToDateTime("1900/01/01");
                    OdbcCommand cmdPreARTLog = new OdbcCommand();
                    cmdPreARTLog.Connection = mConn;
                    cmdPreARTLog.CommandText = "select * from ctc_preartlog where patientcode ='" + mPatientCode + "' and transdate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mYearPart + "/" + mFromDate)) + " AND " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                    cmdPreARTLog.ExecuteNonQuery();

                    OdbcDataAdapter adPreARTLog = new OdbcDataAdapter();
                    DataTable tbPreARTLog = new DataTable();

                    adPreARTLog.SelectCommand = cmdPreARTLog;
                    adPreARTLog.Fill(tbPreARTLog);

                    Int64 mAutoCode = 0;
                    if (tbPreARTLog.Rows.Count != 0)
                    {
                        foreach (DataRow myARTLogRow in tbPreARTLog.Rows)
                        {
                            if (mAutoCode < Convert.ToInt64(myARTLogRow["autocode"]))
                            {
                                Outcome = Convert.ToString(myARTLogRow["followupstatuscode"]);
                                mAutoCode = Convert.ToInt64(myARTLogRow["autocode"]);
                                mPregnant = Convert.ToInt16(myARTLogRow["pregnant"]);
                            }
                        }
                    }


                    if (EntryMode  == "FT")
                    {
                        mFTPatientsInQuarter += 1;
                    }

                    if (EntryMode == "TI")
                    {
                       mTIPatientsInQuarter  += 1;
                    }

                    if (EntryMode == "Re")
                    {
                        mREPatientsInQuarter  += 1;
                    }

                    if (Gender == "M")
                    {
                       mMalePatientsInQuarter += 1;
                    }

                    if ((Gender == "F") && (mPregnant == 0))
                    {
                        mFNPPatientsInQuarter += 1;
                    }

                    if ((Gender == "F") && (mPregnant != 0))
                    {
                        mFPPatientsInQuarter += 1;
                    }

                    if ((AgeInDays  < 58))
                    {
                      mA1InfantsBelow2MonthsInQuarter += 1;
                    }

                    if ((AgeInDays < 729))
                    {
                       mExposedInfantsInQuarter += 1;
                    }

                    if ((AgeInDays >= 58) && (AgeInDays < 729))
                    {
                       mA2Children2toBelow24MonthsInQuarter += 1;
                    }

                    if ((AgeInDays >= 729) && (AgeInDays < 5100))
                    {
                        mBChildren24MonthsTo14YearsATEnrolmentInQuarter += 1;
                    }

                    if ((AgeInDays >= 5479))
                    {
                       mAdults15YrsOrOlderAtEnrolmentInQuarter += 1;
                    }

                    if ((AgeInDays > 729))
                    {
                        mConfirmedInfectedPatientsInQuarter += 1;
                    }

                    if ((AgeInDays > 729))
                    {
                        if (Outcome == "ART")
                        {
                           mARTLess24Months   += 1;
                        }
                    }
                }

                 mExposedInfantsInQuarter = mA1InfantsBelow2MonthsInQuarter + mA2Children2toBelow24MonthsInQuarter;
                 mConfirmedInfectedPatientsInQuarter = mTotalPatientsRegisteredInQuarter - mExposedInfantsInQuarter;

                foreach (DataRow myRow in mDsHIVCareClinicEver.Rows)
                {
                   
                    String Outcome = myRow["latestoutcome"].ToString();
                    String EntryMode = myRow["modeofentry"].ToString();
                    String Gender = myRow["gender"].ToString();
                    TimeSpan Ts = (Convert.ToDateTime(mQuarterEndDate) - Convert.ToDateTime(myRow["birthdate"]));
                    double AgeInDays = Ts.Days;
                    Int16 mPregnant = 0;
                   

                    string mPatientCode = myRow["patientcode"].ToString();
                    DateTime mFromBeginning = Convert.ToDateTime("1900/01/01");
                    OdbcCommand cmdPreARTLog = new OdbcCommand();
                    cmdPreARTLog.Connection = mConn;
                    cmdPreARTLog.CommandText = "select * from ctc_preartlog where patientcode ='" + mPatientCode + "' and transdate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime("1900/01/01")) + " AND " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                    cmdPreARTLog.ExecuteNonQuery();

                    OdbcDataAdapter adPreARTLog = new OdbcDataAdapter();
                    DataTable tbPreARTLog = new DataTable();

                    adPreARTLog.SelectCommand = cmdPreARTLog;
                    adPreARTLog.Fill(tbPreARTLog);

                    Int64 mAutoCode = 0;
                    if (tbPreARTLog.Rows.Count != 0)
                    {
                        foreach (DataRow myARTLogRow in tbPreARTLog.Rows)
                        {
                            if (mAutoCode < Convert.ToInt64(myARTLogRow["autocode"]))
                            {
                                Outcome = Convert.ToString(myARTLogRow["followupstatuscode"]);
                                mAutoCode = Convert.ToInt64(myARTLogRow["autocode"]);
                                mPregnant = Convert.ToInt16(myARTLogRow["pregnant"]);
                            }
                        }
                    }

                    if ((myRow["modeofentry"].ToString() == "TI") && (Convert.ToDateTime(myRow["transferindate"]) < mQuarterStartDate) && (AgeInDays >= 729))
                    {
                        OdbcCommand cmdPreARTAppointment = new OdbcCommand();
                        cmdPreARTAppointment.Connection = mConn;
                        cmdPreARTAppointment.CommandText = "select * from ctc_appointments where patientcode ='" + mPatientCode + "' and apptdate >= " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                        cmdPreARTAppointment.ExecuteNonQuery();

                        OdbcDataAdapter adPreARTAppointment = new OdbcDataAdapter();
                        DataTable tbPreARTAppointment = new DataTable();

                        adPreARTAppointment.SelectCommand = cmdPreARTAppointment;
                        adPreARTAppointment.Fill(tbPreARTAppointment);

                        if ((tbPreARTAppointment.Rows.Count == 0) && (Outcome == "Con") || (Outcome == "Def"))
                        {
                            mTotalDefaulted += 1;
                        }

                       
                        DateTime mAppD;
                        Double mDtInDays = 0;
                       
                        if ((tbPreARTAppointment.Rows.Count > 0))
                        {
                            foreach (DataRow myARTAppointmentRow in tbPreARTAppointment.Rows)
                            {
                                mAppD = Convert.ToDateTime(myARTAppointmentRow["apptdate"]);
                                TimeSpan mT = (mAppD - mQuarterEndDate);
                                mDtInDays = mT.Days;
                            }
                        }

                        if (mDtInDays > 60.83)
                        {
                            mTotalDefaulted += 1;
                        }

                       
                    }


                    if ((myRow["modeofentry"].ToString() != "TI") && (Convert.ToDateTime(myRow["dateofinitiation"]) < mQuarterStartDate) && (AgeInDays >= 729))
                    {
                        OdbcCommand cmdPreARTAppointment = new OdbcCommand();
                        cmdPreARTAppointment.Connection = mConn;
                        cmdPreARTAppointment.CommandText = "select * from ctc_appointments where patientcode ='" + mPatientCode + "' and apptdate >= " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                        cmdPreARTAppointment.ExecuteNonQuery();

                        OdbcDataAdapter adPreARTAppointment = new OdbcDataAdapter();
                        DataTable tbPreARTAppointment = new DataTable();

                        adPreARTAppointment.SelectCommand = cmdPreARTAppointment;
                        adPreARTAppointment.Fill(tbPreARTAppointment);

                        if ((tbPreARTAppointment.Rows.Count == 0) && (Outcome == "Con") || (Outcome == "Def"))
                        {
                            mTotalDefaulted += 1;
                        }


                        DateTime mAppD;
                        Double mDtInDays = 0;

                        if ((tbPreARTAppointment.Rows.Count > 0))
                        {
                            foreach (DataRow myARTAppointmentRow in tbPreARTAppointment.Rows)
                            {
                                mAppD = Convert.ToDateTime(myARTAppointmentRow["apptdate"]);
                                TimeSpan mT = (mAppD - mQuarterEndDate);
                                mDtInDays = mT.Days;
                            }
                        }

                        if (mDtInDays > 60.83)
                        {
                            mTotalDefaulted += 1;
                        }


                    }


                    if (Outcome == "D")
                    {
                        mTotalDied += 1;
                    }

                    if (Outcome == "ART")
                    {
                        mTotalStartedART += 1;
                    }

                    if (Outcome == "TO")
                    {
                        mTotalTransferOut += 1;
                    }

                    if (EntryMode == "FT")
                    {
                        mFTPatientsEverRegistered += 1;
                    }

                    if (EntryMode == "TI")
                    {
                        mTIPatientsEverRegistered += 1;
                    }

                    if (EntryMode == "Re")
                    {
                        mREPatientsEverRegistered += 1;
                    }

                    if (Gender == "M")
                    {
                        mMalePatientsEverRegistered += 1;
                    }

                    if ((Gender == "F") && (mPregnant == 0))
                    {
                        mFNPPatientsEverRegistered += 1;
                    }

                    if ((Gender == "F") && (mPregnant != 0))
                    {
                        mFPPatientsEverRegistered += 1;
                    }

                    if ((AgeInDays < 58))
                    {
                        mA1InfantsBelow2MonthsInRegisteredEver += 1;
                    }

                    if ((AgeInDays < 729))
                    {
                        mExposedInfantsEverRegistered += 1;
                    }

                    if ((AgeInDays >= 58) && (AgeInDays < 729))
                    {
                        mA2Children2toBelow24MonthsRegisteredEver += 1;
                    }

                    if ((AgeInDays >= 729) && (AgeInDays < 5100))
                    {
                        mBChildren24MonthsTo14YearsATEnrolmentRegisteredEver += 1;
                    }

                    if ((AgeInDays >= 5479))
                    {
                        mAdults15YrsOrOlderAtEnrolmentEverRegistered += 1;
                    }

                    if ((AgeInDays > 729))
                    {
                        mConfirmedInfectedPatientsEverRegistered += 1;
                    }

                    if ((AgeInDays > 729))
                    {
                        if (Outcome == "ART")
                        {
                            mARTLess24Months += 1;
                        }
                    }
                }

                #endregion

                #region ExposedChildrenUnder24Months

                mFunctionName = "HIV Care Clinic - Exposed Children under 24 Months";
                OdbcCommand mCmdHIVCareClinicExpChildrenInQuarterOther = new OdbcCommand();
                OdbcCommand mCmdHIVCareClinicExpChildrenInEverOther = new OdbcCommand();

                OdbcCommand mCmdHIVCareClinicExpChildrenInQuarterTI = new OdbcCommand();
                OdbcCommand mCmdHIVCareClinicExpChildrenInEverTI = new OdbcCommand();

                OdbcDataAdapter mAdpHIVCareClinicExpChildrenInQuarter = new OdbcDataAdapter();
                OdbcDataAdapter mAdpHIVCareClinicExpChildrenInEver = new OdbcDataAdapter();

                DataTable mDsHIVCareClinicExpChildrenInQuarter = new DataTable();
                DataTable mDsHIVCareClinicExpChildrenInEver = new DataTable();

                mCmdHIVCareClinicExpChildrenInQuarterOther.Connection = mConn;
                mCmdHIVCareClinicExpChildrenInQuarterOther.CommandText = "Select a.patientcode, a.transferindate, a.enrolmentdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_exposed a, patients p where a.patientcode = p.code and a.modeofentry <> 'TI' and a.enrolmentdate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mYearPart + "/" + mFromDate)) + " AND " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                mCmdHIVCareClinicExpChildrenInQuarterOther.ExecuteNonQuery();

                mCmdHIVCareClinicExpChildrenInEverOther.Connection = mConn;
                mCmdHIVCareClinicExpChildrenInEverOther.CommandText = "Select a.patientcode, a.transferindate, a.enrolmentdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_exposed a, patients p where a.patientcode = p.code and a.modeofentry <> 'TI' and a.enrolmentdate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime("1001/01/01")) + " AND " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                mCmdHIVCareClinicExpChildrenInEverOther.ExecuteNonQuery();

                mAdpHIVCareClinicExpChildrenInQuarter.SelectCommand = mCmdHIVCareClinicExpChildrenInQuarterOther;
                mAdpHIVCareClinicExpChildrenInQuarter.Fill(mDsHIVCareClinicExpChildrenInQuarter);

                mAdpHIVCareClinicExpChildrenInEver.SelectCommand = mCmdHIVCareClinicExpChildrenInEverOther;
                mAdpHIVCareClinicExpChildrenInEver.Fill(mDsHIVCareClinicExpChildrenInEver);


                mCmdHIVCareClinicExpChildrenInQuarterTI.Connection = mConn;
                mCmdHIVCareClinicExpChildrenInQuarterTI.CommandText = "Select a.patientcode, a.transferindate, a.enrolmentdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_exposed a, patients p where a.patientcode = p.code and a.modeofentry = 'TI' and a.transferindate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mYearPart + "/" + mFromDate)) + " AND " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                mCmdHIVCareClinicExpChildrenInQuarterTI.ExecuteNonQuery();

                mCmdHIVCareClinicExpChildrenInEverTI.Connection = mConn;
                mCmdHIVCareClinicExpChildrenInEverTI.CommandText = "Select a.patientcode, a.transferindate, a.enrolmentdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_exposed a, patients p where a.patientcode = p.code and a.modeofentry = 'TI' and a.transferindate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime("1001/01/01")) + " AND " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                mCmdHIVCareClinicExpChildrenInEverTI.ExecuteNonQuery();

                mAdpHIVCareClinicExpChildrenInQuarter.SelectCommand = mCmdHIVCareClinicExpChildrenInQuarterTI;
                mAdpHIVCareClinicExpChildrenInQuarter.Fill(mDsHIVCareClinicExpChildrenInQuarter);

                mAdpHIVCareClinicExpChildrenInEver.SelectCommand = mCmdHIVCareClinicExpChildrenInEverTI;
                mAdpHIVCareClinicExpChildrenInEver.Fill(mDsHIVCareClinicExpChildrenInEver);


                mTotalPatientsRegisteredInQuarter += mDsHIVCareClinicExpChildrenInQuarter.Rows.Count;
                mTotalPatientsRegisteredEver += mDsHIVCareClinicExpChildrenInEver.Rows.Count;

            

                foreach (DataRow myRow in mDsHIVCareClinicExpChildrenInQuarter.Rows)
                {
                   
                    String Outcome = myRow["latestoutcome"].ToString();
                    String EntryMode = myRow["modeofentry"].ToString();
                    String Gender = myRow["gender"].ToString();
                    TimeSpan Ts = (Convert.ToDateTime(mQuarterEndDate) - Convert.ToDateTime(myRow["birthdate"]));
                    double AgeInDays = Ts.Days;
                    Int16 mPregnant = 0;

                    string mPatientCode = myRow["patientcode"].ToString();
                    DateTime mFromBeginning = Convert.ToDateTime("1900/01/01");
                    OdbcCommand cmdPreARTExpChildrenLog = new OdbcCommand();
                    cmdPreARTExpChildrenLog.Connection = mConn;
                    cmdPreARTExpChildrenLog.CommandText = "select * from ctc_exposedlog where patientcode ='" + mPatientCode + "' and transdate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mYearPart + "/" + mFromDate)) + " AND " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                    cmdPreARTExpChildrenLog.ExecuteNonQuery();

                    OdbcDataAdapter adExpChildrenLog = new OdbcDataAdapter();
                    DataTable tbExpChildrenLog = new DataTable();

                    adExpChildrenLog.SelectCommand = cmdPreARTExpChildrenLog;
                    adExpChildrenLog.Fill(tbExpChildrenLog);

                    Int64 mAutoCode = 0;
                    if (tbExpChildrenLog.Rows.Count != 0)
                    {
                        foreach (DataRow myExpChildrenLogRow in tbExpChildrenLog.Rows)
                        {
                            if (mAutoCode < Convert.ToInt64(myExpChildrenLogRow["autocode"]))
                            {
                                Outcome = Convert.ToString(myExpChildrenLogRow["outcomecode"]);
                                mAutoCode = Convert.ToInt64(myExpChildrenLogRow["autocode"]);
                                
                            }
                        }
                    }


                    if (EntryMode == "FT")
                    {
                        mFTPatientsInQuarter += 1;
                    }

                    if (EntryMode == "TI")
                    {
                        mTIPatientsInQuarter += 1;
                    }

                    if (EntryMode == "Re")
                    {
                        mREPatientsInQuarter += 1;
                    }

                    if (Gender == "M")
                    {
                        mMalePatientsInQuarter += 1;
                    }

                    if ((Gender == "F") && (mPregnant == 0))
                    {
                        mFNPPatientsInQuarter += 1;
                    }

                    if ((Gender == "F") && (mPregnant != 0))
                    {
                        mFPPatientsInQuarter += 1;
                    }

                    if ((AgeInDays < 58))
                    {
                        mA1InfantsBelow2MonthsInQuarter += 1;
                    }

                    if ((AgeInDays < 729))
                    {
                        mExposedInfantsInQuarter += 1;
                    }

                    if ((AgeInDays >= 58) && (AgeInDays < 729))
                    {
                        mA2Children2toBelow24MonthsInQuarter += 1;
                    }

                    if ((AgeInDays >= 729) && (AgeInDays < 5100))
                    {
                        mBChildren24MonthsTo14YearsATEnrolmentInQuarter += 1;
                    }

                    if ((AgeInDays >= 5479))
                    {
                        mAdults15YrsOrOlderAtEnrolmentInQuarter += 1;
                    }

                    if ((AgeInDays > 729))
                    {
                        mConfirmedInfectedPatientsInQuarter += 1;
                    }

                    if ((AgeInDays > 729))
                    {
                        if (Outcome == "ART")
                        {
                            mARTLess24Months += 1;
                        }
                    }
                }

                mExposedInfantsInQuarter = mA1InfantsBelow2MonthsInQuarter + mA2Children2toBelow24MonthsInQuarter;
                mConfirmedInfectedPatientsInQuarter = mTotalPatientsRegisteredInQuarter - mExposedInfantsInQuarter;

                foreach (DataRow myRow in mDsHIVCareClinicExpChildrenInEver.Rows)
                {
                   
                    String Outcome = myRow["latestoutcome"].ToString();
                    String EntryMode = myRow["modeofentry"].ToString();
                    String Gender = myRow["gender"].ToString();
                    TimeSpan Ts = (Convert.ToDateTime(mQuarterEndDate) - Convert.ToDateTime(myRow["birthdate"]));
                    double AgeInDays = Ts.Days;
                    Int16 mPregnant = 0;


                    string mPatientCode = myRow["patientcode"].ToString();
                    DateTime mFromBeginning = Convert.ToDateTime("1900/01/01");
                    OdbcCommand cmdPreARTLog = new OdbcCommand();
                    cmdPreARTLog.Connection = mConn;
                    cmdPreARTLog.CommandText = "select * from ctc_exposedlog where patientcode ='" + mPatientCode + "' and transdate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime("1900/01/01")) + " AND " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                    cmdPreARTLog.ExecuteNonQuery();

                    OdbcDataAdapter adPreARTLog = new OdbcDataAdapter();
                    DataTable tbPreARTLog = new DataTable();

                    adPreARTLog.SelectCommand = cmdPreARTLog;
                    adPreARTLog.Fill(tbPreARTLog);

                    Int64 mAutoCode = 0;
                    if (tbPreARTLog.Rows.Count != 0)
                    {
                        foreach (DataRow myARTLogRow in tbPreARTLog.Rows)
                        {
                            if (mAutoCode < Convert.ToInt64(myARTLogRow["autocode"]))
                            {
                                Outcome = Convert.ToString(myARTLogRow["outcomecode"]);
                                mAutoCode = Convert.ToInt64(myARTLogRow["autocode"]);
                               // mPregnant = Convert.ToInt16(myARTLogRow["pregnant"]);
                            }
                        }
                    }

                    if ((myRow["modeofentry"].ToString() == "TI") && (Convert.ToDateTime(myRow["transferindate"]) < mQuarterStartDate) && (AgeInDays >= 729))
                    {
                        OdbcCommand cmdPreARTAppointment = new OdbcCommand();
                        cmdPreARTAppointment.Connection = mConn;
                        cmdPreARTAppointment.CommandText = "select * from ctc_appointments where patientcode ='" + mPatientCode + "' and apptdate >= " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                        cmdPreARTAppointment.ExecuteNonQuery();

                        OdbcDataAdapter adPreARTAppointment = new OdbcDataAdapter();
                        DataTable tbPreARTAppointment = new DataTable();

                        adPreARTAppointment.SelectCommand = cmdPreARTAppointment;
                        adPreARTAppointment.Fill(tbPreARTAppointment);

                        if ((tbPreARTAppointment.Rows.Count == 0) && (Outcome == "Con") || (Outcome == "Def"))
                        {
                            mTotalDefaulted += 1;
                        }


                        DateTime mAppD;
                        Double mDtInDays = 0;

                        if ((tbPreARTAppointment.Rows.Count > 0))
                        {
                            foreach (DataRow myARTAppointmentRow in tbPreARTAppointment.Rows)
                            {
                                mAppD = Convert.ToDateTime(myARTAppointmentRow["apptdate"]);
                                TimeSpan mT = (mAppD - mQuarterEndDate);
                                mDtInDays = mT.Days;
                            }
                        }

                        if (mDtInDays > 60.83)
                        {
                            mTotalDefaulted += 1;
                        }


                    }


                    if ((myRow["modeofentry"].ToString() != "TI") && (Convert.ToDateTime(myRow["enrolmentdate"]) < mQuarterStartDate) && (AgeInDays >= 729))
                    {
                        OdbcCommand cmdPreARTAppointment = new OdbcCommand();
                        cmdPreARTAppointment.Connection = mConn;
                        cmdPreARTAppointment.CommandText = "select * from ctc_appointments where patientcode ='" + mPatientCode + "' and apptdate >= " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                        cmdPreARTAppointment.ExecuteNonQuery();

                        OdbcDataAdapter adPreARTAppointment = new OdbcDataAdapter();
                        DataTable tbPreARTAppointment = new DataTable();

                        adPreARTAppointment.SelectCommand = cmdPreARTAppointment;
                        adPreARTAppointment.Fill(tbPreARTAppointment);

                        if ((tbPreARTAppointment.Rows.Count == 0) && (Outcome == "Con") || (Outcome == "Def"))
                        {
                            mTotalDefaulted += 1;
                        }


                        DateTime mAppD;
                        Double mDtInDays = 0;

                        if ((tbPreARTAppointment.Rows.Count > 0))
                        {
                            foreach (DataRow myARTAppointmentRow in tbPreARTAppointment.Rows)
                            {
                                mAppD = Convert.ToDateTime(myARTAppointmentRow["apptdate"]);
                                TimeSpan mT = (mAppD - mQuarterEndDate);
                                mDtInDays = mT.Days;
                            }
                        }

                        if (mDtInDays > 60.83)
                        {
                            mTotalDefaulted += 1;
                        }


                    }


                    if (Outcome == "D")
                    {
                        mTotalDied += 1;
                    }

                    if (Outcome == "ART")
                    {
                        mTotalStartedART += 1;
                    }

                    if (Outcome == "TO")
                    {
                        mTotalTransferOut += 1;
                    }

                    if (EntryMode == "FT")
                    {
                        mFTPatientsEverRegistered += 1;
                    }

                    if (EntryMode == "TI")
                    {
                        mTIPatientsEverRegistered += 1;
                    }

                    if (EntryMode == "Re")
                    {
                        mREPatientsEverRegistered += 1;
                    }

                    if (Gender == "M")
                    {
                        mMalePatientsEverRegistered += 1;
                    }

                    if ((Gender == "F") && (mPregnant == 0))
                    {
                        mFNPPatientsEverRegistered += 1;
                    }

                    if ((Gender == "F") && (mPregnant != 0))
                    {
                        mFPPatientsEverRegistered += 1;
                    }

                    if ((AgeInDays < 58))
                    {
                        mA1InfantsBelow2MonthsInRegisteredEver += 1;
                    }

                    if ((AgeInDays < 729))
                    {
                        mExposedInfantsEverRegistered += 1;
                    }

                    if ((AgeInDays >= 58) && (AgeInDays < 729))
                    {
                        mA2Children2toBelow24MonthsRegisteredEver += 1;
                    }

                    if ((AgeInDays >= 729) && (AgeInDays < 5100))
                    {
                        mBChildren24MonthsTo14YearsATEnrolmentRegisteredEver += 1;
                    }

                    if ((AgeInDays >= 5479))
                    {
                        mAdults15YrsOrOlderAtEnrolmentEverRegistered += 1;
                    }

                    if ((AgeInDays > 729))
                    {
                        mConfirmedInfectedPatientsEverRegistered += 1;
                    }

                    if ((AgeInDays > 729))
                    {
                        if (Outcome == "ART")
                        {
                            mARTLess24Months += 1;
                        }
                    }
                }

                #endregion

                mExposedInfantsEverRegistered = mA1InfantsBelow2MonthsInRegisteredEver + mA2Children2toBelow24MonthsRegisteredEver;
                mConfirmedInfectedPatientsEverRegistered = mTotalPatientsRegisteredEver - mExposedInfantsEverRegistered;
                mTotalRetainedAlive = mConfirmedInfectedPatientsEverRegistered - mTotalDefaulted - mTotalDied - mTotalTransferOut - mTotalStartedART;

                mDtHIVCareClinic.Columns.Add("keyvalue", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("TotalRegisteredInQuarter", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("TotalEverRegistered", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("FTPatientsInQuarter", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("FTPatientsEverRegistered", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("REPatientsInQuarter", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("REPatientsEverRegistered", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("TIPatientsInQuarter", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("TIPatientsEverRegistered", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("MalePatientsInQuarter", typeof(System.String));

                mDtHIVCareClinic.Columns.Add("MalePatientsEverRegistered", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("FNPPatientsInQuarter", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("FNPPatientsEverRegistered", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("FPPatientsInQuarter", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("FPPatientsEverRegistered", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("A1InfantsBelow2MonthsInQuarter", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("A1InfantsBelow2MonthsInRegisteredEver", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("A2Children2toBelow24MonthsInQuarter", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("A2Children2toBelow24MonthsRegisteredEver", typeof(System.String));

                mDtHIVCareClinic.Columns.Add("BChildren24MonthsTo14YearsATEnrolmentInQuarter", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("BChildren24MonthsTo14YearsATEnrolmentRegisteredEver", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("Adults15YrsOrOlderAtEnrolmentInQuarter", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("Adults15YrsOrOlderAtEnrolmentEverRegistered", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("ExposedInfantsInQuarter", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("ExposedInfantsEverRegistered", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("ConfirmedInfectedPatientsInQuarter", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("ConfirmedInfectedPatientsEverRegistered", typeof(System.String));

                mDtHIVCareClinic.Columns.Add("TotalRetainedAliveInPreART", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("TotalDefaulted", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("TotalDied", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("TotalTransferOut", typeof(System.String));
                mDtHIVCareClinic.Columns.Add("TotalStartedART", typeof(System.String));

                mDtHIVCareClinic.AcceptChanges();

                mDtHIVCareClinic.Rows.Add("parentlink", mTotalPatientsRegisteredInQuarter, mTotalPatientsRegisteredEver, mFTPatientsInQuarter, mFTPatientsEverRegistered, mREPatientsInQuarter, mREPatientsEverRegistered, mTIPatientsInQuarter, mTIPatientsEverRegistered, mMalePatientsInQuarter, mMalePatientsEverRegistered, mFNPPatientsInQuarter, mFNPPatientsEverRegistered, mFPPatientsInQuarter, mFPPatientsEverRegistered, mA1InfantsBelow2MonthsInQuarter, mA1InfantsBelow2MonthsInRegisteredEver, mA2Children2toBelow24MonthsInQuarter, mA2Children2toBelow24MonthsRegisteredEver, mBChildren24MonthsTo14YearsATEnrolmentInQuarter, mBChildren24MonthsTo14YearsATEnrolmentRegisteredEver, mAdults15YrsOrOlderAtEnrolmentInQuarter, mAdults15YrsOrOlderAtEnrolmentEverRegistered, mExposedInfantsInQuarter, mExposedInfantsEverRegistered, mConfirmedInfectedPatientsInQuarter, mConfirmedInfectedPatientsEverRegistered, mTotalRetainedAlive, mTotalDefaulted, mTotalDied, mTotalTransferOut, mTotalStartedART);

                mDtHIVCareClinic.AcceptChanges();

                mFTPatientsEverRegistered = 0;
                mTotalPatientsRegisteredInQuarter = 0;
                mTIPatientsInQuarter = 0;
                mTotalPatientsRegisteredEver = 0;
                mFTPatientsInQuarter = 0;
                mREPatientsEverRegistered = 0;
                mTIPatientsEverRegistered = 0;
                mREPatientsInQuarter = 0;
                mMalePatientsInQuarter = 0;
                mMalePatientsEverRegistered = 0;
                mFNPPatientsInQuarter = 0;
                mFNPPatientsEverRegistered = 0;
                mFPPatientsInQuarter = 0;
                mFPPatientsEverRegistered = 0;
                mA1InfantsBelow2MonthsInQuarter = 0;
                mA1InfantsBelow2MonthsInRegisteredEver = 0;
                mA2Children2toBelow24MonthsInQuarter = 0;
                mA2Children2toBelow24MonthsRegisteredEver = 0;
                mBChildren24MonthsTo14YearsATEnrolmentInQuarter = 0;
                mBChildren24MonthsTo14YearsATEnrolmentRegisteredEver = 0;
                mAdults15YrsOrOlderAtEnrolmentInQuarter = 0;
                mAdults15YrsOrOlderAtEnrolmentEverRegistered = 0;
                mExposedInfantsInQuarter = 0;
                mExposedInfantsEverRegistered = 0;
                mExposedInfantsEverRegistered = 0;
                mConfirmedInfectedPatientsInQuarter = 0;
                mConfirmedInfectedPatientsEverRegistered = 0;
                mARTLess24Months = 0;

                #endregion

                #region ExposedChildUnder24Months

                mFunctionName = "ExposedChildUnder24Months";

                OdbcDataAdapter mAdExposedChildUnder24MonthsTI = new OdbcDataAdapter();
                OdbcDataAdapter mAdExposedChildUnder24MonthsOther = new OdbcDataAdapter();
                DataTable  mDsExposedChild = new DataTable();

                string mTwoMonthsExposedFirstMonth, mTwoMonthsExposedSecondMonth, mTwoMonthsExposedThirdMonth, mTwelveMonthsExposedFirstMonth, mTwelveMonthsExposedSecondMonth, mTwelveMonthsExposedThirdMonth, mTwentyFourMonthsExposedFirstMonth, mTwentyFourMonthsExposedSecondMonth, mTwentyFourMonthsExposedThirdMonth, mTwoMonthsYear, mTwelveMonthsYear;

                Int32 mTotalRegistered2Months, mTotalRegistered12Months, mTotalRegistered24Months;
                mTotalRegistered2Months = 0;
                mTotalRegistered12Months = 0;
                mTotalRegistered24Months = 0;
              
                Int32 mConfirmedUnInfected2Months, mConfirmedUnInfected12Months, mConfirmedUnInfected24Months, mConfirmedInfected2Months, mConfirmedInfected12Months, mConfirmedInfected24Months, mNotEligibleForART2Months, mNotEligibleForART12Months, mNotEligibleForART24Months, mNotConfirmed2Months, mNotConfirmed12Months, mNotConfirmed24Months, mOnCPT2Months, mOnCPT12Months, mOnCPT24Months, mNotOnCPT2Months, mNotOnCPT12Months, mNotOnCPT24Months, mCon2Months, mCon12Months, mCon24Months, mDis2Months, mDis12Months, mDis24Months, mART2Months, mART12Months, mART24Months, mTout2Months, mTout12Months, mTout24Months, mDef2Months, mDef12Months, mDef24Months, mD2Months, mD12Months, mD24Months;
                mConfirmedUnInfected2Months = 0;
                mConfirmedUnInfected12Months = 0;
                mConfirmedUnInfected24Months = 0;;
                mConfirmedInfected2Months = 0;
                mConfirmedInfected12Months = 0;
                mConfirmedInfected24Months = 0;
                mNotEligibleForART2Months = 0;
                mNotEligibleForART12Months = 0;
                mNotEligibleForART24Months = 0;
                mNotConfirmed2Months = 0;
                mNotConfirmed12Months = 0;
                mNotConfirmed24Months = 0;
                mOnCPT2Months = 0;
                mOnCPT12Months = 0;
                mOnCPT24Months = 0;
                mNotOnCPT2Months = 0;
                mNotOnCPT12Months = 0;
                mNotOnCPT24Months = 0;
                mCon2Months = 0;
                mCon12Months = 0;
                mCon24Months = 0;
                mDis2Months = 0;
                mDis12Months = 0;
                mDis24Months = 0;
                mART2Months = 0;
                mART12Months = 0;
                mART24Months = 0;
                mTout2Months = 0;
                mTout12Months = 0;
                mTout24Months = 0;
                mDef2Months = 0;
                mDef12Months = 0;
                mDef24Months = 0;
                mD2Months = 0;
                mD12Months = 0;
                mD24Months = 0;


                mTwoMonthsExposedFirstMonth = "";
                mTwoMonthsExposedSecondMonth = "";
                mTwoMonthsExposedThirdMonth = "";
                mTwelveMonthsExposedFirstMonth = "";
                mTwelveMonthsExposedSecondMonth = "";
                mTwelveMonthsExposedThirdMonth = "";
                mTwentyFourMonthsExposedFirstMonth = "";
                mTwentyFourMonthsExposedSecondMonth = "";
                mTwentyFourMonthsExposedThirdMonth = "";
                mTwoMonthsYear = "";

                if (mQuarter == "Q1")
                {
                    mTwoMonthsExposedFirstMonth = "October";
                    mTwoMonthsExposedSecondMonth = "November";
                    mTwoMonthsExposedThirdMonth = "December";
                    mTwelveMonthsExposedFirstMonth = "November " + Convert.ToInt32(mYearPart - 2);
                    mTwelveMonthsExposedSecondMonth = "December " + Convert.ToInt32(mYearPart - 2);
                    mTwelveMonthsExposedThirdMonth = "January " + Convert.ToInt32(mYearPart - 1);
                    mTwentyFourMonthsExposedFirstMonth = "November " + Convert.ToInt32(mYearPart - 3);
                    mTwentyFourMonthsExposedSecondMonth = "December " + Convert.ToInt32(mYearPart - 3);
                    mTwentyFourMonthsExposedThirdMonth = "January " + Convert.ToInt32(mYearPart - 2);
                    mTwoMonthsYear = Convert.ToString(mYearPart - 1);
                }

                if (mQuarter == "Q2")
                {
                    mTwoMonthsExposedFirstMonth = "January";
                    mTwoMonthsExposedSecondMonth = "February";
                    mTwoMonthsExposedThirdMonth = "March";
                    mTwelveMonthsExposedFirstMonth = "February " + Convert.ToInt32(mYearPart - 1);
                    mTwelveMonthsExposedSecondMonth = "March " + Convert.ToInt32(mYearPart - 1);
                    mTwelveMonthsExposedThirdMonth = "April " + Convert.ToInt32(mYearPart - 1);
                    mTwentyFourMonthsExposedFirstMonth = "February " + Convert.ToInt32(mYearPart - 2);
                    mTwentyFourMonthsExposedSecondMonth = "March " + Convert.ToInt32(mYearPart - 2);
                    mTwentyFourMonthsExposedThirdMonth = "April " + Convert.ToInt32(mYearPart - 2);
                    mTwoMonthsYear = Convert.ToString(mYearPart);
                }

                if (mQuarter == "Q3")
                {
                    mTwoMonthsExposedFirstMonth = "April";
                    mTwoMonthsExposedSecondMonth = "May";
                    mTwoMonthsExposedThirdMonth = "June";
                    mTwelveMonthsExposedFirstMonth = "May " + Convert.ToInt32(mYearPart - 1);
                    mTwelveMonthsExposedSecondMonth = "June " + Convert.ToInt32(mYearPart - 1);
                    mTwelveMonthsExposedThirdMonth = "July " + Convert.ToInt32(mYearPart - 1);
                    mTwentyFourMonthsExposedFirstMonth = "May " + Convert.ToInt32(mYearPart - 2);
                    mTwentyFourMonthsExposedSecondMonth = "June " + Convert.ToInt32(mYearPart - 2);
                    mTwentyFourMonthsExposedThirdMonth = "July " + Convert.ToInt32(mYearPart - 2);
                    mTwoMonthsYear = Convert.ToString(mYearPart);
                }

                if (mQuarter == "Q4")
                {
                    mTwoMonthsExposedFirstMonth = "July";
                    mTwoMonthsExposedSecondMonth = "August";
                    mTwoMonthsExposedThirdMonth = "September";
                    mTwelveMonthsExposedFirstMonth = "August " + Convert.ToInt32(mYearPart - 1);
                    mTwelveMonthsExposedSecondMonth = "September " + Convert.ToInt32(mYearPart - 1);
                    mTwelveMonthsExposedThirdMonth = "October " + Convert.ToInt32(mYearPart - 1);
                    mTwentyFourMonthsExposedFirstMonth = "August " + Convert.ToInt32(mYearPart - 2);
                    mTwentyFourMonthsExposedSecondMonth = "September " + Convert.ToInt32(mYearPart - 2);
                    mTwentyFourMonthsExposedThirdMonth = "October " + Convert.ToInt32(mYearPart - 2);
                    mTwoMonthsYear = Convert.ToString(mYearPart);
                }
             

                            
                #region FirstMonth
                                              
                

                #region FirtMonth_TwoMonths Cohort
                mFunctionName = "ExposedChildUnder24Months - FirtMonth_TwoMonths Cohort";
                OdbcCommand cmdExposedChildFirstMonthTI = new OdbcCommand();
                OdbcCommand cmdExposedChildFirstMonthOther = new OdbcCommand();
                DateTime mFirstDayInFirstMonthOfQuarter = Convert.ToDateTime("1 " + mTwoMonthsExposedFirstMonth + " " + mTwoMonthsYear);
                Int16 mNumberOfDaysInFirstMonth = Convert.ToInt16(DateTime.DaysInMonth(Convert.ToDateTime(mFirstDayInFirstMonthOfQuarter).Year, Convert.ToDateTime(mFirstDayInFirstMonthOfQuarter).Month));

                cmdExposedChildFirstMonthTI.Connection = mConn;
                cmdExposedChildFirstMonthTI.CommandText = "Select a.patientcode, a.transferindate, a.enrolmentdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_exposed a, patients p where a.patientcode = p.code and a.transferindate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mFirstDayInFirstMonthOfQuarter) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mNumberOfDaysInFirstMonth + " " + mTwoMonthsExposedFirstMonth + " " + mTwoMonthsYear)) + " and a.modeofentry = 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(mNumberOfDaysInFirstMonth + " " + mTwoMonthsExposedFirstMonth + " " + mTwoMonthsYear)) + ", p.birthdate) < '728.5'";
                cmdExposedChildFirstMonthTI.ExecuteNonQuery();

                cmdExposedChildFirstMonthOther.Connection = mConn;
                cmdExposedChildFirstMonthOther.CommandText = "Select a.patientcode, a.transferindate, a.enrolmentdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_exposed a, patients p where a.patientcode = p.code and a.enrolmentdate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mFirstDayInFirstMonthOfQuarter) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mNumberOfDaysInFirstMonth + " " + mTwoMonthsExposedFirstMonth + " " + mTwoMonthsYear)) + " and a.modeofentry <> 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(mNumberOfDaysInFirstMonth + " " + mTwoMonthsExposedFirstMonth + " " + mTwoMonthsYear)) + ", p.birthdate) < '728.5'";
                cmdExposedChildFirstMonthOther.ExecuteNonQuery();

                //clsGlobal.Write_Error(pClassName, mFunctionName, "Select a.patientcode, a.transferindate, a.enrolmentdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_exposed a, patients p where a.patientcode = p.code and a.enrolmentdate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mFirstDayInFirstMonthOfQuarter) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mNumberOfDaysInFirstMonth + " " + mTwoMonthsExposedFirstMonth + " " + mTwoMonthsYear)) + " and a.modeofentry <> 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(mNumberOfDaysInFirstMonth + " " + mTwoMonthsExposedFirstMonth + " " + mTwoMonthsYear)) + ", p.birthdate) < '728.5'");

                                 
                mAdExposedChildUnder24MonthsTI.SelectCommand = cmdExposedChildFirstMonthTI;
                mAdExposedChildUnder24MonthsTI.Fill(mDsExposedChild);

                mAdExposedChildUnder24MonthsOther.SelectCommand = cmdExposedChildFirstMonthOther;
                mAdExposedChildUnder24MonthsOther.Fill(mDsExposedChild);

                mTotalRegistered2Months = mDsExposedChild.Rows.Count;


                foreach (DataRow myRow in (mDsExposedChild.Rows))
                {
                    
                    String Outcome = myRow["latestoutcome"].ToString();
                    String EntryMode = myRow["modeofentry"].ToString();
                    String Gender = myRow["gender"].ToString();
                    TimeSpan Ts = (Convert.ToDateTime(mQuarterEndDate) - Convert.ToDateTime(myRow["birthdate"]));
                    double AgeInDays = Ts.Days;


                    string mPatientCode = myRow["patientcode"].ToString();
                    DateTime mFromBeginning = Convert.ToDateTime("1900/01/01");
                    OdbcCommand cmdPreARTExposedLog = new OdbcCommand();
                    cmdPreARTExposedLog.Connection = mConn;
                    cmdPreARTExposedLog.CommandText = "select * from ctc_exposedlog where patientcode ='" + mPatientCode + "'";
                    cmdPreARTExposedLog.ExecuteNonQuery();

                    string mHIVStatus = "";
                    Double mCPTTablets = 0;
                    Double mCPTMills = 0;

                    OdbcDataAdapter adPreARTExposedLog = new OdbcDataAdapter();
                    DataTable tbPreARTExposedLog = new DataTable();

                    adPreARTExposedLog.SelectCommand = cmdPreARTExposedLog;
                    adPreARTExposedLog.Fill(tbPreARTExposedLog);

                    Int64 mAutoCode = 0;
                    if (tbPreARTExposedLog.Rows.Count != 0)
                    {
                        foreach (DataRow myARTLogRow in tbPreARTExposedLog.Rows)
                        {
                            if (mAutoCode < Convert.ToInt64(myARTLogRow["autocode"]))
                            {
                                Outcome = Convert.ToString(myARTLogRow["outcomecode"]);
                                mAutoCode = Convert.ToInt64(myARTLogRow["autocode"]);
                                mHIVStatus = Convert.ToString(myARTLogRow["hivinfectioncode"]);
                                mCPTTablets = Convert.ToDouble(myARTLogRow["cpttablets"]);
                                mCPTMills = Convert.ToDouble(myARTLogRow["cptmills"]); 
                            }
                        }
                    }

                    if ((myRow["modeofentry"].ToString() == "TI") && (Convert.ToDateTime(myRow["transferindate"]) < mQuarterStartDate) && (AgeInDays >= 729))
                    {
                        OdbcCommand cmdPreARTAppointment = new OdbcCommand();
                        cmdPreARTAppointment.Connection = mConn;
                        cmdPreARTAppointment.CommandText = "select * from ctc_appointments where patientcode ='" + mPatientCode + "' and apptdate >= " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                        cmdPreARTAppointment.ExecuteNonQuery();

                        OdbcDataAdapter adPreARTAppointment = new OdbcDataAdapter();
                        DataTable tbPreARTAppointment = new DataTable();

                        adPreARTAppointment.SelectCommand = cmdPreARTAppointment;
                        adPreARTAppointment.Fill(tbPreARTAppointment);

                        if ((tbPreARTAppointment.Rows.Count == 0) && (Outcome == "Con") || (Outcome == "Def"))
                        {
                            Outcome ="Def";
                        }


                        DateTime mAppD;
                        Double mDtInDays = 0;

                        if ((tbPreARTAppointment.Rows.Count > 0))
                        {
                            foreach (DataRow myARTAppointmentRow in tbPreARTAppointment.Rows)
                            {
                                mAppD = Convert.ToDateTime(myARTAppointmentRow["apptdate"]);
                                TimeSpan mT = (mAppD - mQuarterEndDate);
                                mDtInDays = mT.Days;
                            }
                        }

                        if (mDtInDays > 60.83)
                        {
                           Outcome ="Def";
                        }


                    }


                    if ((myRow["modeofentry"].ToString() != "TI") && (Convert.ToDateTime(myRow["enrolmentdate"]) < mQuarterStartDate) && (AgeInDays >= 729))
                    {
                        OdbcCommand cmdPreARTAppointment = new OdbcCommand();
                        cmdPreARTAppointment.Connection = mConn;
                        cmdPreARTAppointment.CommandText = "select * from ctc_appointments where patientcode ='" + mPatientCode + "' and apptdate >= " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                        cmdPreARTAppointment.ExecuteNonQuery();

                        OdbcDataAdapter adPreARTAppointment = new OdbcDataAdapter();
                        DataTable tbPreARTAppointment = new DataTable();

                        adPreARTAppointment.SelectCommand = cmdPreARTAppointment;
                        adPreARTAppointment.Fill(tbPreARTAppointment);

                        if ((tbPreARTAppointment.Rows.Count == 0) && (Outcome == "Con") || (Outcome == "Def"))
                        {
                           Outcome="Def";
                        }


                        DateTime mAppD;
                        Double mDtInDays = 0;

                        if ((tbPreARTAppointment.Rows.Count > 0))
                        {
                            foreach (DataRow myARTAppointmentRow in tbPreARTAppointment.Rows)
                            {
                                mAppD = Convert.ToDateTime(myARTAppointmentRow["apptdate"]);
                                TimeSpan mT = (mAppD - mQuarterEndDate);
                                mDtInDays = mT.Days;
                            }
                        }

                        if (mDtInDays > 60.83)
                        {
                           Outcome ="Def";
                        }


                    }

                    if ((mCPTTablets == 0) && (mCPTMills == 0))
                    {
                        mNotOnCPT2Months += 1;
                    }
                    else
                    {
                        mOnCPT2Months += 1;
                    }

                    if (mHIVStatus == "A")
                    {
                        mConfirmedUnInfected2Months += 1;
                    }

                    if (mHIVStatus == "B")
                    {
                        mConfirmedInfected2Months += 1;
                    }

                    if (mHIVStatus == "C")
                    {
                       mNotEligibleForART2Months += 1;
                    }

                    if (mHIVStatus == "D")
                    {
                        mNotConfirmed2Months += 1;
                    }

                    if (Outcome == "Con")
                    {
                        mCon2Months += 1;
                    }

                    if (Outcome == "D")
                    {
                        mD2Months += 1;
                    }

                    if (Outcome == "TO")
                    {
                        mTout2Months += 1;
                    }

                    if (Outcome == "Def")
                    {
                        mDef2Months += 1;
                    }

                    if (Outcome == "Dis")
                    {
                        mDis2Months += 1;
                    }

                }


                #endregion

                #region FirstMonth_12Months Cohort
                mFunctionName = "ExposedChildUnder24Months - FirstMonth_12Months Cohort";
                OdbcCommand cmdExposedChildFirstMonthM12TI = new OdbcCommand();
                OdbcCommand cmdExposedChildFirstMonthM12Other = new OdbcCommand();
                DateTime mFirstDayInFirstMonthOfQuarterM12 = Convert.ToDateTime("1 " + mTwelveMonthsExposedFirstMonth);
                Int16 mNumberOfDaysInFisrtMonth12 = Convert.ToInt16(DateTime.DaysInMonth(Convert.ToDateTime(mFirstDayInFirstMonthOfQuarterM12).Year, Convert.ToDateTime(mFirstDayInFirstMonthOfQuarterM12).Month));

                cmdExposedChildFirstMonthM12TI.Connection = mConn;
                cmdExposedChildFirstMonthM12TI.CommandText = "Select a.patientcode, a.transferindate, a.enrolmentdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_exposed a, patients p where a.patientcode = p.code and a.transferindate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mFirstDayInFirstMonthOfQuarterM12) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mFirstDayInFirstMonthOfQuarterM12.Year, mFirstDayInFirstMonthOfQuarterM12.Month) + " " + mTwelveMonthsExposedFirstMonth)) + " and a.modeofentry = 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mFirstDayInFirstMonthOfQuarterM12.Year, mFirstDayInFirstMonthOfQuarterM12.Month) + " " + mTwelveMonthsExposedFirstMonth)) + ", p.birthdate) < '728.5'";
                cmdExposedChildFirstMonthM12TI.ExecuteNonQuery();

                cmdExposedChildFirstMonthM12Other.Connection = mConn;
                cmdExposedChildFirstMonthM12Other.CommandText = "Select a.patientcode, a.transferindate, a.enrolmentdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_exposed a, patients p where a.patientcode = p.code and a.enrolmentdate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mFirstDayInFirstMonthOfQuarterM12) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mFirstDayInFirstMonthOfQuarterM12.Year, mFirstDayInFirstMonthOfQuarterM12.Month) + " " + mTwelveMonthsExposedFirstMonth)) + " and a.modeofentry <> 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mFirstDayInFirstMonthOfQuarterM12.Year, mFirstDayInFirstMonthOfQuarterM12.Month) + " " + mTwelveMonthsExposedFirstMonth)) + ", p.birthdate) < '728.5'";
                cmdExposedChildFirstMonthM12Other.ExecuteNonQuery();

                //clsGlobal.Write_Error(pClassName, mFunctionName, "Select a.patientcode, a.transferindate, a.dateofinitiation, a.confirmdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_preart a, patients p where a.patientcode = p.code and a.dateofinitiation between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mFirstDayInFirstMonthOfQuarterM12) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mFirstDayInFirstMonthOfQuarterM12.Year, mFirstDayInFirstMonthOfQuarterM12.Month) + " " + mTwelveMonthsExposedFirstMonth)) + " and a.modeofentry <> 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mFirstDayInFirstMonthOfQuarterM12.Year, mFirstDayInFirstMonthOfQuarterM12.Month) + " " + mTwelveMonthsExposedFirstMonth)) + ", p.birthdate) < '728.5'");

                mDsExposedChild.Clear();
                mAdExposedChildUnder24MonthsTI.SelectCommand = cmdExposedChildFirstMonthM12TI;
                mAdExposedChildUnder24MonthsTI.Fill(mDsExposedChild);

                mAdExposedChildUnder24MonthsOther.SelectCommand = cmdExposedChildFirstMonthM12Other;
                mAdExposedChildUnder24MonthsOther.Fill(mDsExposedChild);

                mTotalRegistered12Months = mDsExposedChild.Rows.Count;


                foreach (DataRow myRow in (mDsExposedChild.Rows))
                {

                    String Outcome = myRow["latestoutcome"].ToString();
                    String EntryMode = myRow["modeofentry"].ToString();
                    String Gender = myRow["gender"].ToString();
                    TimeSpan Ts = (Convert.ToDateTime(mQuarterEndDate) - Convert.ToDateTime(myRow["birthdate"]));
                    double AgeInDays = Ts.Days;

                    string mPatientCode = myRow["patientcode"].ToString();
                    DateTime mFromBeginning = Convert.ToDateTime("1900/01/01");
                    OdbcCommand cmdPreARTExposedLog = new OdbcCommand();
                    cmdPreARTExposedLog.Connection = mConn;
                    cmdPreARTExposedLog.CommandText = "select * from ctc_exposedlog where patientcode ='" + mPatientCode + "'"; // and transdate between " + clsGlobal.Saving_DateValue(mQuarterStartDate) + " AND " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                    cmdPreARTExposedLog.ExecuteNonQuery();

                    OdbcDataAdapter adPreARTExposedLog = new OdbcDataAdapter();
                    DataTable tbPreARTExposedLog = new DataTable();

                    adPreARTExposedLog.SelectCommand = cmdPreARTExposedLog;
                    adPreARTExposedLog.Fill(tbPreARTExposedLog);

                    string mHIVStatus = "";
                    Int64 mAutoCode = 0;
                    Double mCPTTablets = 0;
                    Double mCPTMills = 0;

                    if (tbPreARTExposedLog.Rows.Count != 0)
                    {
                        foreach (DataRow myARTLogRow in tbPreARTExposedLog.Rows)
                        {
                            if (mAutoCode < Convert.ToInt64(myARTLogRow["autocode"]))
                            {
                                Outcome = Convert.ToString(myARTLogRow["outcomecode"]);
                                mAutoCode = Convert.ToInt64(myARTLogRow["autocode"]);
                                mHIVStatus = Convert.ToString(myARTLogRow["hivinfectioncode"]);
                                mCPTTablets = Convert.ToDouble(myARTLogRow["cpttablets"]);
                                mCPTMills = Convert.ToDouble(myARTLogRow["cptmills"]); 
                            }
                        }
                    }

                    if ((myRow["modeofentry"].ToString() == "TI") && (Convert.ToDateTime(myRow["transferindate"]) < mQuarterStartDate) && (AgeInDays >= 729))
                    {
                        OdbcCommand cmdPreARTAppointment = new OdbcCommand();
                        cmdPreARTAppointment.Connection = mConn;
                        cmdPreARTAppointment.CommandText = "select * from ctc_appointments where patientcode ='" + mPatientCode + "' and apptdate >= " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                        cmdPreARTAppointment.ExecuteNonQuery();

                        OdbcDataAdapter adPreARTAppointment = new OdbcDataAdapter();
                        DataTable tbPreARTAppointment = new DataTable();

                        adPreARTAppointment.SelectCommand = cmdPreARTAppointment;
                        adPreARTAppointment.Fill(tbPreARTAppointment);

                        if ((tbPreARTAppointment.Rows.Count == 0) && (Outcome == "Con") || (Outcome == "Def"))
                        {
                            Outcome = "Def";
                        }


                        DateTime mAppD;
                        Double mDtInDays = 0;

                        if ((tbPreARTAppointment.Rows.Count > 0))
                        {
                            foreach (DataRow myARTAppointmentRow in tbPreARTAppointment.Rows)
                            {
                                mAppD = Convert.ToDateTime(myARTAppointmentRow["apptdate"]);
                                TimeSpan mT = (mAppD - mQuarterEndDate);
                                mDtInDays = mT.Days;
                            }
                        }

                        if (mDtInDays > 60.83)
                        {
                            Outcome = "Def";
                        }


                    }


                    if ((myRow["modeofentry"].ToString() != "TI") && (Convert.ToDateTime(myRow["enrolmentdate"]) < mQuarterStartDate) && (AgeInDays >= 729))
                    {
                        OdbcCommand cmdPreARTAppointment = new OdbcCommand();
                        cmdPreARTAppointment.Connection = mConn;
                        cmdPreARTAppointment.CommandText = "select * from ctc_appointments where patientcode ='" + mPatientCode + "' and apptdate >= " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                        cmdPreARTAppointment.ExecuteNonQuery();

                        OdbcDataAdapter adPreARTAppointment = new OdbcDataAdapter();
                        DataTable tbPreARTAppointment = new DataTable();

                        adPreARTAppointment.SelectCommand = cmdPreARTAppointment;
                        adPreARTAppointment.Fill(tbPreARTAppointment);

                        if ((tbPreARTAppointment.Rows.Count == 0) && (Outcome == "Con") || (Outcome == "Def"))
                        {
                            Outcome = "Def";
                        }


                        DateTime mAppD;
                        Double mDtInDays = 0;

                        if ((tbPreARTAppointment.Rows.Count > 0))
                        {
                            foreach (DataRow myARTAppointmentRow in tbPreARTAppointment.Rows)
                            {
                                mAppD = Convert.ToDateTime(myARTAppointmentRow["apptdate"]);
                                TimeSpan mT = (mAppD - mQuarterEndDate);
                                mDtInDays = mT.Days;
                            }
                        }

                        if (mDtInDays > 60.83)
                        {
                            Outcome = "Def";
                        }


                    }

                    if ((mCPTTablets == 0) && (mCPTMills == 0))
                    {
                        mNotOnCPT12Months += 1;
                    }
                    else
                    {
                        mOnCPT12Months += 1;
                    }

                    if (mHIVStatus == "A")
                    {
                        mConfirmedUnInfected12Months += 1;
                    }

                    if (mHIVStatus == "B")
                    {
                        mConfirmedInfected12Months += 1;
                    }

                    if (mHIVStatus == "C")
                    {
                        mNotEligibleForART12Months += 1;
                    }

                    if (mHIVStatus == "D")
                    {
                        mNotConfirmed12Months += 1;
                    }


                    if (Outcome == "Con")
                    {
                        mCon12Months += 1;
                    }

                    if (Outcome == "D")
                    {
                        mD12Months += 1;
                    }

                    if (Outcome == "TO")
                    {
                        mTout12Months += 1;
                    }

                    if (Outcome == "Def")
                    {
                        mDef12Months += 1;
                    }

                    if (Outcome == "Dis")
                    {
                        mDis12Months += 1;
                    }

                }

                #endregion

                #region FirstMonth_24Months Cohort
                mFunctionName = "ExposedChildUnder24Months - FirstMonth_24Months Cohort";
                OdbcCommand cmdExposedChildFirstMonthM24TI = new OdbcCommand();
                OdbcCommand cmdExposedChildFirstMonthM24Other = new OdbcCommand();
                DateTime mFirstDayInFirstMonthOfQuarterM24 = Convert.ToDateTime("1 " + mTwentyFourMonthsExposedFirstMonth);
                Int16 mNumberOfDaysInFisrtMonth24 = Convert.ToInt16(DateTime.DaysInMonth(Convert.ToDateTime(mFirstDayInFirstMonthOfQuarterM24).Year, Convert.ToDateTime(mFirstDayInFirstMonthOfQuarterM24).Month));

                cmdExposedChildFirstMonthM24TI.Connection = mConn;
                cmdExposedChildFirstMonthM24TI.CommandText = "Select a.patientcode, a.transferindate, a.enrolmentdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_exposed a, patients p where a.patientcode = p.code and a.transferindate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mFirstDayInFirstMonthOfQuarterM24) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mFirstDayInFirstMonthOfQuarterM24.Year, mFirstDayInFirstMonthOfQuarterM24.Month) + " " + mTwentyFourMonthsExposedFirstMonth)) + " and a.modeofentry = 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mFirstDayInFirstMonthOfQuarterM24.Year, mFirstDayInFirstMonthOfQuarterM24.Month) + " " + mTwentyFourMonthsExposedFirstMonth)) + ", p.birthdate) < '728.5'";
                cmdExposedChildFirstMonthM24TI.ExecuteNonQuery();

                cmdExposedChildFirstMonthM24Other.Connection = mConn;
                cmdExposedChildFirstMonthM24Other.CommandText = "Select a.patientcode, a.transferindate, a.enrolmentdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_exposed a, patients p where a.patientcode = p.code and a.enrolmentdate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mFirstDayInFirstMonthOfQuarterM24) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mFirstDayInFirstMonthOfQuarterM24.Year, mFirstDayInFirstMonthOfQuarterM24.Month) + " " + mTwentyFourMonthsExposedFirstMonth)) + " and a.modeofentry <> 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mFirstDayInFirstMonthOfQuarterM24.Year, mFirstDayInFirstMonthOfQuarterM24.Month) + " " + mTwentyFourMonthsExposedFirstMonth)) + ", p.birthdate) < '728.5'";
                cmdExposedChildFirstMonthM24Other.ExecuteNonQuery();

                //clsGlobal.Write_Error(pClassName, mFunctionName, "Select a.patientcode, a.transferindate, a.dateofinitiation, a.confirmdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_preart a, patients p where a.patientcode = p.code and a.transferindate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mFirstDayInFirstMonthOfQuarterM24) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mFirstDayInFirstMonthOfQuarterM24.Year, mFirstDayInFirstMonthOfQuarterM24.Month) + " " + mTwentyFourMonthsExposedFirstMonth)) + " and a.modeofentry = 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mFirstDayInFirstMonthOfQuarterM24.Year, mFirstDayInFirstMonthOfQuarterM24.Month) + " " + mTwentyFourMonthsExposedFirstMonth)) + ", p.birthdate) < '728.5'");

                mDsExposedChild.Clear();
                mAdExposedChildUnder24MonthsTI.SelectCommand = cmdExposedChildFirstMonthM24TI;
                mAdExposedChildUnder24MonthsTI.Fill(mDsExposedChild);

                mAdExposedChildUnder24MonthsOther.SelectCommand = cmdExposedChildFirstMonthM24Other;
                mAdExposedChildUnder24MonthsOther.Fill(mDsExposedChild);

                mTotalRegistered24Months = mDsExposedChild.Rows.Count;


                foreach (DataRow myRow in (mDsExposedChild.Rows))
                {

                    String Outcome = myRow["latestoutcome"].ToString();
                    String EntryMode = myRow["modeofentry"].ToString();
                    String Gender = myRow["gender"].ToString();
                    TimeSpan Ts = (Convert.ToDateTime(mQuarterEndDate) - Convert.ToDateTime(myRow["birthdate"]));
                    double AgeInDays = Ts.Days;

                    string mPatientCode = myRow["patientcode"].ToString();
                    DateTime mFromBeginning = Convert.ToDateTime("1900/01/01");
                    OdbcCommand cmdPreARTExposedLog = new OdbcCommand();
                    cmdPreARTExposedLog.Connection = mConn;
                    cmdPreARTExposedLog.CommandText = "select * from ctc_exposedlog where patientcode ='" + mPatientCode + "'"; // and transdate between " + clsGlobal.Saving_DateValue(mQuarterStartDate) + " AND " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                    cmdPreARTExposedLog.ExecuteNonQuery();

                    OdbcDataAdapter adPreARTExposedLog = new OdbcDataAdapter();
                    DataTable tbPreARTExposedLog = new DataTable();

                    adPreARTExposedLog.SelectCommand = cmdPreARTExposedLog;
                    adPreARTExposedLog.Fill(tbPreARTExposedLog);

                    Int64 mAutoCode = 0;
                    string mHIVStatus = "";
                    Double mCPTTablets = 0;
                    Double mCPTMills = 0;

                    if (tbPreARTExposedLog.Rows.Count != 0)
                    {
                        foreach (DataRow myARTLogRow in tbPreARTExposedLog.Rows)
                        {
                            if (mAutoCode < Convert.ToInt64(myARTLogRow["autocode"]))
                            {
                                Outcome = Convert.ToString(myARTLogRow["outcomecode"]);
                                mAutoCode = Convert.ToInt64(myARTLogRow["autocode"]);
                                mHIVStatus = Convert.ToString(myARTLogRow["hivinfectioncode"]);
                                mCPTTablets = Convert.ToDouble(myARTLogRow["cpttablets"]);
                                mCPTMills = Convert.ToDouble(myARTLogRow["cptmills"]); 
                            }
                        }
                    }

                    if ((myRow["modeofentry"].ToString() == "TI") && (Convert.ToDateTime(myRow["transferindate"]) < mQuarterStartDate) && (AgeInDays >= 729))
                    {
                        OdbcCommand cmdPreARTAppointment = new OdbcCommand();
                        cmdPreARTAppointment.Connection = mConn;
                        cmdPreARTAppointment.CommandText = "select * from ctc_appointments where patientcode ='" + mPatientCode + "' and apptdate >= " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                        cmdPreARTAppointment.ExecuteNonQuery();

                        OdbcDataAdapter adPreARTAppointment = new OdbcDataAdapter();
                        DataTable tbPreARTAppointment = new DataTable();

                        adPreARTAppointment.SelectCommand = cmdPreARTAppointment;
                        adPreARTAppointment.Fill(tbPreARTAppointment);

                        if ((tbPreARTAppointment.Rows.Count == 0) && (Outcome == "Con") || (Outcome == "Def"))
                        {
                            Outcome = "Def";
                        }


                        DateTime mAppD;
                        Double mDtInDays = 0;

                        if ((tbPreARTAppointment.Rows.Count > 0))
                        {
                            foreach (DataRow myARTAppointmentRow in tbPreARTAppointment.Rows)
                            {
                                mAppD = Convert.ToDateTime(myARTAppointmentRow["apptdate"]);
                                TimeSpan mT = (mAppD - mQuarterEndDate);
                                mDtInDays = mT.Days;
                            }
                        }

                        if (mDtInDays > 60.83)
                        {
                            Outcome = "Def";
                        }


                    }


                    if ((myRow["modeofentry"].ToString() != "TI") && (Convert.ToDateTime(myRow["enrolmentdate"]) < mQuarterStartDate) && (AgeInDays >= 729))
                    {
                        OdbcCommand cmdPreARTAppointment = new OdbcCommand();
                        cmdPreARTAppointment.Connection = mConn;
                        cmdPreARTAppointment.CommandText = "select * from ctc_appointments where patientcode ='" + mPatientCode + "' and apptdate >= " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                        cmdPreARTAppointment.ExecuteNonQuery();

                        OdbcDataAdapter adPreARTAppointment = new OdbcDataAdapter();
                        DataTable tbPreARTAppointment = new DataTable();

                        adPreARTAppointment.SelectCommand = cmdPreARTAppointment;
                        adPreARTAppointment.Fill(tbPreARTAppointment);

                        if ((tbPreARTAppointment.Rows.Count == 0) && (Outcome == "Con") || (Outcome == "Def"))
                        {
                            Outcome = "Def";
                        }


                        DateTime mAppD;
                        Double mDtInDays = 0;

                        if ((tbPreARTAppointment.Rows.Count > 0))
                        {
                            foreach (DataRow myARTAppointmentRow in tbPreARTAppointment.Rows)
                            {
                                mAppD = Convert.ToDateTime(myARTAppointmentRow["apptdate"]);
                                TimeSpan mT = (mAppD - mQuarterEndDate);
                                mDtInDays = mT.Days;
                            }
                        }

                        if (mDtInDays > 60.83)
                        {
                            Outcome = "Def";
                        }


                    }

                    if ((mCPTTablets == 0) && (mCPTMills == 0))
                    {
                        mNotOnCPT24Months += 1;
                    }
                    else
                    {
                        mOnCPT24Months += 1;
                    }

                    if (mHIVStatus == "A")
                    {
                        mConfirmedUnInfected24Months += 1;
                    }

                    if (mHIVStatus == "B")
                    {
                        mConfirmedInfected24Months += 1;
                    }

                    if (mHIVStatus == "C")
                    {
                        mNotEligibleForART24Months += 1;
                    }

                    if (mHIVStatus == "D")
                    {
                        mNotConfirmed24Months += 1;
                    }


                    if (Outcome == "Con")
                    {
                        mCon24Months += 1;
                    }

                    if (Outcome == "D")
                    {
                        mD24Months += 1;
                    }

                    if (Outcome == "TO")
                    {
                        mTout24Months += 1;
                    }

                    if (Outcome == "Def")
                    {
                        mDef24Months += 1;
                    }

                    if (Outcome == "Dis")
                    {
                        mDis24Months += 1;
                    }

                }

                #endregion

                mDtExposedChildUnder24Months.Columns.Add("keyvalue", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("ClinicName", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("ReportingYear", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("ReportingMonth", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("TotalRegistered2Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("TotalRegistered12Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("TotalRegistered24Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("ConfirmedUnInfected2Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("ConfirmedUnInfected12Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("ConfirmedUnInfected24Months", typeof(System.String));

                mDtExposedChildUnder24Months.Columns.Add("ConfirmedInfected2Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("ConfirmedInfected12Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("ConfirmedInfected24Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("NotConfirmed2Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("NotConfirmed12Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("NotConfirmed24Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("NotEligibleForART2Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("NotEligibleForART12Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("NotEligibleForART24Months", typeof(System.String));

                mDtExposedChildUnder24Months.Columns.Add("OnCPT2Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("OnCPT12Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("OnCPT24Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("NotOnCPT2Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("NotOnCPT12Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("NotOnCPT24Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("Con2Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("Con12Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("Con24Months", typeof(System.String));

                mDtExposedChildUnder24Months.Columns.Add("Dis2Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("Dis12Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("Dis24Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("ART2Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("ART12Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("ART24Months", typeof(System.String));

                mDtExposedChildUnder24Months.Columns.Add("TO2Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("TO12Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("TO24Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("Def2Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("Def12Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("Def24Months", typeof(System.String));

                mDtExposedChildUnder24Months.Columns.Add("D2Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("D12Months", typeof(System.String));
                mDtExposedChildUnder24Months.Columns.Add("D24Months", typeof(System.String));
              


                mDtExposedChildUnder24Months.AcceptChanges();

                mDtExposedChildUnder24Months.Rows.Add("parentlink", " ", mYearPart, mFirstMonthInQuarter, mTotalRegistered2Months, mTotalRegistered12Months, mTotalRegistered24Months, mConfirmedUnInfected2Months, mConfirmedUnInfected12Months, mConfirmedUnInfected24Months, mConfirmedInfected2Months, mConfirmedInfected12Months, mConfirmedInfected24Months, mNotConfirmed2Months, mNotConfirmed12Months, mNotConfirmed24Months, mNotEligibleForART2Months, mNotEligibleForART12Months, mNotEligibleForART24Months, mOnCPT2Months, mOnCPT12Months, mOnCPT24Months, mNotOnCPT2Months, mNotOnCPT12Months, mNotOnCPT24Months, mCon2Months, mCon12Months, mCon24Months, mDis2Months, mDis12Months, mDis24Months, mART2Months, mART12Months, mART24Months, mTout2Months, mTout12Months, mTout24Months, mDef2Months, mDef12Months, mDef24Months, mD2Months, mD12Months, mD24Months);

                mDtExposedChildUnder24Months.AcceptChanges();


                mTotalRegistered2Months = 0;
                mTotalRegistered12Months = 0;
                mTotalRegistered24Months = 0;
                                
                mConfirmedUnInfected2Months = 0;
                mConfirmedUnInfected12Months = 0;
                mConfirmedUnInfected24Months = 0; ;
                mConfirmedInfected2Months = 0;
                mConfirmedInfected12Months = 0;
                mConfirmedInfected24Months = 0;
                mNotEligibleForART2Months = 0;
                mNotEligibleForART12Months = 0;
                mNotEligibleForART24Months = 0;
                mNotConfirmed2Months = 0;
                mNotConfirmed12Months = 0;
                mNotConfirmed24Months = 0;
                mOnCPT2Months = 0;
                mOnCPT12Months = 0;
                mOnCPT24Months = 0;
                mNotOnCPT2Months = 0;
                mNotOnCPT12Months = 0;
                mNotOnCPT24Months = 0;
                mCon2Months = 0;
                mCon12Months = 0;
                mCon24Months = 0;
                mDis2Months = 0;
                mDis12Months = 0;
                mDis24Months = 0;
                mART2Months = 0;
                mART12Months = 0;
                mART24Months = 0;
                mTout2Months = 0;
                mTout12Months = 0;
                mTout24Months = 0;
                mDef2Months = 0;
                mDef12Months = 0;
                mDef24Months = 0;
                mD2Months = 0;
                mD12Months = 0;
                mD24Months = 0;
                

                #endregion

                #region SecondMonth

                
                #region SecondMonth_TwoMonths Cohort
                mFunctionName = "ExposedChildUnder24Months - SecondMonth_TwoMonths Cohort";
                OdbcCommand cmdExposedChildSecondMonthTI = new OdbcCommand();
                OdbcCommand cmdExposedChildSecondMonthOther = new OdbcCommand();
                DateTime mFirstDayInSecondMonthOfQuarter = Convert.ToDateTime("1 " + mTwoMonthsExposedSecondMonth + " " + mTwoMonthsYear);
                Int16 mNumberOfDaysInSecondMonth = Convert.ToInt16(DateTime.DaysInMonth(Convert.ToDateTime(mFirstDayInSecondMonthOfQuarter).Year, Convert.ToDateTime(mFirstDayInSecondMonthOfQuarter).Month));

                cmdExposedChildSecondMonthTI.Connection = mConn;
                cmdExposedChildSecondMonthTI.CommandText = "Select a.patientcode, a.transferindate, a.enrolmentdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_exposed a, patients p where a.patientcode = p.code and a.transferindate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mFirstDayInSecondMonthOfQuarter) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mNumberOfDaysInSecondMonth + " " + mTwoMonthsExposedSecondMonth + " " + mTwoMonthsYear)) + " and a.modeofentry = 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(mNumberOfDaysInSecondMonth + " " + mTwoMonthsExposedSecondMonth + " " + mTwoMonthsYear)) + ", p.birthdate) < '728.5'";
                cmdExposedChildSecondMonthTI.ExecuteNonQuery();

                cmdExposedChildSecondMonthOther.Connection = mConn;
                cmdExposedChildSecondMonthOther.CommandText = "Select a.patientcode, a.transferindate, a.enrolmentdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_exposed a, patients p where a.patientcode = p.code and a.enrolmentdate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mFirstDayInSecondMonthOfQuarter) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mNumberOfDaysInSecondMonth + " " + mTwoMonthsExposedSecondMonth + " " + mTwoMonthsYear)) + " and a.modeofentry <> 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(mNumberOfDaysInSecondMonth + " " + mTwoMonthsExposedSecondMonth + " " + mTwoMonthsYear)) + ", p.birthdate) < '728.5'";
                cmdExposedChildSecondMonthOther.ExecuteNonQuery();

                // clsGlobal.Write_Error(pClassName, mFunctionName, "Select a.patientcode, a.transferindate, a.enrolmentdate, a.confirmdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_preart a, patients p where a.patientcode = p.code and a.dateofinitiation between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mSecondDayInSecondMonthOfQuarter) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mSecondDayInSecondMonthOfQuarter.Year, mSecondDayInSecondMonthOfQuarter.Month) + mSecondMonth)) + " and a.modeofentry <> 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mSecondDayInSecondMonthOfQuarter.Year, mSecondDayInSecondMonthOfQuarter.Month) + " " + mTwoMonthsExposedSecondMonth + " " + mTwoMonthsYear)) + ", p.birthdate) < '728.5'");

                mDsExposedChild.Clear();
                mAdExposedChildUnder24MonthsTI.SelectCommand = cmdExposedChildSecondMonthTI;
                mAdExposedChildUnder24MonthsTI.Fill(mDsExposedChild);

                mAdExposedChildUnder24MonthsOther.SelectCommand = cmdExposedChildSecondMonthOther;
                mAdExposedChildUnder24MonthsOther.Fill(mDsExposedChild);

                mTotalRegistered2Months = mDsExposedChild.Rows.Count;


                foreach (DataRow myRow in (mDsExposedChild.Rows))
                {

                    String Outcome = myRow["latestoutcome"].ToString();
                    String EntryMode = myRow["modeofentry"].ToString();
                    String Gender = myRow["gender"].ToString();
                    TimeSpan Ts = (Convert.ToDateTime(mQuarterEndDate) - Convert.ToDateTime(myRow["birthdate"]));
                    double AgeInDays = Ts.Days;

                    string mPatientCode = myRow["patientcode"].ToString();
                    DateTime mFromBeginning = Convert.ToDateTime("1900/01/01");
                    OdbcCommand cmdPreARTExposedLog = new OdbcCommand();
                    cmdPreARTExposedLog.Connection = mConn;
                    cmdPreARTExposedLog.CommandText = "select * from ctc_exposedlog where patientcode ='" + mPatientCode + "'"; // and transdate between " + clsGlobal.Saving_DateValue(mQuarterStartDate) + " AND " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                    cmdPreARTExposedLog.ExecuteNonQuery();

                    OdbcDataAdapter adPreARTExposedLog = new OdbcDataAdapter();
                    DataTable tbPreARTExposedLog = new DataTable();

                    adPreARTExposedLog.SelectCommand = cmdPreARTExposedLog;
                    adPreARTExposedLog.Fill(tbPreARTExposedLog);

                    Int64 mAutoCode = 0;
                    string mHIVStatus = "";
                    Double mCPTTablets = 0;
                    Double mCPTMills = 0;

                    if (tbPreARTExposedLog.Rows.Count != 0)
                    {
                        foreach (DataRow myARTLogRow in tbPreARTExposedLog.Rows)
                        {
                            if (mAutoCode < Convert.ToInt64(myARTLogRow["autocode"]))
                            {
                                Outcome = Convert.ToString(myARTLogRow["outcomecode"]);
                                mAutoCode = Convert.ToInt64(myARTLogRow["autocode"]);
                                mHIVStatus = Convert.ToString(myARTLogRow["hivinfectioncode"]);
                                mCPTTablets = Convert.ToDouble(myARTLogRow["cpttablets"]);
                                mCPTMills = Convert.ToDouble(myARTLogRow["cptmills"]); 
                            }
                        }
                    }

                    if ((myRow["modeofentry"].ToString() == "TI") && (Convert.ToDateTime(myRow["transferindate"]) < mQuarterStartDate) && (AgeInDays >= 729))
                    {
                        OdbcCommand cmdPreARTAppointment = new OdbcCommand();
                        cmdPreARTAppointment.Connection = mConn;
                        cmdPreARTAppointment.CommandText = "select * from ctc_appointments where patientcode ='" + mPatientCode + "' and apptdate >= " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                        cmdPreARTAppointment.ExecuteNonQuery();

                        OdbcDataAdapter adPreARTAppointment = new OdbcDataAdapter();
                        DataTable tbPreARTAppointment = new DataTable();

                        adPreARTAppointment.SelectCommand = cmdPreARTAppointment;
                        adPreARTAppointment.Fill(tbPreARTAppointment);

                        if ((tbPreARTAppointment.Rows.Count == 0) && (Outcome == "Con") || (Outcome == "Def"))
                        {
                            Outcome = "Def";
                        }


                        DateTime mAppD;
                        Double mDtInDays = 0;

                        if ((tbPreARTAppointment.Rows.Count > 0))
                        {
                            foreach (DataRow myARTAppointmentRow in tbPreARTAppointment.Rows)
                            {
                                mAppD = Convert.ToDateTime(myARTAppointmentRow["apptdate"]);
                                TimeSpan mT = (mAppD - mQuarterEndDate);
                                mDtInDays = mT.Days;
                            }
                        }

                        if (mDtInDays > 60.83)
                        {
                            Outcome = "Def";
                        }


                    }


                    if ((myRow["modeofentry"].ToString() != "TI") && (Convert.ToDateTime(myRow["enrolmentdate"]) < mQuarterStartDate) && (AgeInDays >= 729))
                    {
                        OdbcCommand cmdPreARTAppointment = new OdbcCommand();
                        cmdPreARTAppointment.Connection = mConn;
                        cmdPreARTAppointment.CommandText = "select * from ctc_appointments where patientcode ='" + mPatientCode + "' and apptdate >= " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                        cmdPreARTAppointment.ExecuteNonQuery();

                        OdbcDataAdapter adPreARTAppointment = new OdbcDataAdapter();
                        DataTable tbPreARTAppointment = new DataTable();

                        adPreARTAppointment.SelectCommand = cmdPreARTAppointment;
                        adPreARTAppointment.Fill(tbPreARTAppointment);

                        if ((tbPreARTAppointment.Rows.Count == 0) && (Outcome == "Con") || (Outcome == "Def"))
                        {
                            Outcome = "Def";
                        }


                        DateTime mAppD;
                        Double mDtInDays = 0;

                        if ((tbPreARTAppointment.Rows.Count > 0))
                        {
                            foreach (DataRow myARTAppointmentRow in tbPreARTAppointment.Rows)
                            {
                                mAppD = Convert.ToDateTime(myARTAppointmentRow["apptdate"]);
                                TimeSpan mT = (mAppD - mQuarterEndDate);
                                mDtInDays = mT.Days;
                            }
                        }

                        if (mDtInDays > 60.83)
                        {
                            Outcome = "Def";
                        }


                    }

                    if ((mCPTTablets == 0) && (mCPTMills == 0))
                    {
                        mNotOnCPT2Months += 1;
                    }
                    else
                    {
                        mOnCPT2Months += 1;
                    }

                    if (mHIVStatus == "A")
                    {
                        mConfirmedUnInfected2Months += 1;
                    }

                    if (mHIVStatus == "B")
                    {
                        mConfirmedInfected2Months += 1;
                    }

                    if (mHIVStatus == "C")
                    {
                        mNotEligibleForART2Months += 1;
                    }

                    if (mHIVStatus == "D")
                    {
                        mNotConfirmed2Months += 1;
                    }


                    if (Outcome == "Con")
                    {
                        mCon2Months += 1;
                    }

                    if (Outcome == "D")
                    {
                        mD2Months += 1;
                    }

                    if (Outcome == "TO")
                    {
                        mTout2Months += 1;
                    }

                    if (Outcome == "Def")
                    {
                        mDef2Months += 1;
                    }

                    if (Outcome == "Dis")
                    {
                        mDis2Months += 1;
                    }

                }


                #endregion

                #region SecondMonth_12Months Cohort
                mFunctionName = "ExposedChildUnder24Months - SecondMonth_12Months Cohort";
                OdbcCommand cmdExposedChildSecondMonthM12TI = new OdbcCommand();
                OdbcCommand cmdExposedChildSecondMonthM12Other = new OdbcCommand();
                DateTime mSecondDayInSecondMonthOfQuarterM12 = Convert.ToDateTime("1 " + mTwelveMonthsExposedSecondMonth);
                Int16 mNumberOfDaysInSecondMonth12 = Convert.ToInt16(DateTime.DaysInMonth(Convert.ToDateTime(mSecondDayInSecondMonthOfQuarterM12).Year, Convert.ToDateTime(mSecondDayInSecondMonthOfQuarterM12).Month));

                cmdExposedChildSecondMonthM12TI.Connection = mConn;
                cmdExposedChildSecondMonthM12TI.CommandText = "Select a.patientcode, a.transferindate, a.enrolmentdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_exposed a, patients p where a.patientcode = p.code and a.transferindate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mSecondDayInSecondMonthOfQuarterM12) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mSecondDayInSecondMonthOfQuarterM12.Year, mSecondDayInSecondMonthOfQuarterM12.Month) + " " + mTwelveMonthsExposedSecondMonth)) + " and a.modeofentry = 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mSecondDayInSecondMonthOfQuarterM12.Year, mSecondDayInSecondMonthOfQuarterM12.Month) + " " + mTwelveMonthsExposedSecondMonth)) + ", p.birthdate) < '728.5'";
                cmdExposedChildSecondMonthM12TI.ExecuteNonQuery();

                cmdExposedChildSecondMonthM12Other.Connection = mConn;
                cmdExposedChildSecondMonthM12Other.CommandText = "Select a.patientcode, a.transferindate, a.enrolmentdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_exposed a, patients p where a.patientcode = p.code and a.enrolmentdate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mSecondDayInSecondMonthOfQuarterM12) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mSecondDayInSecondMonthOfQuarterM12.Year, mSecondDayInSecondMonthOfQuarterM12.Month) + " " + mTwelveMonthsExposedSecondMonth)) + " and a.modeofentry <> 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mSecondDayInSecondMonthOfQuarterM12.Year, mSecondDayInSecondMonthOfQuarterM12.Month) + " " + mTwelveMonthsExposedSecondMonth)) + ", p.birthdate) < '728.5'";
                cmdExposedChildSecondMonthM12Other.ExecuteNonQuery();

                //clsGlobal.Write_Error(pClassName, mFunctionName, "Select a.patientcode, a.transferindate, a.dateofinitiation, a.confirmdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_preart a, patients p where a.patientcode = p.code and a.dateofinitiation between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mSecondDayInSecondMonthOfQuarterM12) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mSecondDayInSecondMonthOfQuarterM12.Year, mSecondDayInSecondMonthOfQuarterM12.Month) + " " + mTwelveMonthsExposedSecondMonth)) + " and a.modeofentry <> 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mSecondDayInSecondMonthOfQuarterM12.Year, mSecondDayInSecondMonthOfQuarterM12.Month) + " " + mTwelveMonthsExposedSecondMonth)) + ", p.birthdate) < '728.5'");

                mDsExposedChild.Clear();
                mAdExposedChildUnder24MonthsTI.SelectCommand = cmdExposedChildSecondMonthM12TI;
                mAdExposedChildUnder24MonthsTI.Fill(mDsExposedChild);

                mAdExposedChildUnder24MonthsOther.SelectCommand = cmdExposedChildSecondMonthM12Other;
                mAdExposedChildUnder24MonthsOther.Fill(mDsExposedChild);

                mTotalRegistered12Months = mDsExposedChild.Rows.Count;


                foreach (DataRow myRow in (mDsExposedChild.Rows))
                {

                    String Outcome = myRow["latestoutcome"].ToString();
                    String EntryMode = myRow["modeofentry"].ToString();
                    String Gender = myRow["gender"].ToString();
                    TimeSpan Ts = (Convert.ToDateTime(mQuarterEndDate) - Convert.ToDateTime(myRow["birthdate"]));
                    double AgeInDays = Ts.Days;

                    string mPatientCode = myRow["patientcode"].ToString();
                    DateTime mFromBeginning = Convert.ToDateTime("1900/01/01");
                    OdbcCommand cmdPreARTExposedLog = new OdbcCommand();
                    cmdPreARTExposedLog.Connection = mConn;
                    cmdPreARTExposedLog.CommandText = "select * from ctc_exposedlog where patientcode ='" + mPatientCode + "'"; // and transdate between " + clsGlobal.Saving_DateValue(mQuarterStartDate) + " AND " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                    cmdPreARTExposedLog.ExecuteNonQuery();

                    OdbcDataAdapter adPreARTExposedLog = new OdbcDataAdapter();
                    DataTable tbPreARTExposedLog = new DataTable();

                    adPreARTExposedLog.SelectCommand = cmdPreARTExposedLog;
                    adPreARTExposedLog.Fill(tbPreARTExposedLog);

                    string mHIVStatus = "";
                    Int64 mAutoCode = 0;
                    Double mCPTTablets = 0;
                    Double mCPTMills = 0;

                    if (tbPreARTExposedLog.Rows.Count != 0)
                    {
                        foreach (DataRow myARTLogRow in tbPreARTExposedLog.Rows)
                        {
                            if (mAutoCode < Convert.ToInt64(myARTLogRow["autocode"]))
                            {
                                Outcome = Convert.ToString(myARTLogRow["outcomecode"]);
                                mAutoCode = Convert.ToInt64(myARTLogRow["autocode"]);
                                mHIVStatus = Convert.ToString(myARTLogRow["hivinfectioncode"]);
                                mCPTTablets = Convert.ToDouble(myARTLogRow["cpttablets"]);
                                mCPTMills = Convert.ToDouble(myARTLogRow["cptmills"]); 
                            }
                        }
                    }

                    if ((myRow["modeofentry"].ToString() == "TI") && (Convert.ToDateTime(myRow["transferindate"]) < mQuarterStartDate) && (AgeInDays >= 729))
                    {
                        OdbcCommand cmdPreARTAppointment = new OdbcCommand();
                        cmdPreARTAppointment.Connection = mConn;
                        cmdPreARTAppointment.CommandText = "select * from ctc_appointments where patientcode ='" + mPatientCode + "' and apptdate >= " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                        cmdPreARTAppointment.ExecuteNonQuery();

                        OdbcDataAdapter adPreARTAppointment = new OdbcDataAdapter();
                        DataTable tbPreARTAppointment = new DataTable();

                        adPreARTAppointment.SelectCommand = cmdPreARTAppointment;
                        adPreARTAppointment.Fill(tbPreARTAppointment);

                        if ((tbPreARTAppointment.Rows.Count == 0) && (Outcome == "Con") || (Outcome == "Def"))
                        {
                            Outcome = "Def";
                        }


                        DateTime mAppD;
                        Double mDtInDays = 0;

                        if ((tbPreARTAppointment.Rows.Count > 0))
                        {
                            foreach (DataRow myARTAppointmentRow in tbPreARTAppointment.Rows)
                            {
                                mAppD = Convert.ToDateTime(myARTAppointmentRow["apptdate"]);
                                TimeSpan mT = (mAppD - mQuarterEndDate);
                                mDtInDays = mT.Days;
                            }
                        }

                        if (mDtInDays > 60.83)
                        {
                            Outcome = "Def";
                        }


                    }


                    if ((myRow["modeofentry"].ToString() != "TI") && (Convert.ToDateTime(myRow["enrolmentdate"]) < mQuarterStartDate) && (AgeInDays >= 729))
                    {
                        OdbcCommand cmdPreARTAppointment = new OdbcCommand();
                        cmdPreARTAppointment.Connection = mConn;
                        cmdPreARTAppointment.CommandText = "select * from ctc_appointments where patientcode ='" + mPatientCode + "' and apptdate >= " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                        cmdPreARTAppointment.ExecuteNonQuery();

                        OdbcDataAdapter adPreARTAppointment = new OdbcDataAdapter();
                        DataTable tbPreARTAppointment = new DataTable();

                        adPreARTAppointment.SelectCommand = cmdPreARTAppointment;
                        adPreARTAppointment.Fill(tbPreARTAppointment);

                        if ((tbPreARTAppointment.Rows.Count == 0) && (Outcome == "Con") || (Outcome == "Def"))
                        {
                            Outcome = "Def";
                        }


                        DateTime mAppD;
                        Double mDtInDays = 0;

                        if ((tbPreARTAppointment.Rows.Count > 0))
                        {
                            foreach (DataRow myARTAppointmentRow in tbPreARTAppointment.Rows)
                            {
                                mAppD = Convert.ToDateTime(myARTAppointmentRow["apptdate"]);
                                TimeSpan mT = (mAppD - mQuarterEndDate);
                                mDtInDays = mT.Days;
                            }
                        }

                        if (mDtInDays > 60.83)
                        {
                            Outcome = "Def";
                        }


                    }

                    if ((mCPTTablets == 0) && (mCPTMills == 0))
                    {
                        mNotOnCPT12Months += 1;
                    }
                    else
                    {
                        mOnCPT12Months += 1;
                    }

                    if (mHIVStatus == "A")
                    {
                        mConfirmedUnInfected12Months += 1;
                    }

                    if (mHIVStatus == "B")
                    {
                        mConfirmedInfected12Months += 1;
                    }

                    if (mHIVStatus == "C")
                    {
                        mNotEligibleForART12Months += 1;
                    }

                    if (mHIVStatus == "D")
                    {
                        mNotConfirmed12Months += 1;
                    }

                    if (Outcome == "Con")
                    {
                        mCon12Months += 1;
                    }

                    if (Outcome == "D")
                    {
                        mD12Months += 1;
                    }

                    if (Outcome == "TO")
                    {
                        mTout12Months += 1;
                    }

                    if (Outcome == "Def")
                    {
                        mDef12Months += 1;
                    }

                    if (Outcome == "Dis")
                    {
                        mDis12Months += 1;
                    }

                }

                #endregion

                #region SecondMonth_24Months Cohort
                mFunctionName = "ExposedChildUnder24Months - SecondMonth_24Months Cohort";
                OdbcCommand cmdExposedChildSecondMonthM24TI = new OdbcCommand();
                OdbcCommand cmdExposedChildSecondMonthM24Other = new OdbcCommand();
                DateTime mSecondDayInSecondMonthOfQuarterM24 = Convert.ToDateTime("1 " + mTwentyFourMonthsExposedSecondMonth);
                Int16 mNumberOfDaysInSecondMonth24 = Convert.ToInt16(DateTime.DaysInMonth(Convert.ToDateTime(mSecondDayInSecondMonthOfQuarterM24).Year, Convert.ToDateTime(mSecondDayInSecondMonthOfQuarterM24).Month));

                cmdExposedChildSecondMonthM24TI.Connection = mConn;
                cmdExposedChildSecondMonthM24TI.CommandText = "Select a.patientcode, a.transferindate, a.enrolmentdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_exposed a, patients p where a.patientcode = p.code and a.transferindate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mSecondDayInSecondMonthOfQuarterM24) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mSecondDayInSecondMonthOfQuarterM24.Year, mSecondDayInSecondMonthOfQuarterM24.Month) + " " + mTwentyFourMonthsExposedSecondMonth)) + " and a.modeofentry = 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mSecondDayInSecondMonthOfQuarterM24.Year, mSecondDayInSecondMonthOfQuarterM24.Month) + " " + mTwentyFourMonthsExposedSecondMonth)) + ", p.birthdate) < '728.5'";
                cmdExposedChildSecondMonthM24TI.ExecuteNonQuery();

                cmdExposedChildSecondMonthM24Other.Connection = mConn;
                cmdExposedChildSecondMonthM24Other.CommandText = "Select a.patientcode, a.transferindate, a.enrolmentdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_exposed a, patients p where a.patientcode = p.code and a.enrolmentdate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mSecondDayInSecondMonthOfQuarterM24) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mSecondDayInSecondMonthOfQuarterM24.Year, mSecondDayInSecondMonthOfQuarterM24.Month) + " " + mTwentyFourMonthsExposedSecondMonth)) + " and a.modeofentry <> 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mSecondDayInSecondMonthOfQuarterM24.Year, mSecondDayInSecondMonthOfQuarterM24.Month) + " " + mTwentyFourMonthsExposedSecondMonth)) + ", p.birthdate) < '728.5'";
                cmdExposedChildSecondMonthM24Other.ExecuteNonQuery();

                //clsGlobal.Write_Error(pClassName, mFunctionName, "Select a.patientcode, a.transferindate, a.dateofinitiation, a.confirmdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_preart a, patients p where a.patientcode = p.code and a.transferindate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mSecondDayInSecondMonthOfQuarterM24) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mSecondDayInSecondMonthOfQuarterM24.Year, mSecondDayInSecondMonthOfQuarterM24.Month) + " " + mTwentyFourMonthsExposedSecondMonth)) + " and a.modeofentry = 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mSecondDayInSecondMonthOfQuarterM24.Year, mSecondDayInSecondMonthOfQuarterM24.Month) + " " + mTwentyFourMonthsExposedSecondMonth)) + ", p.birthdate) < '728.5'");

                mDsExposedChild.Clear();
                mAdExposedChildUnder24MonthsTI.SelectCommand = cmdExposedChildSecondMonthM24TI;
                mAdExposedChildUnder24MonthsTI.Fill(mDsExposedChild);

                mAdExposedChildUnder24MonthsOther.SelectCommand = cmdExposedChildSecondMonthM24Other;
                mAdExposedChildUnder24MonthsOther.Fill(mDsExposedChild);

                mTotalRegistered24Months = mDsExposedChild.Rows.Count;


                foreach (DataRow myRow in (mDsExposedChild.Rows))
                {

                    String Outcome = myRow["latestoutcome"].ToString();
                    String EntryMode = myRow["modeofentry"].ToString();
                    String Gender = myRow["gender"].ToString();
                    TimeSpan Ts = (Convert.ToDateTime(mQuarterEndDate) - Convert.ToDateTime(myRow["birthdate"]));
                    double AgeInDays = Ts.Days;

                    string mPatientCode = myRow["patientcode"].ToString();
                    DateTime mFromBeginning = Convert.ToDateTime("1900/01/01");
                    OdbcCommand cmdPreARTExposedLog = new OdbcCommand();
                    cmdPreARTExposedLog.Connection = mConn;
                    cmdPreARTExposedLog.CommandText = "select * from ctc_exposedlog where patientcode ='" + mPatientCode + "'"; // and transdate between " + clsGlobal.Saving_DateValue(mQuarterStartDate) + " AND " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                    cmdPreARTExposedLog.ExecuteNonQuery();

                    OdbcDataAdapter adPreARTExposedLog = new OdbcDataAdapter();
                    DataTable tbPreARTExposedLog = new DataTable();

                    adPreARTExposedLog.SelectCommand = cmdPreARTExposedLog;
                    adPreARTExposedLog.Fill(tbPreARTExposedLog);

                    Int64 mAutoCode = 0;
                    string mHIVStatus = "";
                    Double mCPTTablets = 0;
                    Double mCPTMills = 0;

                    if (tbPreARTExposedLog.Rows.Count != 0)
                    {
                        foreach (DataRow myARTLogRow in tbPreARTExposedLog.Rows)
                        {
                            if (mAutoCode < Convert.ToInt64(myARTLogRow["autocode"]))
                            {
                                Outcome = Convert.ToString(myARTLogRow["outcomecode"]);
                                mAutoCode = Convert.ToInt64(myARTLogRow["autocode"]);
                                mHIVStatus = Convert.ToString(myARTLogRow["hivinfectioncode"]);
                                mCPTTablets = Convert.ToDouble(myARTLogRow["cpttablets"]);
                                mCPTMills = Convert.ToDouble(myARTLogRow["cptmills"]); 
                            }
                        }
                    }

                    if ((myRow["modeofentry"].ToString() == "TI") && (Convert.ToDateTime(myRow["transferindate"]) < mQuarterStartDate) && (AgeInDays >= 729))
                    {
                        OdbcCommand cmdPreARTAppointment = new OdbcCommand();
                        cmdPreARTAppointment.Connection = mConn;
                        cmdPreARTAppointment.CommandText = "select * from ctc_appointments where patientcode ='" + mPatientCode + "' and apptdate >= " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                        cmdPreARTAppointment.ExecuteNonQuery();

                        OdbcDataAdapter adPreARTAppointment = new OdbcDataAdapter();
                        DataTable tbPreARTAppointment = new DataTable();

                        adPreARTAppointment.SelectCommand = cmdPreARTAppointment;
                        adPreARTAppointment.Fill(tbPreARTAppointment);

                        if ((tbPreARTAppointment.Rows.Count == 0) && (Outcome == "Con") || (Outcome == "Def"))
                        {
                            Outcome = "Def";
                        }


                        DateTime mAppD;
                        Double mDtInDays = 0;

                        if ((tbPreARTAppointment.Rows.Count > 0))
                        {
                            foreach (DataRow myARTAppointmentRow in tbPreARTAppointment.Rows)
                            {
                                mAppD = Convert.ToDateTime(myARTAppointmentRow["apptdate"]);
                                TimeSpan mT = (mAppD - mQuarterEndDate);
                                mDtInDays = mT.Days;
                            }
                        }

                        if (mDtInDays > 60.83)
                        {
                            Outcome = "Def";
                        }


                    }


                    if ((myRow["modeofentry"].ToString() != "TI") && (Convert.ToDateTime(myRow["enrolmentdate"]) < mQuarterStartDate) && (AgeInDays >= 729))
                    {
                        OdbcCommand cmdPreARTAppointment = new OdbcCommand();
                        cmdPreARTAppointment.Connection = mConn;
                        cmdPreARTAppointment.CommandText = "select * from ctc_appointments where patientcode ='" + mPatientCode + "' and apptdate >= " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                        cmdPreARTAppointment.ExecuteNonQuery();

                        OdbcDataAdapter adPreARTAppointment = new OdbcDataAdapter();
                        DataTable tbPreARTAppointment = new DataTable();

                        adPreARTAppointment.SelectCommand = cmdPreARTAppointment;
                        adPreARTAppointment.Fill(tbPreARTAppointment);

                        if ((tbPreARTAppointment.Rows.Count == 0) && (Outcome == "Con") || (Outcome == "Def"))
                        {
                            Outcome = "Def";
                        }


                        DateTime mAppD;
                        Double mDtInDays = 0;

                        if ((tbPreARTAppointment.Rows.Count > 0))
                        {
                            foreach (DataRow myARTAppointmentRow in tbPreARTAppointment.Rows)
                            {
                                mAppD = Convert.ToDateTime(myARTAppointmentRow["apptdate"]);
                                TimeSpan mT = (mAppD - mQuarterEndDate);
                                mDtInDays = mT.Days;
                            }
                        }

                        if (mDtInDays > 60.83)
                        {
                            Outcome = "Def";
                        }


                    }

                    if ((mCPTTablets == 0) && (mCPTMills == 0))
                    {
                        mNotOnCPT24Months += 1;
                    }
                    else
                    {
                        mOnCPT24Months += 1;
                    }

                    if (mHIVStatus == "A")
                    {
                        mConfirmedUnInfected24Months += 1;
                    }

                    if (mHIVStatus == "B")
                    {
                        mConfirmedInfected24Months += 1;
                    }

                    if (mHIVStatus == "C")
                    {
                        mNotEligibleForART24Months += 1;
                    }

                    if (mHIVStatus == "D")
                    {
                        mNotConfirmed24Months += 1;
                    }

                    if (Outcome == "Con")
                    {
                        mCon24Months += 1;
                    }

                    if (Outcome == "D")
                    {
                        mD24Months += 1;
                    }

                    if (Outcome == "TO")
                    {
                        mTout24Months += 1;
                    }

                    if (Outcome == "Def")
                    {
                        mDef24Months += 1;
                    }

                    if (Outcome == "Dis")
                    {
                        mDis24Months += 1;
                    }

                }

                #endregion

                mDtExposedChildUnder24MonthsM2.Columns.Add("keyvalue", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("ClinicName", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("ReportingYear", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("ReportingMonth", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("TotalRegistered2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("TotalRegistered12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("TotalRegistered24Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("ConfirmedUnInfected2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("ConfirmedUnInfected12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("ConfirmedUnInfected24Months", typeof(System.String));

                mDtExposedChildUnder24MonthsM2.Columns.Add("ConfirmedInfected2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("ConfirmedInfected12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("ConfirmedInfected24Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("NotConfirmed2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("NotConfirmed12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("NotConfirmed24Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("NotEligibleForART2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("NotEligibleForART12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("NotEligibleForART24Months", typeof(System.String));

                mDtExposedChildUnder24MonthsM2.Columns.Add("OnCPT2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("OnCPT12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("OnCPT24Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("NotOnCPT2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("NotOnCPT12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("NotOnCPT24Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("Con2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("Con12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("Con24Months", typeof(System.String));

                mDtExposedChildUnder24MonthsM2.Columns.Add("Dis2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("Dis12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("Dis24Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("ART2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("ART12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("ART24Months", typeof(System.String));

                mDtExposedChildUnder24MonthsM2.Columns.Add("TO2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("TO12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("TO24Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("Def2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("Def12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("Def24Months", typeof(System.String));

                mDtExposedChildUnder24MonthsM2.Columns.Add("D2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("D12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM2.Columns.Add("D24Months", typeof(System.String));



                mDtExposedChildUnder24MonthsM2.AcceptChanges();

                mDtExposedChildUnder24MonthsM2.Rows.Add("parentlink", " ", mYearPart, mSecondMonthInQuarter, mTotalRegistered2Months, mTotalRegistered12Months, mTotalRegistered24Months, mConfirmedUnInfected2Months, mConfirmedUnInfected12Months, mConfirmedUnInfected24Months, mConfirmedInfected2Months, mConfirmedInfected12Months, mConfirmedInfected24Months, mNotConfirmed2Months, mNotConfirmed12Months, mNotConfirmed24Months, mNotEligibleForART2Months, mNotEligibleForART12Months, mNotEligibleForART24Months, mOnCPT2Months, mOnCPT12Months, mOnCPT24Months, mNotOnCPT2Months, mNotOnCPT12Months, mNotOnCPT24Months, mCon2Months, mCon12Months, mCon24Months, mDis2Months, mDis12Months, mDis24Months, mART2Months, mART12Months, mART24Months, mTout2Months, mTout12Months, mTout24Months, mDef2Months, mDef12Months, mDef24Months, mD2Months, mD12Months, mD24Months);

                mDtExposedChildUnder24MonthsM2.AcceptChanges();

                mTotalRegistered2Months = 0;
                mTotalRegistered12Months = 0;
                mTotalRegistered24Months = 0;

                mConfirmedUnInfected2Months = 0;
                mConfirmedUnInfected12Months = 0;
                mConfirmedUnInfected24Months = 0; ;
                mConfirmedInfected2Months = 0;
                mConfirmedInfected12Months = 0;
                mConfirmedInfected24Months = 0;
                mNotEligibleForART2Months = 0;
                mNotEligibleForART12Months = 0;
                mNotEligibleForART24Months = 0;
                mNotConfirmed2Months = 0;
                mNotConfirmed12Months = 0;
                mNotConfirmed24Months = 0;
                mOnCPT2Months = 0;
                mOnCPT12Months = 0;
                mOnCPT24Months = 0;
                mNotOnCPT2Months = 0;
                mNotOnCPT12Months = 0;
                mNotOnCPT24Months = 0;
                mCon2Months = 0;
                mCon12Months = 0;
                mCon24Months = 0;
                mDis2Months = 0;
                mDis12Months = 0;
                mDis24Months = 0;
                mART2Months = 0;
                mART12Months = 0;
                mART24Months = 0;
                mTout2Months = 0;
                mTout12Months = 0;
                mTout24Months = 0;
                mDef2Months = 0;
                mDef12Months = 0;
                mDef24Months = 0;
                mD2Months = 0;
                mD12Months = 0;
                mD24Months = 0;
                



                #endregion

                #region ThirdMonth


                #region ThirdMonth_TwoMonths Cohort
                mFunctionName = "ExposedChildUnder24Months - ThirdMonth_TwoMonths Cohort";
                OdbcCommand cmdExposedChildThirdMonthTI = new OdbcCommand();
                OdbcCommand cmdExposedChildThirdMonthOther = new OdbcCommand();
                DateTime mFirstDayInThirdMonthOfQuarter = Convert.ToDateTime("1 " + mTwoMonthsExposedThirdMonth + " " + mTwoMonthsYear);
                Int16 mNumberOfDaysInThirdMonth = Convert.ToInt16(DateTime.DaysInMonth(Convert.ToDateTime(mFirstDayInThirdMonthOfQuarter).Year, Convert.ToDateTime(mFirstDayInThirdMonthOfQuarter).Month));

                cmdExposedChildThirdMonthTI.Connection = mConn;
                cmdExposedChildThirdMonthTI.CommandText = "Select a.patientcode, a.transferindate, a.enrolmentdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_exposed a, patients p where a.patientcode = p.code and a.transferindate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mFirstDayInThirdMonthOfQuarter) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mNumberOfDaysInThirdMonth + " " + mTwoMonthsExposedThirdMonth + " " + mTwoMonthsYear)) + " and a.modeofentry = 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(mNumberOfDaysInThirdMonth + " " + mTwoMonthsExposedThirdMonth + " " + mTwoMonthsYear)) + ", p.birthdate) < '728.5'";
                cmdExposedChildThirdMonthTI.ExecuteNonQuery();

                cmdExposedChildThirdMonthOther.Connection = mConn;
                cmdExposedChildThirdMonthOther.CommandText = "Select a.patientcode, a.transferindate, a.enrolmentdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_exposed a, patients p where a.patientcode = p.code and a.enrolmentdate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mFirstDayInThirdMonthOfQuarter) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mNumberOfDaysInThirdMonth + " " + mTwoMonthsExposedThirdMonth + " " + mTwoMonthsYear)) + " and a.modeofentry <> 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(mNumberOfDaysInThirdMonth + " " + mTwoMonthsExposedThirdMonth + " " + mTwoMonthsYear)) + ", p.birthdate) < '728.5'";
                cmdExposedChildThirdMonthOther.ExecuteNonQuery();

                // clsGlobal.Write_Error(pClassName, mFunctionName, "Select a.patientcode, a.transferindate, a.dateofinitiation, a.confirmdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_preart a, patients p where a.patientcode = p.code and a.dateofinitiation between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mThirdDayInThirdMonthOfQuarter) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mThirdDayInThirdMonthOfQuarter.Year, mThirdDayInThirdMonthOfQuarter.Month) + mThirdMonth)) + " and a.modeofentry <> 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mThirdDayInThirdMonthOfQuarter.Year, mThirdDayInThirdMonthOfQuarter.Month) + " " + mTwoMonthsExposedThirdMonth + " " + mTwoMonthsYear)) + ", p.birthdate) < '728.5'");

                mDsExposedChild.Clear();
                mAdExposedChildUnder24MonthsTI.SelectCommand = cmdExposedChildThirdMonthTI;
                mAdExposedChildUnder24MonthsTI.Fill(mDsExposedChild);

                mAdExposedChildUnder24MonthsOther.SelectCommand = cmdExposedChildThirdMonthOther;
                mAdExposedChildUnder24MonthsOther.Fill(mDsExposedChild);

                mTotalRegistered2Months = mDsExposedChild.Rows.Count;


                foreach (DataRow myRow in (mDsExposedChild.Rows))
                {

                    String Outcome = myRow["latestoutcome"].ToString();
                    String EntryMode = myRow["modeofentry"].ToString();
                    String Gender = myRow["gender"].ToString();
                    TimeSpan Ts = (Convert.ToDateTime(mQuarterEndDate) - Convert.ToDateTime(myRow["birthdate"]));
                    double AgeInDays = Ts.Days;

                    string mPatientCode = myRow["patientcode"].ToString();
                    DateTime mFromBeginning = Convert.ToDateTime("1900/01/01");
                    OdbcCommand cmdPreARTExposedLog = new OdbcCommand();
                    cmdPreARTExposedLog.Connection = mConn;
                    cmdPreARTExposedLog.CommandText = "select * from ctc_exposedlog where patientcode ='" + mPatientCode + "'"; // and transdate between " + clsGlobal.Saving_DateValue(mQuarterStartDate) + " AND " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                    cmdPreARTExposedLog.ExecuteNonQuery();

                    OdbcDataAdapter adPreARTExposedLog = new OdbcDataAdapter();
                    DataTable tbPreARTExposedLog = new DataTable();

                    adPreARTExposedLog.SelectCommand = cmdPreARTExposedLog;
                    adPreARTExposedLog.Fill(tbPreARTExposedLog);

                    Int64 mAutoCode = 0;
                    string mHIVStatus = "";
                    Double mCPTTablets = 0;
                    Double mCPTMills = 0;
                    if (tbPreARTExposedLog.Rows.Count != 0)
                    {
                        foreach (DataRow myARTLogRow in tbPreARTExposedLog.Rows)
                        {
                            if (mAutoCode < Convert.ToInt64(myARTLogRow["autocode"]))
                            {
                                Outcome = Convert.ToString(myARTLogRow["outcomecode"]);
                                mAutoCode = Convert.ToInt64(myARTLogRow["autocode"]);
                                mHIVStatus = Convert.ToString(myARTLogRow["hivinfectioncode"]);
                                mCPTTablets = Convert.ToDouble(myARTLogRow["cpttablets"]);
                                mCPTMills = Convert.ToDouble(myARTLogRow["cptmills"]); 
                            }
                        }
                    }

                    if ((myRow["modeofentry"].ToString() == "TI") && (Convert.ToDateTime(myRow["transferindate"]) < mQuarterStartDate) && (AgeInDays >= 729))
                    {
                        OdbcCommand cmdPreARTAppointment = new OdbcCommand();
                        cmdPreARTAppointment.Connection = mConn;
                        cmdPreARTAppointment.CommandText = "select * from ctc_appointments where patientcode ='" + mPatientCode + "' and apptdate >= " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                        cmdPreARTAppointment.ExecuteNonQuery();

                        OdbcDataAdapter adPreARTAppointment = new OdbcDataAdapter();
                        DataTable tbPreARTAppointment = new DataTable();

                        adPreARTAppointment.SelectCommand = cmdPreARTAppointment;
                        adPreARTAppointment.Fill(tbPreARTAppointment);

                        if ((tbPreARTAppointment.Rows.Count == 0) && (Outcome == "Con") || (Outcome == "Def"))
                        {
                            Outcome = "Def";
                        }


                        DateTime mAppD;
                        Double mDtInDays = 0;

                        if ((tbPreARTAppointment.Rows.Count > 0))
                        {
                            foreach (DataRow myARTAppointmentRow in tbPreARTAppointment.Rows)
                            {
                                mAppD = Convert.ToDateTime(myARTAppointmentRow["apptdate"]);
                                TimeSpan mT = (mAppD - mQuarterEndDate);
                                mDtInDays = mT.Days;
                            }
                        }

                        if (mDtInDays > 60.83)
                        {
                            Outcome = "Def";
                        }


                    }


                    if ((myRow["modeofentry"].ToString() != "TI") && (Convert.ToDateTime(myRow["enrolmentdate"]) < mQuarterStartDate) && (AgeInDays >= 729))
                    {
                        OdbcCommand cmdPreARTAppointment = new OdbcCommand();
                        cmdPreARTAppointment.Connection = mConn;
                        cmdPreARTAppointment.CommandText = "select * from ctc_appointments where patientcode ='" + mPatientCode + "' and apptdate >= " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                        cmdPreARTAppointment.ExecuteNonQuery();

                        OdbcDataAdapter adPreARTAppointment = new OdbcDataAdapter();
                        DataTable tbPreARTAppointment = new DataTable();

                        adPreARTAppointment.SelectCommand = cmdPreARTAppointment;
                        adPreARTAppointment.Fill(tbPreARTAppointment);

                        if ((tbPreARTAppointment.Rows.Count == 0) && (Outcome == "Con") || (Outcome == "Def"))
                        {
                            Outcome = "Def";
                        }


                        DateTime mAppD;
                        Double mDtInDays = 0;

                        if ((tbPreARTAppointment.Rows.Count > 0))
                        {
                            foreach (DataRow myARTAppointmentRow in tbPreARTAppointment.Rows)
                            {
                                mAppD = Convert.ToDateTime(myARTAppointmentRow["apptdate"]);
                                TimeSpan mT = (mAppD - mQuarterEndDate);
                                mDtInDays = mT.Days;
                            }
                        }

                        if (mDtInDays > 60.83)
                        {
                            Outcome = "Def";
                        }


                    }

                    if ((mCPTTablets == 0) && (mCPTMills == 0))
                    {
                        mNotOnCPT2Months += 1;
                    }
                    else
                    {
                        mOnCPT2Months += 1;
                    }

                    if (mHIVStatus == "A")
                    {
                        mConfirmedUnInfected2Months += 1;
                    }

                    if (mHIVStatus == "B")
                    {
                        mConfirmedInfected2Months += 1;
                    }

                    if (mHIVStatus == "C")
                    {
                        mNotEligibleForART2Months += 1;
                    }

                    if (mHIVStatus == "D")
                    {
                        mNotConfirmed2Months += 1;
                    }

                    if (Outcome == "Con")
                    {
                        mCon2Months += 1;
                    }

                    if (Outcome == "D")
                    {
                        mD2Months += 1;
                    }

                    if (Outcome == "TO")
                    {
                        mTout2Months += 1;
                    }

                    if (Outcome == "Def")
                    {
                        mDef2Months += 1;
                    }

                    if (Outcome == "Dis")
                    {
                        mDis2Months += 1;
                    }

                }


                #endregion

                #region ThirdMonth_12Months Cohort
                mFunctionName = "ExposedChildUnder24Months - ThirdMonth_12Months Cohort";
                OdbcCommand cmdExposedChildThirdMonthM12TI = new OdbcCommand();
                OdbcCommand cmdExposedChildThirdMonthM12Other = new OdbcCommand();
                DateTime mThirdDayInThirdMonthOfQuarterM12 = Convert.ToDateTime("1 " + mTwelveMonthsExposedThirdMonth);
                Int16 mNumberOfDaysInThirdMonth12 = Convert.ToInt16(DateTime.DaysInMonth(Convert.ToDateTime(mThirdDayInThirdMonthOfQuarterM12).Year, Convert.ToDateTime(mThirdDayInThirdMonthOfQuarterM12).Month));

                cmdExposedChildThirdMonthM12TI.Connection = mConn;
                cmdExposedChildThirdMonthM12TI.CommandText = "Select a.patientcode, a.transferindate, a.enrolmentdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_exposed a, patients p where a.patientcode = p.code and a.transferindate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mThirdDayInThirdMonthOfQuarterM12) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mThirdDayInThirdMonthOfQuarterM12.Year, mThirdDayInThirdMonthOfQuarterM12.Month) + " " + mTwelveMonthsExposedThirdMonth)) + " and a.modeofentry = 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mThirdDayInThirdMonthOfQuarterM12.Year, mThirdDayInThirdMonthOfQuarterM12.Month) + " " + mTwelveMonthsExposedThirdMonth)) + ", p.birthdate) < '728.5'";
                cmdExposedChildThirdMonthM12TI.ExecuteNonQuery();

                cmdExposedChildThirdMonthM12Other.Connection = mConn;
                cmdExposedChildThirdMonthM12Other.CommandText = "Select a.patientcode, a.transferindate, a.enrolmentdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_exposed a, patients p where a.patientcode = p.code and a.enrolmentdate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mThirdDayInThirdMonthOfQuarterM12) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mThirdDayInThirdMonthOfQuarterM12.Year, mThirdDayInThirdMonthOfQuarterM12.Month) + " " + mTwelveMonthsExposedThirdMonth)) + " and a.modeofentry <> 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mThirdDayInThirdMonthOfQuarterM12.Year, mThirdDayInThirdMonthOfQuarterM12.Month) + " " + mTwelveMonthsExposedThirdMonth)) + ", p.birthdate) < '728.5'";
                cmdExposedChildThirdMonthM12Other.ExecuteNonQuery();

                //clsGlobal.Write_Error(pClassName, mFunctionName, "Select a.patientcode, a.transferindate, a.dateofinitiation, a.confirmdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_preart a, patients p where a.patientcode = p.code and a.dateofinitiation between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mThirdDayInThirdMonthOfQuarterM12) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mThirdDayInThirdMonthOfQuarterM12.Year, mThirdDayInThirdMonthOfQuarterM12.Month) + " " + mTwelveMonthsExposedThirdMonth)) + " and a.modeofentry <> 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mThirdDayInThirdMonthOfQuarterM12.Year, mThirdDayInThirdMonthOfQuarterM12.Month) + " " + mTwelveMonthsExposedThirdMonth)) + ", p.birthdate) < '728.5'");

                mDsExposedChild.Clear();
                mAdExposedChildUnder24MonthsTI.SelectCommand = cmdExposedChildThirdMonthM12TI;
                mAdExposedChildUnder24MonthsTI.Fill(mDsExposedChild);

                mAdExposedChildUnder24MonthsOther.SelectCommand = cmdExposedChildThirdMonthM12Other;
                mAdExposedChildUnder24MonthsOther.Fill(mDsExposedChild);

                mTotalRegistered12Months = mDsExposedChild.Rows.Count;


                foreach (DataRow myRow in (mDsExposedChild.Rows))
                {

                    String Outcome = myRow["latestoutcome"].ToString();
                    String EntryMode = myRow["modeofentry"].ToString();
                    String Gender = myRow["gender"].ToString();
                    TimeSpan Ts = (Convert.ToDateTime(mQuarterEndDate) - Convert.ToDateTime(myRow["birthdate"]));
                    double AgeInDays = Ts.Days;

                    string mPatientCode = myRow["patientcode"].ToString();
                    DateTime mFromBeginning = Convert.ToDateTime("1900/01/01");
                    OdbcCommand cmdPreARTExposedLog = new OdbcCommand();
                    cmdPreARTExposedLog.Connection = mConn;
                    cmdPreARTExposedLog.CommandText = "select * from ctc_exposedlog where patientcode ='" + mPatientCode + "'"; // and transdate between " + clsGlobal.Saving_DateValue(mQuarterStartDate) + " AND " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                    cmdPreARTExposedLog.ExecuteNonQuery();

                    OdbcDataAdapter adPreARTExposedLog = new OdbcDataAdapter();
                    DataTable tbPreARTExposedLog = new DataTable();

                    adPreARTExposedLog.SelectCommand = cmdPreARTExposedLog;
                    adPreARTExposedLog.Fill(tbPreARTExposedLog);

                    Int64 mAutoCode = 0;
                    string mHIVStatus = "";
                    Double mCPTTablets = 0;
                    Double mCPTMills = 0;

                    if (tbPreARTExposedLog.Rows.Count != 0)
                    {
                        foreach (DataRow myARTLogRow in tbPreARTExposedLog.Rows)
                        {
                            if (mAutoCode < Convert.ToInt64(myARTLogRow["autocode"]))
                            {
                                Outcome = Convert.ToString(myARTLogRow["outcomecode"]);
                                mAutoCode = Convert.ToInt64(myARTLogRow["autocode"]);
                                mHIVStatus = Convert.ToString(myARTLogRow["hivinfectioncode"]);
                                mCPTTablets = Convert.ToDouble(myARTLogRow["cpttablets"]);
                                mCPTMills = Convert.ToDouble(myARTLogRow["cptmills"]); 
                            }
                        }
                    }

                    if ((myRow["modeofentry"].ToString() == "TI") && (Convert.ToDateTime(myRow["transferindate"]) < mQuarterStartDate) && (AgeInDays >= 729))
                    {
                        OdbcCommand cmdPreARTAppointment = new OdbcCommand();
                        cmdPreARTAppointment.Connection = mConn;
                        cmdPreARTAppointment.CommandText = "select * from ctc_appointments where patientcode ='" + mPatientCode + "' and apptdate >= " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                        cmdPreARTAppointment.ExecuteNonQuery();

                        OdbcDataAdapter adPreARTAppointment = new OdbcDataAdapter();
                        DataTable tbPreARTAppointment = new DataTable();

                        adPreARTAppointment.SelectCommand = cmdPreARTAppointment;
                        adPreARTAppointment.Fill(tbPreARTAppointment);

                        if ((tbPreARTAppointment.Rows.Count == 0) && (Outcome == "Con") || (Outcome == "Def"))
                        {
                            Outcome = "Def";
                        }


                        DateTime mAppD;
                        Double mDtInDays = 0;

                        if ((tbPreARTAppointment.Rows.Count > 0))
                        {
                            foreach (DataRow myARTAppointmentRow in tbPreARTAppointment.Rows)
                            {
                                mAppD = Convert.ToDateTime(myARTAppointmentRow["apptdate"]);
                                TimeSpan mT = (mAppD - mQuarterEndDate);
                                mDtInDays = mT.Days;
                            }
                        }

                        if (mDtInDays > 60.83)
                        {
                            Outcome = "Def";
                        }


                    }


                    if ((myRow["modeofentry"].ToString() != "TI") && (Convert.ToDateTime(myRow["enrolmentdate"]) < mQuarterStartDate) && (AgeInDays >= 729))
                    {
                        OdbcCommand cmdPreARTAppointment = new OdbcCommand();
                        cmdPreARTAppointment.Connection = mConn;
                        cmdPreARTAppointment.CommandText = "select * from ctc_appointments where patientcode ='" + mPatientCode + "' and apptdate >= " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                        cmdPreARTAppointment.ExecuteNonQuery();

                        OdbcDataAdapter adPreARTAppointment = new OdbcDataAdapter();
                        DataTable tbPreARTAppointment = new DataTable();

                        adPreARTAppointment.SelectCommand = cmdPreARTAppointment;
                        adPreARTAppointment.Fill(tbPreARTAppointment);

                        if ((tbPreARTAppointment.Rows.Count == 0) && (Outcome == "Con") || (Outcome == "Def"))
                        {
                            Outcome = "Def";
                        }


                        DateTime mAppD;
                        Double mDtInDays = 0;

                        if ((tbPreARTAppointment.Rows.Count > 0))
                        {
                            foreach (DataRow myARTAppointmentRow in tbPreARTAppointment.Rows)
                            {
                                mAppD = Convert.ToDateTime(myARTAppointmentRow["apptdate"]);
                                TimeSpan mT = (mAppD - mQuarterEndDate);
                                mDtInDays = mT.Days;
                            }
                        }

                        if (mDtInDays > 60.83)
                        {
                            Outcome = "Def";
                        }


                    }

                    if ((mCPTTablets == 0) && (mCPTMills == 0))
                    {
                        mNotOnCPT12Months += 1;
                    }
                    else
                    {
                        mOnCPT12Months += 1;
                    }

                    if (mHIVStatus == "A")
                    {
                        mConfirmedUnInfected12Months += 1;
                    }

                    if (mHIVStatus == "B")
                    {
                        mConfirmedInfected12Months += 1;
                    }

                    if (mHIVStatus == "C")
                    {
                        mNotEligibleForART12Months += 1;
                    }

                    if (mHIVStatus == "D")
                    {
                        mNotConfirmed12Months += 1;
                    }

                    if (Outcome == "Con")
                    {
                        mCon12Months += 1;
                    }

                    if (Outcome == "D")
                    {
                        mD12Months += 1;
                    }

                    if (Outcome == "TO")
                    {
                        mTout12Months += 1;
                    }

                    if (Outcome == "Def")
                    {
                        mDef12Months += 1;
                    }

                    if (Outcome == "Dis")
                    {
                        mDis12Months += 1;
                    }

                }

                #endregion

                #region ThirdMonth_24Months Cohort
                mFunctionName = "ExposedChildUnder24Months - ThirdMonth_24Months Cohort";
                OdbcCommand cmdExposedChildThirdMonthM24TI = new OdbcCommand();
                OdbcCommand cmdExposedChildThirdMonthM24Other = new OdbcCommand();
                DateTime mThirdDayInThirdMonthOfQuarterM24 = Convert.ToDateTime("1 " + mTwentyFourMonthsExposedThirdMonth);
                Int16 mNumberOfDaysInThirdMonth24 = Convert.ToInt16(DateTime.DaysInMonth(Convert.ToDateTime(mThirdDayInThirdMonthOfQuarterM24).Year, Convert.ToDateTime(mThirdDayInThirdMonthOfQuarterM24).Month));

                cmdExposedChildThirdMonthM24TI.Connection = mConn;
                cmdExposedChildThirdMonthM24TI.CommandText = "Select a.patientcode, a.transferindate, a.enrolmentdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_exposed a, patients p where a.patientcode = p.code and a.transferindate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mThirdDayInThirdMonthOfQuarterM24) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mThirdDayInThirdMonthOfQuarterM24.Year, mThirdDayInThirdMonthOfQuarterM24.Month) + " " + mTwentyFourMonthsExposedThirdMonth)) + " and a.modeofentry = 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mThirdDayInThirdMonthOfQuarterM24.Year, mThirdDayInThirdMonthOfQuarterM24.Month) + " " + mTwentyFourMonthsExposedThirdMonth)) + ", p.birthdate) < '728.5'";
                cmdExposedChildThirdMonthM24TI.ExecuteNonQuery();

                cmdExposedChildThirdMonthM24Other.Connection = mConn;
                cmdExposedChildThirdMonthM24Other.CommandText = "Select a.patientcode, a.transferindate, a.enrolmentdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_exposed a, patients p where a.patientcode = p.code and a.enrolmentdate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mThirdDayInThirdMonthOfQuarterM24) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mThirdDayInThirdMonthOfQuarterM24.Year, mThirdDayInThirdMonthOfQuarterM24.Month) + " " + mTwentyFourMonthsExposedThirdMonth)) + " and a.modeofentry <> 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mThirdDayInThirdMonthOfQuarterM24.Year, mThirdDayInThirdMonthOfQuarterM24.Month) + " " + mTwentyFourMonthsExposedThirdMonth)) + ", p.birthdate) < '728.5'";
                cmdExposedChildThirdMonthM24Other.ExecuteNonQuery();

                //clsGlobal.Write_Error(pClassName, mFunctionName, "Select a.patientcode, a.transferindate, a.dateofinitiation, a.confirmdate, a.modeofentry, a.latestoutcome, p.birthdate, p.gender from ctc_preart a, patients p where a.patientcode = p.code and a.transferindate between " + clsGlobal.Saving_DateValue(Convert.ToDateTime(mQuarterStartDate)) + " and " + clsGlobal.Saving_DateValue(mQuarterEndDate) + " AND p.birthdate BETWEEN " + clsGlobal.Saving_DateValue(mThirdDayInThirdMonthOfQuarterM24) + " AND " + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mThirdDayInThirdMonthOfQuarterM24.Year, mThirdDayInThirdMonthOfQuarterM24.Month) + " " + mTwentyFourMonthsExposedThirdMonth)) + " and a.modeofentry = 'TI' AND DateDiff(" + clsGlobal.Saving_DateValue(Convert.ToDateTime(DateTime.DaysInMonth(mThirdDayInThirdMonthOfQuarterM24.Year, mThirdDayInThirdMonthOfQuarterM24.Month) + " " + mTwentyFourMonthsExposedThirdMonth)) + ", p.birthdate) < '728.5'");

                mDsExposedChild.Clear();
                mAdExposedChildUnder24MonthsTI.SelectCommand = cmdExposedChildThirdMonthM24TI;
                mAdExposedChildUnder24MonthsTI.Fill(mDsExposedChild);

                mAdExposedChildUnder24MonthsOther.SelectCommand = cmdExposedChildThirdMonthM24Other;
                mAdExposedChildUnder24MonthsOther.Fill(mDsExposedChild);

                mTotalRegistered24Months = mDsExposedChild.Rows.Count;


                foreach (DataRow myRow in (mDsExposedChild.Rows))
                {

                    String Outcome = myRow["latestoutcome"].ToString();
                    String EntryMode = myRow["modeofentry"].ToString();
                    String Gender = myRow["gender"].ToString();
                    TimeSpan Ts = (Convert.ToDateTime(mQuarterEndDate) - Convert.ToDateTime(myRow["birthdate"]));
                    double AgeInDays = Ts.Days;

                    string mPatientCode = myRow["patientcode"].ToString();
                    DateTime mFromBeginning = Convert.ToDateTime("1900/01/01");
                    OdbcCommand cmdPreARTExposedLog = new OdbcCommand();
                    cmdPreARTExposedLog.Connection = mConn;
                    cmdPreARTExposedLog.CommandText = "select * from ctc_exposedlog where patientcode ='" + mPatientCode + "'"; // and transdate between " + clsGlobal.Saving_DateValue(mQuarterStartDate) + " AND " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                    cmdPreARTExposedLog.ExecuteNonQuery();

                    OdbcDataAdapter adPreARTExposedLog = new OdbcDataAdapter();
                    DataTable tbPreARTExposedLog = new DataTable();

                    adPreARTExposedLog.SelectCommand = cmdPreARTExposedLog;
                    adPreARTExposedLog.Fill(tbPreARTExposedLog);

                    Int64 mAutoCode = 0;
                    string mHIVStatus = "";
                    Double mCPTTablets = 0;
                    Double mCPTMills = 0;

                    if (tbPreARTExposedLog.Rows.Count != 0)
                    {
                        foreach (DataRow myARTLogRow in tbPreARTExposedLog.Rows)
                        {
                            if (mAutoCode < Convert.ToInt64(myARTLogRow["autocode"]))
                            {
                                Outcome = Convert.ToString(myARTLogRow["outcomecode"]);
                                mAutoCode = Convert.ToInt64(myARTLogRow["autocode"]);
                                mHIVStatus = Convert.ToString(myARTLogRow["hivinfectioncode"]);
                                mCPTTablets = Convert.ToDouble(myARTLogRow["cpttablets"]);
                                mCPTMills = Convert.ToDouble(myARTLogRow["cptmills"]); 
                            }
                        }
                    }

                    if ((myRow["modeofentry"].ToString() == "TI") && (Convert.ToDateTime(myRow["transferindate"]) < mQuarterStartDate) && (AgeInDays >= 729))
                    {
                        OdbcCommand cmdPreARTAppointment = new OdbcCommand();
                        cmdPreARTAppointment.Connection = mConn;
                        cmdPreARTAppointment.CommandText = "select * from ctc_appointments where patientcode ='" + mPatientCode + "' and apptdate >= " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                        cmdPreARTAppointment.ExecuteNonQuery();

                        OdbcDataAdapter adPreARTAppointment = new OdbcDataAdapter();
                        DataTable tbPreARTAppointment = new DataTable();

                        adPreARTAppointment.SelectCommand = cmdPreARTAppointment;
                        adPreARTAppointment.Fill(tbPreARTAppointment);

                        if ((tbPreARTAppointment.Rows.Count == 0) && (Outcome == "Con") || (Outcome == "Def"))
                        {
                            Outcome = "Def";
                        }


                        DateTime mAppD;
                        Double mDtInDays = 0;

                        if ((tbPreARTAppointment.Rows.Count > 0))
                        {
                            foreach (DataRow myARTAppointmentRow in tbPreARTAppointment.Rows)
                            {
                                mAppD = Convert.ToDateTime(myARTAppointmentRow["apptdate"]);
                                TimeSpan mT = (mAppD - mQuarterEndDate);
                                mDtInDays = mT.Days;
                            }
                        }

                        if (mDtInDays > 60.83)
                        {
                            Outcome = "Def";
                        }


                    }


                    if ((myRow["modeofentry"].ToString() != "TI") && (Convert.ToDateTime(myRow["enrolmentdate"]) < mQuarterStartDate) && (AgeInDays >= 729))
                    {
                        OdbcCommand cmdPreARTAppointment = new OdbcCommand();
                        cmdPreARTAppointment.Connection = mConn;
                        cmdPreARTAppointment.CommandText = "select * from ctc_appointments where patientcode ='" + mPatientCode + "' and apptdate >= " + clsGlobal.Saving_DateValue(mQuarterEndDate);
                        cmdPreARTAppointment.ExecuteNonQuery();

                        OdbcDataAdapter adPreARTAppointment = new OdbcDataAdapter();
                        DataTable tbPreARTAppointment = new DataTable();

                        adPreARTAppointment.SelectCommand = cmdPreARTAppointment;
                        adPreARTAppointment.Fill(tbPreARTAppointment);

                        if ((tbPreARTAppointment.Rows.Count == 0) && (Outcome == "Con") || (Outcome == "Def"))
                        {
                            Outcome = "Def";
                        }


                        DateTime mAppD;
                        Double mDtInDays = 0;

                        if ((tbPreARTAppointment.Rows.Count > 0))
                        {
                            foreach (DataRow myARTAppointmentRow in tbPreARTAppointment.Rows)
                            {
                                mAppD = Convert.ToDateTime(myARTAppointmentRow["apptdate"]);
                                TimeSpan mT = (mAppD - mQuarterEndDate);
                                mDtInDays = mT.Days;
                            }
                        }

                        if (mDtInDays > 60.83)
                        {
                            Outcome = "Def";
                        }


                    }

                    if ((mCPTTablets == 0) && (mCPTMills == 0))
                    {
                        mNotOnCPT24Months += 1;
                    }
                    else
                    {
                        mOnCPT24Months += 1;
                    }

                    if (mHIVStatus == "A")
                    {
                        mConfirmedUnInfected24Months += 1;
                    }

                    if (mHIVStatus == "B")
                    {
                        mConfirmedInfected24Months += 1;
                    }

                    if (mHIVStatus == "C")
                    {
                        mNotEligibleForART24Months += 1;
                    }

                    if (mHIVStatus == "D")
                    {
                        mNotConfirmed24Months += 1;
                    }

                    if (Outcome == "Con")
                    {
                        mCon24Months += 1;
                    }

                    if (Outcome == "D")
                    {
                        mD24Months += 1;
                    }

                    if (Outcome == "TO")
                    {
                        mTout24Months += 1;
                    }

                    if (Outcome == "Def")
                    {
                        mDef24Months += 1;
                    }

                    if (Outcome == "Dis")
                    {
                        mDis24Months += 1;
                    }

                }

                #endregion

                mDtExposedChildUnder24MonthsM3.Columns.Add("keyvalue", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("ClinicName", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("ReportingYear", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("ReportingMonth", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("TotalRegistered2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("TotalRegistered12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("TotalRegistered24Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("ConfirmedUnInfected2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("ConfirmedUnInfected12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("ConfirmedUnInfected24Months", typeof(System.String));

                mDtExposedChildUnder24MonthsM3.Columns.Add("ConfirmedInfected2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("ConfirmedInfected12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("ConfirmedInfected24Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("NotConfirmed2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("NotConfirmed12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("NotConfirmed24Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("NotEligibleForART2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("NotEligibleForART12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("NotEligibleForART24Months", typeof(System.String));

                mDtExposedChildUnder24MonthsM3.Columns.Add("OnCPT2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("OnCPT12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("OnCPT24Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("NotOnCPT2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("NotOnCPT12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("NotOnCPT24Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("Con2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("Con12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("Con24Months", typeof(System.String));

                mDtExposedChildUnder24MonthsM3.Columns.Add("Dis2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("Dis12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("Dis24Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("ART2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("ART12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("ART24Months", typeof(System.String));

                mDtExposedChildUnder24MonthsM3.Columns.Add("TO2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("TO12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("TO24Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("Def2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("Def12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("Def24Months", typeof(System.String));

                mDtExposedChildUnder24MonthsM3.Columns.Add("D2Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("D12Months", typeof(System.String));
                mDtExposedChildUnder24MonthsM3.Columns.Add("D24Months", typeof(System.String));



                mDtExposedChildUnder24MonthsM3.AcceptChanges();

                mDtExposedChildUnder24MonthsM3.Rows.Add("parentlink", " ", mYearPart, mThirdMonthInQuarter, mTotalRegistered2Months, mTotalRegistered12Months, mTotalRegistered24Months, mConfirmedUnInfected2Months, mConfirmedUnInfected12Months, mConfirmedUnInfected24Months, mConfirmedInfected2Months, mConfirmedInfected12Months, mConfirmedInfected24Months, mNotConfirmed2Months, mNotConfirmed12Months, mNotConfirmed24Months, mNotEligibleForART2Months, mNotEligibleForART12Months, mNotEligibleForART24Months, mOnCPT2Months, mOnCPT12Months, mOnCPT24Months, mNotOnCPT2Months, mNotOnCPT12Months, mNotOnCPT24Months, mCon2Months, mCon12Months, mCon24Months, mDis2Months, mDis12Months, mDis24Months, mART2Months, mART12Months, mART24Months, mTout2Months, mTout12Months, mTout24Months, mDef2Months, mDef12Months, mDef24Months, mD2Months, mD12Months, mD24Months);

                mDtExposedChildUnder24MonthsM3.AcceptChanges();

                mTotalRegistered2Months = 0;
                mTotalRegistered12Months = 0;
                mTotalRegistered24Months = 0;

                mConfirmedUnInfected2Months = 0;
                mConfirmedUnInfected12Months = 0;
                mConfirmedUnInfected24Months = 0; ;
                mConfirmedInfected2Months = 0;
                mConfirmedInfected12Months = 0;
                mConfirmedInfected24Months = 0;
                mNotEligibleForART2Months = 0;
                mNotEligibleForART12Months = 0;
                mNotEligibleForART24Months = 0;
                mNotConfirmed2Months = 0;
                mNotConfirmed12Months = 0;
                mNotConfirmed24Months = 0;
                mOnCPT2Months = 0;
                mOnCPT12Months = 0;
                mOnCPT24Months = 0;
                mNotOnCPT2Months = 0;
                mNotOnCPT12Months = 0;
                mNotOnCPT24Months = 0;
                mCon2Months = 0;
                mCon12Months = 0;
                mCon24Months = 0;
                mDis2Months = 0;
                mDis12Months = 0;
                mDis24Months = 0;
                mART2Months = 0;
                mART12Months = 0;
                mART24Months = 0;
                mTout2Months = 0;
                mTout12Months = 0;
                mTout24Months = 0;
                mDef2Months = 0;
                mDef12Months = 0;
                mDef24Months = 0;
                mD2Months = 0;
                mD12Months = 0;
                mD24Months = 0;




                #endregion

                #endregion

                #region ARTClinic

                mFunctionName = "ARTClinic";

                Int32 mAKS, mAKSEver, mATotalStartedART, mAPSHD, mAPCR, mAU24, mAPreg, mABF, mACD4, mATLC, mAWHOStage3, mAWHOStage4, mAUnknown, mAEverPSHD, mAEverPCR, mAEverU24, mAEverPreg, mAEverBF, mAEverCD4, mAEverTLC, mAEverWHOStage3, mAEverWHOStage4, mAEverUnknown, mANever2Yrs, mANever2YrsEver, mALast2Yrs, mALast2YrsEver, mACurr, mACurrEver, mAAChildrenBelow24MonthsAtInitiationInQuarter, mAAChildrenBelow24MonthsAtInitiationEver, mABChildren24MonthsTo14YearsAtInitaitionEver;
                Int32 mATotalPatientsRegisteredInQuarter = 0;
                Int32 mATotalPatientsRegisteredEver = 0;
                Int32 mAFTPatientsInQuarter = 0;
                Int32 mAFTPatientsEverRegistered = 0;
                Int32 mAREPatientsInQuarter = 0;
                Int32 mAREPatientsEverRegistered = 0;
                Int32 mATIPatientsInQuarter = 0;
                Int32 mATIPatientsEverRegistered = 0;
                Int32 mAMalePatientsInQuarter = 0;
                Int32 mAMalePatientsEverRegistered = 0;
                Int32 mAFNPPatientsInQuarter = 0;
                Int32 mAFNPPatientsEverRegistered = 0;
                Int32 mAFPPatientsInQuarter = 0;
                Int32 mAFPPatientsEverRegistered = 0;
                mAAChildrenBelow24MonthsAtInitiationInQuarter =0;
                mAAChildrenBelow24MonthsAtInitiationEver =0;
                Int32 mABChildren24MonthsTo14YearsAtInitaitionInQuarter = 0;
                mABChildren24MonthsTo14YearsAtInitaitionEver =0;
                Int32 mAAdults15YrsOrOlderAtEnrolmentInQuarter = 0;
                Int32 mAAdults15YrsOrOlderAtEnrolmentEverRegistered = 0;
                Int32 mAExposedInfantsInQuarter = 0;
                Int32 mAExposedInfantsEverRegistered = 0;
                Int32 mAConfirmedInfectedPatientsInQuarter = 0;
                Int32 mAConfirmedInfectedPatientsEverRegistered = 0;
                mANever2Yrs =0;
                mANever2YrsEver =0;
                mALast2Yrs =0;
                mALast2YrsEver =0;
                mACurr =0;
                mACurrEver =0;
                mAKS =0;
                mAKSEver =0;
                mAPSHD =0;
                mAPCR =0;
                mAU24 =0;
                mAPreg =0;
                mABF =0;
                mACD4 =0;
                mAWHOStage3 =0;
                mAWHOStage4 =0;
                mAUnknown =0;
                mAEverPSHD =0;
                mAEverPCR =0;
                mAEverU24 =0;
                mAEverPreg =0;
                mAEverBF =0;
                mAEverCD4 =0;
                mAEverTLC =0;
                mAEverWHOStage3 =0;
                mAEverWHOStage4 =0;
                mAEverUnknown =0;
                mATLC = 0;

                                
                OdbcCommand cmdART = new OdbcCommand();
                OdbcCommand cmdARTP = new OdbcCommand();
                OdbcCommand cmdARTT = new OdbcCommand();

                OdbcDataAdapter mARTAdapter = new OdbcDataAdapter();
                DataTable mDtART = new DataTable();

                OdbcDataAdapter mARTPAdapter = new OdbcDataAdapter();
                DataTable mDtARTP = new DataTable();

                #region ART_AdultFormulations
                mFunctionName = mFunctionName + " _ART";
                cmdART.Connection = mConn;
                cmdART.CommandText = "select a.transferindate, a.clinicalstage, a.tbinitialstatus, a.ks, a.arventrymode, a.latestoutcome, a.arvstartreason, a.artinitiationdate, a.pregnant, p.birthdate, p.gender from ctc_art a, patients p where a.patientcode = p.code";
                cmdART.ExecuteNonQuery();
                              
                mARTAdapter.SelectCommand = cmdART;
                mARTAdapter.Fill(mDtART);

               
                foreach (DataRow MyRow in mDtART.Rows)
                {
                    string mModeOfEntry = Convert.ToString(MyRow["arventrymode"]);
                    string mARTStartReason = Convert.ToString(MyRow["arvstartreason"]);
                    DateTime mDateOfBirth = Convert.ToDateTime(MyRow["birthdate"]);
                    string mGender = Convert.ToString(MyRow["gender"]);
                    string mPregnant = Convert.ToString(MyRow["pregnant"]);
                    double mTBStatusAtInitiation = Convert.ToDouble(MyRow["tbinitialstatus"]);
                    Int16 mKs = Convert.ToInt16(MyRow["ks"]);
                 
                    if (mModeOfEntry == "TI")
                    {
                        DateTime mTransferInDate = Convert.ToDateTime(MyRow["transferindate"]);

                        if (((mTransferInDate >= mQuarterStartDate) && (mTransferInDate <= mQuarterEndDate))) 
                        {
                            mATotalPatientsRegisteredInQuarter += 1;
                            if (mARTStartReason == "Preg")
                            {
                                mAPreg += 1;
                            }

                            if (mARTStartReason == "PSHD")
                            {
                                mAPSHD += 1;
                            }

                            if (mARTStartReason == "PCR")
                            {
                                mAPCR += 1;
                            }

                            if (mARTStartReason == "U24")
                            {
                                mAU24 += 1;
                            }

                            if (mARTStartReason == "BF")
                            {
                                mABF += 1;
                            }

                            if (mARTStartReason == "CD4")
                            {
                                mACD4 += 1;
                            }

                            if (mARTStartReason == "TLC")
                            {
                                mATLC += 1;
                            }

                            if (mARTStartReason == "WHO stage 3")
                            {
                                mAWHOStage3 += 1;
                            }

                            if (mARTStartReason == "WHO stage 4")
                            {
                                mAWHOStage4 += 1;
                            }

                            if (mARTStartReason == "Unk")
                            {
                                mAUnknown += 1;
                            }

                            if (mKs == 1)
                            {
                              mAKS += 1;
                            }
                            mATIPatientsInQuarter += 1;

                            TimeSpan Ts = (Convert.ToDateTime(mTransferInDate) - Convert.ToDateTime(mDateOfBirth));
                           
                            double mAgeInDays = Ts.Days;
                            if (mAgeInDays < 728.5)
                            {
                                mAAChildrenBelow24MonthsAtInitiationInQuarter += 1;
                            }

                            if ((mAgeInDays >= 728.5) && (mAgeInDays <= 5100))
                            {
                                mABChildren24MonthsTo14YearsAtInitaitionInQuarter += 1;
                               
                            }

                            if (mAgeInDays > 5100)
                            {
                                mAAdults15YrsOrOlderAtEnrolmentInQuarter += 1;

                            }

                            if (mGender == "M")
                            {
                                mAMalePatientsInQuarter += 1;
                            }
                            if ((mGender == "F") &&  (mPregnant == "-1"))
                            {
                                mAFPPatientsInQuarter += 1;
                            }

                            if ((mGender == "F") && (mPregnant != "-1"))
                            {
                                mAFNPPatientsInQuarter += 1;
                            }

                            if (mTBStatusAtInitiation == 0 )
                            {
                               mANever2Yrs += 1;
                            }

                            if (mTBStatusAtInitiation == 1)
                            {
                                mALast2Yrs += 1;
                            }

                            if (mTBStatusAtInitiation == 2)
                            {
                                mACurr += 1;
                            }
                            
                        }

                        if (mTransferInDate <= mQuarterEndDate)
                        {
                            mATotalPatientsRegisteredEver += 1;
                            if (mARTStartReason == "Preg")
                            {
                                mAEverPreg += 1;
                            }

                            if (mARTStartReason == "PSHD")
                            {
                                mAEverPSHD += 1;
                            }

                            if (mARTStartReason == "PCR")
                            {
                                mAEverPCR += 1;
                            }

                            if (mARTStartReason == "U24")
                            {
                                mAEverU24 += 1;
                            }

                            if (mARTStartReason == "BF")
                            {
                                mAEverBF += 1;
                            }

                            if (mARTStartReason == "CD4")
                            {
                                mAEverCD4 += 1;
                            }

                            if (mARTStartReason == "TLC")
                            {
                                mAEverTLC += 1;
                            }

                            if (mARTStartReason == "WHO stage 3")
                            {
                                mAEverWHOStage3 += 1;
                            }

                            if (mARTStartReason == "WHO stage 4")
                            {
                                mAEverWHOStage4 += 1;
                            }

                            if (mARTStartReason == "Unk")
                            {
                                mAEverUnknown += 1;
                            }

                            mATIPatientsEverRegistered += 1;

                            if (mKs == 1)
                            {
                                mAKSEver += 1;
                            }

                            TimeSpan Ts = (Convert.ToDateTime(mTransferInDate) - Convert.ToDateTime(mDateOfBirth));

                            double mAgeInDays = Ts.Days;
                            if (mAgeInDays < 728.5)
                            {
                                mAAChildrenBelow24MonthsAtInitiationEver += 1;
                            }

                            if ((mAgeInDays >= 728.5) && (mAgeInDays <= 5100))
                            {
                                mABChildren24MonthsTo14YearsAtInitaitionEver += 1;

                            }

                            if (mAgeInDays > 5100)
                            {
                                mAAdults15YrsOrOlderAtEnrolmentEverRegistered += 1;
                                
                            }

                            if (mGender == "M")
                            {
                                mAMalePatientsEverRegistered += 1;
                            }
                            if ((mGender == "F") && (mPregnant == "-1"))
                            {
                                mAFPPatientsEverRegistered += 1;
                            }

                            if ((mGender == "F") && (mPregnant != "-1"))
                            {
                                mAFNPPatientsEverRegistered += 1;
                            }

                            if (mTBStatusAtInitiation == 0)
                            {
                                mANever2YrsEver += 1;
                            }

                            if (mTBStatusAtInitiation == 1)
                            {
                                mALast2YrsEver += 1;
                            }

                            if (mTBStatusAtInitiation == 2)
                            {
                                mACurrEver += 1;
                            }
                        }
                        
                    }

                    if (mModeOfEntry != "TI")
                    {
                        DateTime mArtInitiationDate = Convert.ToDateTime(MyRow["artinitiationdate"]);

                        if (((mArtInitiationDate >= mQuarterStartDate) && (mArtInitiationDate <= mQuarterEndDate))) 
               
                        {
                            mATotalPatientsRegisteredInQuarter += 1;
                            if (mARTStartReason == "Preg")
                            {
                                mAPreg += 1;
                            }

                            if (mARTStartReason == "PSHD")
                            {
                                mAPSHD += 1;
                            }

                            if (mARTStartReason == "PCR")
                            {
                                mAPCR += 1;
                            }

                            if (mARTStartReason == "U24")
                            {
                                mAU24 += 1;
                            }

                            if (mARTStartReason == "BF")
                            {
                                mABF += 1;
                            }

                            if (mARTStartReason == "CD4")
                            {
                                mACD4 += 1;
                            }

                            if (mARTStartReason == "TLC")
                            {
                                mATLC += 1;
                            }

                            if (mARTStartReason == "WHO stage 3")
                            {
                                mAWHOStage3 += 1;
                            }

                            if (mARTStartReason == "WHO stage 4")
                            {
                                mAWHOStage4 += 1;
                            }

                            if (mARTStartReason == "Unk")
                            {
                                mAUnknown += 1;
                            }

                            if (mModeOfEntry == "RE")
                            {
                               mAREPatientsInQuarter += 1;
                            }
                            if (mModeOfEntry == "FT")
                            {
                             mAFTPatientsInQuarter += 1;
                            }

                            if (mKs == 1)
                            {
                                mAKS += 1;
                            }

                            TimeSpan Ts = (Convert.ToDateTime(mArtInitiationDate) - Convert.ToDateTime(mDateOfBirth));

                            double mAgeInDays = Ts.Days;
                            if (mAgeInDays < 728.5)
                            {
                                mAAChildrenBelow24MonthsAtInitiationInQuarter += 1;
                            }

                            if ((mAgeInDays >= 728.5) && (mAgeInDays <= 5100))
                            {
                                mABChildren24MonthsTo14YearsAtInitaitionInQuarter += 1;

                            }

                            if (mAgeInDays > 5100)
                            {
                                mAAdults15YrsOrOlderAtEnrolmentInQuarter += 1;

                            }

                            if (mGender == "M")
                            {
                                mAMalePatientsInQuarter += 1;
                            }
                            if ((mGender == "F") && (mPregnant == "-1"))
                            {
                                mAFPPatientsInQuarter += 1;
                            }

                            if ((mGender == "F") && (mPregnant != "-1"))
                            {
                                mAFNPPatientsInQuarter += 1;
                            }

                            if (mTBStatusAtInitiation == 0)
                            {
                                mANever2Yrs += 1;
                            }

                            if (mTBStatusAtInitiation == 1)
                            {
                                mALast2Yrs += 1;
                            }

                            if (mTBStatusAtInitiation == 2)
                            {
                                mACurr += 1;
                            }
                        }

                        if (mArtInitiationDate <= mQuarterEndDate)
                        {
                            mATotalPatientsRegisteredEver += 1;
                            if (mARTStartReason == "Preg")
                            {
                                mAEverPreg += 1;
                            }

                            if (mARTStartReason == "PSHD")
                            {
                                mAEverPSHD += 1;
                            }

                            if (mARTStartReason == "PCR")
                            {
                                mAEverPCR += 1;
                            }

                            if (mARTStartReason == "U24")
                            {
                                mAEverU24 += 1;
                            }

                            if (mARTStartReason == "BF")
                            {
                                mAEverBF += 1;
                            }

                            if (mARTStartReason == "CD4")
                            {
                                mAEverCD4 += 1;
                            }

                            if (mARTStartReason == "TLC")
                            {
                                mAEverTLC += 1;
                            }

                            if (mARTStartReason == "WHO stage 3")
                            {
                                mAEverWHOStage3 += 1;
                            }

                            if (mARTStartReason == "WHO stage 4")
                            {
                                mAEverWHOStage4 += 1;
                            }

                            if (mARTStartReason == "Unk")
                            {
                                mAEverUnknown += 1;
                            }

                             if (mModeOfEntry == "RE")
                            {
                              mAREPatientsEverRegistered += 1;
                            }
                             if (mModeOfEntry == "FT")
                             {
                                 mAFTPatientsEverRegistered += 1;
                             }

                             if (mKs == 1)
                             {
                                 mAKSEver += 1;
                             }

                             TimeSpan Ts = (Convert.ToDateTime(mArtInitiationDate) - Convert.ToDateTime(mDateOfBirth));

                             double mAgeInDays = Ts.Days;
                             if (mAgeInDays < 728.5)
                             {
                                 mAAChildrenBelow24MonthsAtInitiationEver += 1;
                             }

                             if ((mAgeInDays >= 728.5) && (mAgeInDays <= 5100))
                             {
                                 mABChildren24MonthsTo14YearsAtInitaitionEver += 1;

                             }

                             if (mAgeInDays > 5100)
                             {
                                 mAAdults15YrsOrOlderAtEnrolmentEverRegistered += 1;


                             }

                             if (mGender == "M")
                             {
                                 mAMalePatientsEverRegistered += 1;
                             }
                             if ((mGender == "F") && (mPregnant == "-1"))
                             {
                                 mAFPPatientsEverRegistered += 1;
                             }

                             if ((mGender == "F") && (mPregnant != "-1"))
                             {
                                 mAFNPPatientsEverRegistered += 1;
                             }

                             if (mTBStatusAtInitiation == 0)
                             {
                                 mANever2YrsEver += 1;
                             }

                             if (mTBStatusAtInitiation == 1)
                             {
                                 mALast2YrsEver += 1;
                             }

                             if (mTBStatusAtInitiation == 2)
                             {
                                 mACurrEver += 1;
                             }
                        }
                    }

                }

                #endregion

                #region ARTP_PaediatricFormulations

                mFunctionName = mFunctionName + " _ARTP";
                cmdARTP.Connection = mConn;
                cmdARTP.CommandText = "select a.transferindate, a.clinicalstage, a.tbinitialstatus, a.ks, a.arventrymode, a.latestoutcome, a.arvstartreason, a.artinitiationdate, p.birthdate, p.gender from ctc_artp a, patients p where a.patientcode = p.code";
                cmdARTP.ExecuteNonQuery();

                mDtARTP.Clear();

                mARTPAdapter.SelectCommand = cmdARTP;
                mARTPAdapter.Fill(mDtARTP);


                foreach (DataRow MyRow1 in (mDtARTP.Rows))
                {
                    string mModeOfEntry = Convert.ToString(MyRow1["arventrymode"]);
                    string mARTStartReason = Convert.ToString(MyRow1["arvstartreason"]);
                    Int16 mKs = Convert.ToInt16(MyRow1["ks"]);
                    DateTime mDateOfBirth = Convert.ToDateTime(MyRow1["birthdate"]);
                    string mGender = Convert.ToString(MyRow1["gender"]);
                    double mTBStatusAtInitiation = Convert.ToDouble(MyRow1["tbinitialstatus"]);
                    

                    if (mModeOfEntry == "TI")
                    {
                        DateTime mTransferInDate = Convert.ToDateTime(MyRow1["transferindate"]);

                        if ((mTransferInDate >= mQuarterStartDate) && (mTransferInDate <= mQuarterEndDate))
                        {
                            mATotalPatientsRegisteredInQuarter += 1;
                            if (mARTStartReason == "Preg")
                            {
                                mAPreg += 1;
                            }

                            if (mARTStartReason == "PSHD")
                            {
                                mAPSHD += 1;
                            }

                            if (mARTStartReason == "PCR")
                            {
                                mAPCR += 1;
                            }

                            if (mARTStartReason == "U24")
                            {
                                mAU24 += 1;
                            }

                            if (mARTStartReason == "BF")
                            {
                                mABF += 1;
                            }

                            if (mARTStartReason == "CD4")
                            {
                                mACD4 += 1;
                            }

                            if (mARTStartReason == "TLC")
                            {
                                mATLC += 1;
                            }

                            if (mARTStartReason == "WHO stage 3")
                            {
                                mAWHOStage3 += 1;
                            }

                            if (mARTStartReason == "WHO stage 4")
                            {
                                mAWHOStage4 += 1;
                            }

                            if (mARTStartReason == "Unk")
                            {
                                mAUnknown += 1;
                            }

                            if (mKs == 1)
                            {
                                mAKS += 1;
                            }

                            mATIPatientsInQuarter += 1;

                            TimeSpan Ts = (Convert.ToDateTime(mTransferInDate) - Convert.ToDateTime(mDateOfBirth));

                            double mAgeInDays = Ts.Days;
                            if (mAgeInDays < 728.5)
                            {
                                mAAChildrenBelow24MonthsAtInitiationInQuarter += 1;
                            }

                            if ((mAgeInDays >= 728.5) && (mAgeInDays <= 5100))
                            {
                                mABChildren24MonthsTo14YearsAtInitaitionInQuarter += 1;

                            }

                            if (mAgeInDays > 5100)
                            {
                                mAAdults15YrsOrOlderAtEnrolmentInQuarter += 1;

                            }

                            if (mGender == "M")
                            {
                                mAMalePatientsInQuarter += 1;
                            }
                           
                            if (mGender == "F")
                            {
                                mAFNPPatientsInQuarter += 1;
                            }

                            if (mTBStatusAtInitiation == 0)
                            {
                                mANever2Yrs += 1;
                            }

                            if (mTBStatusAtInitiation == 1)
                            {
                                mALast2Yrs += 1;
                            }

                            if (mTBStatusAtInitiation == 2)
                            {
                                mACurr += 1;
                            }
                        }

                        if (mTransferInDate <= mQuarterEndDate)
                        {
                            mATotalPatientsRegisteredEver += 1;
                            if (mARTStartReason == "Preg")
                            {
                                mAEverPreg += 1;
                            }

                            if (mARTStartReason == "PSHD")
                            {
                                mAEverPSHD += 1;
                            }

                            if (mARTStartReason == "PCR")
                            {
                                mAEverPCR += 1;
                            }

                            if (mARTStartReason == "U24")
                            {
                                mAEverU24 += 1;
                            }

                            if (mARTStartReason == "BF")
                            {
                                mAEverBF += 1;
                            }

                            if (mARTStartReason == "CD4")
                            {
                                mAEverCD4 += 1;
                            }

                            if (mARTStartReason == "TLC")
                            {
                                mAEverTLC += 1;
                            }

                            if (mARTStartReason == "WHO stage 3")
                            {
                                mAEverWHOStage3 += 1;
                            }

                            if (mARTStartReason == "WHO stage 4")
                            {
                                mAEverWHOStage4 += 1;
                            }

                            if (mARTStartReason == "Unk")
                            {
                                mAEverUnknown += 1;
                            }

                            mATIPatientsEverRegistered += 1;

                            if (mKs == 1)
                            {
                                mAKSEver += 1;
                            }

                            TimeSpan Ts = (Convert.ToDateTime(mTransferInDate) - Convert.ToDateTime(mDateOfBirth));

                            double mAgeInDays = Ts.Days;
                            if (mAgeInDays < 728.5)
                            {
                                mAAChildrenBelow24MonthsAtInitiationEver += 1;
                            }

                            if ((mAgeInDays >= 728.5) && (mAgeInDays <= 5100))
                            {
                                mABChildren24MonthsTo14YearsAtInitaitionEver += 1;

                            }

                            if (mAgeInDays > 5100)
                            {
                                mAAdults15YrsOrOlderAtEnrolmentEverRegistered += 1;
                            }

                            if (mGender == "M")
                            {
                                mAMalePatientsEverRegistered += 1;
                            }
                            
                            if (mGender == "F") 
                            {
                                mAFNPPatientsEverRegistered += 1;
                            }

                            if (mTBStatusAtInitiation == 0)
                            {
                                mANever2YrsEver += 1;
                            }

                            if (mTBStatusAtInitiation == 1)
                            {
                                mALast2YrsEver += 1;
                            }

                            if (mTBStatusAtInitiation == 2)
                            {
                                mACurrEver += 1;
                            }
                        }

                    }

                    if (mModeOfEntry != "TI")
                    {
                        DateTime mArtInitiationDate = Convert.ToDateTime(MyRow1["artinitiationdate"]);

                        if ((mArtInitiationDate >= mQuarterStartDate) && (mArtInitiationDate <= mQuarterEndDate))
                        {
                            mATotalPatientsRegisteredInQuarter += 1;
                            if (mARTStartReason == "Preg")
                            {
                                mAPreg += 1;
                            }

                            if (mARTStartReason == "PSHD")
                            {
                                mAPSHD += 1;
                            }

                            if (mARTStartReason == "PCR")
                            {
                                mAPCR += 1;
                            }

                            if (mARTStartReason == "U24")
                            {
                                mAU24 += 1;
                            }

                            if (mARTStartReason == "BF")
                            {
                                mABF += 1;
                            }

                            if (mARTStartReason == "CD4")
                            {
                                mACD4 += 1;
                            }

                            if (mARTStartReason == "TLC")
                            {
                                mATLC += 1;
                            }

                            if (mARTStartReason == "WHO stage 3")
                            {
                                mAWHOStage3 += 1;
                            }

                            if (mARTStartReason == "WHO stage 4")
                            {
                                mAWHOStage4 += 1;
                            }

                            if (mARTStartReason == "Unk")
                            {
                                mAUnknown += 1;
                            }

                            if (mModeOfEntry == "RE")
                            {
                                mAREPatientsInQuarter += 1;
                            }
                            if (mModeOfEntry == "FT")
                            {
                                mAFTPatientsInQuarter += 1;
                            }

                            if (mKs == 1)
                            {
                                mAKS += 1;
                            }

                            TimeSpan Ts = (Convert.ToDateTime(mArtInitiationDate) - Convert.ToDateTime(mDateOfBirth));

                            double mAgeInDays = Ts.Days;
                            if (mAgeInDays < 728.5)
                            {
                                mAAChildrenBelow24MonthsAtInitiationInQuarter += 1;
                            }

                            if ((mAgeInDays >= 728.5) && (mAgeInDays <= 5100))
                            {
                                mABChildren24MonthsTo14YearsAtInitaitionInQuarter += 1;

                            }

                            if (mAgeInDays > 5100)
                            {
                                mAAdults15YrsOrOlderAtEnrolmentInQuarter += 1;

                            }

                            if (mGender == "M")
                            {
                                mAMalePatientsInQuarter += 1;
                            }
                           
                            if (mGender == "F")
                            {
                                mAFNPPatientsInQuarter += 1;
                            }

                            if (mTBStatusAtInitiation == 0)
                            {
                                mANever2Yrs += 1;
                            }

                            if (mTBStatusAtInitiation == 1)
                            {
                                mALast2Yrs += 1;
                            }

                            if (mTBStatusAtInitiation == 2)
                            {
                                mACurr += 1;
                            }
                        }

                        if (mArtInitiationDate <= mQuarterEndDate)
                        {
                            mATotalPatientsRegisteredEver += 1;
                            if (mARTStartReason == "Preg")
                            {
                                mAEverPreg += 1;
                            }

                            if (mARTStartReason == "PSHD")
                            {
                                mAEverPSHD += 1;
                            }

                            if (mARTStartReason == "PCR")
                            {
                                mAEverPCR += 1;
                            }

                            if (mARTStartReason == "U24")
                            {
                                mAEverU24 += 1;
                            }

                            if (mARTStartReason == "BF")
                            {
                                mAEverBF += 1;
                            }

                            if (mARTStartReason == "CD4")
                            {
                                mAEverCD4 += 1;
                            }

                            if (mARTStartReason == "TLC")
                            {
                                mAEverTLC += 1;
                            }

                            if (mARTStartReason == "WHO stage 3")
                            {
                                mAEverWHOStage3 += 1;
                            }

                            if (mARTStartReason == "WHO stage 4")
                            {
                                mAEverWHOStage4 += 1;
                            }

                            if (mARTStartReason == "Unk")
                            {
                                mAEverUnknown += 1;
                            }

                            if (mModeOfEntry == "RE")
                            {
                                mAREPatientsEverRegistered += 1;
                            }
                            if (mModeOfEntry == "FT")
                            {
                                mAFTPatientsEverRegistered += 1;
                            }

                            if (mKs == 1)
                            {
                                mAKSEver += 1;
                            }

                            TimeSpan Ts = (Convert.ToDateTime(mArtInitiationDate) - Convert.ToDateTime(mDateOfBirth));

                            double mAgeInDays = Ts.Days;
                            if (mAgeInDays < 728.5)
                            {
                                mAAChildrenBelow24MonthsAtInitiationEver += 1;
                            }

                            if ((mAgeInDays >= 728.5) && (mAgeInDays <= 5100))
                            {
                                mABChildren24MonthsTo14YearsAtInitaitionEver += 1;

                            }

                            if (mAgeInDays > 5100)
                            {
                                mAAdults15YrsOrOlderAtEnrolmentEverRegistered += 1;
                                
                            }

                            if (mGender == "M")
                            {
                                mAMalePatientsEverRegistered += 1;
                            }
                             

                            if (mGender == "F")
                            {
                                mAFNPPatientsEverRegistered += 1;
                            }

                            if (mTBStatusAtInitiation == 0)
                            {
                                mANever2YrsEver += 1;
                            }

                            if (mTBStatusAtInitiation == 1)
                            {
                                mALast2YrsEver += 1;
                            }

                            if (mTBStatusAtInitiation == 2)
                            {
                                mACurrEver += 1;
                            }

                        }
                    }

                }
                #endregion

               mDtARTClinic.Columns.Add("keyvalue", typeof(System.String));
               mDtARTClinic.Columns.Add("TotalRegisteredInQuarter", typeof(System.String));
               mDtARTClinic.Columns.Add("TotalEverRegistered", typeof(System.String));
               mDtARTClinic.Columns.Add("FTPatientsInQuarter", typeof(System.String));
               mDtARTClinic.Columns.Add("FTPatientsEverRegistered", typeof(System.String));
               mDtARTClinic.Columns.Add("REPatientsInQuarter", typeof(System.String));
               mDtARTClinic.Columns.Add("REPatientsEverRegistered", typeof(System.String));
               mDtARTClinic.Columns.Add("TIPatientsInQuarter", typeof(System.String));
               mDtARTClinic.Columns.Add("TIPatientsEverRegistered", typeof(System.String));
               mDtARTClinic.Columns.Add("MalePatientsInQuarter", typeof(System.String));

               mDtARTClinic.Columns.Add("MalePatientsEverRegistered", typeof(System.String));
               mDtARTClinic.Columns.Add("FNPPatientsInQuarter", typeof(System.String));
               mDtARTClinic.Columns.Add("FNPPatientsEverRegistered", typeof(System.String));
               mDtARTClinic.Columns.Add("FPPatientsInQuarter", typeof(System.String));
               mDtARTClinic.Columns.Add("FPPatientsEverRegistered", typeof(System.String));
               mDtARTClinic.Columns.Add("ChildrenBelow24MonthsAtInitiationInQuarter", typeof(System.String));
               mDtARTClinic.Columns.Add("ChildrenBelow24MonthsAtInitiationEver", typeof(System.String));
              
               mDtARTClinic.Columns.Add("BChildren24MonthsTo14YearsATEnrolmentInQuarter", typeof(System.String));
               mDtARTClinic.Columns.Add("BChildren24MonthsTo14YearsATEnrolmentRegisteredEver", typeof(System.String));
               mDtARTClinic.Columns.Add("Adults15YrsOrOlderAtEnrolmentInQuarter", typeof(System.String));
               mDtARTClinic.Columns.Add("Adults15YrsOrOlderAtEnrolmentEverRegistered", typeof(System.String));
               mDtARTClinic.Columns.Add("ExposedInfantsInQuarter", typeof(System.String));
               mDtARTClinic.Columns.Add("ExposedInfantsEverRegistered", typeof(System.String));
               mDtARTClinic.Columns.Add("ConfirmedInfectedPatientsInQuarter", typeof(System.String));
               mDtARTClinic.Columns.Add("ConfirmedInfectedPatientsEverRegistered", typeof(System.String));
               mDtARTClinic.Columns.Add("Never2Yrs", typeof(System.String));

               mDtARTClinic.Columns.Add("Never2YrsEver", typeof(System.String));
               mDtARTClinic.Columns.Add("Last2Yrs", typeof(System.String));
               mDtARTClinic.Columns.Add("Last2YrsEver", typeof(System.String));
               mDtARTClinic.Columns.Add("Curr", typeof(System.String));
               mDtARTClinic.Columns.Add("CurrEver", typeof(System.String));
               mDtARTClinic.Columns.Add("KS", typeof(System.String));

               mDtARTClinic.Columns.Add("KSEver", typeof(System.String));
               mDtARTClinic.Columns.Add("mPSHD", typeof(System.String));
               mDtARTClinic.Columns.Add("mPCR", typeof(System.String));
               mDtARTClinic.Columns.Add("mU24", typeof(System.String));
               mDtARTClinic.Columns.Add("mPreg", typeof(System.String));

               mDtARTClinic.Columns.Add("mBF", typeof(System.String));
               mDtARTClinic.Columns.Add("mCD4", typeof(System.String));
               mDtARTClinic.Columns.Add("mTLC", typeof(System.String));
               mDtARTClinic.Columns.Add("mWHOStage3", typeof(System.String));
               mDtARTClinic.Columns.Add("mWHOStage4", typeof(System.String));
               mDtARTClinic.Columns.Add("mUnknown", typeof(System.String));
               mDtARTClinic.Columns.Add("mEverPSHD", typeof(System.String));
               mDtARTClinic.Columns.Add("mEverPCR", typeof(System.String));
               mDtARTClinic.Columns.Add("mEverU24", typeof(System.String));

               mDtARTClinic.Columns.Add("mEverPreg", typeof(System.String));
               mDtARTClinic.Columns.Add("mEverBF", typeof(System.String));
               mDtARTClinic.Columns.Add("mEverCD4", typeof(System.String));
               mDtARTClinic.Columns.Add("mEverTLC", typeof(System.String));
               mDtARTClinic.Columns.Add("mEverWHOStage3", typeof(System.String));
               mDtARTClinic.Columns.Add("mEverWHOStage4", typeof(System.String));

               mDtARTClinic.Columns.Add("mEverUnknown", typeof(System.String));
                
              

               mDtARTClinic.AcceptChanges();

               mDtARTClinic.Rows.Add("parentlink", mATotalPatientsRegisteredInQuarter, mATotalPatientsRegisteredEver, mAFTPatientsInQuarter, mAFTPatientsEverRegistered, mAREPatientsInQuarter, mAREPatientsEverRegistered, mATIPatientsInQuarter, mATIPatientsEverRegistered, mAMalePatientsInQuarter, mAMalePatientsEverRegistered, mAFNPPatientsInQuarter, mAFNPPatientsEverRegistered, mAFPPatientsInQuarter, mAFPPatientsEverRegistered, mAAChildrenBelow24MonthsAtInitiationInQuarter, mAAChildrenBelow24MonthsAtInitiationEver, mABChildren24MonthsTo14YearsAtInitaitionInQuarter, mABChildren24MonthsTo14YearsAtInitaitionEver, mAAdults15YrsOrOlderAtEnrolmentInQuarter, mAAdults15YrsOrOlderAtEnrolmentEverRegistered, mAExposedInfantsInQuarter, mAExposedInfantsEverRegistered, mAConfirmedInfectedPatientsInQuarter, mAConfirmedInfectedPatientsEverRegistered, mANever2Yrs, mANever2YrsEver, mALast2Yrs, mALast2YrsEver, mACurr, mACurrEver, mAKS, mAKSEver, mAPSHD, mAPCR, mAU24, mAPreg, mABF, mACD4, mATLC, mAWHOStage3, mAWHOStage4, mAUnknown, mAEverPSHD, mAEverPCR, mAEverU24, mAEverPreg, mAEverBF, mAEverCD4, mAEverTLC, mAEverWHOStage3, mAEverWHOStage4, mAEverUnknown);

               mDtARTClinic.AcceptChanges();

                #endregion

                #region PrimaryOutcomesAsOfEndOfQuarterEvaluated
                mFunctionName = " PrimaryOutcomesAsOfEndOfQuarterEvaluated";

                #region Variable Declaration

              
                OdbcCommand cmdPrimaryART = new OdbcCommand();
                OdbcCommand cmdPrimaryARTP = new OdbcCommand();
                OdbcDataAdapter adPrimary = new OdbcDataAdapter();

                DataTable DtPrimary = new DataTable();

                int mM1, mTotalAliveOnART, mM2, mM3, mM4, mTotalAdverseOutcome, mPatientsWithoutSideEffects, mP1, mP2, mP3, mP4, mP9, mA1, mA2, mA3, mA4, mA5, mA6, mA7, mA8, mOther, mAdherence6, mAdherence7, mTBNotSuspected, mTBSuspected, mOnTBTreatment, mNotOnTBTreatment, mPatientsWithSideEffects, mTotalFemalesAliveOnART;
                mM1 = 0;
                mM2 = 0;
                mM3 = 0;
                mM4 = 0;
                mOther = 0;
                mTotalAdverseOutcome = 0;
                mP1 = 0;
                mP2 = 0;
                mP3 = 0;
                mP4 = 0;
                mTotalFemalesAliveOnART = 0;
                mP9 = 0;
                mA1 = 0;
                mA2 = 0;
                mA3 = 0;
                mA4 = 0;
                mA5 = 0;
                mA6 = 0;
                mA7 = 0;
                mA8 = 0;
                mTotalAliveOnART = 0;
                mTotalDied = 0;
                mTotalDefaulted = 0;
                mTotalTransferOut = 0;
                mStopped = 0;
                #endregion

                #region ART - Adult Formulations

                mFunctionName = " PrimaryOutcomesAsOfEndOfQuarterEvaluated - ART - Adult Formulations";
                cmdPrimaryART.Connection = mConn;
                cmdPrimaryART.CommandText = "select * from ctc_art";
                cmdPrimaryART.ExecuteNonQuery();

                adPrimary.SelectCommand = cmdPrimaryART;
                adPrimary.Fill(DtPrimary);

                foreach (DataRow MyRow in DtPrimary.Rows)
                {
                    string mModeOfEntry = Convert.ToString(MyRow["arventrymode"]);
                    
                    string Outcome = Convert.ToString(MyRow["latestoutcome"]);

                    if (mModeOfEntry == "TI")
                    {
                        DateTime mTransferInDate = Convert.ToDateTime(MyRow["transferindate"]);

                       
                        if (mTransferInDate <= mQuarterEndDate)
                        {
                            if (Outcome == "Alive")
                            {
                                mTotalAliveOnART += 1;
                            }

                            OdbcCommand cmdDeathRow = new OdbcCommand();
                            cmdDeathRow.Connection = mConn;
                            cmdDeathRow.CommandText = "select * from ctc_artlog where patientcode = '" + Convert.ToString(MyRow["patientcode"]) + "'";
                            cmdDeathRow.ExecuteNonQuery();

                            OdbcDataAdapter dpDeathRow = new OdbcDataAdapter();
                            DataTable mTbDeathrow = new DataTable();

                            mTbDeathrow.Clear();
                           // DateTime FakeDate = Convert.ToDateTime("1901-02-05 13:08:26");
                            dpDeathRow.SelectCommand = cmdDeathRow;
                            dpDeathRow.Fill(mTbDeathrow);
                            DateTime mDateDied = Convert.ToDateTime("1901-02-05 13:08:26");
                            string mRegimen = "";
                           // clsGlobal.Write_Error(pClassName, mFunctionName, "yadutsa");
                            foreach (DataRow MyRow3 in mTbDeathrow.Rows)
                            {
                                mDateDied = Convert.ToDateTime(MyRow3["outcomedate"]);
                                mRegimen = Convert.ToString(MyRow3["artregimencode"]);
                            }

                            if (mRegimen == "")
                            {
                                mRegimen = Convert.ToString(MyRow["artregimencode1"]);
                            }

                            if (Outcome == "D")
                            {
                                mTotalDied += 1;

                               

                                TimeSpan mTm = (mTransferInDate - mDateDied);
                                Double mDays = mTm.Days;

                                if (mDays <= 30)
                                {
                                    mM1 += 1;
                                }

                                if ((mDays > 30) && (mDays <= 60))
                                {
                                    mM2 += 1;
                                }

                                if ((mDays > 60) && (mDays <= 90))
                                {
                                    mM3 += 1;
                                }
                                if (mDays > 90)
                                {
                                    mM4 += 1;
                                }
                            }

                            if (Outcome == "Def")
                            {
                                mTotalDefaulted += 1;
                            }

                            if (Outcome == "TO")
                            {
                                mTotalTransferOut  += 1;
                            }

                            if (Outcome == "Stop")
                            {
                                mStopped  += 1;
                            }

                            if (Outcome == "Alive")
                            {
                                if (mRegimen == "1A")
                                {
                                    mA1 += 1;
                                }
                                if (mRegimen == "2A")
                                {
                                    mA2 += 1;
                                }
                                if (mRegimen == "3A")
                                {
                                    mA3 += 1;
                                }
                                if (mRegimen == "4A")
                                {
                                    mA4 += 1;
                                }
                                if (mRegimen == "5A")
                                {
                                    mA5 += 1;
                                }
                                if (mRegimen == "6A")
                                {
                                    mA6 += 1;
                                }
                                if (mRegimen == "7A")
                                {
                                    mA7 += 1;
                                }
                                if (mRegimen == "8A")
                                {
                                    mA8 += 1;
                                }


                                if (mRegimen == "1P")
                                {
                                    mP1 += 1;
                                }
                                if (mRegimen == "2P")
                                {
                                    mP2 += 1;
                                }
                                if (mRegimen == "3P")
                                {
                                    mP3 += 1;
                                }
                                if (mRegimen == "4P")
                                {
                                    mP4 += 1;
                                }
                                if (mRegimen == "9P")
                                {
                                    mP9 += 1;
                                }
                                 
                                if (mRegimen == "Other")
                                {
                                    mOther += 1;
                                }
                                
                                
                            }
                        }

                    }

                    if (mModeOfEntry != "TI")
                    {
                        DateTime mArtInitiationDate = Convert.ToDateTime(MyRow["artinitiationdate"]);

                       
                        if (mArtInitiationDate <= mQuarterEndDate)
                        {
                             
                                if (Outcome == "Alive")
                                {
                                    mTotalAliveOnART += 1;
                                }

                                OdbcCommand cmdDeathRow = new OdbcCommand();
                                cmdDeathRow.Connection = mConn;
                                cmdDeathRow.CommandText = "select * from ctc_artlog where patientcode = '" + Convert.ToString(MyRow["patientcode"]) + "'";
                                cmdDeathRow.ExecuteNonQuery();

                                OdbcDataAdapter dpDeathRow = new OdbcDataAdapter();
                                DataTable mTbDeathrow = new DataTable();

                                mTbDeathrow.Clear();

                                dpDeathRow.SelectCommand = cmdDeathRow;
                                dpDeathRow.Fill(mTbDeathrow);
                                DateTime mDateDied = Convert.ToDateTime("1901-02-05 13:08:26");
                                string mRegimen = "";
                                foreach (DataRow MyRow3 in mTbDeathrow.Rows)
                                {
                                    mDateDied = Convert.ToDateTime(MyRow3["outcomedate"]);
                                    mRegimen = Convert.ToString(MyRow3["artregimencode"]);
                                }

                                if (mRegimen == "")
                                {
                                    mRegimen = Convert.ToString(MyRow["artregimencode1"]);
                                }

                                if (Outcome == "D")
                                {
                                    mTotalDied += 1;



                                    TimeSpan mTm = (mArtInitiationDate - mDateDied);
                                    Double mDays = mTm.Days;

                                    if (mDays <= 30)
                                    {
                                        mM1 += 1;
                                    }

                                    if ((mDays > 30) && (mDays <= 60))
                                    {
                                        mM2 += 1;
                                    }

                                    if ((mDays > 60) && (mDays <= 90))
                                    {
                                        mM3 += 1;
                                    }
                                    if (mDays > 90)
                                    {
                                        mM4 += 1;
                                    }
                                }

                                if (Outcome == "Def")
                                {
                                    mTotalDefaulted += 1;
                                }

                                if (Outcome == "TO")
                                {
                                    mTotalTransferOut += 1;
                                }

                                if (Outcome == "Stop")
                                {
                                    mStopped += 1;
                                }

                                if (Outcome == "Alive")
                                {
                                    if (mRegimen == "1A")
                                    {
                                        mA1 += 1;
                                    }
                                    if (mRegimen == "2A")
                                    {
                                        mA2 += 1;
                                    }
                                    if (mRegimen == "3A")
                                    {
                                        mA3 += 1;
                                    }
                                    if (mRegimen == "4A")
                                    {
                                        mA4 += 1;
                                    }
                                    if (mRegimen == "5A")
                                    {
                                        mA5 += 1;
                                    }
                                    if (mRegimen == "6A")
                                    {
                                        mA6 += 1;
                                    }
                                    if (mRegimen == "7A")
                                    {
                                        mA7 += 1;
                                    }
                                    if (mRegimen == "8A")
                                    {
                                        mA8 += 1;
                                    }


                                    if (mRegimen == "1P")
                                    {
                                        mP1 += 1;
                                    }
                                    if (mRegimen == "2P")
                                    {
                                        mP2 += 1;
                                    }
                                    if (mRegimen == "3P")
                                    {
                                        mP3 += 1;
                                    }
                                    if (mRegimen == "4P")
                                    {
                                        mP4 += 1;
                                    }
                                    if (mRegimen == "9P")
                                    {
                                        mP9 += 1;
                                    }

                                    if (mRegimen == "Other")
                                    {
                                        mOther += 1;
                                    }

                                                                
                            }
                        }
                    }

                }


                #endregion

                #region Pediatric Formulations

                mFunctionName = " PrimaryOutcomesAsOfEndOfQuarterEvaluated -  Pediatric Formulations";

                cmdPrimaryARTP.Connection = mConn;
                cmdPrimaryARTP.CommandText = "select * from ctc_artp";
                cmdPrimaryARTP.ExecuteNonQuery();

                
                adPrimary.SelectCommand = cmdPrimaryARTP;
                adPrimary.Fill(DtPrimary);

                foreach (DataRow MyRow in DtPrimary.Rows)
                {
                    string mModeOfEntry = Convert.ToString(MyRow["arventrymode"]);

                    string Outcome = Convert.ToString(MyRow["latestoutcome"]);

                    if (mModeOfEntry == "TI")
                    {
                        DateTime mTransferInDate = Convert.ToDateTime(MyRow["transferindate"]);


                        if (mTransferInDate <= mQuarterEndDate)
                        {
                            if (Outcome == "Alive")
                            {
                                mTotalAliveOnART += 1;
                            }

                            OdbcCommand cmdDeathRow = new OdbcCommand();
                            cmdDeathRow.Connection = mConn;
                            cmdDeathRow.CommandText = "select * from ctc_artplog where patientcode = '" + Convert.ToString(MyRow["patientcode"]) + "'";
                            cmdDeathRow.ExecuteNonQuery();

                            OdbcDataAdapter dpDeathRow = new OdbcDataAdapter();
                            DataTable mTbDeathrow = new DataTable();

                            mTbDeathrow.Clear();

                            dpDeathRow.SelectCommand = cmdDeathRow;
                            dpDeathRow.Fill(mTbDeathrow);
                            DateTime mDateDied = Convert.ToDateTime("1901-02-05 13:08:26");
                            string mRegimen = "";
                            foreach (DataRow MyRow3 in mTbDeathrow.Rows)
                            {
                                mDateDied = Convert.ToDateTime(MyRow3["outcomedate"]);
                                mRegimen = Convert.ToString(MyRow3["artregimencode"]);
                            }

                            if (mRegimen == "")
                            {
                                mRegimen = Convert.ToString(MyRow["artregimencode1"]);
                            }

                            if (Outcome == "D")
                            {
                                mTotalDied += 1;



                                TimeSpan mTm = (mTransferInDate - mDateDied);
                                Double mDays = mTm.Days;

                                if (mDays <= 30)
                                {
                                    mM1 += 1;
                                }

                                if ((mDays > 30) && (mDays <= 60))
                                {
                                    mM2 += 1;
                                }

                                if ((mDays > 60) && (mDays <= 90))
                                {
                                    mM3 += 1;
                                }
                                if (mDays > 90)
                                {
                                    mM4 += 1;
                                }
                            }

                            if (Outcome == "Def")
                            {
                                mTotalDefaulted += 1;
                            }

                            if (Outcome == "TO")
                            {
                                mTotalTransferOut += 1;
                            }

                            if (Outcome == "Stop")
                            {
                                mStopped += 1;
                            }

                            if (Outcome == "Alive")
                            {
                                if (mRegimen == "1A")
                                {
                                    mA1 += 1;
                                }
                                if (mRegimen == "2A")
                                {
                                    mA2 += 1;
                                }
                                if (mRegimen == "3A")
                                {
                                    mA3 += 1;
                                }
                                if (mRegimen == "4A")
                                {
                                    mA4 += 1;
                                }
                                if (mRegimen == "5A")
                                {
                                    mA5 += 1;
                                }
                                if (mRegimen == "6A")
                                {
                                    mA6 += 1;
                                }
                                if (mRegimen == "7A")
                                {
                                    mA7 += 1;
                                }
                                if (mRegimen == "8A")
                                {
                                    mA8 += 1;
                                }


                                if (mRegimen == "1P")
                                {
                                    mP1 += 1;
                                }
                                if (mRegimen == "2P")
                                {
                                    mP2 += 1;
                                }
                                if (mRegimen == "3P")
                                {
                                    mP3 += 1;
                                }
                                if (mRegimen == "4P")
                                {
                                    mP4 += 1;
                                }
                                if (mRegimen == "9P")
                                {
                                    mP9 += 1;
                                }

                                if (mRegimen == "Other")
                                {
                                    mOther += 1;
                                }


                            }
                        }

                    }

                    if (mModeOfEntry != "TI")
                    {
                        DateTime mArtInitiationDate = Convert.ToDateTime(MyRow["artinitiationdate"]);


                        if (mArtInitiationDate <= mQuarterEndDate)
                        {

                            if (Outcome == "Alive")
                            {
                                mTotalAliveOnART += 1;
                            }

                            OdbcCommand cmdDeathRow = new OdbcCommand();
                            cmdDeathRow.Connection = mConn;
                            cmdDeathRow.CommandText = "select * from ctc_artplog where patientcode = '" + Convert.ToString(MyRow["patientcode"]) + "'";
                            cmdDeathRow.ExecuteNonQuery();

                            OdbcDataAdapter dpDeathRow = new OdbcDataAdapter();
                            DataTable mTbDeathrow = new DataTable();

                            mTbDeathrow.Clear();

                            dpDeathRow.SelectCommand = cmdDeathRow;
                            dpDeathRow.Fill(mTbDeathrow);
                            DateTime mDateDied = Convert.ToDateTime("1901-02-05 13:08:26");
                            string mRegimen = "";
                            foreach (DataRow MyRow3 in mTbDeathrow.Rows)
                            {
                                mDateDied = Convert.ToDateTime(MyRow3["outcomedate"]);
                                mRegimen = Convert.ToString(MyRow3["artregimencode"]);
                            }

                            if (mRegimen == "")
                            {
                                mRegimen = Convert.ToString(MyRow["artregimencode1"]);
                            }

                            if (Outcome == "D")
                            {
                                mTotalDied += 1;



                                TimeSpan mTm = (mArtInitiationDate - mDateDied);
                                Double mDays = mTm.Days;

                                if (mDays <= 30)
                                {
                                    mM1 += 1;
                                }

                                if ((mDays > 30) && (mDays <= 60))
                                {
                                    mM2 += 1;
                                }

                                if ((mDays > 60) && (mDays <= 90))
                                {
                                    mM3 += 1;
                                }
                                if (mDays > 90)
                                {
                                    mM4 += 1;
                                }
                            }

                            if (Outcome == "Def")
                            {
                                mTotalDefaulted += 1;
                            }

                            if (Outcome == "TO")
                            {
                                mTotalTransferOut += 1;
                            }

                            if (Outcome == "Stop")
                            {
                                mStopped += 1;
                            }

                            if (Outcome == "Alive")
                            {
                                if (mRegimen == "1A")
                                {
                                    mA1 += 1;
                                }
                                if (mRegimen == "2A")
                                {
                                    mA2 += 1;
                                }
                                if (mRegimen == "3A")
                                {
                                    mA3 += 1;
                                }
                                if (mRegimen == "4A")
                                {
                                    mA4 += 1;
                                }
                                if (mRegimen == "5A")
                                {
                                    mA5 += 1;
                                }
                                if (mRegimen == "6A")
                                {
                                    mA6 += 1;
                                }
                                if (mRegimen == "7A")
                                {
                                    mA7 += 1;
                                }
                                if (mRegimen == "8A")
                                {
                                    mA8 += 1;
                                }


                                if (mRegimen == "1P")
                                {
                                    mP1 += 1;
                                }
                                if (mRegimen == "2P")
                                {
                                    mP2 += 1;
                                }
                                if (mRegimen == "3P")
                                {
                                    mP3 += 1;
                                }
                                if (mRegimen == "4P")
                                {
                                    mP4 += 1;
                                }
                                if (mRegimen == "9P")
                                {
                                    mP9 += 1;
                                }

                                if (mRegimen == "Other")
                                {
                                    mOther += 1;
                                }


                            }
                        }
                    }

                }

                #endregion

                
                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("keyvalue", typeof(System.String));
                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("TotalAliveOnART", typeof(System.String));
                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("M1", typeof(System.String));
                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("M2", typeof(System.String));
                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("M3", typeof(System.String));
                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("M4", typeof(System.String));
                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("TotalDied", typeof(System.String));
                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("Defaulted", typeof(System.String));
                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("Stopped", typeof(System.String));
                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("TransferOut", typeof(System.String));

                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("P1", typeof(System.String));
                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("P2", typeof(System.String));
                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("P3", typeof(System.String));
                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("P4", typeof(System.String));
                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("P9", typeof(System.String));
                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("A1", typeof(System.String));
                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("A2", typeof(System.String));
                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("A3", typeof(System.String));
                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("A4", typeof(System.String));
                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("A5", typeof(System.String));

                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("A6", typeof(System.String));
                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("A7", typeof(System.String));
                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("A8", typeof(System.String));
                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("Other", typeof(System.String));
                mDtPrimaryOutcomesEndOfQuarter.Columns.Add("TotalAdverseOutcome", typeof(System.String));

                mDtPrimaryOutcomesEndOfQuarter.AcceptChanges();

                mDtPrimaryOutcomesEndOfQuarter.Rows.Add("parentlink", mTotalAliveOnART, mM1, mM2, mM3, mM4, mTotalDied, mTotalDefaulted, mStopped, mTransferOut, mP1, mP2, mP3, mP4, mP9, mA1, mA2, mA3, mA4, mA5, mA6, mA7, mA8, mOther, Convert.ToInt16(mTotalDied + mTotalDefaulted + mStopped + mTransferOut));
                mDtPrimaryOutcomesEndOfQuarter.AcceptChanges();
                #endregion



                #endregion


                DataSet mDsData = new DataSet("Quarterly supervision report");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtClinicStaff);
                mDsData.Tables.Add(mDtFistPatientOnART);
                mDsData.Tables.Add(mDtCohortSurvivalAnalysis);
                mDsData.Tables.Add(mDtHIVCareClinic);
                mDsData.Tables.Add(mDtExposedChildUnder24Months);
                mDsData.Tables.Add(mDtExposedChildUnder24MonthsM2);
                mDsData.Tables.Add(mDtExposedChildUnder24MonthsM3);
                mDsData.Tables.Add(mDtARTClinic);
                mDsData.Tables.Add(mDtPrimaryOutcomesEndOfQuarter);
                mDsData.Relations.Add("childrelationship", mDtFacilitySetup.Columns["keyvalue"], mDtClinicStaff.Columns["keyvalue"]);
                mDsData.Relations.Add("childrelationship1", mDtFacilitySetup.Columns["keyvalue"], mDtFistPatientOnART.Columns["keyvalue"]);
                mDsData.Relations.Add("childrelationship2", mDtFacilitySetup.Columns["keyvalue"], mDtCohortSurvivalAnalysis.Columns["keyvalue"]);
                mDsData.Relations.Add("childrelationship3", mDtFacilitySetup.Columns["keyvalue"], mDtHIVCareClinic.Columns["keyvalue"]);
                mDsData.Relations.Add("childrelationship4", mDtFacilitySetup.Columns["keyvalue"], mDtExposedChildUnder24Months.Columns["keyvalue"]);
                mDsData.Relations.Add("childrelationship5", mDtFacilitySetup.Columns["keyvalue"], mDtExposedChildUnder24MonthsM2.Columns["keyvalue"]);
                mDsData.Relations.Add("childrelationship6", mDtFacilitySetup.Columns["keyvalue"], mDtExposedChildUnder24MonthsM3.Columns["keyvalue"]);
                mDsData.Relations.Add("childrelationship7", mDtFacilitySetup.Columns["keyvalue"], mDtARTClinic.Columns["keyvalue"]);
                mDsData.Relations.Add("childrelationship8", mDtFacilitySetup.Columns["keyvalue"], mDtPrimaryOutcomesEndOfQuarter.Columns["keyvalue"]);
                mDsData.RemotingFormat = SerializationFormat.Binary;


                return mDsData;
               
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
       
        #endregion

        #region User Login Details
        public DataSet USER_LoginDetails(DateTime mDateFrom, DateTime mDateTo, string mExtraFilter, string mExtraParameters)
        {
            string mFunctionName = "USER_LoginDetails";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                string mCommandText = "";
                DataTable mDtFacilitySetup = new DataTable("facilitysetup");
                DataTable mDtUserLoginDetails= new DataTable("userlogindetails");

                #region facilitysetup

                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilitySetup);

                mDtFacilitySetup.Columns.Add("para_datefrom", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_dateto", typeof(System.DateTime));
                mDtFacilitySetup.Columns.Add("para_otherparameters", typeof(System.String));
                mDtFacilitySetup.Columns.Add("keyvalue", typeof(System.String));

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    DataRow mDataRow = mDtFacilitySetup.Rows[0];
                    mDataRow.BeginEdit();

                    mDataRow["para_datefrom"] = mDateFrom;
                    mDataRow["para_dateto"] = mDateTo;
                    mDataRow["para_otherparameters"] = mExtraParameters;
                    mDataRow["keyvalue"] = "parentlink";

                    mDataRow.EndEdit();
                    mDtFacilitySetup.AcceptChanges();
                }

                #endregion

                #region generate data
                mCommandText = "SELECT "
                                + "'parentlink' keyvalue,"
                                + "us.description AS userdescription,"
                                + "us.usercode,"
                                + "us.machinename as workstation,"
                                + "us.usergroup,"
                                + "us.logintime,"
                                + "us.logouttime "
                                + "FROM view_userlogindetails us "
                                + "WHERE (DATE(logintime) between " + clsGlobal.Saving_DateValue(mDateFrom) + " AND " + clsGlobal.Saving_DateValue(mDateTo) + ") "
                                + "order by logintime";
                               
               
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtUserLoginDetails);

                #endregion

                DataSet mDsData = new DataSet("summary");
                mDsData.Tables.Add(mDtFacilitySetup);
                mDsData.Tables.Add(mDtUserLoginDetails);
                mDsData.Relations.Add("childrelationship", mDtFacilitySetup.Columns["keyvalue"], mDtUserLoginDetails.Columns["keyvalue"]);
                mDsData.RemotingFormat = SerializationFormat.Binary;

                return mDsData;
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

        #region Get_ReportingData
        public DataTable Get_ReportingData(string mReportCode, DataTable mDtParameters)
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            string mFunctionName = "Get_ReportingData";

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
                DataTable mDtData = new DataTable("data");

                string mCommandText = "";
                string mFilterString = "";
                string mNewCommandText = "";
                string mNewFilterString = "";
                string mGroupByFields = "";

                #region get commandtext and filterstring

                DataTable mDtReports = new DataTable("sys_reports");
                mCommand.CommandText = "select * from sys_reports where code='" + mReportCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtReports);

                if (mDtReports.Rows.Count > 0)
                {
                    mCommandText = mDtReports.Rows[0]["commandtext"].ToString().Trim();
                    mFilterString = mDtReports.Rows[0]["filterstring"].ToString().Trim();
                    mGroupByFields = mDtReports.Rows[0]["groupbyfields"].ToString().Trim();
                }

                #endregion

                #region set values to filterstring

                DataView mDvParameters = new DataView();
                mDvParameters.Table = mDtParameters;
                mDvParameters.Sort = "parametercode";

                int mIndex = 0;

                while (mIndex < mFilterString.Length)
                {
                    string mParameterCode = "";
                    int mTotalChars = 1;
                    string mCurrChar = mFilterString.Substring(mIndex, 1);

                    bool mWildCardStart = false;
                    bool mWildCardEnd = false;

                    switch (mCurrChar.ToLower())
                    {
                        case "#":
                            {
                                string mStartParameterCode = mFilterString.Substring(mIndex + 1);
                                mParameterCode = mStartParameterCode.Substring(0, mStartParameterCode.IndexOf("#"));
                                mTotalChars = mParameterCode.Length + 2;

                                if (mIndex > 1)
                                {
                                    try
                                    {
                                        if (mFilterString.Substring(mIndex - 1, 2) == "%#")
                                        {
                                            mWildCardStart = true;
                                        }
                                    }
                                    catch { }
                                }
                                if (mIndex + 1 < mFilterString.Length)
                                {
                                    try
                                    {
                                        if (mStartParameterCode.Substring(mStartParameterCode.IndexOf("#"), 2) == "#%")
                                        {
                                            mWildCardEnd = true;
                                        }
                                    }
                                    catch { }
                                }

                                string mParameterType = "";
                                string mParameterValue = "";

                                #region get input value

                                int mRowIndex = mDvParameters.Find(mParameterCode);
                                if (mRowIndex >= 0)
                                {
                                    mParameterType = mDvParameters[mRowIndex]["parametertype"].ToString().Trim();

                                    switch (mParameterType.ToLower())
                                    {
                                        case "date":
                                            {
                                                mParameterValue = clsGlobal.Saving_DateValue(
                                                    Convert.ToDateTime(mDvParameters[mRowIndex]["valuedatetime"]).Date);
                                            }
                                            break;
                                        case "int":
                                            {
                                                mParameterValue = mDvParameters[mRowIndex]["valueint"].ToString();
                                            }
                                            break;
                                        case "double":
                                            {
                                                mParameterValue = mDvParameters[mRowIndex]["valuedouble"].ToString();
                                            }
                                            break;
                                        case "boolean":
                                            {
                                                mParameterValue = mDvParameters[mRowIndex]["valueint"].ToString();
                                            }
                                            break;
                                        default:
                                            {
                                                if (mWildCardStart == true && mWildCardEnd == true)
                                                {
                                                    mParameterValue = "'%" + mDvParameters[mRowIndex]["valuestring"].ToString() + "%'";
                                                }
                                                else if (mWildCardStart == true && mWildCardEnd == false)
                                                {
                                                    mParameterValue = "'%" + mDvParameters[mRowIndex]["valuestring"].ToString() + "'";
                                                }
                                                else if (mWildCardStart == false && mWildCardEnd == true)
                                                {
                                                    mParameterValue = "'" + mDvParameters[mRowIndex]["valuestring"].ToString() + "%'";
                                                }
                                                else
                                                {
                                                    mParameterValue = "'" + mDvParameters[mRowIndex]["valuestring"].ToString() + "'";
                                                }
                                            }
                                            break;
                                    }
                                }
                                #endregion

                                mNewFilterString = mNewFilterString + mParameterValue;
                            }
                            break;
                        default:
                            {
                                if (mCurrChar.ToLower() != "%")
                                {
                                    mNewFilterString = mNewFilterString + mCurrChar;
                                }
                            }
                            break;
                    }

                    mIndex = mIndex + mTotalChars;
                }

                #endregion

                string mFields = "";

                #region facilitysetup

                DataTable mDtFacilityOptions = new DataTable("facilityoptions");
                mCommand.CommandText = "select * from facilityoptions";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFacilityOptions);

                if (mDtFacilityOptions.Rows.Count > 0)
                {
                    foreach (DataColumn mDataColumn in mDtFacilityOptions.Columns)
                    {
                        if (mDataColumn.ColumnName.ToLower() != "autocode")
                        {
                            string mFieldName = "facility_" + mDataColumn.ColumnName;
                            string mFieldValue = "";

                            #region build fieldvalue

                            switch (mDataColumn.DataType.FullName.Trim().ToLower())
                            {
                                case "system.decimal":
                                    {
                                        mFieldValue = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Double));
                                    }
                                    break;
                                case "system.double":
                                    {
                                        mFieldValue = Convert.ToDouble(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Double));
                                    }
                                    break;
                                case "system.int16":
                                    {
                                        mFieldValue = Convert.ToInt16(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int16));
                                    }
                                    break;
                                case "system.int32":
                                    {
                                        mFieldValue = Convert.ToInt32(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int32));
                                    }
                                    break;
                                case "system.int64":
                                    {
                                        mFieldValue = Convert.ToInt64(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Int64));
                                    }
                                    break;
                                case "system.single":
                                    {
                                        mFieldValue = Convert.ToSingle(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]).ToString();
                                        mDtData.Columns.Add(mFieldName, typeof(System.Single));
                                    }
                                    break;
                                case "system.datetime":
                                    {
                                        mFieldValue = clsGlobal.Saving_DateValue(Convert.ToDateTime(mDtFacilityOptions.Rows[0][mDataColumn.ColumnName]));
                                        mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                                    }
                                    break;
                                default:
                                    {
                                        mFieldValue = "'" + mDtFacilityOptions.Rows[0][mDataColumn.ColumnName].ToString() + "'";
                                        mDtData.Columns.Add(mFieldName, typeof(System.String));
                                    }
                                    break;
                            }
                            #endregion

                            if (mFields.Trim() == "")
                            {
                                mFields = Environment.NewLine + " " + mFieldValue + " " + mFieldName;
                            }
                            else
                            {
                                mFields = mFields + Environment.NewLine  + ", " + mFieldValue + " " + mFieldName;
                            }
                        }
                    }
                }

                #endregion

                #region add parametervalues

                if (mDtParameters != null)
                {
                    foreach (DataRow mDataRow in mDtParameters.Rows)
                    {
                        string mParameterCode = mDataRow["parametercode"].ToString().Trim().ToLower();
                        string mFieldName = "para_" + mParameterCode;
                        string mParameterType = mDataRow["parametertype"].ToString().Trim();
                        string mFieldValue = "";

                        #region build field value

                        switch (mParameterType.ToLower())
                        {
                            case "double":
                                {
                                    mFieldValue = Convert.ToDouble(mDataRow["valuedouble"]).ToString();
                                    mDtData.Columns.Add(mFieldName, typeof(System.Double));
                                }
                                break;
                            case "int":
                                {
                                    mFieldValue = Convert.ToInt32(mDataRow["valueint"]).ToString();
                                    mDtData.Columns.Add(mFieldName, typeof(System.Int32));
                                }
                                break;
                            case "boolean":
                                {
                                    mFieldValue = Convert.ToInt32(mDataRow["valueint"]).ToString();
                                    mDtData.Columns.Add(mFieldName, typeof(System.Int32));
                                }
                                break;
                            case "date":
                                {
                                    mFieldValue = clsGlobal.Saving_DateValue(
                                                    Convert.ToDateTime(mDataRow["valuedatetime"]).Date);
                                    mDtData.Columns.Add(mFieldName, typeof(System.DateTime));
                                }
                                break;
                            default:
                                {
                                    mFieldValue = "'" + mDataRow["valuestring"].ToString() + "'";
                                    mDtData.Columns.Add(mFieldName, typeof(System.String));
                                }
                                break;
                        }

                        #endregion

                        if (mFields.Trim() == "")
                        {
                            mFields = Environment.NewLine + " " + mFieldValue + " " + mFieldName;
                        }
                        else
                        {
                            mFields = mFields + Environment.NewLine + ", " + mFieldValue + " " + mFieldName;
                        }
                    }
                }

                #endregion

                string mAfterSelectStr = mCommandText.Substring(mCommandText.ToLower().IndexOf("select ") + "select ".Length);
                mNewCommandText = "SELECT " + mFields + ", " + mAfterSelectStr;
                if (mNewFilterString.Trim() != "")
                {
                    mNewCommandText = mNewCommandText + " " + Environment.NewLine + "WHERE " + mNewFilterString;
                }
                if (mGroupByFields.Trim() != "")
                {
                    mNewCommandText = mNewCommandText + " " + Environment.NewLine + "GROUP BY " + mGroupByFields;
                }

                mCommand.CommandText = mNewCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtData);
                mDtData.RemotingFormat = SerializationFormat.Binary;

                return mDtData;
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

        #region Save_ReportTemplate
        public bool Save_ReportTemplate(string mReportCode, byte[] mBytes, int mFormHeight, int mFormWidth, bool mIsForm)
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();

            String mFunctionName = "Save_ReportTemplate";

          

            #region database connection
            

            try
            {
                mConn.ConnectionString = clsGlobal.gAfyaConStr;

                //if (mConn.State != ConnectionState.Open)
                //{
                //    mConn.Open();
                //}
            
                mCommand.Connection = mConn;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return false;
            }

            #endregion

            try
            {
                if (mIsForm == true)
                {
                    DataTable mDtFormSizes = new DataTable("formsizes");
                    string mCommandText = "SELECT * FROM sys_formsizes where formname='"
                    + mReportCode.Trim() + "' and layoutname='defaultsettings'";
                    OdbcDataAdapter mDataAdapter1 = new OdbcDataAdapter(mCommandText, mConn);
                    mDataAdapter1.Fill(mDtFormSizes);
                    if (mDtFormSizes.Rows.Count == 0)
                    {
                        mConn.Open();
                        mCommand.CommandText = "insert into sys_formsizes(formname,layoutname)"
                        + " values('" + mReportCode.Trim() + "','defaultsettings')";
                        mCommand.ExecuteNonQuery();
                        mConn.Close();
                    }

                    mCommandText = "SELECT * FROM sys_formsizes where formname='"
                    + mReportCode.Trim() + "' and layoutname='defaultsettings'";
                    DataSet mDataSet = new DataSet("Image");
                    OdbcDataAdapter mDataAdapter = new OdbcDataAdapter(mCommandText, mConn);
                    OdbcCommandBuilder mCommandBuilder = new OdbcCommandBuilder(mDataAdapter);
                    mDataAdapter.Fill(mDataSet, "Table");

                    try
                    {
                        mConn.Open();
                    }
                    catch { }
                    DataRow mDataRow = mDataSet.Tables["Table"].Rows[0];
                    mDataRow["formlayout"] = mBytes;
                    mDataRow["formwidth"] = mFormWidth;
                    mDataRow["formheight"] = mFormHeight;

                    mDataAdapter.Update(mDataSet, "Table");
                }
                else
                {
                    DataTable mDtReportTemplates = new DataTable("sys_reports");
                    string mCommandText = "SELECT * FROM sys_reports where code='" + mReportCode.Trim() + "'";
                    OdbcDataAdapter mDataAdapter1 = new OdbcDataAdapter(mCommandText, mConn);
                    mDataAdapter1.Fill(mDtReportTemplates);
                    if (mDtReportTemplates.Rows.Count == 0)
                    {
                        mConn.Open();
                        mCommand.CommandText = "insert into sys_reports(code)"
                        + " values('" + mReportCode.Trim() + "')";
                        mCommand.ExecuteNonQuery();
                        mConn.Close();
                    }

                 
                    mCommandText = "SELECT * FROM sys_reports where code='" + mReportCode.Trim() + "'";
                    DataSet mDataSet = new DataSet("Image");
                    OdbcDataAdapter mDataAdapter = new OdbcDataAdapter(mCommandText, mConn);
                    OdbcCommandBuilder mCommandBuilder = new OdbcCommandBuilder(mDataAdapter);
                    mDataAdapter.Fill(mDataSet, "Table");
                   
                    try
                    {
                        mConn.Open();
                    }
                    catch { }
                    DataRow mDataRow = mDataSet.Tables["Table"].Rows[0];
                    mDataRow["reporttemplate"] = mBytes;

                    mDataAdapter.Update(mDataSet, "Table");
                }

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
                    mConn.Close();
                }
                catch { }
            }
        }
        #endregion

        #region Load_ReportTemplate
        public Byte[] Load_ReportTemplate(string mReportCode, bool mIsForm)
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            String mFunctionName = "Load_ReportTemplate";

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
                if (mIsForm == true)
                {
                    DataTable mDtLayouts = new DataTable("formlayouts");
                    mCommand.CommandText = "select formlayout from sys_formsizes where formname='"
                    + mReportCode.Trim() + "' and layoutname='defaultsettings'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtLayouts);

                    try
                    {
                        if (mDtLayouts.Rows.Count > 0)
                        {
                            return (byte[])mDtLayouts.Rows[0]["formlayout"];
                        }
                        else
                        {
                            return null;
                        }
                    }
                    catch { return null; }
                }
                else
                {
                    DataTable mDtTemplates = new DataTable("reporttemplates");
                    mCommand.CommandText = "select reporttemplate from sys_reports where code='" + mReportCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtTemplates);

                    if (mDtTemplates.Rows.Count > 0)
                    {
                        return (byte[])mDtTemplates.Rows[0]["reporttemplate"];
                    }
                    else
                    {
                        return null;
                    }
                }
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

        #region Save_ReportChart
        public AfyaPro_Types.clsResult Save_ReportChart(string mReportCode, DataTable mDtAgeGroups)
        {
            String mFunctionName = "Save_ReportChart";

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

            #region do edit
            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                mCommand.CommandText = "delete from sys_reportcharts where reportcode='" + mReportCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtAgeGroups.Rows)
                {
                    mCommand.CommandText = "insert into sys_reportcharts(reportcode,value1,value2,description) values('"
                    + mReportCode.Trim() + "'," + Convert.ToDouble(mDataRow["value1"]) + ","
                    + Convert.ToDouble(mDataRow["value2"]) + ",'" + mDataRow["description"].ToString().Trim() + "')";
                    mCommand.ExecuteNonQuery();
                }

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
