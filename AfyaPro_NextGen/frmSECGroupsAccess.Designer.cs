namespace AfyaPro_NextGen
{
    partial class frmSECGroupsAccess
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
            this.grdUserModules = new DevExpress.XtraGrid.GridControl();
            this.viewUserModules = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.usermoduletext = new DevExpress.XtraGrid.Columns.GridColumn();
            this.usermoduleitemtext = new DevExpress.XtraGrid.Columns.GridColumn();
            this.userfunctionaccesstext = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdActions = new DevExpress.XtraGrid.GridControl();
            this.viewActions = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.functionaccessselected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkfunctionaccessselected = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.functionaccesskey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.functionaccesstext = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdFunctions = new DevExpress.XtraGrid.GridControl();
            this.viewFunctions = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.moduleitemselected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkmoduleitemselected = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.moduleitemkey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.moduleitemtext = new DevExpress.XtraGrid.Columns.GridColumn();
            this.functionsprefix = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdModules = new DevExpress.XtraGrid.GridControl();
            this.viewModules = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.moduleselected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkmoduleselected = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.modulekey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.moduletext = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdUserGroups = new DevExpress.XtraGrid.GridControl();
            this.viewUserGroups = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.description = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbUserGroups = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbFunctions = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbActions = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbModules = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUserModules)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewUserModules)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdActions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewActions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkfunctionaccessselected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFunctions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewFunctions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkmoduleitemselected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdModules)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewModules)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkmoduleselected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUserGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewUserGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbUserGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbFunctions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbActions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbModules)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.grdUserModules);
            this.layoutControl1.Controls.Add(this.grdActions);
            this.layoutControl1.Controls.Add(this.grdFunctions);
            this.layoutControl1.Controls.Add(this.grdModules);
            this.layoutControl1.Controls.Add(this.grdUserGroups);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(814, 500);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // grdUserModules
            // 
            this.grdUserModules.Location = new System.Drawing.Point(12, 266);
            this.grdUserModules.MainView = this.viewUserModules;
            this.grdUserModules.Name = "grdUserModules";
            this.grdUserModules.Size = new System.Drawing.Size(790, 222);
            this.grdUserModules.TabIndex = 8;
            this.grdUserModules.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewUserModules});
            // 
            // viewUserModules
            // 
            this.viewUserModules.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.usermoduletext,
            this.usermoduleitemtext,
            this.userfunctionaccesstext});
            this.viewUserModules.GridControl = this.grdUserModules;
            this.viewUserModules.GroupCount = 2;
            this.viewUserModules.Name = "viewUserModules";
            this.viewUserModules.OptionsBehavior.Editable = false;
            this.viewUserModules.OptionsCustomization.AllowColumnMoving = false;
            this.viewUserModules.OptionsCustomization.AllowFilter = false;
            this.viewUserModules.OptionsCustomization.AllowGroup = false;
            this.viewUserModules.OptionsCustomization.AllowSort = false;
            this.viewUserModules.OptionsMenu.EnableColumnMenu = false;
            this.viewUserModules.OptionsMenu.EnableFooterMenu = false;
            this.viewUserModules.OptionsMenu.EnableGroupPanelMenu = false;
            this.viewUserModules.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.usermoduletext, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.usermoduleitemtext, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // usermoduletext
            // 
            this.usermoduletext.Caption = "usermoduletext";
            this.usermoduletext.FieldName = "usermoduletext";
            this.usermoduletext.Name = "usermoduletext";
            this.usermoduletext.Visible = true;
            this.usermoduletext.VisibleIndex = 0;
            // 
            // usermoduleitemtext
            // 
            this.usermoduleitemtext.Caption = "usermoduleitemtext";
            this.usermoduleitemtext.FieldName = "usermoduleitemtext";
            this.usermoduleitemtext.Name = "usermoduleitemtext";
            this.usermoduleitemtext.Visible = true;
            this.usermoduleitemtext.VisibleIndex = 0;
            // 
            // userfunctionaccesstext
            // 
            this.userfunctionaccesstext.Caption = "userfunctionaccesstext";
            this.userfunctionaccesstext.FieldName = "userfunctionaccesstext";
            this.userfunctionaccesstext.Name = "userfunctionaccesstext";
            this.userfunctionaccesstext.Visible = true;
            this.userfunctionaccesstext.VisibleIndex = 0;
            // 
            // grdActions
            // 
            this.grdActions.Location = new System.Drawing.Point(558, 15);
            this.grdActions.MainView = this.viewActions;
            this.grdActions.Name = "grdActions";
            this.grdActions.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkfunctionaccessselected});
            this.grdActions.Size = new System.Drawing.Size(244, 244);
            this.grdActions.TabIndex = 7;
            this.grdActions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewActions});
            this.grdActions.Enter += new System.EventHandler(this.grdActions_Enter);
            // 
            // viewActions
            // 
            this.viewActions.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.functionaccessselected,
            this.functionaccesskey,
            this.functionaccesstext});
            this.viewActions.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.viewActions.GridControl = this.grdActions;
            this.viewActions.Name = "viewActions";
            this.viewActions.OptionsMenu.EnableColumnMenu = false;
            this.viewActions.OptionsMenu.EnableFooterMenu = false;
            this.viewActions.OptionsMenu.EnableGroupPanelMenu = false;
            this.viewActions.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.viewActions.OptionsView.ShowGroupPanel = false;
            // 
            // functionaccessselected
            // 
            this.functionaccessselected.ColumnEdit = this.chkfunctionaccessselected;
            this.functionaccessselected.FieldName = "functionaccessselected";
            this.functionaccessselected.Name = "functionaccessselected";
            this.functionaccessselected.OptionsColumn.ShowCaption = false;
            this.functionaccessselected.Visible = true;
            this.functionaccessselected.VisibleIndex = 0;
            this.functionaccessselected.Width = 36;
            // 
            // chkfunctionaccessselected
            // 
            this.chkfunctionaccessselected.AutoHeight = false;
            this.chkfunctionaccessselected.Name = "chkfunctionaccessselected";
            // 
            // functionaccesskey
            // 
            this.functionaccesskey.Caption = "functionaccesskey";
            this.functionaccesskey.FieldName = "functionaccesskey";
            this.functionaccesskey.Name = "functionaccesskey";
            this.functionaccesskey.OptionsColumn.AllowEdit = false;
            this.functionaccesskey.OptionsColumn.AllowFocus = false;
            // 
            // functionaccesstext
            // 
            this.functionaccesstext.Caption = "functionaccesstext";
            this.functionaccesstext.FieldName = "functionaccesstext";
            this.functionaccesstext.Name = "functionaccesstext";
            this.functionaccesstext.OptionsColumn.AllowEdit = false;
            this.functionaccesstext.OptionsColumn.AllowFocus = false;
            this.functionaccesstext.Visible = true;
            this.functionaccesstext.VisibleIndex = 1;
            this.functionaccesstext.Width = 191;
            // 
            // grdFunctions
            // 
            this.grdFunctions.Location = new System.Drawing.Point(379, 15);
            this.grdFunctions.MainView = this.viewFunctions;
            this.grdFunctions.Name = "grdFunctions";
            this.grdFunctions.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkmoduleitemselected});
            this.grdFunctions.Size = new System.Drawing.Size(177, 244);
            this.grdFunctions.TabIndex = 6;
            this.grdFunctions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewFunctions});
            this.grdFunctions.Enter += new System.EventHandler(this.grdFunctions_Enter);
            // 
            // viewFunctions
            // 
            this.viewFunctions.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.moduleitemselected,
            this.moduleitemkey,
            this.moduleitemtext,
            this.functionsprefix});
            this.viewFunctions.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.viewFunctions.GridControl = this.grdFunctions;
            this.viewFunctions.Name = "viewFunctions";
            this.viewFunctions.OptionsMenu.EnableColumnMenu = false;
            this.viewFunctions.OptionsMenu.EnableFooterMenu = false;
            this.viewFunctions.OptionsMenu.EnableGroupPanelMenu = false;
            this.viewFunctions.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.viewFunctions.OptionsView.ShowGroupPanel = false;
            this.viewFunctions.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.viewFunctions_FocusedRowChanged);
            // 
            // moduleitemselected
            // 
            this.moduleitemselected.ColumnEdit = this.chkmoduleitemselected;
            this.moduleitemselected.FieldName = "moduleitemselected";
            this.moduleitemselected.Name = "moduleitemselected";
            this.moduleitemselected.OptionsColumn.ShowCaption = false;
            this.moduleitemselected.Visible = true;
            this.moduleitemselected.VisibleIndex = 0;
            this.moduleitemselected.Width = 38;
            // 
            // chkmoduleitemselected
            // 
            this.chkmoduleitemselected.AutoHeight = false;
            this.chkmoduleitemselected.Name = "chkmoduleitemselected";
            // 
            // moduleitemkey
            // 
            this.moduleitemkey.Caption = "moduleitemkey";
            this.moduleitemkey.FieldName = "moduleitemkey";
            this.moduleitemkey.Name = "moduleitemkey";
            this.moduleitemkey.OptionsColumn.AllowEdit = false;
            this.moduleitemkey.OptionsColumn.AllowFocus = false;
            // 
            // moduleitemtext
            // 
            this.moduleitemtext.Caption = "moduleitemtext";
            this.moduleitemtext.FieldName = "moduleitemtext";
            this.moduleitemtext.Name = "moduleitemtext";
            this.moduleitemtext.OptionsColumn.AllowEdit = false;
            this.moduleitemtext.OptionsColumn.AllowFocus = false;
            this.moduleitemtext.Visible = true;
            this.moduleitemtext.VisibleIndex = 1;
            this.moduleitemtext.Width = 121;
            // 
            // functionsprefix
            // 
            this.functionsprefix.Caption = "functionsprefix";
            this.functionsprefix.FieldName = "functionsprefix";
            this.functionsprefix.Name = "functionsprefix";
            this.functionsprefix.OptionsColumn.AllowEdit = false;
            this.functionsprefix.OptionsColumn.AllowFocus = false;
            // 
            // grdModules
            // 
            this.grdModules.Location = new System.Drawing.Point(210, 15);
            this.grdModules.MainView = this.viewModules;
            this.grdModules.Name = "grdModules";
            this.grdModules.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkmoduleselected});
            this.grdModules.Size = new System.Drawing.Size(167, 244);
            this.grdModules.TabIndex = 5;
            this.grdModules.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewModules});
            this.grdModules.Enter += new System.EventHandler(this.grdModules_Enter);
            // 
            // viewModules
            // 
            this.viewModules.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.moduleselected,
            this.modulekey,
            this.moduletext});
            this.viewModules.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.viewModules.GridControl = this.grdModules;
            this.viewModules.Name = "viewModules";
            this.viewModules.OptionsMenu.EnableColumnMenu = false;
            this.viewModules.OptionsMenu.EnableFooterMenu = false;
            this.viewModules.OptionsMenu.EnableGroupPanelMenu = false;
            this.viewModules.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.viewModules.OptionsView.ShowGroupPanel = false;
            this.viewModules.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.viewModules_FocusedRowChanged);
            // 
            // moduleselected
            // 
            this.moduleselected.ColumnEdit = this.chkmoduleselected;
            this.moduleselected.FieldName = "moduleselected";
            this.moduleselected.Name = "moduleselected";
            this.moduleselected.OptionsColumn.ShowCaption = false;
            this.moduleselected.Visible = true;
            this.moduleselected.VisibleIndex = 0;
            this.moduleselected.Width = 35;
            // 
            // chkmoduleselected
            // 
            this.chkmoduleselected.AutoHeight = false;
            this.chkmoduleselected.Name = "chkmoduleselected";
            // 
            // modulekey
            // 
            this.modulekey.Caption = "modulekey";
            this.modulekey.FieldName = "modulekey";
            this.modulekey.Name = "modulekey";
            this.modulekey.OptionsColumn.AllowEdit = false;
            this.modulekey.OptionsColumn.AllowFocus = false;
            // 
            // moduletext
            // 
            this.moduletext.Caption = "moduletext";
            this.moduletext.FieldName = "moduletext";
            this.moduletext.Name = "moduletext";
            this.moduletext.OptionsColumn.AllowEdit = false;
            this.moduletext.OptionsColumn.AllowFocus = false;
            this.moduletext.Visible = true;
            this.moduletext.VisibleIndex = 1;
            this.moduletext.Width = 114;
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
            this.txbActions,
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
            this.txbFunctions.Control = this.grdFunctions;
            this.txbFunctions.CustomizationFormText = "Functions";
            this.txbFunctions.Location = new System.Drawing.Point(367, 0);
            this.txbFunctions.Name = "txbFunctions";
            this.txbFunctions.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 5, 2);
            this.txbFunctions.Size = new System.Drawing.Size(179, 251);
            this.txbFunctions.Text = "Functions";
            this.txbFunctions.TextLocation = DevExpress.Utils.Locations.Top;
            this.txbFunctions.TextSize = new System.Drawing.Size(0, 0);
            this.txbFunctions.TextToControlDistance = 0;
            this.txbFunctions.TextVisible = false;
            // 
            // txbActions
            // 
            this.txbActions.Control = this.grdActions;
            this.txbActions.CustomizationFormText = "Actions";
            this.txbActions.Location = new System.Drawing.Point(546, 0);
            this.txbActions.Name = "txbActions";
            this.txbActions.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 5, 2);
            this.txbActions.Size = new System.Drawing.Size(248, 251);
            this.txbActions.Text = "Actions";
            this.txbActions.TextLocation = DevExpress.Utils.Locations.Top;
            this.txbActions.TextSize = new System.Drawing.Size(0, 0);
            this.txbActions.TextToControlDistance = 0;
            this.txbActions.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdUserModules;
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
            this.txbModules.Control = this.grdModules;
            this.txbModules.CustomizationFormText = "Modules";
            this.txbModules.Location = new System.Drawing.Point(198, 0);
            this.txbModules.Name = "txbModules";
            this.txbModules.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 5, 2);
            this.txbModules.Size = new System.Drawing.Size(169, 251);
            this.txbModules.Text = "Modules";
            this.txbModules.TextLocation = DevExpress.Utils.Locations.Top;
            this.txbModules.TextSize = new System.Drawing.Size(0, 0);
            this.txbModules.TextToControlDistance = 0;
            this.txbModules.TextVisible = false;
            // 
            // frmSECGroupsAccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 500);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmSECGroupsAccess";
            this.Text = "Groups Access";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSECGroupsAccess_FormClosing);
            this.Load += new System.EventHandler(this.frmSECGroupsAccess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdUserModules)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewUserModules)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdActions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewActions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkfunctionaccessselected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFunctions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewFunctions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkmoduleitemselected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdModules)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewModules)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkmoduleselected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUserGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewUserGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbUserGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbFunctions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbActions)).EndInit();
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
        private DevExpress.XtraGrid.GridControl grdModules;
        private DevExpress.XtraGrid.Views.Grid.GridView viewModules;
        private DevExpress.XtraLayout.LayoutControlItem txbModules;
        private DevExpress.XtraGrid.Columns.GridColumn modulekey;
        private DevExpress.XtraGrid.Columns.GridColumn moduletext;
        private DevExpress.XtraGrid.GridControl grdFunctions;
        private DevExpress.XtraGrid.Views.Grid.GridView viewFunctions;
        private DevExpress.XtraLayout.LayoutControlItem txbFunctions;
        private DevExpress.XtraGrid.Columns.GridColumn moduleitemselected;
        private DevExpress.XtraGrid.Columns.GridColumn moduleitemkey;
        private DevExpress.XtraGrid.Columns.GridColumn moduleitemtext;
        private DevExpress.XtraGrid.Columns.GridColumn functionsprefix;
        private DevExpress.XtraGrid.GridControl grdActions;
        private DevExpress.XtraGrid.Views.Grid.GridView viewActions;
        private DevExpress.XtraLayout.LayoutControlItem txbActions;
        private DevExpress.XtraGrid.Columns.GridColumn functionaccesskey;
        private DevExpress.XtraGrid.Columns.GridColumn functionaccesstext;
        private DevExpress.XtraGrid.Columns.GridColumn functionaccessselected;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkfunctionaccessselected;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkmoduleitemselected;
        private DevExpress.XtraGrid.GridControl grdUserModules;
        private DevExpress.XtraGrid.Views.Grid.GridView viewUserModules;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn usermoduletext;
        private DevExpress.XtraGrid.Columns.GridColumn usermoduleitemtext;
        private DevExpress.XtraGrid.Columns.GridColumn userfunctionaccesstext;
        private DevExpress.XtraGrid.Columns.GridColumn moduleselected;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkmoduleselected;
    }
}