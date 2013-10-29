namespace AfyaPro_SerialMaker
{
    partial class frmMain
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
            this.btnGenerate = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.txtInstallationId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkcts = new System.Windows.Forms.CheckBox();
            this.chkctc = new System.Windows.Forms.CheckBox();
            this.chksec = new System.Windows.Forms.CheckBox();
            this.chkgen = new System.Windows.Forms.CheckBox();
            this.chkrpd = new System.Windows.Forms.CheckBox();
            this.chkivs = new System.Windows.Forms.CheckBox();
            this.chkbls = new System.Windows.Forms.CheckBox();
            this.chkrpt = new System.Windows.Forms.CheckBox();
            this.chkmtu = new System.Windows.Forms.CheckBox();
            this.chkbil = new System.Windows.Forms.CheckBox();
            this.chkdxt = new System.Windows.Forms.CheckBox();
            this.chklab = new System.Windows.Forms.CheckBox();
            this.chkcus = new System.Windows.Forms.CheckBox();
            this.chkivt = new System.Windows.Forms.CheckBox();
            this.chkipd = new System.Windows.Forms.CheckBox();
            this.chkopd = new System.Windows.Forms.CheckBox();
            this.chkrch = new System.Windows.Forms.CheckBox();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radTrialReset = new System.Windows.Forms.RadioButton();
            this.txtDays = new System.Windows.Forms.TextBox();
            this.radFullLicence = new System.Windows.Forms.RadioButton();
            this.radTrialExtend = new System.Windows.Forms.RadioButton();
            this.chksms = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(332, 413);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(147, 31);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "Generate Activation File";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(485, 413);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(87, 31);
            this.cmdClose.TabIndex = 5;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // txtInstallationId
            // 
            this.txtInstallationId.Location = new System.Drawing.Point(4, 21);
            this.txtInstallationId.Name = "txtInstallationId";
            this.txtInstallationId.Size = new System.Drawing.Size(359, 20);
            this.txtInstallationId.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Installation ID";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chksms);
            this.groupBox1.Controls.Add(this.chkcts);
            this.groupBox1.Controls.Add(this.chkctc);
            this.groupBox1.Controls.Add(this.chksec);
            this.groupBox1.Controls.Add(this.chkgen);
            this.groupBox1.Controls.Add(this.chkrpd);
            this.groupBox1.Controls.Add(this.chkivs);
            this.groupBox1.Controls.Add(this.chkbls);
            this.groupBox1.Controls.Add(this.chkrpt);
            this.groupBox1.Controls.Add(this.chkmtu);
            this.groupBox1.Controls.Add(this.chkbil);
            this.groupBox1.Controls.Add(this.chkdxt);
            this.groupBox1.Controls.Add(this.chklab);
            this.groupBox1.Controls.Add(this.chkcus);
            this.groupBox1.Controls.Add(this.chkivt);
            this.groupBox1.Controls.Add(this.chkipd);
            this.groupBox1.Controls.Add(this.chkopd);
            this.groupBox1.Controls.Add(this.chkrch);
            this.groupBox1.Location = new System.Drawing.Point(4, 159);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(568, 198);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Modules";
            // 
            // chkcts
            // 
            this.chkcts.AutoSize = true;
            this.chkcts.Location = new System.Drawing.Point(351, 73);
            this.chkcts.Name = "chkcts";
            this.chkcts.Size = new System.Drawing.Size(78, 17);
            this.chkcts.TabIndex = 17;
            this.chkcts.Text = "CTC Setup";
            this.chkcts.UseVisualStyleBackColor = true;
            // 
            // chkctc
            // 
            this.chkctc.AutoSize = true;
            this.chkctc.Location = new System.Drawing.Point(351, 48);
            this.chkctc.Name = "chkctc";
            this.chkctc.Size = new System.Drawing.Size(47, 17);
            this.chkctc.TabIndex = 16;
            this.chkctc.Text = "CTC";
            this.chkctc.UseVisualStyleBackColor = true;
            // 
            // chksec
            // 
            this.chksec.AutoSize = true;
            this.chksec.Checked = true;
            this.chksec.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chksec.Location = new System.Drawing.Point(179, 49);
            this.chksec.Name = "chksec";
            this.chksec.Size = new System.Drawing.Size(95, 17);
            this.chksec.TabIndex = 15;
            this.chksec.Text = "Security Setup";
            this.chksec.UseVisualStyleBackColor = true;
            // 
            // chkgen
            // 
            this.chkgen.AutoSize = true;
            this.chkgen.Checked = true;
            this.chkgen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkgen.Location = new System.Drawing.Point(179, 25);
            this.chkgen.Name = "chkgen";
            this.chkgen.Size = new System.Drawing.Size(94, 17);
            this.chkgen.TabIndex = 14;
            this.chkgen.Text = "General Setup";
            this.chkgen.UseVisualStyleBackColor = true;
            // 
            // chkrpd
            // 
            this.chkrpd.AutoSize = true;
            this.chkrpd.Checked = true;
            this.chkrpd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkrpd.Location = new System.Drawing.Point(10, 165);
            this.chkrpd.Name = "chkrpd";
            this.chkrpd.Size = new System.Drawing.Size(108, 17);
            this.chkrpd.TabIndex = 13;
            this.chkrpd.Text = "Reports Designer";
            this.chkrpd.UseVisualStyleBackColor = true;
            // 
            // chkivs
            // 
            this.chkivs.AutoSize = true;
            this.chkivs.Location = new System.Drawing.Point(179, 119);
            this.chkivs.Name = "chkivs";
            this.chkivs.Size = new System.Drawing.Size(101, 17);
            this.chkivs.TabIndex = 11;
            this.chkivs.Text = "Inventory Setup";
            this.chkivs.UseVisualStyleBackColor = true;
            // 
            // chkbls
            // 
            this.chkbls.AutoSize = true;
            this.chkbls.Checked = true;
            this.chkbls.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbls.Location = new System.Drawing.Point(10, 119);
            this.chkbls.Name = "chkbls";
            this.chkbls.Size = new System.Drawing.Size(84, 17);
            this.chkbls.TabIndex = 10;
            this.chkbls.Text = "Billing Setup";
            this.chkbls.UseVisualStyleBackColor = true;
            // 
            // chkrpt
            // 
            this.chkrpt.AutoSize = true;
            this.chkrpt.Checked = true;
            this.chkrpt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkrpt.Location = new System.Drawing.Point(10, 142);
            this.chkrpt.Name = "chkrpt";
            this.chkrpt.Size = new System.Drawing.Size(63, 17);
            this.chkrpt.TabIndex = 9;
            this.chkrpt.Text = "Reports";
            this.chkrpt.UseVisualStyleBackColor = true;
            this.chkrpt.CheckedChanged += new System.EventHandler(this.chkrpt_CheckedChanged);
            // 
            // chkmtu
            // 
            this.chkmtu.AutoSize = true;
            this.chkmtu.Location = new System.Drawing.Point(179, 73);
            this.chkmtu.Name = "chkmtu";
            this.chkmtu.Size = new System.Drawing.Size(65, 17);
            this.chkmtu.TabIndex = 8;
            this.chkmtu.Text = "MTUHA";
            this.chkmtu.UseVisualStyleBackColor = true;
            // 
            // chkbil
            // 
            this.chkbil.AutoSize = true;
            this.chkbil.Checked = true;
            this.chkbil.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbil.Location = new System.Drawing.Point(10, 96);
            this.chkbil.Name = "chkbil";
            this.chkbil.Size = new System.Drawing.Size(53, 17);
            this.chkbil.TabIndex = 7;
            this.chkbil.Text = "Billing";
            this.chkbil.UseVisualStyleBackColor = true;
            this.chkbil.CheckedChanged += new System.EventHandler(this.chkbil_CheckedChanged);
            // 
            // chkdxt
            // 
            this.chkdxt.AutoSize = true;
            this.chkdxt.Location = new System.Drawing.Point(179, 142);
            this.chkdxt.Name = "chkdxt";
            this.chkdxt.Size = new System.Drawing.Size(153, 17);
            this.chkdxt.TabIndex = 6;
            this.chkdxt.Text = "Diagnoses and Treatments";
            this.chkdxt.UseVisualStyleBackColor = true;
            this.chkdxt.CheckedChanged += new System.EventHandler(this.chkbil_CheckedChanged);
            // 
            // chklab
            // 
            this.chklab.AutoSize = true;
            this.chklab.Location = new System.Drawing.Point(179, 165);
            this.chklab.Name = "chklab";
            this.chklab.Size = new System.Drawing.Size(76, 17);
            this.chklab.TabIndex = 5;
            this.chklab.Text = "Laboratory";
            this.chklab.UseVisualStyleBackColor = true;
            // 
            // chkcus
            // 
            this.chkcus.AutoSize = true;
            this.chkcus.Checked = true;
            this.chkcus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkcus.Location = new System.Drawing.Point(10, 73);
            this.chkcus.Name = "chkcus";
            this.chkcus.Size = new System.Drawing.Size(75, 17);
            this.chkcus.TabIndex = 4;
            this.chkcus.Text = "Customers";
            this.chkcus.UseVisualStyleBackColor = true;
            // 
            // chkivt
            // 
            this.chkivt.AutoSize = true;
            this.chkivt.Location = new System.Drawing.Point(179, 96);
            this.chkivt.Name = "chkivt";
            this.chkivt.Size = new System.Drawing.Size(70, 17);
            this.chkivt.TabIndex = 3;
            this.chkivt.Text = "Inventory";
            this.chkivt.UseVisualStyleBackColor = true;
            this.chkivt.CheckedChanged += new System.EventHandler(this.chkivt_CheckedChanged);
            // 
            // chkipd
            // 
            this.chkipd.AutoSize = true;
            this.chkipd.Checked = true;
            this.chkipd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkipd.Location = new System.Drawing.Point(10, 49);
            this.chkipd.Name = "chkipd";
            this.chkipd.Size = new System.Drawing.Size(125, 17);
            this.chkipd.TabIndex = 2;
            this.chkipd.Text = "Inpatient Department";
            this.chkipd.UseVisualStyleBackColor = true;
            // 
            // chkopd
            // 
            this.chkopd.AutoSize = true;
            this.chkopd.Checked = true;
            this.chkopd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkopd.Location = new System.Drawing.Point(10, 25);
            this.chkopd.Name = "chkopd";
            this.chkopd.Size = new System.Drawing.Size(133, 17);
            this.chkopd.TabIndex = 1;
            this.chkopd.Text = "Outpatient Department";
            this.chkopd.UseVisualStyleBackColor = true;
            // 
            // chkrch
            // 
            this.chkrch.AutoSize = true;
            this.chkrch.Location = new System.Drawing.Point(351, 25);
            this.chkrch.Name = "chkrch";
            this.chkrch.Size = new System.Drawing.Size(171, 17);
            this.chkrch.TabIndex = 0;
            this.chkrch.Text = "Reproductive and Child Health";
            this.chkrch.UseVisualStyleBackColor = true;
            this.chkrch.CheckedChanged += new System.EventHandler(this.chkrpt_CheckedChanged);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(10, 385);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(492, 20);
            this.txtFilePath.TabIndex = 9;
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.Location = new System.Drawing.Point(508, 384);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(64, 23);
            this.cmdBrowse.TabIndex = 10;
            this.cmdBrowse.Text = "Browse";
            this.cmdBrowse.UseVisualStyleBackColor = true;
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 369);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Location for Generated Activation File";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radTrialReset);
            this.groupBox2.Controls.Add(this.txtDays);
            this.groupBox2.Controls.Add(this.radFullLicence);
            this.groupBox2.Controls.Add(this.radTrialExtend);
            this.groupBox2.Location = new System.Drawing.Point(4, 45);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(159, 108);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Licence Type";
            // 
            // radTrialReset
            // 
            this.radTrialReset.AutoSize = true;
            this.radTrialReset.Location = new System.Drawing.Point(18, 50);
            this.radTrialReset.Name = "radTrialReset";
            this.radTrialReset.Size = new System.Drawing.Size(76, 17);
            this.radTrialReset.TabIndex = 3;
            this.radTrialReset.TabStop = true;
            this.radTrialReset.Text = "Trial Reset";
            this.radTrialReset.UseVisualStyleBackColor = true;
            this.radTrialReset.CheckedChanged += new System.EventHandler(this.radTrialReset_CheckedChanged);
            // 
            // txtDays
            // 
            this.txtDays.Location = new System.Drawing.Point(105, 24);
            this.txtDays.Name = "txtDays";
            this.txtDays.Size = new System.Drawing.Size(35, 20);
            this.txtDays.TabIndex = 2;
            // 
            // radFullLicence
            // 
            this.radFullLicence.AutoSize = true;
            this.radFullLicence.Location = new System.Drawing.Point(18, 75);
            this.radFullLicence.Name = "radFullLicence";
            this.radFullLicence.Size = new System.Drawing.Size(82, 17);
            this.radFullLicence.TabIndex = 1;
            this.radFullLicence.TabStop = true;
            this.radFullLicence.Text = "Full Licence";
            this.radFullLicence.UseVisualStyleBackColor = true;
            // 
            // radTrialExtend
            // 
            this.radTrialExtend.AutoSize = true;
            this.radTrialExtend.Location = new System.Drawing.Point(18, 25);
            this.radTrialExtend.Name = "radTrialExtend";
            this.radTrialExtend.Size = new System.Drawing.Size(81, 17);
            this.radTrialExtend.TabIndex = 0;
            this.radTrialExtend.TabStop = true;
            this.radTrialExtend.Text = "Trial Extend";
            this.radTrialExtend.UseVisualStyleBackColor = true;
            this.radTrialExtend.CheckedChanged += new System.EventHandler(this.radTrialExtend_CheckedChanged);
            // 
            // chksms
            // 
            this.chksms.AutoSize = true;
            this.chksms.Location = new System.Drawing.Point(351, 96);
            this.chksms.Name = "chksms";
            this.chksms.Size = new System.Drawing.Size(91, 17);
            this.chksms.TabIndex = 18;
            this.chksms.Text = "Mobile Health";
            this.chksms.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AcceptButton = this.btnGenerate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 456);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdBrowse);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtInstallationId);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.btnGenerate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AfyaPro Serial Maker";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.TextBox txtInstallationId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkipd;
        private System.Windows.Forms.CheckBox chkopd;
        private System.Windows.Forms.CheckBox chkdxt;
        private System.Windows.Forms.CheckBox chklab;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button cmdBrowse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtDays;
        private System.Windows.Forms.RadioButton radFullLicence;
        private System.Windows.Forms.RadioButton radTrialExtend;
        private System.Windows.Forms.RadioButton radTrialReset;
        private System.Windows.Forms.CheckBox chkmtu;
        private System.Windows.Forms.CheckBox chkbil;
        private System.Windows.Forms.CheckBox chkcus;
        private System.Windows.Forms.CheckBox chkivt;
        private System.Windows.Forms.CheckBox chkrch;
        private System.Windows.Forms.CheckBox chkivs;
        private System.Windows.Forms.CheckBox chkbls;
        private System.Windows.Forms.CheckBox chkrpt;
        private System.Windows.Forms.CheckBox chksec;
        private System.Windows.Forms.CheckBox chkgen;
        private System.Windows.Forms.CheckBox chkrpd;
        private System.Windows.Forms.CheckBox chkcts;
        private System.Windows.Forms.CheckBox chkctc;
        private System.Windows.Forms.CheckBox chksms;
    }
}

