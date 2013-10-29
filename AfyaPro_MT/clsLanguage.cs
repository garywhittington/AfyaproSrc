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
using System.Xml.Linq;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;

namespace AfyaPro_MT
{
    public class clsLanguage : MarshalByRefObject
    {
        #region declaration

        private string pClassName = "AfyaPro_MT.clsLanguage";

        #endregion

        #region Get_DefinedLanguages
        public DataTable Get_DefinedLanguages()
        {
            string mFunctionName = "Get_DefinedLanguages";

            try
            {
                DataTable mDtLanguages = new DataTable("languages");
                mDtLanguages.Columns.Add("description", typeof(System.String));

                //language folder path
                string mLanguagesPath = @AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Lang\";

                if (Directory.Exists(mLanguagesPath) == false)
                {
                    Directory.CreateDirectory(mLanguagesPath);
                }

                DirectoryInfo mDirectoryInfo = new DirectoryInfo(mLanguagesPath);
                FileSystemInfo[] mFileSystemInfos = mDirectoryInfo.GetFileSystemInfos();

                char[] mCharsToTrim = {'.','x','m','l'};

                foreach (FileSystemInfo mFileSystemInfo in mFileSystemInfos)
                {
                    if (mFileSystemInfo.Extension.ToLower() == ".xml")
                    {
                        DataRow mNewRow = mDtLanguages.NewRow();
                        mNewRow["description"] = mFileSystemInfo.Name.TrimEnd(mCharsToTrim);
                        mDtLanguages.Rows.Add(mNewRow);
                        mDtLanguages.AcceptChanges();
                    }
                }

                return mDtLanguages;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
        }
        #endregion

        #region Get_Messages
        public DataTable Get_Messages(string mLanguageName, string mObjectName)
        {
            string mFunctionName = "Get_Messages";

            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

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

                DataTable mDtMessages = new DataTable("englishlanguagemessages");
                mCommand.CommandText = "select * from englishlanguagemessages";
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDtMessages);

                #region interprete data

                if (mLanguageName.Trim() != "" && mObjectName.Trim() != "")
                {
                    if (File.Exists(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Lang\" + mLanguageName + ".xml") == false)
                    {
                        mLanguageName = "English";
                    }

                    try
                    {
                        var mCurrLang = from lang in System.Xml.Linq.XElement.Load(
                            AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Lang\" + mLanguageName + ".xml").Elements(mObjectName)
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

                        foreach (DataRow mDataRow in mDtMessages.Rows)
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

                return mDtMessages;
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

        #region Get_Language
        public DataTable Get_Language(string mLanguageName, string mObjectName)
        {
            string mFunctionName = "Get_Language";

            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

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

                if (File.Exists(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Lang\" + mLanguageName + ".xml") == false)
                {
                    mLanguageName = "English";
                }

                var mCurrLang = from lang in XElement.Load(
                    AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Lang\" + mLanguageName + ".xml").Elements(mObjectName)
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

                return mDtLanguage;
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
