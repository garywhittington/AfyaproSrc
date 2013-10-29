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
    public class clsLabTests : MarshalByRefObject
    {

        #region declaration

        private static String pClassName = "AfyaPro_MT.clsLabTest";

        private AfyaPro_MT.clsLabTestGroups pMdtLabtestgroups;          //holding all the lab test groups

        private DataTable pDtLabTestGroups = new DataTable("lab test groups");
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
                string mCommandText = "select * from labtests";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("labtests");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                #region add group and sub group description info

                mDataTable.Columns.Add("groupdescription", typeof(System.String));
                mDataTable.Columns.Add("subgroupdescription", typeof(System.String));

                DataTable mDtLabTestGroups = new DataTable("labtestgroups");
                mCommand.CommandText = "select * from labtestgroups";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtLabTestGroups);
                DataView mDvLabTestGroups = new DataView();
                mDvLabTestGroups.Table = mDtLabTestGroups;
                mDvLabTestGroups.Sort = "code";

                DataTable mDtLabTestSubGroups = new DataTable("labtestsubgroups");
                mCommand.CommandText = "select * from labtestsubgroups";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtLabTestSubGroups);
                DataView mDvLabTestSubGroups = new DataView();
                mDvLabTestSubGroups.Table = mDtLabTestSubGroups;
                mDvLabTestSubGroups.Sort = "code";

                foreach (DataRow mDataRow in mDataTable.Rows)
                {
                    string mGroupDescription = "";
                    string mSubGroupDescription = "";

                    int mRowIndex = mDvLabTestGroups.Find(mDataRow["groupcode"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mGroupDescription = mDvLabTestGroups[mRowIndex]["description"].ToString().Trim();
                    }

                    mRowIndex = mDvLabTestSubGroups.Find(mDataRow["subgroupcode"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mSubGroupDescription = mDvLabTestSubGroups[mRowIndex]["description"].ToString().Trim();
                    }

                    mDataRow["groupdescription"] = mGroupDescription;
                    mDataRow["subgroupdescription"] = mSubGroupDescription;
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

        #region View_DropDownValues
        public DataTable View_DropDownValues(String mFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_DropDownValues";

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
                string mCommandText = "select * from labtestdropdownvalues";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("labtestdropdownvalues");
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

        #region View_Tests
        public DataTable View_Tests(string mFilter, int mAgeYears, int mAgeMonths)
        {
            String mFunctionName = "View_Tests";

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
                DataTable mDataTable = new DataTable("labtests");

                string mCommandText = "select * from labtests";

                if (mFilter.Trim() != "")
                {
                    mCommandText = "select * from labtests where " + mFilter;
                }

                mCommand.CommandText = mCommandText;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                #region add group, subgroup and ranges

                mDataTable.Columns.Add("groupdescription", typeof(System.String));
                mDataTable.Columns.Add("subgroupdescription", typeof(System.String));
                mDataTable.Columns.Add("equipment_malelowerrange", typeof(System.Double));
                mDataTable.Columns.Add("equipment_maleupperrange", typeof(System.Double));
                mDataTable.Columns.Add("equipment_femalelowerrange", typeof(System.Double));
                mDataTable.Columns.Add("equipment_femaleupperrange", typeof(System.Double));
                mDataTable.Columns.Add("normal_malelowerrange", typeof(System.Double));
                mDataTable.Columns.Add("normal_maleupperrange", typeof(System.Double));
                mDataTable.Columns.Add("normal_femalelowerrange", typeof(System.Double));
                mDataTable.Columns.Add("normal_femaleupperrange", typeof(System.Double));

                //labtestgroups
                DataTable mDtLabTestGroups = new DataTable("labtestgroups");
                mCommand.CommandText = "select * from labtestgroups";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtLabTestGroups);
                DataView mDvLabTestGroups = new DataView();
                mDvLabTestGroups.Table = mDtLabTestGroups;
                mDvLabTestGroups.Sort = "code";

                //labtestsubgroups
                DataTable mDtLabTestSubGroups = new DataTable("labtestsubgroups");
                mCommand.CommandText = "select * from labtestsubgroups";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtLabTestSubGroups);
                DataView mDvLabTestSubGroups = new DataView();
                mDvLabTestSubGroups.Table = mDtLabTestSubGroups;
                mDvLabTestSubGroups.Sort = "code";

                //labtestranges
                DataTable mDtLabTestRanges = new DataTable("labtestranges");
                mCommand.CommandText = "select * from labtestranges where "
                    + "(age_loweryears <= " + mAgeYears + " and age_upperyears >= " + mAgeYears
                    + ") and (age_lowermonths <= " + mAgeMonths + " and age_uppermonths >= " + mAgeMonths + ")";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtLabTestRanges);
                DataView mDvLabTestRanges = new DataView();
                mDvLabTestRanges.Table = mDtLabTestRanges;
                mDvLabTestRanges.Sort = "labtesttypecode";

                foreach (DataRow mDataRow in mDataTable.Rows)
                {
                    string mGroupDescription = "";
                    string mSubGroupDescription = "";
                    double mEquipmentMaleLowerRange = 0;
                    double mEquipmentMaleUpperRange = 0;
                    double mEquipmentFemaleLowerRange = 0;
                    double mEquipmentFemaleUpperRange = 0;
                    double mNormalMaleLowerRange = 0;
                    double mNormalMaleUpperRange = 0;
                    double mNormalFemaleLowerRange = 0;
                    double mNormalFemaleUpperRange = 0;

                    //groupcode
                    int mRowIndex = mDvLabTestGroups.Find(mDataRow["groupcode"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mGroupDescription = mDvLabTestGroups[mRowIndex]["description"].ToString().Trim();
                    }
                    //subgroupcode
                    mRowIndex = mDvLabTestSubGroups.Find(mDataRow["subgroupcode"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mSubGroupDescription = mDvLabTestSubGroups[mRowIndex]["description"].ToString().Trim();
                    }
                    //ranges
                    mRowIndex = mDvLabTestRanges.Find(mDataRow["code"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mEquipmentMaleLowerRange = Convert.ToDouble(mDvLabTestRanges[mRowIndex]["equipment_malelowerrange"]);
                        mEquipmentMaleUpperRange = Convert.ToDouble(mDvLabTestRanges[mRowIndex]["equipment_maleupperrange"]);
                        mEquipmentFemaleLowerRange = Convert.ToDouble(mDvLabTestRanges[mRowIndex]["equipment_femalelowerrange"]);
                        mEquipmentFemaleUpperRange = Convert.ToDouble(mDvLabTestRanges[mRowIndex]["equipment_femaleupperrange"]);
                        mNormalMaleLowerRange = Convert.ToDouble(mDvLabTestRanges[mRowIndex]["normal_malelowerrange"]);
                        mNormalMaleUpperRange = Convert.ToDouble(mDvLabTestRanges[mRowIndex]["normal_maleupperrange"]);
                        mNormalFemaleLowerRange = Convert.ToDouble(mDvLabTestRanges[mRowIndex]["normal_femalelowerrange"]);
                        mNormalFemaleUpperRange = Convert.ToDouble(mDvLabTestRanges[mRowIndex]["normal_femaleupperrange"]);
                    }

                    mDataRow.BeginEdit();
                    mDataRow["groupdescription"] = mGroupDescription;
                    mDataRow["subgroupdescription"] = mSubGroupDescription;
                    mDataRow["equipment_malelowerrange"] = mEquipmentMaleLowerRange;
                    mDataRow["equipment_maleupperrange"] = mEquipmentMaleUpperRange;
                    mDataRow["equipment_femalelowerrange"] = mEquipmentFemaleLowerRange;
                    mDataRow["equipment_femaleupperrange"] = mEquipmentFemaleUpperRange;
                    mDataRow["normal_malelowerrange"] = mNormalMaleLowerRange;
                    mDataRow["normal_maleupperrange"] = mNormalMaleUpperRange;
                    mDataRow["normal_femalelowerrange"] = mNormalFemaleLowerRange;
                    mDataRow["normal_femaleupperrange"] = mNormalFemaleUpperRange;
                    mDataRow.EndEdit();
                    mDataTable.AcceptChanges();
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

        #region View_Ranges
        public DataTable View_Ranges(String mFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_Ranges";

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
                string mCommandText = "select * from labtestranges";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("labtestranges");
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
        public AfyaPro_Types.clsResult Add(Int16 mGenerateCode, String mCode, String mDescription, string mDisplayName,
            string mGroupcode, string mSubGroupCode, int mResulttype, int mRejectOutOfRange, string mUnits,
            DataTable mDtDropDownValues, DataTable mDtRanges, int mRestrictToDropDownList, int mAdditionalInfo, string mUserId)
        {
            String mFunctionName = "Add";

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.labtest_add.ToString(), mUserId);
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
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.labtestcode), "labtests", "code");
                if (mObjCode.Exe_Result == -1)
                {
                    mResult.Exe_Result = mObjCode.Exe_Result;
                    mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, mObjCode.Exe_Message);
                    return mResult;
                }
                mCode = mObjCode.GeneratedCode;
            }

            #endregion

            #region check 4 duplicate
            try
            {
                mCommand.CommandText = "select * from labtests where code='"
                + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.LAB_LabTestCodeIsInUse.ToString();
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

                //labtests
                if (mDisplayName.Trim() == "")
                {
                    mDisplayName = mDescription;
                }

                List<clsDataField> mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("code", DataTypes.dbstring.ToString(), mCode.Trim()));
                mDataFields.Add(new clsDataField("description", DataTypes.dbstring.ToString(), mDescription.Trim()));
                mDataFields.Add(new clsDataField("displayname", DataTypes.dbstring.ToString(), mDisplayName.Trim()));
                mDataFields.Add(new clsDataField("groupcode", DataTypes.dbstring.ToString(), mGroupcode.Trim()));
                mDataFields.Add(new clsDataField("subgroupcode", DataTypes.dbstring.ToString(), mSubGroupCode.Trim()));
                mDataFields.Add(new clsDataField("resulttype", DataTypes.dbnumber.ToString(), mResulttype));
                mDataFields.Add(new clsDataField("rejectoutofrange", DataTypes.dbnumber.ToString(), mRejectOutOfRange));
                mDataFields.Add(new clsDataField("restricttodropdownlist", DataTypes.dbnumber.ToString(), mRestrictToDropDownList));
                mDataFields.Add(new clsDataField("additionalinfo", DataTypes.dbnumber.ToString(), mAdditionalInfo));
                mDataFields.Add(new clsDataField("units", DataTypes.dbstring.ToString(), mUnits.Trim()));

                mCommand.CommandText = clsGlobal.Get_InsertStatement("labtests", mDataFields);
                mCommand.ExecuteNonQuery();

                #region labtestdropdownvalues

                if (mResulttype == (int)AfyaPro_Types.clsEnums.LabTestResultTypes.dropdown)
                {
                    mCommand.CommandText = clsGlobal.Get_DeleteStatement("labtestdropdownvalues", "labtesttypecode='" + mCode.Trim() + "'");
                    mCommand.ExecuteNonQuery();

                    foreach (DataRow mDataRow in mDtDropDownValues.Rows)
                    {
                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("labtesttypecode", DataTypes.dbstring.ToString(), mCode.Trim()));
                        mDataFields.Add(new clsDataField("description", DataTypes.dbstring.ToString(), mDataRow["description"].ToString().Trim()));

                        mCommand.CommandText = clsGlobal.Get_InsertStatement("labtestdropdownvalues", mDataFields);
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region labtestranges

                mCommand.CommandText = clsGlobal.Get_DeleteStatement("labtestranges", "labtesttypecode='" + mCode.Trim() + "'");
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtRanges.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("labtesttypecode", DataTypes.dbstring.ToString(), mCode.Trim()));
                    mDataFields.Add(new clsDataField("age_loweryears", DataTypes.dbnumber.ToString(), mDataRow["age_loweryears"]));
                    mDataFields.Add(new clsDataField("age_lowermonths", DataTypes.dbnumber.ToString(), mDataRow["age_lowermonths"]));
                    mDataFields.Add(new clsDataField("age_upperyears", DataTypes.dbnumber.ToString(), mDataRow["age_upperyears"]));
                    mDataFields.Add(new clsDataField("age_uppermonths", DataTypes.dbnumber.ToString(), mDataRow["age_uppermonths"]));
                    mDataFields.Add(new clsDataField("equipment_malelowerrange", DataTypes.dbdecimal.ToString(), mDataRow["equipment_malelowerrange"]));
                    mDataFields.Add(new clsDataField("equipment_maleupperrange", DataTypes.dbdecimal.ToString(), mDataRow["equipment_maleupperrange"]));
                    mDataFields.Add(new clsDataField("normal_malelowerrange", DataTypes.dbdecimal.ToString(), mDataRow["normal_malelowerrange"]));
                    mDataFields.Add(new clsDataField("normal_maleupperrange", DataTypes.dbdecimal.ToString(), mDataRow["normal_maleupperrange"]));
                    mDataFields.Add(new clsDataField("equipment_femalelowerrange", DataTypes.dbdecimal.ToString(), mDataRow["equipment_femalelowerrange"]));
                    mDataFields.Add(new clsDataField("equipment_femaleupperrange", DataTypes.dbdecimal.ToString(), mDataRow["equipment_femaleupperrange"]));
                    mDataFields.Add(new clsDataField("normal_femalelowerrange", DataTypes.dbdecimal.ToString(), mDataRow["normal_femalelowerrange"]));
                    mDataFields.Add(new clsDataField("normal_femaleupperrange", DataTypes.dbdecimal.ToString(), mDataRow["normal_femaleupperrange"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("labtestranges", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                if (mGenerateCode == 1)
                {
                    mCommand.CommandText = "update facilityautocodes set "
                    + "idcurrent=idcurrent+idincrement where codekey="
                    + Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.labtestcode);
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
        public AfyaPro_Types.clsResult Edit(String mCode, String mDescription, string mDisplayName,
            string mGroupcode, string mSubGroupCode, int mResulttype, int mRejectOutOfRange, string mUnits,
            DataTable mDtDropDownValues, DataTable mDtRanges, int mRestrictToDropDownList, int mAdditionalInfo, string mUserId)
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.labtest_edit.ToString(), mUserId);
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
                mCommand.CommandText = "select * from labtests where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.LAB_LabTestCodeDoesNotExist.ToString();
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

                //labtests
                if (mDisplayName.Trim() == "")
                {
                    mDisplayName = mDescription;
                }

                List<clsDataField> mDataFields = new List<clsDataField>();
                mDataFields.Add(new clsDataField("description", DataTypes.dbstring.ToString(), mDescription.Trim()));
                mDataFields.Add(new clsDataField("displayname", DataTypes.dbstring.ToString(), mDisplayName.Trim()));
                mDataFields.Add(new clsDataField("groupcode", DataTypes.dbstring.ToString(), mGroupcode.Trim()));
                mDataFields.Add(new clsDataField("subgroupcode", DataTypes.dbstring.ToString(), mSubGroupCode.Trim()));
                mDataFields.Add(new clsDataField("resulttype", DataTypes.dbnumber.ToString(), mResulttype));
                mDataFields.Add(new clsDataField("rejectoutofrange", DataTypes.dbnumber.ToString(), mRejectOutOfRange));
                mDataFields.Add(new clsDataField("restricttodropdownlist", DataTypes.dbnumber.ToString(), mRestrictToDropDownList));
                mDataFields.Add(new clsDataField("additionalinfo", DataTypes.dbnumber.ToString(), mAdditionalInfo));
                mDataFields.Add(new clsDataField("units", DataTypes.dbstring.ToString(), mUnits.Trim()));

                mCommand.CommandText = clsGlobal.Get_UpdateStatement("labtests", mDataFields, "code='" + mCode.Trim() + "'");
                mCommand.ExecuteNonQuery();

                #region labtestdropdownvalues

                if (mResulttype == (int)AfyaPro_Types.clsEnums.LabTestResultTypes.dropdown)
                {
                    mCommand.CommandText = clsGlobal.Get_DeleteStatement("labtestdropdownvalues", "labtesttypecode='" + mCode.Trim() + "'");
                    mCommand.ExecuteNonQuery();

                    foreach (DataRow mDataRow in mDtDropDownValues.Rows)
                    {
                        mDataFields = new List<clsDataField>();
                        mDataFields.Add(new clsDataField("labtesttypecode", DataTypes.dbstring.ToString(), mCode.Trim()));
                        mDataFields.Add(new clsDataField("description", DataTypes.dbstring.ToString(), mDataRow["description"].ToString().Trim()));

                        mCommand.CommandText = clsGlobal.Get_InsertStatement("labtestdropdownvalues", mDataFields);
                        mCommand.ExecuteNonQuery();
                    }
                }

                #endregion

                #region labtestranges

                mCommand.CommandText = clsGlobal.Get_DeleteStatement("labtestranges", "labtesttypecode='" + mCode.Trim() + "'");
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtRanges.Rows)
                {
                    mDataFields = new List<clsDataField>();
                    mDataFields.Add(new clsDataField("labtesttypecode", DataTypes.dbstring.ToString(), mCode.Trim()));
                    mDataFields.Add(new clsDataField("age_loweryears", DataTypes.dbnumber.ToString(), mDataRow["age_loweryears"]));
                    mDataFields.Add(new clsDataField("age_lowermonths", DataTypes.dbnumber.ToString(), mDataRow["age_lowermonths"]));
                    mDataFields.Add(new clsDataField("age_upperyears", DataTypes.dbnumber.ToString(), mDataRow["age_upperyears"]));
                    mDataFields.Add(new clsDataField("age_uppermonths", DataTypes.dbnumber.ToString(), mDataRow["age_uppermonths"]));
                    mDataFields.Add(new clsDataField("equipment_malelowerrange", DataTypes.dbdecimal.ToString(), mDataRow["equipment_malelowerrange"]));
                    mDataFields.Add(new clsDataField("equipment_maleupperrange", DataTypes.dbdecimal.ToString(), mDataRow["equipment_maleupperrange"]));
                    mDataFields.Add(new clsDataField("normal_malelowerrange", DataTypes.dbdecimal.ToString(), mDataRow["normal_malelowerrange"]));
                    mDataFields.Add(new clsDataField("normal_maleupperrange", DataTypes.dbdecimal.ToString(), mDataRow["normal_maleupperrange"]));
                    mDataFields.Add(new clsDataField("equipment_femalelowerrange", DataTypes.dbdecimal.ToString(), mDataRow["equipment_femalelowerrange"]));
                    mDataFields.Add(new clsDataField("equipment_femaleupperrange", DataTypes.dbdecimal.ToString(), mDataRow["equipment_femaleupperrange"]));
                    mDataFields.Add(new clsDataField("normal_femalelowerrange", DataTypes.dbdecimal.ToString(), mDataRow["normal_femalelowerrange"]));
                    mDataFields.Add(new clsDataField("normal_femaleupperrange", DataTypes.dbdecimal.ToString(), mDataRow["normal_femaleupperrange"]));

                    mCommand.CommandText = clsGlobal.Get_InsertStatement("labtestranges", mDataFields);
                    mCommand.ExecuteNonQuery();
                }

                #endregion

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.rpdreportgroups_delete.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region check 4 existance
            try
            {
                mCommand.CommandText = "select * from labtests where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.LAB_LabTestCodeDoesNotExist.ToString();
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

            #region check if is in use
            try
            {
                mCommand.CommandText = "select * from labpatienttests where labtesttypecode='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.LAB_LabTestCodeIsInUse.ToString();
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

                //labtests
                mCommand.CommandText = clsGlobal.Get_DeleteStatement("labtests", "code='" + mCode.Trim() + "'");
                mCommand.ExecuteNonQuery();

                //labtestdropdownvalues
                mCommand.CommandText = clsGlobal.Get_DeleteStatement("labtestdropdownvalues", "labtesttypecode='" + mCode.Trim() + "'");
                mCommand.ExecuteNonQuery();

                //labtestranges
                mCommand.CommandText = clsGlobal.Get_DeleteStatement("labtestranges", "labtesttypecode='" + mCode.Trim() + "'");
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
    }

}
