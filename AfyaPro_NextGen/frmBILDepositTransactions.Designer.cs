namespace AfyaPro_NextGen
{
    partial class frmBILDepositTransactions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBILDepositTransactions));
            this.txbDate = new DevExpress.XtraEditors.LabelControl();
            this.txtDate = new DevExpress.XtraEditors.DateEdit();
            this.cboFromToWhom = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txbMemo = new DevExpress.XtraEditors.LabelControl();
            this.txtMemo = new DevExpress.XtraEditors.MemoEdit();
            this.txbAmount = new DevExpress.XtraEditors.LabelControl();
            this.txtAmount = new DevExpress.XtraEditors.TextEdit();
            this.radEntrySide = new DevExpress.XtraEditors.RadioGroup();
            this.cmdUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            this.grdTransactions = new DevExpress.XtraGrid.GridControl();
            this.viewTransactions = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.transdate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reference = new DevExpress.XtraGrid.Columns.GridColumn();
            this.transdescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.fromwhomtowhom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.debitamount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.creditamount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grpAccountDetails = new DevExpress.XtraEditors.GroupControl();
            this.txtAccountBalance = new DevExpress.XtraEditors.TextEdit();
            this.txbAccountBalance = new DevExpress.XtraEditors.LabelControl();
            this.txbAccountName = new DevExpress.XtraEditors.LabelControl();
            this.txtAccountName = new DevExpress.XtraEditors.TextEdit();
            this.txtAccountCode = new DevExpress.XtraEditors.TextEdit();
            this.txbAccountCode = new DevExpress.XtraEditors.LabelControl();
            this.txbTransactionId = new DevExpress.XtraEditors.LabelControl();
            this.txtTransactionId = new DevExpress.XtraEditors.TextEdit();
            this.cboDestinationAccount = new DevExpress.XtraEditors.LookUpEdit();
            this.txbFromToWhom = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFromToWhom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radEntrySide.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransactions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewTransactions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAccountDetails)).BeginInit();
            this.grpAccountDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountBalance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTransactionId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDestinationAccount.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txbDate
            // 
            this.txbDate.Location = new System.Drawing.Point(250, 14);
            this.txbDate.Name = "txbDate";
            this.txbDate.Size = new System.Drawing.Size(23, 13);
            this.txbDate.TabIndex = 1;
            this.txbDate.Text = "Date";
            // 
            // txtDate
            // 
            this.txtDate.EditValue = null;
            this.txtDate.Location = new System.Drawing.Point(250, 31);
            this.txtDate.Name = "txtDate";
            this.txtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDate.Size = new System.Drawing.Size(125, 20);
            this.txtDate.TabIndex = 2;
            // 
            // cboFromToWhom
            // 
            this.cboFromToWhom.Location = new System.Drawing.Point(6, 127);
            this.cboFromToWhom.Name = "cboFromToWhom";
            this.cboFromToWhom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboFromToWhom.Size = new System.Drawing.Size(283, 20);
            this.cboFromToWhom.TabIndex = 3;
            // 
            // txbMemo
            // 
            this.txbMemo.Location = new System.Drawing.Point(6, 152);
            this.txbMemo.Name = "txbMemo";
            this.txbMemo.Size = new System.Drawing.Size(85, 13);
            this.txbMemo.TabIndex = 4;
            this.txbMemo.Text = "Memo/Description";
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(6, 171);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(283, 50);
            this.txtMemo.TabIndex = 5;
            // 
            // txbAmount
            // 
            this.txbAmount.Location = new System.Drawing.Point(294, 185);
            this.txbAmount.Name = "txbAmount";
            this.txbAmount.Size = new System.Drawing.Size(37, 13);
            this.txbAmount.TabIndex = 6;
            this.txbAmount.Text = "Amount";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(294, 201);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(129, 20);
            this.txtAmount.TabIndex = 6;
            // 
            // radEntrySide
            // 
            this.radEntrySide.Location = new System.Drawing.Point(6, 6);
            this.radEntrySide.Name = "radEntrySide";
            this.radEntrySide.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Deposit"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "WithDraw"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Transfer")});
            this.radEntrySide.Size = new System.Drawing.Size(125, 95);
            this.radEntrySide.TabIndex = 1;
            this.radEntrySide.SelectedIndexChanged += new System.EventHandler(this.radEntrySide_SelectedIndexChanged);
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Location = new System.Drawing.Point(428, 198);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(139, 26);
            this.cmdUpdate.TabIndex = 7;
            this.cmdUpdate.Text = "Save";
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(567, 198);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(139, 26);
            this.cmdClose.TabIndex = 8;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // grdTransactions
            // 
            this.grdTransactions.EmbeddedNavigator.Name = "";
            this.grdTransactions.Location = new System.Drawing.Point(7, 227);
            this.grdTransactions.MainView = this.viewTransactions;
            this.grdTransactions.Name = "grdTransactions";
            this.grdTransactions.Size = new System.Drawing.Size(699, 289);
            this.grdTransactions.TabIndex = 9;
            this.grdTransactions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewTransactions});
            // 
            // viewTransactions
            // 
            this.viewTransactions.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.transdate,
            this.reference,
            this.transdescription,
            this.fromwhomtowhom,
            this.debitamount,
            this.creditamount});
            this.viewTransactions.GridControl = this.grdTransactions;
            this.viewTransactions.Name = "viewTransactions";
            this.viewTransactions.OptionsBehavior.Editable = false;
            this.viewTransactions.OptionsMenu.EnableColumnMenu = false;
            this.viewTransactions.OptionsMenu.EnableFooterMenu = false;
            this.viewTransactions.OptionsMenu.EnableGroupPanelMenu = false;
            this.viewTransactions.OptionsView.RowAutoHeight = true;
            this.viewTransactions.OptionsView.ShowFooter = true;
            this.viewTransactions.OptionsView.ShowGroupPanel = false;
            this.viewTransactions.OptionsView.ShowIndicator = false;
            // 
            // transdate
            // 
            this.transdate.Caption = "Date";
            this.transdate.FieldName = "transdate";
            this.transdate.Name = "transdate";
            this.transdate.Visible = true;
            this.transdate.VisibleIndex = 0;
            this.transdate.Width = 86;
            // 
            // reference
            // 
            this.reference.Caption = "Ref #";
            this.reference.FieldName = "reference";
            this.reference.Name = "reference";
            this.reference.Visible = true;
            this.reference.VisibleIndex = 1;
            this.reference.Width = 84;
            // 
            // transdescription
            // 
            this.transdescription.Caption = "Memo";
            this.transdescription.FieldName = "transdescription";
            this.transdescription.Name = "transdescription";
            this.transdescription.Visible = true;
            this.transdescription.VisibleIndex = 2;
            this.transdescription.Width = 132;
            // 
            // fromwhomtowhom
            // 
            this.fromwhomtowhom.Caption = "fromwhomtowhom";
            this.fromwhomtowhom.FieldName = "fromwhomtowhom";
            this.fromwhomtowhom.Name = "fromwhomtowhom";
            this.fromwhomtowhom.Visible = true;
            this.fromwhomtowhom.VisibleIndex = 3;
            this.fromwhomtowhom.Width = 150;
            // 
            // debitamount
            // 
            this.debitamount.Caption = "Debit";
            this.debitamount.FieldName = "debitamount";
            this.debitamount.Name = "debitamount";
            this.debitamount.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.debitamount.Visible = true;
            this.debitamount.VisibleIndex = 4;
            this.debitamount.Width = 123;
            // 
            // creditamount
            // 
            this.creditamount.Caption = "Credit";
            this.creditamount.FieldName = "creditamount";
            this.creditamount.Name = "creditamount";
            this.creditamount.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.creditamount.Visible = true;
            this.creditamount.VisibleIndex = 5;
            this.creditamount.Width = 120;
            // 
            // grpAccountDetails
            // 
            this.grpAccountDetails.Controls.Add(this.txtAccountBalance);
            this.grpAccountDetails.Controls.Add(this.txbAccountBalance);
            this.grpAccountDetails.Controls.Add(this.txbAccountName);
            this.grpAccountDetails.Controls.Add(this.txtAccountName);
            this.grpAccountDetails.Controls.Add(this.txtAccountCode);
            this.grpAccountDetails.Controls.Add(this.txbAccountCode);
            this.grpAccountDetails.Location = new System.Drawing.Point(474, 11);
            this.grpAccountDetails.Name = "grpAccountDetails";
            this.grpAccountDetails.Size = new System.Drawing.Size(229, 160);
            this.grpAccountDetails.TabIndex = 12;
            this.grpAccountDetails.Text = "Account Details";
            // 
            // txtAccountBalance
            // 
            this.txtAccountBalance.Enabled = false;
            this.txtAccountBalance.Location = new System.Drawing.Point(7, 130);
            this.txtAccountBalance.Name = "txtAccountBalance";
            this.txtAccountBalance.Size = new System.Drawing.Size(118, 20);
            this.txtAccountBalance.TabIndex = 12;
            // 
            // txbAccountBalance
            // 
            this.txbAccountBalance.Location = new System.Drawing.Point(7, 113);
            this.txbAccountBalance.Name = "txbAccountBalance";
            this.txbAccountBalance.Size = new System.Drawing.Size(77, 13);
            this.txbAccountBalance.TabIndex = 4;
            this.txbAccountBalance.Text = "Current Balance";
            // 
            // txbAccountName
            // 
            this.txbAccountName.Location = new System.Drawing.Point(7, 70);
            this.txbAccountName.Name = "txbAccountName";
            this.txbAccountName.Size = new System.Drawing.Size(69, 13);
            this.txbAccountName.TabIndex = 3;
            this.txbAccountName.Text = "Account Name";
            // 
            // txtAccountName
            // 
            this.txtAccountName.Enabled = false;
            this.txtAccountName.Location = new System.Drawing.Point(7, 87);
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Size = new System.Drawing.Size(214, 20);
            this.txtAccountName.TabIndex = 11;
            // 
            // txtAccountCode
            // 
            this.txtAccountCode.Enabled = false;
            this.txtAccountCode.Location = new System.Drawing.Point(7, 44);
            this.txtAccountCode.Name = "txtAccountCode";
            this.txtAccountCode.Size = new System.Drawing.Size(214, 20);
            this.txtAccountCode.TabIndex = 10;
            // 
            // txbAccountCode
            // 
            this.txbAccountCode.Location = new System.Drawing.Point(7, 27);
            this.txbAccountCode.Name = "txbAccountCode";
            this.txbAccountCode.Size = new System.Drawing.Size(50, 13);
            this.txbAccountCode.TabIndex = 0;
            this.txbAccountCode.Text = "Account #";
            // 
            // txbTransactionId
            // 
            this.txbTransactionId.Location = new System.Drawing.Point(250, 55);
            this.txbTransactionId.Name = "txbTransactionId";
            this.txbTransactionId.Size = new System.Drawing.Size(61, 13);
            this.txbTransactionId.TabIndex = 13;
            this.txbTransactionId.Text = "Reference #";
            // 
            // txtTransactionId
            // 
            this.txtTransactionId.Location = new System.Drawing.Point(250, 72);
            this.txtTransactionId.Name = "txtTransactionId";
            this.txtTransactionId.Size = new System.Drawing.Size(125, 20);
            this.txtTransactionId.TabIndex = 3;
            // 
            // cboDestinationAccount
            // 
            this.cboDestinationAccount.Location = new System.Drawing.Point(6, 127);
            this.cboDestinationAccount.Name = "cboDestinationAccount";
            this.cboDestinationAccount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDestinationAccount.Properties.NullText = "";
            this.cboDestinationAccount.Size = new System.Drawing.Size(283, 20);
            this.cboDestinationAccount.TabIndex = 4;
            // 
            // txbFromToWhom
            // 
            this.txbFromToWhom.Location = new System.Drawing.Point(6, 109);
            this.txbFromToWhom.Name = "txbFromToWhom";
            this.txbFromToWhom.Size = new System.Drawing.Size(106, 13);
            this.txbFromToWhom.TabIndex = 14;
            this.txbFromToWhom.Text = "From Whom/To Whom";
            // 
            // frmBILDepositTransactions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 522);
            this.Controls.Add(this.txbFromToWhom);
            this.Controls.Add(this.cboFromToWhom);
            this.Controls.Add(this.cboDestinationAccount);
            this.Controls.Add(this.txtTransactionId);
            this.Controls.Add(this.txbTransactionId);
            this.Controls.Add(this.grpAccountDetails);
            this.Controls.Add(this.grdTransactions);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdUpdate);
            this.Controls.Add(this.radEntrySide);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.txbAmount);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.txbMemo);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.txbDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBILDepositTransactions";
            this.Text = "Deposit Account Transactions";
            this.Load += new System.EventHandler(this.frmBILDepositTransactions_Load);
            this.Activated += new System.EventHandler(this.frmBILDepositTransactions_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFromToWhom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radEntrySide.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransactions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewTransactions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAccountDetails)).EndInit();
            this.grpAccountDetails.ResumeLayout(false);
            this.grpAccountDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountBalance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTransactionId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDestinationAccount.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl txbDate;
        private DevExpress.XtraEditors.DateEdit txtDate;
        private DevExpress.XtraEditors.ComboBoxEdit cboFromToWhom;
        private DevExpress.XtraEditors.LabelControl txbMemo;
        private DevExpress.XtraEditors.MemoEdit txtMemo;
        private DevExpress.XtraEditors.LabelControl txbAmount;
        private DevExpress.XtraEditors.TextEdit txtAmount;
        private DevExpress.XtraEditors.RadioGroup radEntrySide;
        private DevExpress.XtraEditors.SimpleButton cmdUpdate;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
        private DevExpress.XtraGrid.GridControl grdTransactions;
        private DevExpress.XtraGrid.Views.Grid.GridView viewTransactions;
        private DevExpress.XtraGrid.Columns.GridColumn transdate;
        private DevExpress.XtraGrid.Columns.GridColumn reference;
        private DevExpress.XtraGrid.Columns.GridColumn transdescription;
        private DevExpress.XtraGrid.Columns.GridColumn creditamount;
        private DevExpress.XtraGrid.Columns.GridColumn debitamount;
        private DevExpress.XtraEditors.GroupControl grpAccountDetails;
        private DevExpress.XtraEditors.LabelControl txbAccountBalance;
        private DevExpress.XtraEditors.LabelControl txbAccountName;
        private DevExpress.XtraEditors.LabelControl txbAccountCode;
        internal DevExpress.XtraEditors.TextEdit txtAccountCode;
        internal DevExpress.XtraEditors.TextEdit txtAccountBalance;
        internal DevExpress.XtraEditors.TextEdit txtAccountName;
        private DevExpress.XtraEditors.LabelControl txbTransactionId;
        private DevExpress.XtraEditors.TextEdit txtTransactionId;
        private DevExpress.XtraEditors.LookUpEdit cboDestinationAccount;
        private DevExpress.XtraEditors.LabelControl txbFromToWhom;
        private DevExpress.XtraGrid.Columns.GridColumn fromwhomtowhom;
    }
}