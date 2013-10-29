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
    public class clsClientGroupMembers : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsClientGroupMembers";

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
                string mCommandText = "select * from facilitycorporatemembers";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("facilitycorporatemembers");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                #region add group and sub group description info

                mDataTable.Columns.Add("billinggroupdescription", typeof(System.String));
                mDataTable.Columns.Add("billingsubgroupdescription", typeof(System.String));

                DataTable mDtGroups = new DataTable("customergroups");
                mCommand.CommandText = "select * from facilitycorporates";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtGroups);
                DataView mDvGroups = new DataView();
                mDvGroups.Table = mDtGroups;
                mDvGroups.Sort = "code";

                DataTable mDtSubGroups = new DataTable("customergroups");
                mCommand.CommandText = "select * from facilitycorporatesubgroups";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtSubGroups);
                DataView mDvSubGroups = new DataView();
                mDvSubGroups.Table = mDtSubGroups;
                mDvSubGroups.Sort = "code";

                foreach (DataRow mDataRow in mDataTable.Rows)
                {
                    string mGroupDescription = "";
                    string mSubGroupDescription = "";

                    int mRowIndex = mDvGroups.Find(mDataRow["billinggroupcode"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mGroupDescription = mDvGroups[mRowIndex]["description"].ToString().Trim();
                    }

                    mRowIndex = mDvSubGroups.Find(mDataRow["billingsubgroupcode"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mSubGroupDescription = mDvSubGroups[mRowIndex]["description"].ToString().Trim();
                    }

                    mDataRow["billinggroupdescription"] = mGroupDescription;
                    mDataRow["billingsubgroupdescription"] = mSubGroupDescription;
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

        #region Add
        public AfyaPro_Types.clsResult Add(Int16 mGenerateCode, Int16 mGenerateMemberId, String mCode, string mSurname,
            string mFirstName, string mOtherNames, string mGender, DateTime mBirthDate, string mGroupCode, 
            string mSubGroupCode, string mMembershipNo, double mCeilingAmount, DateTime mRegDate, DateTime mExpiryDate, 
            Byte[] mPicture, string mPictureExtension)
        {
            String mFunctionName = "Add";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            OdbcTransaction mTrans = null;

            mBirthDate = mBirthDate.Date;
            mExpiryDate = mExpiryDate.Date;
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
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }

            #endregion

            #region auto generate code, if option is on

            if (mGenerateCode == 1)
            {
                clsAutoCodes objAutoCodes = new clsAutoCodes();
                AfyaPro_Types.clsCode mObjCode = new AfyaPro_Types.clsCode();
                mObjCode = objAutoCodes.Next_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.clientgroupmembercode), "facilitycorporatemembers", "code");
                if (mObjCode.Exe_Result == -1)
                {
                    mResult.Exe_Result = mObjCode.Exe_Result;
                    mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                    return mResult;
                }
                mCode = mObjCode.GeneratedCode;
            }

            #endregion

            #region check 4 duplicate code
            try
            {
                mCommand.CommandText = "select * from facilitycorporatemembers where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.CUS_MemberCodeIsInUse.ToString();
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

            #region auto generate membership id, if option is on

            if (mGenerateMemberId == 1)
            {
                clsAutoCodes objAutoCodes2 = new clsAutoCodes();
                AfyaPro_Types.clsCode mObjCode2 = new AfyaPro_Types.clsCode();
                mObjCode2 = objAutoCodes2.Next_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.clientgroupmembershipId),
                    "facilitycorporatemembers", "billinggroupmembershipno");
                if (mObjCode2.Exe_Result == -1)
                {
                    mResult.Exe_Result = mObjCode2.Exe_Result;
                    mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode2.Exe_Message);
                    return mResult;
                }
                mMembershipNo = mObjCode2.GeneratedCode;
            }

            #endregion

            #region check 4 duplicate membership id
            try
            {
                mCommand.CommandText = "select * from facilitycorporatemembers where billinggroupmembershipno='"
                + mMembershipNo.Trim() + "' and billinggroupcode='" + mGroupCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.CUS_MembershipIdIsInUse.ToString();
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

                string mPictureName = "";
                if (mPicture != null)
                {
                    mPictureName = "member_" + mCode.Trim().ToLower() + mPictureExtension;
                }

                mCommand.CommandText = "insert into facilitycorporatemembers(code,surname,firstname,othernames,gender,birthdate,"
                + "billinggroupcode,billingsubgroupcode,billinggroupmembershipno,regdate,expirydate,picturename,inactive,"
                + "membershipstatus) values('" + mCode.Trim() + "','" + mSurname.Trim() + "','" + mFirstName.Trim() + "','"
                + mOtherNames.Trim() + "','" + mGender.Trim() + "'," + clsGlobal.Saving_DateValueNullable(mBirthDate) + ",'"
                + mGroupCode.Trim() + "','" + mSubGroupCode.Trim() + "','" + mMembershipNo.Trim() + "',"
                + clsGlobal.Saving_DateValue(mRegDate) + "," + clsGlobal.Saving_DateValueNullable(mExpiryDate) + ",'" + mPictureName + "',1,1)";
                mCommand.ExecuteNonQuery();

                if (mGenerateCode == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.clientgroupmembercode);
                    mCommand.ExecuteNonQuery();
                }

                if (mGenerateMemberId == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.clientgroupmembershipId);
                    mCommand.ExecuteNonQuery();
                }

                //commit
                mTrans.Commit();

                //save picture if any
                if (mPicture != null)
                {
                    //this.Save_Picture(mPicture, "member_" + mCode.Trim().ToLower(), mPictureExtension);
                    this.Save_Picture(mPicture, "member_" + mCode.Trim(), mGroupCode.Trim());
                }

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
        public AfyaPro_Types.clsResult Edit(String mCode, string mSurname,
            string mFirstName, string mOtherNames, string mGender, DateTime mBirthDate, string mGroupCode, 
            string mSubGroupCode, string mMembershipNo, double mCeilingAmount, DateTime mRegDate, DateTime mExpiryDate, 
            Byte[] mPicture, string mPictureExtension)
        {
            String mFunctionName = "Edit";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;

            mBirthDate = mBirthDate.Date;
            mExpiryDate = mExpiryDate.Date;
            mRegDate = mRegDate.Date;

            string mOldPictureName = "";

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

            #region check for code existance

            try
            {
                mCommand.CommandText = "select * from facilitycorporatemembers where code='" + mCode.Trim()
                + "' and membershipstatus=1 and billinggroupcode='" + mGroupCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.CUS_MemberCodeDoesNotExist.ToString();
                    return mResult;
                }
                else
                {
                    mOldPictureName = mDataReader["picturename"].ToString().Trim();
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

            #region check 4 duplicate membership id
            try
            {
                mCommand.CommandText = "select * from facilitycorporatemembers where billinggroupmembershipno='"
                + mMembershipNo.Trim() + "' and billinggroupcode='" + mGroupCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    if (mDataReader["code"].ToString().Trim().ToLower() != mCode.Trim().ToLower())
                    {
                        mResult.Exe_Result = 0;
                        mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.CUS_MembershipIdIsInUse.ToString();
                        return mResult;
                    }
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

                string mPictureName = "";
                if (mPicture != null)
                {
                    if (File.Exists(clsGlobal.gPicturesPath + "\\" + mOldPictureName) == true)
                    {
                        File.Delete(clsGlobal.gPicturesPath + "\\" + mOldPictureName);
                    }

                    mPictureName = "member_" + mCode.Trim().ToLower() + mPictureExtension;
                }

                mCommand.CommandText = "update facilitycorporatemembers set surname='" + mSurname.Trim() 
                + "',firstname='" + mFirstName.Trim() + "',othernames='" + mOtherNames.Trim() + "',gender='"
                + mGender.Trim() + "',birthdate=" + clsGlobal.Saving_DateValueNullable(mBirthDate) 
                + ",billinggroupmembershipno='" + mMembershipNo.Trim() + "',expirydate=" 
                + clsGlobal.Saving_DateValueNullable(mExpiryDate) + ",picturename='" + mPictureName
                + "' where code='" + mCode.Trim() + "' and membershipstatus=1 and billinggroupcode='" 
                + mGroupCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                mTrans.Commit();

                //save picture if any
                if (mPictureName.Trim() != "")
                {
                    //this.Save_Picture(mPicture, "member_" + mCode.Trim().ToLower(), mPictureExtension);
                    //mResult = this.Save_Picture(mPicture, mCode, mGroupCode);
                    this.Save_Picture(mPicture, mCode, mGroupCode.Trim());
                }

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
        public AfyaPro_Types.clsResult Delete(string mGroupCode, string mCode)
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

            #region check 4 existance
            try
            {
                mCommand.CommandText = "select * from facilitycorporatemembers where code='" + mCode.Trim()
                + "' and membershipstatus=1 and billinggroupcode='" + mGroupCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.CUS_MemberCodeDoesNotExist.ToString();
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

                mCommand.CommandText = "delete from facilitycorporatemembers where code='" + mCode.Trim() 
                + "' and membershipstatus=1 and billinggroupcode='" + mGroupCode.Trim() + "'";
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

        #region Activate_DeActivate
        public AfyaPro_Types.clsResult Activate_DeActivate(string mGroupCode, string mMemberId, Int16 mActivationFlag)
        {
            String mFunctionName = "Activate_DeActivate";

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

                mCommand.CommandText = "update facilitycorporatemembers set inactive=" + mActivationFlag
                + " where billinggroupmembershipno='" + mMemberId.Trim() 
                + "' and membershipstatus=1 and billinggroupcode='" + mGroupCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

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

        #region Terminate_Member
        public AfyaPro_Types.clsResult Terminate_Member(string mGroupCode, string mCode, DateTime mTermDate)
        {
            String mFunctionName = "Terminate_Member";

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

            #region check for code existance

            try
            {
                mCommand.CommandText = "select * from facilitycorporatemembers where code='" + mCode.Trim() 
                + "' and membershipstatus=1 and billinggroupcode='" + mGroupCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.CUS_MemberCodeDoesNotExist.ToString();
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

                mCommand.CommandText = "update facilitycorporatemembers set termdate=" + clsGlobal.Saving_DateValue(mTermDate)
                + ",membershipstatus=0 where code='" + mCode.Trim() + "' and membershipstatus=1 and billinggroupcode='" + mGroupCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

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

        #region Save_Picture
        public bool Save_Picture(Byte[] mBytes, string mCode, string mGroupCode)
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();

            String mFunctionName = "Save_Picture";

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
                string mCommandText = "SELECT * FROM facilitycorporatemembers where code='"
                + mCode.Trim() + "' and billinggroupcode='" + mGroupCode.Trim() + "'";

                DataSet mDataSet = new DataSet("Image");
                OdbcDataAdapter mDataAdapter = new OdbcDataAdapter(mCommandText, mConn);
                OdbcCommandBuilder mCommandBuilder = new OdbcCommandBuilder(mDataAdapter);
                mDataAdapter.Fill(mDataSet, "Table");

                mConn.Open();
                DataRow mDataRow = mDataSet.Tables["Table"].Rows[0];
                mDataRow["memberpicture"] = mBytes;

                mDataAdapter.Update(mDataSet, "Table");

                return true;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return false;
            }
            finally
            {
                mConn.Close();
            }
        }
        #endregion

        #region Load_Picture
        public Byte[] Load_Picture(string mCode, string mGroupCode)
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            String mFunctionName = "Load_Picture";

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
                DataTable mDtPictures = new DataTable("pictures");
                mCommand.CommandText = "select memberpicture from facilitycorporatemembers where code='" + mCode.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtPictures);

                if (mDtPictures.Rows.Count > 0)
                {
                    return (byte[])mDtPictures.Rows[0]["memberpicture"];
                }
                else
                {
                    return null;
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
    }
}
