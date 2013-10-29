namespace AfyaPro_NextGen
{
    partial class frmBILDepositAccountMembers
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
            this.cboAccount = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtGender = new DevExpress.XtraEditors.TextEdit();
            this.txbBirthDate = new DevExpress.XtraEditors.LabelControl();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            this.cmdSearch = new DevExpress.XtraEditors.SimpleButton();
            this.cmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.txtMonths = new DevExpress.XtraEditors.TextEdit();
            this.txtYears = new DevExpress.XtraEditors.TextEdit();
            this.txtBirthDate = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtPatientId = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbClose = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbAccount = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbSearch = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbPatientId = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbName = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbBirthDateHolder = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbBirthDateFormat = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbYears = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbMonths = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbGender = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.cboAccount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtGender.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonths.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYears.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBirthDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPatientId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbPatientId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbBirthDateHolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbBirthDateFormat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbYears)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbMonths)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbGender)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboAccount
            // 
            this.cboAccount.Location = new System.Drawing.Point(88, 24);
            this.cboAccount.Name = "cboAccount";
            this.cboAccount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboAccount.Properties.NullText = "";
            this.cboAccount.Size = new System.Drawing.Size(222, 20);
            this.cboAccount.StyleController = this.layoutControl1;
            this.cboAccount.TabIndex = 1;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtGender);
            this.layoutControl1.Controls.Add(this.txbBirthDate);
            this.layoutControl1.Controls.Add(this.cboAccount);
            this.layoutControl1.Controls.Add(this.cmdClose);
            this.layoutControl1.Controls.Add(this.cmdSearch);
            this.layoutControl1.Controls.Add(this.cmdOk);
            this.layoutControl1.Controls.Add(this.txtMonths);
            this.layoutControl1.Controls.Add(this.txtYears);
            this.layoutControl1.Controls.Add(this.txtBirthDate);
            this.layoutControl1.Controls.Add(this.txtName);
            this.layoutControl1.Controls.Add(this.txtPatientId);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(334, 308);
            this.layoutControl1.TabIndex = 20;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtGender
            // 
            this.txtGender.Location = new System.Drawing.Point(100, 202);
            this.txtGender.Name = "txtGender";
            this.txtGender.Properties.ReadOnly = true;
            this.txtGender.Size = new System.Drawing.Size(198, 20);
            this.txtGender.StyleController = this.layoutControl1;
            this.txtGender.TabIndex = 28;
            // 
            // txbBirthDate
            // 
            this.txbBirthDate.Enabled = false;
            this.txbBirthDate.Location = new System.Drawing.Point(36, 157);
            this.txbBirthDate.Name = "txbBirthDate";
            this.txbBirthDate.Size = new System.Drawing.Size(48, 13);
            this.txbBirthDate.StyleController = this.layoutControl1;
            this.txbBirthDate.TabIndex = 27;
            this.txbBirthDate.Text = "Birth Date";
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(175, 248);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(149, 50);
            this.cmdClose.StyleController = this.layoutControl1;
            this.cmdClose.TabIndex = 16;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSearch
            // 
            this.cmdSearch.Location = new System.Drawing.Point(34, 80);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(264, 22);
            this.cmdSearch.StyleController = this.layoutControl1;
            this.cmdSearch.TabIndex = 2;
            this.cmdSearch.Text = "Search a Patient";
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(10, 248);
            this.cmdOk.Margin = new System.Windows.Forms.Padding(0);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(165, 50);
            this.cmdOk.StyleController = this.layoutControl1;
            this.cmdOk.TabIndex = 15;
            this.cmdOk.Text = "Save";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // txtMonths
            // 
            this.txtMonths.Location = new System.Drawing.Point(234, 178);
            this.txtMonths.Name = "txtMonths";
            this.txtMonths.Properties.ReadOnly = true;
            this.txtMonths.Size = new System.Drawing.Size(64, 20);
            this.txtMonths.StyleController = this.layoutControl1;
            this.txtMonths.TabIndex = 9;
            // 
            // txtYears
            // 
            this.txtYears.Location = new System.Drawing.Point(100, 178);
            this.txtYears.Name = "txtYears";
            this.txtYears.Properties.ReadOnly = true;
            this.txtYears.Size = new System.Drawing.Size(66, 20);
            this.txtYears.StyleController = this.layoutControl1;
            this.txtYears.TabIndex = 8;
            // 
            // txtBirthDate
            // 
            this.txtBirthDate.Location = new System.Drawing.Point(155, 154);
            this.txtBirthDate.Name = "txtBirthDate";
            this.txtBirthDate.Properties.ReadOnly = true;
            this.txtBirthDate.Size = new System.Drawing.Size(143, 20);
            this.txtBirthDate.StyleController = this.layoutControl1;
            this.txtBirthDate.TabIndex = 7;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(100, 130);
            this.txtName.Name = "txtName";
            this.txtName.Properties.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(198, 20);
            this.txtName.StyleController = this.layoutControl1;
            this.txtName.TabIndex = 3;
            // 
            // txtPatientId
            // 
            this.txtPatientId.Location = new System.Drawing.Point(100, 106);
            this.txtPatientId.Name = "txtPatientId";
            this.txtPatientId.Size = new System.Drawing.Size(200, 20);
            this.txtPatientId.StyleController = this.layoutControl1;
            this.txtPatientId.TabIndex = 1;
            this.txtPatientId.EditValueChanged += new System.EventHandler(this.txtPatientId_EditValueChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.txbSave,
            this.txbClose,
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(334, 308);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // txbSave
            // 
            this.txbSave.Control = this.cmdOk;
            this.txbSave.CustomizationFormText = "Save";
            this.txbSave.Location = new System.Drawing.Point(0, 238);
            this.txbSave.MinSize = new System.Drawing.Size(48, 33);
            this.txbSave.Name = "txbSave";
            this.txbSave.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.txbSave.Size = new System.Drawing.Size(165, 50);
            this.txbSave.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbSave.Text = "Save";
            this.txbSave.TextSize = new System.Drawing.Size(0, 0);
            this.txbSave.TextToControlDistance = 0;
            this.txbSave.TextVisible = false;
            // 
            // txbClose
            // 
            this.txbClose.Control = this.cmdClose;
            this.txbClose.CustomizationFormText = "Close";
            this.txbClose.Location = new System.Drawing.Point(165, 238);
            this.txbClose.MinSize = new System.Drawing.Size(50, 33);
            this.txbClose.Name = "txbClose";
            this.txbClose.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.txbClose.Size = new System.Drawing.Size(149, 50);
            this.txbClose.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbClose.Text = "Close";
            this.txbClose.TextSize = new System.Drawing.Size(0, 0);
            this.txbClose.TextToControlDistance = 0;
            this.txbClose.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.txbAccount,
            this.layoutControlGroup3});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(314, 238);
            this.layoutControlGroup2.Text = "layoutControlGroup2";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // txbAccount
            // 
            this.txbAccount.Control = this.cboAccount;
            this.txbAccount.CustomizationFormText = "Account";
            this.txbAccount.Location = new System.Drawing.Point(0, 0);
            this.txbAccount.Name = "txbAccount";
            this.txbAccount.Size = new System.Drawing.Size(290, 24);
            this.txbAccount.Text = "Account";
            this.txbAccount.TextSize = new System.Drawing.Size(60, 13);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "Patient Info";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.txbSearch,
            this.txbPatientId,
            this.txbName,
            this.txbBirthDateHolder,
            this.txbBirthDateFormat,
            this.txbYears,
            this.txbMonths,
            this.txbGender});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 24);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(290, 190);
            this.layoutControlGroup3.Text = "Patient Info";
            // 
            // txbSearch
            // 
            this.txbSearch.Control = this.cmdSearch;
            this.txbSearch.CustomizationFormText = "Search";
            this.txbSearch.Location = new System.Drawing.Point(0, 0);
            this.txbSearch.Name = "txbSearch";
            this.txbSearch.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 2, 2, 2);
            this.txbSearch.Size = new System.Drawing.Size(266, 26);
            this.txbSearch.Text = "txbSearch";
            this.txbSearch.TextSize = new System.Drawing.Size(0, 0);
            this.txbSearch.TextToControlDistance = 0;
            this.txbSearch.TextVisible = false;
            // 
            // txbPatientId
            // 
            this.txbPatientId.Control = this.txtPatientId;
            this.txbPatientId.CustomizationFormText = "Patient #";
            this.txbPatientId.Location = new System.Drawing.Point(0, 26);
            this.txbPatientId.Name = "txbPatientId";
            this.txbPatientId.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 2);
            this.txbPatientId.Size = new System.Drawing.Size(266, 24);
            this.txbPatientId.Text = "Patient #";
            this.txbPatientId.TextSize = new System.Drawing.Size(60, 13);
            // 
            // txbName
            // 
            this.txbName.Control = this.txtName;
            this.txbName.CustomizationFormText = "Surname";
            this.txbName.Location = new System.Drawing.Point(0, 50);
            this.txbName.Name = "txbName";
            this.txbName.Size = new System.Drawing.Size(266, 24);
            this.txbName.Text = "Name";
            this.txbName.TextSize = new System.Drawing.Size(60, 13);
            // 
            // txbBirthDateHolder
            // 
            this.txbBirthDateHolder.Control = this.txbBirthDate;
            this.txbBirthDateHolder.CustomizationFormText = "txbBirthDateHolder";
            this.txbBirthDateHolder.Location = new System.Drawing.Point(0, 74);
            this.txbBirthDateHolder.Name = "txbBirthDateHolder";
            this.txbBirthDateHolder.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 5, 5, 5);
            this.txbBirthDateHolder.Size = new System.Drawing.Size(55, 24);
            this.txbBirthDateHolder.Text = "txbBirthDateHolder";
            this.txbBirthDateHolder.TextSize = new System.Drawing.Size(0, 0);
            this.txbBirthDateHolder.TextToControlDistance = 0;
            this.txbBirthDateHolder.TextVisible = false;
            // 
            // txbBirthDateFormat
            // 
            this.txbBirthDateFormat.Control = this.txtBirthDate;
            this.txbBirthDateFormat.CustomizationFormText = "Birth Date";
            this.txbBirthDateFormat.Location = new System.Drawing.Point(55, 74);
            this.txbBirthDateFormat.Name = "txbBirthDateFormat";
            this.txbBirthDateFormat.Size = new System.Drawing.Size(211, 24);
            this.txbBirthDateFormat.Text = "Date Format";
            this.txbBirthDateFormat.TextSize = new System.Drawing.Size(60, 13);
            // 
            // txbYears
            // 
            this.txbYears.Control = this.txtYears;
            this.txbYears.CustomizationFormText = "Age   Years";
            this.txbYears.Location = new System.Drawing.Point(0, 98);
            this.txbYears.Name = "txbYears";
            this.txbYears.Size = new System.Drawing.Size(134, 24);
            this.txbYears.Text = "Age   Years";
            this.txbYears.TextSize = new System.Drawing.Size(60, 13);
            // 
            // txbMonths
            // 
            this.txbMonths.Control = this.txtMonths;
            this.txbMonths.CustomizationFormText = "Months";
            this.txbMonths.Location = new System.Drawing.Point(134, 98);
            this.txbMonths.Name = "txbMonths";
            this.txbMonths.Size = new System.Drawing.Size(132, 24);
            this.txbMonths.Text = "Months";
            this.txbMonths.TextSize = new System.Drawing.Size(60, 13);
            // 
            // txbGender
            // 
            this.txbGender.Control = this.txtGender;
            this.txbGender.CustomizationFormText = "txbGender";
            this.txbGender.Location = new System.Drawing.Point(0, 122);
            this.txbGender.Name = "txbGender";
            this.txbGender.Size = new System.Drawing.Size(266, 24);
            this.txbGender.Text = "txbGender";
            this.txbGender.TextSize = new System.Drawing.Size(60, 13);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(334, 308);
            this.splitContainer1.SplitterDistance = 179;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 13;
            // 
            // frmBILDepositAccountMembers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 308);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBILDepositAccountMembers";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Deposit Account Members";
            this.Load += new System.EventHandler(this.frmBILDepositAccountMembers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboAccount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtGender.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonths.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYears.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBirthDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPatientId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbPatientId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbBirthDateHolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbBirthDateFormat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbYears)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbMonths)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbGender)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.LookUpEdit cboAccount;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.LabelControl txbBirthDate;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
        private DevExpress.XtraEditors.SimpleButton cmdSearch;
        private DevExpress.XtraEditors.SimpleButton cmdOk;
        private DevExpress.XtraEditors.TextEdit txtMonths;
        private DevExpress.XtraEditors.TextEdit txtYears;
        private DevExpress.XtraEditors.TextEdit txtBirthDate;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtPatientId;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem txbSave;
        private DevExpress.XtraLayout.LayoutControlItem txbClose;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem txbYears;
        private DevExpress.XtraLayout.LayoutControlItem txbMonths;
        private DevExpress.XtraLayout.LayoutControlItem txbBirthDateFormat;
        private DevExpress.XtraLayout.LayoutControlItem txbName;
        private DevExpress.XtraLayout.LayoutControlItem txbPatientId;
        private DevExpress.XtraLayout.LayoutControlItem txbSearch;
        private DevExpress.XtraLayout.LayoutControlItem txbBirthDateHolder;
        private DevExpress.XtraLayout.LayoutControlItem txbAccount;
        private DevExpress.XtraEditors.TextEdit txtGender;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem txbGender;
    }
}