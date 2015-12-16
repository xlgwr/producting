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
using MachineSystem.form.Search;
using DevExpress.XtraEditors.Repository;
using System.Collections;
using System.Collections.Specialized;

namespace MachineSystem.form.Pad
{
    /********************************************************************************	
    ** 作者： libing   	
    ** 创始时间：2015-07-14	
    ** 修改人：libing	
    ** 修改时间：
    ** 修改内容：
    ** 描述：排班登记
    *********************************************************************************/
    public partial class frmScheduling : Framework.Abstract.frmBaseXC
    {
        #region 变量定义
        /// <summary>
        /// 排班数据表
        /// </summary>
        public DataTable m_tblDataList = new DataTable();

        /// <summary>
        /// 选中的向别-班别
        /// </summary>
        public string m_teamName = string.Empty;

        /// <summary>
        /// 白班晚班
        /// </summary>
        DataTable m_tblTeamType = new DataTable();

        /// <summary>
        /// 删除的ID信息
        /// </summary>
        Hashtable m_hashtable = new Hashtable();
        
        private List<DateTime> m_lsdatetime = new List<DateTime>();

        // 日志	
        private static readonly ILog log = LogManager.GetLogger(typeof(frmLessFrequently));

        #endregion

        #region 画面初始化
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmScheduling(string m_teamName)
        {
            InitializeComponent();
            this.m_teamName = m_teamName;
            SetFormValue();
            this.TopMost = true;
            
        }
        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                repositoryItemDateEdit2.EditMask = "yyyy-MM-dd HH:mm";
                repositoryItemDateEdit2.Mask.UseMaskAsDisplayFormat = true;

                repositoryItemDateEdit3.EditMask = "yyyy-MM-dd HH:mm";
                repositoryItemDateEdit3.Mask.UseMaskAsDisplayFormat = true;

                gridView1.IndicatorWidth =70;


