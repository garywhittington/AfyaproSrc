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
using System.Xml;
using System.IO;

namespace AfyaPro_MT
{
    public class clsMtuhaDiagnoses : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsMtuhaDiagnoses";

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
                string mCommandText = "select * from dxtmtuhadiagnoses";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("dxtmtuhadiagnoses");
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

        #region View_Mapping
        public DataTable View_Mapping(String mFilter, String mOrder, string mLanguageName, string mGridName)
        {
            String mFunctionName = "View_Mapping";

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
                string mCommandText = "select * from dxtmtuhadiagnosesmapping";

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("dxtmtuhadiagnosesmapping");
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
        public AfyaPro_Types.clsResult Add(string mCode, String mDescription, string mUserId)
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.mtuhadiagnoses_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region check 4 duplicate
            try
            {
                mCommand.CommandText = "select * from dxtmtuhadiagnoses where code='"
                + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.MTU_DiagnosisCodeIsInUse.ToString();
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

                //dxtmtuhadiagnoses
                mCommand.CommandText = "insert into dxtmtuhadiagnoses(code,description) values('"
                + mCode.Trim() + "','" + mDescription.Trim() + "')";
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

        #region Edit
        public AfyaPro_Types.clsResult Edit(String mCode, String mDescription, string mUserId)
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.mtuhadiagnoses_edit.ToString(), mUserId);
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
                mCommand.CommandText = "select * from dxtmtuhadiagnoses where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.MTU_DiagnosisCodeDoesNotExist.ToString();
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

                mCommand.CommandText = "update dxtmtuhadiagnoses set description = '"
                + mDescription.Trim() + "' where code='" + mCode.Trim() + "'";
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
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.mtuhadiagnoses_delete.ToString(), mUserId);
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
                mCommand.CommandText = "select * from dxtmtuhadiagnoses where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.MTU_DiagnosisCodeDoesNotExist.ToString();
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
                mCommand.CommandText = "select * from dxtmtuhadiagnosesmapping where mtuhacode='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.MTU_DiagnosisCodeIsInUse.ToString();
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

                mCommand.CommandText = "delete from dxtmtuhadiagnoses where code='" + mCode.Trim() + "'";
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

        #region Mtuha_Diagnoses_Mapping
        public AfyaPro_Types.clsResult Mtuha_Diagnoses_Mapping(string mMtuhaCode, string mMtuhaDescription,
            DataTable mDtDiagnoses, string mUserId)
        {
            String mFunctionName = "Mtuha_Diagnoses_Mapping";

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
                mCommand.CommandText = "select * from dxtmtuhadiagnoses where code='" + mMtuhaCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.MTU_DiagnosisCodeDoesNotExist.ToString();
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

            #region do stuff
            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                #region mapping

                mCommand.CommandText = "delete from dxtmtuhadiagnosesmapping where mtuhacode='" + mMtuhaCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtDiagnoses.Rows)
                {
                    mCommand.CommandText = "insert into dxtmtuhadiagnosesmapping(mtuhacode,diagnosiscode) "
                    + "values('" + mMtuhaCode.Trim() + "','" + mDataRow["code"].ToString().Trim() + "')";
                    mCommand.ExecuteNonQuery();
                }

                #endregion

                #region synchronize

