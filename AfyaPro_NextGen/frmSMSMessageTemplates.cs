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
    public partial class frmSMSMessageTemplates : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsDiagnosesGroups pMdtDiagnosesGroups;
        private AfyaPro_MT.clsDiagnosesSubGroups pMdtDiagnosesSubGroups;
        private AfyaPro_MT.clsSMS pMdtSMS;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataTable pDtGroups = new DataTable("groups");
        private DataTable pDtSubGroups = new DataTable("subgroups");
        private DataRow pSelectedRow = null;
        internal string gDataState = "";

        #endregion

        #region frmSMSMessageTemplates
        public frmSMSMessageTemplates(DataGridViewRow mSelectedRow, string mMode)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmSMSMessageTemplates";

            gDataState = mMode;

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                this.CancelButton = cmdClose;

               
                pMdtSMS = (AfyaPro_MT.clsSMS)Activator.GetObject(
                    typeof(AfyaPro_MT.clsSMS),
                    Program.gMiddleTier + "clsSMS");

                pDtGroups.Columns.Add("messagecode", typeof(System.String));
                pDtGroups.Columns.Add("message", typeof(System.String));
                pDtGroups.Columns.Add("clients", typeof(System.String));
                pDtGroups.Columns.Add("createdon", typeof(System.DateTime));
                pDtGroups.Columns.Add("createdby", typeof(System.String));

                DataTable mDtPatientGoups = pMdtSMS.Get_PatientGroups();
                foreach (DataRow mRow in mDtPatientGoups.Rows)
                {
                    cboClients.Properties.Items.Add(mRow["colgroup"].ToString());
                }

                if (mMode == "Edit")
                {
                    txtMessageCode.Text = mSelectedRow.Cells[0].Value.ToString();
                    txtMessage.Text = mSelectedRow.Cells[1].Value.ToString();
                    cboClients.Text = mSelectedRow.Cells[2].Value.ToString();
                    txtMessageCode.Enabled = false;
                    txtAuthor.Text = mSelectedRow.Cells[4].Value.ToString();
                }
                else
                {
                    txtMessageCode.Enabled =true;
                }
                

                this.Fill_LookupData();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmSMSMessageTemplates_Load
        private void frmSMSMessageTemplates_Load(object sender, EventArgs e)
        {
            switch (gDataState.Trim().ToLower())
            {
                case "new": Mode_New(); break;
                case "edit": Mode_Edit(); break;
            }

            this.Load_Controls();
        }
        #endregion

        #region cboGroup_EditValueChanged
        private void cboGroup_EditValueChanged(object sender, EventArgs e)
        {
            DataRow mNewRow;
            string mFunctionName = "cboGroup_EditValueChanged";

            try
            {
                #region sub groups

                pDtSubGroups.Rows.Clear();
                DataTable mDtSubGroups = pMdtDiagnosesSubGroups.View("", "code", Program.gLanguageName, "grdDXTSubGroups");
                foreach (DataRow mDataRow in mDtSubGroups.Rows)
                {
                    mNewRow = pDtSubGroups.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtSubGroups.Rows.Add(mNewRow);
                    pDtSubGroups.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtSubGroups.Columns)
                {
                    mDataColumn.Caption = mDtSubGroups.Columns[mDataColumn.ColumnName].Caption;
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

            mObjectsList.Add(txbGroup);
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

                DataTable mDtRegions = pMdtSMS.View_MessageTemplates("", "", Program.gLanguageName, mGridControl.Name, true);
                mGridControl.DataSource = mDtRegions;
                mGridControl.DataSource = mDtRegions;

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
           
        }
        #endregion

        #region Mode_Edit
        private void Mode_Edit()
        {
           
        }
        #endregion

        #region Data_Clear
        private void Data_Clear()
        {
            txtAuthor.Text = "";
        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            DataRow mNewRow;
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region groups

                pDtGroups.Rows.Clear();
                DataTable mDtGroups = pMdtSMS.View_MessageTemplates("", "", Program.gLanguageName, " ", true);
                foreach (DataRow mDataRow in mDtGroups.Rows)
                {
                    mNewRow = pDtGroups.NewRow();
                    mNewRow["messagecode"] = mDataRow["messagecode"].ToString();
                    mNewRow["message"] = mDataRow["message"].ToString();
                    mNewRow["clients"] = mDataRow["clients"].ToString();
                    mNewRow["createdon"] = Convert.ToDateTime(mDataRow["createdon"]);
                    mNewRow["createdby"] = mDataRow["createdby"].ToString();
                    pDtGroups.Rows.Add(mNewRow);
                    pDtGroups.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtGroups.Columns)
                {
                    mDataColumn.Caption = mDtGroups.Columns[mDataColumn.ColumnName].Caption;
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

        #region Data_Fill
        internal void Data_Fill(DataGridView mGridControl)
        {
            string mFunctionName = "Data_Fill";

            try
            {
                //load data
                DataTable mDtRegions = pMdtSMS.View_MessageTemplates("", "", Program.gLanguageName, mGridControl.Name, true);
                mGridControl.DataSource = mDtRegions;
               
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
            if (txtMessageCode.Text =="")
            {
                Program.Display_Error("Please enter message code");
                txtMessageCode.Focus();
                return;
            }

            if (txtMessage.Text == "")
            {
                Program.Display_Error("Please enter message");
                txtMessage.Focus();
                return;
            }

            if (cboClients.SelectedIndex ==-1)
            {
                Program.Display_Error("Please select target group");
                cboClients.Focus();
                return;
            }

           
            #endregion

            try
            {
                //add 

                Boolean mResult = pMdtSMS.Add_MessageTemplate(txtMessageCode.Text, txtMessage.Text, cboClients.Text, txtAuthor.Text, Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue)); 
                if (mResult ==false)
                {
                    Program.Display_Error("There was an error, unable to save");
                    return;
                }
                if (mResult ==true)
                {
                    MessageBox.Show("Message template added successfully", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }

                //refresh
                //this.Data_Fill(((frmMessageTemplates)Program.gMdiForm.ActiveMdiChild).grdMain);
                ((frmMessageTemplates)Program.gMdiForm.ActiveMdiChild).Close();
                
                frmMessageTemplates mTpt = new frmMessageTemplates();
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
            if (txtMessageCode.Text == "")
            {
                Program.Display_Error("Please enter message code");
                txtMessageCode.Focus();
                return;
            }

            if (txtMessage.Text == "")
            {
                Program.Display_Error("Please enter message");
                txtMessage.Focus();
                return;
            }

            if (cboClients.SelectedIndex == -1)
            {
                Program.Display_Error("Please select target group");
                cboClients.Focus();
                return;
            }


            #endregion

            try
            {
                //add 

                Boolean mResult = pMdtSMS.Edit_MessageTemplate(txtMessageCode.Text, txtMessage.Text, cboClients.Text, txtAuthor.Text, Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue));
                if (mResult == false)
                {
                    Program.Display_Error("There was an error, unable to save");
                    return;
                }
                if (mResult == true)
                {
                    MessageBox.Show("Message template updated successfully", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                //refresh
                //this.Data_Fill(((frmMessageTemplates)Program.gMdiForm.ActiveMdiChild).grdMain);
                ((frmMessageTemplates)Program.gMdiForm.ActiveMdiChild).Close();

                frmMessageTemplates mTpt = new frmMessageTemplates();
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
            DialogResult rst = MessageBox.Show("Delete " + frmMessageTemplates.pRow.Cells[0].Value.ToString() + " ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rst == DialogResult.No)
            {
                return;
            }
            else
            {
             Result  = pMdtSMS.Delete_MessageTemplate(frmMessageTemplates.pRow.Cells[0].Value.ToString());

            }
               
                
            if (Result == false )
                
            {
                    Program.Display_Error("Unable to delete, there was an error");
                    return;
               
            }
                
            if (Result ==true)
               
            {
                    Program.Display_Error("Message template deleted successfully");
                  
            }
            ((frmMessageTemplates)Program.gMdiForm.ActiveMdiChild).Close();

            frmMessageTemplates mTpt = new frmMessageTemplates();
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