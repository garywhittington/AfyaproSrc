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
    public partial class frmBLSPriceCategories : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsPriceCategories pMdtPriceCategories;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        #endregion

        #region frmBLSPriceCategories
        public frmBLSPriceCategories()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmBLSPriceCategories";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                
                this.CancelButton = cmdClose;

                pMdtPriceCategories = (AfyaPro_MT.clsPriceCategories)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPriceCategories),
                    Program.gMiddleTier + "clsPriceCategories");
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmBLSPriceCategories_Load
        private void frmBLSPriceCategories_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmBLSPriceCategories";

            try
            {
                DataTable mDtPriceCategories = pMdtPriceCategories.View("", "");

                if (mDtPriceCategories.Rows.Count > 0)
                {
                    chkPrice1.Checked = Convert.ToBoolean(mDtPriceCategories.Rows[0]["useprice1"]);
                    chkPrice2.Checked = Convert.ToBoolean(mDtPriceCategories.Rows[0]["useprice2"]);
                    chkPrice3.Checked = Convert.ToBoolean(mDtPriceCategories.Rows[0]["useprice3"]);
                    chkPrice4.Checked = Convert.ToBoolean(mDtPriceCategories.Rows[0]["useprice4"]);
                    chkPrice5.Checked = Convert.ToBoolean(mDtPriceCategories.Rows[0]["useprice5"]);
                    chkPrice6.Checked = Convert.ToBoolean(mDtPriceCategories.Rows[0]["useprice6"]);
                    chkPrice7.Checked = Convert.ToBoolean(mDtPriceCategories.Rows[0]["useprice7"]);
                    chkPrice8.Checked = Convert.ToBoolean(mDtPriceCategories.Rows[0]["useprice8"]);
                    chkPrice9.Checked = Convert.ToBoolean(mDtPriceCategories.Rows[0]["useprice9"]);
                    chkPrice10.Checked = Convert.ToBoolean(mDtPriceCategories.Rows[0]["useprice10"]);

                    txtPrice1.Text = mDtPriceCategories.Rows[0]["price1"].ToString().Trim();
                    txtPrice2.Text = mDtPriceCategories.Rows[0]["price2"].ToString().Trim();
                    txtPrice3.Text = mDtPriceCategories.Rows[0]["price3"].ToString().Trim();
                    txtPrice4.Text = mDtPriceCategories.Rows[0]["price4"].ToString().Trim();
                    txtPrice5.Text = mDtPriceCategories.Rows[0]["price5"].ToString().Trim();
                    txtPrice6.Text = mDtPriceCategories.Rows[0]["price6"].ToString().Trim();
                    txtPrice7.Text = mDtPriceCategories.Rows[0]["price7"].ToString().Trim();
                    txtPrice8.Text = mDtPriceCategories.Rows[0]["price8"].ToString().Trim();
                    txtPrice9.Text = mDtPriceCategories.Rows[0]["price9"].ToString().Trim();
                    txtPrice10.Text = mDtPriceCategories.Rows[0]["price10"].ToString().Trim();
                }

                this.Load_Controls();
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

            mObjectsList.Add(chkPrice1);
            mObjectsList.Add(chkPrice2);
            mObjectsList.Add(chkPrice3);
            mObjectsList.Add(chkPrice4);
            mObjectsList.Add(chkPrice5);
            mObjectsList.Add(chkPrice6);
            mObjectsList.Add(chkPrice7);
            mObjectsList.Add(chkPrice8);
            mObjectsList.Add(chkPrice9);
            mObjectsList.Add(chkPrice10);
            mObjectsList.Add(cmdOk);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region Data_Edit
        private void Data_Edit()
        {
            String mFunctionName = "Data_Edit";

            try
            {
                //edit 
                pResult = pMdtPriceCategories.Edit(
                    Convert.ToInt16(chkPrice1.Checked),
                    Convert.ToInt16(chkPrice2.Checked),
                    Convert.ToInt16(chkPrice3.Checked),
                    Convert.ToInt16(chkPrice4.Checked),
                    Convert.ToInt16(chkPrice5.Checked),
                    Convert.ToInt16(chkPrice6.Checked),
                    Convert.ToInt16(chkPrice7.Checked),
                    Convert.ToInt16(chkPrice8.Checked),
                    Convert.ToInt16(chkPrice9.Checked),
                    Convert.ToInt16(chkPrice10.Checked),
                    txtPrice1.Text, txtPrice2.Text, txtPrice3.Text, txtPrice4.Text, txtPrice5.Text,
                    txtPrice6.Text, txtPrice7.Text, txtPrice8.Text, txtPrice9.Text,
                    txtPrice10.Text, Program.gCurrentUser.Code);
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

                Program.Display_Info("Settings saved successfully");
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