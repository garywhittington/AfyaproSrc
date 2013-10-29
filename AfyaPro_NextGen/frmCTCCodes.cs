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
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraGrid;

namespace AfyaPro_NextGen
{
    public partial class frmCTCCodes : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsCTCCodes pMdtCTCCodes;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataRow pSelectedRow = null;
        internal string gDataState = "";

        private int pCTCCode = 0;

        #endregion

        #region frmCTCCodes
        public frmCTCCodes(int mCTCCode)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCCodes";

            try
            {
                this.pCTCCode = mCTCCode;
                this.Icon = Program.gMdiForm.Icon;
                this.CancelButton = cmdClose;

                pMdtCTCCodes = (AfyaPro_MT.clsCTCCodes)Activator.GetObject(
                    typeof(AfyaPro_MT.clsCTCCodes),
                    Program.gMiddleTier + "clsCTCCodes");

                if (pCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.MaritalStatus)
                {
                    this.Name = "frmCTCMaritalStatus";
                    this.Text = "Marital Status";
                }
                if (pCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.FunctionalStatus)
                {
                    this.Name = "frmCTCFunctionalStatus";
                    this.Text = "Functional Status";
                }
                if (pCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.TBStatus)
                {
                    this.Name = "frmCTCTBStatus";
                    this.Text = "TB Status";
                }
                if (pCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVStatus)
                {
                    this.Name = "frmCTCARVStatus";
                    this.Text = "ARV Status";
                }
                if (pCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.AIDSIllness)
                {
                    this.Name = "frmCTCAIDSIllness";
                    this.Text = "AIDS Defining Illness, New symptoms, Side effects, Hospitalized";
                }
                if (pCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVCombRegimens)
                {
                    this.Name = "frmCTCARVCombRegimens";
                    this.Text = "ARV Combination Regimens";
                }
                if (pCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVAdherence)
                {
                    this.Name = "frmCTCARVAdherence";
                    this.Text = "ARV Adherence";
                }
                if (pCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVPoorAdherenceReasons)
                {
                    this.Name = "frmCTCARVAPoordherenceReasons";
                    this.Text = "ARV Poor Adherence Reasons";
                }
                if (pCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ReferedTo)
                {
                    this.Name = "frmCTCReferedTo";
                    this.Text = "Referred To";
                }
                if (pCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVReasons)
                {
                    this.Name = "frmCTCARVReasons";
                    this.Text = "ARV Reasons";
                }
                if (pCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.FollowUpStatus)
                {
                    this.Name = "frmCTCFollowUpStatus";
                    this.Text = "Follow Up Status";
                }
                if (pCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.abnormallabresults)
                {
                    this.Name = "frmCTCAbnormalLabResults";
                    this.Text = "Abnormal Lab Results";
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmCTCCodes_Load
        private void frmCTCCodes_Load(object sender, EventArgs e)
        {
            switch (gDataState.Trim().ToLower())
            {
                case "new": Mode_New(); break;
                case "edit": Mode_Edit(); break;
            }

            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);
            this.Load_Controls();
        }
        #endregion

        #region frmCTCCodes_FormClosing
        private void frmCTCCodes_FormClosing(object sender, FormClosingEventArgs e)
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
                mGridView.OptionsView.ShowGroupPanel = false;
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
                txtCode.Text = "";
                this.Data_Clear();

                txtCode.Focus();

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

                this.Data_Clear();
                pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                txtCode.Text = pSelectedRow["code"].ToString();
                txtDescription.Text = pSelectedRow["description"].ToString();

                if (pCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVCombRegimens)
                {
                    txtCategory.Text = pSelectedRow["category"].ToString();
                }
                if (pCTCCode == (int)AfyaPro_Types.clsEnums.CTCCodes.ARVReasons)
                {
                    txtCategory.Text = pSelectedRow["category"].ToString();
                }

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

        #region Data_Fill
        internal void Data_Fill(GridControl mGridControl)
        {
            string mFunctionName = "Data_Fill";

            try
            {
                //load data
                DataTable mDtCountries = pMdtCTCCodes.View("", "", pCTCCode, Program.gLanguageName, mGridControl.Name);
                mGridControl.DataSource = mDtCountries;
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
            String mFunctionName = "Data_New";

            #region validation
            if (txtCode.Text.Trim() == "")
            {
                Program.Display_Error("Invalid Code");
                txtCode.Focus();
                return;
            }

            if (txtDescription.Text.Trim() == "")
            {
                Program.Display_Error("Invalid Description");
                txtDescription.Focus();
                return;
            }
            #endregion

            try
            {
                //add 
                pResult = pMdtCTCCodes.Add(txtCode.Text, txtDescription.Text, pCTCCode, txtCategory.Text, Program.gCurrentUser.Code);
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
            if (txtCode.Text.Trim() == "")
            {
                Program.Display_Error("Invalid Code");
                txtCode.Focus();
                return;
            }

            if (txtDescription.Text.Trim() == "")
            {
                Program.Display_Error("Invalid Description");
                txtDescription.Focus();
                return;
            }
            #endregion

            try
            {
                //add 
                pResult = pMdtCTCCodes.Edit(txtCode.Text, txtDescription.Text, pCTCCode, txtCategory.Text, Program.gCurrentUser.Code);
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
                pResult = pMdtCTCCodes.Delete(pSelectedRow["code"].ToString().Trim(), pCTCCode, Program.gCurrentUser.Code);
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