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
    public partial class frmGENAutoCodes : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private ComboBoxItemCollection pIdPositions;
        private DataRow pSelectedRow = null;

        private Int16 pCodeKey = -1;
        private string pCodeDescription = "";

        #endregion

        #region frmGENAutoCodes
        public frmGENAutoCodes()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmGENAutoCodes";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                
                this.CancelButton = cmdClose;

                pMdtAutoCodes = (AfyaPro_MT.clsAutoCodes)Activator.GetObject(
                    typeof(AfyaPro_MT.clsAutoCodes),
                    Program.gMiddleTier + "clsAutoCodes");

                pIdPositions = cboPosition.Properties.Items;

                this.Fill_LookupData();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmGENAutoCodes_Load
        private void frmGENAutoCodes_Load(object sender, EventArgs e)
        {
            this.Load_Controls();

            this.Text = this.Text + " - " + pCodeDescription;
        }
        #endregion

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(chkAutoGenerate);
            mObjectsList.Add(grpId);
            mObjectsList.Add(txbSeed);
            mObjectsList.Add(txbIncrement);
            mObjectsList.Add(txbLength);
            mObjectsList.Add(txbCurrent);
            mObjectsList.Add(txbPosition);
            mObjectsList.Add(grpPrefix);
            mObjectsList.Add(radPrefType.Properties.Items[0]);
            mObjectsList.Add(radPrefType.Properties.Items[1]);
            mObjectsList.Add(txtPrefText);
            mObjectsList.Add(txtPrefSep);
            mObjectsList.Add(grpSurfix);
            mObjectsList.Add(radSurfType.Properties.Items[0]);
            mObjectsList.Add(radSurfType.Properties.Items[1]);
            mObjectsList.Add(txtSurfText);
            mObjectsList.Add(txtSurfSep);
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

        #region Mode_Edit
        internal void Mode_Edit()
        {
            String mFunctionName = "Mode_Edit";

            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                pCodeKey = Convert.ToInt16(pSelectedRow["codekey"]);
                pCodeDescription = pSelectedRow["codedescription"].ToString().Trim();
                chkAutoGenerate.Checked = Convert.ToBoolean(pSelectedRow["autogenerate"]);
                txtSeed.Text = pSelectedRow["idseed"].ToString().Trim();
                txtIncrement.Text = pSelectedRow["idincrement"].ToString().Trim();
                txtLength.Text = pSelectedRow["idlength"].ToString().Trim();
                txtCurrent.Text = pSelectedRow["idcurrent"].ToString().Trim();
                cboPosition.SelectedIndex = Convert.ToInt16(pSelectedRow["position"]);
                radPrefType.SelectedIndex = Convert.ToInt16(pSelectedRow["preftype"]);
                txtPrefText.Text = pSelectedRow["preftext"].ToString().Trim();
                txtPrefSep.Text = pSelectedRow["prefsep"].ToString().Trim();
                radSurfType.SelectedIndex = Convert.ToInt16(pSelectedRow["surftype"]);
                txtSurfText.Text = pSelectedRow["surftext"].ToString().Trim();
                txtSurfSep.Text = pSelectedRow["surfsep"].ToString().Trim();

                String mGeneratedCode = pMdtAutoCodes.Sample_Code(pCodeKey);
                txbSample.Text = mGeneratedCode;
            }
            catch (Exception ex)
            {
                Program.gMdiForm.Cursor = Cursors.Default;
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region idpositions

                DataTable mDtIdPositions = pMdtAutoCodes.Get_IdPositions(Program.gLanguageName, "grdGENAutoCodes");
                pIdPositions.Clear();
                foreach (DataRow mDataRow in mDtIdPositions.Rows)
                {
                    pIdPositions.Add(mDataRow["description"].ToString());
                }

                #endregion
            }
            catch (Exception ex)
            {
                Program.gMdiForm.Cursor = Cursors.Default;
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
                DataTable mDtAutoCodes = pMdtAutoCodes.View("", "", Program.gLanguageName, mGridControl.Name);
                mGridControl.DataSource = mDtAutoCodes;

                //expand all groups
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)mGridControl.MainView;
                mGridView.ExpandAllGroups();
            }
            catch (Exception ex)
            {
                Program.gMdiForm.Cursor = Cursors.Default;
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Data_Edit
        private void Data_Edit()
        {
            Int16 mPrefType = 0;
            Int16 mSurfType = 0;
            String mFunctionName = "Data_Edit";

            try
            {
                if (Program.IsNumeric(txtSeed.Text) == false)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GEN_IdSeedIsInvalid.ToString());
                    txtSeed.Focus();
                    return;
                }

                if (Program.IsNumeric(txtIncrement.Text) == false)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GEN_IdIncrementIsInvalid.ToString());
                    txtIncrement.Focus();
                    return;
                }

                if (Program.IsNumeric(txtLength.Text) == false)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GEN_IdLengthIsInvalid.ToString());
                    txtLength.Focus();
                    return;
                }

                if (Program.IsNumeric(txtCurrent.Text) == false)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GEN_IdCurrentIsInvalid.ToString());
                    txtCurrent.Focus();
                    return;
                }

                mPrefType = Convert.ToInt16(radPrefType.SelectedIndex);
                mSurfType = Convert.ToInt16(radSurfType.SelectedIndex);

                //add 
                pResult = pMdtAutoCodes.Save(pCodeKey, Convert.ToInt16(chkAutoGenerate.Checked), mPrefType,
                    txtPrefText.Text.Trim(), txtPrefSep.Text.Trim(), mSurfType, txtSurfText.Text.Trim(),
                    txtSurfSep.Text.Trim(), Convert.ToInt32(txtSeed.Text), Convert.ToInt32(txtLength.Text),
                    Convert.ToInt32(txtCurrent.Text), Convert.ToInt32(txtIncrement.Text),
                    Convert.ToInt16(cboPosition.SelectedIndex), Program.gCurrentUser.Code);
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
                String mGeneratedCode = pMdtAutoCodes.Sample_Code(pCodeKey);
                txbSample.Text = mGeneratedCode;

                Program.Display_Info(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SettingsSavedSuccessfully.ToString());
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