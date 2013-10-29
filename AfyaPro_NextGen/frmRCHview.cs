using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AfyaPro_NextGen
{
    public partial class frmRCHview : DevExpress.XtraEditors.XtraForm
    {
        private AfyaPro_MT.clsRCHplanning       pMdtFamilyPlanning;
        private AfyaPro_MT.clsRCHdangerindicators pMdtDangerIndicators;
        private AfyaPro_MT.clsRCHbirthmethods pMdtBirthMethods;
        private AfyaPro_MT.clsRCHbirthcomplications pMdtBirthComplications;


        private string pClassName = "frmRCHview";
        DataTable pDtFamilyPlanningMethods = new DataTable("Family Planning");
        DataTable pDtDangerIndicators = new DataTable("Antenatal Danger Indicators");
        DataTable pDtBirthMethods = new DataTable("Birth Methods");
        DataTable pDtBirthComplications = new DataTable("Birth Complications");

        public frmRCHview()
        {
            this.Icon = Program.gMdiForm.Icon;
            InitializeComponent();

            //retreave a link to clsRCHplanning class
            pMdtFamilyPlanning = (AfyaPro_MT.clsRCHplanning)Activator.GetObject(
                typeof(AfyaPro_MT.clsRCHplanning),
                Program.gMiddleTier + "clsRCHplanning");

            //retreave a link to clsRCHdangerindicators
            pMdtDangerIndicators = (AfyaPro_MT.clsRCHdangerindicators)Activator.GetObject(
               typeof(AfyaPro_MT.clsRCHdangerindicators),
               Program.gMiddleTier + "clsRCHdangerindicators");

            //retreave a link to clsRCHBirthMethods
            pMdtBirthMethods = (AfyaPro_MT.clsRCHbirthmethods)Activator.GetObject(
               typeof(AfyaPro_MT.clsRCHbirthmethods),
               Program.gMiddleTier + "clsRCHbirthmethods");

            //retreave a link to clsRCHBirthComplications
            pMdtBirthComplications = (AfyaPro_MT.clsRCHbirthcomplications)Activator.GetObject(
               typeof(AfyaPro_MT.clsRCHbirthcomplications),
               Program.gMiddleTier + "clsRCHbirthcomplications");

            Fill_LookupData(this.cboFamilyPlanning);
            Fill_LookupData(this.cboDangerIndicators);
            Fill_LookupData(this.cboBirthMethods);
            Fill_LookupData(this.cboBirthComplications);
        }




        #region Data_Lookup
        internal void Fill_LookupData(DevExpress.XtraEditors.LookUpEdit mLookupEdit)
        {
            string mFunctionName = "Fill_LookupData";
            DataRow mNewRow;
            try
            {
                switch (mLookupEdit.Name)
                {
                    #region cboFamilyPlanning
                    case "cboFamilyPlanning":
                        pDtFamilyPlanningMethods.Rows.Clear();

                        if (pDtFamilyPlanningMethods.Columns.Count == 0)
                        {
                            pDtFamilyPlanningMethods.Columns.Add("category", typeof(System.String));
                            pDtFamilyPlanningMethods.Columns.Add("description", typeof(System.String));
                            pDtFamilyPlanningMethods.Columns.Add("quantityentry", typeof(System.String));
                            
                        }

                        cboFamilyPlanning.Properties.DataSource = pDtFamilyPlanningMethods;
                        cboFamilyPlanning.Properties.DisplayMember = "description";
                        cboFamilyPlanning.Properties.ValueMember = "category";


                        DataTable mDtFamilyPlanning = this.pMdtFamilyPlanning.View("", "", Program.gLanguageName, "grdRCHplanning");

                        foreach (DataRow mDataRow in mDtFamilyPlanning.Rows)
                        {
                            AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods m = (AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods)int.Parse(mDataRow["category"].ToString());
                            mNewRow = pDtFamilyPlanningMethods.NewRow();
                            mNewRow["category"] =  m.ToString();
                            mNewRow["description"] = mDataRow["description"].ToString();
                            mNewRow["quantityentry"] = mDataRow["quantityentry"].ToString();
                            pDtFamilyPlanningMethods.Rows.Add(mNewRow);
                            pDtFamilyPlanningMethods.AcceptChanges();
                        }


                        if (pDtFamilyPlanningMethods.DefaultView.Count == 0)
                        {
                            //user needs to add a lab into the system
                            cboFamilyPlanning.Properties.NullText = "[Not Registered]";
                            cboFamilyPlanning.Enabled = false;
                        }
                        else
                        {
                            foreach (DataColumn mDataColumn in pDtFamilyPlanningMethods.Columns)
                            {
                                mDataColumn.Caption = mDtFamilyPlanning.Columns[mDataColumn.ColumnName].Caption;
                            }
                        }


                        break;
                    #endregion

                    #region cboDangerIndicators
                    case "cboDangerIndicators":
                        pDtDangerIndicators.Rows.Clear();

                        if (pDtDangerIndicators.Columns.Count == 0)
                        {
                            pDtDangerIndicators.Columns.Add("category", typeof(System.String));
                            pDtDangerIndicators.Columns.Add("description", typeof(System.String));
                        }

                       
                        cboDangerIndicators.Properties.DataSource = pDtDangerIndicators;
                        cboDangerIndicators.Properties.DisplayMember = "description";
                        cboDangerIndicators.Properties.ValueMember = "category";


                        DataTable mDtDangerIndicators = pMdtDangerIndicators.View("", "", Program.gLanguageName, "grdRCHdanger");

                        foreach (DataRow mDataRow in mDtDangerIndicators.Rows)
                        {
                            //do the converting from int to RCHDangerIndicators
                            AfyaPro_Types.clsEnums.RCHDangerIndicators m = (AfyaPro_Types.clsEnums.RCHDangerIndicators)int.Parse(mDataRow["category"].ToString());

                            mNewRow = pDtDangerIndicators.NewRow();
                            mNewRow["category"] = m.ToString();
                            mNewRow["description"] = mDataRow["description"].ToString();
                            pDtDangerIndicators.Rows.Add(mNewRow);
                            pDtDangerIndicators.AcceptChanges();

                        }

                        if (pDtDangerIndicators.DefaultView.Count == 0)
                        {
                            //user needs to add a lab into the system
                            cboDangerIndicators.Properties.NullText = "[Not Registered]";
                            cboDangerIndicators.Enabled = false;
                        }
                        else
                        {
                            foreach (DataColumn mDataColumn in pDtDangerIndicators.Columns)
                            {
                                mDataColumn.Caption = mDtDangerIndicators.Columns[mDataColumn.ColumnName].Caption;
                            }
                        }


                        break;
                    #endregion


                    #region cboBirthMethods
                    case "cboBirthMethods":
                        pDtBirthMethods.Rows.Clear();

                        if (pDtBirthMethods.Columns.Count == 0)
                        {
                            pDtBirthMethods.Columns.Add("category", typeof(System.String));
                            pDtBirthMethods.Columns.Add("description", typeof(System.String));
                        }


                        cboBirthMethods.Properties.DataSource = pDtBirthMethods;
                        cboBirthMethods.Properties.DisplayMember = "description";
                        cboBirthMethods.Properties.ValueMember = "category";


                        DataTable mDtBirthMethods = pMdtBirthMethods.View("", "", Program.gLanguageName, "grdRCHbirthmethods");

                        foreach (DataRow mDataRow in mDtBirthMethods.Rows)
                        {
                            //do the converting from int to RCHBirthMethods
                            AfyaPro_Types.clsEnums.RCHBirthMethods m = (AfyaPro_Types.clsEnums.RCHBirthMethods)int.Parse(mDataRow["category"].ToString());

                            mNewRow = pDtBirthMethods.NewRow();
                            mNewRow["category"] = m.ToString();
                            mNewRow["description"] = mDataRow["description"].ToString();
                            pDtBirthMethods.Rows.Add(mNewRow);
                            pDtBirthMethods.AcceptChanges();

                        }

                        if (pDtBirthMethods.DefaultView.Count == 0)
                        {
                            //user needs to add a lab into the system
                            cboBirthMethods.Properties.NullText = "[Not Registered]";
                            cboBirthMethods.Enabled = false;
                        }
                        else
                        {
                            foreach (DataColumn mDataColumn in pDtBirthMethods.Columns)
                            {
                                mDataColumn.Caption = mDtBirthMethods.Columns[mDataColumn.ColumnName].Caption;
                            }
                        }


                        break;
                    #endregion



                    #region cboBirthComplications
                    case "cboBirthComplications":
                        pDtBirthComplications.Rows.Clear();

                        if (pDtBirthComplications.Columns.Count == 0)
                        {
                            pDtBirthComplications.Columns.Add("category", typeof(System.String));
                            pDtBirthComplications.Columns.Add("description", typeof(System.String));
                        }


                        cboBirthComplications.Properties.DataSource = pDtBirthComplications;
                        cboBirthComplications.Properties.DisplayMember = "description";
                        cboBirthComplications.Properties.ValueMember = "category";


                        DataTable mDtBirthComplications = this.pMdtBirthComplications.View("", "", Program.gLanguageName, "grdRCHbirthmethods");

                        foreach (DataRow mDataRow in mDtBirthComplications.Rows)
                        {
                            //do the converting from int to RCHBirthComplication
                            AfyaPro_Types.clsEnums.RCHBirthComplication m = (AfyaPro_Types.clsEnums.RCHBirthComplication)int.Parse(mDataRow["category"].ToString());

                            mNewRow = pDtBirthComplications.NewRow();
                            mNewRow["category"] = m.ToString();
                            mNewRow["description"] = mDataRow["description"].ToString();
                            pDtBirthComplications.Rows.Add(mNewRow);
                            pDtBirthComplications.AcceptChanges();

                        }

                        if (pDtBirthComplications.DefaultView.Count == 0)
                        {
                            //user needs to add a lab into the system
                            cboBirthComplications.Properties.NullText = "[Not Registered]";
                            cboBirthComplications.Enabled = false;
                        }
                        else
                        {
                            foreach (DataColumn mDataColumn in pDtBirthComplications.Columns)
                            {
                                mDataColumn.Caption = mDtBirthComplications.Columns[mDataColumn.ColumnName].Caption;
                            }
                        }


                        break;
                    #endregion

                }
                        


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