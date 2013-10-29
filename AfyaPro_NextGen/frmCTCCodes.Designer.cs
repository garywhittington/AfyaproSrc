namespace AfyaPro_NextGen
{
    partial class frmCTCCodes
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
            this.txtDescription = new DevExpress.XtraEditors.TextEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtCategory = new DevExpress.XtraEditors.TextEdit();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            this.cmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbDescription = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbClose = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbCategory = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbCategory)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(69, 36);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(278, 20);
            this.txtDescription.StyleController = this.layoutControl1;
            this.txtDescription.TabIndex = 2;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtCategory);
            this.layoutControl1.Controls.Add(this.cmdClose);
            this.layoutControl1.Controls.Add(this.cmdOk);
            this.layoutControl1.Controls.Add(this.txtDescription);
            this.layoutControl1.Controls.Add(this.txtCode);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(359, 135);
            this.layoutControl1.TabIndex = 14;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtCategory
            // 
            this.txtCategory.Location = new System.Drawing.Point(69, 60);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(278, 20);
            this.txtCategory.StyleController = this.layoutControl1;
            this.txtCategory.TabIndex = 5;
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(177, 84);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(170, 39);
            this.cmdClose.StyleController = this.layoutControl1;
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(12, 84);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(161, 39);
            this.cmdOk.StyleController = this.layoutControl1;
            this.cmdOk.TabIndex = 3;
            this.cmdOk.Text = "Save";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(69, 12);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(278, 20);
            this.txtCode.StyleController = this.layoutControl1;
            this.txtCode.TabIndex = 1;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.txbCode,
            this.txbDescription,
            this.txbSave,
            this.txbClose,
            this.txbCategory});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(359, 135);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // txbCode
            // 
            this.txbCode.Control = this.txtCode;
            this.txbCode.CustomizationFormText = "Code";
            this.txbCode.Location = new System.Drawing.Point(0, 0);
            this.txbCode.Name = "txbCode";
            this.txbCode.Size = new System.Drawing.Size(339, 24);
            this.txbCode.Text = "Code";
            this.txbCode.TextSize = new System.Drawing.Size(53, 13);
            // 
            // txbDescription
            // 
            this.txbDescription.Control = this.txtDescription;
            this.txbDescription.CustomizationFormText = "Description";
            this.txbDescription.Location = new System.Drawing.Point(0, 24);
            this.txbDescription.Name = "txbDescription";
            this.txbDescription.Size = new System.Drawing.Size(339, 24);
            this.txbDescription.Text = "Description";
            this.txbDescription.TextSize = new System.Drawing.Size(53, 13);
            // 
            // txbSave
            // 
            this.txbSave.Control = this.cmdOk;
            this.txbSave.CustomizationFormText = "txbSave";
            this.txbSave.Location = new System.Drawing.Point(0, 72);
            this.txbSave.MinSize = new System.Drawing.Size(39, 26);
            this.txbSave.Name = "txbSave";
            this.txbSave.Size = new System.Drawing.Size(165, 43);
            this.txbSave.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbSave.Text = "txbSave";
            this.txbSave.TextSize = new System.Drawing.Size(0, 0);
            this.txbSave.TextToControlDistance = 0;
            this.txbSave.TextVisible = false;
            // 
            // txbClose
            // 
            this.txbClose.Control = this.cmdClose;
            this.txbClose.CustomizationFormText = "txbClose";
            this.txbClose.Location = new System.Drawing.Point(165, 72);
            this.txbClose.MinSize = new System.Drawing.Size(41, 26);
            this.txbClose.Name = "txbClose";
            this.txbClose.Size = new System.Drawing.Size(174, 43);
            this.txbClose.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbClose.Text = "txbClose";
            this.txbClose.TextSize = new System.Drawing.Size(0, 0);
            this.txbClose.TextToControlDistance = 0;
            this.txbClose.TextVisible = false;
            // 
            // txbCategory
            // 
            this.txbCategory.Control = this.txtCategory;
            this.txbCategory.CustomizationFormText = "Category";
            this.txbCategory.Location = new System.Drawing.Point(0, 48);
            this.txbCategory.Name = "txbCategory";
            this.txbCategory.Size = new System.Drawing.Size(339, 24);
            this.txbCategory.Text = "Category";
            this.txbCategory.TextSize = new System.Drawing.Size(53, 13);
            // 
            // frmCTCCodes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 135);
            this.Controls.Add(this.layoutControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCTCCodes";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CTC Codes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCTCCodes_FormClosing);
            this.Load += new System.EventHandler(this.frmCTCCodes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbCategory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtDescription;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.SimpleButton cmdOk;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit txtCategory;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem txbCode;
        private DevExpress.XtraLayout.LayoutControlItem txbDescription;
        private DevExpress.XtraLayout.LayoutControlItem txbSave;
        private DevExpress.XtraLayout.LayoutControlItem txbClose;
        private DevExpress.XtraLayout.LayoutControlItem txbCategory;
    }
}