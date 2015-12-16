using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework.Abstract;
using MachineSystem.form.ParaLicense;
using Framework.Libs;
using MachineSystem.form.Remove;
using log4net;
using MachineSystem.SysDefine;

/********************************************************************************
** 作者： libing
** 创始时间：2015-6-8

** 修改人：libing
** 修改时间：2015-7-27
** 修改内容：代码规范化

** 描述：
**    调动登记的查询、删除、批量删除操作
*********************************************************************************/
namespace MachineSystem.TabPage
{
    public partial class frmTeamShow_Replace :Framework.Abstract.frmSearchBasic2
    {

        #region 变量定义

        /// <summary>
        /// 表格数据
        /// </summary>
        DataTable m_tblDataList = new DataTable();
        // 日志
        private static readonly ILog log = LogManager.GetLogger(typeof(frmTeamShow_Replace));
        // 界面初始化标示
        bool isLoad = true;
        #endregion

        #region 画面初始化

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmTeamShow_Replace()
        {
            InitializeComponent();
            this.m_GridViewUtil.GridControlList = this.gridControl1;

            this.m_GridViewUtil.ParentGridView = this.gridView1;
            m_ParenSlctColName = "SlctValue";
            this.TableName = "V_Attend_Move_i";
        }

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                base.SetFormValue();

