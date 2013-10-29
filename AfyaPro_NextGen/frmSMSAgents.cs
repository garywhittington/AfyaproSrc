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
    public partial class frmSMSAgents : DevExpress.XtraEditors.XtraForm
    {
        #region declaration
        private AfyaPro_MT.clsSMS pMdtSMS;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();
        public static DataGridViewRow pRow;
        #endregion

        #region frmSMSAgents()
        public frmSMSAgents()
        {
            InitializeComponent();
            pMdtSMS = (AfyaPro_MT.clsSMS)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSMS),
                    Program.gMiddleTier + "clsSMS");
        }
        #endregion

        #region LoadData

        private void LoadData()
        {
            try
            {
                //load data

                DataTable mDtAgents = new DataTable();
                mDtAgents.Clear();
                mDtAgents = pMdtSMS.Get_SMSAgents();

                foreach (DataColumn mCol in mDtAgents.Columns)
                {

                    if (mCol.ColumnName == "phone")
                    {
                        mDtAgents.Columns["phone"].ColumnName = "Phone Number";

                    }
                    if (mCol.ColumnName == "name")
                    {

                        mDtAgents.Columns["name"].ColumnName = "Name";

                    }
                    if (mCol.ColumnName == "location")
                    {
                        mDtAgents.Columns["location"].ColumnName = "Location";

                    }

                    if (mCol.ColumnName == "id")
                    {
                        mDtAgents.Columns["id"].ColumnName = "ID";

                    }




                }

                mDtAgents.AcceptChanges();
                grdMain.DataSource = mDtAgents;

            }
            catch (Exception ex)
            {
                Program.Display_Error(ex.Message, "Load data", ex.Message);
                return;
            }
        }
        #endregion

        #region Data_Delete
        public void Data_Delete(string mId)
        {
            try
            {
                Boolean Result;
                DialogResult rst = MessageBox.Show("Do you want to delete community worker number " + mId.ToString() + " ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rst == DialogResult.No)
                {
                    return;
                }
                             
               
                Result = pMdtSMS.Delete_SMSAgent(mId);
                if (Result == false)
                {
                    Program.Display_Error("Unable to delete the community worker");
                    return;
                }
                Program.Display_Info("Community worker Deleted successfully");
                this.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region frmSMSAgents_Load
        private void frmSMSAgents_Load(object sender, EventArgs e)
        {
           
            this.LoadData();
        }
        #endregion

        #region grdMain_CellClick
        private void grdMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                pRow = grdMain.SelectedRows[0];
            }
            catch { }
           
        }
        #endregion
    }
}