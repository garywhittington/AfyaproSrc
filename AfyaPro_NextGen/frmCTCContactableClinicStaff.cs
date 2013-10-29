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
    public partial class frmCTCContactableClinicStaff : DevExpress.XtraEditors.XtraForm
    {
        public frmCTCContactableClinicStaff()
       
        {
            InitializeComponent();
        }

      

        #region declaration
        private string pClassName = "";
        private AfyaPro_MT.clsQuarterlySupervisionReport pMdtCTCContactableStaffs;
        private AfyaPro_MT.clsQuarterlySupervisionReport CTCContactableStaff;
        #endregion
        private void frmCTCContactableClinicStaff_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCContactableClinicStaff_Load";
        
            try
            {
                Program.Restore_FormLayout(layoutControl1, this.Name);
                Program.Restore_FormSize(this);
                this.Location = new Point(350, 150);
                CTCContactableStaff = (AfyaPro_MT.clsQuarterlySupervisionReport)Activator.GetObject(
                    typeof(AfyaPro_MT.clsQuarterlySupervisionReport),
                    Program.gMiddleTier + "clsQuarterlySupervisionReport");

                DataTable mtbCTCClinicalStaff = new DataTable();
                mtbCTCClinicalStaff = CTCContactableStaff.View_ContactableClinicStaff();
                gridControl1.DataSource = mtbCTCClinicalStaff;
                
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }

        private void tbnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmCTCAddContactableStaff AddContactableStaff = new frmCTCAddContactableStaff();
            AddContactableStaff.Show();
            AddContactableStaff.Location = new Point(this.Location.X + this.Height / 4, this.Location.Y + this.Height / 4);

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            string mFunctionName = "frmCTCContactableClinicStaff_btnRemove";
            try
            {


                pMdtCTCContactableStaffs = (AfyaPro_MT.clsQuarterlySupervisionReport)Activator.GetObject(
                    typeof(AfyaPro_MT.clsQuarterlySupervisionReport),
                    Program.gMiddleTier + "clsQuarterlySupervisionReport");

                DataRow mSelectedDataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                AfyaPro_Types.clsContactableStaff mCtcContactableStaff = pMdtCTCContactableStaffs.Remove_Staff(
                Convert.ToString(mSelectedDataRow["staffname"]));

                gridView1.RefreshData();
                Program.Display_Info("Staff Removed successfully");


            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
    }
}