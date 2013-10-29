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
    public class clsSearchEngine : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsSearchEngine";

        #endregion

        #region Get_SearchObjects
        public DataTable Get_SearchObjects(string mLanguageName, string mGridName)
        {
            String mFunctionName = "Get_SearchObjects";

            try
            {
                DataTable mDataTable = new DataTable("searchobjects");
                mDataTable.Columns.Add("code", typeof(System.String));
                mDataTable.Columns.Add("description", typeof(System.String));
                mDataTable.RemotingFormat = SerializationFormat.Binary;

                DataRow mNewRow;

                #region load searchobject
                
                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "searchobject" + (int)AfyaPro_Types.clsEnums.SearchObjects.Patients;
                mNewRow["description"] = "Patients";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "searchobject" + (int)AfyaPro_Types.clsEnums.SearchObjects.Products;
                mNewRow["description"] = "Products";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "searchobject" + (int)AfyaPro_Types.clsEnums.SearchObjects.ClientGroupMembers;
                mNewRow["description"] = "Client Group Members";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "searchobject" + (int)AfyaPro_Types.clsEnums.SearchObjects.ClientGroups;
                mNewRow["description"] = "Client Groups";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "searchobject" + (int)AfyaPro_Types.clsEnums.SearchObjects.BillingItems;
                mNewRow["description"] = "Billing Items";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "searchobject" + (int)AfyaPro_Types.clsEnums.SearchObjects.Debtors;
                mNewRow["description"] = "Debtors";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "searchobject" + (int)AfyaPro_Types.clsEnums.SearchObjects.Diagnoses;
                mNewRow["description"] = "Diagnoses";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "searchobject" + (int)AfyaPro_Types.clsEnums.SearchObjects.Suppliers;
                mNewRow["description"] = "Suppliers";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "searchobject" + (int)AfyaPro_Types.clsEnums.SearchObjects.Stores;
                mNewRow["description"] = "Stores";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "searchobject" + (int)AfyaPro_Types.clsEnums.SearchObjects.MtuhaDiagnoses;
                mNewRow["description"] = "Mtuha Diagnoses";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "searchobject" + (int)AfyaPro_Types.clsEnums.SearchObjects.RCHClients;
                mNewRow["description"] = "RCH Clients";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "searchobject" + (int)AfyaPro_Types.clsEnums.SearchObjects.DXTIndicators;
                mNewRow["description"] = "Diagnoses Indicators";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "searchobject" + (int)AfyaPro_Types.clsEnums.SearchObjects.CTCClients;
                mNewRow["description"] = "CTC Clients";
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

        #region Search_ProductsStock
        public DataTable Search_ProductsStock(string mStoreCode, string mFilter, string mOrder, bool mExpiryDate)
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            string mControlTable = clsGlobal.gInventoryDbName + mStoreCode.Trim().ToLower() + clsGlobal.gDbNameTableNameSep + "productcontrol";
            string mProductsTable = clsGlobal.gAfyaProDbName + clsGlobal.gDbNameTableNameSep + "som_products";

            string mFunctionName = "Search_ProductsStock";

            #region inventory connection

            try
            {
                mConn.ConnectionString = clsGlobal.Create_GenConStr(clsGlobal.gInventoryDbName + mStoreCode.Trim().ToLower());

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
                #region products

                string mCommandText = "select * from " + mProductsTable + " where (display=1 and visible_" + mStoreCode.Trim().ToLower() + "=1)";
                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " and (" + mFilter + ")";
                }
                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }

                mCommand.CommandText = mCommandText;
                DataTable mDtProducts = new DataTable("products");
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtProducts);

                #endregion

                #region control

                if (mExpiryDate == true)
                {
                    mCommandText = "select productcode,expirydate,sum(qty) qty from " + mControlTable
                    + " group by productcode,expirydate order by productcode, expirydate";
                }
                else
                {
                    mCommandText = "select productcode,sum(qty) qty from " + mControlTable
                     + " group by productcode order by productcode";
                }

                mCommand.CommandText = mCommandText;
                DataTable mDtControl = new DataTable("productcontrol");
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtControl);

                #endregion

                DataTable mDataTable = new DataTable("products");
                mDataTable.Columns.Add("code", typeof(System.String));
                mDataTable.Columns.Add("description", typeof(System.String));
                mDataTable.Columns.Add("opmcode", typeof(System.String));
                mDataTable.Columns.Add("opmdescription", typeof(System.String));
                mDataTable.Columns.Add("departmentcode", typeof(System.String));
                mDataTable.Columns.Add("departmentdescription", typeof(System.String));
                mDataTable.Columns.Add("packagingcode", typeof(System.String));
                mDataTable.Columns.Add("packagingdescription", typeof(System.String));
                mDataTable.Columns.Add("piecesinpackage", typeof(System.String));
                mDataTable.Columns.Add("costprice", typeof(System.Double));
                mDataTable.Columns.Add("price1", typeof(System.Double));
                mDataTable.Columns.Add("price2", typeof(System.Double));
                mDataTable.Columns.Add("price3", typeof(System.Double));
                mDataTable.Columns.Add("price4", typeof(System.Double));
                mDataTable.Columns.Add("price5", typeof(System.Double));
                mDataTable.Columns.Add("price6", typeof(System.Double));
                mDataTable.Columns.Add("price7", typeof(System.Double));
                mDataTable.Columns.Add("price8", typeof(System.Double));
                mDataTable.Columns.Add("price9", typeof(System.Double));
                mDataTable.Columns.Add("price10", typeof(System.Double));
                mDataTable.Columns.Add("hasexpiry", typeof(System.Int32));
                mDataTable.Columns.Add("expirydate", typeof(System.DateTime));
                mDataTable.Columns.Add("qty", typeof(System.Int32));

                DataView mDvControl = new DataView();
                mDvControl.Table = mDtControl;

                foreach (DataRow mDataRow in mDtProducts.Rows)
                {
                    mDvControl.RowFilter = "productcode='" + mDataRow["code"].ToString().Trim() + "'";

                    if (mDvControl.Count > 0)
                    {
                        foreach (DataRowView mDataRowView in mDvControl)
                        {
                            DataRow mNewRow = mDataTable.NewRow();

                            #region add current row

                            if (mExpiryDate == true)
                            {
                                mDvControl.Sort = "expirydate";
                                mNewRow["code"] = mDataRow["code"].ToString();
                                mNewRow["description"] = mDataRow["description"].ToString();
                                mNewRow["opmcode"] = mDataRow["opmcode"].ToString();
                                mNewRow["opmdescription"] = mDataRow["opmdescription"].ToString();
                                mNewRow["departmentcode"] = mDataRow["departmentcode"].ToString();
                                mNewRow["departmentdescription"] = mDataRow["departmentdescription"].ToString();
                                mNewRow["packagingcode"] = mDataRow["packagingcode"].ToString();
                                mNewRow["packagingdescription"] = mDataRow["packagingdescription"].ToString();
                                mNewRow["piecesinpackage"] = Convert.ToInt32(mDataRow["piecesinpackage"]);
                                mNewRow["costprice"] = Convert.ToDouble(mDataRow["costprice"]);
                                mNewRow["price1"] = Convert.ToDouble(mDataRow["price1"]);
                                mNewRow["price2"] = Convert.ToDouble(mDataRow["price2"]);
                                mNewRow["price3"] = Convert.ToDouble(mDataRow["price3"]);
                                mNewRow["price4"] = Convert.ToDouble(mDataRow["price4"]);
                                mNewRow["price5"] = Convert.ToDouble(mDataRow["price5"]);
                                mNewRow["price6"] = Convert.ToDouble(mDataRow["price6"]);
                                mNewRow["price7"] = Convert.ToDouble(mDataRow["price7"]);
                                mNewRow["price8"] = Convert.ToDouble(mDataRow["price8"]);
                                mNewRow["price9"] = Convert.ToDouble(mDataRow["price9"]);
                                mNewRow["price10"] = Convert.ToDouble(mDataRow["price10"]);
                                mNewRow["hasexpiry"] = Convert.ToInt32(mDataRow["hasexpiry"]);

                                if (clsGlobal.IsNullDate(mDataRowView["expirydate"]) == false)
                                {
                                    mNewRow["expirydate"] = Convert.ToDateTime(mDataRowView["expirydate"]);
                                }
                                else
                                {
                                    mNewRow["expirydate"] = new DateTime();
                                }
                                mNewRow["qty"] = Convert.ToDouble(mDataRowView["qty"]);
                                mDataTable.Rows.Add(mNewRow);
                                mDataTable.AcceptChanges();
                            }
                            else
                            {
                                mNewRow["code"] = mDataRow["code"].ToString();
                                mNewRow["description"] = mDataRow["description"].ToString();
                                mNewRow["opmcode"] = mDataRow["opmcode"].ToString();
                                mNewRow["opmdescription"] = mDataRow["opmdescription"].ToString();
                                mNewRow["departmentcode"] = mDataRow["departmentcode"].ToString();
                                mNewRow["departmentdescription"] = mDataRow["departmentdescription"].ToString();
                                mNewRow["packagingcode"] = mDataRow["packagingcode"].ToString();
                                mNewRow["packagingdescription"] = mDataRow["packagingdescription"].ToString();
                                mNewRow["piecesinpackage"] = Convert.ToInt32(mDataRow["piecesinpackage"]);
                                mNewRow["costprice"] = Convert.ToDouble(mDataRow["costprice"]);
                                mNewRow["price1"] = Convert.ToDouble(mDataRow["price1"]);
                                mNewRow["price2"] = Convert.ToDouble(mDataRow["price2"]);
                                mNewRow["price3"] = Convert.ToDouble(mDataRow["price3"]);
                                mNewRow["price4"] = Convert.ToDouble(mDataRow["price4"]);
                                mNewRow["price5"] = Convert.ToDouble(mDataRow["price5"]);
                                mNewRow["price6"] = Convert.ToDouble(mDataRow["price6"]);
                                mNewRow["price7"] = Convert.ToDouble(mDataRow["price7"]);
                                mNewRow["price8"] = Convert.ToDouble(mDataRow["price8"]);
                                mNewRow["price9"] = Convert.ToDouble(mDataRow["price9"]);
                                mNewRow["price10"] = Convert.ToDouble(mDataRow["price10"]);
                                mNewRow["hasexpiry"] = Convert.ToInt32(mDataRow["hasexpiry"]);
                                mNewRow["expirydate"] = new DateTime();
                                mNewRow["qty"] = Convert.ToDouble(mDataRowView["qty"]);
                                mDataTable.Rows.Add(mNewRow);
                                mDataTable.AcceptChanges();
                            }

                            #endregion
                        }
                    }
                    else
                    {
                        DataRow mNewRow = mDataTable.NewRow();

                        #region add current row

                        mNewRow["code"] = mDataRow["code"].ToString();
                        mNewRow["description"] = mDataRow["description"].ToString();
                        mNewRow["opmcode"] = mDataRow["opmcode"].ToString();
                        mNewRow["opmdescription"] = mDataRow["opmdescription"].ToString();
                        mNewRow["departmentcode"] = mDataRow["departmentcode"].ToString();
                        mNewRow["departmentdescription"] = mDataRow["departmentdescription"].ToString();
                        mNewRow["packagingcode"] = mDataRow["packagingcode"].ToString();
                        mNewRow["packagingdescription"] = mDataRow["packagingdescription"].ToString();
                        mNewRow["piecesinpackage"] = Convert.ToInt32(mDataRow["piecesinpackage"]);
                        mNewRow["costprice"] = Convert.ToDouble(mDataRow["costprice"]);
                        mNewRow["price1"] = Convert.ToDouble(mDataRow["price1"]);
                        mNewRow["price2"] = Convert.ToDouble(mDataRow["price2"]);
                        mNewRow["price3"] = Convert.ToDouble(mDataRow["price3"]);
                        mNewRow["price4"] = Convert.ToDouble(mDataRow["price4"]);
                        mNewRow["price5"] = Convert.ToDouble(mDataRow["price5"]);
                        mNewRow["price6"] = Convert.ToDouble(mDataRow["price6"]);
                        mNewRow["price7"] = Convert.ToDouble(mDataRow["price7"]);
                        mNewRow["price8"] = Convert.ToDouble(mDataRow["price8"]);
                        mNewRow["price9"] = Convert.ToDouble(mDataRow["price9"]);
                        mNewRow["price10"] = Convert.ToDouble(mDataRow["price10"]);
                        mNewRow["hasexpiry"] = Convert.ToInt32(mDataRow["hasexpiry"]);
                        mNewRow["expirydate"] = new DateTime();
                        mNewRow["qty"] = 0;
                        mDataTable.Rows.Add(mNewRow);
                        mDataTable.AcceptChanges();

                        #endregion
                    }
                }

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

        #region Get_CTCClients
        public DataTable Get_CTCClients(String mFilter, String mOrder)
        {
            String mFunctionName = "Get_CTCClients";

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
                //columns from patients
                string mPatientColumns = clsGlobal.Get_TableColumns(mCommand, "patients", "", "p", "");
                mPatientColumns = mPatientColumns + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "fullname");
                mPatientColumns = mPatientColumns + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "now()", ""), "age");
                //columns from ctc_patients
                string mCTCColumns = clsGlobal.Get_TableColumns(mCommand, "ctc_patients", "", "b", "ctc");

                string mCommandText = ""
                                    + "SELECT "
                                    + mCTCColumns + ","
                                    + mPatientColumns + " "
                                    + "FROM patients AS p "
                                    + "LEFT OUTER JOIN ctc_patients AS b ON p.code = b.patientcode";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                //clsGlobal.Write_Error(pClassName, mFunctionName, mCommandText);

                DataTable mDataTable = new DataTable("patients");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mDataTable.TableName);

                //patient extra fields
                DataTable mDtExtraFields = new DataTable("extrafields");
                mCommand.CommandText = "select * from patientextrafields";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtExtraFields);

                DataView mDvExtraFields = new DataView();
                mDvExtraFields.Table = mDtExtraFields;
                mDvExtraFields.Sort = "fieldname";

                foreach (DataColumn mDataColumn in mDataTable.Columns)
                {
                    int mRowIndex = mDvExtraFields.Find(mDataColumn.ColumnName);
                    if (mRowIndex >= 0)
                    {
                        mDataColumn.Caption = mDvExtraFields[mRowIndex]["fieldcaption"].ToString().Trim();
                    }
                }

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

        #region Get_Patients
        public DataTable Get_Patients(String mFilter, String mOrder)
        {
            String mFunctionName = "Get_Patients";

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
                //columns from patients
                string mPatientColumns = clsGlobal.Get_TableColumns(mCommand, "patients", "", "p", "");
                mPatientColumns = mPatientColumns + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "fullname");
                mPatientColumns = mPatientColumns + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "now()", ""), "age");

                string mCommandText = ""
                                    + "SELECT "
                                    + mPatientColumns + " "
                                    + "FROM patients AS p";
                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                DataTable mDataTable = new DataTable("patients");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mDataTable.TableName);

                //patient extra fields
                DataTable mDtExtraFields = new DataTable("extrafields");
                mCommand.CommandText = "select * from patientextrafields";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtExtraFields);

                DataView mDvExtraFields = new DataView();
                mDvExtraFields.Table = mDtExtraFields;
                mDvExtraFields.Sort = "fieldname";

                foreach (DataColumn mDataColumn in mDataTable.Columns)
                {
                    int mRowIndex = mDvExtraFields.Find(mDataColumn.ColumnName);
                    if (mRowIndex >= 0)
                    {
                        mDataColumn.Caption = mDvExtraFields[mRowIndex]["fieldcaption"].ToString().Trim();
                    }
                }

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

        #region Get_RCHPatients
        public DataTable Get_RCHPatients(String mFilter, String mOrder)
        {
            String mFunctionName = "Get_RCHPatients";

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
                //columns from patients
                string mPatientColumns = clsGlobal.Get_TableColumns(mCommand, "patients", "", "p", "");
                mPatientColumns = mPatientColumns + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "fullname");
                mPatientColumns = mPatientColumns + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "now()", ""), "age");

                string mCommandText = ""
                                    + "SELECT "
                                    + mPatientColumns + " "
                                    + "FROM patients AS p";
                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                DataTable mDataTable = new DataTable("patients");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, mDataTable.TableName);

                //patient extra fields
                DataTable mDtExtraFields = new DataTable("extrafields");
                mCommand.CommandText = "select * from patientextrafields";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtExtraFields);

                DataView mDvExtraFields = new DataView();
                mDvExtraFields.Table = mDtExtraFields;
                mDvExtraFields.Sort = "fieldname";

                foreach (DataColumn mDataColumn in mDataTable.Columns)
                {
                    int mRowIndex = mDvExtraFields.Find(mDataColumn.ColumnName);
                    if (mRowIndex >= 0)
                    {
                        mDataColumn.Caption = mDvExtraFields[mRowIndex]["fieldcaption"].ToString().Trim();
                    }
                }

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

        #region Search_Debtor
        public DataTable Search_Debtor(String mFilter, String mOrder)
        {
            String mFunctionName = "Search_Debtor";

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
                string mCommandText = "select * from billdebtors";
                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }
                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("billdebtors");
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

        #region View_TableFields
        public DataTable View_TableFields(String mTableName)
        {
            String mFunctionName = "View_TableFields";

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
                DataTable mDataTable = new DataTable("tablefields");
                mDataTable.RemotingFormat = SerializationFormat.Binary;

                string mCommandText = "";

                switch (mTableName.Trim().ToLower())
                {
                    case "patients":
                        {
                            //columns from patients
                            string mPatientColumns = clsGlobal.Get_TableColumns(mCommand, "patients", "", "p", "");
                            mPatientColumns = mPatientColumns + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "fullname");
                            mPatientColumns = mPatientColumns + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "now()", ""), "age");

                            mCommandText = ""
                                + "SELECT "
                                + mPatientColumns + " "
                                + "FROM patients AS p WHERE 1=2";
                        }
                        break;
                    case "rch_patients":
                        {
                            //columns from patients
                            string mPatientColumns = clsGlobal.Get_TableColumns(mCommand, "patients", "", "p", "");
                            mPatientColumns = mPatientColumns + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "fullname");
                            mPatientColumns = mPatientColumns + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "now()", ""), "age");

                            mCommandText = ""
                                + "SELECT "
                                + mPatientColumns + " "
                                + "FROM patients AS p WHERE 1=2";
                        }
                        break;
                    case "ctc_patients":
                        {
                            //columns from patients
                            string mPatientColumns = clsGlobal.Get_TableColumns(mCommand, "patients", "", "p", "");
                            mPatientColumns = mPatientColumns + "," + clsGlobal.Concat_Fields("p.firstname,' ',p.othernames,' ',p.surname", "fullname");
                            mPatientColumns = mPatientColumns + "," + clsGlobal.Age_Display(clsGlobal.Age_Formula("p.birthdate", "now()", ""), "age");
                            //columns from ctc_patients
                            string mCTCColumns = clsGlobal.Get_TableColumns(mCommand, "ctc_patients", "", "b", "ctc");

                            mCommandText = ""
                                + "SELECT "
                                + mCTCColumns + ","
                                + mPatientColumns + " "
                                + "FROM ctc_patients AS b "
                                + "LEFT OUTER JOIN patients AS p ON b.patientcode = p.code WHERE 1=2";
                        }
                        break;
                    default:
                        {
                            mCommandText = "select * from " + mTableName.Trim() + " where 1=2";
                        }
                        break;
                }


                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.FillSchema(mDataTable, SchemaType.Source);

                //columnheaders
                clsGlobal.Scan_ColumnsForLanguage(mCommand, mDataTable, "patients");

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

        #region View_SearchFields
        public DataTable View_SearchFields(String mTableName, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_SearchFields";

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
                DataTable mDataTable = new DataTable("sys_searchfields");
                mDataTable.RemotingFormat = SerializationFormat.Binary;

                mCommand.CommandText = "select * from sys_searchfields where tablename='" + mTableName.Trim() + "'";
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

        #region Save_Fields
        public AfyaPro_Types.clsResult Save_Fields(String mTableName, DataTable mDtFields, string mUserId)
        {
            String mFunctionName = "Save_Fields";

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

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.gensearchengine_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region do save
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //execute statements
                mCommand.CommandText = "delete from sys_searchfields where tablename='" + mTableName.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtFields.Rows)
                {
                    mCommand.CommandText = "insert into sys_searchfields(tablename,fieldname,"
                    + "fielddisplayname,defaultfield) values('" + mTableName.Trim() + "','" + mDataRow["fieldname"].ToString().Trim()
                    + "','" + mDataRow["fielddisplayname"].ToString().Trim() + "'," + Convert.ToInt16(mDataRow["defaultfield"]) + ")";
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
    }
}
