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
using DevExpress.XtraLayout;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.IO;
using System.Xml.Serialization;

namespace AfyaPro_NextGen
{
    public partial class frmRCHPostnatalChildren : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsRCHPostNatalCare pMdtRCHPostNatalCare;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private ComboBoxItemCollection pStillBirths;
        private ComboBoxItemCollection pChildConditions;
        private DataTable pDtStillBirths = new DataTable("stillbirths");
        private DataTable pDtChildConditions = new DataTable("childconditions");
        private DataTable pDtChildren = new DataTable("children");
        private string pClientCode = "";
        private string pBooking = "";

        #endregion

        #region frmRCHPostnatalChildren
        public frmRCHPostnatalChildren(string mClientCode, string mBooking, DataTable mDtChildren)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmRCHPostnatalChildren";

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                this.pClientCode = mClientCode;
                this.pBooking = mBooking;
                this.pDtChildren = mDtChildren;

                pMdtRCHPostNatalCare = (AfyaPro_MT.clsRCHPostNatalCare)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRCHPostNatalCare),
                    Program.gMiddleTier + "clsRCHPostNatalCare");

                //columns for still births
                pDtStillBirths.Columns.Add("description", typeof(System.String));
                pDtStillBirths.Columns.Add("fieldname", typeof(System.String));
                pDtStillBirths.Columns.Add("fieldindex", typeof(System.Int32));
                pStillBirths = cboStillBirths.Properties.Items;

                //columns for child conditions
                pDtChildConditions.Columns.Add("description", typeof(System.String));
                pDtChildConditions.Columns.Add("fieldname", typeof(System.String));
                pDtChildConditions.Columns.Add("fieldindex", typeof(System.Int32));
                pChildConditions = cboChildConditions.Properties.Items;
                
                grdChildren.DataSource = pDtChildren;

                //hide unneccessary columns
                foreach (DevExpress.XtraGrid.Columns.GridColumn mGridColumn in viewChildren.Columns)
                {
                    if (mGridColumn.FieldName.ToLower() != "deliverydate"
                        && mGridColumn.FieldName.ToLower() != "gender"
                        && mGridColumn.FieldName.ToLower() != "weight"
                        && mGridColumn.FieldName.ToLower() != "apgarscore"
                        && mGridColumn.FieldName.ToLower() != "childproblems")
                    {
                        mGridColumn.Visible = false;
                    }
                }

                //fill lookup data
                this.Fill_StillBirths();
                this.Fill_ChildConditions();

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.rchpostnatal_customizelayout.ToString());
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
            List<Object> mObjectsList = new List<object>();

            mObjectsList.Add(txbDeliveryDate);
            mObjectsList.Add(radGender.Properties.Items[0]);
            mObjectsList.Add(radGender.Properties.Items[1]);
            mObjectsList.Add(txbWeight);
            mObjectsList.Add(txbApgarScore);
            mObjectsList.Add(txbStillBirths);
            mObjectsList.Add(txbChildConditions);
            mObjectsList.Add(txbChildProblems);
            mObjectsList.Add(cmdAdd);
            mObjectsList.Add(cmdDelete);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }

        #endregion

        #region frmRCHPostnatalChildren_Load
        private void frmRCHPostnatalChildren_Load(object sender, EventArgs e)
        {
            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            this.Load_Controls();

            txtDeliveryDate.EditValue = Program.gMdiForm.txtDate.EditValue;

            this.pFormWidth = this.Width;
            this.pFormHeight = this.Height;

            Program.Center_Screen(this);

            this.Append_ShortcutKeys();
        }
        #endregion

        #region frmRCHPostnatalChildren_FormClosing
        private void frmRCHPostnatalChildren_FormClosing(object sender, FormClosingEventArgs e)
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

        #region Append_ShortcutKeys
        private void Append_ShortcutKeys()
        {
            cmdAdd.Text = cmdAdd.Text + " (" + Program.KeyCode_ItemAdd.ToString() + ")";
            cmdDelete.Text = cmdDelete.Text + " (" + Program.KeyCode_ItemRemove.ToString() + ")";
            cmdUpdate.Text = cmdUpdate.Text + " (" + Program.KeyCode_ItemUpdate.ToString() + ")";
            cmdClear.Text = cmdClear.Text + " (" + Program.KeyCode_RchClear.ToString() + ")";
        }
        #endregion

        #region viewChildren_RowClick
        private void viewChildren_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle <= -1)
            {
                return;
            }

            this.Display_RowDetails(viewChildren.GetDataRow(e.RowHandle));
        }
        #endregion

        #region viewChildren_GotFocus
        private void viewChildren_GotFocus(object sender, EventArgs e)
        {
            if (viewChildren.FocusedRowHandle <= -1)
            {
                return;
            }

            this.Display_RowDetails(viewChildren.GetDataRow(viewChildren.FocusedRowHandle));
        }
        #endregion

        #region Fill_StillBirths
        private void Fill_StillBirths()
        {
            string mFunctionName = "Fill_StillBirths";

            try
            {
                pStillBirths.Clear();
                pDtStillBirths.Rows.Clear();

                DataTable mDtStillBirths = pMdtRCHPostNatalCare.Get_StillBirths(Program.gLanguageName, "grdRCHStillBirths");

                int mFieldIndex = 0;
                foreach (DataRow mDataRow in mDtStillBirths.Rows)
                {
                    pStillBirths.Add(mDataRow["description"].ToString().Trim());

                    DataRow mNewRow = pDtStillBirths.NewRow();
                    mNewRow["description"] = mDataRow["description"];
                    mNewRow["fieldname"] = mDataRow["fieldname"];
                    mNewRow["fieldindex"] = mFieldIndex;
                    pDtStillBirths.Rows.Add(mNewRow);
                    pDtStillBirths.AcceptChanges();

                    mFieldIndex++;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_ChildConditions
        private void Fill_ChildConditions()
        {
            string mFunctionName = "Fill_ChildConditions";

            try
            {
                pChildConditions.Clear();
                pDtChildConditions.Rows.Clear();

                DataTable mDtChildConditions = pMdtRCHPostNatalCare.Get_ChildConditions(Program.gLanguageName, "grdRCHChildConditions");

                int mFieldIndex = 0;
                foreach (DataRow mDataRow in mDtChildConditions.Rows)
                {
                    pChildConditions.Add(mDataRow["description"].ToString().Trim());

                    DataRow mNewRow = pDtChildConditions.NewRow();
                    mNewRow["description"] = mDataRow["description"];
                    mNewRow["fieldname"] = mDataRow["fieldname"];
                    mNewRow["fieldindex"] = mFieldIndex;
                    pDtChildConditions.Rows.Add(mNewRow);
                    pDtChildConditions.AcceptChanges();

                    mFieldIndex++;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Details_Clear
        private void Details_Clear()
        {
            txtDeliveryDate.EditValue = Program.gMdiForm.txtDate.EditValue;
            txtWeight.Text = "";
            txtApgarScore.Text = "";
            cboStillBirths.Text = "";
            cboChildConditions.Text = "";
            txtChildProblems.Text = "";
            radGender.SelectedIndex = -1;
        }
        #endregion

        #region Display_RowDetails
        private void Display_RowDetails(DataRow mSelectedRow)
        {
            txtDeliveryDate.EditValue = Convert.ToDateTime(mSelectedRow["deliverydate"]);
            txtWeight.Text = mSelectedRow["weight"].ToString().Trim();
            txtApgarScore.Text = mSelectedRow["apgarscore"].ToString().Trim();
            txtChildProblems.Text = mSelectedRow["childproblems"].ToString().Trim();

            //gender
            if (mSelectedRow["gender"].ToString().Trim().ToLower() == "m")
            {
                radGender.SelectedIndex = 1;
            }
            else
            {
                radGender.SelectedIndex = 0;
            }

            //still birth
            cboStillBirths.SelectedIndex = -1;
            if (mSelectedRow["stillbirth"].ToString().Trim().ToLower() == AfyaPro_Types.clsEnums.RCHStillBirths.freshbirth.ToString().ToLower())
            {
                cboStillBirths.SelectedIndex = Convert.ToInt16(AfyaPro_Types.clsEnums.RCHStillBirths.freshbirth);
            }
            if (mSelectedRow["stillbirth"].ToString().Trim().ToLower() == AfyaPro_Types.clsEnums.RCHStillBirths.maceratedbirth.ToString().ToLower())
            {
                cboStillBirths.SelectedIndex = Convert.ToInt16(AfyaPro_Types.clsEnums.RCHStillBirths.maceratedbirth);
            }

            //child condition
            cboChildConditions.SelectedIndex = -1;
            if (mSelectedRow["childcondition"].ToString().Trim().ToLower() == AfyaPro_Types.clsEnums.RCHChildConditions.live.ToString().ToLower())
            {
                cboChildConditions.SelectedIndex = Convert.ToInt16(AfyaPro_Types.clsEnums.RCHChildConditions.live);
            }
            if (mSelectedRow["childcondition"].ToString().Trim().ToLower() == AfyaPro_Types.clsEnums.RCHChildConditions.deathbefore24.ToString().ToLower())
            {
                cboChildConditions.SelectedIndex = Convert.ToInt16(AfyaPro_Types.clsEnums.RCHChildConditions.deathbefore24);
            }
            if (mSelectedRow["childcondition"].ToString().Trim().ToLower() == AfyaPro_Types.clsEnums.RCHChildConditions.deathafter24.ToString().ToLower())
            {
                cboChildConditions.SelectedIndex = Convert.ToInt16(AfyaPro_Types.clsEnums.RCHChildConditions.deathafter24);
            }
        }
        #endregion

        #region viewChildren_FocusedRowChanged
        private void viewChildren_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle <= -1)
            {
                return;
            }

            this.Display_RowDetails(viewChildren.GetDataRow(e.FocusedRowHandle));
        }
        #endregion

        #region cmdAdd_Click
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();

            string mFunctionName = "cmdAdd_Click";

            #region validation

            if (Program.IsDate(txtDeliveryDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                txtDeliveryDate.Focus();
                return;
            }

            if (radGender.SelectedIndex == -1)
            {
                Program.Display_Error("Invalid gender");
                radGender.Focus();
                return;
            }

            if (Program.IsMoney(txtWeight.Text) == false)
            {
                Program.Display_Error("Invalid weight");
                txtWeight.Focus();
                return;
            }

            if (Program.IsMoney(txtApgarScore.Text) == false)
            {
                Program.Display_Error("Invalid apgar score");
                txtApgarScore.Focus();
                return;
            }

            if (cboChildConditions.SelectedIndex == -1)
            {
                Program.Display_Error("Invalid discharge condition");
                cboChildConditions.Focus();
                return;
            }

            #endregion

            try
            {
                DateTime mDeliveryDate = Convert.ToDateTime(txtDeliveryDate.EditValue);

                //still birth
                string mStillBirth = "";
                DataView mDvStillBirths = new DataView();
                mDvStillBirths.Table = pDtStillBirths;
                mDvStillBirths.Sort = "fieldindex";

                int mRowIndex = mDvStillBirths.Find(cboStillBirths.SelectedIndex);
                if (mRowIndex >= 0)
                {
                    mStillBirth = mDvStillBirths[mRowIndex]["fieldname"].ToString().Trim();
                }

                //child condition
                string mChildCondition = "";
                DataView mDvChildConditions = new DataView();
                mDvChildConditions.Table = pDtChildConditions;
                mDvChildConditions.Sort = "fieldindex";

                mRowIndex = mDvChildConditions.Find(cboChildConditions.SelectedIndex);
                if (mRowIndex >= 0)
                {
                    mChildCondition = mDvChildConditions[mRowIndex]["fieldname"].ToString().Trim();
                }

                double mWeight = 0;
                if (Program.IsMoney(txtWeight.Text) == true)
                {
                    mWeight = Convert.ToDouble(txtWeight.Text);
                }

                double mApgarScore = 0;
                if (Program.IsMoney(txtApgarScore.Text) == true)
                {
                    mApgarScore = Convert.ToDouble(txtApgarScore.Text);
                }

                string mGender = "F";
                if (radGender.SelectedIndex == 1)
                {
                    mGender = "M";
                }

                DataRow mNewRow = pDtChildren.NewRow();
                mNewRow["deliverydate"] = mDeliveryDate;
                mNewRow["gender"] = mGender;
                mNewRow["weight"] = mWeight;
                mNewRow["apgarscore"] = mApgarScore;
                mNewRow["childproblems"] = txtChildProblems.Text.Trim();
                mNewRow["stillbirth"] = mStillBirth;
                mNewRow["childcondition"] = mChildCondition;
                pDtChildren.Rows.Add(mNewRow);
                pDtChildren.AcceptChanges();

                //refresh
                this.Details_Clear();
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
            string mFunctionName = "cmdUpdate_Click";

            if (viewChildren.FocusedRowHandle <= -1)
            {
                return;
            }

            DataRow mSelectedRow = viewChildren.GetDataRow(viewChildren.FocusedRowHandle);

            #region validation

            if (Program.IsDate(txtDeliveryDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_DateIsInvalid.ToString());
                txtDeliveryDate.Focus();
                return;
            }

            if (radGender.SelectedIndex == -1)
            {
                Program.Display_Error("Invalid gender");
                radGender.Focus();
                return;
            }

            if (Program.IsMoney(txtWeight.Text) == false)
            {
                Program.Display_Error("Invalid weight");
                txtWeight.Focus();
                return;
            }

            if (Program.IsMoney(txtApgarScore.Text) == false)
            {
                Program.Display_Error("Invalid apgar score");
                txtApgarScore.Focus();
                return;
            }

            if (cboChildConditions.SelectedIndex == -1)
            {
                Program.Display_Error("Invalid discharge condition");
                cboChildConditions.Focus();
                return;
            }

            #endregion

            try
            {
                DateTime mDeliveryDate = Convert.ToDateTime(txtDeliveryDate.EditValue);

                //still birth
                string mStillBirth = "";
                DataView mDvStillBirths = new DataView();
                mDvStillBirths.Table = pDtStillBirths;
                mDvStillBirths.Sort = "fieldindex";

                int mRowIndex = mDvStillBirths.Find(cboStillBirths.SelectedIndex);
                if (mRowIndex >= 0)
                {
                    mStillBirth = mDvStillBirths[mRowIndex]["fieldname"].ToString().Trim();
                }

                //child condition
                string mChildCondition = "";
                DataView mDvChildConditions = new DataView();
                mDvChildConditions.Table = pDtChildConditions;
                mDvChildConditions.Sort = "fieldindex";

                mRowIndex = mDvChildConditions.Find(cboChildConditions.SelectedIndex);
                if (mRowIndex >= 0)
                {
                    mChildCondition = mDvChildConditions[mRowIndex]["fieldname"].ToString().Trim();
                }

                double mWeight = 0;
                if (Program.IsMoney(txtWeight.Text) == true)
                {
                    mWeight = Convert.ToDouble(txtWeight.Text);
                }

                double mApgarScore = 0;
                if (Program.IsMoney(txtApgarScore.Text) == true)
                {
                    mApgarScore = Convert.ToDouble(txtApgarScore.Text);
                }

                string mGender = "F";
                if (radGender.SelectedIndex == 1)
                {
                    mGender = "M";
                }

                mSelectedRow.BeginEdit();

                mSelectedRow["deliverydate"] = mDeliveryDate;
                mSelectedRow["gender"] = mGender;
                mSelectedRow["weight"] = mWeight;
                mSelectedRow["apgarscore"] = mApgarScore;
                mSelectedRow["childproblems"] = txtChildProblems.Text.Trim();
                mSelectedRow["stillbirth"] = mStillBirth;
                mSelectedRow["childcondition"] = mChildCondition;

                mSelectedRow.EndEdit();

                //refresh
                this.Details_Clear();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdDelete_Click
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (viewChildren.FocusedRowHandle <= -1)
            {
                return;
            }

            DataRow mSelectedRow = viewChildren.GetDataRow(viewChildren.FocusedRowHandle);

            DialogResult mResp = Program.Confirm_Deletion("'"
                + Convert.ToDateTime(mSelectedRow["deliverydate"]).Date.ToString("d") + "'   '"
                + mSelectedRow["weight"].ToString().Trim() + "'");

            if (mResp != DialogResult.Yes)
            {
                return;
            }

            viewChildren.DeleteSelectedRows();
            pDtChildren.AcceptChanges();
        }
        #endregion

        #region cmdClear_Click
        private void cmdClear_Click(object sender, EventArgs e)
        {
            this.Details_Clear();
            radGender.Focus();
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region frmRCHPostnatalChildren_KeyDown
        void frmRCHPostnatalChildren_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Program.KeyCode_ItemAdd:
                    {
                        this.cmdAdd_Click(cmdAdd, e);
                    }
                    break;
                case Program.KeyCode_ItemRemove:
                    {
                        this.cmdDelete_Click(cmdDelete, e);
                    }
                    break;
                case Program.KeyCode_ItemUpdate:
                    {
                        this.cmdUpdate_Click(cmdUpdate, e);
                    }
                    break;
                case Program.KeyCode_RchClear:
                    {
                        this.cmdClear_Click(cmdClear, e);
                    }
                    break;
            }
        }
        #endregion
    }
}