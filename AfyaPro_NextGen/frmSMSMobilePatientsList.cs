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
    public partial class frmSMSMobilePatientsList : DevExpress.XtraEditors.XtraForm
    {
        private AfyaPro_MT.clsSMS pMdtSMS;
        public frmSMSMobilePatientsList()
        {
            pMdtSMS = (AfyaPro_MT.clsSMS)Activator.GetObject(
                  typeof(AfyaPro_MT.clsSMS),
                  Program.gMiddleTier + "clsSMS");
            InitializeComponent();
        }

        #region frmSMSMobilePatientsList_Load(
        private void frmSMSMobilePatientsList_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmSMSMobilePatientsList_Load(";
            try
            {
                //load data

                DataTable mDtGroups = new DataTable();
                mDtGroups = pMdtSMS.Get_SMSPatientsList();
               
                foreach (DataColumn mCol in mDtGroups.Columns)
                {

                    if (mCol.ColumnName == "code")
                    {
                        mDtGroups.Columns["code"].ColumnName = "Patient Code";

                    }
                    if (mCol.ColumnName == "fullname")
                    {

                        mDtGroups.Columns["fullname"].ColumnName = "Patient Name";

                    }
                    if (mCol.ColumnName == "phonenumber")
                    {
                        mDtGroups.Columns["phonenumber"].ColumnName = "Phone";

                    }
                    if (mCol.ColumnName == "colgroup")
                    {
                        mDtGroups.Columns["colgroup"].ColumnName = "Group";

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
        #endregion
    }
}