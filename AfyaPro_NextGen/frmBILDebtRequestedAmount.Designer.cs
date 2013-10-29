namespace AfyaPro_NextGen
{
    partial class frmBILDebtRequestedAmount
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtReason = new DevExpress.XtraEditors.MemoEdit();
            this.txtReceiptNo = new DevExpress.XtraEditors.TextEdit();
            this.txtTotalDue = new DevExpress.XtraEditors.TextEdit();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            this.cmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.txtPaidAmount = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbOk = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbClose = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbReceiptNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbTotalDue = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbPaidAmount = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtReason.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiptNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalDue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPaidAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbReceiptNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbTotalDue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbPaidAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtReason);
            this.layoutControl1.Controls.Add(this.txtReceiptNo);
            this.layoutControl1.Controls.Add(this.txtTotalDue);
            this.layoutControl1.Controls.Add(this.cmdClose);
            this.layoutControl1.Controls.Add(this.cmdOk);
            this.layoutControl1.Controls.Add(this.txtPaidAmount);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(389, 268);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(115, 84);
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(262, 116);
            this.txtReason.StyleController = this.layoutControl1;
            this.txtReason.TabIndex = 21;
            // 
            // txtReceiptNo
            // 
            this.txtReceiptNo.Location = new System.Drawing.Point(115, 12);
            this.txtReceiptNo.Name = "txtReceiptNo";
            this.txtReceiptNo.Size = new System.Drawing.Size(262, 20);
            this.txtReceiptNo.StyleController = this.layoutControl1;
            this.txtReceiptNo.TabIndex = 20;
            this.txtReceiptNo.TabStop = false;
            // 
            // txtTotalDue
            // 
            this.txtTotalDue.Location = new System.Drawing.Point(115, 36);
            this.txtTotalDue.Name = "txtTotalDue";
            this.txtTotalDue.Properties.ReadOnly = true;
            this.txtTotalDue.Size = new System.Drawing.Size(262, 20);
            this.txtTotalDue.StyleController = this.layoutControl1;
            this.txtTotalDue.TabIndex = 17;
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(200, 204);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(177, 52);
            this.cmdClose.StyleController = this.layoutControl1;
            this.cmdClose.TabIndex = 13;
            this.cmdClose.Text = "Cancel";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(12, 204);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(184, 52);
            this.cmdOk.StyleController = this.layoutControl1;
            this.cmdOk.TabIndex = 12;
            this.cmdOk.Text = "Ok";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // txtPaidAmount
            // 
            this.txtPaidAmount.Location = new System.Drawing.Point(115, 60);
            this.txtPaidAmount.Name = "txtPaidAmount";
            this.txtPaidAmount.Size = new System.Drawing.Size(262, 20);
            this.txtPaidAmount.StyleController = this.layoutControl1;
            this.txtPaidAmount.TabIndex = 10;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.txbOk,
            this.txbClose,
            this.txbReceiptNo,
            this.txbTotalDue,
            this.txbPaidAmount,
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(389, 268);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // txbOk
            // 
            this.txbOk.Control = this.cmdOk;
            this.txbOk.CustomizationFormText = "txbOk";
            this.txbOk.Location = new System.Drawing.Point(0, 192);
            this.txbOk.MinSize = new System.Drawing.Size(26, 26);
            this.txbOk.Name = "txbOk";
            this.txbOk.Size = new System.Drawing.Size(188, 56);
            this.txbOk.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbOk.Text = "txbOk";
            this.txbOk.TextSize = new System.Drawing.Size(0, 0);
            this.txbOk.TextToControlDistance = 0;
            this.txbOk.TextVisible = false;
            // 
            // txbClose
            // 
            this.txbClose.Control = this.cmdClose;
            this.txbClose.CustomizationFormText = "txbClose";
            this.txbClose.Location = new System.Drawing.Point(188, 192);
            this.txbClose.MinSize = new System.Drawing.Size(83, 27);
            this.txbClose.Name = "txbClose";
            this.txbClose.Size = new System.Drawing.Size(181, 56);
            this.txbClose.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbClose.Text = "txbClose";
            this.txbClose.TextSize = new System.Drawing.Size(0, 0);
            this.txbClose.TextToControlDistance = 0;
            this.txbClose.TextVisible = false;
            // 
            // txbReceiptNo
            // 
            this.txbReceiptNo.Control = this.txtReceiptNo;
            this.txbReceiptNo.CustomizationFormText = "Receipt #";
            this.txbReceiptNo.Location = new System.Drawing.Point(0, 0);
            this.txbReceiptNo.Name = "txbReceiptNo";
            this.txbReceiptNo.Size = new System.Drawing.Size(369, 24);
            this.txbReceiptNo.Text = "Request #";
            this.txbReceiptNo.TextSize = new System.Drawing.Size(99, 13);
            // 
            // txbTotalDue
            // 
            this.txbTotalDue.Control = this.txtTotalDue;
            this.txbTotalDue.CustomizationFormText = "Due";
            this.txbTotalDue.Location = new System.Drawing.Point(0, 24);
            this.txbTotalDue.Name = "txbTotalDue";
            this.txbTotalDue.Size = new System.Drawing.Size(369, 24);
            this.txbTotalDue.Text = "Outstanding Balance";
            this.txbTotalDue.TextSize = new System.Drawing.Size(99, 13);
            // 
            // txbPaidAmount
            // 
            this.txbPaidAmount.Control = this.txtPaidAmount;
            this.txbPaidAmount.CustomizationFormText = "Amount to Request";
            this.txbPaidAmount.Location = new System.Drawing.Point(0, 48);
            this.txbPaidAmount.Name = "txbPaidAmount";
            this.txbPaidAmount.Size = new System.Drawing.Size(369, 24);
            this.txbPaidAmount.Text = "Amount to Request";
            this.txbPaidAmount.TextSize = new System.Drawing.Size(99, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtReason;
            this.layoutControlItem1.CustomizationFormText = "Reason for Relief";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(369, 120);
            this.layoutControlItem1.Text = "Reason for Relief";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(99, 13);
            // 
            // frmBILDebtRequestedAmount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 268);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBILDebtRequestedAmount";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Debt Request Amount";
            this.Load += new System.EventHandler(this.frmBILDebtRequestedAmount_Load);
            this.Shown += new System.EventHandler(this.frmBILDebtRequestedAmount_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtReason.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiptNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalDue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPaidAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbReceiptNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbTotalDue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbPaidAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
        private DevExpress.XtraEditors.SimpleButton cmdOk;
        private DevExpress.XtraLayout.LayoutControlItem txbOk;
        private DevExpress.XtraLayout.LayoutControlItem txbClose;
        private DevExpress.XtraLayout.LayoutControlItem txbTotalDue;
        private DevExpress.XtraEditors.TextEdit txtReceiptNo;
        private DevExpress.XtraLayout.LayoutControlItem txbReceiptNo;
        private DevExpress.XtraLayout.LayoutControlItem txbPaidAmount;
        private DevExpress.XtraEditors.MemoEdit txtReason;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        internal DevExpress.XtraEditors.TextEdit txtTotalDue;
        internal DevExpress.XtraEditors.TextEdit txtPaidAmount;
    }
}