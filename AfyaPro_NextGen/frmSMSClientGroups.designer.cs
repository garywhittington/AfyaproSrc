namespace AfyaPro_NextGen
{
    partial class frmSMSClientGroups
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdMain = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdMain);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(557, 437);
            this.panel1.TabIndex = 1;
            // 
            // grdMain
            // 
            this.grdMain.AllowUserToAddRows = false;
            this.grdMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdMain.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grdMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.Location = new System.Drawing.Point(0, 0);
            this.grdMain.Name = "grdMain";
            this.grdMain.ReadOnly = true;
            this.grdMain.RowHeadersVisible = false;
            this.grdMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdMain.Size = new System.Drawing.Size(557, 437);
            this.grdMain.TabIndex = 0;
            this.grdMain.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdMain_CellContentClick);
            this.grdMain.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdMain_CellContentDoubleClick);
            // 
            // frmSMSClientGroups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 437);
            this.Controls.Add(this.panel1);
            this.Name = "frmSMSClientGroups";
            this.Text = "Patient Categories";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainGrid_FormClosing);
            this.Load += new System.EventHandler(this.frmMainGrid_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView grdMain;

    }
}