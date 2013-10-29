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
    public partial class frmBILFinalPaymentDebtor : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_MT.clsCurrencies pMdtCurrencies;
        private AfyaPro_MT.clsPaymentTypes pMdtPaymentTypes;
        private AfyaPro_MT.clsDepositAccounts pMdtDepositAccounts;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataTable pDtAllCurrencies = new DataTable("currencies");
        private DataTable pDtAllPaymentTypes = new DataTable("paymenttypes");
        private DataTable pDtCurrencies = new DataTable("currencies");
        private DataTable pDtPaymentTypes = new DataTable("paymenttypes");
        private DataTable pDtAccounts = new DataTable("accounts");

        private int pFormWidth = 0;
        private int pFormHeight = 0;
        private bool pFirstTimeLoad = true;
        private double pTotalDue = 0;
        private double pPaidLocal = 0;
        private double pBalance = 0;
        private bool pAccountsVisible = false;

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

        private string pPayTypeCode = "";
        internal string PayTypeCode
        {
            get { return pPayTypeCode; }
            set { pPayTypeCode = value; }
        }

        private string pPayTypeDescription = "";
        internal string PayTypeDescription
        {
            get { return pPayTypeDescription; }
            set { pPayTypeDescription = value; }
        }

        private string pBank = "";
        internal string Bank
        {
            get { return pBank; }
            set { pBank = value; }
        }

        private string pBranch = "";
        internal string Branch
        {
            get { return pBranch; }
            set { pBranch = value; }
        }

        private string pHolder = "";
        internal string Holder
        {
            get { return pHolder; }
            set { pHolder = value; }
        }

        private string pChequeNo = "";
        internal string ChequeNo
        {
            get { return pChequeNo; }
            set { pChequeNo = value; }
        }

        private Int16 pCheckDepositBalance = 0;
        internal Int16 CheckDepositBalance
        {
            get { return pCheckDepositBalance; }
            set { pCheckDepositBalance = value; }
        }

        private string pAccountCode = "";
        internal string AccountCode
        {
            get { return pAccountCode; }
            set { pAccountCode = value; }
        }

        private string pAccountDescription = "";
        internal string AccountDescription
        {
            get { return pAccountDescription; }
            set { pAccountDescription = value; }
        }

        private bool pPaymentsDone = false;
        internal bool PaymentsDone
        {
            get { return pPaymentsDone; }
            set { pPaymentsDone = value; }
        }

        private bool pPostingBill = false;
        internal bool PostingBill
        {
            get { return pPostingBill; }
            set { pPostingBill = value; }
        }

        #endregion

        #endregion

        #region frmBILFinalPaymentDebtor
        public frmBILFinalPaymentDebtor(double mTotalDue, bool mAccountsVisible)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmBILFinalPaymentDebtor";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                this.CancelButton = cmdClose;

                this.pTotalDue = mTotalDue;
                this.pAccountsVisible = mAccountsVisible;

                pMdtCurrencies = (AfyaPro_MT.clsCurrencies)Activator.GetObject(
                    typeof(AfyaPro_MT.clsCurrencies),
                    Program.gMiddleTier + "clsCurrencies");

                pMdtPaymentTypes = (AfyaPro_MT.clsPaymentTypes)Activator.GetObject(
                    typeof(AfyaPro_MT.clsPaymentTypes),
                    Program.gMiddleTier + "clsPaymentTypes");

                pMdtDepositAccounts = (AfyaPro_MT.clsDepositAccounts)Activator.GetObject(
                    typeof(AfyaPro_MT.clsDepositAccounts),
                    Program.gMiddleTier + "clsDepositAccounts");

                pMdtAutoCodes = (AfyaPro_MT.clsAutoCodes)Activator.GetObject(
                    typeof(AfyaPro_MT.clsAutoCodes),
                    Program.gMiddleTier + "clsAutoCodes");

                pDtCurrencies.Columns.Add("code", typeof(System.String));
                pDtCurrencies.Columns.Add("description", typeof(System.String));
                pDtCurrencies.Columns.Add("currencysymbol", typeof(System.String));
                pDtCurrencies.Columns.Add("exchangerate", typeof(System.Double));
                cboCurrency.Properties.DataSource = pDtCurrencies;
                cboCurrency.Properties.DisplayMember = "description";
                cboCurrency.Properties.ValueMember = "code";

                pDtPaymentTypes.Columns.Add("code", typeof(System.String));
                pDtPaymentTypes.Columns.Add("description", typeof(System.String));
                cboPaymentType.Properties.DataSource = pDtPaymentTypes;
                cboPaymentType.Properties.DisplayMember = "description";
                cboPaymentType.Properties.ValueMember = "code";

                pDtAccounts.Columns.Add("code", typeof(System.String));
                pDtAccounts.Columns.Add("description", typeof(System.String));
                pDtAccounts.Columns.Add("balance", typeof(System.Double));
                cboAccount.Properties.DataSource = pDtAccounts;
                cboAccount.Properties.DisplayMember = "description";
                cboAccount.Properties.ValueMember = "code";

                this.Fill_LookupData();

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

        #region frmBILFinalPaymentDebtor_Load
        private void frmBILFinalPaymentDebtor_Load(object sender, EventArgs e)
        {
            this.Top = 0;

            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            pBalance = pTotalPaid - pTotalDue;
            txtTotalDue.Text = Program.gLocalCurrencySymbol + pTotalDue.ToString("c");
            txtTotalPaid.Text = Program.gLocalCurrencySymbol + pTotalPaid.ToString("c");
            txtBalance.Text = Program.gLocalCurrencySymbol + pBalance.ToString("c");

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

            cboCurrency.ItemIndex = Program.Get_LookupItemIndex(cboCurrency, "code", Program.gLocalCurrencyCode);
            cboPaymentType.ItemIndex = Program.Get_LookupItemIndex(cboPaymentType, "code", Program.gCurrentUser.DefaultPaymentTypeCode);
            cboPaymentType.Enabled = Program.GrantDeny_FunctionAccess(
                AfyaPro_Types.clsSystemFunctions.FunctionKeys.bilpostbills_changepaymenttype.ToString());

            this.Load_Controls();

            this.pFormWidth = this.Width;
            this.pFormHeight = this.Height;

            Program.Center_Screen(this);

            if (cboPaymentType.ItemIndex != -1)
            {
                txtPaidAmount.Focus();
            }
            else
            {
                cboPaymentType.Focus();
            }
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
                        Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.receiptno));
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

            mObjectsList.Add(txbCurrency);
            mObjectsList.Add(txbExchangeRate);
            mObjectsList.Add(txbPaymentType);
            mObjectsList.Add(txbBank);
            mObjectsList.Add(txbBranch);
            mObjectsList.Add(txbChequeNo);
            mObjectsList.Add(txbPaidAmount);
            mObjectsList.Add(txbReceiptNo);
            mObjectsList.Add(txbTotalDue);
            mObjectsList.Add(txbTotalPaid);
            mObjectsList.Add(txbBalance);
            mObjectsList.Add(cmdOk);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }
        #endregion

        #region Fill_Accounts
        private void Fill_Accounts()
        {
            string mFunctionName = "Fill_Accounts";

            try
            {
                pDtAccounts.Rows.Clear();

                DataTable mDtAccountMembers = pMdtDepositAccounts.View_Members(
                    "membercode='" + pPatientId.Trim() + "'", "", "", "");
                DataTable mDtAccounts = pMdtDepositAccounts.View("", "", Program.gLanguageName, "grdBILDepositAccounts");

                DataView mDvAccountMembers = new DataView();
                mDvAccountMembers.Table = mDtAccountMembers;
                mDvAccountMembers.Sort = "accountcode";

                foreach (DataRow mDataRow in mDtAccounts.Rows)
                {
                    if (mDvAccountMembers.Find(mDataRow["code"].ToString().Trim()) >= 0)
                    {
                        DataRow mNewRow = pDtAccounts.NewRow();
                        mNewRow["code"] = mDataRow["code"].ToString();
                        mNewRow["description"] = mDataRow["description"].ToString();
                        mNewRow["balance"] = Convert.ToDouble(mDataRow["balance"]);
                        pDtAccounts.Rows.Add(mNewRow);
                        pDtAccounts.AcceptChanges();
                    }
                }

                foreach (DataColumn mDataColumn in pDtAccounts.Columns)
                {
                    mDataColumn.Caption = mDtAccounts.Columns[mDataColumn.ColumnName].Caption;
                }

                if (pDtAccounts.Rows.Count == 1)
                {
                    cboAccount.ItemIndex = 0;
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
            DataRow mNewRow;
            string mFunctionName = "Fill_LookupData";

            try
            {
                #region Currencies

                pDtCurrencies.Rows.Clear();
                pDtAllCurrencies = pMdtCurrencies.View("", "code", Program.gLanguageName, "grdBLSCurrencies");
                foreach (DataRow mDataRow in pDtAllCurrencies.Rows)
                {
                    mNewRow = pDtCurrencies.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    mNewRow["currencysymbol"] = mDataRow["currencysymbol"].ToString();
                    mNewRow["exchangerate"] = Convert.ToDouble(mDataRow["exchangerate"]);
                    pDtCurrencies.Rows.Add(mNewRow);
                    pDtCurrencies.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtCurrencies.Columns)
                {
                    mDataColumn.Caption = pDtAllCurrencies.Columns[mDataColumn.ColumnName].Caption;
                }

                #endregion

                #region PaymentTypes

                pDtPaymentTypes.Rows.Clear();
                pDtAllPaymentTypes = pMdtPaymentTypes.View("", "code", Program.gLanguageName, "grdBLSPaymentTypes");
                foreach (DataRow mDataRow in pDtAllPaymentTypes.Rows)
                {
                    if (Convert.ToInt16(mDataRow["visibilitydebtorpayments"]) == 1)
                    {
                        if (pAccountsVisible == false)
                        {
                            if (Convert.ToInt16(mDataRow["checkdepositbalance"]) != 1)
                            {
                                mNewRow = pDtPaymentTypes.NewRow();
                                mNewRow["code"] = mDataRow["code"].ToString();
                                mNewRow["description"] = mDataRow["description"].ToString();
                                pDtPaymentTypes.Rows.Add(mNewRow);
                                pDtPaymentTypes.AcceptChanges();
                            }
                        }
                        else
                        {
                            mNewRow = pDtPaymentTypes.NewRow();
                            mNewRow["code"] = mDataRow["code"].ToString();
                            mNewRow["description"] = mDataRow["description"].ToString();
                            pDtPaymentTypes.Rows.Add(mNewRow);
                            pDtPaymentTypes.AcceptChanges();
                        }
                    }
                }

                foreach (DataColumn mDataColumn in pDtPaymentTypes.Columns)
                {
                    mDataColumn.Caption = pDtAllPaymentTypes.Columns[mDataColumn.ColumnName].Caption;
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

        #region cboCurrency_EditValueChanged
        private void cboCurrency_EditValueChanged(object sender, EventArgs e)
        {
            string mFunctionName = "cboCurrency_EditValueChanged";

            try
            {
                txtExchangeRate.Text = "";

                if (cboCurrency.ItemIndex == -1)
                {
                    return;
                }

                string mCurrencyCode = cboCurrency.GetColumnValue("code").ToString().Trim();

                DataView mDvCurrencies = new DataView();
                mDvCurrencies.Table = pDtAllCurrencies;
                mDvCurrencies.Sort = "code";

                int mRowIndex = mDvCurrencies.Find(mCurrencyCode);
                if (mRowIndex >= 0)
                {
                    txtExchangeRate.Text = mDvCurrencies[mRowIndex]["exchangerate"].ToString();
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cboPaymentType_EditValueChanged
        private void cboPaymentType_EditValueChanged(object sender, EventArgs e)
        {
            string mFunctionName = "cboPaymentType_EditValueChanged";

            try
            {
                txtBank.Text = "";
                txtBranch.Text = "";
                txtChequeNo.Text = "";
                txtBank.Properties.ReadOnly = true;
                txtBranch.Properties.ReadOnly = true;
                txtChequeNo.Properties.ReadOnly = true;
                cboAccount.EditValue = null;
                cboAccount.Properties.ReadOnly = true;
                txtAccountBalance.Text = Convert.ToDouble("0").ToString("c");

                if (cboPaymentType.ItemIndex == -1)
                {
                    return;
                }

                string mPaymentTypeCode = cboPaymentType.GetColumnValue("code").ToString().Trim();

                DataView mDvPaymentTypes = new DataView();
                mDvPaymentTypes.Table = pDtAllPaymentTypes;
                mDvPaymentTypes.Sort = "code";

                int mRowIndex = mDvPaymentTypes.Find(mPaymentTypeCode);
                if (mRowIndex >= 0)
                {
                    if (Convert.ToInt16(mDvPaymentTypes[mRowIndex]["ischeque"]) == 1)
                    {
                        txtBank.Properties.ReadOnly = false;
                        txtBranch.Properties.ReadOnly = false;
                        txtChequeNo.Properties.ReadOnly = false;
                        txtBank.Focus();
                        txtBank.SelectAll();
                    }
                    else
                    {
                        txtPaidAmount.Focus();
                        txtPaidAmount.SelectAll();
                    }

                    if (Convert.ToInt16(mDvPaymentTypes[mRowIndex]["checkdepositbalance"]) == 1)
                    {
                        cboAccount.Properties.ReadOnly = false;

                        this.Fill_Accounts();

                        cboAccount.Focus();
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

        #region txtPaidAmount_EditValueChanged
        private void txtPaidAmount_EditValueChanged(object sender, EventArgs e)
        {
            double mForeignAmt = 0;
            double mLocalAmt = 0;
            string mFunctionName = "txtPaidAmount_EditValueChanged";

            if (cboCurrency.ItemIndex == -1)
            {
                Program.Display_Error("Invalid Currency");
                cboCurrency.Focus();
                return;
            }

            if (Program.IsMoney(txtExchangeRate.Text) == false)
            {
                Program.Display_Error("Invalid exchange rate");
                txtExchangeRate.Focus();
                return;
            }

            if (cboPaymentType.ItemIndex == -1)
            {
                Program.Display_Error("Invalid Payment Type");
                cboPaymentType.Focus();
                return;
            }

            if (Program.IsMoney(txtPaidAmount.Text) == false)
            {
                Program.Display_Error("Invalid Paid Amount");
                txtPaidAmount.Focus();
                return;
            }

            try
            {
                double mExchangeRate = Convert.ToDouble(txtExchangeRate.Text);
                if (Program.IsMoney(txtPaidAmount.Text) == true)
                {
                    mForeignAmt = Convert.ToDouble(txtPaidAmount.Text);
                }

                mLocalAmt = mForeignAmt * mExchangeRate;
                pPaidLocal = mLocalAmt;
                txtPaidLocal.Text = Program.gLocalCurrencySymbol + mLocalAmt.ToString("c");
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cboAccount_EditValueChanged
        private void cboAccount_EditValueChanged(object sender, EventArgs e)
        {
            double mAccountBalance = Convert.ToDouble(cboAccount.GetColumnValue("balance"));

            txtAccountBalance.Text = mAccountBalance.ToString("c");
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

            if (cboCurrency.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_CurrencyIsInvalid.ToString());
                cboCurrency.Focus();
                return;
            }

            if (Program.IsMoney(txtExchangeRate.Text) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_ExchangeRateIsInvalid.ToString());
                txtExchangeRate.Focus();
                return;
            }

            if (cboPaymentType.ItemIndex == -1)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_PaymentTypeIsInvalid.ToString());
                cboPaymentType.Focus();
                return;
            }

            if (Program.IsMoney(txtPaidAmount.Text) == false || pPaidLocal <= 0)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_PaidAmountIsInvalid.ToString());
                txtPaidAmount.Focus();
                return;
            }

            #endregion

            if (txtReceiptNo.Text.Trim() == "" && txtReceiptNo.Text.Trim().ToLower() != "<<---new--->>")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_ReceiptNoIsInvalid.ToString());
                txtReceiptNo.Focus();
                return;
            }

            if (pPaidLocal > pTotalDue)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_PaymentTypeDoesNotAllowOverPaying.ToString());
                txtPaidAmount.Focus();
                return;
            }

            try
            {
                pPayTypeCode = cboPaymentType.GetColumnValue("code").ToString().Trim();
                pPayTypeDescription = cboPaymentType.GetColumnValue("description").ToString().Trim();
                pBank = txtBank.Text;
                pBranch = txtBranch.Text;
                pHolder = "";
                pChequeNo = txtBranch.Text;
                pCheckDepositBalance = 0;
                pAccountCode = "";
                pAccountDescription = "";
                pTotalPaid = pPaidLocal;

                #region validation specific to payment types

                DataView mDvPaymentTypes = new DataView();
                mDvPaymentTypes.Table = pDtAllPaymentTypes;
                mDvPaymentTypes.Sort = "code";

                int mRowIndex = mDvPaymentTypes.Find(cboPaymentType.GetColumnValue("code").ToString().Trim());

                if (mRowIndex >= 0)
                {
                    double mPendingForPayment = -pBalance;

                    pCheckDepositBalance = Convert.ToInt16(mDvPaymentTypes[mRowIndex]["checkdepositbalance"]);

                    //check deposit balance
                    if (pCheckDepositBalance == 1)
                    {
                        if (cboAccount.ItemIndex == -1)
                        {
                            Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DepositAccountDescriptionIsInvalid.ToString());
                            cboAccount.Focus();
                            return;
                        }

                        double mDepositBalance = 0;
                        int mAllowOverDraft = 0;

                        pAccountCode = cboAccount.GetColumnValue("code").ToString().Trim();
                        pAccountDescription = cboAccount.GetColumnValue("description").ToString().Trim();
                        DataTable mDtAccounts = pMdtDepositAccounts.View("code='" + pAccountCode + "'", "", "", "");
                        if (mDtAccounts.Rows.Count > 0)
                        {
                            mDepositBalance = Convert.ToDouble(mDtAccounts.Rows[0]["balance"]);
                            mAllowOverDraft = Convert.ToInt16(mDtAccounts.Rows[0]["allowoverdraft"]);
                        }

                        if (pPaidLocal > mDepositBalance)
                        {
                            if (mAllowOverDraft == 0)
                            {
                                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_AccountDoesNotAllowOverDraft.ToString());
                                txtPaidAmount.Focus();
                                return;
                            }
                            else
                            {
                                DialogResult mDlgResult = Program.Display_Question(
                                    AfyaPro_Types.clsSystemMessages.MessageIds.BIL_ConfirmAccountOverDraft.ToString(), MessageBoxDefaultButton.Button2);
                                if (mDlgResult != DialogResult.Yes)
                                {
                                    txtPaidAmount.Focus();
                                    return;
                                }
                            }
                        }
                    }
                }

                #endregion

                pReceiptNo = txtReceiptNo.Text.Trim();
                pPaymentsDone = true;
                this.Close();
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