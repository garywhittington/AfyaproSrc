namespace AfyaPro_NextGen
{
    partial class frmCUSCustomerGroups
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
            this.chkInActive = new DevExpress.XtraEditors.CheckEdit();
            this.grpProperties = new DevExpress.XtraEditors.GroupControl();
            this.chkGenerateInvoice = new DevExpress.XtraEditors.CheckEdit();
            this.grpDiagnoses = new DevExpress.XtraEditors.GroupControl();
            this.chkDiagnosesSensitive = new DevExpress.XtraEditors.CheckEdit();
            this.cmdDiagnoses = new DevExpress.XtraEditors.SimpleButton();
            this.chkPromptPayment = new DevExpress.XtraEditors.CheckEdit();
            this.grpPricing = new DevExpress.XtraEditors.GroupControl();
            this.chkItemsSensitive = new DevExpress.XtraEditors.CheckEdit();
            this.cmdItems = new DevExpress.XtraEditors.SimpleButton();
            this.cboPriceCategory = new DevExpress.XtraEditors.LookUpEdit();
            this.txbPriceCategory = new DevExpress.XtraEditors.LabelControl();
            this.chkStrictActivation = new DevExpress.XtraEditors.CheckEdit();
            this.txtCeilingAmount = new DevExpress.XtraEditors.TextEdit();
            this.txbCeilingAmount = new DevExpress.XtraEditors.LabelControl();
            this.chkHasCeiling = new DevExpress.XtraEditors.CheckEdit();
            this.chkIdSensitive = new DevExpress.XtraEditors.CheckEdit();
            this.chkHasSubGroups = new DevExpress.XtraEditors.CheckEdit();
            this.txtDescription = new DevExpress.XtraEditors.TextEdit();
            this.txbDescription = new DevExpress.XtraEditors.LabelControl();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txbCode = new DevExpress.XtraEditors.LabelControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtPayPercent = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkInActive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpProperties)).BeginInit();
            this.grpProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkGenerateInvoice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDiagnoses)).BeginInit();
            this.grpDiagnoses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkDiagnosesSensitive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPromptPayment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpPricing)).BeginInit();
            this.grpPricing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkItemsSensitive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPriceCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkStrictActivation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCeilingAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHasCeiling.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIdSensitive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHasSubGroups.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayPercent.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.chkInActive);
            this.groupControl2.Controls.Add(this.grpProperties);
            this.groupControl2.Controls.Add(this.txtDescription);
            this.groupControl2.Controls.Add(this.txbDescription);
            this.groupControl2.Controls.Add(this.txtCode);
            this.groupControl2.Controls.Add(this.txbCode);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(557, 403);
            this.groupControl2.TabIndex = 11;
            this.groupControl2.Text = "groupControl2";
            // 
            // chkInActive
            // 
            this.chkInActive.Location = new System.Drawing.Point(110, 78);
            this.chkInActive.Name = "chkInActive";
            this.chkInActive.Properties.Caption = "In-Active";
            this.chkInActive.Size = new System.Drawing.Size(193, 19);
            this.chkInActive.TabIndex = 9;
            // 
            // grpProperties
            // 
            this.grpProperties.Controls.Add(this.txtPayPercent);
            this.grpProperties.Controls.Add(this.labelControl1);
            this.grpProperties.Controls.Add(this.chkGenerateInvoice);
            this.grpProperties.Controls.Add(this.grpDiagnoses);
            this.grpProperties.Controls.Add(this.chkPromptPayment);
            this.grpProperties.Controls.Add(this.grpPricing);
            this.grpProperties.Controls.Add(this.chkStrictActivation);
            this.grpProperties.Controls.Add(this.txtCeilingAmount);
            this.grpProperties.Controls.Add(this.txbCeilingAmount);
            this.grpProperties.Controls.Add(this.chkHasCeiling);
            this.grpProperties.Controls.Add(this.chkIdSensitive);
            this.grpProperties.Controls.Add(this.chkHasSubGroups);
            this.grpProperties.Location = new System.Drawing.Point(9, 108);
            this.grpProperties.Name = "grpProperties";
            this.grpProperties.Size = new System.Drawing.Size(543, 289);
            this.grpProperties.TabIndex = 3;
            this.grpProperties.Text = "Properties";
            // 
            // chkGenerateInvoice
            // 
            this.chkGenerateInvoice.Location = new System.Drawing.Point(15, 157);
            this.chkGenerateInvoice.Name = "chkGenerateInvoice";
            this.chkGenerateInvoice.Properties.Caption = "Generate invoice when preparing outgoing bill";
            this.chkGenerateInvoice.Size = new System.Drawing.Size(357, 19);
            this.chkGenerateInvoice.TabIndex = 17;
            // 
            // grpDiagnoses
            // 
            this.grpDiagnoses.Controls.Add(this.chkDiagnosesSensitive);
            this.grpDiagnoses.Controls.Add(this.cmdDiagnoses);
            this.grpDiagnoses.Location = new System.Drawing.Point(379, 189);
            this.grpDiagnoses.Name = "grpDiagnoses";
            this.grpDiagnoses.Size = new System.Drawing.Size(159, 84);
            this.grpDiagnoses.TabIndex = 16;
            this.grpDiagnoses.Text = "Diagnoses";
            // 
            // chkDiagnosesSensitive
            // 
            this.chkDiagnosesSensitive.Location = new System.Drawing.Point(3, 28);
            this.chkDiagnosesSensitive.Name = "chkDiagnosesSensitive";
            this.chkDiagnosesSensitive.Properties.Caption = "Diagnoses Sensitive";
            this.chkDiagnosesSensitive.Size = new System.Drawing.Size(135, 19);
            this.chkDiagnosesSensitive.TabIndex = 18;
            // 
            // cmdDiagnoses
            // 
            this.cmdDiagnoses.Location = new System.Drawing.Point(5, 51);
            this.cmdDiagnoses.Name = "cmdDiagnoses";
            this.cmdDiagnoses.Size = new System.Drawing.Size(137, 23);
            this.cmdDiagnoses.TabIndex = 17;
            this.cmdDiagnoses.Text = "Diagnoses";
            this.cmdDiagnoses.Click += new System.EventHandler(this.cmdDiagnoses_Click);
            // 
            // chkPromptPayment
            // 
            this.chkPromptPayment.Location = new System.Drawing.Point(15, 132);
            this.chkPromptPayment.Name = "chkPromptPayment";
            this.chkPromptPayment.Properties.Caption = "Prompt Payment when Posting Bills";
            this.chkPromptPayment.Size = new System.Drawing.Size(202, 19);
            this.chkPromptPayment.TabIndex = 15;
            // 
            // grpPricing
            // 
            this.grpPricing.Controls.Add(this.chkItemsSensitive);
            this.grpPricing.Controls.Add(this.cmdItems);
            this.grpPricing.Controls.Add(this.cboPriceCategory);
            this.grpPricing.Controls.Add(this.txbPriceCategory);
            this.grpPricing.Location = new System.Drawing.Point(17, 189);
            this.grpPricing.Name = "grpPricing";
            this.grpPricing.Size = new System.Drawing.Size(355, 84);
            this.grpPricing.TabIndex = 14;
            this.grpPricing.Text = "Pricing";
            // 
            // chkItemsSensitive
            // 
            this.chkItemsSensitive.Location = new System.Drawing.Point(209, 27);
            this.chkItemsSensitive.Name = "chkItemsSensitive";
            this.chkItemsSensitive.Properties.Caption = "Items Sensitive";
            this.chkItemsSensitive.Size = new System.Drawing.Size(135, 19);
            this.chkItemsSensitive.TabIndex = 16;
            // 
            // cmdItems
            // 
            this.cmdItems.Location = new System.Drawing.Point(211, 50);
            this.cmdItems.Name = "cmdItems";
            this.cmdItems.Size = new System.Drawing.Size(137, 23);
            this.cmdItems.TabIndex = 13;
            this.cmdItems.Text = "Billing Items";
            this.cmdItems.Click += new System.EventHandler(this.cmdItems_Click);
            // 
            // cboPriceCategory
            // 
            this.cboPriceCategory.Location = new System.Drawing.Point(9, 52);
            this.cboPriceCategory.Name = "cboPriceCategory";
            this.cboPriceCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPriceCategory.Properties.NullText = "";
            this.cboPriceCategory.Size = new System.Drawing.Size(191, 20);
            this.cboPriceCategory.TabIndex = 11;
            // 
            // txbPriceCategory
            // 
            this.txbPriceCategory.Location = new System.Drawing.Point(9, 33);
            this.txbPriceCategory.Name = "txbPriceCategory";
            this.txbPriceCategory.Size = new System.Drawing.Size(45, 13);
            this.txbPriceCategory.TabIndex = 12;
            this.txbPriceCategory.Text = "Category";
            // 
            // chkStrictActivation
            // 
            this.chkStrictActivation.Location = new System.Drawing.Point(15, 84);
            this.chkStrictActivation.Name = "chkStrictActivation";
            this.chkStrictActivation.Properties.Caption = "Validate Ids";
            this.chkStrictActivation.Size = new System.Drawing.Size(261, 19);
            this.chkStrictActivation.TabIndex = 13;
            // 
            // txtCeilingAmount
            // 
            this.txtCeilingAmount.Location = new System.Drawing.Point(272, 107);
            this.txtCeilingAmount.Name = "txtCeilingAmount";
            this.txtCeilingAmount.Size = new System.Drawing.Size(100, 20);
            this.txtCeilingAmount.TabIndex = 10;
            // 
            // txbCeilingAmount
            // 
            this.txbCeilingAmount.Location = new System.Drawing.Point(191, 110);
            this.txbCeilingAmount.Name = "txbCeilingAmount";
            this.txbCeilingAmount.Size = new System.Drawing.Size(75, 13);
            this.txbCeilingAmount.TabIndex = 9;
            this.txbCeilingAmount.Text = "Ceiling Amount:";
            // 
            // chkHasCeiling
            // 
            this.chkHasCeiling.Location = new System.Drawing.Point(15, 107);
            this.chkHasCeiling.Name = "chkHasCeiling";
            this.chkHasCeiling.Properties.Caption = "Has Ceiling";
            this.chkHasCeiling.Size = new System.Drawing.Size(170, 19);
            this.chkHasCeiling.TabIndex = 8;
            // 
            // chkIdSensitive
            // 
            this.chkIdSensitive.Location = new System.Drawing.Point(15, 59);
            this.chkIdSensitive.Name = "chkIdSensitive";
            this.chkIdSensitive.Properties.Caption = "Has Ids";
            this.chkIdSensitive.Size = new System.Drawing.Size(261, 19);
            this.chkIdSensitive.TabIndex = 7;
            // 
            // chkHasSubGroups
            // 
            this.chkHasSubGroups.Location = new System.Drawing.Point(15, 34);
            this.chkHasSubGroups.Name = "chkHasSubGroups";
            this.chkHasSubGroups.Properties.Caption = "Has Sub Groups";
            this.chkHasSubGroups.Size = new System.Drawing.Size(261, 19);
            this.chkHasSubGroups.TabIndex = 4;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(112, 50);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(284, 20);
            this.txtDescription.TabIndex = 2;
            // 
            // txbDescription
            // 
            this.txbDescription.Location = new System.Drawing.Point(9, 53);
            this.txbDescription.Name = "txbDescription";
            this.txbDescription.Size = new System.Drawing.Size(53, 13);
            this.txbDescription.TabIndex = 3;
            this.txbDescription.Text = "Description";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(112, 22);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(100, 20);
            this.txtCode.TabIndex = 1;
            // 
            // txbCode
            // 
            this.txbCode.Location = new System.Drawing.Point(9, 25);
            this.txbCode.Name = "txbCode";
            this.txbCode.Size = new System.Drawing.Size(25, 13);
            this.txbCode.TabIndex = 1;
            this.txbCode.Text = "Code";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 403);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cmdOk);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cmdClose);
            this.splitContainer1.Size = new System.Drawing.Size(557, 35);
            this.splitContainer1.SplitterDistance = 292;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 13;
            // 
            // cmdOk
            // 
            this.cmdOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdOk.Location = new System.Drawing.Point(0, 0);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(292, 35);
            this.cmdOk.TabIndex = 7;
            this.cmdOk.Text = "Save";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdClose.Location = new System.Drawing.Point(0, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(264, 35);
            this.cmdClose.TabIndex = 8;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // txtPayPercent
            // 
            this.txtPayPercent.Location = new System.Drawing.Point(322, 133);
            this.txtPayPercent.Name = "txtPayPercent";
            this.txtPayPercent.Size = new System.Drawing.Size(50, 20);
            this.txtPayPercent.TabIndex = 19;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(223, 136);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(93, 13);
            this.labelControl1.TabIndex = 18;
            this.labelControl1.Text = "Percentage to Pay:";
            // 
            // frmCUSCustomerGroups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 438);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCUSCustomerGroups";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Customer Groups";
            this.Activated += new System.EventHandler(this.frmCUSCustomerGroups_Activated);
            this.Load += new System.EventHandler(this.frmCUSCustomerGroups_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkInActive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpProperties)).EndInit();
            this.grpProperties.ResumeLayout(false);
            this.grpProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkGenerateInvoice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDiagnoses)).EndInit();
            this.grpDiagnoses.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkDiagnosesSensitive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPromptPayment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpPricing)).EndInit();
            this.grpPricing.ResumeLayout(false);
            this.grpPricing.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkItemsSensitive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPriceCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkStrictActivation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCeilingAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHasCeiling.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIdSensitive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHasSubGroups.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtPayPercent.Properties)).EndInit();
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
        private DevExpress.XtraEditors.GroupControl grpProperties;
        private DevExpress.XtraEditors.CheckEdit chkHasSubGroups;
        private DevExpress.XtraEditors.CheckEdit chkInActive;
        private DevExpress.XtraEditors.CheckEdit chkHasCeiling;
        private DevExpress.XtraEditors.CheckEdit chkIdSensitive;
        private DevExpress.XtraEditors.TextEdit txtCeilingAmount;
        private DevExpress.XtraEditors.LabelControl txbCeilingAmount;
        private DevExpress.XtraEditors.LabelControl txbPriceCategory;
        private DevExpress.XtraEditors.LookUpEdit cboPriceCategory;
        private DevExpress.XtraEditors.CheckEdit chkStrictActivation;
        private DevExpress.XtraEditors.GroupControl grpPricing;
        private DevExpress.XtraEditors.CheckEdit chkPromptPayment;
        private DevExpress.XtraEditors.SimpleButton cmdItems;
        private DevExpress.XtraEditors.CheckEdit chkItemsSensitive;
        private DevExpress.XtraEditors.GroupControl grpDiagnoses;
        private DevExpress.XtraEditors.CheckEdit chkDiagnosesSensitive;
        private DevExpress.XtraEditors.SimpleButton cmdDiagnoses;
        private DevExpress.XtraEditors.CheckEdit chkGenerateInvoice;
        private DevExpress.XtraEditors.TextEdit txtPayPercent;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}