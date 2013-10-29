namespace AfyaPro_NextGen
{
    partial class frmIVTransferOutsSwitch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIVTransferOutsSwitch));
            this.radOrderTypes = new DevExpress.XtraEditors.RadioGroup();
            this.cmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.radOrderTypes.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // radOrderTypes
            // 
            this.radOrderTypes.Location = new System.Drawing.Point(3, 3);
            this.radOrderTypes.Name = "radOrderTypes";
            this.radOrderTypes.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Create Inventory Transfer Out Order to Suppliers"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Create Inventory Transfer Out Order from one store to another")});
            this.radOrderTypes.Size = new System.Drawing.Size(350, 94);
            this.radOrderTypes.TabIndex = 0;
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(3, 110);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(138, 39);
            this.cmdOk.TabIndex = 1;
            this.cmdOk.Text = "Ok";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(215, 110);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(138, 39);
            this.cmdClose.TabIndex = 2;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // frmIVTransferOutsSwitch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 157);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.radOrderTypes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmIVTransferOutsSwitch";
            this.Text = "Transfers Inventory Out Order Type";
            this.Load += new System.EventHandler(this.frmIVTransferOutsSwitch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radOrderTypes.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.RadioGroup radOrderTypes;
        private DevExpress.XtraEditors.SimpleButton cmdOk;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
    }
}