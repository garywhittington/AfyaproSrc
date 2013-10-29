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
using DevExpress.XtraEditors.Controls;

namespace AfyaPro_NextGen
{
    public partial class frmSMSSendMessage : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsSMS pMdtSMS;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";
        private string pSendTo = "";
        private DataTable pDtClientGroups = new DataTable("groups");
        private DataTable pDtTemplates = new DataTable("templates");
               

        private int pFormWidth = 0;
        private int pFormHeight = 0;
        private static string pPhoneNumber = "";

        #endregion

        #region frmSMSSendMessage
        public frmSMSSendMessage()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmSMSSendMessage";
            this.KeyDown += new KeyEventHandler(frmIPDAdmit_KeyDown);

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                
                this.CancelButton = cmdClose;
                pMdtSMS = (AfyaPro_MT.clsSMS)Activator.GetObject(
                   typeof(AfyaPro_MT.clsSMS),
                   Program.gMiddleTier + "clsSMS");

                pDtClientGroups.Columns.Add("Code", typeof(System.String));
                pDtClientGroups.Columns.Add("Group", typeof(System.String));
                pDtClientGroups.Columns.Add("Description", typeof(System.String));
                cboClientGroup.Properties.DisplayMember = "Description";
                cboClientGroup.Properties.ValueMember = "Code";
                cboClientGroup.Properties.BestFitMode = BestFitMode.BestFit;
                cboClientGroup.Properties.BestFit();

                pDtTemplates.Columns.Add("MessageCode", typeof(System.String));
                pDtTemplates.Columns.Add("Message", typeof(System.String));
                cboMessageCode.Properties.DisplayMember = "MessageCode";
                cboMessageCode.Properties.ValueMember = "MessageCode";
                cboMessageCode.Properties.BestFitMode = BestFitMode.BestFit;
                cboMessageCode.Properties.BestFit();
                
               

