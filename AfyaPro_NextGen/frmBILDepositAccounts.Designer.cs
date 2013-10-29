namespace AfyaPro_NextGen
{
    partial class frmBILDepositAccounts
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
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.cmdTransactions = new DevExpress.XtraEditors.SimpleButton();
            this.txtBalance = new DevExpress.XtraEditors.TextEdit();
            this.cmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.txbBalance = new DevExpress.XtraEditors.LabelControl();
            this.chkInActive = new DevExpress.XtraEditors.CheckEdit();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            this.chkAllowOverDraft = new DevExpress.XtraEditors.CheckEdit();
            this.txtDescription = new DevExpress.XtraEditors.TextEdit();
            this.txbDescription = new DevExpress.XtraEditors.LabelControl();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txbCode = new DevExpress.XtraEditors.LabelControl();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.txbFromWhomToWhom = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.txbMemo = new DevExpress.XtraEditors.LabelControl();
            this.txtMemo = new DevExpress.XtraEditors.MemoEdit();
            this.cmdUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.txbAmount = new DevExpress.XtraEditors.LabelControl();
            this.txtAmount = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBalance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkInActive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllowOverDraft.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.cmdTransactions);
            this.groupControl2.Controls.Add(this.txtBalance);
            this.groupControl2.Controls.Add(this.cmdOk);
            this.groupControl2.Controls.Add(this.txbBalance);
            this.groupControl2.Controls.Add(this.chkInActive);
            this.groupControl2.Controls.Add(this.cmdClose);
            this.groupControl2.Controls.Add(this.chkAllowOverDraft);
            this.groupControl2.Controls.Add(this.txtDescription);
            this.groupControl2.Controls.Add(this.txbDescription);
            this.groupControl2.Controls.Add(this.txtCode);
            this.groupControl2.Controls.Add(this.txbCode);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(359, 200);
            this.groupControl2.TabIndex = 11;
            this.groupControl2.Text = "groupControl2";
            // 
            // cmdTransactions
            // 
            this.cmdTransactions.Enabled = false;
            this.cmdTransactions.Location = new System.Drawing.Point(120, 146);
            this.cmdTransactions.Name = "cmdTransactions";
            this.cmdTransactions.Size = new System.Drawing.Size(118, 52);
            this.cmdTransactions.TabIndex = 9;
            this.cmdTransactions.Text = "Transactions";
            this.cmdTransactions.Click += new System.EventHandler(this.cmdTransactions_Click);
            // 
            // txtBalance
            // 
            this.txtBalance.Enabled = false;
            this.txtBalance.Location = new System.Drawing.Point(110, 62);
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.Size = new System.Drawing.Size(193, 20);
            this.txtBalance.TabIndex = 3;
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(2, 146);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(118, 52);
            this.cmdOk.TabIndex = 6;
            this.cmdOk.Text = "Save";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // txbBalance
            // 
            this.txbBalance.Location = new System.Drawing.Point(9, 65);
            this.txbBalance.Name = "txbBalance";
            this.txbBalance.Size = new System.Drawing.Size(37, 13);
            this.txbBalance.TabIndex = 7;
            this.txbBalance.Text = "Balance";
            // 
            // chkInActive
            // 
            this.chkInActive.Location = new System.Drawing.Point(108, 115);
            this.chkInActive.Name = "chkInActive";
            this.chkInActive.Properties.Caption = "InActive";
            this.chkInActive.Size = new System.Drawing.Size(195, 19);
            this.chkInActive.TabIndex = 5;
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(238, 146);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(118, 52);
            this.cmdClose.TabIndex = 8;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // chkAllowOverDraft
            // 
            this.chkAllowOverDraft.Location = new System.Drawing.Point(108, 89);
            this.chkAllowOverDraft.Name = "chkAllowOverDraft";
            this.chkAllowOverDraft.Properties.Caption = "Allow Over Draft";
            this.chkAllowOverDraft.Size = new System.Drawing.Size(195, 19);
            this.chkAllowOverDraft.TabIndex = 4;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(110, 35);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(193, 20);
            this.txtDescription.TabIndex = 2;
            // 
            // txbDescription
            // 
            this.txbDescription.Location = new System.Drawing.Point(9, 38);
            this.txbDescription.Name = "txbDescription";
            this.txbDescription.Size = new System.Drawing.Size(53, 13);
            this.txbDescription.TabIndex = 3;
            this.txbDescription.Text = "Description";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(110, 8);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(100, 20);
            this.txtCode.TabIndex = 1;
            // 
            // txbCode
            // 
            this.txbCode.Location = new System.Drawing.Point(9, 11);
            this.txbCode.Name = "txbCode";
            this.txbCode.Size = new System.Drawing.Size(25, 13);
            this.txbCode.TabIndex = 1;
            this.txbCode.Text = "Code";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(6, 3);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Debit"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Credit")});
            this.radioGroup1.Size = new System.Drawing.Size(175, 51);
            this.radioGroup1.TabIndex = 14;
            // 
            // txbFromWhomToWhom
            // 
            this.txbFromWhomToWhom.Location = new System.Drawing.Point(6, 57);
            this.txbFromWhomToWhom.Name = "txbFromWhomToWhom";
            this.txbFromWhomToWhom.Size = new System.Drawing.Size(0, 13);
            this.txbFromWhomToWhom.TabIndex = 21;
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(6, 75);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(310, 20);
            this.textEdit1.TabIndex = 22;
            // 
            // txbMemo
            // 
            this.txbMemo.Location = new System.Drawing.Point(6, 99);
            this.txbMemo.Name = "txbMemo";
            this.txbMemo.Size = new System.Drawing.Size(0, 13);
            this.txbMemo.TabIndex = 23;
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(6, 117);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(310, 74);
            this.txtMemo.TabIndex = 24;
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Location = new System.Drawing.Point(6, 237);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(178, 30);
            this.cmdUpdate.TabIndex = 25;
            // 
            // txbAmount
            // 
            this.txbAmount.Location = new System.Drawing.Point(6, 195);
            this.txbAmount.Name = "txbAmount";
            this.txbAmount.Size = new System.Drawing.Size(0, 13);
            this.txbAmount.TabIndex = 26;
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(6, 211);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(176, 20);
            this.txtAmount.TabIndex = 27;
            // 
            // frmBILDepositAccounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 200);
            this.Controls.Add(this.groupControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBILDepositAccounts";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Deposit Accounts";
            this.Load += new System.EventHandler(this.frmBILDepositAccounts_Load);
            this.Activated += new System.EventHandler(this.frmBILDepositAccounts_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBalance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkInActive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllowOverDraft.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl txbCode;
        private DevExpress.XtraEditors.TextEdit txtDescription;
        private DevExpress.XtraEditors.LabelControl txbDescription;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.SimpleButton cmdOk;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
        private DevExpress.XtraEditors.CheckEdit chkInActive;
        private DevExpress.XtraEditors.CheckEdit chkAllowOverDraft;
        private DevExpress.XtraEditors.LabelControl txbBalance;
        private DevExpress.XtraEditors.SimpleButton cmdTransactions;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.LabelControl txbFromWhomToWhom;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl txbMemo;
        private DevExpress.XtraEditors.MemoEdit txtMemo;
        private DevExpress.XtraEditors.SimpleButton cmdUpdate;
        private DevExpress.XtraEditors.LabelControl txbAmount;
        private DevExpress.XtraEditors.TextEdit txtAmount;
        internal DevExpress.XtraEditors.TextEdit txtBalance;
    }
}