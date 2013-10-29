namespace AfyaPro_NextGen
{
    partial class frmCTCPreARTVisits
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
            this.cmdEdit = new DevExpress.XtraEditors.SimpleButton();
            this.grdCTCPreARTVisits = new DevExpress.XtraGrid.GridControl();
            this.viewCTCPreARTVisits = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cmdDelete = new DevExpress.XtraEditors.SimpleButton();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            this.cmdNew = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbDelete = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbUpdate = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbCTCHIVTests = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCTCPreARTVisits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewCTCPreARTVisits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbCTCHIVTests)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cmdEdit);
            this.layoutControl1.Controls.Add(this.grdCTCPreARTVisits);
            this.layoutControl1.Controls.Add(this.cmdDelete);
            this.layoutControl1.Controls.Add(this.cmdClose);
            this.layoutControl1.Controls.Add(this.cmdNew);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(839, 468);
            this.layoutControl1.TabIndex = 13;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(207, 428);
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Size = new System.Drawing.Size(198, 28);
            this.cmdEdit.StyleController = this.layoutControl1;
            this.cmdEdit.TabIndex = 52;
            this.cmdEdit.Text = "Edit";
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // grdCTCPreARTVisits
            // 
            this.grdCTCPreARTVisits.Location = new System.Drawing.Point(12, 12);
            this.grdCTCPreARTVisits.MainView = this.viewCTCPreARTVisits;
            this.grdCTCPreARTVisits.Name = "grdCTCPreARTVisits";
            this.grdCTCPreARTVisits.Size = new System.Drawing.Size(815, 412);
            this.grdCTCPreARTVisits.TabIndex = 14;
            this.grdCTCPreARTVisits.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewCTCPreARTVisits});
            this.grdCTCPreARTVisits.Visible = false;
            // 
            // viewCTCPreARTVisits
            // 
            this.viewCTCPreARTVisits.GridControl = this.grdCTCPreARTVisits;
            this.viewCTCPreARTVisits.Name = "viewCTCPreARTVisits";
            this.viewCTCPreARTVisits.OptionsBehavior.Editable = false;
            this.viewCTCPreARTVisits.OptionsView.ColumnAutoWidth = false;
            this.viewCTCPreARTVisits.OptionsView.ShowGroupPanel = false;
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(409, 428);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(204, 28);
            this.cmdDelete.StyleController = this.layoutControl1;
            this.cmdDelete.TabIndex = 49;
            this.cmdDelete.Text = "Delete";
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(617, 428);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(210, 28);
            this.cmdClose.StyleController = this.layoutControl1;
            this.cmdClose.TabIndex = 5;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdNew
            // 
            this.cmdNew.Location = new System.Drawing.Point(12, 428);
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Size = new System.Drawing.Size(191, 28);
            this.cmdNew.StyleController = this.layoutControl1;
            this.cmdNew.TabIndex = 4;
            this.cmdNew.Text = "New";
            this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.txbDelete,
            this.txbUpdate,
            this.txbCTCHIVTests});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(839, 468);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cmdNew;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 416);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(28, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(195, 32);
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
            this.layoutControlItem5.Location = new System.Drawing.Point(605, 416);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(47, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(214, 32);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // txbDelete
            // 
            this.txbDelete.Control = this.cmdDelete;
            this.txbDelete.CustomizationFormText = "txbDelete";
            this.txbDelete.Location = new System.Drawing.Point(397, 416);
            this.txbDelete.MinSize = new System.Drawing.Size(50, 26);
            this.txbDelete.Name = "txbDelete";
            this.txbDelete.Size = new System.Drawing.Size(208, 32);
            this.txbDelete.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbDelete.Text = "txbDelete";
            this.txbDelete.TextSize = new System.Drawing.Size(0, 0);
            this.txbDelete.TextToControlDistance = 0;
            this.txbDelete.TextVisible = false;
            // 
            // txbUpdate
            // 
            this.txbUpdate.Control = this.cmdEdit;
            this.txbUpdate.CustomizationFormText = "txbUpdate";
            this.txbUpdate.Location = new System.Drawing.Point(195, 416);
            this.txbUpdate.MinSize = new System.Drawing.Size(46, 26);
            this.txbUpdate.Name = "txbUpdate";
            this.txbUpdate.Size = new System.Drawing.Size(202, 32);
            this.txbUpdate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbUpdate.Text = "txbUpdate";
            this.txbUpdate.TextSize = new System.Drawing.Size(0, 0);
            this.txbUpdate.TextToControlDistance = 0;
            this.txbUpdate.TextVisible = false;
            // 
            // txbCTCHIVTests
            // 
            this.txbCTCHIVTests.Control = this.grdCTCPreARTVisits;
            this.txbCTCHIVTests.CustomizationFormText = "txbCTCHIVTests";
            this.txbCTCHIVTests.Location = new System.Drawing.Point(0, 0);
            this.txbCTCHIVTests.Name = "txbCTCHIVTests";
            this.txbCTCHIVTests.Size = new System.Drawing.Size(819, 416);
            this.txbCTCHIVTests.Text = "txbCTCHIVTests";
            this.txbCTCHIVTests.TextSize = new System.Drawing.Size(0, 0);
            this.txbCTCHIVTests.TextToControlDistance = 0;
            this.txbCTCHIVTests.TextVisible = false;
            // 
            // frmCTCPreARTVisits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 468);
            this.Controls.Add(this.layoutControl1);
            this.KeyPreview = true;
            this.Name = "frmCTCPreARTVisits";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pre-ART Child/Adult History";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCTCPreARTVisits_FormClosing);
            this.Load += new System.EventHandler(this.frmCTCPreARTVisits_Load);
            this.Shown += new System.EventHandler(this.frmCTCPreARTVisits_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCTCPreARTVisits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewCTCPreARTVisits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbCTCHIVTests)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton cmdNew;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton cmdDelete;
        private DevExpress.XtraLayout.LayoutControlItem txbDelete;
        internal DevExpress.XtraGrid.GridControl grdCTCPreARTVisits;
        private DevExpress.XtraGrid.Views.Grid.GridView viewCTCPreARTVisits;
        private DevExpress.XtraLayout.LayoutControlItem txbCTCHIVTests;
        private DevExpress.XtraEditors.SimpleButton cmdEdit;
        private DevExpress.XtraLayout.LayoutControlItem txbUpdate;
    }
}