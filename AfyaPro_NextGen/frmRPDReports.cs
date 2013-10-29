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
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Controls;
using System.Text.RegularExpressions;

namespace AfyaPro_NextGen
{
    public partial class frmRPDReports : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsReportGroups pMdtReportGroups;
        private AfyaPro_MT.clsReports pMdtReports;
        private AfyaPro_MT.clsReporter pMdtReporter;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataRow pSelectedRow = null;
        internal string gDataState = "";
        private bool pFirstTimeLoad = true;

        private DataTable pDtReportGroups = new DataTable("reportgroups");
        private DataTable pDtConditionOperators = new DataTable("conditionoperators");
        private DataTable pDtParameters = new DataTable("parameters");
        private DataTable pDtConditions = new DataTable("conditions");
        private DataTable pDtConditionColumns = new DataTable("columns");

        private ComboBoxItemCollection pConditionFieldNames;
        private ComboBoxItemCollection pConditionValues;
        private ComboBoxItemCollection pConditions;
        private ComboBoxItemCollection pTableNames;

        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbotablenames;

        private bool pErrorOnPage0 = false;
        private bool pErrorOnPage1 = false;
        private bool pErrorOnPage2 = false;
        private bool pErrorOnPage3 = false;
        private bool pErrorOnPage4 = false;
        private bool pErrorOnPage5 = false;
        private bool pErrorOnPage6 = false;

        private string pReportGroupCode = "";
        private string pCommandText = "";
        private string pTableName = "";
        private string pGroupByFields = "";

        #endregion

