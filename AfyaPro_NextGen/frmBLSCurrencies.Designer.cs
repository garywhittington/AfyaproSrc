namespace AfyaPro_NextGen
{
    partial class frmBLSCurrencies
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
            this.txtDescription = new DevExpress.XtraEditors.TextEdit();
            this.txbDescription = new DevExpress.XtraEditors.LabelControl();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txbCode = new DevExpress.XtraEditors.LabelControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtExchangeRate = new DevExpress.XtraEditors.TextEdit();
            this.txbExchangeRate = new DevExpress.XtraEditors.LabelControl();
            this.txtSymbol = new DevExpress.XtraEditors.TextEdit();
            this.txbSymbol = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtExchangeRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSymbol.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.txtSymbol);
            this.groupControl2.Controls.Add(this.txbSymbol);
            this.groupControl2.Controls.Add(this.txtExchangeRate);
            this.groupControl2.Controls.Add(this.txbExchangeRate);
            this.groupControl2.Controls.Add(this.txtDescription);
            this.groupControl2.Controls.Add(this.txbDescription);
            this.groupControl2.Controls.Add(this.txtCode);
            this.groupControl2.Controls.Add(this.txbCode);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(317, 143);
            this.groupControl2.TabIndex = 11;
            this.groupControl2.Text = "groupControl2";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(112, 49);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(193, 20);
            this.txtDescription.TabIndex = 2;
            // 
            // txbDescription
            // 
            this.txbDescription.Location = new System.Drawing.Point(9, 52);
            this.txbDescription.Name = "txbDescription";
            this.txbDescription.Size = new System.Drawing.Size(53, 13);
            this.txbDescription.TabIndex = 3;
            this.txbDescription.Text = "Description";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(112, 22);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(100, 20);
            this.txtCode.TabIndex = 1;
            // 
            // txbCode
            // 
            this.txbCode.Location = new System.Drawing.Point(9, 25);
            this.txbCode.Name = "txbCode";
            this.txbCode.Size = new System.Drawing.Size(25, 13);
            this.txbCode.TabIndex = 1;
            this.txbCode.Text = "Code";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 143);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cmdOk);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cmdClose);
            this.splitContainer1.Size = new System.Drawing.Size(317, 57);
            this.splitContainer1.SplitterDistance = 153;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 13;
            // 
            // cmdOk
            // 
            this.cmdOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdOk.Location = new System.Drawing.Point(0, 0);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(153, 57);
            this.cmdOk.TabIndex = 5;
            this.cmdOk.Text = "Save";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdClose.Location = new System.Drawing.Point(0, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(163, 57);
            this.cmdClose.TabIndex = 6;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // txtExchangeRate
            // 
            this.txtExchangeRate.Location = new System.Drawing.Point(112, 76);
            this.txtExchangeRate.Name = "txtExchangeRate";
            this.txtExchangeRate.Size = new System.Drawing.Size(100, 20);
            this.txtExchangeRate.TabIndex = 3;
            // 
            // txbExchangeRate
            // 
            this.txbExchangeRate.Location = new System.Drawing.Point(9, 79);
            this.txbExchangeRate.Name = "txbExchangeRate";
            this.txbExchangeRate.Size = new System.Drawing.Size(73, 13);
            this.txbExchangeRate.TabIndex = 4;
            this.txbExchangeRate.Text = "Exchange Rate";
            // 
            // txtSymbol
            // 
            this.txtSymbol.Location = new System.Drawing.Point(112, 103);
            this.txtSymbol.Name = "txtSymbol";
            this.txtSymbol.Size = new System.Drawing.Size(100, 20);
            this.txtSymbol.TabIndex = 4;
            // 
            // txbSymbol
            // 
            this.txbSymbol.Location = new System.Drawing.Point(9, 106);
            this.txbSymbol.Name = "txbSymbol";
            this.txbSymbol.Size = new System.Drawing.Size(34, 13);
            this.txbSymbol.TabIndex = 6;
            this.txbSymbol.Text = "Symbol";
            // 
            // frmBLSCurrencies
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 200);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBLSCurrencies";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Currencies";
            this.Load += new System.EventHandler(this.frmBLSCurrencies_Load);
            this.Activated += new System.EventHandler(this.frmBLSCurrencies_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtExchangeRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSymbol.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl txbCode;
        private DevExpress.XtraEditors.TextEdit txtDescription;
        private DevExpress.XtraEditors.LabelControl txbDescription;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SimpleButton cmdOk;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
        private DevExpress.XtraEditors.TextEdit txtSymbol;
        private DevExpress.XtraEditors.LabelControl txbSymbol;
        private DevExpress.XtraEditors.TextEdit txtExchangeRate;
        private DevExpress.XtraEditors.LabelControl txbExchangeRate;
    }
}