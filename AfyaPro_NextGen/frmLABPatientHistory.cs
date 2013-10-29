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
    public partial class frmLABPatientHistory : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsLabTestGroups pMdtLabTestGroups;
        private AfyaPro_MT.clsLabTests pMdtLabTests;
        private AfyaPro_MT.clsPatientLabTests pMdtPatientLabTests;

        private Type pType;
        private string pClassName = "";

        private DataTable pDtGroups = new DataTable("labtestgroups");
        private DataTable pDtLabTests = new DataTable("labtests");

        private string pPatientCode = "";
        private string pGender = "";
        private int pYears = 0;
        private int pMonths = 0;

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        #endregion

        #region frmLABPatientHistory
        public frmLABPatientHistory(string mPatientCode, string mGender, int mYears, int mMonths)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmLABPatientHistory";

            try
            {
                pPatientCode = mPatientCode;
                pGender = mGender;
                pYears = mYears;
                pMonths = mMonths;

                pMdtLabTestGroups = (AfyaPro_MT.clsLabTestGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsLabTestGroups),
                    Program.gMiddleTier + "clsLabTestGroups");

                pMdtLabTests = (AfyaPro_MT.clsLabTests)Activator.GetObject(
                    typeof(AfyaPro_MT.clsLabTests),
                    Program.gMiddleTier + "clsLabTests");

                pMdtPatientLabTests = (AfyaPro_MT.clsPatientLabTests)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPatientLabTests),
                    Program.gMiddleTier + "clsPatientLabTests");

                txtDateTo.EditValue = DateTime.Now.Date;
                txtDateFrom.EditValue = DateTime.Now.Date.AddMonths(-1);

                this.Fill_PatientHistory();

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.labpatienttests_customizelayout.ToString());
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_PatientHistory
        private void Fill_PatientHistory()
        {
            string mFunctionName = "Fill_PatientHistory";

            try
            {
                if (Program.IsNullDate(txtDateFrom.EditValue) == true)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                    txtDateFrom.Focus();
                    return;
                }
                if (Program.IsNullDate(txtDateTo.EditValue) == true)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                    txtDateTo.Focus();
                    return;
                }
                if (Convert.ToDateTime(txtDateFrom.EditValue) > Convert.ToDateTime(txtDateTo.EditValue))
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateRangeIsInvalid.ToString());
                    txtDateTo.Focus();
                    return;
                }

                DateTime mDateFrom = Convert.ToDateTime(txtDateFrom.EditValue);
                DateTime mDateTo = Convert.ToDateTime(txtDateTo.EditValue);

                DataTable mDtLabTests = pMdtLabTests.View_Tests("", pYears, pMonths);
                DataTable mDtActiveDates = pMdtPatientLabTests.View_ActivePatientDates(true, mDateFrom, mDateTo, pPatientCode);
                DataTable mDtPatientTests = pMdtPatientLabTests.View(true, mDateFrom, mDateTo,
                    "patientcode='" + pPatientCode.Trim() + "'", "", "", "");

                viewLABPatientHistory.Columns.Clear();

                #region grid columns

                pDtLabTests = new DataTable("patienttests");
                pDtLabTests.Columns.Add("code", typeof(System.String));
                pDtLabTests.Columns.Add("description", typeof(System.String));
                pDtLabTests.Columns.Add("displayname", typeof(System.String));
                pDtLabTests.Columns.Add("groupcode", typeof(System.String));
                pDtLabTests.Columns.Add("groupdescription", typeof(System.String));
                pDtLabTests.Columns.Add("subgroupcode", typeof(System.String));
                pDtLabTests.Columns.Add("subgroupdescription", typeof(System.String));
                pDtLabTests.Columns.Add("resulttype", typeof(System.Int16));
                pDtLabTests.Columns.Add("equipment_lowerrange", typeof(System.Double));
                pDtLabTests.Columns.Add("equipment_upperrange", typeof(System.Double));
                pDtLabTests.Columns.Add("normal_lowerrange", typeof(System.Double));
                pDtLabTests.Columns.Add("normal_upperrange", typeof(System.Double));
                pDtLabTests.Columns.Add("normalrange", typeof(System.String));
                pDtLabTests.Columns.Add("equipmentrange", typeof(System.String));
                pDtLabTests.Columns.Add("hasnormalrange", typeof(System.Boolean));
                pDtLabTests.Columns.Add("hasequipmentrange", typeof(System.Boolean));

                foreach (DataRow mDateRow in mDtActiveDates.Rows)
                {
                    string mResultsFieldName = "results_" + Convert.ToDateTime(mDateRow["transdate"]).ToShortDateString();
                    string mResultFigureFieldName = "resultfigure_" + Convert.ToDateTime(mDateRow["transdate"]).ToShortDateString();
                    string mRemarksFieldName = "remarks_" + Convert.ToDateTime(mDateRow["transdate"]).ToShortDateString();

                    pDtLabTests.Columns.Add(mResultFigureFieldName, typeof(System.String));
                    pDtLabTests.Columns.Add(mRemarksFieldName, typeof(System.String));
                    pDtLabTests.Columns.Add(mResultsFieldName, typeof(System.String));
                    pDtLabTests.Columns[mResultsFieldName].Caption = Convert.ToDateTime(mDateRow["transdate"]).ToShortDateString();
                }

                grdLABPatientHistory.DataSource = pDtLabTests;

                viewLABPatientHistory.Columns["code"].Visible = false;
                viewLABPatientHistory.Columns["description"].Visible = false;
                viewLABPatientHistory.Columns["groupcode"].Visible = false;
                viewLABPatientHistory.Columns["groupdescription"].Visible = false;
                viewLABPatientHistory.Columns["subgroupcode"].Visible = false;
                viewLABPatientHistory.Columns["subgroupdescription"].Visible = false;
                viewLABPatientHistory.Columns["resulttype"].Visible = false;
                viewLABPatientHistory.Columns["equipment_lowerrange"].Visible = false;
                viewLABPatientHistory.Columns["equipment_upperrange"].Visible = false;
                viewLABPatientHistory.Columns["normal_lowerrange"].Visible = false;
                viewLABPatientHistory.Columns["normal_upperrange"].Visible = false;
                viewLABPatientHistory.Columns["hasnormalrange"].Visible = false;
                viewLABPatientHistory.Columns["hasequipmentrange"].Visible = false;

                foreach (DevExpress.XtraGrid.Columns.GridColumn mGridColumn in viewLABPatientHistory.Columns)
                {
                    if (mGridColumn.FieldName.ToLower().StartsWith("resultfigure_") == true
                        || mGridColumn.FieldName.ToLower().StartsWith("remarks_") == true)
                    {
                        mGridColumn.Visible = false;
                    }
                }

                viewLABPatientHistory.Columns["displayname"].Caption = "Test";
                viewLABPatientHistory.Columns["normalrange"].Caption = "Normal Range";
                viewLABPatientHistory.Columns["equipmentrange"].Caption = "Equipment Range";
                viewLABPatientHistory.Columns["groupdescription"].Caption = "GROUP";
                viewLABPatientHistory.Columns["subgroupdescription"].Caption = "SUB GROUP";
                viewLABPatientHistory.Columns["normalrange"].BestFit();
                viewLABPatientHistory.Columns["equipmentrange"].BestFit();
                viewLABPatientHistory.Columns["displayname"].Width = viewLABPatientHistory.Columns["displayname"].Width * 2;

                viewLABPatientHistory.OptionsView.ColumnAutoWidth = false;
                viewLABPatientHistory.Columns["displayname"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                viewLABPatientHistory.Columns["equipmentrange"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
                viewLABPatientHistory.Columns["normalrange"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;

                viewLABPatientHistory.Columns["groupdescription"].Group();
                viewLABPatientHistory.Columns["subgroupdescription"].Group();

                #endregion

                DataView mDvPatientTests = new DataView();
                mDvPatientTests.Table = mDtPatientTests;
                mDvPatientTests.Sort = "labtesttypecode";

                foreach (DataRow mDataRow in mDtLabTests.Rows)
                {
                    Int16 mResultType = Convert.ToInt16(mDataRow["resulttype"]);
                    string mUnits = mDataRow["units"].ToString().Trim();
                    string mNormalRange = "";
                    string mEquipmentRange = "";
                    bool mHasNormalRange = false;
                    bool mHasEquipmentRange = false;
                    double mEquipmentLowerRange = 0;
                    double mEquipmentUpperRange = 0;
                    double mNormalLowerRange = 0;
                    double mNormalUpperRange = 0;

                    DataRowView[] mDateResults = mDvPatientTests.FindRows(mDataRow["code"].ToString().Trim());

                    if (mDateResults.Length > 0)
                    {
                        #region build ranges

                        if (mResultType == (int)AfyaPro_Types.clsEnums.LabTestResultTypes.number)
                        {
                            switch (pGender.ToLower())
                            {
                                case "f"://female
                                    {
                                        mEquipmentLowerRange = Convert.ToDouble(mDataRow["equipment_femalelowerrange"]);
                                        mEquipmentUpperRange = Convert.ToDouble(mDataRow["equipment_femaleupperrange"]);
                                        mNormalLowerRange = Convert.ToDouble(mDataRow["normal_femalelowerrange"]);
                                        mNormalUpperRange = Convert.ToDouble(mDataRow["normal_femaleupperrange"]);
                                    }
                                    break;
                                case "m"://male
                                    {
                                        mEquipmentLowerRange = Convert.ToDouble(mDataRow["equipment_malelowerrange"]);
                                        mEquipmentUpperRange = Convert.ToDouble(mDataRow["equipment_maleupperrange"]);
                                        mNormalLowerRange = Convert.ToDouble(mDataRow["normal_malelowerrange"]);
                                        mNormalUpperRange = Convert.ToDouble(mDataRow["normal_maleupperrange"]);
                                    }
                                    break;
                            }

                            if (mEquipmentLowerRange != 0 || mEquipmentUpperRange != 0)
                            {
                                mEquipmentRange = mEquipmentLowerRange + " - " + mEquipmentUpperRange + " " + mUnits;
                                mHasEquipmentRange = true;
                            }

                            if (mNormalLowerRange != 0 || mNormalUpperRange != 0)
                            {
                                mNormalRange = mNormalLowerRange + " - " + mNormalUpperRange + " " + mUnits;
                                mHasNormalRange = true;
                            }
                        }

                        #endregion

                        DataRow mNewRow = pDtLabTests.NewRow();
                        mNewRow["code"] = mDataRow["code"];
                        mNewRow["description"] = mDataRow["description"];
                        mNewRow["displayname"] = mDataRow["displayname"];
                        mNewRow["groupcode"] = mDataRow["groupcode"];
                        mNewRow["groupdescription"] = mDataRow["groupdescription"];
                        mNewRow["subgroupcode"] = mDataRow["subgroupcode"];
                        mNewRow["subgroupdescription"] = mDataRow["subgroupdescription"];
                        mNewRow["resulttype"] = mDataRow["resulttype"];
                        mNewRow["equipment_lowerrange"] = mEquipmentLowerRange;
                        mNewRow["equipment_upperrange"] = mEquipmentUpperRange;
                        mNewRow["normal_lowerrange"] = mNormalLowerRange;
                        mNewRow["normal_upperrange"] = mNormalUpperRange;
                        mNewRow["normalrange"] = mNormalRange;
                        mNewRow["equipmentrange"] = mEquipmentRange;
                        mNewRow["hasnormalrange"] = mHasNormalRange;
                        mNewRow["hasequipmentrange"] = mHasEquipmentRange;

                        #region results

                        foreach (DataRowView mDateResult in mDateResults)
                        {
                            string mResultsFieldName = "results_" + Convert.ToDateTime(mDateResult["transdate"]).ToShortDateString();
                            string mResultFigureFieldName = "resultfigure_" + Convert.ToDateTime(mDateResult["transdate"]).ToShortDateString();
                            string mRemarksFieldName = "remarks_" + Convert.ToDateTime(mDateResult["transdate"]).ToShortDateString();

                            if (Convert.ToDateTime(mDateResult["transdate"]) == Convert.ToDateTime(mDateResult["transdate"]))
                            {
                                mNewRow[mResultFigureFieldName] = mDateResult["resultfigure"];
                                mNewRow[mRemarksFieldName] = mDateResult["remarks"];
                                mNewRow[mResultsFieldName] = mDateResult["results"];
                            }
                        }

                        #endregion

                        pDtLabTests.Rows.Add(mNewRow);
                        pDtLabTests.AcceptChanges();
                    }
                }

                viewLABPatientHistory.ExpandAllGroups();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmLABPatientHistory_Load
        private void frmLABPatientHistory_Load(object sender, EventArgs e)
        {
            this.Top = 0;

            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            this.pFormWidth = this.Width;
            this.pFormHeight = this.Height;

            Program.Center_Screen(this);
        }
        #endregion

        #region frmLABPatientHistory_FormClosing
        private void frmLABPatientHistory_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //layout
                if (layoutControl1.IsModified == true)
                {
                    Program.Save_FormLayout(this, layoutControl1, this.Name);
                }
            }
            catch { }
        }
        #endregion

        #region viewLABPatientHistory_RowCellStyle
        void viewLABPatientHistory_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName.Trim().ToLower().Contains("results_") == true)
                {
                    DataRow mDataRow = viewLABPatientHistory.GetDataRow(e.RowHandle);

                    Int16 mResultType = Convert.ToInt16(mDataRow["resulttype"]);

                    switch (mResultType)
                    {
                        case 1:
                            {
                                try
                                {
                                    string mFieldName = e.Column.FieldName.Replace("results_", "resultfigure_");

                                    if (mDataRow[mFieldName].ToString().Trim() != "")
                                    {
                                        double mValue = Convert.ToDouble(mDataRow[mFieldName]);
                                        double mNormal_LowerRange = Convert.ToDouble(mDataRow["normal_lowerrange"]);
                                        double mNormal_UpperRange = Convert.ToDouble(mDataRow["normal_upperrange"]);
                                        double Equipment_LowerRange = Convert.ToDouble(mDataRow["equipment_lowerrange"]);
                                        double Equipment_UpperRange = Convert.ToDouble(mDataRow["equipment_upperrange"]);

                                        //outside of normal range
                                        if ((mValue < mNormal_LowerRange || mValue > mNormal_UpperRange)
                                            && (mNormal_LowerRange != 0 && mNormal_UpperRange != 0))
                                        {
                                            e.Appearance.BackColor = txtNormalRangeColor.BackColor;
                                        }

                                        //outside of equipment range
                                        if ((mValue < Equipment_LowerRange || mValue > Equipment_UpperRange)
                                            && (Equipment_LowerRange != 0 && Equipment_UpperRange != 0))
                                        {
                                            e.Appearance.BackColor = txtEquipmentRangeColor.BackColor;
                                        }
                                    }

                                    string mRemarkFieldName = e.Column.FieldName.Replace("results_", "remarks_");
                                    string mRemarks = mDataRow[mRemarkFieldName].ToString().Trim();

                                    //remarked
                                    if (mRemarks != "")
                                    {
                                        e.Appearance.BackColor = txtRemarked.BackColor;
                                    }
                                }
                                catch { }
                            }
                            break;
                    }
                }
            }
        }
        #endregion

        #region toolTipController1_GetActiveObjectInfo
        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (!(e.SelectedControl is DevExpress.XtraGrid.GridControl))
            {
                return;
            }

            DevExpress.XtraGrid.GridControl mGridControl = (DevExpress.XtraGrid.GridControl)e.SelectedControl;
            DevExpress.XtraGrid.Views.Grid.GridView mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)mGridControl.GetViewAt(e.ControlMousePosition);

            if (mGridView != null)
            {
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo mGridHitInfo = mGridView.CalcHitInfo(e.ControlMousePosition);

                switch (mGridHitInfo.HitTest)
                {
                    case DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell:
                        {
                            DataRow mDataRow = mGridView.GetDataRow(mGridHitInfo.RowHandle);

                            if (mGridHitInfo.Column.FieldName.ToLower().StartsWith("results_") == true)
                            {
                                string mRemarksFieldName = mGridHitInfo.Column.FieldName.Replace("results_", "remarks_");
                                string mRemarks = mDataRow[mRemarksFieldName].ToString().Trim();
                                if (mRemarks != "")
                                {
                                    e.Info = new DevExpress.Utils.ToolTipControlInfo(mGridHitInfo.HitTest.ToString() + mGridHitInfo.RowHandle.ToString(), mRemarks);
                                }
                            }
                        }
                        break;
                }
            }
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region cmdApplyFilter_Click
        private void cmdApplyFilter_Click(object sender, EventArgs e)
        {
            this.Fill_PatientHistory();
        }
        #endregion
    }
}