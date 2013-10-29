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
    public partial class frmSMSSentMessages : DevExpress.XtraEditors.XtraForm
    {
        public static DataGridViewRow pRow = new DataGridViewRow();
        public static DataGridViewRowCollection mSelectedRows;
        private AfyaPro_MT.clsSMS pMdtSMS;

        #region frmSMSSentMessages()
        public frmSMSSentMessages()
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

                DataTable mDtSentSmS = new DataTable();
                mDtSentSmS.Clear();
                grdMain.Rows.Clear();
                mDtSentSmS = pMdtSMS.View_SentMessages("", "");

                //mDtSentSmS.Columns.Add("check", typeof(System.Boolean));

                foreach (DataColumn mCol in mDtSentSmS.Columns)
                {

                    if (mCol.ColumnName == "messagecode")
                    {
                        mDtSentSmS.Columns["messagecode"].ColumnName = "Message Code";

                    }
                    if (mCol.ColumnName == "id")
                    {
                        mDtSentSmS.Columns["id"].ColumnName = "ID";

                    }
                    if (mCol.ColumnName == "message")
                    {

                        mDtSentSmS.Columns["message"].ColumnName = "Message";

                    }
                    if (mCol.ColumnName == "phonenumber")
                    {
                        mDtSentSmS.Columns["phonenumber"].ColumnName = "Phone Number";

                    }

                    if (mCol.ColumnName == "datesent")
                    {
                        mDtSentSmS.Columns["datesent"].ColumnName = "Date Sent";

                    }

                    if (mCol.ColumnName == "sendmethod")
                    {
                        mDtSentSmS.Columns["sendmethod"].ColumnName = "Send Method";

                    }

                    if (mCol.ColumnName == "patientcode")
                    {
                        mDtSentSmS.Columns["patientcode"].ColumnName = "Patient Code";

                    }






                }

                mDtSentSmS.AcceptChanges();
                grdMain.DataSource = mDtSentSmS;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion

        #region frmSMSSentMessages_Load

        private void frmSMSSentMessages_Load(object sender, EventArgs e)
        {
            this.Load_Data();
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

                Result = pMdtSMS.Delete_SentMessage(pRow.Cells[0].Value.ToString());
                if (Result == false)
                {
                    Program.Display_Error("Unable to delete the SMS");
                    return;
                }
                Program.Display_Info("SMS Deleted successfully");
                grdMain.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       #endregion

       #region grdMain_CellContentClick
        private void grdMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                pRow = grdMain.SelectedRows[0];
               // MessageBox.Show("H");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       #endregion








    }
}