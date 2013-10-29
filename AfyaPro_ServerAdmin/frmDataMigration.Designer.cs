namespace AfyaPro_ServerAdmin
{
    partial class frmDataMigration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataMigration));
            this.wizardControl1 = new DevExpress.XtraWizard.WizardControl();
            this.welcomePage = new DevExpress.XtraWizard.WelcomeWizardPage();
            this.page0 = new DevExpress.XtraWizard.WizardPage();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.grdObjects = new DevExpress.XtraGrid.GridControl();
            this.viewObjects = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.selected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.description = new DevExpress.XtraGrid.Columns.GridColumn();
            this.progObjects = new DevExpress.XtraEditors.ProgressBarControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.completionWizardPage1 = new DevExpress.XtraWizard.CompletionWizardPage();
            this.page1 = new DevExpress.XtraWizard.WizardPage();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.progMigrating = new DevExpress.XtraEditors.ProgressBarControl();
            this.grdMigrating = new DevExpress.XtraGrid.GridControl();
            this.viewMigrating = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wizardControl1.SuspendLayout();
            this.page0.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdObjects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewObjects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progObjects.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.page1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progMigrating.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMigrating)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewMigrating)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.Controls.Add(this.welcomePage);
            this.wizardControl1.Controls.Add(this.page0);
            this.wizardControl1.Controls.Add(this.completionWizardPage1);
            this.wizardControl1.Controls.Add(this.page1);
            this.wizardControl1.Image = global::AfyaPro_ServerAdmin.Properties.Resources.AfyaProLogoVertical;
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.welcomePage,
            this.page0,
            this.page1,
            this.completionWizardPage1});
            this.wizardControl1.SelectedPageChanging += new DevExpress.XtraWizard.WizardPageChangingEventHandler(this.wizardControl1_SelectedPageChanging);
            this.wizardControl1.FinishClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_FinishClick);
            this.wizardControl1.NextClick += new DevExpress.XtraWizard.WizardCommandButtonClickEventHandler(this.wizardControl1_NextClick);
            this.wizardControl1.CancelClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_CancelClick);
            // 
            // welcomePage
            // 
            this.welcomePage.IntroductionText = "This wizard will guide you through the process of migarting data from Version 2 t" +
                "o Version 3";
            this.welcomePage.Name = "welcomePage";
            this.welcomePage.Size = new System.Drawing.Size(281, 213);
            this.welcomePage.Text = "Welcome to AfyaPro data migration wizard";
            // 
            // page0
            // 
            this.page0.AllowBack = false;
            this.page0.Controls.Add(this.layoutControl1);
            this.page0.DescriptionText = "Please wait while the wizard is collecting data for migration";
            this.page0.Name = "page0";
            this.page0.Size = new System.Drawing.Size(466, 224);
            this.page0.Text = "Data Collection";
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomizationMenu = false;
            this.layoutControl1.Controls.Add(this.grdObjects);
            this.layoutControl1.Controls.Add(this.progObjects);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(466, 224);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // grdObjects
            // 
            this.grdObjects.Location = new System.Drawing.Point(12, 12);
            this.grdObjects.MainView = this.viewObjects;
            this.grdObjects.Name = "grdObjects";
            this.grdObjects.Size = new System.Drawing.Size(442, 178);
            this.grdObjects.TabIndex = 6;
            this.grdObjects.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewObjects});
            // 
            // viewObjects
            // 
            this.viewObjects.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.selected,
            this.description});
            this.viewObjects.GridControl = this.grdObjects;
            this.viewObjects.Name = "viewObjects";
            this.viewObjects.OptionsBehavior.Editable = false;
            this.viewObjects.OptionsView.ShowColumnHeaders = false;
            this.viewObjects.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.viewObjects.OptionsView.ShowGroupPanel = false;
            this.viewObjects.OptionsView.ShowHorzLines = false;
            this.viewObjects.OptionsView.ShowIndicator = false;
            this.viewObjects.OptionsView.ShowPreviewLines = false;
            this.viewObjects.OptionsView.ShowVertLines = false;
            // 
            // selected
            // 
            this.selected.Caption = "gridColumn1";
            this.selected.FieldName = "selected";
            this.selected.Name = "selected";
            this.selected.OptionsColumn.ShowCaption = false;
            this.selected.Visible = true;
            this.selected.VisibleIndex = 0;
            this.selected.Width = 33;
            // 
            // description
            // 
            this.description.Caption = "Description";
            this.description.FieldName = "description";
            this.description.Name = "description";
            this.description.Visible = true;
            this.description.VisibleIndex = 1;
            this.description.Width = 409;
            // 
            // progObjects
            // 
            this.progObjects.Location = new System.Drawing.Point(12, 194);
            this.progObjects.Name = "progObjects";
            this.progObjects.Size = new System.Drawing.Size(442, 18);
            this.progObjects.StyleController = this.layoutControl1;
            this.progObjects.TabIndex = 5;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(466, 224);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.progObjects;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 182);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(446, 22);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdObjects;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(446, 182);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // completionWizardPage1
            // 
            this.completionWizardPage1.Name = "completionWizardPage1";
            this.completionWizardPage1.Size = new System.Drawing.Size(281, 236);
            // 
            // page1
            // 
            this.page1.AllowBack = false;
            this.page1.Controls.Add(this.layoutControl2);
            this.page1.DescriptionText = "Please wait while the wizard is migrating data";
            this.page1.Name = "page1";
            this.page1.Size = new System.Drawing.Size(466, 224);
            this.page1.Text = "Data migration";
            // 
            // layoutControl2
            // 
            this.layoutControl2.AllowCustomizationMenu = false;
            this.layoutControl2.Controls.Add(this.progMigrating);
            this.layoutControl2.Controls.Add(this.grdMigrating);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup2;
            this.layoutControl2.Size = new System.Drawing.Size(466, 224);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // progMigrating
            // 
            this.progMigrating.Location = new System.Drawing.Point(12, 194);
            this.progMigrating.Name = "progMigrating";
            this.progMigrating.Size = new System.Drawing.Size(442, 18);
            this.progMigrating.StyleController = this.layoutControl2;
            this.progMigrating.TabIndex = 5;
            // 
            // grdMigrating
            // 
            this.grdMigrating.Location = new System.Drawing.Point(12, 12);
            this.grdMigrating.MainView = this.viewMigrating;
            this.grdMigrating.Name = "grdMigrating";
            this.grdMigrating.Size = new System.Drawing.Size(442, 178);
            this.grdMigrating.TabIndex = 4;
            this.grdMigrating.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewMigrating});
            // 
            // viewMigrating
            // 
            this.viewMigrating.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.viewMigrating.GridControl = this.grdMigrating;
            this.viewMigrating.Name = "viewMigrating";
            this.viewMigrating.OptionsBehavior.Editable = false;
            this.viewMigrating.OptionsView.ShowColumnHeaders = false;
            this.viewMigrating.OptionsView.ShowDetailButtons = false;
            this.viewMigrating.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.viewMigrating.OptionsView.ShowGroupPanel = false;
            this.viewMigrating.OptionsView.ShowHorzLines = false;
            this.viewMigrating.OptionsView.ShowIndicator = false;
            this.viewMigrating.OptionsView.ShowPreviewLines = false;
            this.viewMigrating.OptionsView.ShowVertLines = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.FieldName = "selected";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.ShowCaption = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 33;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Description";
            this.gridColumn2.FieldName = "description";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 409;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(466, 224);
            this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Text = "layoutControlGroup2";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.grdMigrating;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(446, 182);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.progMigrating;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 182);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(446, 22);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // frmDataMigration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 369);
            this.Controls.Add(this.wizardControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDataMigration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AfyaPro Data migartion wizard";
            this.Load += new System.EventHandler(this.frmDataMigration_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.wizardControl1.ResumeLayout(false);
            this.page0.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdObjects)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewObjects)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progObjects.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.page1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.progMigrating.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMigrating)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewMigrating)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraWizard.WizardControl wizardControl1;
        private DevExpress.XtraWizard.WelcomeWizardPage welcomePage;
        private DevExpress.XtraWizard.WizardPage page0;
        private DevExpress.XtraWizard.CompletionWizardPage completionWizardPage1;
        private DevExpress.XtraWizard.WizardPage page1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.ProgressBarControl progObjects;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.GridControl grdObjects;
        private DevExpress.XtraGrid.Views.Grid.GridView viewObjects;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn selected;
        private DevExpress.XtraGrid.Columns.GridColumn description;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraEditors.ProgressBarControl progMigrating;
        private DevExpress.XtraGrid.GridControl grdMigrating;
        private DevExpress.XtraGrid.Views.Grid.GridView viewMigrating;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    }
}