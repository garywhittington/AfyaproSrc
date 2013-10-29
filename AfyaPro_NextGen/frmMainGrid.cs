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
    public partial class frmMainGrid : DevExpress.XtraEditors.XtraForm
    {
        public frmMainGrid()
        {
            InitializeComponent();
        }

        private void frmMainGrid_Load(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                (DevExpress.XtraGrid.Views.Grid.GridView)grdMain.MainView;

            grdMain.ForceInitialize();
            Program.Restore_GridLayout(grdMain, grdMain.Name);
        }

        private void frmMainGrid_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                MemoryStream mMemoryStream = new MemoryStream();
                grdMain.MainView.SaveLayoutToStream(mMemoryStream);

                Program.gMdtFacilitySetup.Save_GridLayout(grdMain.Name, Program.gCurrentUser.Code, mMemoryStream.ToArray());
            }
            catch { }
        }

        private void grdMain_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)grdMain.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                clsMainWindowCodes.Edit_Clicked();
            }
            catch
            {
                return;
            }
        }

        private void grdMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                        (DevExpress.XtraGrid.Views.Grid.GridView)grdMain.MainView;

                    if (mGridView.FocusedRowHandle < 0)
                    {
                        return;
                    }

                    clsMainWindowCodes.Delete_Clicked();
                }
                catch
                {
                    return;
                }
            }
        }
    }
}