namespace AfyaPro_NextGen
{
    partial class frmRCHAntenatalVisitDetails1
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.cmdBrowse = new DevExpress.XtraEditors.SimpleButton();
            this.cmdCopy = new DevExpress.XtraEditors.SimpleButton();
            this.txbText = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.cmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.txtActivationKey = new DevExpress.XtraEditors.MemoEdit();
            this.txtInstallationId = new DevExpress.XtraEditors.TextEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.cmdContinue = new DevExpress.XtraEditors.SimpleButton();
            this.txbDays = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtActivationKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInstallationId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 67);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(71, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Installation ID:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(9, 111);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(80, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Activation Code:";
            // 
            // labelControl3
            // 
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl3.Location = new System.Drawing.Point(5, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(465, 26);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "To use this application, you must purchase. Before purchasing you can use this ap" +
                "plication in trial mode with all of its features activated for specified number " +
                "of days.";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.cmdBrowse);
            this.groupControl1.Controls.Add(this.cmdCopy);
            this.groupControl1.Controls.Add(this.txbText);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.cmdOk);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtActivationKey);
            this.groupControl1.Controls.Add(this.txtInstallationId);
            this.groupControl1.Location = new System.Drawing.Point(4, 36);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(466, 293);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "Registration Info";
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.Location = new System.Drawing.Point(385, 130);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(75, 23);
            this.cmdBrowse.TabIndex = 9;
            this.cmdBrowse.Text = "Browse";
            this.cmdBrowse.Click += new System.EventHandler(this.cmdPasteId_Click);
            // 
            // cmdCopy
            // 
            this.cmdCopy.Location = new System.Drawing.Point(385, 84);
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.Size = new System.Drawing.Size(75, 22);
            this.cmdCopy.TabIndex = 7;
            this.cmdCopy.Text = "Copy ID";
            this.cmdCopy.Visible = false;
            this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
            // 
            // txbText
            // 
            this.txbText.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.txbText.Location = new System.Drawing.Point(7, 270);
            this.txbText.Name = "txbText";
            this.txbText.Size = new System.Drawing.Size(451, 13);
            this.txbText.TabIndex = 6;
            this.txbText.Text = "Phone #: +255 22 2771573/4";
            // 
            // labelControl4
            // 
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl4.Location = new System.Drawing.Point(7, 26);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(451, 26);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "Copy your installation Id below and send it to your shopping part. They will give" +
                " you activation code for this application";
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(385, 155);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(75, 23);
            this.cmdOk.TabIndex = 2;
            this.cmdOk.Text = "Activate";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // txtActivationKey
            // 
            this.txtActivationKey.Location = new System.Drawing.Point(8, 130);
            this.txtActivationKey.Name = "txtActivationKey";
            this.txtActivationKey.Properties.Appearance.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtActivationKey.Properties.Appearance.Options.UseForeColor = true;
            this.txtActivationKey.Properties.ReadOnly = true;
            this.txtActivationKey.Size = new System.Drawing.Size(372, 136);
            this.txtActivationKey.TabIndex = 1;
            this.txtActivationKey.TabStop = false;
            // 
            // txtInstallationId
            // 
            this.txtInstallationId.Location = new System.Drawing.Point(8, 85);
            this.txtInstallationId.Name = "txtInstallationId";
            this.txtInstallationId.Properties.ReadOnly = true;
            this.txtInstallationId.Size = new System.Drawing.Size(372, 20);
            this.txtInstallationId.TabIndex = 8;
            this.txtInstallationId.TabStop = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.cmdContinue);
            this.groupControl2.Controls.Add(this.txbDays);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Location = new System.Drawing.Point(4, 335);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(466, 70);
            this.groupControl2.TabIndex = 6;
            this.groupControl2.Text = "Trial Running";
            // 
            // cmdContinue
            // 
            this.cmdContinue.Location = new System.Drawing.Point(385, 37);
            this.cmdContinue.Name = "cmdContinue";
            this.cmdContinue.Size = new System.Drawing.Size(75, 23);
            this.cmdContinue.TabIndex = 3;
            this.cmdContinue.Text = "Continue";
            this.cmdContinue.Click += new System.EventHandler(this.cmdContinue_Click);
            // 
            // txbDays
            // 
            this.txbDays.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txbDays.Appearance.Options.UseForeColor = true;
            this.txbDays.Location = new System.Drawing.Point(128, 37);
            this.txbDays.Name = "txbDays";
            this.txbDays.Size = new System.Drawing.Size(32, 13);
            this.txbDays.TabIndex = 1;
            this.txbDays.Text = "[Days]";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(9, 37);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(112, 13);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "Days to end trial period";
            // 
            // frmActivation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 412);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.labelControl3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRCHAntenatalVisitDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Antenatal care visit";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtActivationKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInstallationId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton cmdOk;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl txbText;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl txbDays;
        private DevExpress.XtraEditors.SimpleButton cmdContinue;
        private DevExpress.XtraEditors.MemoEdit txtActivationKey;
        private DevExpress.XtraEditors.SimpleButton cmdCopy;
        private DevExpress.XtraEditors.SimpleButton cmdBrowse;
        private DevExpress.XtraEditors.TextEdit txtInstallationId;
    }
}