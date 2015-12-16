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
using Framework.DataAccess;
using Framework.Libs;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using Framework.FileOperate;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using MachineSystem.SettingTable;

namespace MachineSystem.TabPage
{
    public partial class frmV_Attend_Line_Detail : Framework.Abstract.frmSearchBasic2
    {
        #region 变量定义
        /// <summary>
        /// 数据表
        /// </summary>
        DataTable m_tblDataList = new DataTable();
        string strTeamKind = "ALL";
        //异常导出
        string strAbnority = "";

        public DataTable m_tblAttendConfirm = new DataTable();


        #endregion

        #region 画面初始化
        public frmV_Attend_Line_Detail()
        {
            InitializeComponent();
 
            dtDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd");

            this.m_GridViewUtil.GridControlList = this.gridControl1;
            this.m_GridViewUtil.ParentGridView = this.gridView1;
            m_ParenSlctColName = "SlctValue";
            CopyAddVisibility = false;
            NewButtonVisibility = false;
            EditButtonVisibility = false;
            SaveButtonVisibility = false;
            CancelButtonVisibility = false;
            ExcelButtonVisibility = false;
            PrintButtonVisibility = false;
            SearchButtonVisibility = false;
            SelectAllButtonVisibility = false;
            SelectOffButtonVisibility = false;
            DeleteButtonVisibility = false;

        }
        public frmV_Attend_Line_Detail(string pardate)
        {
            InitializeComponent();
            DateTime dtpar = DateTime.Parse(pardate);
            string m_CurrentTime = dtpar.ToString("yyyy-MM-dd");
            dtDateTime.Text = dtpar.ToLongDateString();
            
            this.m_GridViewUtil.GridControlList = this.gridControl1;
            this.m_GridViewUtil.ParentGridView = this.gridView1;
            m_ParenSlctColName = "SlctValue";
            CopyAddVisibility = false;
            NewButtonVisibility = false;
            EditButtonVisibility = false;
            SaveButtonVisibility = false;
            CancelButtonVisibility = false;
            ExcelButtonVisibility = false;
            PrintButtonVisibility = false;
            SearchButtonVisibility = false;
            SelectAllButtonVisibility = false;
            SelectOffButtonVisibility = false;
            DeleteButtonVisibility = false;

        }

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                base.SetFormValue();

                repositoryItemComboBox4.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(repositoryItemComboBox4_ParseEditValue);
                repositoryItemComboBox5.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(repositoryItemComboBox4_ParseEditValue);
                repositoryItemComboBox6.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(repositoryItemComboBox4_ParseEditValue);

                repositoryItemComboBox4.SelectedValueChanged += new EventHandler(repositoryItemComboBox4_SelectedValueChanged);
                repositoryItemComboBox5.SelectedValueChanged += new EventHandler(repositoryItemComboBox5_SelectedValueChanged);
                repositoryItemComboBox6.SelectedValueChanged += new EventHandler(repositoryItemComboBox6_SelectedValueChanged);
                GetGridInitComBobox();
                //全部查看
                this.btnShowAll.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
                this.btnShowDay.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                this.btnShowNight.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                strTeamKind = "ALL";
                GetDspDataList();
            }
            catch (Exception ex)
            {
                XtraMsgBox.Show("画面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }


        /// <summary>
        /// 获取科长系长组长 确认下拉
        /// </summary>
        public void GetGridInitComBobox()
        {
            string str1 = string.Format(@"SELECT ID,pName FROM Attend_Confirm");
            m_tblAttendConfirm = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str1);
            for (int i = 0; i < m_tblAttendConfirm.Rows.Count; i++)
            {
                CboItemEntity item4 = new CboItemEntity();
                item4.Value = m_tblAttendConfirm.Rows[i]["ID"].ToString();
                item4.Text = m_tblAttendConfirm.Rows[i]["pName"].ToString();
                repositoryItemComboBox4.Items.Add(item4);

                CboItemEntity item5 = new CboItemEntity();
                item5.Value = m_tblAttendConfirm.Rows[i]["ID"].ToString();
                item5.Text = m_tblAttendConfirm.Rows[i]["pName"].ToString();
                repositoryItemComboBox5.Items.Add(item5);

                CboItemEntity item6 = new CboItemEntity();
                item6.Value = m_tblAttendConfirm.Rows[i]["ID"].ToString();
                item6.Text = m_tblAttendConfirm.Rows[i]["pName"].ToString();
                repositoryItemComboBox6.Items.Add(item6);
            }
        }
        #endregion

        #region 画面按钮功能处理方法

        /// <summary>
        /// 检索
        /// </summary>
        /// <param name="frmBaseToolXC"></param>
        protected override void SetSearchProc(frmSearchBasic2 frmBaseToolXC)
        {
            GetDspDataList();
        }

        #endregion

        #region 事件处理方法
        

        private void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
            GridView grid = sender as GridView;

            DataRow dr = gridView1.GetDataRow(e.RowHandle);

            //确定按钮的事件方法
            if (grid.FocusedColumn.Name == "gridColumn23")
            {
                //增加更新数据
                InsertOrUpdate(int.Parse(dr["ID"].ToString ()),
            DateTime.Parse (dr["attenddate"].ToString()),
            dr["WearConfirmName"].ToString(),
            dr["LeaderConfirmName"].ToString(),
            dr["SectionConfirmName"].ToString(),
            int.Parse(dr["JobForID"].ToString()), 
            int.Parse(dr["ProjectID"].ToString()), 
            int.Parse(dr["LineID"].ToString ()), 
            int.Parse(dr["TeamID"].ToString ()));  
            }
        }

