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
using DevExpress.XtraGrid;

namespace AfyaPro_NextGen
{
    public partial class frmRPDEditReport : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsReportGroups pMdtReportGroups;
        private AfyaPro_MT.clsReports pMdtReports;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataRow pSelectedRow = null;
        private bool pFirstTimeLoad = true;

        private DataTable pDtReportGroups = new DataTable("reportgroups");

        private string pReportGroupCode = "";

        #endregion

        #region frmRPDEditReport
        public frmRPDEditReport()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmRPDReports";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                this.CancelButton = cmdClose;

                pMdtReportGroups = (AfyaPro_MT.clsReportGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReportGroups),
                    Program.gMiddleTier + "clsReportGroups");

                pMdtReports = (AfyaPro_MT.clsReports)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReports),
                    Program.gMiddleTier + "clsReports");

                pDtReportGroups.Columns.Add("code", typeof(System.String));
                pDtReportGroups.Columns.Add("description", typeof(System.String));
                cboGroup.Properties.DataSource = pDtReportGroups;
                cboGroup.Properties.DisplayMember = "description";
                cboGroup.Properties.ValueMember = "code";

                this.Fill_ReportGroups();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmRPDReports_Load
        private void frmRPDReports_Load(object sender, EventArgs e)
        {
            this.Load_Controls();
            this.Mode_Edit();
        }
        #endregion

        #region frmRPDReports_Activated
        private void frmRPDReports_Activated(object sender, EventArgs e)
        {
            if (pFirstTimeLoad == true)
            {
                txtDescription.Focus();
                pFirstTimeLoad = false;
            }
        }
        #endregion

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(txbCode);
            mObjectsList.Add(txbDescription);
            mObjectsList.Add(cmdOk);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region Mode_Edit
        private void Mode_Edit()
        {
            string mFunctionName = "Mode_Edit";

            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);
                DataTable mDtReports = pMdtReports.View("code='" + pSelectedRow["code"].ToString().Trim() + "'", "", "", "");

                if (mDtReports.Rows.Count > 0)
                {
                    pReportGroupCode = pSelectedRow["groupcode"].ToString();
                    cboGroup.ItemIndex = Program.Get_LookupItemIndex(cboGroup, "code", pReportGroupCode);
                    txtCode.Text = pSelectedRow["code"].ToString().Trim();
                    txtDescription.Text = pSelectedRow["description"].ToString().Trim();
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
        private void Fill_ReportGroups()
        {
            DataRow mNewRow;
            string mFunctionName = "Fill_ReportGroups";

            try
            {
                #region ReportGroups

                pDtReportGroups.Rows.Clear();
                DataTable mDtReportGroups = pMdtReportGroups.View("", "code", Program.gLanguageName, "grdRPDReportGroups");
                foreach (DataRow mDataRow in mDtReportGroups.Rows)
                {
                    mNewRow = pDtReportGroups.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtReportGroups.Rows.Add(mNewRow);
                    pDtReportGroups.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtReportGroups.Columns)
                {
                    mDataColumn.Caption = mDtReportGroups.Columns[mDataColumn.ColumnName].Caption;
                }

                #endregion
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Data_Edit
        private void Data_Edit()
        {
            String mFunctionName = "Data_Edit";

            #region validation
            if (cboGroup.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RPD_ReportGroupDescriptionIsInvalid.ToString());
                cboGroup.Focus();
                return;
            }

            if (txtCode.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RPD_ReportCodeIsInvalid.ToString());
                txtCode.Focus();
                return;
            }

            if (txtDescription.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RPD_ReportDescriptionIsInvalid.ToString());
                txtDescription.Focus();
                return;
            }
            #endregion

            try
            {
                //edit
                pResult = pMdtReports.Edit(txtCode.Text, txtDescription.Text,
                    cboGroup.GetColumnValue("code").ToString(), Program.gCurrentUser.Code);
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

                this.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                this.Close();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Data_Fill
        private void Data_Fill(GridControl mGridControl)
        {
            string mFunctionName = "Data_Fill";

            try
            {
                //load data
                DataTable mDtReports = pMdtReports.View("", "", Program.gLanguageName, mGridControl.Name);
                mGridControl.DataSource = mDtReports;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdOk_Click
        private void cmdOk_Click(object sender, EventArgs e)
        {
            this.Data_Edit();
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