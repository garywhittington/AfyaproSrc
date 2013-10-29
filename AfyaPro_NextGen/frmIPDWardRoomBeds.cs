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
    public partial class frmIPDWardRoomBeds : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsWards pMdtWards;
        private AfyaPro_MT.clsWardRooms pMdtWardRooms;
        private AfyaPro_MT.clsWardRoomBeds pMdtWardRoomBeds;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataTable pDtWards = new DataTable("wards");
        private DataTable pDtRooms = new DataTable("rooms");
        private DataRow pSelectedRow = null;
        internal string gDataState = "";
        private bool pFirstTimeLoad = true;

        private ComboBoxItemCollection pBedStatus;

        #endregion

        #region frmIPDWardRoomBeds
        public frmIPDWardRoomBeds()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmIPDWardRoomBeds";

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                this.CancelButton = cmdClose;

                pMdtWards = (AfyaPro_MT.clsWards)Activator.GetObject(
                    typeof(AfyaPro_MT.clsWards),
                    Program.gMiddleTier + "clsWards");

                pMdtWardRooms = (AfyaPro_MT.clsWardRooms)Activator.GetObject(
                    typeof(AfyaPro_MT.clsWardRooms),
                    Program.gMiddleTier + "clsWardRooms");

                pMdtWardRoomBeds = (AfyaPro_MT.clsWardRoomBeds)Activator.GetObject(
                    typeof(AfyaPro_MT.clsWardRoomBeds),
                    Program.gMiddleTier + "clsWardRoomBeds");

                pMdtAutoCodes = (AfyaPro_MT.clsAutoCodes)Activator.GetObject(
                    typeof(AfyaPro_MT.clsAutoCodes),
                    Program.gMiddleTier + "clsAutoCodes");

                pDtWards.Columns.Add("code", typeof(System.String));
                pDtWards.Columns.Add("description", typeof(System.String));
                cboWard.Properties.DataSource = pDtWards;
                cboWard.Properties.DisplayMember = "description";
                cboWard.Properties.ValueMember = "code";

                pDtRooms.Columns.Add("code", typeof(System.String));
                pDtRooms.Columns.Add("description", typeof(System.String));
                cboRoom.Properties.DataSource = pDtRooms;
                cboRoom.Properties.DisplayMember = "description";
                cboRoom.Properties.ValueMember = "code";

                pBedStatus = cboBedStatus.Properties.Items;

                this.Fill_LookupData();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmIPDWardRoomBeds_Load
        private void frmIPDWardRoomBeds_Load(object sender, EventArgs e)
        {
            switch (gDataState.Trim().ToLower())
            {
                case "new": Mode_New(); break;
                case "edit": Mode_Edit(); break;
            }

            this.Load_Controls();
        }
        #endregion

        #region frmIPDWardRoomBeds_Activated
        private void frmIPDWardRoomBeds_Activated(object sender, EventArgs e)
        {
            if (pFirstTimeLoad == true)
            {
                if (gDataState.Trim().ToLower() == "new")
                {
                    if (txtCode.Text.Trim().ToLower() == "<<---new--->>")
                    {
                        txtDescription.Focus();
                    }
                    else
                    {
                        txtCode.Focus();
                    }
                }
                else
                {
                    txtDescription.Focus();
                }
                pFirstTimeLoad = false;
            }
        }
        #endregion

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(txbWard);
            mObjectsList.Add(txbRoom);
            mObjectsList.Add(txbCode);
            mObjectsList.Add(txbDescription);
            mObjectsList.Add(cmdOk);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region Grid_Settings
        internal void Grid_Settings(GridControl mGridControl)
        {
            string mFunctionName = "Grid_Settings";

            try
            {
                if (mGridControl.Visible == false)
                {
                    mGridControl.Visible = true;
                }

                mGridControl.DataSource = null;

                //prepare grid view
                DevExpress.XtraGrid.Views.Grid.GridView mGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
                mGridView.OptionsBehavior.Editable = false;
                mGridView.OptionsView.ShowGroupPanel = true;
                mGridControl.MainView = mGridView;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Mode_New
        private void Mode_New()
        {
            String mFunctionName = "Mode_New";

            try
            {
                Int16 mGenerateCode = pMdtAutoCodes.Auto_Generate_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.wardroombedcode));
                if (mGenerateCode == -1)
                {
                    Program.Display_Server_Error("");
                    return;
                }

                txtCode.Text = "";
                this.Data_Clear();

                #region display owning groups if any
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain.MainView;

                if (mGridView.FocusedRowHandle >= 0)
                {
                    pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);
                    cboWard.ItemIndex = Program.Get_LookupItemIndex(cboWard, "code", pSelectedRow["wardcode"].ToString());
                    cboRoom.ItemIndex = Program.Get_LookupItemIndex(cboRoom, "code", pSelectedRow["roomcode"].ToString());
                }
                #endregion

                if (mGenerateCode == 1)
                {
                    txtCode.Text = "<<---New--->>";
                    txtCode.Enabled = false;
                    txtDescription.Focus();
                }
                else
                {
                    txtCode.Enabled = true;
                    txtCode.Focus();
                }

                gDataState = "New";
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
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

                cboWard.EditValue = null;
                cboRoom.EditValue = null;
                this.Data_Clear();
                pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                cboWard.ItemIndex = Program.Get_LookupItemIndex(cboWard, "code", pSelectedRow["wardcode"].ToString());
                cboRoom.ItemIndex = Program.Get_LookupItemIndex(cboRoom, "code", pSelectedRow["roomcode"].ToString());
                txtCode.Text = pSelectedRow["code"].ToString();
                txtDescription.Text = pSelectedRow["description"].ToString();
                cboBedStatus.Text = pSelectedRow["bedstatus"].ToString();

                gDataState = "Edit";
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Data_Clear
        private void Data_Clear()
        {
            txtDescription.Text = "";
        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            DataRow mNewRow;
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region wards

                pDtWards.Rows.Clear();
                DataTable mDtWards = pMdtWards.View("", "code", Program.gLanguageName, "grdIPDWards");
                foreach (DataRow mDataRow in mDtWards.Rows)
                {
                    mNewRow = pDtWards.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtWards.Rows.Add(mNewRow);
                    pDtWards.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtWards.Columns)
                {
                    mDataColumn.Caption = mDtWards.Columns[mDataColumn.ColumnName].Caption;
                }

                #endregion

                #region bed status

                pBedStatus.Clear();

                Type mBedStatus = typeof(AfyaPro_Types.clsEnums.IPDBedStatus);
                foreach (string mStatus in Enum.GetNames(mBedStatus))
                {
                    pBedStatus.Add(mStatus);
                }

                cboBedStatus.SelectedIndex = 0;

                #endregion
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cboWard_EditValueChanged
        void cboWard_EditValueChanged(object sender, EventArgs e)
        {
            string mFunctionName = "cboWard_EditValueChanged";

            try
            {
                pDtRooms.Rows.Clear();
                if (cboWard.ItemIndex == -1)
                {
                    return;
                }

                string mCountryCode = cboWard.GetColumnValue("code").ToString().Trim();

                DataTable mDtRooms = pMdtWardRooms.View(
                    "wardcode='" + mCountryCode + "'", "code", Program.gLanguageName, "grdIPDWardRooms");
                foreach (DataRow mDataRow in mDtRooms.Rows)
                {
                    DataRow mNewRow = pDtRooms.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtRooms.Rows.Add(mNewRow);
                    pDtRooms.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtRooms.Columns)
                {
                    mDataColumn.Caption = mDtRooms.Columns[mDataColumn.ColumnName].Caption;
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
                DataTable mDtBeds = pMdtWardRoomBeds.View("", "", Program.gLanguageName, mGridControl.Name);
                mGridControl.DataSource = mDtBeds;

                //expand all groups
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)mGridControl.MainView;
                mGridView.ExpandAllGroups();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Data_New
        private void Data_New()
        {
            Int16 mGenerateCode = 0;
            String mFunctionName = "Data_New";

            #region validation
            if (cboWard.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_WardDescriptionIsInvalid.ToString());
                cboWard.Focus();
                return;
            }

            if (cboRoom.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_WardRoomDescriptionIsInvalid.ToString());
                cboRoom.Focus();
                return;
            }

            if (txtCode.Text.Trim() == "" && txtCode.Text.Trim().ToLower() != "<<---new--->>")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_WardRoomBedCodeIsInvalid.ToString());
                txtCode.Focus();
                return;
            }

            if (txtDescription.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_WardRoomBedDescriptionIsInvalid.ToString());
                txtDescription.Focus();
                return;
            }
            #endregion

            try
            {
                if (txtCode.Text.Trim().ToLower() == "<<---new--->>")
                {
                    mGenerateCode = 1;
                }

                //add
                pResult = pMdtWardRoomBeds.Add(mGenerateCode, txtCode.Text, txtDescription.Text,
                    cboWard.GetColumnValue("code").ToString(), cboRoom.GetColumnValue("code").ToString(),
                    cboBedStatus.Text, Program.gCurrentUser.Code);
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

                //refresh
                this.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                this.Mode_New();
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
            if (cboWard.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_WardDescriptionIsInvalid.ToString());
                cboWard.Focus();
                return;
            }

            if (cboRoom.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_WardRoomDescriptionIsInvalid.ToString());
                cboRoom.Focus();
                return;
            }

            if (txtCode.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_WardRoomBedCodeIsInvalid.ToString());
                txtCode.Focus();
                return;
            }

            if (txtDescription.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_WardRoomBedDescriptionIsInvalid.ToString());
                txtDescription.Focus();
                return;
            }
            #endregion

            try
            {
                //add 
                pResult = pMdtWardRoomBeds.Edit(txtCode.Text, txtDescription.Text, cboBedStatus.Text, Program.gCurrentUser.Code);
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

                //refresh
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

        #region Data_Delete
        internal void Data_Delete()
        {
            String mFunctionName = "Data_Delete";

            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                DialogResult mResp = Program.Confirm_Deletion(
                    pSelectedRow["code"].ToString().Trim() + "'   '"
                    + pSelectedRow["description"].ToString().Trim());

                if (mResp != DialogResult.Yes)
                {
                    return;
                }

                //add 
                pResult = pMdtWardRoomBeds.Delete(pSelectedRow["code"].ToString().Trim(), Program.gCurrentUser.Code);
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

                //refresh
                this.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
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
            switch (gDataState.Trim().ToLower())
            {
                case "new": this.Data_New(); break;
                case "edit": this.Data_Edit(); break;
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