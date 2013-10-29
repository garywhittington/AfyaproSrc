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
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Linq;

namespace AfyaPro_MT
{
    public class clsFacilitySetup : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsFacilitySetup";

        #endregion

        #region Get_FacilityTypes
        public DataTable Get_FacilityTypes(string mLanguageName, string mGridName)
        {
            String mFunctionName = "Get_FacilityTypes";

            try
            {
                DataTable mDataTable = new DataTable("facilitytypes");
                mDataTable.Columns.Add("code", typeof(System.String));
                mDataTable.Columns.Add("description", typeof(System.String));
                mDataTable.RemotingFormat = SerializationFormat.Binary;

                DataRow mNewRow;

                #region load searchobject

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "facilitytype" + (int)AfyaPro_Types.clsEnums.FacilityTypes.Dispensary;
                mNewRow["description"] = "Dispensary";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "facilitytype" + (int)AfyaPro_Types.clsEnums.FacilityTypes.HealthCentre;
                mNewRow["description"] = "Health Centre";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "facilitytype" + (int)AfyaPro_Types.clsEnums.FacilityTypes.DistrictHospital;
                mNewRow["description"] = "District Hospital";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "facilitytype" + (int)AfyaPro_Types.clsEnums.FacilityTypes.RegionalHospital;
                mNewRow["description"] = "Regional Hospital";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "facilitytype" + (int)AfyaPro_Types.clsEnums.FacilityTypes.NationalHospital;
                mNewRow["description"] = "National Hospital";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "facilitytype" + (int)AfyaPro_Types.clsEnums.FacilityTypes.ReferalHospital;
                mNewRow["description"] = "Referal Hospital";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "facilitytype" + (int)AfyaPro_Types.clsEnums.FacilityTypes.Laboratory;
                mNewRow["description"] = "Laboratory";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                mNewRow = mDataTable.NewRow();
                mNewRow["code"] = "facilitytype" + (int)AfyaPro_Types.clsEnums.FacilityTypes.RCH;
                mNewRow["description"] = "RCH";
                mDataTable.Rows.Add(mNewRow);
                mDataTable.AcceptChanges();

                #endregion

                #region interprete data and column headers

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

        #region View
        public DataTable View(String mFilter, String mOrder)
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
                string mCommandText = "select * from facilityoptions";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("facilityoptions");
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

        #region Get_FormSizes
        public DataTable Get_FormSizes(String mFilter, String mOrder)
        {
            String mFunctionName = "Get_FormSizes";

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
                string mCommandText = "select formname,formwidth,formheight,layoutname from sys_formsizes";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("formsizes");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                if (mDataTable.Rows.Count == 0)
                {
                    mCommand.CommandText = "select formname,formwidth,formheight,layoutname from sys_formsizes "
                    + "where layoutname='defaultsettings'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDataTable);
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

        #region Edit
        public AfyaPro_Types.clsResult Edit(String mCode, String mDescription, String mBox, String mStreet, String mTeleNo,
            String mRegionCode, String mDistrictCode, String mCountryCode, String mHeadName, String mHeadDesignation,
            int mFaciltyType, int mRoundingOption, int mRoundingFigure, int mRoundingDecimals,
            int mRoundingStrictness, int mRoundingMidpointOption, int mAffectStockAtCashier, int mDoubleEntryIssuing,
            int mTransferOutRefreshInterval, int mCharacterCasingOptionPatientNames, string mUserId)
        {
            String mFunctionName = "Edit";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;
            Boolean mDataFound = false;

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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.genfacilitysetup_edit.ToString(), mUserId);
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
                mCommand.CommandText = "select * from facilityoptions";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mDataFound = true;
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

                //facilityoptions
                if (mDataFound == false)
                {
                    mCommand.CommandText = "insert into facilityoptions(facilitycode,facilitydescription) values('001','DEMO HOSPITAL')";
                    mCommand.ExecuteNonQuery();
                }

                mCommand.CommandText = "update facilityoptions set facilitycode='" + mCode.Trim() + "', facilitydescription = '"
                    + mDescription.Trim() + "',box='" + mBox + "',street='" + mStreet + "',teleno='" + mTeleNo + "',regioncode='"
                    + mRegionCode.Trim() + "',districtcode='" + mDistrictCode.Trim() + "',countrycode='" + mCountryCode.Trim()
                    + "',headname='" + mHeadName + "',headdesignation='" + mHeadDesignation + "',facilitytype=" + mFaciltyType
                    + ",roundingoption=" + mRoundingOption + ",roundingfigure=" + mRoundingFigure + ",roundingdecimals="
                    + mRoundingDecimals + ",roundingstrictness=" + mRoundingStrictness + ",roundingmidpointoption="
                    + mRoundingMidpointOption + ",affectstockatcashier=" + mAffectStockAtCashier
                    + ",doubleentryissuing=" + mDoubleEntryIssuing + ",transferoutrefreshinterval=" + mTransferOutRefreshInterval
                    + ",charactercasingoptionpatientnames=" + mCharacterCasingOptionPatientNames;
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

        #region Initialize_English
        public AfyaPro_Types.clsResult Initialize_English(DataTable mDtEnglish)
        {
            String mFunctionName = "Initialize_English";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
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

            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                DataTable mDtLang = new DataTable("lang");
                mCommand.CommandText = "select * from englishlanguage";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtLang);
                DataView mDvLang = new DataView();
                mDvLang.Table = mDtLang;
                mDvLang.Sort = "controlname";

                foreach (DataRow mDataRow in mDtEnglish.Rows)
                {
                    if (mDvLang.Find(mDataRow["controlname"].ToString().Trim()) < 0)
                    {
                        mCommand.CommandText = "insert into englishlanguage(objecttype,controlname,description) "
                        + "values('" + mDataRow["objecttype"].ToString() + "','" + mDataRow["controlname"].ToString()
                        + "','" + mDataRow["description"].ToString() + "')";
                        mCommand.ExecuteNonQuery();
                    }
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
        }
        #endregion

        #region Get_DbDateQuote
        public String Get_DbDateQuote()
        {
            return clsGlobal.gDbDateQuote;
        }
        #endregion

        #region Get_ServerDate
        public DateTime Get_ServerDate()
        {
            return DateTime.Now;
        }
        #endregion

        #region Saving_DateValue
        public String Saving_DateValue(DateTime mDateValue)
        {
            String mDateString = "";
            mDateString = mDateValue.ToString("d", clsGlobal.gCulture);
            mDateString = clsGlobal.gDbDateQuote + mDateString + clsGlobal.gDbDateQuote;

            return mDateString;
        }
        #endregion

        #region Get_SavingDateFormat
        public String Get_SavingDateFormat()
        {
            String mDateFormat = "";
            String mFunctionName = "Get_SavingDateFormat";

            try
            {
                switch (clsGlobal.gAfyaPro_ServerType)
                {
                    case 0:
                        {
                            mDateFormat = "yyyy-MM-dd HH:mm";
                        }
                        break;
                    case 1:
                        {
                            mDateFormat = "MM-dd-yyyy HH:mm";
                        }
                        break;
                }

                return mDateFormat;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return "";
            }
        }
        #endregion

        #region Get_DefinedFormLayouts
        public DataTable Get_DefinedFormLayouts()
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            string mFunctionName = "Get_DefinedFormLayouts";

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
                DataTable mDtFormlayouts = new DataTable("formlayouts");
                mCommand.CommandText = "select distinct(layoutname) description from sys_formsizes";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtFormlayouts);

                return mDtFormlayouts;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
        }
        #endregion

        #region Save_FormLayout
        public bool Save_FormLayout(string mFormName, string mLayoutName, byte[] mBytes, int mFormHeight, int mFormWidth)
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();

            String mFunctionName = "Save_FormLayout";

            #region database connection

            try
            {
                mConn.ConnectionString = clsGlobal.gAfyaConStr;
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
                DataTable mDtFormSizes = new DataTable("formsizes");
                string mCommandText = "SELECT * FROM sys_formsizes where formname='"
                + mFormName.Trim() + "' and layoutname='" + mLayoutName.Trim() + "'";
                OdbcDataAdapter mDataAdapter1 = new OdbcDataAdapter(mCommandText, mConn);
                mDataAdapter1.Fill(mDtFormSizes);
                if (mDtFormSizes.Rows.Count == 0)
                {
                    mConn.Open();
                    mCommand.CommandText = "insert into sys_formsizes(formname,layoutname)"
                    + " values('" + mFormName.Trim() + "','" + mLayoutName.Trim() + "')";
                    mCommand.ExecuteNonQuery();
                    mConn.Close();
                }

                mCommandText = "SELECT * FROM sys_formsizes where formname='"
                + mFormName.Trim() + "' and layoutname='" + mLayoutName.Trim() + "'";
                DataSet mDataSet = new DataSet("Image");
                OdbcDataAdapter mDataAdapter = new OdbcDataAdapter(mCommandText, mConn);
                OdbcCommandBuilder mCommandBuilder = new OdbcCommandBuilder(mDataAdapter);
                mDataAdapter.Fill(mDataSet, "Table");

                try
                {
                    mConn.Open();
                }
                catch { }
                DataRow mDataRow = mDataSet.Tables["Table"].Rows[0];
                mDataRow["formlayout"] = mBytes;
                mDataRow["formwidth"] = mFormWidth;
                mDataRow["formheight"] = mFormHeight;

                mDataAdapter.Update(mDataSet, "Table");

                return true;
            }
            catch (Exception ex)
            {
                //clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return false;
            }
            finally
            {
                try
                {
                    mConn.Close();
                }
                catch { }
            }
        }
        #endregion

        #region Load_FormLayout
        public Byte[] Load_FormLayout(string mFormName, string mLayoutName)
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            String mFunctionName = "Load_FormLayout";

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
                DataTable mDtLayouts = new DataTable("formlayouts");
                mCommand.CommandText = "select formlayout from sys_formsizes where formname='" 
                + mFormName.Trim() + "' and layoutname='" +mLayoutName.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtLayouts);

