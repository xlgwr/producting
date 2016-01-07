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

** 修改人：libing
** 修改时间：2015-11-28
** 修改内容：代码规范化

** 描述：
**    主要用于加班登记信息的资料查询、删除、批量删除操作
*********************************************************************************/
namespace MachineSystem.TabPage
{
    public partial class frmTeamShow_OT :Framework.Abstract.frmSearchBasic2
    {
        #region 变量定义
        /// <summary>
        /// 数据表
        /// </summary>
        DataTable m_tblDataList = new DataTable();
        //DateTime dtBegin = DateTime.Now;
        //DateTime dtEnd = DateTime.Now; 
        // 日志
        private static readonly ILog log = LogManager.GetLogger(typeof(frmTeamShow_OT));
        /// 界面初始化标示
        bool isLoad = true;
        #endregion

        #region 画面初始化

        public frmTeamShow_OT()
        {
            InitializeComponent();

            this.m_GridViewUtil.GridControlList = this.gridControl1;
            this.m_GridViewUtil.ParentGridView = this.gridView1;
            this.TableName = "V_Attend_OT_Set";//操作表名称
            m_ParenSlctColName = "SlctValue";
            CopyAddCaption = "数据上传";
        }

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                //gridView 行号
                base.SetFormValue();
                DateTime FirstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime LastDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                //初始化日期

                dateBeginDate.EditValue = string.Format("{0:yyyy-MM-dd HH:mm}", FirstDay);
                //获取检索条件中下拉框数据
                GetSelectLookUpList();
                lookUpEditJobFor.EditValue = Common._ProjectId;
                lookUpEditmyTeamName.EditValue = Common._orgTeamName;
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

            GetDspDataList();
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
                frmTeamShow_OTAdd frm = new frmTeamShow_OTAdd();
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
               

