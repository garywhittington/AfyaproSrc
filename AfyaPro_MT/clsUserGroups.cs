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
    public class clsUserGroups : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsUserGroups";

        #endregion

        #region View
        public DataTable View(String mFilter, String mOrder, string mLanguageName, string mGridName)
        {
            string mFunctionName = "View";

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
                string mCommandText = "select * from sys_usergroups";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("sys_usergroups");
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

        #region Add
        public AfyaPro_Types.clsResult Add(Int16 mGenerateCode, String mCode, String mDescription, 
            string mFormLayoutTemplateName, string mStaffCategory, Int16 mSynchronizeWithStaffs, string mUserId)
        {
            string mFunctionName = "Add";

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.secusergroups_add.ToString(), mUserId);
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
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.usergroupcode), "sys_usergroups", "code");
                if (mObjCode.Exe_Result == -1)
                {
                    mResult.Exe_Result = mObjCode.Exe_Result;
                    mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                    return mResult;
                }
                mCode = mObjCode.GeneratedCode;
            }

            #endregion

            #region check for duplicate
            try
            {
                mCommand.CommandText = "select * from sys_usergroups where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.SEC_UserGroupCodeIsInUse.ToString();
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

                //execute statements
                mCommand.CommandText = "insert into sys_usergroups(code,description,formlayouttemplatename,"
                    + "staffcategorycode,synchronizewithstaffs) values('"
                    + mCode.Trim() + "','" + mDescription.Trim() + "','" + mFormLayoutTemplateName.Trim() 
                    + "','" + mStaffCategory + "'," + mSynchronizeWithStaffs + ")";
                mCommand.ExecuteNonQuery();

                if (mGenerateCode == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.usergroupcode);
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
        public AfyaPro_Types.clsResult Edit(String mCode, String mDescription,
            string mFormLayoutTemplateName, string mStaffCategory, Int16 mSynchronizeWithStaffs, string mUserId)
        {
            String mFunctionName = "Edit";

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.secusergroups_edit.ToString(), mUserId);
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
                mCommand.CommandText = "select * from sys_usergroups where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.SEC_UserGroupCodeDoesNotExist.ToString();
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

                mCommand.CommandText = "update sys_usergroups set description='"
                    + mDescription.Trim() + "',formlayouttemplatename='" + mFormLayoutTemplateName
                    + "',staffcategorycode='" + mStaffCategory + "',synchronizewithstaffs="
                    + mSynchronizeWithStaffs + " where code='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                mTrans.Commit();

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.secusergroups_delete.ToString(), mUserId);
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
                mCommand.CommandText = "select * from sys_usergroups where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.SEC_UserGroupCodeDoesNotExist.ToString();
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

            #region check if group is in use
            try
            {
                mCommand.CommandText = "select * from sys_users where usergroupcode='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.SEC_UserGroupCodeIsInUse.ToString();
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

                mCommand.CommandText = "delete from sys_usergroups where code='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                mTrans.Commit();

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

        #region Get_Modules
        public DataTable Get_Modules(string mLanguageName, string mGridName)
        {
            String mFunctionName = "Get_Modules";

            try
            {
                DataTable mDtModules = new DataTable("modules");
                mDtModules.Columns.Add("modulekey", typeof(System.String));
                mDtModules.Columns.Add("moduletext", typeof(System.String));
                mDtModules.Columns.Add("iconindex", typeof(System.Int16));
                mDtModules.Columns.Add("activationkey", typeof(System.String));

                DataRow mNewRow;
                Int16 mIconIndex = 0;

                #region modules

                //outpatientdept
                mNewRow = mDtModules.NewRow();
                mNewRow["modulekey"] = "outpatientdept";
                mNewRow["moduletext"] = "Outpatient Dept";
                mNewRow["activationkey"] = "opd";
                mNewRow["iconindex"] = mIconIndex;
                mDtModules.Rows.Add(mNewRow);
                mDtModules.AcceptChanges();
                mIconIndex++;

                //inpatientdept
                mNewRow = mDtModules.NewRow();
                mNewRow["modulekey"] = "inpatientdept";
                mNewRow["moduletext"] = "Inpatient Dept";
                mNewRow["activationkey"] = "ipd";
                mNewRow["iconindex"] = mIconIndex;
                mDtModules.Rows.Add(mNewRow);
                mDtModules.AcceptChanges();
                mIconIndex++;

                //diagnosesandtreatments
                mNewRow = mDtModules.NewRow();
                mNewRow["modulekey"] = "diagnosesandtreatments";
                mNewRow["moduletext"] = "Diagnoses and Treatments";
                mNewRow["activationkey"] = "dxt";
                mNewRow["iconindex"] = mIconIndex;
                mDtModules.Rows.Add(mNewRow);
                mDtModules.AcceptChanges();
                mIconIndex++;

                //laboratory
                mNewRow = mDtModules.NewRow();
                mNewRow["modulekey"] = "laboratory";
                mNewRow["moduletext"] = "Medical Laboratory";
                mNewRow["activationkey"] = "lab";
                mNewRow["iconindex"] = mIconIndex;
                mDtModules.Rows.Add(mNewRow);
                mDtModules.AcceptChanges();
                mIconIndex++;

                //RCH
                mNewRow = mDtModules.NewRow();
                mNewRow["modulekey"] = "RCH";
                mNewRow["moduletext"] = "Reproductive & Child Health";
                mNewRow["activationkey"] = "rch";
                mNewRow["iconindex"] = mIconIndex;
                mDtModules.Rows.Add(mNewRow);
                mDtModules.AcceptChanges();
                mIconIndex++;

                //inventory
                mNewRow = mDtModules.NewRow();
                mNewRow["modulekey"] = "inventory";
                mNewRow["moduletext"] = "Inventory";
                mNewRow["activationkey"] = "ivt";
                mNewRow["iconindex"] = mIconIndex;
                mDtModules.Rows.Add(mNewRow);
                mDtModules.AcceptChanges();
                mIconIndex++;

                //customers
                mNewRow = mDtModules.NewRow();
                mNewRow["modulekey"] = "customers";
                mNewRow["moduletext"] = "Customers";
                mNewRow["activationkey"] = "cus";
                mNewRow["iconindex"] = mIconIndex;
                mDtModules.Rows.Add(mNewRow);
                mDtModules.AcceptChanges();
                mIconIndex++;

                //billing
                mNewRow = mDtModules.NewRow();
                mNewRow["modulekey"] = "billing";
                mNewRow["moduletext"] = "Patient Billing";
                mNewRow["activationkey"] = "bil";
                mNewRow["iconindex"] = mIconIndex;
                mDtModules.Rows.Add(mNewRow);
                mDtModules.AcceptChanges();
                mIconIndex++;

                //ctc
                mNewRow = mDtModules.NewRow();
                mNewRow["modulekey"] = "ctc";
                mNewRow["moduletext"] = "HIV-ART";
                mNewRow["activationkey"] = "ctc";
                mNewRow["iconindex"] = mIconIndex;
                mDtModules.Rows.Add(mNewRow);
                mDtModules.AcceptChanges();
                mIconIndex++;

                //MTUHA
                mNewRow = mDtModules.NewRow();
                mNewRow["modulekey"] = "mtuha";
                mNewRow["moduletext"] = "MTUHA";
                mNewRow["activationkey"] = "mtu";
                mNewRow["iconindex"] = mIconIndex;
                mDtModules.Rows.Add(mNewRow);
                mDtModules.AcceptChanges();
                mIconIndex++;

                //reports
                mNewRow = mDtModules.NewRow();
                mNewRow["modulekey"] = "reports";
                mNewRow["moduletext"] = "Reports";
                mNewRow["activationkey"] = "rpt";
                mNewRow["iconindex"] = mIconIndex;
                mDtModules.Rows.Add(mNewRow);
                mDtModules.AcceptChanges();
                mIconIndex++;

                //billingsetup
                mNewRow = mDtModules.NewRow();
                mNewRow["modulekey"] = "billingsetup";
                mNewRow["moduletext"] = "Billing Setup";
                mNewRow["activationkey"] = "bls";
                mNewRow["iconindex"] = mIconIndex;
                mDtModules.Rows.Add(mNewRow);
                mDtModules.AcceptChanges();
                mIconIndex++;

                //inventorysetup
                mNewRow = mDtModules.NewRow();
                mNewRow["modulekey"] = "inventorysetup";
                mNewRow["moduletext"] = "Inventory Setup";
                mNewRow["activationkey"] = "ivs";
                mNewRow["iconindex"] = mIconIndex;
                mDtModules.Rows.Add(mNewRow);
                mDtModules.AcceptChanges();
                mIconIndex++;

                //ctcsetup
                mNewRow = mDtModules.NewRow();
                mNewRow["modulekey"] = "ctcsetup";
                mNewRow["moduletext"] = "HIV-ART Setup";
                mNewRow["activationkey"] = "cts";
                mNewRow["iconindex"] = mIconIndex;
                mDtModules.Rows.Add(mNewRow);
                mDtModules.AcceptChanges();
                mIconIndex++;

                //reportdesigner
                mNewRow = mDtModules.NewRow();
                mNewRow["modulekey"] = "reportdesigner";
                mNewRow["moduletext"] = "Report Designer";
                mNewRow["activationkey"] = "rpd";
                mNewRow["iconindex"] = mIconIndex;
                mDtModules.Rows.Add(mNewRow);
                mDtModules.AcceptChanges();
                mIconIndex++;

                //generalsetup
                mNewRow = mDtModules.NewRow();
                mNewRow["modulekey"] = "generalsetup";
                mNewRow["moduletext"] = "General Setup";
                mNewRow["activationkey"] = "gen";
                mNewRow["iconindex"] = mIconIndex;
                mDtModules.Rows.Add(mNewRow);
                mDtModules.AcceptChanges();
                mIconIndex++;

                //securitysetup
                mNewRow = mDtModules.NewRow();
                mNewRow["modulekey"] = "securitysetup";
                mNewRow["moduletext"] = "Security Setup";
                mNewRow["activationkey"] = "sec";
                mNewRow["iconindex"] = mIconIndex;
                mDtModules.Rows.Add(mNewRow);
                mDtModules.AcceptChanges();
                mIconIndex++;

                //mobilehealth
                mNewRow = mDtModules.NewRow();
                mNewRow["modulekey"] = "mobilehealth";
                mNewRow["moduletext"] = "Mobile Health";
                mNewRow["activationkey"] = "sms";
                mNewRow["iconindex"] = 8;
                mDtModules.Rows.Add(mNewRow);
                mDtModules.AcceptChanges();
                mIconIndex++;

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

                        foreach (DataRow mDataRow in mDtModules.Rows)
                        {
                            int mRowIndex = mDvLanguage.Find(mDataRow["modulekey"].ToString().Trim());
                            if (mRowIndex >= 0)
                            {
                                mDataRow.BeginEdit();
                                mDataRow["moduletext"] = mDvLanguage[mRowIndex]["description"].ToString().Trim();
                                mDataRow.EndEdit();
                            }
                        }
                    }
                    catch { }
                }

                #endregion

                return mDtModules;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
        }
        #endregion

        #region Get_ModuleItems
        public DataTable Get_ModuleItems(string mLanguageName, string mGridName)
        {
            String mFunctionName = "Get_ModuleItems";

            try
            {
                DataTable mDtModuleItems = new DataTable("moduleitems");
                mDtModuleItems.Columns.Add("modulekey", typeof(System.String));
                mDtModuleItems.Columns.Add("moduleitemkey", typeof(System.String));
                mDtModuleItems.Columns.Add("moduleitemtext", typeof(System.String));
                mDtModuleItems.Columns.Add("iconindex", typeof(System.Int16));
                mDtModuleItems.Columns.Add("functionsprefix", typeof(System.String));

                DataRow mNewRow;
                Int16 mIconIndex = 0;

                #region moduleitems

                #region outpatientdept

                //OPD Registrations
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "outpatientdept";
                mNewRow["moduleitemkey"] = "outpatientdept0";
                mNewRow["moduleitemtext"] = "OPD Registrations";
                mNewRow["functionsprefix"] = "opdregistrations";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Edit Patient Details
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "outpatientdept";
                mNewRow["moduleitemkey"] = "outpatientdept1";
                mNewRow["moduleitemtext"] = "Edit Patient Details";
                mNewRow["functionsprefix"] = "opdeditpatientdetails";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Patient Documents
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "outpatientdept";
                mNewRow["moduleitemkey"] = "outpatientdept2";
                mNewRow["moduleitemtext"] = "Patient Documents";
                mNewRow["functionsprefix"] = "opdpatientdocuments";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Patient Listing
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "outpatientdept";
                mNewRow["moduleitemkey"] = "outpatientdept3";
                mNewRow["moduleitemtext"] = "Patient Listing";
                mNewRow["functionsprefix"] = "";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                #endregion

                //mIconIndex = 4

                #region inpatientdept

                //Wards
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "inpatientdept";
                mNewRow["moduleitemkey"] = "inpatientdept0";
                mNewRow["moduleitemtext"] = "Wards";
                mNewRow["functionsprefix"] = "ipdwards";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Rooms
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "inpatientdept";
                mNewRow["moduleitemkey"] = "inpatientdept1";
                mNewRow["moduleitemtext"] = "Rooms";
                mNewRow["functionsprefix"] = "ipdwardrooms";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Beds
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "inpatientdept";
                mNewRow["moduleitemkey"] = "inpatientdept2";
                mNewRow["moduleitemtext"] = "Beds";
                mNewRow["functionsprefix"] = "ipdwardroombeds";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Discharge Status
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "inpatientdept";
                mNewRow["moduleitemkey"] = "inpatientdept3";
                mNewRow["moduleitemtext"] = "Discharge Status";
                mNewRow["functionsprefix"] = "ipddischargestatus";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //IPD Registrations
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "inpatientdept";
                mNewRow["moduleitemkey"] = "inpatientdept4";
                mNewRow["moduleitemtext"] = "IPD Registrations";
                mNewRow["functionsprefix"] = "ipdregistrations";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Edit Patient Details
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "inpatientdept";
                mNewRow["moduleitemkey"] = "inpatientdept5";
                mNewRow["moduleitemtext"] = "Edit Patient Details";
                mNewRow["functionsprefix"] = "opdeditpatientdetails";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Patient Documents
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "inpatientdept";
                mNewRow["moduleitemkey"] = "inpatientdept6";
                mNewRow["moduleitemtext"] = "Patient Documents";
                mNewRow["functionsprefix"] = "";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Patient Listing
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "inpatientdept";
                mNewRow["moduleitemkey"] = "inpatientdept7";
                mNewRow["moduleitemtext"] = "Patient Listing";
                mNewRow["functionsprefix"] = "";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Transfers
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "inpatientdept";
                mNewRow["moduleitemkey"] = "inpatientdept8";
                mNewRow["moduleitemtext"] = "Transfers";
                mNewRow["functionsprefix"] = "ipdtransfers";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Discharges
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "inpatientdept";
                mNewRow["moduleitemkey"] = "inpatientdept9";
                mNewRow["moduleitemtext"] = "Discharges";
                mNewRow["functionsprefix"] = "ipddischarges";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                #endregion

                #region diagnosesandtreatments

                //Groups
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "diagnosesandtreatments";
                mNewRow["moduleitemkey"] = "diagnosesandtreatments0";
                mNewRow["moduleitemtext"] = "Groups";
                mNewRow["functionsprefix"] = "dxtgroups";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Sub Groups
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "diagnosesandtreatments";
                mNewRow["moduleitemkey"] = "diagnosesandtreatments1";
                mNewRow["moduleitemtext"] = "Sub Groups";
                mNewRow["functionsprefix"] = "dxtsubgroups";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Diagnoses
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "diagnosesandtreatments";
                mNewRow["moduleitemkey"] = "diagnosesandtreatments2";
                mNewRow["moduleitemtext"] = "Diagnoses";
                mNewRow["functionsprefix"] = "dxtdiagnoses";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Patients Diagnoses
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "diagnosesandtreatments";
                mNewRow["moduleitemkey"] = "diagnosesandtreatments3";
                mNewRow["moduleitemtext"] = "Patients Diagnoses";
                mNewRow["functionsprefix"] = "dxtpatientdiagnoses";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Listing
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "diagnosesandtreatments";
                mNewRow["moduleitemkey"] = "diagnosesandtreatments4";
                mNewRow["moduleitemtext"] = "Diagnoses listing";
                mNewRow["functionsprefix"] = "dxtlisting";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Print Treatment Prescription
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "diagnosesandtreatments";
                mNewRow["moduleitemkey"] = "diagnosesandtreatments8";
                mNewRow["moduleitemtext"] = "Print Treatment Prescription";
                mNewRow["functionsprefix"] = "";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                ////Indicators
                //mNewRow = mDtModuleItems.NewRow();
                //mNewRow["modulekey"] = "diagnosesandtreatments";
                //mNewRow["moduleitemkey"] = "diagnosesandtreatments6";
                //mNewRow["moduleitemtext"] = "Indicators";
                //mNewRow["functionsprefix"] = "dxtindicators";
                //mNewRow["iconindex"] = mIconIndex;
                //mDtModuleItems.Rows.Add(mNewRow);
                //mDtModuleItems.AcceptChanges();
                //mIconIndex++;

                ////Indicators to Diagnoses Mapping
                //mNewRow = mDtModuleItems.NewRow();
                //mNewRow["modulekey"] = "diagnosesandtreatments";
                //mNewRow["moduleitemkey"] = "diagnosesandtreatments7";
                //mNewRow["moduleitemtext"] = "Indicators to Diagnoses Mapping";
                //mNewRow["functionsprefix"] = "";
                //mNewRow["iconindex"] = mIconIndex;
                //mDtModuleItems.Rows.Add(mNewRow);
                //mDtModuleItems.AcceptChanges();
                //mIconIndex++;

                #endregion

                #region laboratory

                //laboratories
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "laboratory";
                mNewRow["moduleitemkey"] = "laboratory0";
                mNewRow["moduleitemtext"] = "Laboratories";
                mNewRow["functionsprefix"] = "lab";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //lab test groups
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "laboratory";
                mNewRow["moduleitemkey"] = "laboratory1";
                mNewRow["moduleitemtext"] = "Lab test groups";
                mNewRow["functionsprefix"] = "labtestgroup";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //lab test sub groups
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "laboratory";
                mNewRow["moduleitemkey"] = "laboratory2";
                mNewRow["moduleitemtext"] = "Lab test sub groups";
                mNewRow["functionsprefix"] = "labtestsubgroup";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //lab tests
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "laboratory";
                mNewRow["moduleitemkey"] = "laboratory3";
                mNewRow["moduleitemtext"] = "Lab Tests";
                mNewRow["functionsprefix"] = "labtest";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Remarks
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "laboratory";
                mNewRow["moduleitemkey"] = "laboratory4";
                mNewRow["moduleitemtext"] = "Remarks";
                mNewRow["functionsprefix"] = "labremarks";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //patient lab tests
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "laboratory";
                mNewRow["moduleitemkey"] = "laboratory5";
                mNewRow["moduleitemtext"] = "Patient lab tests";
                mNewRow["functionsprefix"] = "labpatienttests";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Listing
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "laboratory";
                mNewRow["moduleitemkey"] = "laboratory6";
                mNewRow["moduleitemtext"] = "Lab tests listing";
                mNewRow["functionsprefix"] = "";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                #endregion

                #region rch

                //Family Planning Methods
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "RCH";
                mNewRow["moduleitemkey"] = "rch0";
                mNewRow["moduleitemtext"] = "Family Planning Methods";
                mNewRow["functionsprefix"] = "rchfplanmethods";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Antenatal Danger Indicators
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "RCH";
                mNewRow["moduleitemkey"] = "rch1";
                mNewRow["moduleitemtext"] = "Antenatal Danger Indicators";
                mNewRow["functionsprefix"] = "rchdangerindicators";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Birth Methods
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "RCH";
                mNewRow["moduleitemkey"] = "rch2";
                mNewRow["moduleitemtext"] = "Birth Methods";
                mNewRow["functionsprefix"] = "rchbirthmethods";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Birth Complications
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "RCH";
                mNewRow["moduleitemkey"] = "rch3";
                mNewRow["moduleitemtext"] = "Birth Complications";
                mNewRow["functionsprefix"] = "rchbirthcomplications";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Vaccines
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "RCH";
                mNewRow["moduleitemkey"] = "rch4";
                mNewRow["moduleitemtext"] = "Vaccines";
                mNewRow["functionsprefix"] = "rchvaccines";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Clients
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "RCH";
                mNewRow["moduleitemkey"] = "rch5";
                mNewRow["moduleitemtext"] = "Clients";
                mNewRow["functionsprefix"] = "rchclients";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Edit Client Details
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "RCH";
                mNewRow["moduleitemkey"] = "rch6";
                mNewRow["moduleitemtext"] = "Edit Client Details";
                mNewRow["functionsprefix"] = "rchclients";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Family Planning
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "RCH";
                mNewRow["moduleitemkey"] = "rch7";
                mNewRow["moduleitemtext"] = "Family Planning";
                mNewRow["functionsprefix"] = "rchfamilyplanning";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Antenatal Care
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "RCH";
                mNewRow["moduleitemkey"] = "rch8";
                mNewRow["moduleitemtext"] = "Antenatal Care";
                mNewRow["functionsprefix"] = "rchantenatal";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Maternity care
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "RCH";
                mNewRow["moduleitemkey"] = "rch12";
                mNewRow["moduleitemtext"] = "Maternity";
                mNewRow["functionsprefix"] = "rchmaternity";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Postnatal Care
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "RCH";
                mNewRow["moduleitemkey"] = "rch9";
                mNewRow["moduleitemtext"] = "Postnatal Care";
                mNewRow["functionsprefix"] = "rchpostnatal";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Children Health
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "RCH";
                mNewRow["moduleitemkey"] = "rch10";
                mNewRow["moduleitemtext"] = "Children Health";
                mNewRow["functionsprefix"] = "rchchildren";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Vaccinations
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "RCH";
                mNewRow["moduleitemkey"] = "rch11";
                mNewRow["moduleitemtext"] = "Vaccinations";
                mNewRow["functionsprefix"] = "rchvaccinations";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;


                #endregion

                #region inventory

                //Purchase Orders
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "inventory";
                mNewRow["moduleitemkey"] = "inventory0";
                mNewRow["moduleitemtext"] = "Purchase Orders";
                mNewRow["functionsprefix"] = "ivorders";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Transfer Inventory In
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "inventory";
                mNewRow["moduleitemkey"] = "inventory1";
                mNewRow["moduleitemtext"] = "Transfer Inventory In";
                mNewRow["functionsprefix"] = "ivtransferins";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Transfer Inventory Out
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "inventory";
                mNewRow["moduleitemkey"] = "inventory2";
                mNewRow["moduleitemtext"] = "Transfer Inventory Out";
                mNewRow["functionsprefix"] = "ivtransferouts";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Physical Inventory
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "inventory";
                mNewRow["moduleitemkey"] = "inventory3";
                mNewRow["moduleitemtext"] = "Physical Inventory";
                mNewRow["functionsprefix"] = "ivphysicalinventory";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Issues History
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "inventory";
                mNewRow["moduleitemkey"] = "inventory4";
                mNewRow["moduleitemtext"] = "Issues History";
                mNewRow["functionsprefix"] = "ivissueshistory";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Receipts History
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "inventory";
                mNewRow["moduleitemkey"] = "inventory5";
                mNewRow["moduleitemtext"] = "Receipts History";
                mNewRow["functionsprefix"] = "ivreceiptshistory";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                #endregion

                #region customers

                //Groups
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "customers";
                mNewRow["moduleitemkey"] = "customers0";
                mNewRow["moduleitemtext"] = "Groups";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Sub-Groups
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "customers";
                mNewRow["moduleitemkey"] = "customers1";
                mNewRow["moduleitemtext"] = "Sub-Groups";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Members
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "customers";
                mNewRow["moduleitemkey"] = "customers2";
                mNewRow["moduleitemtext"] = "Members";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Import Members
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "customers";
                mNewRow["moduleitemkey"] = "customers3";
                mNewRow["moduleitemtext"] = "Import Members";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Activate/DeActivate Members
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "customers";
                mNewRow["moduleitemkey"] = "customers4";
                mNewRow["moduleitemtext"] = "Activate/DeActivate Members";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Terminate Members
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "customers";
                mNewRow["moduleitemkey"] = "customers5";
                mNewRow["moduleitemtext"] = "Terminate Members";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                #endregion

                #region billing

                //Deposits Accounts
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "billing";
                mNewRow["moduleitemkey"] = "billing0";
                mNewRow["moduleitemtext"] = "Deposit Accounts";
                mNewRow["functionsprefix"] = "bildepositaccounts";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Deposit Account Members
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "billing";
                mNewRow["moduleitemkey"] = "billing1";
                mNewRow["moduleitemtext"] = "Deposit Account Members";
                mNewRow["functionsprefix"] = "bildepositaccountmembers";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Post Bills
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "billing";
                mNewRow["moduleitemkey"] = "billing2";
                mNewRow["moduleitemtext"] = "Post Bills";
                mNewRow["functionsprefix"] = "bilpostbills";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Pay Bills (Patients)
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "billing";
                mNewRow["moduleitemkey"] = "billing3";
                mNewRow["moduleitemtext"] = "Pay Bills (Patients)";
                mNewRow["functionsprefix"] = "bilpaybillspatients";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Pay Bills (Groups)
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "billing";
                mNewRow["moduleitemkey"] = "billing4";
                mNewRow["moduleitemtext"] = "Pay Bills (Groups)";
                mNewRow["functionsprefix"] = "bilpaybillsgroups";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Sales History (All)
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "billing";
                mNewRow["moduleitemkey"] = "billing5";
                mNewRow["moduleitemtext"] = "Sales History (All)";
                mNewRow["functionsprefix"] = "";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Sales History (Cash)
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "billing";
                mNewRow["moduleitemkey"] = "billing6";
                mNewRow["moduleitemtext"] = "Sales History (Cash)";
                mNewRow["functionsprefix"] = "billsaleshistorycash";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Sales History (Invoices)
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "billing";
                mNewRow["moduleitemkey"] = "billing7";
                mNewRow["moduleitemtext"] = "Sales History (Invoices)";
                mNewRow["functionsprefix"] = "billsaleshistoryinvoices";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Invoice Payments History
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "billing";
                mNewRow["moduleitemkey"] = "billing8";
                mNewRow["moduleitemtext"] = "Invoice Payments History";
                mNewRow["functionsprefix"] = "billpaymentshistory";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Payment Refunds
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "billing";
                mNewRow["moduleitemkey"] = "billing9";
                mNewRow["moduleitemtext"] = "Payment Refunds";
                mNewRow["functionsprefix"] = "billvoidedsaleshistory";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Request for Debt Relief
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "billing";
                mNewRow["moduleitemkey"] = "billing10";
                mNewRow["moduleitemtext"] = "Request for Debt Relief";
                mNewRow["functionsprefix"] = "billdebtreliefrequests";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Debt Relief Requests View
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "billing";
                mNewRow["moduleitemkey"] = "billing11";
                mNewRow["moduleitemtext"] = "Debt Relief Requests View";
                mNewRow["functionsprefix"] = "billapprovedebtreliefs";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Change Billing Information
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "billing";
                mNewRow["moduleitemkey"] = "billing12";
                mNewRow["moduleitemtext"] = "Change Billing Information";
                mNewRow["functionsprefix"] = "";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                #endregion

                #region MTUHA

                //Diagnoses
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "mtuha";
                mNewRow["moduleitemkey"] = "mtuha0";
                mNewRow["moduleitemtext"] = "Diagnoses";
                mNewRow["functionsprefix"] = "mtuhadiagnoses";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Diagnoses Mapping
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "mtuha";
                mNewRow["moduleitemkey"] = "mtuha1";
                mNewRow["moduleitemtext"] = "Diagnoses Mapping";
                mNewRow["functionsprefix"] = "mtuhadiagnosesmapping";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //DHIS Export
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "mtuha";
                mNewRow["moduleitemkey"] = "mtuha2";
                mNewRow["moduleitemtext"] = "DHIS Export";
                mNewRow["functionsprefix"] = "dhisexport";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                #endregion

                #region billingsetup

                //Currencies
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "billingsetup";
                mNewRow["moduleitemkey"] = "billingsetup0";
                mNewRow["moduleitemtext"] = "Currencies";
                mNewRow["functionsprefix"] = "blscurrencies";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Payment Types
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "billingsetup";
                mNewRow["moduleitemkey"] = "billingsetup1";
                mNewRow["moduleitemtext"] = "Payment Types";
                mNewRow["functionsprefix"] = "blspaymenttypes";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Price Categories
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "billingsetup";
                mNewRow["moduleitemkey"] = "billingsetup2";
                mNewRow["moduleitemtext"] = "Price Categories";
                mNewRow["functionsprefix"] = "blspricecategories";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Item Groups
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "billingsetup";
                mNewRow["moduleitemkey"] = "billingsetup3";
                mNewRow["moduleitemtext"] = "Item Groups";
                mNewRow["functionsprefix"] = "blsitemgroups";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Item Sub-Groups
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "billingsetup";
                mNewRow["moduleitemkey"] = "billingsetup4";
                mNewRow["moduleitemtext"] = "Item Sub-Groups";
                mNewRow["functionsprefix"] = "blsitemsubgroups";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Items
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "billingsetup";
                mNewRow["moduleitemkey"] = "billingsetup5";
                mNewRow["moduleitemtext"] = "Items";
                mNewRow["functionsprefix"] = "blsitems";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                #endregion

                #region inventorysetup

                //Stores
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "inventorysetup";
                mNewRow["moduleitemkey"] = "inventorysetup0";
                mNewRow["moduleitemtext"] = "Stores";
                mNewRow["functionsprefix"] = "ivsstores";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Product Categories
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "inventorysetup";
                mNewRow["moduleitemkey"] = "inventorysetup1";
                mNewRow["moduleitemtext"] = "Product Categories";
                mNewRow["functionsprefix"] = "ivsproductcategories";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Units
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "inventorysetup";
                mNewRow["moduleitemkey"] = "inventorysetup2";
                mNewRow["moduleitemtext"] = "Units";
                mNewRow["functionsprefix"] = "ivspackagings";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Suppliers Register
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "inventorysetup";
                mNewRow["moduleitemkey"] = "inventorysetup3";
                mNewRow["moduleitemtext"] = "Sources of Stock";
                mNewRow["functionsprefix"] = "ivssourcesofstock";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Customers Register
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "inventorysetup";
                mNewRow["moduleitemkey"] = "inventorysetup4";
                mNewRow["moduleitemtext"] = "Customers Register";
                mNewRow["functionsprefix"] = "ivscustomers";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Products Register
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "inventorysetup";
                mNewRow["moduleitemkey"] = "inventorysetup5";
                mNewRow["moduleitemtext"] = "Products Register";
                mNewRow["functionsprefix"] = "ivsproducts";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                #endregion

                #region reportdesigner

                //Report Groups
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "reportdesigner";
                mNewRow["moduleitemkey"] = "reportdesigner0";
                mNewRow["moduleitemtext"] = "Report Groups";
                mNewRow["functionsprefix"] = "rpdreportgroups";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Report Templates
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "reportdesigner";
                mNewRow["moduleitemkey"] = "reportdesigner1";
                mNewRow["moduleitemtext"] = "Report Templates";
                mNewRow["functionsprefix"] = "rpdreporttemplates";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                #endregion

                #region generalsetup

                //Facility Setup
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "generalsetup";
                mNewRow["moduleitemkey"] = "generalsetup0";
                mNewRow["moduleitemtext"] = "Facility Setup";
                mNewRow["functionsprefix"] = "genfacilitysetup";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Auto Generated Codes
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "generalsetup";
                mNewRow["moduleitemkey"] = "generalsetup1";
                mNewRow["moduleitemtext"] = "Auto Generated Codes";
                mNewRow["functionsprefix"] = "genautocodes";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Countries
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "generalsetup";
                mNewRow["moduleitemkey"] = "generalsetup2";
                mNewRow["moduleitemtext"] = "Countries";
                mNewRow["functionsprefix"] = "gencountries";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Regions
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "generalsetup";
                mNewRow["moduleitemkey"] = "generalsetup3";
                mNewRow["moduleitemtext"] = "Regions";
                mNewRow["functionsprefix"] = "genregions";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Districts
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "generalsetup";
                mNewRow["moduleitemkey"] = "generalsetup4";
                mNewRow["moduleitemtext"] = "Districts";
                mNewRow["functionsprefix"] = "gendistricts";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Medical Staffs
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "generalsetup";
                mNewRow["moduleitemkey"] = "generalsetup5";
                mNewRow["moduleitemtext"] = "Medical Staffs";
                mNewRow["functionsprefix"] = "genmedicalstaffs";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Treatment Points
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "generalsetup";
                mNewRow["moduleitemkey"] = "generalsetup6";
                mNewRow["moduleitemtext"] = "Treatment Points";
                mNewRow["functionsprefix"] = "gentreatmentpoints";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Search Engine
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "generalsetup";
                mNewRow["moduleitemkey"] = "generalsetup7";
                mNewRow["moduleitemtext"] = "Search Engine";
                mNewRow["functionsprefix"] = "gensearchengine";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Patient Documents Setup
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "generalsetup";
                mNewRow["moduleitemkey"] = "generalsetup8";
                mNewRow["moduleitemtext"] = "Patient Documents Setup";
                mNewRow["functionsprefix"] = "opdpatientdocuments";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Patient Extra Fields
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "generalsetup";
                mNewRow["moduleitemkey"] = "generalsetup9";
                mNewRow["moduleitemtext"] = "Patient Extra Fields";
                mNewRow["functionsprefix"] = "genpatientextrafields";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                #endregion

                #region securitysetup

                //user groups
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "securitysetup";
                mNewRow["moduleitemkey"] = "securitysetup0";
                mNewRow["moduleitemtext"] = "User Groups";
                mNewRow["functionsprefix"] = "secusergroups";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //groups access
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "securitysetup";
                mNewRow["moduleitemkey"] = "securitysetup1";
                mNewRow["moduleitemtext"] = "Groups Access";
                mNewRow["functionsprefix"] = "secgroupsaccess";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //reports access
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "securitysetup";
                mNewRow["moduleitemkey"] = "securitysetup2";
                mNewRow["moduleitemtext"] = "Reports Access";
                mNewRow["functionsprefix"] = "secreportsaccess";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Group Printers
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "securitysetup";
                mNewRow["moduleitemkey"] = "securitysetup3";
                mNewRow["moduleitemtext"] = "Group Printers";
                mNewRow["functionsprefix"] = "";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //users
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "securitysetup";
                mNewRow["moduleitemkey"] = "securitysetup4";
                mNewRow["moduleitemtext"] = "Users";
                mNewRow["functionsprefix"] = "secusers";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                #endregion

                #region ctcsetup

                //Marital Status
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctcsetup";
                mNewRow["moduleitemkey"] = "ctcsetup0";
                mNewRow["moduleitemtext"] = "Marital Status";
                mNewRow["functionsprefix"] = "maritalstatus";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Functional Status
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctcsetup";
                mNewRow["moduleitemkey"] = "ctcsetup1";
                mNewRow["moduleitemtext"] = "Functional Status";
                mNewRow["functionsprefix"] = "functionalstatus";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //TB Status
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctcsetup";
                mNewRow["moduleitemkey"] = "ctcsetup2";
                mNewRow["moduleitemtext"] = "TB Status";
                mNewRow["functionsprefix"] = "tbstatus";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //ARV Status
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctcsetup";
                mNewRow["moduleitemkey"] = "ctcsetup3";
                mNewRow["moduleitemtext"] = "ARV Status";
                mNewRow["functionsprefix"] = "arvstatus";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //AIDS Defining Illness, New Symptoms, Side Effects, Hospitalized
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctcsetup";
                mNewRow["moduleitemkey"] = "ctcsetup4";
                mNewRow["moduleitemtext"] = "AIDS Defining Illness, New Symptoms, Side Effects, Hospitalized";
                mNewRow["functionsprefix"] = "aidsillness";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //ARV Combination Regimens
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctcsetup";
                mNewRow["moduleitemkey"] = "ctcsetup5";
                mNewRow["moduleitemtext"] = "ARV Combination Regimens";
                mNewRow["functionsprefix"] = "arvcombregimens";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //ARV Adherence
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctcsetup";
                mNewRow["moduleitemkey"] = "ctcsetup6";
                mNewRow["moduleitemtext"] = "ARV Adherence";
                mNewRow["functionsprefix"] = "arvadherence";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //ARV Poor Adherence Reasons
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctcsetup";
                mNewRow["moduleitemkey"] = "ctcsetup7";
                mNewRow["moduleitemtext"] = "ARV Poor Adherence Reasons";
                mNewRow["functionsprefix"] = "ctc_arvpooradherencereasons";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Refered To
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctcsetup";
                mNewRow["moduleitemkey"] = "ctcsetup8";
                mNewRow["moduleitemtext"] = "Refered To";
                mNewRow["functionsprefix"] = "referedto";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //ARV Reasons
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctcsetup";
                mNewRow["moduleitemkey"] = "ctcsetup9";
                mNewRow["moduleitemtext"] = "ARV Reasons";
                mNewRow["functionsprefix"] = "arvreasons";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Follow Up Status
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctcsetup";
                mNewRow["moduleitemkey"] = "ctcsetup10";
                mNewRow["moduleitemtext"] = "Follow Up Status";
                mNewRow["functionsprefix"] = "followupstatus";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Refered From
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctcsetup";
                mNewRow["moduleitemkey"] = "ctcsetup11";
                mNewRow["moduleitemtext"] = "Refered From";
                mNewRow["functionsprefix"] = "referedfrom";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Prior ARV Exposure
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctcsetup";
                mNewRow["moduleitemkey"] = "ctcsetup12";
                mNewRow["moduleitemtext"] = "Prior ARV Exposure";
                mNewRow["functionsprefix"] = "priorarvexposure";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //ART Why Eligible
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctcsetup";
                mNewRow["moduleitemkey"] = "ctcsetup13";
                mNewRow["moduleitemtext"] = "ART Why Eligible";
                mNewRow["functionsprefix"] = "artwhyeligible";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Relevant Co-Meds
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctcsetup";
                mNewRow["moduleitemkey"] = "ctcsetup14";
                mNewRow["moduleitemtext"] = "Relevant Co-Meds";
                mNewRow["functionsprefix"] = "ctcrelevantcomeds";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Abnormal Lab Results
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctcsetup";
                mNewRow["moduleitemkey"] = "ctcsetup15";
                mNewRow["moduleitemtext"] = "Abnormal Lab Results";
                mNewRow["functionsprefix"] = "ctcabnormallabresults";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                #endregion

                #region ctc

                //Registration
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctc";
                mNewRow["moduleitemkey"] = "ctc0";
                mNewRow["moduleitemtext"] = "Registration";
                mNewRow["functionsprefix"] = "ctcclients";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Edit Patient Details
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctc";
                mNewRow["moduleitemkey"] = "ctc1";
                mNewRow["moduleitemtext"] = "Edit Patient Details";
                mNewRow["functionsprefix"] = "ctcclients";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Appointments
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctc";
                mNewRow["moduleitemkey"] = "ctc2";
                mNewRow["moduleitemtext"] = "Appointments";
                mNewRow["functionsprefix"] = "ctcappointments";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Measurements
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctc";
                mNewRow["moduleitemkey"] = "ctc3";
                mNewRow["moduleitemtext"] = "Triage";
                mNewRow["functionsprefix"] = "ctcmeasurements";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //HTC
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctc";
                mNewRow["moduleitemkey"] = "ctc4";
                mNewRow["moduleitemtext"] = "HTC/VCT";
                mNewRow["functionsprefix"] = "ctchivtests";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //DNA PCR
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctc";
                mNewRow["moduleitemkey"] = "ctc5";
                mNewRow["moduleitemtext"] = "DNA PCR";
                mNewRow["functionsprefix"] = "ctcpcrtests";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //CD4 Testing
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctc";
                mNewRow["moduleitemkey"] = "ctc6";
                mNewRow["moduleitemtext"] = "CD4 Testing";
                mNewRow["functionsprefix"] = "ctccd4tests";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Pre-ART Child/Adult
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctc";
                mNewRow["moduleitemkey"] = "ctc7";
                mNewRow["moduleitemtext"] = "Pre-ART Child/Adult";
                mNewRow["functionsprefix"] = "ctcpreart";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Adult ARV Formulations
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctc";
                mNewRow["moduleitemkey"] = "ctc8";
                mNewRow["moduleitemtext"] = "Adult ARV Formulations";
                mNewRow["functionsprefix"] = "ctcart";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Paediatric ARV Formulations
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctc";
                mNewRow["moduleitemkey"] = "ctc9";
                mNewRow["moduleitemtext"] = "Paediatric ARV Formulations";
                mNewRow["functionsprefix"] = "ctcartp";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Exposed Child Under 24 Months
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctc";
                mNewRow["moduleitemkey"] = "ctc10";
                mNewRow["moduleitemtext"] = "Exposed Child Under 24 Months";
                mNewRow["functionsprefix"] = "ctcexposed";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //CTC 2
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctc";
                mNewRow["moduleitemkey"] = "ctc11";
                mNewRow["moduleitemtext"] = "CTC 2: Patient Records";
                mNewRow["functionsprefix"] = "ctcartt";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //PMTCT ANC Register
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctc";
                mNewRow["moduleitemkey"] = "ctc12";
                mNewRow["moduleitemtext"] = "PMTCT ANC Register";
                mNewRow["functionsprefix"] = "ctcpmtctanc";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //PMTCT Delivery Register
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctc";
                mNewRow["moduleitemkey"] = "ctc13";
                mNewRow["moduleitemtext"] = "PMTCT Delivery Register";
                mNewRow["functionsprefix"] = "ctcpmtctdelivery";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //PMTCT Mother-Child Register
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctc";
                mNewRow["moduleitemkey"] = "ctc14";
                mNewRow["moduleitemtext"] = "PMTCT Mother-Child Register";
                mNewRow["functionsprefix"] = "ctcpmtctmotherchild";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Contactable Clinic Staff
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "ctc";
                mNewRow["moduleitemkey"] = "ctc15";
                mNewRow["moduleitemtext"] = "Contactable Clinical Staff";
                mNewRow["functionsprefix"] = "ctcpmtctclinicstaff";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                #endregion

                #region sms
                mIconIndex = 0;
                //SMS Module
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "mobilehealth";
                mNewRow["moduleitemkey"] = "mobilehealth0";
                mNewRow["moduleitemtext"] = "Message Templates";
                mNewRow["functionsprefix"] = "smsmessagetemplates";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Patient categories
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "mobilehealth";
                mNewRow["moduleitemkey"] = "mobilehealth1";
                mNewRow["moduleitemtext"] = "Patient Categories";
                mNewRow["functionsprefix"] = "smspatientcategories";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Sent Messages
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "mobilehealth";
                mNewRow["moduleitemkey"] = "mobilehealth2";
                mNewRow["moduleitemtext"] = "Sent Messages";
                mNewRow["functionsprefix"] = "smssentmessages";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Received Messages
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "mobilehealth";
                mNewRow["moduleitemkey"] = "mobilehealth3";
                mNewRow["moduleitemtext"] = "Received Messages";
                mNewRow["functionsprefix"] = "smsreceivedmessages";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Registered Patients
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "mobilehealth";
                mNewRow["moduleitemkey"] = "mobilehealth4";
                mNewRow["moduleitemtext"] = "Mobile Registrations";
                mNewRow["functionsprefix"] = "smsregisteredpatients";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Registered Patients
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "mobilehealth";
                mNewRow["moduleitemkey"] = "mobilehealth5";
                mNewRow["moduleitemtext"] = "Registered Patients";
                mNewRow["functionsprefix"] = "smsactivepatients";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //In active patients
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "mobilehealth";
                mNewRow["moduleitemkey"] = "mobilehealth6";
                mNewRow["moduleitemtext"] = "Community Workers";
                mNewRow["functionsprefix"] = "smscommunityworkers";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Trash messages
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "mobilehealth";
                mNewRow["moduleitemkey"] = "mobilehealth7";
                mNewRow["moduleitemtext"] = "Trash Messages";
                mNewRow["functionsprefix"] = "smstrashmessages";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Send messages
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "mobilehealth";
                mNewRow["moduleitemkey"] = "mobilehealth8";
                mNewRow["moduleitemtext"] = "Send Messages";
                mNewRow["functionsprefix"] = "smssendmessages";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Settings messages
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "mobilehealth";
                mNewRow["moduleitemkey"] = "mobilehealth9";
                mNewRow["moduleitemtext"] = "Module Settings";
                mNewRow["functionsprefix"] = "smsmodulesettings";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;

                //Mobile Patients
                mNewRow = mDtModuleItems.NewRow();
                mNewRow["modulekey"] = "mobilehealth";
                mNewRow["moduleitemkey"] = "mobilehealth10";
                mNewRow["moduleitemtext"] = "Mobile Patients List";
                mNewRow["functionsprefix"] = "smspatientslist";
                mNewRow["iconindex"] = mIconIndex;
                mDtModuleItems.Rows.Add(mNewRow);
                mDtModuleItems.AcceptChanges();
                mIconIndex++;



                #endregion

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

                        foreach (DataRow mDataRow in mDtModuleItems.Rows)
                        {
                            int mRowIndex = mDvLanguage.Find(mDataRow["moduleitemkey"].ToString().Trim());
                            if (mRowIndex >= 0)
                            {
                                mDataRow.BeginEdit();
                                mDataRow["moduleitemtext"] = mDvLanguage[mRowIndex]["description"].ToString().Trim();
                                mDataRow.EndEdit();
                            }
                        }
                    }
                    catch { }
                }

                #endregion

                return mDtModuleItems;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
        }
        #endregion

        #region Get_AccessFunctions
        public DataTable Get_AccessFunctions(string mLanguageName, string mGridName)
        {
            String mFunctionName = "Get_AccessFunctions";

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
                DataTable mDtAccessFunctions = new DataTable("accessfunctions");
                mDtAccessFunctions.Columns.Add("functionaccesskey", typeof(System.String));
                mDtAccessFunctions.Columns.Add("functionaccesstext", typeof(System.String));

                Type mFunctionKeys = typeof(AfyaPro_Types.clsSystemFunctions.FunctionKeys);
                foreach (string mFunctionKey in Enum.GetNames(mFunctionKeys))
                {
                    DataRow mNewRow = mDtAccessFunctions.NewRow();
                    mNewRow["functionaccesskey"] = mFunctionKey;
                    mNewRow["functionaccesstext"] = mFunctionKey;
                    mDtAccessFunctions.Rows.Add(mNewRow);
                    mDtAccessFunctions.AcceptChanges();
                }

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
                            DataRow mNewRow = mDtLanguage.NewRow();
                            mNewRow["controlname"] = (string)mElement.Element("controlname").Value.Trim().ToLower();
                            mNewRow["description"] = (string)mElement.Element("description").Value.Trim();
                            mDtLanguage.Rows.Add(mNewRow);
                            mDtLanguage.AcceptChanges();
                        }

                        DataView mDvLanguage = new DataView();
                        mDvLanguage.Table = mDtLanguage;
                        mDvLanguage.Sort = "controlname";

                        foreach (DataRow mDataRow in mDtAccessFunctions.Rows)
                        {
                            int mRowIndex = mDvLanguage.Find(mDataRow["functionaccesskey"].ToString().Trim());
                            if (mRowIndex >= 0)
                            {
                                mDataRow.BeginEdit();
                                mDataRow["functionaccesstext"] = mDvLanguage[mRowIndex]["description"].ToString().Trim();
                                mDataRow.EndEdit();
                            }
                        }
                    }
                    catch { }
                }

                #endregion

                return mDtAccessFunctions;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
        }
        #endregion

        #region Get_UserModuleItems
        public DataTable Get_UserModuleItems(String mFilter, String mOrder)
        {
            String mFunctionName = "Get_UserModuleItems";

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
                string mCommandText = "select * from sys_usergroupmoduleitems";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("sys_usergroupmoduleitems");
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

        #region Get_UserFunctionAccessKeys
        public DataTable Get_UserFunctionAccessKeys(String mFilter, String mOrder)
        {
            String mFunctionName = "Get_UserFunctionAccessKeys";

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
                string mCommandText = "select * from sys_usergroupfunctions";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("sys_usergroupfunctions");
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

        #region Get_UserReports
        public DataTable Get_UserReports(String mFilter, String mOrder)
        {
            String mFunctionName = "Get_UserReports";

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
                string mCommandText = "select * from sys_usergroupreports";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("sys_usergroupreports");
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

        #region Save_UserModuleItems
        public AfyaPro_Types.clsResult Save_UserModuleItems(String mUserGroupCode, DataTable mDtModuleItems, 
            DataTable mDtFunctionAccessKeys, string mUserId)
        {
            String mFunctionName = "Save_UserModuleItems";

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
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.secgroupsaccess_edit.ToString(), mUserId);
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
                mCommand.CommandText = "select * from sys_usergroups where code='" + mUserGroupCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.SEC_UserGroupCodeDoesNotExist.ToString();
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

            #region do save
            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                mCommand.CommandText = "delete from sys_usergroupmoduleitems where usergroupcode='" + mUserGroupCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                mCommand.CommandText = "delete from sys_usergroupfunctions where usergroupcode='" + mUserGroupCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtModuleItems.Rows)
                {
                    mCommand.CommandText = "insert into sys_usergroupmoduleitems(usergroupcode,moduleitemkey) values('"
                    + mUserGroupCode.Trim() + "','" + mDataRow["moduleitemkey"].ToString().Trim() + "')";
                    mCommand.ExecuteNonQuery();
                }

                foreach (DataRow mDataRow in mDtFunctionAccessKeys.Rows)
                {
                    mCommand.CommandText = "insert into sys_usergroupfunctions(usergroupcode,functionaccesskey) values('"
                    + mUserGroupCode.Trim() + "','" + mDataRow["functionaccesskey"].ToString().Trim() + "')";
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

        #region Save_ReportsAccess
        public AfyaPro_Types.clsResult Save_ReportsAccess(String mUserGroupCode, DataTable mDtReports, string mUserId)
        {
            String mFunctionName = "Save_ReportsAccess";

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
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.secreportsaccess_edit.ToString(), mUserId);
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
                mCommand.CommandText = "select * from sys_usergroups where code='" + mUserGroupCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.SEC_UserGroupCodeDoesNotExist.ToString();
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

            #region do save
            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                mCommand.CommandText = "delete from sys_usergroupreports where usergroupcode='" + mUserGroupCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtReports.Rows)
                {
                    mCommand.CommandText = "insert into sys_usergroupreports(usergroupcode,reportcode,reportview,"
                    + "reportdesign,reportformcustomization) values('" + mUserGroupCode.Trim() + "','" 
                    + mDataRow["reportcode"].ToString().Trim() + "'," + Convert.ToInt16(mDataRow["reportview"]) + ","
                    + Convert.ToInt16(mDataRow["reportdesign"]) + "," + Convert.ToInt16(mDataRow["reportformcustomization"]) + ")";
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
