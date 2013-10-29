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
    public class clsBilling : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsBilling";

        #endregion

        #region View_Sales
        public DataTable View_Sales(bool mDateSpecified, DateTime mDateFrom, DateTime mDateTo, String mExtraFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_Sales";

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

                //columns from patients
                string mPatientColumns = clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "patient");
                mPatientColumns = mPatientColumns + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "patientfullname");
                //columns from billsales
                string mBillSalesColumns = clsGlobal.Get_TableColumns(mCommand, "billsales", "", "b", "");
                
                string mCommandText = ""
                    + "SELECT "
                    + mBillSalesColumns + ","
                    + mPatientColumns + " "
                    + "FROM billsales AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " WHERE " + mExtraFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " ORDER BY " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("billsales");
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

        #region View_SalesItems
        public DataTable View_SalesItems(bool mDateSpecified, DateTime mDateFrom, DateTime mDateTo, String mExtraFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_SalesItems";

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

                string mCommandText = "select * from billsalesitems";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mExtraFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("billsalesitems");
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
        public DataTable View_Invoices(bool mDateSpecified, DateTime mDateFrom, DateTime mDateTo, String mExtraFilter, String mOrder, string mLanguageName, string mGridName)
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

                //columns from patients
                string mPatientColumns = clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "patient");
                mPatientColumns = mPatientColumns + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "patientfullname");
                //columns from billinvoices
                string mBillInvoicesColumns = clsGlobal.Get_TableColumns(mCommand, "billinvoices", "", "b", "");

                string mCommandText = ""
                    + "SELECT "
                    + mBillInvoicesColumns + ","
                    + mPatientColumns + " "
                    + "FROM billinvoices AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where (" + mExtraFilter + ")";
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("billinvoices");
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

        #region View_InvoiceItems
        public DataTable View_InvoiceItems(bool mDateSpecified, DateTime mDateFrom, DateTime mDateTo, String mExtraFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_InvoiceItems";

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

                string mCommandText = "select * from billinvoiceitems";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mExtraFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("billinvoiceitems");
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

        #region View_Receipts
        public DataTable View_Receipts(bool mDateSpecified, DateTime mDateFrom, DateTime mDateTo, String mExtraFilter, String mOrder, string mLanguageName, string mGridName)
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

                //columns from patients
                string mPatientColumns = clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "patient");
                mPatientColumns = mPatientColumns + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "patientfullname");
                //columns from billreceipts
                string mBillReceiptsColumns = clsGlobal.Get_TableColumns(mCommand, "billreceipts", "", "b", "");

                string mCommandText = ""
                    + "SELECT "
                    + mBillReceiptsColumns + ","
                    + mPatientColumns + " "
                    + "FROM billreceipts AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mExtraFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("billreceipts");
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
        public DataTable View_ReceiptItems(bool mDateSpecified, DateTime mDateFrom, DateTime mDateTo, String mExtraFilter, String mOrder, string mLanguageName, string mGridName)
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

                string mCommandText = "select * from billreceiptitems";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mExtraFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("billreceiptitems");
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

        #region View_InvoicePayments
        public DataTable View_InvoicePayments(bool mDateSpecified, DateTime mDateFrom, DateTime mDateTo, String mExtraFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_InvoicePayments";

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

                //columns from patients
                string mPatientColumns = clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "patient");
                mPatientColumns = mPatientColumns + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "patientfullname");
                //columns from billinvoicepayments
                string mBillInvoicePaymentsColumns = clsGlobal.Get_TableColumns(mCommand, "billinvoicepayments", "", "b", "");

                string mCommandText = ""
                    + "SELECT "
                    + mBillInvoicePaymentsColumns + ","
                    + mPatientColumns + " "
                    + "FROM billinvoicepayments AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mExtraFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("billinvoicepayments");
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

        #region View_InvoicePaymentDetails
        public DataTable View_InvoicePaymentDetails(bool mDateSpecified, DateTime mDateFrom, DateTime mDateTo, String mExtraFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_InvoicePaymentDetails";

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

                string mCommandText = "select * from billinvoicepaymentdetails";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mExtraFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("billinvoicepaymentdetails");
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

        #region View_VoidedSales
        public DataTable View_VoidedSales(bool mDateSpecified, DateTime mDateFrom, DateTime mDateTo, String mExtraFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_VoidedSales";

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

                //columns from patients
                string mPatientColumns = clsGlobal.Get_TableColumns(mCommand, "patients", "autocode,code,weight,temperature", "p", "patient");
                mPatientColumns = mPatientColumns + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "patientfullname");
                //columns from billvoidedsales
                string mBillVoidedSalesColumns = clsGlobal.Get_TableColumns(mCommand, "billvoidedsales", "", "b", "");

                string mCommandText = ""
                    + "SELECT "
                    + mBillVoidedSalesColumns + ","
                    + mPatientColumns + " "
                    + "FROM billvoidedsales AS b "
                    + "LEFT OUTER JOIN patients AS p ON b.patientcode=p.code";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mExtraFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("billinvoices");
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

        #region View_VoidedSalesItems
        public DataTable View_VoidedSalesItems(bool mDateSpecified, DateTime mDateFrom, DateTime mDateTo, String mExtraFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_VoidedSalesItems";

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

                string mCommandText = "select * from billvoidedsalesitems";

                if (mExtraFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mExtraFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("billvoidedsalesitems");
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

        #region View_CollectionByReceipt
        public DataTable View_CollectionByReceipt(string mReceiptNo, Int16 mPaymentSource, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_CollectionByReceipt";

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
                DataTable mDtCollections = new DataTable("billcollections");
                mCommand.CommandText = "select * from billcollections where 1=2";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtCollections);

                string mSumColumns = "";
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
                    }
                }

                mDtCollections = new DataTable("billcollections");
                mCommand.CommandText = "select bank,branch,holder,chequeno," + mSumColumns
                + " from billcollections where receiptno='" + mReceiptNo + "' and paymentsource="
                + mPaymentSource + " group by bank,branch,holder,chequeno";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtCollections);

                DataTable mDtPaymentTypes = new DataTable("paymenttypes");
                mCommand.CommandText = "select * from facilitypaymenttypes";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtPaymentTypes);

                DataView mDvPaymentTypes = new DataView();
                mDvPaymentTypes.Table = mDtPaymentTypes;
                mDvPaymentTypes.Sort = "code";

                DataTable mDtPaymentsMade = new DataTable("paymentsmade");
                mDtPaymentsMade.Columns.Add("paytypecode", typeof(System.String));
                mDtPaymentsMade.Columns.Add("paytypedescription", typeof(System.String));
                mDtPaymentsMade.Columns.Add("allowrefund", typeof(System.Int16));
                mDtPaymentsMade.Columns.Add("bank", typeof(System.String));
                mDtPaymentsMade.Columns.Add("branch", typeof(System.String));
                mDtPaymentsMade.Columns.Add("holder", typeof(System.String));
                mDtPaymentsMade.Columns.Add("chequeno", typeof(System.String));
                mDtPaymentsMade.Columns.Add("paidamount", typeof(System.String));
                mDtPaymentsMade.Columns.Add("refundedamount", typeof(System.Double));
                mDtPaymentsMade.RemotingFormat = SerializationFormat.Binary;

                foreach (DataRow mDataRow in mDtCollections.Rows)
                {
                    foreach (DataColumn mDataColumn in mDtCollections.Columns)
                    {
                        if (mDataColumn.ColumnName.ToLower().StartsWith("paytype") == true)
                        {
                            string mPayTypeCode = mDataColumn.ColumnName.ToLower();
                            string mPayTypeDescription = mPayTypeCode;
                            Int16 mAllowRefund = 1;

                            int mRowIndex = mDvPaymentTypes.Find(mPayTypeCode.Substring("paytype".Length));
                            if (mRowIndex >= 0)
                            {
                                mPayTypeDescription = mDvPaymentTypes[mRowIndex]["description"].ToString().Trim();
                                mAllowRefund = Convert.ToInt16(mDvPaymentTypes[mRowIndex]["allowrefund"]);
                            }

                            string mBank = mDataRow["bank"].ToString().Trim();
                            string mBranch = mDataRow["branch"].ToString().Trim();
                            string mHolder = mDataRow["holder"].ToString().Trim();
                            string mChequeNo = mDataRow["chequeno"].ToString().Trim();
                            double mPaidAmount = Convert.ToDouble(mDataRow[mPayTypeCode]);

                            DataRow mNewRow = mDtPaymentsMade.NewRow();
                            mNewRow["paytypecode"] = mPayTypeCode;
                            mNewRow["paytypedescription"] = mPayTypeDescription;
                            mNewRow["allowrefund"] = mAllowRefund;
                            mNewRow["bank"] = mBank;
                            mNewRow["branch"] = mBranch;
                            mNewRow["holder"] = mHolder;
                            mNewRow["chequeno"] = mChequeNo;
                            mNewRow["paidamount"] = mPaidAmount;
                            mNewRow["refundedamount"] = 0;
                            mDtPaymentsMade.Rows.Add(mNewRow);
                            mDtPaymentsMade.AcceptChanges();
                        }
                    }
                }

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
                            if (mDtPaymentsMade.Columns.Contains((string)mElement.Element("controlname").Value.Trim().Substring(3)) == true)
                            {
                                mDtPaymentsMade.Columns[(string)mElement.Element("controlname").Value.Trim().Substring(3)].Caption =
                                    (string)mElement.Element("description").Value.Trim();
                            }
                        }
                    }
                    catch { }
                }

                #endregion

                return mDtPaymentsMade;
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

        #region View_BilledItems
        public DataTable View_BilledItems(String mFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_BilledItems";

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
                string mCommandText = "select * from view_billpatientbilleditems";
                mFilter = mFilter +" and transdate=" + clsGlobal.Saving_DateValue(DateTime.Now.Date);
                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter ;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("billeditems");
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

        #region View_IncomingItems
        public DataTable View_IncomingItems(String mFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_IncomingItems";

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
                string mCommandText = "select * from billincomingitems";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("billincomingitems");
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
        
        #region Get_DebtorBalances
        public DataTable Get_DebtorBalances(String mFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "Get_DebtorBalances";

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
                string mCommandText = "select accountcode,balancedue balance from view_debtorbalance";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                mCommandText = mCommandText + " group by accountcode";

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("billdebtors");
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

        #region Get_IncomingBalances
        public DataTable Get_IncomingBalances(String mFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "Get_IncomingBalances";

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
                string mCommandText = "select patientcode,sum(amount) amount from billincomingitems";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                mCommandText = mCommandText + " group by patientcode";

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("billincomingitems");
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

        #region Get_PatientAccountBalances
        public DataTable Get_PatientAccountBalances(string mPatientCode, string mLanguageName, string mGridName)
        {
            String mFunctionName = "Get_PatientAccountBalances";

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
                //get account codes for this patient
                DataTable mDtAccountUsers = new DataTable("billaccountusers");
                mCommand.CommandText = "select * from billaccountusers where membercode='" + mPatientCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtAccountUsers);

                //build filter for accounts
                string mAccountsFilter = "";
                foreach (DataRow mDataRow in mDtAccountUsers.Rows)
                {
                    if (mAccountsFilter.Trim() == "")
                    {
                        mAccountsFilter = "code='" + mDataRow["accountcode"].ToString().Trim() + "'";
                    }
                    else
                    {
                        mAccountsFilter = mAccountsFilter + " or code='" + mDataRow["accountcode"].ToString().Trim() + "'";
                    }
                }

                if (mAccountsFilter == "")
                {
                    mAccountsFilter = "1=2";
                }

                //get accounts
                mCommand.CommandText = "select code,description,balance from billaccounts where " + mAccountsFilter;
                DataTable mDataTable = new DataTable("billaccounts");
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

        #region Bill_Patient
        public AfyaPro_Types.clsBill Bill_Patient(Int16 mGenerateInvoice, Int16 mGenerateReceipt, string mInvoiceNo, string mReceiptNo,
            DateTime mTransDate, string mPatientCode, string mBillingGroupCode, string mBillingSubGroupCode, string mBillingGroupMembershipNo,
            string mPriceCategory, double mAmtDue, double mDiscount, double mTotalPaid, string mCurrencyCode, string mCurrencyDescription, string mCurrencySymbol,
            string mCompanyDescription, DataTable mDtPaymentTypes, DataTable mDtItems, bool mDirectStockIssuing, bool mProcessIncoming,
            bool mAffectBillsDirect, DataTable mDtBookingDetails, DataTable mDtAdmissionDetails, DataTable mDtDischargeDetails,
            AfyaPro_Types.clsPatientDiagnosis mPatientDiagnosis, string mUserId, string mUserName)
        {
            String mFunctionName = "Bill_Patient";

            AfyaPro_Types.clsBill mBill = new AfyaPro_Types.clsBill();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            DataTable mDtBookings = new DataTable("bookings");
            DataTable mDtPatientCorporates = new DataTable("patientcorporates");
            string mNewBooking = "";
            DateTime mPrevBookDate = new DateTime();
            string mPrevDepartment = "";
            bool mIsReAttendance = false;
            bool mIsForcedReAttendance = false;
            int mRecsAffected = 0;
            string mSurname = "";
            string mFirstName = "";
            string mOtherNames = "";
            string mGender = "";
            string mBillingGroup = "";
            string mBillingSubGroup = "";
            string mPaymentFieldNames = "";
            string mPaymentFieldValues = "";

            double mChange = 0;
            double mAmtToInvoice = 0;
            double mAmtPaid = 0;

            DateTime mBirthDate = new DateTime();
            DateTime mRegDate = new DateTime();

            string mWardCode = "";
            string mRemarks = "";
            string mBedCode = "";
            string mRoomCode = "";
            string mDischargeStatusCode = "";
            int mAdmissionId = 0;

            #region get booking details

            bool mIsBooking = false;
            string mTreatmentPointCode = "";
            string mTreatmentPoint = "";
            double mWeight = 0;
            double mTemperature = 0;
            if (mDtBookingDetails.Rows.Count > 0)
            {
                mIsBooking = true;
                mTreatmentPointCode = mDtBookingDetails.Rows[0]["treatmentpointcode"].ToString().Trim();
                mTreatmentPoint = mDtBookingDetails.Rows[0]["treatmentpoint"].ToString().Trim();
                mWeight = Convert.ToDouble(mDtBookingDetails.Rows[0]["weight"]);
                mTemperature = Convert.ToDouble(mDtBookingDetails.Rows[0]["temperature"]);
            }

            #endregion

            #region get admission details
            bool mAdmitting = false;
            if (mDtAdmissionDetails.Rows.Count > 0)
            {
                mAdmitting = true;
                mTreatmentPointCode = "IPD";
                mTreatmentPoint = "IPD";
                mAdmissionId = Convert.ToInt32(mDtAdmissionDetails.Rows[0]["admissionid"]);
                mWardCode = mDtAdmissionDetails.Rows[0]["wardcode"].ToString().Trim();
                mRoomCode = mDtAdmissionDetails.Rows[0]["roomcode"].ToString().Trim();
                mRemarks = mDtAdmissionDetails.Rows[0]["remarks"].ToString().Trim();
                mBedCode = mDtAdmissionDetails.Rows[0]["bedno"].ToString().Trim();
                mWeight = Convert.ToDouble(mDtAdmissionDetails.Rows[0]["weight"]);
                mTemperature = Convert.ToDouble(mDtAdmissionDetails.Rows[0]["temperature"]);
            }
            #endregion

            #region get discharge details
            bool mDischarging = false;
            if (mDtDischargeDetails.Rows.Count > 0)
            {
                mDischarging = true;
                mAdmissionId = Convert.ToInt32(mDtDischargeDetails.Rows[0]["admissionid"]);
                mWardCode = mDtDischargeDetails.Rows[0]["wardcode"].ToString().Trim();
                mRoomCode = mDtDischargeDetails.Rows[0]["roomcode"].ToString().Trim();
                mRemarks = mDtDischargeDetails.Rows[0]["remarks"].ToString().Trim();
                mBedCode = mDtDischargeDetails.Rows[0]["bedno"].ToString().Trim();
                mDischargeStatusCode = mDtDischargeDetails.Rows[0]["dischargestatuscode"].ToString().Trim();
            }
            #endregion

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

            #region this will update weight & temperature for this visit
            if (mIsBooking == true || mAdmitting == true)
            {
                if (mWeight > 0 || mTemperature > 0)
                {
                    try
                    {
                        mCommand.CommandText = "update patients set weight=" + mWeight + ",temperature=" + mTemperature
                        + " where code='" + mPatientCode.Trim() + "'";
                        mCommand.ExecuteNonQuery();
                    }
                    catch (OdbcException ex)
                    {
                        mBill.Exe_Result = -1;
                        mBill.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                        return mBill;
                    }
                }
            }
            #endregion

            #region check for patient existance

            try
            {
                DataTable mDtPatients = new DataTable("patients");
                mCommand.CommandText = "select * from patients where code='" + mPatientCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtPatients);

                if (mDtPatients.Rows.Count > 0)
                {
                    mSurname = mDtPatients.Rows[0]["surname"].ToString();
                    mFirstName = mDtPatients.Rows[0]["firstname"].ToString();
                    mOtherNames = mDtPatients.Rows[0]["othernames"].ToString();
                    mGender = mDtPatients.Rows[0]["gender"].ToString();
                    mBirthDate = Convert.ToDateTime(mDtPatients.Rows[0]["birthdate"]);
                    mRegDate = Convert.ToDateTime(mDtPatients.Rows[0]["regdate"]);
                }
                else
                {
                    mBill.Exe_Result = 0;
                    mBill.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoDoesNotExist.ToString();
                    return mBill;
                }
            }
            catch (OdbcException ex)
            {
                mBill.Exe_Result = -1;
                mBill.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBill;
            }

            #endregion

            try
            {
                #region prepare payments

                DataTable mDtAllPaymentTypes = new DataTable("facilitypaymenttypes");
                mCommand.CommandText = "select * from facilitypaymenttypes";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtAllPaymentTypes);

                foreach (DataRow mDataRow in mDtPaymentTypes.Rows)
                {
                    mAmtPaid += Convert.ToDouble(mDataRow["paidforbill"]);
                }

                foreach (DataRow mDataRow in mDtAllPaymentTypes.Rows)
                {
                    double mPayTypeValue = 0;
                    foreach (DataRow mPayTypeRow in mDtPaymentTypes.Rows)
                    {
                        if (mPayTypeRow["paymenttypecode"].ToString().Trim().ToLower() ==
                            mDataRow["code"].ToString().Trim().ToLower())
                        {
                            mPayTypeValue = mPayTypeValue + Convert.ToDouble(mPayTypeRow["paidforbill"]);
                        }
                    }

                    mPaymentFieldNames = mPaymentFieldNames + ",paytype" + mDataRow["code"].ToString().Trim().ToLower();
                    mPaymentFieldValues = mPaymentFieldValues + "," + mPayTypeValue;
                }

                #endregion

                mAmtToInvoice = mAmtDue - mDiscount;
                mChange = mTotalPaid - mAmtToInvoice;
                Int16 mPaymentSource = Convert.ToInt16(AfyaPro_Types.clsEnums.PaymentSources.CashSale);

                #region auto generate receipt/invoice #

                if (mAffectBillsDirect == true)
                {
                    clsAutoCodes objAutoCodes = new clsAutoCodes();
                    AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();

                    mBill.HasReceipt = false;
                    mBill.HasInvoice = false;

                    #region receiptno if any

                    if (mAmtPaid > 0)
                    {
                        mBill.HasReceipt = true;

                        if (mGenerateReceipt == 1)
                        {
                            mObjCode = objAutoCodes.Next_Code(
                                Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.receiptno), "billreceipts", "receiptno");

                            if (mObjCode.Exe_Result == -1)
                            {
                                mBill.Exe_Result = mObjCode.Exe_Result;
                                mBill.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                                return mBill;
                            }
                            mReceiptNo = mObjCode.GeneratedCode;
                        }

                        //check duplicate receiptno
                        DataTable mDtReceipts = new DataTable("billreceipts");
                        mCommand.CommandText = "select receiptno from billreceipts where receiptno='" + mReceiptNo.Trim() + "'";
                        mDataAdapter.SelectCommand = mCommand;
                        mDataAdapter.Fill(mDtReceipts);

                        if (mDtReceipts.Rows.Count > 0)
                        {
                            mBill.Exe_Result = 0;
                            mBill.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DuplicateReceiptNoDetected.ToString();
                            return mBill;
                        }
                    }
                    else
                    {
                        mReceiptNo = "";
                    }

                    #endregion

                    #region invoiceno if any

                    if (mAmtPaid < mAmtToInvoice)
                    {
                        mBill.HasInvoice = true;

                        #region invoiceno

                        if (mGenerateInvoice == 1)
                        {
                            mObjCode = objAutoCodes.Next_Code(
                                Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.invoiceno), "billinvoices", "invoiceno");

                            if (mObjCode.Exe_Result == -1)
                            {
                                mBill.Exe_Result = mObjCode.Exe_Result;
                                mBill.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                                return mBill;
                            }
                            mInvoiceNo = mObjCode.GeneratedCode;
                        }

                        //check duplicate invoiceno
                        DataTable mDtInvoices = new DataTable("billinvoices");
                        mCommand.CommandText = "select invoiceno from billinvoices where invoiceno='" + mInvoiceNo.Trim() + "'";
                        mDataAdapter.SelectCommand = mCommand;
                        mDataAdapter.Fill(mDtInvoices);

                        if (mDtInvoices.Rows.Count > 0)
                        {
                            mBill.Exe_Result = 0;
                            mBill.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DuplicateInvoiceNoDetected.ToString();
                            return mBill;
                        }

                        #endregion

                        #region payment receiptno

                        if (mBill.HasReceipt == true)
                        {
                            mPaymentSource = Convert.ToInt16(AfyaPro_Types.clsEnums.PaymentSources.InvoicePayment);

                            if (mGenerateReceipt == 1)
                            {
                                mObjCode = objAutoCodes.Next_Code(
                                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.paymentno), "billinvoicepayments", "receiptno");

                                if (mObjCode.Exe_Result == -1)
                                {
                                    mBill.Exe_Result = mObjCode.Exe_Result;
                                    mBill.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                                    return mBill;
                                }
                                mReceiptNo = mObjCode.GeneratedCode;
                            }

                            //check duplicate receiptno
                            DataTable mDtReceipts = new DataTable("billinvoicepayments");
                            mCommand.CommandText = "select receiptno from billinvoicepayments where receiptno='" + mReceiptNo.Trim() + "'";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(mDtReceipts);

                            if (mDtReceipts.Rows.Count > 0)
                            {
                                mBill.Exe_Result = 0;
                                mBill.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DuplicateReceiptNoDetected.ToString();
                                return mBill;
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        mInvoiceNo = "";
                    }

                    #endregion
                }

                #endregion

                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                int mYearPart = mTransDate.Year;
                int mMonthPart = mTransDate.Month;

                clsWorkFlowAgent mWorkFlowAgent = new clsWorkFlowAgent();
                clsRegistrations mMdtRegistrations;
                string mIPDDiagnosisType = "";

                #region do booking

                AfyaPro_Types.clsBooking mBooking;

                if (mIsBooking == true || mAdmitting == true)
                {
                    mBooking = mWorkFlowAgent.Do_Booking(
                        mCommand,
                        mTransDate,
                        mPatientCode,
                        mBillingGroupCode,
                        mBillingSubGroupCode,
                        mBillingGroupMembershipNo,
                        mPriceCategory,
                        mTreatmentPointCode,
                        mTreatmentPoint,
                        mWeight,
                        mTemperature,
                        mAdmitting,
                        mUserId);
                }
                else
                {
                    mBooking = mWorkFlowAgent.Get_Booking(mCommand, mPatientCode);
                }

                if (mBooking.Exe_Result != 1)
                {
                    try { mTrans.Rollback(); }
                    catch { }
                    mBill.Exe_Result = mBooking.Exe_Result;
                    mBill.Exe_Message = mBooking.Exe_Message;
                    return mBill;
                }

                mNewBooking = mBooking.Booking;
                mPrevBookDate = mBooking.BookDate;
                mPrevDepartment = mBooking.Department;
                mIsReAttendance = !mBooking.IsNewAttendance;
                mBillingGroup = mBooking.BillingGroupDescription;
                mBillingSubGroup = mBooking.BillingSubGroupDescription;

                #endregion

                #region admission

                if (mAdmitting == true)
                {
                    AfyaPro_Types.clsBooking mAdmission = mWorkFlowAgent.Do_Admission(
                        mCommand,
                        mAdmissionId,
                        mTransDate,
                        mPatientCode,
                        mWardCode,
                        mRoomCode,
                        mBedCode,
                        mRemarks,
                        mWeight,
                        mTemperature,
                        mBooking,
                        mUserId);

                    if (mAdmission.Exe_Result != 1)
                    {
                        try { mTrans.Rollback(); }
                        catch { }
                        mBill.Exe_Result = mAdmission.Exe_Result;
                        mBill.Exe_Message = mAdmission.Exe_Message;
                        return mBill;
                    }

                    mIPDDiagnosisType = "Admission";
                }

                #endregion

                #region discharging

                if (mDischarging == true)
                {
                    AfyaPro_Types.clsBooking mDischarge = mWorkFlowAgent.Do_Discharging(
                        mCommand,
                        mAdmissionId,
                        mTransDate,
                        mPatientCode,
                        mWardCode,
                        mRoomCode,
                        mBedCode,
                        mDischargeStatusCode,
                        mRemarks,
                        mWeight,
                        mTemperature,
                        mBooking,
                        mUserId);

                    if (mDischarge.Exe_Result != 1)
                    {
                        try { mTrans.Rollback(); }
                        catch { }
                        mBill.Exe_Result = mDischarge.Exe_Result;
                        mBill.Exe_Message = mDischarge.Exe_Message;
                        return mBill;
                    }

                    mIPDDiagnosisType = "Discharge";
                }

                #endregion

                #region diagnosesandtreatments

                if (mPatientDiagnosis != null)
                {
                    #region check security for this function
                    bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                        AfyaPro_Types.clsSystemFunctions.FunctionKeys.dxtpatientdiagnoses_add.ToString(), mUserId);
                    if (mGranted == false)
                    {
                        try { mTrans.Rollback(); }
                        catch { }
                        mBill.Exe_Result = 0;
                        mBill.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                        return mBill;
                    }
                    #endregion

                    AfyaPro_Types.clsResult mDiagnosesResult = mWorkFlowAgent.Add_Diagnoses(
                            mCommand,
                            mTransDate,
                            mPatientCode,
                            mPatientDiagnosis.diagnosiscode,
                            mPatientDiagnosis.episodecode,
                            mPatientDiagnosis.isprimary,
                            mIPDDiagnosisType,
                            mPatientDiagnosis.history,
                            mPatientDiagnosis.examination,
                            mPatientDiagnosis.investigation,
                            mPatientDiagnosis.treatments,
                            mPatientDiagnosis.referaldescription,
                            mPatientDiagnosis.doctorcode,
                            mWeight,
                            mTemperature,
                            mBooking,
                            mUserId);

                    if (mDiagnosesResult.Exe_Result != 1)
                    {
                        try { mTrans.Rollback(); }
                        catch { }
                        mBill.Exe_Result = mDiagnosesResult.Exe_Result;
                        mBill.Exe_Message = mDiagnosesResult.Exe_Message;
                        return mBill;
                    }
                }

                #endregion

                #region billing

                AfyaPro_Types.clsAdmission mAdmissionDetails = new AfyaPro_Types.clsAdmission();
                mMdtRegistrations = new clsRegistrations();
                mAdmissionDetails = mMdtRegistrations.Get_Admission(mNewBooking, mPatientCode);

                if (mAdmitting == true || mDischarging == true)
                {
                    mAdmissionDetails.WardCode = mWardCode;
                    mAdmissionDetails.RoomCode = mRoomCode;
                    mAdmissionDetails.BedCode = mBedCode;
                }

                if (mAffectBillsDirect == false)
                {
                    #region billincomingitems

                    foreach (DataRow mDataRow in mDtItems.Rows)
                    {
                        int mAutoCode = Convert.ToInt32(mDataRow["autocode"]);

                        if (mAutoCode <= 0)
                        {
                            mCommand.CommandText = "insert into billincomingitems(sysdate,transdate,patientcode,wardcode,roomcode,bedcode,itemgroupcode,"
                            + "itemgroupdescription,itemsubgroupcode,itemsubgroupdescription,itemcode,itemdescription,expirydate,"
                            + "storecode,qty,actualamount,amount,userid,display) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                            + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mPatientCode.Trim() + "','"
                            + mAdmissionDetails.WardCode + "','" + mAdmissionDetails.RoomCode + "','" + mAdmissionDetails.BedCode + "','"
                            + mDataRow["itemgroupcode"].ToString().Trim() + "','" + mDataRow["itemgroupdescription"].ToString().Trim() + "','"
                            + mDataRow["itemsubgroupcode"].ToString().Trim() + "','" + mDataRow["itemsubgroupdescription"].ToString().Trim() + "','"
                            + mDataRow["itemcode"].ToString().Trim() + "','" + mDataRow["itemdescription"].ToString().Trim()
                            + "'," + clsGlobal.Saving_DateValueNullable(mDataRow["expirydate"]) + ",'"
                            + mDataRow["storecode"].ToString().Trim() + "'," + Convert.ToDouble(mDataRow["qty"]) + ","
                            + Convert.ToDouble(mDataRow["actualamount"]) + "," + Convert.ToDouble(mDataRow["amount"]) + ",'" + mUserId.Trim() + "',1)";
                            mCommand.ExecuteNonQuery();
                        }
                    }

                    #endregion
                }
                else
                {
                    string mTransDescription1 = "Sales";
                    string mTransDescription2 = "Payment";
                    string mDebtorType = AfyaPro_Types.clsEnums.DebtorTypes.Individual.ToString();
                    string mAccountCode = mPatientCode.Trim();
                    string mAccountDescription = mFirstName;
                    string mFromWhomToWhom = mFirstName;
                    string mFromWhomToWhomCode = mPatientCode;
                    if (mOtherNames.Trim() != "")
                    {
                        mFromWhomToWhom = mFromWhomToWhom + " " + mOtherNames.Trim();
                        mAccountDescription = mAccountDescription + " " + mOtherNames.Trim();
                    }
                    mFromWhomToWhom = mFromWhomToWhom + " " + mSurname.Trim();
                    mAccountDescription = mAccountDescription + " " + mSurname.Trim();
                    if (mBillingGroupCode.Trim() != "")
                    {
                        mAccountCode = mBillingGroupCode.Trim();
                        mAccountDescription = mBillingGroup.Trim();
                        mDebtorType = AfyaPro_Types.clsEnums.DebtorTypes.Group.ToString();
                    }

                    #region sales

                    #region billsales

                    mCommand.CommandText = "insert into billsales(sysdate,transdate,receiptno,invoiceno,patientcode,wardcode,roomcode,"
                    + "bedcode,billinggroupcode,billinggroupdescription,billingsubgroupcode,billingsubgroupdescription,billinggroupmembershipno,"
                    + "totaldue,discount,totalpaid,changeamount,transtype,yearpart,monthpart,userid) values("
                    + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mReceiptNo.Trim() + "','"
                    + mInvoiceNo.Trim() + "','" + mPatientCode.Trim() + "','" + mAdmissionDetails.WardCode + "','" + mAdmissionDetails.RoomCode
                    + "','" + mAdmissionDetails.BedCode + "','" + mBillingGroupCode.Trim() + "','" + mBillingGroup
                    + "','" + mBillingSubGroupCode.Trim() + "','" + mBillingSubGroup + "','" + mBillingGroupMembershipNo.Trim() + "',"
                    + mAmtDue + "," + mDiscount + "," + mTotalPaid + "," + mChange + "," + (int)AfyaPro_Types.clsEnums.BillingTransTypes.Sales + ","
                    + mYearPart + "," + mMonthPart + ",'" + mUserId.Trim() + "')";
                    mCommand.ExecuteNonQuery();

                    #endregion

                    string mSalesReference = mInvoiceNo;
                    if (mSalesReference.Trim() == "")
                    {
                        mSalesReference = mReceiptNo;
                    }

                    foreach (DataRow mDataRow in mDtItems.Rows)
                    {
                        #region billsalesitems

                        mCommand.CommandText = "insert into billsalesitems(sysdate,transdate,receiptno,invoiceno,reference,"
                        + "patientcode,wardcode,roomcode,bedcode,billinggroupcode,billinggroupdescription,billingsubgroupcode,billingsubgroupdescription,"
                        + "billinggroupmembershipno,itemgroupcode,itemgroupdescription,itemsubgroupcode,itemsubgroupdescription,itemcode,"
                        + "itemdescription,expirydate,storecode,qty,actualamount,amount,transtype,yearpart,monthpart,userid) values("
                        + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                        + mReceiptNo.Trim() + "','" + mInvoiceNo.Trim() + "','"
                        + mSalesReference.Trim() + "','" + mPatientCode.Trim() + "','" + mAdmissionDetails.WardCode + "','" + mAdmissionDetails.RoomCode + "','"
                        + mAdmissionDetails.BedCode + "','" + mBillingGroupCode.Trim() + "','"
                        + mBillingGroup + "','" + mBillingSubGroupCode.Trim() + "','" + mBillingSubGroup + "','" + mBillingGroupMembershipNo.Trim() + "','"
                        + mDataRow["itemgroupcode"].ToString().Trim() + "','" + mDataRow["itemgroupdescription"].ToString().Trim() + "','"
                        + mDataRow["itemsubgroupcode"].ToString().Trim() + "','" + mDataRow["itemsubgroupdescription"].ToString().Trim() + "','"
                        + mDataRow["itemcode"].ToString().Trim() + "','" + mDataRow["itemdescription"].ToString().Trim()
                        + "'," + clsGlobal.Saving_DateValueNullable(mDataRow["expirydate"]) + ",'"
                        + mDataRow["storecode"].ToString().Trim() + "'," + Convert.ToDouble(mDataRow["qty"]) + ","
                        + Convert.ToDouble(mDataRow["actualamount"]) + "," + Convert.ToDouble(mDataRow["amount"]) + ","
                        + (int)AfyaPro_Types.clsEnums.BillingTransTypes.Sales + "," + mYearPart + ","
                        + mMonthPart + ",'" + mUserId.Trim() + "')";
                        mCommand.ExecuteNonQuery();

                        #endregion
                    }

                    #endregion

                    #region invoice if any

                    if (mBill.HasInvoice == true)
                    {
                        #region billinvoices

                        mCommand.CommandText = "insert into billinvoices(sysdate,transdate,invoiceno,totaldue,totalpaid,balancedue,"
                        + "status,patientcode,wardcode,roomcode,bedcode,billinggroupcode,billinggroupdescription,billingsubgroupcode,"
                        + "billingsubgroupdescription,billinggroupmembershipno,yearpart,monthpart,userid,currencycode,currencydescription,"
                        + "currencysymbol,paidforinvoice,booking) values("
                        + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mInvoiceNo.Trim()
                        + "'," + mAmtToInvoice + ",0," + mAmtToInvoice + "," + (int)AfyaPro_Types.clsEnums.InvoiceStatus.Open + ",'"
                        + mPatientCode.Trim() + "','" + mAdmissionDetails.WardCode + "','" + mAdmissionDetails.RoomCode + "','" 
                        + mAdmissionDetails.BedCode + "','" + mBillingGroupCode.Trim() + "','" + mBillingGroup
                        + "','" + mBillingSubGroupCode.Trim() + "','" + mBillingSubGroup + "','" + mBillingGroupMembershipNo.Trim() + "',"
                        + mYearPart + "," + mMonthPart + ",'" + mUserId.Trim() + "','" + mCurrencyCode.Trim() + "','" + mCurrencyDescription.Trim()
                        + "','" + mCurrencySymbol.Trim() + "'," + mAmtPaid + ",'" + mNewBooking + "')";
                        mCommand.ExecuteNonQuery();

                        if (mBill.HasReceipt == true)
                        {
                            mCommand.CommandText = "update billinvoices set balancedue=balancedue-" + mAmtPaid
                            + ",totalpaid=totalpaid+" + mAmtPaid + " where invoiceno='" + mInvoiceNo.Trim() + "'";
                            mCommand.ExecuteNonQuery();

                            mCommand.CommandText = "update billinvoices set status=" + (int)AfyaPro_Types.clsEnums.InvoiceStatus.Closed
                            + " where invoiceno='" + mInvoiceNo.Trim() + "' and balancedue=0";
                            mCommand.ExecuteNonQuery();
                        }

                        #endregion

                        #region billinvoiceslog

                        mCommand.CommandText = "insert into billinvoiceslog(sysdate,transdate,reference,invoiceno,patientcode,"
                        + "wardcode,roomcode,bedcode,billinggroupcode,billinggroupdescription,"
                        + "billingsubgroupcode,billingsubgroupdescription,billinggroupmembershipno,transtype,transdescription,debitamount,"
                        + "creditamount,yearpart,monthpart,userid) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                        + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mInvoiceNo.Trim() + "','" + mInvoiceNo.Trim() + "','"
                        + mPatientCode.Trim() + "','"
                        + mAdmissionDetails.WardCode + "','" + mAdmissionDetails.RoomCode + "','" + mAdmissionDetails.BedCode + "','"
                        + mBillingGroupCode.Trim() + "','" + mBillingGroup + "','" + mBillingSubGroupCode.Trim() + "','"
                        + mBillingSubGroup + "','" + mBillingGroupMembershipNo.Trim() + "'," + (int)AfyaPro_Types.clsEnums.InvoiceTransTypes.NewInvoice
                        + ",'" + mTransDescription1 + "'," + mAmtToInvoice + ",0," + mYearPart + "," + mMonthPart + ",'" + mUserId.Trim() + "')";
                        mCommand.ExecuteNonQuery();

                        if (mBill.HasReceipt == true)
                        {
                            mCommand.CommandText = "insert into billinvoiceslog(sysdate,transdate,reference,invoiceno,patientcode,"
                            + "wardcode,roomcode,bedcode,billinggroupcode,billinggroupdescription,"
                            + "billingsubgroupcode,billingsubgroupdescription,billinggroupmembershipno,transtype,transdescription,debitamount,"
                            + "creditamount,yearpart,monthpart,userid) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                            + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mReceiptNo.Trim() + "','" + mInvoiceNo.Trim() + "','"
                            + mPatientCode.Trim() + "','"
                            + mAdmissionDetails.WardCode + "','" + mAdmissionDetails.RoomCode + "','" + mAdmissionDetails.BedCode + "','"
                            + mBillingGroupCode.Trim() + "','" + mBillingGroup + "','" + mBillingSubGroupCode.Trim() + "','"
                            + mBillingSubGroup + "','" + mBillingGroupMembershipNo.Trim() + "'," + (int)AfyaPro_Types.clsEnums.InvoiceTransTypes.Payment
                            + ",'" + mTransDescription2 + "',0," + mAmtPaid + "," + mYearPart + "," + mMonthPart + ",'" + mUserId.Trim() + "')";
                            mCommand.ExecuteNonQuery();
                        }

                        #endregion

                        foreach (DataRow mDataRow in mDtItems.Rows)
                        {
                            string mStoreCode = mDataRow["storecode"].ToString().Trim();
                            string mItemGroupCode = mDataRow["itemgroupcode"].ToString().Trim();
                            string mItemGroupDescription = mDataRow["itemgroupdescription"].ToString().Trim();
                            string mItemSubGroupCode = mDataRow["itemsubgroupcode"].ToString().Trim();
                            string mItemSubGroupDescription = mDataRow["itemsubgroupdescription"].ToString().Trim();
                            string mItemCode = mDataRow["itemcode"].ToString().Trim();
                            string mItemDescription = mDataRow["itemdescription"].ToString().Trim();
                            double mQuantity = Convert.ToDouble(mDataRow["qty"]);
                            double mActualAmount = Convert.ToDouble(mDataRow["actualamount"]);
                            double mAmount = Convert.ToDouble(mDataRow["amount"]);
                            string mExpiryDate = clsGlobal.Saving_DateValueNullable(mDataRow["expirydate"]);

                            #region billinvoiceitems

                            mCommand.CommandText = "insert into billinvoiceitems(sysdate,transdate,invoiceno,reference,"
                            + "patientcode,wardcode,roomcode,bedcode,billinggroupcode,billinggroupdescription,billingsubgroupcode,billingsubgroupdescription,"
                            + "billinggroupmembershipno,itemgroupcode,itemgroupdescription,itemsubgroupcode,itemsubgroupdescription,itemcode,"
                            + "itemdescription,expirydate,storecode,qty,actualamount,amount,transtype,yearpart,monthpart,userid) values("
                            + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                            + mInvoiceNo.Trim() + "','" + mInvoiceNo.Trim() + "','" + mPatientCode.Trim() + "','"
                            + mAdmissionDetails.WardCode + "','" + mAdmissionDetails.RoomCode + "','" + mAdmissionDetails.BedCode + "','"
                            + mBillingGroupCode.Trim() + "','"
                            + mBillingGroup + "','" + mBillingSubGroupCode.Trim() + "','" + mBillingSubGroup + "','" + mBillingGroupMembershipNo.Trim()
                            + "','" + mItemGroupCode + "','" + mItemGroupDescription + "','" + mItemSubGroupCode + "','" + mItemSubGroupDescription
                            + "','" + mItemCode + "','" + mItemDescription + "'," + mExpiryDate + ",'" + mStoreCode + "'," + mQuantity + ","
                            + mActualAmount + "," + mAmount + "," + (int)AfyaPro_Types.clsEnums.InvoiceTransTypes.NewInvoice + "," + mYearPart + ","
                            + mMonthPart + ",'" + mUserId.Trim() + "')";
                            mCommand.ExecuteNonQuery();

                            #endregion
                        }

                        #region billdebtors

                        mCommand.CommandText = "update billdebtors set balance = balance + " + mAmtToInvoice
                        + " where accountcode='" + mAccountCode.Trim() + "' and debtortype='" + mDebtorType + "'";
                        mRecsAffected = mCommand.ExecuteNonQuery();

                        if (mRecsAffected == 0)
                        {
                            mCommand.CommandText = "insert into billdebtors(sysdate,transdate,accountcode,accountdescription,"
                            + "debtortype,balance) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                            + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mAccountCode.Trim() + "','" + mAccountDescription.Trim() + "','"
                            + mDebtorType.Trim() + "'," + mAmtToInvoice + ")";
                            mCommand.ExecuteNonQuery();
                        }

                        if (mBill.HasReceipt == true)
                        {
                            mCommand.CommandText = "update billdebtors set balance = balance - " + mAmtPaid
                            + " where accountcode='" + mAccountCode.Trim() + "' and debtortype='" + mDebtorType.Trim() + "'";
                            mRecsAffected = mCommand.ExecuteNonQuery();
                        }

                        #endregion

                        #region billdebtorslog

                        mCommand.CommandText = "insert into billdebtorslog(sysdate,transdate,reference,accountcode,"
                        + "accountdescription,debtortype,transtype,transdescription,debitamount,creditamount,yearpart,"
                        + "monthpart,userid,fromwhomtowhomcode,fromwhomtowhom) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                        + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mInvoiceNo.Trim() + "','" + mAccountCode.Trim() + "','"
                        + mAccountDescription + "','" + mDebtorType.Trim() + "'," + (int)AfyaPro_Types.clsEnums.BillingTransTypes.Invoice + ",'"
                        + mTransDescription1 + "'," + mAmtToInvoice + ",0," + mYearPart + "," + mMonthPart + ",'" + mUserId.Trim()
                        + "','" + mFromWhomToWhomCode + "','" + mFromWhomToWhom + "')";
                        mCommand.ExecuteNonQuery();

                        if (mBill.HasReceipt == true)
                        {
                            mCommand.CommandText = "insert into billdebtorslog(sysdate,transdate,reference,accountcode,"
                            + "accountdescription,debtortype,transtype,transdescription,debitamount,creditamount,yearpart,"
                            + "monthpart,userid,fromwhomtowhomcode,fromwhomtowhom) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                            + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mReceiptNo.Trim() + "','" + mAccountCode.Trim() + "','"
                            + mAccountDescription + "','" + mDebtorType.Trim() + "'," + (int)AfyaPro_Types.clsEnums.BillingTransTypes.Payment + ",'"
                            + mTransDescription2 + "',0," + mAmtPaid + "," + mYearPart + "," + mMonthPart + ",'" + mUserId.Trim()
                            + "','" + mFromWhomToWhomCode + "','" + mFromWhomToWhom + "')";
                            mCommand.ExecuteNonQuery();
                        }

                        #endregion
                    }

                    #endregion

                    #region invoice payment if any

                    if (mBill.HasInvoice == true && mBill.HasReceipt == true)
                    {
                        #region billinvoicepayments

                        mCommand.CommandText = "insert into billinvoicepayments(sysdate,transdate,receiptno,patientcode,"
                        + "totalpaid,yearpart,monthpart,userid,currencycode,currencydescription,currencysymbol"
                        + mPaymentFieldNames + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                        + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mReceiptNo.Trim() + "','" + mPatientCode.Trim() + "'," + mAmtPaid
                        + "," + mYearPart + "," + mMonthPart + ",'" + mUserId.Trim() + "','" + mCurrencyCode.Trim() + "','"
                        + mCurrencyDescription.Trim() + "','" + mCurrencySymbol.Trim() + "'" + mPaymentFieldValues + ")";
                        mCommand.ExecuteNonQuery();

                        #endregion

                        #region billinvoicepaymentdetails

                        mCommand.CommandText = "insert into billinvoicepaymentdetails(sysdate,transdate,receiptno,invoiceno,patientcode,wardcode,roomcode,bedcode,"
                        + "billinggroupcode,billinggroupdescription,billingsubgroupcode,billingsubgroupdescription,billinggroupmembershipno,"
                        + "totaldue,totalpaid,yearpart,monthpart,userid) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                        + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mReceiptNo.Trim() + "','" + mInvoiceNo.Trim() + "','"
                        + mPatientCode.Trim() + "','"
                        + mAdmissionDetails.WardCode + "','" + mAdmissionDetails.RoomCode + "','" + mAdmissionDetails.BedCode + "','"
                        + mBillingGroupCode.Trim() + "','" + mBillingGroup
                        + "','" + mBillingSubGroupCode.Trim() + "','" + mBillingSubGroup + "','" + mBillingGroupMembershipNo.Trim() + "',"
                        + mAmtToInvoice + "," + mAmtPaid + "," + mYearPart + "," + mMonthPart + ",'" + mUserId.Trim() + "')";
                        mCommand.ExecuteNonQuery();

                        #endregion
                    }

                    #endregion

                    #region receipt if any

                    if (mBill.HasReceipt == true && mBill.HasInvoice == false)
                    {
                        #region billreceipts

                        mCommand.CommandText = "insert into billreceipts(sysdate,transdate,receiptno,patientcode,wardcode,roomcode,bedcode,"
                        + "billinggroupcode,billinggroupdescription,billingsubgroupcode,billingsubgroupdescription,billinggroupmembershipno,"
                        + "totaldue,discount,totalpaid,changeamount,yearpart,monthpart,userid,currencycode,currencydescription,currencysymbol"
                        + mPaymentFieldNames + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                        + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mReceiptNo.Trim() + "','" + mPatientCode.Trim() + "','"
                        + mAdmissionDetails.WardCode + "','" + mAdmissionDetails.RoomCode + "','" + mAdmissionDetails.BedCode + "','"
                        + mBillingGroupCode.Trim() + "','" + mBillingGroup
                        + "','" + mBillingSubGroupCode.Trim() + "','" + mBillingSubGroup + "','" + mBillingGroupMembershipNo.Trim() + "',"
                        + mAmtDue + "," + mDiscount + "," + mTotalPaid + "," + mChange + "," + mYearPart + "," + mMonthPart + ",'"
                        + mUserId.Trim() + "','" + mCurrencyCode.Trim() + "','" + mCurrencyDescription.Trim() + "','"
                        + mCurrencySymbol.Trim() + "'" + mPaymentFieldValues + ")";
                        mCommand.ExecuteNonQuery();

                        #endregion

                        foreach (DataRow mDataRow in mDtItems.Rows)
                        {
                            #region billreceiptitems

                            mCommand.CommandText = "insert into billreceiptitems(sysdate,transdate,receiptno,reference,"
                            + "patientcode,wardcode,roomcode,bedcode,billinggroupcode,billinggroupdescription,billingsubgroupcode,billingsubgroupdescription,"
                            + "billinggroupmembershipno,itemgroupcode,itemgroupdescription,itemsubgroupcode,itemsubgroupdescription,itemcode,"
                            + "itemdescription,expirydate,storecode,qty,actualamount,amount,transtype,yearpart,monthpart,userid) values("
                            + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mReceiptNo.Trim() + "','"
                            + mReceiptNo.Trim() + "','" + mPatientCode.Trim() + "','"
                            + mAdmissionDetails.WardCode + "','" + mAdmissionDetails.RoomCode + "','" + mAdmissionDetails.BedCode + "','"
                            + mBillingGroupCode.Trim() + "','"
                            + mBillingGroup + "','" + mBillingSubGroupCode.Trim() + "','" + mBillingSubGroup + "','" + mBillingGroupMembershipNo.Trim() + "','"
                            + mDataRow["itemgroupcode"].ToString().Trim() + "','" + mDataRow["itemgroupdescription"].ToString().Trim() + "','"
                            + mDataRow["itemsubgroupcode"].ToString().Trim() + "','" + mDataRow["itemsubgroupdescription"].ToString().Trim() + "','"
                            + mDataRow["itemcode"].ToString().Trim() + "','" + mDataRow["itemdescription"].ToString().Trim()
                            + "'," + clsGlobal.Saving_DateValueNullable(mDataRow["expirydate"]) + ",'"
                            + mDataRow["storecode"].ToString().Trim() + "'," + Convert.ToDouble(mDataRow["qty"]) + ","
                            + Convert.ToDouble(mDataRow["actualamount"]) + "," + Convert.ToDouble(mDataRow["amount"]) + ","
                            + (int)AfyaPro_Types.clsEnums.BillingTransTypes.Payment + "," + mYearPart + ","
                            + mMonthPart + ",'" + mUserId.Trim() + "')";
                            mCommand.ExecuteNonQuery();

                            #endregion
                        }
                    }

                    #endregion

                    #region billcollections

                    if (mBill.HasReceipt == true)
                    {
                        foreach (DataRow mDataRow in mDtPaymentTypes.Rows)
                        {
                            string mBank = mDataRow["bank"].ToString().Trim();
                            string mBranch = mDataRow["branch"].ToString().Trim();
                            string mHolder = mDataRow["holder"].ToString().Trim();
                            string mChequeNo = mDataRow["chequeno"].ToString().Trim();

                            string mPaymentFieldName = ",paytype" + mDataRow["paymenttypecode"].ToString().Trim().ToLower();
                            string mPaymentFieldValue = "," + Convert.ToDouble(mDataRow["paidforbill"]);

                            mCommand.CommandText = "insert into billcollections(sysdate,transdate,receiptno,paymentsource,patientcode,wardcode,roomcode,bedcode,"
                            + "bank,branch,holder,chequeno,transtype,yearpart,monthpart,userid" + mPaymentFieldName + ") values("
                            + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                            + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mReceiptNo.Trim() + "'," + mPaymentSource + ",'"
                            + mPatientCode.Trim() + "','"
                            + mAdmissionDetails.WardCode + "','" + mAdmissionDetails.RoomCode + "','" + mAdmissionDetails.BedCode + "','"
                            + mBank + "','" + mBranch + "','" + mHolder + "','" + mChequeNo + "',"
                            + (int)AfyaPro_Types.clsEnums.BillingTransTypes.Payment + "," + mYearPart + "," + mMonthPart + ",'"
                            + mUserId.Trim() + "'" + mPaymentFieldValue + ")";
                            mCommand.ExecuteNonQuery();
                        }
                    }

                    #endregion

                    #region payment from depositaccount

                    foreach (DataRow mDataRow in mDtPaymentTypes.Rows)
                    {
                        if (Convert.ToInt16(mDataRow["checkdepositbalance"]) == 1)
                        {
                            string mDepoAccountCode = mDataRow["accountcode"].ToString().Trim();
                            string mDepoAccountDescription = mDataRow["accountdescription"].ToString().Trim();
                            double mPaidForBill = Convert.ToDouble(mDataRow["paidforbill"]);
                            string mPatientName = mFirstName;
                            if (mOtherNames.Trim() != "")
                            {
                                mPatientName = mPatientName + " " + mOtherNames;
                            }
                            mPatientName = mPatientName + " " + mSurname;

                            //billaccounts
                            mCommand.CommandText = "update billaccounts set balance=balance-" + mPaidForBill
                                + " where code='" + mDepoAccountCode.Trim() + "'";
                            mCommand.ExecuteNonQuery();

                            //billaccountslog
                            mCommand.CommandText = "insert into billaccountslog(sysdate,transdate,reference,"
                                + "accountcode,accountdescription,fromwhomtowhomcode,fromwhomtowhom,entryside,transdescription,"
                                + "creditamount,debitamount,userid) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                                + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mReceiptNo.Trim() + "','"
                                + mDepoAccountCode.Trim() + "','" + mDepoAccountDescription.Trim() + "','" + mPatientCode.Trim()
                                + "','" + mPatientName + "'," + (int)AfyaPro_Types.clsEnums.AccountEntrySides.Credit + ",'Payment',"
                                + mPaidForBill + ",0,'" + mUserId.Trim() + "')";
                            mCommand.ExecuteNonQuery();
                        }
                    }

                    #endregion

                    #region inventory

                    string mLocalCurrencyCode = "";
                    string mLocalCurrencyDesc = "";
                    string mLocalCurrencySymbol = "";
                    double mLocalExchangeRate = 1;

                    DataTable mDtCurrencies = new DataTable("currencies");
                    mCommand.CommandText = "select * from facilitycurrencies where code='LocalCurr'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtCurrencies);
                    if (mDtCurrencies.Rows.Count > 0)
                    {
                        mLocalCurrencyCode = mDtCurrencies.Rows[0]["code"].ToString().Trim();
                        mLocalCurrencyDesc = mDtCurrencies.Rows[0]["description"].ToString().Trim();
                        mLocalCurrencySymbol = mDtCurrencies.Rows[0]["currencysymbol"].ToString().Trim();
                    }

                    if (mDirectStockIssuing == true)
                    {
                        string mProductTransDescription = "Issue";
                        string mToCode = mPatientCode.Trim();
                        string mToDescription = mFirstName;
                        if (mOtherNames.Trim() != "")
                        {
                            mToDescription = mToDescription + " " + mOtherNames.Trim();
                        }
                        mToDescription = mToDescription + " " + mSurname.Trim();
                        string mReference = mInvoiceNo;
                        if (mReference.Trim() == "")
                        {
                            mReference = mReceiptNo;
                        }

                        foreach (DataRow mDataRow in mDtItems.Rows)
                        {
                            string mStoreCode = mDataRow["storecode"].ToString().Trim();

                            if (mStoreCode.Trim() != "")
                            {
                                string mProductCode = mDataRow["itemcode"].ToString().Trim();
                                string mPackagingCode = mDataRow["packagingcode"].ToString().Trim();
                                string mPackagingDescription = mDataRow["packagingdescription"].ToString().Trim();
                                int mPiecesInPackage = Convert.ToInt32(mDataRow["piecesinpackage"]);
                                string mExpiryDate = clsGlobal.Saving_DateValueNullable(mDataRow["expirydate"]);
                                double mIssuedQty = Convert.ToDouble(mDataRow["qty"]);
                                double mTransPrice = Convert.ToDouble(mDataRow["amount"]) / mIssuedQty;

                                double mProductQty = mIssuedQty * mPiecesInPackage;

                                DataTable mDtProducts = new DataTable("products");
                                mCommand.CommandText = "select * from som_products where code='" + mProductCode.Trim() + "'";
                                mDataAdapter.SelectCommand = mCommand;
                                mDataAdapter.Fill(mDtProducts);
                                DataView mDvProducts = new DataView();
                                mDvProducts.Table = mDtProducts;
                                mDvProducts.Sort = "code";

                                string mExtraProductFields = clsGlobal.Build_FieldsForProductExtraInfos();
                                string mExtraProductFieldValues = clsGlobal.Build_FieldValuesForProductExtraInfos(mDvProducts, mProductCode);

                                #region stock

                                string mWhereProduct = " where productcode='" + mProductCode + "' and expirydate=" + mExpiryDate;
                                if (mExpiryDate.Trim().ToLower() == "null")
                                {
                                    mWhereProduct = " where (productcode='" + mProductCode + "') and (expirydate=Null or expirydate is Null)";
                                }

                                #region som_productexpirydates

                                //som_productexpirydates
                                mCommand.CommandText = "update som_productexpirydates set quantity=quantity-" + mProductQty + " where storecode='"
                                    + mStoreCode + "' and productcode='" + mProductCode + "' and expirydate=" + mExpiryDate;
                                mCommand.ExecuteNonQuery();

                                mCommand.CommandText = "delete from som_productexpirydates where storecode='" + mStoreCode
                                    + "' and productcode='" + mProductCode + "' and expirydate=" + mExpiryDate + " and quantity=0";
                                mCommand.ExecuteNonQuery();

                                #endregion

                                #region productcontrol

                                //som_productcontrol
                                mCommand.CommandText = "update som_productcontrol set qty_" + mStoreCode + " = qty_" + mStoreCode + " - "
                                + mProductQty + " where productcode='" + mProductCode + "'";
                                mRecsAffected = mCommand.ExecuteNonQuery();
                                if (mRecsAffected == 0)
                                {
                                    mCommand.CommandText = "insert into som_productcontrol(productcode,qty_" + mStoreCode + ") "
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
                                + "transdescription,userid,qtyout_" + mStoreCode + ",transprice," + mExtraProductFields + ") values("
                                + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                                + mToCode.Trim() + "','" + mToDescription.Trim() + "','" + mProductCode + "','"
                                + mPackagingCode + "','" + mPackagingDescription + "'," + mPiecesInPackage + "," + mExpiryDate + ",'"
                                + mReference + "','" + AfyaPro_Types.clsEnums.ProductTransTypes.Issue.ToString() + "','"
                                + mProductTransDescription + "','" + mUserId.Trim() + "'," + mProductQty + "," + mTransPrice + ","
                                + mExtraProductFieldValues + ")";
                                mRecsAffected = mCommand.ExecuteNonQuery();

                                #region audit som_producttransactions

                                if (mRecsAffected > 0)
                                {
                                    string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_producttransactions";
                                    string mAuditingFields = clsGlobal.AuditingFields();
                                    string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                        AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                                    mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,sourcecode,sourcedescription,"
                                    + "productcode,packagingcode,packagingdescription,piecesinpackage,expirydate,reference,transtype,"
                                    + "transdescription,userid,qtyout_" + mStoreCode + ",transprice," + mAuditingFields + "," + mExtraProductFields + ") values("
                                    + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                                    + mToCode.Trim() + "','" + mToDescription.Trim() + "','" + mProductCode + "','"
                                    + mPackagingCode + "','" + mPackagingDescription + "'," + mPiecesInPackage + "," + mExpiryDate + ",'"
                                    + mReference + "','" + AfyaPro_Types.clsEnums.ProductTransTypes.Issue.ToString() + "','"
                                    + mProductTransDescription + "','" + mUserId.Trim() + "'," + mProductQty + "," + mTransPrice + ","
                                    + mAuditingValues + "," + mExtraProductFieldValues + ")";
                                    mCommand.ExecuteNonQuery();
                                }

                                #endregion

                                #endregion

                                #endregion
                            }
                        }
                    }
                    else
                    {
                        string mToCode = mPatientCode.Trim();
                        string mToDescription = mFirstName;
                        if (mOtherNames.Trim() != "")
                        {
                            mToDescription = mToDescription + " " + mOtherNames.Trim();
                        }
                        mToDescription = mToDescription + " " + mSurname.Trim();
                        string mReference = mInvoiceNo;
                        if (mReference.Trim() == "")
                        {
                            mReference = mReceiptNo;
                        }

                        int mRowCount = 1;
                        bool mTransferCreated = false;
                        foreach (DataRow mDataRow in mDtItems.Rows)
                        {
                            string mStoreCode = mDataRow["storecode"].ToString().Trim();
                            string mStoreDescription = mDataRow["storedescription"].ToString().Trim();
                            string mTransferNo = "sales_" + mReference;

                            if (mStoreCode.Trim() != "")
                            {
                                if (mTransferCreated == false)
                                {
                                    //som_transferouts
                                    mCommand.CommandText = "insert into som_transferouts(sysdate,transdate,transferno,"
                                    + "tocode,todescription,fromcode,fromdescription,transfertype,currencycode,currencydescription,currencysymbol,"
                                    + "exchangerate,transferstatus,userid) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                                    + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mTransferNo.Trim() + "','" + mToCode.Trim() + "','"
                                    + mToDescription.Trim() + "','" + mStoreCode.Trim() + "','" + mStoreDescription.Trim() + "','"
                                    + AfyaPro_Types.clsEnums.SomTransferTypes.Patient.ToString() + "','" + mLocalCurrencyCode.Trim() + "','"
                                    + mLocalCurrencyDesc.Trim() + "','" + mLocalCurrencySymbol.Trim() + "'," + mLocalExchangeRate + ",'"
                                    + AfyaPro_Types.clsEnums.SomTransferStatus.Open.ToString() + "','" + mUserId.Trim() + "')";
                                    mRecsAffected = mCommand.ExecuteNonQuery();

                                    #region audit som_transferouts

                                    if (mRecsAffected > 0)
                                    {
                                        string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_transferouts";
                                        string mAuditingFields = clsGlobal.AuditingFields();
                                        string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                            AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                                        mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,transferno,"
                                        + "tocode,todescription,fromcode,fromdescription,transfertype,currencycode,currencydescription,currencysymbol,"
                                        + "exchangerate,transferstatus,userid," + mAuditingFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                                        + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mTransferNo.Trim() + "','" + mToCode.Trim() + "','"
                                        + mToDescription.Trim() + "','" + mStoreCode.Trim() + "','" + mStoreDescription.Trim() + "','"
                                        + AfyaPro_Types.clsEnums.SomTransferTypes.Patient.ToString() + "','" + mLocalCurrencyCode.Trim() + "','"
                                        + mLocalCurrencyDesc.Trim() + "','" + mCurrencySymbol.Trim() + "'," + mLocalExchangeRate + ",'"
                                        + AfyaPro_Types.clsEnums.SomTransferStatus.Open.ToString() + "','" + mUserId.Trim() + "'," + mAuditingValues + ")";
                                        mRecsAffected = mCommand.ExecuteNonQuery();
                                        mCommand.ExecuteNonQuery();
                                    }

                                    #endregion

                                    mTransferCreated = true;
                                }

                                string mProductCode = mDataRow["itemcode"].ToString().Trim();
                                string mPackagingCode = mDataRow["packagingcode"].ToString().Trim();
                                string mPackagingDescription = mDataRow["packagingdescription"].ToString().Trim();
                                int mPiecesInPackage = Convert.ToInt32(mDataRow["piecesinpackage"]);
                                double mTransferedQty = Convert.ToDouble(mDataRow["qty"]);
                                double mTransPrice = Convert.ToDouble(mDataRow["amount"]) / mTransferedQty;

                                string mItemTransferId = clsGlobal.Generate_AutoId() + mRowCount;

                                DataTable mDtProducts = new DataTable("products");
                                mCommand.CommandText = "select * from som_products where code='" + mProductCode.Trim() + "'";
                                mDataAdapter.SelectCommand = mCommand;
                                mDataAdapter.Fill(mDtProducts);
                                DataView mDvProducts = new DataView();
                                mDvProducts.Table = mDtProducts;
                                mDvProducts.Sort = "code";

                                string mExtraProductFields = clsGlobal.Build_FieldsForProductExtraInfos();
                                string mExtraProductFieldValues = clsGlobal.Build_FieldValuesForProductExtraInfos(mDvProducts, mProductCode);

                                //som_transferoutitems
                                mCommand.CommandText = "insert into som_transferoutitems(sysdate,transdate,transferno,itemtransferid,productcode,"
                                + "packagingcode,packagingdescription,piecesinpackage,transferedqty,transprice,userid," + mExtraProductFields + ") values("
                                + clsGlobal.Saving_DateValue(DateTime.Now) + "," + clsGlobal.Saving_DateValue(mTransDate) + ",'"
                                + mTransferNo + "','" + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode + "','"
                                + mPackagingDescription + "'," + mPiecesInPackage + "," + mTransferedQty + "," + mTransPrice + ",'"
                                + mUserId.Trim() + "'," + mExtraProductFieldValues + ")";
                                mRecsAffected = mCommand.ExecuteNonQuery();

                                #region audit som_transferoutitems

                                if (mRecsAffected > 0)
                                {
                                    string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "som_transferoutitems";
                                    string mAuditingFields = clsGlobal.AuditingFields();
                                    string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                                        AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                                    mCommand.CommandText = "insert into " + mAuditTableName + "(sysdate,transdate,transferno,itemtransferid,productcode,"
                                    + "packagingcode,packagingdescription,piecesinpackage,transferedqty,transprice,userid,"
                                    + mAuditingFields + "," + mExtraProductFields + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," 
                                    + clsGlobal.Saving_DateValue(mTransDate)
                                    + ",'" + mTransferNo + "','" + mItemTransferId + "','" + mProductCode + "','" + mPackagingCode + "','"
                                    + mPackagingDescription + "'," + mPiecesInPackage + "," + mTransferedQty + "," + mTransPrice + ",'" + mUserId.Trim() + "',"
                                    + mAuditingValues + "," + mExtraProductFieldValues + ")";
                                    mCommand.ExecuteNonQuery();
                                }

                                #endregion

                                mRowCount++;
                            }
                        }
                    }

                    #endregion

                    if (mProcessIncoming == true)
                    {
                        #region billincomingitems

                        foreach (DataRow mDataRow in mDtItems.Rows)
                        {
                            int mAutoCode = Convert.ToInt32(mDataRow["autocode"]);

                            mCommand.CommandText = "update billincomingitems set display=0 where autocode=" + mAutoCode;
                            mCommand.ExecuteNonQuery();
                        }

                        #endregion
                    }

                    #region prepare next receipt/invoice #

                    if (mBill.HasReceipt && mGenerateReceipt == 1)
                    {
                        Int16 mCodeKey = Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.receiptno);
                        if (mBill.HasInvoice == true)
                        {
                            mCodeKey = Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.paymentno);
                        }

                        mCommand.CommandText = "update facilityautocodes set "
                        + "idcurrent=idcurrent+idincrement where codekey=" + mCodeKey;
                        mCommand.ExecuteNonQuery();
                    }

                    if (mBill.HasInvoice && mGenerateInvoice == 1)
                    {
                        mCommand.CommandText = "update facilityautocodes set "
                        + "idcurrent=idcurrent+idincrement where codekey="
                        + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.invoiceno);
                        mCommand.ExecuteNonQuery();
                    }

                    #endregion
                }

                #endregion

                mTrans.Commit();

                //get booking that has been done
                clsRegistrations mRegistrations = new clsRegistrations();
                mBooking = mRegistrations.Get_Booking(mPatientCode);

                mBill.ReceiptNo = mReceiptNo;
                mBill.InvoiceNo = mInvoiceNo;
                mBill.Booking = mBooking.Booking;
                mBill.PatientCode = mPatientCode;
                mBill.BillingGroupCode = mBooking.BillingGroupCode;
                mBill.BillingSubGroupCode = mBooking.BillingSubGroupCode;
                mBill.BillingGroupMembershipNo = mBooking.BillingGroupMembershipNo;

                if (mIsReAttendance == false && mIsForcedReAttendance == false)
                {
                    mBill.IsNewAttendance = true;
                }
                else
                {
                    mBill.IsNewAttendance = false;
                }

                //return
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

        #region Remove_ItemFromCart
        public AfyaPro_Types.clsBill Remove_ItemFromCart(DateTime mTransDate, int mAutoCode, string mUserId)
        {
            AfyaPro_Types.clsBill mBill = new AfyaPro_Types.clsBill();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();

            string mFunctionName = "Remove_ItemFromCart";

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
                List<clsDataField> mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("deleteddate", DataTypes.dbdatetime.ToString(), mTransDate));
                mDataFields.Add(new clsDataField("deletedbyuserid", DataTypes.dbstring.ToString(), mUserId));
                mDataFields.Add(new clsDataField("display", DataTypes.dbnumber.ToString(), 0));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("billincomingitems", mDataFields, "autocode=" + mAutoCode);
                mCommand.ExecuteNonQuery();

                return mBill;
            }
            catch (OdbcException ex)
            {
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

        #region Pay_FromPatient
        public AfyaPro_Types.clsBill Pay_FromPatient(Int16 mGenerateReceipt, string mReceiptNo,
            DateTime mTransDate, string mPatientCode, string mPayTypeCode, string mBank, string mBranch,
            string mChequeNo, string mHolder, double mAmtPaid, int mCheckDepositBalance, 
            string mDepoAccountCode, string mDepoAccountDescription, DataTable mDtInvoices, string mCurrencyCode, 
            string mCurrencyDescription, string mCurrencySymbol, DataTable mDtDebtRelief, string mUserId)
        {
            AfyaPro_Types.clsBill mBill = new AfyaPro_Types.clsBill();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            string mSurname = "";
            string mFirstName = "";
            string mOtherNames = "";
            string mGender = "";
            DateTime mBirthDate = new DateTime();
            DateTime mRegDate = new DateTime();

            String mFunctionName = "Pay_FromPatient";

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

            #region check for patient existance

            try
            {
                mCommand.CommandText = "select * from patients where code='" + mPatientCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mSurname = mDataReader["surname"].ToString();
                    mFirstName = mDataReader["firstname"].ToString();
                    mOtherNames = mDataReader["othernames"].ToString();
                    mGender = mDataReader["gender"].ToString();
                    mBirthDate = Convert.ToDateTime(mDataReader["birthdate"]);
                    mRegDate = Convert.ToDateTime(mDataReader["regdate"]);
                }
                else
                {
                    mBill.Exe_Result = 0;
                    mBill.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoDoesNotExist.ToString();
                    return mBill;
                }
            }
            catch (OdbcException ex)
            {
                mBill.Exe_Result = -1;
                mBill.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBill;
            }
            finally
            {
                mDataReader.Close();
            }

            #endregion

            #region auto generate receiptno, if option is on

            if (mGenerateReceipt == 1)
            {
                clsAutoCodes objAutoCodes = new clsAutoCodes();
                AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                mObjCode = objAutoCodes.Next_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.paymentno), "billinvoicepayments", "receiptno");
                if (mObjCode.Exe_Result == -1)
                {
                    mBill.Exe_Result = 0;
                    mBill.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DuplicateReceiptNoDetected.ToString();
                    return mBill;
                }
                mReceiptNo = mObjCode.GeneratedCode;
            }

            #endregion

            #region check 4 duplicate receiptno
            try
            {
                mCommand.CommandText = "select * from billinvoicepayments where receiptno='" + mReceiptNo.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBill.Exe_Result = 0;
                    mBill.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DuplicateReceiptNoDetected.ToString();
                    return mBill;
                }
            }
            catch (OdbcException ex)
            {
                mBill.Exe_Result = -1;
                mBill.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBill;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion

            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                int mYearPart = mTransDate.Year;
                int mMonthPart = mTransDate.Month;

                string mTransDescription = "Payment";
                if (mDtDebtRelief.Rows.Count > 0)
                {
                    mTransDescription = "Debt Relief";
                }

                Int16 mPaymentSource = Convert.ToInt16(AfyaPro_Types.clsEnums.PaymentSources.InvoicePayment);

                string mDebtorType = AfyaPro_Types.clsEnums.DebtorTypes.Individual.ToString();
                string mAccountCode = mPatientCode.Trim();
                string mAccountDescription = mFirstName;
                string mFromWhomToWhom = mFirstName;
                string mFromWhomToWhomCode = mPatientCode;
                if (mOtherNames.Trim() != "")
                {
                    mFromWhomToWhom = mFromWhomToWhom + " " + mOtherNames.Trim();
                    mAccountDescription = mAccountDescription + " " + mOtherNames.Trim();
                }
                mFromWhomToWhom = mFromWhomToWhom + " " + mSurname.Trim();
                mAccountDescription = mAccountDescription + " " + mSurname.Trim();

                double mTotalDebtReliefApproved = 0;

                foreach (DataRow mDataRow in mDtInvoices.Rows)
                {
                    string mInvoiceNo = mDataRow["invoiceno"].ToString().Trim();
                    double mTotalDue = Convert.ToDouble(mDataRow["balancedue"]);
                    double mPaidForInvoice = Convert.ToDouble(mDataRow["paidforbill"]);
                    string mBillingGroupCode = mDataRow["billinggroupcode"].ToString().Trim();
                    string mBillingGroup = mDataRow["billinggroupdescription"].ToString().Trim();
                    string mBillingSubGroupCode = mDataRow["billingsubgroupcode"].ToString().Trim();
                    string mBillingSubGroup = mDataRow["billingsubgroupdescription"].ToString().Trim();
                    string mBillingGroupMembershipNo = mDataRow["billinggroupmembershipno"].ToString().Trim();
                    string mBooking = mDataRow["booking"].ToString().Trim();

                    //get admission details
                    AfyaPro_Types.clsAdmission mAdmissionDetails = new AfyaPro_Types.clsAdmission();
                    clsRegistrations mMdtRegistrations = new clsRegistrations();
                    mAdmissionDetails = mMdtRegistrations.Get_Admission(mBooking, mPatientCode);

                    #region billinvoices

                    mCommand.CommandText = "update billinvoices set balancedue=balancedue-" + mPaidForInvoice
                    + ",totalpaid=totalpaid+" + mPaidForInvoice + " where invoiceno='" + mInvoiceNo.Trim() + "'";
                    mCommand.ExecuteNonQuery();

                    mCommand.CommandText = "update billinvoices set status=" + (int)AfyaPro_Types.clsEnums.InvoiceStatus.Closed
                    + " where invoiceno='" + mInvoiceNo.Trim() + "' and balancedue=0";
                    mCommand.ExecuteNonQuery();

                    #endregion

                    #region billinvoiceslog

                    mCommand.CommandText = "insert into billinvoiceslog(sysdate,transdate,reference,invoiceno,patientcode,"
                    + "wardcode,roomcode,bedcode,billinggroupcode,billinggroupdescription,"
                    + "billingsubgroupcode,billingsubgroupdescription,billinggroupmembershipno,transtype,transdescription,debitamount,"
                    + "creditamount,yearpart,monthpart,userid) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                    + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mReceiptNo.Trim() + "','" + mInvoiceNo.Trim() + "','"
                    + mPatientCode.Trim() + "','"
                    + mAdmissionDetails.WardCode + "','" + mAdmissionDetails.RoomCode + "','" + mAdmissionDetails.BedCode + "','"
                    + mBillingGroupCode.Trim() + "','" + mBillingGroup + "','" + mBillingSubGroupCode.Trim() + "','"
                    + mBillingSubGroup + "','" + mBillingGroupMembershipNo.Trim() + "'," + (int)AfyaPro_Types.clsEnums.InvoiceTransTypes.Payment
                    + ",'" + mTransDescription + "',0," + mPaidForInvoice + "," + mYearPart + "," + mMonthPart + ",'" + mUserId.Trim() + "')";
                    mCommand.ExecuteNonQuery();

                    #endregion

                    #region billinvoicepaymentdetails

                    mCommand.CommandText = "insert into billinvoicepaymentdetails(sysdate,transdate,receiptno,invoiceno,patientcode,wardcode,roomcode,bedcode,"
                    + "billinggroupcode,billinggroupdescription,billingsubgroupcode,billingsubgroupdescription,billinggroupmembershipno,"
                    + "totaldue,totalpaid,yearpart,monthpart,userid) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                    + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mReceiptNo.Trim() + "','" + mInvoiceNo.Trim() + "','"
                    + mPatientCode.Trim() + "','"
                    + mAdmissionDetails.WardCode + "','" + mAdmissionDetails.RoomCode + "','" + mAdmissionDetails.BedCode + "','" 
                    + mBillingGroupCode.Trim() + "','" + mBillingGroup
                    + "','" + mBillingSubGroupCode.Trim() + "','" + mBillingSubGroup + "','" + mBillingGroupMembershipNo.Trim() + "',"
                    + mTotalDue + "," + mPaidForInvoice + "," + mYearPart + "," + mMonthPart + ",'" + mUserId.Trim() + "')";
                    mCommand.ExecuteNonQuery();

                    #endregion

                    #region billcollections

                    string mPaymentFieldName = "paytype" + mPayTypeCode.Trim().ToLower();
                    double mPaymentFieldValue = Convert.ToDouble(mDataRow["paidforbill"]);

                    mCommand.CommandText = "insert into billcollections(sysdate,transdate,receiptno,paymentsource,patientcode,"
                    + "wardcode,roomcode,bedcode,bank,branch,holder,chequeno,transtype,yearpart,monthpart,userid," + mPaymentFieldName
                    + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                    + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mReceiptNo.Trim() + "'," + mPaymentSource + ",'" + mPatientCode.Trim()
                    + "','" + mAdmissionDetails.WardCode + "','" + mAdmissionDetails.RoomCode + "','" + mAdmissionDetails.BedCode + "','" 
                    + mBank + "','" + mBranch + "','" + mHolder + "','" + mChequeNo + "',"
                    + (int)AfyaPro_Types.clsEnums.BillingTransTypes.Payment + "," + mYearPart + "," + mMonthPart + ",'"
                    + mUserId.Trim() + "'," + mPaymentFieldValue + ")";
                    mCommand.ExecuteNonQuery();

                    #endregion

                    #region billdebtreliefrequestinvoices

                    if (mDtDebtRelief.Rows.Count > 0)
                    {
                        string mDebtReliefRequestNo = mDtDebtRelief.Rows[0]["requestno"].ToString().Trim();
                        mTotalDebtReliefApproved = mTotalDebtReliefApproved + mPaidForInvoice;

                        List<clsDataField> mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("approvedamount", DataTypes.dbdecimal.ToString(), mPaidForInvoice));

                        mCommand.CommandText = clsGlobal.Get_UpdateStatement("billdebtreliefrequestinvoices", mDataFields,
                            "requestno='" + mDebtReliefRequestNo.Trim() + "' and invoiceno='" + mInvoiceNo.Trim() + "'");
                        mCommand.ExecuteNonQuery();
                    }

                    #endregion
                }

                #region billdebtreliefrequests

                if (mDtDebtRelief.Rows.Count > 0)
                {
                    string mApprovalRemarks = mDtDebtRelief.Rows[0]["approvalremarks"].ToString().Trim();
                    string mDebtReliefRequestNo = mDtDebtRelief.Rows[0]["requestno"].ToString().Trim();

                    List<clsDataField> mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("approveddate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("totalapprovedamount", DataTypes.dbdecimal.ToString(), mTotalDebtReliefApproved));
                    mDataFields.Add(new clsDataField("approvalremarks", DataTypes.dbstring.ToString(), mApprovalRemarks));
                    mDataFields.Add(new clsDataField("approvedby", DataTypes.dbstring.ToString(), mUserId.Trim()));
                    mDataFields.Add(new clsDataField("requeststatus", DataTypes.dbnumber.ToString(), (int)AfyaPro_Types.clsEnums.DebtReliefRequestStatus.Approved));

                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("billdebtreliefrequests", mDataFields,
                        "requestno='" + mDebtReliefRequestNo.Trim() + "' and accountcode='" + mPatientCode.Trim() + "' and debtortype='" + mDebtorType.Trim() + "'");
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region billdebtors

                mCommand.CommandText = "update billdebtors set balance = balance - " + mAmtPaid
                + " where accountcode='" + mAccountCode.Trim() + "' and debtortype='" + mDebtorType.Trim() + "'";
                mCommand.ExecuteNonQuery();

                #endregion

                #region billdebtorslog

                mCommand.CommandText = "insert into billdebtorslog(sysdate,transdate,reference,accountcode,"
                + "accountdescription,debtortype,transtype,transdescription,debitamount,creditamount,yearpart,"
                + "monthpart,userid,fromwhomtowhomcode,fromwhomtowhom) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mReceiptNo.Trim() + "','" + mAccountCode.Trim() + "','"
                + mAccountDescription + "','" + mDebtorType.Trim() + "'," + (int)AfyaPro_Types.clsEnums.BillingTransTypes.Payment + ",'"
                + mTransDescription + "',0," + mAmtPaid + "," + mYearPart + "," + mMonthPart + ",'" + mUserId.Trim()
                + "','" + mFromWhomToWhomCode + "','" + mFromWhomToWhom + "')";
                mCommand.ExecuteNonQuery();

                #endregion

                #region billinvoicepayments

                mCommand.CommandText = "insert into billinvoicepayments(sysdate,transdate,receiptno,patientcode,"
                + "totalpaid,yearpart,monthpart,userid,currencycode,currencydescription,currencysymbol," 
                + "paytype" + mPayTypeCode.Trim().ToLower() + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," 
                + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mReceiptNo.Trim() + "','" + mPatientCode.Trim() + "',"
                + mAmtPaid + "," + mYearPart + "," + mMonthPart + ",'" + mUserId.Trim() + "','" + mCurrencyCode.Trim()
                + "','" + mCurrencyDescription.Trim() + "','" + mCurrencySymbol.Trim() + "'," + mAmtPaid + ")";
                mCommand.ExecuteNonQuery();

                #endregion

                #region payment from depositaccount

                if (mCheckDepositBalance == 1)
                {
                    string mPatientName = mFirstName;
                    if (mOtherNames.Trim() != "")
                    {
                        mPatientName = mPatientName + " " + mOtherNames;
                    }
                    mPatientName = mPatientName + " " + mSurname;

                    //billaccounts
                    mCommand.CommandText = "update billaccounts set balance=balance-" + mAmtPaid
                        + " where code='" + mDepoAccountCode.Trim() + "'";
                    mCommand.ExecuteNonQuery();

                    //billaccountslog
                    mCommand.CommandText = "insert into billaccountslog(sysdate,transdate,reference,"
                        + "accountcode,accountdescription,fromwhomtowhomcode,fromwhomtowhom,entryside,transdescription,"
                        + "creditamount,debitamount,userid) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                        + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mReceiptNo.Trim() + "','"
                        + mDepoAccountCode.Trim() + "','" + mDepoAccountDescription.Trim() + "','" + mPatientCode.Trim()
                        + "','" + mPatientName + "'," + (int)AfyaPro_Types.clsEnums.AccountEntrySides.Credit + ",'Payment',"
                        + mAmtPaid + ",0,'" + mUserId.Trim() + "')";
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region prepare next receipt #

                if (mGenerateReceipt == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.paymentno);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                mBill.ReceiptNo = mReceiptNo;
                mBill.PatientCode = mPatientCode;

                //commit changes
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

        #region Pay_FromGroup
        public AfyaPro_Types.clsBill Pay_FromGroup(Int16 mGenerateReceipt, string mReceiptNo,
            DateTime mTransDate, string mBillingGroupCode, string mPayTypeCode, string mBank, string mBranch,
            string mChequeNo, string mHolder, double mAmtPaid, int mCheckDepositBalance, 
            string mDepoAccountCode, string mDepoAccountDescription, DataTable mDtInvoices, string mCurrencyCode,
            string mCurrencyDescription, string mCurrencySymbol, DataTable mDtDebtRelief, string mUserId)
        {
            AfyaPro_Types.clsBill mBill = new AfyaPro_Types.clsBill();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            string mBillingGroup = "";

            String mFunctionName = "Pay_FromGroup";

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

            if (mGenerateReceipt == 1)
            {
                clsAutoCodes objAutoCodes = new clsAutoCodes();
                AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                mObjCode = objAutoCodes.Next_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.paymentno), "billinvoicepayments", "receiptno");
                if (mObjCode.Exe_Result == -1)
                {
                    mBill.Exe_Result = 0;
                    mBill.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DuplicateReceiptNoDetected.ToString();
                    return mBill;
                }
                mReceiptNo = mObjCode.GeneratedCode;
            }

            #endregion

            #region check 4 duplicate receiptno
            try
            {
                mCommand.CommandText = "select * from billinvoicepayments where receiptno='" + mReceiptNo.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBill.Exe_Result = 0;
                    mBill.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DuplicateReceiptNoDetected.ToString();
                    return mBill;
                }
            }
            catch (OdbcException ex)
            {
                mBill.Exe_Result = -1;
                mBill.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBill;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion

            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                int mYearPart = mTransDate.Year;
                int mMonthPart = mTransDate.Month;

                string mTransDescription = "Payment";
                if (mDtDebtRelief.Rows.Count > 0)
                {
                    mTransDescription = "Debt Relief";
                }

                Int16 mPaymentSource = Convert.ToInt16(AfyaPro_Types.clsEnums.PaymentSources.InvoicePayment);

                #region billing group

                mCommand.CommandText = "select * from facilitycorporates where code='" + mBillingGroupCode.Trim() + "'";
                DataTable mDtBillingGroups = new DataTable("facilitycorporates");
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtBillingGroups);
                if (mDtBillingGroups.Rows.Count > 0)
                {
                    mBillingGroup = mDtBillingGroups.Rows[0]["description"].ToString().Trim();
                }

                #endregion

                string mDebtorType = AfyaPro_Types.clsEnums.DebtorTypes.Group.ToString();
                string mAccountCode = mBillingGroupCode.Trim();
                string mAccountDescription = mBillingGroup;
                string mFromWhomToWhom = mBillingGroup.Trim();
                string mFromWhomToWhomCode = mBillingGroupCode.Trim();

                string mInvoicesList = "";
                foreach (DataRow mDataRow in mDtInvoices.Rows)
                {
                    if (mInvoicesList.Trim() == "")
                    {
                        mInvoicesList = "'" + mDataRow["invoiceno"].ToString().Trim() + "'";
                    }
                    else
                    {
                        mInvoicesList = mInvoicesList + ",'" + mDataRow["invoiceno"].ToString().Trim() + "'";
                    }
                }

                DataTable mDtDbInvoices = new DataTable("savedinvoices");
                mCommand.CommandText = "select * from billinvoices where invoiceno in (" + mInvoicesList + ")";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtDbInvoices);
                DataView mDvDbInvoices = new DataView();
                mDvDbInvoices.Table = mDtDbInvoices;
                mDvDbInvoices.Sort = "invoiceno";

                double mTotalDebtReliefApproved = 0;

                foreach (DataRow mDataRow in mDtInvoices.Rows)
                {
                    string mInvoiceNo = mDataRow["invoiceno"].ToString().Trim();
                    double mTotalDue = Convert.ToDouble(mDataRow["balancedue"]);
                    double mPaidForInvoice = Convert.ToDouble(mDataRow["paidforbill"]);
                    string mPatientCode = mDataRow["patientcode"].ToString();
                    string mBillingSubGroupCode = mDataRow["billingsubgroupcode"].ToString();
                    string mBillingSubGroup = mDataRow["billingsubgroupdescription"].ToString();
                    string mBillingGroupMembershipNo = mDataRow["billinggroupmembershipno"].ToString();
                    string mBooking = mDataRow["booking"].ToString().Trim();

                    //get admission details
                    AfyaPro_Types.clsAdmission mAdmissionDetails = new AfyaPro_Types.clsAdmission();
                    clsRegistrations mMdtRegistrations = new clsRegistrations();
                    mAdmissionDetails = mMdtRegistrations.Get_Admission(mBooking, mPatientCode);

                    #region billinvoices

                    mCommand.CommandText = "update billinvoices set balancedue=balancedue-" + mPaidForInvoice
                    + ",totalpaid=totalpaid+" + mPaidForInvoice + " where invoiceno='" + mInvoiceNo.Trim() + "'";
                    mCommand.ExecuteNonQuery();

                    mCommand.CommandText = "update billinvoices set status=" + (int)AfyaPro_Types.clsEnums.InvoiceStatus.Closed
                    + " where invoiceno='" + mInvoiceNo.Trim() + "' and balancedue=0";
                    mCommand.ExecuteNonQuery();

                    #endregion

                    #region billinvoiceslog

                    mCommand.CommandText = "insert into billinvoiceslog(sysdate,transdate,reference,invoiceno,patientcode,"
                    + "wardcode,roomcode,bedcode,billinggroupcode,billinggroupdescription,"
                    + "billingsubgroupcode,billingsubgroupdescription,billinggroupmembershipno,transtype,transdescription,debitamount,"
                    + "creditamount,yearpart,monthpart,userid) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                    + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mReceiptNo.Trim() + "','" + mInvoiceNo.Trim() + "','"
                    + mPatientCode.Trim() + "','"
                    + mAdmissionDetails.WardCode + "','" + mAdmissionDetails.RoomCode + "','" + mAdmissionDetails.BedCode + "','" 
                    + mBillingGroupCode.Trim() + "','" + mBillingGroup + "','" + mBillingSubGroupCode.Trim() + "','"
                    + mBillingSubGroup + "','" + mBillingGroupMembershipNo.Trim() + "'," + (int)AfyaPro_Types.clsEnums.InvoiceTransTypes.Payment
                    + ",'" + mTransDescription + "',0," + mPaidForInvoice + "," + mYearPart + "," + mMonthPart + ",'" + mUserId.Trim() + "')";
                    mCommand.ExecuteNonQuery();

                    #endregion

                    #region billinvoicepaymentdetails

                    mCommand.CommandText = "insert into billinvoicepaymentdetails(sysdate,transdate,receiptno,invoiceno,patientcode,wardcode,roomcode,bedcode,"
                    + "billinggroupcode,billinggroupdescription,billingsubgroupcode,billingsubgroupdescription,billinggroupmembershipno,"
                    + "totaldue,totalpaid,yearpart,monthpart,userid) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                    + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mReceiptNo.Trim() + "','" + mInvoiceNo.Trim() + "','"
                    + mPatientCode.Trim() + "','"
                    + mAdmissionDetails.WardCode + "','" + mAdmissionDetails.RoomCode + "','" + mAdmissionDetails.BedCode + "','" 
                    + mBillingGroupCode.Trim() + "','" + mBillingGroup
                    + "','" + mBillingSubGroupCode.Trim() + "','" + mBillingSubGroup + "','" + mBillingGroupMembershipNo.Trim() + "',"
                    + mTotalDue + "," + mPaidForInvoice + "," + mYearPart + "," + mMonthPart + ",'" + mUserId.Trim() + "')";
                    mCommand.ExecuteNonQuery();

                    #endregion

                    #region billcollections

                    string mPaymentFieldName = "paytype" + mPayTypeCode.Trim().ToLower();
                    double mPaymentFieldValue = Convert.ToDouble(mDataRow["paidforbill"]);

                    mCommand.CommandText = "insert into billcollections(sysdate,transdate,receiptno,paymentsource,patientcode,wardcode,roomcode,bedcode,billinggroupcode,"
                    + "billinggroupdescription,bank,branch,holder,chequeno,transtype,yearpart,monthpart,userid," + mPaymentFieldName
                    + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                    + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mReceiptNo.Trim() + "'," + mPaymentSource + ",'" + mPatientCode + "','"
                    + mAdmissionDetails.WardCode + "','" + mAdmissionDetails.RoomCode + "','" + mAdmissionDetails.BedCode + "','"
                    + mBillingGroupCode.Trim() + "','"
                    + mBillingGroup + "','" + mBank + "','" + mBranch + "','" + mHolder + "','" + mChequeNo + "',"
                    + (int)AfyaPro_Types.clsEnums.BillingTransTypes.Payment + "," + mYearPart + "," + mMonthPart + ",'"
                    + mUserId.Trim() + "'," + mPaymentFieldValue + ")";
                    mCommand.ExecuteNonQuery();

                    #endregion

                    #region billdebtreliefrequestinvoices

                    if (mDtDebtRelief.Rows.Count > 0)
                    {
                        string mDebtReliefRequestNo = mDtDebtRelief.Rows[0]["requestno"].ToString().Trim();
                        mTotalDebtReliefApproved = mTotalDebtReliefApproved + mPaidForInvoice;

                        List<clsDataField> mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("approvedamount", DataTypes.dbdecimal.ToString(), mPaidForInvoice));

                        mCommand.CommandText = clsGlobal.Get_UpdateStatement("billdebtreliefrequestinvoices", mDataFields,
                            "requestno='" + mDebtReliefRequestNo.Trim() + "' and invoiceno='" + mInvoiceNo.Trim() + "'");
                        mCommand.ExecuteNonQuery();
                    }

                    #endregion
                }

                #region billdebtreliefrequests

                if (mDtDebtRelief.Rows.Count > 0)
                {
                    string mApprovalRemarks = mDtDebtRelief.Rows[0]["approvalremarks"].ToString().Trim();
                    string mDebtReliefRequestNo = mDtDebtRelief.Rows[0]["requestno"].ToString().Trim();

                    List<clsDataField> mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("approveddate", DataTypes.dbdatetime.ToString(), mTransDate));
                    mDataFields.Add(new clsDataField("totalapprovedamount", DataTypes.dbdecimal.ToString(), mTotalDebtReliefApproved));
                    mDataFields.Add(new clsDataField("approvalremarks", DataTypes.dbstring.ToString(), mApprovalRemarks));
                    mDataFields.Add(new clsDataField("approvedby", DataTypes.dbstring.ToString(), mUserId.Trim()));

                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("billdebtreliefrequests", mDataFields,
                        "requestno='" + mDebtReliefRequestNo.Trim() + "' and accountcode='" + mAccountCode.Trim() + "' and debtortype='" + mDebtorType.Trim() + "'");
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region billdebtors

                mCommand.CommandText = "update billdebtors set balance = balance - " + mAmtPaid
                + " where accountcode='" + mAccountCode.Trim() + "' and debtortype='" + mDebtorType.Trim() + "'";
                mCommand.ExecuteNonQuery();

                #endregion

                #region billdebtorslog

                mCommand.CommandText = "insert into billdebtorslog(sysdate,transdate,reference,accountcode,"
                + "accountdescription,debtortype,transtype,transdescription,debitamount,creditamount,yearpart,"
                + "monthpart,userid,fromwhomtowhomcode,fromwhomtowhom) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mReceiptNo.Trim() + "','" + mAccountCode.Trim() + "','"
                + mAccountDescription + "','" + mDebtorType.Trim() + "'," + (int)AfyaPro_Types.clsEnums.BillingTransTypes.Payment + ",'"
                + mTransDescription + "',0," + mAmtPaid + "," + mYearPart + "," + mMonthPart + ",'" + mUserId.Trim()
                + "','" + mFromWhomToWhomCode + "','" + mFromWhomToWhom + "')";
                mCommand.ExecuteNonQuery();

                #endregion

                #region billinvoicepayments

                mCommand.CommandText = "insert into billinvoicepayments(sysdate,transdate,receiptno,billinggroupcode,"
                + "billinggroupdescription,totalpaid,yearpart,monthpart,userid,currencycode,currencydescription,currencysymbol," 
                + "paytype" + mPayTypeCode.Trim().ToLower() + ") values(" + clsGlobal.Saving_DateValue(DateTime.Now) + "," 
                + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mReceiptNo.Trim() + "','" + mBillingGroupCode.Trim() 
                + "','" + mBillingGroup + "'," + mAmtPaid + "," + mYearPart + "," 
                + mMonthPart + ",'" + mUserId.Trim() + "','" + mCurrencyCode.Trim() + "','" + mCurrencyDescription.Trim()
                + "','" + mCurrencySymbol.Trim() + "'," + mAmtPaid + ")";

                mCommand.ExecuteNonQuery();

                #endregion

                #region prepare next receipt #

                if (mGenerateReceipt == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.paymentno);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                mBill.ReceiptNo = mReceiptNo;
                mBill.BillingGroupCode = mBillingGroupCode;

                //commit changes
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

        #region Void_CashSale
        public AfyaPro_Types.clsBill Void_CashSale(Int16 mGenerateVoidNo, string mVoidNo, string mReceiptNo,
            DateTime mTransDate, string mUserId, string mUserName)
        {
            String mFunctionName = "Void_CashSale";

            AfyaPro_Types.clsBill mBill = new AfyaPro_Types.clsBill();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

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

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.billsaleshistorycash_voidreceipt.ToString(), mUserId);
            if (mGranted == false)
            {
                mBill.Exe_Result = 0;
                mBill.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mBill;
            }
            #endregion

            #region auto generate voidno, if option is on

            if (mGenerateVoidNo == 1)
            {
                clsAutoCodes objAutoCodes = new clsAutoCodes();
                AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                mObjCode = objAutoCodes.Next_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.slcanno), "billvoidedsales", "voidno");
                if (mObjCode.Exe_Result == -1)
                {
                    mBill.Exe_Result = 0;
                    mBill.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DuplicateReceiptNoDetected.ToString();
                    return mBill;
                }
                mVoidNo = mObjCode.GeneratedCode;
            }

            #endregion

            #region check 4 duplicate voidno
            try
            {
                mCommand.CommandText = "select * from billvoidedsales where voidno='" + mVoidNo.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBill.Exe_Result = 0;
                    mBill.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DuplicateVoidNoDetected.ToString();
                    return mBill;
                }
            }
            catch (OdbcException ex)
            {
                mBill.Exe_Result = -1;
                mBill.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBill;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion

            #region do stuff

            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                int mYearPart = mTransDate.Year;
                int mMonthPart = mTransDate.Month;

                #region billvoidedsales

                DataTable mDtReceipts = new DataTable("billreceitps");
                mCommand.CommandText = "select * from billreceipts where receiptno='" + mReceiptNo.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtReceipts);

                mDtReceipts.Columns.Add("voidsource", typeof(System.Int16));

                foreach (DataRow mDataRow in mDtReceipts.Rows)
                {
                    mDataRow.BeginEdit();
                    mDataRow["sysdate"] = DateTime.Now;
                    mDataRow["transdate"] = mTransDate;
                    mDataRow["voidsource"] = Convert.ToInt16(AfyaPro_Types.clsEnums.VoidSources.CashSale);
                    mDataRow["voided"] = 1;
                    mDataRow["voidsysdate"] = DateTime.Now;
                    mDataRow["voidtransdate"] = mTransDate;
                    mDataRow["voidno"] = mVoidNo.Trim();
                    mDataRow["yearpart"] = mYearPart;
                    mDataRow["monthpart"] = mMonthPart;
                    mDataRow["voiduserid"] = mUserId.Trim();
                    mDataRow.EndEdit();
                    mDtReceipts.AcceptChanges();

                    mCommand.CommandText = clsGlobal.Build_InsertToTable(mDataRow, mDtReceipts.Columns, "billvoidedsales");
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region billvoidedsalesitems

                DataTable mDtReceiptItems = new DataTable("billreceiptitems");
                mCommand.CommandText = "select * from billreceiptitems where receiptno='" + mReceiptNo.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtReceiptItems);

                mDtReceiptItems.Columns.Add("voidsource", typeof(System.Int16));

                foreach (DataRow mDataRow in mDtReceiptItems.Rows)
                {
                    mDataRow.BeginEdit();
                    mDataRow["sysdate"] = DateTime.Now;
                    mDataRow["transdate"] = mTransDate;
                    mDataRow["voidsource"] = Convert.ToInt16(AfyaPro_Types.clsEnums.VoidSources.CashSale);
                    mDataRow["voidno"] = mVoidNo.Trim();
                    mDataRow["reference"] = mVoidNo.Trim();
                    mDataRow["qty"] = Convert.ToDouble(mDataRow["qty"]);
                    mDataRow["actualamount"] = Convert.ToDouble(mDataRow["actualamount"]);
                    mDataRow["amount"] = Convert.ToDouble(mDataRow["amount"]);
                    mDataRow["transtype"] = (int)AfyaPro_Types.clsEnums.BillingTransTypes.SaleCancellation;
                    mDataRow["yearpart"] = mYearPart;
                    mDataRow["monthpart"] = mMonthPart;
                    mDataRow["voiduserid"] = mUserId.Trim();
                    mDataRow.EndEdit();
                    mDtReceiptItems.AcceptChanges();

                    mCommand.CommandText = clsGlobal.Build_InsertToTable(mDataRow, mDtReceiptItems.Columns, "billvoidedsalesitems");
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region billreceipts

                mCommand.CommandText = "update billreceipts set voided=1, voidsysdate=" + clsGlobal.Saving_DateValue(DateTime.Now)
                + ",voidtransdate=" + clsGlobal.Saving_DateValue(mTransDate) + ",voiduserid='" + mUserId.Trim()
                + "',voidno='" + mVoidNo.Trim() + "' where receiptno='" + mReceiptNo.Trim() + "'";
                mCommand.ExecuteNonQuery();

                #endregion

                #region billreceiptitems

                mDtReceiptItems = new DataTable("billreceiptitems");
                mCommand.CommandText = "select * from billreceiptitems where receiptno='" + mReceiptNo.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtReceiptItems);

                foreach (DataRow mDataRow in mDtReceiptItems.Rows)
                {
                    mDataRow.BeginEdit();
                    mDataRow["sysdate"] = DateTime.Now;
                    mDataRow["transdate"] = mTransDate;
                    mDataRow["voidno"] = mVoidNo.Trim();
                    mDataRow["reference"] = mVoidNo.Trim();
                    mDataRow["qty"] = -Convert.ToDouble(mDataRow["qty"]);
                    mDataRow["actualamount"] = -Convert.ToDouble(mDataRow["actualamount"]);
                    mDataRow["amount"] = -Convert.ToDouble(mDataRow["amount"]);
                    mDataRow["transtype"] = (int)AfyaPro_Types.clsEnums.BillingTransTypes.SaleCancellation;
                    mDataRow["yearpart"] = mYearPart;
                    mDataRow["monthpart"] = mMonthPart;
                    mDataRow["voiduserid"] = mUserId.Trim();
                    mDataRow.EndEdit();
                    mDtReceiptItems.AcceptChanges();

                    mCommand.CommandText = clsGlobal.Build_InsertToTable(mDataRow, mDtReceiptItems.Columns, "billreceiptitems");
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region billsales

                mCommand.CommandText = "update billsales set voided=1, voidsysdate=" + clsGlobal.Saving_DateValue(DateTime.Now)
                + ",voidtransdate=" + clsGlobal.Saving_DateValue(mTransDate) + ",voiduserid='" + mUserId.Trim()
                + "',voidno='" + mVoidNo.Trim() + "' where receiptno='" + mReceiptNo.Trim() + "'";
                mCommand.ExecuteNonQuery();

                #endregion

                #region billsalesitems

                DataTable mDtSalesItems = new DataTable("billsalesitems");
                mCommand.CommandText = "select * from billsalesitems where receiptno='" + mReceiptNo.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSalesItems);

                foreach (DataRow mDataRow in mDtSalesItems.Rows)
                {
                    mDataRow.BeginEdit();
                    mDataRow["sysdate"] = DateTime.Now;
                    mDataRow["transdate"] = mTransDate;
                    mDataRow["voidno"] = mVoidNo.Trim();
                    mDataRow["reference"] = mVoidNo.Trim();
                    mDataRow["qty"] = -Convert.ToDouble(mDataRow["qty"]);
                    mDataRow["actualamount"] = -Convert.ToDouble(mDataRow["actualamount"]);
                    mDataRow["amount"] = -Convert.ToDouble(mDataRow["amount"]);
                    mDataRow["transtype"] = (int)AfyaPro_Types.clsEnums.BillingTransTypes.SaleCancellation;
                    mDataRow["yearpart"] = mYearPart;
                    mDataRow["monthpart"] = mMonthPart;
                    mDataRow["voiduserid"] = mUserId.Trim();
                    mDataRow.EndEdit();
                    mDtSalesItems.AcceptChanges();

                    mCommand.CommandText = clsGlobal.Build_InsertToTable(mDataRow, mDtSalesItems.Columns, "billsalesitems");
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region prepare next voidno

                if (mGenerateVoidNo == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set idcurrent=idcurrent+idincrement where codekey=" 
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.slcanno);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                mTrans.Commit();

                mBill.ReceiptNo = mReceiptNo;

                //return
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

            #endregion
        }
        #endregion

        #region Void_InvoiceSale
        public AfyaPro_Types.clsBill Void_InvoiceSale(Int16 mGenerateVoidNo, string mVoidNo, string mInvoiceNo,
            DateTime mTransDate, string mUserId, string mUserName)
        {
            String mFunctionName = "Void_InvoiceSale";

            AfyaPro_Types.clsBill mBill = new AfyaPro_Types.clsBill();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

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

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.billsaleshistoryinvoices_voidinvoice.ToString(), mUserId);
            if (mGranted == false)
            {
                mBill.Exe_Result = 0;
                mBill.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mBill;
            }
            #endregion

            #region auto generate voidno, if option is on

            if (mGenerateVoidNo == 1)
            {
                clsAutoCodes objAutoCodes = new clsAutoCodes();
                AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                mObjCode = objAutoCodes.Next_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.slcanno), "billvoidedsales", "voidno");
                if (mObjCode.Exe_Result == -1)
                {
                    mBill.Exe_Result = 0;
                    mBill.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DuplicateReceiptNoDetected.ToString();
                    return mBill;
                }
                mVoidNo = mObjCode.GeneratedCode;
            }

            #endregion

            #region check 4 duplicate voidno
            try
            {
                mCommand.CommandText = "select * from billvoidedsales where voidno='" + mVoidNo.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBill.Exe_Result = 0;
                    mBill.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DuplicateVoidNoDetected.ToString();
                    return mBill;
                }
            }
            catch (OdbcException ex)
            {
                mBill.Exe_Result = -1;
                mBill.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBill;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion

            #region do stuff

            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                int mYearPart = mTransDate.Year;
                int mMonthPart = mTransDate.Month;

                string mAccountCode = "";
                string mAccountDescription = "";
                string mDebtorType = "Individual";
                double mInvoiceAmount = 0;
                string mTransDescription = "Voided Invoice";

                #region billvoidedsales

                DataTable mDtInvoices = new DataTable("billinvoices");
                mCommand.CommandText = "select * from billinvoices where invoiceno='" + mInvoiceNo.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtInvoices);

                if (mDtInvoices.Rows.Count > 0)
                {
                    if (mDtInvoices.Rows[0]["billinggroupcode"].ToString().Trim() == "")
                    {
                        mAccountCode = mDtInvoices.Rows[0]["patientcode"].ToString().Trim();

                        DataTable mDtPatients = new DataTable("patients");
                        mCommand.CommandText = "select * from patients where code='" + mAccountCode + "'";
                        mDataAdapter.SelectCommand = mCommand;
                        mDataAdapter.Fill(mDtPatients);

                        if (mDtPatients.Rows.Count > 0)
                        {
                            mAccountDescription = mDtPatients.Rows[0]["firstname"].ToString().Trim();
                            if (mDtPatients.Rows[0]["othernames"].ToString().Trim() != "")
                            {
                                mAccountDescription = mAccountDescription + " " + mDtPatients.Rows[0]["othernames"].ToString().Trim();
                            }
                            mAccountDescription = mAccountDescription + " " + mDtPatients.Rows[0]["surname"].ToString().Trim();
                        }

                        mDebtorType = "Individual";
                        mInvoiceAmount = Convert.ToDouble(mDtInvoices.Rows[0]["balancedue"]);
                    }
                    else
                    {
                        mAccountCode = mDtInvoices.Rows[0]["billinggroupcode"].ToString().Trim();
                        mAccountDescription = mDtInvoices.Rows[0]["billinggroupdescription"].ToString().Trim();
                        mDebtorType = "Group";
                        mInvoiceAmount = Convert.ToDouble(mDtInvoices.Rows[0]["balancedue"]);
                    }
                }

                mDtInvoices.Columns.Add("voidsource", typeof(System.Int16));

                foreach (DataRow mDataRow in mDtInvoices.Rows)
                {
                    mDataRow.BeginEdit();
                    mDataRow["sysdate"] = DateTime.Now;
                    mDataRow["transdate"] = mTransDate;
                    mDataRow["status"] = Convert.ToInt16(AfyaPro_Types.clsEnums.InvoiceStatus.Cancelled);
                    mDataRow["voidsource"] = Convert.ToInt16(AfyaPro_Types.clsEnums.VoidSources.InvoiceSale);
                    mDataRow["voided"] = 1;
                    mDataRow["voidsysdate"] = DateTime.Now;
                    mDataRow["voidtransdate"] = mTransDate;
                    mDataRow["voidno"] = mVoidNo.Trim();
                    mDataRow["yearpart"] = mYearPart;
                    mDataRow["monthpart"] = mMonthPart;
                    mDataRow["voiduserid"] = mUserId.Trim();
                    mDataRow.EndEdit();
                    mDtInvoices.AcceptChanges();

                    mCommand.CommandText = clsGlobal.Build_InsertToTable(mDataRow, mDtInvoices.Columns, "billvoidedsales");
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region billvoidedsalesitems

                DataTable mDtInvoiceItems = new DataTable("billinvoiceitems");
                mCommand.CommandText = "select * from billinvoiceitems where invoiceno='" + mInvoiceNo.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtInvoiceItems);

                mDtInvoiceItems.Columns.Add("voidsource", typeof(System.Int16));

                foreach (DataRow mDataRow in mDtInvoiceItems.Rows)
                {
                    mDataRow.BeginEdit();
                    mDataRow["sysdate"] = DateTime.Now;
                    mDataRow["transdate"] = mTransDate;
                    mDataRow["voidsource"] = Convert.ToInt16(AfyaPro_Types.clsEnums.VoidSources.InvoiceSale);
                    mDataRow["voidno"] = mVoidNo.Trim();
                    mDataRow["reference"] = mVoidNo.Trim();
                    mDataRow["qty"] = Convert.ToDouble(mDataRow["qty"]);
                    mDataRow["actualamount"] = Convert.ToDouble(mDataRow["actualamount"]);
                    mDataRow["amount"] = Convert.ToDouble(mDataRow["amount"]);
                    mDataRow["transtype"] = (int)AfyaPro_Types.clsEnums.BillingTransTypes.SaleCancellation;
                    mDataRow["yearpart"] = mYearPart;
                    mDataRow["monthpart"] = mMonthPart;
                    mDataRow["voiduserid"] = mUserId.Trim();
                    mDataRow.EndEdit();
                    mDtInvoiceItems.AcceptChanges();

                    mCommand.CommandText = clsGlobal.Build_InsertToTable(mDataRow, mDtInvoiceItems.Columns, "billvoidedsalesitems");
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region billinvoices

                mCommand.CommandText = "update billinvoices set voided=1, voidsysdate=" + clsGlobal.Saving_DateValue(DateTime.Now)
                + ",voidtransdate=" + clsGlobal.Saving_DateValue(mTransDate) + ",voiduserid='" + mUserId.Trim()
                + "',voidno='" + mVoidNo.Trim() + "',status=" + Convert.ToInt16(AfyaPro_Types.clsEnums.InvoiceStatus.Cancelled)
                + " where invoiceno='" + mInvoiceNo.Trim() + "'";
                mCommand.ExecuteNonQuery();

                #endregion

                #region billinvoiceitems

                mDtInvoiceItems = new DataTable("billinvoiceitems");
                mCommand.CommandText = "select * from billinvoiceitems where invoiceno='" + mInvoiceNo.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtInvoiceItems);

                foreach (DataRow mDataRow in mDtInvoiceItems.Rows)
                {
                    mDataRow.BeginEdit();
                    mDataRow["sysdate"] = DateTime.Now;
                    mDataRow["transdate"] = mTransDate;
                    mDataRow["voidno"] = mVoidNo.Trim();
                    mDataRow["reference"] = mVoidNo.Trim();
                    mDataRow["qty"] = -Convert.ToDouble(mDataRow["qty"]);
                    mDataRow["actualamount"] = -Convert.ToDouble(mDataRow["actualamount"]);
                    mDataRow["amount"] = -Convert.ToDouble(mDataRow["amount"]);
                    mDataRow["transtype"] = (int)AfyaPro_Types.clsEnums.BillingTransTypes.SaleCancellation;
                    mDataRow["yearpart"] = mYearPart;
                    mDataRow["monthpart"] = mMonthPart;
                    mDataRow["voiduserid"] = mUserId.Trim();
                    mDataRow.EndEdit();
                    mDtInvoiceItems.AcceptChanges();

                    mCommand.CommandText = clsGlobal.Build_InsertToTable(mDataRow, mDtInvoiceItems.Columns, "billinvoiceitems");
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region billsales

                mCommand.CommandText = "update billsales set voided=1, voidsysdate=" + clsGlobal.Saving_DateValue(DateTime.Now)
                + ",voidtransdate=" + clsGlobal.Saving_DateValue(mTransDate) + ",voiduserid='" + mUserId.Trim()
                + "',voidno='" + mVoidNo.Trim() + "' where invoiceno='" + mInvoiceNo.Trim() + "'";
                mCommand.ExecuteNonQuery();

                #endregion

                #region billsalesitems

                DataTable mDtSalesItems = new DataTable("billsalesitems");
                mCommand.CommandText = "select * from billsalesitems where invoiceno='" + mInvoiceNo.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSalesItems);

                foreach (DataRow mDataRow in mDtSalesItems.Rows)
                {
                    mDataRow.BeginEdit();
                    mDataRow["sysdate"] = DateTime.Now;
                    mDataRow["transdate"] = mTransDate;
                    mDataRow["voidno"] = mVoidNo.Trim();
                    mDataRow["reference"] = mVoidNo.Trim();
                    mDataRow["qty"] = -Convert.ToDouble(mDataRow["qty"]);
                    mDataRow["actualamount"] = -Convert.ToDouble(mDataRow["actualamount"]);
                    mDataRow["amount"] = -Convert.ToDouble(mDataRow["amount"]);
                    mDataRow["transtype"] = (int)AfyaPro_Types.clsEnums.BillingTransTypes.SaleCancellation;
                    mDataRow["yearpart"] = mYearPart;
                    mDataRow["monthpart"] = mMonthPart;
                    mDataRow["voiduserid"] = mUserId.Trim();
                    mDataRow.EndEdit();
                    mDtSalesItems.AcceptChanges();

                    mCommand.CommandText = clsGlobal.Build_InsertToTable(mDataRow, mDtSalesItems.Columns, "billsalesitems");
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region billdebtors

                mCommand.CommandText = "update billdebtors set balance = balance - " + mInvoiceAmount
                + " where accountcode='" + mAccountCode.Trim() + "' and debtortype='" + mDebtorType.Trim() + "'";
                mCommand.ExecuteNonQuery();

                #endregion

                #region billdebtorslog

                mCommand.CommandText = "insert into billdebtorslog(sysdate,transdate,reference,accountcode,"
                + "accountdescription,debtortype,transtype,transdescription,debitamount,creditamount,yearpart,"
                + "monthpart,userid,fromwhomtowhomcode,fromwhomtowhom) values(" + clsGlobal.Saving_DateValue(DateTime.Now) + ","
                + clsGlobal.Saving_DateValue(mTransDate) + ",'" + mVoidNo.Trim() + "','" + mAccountCode.Trim() + "','"
                + mAccountDescription + "','" + mDebtorType.Trim() + "'," + (int)AfyaPro_Types.clsEnums.BillingTransTypes.SaleCancellation + ",'"
                + mTransDescription + "',0," + mInvoiceAmount + "," + mYearPart + "," + mMonthPart + ",'" + mUserId.Trim()
                + "','" + mAccountCode + "','" + mAccountDescription + "')";
                mCommand.ExecuteNonQuery();

                #endregion

                #region prepare next voidno

                if (mGenerateVoidNo == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.slcanno);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                mTrans.Commit();

                mBill.ReceiptNo = mInvoiceNo;

                //return
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

            #endregion
        }
        #endregion

        #region Void_InvoicePayment
        public AfyaPro_Types.clsBill Void_InvoicePayment(Int16 mGenerateVoidNo, string mVoidNo, string mReceiptNo,
            DateTime mTransDate, string mUserId, string mUserName)
        {
            String mFunctionName = "Void_InvoicePayment";

            AfyaPro_Types.clsBill mBill = new AfyaPro_Types.clsBill();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

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

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.billpaymentshistory_voidreceipt.ToString(), mUserId);
            if (mGranted == false)
            {
                mBill.Exe_Result = 0;
                mBill.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mBill;
            }
            #endregion

            #region auto generate voidno, if option is on

            if (mGenerateVoidNo == 1)
            {
                clsAutoCodes objAutoCodes = new clsAutoCodes();
                AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                mObjCode = objAutoCodes.Next_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.slcanno), "billvoidedsales", "voidno");
                if (mObjCode.Exe_Result == -1)
                {
                    mBill.Exe_Result = 0;
                    mBill.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DuplicateReceiptNoDetected.ToString();
                    return mBill;
                }
                mVoidNo = mObjCode.GeneratedCode;
            }

            #endregion

            #region check 4 duplicate voidno
            try
            {
                mCommand.CommandText = "select * from billvoidedsales where voidno='" + mVoidNo.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mBill.Exe_Result = 0;
                    mBill.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DuplicateVoidNoDetected.ToString();
                    return mBill;
                }
            }
            catch (OdbcException ex)
            {
                mBill.Exe_Result = -1;
                mBill.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mBill;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion

            #region do stuff

            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                int mYearPart = mTransDate.Year;
                int mMonthPart = mTransDate.Month;

                #region billvoidedsales

                DataTable mDtInvoices = new DataTable("billinvoicepayments");
                mCommand.CommandText = "select * from billinvoicepayments where receiptno='" + mReceiptNo.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtInvoices);

                mDtInvoices.Columns.Add("voidsource", typeof(System.Int16));

                foreach (DataRow mDataRow in mDtInvoices.Rows)
                {
                    mDataRow.BeginEdit();
                    mDataRow["sysdate"] = DateTime.Now;
                    mDataRow["transdate"] = mTransDate;
                    mDataRow["voidsource"] = Convert.ToInt16(AfyaPro_Types.clsEnums.VoidSources.InvoicePayment);
                    mDataRow["voided"] = 1;
                    mDataRow["voidsysdate"] = DateTime.Now;
                    mDataRow["voidtransdate"] = mTransDate;
                    mDataRow["voidno"] = mVoidNo.Trim();
                    mDataRow["yearpart"] = mYearPart;
                    mDataRow["monthpart"] = mMonthPart;
                    mDataRow["voiduserid"] = mUserId.Trim();
                    mDataRow.EndEdit();
                    mDtInvoices.AcceptChanges();

                    mCommand.CommandText = clsGlobal.Build_InsertToTable(mDataRow, mDtInvoices.Columns, "billvoidedsales");
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region billinvoicepayments

                mCommand.CommandText = "update billinvoicepayments set voided=1, voidsysdate=" + clsGlobal.Saving_DateValue(DateTime.Now)
                + ",voidtransdate=" + clsGlobal.Saving_DateValue(mTransDate) + ",voiduserid='" + mUserId.Trim()
                + "',voidno='" + mVoidNo.Trim() + "' where receiptno='" + mReceiptNo.Trim() + "'";
                mCommand.ExecuteNonQuery();

                #endregion

                #region prepare next voidno

                if (mGenerateVoidNo == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.slcanno);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                mTrans.Commit();

                mBill.ReceiptNo = mReceiptNo;

                //return
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

            #endregion
        }
        #endregion

        #region Refund
        public AfyaPro_Types.clsBill Refund(string mReceiptNo, Int16 mPaymentSource,
            DateTime mTransDate, DataTable mDtRefunds, string mUserId, string mUserName)
        {
            String mFunctionName = "Refund";

            AfyaPro_Types.clsBill mBill = new AfyaPro_Types.clsBill();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

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

            #region do stuff

            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                int mYearPart = mTransDate.Year;
                int mMonthPart = mTransDate.Month;

                #region billrefunds

                DataTable mDtCollections = new DataTable("billcollections");
                mCommand.CommandText = "select * from billcollections where receiptno='" + mReceiptNo.Trim() 
                + "' and paymentsource=" + mPaymentSource;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtCollections);

                foreach (DataRow mRefundRow in mDtRefunds.Rows)
                {
                    if (Convert.ToDouble(mRefundRow["refundedamount"]) > 0)
                    {
                        if (mDtCollections.Rows.Count > 0)
                        {
                            DataRow mDataRow = mDtCollections.Rows[0];

                            mDataRow.BeginEdit();
                            mDataRow["sysdate"] = DateTime.Now;
                            mDataRow["transdate"] = mTransDate;
                            mDataRow["refunded"] = 1;
                            mDataRow["refundsysdate"] = DateTime.Now;
                            mDataRow["refundtransdate"] = mTransDate;
                            mDataRow["yearpart"] = mYearPart;
                            mDataRow["monthpart"] = mMonthPart;
                            mDataRow["refunduserid"] = mUserId.Trim();
                            mDataRow["bank"] = mRefundRow["bank"].ToString().Trim();
                            mDataRow["branch"] = mRefundRow["branch"].ToString().Trim();
                            mDataRow["holder"] = mRefundRow["holder"].ToString().Trim();
                            mDataRow["chequeno"] = mRefundRow["chequeno"].ToString().Trim();
                            mDataRow[mRefundRow["paytypecode"].ToString().Trim().ToLower()] = Convert.ToDouble(mRefundRow["refundedamount"]);

                            mDataRow.EndEdit();
                            mDtCollections.AcceptChanges();

                            mCommand.CommandText = clsGlobal.Build_InsertToTable(mDataRow, mDtCollections.Columns, "billrefunds");
                            mCommand.ExecuteNonQuery();
                        }
                    }
                }

                #endregion

                #region billcollections

                foreach (DataRow mRefundRow in mDtRefunds.Rows)
                {
                    if (Convert.ToDouble(mRefundRow["refundedamount"]) > 0)
                    {
                        if (mDtCollections.Rows.Count > 0)
                        {
                            DataRow mDataRow = mDtCollections.Rows[0];

                            mDataRow.BeginEdit();
                            mDataRow["sysdate"] = DateTime.Now;
                            mDataRow["transdate"] = mTransDate;
                            mDataRow["refunded"] = 1;
                            mDataRow["refundsysdate"] = DateTime.Now;
                            mDataRow["refundtransdate"] = mTransDate;
                            mDataRow["yearpart"] = mYearPart;
                            mDataRow["monthpart"] = mMonthPart;
                            mDataRow["refunduserid"] = mUserId.Trim();
                            mDataRow["bank"] = mRefundRow["bank"].ToString().Trim();
                            mDataRow["branch"] = mRefundRow["branch"].ToString().Trim();
                            mDataRow["holder"] = mRefundRow["holder"].ToString().Trim();
                            mDataRow["chequeno"] = mRefundRow["chequeno"].ToString().Trim();
                            mDataRow[mRefundRow["paytypecode"].ToString().Trim().ToLower()] = -Convert.ToDouble(mRefundRow["refundedamount"]);

                            mDataRow.EndEdit();
                            mDtCollections.AcceptChanges();

                            mCommand.CommandText = clsGlobal.Build_InsertToTable(mDataRow, mDtCollections.Columns, "billcollections");
                            mCommand.ExecuteNonQuery();
                        }
                    }
                }

                #endregion

                mTrans.Commit();

                mBill.ReceiptNo = mReceiptNo;

                //return
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

            #endregion
        }
        #endregion
    }
}
