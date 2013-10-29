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
    public partial class frmIVTransferInsSwitch : DevExpress.XtraEditors.XtraForm
    {
        #region frmIVTransferInsSwitch
        public frmIVTransferInsSwitch()
        {
            InitializeComponent();
        }
        #endregion

        #region frmIVTransferInsSwitch_Load
        private void frmIVTransferInsSwitch_Load(object sender, EventArgs e)
        {
            Program.Center_Screen(this);

            radOrderTypes.SelectedIndex = -1;
        }
        #endregion

        #region cmdOk_Click
        private void cmdOk_Click(object sender, EventArgs e)
        {
            if (radOrderTypes.SelectedIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.IV_SelecteOrderTypePrompt.ToString());
                radOrderTypes.Focus();
                return;
            }

            switch (radOrderTypes.SelectedIndex)
            {
                case 0:
                    {
                        frmIVTransferInsView mIVTransferInsView = (frmIVTransferInsView)Program.gMdiForm.ActiveMdiChild;

                        string mStoreCode = "";
                        if (mIVTransferInsView.cboStore.ItemIndex != -1)
                        {
                            mStoreCode = mIVTransferInsView.cboStore.GetColumnValue("code").ToString();
                        }

                        frmIVTransferIns mIVTransferIns = new frmIVTransferIns(mStoreCode);
                        mIVTransferIns.gDataState = "New";
                        mIVTransferIns.Show();
                        this.Close();
                    }
                    break;
                case 1:
                    {
                        frmIVTransferInsView mIVTransferInsView = (frmIVTransferInsView)Program.gMdiForm.ActiveMdiChild;

                        string mStoreCode = "";
                        if (mIVTransferInsView.cboStore.ItemIndex != -1)
                        {
                            mStoreCode = mIVTransferInsView.cboStore.GetColumnValue("code").ToString();
                        }
                        
                        frmIVTransferInsStore mIVTransferInsStore = new frmIVTransferInsStore(mStoreCode);
                        mIVTransferInsStore.gDataState = "New";
                        mIVTransferInsStore.Show();
                        this.Close();
                    }
                    break;
            }
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}