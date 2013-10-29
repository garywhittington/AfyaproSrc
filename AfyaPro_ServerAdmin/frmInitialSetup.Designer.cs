namespace AfyaPro_ServerAdmin
{
    partial class frmInitialSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInitialSetup));
            this.wizardControl1 = new DevExpress.XtraWizard.WizardControl();
            this.welcomePage = new DevExpress.XtraWizard.WelcomeWizardPage();
            this.page0 = new DevExpress.XtraWizard.WizardPage();
            this.txtServerPort = new DevExpress.XtraEditors.TextEdit();
            this.txbServerPort = new DevExpress.XtraEditors.LabelControl();
            this.completionWizardPage1 = new DevExpress.XtraWizard.CompletionWizardPage();
            this.page1 = new DevExpress.XtraWizard.WizardPage();
            this.cmdImport = new DevExpress.XtraEditors.SimpleButton();
            this.cmdExport = new DevExpress.XtraEditors.SimpleButton();
            this.cboDBMSDatabase = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtDBMSPassword = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtDBMSUser = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.cboDBMSAuthentication = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtDBMSPort = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtDBMSServer = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cboDBMSType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txbServerType = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wizardControl1.SuspendLayout();
            this.page0.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerPort.Properties)).BeginInit();
            this.page1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboDBMSDatabase.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBMSPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBMSUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDBMSAuthentication.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBMSPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBMSServer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDBMSType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.Controls.Add(this.welcomePage);
            this.wizardControl1.Controls.Add(this.page0);
            this.wizardControl1.Controls.Add(this.completionWizardPage1);
            this.wizardControl1.Controls.Add(this.page1);
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.Image = global::AfyaPro_ServerAdmin.Properties.Resources.AfyaProLogoVertical;
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.welcomePage,
            this.page0,
            this.page1,
            this.completionWizardPage1});
            this.wizardControl1.Size = new System.Drawing.Size(502, 389);
            this.wizardControl1.SelectedPageChanging += new DevExpress.XtraWizard.WizardPageChangingEventHandler(this.wizardControl1_SelectedPageChanging);
            this.wizardControl1.CancelClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_CancelClick);
            this.wizardControl1.FinishClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_FinishClick);
            this.wizardControl1.NextClick += new DevExpress.XtraWizard.WizardCommandButtonClickEventHandler(this.wizardControl1_NextClick);
            // 
            // welcomePage
            // 
            this.welcomePage.IntroductionText = "This wizard will guide you through a series of steps towards configuring AfyaPro " +
                "server";
            this.welcomePage.Name = "welcomePage";
            this.welcomePage.Size = new System.Drawing.Size(285, 233);
            this.welcomePage.Text = "Welcome to the AfyaPro Server configuration wizard";
            // 
            // page0
            // 
            this.page0.Controls.Add(this.txtServerPort);
            this.page0.Controls.Add(this.txbServerPort);
            this.page0.DescriptionText = "Enter port # that will be used by the server. All clients will connect to this se" +
                "rver using the specified port #";
            this.page0.Name = "page0";
            this.page0.Size = new System.Drawing.Size(470, 244);
            this.page0.Text = "Server Port";
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(30, 103);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(100, 20);
            this.txtServerPort.TabIndex = 1;
            // 
            // txbServerPort
            // 
            this.txbServerPort.Location = new System.Drawing.Point(31, 86);
            this.txbServerPort.Name = "txbServerPort";
            this.txbServerPort.Size = new System.Drawing.Size(55, 13);
            this.txbServerPort.TabIndex = 0;
            this.txbServerPort.Text = "Server Port";
            // 
            // completionWizardPage1
            // 
            this.completionWizardPage1.Name = "completionWizardPage1";
            this.completionWizardPage1.Size = new System.Drawing.Size(285, 256);
            // 
            // page1
            // 
            this.page1.Controls.Add(this.cmdImport);
            this.page1.Controls.Add(this.cmdExport);
            this.page1.Controls.Add(this.cboDBMSDatabase);
            this.page1.Controls.Add(this.labelControl6);
            this.page1.Controls.Add(this.txtDBMSPassword);
            this.page1.Controls.Add(this.labelControl5);
            this.page1.Controls.Add(this.txtDBMSUser);
            this.page1.Controls.Add(this.labelControl4);
            this.page1.Controls.Add(this.cboDBMSAuthentication);
            this.page1.Controls.Add(this.labelControl3);
            this.page1.Controls.Add(this.txtDBMSPort);
            this.page1.Controls.Add(this.labelControl2);
            this.page1.Controls.Add(this.txtDBMSServer);
            this.page1.Controls.Add(this.labelControl1);
            this.page1.Controls.Add(this.cboDBMSType);
            this.page1.Controls.Add(this.txbServerType);
            this.page1.DescriptionText = "Provide the necessary detailsto be used by the server when connecting to DBMS";
            this.page1.Name = "page1";
            this.page1.Size = new System.Drawing.Size(470, 244);
            this.page1.Text = "DBMS Connection";
            // 
            // cmdImport
            // 
            this.cmdImport.Location = new System.Drawing.Point(231, 214);
            this.cmdImport.Name = "cmdImport";
            this.cmdImport.Size = new System.Drawing.Size(185, 21);
            this.cmdImport.TabIndex = 16;
            this.cmdImport.Text = "Import Default Settings";
            this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
            // 
            // cmdExport
            // 
            this.cmdExport.Location = new System.Drawing.Point(41, 214);
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.Size = new System.Drawing.Size(185, 21);
            this.cmdExport.TabIndex = 15;
            this.cmdExport.Text = "Export Default Settings";
            this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
            // 
            // cboDBMSDatabase
            // 
            this.cboDBMSDatabase.Location = new System.Drawing.Point(173, 182);
            this.cboDBMSDatabase.Name = "cboDBMSDatabase";
            this.cboDBMSDatabase.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDBMSDatabase.Size = new System.Drawing.Size(243, 20);
            this.cboDBMSDatabase.TabIndex = 13;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(42, 184);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(76, 13);
            this.labelControl6.TabIndex = 12;
            this.labelControl6.Text = "Database Name";
            // 
            // txtDBMSPassword
            // 
            this.txtDBMSPassword.Location = new System.Drawing.Point(173, 156);
            this.txtDBMSPassword.Name = "txtDBMSPassword";
            this.txtDBMSPassword.Properties.PasswordChar = '*';
            this.txtDBMSPassword.Size = new System.Drawing.Size(243, 20);
            this.txtDBMSPassword.TabIndex = 11;
            this.txtDBMSPassword.Leave += new System.EventHandler(this.txtDBMSPassword_Leave);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(42, 158);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(46, 13);
            this.labelControl5.TabIndex = 10;
            this.labelControl5.Text = "Password";
            // 
            // txtDBMSUser
            // 
            this.txtDBMSUser.Location = new System.Drawing.Point(173, 130);
            this.txtDBMSUser.Name = "txtDBMSUser";
            this.txtDBMSUser.Size = new System.Drawing.Size(243, 20);
            this.txtDBMSUser.TabIndex = 9;
            this.txtDBMSUser.Leave += new System.EventHandler(this.txtDBMSUser_Leave);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(42, 132);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(22, 13);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "User";
            // 
            // cboDBMSAuthentication
            // 
            this.cboDBMSAuthentication.Location = new System.Drawing.Point(173, 104);
            this.cboDBMSAuthentication.Name = "cboDBMSAuthentication";
            this.cboDBMSAuthentication.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDBMSAuthentication.Properties.Items.AddRange(new object[] {
            "Windows Authentication",
            "SQL Server Authentication"});
            this.cboDBMSAuthentication.Size = new System.Drawing.Size(243, 20);
            this.cboDBMSAuthentication.TabIndex = 7;
            this.cboDBMSAuthentication.Leave += new System.EventHandler(this.cboDBMSAuthentication_Leave);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(42, 106);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(70, 13);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Authentication";
            // 
            // txtDBMSPort
            // 
            this.txtDBMSPort.Location = new System.Drawing.Point(173, 78);
            this.txtDBMSPort.Name = "txtDBMSPort";
            this.txtDBMSPort.Size = new System.Drawing.Size(243, 20);
            this.txtDBMSPort.TabIndex = 5;
            this.txtDBMSPort.Leave += new System.EventHandler(this.txtDBMSPort_Leave);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(42, 80);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(31, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Port #";
            // 
            // txtDBMSServer
            // 
            this.txtDBMSServer.Location = new System.Drawing.Point(173, 52);
            this.txtDBMSServer.Name = "txtDBMSServer";
            this.txtDBMSServer.Size = new System.Drawing.Size(243, 20);
            this.txtDBMSServer.TabIndex = 3;
            this.txtDBMSServer.Leave += new System.EventHandler(this.txtDBMSServer_Leave);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(42, 54);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(62, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Server Name";
            // 
            // cboDBMSType
            // 
            this.cboDBMSType.Location = new System.Drawing.Point(173, 26);
            this.cboDBMSType.Name = "cboDBMSType";
            this.cboDBMSType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDBMSType.Properties.Items.AddRange(new object[] {
            "MySQL 4.1 and above",
            "Microsoft SQL Server 2000  and above"});
            this.cboDBMSType.Size = new System.Drawing.Size(243, 20);
            this.cboDBMSType.TabIndex = 1;
            this.cboDBMSType.Leave += new System.EventHandler(this.cboDBMSType_Leave);
            // 
            // txbServerType
            // 
            this.txbServerType.Location = new System.Drawing.Point(42, 28);
            this.txbServerType.Name = "txbServerType";
            this.txbServerType.Size = new System.Drawing.Size(54, 13);
            this.txbServerType.TabIndex = 0;
            this.txbServerType.Text = "DBMS Type";
            // 
            // frmInitialSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 389);
            this.Controls.Add(this.wizardControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInitialSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AfyaPro Server Configuration";
            this.Load += new System.EventHandler(this.frmInitialSetup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.wizardControl1.ResumeLayout(false);
            this.page0.ResumeLayout(false);
            this.page0.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerPort.Properties)).EndInit();
            this.page1.ResumeLayout(false);
            this.page1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboDBMSDatabase.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBMSPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBMSUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDBMSAuthentication.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBMSPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBMSServer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDBMSType.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraWizard.WizardControl wizardControl1;
        private DevExpress.XtraWizard.WelcomeWizardPage welcomePage;
        private DevExpress.XtraWizard.WizardPage page0;
        private DevExpress.XtraWizard.CompletionWizardPage completionWizardPage1;
        private DevExpress.XtraWizard.WizardPage page1;
        private DevExpress.XtraEditors.TextEdit txtServerPort;
        private DevExpress.XtraEditors.LabelControl txbServerPort;
        private DevExpress.XtraEditors.ComboBoxEdit cboDBMSType;
        private DevExpress.XtraEditors.LabelControl txbServerType;
        private DevExpress.XtraEditors.TextEdit txtDBMSServer;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cboDBMSAuthentication;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtDBMSPort;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtDBMSPassword;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtDBMSUser;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ComboBoxEdit cboDBMSDatabase;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SimpleButton cmdExport;
        private DevExpress.XtraEditors.SimpleButton cmdImport;
    }
}