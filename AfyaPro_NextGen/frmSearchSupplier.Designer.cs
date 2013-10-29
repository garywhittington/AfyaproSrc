namespace AfyaPro_NextGen
{
    partial class frmSearchSupplier
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.chkSearchWhileTyping = new DevExpress.XtraEditors.CheckEdit();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            this.cmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.grpOptions = new DevExpress.XtraEditors.GroupControl();
            this.radOptions = new DevExpress.XtraEditors.RadioGroup();
            this.txbSearchText = new DevExpress.XtraEditors.LabelControl();
            this.txtSearchText = new DevExpress.XtraEditors.TextEdit();
            this.txbSearchField = new DevExpress.XtraEditors.LabelControl();
            this.cboSearchField = new DevExpress.XtraEditors.LookUpEdit();
            this.grdSearch = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkSearchWhileTyping.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpOptions)).BeginInit();
            this.grpOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radOptions.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSearchField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.chkSearchWhileTyping);
            this.groupControl1.Controls.Add(this.cmdClose);
            this.groupControl1.Controls.Add(this.cmdOk);
            this.groupControl1.Controls.Add(this.grpOptions);
            this.groupControl1.Controls.Add(this.txbSearchText);
            this.groupControl1.Controls.Add(this.txtSearchText);
            this.groupControl1.Controls.Add(this.txbSearchField);
            this.groupControl1.Controls.Add(this.cboSearchField);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(589, 135);
            this.groupControl1.TabIndex = 0;
            // 
            // chkSearchWhileTyping
            // 
            this.chkSearchWhileTyping.Location = new System.Drawing.Point(3, 105);
            this.chkSearchWhileTyping.Name = "chkSearchWhileTyping";
            this.chkSearchWhileTyping.Properties.Caption = "Do search while typing";
            this.chkSearchWhileTyping.Size = new System.Drawing.Size(165, 19);
            this.chkSearchWhileTyping.TabIndex = 1;
            this.chkSearchWhileTyping.CheckedChanged += new System.EventHandler(this.chkSearchWhileTyping_CheckedChanged);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(391, 70);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(110, 60);
            this.cmdClose.TabIndex = 13;
            this.cmdClose.Text = "Close";
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(391, 4);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(110, 60);
            this.cmdOk.TabIndex = 12;
            this.cmdOk.Text = "Search";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.radOptions);
            this.grpOptions.Location = new System.Drawing.Point(174, 5);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(214, 125);
            this.grpOptions.TabIndex = 11;
            this.grpOptions.Text = "Search Options";
            // 
            // radOptions
            // 
            this.radOptions.Location = new System.Drawing.Point(5, 23);
            this.radOptions.Name = "radOptions";
            this.radOptions.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Search text Anywhere"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Beginning with Search Text"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Ending with Search Text"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Match exact search text")});
            this.radOptions.Size = new System.Drawing.Size(204, 97);
            this.radOptions.TabIndex = 0;
            this.radOptions.SelectedIndexChanged += new System.EventHandler(this.radOptions_SelectedIndexChanged);
            // 
            // txbSearchText
            // 
            this.txbSearchText.Location = new System.Drawing.Point(7, 52);
            this.txbSearchText.Name = "txbSearchText";
            this.txbSearchText.Size = new System.Drawing.Size(58, 13);
            this.txbSearchText.TabIndex = 10;
            this.txbSearchText.Text = "Search Text";
            // 
            // txtSearchText
            // 
            this.txtSearchText.Location = new System.Drawing.Point(5, 69);
            this.txtSearchText.Name = "txtSearchText";
            this.txtSearchText.Size = new System.Drawing.Size(163, 20);
            this.txtSearchText.TabIndex = 9;
            this.txtSearchText.EditValueChanged += new System.EventHandler(this.txtSearchText_EditValueChanged);
            this.txtSearchText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchText_KeyDown);
            // 
            // txbSearchField
            // 
            this.txbSearchField.Location = new System.Drawing.Point(5, 5);
            this.txbSearchField.Name = "txbSearchField";
            this.txbSearchField.Size = new System.Drawing.Size(58, 13);
            this.txbSearchField.TabIndex = 8;
            this.txbSearchField.Text = "Search Field";
            // 
            // cboSearchField
            // 
            this.cboSearchField.Location = new System.Drawing.Point(5, 24);
            this.cboSearchField.Name = "cboSearchField";
            this.cboSearchField.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSearchField.Properties.NullText = "";
            this.cboSearchField.Size = new System.Drawing.Size(163, 20);
            this.cboSearchField.TabIndex = 7;
            // 
            // grdSearch
            // 
            this.grdSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSearch.EmbeddedNavigator.Buttons.Append.Enabled = false;
            this.grdSearch.EmbeddedNavigator.Buttons.CancelEdit.Enabled = false;
            this.grdSearch.EmbeddedNavigator.Buttons.Edit.Enabled = false;
            this.grdSearch.EmbeddedNavigator.Buttons.EndEdit.Enabled = false;
            this.grdSearch.EmbeddedNavigator.Buttons.First.Enabled = false;
            this.grdSearch.EmbeddedNavigator.Buttons.Last.Enabled = false;
            this.grdSearch.EmbeddedNavigator.Buttons.Next.Enabled = false;
            this.grdSearch.EmbeddedNavigator.Buttons.NextPage.Enabled = false;
            this.grdSearch.EmbeddedNavigator.Buttons.Prev.Enabled = false;
            this.grdSearch.EmbeddedNavigator.Buttons.PrevPage.Enabled = false;
            this.grdSearch.Location = new System.Drawing.Point(0, 135);
            this.grdSearch.MainView = this.gridView1;
            this.grdSearch.Name = "grdSearch";
            this.grdSearch.Size = new System.Drawing.Size(589, 330);
            this.grdSearch.TabIndex = 1;
            this.grdSearch.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.grdSearch.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.grdSearch_ProcessGridKey);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdSearch;
            this.gridView1.Name = "gridView1";
            // 
            // frmSearchSupplier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 465);
            this.Controls.Add(this.grdSearch);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSearchSupplier";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search Supplier";
            this.Load += new System.EventHandler(this.frmSearchSupplier_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSearchSupplier_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkSearchWhileTyping.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpOptions)).EndInit();
            this.grpOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radOptions.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSearchField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl txbSearchField;
        private DevExpress.XtraEditors.LookUpEdit cboSearchField;
        private DevExpress.XtraEditors.GroupControl grpOptions;
        private DevExpress.XtraEditors.LabelControl txbSearchText;
        private DevExpress.XtraEditors.TextEdit txtSearchText;
        private DevExpress.XtraEditors.RadioGroup radOptions;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
        private DevExpress.XtraEditors.SimpleButton cmdOk;
        private DevExpress.XtraEditors.CheckEdit chkSearchWhileTyping;
        private DevExpress.XtraGrid.GridControl grdSearch;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}