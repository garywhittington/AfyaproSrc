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
    /// <summary>
    /// This is test for documentation
    /// </summary>
    public class clsUsers : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsUsers";

        #endregion

        #region View
        public DataTable View(String mFilter, String mOrder, string mLanguageName, string mGridName)
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
                string mCommandText = "select * from sys_users";
                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }
                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("sys_users");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                #region add usergroup description info

                mDataTable.Columns.Add("usergroupdescription", typeof(System.String));

                DataTable mDtUserGroups = new DataTable("sys_usergroups");
                mCommand.CommandText = "select * from sys_usergroups";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtUserGroups);
                DataView mDvUserGroups = new DataView();
                mDvUserGroups.Table = mDtUserGroups;
                mDvUserGroups.Sort = "code";

                foreach (DataRow mDataRow in mDataTable.Rows)
                {
                    string mUserGroupDescription = "";

                    int mRowIndex = mDvUserGroups.Find(mDataRow["usergroupcode"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mUserGroupDescription = mDvUserGroups[mRowIndex]["description"].ToString().Trim();
                    }

                    mDataRow["usergroupdescription"] = mUserGroupDescription;
                    mDataRow.EndEdit();
                    mDataTable.AcceptChanges();
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

        #region View_StoreUsers
        public DataTable View_StoreUsers(String mFilter, String mOrder)
        {
            String mFunctionName = "View_StoreUsers";

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
                string mCommandText = "select * from sys_storeusers";
                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }
                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("sys_storeusers");
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

        #region Add
        public AfyaPro_Types.clsResult Add(String mCode, String mPassword, String mDescription,
            String mUserGroupCode, String mOccupation, String mAddress, String mPhone,
            String mLanguage, string mClientGroupCode, string mClientSubGroupCode, string mPaymentTypeCode,
            string mPriceCategoryCode, string mDefaultStoreCode, int mAllowChangingTransDate, DataTable mDtStores,  string mUserId)
        {
            String mFunctionName = "Add";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.secusers_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region check for duplicate
            try
            {
                mCommand.CommandText = "select * from sys_users where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.SEC_UserIdIsInUse.ToString();
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

            #region do add
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //ecrypt password
                clsCryptoService mCryptoService = new clsCryptoService(clsCryptoService.SymmProvEnum.Rijndael);
                mPassword = mCryptoService.Encrypt(mPassword, clsGlobal.gCryptoKey);

                //sys_users
                mCommand.CommandText = "insert into sys_users(code, description, password, "
                    + "usergroupcode, occupation, address, phone, storecode,defaultlanguage,"
                    + "defaultclientgroupcode,defaultclientsubgroupcode,defaultpaymenttypecode,"
                    + "defaultpricecategorycode,allowchangingtransdate) values('" + mCode + "','" + mDescription + "','" + mPassword 
                    + "','" + mUserGroupCode.Trim() + "','" + mOccupation + "','" + mAddress + "','" 
                    + mPhone + "','" + mDefaultStoreCode.Trim() + "','" + mLanguage.Trim() + "','" + mClientGroupCode.Trim()
                    + "','" + mClientSubGroupCode.Trim() + "','" + mPaymentTypeCode.Trim() + "','"
                    + mPriceCategoryCode.Trim() + "'," + mAllowChangingTransDate + ")";
                mCommand.ExecuteNonQuery();

                //facilitystaffs
                int mStaffCategoryCode = -1;
                Int16 mSync = 0;

                DataTable mDtUserGroups = new DataTable("usergroups");
                mCommand.CommandText = "select * from sys_usergroups where code='" + mUserGroupCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtUserGroups);

                if (mDtUserGroups.Rows.Count > 0)
                {
                    mStaffCategoryCode = Convert.ToInt16(mDtUserGroups.Rows[0]["staffcategorycode"]);
                    mSync = Convert.ToInt16(mDtUserGroups.Rows[0]["synchronizewithstaffs"]);
                }
                if (mStaffCategoryCode != -1)
                {
                    List<clsDataField> mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("code", DataTypes.dbstring.ToString(), mCode));
                    mDataFields.Add(new clsDataField("description", DataTypes.dbstring.ToString(), mDescription));
                    mDataFields.Add(new clsDataField("category", DataTypes.dbnumber.ToString(), mStaffCategoryCode));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("facilitystaffs", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                //sys_storeusers
                mCommand.CommandText = "delete from sys_storeusers where usercode='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtStores.Rows)
                {
                    mCommand.CommandText = "insert into sys_storeusers(usercode,storecode) values('"
                    + mCode.Trim() + "','" + mDataRow["storecode"].ToString().Trim() + "')";
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
            String mUserGroupCode, String mOccupation, String mAddress, String mPhone,
            String mLanguage, string mClientGroupCode, string mClientSubGroupCode, string mPaymentTypeCode,
            string mPriceCategoryCode, string mDefaultStoreCode, int mAllowChangingTransDate, DataTable mDtStores,  string mUserId)
        {
            String mFunctionName = "Edit";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.secusers_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region check for existance
            try
            {
                mCommand.CommandText = "select * from sys_users where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.SEC_UserIdDoesNotExist.ToString();
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
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //sys_users
                mCommand.CommandText = "update sys_users set description='" + mDescription
                + "', usergroupcode='" + mUserGroupCode.Trim() + "', occupation='"
                + mOccupation + "', address='" + mAddress + "', phone='" + mPhone
                + "', storecode='" + mDefaultStoreCode.Trim() + "',defaultlanguage='" + mLanguage.Trim()
                + "',defaultclientgroupcode='" + mClientGroupCode.Trim() + "',defaultclientsubgroupcode='"
                + mClientSubGroupCode.Trim() + "',defaultpaymenttypecode='" + mPaymentTypeCode.Trim()
                + "',defaultpricecategorycode='" + mPriceCategoryCode.Trim() + "',allowchangingtransdate="
                + mAllowChangingTransDate + " where code='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                //facilitystaffs
                int mStaffCategoryCode = -1;
                Int16 mSync = 0;

                DataTable mDtUserGroups = new DataTable("usergroups");
                mCommand.CommandText = "select * from sys_usergroups where code='" + mUserGroupCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtUserGroups);

                if (mDtUserGroups.Rows.Count > 0)
                {
                    mStaffCategoryCode = Convert.ToInt16(mDtUserGroups.Rows[0]["staffcategorycode"]);
                    mSync = Convert.ToInt16(mDtUserGroups.Rows[0]["synchronizewithstaffs"]);
                }
                if (mStaffCategoryCode != -1)
                {
                    List<clsDataField> mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("description", DataTypes.dbstring.ToString(), mDescription));

                    mCommand.CommandText = clsGlobal.Get_UpdateStatement("facilitystaffs", mDataFields,
                        "category=" + mStaffCategoryCode + " and code='" + mCode + "'");
                    mCommand.ExecuteNonQuery();
                }

                //sys_storeusers
                mCommand.CommandText = "delete from sys_storeusers where usercode='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtStores.Rows)
                {
                    mCommand.CommandText = "insert into sys_storeusers(usercode,storecode) values('"
                    + mCode.Trim() + "','" + mDataRow["storecode"].ToString().Trim() + "')";
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.secusers_delete.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region check for existance
            try
            {
                mCommand.CommandText = "select * from sys_users where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.SEC_UserIdDoesNotExist.ToString();
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
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //sys_users
                mCommand.CommandText = "delete from sys_users where code='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                //sys_storeusers
                mCommand.CommandText = "delete from sys_storeusers where usercode='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

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

        #region Change_Password
        public AfyaPro_Types.clsResult Change_Password(String mCode, String mNewPassword)
        {
            String mFunctionName = "Change_Password";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;

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

            #region check for existance
            try
            {
                mCommand.CommandText = "select * from sys_users where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.SEC_UserIdDoesNotExist.ToString();
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

            #region do change password
            try
            {
                clsCryptoService mCryptoService = new clsCryptoService(clsCryptoService.SymmProvEnum.Rijndael);
                mNewPassword = mCryptoService.Encrypt(mNewPassword, clsGlobal.gCryptoKey);

                //execute statements
                mCommand.CommandText = "update sys_users set password='" + mNewPassword + "' where code='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                //return
                return mResult;
            }
            catch (OdbcException ex)
            {
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

        #region Login
        public AfyaPro_Types.clsUser Login(String mCode, String mPassword)
        {
            String mFunctionName = "Login";

            AfyaPro_Types.clsUser mUser = new AfyaPro_Types.clsUser();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            String mDbPassword = "";

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
                mUser.Exe_Result = -1;
                mUser.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mUser;
            }

            #endregion

            #region do login
            try
            {
                mCommand.CommandText = "select * from sys_users where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mDbPassword = mDataReader["password"].ToString();

                    mUser.ValidCode = true;
                    mUser.ValidPassword = false;
                    mUser.Code = mDataReader["code"].ToString();
                    mUser.Description = mDataReader["description"].ToString();
                    mUser.Password = mPassword;
                    mUser.UserGroupCode = mDataReader["usergroupcode"].ToString();
                    mUser.StoreCode = mDataReader["storecode"].ToString();
                    mUser.Address = mDataReader["address"].ToString();
                    mUser.Phone = mDataReader["phone"].ToString();
                    mUser.Occupation = mDataReader["occupation"].ToString();
                    mUser.DefaultCustomerGroupCode = mDataReader["defaultclientgroupcode"].ToString();
                    mUser.DefaultCustomerSubGroupCode = mDataReader["defaultclientsubgroupcode"].ToString();
                    mUser.DefaultPaymentTypeCode = mDataReader["defaultpaymenttypecode"].ToString();
                    mUser.DefaultPriceCategoryCode = mDataReader["defaultpricecategorycode"].ToString();
                    mUser.AllowChangingTransDate = Convert.ToInt32(mDataReader["allowchangingtransdate"]);
                }
            }
            catch (OdbcException ex)
            {
                mUser.Exe_Result = -1;
                mUser.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mUser;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion

            #region user group info
            try
            {
                mCommand.CommandText = "select * from sys_usergroups where code='" + mUser.UserGroupCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mUser.UserGroupFormsLayoutTemplateName = mDataReader["formlayouttemplatename"].ToString();
                }
            }
            catch (OdbcException ex)
            {
                mUser.Exe_Result = -1;
                mUser.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mUser;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion

            #region check for valid user id and password
            try
            {
                if (mUser.ValidCode == false)
                {
                    mUser.Exe_Result = 0;
                    mUser.ValidCode = false;
                    mUser.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.SEC_UserIdDoesNotExist.ToString();
                    return mUser;
                }
                else
                {
                    //decrypt password
                    clsCryptoService mCryptoService = new clsCryptoService(clsCryptoService.SymmProvEnum.Rijndael);
                    mDbPassword = mCryptoService.Decrypt(mDbPassword, clsGlobal.gCryptoKey);

                    if (mDbPassword == mPassword)
                    {
                        mUser.Exe_Result = 1;
                        mUser.ValidPassword = true;

                        return mUser;
                    }
                    else
                    {
                        mUser.Exe_Result = 0;
                        mUser.ValidCode = true;
                        mUser.ValidPassword = false;
                        mUser.Password = mPassword;
                        mUser.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.SEC_PasswordIsInvalid.ToString();
                        return mUser;
                    }
                }
            }
            catch (OdbcException ex)
            {
                mUser.Exe_Result = -1;
                mUser.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mUser;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Check_Licence
        public AfyaPro_Types.clsLicence Check_Licence(DateTime mTransDate)
        {
            AfyaPro_Types.clsLicence mLicence = new AfyaPro_Types.clsLicence();

            string mFunctionName = "Check_Licence";

            try
            {
                AfyaPro_SoftwareLocker.Licencing mLicensing =
                    new AfyaPro_SoftwareLocker.Licencing("AfyaProServer",
                        Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\winRidem1978.kum",
                        "Phone: ",
                        30,
                        "141");

                byte[] MyOwnKey = { 97, 250, 1, 5, 84, 21, 7, 63,
                                4, 54, 87, 56, 123, 10, 3, 62,
                                7, 9, 20, 36, 37, 21, 101, 57};
                mLicensing.TripleDESKey = MyOwnKey;

                mLicensing.Check_Licence(mTransDate);

                string mSavedPassword = mLicensing.Get_SavedPassword();

                if (mSavedPassword.Trim().ToLower() == mLicensing.Password.Trim().ToLower())
                {
                    mLicence.FullyActive = true;
                }
                else
                {
                    mLicence.FullyActive = false;
                }
                mLicence.BaseString = mLicensing.BaseString;
                mLicence.TrialDays = mLicensing.TrialDays;
                mLicence.InfoText = mLicensing.InfoText;
                mLicence.ModuleKeys = mLicensing.ModuleKeys;

                return mLicence;
            }
            catch (Exception ex)
            {
                mLicence.Exe_Result = -1;
                mLicence.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mLicence;
            }
        }
        #endregion

        #region Activate_Licence
        public AfyaPro_Types.clsLicence Activate_Licence(byte[] mBytes)
        {
            AfyaPro_Types.clsLicence mLicence = new AfyaPro_Types.clsLicence();

            string mFunctionName = "Activate_Licence";

            try
            {
                string mHidenFilePath = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\winRidem1978.kum";
                AfyaPro_SoftwareLocker.Licencing mLicensing =
                    new AfyaPro_SoftwareLocker.Licencing("AfyaProServer",
                        mHidenFilePath,
                        "Phone: ",
                        30,
                        "141");

                byte[] MyOwnKey = { 97, 250, 1, 5, 84, 21, 7, 63,
                                4, 54, 87, 56, 123, 10, 3, 62,
                                7, 9, 20, 36, 37, 21, 101, 57};
                mLicensing.TripleDESKey = MyOwnKey;

                mLicensing.Check_Licence(DateTime.Now.Date);

                mLicence.InstallationDate = mLicensing.InstallationDate;
                mLicence.BaseString = mLicensing.BaseString;
                mLicence.TrialDays = mLicensing.TrialDays;
                mLicence.InfoText = mLicensing.InfoText;
                mLicence.ModuleKeys = mLicensing.ModuleKeys;
                mLicence.LicenceIds = mLicensing.LicenceIds;

                char[] asciiChars = new char[Encoding.ASCII.GetCharCount(mBytes, 0, mBytes.Length)];
                Encoding.ASCII.GetChars(mBytes, 0, mBytes.Length, asciiChars, 0);
                string asciiString = new string(asciiChars);
                string[] mHideInfo = asciiString.Split(';');

                int mExtendDays = Convert.ToInt32(mHideInfo[0]);
                string mPassword = mHideInfo[1];
                string mBaseString = mHideInfo[2];
                string mModuleKeys = mHideInfo[3];
                string mLicenceId = mHideInfo[4];
                string mLicenceType = mHideInfo[5];

                #region is licence for this machine
                if (mLicence.BaseString != mBaseString)
                {
                    mLicence.Exe_Result = 0;
                    mLicence.Exe_Message = "Activation failed; Specified licence file is not valid";
                    return mLicence;
                }
                #endregion

                #region is licence used before
                bool mLicenceIsUsed = false;

                string[] mUsedLicences = mLicence.LicenceIds.Split(',');
                foreach (string mUsedLicence in mUsedLicences)
                {
                    if (mUsedLicence.Trim().ToLower() == mLicenceId.Trim().ToLower())
                    {
                        mLicenceIsUsed = true;
                        break;
                    }
                }

                if (mLicenceIsUsed == true)
                {
                    mLicence.Exe_Result = 0;
                    mLicence.Exe_Message = "Activation failed; Specified licence has been used";
                    return mLicence;
                }
                #endregion

                switch (mLicenceType.Trim().ToLower())
                {
                    case "trialextend":
                        {
                            string mLicPassword = "Nill";
                            string mHideInfoWrite = ""
                                + mLicence.InstallationDate + ";"
                                + (mLicence.TrialDays + mExtendDays) + ";"
                                + mLicPassword + ";"
                                + mLicence.BaseString + ";"
                                + mLicence.ModuleKeys + ";"
                                + mLicence.LicenceIds + "," + mLicenceId + ";"
                                + "trialextend";

                            AfyaPro_SoftwareLocker.FileReadWrite.WriteFile(mHidenFilePath, mHideInfoWrite);

                            mLicence.Exe_Message = "Trial extended successfully. This will take effect the next time you start application";
                        }
                        break;
                    case "trialreset":
                        {
                            string mLicPassword = "Nill";
                            string mHideInfoWrite = ""
                                + DateTime.Now.Ticks + ";"
                                + mExtendDays + ";"
                                + mLicPassword + ";"
                                + mLicence.BaseString + ";"
                                + mModuleKeys + ";"
                                + mLicence.LicenceIds + "," + mLicenceId + ";"
                                + "trialreset";

                            AfyaPro_SoftwareLocker.FileReadWrite.WriteFile(mHidenFilePath, mHideInfoWrite);

                            mLicence.Exe_Message = "Trial reset successfully. This will take effect the next time you start application";
                        }
                        break;
                    case "full":
                        {
                            #region validate password

                            if (mLicensing.Password.Trim().ToLower() != mPassword.Trim().ToLower())
                            {
                                mLicence.Exe_Result = 0;
                                mLicence.Exe_Message = "Activation failed; Specified licence file is not valid";
                                return mLicence;
                            }

                            #endregion

                            string mLicPassword = mPassword;
                            string mHideInfoWrite = ""
                                + mLicence.InstallationDate + ";"
                                + mLicence.TrialDays + ";"
                                + mLicPassword + ";"
                                + mBaseString + ";"
                                + mModuleKeys + ";"
                                + mLicence.LicenceIds + "," + mLicenceId + ";"
                                + "full";

                            AfyaPro_SoftwareLocker.FileReadWrite.WriteFile(mHidenFilePath, mHideInfoWrite);

                            mLicence.Exe_Message = "Activated successfully. This will take effect the next time you start application";
                        }
                        break;
                }

                mLicence.Exe_Result = 1;
                return mLicence;
            }
            catch (Exception ex)
            {
                mLicence.Exe_Result = -1;
                mLicence.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mLicence;
            }
        }
        #endregion
    }
}
