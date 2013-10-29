namespace AfyaPro_NextGen
{
    partial class frmSECUserGroupPrinters
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
            this.txbInvoices = new DevExpress.XtraEditors.LabelControl();
            this.txbReceipts = new DevExpress.XtraEditors.LabelControl();
            this.grdSECUserGroupPrinters = new DevExpress.XtraGrid.GridControl();
            this.viewUserGroupPrinters = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.documenttypecode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.documenttypedescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.printername = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboprintername = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.printedwhen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.printedwhendescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboprintedwhendescription = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.autoprint = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkautoprint = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.txbUserGroup = new DevExpress.XtraEditors.LabelControl();
            this.cboUserGroup = new DevExpress.XtraEditors.LookUpEdit();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkprinttoscreen = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSECUserGroupPrinters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewUserGroupPrinters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboprintername)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboprintedwhendescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkautoprint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUserGroup.Properties)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkprinttoscreen)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.txbInvoices);
            this.groupControl2.Controls.Add(this.txbReceipts);
            this.groupControl2.Controls.Add(this.grdSECUserGroupPrinters);
            this.groupControl2.Controls.Add(this.txbUserGroup);
            this.groupControl2.Controls.Add(this.cboUserGroup);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(771, 356);
            this.groupControl2.TabIndex = 11;
            this.groupControl2.Text = "groupControl2";
            // 
            // txbInvoices
            // 
            this.txbInvoices.Location = new System.Drawing.Point(596, 5);
            this.txbInvoices.Name = "txbInvoices";
            this.txbInvoices.Size = new System.Drawing.Size(40, 13);
            this.txbInvoices.TabIndex = 10;
            this.txbInvoices.Text = "Invoices";
            this.txbInvoices.Visible = false;
            // 
            // txbReceipts
            // 
            this.txbReceipts.Location = new System.Drawing.Point(516, 5);
            this.txbReceipts.Name = "txbReceipts";
            this.txbReceipts.Size = new System.Drawing.Size(41, 13);
            this.txbReceipts.TabIndex = 9;
            this.txbReceipts.Text = "Receipts";
            this.txbReceipts.Visible = false;
            // 
            // grdSECUserGroupPrinters
            // 
            this.grdSECUserGroupPrinters.Location = new System.Drawing.Point(5, 42);
            this.grdSECUserGroupPrinters.MainView = this.viewUserGroupPrinters;
            this.grdSECUserGroupPrinters.Name = "grdSECUserGroupPrinters";
            this.grdSECUserGroupPrinters.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cboprintername,
            this.chkautoprint,
            this.cboprintedwhendescription,
            this.chkprinttoscreen});
            this.grdSECUserGroupPrinters.Size = new System.Drawing.Size(760, 307);
            this.grdSECUserGroupPrinters.TabIndex = 8;
            this.grdSECUserGroupPrinters.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewUserGroupPrinters});
            // 
            // viewUserGroupPrinters
            // 
            this.viewUserGroupPrinters.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.documenttypecode,
            this.documenttypedescription,
            this.printername,
            this.printedwhen,
            this.printedwhendescription,
            this.autoprint,
            this.gridColumn1});
            this.viewUserGroupPrinters.GridControl = this.grdSECUserGroupPrinters;
            this.viewUserGroupPrinters.Name = "viewUserGroupPrinters";
            this.viewUserGroupPrinters.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.viewUserGroupPrinters.OptionsView.ShowGroupPanel = false;
            this.viewUserGroupPrinters.OptionsView.ShowIndicator = false;
            // 
            // documenttypecode
            // 
            this.documenttypecode.Caption = "documenttypecode";
            this.documenttypecode.FieldName = "documenttypecode";
            this.documenttypecode.Name = "documenttypecode";
            this.documenttypecode.OptionsColumn.AllowEdit = false;
            this.documenttypecode.OptionsColumn.AllowFocus = false;
            // 
            // documenttypedescription
            // 
            this.documenttypedescription.Caption = "documenttypedescription";
            this.documenttypedescription.FieldName = "documenttypedescription";
            this.documenttypedescription.Name = "documenttypedescription";
            this.documenttypedescription.OptionsColumn.AllowEdit = false;
            this.documenttypedescription.OptionsColumn.AllowFocus = false;
            this.documenttypedescription.Visible = true;
            this.documenttypedescription.VisibleIndex = 0;
            this.documenttypedescription.Width = 174;
            // 
            // printername
            // 
            this.printername.Caption = "printername";
            this.printername.ColumnEdit = this.cboprintername;
            this.printername.FieldName = "printername";
            this.printername.Name = "printername";
            this.printername.Visible = true;
            this.printername.VisibleIndex = 1;
            this.printername.Width = 223;
            // 
            // cboprintername
            // 
            this.cboprintername.AutoHeight = false;
            this.cboprintername.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboprintername.Name = "cboprintername";
            // 
            // printedwhen
            // 
            this.printedwhen.Caption = "printedwhen";
            this.printedwhen.FieldName = "printedwhen";
            this.printedwhen.Name = "printedwhen";
            this.printedwhen.OptionsColumn.AllowEdit = false;
            this.printedwhen.OptionsColumn.AllowFocus = false;
            // 
            // printedwhendescription
            // 
            this.printedwhendescription.Caption = "printedwhendescription";
            this.printedwhendescription.ColumnEdit = this.cboprintedwhendescription;
            this.printedwhendescription.FieldName = "printedwhendescription";
            this.printedwhendescription.Name = "printedwhendescription";
            this.printedwhendescription.Visible = true;
            this.printedwhendescription.VisibleIndex = 2;
            this.printedwhendescription.Width = 166;
            // 
            // cboprintedwhendescription
            // 
            this.cboprintedwhendescription.AutoHeight = false;
            this.cboprintedwhendescription.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboprintedwhendescription.Name = "cboprintedwhendescription";
            // 
            // autoprint
            // 
            this.autoprint.Caption = "autoprint";
            this.autoprint.ColumnEdit = this.chkautoprint;
            this.autoprint.FieldName = "autoprint";
            this.autoprint.Name = "autoprint";
            this.autoprint.Visible = true;
            this.autoprint.VisibleIndex = 3;
            this.autoprint.Width = 85;
            // 
            // chkautoprint
            // 
            this.chkautoprint.AutoHeight = false;
            this.chkautoprint.Name = "chkautoprint";
            // 
            // txbUserGroup
            // 
            this.txbUserGroup.Location = new System.Drawing.Point(5, 15);
            this.txbUserGroup.Name = "txbUserGroup";
            this.txbUserGroup.Size = new System.Drawing.Size(54, 13);
            this.txbUserGroup.TabIndex = 7;
            this.txbUserGroup.Text = "User Group";
            // 
            // cboUserGroup
            // 
            this.cboUserGroup.Location = new System.Drawing.Point(108, 12);
            this.cboUserGroup.Name = "cboUserGroup";
            this.cboUserGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboUserGroup.Properties.NullText = "";
            this.cboUserGroup.Size = new System.Drawing.Size(298, 20);
            this.cboUserGroup.TabIndex = 6;
            this.cboUserGroup.EditValueChanged += new System.EventHandler(this.cboUserGroup_EditValueChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 356);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cmdOk);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cmdClose);
            this.splitContainer1.Size = new System.Drawing.Size(771, 63);
            this.splitContainer1.SplitterDistance = 372;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 13;
            // 
            // cmdOk
            // 
            this.cmdOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdOk.Location = new System.Drawing.Point(0, 0);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(372, 63);
            this.cmdOk.TabIndex = 4;
            this.cmdOk.Text = "Save";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdClose.Location = new System.Drawing.Point(0, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(398, 63);
            this.cmdClose.TabIndex = 5;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Print to Screen";
            this.gridColumn1.ColumnEdit = this.chkprinttoscreen;
            this.gridColumn1.FieldName = "printtoscreen";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 4;
            this.gridColumn1.Width = 108;
            // 
            // chkprinttoscreen
            // 
            this.chkprinttoscreen.AutoHeight = false;
            this.chkprinttoscreen.Name = "chkprinttoscreen";
            // 
            // frmSECUserGroupPrinters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 419);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSECUserGroupPrinters";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User Group Printers";
            this.Activated += new System.EventHandler(this.frmSECUserGroupPrinters_Activated);
            this.Load += new System.EventHandler(this.frmSECUserGroupPrinters_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSECUserGroupPrinters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewUserGroupPrinters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboprintername)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboprintedwhendescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkautoprint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUserGroup.Properties)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkprinttoscreen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SimpleButton cmdOk;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
        private DevExpress.XtraEditors.LabelControl txbUserGroup;
        private DevExpress.XtraEditors.LookUpEdit cboUserGroup;
        private DevExpress.XtraGrid.GridControl grdSECUserGroupPrinters;
        private DevExpress.XtraGrid.Views.Grid.GridView viewUserGroupPrinters;
        private DevExpress.XtraGrid.Columns.GridColumn documenttypecode;
        private DevExpress.XtraGrid.Columns.GridColumn documenttypedescription;
        private DevExpress.XtraGrid.Columns.GridColumn printername;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cboprintername;
        private DevExpress.XtraGrid.Columns.GridColumn autoprint;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkautoprint;
        private DevExpress.XtraEditors.LabelControl txbInvoices;
        private DevExpress.XtraEditors.LabelControl txbReceipts;
        private DevExpress.XtraGrid.Columns.GridColumn printedwhendescription;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cboprintedwhendescription;
        private DevExpress.XtraGrid.Columns.GridColumn printedwhen;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkprinttoscreen;
    }
}