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
using Ionic.Zip;
using System.IO;

namespace AfyaPro_NextGen
{
    public partial class frmMTUDHISExport : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsMtuhaDiagnoses pMdtMtuhaDiagnoses;

        private Type pType;
        private string pClassName = "";

        #endregion

        #region frmMTUDHISExport
        public frmMTUDHISExport()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmMTUDHISExport";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                this.CancelButton = cmdClose;

                pMdtMtuhaDiagnoses = (AfyaPro_MT.clsMtuhaDiagnoses)Activator.GetObject(
                    typeof(AfyaPro_MT.clsMtuhaDiagnoses),
                    Program.gMiddleTier + "clsMtuhaDiagnoses");
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmMTUDHISExport_Load
        private void frmMTUDHISExport_Load(object sender, EventArgs e)
        {
            DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

            txtDateFrom.EditValue = new DateTime(mTransDate.Year, mTransDate.Month, 1);
            txtDateTo.EditValue = new DateTime(mTransDate.Year, mTransDate.Month, 1).AddMonths(1).AddDays(-1);
        }
        #endregion

        #region cmdBrowse_Click
        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            //saveFileDialog1.Filter = "XML Files (*.xml)|*.xml";
            //saveFileDialog1.FilterIndex = 2;
            //saveFileDialog1.RestoreDirectory = true;
            //saveFileDialog1.FileName = "Export";

            FolderBrowserDialog fdlg = new FolderBrowserDialog();
            fdlg.Description = "select saving location";
            fdlg.ShowNewFolderButton = true;

            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = fdlg.SelectedPath;
            }
        }
        #endregion

        #region cmdOk_Click
        private void cmdOk_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdOk_Click";

            try
            {
                if (txtFileName.Text.Trim() == "")
                {
                    Program.Display_Error("Invalid file name");
                    txtFileName.Focus();
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                byte[] mData = pMdtMtuhaDiagnoses.Export_To_DHIS(
                    Convert.ToDateTime(txtDateFrom.EditValue), Convert.ToDateTime(txtDateTo.EditValue));

                if (mData == null)
                {
                    this.Cursor = Cursors.Default;
                    Program.Display_Error("Error occured when fetching data for export");
                    return;
                }

                MemoryStream mMemoryStream = new MemoryStream(mData);

                ZipFile zFile = new ZipFile();
                zFile.AddFileStream("Export.xml", "", mMemoryStream);
                zFile.Save(txtFileName.Text + "\\Export" + FormatDate(Convert.ToDateTime(txtDateFrom.EditValue)) 
                    + " to " + FormatDate(Convert.ToDateTime(txtDateTo.EditValue)) + ".zip");

                this.Cursor = Cursors.Default;
                Program.Display_Info("Export zip file has been created and saved to location: " 
                    + Environment.NewLine + txtFileName.Text);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region FormatDate
        private string FormatDate(DateTime tDate)
        {
            return tDate.Year + "-" + tDate.Month + "-" + tDate.Day;
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