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
    public partial class frmOPDPatientListing : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsRegistrations pMdtRegistrations;

        private Type pType;
        private String pClassName = "";

        #endregion

        #region frmOPDPatientListing
        public frmOPDPatientListing()
        {
            InitializeComponent();

            String mFunctionName = "frmOPDPatientListing";

            try
            {
                pType = this.GetType();
                pClassName = pType.FullName;

                pMdtRegistrations = (AfyaPro_MT.clsRegistrations)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRegistrations),
                    Program.gMiddleTier + "clsRegistrations");

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

        #region frmOPDPatientListing_Load
        private void frmOPDPatientListing_Load(object sender, EventArgs e)
        {
            chkOPD.Checked = false;
            chkIPD.Checked = false;

            if (this.Name.ToLower() == "frmopdpatientlisting")
            {
                chkOPD.Checked = true;
            }
            if (this.Name.ToLower() == "frmipdpatientlisting")
            {
                chkIPD.Checked = true;
            }

            chkNew.Checked = true;
            chkReAttendance.Checked = true;

            //Data_Fill
            this.Data_Fill();

            DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                (DevExpress.XtraGrid.Views.Grid.GridView)grdOPDPatientListing.MainView;
            mGridView.OptionsBehavior.Editable = false;
            mGridView.OptionsView.ShowGroupPanel = true;
            mGridView.OptionsView.ShowFooter = true;
            mGridView.OptionsCustomization.AllowGroup = true;
            mGridView.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "patientid");

            grdOPDPatientListing.ForceInitialize();
            Program.Restore_GridLayout(grdOPDPatientListing, grdOPDPatientListing.Name);

            this.Load_Controls();
        }
        #endregion

        #region frmOPDPatientListing_FormClosing
        private void frmOPDPatientListing_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                MemoryStream mMemoryStream = new MemoryStream();
                grdOPDPatientListing.MainView.SaveLayoutToStream(mMemoryStream);

                Program.gMdtFacilitySetup.Save_GridLayout(grdOPDPatientListing.Name, Program.gCurrentUser.Code, mMemoryStream.ToArray());
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
            mObjectsList.Add(grpDepartments);
            mObjectsList.Add(chkIPD);
            mObjectsList.Add(chkOPD);
            mObjectsList.Add(grpRegisterType);
            mObjectsList.Add(chkNew);
            mObjectsList.Add(chkReAttendance);
            mObjectsList.Add(cmdRefresh);
            mObjectsList.Add(cmdPreview);

            Program.Apply_Language("frmOPDPatientListing", mObjectsList);
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

            if (chkOPD.Checked == false && chkIPD.Checked == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_TickDepartments.ToString());
                return;
            }

            if (chkNew.Checked == false && chkReAttendance.Checked == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_TickAttendanceTypes.ToString());
                return;
            }
            #endregion

            try
            {
                //date range
                bool mDateSpecified = true;
                DateTime mDateFrom = Convert.ToDateTime(txtDateFrom.EditValue);
                DateTime mDateTo = Convert.ToDateTime(txtDateTo.EditValue);

                //categories
                string mCategoriesFilter = "";
                if (chkOPD.Checked == true)
                {
                    mCategoriesFilter = "department='" + AfyaPro_Types.clsEnums.PatientCategories.OPD.ToString() + "'";
                }
                if (chkIPD.Checked == true)
                {
                    if (mCategoriesFilter.Trim() == "")
                    {
                        mCategoriesFilter = "department='" + AfyaPro_Types.clsEnums.PatientCategories.IPD.ToString() + "'";
                    }
                    else
                    {
                        mCategoriesFilter = mCategoriesFilter + " or department='" + AfyaPro_Types.clsEnums.PatientCategories.IPD.ToString() + "'";
                    }
                }

                //registrystatus
                string mRegistryStatusFilter = "";
                if (chkNew.Checked == true)
                {
                    mRegistryStatusFilter = "registrystatus='" + AfyaPro_Types.clsEnums.RegistryStatus.New.ToString() + "'";
                }
                if (chkReAttendance.Checked == true)
                {
                    if (mRegistryStatusFilter.Trim() == "")
                    {
                        mRegistryStatusFilter = "registrystatus='" + AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString() + "'";
                    }
                    else
                    {
                        mRegistryStatusFilter = mRegistryStatusFilter + " or registrystatus='" + AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString() + "'";
                    }
                }

                string mExtraFilter = "(" + mCategoriesFilter + ") and (" + mRegistryStatusFilter + ")";

                Program.gMdiForm.Cursor = Cursors.WaitCursor;

                DataTable mDtPatientListing = pMdtRegistrations.View_ArchiveBookings(mDateSpecified, mDateFrom, mDateTo,
                    mExtraFilter, "bookdate desc,autocode desc", Program.gLanguageName, grdOPDPatientListing.Name);
                grdOPDPatientListing.DataSource = mDtPatientListing;

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

                grdOPDPatientListing.ShowPrintPreview();

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