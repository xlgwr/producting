using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Abstract;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace MachineSystem.SysCommon
{
    class ExcelOperate
    {

    }

    public class ConsExcelDefine : ICloneable
    {
        public string Name;
        public string Text;
        /// <summary>
        /// 信息所对应的位置
        /// </summary>
        public string HeadPlace;

        #region ICloneable 成员

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }

    /// <summary>
    /// excel定义文件
    /// </summary>
    public class ExcelDefine
    {
       
        /// <summary>
        ///免许信息(excel定义)
        /// </summary>
        public List<ConsExcelDefine> License = new List<ConsExcelDefine>
        {
            new ConsExcelDefine(){ Name="SlctValue", Text="选择",HeadPlace ="A1"},
            new ConsExcelDefine(){ Name="ID", Text="ID",HeadPlace ="B1"},
            new ConsExcelDefine(){ Name="UserID", Text="工号",HeadPlace ="C1"},
            new ConsExcelDefine(){ Name= "UserName", Text="姓名", HeadPlace ="D1"},
            new ConsExcelDefine(){ Name= "partName", Text="部门", HeadPlace ="E1"},
            new ConsExcelDefine(){ Name= "JobForName", Text="向别", HeadPlace ="F1"},
            new ConsExcelDefine(){ Name= "ProjectName", Text="工程别", HeadPlace ="G1"},
             new ConsExcelDefine(){ Name= "LineName", Text="线别", HeadPlace ="H1"},
              new ConsExcelDefine(){ Name= "FlagName", Text="新增加", HeadPlace ="I1"},
               new ConsExcelDefine(){ Name= "TeamName", Text="班别", HeadPlace ="J1"},
               new ConsExcelDefine(){ Name= "GuanweiName", Text="关位", HeadPlace ="K1"},
               new ConsExcelDefine(){ Name= "DutyName", Text="职位", HeadPlace ="L1"},
               new ConsExcelDefine(){ Name= "User_Status", Text="状态", HeadPlace ="M1"},
               new ConsExcelDefine(){ Name= "LicenseType", Text="免许类型", HeadPlace ="N1"},
               new ConsExcelDefine(){ Name= "ValidDate", Text="有效期限", HeadPlace ="O1"},
                new ConsExcelDefine(){ Name= "Oper", Text="操作员", HeadPlace ="P1"},
                 new ConsExcelDefine(){ Name= "OperTime", Text="操作时间", HeadPlace ="Q1"}
        };
     
       

    }
    public class ExcelHelp
    {
        /// <summary>
        /// 完整的文件名(包括路径)
        /// </summary>
        public string FileName;
        /// <summary>
        /// 指导书
        /// </summary>
        public string Sheet1Name = "CustomerOrder";
        /// <summary>
        /// 数据信息
        /// </summary>
        public string Sheet2Name = "CustomerOrderInfo";
        /// <summary>
        /// 构造数据时的起始行

        /// </summary>
        public static  int StartRowIndex = 3;

        private object Missing = System.Reflection.Missing.Value;
        /// <summary>
        /// 模板文件名

        /// </summary>
        public string ModelFileName = "CustomerOrder.xls";
        //private LcsExcelDefineList LcsList = new LcsExcelDefineList();
        public bool WriteDataBaseDateExel(System.Data.DataTable dt)
        {
            bool IsSuccess = false;
            string ModelFullName = AppDomain.CurrentDomain.BaseDirectory.ToString() + ModelFileName;
            //判断模板文件存在
            if (!File.Exists(ModelFullName))
            {
                XtraMsgBox.Show("模板文件不存在!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }



            Excel.Application exapp = new Excel.Application(); ;  //'定义excel应用程序

            Excel.Workbook exbook = null;//       '定义工作簿
            Excel.Worksheet exsheet2 = null;//      数据工作簿
            Excel.Worksheet exsheet1 = null;//指导工作


            ////区域信息
            //Object oBooks = exapp.Workbooks;
            //System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ja-JP");
            //oBooks.GetType().InvokeMember("Add", System.Reflection.BindingFlags.InvokeMethod, null, oBooks, null, ci);
            try
            {


                exapp.Visible = false;
                exapp.UserControl = true;

                int StartCollon = 0;

                string[,] w_strValue = new string[dt.Rows.Count, dt.Columns.Count];
                //将数据转换成array
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        w_strValue[j, i] = dt.Rows[j][i].ToString();
                    }
                }

                //打开excel
                exbook = exapp.Workbooks.Open(ModelFullName, Missing, Missing, Missing, Missing, Missing, Missing
                                                , Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing);
                //exsheet2 = (Excel.Worksheet)exbook.Worksheets;

                //从excel中获取sheet数据
                foreach (Excel.Worksheet item in exbook.Worksheets)
                {
                    if (item.Name == Sheet2Name)
                    {
                        if (exsheet2 == null)
                            exsheet2 = item;

                    }
                    //else if (item.Name == Sheet1Name)
                    //{
                    //    if (exsheet1 == null)
                    //        exsheet1 = item;
                    //}
                    //if (exsheet1 != null && exsheet2 != null)
                    //    break;
                    if ( exsheet2 != null)
                        break;
                }
                //添加数据
                //exsheet2.get_Range(exsheet2.Cells[5, 1], exsheet2.Cells[StartRowIndex + dt.Rows.Count - 1, StartCollon + dt.Columns.Count]).Style = exsheet2.get_Range(exsheet2.Cells[StartRowIndex, 1], exsheet2.Cells[StartRowIndex, StartCollon + dt.Columns.Count]).Style;
                exsheet2.get_Range(exsheet2.Cells[StartRowIndex, 1], exsheet2.Cells[StartRowIndex + dt.Rows.Count - 1, StartCollon + dt.Columns.Count]).Value2 = w_strValue;
                //保存数据
                exbook.RefreshAll();
                //保存数据
                exbook.SaveCopyAs(FileName);
                //退出excel
                exbook.Close(false, Missing, Missing);
                IsSuccess = true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                exbook = null;
                exapp.Quit();
                exapp = null;
                GC.Collect();
            }
            return IsSuccess;
        }
        public bool WriteExcelDateDatabase(ref System.Data.DataTable dt)
        {
            bool IsSuccess = false;

            Excel.Application exapp = new Excel.Application(); ;  //'定义excel应用程序

            Excel.Workbook exbook = null;//       '定义工作簿
            Excel.Worksheet exsheet2 = null;//      数据工作簿
            Excel.Worksheet exsheet1 = null;//指导工作

            System.Array w_ArrayData = null;//需要导出的数组

            int w_ExcelMaxRow = 0;//excel最大行

            int w_ExcelMaxCol = 0;//excel最大列


            ////区域信息
            //Object oBooks = exapp.Workbooks;
            //System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ja-JP");
            //oBooks.GetType().InvokeMember("Add", System.Reflection.BindingFlags.InvokeMethod, null, oBooks, null, ci);
            try
            {

                exapp.Visible = false;
                exapp.UserControl = true;
                //打开excel
                exbook = exapp.Workbooks.Open(FileName, Missing, Missing, Missing, Missing, Missing, Missing
                                                , Missing, Missing, Missing, Missing, Missing, Missing, Missing, Missing);
                //从excel中获取sheet数据
                foreach (Excel.Worksheet item in exbook.Worksheets)
                {
                    if (item.Name == Sheet2Name)
                    {
                        if (exsheet2 == null)
                            exsheet2 = item;

                    }
                    //else if (item.Name == Sheet1Name)
                    //{
                    //    if (exsheet1 == null)
                    //        exsheet1 = item;
                    //}
                    //if (exsheet1 != null && exsheet2 != null)
                    //    break;
                    if (exsheet2 != null)
                        break;
                }

                //读取使用的最大的行数/列

                w_ExcelMaxRow = exsheet2.UsedRange.Rows.Count;
                w_ExcelMaxCol = exsheet2.UsedRange.Columns.Count;
                w_ArrayData = (System.Array)exsheet2.get_Range(exsheet2.Cells[StartRowIndex, 1], exsheet2.Cells[w_ExcelMaxRow, w_ExcelMaxCol]).Value2;
                //for (int i = 0; i < w_ArrayData.Length; i++)
                //{
                //    string tt = w_ArrayData.GetValue(0, 2);
                //}
                DataRow dr;
                for (int i = 0; i < w_ExcelMaxRow - StartRowIndex + 1; i++)
                {
                    dr = dt.NewRow();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        dr[j] = w_ArrayData.GetValue(i + 1, j + 1);
                    }
                    dt.Rows.Add(dr);

                }
                dt.AcceptChanges();
                //退出excel
                exbook.Close(false, Missing, Missing);
                IsSuccess = true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                exbook = null;
                exapp.Quit();
                exapp = null;
                GC.Collect();
            }
            return IsSuccess;
        }

    }
}
