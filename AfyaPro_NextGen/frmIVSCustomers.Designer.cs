namespace AfyaPro_NextGen
{
    partial class frmIVSCustomers
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
            this.txtPhone = new DevExpress.XtraEditors.TextEdit();
            this.txbPhone = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txbName = new DevExpress.XtraEditors.LabelControl();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txbCode = new DevExpress.XtraEditors.LabelControl();
            this.cmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            this.txbAddress = new DevExpress.XtraEditors.LabelControl();
            this.txtAddress = new DevExpress.XtraEditors.MemoEdit();
            this.txtFax = new DevExpress.XtraEditors.TextEdit();
            this.txbFax = new DevExpress.XtraEditors.LabelControl();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.txbEmail = new DevExpress.XtraEditors.LabelControl();
            this.txtWebsite = new DevExpress.XtraEditors.TextEdit();
            this.txbWebsite = new DevExpress.XtraEditors.LabelControl();
            this.grdStores = new DevExpress.XtraGrid.GridControl();
            this.viewStores = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.selected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkselected = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.description = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grpVisibility = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWebsite.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdStores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewStores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkselected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpVisibility)).BeginInit();
            this.grpVisibility.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(110, 123);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(203, 20);
            this.txtPhone.TabIndex = 4;
            // 
            // txbPhone
            // 
            this.txbPhone.Location = new System.Drawing.Point(9, 126);
            this.txbPhone.Name = "txbPhone";
            this.txbPhone.Size = new System.Drawing.Size(30, 13);
            this.txbPhone.TabIndex = 4;
            this.txbPhone.Text = "Phone";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(110, 33);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(203, 20);
            this.txtName.TabIndex = 2;
            // 
            // txbName
            // 
            this.txbName.Location = new System.Drawing.Point(9, 36);
            this.txbName.Name = "txbName";
            this.txbName.Size = new System.Drawing.Size(27, 13);
            this.txbName.TabIndex = 3;
            this.txbName.Text = "Name";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(110, 8);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(97, 20);
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
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(4, 237);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(333, 36);
            this.cmdOk.TabIndex = 11;
            this.cmdOk.Text = "Save";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(342, 237);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(294, 36);
            this.cmdClose.TabIndex = 12;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // txbAddress
            // 
            this.txbAddress.Location = new System.Drawing.Point(9, 61);
            this.txbAddress.Name = "txbAddress";
            this.txbAddress.Size = new System.Drawing.Size(39, 13);
            this.txbAddress.TabIndex = 5;
            this.txbAddress.Text = "Address";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(110, 58);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(203, 60);
            this.txtAddress.TabIndex = 3;
            // 
            // txtFax
            // 
            this.txtFax.Location = new System.Drawing.Point(110, 148);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(203, 20);
            this.txtFax.TabIndex = 5;
            // 
            // txbFax
            // 
            this.txbFax.Location = new System.Drawing.Point(9, 151);
            this.txbFax.Name = "txbFax";
            this.txbFax.Size = new System.Drawing.Size(18, 13);
            this.txbFax.TabIndex = 8;
            this.txbFax.Text = "Fax";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(110, 173);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(203, 20);
            this.txtEmail.TabIndex = 6;
            // 
            // txbEmail
            // 
            this.txbEmail.Location = new System.Drawing.Point(9, 176);
            this.txbEmail.Name = "txbEmail";
            this.txbEmail.Size = new System.Drawing.Size(24, 13);
            this.txbEmail.TabIndex = 10;
            this.txbEmail.Text = "Email";
            // 
            // txtWebsite
            // 
            this.txtWebsite.Location = new System.Drawing.Point(110, 198);
            this.txtWebsite.Name = "txtWebsite";
            this.txtWebsite.Size = new System.Drawing.Size(203, 20);
            this.txtWebsite.TabIndex = 7;
            // 
            // txbWebsite
            // 
            this.txbWebsite.Location = new System.Drawing.Point(9, 201);
            this.txbWebsite.Name = "txbWebsite";
            this.txbWebsite.Size = new System.Drawing.Size(39, 13);
            this.txbWebsite.TabIndex = 12;
            this.txbWebsite.Text = "Website";
            // 
            // grdStores
            // 
            this.grdStores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdStores.Location = new System.Drawing.Point(2, 22);
            this.grdStores.MainView = this.viewStores;
            this.grdStores.Name = "grdStores";
            this.grdStores.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkselected});
            this.grdStores.Size = new System.Drawing.Size(311, 186);
            this.grdStores.TabIndex = 10;
            this.grdStores.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewStores});
            // 
            // viewStores
            // 
            this.viewStores.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.selected,
            this.code,
            this.description});
            this.viewStores.GridControl = this.grdStores;
            this.viewStores.Name = "viewStores";
            this.viewStores.OptionsView.ShowGroupPanel = false;
            this.viewStores.OptionsView.ShowIndicator = false;
            // 
            // selected
            // 
            this.selected.ColumnEdit = this.chkselected;
            this.selected.FieldName = "selected";
            this.selected.Name = "selected";
            this.selected.OptionsColumn.ShowCaption = false;
            this.selected.Visible = true;
            this.selected.VisibleIndex = 0;
            this.selected.Width = 29;
            // 
            // chkselected
            // 
            this.chkselected.AutoHeight = false;
            this.chkselected.Name = "chkselected";
            // 
            // code
            // 
            this.code.Caption = "Code";
            this.code.FieldName = "code";
            this.code.Name = "code";
            this.code.OptionsColumn.AllowEdit = false;
            this.code.OptionsColumn.AllowFocus = false;
            this.code.Visible = true;
            this.code.VisibleIndex = 1;
            this.code.Width = 87;
            // 
            // description
            // 
            this.description.Caption = "Description";
            this.description.FieldName = "description";
            this.description.Name = "description";
            this.description.OptionsColumn.AllowEdit = false;
            this.description.OptionsColumn.AllowFocus = false;
            this.description.Visible = true;
            this.description.VisibleIndex = 2;
            this.description.Width = 280;
            // 
            // grpVisibility
            // 
            this.grpVisibility.Controls.Add(this.grdStores);
            this.grpVisibility.Location = new System.Drawing.Point(320, 8);
            this.grpVisibility.Name = "grpVisibility";
            this.grpVisibility.Size = new System.Drawing.Size(315, 210);
            this.grpVisibility.TabIndex = 9;
            this.grpVisibility.Text = "Visibility to Stores";
            // 
            // frmIVSCustomers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 278);
            this.Controls.Add(this.grpVisibility);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.txtWebsite);
            this.Controls.Add(this.txbWebsite);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txbCode);
            this.Controls.Add(this.txbEmail);
            this.Controls.Add(this.txbName);
            this.Controls.Add(this.txtFax);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txbFax);
            this.Controls.Add(this.txbPhone);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txbAddress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmIVSCustomers";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Customers";
            this.Load += new System.EventHandler(this.frmIVSCustomers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtPhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWebsite.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdStores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewStores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkselected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpVisibility)).EndInit();
            this.grpVisibility.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl txbCode;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl txbName;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.SimpleButton cmdOk;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
        private DevExpress.XtraEditors.TextEdit txtPhone;
        private DevExpress.XtraEditors.LabelControl txbPhone;
        private DevExpress.XtraEditors.MemoEdit txtAddress;
        private DevExpress.XtraEditors.LabelControl txbAddress;
        private DevExpress.XtraEditors.TextEdit txtWebsite;
        private DevExpress.XtraEditors.LabelControl txbWebsite;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.LabelControl txbEmail;
        private DevExpress.XtraEditors.TextEdit txtFax;
        private DevExpress.XtraEditors.LabelControl txbFax;
        private DevExpress.XtraGrid.GridControl grdStores;
        private DevExpress.XtraGrid.Views.Grid.GridView viewStores;
        private DevExpress.XtraGrid.Columns.GridColumn selected;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkselected;
        private DevExpress.XtraGrid.Columns.GridColumn code;
        private DevExpress.XtraGrid.Columns.GridColumn description;
        private DevExpress.XtraEditors.GroupControl grpVisibility;
    }
}