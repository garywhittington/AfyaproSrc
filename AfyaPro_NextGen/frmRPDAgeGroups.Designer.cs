namespace AfyaPro_NextGen
{
    partial class frmRPDAgeGroups
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRPDAgeGroups));
            this.grdRPDAgeGroups = new DevExpress.XtraGrid.GridControl();
            this.viewRPDAgeGroups = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.description = new DevExpress.XtraGrid.Columns.GridColumn();
            this.value1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtvalue1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.value2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtvalue2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.cmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            this.cmdAdd = new DevExpress.XtraEditors.SimpleButton();
            this.cmdRemove = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdRPDAgeGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewRPDAgeGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtvalue1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtvalue2)).BeginInit();
            this.SuspendLayout();
            // 
            // grdRPDAgeGroups
            // 
            this.grdRPDAgeGroups.Location = new System.Drawing.Point(6, 34);
            this.grdRPDAgeGroups.MainView = this.viewRPDAgeGroups;
            this.grdRPDAgeGroups.Name = "grdRPDAgeGroups";
            this.grdRPDAgeGroups.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.txtvalue1,
            this.txtvalue2});
            this.grdRPDAgeGroups.Size = new System.Drawing.Size(601, 242);
            this.grdRPDAgeGroups.TabIndex = 0;
            this.grdRPDAgeGroups.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewRPDAgeGroups});
            // 
            // viewRPDAgeGroups
            // 
            this.viewRPDAgeGroups.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.description,
            this.value1,
            this.value2});
            this.viewRPDAgeGroups.GridControl = this.grdRPDAgeGroups;
            this.viewRPDAgeGroups.Name = "viewRPDAgeGroups";
            this.viewRPDAgeGroups.OptionsView.ShowGroupPanel = false;
            this.viewRPDAgeGroups.OptionsView.ShowIndicator = false;
            this.viewRPDAgeGroups.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.viewRPDAgeGroups_InitNewRow);
            // 
            // description
            // 
            this.description.Caption = "Description";
            this.description.FieldName = "description";
            this.description.Name = "description";
            this.description.Visible = true;
            this.description.VisibleIndex = 0;
            this.description.Width = 195;
            // 
            // value1
            // 
            this.value1.Caption = "Lower (Inclusive)";
            this.value1.ColumnEdit = this.txtvalue1;
            this.value1.FieldName = "value1";
            this.value1.Name = "value1";
            this.value1.Visible = true;
            this.value1.VisibleIndex = 1;
            this.value1.Width = 100;
            // 
            // txtvalue1
            // 
            this.txtvalue1.AutoHeight = false;
            this.txtvalue1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtvalue1.Name = "txtvalue1";
            // 
            // value2
            // 
            this.value2.Caption = "Upper (Exclusive)";
            this.value2.ColumnEdit = this.txtvalue2;
            this.value2.FieldName = "value2";
            this.value2.Name = "value2";
            this.value2.Visible = true;
            this.value2.VisibleIndex = 2;
            this.value2.Width = 101;
            // 
            // txtvalue2
            // 
            this.txtvalue2.AutoHeight = false;
            this.txtvalue2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtvalue2.Name = "txtvalue2";
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(6, 285);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(205, 43);
            this.cmdOk.TabIndex = 1;
            this.cmdOk.Text = "Save";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(214, 285);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(191, 43);
            this.cmdClose.TabIndex = 2;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(6, 7);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(75, 23);
            this.cmdAdd.TabIndex = 3;
            this.cmdAdd.Text = "Add";
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdRemove
            // 
            this.cmdRemove.Location = new System.Drawing.Point(86, 7);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(75, 23);
            this.cmdRemove.TabIndex = 4;
            this.cmdRemove.Text = "Remove";
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // frmRPDAgeGroups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 334);
            this.Controls.Add(this.cmdRemove);
            this.Controls.Add(this.cmdAdd);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.grdRPDAgeGroups);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRPDAgeGroups";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Age Groups";
            this.Load += new System.EventHandler(this.frmRPDAgeGroups_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdRPDAgeGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewRPDAgeGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtvalue1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtvalue2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdRPDAgeGroups;
        private DevExpress.XtraGrid.Views.Grid.GridView viewRPDAgeGroups;
        private DevExpress.XtraGrid.Columns.GridColumn description;
        private DevExpress.XtraGrid.Columns.GridColumn value1;
        private DevExpress.XtraGrid.Columns.GridColumn value2;
        private DevExpress.XtraEditors.SimpleButton cmdOk;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtvalue1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtvalue2;
        private DevExpress.XtraEditors.SimpleButton cmdAdd;
        private DevExpress.XtraEditors.SimpleButton cmdRemove;
    }
}