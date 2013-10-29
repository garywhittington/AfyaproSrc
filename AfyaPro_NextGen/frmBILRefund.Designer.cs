namespace AfyaPro_NextGen
{
    partial class frmBILRefund
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBILRefund));
            this.grdBILRefund = new DevExpress.XtraGrid.GridControl();
            this.viewBILRefund = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.paytypecode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.paytypedescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bank = new DevExpress.XtraGrid.Columns.GridColumn();
            this.branch = new DevExpress.XtraGrid.Columns.GridColumn();
            this.holder = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chequeno = new DevExpress.XtraGrid.Columns.GridColumn();
            this.paidamount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.refundedamount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txbReceiptNo = new DevExpress.XtraEditors.LabelControl();
            this.txtReceiptNo = new DevExpress.XtraEditors.TextEdit();
            this.cmdRefund = new DevExpress.XtraEditors.SimpleButton();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdBILRefund)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewBILRefund)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiptNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grdBILRefund
            // 
            this.grdBILRefund.Location = new System.Drawing.Point(9, 51);
            this.grdBILRefund.MainView = this.viewBILRefund;
            this.grdBILRefund.Name = "grdBILRefund";
            this.grdBILRefund.Size = new System.Drawing.Size(635, 274);
            this.grdBILRefund.TabIndex = 0;
            this.grdBILRefund.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewBILRefund});
            // 
            // viewBILRefund
            // 
            this.viewBILRefund.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.paytypecode,
            this.paytypedescription,
            this.bank,
            this.branch,
            this.holder,
            this.chequeno,
            this.paidamount,
            this.refundedamount});
            this.viewBILRefund.GridControl = this.grdBILRefund;
            this.viewBILRefund.Name = "viewBILRefund";
            this.viewBILRefund.OptionsMenu.EnableColumnMenu = false;
            this.viewBILRefund.OptionsMenu.EnableFooterMenu = false;
            this.viewBILRefund.OptionsMenu.EnableGroupPanelMenu = false;
            this.viewBILRefund.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.viewBILRefund.OptionsView.ShowFooter = true;
            this.viewBILRefund.OptionsView.ShowGroupPanel = false;
            this.viewBILRefund.OptionsView.ShowIndicator = false;
            this.viewBILRefund.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.viewBILRefund_ValidatingEditor);
            // 
            // paytypecode
            // 
            this.paytypecode.Caption = "paytypecode";
            this.paytypecode.FieldName = "paytypecode";
            this.paytypecode.Name = "paytypecode";
            this.paytypecode.OptionsColumn.AllowEdit = false;
            this.paytypecode.OptionsColumn.AllowFocus = false;
            // 
            // paytypedescription
            // 
            this.paytypedescription.Caption = "Payment Type";
            this.paytypedescription.FieldName = "paytypedescription";
            this.paytypedescription.Name = "paytypedescription";
            this.paytypedescription.OptionsColumn.AllowEdit = false;
            this.paytypedescription.OptionsColumn.AllowFocus = false;
            this.paytypedescription.Visible = true;
            this.paytypedescription.VisibleIndex = 0;
            // 
            // bank
            // 
            this.bank.Caption = "Bank";
            this.bank.FieldName = "bank";
            this.bank.Name = "bank";
            this.bank.OptionsColumn.AllowEdit = false;
            this.bank.OptionsColumn.AllowFocus = false;
            this.bank.Visible = true;
            this.bank.VisibleIndex = 1;
            // 
            // branch
            // 
            this.branch.Caption = "Branch";
            this.branch.FieldName = "branch";
            this.branch.Name = "branch";
            this.branch.OptionsColumn.AllowEdit = false;
            this.branch.OptionsColumn.AllowFocus = false;
            this.branch.Visible = true;
            this.branch.VisibleIndex = 2;
            // 
            // holder
            // 
            this.holder.Caption = "Holder";
            this.holder.FieldName = "holder";
            this.holder.Name = "holder";
            this.holder.OptionsColumn.AllowEdit = false;
            this.holder.OptionsColumn.AllowFocus = false;
            // 
            // chequeno
            // 
            this.chequeno.Caption = "Cheque #";
            this.chequeno.FieldName = "chequeno";
            this.chequeno.Name = "chequeno";
            this.chequeno.OptionsColumn.AllowEdit = false;
            this.chequeno.OptionsColumn.AllowFocus = false;
            this.chequeno.Visible = true;
            this.chequeno.VisibleIndex = 3;
            // 
            // paidamount
            // 
            this.paidamount.Caption = "Pending for Refund";
            this.paidamount.FieldName = "paidamount";
            this.paidamount.Name = "paidamount";
            this.paidamount.OptionsColumn.AllowEdit = false;
            this.paidamount.OptionsColumn.AllowFocus = false;
            this.paidamount.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.paidamount.Visible = true;
            this.paidamount.VisibleIndex = 4;
            // 
            // refundedamount
            // 
            this.refundedamount.Caption = "Refunded";
            this.refundedamount.FieldName = "refundedamount";
            this.refundedamount.Name = "refundedamount";
            this.refundedamount.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.refundedamount.Visible = true;
            this.refundedamount.VisibleIndex = 5;
            // 
            // txbReceiptNo
            // 
            this.txbReceiptNo.Location = new System.Drawing.Point(9, 8);
            this.txbReceiptNo.Name = "txbReceiptNo";
            this.txbReceiptNo.Size = new System.Drawing.Size(91, 13);
            this.txbReceiptNo.TabIndex = 1;
            this.txbReceiptNo.Text = "Selected Receipt #";
            // 
            // txtReceiptNo
            // 
            this.txtReceiptNo.Enabled = false;
            this.txtReceiptNo.Location = new System.Drawing.Point(9, 25);
            this.txtReceiptNo.Name = "txtReceiptNo";
            this.txtReceiptNo.Size = new System.Drawing.Size(146, 20);
            this.txtReceiptNo.TabIndex = 2;
            // 
            // cmdRefund
            // 
            this.cmdRefund.Location = new System.Drawing.Point(9, 331);
            this.cmdRefund.Name = "cmdRefund";
            this.cmdRefund.Size = new System.Drawing.Size(228, 54);
            this.cmdRefund.TabIndex = 3;
            this.cmdRefund.Text = "Process Refund";
            this.cmdRefund.Click += new System.EventHandler(this.cmdRefund_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(416, 331);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(228, 54);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // frmBILRefund
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 394);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdRefund);
            this.Controls.Add(this.txtReceiptNo);
            this.Controls.Add(this.txbReceiptNo);
            this.Controls.Add(this.grdBILRefund);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBILRefund";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payment Refund";
            this.Load += new System.EventHandler(this.frmBILRefund_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdBILRefund)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewBILRefund)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiptNo.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdBILRefund;
        private DevExpress.XtraGrid.Views.Grid.GridView viewBILRefund;
        private DevExpress.XtraEditors.LabelControl txbReceiptNo;
        private DevExpress.XtraEditors.SimpleButton cmdRefund;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
        private DevExpress.XtraGrid.Columns.GridColumn paytypecode;
        private DevExpress.XtraGrid.Columns.GridColumn paytypedescription;
        private DevExpress.XtraGrid.Columns.GridColumn bank;
        private DevExpress.XtraGrid.Columns.GridColumn branch;
        private DevExpress.XtraGrid.Columns.GridColumn holder;
        private DevExpress.XtraGrid.Columns.GridColumn chequeno;
        private DevExpress.XtraGrid.Columns.GridColumn paidamount;
        private DevExpress.XtraGrid.Columns.GridColumn refundedamount;
        internal DevExpress.XtraEditors.TextEdit txtReceiptNo;

    }
}