                mCommand.CommandText = "update dxtpatientdiagnoseslog set mtuhadiagnosiscode='',"
                + "mtuhadiagnosisdescription='' where mtuhadiagnosiscode='" + mMtuhaCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                foreach (DataRow mDataRow in mDtDiagnoses.Rows)
                {
                    mCommand.CommandText = "update dxtpatientdiagnoseslog set mtuhadiagnosiscode='"
                    + mMtuhaCode.Trim() + "',mtuhadiagnosisdescription='" + mMtuhaDescription.Trim()
                    + "' where diagnosiscode='" + mDataRow["code"].ToString().Trim() + "'";
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

        #region Export_To_DHIS
        public byte[] Export_To_DHIS(DateTime mDateFrom, DateTime mDateTo)
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            string mFunctionName = "Export_To_DHIS";

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
                mDateFrom = mDateFrom.Date;
                mDateTo = mDateTo.Date;

                //Prepare XML Document
                XmlDocument xmlDoc = new XmlDocument();
                XmlNode nodeDeclaration, nodeTitle, nodeParent, nodeChild, nodeData;
                XmlNode nodeParentInner, nodeChildInner, nodeDataInner;

                nodeDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlDoc.AppendChild(nodeDeclaration);

                nodeTitle = xmlDoc.CreateElement("dxf");

                #region Insert constants

                #region Category Options
                nodeParent = xmlDoc.CreateElement("categoryOptions");
                nodeChild = xmlDoc.CreateElement("categoryOption");

                nodeData = xmlDoc.CreateElement("id");
                nodeData.AppendChild(xmlDoc.CreateTextNode("1"));
                nodeChild.AppendChild(nodeData);

                nodeData = xmlDoc.CreateElement("name");
                nodeData.AppendChild(xmlDoc.CreateTextNode("default"));
                nodeChild.AppendChild(nodeData);

                nodeParent.AppendChild(nodeChild);
                nodeTitle.AppendChild(nodeParent);
                #endregion

                #region Categories
                nodeParent = xmlDoc.CreateElement("categories");
                nodeChild = xmlDoc.CreateElement("category");

                nodeData = xmlDoc.CreateElement("id");
                nodeData.AppendChild(xmlDoc.CreateTextNode("2"));
                nodeChild.AppendChild(nodeData);

                nodeData = xmlDoc.CreateElement("name");
                nodeData.AppendChild(xmlDoc.CreateTextNode("default"));
                nodeChild.AppendChild(nodeData);

                nodeParent.AppendChild(nodeChild);
                nodeTitle.AppendChild(nodeParent);
                #endregion

                #region Catebory Combos
                nodeParent = xmlDoc.CreateElement("categoryCombos");
                nodeChild = xmlDoc.CreateElement("categoryCombo");

                nodeData = xmlDoc.CreateElement("id");
                nodeData.AppendChild(xmlDoc.CreateTextNode("3"));
                nodeChild.AppendChild(nodeData);

                nodeData = xmlDoc.CreateElement("name");
                nodeData.AppendChild(xmlDoc.CreateTextNode("default"));
                nodeChild.AppendChild(nodeData);

                nodeParent.AppendChild(nodeChild);
                nodeTitle.AppendChild(nodeParent);
                #endregion

                #region Category Option Combos
                nodeParent = xmlDoc.CreateElement("categoryOptionCombos");
                nodeChild = xmlDoc.CreateElement("categoryOptionCombo");

                nodeData = xmlDoc.CreateElement("id");
                nodeData.AppendChild(xmlDoc.CreateTextNode("4"));
                nodeChild.AppendChild(nodeData);

                nodeChildInner = xmlDoc.CreateElement("categoryCombo");
                nodeDataInner = xmlDoc.CreateElement("id");
                nodeDataInner.AppendChild(xmlDoc.CreateTextNode("3"));
                nodeChildInner.AppendChild(nodeDataInner);
                nodeChild.AppendChild(nodeChildInner);

                nodeDataInner = xmlDoc.CreateElement("name");
                nodeDataInner.AppendChild(xmlDoc.CreateTextNode("default"));
                nodeChildInner.AppendChild(nodeDataInner);
                nodeChild.AppendChild(nodeChildInner);

                nodeParentInner = xmlDoc.CreateElement("categoryOptions");
                nodeChildInner = xmlDoc.CreateElement("categoryOption");

                nodeDataInner = xmlDoc.CreateElement("id");
                nodeDataInner.AppendChild(xmlDoc.CreateTextNode("1"));
                nodeChildInner.AppendChild(nodeDataInner);
                nodeParentInner.AppendChild(nodeChildInner);

                nodeDataInner = xmlDoc.CreateElement("name");
                nodeDataInner.AppendChild(xmlDoc.CreateTextNode("default"));
                nodeChildInner.AppendChild(nodeDataInner);
                nodeParentInner.AppendChild(nodeChildInner);

                nodeChild.AppendChild(nodeParentInner);
                nodeParent.AppendChild(nodeChild);
                nodeTitle.AppendChild(nodeParent);
                #endregion

                #region categoryCategoryOptionAssociations
                nodeParent = xmlDoc.CreateElement("categoryCategoryOptionAssociations");
                nodeChild = xmlDoc.CreateElement("categoryCategoryOptionAssociation");

                nodeData = xmlDoc.CreateElement("category");
                nodeData.AppendChild(xmlDoc.CreateTextNode("2"));
                nodeChild.AppendChild(nodeData);

                nodeData = xmlDoc.CreateElement("categoryOption");
                nodeData.AppendChild(xmlDoc.CreateTextNode("1"));
                nodeChild.AppendChild(nodeData);

                nodeParent.AppendChild(nodeChild);
                nodeTitle.AppendChild(nodeParent);
                #endregion

                #region categoryComboCategoryAssociation
                nodeParent = xmlDoc.CreateElement("categoryComboCategoryAssociations");
                nodeChild = xmlDoc.CreateElement("categoryComboCategoryAssociation");

                nodeData = xmlDoc.CreateElement("categoryCombo");
                nodeData.AppendChild(xmlDoc.CreateTextNode("3"));
                nodeChild.AppendChild(nodeData);

                nodeData = xmlDoc.CreateElement("category");
                nodeData.AppendChild(xmlDoc.CreateTextNode("2"));
                nodeChild.AppendChild(nodeData);

                nodeParent.AppendChild(nodeChild);
                nodeTitle.AppendChild(nodeParent);
                #endregion

                #endregion

                mDataAdapter = new OdbcDataAdapter();

                #region dataElements

                DataTable mDtElements = new DataTable("dataElements");

                mCommand.CommandText = ""
                    + "SELECT "
                        + "CAST(" + Concat_Fields("elementId,'001'") + " AS char) elementId, "
                        + "CAST(" + Concat_Fields("element,' - Under 5'") + " AS char) element "
                    + "FROM afyapro_dhis_view_under5 "
                    + "WHERE startdate >= " + clsGlobal.Saving_DateValue(mDateFrom) + " "
                    + "AND endDate <= " + clsGlobal.Saving_DateValue(mDateTo) + " "
                    + "GROUP BY elementId "
                    + "UNION "
                    + "SELECT "
                        + "CAST(" + Concat_Fields("elementId,'002'") + " AS char) elementId, "
                        + "CAST(" + Concat_Fields("element,' - Above 5'") + " AS char) element "
                    + "FROM afyapro_dhis_view_above5 "
                    + "WHERE startdate >= " + clsGlobal.Saving_DateValue(mDateFrom) + " "
                    + "AND endDate <= " + clsGlobal.Saving_DateValue(mDateTo) + " "
                    + "GROUP BY elementId "
                    + "UNION "
                    + "SELECT "
                        + "CAST(elementId AS char) elementId, "
                        + "element "
                    + "FROM afyapro_dhis_view "
                    + "WHERE startDate >= " + clsGlobal.Saving_DateValue(mDateFrom) + " "
                    + "AND endDate <= " + clsGlobal.Saving_DateValue(mDateTo) + " "
                    + "GROUP BY elementId";

                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtElements);
                nodeParent = xmlDoc.CreateElement("dataElements");
                foreach (DataRow dRow in mDtElements.Rows)
                {
                    nodeChild = xmlDoc.CreateElement("dataElement");

                    nodeData = xmlDoc.CreateElement("id");
                    nodeData.AppendChild(xmlDoc.CreateTextNode(dRow["elementid"].ToString()));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("uuid");
                    nodeData.AppendChild(xmlDoc.CreateTextNode(dRow["elementid"].ToString()));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("name");
                    nodeData.AppendChild(xmlDoc.CreateTextNode(dRow["element"].ToString()));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("alternativeName");
                    nodeData.AppendChild(xmlDoc.CreateTextNode(dRow["element"].ToString()));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("shortName");
                    nodeData.AppendChild(xmlDoc.CreateTextNode(dRow["element"].ToString()));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("code");
                    nodeData.AppendChild(xmlDoc.CreateTextNode(dRow["elementid"].ToString()));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("description");
                    nodeData.AppendChild(xmlDoc.CreateTextNode(dRow["element"].ToString()));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("active");
                    nodeData.AppendChild(xmlDoc.CreateTextNode("true"));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("type");
                    nodeData.AppendChild(xmlDoc.CreateTextNode("int"));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("aggregationOperator");
                    nodeData.AppendChild(xmlDoc.CreateTextNode("sum"));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("categoryCombo");
                    nodeData.AppendChild(xmlDoc.CreateTextNode("3"));
                    nodeChild.AppendChild(nodeData);

                    nodeParent.AppendChild(nodeChild);
                }
                nodeTitle.AppendChild(nodeParent);

