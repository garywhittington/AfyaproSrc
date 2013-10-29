namespace AfyaPro_NextGen
{
    partial class frmLABPatientHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLABPatientHistory));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cmdApplyFilter = new DevExpress.XtraEditors.SimpleButton();
            this.txtDateTo = new DevExpress.XtraEditors.DateEdit();
            this.txtDateFrom = new DevExpress.XtraEditors.DateEdit();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtRemarked = new DevExpress.XtraEditors.TextEdit();
            this.txtEquipmentRangeColor = new DevExpress.XtraEditors.TextEdit();
            this.txtNormalRangeColor = new DevExpress.XtraEditors.TextEdit();
            this.grdLABPatientHistory = new DevExpress.XtraGrid.GridControl();
            this.viewLABPatientHistory = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbRemarked = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemarked.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEquipmentRangeColor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNormalRangeColor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLABPatientHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewLABPatientHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbRemarked)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cmdApplyFilter);
            this.layoutControl1.Controls.Add(this.txtDateTo);
            this.layoutControl1.Controls.Add(this.txtDateFrom);
            this.layoutControl1.Controls.Add(this.cmdClose);
            this.layoutControl1.Controls.Add(this.txtRemarked);
            this.layoutControl1.Controls.Add(this.txtEquipmentRangeColor);
            this.layoutControl1.Controls.Add(this.txtNormalRangeColor);
            this.layoutControl1.Controls.Add(this.grdLABPatientHistory);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(880, 628);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cmdApplyFilter
            // 
            this.cmdApplyFilter.Location = new System.Drawing.Point(452, 582);
            this.cmdApplyFilter.Name = "cmdApplyFilter";
            this.cmdApplyFilter.Size = new System.Drawing.Size(215, 22);
            this.cmdApplyFilter.StyleController = this.layoutControl1;
            this.cmdApplyFilter.TabIndex = 11;
            this.cmdApplyFilter.Text = "Apply Date Filter";
            this.cmdApplyFilter.Click += new System.EventHandler(this.cmdApplyFilter_Click);
            // 
            // txtDateTo
            // 
            this.txtDateTo.EditValue = null;
            this.txtDateTo.Location = new System.Drawing.Point(507, 558);
            this.txtDateTo.Name = "txtDateTo";
            this.txtDateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDateTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDateTo.Size = new System.Drawing.Size(160, 20);
            this.txtDateTo.StyleController = this.layoutControl1;
            this.txtDateTo.TabIndex = 10;
            // 
            // txtDateFrom
            // 
            this.txtDateFrom.EditValue = null;
            this.txtDateFrom.Location = new System.Drawing.Point(507, 534);
            this.txtDateFrom.Name = "txtDateFrom";
            this.txtDateFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDateFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDateFrom.Size = new System.Drawing.Size(160, 20);
            this.txtDateFrom.StyleController = this.layoutControl1;
            this.txtDateFrom.TabIndex = 9;
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(683, 522);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(185, 94);
            this.cmdClose.StyleController = this.layoutControl1;
            this.cmdClose.TabIndex = 8;
            this.cmdClose.Text = "&Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // txtRemarked
            // 
            this.txtRemarked.Location = new System.Drawing.Point(12, 570);
            this.txtRemarked.Name = "txtRemarked";
            this.txtRemarked.Properties.Appearance.BackColor = System.Drawing.Color.Green;
            this.txtRemarked.Properties.Appearance.Options.UseBackColor = true;
            this.txtRemarked.Properties.ReadOnly = true;
            this.txtRemarked.Size = new System.Drawing.Size(94, 20);
            this.txtRemarked.StyleController = this.layoutControl1;
            this.txtRemarked.TabIndex = 7;
            this.txtRemarked.TabStop = false;
            // 
            // txtEquipmentRangeColor
            // 
            this.txtEquipmentRangeColor.Location = new System.Drawing.Point(12, 546);
            this.txtEquipmentRangeColor.Name = "txtEquipmentRangeColor";
            this.txtEquipmentRangeColor.Properties.Appearance.BackColor = System.Drawing.Color.Red;
            this.txtEquipmentRangeColor.Properties.Appearance.Options.UseBackColor = true;
            this.txtEquipmentRangeColor.Properties.ReadOnly = true;
            this.txtEquipmentRangeColor.Size = new System.Drawing.Size(94, 20);
            this.txtEquipmentRangeColor.StyleController = this.layoutControl1;
            this.txtEquipmentRangeColor.TabIndex = 6;
            this.txtEquipmentRangeColor.TabStop = false;
            // 
            // txtNormalRangeColor
            // 
            this.txtNormalRangeColor.Location = new System.Drawing.Point(12, 522);
            this.txtNormalRangeColor.Name = "txtNormalRangeColor";
            this.txtNormalRangeColor.Properties.Appearance.BackColor = System.Drawing.Color.Yellow;
            this.txtNormalRangeColor.Properties.Appearance.Options.UseBackColor = true;
            this.txtNormalRangeColor.Properties.ReadOnly = true;
            this.txtNormalRangeColor.Size = new System.Drawing.Size(94, 20);
            this.txtNormalRangeColor.StyleController = this.layoutControl1;
            this.txtNormalRangeColor.TabIndex = 5;
            this.txtNormalRangeColor.TabStop = false;
            // 
            // grdLABPatientHistory
            // 
            this.grdLABPatientHistory.Location = new System.Drawing.Point(12, 12);
            this.grdLABPatientHistory.MainView = this.viewLABPatientHistory;
            this.grdLABPatientHistory.Name = "grdLABPatientHistory";
            this.grdLABPatientHistory.Size = new System.Drawing.Size(856, 506);
            this.grdLABPatientHistory.TabIndex = 4;
            this.grdLABPatientHistory.ToolTipController = this.toolTipController1;
            this.grdLABPatientHistory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewLABPatientHistory});
            // 
            // viewLABPatientHistory
            // 
            this.viewLABPatientHistory.GridControl = this.grdLABPatientHistory;
            this.viewLABPatientHistory.Name = "viewLABPatientHistory";
            this.viewLABPatientHistory.OptionsBehavior.Editable = false;
            this.viewLABPatientHistory.OptionsView.ShowGroupPanel = false;
            this.viewLABPatientHistory.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.viewLABPatientHistory_RowCellStyle);
            // 
            // toolTipController1
            // 
            this.toolTipController1.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(this.toolTipController1_GetActiveObjectInfo);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem5,
            this.txbRemarked,
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(880, 628);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdLABPatientHistory;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(860, 510);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtNormalRangeColor;
            this.layoutControlItem2.CustomizationFormText = "Outside of Normal Range";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 510);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(428, 24);
            this.layoutControlItem2.Text = "Outside of Normal Range";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Right;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(326, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtEquipmentRangeColor;
            this.layoutControlItem3.CustomizationFormText = "Outside of Equipment Range";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 534);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(428, 24);
            this.layoutControlItem3.Text = "Outside of Equipment Range";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Right;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(326, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.cmdClose;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(671, 510);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(82, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(189, 98);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // txbRemarked
            // 
            this.txbRemarked.Control = this.txtRemarked;
            this.txbRemarked.CustomizationFormText = "Remarked (Please hover the mouse on the result to see the remark)";
            this.txbRemarked.Location = new System.Drawing.Point(0, 558);
            this.txbRemarked.Name = "txbRemarked";
            this.txbRemarked.Size = new System.Drawing.Size(428, 50);
            this.txbRemarked.Text = "Remarked (Please hover the mouse on the result to see the remark)";
            this.txbRemarked.TextLocation = DevExpress.Utils.Locations.Right;
            this.txbRemarked.TextSize = new System.Drawing.Size(326, 13);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7,
            this.layoutControlItem6,
            this.layoutControlItem4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(428, 510);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(243, 98);
            this.layoutControlGroup2.Text = "layoutControlGroup2";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.cmdApplyFilter;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(219, 26);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtDateTo;
            this.layoutControlItem6.CustomizationFormText = "To";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(219, 24);
            this.layoutControlItem6.Text = "To";
            this.layoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(50, 20);
            this.layoutControlItem6.TextToControlDistance = 5;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtDateFrom;
            this.layoutControlItem4.CustomizationFormText = "From";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(219, 24);
            this.layoutControlItem4.Text = "From";
            this.layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(50, 20);
            this.layoutControlItem4.TextToControlDistance = 5;
            // 
            // frmLABPatientHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 628);
            this.Controls.Add(this.layoutControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLABPatientHistory";
            this.Text = "Patient Lab tests history";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLABPatientHistory_FormClosing);
            this.Load += new System.EventHandler(this.frmLABPatientHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDateTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemarked.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEquipmentRangeColor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNormalRangeColor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLABPatientHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewLABPatientHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbRemarked)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl grdLABPatientHistory;
        private DevExpress.XtraGrid.Views.Grid.GridView viewLABPatientHistory;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit txtEquipmentRangeColor;
        private DevExpress.XtraEditors.TextEdit txtNormalRangeColor;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
        private DevExpress.XtraEditors.TextEdit txtRemarked;
        private DevExpress.XtraLayout.LayoutControlItem txbRemarked;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton cmdApplyFilter;
        private DevExpress.XtraEditors.DateEdit txtDateTo;
        private DevExpress.XtraEditors.DateEdit txtDateFrom;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;

    }
}