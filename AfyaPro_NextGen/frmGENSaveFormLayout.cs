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
using DevExpress.XtraEditors.Controls;

namespace AfyaPro_NextGen
{
    public partial class frmGENSaveFormLayout : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsFacilitySetup pMdtFacilitySetup;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        internal string gDataState = "";

        private ComboBoxItemCollection pFormLayouts;

        private string pFormName = "";
        //internal string FormName
        //{
        //    get { return pFormName; }
        //    set { pFormName = value; }
        //}

        private byte[] pLayoutBytes;
        //internal byte[] FormLayoutBytes
        //{
        //    get { return pLayoutBytes; }
        //    set { pLayoutBytes = value; }
        //}

        #endregion

        private DevExpress.XtraEditors.XtraForm pXtraForm = null;

        #region frmGENSaveFormLayout
        public frmGENSaveFormLayout(DevExpress.XtraEditors.XtraForm mXtraForm, string mFormName, byte[] mLayoutBytes)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmGENCountries";

            try
            {
                this.pFormName = mFormName;
                this.pXtraForm = mXtraForm;
                this.pLayoutBytes = mLayoutBytes;

                this.Icon = Program.gMdiForm.Icon;
                this.CancelButton = cmdClose;

                pMdtFacilitySetup = (AfyaPro_MT.clsFacilitySetup)Activator.GetObject(
                    typeof(AfyaPro_MT.clsFacilitySetup),
                    Program.gMiddleTier + "clsFacilitySetup");

                pFormLayouts = cboLayoutTemplate.Properties.Items;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmGENSaveFormLayout_Load
        private void frmGENSaveFormLayout_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmGENSaveFormLayout_Load";

            try
            {
                DataTable mDtFormLayouts = pMdtFacilitySetup.Get_DefinedFormLayouts();

                pFormLayouts.Clear();
                foreach (DataRow mDataRow in mDtFormLayouts.Rows)
                {
                    pFormLayouts.Add(mDataRow["description"].ToString());
                }

                cboLayoutTemplate.Text = Program.gCurrentUser.UserGroupFormsLayoutTemplateName;
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
            string mFunctionName = "cmdOk_Click";

            try
            {
                if (cboLayoutTemplate.Text.Trim() == "")
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GEN_LayoutTemplateNameIsInvalid.ToString());
                    cboLayoutTemplate.Focus();
                    return;
                }

                DataView mDvFormSizes = new DataView();
                mDvFormSizes.Table = Program.gDtFormSizes;
                mDvFormSizes.Sort = "formname";

                int mRowIndex = mDvFormSizes.Find(pFormName);
                if (mRowIndex >= 0)
                {
                    mDvFormSizes.BeginInit();

                    mDvFormSizes[mRowIndex]["formwidth"] = pXtraForm.Width;
                    mDvFormSizes[mRowIndex]["formheight"] = pXtraForm.Height;

                    mDvFormSizes.EndInit();
                    Program.gDtFormSizes.AcceptChanges();
                }
                else
                {
                    DataRow mNewRow = Program.gDtFormSizes.NewRow();
                    mNewRow["formname"] = pFormName;
                    mNewRow["formwidth"] = pXtraForm.Width;
                    mNewRow["formheight"] = pXtraForm.Height;
                    Program.gDtFormSizes.Rows.Add(mNewRow);
                    Program.gDtFormSizes.AcceptChanges();
                }

                pMdtFacilitySetup.Save_FormLayout(pFormName, cboLayoutTemplate.Text, pLayoutBytes, pXtraForm.Height, pXtraForm.Width);
                this.Close();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
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