                #endregion

                #region Organization Units

                DataTable mOgUnits = new DataTable("mOgUnits");
                mCommand.CommandText = "SELECT autocode, facilitydescription from facility_view;";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mOgUnits);

                nodeParent = xmlDoc.CreateElement("organisationUnits");
                foreach (DataRow dRow in mOgUnits.Rows)
                {
                    nodeChild = xmlDoc.CreateElement("organisationUnit");

                    nodeData = xmlDoc.CreateElement("id");
                    nodeData.AppendChild(xmlDoc.CreateTextNode(dRow["autocode"].ToString()));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("uuid");
                    nodeData.AppendChild(xmlDoc.CreateTextNode(dRow["autocode"].ToString()));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("name");
                    nodeData.AppendChild(xmlDoc.CreateTextNode(dRow["facilitydescription"].ToString()));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("shortName");
                    nodeData.AppendChild(xmlDoc.CreateTextNode(dRow["facilitydescription"].ToString()));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("code");
                    nodeData.AppendChild(xmlDoc.CreateTextNode(dRow["autocode"].ToString()));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("openingDate");
                    nodeData.AppendChild(xmlDoc.CreateTextNode("2005-01-01"));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("closedDate");
                    nodeData.AppendChild(xmlDoc.CreateTextNode("2099-01-01"));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("active");
                    nodeData.AppendChild(xmlDoc.CreateTextNode("true"));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("comment");
                    nodeData.AppendChild(xmlDoc.CreateTextNode(string.Empty));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("geoCode");
                    nodeData.AppendChild(xmlDoc.CreateTextNode(string.Empty));
                    nodeChild.AppendChild(nodeData);

                    nodeParent.AppendChild(nodeChild);
                }
                nodeTitle.AppendChild(nodeParent);

                #endregion

                #region Periods

                DataTable mPeriods = new DataTable("mPeriods");

                mCommand.CommandText = ""
                    + "SELECT "
                        + "DISTINCT(afya_dhis_period.periodid), "
                        + "afya_dhis_period.startdate, "
                        + "afya_dhis_period.enddate "
                    + "FROM afyapro_dhis_view, afya_dhis_period "
                    + "WHERE afya_dhis_period.periodid = afyapro_dhis_view.periodid "
                    + "AND afyapro_dhis_view.startdate >= " + clsGlobal.Saving_DateValue(mDateFrom) + " "
                    + "AND afyapro_dhis_view.endDate <= " + clsGlobal.Saving_DateValue(mDateTo);
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mPeriods);

                nodeParent = xmlDoc.CreateElement("periods");
                foreach (DataRow dRow in mPeriods.Rows)
                {
                    nodeChild = xmlDoc.CreateElement("period");

                    nodeData = xmlDoc.CreateElement("id");
                    nodeData.AppendChild(xmlDoc.CreateTextNode(dRow["periodid"].ToString()));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("periodType");
                    nodeData.AppendChild(xmlDoc.CreateTextNode("Monthly"));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("startDate");
                    nodeData.AppendChild(xmlDoc.CreateTextNode(FormatDate(Convert.ToDateTime(dRow["startdate"].ToString()))));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("endDate");
                    nodeData.AppendChild(xmlDoc.CreateTextNode(FormatDate(Convert.ToDateTime(dRow["endDate"].ToString()))));
                    nodeChild.AppendChild(nodeData);

                    nodeParent.AppendChild(nodeChild);
                }
                nodeTitle.AppendChild(nodeParent);

                #endregion

                #region DataValues

                DataTable mDataValues = new DataTable("mDataValues");

                mCommand.CommandText = ""
                    + "SELECT "
                        + "CAST(" + Concat_Fields("elementId,'001'") + " AS char) elementId, "
                        + "periodid, "
                        + "cases, "
                        + "'1' AS sourceId "
                    + "FROM afyapro_dhis_view_under5 "
                    + "WHERE startdate >= " + clsGlobal.Saving_DateValue(mDateFrom) + " "
                    + "AND endDate <= " + clsGlobal.Saving_DateValue(mDateTo) + " "
                    + "GROUP BY elementId "
                    + "UNION "
                    + "SELECT "
                        + "CAST(" + Concat_Fields("elementId,'002'") + " AS char) elementId, "
                        + "periodid, "
                        + "cases, "
                        + "'1' AS sourceId "
                    + "FROM afyapro_dhis_view_above5 "
                    + "WHERE startdate >= " + clsGlobal.Saving_DateValue(mDateFrom) + " "
                    + "AND endDate <= " + clsGlobal.Saving_DateValue(mDateTo) + " "
                    + "GROUP BY elementId  "
                    + "UNION "
                    + "SELECT "
                        + "CAST(elementId AS CHAR) AS elementId, "
                        + "periodid, "
                        + "cases, "
                        + "'1' AS sourceId "
                    + "FROM afyapro_dhis_view "
                    + "WHERE startdate >= " + clsGlobal.Saving_DateValue(mDateFrom) + " "
                    + "AND endDate <= " + clsGlobal.Saving_DateValue(mDateTo) + " "
                    + "GROUP BY elementId";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataValues);

                nodeParent = xmlDoc.CreateElement("dataValues");
                foreach (DataRow dRow in mDataValues.Rows)
                {
                    nodeChild = xmlDoc.CreateElement("dataValue");

                    nodeData = xmlDoc.CreateElement("dataElement");
                    nodeData.AppendChild(xmlDoc.CreateTextNode(dRow["elementid"].ToString()));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("period");
                    nodeData.AppendChild(xmlDoc.CreateTextNode(dRow["periodid"].ToString()));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("source");
                    nodeData.AppendChild(xmlDoc.CreateTextNode(dRow["sourceid"].ToString()));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("value");
                    nodeData.AppendChild(xmlDoc.CreateTextNode(dRow["cases"].ToString()));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("storedBy");
                    nodeData.AppendChild(xmlDoc.CreateTextNode("15"));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("timeStamp");
                    nodeData.AppendChild(xmlDoc.CreateTextNode("2009-05-01"));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("comment");
                    nodeData.AppendChild(xmlDoc.CreateTextNode("Data From Afya Pro"));
                    nodeChild.AppendChild(nodeData);

                    nodeData = xmlDoc.CreateElement("categoryOptionCombo");
                    nodeData.AppendChild(xmlDoc.CreateTextNode("4"));
                    nodeChild.AppendChild(nodeData);

                    nodeParent.AppendChild(nodeChild);
                }
                nodeTitle.AppendChild(nodeParent);

                #endregion

                //Close title
                xmlDoc.AppendChild(nodeTitle);

                //Save to Memory stream
                MemoryStream xmlStream = new MemoryStream();
                xmlDoc.Save(xmlStream);

                return xmlStream.ToArray();
            }
            catch (OdbcException ex)
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

        #region Concat_Fields
        private string Concat_Fields(string mCommaSepStrings)
        {
            string mResult = "";

            string[] mStrings = mCommaSepStrings.Split(',');

            foreach (string mString in mStrings)
            {
                if (mResult.Trim() == "")
                {
                    mResult = mString;
                }
                else
                {
                    mResult = mResult + "," + mString;
                }
            }

            return "CONCAT(" + mResult + ")";
        }
        #endregion

        #region FormatDate
        private string FormatDate(DateTime tDate)
        {
            return tDate.Year + "-" + tDate.Month + "-" + tDate.Day;
        }
        #endregion
    }
}
