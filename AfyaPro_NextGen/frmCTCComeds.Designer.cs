﻿namespace AfyaPro_NextGen
{
    partial class frmCTCComeds
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
            this.grdCTCComeds = new DevExpress.XtraGrid.GridControl();
            this.viewCTCComeds = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.selected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.description = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbOk = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.grdCTCComeds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewCTCComeds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbOk)).BeginInit();
            this.SuspendLayout();
            // 
            // grdCTCComeds
            // 
            this.grdCTCComeds.Location = new System.Drawing.Point(12, 12);
            this.grdCTCComeds.MainView = this.viewCTCComeds;
            this.grdCTCComeds.Name = "grdCTCComeds";
            this.grdCTCComeds.Size = new System.Drawing.Size(346, 261);
            this.grdCTCComeds.TabIndex = 38;
            this.grdCTCComeds.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewCTCComeds,
            this.gridView2});
            // 
            // viewCTCComeds
            // 
            this.viewCTCComeds.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.selected,
            this.code,
            this.description});
            this.viewCTCComeds.GridControl = this.grdCTCComeds;
            this.viewCTCComeds.Name = "viewCTCComeds";
            this.viewCTCComeds.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.viewCTCComeds.OptionsView.ShowGroupPanel = false;
            // 
            // selected
            // 
            this.selected.Caption = "gridColumn1";
            this.selected.FieldName = "selected";
            this.selected.Name = "selected";
            this.selected.OptionsColumn.ShowCaption = false;
            this.selected.Visible = true;
            this.selected.VisibleIndex = 0;
            this.selected.Width = 43;
            // 
            // code
            // 
            this.code.Caption = "Code";
            this.code.FieldName = "code";
            this.code.Name = "code";
            this.code.OptionsColumn.AllowEdit = false;
            this.code.Visible = true;
            this.code.VisibleIndex = 1;
            this.code.Width = 90;
            // 
            // description
            // 
            this.description.Caption = "Description";
            this.description.FieldName = "description";
            this.description.Name = "description";
            this.description.OptionsColumn.AllowEdit = false;
            this.description.Visible = true;
            this.description.VisibleIndex = 2;
            this.description.Width = 244;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.grdCTCComeds;
            this.gridView2.Name = "gridView2";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cmdOk);
            this.layoutControl1.Controls.Add(this.grdCTCComeds);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(370, 311);
            this.layoutControl1.TabIndex = 39;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(12, 277);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(346, 22);
            this.cmdOk.StyleController = this.layoutControl1;
            this.cmdOk.TabIndex = 39;
            this.cmdOk.Text = "Ok";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.txbOk});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(370, 311);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdCTCComeds;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(350, 265);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // txbOk
            // 
            this.txbOk.Control = this.cmdOk;
            this.txbOk.CustomizationFormText = "txbOk";
            this.txbOk.Location = new System.Drawing.Point(0, 265);
            this.txbOk.Name = "txbOk";
            this.txbOk.Size = new System.Drawing.Size(350, 26);
            this.txbOk.Text = "txbOk";
            this.txbOk.TextSize = new System.Drawing.Size(0, 0);
            this.txbOk.TextToControlDistance = 0;
            this.txbOk.TextVisible = false;
            // 
            // frmCTCComeds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 311);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmCTCComeds";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relevant Comeds";
            ((System.ComponentModel.ISupportInitialize)(this.grdCTCComeds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewCTCComeds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbOk)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdCTCComeds;
        private DevExpress.XtraGrid.Views.Grid.GridView viewCTCComeds;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn selected;
        private DevExpress.XtraGrid.Columns.GridColumn code;
        private DevExpress.XtraGrid.Columns.GridColumn description;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton cmdOk;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem txbOk;
    }
}