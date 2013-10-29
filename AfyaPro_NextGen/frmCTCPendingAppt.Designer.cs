namespace AfyaPro_NextGen
{
    partial class frmCTCPendingAppt
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
            this.txtEndDate = new DevExpress.XtraEditors.DateEdit();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtStartDate = new DevExpress.XtraEditors.DateEdit();
            this.cmdRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.grdCTCPendingAppt = new DevExpress.XtraGrid.GridControl();
            this.viewCTCPendingAppt = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbQueueTreatmentPoint = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbRefresh = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbClose = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbStartDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbEndDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.chkAll = new DevExpress.XtraEditors.CheckEdit();
            this.txbAll = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCTCPendingAppt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewCTCPendingAppt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbQueueTreatmentPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbStartDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbEndDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbAll)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chkAll);
            this.layoutControl1.Controls.Add(this.txtEndDate);
            this.layoutControl1.Controls.Add(this.cmdClose);
            this.layoutControl1.Controls.Add(this.txtStartDate);
            this.layoutControl1.Controls.Add(this.cmdRefresh);
            this.layoutControl1.Controls.Add(this.grdCTCPendingAppt);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(421, 454);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtEndDate
            // 
            this.txtEndDate.EditValue = null;
            this.txtEndDate.Location = new System.Drawing.Point(245, 44);
            this.txtEndDate.Name = "txtEndDate";
            this.txtEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtEndDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtEndDate.Size = new System.Drawing.Size(152, 20);
            this.txtEndDate.StyleController = this.layoutControl1;
            this.txtEndDate.TabIndex = 13;
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(212, 408);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(197, 34);
            this.cmdClose.StyleController = this.layoutControl1;
            this.cmdClose.TabIndex = 6;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // txtStartDate
            // 
            this.txtStartDate.EditValue = null;
            this.txtStartDate.Location = new System.Drawing.Point(57, 44);
            this.txtStartDate.Name = "txtStartDate";
            this.txtStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtStartDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtStartDate.Size = new System.Drawing.Size(151, 20);
            this.txtStartDate.StyleController = this.layoutControl1;
            this.txtStartDate.TabIndex = 12;
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Location = new System.Drawing.Point(12, 408);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(196, 34);
            this.cmdRefresh.StyleController = this.layoutControl1;
            this.cmdRefresh.TabIndex = 5;
            this.cmdRefresh.Text = "Refresh Appointments";
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // grdCTCPendingAppt
            // 
            this.grdCTCPendingAppt.Location = new System.Drawing.Point(12, 103);
            this.grdCTCPendingAppt.MainView = this.viewCTCPendingAppt;
            this.grdCTCPendingAppt.Name = "grdCTCPendingAppt";
            this.grdCTCPendingAppt.Size = new System.Drawing.Size(397, 301);
            this.grdCTCPendingAppt.TabIndex = 4;
            this.grdCTCPendingAppt.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewCTCPendingAppt});
            this.grdCTCPendingAppt.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.grdQueueTreatmentPoint_ProcessGridKey);
            this.grdCTCPendingAppt.DoubleClick += new System.EventHandler(this.grdQueueTreatmentPoint_DoubleClick);
            // 
            // viewCTCPendingAppt
            // 
            this.viewCTCPendingAppt.GridControl = this.grdCTCPendingAppt;
            this.viewCTCPendingAppt.Name = "viewCTCPendingAppt";
            this.viewCTCPendingAppt.OptionsBehavior.Editable = false;
            this.viewCTCPendingAppt.OptionsBehavior.ReadOnly = true;
            this.viewCTCPendingAppt.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.txbQueueTreatmentPoint,
            this.txbRefresh,
            this.txbClose,
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(421, 454);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // txbQueueTreatmentPoint
            // 
            this.txbQueueTreatmentPoint.Control = this.grdCTCPendingAppt;
            this.txbQueueTreatmentPoint.CustomizationFormText = "txbQueueTreatmentPoint";
            this.txbQueueTreatmentPoint.Location = new System.Drawing.Point(0, 91);
            this.txbQueueTreatmentPoint.Name = "txbQueueTreatmentPoint";
            this.txbQueueTreatmentPoint.Size = new System.Drawing.Size(401, 305);
            this.txbQueueTreatmentPoint.Text = "txbQueueTreatmentPoint";
            this.txbQueueTreatmentPoint.TextSize = new System.Drawing.Size(0, 0);
            this.txbQueueTreatmentPoint.TextToControlDistance = 0;
            this.txbQueueTreatmentPoint.TextVisible = false;
            // 
            // txbRefresh
            // 
            this.txbRefresh.Control = this.cmdRefresh;
            this.txbRefresh.CustomizationFormText = "txbRefresh";
            this.txbRefresh.Location = new System.Drawing.Point(0, 396);
            this.txbRefresh.MinSize = new System.Drawing.Size(88, 26);
            this.txbRefresh.Name = "txbRefresh";
            this.txbRefresh.Size = new System.Drawing.Size(200, 38);
            this.txbRefresh.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbRefresh.Text = "txbRefresh";
            this.txbRefresh.TextSize = new System.Drawing.Size(0, 0);
            this.txbRefresh.TextToControlDistance = 0;
            this.txbRefresh.TextVisible = false;
            // 
            // txbClose
            // 
            this.txbClose.Control = this.cmdClose;
            this.txbClose.CustomizationFormText = "txbClose";
            this.txbClose.Location = new System.Drawing.Point(200, 396);
            this.txbClose.MinSize = new System.Drawing.Size(41, 26);
            this.txbClose.Name = "txbClose";
            this.txbClose.Size = new System.Drawing.Size(201, 38);
            this.txbClose.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbClose.Text = "txbClose";
            this.txbClose.TextSize = new System.Drawing.Size(0, 0);
            this.txbClose.TextToControlDistance = 0;
            this.txbClose.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "DATE RANGE";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.txbStartDate,
            this.txbEndDate,
            this.txbAll});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(401, 91);
            this.layoutControlGroup2.Text = "DATE RANGE";
            // 
            // txbStartDate
            // 
            this.txbStartDate.Control = this.txtStartDate;
            this.txbStartDate.CustomizationFormText = "FROM";
            this.txbStartDate.Location = new System.Drawing.Point(0, 0);
            this.txbStartDate.Name = "txbStartDate";
            this.txbStartDate.Size = new System.Drawing.Size(188, 24);
            this.txbStartDate.Text = "FROM";
            this.txbStartDate.TextSize = new System.Drawing.Size(29, 13);
            // 
            // txbEndDate
            // 
            this.txbEndDate.Control = this.txtEndDate;
            this.txbEndDate.CustomizationFormText = "TO";
            this.txbEndDate.Location = new System.Drawing.Point(188, 0);
            this.txbEndDate.Name = "txbEndDate";
            this.txbEndDate.Size = new System.Drawing.Size(189, 24);
            this.txbEndDate.Text = "TO";
            this.txbEndDate.TextSize = new System.Drawing.Size(29, 13);
            // 
            // chkAll
            // 
            this.chkAll.EditValue = true;
            this.chkAll.Location = new System.Drawing.Point(24, 68);
            this.chkAll.Name = "chkAll";
            this.chkAll.Properties.Caption = "View only unbooked appointments";
            this.chkAll.Size = new System.Drawing.Size(373, 19);
            this.chkAll.StyleController = this.layoutControl1;
            this.chkAll.TabIndex = 17;
            // 
            // txbAll
            // 
            this.txbAll.Control = this.chkAll;
            this.txbAll.CustomizationFormText = "txbAll";
            this.txbAll.Location = new System.Drawing.Point(0, 24);
            this.txbAll.Name = "txbAll";
            this.txbAll.Size = new System.Drawing.Size(377, 23);
            this.txbAll.Text = "txbAll";
            this.txbAll.TextSize = new System.Drawing.Size(0, 0);
            this.txbAll.TextToControlDistance = 0;
            this.txbAll.TextVisible = false;
            // 
            // frmCTCPendingAppt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 454);
            this.Controls.Add(this.layoutControl1);
            this.KeyPreview = true;
            this.Name = "frmCTCPendingAppt";
            this.Text = "Client Appointments";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCTCPendingAppt_FormClosing);
            this.Load += new System.EventHandler(this.frmCTCPendingAppt_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCTCPendingAppt_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtEndDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCTCPendingAppt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewCTCPendingAppt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbQueueTreatmentPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbStartDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbEndDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbAll)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl grdCTCPendingAppt;
        private DevExpress.XtraGrid.Views.Grid.GridView viewCTCPendingAppt;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem txbQueueTreatmentPoint;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
        private DevExpress.XtraEditors.SimpleButton cmdRefresh;
        private DevExpress.XtraLayout.LayoutControlItem txbRefresh;
        private DevExpress.XtraLayout.LayoutControlItem txbClose;
        private DevExpress.XtraEditors.DateEdit txtEndDate;
        private DevExpress.XtraEditors.DateEdit txtStartDate;
        private DevExpress.XtraLayout.LayoutControlItem txbStartDate;
        private DevExpress.XtraLayout.LayoutControlItem txbEndDate;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraEditors.CheckEdit chkAll;
        private DevExpress.XtraLayout.LayoutControlItem txbAll;
    }
}