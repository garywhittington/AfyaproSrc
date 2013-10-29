namespace AfyaPro_NextGen
{
    partial class frmGENPatientFields
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
            this.radCharacterCasingOption = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtErrorOnEmpty = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.chkCompulsory = new DevExpress.XtraEditors.CheckEdit();
            this.txbFilterValueOn = new DevExpress.XtraEditors.LabelControl();
            this.txtDefaultValue = new DevExpress.XtraEditors.TextEdit();
            this.txbDefaultValue = new DevExpress.XtraEditors.LabelControl();
            this.txbDataType = new DevExpress.XtraEditors.LabelControl();
            this.txbFieldType = new DevExpress.XtraEditors.LabelControl();
            this.txtCaption = new DevExpress.XtraEditors.TextEdit();
            this.txbCaption = new DevExpress.XtraEditors.LabelControl();
            this.txtFieldName = new DevExpress.XtraEditors.TextEdit();
            this.txbFieldName = new DevExpress.XtraEditors.LabelControl();
            this.cboFieldType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cboDataType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cboFilterValueFrom = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkRememberEntries = new DevExpress.XtraEditors.CheckEdit();
            this.chkAllowInput = new DevExpress.XtraEditors.CheckEdit();
            this.cmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
            this.grpDropDownOptions = new DevExpress.XtraEditors.GroupControl();
            this.cmdRemove = new DevExpress.XtraEditors.SimpleButton();
            this.cmdAdd = new DevExpress.XtraEditors.SimpleButton();
            this.grdDropDownValues = new DevExpress.XtraGrid.GridControl();
            this.viewDropDownValues = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkRestrictToDropDownList = new DevExpress.XtraEditors.CheckEdit();
            this.txtErrorOnInvalidInput = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cmdNew = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.radCharacterCasingOption.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtErrorOnEmpty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCompulsory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefaultValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCaption.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFieldName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFieldType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDataType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFilterValueFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRememberEntries.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllowInput.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDropDownOptions)).BeginInit();
            this.grpDropDownOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDropDownValues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewDropDownValues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRestrictToDropDownList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtErrorOnInvalidInput.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // radCharacterCasingOption
            // 
            this.radCharacterCasingOption.Location = new System.Drawing.Point(140, 296);
            this.radCharacterCasingOption.Name = "radCharacterCasingOption";
            this.radCharacterCasingOption.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Normal"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Upper"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Lower")});
            this.radCharacterCasingOption.Size = new System.Drawing.Size(231, 86);
            this.radCharacterCasingOption.TabIndex = 8;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(11, 299);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(114, 13);
            this.labelControl2.TabIndex = 19;
            this.labelControl2.Text = "Character casing option";
            // 
            // txtErrorOnEmpty
            // 
            this.txtErrorOnEmpty.Location = new System.Drawing.Point(140, 244);
            this.txtErrorOnEmpty.Name = "txtErrorOnEmpty";
            this.txtErrorOnEmpty.Size = new System.Drawing.Size(229, 20);
            this.txtErrorOnEmpty.TabIndex = 17;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(11, 247);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 13);
            this.labelControl1.TabIndex = 18;
            this.labelControl1.Text = "Error on empty";
            // 
            // chkCompulsory
            // 
            this.chkCompulsory.EditValue = null;
            this.chkCompulsory.Location = new System.Drawing.Point(138, 219);
            this.chkCompulsory.Name = "chkCompulsory";
            this.chkCompulsory.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.chkCompulsory.Properties.Caption = "Compulsory";
            this.chkCompulsory.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.chkCompulsory.Size = new System.Drawing.Size(193, 19);
            this.chkCompulsory.TabIndex = 16;
            this.chkCompulsory.TabStop = false;
            // 
            // txbFilterValueOn
            // 
            this.txbFilterValueOn.Location = new System.Drawing.Point(11, 145);
            this.txbFilterValueOn.Name = "txbFilterValueOn";
            this.txbFilterValueOn.Size = new System.Drawing.Size(68, 13);
            this.txbFilterValueOn.TabIndex = 11;
            this.txbFilterValueOn.Text = "Filter value on";
            // 
            // txtDefaultValue
            // 
            this.txtDefaultValue.Location = new System.Drawing.Point(140, 116);
            this.txtDefaultValue.Name = "txtDefaultValue";
            this.txtDefaultValue.Size = new System.Drawing.Size(231, 20);
            this.txtDefaultValue.TabIndex = 8;
            // 
            // txbDefaultValue
            // 
            this.txbDefaultValue.Location = new System.Drawing.Point(11, 119);
            this.txbDefaultValue.Name = "txbDefaultValue";
            this.txbDefaultValue.Size = new System.Drawing.Size(64, 13);
            this.txbDefaultValue.TabIndex = 9;
            this.txbDefaultValue.Text = "Default Value";
            // 
            // txbDataType
            // 
            this.txbDataType.Location = new System.Drawing.Point(11, 93);
            this.txbDataType.Name = "txbDataType";
            this.txbDataType.Size = new System.Drawing.Size(50, 13);
            this.txbDataType.TabIndex = 7;
            this.txbDataType.Text = "Data Type";
            // 
            // txbFieldType
            // 
            this.txbFieldType.Location = new System.Drawing.Point(11, 67);
            this.txbFieldType.Name = "txbFieldType";
            this.txbFieldType.Size = new System.Drawing.Size(49, 13);
            this.txbFieldType.TabIndex = 5;
            this.txbFieldType.Text = "Field Type";
            // 
            // txtCaption
            // 
            this.txtCaption.Location = new System.Drawing.Point(140, 38);
            this.txtCaption.Name = "txtCaption";
            this.txtCaption.Size = new System.Drawing.Size(231, 20);
            this.txtCaption.TabIndex = 2;
            // 
            // txbCaption
            // 
            this.txbCaption.Location = new System.Drawing.Point(11, 41);
            this.txbCaption.Name = "txbCaption";
            this.txbCaption.Size = new System.Drawing.Size(37, 13);
            this.txbCaption.TabIndex = 3;
            this.txbCaption.Text = "Caption";
            // 
            // txtFieldName
            // 
            this.txtFieldName.Location = new System.Drawing.Point(140, 12);
            this.txtFieldName.Name = "txtFieldName";
            this.txtFieldName.Size = new System.Drawing.Size(231, 20);
            this.txtFieldName.TabIndex = 1;
            // 
            // txbFieldName
            // 
            this.txbFieldName.Location = new System.Drawing.Point(11, 15);
            this.txbFieldName.Name = "txbFieldName";
            this.txbFieldName.Size = new System.Drawing.Size(52, 13);
            this.txbFieldName.TabIndex = 1;
            this.txbFieldName.Text = "Field Name";
            // 
            // cboFieldType
            // 
            this.cboFieldType.Location = new System.Drawing.Point(140, 64);
            this.cboFieldType.Name = "cboFieldType";
            this.cboFieldType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboFieldType.Properties.Items.AddRange(new object[] {
            "Text",
            "Dropdown",
            "Checkbox"});
            this.cboFieldType.Size = new System.Drawing.Size(231, 20);
            this.cboFieldType.TabIndex = 4;
            this.cboFieldType.TabStop = false;
            this.cboFieldType.SelectedIndexChanged += new System.EventHandler(this.cboFieldType_SelectedIndexChanged);
            // 
            // cboDataType
            // 
            this.cboDataType.Location = new System.Drawing.Point(140, 90);
            this.cboDataType.Name = "cboDataType";
            this.cboDataType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDataType.Properties.Items.AddRange(new object[] {
            "General",
            "Currency",
            "Number",
            "DateTime"});
            this.cboDataType.Size = new System.Drawing.Size(231, 20);
            this.cboDataType.TabIndex = 6;
            this.cboDataType.TabStop = false;
            // 
            // cboFilterValueFrom
            // 
            this.cboFilterValueFrom.Location = new System.Drawing.Point(140, 142);
            this.cboFilterValueFrom.Name = "cboFilterValueFrom";
            this.cboFilterValueFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboFilterValueFrom.Size = new System.Drawing.Size(231, 20);
            this.cboFilterValueFrom.TabIndex = 10;
            this.cboFilterValueFrom.TabStop = false;
            // 
            // chkRememberEntries
            // 
            this.chkRememberEntries.EditValue = null;
            this.chkRememberEntries.Location = new System.Drawing.Point(138, 194);
            this.chkRememberEntries.Name = "chkRememberEntries";
            this.chkRememberEntries.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.chkRememberEntries.Properties.Caption = "Auto complete entries";
            this.chkRememberEntries.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.chkRememberEntries.Size = new System.Drawing.Size(193, 19);
            this.chkRememberEntries.TabIndex = 14;
            this.chkRememberEntries.TabStop = false;
            // 
            // chkAllowInput
            // 
            this.chkAllowInput.EditValue = null;
            this.chkAllowInput.Location = new System.Drawing.Point(138, 168);
            this.chkAllowInput.Name = "chkAllowInput";
            this.chkAllowInput.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.chkAllowInput.Properties.Caption = "Allow Input";
            this.chkAllowInput.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.chkAllowInput.Size = new System.Drawing.Size(193, 19);
            this.chkAllowInput.TabIndex = 12;
            this.chkAllowInput.TabStop = false;
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(205, 388);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(188, 37);
            this.cmdOk.TabIndex = 3;
            this.cmdOk.Text = "Save";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(399, 388);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(166, 37);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // grpDropDownOptions
            // 
            this.grpDropDownOptions.Controls.Add(this.cmdRemove);
            this.grpDropDownOptions.Controls.Add(this.cmdAdd);
            this.grpDropDownOptions.Controls.Add(this.grdDropDownValues);
            this.grpDropDownOptions.Controls.Add(this.chkRestrictToDropDownList);
            this.grpDropDownOptions.Enabled = false;
            this.grpDropDownOptions.Location = new System.Drawing.Point(384, 12);
            this.grpDropDownOptions.Name = "grpDropDownOptions";
            this.grpDropDownOptions.Size = new System.Drawing.Size(367, 370);
            this.grpDropDownOptions.TabIndex = 20;
            this.grpDropDownOptions.Text = "Drop Down Options";
            // 
            // cmdRemove
            // 
            this.cmdRemove.Location = new System.Drawing.Point(186, 64);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(171, 22);
            this.cmdRemove.TabIndex = 17;
            this.cmdRemove.Text = "&Remove";
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(9, 64);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(173, 22);
            this.cmdAdd.TabIndex = 16;
            this.cmdAdd.Text = "&Add";
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // grdDropDownValues
            // 
            this.grdDropDownValues.Location = new System.Drawing.Point(9, 89);
            this.grdDropDownValues.MainView = this.viewDropDownValues;
            this.grdDropDownValues.Name = "grdDropDownValues";
            this.grdDropDownValues.Size = new System.Drawing.Size(350, 274);
            this.grdDropDownValues.TabIndex = 15;
            this.grdDropDownValues.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewDropDownValues});
            // 
            // viewDropDownValues
            // 
            this.viewDropDownValues.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.viewDropDownValues.GridControl = this.grdDropDownValues;
            this.viewDropDownValues.Name = "viewDropDownValues";
            this.viewDropDownValues.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.viewDropDownValues.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.viewDropDownValues.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Description";
            this.gridColumn1.FieldName = "description";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 162;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Filter Value";
            this.gridColumn2.FieldName = "filtervalue";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 167;
            // 
            // chkRestrictToDropDownList
            // 
            this.chkRestrictToDropDownList.EditValue = null;
            this.chkRestrictToDropDownList.Location = new System.Drawing.Point(7, 27);
            this.chkRestrictToDropDownList.Name = "chkRestrictToDropDownList";
            this.chkRestrictToDropDownList.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.chkRestrictToDropDownList.Properties.Caption = "Restrict entries to drop down list";
            this.chkRestrictToDropDownList.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.chkRestrictToDropDownList.Size = new System.Drawing.Size(193, 19);
            this.chkRestrictToDropDownList.TabIndex = 13;
            this.chkRestrictToDropDownList.TabStop = false;
            // 
            // txtErrorOnInvalidInput
            // 
            this.txtErrorOnInvalidInput.Location = new System.Drawing.Point(140, 270);
            this.txtErrorOnInvalidInput.Name = "txtErrorOnInvalidInput";
            this.txtErrorOnInvalidInput.Size = new System.Drawing.Size(229, 20);
            this.txtErrorOnInvalidInput.TabIndex = 21;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(11, 273);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(99, 13);
            this.labelControl3.TabIndex = 22;
            this.labelControl3.Text = "Error on invalid input";
            // 
            // cmdNew
            // 
            this.cmdNew.Location = new System.Drawing.Point(11, 388);
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Size = new System.Drawing.Size(188, 37);
            this.cmdNew.TabIndex = 23;
            this.cmdNew.Text = "New";
            this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
            // 
            // frmGENPatientFields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 436);
            this.Controls.Add(this.cmdNew);
            this.Controls.Add(this.txtErrorOnInvalidInput);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.grpDropDownOptions);
            this.Controls.Add(this.radCharacterCasingOption);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.txtErrorOnEmpty);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtFieldName);
            this.Controls.Add(this.chkCompulsory);
            this.Controls.Add(this.chkAllowInput);
            this.Controls.Add(this.txbFilterValueOn);
            this.Controls.Add(this.chkRememberEntries);
            this.Controls.Add(this.txtDefaultValue);
            this.Controls.Add(this.cboFilterValueFrom);
            this.Controls.Add(this.txbDefaultValue);
            this.Controls.Add(this.cboDataType);
            this.Controls.Add(this.txbDataType);
            this.Controls.Add(this.cboFieldType);
            this.Controls.Add(this.txbFieldType);
            this.Controls.Add(this.txbFieldName);
            this.Controls.Add(this.txtCaption);
            this.Controls.Add(this.txbCaption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGENPatientFields";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Patient extra fields";
            this.Load += new System.EventHandler(this.frmGENPatientFields_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radCharacterCasingOption.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtErrorOnEmpty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCompulsory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefaultValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCaption.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFieldName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFieldType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDataType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFilterValueFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRememberEntries.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllowInput.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDropDownOptions)).EndInit();
            this.grpDropDownOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDropDownValues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewDropDownValues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRestrictToDropDownList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtErrorOnInvalidInput.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl txbFieldName;
        private DevExpress.XtraEditors.TextEdit txtCaption;
        private DevExpress.XtraEditors.LabelControl txbCaption;
        private DevExpress.XtraEditors.TextEdit txtFieldName;
        private DevExpress.XtraEditors.SimpleButton cmdOk;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
        private DevExpress.XtraEditors.LabelControl txbFilterValueOn;
        private DevExpress.XtraEditors.TextEdit txtDefaultValue;
        private DevExpress.XtraEditors.LabelControl txbDefaultValue;
        private DevExpress.XtraEditors.LabelControl txbDataType;
        private DevExpress.XtraEditors.LabelControl txbFieldType;
        private DevExpress.XtraEditors.ComboBoxEdit cboFieldType;
        private DevExpress.XtraEditors.ComboBoxEdit cboDataType;
        private DevExpress.XtraEditors.ComboBoxEdit cboFilterValueFrom;
        private DevExpress.XtraEditors.CheckEdit chkRememberEntries;
        private DevExpress.XtraEditors.CheckEdit chkAllowInput;
        private DevExpress.XtraEditors.RadioGroup radCharacterCasingOption;
        private DevExpress.XtraEditors.CheckEdit chkCompulsory;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtErrorOnEmpty;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl grpDropDownOptions;
        private DevExpress.XtraEditors.TextEdit txtErrorOnInvalidInput;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckEdit chkRestrictToDropDownList;
        private DevExpress.XtraGrid.GridControl grdDropDownValues;
        private DevExpress.XtraGrid.Views.Grid.GridView viewDropDownValues;
        private DevExpress.XtraEditors.SimpleButton cmdAdd;
        private DevExpress.XtraEditors.SimpleButton cmdRemove;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.SimpleButton cmdNew;
    }
}