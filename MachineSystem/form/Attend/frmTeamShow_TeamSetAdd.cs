using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework.Abstract;
using MachineSystem.SysDefine;
using log4net;
using Framework.Libs;
using MachineSystem.SettingTable;
/********************************************************************************
** 作者： xxx
** 创始时间：2015-6-8

** 修改人：xuensheng
** 修改时间：2015-7-9
** 修改内容：代码规范化

** 描述：
**    主要用于排班登记信息的资料资料新增、修改操作
*********************************************************************************/
namespace MachineSystem.TabPage
{
    public partial class frmTeamShow_TeamSetAdd : Framework.Abstract.frmBaseToolEntryXC
    {
        #region 变量定义
        public DataRow[] drs;
        /// <summary>
        /// 排班数据表
        /// </summary>
        public DataTable m_tblDataList = new DataTable();

        /// <summary>
        /// 选中的向别-班别
        /// </summary>
        public string m_teamName = string.Empty;

        /// <summary>
        /// 班种：白班晚班
        /// </summary>
        DataTable m_tblTeamType = new DataTable();


        /// 界面初始化标示
        bool isLoad = true;
        // 日志
        private static readonly ILog log = LogManager.GetLogger(typeof(frmTeamShow_TeamSetAdd));
        #endregion

        #region 画面初始化

        public frmTeamShow_TeamSetAdd()
        {
            InitializeComponent();

            this.m_GridViewUtil.GridControlList = this.gridControl1;
            this.m_GridViewUtil.ParentGridView = this.gridView1;
            this.TableName = "Attend_TeamSet";//操作表名称

        }

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                  //获取检索条件中下拉框数据
                GetSelectLookUpList();
                gridView1.IndicatorWidth = 30;
                isLoad = false;
                if (this.ScanMode == Common.DataModifyMode.add)
                {
                    this.Text = "新增排班登记";
                    lookUpEditScheduling.Visible = true;
                    labelControl10.Visible = true;
                }
                else
                {
                    this.Text = "修改排班记录";
                    //取得更新数据
                    GetDataRowValue(drs);
                    lookUpEditScheduling.Visible = false;
                    labelControl10.Visible = false;
                }

            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("画面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }
        void repositoryItemComboBox2_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            if (e.Value != null)
            {
                e.Value = e.Value.ToString();
                e.Handled = true;
            }
        }

