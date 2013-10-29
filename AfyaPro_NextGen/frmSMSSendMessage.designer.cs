namespace AfyaPro_NextGen
{
    partial class frmSMSSendMessage
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lstRecepients = new DevExpress.XtraEditors.ListBoxControl();
            this.rdbGroup = new DevExpress.XtraEditors.RadioGroup();
            this.btnRemove = new DevExpress.XtraEditors.SimpleButton();
            this.cboMessageCode = new DevExpress.XtraEditors.LookUpEdit();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            this.cmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.cboClientGroup = new DevExpress.XtraEditors.LookUpEdit();
            this.txtMessage = new DevExpress.XtraEditors.MemoEdit();
            this.cboMessageSource = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txbWard = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbRemarks = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.txbBedNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txbRoom = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstRecepients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdbGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMessageCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboClientGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMessageSource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbWard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbRemarks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbBedNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbRoom)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnAdd);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.lstRecepients);
            this.layoutControl1.Controls.Add(this.rdbGroup);
            this.layoutControl1.Controls.Add(this.btnRemove);
            this.layoutControl1.Controls.Add(this.cboMessageCode);
            this.layoutControl1.Controls.Add(this.cmdClose);
            this.layoutControl1.Controls.Add(this.cmdOk);
            this.layoutControl1.Controls.Add(this.cboClientGroup);
            this.layoutControl1.Controls.Add(this.txtMessage);
            this.layoutControl1.Controls.Add(this.cboMessageSource);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(458, 438);
            this.layoutControl1.TabIndex = 11;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(352, 190);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(94, 22);
            this.btnAdd.StyleController = this.layoutControl1;
            this.btnAdd.TabIndex = 17;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 190);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(85, 13);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 16;
            this.labelControl1.Text = "Recepient(s)        ";
            // 
            // lstRecepients
            // 
            this.lstRecepients.Location = new System.Drawing.Point(101, 190);
            this.lstRecepients.Name = "lstRecepients";
            this.lstRecepients.Size = new System.Drawing.Size(247, 154);
            this.lstRecepients.StyleController = this.layoutControl1;
            this.lstRecepients.TabIndex = 15;
            // 
            // rdbGroup
            // 
            this.rdbGroup.Location = new System.Drawing.Point(100, 12);
            this.rdbGroup.Name = "rdbGroup";
            this.rdbGroup.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Individuals"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Group"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Every one")});
            this.rdbGroup.Size = new System.Drawing.Size(346, 27);
            this.rdbGroup.StyleController = this.layoutControl1;
            this.rdbGroup.TabIndex = 12;
            this.rdbGroup.SelectedIndexChanged += new System.EventHandler(this.rdbGroup_SelectedIndexChanged);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(352, 216);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(94, 22);
            this.btnRemove.StyleController = this.layoutControl1;
            this.btnRemove.TabIndex = 18;
            this.btnRemove.Text = "Remove";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // cboMessageCode
            // 
            this.cboMessageCode.Location = new System.Drawing.Point(100, 91);
            this.cboMessageCode.Name = "cboMessageCode";
            this.cboMessageCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboMessageCode.Properties.NullText = "";
            this.cboMessageCode.Size = new System.Drawing.Size(346, 20);
            this.cboMessageCode.StyleController = this.layoutControl1;
            this.cboMessageCode.TabIndex = 10;
            this.cboMessageCode.EditValueChanged += new System.EventHandler(this.cboMessageCode_EditValueChanged);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(228, 348);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(218, 78);
            this.cmdClose.StyleController = this.layoutControl1;
            this.cmdClose.TabIndex = 8;
            this.cmdClose.Text = "Cancel";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(12, 348);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(216, 78);
            this.cmdOk.StyleController = this.layoutControl1;
            this.cmdOk.TabIndex = 7;
            this.cmdOk.Text = "Ok";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cboClientGroup
            // 
            this.cboClientGroup.Location = new System.Drawing.Point(100, 43);
            this.cboClientGroup.Name = "cboClientGroup";
            this.cboClientGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboClientGroup.Properties.NullText = "";
            this.cboClientGroup.Size = new System.Drawing.Size(346, 20);
            this.cboClientGroup.StyleController = this.layoutControl1;
            this.cboClientGroup.TabIndex = 4;
            this.cboClientGroup.EditValueChanged += new System.EventHandler(this.cboClientGroup_EditValueChanged);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(100, 115);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(346, 71);
            this.txtMessage.StyleController = this.layoutControl1;
            this.txtMessage.TabIndex = 6;
            // 
            // cboMessageSource
            // 
            this.cboMessageSource.Location = new System.Drawing.Point(100, 67);
            this.cboMessageSource.Name = "cboMessageSource";
            this.cboMessageSource.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboMessageSource.Properties.Items.AddRange(new object[] {
            "From Templates",
            "New Message"});
            this.cboMessageSource.Properties.PopupSizeable = true;
            this.cboMessageSource.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboMessageSource.Size = new System.Drawing.Size(346, 20);
            this.cboMessageSource.StyleController = this.layoutControl1;
            this.cboMessageSource.TabIndex = 3;
            this.cboMessageSource.SelectedIndexChanged += new System.EventHandler(this.cboMessageSource_SelectedIndexChanged);
            this.cboMessageSource.EditValueChanged += new System.EventHandler(this.cboWard_EditValueChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.txbWard,
            this.txbRemarks,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.emptySpaceItem3,
            this.txbBedNo,
            this.layoutControlItem3,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.txbRoom});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(458, 438);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // txbWard
            // 
            this.txbWard.Control = this.cboMessageSource;
            this.txbWard.CustomizationFormText = "txbWard";
            this.txbWard.Location = new System.Drawing.Point(0, 55);
            this.txbWard.Name = "txbWard";
            this.txbWard.Size = new System.Drawing.Size(438, 24);
            this.txbWard.Text = "Message Source";
            this.txbWard.TextSize = new System.Drawing.Size(84, 13);
            // 
            // txbRemarks
            // 
            this.txbRemarks.Control = this.txtMessage;
            this.txbRemarks.CustomizationFormText = "txbRemarks";
            this.txbRemarks.Location = new System.Drawing.Point(0, 103);
            this.txbRemarks.Name = "txbRemarks";
            this.txbRemarks.Size = new System.Drawing.Size(438, 75);
            this.txbRemarks.Text = "Message";
            this.txbRemarks.TextSize = new System.Drawing.Size(84, 13);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.cmdOk;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 336);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(37, 33);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 2);
            this.layoutControlItem7.Size = new System.Drawing.Size(218, 82);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.cmdClose;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(218, 336);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(56, 33);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 2, 2, 2);
            this.layoutControlItem8.Size = new System.Drawing.Size(220, 82);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 195);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(89, 141);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // txbBedNo
            // 
            this.txbBedNo.Control = this.cboMessageCode;
            this.txbBedNo.CustomizationFormText = "Bed";
            this.txbBedNo.Location = new System.Drawing.Point(0, 79);
            this.txbBedNo.Name = "txbBedNo";
            this.txbBedNo.Size = new System.Drawing.Size(438, 24);
            this.txbBedNo.Text = "Message Code";
            this.txbBedNo.TextSize = new System.Drawing.Size(84, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.rdbGroup;
            this.layoutControlItem3.CustomizationFormText = "Send Message To";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(438, 31);
            this.layoutControlItem3.Text = "Send Message To";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(84, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.lstRecepients;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(89, 178);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(251, 158);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.labelControl1;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 178);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(89, 17);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(340, 230);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(98, 106);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnAdd;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(340, 178);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(98, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnRemove;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(340, 204);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(98, 26);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // txbRoom
            // 
            this.txbRoom.Control = this.cboClientGroup;
            this.txbRoom.CustomizationFormText = "layoutControlItem2";
            this.txbRoom.Location = new System.Drawing.Point(0, 31);
            this.txbRoom.Name = "txbRoom";
            this.txbRoom.Size = new System.Drawing.Size(438, 24);
            this.txbRoom.Text = "Client Group";
            this.txbRoom.TextSize = new System.Drawing.Size(84, 13);
            // 
            // frmSMSSendMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 438);
            this.Controls.Add(this.layoutControl1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSMSSendMessage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Admit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmIPDAdmit_FormClosing);
            this.Load += new System.EventHandler(this.frmIPDAdmit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstRecepients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdbGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMessageCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboClientGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMessageSource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbWard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbRemarks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbBedNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbRoom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton cmdOk;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
        private DevExpress.XtraEditors.LookUpEdit cboClientGroup;
        private DevExpress.XtraEditors.MemoEdit txtMessage;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem txbWard;
        private DevExpress.XtraLayout.LayoutControlItem txbRoom;
        private DevExpress.XtraLayout.LayoutControlItem txbRemarks;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.LookUpEdit cboMessageCode;
        private DevExpress.XtraLayout.LayoutControlItem txbBedNo;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ListBoxControl lstRecepients;
        private DevExpress.XtraEditors.RadioGroup rdbGroup;
        private DevExpress.XtraEditors.SimpleButton btnRemove;
        private DevExpress.XtraEditors.ComboBoxEdit cboMessageSource;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}