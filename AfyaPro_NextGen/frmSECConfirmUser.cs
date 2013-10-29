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
    public partial class frmSECConfirmUser : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();
        private AfyaPro_MT.clsUsers pMtUsers;
        private Type pType;
        private String pClassName = "";

        private bool pCanChangeDate = false;
        internal bool CanChangeDate
        {
            set { pCanChangeDate = value; }
            get { return pCanChangeDate; }
        }

        #endregion

        #region frmSECConfirmUser
        public frmSECConfirmUser()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            String mFunctionName = "wndLogin";

            try
            {
                pMtUsers = (AfyaPro_MT.clsUsers)Activator.GetObject(
                    typeof(AfyaPro_MT.clsUsers),
                    Program.gMiddleTier + "clsUsers");
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
            this.Close();
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
                AfyaPro_Types.clsUser mCurrentUser = pMtUsers.Login(txtUserId.Text, txtPassword.Text);

                if (mCurrentUser.Exe_Result == 0)
                {
                    if (mCurrentUser.ValidCode == false)
                    {
                        Program.Display_Error(mCurrentUser.Exe_Message);
                        txtUserId.Focus();
                        txtUserId.SelectAll();
                        return;
                    }

                    if (mCurrentUser.ValidPassword == false)
                    {
                        Program.Display_Error(mCurrentUser.Exe_Message);
                        txtPassword.Focus();
                        txtPassword.SelectAll();
                        return;
                    }
                }

                if (mCurrentUser.Exe_Result == -1)
                {
                    Program.Display_Server_Error(mCurrentUser.Exe_Message);
                    return;
                }

                pCanChangeDate = Convert.ToBoolean(mCurrentUser.AllowChangingTransDate);
                this.Close();
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