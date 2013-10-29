namespace AfyaPro_NextGen
{
    partial class frmCTCBook
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
            this.cboTreatmentPoint = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtGender = new DevExpress.XtraEditors.TextEdit();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtMonths = new DevExpress.XtraEditors.TextEdit();
            this.txtYears = new DevExpress.XtraEditors.TextEdit();
            this.cmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtLastVisitDate = new DevExpress.XtraEditors.DateEdit();
            this.txtPatientId = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbTreatmentPoint = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbPatientId = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbName = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbYears = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbMonths = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbGender = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.cboTreatmentPoint.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtGender.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonths.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYears.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastVisitDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastVisitDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPatientId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbTreatmentPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbPatientId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbYears)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbMonths)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbGender)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // cboTreatmentPoint
            // 
            this.cboTreatmentPoint.Location = new System.Drawing.Point(100, 176);
            this.cboTreatmentPoint.Name = "cboTreatmentPoint";
            this.cboTreatmentPoint.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTreatmentPoint.Properties.NullText = "";
            this.cboTreatmentPoint.Size = new System.Drawing.Size(221, 20);
            this.cboTreatmentPoint.StyleController = this.layoutControl1;
            this.cboTreatmentPoint.TabIndex = 1;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtGender);
            this.layoutControl1.Controls.Add(this.cmdClose);
            this.layoutControl1.Controls.Add(this.txtMonths);
            this.layoutControl1.Controls.Add(this.cboTreatmentPoint);
            this.layoutControl1.Controls.Add(this.txtYears);
            this.layoutControl1.Controls.Add(this.cmdOk);
            this.layoutControl1.Controls.Add(this.txtName);
            this.layoutControl1.Controls.Add(this.txtLastVisitDate);
            this.layoutControl1.Controls.Add(this.txtPatientId);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(333, 256);
            this.layoutControl1.TabIndex = 13;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtGender
            // 
            this.txtGender.Location = new System.Drawing.Point(112, 116);
            this.txtGender.Name = "txtGender";
            this.txtGender.Properties.ReadOnly = true;
            this.txtGender.Size = new System.Drawing.Size(197, 20);
            this.txtGender.StyleController = this.layoutControl1;
            this.txtGender.TabIndex = 44;
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(170, 200);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(151, 44);
            this.cmdClose.StyleController = this.layoutControl1;
            this.cmdClose.TabIndex = 5;
            this.cmdClose.Text = "Cancel";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // txtMonths
            // 
            this.txtMonths.Location = new System.Drawing.Point(259, 92);
            this.txtMonths.Name = "txtMonths";
            this.txtMonths.Properties.ReadOnly = true;
            this.txtMonths.Size = new System.Drawing.Size(50, 20);
            this.txtMonths.StyleController = this.layoutControl1;
            this.txtMonths.TabIndex = 43;
            // 
            // txtYears
            // 
            this.txtYears.Location = new System.Drawing.Point(112, 92);
            this.txtYears.Name = "txtYears";
            this.txtYears.Properties.ReadOnly = true;
            this.txtYears.Size = new System.Drawing.Size(55, 20);
            this.txtYears.StyleController = this.layoutControl1;
            this.txtYears.TabIndex = 42;
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(12, 200);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(154, 44);
            this.cmdOk.StyleController = this.layoutControl1;
            this.cmdOk.TabIndex = 4;
            this.cmdOk.Text = "Ok";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(112, 68);
            this.txtName.Name = "txtName";
            this.txtName.Properties.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(197, 20);
            this.txtName.StyleController = this.layoutControl1;
            this.txtName.TabIndex = 41;
            // 
            // txtLastVisitDate
            // 
            this.txtLastVisitDate.EditValue = null;
            this.txtLastVisitDate.Location = new System.Drawing.Point(112, 140);
            this.txtLastVisitDate.Name = "txtLastVisitDate";
            this.txtLastVisitDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtLastVisitDate.Properties.Mask.EditMask = "";
            this.txtLastVisitDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtLastVisitDate.Properties.ReadOnly = true;
            this.txtLastVisitDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtLastVisitDate.Size = new System.Drawing.Size(197, 20);
            this.txtLastVisitDate.StyleController = this.layoutControl1;
            this.txtLastVisitDate.TabIndex = 7;
            this.txtLastVisitDate.TabStop = false;
            // 
            // txtPatientId
            // 
            this.txtPatientId.Location = new System.Drawing.Point(112, 44);
            this.txtPatientId.Name = "txtPatientId";
            this.txtPatientId.Properties.ReadOnly = true;
            this.txtPatientId.Size = new System.Drawing.Size(197, 20);
            this.txtPatientId.StyleController = this.layoutControl1;
            this.txtPatientId.TabIndex = 40;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.txbTreatmentPoint,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(333, 256);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // txbTreatmentPoint
            // 
            this.txbTreatmentPoint.Control = this.cboTreatmentPoint;
            this.txbTreatmentPoint.CustomizationFormText = "txbTreatmentPoint";
            this.txbTreatmentPoint.Location = new System.Drawing.Point(0, 164);
            this.txbTreatmentPoint.Name = "txbTreatmentPoint";
            this.txbTreatmentPoint.Size = new System.Drawing.Size(313, 24);
            this.txbTreatmentPoint.Text = "Treatment Point";
            this.txbTreatmentPoint.TextSize = new System.Drawing.Size(84, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cmdOk;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 188);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(28, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(158, 48);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.cmdClose;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(158, 188);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(47, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(155, 48);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "Patient Info";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.txbPatientId,
            this.txbName,
            this.txbYears,
            this.txbMonths,
            this.txbGender,
            this.layoutControlItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(313, 164);
            this.layoutControlGroup2.Text = "Patient Info";
            // 
            // txbPatientId
            // 
            this.txbPatientId.Control = this.txtPatientId;
            this.txbPatientId.CustomizationFormText = "Patient #";
            this.txbPatientId.Location = new System.Drawing.Point(0, 0);
            this.txbPatientId.Name = "txbPatientId";
            this.txbPatientId.Size = new System.Drawing.Size(289, 24);
            this.txbPatientId.Text = "Patient #";
            this.txbPatientId.TextSize = new System.Drawing.Size(84, 13);
            // 
            // txbName
            // 
            this.txbName.Control = this.txtName;
            this.txbName.CustomizationFormText = "Name";
            this.txbName.Location = new System.Drawing.Point(0, 24);
            this.txbName.Name = "txbName";
            this.txbName.Size = new System.Drawing.Size(289, 24);
            this.txbName.Text = "Name";
            this.txbName.TextSize = new System.Drawing.Size(84, 13);
            // 
            // txbYears
            // 
            this.txbYears.Control = this.txtYears;
            this.txbYears.CustomizationFormText = "Age     Years";
            this.txbYears.Location = new System.Drawing.Point(0, 48);
            this.txbYears.Name = "txbYears";
            this.txbYears.Size = new System.Drawing.Size(147, 24);
            this.txbYears.Text = "Age     Years";
            this.txbYears.TextSize = new System.Drawing.Size(84, 13);
            // 
            // txbMonths
            // 
            this.txbMonths.Control = this.txtMonths;
            this.txbMonths.CustomizationFormText = "Months";
            this.txbMonths.Location = new System.Drawing.Point(147, 48);
            this.txbMonths.Name = "txbMonths";
            this.txbMonths.Size = new System.Drawing.Size(142, 24);
            this.txbMonths.Text = "Months";
            this.txbMonths.TextSize = new System.Drawing.Size(84, 13);
            // 
            // txbGender
            // 
            this.txbGender.Control = this.txtGender;
            this.txbGender.CustomizationFormText = "Gender";
            this.txbGender.Location = new System.Drawing.Point(0, 72);
            this.txbGender.Name = "txbGender";
            this.txbGender.Size = new System.Drawing.Size(289, 24);
            this.txbGender.Text = "Gender";
            this.txbGender.TextSize = new System.Drawing.Size(84, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtLastVisitDate;
            this.layoutControlItem2.CustomizationFormText = "Last visited Date";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(289, 24);
            this.layoutControlItem2.Text = "Date Last Booked";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(84, 13);
            // 
            // frmCTCBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 256);
            this.Controls.Add(this.layoutControl1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCTCBook";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Book";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCTCBook_FormClosing);
            this.Load += new System.EventHandler(this.frmCTCBook_Load);
            this.Shown += new System.EventHandler(this.frmCTCBook_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.cboTreatmentPoint.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtGender.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonths.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYears.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastVisitDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastVisitDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPatientId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbTreatmentPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbPatientId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbYears)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbMonths)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbGender)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit cboTreatmentPoint;
        private DevExpress.XtraEditors.SimpleButton cmdOk;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem txbTreatmentPoint;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.DateEdit txtLastVisitDate;
        private DevExpress.XtraEditors.TextEdit txtGender;
        private DevExpress.XtraEditors.TextEdit txtMonths;
        private DevExpress.XtraEditors.TextEdit txtYears;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtPatientId;
        private DevExpress.XtraLayout.LayoutControlItem txbPatientId;
        private DevExpress.XtraLayout.LayoutControlItem txbName;
        private DevExpress.XtraLayout.LayoutControlItem txbYears;
        private DevExpress.XtraLayout.LayoutControlItem txbMonths;
        private DevExpress.XtraLayout.LayoutControlItem txbGender;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
    }
}