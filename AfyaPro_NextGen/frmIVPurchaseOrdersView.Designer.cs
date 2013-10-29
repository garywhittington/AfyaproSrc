namespace AfyaPro_NextGen
{
    partial class frmIVPurchaseOrdersView
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
            this.grdIVPurchaseOrdersView = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.supplierdescription = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cmdDesign = new DevExpress.XtraEditors.SimpleButton();
            this.cmdReceive = new DevExpress.XtraEditors.SimpleButton();
            this.cmdEdit = new DevExpress.XtraEditors.SimpleButton();
            this.radOrderStatus = new DevExpress.XtraEditors.RadioGroup();
            this.cmdPreview = new DevExpress.XtraEditors.SimpleButton();
            this.cmdRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.txtDateTo = new DevExpress.XtraEditors.DateEdit();
            this.txtDateFrom = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.grpDate = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbDateFrom = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbDateTo = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbRefresh = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbOrderStatus = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbEdit = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbReceive = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbDesign = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.grdIVPurchaseOrdersView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.supplierdescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radOrderStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbDateFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbDateTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbOrderStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbReceive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbDesign)).BeginInit();
            this.SuspendLayout();
            // 
            // grdIVPurchaseOrdersView
            // 
            this.grdIVPurchaseOrdersView.Location = new System.Drawing.Point(12, 104);
            this.grdIVPurchaseOrdersView.MainView = this.gridView1;
            this.grdIVPurchaseOrdersView.Name = "grdIVPurchaseOrdersView";
            this.grdIVPurchaseOrdersView.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.supplierdescription});
            this.grdIVPurchaseOrdersView.Size = new System.Drawing.Size(785, 357);
            this.grdIVPurchaseOrdersView.TabIndex = 10;
            this.grdIVPurchaseOrdersView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.grdIVPurchaseOrdersView.Visible = false;
            this.grdIVPurchaseOrdersView.DoubleClick += new System.EventHandler(this.grdIVPurchaseOrdersView_DoubleClick);
            this.grdIVPurchaseOrdersView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdIVPurchaseOrdersView_KeyDown);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdIVPurchaseOrdersView;
            this.gridView1.Name = "gridView1";
            // 
            // supplierdescription
            // 
            this.supplierdescription.Appearance.Options.UseTextOptions = true;
            this.supplierdescription.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.supplierdescription.Name = "supplierdescription";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cmdDesign);
            this.layoutControl1.Controls.Add(this.cmdReceive);
            this.layoutControl1.Controls.Add(this.cmdEdit);
            this.layoutControl1.Controls.Add(this.radOrderStatus);
            this.layoutControl1.Controls.Add(this.cmdPreview);
            this.layoutControl1.Controls.Add(this.cmdRefresh);
            this.layoutControl1.Controls.Add(this.txtDateTo);
            this.layoutControl1.Controls.Add(this.txtDateFrom);
            this.layoutControl1.Controls.Add(this.grdIVPurchaseOrdersView);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(809, 473);
            this.layoutControl1.TabIndex = 11;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cmdDesign
            // 
            this.cmdDesign.Location = new System.Drawing.Point(625, 12);
            this.cmdDesign.Name = "cmdDesign";
            this.cmdDesign.Size = new System.Drawing.Size(81, 88);
            this.cmdDesign.StyleController = this.layoutControl1;
            this.cmdDesign.TabIndex = 21;
            this.cmdDesign.Text = "Design";
            this.cmdDesign.Click += new System.EventHandler(this.cmdDesign_Click);
            // 
            // cmdReceive
            // 
            this.cmdReceive.Location = new System.Drawing.Point(543, 12);
            this.cmdReceive.Name = "cmdReceive";
            this.cmdReceive.Size = new System.Drawing.Size(78, 88);
            this.cmdReceive.StyleController = this.layoutControl1;
            this.cmdReceive.TabIndex = 20;
            this.cmdReceive.Text = "Receive";
            this.cmdReceive.Click += new System.EventHandler(this.cmdReceive_Click);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(461, 12);
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Size = new System.Drawing.Size(78, 88);
            this.cmdEdit.StyleController = this.layoutControl1;
            this.cmdEdit.TabIndex = 19;
            this.cmdEdit.Text = "Edit";
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // radOrderStatus
            // 
            this.radOrderStatus.Location = new System.Drawing.Point(212, 12);
            this.radOrderStatus.Name = "radOrderStatus";
            this.radOrderStatus.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Show All"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Show Open"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Show Closed")});
            this.radOrderStatus.Size = new System.Drawing.Size(138, 88);
            this.radOrderStatus.StyleController = this.layoutControl1;
            this.radOrderStatus.TabIndex = 18;
            // 
            // cmdPreview
            // 
            this.cmdPreview.Location = new System.Drawing.Point(710, 12);
            this.cmdPreview.Name = "cmdPreview";
            this.cmdPreview.Size = new System.Drawing.Size(87, 88);
            this.cmdPreview.StyleController = this.layoutControl1;
            this.cmdPreview.TabIndex = 17;
            this.cmdPreview.Text = "Print Preview";
            this.cmdPreview.Click += new System.EventHandler(this.cmdPreview_Click);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Location = new System.Drawing.Point(354, 12);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(103, 88);
            this.cmdRefresh.StyleController = this.layoutControl1;
            this.cmdRefresh.TabIndex = 16;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // txtDateTo
            // 
            this.txtDateTo.EditValue = null;
            this.txtDateTo.Location = new System.Drawing.Point(52, 68);
            this.txtDateTo.Name = "txtDateTo";
            this.txtDateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDateTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDateTo.Size = new System.Drawing.Size(144, 20);
            this.txtDateTo.StyleController = this.layoutControl1;
            this.txtDateTo.TabIndex = 11;
            // 
            // txtDateFrom
            // 
            this.txtDateFrom.EditValue = null;
            this.txtDateFrom.Location = new System.Drawing.Point(52, 44);
            this.txtDateFrom.Name = "txtDateFrom";
            this.txtDateFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDateFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDateFrom.Size = new System.Drawing.Size(144, 20);
            this.txtDateFrom.StyleController = this.layoutControl1;
            this.txtDateFrom.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.txbGrid,
            this.grpDate,
            this.txbRefresh,
            this.layoutControlItem1,
            this.txbOrderStatus,
            this.txbEdit,
            this.txbReceive,
            this.txbDesign});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(809, 473);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // txbGrid
            // 
            this.txbGrid.Control = this.grdIVPurchaseOrdersView;
            this.txbGrid.CustomizationFormText = "txbGrid";
            this.txbGrid.Location = new System.Drawing.Point(0, 92);
            this.txbGrid.Name = "txbGrid";
            this.txbGrid.Size = new System.Drawing.Size(789, 361);
            this.txbGrid.Text = "txbGrid";
            this.txbGrid.TextLocation = DevExpress.Utils.Locations.Top;
            this.txbGrid.TextSize = new System.Drawing.Size(0, 0);
            this.txbGrid.TextToControlDistance = 0;
            this.txbGrid.TextVisible = false;
            // 
            // grpDate
            // 
            this.grpDate.CustomizationFormText = "Date";
            this.grpDate.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.txbDateFrom,
            this.txbDateTo});
            this.grpDate.Location = new System.Drawing.Point(0, 0);
            this.grpDate.Name = "grpDate";
            this.grpDate.Size = new System.Drawing.Size(200, 92);
            this.grpDate.Text = "Date";
            // 
            // txbDateFrom
            // 
            this.txbDateFrom.Control = this.txtDateFrom;
            this.txbDateFrom.CustomizationFormText = "From";
            this.txbDateFrom.Location = new System.Drawing.Point(0, 0);
            this.txbDateFrom.Name = "txbDateFrom";
            this.txbDateFrom.Size = new System.Drawing.Size(176, 24);
            this.txbDateFrom.Text = "From";
            this.txbDateFrom.TextSize = new System.Drawing.Size(24, 13);
            // 
            // txbDateTo
            // 
            this.txbDateTo.Control = this.txtDateTo;
            this.txbDateTo.CustomizationFormText = "To";
            this.txbDateTo.Location = new System.Drawing.Point(0, 24);
            this.txbDateTo.Name = "txbDateTo";
            this.txbDateTo.Size = new System.Drawing.Size(176, 24);
            this.txbDateTo.Text = "To";
            this.txbDateTo.TextSize = new System.Drawing.Size(24, 13);
            // 
            // txbRefresh
            // 
            this.txbRefresh.Control = this.cmdRefresh;
            this.txbRefresh.CustomizationFormText = "Refresh";
            this.txbRefresh.Location = new System.Drawing.Point(342, 0);
            this.txbRefresh.MinSize = new System.Drawing.Size(91, 33);
            this.txbRefresh.Name = "txbRefresh";
            this.txbRefresh.Size = new System.Drawing.Size(107, 92);
            this.txbRefresh.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbRefresh.Text = "Refresh";
            this.txbRefresh.TextSize = new System.Drawing.Size(0, 0);
            this.txbRefresh.TextToControlDistance = 0;
            this.txbRefresh.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cmdPreview;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(698, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(91, 33);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(91, 92);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // txbOrderStatus
            // 
            this.txbOrderStatus.Control = this.radOrderStatus;
            this.txbOrderStatus.CustomizationFormText = "txbOrderStatus";
            this.txbOrderStatus.Location = new System.Drawing.Point(200, 0);
            this.txbOrderStatus.Name = "txbOrderStatus";
            this.txbOrderStatus.Size = new System.Drawing.Size(142, 92);
            this.txbOrderStatus.Text = "txbOrderStatus";
            this.txbOrderStatus.TextSize = new System.Drawing.Size(0, 0);
            this.txbOrderStatus.TextToControlDistance = 0;
            this.txbOrderStatus.TextVisible = false;
            // 
            // txbEdit
            // 
            this.txbEdit.Control = this.cmdEdit;
            this.txbEdit.CustomizationFormText = "txbEdit";
            this.txbEdit.Location = new System.Drawing.Point(449, 0);
            this.txbEdit.MinSize = new System.Drawing.Size(82, 26);
            this.txbEdit.Name = "txbEdit";
            this.txbEdit.Size = new System.Drawing.Size(82, 92);
            this.txbEdit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbEdit.Text = "txbEdit";
            this.txbEdit.TextSize = new System.Drawing.Size(0, 0);
            this.txbEdit.TextToControlDistance = 0;
            this.txbEdit.TextVisible = false;
            // 
            // txbReceive
            // 
            this.txbReceive.Control = this.cmdReceive;
            this.txbReceive.CustomizationFormText = "txbReceive";
            this.txbReceive.Location = new System.Drawing.Point(531, 0);
            this.txbReceive.MinSize = new System.Drawing.Size(82, 26);
            this.txbReceive.Name = "txbReceive";
            this.txbReceive.Size = new System.Drawing.Size(82, 92);
            this.txbReceive.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbReceive.Text = "txbReceive";
            this.txbReceive.TextSize = new System.Drawing.Size(0, 0);
            this.txbReceive.TextToControlDistance = 0;
            this.txbReceive.TextVisible = false;
            // 
            // txbDesign
            // 
            this.txbDesign.Control = this.cmdDesign;
            this.txbDesign.CustomizationFormText = "txbDesign";
            this.txbDesign.Location = new System.Drawing.Point(613, 0);
            this.txbDesign.MinSize = new System.Drawing.Size(82, 26);
            this.txbDesign.Name = "txbDesign";
            this.txbDesign.Size = new System.Drawing.Size(85, 92);
            this.txbDesign.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbDesign.Text = "txbDesign";
            this.txbDesign.TextSize = new System.Drawing.Size(0, 0);
            this.txbDesign.TextToControlDistance = 0;
            this.txbDesign.TextVisible = false;
            // 
            // frmIVPurchaseOrdersView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 473);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmIVPurchaseOrdersView";
            this.Text = "Purchase Orders";
            this.Load += new System.EventHandler(this.frmIVPurchaseOrdersView_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmIVPurchaseOrdersView_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.grdIVPurchaseOrdersView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.supplierdescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radOrderStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbDateFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbDateTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbOrderStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbReceive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbDesign)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraGrid.GridControl grdIVPurchaseOrdersView;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem txbGrid;
        private DevExpress.XtraEditors.DateEdit txtDateTo;
        private DevExpress.XtraEditors.DateEdit txtDateFrom;
        private DevExpress.XtraLayout.LayoutControlGroup grpDate;
        private DevExpress.XtraLayout.LayoutControlItem txbDateFrom;
        private DevExpress.XtraLayout.LayoutControlItem txbDateTo;
        private DevExpress.XtraEditors.SimpleButton cmdRefresh;
        private DevExpress.XtraLayout.LayoutControlItem txbRefresh;
        private DevExpress.XtraEditors.SimpleButton cmdPreview;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.RadioGroup radOrderStatus;
        private DevExpress.XtraLayout.LayoutControlItem txbOrderStatus;
        private DevExpress.XtraEditors.SimpleButton cmdReceive;
        private DevExpress.XtraEditors.SimpleButton cmdEdit;
        private DevExpress.XtraLayout.LayoutControlItem txbEdit;
        private DevExpress.XtraLayout.LayoutControlItem txbReceive;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit supplierdescription;
        private DevExpress.XtraEditors.SimpleButton cmdDesign;
        private DevExpress.XtraLayout.LayoutControlItem txbDesign;

    }
}