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
    public partial class frmBILDebtRequestedAmount : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        #region properties

        private string pPatientId = "";
        internal string PatientId
        {
            get { return pPatientId; }
            set { pPatientId = value; }
        }

        private string pReceiptNo = "";
        internal string ReceiptNo
        {
            get { return pReceiptNo; }
            set { pReceiptNo = value; }
        }

        private double pTotalPaid = 0;
        internal double TotalPaid
        {
            get { return pTotalPaid; }
            set { pTotalPaid = value; }
        }

        private bool pPaymentsDone = false;
        internal bool PaymentsDone
        {
            get { return pPaymentsDone; }
            set { pPaymentsDone = value; }
        }

        private string pReason = "";
        internal string Reason
        {
            get { return pReason; }
            set { pReason = value; }
        }

        #endregion

        #endregion

        #region frmBILDebtRequestedAmount
        public frmBILDebtRequestedAmount()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmBILDebtRequestedAmount";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                this.CancelButton = cmdClose;

                pMdtAutoCodes = (AfyaPro_MT.clsAutoCodes)Activator.GetObject(
                    typeof(AfyaPro_MT.clsAutoCodes),
                    Program.gMiddleTier + "clsAutoCodes");

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.bilpostbills_customizefinalpayment.ToString());
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmBILDebtRequestedAmount_Load
        private void frmBILDebtRequestedAmount_Load(object sender, EventArgs e)
        {
            this.Top = 0;

            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            txtPaidAmount.Text = pTotalPaid.ToString("c");

            if (pReceiptNo.Trim() != "")
            {
                txtReceiptNo.Text = pReceiptNo.Trim();

                if (txtReceiptNo.Text.Trim().ToLower() == "<<---New--->>")
                {
                    txtReceiptNo.Properties.ReadOnly = true;
                }
                else
                {
                    txtReceiptNo.Properties.ReadOnly = false;
                }
            }
            else
            {
                this.New_ReceiptNo();
            }

            this.Load_Controls();

            this.pFormWidth = this.Width;
            this.pFormHeight = this.Height;

            Program.Center_Screen(this);
        }
        #endregion

        #region New_ReceiptNo
        private void New_ReceiptNo()
        {
            string mFunctionName = "New_ReceiptNo";

            try
            {
                txtReceiptNo.Text = "";
                int mGenerateReceiptNo = pMdtAutoCodes.Auto_Generate_Code(
                        Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.debtreliefrequestno));
                if (mGenerateReceiptNo == -1)
                {
                    Program.Display_Server_Error("");
                    return;
                }

                if (mGenerateReceiptNo == 1)
                {
                    txtReceiptNo.Text = "<<---New--->>";
                    txtReceiptNo.Properties.ReadOnly = true;
                }
                else
                {
                    txtReceiptNo.Properties.ReadOnly = false;
                }
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

            mObjectsList.Add(txbPaidAmount);
            mObjectsList.Add(txbReceiptNo);
            mObjectsList.Add(txbTotalDue);
            mObjectsList.Add(cmdOk);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region cmdOk_Click
        private void cmdOk_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdOk_Click";

            #region validation

            if (txtReceiptNo.Text.Trim() == "" && txtReceiptNo.Text.Trim().ToLower() != "<<---new--->>")
            {
                Program.Display_Error("Please enter debt request #");
                txtReceiptNo.Focus();
                return;
            }

            if (Program.IsMoney(txtPaidAmount.Text) == false)
            {
                Program.Display_Error("Please enter a valid amount for debt request");
                txtPaidAmount.Focus();
                return;
            }

            if (Convert.ToDouble(txtPaidAmount.Text) <= 0)
            {
                Program.Display_Error("Please enter a valid amount for debt request");
                txtPaidAmount.Focus();
                return;
            }

            #endregion

            try
            {
                pReceiptNo = txtReceiptNo.Text.Trim();
                pPaymentsDone = true;
                pTotalPaid = Convert.ToDouble(txtPaidAmount.Text);
                pReason = txtReason.Text;

                this.Close();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmBILDebtRequestedAmount_Shown
        private void frmBILDebtRequestedAmount_Shown(object sender, EventArgs e)
        {
            txtPaidAmount.Focus();
        }
        #endregion
    }
}