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
    public partial class frmSMSReceivedMessages : DevExpress.XtraEditors.XtraForm
    {
        public static DataGridViewRow pRow = new DataGridViewRow();
        public static DataGridViewRowCollection mSelectedRows;
        private AfyaPro_MT.clsSMS pMdtSMS;

        #region frmSMSReceivedMessages()
        public frmSMSReceivedMessages()
        {
            pMdtSMS = (AfyaPro_MT.clsSMS)Activator.GetObject(
                  typeof(AfyaPro_MT.clsSMS),
                  Program.gMiddleTier + "clsSMS");
            InitializeComponent();
        }
        #endregion

        #region Load_Data
        private void Load_Data()
        {
            try
            {
                //load data

                DataTable mDtReceivedSmS = new DataTable();
                mDtReceivedSmS.Clear();
                grdMain.Rows.Clear();
                mDtReceivedSmS = pMdtSMS.View_ReceivedMessages();

                //mDtReceivedSmS.Columns.Add("check", typeof(System.Boolean));

                foreach (DataColumn mCol in mDtReceivedSmS.Columns)
                {

                    if (mCol.ColumnName == "id")
                    {
                        mDtReceivedSmS.Columns["id"].ColumnName = "Message Number";

                    }
                    if (mCol.ColumnName == "message")
                    {
                        mDtReceivedSmS.Columns["message"].ColumnName = "Message";

                    }
                    if (mCol.ColumnName == "phonenumber")
                    {

                        mDtReceivedSmS.Columns["phonenumber"].ColumnName = "Phone Number";

                    }
                    if (mCol.ColumnName == "datereceived")
                    {
                        mDtReceivedSmS.Columns["datereceived"].ColumnName = "Date Received";

                    }

                    if (mCol.ColumnName == "datesent")
                    {
                        mDtReceivedSmS.Columns["datesent"].ColumnName = "Date Sent";

                    }

                    if (mCol.ColumnName == "processed")
                    {
                        mDtReceivedSmS.Columns["processed"].ColumnName = "Processed";

                    }



                }

                mDtReceivedSmS.AcceptChanges();
                grdMain.DataSource = mDtReceivedSmS;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion

        #region Data_Delete
        public void Data_Delete(DataGridViewRow mRow)
        {
            try
            {
                Boolean Result;
                DialogResult rst = MessageBox.Show("Do you want to delete SMS number " + pRow.Cells[0].Value.ToString() + " ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rst == DialogResult.No)
                {
                    return;
                }

                String mMessage = pRow.Cells[1].Value.ToString();
                string mPhoneNo = pRow.Cells[2].Value.ToString();
                DateTime mDateReceived = Convert.ToDateTime(pRow.Cells[3].Value.ToString());
                DateTime mDateDeleted = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);
                string mDeletedBy = Program.gCurrentUser.Code;


                pMdtSMS.Add_Trash(mMessage, mPhoneNo, mDateReceived, mDateDeleted, mDeletedBy);
                Result = pMdtSMS.Delete_ReceivedMessage(pRow.Cells[0].Value.ToString());
                if (Result == false)
                {
                    Program.Display_Error("Unable to delete the SMS");
                    return;
                }
                Program.Display_Info("SMS Deleted successfully");
               this.Load_Data();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region frmSMSReceivedMessages_Load
        private void frmSMSReceivedMessages_Load(object sender, EventArgs e)
        {
            
                this.Load_Data();
        }
        #endregion

        #region grdMain_CellContentClick
        private void grdMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                pRow = grdMain.SelectedRows[0];
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}