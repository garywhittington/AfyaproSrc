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
    public class clsSMS : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsSMS";

        #endregion

        #region View_SentMessages
        public DataTable View_SentMessages(String mFilter, String mOrder)
        {
            String mFunctionName = "View_SentMessages";

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
                string mCommandText = "select * from smssentmessages";

               
                mCommand.CommandText = mCommandText;
                mCommand.ExecuteNonQuery();

                DataTable mDataTable = new DataTable("sentmessages");
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

        #region Delete_SentMessage
        public Boolean Delete_SentMessage(String mMessageID)
        {
            String mFunctionName = "Delete_SentMessage";

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
                return false;
            }

            #endregion

            try
            {
                string mCommandText = "delete from smssentmessages where id ='" + mMessageID + "'";


                mCommand.CommandText = mCommandText;
                mCommand.ExecuteNonQuery();

                
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

        #region View_ReceivedMessages
        public DataTable View_ReceivedMessages()
        {
            String mFunctionName = "View_ReceivedMessages";

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
                string mCommandText = "select * from smsreceivedmessages";


                mCommand.CommandText = mCommandText;
                mCommand.ExecuteNonQuery();

                DataTable mDataTable = new DataTable("receivedmessages");
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

        #region Delete_ReceivedMessage
        public Boolean Delete_ReceivedMessage(String mMessageID)
        {
            String mFunctionName = "Delete_SentMessage";

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
                return false;
            }

            #endregion

            try
            {
                string mCommandText = "delete from smsreceivedmessages where id ='" + mMessageID + "'";


                mCommand.CommandText = mCommandText;
                mCommand.ExecuteNonQuery();


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

        #region View_MessageTemplates
        public DataTable View_MessageTemplates(String mFilter, String mOrder, string mLanguageName, string mGridName, bool mSearchICD = false)
        {
            String mFunctionName = "View_MessageTemplates";

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
                string mCommandText = "select messagecode, message, clients, createdon, createdby from smsmessagetemplates";

               
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("smstemplates");
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

        #region Get_PatientGroups
        public DataTable Get_PatientGroups()
        {
            String mFunctionName = "Get_PatientGroups";

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
                string mCommandText = "select * from smspatientcategories";

               
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("smspatientcategories");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);
                mDataTable.Columns.Remove("id");
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

        #region Add_MessageTemplate
        public Boolean Add_MessageTemplate(String mMessageCode, String mMessage, string mGroup, string mAuthor, DateTime mDateCreated)
        {
            String mFunctionName = "Add_MessageTemplate";

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
                return false;
            }

            #endregion

            try
            {
                
                mCommand.CommandText = "insert into smsmessagetemplates(messagecode, message, clients, createdon, createdby) values('" + mMessageCode + "', '" + mMessage + "', '" + mGroup + "', " + clsGlobal.Saving_DateValue(mDateCreated) + ", '" + mAuthor + "')";
                mCommand.ExecuteNonQuery();

                

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

        #region Edit_MessageTemplate
        public Boolean Edit_MessageTemplate(String mMessageCode, String mMessage, string mGroup, string mAuthor, DateTime mDateCreated)
        {
            String mFunctionName = "Edit_MessageTemplate";

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
                return false;
            }

            #endregion

            try
            {

                mCommand.CommandText = "update smsmessagetemplates set message = '" + mMessage + "',  clients = '" + mGroup + "' where messagecode ='" + mMessageCode + "'";
                mCommand.ExecuteNonQuery();


                
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

        #region Delete_MessageTemplate
        public Boolean Delete_MessageTemplate(String mMessagecode)
        {
            String mFunctionName = "Delete_MessageTemplate";

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
                return false;
            }

            #endregion

            try
            {

                mCommand.CommandText = "delete from smsmessagetemplates where messagecode ='" + mMessagecode + "'";
                mCommand.ExecuteNonQuery();



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

        #region Add_ClientGroup
        public Boolean Add_ClientGroup(String mGroup, String mDescription, string mCode)
        {
            String mFunctionName = "Add_ClientGroup";

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
                return false;
            }

            #endregion

            try
            {

                mCommand.CommandText = "insert into smspatientcategories(colgroup, description, code) values('" + mGroup  + "', '" + mDescription  + "', '" + mCode + "')";
                mCommand.ExecuteNonQuery();



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

        #region Edit_PatientGroups
        public Boolean  Edit_PatientGroups(string mGroup, string mDescription, string mCode)
        {
            String mFunctionName = "Edit_PatientGroups";

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
                return false;
            }

            #endregion

            try
            {
                string mCommandText = "update smspatientcategories set colgroup ='" + mGroup + "', description ='" + mDescription + "' where code ='" + mCode + "'";


                mCommand.CommandText = mCommandText;
                mCommand.ExecuteNonQuery();
               
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

        #region Delete_PatientGroup
        public Boolean Delete_PatientGroup(String mGroupCode)
        {
            String mFunctionName = "Delete_PatientGroup";

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
                return false;
            }

            #endregion

            try
            {

                mCommand.CommandText = "delete from smspatientcategories where code ='" + mGroupCode + "'";
                mCommand.ExecuteNonQuery();



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

        #region Add_Trash
        public Boolean Add_Trash(String mMessage, string mPhoneNo, DateTime mDateReceived, DateTime mDateDeleted, string mDeletedBy)
        {
            String mFunctionName = "Add_Trash";

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
                return false;
            }

            #endregion

            try
            {

                mCommand.CommandText = "insert into smstrash(message, phonenumber, datereceived, datedeleted, deletedby) values('" + mMessage + "', '" + mPhoneNo +  "', " + clsGlobal.Saving_DateValue(mDateReceived) + ", " + clsGlobal.Saving_DateValue(mDateDeleted) + " ,'"  +  mDeletedBy + "')";
                mCommand.ExecuteNonQuery();



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

        #region View_Trash
        public DataTable View_Trash()
        {
            String mFunctionName = "View_Trash";

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
                string mCommandText = "select * from smstrash";


                mCommand.CommandText = mCommandText;
                mCommand.ExecuteNonQuery();

                DataTable mDataTable = new DataTable("receivedmessages");
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

        #region Get_GroupClients
        public DataTable Get_GroupClients(string mGroupCode)
        {
            String mFunctionName = "Get_GroupClients";

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
                string mCommandText = "select * from smspatients" + mGroupCode;


                mCommand.CommandText = mCommandText;
                mCommand.ExecuteNonQuery();

                DataTable mDataTable = new DataTable("smspatients");
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

        #region Add_ToOutBox
        public Boolean Add_ToOutBox(String mMessageCode, String mMessage, string[] mPhoneNumbers)
        {
            String mFunctionName = "Add_ToOutBox";

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
                return false;
            }

            #endregion

            try
            {
                int index = 0;
                for (index = 0; index <= mPhoneNumbers.Length - 1; index++)
                {
                    string mPhoneNo = mPhoneNumbers.GetValue(index).ToString();
                    mCommand.CommandText = "insert into smsoutbox(messagecode, message, phonenumber, status) values('" + mMessageCode + "', '" + mMessage + "', '" + mPhoneNo + "', 'ToSend')";
                    mCommand.ExecuteNonQuery();
                }



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

        #region Get_SMSPatientsList
        public DataTable Get_SMSPatientsList()
        {
            String mFunctionName = "Get_SMSPatientsList";

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
                string mCommandText = "select p.code, p.fullname, p.phonenumber, g.colgroup from smspatients p, smspatientcategories g where p.groupcode = g.code";


                mCommand.CommandText = mCommandText;
                mCommand.ExecuteNonQuery();

                DataTable mDataTable = new DataTable("smspatients");
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

        #region Add_SMSPatient
        public Boolean Add_SMSPatient(String mCode, string mName, string mPhoneNo, string mGroupCode)
        {
            String mFunctionName = "Add_SMSPatient";

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
                return false;
            }

            #endregion


           
            try
            {

                mCommand.CommandText = "insert into smspatients(code, fullname, phonenumber, groupcode) values('" + mCode + "', '" + mName + "', '" + mPhoneNo + "', '" + mGroupCode + "')";
                mCommand.ExecuteNonQuery();



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

        #region Get_MobileRegistrations
        public DataTable Get_MobileRegistrations(int mRegistered)
        {
            String mFunctionName = "Get_MobileRegistrations";

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
                string mCommandText = "select * from smspatients_mobile where registered =" + mRegistered;


                mCommand.CommandText = mCommandText;
                mCommand.ExecuteNonQuery();

                DataTable mDataTable = new DataTable("smspatients");
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

        #region Update_MobileRegistrations
        public DataTable Update_MobileRegistrations(string mAutoCode, string mPatientCode)
        {
            String mFunctionName = "Update_MobileRegistrations";

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
                string mCommandText = "update smspatients_mobile set code='" + mPatientCode + "', registered =1 where autocode ='" + mAutoCode + "'";


                mCommand.CommandText = mCommandText;
                mCommand.ExecuteNonQuery();

                DataTable mDataTable = new DataTable("smspatients");
                

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

        #region Get_SMSAgents
        public DataTable Get_SMSAgents()
        {
            String mFunctionName = "Get_SMSAgents";

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
                string mCommandText = "select id, name, phone, location from smsagents";


                mCommand.CommandText = mCommandText;
                mCommand.ExecuteNonQuery();

                DataTable mDataTable = new DataTable("smsagents");
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

        #region Add_SMSAgent
        public Boolean Add_SMSAgent(string mName, string mPhoneNo, string mLocation)
        {
            String mFunctionName = "Add_SMSAgent";

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
                return false;
            }

            #endregion



            try
            {

                mCommand.CommandText = "insert into smsagents(name, phone, location) values('" + mName + "', '" + mPhoneNo + "', '" + mLocation + "')";
                mCommand.ExecuteNonQuery();



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

        #region Edit_SMSAgent
        public Boolean Edit_SMSAgent(string mName, string mPhoneNo, string mLocation, string mId)
        {
            String mFunctionName = "Edit_SMSAgent";

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
                return false;
            }

            #endregion



            try
            {

                mCommand.CommandText = "update smsagents set name ='" + mName + "', phone ='" + mPhoneNo + "', location ='" + mLocation + "' where id ='" + mId + "'";
                mCommand.ExecuteNonQuery();



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

        #region Delete_SMSAgent
        public Boolean Delete_SMSAgent(string mId)
        {
            String mFunctionName = "Delete_SMSAgent";

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
                return false;
            }

            #endregion



            try
            {

                mCommand.CommandText = "delete from smsagents where id ='" + mId + "'";
                mCommand.ExecuteNonQuery();



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



                
      
    }
}
