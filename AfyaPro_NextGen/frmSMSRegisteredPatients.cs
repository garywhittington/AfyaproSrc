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
    public partial class frmSMSRegisteredPatients : DevExpress.XtraEditors.XtraForm
    {
        private AfyaPro_MT.clsSMS pMdtSMS;

        #region frmSMSRegisteredPatients()
        public frmSMSRegisteredPatients()
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
                mDtTrashSmS.Clear();
                grdMain.Rows.Clear();
                mDtTrashSmS = pMdtSMS.Get_MobileRegistrations(1);

                //mDtTrashSmS .Columns.Add("check", typeof(System.Boolean));

                foreach (DataColumn mCol in mDtTrashSmS.Columns)
                {

                    

                    if (mCol.ColumnName == "code")
                    {

                        mDtTrashSmS.Columns["code"].ColumnName = "Patient Code";

                    }
                    if (mCol.ColumnName == "surname")
                    {
                        mDtTrashSmS.Columns["surname"].ColumnName = "Surname";

                    }

                    if (mCol.ColumnName == "firstname")
                    {
                        mDtTrashSmS.Columns["firstname"].ColumnName = "First Name";

                    }

                    if (mCol.ColumnName == "birthdate")
                    {
                        mDtTrashSmS.Columns["birthdate"].ColumnName = "Date of Birth";

                    }

                    if (mCol.ColumnName == "regdate")
                    {
                        mDtTrashSmS.Columns["regdate"].ColumnName = "Reg. Date";

                    }
                    if (mCol.ColumnName == "district")
                    {
                        mDtTrashSmS.Columns["district"].ColumnName = "District";

                    }
                    if (mCol.ColumnName == "village")
                    {
                        mDtTrashSmS.Columns["village"].ColumnName = "Village";

                    }
                    if (mCol.ColumnName == "phone_num")
                    {
                        mDtTrashSmS.Columns["phone_num"].ColumnName = "Phone";

                    }

                    if (mCol.ColumnName == "groupcode")
                    {
                        mDtTrashSmS.Columns["groupcode"].ColumnName = "Group";

                    }

                  


                }

                // mDtTrashSmS.Columns.Remove("deletedby");
                mDtTrashSmS.Columns.Remove("preg_age");
                mDtTrashSmS.Columns.Remove("registered");
                mDtTrashSmS.Columns.Remove("autocode");
                mDtTrashSmS.Columns.Remove("edd");

                mDtTrashSmS.AcceptChanges();
                grdMain.DataSource = mDtTrashSmS;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion

        #region frmSMSRegisteredPatients_Load
        private void frmSMSRegisteredPatients_Load(object sender, EventArgs e)
        {
            this.Load_Data();
        }
        #endregion
    }
}