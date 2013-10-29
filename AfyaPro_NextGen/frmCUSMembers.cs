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
using DevExpress.XtraEditors;
using System.IO;

namespace AfyaPro_NextGen
{
    public partial class frmCUSMembers : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsClientGroups pMdtClientGroups;
        private AfyaPro_MT.clsClientSubGroups pMdtClientSubGroups;
        private AfyaPro_MT.clsClientGroupMembers pMdtClientGroupMembers;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataTable pDtCustomerGroups = new DataTable("customergroups");
        private DataTable pDtCustomerSubGroups = new DataTable("customersubgroups");
        private DataRow pSelectedRow = null;
        internal string gDataState = "";
        private bool pFirstTimeLoad = true;

        private bool pGroupHasId = false;
        private bool pGroupHasSubGroups = false;

        #endregion

        #region frmCUSMembers
        public frmCUSMembers()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCUSMembers";

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

                pMdtAutoCodes = (AfyaPro_MT.clsAutoCodes)Activator.GetObject(
                    typeof(AfyaPro_MT.clsAutoCodes),
                    Program.gMiddleTier + "clsAutoCodes");

                pDtCustomerGroups.Columns.Add("code", typeof(System.String));
                pDtCustomerGroups.Columns.Add("description", typeof(System.String));
                cboGroup.Properties.DataSource = pDtCustomerGroups;
                cboGroup.Properties.DisplayMember = "description";
                cboGroup.Properties.ValueMember = "code";

                pDtCustomerSubGroups.Columns.Add("code", typeof(System.String));
                pDtCustomerSubGroups.Columns.Add("description", typeof(System.String));
                cboSubGroup.Properties.DataSource = pDtCustomerSubGroups;
                cboSubGroup.Properties.DisplayMember = "description";
                cboSubGroup.Properties.ValueMember = "code";

