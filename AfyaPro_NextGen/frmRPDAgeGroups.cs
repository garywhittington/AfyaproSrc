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
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AfyaPro_NextGen
{
    public partial class frmRPDAgeGroups : Form
    {
        #region declaration

        private AfyaPro_MT.clsReporter pMdtReporter;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataTable pDtReportCharts = new DataTable("reportcharts");
        private string pReportCode = "";

        #endregion

        #region frmRPDAgeGroups
        public frmRPDAgeGroups(string mReportCode)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmRPDAgeGroups";

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                pReportCode = mReportCode;

                pMdtReporter = (AfyaPro_MT.clsReporter)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReporter),
                    Program.gMiddleTier + "clsReporter");

                pDtReportCharts.Columns.Add("description", typeof(System.String));
                pDtReportCharts.Columns.Add("value1", typeof(System.Double));
                pDtReportCharts.Columns.Add("value2", typeof(System.Double));

                grdRPDAgeGroups.DataSource = pDtReportCharts;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmRPDAgeGroups_Load
        private void frmRPDAgeGroups_Load(object sender, EventArgs e)
        {
            this.Load_Controls();
            this.Fill_Data();
        }
        #endregion

        #region Fill_Data
        private void Fill_Data()
        {
            string mFunctionName = "Fill_Data";

            try
            {
                DataTable mDtReportCharts = pMdtReporter.View_ReportCharts("reportcode='" + pReportCode + "'", "", "", "");

                pDtReportCharts.Rows.Clear();
                foreach (DataRow mDataRow in mDtReportCharts.Rows)
                {
                    DataRow mNewRow = pDtReportCharts.NewRow();
                    mNewRow["description"] = mDataRow["description"].ToString().Trim();
                    mNewRow["value1"] = Convert.ToDouble(mDataRow["value1"]);
                    mNewRow["value2"] = Convert.ToDouble(mDataRow["value2"]);
                    pDtReportCharts.Rows.Add(mNewRow);
                    pDtReportCharts.AcceptChanges();
                }
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

            mObjectsList.Add(cmdAdd);
            mObjectsList.Add(cmdRemove);
            mObjectsList.Add(description);
            mObjectsList.Add(value1);
            mObjectsList.Add(value2);
            mObjectsList.Add(cmdOk);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region cmdAdd_Click
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            viewRPDAgeGroups.AddNewRow();
        }
        #endregion

        #region cmdRemove_Click
        private void cmdRemove_Click(object sender, EventArgs e)
        {
            viewRPDAgeGroups.DeleteSelectedRows();
        }
        #endregion

        #region viewRPDAgeGroups_InitNewRow
        private void viewRPDAgeGroups_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            viewRPDAgeGroups.SetRowCellValue(e.RowHandle,
                viewRPDAgeGroups.Columns["description"], "");
            viewRPDAgeGroups.SetRowCellValue(e.RowHandle,
                viewRPDAgeGroups.Columns["value1"], -1);
            viewRPDAgeGroups.SetRowCellValue(e.RowHandle,
                viewRPDAgeGroups.Columns["value2"], -1);
        }
        #endregion

        #region cmdOk_Click
        private void cmdOk_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdOk_Click";

            try
            {
                pDtReportCharts.AcceptChanges();

                //save 
                pResult = pMdtReporter.Save_ReportChart(pReportCode, pDtReportCharts);
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

                this.Close();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
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
