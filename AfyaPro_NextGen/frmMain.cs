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
using System.IO;
using System.Xml;

namespace AfyaPro_NextGen
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Type pType;
        private string pClassName = "";

        //private DevExpress.XtraEditors.DateEdit pDateEdit = new DevExpress.XtraEditors.DateEdit();

        #region frmMain
        public frmMain()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmMain";

            try
            {
                navBar.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(navBar_LinkClicked);
                //administrativetoolscontainer.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(navBar_LinkClicked);
                //reportscontainer.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(reportscontainer_LinkClicked);
                cboSkinsEditor.SelectedIndexChanged += new EventHandler(cboSkinsEditor_SelectedIndexChanged);
                if (Program.gMainNavBarWidth != 0)
                {
                    navBar.Width = Program.gMainNavBarWidth;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmMain_Load
        private void frmMain_Load(object sender, EventArgs e)
        {
            Program.gMainWindowCodes.Fill_Modules(navBar);
            txtDate.EditValue = Program.gServerDate;
            Program.gDateChangeAtStartup = false;

            this.Load_Controls();

            this.ribbonBar.ApplicationCaption = Program.gApplicationName + " <" + Program.gCurrentUser.Description + ">";
            this.Text = Program.gMdiForm.ribbonBar.ApplicationCaption;
            txtDate.Caption = txtDate.Caption + " (" + Program.gCulture.DateTimeFormat.ShortDatePattern + ")";

            //fill skins
            int mSkinCount = 0;
            foreach (DevExpress.Skins.SkinContainer mSkinContainer in DevExpress.Skins.SkinManager.Default.Skins)
            {
                cboSkinsEditor.Items.Add(mSkinContainer.SkinName);

                if (mSkinContainer.SkinName.Trim().ToLower() == Program.gDefaultSkinName.Trim().ToLower())
                {
                    cboSkins.EditValue = mSkinContainer.SkinName;
                }

                mSkinCount++;
            }

            this.HelpButton = true;
        }
        #endregion

        #region cboSkinsEditor_SelectedIndexChanged
        void cboSkinsEditor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.ComboBoxEdit mComboBoxEdit = sender as DevExpress.XtraEditors.ComboBoxEdit;
            string mSkinName = mComboBoxEdit.Text;
            cboSkins.EditValue = mSkinName;
            Program.gDefaultSkinName = mSkinName;
            Program.gDefaultLookAndFeel.LookAndFeel.SkinName = Program.gDefaultSkinName;
        }
        #endregion

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            //other objects
            mObjectsList.Add(pagToolBar);
            mObjectsList.Add(txtDate);
            mObjectsList.Add(cmdNew);
            mObjectsList.Add(cmdEdit);
            mObjectsList.Add(cmdDelete);
            mObjectsList.Add(cmdRefresh);
            Program.Apply_Language(this.Name, mObjectsList);

            ////modules
            //List<object> mModuleObjectsList = new List<object>();
            //mModuleObjectsList.Add(outpatientdept);
            //mModuleObjectsList.Add(inpatientdept);
            //mModuleObjectsList.Add(customers);
            //mModuleObjectsList.Add(billing);
            //mModuleObjectsList.Add(administrativetools);
            //mModuleObjectsList.Add(generalsetup);
            //mModuleObjectsList.Add(billingsetup);
            //mModuleObjectsList.Add(inventorysetup);
            //mModuleObjectsList.Add(securitysetup);

            DataView mDvModules = new DataView();
            mDvModules.Table = Program.gDtModules;
            mDvModules.Sort = "modulekey";

            //for (Int32 mRow = 0; mRow < mModuleObjectsList.Count; mRow++)
            //{
            //    DevExpress.XtraNavBar.NavBarGroup mNavBarGroup =
            //        (DevExpress.XtraNavBar.NavBarGroup)mModuleObjectsList[mRow];

            //    int mRowIndex = mDvModules.Find(mNavBarGroup.Name.Trim());
            //    if (mRowIndex >= 0)
            //    {
            //        mNavBarGroup.Caption = mDvModules[mRowIndex]["moduletext"].ToString().Trim();
            //    }
            //}
        }
        #endregion

        #region navBar_LinkClicked
        private void navBar_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Program.gMainWindowCodes.mNavBar_LinkClicked(e.Link);
        }
        #endregion

        #region frmMain_FormClosing
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            string mFunctionName = "frmMain_FormClosing";

            try
            {
                DialogResult mResult = Program.Display_Question("Exit Application", MessageBoxDefaultButton.Button2);
                if (mResult != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                Program.Save_UserSettings(navBar.Width);

                Program.Update_UserLogin();

                foreach (DevExpress.XtraEditors.XtraForm mForm in this.MdiChildren)
                {
                    mForm.Close();
                }

                //Program.gMdtFacilitySetup.Initialize_English(Program.gDtEnglish);

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region toolbar command events

        private void cmdNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            clsMainWindowCodes.New_Clicked();
        }

        private void cmdEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            clsMainWindowCodes.Edit_Clicked();
        }

        private void cmdDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            clsMainWindowCodes.Delete_Clicked();
        }

        private void cmdRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            clsMainWindowCodes.Refresh_Clicked();
        }

        #endregion

        #region cmdActivate_ItemClick
        private void cmdActivate_ItemClick(object sender, ItemClickEventArgs e)
        {
            string mFunctionName = "Get_Period";

            try
            {
                AfyaPro_MT.clsUsers mMdtUsers = (AfyaPro_MT.clsUsers)Activator.GetObject(
                        typeof(AfyaPro_MT.clsUsers),
                        Program.gMiddleTier + "clsUsers");

                AfyaPro_Types.clsLicence mLicence = mMdtUsers.Check_Licence(DateTime.Now);
                frmRCHAntenatalVisitDetails1 PassDialog = new frmRCHAntenatalVisitDetails1(mLicence.BaseString,
                                                            mLicence.TrialDays,
                                                            mLicence.InfoText,
                                                            mLicence.FullyActive);
                PassDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region txtDate_EditValueChanged
        private void txtDate_EditValueChanged(object sender, EventArgs e)
        {
            if (Program.gDateChangeAtStartup == false)
            {
                AfyaPro_MT.clsUsers mMdtUsers = (AfyaPro_MT.clsUsers)Activator.GetObject(
                                    typeof(AfyaPro_MT.clsUsers),
                                    Program.gMiddleTier + "clsUsers");

                AfyaPro_Types.clsLicence mLicence = mMdtUsers.Check_Licence(Convert.ToDateTime(txtDate.EditValue).Date);
                if (mLicence.FullyActive == false)
                {
                    frmRCHAntenatalVisitDetails1 PassDialog = new frmRCHAntenatalVisitDetails1(mLicence.BaseString,
                                                                mLicence.TrialDays,
                                                                mLicence.InfoText,
                                                                mLicence.FullyActive);

                    DialogResult DR = PassDialog.ShowDialog();
                    Program.RunTypes mRunType;

                    if (DR == System.Windows.Forms.DialogResult.OK)
                    {
                        mRunType = Program.RunTypes.Full;
                    }
                    else if (DR == DialogResult.Retry)
                    {
                        mRunType = Program.RunTypes.Trial;
                    }
                    else
                    {
                        mRunType = Program.RunTypes.Expired;
                    }

                    if (mRunType != Program.RunTypes.Full && mRunType != Program.RunTypes.Trial)
                    {
                        System.Environment.Exit(1);
                    }
                }
            }
        }
        #endregion

        #region txtDate_ShowingEditor
        private void txtDate_ShowingEditor(object sender, ItemCancelEventArgs e)
        {
            if (Program.gCurrentUser.AllowChangingTransDate == 0)
            {
                frmSECConfirmUser mSECConfirmUser = new frmSECConfirmUser();
                mSECConfirmUser.ShowDialog();

                e.Cancel = !mSECConfirmUser.CanChangeDate;
            }
        }
        #endregion
    }
}