                this.Fill_LookupData();

               
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(txbWard);
            mObjectsList.Add(txbRoom);
            mObjectsList.Add(txbBedNo);
            mObjectsList.Add(txbRemarks);
            //mObjectsList.Add(txbWeight);
            //mObjectsList.Add(txbTemperature);
            mObjectsList.Add(cmdOk);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region frmSMSSendMessage
        private void frmIPDAdmit_Load(object sender, EventArgs e)
        {
            string mFunctionName = "frmSMSSendMessage_Load";

            try
            {
                               
                Program.Restore_FormLayout(layoutControl1, this.Name);
                Program.Restore_FormSize(this);

                this.pFormWidth = this.Width;
                this.pFormHeight = this.Height;

                cboClientGroup.EditValue = "";
                cboClientGroup.Enabled = false;

                this.Load_Controls();

                Program.Center_Screen(this);

                this.Append_ShortcutKeys();

            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmIPDAdmit_FormClosing
        private void frmIPDAdmit_FormClosing(object sender, FormClosingEventArgs e)
        {
            //layout
            if (layoutControl1.IsModified == true)
            {
                Program.Save_FormLayout(this, layoutControl1, this.Name);
            }
        }
        #endregion

        #region Append_ShortcutKeys
        private void Append_ShortcutKeys()
        {
            cmdOk.Text = cmdOk.Text + " (" + Program.KeyCode_Ok.ToString() + ")";
        }
        #endregion

        #region InputBox
        public static DialogResult InputBox(string title, string promptText, string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            pPhoneNumber = value;

            
            return dialogResult;
        }
        #endregion
        
        #region Fill_LookupData
        private void Fill_LookupData()
        {
            DataRow mNewRow;
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region Groups

                pDtClientGroups.Rows.Clear();
                DataTable mDtGroups = pMdtSMS.Get_PatientGroups();
               
                foreach (DataRow mDataRow in mDtGroups.Rows)
                {
                    mNewRow = pDtClientGroups.NewRow();
                    mNewRow["Code"] = mDataRow["code"].ToString();
                    mNewRow["Group"] = mDataRow["colgroup"].ToString();
                    mNewRow["Description"] = mDataRow["description"].ToString();
                    pDtClientGroups.Rows.Add(mNewRow);
                    pDtClientGroups.AcceptChanges();
                    
                }
                                

                cboClientGroup.Properties.DataSource = pDtClientGroups;
               

                #endregion

                #region MessageTemplates

                pDtTemplates.Rows.Clear();
                DataTable mDtTemplates = pMdtSMS.View_MessageTemplates("", "", Program.gLanguageName, "", true); ;

                foreach (DataRow mDataRow in mDtTemplates.Rows)
                {
                    mNewRow = pDtTemplates.NewRow();
                    mNewRow["MessageCode"] = mDataRow["messagecode"].ToString();
                    mNewRow["Message"] = mDataRow["message"].ToString();
                    pDtTemplates.Rows.Add(mNewRow);
                    pDtTemplates.AcceptChanges();

                }


                cboMessageCode.Properties.DataSource = pDtTemplates;


                #endregion
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cboWard_EditValueChanged
        void cboWard_EditValueChanged(object sender, EventArgs e)
        {
            string mFunctionName = "cboWard_EditValueChanged";

            try
            {
                //pDtRooms.Rows.Clear();
                //pDtBeds.Rows.Clear();
                //if (cboMessageSource.ItemIndex == -1)
                //{
                //    return;
                //}

                //string mWardCode = cboMessageSource.GetColumnValue("code").ToString().Trim();

                //DataTable mDtRooms = pMdtWardRooms.View(
                //    "wardcode='" + mWardCode + "'", "code", Program.gLanguageName, "grdIPDWardRooms");
                //foreach (DataRow mDataRow in mDtRooms.Rows)
                //{
                //    DataRow mNewRow = pDtRooms.NewRow();
                //    mNewRow["code"] = mDataRow["code"].ToString();
                //    mNewRow["description"] = mDataRow["description"].ToString();
                //    pDtRooms.Rows.Add(mNewRow);
                //    pDtRooms.AcceptChanges();
                //}

                //foreach (DataColumn mDataColumn in pDtRooms.Columns)
                //{
                //    mDataColumn.Caption = mDtRooms.Columns[mDataColumn.ColumnName].Caption;
                //}
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion
              
        #region Ok
        private void Ok()
        {
            string mFunctionName = "Ok";
            try
            {
                
                #region validation



                if (cboClientGroup.ItemIndex == -1 && pSendTo == "group")
                {
                    Program.Display_Error("Select client group");
                    cboClientGroup.Focus();
                    return;
                }

                if (cboMessageSource.Text == "")
                {
                    Program.Display_Error("Select message source");
                    cboMessageSource.Focus();
                    return;
                }

                if (cboMessageSource.Text == "From Templates" && cboMessageCode.Text == "")
                {
                    Program.Display_Error("Select message from the list");
                    cboMessageCode.Focus();
                    return;
                }

                if (txtMessage.Text.Trim() == "")
                {
                    Program.Display_Error("There is no message to send");
                    txtMessage.Focus();
                    return;
                }

                if (lstRecepients.Items.Count == 0)
                {
                    Program.Display_Error("There are no message recepients");
                    txtMessage.Focus();
                    return;
                }
                
                #endregion

                #region sendmessagetooutbox
                string[] mPhoneNumbers = new string[lstRecepients.Items.Count];
                int index = 0;
                for (index = 0; index <= lstRecepients.Items.Count -1; index++)
                {
                    mPhoneNumbers.SetValue(lstRecepients.Items[index], index);
                }

                pMdtSMS.Add_ToOutBox(cboMessageCode.Text, txtMessage.Text, mPhoneNumbers);
                MessageBox.Show("Message(s) successfully queued for sending", "Sending", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboClientGroup.ResetText();
                //DataTable mTb = new DataTable();
                //cboMessageCode.Properties.DataSource = mTb;
                //cboMessageSource.ResetText();
                txtMessage.Reset();
                lstRecepients.Items.Clear();


                #endregion
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
            this.Ok();
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region frmIPDAdmit_KeyDown
        void frmIPDAdmit_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Program.KeyCode_Ok:
                    {
                        this.Ok();
                    }
                    break;
            }
        }
        #endregion

        #region cboMessageSource_SelectedIndexChanged
        private void cboMessageSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
            if (cboMessageSource.Text == "From Templates")
            {
                 
                cboMessageCode.Enabled = true;
            }
            else
            {
               
                cboMessageCode.Enabled = false;
            }
        }
        #endregion

        #region cboMessageCode_EditValueChanged

        private void cboMessageCode_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
               
                txtMessage.Text = cboMessageCode.GetColumnValue("Message").ToString().Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region btnAdd_Click
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
               InputBox("Phone", "Destination phone number", "");
               pPhoneNumber = pPhoneNumber.Trim();
               
               if (pPhoneNumber.Length < 10)
               {
                   MessageBox.Show("Invalid phone number", "Phone number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   return;
               }

               if (pPhoneNumber.Substring(0, 1) == "+" && pPhoneNumber.Length < 13)
               {
                   MessageBox.Show("Invalid phone number", "Phone number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   return;
               }
               
               if (pPhoneNumber.Substring(0, 1) != "0" && pPhoneNumber.Substring(0, 1) != "+")
               {
                   MessageBox.Show("Invalid phone number", "Phone number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   return;
               }
                                              

              
               lstRecepients.Items.Add(pPhoneNumber.ToString());
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region rdbGroup_SelectedIndexChanged

        private void rdbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pSendTo = "";
                if (rdbGroup.SelectedIndex == 0) 
                {
                    cboClientGroup.EditValue = "";
                    cboClientGroup.Enabled = false;
                    pSendTo = "individual";
                }
                else if (rdbGroup.SelectedIndex == 1)
                {
                    cboClientGroup.EditValue = "";
                    cboClientGroup.Enabled = true;
                    pSendTo = "group";
                }
                else if (rdbGroup.SelectedIndex == 2)
                {

                    try
                    {
                        lstRecepients.Items.Clear();
                        cboClientGroup.Enabled = false;
                        DataTable mDtGroupClients = new DataTable();
                        mDtGroupClients.Clear();
                        mDtGroupClients = pMdtSMS.Get_GroupClients("");
                        
                        foreach (DataRow mDataRow in mDtGroupClients.Rows)
                        {
                            lstRecepients.Items.Add(mDataRow["phonenumber"].ToString());
                        }


                    }
                    catch (Exception ex)
                    {
                       
                        return;
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion

        #region cboClientGroup_EditValueChanged
        private void cboClientGroup_EditValueChanged(object sender, EventArgs e)
        {
            string mFunctionName = "cboGroup_EditValueChanged";

            try
            {
                lstRecepients.Items.Clear();
                string mGroupCode = cboClientGroup.GetColumnValue("Code").ToString().Trim();

                 DataTable mDtGroupClients = new DataTable();
                if (pSendTo == "group")
                {
                    mGroupCode = " where groupcode = '" + mGroupCode + "'";
                    mDtGroupClients = pMdtSMS.Get_GroupClients(mGroupCode);
                }

                if (pSendTo == "all")
                {
                    mGroupCode = "";
                    mDtGroupClients = pMdtSMS.Get_GroupClients(mGroupCode);
                }
         
              

                foreach (DataRow mDataRow in mDtGroupClients.Rows)
                {
                   lstRecepients.Items.Add(mDataRow["phonenumber"].ToString());
                }


            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region btnRemove_Click
        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                
                    lstRecepients.Items.RemoveAt(lstRecepients.SelectedIndex);
                
            }
            catch (Exception)
            {
                return;
            }

        }
        #endregion
    }
}