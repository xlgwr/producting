using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework.Abstract;
using Framework.Libs;

namespace MachineSystem.TabPage
{
    public partial class frmSiteLog : Framework.Abstract.frmSearchBasic2
    {
        #region 变量定义

        #endregion


        #region 画面初始化

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmSiteLog()
        {
            InitializeComponent();

            //初始化表格控件处理
            this.m_GridViewUtil.GridControlList = this.gridControl1;

            //初始化主表对象
            this.m_GridViewUtil.ParentGridView = this.ListData;

            this.TableName = "SiteLog";
        }

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                base.SetFormValue();
                DateTime FirstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime LastDay = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.AddMonths(1).Day);
                dateOperDate1.EditValue = string.Format("{0:yyyy-MM-dd}", FirstDay);
                dateOperDate2.EditValue = string.Format("{0:yyyy-MM-dd}", LastDay);

            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("画面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        #endregion

        #region 画面按钮功能处理方法

        protected override void SetSearchProc(frmSearchBasic2 frmBaseToolXC)
        {
            //base.SetSearchProc(frmBaseToolXC);

            GetDspDataList();
        }

        /// <summary>
        /// 导出
        /// </summary>
        protected override void GetExcelDerivedProc()
        {
            base.GetExcelDerivedProc();
        }
        #endregion

        #region 共同方法

        /// <summary>
        /// 获取表格信息一览
        /// </summary>
        protected override void GetDspDataList()
        {
            try
            {
                //base.GetDspDataList();
                string str_sql = "Select [ID] ,[OperDate] ,[OperNo] ,[OperName]  ,[myTeamName] ,[moduleName]  ,[functName] ,[OperType] ,[Memo] From SiteLog ";
                if (txtOperNo.Text.Trim() != "" || txtOperName.Text.Trim() != "" || dateOperDate1.Text.Trim() != "" || dateOperDate2.Text.Trim() != "")
                {
                    str_sql += " where ";
                    if (txtOperNo.Text.Trim() != "")
                    {
                        str_sql += " OperNo like '" + txtOperNo.Text.Trim() + "' and ";
                    }
                    if (txtOperName.Text.Trim() != "")
                    {
                        str_sql += " OperName like '" + txtOperName.Text.Trim() + "' and ";
                    }
                    if (dateOperDate1.Text.Trim() != "" &&dateOperDate2.Text.Trim()!="")
                    {
                        str_sql += " OperDate  between  '" + dateOperDate1.Text.Trim() + "'  and  '" + dateOperDate2.Text.Trim() + "'  ";
                    }
                    if (lookType.EditValue.ToString() != "")
                    {
                        str_sql += " and OperType like'%" + lookType.EditValue.ToString() + "%'";
                    }
                    
                }
                str_sql += "  order by OperDate desc ";
                DataTable dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (dt.Rows.Count > 0)
                {
                    ExcelButtonEnabled = true;
                }
                else
                {
                    ExcelButtonEnabled = false;
                }
                gridControl1.DataSource = dt;
                ListData.OptionsBehavior.Editable = false;
                ListData.BestFitColumns();
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }


        #endregion
         
    }
}