                if (mDtLayouts.Rows.Count == 0)
                {
                    mDtLayouts = new DataTable("formlayouts");
                    mCommand.CommandText = "select formlayout from sys_formsizes where formname='"
                    + mFormName.Trim() + "' and layoutname='defaultsettings'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtLayouts);
                }

                if (mDtLayouts.Rows.Count > 0)
                {
                    return (byte[])mDtLayouts.Rows[0]["formlayout"];
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

        #region Save_GridLayout
        public bool Save_GridLayout(string mGridName, string mUserId, byte[] mBytes)
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();

            String mFunctionName = "Save_GridLayout";

            #region database connection

            try
            {
                mConn.ConnectionString = clsGlobal.gAfyaConStr;
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
                DataTable mDtGridLayouts = new DataTable("gridlayouts");
                string mCommandText = "SELECT * FROM sys_gridlayouts where gridname='"
                + mGridName.Trim() + "' and userid='" + mUserId.Trim() + "'";
                OdbcDataAdapter mDataAdapter1 = new OdbcDataAdapter(mCommandText, mConn);
                mDataAdapter1.Fill(mDtGridLayouts);
                if (mDtGridLayouts.Rows.Count == 0)
                {
                    mConn.Open();
                    mCommand.CommandText = "insert into sys_gridlayouts(gridname,userid)"
                    + " values('" + mGridName.Trim() + "','" + mUserId.Trim() + "')";
                    mCommand.ExecuteNonQuery();
                    mConn.Close();
                }

                mCommandText = "SELECT * FROM sys_gridlayouts where gridname='"
                + mGridName.Trim() + "' and userid='" + mUserId.Trim() + "'";
                DataSet mDataSet = new DataSet("Image");
                OdbcDataAdapter mDataAdapter = new OdbcDataAdapter(mCommandText, mConn);
                OdbcCommandBuilder mCommandBuilder = new OdbcCommandBuilder(mDataAdapter);
                mDataAdapter.Fill(mDataSet, "Table");

                try
                {
                    mConn.Open();
                }
                catch { }
                DataRow mDataRow = mDataSet.Tables["Table"].Rows[0];
                mDataRow["gridlayout"] = mBytes;

                mDataAdapter.Update(mDataSet, "Table");

                return true;
            }
            catch (Exception ex)
            {
                //clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return false;
            }
            finally
            {
                try
                {
                    mConn.Close();
                }
                catch { }
            }
        }
        #endregion

