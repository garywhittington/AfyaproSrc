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
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Linq;

namespace AfyaPro_MT
{
    public class clsBillDebtReliefs : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsBillDebtReliefRequests";

        #endregion

        #region View
        public DataTable View(bool mDateSpecified, DateTime mDateFrom, DateTime mDateTo, String mExtraFilter, String mOrder, string mLanguageName, string mGridName)
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

                string mCommandText = "select * from billdebtreliefrequests";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mExtraFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("billdebtreliefrequests");
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

        #region View_Invoices
        public DataTable View_Invoices(String mFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_Invoices";

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
                string mCommandText = "select * from billdebtreliefrequestinvoices";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("billdebtreliefrequestinvoices");
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

        #region Request

        public AfyaPro_Types.clsBill Request(Int16 mGenerateRequestNo, string mRequestNo, DateTime mTransDate, 
            string mAccountCode, string mAccountDescription, string mDebtorType, string mReasonForRelief, 
            double mTotalBalanceDue, double mTotalRequestedAmount, DataTable mDtInvoices, string mUserId)
        {
            AfyaPro_Types.clsBill mBill = new AfyaPro_Types.clsBill();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            String mFunctionName = "Request";

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
                mBill.Exe_Result = -1;
                mBill.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBill;
            }

            #endregion

            #region auto generate receiptno, if option is on

            if (mGenerateRequestNo == 1)
            {
                clsAutoCodes objAutoCodes = new clsAutoCodes();
                AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                mObjCode = objAutoCodes.Next_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.debtreliefrequestno), "billdebtreliefrequests", "requestno");
                if (mObjCode.Exe_Result == -1)
                {
                    mBill.Exe_Result = 0;
                    mBill.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DuplicateReceiptNoDetected.ToString();
                    return mBill;
                }
                mRequestNo = mObjCode.GeneratedCode;
            }

            #endregion

            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                #region billdebtreliefrequests

                List<clsDataField> mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("sysdate", DataTypes.dbdatetime.ToString(), DateTime.Now));
                mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mTransDate));
                mDataFields.Add(new clsDataField("requestno", DataTypes.dbstring.ToString(), mRequestNo.Trim()));
                mDataFields.Add(new clsDataField("accountcode", DataTypes.dbstring.ToString(), mAccountCode.Trim()));
                mDataFields.Add(new clsDataField("accountdescription", DataTypes.dbstring.ToString(), mAccountDescription.Trim()));
                mDataFields.Add(new clsDataField("debtortype", DataTypes.dbstring.ToString(), mDebtorType.Trim()));
                mDataFields.Add(new clsDataField("reasonforrelief", DataTypes.dbstring.ToString(), mReasonForRelief.Trim()));
                mDataFields.Add(new clsDataField("totalbalancedue", DataTypes.dbdecimal.ToString(), mTotalBalanceDue));
                mDataFields.Add(new clsDataField("totalrequestedamount", DataTypes.dbdecimal.ToString(), mTotalRequestedAmount));
                mDataFields.Add(new clsDataField("requeststatus", DataTypes.dbnumber.ToString(), (int)AfyaPro_Types.clsEnums.DebtReliefRequestStatus.Open));
                mDataFields.Add(new clsDataField("requestedby", DataTypes.dbstring.ToString(), mUserId.Trim()));

                mCommand.CommandText = clsGlobal.Get_InsertStatement("billdebtreliefrequests", mDataFields);
                mCommand.ExecuteNonQuery();

                #endregion

                #region billdebtreliefrequestinvoices

                foreach (DataRow mDataRow in mDtInvoices.Rows)
                {
                    DateTime mInvoiceDate = Convert.ToDateTime(mDataRow["transdate"]);
                    string mInvoiceNo = mDataRow["invoiceno"].ToString().Trim();
                    string mBooking = mDataRow["booking"].ToString().Trim();
                    double mBalanceDue = Convert.ToDouble(mDataRow["balancedue"]);
                    double mRequestedAmount = Convert.ToDouble(mDataRow["requestedamount"]);

                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("requestno", DataTypes.dbstring.ToString(), mRequestNo.Trim()));
                    mDataFields.Add(new clsDataField("transdate", DataTypes.dbdatetime.ToString(), mInvoiceDate));
                    mDataFields.Add(new clsDataField("invoiceno", DataTypes.dbstring.ToString(), mInvoiceNo.Trim()));
                    mDataFields.Add(new clsDataField("booking", DataTypes.dbstring.ToString(), mBooking.Trim()));
                    mDataFields.Add(new clsDataField("balancedue", DataTypes.dbdecimal.ToString(), mBalanceDue));
                    mDataFields.Add(new clsDataField("requestedamount", DataTypes.dbdecimal.ToString(), mRequestedAmount));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("billdebtreliefrequestinvoices", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                if (mGenerateRequestNo == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.debtreliefrequestno);
                    mCommand.ExecuteNonQuery();
                }

                //commit transaction
                mTrans.Commit();

                return mBill;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mBill.Exe_Result = -1;
                mBill.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBill;
            }
            finally
            {
                mConn.Close();
            }
        }

        #endregion

        #region Reject

        public AfyaPro_Types.clsBill Reject(string mRequestNo, DateTime mTransDate,
            string mAccountCode, string mRemarks, string mUserId)
        {
            AfyaPro_Types.clsBill mBill = new AfyaPro_Types.clsBill();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            String mFunctionName = "Reject";

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
                mBill.Exe_Result = -1;
                mBill.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBill;
            }

            #endregion

            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                string mDebtorType = AfyaPro_Types.clsEnums.DebtorTypes.Individual.ToString();

                List<clsDataField> mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("approveddate", DataTypes.dbdatetime.ToString(), mTransDate));
                mDataFields.Add(new clsDataField("approvalremarks", DataTypes.dbstring.ToString(), mRemarks));
                mDataFields.Add(new clsDataField("approvedby", DataTypes.dbstring.ToString(), mUserId.Trim()));
                mDataFields.Add(new clsDataField("requeststatus", DataTypes.dbnumber.ToString(), (int)AfyaPro_Types.clsEnums.DebtReliefRequestStatus.Rejected));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("billdebtreliefrequests", mDataFields,
                    "requestno='" + mRequestNo.Trim() + "' and accountcode='" + mAccountCode.Trim() + "' and debtortype='" + mDebtorType.Trim() + "'");
                mCommand.ExecuteNonQuery();

                //commit transaction
                mTrans.Commit();

                return mBill;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mBill.Exe_Result = -1;
                mBill.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBill;
            }
            finally
            {
                mConn.Close();
            }
        }

        #endregion
    }
}
