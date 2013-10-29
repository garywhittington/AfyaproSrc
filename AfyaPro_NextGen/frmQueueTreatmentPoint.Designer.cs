namespace AfyaPro_NextGen
{
    partial class frmQueueTreatmentPoint
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
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            this.cmdRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.grdQueueTreatmentPoint = new DevExpress.XtraGrid.GridControl();
            this.viewQueueTreatmentPoint = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbQueueTreatmentPoint = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbRefresh = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbClose = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdQueueTreatmentPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewQueueTreatmentPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbQueueTreatmentPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbClose)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cmdClose);
            this.layoutControl1.Controls.Add(this.cmdRefresh);
            this.layoutControl1.Controls.Add(this.grdQueueTreatmentPoint);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(445, 396);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(224, 343);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(209, 41);
            this.cmdClose.StyleController = this.layoutControl1;
            this.cmdClose.TabIndex = 6;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Location = new System.Drawing.Point(12, 343);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(208, 41);
            this.cmdRefresh.StyleController = this.layoutControl1;
            this.cmdRefresh.TabIndex = 5;
            this.cmdRefresh.Text = "Refresh Queue";
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // grdQueueTreatmentPoint
            // 
            this.grdQueueTreatmentPoint.Location = new System.Drawing.Point(12, 12);
            this.grdQueueTreatmentPoint.MainView = this.viewQueueTreatmentPoint;
            this.grdQueueTreatmentPoint.Name = "grdQueueTreatmentPoint";
            this.grdQueueTreatmentPoint.Size = new System.Drawing.Size(421, 327);
            this.grdQueueTreatmentPoint.TabIndex = 4;
            this.grdQueueTreatmentPoint.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewQueueTreatmentPoint});
            this.grdQueueTreatmentPoint.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.grdQueueTreatmentPoint_ProcessGridKey);
            this.grdQueueTreatmentPoint.DoubleClick += new System.EventHandler(this.grdQueueTreatmentPoint_DoubleClick);
            // 
            // viewQueueTreatmentPoint
            // 
            this.viewQueueTreatmentPoint.GridControl = this.grdQueueTreatmentPoint;
            this.viewQueueTreatmentPoint.Name = "viewQueueTreatmentPoint";
            this.viewQueueTreatmentPoint.OptionsBehavior.Editable = false;
            this.viewQueueTreatmentPoint.OptionsBehavior.ReadOnly = true;
            this.viewQueueTreatmentPoint.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.txbQueueTreatmentPoint,
            this.txbRefresh,
            this.txbClose});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(445, 396);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // txbQueueTreatmentPoint
            // 
            this.txbQueueTreatmentPoint.Control = this.grdQueueTreatmentPoint;
            this.txbQueueTreatmentPoint.CustomizationFormText = "txbQueueTreatmentPoint";
            this.txbQueueTreatmentPoint.Location = new System.Drawing.Point(0, 0);
            this.txbQueueTreatmentPoint.Name = "txbQueueTreatmentPoint";
            this.txbQueueTreatmentPoint.Size = new System.Drawing.Size(425, 331);
            this.txbQueueTreatmentPoint.Text = "txbQueueTreatmentPoint";
            this.txbQueueTreatmentPoint.TextSize = new System.Drawing.Size(0, 0);
            this.txbQueueTreatmentPoint.TextToControlDistance = 0;
            this.txbQueueTreatmentPoint.TextVisible = false;
            // 
            // txbRefresh
            // 
            this.txbRefresh.Control = this.cmdRefresh;
            this.txbRefresh.CustomizationFormText = "txbRefresh";
            this.txbRefresh.Location = new System.Drawing.Point(0, 331);
            this.txbRefresh.MinSize = new System.Drawing.Size(88, 26);
            this.txbRefresh.Name = "txbRefresh";
            this.txbRefresh.Size = new System.Drawing.Size(212, 45);
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
            this.txbClose.Location = new System.Drawing.Point(212, 331);
            this.txbClose.MinSize = new System.Drawing.Size(41, 26);
            this.txbClose.Name = "txbClose";
            this.txbClose.Size = new System.Drawing.Size(213, 45);
            this.txbClose.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbClose.Text = "txbClose";
            this.txbClose.TextSize = new System.Drawing.Size(0, 0);
            this.txbClose.TextToControlDistance = 0;
            this.txbClose.TextVisible = false;
            // 
            // frmQueueTreatmentPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 396);
            this.Controls.Add(this.layoutControl1);
            this.KeyPreview = true;
            this.Name = "frmQueueTreatmentPoint";
            this.Text = "Patient Queue";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmQueueTreatmentPoint_FormClosing);
            this.Load += new System.EventHandler(this.frmQueueTreatmentPoint_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmQueueTreatmentPoint_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdQueueTreatmentPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewQueueTreatmentPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbQueueTreatmentPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl grdQueueTreatmentPoint;
        private DevExpress.XtraGrid.Views.Grid.GridView viewQueueTreatmentPoint;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem txbQueueTreatmentPoint;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
        private DevExpress.XtraEditors.SimpleButton cmdRefresh;
        private DevExpress.XtraLayout.LayoutControlItem txbRefresh;
        private DevExpress.XtraLayout.LayoutControlItem txbClose;
    }
}