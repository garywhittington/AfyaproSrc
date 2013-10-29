namespace AfyaPro_NextGen
{
    partial class frmSECReportsAccess
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.grdUserReports = new DevExpress.XtraGrid.GridControl();
            this.viewUserReports = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.userreportgroupdescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.userreportdescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.userreportview = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkuserreportview = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.userreportdesign = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkuserreportdesign = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.userreportformcustomization = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkuserreportformcustomization = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.grdReports = new DevExpress.XtraGrid.GridControl();
            this.viewReports = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.reportselected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkreportselected = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.reportcode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reportdescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reportview = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkreportview = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.reportdesign = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkreportdesign = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.reportformcustomization = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkreportformcustomization = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.grdReportGroups = new DevExpress.XtraGrid.GridControl();
            this.viewReportGroups = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.reportgroupselected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkreportgroupselected = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.reportgroupcode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reportgroupdescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdUserGroups = new DevExpress.XtraGrid.GridControl();
            this.viewUserGroups = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.description = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbUserGroups = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbFunctions = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbModules = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUserReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewUserReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkuserreportview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkuserreportdesign)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkuserreportformcustomization)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkreportselected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkreportview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkreportdesign)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkreportformcustomization)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReportGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewReportGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkreportgroupselected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUserGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewUserGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbUserGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbFunctions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbModules)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.grdUserReports);
            this.layoutControl1.Controls.Add(this.grdReports);
            this.layoutControl1.Controls.Add(this.grdReportGroups);
            this.layoutControl1.Controls.Add(this.grdUserGroups);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(814, 500);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // grdUserReports
            // 
            this.grdUserReports.Location = new System.Drawing.Point(12, 266);
            this.grdUserReports.MainView = this.viewUserReports;
            this.grdUserReports.Name = "grdUserReports";
            this.grdUserReports.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkuserreportview,
            this.chkuserreportdesign,
            this.chkuserreportformcustomization});
            this.grdUserReports.Size = new System.Drawing.Size(790, 222);
            this.grdUserReports.TabIndex = 8;
            this.grdUserReports.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewUserReports});
            // 
            // viewUserReports
            // 
            this.viewUserReports.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.userreportgroupdescription,
            this.userreportdescription,
            this.userreportview,
            this.userreportdesign,
            this.userreportformcustomization});
            this.viewUserReports.GridControl = this.grdUserReports;
            this.viewUserReports.GroupCount = 1;
            this.viewUserReports.Name = "viewUserReports";
            this.viewUserReports.OptionsBehavior.Editable = false;
            this.viewUserReports.OptionsCustomization.AllowColumnMoving = false;
            this.viewUserReports.OptionsCustomization.AllowFilter = false;
            this.viewUserReports.OptionsCustomization.AllowGroup = false;
            this.viewUserReports.OptionsCustomization.AllowSort = false;
            this.viewUserReports.OptionsMenu.EnableColumnMenu = false;
            this.viewUserReports.OptionsMenu.EnableFooterMenu = false;
            this.viewUserReports.OptionsMenu.EnableGroupPanelMenu = false;
            this.viewUserReports.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.userreportgroupdescription, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // userreportgroupdescription
            // 
            this.userreportgroupdescription.Caption = "userreportgroupdescription";
            this.userreportgroupdescription.FieldName = "userreportgroupdescription";
            this.userreportgroupdescription.Name = "userreportgroupdescription";
            // 
            // userreportdescription
            // 
            this.userreportdescription.Caption = "userreportdescription";
            this.userreportdescription.FieldName = "userreportdescription";
            this.userreportdescription.Name = "userreportdescription";
            this.userreportdescription.Visible = true;
            this.userreportdescription.VisibleIndex = 0;
            this.userreportdescription.Width = 357;
            // 
            // userreportview
            // 
            this.userreportview.Caption = "userreportview";
            this.userreportview.ColumnEdit = this.chkuserreportview;
            this.userreportview.FieldName = "userreportview";
            this.userreportview.Name = "userreportview";
            this.userreportview.Visible = true;
            this.userreportview.VisibleIndex = 1;
            this.userreportview.Width = 153;
            // 
            // chkuserreportview
            // 
            this.chkuserreportview.AutoHeight = false;
            this.chkuserreportview.Name = "chkuserreportview";
            // 
            // userreportdesign
            // 
            this.userreportdesign.Caption = "userreportdesign";
            this.userreportdesign.ColumnEdit = this.chkuserreportdesign;
            this.userreportdesign.FieldName = "userreportdesign";
            this.userreportdesign.Name = "userreportdesign";
            this.userreportdesign.Visible = true;
            this.userreportdesign.VisibleIndex = 2;
            this.userreportdesign.Width = 140;
            // 
            // chkuserreportdesign
            // 
            this.chkuserreportdesign.AutoHeight = false;
            this.chkuserreportdesign.Name = "chkuserreportdesign";
            // 
            // userreportformcustomization
            // 
            this.userreportformcustomization.Caption = "userreportformcustomization";
            this.userreportformcustomization.ColumnEdit = this.chkuserreportformcustomization;
            this.userreportformcustomization.FieldName = "userreportformcustomization";
            this.userreportformcustomization.Name = "userreportformcustomization";
            this.userreportformcustomization.Visible = true;
            this.userreportformcustomization.VisibleIndex = 3;
            this.userreportformcustomization.Width = 136;
            // 
            // chkuserreportformcustomization
            // 
            this.chkuserreportformcustomization.AutoHeight = false;
            this.chkuserreportformcustomization.Name = "chkuserreportformcustomization";
            // 
            // grdReports
            // 
            this.grdReports.Location = new System.Drawing.Point(408, 15);
            this.grdReports.MainView = this.viewReports;
            this.grdReports.Name = "grdReports";
            this.grdReports.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkreportselected,
            this.chkreportview,
            this.chkreportdesign,
            this.chkreportformcustomization});
            this.grdReports.Size = new System.Drawing.Size(396, 244);
            this.grdReports.TabIndex = 6;
            this.grdReports.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewReports});
            this.grdReports.Enter += new System.EventHandler(this.grdReports_Enter);
            // 
            // viewReports
            // 
            this.viewReports.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.reportselected,
            this.reportcode,
            this.reportdescription,
            this.reportview,
            this.reportdesign,
            this.reportformcustomization});
            this.viewReports.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.viewReports.GridControl = this.grdReports;
            this.viewReports.Name = "viewReports";
            this.viewReports.OptionsMenu.EnableColumnMenu = false;
            this.viewReports.OptionsMenu.EnableFooterMenu = false;
            this.viewReports.OptionsMenu.EnableGroupPanelMenu = false;
            this.viewReports.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.viewReports.OptionsView.ShowGroupPanel = false;
            this.viewReports.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.viewReports_FocusedRowChanged);
            // 
            // reportselected
            // 
            this.reportselected.ColumnEdit = this.chkreportselected;
            this.reportselected.FieldName = "reportselected";
            this.reportselected.Name = "reportselected";
            this.reportselected.OptionsColumn.ShowCaption = false;
            this.reportselected.Visible = true;
            this.reportselected.VisibleIndex = 0;
            this.reportselected.Width = 35;
            // 
            // chkreportselected
            // 
            this.chkreportselected.AutoHeight = false;
            this.chkreportselected.Name = "chkreportselected";
            // 
            // reportcode
            // 
            this.reportcode.Caption = "reportcode";
            this.reportcode.FieldName = "reportcode";
            this.reportcode.Name = "reportcode";
            this.reportcode.OptionsColumn.AllowEdit = false;
            this.reportcode.OptionsColumn.AllowFocus = false;
            // 
            // reportdescription
            // 
            this.reportdescription.Caption = "reportdescription";
            this.reportdescription.FieldName = "reportdescription";
            this.reportdescription.Name = "reportdescription";
            this.reportdescription.OptionsColumn.AllowEdit = false;
            this.reportdescription.OptionsColumn.AllowFocus = false;
            this.reportdescription.Visible = true;
            this.reportdescription.VisibleIndex = 1;
            this.reportdescription.Width = 106;
            // 
            // reportview
            // 
            this.reportview.Caption = "reportview";
            this.reportview.ColumnEdit = this.chkreportview;
            this.reportview.FieldName = "reportview";
            this.reportview.Name = "reportview";
            this.reportview.Visible = true;
            this.reportview.VisibleIndex = 2;
            this.reportview.Width = 82;
            // 
            // chkreportview
            // 
            this.chkreportview.AutoHeight = false;
            this.chkreportview.Name = "chkreportview";
            // 
            // reportdesign
            // 
            this.reportdesign.Caption = "reportdesign";
            this.reportdesign.ColumnEdit = this.chkreportdesign;
            this.reportdesign.FieldName = "reportdesign";
            this.reportdesign.Name = "reportdesign";
            this.reportdesign.Visible = true;
            this.reportdesign.VisibleIndex = 3;
            this.reportdesign.Width = 76;
            // 
            // chkreportdesign
            // 
            this.chkreportdesign.AutoHeight = false;
            this.chkreportdesign.Name = "chkreportdesign";
            // 
            // reportformcustomization
            // 
            this.reportformcustomization.Caption = "reportformcustomization";
            this.reportformcustomization.ColumnEdit = this.chkreportformcustomization;
            this.reportformcustomization.FieldName = "reportformcustomization";
            this.reportformcustomization.Name = "reportformcustomization";
            this.reportformcustomization.Visible = true;
            this.reportformcustomization.VisibleIndex = 4;
            this.reportformcustomization.Width = 85;
            // 
            // chkreportformcustomization
            // 
            this.chkreportformcustomization.AutoHeight = false;
            this.chkreportformcustomization.Name = "chkreportformcustomization";
            // 
            // grdReportGroups
            // 
            this.grdReportGroups.Location = new System.Drawing.Point(210, 15);
            this.grdReportGroups.MainView = this.viewReportGroups;
            this.grdReportGroups.Name = "grdReportGroups";
            this.grdReportGroups.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkreportgroupselected});
            this.grdReportGroups.Size = new System.Drawing.Size(196, 244);
            this.grdReportGroups.TabIndex = 5;
            this.grdReportGroups.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewReportGroups});
            this.grdReportGroups.Enter += new System.EventHandler(this.grdReportGroups_Enter);
            // 
            // viewReportGroups
            // 
            this.viewReportGroups.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.reportgroupselected,
            this.reportgroupcode,
            this.reportgroupdescription});
            this.viewReportGroups.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.viewReportGroups.GridControl = this.grdReportGroups;
            this.viewReportGroups.Name = "viewReportGroups";
            this.viewReportGroups.OptionsMenu.EnableColumnMenu = false;
            this.viewReportGroups.OptionsMenu.EnableFooterMenu = false;
            this.viewReportGroups.OptionsMenu.EnableGroupPanelMenu = false;
            this.viewReportGroups.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.viewReportGroups.OptionsView.ShowGroupPanel = false;
            this.viewReportGroups.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.viewReportGroups_FocusedRowChanged);
            // 
            // reportgroupselected
            // 
            this.reportgroupselected.ColumnEdit = this.chkreportgroupselected;
            this.reportgroupselected.FieldName = "reportgroupselected";
            this.reportgroupselected.Name = "reportgroupselected";
            this.reportgroupselected.OptionsColumn.ShowCaption = false;
            this.reportgroupselected.Visible = true;
            this.reportgroupselected.VisibleIndex = 0;
            this.reportgroupselected.Width = 32;
            // 
            // chkreportgroupselected
            // 
            this.chkreportgroupselected.AutoHeight = false;
            this.chkreportgroupselected.Name = "chkreportgroupselected";
            // 
            // reportgroupcode
            // 
            this.reportgroupcode.Caption = "reportgroupcode";
            this.reportgroupcode.FieldName = "reportgroupcode";
            this.reportgroupcode.Name = "reportgroupcode";
            this.reportgroupcode.OptionsColumn.AllowEdit = false;
            this.reportgroupcode.OptionsColumn.AllowFocus = false;
            // 
            // reportgroupdescription
            // 
            this.reportgroupdescription.Caption = "reportgroupdescription";
            this.reportgroupdescription.FieldName = "reportgroupdescription";
            this.reportgroupdescription.Name = "reportgroupdescription";
            this.reportgroupdescription.OptionsColumn.AllowEdit = false;
            this.reportgroupdescription.OptionsColumn.AllowFocus = false;
            this.reportgroupdescription.Visible = true;
            this.reportgroupdescription.VisibleIndex = 1;
            this.reportgroupdescription.Width = 137;
            // 
            // grdUserGroups
            // 
            this.grdUserGroups.Location = new System.Drawing.Point(12, 15);
            this.grdUserGroups.MainView = this.viewUserGroups;
            this.grdUserGroups.Name = "grdUserGroups";
            this.grdUserGroups.Size = new System.Drawing.Size(196, 244);
            this.grdUserGroups.TabIndex = 4;
            this.grdUserGroups.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewUserGroups});
            // 
            // viewUserGroups
            // 
            this.viewUserGroups.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.code,
            this.description});
            this.viewUserGroups.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.viewUserGroups.GridControl = this.grdUserGroups;
            this.viewUserGroups.Name = "viewUserGroups";
            this.viewUserGroups.OptionsBehavior.Editable = false;
            this.viewUserGroups.OptionsMenu.EnableColumnMenu = false;
            this.viewUserGroups.OptionsMenu.EnableFooterMenu = false;
            this.viewUserGroups.OptionsMenu.EnableGroupPanelMenu = false;
            this.viewUserGroups.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.viewUserGroups.OptionsView.ShowGroupPanel = false;
            this.viewUserGroups.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.viewUserGroups_FocusedRowChanged);
            // 
            // code
            // 
            this.code.Caption = "code";
            this.code.FieldName = "code";
            this.code.Name = "code";
            this.code.Visible = true;
            this.code.VisibleIndex = 0;
            // 
            // description
            // 
            this.description.Caption = "description";
            this.description.FieldName = "description";
            this.description.Name = "description";
            this.description.Visible = true;
            this.description.VisibleIndex = 1;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.txbUserGroups,
            this.txbFunctions,
            this.layoutControlItem1,
            this.txbModules});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(814, 500);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // txbUserGroups
            // 
            this.txbUserGroups.Control = this.grdUserGroups;
            this.txbUserGroups.CustomizationFormText = "layoutControlItem1";
            this.txbUserGroups.Location = new System.Drawing.Point(0, 0);
            this.txbUserGroups.Name = "txbUserGroups";
            this.txbUserGroups.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 5, 2);
            this.txbUserGroups.Size = new System.Drawing.Size(198, 251);
            this.txbUserGroups.Text = "User Groups";
            this.txbUserGroups.TextLocation = DevExpress.Utils.Locations.Top;
            this.txbUserGroups.TextSize = new System.Drawing.Size(0, 0);
            this.txbUserGroups.TextToControlDistance = 0;
            this.txbUserGroups.TextVisible = false;
            // 
            // txbFunctions
            // 
            this.txbFunctions.Control = this.grdReports;
            this.txbFunctions.CustomizationFormText = "Functions";
            this.txbFunctions.Location = new System.Drawing.Point(396, 0);
            this.txbFunctions.Name = "txbFunctions";
            this.txbFunctions.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 5, 2);
            this.txbFunctions.Size = new System.Drawing.Size(398, 251);
            this.txbFunctions.Text = "Functions";
            this.txbFunctions.TextLocation = DevExpress.Utils.Locations.Top;
            this.txbFunctions.TextSize = new System.Drawing.Size(0, 0);
            this.txbFunctions.TextToControlDistance = 0;
            this.txbFunctions.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdUserReports;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 251);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 5, 2);
            this.layoutControlItem1.Size = new System.Drawing.Size(794, 229);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // txbModules
            // 
            this.txbModules.Control = this.grdReportGroups;
            this.txbModules.CustomizationFormText = "Modules";
            this.txbModules.Location = new System.Drawing.Point(198, 0);
            this.txbModules.Name = "txbModules";
            this.txbModules.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 5, 2);
            this.txbModules.Size = new System.Drawing.Size(198, 251);
            this.txbModules.Text = "Modules";
            this.txbModules.TextLocation = DevExpress.Utils.Locations.Top;
            this.txbModules.TextSize = new System.Drawing.Size(0, 0);
            this.txbModules.TextToControlDistance = 0;
            this.txbModules.TextVisible = false;
            // 
            // frmSECReportsAccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 500);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmSECReportsAccess";
            this.Text = "Reports Access";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSECReportsAccess_FormClosing);
            this.Load += new System.EventHandler(this.frmSECReportsAccess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdUserReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewUserReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkuserreportview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkuserreportdesign)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkuserreportformcustomization)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkreportselected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkreportview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkreportdesign)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkreportformcustomization)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReportGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewReportGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkreportgroupselected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUserGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewUserGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbUserGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbFunctions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbModules)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl grdUserGroups;
        private DevExpress.XtraGrid.Views.Grid.GridView viewUserGroups;
        private DevExpress.XtraLayout.LayoutControlItem txbUserGroups;
        private DevExpress.XtraGrid.Columns.GridColumn code;
        private DevExpress.XtraGrid.Columns.GridColumn description;
        private DevExpress.XtraGrid.GridControl grdReportGroups;
        private DevExpress.XtraGrid.Views.Grid.GridView viewReportGroups;
        private DevExpress.XtraLayout.LayoutControlItem txbModules;
        private DevExpress.XtraGrid.Columns.GridColumn reportgroupcode;
        private DevExpress.XtraGrid.Columns.GridColumn reportgroupdescription;
        private DevExpress.XtraGrid.GridControl grdReports;
        private DevExpress.XtraGrid.Views.Grid.GridView viewReports;
        private DevExpress.XtraLayout.LayoutControlItem txbFunctions;
        private DevExpress.XtraGrid.Columns.GridColumn reportselected;
        private DevExpress.XtraGrid.Columns.GridColumn reportcode;
        private DevExpress.XtraGrid.Columns.GridColumn reportdescription;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkreportselected;
        private DevExpress.XtraGrid.GridControl grdUserReports;
        private DevExpress.XtraGrid.Views.Grid.GridView viewUserReports;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn userreportgroupdescription;
        private DevExpress.XtraGrid.Columns.GridColumn reportgroupselected;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkreportgroupselected;
        private DevExpress.XtraGrid.Columns.GridColumn userreportview;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkuserreportview;
        private DevExpress.XtraGrid.Columns.GridColumn userreportdesign;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkuserreportdesign;
        private DevExpress.XtraGrid.Columns.GridColumn userreportformcustomization;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkuserreportformcustomization;
        private DevExpress.XtraGrid.Columns.GridColumn reportview;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkreportview;
        private DevExpress.XtraGrid.Columns.GridColumn reportdesign;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkreportdesign;
        private DevExpress.XtraGrid.Columns.GridColumn reportformcustomization;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkreportformcustomization;
        private DevExpress.XtraGrid.Columns.GridColumn userreportdescription;
    }
}