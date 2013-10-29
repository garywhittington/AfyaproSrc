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
using System.Xml;

namespace AfyaPro_NextGen
{
    public partial class frmMessageTemplates : DevExpress.XtraEditors.XtraForm
    {
        private AfyaPro_MT.clsSMS pMdtSMS;
        internal static DataGridViewRow pRow = new DataGridViewRow();
        public frmMessageTemplates()
        {
            pMdtSMS = (AfyaPro_MT.clsSMS)Activator.GetObject(
                   typeof(AfyaPro_MT.clsSMS),
                   Program.gMiddleTier + "clsSMS");
            InitializeComponent();
        }

        private void Data_Delete(DataGridViewRow mRow)
        {

        }

        private void frmMessageTemplates_FormClosing(object sender, FormClosingEventArgs e)
        {
            //try
            //{
            //    MemoryStream mMemoryStream = new MemoryStream();
            //    //grdMain.MainView.SaveLayoutToStream(mMemoryStream);

            //    Program.gMdtFacilitySetup.Save_GridLayout(grdMain.Name, Program.gCurrentUser.Code, mMemoryStream.ToArray());
            //}
            //catch { }
        }

        private void grdMain_DoubleClick(object sender, EventArgs e)
        {
            //try
            //{
            //    DevExpress.XtraGrid.Views.Grid.GridView mGridView =
            //        //(DevExpress.XtraGrid.Views.Grid.GridView)grdMain.MainView;

            //    if (mGridView.FocusedRowHandle < 0)
            //    {
            //        return;
            //    }

            //    clsMainWindowCodes.Edit_Clicked();
            //}
            //catch
            //{
            //    return;
            //}
        }

        private void grdMain_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Delete)
            //{
            //    try
            //    {
            //        DevExpress.XtraGrid.Views.Grid.GridView mGridView =
            //            (DevExpress.XtraGrid.Views.Grid.GridView)grdMain.MainView;

            //        if (mGridView.FocusedRowHandle < 0)
            //        {
            //            return;
            //        }

            //        clsMainWindowCodes.Delete_Clicked();
            //    }
            //    catch
            //    {
            //        return;
            //    }
            //}
        }

        private void frmMessageTemplates_Load(object sender, EventArgs e)
        {
            string mFunctionName = "Data_Fill";

            try
            {
                //load data

                DataTable mDtTemplates = new DataTable();
               mDtTemplates = pMdtSMS.View_MessageTemplates("", "", Program.gLanguageName, grdMain.Name, true);

               foreach (DataColumn mCol in mDtTemplates.Columns)
               {
                   if (mCol.ColumnName == "messagecode")
                   {
                       
                       mDtTemplates.Columns["messagecode"].ColumnName = "Message Code";
                      
                   }
                   if (mCol.ColumnName == "message")
                   {
                       mDtTemplates.Columns["message"].ColumnName = "Message";
                       
                   }

                   if (mCol.ColumnName == "clients")
                   {
                       mDtTemplates.Columns["clients"].ColumnName = "Target Audience";
                       
                   }

                   if (mCol.ColumnName == "createdon")
                   {
                       mDtTemplates.Columns["createdon"].ColumnName = "Date of Creation";
                       
                   }

                   if (mCol.ColumnName == "createdby")
                   {
                       mDtTemplates.Columns["createdby"].ColumnName = "Author";
                       
                   }
                   
               }

               mDtTemplates.AcceptChanges();
               grdMain.DataSource = mDtTemplates;
            
            }
            catch (Exception ex)
            {
                Program.Display_Error(ex.Message, mFunctionName, ex.Message);
                return;
            }
           
        }

        private void grdMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                frmSMSMessageTemplates mSmsT = new frmSMSMessageTemplates(grdMain.SelectedRows[0], "Edit");
                mSmsT.ShowDialog();
            }
            catch
            {
                return;
            }
        }

        private void grdMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                pRow = grdMain.SelectedRows[0];



            }
            catch
            {
                return;
            }
        }

       
    }
}