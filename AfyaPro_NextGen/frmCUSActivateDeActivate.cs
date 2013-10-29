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
using System.IO;

namespace AfyaPro_NextGen
{
    public partial class frmCUSActivateDeActivate : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsClientGroups pMdtClientGroups;
        private AfyaPro_MT.clsClientSubGroups pMdtClientSubGroups;
        private AfyaPro_MT.clsClientGroupMembers pMdtClientGroupMembers;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private bool pFirstTimeLoad = true;

        private string pPrevMemberId = "";
        private string pCurrMemberId = "";

        private string pGroupCode = "";
        private string pSubGroupCode = "";
        private bool pSearchingMember = false;

        private DataTable pDtCustomerGroups = new DataTable("customergroups");

        #endregion

        #region frmCUSActivateDeActivate
        public frmCUSActivateDeActivate()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCUSActivateDeActivate";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                this.CancelButton = cmdClose;

                pMdtClientGroups = (AfyaPro_MT.clsClientGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsClientGroups),
                    Program.gMiddleTier + "clsClientGroups");

                pMdtClientSubGroups = (AfyaPro_MT.clsClientSubGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsClientSubGroups),
                    Program.gMiddleTier + "clsClientSubGroups");

                pMdtClientGroupMembers = (AfyaPro_MT.clsClientGroupMembers)Activator.GetObject(
                    typeof(AfyaPro_MT.clsClientGroupMembers),
                    Program.gMiddleTier + "clsClientGroupMembers");

                pDtCustomerGroups.Columns.Add("code", typeof(System.String));
                pDtCustomerGroups.Columns.Add("description", typeof(System.String));
                cboGroup.Properties.DataSource = pDtCustomerGroups;
                cboGroup.Properties.DisplayMember = "description";
                cboGroup.Properties.ValueMember = "code";
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmCUSActivateDeActivate_Load
        private void frmCUSActivateDeActivate_Load(object sender, EventArgs e)
        {
            this.Load_Controls();
            this.Fill_LookupData();
        }
        #endregion

        #region frmCUSActivateDeActivate_Activated
        private void frmCUSActivateDeActivate_Activated(object sender, EventArgs e)
        {
            if (pFirstTimeLoad == true)
            {
                cboGroup.Focus();
                pFirstTimeLoad = false;
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
                #region customergroups

                pDtCustomerGroups.Rows.Clear();
                DataTable mDtCustomerGroups = pMdtClientGroups.View("", "code", Program.gLanguageName, "grdBLSCustomerGroups");
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

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(txbGroup);
            mObjectsList.Add(txbFullName);
            mObjectsList.Add(txbCeilingAmount);
            mObjectsList.Add(txbMembershipNo);
            mObjectsList.Add(chkActive);
            mObjectsList.Add(cmdOk);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region Data_Display
        internal void Data_Display(DataRow mDataRow)
        {
            Byte[] mBytes = null;
            string mFunctionName = "Data_Display";

            try
            {
                if (mDataRow == null)
                {
                    return;
                }

                this.Data_Clear();

                pGroupCode = mDataRow["billinggroupcode"].ToString().Trim();
                pSubGroupCode = mDataRow["billingsubgroupcode"].ToString().Trim();
                string mFullName = mDataRow["firstname"].ToString().Trim();
                if (mDataRow["othernames"].ToString().Trim() != "")
                {
                    mFullName = mFullName + " " + mDataRow["othernames"].ToString().Trim();
                }
                mFullName = mFullName + " " + mDataRow["surname"].ToString().Trim();
                txtFullName.Text = mFullName;
                txtCeilingAmount.Text = mDataRow["ceilingamount"].ToString();
                txtMembershipNo.Text = mDataRow["billinggroupmembershipno"].ToString();
                txtExpiryDate.EditValue = Program.DateValueNullable(mDataRow["expirydate"]);
                cboGroup.ItemIndex = Program.Get_LookupItemIndex(cboGroup, "code", mDataRow["billinggroupcode"].ToString());

                if (mDataRow["picturename"].ToString().Trim() != "")
                {
                    try
                    {
                        mBytes = pMdtClientGroupMembers.Load_Picture(txtCode.Text, pGroupCode);
                    }
                    catch { }

                    if (mBytes != null)
                    {
                        MemoryStream mStream = new MemoryStream(mBytes);
                        Image mImage = Image.FromStream(mStream);
                        picPatient.Image = mImage;
                    }
                }

                chkActive.Checked = !Convert.ToBoolean(mDataRow["inactive"]);
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
            txtFullName.Text = "";
            txtCeilingAmount.Text = "";
            txtExpiryDate.EditValue = DBNull.Value;
            picPatient.Image = null;
            chkActive.Checked = false;
        }
        #endregion

        #region Data_Edit
        private void Data_Edit()
        {
            String mFunctionName = "Data_Edit";

            #region validation
            if (Program.IsDate(Program.gMdiForm.txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                return;
            }

            if (txtFullName.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_NameIsInvalid.ToString());
                txtFullName.Focus();
                return;
            }

            #endregion

            try
            {
                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                //Activate/DeActivate 
                pResult = pMdtClientGroupMembers.Activate_DeActivate(pGroupCode,
                    txtMembershipNo.Text, Convert.ToInt16(!chkActive.Checked));
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
                if (chkActive.Checked == true)
                {
                    Program.Display_Info(AfyaPro_Types.clsSystemMessages.MessageIds.CUS_MemberActivationSuccess.ToString());
                }
                else
                {
                    Program.Display_Info(AfyaPro_Types.clsSystemMessages.MessageIds.CUS_MemberDeActivationSuccess.ToString());
                }

                this.Data_Clear();
                txtMembershipNo.Text = "";
                txtMembershipNo.Focus();
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

        #region txtMembershipNo_KeyDown
        private void txtMembershipNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Search_Member();
            }
        }
        #endregion

        #region txtMembershipNo_Leave
        private void txtMembershipNo_Leave(object sender, EventArgs e)
        {
            pCurrMemberId = txtMembershipNo.Text;
            
            if (pCurrMemberId.Trim().ToLower() != pPrevMemberId.Trim().ToLower())
            {
                this.Search_Member();
            }
        }
        #endregion

        #region Search_Member
        private void Search_Member()
        {
            string mFunctionName = "Search_Member";

            try
            {
                string mGroupCode = cboGroup.GetColumnValue("code").ToString().Trim();

                DataTable mDtMembers = pMdtClientGroupMembers.View(
                    "billinggroupmembershipno='" + txtMembershipNo.Text.Trim()
                    + "' and billinggroupcode='" + mGroupCode + "'", "", "", "");
                if (mDtMembers.Rows.Count > 0)
                {
                    this.Data_Display(mDtMembers.Rows[0]);
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdSearch_Click
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            txtCode.Text = "";
            pSearchingMember = true;

            string mGroupCode = "";
            if (cboGroup.ItemIndex != -1)
            {
                mGroupCode = cboGroup.GetColumnValue("code").ToString().Trim();
            }

            frmSearchGroupMember mSearchGroupMember = new frmSearchGroupMember(txtCode);
            mSearchGroupMember.GroupCode = mGroupCode;
            mSearchGroupMember.ShowDialog();

            pSearchingMember = false;
        }
        #endregion

        #region txtCode_EditValueChanged
        private void txtCode_EditValueChanged(object sender, EventArgs e)
        {
            string mFunctionName = "txtCode_EditValueChanged";

            if (pSearchingMember == true)
            {
                try
                {
                    DataTable mDtMembers = pMdtClientGroupMembers.View(
                        "code='" + txtCode.Text.Trim() + "'", "", "", "");
                    if (mDtMembers.Rows.Count > 0)
                    {
                        this.Data_Display(mDtMembers.Rows[0]);
                    }
                }
                catch (Exception ex)
                {
                    Program.Display_Error(pClassName, mFunctionName, ex.Message);
                    return;
                }
            }
        }
        #endregion

        #region cboGroup_EditValueChanged
        private void cboGroup_EditValueChanged(object sender, EventArgs e)
        {
            this.Search_Member();
        }
        #endregion
    }
}