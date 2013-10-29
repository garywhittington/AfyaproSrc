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
    public partial class frmBILDebtReliefRequestsView : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsBilling pMdtBilling;
        private AfyaPro_MT.clsBillDebtReliefs pMdtBillDebtReliefs;

        private Type pType;
        private String pClassName = "";

        #endregion

        #region frmBILDebtReliefRequestsView
        public frmBILDebtReliefRequestsView()
        {
            InitializeComponent();

            String mFunctionName = "frmBILDebtReliefRequestsView";

            try
            {
                pType = this.GetType();
                pClassName = pType.FullName;

                pMdtBilling = (AfyaPro_MT.clsBilling)Activator.GetObject(
                    typeof(AfyaPro_MT.clsBilling),
                    Program.gMiddleTier + "clsBilling");

                pMdtBillDebtReliefs = (AfyaPro_MT.clsBillDebtReliefs)Activator.GetObject(
                    typeof(AfyaPro_MT.clsBillDebtReliefs),
                    Program.gMiddleTier + "clsBillDebtReliefs");

                txtDateFrom.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                txtDateTo.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                txtDateFrom.EditValue = Program.gMdiForm.txtDate.EditValue;
                txtDateTo.EditValue = Program.gMdiForm.txtDate.EditValue;

                radStatus.SelectedIndex = 1;

                //Data_Fill
                this.Data_Fill();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region mDateEdit_EditValueChanged
        void mDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            Program.AddTimeToDate((DateEdit)sender);
        }
        #endregion

        #region frmBILDebtReliefRequestsView_Load
        private void frmBILDebtReliefRequestsView_Load(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                (DevExpress.XtraGrid.Views.Grid.GridView)grdBILDebtReliefRequestsView.MainView;
            mGridView.OptionsBehavior.Editable = false;
            mGridView.OptionsView.ShowGroupPanel = true;
            mGridView.OptionsView.ShowFooter = true;
            mGridView.OptionsCustomization.AllowGroup = true;

            grdBILDebtReliefRequestsView.ForceInitialize();
            Program.Restore_GridLayout(grdBILDebtReliefRequestsView, grdBILDebtReliefRequestsView.Name);

            for (int mIndex = 0; mIndex < mGridView.Columns.Count; mIndex++)
            {
                switch (mGridView.Columns[mIndex].ColumnType.ToString().ToLower())
                {
                    case "system.double":
                        {
                            mGridView.Columns[mIndex].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            mGridView.Columns[mIndex].DisplayFormat.FormatString = "{0:c}";
                        }
                        break;
                }
            }

            this.Load_Controls();
        }
        #endregion

        #region frmBILDebtReliefRequestsView_FormClosing
        private void frmBILDebtReliefRequestsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                MemoryStream mMemoryStream = new MemoryStream();
                grdBILDebtReliefRequestsView.MainView.SaveLayoutToStream(mMemoryStream);

                Program.gMdtFacilitySetup.Save_GridLayout(grdBILDebtReliefRequestsView.Name, Program.gCurrentUser.Code, mMemoryStream.ToArray());
            }
            catch { }
        }
        #endregion

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(grpDate);
            mObjectsList.Add(txbDateFrom);
            mObjectsList.Add(txbDateTo);
            mObjectsList.Add(cmdRefresh);
            mObjectsList.Add(cmdDetails);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region Data_Fill
        internal void Data_Fill()
        {
            String mFunctionName = "Data_Fill";

            #region validation
            if (Program.IsDate(txtDateFrom.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                txtDateFrom.Focus();
                return;
            }
            if (Program.IsDate(txtDateTo.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                txtDateTo.Focus();
                return;
            }
            if (Convert.ToDateTime(txtDateTo.Text) < Convert.ToDateTime(txtDateFrom.Text))
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateRangeIsInvalid.ToString());
                return;
            }

            #endregion

            try
            {
                //date range
                bool mDateSpecified = true;
                DateTime mDateFrom = Convert.ToDateTime(txtDateFrom.EditValue);
                DateTime mDateTo = Convert.ToDateTime(txtDateTo.EditValue);

                Program.gMdiForm.Cursor = Cursors.WaitCursor;

                string mExtraFilter = "";
                switch (radStatus.SelectedIndex)
                {
                    case 1: mExtraFilter = "requeststatus=" + Convert.ToInt16(AfyaPro_Types.clsEnums.DebtReliefRequestStatus.Open); break;
                    case 2: mExtraFilter = "requeststatus=" + Convert.ToInt16(AfyaPro_Types.clsEnums.DebtReliefRequestStatus.Approved); break;
                    case 3: mExtraFilter = "requeststatus=" + Convert.ToInt16(AfyaPro_Types.clsEnums.DebtReliefRequestStatus.Rejected); break;
                }

                DataTable mDtPatientListing = pMdtBillDebtReliefs.View(mDateSpecified, mDateFrom, mDateTo,
                    mExtraFilter, "transdate desc,autocode desc", Program.gLanguageName, grdBILDebtReliefRequestsView.Name);
                grdBILDebtReliefRequestsView.DataSource = mDtPatientListing;

                Program.gMdiForm.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Program.gMdiForm.Cursor = Cursors.Default;
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdRefresh_Click
        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            this.Data_Fill();
        }
        #endregion

        #region cmdDetails_Click
        private void cmdDetails_Click(object sender, EventArgs e)
        {
            if (viewBILDebtReliefRequestsView.FocusedRowHandle < 0)
            {
                return;
            }

            DataRow mDataRow = viewBILDebtReliefRequestsView.GetDataRow(viewBILDebtReliefRequestsView.FocusedRowHandle);

            int mStatus = Convert.ToInt32(mDataRow["requeststatus"]);

            string mStatusDesc = "";
            switch (mStatus)
            {
                case 1: mStatusDesc = "Open"; break;
                case 2: mStatusDesc = "Approved"; break;
                case 3: mStatusDesc = "Rejected"; break;
            }

            frmBILDebtReliefRequestDetails mBILDebtReliefRequestDetails = new frmBILDebtReliefRequestDetails(mStatus);
            mBILDebtReliefRequestDetails.txtRequestNo.Text = mDataRow["requestno"].ToString().Trim();
            mBILDebtReliefRequestDetails.txtAccountNo.Text = mDataRow["accountcode"].ToString().Trim();
            mBILDebtReliefRequestDetails.txtAccountName.Text = mDataRow["accountdescription"].ToString().Trim();
            mBILDebtReliefRequestDetails.txtBalanceDue.Text = Convert.ToDouble(mDataRow["totalbalancedue"]).ToString("c");
            mBILDebtReliefRequestDetails.txtStatus.Text = mStatusDesc;

            mBILDebtReliefRequestDetails.ShowDialog();

            this.Data_Fill();
        }
        #endregion

        #region cmdPreviewList_Click
        private void cmdPreviewList_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdPreview_Click";

            try
            {
                Program.gMdiForm.Cursor = Cursors.WaitCursor;

                grdBILDebtReliefRequestsView.ShowPrintPreview();

                Program.gMdiForm.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Program.gMdiForm.Cursor = Cursors.Default;
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion
    }
}