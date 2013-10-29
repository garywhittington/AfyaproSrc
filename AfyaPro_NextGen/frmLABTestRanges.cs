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
    public partial class frmLABTestRanges : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private Type pType;
        private string pClassName = "";

        private DataTable pDtRanges = new DataTable("labtestranges");

        #endregion

        #region frmLABTestRanges
        public frmLABTestRanges(DataTable mDtRanges)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmLABTestRanges";

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                this.pDtRanges = mDtRanges;
                grdLABTestRanges.DataSource = pDtRanges;

                layoutControl1.AllowCustomizationMenu = false;
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

            mObjectsList.Add(cmdAdd);
            mObjectsList.Add(cmdDelete);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }

        #endregion

        #region frmLABTestRanges_Load
        private void frmLABTestRanges_Load(object sender, EventArgs e)
        {
            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            this.Load_Controls();
            this.Details_Clear();

            Program.Center_Screen(this);
        }
        #endregion

        #region frmLABTestRanges_FormClosing
        private void frmLABTestRanges_FormClosing(object sender, FormClosingEventArgs e)
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

        #region Details_Clear
        private void Details_Clear()
        {
            txtAgeLowerYears.Text = "0";
            txtAgeLowerMonths.Text = "0";
            txtAgeUpperYears.Text = "500";
            txtAgeUpperMonths.Text = "12";
            txtNormalLowerMale.Text = "0";
            txtNormalUpperMale.Text = "0";
            txtNormalLowerFemale.Text = "0";
            txtNormalUpperFemale.Text = "0";
            txtEquipmentLowerMale.Text = "0";
            txtEquipmentUpperMale.Text = "0";
            txtEquipmentLowerFemale.Text = "0";
            txtEquipmentUpperFemale.Text = "0";
        }
        #endregion

        #region cmdAdd_Click
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();

            string mFunctionName = "cmdAdd_Click";

            try
            {
                DataRow mNewRow = pDtRanges.NewRow();
                mNewRow["age_loweryears"] = Convert.ToInt32(txtAgeLowerYears.Text);
                mNewRow["age_lowermonths"] = Convert.ToInt32(txtAgeLowerMonths.Text);
                mNewRow["age_upperyears"] = Convert.ToInt32(txtAgeUpperYears.Text);
                mNewRow["age_uppermonths"] = Convert.ToInt32(txtAgeUpperMonths.Text);
                mNewRow["normal_malelowerrange"] = Convert.ToDouble(txtNormalLowerMale.Text);
                mNewRow["normal_maleupperrange"] = Convert.ToDouble(txtNormalUpperMale.Text);
                mNewRow["normal_femalelowerrange"] = Convert.ToDouble(txtNormalLowerFemale.Text);
                mNewRow["normal_femaleupperrange"] = Convert.ToDouble(txtNormalUpperFemale.Text);
                mNewRow["equipment_malelowerrange"] = Convert.ToDouble(txtEquipmentLowerMale.Text);
                mNewRow["equipment_maleupperrange"] = Convert.ToDouble(txtEquipmentUpperMale.Text);
                mNewRow["equipment_femalelowerrange"] = Convert.ToDouble(txtEquipmentLowerFemale.Text);
                mNewRow["equipment_femaleupperrange"] = Convert.ToDouble(txtEquipmentUpperFemale.Text);
                pDtRanges.Rows.Add(mNewRow);
                pDtRanges.AcceptChanges();

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
            if (viewLABTestRanges.FocusedRowHandle <= -1)
            {
                return;
            }

            DataRow mSelectedRow = viewLABTestRanges.GetDataRow(viewLABTestRanges.FocusedRowHandle);

            DialogResult mResp = Program.Confirm_Deletion("Delete selected range");

            if (mResp != DialogResult.Yes)
            {
                return;
            }

            viewLABTestRanges.DeleteSelectedRows();
            pDtRanges.AcceptChanges();
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            viewLABTestRanges.PostEditor();
            pDtRanges.AcceptChanges();
            this.Close();
        }
        #endregion
    }
}