        /// <summary>
        /// 行编号显示事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
                else if (e.RowHandle < 0 && e.RowHandle > -1000)
                {
                    e.Info.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
                    e.Info.DisplayText = e.RowHandle.ToString();
                }
            }
        }
      
        #endregion

        #region 画面按钮功能处理方法
        
        
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="frmbase"></param>
        protected override void SetSaveDataProc(frmBaseToolEntryXC frmbase)
        {
            try
            {
                base.SetSaveDataProc(frmbase);

            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("新增失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="isClear"></param>
        protected override void SetCancelInit(bool isClear)
        {
            this.Close();
        }
   
                
        #endregion
        

        #region 事件处理方法
        /// <summary>
        /// 班种变更，重新取得排班数据
        /// </summary>
        private void lookUpEditScheduling_EditValueChanged(object sender, EventArgs e)
        {
             //存在信息检查
            string str_sql = string.Format("select * from  V_Attend_TeamSet_i where MyTeamName in ('" + lookUpEditmyTeamName.Text.Trim() + "')");
            
            if (SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql).Rows.Count > 0)
            {
                isLoad = true;
                XtraMsgBox.Show("已经存在这个班的排班信息，不能重复增加排班信息！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            str_sql = " select  "
                            + " A.SchedulingID AS ID, " 
                            + " A.StrDate, "
                            + " A.EndDate,"
                            +  " A.TypeID ,"
                            + " B.pName AS TypeName  "
                            + " from P_Produce_Scheduling_detail A "
                            + " LEFT JOIN P_Produce_TeamKind  B "
                            + " ON A.TypeID=B.ID "
                            + " where 1=1  "
                            + " and  SchedulingID='" + lookUpEditScheduling.EditValue.ToString() + "'";
            m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
            
            this.m_GridViewUtil.GridControlList.DataSource = m_tblDataList;
        }

        
        /// <summary>
        /// 新增
        /// </summary>
        protected override void SetInsertProc(ref int RtnValue)
        {
            base.SetInsertProc(ref RtnValue);
            try
            {

                gridView1.CloseEditor();
                gridView1.UpdateCurrentRow();
                if (lookUpEditScheduling.EditValue.ToString() == "-1")
                {
                    isLoad = true;
                    XtraMsgBox.Show("请选择班种！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return;
                }
                if (lookUpEditmyTeamName.EditValue.ToString() == "-1")
                {
                    isLoad = true;
                    XtraMsgBox.Show("请选择班别-向别！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return;
                }

                DataTable dt_Data = ((DataView)this.gridView1.DataSource).Table.Copy();
                string str_sql = string.Empty;
                int result = 0;
                DataTable dt_temp = ((DataView)gridView1.DataSource).ToTable();
 
               
                //取得向别信息
                str_sql = string.Format("select top 1 * from  V_Produce_Para where myTeamName in ('" + lookUpEditmyTeamName.Text.Trim() + "')");
                DataTable dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (dt.Rows.Count > 0)
                {
                    Common.AdoConnect.Connect.CreateSqlTransaction();
                    DataView view = new DataView(m_tblTeamType.Copy());
                    //保存信息
                    for (int i = 0; i < dt_temp.Rows.Count; i++)
                    {
                        m_dicItemData.Clear();
                        m_dicItemData["JobForID"] = dt.Rows[0]["JobForID"].ToString();//向别
                        m_dicItemData["ProjectID"] = dt.Rows[0]["ProjectID"].ToString();//工程别
                        m_dicItemData["LineID"] = dt.Rows[0]["LineID"].ToString();//line别
                        m_dicItemData["TeamID"] = dt.Rows[0]["TeamID"].ToString();//班别
                        m_dicItemData["BgnDate"] = dt_temp.Rows[i]["StrDate"].ToString();//开始时间
                        m_dicItemData["EndDate"] = dt_temp.Rows[i]["EndDate"].ToString();//结束时间
 
                        if (i>0 && DateTime.Compare(DateTime.Parse(dt_temp.Rows[i]["StrDate"].ToString()), DateTime.Parse(dt_temp.Rows[i - 1]["EndDate"].ToString())) < 0)
                        {

                            XtraMsgBox.Show("月排班开始时间不能小于上月的结束时间!", dt_temp.Rows[i]["StrDate"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (DateTime.Compare(DateTime.Parse(dt_temp.Rows[i]["EndDate"].ToString()), DateTime.Parse(dt_temp.Rows[i]["StrDate"].ToString())) < 0)
                        {

                            XtraMsgBox.Show("月排班开始时间不能小于结束时间!", dt_temp.Rows[i]["EndDate"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        view.RowFilter = "TypeName='" + dt_temp.Rows[i]["TypeName"].ToString() + "'";
                        DataTable dt2 = view.ToTable();
                        m_dicItemData["pTypeID"] = dt2.Rows[0]["TypeID"].ToString();//班种
                        m_dicItemData["OperID"] = Common._personid;
                        m_dicItemData["OperTime"] = DateTime.Now.ToString();
                        result = SysParam.m_daoCommon.SetInsertDataItem(this.TableName, m_dicItemData);
 
                    }
                }
                if (result > 0)
                {
                    Common.AdoConnect.Connect.TransactionCommit();
                    XtraMsgBox.Show("新增数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //日志
                    SysParam.m_daoCommon.WriteLog("排班设置", "新增", lookUpEditmyTeamName.Text.Trim());
                    DialogResult = DialogResult.OK;
                }
                
            }
            catch (Exception ex)
            {
                Common.AdoConnect.Connect.TransactionRollback();
                log.Error(ex);

                throw ex;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="RtnValue"></param>
        public override void SetModifyProc(ref int RtnValue)
        {
            base.SetModifyProc(ref RtnValue);
            try
            {
                int result = 0;
                gridView1.CloseEditor();
                gridView1.UpdateCurrentRow();
                DataTable dt = ((DataView)this.gridView1.DataSource).Table.Copy();
                DataView view = new DataView(m_tblTeamType.Copy());
                Common.AdoConnect.Connect.CreateSqlTransaction();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    m_dicPrimarName.Clear();
                    m_dicItemData.Clear();
                    m_dicPrimarName["ID"] = dt.Rows[i]["ID"].ToString();

                    m_dicItemData["BgnDate"] = dt.Rows[i]["StrDate"].ToString();//开始时间
                    m_dicItemData["EndDate"] = dt.Rows[i]["EndDate"].ToString();//结束时间

                    if (i > 0 && DateTime.Compare(DateTime.Parse(dt.Rows[i]["StrDate"].ToString()), DateTime.Parse(dt.Rows[i - 1]["EndDate"].ToString())) < 0)
                    {

                        XtraMsgBox.Show("月排班开始时间不能小于上月的结束时间!", dt.Rows[i]["StrDate"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (DateTime.Compare(DateTime.Parse(dt.Rows[i]["EndDate"].ToString()), DateTime.Parse(dt.Rows[i]["StrDate"].ToString())) < 0)
                    {

                        XtraMsgBox.Show("月排班开始时间不能小于结束时间!", dt.Rows[i]["EndDate"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    view.RowFilter = "TypeName='" + dt.Rows[i]["TypeName"].ToString() + "'";
                    DataTable dt2 = view.ToTable();
                    m_dicItemData["pTypeID"] = dt2.Rows[0]["TypeID"].ToString();//班种
                    m_dicItemData["OperID"] = Common._personid;
                    m_dicItemData["OperTime"] = DateTime.Now.ToString();

                    result = SysParam.m_daoCommon.SetModifyDataIdentityColumn(this.TableName, m_dicItemData, m_dicPrimarName);
               }
               
                if (result > 0)
                {
                    Common.AdoConnect.Connect.TransactionCommit();
                    XtraMsgBox.Show("修改数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //日志
                    SysParam.m_daoCommon.WriteLog("排班登记", "修改", lookUpEditmyTeamName.Text.Trim());
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                Common.AdoConnect.Connect.TransactionRollback();
                log.Error(ex);
                throw ex;
            }
        }
        #endregion

        #region 共同方法
        //修改数据取得
        public void GetDataRowValue(DataRow[] drs)
        {
            //向别-班别
            lookUpEditmyTeamName.EditValue = drs[0]["myTeamName"].ToString();
          
           DataRow dr;
           DataView view = new DataView(m_tblTeamType.Copy());
           
           for (int i = 0; i < drs.Length; i++)
           {
               dr = m_tblDataList.NewRow();
               dr["ID"] = drs[i]["ID"].ToString();
               dr["StrDate"] = drs[i]["BgnDate"].ToString();//开始时间
               dr["EndDate"] = drs[i]["EndDate"].ToString();//结束时间

               view.RowFilter = "TypeName='" + drs[i]["TeamSetName"].ToString() + "'";
               DataTable dt_temp = view.ToTable();
               dr["TypeID"] = dt_temp.Rows[0]["TypeID"].ToString();//班种
               dr["TypeName"] = drs[i]["TeamSetName"].ToString();//班种

               m_tblDataList.Rows.Add(dr);
           }
           
            this.m_GridViewUtil.GridControlList.DataSource = m_tblDataList;
           
       }
        /// <summary>
        /// 获取下拉框数据
        /// </summary>
        public void GetSelectLookUpList()
        {
            try
            {
               

                string strSql = "";
                //************向别-班别************
                if (Common._myTeamName == "" || Common._myTeamName == null)
                {
                    strSql = string.Format(@"select distinct myteamName from V_Produce_Para WHERE myteamName<>''  order by myteamName");
                }
                else
                {
                    strSql = string.Format(@"Select distinct myteamName From V_Produce_Para  where  myTeamName in ('{0}') AND myteamName<>'' Order by myteamName", Common._myTeamName);
                }
                DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);

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

                //************班种************
                //班别
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_Produce_Scheduling, lookUpEditScheduling, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefalutItemAllText);
                repositoryItemComboBox2.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(repositoryItemComboBox2_ParseEditValue);
                if (gridView1 != null)
                {
                    gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(gridView1_CustomDrawRowIndicator);
                }
                

                //获取白班晚班
                string str_sql = string.Format(@"select ID as TypeID,pName as TypeName from P_Produce_TeamKind ");
                m_tblTeamType = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                for (int i = 0; i < m_tblTeamType.Rows.Count; i++)
                {
                    CboItemEntity item = new CboItemEntity();
                    item.Value = m_tblTeamType.Rows[i]["TypeID"].ToString();
                    item.Text = m_tblTeamType.Rows[i]["TypeName"].ToString();
                    repositoryItemComboBox2.Items.Add(item);
                }

                //开始时间
                repositoryItemDateEdit1.EditMask = "yyyy-MM-dd HH:mm";
                repositoryItemDateEdit1.Mask.UseMaskAsDisplayFormat = true;
                //结束时间
                repositoryItemDateEdit2.EditMask = "yyyy-MM-dd HH:mm";
                repositoryItemDateEdit2.Mask.UseMaskAsDisplayFormat = true;

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
