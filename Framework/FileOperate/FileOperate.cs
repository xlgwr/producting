using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using Framework.Libs;

namespace Framework.FileOperate
{
    public partial class FileOperate 
    {
        
        /// <summary>
        /// 导出Excel过程
        /// </summary>
        /// <param name="grdList">数据表格</param>
        public static void ExportExcel(DevExpress.XtraGrid.Views.Grid.GridView grdList)
        {
            string fileName = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                grdList.ExportToXls(fileName);

            //DevExpress.XtraGrid.Export.GridViewExportLink  gvlink;
            //using (DevExpress.XtraExport.ExportXlsProvider provider = new DevExpress.XtraExport.ExportXlsProvider(fileName))
            //{
            //    gvlink = (DevExpress.XtraGrid.Export.GridViewExportLink)grdList.CreateExportLink(provider);
            //    gvlink.ExportAll = true;
            //    gvlink.ExpandAll = true;
            //    gvlink.ExportDetails = true;
            //    gvlink.ExportTo(true);

                Framework.Abstract.ExcelMsg.ExcelMsgInfo.Show("EXCEL文件导出", "表格数据已成功导出!", fileName);

                //Framework.Abstract.frmExcelMsg frm = new Framework.Abstract.frmExcelMsg();
                //frm.ShowDialog();

                //"Export listgrid data to excel file successful!" ,"File name:" 
               // DevExpress.XtraEditors.XtraMessageBox.Show("表格数据已成功导出!" + "\r\n文件名：" + fileName, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //OpenFile(fileName);
            //}
            }
        }


        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="grdList">数据表格</param>
        public static void ExportExcelList(DevExpress.XtraGrid.Views.Grid.GridView grdList)
        {
            string fileName = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                grdList.ExportToXls(fileName);
                DevExpress.XtraEditors.XtraMessageBox.Show("表格数据已成功导出!" + "\r\n文件名：" + fileName, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        /// <summary>
        /// 打开Excel文档对话框
        /// </summary>
        /// <param name="fileName">文件名</param>
        public static void OpenFile(string fileName)
        {
            if (DevExpress.XtraEditors.XtraMessageBox.Show("Do you want to open this file?", "Export To...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = fileName;
                    process.StartInfo.Verb = "Open";
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    process.Start();
                }
                catch
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Cannot find an application on your system suitable for openning the file with exported data.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 保存文件对话框
        /// </summary>
        /// <param name="title">窗体Caption</param>
        /// <param name="filter">文件过滤条件</param>
        /// <returns>文件名</returns>
        public static string ShowSaveFileDialog(string title, string filter)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            string name = Application.ProductName + System.DateTime.Now.ToString("yyyyMMddHHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            int n = name.LastIndexOf(".") + 1;
            if (n > 0) name = name.Substring(n, name.Length - n);
            //"Export To "
            dlg.Title = "文件保存";
            dlg.FileName = name;
            dlg.Filter = filter;
            
            if (dlg.ShowDialog() == DialogResult.OK) return dlg.FileName;
            return "";
        }


        /// <summary>
        /// 保存文件对话框
        /// </summary>
        /// <param name="title">窗体Caption</param>
        /// <param name="filter">文件过滤条件</param>
        /// <returns>文件名</returns>
        public static string ShowSaveFileDialog(string title, string filter, string ModelFile)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            string name = Application.ProductName + "_" + ModelFile;//System.DateTime.Now.ToString("yyyyMMddHHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            int n = name.LastIndexOf(".") + 1;
            if (n > 0) name = name.Substring(n, name.Length - n);
            //"Export To "
            dlg.Title = "文件保存";
            dlg.FileName = name;
            dlg.Filter = filter;
            if (dlg.ShowDialog() == DialogResult.OK) return dlg.FileName;
            return "";
        }



        /// <summary>
        /// 打开文件对话框
        /// </summary>
        /// <param name="title">窗体Caption</param>
        /// <param name="filter">文件过滤条件</param>
        /// <returns>文件名</returns>
        public static string ShowOpenFileDialog(string title, string filter)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            //"Open a file " 
            dlg.Title = title;
            dlg.Filter = filter;
            if (dlg.ShowDialog() == DialogResult.OK) return dlg.FileName;
            return "";
        }

        /// <summary>
        /// 数据表格打印预览
        /// </summary>
        /// <param name="ListGrid">数据表格</param>
        /// <param name="strHead">表头显示信息</param>
        public static void PrintView(DevExpress.XtraGrid.GridControl ListGrid,DevExpress.XtraGrid.Views.Grid.GridView gridView, string strHead)
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            //"Report file creating","Read Data, Please waiting..."
            DevExpress.Utils.WaitDialogForm frmWait = new DevExpress.Utils.WaitDialogForm("正在获取数据，请稍候...", "表格报表");
            frmWait.Show();

            //以下以前的一种用法，现在改为可加上表头信息的印方式
            //if (DevExpress.XtraPrinting.PrintHelper.IsPrintingAvailable)
            //    DevExpress.XtraPrinting.PrintHelper.ShowPreview(ListGrid);

            //else
            //    //"XtraPrinting Library is not found..."
            //    DevExpress.XtraEditors.XtraMessageBox.Show(common.GetLanguageWord("MESSAGE", "M0028"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //以下为采用新的打印方式
            string middleColumn = strHead;
            string rightColumn = string.Format("打印时间:{0:g}", DateTime.Now);
            PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());


            PageHeaderFooter phf = link.PageHeaderFooter as PageHeaderFooter;

            phf.Header.Content.Clear();
            phf.Header.Font = new Font("宋体", 12, FontStyle.Bold);

            phf.Header.Content.AddRange(new string[] { "打印用户:" + Common._personname, middleColumn, rightColumn });
            phf.Footer.Content.AddRange(new string[] { "", "页次[页 #/#]", "" });

            gridView.OptionsPrint.AutoWidth = false;    //<- 2012-09-18 add

            link.Component = ListGrid;
           


            //link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);
          
            DevExpress.LookAndFeel.UserLookAndFeel lookfeel = new DevExpress.LookAndFeel.UserLookAndFeel(ListGrid);
            lookfeel.ParentLookAndFeel = ListGrid.LookAndFeel;
            PrintingSystem printSystem = new PrintingSystem();
            PrinterSettingsUsing pst = new PrinterSettingsUsing();
            pst.UseMargins = true;
            pst.UsePaperKind = true;
            printSystem.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            printSystem.PageSettings.PaperName = "A4";
            printSystem.PageSettings.LeftMargin = 1;
            printSystem.PageSettings.RightMargin = 1;
            printSystem.PageSettings.Landscape = true;
            printSystem.PageSettings.AssignDefaultPrinterSettings(pst);
            link.PaperKind = printSystem.PageSettings.PaperKind;
            link.Margins = printSystem.PageSettings.Margins;
            link.Landscape = printSystem.PageSettings.Landscape;
            link.CreateDocument();
            link.ShowPreview(lookfeel);

            Cursor.Current = currentCursor;
            frmWait.Dispose();
            frmWait = null;
        }

