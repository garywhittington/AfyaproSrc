namespace AfyaPro_NextGen
{
    partial class frmGENSearchEngine
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
            this.grpSelectedFields = new DevExpress.XtraEditors.GroupControl();
            this.cmdDown = new DevExpress.XtraEditors.SimpleButton();
            this.cmdUp = new DevExpress.XtraEditors.SimpleButton();
            this.grdGENSearchEngineSelectedFields = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grdFields = new DevExpress.XtraEditors.GroupControl();
            this.cmdUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.txbDisplayName = new DevExpress.XtraEditors.LabelControl();
            this.txtDisplayName = new DevExpress.XtraEditors.TextEdit();
            this.txbFieldName = new DevExpress.XtraEditors.LabelControl();
            this.txtFieldName = new DevExpress.XtraEditors.TextEdit();
            this.cmdRemove = new DevExpress.XtraEditors.SimpleButton();
            this.cmdAdd = new DevExpress.XtraEditors.SimpleButton();
            this.grpAvailableFields = new DevExpress.XtraEditors.GroupControl();
            this.grdGENSearchEngineAvailableFields = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txbSearchObject = new DevExpress.XtraEditors.LabelControl();
            this.cboSearchObject = new DevExpress.XtraEditors.LookUpEdit();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpSelectedFields)).BeginInit();
            this.grpSelectedFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdGENSearchEngineSelectedFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFields)).BeginInit();
            this.grdFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDisplayName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFieldName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAvailableFields)).BeginInit();
            this.grpAvailableFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdGENSearchEngineAvailableFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboSearchObject.Properties)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.grpSelectedFields);
            this.groupControl2.Controls.Add(this.cmdRemove);
            this.groupControl2.Controls.Add(this.cmdAdd);
            this.groupControl2.Controls.Add(this.grpAvailableFields);
            this.groupControl2.Controls.Add(this.groupControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(797, 498);
            this.groupControl2.TabIndex = 14;
            this.groupControl2.Text = "groupControl2";
            // 
            // grpSelectedFields
            // 
            this.grpSelectedFields.Controls.Add(this.cmdDown);
            this.grpSelectedFields.Controls.Add(this.cmdUp);
            this.grpSelectedFields.Controls.Add(this.grdGENSearchEngineSelectedFields);
            this.grpSelectedFields.Controls.Add(this.grdFields);
            this.grpSelectedFields.Dock = System.Windows.Forms.DockStyle.Right;
            this.grpSelectedFields.Location = new System.Drawing.Point(374, 44);
            this.grpSelectedFields.Name = "grpSelectedFields";
            this.grpSelectedFields.Size = new System.Drawing.Size(421, 452);
            this.grpSelectedFields.TabIndex = 11;
            this.grpSelectedFields.Text = "Selected Fields";
            // 
            // cmdDown
            // 
            this.cmdDown.Location = new System.Drawing.Point(381, 242);
            this.cmdDown.Name = "cmdDown";
            this.cmdDown.Size = new System.Drawing.Size(34, 33);
            this.cmdDown.TabIndex = 12;
            this.cmdDown.Text = "\\/";
            this.cmdDown.Click += new System.EventHandler(this.cmdDown_Click);
            // 
            // cmdUp
            // 
            this.cmdUp.Location = new System.Drawing.Point(381, 209);
            this.cmdUp.Name = "cmdUp";
            this.cmdUp.Size = new System.Drawing.Size(34, 33);
            this.cmdUp.TabIndex = 11;
            this.cmdUp.Text = "/\\";
            this.cmdUp.Click += new System.EventHandler(this.cmdUp_Click);
            // 
            // grdGENSearchEngineSelectedFields
            // 
            this.grdGENSearchEngineSelectedFields.Dock = System.Windows.Forms.DockStyle.Left;
            this.grdGENSearchEngineSelectedFields.Location = new System.Drawing.Point(2, 77);
            this.grdGENSearchEngineSelectedFields.MainView = this.gridView1;
            this.grdGENSearchEngineSelectedFields.Name = "grdGENSearchEngineSelectedFields";
            this.grdGENSearchEngineSelectedFields.Size = new System.Drawing.Size(374, 373);
            this.grdGENSearchEngineSelectedFields.TabIndex = 1;
            this.grdGENSearchEngineSelectedFields.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdGENSearchEngineSelectedFields;
            this.gridView1.Name = "gridView1";
            // 
            // grdFields
            // 
            this.grdFields.Controls.Add(this.cmdUpdate);
            this.grdFields.Controls.Add(this.txbDisplayName);
            this.grdFields.Controls.Add(this.txtDisplayName);
            this.grdFields.Controls.Add(this.txbFieldName);
            this.grdFields.Controls.Add(this.txtFieldName);
            this.grdFields.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdFields.Location = new System.Drawing.Point(2, 22);
            this.grdFields.Name = "grdFields";
            this.grdFields.ShowCaption = false;
            this.grdFields.Size = new System.Drawing.Size(417, 55);
            this.grdFields.TabIndex = 0;
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Location = new System.Drawing.Point(308, 24);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(104, 23);
            this.cmdUpdate.TabIndex = 4;
            this.cmdUpdate.Text = "Update";
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // txbDisplayName
            // 
            this.txbDisplayName.Location = new System.Drawing.Point(148, 8);
            this.txbDisplayName.Name = "txbDisplayName";
            this.txbDisplayName.Size = new System.Drawing.Size(64, 13);
            this.txbDisplayName.TabIndex = 3;
            this.txbDisplayName.Text = "Display Name";
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Location = new System.Drawing.Point(146, 25);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(156, 20);
            this.txtDisplayName.TabIndex = 2;
            // 
            // txbFieldName
            // 
            this.txbFieldName.Location = new System.Drawing.Point(7, 8);
            this.txbFieldName.Name = "txbFieldName";
            this.txbFieldName.Size = new System.Drawing.Size(52, 13);
            this.txbFieldName.TabIndex = 1;
            this.txbFieldName.Text = "Field Name";
            // 
            // txtFieldName
            // 
            this.txtFieldName.Enabled = false;
            this.txtFieldName.Location = new System.Drawing.Point(5, 25);
            this.txtFieldName.Name = "txtFieldName";
            this.txtFieldName.Size = new System.Drawing.Size(135, 20);
            this.txtFieldName.TabIndex = 0;
            // 
            // cmdRemove
            // 
            this.cmdRemove.Location = new System.Drawing.Point(325, 280);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(47, 23);
            this.cmdRemove.TabIndex = 10;
            this.cmdRemove.Text = "<";
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(325, 257);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(47, 23);
            this.cmdAdd.TabIndex = 9;
            this.cmdAdd.Text = ">";
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // grpAvailableFields
            // 
            this.grpAvailableFields.Controls.Add(this.grdGENSearchEngineAvailableFields);
            this.grpAvailableFields.Dock = System.Windows.Forms.DockStyle.Left;
            this.grpAvailableFields.Location = new System.Drawing.Point(2, 44);
            this.grpAvailableFields.Name = "grpAvailableFields";
            this.grpAvailableFields.Size = new System.Drawing.Size(320, 452);
            this.grpAvailableFields.TabIndex = 8;
            this.grpAvailableFields.Text = "Available Fields";
            // 
            // grdGENSearchEngineAvailableFields
            // 
            this.grdGENSearchEngineAvailableFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdGENSearchEngineAvailableFields.Location = new System.Drawing.Point(2, 22);
            this.grdGENSearchEngineAvailableFields.MainView = this.gridView2;
            this.grdGENSearchEngineAvailableFields.Name = "grdGENSearchEngineAvailableFields";
            this.grdGENSearchEngineAvailableFields.Size = new System.Drawing.Size(316, 428);
            this.grdGENSearchEngineAvailableFields.TabIndex = 2;
            this.grdGENSearchEngineAvailableFields.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.grdGENSearchEngineAvailableFields;
            this.gridView2.Name = "gridView2";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txbSearchObject);
            this.groupControl1.Controls.Add(this.cboSearchObject);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(2, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(793, 42);
            this.groupControl1.TabIndex = 7;
            this.groupControl1.Text = "groupControl1";
            // 
            // txbSearchObject
            // 
            this.txbSearchObject.Location = new System.Drawing.Point(11, 15);
            this.txbSearchObject.Name = "txbSearchObject";
            this.txbSearchObject.Size = new System.Drawing.Size(68, 13);
            this.txbSearchObject.TabIndex = 6;
            this.txbSearchObject.Text = "Search Object";
            // 
            // cboSearchObject
            // 
            this.cboSearchObject.Location = new System.Drawing.Point(111, 12);
            this.cboSearchObject.Name = "cboSearchObject";
            this.cboSearchObject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSearchObject.Properties.NullText = "";
            this.cboSearchObject.Size = new System.Drawing.Size(209, 20);
            this.cboSearchObject.TabIndex = 1;
            this.cboSearchObject.EditValueChanged += new System.EventHandler(this.cboSearchObject_EditValueChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 498);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cmdOk);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cmdClose);
            this.splitContainer1.Size = new System.Drawing.Size(797, 50);
            this.splitContainer1.SplitterDistance = 381;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 16;
            // 
            // cmdOk
            // 
            this.cmdOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdOk.Location = new System.Drawing.Point(0, 0);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(381, 50);
            this.cmdOk.TabIndex = 4;
            this.cmdOk.Text = "Save";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdClose.Location = new System.Drawing.Point(0, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(415, 50);
            this.cmdClose.TabIndex = 5;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // frmGENSearchEngine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 548);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmGENSearchEngine";
            this.Text = "Search Engine";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmGENSearchEngine_FormClosing);
            this.Load += new System.EventHandler(this.frmGENSearchEngine_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpSelectedFields)).EndInit();
            this.grpSelectedFields.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdGENSearchEngineSelectedFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFields)).EndInit();
            this.grdFields.ResumeLayout(false);
            this.grdFields.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDisplayName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFieldName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAvailableFields)).EndInit();
            this.grpAvailableFields.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdGENSearchEngineAvailableFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboSearchObject.Properties)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl txbSearchObject;
        private DevExpress.XtraEditors.LookUpEdit cboSearchObject;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SimpleButton cmdOk;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl grpAvailableFields;
        private DevExpress.XtraEditors.SimpleButton cmdRemove;
        private DevExpress.XtraEditors.SimpleButton cmdAdd;
        private DevExpress.XtraEditors.GroupControl grpSelectedFields;
        private DevExpress.XtraEditors.GroupControl grdFields;
        private DevExpress.XtraGrid.GridControl grdGENSearchEngineSelectedFields;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.GridControl grdGENSearchEngineAvailableFields;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.LabelControl txbDisplayName;
        private DevExpress.XtraEditors.TextEdit txtDisplayName;
        private DevExpress.XtraEditors.LabelControl txbFieldName;
        private DevExpress.XtraEditors.TextEdit txtFieldName;
        private DevExpress.XtraEditors.SimpleButton cmdUpdate;
        private DevExpress.XtraEditors.SimpleButton cmdDown;
        private DevExpress.XtraEditors.SimpleButton cmdUp;
    }
}