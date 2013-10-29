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
using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Controls;

namespace AfyaPro_NextGen
{
    public partial class frmSECUserGroupPrinters : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsUserGroupPrinters pMdtUserGroupPrinters;
        private AfyaPro_MT.clsUserGroups pMdtUserGroups;
        private AfyaPro_MT.clsPatientDocuments pMdtPatientDocuments;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private bool pFirstTimeLoad = true;

        private ComboBoxItemCollection pPrinters;
        private ComboBoxItemCollection pPrintedWhens;
        private DataTable pDtUserGroups = new DataTable("usergroups");
        private DataTable pDtPrinters = new DataTable("printers");

        #endregion

        #region frmSECUserGroupPrinters
        public frmSECUserGroupPrinters()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmSECUserGroupPrinters";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                
                this.CancelButton = cmdClose;

                pMdtUserGroupPrinters = (AfyaPro_MT.clsUserGroupPrinters)Activator.GetObject(
                    typeof(AfyaPro_MT.clsUserGroupPrinters),
                    Program.gMiddleTier + "clsUserGroupPrinters");

                pMdtUserGroups = (AfyaPro_MT.clsUserGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsUserGroups),
                    Program.gMiddleTier + "clsUserGroups");

                pMdtPatientDocuments = (AfyaPro_MT.clsPatientDocuments)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPatientDocuments),
                    Program.gMiddleTier + "clsPatientDocuments");

                pDtUserGroups.Columns.Add("code", typeof(System.String));
                pDtUserGroups.Columns.Add("description", typeof(System.String));
                cboUserGroup.Properties.DataSource = pDtUserGroups;
                cboUserGroup.Properties.DisplayMember = "description";
                cboUserGroup.Properties.ValueMember = "code";

                pDtPrinters.Columns.Add("documenttypecode", typeof(System.String));
                pDtPrinters.Columns.Add("documenttypedescription", typeof(System.String));
                pDtPrinters.Columns.Add("printername", typeof(System.String));
                pDtPrinters.Columns.Add("printedwhen", typeof(System.Int16));
                pDtPrinters.Columns.Add("printedwhendescription", typeof(System.String));
                pDtPrinters.Columns.Add("autoprint", typeof(System.Boolean));
                pDtPrinters.Columns.Add("printtoscreen", typeof(System.Boolean));

                pPrinters = cboprintername.Items;
                pPrintedWhens = cboprintedwhendescription.Items;

                cboprintedwhendescription.SelectedIndexChanged += new EventHandler(cboprintedwhendescription_SelectedIndexChanged);

                grdSECUserGroupPrinters.DataSource = pDtPrinters;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmSECUserGroupPrinters_Load
        private void frmSECUserGroupPrinters_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmSECUserGroupPrinters_Load";

            try
            {
                this.Load_Controls();

                this.Fill_LookupData();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmSECUserGroupPrinters_Activated
        private void frmSECUserGroupPrinters_Activated(object sender, EventArgs e)
        {
            if (pFirstTimeLoad == true)
            {
                cboUserGroup.Focus();
                pFirstTimeLoad = false;
            }
        }
        #endregion

        #region cboUserGroup_EditValueChanged
        private void cboUserGroup_EditValueChanged(object sender, EventArgs e)
        {
            this.Data_Display();
        }
        #endregion

        #region cboprintedwhendescription_SelectedIndexChanged
        void cboprintedwhendescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView mDvPrinters = new DataView();
            mDvPrinters.Table = pDtPrinters;
            mDvPrinters.Sort = "documenttypecode";

            DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                (DevExpress.XtraGrid.Views.Grid.GridView)grdSECUserGroupPrinters.MainView;

            if (mGridView.FocusedRowHandle < 0)
            {
                return;
            }

            mGridView.PostEditor();

            DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);
            int mRowIndex = mDvPrinters.Find(mSelectedRow["documenttypecode"].ToString().Trim());
            if (mRowIndex >= 0)
            {
                DataRowView mDataRowView = mDvPrinters[mRowIndex];
                mDataRowView.BeginEdit();
                mDataRowView["printedwhen"] = ((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedIndex;
                mDataRowView.EndEdit();
                pDtPrinters.AcceptChanges();
            }
        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            DataRow mNewRow;
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region usergroups

                pDtUserGroups.Rows.Clear();
                DataTable mDtUserGroups = pMdtUserGroups.View("", "code", Program.gLanguageName, "grdSECUserGroups");
                foreach (DataRow mDataRow in mDtUserGroups.Rows)
                {
                    mNewRow = pDtUserGroups.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtUserGroups.Rows.Add(mNewRow);
                    pDtUserGroups.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtUserGroups.Columns)
                {
                    mDataColumn.Caption = mDtUserGroups.Columns[mDataColumn.ColumnName].Caption;
                }

                #endregion

                #region installed printers

                for (int mIndex = 0; mIndex < System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count; mIndex++)
                {
                    string mPrinterName = System.Drawing.Printing.PrinterSettings.InstalledPrinters[mIndex];
                    pPrinters.Add(mPrinterName);
                }

                #endregion

                #region printedwhens

                DataTable mDtPrintedWhens = pMdtUserGroupPrinters.Get_PrintedWhens(Program.gLanguageName, this.Name);
                foreach (DataRow mDataRow in mDtPrintedWhens.Rows)
                {
                    pPrintedWhens.Add(mDataRow["description"].ToString().Trim());
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

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(txbUserGroup);
            mObjectsList.Add(txbReceipts);
            mObjectsList.Add(txbInvoices);
            mObjectsList.Add(documenttypecode);
            mObjectsList.Add(documenttypedescription);
            mObjectsList.Add(printername);
            mObjectsList.Add(printedwhen);
            mObjectsList.Add(printedwhendescription);
            mObjectsList.Add(autoprint);
            mObjectsList.Add(cmdOk);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region Data_Display
        private void Data_Display()
        {
            string mFunctionName = "Mode_Edit";

            try
            {
                pDtPrinters.Rows.Clear();

                if (cboUserGroup.ItemIndex == -1)
                {
                    return;
                }

                string mUserGroupCode = cboUserGroup.GetColumnValue("code").ToString().Trim();

                DataTable mDtUserGroupPrinters = pMdtUserGroupPrinters.View(
                    "machinename='" + Program.gMachineName.Trim() + "' and usergroupcode='" + mUserGroupCode + "'", "", Program.gLanguageName, this.Name);

                DataTable mDtDocuments = pMdtPatientDocuments.View("", "", Program.gLanguageName, "");

                DataView mDvUserGroupPrinters = new DataView();
                mDvUserGroupPrinters.Table = mDtUserGroupPrinters;
                mDvUserGroupPrinters.Sort = "documenttypecode";

                //receipts
                string mPrinterName = "";
                int mPrintedWhen = -1;
                string mPrintedWhenDescription = "";
                bool mAutoPrint = false;
                bool mPrintToScreen = false;

                int mRowIndex = mDvUserGroupPrinters.Find("receipts");
                if (mRowIndex >= 0)
                {
                    mPrinterName = @mDvUserGroupPrinters[mRowIndex]["printername"].ToString().Trim();
                    mAutoPrint = Convert.ToBoolean(mDvUserGroupPrinters[mRowIndex]["autoprint"]);
                    mPrintToScreen = Convert.ToBoolean(mDvUserGroupPrinters[mRowIndex]["printtoscreen"]);
                }

                DataRow mNewRow = pDtPrinters.NewRow();
                mNewRow["documenttypecode"] = "receipts";
                mNewRow["documenttypedescription"] = txbReceipts.Text;
                mNewRow["printername"] = mPrinterName;
                mNewRow["printedwhen"] = -1;
                mNewRow["printedwhendescription"] = "";
                mNewRow["autoprint"] = mAutoPrint;
                mNewRow["printtoscreen"] = mPrintToScreen;
                pDtPrinters.Rows.Add(mNewRow);
                pDtPrinters.AcceptChanges();

                //invoices
                mPrinterName = "";
                mAutoPrint = false;

                mRowIndex = mDvUserGroupPrinters.Find("invoices");
                if (mRowIndex >= 0)
                {
                    mPrinterName = @mDvUserGroupPrinters[mRowIndex]["printername"].ToString().Trim();
                    mAutoPrint = Convert.ToBoolean(mDvUserGroupPrinters[mRowIndex]["autoprint"]);
                    mPrintToScreen = Convert.ToBoolean(mDvUserGroupPrinters[mRowIndex]["printtoscreen"]);
                }

                mNewRow = pDtPrinters.NewRow();
                mNewRow["documenttypecode"] = "invoices";
                mNewRow["documenttypedescription"] = txbInvoices.Text;
                mNewRow["printername"] = mPrinterName;
                mNewRow["printedwhen"] = -1;
                mNewRow["printedwhendescription"] = "";
                mNewRow["autoprint"] = mAutoPrint;
                mNewRow["printtoscreen"] = mPrintToScreen;
                pDtPrinters.Rows.Add(mNewRow);
                pDtPrinters.AcceptChanges();

                foreach (DataRow mDataRow in mDtDocuments.Rows)
                {
                    mPrinterName = "";
                    mPrintedWhen = -1;
                    mPrintedWhenDescription = "";
                    mAutoPrint = false;

                    mRowIndex = mDvUserGroupPrinters.Find(mDataRow["code"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mPrinterName = @mDvUserGroupPrinters[mRowIndex]["printername"].ToString().Trim();
                        mPrintedWhen = Convert.ToInt16(mDvUserGroupPrinters[mRowIndex]["printedwhen"]);
                        mPrintedWhenDescription = mDvUserGroupPrinters[mRowIndex]["printedwhendescription"].ToString().Trim();
                        mAutoPrint = Convert.ToBoolean(mDvUserGroupPrinters[mRowIndex]["autoprint"]);
                        mPrintToScreen = Convert.ToBoolean(mDvUserGroupPrinters[mRowIndex]["printtoscreen"]);
                    }

                    mNewRow = pDtPrinters.NewRow();
                    mNewRow["documenttypecode"] = mDataRow["code"].ToString().Trim();
                    mNewRow["documenttypedescription"] = mDataRow["description"].ToString().Trim();
                    mNewRow["printername"] = mPrinterName;
                    mNewRow["printedwhen"] = mPrintedWhen;
                    mNewRow["printedwhendescription"] = mPrintedWhenDescription;
                    mNewRow["autoprint"] = mAutoPrint;
                    mNewRow["printtoscreen"] = mPrintToScreen;
                    pDtPrinters.Rows.Add(mNewRow);
                    pDtPrinters.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Data_Fill
        internal void Data_Fill(GridControl mGridControl)
        {
            string mFunctionName = "Data_Fill";

            try
            {
                //load data
                DataTable mDtUserGroups = pMdtUserGroups.View("", "", Program.gLanguageName, mGridControl.Name);
                mGridControl.DataSource = mDtUserGroups;
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
            String mFunctionName = "cmdOk_Click";

            try
            {
                if (cboUserGroup.ItemIndex == -1)
                {
                    return;
                }

                string mUserGroupCode = cboUserGroup.GetColumnValue("code").ToString();

                //edit 
                pResult = pMdtUserGroupPrinters.Edit(Program.gMachineName, mUserGroupCode, pDtPrinters, Program.gCurrentUser.Code);
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

                Program.Get_UserGroupPrinters();
                Program.Display_Info(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SettingsSavedSuccessfully.ToString());
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