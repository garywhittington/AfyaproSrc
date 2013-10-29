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

namespace AfyaPro_NextGen
{
    public partial class frmGENFacilitySetup : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsFacilitySetup pMdtFacilitySetup;
        private AfyaPro_MT.clsCountries pMdtCountries;
        private AfyaPro_MT.clsRegions pMdtRegions;
        private AfyaPro_MT.clsDistricts pMdtDistricts;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private String pClassName = "";

        private DataTable pDtCountries = new DataTable("countries");
        private DataTable pDtRegions = new DataTable("regions");
        private DataTable pDtDistricts = new DataTable("districts");
        private DataTable pDtFacilityTypes = new DataTable("facilitytypes");

        #endregion

        #region frmGENFacilitySetup
        public frmGENFacilitySetup()
        {
            InitializeComponent();

            string mFunctionName = "frmGENFacilitySetup";
            try
            {
                pType = this.GetType();
                pClassName = pType.FullName;

                pMdtCountries = (AfyaPro_MT.clsCountries)Activator.GetObject(
                    typeof(AfyaPro_MT.clsCountries),
                    Program.gMiddleTier + "clsCountries");

                pMdtRegions = (AfyaPro_MT.clsRegions)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRegions),
                    Program.gMiddleTier + "clsRegions");

                pMdtDistricts = (AfyaPro_MT.clsDistricts)Activator.GetObject(
                    typeof(AfyaPro_MT.clsDistricts),
                    Program.gMiddleTier + "clsDistricts");

                pMdtFacilitySetup = (AfyaPro_MT.clsFacilitySetup)Activator.GetObject(
                    typeof(AfyaPro_MT.clsFacilitySetup),
                    Program.gMiddleTier + "clsFacilitySetup");

                pDtCountries.Columns.Add("code", typeof(System.String));
                pDtCountries.Columns.Add("description", typeof(System.String));
                cboCountry.Properties.DataSource = pDtCountries;
                cboCountry.Properties.DisplayMember = "description";
                cboCountry.Properties.ValueMember = "code";
                cboCountry.Properties.BestFit();

                pDtRegions.Columns.Add("code", typeof(System.String));
                pDtRegions.Columns.Add("description", typeof(System.String));
                cboRegion.Properties.DataSource = pDtRegions;
                cboRegion.Properties.DisplayMember = "description";
                cboRegion.Properties.ValueMember = "code";
                cboRegion.Properties.BestFit();

                pDtDistricts.Columns.Add("code", typeof(System.String));
                pDtDistricts.Columns.Add("description", typeof(System.String));
                cboDistrict.Properties.DataSource = pDtDistricts;
                cboDistrict.Properties.DisplayMember = "description";
                cboDistrict.Properties.ValueMember = "code";
                cboDistrict.Properties.BestFit();

