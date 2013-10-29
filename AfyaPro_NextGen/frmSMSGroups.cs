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

namespace AfyaPro_NextGen
{
    public partial class frmSMSGroups : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

       
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataTable pDtGroups = new DataTable("groups");
        private DataTable pDtSubGroups = new DataTable("subgroups");
        private DataRow pSelectedRow = null;
        internal string gDataState = "";

        private AfyaPro_MT.clsSMS pMdtSMS;

        #endregion

        #region frmSMSGroups
        public frmSMSGroups(DataGridViewRow mSelectedRow, string mMode)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;

            gDataState = mMode;
            string mFunctionName = "frmSMSGroups";

            if (mMode == "Edit")
            {
                txtGropu.Text = mSelectedRow.Cells[0].Value.ToString();
                txtDescription.Text = mSelectedRow.Cells[1].Value.ToString();
                txtCode.Text = mSelectedRow.Cells[2].Value.ToString();
            }

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                this.CancelButton = cmdClose;

                pMdtSMS = (AfyaPro_MT.clsSMS)Activator.GetObject(
                  typeof(AfyaPro_MT.clsSMS),
                  Program.gMiddleTier + "clsSMS");

                 
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmDXTDiagnoses_Load
        private void frmDXTDiagnoses_Load(object sender, EventArgs e)
        {
            switch (gDataState.Trim().ToLower())
            {
                case "new": Mode_New(); break;
                case "edit": Mode_Edit(); break;
            }

            this.Load_Controls();
        }
        #endregion
             

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(txbGroup);
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
                txtGropu.Text = "";
                this.Data_Clear();

               

                txtGropu.Focus();

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
               
                

                txtDescription.Focus();

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
                DataTable mDtRegions = pMdtSMS.Get_PatientGroups();
                mGridControl.DataSource = mDtRegions;

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
            String mFunctionName = "Data_New";

            #region validation

            if (txtCode.Text == "")
            {
                Program.Display_Error("Invalid code");
                txtGropu.Focus();
                return;
            }
            if (txtGropu.Text  =="")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DxGroupDescriptionIsInvalid.ToString());
                txtGropu.Focus();
                return;
            }
                        

            if (txtDescription.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DxDescriptionIsInvalid.ToString());
                txtDescription.Focus();
                return;
            }
            #endregion

            try
            {
                //add 
                Boolean Result;
                Result = pMdtSMS.Add_ClientGroup(txtGropu.Text, txtDescription.Text, txtCode.Text);
                    
                if (Result  == false)
                {
                    Program.Display_Error("Unable to save, Error occured");
                    return;
                }
                if (Result  == true )
                {
                    Program.Display_Info("Patient group saved successfully");
                     
                }

                //refresh
                ((frmSMSClientGroups)Program.gMdiForm.ActiveMdiChild).Close();

                frmSMSClientGroups mTpt = new frmSMSClientGroups();
                mTpt.MdiParent = Program.gMdiForm;
                mTpt.Show();
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
            

            if (txtGropu.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DxCodeIsInvalid.ToString());
                txtGropu.Focus();
                return;
            }

            if (txtDescription.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DxDescriptionIsInvalid.ToString());
                txtDescription.Focus();
                return;
            }
            #endregion

            try
            {
                Boolean Result;
                Result = pMdtSMS.Edit_PatientGroups(txtGropu.Text, txtDescription.Text, txtCode.Text);

                if (Result == false)
                {
                    Program.Display_Error("Unable to save, Error occured");
                    return;
                }
                if (Result == true)
                {
                    Program.Display_Info("Patient group updated successfully");

                }

                //refresh
                ((frmSMSClientGroups)Program.gMdiForm.ActiveMdiChild).Close();

                frmSMSClientGroups mTpt = new frmSMSClientGroups();
                mTpt.MdiParent = Program.gMdiForm;
                mTpt.Show();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Data_Delete
        internal void Data_Delete(DataGridViewRow mRow)
        {
            String mFunctionName = "Data_Delete";

            try
            {
                Boolean Result;
                DialogResult rst = MessageBox.Show("Delete " + frmSMSClientGroups.pRow.Cells[2].Value.ToString() + " ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rst == DialogResult.No)
                {
                    return;
                }
                else
                {
                    Result = pMdtSMS.Delete_PatientGroup(frmSMSClientGroups.pRow.Cells[2].Value.ToString());

                }


                if (Result == false)
                {
                    Program.Display_Error("Unable to delete, there was an error");
                    return;

                }

                if (Result == true)
                {
                    Program.Display_Error("Client Group deleted successfully");

                }
                ((frmSMSClientGroups)Program.gMdiForm.ActiveMdiChild).Close();

                frmSMSClientGroups mTpt = new frmSMSClientGroups();
                mTpt.MdiParent = Program.gMdiForm;
                mTpt.Show();

                
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