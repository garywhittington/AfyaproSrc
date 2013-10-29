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
    public partial class frmRCHadd : DevExpress.XtraEditors.XtraForm
    {

        private AfyaPro_MT.clsRCHplanning pMdtFamilyPlanning;
        private AfyaPro_MT.clsRCHdangerindicators pMdtDangerIndicators;
        private AfyaPro_MT.clsRCHbirthmethods pMdtBirthMethods;
        private AfyaPro_MT.clsRCHbirthcomplications pMdtBirthComplications;


        private string pClassName = "frmRCHadd";
        DataTable pDtFamilyPlanningMethods = new DataTable("Family Planning");
        DataTable pDtDangerIndicators = new DataTable("Antenatal Danger Indicators");
        DataTable pDtBirthMethods = new DataTable("Birth Methods");
        DataTable pDtBirthComplications = new DataTable("Birth Complications");



        //to show whats visible
        private string pEditor = string.Empty;

        public frmRCHadd()
        {
            string mFunctionName = "frmRCHadd()";
            this.Icon = Program.gMdiForm.Icon;
            InitializeComponent();

            try
            {

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



                //make the editable part not visibal.

                grpPlanningMethod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                grpDangerIndicator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                grpBirthMethod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                grpBirthComplication.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                //fill all the data tables
                Fill_DataTables(pDtFamilyPlanningMethods);
                Fill_DataTables(pDtBirthComplications);
                Fill_DataTables(pDtBirthMethods);
                Fill_DataTables(pDtDangerIndicators);

                Fill_Categories(cboPlanningCat);
                Fill_Categories(cboBirthMethodCat);
                Fill_Categories(cboBirthComplicationCat);
                Fill_Categories(cboDangerCat);

                Clear_All();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }

        }


        /// <summary>
        /// Set everything on the Form to default
        /// </summary>
        /// <returns></returns>
		private void Clear_All()
		{
			//clear all the txtBox's and set everything to default
			
			foreach(Control ctr in layoutControl1.Controls)
            {

                if (ctr.GetType() == typeof(DevExpress.XtraEditors.TextEdit))
                {
                    ctr.Text = "";      //clears it

                }
                if(ctr.GetType() == typeof(DevExpress.XtraEditors.RadioGroup))         //set the combo boxes to null
                {
                    
                    DevExpress.XtraEditors.RadioGroup tmp = (DevExpress.XtraEditors.RadioGroup)ctr;
                    tmp.SelectedIndex = -1;         

                }
                if (ctr.GetType() == typeof(DevExpress.XtraEditors.ComboBoxEdit))
                {

                    DevExpress.XtraEditors.ComboBoxEdit tmp = (DevExpress.XtraEditors.ComboBoxEdit)ctr;
                    tmp.EditValue = null;
                }
                    
            }
			
		
		
		}

        /// <summary>
        /// Fills' frmRCHadd designated comboboxedit with the proper information.
        /// </summary>
        /// <param name="obj"></param>
        private void Fill_Categories(DevExpress.XtraEditors.ComboBoxEdit obj)
        {

            switch (obj.Name)
            {
                case "cboPlanningCat":
                    foreach (AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods d in Enum.GetValues(typeof(AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods)))
                    {
                        obj.Properties.Items.Add(d.ToString());
                        
                    }


                    break;

                case "cboDangerCat":
                   
                    foreach(AfyaPro_Types.clsEnums.RCHDangerIndicators d in Enum.GetValues(typeof(AfyaPro_Types.clsEnums.RCHDangerIndicators)))
                    {

                        obj.Properties.Items.Add(d.ToString());
                    }
                    
                    break;
                case "cboBirthMethodCat":
                    foreach (AfyaPro_Types.clsEnums.RCHBirthMethods d in Enum.GetValues(typeof(AfyaPro_Types.clsEnums.RCHBirthMethods)))
                    {

                        obj.Properties.Items.Add(d.ToString());
                    }

                    break;
                case "cboBirthComplicationCat":
                      foreach(AfyaPro_Types.clsEnums.RCHBirthComplication d in Enum.GetValues(typeof(AfyaPro_Types.clsEnums.RCHBirthComplication)))
                    {

                        obj.Properties.Items.Add(d.ToString());
                    }
                   

                    break;

            }
            obj.Update();

        }

        /// <summary>
        /// Fills the Appropriate Data for the DataTable
        /// </summary>
        /// <param name="dt">frmRCHadd. DataTable</param>
		private void Fill_DataTables(DataTable dt)
		{
            DataRow mNewRow;
            switch (dt.TableName)
            {
                case "Family Planning":

                    
                    pDtFamilyPlanningMethods.Columns.Clear();

                    //check if the coumnds dont' already exists
                    if (pDtFamilyPlanningMethods.Columns.Count == 0)
                    {
                        pDtFamilyPlanningMethods.Columns.Add("description", typeof(System.String));
                        pDtFamilyPlanningMethods.Columns.Add("quantityentry", typeof(System.String));
                        pDtFamilyPlanningMethods.Columns.Add("category", typeof(System.String));

                    }

                    DataTable mDtFamilyPlanningMethods = this.pMdtFamilyPlanning.View("", "", Program.gLanguageName, "grdRCHdanger");

                    foreach (DataRow mDataRow in mDtFamilyPlanningMethods.Rows)
                    {
                        //do the converting from int to RCHDangerIndicators
                        AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods m = (AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods)int.Parse(mDataRow["category"].ToString());

                        mNewRow = pDtFamilyPlanningMethods.NewRow();
                        mNewRow["category"] = m.ToString();
                        mNewRow["description"] = mDataRow["description"].ToString();
                        pDtFamilyPlanningMethods.Rows.Add(mNewRow);
                        pDtFamilyPlanningMethods.AcceptChanges();
                    }
                    break;

                case "Antenatal Danger Indicators":
                    //fill data table pDtDangerIndicators

                    pDtDangerIndicators.Columns.Clear();

                    //check if the coumnds dont' already exists
                    if (pDtDangerIndicators.Columns.Count == 0)
                    {
                        pDtDangerIndicators.Columns.Add("description", typeof(System.String));
                        pDtDangerIndicators.Columns.Add("category", typeof(System.String));

                    }

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

                    break;

                case "Birth Methods":

                    //fill data tables for pDtBirthMethods

                    pDtBirthMethods.Columns.Clear();

                    //check if the coumnds dont' already exists
                    if (pDtBirthMethods.Columns.Count == 0)
                    {
                        pDtBirthMethods.Columns.Add("description", typeof(System.String));
                        pDtBirthMethods.Columns.Add("category", typeof(System.String));

                    }

                    DataTable mDtBirthMethods = this.pMdtBirthMethods.View("", "", Program.gLanguageName, "grdRCHdanger");

                    foreach (DataRow mDataRow in mDtBirthMethods.Rows)
                    {
                        //do the converting from int to RCHDangerIndicators
                        AfyaPro_Types.clsEnums.RCHBirthMethods m = (AfyaPro_Types.clsEnums.RCHBirthMethods)int.Parse(mDataRow["category"].ToString());

                        mNewRow = pDtBirthMethods.NewRow();
                        mNewRow["category"] = m.ToString();
                        mNewRow["description"] = mDataRow["description"].ToString();
                        pDtBirthMethods.Rows.Add(mNewRow);
                        pDtBirthMethods.AcceptChanges();

                    }
                    break;

                case "Birth Complications":

                    //fill data tables for pDtBirthComplications

                    pDtBirthComplications.Columns.Clear();

                    //check if the coumnds dont' already exists
                    if (pDtBirthComplications.Columns.Count == 0)
                    {
                        pDtBirthComplications.Columns.Add("description", typeof(System.String));
                        pDtBirthComplications.Columns.Add("category", typeof(System.String));

                    }

                    DataTable mDtBirthComplications = this.pMdtBirthComplications.View("", "", Program.gLanguageName, "grdRCHdanger");

                    foreach (DataRow mDataRow in mDtBirthComplications.Rows)
                    {
                        //do the converting from int to RCHDangerIndicators
                        AfyaPro_Types.clsEnums.RCHBirthComplication m = (AfyaPro_Types.clsEnums.RCHBirthComplication)int.Parse(mDataRow["category"].ToString());

                        mNewRow = pDtBirthComplications.NewRow();
                        mNewRow["category"] = m.ToString();
                        mNewRow["description"] = mDataRow["description"].ToString();
                        pDtBirthComplications.Rows.Add(mNewRow);
                        pDtBirthComplications.AcceptChanges();
                    }
                    break;

            }
		
		
		
		}
     
        /// <summary>
        /// checks to see if the category is in the datatable
        /// </summary>
        /// <param name="Cat">Category number</param>
        /// <param name="dt">DataTabe to parse</param>
        /// <returns></returns>
        public bool CategoryExistInDt(int Cat, DataTable dt)
        {
            bool ret = true;
          

            foreach (DataRow row in  dt.Rows)
            {
                //check if they are alike
                if (row["category"].ToString() == Enum.GetValues(typeof(AfyaPro_Types.clsEnums.RCHDangerIndicators)).GetValue(Cat).ToString())
                {
                    //return right there and then
                    return true;
                }
            }


            return ret;
        }


        /// <summary>
        /// checks to see if the specifit category already exists in the RCH enum types
        /// we have
        /// </summary>
        /// <param name="cat">Category Number</param>
        /// <param name="type">RCH enum type</param>
        /// <returns></returns>
        private bool CategoryExistsInEnum(int cat, System.Type type)
        {




            //check if its in our types enum
            switch (type.Name)
            {
                case "RCHFamilyPlanningMethods":
                    //enum through it all
                    foreach (AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods obj in Enum.GetValues(typeof(AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods)))
                    {

                        if ((int)obj == cat)
                            return true;
                    }
                    break;
                case "RCHDangerIndicators":
                    //enum through it all
                    foreach(AfyaPro_Types.clsEnums.RCHDangerIndicators obj in Enum.GetValues(typeof(AfyaPro_Types.clsEnums.RCHDangerIndicators)))
                    {

                        if ((int)obj == cat)
                            return true;
                    }

                    break;
                case "RCHBirthMethods":
                    //enum through it all
                    foreach (AfyaPro_Types.clsEnums.RCHBirthMethods obj in Enum.GetValues(typeof(AfyaPro_Types.clsEnums.RCHBirthMethods)))
                    {

                        if ((int)obj == cat)
                            return true;
                    }

                    break;
                case "RCHBirthComplication":
                    //enum through it all
                    foreach (AfyaPro_Types.clsEnums.RCHBirthComplication obj in Enum.GetValues(typeof(AfyaPro_Types.clsEnums.RCHBirthComplication)))
                    {

                        if ((int)obj == cat)
                            return true;
                    }

                    break;

            }
            return true;

        }

   //conver string into our enums

        #region Category Interpritor
        /// <summary>
        /// Converts the users string into one of our FamilyMethods
        /// </summary>
        /// <param name="str">users string for family method name</param>
        /// <returns></returns>
        private AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods ToFamilyPlanningMethod(string str)
        {
          
        
                if( str.ToLower().Contains("implanon"))
                    return AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods.implanon;
                    

            else if( str.ToLower().Contains("oral"))
                    return AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods.oralpills;
                    
              else  if( str.ToUpper().Contains("inject"))
                    return AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods.injection;
                    
               else if( str.ToLower().Contains("diaphragm"))
                    return AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods.diaphragm;
                    
               else if( str.ToLower().Contains("iucd"))
                    return AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods.iucd;
                    
                else if( str.ToLower().Contains("condoms"))
                    return AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods.condoms;
                    
                else if( str.ToLower().Contains("foaming"))
                    return AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods.foamingtablets;
                    
                else if( str.ToLower().Contains("bilateral"))
                    return AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods.bilateraltubeligation;
                    
               else if( str.ToLower().Contains("natural"))
                    return AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods.naturalmethods;
            //anything else just return "others" type
                    return AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods.othermethods;

        }

        /// <summary>
        /// Transforms a string to a RCHDangerIndicators type
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private AfyaPro_Types.clsEnums.RCHDangerIndicators ToDangerIndicator(string str)
        {

           
                if( str.ToLower().Contains("abort"))
                    return AfyaPro_Types.clsEnums.RCHDangerIndicators.abortion;
                    
               else if( str.ToLower().Contains("caesarian"))
                    return AfyaPro_Types.clsEnums.RCHDangerIndicators.caesariansection;
                    
               else if( str.ToLower().Contains("anaemia"))
                    return AfyaPro_Types.clsEnums.RCHDangerIndicators.anaemia;
                    
               else if( str.ToLower().Contains("oedema"))
                    return AfyaPro_Types.clsEnums.RCHDangerIndicators.oedema;
                    
               else if( str.ToLower().Contains("protenuria"))
                    return AfyaPro_Types.clsEnums.RCHDangerIndicators.protenuria;
                    
               else if( str.ToLower().Contains("pressure"))
                    return AfyaPro_Types.clsEnums.RCHDangerIndicators.highbloodpressure;
                    
               else if( str.ToLower().Contains("retard"))
                    return AfyaPro_Types.clsEnums.RCHDangerIndicators.retardedweight;
                    
               else if( str.ToLower().Contains("bleed"))
                    return AfyaPro_Types.clsEnums.RCHDangerIndicators.bleeding;
                    
               else if( str.ToLower().Contains("position"))
                    return AfyaPro_Types.clsEnums.RCHDangerIndicators.badbabyposition;
                    
      
                    return AfyaPro_Types.clsEnums.RCHDangerIndicators.otherindicators;



        }

        /// <summary>
        /// Transforms a string to a RCHBirthMethods type
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private AfyaPro_Types.clsEnums.RCHBirthMethods ToBirthMethod(string str)
        {

           
                if( str.ToLower().Contains("bba"))
                    return AfyaPro_Types.clsEnums.RCHBirthMethods.bba;
                    
                else if( str.ToLower().Contains("normal"))
                    return AfyaPro_Types.clsEnums.RCHBirthMethods.normaldelivery;
                    
               else if( str.ToLower().Contains("vacuum"))
                    return AfyaPro_Types.clsEnums.RCHBirthMethods.vacuum;
                    
               else if( str.ToLower().Contains("caesarian"))
                    return AfyaPro_Types.clsEnums.RCHBirthMethods.caesariansection;
                    

               else if( str.ToLower().Contains("abort"))
                    return AfyaPro_Types.clsEnums.RCHBirthMethods.abortion;


            //anything else just return the "Others" type
                    return AfyaPro_Types.clsEnums.RCHBirthMethods.othermethods;


        }

        /// <summary>
        /// Transforms a string into RCHBirthComplication type
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private AfyaPro_Types.clsEnums.RCHBirthComplication ToBirthComplications(string str)
        {

                  if( str.ToLower().Contains("haemorrg"))
                    return AfyaPro_Types.clsEnums.RCHBirthComplication.postpartumhaemorrg;
                  
                else if( str.ToLower().Contains("placentra"))
                    return AfyaPro_Types.clsEnums.RCHBirthComplication.retainedplacenta;
                    
                else if( str.ToLower().Contains("taer"))
                    return AfyaPro_Types.clsEnums.RCHBirthComplication.thirddegreetear;
                    
                else if( str.ToLower().Contains("death"))
                    return AfyaPro_Types.clsEnums.RCHBirthComplication.death;
                    
                else
                    return AfyaPro_Types.clsEnums.RCHBirthComplication.othercomplications;
                    

        }
        #endregion 

        
        #region Events
        private void cboRCHtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set the value to the new one
            DevExpress.XtraEditors.ComboBoxEdit cb = (DevExpress.XtraEditors.ComboBoxEdit)sender;

            switch (cb.SelectedIndex)
            {
                case 0:                             //family planning
                    //hide all but family planning
                    grpPlanningMethod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    grpDangerIndicator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    grpBirthMethod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    grpBirthComplication.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    this.pEditor = "PlanningMethod";

                    break;

                case 1:                             //Danger Indicators

                    //hide all but family planning
                    grpDangerIndicator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    grpPlanningMethod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    grpBirthMethod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    grpBirthComplication.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                    this.pEditor = "DangerIndicator";
                    break;
                case 2:                             //Birth Methods

                    //hide all but family planning
                    grpBirthMethod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    grpPlanningMethod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    grpDangerIndicator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    grpBirthComplication.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                    this.pEditor = "BirthMethod";
                    break;
                case 3:                             //Birth Complication
                    //hide all but family planning
                    grpBirthComplication.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    grpPlanningMethod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    grpDangerIndicator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    grpBirthMethod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                    this.pEditor = "BirthComplication";
                    break;




            }


            //reclear everything
            Clear_All();
        }


        private void cmdSave_Click(object sender, EventArgs e)
        {
            int Cat;
            Array a;
            //process which ever pEditor is visible
            switch (pEditor)
            {

                case "PlanningMethod":
                    //add the new planning method into the database
                    Cat = (int)ToFamilyPlanningMethod(cboPlanningCat.Text);

                    //todo:do some checking for valid values
                    pMdtFamilyPlanning.Add(txtPlanningDesc.Text, radQuantity.SelectedIndex, (int)ToFamilyPlanningMethod(this.cboPlanningCat.Text));
                    a = Enum.GetValues(typeof(AfyaPro_Types.clsEnums.RCHFamilyPlanningMethods));

                    MessageBox.Show(string.Format("Added succesfuly [{0}]", a.GetValue(Cat).ToString()));
                    //clear

                    break;
                case "DangerIndicator":
                    //add the new danger indicator into the database
                    Cat = (int)ToDangerIndicator(cboDangerCat.Text);


                    pMdtDangerIndicators.Add(txtDangerDesc.Text, Cat);

                    a = Enum.GetValues(typeof(AfyaPro_Types.clsEnums.RCHDangerIndicators));
                    MessageBox.Show(string.Format("Added succesfuly [{0}]", a.GetValue(Cat).ToString()));
                    //clear

                    break;
                case "BirthMethod":
                    //add the new birthMethod into the database
                    Cat = (int)ToBirthMethod(cboBirthMethodCat.Text);


                    pMdtBirthMethods.Add(txtBirthMethodDesc.Text, (int)ToBirthComplications(cboBirthMethodCat.Text));

                    a = Enum.GetValues(typeof(AfyaPro_Types.clsEnums.RCHBirthMethods));
                    MessageBox.Show(string.Format("Added succesfuly [{0}]", a.GetValue(Cat).ToString()));
                    //clear


                    break;
                case "BirthComplication":
                    //add the new birthComplication into the database
                    Cat = (int)ToBirthComplications(cboBirthComplicationCat.Text);


                    pMdtBirthComplications.Add(txtBirthComplicationDesc.Text, (int)ToBirthComplications(cboBirthComplicationCat.Text));

                    a = Enum.GetValues(typeof(AfyaPro_Types.clsEnums.RCHBirthComplication));
                    MessageBox.Show(string.Format("Added succesfuly [{0}]", a.GetValue(Cat).ToString()));
                    //clear

                    break;

            }

            //clear everything
            Clear_All();

        }

     
        private void cmdClear_Click(object sender, EventArgs e)
        {

            Clear_All();
        }
        #endregion

    }
}