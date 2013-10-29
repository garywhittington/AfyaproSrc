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

namespace AfyaPro_SerialMaker
{
    public partial class frmMain : Form
    {
        private string pId = "141";

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if (radTrialExtend.Checked == false
                    && radTrialReset.Checked == false
                    && radFullLicence.Checked == false)
                {
                    MessageBox.Show("Please select the licence type",
                                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string mLicenceType = "Full";
                if (radTrialExtend.Checked == true)
                {
                    mLicenceType = "TrialExtend";
                }
                if (radTrialReset.Checked == true)
                {
                    mLicenceType = "TrialReset";
                }

                int mExtendDays = 0;
                if (radTrialExtend.Checked == true || radTrialReset.Checked == true)
                {
                    try
                    {
                        mExtendDays = Convert.ToInt32(txtDays.Text);
                    }
                    catch
                    {
                        MessageBox.Show("Invalid number of days");
                        txtDays.Focus();
                        return;
                    }
                }

                if (System.IO.Directory.Exists(txtFilePath.Text) == false)
                {
                    MessageBox.Show("Invalid file path");
                    txtFilePath.Focus();
                    return;
                }

                string mFileName = txtFilePath.Text + "\\AfyaPro_Licence.ap";
                if (System.IO.File.Exists(mFileName) == true)
                {
                    DialogResult mDlgResult = MessageBox.Show("File already exist. Overwrite?",
                        Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (mDlgResult == DialogResult.Yes)
                    {
                        System.IO.File.Delete(mFileName);
                    }
                    else
                    {
                        return;
                    }
                }

                string mPassword = "";
                string mModuleKeys = "";
                string mLicenceId = "";

                #region make key
                string mBaseString = txtInstallationId.Text.Replace("-", "");
                string mTempPassword = AfyaPro_SoftwareLocker.Encryption.MakePassword(mBaseString, pId);

                int mGroupStart = 0;
                for (int mIndex = 0; mIndex < 5; mIndex++)
                {
                    if (mPassword.Trim() == "")
                    {
                        mPassword = mTempPassword.Substring(mGroupStart, 5);
                    }
                    else
                    {
                        mPassword = mPassword + "-" + mTempPassword.Substring(mGroupStart, 5);
                    }

                    mGroupStart = mGroupStart + 5;
                }
                #endregion

                #region modulekeys
                foreach (Control mControl in groupBox1.Controls)
                {
                    if (mControl is CheckBox)
                    {
                        CheckBox mCheckBox = (CheckBox)mControl;

                        if (mCheckBox.Checked == true)
                        {
                            if (mModuleKeys.Trim() == "")
                            {
                                mModuleKeys = mCheckBox.Name.Substring(3);
                            }
                            else
                            {
                                mModuleKeys = mModuleKeys + "," + mCheckBox.Name.Substring(3);
                            }
                        }
                    }
                }
                #endregion

                #region licenceid

                StringBuilder builder = new StringBuilder();
                Random random = new Random();
                char ch;
                for (int i = 0; i < 6; i++)
                {
                    ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                    builder.Append(ch);
                }

                mLicenceId = builder.ToString().ToLower();

                #endregion

                string mHideInfo = ""
                    + mExtendDays + ";"
                    + mPassword + ";"
                    + txtInstallationId.Text + ";"
                    + mModuleKeys + ";"
                    + mLicenceId + ";"
                    + mLicenceType;

                AfyaPro_SoftwareLocker.Licencing.Save_ActivationCode(mFileName, mHideInfo);

                MessageBox.Show("Licence file saved to " + mFileName,
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to make serial\n" + ex.Message);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog mDlg = new FolderBrowserDialog();

            DialogResult mDlgResult = mDlg.ShowDialog();

            if (mDlgResult == DialogResult.OK)
            {
                txtFilePath.Text = mDlg.SelectedPath;
            }
        }

        private void radTrialExtend_CheckedChanged(object sender, EventArgs e)
        {
            txtDays.Focus();
        }

        private void radTrialReset_CheckedChanged(object sender, EventArgs e)
        {
            txtDays.Focus();
        }

        private void chkrpt_CheckedChanged(object sender, EventArgs e)
        {
            chkrpd.Checked = chkrpt.Checked;
        }

        private void chkivt_CheckedChanged(object sender, EventArgs e)
        {
            chkivs.Checked = chkivt.Checked;
        }

        private void chkbil_CheckedChanged(object sender, EventArgs e)
        {
            chkbls.Checked = chkbil.Checked;
        }
    }
}