        void repositoryItemComboBox4_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            if (e.Value != null)
            {
                e.Value = e.Value.ToString();
                e.Handled = true;
            }
        }

        /// <summary>
        /// 选择组长确认
        /// </summary>
        void repositoryItemComboBox4_SelectedValueChanged(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0) return;

            ComboBoxEdit cbx = (ComboBoxEdit)sender;
            CboItemEntity item = (CboItemEntity)cbx.SelectedItem;

            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();

            m_tblDataList.Rows[gridView1.FocusedRowHandle]["LeaderConfirmName"] = item.Text.ToString();
            m_tblDataList.Rows[gridView1.FocusedRowHandle]["LeaderConfirm"] = item.Value.ToString(); 
            string str_sql = string.Empty;
            int result = 0;
            str_sql = string.Format(@"select * from Attend_Line_Detail_Confirm where JobForID='{0}'and ProjectID='{1}'and LineID='{2}'and TeamID='{3}'and AttendDate='{4}'",
                                    m_tblDataList.Rows[gridView1.FocusedRowHandle]["JobForID"].ToString(), m_tblDataList.Rows[gridView1.FocusedRowHandle]["ProjectID"].ToString(),
                                    m_tblDataList.Rows[gridView1.FocusedRowHandle]["LineID"].ToString(), m_tblDataList.Rows[gridView1.FocusedRowHandle]["TeamID"].ToString(),
                                    dtDateTime.DateTime.ToString("yyyy-MM-dd"));
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
            if (dt_temp.Rows.Count > 0)
            {
                //修改
                m_dicPrimarName["ID"] = dt_temp.Rows[0]["ID"].ToString();
                m_dicItemData["LeaderConfirm"] = m_tblDataList.Rows[gridView1.FocusedRowHandle]["LeaderConfirm"].ToString();

                result = SysParam.m_daoCommon.SetModifyDataIdentityColumn("Attend_Line_Detail_Confirm", m_dicItemData, m_dicPrimarName);
            }
            else
            {
                //新增
                string ID = SysParam.m_daoCommon.GetMaxNoteNo("Attend_Line_Detail_Confirm", "ID");
                m_dicItemData["ID"] = ID;
                m_dicItemData["JobForID"] = m_tblDataList.Rows[gridView1.FocusedRowHandle]["JobForID"].ToString();
                m_dicItemData["ProjectID"] = m_tblDataList.Rows[gridView1.FocusedRowHandle]["ProjectID"].ToString();
                m_dicItemData["LineID"] = m_tblDataList.Rows[gridView1.FocusedRowHandle]["LineID"].ToString();
                m_dicItemData["TeamID"] = m_tblDataList.Rows[gridView1.FocusedRowHandle]["TeamID"].ToString();
                m_dicItemData["AttendDate"] = dtDateTime.DateTime.ToString("yyyy-MM-dd");
                m_dicItemData["LeaderConfirm"] = m_tblDataList.Rows[gridView1.FocusedRowHandle]["LeaderConfirm"].ToString();

                result = SysParam.m_daoCommon.SetInsertDataItem("Attend_Line_Detail_Confirm", m_dicItemData);
            }
        }

        /// <summary>
        /// 选择系长确认
        /// </summary>
        void repositoryItemComboBox5_SelectedValueChanged(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0) return;

            ComboBoxEdit cbx = (ComboBoxEdit)sender;
            CboItemEntity item = (CboItemEntity)cbx.SelectedItem;

            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();

            m_tblDataList.Rows[gridView1.FocusedRowHandle]["WearConfirmName"] = item.Text.ToString();
            m_tblDataList.Rows[gridView1.FocusedRowHandle]["WearConfirm"] = item.Value.ToString();

            string str_sql = string.Empty;
            int result = 0;
            str_sql = string.Format(@"select * from Attend_Line_Detail_Confirm where JobForID='{0}'and ProjectID='{1}'and LineID='{2}'and TeamID='{3}'and AttendDate='{4}'",
                                    m_tblDataList.Rows[gridView1.FocusedRowHandle]["JobForID"].ToString(), m_tblDataList.Rows[gridView1.FocusedRowHandle]["ProjectID"].ToString(),
                                    m_tblDataList.Rows[gridView1.FocusedRowHandle]["LineID"].ToString(), m_tblDataList.Rows[gridView1.FocusedRowHandle]["TeamID"].ToString(),
                                    dtDateTime.DateTime.ToString("yyyy-MM-dd"));
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
            if (dt_temp.Rows.Count > 0)
            {
                //修改
                m_dicPrimarName["ID"] = dt_temp.Rows[0]["ID"].ToString();
                m_dicItemData["WearConfirm"] = m_tblDataList.Rows[gridView1.FocusedRowHandle]["WearConfirm"].ToString();

                result = SysParam.m_daoCommon.SetModifyDataIdentityColumn("Attend_Line_Detail_Confirm",m_dicItemData,m_dicPrimarName);
            }
            else 
            {
                //新增
                string ID = SysParam.m_daoCommon.GetMaxNoteNo("Attend_Line_Detail_Confirm", "ID");
                m_dicItemData["ID"] = ID;
                m_dicItemData["JobForID"] = m_tblDataList.Rows[gridView1.FocusedRowHandle]["JobForID"].ToString();
                m_dicItemData["ProjectID"] = m_tblDataList.Rows[gridView1.FocusedRowHandle]["ProjectID"].ToString();
                m_dicItemData["LineID"] = m_tblDataList.Rows[gridView1.FocusedRowHandle]["LineID"].ToString();
                m_dicItemData["TeamID"] = m_tblDataList.Rows[gridView1.FocusedRowHandle]["TeamID"].ToString();
                m_dicItemData["AttendDate"] = dtDateTime.DateTime.ToString("yyyy-MM-dd");
                m_dicItemData["WearConfirm"] = m_tblDataList.Rows[gridView1.FocusedRowHandle]["WearConfirm"].ToString();

                result = SysParam.m_daoCommon.SetInsertDataItem("Attend_Line_Detail_Confirm", m_dicItemData);
            }

        }

        /// <summary>
        /// 选择课长确认
        /// </summary>
        void repositoryItemComboBox6_SelectedValueChanged(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0) return;

            ComboBoxEdit cbx = (ComboBoxEdit)sender;
            CboItemEntity item = (CboItemEntity)cbx.SelectedItem;

            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();

            m_tblDataList.Rows[gridView1.FocusedRowHandle]["SectionConfirmName"] = item.Text.ToString();
            m_tblDataList.Rows[gridView1.FocusedRowHandle]["SectionConfirm"] = item.Value.ToString(); 
            string str_sql = string.Empty;
            int result = 0;
            str_sql = string.Format(@"select * from Attend_Line_Detail_Confirm where JobForID='{0}'and ProjectID='{1}'and LineID='{2}'and TeamID='{3}'and AttendDate='{4}'",
                                    m_tblDataList.Rows[gridView1.FocusedRowHandle]["JobForID"].ToString(), m_tblDataList.Rows[gridView1.FocusedRowHandle]["ProjectID"].ToString(),
                                    m_tblDataList.Rows[gridView1.FocusedRowHandle]["LineID"].ToString(), m_tblDataList.Rows[gridView1.FocusedRowHandle]["TeamID"].ToString(),
                                    dtDateTime.DateTime.ToString("yyyy-MM-dd"));
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
            if (dt_temp.Rows.Count > 0)
            {
                //修改
                m_dicPrimarName["ID"] = dt_temp.Rows[0]["ID"].ToString();
                m_dicItemData["SectionConfirm"] = m_tblDataList.Rows[gridView1.FocusedRowHandle]["SectionConfirm"].ToString();

                result = SysParam.m_daoCommon.SetModifyDataIdentityColumn("Attend_Line_Detail_Confirm", m_dicItemData, m_dicPrimarName);

            }
            else
            {
                //新增
                string ID = SysParam.m_daoCommon.GetMaxNoteNo("Attend_Line_Detail_Confirm", "ID");
                m_dicItemData["ID"] = ID;
                m_dicItemData["JobForID"] = m_tblDataList.Rows[gridView1.FocusedRowHandle]["JobForID"].ToString();
                m_dicItemData["ProjectID"] = m_tblDataList.Rows[gridView1.FocusedRowHandle]["ProjectID"].ToString();
                m_dicItemData["LineID"] = m_tblDataList.Rows[gridView1.FocusedRowHandle]["LineID"].ToString();
                m_dicItemData["TeamID"] = m_tblDataList.Rows[gridView1.FocusedRowHandle]["TeamID"].ToString();
                m_dicItemData["AttendDate"] = dtDateTime.DateTime.ToString("yyyy-MM-dd");
                m_dicItemData["SectionConfirm"] = m_tblDataList.Rows[gridView1.FocusedRowHandle]["SectionConfirm"].ToString();

                result = SysParam.m_daoCommon.SetInsertDataItem("Attend_Line_Detail_Confirm", m_dicItemData);
            }
        }

        //全部
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            this.btnShowAll.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnShowDay.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.btnShowNight.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            strTeamKind = "ALL";
            GetDspDataList();
        }

        //白班
        private void btnShowDay_Click(object sender, EventArgs e)
        {
            strTeamKind = "DAY";
            this.btnShowAll.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.btnShowDay.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnShowNight.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            GetDspDataList();
        }

        //晚班
        private void btnShowNight_Click(object sender, EventArgs e)
        {
            strTeamKind = "NIGHT";
            this.btnShowAll.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.btnShowDay.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.btnShowNight.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            GetDspDataList();
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
                string str_sql = "Select  CAST('0' AS Bit) AS SlctValue, * from	V_Attend_Line_Detail_Confirm  where 1=1 ";
                //传入日期
                DateTime dtBegin = DateTime.Parse(dtDateTime.Text);
                str_sql += " and AttendDate='" + dtBegin.ToString("yyyy-MM-dd") + "'";

                if (strTeamKind == "DAY")
                {
                    str_sql += " and TeamSetNM ='白班' ";
                }
                if (strTeamKind == "NIGHT")
                {
                    str_sql += " and TeamSetNM ='晚班' ";
                }
                //显示红色的
                if (strAbnority != "")
                {
                    str_sql += " and chuqinLvColor ='red' ";
                }
                if (Common._personid != Common._Administrator)
                {// &&Common._myTeamName != "" && Common._myTeamName != null
                    str_sql += " and myTeamName='" + Common._myTeamName + "'";
                }

                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (!m_tblDataList.Columns.Contains("LeaderConfirmName")) 
                {
                    m_tblDataList.Columns.Add("LeaderConfirmName");
                    m_tblDataList.Columns.Add("WearConfirmName");
                    m_tblDataList.Columns.Add("SectionConfirmName");
                }
                for (int i = 0; i < m_tblDataList.Rows.Count; i++) 
                {
                    for (int k = 0; k < m_tblAttendConfirm.Rows.Count; k++) 
                    {
                        if (m_tblDataList.Rows[i]["LeaderConfirm"].ToString() == m_tblAttendConfirm.Rows[k]["id"].ToString()) 
                        {
                            m_tblDataList.Rows[i]["LeaderConfirmName"] = m_tblAttendConfirm.Rows[k]["pName"].ToString();
                        }
                        else if (m_tblDataList.Rows[i]["WearConfirm"].ToString() == m_tblAttendConfirm.Rows[k]["id"].ToString())
                        {
                            m_tblDataList.Rows[i]["WearConfirmName"] = m_tblAttendConfirm.Rows[k]["pName"].ToString();
                        }
                        else if (m_tblDataList.Rows[i]["SectionConfirm"].ToString() == m_tblAttendConfirm.Rows[k]["id"].ToString())
                        {
                            m_tblDataList.Rows[i]["SectionConfirmName"] = m_tblAttendConfirm.Rows[k]["pName"].ToString();
                        }
                    }
                }
                this.gridControl1.DataSource = m_tblDataList;
                gridView1.Columns["LeaderConfirmName"].OptionsColumn.AllowEdit = true;
                gridView1.Columns["LeaderConfirmName"].OptionsColumn.ReadOnly = false;
                gridView1.Columns["WearConfirmName"].OptionsColumn.AllowEdit = true;
                gridView1.Columns["WearConfirmName"].OptionsColumn.ReadOnly = false;
                gridView1.Columns["SectionConfirmName"].OptionsColumn.AllowEdit = true;
                gridView1.Columns["SectionConfirmName"].OptionsColumn.ReadOnly = false;
               
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }
        #endregion

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            DevExpress.Utils.AppearanceDefault appRed = new DevExpress.Utils.AppearanceDefault
                (Color.Black, Color.Red, Color.Empty, Color.SeaShell,System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            DevExpress.Utils.AppearanceDefault appYellow = new DevExpress.Utils.AppearanceDefault
                (Color.Black, Color.Yellow, Color.Empty, Color.SeaShell,System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            DevExpress.Utils.AppearanceDefault appGreen = new DevExpress.Utils.AppearanceDefault
                (Color.Black, Color.Green, Color.Empty, Color.SeaShell,System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            string strTemp = gridView1.GetRowCellValue(e.RowHandle, "cntColor").ToString().Trim();
            if (e.Column.FieldName == "InCount" || e.Column.FieldName == "NeedCount")
            {
                switch (strTemp)
                {
                    case "yellow": DevExpress.Utils.AppearanceHelper.Apply(e.Appearance, appYellow); break;
                }
            }
        }

        private void btnAbnority_Click(object sender, EventArgs e)
        {
            //异常导出,颜色红色的导出
            strAbnority = "red";
            GetDspDataList();
            FileOperate.ExportExcel(this.m_GridViewUtil.ParentGridView);
        }

        private void btnExcelAll_Click(object sender, EventArgs e)
        {
            FileOperate.ExportExcel(this.m_GridViewUtil.ParentGridView);
        }

      
        /// <summary>
        /// 增加更新操作
        /// </summary>
        private void InsertOrUpdate(int intID,
            DateTime dtattenddate,
            string strWearConfirm,
            string strLeaderConfirm,
            string strSectionConfirm,
            int intJobForID, 
            int intProjectID, 
            int intLineID, 
            int intTeamID)
        {
            try
            {
                 //如果有数据
                if (IsAttendConfirmDuplicated(dtattenddate, intJobForID, intProjectID, intLineID, intTeamID)) 
                    this.ScanMode = Common.DataModifyMode.upd;
                else
                    this.ScanMode = Common.DataModifyMode.add;

                    if (this.ScanMode == Common.DataModifyMode.add)
                    {
                        m_dicItemData["ID"] = SysParam.m_daoCommon.GetMaxNoteNo("Attend_Line_Detail_Confirm", "ID").ToString();
                        m_dicItemData["AttendDate"] = dtattenddate.ToString();
                        m_dicItemData["WearConfirm"] = strWearConfirm;
                        m_dicItemData["LeaderConfirm"] = strLeaderConfirm;
                        m_dicItemData["SectionConfirm"] = strSectionConfirm;
                        m_dicItemData["JobForID"] = intJobForID.ToString();
                        m_dicItemData["ProjectID"] = intProjectID.ToString();
                        m_dicItemData["LineID"] = intLineID.ToString();
                        m_dicItemData["TeamID"] = intTeamID.ToString();
                        int effCount = SysParam.m_daoCommon.SetInsertDataItem("Attend_Line_Detail_Confirm", m_dicItemData);
                    }
                    else
                    {
                        m_dicItemData["ID"] =intID.ToString();
                        m_dicItemData["AttendDate"] = dtattenddate.ToString();
                        m_dicItemData["WearConfirm"] = strWearConfirm;
                        m_dicItemData["LeaderConfirm"] = strLeaderConfirm;
                        m_dicItemData["SectionConfirm"] = strSectionConfirm;
                        m_dicItemData["JobForID"] = intJobForID.ToString();
                        m_dicItemData["ProjectID"] = intProjectID.ToString();
                        m_dicItemData["LineID"] = intLineID.ToString();
                        m_dicItemData["TeamID"] = intTeamID.ToString();
                        StringDictionary whereParamDic = new StringDictionary();
                        whereParamDic["ID"] = intID.ToString();
                        int effCount = SysParam.m_daoCommon.SetModifyDataItem("Attend_Line_Detail_Confirm", m_dicItemData, whereParamDic);
                    }
            }
            catch (Exception ex)
            {
                XtraMsgBox.Show("出现异常！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw ex;
            }
        }

        /// <summary>
        /// 是否存在确认数据
        /// </summary>
        private bool IsAttendConfirmDuplicated(DateTime dtattenddate,int intJobForID,int intProjectID,int intLineID,int intTeamID)
        {
          string strSql=" select count(1) as CountID "+
                "  from Attend_Line_Detail_Confirm where 1=1 " + 
                " and  AttendDate ='"+ dtattenddate.ToString("yyyy-MM-dd") + "' "+
                " and  intJobForID ="+ intJobForID + " "+
                " and intProjectID ="+intProjectID +" " +
                " and  intLineID ="+ intLineID + " "+
                " and intTeamID ="+intTeamID +" ";

            DataTable dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
