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
using System.Xml;

namespace AfyaPro_NextGen
{
    public partial class frmSMSClientGroups : DevExpress.XtraEditors.XtraForm
    {
        public static DataGridViewRow pRow = new DataGridViewRow();
        private AfyaPro_MT.clsSMS pMdtSMS;
        public frmSMSClientGroups()
        {
            pMdtSMS = (AfyaPro_MT.clsSMS)Activator.GetObject(
                  typeof(AfyaPro_MT.clsSMS),
                  Program.gMiddleTier + "clsSMS");
            InitializeComponent();
        }

        private void frmMainGrid_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmMainGrid_Load";
            try
            {
                //load data
               
                DataTable mDtGroups = new DataTable();
                mDtGroups = pMdtSMS.Get_PatientGroups();
               // MessageBox.Show("Hallo");
                foreach (DataColumn mCol in mDtGroups.Columns)

                {

                    if (mCol.ColumnName == "code")
                    {
                        mDtGroups.Columns["code"].ColumnName = "Group Code";

                    }
                    if (mCol.ColumnName == "colgroup")
                    {

                        mDtGroups.Columns["colgroup"].ColumnName = "Group Name";

                    }
                    if (mCol.ColumnName == "description")
                    {
                        mDtGroups.Columns["description"].ColumnName = "Group Description";

                    }

                   

                    
                }

                mDtGroups.AcceptChanges();
                grdMain.DataSource = mDtGroups;

            }
            catch (Exception ex)
            {
                Program.Display_Error(ex.Message, mFunctionName, ex.Message);
                return;
            }
        }

        private void frmMainGrid_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void grdMain_DoubleClick(object sender, EventArgs e)
        {
          
        }

        private void grdMain_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void grdMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            pRow = grdMain.SelectedRows[0];
        }

        private void grdMain_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            pRow = grdMain.SelectedRows[0];

            try
            {
                //DialogResult rst = MessageBox.Show("Are you sure you want to delete " + pRow.Cells[2].Value.ToString() + "?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                //if (rst == DialogResult.No)
                //{
                //    return;
                //}

                //pMdtSMS.Delete_PatientGroup(pRow.Cells[2].Value.ToString());

                frmSMSGroups mGroups = new frmSMSGroups(pRow, "Edit");
                mGroups.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}