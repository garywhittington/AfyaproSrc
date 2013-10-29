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
    public class clsUserGroupPrinters : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsUserGroupPrinters";

        #endregion

        #region Get_PrintedWhens
        public DataTable Get_PrintedWhens(string mLanguageName, string mGridName)
        {
            String mFunctionName = "Get_PrintedWhens";

            try
            {
                DataTable mDataTable = new DataTable("printedwhens");
                mDataTable.Columns.Add("code", typeof(System.String));
                mDataTable.Columns.Add("description", typeof(System.String));
                mDataTable.RemotingFormat = SerializationFormat.Binary;

                DataRow mNewRow;

                #region load printedwhens

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "printedwhen" + (int)AfyaPro_Types.clsEnums.PrintedWhens.NewPatientIsRegistered;
                mNewRow["description"] = "New patient is registered";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "printedwhen" + (int)AfyaPro_Types.clsEnums.PrintedWhens.PatientIsReAttending;
                mNewRow["description"] = "Patient is reattending";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "printedwhen" + (int)AfyaPro_Types.clsEnums.PrintedWhens.PatientIsAdmitted;
                mNewRow["description"] = "Patient is admitted";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "printedwhen" + (int)AfyaPro_Types.clsEnums.PrintedWhens.PatientIsDischarged;
                mNewRow["description"] = "Patient is discharged";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                #endregion

                #region interprete data

                if (mLanguageName.Trim() != "" && mGridName.Trim() != "")
                {
                    try
                    {
                        var mCurrLang = from lang in System.Xml.Linq.XElement.Load(
                            AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Lang\" + mLanguageName + ".xml").Elements(mGridName)
                                        select lang;

                        DataTable mDtLanguage = new DataTable("language");
                        mDtLanguage.Columns.Add("controlname");
                        mDtLanguage.Columns.Add("description");

                        foreach (var mElement in mCurrLang)
                        {
                            mNewRow = mDtLanguage.NewRow();
                            mNewRow["controlname"] = (string)mElement.Element("controlname").Value.Trim().ToLower();
                            mNewRow["description"] = (string)mElement.Element("description").Value.Trim();
                            mDtLanguage.Rows.Add(mNewRow);
                            mDtLanguage.AcceptChanges();
                        }

                        DataView mDvLanguage = new DataView();
                        mDvLanguage.Table = mDtLanguage;
                        mDvLanguage.Sort = "controlname";

                        foreach (DataRow mDataRow in mDataTable.Rows)
                        {
                            int mRowIndex = mDvLanguage.Find(mDataRow["code"].ToString().Trim());
                            if (mRowIndex >= 0)
                            {
                                mDataRow.BeginEdit();
                                mDataRow["description"] = mDvLanguage[mRowIndex]["description"].ToString().Trim();
                                mDataRow.EndEdit();
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
        }
        #endregion

        #region View
        public DataTable View(String mFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View";

            #region get printedwhens

            DataTable mDtPrintedWhens = this.Get_PrintedWhens(mLanguageName, mGridName);

            #endregion

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
                string mCommandText = "select * from sys_usergroupprinters";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("sys_usergroupprinters");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                #region add printedwhendescription info

                mDataTable.Columns.Add("printedwhendescription", typeof(System.String));

                DataView mDvPrintedWhens = new DataView();
                mDvPrintedWhens.Table = mDtPrintedWhens;
                mDvPrintedWhens.Sort = "code";

                foreach (DataRow mDataRow in mDataTable.Rows)
                {
                    string mPrintedWhenDescription = "";

                    int mRowIndex = mDvPrintedWhens.Find("printedwhen" + mDataRow["printedwhen"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mPrintedWhenDescription = mDvPrintedWhens[mRowIndex]["description"].ToString().Trim();
                    }

                    mDataRow.BeginEdit();
                    mDataRow["printedwhendescription"] = mPrintedWhenDescription;
                    mDataRow.EndEdit();
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

        #region Edit
        public AfyaPro_Types.clsResult Edit(string mMachineName, string mUserGroupCode, DataTable mDtPrinters, string mUserId)
        {
            string mFunctionName = "Edit";

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

                mCommand.CommandText = "delete from sys_usergroupprinters where usergroupcode='" + mUserGroupCode.Trim()
                + "' and machinename='" + mMachineName.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtPrinters.Rows)
                {
                    string mDocumentType = mDataRow["documenttypecode"].ToString().Trim();
                    Int16 mPrintedWhen = Convert.ToInt16(mDataRow["printedwhen"]);
                    Int16 mAutoPrint = Convert.ToInt16(mDataRow["autoprint"]);
                    Int16 mPrintToScreen = Convert.ToInt16(mDataRow["printtoscreen"]);

                    string mPrinterName = mDataRow["printername"].ToString().Trim();
                    switch (clsGlobal.gAfyaPro_ServerType)
                    {
                        case 0:
                            {
                                mPrinterName = mPrinterName.Replace("\\", "\\\\");
                            }
                            break;
                    }

                    mCommand.CommandText = "insert into sys_usergroupprinters(machinename,usergroupcode,documenttypecode,printername,"
                    + "printedwhen,autoprint,printtoscreen) values('" + mMachineName.Trim() + "','" + mUserGroupCode.Trim() + "','" + mDocumentType + "','"
                    + @mPrinterName + "'," + mPrintedWhen + "," + mAutoPrint + "," + mPrintToScreen + ")";
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