        #region Load_GridLayout
        public Byte[] Load_GridLayout(string mGridName, string mUserId)
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            String mFunctionName = "Load_GridLayout";

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
                DataTable mDtLayouts = new DataTable("formlayouts");
                mCommand.CommandText = "select gridlayout from sys_gridlayouts where gridname='"
                + mGridName.Trim() + "' and userid='" + mUserId.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtLayouts);

                if (mDtLayouts.Rows.Count == 0)
                {
                    mDtLayouts = new DataTable("formlayouts");
                    mCommand.CommandText = "select gridlayout from sys_gridlayouts where gridname='"
                    + mGridName.Trim() + "' and userid='admin'";
                    mDataAdapter.SelectCommand = mCommand;
                    mDataAdapter.Fill(mDtLayouts);
                }

                try
                {
                    if (mDtLayouts.Rows.Count > 0)
                    {
                        return (byte[])mDtLayouts.Rows[0]["gridlayout"];
                    }
                    else
                    {
                        return null;
                    }
                }
                catch { return null; }
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

        #region Save_UserSettings
        public AfyaPro_Types.clsResult Save_UserSettings(string mMachineName, int mMainNavBarWidth, string mDefaultSkinName, 
            string mDefaultLanguage, string mUserId)
        {
            FileStream mFileStream = null;
            string mFunctionName = "Save_UserSettings";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
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

            #region do edit
            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;
                
                //sys_workstations
                DataTable mDtWorkstations = new DataTable("sys_workstations");
                mCommand.CommandText = "select * from sys_workstations where machinename='" + mMachineName.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtWorkstations);
                if (mDtWorkstations.Rows.Count == 0)
                {
                    mCommand.CommandText = "insert into sys_workstations(machinename,defaultskinname,defaultlanguage) values('"
                    + mMachineName.Trim() + "','" + mDefaultSkinName.Trim() + "','" + mDefaultLanguage.Trim() + "')";
                    mCommand.ExecuteNonQuery();
                }
                else
                {
                    mCommand.CommandText = "update sys_workstations set defaultlanguage='" + mDefaultLanguage.Trim()
                    + "',defaultskinname='" + mDefaultSkinName.Trim() + "' where machinename='" + mMachineName.Trim() + "'";
                    mCommand.ExecuteNonQuery();
                }

                //sys_users
                mCommand.CommandText = "update sys_users set defaultlanguage='" + mDefaultLanguage.Trim()
                + "',defaultskinname='" + mDefaultSkinName.Trim() + "',mainnavbarwidth=" + mMainNavBarWidth
                + " where code='" + mUserId.Trim() + "'";
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
                try
                {
                    mFileStream.Close();
                }
                catch { }
            }
            #endregion
        }
        #endregion

        #region Get_UserSettings
        public DataTable Get_UserSettings(string mMachineName, string mUserId)
        {
            string mFunctionName = "Get_UserSettings";

            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            DataTable mDtUserSettings = new DataTable("usersettings");

            mDtUserSettings.Columns.Add("defaultlanguage", typeof(System.String));
            mDtUserSettings.Columns.Add("defaultskinname", typeof(System.String));
            mDtUserSettings.Columns.Add("userdefaultlanguage", typeof(System.String));
            mDtUserSettings.Columns.Add("userdefaultskinname", typeof(System.String));
            mDtUserSettings.Columns.Add("usermainnavbarwidth", typeof(System.Int16));

            DataRow mNewRow = mDtUserSettings.NewRow();
            mNewRow["defaultlanguage"] = "English";
            mNewRow["defaultskinname"] = "Office 2007 Green";
            mNewRow["userdefaultlanguage"] = "English";
            mNewRow["userdefaultskinname"] = "Office 2007 Green";
            mNewRow["usermainnavbarwidth"] = 211;
            mDtUserSettings.Rows.Add(mNewRow);
            mDtUserSettings.AcceptChanges();

            try
            {
                #region database connection

                mConn.ConnectionString = clsGlobal.gAfyaConStr;

                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                mCommand.Connection = mConn;

                #endregion

                DataRow mEditRow = mDtUserSettings.Rows[0];
                mEditRow.BeginEdit();

                //sys_users
                DataTable mDtUsers = new DataTable("sys_users");
                mCommand.CommandText = "select * from sys_users where code='" + mUserId.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtUsers);
                if (mDtUsers.Rows.Count > 0)
                {
                    if (mDtUsers.Rows[0]["defaultlanguage"].ToString().Trim() != "")
                    {
                        mEditRow["userdefaultlanguage"] = mDtUsers.Rows[0]["defaultlanguage"].ToString().Trim();
                        mEditRow["userdefaultskinname"] = mDtUsers.Rows[0]["defaultskinname"].ToString().Trim();
                        mEditRow["usermainnavbarwidth"] = Convert.ToInt16(mDtUsers.Rows[0]["mainnavbarwidth"]);
                    }
                }

                //sys_workstations
                DataTable mDtMachines = new DataTable("sys_workstations");
                mCommand.CommandText = "select * from sys_workstations where machinename='" + mMachineName.Trim() + "'";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtMachines);

                if (mDtMachines.Rows.Count > 0)
                {
                    if (mDtMachines.Rows[0]["defaultlanguage"].ToString().Trim() != "")
                    {
                        mEditRow["defaultlanguage"] = mDtMachines.Rows[0]["defaultlanguage"].ToString().Trim();
                        mEditRow["defaultskinname"] = mDtMachines.Rows[0]["defaultskinname"].ToString().Trim();
                    }
                }

                mEditRow.EndEdit();
                mDtUserSettings.AcceptChanges();

                return mDtUserSettings;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mDtUserSettings;
            }
            finally
            {
                mConn.Close();
            }
        }
        #endregion

        #region Add_UserLogin
        public AfyaPro_Types.clsResult Add_UserLogin(string mMachineName, string mUserId, DateTime mLoginTime, int mProcessId)
        {
            FileStream mFileStream = null;
            string mFunctionName = "Add_UserLogin";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
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

            #region add
            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //sys_userlogin
                DataTable mDtUserLogin = new DataTable("sys_userlogin");

                mCommand.CommandText = "insert into sys_userlogin(machinename,usercode,logintime,processid) values('"
                + mMachineName.Trim() + "','" + mUserId.Trim() + "'," + clsGlobal.Saving_DateValueNullable(mLoginTime) + ",'" + mProcessId + "')";
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
                try
                {
                    mFileStream.Close();
                }
                catch { }
            }
            #endregion

        }
        #endregion

        #region Add_UserLogin
        public AfyaPro_Types.clsResult Edit_UserLogin(string mMachineName, string mUserId, DateTime mLoginTime, int mProcessId)
        {
            FileStream mFileStream = null;
            string mFunctionName = "Add_UserLogin";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            OdbcTransaction mTrans = null;
            OdbcDataReader mDataReader = null;

            DateTime mEndDateTime = Get_ServerDate();

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

            #region check whether the data exists

            try
            {
                mCommand.CommandText = "select * from sys_userlogin where processid='" + mProcessId 
                    + "' and machinename='" + mMachineName
                    + "' and logintime=" + clsGlobal.Saving_DateValueNullable(mLoginTime);
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = "User Log in information is not available";
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
              
                mCommand.CommandText = "update sys_userlogin set logouttime = " + clsGlobal.Saving_DateValueNullable(mEndDateTime)
                + " where processid='" + mProcessId + "' and machinename='" + mMachineName
                + "' and logintime=" + clsGlobal.Saving_DateValueNullable(mLoginTime);
                
                
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
                try
                {
                    mFileStream.Close();
                }
                catch { }
            }
            
            #endregion
        }
        #endregion
    }
}
