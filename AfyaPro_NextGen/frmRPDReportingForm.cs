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
using DevExpress.XtraLayout;
using DevExpress.XtraEditors.Controls;
using System.IO;

namespace AfyaPro_NextGen
{
    public partial class frmRPDReportingForm : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsReports pMdtReports;
        private AfyaPro_MT.clsReporter pMdtReporter;

        private Type pType;
        private string pClassName = "";

        private string pReportCode = "";
        private DataTable pDtParameters;
        private string pCommandText;
        private string pFilterString;

        private int pFormWidth = 0;
        private int pFormHeight = 0;
        private bool pSaveReportingForm = false;

        #endregion

        #region frmRPDReportingForm
        public frmRPDReportingForm(string mReportCode, bool mSaveReportingForm)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmRPDReportingForm";

            try
            {
                pReportCode = mReportCode;
                pSaveReportingForm = mSaveReportingForm;

                pMdtReports = (AfyaPro_MT.clsReports)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReports),
                    Program.gMiddleTier + "clsReports");

                pMdtReporter = (AfyaPro_MT.clsReporter)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReporter),
                    Program.gMiddleTier + "clsReporter");

                this.Get_ReportSettings();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmRPDReportingForm_Load
        private void frmRPDReportingForm_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmOPDRegistrations_Load";

            try
            {
                this.Get_ReportingControls();

                byte[] mByte = pMdtReporter.Load_ReportTemplate("frmReporter" + pReportCode, true);
                if (mByte != null)
                {
                    MemoryStream mMemoryStream = new MemoryStream(mByte);
                    layoutControl1.RestoreLayoutFromStream(mMemoryStream);
                    mMemoryStream.Close();
                }

                Program.Restore_FormSize("frmReporter" + pReportCode, this, true);

                this.pFormWidth = this.Width;
                this.pFormHeight = this.Height;

                Program.Center_Screen(this);
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmRPDReportingForm_FormClosing
        private void frmRPDReportingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string mFunctionName = "frmRPDReportingForm_FormClosing";

            try
            {
                //layout
                if (layoutControl1.IsModified == true || pSaveReportingForm == true)
                {
                    MemoryStream mMemoryStream = new MemoryStream();
                    layoutControl1.SaveLayoutToStream(mMemoryStream);

                    pMdtReporter.Save_ReportTemplate("frmReporter" + pReportCode, mMemoryStream.ToArray(), this.Height, this.Width, true);
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Get_ReportingControls
        private void Get_ReportingControls()
        {
            string mFunctionName = "Get_ReportingControls";

            try
            {
                LayoutControlItem mLayoutControlItem;

                foreach (DataRow mDataRow in pDtParameters.Rows)
                {
                    string mParameterName = mDataRow["parametercode"].ToString().Trim();
                    string mParameterDescription = mDataRow["parameterdescription"].ToString().Trim();
                    string mParameterType = mDataRow["parametertype"].ToString().Trim();
                    string mParameterControl = mDataRow["parametercontrol"].ToString().Trim();
                    string mLookupTableName = mDataRow["lookuptablename"].ToString().Trim();

                    switch (mParameterControl.ToLower())
                    {
                        case "datepicker":
                            {
                                mLayoutControlItem = new LayoutControlItem();
                                mLayoutControlItem.Name = "txb" + mParameterName;
                                mLayoutControlItem.CustomizationFormText = mParameterDescription;
                                mLayoutControlItem.Text = mParameterDescription;
                                DateEdit mDateEdit = new DateEdit();
                                mDateEdit.Name = "txt" + mParameterName;
                                mDateEdit.EditValue = null;
                                mDateEdit.EditValueChanged += new EventHandler(mDateEdit_EditValueChanged);
                                try
                                {
                                    mDateEdit.EditValue = Convert.ToDateTime(mDataRow["valuedatetime"]);
                                }
                                catch { }
                                mLayoutControlItem.Control = mDateEdit;
                                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                            }
                            break;
                        case "checkbox":
                            {
                                mLayoutControlItem = new LayoutControlItem();
                                mLayoutControlItem.Name = "txb" + mParameterName;
                                mLayoutControlItem.CustomizationFormText = mParameterDescription;
                                mLayoutControlItem.Text = mParameterDescription;
                                CheckEdit mCheckEdit = new CheckEdit();
                                mCheckEdit.Name = "chk" + mParameterName;
                                mCheckEdit.Text = mParameterDescription;
                                mCheckEdit.Checked = false;
                                try
                                {
                                    mCheckEdit.Checked = Convert.ToBoolean(mDataRow["valueint"]);
                                }
                                catch { }
                                mLayoutControlItem.Control = mCheckEdit;
                                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                            }
                            break;
                        case "lookup":
                            {
                                //#region fill lookup data

                                //if (mLookupTableName.Trim() != "")
                                //{
                                //    DataTable mDtLookupData = pMdtReports.Get_GetLookupData(mLookupTableName);

                                //    if (mDtLookupData.Columns.Count == 1)
                                //    {
                                //        mLayoutControlItem = new LayoutControlItem();
                                //        mLayoutControlItem.Name = "txb" + mParameterName;
                                //        mLayoutControlItem.CustomizationFormText = mParameterDescription;
                                //        mLayoutControlItem.Text = mParameterDescription;
                                //        ComboBoxEdit mComboBoxEdit = new ComboBoxEdit();
                                //        mComboBoxEdit.Name = "cbo" + mParameterName;
                                //        mLayoutControlItem.Control = mComboBoxEdit;
                                //        mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                                //        layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                                //        ComboBoxItemCollection mItems = mComboBoxEdit.Properties.Items;
                                //        mItems.Clear();
                                //        foreach (DataRow mItemRow in mDtLookupData.Rows)
                                //        {
                                //            mItems.Add(mItemRow["description"].ToString().Trim());
                                //        }
                                //    }
                                //    else if (mDtLookupData.Columns.Count == 2)
                                //    {
                                //        mLayoutControlItem = new LayoutControlItem();
                                //        mLayoutControlItem.Name = "txb" + mParameterName;
                                //        mLayoutControlItem.CustomizationFormText = mParameterDescription;
                                //        mLayoutControlItem.Text = mParameterDescription;
                                //        LookUpEdit mLookUpEdit = new LookUpEdit();
                                //        mLookUpEdit.Name = "cbo" + mParameterName;
                                //        mLookUpEdit.Properties.TextEditStyle = TextEditStyles.Standard;
                                //        mLookUpEdit.Properties.ShowHeader = false;
                                //        mLookUpEdit.Properties.NullText = "";
                                //        mLayoutControlItem.Control = mLookUpEdit;
                                //        mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                                //        layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });

                                //        mLookUpEdit.Properties.DataSource = mDtLookupData;
                                //        mLookUpEdit.Properties.DisplayMember = "description";
                                //        mLookUpEdit.Properties.ValueMember = "code";
                                //    }
                                //}

                                //#endregion
                            }
                            break;
                        default://textbox
                            {
                                mLayoutControlItem = new LayoutControlItem();
                                mLayoutControlItem.Name = "txb" + mParameterName;
                                mLayoutControlItem.CustomizationFormText = mParameterDescription;
                                mLayoutControlItem.Text = mParameterDescription;
                                TextEdit mTextEdit = new TextEdit();
                                mTextEdit.Name = "txt" + mParameterName;
                                mTextEdit.Text = "";
                                switch (mParameterType.ToLower())
                                {
                                    case "double":
                                        {
                                            mTextEdit.Text = mDataRow["valuedouble"].ToString();
                                        }
                                        break;
                                    case "int":
                                        {
                                            mTextEdit.Text = mDataRow["valueint"].ToString();
                                        }
                                        break;
                                    default:
                                        {
                                            mTextEdit.Text = mDataRow["valuestring"].ToString();
                                        }
                                        break;
                                }
                                mLayoutControlItem.Control = mTextEdit;
                                mLayoutControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
                                layoutControl1.HiddenItems.AddRange(new BaseLayoutItem[] { mLayoutControlItem });
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region mDateEdit_EditValueChanged
        void mDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            Program.AddTimeToDate((DateEdit)sender);
        }
        #endregion

        #region Get_ReportSettings
        private void Get_ReportSettings()
        {
            string mFunctionName = "Get_ReportSettings";

            try
            {
                DataTable mDtReports = pMdtReports.View("code='" + pReportCode.Trim() + "'", "", "", "");
                if (mDtReports.Rows.Count > 0)
                {
                    pCommandText = mDtReports.Rows[0]["commandtext"].ToString().Trim();
                    pFilterString = mDtReports.Rows[0]["filterstring"].ToString().Trim();
                }

                pDtParameters = pMdtReports.View_Parameters("reportcode='" + pReportCode.Trim() + "'", "", "", "");
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdView_Click
        private void cmdView_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdView_Click";

            #region validation

            foreach (DataRow mDataRow in pDtParameters.Rows)
            {
                string mParameterName = mDataRow["parametercode"].ToString().Trim();
                string mParameterDescription = mDataRow["parameterdescription"].ToString().Trim();
                string mParameterType = mDataRow["parametertype"].ToString().Trim();
                string mParameterControl = mDataRow["parametercontrol"].ToString().Trim();

                switch (mParameterControl.ToLower())
                {
                    case "datepicker":
                        {
                            DateEdit mDateEdit = (DateEdit)layoutControl1.Controls["txt" + mParameterName];
                            mDataRow.BeginEdit();
                            if (Program.IsDate(mDateEdit.EditValue) == false)
                            {
                                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_EntryIsInvalid.ToString());
                                mDateEdit.Focus();
                                return;
                            }

                            DateTime mDateTime = Convert.ToDateTime(mDateEdit.DateTime);
                            mDataRow["valuedatetime"] = mDateTime;
                            mDataRow.EndEdit();
                            pDtParameters.AcceptChanges();
                        }
                        break;
                    case "checkbox":
                        {
                            CheckEdit mCheckEdit = (CheckEdit)layoutControl1.Controls["chk" + mParameterName];
                            mDataRow.BeginEdit();
                            mDataRow["valueint"] = Convert.ToInt16(mCheckEdit.Checked);
                            mDataRow.EndEdit();
                            pDtParameters.AcceptChanges();
                        }
                        break;
                    case "lookup":
                        {
                            if ((Control)layoutControl1.Controls["cbo" + mParameterName] is ComboBoxEdit)
                            {
                                mDataRow.BeginEdit();
                                mDataRow["valuestring"] = ((ComboBoxEdit)layoutControl1.Controls["cbo" + mParameterName]).Text.Trim();
                            }
                            else
                            {
                                LookUpEdit mLookUpEdit = (LookUpEdit)layoutControl1.Controls["cbo" + mParameterName];
                                mDataRow.BeginEdit();
                                mDataRow["valuestring"] = mLookUpEdit.GetColumnValue("code").ToString();
                            }

                            mDataRow.EndEdit();
                            pDtParameters.AcceptChanges();
                        }
                        break;
                    default:
                        {
                            TextEdit mTextEdit = (TextEdit)layoutControl1.Controls["txt" + mParameterName];
                            mDataRow.BeginEdit();
                            switch (mParameterType.ToLower())
                            {
                                case "double":
                                    {
                                        if (Program.IsMoney(mTextEdit.Text) == false)
                                        {
                                            Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_EntryIsInvalid.ToString());
                                            mTextEdit.Focus();
                                            return;
                                        }
                                        mDataRow["valuedouble"] = Convert.ToDouble(mTextEdit.Text);
                                    }
                                    break;
                                case "int":
                                    {
                                        if (Program.IsNumeric(mTextEdit.Text) == false)
                                        {
                                            Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_EntryIsInvalid.ToString());
                                            mTextEdit.Focus();
                                            return;
                                        }
                                        mDataRow["valueint"] = Convert.ToInt32(mTextEdit.Text);
                                    }
                                    break;
                                default:
                                    {
                                        mDataRow["valuestring"] = mTextEdit.Text;
                                    }
                                    break;
                            }
                            mDataRow.EndEdit();
                            pDtParameters.AcceptChanges();
                        }
                        break;
                }
            }

            #endregion

            try
            {
                this.Cursor = Cursors.WaitCursor;

                //retrieve data
                DataTable mDtData = pMdtReporter.Get_ReportingData(pReportCode, pDtParameters);

                string mFileName = pReportCode;
                byte[] mBytes = pMdtReporter.Load_ReportTemplate(mFileName, false);

                DevExpress.XtraReports.UI.XtraReport mReportDoc = Program.Load_ReportTemplate(mBytes, mDtData);

                if (mReportDoc == null)
                {
                    Program.gMdiForm.Cursor = Cursors.Default;
                    return;
                }

                //frmReportViewer mReportViewer = new frmReportViewer();
                //mReportViewer.printControl1.PrintingSystem = mReportDoc.PrintingSystem;
                //mReportDoc.CreateDocument();
                //mReportViewer.Show();
                mReportDoc.CreateDocument();
                mReportDoc.ShowPreview();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdDesign_Click
        private void cmdDesign_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdDesign_Click";

            #region validation

            foreach (DataRow mDataRow in pDtParameters.Rows)
            {
                string mParameterName = mDataRow["parametercode"].ToString().Trim();
                string mParameterDescription = mDataRow["parameterdescription"].ToString().Trim();
                string mParameterType = mDataRow["parametertype"].ToString().Trim();
                string mParameterControl = mDataRow["parametercontrol"].ToString().Trim();

                switch (mParameterControl.ToLower())
                {
                    case "datepicker":
                        {
                            DateEdit mDateEdit = (DateEdit)layoutControl1.Controls["txt" + mParameterName];
                            mDataRow.BeginEdit();
                            if (Program.IsDate(mDateEdit.EditValue) == false)
                            {
                                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_EntryIsInvalid.ToString());
                                mDateEdit.Focus();
                                return;
                            }
                            mDataRow["valuedatetime"] = Convert.ToDateTime(mDateEdit.EditValue);
                            mDataRow.EndEdit();
                            pDtParameters.AcceptChanges();
                        }
                        break;
                    case "checkbox":
                        {
                            CheckEdit mCheckEdit = (CheckEdit)layoutControl1.Controls["chk" + mParameterName];
                            mDataRow.BeginEdit();
                            mDataRow["valueint"] = Convert.ToInt16(mCheckEdit.Checked);
                            mDataRow.EndEdit();
                            pDtParameters.AcceptChanges();
                        }
                        break;
                    case "lookup":
                        {
                            if ((Control)layoutControl1.Controls["cbo" + mParameterName] is ComboBoxEdit)
                            {
                                mDataRow.BeginEdit();
                                mDataRow["valuestring"] = ((ComboBoxEdit)layoutControl1.Controls["cbo" + mParameterName]).Text.Trim();
                            }
                            else
                            {
                                LookUpEdit mLookUpEdit = (LookUpEdit)layoutControl1.Controls["cbo" + mParameterName];
                                mDataRow.BeginEdit();
                                mDataRow["valuestring"] = mLookUpEdit.GetColumnValue("code").ToString();
                            }

                            mDataRow.EndEdit();
                            pDtParameters.AcceptChanges();
                        }
                        break;
                    default:
                        {
                            TextEdit mTextEdit = (TextEdit)layoutControl1.Controls["txt" + mParameterName];
                            mDataRow.BeginEdit();
                            switch (mParameterType.ToLower())
                            {
                                case "double":
                                    {
                                        if (Program.IsMoney(mTextEdit.Text) == false)
                                        {
                                            Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_EntryIsInvalid.ToString());
                                            mTextEdit.Focus();
                                            return;
                                        }
                                        mDataRow["valuedouble"] = Convert.ToDouble(mTextEdit.Text);
                                    }
                                    break;
                                case "int":
                                    {
                                        if (Program.IsNumeric(mTextEdit.Text) == false)
                                        {
                                            Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_EntryIsInvalid.ToString());
                                            mTextEdit.Focus();
                                            return;
                                        }
                                        mDataRow["valueint"] = Convert.ToInt32(mTextEdit.Text);
                                    }
                                    break;
                                default:
                                    {
                                        mDataRow["valuestring"] = mTextEdit.Text;
                                    }
                                    break;
                            }
                            mDataRow.EndEdit();
                            pDtParameters.AcceptChanges();
                        }
                        break;
                }
            }

            #endregion

            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Create a design form and get its panel.
                DevExpress.XtraReports.UserDesigner.XRDesignFormEx mDesignForm =
                    new DevExpress.XtraReports.UserDesigner.XRDesignFormEx();
                DevExpress.XtraReports.UserDesigner.XRDesignPanel mDesignPanel = mDesignForm.DesignPanel;

                //retrieve data
                DataTable mDtData = pMdtReporter.Get_ReportingData(pReportCode, pDtParameters);

                string mFileName = pReportCode;
                byte[] mBytes = pMdtReporter.Load_ReportTemplate(mFileName, false);

                DevExpress.XtraReports.UI.XtraReport mReportDoc = Program.Load_ReportTemplate(mBytes, mDtData);

                // Add a new command handler which saves a report in a custom way.
                mDesignPanel.AddCommandHandler(new clsReportDesignerSaveCommandHandler(mDesignPanel, mFileName));

                // Load a report into the design form and show the form.
                mDesignPanel.OpenReport(mReportDoc);
                mDesignForm.ShowDialog();
                mDesignPanel.CloseReport();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
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