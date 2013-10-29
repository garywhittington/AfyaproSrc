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
    public partial class frmCTCTransferIns : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private Type pType;
        private string pClassName = "";

        private AfyaPro_MT.clsReporter pMdtReporter;

        private DataTable pDtAllData = new DataTable("alldata");

        private DataTable pDtData;

        private string pSelectedCodes = "";
        internal string SelectedCodes
        {
            set { pSelectedCodes = value; }
            get { return pSelectedCodes; }
        }

        #endregion

        #region frmCTCTransferIns

        public frmCTCTransferIns(DataTable mDtData)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmDXTPrescribeLabTests";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                this.pDtData = mDtData;

                pMdtReporter = (AfyaPro_MT.clsReporter)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReporter),
                    Program.gMiddleTier + "clsReporter");

                pDtAllData.Columns.Add("selected", typeof(System.Boolean));
                pDtAllData.Columns.Add("code", typeof(System.String));
                pDtAllData.Columns.Add("description", typeof(System.String));

                grdCTCTransferIns.DataSource = pDtAllData;

                this.Fill_Data();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }

        #endregion

        #region Fill_Data
        private void Fill_Data()
        {
            string mFunctionName = "Fill_Data";

            try
            {
                DataTable mDtData = pMdtReporter.View_LookupData("ctc_transferins", "code,description", "", "", Program.gLanguageName, "grdCTCTransferIns");

                DataView mDvData = new DataView();
                mDvData.Table = pDtData;
                mDvData.Sort = "code";

                foreach (DataRow mDataRow in mDtData.Rows)
                {
                    bool mSelected = false;
                    int mRowIndex = mDvData.Find(mDataRow["code"]);

                    if (mRowIndex >= 0)
                    {
                        mSelected = true;
                    }

                    DataRow mNewRow = pDtAllData.NewRow();
                    mNewRow["selected"] = mSelected;
                    mNewRow["code"] = mDataRow["code"];
                    mNewRow["description"] = mDataRow["description"];
                    pDtAllData.Rows.Add(mNewRow);
                    pDtAllData.AcceptChanges();
                }
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
            pDtData.Rows.Clear();

            pSelectedCodes = "";
            foreach (DataRow mDataRow in pDtAllData.Rows)
            {
                if (Convert.ToBoolean(mDataRow["selected"]) == true)
                {
                    DataRow mNewRow = pDtData.NewRow();
                    mNewRow["code"] = mDataRow["code"];
                    pDtData.Rows.Add(mNewRow);
                    pDtData.AcceptChanges();

                    if (pSelectedCodes.Trim() == "")
                    {
                        pSelectedCodes = mDataRow["code"].ToString().Trim();
                    }
                    else
                    {
                        pSelectedCodes = pSelectedCodes + "; " + mDataRow["code"].ToString().Trim();
                    }
                }
            }

            this.Close();
        }
        #endregion
    }
}