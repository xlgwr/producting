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
using DevExpress.XtraEditors.DXErrorProvider;
using MachineSystem.form.ParaLicense;
using MachineSystem.SysDefine;
using System.Collections.Specialized;
using log4net;
/********************************************************************************
** 作者： xxx
** 创始时间：2015-6-8

** 修改人：xuensheng
** 修改时间：2015-7-9
** 修改内容：代码规范化

** 描述：
**    主要用于排班登记信息的资料资料查询、删除、批量删除操作
*********************************************************************************/

namespace MachineSystem.TabPage
{
    public partial class frmTeamShow_TeamSet :  Framework.Abstract.frmSearchBasic2
    {
        #region 变量定义
        /// <summary>
        /// 数据表
        /// </summary>
        DataTable m_tblDataList = new DataTable();
        DateTime dtBegin = DateTime.Now;
        DateTime dtEnd = DateTime.Now; 
         // 日志
        private static readonly ILog log = LogManager.GetLogger(typeof(frmTeamShow_TeamSet));
        /// 界面初始化标示
        bool isLoad = true;
        #endregion

        #region 画面初始化
        public frmTeamShow_TeamSet()
        {
            InitializeComponent();

            this.m_GridViewUtil.GridControlList = this.gridControl1;
            this.m_GridViewUtil.ParentGridView = this.gridView1;
            m_ParenSlctColName = "SlctValue";
        }

     

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                base.SetFormValue();
                //初始化日期
                 dateBeginDate.EditValue = new DateTime(DateTime.Now.Year, 1, 1);
                dateEndDate.EditValue = new DateTime(DateTime.Now.Year, 12, 31);
                //获取检索条件中下拉框数据
                GetSelectLookUpList();
                if (Common._myTeamName!="") {
                    lookUpEditmyTeamName.Text = Common._myTeamName;
                }
                