                txtBirthDate.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                txtExpiryDate.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);

                this.Fill_LookupData();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region mDateEdit_EditValueChanged
        void mDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            Program.AddTimeToDate((DateEdit)sender);
        }
        #endregion

        #region frmCUSMembers_Load
        private void frmCUSMembers_Load(object sender, EventArgs e)
        {
            switch (gDataState.Trim().ToLower())
            {
                case "new": Mode_New(); break;
                case "edit": Mode_Edit(); break;
            }

            this.Load_Controls();
        }
        #endregion

        #region frmCUSMembers_Activated
        private void frmCUSMembers_Activated(object sender, EventArgs e)
        {
            if (pFirstTimeLoad == true)
            {
                if (gDataState.Trim().ToLower() == "new")
                {
                    if (txtCode.Text.Trim().ToLower() == "<<---new--->>")
                    {
                        txtSurname.Focus();
                    }
                    else
                    {
                        txtCode.Focus();
                    }
                }
                else
                {
                    txtSurname.Focus();
                }
                pFirstTimeLoad = false;
            }
        }
        #endregion

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(txbGroup);
            mObjectsList.Add(txbSubGroup);
            mObjectsList.Add(txbCode);
            mObjectsList.Add(txbSurname);
            mObjectsList.Add(txbCeilingAmount);
            mObjectsList.Add(txbMembershipNo);
            mObjectsList.Add(txbExpiryDate);
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
            Int16 mGenerateCode;
            String mFunctionName = "Mode_New";

            try
            {
                txtCode.Text = "";
                txtMembershipNo.Text = "";
                this.Data_Clear();

                #region display owning groups if any
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain.MainView;

                if (mGridView.FocusedRowHandle >= 0)
                {
                    pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);
                    cboGroup.ItemIndex = Program.Get_LookupItemIndex(cboGroup, "code", pSelectedRow["billinggroupcode"].ToString());
                    cboSubGroup.ItemIndex = Program.Get_LookupItemIndex(cboSubGroup, "code", pSelectedRow["billingsubgroupcode"].ToString());
                }
                #endregion

                #region code
                mGenerateCode = pMdtAutoCodes.Auto_Generate_Code(
                     Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.clientgroupmembercode));
                if (mGenerateCode == -1)
                {
                    Program.Display_Server_Error("");
                    return;
                }
                if (mGenerateCode == 1)
                {
                    txtCode.Text = "<<---New--->>";
                    txtCode.Enabled = false;
                    txtSurname.Focus();
                }
                else
                {
                    txtCode.Enabled = true;
                    txtCode.Focus();
                }
                #endregion

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

                cboGroup.EditValue = null;
                this.Data_Clear();
                pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                this.Data_Display(pSelectedRow);
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
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

                cboGroup.EditValue = null;
                this.Data_Clear();

                cboGroup.ItemIndex = Program.Get_LookupItemIndex(cboGroup, "code", mDataRow["billinggroupcode"].ToString());
                cboSubGroup.ItemIndex = Program.Get_LookupItemIndex(cboSubGroup, "code", mDataRow["billingsubgroupcode"].ToString());
                txtCode.Text = mDataRow["code"].ToString();
                txtSurname.Text = mDataRow["surname"].ToString();
                txtFirstName.Text = mDataRow["firstname"].ToString();
                txtOtherNames.Text = mDataRow["othernames"].ToString();
                if (mDataRow["gender"].ToString().Trim().ToLower() == "f")
                {
                    radGender.SelectedIndex = 0;
                }
                else
                {
                    radGender.SelectedIndex = 1;
                }
                txtExpiryDate.EditValue = Program.DateValueNullable(mDataRow["birthdate"]);
                txtCeilingAmount.Text = mDataRow["ceilingamount"].ToString();
                txtMembershipNo.Text = mDataRow["billinggroupmembershipno"].ToString();
                txtExpiryDate.EditValue = Program.DateValueNullable(mDataRow["expirydate"]);

                //if (mDataRow["cod"].ToString().Trim() != "")
                //{
                    try
                    {
                        mBytes = pMdtClientGroupMembers.Load_Picture(txtCode.Text, mDataRow["billinggroupcode"].ToString());
                    }
                    catch { }

                    if (mBytes != null)
                    {
                        MemoryStream mStream = new MemoryStream(mBytes);
                        Image mImage = Image.FromStream(mStream);
                        picPatient.Image = mImage;
                    }
                //}

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
            txtSurname.Text = "";
            txtFirstName.Text = "";
            txtOtherNames.Text = "";
            radGender.SelectedIndex = -1;
            txtMembershipNo.Text = "";
            txtCeilingAmount.Text = "";
            txtBirthDate.EditValue = DBNull.Value;
            txtExpiryDate.EditValue = DBNull.Value;
            picPatient.Image = null;
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
                DataTable mDtCustomerGroups = pMdtClientGroups.View("", "code", Program.gLanguageName, "grdCUSCustomerGroups");
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

        #region cboGroup_EditValueChanged
        void cboGroup_EditValueChanged(object sender, EventArgs e)
        {
            DataRow mNewRow;
            string mFunctionName = "cboGroup_EditValueChanged";

            try
            {
                pDtCustomerSubGroups.Rows.Clear();
                if (cboGroup.ItemIndex == -1)
                {
                    return;
                }

                pGroupHasId = false;
                pGroupHasSubGroups = false;
                cboSubGroup.EditValue = null;
                txtMembershipNo.Text = "";

                string mGroupCode = cboGroup.GetColumnValue("code").ToString().Trim();

                DataTable mDtGroups = pMdtClientGroups.View("code='" + mGroupCode + "'", "", Program.gLanguageName, "grdCUSCustomerGroups");
                if (mDtGroups.Rows.Count > 0)
                {
                    pGroupHasId = Convert.ToBoolean(mDtGroups.Rows[0]["hasid"]);
                    pGroupHasSubGroups = Convert.ToBoolean(mDtGroups.Rows[0]["hassubgroups"]);
                }

                if (pGroupHasSubGroups == true)
                {
                    DataTable mDtSubGroups = pMdtClientSubGroups.View("groupcode='" + mGroupCode + "'", "code", Program.gLanguageName, "grdCUSCustomerSubGroups");

                    foreach (DataRow mDataRow in mDtSubGroups.Rows)
                    {
                        mNewRow = pDtCustomerSubGroups.NewRow();
                        mNewRow["code"] = mDataRow["code"].ToString();
                        mNewRow["description"] = mDataRow["description"].ToString();
                        pDtCustomerSubGroups.Rows.Add(mNewRow);
                        pDtCustomerSubGroups.AcceptChanges();
                    }

                    foreach (DataColumn mDataColumn in pDtCustomerSubGroups.Columns)
                    {
                        mDataColumn.Caption = mDtSubGroups.Columns[mDataColumn.ColumnName].Caption;
                    }
                }

                #region membership id
                Int16 mGenerateCode = pMdtAutoCodes.Auto_Generate_Code(
                     Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.clientgroupmembershipId));
                if (mGenerateCode == -1)
                {
                    Program.Display_Server_Error("");
                    return;
                }
                if (mGenerateCode == 1 && pGroupHasId == false)
                {
                    txtMembershipNo.Text = "<<---New--->>";
                    txtMembershipNo.Enabled = false;
                }
                else
                {
                    txtMembershipNo.Enabled = true;
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
        internal void Data_Fill(GridControl mGridControl)
        {
            string mFunctionName = "Data_Fill";

            try
            {
                //load data
                DataTable mDtCustomerGroupMembers = pMdtClientGroupMembers.View("", "", Program.gLanguageName, mGridControl.Name);
                mGridControl.DataSource = mDtCustomerGroupMembers;

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
            string mGender = "F";
            string mSubGroupCode = "";
            double mCeilingAmount = 0;
            DateTime mBirthDate = new DateTime();
            DateTime mExpiryDate = new DateTime();
            Byte[] mPicture = null;
            MemoryStream mStream = new MemoryStream();
            string mExtension = "";
            Int16 mGenerateCode = 0;
            Int16 mGenerateMemberId = 0;

            String mFunctionName = "Data_New";

            #region validation

            if (Program.IsDate(Program.gMdiForm.txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                return;
            }

            if (cboGroup.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_CustomerGroupIsInvalid.ToString());
                cboGroup.Focus();
                return;
            }

            if (pGroupHasSubGroups == true && cboGroup.ItemIndex != -1)
            {
                if (cboSubGroup.ItemIndex == -1)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_CustomerSubGroupIsInvalid.ToString());
                    cboSubGroup.Focus();
                    return;
                }
                mSubGroupCode = cboSubGroup.GetColumnValue("code").ToString().Trim();
            }

            if (txtCode.Text.Trim() == "" && txtCode.Text.Trim().ToLower() != "<<---new--->>")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.CUS_MemberCodeIsInvalid.ToString());
                txtCode.Focus();
                return;
            }

            if (txtSurname.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_NameIsInvalid.ToString());
                txtSurname.Focus();
                return;
            }

            if (txtFirstName.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_NameIsInvalid.ToString());
                txtFirstName.Focus();
                return;
            }

            if (radGender.SelectedIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_GenderIsInvalid.ToString());
                txtFirstName.Focus();
                return;
            }

            if (txtMembershipNo.Text.Trim() == "" && txtMembershipNo.Text.Trim().ToLower() != "<<---new--->>")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.CUS_MembershipIdIsInvalid.ToString());
                txtMembershipNo.Focus();
                return;
            }
            #endregion

            try
            {
                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                if (txtCode.Text.Trim().ToLower() == "<<---new--->>")
                {
                    mGenerateCode = 1;
                }

                if (txtMembershipNo.Text.Trim().ToLower() == "<<---new--->>")
                {
                    mGenerateMemberId = 1;
                }

                if (Program.IsDate(txtBirthDate.EditValue) == true)
                {
                    mBirthDate = Convert.ToDateTime(txtBirthDate.EditValue);
                }

                if (Program.IsDate(txtExpiryDate.EditValue) == true)
                {
                    mExpiryDate = Convert.ToDateTime(txtExpiryDate.EditValue);
                }

                if (picPatient.Image != null)
                {
                    picPatient.Image.Save(mStream, picPatient.Image.RawFormat);
                    mPicture = mStream.ToArray();

                    if (picPatient.Image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                    {
                        mExtension = ".bmp";
                    }
                    else if (picPatient.Image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
                    {
                        mExtension = ".gif";
                    }
                    else if (picPatient.Image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                    {
                        mExtension = ".jpg";
                    }
                    else if (picPatient.Image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                    {
                        mExtension = ".png";
                    }
                    else
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_ImageFormatIsNotSupported.ToString());
                        return;
                    }
                }

                if (Program.IsMoney(txtCeilingAmount.Text) == true)
                {
                    mCeilingAmount = Convert.ToDouble(txtCeilingAmount.Text);
                }

                if (radGender.SelectedIndex == 1)
                {
                    mGender = "M";
                }

                //add 
                pResult = pMdtClientGroupMembers.Add(mGenerateCode, mGenerateMemberId, txtCode.Text, txtSurname.Text,
                    txtFirstName.Text, txtOtherNames.Text, mGender, mBirthDate, cboGroup.GetColumnValue("code").ToString(),
                    mSubGroupCode, txtMembershipNo.Text, mCeilingAmount, mTransDate, mExpiryDate, mPicture, mExtension);
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
            string mGender = "F";
            string mSubGroupCode = "";
            double mCeilingAmount = 0;
            DateTime mBirthDate = new DateTime();
            DateTime mExpiryDate = new DateTime();
            Byte[] mPicture = null;
            MemoryStream mStream = new MemoryStream();
            string mExtension = "";

            String mFunctionName = "Data_Edit";

            #region validation
            if (Program.IsDate(Program.gMdiForm.txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                return;
            }

            if (cboGroup.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_CustomerGroupIsInvalid.ToString());
                cboGroup.Focus();
                return;
            }

            if (pGroupHasSubGroups == true && cboGroup.ItemIndex != -1)
            {
                if (cboSubGroup.ItemIndex == -1)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_CustomerSubGroupIsInvalid.ToString());
                    cboSubGroup.Focus();
                    return;
                }
                mSubGroupCode = cboSubGroup.GetColumnValue("code").ToString().Trim();
            }

            if (txtCode.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.CUS_MemberCodeIsInvalid.ToString());
                txtCode.Focus();
                return;
            }

            if (txtSurname.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_NameIsInvalid.ToString());
                txtSurname.Focus();
                return;
            }

            if (txtFirstName.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_NameIsInvalid.ToString());
                txtFirstName.Focus();
                return;
            }

            if (radGender.SelectedIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_GenderIsInvalid.ToString());
                txtFirstName.Focus();
                return;
            }

            #endregion

            try
            {
                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                if (picPatient.Image != null)
                {
                    picPatient.Image.Save(mStream, picPatient.Image.RawFormat);
                    mPicture = mStream.GetBuffer();

                    if (picPatient.Image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                    {
                        mExtension = ".bmp";
                    }
                    else if (picPatient.Image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Icon))
                    {
                        mExtension = ".ico";
                    }
                    else if (picPatient.Image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
                    {
                        mExtension = ".gif";
                    }
                    else if (picPatient.Image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                    {
                        mExtension = ".jpg";
                    }
                    else if (picPatient.Image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                    {
                        mExtension = ".png";
                    }
                    else
                    {
                        Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_ImageFormatIsNotSupported.ToString());
                        return;
                    }
                }

                if (Program.IsDate(txtBirthDate.EditValue) == true)
                {
                    mBirthDate = Convert.ToDateTime(txtBirthDate.EditValue);
                }

                if (radGender.SelectedIndex == 1)
                {
                    mGender = "M";
                }

                //Edit 
                //pResult = pMdtClientGroupMembers.Edit(cboGroup.GetColumnValue("code").ToString().Trim(),
                //    txtCode.Text, txtSurname.Text, txtMembershipNo.Text,
                //    mCeilingAmount, mTransDate, mExpiryDate, mPicture, mExtension);
                pResult = pMdtClientGroupMembers.Edit(txtCode.Text, txtSurname.Text,
                    txtFirstName.Text, txtOtherNames.Text, mGender, mBirthDate, cboGroup.GetColumnValue("code").ToString(),
                    mSubGroupCode, txtMembershipNo.Text, mCeilingAmount, mTransDate, mExpiryDate, mPicture, mExtension);
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

                DialogResult mResp = Program.Confirm_Deletion(pSelectedRow["code"].ToString().Trim() + "'   '"
                    + pSelectedRow["fullname"].ToString().Trim() + "'");

                if (mResp != DialogResult.Yes)
                {
                    return;
                }

                //Delete 
                pResult = pMdtClientGroupMembers.Delete(pSelectedRow["billinggroupcode"].ToString().Trim(),
                    pSelectedRow["code"].ToString().Trim());
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