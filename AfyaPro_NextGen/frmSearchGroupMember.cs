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
    public partial class frmSearchGroupMember : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsClientGroups pMdtClientGroups;
        private AfyaPro_MT.clsClientGroupMembers pMdtClientGroupMembers;
        private AfyaPro_MT.clsSearchEngine pMdtSearchEngine;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataTable pDtSearchFields = new DataTable("searchfields");
        private DataTable pDtSearchResults = new DataTable("searchresults");
        private DataTable pDtCustomerGroups = new DataTable("customergroups");

        private bool pFirstTimeLoad = true;
        private object pSourceObject;

        private string pGroupCode = "";
        internal string GroupCode
        {
            get { return pGroupCode; }
            set { pGroupCode = value; }
        }

        #endregion

        #region frmSearchGroupMember
        public frmSearchGroupMember(object mSourceObject)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmSearchGroupMember";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                
                this.CancelButton = cmdClose;
                this.pSourceObject = mSourceObject;

                pMdtClientGroupMembers = (AfyaPro_MT.clsClientGroupMembers)Activator.GetObject(
                    typeof(AfyaPro_MT.clsClientGroupMembers),
                    Program.gMiddleTier + "clsClientGroupMembers");

                pMdtSearchEngine = (AfyaPro_MT.clsSearchEngine)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSearchEngine),
                    Program.gMiddleTier + "clsSearchEngine");

                pMdtClientGroups = (AfyaPro_MT.clsClientGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsClientGroups),
                    Program.gMiddleTier + "clsClientGroups");

                pDtSearchFields.Columns.Add("fieldname", typeof(System.String));
                pDtSearchFields.Columns.Add("fielddisplayname", typeof(System.String));
                cboSearchField.Properties.DataSource = pDtSearchFields;
                cboSearchField.Properties.DisplayMember = "fielddisplayname";
                cboSearchField.Properties.ValueMember = "fieldname";
                cboSearchField.Properties.BestFit();

                pDtCustomerGroups.Columns.Add("code", typeof(System.String));
                pDtCustomerGroups.Columns.Add("description", typeof(System.String));
                cboGroup.Properties.DataSource = pDtCustomerGroups;
                cboGroup.Properties.DisplayMember = "description";
                cboGroup.Properties.ValueMember = "code";

                this.Fill_LookupData();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmSearchGroupMember_Load
        private void frmSearchGroupMember_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmSearchGroupMember_Load";

            try
            {
                this.Top = 0;
                this.Grid_Settings();
                this.Load_Controls();
                this.Load_DefaultOptions();
                this.Search();

                Program.Center_Screen(this);
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmSearchGroupMember_Activated
        private void frmSearchGroupMember_Activated(object sender, EventArgs e)
        {
            if (pFirstTimeLoad == true)
            {
                if (cboSearchField.ItemIndex == -1)
                {
                    cboSearchField.Focus();
                }
                else
                {
                    txtSearchText.Focus();
                }

                if (pGroupCode.Trim() != "")
                {
                    cboGroup.ItemIndex = Program.Get_LookupItemIndex(cboGroup, "code", pGroupCode);
                }
                else
                {
                    cboGroup.ItemIndex = 0;
                }

                pFirstTimeLoad = false;
            }
        }
        #endregion

        #region frmSearchGroupMember_FormClosing
        private void frmSearchGroupMember_FormClosing(object sender, FormClosingEventArgs e)
        {
            string mFunctionName = "frmSearchGroupMember_FormClosing";

            try
            {
                string mFieldName = "";

                if (cboSearchField.ItemIndex != -1)
                {
                    mFieldName = cboSearchField.GetColumnValue("fieldname").ToString().Trim();
                }

                Program.Save_GridLayout(grdSearch, "grdSearchGroupMember");

                Program.gMdtUserActions.Save_ClientGroupMemberSearching(
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
            mObjectsList.Add(grpFilter);
            mObjectsList.Add(txbGroup);
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

        #region Grid_Settings
        private void Grid_Settings()
        {
            string mFunctionName = "Grid_Settings";

            try
            {
                //mGridView1
                DevExpress.XtraGrid.Views.Grid.GridView mGridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
                mGridView1.OptionsBehavior.Editable = false;
                mGridView1.OptionsView.ShowGroupPanel = false;
                mGridView1.OptionsNavigation.EnterMoveNextColumn = false;
                mGridView1.DoubleClick += new EventHandler(mGridView1_DoubleClick);
                grdSearch.MainView = mGridView1;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
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
                            Program.gDtUserActions.Rows[0]["searchclientgroupmemberfieldname"].ToString().Trim().ToLower())
                        {
                            cboSearchField.ItemIndex = Program.Get_LookupItemIndex(cboSearchField, "fieldname", mDataRow["fieldname"].ToString().Trim());
                            break;
                        }
                    }
                }

                if (Program.gDtUserActions.Rows.Count > 0)
                {
                    radOptions.SelectedIndex = Convert.ToInt16(Program.gDtUserActions.Rows[0]["searchclientgroupmembersoption"]);
                    chkSearchWhileTyping.Checked = Convert.ToBoolean(Program.gDtUserActions.Rows[0]["searchclientgroupmemberswhiletyping"]);
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
                DataTable mDtSearchFields = pMdtSearchEngine.View_SearchFields("facilitycorporatemembers", Program.gLanguageName, "grdGENSearchEngine");
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

                #region customergroups

                pDtCustomerGroups.Rows.Clear();
                DataTable mDtCustomerGroups = pMdtClientGroups.View("", "code", Program.gLanguageName, "grdBLSCustomerGroups");

                mNewRow = pDtCustomerGroups.NewRow();
                mNewRow["code"] = "";
                mNewRow["description"] = "";
                pDtCustomerGroups.Rows.Add(mNewRow);
                pDtCustomerGroups.AcceptChanges();

                foreach (DataRow mDataRow in mDtCustomerGroups.Rows)
                {
                    mNewRow = pDtCustomerGroups.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtCustomerGroups.Rows.Add(mNewRow);
                    pDtCustomerGroups.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtCustomerGroups.Columns)
                {
                    mDataColumn.Caption = mDtCustomerGroups.Columns[mDataColumn.ColumnName].Caption;
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
                }

                if (txtSearchText.Text.Trim() == "")
                {
                    mSearchText = "*********************";
                }

                switch (radOptions.SelectedIndex)
                {
                    case 0: mRowFilter = mFieldName + " like '%" + mSearchText + "%'"; break;
                    case 1: mRowFilter = mFieldName + " like '" + mSearchText + "%'"; break;
                    case 2: mRowFilter = mFieldName + " like '%" + mSearchText + "'"; break;
                    case 3: mRowFilter = mFieldName + " = '" + mSearchText + "'"; break;
                }

                if (cboGroup.ItemIndex != -1)
                {
                    string mGroupCode = cboGroup.GetColumnValue("code").ToString().Trim();
                    if (mGroupCode != "")
                    {
                        mRowFilter = "(" + mRowFilter + ") and billinggroupcode='" + mGroupCode + "'";
                    }
                }

                pDtSearchResults = pMdtClientGroupMembers.View(mRowFilter, mFieldName, Program.gLanguageName, "grdSearchGroupMember");
                grdSearch.DataSource = pDtSearchResults;

                //restore current document layout
                Program.Restore_GridLayout(grdSearch, "grdSearchGroupMember");

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

                this.Close();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cboGroup_EditValueChanged
        private void cboGroup_EditValueChanged(object sender, EventArgs e)
        {
            this.Search();
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
    }
}
