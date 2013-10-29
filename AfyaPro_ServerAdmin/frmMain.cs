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

namespace AfyaPro_ServerAdmin
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        #region frmMain
        public frmMain()
        {
            InitializeComponent();
        }
        #endregion

        #region cmdConnection_Click
        private void cmdConnection_Click(object sender, EventArgs e)
        {
            frmInitialSetup mInitialSetup = new frmInitialSetup();
            mInitialSetup.ShowDialog();
        }
        #endregion

        #region cmdDataMigration_Click
        private void cmdDataMigration_Click(object sender, EventArgs e)
        {
            frmDataMigration mDataMigration = new frmDataMigration();
            mDataMigration.ShowDialog();
        }
        #endregion

        #region cmdImportSettings_Click
        private void cmdImportSettings_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}