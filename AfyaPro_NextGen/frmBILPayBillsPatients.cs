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
using DevExpress.XtraLayout;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.IO;
using System.Xml.Serialization;

namespace AfyaPro_NextGen
{
    public partial class frmBILPayBillsPatients : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsRegistrations pMdtRegistrations;
        private AfyaPro_MT.clsBilling pMdtBilling;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private string pCurrPatientId = "";
        private string pPrevPatientId = "";
        private AfyaPro_Types.clsPatient pCurrentPatient;

        private string pBillingGroupCode = "";
        private string pBillingSubGroupCode = "";
        private DataTable pDtInvoices = new DataTable("invoices");

        private int pFormWidth = 0;
        private int pFormHeight = 0;

        private bool pCheckedCheckBox = false;
        private bool pCheckedGrid = false;

        private string pReceiptNo = "";
        private double pTotalPaid = 0;
        private string pPayTypeCode = "";
        private string pPayTypeDescription = "";
        private string pBank = "";
        private string pBranch = "";
        private string pHolder = "";
        private string pChequeNo = "";
        private Int16 pCheckDepositBalance = 0;
        private string pAccountCode = "";
        private string pAccountDescription = "";
        private bool pPaymentsDone = false;
        private bool pSearchingPatient = false;

        #endregion

        #region frmBILPayBillsPatients
        public frmBILPayBillsPatients()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmBILPayBillsPatients";

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                pMdtRegistrations = (AfyaPro_MT.clsRegistrations)Activator.GetObject(
                    typeof(AfyaPro_MT.clsRegistrations),
                    Program.gMiddleTier + "clsRegistrations");

                pMdtBilling = (AfyaPro_MT.clsBilling)Activator.GetObject(
                    typeof(AfyaPro_MT.clsBilling),
                    Program.gMiddleTier + "clsBilling");

                pDtInvoices.Columns.Add("selected", typeof(System.Boolean));
                pDtInvoices.Columns.Add("transdate", typeof(System.DateTime));
                pDtInvoices.Columns.Add("invoiceno", typeof(System.String));
                pDtInvoices.Columns.Add("totaldue", typeof(System.Double));
                pDtInvoices.Columns.Add("totalpaid", typeof(System.Double));
                pDtInvoices.Columns.Add("balancedue", typeof(System.Double));
                pDtInvoices.Columns.Add("patientcode", typeof(System.String));
                pDtInvoices.Columns.Add("patientsurname", typeof(System.String));
                pDtInvoices.Columns.Add("patientfirstname", typeof(System.String));
                pDtInvoices.Columns.Add("patientothernames", typeof(System.String));
                pDtInvoices.Columns.Add("patientgender", typeof(System.String));
                pDtInvoices.Columns.Add("patientbirthdate", typeof(System.DateTime));
                pDtInvoices.Columns.Add("patientfullname", typeof(System.String));
                pDtInvoices.Columns.Add("billinggroupcode", typeof(System.String));
                pDtInvoices.Columns.Add("billinggroupdescription", typeof(System.String));
                pDtInvoices.Columns.Add("billingsubgroupcode", typeof(System.String));
                pDtInvoices.Columns.Add("billingsubgroupdescription", typeof(System.String));
                pDtInvoices.Columns.Add("billinggroupmembershipno", typeof(System.String));
                pDtInvoices.Columns.Add("booking", typeof(System.String));

