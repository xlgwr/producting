namespace Framework.Abstract
{
    partial class frmBaseToolXC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBaseToolXC));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barTool = new DevExpress.XtraBars.Bar();
            this.cmdImport = new DevExpress.XtraBars.BarButtonItem();
            this.cmdNew = new DevExpress.XtraBars.BarButtonItem();
            this.cmdEdit = new DevExpress.XtraBars.BarButtonItem();
            this.cmdDelete = new DevExpress.XtraBars.BarButtonItem();
            this.cmdSave = new DevExpress.XtraBars.BarButtonItem();
            this.cmdCancel = new DevExpress.XtraBars.BarButtonItem();
            this.cmdRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.cmdSearch = new DevExpress.XtraBars.BarButtonItem();
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
            this.cmdNew,
            this.cmdEdit,
            this.cmdDelete,
            this.cmdSave,
            this.cmdCancel,
            this.cmdImport,
            this.cmdExcel,
            this.cmdSearch,
            this.cmdPrint,
            this.cmdExit,
            this.cmdRefresh});
            this.barManager.MaxItemId = 11;
            this.barManager.MdiMenuMergeStyle = DevExpress.XtraBars.BarMdiMenuMergeStyle.WhenChildActivated;
            // 
            // barTool
            // 
            this.barTool.BarName = "Tools";
            this.barTool.DockCol = 0;
            this.barTool.DockRow = 0;
            this.barTool.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barTool.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.cmdImport, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.cmdNew, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.cmdEdit, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.cmdDelete, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.cmdSave, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.cmdCancel, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.cmdRefresh, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.cmdSearch, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.cmdExcel, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.cmdPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.cmdExit, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barTool.Text = "Tools";
            // 
            // cmdImport
            // 
            this.cmdImport.Caption = "导入(F7)";
            this.cmdImport.Id = 5;
            this.cmdImport.ImageIndex = 5;
            this.cmdImport.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7);
            this.cmdImport.Name = "cmdImport";
            this.cmdImport.Tag = "import";
            this.cmdImport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.cmdImport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemClick);
            // 
            // cmdNew
            // 
            this.cmdNew.Caption = "新增(F1)";
            this.cmdNew.Id = 0;
            this.cmdNew.ImageIndex = 16;
            this.cmdNew.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F1);
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Tag = "new";
            this.cmdNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemClick);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Caption = "修改(F2)";
            this.cmdEdit.Id = 1;
            this.cmdEdit.ImageIndex = 10;
            this.cmdEdit.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2);
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Tag = "edit";
            this.cmdEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemClick);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Caption = "删除(F3)";
            this.cmdDelete.Id = 2;
            this.cmdDelete.ImageIndex = 7;
            this.cmdDelete.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Tag = "delete";
            this.cmdDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemClick);
            // 
            // cmdSave
            // 
            this.cmdSave.Caption = "保存(F4)";
            this.cmdSave.Id = 3;
            this.cmdSave.ImageIndex = 17;
            this.cmdSave.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Tag = "save";
            this.cmdSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemClick);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Caption = "取消(F5)";
            this.cmdCancel.Id = 4;
            this.cmdCancel.ImageIndex = 18;
            this.cmdCancel.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Tag = "cancel";
            this.cmdCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemClick);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Caption = "刷新(F6)";
            this.cmdRefresh.Id = 10;
            this.cmdRefresh.ImageIndex = 15;
            this.cmdRefresh.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Tag = "refresh";
            this.cmdRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemClick);
            // 
            // cmdSearch
            // 
            this.cmdSearch.Caption = "检索(F8)";
            this.cmdSearch.Id = 7;
            this.cmdSearch.ImageIndex = 8;
            this.cmdSearch.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Tag = "search";
            this.cmdSearch.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.cmdSearch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemClick);
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
            // 
            // frmBaseToolXC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.ClientSize = new System.Drawing.Size(1005, 492);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Office 2007 Blue";
            this.Name = "frmBaseToolXC";
            this.Text = "frmSearchBasic";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBaseToolXC_FormClosing);
            this.Load += new System.EventHandler(this.frmBaseToolXC_Load);
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
        private DevExpress.XtraBars.BarButtonItem cmdNew;
        private System.Windows.Forms.ImageList imgList;
        private DevExpress.XtraBars.BarButtonItem cmdEdit;
        private DevExpress.XtraBars.BarButtonItem cmdDelete;
        private DevExpress.XtraBars.BarButtonItem cmdSave;
        private DevExpress.XtraBars.BarButtonItem cmdCancel;
        private DevExpress.XtraBars.BarButtonItem cmdImport;
        private DevExpress.XtraBars.BarButtonItem cmdExcel;
        private DevExpress.XtraBars.BarButtonItem cmdSearch;
        private DevExpress.XtraBars.BarButtonItem cmdRefresh;
        private DevExpress.XtraBars.BarButtonItem cmdPrint;
        private DevExpress.XtraBars.BarButtonItem cmdExit;
    }
}
