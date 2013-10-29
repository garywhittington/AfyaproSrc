namespace AfyaPro_NextGen
{
    partial class frmIVSuggestStock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIVSuggestStock));
            this.grdExpiryDates = new DevExpress.XtraGrid.GridControl();
            this.viewExpiryDates = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.expirydate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtexpirydate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.packaging = new DevExpress.XtraGrid.Columns.GridColumn();
            this.balance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.quantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtquantityeditor = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cmdRemove = new DevExpress.XtraEditors.SimpleButton();
            this.cmdAdd = new DevExpress.XtraEditors.SimpleButton();
            this.txtDate = new DevExpress.XtraEditors.DateEdit();
            this.cmdSuggest = new DevExpress.XtraEditors.SimpleButton();
            this.txtQuantity = new DevExpress.XtraEditors.TextEdit();
            this.txtItemDescription = new DevExpress.XtraEditors.TextEdit();
            this.txtItemCode = new DevExpress.XtraEditors.TextEdit();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            this.cmdAccept = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbSuggest = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.grdExpiryDates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewExpiryDates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtexpirydate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtexpirydate.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtquantityeditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbSuggest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            this.SuspendLayout();
            // 
            // grdExpiryDates
            // 
            this.grdExpiryDates.Location = new System.Drawing.Point(12, 100);
            this.grdExpiryDates.MainView = this.viewExpiryDates;
            this.grdExpiryDates.Name = "grdExpiryDates";
            this.grdExpiryDates.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.txtexpirydate,
            this.txtquantityeditor});
            this.grdExpiryDates.Size = new System.Drawing.Size(351, 273);
            this.grdExpiryDates.TabIndex = 0;
            this.grdExpiryDates.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewExpiryDates,
            this.gridView2});
            // 
            // viewExpiryDates
            // 
            this.viewExpiryDates.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.expirydate,
            this.packaging,
            this.balance,
            this.quantity});
            this.viewExpiryDates.GridControl = this.grdExpiryDates;
            this.viewExpiryDates.Name = "viewExpiryDates";
            this.viewExpiryDates.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.viewExpiryDates.OptionsView.ShowGroupPanel = false;
            this.viewExpiryDates.OptionsView.ShowIndicator = false;
            this.viewExpiryDates.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.viewExpiryDates_ValidatingEditor);
            // 
            // expirydate
            // 
            this.expirydate.Caption = "Expiry Date";
            this.expirydate.ColumnEdit = this.txtexpirydate;
            this.expirydate.FieldName = "expirydate";
            this.expirydate.Name = "expirydate";
            this.expirydate.Visible = true;
            this.expirydate.VisibleIndex = 0;
            this.expirydate.Width = 81;
            // 
            // txtexpirydate
            // 
            this.txtexpirydate.AutoHeight = false;
            this.txtexpirydate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtexpirydate.Name = "txtexpirydate";
            this.txtexpirydate.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // packaging
            // 
            this.packaging.Caption = "Unit";
            this.packaging.FieldName = "packaging";
            this.packaging.Name = "packaging";
            this.packaging.OptionsColumn.AllowEdit = false;
            this.packaging.Visible = true;
            this.packaging.VisibleIndex = 1;
            this.packaging.Width = 87;
            // 
            // balance
            // 
            this.balance.Caption = "Balance";
            this.balance.FieldName = "balance";
            this.balance.Name = "balance";
            this.balance.OptionsColumn.AllowEdit = false;
            this.balance.Visible = true;
            this.balance.VisibleIndex = 2;
            this.balance.Width = 87;
            // 
            // quantity
            // 
            this.quantity.Caption = "Suggested Qty";
            this.quantity.ColumnEdit = this.txtquantityeditor;
            this.quantity.FieldName = "quantity";
            this.quantity.Name = "quantity";
            this.quantity.Visible = true;
            this.quantity.VisibleIndex = 3;
            this.quantity.Width = 92;
            // 
            // txtquantityeditor
            // 
            this.txtquantityeditor.AutoHeight = false;
            this.txtquantityeditor.Name = "txtquantityeditor";
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.grdExpiryDates;
            this.gridView2.Name = "gridView2";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cmdRemove);
            this.layoutControl1.Controls.Add(this.cmdAdd);
            this.layoutControl1.Controls.Add(this.txtDate);
            this.layoutControl1.Controls.Add(this.cmdSuggest);
            this.layoutControl1.Controls.Add(this.txtQuantity);
            this.layoutControl1.Controls.Add(this.txtItemDescription);
            this.layoutControl1.Controls.Add(this.txtItemCode);
            this.layoutControl1.Controls.Add(this.cmdClose);
            this.layoutControl1.Controls.Add(this.cmdAccept);
            this.layoutControl1.Controls.Add(this.grdExpiryDates);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(375, 423);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cmdRemove
            // 
            this.cmdRemove.Location = new System.Drawing.Point(299, 74);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(64, 22);
            this.cmdRemove.StyleController = this.layoutControl1;
            this.cmdRemove.TabIndex = 10;
            this.cmdRemove.Text = "Remove";
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(232, 74);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(63, 22);
            this.cmdAdd.StyleController = this.layoutControl1;
            this.cmdAdd.TabIndex = 5;
            this.cmdAdd.Text = "Add";
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // txtDate
            // 
            this.txtDate.EditValue = null;
            this.txtDate.Location = new System.Drawing.Point(12, 76);
            this.txtDate.Name = "txtDate";
            this.txtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDate.Size = new System.Drawing.Size(121, 20);
            this.txtDate.StyleController = this.layoutControl1;
            this.txtDate.TabIndex = 2;
            // 
            // cmdSuggest
            // 
            this.cmdSuggest.Location = new System.Drawing.Point(12, 377);
            this.cmdSuggest.Name = "cmdSuggest";
            this.cmdSuggest.Size = new System.Drawing.Size(153, 34);
            this.cmdSuggest.StyleController = this.layoutControl1;
            this.cmdSuggest.TabIndex = 9;
            this.cmdSuggest.Text = "Suggest Stock to Issue";
            this.cmdSuggest.Click += new System.EventHandler(this.cmdSuggest_Click);
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(137, 76);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(91, 20);
            this.txtQuantity.StyleController = this.layoutControl1;
            this.txtQuantity.TabIndex = 8;
            this.txtQuantity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuantity_KeyDown);
            // 
            // txtItemDescription
            // 
            this.txtItemDescription.Location = new System.Drawing.Point(94, 36);
            this.txtItemDescription.Name = "txtItemDescription";
            this.txtItemDescription.Properties.ReadOnly = true;
            this.txtItemDescription.Size = new System.Drawing.Size(269, 20);
            this.txtItemDescription.StyleController = this.layoutControl1;
            this.txtItemDescription.TabIndex = 7;
            // 
            // txtItemCode
            // 
            this.txtItemCode.Location = new System.Drawing.Point(94, 12);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Properties.ReadOnly = true;
            this.txtItemCode.Size = new System.Drawing.Size(102, 20);
            this.txtItemCode.StyleController = this.layoutControl1;
            this.txtItemCode.TabIndex = 6;
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(272, 377);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(91, 34);
            this.cmdClose.StyleController = this.layoutControl1;
            this.cmdClose.TabIndex = 5;
            this.cmdClose.Text = "Cancel";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdAccept
            // 
            this.cmdAccept.Location = new System.Drawing.Point(169, 377);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(99, 34);
            this.cmdAccept.StyleController = this.layoutControl1;
            this.cmdAccept.TabIndex = 4;
            this.cmdAccept.Text = "Accept";
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.emptySpaceItem1,
            this.layoutControlItem6,
            this.txbSuggest,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.emptySpaceItem2,
            this.layoutControlItem9});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(375, 423);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdExpiryDates;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 88);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(355, 277);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmdAccept;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(157, 365);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(82, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(103, 38);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cmdClose;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(260, 365);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(82, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(95, 38);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtItemCode;
            this.layoutControlItem4.CustomizationFormText = "Item Code";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(188, 24);
            this.layoutControlItem4.Text = "Item Code";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(78, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtItemDescription;
            this.layoutControlItem5.CustomizationFormText = "Item Description";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(355, 24);
            this.layoutControlItem5.Text = "Item Description";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(78, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(188, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(167, 24);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtQuantity;
            this.layoutControlItem6.CustomizationFormText = "Quantity";
            this.layoutControlItem6.Location = new System.Drawing.Point(125, 48);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(95, 40);
            this.layoutControlItem6.Text = "Quantity";
            this.layoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(78, 13);
            // 
            // txbSuggest
            // 
            this.txbSuggest.Control = this.cmdSuggest;
            this.txbSuggest.CustomizationFormText = "txbSuggest";
            this.txbSuggest.Location = new System.Drawing.Point(0, 365);
            this.txbSuggest.MinSize = new System.Drawing.Size(54, 26);
            this.txbSuggest.Name = "txbSuggest";
            this.txbSuggest.Size = new System.Drawing.Size(157, 38);
            this.txbSuggest.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.txbSuggest.Text = "txbSuggest";
            this.txbSuggest.TextSize = new System.Drawing.Size(0, 0);
            this.txbSuggest.TextToControlDistance = 0;
            this.txbSuggest.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.txtDate;
            this.layoutControlItem7.CustomizationFormText = "Expiry Date";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(125, 40);
            this.layoutControlItem7.Text = "Expiry Date";
            this.layoutControlItem7.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(78, 13);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.cmdAdd;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(220, 62);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(67, 26);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(220, 48);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(135, 14);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.cmdRemove;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(287, 62);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(68, 26);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // frmIVSuggestStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 423);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmIVSuggestStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Stock to Issue";
            this.Load += new System.EventHandler(this.frmIVSuggestStock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdExpiryDates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewExpiryDates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtexpirydate.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtexpirydate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtquantityeditor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbSuggest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView viewExpiryDates;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
        private DevExpress.XtraEditors.SimpleButton cmdAccept;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn expirydate;
        private DevExpress.XtraGrid.Columns.GridColumn balance;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit txtexpirydate;
        private DevExpress.XtraGrid.Columns.GridColumn quantity;
        internal DevExpress.XtraEditors.TextEdit txtItemDescription;
        internal DevExpress.XtraEditors.TextEdit txtItemCode;
        internal DevExpress.XtraGrid.GridControl grdExpiryDates;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        internal DevExpress.XtraEditors.TextEdit txtQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn packaging;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtquantityeditor;
        private DevExpress.XtraEditors.SimpleButton cmdSuggest;
        private DevExpress.XtraLayout.LayoutControlItem txbSuggest;
        private DevExpress.XtraEditors.DateEdit txtDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.SimpleButton cmdAdd;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.SimpleButton cmdRemove;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
    }
}