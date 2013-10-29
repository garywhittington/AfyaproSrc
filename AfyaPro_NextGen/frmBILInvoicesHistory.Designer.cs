namespace AfyaPro_NextGen
{
    partial class frmBILInvoicesHistory
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
            this.grdBILInvoicesHistory = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cmdDesign = new DevExpress.XtraEditors.SimpleButton();
            this.cmdPreviewList = new DevExpress.XtraEditors.SimpleButton();
            this.chkClosed = new DevExpress.XtraEditors.CheckEdit();
            this.cmdPreview = new DevExpress.XtraEditors.SimpleButton();
            this.cmdRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.chkCancelled = new DevExpress.XtraEditors.CheckEdit();
            this.chkOpen = new DevExpress.XtraEditors.CheckEdit();
            this.txtDateTo = new DevExpress.XtraEditors.DateEdit();
            this.txtDateFrom = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.grpDate = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbDateFrom = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbDateTo = new DevExpress.XtraLayout.LayoutControlItem();
            this.grpInvoiceStatus = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbOpen = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbCancelled = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbClosed = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbRefresh = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbPreviewList = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbDesign = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmdVoid = new DevExpress.XtraEditors.SimpleButton();
            this.txbVoid = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.grdBILInvoicesHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkClosed.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCancelled.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOpen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbDateFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbDateTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpInvoiceStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbOpen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbCancelled)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbClosed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbPreviewList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbDesign)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbVoid)).BeginInit();
            this.SuspendLayout();
            // 
            // grdBILInvoicesHistory
            // 
            this.grdBILInvoicesHistory.Location = new System.Drawing.Point(12, 125);
            this.grdBILInvoicesHistory.MainView = this.gridView1;
            this.grdBILInvoicesHistory.Name = "grdBILInvoicesHistory";
            this.grdBILInvoicesHistory.Size = new System.Drawing.Size(636, 319);
            this.grdBILInvoicesHistory.TabIndex = 10;
            this.grdBILInvoicesHistory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.grdBILInvoicesHistory.Visible = false;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdBILInvoicesHistory;
            this.gridView1.Name = "gridView1";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cmdVoid);
            this.layoutControl1.Controls.Add(this.cmdDesign);
            this.layoutControl1.Controls.Add(this.cmdPreviewList);
            this.layoutControl1.Controls.Add(this.chkClosed);
            this.layoutControl1.Controls.Add(this.cmdPreview);
            this.layoutControl1.Controls.Add(this.cmdRefresh);
            this.layoutControl1.Controls.Add(this.chkCancelled);
            this.layoutControl1.Controls.Add(this.chkOpen);
            this.layoutControl1.Controls.Add(this.txtDateTo);
            this.layoutControl1.Controls.Add(this.txtDateFrom);
            this.layoutControl1.Controls.Add(this.grdBILInvoicesHistory);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(641, 473);
            this.layoutControl1.TabIndex = 11;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cmdDesign
            // 
            this.cmdDesign.Location = new System.Drawing.Point(478, 12);
            this.cmdDesign.Name = "cmdDesign";
            this.cmdDesign.Size = new System.Drawing.Size(78, 109);
            this.cmdDesign.StyleController = this.layoutControl1;
            this.cmdDesign.TabIndex = 20;
            this.cmdDesign.Text = "Design Invoice";
            this.cmdDesign.Click += new System.EventHandler(this.cmdDesign_Click);
            // 
            // cmdPreviewList
            // 
            this.cmdPreviewList.Location = new System.Drawing.Point(397, 12);
            this.cmdPreviewList.Name = "cmdPreviewList";
            this.cmdPreviewList.Size = new System.Drawing.Size(77, 109);
            this.cmdPreviewList.StyleController = this.layoutControl1;
            this.cmdPreviewList.TabIndex = 19;
            this.cmdPreviewList.Text = "Preview List";
            this.cmdPreviewList.Click += new System.EventHandler(this.cmdPreviewList_Click);
            // 
            // chkClosed
            // 
            this.chkClosed.Location = new System.Drawing.Point(130, 90);
            this.chkClosed.Name = "chkClosed";
            this.chkClosed.Properties.Caption = "Closed";
            this.chkClosed.Size = new System.Drawing.Size(69, 19);
            this.chkClosed.StyleController = this.layoutControl1;
            this.chkClosed.TabIndex = 18;
            // 
            // cmdPreview
            // 
            this.cmdPreview.Location = new System.Drawing.Point(306, 12);
            this.cmdPreview.Name = "cmdPreview";
            this.cmdPreview.Size = new System.Drawing.Size(87, 109);
            this.cmdPreview.StyleController = this.layoutControl1;
            this.cmdPreview.TabIndex = 17;
            this.cmdPreview.Text = "Preview Invoice";
            this.cmdPreview.Click += new System.EventHandler(this.cmdPreview_Click);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Location = new System.Drawing.Point(215, 12);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(87, 109);
            this.cmdRefresh.StyleController = this.layoutControl1;
            this.cmdRefresh.TabIndex = 16;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // chkCancelled
            // 
            this.chkCancelled.Location = new System.Drawing.Point(130, 67);
            this.chkCancelled.Name = "chkCancelled";
            this.chkCancelled.Properties.Caption = "Cancelled";
            this.chkCancelled.Size = new System.Drawing.Size(69, 19);
            this.chkCancelled.StyleController = this.layoutControl1;
            this.chkCancelled.TabIndex = 13;
            // 
            // chkOpen
            // 
            this.chkOpen.Location = new System.Drawing.Point(130, 44);
            this.chkOpen.Name = "chkOpen";
            this.chkOpen.Properties.Caption = "Open";
            this.chkOpen.Size = new System.Drawing.Size(69, 19);
            this.chkOpen.StyleController = this.layoutControl1;
            this.chkOpen.TabIndex = 12;
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
            this.txtDateTo.Size = new System.Drawing.Size(50, 20);
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
            this.txtDateFrom.Size = new System.Drawing.Size(50, 20);
            this.txtDateFrom.StyleController = this.layoutControl1;
            this.txtDateFrom.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.txbGrid,
            this.emptySpaceItem1,
            this.grpDate,
            this.grpInvoiceStatus,
            this.txbRefresh,
            this.layoutControlItem1,
            this.txbPreviewList,
            this.txbDesign,
            this.txbVoid});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(660, 456);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // txbGrid
            // 
            this.txbGrid.Control = this.grdBILInvoicesHistory;
            this.txbGrid.CustomizationFormText = "txbGrid";
            this.txbGrid.Location = new System.Drawing.Point(0, 113);
            this.txbGrid.Name = "txbGrid";
            this.txbGrid.Size = new System.Drawing.Size(640, 323);
            this.txbGrid.Text = "txbGrid";
            this.txbGrid.TextLocation = DevExpress.Utils.Locations.Top;
            this.txbGrid.TextSize = new System.Drawing.Size(0, 0);
            this.txbGrid.TextToControlDistance = 0;
            this.txbGrid.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(630, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(10, 113);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // grpDate
            // 
            this.grpDate.CustomizationFormText = "Date";
            this.grpDate.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.txbDateFrom,
            this.txbDateTo});
            this.grpDate.Location = new System.Drawing.Point(0, 0);
            this.grpDate.Name = "grpDate";
            this.grpDate.Size = new System.Drawing.Size(106, 113);
            this.grpDate.Text = "Date";
            // 
            // txbDateFrom
            // 
            this.txbDateFrom.Control = this.txtDateFrom;
            this.txbDateFrom.CustomizationFormText = "From";
            this.txbDateFrom.Location = new System.Drawing.Point(0, 0);
            this.txbDateFrom.Name = "txbDateFrom";
            this.txbDateFrom.Size = new System.Drawing.Size(82, 24);
            this.txbDateFrom.Text = "From";
            this.txbDateFrom.TextSize = new System.Drawing.Size(24, 13);
            // 
            // txbDateTo
            // 
            this.txbDateTo.Control = this.txtDateTo;
            this.txbDateTo.CustomizationFormText = "To";
            this.txbDateTo.Location = new System.Drawing.Point(0, 24);
            this.txbDateTo.Name = "txbDateTo";
            this.txbDateTo.Size = new System.Drawing.Size(82, 45);
            this.txbDateTo.Text = "To";
            this.txbDateTo.TextSize = new System.Drawing.Size(24, 13);
            // 
            // grpInvoiceStatus
            // 
            this.grpInvoiceStatus.CustomizationFormText = "Departments";
            this.grpInvoiceStatus.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.txbOpen,
            this.txbCancelled,
            this.txbClosed});
            this.grpInvoiceStatus.Location = new System.Drawing.Point(106, 0);
            this.grpInvoiceStatus.Name = "grpInvoiceStatus";
            this.grpInvoiceStatus.Size = new System.Drawing.Size(97, 113);
            this.grpInvoiceStatus.Text = "Invoice Status";
            // 
            // txbOpen
            // 
            this.txbOpen.Control = this.chkOpen;
            this.txbOpen.CustomizationFormText = "txbOPD";
            this.txbOpen.Location = new System.Drawing.Point(0, 0);
            this.txbOpen.Name = "txbOpen";
            this.txbOpen.Size = new System.Drawing.Size(73, 23);
            this.txbOpen.Text = "txbOpen";
            this.txbOpen.TextSize = new System.Drawing.Size(0, 0);
            this.txbOpen.TextToControlDistance = 0;
            this.txbOpen.TextVisible = false;
            // 
            // txbCancelled
            // 
            this.txbCancelled.Control = this.chkCancelled;
            this.txbCancelled.CustomizationFormText = "txbIPD";
            this.txbCancelled.Location = new System.Drawing.Point(0, 23);
            this.txbCancelled.Name = "txbCancelled";
            this.txbCancelled.Size = new System.Drawing.Size(73, 23);
            this.txbCancelled.Text = "txbCancelled";
            this.txbCancelled.TextSize = new System.Drawing.Size(0, 0);
            this.txbCancelled.TextToControlDistance = 0;
            this.txbCancelled.TextVisible = false;
            // 
            // txbClosed
            // 
            this.txbClosed.Control = this.chkClosed;
            this.txbClosed.CustomizationFormText = "txbClosed";
            this.txbClosed.Location = new System.Drawing.Point(0, 46);
            this.txbClosed.Name = "txbClosed";
            this.txbClosed.Size = new System.Drawing.Size(73, 23);
            this.txbClosed.Text = "txbClosed";
            this.txbClosed.TextSize = new System.Drawing.Size(0, 0);
            this.txbClosed.TextToControlDistance = 0;
            this.txbClosed.TextVisible = false;
            // 
            // txbRefresh
            // 
            this.txbRefresh.Control = this.cmdRefresh;
            this.txbRefresh.CustomizationFormText = "Refresh";
            this.txbRefresh.Location = new System.Drawing.Point(203, 0);
            this.txbRefresh.MinSize = new System.Drawing.Size(91, 33);
            this.txbRefresh.Name = "txbRefresh";
            this.txbRefresh.Size = new System.Drawing.Size(91, 113);
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
            this.layoutControlItem1.Location = new System.Drawing.Point(294, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(91, 33);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(91, 113);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // txbPreviewList
            // 
            this.txbPreviewList.Control = this.cmdPreviewList;
            this.txbPreviewList.CustomizationFormText = "txbPreviewList";
            this.txbPreviewList.Location = new System.Drawing.Point(385, 0);
            this.txbPreviewList.MinSize = new System.Drawing.Size(81, 33);
            this.txbPreviewList.Name = "txbPreviewList";
            this.txbPreviewList.Size = new System.Drawing.Size(81, 113);
            this.txbPreviewList.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbPreviewList.Text = "txbPreviewList";
            this.txbPreviewList.TextSize = new System.Drawing.Size(0, 0);
            this.txbPreviewList.TextToControlDistance = 0;
            this.txbPreviewList.TextVisible = false;
            // 
            // txbDesign
            // 
            this.txbDesign.Control = this.cmdDesign;
            this.txbDesign.CustomizationFormText = "txbDesign";
            this.txbDesign.Location = new System.Drawing.Point(466, 0);
            this.txbDesign.MinSize = new System.Drawing.Size(82, 26);
            this.txbDesign.Name = "txbDesign";
            this.txbDesign.Size = new System.Drawing.Size(82, 113);
            this.txbDesign.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbDesign.Text = "txbDesign";
            this.txbDesign.TextSize = new System.Drawing.Size(0, 0);
            this.txbDesign.TextToControlDistance = 0;
            this.txbDesign.TextVisible = false;
            // 
            // cmdVoid
            // 
            this.cmdVoid.Location = new System.Drawing.Point(560, 12);
            this.cmdVoid.Name = "cmdVoid";
            this.cmdVoid.Size = new System.Drawing.Size(78, 109);
            this.cmdVoid.StyleController = this.layoutControl1;
            this.cmdVoid.TabIndex = 21;
            this.cmdVoid.Text = "Void";
            this.cmdVoid.Click += new System.EventHandler(this.cmdVoid_Click);
            // 
            // txbVoid
            // 
            this.txbVoid.Control = this.cmdVoid;
            this.txbVoid.CustomizationFormText = "txbVoid";
            this.txbVoid.Location = new System.Drawing.Point(548, 0);
            this.txbVoid.MinSize = new System.Drawing.Size(82, 26);
            this.txbVoid.Name = "txbVoid";
            this.txbVoid.Size = new System.Drawing.Size(82, 113);
            this.txbVoid.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbVoid.Text = "txbVoid";
            this.txbVoid.TextSize = new System.Drawing.Size(0, 0);
            this.txbVoid.TextToControlDistance = 0;
            this.txbVoid.TextVisible = false;
            // 
            // frmBILInvoicesHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 473);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmBILInvoicesHistory";
            this.Text = "Invoices History";
            this.Load += new System.EventHandler(this.frmBILInvoicesHistory_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBILInvoicesHistory_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.grdBILInvoicesHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkClosed.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCancelled.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOpen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbDateFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbDateTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpInvoiceStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbOpen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbCancelled)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbClosed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbPreviewList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbDesign)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbVoid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraGrid.GridControl grdBILInvoicesHistory;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem txbGrid;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.DateEdit txtDateTo;
        private DevExpress.XtraEditors.DateEdit txtDateFrom;
        private DevExpress.XtraLayout.LayoutControlGroup grpDate;
        private DevExpress.XtraLayout.LayoutControlItem txbDateFrom;
        private DevExpress.XtraLayout.LayoutControlItem txbDateTo;
        private DevExpress.XtraEditors.CheckEdit chkOpen;
        private DevExpress.XtraLayout.LayoutControlItem txbOpen;
        private DevExpress.XtraEditors.CheckEdit chkCancelled;
        private DevExpress.XtraLayout.LayoutControlGroup grpInvoiceStatus;
        private DevExpress.XtraLayout.LayoutControlItem txbCancelled;
        private DevExpress.XtraEditors.SimpleButton cmdRefresh;
        private DevExpress.XtraLayout.LayoutControlItem txbRefresh;
        private DevExpress.XtraEditors.SimpleButton cmdPreview;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.CheckEdit chkClosed;
        private DevExpress.XtraLayout.LayoutControlItem txbClosed;
        private DevExpress.XtraEditors.SimpleButton cmdPreviewList;
        private DevExpress.XtraLayout.LayoutControlItem txbPreviewList;
        private DevExpress.XtraEditors.SimpleButton cmdDesign;
        private DevExpress.XtraLayout.LayoutControlItem txbDesign;
        private DevExpress.XtraEditors.SimpleButton cmdVoid;
        private DevExpress.XtraLayout.LayoutControlItem txbVoid;

    }
}