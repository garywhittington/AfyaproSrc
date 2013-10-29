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
    public partial class frmSMSTrashMessages : DevExpress.XtraEditors.XtraForm
    {
        private AfyaPro_MT.clsSMS pMdtSMS;

        #region frmSMSTrashMessages()
        public frmSMSTrashMessages()
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

                DataTable mDtTrashSmS = new DataTable();
                mDtTrashSmS .Clear();
                grdMain.Rows.Clear();
                mDtTrashSmS  = pMdtSMS.View_Trash();

                //mDtTrashSmS .Columns.Add("check", typeof(System.Boolean));

                foreach (DataColumn mCol in mDtTrashSmS .Columns)
                {

                    if (mCol.ColumnName == "id")
                    {
                        mDtTrashSmS .Columns["id"].ColumnName = "Message Number";

                    }
                  
                    if (mCol.ColumnName == "message")
                    {

                        mDtTrashSmS .Columns["message"].ColumnName = "Message";

                    }
                    if (mCol.ColumnName == "phonenumber")
                    {
                        mDtTrashSmS .Columns["phonenumber"].ColumnName = "Phone Number";

                    }

                    if (mCol.ColumnName == "datereceived")
                    {
                        mDtTrashSmS.Columns["datereceived"].ColumnName = "Date Received";

                    }

                    if (mCol.ColumnName == "datedeleted")
                    {
                        mDtTrashSmS.Columns["datedeleted"].ColumnName = "Date Deleted";

                    }

                   






                }

                mDtTrashSmS.Columns.Remove("deletedby");
                mDtTrashSmS .AcceptChanges();
                grdMain.DataSource = mDtTrashSmS ;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion

        #region frmSMSTrashMessages_Load
        private void frmSMSTrashMessages_Load(object sender, EventArgs e)
        {
            this.Load_Data();
        }
        #endregion
    }
}