﻿/*
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
    public partial class frmLABListing : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsPatientLabTests pMdtPatientLabTests;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private String pClassName = "";

        #endregion

        #region frmLABListing
        public frmLABListing()
        {
            InitializeComponent();

            String mFunctionName = "frmLABListing";

            try
            {
                pType = this.GetType();
                pClassName = pType.FullName;

                pMdtPatientLabTests = (AfyaPro_MT.clsPatientLabTests)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPatientLabTests),
                    Program.gMiddleTier + "clsPatientLabTests");

                txtDateFrom.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                txtDateTo.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                txtDateFrom.EditValue = Program.gMdiForm.txtDate.EditValue;
                txtDateTo.EditValue = Program.gMdiForm.txtDate.EditValue;
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

        #region frmLABListing_Load
        private void frmLABListing_Load(object sender, EventArgs e)
        {
            //Data_Fill
            this.Data_Fill();

            DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                (DevExpress.XtraGrid.Views.Grid.GridView)grdLABListing.MainView;
            mGridView.OptionsBehavior.Editable = false;
            mGridView.OptionsView.ShowGroupPanel = true;
            mGridView.OptionsView.ShowFooter = true;
            mGridView.OptionsCustomization.AllowGroup = true;
            mGridView.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "patientid");

            grdLABListing.ForceInitialize();
            Program.Restore_GridLayout(grdLABListing, grdLABListing.Name);

            this.Load_Controls();
        }
        #endregion

        #region frmLABListing_FormClosing
        private void frmLABListing_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                MemoryStream mMemoryStream = new MemoryStream();
                grdLABListing.MainView.SaveLayoutToStream(mMemoryStream);

                Program.gMdtFacilitySetup.Save_GridLayout(grdLABListing.Name, Program.gCurrentUser.Code, mMemoryStream.ToArray());
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
            mObjectsList.Add(cmdPreview);

            Program.Apply_Language("frmLABListing", mObjectsList);
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

                DataTable mDtPatientListing = pMdtPatientLabTests.View(mDateSpecified, mDateFrom, mDateTo,
                    "", "transdate desc,autocode desc", Program.gLanguageName, grdLABListing.Name);
                grdLABListing.DataSource = mDtPatientListing;

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

        #region cmdPreview_Click
        private void cmdPreview_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdPreview_Click";

            try
            {
                Program.gMdiForm.Cursor = Cursors.WaitCursor;

                grdLABListing.ShowPrintPreview();

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