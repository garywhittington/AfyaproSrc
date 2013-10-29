namespace AfyaPro_NextGen
{
    partial class frmOPDPatientListing
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
            this.grdOPDPatientListing = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cmdPreview = new DevExpress.XtraEditors.SimpleButton();
            this.cmdRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.chkReAttendance = new DevExpress.XtraEditors.CheckEdit();
            this.chkNew = new DevExpress.XtraEditors.CheckEdit();
            this.chkIPD = new DevExpress.XtraEditors.CheckEdit();
            this.chkOPD = new DevExpress.XtraEditors.CheckEdit();
            this.txtDateTo = new DevExpress.XtraEditors.DateEdit();
            this.txtDateFrom = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.grpDate = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbDateFrom = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbDateTo = new DevExpress.XtraLayout.LayoutControlItem();
            this.grpDepartments = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbOPD = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbIPD = new DevExpress.XtraLayout.LayoutControlItem();
            this.grpRegisterType = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbNew = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbReAttendance = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbRefresh = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.grdOPDPatientListing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkReAttendance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkNew.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIPD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOPD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbDateFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbDateTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDepartments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbOPD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbIPD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpRegisterType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbReAttendance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // grdOPDPatientListing
            // 
            this.grdOPDPatientListing.Location = new System.Drawing.Point(12, 104);
            this.grdOPDPatientListing.MainView = this.gridView1;
            this.grdOPDPatientListing.Name = "grdOPDPatientListing";
            this.grdOPDPatientListing.Size = new System.Drawing.Size(617, 357);
            this.grdOPDPatientListing.TabIndex = 10;
            this.grdOPDPatientListing.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.grdOPDPatientListing.Visible = false;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdOPDPatientListing;
            this.gridView1.Name = "gridView1";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cmdPreview);
            this.layoutControl1.Controls.Add(this.cmdRefresh);
            this.layoutControl1.Controls.Add(this.chkReAttendance);
            this.layoutControl1.Controls.Add(this.chkNew);
            this.layoutControl1.Controls.Add(this.chkIPD);
            this.layoutControl1.Controls.Add(this.chkOPD);
            this.layoutControl1.Controls.Add(this.txtDateTo);
            this.layoutControl1.Controls.Add(this.txtDateFrom);
            this.layoutControl1.Controls.Add(this.grdOPDPatientListing);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(641, 473);
            this.layoutControl1.TabIndex = 11;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cmdPreview
            // 
            this.cmdPreview.Location = new System.Drawing.Point(542, 12);
            this.cmdPreview.Name = "cmdPreview";
            this.cmdPreview.Size = new System.Drawing.Size(87, 88);
            this.cmdPreview.StyleController = this.layoutControl1;
            this.cmdPreview.TabIndex = 17;
            this.cmdPreview.Text = "Print Preview";
            this.cmdPreview.Click += new System.EventHandler(this.cmdPreview_Click);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Location = new System.Drawing.Point(440, 12);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(98, 88);
            this.cmdRefresh.StyleController = this.layoutControl1;
            this.cmdRefresh.TabIndex = 16;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // chkReAttendance
            // 
            this.chkReAttendance.Location = new System.Drawing.Point(326, 67);
            this.chkReAttendance.Name = "chkReAttendance";
            this.chkReAttendance.Properties.Caption = "ReAttendance";
            this.chkReAttendance.Size = new System.Drawing.Size(98, 19);
            this.chkReAttendance.StyleController = this.layoutControl1;
            this.chkReAttendance.TabIndex = 15;
            // 
            // chkNew
            // 
            this.chkNew.Location = new System.Drawing.Point(326, 44);
            this.chkNew.Name = "chkNew";
            this.chkNew.Properties.Caption = "New";
            this.chkNew.Size = new System.Drawing.Size(98, 19);
            this.chkNew.StyleController = this.layoutControl1;
            this.chkNew.TabIndex = 14;
            // 
            // chkIPD
            // 
            this.chkIPD.Location = new System.Drawing.Point(187, 67);
            this.chkIPD.Name = "chkIPD";
            this.chkIPD.Properties.Caption = "In Patient";
            this.chkIPD.Size = new System.Drawing.Size(111, 19);
            this.chkIPD.StyleController = this.layoutControl1;
            this.chkIPD.TabIndex = 13;
            // 
            // chkOPD
            // 
            this.chkOPD.Location = new System.Drawing.Point(187, 44);
            this.chkOPD.Name = "chkOPD";
            this.chkOPD.Properties.Caption = "Out Patient";
            this.chkOPD.Size = new System.Drawing.Size(111, 19);
            this.chkOPD.StyleController = this.layoutControl1;
            this.chkOPD.TabIndex = 12;
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
            this.txtDateTo.Size = new System.Drawing.Size(107, 20);
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
            this.txtDateFrom.Size = new System.Drawing.Size(107, 20);
            this.txtDateFrom.StyleController = this.layoutControl1;
            this.txtDateFrom.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.txbGrid,
            this.grpDate,
            this.grpDepartments,
            this.grpRegisterType,
            this.txbRefresh,
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(641, 473);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // txbGrid
            // 
            this.txbGrid.Control = this.grdOPDPatientListing;
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
            this.grpDate.Size = new System.Drawing.Size(163, 92);
            this.grpDate.Text = "Date";
            // 
            // txbDateFrom
            // 
            this.txbDateFrom.Control = this.txtDateFrom;
            this.txbDateFrom.CustomizationFormText = "From";
            this.txbDateFrom.Location = new System.Drawing.Point(0, 0);
            this.txbDateFrom.Name = "txbDateFrom";
            this.txbDateFrom.Size = new System.Drawing.Size(139, 24);
            this.txbDateFrom.Text = "From";
            this.txbDateFrom.TextSize = new System.Drawing.Size(24, 13);
            // 
            // txbDateTo
            // 
            this.txbDateTo.Control = this.txtDateTo;
            this.txbDateTo.CustomizationFormText = "To";
            this.txbDateTo.Location = new System.Drawing.Point(0, 24);
            this.txbDateTo.Name = "txbDateTo";
            this.txbDateTo.Size = new System.Drawing.Size(139, 24);
            this.txbDateTo.Text = "To";
            this.txbDateTo.TextSize = new System.Drawing.Size(24, 13);
            // 
            // grpDepartments
            // 
            this.grpDepartments.CustomizationFormText = "Departments";
            this.grpDepartments.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.txbOPD,
            this.txbIPD});
            this.grpDepartments.Location = new System.Drawing.Point(163, 0);
            this.grpDepartments.Name = "grpDepartments";
            this.grpDepartments.Size = new System.Drawing.Size(139, 92);
            this.grpDepartments.Text = "Departments";
            // 
            // txbOPD
            // 
            this.txbOPD.Control = this.chkOPD;
            this.txbOPD.CustomizationFormText = "txbOPD";
            this.txbOPD.Location = new System.Drawing.Point(0, 0);
            this.txbOPD.Name = "txbOPD";
            this.txbOPD.Size = new System.Drawing.Size(115, 23);
            this.txbOPD.Text = "txbOPD";
            this.txbOPD.TextSize = new System.Drawing.Size(0, 0);
            this.txbOPD.TextToControlDistance = 0;
            this.txbOPD.TextVisible = false;
            // 
            // txbIPD
            // 
            this.txbIPD.Control = this.chkIPD;
            this.txbIPD.CustomizationFormText = "txbIPD";
            this.txbIPD.Location = new System.Drawing.Point(0, 23);
            this.txbIPD.Name = "txbIPD";
            this.txbIPD.Size = new System.Drawing.Size(115, 25);
            this.txbIPD.Text = "txbIPD";
            this.txbIPD.TextSize = new System.Drawing.Size(0, 0);
            this.txbIPD.TextToControlDistance = 0;
            this.txbIPD.TextVisible = false;
            // 
            // grpRegisterType
            // 
            this.grpRegisterType.CustomizationFormText = "Register Type";
            this.grpRegisterType.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.txbNew,
            this.txbReAttendance});
            this.grpRegisterType.Location = new System.Drawing.Point(302, 0);
            this.grpRegisterType.Name = "grpRegisterType";
            this.grpRegisterType.Size = new System.Drawing.Size(126, 92);
            this.grpRegisterType.Text = "Register Type";
            // 
            // txbNew
            // 
            this.txbNew.Control = this.chkNew;
            this.txbNew.CustomizationFormText = "New";
            this.txbNew.Location = new System.Drawing.Point(0, 0);
            this.txbNew.Name = "txbNew";
            this.txbNew.Size = new System.Drawing.Size(102, 23);
            this.txbNew.Text = "New";
            this.txbNew.TextSize = new System.Drawing.Size(0, 0);
            this.txbNew.TextToControlDistance = 0;
            this.txbNew.TextVisible = false;
            // 
            // txbReAttendance
            // 
            this.txbReAttendance.Control = this.chkReAttendance;
            this.txbReAttendance.CustomizationFormText = "ReAttendance";
            this.txbReAttendance.Location = new System.Drawing.Point(0, 23);
            this.txbReAttendance.Name = "txbReAttendance";
            this.txbReAttendance.Size = new System.Drawing.Size(102, 25);
            this.txbReAttendance.Text = "ReAttendance";
            this.txbReAttendance.TextSize = new System.Drawing.Size(0, 0);
            this.txbReAttendance.TextToControlDistance = 0;
            this.txbReAttendance.TextVisible = false;
            // 
            // txbRefresh
            // 
            this.txbRefresh.Control = this.cmdRefresh;
            this.txbRefresh.CustomizationFormText = "Refresh";
            this.txbRefresh.Location = new System.Drawing.Point(428, 0);
            this.txbRefresh.MinSize = new System.Drawing.Size(91, 33);
            this.txbRefresh.Name = "txbRefresh";
            this.txbRefresh.Size = new System.Drawing.Size(102, 92);
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
            this.layoutControlItem1.Location = new System.Drawing.Point(530, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(91, 33);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(91, 92);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmOPDPatientListing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 473);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmOPDPatientListing";
            this.Text = "Patients Listing";
            this.Load += new System.EventHandler(this.frmOPDPatientListing_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOPDPatientListing_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.grdOPDPatientListing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkReAttendance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkNew.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIPD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOPD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbDateFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbDateTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDepartments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbOPD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbIPD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpRegisterType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbReAttendance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraGrid.GridControl grdOPDPatientListing;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem txbGrid;
        private DevExpress.XtraEditors.DateEdit txtDateTo;
        private DevExpress.XtraEditors.DateEdit txtDateFrom;
        private DevExpress.XtraLayout.LayoutControlGroup grpDate;
        private DevExpress.XtraLayout.LayoutControlItem txbDateFrom;
        private DevExpress.XtraLayout.LayoutControlItem txbDateTo;
        private DevExpress.XtraEditors.CheckEdit chkOPD;
        private DevExpress.XtraLayout.LayoutControlItem txbOPD;
        private DevExpress.XtraEditors.CheckEdit chkIPD;
        private DevExpress.XtraLayout.LayoutControlGroup grpDepartments;
        private DevExpress.XtraLayout.LayoutControlItem txbIPD;
        private DevExpress.XtraEditors.CheckEdit chkReAttendance;
        private DevExpress.XtraEditors.CheckEdit chkNew;
        private DevExpress.XtraLayout.LayoutControlGroup grpRegisterType;
        private DevExpress.XtraLayout.LayoutControlItem txbNew;
        private DevExpress.XtraLayout.LayoutControlItem txbReAttendance;
        private DevExpress.XtraEditors.SimpleButton cmdRefresh;
        private DevExpress.XtraLayout.LayoutControlItem txbRefresh;
        private DevExpress.XtraEditors.SimpleButton cmdPreview;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;

    }
}