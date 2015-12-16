using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.Office.Interop.Excel;

namespace Framework.FileOperate
{
    public  class GridControlToExcel
    {
        /// <summary>
        /// 可以自定义导出Excel的格式，传的参数为GridView
        /// </summary>
        /// <param name="gridView">需要保存的GridView</param>
        /// <param name="sheetName">Sheet名</param>
        /// <param name="filename">文件名</param>
        public static void ExportGridViewToExcel(List<DevExpress.XtraGrid.Views.Grid.GridView> gridView, List<string> sheetName, string filename)
        {
            //System.Data.DataTable dt = (System.Data.DataTable)gridView.DataSource;
            if (gridView.Count == 0)
                return;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = filename;
            sfd.Filter = "Excel files (*xls) | *.xls";
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK && sfd.FileName.Trim() != null)
            {
                int colIndex = 0;
                System.Reflection.Missing miss = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
                xlapp.Visible = false; //是否显示导出过程 ，建议关闭，否则在导出过程中鼠标点击Excel文件时会出错。
                Microsoft.Office.Interop.Excel.Workbooks mBooks = (Microsoft.Office.Interop.Excel.Workbooks)xlapp.Workbooks;
                Microsoft.Office.Interop.Excel.Workbook mBook = (Microsoft.Office.Interop.Excel.Workbook)mBooks.Add(miss);
                Microsoft.Office.Interop.Excel.Worksheet mSheet = (Microsoft.Office.Interop.Excel.Worksheet)mBook.Worksheets.Add(miss, miss, gridView.Count - 1, Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);

                //设置文字自动换行 
                //mSheet.Cells.WrapText = true;
                try
                {

                    int gridViewIndex = 1;
                    foreach (GridView gd in gridView)
                    {
                        int rowIndex = 1;
                        Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)mBook.Worksheets[gridViewIndex];
                        sheet.Name = sheetName[gridViewIndex - 1];

                        //设置对齐方式
                        sheet.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        //设置第一行高度，即标题栏
                        ((Microsoft.Office.Interop.Excel.Range)sheet.Rows["1:1", System.Type.Missing]).RowHeight = 20;

                        //设置数据行行高度
                        ((Microsoft.Office.Interop.Excel.Range)sheet.Rows["2:" + gd.RowCount + 1, System.Type.Missing]).RowHeight = 16;

                        //设置字体大小（10号字体）
                        sheet.Range[sheet.Cells[1, 1], sheet.Cells[gd.RowCount + 1, gd.Columns.Count]].Font.Size = 10;

                        //设置单元格边框
                        Microsoft.Office.Interop.Excel.Range range1 = sheet.Range[sheet.Cells[1, 1], sheet.Cells[gd.RowCount + 1, gd.Columns.Count]];

                        //写标题
                        for (int row = 1; row <= gd.Columns.Count; row++)
                        {
                            sheet.Cells[1, row] = gd.Columns[row - 1].GetTextCaption();
                        }
                        for (int i = 0; i < gd.RowCount; i++)
                        {
                            rowIndex++;
                            colIndex = 0;
                            for (int j = 0; j < gd.Columns.Count; j++)
                            {
                                colIndex++;
                                sheet.Cells[rowIndex, colIndex] = gd.GetRowCellValue(i, gd.Columns[j]);
                            }
                        }
                        gridViewIndex++;
                    }
                    mBook.SaveAs(sfd.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel7, miss, miss, miss, miss, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                                 miss, miss, miss, miss, miss);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                finally
                {
                    mBooks.Close();
                    xlapp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(mSheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(mBook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(mBooks);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlapp);
                    GC.Collect();
                }
            }
            else
            {
                //return false;
            }
        }

        /// <summary>
        /// 可以自定义导出Excel的格式，传的参数为GridView
        /// </summary>
        /// <param name="gridView">需要保存的GridView</param>
        /// <param name="sheetName">Sheet名</param>
        /// <param name="filename">文件名</param>
        public static void ExportGridViewToExcel(DevExpress.XtraGrid.Views.Grid.GridView gridView, string sheetName, string filename)
        {
            //System.Data.DataTable dt = (System.Data.DataTable)gridView.DataSource;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = filename;
            sfd.Filter = "Excel files (*xls) | *.xls";
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK && sfd.FileName.Trim() != null)
            {
                int colIndex = 0;
                System.Reflection.Missing miss = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
                xlapp.Visible = false; //是否显示导出过程 ，建议关闭，否则在导出过程中鼠标点击Excel文件时会出错。
                Microsoft.Office.Interop.Excel.Workbooks mBooks = (Microsoft.Office.Interop.Excel.Workbooks)xlapp.Workbooks;
                Microsoft.Office.Interop.Excel.Workbook mBook = (Microsoft.Office.Interop.Excel.Workbook)mBooks.Add(miss);
                Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)mBook.Worksheets.Add(miss, miss, 0, Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);

                //设置文字自动换行 
                //mSheet.Cells.WrapText = true;
                try
                {

                    int gridViewIndex = 1;
                    int rowIndex = 1;
                    sheet.Name = sheetName;

                    //设置对齐方式
                    sheet.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    //设置第一行高度，即标题栏
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Rows["1:1", System.Type.Missing]).RowHeight = 20;

                    //设置数据行行高度
                    ((Microsoft.Office.Interop.Excel.Range)sheet.Rows["2:" + gridView.RowCount + 1, System.Type.Missing]).RowHeight = 16;

                    //设置字体大小（10号字体）
                    sheet.Range[sheet.Cells[1, 1], sheet.Cells[gridView.RowCount + 1, gridView.Columns.Count]].Font.Size = 10;

                    //设置单元格边框
                    Microsoft.Office.Interop.Excel.Range range1 = sheet.Range[sheet.Cells[1, 1], sheet.Cells[gridView.RowCount + 1, gridView.Columns.Count]];

                    //写标题
                    for (int row = 1; row <= gridView.Columns.Count; row++)
                    {
                        sheet.Cells[1, row] = gridView.Columns[row - 1].GetTextCaption();
                    }
                    for (int i = 0; i < gridView.RowCount; i++)
                    {
                        rowIndex++;
                        colIndex = 0;
                        for (int j = 0; j < gridView.Columns.Count; j++)
                        {
                            colIndex++;
                            sheet.Cells[rowIndex, colIndex] = gridView.GetRowCellValue(i, gridView.Columns[j]);
                        }
                    }
                    gridViewIndex++;

                    mBook.SaveAs(sfd.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel7, miss, miss, miss, miss, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                                 miss, miss, miss, miss, miss);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                finally
                {
                    mBooks.Close();
                    xlapp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(sheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(mBook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(mBooks);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlapp);
                    GC.Collect();
                }
            }
            else
            {
                //return false;
            }
        }
    }
}
