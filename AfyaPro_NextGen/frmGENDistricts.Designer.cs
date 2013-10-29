namespace AfyaPro_NextGen
{
    partial class frmGENDistricts
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
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txbRegion = new DevExpress.XtraEditors.LabelControl();
            this.cboRegion = new DevExpress.XtraEditors.LookUpEdit();
            this.txbCountry = new DevExpress.XtraEditors.LabelControl();
            this.cboCountry = new DevExpress.XtraEditors.LookUpEdit();
            this.txtDescription = new DevExpress.XtraEditors.TextEdit();
            this.txbDescription = new DevExpress.XtraEditors.LabelControl();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txbCode = new DevExpress.XtraEditors.LabelControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboRegion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCountry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.txbRegion);
            this.groupControl2.Controls.Add(this.cboRegion);
            this.groupControl2.Controls.Add(this.txbCountry);
            this.groupControl2.Controls.Add(this.cboCountry);
            this.groupControl2.Controls.Add(this.txtDescription);
            this.groupControl2.Controls.Add(this.txbDescription);
            this.groupControl2.Controls.Add(this.txtCode);
            this.groupControl2.Controls.Add(this.txbCode);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(317, 158);
            this.groupControl2.TabIndex = 11;
            this.groupControl2.Text = "groupControl2";
            // 
            // txbRegion
            // 
            this.txbRegion.Location = new System.Drawing.Point(9, 56);
            this.txbRegion.Name = "txbRegion";
            this.txbRegion.Size = new System.Drawing.Size(33, 13);
            this.txbRegion.TabIndex = 8;
            this.txbRegion.Text = "Region";
            // 
            // cboRegion
            // 
            this.cboRegion.Location = new System.Drawing.Point(112, 53);
            this.cboRegion.Name = "cboRegion";
            this.cboRegion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboRegion.Properties.NullText = "";
            this.cboRegion.Size = new System.Drawing.Size(193, 20);
            this.cboRegion.TabIndex = 2;
            // 
            // txbCountry
            // 
            this.txbCountry.Location = new System.Drawing.Point(9, 25);
            this.txbCountry.Name = "txbCountry";
            this.txbCountry.Size = new System.Drawing.Size(39, 13);
            this.txbCountry.TabIndex = 6;
            this.txbCountry.Text = "Country";
            // 
            // cboCountry
            // 
            this.cboCountry.Location = new System.Drawing.Point(112, 22);
            this.cboCountry.Name = "cboCountry";
            this.cboCountry.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCountry.Properties.NullText = "";
            this.cboCountry.Size = new System.Drawing.Size(193, 20);
            this.cboCountry.TabIndex = 1;
            this.cboCountry.EditValueChanged += new System.EventHandler(this.cboCountry_EditValueChanged);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(112, 115);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(193, 20);
            this.txtDescription.TabIndex = 4;
            // 
            // txbDescription
            // 
            this.txbDescription.Location = new System.Drawing.Point(9, 118);
            this.txbDescription.Name = "txbDescription";
            this.txbDescription.Size = new System.Drawing.Size(53, 13);
            this.txbDescription.TabIndex = 3;
            this.txbDescription.Text = "Description";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(112, 84);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(100, 20);
            this.txtCode.TabIndex = 3;
            // 
            // txbCode
            // 
            this.txbCode.Location = new System.Drawing.Point(9, 87);
            this.txbCode.Name = "txbCode";
            this.txbCode.Size = new System.Drawing.Size(25, 13);
            this.txbCode.TabIndex = 1;
            this.txbCode.Text = "Code";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 158);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cmdOk);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cmdClose);
            this.splitContainer1.Size = new System.Drawing.Size(317, 73);
            this.splitContainer1.SplitterDistance = 153;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 13;
            // 
            // cmdOk
            // 
            this.cmdOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdOk.Location = new System.Drawing.Point(0, 0);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(153, 73);
            this.cmdOk.TabIndex = 5;
            this.cmdOk.Text = "Save";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdClose.Location = new System.Drawing.Point(0, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(163, 73);
            this.cmdClose.TabIndex = 6;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // frmGENDistricts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 231);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGENDistricts";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Districts";
            this.Load += new System.EventHandler(this.frmGENDistricts_Load);
            this.Activated += new System.EventHandler(this.frmGENDistricts_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboRegion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCountry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl txbCode;
        private DevExpress.XtraEditors.TextEdit txtDescription;
        private DevExpress.XtraEditors.LabelControl txbDescription;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SimpleButton cmdOk;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
        private DevExpress.XtraEditors.LabelControl txbCountry;
        private DevExpress.XtraEditors.LookUpEdit cboCountry;
        private DevExpress.XtraEditors.LabelControl txbRegion;
        private DevExpress.XtraEditors.LookUpEdit cboRegion;
    }
}