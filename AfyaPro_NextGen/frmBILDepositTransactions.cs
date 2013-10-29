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
using DevExpress.XtraEditors.Controls;

namespace AfyaPro_NextGen
{
    public partial class frmBILDepositTransactions : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsDepositAccounts pMdtDepositAccounts;
        private AfyaPro_MT.clsAutoCodes pMdtAutoCodes;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        internal string gDataState = "";
        private bool pFirstTimeLoad = true;

        private DataTable pDtAccounts = new DataTable("accounts");
        private DataTable pDtTransactions = new DataTable("transactions");
        private ComboBoxItemCollection pFromToWhom;
        private DevExpress.XtraEditors.XtraForm pForm;

        #endregion

        #region frmBILDepositTransactions
        public frmBILDepositTransactions(DevExpress.XtraEditors.XtraForm mForm)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmBILDepositTransactions";

            try
            {
                this.Icon = Program.gMdiForm.Icon;
                this.CancelButton = cmdClose;
                pForm = mForm;

                pMdtDepositAccounts = (AfyaPro_MT.clsDepositAccounts)Activator.GetObject(
                    typeof(AfyaPro_MT.clsDepositAccounts),
                    Program.gMiddleTier + "clsDepositAccounts");

                pMdtAutoCodes = (AfyaPro_MT.clsAutoCodes)Activator.GetObject(
                    typeof(AfyaPro_MT.clsAutoCodes),
                    Program.gMiddleTier + "clsAutoCodes");

                pDtTransactions.Columns.Add("transdate", typeof(System.DateTime));
                pDtTransactions.Columns.Add("reference", typeof(System.String));
                pDtTransactions.Columns.Add("transdescription", typeof(System.String));
                pDtTransactions.Columns.Add("fromwhomtowhom", typeof(System.String));
                pDtTransactions.Columns.Add("creditamount", typeof(System.Double));
                pDtTransactions.Columns.Add("debitamount", typeof(System.Double));

                pDtAccounts.Columns.Add("code", typeof(System.String));
                pDtAccounts.Columns.Add("description", typeof(System.String));
                pDtAccounts.Columns.Add("balance", typeof(System.Double));
                cboDestinationAccount.Properties.DataSource = pDtAccounts;
                cboDestinationAccount.Properties.DisplayMember = "description";
                cboDestinationAccount.Properties.ValueMember = "code";
                cboDestinationAccount.Properties.BestFit();

                grdTransactions.DataSource = pDtTransactions;

                pFromToWhom = cboFromToWhom.Properties.Items;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region frmBILDepositTransactions_Load
        private void frmBILDepositTransactions_Load(object sender, EventArgs e)
        {
            this.Top = 0;

            for (int mIndex = 0; mIndex < viewTransactions.Columns.Count; mIndex++)
            {
                switch (viewTransactions.Columns[mIndex].ColumnType.ToString().ToLower())
                {
                    case "system.double":
                        {
                            viewTransactions.Columns[mIndex].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            viewTransactions.Columns[mIndex].DisplayFormat.FormatString = "{0:c}";

                            viewTransactions.Columns[mIndex].SummaryItem.DisplayFormat = "{0:c}";
                        }
                        break;
                }
            }

            this.Load_Controls();
            this.Fill_Accounts();
            radEntrySide.SelectedIndex = -1;
            txtDate.EditValue = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);
            this.Fill_Transactions();
            this.Fill_FromToWhom();
            this.Mode_New();
            radEntrySide.SelectedIndex = 0;
        }
        #endregion

        #region Mode_New
        private void Mode_New()
        {
            Int16 mGenerateCode = pMdtAutoCodes.Auto_Generate_Code(
                Convert.ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.deposittransactionid));
            if (mGenerateCode == -1)
            {
                Program.Display_Server_Error("");
                return;
            }

            txtTransactionId.Text = "";