                //班别
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_Produce_Scheduling, lookTeamType, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefalutItemAllText);
                repositoryItemComboBox2.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(repositoryItemComboBox2_ParseEditValue);
                repositoryItemButtonEdit1.ButtonClick+=new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit1_ButtonClick);
                if (gridView1 != null)
                {
                    gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(gridView1_CustomDrawRowIndicator);
                }
               

                //获取白班晚班
                string str_sql = string.Format(@"select ID as pTypeID,pName as TeamSetName from P_Produce_TeamKind ");
                m_tblTeamType = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                for (int i = 0; i < m_tblTeamType.Rows.Count; i++)
                {
                    CboItemEntity item = new CboItemEntity();
                    item.Value = m_tblTeamType.Rows[i]["pTypeID"].ToString();
                    item.Text = m_tblTeamType.Rows[i]["TeamSetName"].ToString();
                    repositoryItemComboBox2.Items.Add(item);
                }
                if (Common._myTeamName != "")
                {
                    repositoryItemComboBox2.NullText = Common._myTeamName;
                }
                GetDspDataList();
            }
            catch (Exception ex)
            {

                 FrmAttendDialog FrmDialog = new FrmAttendDialog( "画面初始化失败！"+ex);
                 FrmDialog.ShowDialog();
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
                    e.Info.DisplayText =  e.RowHandle.ToString();
                }
            }
        }
        #endregion

        #region 画面按钮功能处理方法


        private void btnCanel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 保存加班登记
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                gridView1.CloseEditor();
                gridView1.UpdateCurrentRow();
                DataTable dt_Data = ((DataView)this.gridView1.DataSource).Table.Copy();
                dt_Data.AcceptChanges();
                //DataView view = new DataView(dt_Data.Copy());
                //view.Sort = "StrDate";
                //dt_Data = view.ToTable();

                string str_sql = string.Empty;

                //向别-班别
                str_sql = string.Format(@"select * from V_Produce_para where  myTeamName ='{0}'", m_teamName);

                DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

                Common.AdoConnect.Connect.CreateSqlTransaction();

                //先删除Attend_TeamSet对应向别的数据在添加
                m_dicItemData.Clear();
                m_dicPrimarName.Clear();
                m_dicItemData["JobForID"] = dt_temp.Rows[0]["JobForID"].ToString();
                m_dicItemData["ProjectID"] = dt_temp.Rows[0]["ProjectID"].ToString();
                m_dicItemData["LineID"] = dt_temp.Rows[0]["LineID"].ToString();
                m_dicItemData["TeamID"] = dt_temp.Rows[0]["TeamID"].ToString();
                m_dicPrimarName["JobForID"] = "true";
                m_dicPrimarName["LineID"] = "true";
                result = SysParam.m_daoCommon.SetDeleteDataItem(TableNames.Attend_TeamSet, m_dicItemData, m_dicPrimarName);


                for (int i = 0; i < dt_Data.Rows.Count; i++)
                {
                    if (i == dt_Data.Rows.Count - 1)
                    {
                        continue;
                    }
                    if (i > 0 && DateTime.Compare(DateTime.Parse(dt_Data.Rows[i]["StrDate"].ToString()),
                        DateTime.Parse(dt_Data.Rows[i - 1]["EndDate"].ToString())) < 0)
                    {
                        Common.AdoConnect.Connect.TransactionRollback();
                        FrmAttendDialog FrmDialog = new FrmAttendDialog("第【" + (i + 1) + "】行【" +
                            dt_Data.Rows[i]["StrDate"].ToString() + "】\n月排班开始时间不能小于上月的结束时间!");
                        FrmDialog.ShowDialog();
                        return;
                    }
                    if (DateTime.Compare(DateTime.Parse(dt_Data.Rows[i]["EndDate"].ToString()),
                        DateTime.Parse(dt_Data.Rows[i]["StrDate"].ToString())) < 0)
                    {
                        Common.AdoConnect.Connect.TransactionRollback();
                        FrmAttendDialog FrmDialog = new FrmAttendDialog("第【" + (i + 1) + "】行【" +
                           dt_Data.Rows[i]["StrDate"].ToString() + "】\n月排班开始时间不能小于结束时间!");
                        FrmDialog.ShowDialog();
                        return;
                    }

                    m_dicItemData.Clear();

                    m_dicItemData["JobForID"] = dt_temp.Rows[0]["JobForID"].ToString();
                    m_dicItemData["ProjectID"] = dt_temp.Rows[0]["ProjectID"].ToString();
                    m_dicItemData["LineID"] = dt_temp.Rows[0]["LineID"].ToString();
                    m_dicItemData["TeamID"] = dt_temp.Rows[0]["TeamID"].ToString();
                    m_dicItemData["BgnDate"] = dt_Data.Rows[i]["StrDate"].ToString();//开始时间
                    m_dicItemData["EndDate"] = dt_Data.Rows[i]["EndDate"].ToString();//结束时间
                    m_dicItemData["pTypeID"] = dt_Data.Rows[i]["pTypeID"].ToString(); ;//班种
                    m_dicItemData["Operid"] = Common._personid;
                    m_dicItemData["OperTime"] = DateTime.Now.ToString();

                    result = SysParam.m_daoCommon.SetInsertDataItem(TableNames.Attend_TeamSet, m_dicItemData);
                }

                ////删除界面已删除的数据
                //StringDictionary dicItemData = new StringDictionary();
                //StringDictionary dicPrimar = new StringDictionary();
                //foreach (string value in m_hashtable.Values)
                //{
                //    dicItemData["ID"] = value;
                //    dicPrimar["ID"] = value;
                //    result = SysParam.m_daoCommon.SetDeleteDataItem(TableNames.Attend_TeamSet, dicItemData, dicPrimar);
                //}

                Common.AdoConnect.Connect.TransactionCommit();
                FrmAttendDialog FrmDialogs = new FrmAttendDialog("保存数据成功！");
                FrmDialogs.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                Common.AdoConnect.Connect.TransactionRollback();
                FrmAttendDialog FrmDialog = new FrmAttendDialog("保存数据失败！" + ex);
                FrmDialog.ShowDialog();
                log.Error(ex);
            }
        }


        /// <summary>
        /// 选择班种
        /// </summary>
        private void lookTeamType_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lookTeamType.EditValue.ToString() != "-1")
                { 
                    GetDspDataList(); 
                }
            }
            catch (Exception ex)
            {

                 FrmAttendDialog FrmDialog = new FrmAttendDialog( "数据加载失败！"+ex);
                 FrmDialog.ShowDialog();
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
            string str_sql = string.Empty;
            try
            {
                //if (lookTeamType.EditValue.ToString() == "-1") return;

                //取得排班信息
                str_sql = string.Format(@"select BgnDate as StrDate,* from V_Attend_TeamSet_i where  myTeamName ='{0}'", m_teamName);

                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (m_tblDataList.Rows.Count > 0)
                {
                    lookTeamType.Visible = false;
                    label2.Visible = false;
                }
                else
                {
                    lookTeamType.Visible = true;
                    label2.Visible = true;
                    //如果没有则查询P_Produce_Scheduling_detail表中信息显示
                    if (lookTeamType.EditValue.ToString()!="-1") {
                        str_sql = string.Format(@"SELECT SchedulingID as ID ,StrDate,EndDate,TypeID as pTypeID,'' AS TeamSetName
                                                        FROM P_Produce_Scheduling_detail  where SchedulingID='{0}'",
                                                             lookTeamType.EditValue.ToString());
                        m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                    }
                    
                }
                if (!m_tblDataList.Columns.Contains("rowStatus"))
                {
                    m_tblDataList.Columns.Add("rowStatus");
                }

                //白班晚班赋值
                DataView view = new DataView(m_tblTeamType.Copy());
                for (int i = 0; i < m_tblDataList.Rows.Count; i++)
                {
                    view.RowFilter = "pTypeID='" + m_tblDataList.Rows[i]["pTypeID"] + "'";
                    DataTable dt_temp = view.ToTable();
                    if (dt_temp.Rows.Count > 0)
                    {
                        m_tblDataList.Rows[i]["TeamSetName"] = dt_temp.Rows[0]["TeamSetName"].ToString();
                    }

                }
                DataRow dr = m_tblDataList.NewRow();
                m_tblDataList.Rows.Add(dr);
                gridControl1.DataSource = m_tblDataList;
            }
            catch (Exception ex)
            {

                 FrmAttendDialog FrmDialog = new FrmAttendDialog( "画面加载失败！"+ex);
                 FrmDialog.ShowDialog();
            }
        }

        #endregion   

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();

            if (e.RowHandle<0) return;

            m_tblDataList = ((DataView)this.gridView1.DataSource).Table.Copy();
            m_tblDataList.AcceptChanges();

            if (e.Column.FieldName == "TeamSetName") 
            {
                DataTable dt = new DataTable();
                DataView view = new DataView(m_tblTeamType.Copy());
                view.RowFilter = "TeamSetName='" + m_tblDataList.Rows[e.RowHandle]["TeamSetName"].ToString() + "'";
                dt=view.ToTable();
                m_tblDataList.Rows[e.RowHandle]["pTypeID"] = dt.Rows[0]["pTypeID"].ToString();
                gridControl1.DataSource = m_tblDataList;
            }
            if (e.RowHandle == gridView1.RowCount - 1) 
            {
                DataRow dr = m_tblDataList.Rows[e.RowHandle];
                if (dr["StrDate"].ToString() != "" && dr["EndDate"].ToString() != "" && dr["pTypeID"].ToString() != "")
                {
                    dr["rowStatus"] = "2";//新增
                    DataRow row = m_tblDataList.NewRow();
                    m_tblDataList.Rows.Add(row);
                    gridControl1.DataSource = m_tblDataList;
                }
            }
            
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "Operate" && e.RowHandle < 0)
            {
                RepositoryItemButtonEdit _btn = (RepositoryItemButtonEdit)e.CellValue;
                if (_btn != null)
                {
                    _btn.Buttons[0].Caption = "";
                }

                return;
            }
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int _rowhandle;

            try
            {
                _rowhandle = gridView1.FocusedRowHandle;
                if (_rowhandle < 0)
                {
                    return;
                }
                DataRow dr=gridView1.GetFocusedDataRow();
                if (dr["StrDate"].ToString() == "" && dr["EndDate"].ToString() == "") return;

                //保存已删除的行
                if (dr["ID"].ToString() != "")
                {
                    m_hashtable.Add(m_hashtable.Count + 1, dr["ID"].ToString());
                }

                gridView1.DeleteRow(_rowhandle);
                gridControl1.Refresh();
                gridControl1.RefreshDataSource();
                m_tblDataList.AcceptChanges();
                SetAddDateTime();

                //gridView1.CloseEditor();
                //gridView1.UpdateCurrentRow();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SetAddDateTime()
        {
            DateTime _begin, _end;
            string _strbegin;
            string _strend;
            try
            {

                if (m_lsdatetime.Count > 0)
                {
                    m_lsdatetime.Clear();
                }
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    if (i == (gridView1.RowCount - 1))
                    {
                        continue;
                    }
                    _strbegin = gridView1.GetRowCellValue(i, "StrDate").ToString();
                    _strend = gridView1.GetRowCellValue(i,"EndDate").ToString();
                    _begin = DateTime.Parse(_strbegin);
                    _end = DateTime.Parse(_strend);
                    m_lsdatetime.Add(_begin);
                    m_lsdatetime.Add(_end);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
         
    }
}
