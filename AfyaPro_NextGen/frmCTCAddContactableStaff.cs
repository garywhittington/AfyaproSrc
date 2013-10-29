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
    public partial class frmCTCAddContactableStaff : DevExpress.XtraEditors.XtraForm
    {
        public frmCTCAddContactableStaff()
        {
            InitializeComponent();
        }
        private AfyaPro_MT.clsQuarterlySupervisionReport pMdtCTCContactableStaffs;
        private AfyaPro_MT.clsReporter pMdtReporter;
        #region Ok
        private void Ok()
        {
         
            string mFunctionName = "Ok";
            string pClassName = "AddContactableStaff";
            if (txtStaffName.Text.Trim() == "")
            {
                Program.Display_Error("Please enter staff name");
                txtStaffName.Focus();
                return;
            }


            if (txtPosition.Text  == "")
            {
                Program.Display_Error("Please enter staff position");
                txtPosition.Focus();
                return;
            }

            if (txtPhone.Text == "")
            {
                Program.Display_Error("Please enter staff phone number");
                txtPosition.Focus();
                return;
            }

            try
            {
                pMdtCTCContactableStaffs = (AfyaPro_MT.clsQuarterlySupervisionReport)Activator.GetObject(
                    typeof(AfyaPro_MT.clsQuarterlySupervisionReport),
                    Program.gMiddleTier + "clsQuarterlySupervisionReport");
                

                AfyaPro_Types.clsContactableStaff mCtcContactableStaff = pMdtCTCContactableStaffs.Add_Staff(
                    txtStaffName.Text,
                   txtPosition.Text,
                    txtPhone.Text);
               
              
                Program.Display_Info("Record saved successfully");
             
                this.Close();
                frmCTCContactableClinicStaff.ActiveForm.Close();
                frmCTCContactableClinicStaff mContactableStaff = new frmCTCContactableClinicStaff();
                mContactableStaff.Show();
                 
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmd_close
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Ok();
        }
        #endregion
    }
}