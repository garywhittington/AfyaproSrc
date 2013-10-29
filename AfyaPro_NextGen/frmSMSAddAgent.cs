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

namespace AfyaPro_NextGen
{
    public partial class frmSMSAddAgent : DevExpress.XtraEditors.XtraForm
    {
        #region declaration
        private AfyaPro_MT.clsSMS pMdtSMS;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();
        private DataGridViewRow pRow;
        private string pMode;
        #endregion

        #region frmSMSAddAgent()
        public frmSMSAddAgent(DataGridViewRow mRow, string mMode)
        {
            pRow = mRow;
            pMode = mMode;
            pMdtSMS = (AfyaPro_MT.clsSMS)Activator.GetObject(
                   typeof(AfyaPro_MT.clsSMS),
                   Program.gMiddleTier + "clsSMS");
            InitializeComponent();
        }
        #endregion

        #region btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region btnSave_Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            Boolean mResult =false;
            try
            {
               

                #region validation
                if (txtName.Text == "")
                {
                    Program.Display_Error("Please enter worker's name");
                    return;
                }

                if (txtPhone.Text == "")
                {
                    Program.Display_Error("Please enter worker's phone");
                    return;
                }

                if (txtLocation.Text == "")
                {
                    Program.Display_Error("Please enter worker's location");
                    return;
                }
                #endregion
                                                
                #region Add
                if (pMode == "New")
                {
                    mResult = pMdtSMS.Add_SMSAgent(txtName.Text, txtPhone.Text, txtLocation.Text);
                }

                #endregion

                #region Edit
                if (pMode == "Edit")
                {
                    //MessageBox.Show(pRow.Cells[0].Value.ToString());
                    mResult = pMdtSMS.Edit_SMSAgent(txtName.Text, txtPhone.Text, txtLocation.Text, pRow.Cells[0].Value.ToString());
                }

                #endregion

                #region Infor
                if (mResult == false)
                {
                    Program.Display_Error("Unable to save the work, an error was encountered");
                }
                else
                {
                   
                    ((frmSMSAgents)Program.gMdiForm.ActiveMdiChild).Close();

                    frmSMSAgents mTpt = new frmSMSAgents();
                    mTpt.MdiParent = Program.gMdiForm;
                    mTpt.Show();

                    if (pMode == "New")
                    {
                        Program.Display_Info("Community worker successfully added!");
                    }
                    else if (pMode == "Edit")
                    {
                        Program.Display_Info("Community worker successfully updated!");
                    }
                    txtLocation.ResetText();
                    txtName.ResetText();
                    txtPhone.ResetText();
                }
                #endregion


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region frmSMSAddAgent_Load
        private void frmSMSAddAgent_Load(object sender, EventArgs e)
        {
            try
            {
               
                if (pMode == "Edit")
                {
                    txtName.Text = pRow.Cells[1].Value.ToString();
                    txtPhone.Text = pRow.Cells[2].Value.ToString();
                    txtLocation.Text = pRow.Cells[3].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("frmSMSAddAgent_Load " + ex.Message);
            }

        }
        #endregion


    }
}