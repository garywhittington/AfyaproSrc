namespace AfyaPro_NextGen
{
    partial class frmIVQuickScanExpiryDates
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
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txbItemCode = new DevExpress.XtraEditors.LabelControl();
            this.txbDescription = new DevExpress.XtraEditors.LabelControl();
            this.txtDescription = new DevExpress.XtraEditors.TextEdit();
            this.txbQuantity = new DevExpress.XtraEditors.LabelControl();
            this.txtQuantity = new DevExpress.XtraEditors.TextEdit();
            this.cmdSearch = new DevExpress.XtraEditors.SimpleButton();
            this.cmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            this.lstExpiryDates = new DevExpress.XtraEditors.ListBoxControl();
            this.txtExpiryDate = new DevExpress.XtraEditors.DateEdit();
            this.txbExpiryDate = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstExpiryDates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpiryDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpiryDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(4, 22);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(122, 20);
            this.txtCode.TabIndex = 0;
            this.txtCode.EditValueChanged += new System.EventHandler(this.txtCode_EditValueChanged);
            this.txtCode.Leave += new System.EventHandler(this.txtCode_Leave);
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCode_KeyDown);
            // 
            // txbItemCode
            // 
            this.txbItemCode.Location = new System.Drawing.Point(4, 4);
            this.txbItemCode.Name = "txbItemCode";
            this.txbItemCode.Size = new System.Drawing.Size(50, 13);
            this.txbItemCode.TabIndex = 1;
            this.txbItemCode.Text = "Item Code";
            // 
            // txbDescription
            // 
            this.txbDescription.Location = new System.Drawing.Point(4, 46);
            this.txbDescription.Name = "txbDescription";
            this.txbDescription.Size = new System.Drawing.Size(53, 13);
            this.txbDescription.TabIndex = 3;
            this.txbDescription.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Enabled = false;
            this.txtDescription.Location = new System.Drawing.Point(4, 64);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(263, 20);
            this.txtDescription.TabIndex = 2;
            // 
            // txbQuantity
            // 
            this.txbQuantity.Location = new System.Drawing.Point(4, 232);
            this.txbQuantity.Name = "txbQuantity";
            this.txbQuantity.Size = new System.Drawing.Size(42, 13);
            this.txbQuantity.TabIndex = 5;
            this.txbQuantity.Text = "Quantity";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(4, 250);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(122, 20);
            this.txtQuantity.TabIndex = 3;
            this.txtQuantity.Enter += new System.EventHandler(this.txtQuantity_Enter);
            this.txtQuantity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuantity_KeyDown);
            // 
            // cmdSearch
            // 
            this.cmdSearch.Location = new System.Drawing.Point(129, 21);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(64, 22);
            this.cmdSearch.TabIndex = 1;
            this.cmdSearch.Text = "Search";
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(4, 277);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(122, 23);
            this.cmdOk.TabIndex = 4;
            this.cmdOk.Text = "&Accept";
            this.cmdOk.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(180, 277);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(87, 23);
            this.cmdClose.TabIndex = 5;
            this.cmdClose.Text = "&Done";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // lstExpiryDates
            // 
            this.lstExpiryDates.Location = new System.Drawing.Point(4, 92);
            this.lstExpiryDates.Name = "lstExpiryDates";
            this.lstExpiryDates.Size = new System.Drawing.Size(122, 95);
            this.lstExpiryDates.TabIndex = 6;
            this.lstExpiryDates.SelectedIndexChanged += new System.EventHandler(this.lstExpiryDates_SelectedIndexChanged);
            // 
            // txtExpiryDate
            // 
            this.txtExpiryDate.EditValue = null;
            this.txtExpiryDate.Location = new System.Drawing.Point(4, 209);
            this.txtExpiryDate.Name = "txtExpiryDate";
            this.txtExpiryDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtExpiryDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtExpiryDate.Size = new System.Drawing.Size(122, 20);
            this.txtExpiryDate.TabIndex = 7;
            // 
            // txbExpiryDate
            // 
            this.txbExpiryDate.Location = new System.Drawing.Point(4, 191);
            this.txbExpiryDate.Name = "txbExpiryDate";
            this.txbExpiryDate.Size = new System.Drawing.Size(56, 13);
            this.txbExpiryDate.TabIndex = 8;
            this.txbExpiryDate.Text = "Expiry Date";
            // 
            // frmIVQuickScanExpiryDates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 307);
            this.Controls.Add(this.txbExpiryDate);
            this.Controls.Add(this.txtExpiryDate);
            this.Controls.Add(this.lstExpiryDates);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.cmdSearch);
            this.Controls.Add(this.txbQuantity);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.txbDescription);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txbItemCode);
            this.Controls.Add(this.txtCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmIVQuickScanExpiryDates";
            this.Text = "Item Quick Scan";
            this.Load += new System.EventHandler(this.frmIVQuickScanExpiryDates_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstExpiryDates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpiryDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpiryDate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.LabelControl txbItemCode;
        private DevExpress.XtraEditors.LabelControl txbDescription;
        private DevExpress.XtraEditors.TextEdit txtDescription;
        private DevExpress.XtraEditors.LabelControl txbQuantity;
        private DevExpress.XtraEditors.TextEdit txtQuantity;
        private DevExpress.XtraEditors.SimpleButton cmdSearch;
        private DevExpress.XtraEditors.SimpleButton cmdOk;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
        private DevExpress.XtraEditors.ListBoxControl lstExpiryDates;
        private DevExpress.XtraEditors.DateEdit txtExpiryDate;
        private DevExpress.XtraEditors.LabelControl txbExpiryDate;
    }
}