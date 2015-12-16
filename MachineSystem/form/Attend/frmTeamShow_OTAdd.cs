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
using MachineSystem.SysDefine;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using log4net;
/********************************************************************************
** 作者： xxx
** 创始时间：2015-6-8

** 修改人：xuensheng
** 修改时间：2015-7-9
** 修改内容：代码规范化

** 描述：
**    主要用于加班登记信息的资料资料新增、修改操作
*********************************************************************************/
namespace MachineSystem.TabPage
{
    public partial class frmTeamShow_OTAdd : Framework.Abstract.frmBaseToolEntryXC
    {
        #region 变量定义
        /// <summary>
        /// 数据表
        /// </summary>
        public DataRow[] drs;
        DataTable m_tblDataList = new DataTable();
        DataTable m_tblOtType = new DataTable();//加班事由
        DataTable m_tblOtApply = new DataTable();//s申请事由
        private string m_ParenSlctColName = "";
        DateTime dtBegin;
        DateTime dtEnd;
        string otNum;//加班时数
        string otKind;//加班类型
        string otTypeNm; //加班事由
        string otApplyNm; //申请事由
        string otTypeId; //加班事由
        string otApplyId; //申请事由
        RepositoryItemLookUpEdit rc1 = new RepositoryItemLookUpEdit();
        /// 界面初始化标示
        bool isLoad = true;
        // 日志
        private static readonly ILog log = LogManager.GetLogger(typeof(frmTeamShow_OTAdd));
        #endregion

        #region 画面初始化

        public frmTeamShow_OTAdd()
        {
            InitializeComponent();

            this.m_GridViewUtil.GridControlList = this.gridControl1;
            this.m_GridViewUtil.ParentGridView = this.gridView1;
            m_ParenSlctColName = "SlctValue";
            this.TableName = "Attend_OT_Set";//操作表名称

        }

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                PrintButtonCaption = "加载";
                //开始日期
                dateBeginDate.EditValue = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                //开始时间
                dtEndTime.EditValue = string.Format("{0:HH:mm}", DateTime.Now);
                dtEndTime2.EditValue = string.Format("{0:HH:mm}", DateTime.Now);
                //获取检索条件中下拉框数据
                GetSelectLookUpList();
                isLoad = false;


