namespace AfyaPro_NextGen
{
    partial class frmIVTransferOutsView
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
            this.components = new System.ComponentModel.Container();
            this.grdIVTransferOutsView = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cmdDesignOrder = new DevExpress.XtraEditors.SimpleButton();
            this.cboStore = new DevExpress.XtraEditors.LookUpEdit();
            this.cmdIssue = new DevExpress.XtraEditors.SimpleButton();
            this.cmdEdit = new DevExpress.XtraEditors.SimpleButton();
            this.radTransferStatus = new DevExpress.XtraEditors.RadioGroup();
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
            this.txbIssue = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbStore = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbDesignOrder = new DevExpress.XtraLayout.LayoutControlItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdIVTransferOutsView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStore.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTransferStatus.Properties)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.txbIssue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbStore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbDesignOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // grdIVTransferOutsView
            // 
            this.grdIVTransferOutsView.Location = new System.Drawing.Point(12, 128);
            this.grdIVTransferOutsView.MainView = this.gridView1;
            this.grdIVTransferOutsView.Name = "grdIVTransferOutsView";
            this.grdIVTransferOutsView.Size = new System.Drawing.Size(785, 333);
            this.grdIVTransferOutsView.TabIndex = 10;
            this.grdIVTransferOutsView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.grdIVTransferOutsView.Visible = false;
            this.grdIVTransferOutsView.DoubleClick += new System.EventHandler(this.grdIVTransferOutsView_DoubleClick);
            this.grdIVTransferOutsView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdIVTransferOutsView_KeyDown);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdIVTransferOutsView;
            this.gridView1.Name = "gridView1";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cmdDesignOrder);
            this.layoutControl1.Controls.Add(this.cboStore);
            this.layoutControl1.Controls.Add(this.cmdIssue);
            this.layoutControl1.Controls.Add(this.cmdEdit);
            this.layoutControl1.Controls.Add(this.radTransferStatus);
            this.layoutControl1.Controls.Add(this.cmdPreview);
            this.layoutControl1.Controls.Add(this.cmdRefresh);
            this.layoutControl1.Controls.Add(this.txtDateTo);
            this.layoutControl1.Controls.Add(this.txtDateFrom);
            this.layoutControl1.Controls.Add(this.grdIVTransferOutsView);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(809, 473);
            this.layoutControl1.TabIndex = 11;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cmdDesignOrder
            // 
            this.cmdDesignOrder.Location = new System.Drawing.Point(593, 12);
            this.cmdDesignOrder.Name = "cmdDesignOrder";
            this.cmdDesignOrder.Size = new System.Drawing.Size(95, 112);
            this.cmdDesignOrder.StyleController = this.layoutControl1;
            this.cmdDesignOrder.TabIndex = 22;
            this.cmdDesignOrder.Text = "Design";
            this.cmdDesignOrder.Click += new System.EventHandler(this.cmdDesignOrder_Click);
            // 
            // cboStore
            // 
            this.cboStore.Location = new System.Drawing.Point(12, 12);
            this.cboStore.Name = "cboStore";
            this.cboStore.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStore.Properties.NullText = "";
            this.cboStore.Size = new System.Drawing.Size(154, 20);
            this.cboStore.StyleController = this.layoutControl1;
            this.cboStore.TabIndex = 21;
            this.cboStore.EditValueChanged += new System.EventHandler(this.cboStore_EditValueChanged);
            // 
            // cmdIssue
            // 
            this.cmdIssue.Location = new System.Drawing.Point(490, 12);
            this.cmdIssue.Name = "cmdIssue";
            this.cmdIssue.Size = new System.Drawing.Size(99, 112);
            this.cmdIssue.StyleController = this.layoutControl1;
            this.cmdIssue.TabIndex = 20;
            this.cmdIssue.Text = "Issue";
            this.cmdIssue.Click += new System.EventHandler(this.cmdIssue_Click);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(389, 12);
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Size = new System.Drawing.Size(97, 112);
            this.cmdEdit.StyleController = this.layoutControl1;
            this.cmdEdit.TabIndex = 19;
            this.cmdEdit.Text = "Edit";
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // radTransferStatus
            // 
            this.radTransferStatus.Location = new System.Drawing.Point(170, 12);
            this.radTransferStatus.Name = "radTransferStatus";
            this.radTransferStatus.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Show All"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Show Open"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Show Closed")});
            this.radTransferStatus.Size = new System.Drawing.Size(117, 112);
            this.radTransferStatus.StyleController = this.layoutControl1;
            this.radTransferStatus.TabIndex = 18;
            // 
            // cmdPreview
            // 
            this.cmdPreview.Location = new System.Drawing.Point(692, 12);
            this.cmdPreview.Name = "cmdPreview";
            this.cmdPreview.Size = new System.Drawing.Size(105, 112);
            this.cmdPreview.StyleController = this.layoutControl1;
            this.cmdPreview.TabIndex = 17;
            this.cmdPreview.Text = "Print Preview";
            this.cmdPreview.Click += new System.EventHandler(this.cmdPreview_Click);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Location = new System.Drawing.Point(291, 12);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(94, 112);
            this.cmdRefresh.StyleController = this.layoutControl1;
            this.cmdRefresh.TabIndex = 16;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // txtDateTo
            // 
            this.txtDateTo.EditValue = null;
            this.txtDateTo.Location = new System.Drawing.Point(52, 92);
            this.txtDateTo.Name = "txtDateTo";
            this.txtDateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDateTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDateTo.Size = new System.Drawing.Size(102, 20);
            this.txtDateTo.StyleController = this.layoutControl1;
            this.txtDateTo.TabIndex = 11;
            // 
            // txtDateFrom
            // 
            this.txtDateFrom.EditValue = null;
            this.txtDateFrom.Location = new System.Drawing.Point(52, 68);
            this.txtDateFrom.Name = "txtDateFrom";
            this.txtDateFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDateFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDateFrom.Size = new System.Drawing.Size(102, 20);
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
            this.txbIssue,
            this.txbStore,
            this.txbDesignOrder});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(809, 473);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // txbGrid
            // 
            this.txbGrid.Control = this.grdIVTransferOutsView;
            this.txbGrid.CustomizationFormText = "txbGrid";
            this.txbGrid.Location = new System.Drawing.Point(0, 116);
            this.txbGrid.Name = "txbGrid";
            this.txbGrid.Size = new System.Drawing.Size(789, 337);
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
            this.grpDate.Location = new System.Drawing.Point(0, 24);
            this.grpDate.Name = "grpDate";
            this.grpDate.Size = new System.Drawing.Size(158, 92);
            this.grpDate.Text = "Date";
            // 
            // txbDateFrom
            // 
            this.txbDateFrom.Control = this.txtDateFrom;
            this.txbDateFrom.CustomizationFormText = "From";
            this.txbDateFrom.Location = new System.Drawing.Point(0, 0);
            this.txbDateFrom.Name = "txbDateFrom";
            this.txbDateFrom.Size = new System.Drawing.Size(134, 24);
            this.txbDateFrom.Text = "From";
            this.txbDateFrom.TextSize = new System.Drawing.Size(24, 13);
            // 
            // txbDateTo
            // 
            this.txbDateTo.Control = this.txtDateTo;
            this.txbDateTo.CustomizationFormText = "To";
            this.txbDateTo.Location = new System.Drawing.Point(0, 24);
            this.txbDateTo.Name = "txbDateTo";
            this.txbDateTo.Size = new System.Drawing.Size(134, 24);
            this.txbDateTo.Text = "To";
            this.txbDateTo.TextSize = new System.Drawing.Size(24, 13);
            // 
            // txbRefresh
            // 
            this.txbRefresh.Control = this.cmdRefresh;
            this.txbRefresh.CustomizationFormText = "Refresh";
            this.txbRefresh.Location = new System.Drawing.Point(279, 0);
            this.txbRefresh.MinSize = new System.Drawing.Size(91, 33);
            this.txbRefresh.Name = "txbRefresh";
            this.txbRefresh.Size = new System.Drawing.Size(98, 116);
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
            this.layoutControlItem1.Location = new System.Drawing.Point(680, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(91, 33);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(109, 116);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // txbOrderStatus
            // 
            this.txbOrderStatus.Control = this.radTransferStatus;
            this.txbOrderStatus.CustomizationFormText = "txbOrderStatus";
            this.txbOrderStatus.Location = new System.Drawing.Point(158, 0);
            this.txbOrderStatus.Name = "txbOrderStatus";
            this.txbOrderStatus.Size = new System.Drawing.Size(121, 116);
            this.txbOrderStatus.Text = "txbOrderStatus";
            this.txbOrderStatus.TextSize = new System.Drawing.Size(0, 0);
            this.txbOrderStatus.TextToControlDistance = 0;
            this.txbOrderStatus.TextVisible = false;
            // 
            // txbEdit
            // 
            this.txbEdit.Control = this.cmdEdit;
            this.txbEdit.CustomizationFormText = "txbEdit";
            this.txbEdit.Location = new System.Drawing.Point(377, 0);
            this.txbEdit.MinSize = new System.Drawing.Size(82, 26);
            this.txbEdit.Name = "txbEdit";
            this.txbEdit.Size = new System.Drawing.Size(101, 116);
            this.txbEdit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbEdit.Text = "txbEdit";
            this.txbEdit.TextSize = new System.Drawing.Size(0, 0);
            this.txbEdit.TextToControlDistance = 0;
            this.txbEdit.TextVisible = false;
            // 
            // txbIssue
            // 
            this.txbIssue.Control = this.cmdIssue;
            this.txbIssue.CustomizationFormText = "txbReceive";
            this.txbIssue.Location = new System.Drawing.Point(478, 0);
            this.txbIssue.MinSize = new System.Drawing.Size(82, 26);
            this.txbIssue.Name = "txbIssue";
            this.txbIssue.Size = new System.Drawing.Size(103, 116);
            this.txbIssue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbIssue.Text = "txbIssue";
            this.txbIssue.TextSize = new System.Drawing.Size(0, 0);
            this.txbIssue.TextToControlDistance = 0;
            this.txbIssue.TextVisible = false;
            // 
            // txbStore
            // 
            this.txbStore.Control = this.cboStore;
            this.txbStore.CustomizationFormText = "txbStore";
            this.txbStore.Location = new System.Drawing.Point(0, 0);
            this.txbStore.Name = "txbStore";
            this.txbStore.Size = new System.Drawing.Size(158, 24);
            this.txbStore.Text = "txbStore";
            this.txbStore.TextSize = new System.Drawing.Size(0, 0);
            this.txbStore.TextToControlDistance = 0;
            this.txbStore.TextVisible = false;
            // 
            // txbDesignOrder
            // 
            this.txbDesignOrder.Control = this.cmdDesignOrder;
            this.txbDesignOrder.CustomizationFormText = "txbDesignOrder";
            this.txbDesignOrder.Location = new System.Drawing.Point(581, 0);
            this.txbDesignOrder.MinSize = new System.Drawing.Size(82, 26);
            this.txbDesignOrder.Name = "txbDesignOrder";
            this.txbDesignOrder.Size = new System.Drawing.Size(99, 116);
            this.txbDesignOrder.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbDesignOrder.Text = "txbDesignOrder";
            this.txbDesignOrder.TextSize = new System.Drawing.Size(0, 0);
            this.txbDesignOrder.TextToControlDistance = 0;
            this.txbDesignOrder.TextVisible = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmIVTransferOutsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 473);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmIVTransferOutsView";
            this.Text = "Transfer Inventory Out";
            this.Load += new System.EventHandler(this.frmIVTransferOutsView_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmIVTransferOutsView_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.grdIVTransferOutsView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboStore.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTransferStatus.Properties)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.txbIssue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbStore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbDesignOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraGrid.GridControl grdIVTransferOutsView;
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
        private DevExpress.XtraEditors.RadioGroup radTransferStatus;
        private DevExpress.XtraLayout.LayoutControlItem txbOrderStatus;
        private DevExpress.XtraEditors.SimpleButton cmdIssue;
        private DevExpress.XtraEditors.SimpleButton cmdEdit;
        private DevExpress.XtraLayout.LayoutControlItem txbEdit;
        private DevExpress.XtraLayout.LayoutControlItem txbIssue;
        private DevExpress.XtraLayout.LayoutControlItem txbStore;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.SimpleButton cmdDesignOrder;
        private DevExpress.XtraLayout.LayoutControlItem txbDesignOrder;
        internal DevExpress.XtraEditors.LookUpEdit cboStore;

    }
}