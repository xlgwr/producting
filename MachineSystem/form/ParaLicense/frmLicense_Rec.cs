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
using System.IO;
using MachineSystem.SysCommon;
using log4net;
/********************************************************************************
** 作者： xxx
** 创始时间：2015-6-8

** 修改人：xuensheng
** 修改时间：2015-7-14
** 修改内容：代码规范化

** 描述：
**    主要用于 免许登记 信息的资料查询、删除、批量删除操作
*********************************************************************************/
namespace MachineSystem.TabPage
{
    public partial class frmLicense_Rec : Framework.Abstract.frmSearchBasic2
    {
        #region 变量定义
        /// <summary>
        /// 数据表
        /// </summary>
        DataTable m_tblDataList = new DataTable();
        // 日志
        private static readonly ILog log = LogManager.GetLogger(typeof(frmLicense_Rec));
        /// <summary>
        /// 界面初始化标示
        /// </summary>
        bool isLoad = true;
        #endregion

        #region 画面初始化

        public frmLicense_Rec()
        {
            InitializeComponent();

            this.m_GridViewUtil.GridControlList = this.gridControl1;
            this.m_GridViewUtil.ParentGridView = this.gridView1;
            this.TableName = "V_License_Rec_i";//操作表名称
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
                //获取下拉框数据
                GetLookUpList();
                isLoad = false;
                lookUpEditJobFor.Text = Common._JobForName;
                lookUpEditProjectName.Text = Common._ProjectName;
                lookUpEditLineName.Text = Common._LineName;
                lookUpEditTeamName.Text = Common._TeamName;
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
        /// 查询
        /// </summary>
        protected override void SetSearchProc(frmSearchBasic2 frmBaseToolXC)
        {
            base.SetSearchProc(frmBaseToolXC);

            //GetDspDataList();
        }


        /// <summary>
        /// 新增
        /// </summary>
        protected override void SetInsertInit(bool isClear)
        {
            base.SetInsertInit(isClear);
            try
            {
                frmLicense_RecAdd frm = new frmLicense_RecAdd();
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
                else if (this.GetSelectList().Length > 1)
                {
                    XtraMsgBox.Show("修改时，只能勾选一条数据！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                frmLicense_RecAdd frm = new frmLicense_RecAdd();
                frm.ScanMode = Common.DataModifyMode.upd;
                //选择所有选择的数据
                frm.dr = this.GetSelectList()[0];

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
                if (XtraMsgBox.Show("是否删除数据？", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    Common.AdoConnect.Connect.CreateSqlTransaction();
                    if (drs.Length > 0)
                    {
                        for (int i = 0; i < drs.Length; i++)
                        {
                            DataRow dr = drs[i];

                            string str_sql = "select ID FROM P_License_Detail where ID='" + dr["myID"].ToString() + "'";
                            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                            if (dt_temp.Rows.Count == 1)
                            {
                                //***********免许资格数据删除*********
                                m_dicPrimarName.Clear();
                                m_dicItemData.Clear();
                                m_dicPrimarName["RecID"] = dr["myID"].ToString();
                                m_dicItemData["RecID"] = dr["myID"].ToString();
                                result = SysParam.m_daoCommon.SetDeleteDataItem("P_License_Rec_Entitle", m_dicItemData, m_dicPrimarName);

                                //***********免许表数据删除*********
                                m_dicPrimarName.Clear();
                                m_dicItemData.Clear();
                                m_dicPrimarName["ID"] = dr["myID"].ToString();
                                m_dicItemData["ID"] = dr["myID"].ToString();
                                result = SysParam.m_daoCommon.SetDeleteDataItem("License_Rec_i", m_dicItemData, m_dicPrimarName);

                                //***********免许明细数据删除*********
                                m_dicPrimarName.Clear();
                                m_dicItemData.Clear();
                                m_dicPrimarName["ID"] = dr["myID"].ToString();
                                m_dicItemData["ID"] = dr["myID"].ToString();
                                result = SysParam.m_daoCommon.SetDeleteDataItem("P_License_Detail", m_dicItemData, m_dicPrimarName);
                            }
                            else //免许明细数据多条时，多条件删除选中的一条
                            {
                                m_dicPrimarName.Clear();
                                m_dicItemData.Clear();
                                m_dicPrimarName["ID"] = dr["myID"].ToString();
                                m_dicItemData["ID"] = dr["myID"].ToString();

                                m_dicPrimarName["JobForID"] = dr["JobForID"].ToString();
                                m_dicItemData["JobForID"] = dr["JobForID"].ToString();

                                m_dicPrimarName["ProjectID"] = dr["ProjectID"].ToString();
                                m_dicItemData["ProjectID"] = dr["ProjectID"].ToString();

                                m_dicPrimarName["LineID"] = dr["LineID"].ToString();
                                m_dicItemData["LineID"] = dr["LineID"].ToString();

                                m_dicPrimarName["guanweiID"] = dr["guanweiID"].ToString();
                                m_dicItemData["guanweiID"] = dr["guanweiID"].ToString();

                                //m_dicPrimarName["level"] = dr["level"].ToString();
                                //m_dicItemData["level"] = dr["level"].ToString();
                                result = SysParam.m_daoCommon.SetDeleteDataItem("P_License_Detail", m_dicItemData, m_dicPrimarName);
                            }
                            if (result > 0)
                            {
                                //日志
                                SysParam.m_daoCommon.WriteLog("免许登记", "删除", dr["UserID"].ToString());
                            }
                        }
                    }
                    Common.AdoConnect.Connect.TransactionCommit();
                    GetDspDataList();
                    if (result > 0)
                    {
                        XtraMsgBox.Show("删除数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                       
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
        #endregion

        #region 事件处理方法

        /// <summary>
        /// 选择向别加载该向别下的工程别
        /// </summary>
        /// 
        private void lookUpEditJobFor_EditValueChanged(object sender, EventArgs e)
        {
            if (isLoad) return;

            string str_where = " where 1=1 ";
            if (lookUpEditJobFor.EditValue.ToString() != "-1")
            {
                str_where += " and JobForID='" + lookUpEditJobFor.EditValue.ToString() + "'";
            }
            string str_sql = string.Format(@"select DISTINCT ProjectID,ProjectName FROM  V_Produce_Para  " + str_where + " order by ProjectName");
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //默认选中
            DataRow dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            lookUpEditProjectName.Properties.DataSource = dt_temp.DefaultView;
            lookUpEditProjectName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookUpEditProjectName.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;
            if (dt_temp.Rows.Count > 0)
            {
                lookUpEditProjectName.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookUpEditProjectName.ItemIndex = 0;
            lookUpEditProjectName.Properties.BestFit();
        }

        /// <summary>
        /// 选择工程别加载该向别下的Line别
        /// </summary>

        private void lookUpEditProjectName_EditValueChanged(object sender, EventArgs e)
        {
            if (isLoad) return;

            string str_where = " where 1=1 ";
            if (lookUpEditJobFor.EditValue.ToString() != "-1")
            {
                str_where += " AND JobForID ='" + lookUpEditJobFor.EditValue.ToString() + "'";
            }
            if (lookUpEditProjectName.EditValue.ToString() != "-1")
            {
                str_where += " AND ProjectID='" + lookUpEditProjectName.EditValue.ToString() + "'";
            }

            string str_sql = string.Format(@"select DISTINCT LineID,LineName FROM  V_Produce_Para " + str_where + " order by LineName");
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //默认选中
            DataRow dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            lookUpEditLineName.Properties.DataSource = dt_temp.DefaultView;
            lookUpEditLineName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookUpEditLineName.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;
            if (dt_temp.Rows.Count > 0)
            {
                lookUpEditLineName.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookUpEditLineName.ItemIndex = 0;
            lookUpEditLineName.Properties.BestFit();
        }

        /// <summary>
        /// 选择Line别加载该LINE别下的班别
        /// </summary>
        /// 
        private void lookUpEditLineName_EditValueChanged(object sender, EventArgs e)
        {
            if (isLoad) return;

            string str_where = " where 1=1 ";
            if (lookUpEditJobFor.EditValue.ToString() != "-1")
            {
                str_where += " AND JobForID ='" + lookUpEditJobFor.EditValue.ToString() + "'";
            }
            if (lookUpEditProjectName.EditValue.ToString() != "-1")
            {
                str_where += " AND ProjectID='" + lookUpEditProjectName.EditValue.ToString() + "'";
            }
            if (lookUpEditLineName.EditValue.ToString() != "-1")
            {
                str_where += " AND LineID='" + lookUpEditLineName.EditValue.ToString() + "'";

            }
            string str_sql = string.Format(@"select DISTINCT TeamID ,TeamName FROM  V_Produce_Para  " + str_where + " order by TeamName");
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //默认选中
            DataRow dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            lookUpEditTeamName.Properties.DataSource = dt_temp.DefaultView;
            lookUpEditTeamName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookUpEditTeamName.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;
            if (dt_temp.Rows.Count > 0)
            {
                lookUpEditTeamName.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookUpEditTeamName.ItemIndex = 0;
            lookUpEditTeamName.Properties.BestFit();
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



        #region 共同方法
        /// <summary>
        /// 获取表格信息一览
        /// </summary>
        protected override void GetDspDataList()
        {
            try
            {
                //增加可用
                NewButtonEnabled = true;
                lookUpEditJobFor.Enabled = true;
                lookUpEditJobFor.Properties.ReadOnly = false;
                lookUpEditProjectName.Enabled = true;
                lookUpEditProjectName.Properties.ReadOnly = false;
                lookUpEditLineName.Enabled = true;
                lookUpEditLineName.Properties.ReadOnly = false;
                lookUpEditTeamName.Enabled = true;
                lookUpEditTeamName.Properties.ReadOnly = false;

                string str_sql = "Select  CAST('0' AS Bit) AS SlctValue,*  from  " + this.TableName + "  where 1=1 ";

                if (txtUserID.Text.Trim() != "")
                {
                    str_sql += " and UserID like '%" + txtUserID.Text.Trim() + "%' ";
                }
                if (txtUserName.Text.Trim() != "")
                {
                    str_sql += " and UserName like '%" + txtUserName.Text.Trim() + "%' ";
                }
                if (lookUpEditPart.Text != "" && lookUpEditPart.EditValue.ToString() != "-1")
                {
                    str_sql += " and  PartName = '" + lookUpEditPart.Text.ToString().Trim() + "' ";
                }
                if (lookUpEditDuty.Text != "" && lookUpEditDuty.EditValue.ToString() != "-1")
                {
                    str_sql += " and  DutyName = '" + lookUpEditDuty.Text.ToString().Trim() + "' ";
                }
                if (lookUpEditJobFor.EditValue != null && lookUpEditJobFor.EditValue.ToString() != "-1")
                {
                    str_sql += " and JobForID= " + lookUpEditJobFor.EditValue.ToString().Trim() + " ";
                }
                if (lookUpEditProjectName.EditValue != null && lookUpEditProjectName.EditValue.ToString() != "-1")
                {
                    str_sql += " and ProjectID= " + lookUpEditProjectName.EditValue.ToString().Trim() + " ";
                }
                if (lookUpEditLineName.EditValue != null && lookUpEditLineName.EditValue.ToString() != "-1")
                {
                    str_sql += " and LineID= " + lookUpEditLineName.EditValue.ToString().Trim() + " ";
                }
                if (lookUpEditTeamName.EditValue != null && lookUpEditTeamName.EditValue.ToString() != "-1")
                {
                    str_sql += " and TeamID= " + lookUpEditTeamName.EditValue.ToString().Trim() + " ";
                }

                if (lookUpEditLicenseType.EditValue != null && lookUpEditLicenseType.EditValue.ToString() != "-1")
                {
                    str_sql += " and LicenseType= " + lookUpEditLicenseType.EditValue.ToString().Trim() + " ";
                }

                //有效日期
                if ((dateValueBegin.EditValue != null && dateValueBegin.EditValue.ToString() != "") &&
             (dateValueEnd.EditValue != null && dateValueEnd.EditValue.ToString() != ""))
                {
                    DateTime dtBegin = DateTime.Parse(dateValueBegin.EditValue.ToString());
                    DateTime dtEnd = DateTime.Parse(dateValueEnd.EditValue.ToString());

                    str_sql += " and  ValidDate >= '" + dtBegin.ToString("yyyy-MM-dd") + "' AND ValidDate <= '" + dtEnd.ToString("yyyy-MM-dd") + "'  ";
                }

                if (cboNewFlag.Text != null && cboNewFlag.Text.ToString() != "-请选择-")
                {
                    str_sql += " and FlagName= '" + cboNewFlag.Text.ToString().Trim() + "' ";
                }

                //str_sql += " ORDER BY  OperDate,JobForID ,ProjectID ,LineID,TeamID DESC";
                str_sql += " ORDER BY JobForID asc,ProjectID asc ,LineID asc,GuanweiID asc";
                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (m_tblDataList.Rows.Count > 0)
                {
                    DeleteButtonEnabled = true;
                    this.SelectAllButtonEnabled = true;
                    this.SelectOffButtonEnabled = true;
                    EditButtonEnabled = true;
                }
                else
                {
                    DeleteButtonEnabled = false;
                    this.SelectAllButtonEnabled = false;
                    this.SelectOffButtonEnabled = false;
                    EditButtonEnabled = false;
                }

                this.m_GridViewUtil.GridControlList.DataSource = m_tblDataList;
                //设置只读在gridView1 设定
            }
            catch (Exception ex)
            {
                log.Error(ex);
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
        public void GetLookUpList()
        {
            //部门lookUpEditPart
            SysParam.m_daoCommon.SetLoopUpEdit(TableNames.V_User_Dept, this.lookUpEditPart, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect);
            //职等
            SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_User_Duty, this.lookUpEditDuty, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect);
            //免许类型
            SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_License_Entitle, this.lookUpEditLicenseType, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect);
            string strSql = "";
            //向别
            if (Common._myTeamName == "" || Common._myTeamName == null)
            {
                strSql = string.Format(@"select distinct JobForID,JobForName from V_Produce_para order by JobForName");
            }
            else
            {
                strSql = string.Format(@"select distinct JobForID,JobForName from V_Produce_para where  myTeamName in ('{0}') order by JobForName", Common._myTeamName);
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


            //工程别
            if (Common._myTeamName == "" || Common._myTeamName == null)
            {
                strSql = string.Format(@"select distinct ProjectID,ProjectName from V_Produce_para order by ProjectName");
            }
            else
            {
                strSql = string.Format(@"Select distinct ProjectID,ProjectName From v_Produce_para  where  myTeamName in ('{0}') Order by ProjectName", Common._myTeamName);
            }
            dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);

            //默认选中
            dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);
            lookUpEditProjectName.Properties.DataSource = dt_temp.DefaultView;
            lookUpEditProjectName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookUpEditProjectName.Properties.DisplayMember = dt_temp.Columns[1].ColumnName; ;

            if (dt_temp.Rows.Count > 0)
            {
                lookUpEditProjectName.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookUpEditProjectName.ItemIndex = 0;
            lookUpEditProjectName.Properties.BestFit();


            //Line别
            if (Common._myTeamName == "" || Common._myTeamName == null)
            {
                strSql = string.Format(@"select distinct LineID,LineName from V_Produce_para order by LineName");
            }
            else
            {
                strSql = string.Format(@"Select distinct LineID,LineName FROM v_Produce_Para  where  myTeamName in ('{0}') order by LineName", Common._myTeamName);
            }
            dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);

            //默认选中
            dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);
            lookUpEditLineName.Properties.DataSource = dt_temp.DefaultView;
            lookUpEditLineName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookUpEditLineName.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;

            if (dt_temp.Rows.Count > 0)
            {
                lookUpEditLineName.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookUpEditLineName.ItemIndex = 0;
            lookUpEditLineName.Properties.BestFit();

            //班别
            if (Common._myTeamName == "" || Common._myTeamName == null)
            {
                strSql = string.Format(@"select distinct TeamID,TeamName from V_Produce_para order by TeamName");
            }
            else
            {
                strSql = string.Format(@"Select distinct TeamID,TeamName FROM v_Produce_Para  where  myTeamName in ('{0}') order by TeamName", Common._myTeamName);
            }
            dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);

            //默认选中
            dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);
            lookUpEditTeamName.Properties.DataSource = dt_temp.DefaultView;
            lookUpEditTeamName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookUpEditTeamName.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;

            if (dt_temp.Rows.Count > 0)
            {
                lookUpEditTeamName.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookUpEditTeamName.ItemIndex = 0;
            lookUpEditTeamName.Properties.BestFit();


        }

        #endregion

    }
}
