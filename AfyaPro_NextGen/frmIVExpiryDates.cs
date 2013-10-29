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
    public partial class frmIVExpiryDates : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private string pLookupField;
        internal string LookupField
        {
            set { pLookupField = value; }
            get { return pLookupField; }
        }

        private string pLookupValue;
        internal string LookupValue
        {
            set { pLookupValue = value; }
            get { return pLookupValue; }
        }

        private double pQuantity;
        internal double Quantity
        {
            set { pQuantity = value; }
            get { return pQuantity; }
        }

        private DataTable pDtExpiryDates;
        internal DataTable DtExpiryDates
        {
            set { pDtExpiryDates = value; }
            get { return pDtExpiryDates; }
        }

        private DataView pDvExpiryDates;

        #endregion

        #region frmIVExpiryDates
        public frmIVExpiryDates()
        {
            InitializeComponent();
        }
        #endregion

        #region frmIVExpiryDates_Load
        private void frmIVExpiryDates_Load(object sender, EventArgs e)
        {
            Program.Center_Screen(this);

            pDvExpiryDates = new DataView();
            pDvExpiryDates.Table = pDtExpiryDates;

            pDvExpiryDates.RowFilter = pLookupField + "=" + pLookupValue;

            grdExpiryDates.DataSource = pDvExpiryDates;

            txtDate.Focus();
        }
        #endregion

        #region Add_Item
        private void Add_Item()
        {
            DataRowView mNewRowView = pDvExpiryDates.AddNew();

            mNewRowView.BeginEdit();

            mNewRowView[pLookupField] = pLookupValue;
            mNewRowView["productcode"] = txtItemCode.Text.Trim();
            if (Program.IsNullDate(txtDate.EditValue) == false)
            {
                mNewRowView["expirydate"] = Convert.ToDateTime(txtDate.EditValue);
            }
            else
            {
                mNewRowView["expirydate"] = DBNull.Value;
            }

            mNewRowView["quantity"] = Convert.ToDouble(txtQuantity.Text);

            mNewRowView.EndEdit();

            txtDate.EditValue = DBNull.Value;
            txtQuantity.Text = "";
            txtDate.Focus();
        }
        #endregion

        #region cmdAdd_Click
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            this.Add_Item();
        }
        #endregion

        #region cmdRemove_Click
        private void cmdRemove_Click(object sender, EventArgs e)
        {
            viewExpiryDate.DeleteSelectedRows();
            pDtExpiryDates.AcceptChanges();
        }
        #endregion

        #region cmdOk_Click
        private void cmdOk_Click(object sender, EventArgs e)
        {
            viewExpiryDate.PostEditor();

            double mQuantity = 0;
            foreach (DataRowView mDataRowView in pDvExpiryDates)
            {
                mQuantity = mQuantity + Convert.ToDouble(mDataRowView["quantity"]);
            }

            pQuantity = mQuantity;

            this.Close();
        }
        #endregion

        #region txtDate_EditValueChanged
        private void txtDate_EditValueChanged(object sender, EventArgs e)
        {
            txtQuantity.Focus();
        }
        #endregion

        #region txtQuantity_KeyDown
        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Add_Item();
            }
        }
        #endregion
    }
}