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
using System.IO;

namespace AfyaPro_NextGen
{
    public partial class frmRCHAntenatalVisitDetail : DevExpress.XtraEditors.XtraForm
    {
        #region Declaration
        private string pClassName = "";
        private string pClientId = "";
        private string pAutocode = "";
        private Boolean mEditig = false;
        //private string pFileContents = "";
        private AfyaPro_MT.clsRCHClients pMdtRCHClients;
        private AfyaPro_MT.clsRCHAnteNatalCare pMdtRCHAnteNatalCare;
        //private AfyaPro_MT.clsRCHAnteNatalCare pMdtRCHClients;
        #endregion

        #region frmRCHAntenatalVisitDetail
        public frmRCHAntenatalVisitDetail(string mClientCode, Boolean isEditing, DataRow mSelectedRow, string mAutocode)
        {
            InitializeComponent();
            pClientId = mClientCode;
            pMdtRCHClients = (AfyaPro_MT.clsRCHClients)Activator.GetObject(
                  typeof(AfyaPro_MT.clsRCHClients),
                  Program.gMiddleTier + "clsRCHClients");

            pMdtRCHAnteNatalCare = (AfyaPro_MT.clsRCHAnteNatalCare)Activator.GetObject(
                typeof(AfyaPro_MT.clsRCHAnteNatalCare),
                Program.gMiddleTier + "clsRCHAnteNatalCare");

            layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                   AfyaPro_Types.clsSystemFunctions.FunctionKeys.rchantenatal_customizelayout.ToString());
            mEditig = isEditing;
            if (isEditing == true)
            {

             
                if (mSelectedRow["visitdate"].ToString() != "")
                {
                    txtVisitDate.EditValue = Convert.ToDateTime(mSelectedRow["visitdate"]);
                }
                //pAutocode = mSelectedRow["autocode"].ToString();
                txtAlbendazole.Text = mSelectedRow["albendazole"].ToString();
                txtBedNet.Text = mSelectedRow["bednetgiven"].ToString();
                txtBP.Text = mSelectedRow["bp"].ToString();
                txtComment.Text = mSelectedRow["comment"].ToString();
                txtFefo.Text = mSelectedRow["fefo"].ToString();
                txtFetalHeart.Text = mSelectedRow["fetalheart"].ToString();
                txtGestationWeeks.Text = mSelectedRow["pregage"].ToString();
                txtHB.Text = mSelectedRow["hb"].ToString();
                txtHIVTestNew.Text = mSelectedRow["newhivtestresult"].ToString();
                txtHIVTestPrevious.Text = mSelectedRow["previoushivtestresult"].ToString();
                txtNVPSyrup.Text = mSelectedRow["nvpsyrupdispensed"].ToString();
                txtOnART.Text = mSelectedRow["onart"].ToString();
                txtOnCPT.Text = mSelectedRow["oncpt"].ToString();

                txtProvider.Text = mSelectedRow["providername"].ToString();
                txtSP.Text = mSelectedRow["sp"].ToString();
                txtSyphilis.Text = mSelectedRow["syphilistest"].ToString();
                txtTTVNew.Text = mSelectedRow["ttvnewdosses"].ToString();
                txtTTVPrevious.Text = mSelectedRow["ttvpreviousdosses"].ToString();
                txtUrineProtein.Text = mSelectedRow["urineprotein"].ToString();
               // txtVisitDate.Text = mSelectedRow["visitdate"].ToString();
                txtWeight.Text = mSelectedRow["weight"].ToString();
                pAutocode =mSelectedRow["autocode"].ToString();
            }
        }
        #endregion

        #region Ok
        private void Ok()
        {

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            string mFunctionName = "Save";

                     
            if (txtVisitDate.Text == "")
            {
                Program.Display_Error("Please enter visit date", false);
                txtVisitDate.Focus();
                return;
            }

            try
            {
                DateTime? mVisitDate = null;

              
                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);
                mVisitDate = Convert.ToDateTime(txtVisitDate.Text);
               // MessageBox.Show("Hey");
                mResult = pMdtRCHAnteNatalCare.Add_Visit(
                    Program.gCurrentUser.Code,
                    pClientId, 
                    mTransDate,
                    Convert.ToDateTime(mVisitDate),
                    txtGestationWeeks.Text,
                    txtFetalHeart.Text, 
                    txtWeight.Text,
                    txtSyphilis.Text,
                    txtBP.Text,
                    txtUrineProtein.Text,
                    txtTTVPrevious.Text,
                    txtTTVNew.Text, 
                    txtSP.Text,
                    txtFefo.Text,
                    txtAlbendazole.Text,
                    txtBedNet.Text, 
                    txtHB.Text,
                    txtHIVTestPrevious.Text,
                    txtHIVTestNew.Text,
                    txtOnCPT.Text,
                    txtNVPSyrup.Text,
                    txtOnART.Text,
                    txtComment.Text,
                    txtProvider.Text,
                    mEditig,
                    pAutocode);

            
                if (mResult.Exe_Result == 0)
                {
                   
                    Program.Display_Error(mResult.Exe_Message);
                    return;
                }
                if (mResult.Exe_Result == -1)
                {
                  
                    Program.Display_Server_Error(mResult.Exe_Message);
                    return;
                }

                Program.Display_Info("Patient visit saved successfully");

                this.Clear_Data();
                 
            }
            catch (Exception ex)
            {
               // MessageBox.Show(Convert.ToString(mResult));
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Clear_Data

        private void  Clear_Data()
        {
            txtAlbendazole.ResetText();
            txtBedNet.ResetText();
            txtBP.ResetText();
            txtComment.ResetText();
            txtFefo.ResetText();
            txtFetalHeart.ResetText();
            txtGestationWeeks.ResetText();
            txtHB.ResetText();
            txtHIVTestNew.ResetText();
            txtHIVTestPrevious.ResetText();
            txtNVPSyrup.ResetText();
            txtOnART.ResetText();
            txtOnCPT.ResetText();
            txtOnCPT.ResetText();
            txtProvider.ResetText();
            txtSP.ResetText();
            txtSyphilis.ResetText();
            txtTTVNew.ResetText();
            txtTTVPrevious.ResetText();
            txtUrineProtein.ResetText();
            txtVisitDate.ResetText();
            txtWeight.ResetText();

        }


        #endregion

        #region btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region btnSave_Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Ok();
        }
        #endregion

        #region Load_Form
        private void frmRCHAntenatalVisitDetail_Load(object sender, EventArgs e)
        {

        }
        #endregion
    }
}