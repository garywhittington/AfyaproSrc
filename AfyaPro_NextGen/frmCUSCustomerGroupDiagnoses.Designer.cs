namespace AfyaPro_NextGen
{
    partial class frmCUSCustomerGroupDiagnoses
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
            this.chkAll = new DevExpress.XtraEditors.CheckEdit();
            this.cmdSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            this.cmdSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.grdCUSCustomerGroupDiagnoses = new DevExpress.XtraGrid.GridControl();
            this.viewCUSCustomerGroupDiagnoses = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.selected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkselected = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.description = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCUSCustomerGroupDiagnoses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewCUSCustomerGroupDiagnoses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkselected)).BeginInit();
            this.SuspendLayout();
            // 
            // chkAll
            // 
            this.chkAll.Location = new System.Drawing.Point(3, 35);
            this.chkAll.Name = "chkAll";
            this.chkAll.Properties.Caption = "All Items";
            this.chkAll.Size = new System.Drawing.Size(75, 19);
            this.chkAll.TabIndex = 1;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            this.chkAll.Enter += new System.EventHandler(this.chkAll_Enter);
            // 
            // cmdSearch
            // 
            this.cmdSearch.Location = new System.Drawing.Point(410, 5);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(95, 50);
            this.cmdSearch.TabIndex = 5;
            this.cmdSearch.Text = "Search";
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.cmdClose);
            this.panelControl1.Controls.Add(this.cmdSave);
            this.panelControl1.Controls.Add(this.txtCode);
            this.panelControl1.Controls.Add(this.cmdSearch);
            this.panelControl1.Controls.Add(this.chkAll);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(712, 64);
            this.panelControl1.TabIndex = 6;
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(606, 5);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(95, 50);
            this.cmdClose.TabIndex = 8;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(508, 5);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(95, 50);
            this.cmdSave.TabIndex = 7;
            this.cmdSave.Text = "Save";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(12, 2);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(100, 20);
            this.txtCode.TabIndex = 6;
            this.txtCode.Visible = false;
            this.txtCode.EditValueChanged += new System.EventHandler(this.txtCode_EditValueChanged);
            // 
            // grdCUSCustomerGroupDiagnoses
            // 
            this.grdCUSCustomerGroupDiagnoses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCUSCustomerGroupDiagnoses.Location = new System.Drawing.Point(0, 64);
            this.grdCUSCustomerGroupDiagnoses.MainView = this.viewCUSCustomerGroupDiagnoses;
            this.grdCUSCustomerGroupDiagnoses.Name = "grdCUSCustomerGroupDiagnoses";
            this.grdCUSCustomerGroupDiagnoses.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkselected});
            this.grdCUSCustomerGroupDiagnoses.Size = new System.Drawing.Size(712, 445);
            this.grdCUSCustomerGroupDiagnoses.TabIndex = 7;
            this.grdCUSCustomerGroupDiagnoses.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewCUSCustomerGroupDiagnoses});
            this.grdCUSCustomerGroupDiagnoses.Enter += new System.EventHandler(this.grdCUSCustomerGroupDiagnoses_Enter);
            // 
            // viewCUSCustomerGroupDiagnoses
            // 
            this.viewCUSCustomerGroupDiagnoses.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.selected,
            this.code,
            this.description});
            this.viewCUSCustomerGroupDiagnoses.GridControl = this.grdCUSCustomerGroupDiagnoses;
            this.viewCUSCustomerGroupDiagnoses.Name = "viewCUSCustomerGroupDiagnoses";
            this.viewCUSCustomerGroupDiagnoses.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.viewCUSCustomerGroupDiagnoses.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.viewCUSCustomerGroupDiagnoses.OptionsView.ShowGroupPanel = false;
            this.viewCUSCustomerGroupDiagnoses.OptionsView.ShowIndicator = false;
            // 
            // selected
            // 
            this.selected.ColumnEdit = this.chkselected;
            this.selected.FieldName = "selected";
            this.selected.Name = "selected";
            this.selected.OptionsColumn.ShowCaption = false;
            this.selected.Visible = true;
            this.selected.VisibleIndex = 0;
            this.selected.Width = 46;
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
            this.code.Width = 99;
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
            this.description.Width = 579;
            // 
            // frmCUSCustomerGroupDiagnoses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 509);
            this.Controls.Add(this.grdCUSCustomerGroupDiagnoses);
            this.Controls.Add(this.panelControl1);
            this.Name = "frmCUSCustomerGroupDiagnoses";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Group Diagnoses";
            this.Load += new System.EventHandler(this.frmCUSCustomerGroupDiagnoses_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCUSCustomerGroupDiagnoses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewCUSCustomerGroupDiagnoses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkselected)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit chkAll;
        private DevExpress.XtraEditors.SimpleButton cmdSearch;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl grdCUSCustomerGroupDiagnoses;
        private DevExpress.XtraGrid.Views.Grid.GridView viewCUSCustomerGroupDiagnoses;
        private DevExpress.XtraGrid.Columns.GridColumn selected;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkselected;
        private DevExpress.XtraGrid.Columns.GridColumn code;
        private DevExpress.XtraGrid.Columns.GridColumn description;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.SimpleButton cmdSave;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
    }
}