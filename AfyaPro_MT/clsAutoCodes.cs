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
using System.Diagnostics;
using System.Linq;

namespace AfyaPro_MT
{
    /// <summary>
    /// <c><b>clsAutoCodes</b></c> class deals with management of system generated codes.
    /// This class is remoted, you can use its methods by creating a proxy class
    /// </summary>
    public class clsAutoCodes : System.MarshalByRefObject
    {
        #region declaration

        private String pClassName = "AfyaPro_MT.clsAutoCodes";

        #endregion

        #region Get_IdPositions
        public DataTable Get_IdPositions(string mLanguageName, string mGridName)
        {
            String mFunctionName = "Get_IdPositions";

            try
            {
                DataTable mDataTable = new DataTable("idpositions");
                mDataTable.Columns.Add("code", typeof(System.String));
                mDataTable.Columns.Add("description", typeof(System.String));
                mDataTable.RemotingFormat = SerializationFormat.Binary;

                DataRow mNewRow;

                #region load idpositions

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "idposition" + (int)AfyaPro_Types.clsEnums.IdPositions.IdLeft;
                mNewRow["description"] = "Left";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "idposition" + (int)AfyaPro_Types.clsEnums.IdPositions.IdCenter;
                mNewRow["description"] = "Center";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "idposition" + (int)AfyaPro_Types.clsEnums.IdPositions.IdRight;
                mNewRow["description"] = "Right";
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

                            if (mDataTable.Columns.Contains((string)mElement.Element("controlname").Value.Trim().Substring(3)) == true)
                            {
                                mDataTable.Columns[(string)mElement.Element("controlname").Value.Trim().Substring(3)].Caption =
                                    (string)mElement.Element("description").Value.Trim();
                            }
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

        #region Get_CodeTypes
        public DataTable Get_CodeTypes(string mLanguageName, string mGridName)
        {
            String mFunctionName = "Get_CodeTypes";

            try
            {
                DataTable mDataTable = new DataTable("codetypes");
                mDataTable.Columns.Add("code", typeof(System.String));
                mDataTable.Columns.Add("description", typeof(System.String));
                mDataTable.Columns.Add("tablename", typeof(System.String));
                mDataTable.Columns.Add("fieldname", typeof(System.String));
                mDataTable.RemotingFormat = SerializationFormat.Binary;

                DataRow mNewRow;

                #region load codetypes

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.patientno;
                mNewRow["description"] = "Patient Numbers";
                mNewRow["tablename"] = "patients";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.rchcustomerno;
                mNewRow["description"] = "Customer Numbers";
                mNewRow["tablename"] = "";
                mNewRow["fieldname"] = "";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.receiptno;
                mNewRow["description"] = "Receipt Numbers";
                mNewRow["tablename"] = "";
                mNewRow["fieldname"] = "";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.invoiceno;
                mNewRow["description"] = "Invoice Numbers";
                mNewRow["tablename"] = "";
                mNewRow["fieldname"] = "";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.surgeryno;
                mNewRow["description"] = "Surgery Numbers";
                mNewRow["tablename"] = "";
                mNewRow["fieldname"] = "";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.deposittransactionid;
                mNewRow["description"] = "Deposit Transaction Id";
                mNewRow["tablename"] = "";
                mNewRow["fieldname"] = "";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.deposittransferno;
                mNewRow["description"] = "Deposit Transfer Numbers";
                mNewRow["tablename"] = "";
                mNewRow["fieldname"] = "";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.pymtno;
                mNewRow["description"] = "Payment Numbers";
                mNewRow["tablename"] = "";
                mNewRow["fieldname"] = "";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.crnoteno;
                mNewRow["description"] = "Credit Note Numbers";
                mNewRow["tablename"] = "";
                mNewRow["fieldname"] = "";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.pycanno;
                mNewRow["description"] = "Payment Cancellation Numbers";
                mNewRow["tablename"] = "";
                mNewRow["fieldname"] = "";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.slcanno;
                mNewRow["description"] = "Sale Cancellation Numbers";
                mNewRow["tablename"] = "";
                mNewRow["fieldname"] = "";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.usergroupcode;
                mNewRow["description"] = "User Group Codes";
                mNewRow["tablename"] = "sys_usergroups";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.countrycode;
                mNewRow["description"] = "Country Codes";
                mNewRow["tablename"] = "countries";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.regioncode;
                mNewRow["description"] = "Regional Codes";
                mNewRow["tablename"] = "regions";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.districtcode;
                mNewRow["description"] = "District Codes";
                mNewRow["tablename"] = "districts";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.staffcode;
                mNewRow["description"] = "Staff Codes";
                mNewRow["tablename"] = "facilitystaffs";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.treatmentpointcode;
                mNewRow["description"] = "Treatment Point Codes";
                mNewRow["tablename"] = "facilitytreatmentpoints";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.currencycode;
                mNewRow["description"] = "Currency Codes";
                mNewRow["tablename"] = "facilitycurrencies";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.paymenttypecode;
                mNewRow["description"] = "Payment Type Codes";
                mNewRow["tablename"] = "facilitypaymenttypes";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.clientgroupcode;
                mNewRow["description"] = "Client Group Codes";
                mNewRow["tablename"] = "facilitycorporates";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.billingitemgroupcode;
                mNewRow["description"] = "Billing Group Codes";
                mNewRow["tablename"] = "facilitybillinggroups";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.billingitemsubgroupcode;
                mNewRow["description"] = "Billing Sub Group Codes";
                mNewRow["tablename"] = "facilitybillingsubgroups";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.billingitemcode;
                mNewRow["description"] = "Billing Item Codes";
                mNewRow["tablename"] = "facilitybillingitems";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.clientsubgroupcode;
                mNewRow["description"] = "Client Sub Group Codes";
                mNewRow["tablename"] = "facilitycorporatesubgroups";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.ipdwardcode;
                mNewRow["description"] = "IPD Ward Codes";
                mNewRow["tablename"] = "facilitywards";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.ipdroomcode;
                mNewRow["description"] = "IPD Room Codes";
                mNewRow["tablename"] = "facilitywardrooms";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.ipddischargestatus;
                mNewRow["description"] = "IPD Discharge Status Codes";
                mNewRow["tablename"] = "facilitydischargestatus";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.productcode;
                mNewRow["description"] = "Inventory Product Codes";
                mNewRow["tablename"] = "som_products";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.suppliercode;
                mNewRow["description"] = "Inventory Supplier Codes";
                mNewRow["tablename"] = "som_suppliers";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.productcategorycode;
                mNewRow["description"] = "Inventory Product Category Codes";
                mNewRow["tablename"] = "som_productcategories";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.packagingcode;
                mNewRow["description"] = "Inventory Packaging Codes";
                mNewRow["tablename"] = "som_packagings";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.customerid;
                mNewRow["description"] = "Inventory Customer Codes";
                mNewRow["tablename"] = "patients";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.clientgroupmembercode;
                mNewRow["description"] = "Group Member Code";
                mNewRow["tablename"] = "facilitycorporatemembers";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.clientgroupmembershipId;
                mNewRow["description"] = "Group Membership Id";
                mNewRow["tablename"] = "facilitycorporatemembers";
                mNewRow["fieldname"] = "billinggroupmembershipno";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.paymentno;
                mNewRow["description"] = "Invoice Payment Receipt Numbers";
                mNewRow["tablename"] = "billinvoicepayments";
                mNewRow["fieldname"] = "receiptno";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.patientdocumentcode;
                mNewRow["description"] = "Patient Document Codes";
                mNewRow["tablename"] = "facilitypatientdocuments";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.reportgroupcode;
                mNewRow["description"] = "Report Group Codes";
                mNewRow["tablename"] = "sys_reportgroups";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.reportcode;
                mNewRow["description"] = "Reports";
                mNewRow["tablename"] = "sys_reports";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.depositaccountcode;
                mNewRow["description"] = "Account deposit numbers";
                mNewRow["tablename"] = "billaccounts";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                //mNewRow = mDataTable.NewRow();
                //mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.refundno;
                //mNewRow["description"] = "Refund Ref #";
                //mNewRow["tablename"] = "";
                //mNewRow["fieldname"] = "refundno";
                //mDataTable.Rows.Add(mNewRow);
                //mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.dxincidencekey;
                mNewRow["description"] = "Diagnosis Episode #";
                mNewRow["tablename"] = "dxtpatientdiagnoses";
                mNewRow["fieldname"] = "episodecode";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.patientreferalcodes;
                mNewRow["description"] = "Patient Referal #";
                mNewRow["tablename"] = "dxtpatientreferals";
                mNewRow["fieldname"] = "referalno";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.dxgroupcode;
                mNewRow["description"] = "Diagnoses Group Code";
                mNewRow["tablename"] = "dxtdiagnosesgroups";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.orderno;
                mNewRow["description"] = "Purchase Order #";
                mNewRow["tablename"] = "som_orders";
                mNewRow["fieldname"] = "orderno";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.deliveryno;
                mNewRow["description"] = "Delivery Numbers";
                mNewRow["tablename"] = "som_transferoutissues";
                mNewRow["fieldname"] = "deliveryno";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.transferinno;
                mNewRow["description"] = "Transfer In #";
                mNewRow["tablename"] = "som_transferins";
                mNewRow["fieldname"] = "transferno";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.transferoutno;
                mNewRow["description"] = "Transfer Out #";
                mNewRow["tablename"] = "som_transferouts";
                mNewRow["fieldname"] = "transferno";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.stocktakingno;
                mNewRow["description"] = "Physical Inventory Ref #";
                mNewRow["tablename"] = "som_physicalinventory";
                mNewRow["fieldname"] = "referenceno";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.laboritoryno;
                mNewRow["description"] = "Lab Number";
                mNewRow["tablename"] = "facilitylaboratories";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.labtestgroupcode;
                mNewRow["description"] = "Lab Test Group Code";
                mNewRow["tablename"] = "facilitylaboratorytestgroups";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.labtestcode;
                mNewRow["description"] = "Lab Test Code";
                mNewRow["tablename"] = "facilitylaboratorytests";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.wardroombedcode;
                mNewRow["description"] = "Ward room bed code";
                mNewRow["tablename"] = "facilitywardroombeds";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.labtestsubgroupcode;
                mNewRow["description"] = "Lab test sub group code";
                mNewRow["tablename"] = "labtestsubgroups";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.debtreliefrequestno;
                mNewRow["description"] = "Debt relief request #";
                mNewRow["tablename"] = "billdebtreliefrequests";
                mNewRow["fieldname"] = "requestno";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.bookingno;
                mNewRow["description"] = "Patient booking #";
                mNewRow["tablename"] = "facilitybookinglog";
                mNewRow["fieldname"] = "booking";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.indicatorgroups;
                mNewRow["description"] = "Diagnoses Indicator Groups";
                mNewRow["tablename"] = "dxtindicatorgroups";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.indicators;
                mNewRow["description"] = "Diagnoses Indicators";
                mNewRow["tablename"] = "dxtindicators";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.vctnumbers;
                mNewRow["description"] = "HTC-ART Client Numbers";
                mNewRow["tablename"] = "patients";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcbookingno;
                mNewRow["description"] = "CTC Booking";
                mNewRow["tablename"] = "ctc_bookinglog";
                mNewRow["fieldname"] = "booking";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.hivnumbers;
                mNewRow["description"] = "HIV Numbers";
                mNewRow["tablename"] = "ctc_patients";
                mNewRow["fieldname"] = "hivno";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcnumbers;
                mNewRow["description"] = "CTC Numbers";
                mNewRow["tablename"] = "dxtindicators";
                mNewRow["fieldname"] = "code";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcsampleid;
                mNewRow["description"] = "CD4 Sample Id";
                mNewRow["tablename"] = "ctc_cd4tests";
                mNewRow["fieldname"] = "sampleid";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctchivtestnumber;
                mNewRow["description"] = "HIV Test Serial #";
                mNewRow["tablename"] = "ctc_patients";
                mNewRow["fieldname"] = "hivtestno";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "codetype" + (int)AfyaPro_Types.clsEnums.SystemGeneratedCodes.ctcarvnumber;
                mNewRow["description"] = "ARV #";
                mNewRow["tablename"] = "ctc_patients";
                mNewRow["fieldname"] = "arvno";
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
        /// <summary>
        /// Used to retrieve a list of pre-defined codes that can be generated.
        /// </summary>
        /// <param name="mFilter">
        /// A <see cref="String"/> type representing SQL filter clause (without Where keyword) to append to select statement
        /// </param>
        /// <param name="mOrder">
        /// A <see cref="String"/> representing the field name for sorting the results. Multiple field names can be separated by comas.
        /// </param>
        /// <returns>
        /// A <see cref="DataTable"/> type holding a list of pre-defined codes
        /// </returns>
        public DataTable View(String mFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View";

            #region get code types

            DataTable mDtCodeTypes = this.Get_CodeTypes(mLanguageName, mGridName);

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
                string mCommandText = "select * from facilityautocodes";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("facilityautocodes");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                #region add codetype description info

                mDataTable.Columns.Add("codedescription", typeof(System.String));

                DataView mDvCodeTypes = new DataView();
                mDvCodeTypes.Table = mDtCodeTypes;
                mDvCodeTypes.Sort = "code";

                foreach (DataRow mDataRow in mDataTable.Rows)
                {
                    string mCodeDescription = "";

                    int mRowIndex = mDvCodeTypes.Find("codetype" + mDataRow["codekey"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mCodeDescription = mDvCodeTypes[mRowIndex]["description"].ToString().Trim();
                    }

                    mDataRow.BeginEdit();
                    mDataRow["codedescription"] = mCodeDescription;
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

        #region Save
        /// <summary>
        /// Saves the settings for the system generated code
        /// </summary>
        /// <param name="mCodeKey">
        /// A <see cref="Int16"/> that uniquely identifies a pre-defined code
        /// See <see cref="AfyaPro_Types.clsEnums.SystemGeneratedCodes"/> for a list of possible values to pass
        /// </param>
        /// <param name="mCodeStatus">A <see cref="Int16"/> flag to tell whether auto-generation feature should be on or off - 0 stands for Off and 1 for On</param>
        /// <param name="mPrefType">A <see cref="Int16"/> indicating the type of prefix. 0 for Manual and 1 for date based</param>
        /// <param name="mPrefixText">A <see cref="String"/> to be used as the prefix</param>
        /// <param name="mPrefixSep">A <see cref="String"/> to be used as the separator just after the prefix</param>
        /// <param name="mSurfixType">A <see cref="Int16"/> indicating the type of surfix. 0 for Manual and 1 for date based</param>
        /// <param name="mSurfixText">A <see cref="String"/> to be used as the surfix</param>
        /// <param name="mSurfixSep">A <see cref="String"/> to be used as the separator just after the surfix</param>
        /// <param name="mIdSeed">A <see cref="Int32"/> used as a starting point for the generator</param>
        /// <param name="mIdLength">A <see cref="Int32"/> to be used as the length (number of character) occupied by the incrementing part of the code</param>
        /// <param name="mIdCurrent">A <see cref="Int23"/> indicating the next incremented number</param>
        /// <param name="mIdIncrement">A <see cref="Int32"/> used as the incrementing number</param>
        /// <param name="mIdPosition">A <see cref="Int16"/> indicating the position for the incrementing part of the code in relation to prefix and surfix. 0 for Left, 1 for Center and 2 for Right</param>
        /// <returns>A <see cref="AfyaPro_Types.clsResult"/> type</returns>
        public AfyaPro_Types.clsResult Save(Int16 mCodeKey, Int16 mCodeStatus, Int16 mPrefType, String mPrefixText, 
            String mPrefixSep, Int16 mSurfixType, String mSurfixText, String mSurfixSep, Int32 mIdSeed, Int32 mIdLength,
            Int32 mIdCurrent, Int32 mIdIncrement, Int16 mIdPosition, string mUserId)
        {
            String mFunctionName = "Save";
            
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.genautocodes_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                mCommand.CommandText = "update facilityautocodes set autogenerate=" + mCodeStatus
                + ",preftype=" + mPrefType + ",preftext='" + mPrefixText + "',prefsep='" + mPrefixSep + "',surftype=" 
                + mSurfixType + ",surftext='" + mSurfixText  + "',surfsep='" + mSurfixSep + "',idseed=" + mIdSeed 
                + ",idlength=" + mIdLength + ",idcurrent=" + mIdCurrent + ",idincrement=" + mIdIncrement + ",position="
                + mIdPosition + " where codekey=" + mCodeKey;
                mCommand.ExecuteNonQuery();

                mTrans.Commit();

                return mResult;
            }
            catch (Exception ex)
            {
                try
                {
                    mTrans.Rollback();
                }
                catch
                {
                }
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

        #region Auto_Generate_Code
        public Int16 Auto_Generate_Code(Int16 mCodeKey)
        {
            String mFunctionName = "Auto_Generate_Code";

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
                return -1;
            }

            #endregion

            try
            {
                DataTable mDataTable = new DataTable("facilityautocodes");
                mCommand.CommandText = "select * from facilityautocodes"
                + " where codekey=" + mCodeKey;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                if (mDataTable.Rows.Count > 0)
                {
                    if (Convert.ToInt16(mDataTable.Rows[0]["autogenerate"]) == 1)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return -1;
            }
            finally
            {
                mConn.Close();
            }
        }
        #endregion

        #region Next_Code
        public AfyaPro_Types.clsCode Next_Code(Int16 mCodeKey, String mTableName, String mFieldName)
        {
            string mFunctionName = "Next_Code";

            String mId = "";
            String mGeneratedCode = "";

            String mPrefixText = "";
            String mPrefixSep = "";
            String mSurfixText = "";
            String mSurfixSep = "";
            Int16 mIdPosition = 0;

            Int32 mIdSeed = 1;
            Int32 mIdLength = 6;
            Int32 mIdCurrent = 1;
            Int32 mIdIncrement = 1;

            AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
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
                mObjCode.Exe_Result = -1;
                mObjCode.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mObjCode;
            }

            #endregion

            try
            {
                Boolean mBreak = false;
                while (mBreak == false)
                {
                    mGeneratedCode = "";
                    DataTable mDtAutoCodes = new DataTable("facilityautocodes");
                    mCommand.CommandText = "select * from facilityautocodes where codekey=" + mCodeKey;
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAutoCodes);

                    if (mDtAutoCodes.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(mDtAutoCodes.Rows[0]["idcurrent"]) ==
                            Convert.ToInt32("".PadRight(Convert.ToInt32(mDtAutoCodes.Rows[0]["idlength"]), '9')))
                        {
                            mCommand.CommandText = "update facilityautocodes set "
                            + "idlength=idlength+1 where codekey=" + mCodeKey;
                            mCommand.ExecuteNonQuery();
                        }
                    }

                    if (mDtAutoCodes.Rows.Count > 0)
                    {
                        //prefix text
                        if (Convert.ToInt16(mDtAutoCodes.Rows[0]["preftype"]) == 0)
                        {
                            mPrefixText = DateTime.Now.ToString(mDtAutoCodes.Rows[0]["preftext"].ToString());
                        }
                        else
                        {
                            mPrefixText = mDtAutoCodes.Rows[0]["preftext"].ToString();
                        }

                        //surfix text
                        if (Convert.ToInt16(mDtAutoCodes.Rows[0]["surftype"]) == 0)
                        {
                            mSurfixText = DateTime.Now.ToString(mDtAutoCodes.Rows[0]["surftext"].ToString());
                        }
                        else
                        {
                            mSurfixText = mDtAutoCodes.Rows[0]["surftext"].ToString();
                        }

                        mPrefixSep = mDtAutoCodes.Rows[0]["prefsep"].ToString();
                        mSurfixSep = mDtAutoCodes.Rows[0]["surfsep"].ToString();
                        mIdSeed = Convert.ToInt32(mDtAutoCodes.Rows[0]["idseed"]);
                        mIdLength = Convert.ToInt32(mDtAutoCodes.Rows[0]["idlength"]);
                        mIdCurrent = Convert.ToInt32(mDtAutoCodes.Rows[0]["idcurrent"]);
                        mIdIncrement = Convert.ToInt32(mDtAutoCodes.Rows[0]["idincrement"]);
                        mIdPosition = Convert.ToInt16(mDtAutoCodes.Rows[0]["position"]);

                        if (mIdLength >= mIdCurrent.ToString().Length)
                        {
                            if (mIdSeed > mIdCurrent)
                            {
                                mIdCurrent = mIdSeed;
                            }
                            mId = mIdCurrent.ToString().PadLeft(mIdLength, '0');
                        }

                        switch (mIdPosition)
                        {
                            case 0:
                                {
                                    if (mPrefixText.Trim() != "")
                                    {
                                        mGeneratedCode = mPrefixSep + mPrefixText;
                                    }
                                    if (mSurfixText.Trim() != "")
                                    {
                                        mGeneratedCode = mGeneratedCode + mSurfixSep + mSurfixText;
                                    }
                                    mGeneratedCode = mId + mGeneratedCode;
                                }
                                break;
                            case 1:
                                {
                                    if (mPrefixText.Trim() != "")
                                    {
                                        mGeneratedCode = mPrefixText + mPrefixSep;
                                    }
                                    mGeneratedCode = mGeneratedCode + mId;
                                    if (mSurfixText.Trim() != "")
                                    {
                                        mGeneratedCode = mGeneratedCode + mSurfixSep + mSurfixText;
                                    }
                                }
                                break;
                            default:
                                {
                                    if (mPrefixText.Trim() != "")
                                    {
                                        mGeneratedCode = mPrefixText + mPrefixSep;
                                    }
                                    if (mSurfixText.Trim() != "")
                                    {
                                        mGeneratedCode = mGeneratedCode + mSurfixText + mSurfixSep;
                                    }
                                    mGeneratedCode = mGeneratedCode + mId;
                                }
                                break;
                        }
                    }

                    mCommand.CommandText = "select " + mFieldName + " from " + mTableName + " where "
                    + mFieldName + "='" + mGeneratedCode + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    DataTable mDtCodes = new DataTable("usedcodes");
                    mDataAdapter.Fill(mDtCodes);

                    if (mDtCodes.Rows.Count > 0)
                    {
                        mCommand.CommandText = "update facilityautocodes set "
                        + "idcurrent=idcurrent+idincrement where codekey=" + mCodeKey;
                        mCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        mBreak = true;
                        break;
                    }
                }

                mObjCode.GeneratedCode = mGeneratedCode;
                return mObjCode;
            }
            catch (Exception ex)
            {
                mObjCode.Exe_Result = -1;
                mObjCode.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mObjCode;
            }
            finally
            {
                mConn.Close();
            }
        }
        #endregion

        #region Next_Code
        public AfyaPro_Types.clsCode Next_Code(OdbcCommand mCommand, 
            Int16 mCodeKey, String mTableName, String mFieldName)
        {
            string mFunctionName = "Next_Code";

            String mId = "";
            String mGeneratedCode = "";

            String mPrefixText = "";
            String mPrefixSep = "";
            String mSurfixText = "";
            String mSurfixSep = "";
            Int16 mIdPosition = 0;

            Int32 mIdSeed = 1;
            Int32 mIdLength = 6;
            Int32 mIdCurrent = 1;
            Int32 mIdIncrement = 1;

            AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            try
            {
                Boolean mBreak = false;
                while (mBreak == false)
                {
                    mGeneratedCode = "";
                    DataTable mDtAutoCodes = new DataTable("facilityautocodes");
                    mCommand.CommandText = "select * from facilityautocodes where codekey=" + mCodeKey;
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtAutoCodes);

                    if (mDtAutoCodes.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(mDtAutoCodes.Rows[0]["idcurrent"]) ==
                            Convert.ToInt32("".PadRight(Convert.ToInt32(mDtAutoCodes.Rows[0]["idlength"]), '9')))
                        {
                            mCommand.CommandText = "update facilityautocodes set "
                            + "idlength=idlength+1 where codekey=" + mCodeKey;
                            mCommand.ExecuteNonQuery();
                        }
                    }

                    if (mDtAutoCodes.Rows.Count > 0)
                    {
                        //prefix text
                        if (Convert.ToInt16(mDtAutoCodes.Rows[0]["preftype"]) == 0)
                        {
                            mPrefixText = DateTime.Now.ToString(mDtAutoCodes.Rows[0]["preftext"].ToString());
                        }
                        else
                        {
                            mPrefixText = mDtAutoCodes.Rows[0]["preftext"].ToString();
                        }

                        //surfix text
                        if (Convert.ToInt16(mDtAutoCodes.Rows[0]["surftype"]) == 0)
                        {
                            mSurfixText = DateTime.Now.ToString(mDtAutoCodes.Rows[0]["surftext"].ToString());
                        }
                        else
                        {
                            mSurfixText = mDtAutoCodes.Rows[0]["surftext"].ToString();
                        }

                        mPrefixSep = mDtAutoCodes.Rows[0]["prefsep"].ToString();
                        mSurfixSep = mDtAutoCodes.Rows[0]["surfsep"].ToString();
                        mIdSeed = Convert.ToInt32(mDtAutoCodes.Rows[0]["idseed"].ToString());
                        mIdLength = Convert.ToInt32(mDtAutoCodes.Rows[0]["idlength"].ToString());
                        mIdCurrent = Convert.ToInt32(mDtAutoCodes.Rows[0]["idcurrent"].ToString());
                        mIdIncrement = Convert.ToInt32(mDtAutoCodes.Rows[0]["idincrement"].ToString());
                        mIdPosition = Convert.ToInt16(mDtAutoCodes.Rows[0]["position"]);

                        if (mIdLength >= mIdCurrent.ToString().Length)
                        {
                            if (mIdSeed > mIdCurrent)
                            {
                                mIdCurrent = mIdSeed;
                            }
                            mId = mIdCurrent.ToString().PadLeft(mIdLength, '0');
                        }

                        switch (mIdPosition)
                        {
                            case 0:
                                {
                                    if (mPrefixText.Trim() != "")
                                    {
                                        mGeneratedCode = mPrefixSep + mPrefixText;
                                    }
                                    if (mSurfixText.Trim() != "")
                                    {
                                        mGeneratedCode = mGeneratedCode + mSurfixSep + mSurfixText;
                                    }
                                    mGeneratedCode = mId + mGeneratedCode;
                                }
                                break;
                            case 1:
                                {
                                    if (mPrefixText.Trim() != "")
                                    {
                                        mGeneratedCode = mPrefixText + mPrefixSep;
                                    }
                                    mGeneratedCode = mGeneratedCode + mId;
                                    if (mSurfixText.Trim() != "")
                                    {
                                        mGeneratedCode = mGeneratedCode + mSurfixSep + mSurfixText;
                                    }
                                }
                                break;
                            default:
                                {
                                    if (mPrefixText.Trim() != "")
                                    {
                                        mGeneratedCode = mPrefixText + mPrefixSep;
                                    }
                                    if (mSurfixText.Trim() != "")
                                    {
                                        mGeneratedCode = mGeneratedCode + mSurfixText + mSurfixSep;
                                    }
                                    mGeneratedCode = mGeneratedCode + mId;
                                }
                                break;
                        }
                    }

                    mCommand.CommandText = "select " + mFieldName + " from " + mTableName + " where "
                    + mFieldName + "='" + mGeneratedCode + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    DataTable mDtCodes = new DataTable("usedcodes");
                    mDataAdapter.Fill(mDtCodes);

                    if (mDtCodes.Rows.Count > 0)
                    {
                        mCommand.CommandText = "update facilityautocodes set "
                        + "idcurrent=idcurrent+idincrement where codekey=" + mCodeKey;
                        mCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        mBreak = true;
                        break;
                    }
                }

                mObjCode.GeneratedCode = mGeneratedCode;
                return mObjCode;
            }
            catch (Exception ex)
            {
                mObjCode.Exe_Result = -1;
                mObjCode.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mObjCode;
            }
        }
        #endregion

        #region Sample_Code
        public String Sample_Code(Int16 mCodeKey)
        {
            String mFunctionName = "Sample_Code";

            String mId = "";
            String mGeneratedCode = "";

            String mPrefixText = "";
            String mPrefixSep = "";
            String mSurfixText = "";
            String mSurfixSep = "";
            Int16 mIdPosition = 0;

            Int32 mIdSeed = 1;
            Int32 mIdLength = 6;
            Int32 mIdCurrent = 1;
            Int32 mIdIncrement = 1;

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
                return "";
            }

            #endregion

            try
            {
                mGeneratedCode = "";
                DataTable mDtAutoCodes = new DataTable("facilityautocodes");
                mCommand.CommandText = "select * from facilityautocodes where codekey=" + mCodeKey;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtAutoCodes);

                if (mDtAutoCodes.Rows.Count > 0)
                {
                    //prefix text
                    if (Convert.ToInt16(mDtAutoCodes.Rows[0]["preftype"]) == 0)
                    {
                        mPrefixText = DateTime.Now.ToString(mDtAutoCodes.Rows[0]["preftext"].ToString());
                    }
                    else
                    {
                        mPrefixText = mDtAutoCodes.Rows[0]["preftext"].ToString();
                    }

                    //surfix text
                    if (Convert.ToInt16(mDtAutoCodes.Rows[0]["surftype"]) == 0)
                    {
                        mSurfixText = DateTime.Now.ToString(mDtAutoCodes.Rows[0]["surftext"].ToString());
                    }
                    else
                    {
                        mSurfixText = mDtAutoCodes.Rows[0]["surftext"].ToString();
                    }

                    mPrefixSep = mDtAutoCodes.Rows[0]["prefsep"].ToString();
                    mSurfixSep = mDtAutoCodes.Rows[0]["surfsep"].ToString();
                    mIdSeed = Convert.ToInt32(mDtAutoCodes.Rows[0]["idseed"].ToString());
                    mIdLength = Convert.ToInt32(mDtAutoCodes.Rows[0]["idlength"].ToString());
                    mIdCurrent = Convert.ToInt32(mDtAutoCodes.Rows[0]["idcurrent"].ToString());
                    mIdIncrement = Convert.ToInt32(mDtAutoCodes.Rows[0]["idincrement"].ToString());
                    mIdPosition = Convert.ToInt16(mDtAutoCodes.Rows[0]["position"]);

                    if (mIdLength >= mIdCurrent.ToString().Length)
                    {
                        if (mIdSeed > mIdCurrent)
                        {
                            mIdCurrent = mIdSeed;
                        }
                        mId = mIdCurrent.ToString().PadLeft(mIdLength, '0');
                    }

                    switch (mIdPosition)
                    {
                        case 0:
                            {
                                if (mPrefixText.Trim() != "")
                                {
                                    mGeneratedCode = mPrefixSep + mPrefixText;
                                }
                                if (mSurfixText.Trim() != "")
                                {
                                    mGeneratedCode = mGeneratedCode + mSurfixSep + mSurfixText;
                                }
                                mGeneratedCode = mId + mGeneratedCode;
                            }
                            break;
                        case 1:
                            {
                                if (mPrefixText.Trim() != "")
                                {
                                    mGeneratedCode = mPrefixText + mPrefixSep;
                                }
                                mGeneratedCode = mGeneratedCode + mId;
                                if (mSurfixText.Trim() != "")
                                {
                                    mGeneratedCode = mGeneratedCode + mSurfixSep + mSurfixText;
                                }
                            }
                            break;
                        default:
                            {
                                if (mPrefixText.Trim() != "")
                                {
                                    mGeneratedCode = mPrefixText + mPrefixSep;
                                }
                                if (mSurfixText.Trim() != "")
                                {
                                    mGeneratedCode = mGeneratedCode + mSurfixText + mSurfixSep;
                                }
                                mGeneratedCode = mGeneratedCode + mId;
                            }
                            break;
                    }
                }

                return mGeneratedCode;
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
    }
}
