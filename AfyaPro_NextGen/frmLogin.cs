/*
This file is part of AfyaPro.

	Copyright (C) 2013 AfyaPro Foundation.

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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace AfyaPro_NextGen
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsLanguage pMdtLanguage;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();
        private AfyaPro_MT.clsUsers pMtUsers;
        private AfyaPro_MT.clsUserGroups pMtUserGroups;
        private Type pType;
        private String pClassName = "";

        private DataTable pDtLanguage = new DataTable();

        #endregion

        #region frmLogin
        public frmLogin()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            String mFunctionName = "wndLogin";

            try
            {
                pMdtLanguage = (AfyaPro_MT.clsLanguage)Activator.GetObject(
                    typeof(AfyaPro_MT.clsLanguage),
                    Program.gMiddleTier + "clsLanguage");
                pMtUsers = (AfyaPro_MT.clsUsers)Activator.GetObject(
                    typeof(AfyaPro_MT.clsUsers),
                    Program.gMiddleTier + "clsUsers");
                pMtUserGroups = (AfyaPro_MT.clsUserGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsUserGroups),
                    Program.gMiddleTier + "clsUserGroups");

                pDtLanguage = pMdtLanguage.Get_Language(Program.gDefaultLanguageName, this.Name);

                this.Load_Controls();

                this.Text = Program.gApplicationName;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdExit_Click
        private void cmdExit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }
        #endregion

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(lblUserId);
            mObjectsList.Add(lblPassword);

            mObjectsList.Add(cmdLogin);
            mObjectsList.Add(cmdExit);

            Program.Apply_Language(pDtLanguage, mObjectsList);
        }
        #endregion

        #region cmdLogin_Click
        private void cmdLogin_Click(object sender, EventArgs e)
        {
            String mFunctionName = "cmdOk_Click";

            #region validation
            if (txtUserId.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.SEC_UserIdIsInvalid.ToString());
                txtUserId.Focus();
                return;
            }

            if (txtPassword.Text == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.SEC_PasswordIsInvalid.ToString());
                txtPassword.Focus();
                return;
            }
            #endregion

            try
            {
                Program.gCurrentUser = pMtUsers.Login(txtUserId.Text, txtPassword.Text);

                if (Program.gCurrentUser.Exe_Result == 0)
                {
                    if (Program.gCurrentUser.ValidCode == false)
                    {
                        Program.Display_Error(Program.gCurrentUser.Exe_Message);
                        txtUserId.Focus();
                        txtUserId.SelectAll();
                        return;
                    }

                    if (Program.gCurrentUser.ValidPassword == false)
                    {
                        Program.Display_Error(Program.gCurrentUser.Exe_Message);
                        txtPassword.Focus();
                        txtPassword.SelectAll();
                        return;
                    }
                }

                if (Program.gCurrentUser.Exe_Result == -1)
                {
                    Program.Display_Server_Error(Program.gCurrentUser.Exe_Message);
                    return;
                }

                //load user language
                Program.Get_UserSettings(true);

                //get usermoduleitems
                Program.gDtUserModuleItems = pMtUserGroups.Get_UserModuleItems("usergroupcode='"
                + Program.gCurrentUser.UserGroupCode.Trim() + "'", "");

                //get userfunctionaccesskeys
                Program.gDtUserFunctionAccessKeys = pMtUserGroups.Get_UserFunctionAccessKeys("usergroupcode='"
                + Program.gCurrentUser.UserGroupCode.Trim() + "'", "");

                //get all modules
                Program.gDtModules = pMtUserGroups.Get_Modules(Program.gLanguageName, "grdModules");

                //get all moduleitems
                Program.gDtModuleItems = pMtUserGroups.Get_ModuleItems(Program.gLanguageName, "grdModuleItems");

                //get all accessfunctions
                Program.gDtAccessFunctions = pMtUserGroups.Get_AccessFunctions(Program.gLanguageName, "AccessFunctions");

                //get userreports
                Program.gDtUserReports = pMtUserGroups.Get_UserReports("usergroupcode='"
                + Program.gCurrentUser.UserGroupCode.Trim() + "'", "");

                Program.gLoginOk = true;
                this.Close();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdChangePassword_Click
        private void cmdChangePassword_Click(object sender, EventArgs e)
        {
            String mFunctionName = "cmdChangePassword_Click";

            #region validation
            if (txtUserId.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.SEC_UserIdIsInvalid.ToString());
                txtUserId.Focus();
                return;
            }

            if (txtPassword.Text == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.SEC_PasswordIsInvalid.ToString());
                txtPassword.Focus();
                return;
            }
            #endregion

            try
            {
                Program.gCurrentUser = pMtUsers.Login(txtUserId.Text, txtPassword.Text);

                if (Program.gCurrentUser.Exe_Result == 0)
                {
                    if (Program.gCurrentUser.ValidCode == false)
                    {
                        Program.Display_Error(Program.gCurrentUser.Exe_Message);
                        txtUserId.Focus();
                        txtUserId.SelectAll();
                        return;
                    }

                    if (Program.gCurrentUser.ValidPassword == false)
                    {
                        Program.Display_Error(Program.gCurrentUser.Exe_Message);
                        txtPassword.Focus();
                        txtPassword.SelectAll();
                        return;
                    }
                }

                if (Program.gCurrentUser.Exe_Result == -1)
                {
                    Program.Display_Server_Error(Program.gCurrentUser.Exe_Message);
                    return;
                }

                //load user language
                Program.Get_UserSettings(true);

                //get usermoduleitems
                Program.gDtUserModuleItems = pMtUserGroups.Get_UserModuleItems("usergroupcode='"
                + Program.gCurrentUser.UserGroupCode.Trim() + "'", "");

                //get userfunctionaccesskeys
                Program.gDtUserFunctionAccessKeys = pMtUserGroups.Get_UserFunctionAccessKeys("usergroupcode='"
                + Program.gCurrentUser.UserGroupCode.Trim() + "'", "");

                //get all modules
                Program.gDtModules = pMtUserGroups.Get_Modules(Program.gLanguageName, "grdModules");

                //get all moduleitems
                Program.gDtModuleItems = pMtUserGroups.Get_ModuleItems(Program.gLanguageName, "grdModuleItems");

                //get all accessfunctions
                Program.gDtAccessFunctions = pMtUserGroups.Get_AccessFunctions(Program.gLanguageName, "AccessFunctions");

                frmChangePassword mChangePassword = new frmChangePassword(txtUserId.Text, false);
                
                mChangePassword.ShowDialog() ;
              
                // Reset the password field if the password is changed.
                if (mChangePassword.getPasswordChanged())
                    txtPassword.Text = "";
                    
                txtPassword.Focus();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion
    }
}