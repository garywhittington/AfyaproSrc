namespace AfyaPro_NextGen
{
    partial class frmBILSalesHistory
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
            this.grdBILSalesHistory = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cmdPreviewList = new DevExpress.XtraEditors.SimpleButton();
            this.cmdPreviewInvoice = new DevExpress.XtraEditors.SimpleButton();
            this.cmdPreviewReceipt = new DevExpress.XtraEditors.SimpleButton();
            this.cmdRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.txtDateTo = new DevExpress.XtraEditors.DateEdit();
            this.txtDateFrom = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.grpDate = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbDateFrom = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbDateTo = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbRefresh = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbPreviewReceipt = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbPreviewInvoice = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbPreviewList = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.grdBILSalesHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.txbPreviewReceipt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbPreviewInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbPreviewList)).BeginInit();
            this.SuspendLayout();
            // 
            // grdBILSalesHistory
            // 
            this.grdBILSalesHistory.Location = new System.Drawing.Point(12, 104);
            this.grdBILSalesHistory.MainView = this.gridView1;
            this.grdBILSalesHistory.Name = "grdBILSalesHistory";
            this.grdBILSalesHistory.Size = new System.Drawing.Size(617, 357);
            this.grdBILSalesHistory.TabIndex = 10;
            this.grdBILSalesHistory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.grdBILSalesHistory.Visible = false;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdBILSalesHistory;
            this.gridView1.Name = "gridView1";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cmdPreviewList);
            this.layoutControl1.Controls.Add(this.cmdPreviewInvoice);
            this.layoutControl1.Controls.Add(this.cmdPreviewReceipt);
            this.layoutControl1.Controls.Add(this.cmdRefresh);
            this.layoutControl1.Controls.Add(this.txtDateTo);
            this.layoutControl1.Controls.Add(this.txtDateFrom);
            this.layoutControl1.Controls.Add(this.grdBILSalesHistory);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(641, 473);
            this.layoutControl1.TabIndex = 11;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cmdPreviewList
            // 
            this.cmdPreviewList.Location = new System.Drawing.Point(519, 12);
            this.cmdPreviewList.Name = "cmdPreviewList";
            this.cmdPreviewList.Size = new System.Drawing.Size(110, 88);
            this.cmdPreviewList.StyleController = this.layoutControl1;
            this.cmdPreviewList.TabIndex = 19;
            this.cmdPreviewList.Text = "Preview List";
            this.cmdPreviewList.Click += new System.EventHandler(this.cmdPreviewList_Click);
            // 
            // cmdPreviewInvoice
            // 
            this.cmdPreviewInvoice.Location = new System.Drawing.Point(410, 12);
            this.cmdPreviewInvoice.Name = "cmdPreviewInvoice";
            this.cmdPreviewInvoice.Size = new System.Drawing.Size(105, 88);
            this.cmdPreviewInvoice.StyleController = this.layoutControl1;
            this.cmdPreviewInvoice.TabIndex = 18;
            this.cmdPreviewInvoice.Text = "Preview Invoice";
            this.cmdPreviewInvoice.Click += new System.EventHandler(this.cmdPreviewInvoice_Click);
            // 
            // cmdPreviewReceipt
            // 
            this.cmdPreviewReceipt.Location = new System.Drawing.Point(295, 12);
            this.cmdPreviewReceipt.Name = "cmdPreviewReceipt";
            this.cmdPreviewReceipt.Size = new System.Drawing.Size(111, 88);
            this.cmdPreviewReceipt.StyleController = this.layoutControl1;
            this.cmdPreviewReceipt.TabIndex = 17;
            this.cmdPreviewReceipt.Text = "Preview Receipt";
            this.cmdPreviewReceipt.Click += new System.EventHandler(this.cmdPreviewReceipt_Click);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Location = new System.Drawing.Point(178, 12);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(113, 88);
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
            this.txtDateTo.Size = new System.Drawing.Size(110, 20);
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
            this.txtDateFrom.Size = new System.Drawing.Size(110, 20);
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
            this.txbPreviewReceipt,
            this.txbPreviewInvoice,
            this.txbPreviewList});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(641, 473);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // txbGrid
            // 
            this.txbGrid.Control = this.grdBILSalesHistory;
            this.txbGrid.CustomizationFormText = "txbGrid";
            this.txbGrid.Location = new System.Drawing.Point(0, 92);
            this.txbGrid.Name = "txbGrid";
            this.txbGrid.Size = new System.Drawing.Size(621, 361);
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
            this.grpDate.Size = new System.Drawing.Size(166, 92);
            this.grpDate.Text = "Date";
            // 
            // txbDateFrom
            // 
            this.txbDateFrom.Control = this.txtDateFrom;
            this.txbDateFrom.CustomizationFormText = "From";
            this.txbDateFrom.Location = new System.Drawing.Point(0, 0);
            this.txbDateFrom.Name = "txbDateFrom";
            this.txbDateFrom.Size = new System.Drawing.Size(142, 24);
            this.txbDateFrom.Text = "From";
            this.txbDateFrom.TextSize = new System.Drawing.Size(24, 13);
            // 
            // txbDateTo
            // 
            this.txbDateTo.Control = this.txtDateTo;
            this.txbDateTo.CustomizationFormText = "To";
            this.txbDateTo.Location = new System.Drawing.Point(0, 24);
            this.txbDateTo.Name = "txbDateTo";
            this.txbDateTo.Size = new System.Drawing.Size(142, 24);
            this.txbDateTo.Text = "To";
            this.txbDateTo.TextSize = new System.Drawing.Size(24, 13);
            // 
            // txbRefresh
            // 
            this.txbRefresh.Control = this.cmdRefresh;
            this.txbRefresh.CustomizationFormText = "Refresh";
            this.txbRefresh.Location = new System.Drawing.Point(166, 0);
            this.txbRefresh.MinSize = new System.Drawing.Size(91, 33);
            this.txbRefresh.Name = "txbRefresh";
            this.txbRefresh.Size = new System.Drawing.Size(117, 92);
            this.txbRefresh.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbRefresh.Text = "Refresh";
            this.txbRefresh.TextSize = new System.Drawing.Size(0, 0);
            this.txbRefresh.TextToControlDistance = 0;
            this.txbRefresh.TextVisible = false;
            // 
            // txbPreviewReceipt
            // 
            this.txbPreviewReceipt.Control = this.cmdPreviewReceipt;
            this.txbPreviewReceipt.CustomizationFormText = "layoutControlItem1";
            this.txbPreviewReceipt.Location = new System.Drawing.Point(283, 0);
            this.txbPreviewReceipt.MinSize = new System.Drawing.Size(91, 33);
            this.txbPreviewReceipt.Name = "txbPreviewReceipt";
            this.txbPreviewReceipt.Size = new System.Drawing.Size(115, 92);
            this.txbPreviewReceipt.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbPreviewReceipt.Text = "txbPreviewReceipt";
            this.txbPreviewReceipt.TextSize = new System.Drawing.Size(0, 0);
            this.txbPreviewReceipt.TextToControlDistance = 0;
            this.txbPreviewReceipt.TextVisible = false;
            // 
            // txbPreviewInvoice
            // 
            this.txbPreviewInvoice.Control = this.cmdPreviewInvoice;
            this.txbPreviewInvoice.CustomizationFormText = "txbPreviewInvoice";
            this.txbPreviewInvoice.Location = new System.Drawing.Point(398, 0);
            this.txbPreviewInvoice.MinSize = new System.Drawing.Size(91, 33);
            this.txbPreviewInvoice.Name = "txbPreviewInvoice";
            this.txbPreviewInvoice.Size = new System.Drawing.Size(109, 92);
            this.txbPreviewInvoice.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbPreviewInvoice.Text = "txbPreviewInvoice";
            this.txbPreviewInvoice.TextSize = new System.Drawing.Size(0, 0);
            this.txbPreviewInvoice.TextToControlDistance = 0;
            this.txbPreviewInvoice.TextVisible = false;
            // 
            // txbPreviewList
            // 
            this.txbPreviewList.Control = this.cmdPreviewList;
            this.txbPreviewList.CustomizationFormText = "txbPreviewList";
            this.txbPreviewList.Location = new System.Drawing.Point(507, 0);
            this.txbPreviewList.MinSize = new System.Drawing.Size(91, 33);
            this.txbPreviewList.Name = "txbPreviewList";
            this.txbPreviewList.Size = new System.Drawing.Size(114, 92);
            this.txbPreviewList.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbPreviewList.Text = "txbPreviewList";
            this.txbPreviewList.TextSize = new System.Drawing.Size(0, 0);
            this.txbPreviewList.TextToControlDistance = 0;
            this.txbPreviewList.TextVisible = false;
            // 
            // frmBILSalesHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 473);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmBILSalesHistory";
            this.Text = "Sales History";
            this.Load += new System.EventHandler(this.frmBILSalesHistory_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBILSalesHistory_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.grdBILSalesHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)(this.txbPreviewReceipt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbPreviewInvoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbPreviewList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraGrid.GridControl grdBILSalesHistory;
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
        private DevExpress.XtraEditors.SimpleButton cmdPreviewReceipt;
        private DevExpress.XtraLayout.LayoutControlItem txbPreviewReceipt;
        private DevExpress.XtraEditors.SimpleButton cmdPreviewInvoice;
        private DevExpress.XtraLayout.LayoutControlItem txbPreviewInvoice;
        private DevExpress.XtraEditors.SimpleButton cmdPreviewList;
        private DevExpress.XtraLayout.LayoutControlItem txbPreviewList;

    }
}