                pDtFacilityTypes.Columns.Add("code", typeof(System.String));
                pDtFacilityTypes.Columns.Add("Description", typeof(System.String));                
                cboFacilityType.Properties.PopulateColumns();
                cboFacilityType.Properties.DataSource = pDtFacilityTypes;
                cboFacilityType.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("code"));
                cboFacilityType.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Description"));
                cboFacilityType.Properties.Columns["code"].Visible = false;
                cboFacilityType.Properties.DisplayMember = "Description";
                cboFacilityType.Properties.ValueMember = "code";
                cboFacilityType.Properties.BestFit();

                chkRoundingStrictness.Enabled = false;
                radMidpointOption.Enabled = false;

                chkRoundingStrictness.Checked = false;
                radMidpointOption.SelectedIndex = -1;

                this.Fill_LookupData();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmGENFacilitySetup_Load
        private void frmGENFacilitySetup_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Load_Controls();
            this.Data_Display();

            Program.Center_Screen(this);
        }
        #endregion

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(txbCode);
            mObjectsList.Add(txbFacilityName);
            mObjectsList.Add(txbCountry);
            mObjectsList.Add(txbRegion);
            mObjectsList.Add(txbDistrict);
            mObjectsList.Add(txbBox);
            mObjectsList.Add(txbPhoneNo);
            mObjectsList.Add(txbStreet);
            mObjectsList.Add(txbFacilityType);
            mObjectsList.Add(grpHeadOfFacility);
            mObjectsList.Add(txbHeadName);
            mObjectsList.Add(txbHeadDesignation);
            mObjectsList.Add(grpRounding);
            mObjectsList.Add(radRoundingOption.Properties.Items[0]);
            mObjectsList.Add(radRoundingOption.Properties.Items[1]);
            mObjectsList.Add(radRoundingOption.Properties.Items[2]);
            mObjectsList.Add(txbRoundingFigure);
            mObjectsList.Add(txbRoundingDecimals);
            mObjectsList.Add(chkRoundingStrictness);
            mObjectsList.Add(txbMidpointOption);
            mObjectsList.Add(radMidpointOption.Properties.Items[0]);
            mObjectsList.Add(radMidpointOption.Properties.Items[1]);
            mObjectsList.Add(cmdOk);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region Data_Display
        private void Data_Display()
        {
            String mFunctionName = "Data_Display";

            try
            {
                DataTable mDtFacilitySetup = pMdtFacilitySetup.View("", "");

                if (mDtFacilitySetup.Rows.Count > 0)
                {
                    txtCode.Text = mDtFacilitySetup.Rows[0]["facilitycode"].ToString().Trim();
                    txtFacilityName.Text = mDtFacilitySetup.Rows[0]["facilitydescription"].ToString().Trim();
                    txtBox.Text = mDtFacilitySetup.Rows[0]["box"].ToString().Trim();
                    txtStreet.Text = mDtFacilitySetup.Rows[0]["street"].ToString().Trim();
                    cboCountry.ItemIndex = Program.Get_LookupItemIndex(cboCountry, "code", mDtFacilitySetup.Rows[0]["countrycode"].ToString());
                    cboRegion.ItemIndex = Program.Get_LookupItemIndex(cboRegion, "code", mDtFacilitySetup.Rows[0]["regioncode"].ToString());
                    cboDistrict.ItemIndex = Program.Get_LookupItemIndex(cboDistrict, "code", mDtFacilitySetup.Rows[0]["districtcode"].ToString());
                    txtPhoneNo.Text = mDtFacilitySetup.Rows[0]["teleno"].ToString().Trim();
                    cboFacilityType.ItemIndex = Program.Get_LookupItemIndex(cboFacilityType, "code", "facilitytype" + mDtFacilitySetup.Rows[0]["facilitytype"].ToString());
                    txtHeadName.Text = mDtFacilitySetup.Rows[0]["headname"].ToString().Trim();
                    txtHeadDesignation.Text = mDtFacilitySetup.Rows[0]["headdesignation"].ToString().Trim();
                    radRoundingOption.SelectedIndex = Convert.ToInt16(mDtFacilitySetup.Rows[0]["roundingoption"]);
                    txtRoundingFigure.Text = mDtFacilitySetup.Rows[0]["roundingfigure"].ToString().Trim();
                    txtRoundingDecimals.Text = mDtFacilitySetup.Rows[0]["roundingdecimals"].ToString().Trim();
                    chkRoundingStrictness.Checked = Convert.ToBoolean(mDtFacilitySetup.Rows[0]["roundingstrictness"]);
                    radMidpointOption.SelectedIndex = Convert.ToInt16(mDtFacilitySetup.Rows[0]["roundingmidpointoption"]);
                    chkAffectStockAtCashier.Checked = Convert.ToBoolean(mDtFacilitySetup.Rows[0]["affectstockatcashier"]);
                    chkDoubleEntryIssuing.Checked = Convert.ToBoolean(mDtFacilitySetup.Rows[0]["doubleentryissuing"]);
                    txtRefreshMinutes.Text = mDtFacilitySetup.Rows[0]["transferoutrefreshinterval"].ToString();
                    radCharacterCasingOption.SelectedIndex = Convert.ToInt16(mDtFacilitySetup.Rows[0]["charactercasingoptionpatientnames"]);
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_LookupData
        private void Fill_LookupData()
        {
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region countries
                pDtCountries.Rows.Clear();

                DataTable mDtCountries = pMdtCountries.View("", "code", Program.gLanguageName, "grdGENCountries");
                foreach (DataRow mDataRow in mDtCountries.Rows)
                {
                    DataRow mNewRow = pDtCountries.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtCountries.Rows.Add(mNewRow);
                    pDtCountries.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtCountries.Columns)
                {
                    mDataColumn.Caption = mDtCountries.Columns[mDataColumn.ColumnName].Caption;
                }
                #endregion

                #region facilitytypes
                pDtFacilityTypes.Rows.Clear();

                DataTable mDtFacilityTypes = pMdtFacilitySetup.Get_FacilityTypes(Program.gLanguageName, this.Name);
                foreach (DataRow mDataRow in mDtFacilityTypes.Rows)
                {
                    DataRow mNewRow = pDtFacilityTypes.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtFacilityTypes.Rows.Add(mNewRow);
                    pDtFacilityTypes.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtFacilityTypes.Columns)
                {
                    mDataColumn.Caption = mDtFacilityTypes.Columns[mDataColumn.ColumnName].Caption;
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

        #region cboCountry_EditValueChanged
        void cboCountry_EditValueChanged(object sender, EventArgs e)
        {
            string mFunctionName = "cboCountry_EditValueChanged";

            try
            {
                pDtRegions.Rows.Clear();
                if (cboCountry.ItemIndex == -1)
                {
                    return;
                }

                string mCountryCode = cboCountry.GetColumnValue("code").ToString().Trim();

                DataTable mDtRegions = pMdtRegions.View(
                    "countrycode='" + mCountryCode + "'", "code", Program.gLanguageName, "grdGENRegions");
                foreach (DataRow mDataRow in mDtRegions.Rows)
                {
                    DataRow mNewRow = pDtRegions.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtRegions.Rows.Add(mNewRow);
                    pDtRegions.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtRegions.Columns)
                {
                    mDataColumn.Caption = mDtRegions.Columns[mDataColumn.ColumnName].Caption;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cboRegion_EditValueChanged
        void cboRegion_EditValueChanged(object sender, EventArgs e)
        {
            string mFunctionName = "cboRegion_EditValueChanged";

            try
            {
                pDtDistricts.Rows.Clear();
                if (cboCountry.ItemIndex == -1 || cboRegion.ItemIndex == -1)
                {
                    return;
                }

                string mCountryCode = cboCountry.GetColumnValue("code").ToString().Trim();
                string mRegionCode = cboRegion.GetColumnValue("code").ToString().Trim();

                DataTable mDtDistricts = pMdtDistricts.View(
                    "countrycode='" + mCountryCode + "' and regioncode='" + mRegionCode + "'", "code", Program.gLanguageName, "grdGENDistricts");
                foreach (DataRow mDataRow in mDtDistricts.Rows)
                {
                    DataRow mNewRow = pDtDistricts.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    pDtDistricts.Rows.Add(mNewRow);
                    pDtDistricts.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtDistricts.Columns)
                {
                    mDataColumn.Caption = mDtDistricts.Columns[mDataColumn.ColumnName].Caption;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdOk_Click
        private void cmdOk_Click(object sender, EventArgs e)
        {
            string mCountry = "";
            string mRegion = "";
            string mDistrict = "";
            Int16 mFacilityType = 0;

            string mFunctionName = "cmdOk_Click";

            #region validation
            if (txtFacilityName.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GEN_FacilityNameIsInvalid.ToString());
                txtFacilityName.Focus();
                return;
            }
            #endregion

            try
            {
                if (cboCountry.ItemIndex != -1)
                {
                    mCountry = cboCountry.GetColumnValue("code").ToString();
                }
                if (cboRegion.ItemIndex != -1)
                {
                    mRegion = cboRegion.GetColumnValue("code").ToString();
                }
                if (cboDistrict.ItemIndex != -1)
                {
                    mDistrict = cboDistrict.GetColumnValue("code").ToString();
                }
                if (cboFacilityType.ItemIndex != -1)
                {
                    mFacilityType = Convert.ToInt16(cboFacilityType.GetColumnValue("code").ToString().Substring(12));
                }

                //Edit 
                pResult = pMdtFacilitySetup.Edit(txtCode.Text, txtFacilityName.Text,
                    txtBox.Text, txtStreet.Text, txtPhoneNo.Text, mRegion, mDistrict,
                    mCountry, txtHeadName.Text, txtHeadDesignation.Text,
                    mFacilityType, radRoundingOption.SelectedIndex, Convert.ToInt16(txtRoundingFigure.Text),
                    Convert.ToUInt16(txtRoundingDecimals.Text), Convert.ToInt16(chkRoundingStrictness.Checked),
                    radMidpointOption.SelectedIndex, Convert.ToInt16(chkAffectStockAtCashier.Checked),
                    Convert.ToInt16(chkDoubleEntryIssuing.Checked), Convert.ToInt16(txtRefreshMinutes.Text),
                    radCharacterCasingOption.SelectedIndex, Program.gCurrentUser.Code);
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

                Program.Display_Info(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_SettingsSavedSuccessfully.ToString());
                Program.Get_FacilitySetup();
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

        #region radRoundingOption_SelectedIndexChanged
        private void radRoundingOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (radRoundingOption.SelectedIndex)
            {
                case 1:
                    {
                        chkRoundingStrictness.Enabled = true;
                        radMidpointOption.Enabled = true;
                    }
                    break;
                default:
                    {
                        chkRoundingStrictness.Enabled = false;
                        radMidpointOption.Enabled = false;

                        chkRoundingStrictness.Checked = false;
                        radMidpointOption.SelectedIndex = -1;
                    }
                    break;
            }
        }
        #endregion
    }
}