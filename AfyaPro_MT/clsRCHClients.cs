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

namespace AfyaPro_MT
{
    public class clsRCHClients : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsRCHClients";

        #endregion

        #region View_Clients
        public DataTable View_Clients(String mFilter, String mOrder)
        {
            String mFunctionName = "View_Clients";

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
                string mCommandText = "select * from patients";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("patients");
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

        #region Get_RCHServices
        public DataTable Get_RCHServices(string mLanguageName, string mGridName)
        {
            String mFunctionName = "Get_RCHServices";

            try
            {
                DataTable mDataTable = new DataTable("rchservices");
                mDataTable.Columns.Add("code", typeof(System.String));
                mDataTable.Columns.Add("description", typeof(System.String));
                mDataTable.Columns.Add("fieldname", typeof(System.String));
                mDataTable.RemotingFormat = SerializationFormat.Binary;

                DataRow mNewRow;

                #region load planning methods

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "rchservice0";
                mNewRow["description"] = "Family planning";
                mNewRow["fieldname"] = AfyaPro_Types.clsEnums.RCHServices.familyplanning.ToString();
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "rchservice1";
                mNewRow["description"] = "Antenatal care";
                mNewRow["fieldname"] = AfyaPro_Types.clsEnums.RCHServices.antenatalcare.ToString();
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "rchservice2";
                mNewRow["description"] = "Postnatal care";
                mNewRow["fieldname"] = AfyaPro_Types.clsEnums.RCHServices.postnatalcare.ToString();
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "rchservice3";
                mNewRow["description"] = "Children Health";
                mNewRow["fieldname"] = AfyaPro_Types.clsEnums.RCHServices.childrenhealth.ToString();
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "rchservice4";
                mNewRow["description"] = "Vaccinations";
                mNewRow["fieldname"] = AfyaPro_Types.clsEnums.RCHServices.vaccinations.ToString();
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

        #region Get_Client
        public AfyaPro_Types.clsRchClient Get_Client(string mClientCode)
        {
            string mFunctionName = "Get_Client";

            AfyaPro_Types.clsRchClient mClient = new AfyaPro_Types.clsRchClient();
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
                DataTable mDtClients = new DataTable("patients");
                mCommand.CommandText = "select * from patients where code='" + mClientCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtClients);

                if (mDtClients.Rows.Count > 0)
                {
                    mClient.Exist = true;
                    mClient.code = mDtClients.Rows[0]["code"].ToString();
                    mClient.surname = mDtClients.Rows[0]["surname"].ToString();
                    mClient.firstname = mDtClients.Rows[0]["firstname"].ToString();
                    mClient.othernames = mDtClients.Rows[0]["othernames"].ToString();
                    mClient.gender = mDtClients.Rows[0]["gender"].ToString();
                    mClient.birthdate = Convert.ToDateTime(mDtClients.Rows[0]["birthdate"]);
                    mClient.regdate = Convert.ToDateTime(mDtClients.Rows[0]["regdate"]);
                    mClient.maritalstatuscode = mDtClients.Rows[0]["maritalstatuscode"].ToString();
                    mClient.allergies = mDtClients.Rows[0]["allergies"].ToString();

                    mDtClients.Clear();
                    mCommand.CommandText = "select * from rch_antenatalattendances where clientcode='" + mClientCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtClients);

                    if (mDtClients.Rows.Count > 0)
                    {
                        mClient.RegNo = mDtClients.Rows[0]["regno"].ToString();
                        mClient.Residence = mDtClients.Rows[0]["residence"].ToString();
                        mClient.Gravida = mDtClients.Rows[0]["gravida"].ToString();
                        mClient.Para = mDtClients.Rows[0]["para"].ToString();
                        mClient.LMP = mDtClients.Rows[0]["lmp"].ToString();
                        mClient.EDD = mDtClients.Rows[0]["edd"].ToString();
                         
                    }
                }
                else
                {
                    mClient.Exe_Result = 0;
                    mClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.RCH_ClientCodeDoesNotExist.ToString();
                    return mClient;
                }

                return mClient;
            }
            catch (OdbcException ex)
            {
                mClient.Exe_Result = -1;
                mClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mClient;
            }
        }
        #endregion

