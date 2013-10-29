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
    public partial class frmGENSearchEngine : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsSearchEngine pMdtSearchEngine;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataTable pDtSearchObjects = new DataTable("searchobjects");

        private DataTable pDtAvailable = new DataTable("available");
        private DataTable pDtSelected = new DataTable("selected");

        private string pTableName = "";

        #endregion

        #region frmGENSearchEngine
        public frmGENSearchEngine()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmGENSearchEngine";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                
                this.CancelButton = cmdClose;

                pMdtSearchEngine = (AfyaPro_MT.clsSearchEngine)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSearchEngine),
                    Program.gMiddleTier + "clsSearchEngine");

                pDtSearchObjects.Columns.Add("code", typeof(System.String));
                pDtSearchObjects.Columns.Add("description", typeof(System.String));
                cboSearchObject.Properties.DataSource = pDtSearchObjects;
                cboSearchObject.Properties.DisplayMember = "description";
                cboSearchObject.Properties.ValueMember = "code";

                pDtAvailable.Columns.Add("tablename", typeof(System.String));
                pDtAvailable.Columns.Add("fieldname", typeof(System.String));
                pDtAvailable.Columns.Add("fielddisplayname", typeof(System.String));
                pDtAvailable.Columns.Add("defaultfield", typeof(System.Int16));

                pDtSelected.Columns.Add("tablename", typeof(System.String));
                pDtSelected.Columns.Add("fieldname", typeof(System.String));
                pDtSelected.Columns.Add("fielddisplayname", typeof(System.String));
                pDtSelected.Columns.Add("defaultfield", typeof(System.Int16));

                this.Fill_LookupData();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmGENSearchEngine_Load
        private void frmGENSearchEngine_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmGENSearchEngine_Load";

            try
            {
                this.Top = 0;
                this.Grid_Settings();
                this.Load_Controls();

                DataTable mDtSearchFields = pMdtSearchEngine.View_SearchFields("**********", Program.gLanguageName, "grdGENSearchEngine");
                foreach (DataColumn mDataColumn in pDtAvailable.Columns)
                {
                    mDataColumn.Caption = mDtSearchFields.Columns[mDataColumn.ColumnName].Caption;
                }

                foreach (DataColumn mDataColumn in pDtSelected.Columns)
                {
                    mDataColumn.Caption = mDtSearchFields.Columns[mDataColumn.ColumnName].Caption;
                }

                grdGENSearchEngineAvailableFields.DataSource = pDtAvailable;
                Program.Restore_GridLayout(grdGENSearchEngineAvailableFields, grdGENSearchEngineAvailableFields.Name);

                grdGENSearchEngineSelectedFields.DataSource = pDtSelected;
                Program.Restore_GridLayout(grdGENSearchEngineSelectedFields, grdGENSearchEngineSelectedFields.Name);
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmGENSearchEngine_FormClosing
        private void frmGENSearchEngine_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.Save_GridLayout(grdGENSearchEngineAvailableFields, grdGENSearchEngineAvailableFields.Name);
            Program.Save_GridLayout(grdGENSearchEngineSelectedFields, grdGENSearchEngineSelectedFields.Name);
        }
        #endregion

        #region cboSearchObject_EditValueChanged
        private void cboSearchObject_EditValueChanged(object sender, EventArgs e)
        {
            string mSearchObjectCode = cboSearchObject.GetColumnValue("code").ToString().Trim();

            switch (mSearchObjectCode.Trim().ToLower())
            {
                case "searchobject0": pTableName = "patients"; break;
                case "searchobject1": pTableName = "som_products"; break;
                case "searchobject2": pTableName = "facilitycorporatemembers"; break;
                case "searchobject3": pTableName = "facilitycorporates"; break;
                case "searchobject4": pTableName = "facilitybillingitems"; break;
                case "searchobject5": pTableName = "billdebtors"; break;
                case "searchobject6": pTableName = "dxtdiagnoses"; break;
                case "searchobject7": pTableName = "som_suppliers"; break;
                case "searchobject8": pTableName = "som_stores"; break;
                case "searchobject10": pTableName = "rch_patients"; break;
                case "searchobject11": pTableName = "dxtindicators"; break;
                case "searchobject12": pTableName = "ctc_patients"; break;
                default: return;
            }

            this.Fill_Available();
            this.Fill_Selected();
        }
        #endregion

        #region mGridView2_FocusedRowChanged
        void mGridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            string mFunctionName = "mGridView2_FocusedRowChanged";

            try
            {
                if (e.FocusedRowHandle < 0)
                {
                    return;
                }

                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                (DevExpress.XtraGrid.Views.Grid.GridView)grdGENSearchEngineSelectedFields.MainView;

                DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                txtFieldName.Text = mSelectedRow["fieldname"].ToString().Trim();
                txtDisplayName.Text = mSelectedRow["fielddisplayname"].ToString().Trim();
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

            mObjectsList.Add(txbSearchObject);
            mObjectsList.Add(grpAvailableFields);
            mObjectsList.Add(grpSelectedFields);
            mObjectsList.Add(txbFieldName);
            mObjectsList.Add(txbDisplayName);
            mObjectsList.Add(cmdUpdate);
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
                grdGENSearchEngineAvailableFields.MainView = mGridView1;

                //mGridView2
                DevExpress.XtraGrid.Views.Grid.GridView mGridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
                mGridView2.OptionsBehavior.Editable = false;
                mGridView2.OptionsView.ShowGroupPanel = false;
                mGridView2.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(mGridView2_FocusedRowChanged);
                grdGENSearchEngineSelectedFields.MainView = mGridView2;
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
                #region Categories

                pDtSearchObjects.Rows.Clear();
                DataTable mDtSearchObjects = pMdtSearchEngine.Get_SearchObjects(Program.gLanguageName, "grdGENSearchEngine");
                foreach (DataRow mDataRow in mDtSearchObjects.Rows)
                {
                    mNewRow = pDtSearchObjects.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtSearchObjects.Rows.Add(mNewRow);
                    pDtSearchObjects.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtSearchObjects.Columns)
                {
                    mDataColumn.Caption = mDtSearchObjects.Columns[mDataColumn.ColumnName].Caption;
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

        #region Fill_Available
        internal void Fill_Available()
        {
            string mFunctionName = "Fill_Available";

            if (cboSearchObject.ItemIndex == -1)
            {
                return;
            }

            try
            {
                pDtAvailable.Rows.Clear();

                string mGridName = "grdGENSearchEngine";
 
                //load data
                DataTable mDtAllFields = pMdtSearchEngine.View_TableFields(pTableName);

                DataTable mDtUsed = pMdtSearchEngine.View_SearchFields(pTableName, Program.gLanguageName, mGridName);
                DataView mDvUsed = new DataView();
                mDvUsed.Table = mDtUsed;
                mDvUsed.Sort = "fieldname";

                foreach (DataColumn mDataColumn in mDtAllFields.Columns)
                {
                    if (mDvUsed.Find(mDataColumn.ColumnName.Trim()) < 0)
                    {
                        DataRow mNewRow = pDtAvailable.NewRow();
                        mNewRow["tablename"] = pTableName;
                        mNewRow["fieldname"] = mDataColumn.ColumnName.Trim();
                        mNewRow["fielddisplayname"] = mDataColumn.ColumnName.Trim();
                        mNewRow["defaultfield"] = 0;
                        pDtAvailable.Rows.Add(mNewRow);
                        pDtAvailable.AcceptChanges();
                    }
                }

                foreach (DataColumn mDataColumn in pDtAvailable.Columns)
                {
                    mDataColumn.Caption = mDtUsed.Columns[mDataColumn.ColumnName].Caption;
                }

                grdGENSearchEngineAvailableFields.DataSource = pDtAvailable;

                //restore current document layout
                Program.Restore_GridLayout(grdGENSearchEngineAvailableFields, grdGENSearchEngineAvailableFields.Name);

                //expand all groups
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)grdGENSearchEngineAvailableFields.MainView;
                mGridView.ExpandAllGroups();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_Selected
        private void Fill_Selected()
        {
            String mFunctionName = "Fill_Selected";

            if (cboSearchObject.ItemIndex == -1)
            {
                return;
            }

            try
            {
                pDtSelected.Rows.Clear();

                string mGridName = "grdGENSearchEngine";

                DataTable mDtUsed = pMdtSearchEngine.View_SearchFields(pTableName, Program.gLanguageName, mGridName);

                foreach (DataRow mDataRow in mDtUsed.Rows)
                {
                    DataRow mNewRow = pDtSelected.NewRow();
                    mNewRow["tablename"] = pTableName;
                    mNewRow["fieldname"] = mDataRow["fieldname"].ToString();
                    mNewRow["fielddisplayname"] = mDataRow["fielddisplayname"].ToString();
                    mNewRow["defaultfield"] = Convert.ToInt16(mDataRow["defaultfield"]);
                    pDtSelected.Rows.Add(mNewRow);
                    pDtSelected.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtSelected.Columns)
                {
                    mDataColumn.Caption = mDtUsed.Columns[mDataColumn.ColumnName].Caption;
                }

                grdGENSearchEngineSelectedFields.DataSource = pDtSelected;

                //restore current document layout
                Program.Restore_GridLayout(grdGENSearchEngineSelectedFields, grdGENSearchEngineSelectedFields.Name);

                //expand all groups
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)grdGENSearchEngineAvailableFields.MainView;
                mGridView.ExpandAllGroups();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdAdd_Click
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdAdd_Click";

            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                (DevExpress.XtraGrid.Views.Grid.GridView)grdGENSearchEngineAvailableFields.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                DataView mDvSelected = new DataView();
                mDvSelected.Table = pDtSelected;
                mDvSelected.Sort = "fieldname";

                int mRowIndex = mDvSelected.Find(mSelectedRow["fieldname"].ToString().Trim());

                if (mRowIndex < 0)
                {
                    DataRow mNewRow = pDtSelected.NewRow();
                    mNewRow["tablename"] = mSelectedRow["tablename"];
                    mNewRow["fieldname"] = mSelectedRow["fieldname"];
                    mNewRow["fielddisplayname"] = mSelectedRow["fielddisplayname"];
                    mNewRow["defaultfield"] = mSelectedRow["defaultfield"];
                    pDtSelected.Rows.Add(mNewRow);
                    pDtSelected.AcceptChanges();
                }

                mSelectedRow.Delete();
                pDtAvailable.AcceptChanges();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdRemove_Click
        private void cmdRemove_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdRemove_Click";

            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                (DevExpress.XtraGrid.Views.Grid.GridView)grdGENSearchEngineSelectedFields.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                DataView mDvAvailable = new DataView();
                mDvAvailable.Table = pDtAvailable;
                mDvAvailable.Sort = "fieldname";

                int mRowIndex = mDvAvailable.Find(mSelectedRow["fieldname"].ToString().Trim());

                if (mRowIndex < 0)
                {
                    DataRow mNewRow = pDtAvailable.NewRow();
                    mNewRow["tablename"] = mSelectedRow["tablename"];
                    mNewRow["fieldname"] = mSelectedRow["fieldname"];
                    mNewRow["fielddisplayname"] = mSelectedRow["fielddisplayname"];
                    mNewRow["defaultfield"] = mSelectedRow["defaultfield"];
                    pDtAvailable.Rows.Add(mNewRow);
                    pDtAvailable.AcceptChanges();
                }

                mSelectedRow.Delete();
                pDtSelected.AcceptChanges();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdUp_Click
        private void cmdUp_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdUp_Click";

            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                (DevExpress.XtraGrid.Views.Grid.GridView)grdGENSearchEngineSelectedFields.MainView;

                if (mGridView.FocusedRowHandle < 1)
                {
                    return;
                }

                DataRow mTopRow = mGridView.GetDataRow(mGridView.FocusedRowHandle - 1);
                DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                string mTopTableName = mTopRow["tablename"].ToString();
                string mTopFieldName = mTopRow["fieldname"].ToString();
                string mTopFieldDisplayName = mTopRow["fielddisplayname"].ToString();
                string mTopDefaultField = mTopRow["defaultfield"].ToString();

                string mCurrTableName = mSelectedRow["tablename"].ToString();
                string mCurrFieldName = mSelectedRow["fieldname"].ToString();
                string mCurrFieldDisplayName = mSelectedRow["fielddisplayname"].ToString();
                string mCurrDefaultField = mSelectedRow["defaultfield"].ToString();

                pDtSelected.BeginInit();

                pDtSelected.Rows[mGridView.FocusedRowHandle - 1]["tablename"] = mCurrTableName;
                pDtSelected.Rows[mGridView.FocusedRowHandle - 1]["fieldname"] = mCurrFieldName;
                pDtSelected.Rows[mGridView.FocusedRowHandle - 1]["fielddisplayname"] = mCurrFieldDisplayName;
                pDtSelected.Rows[mGridView.FocusedRowHandle - 1]["defaultfield"] = mCurrDefaultField;

                pDtSelected.Rows[mGridView.FocusedRowHandle]["tablename"] = mTopTableName;
                pDtSelected.Rows[mGridView.FocusedRowHandle]["fieldname"] = mTopFieldName;
                pDtSelected.Rows[mGridView.FocusedRowHandle]["fielddisplayname"] = mTopFieldDisplayName;
                pDtSelected.Rows[mGridView.FocusedRowHandle]["defaultfield"] = mTopDefaultField;

                pDtSelected.EndInit();
                pDtSelected.AcceptChanges();

                mGridView.FocusedRowHandle = mGridView.FocusedRowHandle - 1;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdDown_Click
        private void cmdDown_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdDown_Click";

            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                (DevExpress.XtraGrid.Views.Grid.GridView)grdGENSearchEngineSelectedFields.MainView;

                if (mGridView.FocusedRowHandle >= pDtSelected.Rows.Count - 1)
                {
                    return;
                }

                DataRow mBottomRow = mGridView.GetDataRow(mGridView.FocusedRowHandle + 1);
                DataRow mSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                string mBottopTableName = mBottomRow["tablename"].ToString();
                string mBottomFieldName = mBottomRow["fieldname"].ToString();
                string mBottomFieldDisplayName = mBottomRow["fielddisplayname"].ToString();
                string mBottomDefaultField = mBottomRow["defaultfield"].ToString();

                string mCurrTableName = mSelectedRow["tablename"].ToString();
                string mCurrFieldName = mSelectedRow["fieldname"].ToString();
                string mCurrFieldDisplayName = mSelectedRow["fielddisplayname"].ToString();
                string mCurrDefaultField = mSelectedRow["defaultfield"].ToString();

                pDtSelected.BeginInit();

                pDtSelected.Rows[mGridView.FocusedRowHandle + 1]["tablename"] = mCurrTableName;
                pDtSelected.Rows[mGridView.FocusedRowHandle + 1]["fieldname"] = mCurrFieldName;
                pDtSelected.Rows[mGridView.FocusedRowHandle + 1]["fielddisplayname"] = mCurrFieldDisplayName;
                pDtSelected.Rows[mGridView.FocusedRowHandle + 1]["defaultfield"] = mCurrDefaultField;

                pDtSelected.Rows[mGridView.FocusedRowHandle]["tablename"] = mBottopTableName;
                pDtSelected.Rows[mGridView.FocusedRowHandle]["fieldname"] = mBottomFieldName;
                pDtSelected.Rows[mGridView.FocusedRowHandle]["fielddisplayname"] = mBottomFieldDisplayName;
                pDtSelected.Rows[mGridView.FocusedRowHandle]["defaultfield"] = mBottomDefaultField;

                pDtSelected.EndInit();
                pDtSelected.AcceptChanges();

                mGridView.FocusedRowHandle = mGridView.FocusedRowHandle + 1;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdUpdate_Click
        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            this.Update_Field();
        }
        #endregion

        #region Update_Field
        private void Update_Field()
        {
            string mFunctionName = "Update_Field";

            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                (DevExpress.XtraGrid.Views.Grid.GridView)grdGENSearchEngineAvailableFields.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                DataView mDvSelected = new DataView();
                mDvSelected.Table = pDtSelected;
                mDvSelected.Sort = "fieldname";

                int mRowIndex = mDvSelected.Find(txtFieldName.Text.Trim());

                if (mRowIndex >= 0)
                {
                    mDvSelected.BeginInit();

                    mDvSelected[mRowIndex]["fielddisplayname"] = txtDisplayName.Text.Trim();

                    mDvSelected.EndInit();
                }
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
            if (cboSearchObject.ItemIndex == -1)
            {
                Program.Display_Error("Invalid search object");
                cboSearchObject.Focus();
                return;
            }
            #endregion

            try
            {
                if (pDtSelected.Rows.Count == 0)
                {
                    return;
                }

                //save search fields 
                pResult = pMdtSearchEngine.Save_Fields(pTableName, pDtSelected, Program.gCurrentUser.Code);
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

                Program.Display_Info("Settings saved successfully");
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