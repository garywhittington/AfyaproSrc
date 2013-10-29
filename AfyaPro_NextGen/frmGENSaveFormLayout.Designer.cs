namespace AfyaPro_NextGen
{
    partial class frmGENSaveFormLayout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGENSaveFormLayout));
            this.cboLayoutTemplate = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txbLayoutTemplate = new DevExpress.XtraEditors.LabelControl();
            this.cmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cboLayoutTemplate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cboLayoutTemplate
            // 
            this.cboLayoutTemplate.Location = new System.Drawing.Point(7, 28);
            this.cboLayoutTemplate.Name = "cboLayoutTemplate";
            this.cboLayoutTemplate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboLayoutTemplate.Size = new System.Drawing.Size(254, 20);
            this.cboLayoutTemplate.TabIndex = 0;
            // 
            // txbLayoutTemplate
            // 
            this.txbLayoutTemplate.Location = new System.Drawing.Point(8, 8);
            this.txbLayoutTemplate.Name = "txbLayoutTemplate";
            this.txbLayoutTemplate.Size = new System.Drawing.Size(80, 13);
            this.txbLayoutTemplate.TabIndex = 1;
            this.txbLayoutTemplate.Text = "Layout Template";
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(6, 56);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(127, 44);
            this.cmdOk.TabIndex = 2;
            this.cmdOk.Text = "Save";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(133, 56);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(127, 44);
            this.cmdClose.TabIndex = 3;
            this.cmdClose.Text = "Cancel";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // frmGENSaveFormLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 107);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.txbLayoutTemplate);
            this.Controls.Add(this.cboLayoutTemplate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmGENSaveFormLayout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Save Layout";
            this.Load += new System.EventHandler(this.frmGENSaveFormLayout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboLayoutTemplate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit cboLayoutTemplate;
        private DevExpress.XtraEditors.LabelControl txbLayoutTemplate;
        private DevExpress.XtraEditors.SimpleButton cmdOk;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
    }
}