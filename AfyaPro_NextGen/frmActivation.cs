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
using System.IO;

namespace AfyaPro_NextGen
{
    public partial class frmRCHAntenatalVisitDetails1 : DevExpress.XtraEditors.XtraForm
    {
        private AfyaPro_MT.clsUsers pMdtUsers;
        private AfyaPro_MT.clsUserGroups pMdtUserGroups;
        private Type pType;
        private string pClassName = "";
        private string pFileContents = "";

        public frmRCHAntenatalVisitDetails1(string BaseString, int DaysToEnd, string info, bool mFullActive)
        {
            InitializeComponent();

            string mFunctionName = "frmActivation";

            pType = this.GetType();
            pClassName = pType.FullName;

            try
            {
                pMdtUsers = (AfyaPro_MT.clsUsers)Activator.GetObject(
                typeof(AfyaPro_MT.clsUsers),
                Program.gMiddleTier + "clsUsers");

                pMdtUserGroups = (AfyaPro_MT.clsUserGroups)Activator.GetObject(
                typeof(AfyaPro_MT.clsUserGroups),
                Program.gMiddleTier + "clsUserGroups");

                if (mFullActive == true)
                {
                    //retrieve licenced modules
                    DataTable mDtModules = pMdtUserGroups.Get_Modules("", "");

                    DataView mDvActiveModules = new DataView();
                    mDvActiveModules.Table = Program.gDtActiveModules;
                    mDvActiveModules.Sort = "activationkey";

                    string mModules = "";
                    foreach (DataRow mDataRow in mDtModules.Rows)
                    {
                        if (mDvActiveModules.Find(mDataRow["activationkey"].ToString().Trim()) >= 0)
                        {
                            if (mModules.Trim() == "")
                            {
                                mModules = "    -" + mDataRow["moduletext"].ToString().Trim();
                            }
                            else
                            {
                                mModules = mModules + Environment.NewLine + "   -" + mDataRow["moduletext"].ToString().Trim();
                            }
                        }
                    }

                    if (mModules.Trim() != "")
                    {
                        mModules = "This computer program is licenced for:-"
                            + Environment.NewLine + mModules;
                    }

                    txtInstallationId.Text = BaseString;
                    txtActivationKey.Text = mModules;
                    txbDays.Text = " Licenced";
                    cmdOk.Text = "Upgrade";
                }
                else
                {
                    txtInstallationId.Text = BaseString;
                    txbDays.Text = DaysToEnd.ToString() + " Day(s)";
                    cmdOk.Text = "Activate";
                }
                txbText.Text = info;

                if (DaysToEnd <= 0)
                {
                    txbDays.Text = "Finished";
                    cmdContinue.Enabled = false;
                }

                txtActivationKey.Focus();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            string mFunctionName = "frmActivation";

            try
            {
                if (pFileContents.Trim() == "")
                {
                    Program.Display_Error("Invalid activation file");
                    return;
                }

                byte[] mContents = Encoding.ASCII.GetBytes(pFileContents);

                AfyaPro_Types.clsLicence mLicence = pMdtUsers.Activate_Licence(mContents);
                if (mLicence.Exe_Result == -1)
                {
                    Program.Display_Server_Error(mLicence.Exe_Message);
                    return;
                }
                if (mLicence.Exe_Result == 0)
                {
                    Program.Display_Info(mLicence.Exe_Message, false);
                    return;
                }

                Program.Display_Info(mLicence.Exe_Message, false);
                System.Environment.Exit(1);
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }

        private void cmdContinue_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Retry;
        }

        private void cmdCopy_Click(object sender, EventArgs e)
        {

        }

        private void cmdPasteId_Click(object sender, EventArgs e)
        {
            OpenFileDialog mFileDlg = new OpenFileDialog();
            DialogResult mResult = mFileDlg.ShowDialog();

            pFileContents = "";

            if (mResult == DialogResult.OK)
            {
                string mFileName = mFileDlg.FileName;

                if (System.IO.File.Exists(mFileName) == false)
                {
                    Program.Display_Error("Licence file does not exist");
                    return;
                }

                FileStream fin = new FileStream(mFileName, FileMode.Open, FileAccess.Read);

                StringBuilder SB = new StringBuilder();
                int ch;
                for (int i = 0; i < fin.Length; i++)
                {
                    ch = fin.ReadByte();
                    if (ch == 0)
                        break;
                    SB.Append(Convert.ToChar(ch));
                }

                fin.Close();

                txtActivationKey.Text = SB.ToString();

                try
                {
                    pFileContents = AfyaPro_SoftwareLocker.FileReadWrite.ReadFile(mFileName);
                }
                catch
                {
                    Program.Display_Error("Invalid activation file", false);
                }
            }
        }
    }
}