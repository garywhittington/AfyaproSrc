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
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AfyaPro_NextGen
{
    public partial class frmSearchPatient : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsRegistrations pMdtRegistrations;
        private AfyaPro_MT.clsSearchEngine pMdtSearchEngine;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataTable pDtSearchFields = new DataTable("searchfields");
        private DataTable pDtSearchResults = new DataTable("searchresults");

        private object pSourceObject;

        private bool pSearchingDone = false;
        internal bool SearchingDone
        {
            get { return pSearchingDone; }
            set { pSearchingDone = value; }
        }

        #endregion

        #region frmSearchPatient
        public frmSearchPatient(object mSourceObject)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmSearchPatient";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                
                this.CancelButton = cmdClose;
                this.pSourceObject = mSourceObject;

                pMdtRegistrations = (AfyaPro_MT.clsRegistrations)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRegistrations),
                    Program.gMiddleTier + "clsRegistrations");

                pMdtSearchEngine = (AfyaPro_MT.clsSearchEngine)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSearchEngine),
                    Program.gMiddleTier + "clsSearchEngine");

                pDtSearchFields.Columns.Add("fieldname", typeof(System.String));
                pDtSearchFields.Columns.Add("fielddisplayname", typeof(System.String));
                cboSearchField.Properties.DataSource = pDtSearchFields;
                cboSearchField.Properties.DisplayMember = "fielddisplayname";
                cboSearchField.Properties.ValueMember = "fieldname";
                cboSearchField.Properties.BestFit();

                this.Fill_LookupData();

                //gridview properties
                gridView1.OptionsBehavior.Editable = false;
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.OptionsNavigation.EnterMoveNextColumn = false;
                gridView1.OptionsNavigation.AutoMoveRowFocus = false;
                gridView1.OptionsNavigation.AutoFocusNewRow = false;
                gridView1.OptionsView.ColumnAutoWidth = false;
                gridView1.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
                gridView1.DoubleClick += new EventHandler(mGridView1_DoubleClick);
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmSearchPatient_Load
        private void frmSearchPatient_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmSearchPatient_Load";

            try
            {
                this.Top = 0;
                this.Load_Controls();
                this.Load_DefaultOptions();
                this.Search();

                Program.Center_Screen(this);

                if (cboSearchField.ItemIndex == -1)
                {
                    cboSearchField.Focus();
                }
                else
                {
                    txtSearchText.Focus();
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmSearchPatient_FormClosing
        private void frmSearchPatient_FormClosing(object sender, FormClosingEventArgs e)
        {
            string mFunctionName = "frmSearchPatient_FormClosing";

            try
            {
                string mFieldName = "";

                if (cboSearchField.ItemIndex != -1)
                {
                    mFieldName = cboSearchField.GetColumnValue("fieldname").ToString().Trim();
                }

                Program.Save_GridLayout(grdSearch, "grdSearchPatient");

                Program.gMdtUserActions.Save_PatientSearching(
                    Program.gCurrentUser.Code.Trim(), Convert.ToInt16(chkSearchWhileTyping.Checked),
                    radOptions.SelectedIndex, mFieldName);

                Program.Get_UserActions();
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

            mObjectsList.Add(txbSearchField);
            mObjectsList.Add(txbSearchText);
            mObjectsList.Add(grpOptions);
            mObjectsList.Add(radOptions.Properties.Items[0]);
            mObjectsList.Add(radOptions.Properties.Items[1]);
            mObjectsList.Add(radOptions.Properties.Items[2]);
            mObjectsList.Add(radOptions.Properties.Items[3]);
            mObjectsList.Add(chkSearchWhileTyping);
            mObjectsList.Add(cmdOk);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region Load_DefaultOptions
        void Load_DefaultOptions()
        {
            string mFunctionName = "Load_DefaultOptions";

            try
            {
                foreach (DataRow mDataRow in pDtSearchFields.Rows)
                {
                    if (Program.gDtUserActions.Rows.Count > 0)
                    {
                        if (mDataRow["fieldname"].ToString().Trim().ToLower() ==
                            Program.gDtUserActions.Rows[0]["searchpatientfieldname"].ToString().Trim().ToLower())
                        {
                            cboSearchField.ItemIndex = Program.Get_LookupItemIndex(cboSearchField, "fieldname", mDataRow["fieldname"].ToString().Trim());
                            break;
                        }
                    }
                }

                if (Program.gDtUserActions.Rows.Count > 0)
                {
                    radOptions.SelectedIndex = Convert.ToInt16(Program.gDtUserActions.Rows[0]["searchpatientsoption"]);
                    chkSearchWhileTyping.Checked = Convert.ToBoolean(Program.gDtUserActions.Rows[0]["searchpatientswhiletyping"]);
                }

                if (cboSearchField.ItemIndex == -1)
                {
                    radOptions.SelectedIndex = 0;
                    chkSearchWhileTyping.Checked = true;
                    cboSearchField.ItemIndex = Program.Get_LookupItemIndex(cboSearchField, "fieldname", "surname");
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
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
                #region SearchFields

                pDtSearchFields.Rows.Clear();
                DataTable mDtSearchFields = pMdtSearchEngine.View_SearchFields("patients", Program.gLanguageName, "grdGENSearchEngine");
                foreach (DataRow mDataRow in mDtSearchFields.Rows)
                {
                    mNewRow = pDtSearchFields.NewRow();
                    mNewRow["fieldname"] = mDataRow["fieldname"].ToString();
                    mNewRow["fielddisplayname"] = mDataRow["fielddisplayname"].ToString();
                    pDtSearchFields.Rows.Add(mNewRow);
                    pDtSearchFields.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtSearchFields.Columns)
                {
                    mDataColumn.Caption = mDtSearchFields.Columns[mDataColumn.ColumnName].Caption;
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

        #region Search
        private void Search()
        {
            string mFunctionName = "Search";

            try
            {
                Program.gMdiForm.Cursor = Cursors.WaitCursor;

                string mFieldName = "code";
                string mSearchText = txtSearchText.Text.Trim();
                string mRowFilter = "";
                pDtSearchResults.Clear();

                if (cboSearchField.ItemIndex != -1)
                {
                    mFieldName = cboSearchField.GetColumnValue("fieldname").ToString().Trim();

                    if (mFieldName.Trim().ToLower() == "fullname")
                    {
                        mFieldName = "CONCAT(firstname,' ',othernames,' ',surname)";
                    }
                }

                if (txtSearchText.Text.Trim() == "")
                {
                    mSearchText = "&&&&&&&&&^^^^^^^^^&&&&&&&$$$$$$$$$$";
                }

                switch (radOptions.SelectedIndex)
                {
                    case 0: mRowFilter = mFieldName + " like '%" + mSearchText + "%'"; break;
                    case 1: mRowFilter = mFieldName + " like '" + mSearchText + "%'"; break;
                    case 2: mRowFilter = mFieldName + " like '%" + mSearchText + "'"; break;
                    case 3: mRowFilter = mFieldName + " = '" + mSearchText + "'"; break;
                }

                if (chkSpecific.Checked == true)
                {
                    mRowFilter = mRowFilter + " AND fromopd=1";
                }

                DataTable mDtSearchResults = pMdtSearchEngine.Get_Patients(mRowFilter, "");
                grdSearch.DataSource = mDtSearchResults;

                //restore current document layout
                Program.Restore_GridLayout(grdSearch, "grdSearchPatient");

                Program.gMdiForm.Cursor = Cursors.Default;

                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
                Program.gMdiForm.Cursor = Cursors.Default;
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region txtSearchText_EditValueChanged
        private void txtSearchText_EditValueChanged(object sender, EventArgs e)
        {
            if (chkSearchWhileTyping.Checked == true)
            {
                this.Search();
            }
        }
        #endregion

        #region txtSearchText_KeyDown
        private void txtSearchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Search();
            }
            else if (e.KeyCode == Keys.Down)
            {
                grdSearch.Focus();
            }
        }
        #endregion

        #region cmdOk_Click
        private void cmdOk_Click(object sender, EventArgs e)
        {
            this.Search();
        }
        #endregion

        #region radOptions_SelectedIndexChanged
        private void radOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Search();
        }
        #endregion

        #region chkSearchWhileTyping_CheckedChanged
        private void chkSearchWhileTyping_CheckedChanged(object sender, EventArgs e)
        {
            this.Search();
        }
        #endregion

        #region mGridView1_DoubleClick
        void mGridView1_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                sender as DevExpress.XtraGrid.Views.Grid.GridView;

            if (mGridView.FocusedRowHandle < 0)
            {
                return;
            }

            this.Get_SelectedItem();
        }
        #endregion

        #region Get_SelectedItem
        private void Get_SelectedItem()
        {
            string mFunctionName = "Get_SelectedItem";

            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)grdSearch.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow mDataRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);
                string mCode = mDataRow["code"].ToString().Trim();

                if (pSourceObject is DevExpress.XtraEditors.TextEdit)
                {
                    ((DevExpress.XtraEditors.TextEdit)pSourceObject).Text = mCode;
                }

                pSearchingDone = true;

                this.Close();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region grdSearch_ProcessGridKey
        private void grdSearch_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Get_SelectedItem();
                e.Handled = true;
            }
        }
        #endregion

        private void chkSpecific_CheckedChanged(object sender, EventArgs e)
        {
            this.Search();
        }
    }
}
