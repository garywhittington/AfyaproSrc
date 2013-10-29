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
    public partial class frmRCHAntenatalPatientStatus : DevExpress.XtraEditors.XtraForm
    {
        #region Declaration

        private DataTable mDHistory;
        private AfyaPro_MT.clsRCHClients pMdtRCHClients;
        private AfyaPro_MT.clsRCHAnteNatalCare pMdtRCHAnteNatalCare;
        //private AfyaPro_MT.clsRCHAnteNatalCare pMdtRCHClients;
        private string pClassName = "";
        private DataRow mRwFirstVist;
        #endregion

        #region frmRCHAntenatalPatientStatus
        public frmRCHAntenatalPatientStatus(DataTable mHistory)
        {
            InitializeComponent();
            mDHistory = mHistory;
            pMdtRCHClients = (AfyaPro_MT.clsRCHClients)Activator.GetObject(
                  typeof(AfyaPro_MT.clsRCHClients),
                  Program.gMiddleTier + "clsRCHClients");

            pMdtRCHAnteNatalCare = (AfyaPro_MT.clsRCHAnteNatalCare)Activator.GetObject(
                typeof(AfyaPro_MT.clsRCHAnteNatalCare),
                Program.gMiddleTier + "clsRCHAnteNatalCare");

            layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                   AfyaPro_Types.clsSystemFunctions.FunctionKeys.rchantenatal_customizelayout.ToString());
          
        }
        #endregion

        
 

        #region btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        

        #region Load_Form
        private void frmRCHAntenatalVisitDetail_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmRCHAntenatalVisitDetail_Load";

            try
            {
                txtTotalVists.Text = Convert.ToString(mDHistory.Rows.Count.ToString());
                Int64 mAutocode = 9999999999999999;
                int mTTVDoses = 0;
                int mSP = 0;
                int mFefoTablets = 0;
                int mAlbendazole = 0;
                string mNetGiven = "";
                string mSyphilis = "";
                string mPreviousHIV = "";
                string mNewHIV = "";

                string mONCPT = "";
                string mONART = "";
                string mNVP = "";
                foreach (DataRow myRow in mDHistory.Rows)
                {

                    if (Convert.ToUInt16(myRow["autocode"]) < mAutocode)
                    {
                        mAutocode = Convert.ToUInt16(myRow["autocode"]);
                        mRwFirstVist = myRow;
                    }

                    if ((myRow["ttvpreviousdosses"].ToString().Trim() != "0" && myRow["ttvpreviousdosses"].ToString() != "") || (myRow["ttvnewdosses"].ToString() != "0" && myRow["ttvnewdosses"].ToString() != ""))
                    {
                        mTTVDoses += 1;
                    }

                    if (myRow["sp"].ToString().Trim() != "0" && myRow["sp"].ToString() != "")
                    {
                        mSP += 1;
                    }
                    if (myRow["fefo"].ToString().Trim() != "0" && myRow["fefo"].ToString() != "")
                    {
                        mFefoTablets += Convert.ToInt32(myRow["fefo"]);
                    }

                    if (myRow["albendazole"].ToString().Trim() != "0" && myRow["albendazole"].ToString() != "")
                    {
                        mAlbendazole += 1;
                    }

                    if (myRow["bednetgiven"].ToString().Trim() != "0" && myRow["bednetgiven"].ToString() != "")
                    {
                        mNetGiven = myRow["bednetgiven"].ToString();
                    }

                    if (myRow["syphilistest"].ToString().Trim() != "0" && myRow["syphilistest"].ToString() != "")
                    {
                        mSyphilis = myRow["syphilistest"].ToString();
                    }

                    if (myRow["previoushivtestresult"].ToString().Trim() != "0" && myRow["previoushivtestresult"].ToString() != "")
                    {
                        mPreviousHIV = myRow["previoushivtestresult"].ToString();
                    }

                    if (myRow["newhivtestresult"].ToString().Trim() != "0" && myRow["newhivtestresult"].ToString() != "")
                    {
                        mNewHIV = myRow["newhivtestresult"].ToString();
                    }

                    if (myRow["oncpt"].ToString().Trim() != "0" && myRow["oncpt"].ToString() != "")
                    {
                        mONCPT = myRow["oncpt"].ToString();
                    }

                    if (myRow["nvpsyrupdispensed"].ToString().Trim() != "0" && myRow["nvpsyrupdispensed"].ToString() != "")
                    {
                        mNVP = "Yes";
                    }
                    else
                    {
                        mNVP = "No";
                    }

                    if (myRow["onart"].ToString().Trim() != "0" && myRow["onart"].ToString() != "")
                    {
                        mONART = myRow["onart"].ToString();
                    }


                }

                txtWeekOfFirstVisit.Text = mRwFirstVist["pregage"].ToString();
                txtTTVdoses.Text = mTTVDoses.ToString();
                txtSpdoses.Text = mSP.ToString();
                txtFefoTablets.Text = mFefoTablets.ToString();
                txtAlbendazole.Text = mAlbendazole.ToString();
                txtITN.Text = mNetGiven.ToString();
                txtSyphilis.Text = mSyphilis.ToString();
                txtPreviousHIV.Text = mPreviousHIV.ToString();
                txtNewHIV.Text = mNewHIV.ToString();
                txtOnCPT.Text = mONCPT.ToString();
                txtNVP.Text = mNVP.ToString();
                txtOnART.Text = mONART.ToString();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion
    }
}