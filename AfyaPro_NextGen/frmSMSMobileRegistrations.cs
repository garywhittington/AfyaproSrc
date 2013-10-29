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
    public partial class frmSMSMobileRegistrations : DevExpress.XtraEditors.XtraForm
    {
        
        private AfyaPro_MT.clsRegistrations pMdtRegistrations;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_MT.clsPatientExtraFields pMdtPatientExtraFields;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();
        private AfyaPro_MT.clsSMS pMdtSMS;
        
        private string pPhone ="";
        DataTable mDtExtraDetails = new DataTable("patientextradetails");

        #region frmSMSMobileRegistrations()
        public frmSMSMobileRegistrations()
        {
            pMdtSMS = (AfyaPro_MT.clsSMS)Activator.GetObject(
                 typeof(AfyaPro_MT.clsSMS),
                 Program.gMiddleTier + "clsSMS");

            pMdtRegistrations = (AfyaPro_MT.clsRegistrations)Activator.GetObject(
                   typeof(AfyaPro_MT.clsRegistrations),
                   Program.gMiddleTier + "clsRegistrations");

            InitializeComponent();
        }
        #endregion

        #region Load_Data
        private void Load_Data()
        {
            try
            {
                //load data

                DataTable mDtTrashSmS = new DataTable();
                mDtTrashSmS.Clear();
                grdMain.DataSource = mDtTrashSmS;
             
                mDtTrashSmS = pMdtSMS.Get_MobileRegistrations(0);

                
               

                foreach (DataColumn mCol in mDtTrashSmS.Columns)
                {

                    if (mCol.ColumnName == "autocode")
                    {
                        mDtTrashSmS.Columns["autocode"].ColumnName = "Auto Code";

                    }

                    if (mCol.ColumnName == "code")
                    {

                        mDtTrashSmS.Columns["code"].ColumnName = "Patient Code";

                    }
                    if (mCol.ColumnName == "surname")
                    {
                        mDtTrashSmS.Columns["surname"].ColumnName = "Surname";

                    }

                    if (mCol.ColumnName == "firstname")
                    {
                        mDtTrashSmS.Columns["firstname"].ColumnName = "First Name";

                    }

                    if (mCol.ColumnName == "birthdate")
                    {
                        mDtTrashSmS.Columns["birthdate"].ColumnName = "Date of Birth";

                    }

                    if (mCol.ColumnName == "regdate")
                    {
                        mDtTrashSmS.Columns["regdate"].ColumnName = "Reg. Date";

                    }
                    if (mCol.ColumnName == "district")
                    {
                        mDtTrashSmS.Columns["district"].ColumnName = "District";

                    }
                    if (mCol.ColumnName == "village")
                    {
                        mDtTrashSmS.Columns["village"].ColumnName = "Village";

                    }
                    if (mCol.ColumnName == "phone_num")
                    {
                        mDtTrashSmS.Columns["phone_num"].ColumnName = "Phone Number";

                    }

                    if (mCol.ColumnName == "edd")
                    {
                        mDtTrashSmS.Columns["edd"].ColumnName = "EDD";

                    }
                    if (mCol.ColumnName == "preg_age")
                    {
                        mDtTrashSmS.Columns["preg_age"].ColumnName = "Preg. Age";

                    }







                }

               // mDtTrashSmS.Columns.Remove("deletedby");

                mDtTrashSmS.AcceptChanges();

                if (mDtTrashSmS.Rows.Count > 0)
                {

                    grdMain.DataSource = mDtTrashSmS;
                    grdMain.Columns[0].Visible = false;
                    grdMain.Columns[9].Visible = false;
                    grdMain.Columns[10].Visible = false;
                    grdMain.Columns[11].Visible = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion

        #region frmMainGrid_Load
        private void frmMainGrid_Load(object sender, EventArgs e)
        {
            this.Load_Data();
        }
        #endregion

        #region frmMainGrid_FormClosing
        private void frmMainGrid_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //MemoryStream mMemoryStream = new MemoryStream();
                //grdMain.MainView.SaveLayoutToStream(mMemoryStream);

                //Program.gMdtFacilitySetup.Save_GridLayout(grdMain.Name, Program.gCurrentUser.Code, mMemoryStream.ToArray());
            }
            catch { }
        }
        #endregion

        #region grdMain_DoubleClick
        private void grdMain_DoubleClick(object sender, EventArgs e)
        {
            //try
            //{
            //    DevExpress.XtraGrid.Views.Grid.GridView mGridView =
            //        (DevExpress.XtraGrid.Views.Grid.GridView)grdMain.MainView;

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
        #endregion

        #region grdMain_KeyDown
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
        #endregion

        #region btnCommit_Click
        private void btnCommit_Click(object sender, EventArgs e)
        {
            try
            {
                  AfyaPro_Types.clsPatient mPatient = new AfyaPro_Types.clsPatient();
                  AfyaPro_Types.clsPatient mPatientCheck = new AfyaPro_Types.clsPatient();

                  if (grdMain.Rows.Count > 0)
                  {
                      int count = 0;
                      int PatientsSaved = 0;
                      DataTable mFakeTable = new DataTable();
                      DialogResult rst = MessageBox.Show("Do you want to save the imported patients?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                      if (rst == DialogResult.No)
                      {

                          grdMain.DataSource = mFakeTable;
                          return;
                      }
                      else
                      {

                          foreach (DataGridViewRow mRow in grdMain.Rows)
                          {
                             

                              string mPatientId = mRow.Cells[1].Value.ToString().Trim();
                              mPatientCheck = null;
                              mPatient = null;

                              if (mRow.Cells[8].Value.ToString() != "")
                              {

                                 
                                  if (mPatientId != "")
                                  {
                                    
                                      mPatientCheck = pMdtRegistrations.Get_Patient(mPatientId);
                                      mPatientId = mPatientCheck.code;
                                  }


                                  if (mPatientId == "")
                                  {
                                     
                                      mPatient = pMdtRegistrations.Add_Patient(1, "", mRow.Cells[2].Value.ToString(), mRow.Cells[3].Value.ToString(),
                                         "", "", Convert.ToDateTime(mRow.Cells[4].Value.ToString()), "", "", "",
                                    Convert.ToDateTime(mRow.Cells[5].Value.ToString()), "", "", mDtExtraDetails, Convert.ToDateTime(mRow.Cells[5].Value.ToString()), Program.gMachineName, Program.gMachineUser, Program.gCurrentUser.Code);
                                  }
                                  else
                                  {
                                       mPatient = mPatientCheck;
                                  }

                                  pPhone = mRow.Cells[8].Value.ToString();
                                  string mGroupCode = mRow.Cells[12].Value.ToString();
                                  
                                  pMdtSMS.Add_SMSPatient(mPatient.code, mPatient.firstname + " " + mPatient.surname, pPhone, mGroupCode);

                                  pMdtSMS.Update_MobileRegistrations(mRow.Cells[0].Value.ToString(), mPatient.code);
                                  PatientsSaved += 1;

                              }
                              else
                              {

                                  count += 1;
                              }
                          }

                      }

                      this.Load_Data();

                      MessageBox.Show("[" + PatientsSaved + "] patients successfully added to the list", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      if (count > 0)
                      {
                          MessageBox.Show(count + " patient(s) were not saved because phone number was missing", "Phone number", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      }
                      count = 0;
                      PatientsSaved = 0;
                      grdMain.DataSource = mFakeTable;
                      return;
                  }

            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}