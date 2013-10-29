namespace AfyaPro_NextGen
{
    partial class frmIVExpiryDates
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIVExpiryDates));
            this.txtDate = new DevExpress.XtraEditors.DateEdit();
            this.txbDate = new DevExpress.XtraEditors.LabelControl();
            this.txbQuantity = new DevExpress.XtraEditors.LabelControl();
            this.txtQuantity = new DevExpress.XtraEditors.TextEdit();
            this.cmdAdd = new DevExpress.XtraEditors.SimpleButton();
            this.cmdRemove = new DevExpress.XtraEditors.SimpleButton();
            this.grdExpiryDates = new DevExpress.XtraGrid.GridControl();
            this.viewExpiryDate = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.itemorderid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.productcode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.expirydate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtexpirydate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.quantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.txtItemCode = new DevExpress.XtraEditors.TextEdit();
            this.txbItemCode = new DevExpress.XtraEditors.LabelControl();
            this.txbItemDescription = new DevExpress.XtraEditors.LabelControl();
            this.txtItemDescription = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExpiryDates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewExpiryDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtexpirydate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtexpirydate.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemDescription.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDate
            // 
            this.txtDate.EditValue = null;
            this.txtDate.Location = new System.Drawing.Point(3, 71);
            this.txtDate.Name = "txtDate";
            this.txtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDate.Size = new System.Drawing.Size(113, 20);
            this.txtDate.TabIndex = 0;
            this.txtDate.EditValueChanged += new System.EventHandler(this.txtDate_EditValueChanged);
            // 
            // txbDate
            // 
            this.txbDate.Location = new System.Drawing.Point(4, 53);
            this.txbDate.Name = "txbDate";
            this.txbDate.Size = new System.Drawing.Size(23, 13);
            this.txbDate.TabIndex = 1;
            this.txbDate.Text = "Date";
            // 
            // txbQuantity
            // 
            this.txbQuantity.Location = new System.Drawing.Point(119, 53);
            this.txbQuantity.Name = "txbQuantity";
            this.txbQuantity.Size = new System.Drawing.Size(42, 13);
            this.txbQuantity.TabIndex = 2;
            this.txbQuantity.Text = "Quantity";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(120, 71);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(51, 20);
            this.txtQuantity.TabIndex = 3;
            this.txtQuantity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuantity_KeyDown);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(176, 69);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(52, 24);
            this.cmdAdd.TabIndex = 4;
            this.cmdAdd.Text = "Add";
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdRemove
            // 
            this.cmdRemove.Location = new System.Drawing.Point(231, 69);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(52, 24);
            this.cmdRemove.TabIndex = 6;
            this.cmdRemove.Text = "Delete";
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // grdExpiryDates
            // 
            this.grdExpiryDates.Location = new System.Drawing.Point(3, 99);
            this.grdExpiryDates.MainView = this.viewExpiryDate;
            this.grdExpiryDates.Name = "grdExpiryDates";
            this.grdExpiryDates.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.txtexpirydate});
            this.grdExpiryDates.Size = new System.Drawing.Size(280, 145);
            this.grdExpiryDates.TabIndex = 7;
            this.grdExpiryDates.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewExpiryDate});
            // 
            // viewExpiryDate
            // 
            this.viewExpiryDate.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.itemorderid,
            this.productcode,
            this.expirydate,
            this.quantity});
            this.viewExpiryDate.GridControl = this.grdExpiryDates;
            this.viewExpiryDate.Name = "viewExpiryDate";
            this.viewExpiryDate.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.viewExpiryDate.OptionsView.ShowFooter = true;
            this.viewExpiryDate.OptionsView.ShowGroupPanel = false;
            this.viewExpiryDate.OptionsView.ShowIndicator = false;
            // 
            // itemorderid
            // 
            this.itemorderid.Caption = "itemorderid";
            this.itemorderid.FieldName = "itemorderid";
            this.itemorderid.Name = "itemorderid";
            // 
            // productcode
            // 
            this.productcode.Caption = "productcode";
            this.productcode.FieldName = "productcode";
            this.productcode.Name = "productcode";
            // 
            // expirydate
            // 
            this.expirydate.Caption = "Expiry Date";
            this.expirydate.ColumnEdit = this.txtexpirydate;
            this.expirydate.FieldName = "expirydate";
            this.expirydate.Name = "expirydate";
            this.expirydate.Visible = true;
            this.expirydate.VisibleIndex = 0;
            this.expirydate.Width = 219;
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
            // quantity
            // 
            this.quantity.Caption = "Qty Rcv";
            this.quantity.FieldName = "quantity";
            this.quantity.Name = "quantity";
            this.quantity.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.quantity.Visible = true;
            this.quantity.VisibleIndex = 1;
            this.quantity.Width = 112;
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(205, 250);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(78, 27);
            this.cmdOk.TabIndex = 8;
            this.cmdOk.Text = "Ok";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // txtItemCode
            // 
            this.txtItemCode.Enabled = false;
            this.txtItemCode.Location = new System.Drawing.Point(3, 26);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(79, 20);
            this.txtItemCode.TabIndex = 10;
            // 
            // txbItemCode
            // 
            this.txbItemCode.Location = new System.Drawing.Point(3, 9);
            this.txbItemCode.Name = "txbItemCode";
            this.txbItemCode.Size = new System.Drawing.Size(50, 13);
            this.txbItemCode.TabIndex = 11;
            this.txbItemCode.Text = "Item Code";
            // 
            // txbItemDescription
            // 
            this.txbItemDescription.Location = new System.Drawing.Point(88, 9);
            this.txbItemDescription.Name = "txbItemDescription";
            this.txbItemDescription.Size = new System.Drawing.Size(78, 13);
            this.txbItemDescription.TabIndex = 13;
            this.txbItemDescription.Text = "Item Description";
            // 
            // txtItemDescription
            // 
            this.txtItemDescription.Enabled = false;
            this.txtItemDescription.Location = new System.Drawing.Point(88, 26);
            this.txtItemDescription.Name = "txtItemDescription";
            this.txtItemDescription.Size = new System.Drawing.Size(195, 20);
            this.txtItemDescription.TabIndex = 12;
            // 
            // frmIVExpiryDates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 283);
            this.Controls.Add(this.txbItemDescription);
            this.Controls.Add(this.txtItemDescription);
            this.Controls.Add(this.txbItemCode);
            this.Controls.Add(this.txtItemCode);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.grdExpiryDates);
            this.Controls.Add(this.cmdRemove);
            this.Controls.Add(this.cmdAdd);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.txbQuantity);
            this.Controls.Add(this.txbDate);
            this.Controls.Add(this.txtDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmIVExpiryDates";
            this.Text = "Product Expiry Dates";
            this.Load += new System.EventHandler(this.frmIVExpiryDates_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExpiryDates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewExpiryDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtexpirydate.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtexpirydate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemDescription.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit txtDate;
        private DevExpress.XtraEditors.LabelControl txbDate;
        private DevExpress.XtraEditors.LabelControl txbQuantity;
        private DevExpress.XtraEditors.TextEdit txtQuantity;
        private DevExpress.XtraEditors.SimpleButton cmdAdd;
        private DevExpress.XtraEditors.SimpleButton cmdRemove;
        private DevExpress.XtraGrid.GridControl grdExpiryDates;
        private DevExpress.XtraGrid.Views.Grid.GridView viewExpiryDate;
        private DevExpress.XtraGrid.Columns.GridColumn itemorderid;
        private DevExpress.XtraGrid.Columns.GridColumn productcode;
        private DevExpress.XtraGrid.Columns.GridColumn expirydate;
        private DevExpress.XtraGrid.Columns.GridColumn quantity;
        private DevExpress.XtraEditors.SimpleButton cmdOk;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit txtexpirydate;
        private DevExpress.XtraEditors.LabelControl txbItemCode;
        private DevExpress.XtraEditors.LabelControl txbItemDescription;
        internal DevExpress.XtraEditors.TextEdit txtItemCode;
        internal DevExpress.XtraEditors.TextEdit txtItemDescription;
    }
}