/*
Copyright (C) 2013 AfyaPro Foundation

This file is part of AfyaPro.

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
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Linq;

namespace AfyaPro_MT
{
    public class clsDiagnoses : MarshalByRefObject
    {
        #region declaration

        private static String pClassName = "AfyaPro_MT.clsDiagnoses";

        #endregion

        #region View
        public DataTable View(String mFilter, String mOrder, string mLanguageName, string mGridName, bool mSearchICD = false)
        {
            String mFunctionName = "View";

            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();

            #region database connection

            try
            {
                mConn.ConnectionString = clsGlobal.gAfyaConStr;

                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                mCommand.Connection = mConn;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }

            #endregion

            try
            {
                string mTableName = "view_dxtdiagnoses";

                if (mSearchICD == true)
                {
                    mTableName = "view_dxticddiagnoses";
                }

                string mCommandText = "select * from " + mTableName;

                if (mFilter.Trim() != "")
                {
                    mCommandText = mCommandText + " where " + mFilter;
                }

                if (mOrder.Trim() != "")
                {
                    mCommandText = mCommandText + " order by " + mOrder;
                }
                mCommand.CommandText = mCommandText;

                DataTable mDataTable = new DataTable("view_dxticddiagnoses");
                mDataTable.RemotingFormat = SerializationFormat.Binary;
                mDataAdapter.SelectCommand = mCommand;
                mDataAdapter.Fill(mDataTable);

                #region column headers

                if (mLanguageName.Trim() != "" && mGridName.Trim() != "")
                {
                    try
                    {
                        var mCurrLang = from lang in System.Xml.Linq.XElement.Load(
                            AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Lang\" + mLanguageName + ".xml").Elements(mGridName)
                                        select lang;

                        foreach (var mElement in mCurrLang)
                        {
                            if (mDataTable.Columns.Contains((string)mElement.Element("controlname").Value.Trim().Substring(3)) == true)
                            {
                                mDataTable.Columns[(string)mElement.Element("controlname").Value.Trim().Substring(3)].Caption =
                                    (string)mElement.Element("description").Value.Trim();
                            }
                        }
                    }
                    catch { }
                }

                #endregion

                return mDataTable;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
            finally
            {
                mConn.Close();
            }
        }
        #endregion

        #region Get_MTUHA_Diagnoses
        public DataTable Get_MTUHA_Diagnoses()
        {
            string mFunctionName = "Get_MTUHA_Diagnoses";

            try
            {
                DataTable mDtMTUHADiagnoses = new DataTable("mtuhadiagnoses");
                mDtMTUHADiagnoses.Columns.Add("code", typeof(System.String));
                mDtMTUHADiagnoses.Columns.Add("description", typeof(System.String));

                DataRow mNewRow;

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.acflpr.ToString();
                mNewRow["description"] = "Acute Flaccid Paralysis";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.acrein.ToString();
                mNewRow["description"] = "Acute Respiratory Infection";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.anaoth.ToString();
                mNewRow["description"] = "Anaemia (Others)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.anasic.ToString();
                mNewRow["description"] = "Anaemia (Sickle cell)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.asthma.ToString();
                mNewRow["description"] = "Asthma";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.burns.ToString();
                mNewRow["description"] = "Burns";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.cardca.ToString();
                mNewRow["description"] = "Cardiovascular Diseases - (Cardiac Failure)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.cardhy.ToString();
                mNewRow["description"] = "Cardiovascular Diseases - (Hypertension)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.cardot.ToString();
                mNewRow["description"] = "Cardiovascular Diseases - (Others)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.choler.ToString();
                mNewRow["description"] = "Cholera";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.cliaid.ToString();
                mNewRow["description"] = "Clinical Aids";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.congdi.ToString();
                mNewRow["description"] = "Congenital Diseases";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.dentca.ToString();
                mNewRow["description"] = "Dental Diseases (Caries)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.dentot.ToString();
                mNewRow["description"] = "Dental Diseases (Others)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.dentpe.ToString();
                mNewRow["description"] = "Dental Diseases (Periodontal)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.diabme.ToString();
                mNewRow["description"] = "Diabetes Mellitus";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.diarba.ToString();
                mNewRow["description"] = "Diarrhoeal Diseases-(Bacterial)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.diarno.ToString();
                mNewRow["description"] = "Diarrhoeal Diseases-(Non-bacterial)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.dysent.ToString();
                mNewRow["description"] = "Dysentry";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.earinf.ToString();
                mNewRow["description"] = "Ear Infections";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.emorca.ToString();
                mNewRow["description"] = "Emergency Oral Care";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.epilep.ToString();
                mNewRow["description"] = "Epilepsy";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.eyeinf.ToString();
                mNewRow["description"] = "Eye Infections";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.fradis.ToString();
                mNewRow["description"] = "Fractures/Dislocations";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.gedisy.ToString();
                mNewRow["description"] = "Genital Discharge Syndrome (GDS)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.genulc.ToString();
                mNewRow["description"] = "Genital Ulcer Diseases (GUD)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.gynaot.ToString();
                mNewRow["description"] = "Gynaecological Disorders (Others)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.gynape.ToString();
                mNewRow["description"] = "Gynaecological Disorders (Pelvic Inflammatory Disease (PID))";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.haemea.ToString();
                mNewRow["description"] = "Haematological Diseases (except Anaemia)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.hephav.ToString();
                mNewRow["description"] = "Hepatitis (HAV)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.hephbv.ToString();
                mNewRow["description"] = "Hepatitis (HBV)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.hepoth.ToString();
                mNewRow["description"] = "Hepatitis (Others)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.iiides.ToString();
                mNewRow["description"] = "III Defined Symptoms or no Diagnosis";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.intest.ToString();
                mNewRow["description"] = "Intestinal Worms";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.lepros.ToString();
                mNewRow["description"] = "Leprosy";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.loboty.ToString();
                mNewRow["description"] = "Louse Borne Typhus (Relapsing) Fever";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.malsev.ToString();
                mNewRow["description"] = "Malaria-severe, complicated";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.malunc.ToString();
                mNewRow["description"] = "Malaria-uncomplicated";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.measle.ToString();
                mNewRow["description"] = "Measles";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.mening.ToString();
                mNewRow["description"] = "Meningitis";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.misuco.ToString();
                mNewRow["description"] = "Minor Surgical Conditions";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.neotet.ToString();
                mNewRow["description"] = "Neonatal Tetanus";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.neopla.ToString();
                mNewRow["description"] = "Neoplasms";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.neuros.ToString();
                mNewRow["description"] = "Neuroses";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.nigaot.ToString();
                mNewRow["description"] = "Non-infectious Gastrointestinal Diseases (Others)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.nigape.ToString();
                mNewRow["description"] = "Non-infectious Gastrointestinal Diseases (Peptic Ulcers)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.ninkid.ToString();
                mNewRow["description"] = "Non-infectious kidney diseases";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.ninski.ToString();
                mNewRow["description"] = "Non-Infectious Skin Diseases";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.nineye.ToString();
                mNewRow["description"] = "Non-Infectiuos Eye Diseases (except Vit A Defc)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.nskifu.ToString();
                mNewRow["description"] = "Non-skin Fungal Infections";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.nutrot.ToString();
                mNewRow["description"] = "Nutritional Disorders (Others)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.osteom.ToString();
                mNewRow["description"] = "Osteomyelitis";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.oeyeca.ToString();
                mNewRow["description"] = "Other eye diseases (Cataract)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.oeyeni.ToString();
                mNewRow["description"] = "Other eye non-inf eye diseases";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.penena.ToString();
                mNewRow["description"] = "Peri and neo-natal Conditions";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.plague.ToString();
                mNewRow["description"] = "Plague";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.pneumo.ToString();
                mNewRow["description"] = "Pneumonia";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.poison.ToString();
                mNewRow["description"] = "Poisoning";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.proenm.ToString();
                mNewRow["description"] = "Protein Energy Malnutrition";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.psycho.ToString();
                mNewRow["description"] = "Psychoses";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.raanbi.ToString();
                mNewRow["description"] = "Rabid Animal Bites";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.rabies.ToString();
                mNewRow["description"] = "Rabies";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.respot.ToString();
                mNewRow["description"] = "Respiratory Diseases (Others)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.rheuma.ToString();
                mNewRow["description"] = "Rheumatic Fever";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.rhejoin.ToString();
                mNewRow["description"] = "Rheumatoid/Joint Diseases";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.schist.ToString();
                mNewRow["description"] = "Schistosomiasis";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.sextra.ToString();
                mNewRow["description"] = "Sexually Transmitted Dis/Inf (Others)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.skiinf.ToString();
                mNewRow["description"] = "Skin diseases (infectious)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.skinin.ToString();
                mNewRow["description"] = "Skin diseases (non-infectious)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.sninbi.ToString();
                mNewRow["description"] = "Snake and Insect bites";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.thyroi.ToString();
                mNewRow["description"] = "Thyroid Diseases";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.tuberc.ToString();
                mNewRow["description"] = "Tuberculosis";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.typhoi.ToString();
                mNewRow["description"] = "Typhoid";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.urtrin.ToString();
                mNewRow["description"] = "Urinary Tract Infections (UTI)";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.vitade.ToString();
                mNewRow["description"] = "Vitamin A Def/Xerophthalmia";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.yellfe.ToString();
                mNewRow["description"] = "Yellow Fever";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                mNewRow = mDtMTUHADiagnoses.NewRow();
                mNewRow["code"] = AfyaPro_Types.clsEnums.MTUHA_Diagnoses.othdia.ToString();
                mNewRow["description"] = "Other Diagnoses";
                mDtMTUHADiagnoses.Rows.Add(mNewRow);
                mDtMTUHADiagnoses.AcceptChanges();

                return mDtMTUHADiagnoses;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
        }
        #endregion

        #region Add
        public AfyaPro_Types.clsResult Add(String mCode, String mDescription, String mGroupCode, string mSubGroupCode, string mUserId)
        {
            String mFunctionName = "Add";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;

            #region database connection

            try
            {
                mConn.ConnectionString = clsGlobal.gAfyaConStr;

                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                mCommand.Connection = mConn;
            }
            catch (Exception ex)
            {
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.dxtdiagnoses_add.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region check 4 duplicate
            try
            {
                mCommand.CommandText = "select * from dxticddiagnoses where code='"
                + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == true)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DxCodeIsInUse.ToString();
                    return mResult;
                }
            }
            catch (OdbcException ex)
            {
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion

            #region add
            try
            {
                //begin transaction
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //dxtdiagnoses
                mCommand.CommandText = "insert into dxtdiagnoses(code,description,groupcode,subgroupcode) values('"
                + mCode.Trim() + "','" + mDescription.Trim() + "','" + mGroupCode.Trim() + "','" + mSubGroupCode.Trim() + "')";
                mCommand.ExecuteNonQuery();

                //dxticddiagnoses
                mCommand.CommandText = "insert into dxticddiagnoses(code,description,groupcode,subgroupcode) values('"
                + mCode.Trim() + "','" + mDescription.Trim() + "','" + mGroupCode.Trim() + "','" + mSubGroupCode.Trim() + "')";
                mCommand.ExecuteNonQuery();

                //commit
                mTrans.Commit();

                //return
                return mResult;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Edit
        public AfyaPro_Types.clsResult Edit(String mCode, String mDescription, string mGroupCode, string mSubGroupCode, string mUserId)
        {
            String mFunctionName = "Edit";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;

            #region database connection

            try
            {
                mConn.ConnectionString = clsGlobal.gAfyaConStr;

                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                mCommand.Connection = mConn;
            }
            catch (Exception ex)
            {
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.dxtdiagnoses_edit.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region check for code existance

            try
            {
                mCommand.CommandText = "select * from dxticddiagnoses where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DxCodeDoesNotExist.ToString();
                    return mResult;
                }
            }
            catch (OdbcException ex)
            {
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }
            finally
            {
                mDataReader.Close();
            }

            #endregion

            #region do edit
            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                //dxtdiagnoses
                mCommand.CommandText = "update dxtdiagnoses set description = '"
                + mDescription.Trim() + "',groupcode='" + mGroupCode.Trim() + "', subgroupcode='"
                + mSubGroupCode.Trim() + "' where code='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                //dxticddiagnoses
                mCommand.CommandText = "update dxticddiagnoses set description = '"
                + mDescription.Trim() + "',groupcode='" + mGroupCode.Trim() + "', subgroupcode='"
                + mSubGroupCode.Trim() + "' where code='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                //dxtpatientdiagnoseslog
                mCommand.CommandText = "update dxtpatientdiagnoseslog set diagnosisdescription = '"
                + mDescription.Trim() + "' where diagnosiscode='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                mTrans.Commit();

                //return
                return mResult;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion

        #region Delete
        public AfyaPro_Types.clsResult Delete(String mCode, string mUserId)
        {
            String mFunctionName = "Delete";

            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();
            OdbcConnection mConn = new OdbcConnection();
            OdbcCommand mCommand = new OdbcCommand();
            OdbcDataReader mDataReader = null;
            OdbcTransaction mTrans = null;

            #region database connection

            try
            {
                mConn.ConnectionString = clsGlobal.gAfyaConStr;

                if (mConn.State != ConnectionState.Open)
                {
                    mConn.Open();
                }

                mCommand.Connection = mConn;
            }
            catch (Exception ex)
            {
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }

            #endregion

            #region check security for this function
            bool mGranted = clsGlobal.GrantDeny_FunctionAccess(mCommand,
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.dxtdiagnoses_delete.ToString(), mUserId);
            if (mGranted == false)
            {
                mResult.Exe_Result = 0;
                mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.GRL_AccessDeniedToSystemFunction.ToString();
                return mResult;
            }
            #endregion

            #region check 4 existance
            try
            {
                mCommand.CommandText = "select * from dxtdiagnoses where code='" + mCode.Trim() + "'";
                mDataReader = mCommand.ExecuteReader();
                if (mDataReader.Read() == false)
                {
                    mResult.Exe_Result = 0;
                    mResult.Exe_Message = AfyaPro_Types.clsSystemMessages.MessageIds.DXT_DxCodeDoesNotExist.ToString();
                    return mResult;
                }
            }
            catch (OdbcException ex)
            {
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }
            finally
            {
                mDataReader.Close();
            }
            #endregion

            #region do delete
            try
            {
                mTrans = mConn.BeginTransaction();
                mCommand.Transaction = mTrans;

                mCommand.CommandText = "delete from dxtdiagnoses where code='" + mCode.Trim() + "'";
                mCommand.ExecuteNonQuery();

                //commit
                mTrans.Commit();

                //return
                return mResult;
            }
            catch (OdbcException ex)
            {
                //rollback
                try { mTrans.Rollback(); }
                catch { }
                mResult.Exe_Result = -1;
                mResult.Exe_Message = clsGlobal.Write_Error(pClassName, mFunctionName, ex.Message);
                return mResult;
            }
            finally
            {
                mConn.Close();
            }
            #endregion
        }
        #endregion
    }
}