        #region frmRPDReports
        public frmRPDReports()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmRPDReports";

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                pMdtReportGroups = (AfyaPro_MT.clsReportGroups)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReportGroups),
                    Program.gMiddleTier + "clsReportGroups");

                pMdtReports = (AfyaPro_MT.clsReports)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReports),
                    Program.gMiddleTier + "clsReports");

                pMdtReporter = (AfyaPro_MT.clsReporter)Activator.GetObject(
                    typeof(AfyaPro_MT.clsReporter),
                    Program.gMiddleTier + "clsReporter");

                pMdtAutoCodes = (AfyaPro_MT.clsAutoCodes)Activator.GetObject(
                    typeof(AfyaPro_MT.clsAutoCodes),
                    Program.gMiddleTier + "clsAutoCodes");

                pDtReportGroups.Columns.Add("code", typeof(System.String));
                pDtReportGroups.Columns.Add("description", typeof(System.String));
                cboGroup.Properties.DataSource = pDtReportGroups;
                cboGroup.Properties.DisplayMember = "description";
                cboGroup.Properties.ValueMember = "code";

                this.Fill_ReportGroups();
                this.Fill_Views();

                pDtParameters.Columns.Add("parametercode", typeof(System.String));
                pDtParameters.Columns.Add("parameterdescription", typeof(System.String));
                pDtParameters.Columns.Add("parametertype", typeof(System.String));
                pDtParameters.Columns.Add("parametercontrol", typeof(System.String));
                pDtParameters.Columns.Add("parametervalue", typeof(System.Object));
                pDtParameters.Columns.Add("parametersavevalue", typeof(System.Boolean));
                pDtParameters.Columns.Add("lookuptablename", typeof(System.String));
                pDtParameters.Columns.Add("valuedouble", typeof(System.Double));
                pDtParameters.Columns.Add("valuedatetime", typeof(System.DateTime));
                pDtParameters.Columns.Add("valueint", typeof(System.Int32));
                pDtParameters.Columns.Add("valuestring", typeof(System.String));
                
                pDtConditions.Columns.Add("conditionfieldname", typeof(System.String));
                pDtConditions.Columns.Add("conditionoperator", typeof(System.String));
                pDtConditions.Columns.Add("conditionvalue1", typeof(System.Object));
                pDtConditions.Columns.Add("conditionvalue2", typeof(System.Object));

                pDtConditionOperators.Columns.Add("code", typeof(System.String));
                pDtConditionOperators.Columns.Add("description", typeof(System.String));
                cboconditionoperator.DataSource = pDtConditionOperators;
                cboconditionoperator.DisplayMember = "code";
                cboconditionoperator.ValueMember = "code";

                grdParameters.DataSource = pDtParameters;
                viewParameters.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(viewParameters_InitNewRow);
                viewParameters.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(viewParameters_CustomRowCellEdit);

                pConditionFieldNames = cboconditionfieldname.Items;
                pConditionValues = cboconditionvalue.Items;
                pConditions = cboCondition.Properties.Items;

                cbotablenames = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
                pTableNames = cbotablenames.Items;

                grdConditions.DataSource = pDtConditions;

                this.Fill_ConditionOperators();

                DataTable mDtTableNames = pMdtReports.Get_LookupTables();
                foreach (DataRow mDataRow in mDtTableNames.Rows)
                {
                    pTableNames.Add(mDataRow["description"].ToString());
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmRPDReports_Load
        private void frmRPDReports_Load(object sender, EventArgs e)
        {
            switch (gDataState.Trim().ToLower())
            {
                case "new": Mode_New(); break;
                case "edit": Mode_Edit(); break;
            }

            this.Load_Controls();
        }
        #endregion

        #region frmRPDReports_Activated
        private void frmRPDReports_Activated(object sender, EventArgs e)
        {
            if (pFirstTimeLoad == true)
            {
                if (gDataState.Trim().ToLower() == "new")
                {
                    if (txtCode.Text.Trim().ToLower() == "<<---new--->>")
                    {
                        txtDescription.Focus();
                    }
                    else
                    {
                        txtCode.Focus();
                    }
                }
                else
                {
                    txtDescription.Focus();
                }

                pFirstTimeLoad = false;
            }
        }
        #endregion

        #region Fill_ReportGroups
        private void Fill_ReportGroups()
        {
            DataRow mNewRow;
            string mFunctionName = "Fill_ReportGroups";

            try
            {
                #region ReportGroups

                pDtReportGroups.Rows.Clear();
                DataTable mDtReportGroups = pMdtReportGroups.View("", "code", Program.gLanguageName, "grdRPDReportGroups");
                foreach (DataRow mDataRow in mDtReportGroups.Rows)
                {
                    mNewRow = pDtReportGroups.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtReportGroups.Rows.Add(mNewRow);
                    pDtReportGroups.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtReportGroups.Columns)
                {
                    mDataColumn.Caption = mDtReportGroups.Columns[mDataColumn.ColumnName].Caption;
                }

                #endregion
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_ConditionOperators
        private void Fill_ConditionOperators()
        {
            DataRow mNewRow;
            string mFunctionName = "Fill_ConditionOperators";

            try
            {
                pDtConditionOperators.Rows.Clear();
                DataTable mDtConditionOperators = pMdtReporter.Get_ConditionOperators(Program.gLanguageName, "grdRPDReports");
                foreach (DataRow mDataRow in mDtConditionOperators.Rows)
                {
                    mNewRow = pDtConditionOperators.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtConditionOperators.Rows.Add(mNewRow);
                    pDtConditionOperators.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtConditionOperators.Columns)
                {
                    mDataColumn.Caption = mDtConditionOperators.Columns[mDataColumn.ColumnName].Caption;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_ConditionFieldNames
        private void Fill_ConditionFieldNames()
        {
            string mFunctionName = "Fill_ConditionFieldNames";

            try
            {
                pDtConditionColumns = pMdtReporter.Get_TableFields(lstViews.SelectedItem.ToString());
                pConditionFieldNames.Clear();
                foreach (DataColumn mDataColumn in pDtConditionColumns.Columns)
                {
                    pConditionFieldNames.Add(mDataColumn.ColumnName);
                }
            }
            catch (Exception ex)
            {
                pErrorOnPage1 = true;
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_DefinedParameters
        private void Fill_DefinedParameters()
        {
            string mFunctionName = "Fill_DefinedParameters";

            try
            {
                pConditionValues.Clear();
                foreach (DataRow mDataRow in pDtParameters.Rows)
                {
                    if (mDataRow["parametercode"].ToString().Trim() != "")
                    {
                        pConditionValues.Add(mDataRow["parametercode"].ToString().Trim());
                    }

                    if (Convert.ToBoolean(mDataRow["parametersavevalue"]) == true)
                    {
                        mDataRow.BeginEdit();

                        if (mDataRow["parametercontrol"].ToString().Trim().ToLower() == "lookup")
                        {
                            mDataRow["lookuptablename"] = mDataRow["parametervalue"].ToString();
                        }

                        #region edit values

                        switch (mDataRow["parametertype"].ToString().Trim().ToLower())
                        {
                            case "date":
                                {
                                    if (Program.IsDate(mDataRow["parametervalue"]) == true)
                                    {
                                        mDataRow["valuedatetime"] = Convert.ToDateTime(mDataRow["parametervalue"]);
                                    }
                                    else
                                    {
                                        mDataRow["valuedatetime"] = DBNull.Value;
                                    }
                                }
                                break;
                            case "double":
                                {
                                    if (Program.IsMoney(mDataRow["parametervalue"].ToString()) == true)
                                    {
                                        mDataRow["valuedouble"] = Convert.ToDouble(mDataRow["parametervalue"]);
                                    }
                                    else
                                    {
                                        mDataRow["valuedouble"] = 0;
                                    }
                                }
                                break;
                            case "int":
                                {
                                    if (Program.IsNumeric(mDataRow["parametervalue"].ToString()) == true)
                                    {
                                        mDataRow["valueint"] = Convert.ToInt32(mDataRow["parametervalue"]);
                                    }
                                    else
                                    {
                                        mDataRow["valueint"] = 0;
                                    }
                                }
                                break;
                            case "boolean":
                                {
                                    try
                                    {
                                        mDataRow["valueint"] = Convert.ToBoolean(mDataRow["parametervalue"]);
                                    }
                                    catch
                                    {
                                        mDataRow["valueint"] = false;
                                    }
                                }
                                break;
                            default:
                                {
                                    mDataRow["valuestring"] = mDataRow["parametervalue"].ToString();
                                }
                                break;
                        }

                        #endregion

                        mDataRow.EndEdit();
                        pDtParameters.AcceptChanges();
                    }
                    else
                    {
                        if (mDataRow["parametercontrol"].ToString().Trim().ToLower() == "lookup")
                        {
                            mDataRow["lookuptablename"] = mDataRow["parametervalue"].ToString();
                        }

                        #region discard values

                        switch (mDataRow["parametertype"].ToString().Trim().ToLower())
                        {
                            case "date":
                                {
                                    mDataRow["valuedatetime"] = DBNull.Value;
                                }
                                break;
                            case "double":
                                {
                                    mDataRow["valuedouble"] = 0;
                                }
                                break;
                            case "int":
                                {
                                    mDataRow["valueint"] = 0;
                                }
                                break;
                            case "boolean":
                                {
                                    mDataRow["valueint"] = false;
                                }
                                break;
                            default:
                                {
                                    mDataRow["valuestring"] = "";
                                }
                                break;
                        }

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                pErrorOnPage2 = true;
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_Conditions
        private void Fill_Conditions()
        {
            string mFunctionName = "Fill_Conditions";
            
            try
            {
                pConditions.Clear();

                pConditions.Add(" ");
                pConditions.Add("AND");
                pConditions.Add("OR");
                pConditions.Add("(");
                pConditions.Add(")");

                foreach (DataRow mDataRow in pDtConditions.Rows)
                {
                    if (mDataRow["conditionfieldname"].ToString().Trim() != "")
                    {
                        string mFieldName = mDataRow["conditionfieldname"].ToString().ToLower().Trim();
                        string mOperator = mDataRow["conditionoperator"].ToString().Trim();
                        string mValue1 = mDataRow["conditionvalue1"].ToString().Trim();
                        string mValue2 = mDataRow["conditionvalue2"].ToString().Trim();

                        string mCondition = "";

                        #region build sql condition

                        switch (mOperator.ToLower())
                        {
                            case "=":
                                {
                                    mCondition = mFieldName + " = #" + mValue1 + "#";
                                }
                                break;
                            case "!=":
                                {
                                    mCondition = mFieldName + " <> #" + mValue1 + "#";
                                }
                                break;
                            case ">":
                                {
                                    mCondition = mFieldName + " > #" + mValue1 + "#";
                                }
                                break;
                            case ">=":
                                {
                                    mCondition = mFieldName + " >= #" + mValue1 + "#";
                                }
                                break;
                            case "<":
                                {
                                    mCondition = mFieldName + " < #" + mValue1 + "#";
                                }
                                break;
                            case "<=":
                                {
                                    mCondition = mFieldName + " <= #" + mValue1 + "#";
                                }
                                break;
                            case "axb":
                                {
                                    mCondition = mFieldName + " BETWEEN #" + mValue1 + "# AND #" + mValue2 + "#";
                                }
                                break;
                            case "!axb":
                                {
                                    mCondition = mFieldName + "NOT (BETWEEN #" + mValue1 + "# AND #" + mValue2 + "#)";
                                }
                                break;
                            case "%x%":
                                {
                                    mCondition = mFieldName + " LIKE %#" + mValue1 + "#%";
                                }
                                break;
                            case "!%x%":
                                {
                                    mCondition = mFieldName + "NOT (LIKE %#" + mValue1 + "#%)";
                                }
                                break;
                            case "x%":
                                {
                                    mCondition = mFieldName + " LIKE #" + mValue1 + "#%";
                                }
                                break;
                            case "%x":
                                {
                                    mCondition = mFieldName + " LIKE %#" + mValue1 + "#";
                                }
                                break;
                        }

                        #endregion

                        pConditions.Add(mCondition);
                    }
                }
            }
            catch (Exception ex)
            {
                pErrorOnPage3 = true;
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(pageWelcome);
            mObjectsList.Add(page0);
            mObjectsList.Add(page1);
            mObjectsList.Add(page2);
            mObjectsList.Add(page3);
            mObjectsList.Add(page4);
            mObjectsList.Add(page5);
            mObjectsList.Add(page6);
            mObjectsList.Add(pageFinish);
            mObjectsList.Add(txbCode);
            mObjectsList.Add(txbDescription);
            mObjectsList.Add(cmdAdd);
            mObjectsList.Add(cmdRemove);
            mObjectsList.Add(parametercode);
            mObjectsList.Add(parameterdescription);
            mObjectsList.Add(parametertype);
            mObjectsList.Add(parametercontrol);
            mObjectsList.Add(parametervalue);
            mObjectsList.Add(parametersavevalue);
            mObjectsList.Add(cmdAddCondition);
            mObjectsList.Add(cmdRemoveCondition);
            mObjectsList.Add(conditionfieldname);
            mObjectsList.Add(conditionoperator);
            mObjectsList.Add(conditionvalue1);
            mObjectsList.Add(conditionvalue2);
            mObjectsList.Add(txbUnGrouped);
            mObjectsList.Add(txbGrouped);
            mObjectsList.Add(cmdRemove);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region Grid_Settings
        internal void Grid_Settings(GridControl mGridControl)
        {
            string mFunctionName = "Grid_Settings";

            try
            {
                if (mGridControl.Visible == false)
                {
                    mGridControl.Visible = true;
                }

                mGridControl.DataSource = null;

                //prepare grid view
                DevExpress.XtraGrid.Views.Grid.GridView mGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
                mGridView.OptionsBehavior.Editable = false;
                mGridView.OptionsView.ShowGroupPanel = false;
                mGridControl.MainView = mGridView;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Mode_New
        private void Mode_New()
        {
            String mFunctionName = "Mode_New";

            try
            {
                Int16 mGenerateCode = pMdtAutoCodes.Auto_Generate_Code(
                    Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.reportcode));
                if (mGenerateCode == -1)
                {
                    Program.Display_Server_Error("");
                    return;
                }

                txtCode.Text = "";
                this.Data_Clear();

                if (mGenerateCode == 1)
                {
                    txtCode.Text = "<<---New--->>";
                    txtCode.Enabled = false;
                    txtDescription.Focus();
                }
                else
                {
                    txtCode.Enabled = true;
                    txtCode.Focus();
                }

                gDataState = "New";
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Mode_Edit
        private void Mode_Edit()
        {
            string mFunctionName = "Mode_Edit";

            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                this.Data_Clear();
                pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);
                txtCode.Text = pSelectedRow["code"].ToString();

                this.Data_Display();

                gDataState = "Edit";
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Data_Display
        private void Data_Display()
        {
            string mFunctionName = "Data_Display";

            try
            {
                DataTable mDtReports = pMdtReports.View("code='" + txtCode.Text.Trim() + "'", "", "", "");

                if (mDtReports.Rows.Count > 0)
                {
                    pReportGroupCode = mDtReports.Rows[0]["groupcode"].ToString();
                    txtCode.Text = mDtReports.Rows[0]["code"].ToString().Trim();
                    txtDescription.Text = mDtReports.Rows[0]["description"].ToString().Trim();
                    pTableName = mDtReports.Rows[0]["tablename"].ToString().Trim();
                    pCommandText = mDtReports.Rows[0]["commandtext"].ToString().Trim();
                    txtFilterString.Text = mDtReports.Rows[0]["filterstring"].ToString().Trim();
                    pGroupByFields = mDtReports.Rows[0]["groupbyfields"].ToString().Trim();
                    txtCustomSql.Text = pCommandText;
                }

                #region get selected table/view
                for (int mIndex = 0; mIndex < lstViews.Items.Count; mIndex++)
                {
                    if (pTableName.Trim().ToLower() == lstViews.Items[mIndex].ToString().Trim().ToLower())
                    {
                        lstViews.SelectedIndex = mIndex;
                        break;
                    }
                }
                #endregion

                #region fill parameters
                DataTable mDtParameters = pMdtReports.View_Parameters("reportcode='" + txtCode.Text.Trim() + "'", "", "", "");

                pDtParameters.Rows.Clear();
                foreach (DataRow mDataRow in mDtParameters.Rows)
                {
                    string mParameterType = mDataRow["parametertype"].ToString().Trim();
                    string mParameterControl = mDataRow["parametercontrol"].ToString().Trim();
                    string mLookupTableName = "";
                    object mParameterValue = null;

                    #region edit parametervalue

                    switch (mParameterType.ToLower())
                    {
                        case "date":
                            {
                                if (Program.IsDate(mDataRow["valuedatetime"]) == true)
                                {
                                    mParameterValue = Convert.ToDateTime(mDataRow["valuedatetime"]);
                                }
                                else
                                {
                                    mParameterValue = DBNull.Value;
                                }
                            }
                            break;
                        case "double":
                            {
                                if (Program.IsMoney(mDataRow["valuedouble"].ToString()) == true)
                                {
                                    mParameterValue = Convert.ToDouble(mDataRow["valuedouble"]);
                                }
                                else
                                {
                                    mParameterValue = 0;
                                }
                            }
                            break;
                        case "int":
                            {
                                if (Program.IsNumeric(mDataRow["valueint"].ToString()) == true)
                                {
                                    mParameterValue = Convert.ToInt32(mDataRow["valueint"]);
                                }
                                else
                                {
                                    mParameterValue = 0;
                                }
                            }
                            break;
                        case "boolean":
                            {
                                try
                                {
                                    mParameterValue = Convert.ToBoolean(mDataRow["valueint"]);
                                }
                                catch
                                {
                                    mParameterValue = false;
                                }
                            }
                            break;
                        default:
                            {
                                mParameterValue = mDataRow["valuestring"].ToString();
                            }
                            break;
                    }

                    #endregion

                    object mValueDateTime = DBNull.Value;
                    if (Program.IsDate(mDataRow["valuedatetime"]) == true)
                    {
                        mValueDateTime = Convert.ToDateTime(mDataRow["valuedatetime"]);
                    }

                    if (mParameterControl.ToLower() == "lookup")
                    {
                        mLookupTableName = mDataRow["lookuptablename"].ToString().Trim();
                        mParameterValue = mLookupTableName;
                    }

                    DataRow mNewRow = pDtParameters.NewRow();
                    mNewRow["parametercode"] = mDataRow["parametercode"].ToString().Trim();
                    mNewRow["parameterdescription"] = mDataRow["parameterdescription"].ToString().Trim();
                    mNewRow["parametertype"] = mDataRow["parametertype"].ToString().Trim();
                    mNewRow["parametercontrol"] = mDataRow["parametercontrol"].ToString().Trim();
                    mNewRow["parametervalue"] = mParameterValue;
                    mNewRow["parametersavevalue"] = Convert.ToBoolean(mDataRow["parametersavevalue"]);
                    mNewRow["valuedouble"] = Convert.ToDouble(mDataRow["valuedouble"]);
                    mNewRow["valuedatetime"] = mValueDateTime;
                    mNewRow["valueint"] = Convert.ToInt32(mDataRow["valueint"]);
                    mNewRow["valuestring"] = mDataRow["valuestring"].ToString().Trim();
                    mNewRow["lookuptablename"] = mLookupTableName;
                    pDtParameters.Rows.Add(mNewRow);
                    pDtParameters.AcceptChanges();
                }
                #endregion

                #region fill filter conditions

                DataTable mDtConditions = pMdtReports.View_FilterConditions("reportcode='" + txtCode.Text.Trim() + "'", "", "", "");
                pDtConditions.Rows.Clear();
                foreach (DataRow mDataRow in mDtConditions.Rows)
                {
                    DataRow mNewRow = pDtConditions.NewRow();
                    mNewRow["conditionfieldname"] = mDataRow["conditionfieldname"].ToString().Trim();
                    mNewRow["conditionoperator"] = mDataRow["conditionoperator"].ToString().Trim();
                    mNewRow["conditionvalue1"] = mDataRow["conditionvalue1"].ToString().Trim();
                    mNewRow["conditionvalue2"] = mDataRow["conditionvalue2"].ToString().Trim();
                    pDtConditions.Rows.Add(mNewRow);
                    pDtConditions.AcceptChanges();
                }

                #endregion
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Data_Clear
        private void Data_Clear()
        {
            txtDescription.Text = "";
        }
        #endregion

        #region Fill_Views
        private void Fill_Views()
        {
            string mFunctionName = "Fill_Views";

            try
            {
                DataTable mDtViews = pMdtReporter.Get_ViewsList();

                lstViews.Items.Clear();
                foreach (DataRow mDataRow in mDtViews.Rows)
                {
                    lstViews.Items.Add(mDataRow["TABLE_NAME"].ToString());
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_GroupByFields
        private void Fill_GroupByFields()
        {
            string mFunctionName = "Fill_GroupByFields";

            try
            {
                lstUnGrouped.Items.Clear();
                lstGrouped.Items.Clear();

                DataTable mDtColumns = pMdtReporter.Get_TableFields(lstViews.SelectedItem.ToString());

                string[] mGroupByFields = pGroupByFields.Split(',');

                foreach (DataColumn mDataColumn in mDtColumns.Columns)
                {
                    bool mFieldFound = false;
                    for (int mIndex = 0; mIndex < mGroupByFields.Length; mIndex++)
                    {
                        if (mDataColumn.ColumnName.ToLower() == mGroupByFields[mIndex].Trim().ToLower())
                        {
                            mFieldFound = true;
                            break;
                        }
                    }

                    if (mFieldFound == false)
                    {
                        lstUnGrouped.Items.Add(mDataColumn.ColumnName);
                    }
                }

                for (int mIndex = 0; mIndex < mGroupByFields.Length; mIndex++)
                {
                    if (mGroupByFields[mIndex].Trim() != "")
                    {
                        lstGrouped.Items.Add(mGroupByFields[mIndex].Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                pErrorOnPage3 = true;
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Get_TableSelectStatement
        private void Get_TableSelectStatement()
        {
            string mFunctionName = "Get_TableSelectStatement";

            try
            {
                DataTable mDtColumns = pMdtReporter.Get_TableFields(pTableName);

                string mFieldsList = "";

                foreach (DataColumn mDataColumn in mDtColumns.Columns)
                {
                    if (mFieldsList.Trim() == "")
                    {
                        mFieldsList = mDataColumn.ColumnName;
                    }
                    else
                    {
                        mFieldsList = mFieldsList + Environment.NewLine + ", " + mDataColumn.ColumnName;
                    }
                }

                pCommandText = "SELECT " + Environment.NewLine
                    + mFieldsList + " " + Environment.NewLine
                    + "FROM " + pTableName;
                txtCustomSql.Text = pCommandText;
            }
            catch (Exception ex)
            {
                pErrorOnPage0 = true;
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Data_Fill
        internal void Data_Fill(GridControl mGridControl)
        {
            string mFunctionName = "Data_Fill";

            try
            {
                //load data
                DataTable mDtReports = pMdtReports.View("", "", Program.gLanguageName, mGridControl.Name);
                mGridControl.DataSource = mDtReports;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Data_New
        private void Data_New()
        {
            Int16 mGenerateCode = 0;
            String mFunctionName = "Data_New";

            #region validation
            if (cboGroup.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RPD_ReportGroupDescriptionIsInvalid.ToString());
                cboGroup.Focus();
                return;
            }

            if (txtCode.Text.Trim() == "" && txtCode.Text.Trim().ToLower() != "<<---new--->>")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RPD_ReportCodeIsInvalid.ToString());
                txtCode.Focus();
                return;
            }

            if (txtDescription.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RPD_ReportDescriptionIsInvalid.ToString());
                txtDescription.Focus();
                return;
            }
            #endregion

            try
            {
                if (txtCode.Text.Trim().ToLower() == "<<---new--->>")
                {
                    mGenerateCode = 1;
                }

                //add
                pResult = pMdtReports.Add(mGenerateCode, txtCode.Text, txtDescription.Text,
                    cboGroup.GetColumnValue("code").ToString(), pDtParameters, pDtConditions, 
                    pTableName, pCommandText, txtFilterString.Text.Trim(), pGroupByFields, 
                    Program.gCurrentUser.Code);
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

                txtCode.Text = pResult.GeneratedCode;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Data_Edit
        private void Data_Edit()
        {
            String mFunctionName = "Data_Edit";

            #region validation
            if (cboGroup.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RPD_ReportGroupDescriptionIsInvalid.ToString());
                cboGroup.Focus();
                return;
            }

            if (txtCode.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RPD_ReportCodeIsInvalid.ToString());
                txtCode.Focus();
                return;
            }

            if (txtDescription.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RPD_ReportDescriptionIsInvalid.ToString());
                txtDescription.Focus();
                return;
            }
            #endregion

            try
            {
                //edit 
                pResult = pMdtReports.Edit(txtCode.Text, txtDescription.Text,
                    cboGroup.GetColumnValue("code").ToString(), pDtParameters, pDtConditions,
                    pTableName, pCommandText, txtFilterString.Text.Trim(), pGroupByFields,
                    Program.gCurrentUser.Code);
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
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Data_Delete
        internal void Data_Delete()
        {
            String mFunctionName = "Data_Delete";

            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView mGridView =
                    (DevExpress.XtraGrid.Views.Grid.GridView)((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain.MainView;

                if (mGridView.FocusedRowHandle < 0)
                {
                    return;
                }

                pSelectedRow = mGridView.GetDataRow(mGridView.FocusedRowHandle);

                DialogResult mResp = Program.Confirm_Deletion(
                    pSelectedRow["code"].ToString().Trim() + "'   '"
                    + pSelectedRow["description"].ToString().Trim());

                if (mResp != DialogResult.Yes)
                {
                    return;
                }

                //add 
                pResult = pMdtReports.Delete(pSelectedRow["code"].ToString().Trim(), Program.gCurrentUser.Code);
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

                //refresh
                this.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
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

        #region viewParameters_CustomRowCellEdit
        void viewParameters_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName != "parametervalue")
            {
                return;
            }

            if (e.RowHandle < 0)
            {
                return;
            }

            viewParameters.PostEditor();

            string mParameterControl = viewParameters.GetRowCellValue(e.RowHandle,
                viewParameters.Columns["parametercontrol"]).ToString();
            switch (mParameterControl.Trim().ToLower())
            {
                case "datepicker":
                    {
                        e.RepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
                    }
                    break;
                case "textbox":
                    {
                        e.RepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
                    }
                    break;
                case "combobox":
                    {
                        e.RepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
                    }
                    break;
                case "checkbox":
                    {
                        e.RepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
                    }
                    break;
                case "lookup":
                    {
                        e.RepositoryItem = cbotablenames;
                    }
                    break;
            }
        }
        #endregion

        #region viewParameters_InitNewRow
        void viewParameters_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            viewParameters.SetRowCellValue(e.RowHandle,
                viewParameters.Columns["parametersavevalue"], false);
            viewParameters.SetRowCellValue(e.RowHandle,
                viewParameters.Columns["valuedatetime"], new DateTime());
            viewParameters.SetRowCellValue(e.RowHandle,
                viewParameters.Columns["valuedouble"], 0);
            viewParameters.SetRowCellValue(e.RowHandle,
                viewParameters.Columns["valueint"], 0);
            viewParameters.SetRowCellValue(e.RowHandle,
                viewParameters.Columns["valuestring"], "");
            viewParameters.SetRowCellValue(e.RowHandle,
                viewParameters.Columns["lookuptablename"], "");
        }
        #endregion

        #region cmdAdd_Click
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            viewParameters.AddNewRow();
        }
        #endregion

        #region cmdRemove_Click
        private void cmdRemove_Click(object sender, EventArgs e)
        {
            viewParameters.DeleteSelectedRows();
        }
        #endregion

        #region cmdAddCondition_Click
        private void cmdAddCondition_Click(object sender, EventArgs e)
        {
            viewConditions.AddNewRow();
        }
        #endregion

        #region cmdRemoveCondition_Click
        private void cmdRemoveCondition_Click(object sender, EventArgs e)
        {
            viewConditions.DeleteSelectedRows();
        }
        #endregion

        #region wizardControl1_NextClick
        private void wizardControl1_NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            string mFunctionName = "wizardControl1_NextClick";

            try
            {
                switch (e.Page.Name.ToLower())
                {
                    case "page0":
                        {
                            pErrorOnPage0 = false;

                            if (txtCode.Text.Trim() == "" && txtCode.Text.Trim().ToLower() != "<<---new--->>")
                            {
                                pErrorOnPage0 = true;
                                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RPD_ReportCodeIsInvalid.ToString());
                                txtCode.Focus();
                                return;
                            }

                            if (txtDescription.Text.Trim() == "")
                            {
                                pErrorOnPage0 = true;
                                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.RPD_ReportDescriptionIsInvalid.ToString());
                                txtDescription.Focus();
                                return;
                            }

                            if (lstViews.Items.Count == 0)
                            {
                                pErrorOnPage0 = true;
                                return;
                            }

                            if (lstViews.SelectedIndex == -1)
                            {
                                pErrorOnPage0 = true;
                                Program.Display_Info(AfyaPro_Types.clsSystemMessages.MessageIds.RPD_ViewNotSelected.ToString());
                                return;
                            }

                            if (pTableName.Trim().ToLower() != lstViews.SelectedItem.ToString().Trim().ToLower())
                            {
                                pTableName = lstViews.SelectedItem.ToString();
                                this.Get_TableSelectStatement();
                            }
                        }
                        break;
                    case "page1":
                        {
                            pErrorOnPage1 = false;

                            this.Fill_ConditionFieldNames();
                            pCommandText = txtCustomSql.Text;
                        }
                        break;
                    case "page2":
                        {
                            pErrorOnPage2 = false;

                            pDtParameters.AcceptChanges();
                            this.Fill_DefinedParameters();
                        }
                        break;
                    case "page3":
                        {
                            pErrorOnPage3 = false;

                            pDtConditions.AcceptChanges();
                            this.Fill_Conditions();
                            this.Fill_GroupByFields();
                        }
                        break;
                    case "page5":
                        {
                            pErrorOnPage5 = false;

                            txtSQLStatement.Text = pMdtReporter.Get_GeneratedSQL(
                                pCommandText, txtFilterString.Text, pGroupByFields, pDtParameters);

                            switch (gDataState.Trim().ToLower())
                            {
                                case "new": this.Data_New(); break;
                                case "edit": this.Data_Edit(); break;
                            }

                            this.Data_Display();
                        }
                        break;
                    case "page6":
                        {
                            pErrorOnPage6 = false;

                            frmRPDReportingForm mRPDReportingForm = new frmRPDReportingForm(txtCode.Text, true);
                            mRPDReportingForm.Text = txtDescription.Text;
                            mRPDReportingForm.ShowDialog();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region wizardControl1_SelectedPageChanged
        private void wizardControl1_SelectedPageChanged(object sender, DevExpress.XtraWizard.WizardPageChangedEventArgs e)
        {
            switch (e.Page.Name.ToLower())
            {
                case "page0":
                    {
                        cboGroup.ItemIndex = Program.Get_LookupItemIndex(cboGroup, "code", pReportGroupCode);
                    }
                    break;
            }
        }
        #endregion

        #region wizardControl1_SelectedPageChanging
        private void wizardControl1_SelectedPageChanging(object sender, DevExpress.XtraWizard.WizardPageChangingEventArgs e)
        {
            if (e.Direction == DevExpress.XtraWizard.Direction.Forward)
            {
                switch (e.PrevPage.Name.ToLower())
                {
                    case "page0":
                        {
                            if (pErrorOnPage0 == true)
                            {
                                e.Cancel = true;
                            }
                        }
                        break;
                    case "page1":
                        {
                            if (pErrorOnPage1 == true)
                            {
                                e.Cancel = true;
                            }
                        }
                        break;
                    case "page2":
                        {
                            if (pErrorOnPage2 == true)
                            {
                                e.Cancel = true;
                            }
                        }
                        break;
                    case "page3":
                        {
                            if (pErrorOnPage3 == true)
                            {
                                e.Cancel = true;
                            }
                        }
                        break;
                    case "page4":
                        {
                            if (pErrorOnPage4 == true)
                            {
                                e.Cancel = true;
                            }
                        }
                        break;
                    case "page5":
                        {
                            if (pErrorOnPage5 == true)
                            {
                                e.Cancel = true;
                            }
                        }
                        break;
                    case "page6":
                        {
                            if (pErrorOnPage6 == true)
                            {
                                e.Cancel = true;
                            }
                        }
                        break;
                }
            }
        }
        #endregion

        #region cboGroup_EditValueChanged
        private void cboGroup_EditValueChanged(object sender, EventArgs e)
        {
            pReportGroupCode = cboGroup.GetColumnValue("code").ToString();
        }
        #endregion

        #region viewParameters_ValidatingEditor
        private void viewParameters_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            switch (viewParameters.FocusedColumn.Name.Trim().ToLower())
            {
                case "parametercode":
                    {
                        #region parametercode

                        if (Regex.IsMatch(e.Value.ToString(), @"^[a-zA-Z0-9]*$") == false)
                        {
                            e.Valid = false;
                            e.ErrorText = Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.RPD_ParameterCodeIsInvalid.ToString());
                            pErrorOnPage2 = true;
                            return;
                        }

                        bool mDuplicateFound = false;
                        foreach (DataRow mDataRow in pDtParameters.Rows)
                        {
                            if (e.Value.ToString().Trim().ToLower() ==
                                mDataRow["parametercode"].ToString().Trim().ToLower())
                            {
                                mDuplicateFound = true;
                            }
                        }

                        if (mDuplicateFound == true)
                        {
                            if (viewParameters.FocusedRowHandle >= 0)
                            {
                                DataRow mSelectedRow = viewParameters.GetDataRow(viewParameters.FocusedRowHandle);
                                if (mSelectedRow["parametercode"].ToString().Trim().ToLower() == e.Value.ToString().Trim().ToLower())
                                {
                                    mDuplicateFound = false;
                                }
                            }
                        }

                        if (mDuplicateFound == true)
                        {
                            e.Valid = false;
                            e.ErrorText = Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.RPD_ParameterCodeIsInUse.ToString());
                            pErrorOnPage2 = true;
                            return;
                        }

                        e.Value = e.Value.ToString().Trim().ToLower();

                        #endregion
                    }
                    break;
                case "parametervalue":
                    {
                        DataRow mSelectedRow = viewParameters.GetDataRow(viewParameters.FocusedRowHandle);
                        string mParameterType = mSelectedRow["parametertype"].ToString().Trim();

                        #region validate inputs

                        switch (mParameterType.ToLower())
                        {
                            case "date":
                                {
                                    if (e.Value == null)
                                    {
                                        e.Value = DBNull.Value;
                                    }
                                    else
                                    {
                                        if (Program.IsDate(e.Value) == false)
                                        {
                                            e.Valid = false;
                                            e.ErrorText = Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_InvalidTypeConvertion.ToString());
                                            pErrorOnPage2 = true;
                                            return;
                                        }
                                    }
                                }
                                break;
                            case "double":
                                {
                                    if (e.Value.ToString() == "" || e.Value == null)
                                    {
                                        e.Value = 0;
                                    }
                                    else
                                    {
                                        if (Program.IsMoney(e.Value.ToString()) == false)
                                        {
                                            e.Valid = false;
                                            e.ErrorText = Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_InvalidTypeConvertion.ToString());
                                            pErrorOnPage2 = true;
                                            return;
                                        }
                                    }
                                }
                                break;
                            case "int":
                                {
                                    if (e.Value.ToString() == "" || e.Value == null)
                                    {
                                        e.Value = 0;
                                    }
                                    else
                                    {
                                        if (Program.IsNumeric(e.Value.ToString()) == false)
                                        {
                                            e.Valid = false;
                                            e.ErrorText = Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_InvalidTypeConvertion.ToString());
                                            pErrorOnPage2 = true;
                                            return;
                                        }
                                    }
                                }
                                break;
                            case "boolean":
                                {
                                    if (e.Value.ToString() == "" || e.Value == null)
                                    {
                                        e.Value = false;
                                    }
                                    else
                                    {
                                        if (Convert.ToInt32(e.Value) != 0 && Convert.ToInt32(e.Value) != 1)
                                        {
                                            e.Valid = false;
                                            e.ErrorText = Program.TransLate(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_InvalidTypeConvertion.ToString());
                                            pErrorOnPage2 = true;
                                            return;
                                        }
                                    }
                                }
                                break;
                        }

                        #endregion
                    }
                    break;
            }
        }
        #endregion

        #region cmdInsert_Click
        private void cmdInsert_Click(object sender, EventArgs e)
        {
            txtFilterString.Focus();
            txtFilterString.SelectedText = cboCondition.Text;
            cboCondition.SelectedIndex = -1;
        }
        #endregion

        #region wizardControl1_FinishClick
        private void wizardControl1_FinishClick(object sender, CancelEventArgs e)
        {
            string mFunctionName = "wizardControl1_FinishClick";

            try
            {
                this.Data_Fill(((frmMainGrid)Program.gMdiForm.ActiveMdiChild).grdMain);
                this.Close();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdReset_Click
        private void cmdReset_Click(object sender, EventArgs e)
        {
            this.Get_TableSelectStatement();
        }
        #endregion

        #region cmdAddGroup_Click
        private void cmdAddGroup_Click(object sender, EventArgs e)
        {
            if (lstUnGrouped.SelectedIndex == -1)
            {
                return;
            }

            object mSelected = lstUnGrouped.SelectedItem;

            lstGrouped.Items.Add(mSelected);
            lstUnGrouped.Items.Remove(lstUnGrouped.SelectedItem);

            pGroupByFields = "";
            for (int mIndex = 0; mIndex < lstGrouped.Items.Count; mIndex++)
            {
                if (pGroupByFields.Trim() == "")
                {
                    pGroupByFields = lstGrouped.Items[mIndex].ToString();
                }
                else
                {
                    pGroupByFields = pGroupByFields + ", " + lstGrouped.Items[mIndex].ToString();
                }
            }
        }
        #endregion

        #region cmdRemoveGroup_Click
        private void cmdRemoveGroup_Click(object sender, EventArgs e)
        {
            if (lstGrouped.SelectedIndex == -1)
            {
                return;
            }

            object mSelected = lstGrouped.SelectedItem;

            lstUnGrouped.Items.Add(mSelected);
            lstGrouped.Items.Remove(lstGrouped.SelectedItem);

            pGroupByFields = "";
            for (int mIndex = 0; mIndex < lstGrouped.Items.Count; mIndex++)
            {
                if (pGroupByFields.Trim() == "")
                {
                    pGroupByFields = lstGrouped.Items[mIndex].ToString();
                }
                else
                {
                    pGroupByFields = pGroupByFields + ", " + lstGrouped.Items[mIndex].ToString();
                }
            }
        }
        #endregion

        #region cmdUpGroup_Click
        private void cmdUpGroup_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region cmdDownGroup_Click
        private void cmdDownGroup_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}