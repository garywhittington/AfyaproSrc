namespace AfyaPro_NextGen
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.ribbonBar = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.applicationMenu1 = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.cmdActivate = new DevExpress.XtraBars.BarButtonItem();
            this.mnuHelp = new DevExpress.XtraBars.BarButtonItem();
            this.imgToolBar = new DevExpress.Utils.ImageCollection(this.components);
            this.txtDate = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.cmdNew = new DevExpress.XtraBars.BarButtonItem();
            this.cmdEdit = new DevExpress.XtraBars.BarButtonItem();
            this.cmdDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.cmdRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.cboSkins = new DevExpress.XtraBars.BarEditItem();
            this.cboSkinsEditor = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.pagToolBar = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.repositoryItemDateEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imgReports = new System.Windows.Forms.ImageList(this.components);
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.navBar = new DevExpress.XtraNavBar.NavBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgToolBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSkinsEditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBar)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonBar
            // 
            this.ribbonBar.ApplicationButtonDropDownControl = this.applicationMenu1;
            this.ribbonBar.ApplicationButtonText = null;
            this.ribbonBar.ApplicationIcon = global::AfyaPro_NextGen.Properties.Resources.applicationicon;
            this.ribbonBar.Images = this.imgToolBar;
            this.ribbonBar.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.txtDate,
            this.cmdNew,
            this.cmdEdit,
            this.cmdDelete,
            this.barSubItem1,
            this.barButtonItem1,
            this.barButtonItem3,
            this.barButtonItem4,
            this.cmdRefresh,
            this.cboSkins,
            this.barButtonItem2,
            this.cmdActivate,
            this.mnuHelp});
            this.ribbonBar.Location = new System.Drawing.Point(0, 0);
            this.ribbonBar.MaxItemId = 27;
            this.ribbonBar.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Never;
            this.ribbonBar.Name = "ribbonBar";
            this.ribbonBar.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.pagToolBar});
            this.ribbonBar.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDateEdit1,
            this.cboSkinsEditor,
            this.repositoryItemDateEdit2});
            this.ribbonBar.SelectedPage = this.pagToolBar;
            this.ribbonBar.Size = new System.Drawing.Size(854, 143);
            this.ribbonBar.StatusBar = this.ribbonStatusBar;
            this.ribbonBar.Toolbar.ItemLinks.Add(this.barButtonItem2);
            this.ribbonBar.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // applicationMenu1
            // 
            this.applicationMenu1.BottomPaneControlContainer = null;
            this.applicationMenu1.ItemLinks.Add(this.cmdActivate);
            this.applicationMenu1.ItemLinks.Add(this.mnuHelp);
            this.applicationMenu1.Name = "applicationMenu1";
            this.applicationMenu1.Ribbon = this.ribbonBar;
            this.applicationMenu1.RightPaneControlContainer = null;
            // 
            // cmdActivate
            // 
            this.cmdActivate.Caption = "&Activate/Upgrade...";
            this.cmdActivate.Id = 23;
            this.cmdActivate.Name = "cmdActivate";
            this.cmdActivate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.cmdActivate_ItemClick);
            // 
            // mnuHelp
            // 
            this.mnuHelp.Id = 25;
            this.mnuHelp.Name = "mnuHelp";
            // 
            // imgToolBar
            // 
            this.imgToolBar.ImageSize = new System.Drawing.Size(32, 32);
            this.imgToolBar.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgToolBar.ImageStream")));
            // 
            // txtDate
            // 
            this.txtDate.Caption = "Date";
            this.txtDate.Edit = this.repositoryItemDateEdit1;
            this.txtDate.Id = 2;
            this.txtDate.Name = "txtDate";
            this.txtDate.Width = 120;
            this.txtDate.EditValueChanged += new System.EventHandler(this.txtDate_EditValueChanged);
            this.txtDate.ShowingEditor += new DevExpress.XtraBars.ItemCancelEventHandler(this.txtDate_ShowingEditor);
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            this.repositoryItemDateEdit1.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // cmdNew
            // 
            this.cmdNew.Caption = "&New";
            this.cmdNew.Id = 4;
            this.cmdNew.ImageIndex = 0;
            this.cmdNew.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N));
            this.cmdNew.LargeWidth = 80;
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.cmdNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.cmdNew_ItemClick);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Caption = "Edit";
            this.cmdEdit.Id = 5;
            this.cmdEdit.ImageIndex = 1;
            this.cmdEdit.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E));
            this.cmdEdit.LargeWidth = 80;
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.cmdEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.cmdEdit_ItemClick);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Caption = "&Delete";
            this.cmdDelete.Id = 6;
            this.cmdDelete.ImageIndex = 2;
            this.cmdDelete.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D));
            this.cmdDelete.LargeWidth = 80;
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.cmdDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.cmdDelete_ItemClick);
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "Skin";
            this.barSubItem1.Id = 10;
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 11;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "Skin";
            this.barButtonItem3.Id = 18;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "E&xit";
            this.barButtonItem4.Id = 19;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Caption = "Refresh";
            this.cmdRefresh.Id = 20;
            this.cmdRefresh.ImageIndex = 4;
            this.cmdRefresh.LargeWidth = 80;
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.cmdRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.cmdRefresh_ItemClick);
            // 
            // cboSkins
            // 
            this.cboSkins.Caption = "Appearance";
            this.cboSkins.Edit = this.cboSkinsEditor;
            this.cboSkins.Id = 21;
            this.cboSkins.Name = "cboSkins";
            this.cboSkins.Width = 150;
            // 
            // cboSkinsEditor
            // 
            this.cboSkinsEditor.AutoHeight = false;
            this.cboSkinsEditor.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSkinsEditor.Name = "cboSkinsEditor";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "barButtonItem2";
            this.barButtonItem2.Id = 22;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // pagToolBar
            // 
            this.pagToolBar.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2,
            this.ribbonPageGroup3});
            this.pagToolBar.Name = "pagToolBar";
            this.pagToolBar.Text = "ToolBar";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.txtDate);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.ShowCaptionButton = false;
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.cmdNew);
            this.ribbonPageGroup2.ItemLinks.Add(this.cmdEdit);
            this.ribbonPageGroup2.ItemLinks.Add(this.cmdDelete);
            this.ribbonPageGroup2.ItemLinks.Add(this.cmdRefresh);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.ShowCaptionButton = false;
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.cboSkins);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.ShowCaptionButton = false;
            // 
            // repositoryItemDateEdit2
            // 
            this.repositoryItemDateEdit2.AutoHeight = false;
            this.repositoryItemDateEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit2.Name = "repositoryItemDateEdit2";
            this.repositoryItemDateEdit2.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 595);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbonBar;
            this.ribbonStatusBar.Size = new System.Drawing.Size(854, 25);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "outpatientdept1.ico");
            this.imageList1.Images.SetKeyName(1, "outpatientdept2.ico");
            this.imageList1.Images.SetKeyName(2, "outpatientdept3.ico");
            this.imageList1.Images.SetKeyName(3, "outpatientdept4.ico");
            this.imageList1.Images.SetKeyName(4, "inpatientdept1.ico");
            this.imageList1.Images.SetKeyName(5, "inpatientdept2.ico");
            this.imageList1.Images.SetKeyName(6, "inpatientdept10.ico");
            this.imageList1.Images.SetKeyName(7, "inpatientdept3.ico");
            this.imageList1.Images.SetKeyName(8, "inpatientdept4.ico");
            this.imageList1.Images.SetKeyName(9, "inpatientdept7.ico");
            this.imageList1.Images.SetKeyName(10, "inpatientdept5.ico");
            this.imageList1.Images.SetKeyName(11, "inpatientdept6.ico");
            this.imageList1.Images.SetKeyName(12, "inpatientdept8.ico");
            this.imageList1.Images.SetKeyName(13, "inpatientdept9.ico");
            this.imageList1.Images.SetKeyName(14, "diagnosesandtreatments1.ico");
            this.imageList1.Images.SetKeyName(15, "diagnosesandtreatments1.ico");
            this.imageList1.Images.SetKeyName(16, "diagnosesandtreatments2.ico");
            this.imageList1.Images.SetKeyName(17, "diagnosesandtreatments6.ico");
            this.imageList1.Images.SetKeyName(18, "diagnosesandtreatments2.ico");
            this.imageList1.Images.SetKeyName(19, "generalsetup10.ico");
            this.imageList1.Images.SetKeyName(20, "lab1.ico");
            this.imageList1.Images.SetKeyName(21, "lab2.ico");
            this.imageList1.Images.SetKeyName(22, "lab2.ico");
            this.imageList1.Images.SetKeyName(23, "lab3.ico");
            this.imageList1.Images.SetKeyName(24, "lab2.ico");
            this.imageList1.Images.SetKeyName(25, "lab4.ico");
            this.imageList1.Images.SetKeyName(26, "lab5.ico");
            this.imageList1.Images.SetKeyName(27, "billingsetup6.ico");
            this.imageList1.Images.SetKeyName(28, "billingsetup6.ico");
            this.imageList1.Images.SetKeyName(29, "billingsetup6.ico");
            this.imageList1.Images.SetKeyName(30, "billingsetup6.ico");
            this.imageList1.Images.SetKeyName(31, "billingsetup6.ico");
            this.imageList1.Images.SetKeyName(32, "inpatientdept4.ico");
            this.imageList1.Images.SetKeyName(33, "outpatientdept2.ico");
            this.imageList1.Images.SetKeyName(34, "rch2.ico");
            this.imageList1.Images.SetKeyName(35, "rch3.ico");
            this.imageList1.Images.SetKeyName(36, "rch4.ico");
            this.imageList1.Images.SetKeyName(37, "rch5.ico");
            this.imageList1.Images.SetKeyName(38, "diagnosesandtreatments6.ico");
            this.imageList1.Images.SetKeyName(39, "inventory1.ico");
            this.imageList1.Images.SetKeyName(40, "inventory2.ico");
            this.imageList1.Images.SetKeyName(41, "inventory3.ico");
            this.imageList1.Images.SetKeyName(42, "inventory4.ico");
            this.imageList1.Images.SetKeyName(43, "inventory5.ico");
            this.imageList1.Images.SetKeyName(44, "inventory6.ico");
            this.imageList1.Images.SetKeyName(45, "customers1.ico");
            this.imageList1.Images.SetKeyName(46, "customers2.ico");
            this.imageList1.Images.SetKeyName(47, "customers3.ico");
            this.imageList1.Images.SetKeyName(48, "customers4.ico");
            this.imageList1.Images.SetKeyName(49, "customers5.ico");
            this.imageList1.Images.SetKeyName(50, "customers6.ico");
            this.imageList1.Images.SetKeyName(51, "billing1.ico");
            this.imageList1.Images.SetKeyName(52, "billing2.ico");
            this.imageList1.Images.SetKeyName(53, "billing3.ico");
            this.imageList1.Images.SetKeyName(54, "billing4.ico");
            this.imageList1.Images.SetKeyName(55, "billing6.ico");
            this.imageList1.Images.SetKeyName(56, "billingsetup6.ico");
            this.imageList1.Images.SetKeyName(57, "billingsetup6.ico");
            this.imageList1.Images.SetKeyName(58, "billingsetup6.ico");
            this.imageList1.Images.SetKeyName(59, "billingsetup6.ico");
            this.imageList1.Images.SetKeyName(60, "billingsetup6.ico");
            this.imageList1.Images.SetKeyName(61, "inventory3.ico");
            this.imageList1.Images.SetKeyName(62, "data_delete.ico");
            this.imageList1.Images.SetKeyName(63, "doc_edit.ico");
            this.imageList1.Images.SetKeyName(64, "mtuha1.ico");
            this.imageList1.Images.SetKeyName(65, "mtuha2.ico");
            this.imageList1.Images.SetKeyName(66, "mtuha3.ico");
            this.imageList1.Images.SetKeyName(67, "billingsetup1.ico");
            this.imageList1.Images.SetKeyName(68, "billingsetup2.ico");
            this.imageList1.Images.SetKeyName(69, "billingsetup3.ico");
            this.imageList1.Images.SetKeyName(70, "billingsetup4.ico");
            this.imageList1.Images.SetKeyName(71, "billingsetup5.ico");
            this.imageList1.Images.SetKeyName(72, "billingsetup6.ico");
            this.imageList1.Images.SetKeyName(73, "inventorysetup1.ico");
            this.imageList1.Images.SetKeyName(74, "inventorysetup2.ico");
            this.imageList1.Images.SetKeyName(75, "inventorysetup3.ico");
            this.imageList1.Images.SetKeyName(76, "inventorysetup4.ico");
            this.imageList1.Images.SetKeyName(77, "inventorysetup5.ico");
            this.imageList1.Images.SetKeyName(78, "inventorysetup6.ico");
            this.imageList1.Images.SetKeyName(79, "reportdesigner1.ico");
            this.imageList1.Images.SetKeyName(80, "reportdesigner2.ico");
            this.imageList1.Images.SetKeyName(81, "generalsetup1.ico");
            this.imageList1.Images.SetKeyName(82, "generalsetup2.ico");
            this.imageList1.Images.SetKeyName(83, "generalsetup3.ico");
            this.imageList1.Images.SetKeyName(84, "generalsetup4.ico");
            this.imageList1.Images.SetKeyName(85, "generalsetup5.ico");
            this.imageList1.Images.SetKeyName(86, "generalsetup6.ico");
            this.imageList1.Images.SetKeyName(87, "generalsetup7.ico");
            this.imageList1.Images.SetKeyName(88, "generalsetup9.ico");
            this.imageList1.Images.SetKeyName(89, "generalsetup10.ico");
            this.imageList1.Images.SetKeyName(90, "generalsetup11.ico");
            this.imageList1.Images.SetKeyName(91, "securitysetup1.ico");
            this.imageList1.Images.SetKeyName(92, "securitysetup2.ico");
            this.imageList1.Images.SetKeyName(93, "securitysetup3.ico");
            this.imageList1.Images.SetKeyName(94, "securitysetup4.ico");
            this.imageList1.Images.SetKeyName(95, "securitysetup5.ico");
            // 
            // imgReports
            // 
            this.imgReports.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgReports.ImageStream")));
            this.imgReports.TransparentColor = System.Drawing.Color.Transparent;
            this.imgReports.Images.SetKeyName(0, "report.ico");
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageSize = new System.Drawing.Size(24, 24);
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "folder_out.ico");
            this.imageCollection1.Images.SetKeyName(1, "folder_in.ico");
            this.imageCollection1.Images.SetKeyName(2, "add.ico");
            this.imageCollection1.Images.SetKeyName(3, "funnel.ico");
            this.imageCollection1.Images.SetKeyName(4, "home.ico");
            this.imageCollection1.Images.SetKeyName(5, "tables.ico");
            this.imageCollection1.Images.SetKeyName(6, "users.ico");
            this.imageCollection1.Images.SetKeyName(7, "dollar.ico");
            this.imageCollection1.Images.SetKeyName(8, "add2.ico");
            this.imageCollection1.Images.SetKeyName(9, "stats.ico");
            this.imageCollection1.Images.SetKeyName(10, "stats.ico");
            this.imageCollection1.Images.SetKeyName(11, "tools.ico");
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InActiveTabPageHeader;
            this.xtraTabbedMdiManager1.MdiParent = this;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(211, 143);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(6, 452);
            this.splitterControl1.TabIndex = 5;
            this.splitterControl1.TabStop = false;
            // 
            // navBar
            // 
            this.navBar.ActiveGroup = null;
            this.navBar.ContentButtonHint = null;
            this.navBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.navBar.LargeImages = this.imageCollection1;
            this.navBar.Location = new System.Drawing.Point(0, 143);
            this.navBar.Name = "navBar";
            this.navBar.NavigationPaneMaxVisibleGroups = 7;
            this.navBar.NavigationPaneOverflowPanelUseSmallImages = false;
            this.navBar.OptionsNavPane.ExpandedWidth = 140;
            this.navBar.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.navBar.Size = new System.Drawing.Size(211, 452);
            this.navBar.SmallImages = this.imageList1;
            this.navBar.TabIndex = 0;
            this.navBar.Text = "navBarControl1";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 620);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.navBar);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbonBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "frmMain";
            this.Ribbon = this.ribbonBar;
            this.StatusBar = this.ribbonStatusBar;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgToolBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSkinsEditor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonPage pagToolBar;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraBars.BarButtonItem cmdNew;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarButtonItem cmdEdit;
        private DevExpress.XtraBars.BarButtonItem cmdDelete;
        internal DevExpress.XtraBars.Ribbon.RibbonControl ribbonBar;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        internal DevExpress.Utils.ImageCollection imgToolBar;
        internal DevExpress.XtraBars.BarEditItem txtDate;
        private DevExpress.XtraBars.BarButtonItem cmdRefresh;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.BarEditItem cboSkins;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cboSkinsEditor;
        internal System.Windows.Forms.ImageList imageList1;
        internal DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraNavBar.NavBarControl navBar;
        internal System.Windows.Forms.ImageList imgReports;
        private DevExpress.XtraBars.Ribbon.ApplicationMenu applicationMenu1;
        private DevExpress.XtraBars.BarButtonItem cmdActivate;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem mnuHelp;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit2;
    }
}