        #region Add_Client
        public AfyaPro_Types.clsRchClient Add_Client(Int16 mGenerateCode, string mCode, string mSurname, string mFirstName,
            string mOtherNames, string mGender, DateTime mBirthDate, DateTime mRegDate, DataTable mDtExtraDetails, 
            DateTime mTransDate, string mMachineName, string mMachineUser,
            string mUserId)
        {
            String mFunctionName = "Add_Client";

            AfyaPro_Types.clsRchClient mClient = new AfyaPro_Types.clsRchClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;
            mBirthDate = mBirthDate.Date;
            mRegDate = mRegDate.Date;

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
                mClient.Exe_Result = -1;
                mClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.rchclients_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mClient.Exe_Result = 0;
                mClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mClient;
            }
            #endregion

            #region auto generate code, if option is on

            if (mGenerateCode == 1)
            {
                clsAutoCodes objAutoCodes = new clsAutoCodes();
                AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                mObjCode = objAutoCodes.Next_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.rchcustomerno), "patients", "code");
                if (mObjCode.Exe_Result == -1)
                {
                    mClient.Exe_Result = mObjCode.Exe_Result;
                    mClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                    return mClient;
                }
                mCode = mObjCode.GeneratedCode;
            }

            #endregion

            #region check 4 duplicate
            try
            {
                mCommand.CommandText = "select * from patients where code='"
                + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mClient.Exe_Result = 0;
                    mClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.RCH_ClientCodeIsInUse.ToString();
                    return mClient;
                }
            }
            catch (OdbcException ex)
            {
                mClient.Exe_Result = -1;
                mClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mClient;
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

                string mExtraDetailFields = "";
                string mExtraDetailValues = "";

                #region prepare extra field details

                if (mDtExtraDetails.Rows.Count > 0)
                {
                    foreach (DataColumn mDataColumn in mDtExtraDetails.Columns)
                    {
                        string mFieldValue = "";

                        #region get field value
                        switch (mDataColumn.DataType.FullName.Trim().ToLower())
                        {
                            case "system.datetime":
                                {
                                    mFieldValue = clsGlobal.Saving_DateValueNullable(mDtExtraDetails.Rows[0][mDataColumn.ColumnName]);
                                }
                                break;
                            case "system.double":
                                {
                                    mFieldValue = mDtExtraDetails.Rows[0][mDataColumn.ColumnName].ToString();
                                }
                                break;
                            case "system.decimal":
                                {
                                    mFieldValue = mDtExtraDetails.Rows[0][mDataColumn.ColumnName].ToString();
                                }
                                break;
                            case "system.int32":
                                {
                                    mFieldValue = mDtExtraDetails.Rows[0][mDataColumn.ColumnName].ToString();
                                }
                                break;
                            default:
                                {
                                    mFieldValue = "'" + mDtExtraDetails.Rows[0][mDataColumn.ColumnName].ToString() + "'";
                                }
                                break;
                        }
                        #endregion

                        if (mExtraDetailFields.Trim() == "")
                        {
                            mExtraDetailFields = mDataColumn.ColumnName;
                            mExtraDetailValues = mFieldValue;
                        }
                        else
                        {
                            mExtraDetailFields = mExtraDetailFields + "," + mDataColumn.ColumnName;
                            mExtraDetailValues = mExtraDetailValues + "," + mFieldValue;
                        }
                    }
                }

                if (mExtraDetailFields.Trim() != "")
                {
                    mExtraDetailFields = "," + mExtraDetailFields;
                    mExtraDetailValues = "," + mExtraDetailValues;
                }

                #endregion

                //patients
                mCommand.CommandText = "insert into patients(code,surname,firstname,othernames,gender,birthdate"
                + ",regdate" + mExtraDetailFields + ") values('" + mCode.Trim() + "','"
                + mSurname.Trim() + "','" + mFirstName.Trim() + "','" + mOtherNames.Trim() + "','" + mGender + "',"
                + clsGlobal.Saving_DateValue(mBirthDate) + "," + clsGlobal.Saving_DateValue(mRegDate) + mExtraDetailValues + ")";
                int mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit patients

                if (mRecsAffected > 0)
                {
                    string mAuditTableName = clsGlobal.gAfyaProAuditDbName + clsGlobal.gDbNameTableNameSep + "patients";
                    string mAuditingFields = clsGlobal.AuditingFields();
                    string mAuditingValues = clsGlobal.AuditingValues(mTransDate, mUserId,
                        AfyaPro_Types.clsEnums.AuditChangeTypes.Insert.ToString());

                    mCommand.CommandText = "insert into " + mAuditTableName + "(code,surname,firstname,othernames,gender,birthdate"
                    + ",regdate" + mExtraDetailFields + "," + mAuditingFields + ") values('" + mCode.Trim() + "','"
                    + mSurname.Trim() + "','" + mFirstName.Trim() + "','" + mOtherNames.Trim() + "','" + mGender + "',"
                    + clsGlobal.Saving_DateValue(mBirthDate) + "," + clsGlobal.Saving_DateValue(mRegDate) + mExtraDetailValues 
                    + "," + mAuditingValues + ")";
                    mCommand.ExecuteNonQuery();

                }

                #endregion

                #region remember user entries for next time use

                DataTable mDtExtraFields = new DataTable("patientextrafields");
                mCommand.CommandText = "select * from patientextrafields";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtExtraFields);

                DataView mDvExtraFields = new DataView();
                mDvExtraFields.Table = mDtExtraFields;
                mDvExtraFields.Sort = "fieldname";

                if (mDtExtraDetails.Rows.Count > 0)
                {
                    foreach (DataColumn mDataColumn in mDtExtraDetails.Columns)
                    {
                        string mFieldName = mDataColumn.ColumnName;
                        string mFilterOnValueFrom = "";
                        string mFieldType = "";

                        int mRowIndex = mDvExtraFields.Find(mFieldName);
                        if (mRowIndex >= 0)
                        {
                            mFieldType = mDvExtraFields[mRowIndex]["fieldtype"].ToString().Trim();
                            mFilterOnValueFrom = mDvExtraFields[mRowIndex]["filteronvaluefrom"].ToString().Trim();
                        }

                        if (mFieldType.Trim().ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            string mFilterValue = "";
                            string mDescription = mDtExtraDetails.Rows[0][mFieldName].ToString();
                            if (mFilterOnValueFrom.Trim() != "")
                            {
                                mFilterValue = mDtExtraDetails.Rows[0][mFilterOnValueFrom].ToString().Trim();
                            }

                            DataTable mDtLookup = new DataTable("lookup");
                            mCommand.CommandText = "select * from patientextrafieldlookup where fieldname='"
                            + mFieldName + "' and description='" + mDescription + "' and filtervalue='" + mFilterValue + "'";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(mDtLookup);

                            if (mDtLookup.Rows.Count == 0)
                            {
                                mCommand.CommandText = "insert into patientextrafieldlookup(fieldname,description,"
                                + "filtervalue) values('" + mFieldName + "','" + mDescription + "','" + mFilterValue + "')";
                                mCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }

                //surname
                DataTable mDtSurname = new DataTable("surname");
                mCommand.CommandText = "select * from patientsurnames where description='" + mSurname.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSurname);
                if (mDtSurname.Rows.Count == 0)
                {
                    mCommand.CommandText = "insert into patientsurnames(description) values('" + mSurname.Trim() + "')";
                    mCommand.ExecuteNonQuery();
                }

                //firstname
                DataTable mDtFirstName = new DataTable("firstname");
                mCommand.CommandText = "select * from patientfirstnames where description='" + mFirstName.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFirstName);
                if (mDtFirstName.Rows.Count == 0)
                {
                    mCommand.CommandText = "insert into patientfirstnames(description) values('" + mFirstName.Trim() + "')";
                    mCommand.ExecuteNonQuery();
                }

                //othernames
                DataTable mDtOtherNames = new DataTable("othernames");
                mCommand.CommandText = "select * from patientothernames where description='" + mOtherNames.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtOtherNames);
                if (mDtOtherNames.Rows.Count == 0)
                {
                    mCommand.CommandText = "insert into patientothernames(description) values('" + mOtherNames.Trim() + "')";
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                if (mGenerateCode == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.rchcustomerno);
                    mCommand.ExecuteNonQuery();
                }

                //commit
                mTrans.Commit();

                //get patient
                mClient = this.Get_Client(mCode);

                //return
                return mClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mClient.Exe_Result = -1;
                mClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mClient;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Edit_Client
        public AfyaPro_Types.clsRchClient Edit_Client(string mCode, string mSurname, string mFirstName,
            string mOtherNames, string mGender, DateTime mBirthDate, DateTime mRegDate, DataTable mDtExtraDetails, 
            DateTime mTransDate, string mMachineName, string mMachineUser,
            string mUserId)
        {
            String mFunctionName = "Edit_Client";

            AfyaPro_Types.clsRchClient mClient = new AfyaPro_Types.clsRchClient();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            mTransDate = mTransDate.Date;
            mBirthDate = mBirthDate.Date;
            mRegDate = mRegDate.Date;

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
                mClient.Exe_Result = -1;
                mClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mClient;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.rchclients_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mClient.Exe_Result = 0;
                mClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mClient;
            }
            #endregion

            #region check 4 existance
            try
            {
                mCommand.CommandText = "select * from patients where code='"
                + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mClient.Exe_Result = 0;
                    mClient.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.RCH_ClientCodeDoesNotExist.ToString();
                    return mClient;
                }
            }
            catch (OdbcException ex)
            {
                mClient.Exe_Result = -1;
                mClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mClient;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion

            #region edit
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                string mUpdateExtraDetails = "";

                #region prepare extra field details

                if (mDtExtraDetails.Rows.Count > 0)
                {
                    foreach (DataColumn mDataColumn in mDtExtraDetails.Columns)
                    {
                        string mFieldValue = "";

                        #region get field value
                        switch (mDataColumn.DataType.FullName.Trim().ToLower())
                        {
                            case "system.datetime":
                                {
                                    mFieldValue = clsGlobal.Saving_DateValueNullable(mDtExtraDetails.Rows[0][mDataColumn.ColumnName]);
                                }
                                break;
                            case "system.double":
                                {
                                    mFieldValue = mDtExtraDetails.Rows[0][mDataColumn.ColumnName].ToString();
                                }
                                break;
                            case "system.decimal":
                                {
                                    mFieldValue = mDtExtraDetails.Rows[0][mDataColumn.ColumnName].ToString();
                                }
                                break;
                            case "system.int32":
                                {
                                    mFieldValue = mDtExtraDetails.Rows[0][mDataColumn.ColumnName].ToString();
                                }
                                break;
                            default:
                                {
                                    mFieldValue = "'" + mDtExtraDetails.Rows[0][mDataColumn.ColumnName].ToString() + "'";
                                }
                                break;
                        }
                        #endregion

                        if (mUpdateExtraDetails.Trim() == "")
                        {
                            mUpdateExtraDetails = mDataColumn.ColumnName + "=" + mFieldValue;
                        }
                        else
                        {
                            mUpdateExtraDetails = mUpdateExtraDetails + "," + mDataColumn.ColumnName + "=" + mFieldValue;
                        }
                    }
                }

                if (mUpdateExtraDetails.Trim() != "")
                {
                    mUpdateExtraDetails = "," + mUpdateExtraDetails;
                }

                #endregion

                //patients
                mCommand.CommandText = "update patients set surname='" + mSurname.Trim() + "',firstname='" + mFirstName.Trim()
                + "',othernames='" + mOtherNames.Trim() + "',gender='" + mGender + "',birthdate="
                + clsGlobal.Saving_DateValue(mBirthDate) + "',regdate=" + clsGlobal.Saving_DateValue(mRegDate)
                + mUpdateExtraDetails + " where code='" + mCode.Trim() + "'";
                int mRecsAffected = mCommand.ExecuteNonQuery();

                #region audit patients

                if (mRecsAffected > 0)
                {
                    DataTable mDtClients = new DataTable("patients");
                    mCommand.CommandText = "select * from patients where code='" + mCode.Trim() + "'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtClients);

                    mCommand.CommandText = clsGlobal.Audit_ThisRecord(mDtClients, "patients",
                    mTransDate, mUserId, AfyaPro_Types.clsEnums.AuditChangeTypes.Update.ToString());
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region remember user entries for next time use

                DataTable mDtExtraFields = new DataTable("patientextrafields");
                mCommand.CommandText = "select * from patientextrafields";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtExtraFields);

                DataView mDvExtraFields = new DataView();
                mDvExtraFields.Table = mDtExtraFields;
                mDvExtraFields.Sort = "fieldname";

                if (mDtExtraDetails.Rows.Count > 0)
                {
                    foreach (DataColumn mDataColumn in mDtExtraDetails.Columns)
                    {
                        string mFieldName = mDataColumn.ColumnName;
                        string mFilterOnValueFrom = "";
                        string mFieldType = "";

                        int mRowIndex = mDvExtraFields.Find(mFieldName);
                        if (mRowIndex >= 0)
                        {
                            mFieldType = mDvExtraFields[mRowIndex]["fieldtype"].ToString().Trim();
                            mFilterOnValueFrom = mDvExtraFields[mRowIndex]["filteronvaluefrom"].ToString().Trim();
                        }

                        if (mFieldType.Trim().ToLower() == AfyaPro_Types.clsEnums.FieldTypes.dropdown.ToString().ToLower())
                        {
                            string mFilterValue = "";
                            string mDescription = mDtExtraDetails.Rows[0][mFieldName].ToString();
                            if (mFilterOnValueFrom.Trim() != "")
                            {
                                mFilterValue = mDtExtraDetails.Rows[0][mFilterOnValueFrom].ToString().Trim();
                            }

                            DataTable mDtLookup = new DataTable("lookup");
                            mCommand.CommandText = "select * from patientextrafieldlookup where fieldname='"
                            + mFieldName + "' and description='" + mDescription + "' and filtervalue='" + mFilterValue + "'";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(mDtLookup);

                            if (mDtLookup.Rows.Count == 0)
                            {
                                mCommand.CommandText = "insert into patientextrafieldlookup(fieldname,description,"
                                + "filtervalue) values('" + mFieldName + "','" + mDescription + "','" + mFilterValue + "')";
                                mCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }

                //surname
                DataTable mDtSurname = new DataTable("surname");
                mCommand.CommandText = "select * from patientsurnames where description='" + mSurname.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSurname);
                if (mDtSurname.Rows.Count == 0)
                {
                    mCommand.CommandText = "insert into patientsurnames(description) values('" + mSurname.Trim() + "')";
                    mCommand.ExecuteNonQuery();
                }

                //firstname
                DataTable mDtFirstName = new DataTable("firstname");
                mCommand.CommandText = "select * from patientfirstnames where description='" + mFirstName.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFirstName);
                if (mDtFirstName.Rows.Count == 0)
                {
                    mCommand.CommandText = "insert into patientfirstnames(description) values('" + mFirstName.Trim() + "')";
                    mCommand.ExecuteNonQuery();
                }

                //othernames
                DataTable mDtOtherNames = new DataTable("othernames");
                mCommand.CommandText = "select * from patientothernames where description='" + mOtherNames.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtOtherNames);
                if (mDtOtherNames.Rows.Count == 0)
                {
                    mCommand.CommandText = "insert into patientothernames(description) values('" + mOtherNames.Trim() + "')";
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                //commit
                mTrans.Commit();

                //get patient
                mClient = this.Get_Client(mCode);

                //return
                return mClient;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mClient.Exe_Result = -1;
                mClient.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mClient;
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