                frmTeamShow_OTAdd frm = new frmTeamShow_OTAdd();
                frm.ScanMode = Common.DataModifyMode.upd;
                //选择所有选择的数据
                frm.drs = this.GetSelectList();
                string orgTeamNm = frm.drs[0]["orgTeamName"].ToString();
                for (int i = 0; i < frm.drs.Length; i++)
                {
                    if (orgTeamNm != frm.drs[i]["orgTeamName"].ToString()) {
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
                int result = 0;
                //选择所有选择的数据
                DataRow[] drs = this.GetSelectList();

                //没有选择任何数据情况
                if (drs.Length <= 0)
                {
                    XtraMsgBox.Show("未勾选任何数据！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //检查是否有关联数据
                if (CheckDeleteDate(drs))
                {
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
                            m_dicItemData["ID"] = dr["myID"].ToString();
                            m_dicPrimarName["ID"] = dr["myID"].ToString();
                            result = SysParam.m_daoCommon.SetDeleteDataItem("Attend_OT_Set", m_dicItemData, m_dicPrimarName);
                            if (result > 0)
                            {
                                //日志
                                SysParam.m_daoCommon.WriteLog("加班登记:", "删除", dr["myID"].ToString());
                            }
                        }
                    }
                    if (result > 0)
                    {
                        Common.AdoConnect.Connect.TransactionCommit();
                        XtraMsgBox.Show("删除数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetDspDataList();
                    }
                }

            }
            catch (Exception ex)
            {
                Common.AdoConnect.Connect.TransactionRollback();
                log.Error(ex);
                XtraMsgBox.Show("删除数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //根据用户id，查询用户其他信息
        private void txtUserID_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmUserInfoSearch _frm = new frmUserInfoSearch();
            if (_frm.ShowDialog() == DialogResult.OK)
            {
                this.txtUserID.EditValue = _frm.SelectRowData["UserID"].ToString();
                this.txtUserName.EditValue = _frm.SelectRowData["UserName"].ToString();
            }
        }


        #endregion

        #region 事件处理方法
        /// <summary>
        /// 选择向别别加载该班别下的班别
        /// </summary>
        private void lookJobFor_EditValueChanged(object sender, EventArgs e)
        {

            if (isLoad) return;

            string str_where = " where 1=1 ";
            if (lookUpEditJobFor.EditValue.ToString() != "-1")
            {
                str_where = " and JobForID ='" + lookUpEditJobFor.EditValue.ToString() + "'";
            }
            string str_sql = string.Format(@"Select DISTINCT orgTeamName From  V_Attend_OT_Set   " + str_where + " Order by orgTeamName");

            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //默认选中
            DataRow dr = dt_temp.NewRow();
            dr[0] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            lookUpEditmyTeamName.Properties.DataSource = dt_temp.DefaultView;
            lookUpEditmyTeamName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookUpEditmyTeamName.Properties.DisplayMember = dt_temp.Columns[0].ColumnName;
            if (dt_temp.Rows.Count > 0)
            {
                lookUpEditmyTeamName.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookUpEditmyTeamName.ItemIndex = 0;
            lookUpEditmyTeamName.Properties.BestFit();
        }
        /// <summary>
        /// 选择班别加载该班别下的关位
        /// </summary>
        private void lookmyTeamName_EditValueChanged(object sender, EventArgs e)
        {

            if (isLoad) return;

            string str_where = " where 1=1 ";
            if (lookUpEditmyTeamName.Text.ToString() != "全部")
            {
                str_where = " and orgTeamName ='" + lookUpEditmyTeamName.Text.ToString() + "'";
            }
            string str_sql = string.Format(@"Select DISTINCT GuanweiID,GuanweiName From  V_Attend_OT_Set  " + str_where + " Order by GuanweiName");

            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //默认选中
            DataRow dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[0] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            lookGuanweiName.Properties.DataSource = dt_temp.DefaultView;
            lookGuanweiName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookGuanweiName.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;
            if (dt_temp.Rows.Count > 0)
            {
                lookGuanweiName.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookGuanweiName.ItemIndex = 0;
            lookGuanweiName.Properties.BestFit();
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
             string str_sql = "Select  CAST('0' AS Bit) AS SlctValue,* from V_Attend_OT_Set where 1=1 ";

               if (dateBeginDate.EditValue != null && dateBeginDate.EditValue.ToString() != "")
               {
                   string dtBegin = DateTime.Parse(dateBeginDate.EditValue.ToString()).ToString("yyyy-MM-dd");
                  
                   //当天的时间，没有时分yyyy-MM-dd ,CONVERT(char(10), OTStrDate, 120)；
                   str_sql += " and  CONVERT(char(10), OtDate, 120) = '" + dtBegin + "'  ";
               }
               if (txtUserID.Text.Trim() != "")
               {
                   str_sql += " and myUserID like '%" + txtUserID.Text.Trim() + "%' ";
               }
               if (txtUserName.Text.Trim() != "")
               {
                   str_sql += " and UserName like '%" + txtUserName.Text.Trim() + "%' ";
               }
               if (lookUpEditJobFor.EditValue.ToString() != "-1")//向别
               {
                   str_sql += " and JobForID= " + lookUpEditJobFor.EditValue.ToString().Trim() + " ";
               }
               if (lookUpEditmyTeamName.Text.ToString() != "" && lookUpEditmyTeamName.Text.ToString() != "全部")//班别
               {
                   str_sql += " and orgTeamName in ('" + lookUpEditmyTeamName.Text.Trim() + "') ";
               }
               if (lookGuanweiName.EditValue.ToString() != "-1")//关位
               {
                   str_sql += " and  GuanweiID = '" + lookGuanweiName.EditValue.ToString() + "' ";
               }  
               
                //加班事由
               if (lookUpOTTypeName.EditValue.ToString() != "-1")
               {
                   str_sql += " and  OTType = '" + lookUpOTTypeName.EditValue.ToString() + "' ";
               }
               //申请理由
               if (lookUpOTApplyName.EditValue.ToString() != "-1")
               {
                   str_sql += " and  OTApplyID = '" + lookUpOTApplyName.EditValue.ToString() + "' ";
               }
               //排班
               if (lookUpTeamSetName.EditValue.ToString() != "-1")
               {
                   str_sql += " and  TeamSetiD = '" + lookUpTeamSetName.EditValue.ToString() + "' ";
               }

               if (Common._personid!=Common._Administrator)
               {// &&Common._myTeamName != "" && Common._myTeamName != null
                   str_sql += " and myTeamName='" + Common._myTeamName + "'";
               }
                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (m_tblDataList.Rows.Count > 0)
                {
                    DeleteButtonEnabled = true;
                    EditButtonEnabled = true;
                    SelectAllButtonEnabled = true;
                    SelectOffButtonEnabled = true;
                    PrintButtonEnabled = true;
                }
                else
                {
                    DeleteButtonEnabled = false;
                    EditButtonEnabled = false;
                    SelectAllButtonEnabled = false;
                    SelectOffButtonEnabled = false;
                    PrintButtonEnabled = false;
                    
                }
                this.m_GridViewUtil.GridControlList.DataSource = m_tblDataList;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 检查删除数据在【出勤情况】里是否有关联数据
        /// </summary>
        public Boolean CheckDeleteDate(DataRow[] drs)
        {
            try
            {
                if (drs.Length > 0)
                {
                    for (int i = 0; i < drs.Length; i++)
                    {
                        DataRow dr = drs[i];
                        string str_sql = "Select UserID from V_Attend_Result_Info where 1=1 ";
                        str_sql += " and JobForID= " + dr["JobForID"].ToString();

                        str_sql += " and (AttendDate between '" + dr["OTStrDateSel"].ToString();
                        str_sql += "' and  '" + dr["OTEndTimeSel"].ToString() + "')";
                        m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                        if (m_tblDataList.Rows.Count > 0)
                        {

                            XtraMsgBox.Show("请假信息已经被统计到出勤情况中，不能删除！",
                                            this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("删除检查失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
                return true;
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
                //向别
                if (Common._Administrator == Common._personid)
                {
                    strSql = string.Format(@"select distinct JobForID,JobForName from V_Produce_User WHERE JobForID<>0 and JobForName<>'' order by JobForName");
                }
                else
                {
                    strSql = string.Format(@"select distinct JobForID,JobForName from V_Produce_User_Line where  UserID ='{0}' AND  JobForID<>0 order by JobForName", Common._personid);
                }

                DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);

                //默认选中
                DataRow dr = dt_temp.NewRow();
                dr[0] = "-1";
                dr[1] = "全部";
                dt_temp.Rows.InsertAt(dr, 0);
                lookUpEditJobFor.Properties.DataSource = dt_temp.DefaultView;
                lookUpEditJobFor.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
                lookUpEditJobFor.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;
                if (dt_temp.Rows.Count > 0)
                {
                    lookUpEditJobFor.EditValue = dt_temp.Rows[0][0].ToString();
                }
                lookUpEditJobFor.ItemIndex = 0;
                lookUpEditJobFor.Properties.BestFit();


                //工程别-班别
                if (Common._myTeamName == "" || Common._myTeamName == null)
                {
                    strSql = string.Format(@"select distinct orgTeamName from V_Produce_Para WHERE orgTeamName<>''  order by orgTeamName");
                }
                else
                {
                    strSql = string.Format(@"Select distinct orgTeamName From V_Produce_Para  where  myTeamName ='{0}' AND orgTeamName<>'' Order by orgTeamName", Common._myTeamName);
                }
                dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);

                //默认选中
                dr = dt_temp.NewRow();

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

                //关位
                if (Common._myTeamName == "" || Common._myTeamName == null)
                {
                    strSql = string.Format(@"select distinct GuanweiID,GuanweiName from V_Produce_Para WHERE GuanweiID<>0  order by GuanweiName");
                }
                else
                {
                    strSql = string.Format(@"select distinct GuanweiID,GuanweiName  from V_Produce_Para   where  myTeamName ='{0}' AND   GuanweiID<>0 Order by GuanweiName", Common._myTeamName);
                }
                dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);

                //默认选中
                dr = dt_temp.NewRow();
                dr[0] = "-1";
                dr[1] = "全部";
                dt_temp.Rows.InsertAt(dr, 0);
                lookGuanweiName.Properties.DataSource = dt_temp.DefaultView;
                lookGuanweiName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
                lookGuanweiName.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;

                if (dt_temp.Rows.Count > 0)
                {
                    lookGuanweiName.EditValue = dt_temp.Rows[0][0].ToString();
                }
                lookGuanweiName.ItemIndex = 0;
                lookGuanweiName.Properties.BestFit();

                //加班事由
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_OTType, this.lookUpOTTypeName, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect);
                //申请事由
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_OTApply, this.lookUpOTApplyName, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect);
                //排班
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_Produce_TeamKind, this.lookUpTeamSetName, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect);

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
