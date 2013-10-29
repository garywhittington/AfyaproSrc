namespace AfyaPro_NextGen
{
    partial class frmBILDebtReliefRequestsView
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
            this.grdBILDebtReliefRequestsView = new DevExpress.XtraGrid.GridControl();
            this.viewBILDebtReliefRequestsView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cmdDetails = new DevExpress.XtraEditors.SimpleButton();
            this.cmdPreviewList = new DevExpress.XtraEditors.SimpleButton();
            this.cmdRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.txtDateTo = new DevExpress.XtraEditors.DateEdit();
            this.txtDateFrom = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.grpDate = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbDateFrom = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbDateTo = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbRefresh = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbPreviewList = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbDetails = new DevExpress.XtraLayout.LayoutControlItem();
            this.radStatus = new DevExpress.XtraEditors.RadioGroup();
            this.txbStatus = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            ((System.ComponentModel.ISupportInitialize)(this.grdBILDebtReliefRequestsView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewBILDebtReliefRequestsView)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.txbPreviewList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            this.SuspendLayout();
            // 
            // grdBILDebtReliefRequestsView
            // 
            this.grdBILDebtReliefRequestsView.Location = new System.Drawing.Point(12, 160);
            this.grdBILDebtReliefRequestsView.MainView = this.viewBILDebtReliefRequestsView;
            this.grdBILDebtReliefRequestsView.Name = "grdBILDebtReliefRequestsView";
            this.grdBILDebtReliefRequestsView.Size = new System.Drawing.Size(617, 301);
            this.grdBILDebtReliefRequestsView.TabIndex = 10;
            this.grdBILDebtReliefRequestsView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewBILDebtReliefRequestsView});
            this.grdBILDebtReliefRequestsView.Visible = false;
            // 
            // viewBILDebtReliefRequestsView
            // 
            this.viewBILDebtReliefRequestsView.GridControl = this.grdBILDebtReliefRequestsView;
            this.viewBILDebtReliefRequestsView.Name = "viewBILDebtReliefRequestsView";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.radStatus);
            this.layoutControl1.Controls.Add(this.cmdDetails);
            this.layoutControl1.Controls.Add(this.cmdPreviewList);
            this.layoutControl1.Controls.Add(this.cmdRefresh);
            this.layoutControl1.Controls.Add(this.txtDateTo);
            this.layoutControl1.Controls.Add(this.txtDateFrom);
            this.layoutControl1.Controls.Add(this.grdBILDebtReliefRequestsView);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(641, 473);
            this.layoutControl1.TabIndex = 11;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cmdDetails
            // 
            this.cmdDetails.Location = new System.Drawing.Point(460, 12);
            this.cmdDetails.Name = "cmdDetails";
            this.cmdDetails.Size = new System.Drawing.Size(78, 144);
            this.cmdDetails.StyleController = this.layoutControl1;
            this.cmdDetails.TabIndex = 20;
            this.cmdDetails.Text = "&Details";
            this.cmdDetails.Click += new System.EventHandler(this.cmdDetails_Click);
            // 
            // cmdPreviewList
            // 
            this.cmdPreviewList.Location = new System.Drawing.Point(542, 12);
            this.cmdPreviewList.Name = "cmdPreviewList";
            this.cmdPreviewList.Size = new System.Drawing.Size(87, 144);
            this.cmdPreviewList.StyleController = this.layoutControl1;
            this.cmdPreviewList.TabIndex = 19;
            this.cmdPreviewList.Text = "Preview List";
            this.cmdPreviewList.Click += new System.EventHandler(this.cmdPreviewList_Click);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Location = new System.Drawing.Point(369, 12);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(87, 144);
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
            this.txtDateTo.Size = new System.Drawing.Size(119, 20);
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
            this.txtDateFrom.Size = new System.Drawing.Size(119, 20);
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
            this.txbPreviewList,
            this.txbDetails,
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(641, 473);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // txbGrid
            // 
            this.txbGrid.Control = this.grdBILDebtReliefRequestsView;
            this.txbGrid.CustomizationFormText = "txbGrid";
            this.txbGrid.Location = new System.Drawing.Point(0, 148);
            this.txbGrid.Name = "txbGrid";
            this.txbGrid.Size = new System.Drawing.Size(621, 305);
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
            this.txbDateTo,
            this.emptySpaceItem1});
            this.grpDate.Location = new System.Drawing.Point(0, 0);
            this.grpDate.Name = "grpDate";
            this.grpDate.Size = new System.Drawing.Size(175, 148);
            this.grpDate.Text = "Date";
            // 
            // txbDateFrom
            // 
            this.txbDateFrom.Control = this.txtDateFrom;
            this.txbDateFrom.CustomizationFormText = "From";
            this.txbDateFrom.Location = new System.Drawing.Point(0, 0);
            this.txbDateFrom.Name = "txbDateFrom";
            this.txbDateFrom.Size = new System.Drawing.Size(151, 24);
            this.txbDateFrom.Text = "From";
            this.txbDateFrom.TextSize = new System.Drawing.Size(24, 13);
            // 
            // txbDateTo
            // 
            this.txbDateTo.Control = this.txtDateTo;
            this.txbDateTo.CustomizationFormText = "To";
            this.txbDateTo.Location = new System.Drawing.Point(0, 24);
            this.txbDateTo.Name = "txbDateTo";
            this.txbDateTo.Size = new System.Drawing.Size(151, 24);
            this.txbDateTo.Text = "To";
            this.txbDateTo.TextSize = new System.Drawing.Size(24, 13);
            // 
            // txbRefresh
            // 
            this.txbRefresh.Control = this.cmdRefresh;
            this.txbRefresh.CustomizationFormText = "Refresh";
            this.txbRefresh.Location = new System.Drawing.Point(357, 0);
            this.txbRefresh.MinSize = new System.Drawing.Size(91, 33);
            this.txbRefresh.Name = "txbRefresh";
            this.txbRefresh.Size = new System.Drawing.Size(91, 148);
            this.txbRefresh.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbRefresh.Text = "Refresh";
            this.txbRefresh.TextSize = new System.Drawing.Size(0, 0);
            this.txbRefresh.TextToControlDistance = 0;
            this.txbRefresh.TextVisible = false;
            // 
            // txbPreviewList
            // 
            this.txbPreviewList.Control = this.cmdPreviewList;
            this.txbPreviewList.CustomizationFormText = "txbPreviewList";
            this.txbPreviewList.Location = new System.Drawing.Point(530, 0);
            this.txbPreviewList.MinSize = new System.Drawing.Size(91, 33);
            this.txbPreviewList.Name = "txbPreviewList";
            this.txbPreviewList.Size = new System.Drawing.Size(91, 148);
            this.txbPreviewList.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbPreviewList.Text = "txbPreviewList";
            this.txbPreviewList.TextSize = new System.Drawing.Size(0, 0);
            this.txbPreviewList.TextToControlDistance = 0;
            this.txbPreviewList.TextVisible = false;
            // 
            // txbDetails
            // 
            this.txbDetails.Control = this.cmdDetails;
            this.txbDetails.CustomizationFormText = "txbApprove";
            this.txbDetails.Location = new System.Drawing.Point(448, 0);
            this.txbDetails.MinSize = new System.Drawing.Size(82, 26);
            this.txbDetails.Name = "txbDetails";
            this.txbDetails.Size = new System.Drawing.Size(82, 148);
            this.txbDetails.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbDetails.Text = "txbDetails";
            this.txbDetails.TextSize = new System.Drawing.Size(0, 0);
            this.txbDetails.TextToControlDistance = 0;
            this.txbDetails.TextVisible = false;
            // 
            // radStatus
            // 
            this.radStatus.Location = new System.Drawing.Point(199, 44);
            this.radStatus.Name = "radStatus";
            this.radStatus.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "All"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Open"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Approved"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Rejected")});
            this.radStatus.Size = new System.Drawing.Size(154, 100);
            this.radStatus.StyleController = this.layoutControl1;
            this.radStatus.TabIndex = 21;
            // 
            // txbStatus
            // 
            this.txbStatus.Control = this.radStatus;
            this.txbStatus.CustomizationFormText = "txbStatus";
            this.txbStatus.Location = new System.Drawing.Point(0, 0);
            this.txbStatus.Name = "txbStatus";
            this.txbStatus.Size = new System.Drawing.Size(158, 104);
            this.txbStatus.Text = "txbStatus";
            this.txbStatus.TextSize = new System.Drawing.Size(0, 0);
            this.txbStatus.TextToControlDistance = 0;
            this.txbStatus.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 48);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(151, 56);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "REQUEST STATUS";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.txbStatus});
            this.layoutControlGroup2.Location = new System.Drawing.Point(175, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(182, 148);
            this.layoutControlGroup2.Text = "REQUEST STATUS";
            // 
            // frmBILDebtReliefRequestsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 473);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmBILDebtReliefRequestsView";
            this.Text = "Debt Relief Requests View";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBILDebtReliefRequestsView_FormClosing);
            this.Load += new System.EventHandler(this.frmBILDebtReliefRequestsView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdBILDebtReliefRequestsView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewBILDebtReliefRequestsView)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.txbPreviewList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraGrid.GridControl grdBILDebtReliefRequestsView;
        private DevExpress.XtraGrid.Views.Grid.GridView viewBILDebtReliefRequestsView;
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
        private DevExpress.XtraEditors.SimpleButton cmdPreviewList;
        private DevExpress.XtraLayout.LayoutControlItem txbPreviewList;
        private DevExpress.XtraEditors.SimpleButton cmdDetails;
        private DevExpress.XtraLayout.LayoutControlItem txbDetails;
        private DevExpress.XtraEditors.RadioGroup radStatus;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem txbStatus;

    }
}