            if (mGenerateCode == 1)
            {
                txtTransactionId.Text = "<<---New--->>";
                txtTransactionId.Enabled = false;
                cboFromToWhom.Focus();
            }
            else
            {
                txtTransactionId.Enabled = true;
                txtTransactionId.Focus();
            }
        }
        #endregion

        #region frmBILDepositTransactions_Activated
        private void frmBILDepositTransactions_Activated(object sender, EventArgs e)
        {
            if (pFirstTimeLoad == true)
            {
                if (gDataState.Trim().ToLower() == "new")
                {
                    if (txtTransactionId.Text.Trim().ToLower() == "<<---new--->>")
                    {
                        cboFromToWhom.Focus();
                    }
                    else
                    {
                        txtTransactionId.Focus();
                    }
                }
                else
                {
                    cboFromToWhom.Focus();
                }
                pFirstTimeLoad = false;
            }
        }
        #endregion

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<Object>();

            mObjectsList.Add(radEntrySide.Properties.Items[0]);
            mObjectsList.Add(radEntrySide.Properties.Items[1]);
            mObjectsList.Add(radEntrySide.Properties.Items[2]);
            mObjectsList.Add(txbDate);
            mObjectsList.Add(txbTransactionId);
            mObjectsList.Add(txbFromToWhom);
            mObjectsList.Add(txbMemo);
            mObjectsList.Add(txbAmount);
            mObjectsList.Add(grpAccountDetails);
            mObjectsList.Add(txbAccountCode);
            mObjectsList.Add(txbAccountName);
            mObjectsList.Add(txbAccountBalance);
            mObjectsList.Add(transdate);
            mObjectsList.Add(reference);
            mObjectsList.Add(transdescription);
            mObjectsList.Add(fromwhomtowhom);
            mObjectsList.Add(debitamount);
            mObjectsList.Add(creditamount);
            mObjectsList.Add(cmdUpdate);
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

                DataTable mDtAccounts = pMdtDepositAccounts.View(
                    "code<>'" + txtAccountCode.Text.Trim() + "'", "code", Program.gLanguageName, "grdBILDepositAccounts");
                foreach (DataRow mDataRow in mDtAccounts.Rows)
                {
                    DataRow mNewRow = pDtAccounts.NewRow();
                    mNewRow["code"] = mDataRow["code"].ToString();
                    mNewRow["description"] = mDataRow["description"].ToString();
                    mNewRow["balance"] = Convert.ToDouble(mDataRow["balance"]);
                    pDtAccounts.Rows.Add(mNewRow);
                    pDtAccounts.AcceptChanges();
                }

                foreach (DataColumn mDataColumn in pDtAccounts.Columns)
                {
                    mDataColumn.Caption = mDtAccounts.Columns[mDataColumn.ColumnName].Caption;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_FromToWhom
        private void Fill_FromToWhom()
        {
            DataTable mDtFromToWhom = pMdtDepositAccounts.Get_FromToWhom("", "", "", "");
            pFromToWhom.Clear();
            foreach (DataRow mDataRow in mDtFromToWhom.Rows)
            {
                pFromToWhom.Add(mDataRow["description"].ToString());
            }
        }
        #endregion

        #region Fill_Transactions
        internal void Fill_Transactions()
        {
            String mFunctionName = "Fill_Transactions";

            try
            {
                pDtTransactions.Rows.Clear();
                Program.gMdiForm.Cursor = Cursors.WaitCursor;

                DataTable mDtTransactions = pMdtDepositAccounts.View_Transactions(
                    "accountcode='" + txtAccountCode.Text.Trim() + "'", "transdate desc, autocode desc", "", "");
                foreach (DataRow mDataRow in mDtTransactions.Rows)
                {
                    DataRow mNewRow = pDtTransactions.NewRow();
                    mNewRow["transdate"] = Convert.ToDateTime(mDataRow["transdate"]);
                    mNewRow["reference"] = mDataRow["reference"].ToString();
                    mNewRow["transdescription"] = mDataRow["transdescription"].ToString();
                    mNewRow["fromwhomtowhom"] = mDataRow["fromwhomtowhom"].ToString();
                    mNewRow["creditamount"] = Convert.ToDouble(mDataRow["creditamount"]);
                    mNewRow["debitamount"] = Convert.ToDouble(mDataRow["debitamount"]);
                    pDtTransactions.Rows.Add(mNewRow);
                    pDtTransactions.AcceptChanges();
                }

                Program.gMdiForm.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Program.gMdiForm.Cursor = Cursors.Default;
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Get_CurrentBalance
        private void Get_CurrentBalance()
        {
            string mFunctionName = "Get_CurrentBalance";

            try
            {
                DataTable mDtDepositAccounts = pMdtDepositAccounts.View(
                    "code='" + txtAccountCode.Text.Trim() + "'", "", "", "");

                double mCurrentBalance = 0;
                if (mDtDepositAccounts.Rows.Count > 0)
                {
                    mCurrentBalance = Convert.ToDouble(mDtDepositAccounts.Rows[0]["balance"]);
                }

                txtAccountBalance.Text = mCurrentBalance.ToString("c");
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdUpdate_Click
        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            string mFromWhomToWhomCode = "";
            string mFromWhomToWhom = "";
            Int16 mGenerateCode = 0;
            string mFunctionName = "cmdUpdate_Click";

            #region validation
            if (Program.IsDate(Program.gMdiForm.txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                return;
            }
            if (txtTransactionId.Text.Trim() == "" && txtTransactionId.Text.Trim().ToLower() != "<<---new--->>")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DepositTransactionIdIsInvalid.ToString());
                txtTransactionId.Focus();
                return;
            }

            if (radEntrySide.SelectedIndex == 2)
            {
                if (cboDestinationAccount.ItemIndex == -1)
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_DestinationAccountIsInvalid.ToString());
                    cboDestinationAccount.Focus();
                    return;
                }
                mFromWhomToWhomCode = cboDestinationAccount.GetColumnValue("code").ToString();
                mFromWhomToWhom = cboDestinationAccount.GetColumnValue("description").ToString();
            }
            else
            {
                if (cboFromToWhom.Text.Trim() == "")
                {
                    Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_FromToWhonIsInvalid.ToString());
                    cboFromToWhom.Focus();
                    return;
                }
                mFromWhomToWhomCode = "";
                mFromWhomToWhom = cboFromToWhom.Text;
            }

            if (txtMemo.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_MemoIsInvalid.ToString());
                txtMemo.Focus();
                return;
            }

            if (Program.IsMoney(txtAmount.Text)==false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.BIL_AmountIsInvalid.ToString());
                txtAmount.Focus();
                return;
            }
            #endregion

            try
            {
                if (txtTransactionId.Text.Trim().ToLower() == "<<---new--->>")
                {
                    mGenerateCode = 1;
                }

                DateTime mTransDate = Convert.ToDateTime(txtDate.EditValue);

                string mEntrySide = "Debit";
                if (radEntrySide.SelectedIndex == 1)
                {
                    mEntrySide = "Credit";
                }
                if (radEntrySide.SelectedIndex == 2)
                {
                    mEntrySide = "Transfer";
                }

                //transact 
                pResult = pMdtDepositAccounts.Transact(mTransDate, mGenerateCode, txtTransactionId.Text,
                    txtAccountCode.Text, txtAccountName.Text, mFromWhomToWhomCode, mFromWhomToWhom,
                    Convert.ToDouble(txtAmount.Text), txtMemo.Text, mEntrySide, Program.gCurrentUser.Code);
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

                this.Fill_Accounts();
                this.Fill_Transactions();
                this.Fill_FromToWhom();
                this.Get_CurrentBalance();
                this.Mode_New();

                cboFromToWhom.Text = "";
                txtMemo.Text = "";
                txtAmount.Text = "";
                switch (radEntrySide.SelectedIndex)
                {
                    case 0: txtMemo.Text = radEntrySide.Properties.Items[0].Description; break;
                    case 1: txtMemo.Text = radEntrySide.Properties.Items[1].Description; break;
                    case 2: txtMemo.Text = radEntrySide.Properties.Items[2].Description; break;
                    default: txtMemo.Text = ""; break;
                }

                if (pForm != null)
                {
                    ((frmBILDepositAccounts)pForm).txtBalance.Text = txtAccountBalance.Text;
                }
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

        #region radEntrySide_SelectedIndexChanged
        private void radEntrySide_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFromToWhom.Text = "";
            cboDestinationAccount.EditValue = null;

            switch (radEntrySide.SelectedIndex)
            {
                case 0:
                    {
                        cboDestinationAccount.Visible = false;
                        cboFromToWhom.Visible = true;
                        txtMemo.Text = radEntrySide.Properties.Items[0].Description;
                    }
                    break;
                case 1:
                    {
                        cboDestinationAccount.Visible = false;
                        cboFromToWhom.Visible = true;
                        txtMemo.Text = radEntrySide.Properties.Items[1].Description;
                    }
                    break;
                case 2:
                    {
                        cboFromToWhom.Visible = false;
                        cboDestinationAccount.Visible = true;
                        txtMemo.Text = radEntrySide.Properties.Items[2].Description;
                    }
                    break;
                default:
                    {
                        cboDestinationAccount.Visible = false;
                        cboFromToWhom.Visible = true;
                        txtMemo.Text = "";
                    }
                    break;
            }
        }
        #endregion
    }
}