                grdBILInvoicesHistory.ForceInitialize();

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.bilpaybillspatients_customizelayout.ToString());
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
            List<Object> mObjectsList = new List<object>();

            mObjectsList.Add(txbPatientId);
            mObjectsList.Add(txbName);
            mObjectsList.Add(txbStreet);
            mObjectsList.Add(txbBirthDate);
            mObjectsList.Add(txbYears);
            mObjectsList.Add(txbMonths);
            mObjectsList.Add(txbGender);
            mObjectsList.Add(radGender.Properties.Items[0]);
            mObjectsList.Add(radGender.Properties.Items[1]);
            mObjectsList.Add(txbBalanceDue);
            mObjectsList.Add(txbDepositBalance);
            mObjectsList.Add(txbToPay);
            mObjectsList.Add(chkAllInvoices);
            mObjectsList.Add(cmdSearch);
            mObjectsList.Add(cmdPay);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
            this.Data_Clear();
        }

        #endregion

        #region frmBILPayBillsPatients_Load
        private void frmBILPayBillsPatients_Load(object sender, EventArgs e)
        {
            this.Top = 0;

            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);

            DevExpress.XtraGrid.Views.Grid.GridView mGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            mGridView.OptionsBehavior.Editable = true;
            mGridView.OptionsView.ShowGroupPanel = false;
            mGridView.OptionsView.ShowFooter = false;
            mGridView.OptionsCustomization.AllowGroup = false;
            mGridView.OptionsView.ShowIndicator = false;
            grdBILInvoicesHistory.MainView = mGridView;

            this.Fill_Invoices();
            this.Load_Controls();

            chkAllInvoices.Checked = false;

            txbBirthDateFormat.Text = "(" + Program.gCulture.DateTimeFormat.ShortDatePattern + ")";

            this.pFormWidth = this.Width;
            this.pFormHeight = this.Height;

            Program.Center_Screen(this);
        }
        #endregion

        #region frmBILPayBillsPatients_FormClosing
        private void frmBILPayBillsPatients_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //layout
                if (layoutControl1.IsModified == true)
                {
                    Program.Save_FormLayout(this, layoutControl1, this.Name);
                }

                //grid
                Program.Save_GridLayout(grdBILInvoicesHistory, "grdBILPatientInvoices");
            }
            catch { }
        }
        #endregion

        #region Data_Clear
        private void Data_Clear()
        {
            txtName.Text = "";
            txtStreet.Text = "";
            radGender.SelectedIndex = -1;
            txtBirthDate.Text = "";
            txtYears.Text = "";
            txtMonths.Text = "";

            txtDepositAmt.Text = "";
            txtBalanceDue.Text = "";
            txtToPay.Text = "";
            pBillingGroupCode = "";
            pBillingSubGroupCode = "";
            txtToPay.Text = "";

            pTotalPaid = 0;
            pPayTypeCode = "";
            pPayTypeDescription = "";
            pBank = "";
            pBranch = "";
            pHolder = "";
            pChequeNo = "";
            pCheckDepositBalance = 0;
            pAccountCode = "";
            pAccountDescription = "";
            pDtInvoices.Rows.Clear();

            pPrevPatientId = "";
            pCurrPatientId = pPrevPatientId;
        }
        #endregion

        #region Data_Display
        internal void Data_Display(AfyaPro_Types.clsPatient mPatient)
        {
            String mFunctionName = "Data_Display";

            try
            {
                this.Data_Clear();

                if (mPatient != null)
                {
                    if (mPatient.Exist == true)
                    {
                        string mFullName = mPatient.firstname;
                        if (mPatient.othernames.Trim() != "")
                        {
                            mFullName = mFullName + " " + mPatient.othernames;
                        }
                        mFullName = mFullName + " " + mPatient.surname;

                        txtPatientId.Text = mPatient.code;
                        txtName.Text = mFullName;
                        if (mPatient.gender.Trim().ToLower() == "f")
                        {
                            radGender.SelectedIndex = 0;
                        }
                        else
                        {
                            radGender.SelectedIndex = 1;
                        }
                        txtBirthDate.Text = mPatient.birthdate.ToString("d");
                        int mDays = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue).Subtract(mPatient.birthdate).Days;
                        int mYears = (int)mDays / 365;
                        int mMonths = (int)(mDays % 365) / 30;

                        txtYears.Text = mYears.ToString();
                        txtMonths.Text = mMonths.ToString();

                        #region summary

                        //balance
                        DataTable mDtBalances = pMdtBilling.Get_DebtorBalances(
                        "accountcode='" + txtPatientId.Text.Trim() + "' and debtortype='"
                        + AfyaPro_Types.clsEnums.DebtorTypes.Individual.ToString().Trim() + "'", "", "", "");
                        double mBalanceDue = 0;
                        if (mDtBalances.Rows.Count > 0)
                        {
                            mBalanceDue = Convert.ToDouble(mDtBalances.Rows[0]["balance"]);
                        }
                        txtBalanceDue.Text = mBalanceDue.ToString("c");

                        //account balance
                        DataTable mDtAccountBalances = pMdtBilling.Get_PatientAccountBalances(txtPatientId.Text, "", "");
                        double mAccountBalance = 0;
                        foreach (DataRow mDataRow in mDtAccountBalances.Rows)
                        {
                            mAccountBalance = mAccountBalance + Convert.ToDouble(mDataRow["balance"]);
                        }
                        txtDepositAmt.Text = mAccountBalance.ToString("c");

                        #endregion

                        this.Fill_Invoices();

                        pCurrPatientId = mPatient.code;
                        pPrevPatientId = pCurrPatientId;

                        System.Media.SystemSounds.Beep.Play();
                    }
                    else
                    {
                        pCurrPatientId = txtPatientId.Text;
                        pPrevPatientId = pCurrPatientId;
                    }
                }
                else
                {
                    pCurrPatientId = txtPatientId.Text;
                    pPrevPatientId = pCurrPatientId;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Fill_Invoices
        private void Fill_Invoices()
        {
            string mFunctionName = "Fill_Invoices";

            try
            {
                bool mDateSpecified = false;

                DataTable mDtInvoices = pMdtBilling.View_Invoices(mDateSpecified, new DateTime(), new DateTime(),
                "patientcode='" + txtPatientId.Text.Trim() + "' and balancedue<>0 and billinggroupcode='' and voided<>1", "transdate,autocode",
                Program.gLanguageName, "grdBILInvoicesHistory");

                double mToPay = 0;

                pDtInvoices.Rows.Clear();
                chkAllInvoices.Checked = false;

                foreach (DataRow mDataRow in mDtInvoices.Rows)
                {
                    DataRow mNewRow = pDtInvoices.NewRow();
                    mNewRow["selected"] = true;
                    mNewRow["transdate"] = Convert.ToDateTime(mDataRow["transdate"]);
                    mNewRow["invoiceno"] = mDataRow["invoiceno"].ToString().Trim();
                    mNewRow["totaldue"] = Convert.ToDouble(mDataRow["totaldue"]);
                    mNewRow["totalpaid"] = Convert.ToDouble(mDataRow["totalpaid"]);
                    mNewRow["balancedue"] = Convert.ToDouble(mDataRow["balancedue"]);
                    mNewRow["patientcode"] = mDataRow["patientcode"].ToString().Trim();
                    mNewRow["patientsurname"] = mDataRow["patientsurname"].ToString().Trim();
                    mNewRow["patientfirstname"] = mDataRow["patientfirstname"].ToString().Trim();
                    mNewRow["patientothernames"] = mDataRow["patientothernames"].ToString().Trim();
                    mNewRow["patientgender"] = mDataRow["patientgender"].ToString().Trim();
                    mNewRow["patientbirthdate"] = Convert.ToDateTime(mDataRow["patientbirthdate"]);
                    mNewRow["patientfullname"] = mDataRow["patientfullname"].ToString().Trim();
                    mNewRow["billinggroupcode"] = mDataRow["billinggroupcode"].ToString().Trim();
                    mNewRow["billinggroupdescription"] = mDataRow["billinggroupdescription"].ToString().Trim();
                    mNewRow["billingsubgroupcode"] = mDataRow["billingsubgroupcode"].ToString().Trim();
                    mNewRow["billingsubgroupdescription"] = mDataRow["billingsubgroupdescription"].ToString().Trim();
                    mNewRow["billinggroupmembershipno"] = mDataRow["billinggroupmembershipno"].ToString().Trim();
                    mNewRow["booking"] = mDataRow["booking"].ToString().Trim();
                    pDtInvoices.Rows.Add(mNewRow);
                    pDtInvoices.AcceptChanges();

                    mToPay = mToPay + Convert.ToDouble(mDataRow["balancedue"]);
                }

                foreach (DataColumn mDataColumn in pDtInvoices.Columns)
                {
                    if (mDataColumn.ColumnName.ToLower() != "selected")
                    {
                        mDataColumn.Caption = mDtInvoices.Columns[mDataColumn.ColumnName].Caption;
                    }
                }

                grdBILInvoicesHistory.DataSource = pDtInvoices;

                DevExpress.XtraGrid.Views.Grid.GridView mGridView = (DevExpress.XtraGrid.Views.Grid.GridView)grdBILInvoicesHistory.MainView;
                mGridView.Columns["selected"].OptionsColumn.ShowCaption = false;

                #region extra settings

                Program.Restore_GridLayout(grdBILInvoicesHistory, "grdBILPatientInvoices");

                for (int mIndex = 0; mIndex < mGridView.Columns.Count; mIndex++)
                {
                    if (mGridView.Columns[mIndex].FieldName.ToLower() != "selected")
                    {
                        mGridView.Columns[mIndex].OptionsColumn.AllowEdit = false;
                    }

                    switch (mGridView.Columns[mIndex].ColumnType.ToString().ToLower())
                    {
                        case "system.double":
                            {
                                mGridView.Columns[mIndex].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                                mGridView.Columns[mIndex].DisplayFormat.FormatString = "{0:c}";
                            }
                            break;
                    }
                }

                mGridView.Columns["selected"].Caption = "";

                DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit mCheckEdit =
                    new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
                mCheckEdit.CheckedChanged += new EventHandler(mCheckEdit_CheckedChanged);
                mGridView.Columns["selected"].ColumnEdit = mCheckEdit;

                #endregion

                if (pDtInvoices.Rows.Count > 0)
                {
                    chkAllInvoices.Checked = true;
                }

                txtToPay.Text = mToPay.ToString("c");
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Search_Patient
        private AfyaPro_Types.clsPatient Search_Patient()
        {
            string mFunctionName = "Search_Patient";

            try
            {
                pCurrentPatient = pMdtRegistrations.Get_Patient(txtPatientId.Text);
                return pCurrentPatient;
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return null;
            }
        }
        #endregion

        #region txtPatientId_KeyDown
        private void txtPatientId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.pCurrentPatient = this.Search_Patient();
                this.Data_Display(pCurrentPatient);
            }
        }
        #endregion

        #region txtPatientId_Leave
        private void txtPatientId_Leave(object sender, EventArgs e)
        {
            pCurrPatientId = txtPatientId.Text;

            if (pCurrPatientId.Trim().ToLower() != pPrevPatientId.Trim().ToLower())
            {
                this.pCurrentPatient = this.Search_Patient();
                this.Data_Display(pCurrentPatient);
            }
        }
        #endregion

        #region chkAllInvoices_CheckedChanged
        private void chkAllInvoices_CheckedChanged(object sender, EventArgs e)
        {
            if (pCheckedCheckBox == true)
            {
                double mToPay = 0;

                foreach (DataRow mDataRow in pDtInvoices.Rows)
                {
                    mDataRow["selected"] = chkAllInvoices.Checked;

                    if (Convert.ToBoolean(mDataRow["selected"]) == true)
                    {
                        mToPay = mToPay + Convert.ToDouble(mDataRow["balancedue"]);
                    }
                }

                txtToPay.Text = mToPay.ToString("c");
            }
        }
        #endregion

        #region mCheckEdit_CheckedChanged
        void mCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (pCheckedGrid == true)
            {
                grdBILInvoicesHistory.FocusedView.PostEditor();

                int mChecked = 0;
                int mUnChecked = 0;
                double mToPay = 0;

                foreach (DataRow mDataRow in pDtInvoices.Rows)
                {
                    if (Convert.ToBoolean(mDataRow["selected"]) == true)
                    {
                        mChecked++;
                        mToPay = mToPay + Convert.ToDouble(mDataRow["balancedue"]);
                    }
                    else
                    {
                        mUnChecked++;
                    }
                }

                if (mChecked == pDtInvoices.Rows.Count)
                {
                    chkAllInvoices.CheckState = CheckState.Checked;
                }
                else if (mUnChecked == pDtInvoices.Rows.Count)
                {
                    chkAllInvoices.CheckState = CheckState.Unchecked;
                }
                else
                {
                    chkAllInvoices.CheckState = CheckState.Indeterminate;
                }

                txtToPay.Text = mToPay.ToString("c");
            }
        }
        #endregion

        #region chkAllInvoices_Enter
        private void chkAllInvoices_Enter(object sender, EventArgs e)
        {
            pCheckedGrid = false;
            pCheckedCheckBox = true;
        }
        #endregion

        #region grdBILInvoicesHistory_Enter
        private void grdBILInvoicesHistory_Enter(object sender, EventArgs e)
        {
            pCheckedCheckBox = false;
            pCheckedGrid = true;
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region cmdSave_Click
        private void cmdSave_Click(object sender, EventArgs e)
        {
            AfyaPro_Types.clsBill mBill = new AfyaPro_Types.clsBill();
            Int16 mGenerateReceiptNo = 0;

            string mFunctionName = "cmdSave_Click";

            #region validation

            if (Program.IsDate(Program.gMdiForm.txtDate.EditValue) == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.GRL_TransactionDateIsInvalid.ToString());
                return;
            }

            if (txtPatientId.Text.Trim() == "")
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoIsInvalid.ToString());
                txtPatientId.Focus();
                return;
            }

            AfyaPro_Types.clsPatient mPatient = pMdtRegistrations.Get_Patient(txtPatientId.Text);
            if (mPatient.Exist == false)
            {
                Program.Display_Error(AfyaPro_Types.clsSystemMessages.MessageIds.OUTIN_PatientNoDoesNotExist.ToString());
                txtPatientId.Focus();
                return;
            }

            #endregion

            try
            {
                DateTime mTransDate = Convert.ToDateTime(Program.gMdiForm.txtDate.EditValue);

                double mTotalDue = 0;
                foreach (DataRow mDataRow in pDtInvoices.Rows)
                {
                    if (Convert.ToBoolean(mDataRow["selected"]) == true)
                    {
                        mTotalDue = mTotalDue + Convert.ToDouble(mDataRow["balancedue"]);
                    }
                }

                if (mTotalDue == 0)
                {
                    return;
                }

                #region do payments

                pPaymentsDone = false;

                frmBILFinalPaymentDebtor mBILFinalPaymentDebtor = new frmBILFinalPaymentDebtor(mTotalDue, true);
                mBILFinalPaymentDebtor.PatientId = txtPatientId.Text.Trim();
                mBILFinalPaymentDebtor.ReceiptNo = pReceiptNo;
                mBILFinalPaymentDebtor.TotalPaid = pTotalPaid;
                mBILFinalPaymentDebtor.PaymentsDone = pPaymentsDone;

                mBILFinalPaymentDebtor.ShowDialog();

                pReceiptNo = mBILFinalPaymentDebtor.ReceiptNo;
                pTotalPaid = mBILFinalPaymentDebtor.TotalPaid;
                pPayTypeCode = mBILFinalPaymentDebtor.PayTypeCode;
                pPayTypeDescription = mBILFinalPaymentDebtor.PayTypeDescription;
                pBank = mBILFinalPaymentDebtor.Bank;
                pBranch = mBILFinalPaymentDebtor.Branch;
                pHolder = mBILFinalPaymentDebtor.Holder;
                pChequeNo = mBILFinalPaymentDebtor.ChequeNo;
                pCheckDepositBalance = mBILFinalPaymentDebtor.CheckDepositBalance;
                pAccountCode = mBILFinalPaymentDebtor.AccountCode;
                pAccountDescription = mBILFinalPaymentDebtor.AccountDescription;
                pPaymentsDone = mBILFinalPaymentDebtor.PaymentsDone;

                #endregion

                if (pPaymentsDone == true)
                {
                    #region distribute amount paid to selected invoices

                    DataTable mDtInvoices = new DataTable("invoices");
                    mDtInvoices.Columns.Add("invoiceno", typeof(System.String));
                    mDtInvoices.Columns.Add("balancedue", typeof(System.Double));
                    mDtInvoices.Columns.Add("paidforbill", typeof(System.Double));
                    mDtInvoices.Columns.Add("patientcode", typeof(System.String));
                    mDtInvoices.Columns.Add("patientsurname", typeof(System.String));
                    mDtInvoices.Columns.Add("patientfirstname", typeof(System.String));
                    mDtInvoices.Columns.Add("patientothernames", typeof(System.String));
                    mDtInvoices.Columns.Add("patientgender", typeof(System.String));
                    mDtInvoices.Columns.Add("patientbirthdate", typeof(System.DateTime));
                    mDtInvoices.Columns.Add("billinggroupcode", typeof(System.String));
                    mDtInvoices.Columns.Add("billinggroupdescription", typeof(System.String));
                    mDtInvoices.Columns.Add("billingsubgroupcode", typeof(System.String));
                    mDtInvoices.Columns.Add("billingsubgroupdescription", typeof(System.String));
                    mDtInvoices.Columns.Add("billinggroupmembershipno", typeof(System.String));
                    mDtInvoices.Columns.Add("booking", typeof(System.String));

                    double mPaidBalance = pTotalPaid;
                    foreach (DataRow mDataRow in pDtInvoices.Rows)
                    {
                        if (mPaidBalance <= 0)
                        {
                            break;
                        }

                        if (Convert.ToBoolean(mDataRow["selected"]) == true)
                        {
                            double mPaidForBill = mPaidBalance;
                            double mInvoiceBalanceDue = Convert.ToDouble(mDataRow["balancedue"]);

                            if (mInvoiceBalanceDue < mPaidForBill)
                            {
                                mPaidForBill = mInvoiceBalanceDue;
                            }

                            DataRow mNewRow = mDtInvoices.NewRow();
                            mNewRow["invoiceno"] = mDataRow["invoiceno"].ToString().Trim();
                            mNewRow["balancedue"] = mInvoiceBalanceDue;
                            mNewRow["paidforbill"] = mPaidForBill;
                            mNewRow["patientcode"] = mDataRow["patientcode"].ToString().Trim();
                            mNewRow["patientsurname"] = mDataRow["patientsurname"].ToString().Trim();
                            mNewRow["patientfirstname"] = mDataRow["patientfirstname"].ToString().Trim();
                            mNewRow["patientothernames"] = mDataRow["patientothernames"].ToString().Trim();
                            mNewRow["patientgender"] = mDataRow["patientgender"].ToString().Trim();
                            mNewRow["patientbirthdate"] = Convert.ToDateTime(mDataRow["patientbirthdate"]);
                            mNewRow["billinggroupcode"] = mDataRow["billinggroupcode"].ToString().Trim();
                            mNewRow["billinggroupdescription"] = mDataRow["billinggroupdescription"].ToString().Trim();
                            mNewRow["billingsubgroupcode"] = mDataRow["billingsubgroupcode"].ToString().Trim();
                            mNewRow["billingsubgroupdescription"] = mDataRow["billingsubgroupdescription"].ToString().Trim();
                            mNewRow["billinggroupmembershipno"] = mDataRow["billinggroupmembershipno"].ToString().Trim();
                            mNewRow["booking"] = mDataRow["booking"].ToString().Trim();
                            mDtInvoices.Rows.Add(mNewRow);
                            mDtInvoices.AcceptChanges();

                            mPaidBalance = mPaidBalance - mPaidForBill;
                        }
                    }

                    #endregion

                    if (pReceiptNo.Trim().ToLower() == "<<---new--->>")
                    {
                        mGenerateReceiptNo = 1;
                    }

                    mBill = pMdtBilling.Pay_FromPatient(mGenerateReceiptNo, pReceiptNo, mTransDate, txtPatientId.Text,
                        pPayTypeCode, pBank, pBranch, pChequeNo, pHolder, pTotalPaid, pCheckDepositBalance,
                        pAccountCode, pAccountDescription, mDtInvoices, Program.gLocalCurrencyCode, Program.gLocalCurrencyDesc,
                        Program.gLocalCurrencySymbol, new DataTable("debtrelief"), Program.gCurrentUser.Code);
                    if (mBill.Exe_Result == 0)
                    {
                        Program.Display_Error(mBill.Exe_Message);
                        return;
                    }
                    if (mBill.Exe_Result == -1)
                    {
                        Program.Display_Server_Error(mBill.Exe_Message);
                        return;
                    }

                    txtPatientId.Text = "";
                    this.Data_Clear();
                    txtPatientId.Focus();
                }

                #region receipt printing

                if (mBill.ReceiptNo.Trim() != "")
                {
                    frmOPDPatientDocuments mOPDPatientDocuments = new frmOPDPatientDocuments();
                    mOPDPatientDocuments.AutoPrint_InvoicePayment(mBill.ReceiptNo);
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

        #region cmdSearch_Click
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            pSearchingPatient = true;

            frmSearchPatient mSearchPatient = new frmSearchPatient(txtPatientId);
            mSearchPatient.ShowDialog();

            pSearchingPatient = false;
        }
        #endregion

        #region txtPatientId_EditValueChanged
        private void txtPatientId_EditValueChanged(object sender, EventArgs e)
        {
            if (pSearchingPatient == true)
            {
                this.pCurrentPatient = this.Search_Patient();
                this.Data_Display(pCurrentPatient);
            }
        }
        #endregion

        private void frmBILPayBillsPatients_Shown(object sender, EventArgs e)
        {
            txtPatientId.Focus();
        }
    }
}