                isLoad = false;
           
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("画面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        #endregion

        #region 画面按钮功能处理方法
        /// <summary>
        /// 检索
        /// </summary>
        /// <param name="isClear"></param>
        protected override void SetSearchProc(frmSearchBasic2 frmBaseToolXC)
        {
            base.SetSearchProc(frmBaseToolXC);
            if ((dateBeginDate.EditValue == null || dateBeginDate.EditValue.ToString() == "") && (dateEndDate.EditValue != null || dateEndDate.EditValue.ToString() != ""))
            {
                DataValid.ShowErrorInfo(this.ErrorInfo, this.dateBeginDate, "请选择开始时间!");
                return;
            }
            if ((dateBeginDate.EditValue != null || dateBeginDate.EditValue.ToString() != "") && (dateEndDate.EditValue == null || dateEndDate.EditValue.ToString() == ""))
            {
                DataValid.ShowErrorInfo(this.ErrorInfo, this.dateEndDate, "请选择结束时间!");
                return;
            }
            if ((dateBeginDate.EditValue != null && dateBeginDate.EditValue.ToString() != "") && (dateEndDate.EditValue != null && dateEndDate.EditValue.ToString() != ""))
            {

                dtBegin = DateTime.Parse(dateBeginDate.EditValue.ToString());
                dtEnd = DateTime.Parse(dateEndDate.EditValue.ToString());

                if (dtEnd < dtBegin)
                {
                    DataValid.ShowErrorInfo(this.ErrorInfo, this.dateEndDate, "请选择开始时间小于结束时间!");
                    return;
                }
            }

        }

        

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="isClear"></param>
        protected override void SetInsertInit(bool isClear)
        {
            base.SetInsertInit(isClear);
            try
            {
                frmTeamShow_TeamSetAdd frm = new frmTeamShow_TeamSetAdd();
                frm.ScanMode = Common.DataModifyMode.add;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    this.ScanMode = Common.DataModifyMode.dsp;
                    GetDspDataList();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("新增数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        public override void SetModifyInit()
        {
            base.SetModifyInit();
            try
            {
                if (this.GetSelectList().Length <= 0)
                {
                    XtraMsgBox.Show("未勾选任何数据！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
               
                frmTeamShow_TeamSetAdd frm = new frmTeamShow_TeamSetAdd();
                frm.ScanMode = Common.DataModifyMode.upd;
                //选择所有选择的数据
                frm.drs = this.GetSelectList();
                string myTeamNm = frm.drs[0]["myTeamName"].ToString();
                for (int i = 0; i < frm.drs.Length; i++)
                {
                    if (myTeamNm != frm.drs[i]["myTeamName"].ToString())
                    {
                        XtraMsgBox.Show("请选择相同向别-班别加班数据，才可以修改！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                 }

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    this.ScanMode = Common.DataModifyMode.dsp;
                    GetDspDataList();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("修改数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }
       
        /// <summary>
        /// 删除数据
        /// </summary>
        protected override void SetDeleteInit()
        {
            try
            {
                base.SetDeleteInit();
                int result = 0;
                if (gridView1.FocusedRowHandle < 0) return;
                gridView1.CloseEditor();
                gridView1.UpdateCurrentRow();
                //选择所有选择的数据
                DataRow[] _drs = this.GetSelectList();

                //没有选择任何数据情况
                if (_drs.Length <= 0)
                {
                    XtraMsgBox.Show("未勾选任何数据！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (XtraMsgBox.Show("是否删除数据？", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    Common.AdoConnect.Connect.CreateSqlTransaction();
                    if (_drs.Length > 0)
                    {
                        for (int i = 0; i < _drs.Length; i++)
                        {
                            DataRow dr = _drs[i];
                            m_dicItemData = new System.Collections.Specialized.StringDictionary();
                            m_dicItemData["ID"] = dr["ID"].ToString();
                            m_dicPrimarName["ID"] = dr["ID"].ToString();
                            result = SysParam.m_daoCommon.SetDeleteDataItem("Attend_TeamSet", m_dicItemData, m_dicPrimarName);
                            if (result > 0)
                            {
                                //日志
                                SysParam.m_daoCommon.WriteLog("排班登记", "删除", dr["MyTeamName"].ToString()
                                    + "，开始时间：" + dr["BgnDate"].ToString() + "，结束时间：" + dr["BgnDate"].ToString());
                            }
                        }
                    }
                    if (result > 0)
                    {
                        Common.AdoConnect.Connect.TransactionCommit();
                        XtraMsgBox.Show("删除数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //SetSearchProc(this);
                        this.ScanMode = Common.DataModifyMode.dsp;
                        GetDspDataList();
                    }
                }
                else
                {
                    NewButtonEnabled = true;
                    SearchButtonEnabled = true;
                    EditButtonEnabled = true;
                    DeleteButtonEnabled = true;
                    SelectAllButtonEnabled = true;
                    SelectOffButtonEnabled = true;
                    ExcelButtonEnabled = true;
                    PrintButtonEnabled = true;
                }
                //显示检索可用按钮
                //SetButtonEnabled();
            }
            catch (Exception ex)
            {
                Common.AdoConnect.Connect.TransactionRollback();
                log.Error(ex);
                XtraMsgBox.Show("删除数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        


        #endregion

        #region 事件处理方法


        #endregion

        #region 共同方法

        /// <summary>
        /// 获取表格信息一览
        /// </summary>
        protected override void GetDspDataList()
        {
            try
            {
                NewButtonEnabled = true;
                dateBeginDate.Enabled = true;
                dateBeginDate.Properties.ReadOnly = false;
                dateEndDate.Enabled = true;
                dateEndDate.Properties.ReadOnly = false;
                lookUpEditmyTeamName.Enabled = true;
                lookUpEditmyTeamName.Properties.ReadOnly = false;

                string str_sql = "Select CAST('0' AS Bit) AS SlctValue, * " +
                                 " from V_Attend_TeamSet_i " + 
                                 " where 1=1  ";

               
                if ((dateBeginDate.EditValue != null && dateBeginDate.EditValue.ToString() != "") &&
                    (dateEndDate.EditValue != null && dateEndDate.EditValue.ToString() != ""))
                {
                    dtBegin = DateTime.Parse(dateBeginDate.EditValue.ToString());
                    dtEnd = DateTime.Parse(dateEndDate.EditValue.ToString());

                    str_sql += " and   ( BgnDate between '" + dtBegin.ToString("yyyy-MM-dd HH:mm") + "'  AND   '" + dtEnd.ToString("yyyy-MM-dd HH:mm") + "'";
                    str_sql += "  OR   EndDate between '" + dtBegin.ToString("yyyy-MM-dd HH:mm") + "'  AND   '" + dtEnd.ToString("yyyy-MM-dd HH:mm") + "' )";
                }
                //向别-班别
                if (lookUpEditmyTeamName.Text.Trim() != "全部")
                {
                    str_sql += " and MyTeamName in ('" + lookUpEditmyTeamName.Text.Trim() + "') ";
                }
                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (m_tblDataList.Rows.Count > 0)
                {
                    SearchButtonEnabled = true;
                    EditButtonEnabled = true;
                    DeleteButtonEnabled = true;
                    SelectAllButtonEnabled = true;
                    SelectOffButtonEnabled = true;
                    ExcelButtonEnabled = true;
                    PrintButtonEnabled = true;
                }
                else
                {
                    SearchButtonEnabled =true;
                    EditButtonEnabled = false;
                    DeleteButtonEnabled = false;
                    SelectAllButtonEnabled = false;
                    SelectOffButtonEnabled = false;
                    ExcelButtonEnabled = false;
                    PrintButtonEnabled = false;

                }
                this.m_GridViewUtil.GridControlList.DataSource = m_tblDataList;
                
              
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 获取勾选数据
        /// </summary>
        protected DataRow[] GetSelectList()
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            //获取删除行
            DataTable dt = ((DataView)this.gridView1.DataSource).Table.Copy();
            //选择所有选择的数据
            DataRow[] drs = dt.Select(EnumDefine.SlctValue + "='true'");
            return drs;
        }

        /// <summary>
        /// 获取下拉框数据
        /// </summary>
        public void GetSelectLookUpList()
        {
            try
            {
                string strSql = "";
                DataTable dt_temp;
                

                //向别-班别
                if (Common._myTeamName == "" || Common._myTeamName == null)
                {
                    strSql = string.Format(@"select distinct myTeamName from V_Produce_Para WHERE myTeamName<>''  order by myTeamName");
                }
                else
                {
                    strSql = string.Format(@"Select distinct myTeamName From V_Produce_Para  where  myTeamName ='{0}' AND myTeamName<>'' Order by myTeamName", Common._myTeamName);
                }
                dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);

                //默认选中
                DataRow dr = dt_temp.NewRow();

                dr[0] = "全部";
                dt_temp.Rows.InsertAt(dr, 0);
                lookUpEditmyTeamName.Properties.DataSource = dt_temp.DefaultView;
                lookUpEditmyTeamName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
                lookUpEditmyTeamName.Properties.DisplayMember = dt_temp.Columns[0].ColumnName; ;

                if (dt_temp.Rows.Count > 0)
                {
                    lookUpEditmyTeamName.EditValue = dt_temp.Rows[0][0].ToString();
                }
                lookUpEditmyTeamName.ItemIndex = 0;
                lookUpEditmyTeamName.Properties.BestFit();
                
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }
        #endregion

      
    }
}
