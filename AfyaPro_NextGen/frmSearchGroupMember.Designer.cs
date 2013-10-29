namespace AfyaPro_NextGen
{
    partial class frmSearchGroupMember
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
            this.grpFilter = new DevExpress.XtraEditors.GroupControl();
            this.txbGroup = new DevExpress.XtraEditors.LabelControl();
            this.cboGroup = new DevExpress.XtraEditors.LookUpEdit();
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
            ((System.ComponentModel.ISupportInitialize)(this.grpFilter)).BeginInit();
            this.grpFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboGroup.Properties)).BeginInit();
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
            this.groupControl1.Controls.Add(this.grpFilter);
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
            this.groupControl1.Size = new System.Drawing.Size(650, 144);
            this.groupControl1.TabIndex = 0;
            // 
            // grpFilter
            // 
            this.grpFilter.Controls.Add(this.txbGroup);
            this.grpFilter.Controls.Add(this.cboGroup);
            this.grpFilter.Location = new System.Drawing.Point(175, 5);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Size = new System.Drawing.Size(180, 124);
            this.grpFilter.TabIndex = 17;
            this.grpFilter.Text = "Filter";
            // 
            // txbGroup
            // 
            this.txbGroup.Location = new System.Drawing.Point(8, 46);
            this.txbGroup.Name = "txbGroup";
            this.txbGroup.Size = new System.Drawing.Size(29, 13);
            this.txbGroup.TabIndex = 17;
            this.txbGroup.Text = "Group";
            // 
            // cboGroup
            // 
            this.cboGroup.Location = new System.Drawing.Point(8, 65);
            this.cboGroup.Name = "cboGroup";
            this.cboGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboGroup.Properties.NullText = "";
            this.cboGroup.Size = new System.Drawing.Size(163, 20);
            this.cboGroup.TabIndex = 16;
            this.cboGroup.EditValueChanged += new System.EventHandler(this.cboGroup_EditValueChanged);
            // 
            // chkSearchWhileTyping
            // 
            this.chkSearchWhileTyping.Location = new System.Drawing.Point(3, 108);
            this.chkSearchWhileTyping.Name = "chkSearchWhileTyping";
            this.chkSearchWhileTyping.Properties.Caption = "Do search while typing";
            this.chkSearchWhileTyping.Size = new System.Drawing.Size(165, 19);
            this.chkSearchWhileTyping.TabIndex = 1;
            this.chkSearchWhileTyping.CheckedChanged += new System.EventHandler(this.chkSearchWhileTyping_CheckedChanged);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(556, 66);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(91, 64);
            this.cmdClose.TabIndex = 13;
            this.cmdClose.Text = "Close";
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(556, 2);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(91, 64);
            this.cmdOk.TabIndex = 12;
            this.cmdOk.Text = "Search";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.radOptions);
            this.grpOptions.Location = new System.Drawing.Point(361, 4);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(190, 125);
            this.grpOptions.TabIndex = 11;
            this.grpOptions.Text = "Search Options";
            // 
            // radOptions
            // 
            this.radOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radOptions.Location = new System.Drawing.Point(2, 22);
            this.radOptions.Name = "radOptions";
            this.radOptions.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Search text Anywhere"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Beginning with Search Text"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Ending with Search Text"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Match exact search text")});
            this.radOptions.Size = new System.Drawing.Size(186, 101);
            this.radOptions.TabIndex = 0;
            this.radOptions.SelectedIndexChanged += new System.EventHandler(this.radOptions_SelectedIndexChanged);
            // 
            // txbSearchText
            // 
            this.txbSearchText.Location = new System.Drawing.Point(7, 58);
            this.txbSearchText.Name = "txbSearchText";
            this.txbSearchText.Size = new System.Drawing.Size(58, 13);
            this.txbSearchText.TabIndex = 10;
            this.txbSearchText.Text = "Search Text";
            // 
            // txtSearchText
            // 
            this.txtSearchText.Location = new System.Drawing.Point(5, 75);
            this.txtSearchText.Name = "txtSearchText";
            this.txtSearchText.Size = new System.Drawing.Size(163, 20);
            this.txtSearchText.TabIndex = 9;
            this.txtSearchText.EditValueChanged += new System.EventHandler(this.txtSearchText_EditValueChanged);
            this.txtSearchText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchText_KeyDown);
            // 
            // txbSearchField
            // 
            this.txbSearchField.Location = new System.Drawing.Point(5, 10);
            this.txbSearchField.Name = "txbSearchField";
            this.txbSearchField.Size = new System.Drawing.Size(58, 13);
            this.txbSearchField.TabIndex = 8;
            this.txbSearchField.Text = "Search Field";
            // 
            // cboSearchField
            // 
            this.cboSearchField.Location = new System.Drawing.Point(5, 29);
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
            this.grdSearch.Location = new System.Drawing.Point(0, 144);
            this.grdSearch.MainView = this.gridView1;
            this.grdSearch.Name = "grdSearch";
            this.grdSearch.Size = new System.Drawing.Size(650, 321);
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
            // frmSearchGroupMember
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 465);
            this.Controls.Add(this.grdSearch);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSearchGroupMember";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search Group Member";
            this.Load += new System.EventHandler(this.frmSearchGroupMember_Load);
            this.Activated += new System.EventHandler(this.frmSearchGroupMember_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSearchGroupMember_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpFilter)).EndInit();
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboGroup.Properties)).EndInit();
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
        private DevExpress.XtraEditors.LookUpEdit cboGroup;
        private DevExpress.XtraEditors.GroupControl grpFilter;
        private DevExpress.XtraEditors.LabelControl txbGroup;
    }
}