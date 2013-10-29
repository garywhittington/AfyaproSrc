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
    public partial class frmSECGroupsAccess : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsUserGroups pMdtUserGroups;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataTable pDtUserGroups = new DataTable("usergroups");
        private DataTable pDtModules = new DataTable("modules");
        private DataTable pDtModuleItems = new DataTable("moduleitems");
        private DataTable pDtAccessFunctions = new DataTable("accessfunctions");
        private DataTable pDtUserModules = new DataTable("usermodules");

        private DataTable pDtAllAccessFunctions = new DataTable("allfuctionaccesskeys");
        private DataTable pDtUserModuleItems = new DataTable("moduleitems");
        private DataTable pDtUserFunctionAccessKeys = new DataTable("fuctionaccesskeys");

        private string pUserGroupCode = "";
        private string pModuleKey = "";
        private string pModuleItemKey = "";
        private string pFunctionsPrefix = "";

        private bool pCheckedFromModule = false;
        private bool pCheckedFromModuleItem = false;
        private bool pCheckedFromActions = false;

        #endregion

        #region frmSECGroupsAccess
        public frmSECGroupsAccess()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmSECGroupsAccess";

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                pMdtUserGroups = (AfyaPro_MT.clsUserGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsUserGroups),
                    Program.gMiddleTier + "clsUserGroups");

                pDtUserGroups.Columns.Add("code", typeof(System.String));
                pDtUserGroups.Columns.Add("description", typeof(System.String));

                pDtModules.Columns.Add("moduleselected", typeof(System.Boolean));
                pDtModules.Columns.Add("modulekey", typeof(System.String));
                pDtModules.Columns.Add("moduletext", typeof(System.String));

                pDtModuleItems.Columns.Add("moduleitemselected", typeof(System.Boolean));
                pDtModuleItems.Columns.Add("moduleitemkey", typeof(System.String));
                pDtModuleItems.Columns.Add("moduleitemtext", typeof(System.String));
                pDtModuleItems.Columns.Add("functionsprefix", typeof(System.String));

                pDtUserModules.Columns.Add("usermoduletext", typeof(System.String));
                pDtUserModules.Columns.Add("usermoduleitemtext", typeof(System.String));
                pDtUserModules.Columns.Add("userfunctionaccesstext", typeof(System.String));

                pDtAccessFunctions.Columns.Add("functionaccessselected", typeof(System.Boolean));
                pDtAccessFunctions.Columns.Add("functionaccesskey", typeof(System.String));
                pDtAccessFunctions.Columns.Add("functionaccesstext", typeof(System.String));

                grdUserGroups.DataSource = pDtUserGroups;
                grdModules.DataSource = pDtModules;
                grdFunctions.DataSource = pDtModuleItems;
                grdActions.DataSource = pDtAccessFunctions;
                grdUserModules.DataSource = pDtUserModules;

                chkmoduleselected.CheckedChanged += new EventHandler(chkmoduleselect_CheckedChanged);
                chkmoduleitemselected.CheckedChanged += new EventHandler(chkmoduleitemselected_CheckedChanged);
                chkfunctionaccessselected.CheckedChanged += new EventHandler(chkfunctionaccessselected_CheckedChanged);
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmSECGroupsAccess_Load
        private void frmSECGroupsAccess_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmSECGroupsAccess_Load";

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
            mObjectsList.Add(moduletext);
            mObjectsList.Add(moduleitemtext);
            mObjectsList.Add(functionaccesstext);
            mObjectsList.Add(functionaccesstext);
            mObjectsList.Add(usermoduletext);
            mObjectsList.Add(usermoduleitemtext);
            mObjectsList.Add(userfunctionaccesstext);

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

        #region Fill_Modules
        internal void Fill_Modules()
        {
            string mFunctionName = "Fill_Modules";

            try
            {
                pDtModules.Rows.Clear();
                pDtModuleItems.Rows.Clear();
                pDtAccessFunctions.Rows.Clear();

                DataView mDvUserModuleItems = new DataView();
                mDvUserModuleItems.Table = pDtUserModuleItems;
                mDvUserModuleItems.Sort = "moduleitemkey";

                foreach (DataRow mDataRow in Program.gDtModules.Rows)
                {
                    bool mGranted = false;

                    DataView mDvModuleItems = new DataView();
                    mDvModuleItems.Table = Program.gDtModuleItems;
                    mDvModuleItems.RowFilter = "modulekey='" + mDataRow["modulekey"].ToString().Trim() + "'";

                    foreach (DataRowView mModuleItemRow in mDvModuleItems)
                    {
                        if (mDvUserModuleItems.Find(mModuleItemRow["moduleitemkey"].ToString().Trim()) >= 0)
                        {
                            mGranted = true;
                            break;
                        }
                    }

                    DataRow mNewRow = pDtModules.NewRow();
                    mNewRow["moduleselected"] = mGranted;
                    mNewRow["modulekey"] = mDataRow["modulekey"].ToString().Trim();
                    mNewRow["moduletext"] = mDataRow["moduletext"].ToString().Trim();
                    pDtModules.Rows.Add(mNewRow);
                    pDtModules.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_ModuleItems
        internal void Fill_ModuleItems()
        {
            string mFunctionName = "Fill_ModuleItems";

            try
            {
                pDtModuleItems.Rows.Clear();
                pDtAccessFunctions.Rows.Clear();

                if (Program.gDtModuleItems == null)
                {
                    return;
                }

                DataView mDvModuleItems = new DataView();
                mDvModuleItems.Table = Program.gDtModuleItems;
                mDvModuleItems.RowFilter = "modulekey='" + pModuleKey + "'";

                DataView mDvUserModuleItems = new DataView();
                mDvUserModuleItems.Table = pDtUserModuleItems;
                mDvUserModuleItems.Sort = "moduleitemkey";

                foreach (DataRowView mDataRowView in mDvModuleItems)
                {
                    bool mGranted = false;
                    if (mDvUserModuleItems.Find(mDataRowView["moduleitemkey"].ToString().Trim()) >= 0)
                    {
                        mGranted = true;
                    }

                    DataRow mNewRow = pDtModuleItems.NewRow();
                    mNewRow["moduleitemselected"] = mGranted;
                    mNewRow["moduleitemkey"] = mDataRowView["moduleitemkey"].ToString().Trim();
                    mNewRow["moduleitemtext"] = mDataRowView["moduleitemtext"].ToString().Trim();
                    mNewRow["functionsprefix"] = mDataRowView["functionsprefix"].ToString().Trim();
                    pDtModuleItems.Rows.Add(mNewRow);
                    pDtModuleItems.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_AccessFunctions
        internal void Fill_AccessFunctions()
        {
            string mFunctionName = "Fill_AccessFunctions";

            try
            {
                pDtAccessFunctions.Rows.Clear();

                DataView pDvAllAccessFunctions = new DataView();
                pDvAllAccessFunctions.Table = Program.gDtAccessFunctions;
                pDvAllAccessFunctions.RowFilter = "functionaccesskey like '" + pFunctionsPrefix + "_%'";

                DataView mDvUserFunctionAccessKeys = new DataView();
                mDvUserFunctionAccessKeys.Table = pDtUserFunctionAccessKeys;
                mDvUserFunctionAccessKeys.Sort = "functionaccesskey";

                foreach (DataRowView mDataRowView in pDvAllAccessFunctions)
                {
                    bool mGranted = false;
                    if (mDvUserFunctionAccessKeys.Find(mDataRowView["functionaccesskey"].ToString().Trim()) >= 0)
                    {
                        mGranted = true;
                    }

                    DataRow mNewRow = pDtAccessFunctions.NewRow();
                    mNewRow["functionaccessselected"] = mGranted;
                    mNewRow["functionaccesskey"] = mDataRowView["functionaccesskey"].ToString().Trim();
                    mNewRow["functionaccesstext"] = mDataRowView["functionaccesstext"].ToString().Trim();
                    pDtAccessFunctions.Rows.Add(mNewRow);
                    pDtAccessFunctions.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_UserModules
        private void Fill_UserModules()
        {
            int mRowIndex = -1;
            string mFunctionName = "Fill_UserModules";

            try
            {
                DataView mDvModules = new DataView();
                mDvModules.Table = Program.gDtModules;
                mDvModules.Sort = "modulekey";

                DataView mDvModuleItems = new DataView();
                mDvModuleItems.Table = Program.gDtModuleItems;
                mDvModuleItems.Sort = "moduleitemkey";

                DataView mDvAccessFunctions = new DataView();
                mDvAccessFunctions.Table = Program.gDtAccessFunctions;

                DataView mDvUserFunctionAccessKeys = new DataView();
                mDvUserFunctionAccessKeys.Table = pDtUserFunctionAccessKeys;
                mDvUserFunctionAccessKeys.Sort = "functionaccesskey";

                pDtUserModules.Rows.Clear();

                foreach (DataRow mModuleItemDataRow in pDtUserModuleItems.Rows)
                {
                    string mModuleItemText = "";
                    string mModuleKey = "";
                    string mModuleText = "";
                    string mFunctionsPrefix = "";

                    //modulekey
                    mRowIndex = mDvModuleItems.Find(mModuleItemDataRow["moduleitemkey"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mModuleKey = mDvModuleItems[mRowIndex]["modulekey"].ToString().Trim();
                        mModuleItemText = mDvModuleItems[mRowIndex]["moduleitemtext"].ToString().Trim();
                        mFunctionsPrefix = mDvModuleItems[mRowIndex]["functionsprefix"].ToString().Trim();
                    }

                    //moduletext
                    mRowIndex = mDvModules.Find(mModuleKey);
                    if (mRowIndex >= 0)
                    {
                        mModuleText = mDvModules[mRowIndex]["moduletext"].ToString().Trim();
                    }

                    if (mModuleItemText != "")
                    {
                        mDvAccessFunctions.RowFilter = "functionaccesskey like '" + mFunctionsPrefix + "_%'";
                        if (mDvAccessFunctions.Count == 0)
                        {
                            DataRow mNewRow = pDtUserModules.NewRow();
                            mNewRow["usermoduletext"] = mModuleText;
                            mNewRow["usermoduleitemtext"] = mModuleItemText;
                            mNewRow["userfunctionaccesstext"] = "NA";
                            pDtUserModules.Rows.Add(mNewRow);
                            pDtUserModules.AcceptChanges();
                        }
                        else
                        {
                            foreach (DataRowView mAccessKeyRow in mDvAccessFunctions)
                            {
                                mRowIndex = mDvUserFunctionAccessKeys.Find(mAccessKeyRow["functionaccesskey"].ToString().Trim());
                                if (mRowIndex >= 0)
                                {
                                    DataRow mNewRow = pDtUserModules.NewRow();
                                    mNewRow["usermoduletext"] = mModuleText;
                                    mNewRow["usermoduleitemtext"] = mModuleItemText;
                                    mNewRow["userfunctionaccesstext"] = mAccessKeyRow["functionaccesstext"].ToString().Trim();
                                    pDtUserModules.Rows.Add(mNewRow);
                                    pDtUserModules.AcceptChanges();
                                }
                            }
                        }
                    }
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

                //fill usermoduleitems
                pDtUserModuleItems = pMdtUserGroups.Get_UserModuleItems(
                    "usergroupcode='" + pUserGroupCode.Trim() + "'", "");

                //fill userfunctionaccesskeys
                pDtUserFunctionAccessKeys = pMdtUserGroups.Get_UserFunctionAccessKeys(
                    "usergroupcode='" + pUserGroupCode.Trim() + "'", "");

                this.Fill_Modules();
                this.Fill_UserModules();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region viewModules_FocusedRowChanged
        private void viewModules_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            string mFunctionName = "viewModules_FocusedRowChanged";

            try
            {
                if (e.FocusedRowHandle < 0)
                {
                    return;
                }

                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)grdModules.MainView;

                DataRow mSelectedRow = mGridView.GetDataRow(e.FocusedRowHandle);
                pModuleKey = mSelectedRow["modulekey"].ToString().Trim();
                txbModules.Text = mSelectedRow["moduletext"].ToString().Trim();

                this.Fill_ModuleItems();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region viewFunctions_FocusedRowChanged
        private void viewFunctions_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            string mFunctionName = "viewFunctions_FocusedRowChanged";

            try
            {
                if (e.FocusedRowHandle < 0)
                {
                    return;
                }

                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)grdFunctions.MainView;

                DataRow mSelectedRow = mGridView.GetDataRow(e.FocusedRowHandle);
                pModuleItemKey = mSelectedRow["moduleitemkey"].ToString().Trim();
                pFunctionsPrefix = mSelectedRow["functionsprefix"].ToString().Trim();

                this.Fill_AccessFunctions();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region chkmoduleselect_CheckedChanged
        void chkmoduleselect_CheckedChanged(object sender, EventArgs e)
        {
            if (pCheckedFromModule == true)
            {
                grdModules.FocusedView.PostEditor();

                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)grdModules.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                DataView mDvModuleItems = new DataView();
                mDvModuleItems.Table = Program.gDtModuleItems;
                mDvModuleItems.RowFilter = "modulekey='" + mSelectedRow["modulekey"].ToString().Trim() + "'";

                DataView mDvUserModuleItems = new DataView();
                mDvUserModuleItems.Table = pDtUserModuleItems;
                mDvUserModuleItems.Sort = "moduleitemkey";

                DataView mDvUserAccessFunctions = new DataView();
                mDvUserAccessFunctions.Table = pDtUserFunctionAccessKeys;
                mDvUserAccessFunctions.Sort = "functionaccesskey";

                foreach (DataRowView mModuleItemRow in mDvModuleItems)
                {
                    //delete
                    int mRowIndex = mDvUserModuleItems.Find(mModuleItemRow["moduleitemkey"].ToString().Trim());
                    if (mRowIndex >= 0)
                    {
                        mDvUserModuleItems[mRowIndex].Delete();
                        pDtUserModuleItems.AcceptChanges();

                        DataTable mDtActions = (DataTable)grdActions.DataSource;
                        foreach (DataRow mDataRow in mDtActions.Rows)
                        {
                            mRowIndex = mDvUserAccessFunctions.Find(mDataRow["functionaccesskey"].ToString().Trim());
                            if (mRowIndex >= 0)
                            {
                                mDvUserAccessFunctions[mRowIndex].Delete();
                                pDtUserFunctionAccessKeys.AcceptChanges();
                            }
                        }
                    }

                    if (Convert.ToBoolean(mSelectedRow["moduleselected"]) == true)
                    {
                        //add
                        DataRowView mDataRowView = mDvUserModuleItems.AddNew();
                        mDataRowView["usergroupcode"] = pUserGroupCode;
                        mDataRowView["moduleitemkey"] = mModuleItemRow["moduleitemkey"].ToString().Trim();
                        mDataRowView.EndEdit();
                        pDtUserModuleItems.AcceptChanges();

                        DataView pDvAllAccessFunctions = new DataView();
                        pDvAllAccessFunctions.Table = Program.gDtAccessFunctions;
                        pDvAllAccessFunctions.RowFilter = "functionaccesskey like '" + mModuleItemRow["functionsprefix"].ToString().Trim() + "_%'";

                        foreach (DataRowView mCurrDataRowView in pDvAllAccessFunctions)
                        {
                            DataRowView mNewRowView = mDvUserAccessFunctions.AddNew();
                            mNewRowView["usergroupcode"] = pUserGroupCode;
                            mNewRowView["functionaccesskey"] = mCurrDataRowView["functionaccesskey"].ToString().Trim();
                            mNewRowView.EndEdit();
                            pDtUserFunctionAccessKeys.AcceptChanges();
                        }
                    }
                }

                this.Fill_ModuleItems();
                this.Fill_AccessFunctions();
                this.Fill_UserModules();

                this.Data_Save();
            }
        }
        #endregion

        #region chkmoduleitemselected_CheckedChanged
        void chkmoduleitemselected_CheckedChanged(object sender, EventArgs e)
        {
            if (pCheckedFromModuleItem == true)
            {
                grdFunctions.FocusedView.PostEditor();

                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)grdFunctions.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                DataView mDvUserModuleItems = new DataView();
                mDvUserModuleItems.Table = pDtUserModuleItems;
                mDvUserModuleItems.Sort = "moduleitemkey";

                DataView mDvUserAccessFunctions = new DataView();
                mDvUserAccessFunctions.Table = pDtUserFunctionAccessKeys;
                mDvUserAccessFunctions.Sort = "functionaccesskey";

                //delete
                int mRowIndex = mDvUserModuleItems.Find(mSelectedRow["moduleitemkey"].ToString().Trim());
                if (mRowIndex >= 0)
                {
                    mDvUserModuleItems[mRowIndex].Delete();
                    pDtUserModuleItems.AcceptChanges();

                    DataTable mDtActions = (DataTable)grdActions.DataSource;
                    foreach (DataRow mDataRow in mDtActions.Rows)
                    {
                        mRowIndex = mDvUserAccessFunctions.Find(mDataRow["functionaccesskey"].ToString().Trim());
                        if (mRowIndex >= 0)
                        {
                            mDvUserAccessFunctions[mRowIndex].Delete();
                            pDtUserFunctionAccessKeys.AcceptChanges();
                        }
                    }
                }

                if (Convert.ToBoolean(mSelectedRow["moduleitemselected"]) == true)
                {
                    //add
                    DataRowView mDataRowView = mDvUserModuleItems.AddNew();
                    mDataRowView["usergroupcode"] = pUserGroupCode;
                    mDataRowView["moduleitemkey"] = mSelectedRow["moduleitemkey"].ToString().Trim();
                    mDataRowView.EndEdit();
                    pDtUserModuleItems.AcceptChanges();

                    DataView pDvAllAccessFunctions = new DataView();
                    pDvAllAccessFunctions.Table = Program.gDtAccessFunctions;
                    pDvAllAccessFunctions.RowFilter = "functionaccesskey like '" + pFunctionsPrefix + "_%'";

                    foreach (DataRowView mCurrDataRowView in pDvAllAccessFunctions)
                    {
                        DataRowView mNewRowView = mDvUserAccessFunctions.AddNew();
                        mNewRowView["usergroupcode"] = pUserGroupCode;
                        mNewRowView["functionaccesskey"] = mCurrDataRowView["functionaccesskey"].ToString().Trim();
                        mNewRowView.EndEdit();
                        pDtUserFunctionAccessKeys.AcceptChanges();
                    }
                }

                this.Fill_AccessFunctions();
                this.Fill_UserModules();

                this.Data_Save();
            }
        }
        #endregion

        #region chkfunctionaccessselected_CheckedChanged
        void chkfunctionaccessselected_CheckedChanged(object sender, EventArgs e)
        {
            if (pCheckedFromActions == true)
            {
                grdActions.FocusedView.PostEditor();

                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                                    (DevExpress.XtraGrid.Views.Grid.GridView)grdActions.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                DataView mDvUserAccessFunctions = new DataView();
                mDvUserAccessFunctions.Table = pDtUserFunctionAccessKeys;
                mDvUserAccessFunctions.Sort = "functionaccesskey";

                //delete
                int mRowIndex = mDvUserAccessFunctions.Find(mSelectedRow["functionaccesskey"].ToString().Trim());
                if (mRowIndex >= 0)
                {
                    mDvUserAccessFunctions[mRowIndex].Delete();
                    pDtUserFunctionAccessKeys.AcceptChanges();
                }

                if (Convert.ToBoolean(mSelectedRow["functionaccessselected"]) == true)
                {
                    //add
                    DataRowView mDataRowView = mDvUserAccessFunctions.AddNew();
                    mDataRowView["usergroupcode"] = pUserGroupCode;
                    mDataRowView["functionaccesskey"] = mSelectedRow["functionaccesskey"].ToString().Trim();
                    mDataRowView.EndEdit();
                    pDtUserFunctionAccessKeys.AcceptChanges();
                }

                this.Fill_UserModules();

                this.Data_Save();
            }
        }
        #endregion

        #region grdModules_Enter
        private void grdModules_Enter(object sender, EventArgs e)
        {
            pCheckedFromActions = false;
            pCheckedFromModuleItem = false;
            pCheckedFromModule = true;
        }
        #endregion

        #region grdFunctions_Enter
        private void grdFunctions_Enter(object sender, EventArgs e)
        {
            pCheckedFromModule = false;
            pCheckedFromActions = false;
            pCheckedFromModuleItem = true;
        }
        #endregion

        #region grdActions_Enter
        private void grdActions_Enter(object sender, EventArgs e)
        {
            pCheckedFromModule = false;
            pCheckedFromModuleItem = false;
            pCheckedFromActions = true;
        }
        #endregion

        #region Data_Save
        private void Data_Save()
        {
            String mFunctionName = "Data_Save";

            try
            {
                pResult = pMdtUserGroups.Save_UserModuleItems(pUserGroupCode, pDtUserModuleItems,
                    pDtUserFunctionAccessKeys, Program.gCurrentUser.Code);
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

        #region frmSECGroupsAccess_FormClosing
        private void frmSECGroupsAccess_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Data_Save();
        }
        #endregion
    }
}