        /// <summary>
        /// 打印数据表格时，生成表头信息
        /// </summary>
        /// <param name="sender">数据表格对象</param>
        /// <param name="e"></param>
        private void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            PageInfoBrick brick = e.Graph.DrawPageInfo(PageInfo.DateTime, "", Color.DarkBlue,
               new RectangleF(0, 0, 100, 20), DevExpress.XtraPrinting.BorderSide.None);
            brick.LineAlignment = BrickAlignment.Center;
            brick.Alignment = BrickAlignment.Center;
            brick.AutoWidth = true;
        }

        /// <summary>
        /// 取得Excel单元格的值
        /// </summary>
        /// <param name="xApp">Excel应用程序</param>
        /// <param name="xSheet">工作表</param>
        /// <param name="row">行</param>
        /// <param name="col">列</param>
        /// <returns>返回一个字符串</returns>
        public static string GetCellData(Excel.Application xApp, Excel.Worksheet xSheet, int row, int col, bool txtFlag)
        {
            if(!txtFlag)
            {
                object obj = xApp.get_Range(xSheet.Cells[row, col], xSheet.Cells[row, col]).Value2;
                if (obj != null)
                    return xApp.get_Range(xSheet.Cells[row, col], xSheet.Cells[row, col]).Value2.ToString();
                else
                    return "";
            }
            else
            {
                object obj = xApp.get_Range(xSheet.Cells[row, col], xSheet.Cells[row, col]).Text;
                if (obj != null)
                    return xApp.get_Range(xSheet.Cells[row, col], xSheet.Cells[row, col]).Text.ToString();
                else
                    return "";
            }
           

        }

    }

    public class KillExcelProcess
    {
        public KillExcelProcess()
        {

        }
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        public static void Kill(Excel.Application excel)
        {
            IntPtr t = new IntPtr(excel.Hwnd); //得到这个句柄，具体作用是得到这块内存入口 

            int k = 0;
            GetWindowThreadProcessId(t, out k); //得到本进程唯一标志k 
            System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k); //得到对进程k的引用 
            p.Kill(); //关闭进程k 
        }


    }

}
