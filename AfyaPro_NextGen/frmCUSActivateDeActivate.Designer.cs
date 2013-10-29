namespace AfyaPro_NextGen
{
    partial class frmCUSActivateDeActivate
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
            this.cboGroup = new DevExpress.XtraEditors.LookUpEdit();
            this.chkActive = new DevExpress.XtraEditors.CheckEdit();
            this.cmdSearch = new DevExpress.XtraEditors.SimpleButton();
            this.picPatient = new DevExpress.XtraEditors.PictureEdit();
            this.txbExpiryDate = new DevExpress.XtraEditors.LabelControl();
            this.txtExpiryDate = new DevExpress.XtraEditors.DateEdit();
            this.txtCeilingAmount = new DevExpress.XtraEditors.TextEdit();
            this.txbCeilingAmount = new DevExpress.XtraEditors.LabelControl();
            this.txtMembershipNo = new DevExpress.XtraEditors.TextEdit();
            this.txbMembershipNo = new DevExpress.XtraEditors.LabelControl();
            this.txbGroup = new DevExpress.XtraEditors.LabelControl();
            this.txtFullName = new DevExpress.XtraEditors.TextEdit();
            this.txbFullName = new DevExpress.XtraEditors.LabelControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPatient.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpiryDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpiryDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCeilingAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMembershipNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFullName.Properties)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.txtCode);
            this.groupControl2.Controls.Add(this.cboGroup);
            this.groupControl2.Controls.Add(this.chkActive);
            this.groupControl2.Controls.Add(this.cmdSearch);
            this.groupControl2.Controls.Add(this.picPatient);
            this.groupControl2.Controls.Add(this.txbExpiryDate);
            this.groupControl2.Controls.Add(this.txtExpiryDate);
            this.groupControl2.Controls.Add(this.txtCeilingAmount);
            this.groupControl2.Controls.Add(this.txbCeilingAmount);
            this.groupControl2.Controls.Add(this.txtMembershipNo);
            this.groupControl2.Controls.Add(this.txbMembershipNo);
            this.groupControl2.Controls.Add(this.txbGroup);
            this.groupControl2.Controls.Add(this.txtFullName);
            this.groupControl2.Controls.Add(this.txbFullName);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(483, 179);
            this.groupControl2.TabIndex = 11;
            this.groupControl2.Text = "groupControl2";
            // 
            // cboGroup
            // 
            this.cboGroup.Location = new System.Drawing.Point(112, 12);
            this.cboGroup.Name = "cboGroup";
            this.cboGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboGroup.Properties.NullText = "";
            this.cboGroup.Size = new System.Drawing.Size(193, 20);
            this.cboGroup.TabIndex = 1;
            this.cboGroup.EditValueChanged += new System.EventHandler(this.cboGroup_EditValueChanged);
            // 
            // chkActive
            // 
            this.chkActive.Location = new System.Drawing.Point(110, 146);
            this.chkActive.Name = "chkActive";
            this.chkActive.Properties.Caption = "Activate";
            this.chkActive.Size = new System.Drawing.Size(189, 19);
            this.chkActive.TabIndex = 7;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Location = new System.Drawing.Point(246, 37);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(59, 22);
            this.cmdSearch.TabIndex = 3;
            this.cmdSearch.Text = "Search";
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // picPatient
            // 
            this.picPatient.Location = new System.Drawing.Point(311, 10);
            this.picPatient.Name = "picPatient";
            this.picPatient.Properties.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.ByteArray;
            this.picPatient.Properties.ShowMenu = false;
            this.picPatient.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picPatient.Size = new System.Drawing.Size(161, 155);
            this.picPatient.TabIndex = 10;
            // 
            // txbExpiryDate
            // 
            this.txbExpiryDate.Location = new System.Drawing.Point(9, 120);
            this.txbExpiryDate.Name = "txbExpiryDate";
            this.txbExpiryDate.Size = new System.Drawing.Size(69, 13);
            this.txbExpiryDate.TabIndex = 14;
            this.txbExpiryDate.Text = "Id Expiry Date";
            // 
            // txtExpiryDate
            // 
            this.txtExpiryDate.EditValue = null;
            this.txtExpiryDate.Enabled = false;
            this.txtExpiryDate.Location = new System.Drawing.Point(112, 117);
            this.txtExpiryDate.Name = "txtExpiryDate";
            this.txtExpiryDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtExpiryDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtExpiryDate.Size = new System.Drawing.Size(100, 20);
            this.txtExpiryDate.TabIndex = 6;
            // 
            // txtCeilingAmount
            // 
            this.txtCeilingAmount.Enabled = false;
            this.txtCeilingAmount.Location = new System.Drawing.Point(112, 91);
            this.txtCeilingAmount.Name = "txtCeilingAmount";
            this.txtCeilingAmount.Size = new System.Drawing.Size(100, 20);
            this.txtCeilingAmount.TabIndex = 5;
            // 
            // txbCeilingAmount
            // 
            this.txbCeilingAmount.Location = new System.Drawing.Point(9, 94);
            this.txbCeilingAmount.Name = "txbCeilingAmount";
            this.txbCeilingAmount.Size = new System.Drawing.Size(71, 13);
            this.txbCeilingAmount.TabIndex = 11;
            this.txbCeilingAmount.Text = "Ceiling Amount";
            // 
            // txtMembershipNo
            // 
            this.txtMembershipNo.Location = new System.Drawing.Point(112, 38);
            this.txtMembershipNo.Name = "txtMembershipNo";
            this.txtMembershipNo.Size = new System.Drawing.Size(132, 20);
            this.txtMembershipNo.TabIndex = 2;
            this.txtMembershipNo.Leave += new System.EventHandler(this.txtMembershipNo_Leave);
            this.txtMembershipNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMembershipNo_KeyDown);
            // 
            // txbMembershipNo
            // 
            this.txbMembershipNo.Location = new System.Drawing.Point(9, 41);
            this.txbMembershipNo.Name = "txbMembershipNo";
            this.txbMembershipNo.Size = new System.Drawing.Size(70, 13);
            this.txbMembershipNo.TabIndex = 9;
            this.txbMembershipNo.Text = "Membership Id";
            // 
            // txbGroup
            // 
            this.txbGroup.Location = new System.Drawing.Point(9, 15);
            this.txbGroup.Name = "txbGroup";
            this.txbGroup.Size = new System.Drawing.Size(29, 13);
            this.txbGroup.TabIndex = 6;
            this.txbGroup.Text = "Group";
            // 
            // txtFullName
            // 
            this.txtFullName.Enabled = false;
            this.txtFullName.Location = new System.Drawing.Point(112, 64);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(193, 20);
            this.txtFullName.TabIndex = 4;
            // 
            // txbFullName
            // 
            this.txbFullName.Location = new System.Drawing.Point(9, 67);
            this.txbFullName.Name = "txbFullName";
            this.txbFullName.Size = new System.Drawing.Size(46, 13);
            this.txbFullName.TabIndex = 3;
            this.txbFullName.Text = "Full Name";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 179);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cmdOk);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cmdClose);
            this.splitContainer1.Size = new System.Drawing.Size(483, 53);
            this.splitContainer1.SplitterDistance = 297;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 13;
            // 
            // cmdOk
            // 
            this.cmdOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdOk.Location = new System.Drawing.Point(0, 0);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(297, 53);
            this.cmdOk.TabIndex = 8;
            this.cmdOk.Text = "Save";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdClose.Location = new System.Drawing.Point(0, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(185, 53);
            this.cmdClose.TabIndex = 9;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(238, 120);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(59, 20);
            this.txtCode.TabIndex = 15;
            this.txtCode.Visible = false;
            this.txtCode.EditValueChanged += new System.EventHandler(this.txtCode_EditValueChanged);
            // 
            // frmCUSActivateDeActivate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 232);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmCUSActivateDeActivate";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Activate/DeActivate Member";
            this.Load += new System.EventHandler(this.frmCUSActivateDeActivate_Load);
            this.Activated += new System.EventHandler(this.frmCUSActivateDeActivate_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPatient.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpiryDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpiryDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCeilingAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMembershipNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFullName.Properties)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.TextEdit txtFullName;
        private DevExpress.XtraEditors.LabelControl txbFullName;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SimpleButton cmdOk;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
        private DevExpress.XtraEditors.LabelControl txbGroup;
        private DevExpress.XtraEditors.TextEdit txtCeilingAmount;
        private DevExpress.XtraEditors.LabelControl txbCeilingAmount;
        private DevExpress.XtraEditors.TextEdit txtMembershipNo;
        private DevExpress.XtraEditors.LabelControl txbMembershipNo;
        private DevExpress.XtraEditors.LabelControl txbExpiryDate;
        private DevExpress.XtraEditors.DateEdit txtExpiryDate;
        private DevExpress.XtraEditors.PictureEdit picPatient;
        private DevExpress.XtraEditors.SimpleButton cmdSearch;
        private DevExpress.XtraEditors.CheckEdit chkActive;
        private DevExpress.XtraEditors.LookUpEdit cboGroup;
        private DevExpress.XtraEditors.TextEdit txtCode;
    }
}