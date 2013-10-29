namespace AfyaPro_NextGen
{
    partial class frmCUSCustomerGroupItems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCUSCustomerGroupItems));
            this.chkAll = new DevExpress.XtraEditors.CheckEdit();
            this.txbPercent = new DevExpress.XtraEditors.LabelControl();
            this.txtPercent = new DevExpress.XtraEditors.TextEdit();
            this.cmdApply = new DevExpress.XtraEditors.SimpleButton();
            this.cmdSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.grdCUSCustomerGroupItems = new DevExpress.XtraGrid.GridControl();
            this.viewCUSCustomerGroupItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.selected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkselected = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.description = new DevExpress.XtraGrid.Columns.GridColumn();
            this.price = new DevExpress.XtraGrid.Columns.GridColumn();
            this.percent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmdSave = new DevExpress.XtraEditors.SimpleButton();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPercent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCUSCustomerGroupItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewCUSCustomerGroupItems)).BeginInit();
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
            // txbPercent
            // 
            this.txbPercent.Location = new System.Drawing.Point(201, 9);
            this.txbPercent.Name = "txbPercent";
            this.txbPercent.Size = new System.Drawing.Size(77, 13);
            this.txbPercent.TabIndex = 2;
            this.txbPercent.Text = "Percentage (%)";
            // 
            // txtPercent
            // 
            this.txtPercent.Location = new System.Drawing.Point(202, 29);
            this.txtPercent.Name = "txtPercent";
            this.txtPercent.Size = new System.Drawing.Size(100, 20);
            this.txtPercent.TabIndex = 3;
            // 
            // cmdApply
            // 
            this.cmdApply.Location = new System.Drawing.Point(312, 5);
            this.cmdApply.Name = "cmdApply";
            this.cmdApply.Size = new System.Drawing.Size(95, 50);
            this.cmdApply.TabIndex = 4;
            this.cmdApply.Text = "Apply to All";
            this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
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
            this.panelControl1.Controls.Add(this.cmdApply);
            this.panelControl1.Controls.Add(this.txbPercent);
            this.panelControl1.Controls.Add(this.txtPercent);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(728, 64);
            this.panelControl1.TabIndex = 6;
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
            // grdCUSCustomerGroupItems
            // 
            this.grdCUSCustomerGroupItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCUSCustomerGroupItems.Location = new System.Drawing.Point(0, 64);
            this.grdCUSCustomerGroupItems.MainView = this.viewCUSCustomerGroupItems;
            this.grdCUSCustomerGroupItems.Name = "grdCUSCustomerGroupItems";
            this.grdCUSCustomerGroupItems.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkselected});
            this.grdCUSCustomerGroupItems.Size = new System.Drawing.Size(728, 445);
            this.grdCUSCustomerGroupItems.TabIndex = 7;
            this.grdCUSCustomerGroupItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewCUSCustomerGroupItems});
            this.grdCUSCustomerGroupItems.Enter += new System.EventHandler(this.grdCUSCustomerGroupItems_Enter);
            // 
            // viewCUSCustomerGroupItems
            // 
            this.viewCUSCustomerGroupItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.selected,
            this.code,
            this.description,
            this.price,
            this.percent});
            this.viewCUSCustomerGroupItems.GridControl = this.grdCUSCustomerGroupItems;
            this.viewCUSCustomerGroupItems.Name = "viewCUSCustomerGroupItems";
            this.viewCUSCustomerGroupItems.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.viewCUSCustomerGroupItems.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.viewCUSCustomerGroupItems.OptionsView.ShowGroupPanel = false;
            this.viewCUSCustomerGroupItems.OptionsView.ShowIndicator = false;
            // 
            // selected
            // 
            this.selected.ColumnEdit = this.chkselected;
            this.selected.FieldName = "selected";
            this.selected.Name = "selected";
            this.selected.OptionsColumn.ShowCaption = false;
            this.selected.Visible = true;
            this.selected.VisibleIndex = 0;
            this.selected.Width = 32;
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
            this.code.Width = 89;
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
            this.description.Width = 259;
            // 
            // price
            // 
            this.price.Caption = "gridColumn4";
            this.price.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.price.FieldName = "price";
            this.price.Name = "price";
            this.price.OptionsColumn.AllowEdit = false;
            this.price.OptionsColumn.AllowFocus = false;
            this.price.Visible = true;
            this.price.VisibleIndex = 3;
            this.price.Width = 105;
            // 
            // percent
            // 
            this.percent.Caption = "Percent";
            this.percent.FieldName = "percent";
            this.percent.Name = "percent";
            this.percent.Visible = true;
            this.percent.VisibleIndex = 4;
            this.percent.Width = 67;
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
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(606, 5);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(95, 50);
            this.cmdClose.TabIndex = 8;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // frmCUSCustomerGroupItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 509);
            this.Controls.Add(this.grdCUSCustomerGroupItems);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCUSCustomerGroupItems";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Group Items";
            this.Load += new System.EventHandler(this.frmCUSCustomerGroupItems_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPercent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCUSCustomerGroupItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewCUSCustomerGroupItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkselected)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit chkAll;
        private DevExpress.XtraEditors.LabelControl txbPercent;
        private DevExpress.XtraEditors.TextEdit txtPercent;
        private DevExpress.XtraEditors.SimpleButton cmdApply;
        private DevExpress.XtraEditors.SimpleButton cmdSearch;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl grdCUSCustomerGroupItems;
        private DevExpress.XtraGrid.Views.Grid.GridView viewCUSCustomerGroupItems;
        private DevExpress.XtraGrid.Columns.GridColumn selected;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkselected;
        private DevExpress.XtraGrid.Columns.GridColumn code;
        private DevExpress.XtraGrid.Columns.GridColumn description;
        private DevExpress.XtraGrid.Columns.GridColumn price;
        private DevExpress.XtraGrid.Columns.GridColumn percent;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.SimpleButton cmdSave;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
    }
}