                if (this.ScanMode == Common.DataModifyMode.add)
                {
                    this.Text = "新增加班登记";
                }
                else
                {
                    this.Text = "修改加班记录";
                    //取得更新数据
                    GetDataRowValue(drs);
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
        /// 加载按钮
        /// </summary>
        protected override void GetPrintReportProc()
        {

            try
            {

                DataView view = new DataView(m_tblOtApply.Copy());
                view.RowFilter = "ID='" + lookUpOTApplyName.EditValue.ToString() + "'";
                otNum = txtOTNum.Text.ToString();//加班时数
                otKind = view.ToTable().Rows[0]["OTKind"].ToString();////加班类型
                otTypeNm = lookUpOTTypeName.Text.ToString(); //加班事由
                otApplyNm = lookUpOTApplyName.Text.ToString(); //申请事由
                if (m_tblDataList.Rows.Count > 0)
                {
                    //保存当前用户的加班信息
                    for (int i = 0; i < m_tblDataList.Rows.Count; i++)
                    {
                        m_tblDataList.Rows[i]["OtDate"] = dateBeginDate.EditValue.ToString();//开始时间
                        m_tblDataList.Rows[i]["StartTime"] = dtEndTime.EditValue.ToString();//结束时间
                        m_tblDataList.Rows[i]["EndTime"] = dtEndTime2.EditValue.ToString();//结束时间

                        m_tblDataList.Rows[i]["OTNum"] = otNum;//加班小时数
                        m_tblDataList.Rows[i]["OTKind"] = otKind;//加班类型
                        m_tblDataList.Rows[i]["OTType"] = otTypeNm;//加班事由
                        m_tblDataList.Rows[i]["OTApply"] = otApplyNm; //申请事由

                    }
                }
                else
                {

                    string str_sql = "select CAST('1' AS Bit) AS SlctValue, '"
                         + dateBeginDate.EditValue.ToString() + "' as  OtDate, '" //开始时间
                            + dtEndTime.EditValue.ToString() + "' as  StartTime,  '" //开始时间
                            + dtEndTime2.EditValue.ToString() + "' as  EndTime,  '" //开始时间
                            + otKind + "' as  OTKind,  '" //加班类型
                            + otNum + "' as  OTNum,  '" //加班时数
                            + otTypeNm + "' as  OTType, '" //加班事由
                            + otApplyNm + "' as  OTApply,  " //申请事由
                            + "*   from v_Produce_User where 1=1 and  User_Status='在职'  "
                           + "  AND JobForID='" + lookUpEditJobFor.EditValue.ToString() + "' "//向别
                            + "  AND orgTeamName='" + lookUpEditmyTeamName.Text.ToString() + "' ";//班别

                    m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                    if (m_tblDataList.Rows.Count <= 0)
                    {
                        Exception ex = new Exception();
                        XtraMsgBox.Show("没有符合条件的人员，请重新选择向别、班别！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
                        return;
                    }
                }
                this.m_GridViewUtil.GridControlList.DataSource = m_tblDataList;
                SaveButtonEnabled = true;//保存
                CancelButtonEnabled = true;//取消


            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

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
        /// 选择向别别加载该班别下的班别
        /// </summary>
        private void lookJobFor_EditValueChanged(object sender, EventArgs e)
        {
            if (isLoad) return;

            string str_where = "";
            if (lookUpEditJobFor.EditValue.ToString() != "-1")
            {
                str_where = " AND JobForID ='" + lookUpEditJobFor.EditValue.ToString() + "'";
            }
            string str_sql = string.Format(@"Select DISTINCT orgTeamName From  V_Produce_Para where 1=1  " + str_where + " Order by orgTeamName");

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
        /// 结束日期输入以后，计算加班小时数
        /// </summary>
        ///  
        private void dateEndDate_EditValueChanged(object sender, EventArgs e)
        {
            //dtBegin = DateTime.Parse(dateBeginDate.EditValue.ToString());
            //dtEnd = DateTime.Parse(dateEndDate.EditValue.ToString());

            //if (DateTime.Compare(dtEnd ,dtBegin) < 0)
            //{
            //    DataValid.ShowErrorInfo(this.ErrorInfo, this.dateEndDate, "请选择开始时间小于结束时间!");
            //    return;
            //}
            //TimeSpan ts = dtBegin.Subtract(dtEnd).Duration();
            ////计算时间差//加班时数
            //txtOTNum.Text = ts.TotalHours.ToString();// ).ToString ();//取得整数
            ////ts.Days.ToString() + "天" +
            ////       ts.Hours.ToString() + "小时"
            ////       + ts.Minutes.ToString() + "分钟"
            ////       + ts.Seconds.ToString() + "秒"; 
        }
        /// <summary>
        /// 新增
        /// </summary>
        protected override void SetInsertProc(ref int RtnValue)
        {
            base.SetInsertProc(ref RtnValue);
            try
            {

                int result = 0;
                DataTable dt_temp = ((DataView)gridView1.DataSource).ToTable();

                DataView view = new DataView(m_tblOtApply.Copy());
                view.RowFilter = "OTApply='" + dt_temp.Rows[0]["OTApply"].ToString() + "'";
                otKind = view.ToTable().Rows[0]["OTKind"].ToString();////加班类型
                otApplyId = view.ToTable().Rows[0]["ID"].ToString();////加班类型

                view = new DataView(m_tblOtType.Copy());
                view.RowFilter = "pName='" + dt_temp.Rows[0]["OTType"].ToString() + "'";
                otTypeId = view.ToTable().Rows[0]["ID"].ToString();////加班类型
                //保存当前用户的加班信息
                for (int i = 0; i < dt_temp.Rows.Count; i++)
                {

                    m_dicItemData.Clear();
                    if (dt_temp.Rows[i]["SlctValue"].ToString() == "True")
                    {
                        m_dicItemData["UserID"] = dt_temp.Rows[i]["UserID"].ToString();//人会员编号
                        m_dicItemData["OtDate"] = dt_temp.Rows[i]["OtDate"].ToString();//开始日期
                        m_dicItemData["StartTime"] = dt_temp.Rows[i]["StartTime"].ToString();//开始时间
                        m_dicItemData["EndTime"] = dt_temp.Rows[i]["EndTime"].ToString();//结束时间
                        m_dicItemData["OTNum"] = dt_temp.Rows[i]["OTNum"].ToString();//加班小时数
                        m_dicItemData["OTType"] = otTypeId;//加班事由
                        m_dicItemData["OTApplyID"] = otApplyId; //申请事由
                        m_dicItemData["OperID"] = Common._personid;
                        result = SysParam.m_daoCommon.SetInsertDataItem("Attend_OT_Set", m_dicItemData);
                    }

                }
                if (result > 0)
                {
                    XtraMsgBox.Show("新增数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //日志
                    SysParam.m_daoCommon.WriteLog("加班登记", "新增", "开始日期：" + DateTime.Parse(dateBeginDate.EditValue.ToString())
                        + " ；开始时间：" + dtEndTime.EditValue.ToString()
                        + " ；结束时间：" + dtEndTime2.EditValue.ToString());
                    DialogResult = DialogResult.OK;
                }

            }
            catch (Exception ex)
            {
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

                DataView view = new DataView(m_tblOtApply.Copy());
                view.RowFilter = "OTApply='" + dt.Rows[0]["OTApply"].ToString() + "'";
                otApplyId = view.ToTable().Rows[0]["ID"].ToString();////加班申请事由

                view = new DataView(m_tblOtType.Copy());
                view.RowFilter = "pName='" + dt.Rows[0]["OTType"].ToString() + "'";
                otTypeId = view.ToTable().Rows[0]["ID"].ToString();////加班事由


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    m_dicPrimarName.Clear();
                    m_dicItemData.Clear();

                    m_dicPrimarName["ID"] = dt.Rows[i]["ID"].ToString();

                    //m_dicItemData["ID"] = dt.Rows[i]["ID"].ToString();
                    m_dicItemData["OtDate"] = dt.Rows[i]["OtDate"].ToString();//开始日期
                    m_dicItemData["StartTime"] = dt.Rows[i]["StartTime"].ToString();//开始时间
                    m_dicItemData["EndTime"] = dt.Rows[i]["EndTime"].ToString();//结束时间
                    m_dicItemData["OTNum"] = dt.Rows[i]["OTNum"].ToString();//加班小时数
                    m_dicItemData["OTType"] = otTypeId;//加班事由
                    m_dicItemData["OTApplyID"] = otApplyId; //申请事由
                    m_dicItemData["OperID"] = Common._personid;
                    m_dicItemData["OperDate"] = DateTime.Now.ToString();

                    //                    string str_sql = string.Format(@" update Attend_OT_Set set OTStrDate='{0}',OTEndTime='{1}',OTNum='{2}',OTType='{3}',OTApplyID='{4}',OperID='{5}'
                    //                                                       ,OperDate='{6}' where ID='{7}'",
                    //                        dt.Rows[i]["OTStrDate"].ToString(), dt.Rows[i]["OTEndTime"].ToString(), dt.Rows[i]["OTNum"].ToString(), otTypeId, otApplyId, 
                    //Common._personid, DateTime.Now.ToString(), dt.Rows[i]["ID"].ToString());
                    //result = SysParam.m_daoCommon.SetModifyDataItemBySql(str_sql, m_dicItemData, m_dicPrimarName, m_dicUserColum);
                    result = SysParam.m_daoCommon.SetModifyDataIdentityColumn(this.TableName, m_dicItemData, m_dicPrimarName);

                }

                if (result > 0)
                {
                    XtraMsgBox.Show("修改数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //日志
                    SysParam.m_daoCommon.WriteLog(":加班登记", "修改", "开始日期：" + DateTime.Parse(dateBeginDate.EditValue.ToString())
                          + " ；开始时间：" + dtEndTime.EditValue.ToString()
                        + " ；结束时间：" + dtEndTime2.EditValue.ToString());
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        #endregion

        #region 共同方法
        /// 画面数据有效检查处理
        /// </summary>
        protected override void GetInputCheck(ref bool isSucces)
        {
            try
            {
                base.GetInputCheck(ref isSucces);
                isSucces = false;
                if (isSucces)
                {
                    isSucces = false;
                    DataTable dt = ((DataView)this.gridView1.DataSource).Table.Copy();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //已经存在这个人的加班信息
                        string str_sql = "select * from V_Attend_OT_Set as a  where 1=1 ";
                        str_sql += " AND myUserID='" + dt.Rows[i]["UserID"].ToString() + "' ";//用户编号
                        str_sql += " AND( CONVERT(varchar(16),OTStrDateSel,120) between '"
                            + DateTime.Parse(dt.Rows[i]["OTStrDate"].ToString()).ToString("yyyy-MM-dd hh:mm")
                            + " and  " + DateTime.Parse(dt.Rows[i]["OTEndTime"].ToString()).ToString("yyyy-MM-dd hh:mm");//加班日期
                        str_sql += " or CONVERT(varchar(16),OTEndTimeSel,120) between '"
                            + DateTime.Parse(dt.Rows[i]["OTStrDate"].ToString()).ToString("yyyy-MM-dd hh:mm")
                           + " and  " + DateTime.Parse(dt.Rows[i]["OTEndTime"].ToString()).ToString("yyyy-MM-dd hh:mm") + ")";//加班日期
                        if (SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql).Rows.Count > 0)
                        {
                            DataValid.ShowErrorInfo(this.ErrorInfo, null, dt.Rows[i]["UserName"].ToString() + "已经存在【" +
                                DateTime.Parse(dt.Rows[i]["OTStrDate"].ToString()).ToString("yyyy-MM-dd hh:mm").ToString()
                                + "】与【" + DateTime.Parse(dt.Rows[i]["OTStrDate"].ToString()).ToString("yyyy-MM-dd hh:mm").ToString()
                                + "】的时间段内已经有加班登记信息！");
                            return;
                        }


                        //已经存在这个人的欠勤信息
                        str_sql = "select * from V_Attend_NoAttend_i as a  where 1=1 ";
                        str_sql += " AND myUserID='" + dt.Rows[i]["UserID"].ToString() + "' ";//用户编号
                        str_sql += " AND CONVERT(varchar(10),NoAttendDate,120) between '" + DateTime.Parse(dt.Rows[i]["OTStrDate"].ToString()).ToString("yyyy-MM-dd")
                            + " and  " + DateTime.Parse(dt.Rows[i]["OTEndTime"].ToString()).ToString("yyyy-MM-dd");//欠勤日期

                        if (SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql).Rows.Count > 0)
                        {
                            DataValid.ShowErrorInfo(this.ErrorInfo, null, dt.Rows[i]["UserName"].ToString() + "已经存在【" +
                                DateTime.Parse(dt.Rows[i]["OTStrDate"].ToString()).ToString("yyyy-MM-dd hh:mm").ToString()
                                + "】与【" + DateTime.Parse(dt.Rows[i]["OTStrDate"].ToString()).ToString("yyyy-MM-dd hh:mm").ToString()
                                + "】存在欠勤信息,不能登记加班信息！");
                            return;
                        }

                        //已经存在了请假信息
                        str_sql = "select * from V_Attend_Vacation_i as a  where 1=1 ";
                        str_sql += " AND myTeamName='" + this.lookUpEditmyTeamName.EditValue.ToString() + "' ";//向别-班别
                        str_sql += " AND( CONVERT(varchar(16),VacationBgnDate,120) between '"
                          + DateTime.Parse(dt.Rows[i]["OTStrDate"].ToString()).ToString("yyyy-MM-dd hh:mm")
                          + " and  " + DateTime.Parse(dt.Rows[i]["OTEndTime"].ToString()).ToString("yyyy-MM-dd hh:mm");//加班日期
                        str_sql += " or CONVERT(varchar(16),VacationEndDate,120) between '"
                            + DateTime.Parse(dt.Rows[i]["OTStrDate"].ToString()).ToString("yyyy-MM-dd hh:mm")
                           + " and  " + DateTime.Parse(dt.Rows[i]["OTEndTime"].ToString()).ToString("yyyy-MM-dd hh:mm") + ")";//加班日期
                        if (SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql).Rows.Count <= 0)
                        {
                            DataValid.ShowErrorInfo(this.ErrorInfo, null, dt.Rows[i]["UserName"].ToString() + "已经存在【" +
                                  DateTime.Parse(dt.Rows[i]["OTStrDate"].ToString()).ToString("yyyy-MM-dd hh:mm").ToString()
                                  + "】与【" + DateTime.Parse(dt.Rows[i]["OTStrDate"].ToString()).ToString("yyyy-MM-dd hh:mm").ToString() + "】的请假信息，不不能登记加班信息！");
                            return;
                        }

                    }
                }
                isSucces = true;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        //修改数据取得
        public void GetDataRowValue(DataRow[] drs)
        {
            lookUpEditJobFor.Text = drs[0]["JobForName"].ToString();//向别
            lookUpEditmyTeamName.Text = drs[0]["orgTeamName"].ToString();//班别!
            lookUpOTTypeName.Text = drs[0]["OTTypeName"].ToString();//加班事由!;
            lookUpOTApplyName.Text = drs[0]["OTApplyName"].ToString();//申请事由!;
            dateBeginDate.EditValue = drs[0]["OtDate"].ToString();//加班开始日期!/
            dtEndTime.EditValue = drs[0]["StartTime"].ToString();//加班开始时间!");
            dtEndTime2.EditValue = drs[0]["EndTime"].ToString();//加班结束时间!");

            if (gridView1.DataSource == null)
            {
                m_tblDataList.Columns.Add("SlctValue", typeof(Boolean));
                m_tblDataList.Columns.Add("ID");
                m_tblDataList.Columns.Add("UserID");
                m_tblDataList.Columns.Add("UserName");
                m_tblDataList.Columns.Add("DutyName");
                m_tblDataList.Columns.Add("StatusNames");
                m_tblDataList.Columns.Add("OtDate");
                m_tblDataList.Columns.Add("StartTime");
                m_tblDataList.Columns.Add("EndTime");
                m_tblDataList.Columns.Add("OTNum");
                m_tblDataList.Columns.Add("OTKind");
                m_tblDataList.Columns.Add("OTType");
                m_tblDataList.Columns.Add("OTApply");
                m_tblDataList.Columns.Add("");
            }
            DataRow dr;
            for (int i = 0; i < drs.Length; i++)
            {
                dr = m_tblDataList.NewRow();
                dr["SlctValue"] = true;
                dr["ID"] = drs[i]["myID"].ToString();
                dr["UserID"] = drs[i]["myUserID"].ToString();//人会员编号
                dr["UserName"] = drs[i]["UserName"].ToString();//会员姓名
                dr["DutyName"] = drs[i]["myUserID"].ToString();//人会员职等
                dr["StatusNames"] = drs[i]["StatusNames"].ToString();//人会员状态
                dr["OtDate"] = drs[i]["OtDate"].ToString();//开始日期
                dr["StartTime"] = drs[i]["StartTime"].ToString();//开始时间
                dr["EndTime"] = drs[i]["EndTime"].ToString();//结束时间
                dr["OTNum"] = drs[i]["OTNum"].ToString();//加班小时数
                dr["OTKind"] = drs[i]["OTKind"].ToString();//加班类型
                dr["OTType"] = drs[i]["OTTypeName"].ToString();//加班事由
                dr["OTApply"] = drs[i]["OTApplyName"].ToString(); //申请事由
                m_tblDataList.Rows.Add(dr);
            }
            this.m_GridViewUtil.GridControlList.DataSource = m_tblDataList;

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
                //************向别************
                if (Common._myTeamName == "" || Common._myTeamName == null)
                {
                    strSql = string.Format(@"select distinct JobForID,JobForName from V_Produce_Para WHERE JobForID<>0 order by JobForName");
                }
                else
                {
                    strSql = string.Format(@"select distinct JobForID,JobForName from V_Produce_Para where  myTeamName in ('{0}') AND  JobForID<>0 order by JobForName", Common._myTeamName);
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


                //************工程别-班别************
                if (Common._myTeamName == "" || Common._myTeamName == null)
                {
                    strSql = string.Format(@"select distinct orgTeamName from V_Produce_Para WHERE orgTeamName<>''  order by orgTeamName");
                }
                else
                {
                    strSql = string.Format(@"Select distinct orgTeamName From V_Produce_Para  where  myTeamName in ('{0}') AND orgTeamName<>'' Order by orgTeamName", Common._myTeamName);
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

                //************加班事由************
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_OTType, lookUpOTTypeName, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect);
                strSql = string.Format(@"Select distinct * From P_OTType");
                m_tblOtType = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);
                //默认选中
                dr = m_tblOtType.NewRow();

                dr[0] = "-1";
                dr[1] = "全部";
                m_tblOtType.Rows.InsertAt(dr, 0);
                lookUpOTTypeName.Properties.DataSource = m_tblOtType.DefaultView;
                lookUpOTTypeName.Properties.ValueMember = m_tblOtType.Columns[0].ColumnName;
                lookUpOTTypeName.Properties.DisplayMember = m_tblOtType.Columns[1].ColumnName; ;

                if (dt_temp.Rows.Count > 0)
                {
                    lookUpOTTypeName.EditValue = m_tblOtType.Rows[0][0].ToString();
                }
                lookUpOTTypeName.ItemIndex = 0;
                lookUpOTTypeName.Properties.BestFit();


                //************申请事由***********
                strSql = string.Format(@"Select distinct * From P_OTApply");
                m_tblOtApply = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);
                //默认选中
                dr = m_tblOtApply.NewRow();

                dr[0] = "-1";
                dr[1] = "全部";
                m_tblOtApply.Rows.InsertAt(dr, 0);
                lookUpOTApplyName.Properties.DataSource = m_tblOtApply.DefaultView;
                lookUpOTApplyName.Properties.ValueMember = m_tblOtApply.Columns[0].ColumnName;
                lookUpOTApplyName.Properties.DisplayMember = m_tblOtApply.Columns[1].ColumnName; ;

                if (dt_temp.Rows.Count > 0)
                {
                    lookUpOTApplyName.EditValue = m_tblOtApply.Rows[0][0].ToString();
                }
                lookUpOTApplyName.ItemIndex = 0;
                lookUpOTApplyName.Properties.BestFit();


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
