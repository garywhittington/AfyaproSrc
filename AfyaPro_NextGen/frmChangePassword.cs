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
using DevExpress.XtraEditors;
using System.Text.RegularExpressions;

namespace AfyaPro_NextGen
{
    public partial class frmChangePassword : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();
        private AfyaPro_MT.clsUsers pMtUsers;
        private Type pType;
        private String pClassName = "";

        private DataTable pDtLanguage = new DataTable();
        private string pUserId = "";
        private bool pReset = false;
        private bool pPasswordChanged = false;

        #endregion

        #region frmChangePassword
        public frmChangePassword(string mUserId, bool mReset)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            String mFunctionName = "wndLogin";

            try
            {
                pUserId = mUserId;
                pReset = mReset;

                if (pReset == true)
                {
                    this.Text = "Reset Password for user '" + mUserId + "'";
                }

                pMtUsers = (AfyaPro_MT.clsUsers)Activator.GetObject(
                    typeof(AfyaPro_MT.clsUsers),
                    Program.gMiddleTier + "clsUsers");

                this.Load_Controls();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(txbNewPassword);
            mObjectsList.Add(txtConfirmPassword);
            mObjectsList.Add(cmdOk);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region cmdOk_Click
        private void cmdOk_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdOk_Click";

            #region validation
            if (Regex.IsMatch(txtNewPassword.Text, @"^[a-zA-Z0-9]{3,}$") == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.SEC_PasswordLettersOrNumbersOnly.ToString());
                txtNewPassword.Focus();
                return;
            }

            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.SEC_PasswordConfirmedDoNotMatch.ToString());
                txtNewPassword.Focus();
                return;
            }
            #endregion

            try
            {
                //change pwd 
                pResult = pMtUsers.Change_Password(pUserId, txtNewPassword.Text);
                if (pResult.Exe_Result == 0)
                {
                    Program.Display_Error(pResult.Exe_Message);
                    return;
                }
                if (pResult.Exe_Result == -1)
                {
                    Program.Display_Server_Error(pResult.Exe_Message);
                    return;
                }

                //feedback
                if (pReset == true)
                {
                    Program.Display_Info("Password for user '" + pUserId + "' has been reset successfully");
                }
                else
                {
                    Program.Display_Info(AfyaPro_Types.clsSystemMessages.MessageIds.SEC_PasswordChanged.ToString());
                }
                pPasswordChanged = true;
                this.Close();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        public bool getPasswordChanged()
        {
            return pPasswordChanged;
        }
    }
}