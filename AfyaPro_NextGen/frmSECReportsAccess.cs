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
    public partial class frmSECReportsAccess : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsUserGroups pMdtUserGroups;
        private AfyaPro_MT.clsReportGroups pMdtReportGroups;
        private AfyaPro_MT.clsReports pMdtReports;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataTable pDtUserGroups = new DataTable("usergroups");
        private DataTable pDtReportGroups = new DataTable("reportgroups");
        private DataTable pDtReports = new DataTable("reports");
        private DataTable pDtAllReportGroups = new DataTable("allreportgroups");
        private DataTable pDtAllReports = new DataTable("allreports");
        private DataTable pDtUserReportGroups = new DataTable("userreportgroups");

        private DataTable pDtUserReports = new DataTable("userreports");

        private string pUserGroupCode = "";
        private string pReportGroupCode = "";
        private string pReportCode = "";

        private bool pCheckedFromReportGroup = false;
        private bool pCheckedFromReport = false;

        private bool pSettingsChanged = false;

        #endregion

        #region frmSECReportsAccess
        public frmSECReportsAccess()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmSECReportsAccess";

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                pMdtUserGroups = (AfyaPro_MT.clsUserGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsUserGroups),
                    Program.gMiddleTier + "clsUserGroups");

                pMdtReportGroups = (AfyaPro_MT.clsReportGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReportGroups),
                    Program.gMiddleTier + "clsReportGroups");

                pMdtReports = (AfyaPro_MT.clsReports)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReports),
                    Program.gMiddleTier + "clsReports");

                pDtUserGroups.Columns.Add("code", typeof(System.String));
                pDtUserGroups.Columns.Add("description", typeof(System.String));

                pDtReportGroups.Columns.Add("reportgroupselected", typeof(System.Boolean));
                pDtReportGroups.Columns.Add("reportgroupcode", typeof(System.String));
                pDtReportGroups.Columns.Add("reportgroupdescription", typeof(System.String));

                pDtReports.Columns.Add("reportselected", typeof(System.Boolean));
                pDtReports.Columns.Add("reportcode", typeof(System.String));
                pDtReports.Columns.Add("reportdescription", typeof(System.String));
                pDtReports.Columns.Add("reportview", typeof(System.Boolean));
                pDtReports.Columns.Add("reportdesign", typeof(System.Boolean));
                pDtReports.Columns.Add("reportformcustomization", typeof(System.Boolean));

                pDtUserReportGroups.Columns.Add("userreportgroupdescription", typeof(System.String));
                pDtUserReportGroups.Columns.Add("userreportdescription", typeof(System.String));
                pDtUserReportGroups.Columns.Add("userreportview", typeof(System.Boolean));
                pDtUserReportGroups.Columns.Add("userreportdesign", typeof(System.Boolean));
                pDtUserReportGroups.Columns.Add("userreportformcustomization", typeof(System.Boolean));

                grdUserGroups.DataSource = pDtUserGroups;
                grdReportGroups.DataSource = pDtReportGroups;
                grdReports.DataSource = pDtReports;
                grdUserReports.DataSource = pDtUserReportGroups;

                pDtAllReportGroups = pMdtReportGroups.View("", "", "", "");
                pDtAllReports = pMdtReports.View("", "", "", "");

                chkreportgroupselected.CheckedChanged += new EventHandler(chkreportgroupselected_CheckedChanged);
                chkreportselected.CheckedChanged += new EventHandler(chkreportselected_CheckedChanged);
                chkreportview.CheckedChanged += new EventHandler(chkreportactions_CheckedChanged);
                chkreportdesign.CheckedChanged += new EventHandler(chkreportactions_CheckedChanged);
                chkreportformcustomization.CheckedChanged += new EventHandler(chkreportactions_CheckedChanged);
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmSECReportsAccess_Load
        private void frmSECReportsAccess_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmSECReportsAccess_Load";

            try
            {
                this.Fill_UserGroups();

                this.Load_Controls();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(txbUserGroups);
            mObjectsList.Add(code);
            mObjectsList.Add(description);
            mObjectsList.Add(reportgroupdescription);
            mObjectsList.Add(reportdescription);
            mObjectsList.Add(reportview);
            mObjectsList.Add(reportdesign);
            mObjectsList.Add(reportformcustomization);
            mObjectsList.Add(userreportgroupdescription);
            mObjectsList.Add(userreportdescription);
            mObjectsList.Add(userreportview);
            mObjectsList.Add(userreportdesign);
            mObjectsList.Add(userreportformcustomization);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region Fill_UserGroups
        internal void Fill_UserGroups()
        {
            string mFunctionName = "Fill_UserGroups";

            try
            {
                pDtUserGroups.Rows.Clear();

                //load data
                DataTable mDtUserGroups = pMdtUserGroups.View("", "", Program.gLanguageName, "");

                foreach (DataRow mDataRow in mDtUserGroups.Rows)
                {
                    DataRow mNewRow = pDtUserGroups.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString().Trim();
                    mNewRow["description"] = mDataRow["description"].ToString().Trim();
                    pDtUserGroups.Rows.Add(mNewRow);
                    pDtUserGroups.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_ReportGroups
        internal void Fill_ReportGroups()
        {
            string mFunctionName = "Fill_ReportGroups";

            try
            {
                pDtReportGroups.Rows.Clear();
                pDtReports.Rows.Clear();

                DataView mDvUserReports = new DataView();
                mDvUserReports.Table = pDtUserReports;
                mDvUserReports.Sort = "reportcode";

                foreach (DataRow mDataRow in pDtAllReportGroups.Rows)
                {
                    bool mGranted = false;

                    DataView mDvReports = new DataView();
                    mDvReports.Table = pDtAllReports;
                    mDvReports.RowFilter = "groupcode='" + mDataRow["code"].ToString().Trim() + "'";

                    foreach (DataRowView mReportRow in mDvReports)
                    {
                        if (mDvUserReports.Find(mReportRow["code"].ToString().Trim()) >= 0)
                        {
                            mGranted = true;
                            break;
                        }
                    }

                    DataRow mNewRow = pDtReportGroups.NewRow();
                    mNewRow["reportgroupselected"] = mGranted;
                    mNewRow["reportgroupcode"] = mDataRow["code"].ToString().Trim();
                    mNewRow["reportgroupdescription"] = mDataRow["description"].ToString().Trim();
                    pDtReportGroups.Rows.Add(mNewRow);
                    pDtReportGroups.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_Reports
        internal void Fill_Reports()
        {
            string mFunctionName = "Fill_Reports";

            try
            {
                pDtReports.Rows.Clear();

                if (pDtAllReports == null)
                {
                    return;
                }

                DataView mDvReports = new DataView();
                mDvReports.Table = pDtAllReports;
                mDvReports.RowFilter = "groupcode='" + pReportGroupCode + "'";

                DataView mDvUserReports = new DataView();
                mDvUserReports.Table = pDtUserReports;
                mDvUserReports.Sort = "reportcode";

                foreach (DataRowView mDataRowView in mDvReports)
                {
                    bool mGranted = false;
                    bool mView = false;
                    bool mDesign = false;
                    bool mFormCustomization = false;

                    int mRowIndex = mDvUserReports.Find(mDataRowView["code"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mGranted = true;
                        mView = Convert.ToBoolean(mDvUserReports[mRowIndex]["reportview"]);
                        mDesign = Convert.ToBoolean(mDvUserReports[mRowIndex]["reportdesign"]);
                        mFormCustomization = Convert.ToBoolean(mDvUserReports[mRowIndex]["reportformcustomization"]);
                    }

                    DataRow mNewRow = pDtReports.NewRow();
                    mNewRow["reportselected"] = mGranted;
                    mNewRow["reportcode"] = mDataRowView["code"].ToString().Trim();
                    mNewRow["reportdescription"] = mDataRowView["description"].ToString().Trim();
                    mNewRow["reportview"] = mView;
                    mNewRow["reportdesign"] = mDesign;
                    mNewRow["reportformcustomization"] = mFormCustomization;
                    pDtReports.Rows.Add(mNewRow);
                    pDtReports.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_UserReportGroups
        private void Fill_UserReportGroups()
        {
            int mRowIndex = -1;
            string mFunctionName = "Fill_UserReportGroups";

            try
            {
                DataView mDvReportGroups = new DataView();
                mDvReportGroups.Table = pDtAllReportGroups;
                mDvReportGroups.Sort = "code";

                DataView mDvReports = new DataView();
                mDvReports.Table = pDtAllReports;
                mDvReports.Sort = "code";

                pDtUserReportGroups.Rows.Clear();

                foreach (DataRow mUserReportRow in pDtUserReports.Rows)
                {
                    string mReportGroupCode = "";
                    string mReportGroupDescription = "";
                    string mReportDescription = "";

                    //reportdescription
                    mRowIndex = mDvReports.Find(mUserReportRow["reportcode"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mReportGroupCode = mDvReports[mRowIndex]["groupcode"].ToString().Trim();
                        mReportDescription = mDvReports[mRowIndex]["description"].ToString().Trim();
                    }

                    //reportgroupdescription
                    mRowIndex = mDvReportGroups.Find(mReportGroupCode);
                    if (mRowIndex >= 0)
                    {
                        mReportGroupDescription = mDvReportGroups[mRowIndex]["description"].ToString().Trim();
                    }

                    DataRow mNewRow = pDtUserReportGroups.NewRow();
                    mNewRow["userreportgroupdescription"] = mReportGroupDescription;
                    mNewRow["userreportdescription"] = mReportDescription;
                    mNewRow["userreportview"] = Convert.ToBoolean(mUserReportRow["reportview"]);
                    mNewRow["userreportdesign"] = Convert.ToBoolean(mUserReportRow["reportdesign"]);
                    mNewRow["userreportformcustomization"] = Convert.ToBoolean(mUserReportRow["reportformcustomization"]);
                    pDtUserReportGroups.Rows.Add(mNewRow);
                    pDtUserReportGroups.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region viewUserGroups_FocusedRowChanged
        private void viewUserGroups_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            string mFunctionName = "viewUserGroups_FocusedRowChanged";

            try
            {
                if (e.FocusedRowHandle < 0)
                {
                    return;
                }

                if (e.PrevFocusedRowHandle >= 0)
                {
                    this.Data_Save();
                }

                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)grdUserGroups.MainView;

                DataRow mSelectedRow = mGridView.GetDataRow(e.FocusedRowHandle);
                pUserGroupCode = mSelectedRow["code"].ToString().Trim();

                //get userreports
                pDtUserReports = pMdtUserGroups.Get_UserReports(
                    "usergroupcode='" + pUserGroupCode.Trim() + "'", "");

                this.Fill_ReportGroups();
                this.Fill_UserReportGroups();

            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region viewReportGroups_FocusedRowChanged
        private void viewReportGroups_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            string mFunctionName = "viewReportGroups_FocusedRowChanged";

            try
            {
                if (e.FocusedRowHandle < 0)
                {
                    return;
                }
                
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)grdReportGroups.MainView;

                DataRow mSelectedRow = mGridView.GetDataRow(e.FocusedRowHandle);
                pReportGroupCode = mSelectedRow["reportgroupcode"].ToString().Trim();
                txbModules.Text = mSelectedRow["reportgroupdescription"].ToString().Trim();

                this.Fill_Reports();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region viewReports_FocusedRowChanged
        private void viewReports_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            string mFunctionName = "viewReports_FocusedRowChanged";

            try
            {
                if (e.FocusedRowHandle < 0)
                {
                    return;
                }
                
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)grdReports.MainView;

                DataRow mSelectedRow = mGridView.GetDataRow(e.FocusedRowHandle);
                pReportCode = mSelectedRow["reportcode"].ToString().Trim();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region chkreportgroupselected_CheckedChanged
        void chkreportgroupselected_CheckedChanged(object sender, EventArgs e)
        {
            if (pCheckedFromReportGroup == true)
            {
                grdReportGroups.FocusedView.PostEditor();

                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)grdReportGroups.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }
                
                DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                DataView mDvReports = new DataView();
                mDvReports.Table = pDtAllReports;
                mDvReports.RowFilter = "groupcode='" + mSelectedRow["reportgroupcode"].ToString().Trim() + "'";

                DataView mDvUserReports = new DataView();
                mDvUserReports.Table = pDtUserReports;
                mDvUserReports.Sort = "reportcode";

                foreach (DataRowView mReportRow in mDvReports)
                {
                    //delete
                    int mRowIndex = mDvUserReports.Find(mReportRow["code"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mDvUserReports[mRowIndex].Delete();
                        pDtUserReports.AcceptChanges();
                    }

                    if (Convert.ToBoolean(mSelectedRow["reportgroupselected"]) == true)
                    {
                        //add
                        DataRowView mDataRowView = mDvUserReports.AddNew();
                        mDataRowView["usergroupcode"] = pUserGroupCode;
                        mDataRowView["reportcode"] = mReportRow["code"].ToString().Trim();
                        mDataRowView["reportview"] = true;
                        mDataRowView["reportdesign"] = true;
                        mDataRowView["reportformcustomization"] = true;
                        mDataRowView.EndEdit();
                        pDtUserReports.AcceptChanges();
                    }
                }

                this.Fill_Reports();
                this.Fill_UserReportGroups();

                this.Data_Save();
            }
        }
        #endregion

        #region chkreportselected_CheckedChanged
        void chkreportselected_CheckedChanged(object sender, EventArgs e)
        {
            if (pCheckedFromReport == true)
            {
                grdReports.FocusedView.PostEditor();

                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)grdReports.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                DataView mDvUserReports = new DataView();
                mDvUserReports.Table = pDtUserReports;
                mDvUserReports.Sort = "reportcode";

                //delete
                int mRowIndex = mDvUserReports.Find(mSelectedRow["reportcode"].ToString().Trim());
                if (mRowIndex >= 0)
                {
                    mDvUserReports[mRowIndex].Delete();
                    pDtUserReports.AcceptChanges();

                    mSelectedRow.BeginEdit();
                    mSelectedRow["reportview"] = false;
                    mSelectedRow["reportdesign"] = false;
                    mSelectedRow["reportformcustomization"] = false;
                    mSelectedRow.EndEdit();
                    pDtReports.AcceptChanges();
                }

                if (Convert.ToBoolean(mSelectedRow["reportselected"]) == true)
                {
                    //add
                    mSelectedRow.BeginEdit();
                    mSelectedRow["reportview"] = true;
                    mSelectedRow["reportdesign"] = true;
                    mSelectedRow["reportformcustomization"] = true;
                    mSelectedRow.EndEdit();
                    pDtReports.AcceptChanges();

                    DataRowView mDataRowView = mDvUserReports.AddNew();
                    mDataRowView["usergroupcode"] = pUserGroupCode;
                    mDataRowView["reportcode"] = mSelectedRow["reportcode"].ToString().Trim();
                    mDataRowView["reportview"] = mSelectedRow["reportview"];
                    mDataRowView["reportdesign"] = mSelectedRow["reportdesign"];
                    mDataRowView["reportformcustomization"] = mSelectedRow["reportformcustomization"];
                    mDataRowView.EndEdit();
                    pDtUserReports.AcceptChanges();
                }

                this.Fill_UserReportGroups();

                this.Data_Save();
            }
        }
        #endregion

        #region chkreportactions_CheckedChanged
        void chkreportactions_CheckedChanged(object sender, EventArgs e)
        {
            grdReports.FocusedView.PostEditor();

            DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                (DevExpress.XtraGrid.Views.Grid.GridView)grdReports.MainView;

            if (mGridView.FocusedRowHandle < 0)
            {
                return;
            }
            
            DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

            DataView mDvUserReports = new DataView();
            mDvUserReports.Table = pDtUserReports;
            mDvUserReports.Sort = "reportcode";

            int mRowIndex = mDvUserReports.Find(mSelectedRow["reportcode"].ToString().Trim());
            if (mRowIndex >= 0)
            {
                DataRowView mDataRowView = mDvUserReports[mRowIndex];
                mDataRowView.BeginEdit();
                mDataRowView["reportview"] = Convert.ToBoolean(mSelectedRow["reportview"]);
                mDataRowView["reportdesign"] = Convert.ToBoolean(mSelectedRow["reportdesign"]);
                mDataRowView["reportformcustomization"] = Convert.ToBoolean(mSelectedRow["reportformcustomization"]);
                mDataRowView.EndEdit();
                pDtUserReports.AcceptChanges();
            }
        }
        #endregion

        #region grdReportGroups_Enter
        private void grdReportGroups_Enter(object sender, EventArgs e)
        {
            pCheckedFromReport = false;
            pCheckedFromReportGroup = true;
        }
        #endregion

        #region grdReports_Enter
        private void grdReports_Enter(object sender, EventArgs e)
        {
            pCheckedFromReportGroup = false;
            pCheckedFromReport = true;
        }
        #endregion

        #region Data_Save
        private void Data_Save()
        {
            String mFunctionName = "Data_Save";

            try
            {
                
                pResult = pMdtUserGroups.Save_ReportsAccess(pUserGroupCode, pDtUserReports,
                    Program.gCurrentUser.Code);
                if (pResult.Exe_Result == 0)
                {
                    Program.Display_Error(pResult.Exe_Message);
                    return;
                }
                if (pResult.Exe_Result == -1)
                {
                    Program.Display_Server_Error(pResult.Exe_Message);
                    return;
                }
               
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmSECReportsAccess_FormClosing
        private void frmSECReportsAccess_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Data_Save();
        }
        #endregion
    }
}