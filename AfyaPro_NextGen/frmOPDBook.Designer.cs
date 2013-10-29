namespace AfyaPro_NextGen
{
    partial class frmOPDBook
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
            this.chkCharge = new DevExpress.XtraEditors.CheckEdit();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtTemperature = new DevExpress.XtraEditors.TextEdit();
            this.txtWeight = new DevExpress.XtraEditors.TextEdit();
            this.cmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.txtLastVisitDate = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbTreatmentPoint = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbWeight = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbTemperature = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.cboTreatmentPoint.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkCharge.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTemperature.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastVisitDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastVisitDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbTreatmentPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbTemperature)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // cboTreatmentPoint
            // 
            this.cboTreatmentPoint.Location = new System.Drawing.Point(96, 36);
            this.cboTreatmentPoint.Name = "cboTreatmentPoint";
            this.cboTreatmentPoint.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTreatmentPoint.Properties.NullText = "";
            this.cboTreatmentPoint.Size = new System.Drawing.Size(225, 20);
            this.cboTreatmentPoint.StyleController = this.layoutControl1;
            this.cboTreatmentPoint.TabIndex = 1;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chkCharge);
            this.layoutControl1.Controls.Add(this.cmdClose);
            this.layoutControl1.Controls.Add(this.txtTemperature);
            this.layoutControl1.Controls.Add(this.txtWeight);
            this.layoutControl1.Controls.Add(this.cboTreatmentPoint);
            this.layoutControl1.Controls.Add(this.cmdOk);
            this.layoutControl1.Controls.Add(this.txtLastVisitDate);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(333, 197);
            this.layoutControl1.TabIndex = 13;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chkCharge
            // 
            this.chkCharge.Location = new System.Drawing.Point(12, 108);
            this.chkCharge.Name = "chkCharge";
            this.chkCharge.Properties.Caption = "Charge";
            this.chkCharge.Size = new System.Drawing.Size(309, 19);
            this.chkCharge.StyleController = this.layoutControl1;
            this.chkCharge.TabIndex = 16;
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(171, 131);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(150, 54);
            this.cmdClose.StyleController = this.layoutControl1;
            this.cmdClose.TabIndex = 5;
            this.cmdClose.Text = "Cancel";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // txtTemperature
            // 
            this.txtTemperature.Location = new System.Drawing.Point(96, 84);
            this.txtTemperature.Name = "txtTemperature";
            this.txtTemperature.Size = new System.Drawing.Size(225, 20);
            this.txtTemperature.StyleController = this.layoutControl1;
            this.txtTemperature.TabIndex = 15;
            // 
            // txtWeight
            // 
            this.txtWeight.Location = new System.Drawing.Point(96, 60);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(225, 20);
            this.txtWeight.StyleController = this.layoutControl1;
            this.txtWeight.TabIndex = 14;
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(12, 131);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(159, 54);
            this.cmdOk.StyleController = this.layoutControl1;
            this.cmdOk.TabIndex = 4;
            this.cmdOk.Text = "Ok";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // txtLastVisitDate
            // 
            this.txtLastVisitDate.EditValue = null;
            this.txtLastVisitDate.Location = new System.Drawing.Point(96, 12);
            this.txtLastVisitDate.Name = "txtLastVisitDate";
            this.txtLastVisitDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtLastVisitDate.Properties.Mask.EditMask = "";
            this.txtLastVisitDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtLastVisitDate.Properties.ReadOnly = true;
            this.txtLastVisitDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtLastVisitDate.Size = new System.Drawing.Size(225, 20);
            this.txtLastVisitDate.StyleController = this.layoutControl1;
            this.txtLastVisitDate.TabIndex = 7;
            this.txtLastVisitDate.TabStop = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.txbTreatmentPoint,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem2,
            this.txbWeight,
            this.txbTemperature,
            this.layoutControlItem6});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(333, 197);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // txbTreatmentPoint
            // 
            this.txbTreatmentPoint.Control = this.cboTreatmentPoint;
            this.txbTreatmentPoint.CustomizationFormText = "txbTreatmentPoint";
            this.txbTreatmentPoint.Location = new System.Drawing.Point(0, 24);
            this.txbTreatmentPoint.Name = "txbTreatmentPoint";
            this.txbTreatmentPoint.Size = new System.Drawing.Size(313, 24);
            this.txbTreatmentPoint.Text = "Treatment Point";
            this.txbTreatmentPoint.TextSize = new System.Drawing.Size(80, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cmdOk;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 119);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(37, 33);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 2);
            this.layoutControlItem4.Size = new System.Drawing.Size(161, 58);
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
            this.layoutControlItem5.Location = new System.Drawing.Point(161, 119);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(56, 33);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 2, 2, 2);
            this.layoutControlItem5.Size = new System.Drawing.Size(152, 58);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtLastVisitDate;
            this.layoutControlItem2.CustomizationFormText = "Last visited Date";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(313, 24);
            this.layoutControlItem2.Text = "Last visited Date";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(80, 13);
            // 
            // txbWeight
            // 
            this.txbWeight.Control = this.txtWeight;
            this.txbWeight.CustomizationFormText = "Weight";
            this.txbWeight.Location = new System.Drawing.Point(0, 48);
            this.txbWeight.Name = "txbWeight";
            this.txbWeight.Size = new System.Drawing.Size(313, 24);
            this.txbWeight.Text = "Weight";
            this.txbWeight.TextSize = new System.Drawing.Size(80, 13);
            // 
            // txbTemperature
            // 
            this.txbTemperature.Control = this.txtTemperature;
            this.txbTemperature.CustomizationFormText = "Temperature";
            this.txbTemperature.Location = new System.Drawing.Point(0, 72);
            this.txbTemperature.Name = "txbTemperature";
            this.txbTemperature.Size = new System.Drawing.Size(313, 24);
            this.txbTemperature.Text = "Temperature";
            this.txbTemperature.TextSize = new System.Drawing.Size(80, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.chkCharge;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(313, 23);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // frmOPDBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 197);
            this.Controls.Add(this.layoutControl1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOPDBook";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Book";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOPDBook_FormClosing);
            this.Load += new System.EventHandler(this.frmOPDBook_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboTreatmentPoint.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkCharge.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTemperature.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastVisitDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastVisitDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbTreatmentPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbTemperature)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
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
        private DevExpress.XtraEditors.CheckEdit chkCharge;
        private DevExpress.XtraEditors.TextEdit txtTemperature;
        private DevExpress.XtraEditors.TextEdit txtWeight;
        private DevExpress.XtraLayout.LayoutControlItem txbWeight;
        private DevExpress.XtraLayout.LayoutControlItem txbTemperature;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}