namespace AfyaPro_NextGen
{
    partial class frmTOOImportation
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
            this.wizardControl1 = new DevExpress.XtraWizard.WizardControl();
            this.pageWelcome = new DevExpress.XtraWizard.WelcomeWizardPage();
            this.page0 = new DevExpress.XtraWizard.WizardPage();
            this.grpSepChar = new DevExpress.XtraEditors.GroupControl();
            this.radSepChar = new DevExpress.XtraEditors.RadioGroup();
            this.cmdBrowse = new DevExpress.XtraEditors.SimpleButton();
            this.txtFileName = new DevExpress.XtraEditors.TextEdit();
            this.txbFileName = new DevExpress.XtraEditors.LabelControl();
            this.radSourceType = new DevExpress.XtraEditors.RadioGroup();
            this.pageFinish = new DevExpress.XtraWizard.CompletionWizardPage();
            this.page3 = new DevExpress.XtraWizard.WizardPage();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.chkAutoGenerate = new DevExpress.XtraEditors.CheckEdit();
            this.page1 = new DevExpress.XtraWizard.WizardPage();
            this.txbDestinationTable = new DevExpress.XtraEditors.LabelControl();
            this.lstDestinationTables = new DevExpress.XtraEditors.ListBoxControl();
            this.txbSourceTable = new DevExpress.XtraEditors.LabelControl();
            this.lstSourceTables = new DevExpress.XtraEditors.ListBoxControl();
            this.page2 = new DevExpress.XtraWizard.WizardPage();
            this.grdFields = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.sourcefieldname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboSourceFields = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.destinationfieldname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.allowdbnull = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkAllowDbNull = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wizardControl1.SuspendLayout();
            this.page0.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpSepChar)).BeginInit();
            this.grpSepChar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radSepChar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSourceType.Properties)).BeginInit();
            this.page3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoGenerate.Properties)).BeginInit();
            this.page1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstDestinationTables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstSourceTables)).BeginInit();
            this.page2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSourceFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllowDbNull)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.Controls.Add(this.pageWelcome);
            this.wizardControl1.Controls.Add(this.page0);
            this.wizardControl1.Controls.Add(this.pageFinish);
            this.wizardControl1.Controls.Add(this.page3);
            this.wizardControl1.Controls.Add(this.page1);
            this.wizardControl1.Controls.Add(this.page2);
            this.wizardControl1.Image = global::AfyaPro_NextGen.Properties.Resources.AfyaProLogoVertical;
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.pageWelcome,
            this.page0,
            this.page1,
            this.page2,
            this.page3,
            this.pageFinish});
            this.wizardControl1.UseCancelButton = false;
            this.wizardControl1.SelectedPageChanging += new DevExpress.XtraWizard.WizardPageChangingEventHandler(this.wizardControl1_SelectedPageChanging);
            this.wizardControl1.FinishClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_FinishClick);
            this.wizardControl1.NextClick += new DevExpress.XtraWizard.WizardCommandButtonClickEventHandler(this.wizardControl1_NextClick);
            this.wizardControl1.CancelClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_CancelClick);
            // 
            // pageWelcome
            // 
            this.pageWelcome.IntroductionText = "This wizard will walk you through the process of importing data from a different " +
                "source to afyapro";
            this.pageWelcome.Name = "pageWelcome";
            this.pageWelcome.Size = new System.Drawing.Size(302, 293);
            this.pageWelcome.Text = "Welcome to Sample Wizard";
            // 
            // page0
            // 
            this.page0.Controls.Add(this.grpSepChar);
            this.page0.Controls.Add(this.cmdBrowse);
            this.page0.Controls.Add(this.txtFileName);
            this.page0.Controls.Add(this.txbFileName);
            this.page0.Controls.Add(this.radSourceType);
            this.page0.DescriptionText = "From where do you want to import data";
            this.page0.Name = "page0";
            this.page0.Size = new System.Drawing.Size(487, 281);
            this.page0.Text = "Source";
            // 
            // grpSepChar
            // 
            this.grpSepChar.Controls.Add(this.radSepChar);
            this.grpSepChar.Enabled = false;
            this.grpSepChar.Location = new System.Drawing.Point(15, 127);
            this.grpSepChar.Name = "grpSepChar";
            this.grpSepChar.Size = new System.Drawing.Size(269, 94);
            this.grpSepChar.TabIndex = 5;
            this.grpSepChar.Text = "Columns Separator for CSV";
            // 
            // radSepChar
            // 
            this.radSepChar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radSepChar.Location = new System.Drawing.Point(2, 20);
            this.radSepChar.Name = "radSepChar";
            this.radSepChar.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Comma"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tab")});
            this.radSepChar.Size = new System.Drawing.Size(265, 72);
            this.radSepChar.TabIndex = 4;
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.Location = new System.Drawing.Point(378, 248);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(104, 23);
            this.cmdBrowse.TabIndex = 3;
            this.cmdBrowse.Text = "Browse";
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(15, 250);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(361, 20);
            this.txtFileName.TabIndex = 2;
            // 
            // txbFileName
            // 
            this.txbFileName.Location = new System.Drawing.Point(16, 231);
            this.txbFileName.Name = "txbFileName";
            this.txbFileName.Size = new System.Drawing.Size(43, 13);
            this.txbFileName.TabIndex = 1;
            this.txbFileName.Text = "FileName";
            // 
            // radSourceType
            // 
            this.radSourceType.Location = new System.Drawing.Point(15, 15);
            this.radSourceType.Name = "radSourceType";
            this.radSourceType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Microsoft Excel 97-2003 File"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "CSV File"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "XML File")});
            this.radSourceType.Size = new System.Drawing.Size(269, 104);
            this.radSourceType.TabIndex = 0;
            this.radSourceType.SelectedIndexChanged += new System.EventHandler(this.radSourceType_SelectedIndexChanged);
            // 
            // pageFinish
            // 
            this.pageFinish.FinishText = "You have successfully completed the data importation wizard";
            this.pageFinish.Name = "pageFinish";
            this.pageFinish.Size = new System.Drawing.Size(302, 293);
            this.pageFinish.Text = "Wizard Completed";
            // 
            // page3
            // 
            this.page3.Controls.Add(this.grdData);
            this.page3.Controls.Add(this.chkAutoGenerate);
            this.page3.DescriptionText = "The wizard has finished collection information necessary to start importation. Cl" +
                "ient Next to begin importation process";
            this.page3.Name = "page3";
            this.page3.Size = new System.Drawing.Size(487, 281);
            this.page3.Text = "Ready to Start Importation";
            // 
            // grdData
            // 
            this.grdData.EmbeddedNavigator.Name = "";
            this.grdData.Location = new System.Drawing.Point(4, 25);
            this.grdData.MainView = this.gridView1;
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(477, 251);
            this.grdData.TabIndex = 2;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.gridView2});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdData;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.grdData;
            this.gridView2.Name = "gridView2";
            // 
            // chkAutoGenerate
            // 
            this.chkAutoGenerate.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkAutoGenerate.Location = new System.Drawing.Point(0, 0);
            this.chkAutoGenerate.Name = "chkAutoGenerate";
            this.chkAutoGenerate.Properties.Caption = "Auto Generate Code when Importing";
            this.chkAutoGenerate.Size = new System.Drawing.Size(487, 19);
            this.chkAutoGenerate.TabIndex = 1;
            // 
            // page1
            // 
            this.page1.Controls.Add(this.txbDestinationTable);
            this.page1.Controls.Add(this.lstDestinationTables);
            this.page1.Controls.Add(this.txbSourceTable);
            this.page1.Controls.Add(this.lstSourceTables);
            this.page1.DescriptionText = "Select source and destination table and then click Next";
            this.page1.Name = "page1";
            this.page1.Size = new System.Drawing.Size(487, 281);
            this.page1.Text = "Table Selection";
            // 
            // txbDestinationTable
            // 
            this.txbDestinationTable.Location = new System.Drawing.Point(227, 6);
            this.txbDestinationTable.Name = "txbDestinationTable";
            this.txbDestinationTable.Size = new System.Drawing.Size(54, 13);
            this.txbDestinationTable.TabIndex = 3;
            this.txbDestinationTable.Text = "Destination";
            // 
            // lstDestinationTables
            // 
            this.lstDestinationTables.Location = new System.Drawing.Point(227, 26);
            this.lstDestinationTables.Name = "lstDestinationTables";
            this.lstDestinationTables.Size = new System.Drawing.Size(215, 249);
            this.lstDestinationTables.TabIndex = 2;
            // 
            // txbSourceTable
            // 
            this.txbSourceTable.Location = new System.Drawing.Point(4, 6);
            this.txbSourceTable.Name = "txbSourceTable";
            this.txbSourceTable.Size = new System.Drawing.Size(33, 13);
            this.txbSourceTable.TabIndex = 1;
            this.txbSourceTable.Text = "Source";
            // 
            // lstSourceTables
            // 
            this.lstSourceTables.Location = new System.Drawing.Point(5, 26);
            this.lstSourceTables.Name = "lstSourceTables";
            this.lstSourceTables.Size = new System.Drawing.Size(215, 249);
            this.lstSourceTables.TabIndex = 0;
            // 
            // page2
            // 
            this.page2.Controls.Add(this.grdFields);
            this.page2.DescriptionText = "Map fields from the source table to destination table and then click Next";
            this.page2.Name = "page2";
            this.page2.Size = new System.Drawing.Size(487, 281);
            this.page2.Text = "Field Mapping";
            // 
            // grdFields
            // 
            this.grdFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFields.EmbeddedNavigator.Name = "";
            this.grdFields.Location = new System.Drawing.Point(0, 0);
            this.grdFields.MainView = this.gridView3;
            this.grdFields.Name = "grdFields";
            this.grdFields.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cboSourceFields,
            this.chkAllowDbNull});
            this.grdFields.Size = new System.Drawing.Size(487, 281);
            this.grdFields.TabIndex = 0;
            this.grdFields.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.sourcefieldname,
            this.destinationfieldname,
            this.allowdbnull});
            this.gridView3.GridControl = this.grdFields;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            this.gridView3.OptionsView.ShowIndicator = false;
            // 
            // sourcefieldname
            // 
            this.sourcefieldname.Caption = "Source";
            this.sourcefieldname.ColumnEdit = this.cboSourceFields;
            this.sourcefieldname.FieldName = "sourcefieldname";
            this.sourcefieldname.Name = "sourcefieldname";
            this.sourcefieldname.Visible = true;
            this.sourcefieldname.VisibleIndex = 0;
            // 
            // cboSourceFields
            // 
            this.cboSourceFields.AutoHeight = false;
            this.cboSourceFields.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSourceFields.Name = "cboSourceFields";
            this.cboSourceFields.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // destinationfieldname
            // 
            this.destinationfieldname.Caption = "Destination";
            this.destinationfieldname.FieldName = "destinationfieldname";
            this.destinationfieldname.Name = "destinationfieldname";
            this.destinationfieldname.OptionsColumn.AllowEdit = false;
            this.destinationfieldname.OptionsColumn.AllowFocus = false;
            this.destinationfieldname.Visible = true;
            this.destinationfieldname.VisibleIndex = 1;
            // 
            // allowdbnull
            // 
            this.allowdbnull.Caption = "Allow Null";
            this.allowdbnull.ColumnEdit = this.chkAllowDbNull;
            this.allowdbnull.FieldName = "allowdbnull";
            this.allowdbnull.Name = "allowdbnull";
            this.allowdbnull.Visible = true;
            this.allowdbnull.VisibleIndex = 2;
            // 
            // chkAllowDbNull
            // 
            this.chkAllowDbNull.AutoHeight = false;
            this.chkAllowDbNull.Name = "chkAllowDbNull";
            // 
            // frmTOOImportation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 426);
            this.Controls.Add(this.wizardControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmTOOImportation";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AfyaPro Data Importation Wizard";
            this.Load += new System.EventHandler(this.frmTOOImportation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.wizardControl1.ResumeLayout(false);
            this.page0.ResumeLayout(false);
            this.page0.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpSepChar)).EndInit();
            this.grpSepChar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radSepChar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSourceType.Properties)).EndInit();
            this.page3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoGenerate.Properties)).EndInit();
            this.page1.ResumeLayout(false);
            this.page1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstDestinationTables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstSourceTables)).EndInit();
            this.page2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSourceFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllowDbNull)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraWizard.WizardControl wizardControl1;
        private DevExpress.XtraWizard.WelcomeWizardPage pageWelcome;
        private DevExpress.XtraWizard.WizardPage page0;
        private DevExpress.XtraWizard.CompletionWizardPage pageFinish;
        private DevExpress.XtraEditors.RadioGroup radSourceType;
        private DevExpress.XtraEditors.SimpleButton cmdBrowse;
        private DevExpress.XtraEditors.TextEdit txtFileName;
        private DevExpress.XtraEditors.LabelControl txbFileName;
        private DevExpress.XtraEditors.GroupControl grpSepChar;
        private DevExpress.XtraEditors.RadioGroup radSepChar;
        private DevExpress.XtraWizard.WizardPage page3;
        private DevExpress.XtraWizard.WizardPage page1;
        private DevExpress.XtraEditors.ListBoxControl lstSourceTables;
        private DevExpress.XtraWizard.WizardPage page2;
        private DevExpress.XtraGrid.GridControl grdFields;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn sourcefieldname;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cboSourceFields;
        private DevExpress.XtraGrid.Columns.GridColumn destinationfieldname;
        private DevExpress.XtraGrid.Columns.GridColumn allowdbnull;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkAllowDbNull;
        private DevExpress.XtraEditors.LabelControl txbDestinationTable;
        private DevExpress.XtraEditors.ListBoxControl lstDestinationTables;
        private DevExpress.XtraEditors.LabelControl txbSourceTable;
        private DevExpress.XtraEditors.CheckEdit chkAutoGenerate;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
    }
}