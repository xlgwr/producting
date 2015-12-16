namespace Framework.Abstract
{
    partial class frmSearchBasic
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearchBasic));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barTool = new DevExpress.XtraBars.Bar();
            this.cmdImport = new DevExpress.XtraBars.BarButtonItem();
            this.cmdSearch = new DevExpress.XtraBars.BarButtonItem();
            this.cmdSave = new DevExpress.XtraBars.BarButtonItem();
            this.cmdSelectAll = new DevExpress.XtraBars.BarButtonItem();
            this.cmdSelectOff = new DevExpress.XtraBars.BarButtonItem();
            this.cmdExcel = new DevExpress.XtraBars.BarButtonItem();
            this.cmdPrint = new DevExpress.XtraBars.BarButtonItem();
            this.cmdExit = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.validData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barTool});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Images = this.imgList;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.cmdSave,
            this.cmdExcel,
            this.cmdSearch,
            this.cmdPrint,
            this.cmdExit,
            this.cmdSelectAll,
            this.cmdSelectOff,
            this.cmdImport});
            this.barManager.MaxItemId = 14;
            this.barManager.MdiMenuMergeStyle = DevExpress.XtraBars.BarMdiMenuMergeStyle.WhenChildActivated;
            // 
            // barTool
            // 
            this.barTool.BarName = "Tools";
            this.barTool.DockCol = 0;
            this.barTool.DockRow = 0;
            this.barTool.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barTool.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.cmdImport, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.cmdSearch, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.cmdSave, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.cmdSelectAll, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.cmdSelectOff, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.cmdExcel, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.cmdPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.cmdExit, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barTool.Text = "Tools";
            // 
            // cmdImport
            // 
            this.cmdImport.Caption = "导入(F7)";
            this.cmdImport.Id = 13;
            this.cmdImport.ImageIndex = 5;
            this.cmdImport.Name = "cmdImport";
            this.cmdImport.Tag = "import";
            this.cmdImport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.cmdImport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemClick);
            // 
            // cmdSearch
            // 
            this.cmdSearch.Caption = "检索(F4)";
            this.cmdSearch.Id = 7;
            this.cmdSearch.ImageIndex = 8;
            this.cmdSearch.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Tag = "search";
            this.cmdSearch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemClick);
            // 
            // cmdSave
            // 
            this.cmdSave.Caption = "处理(F5)";
            this.cmdSave.Id = 3;
            this.cmdSave.ImageIndex = 10;
            this.cmdSave.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Tag = "save";
            this.cmdSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemClick);
            // 
            // cmdSelectAll
            // 
            this.cmdSelectAll.Caption = "全选(F8)";
            this.cmdSelectAll.Id = 11;
            this.cmdSelectAll.ImageIndex = 20;
            this.cmdSelectAll.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8);
            this.cmdSelectAll.Name = "cmdSelectAll";
            this.cmdSelectAll.Tag = "selectall";
            this.cmdSelectAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemClick);
            // 
            // cmdSelectOff
            // 
            this.cmdSelectOff.Caption = "全非选(F9)";
            this.cmdSelectOff.Id = 12;
            this.cmdSelectOff.ImageIndex = 19;
            this.cmdSelectOff.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9);
            this.cmdSelectOff.Name = "cmdSelectOff";
            this.cmdSelectOff.Tag = "selectoff";
            this.cmdSelectOff.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemClick);
            // 
            // cmdExcel
            // 
            this.cmdExcel.Caption = "导出(F10)";
            this.cmdExcel.Id = 6;
            this.cmdExcel.ImageIndex = 0;
            this.cmdExcel.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F10);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Tag = "excel";
            this.cmdExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemClick);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Caption = "打印(F11)";
            this.cmdPrint.Id = 8;
            this.cmdPrint.ImageIndex = 1;
            this.cmdPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F11);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Tag = "print";
            this.cmdPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemClick);
            // 
            // cmdExit
            // 
            this.cmdExit.Caption = "退出(F12)";
            this.cmdExit.Id = 9;
            this.cmdExit.ImageIndex = 2;
            this.cmdExit.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Tag = "exit";
            this.cmdExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemClick);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Magenta;
            this.imgList.Images.SetKeyName(0, "");
            this.imgList.Images.SetKeyName(1, "");
            this.imgList.Images.SetKeyName(2, "");
            this.imgList.Images.SetKeyName(3, "");
            this.imgList.Images.SetKeyName(4, "");
            this.imgList.Images.SetKeyName(5, "sql_script_open16_h.bmp");
            this.imgList.Images.SetKeyName(6, "import.ico");
            this.imgList.Images.SetKeyName(7, "gif_45_025-16x16.png");
            this.imgList.Images.SetKeyName(8, "Zoom-Out.gif");
            this.imgList.Images.SetKeyName(9, "find.ico");
            this.imgList.Images.SetKeyName(10, "edit-2.ico");
            this.imgList.Images.SetKeyName(11, "filter-2.ico");
            this.imgList.Images.SetKeyName(12, "barcode.ico");
            this.imgList.Images.SetKeyName(13, "binoculars-2.ico");
            this.imgList.Images.SetKeyName(14, "print.ico");
            this.imgList.Images.SetKeyName(15, "refresh.ico");
            this.imgList.Images.SetKeyName(16, "file add.ico");
            this.imgList.Images.SetKeyName(17, "save.ico");
            this.imgList.Images.SetKeyName(18, "redo.ico");
            this.imgList.Images.SetKeyName(19, "minus.png");
            this.imgList.Images.SetKeyName(20, "add.png");
            // 
            // frmSearchBasic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.ClientSize = new System.Drawing.Size(1005, 492);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Office 2007 Blue";
            this.Name = "frmSearchBasic";
            this.Text = "frmSearchBasic";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBaseToolXC_FormClosing);
            this.Load += new System.EventHandler(this.frmSearchBasic_Load);
            ((System.ComponentModel.ISupportInitialize)(this.validData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar barTool;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.ImageList imgList;
        private DevExpress.XtraBars.BarButtonItem cmdSave;
        private DevExpress.XtraBars.BarButtonItem cmdExcel;
        private DevExpress.XtraBars.BarButtonItem cmdSearch;
        private DevExpress.XtraBars.BarButtonItem cmdPrint;
        private DevExpress.XtraBars.BarButtonItem cmdExit;
        private DevExpress.XtraBars.BarButtonItem cmdSelectAll;
        private DevExpress.XtraBars.BarButtonItem cmdSelectOff;
        private DevExpress.XtraBars.BarButtonItem cmdImport;
    }
}