                CopyAddVisibility = false;
                SelectAllButtonEnabled = false;
                SelectOffButtonEnabled = false;
                DateTime FirstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime LastDay = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.AddMonths(1).Day);
                dtpStartDate.EditValue = string.Format("{0:yyyy-MM-dd}", FirstDay);
                dtpEndDate.EditValue = string.Format("{0:yyyy-MM-dd}", LastDay);
                GetLookUpList();//获取检索条件中下拉框数据
                isLoad = false;
                if (Common._myTeamName != "")
                {
                    lookmyteamName.Text = Common._myTeamName;
                }
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
        /// 搜索
        /// </summary>
        /// <param name="frmBaseToolXC"></param>
        protected override void SetSearchProc(frmSearchBasic2 frmBaseToolXC)
        {
            base.SetSearchProc(frmBaseToolXC);

            //GetDspDataList();
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
                frmTeamShowEdit_Replace frm = new frmTeamShowEdit_Replace();
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
                XtraMsgBox.Show("新增失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
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
                else if (this.GetSelectList().Length > 1)
                {
                    XtraMsgBox.Show("修改时，只能勾选一条数据！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                frmTeamShowEdit_Replace frm = new frmTeamShowEdit_Replace();
                frm.ScanMode = Common.DataModifyMode.upd;
                //选择所有选择的数据
                frm.dr = this.GetSelectList()[0];

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    this.ScanMode = Common.DataModifyMode.dsp;
                    //日志
                    SysParam.m_daoCommon.WriteLog("人员调动:", "修改", frm.dr["UserID"].ToString());
                    GetDspDataList();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("修改失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        protected override void SetDeleteInit()
        {
            try
            {
                int result = 0;
                //选择所有选择的数据
                DataRow[] drs = this.GetSelectList();

                //没有选择任何数据情况
                if (drs.Length <= 0)
                {
                    XtraMsgBox.Show("未勾选任何数据！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (XtraMsgBox.Show("是否删除数据？", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    Common.AdoConnect.Connect.CreateSqlTransaction();
                    if (drs.Length > 0)
                    {
                        for (int i = 0; i < drs.Length; i++)
                        {
                            DataRow dr = drs[i];
                            m_dicItemData = new System.Collections.Specialized.StringDictionary();
                            m_dicItemData["ID"] = dr["ID"].ToString();
                            m_dicPrimarName["ID"] = dr["ID"].ToString();

                            result = SysParam.m_daoCommon.SetDeleteDataItem("Attend_Move", m_dicItemData, m_dicPrimarName);
                            if (result > 0)
                            {
                                //日志
                                SysParam.m_daoCommon.WriteLog("调动登记:", "删除", dr["UserID"].ToString());
                            }
                        }
                    }
                }
                if (result > 0)
                {
                    Common.AdoConnect.Connect.TransactionCommit();
                    XtraMsgBox.Show("删除数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                    SetSearchProc(this);
                }
            }
            catch (Exception ex)
            {
                Common.AdoConnect.Connect.TransactionRollback();
                log.Error(ex);
                XtraMsgBox.Show("删除数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtpUserID_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmUserInfoSearch _frm = new frmUserInfoSearch();
            if (_frm.ShowDialog() == DialogResult.OK)
            {
                this.txtpUserID.EditValue = _frm.SelectRowData["UserID"].ToString();
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
               if (dtpStartDate.DateTime > dtpEndDate.DateTime)
                {
                    DataValid.ShowErrorInfo(this.ErrorInfo, this.dtpStartDate, "开始时间必须小于结束时间!");
                    return;
                }
                txtpName.Enabled = true;
                string str_sql = "select   CAST('0' AS Bit) AS SlctValue,* from V_Attend_Move_i where 1=1";
                string str_where = "";
                if (!string.IsNullOrEmpty(txtpUserID.Text.Trim())) 
                {
                    str_where += " and UserID='" + txtpUserID.Text.Trim() + "'";
                }
                if (!string.IsNullOrEmpty(txtpName.Text.Trim()))
                {
                    str_where += " and UserName='" + txtpName.Text.Trim() + "'";
                }
                if (lookGuanwei.EditValue.ToString() != "-1") 
                {
                    str_where += " and GuanweiID='" + lookGuanwei.EditValue.ToString() +"'";
                }
                if (lookmyteamName.EditValue.ToString() != "-1" && lookmyteamName.Text!="全部")
                {
                    str_where += " and myteamName='" + lookmyteamName.EditValue.ToString() + "'";
                }
                if (lookType.SelectedIndex > 0)
                {
                    int index=0;
                    if (lookType.SelectedIndex == 1) 
                    {
                        index = 6;
                    }
                    else if (lookType.SelectedIndex == 2)
                    {
                        index = 5;
                    }
                    else if (lookType.SelectedIndex == 3)
                    {
                        index = 4;
                    }
                    str_where += " and pFlag='" + index+"'";
                }
                str_where += " and StrDate>=  '" + dtpStartDate.DateTime.ToString("yyyy-MM-dd") + "' and EndDate<='" + dtpEndDate.DateTime.ToString("yyyy-MM-dd") + "' ";

                if (str_where != "") 
                {
                    str_sql += str_where;
                }
                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                SetModifyColumns();
                if (m_tblDataList.Rows.Count > 0)
                {
                    DeleteButtonEnabled = true;
                    SelectAllButtonEnabled = true;
                    SelectOffButtonEnabled = true;
                    EditButtonEnabled = true;
                }
                else
                {
                    DeleteButtonEnabled = false;
                    SelectAllButtonEnabled = false;
                    SelectOffButtonEnabled = false;
                    EditButtonEnabled = false;
                }
                this.m_GridViewUtil.GridControlList.DataSource = m_tblDataList;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        private void SetModifyColumns()
        {
            DataRow _dr;
            try
            {
                for (int i = 0; i < m_tblDataList.Rows.Count; i++)
                {
                    _dr = m_tblDataList.Rows[i];
                    if (_dr["MoveStatus"].ToString().Equals("人员调入"))
                    {
                        _dr.BeginEdit();
                        _dr["EndDate"] = "";
                        _dr.EndEdit();
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }


        /// <summary>
        /// 获取下拉框数据
        /// </summary>
        public void GetLookUpList() 
        {
            //向别-班别
            string str = string.Format(@"select distinct myteamName from [V_Produce_Para] where  myTeamName in ('{0}')", Common._myTeamName);
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str);

            //默认选中
            DataRow dr = dt_temp.NewRow();
            dr[0] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);
            lookmyteamName.Properties.DataSource = dt_temp.DefaultView;
            lookmyteamName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookmyteamName.Properties.DisplayMember = dt_temp.Columns[0].ColumnName;
            if (dt_temp.Rows.Count > 0)
            {
                lookmyteamName.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookmyteamName.ItemIndex = 0;
            lookmyteamName.Properties.BestFit();
            
            if (Common._myTeamName != "") 
            {
                lookmyteamName.EditValue = Common._myTeamName;
            }

            //关位
            str = string.Format(@"select distinct GuanweiID id,GuanweiName from V_Produce_Para where  myTeamName in ('{0}')", Common._myTeamName);
            dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str);

            //默认选中
            dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);
            lookGuanwei.Properties.DataSource = dt_temp.DefaultView;
            lookGuanwei.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookGuanwei.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;
            if (dt_temp.Rows.Count > 0)
            {
                lookGuanwei.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookGuanwei.ItemIndex = 0;
            lookGuanwei.Properties.BestFit();
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
     #endregion

         


    }
}
