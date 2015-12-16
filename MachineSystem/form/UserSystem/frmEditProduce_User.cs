using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework.Libs;
using Framework.Abstract;
using MachineSystem.SysDefine;
using DevExpress.XtraEditors.Controls;
using MachineSystem.SettingTable;
using DevExpress.XtraEditors;
using System.Collections;
using System.Collections.Specialized;
using log4net;

/********************************************************************************
** 作者： xxx
** 创始时间：2015-6-8

** 修改人：xuensheng
** 修改时间：2015-8-1
** 修改内容：代码规范化

** 描述：
**    主要用于【 人员资料登记 】信息修改操作
*********************************************************************************/
namespace MachineSystem.form.UserSystem
{
    public partial class frmEditProduce_User : Framework.Abstract.frmBaseXC
    {
        #region 变量定义

        /// <summary>
        /// 选中行信息
        /// </summary>
        public DataRow dr;

        /// <summary>
        /// 界面初始化标示
        /// </summary>
        bool isLoad = true;
        /// <summary>
        /// 向别
        /// </summary>
        DataTable m_tblJobForID = new DataTable();

        /// <summary>
        /// 关位
        /// </summary>
        DataTable m_tblGuanwei = new DataTable();


        DataTable m_tblDataList = new DataTable();

        /// <summary>
        /// Flag标识
        /// </summary>
        ArrayList m_arrList = new ArrayList();
        // 日志
        private static readonly ILog log = LogManager.GetLogger(typeof(frmEditProduce_User));
        #endregion


        #region 画面初始化

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmEditProduce_User()
        {
            InitializeComponent();
            this.TopMost = true;
        }

        private void frmEditProduce_User_Load(object sender, EventArgs e)
        {
            SetFormValue();
        }
        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                //repositoryItemCheckEdit1.CheckedChanged += new EventHandler(repositoryItemCheckEdit1_CheckedChanged);
                repositoryItemComboBox2.ParseEditValue += new ConvertEditValueEventHandler(repositoryItemComboBox2_ParseEditValue);
                repositoryItemComboBox3.ParseEditValue += new ConvertEditValueEventHandler(repositoryItemComboBox2_ParseEditValue);
                repositoryItemComboBox6.ParseEditValue += new ConvertEditValueEventHandler(repositoryItemComboBox2_ParseEditValue);
                repositoryItemDateEdit2.ParseEditValue += new ConvertEditValueEventHandler(repositoryItemComboBox2_ParseEditValue);
                repositoryItemComboBox7.ParseEditValue += new ConvertEditValueEventHandler(repositoryItemComboBox2_ParseEditValue);
 
                repositoryItemComboBox2.EditValueChanged += new EventHandler(repositoryItemComboBox2_EditValueChanged);
                repositoryItemComboBox3.EditValueChanged += new EventHandler(repositoryItemComboBox3_EditValueChanged);
                repositoryItemComboBox6.EditValueChanged += new EventHandler(repositoryItemComboBox6_EditValueChanged);
                repositoryItemDateEdit2.EditValueChanged += new EventHandler(repositoryItemDateEdit2_EditValueChanged);
                repositoryItemComboBox7.EditValueChanged += new EventHandler(repositoryItemComboBox7_EditValueChanged);

                //状态
                string str_sql = string.Format("SELECT ID,pName FROM P_Produce_User_Status1 ");
                DataTable dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

                foreach (DataRow row in dt.Rows)
                {
                    this.lookStatus.Properties.Items.Add(row["ID"].ToString(), row["pName"].ToString());
                }
                GetComboBox();

                if (dr != null)
                {
                    GetDspDataList();
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
        /// 保存
        /// </summary>
        /// <param name="frmbase"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool isSucces = false ;
                GetInputChecks(ref isSucces);
                
                
                if (this.ScanMode == Common.DataModifyMode.del)
                {
                    SetModifyStatus();
                }
                else
                {
                    SetModifyProc();
                }
            }
            catch (Exception ex)
            {
                XtraMsgBox.Show("保存数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
         
        /// <summary>
        /// 修改人员状态
        /// </summary>
        public void SetModifyStatus()
        {
            int result = 0;
            m_dicPrimarName.Clear();
            m_dicPrimarName["UserID"] = dr["UserID"].ToString();
            m_dicItemData.Clear();
            m_dicItemData["UserID"] = dr["UserID"].ToString();

            //保存用户状态
            string strStatus = string.Empty;
            string strStatusName = string.Empty;
            for (int j = 0; j < lookStatus.Properties.Items.Count; j++)
            {
                CheckedListBoxItem item = lookStatus.Properties.Items[j];
                if (item.CheckState == CheckState.Checked)
                {
                    strStatus += item.Value.ToString() + ",";
                    strStatusName += item.Description.ToString() + ",";
                }
            }
            strStatus = strStatus.Substring(0, strStatus.Length - 1);
            strStatusName = strStatusName.Substring(0, strStatusName.Length - 1);

            m_dicItemData["StatusIDs"] = strStatus;
            m_dicItemData["StatusNames"] = strStatusName;
            result = SysParam.m_daoCommon.SetModifyDataItem("Produce_User", m_dicItemData, m_dicPrimarName);
            if (result > 0)
            {
                XtraMsgBox.Show("修改数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //日志
                SysParam.m_daoCommon.WriteLog("人员资料登记", "修改状态", txtID.Text.Trim());
                DialogResult = DialogResult.OK;
            }
        }


        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="RtnValue"></param>
        public void SetModifyProc()
        {

            try
            {
               
                int result = 0;
                Common.AdoConnect.Connect.CreateSqlTransaction();

                //获取当前的向别，工程别，Line别，班别
                string str_sql = string.Empty;
                m_tblDataList = ((DataView)this.gridView1.DataSource).ToTable();
                
                for (int i = 0; i < m_tblDataList.Rows.Count; i++)
                {
                    if (m_tblDataList.Rows.Count > 1//只有班长、组长才可以管理多个line
                     && m_tblDataList.Rows[i]["GuanweiName"].ToString().Trim()!="班长"
                     && m_tblDataList.Rows[i]["GuanweiName"].ToString().Trim() != "副班长"
                     && m_tblDataList.Rows[i]["GuanweiName"].ToString().Trim() != "组长")
                    {
                        XtraMsgBox.Show("只有班长、组长才可以管理多个line！",
                                   this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //取得向别，班别 ID
                    str_sql = string.Format(@"select top 1 JobForID,ProjectID,LineID,TeamID,GuanWeiID from V_Produce_Para where myTeamName='{0}' and GuanweiName='{1}'  Order by GuanweiName"
                        , m_tblDataList.Rows[i]["myTeamName"].ToString(), m_tblDataList.Rows[i]["GuanweiName"].ToString());
                    DataTable dt_temp_1 = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                    m_dicPrimarName.Clear();
                    m_dicPrimarName["UserID"] = dr["UserID"].ToString();
                    m_dicItemData.Clear();
                    m_dicItemData["UserID"] = dr["UserID"].ToString();

                    if (dt_temp_1.Rows.Count == 1)
                    {
                        m_dicItemData["JobForID"] = dt_temp_1.Rows[0]["JobForID"].ToString();
                        m_dicItemData["ProjectID"] = dt_temp_1.Rows[0]["ProjectID"].ToString();
                        m_dicItemData["LineID"] = dt_temp_1.Rows[0]["LineID"].ToString();
                        m_dicItemData["TeamID"] = dt_temp_1.Rows[0]["TeamID"].ToString();
                        m_dicItemData["GuanWeiID"] = dt_temp_1.Rows[0]["GuanWeiID"].ToString();
                        m_dicItemData["GuanweiSite"] = m_tblDataList.Rows[i]["GuanweiSite"].ToString();
                        
                    }
                    else
                    {
                        m_dicItemData["JobForID"] = "";
                        m_dicItemData["ProjectID"] = "";
                        m_dicItemData["LineID"] = "";
                        m_dicItemData["TeamID"] = "";
                        m_dicItemData["GuanWeiID"] = "";
                        m_dicItemData["GuanweiSite"] = "";
                    }
                    
                    //保存用户状态
                    string strStatus = string.Empty;
                    string strStatusName = string.Empty;
                    for (int j = 0; j < lookStatus.Properties.Items.Count; j++)
                    {
                        CheckedListBoxItem item = lookStatus.Properties.Items[j];
                        if (item.CheckState == CheckState.Checked)
                        {
                            strStatus += item.Value.ToString() + ",";
                            strStatusName += item.Description.ToString() + ",";
                        }
                    }
                    strStatus = strStatus.Substring(0, strStatus.Length - 1);
                    strStatusName = strStatusName.Substring(0, strStatusName.Length - 1);
                    
                    m_dicItemData["StatusIDs"] = strStatus;
                    m_dicItemData["StatusNames"] = strStatusName;

                     if (m_tblDataList.Rows[i]["AttendUnitName"].ToString() == "是")
                    {
                        m_dicItemData["AttendUnit"] = "1";
                        //更新考勤单位是1的数据到Produce_User表中
                        result = SysParam.m_daoCommon.SetModifyDataItem("Produce_User", m_dicItemData, m_dicPrimarName);
                    }
                     if (m_tblDataList.Rows.Count==1 && m_tblDataList.Rows[0]["pmark"].ToString() == "调出")
                     {
                         m_dicItemData["JobForID"] = "0";
                         m_dicItemData["ProjectID"] = "0";
                         m_dicItemData["LineID"] = "0";
                         m_dicItemData["TeamID"] = "0";
                         m_dicItemData["GuanWeiID"] = "0";
                         m_dicItemData["GuanweiSite"] = "0";
                         m_dicItemData["AttendUnit"] = "0";
                         //更新考勤单位是1的数据到Produce_User表中
                         result = SysParam.m_daoCommon.SetModifyDataItem("Produce_User", m_dicItemData, m_dicPrimarName);
                         m_dicItemData["JobForID"] = dt_temp_1.Rows[0]["JobForID"].ToString();
                         m_dicItemData["ProjectID"] = dt_temp_1.Rows[0]["ProjectID"].ToString();
                         m_dicItemData["LineID"] = dt_temp_1.Rows[0]["LineID"].ToString();
                         m_dicItemData["TeamID"] = dt_temp_1.Rows[0]["TeamID"].ToString();
                         m_dicItemData["GuanWeiID"] = dt_temp_1.Rows[0]["GuanWeiID"].ToString();
                         m_dicItemData["GuanweiSite"] = m_tblDataList.Rows[i]["GuanweiSite"].ToString();
                     }
                    m_dicItemData["StrDate"] = DateTime.Parse(m_tblDataList.Rows[i]["StrDate"].ToString()).ToString("yyyy-MM-dd");
                    if (m_tblDataList.Rows[i]["pmark"].ToString() == "调入")
                    {
                        //保存数据到Attend_Move表中
                        result = SetAttendMoveData(m_tblDataList.Rows[i]["ID"].ToString(),
                            m_dicItemData["JobForID"].ToString(), m_dicItemData["ProjectID"].ToString(),
                            m_dicItemData["LineID"].ToString(), m_dicItemData["TeamID"].ToString(),
                            m_dicItemData["GuanweiID"].ToString(), m_dicItemData["GuanweiSite"].ToString(),
                             m_dicItemData["StrDate"].ToString(), "调入");
                    }
                    else if (m_tblDataList.Rows[i]["pmark"].ToString() == "调整关位")
                    {
                        
                        //保存新数据到Attend_Move表中
                        result = SetAttendMoveData(m_tblDataList.Rows[i]["ID"].ToString(),
                            m_dicItemData["JobForID"].ToString(), m_dicItemData["ProjectID"].ToString(),
                            m_dicItemData["LineID"].ToString(), m_dicItemData["TeamID"].ToString(),
                            m_dicItemData["GuanweiID"].ToString(), m_dicItemData["GuanweiSite"].ToString(),
                             m_dicItemData["StrDate"].ToString(),   "调整关位");
                    }
                    else if (m_tblDataList.Rows[i]["pmark"].ToString() == "调出")
                    {
                        //保存新数据到Attend_Move表中
                        result = SetAttendMoveData(m_tblDataList.Rows[i]["ID"].ToString(),
                            m_dicItemData["JobForID"].ToString(), m_dicItemData["ProjectID"].ToString(),
                            m_dicItemData["LineID"].ToString(), m_dicItemData["TeamID"].ToString(),
                            m_dicItemData["GuanweiID"].ToString(), m_dicItemData["GuanweiSite"].ToString(),
                             m_dicItemData["StrDate"].ToString(),  "调出");
                    }
                    
                }

                if (result > 0)
                {
                    Common.AdoConnect.Connect.TransactionCommit();
                    XtraMsgBox.Show("修改数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //日志
                    SysParam.m_daoCommon.WriteLog("人员资料登记", "删除", txtID.Text.Trim());
                    DialogResult = DialogResult.OK;
                }

            }
            catch (Exception ex)
            {
                Common.AdoConnect.Connect.TransactionRollback();
                XtraMsgBox.Show("保存数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        #endregion

        #region 事件处理方法
        /// <summary>
        /// 选择向别信息
        /// </summary>
        void repositoryItemComboBox2_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string strID = gridView1.GetFocusedDataRow()["ID"].ToString();
                if (strID != "")
                {
                    XtraMsgBox.Show("向别-班别不能修改！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gridView1.BestFitColumns();
                    return;
                }
                gridView1.CloseEditor();
                gridView1.UpdateCurrentRow();
                m_tblDataList = ((DataView)this.gridView1.DataSource).ToTable();
                DataRow row = gridView1.GetFocusedDataRow();
                if (row["ID"].ToString()!="") {
                    return;
                }
                string strmyteamName = row["myTeamName"].ToString();
                m_tblDataList.Rows[gridView1.FocusedRowHandle]["myTeamName"] = strmyteamName;
                string str_sql = string.Format(@"Select GuanweiID, GuanweiName,SetNum From V_Produce_Para where myTeamName='{0}' Order by GuanweiID", strmyteamName);
                m_tblGuanwei = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                repositoryItemComboBox3.Items.Clear();
                //关位
                for (int i = 0; i < m_tblGuanwei.Rows.Count; i++)
                {
                    CboItemEntity item = new CboItemEntity();
                    //item.Value = i;
                    item.Value = m_tblGuanwei.Rows[i]["GuanweiID"].ToString();
                    item.Text = m_tblGuanwei.Rows[i]["GuanweiName"].ToString();
                    repositoryItemComboBox3.Items.Add(item);

                }

                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMsgBox.Show("加载关位失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 选择关位信息
        /// </summary>
         void repositoryItemComboBox3_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                gridView1.CloseEditor();
                gridView1.UpdateCurrentRow();
                m_tblDataList = ((DataView)this.gridView1.DataSource).ToTable();
                DataRow row = gridView1.GetFocusedDataRow();
                DataView view = new DataView(m_tblGuanwei.Copy());
                
                view.RowFilter = "GuanweiName='" + row["GuanweiName"].ToString() + "'";
                DataTable dt = view.ToTable();
                if (dt.Rows.Count > 0)
                {
                    m_tblDataList.Rows[gridView1.FocusedRowHandle]["GuanWeiID"] = dt.Rows[0]["GuanWeiID"].ToString();
                    //位置
                    int guanweiSite = int.Parse(dt.Rows[0]["SetNum"].ToString());
                    CboItemEntity item = new CboItemEntity();
                    repositoryItemComboBox6.Items.Clear();
                    for (int i = 0; i < guanweiSite; i++)
                    {
                        item = new CboItemEntity();
                        item.Value = i + 1;
                        item.Text = i + 1;
                        repositoryItemComboBox6.Items.Add(item);

                    }
                    item = new CboItemEntity();
                    item.Value = "99";
                    item.Text = "99";
                    repositoryItemComboBox6.Items.Add(item);
                    gridView1.BestFitColumns();

                }

                if (gridView1.GetFocusedDataRow()["ID"].ToString() != ""){
                    if(gridView1.GetFocusedDataRow()["GuanWeiNameBak"].ToString() != gridView1.GetFocusedDataRow()["GuanWeiName"].ToString()
                        ||gridView1.GetFocusedDataRow()["GuanweiSiteBak"].ToString() != gridView1.GetFocusedDataRow()["GuanweiSite" ].ToString())
                     {
                        m_tblDataList.Rows[gridView1.FocusedRowHandle]["pmark"] = "调整关位";
                        gridControl1.DataSource = m_tblDataList;
                    }
                    else {
                        m_tblDataList.Rows[gridView1.FocusedRowHandle]["pmark"] = "";
                        gridControl1.DataSource = m_tblDataList;
                    }
                }
                   
                
            }
            catch (Exception ex)
            {
                XtraMsgBox.Show("加载关位失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

         /// <summary>
         /// 选择关位位置信息
         /// </summary>
         void repositoryItemComboBox6_EditValueChanged(object sender, EventArgs e)
         {
             try
             {
                 gridView1.CloseEditor();
                 gridView1.UpdateCurrentRow();
                 m_tblDataList = ((DataView)this.gridView1.DataSource).ToTable();
                 DataRow row = gridView1.GetFocusedDataRow();
  
                m_tblDataList.Rows[gridView1.FocusedRowHandle]["GuanweiSite"] = row["GuanweiSite"].ToString(); 
                DataView view = new DataView(m_tblGuanwei.Copy());

                view.RowFilter = "SetNum='" + row["GuanweiSite"].ToString() + "'";
                DataTable dt = view.ToTable();
                if (dt.Rows.Count <= 0)
                {
                    m_tblDataList.Rows[gridView1.FocusedRowHandle]["GuanweiSite"] = "99"; 
                }

                if (gridView1.GetFocusedDataRow()["ID"].ToString() != "")
                {
                    if (gridView1.GetFocusedDataRow()["GuanWeiNameBak"].ToString() != gridView1.GetFocusedDataRow()["GuanWeiName"].ToString()
                        || gridView1.GetFocusedDataRow()["GuanweiSiteBak"].ToString() != gridView1.GetFocusedDataRow()["GuanweiSite"].ToString())
                    {
                        m_tblDataList.Rows[gridView1.FocusedRowHandle]["pmark"] = "调整关位";
                        gridControl1.DataSource = m_tblDataList;
                        
                    }
                    else
                    {
                        m_tblDataList.Rows[gridView1.FocusedRowHandle]["pmark"] = "";
                        gridControl1.DataSource = m_tblDataList;
                    }
                }
             }
             catch (Exception ex)
             {
                 XtraMsgBox.Show("加载关位失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
             }
         }
        

        /// <summary>
        /// 选择时间
        /// </summary>
        void repositoryItemDateEdit2_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string strID = gridView1.GetFocusedDataRow()["ID"].ToString();
                if (strID != "")
                {
                    XtraMsgBox.Show("调入日期不能修改！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                   gridView1.BestFitColumns();
                    return;
                }
                gridView1.CloseEditor();
                gridView1.UpdateCurrentRow();
                if (gridView1.FocusedRowHandle < 0) return;

                m_tblDataList = ((DataView)this.gridView1.DataSource).ToTable();
                DateEdit date = (DateEdit)sender;
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                m_tblDataList.Rows[gridView1.FocusedRowHandle]["StrDate"] = row["StrDate"].ToString();

                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMsgBox.Show("加载数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 考勤单位（只能选一个）
        /// </summary>
        void repositoryItemComboBox7_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                gridView1.CloseEditor();
                gridView1.UpdateCurrentRow();
                m_tblDataList = ((DataView)this.gridView1.DataSource).ToTable();
                DataRow row = gridView1.GetFocusedDataRow();

                m_tblDataList.Rows[gridView1.FocusedRowHandle]["AttendUnitName"] = row["AttendUnitName"].ToString();

                m_tblDataList = ((DataView)this.gridView1.DataSource).ToTable();
                
                if (row["AttendUnitName"].ToString()=="是"
                    && row["pmark"].ToString() != "调出")
                {
                    for (int i = 0; i < this.gridView1.RowCount; i++)
                    {
                        if (i == this.gridView1.FocusedRowHandle) continue;
                        this.gridView1.SetRowCellValue(i, Select2, "否");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        void repositoryItemComboBox2_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            if (e.Value != null)
            {
                e.Value = e.Value.ToString();
                e.Handled = true;
            }
        }

        

        private void btnAdd1_Click(object sender, EventArgs e)
        {
            try
            {
                gridView1.CloseEditor();
                gridView1.UpdateCurrentRow();

                if (gridView1.GetFocusedDataRow()!=null
                    && gridView1.GetFocusedDataRow()["ID"].ToString() != ""
                    && gridView1.GetFocusedDataRow()["pmark"].ToString()=="调出")
                {//取消删除
                    gridView1.GetFocusedDataRow()["pmark"] = "";
                    return;
                }
                if (m_tblDataList == null)
                {
                    m_tblDataList = new DataTable();
                }

                if (!m_tblDataList.Columns.Contains("AttendUnitName")) 
                {
                    m_tblDataList.Columns.Add("AttendUnitName", typeof(string));
                }
                if (!m_tblDataList.Columns.Contains("GuanweiSite"))
                {
                    m_tblDataList.Columns.Add("GuanweiSite", typeof(int));
                }
                if (!m_tblDataList.Columns.Contains("pmark"))
                {
                    m_tblDataList.Columns.Add("pmark", typeof(string));
                }
                DataRow dr = m_tblDataList.NewRow();
                
              
                for (int i = 0; i < m_tblDataList.Rows.Count; i++)
                {
                    if (m_tblDataList.Rows[i]["myTeamName"].ToString() == "" 
                        || m_tblDataList.Rows[i]["GuanweiName"].ToString() == "" 
                        || m_tblDataList.Rows[i]["GuanWeiSite"].ToString() == ""
                        || m_tblDataList.Rows[i]["StrDate"].ToString() == "")
                    {
                        XtraMsgBox.Show("请填写完整信息！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                dr["pmark"] = "调入";
                dr["AttendUnitName"] = "否";
                m_tblDataList.Rows.Add(dr);
                m_tblDataList.AcceptChanges();

                this.gridControl1.DataSource = m_tblDataList;
            }
            catch (Exception ex)
            {
                XtraMsgBox.Show("加载数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        private void btnDel1_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr= gridView1.GetFocusedDataRow();
                if (dr==null)
                {
                    return;
                }
                if (dr["ID"].ToString() != "")
                {
                    
                    m_tblDataList.Rows[gridView1.FocusedRowHandle]["pmark"] = "调出";
                    m_tblDataList.Rows[gridView1.FocusedRowHandle]["AttendUnitName"] = "否";
                    gridControl1.DataSource = m_tblDataList;
                   
                }
                else
                {
                    m_tblDataList.Rows[gridView1.FocusedRowHandle].Delete();
                    gridControl1.DataSource = m_tblDataList;
                }

                if (gridView1.DataSource != null)
                {
                    ((DataView)gridView1.DataSource).Table.AcceptChanges();
                    m_tblDataList.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                try
                {
                    txtID.Text = dr["Userid"].ToString();
                    txtName.Text = dr["UserName"].ToString();
                    txtpSex.Text = dr["Sex"].ToString();
                    txtDuty.Text = "";
                    string[] strStatus = dr["StatusIDs"].ToString().Split(',');

                    for (int j = 0; j < lookStatus.Properties.Items.Count; j++)
                    {
                        CheckedListBoxItem item = lookStatus.Properties.Items[j];
                        for (int i = 0; i < strStatus.Length; i++)
                        {
                            if (item.Value.ToString() == strStatus[i])
                            {
                                item.CheckState = CheckState.Checked;
                            }
                        }
                    }
                    if (this.ScanMode != Common.DataModifyMode.del)
                    {
                        string str_sql = string.Format(@" Select ''as pmark,GuanWeiName as GuanWeiNameBak,GuanweiSite as GuanweiSiteBak,* from V_Produce_User_Line   where UserID='{0}'", dr["Userid"].ToString());
                        m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

                        gridControl1.DataSource = m_tblDataList;
                        if (gridView1.DataSource != null)
                        {
                            ((DataView)gridView1.DataSource).Table.AcceptChanges();
                            m_tblDataList.AcceptChanges();
                        }
                    }
                    else 
                    {
                        btnAdd1.Visible = false;
                        gridControl1.Visible = false;
                        btnDel1.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 画面数据有效检查处理
        /// </summary>
        public  void GetInputChecks(ref bool isSucces)
        {
            isSucces = false;
            int count = 0;
            string strStatus = string.Empty;
            string strStatusName = string.Empty;

            DataTable tblList = ((DataView)this.gridView1.DataSource).ToTable();
            try
            {
                if (string.IsNullOrEmpty(this.txtID.Text.Trim() + ""))
                {
                    DataValid.ShowErrorInfo(this.ErrorInfo, this.txtID, "工号不能为空!");
                    return;
                } 
                
                for (int j = 0; j < lookStatus.Properties.Items.Count; j++)
                {
                    CheckedListBoxItem item = lookStatus.Properties.Items[j];
                    if (item.CheckState == CheckState.Checked)
                    {
                        strStatus += item.Value.ToString() + ",";
                        strStatusName += item.Description.ToString() + ",";
                    }
                }
                if (strStatus == "")
                {
                    DataValid.ShowErrorInfo(this.ErrorInfo, this.lookStatus, "状态不能为空!");
                    return;
                }

                for (int i = 0; i < tblList.Rows.Count; i++)
                {
                    if (tblList.Rows[i]["myteamName"].ToString() == "")
                    {

                        XtraMsgBox.Show("向别-班别不能为空!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else if (tblList.Rows[i]["GuanweiName"].ToString() == "")
                    {
                        XtraMsgBox.Show("关位不能为空!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else if (tblList.Rows[i]["GuanweiSite"].ToString() == "")
                    {
                        XtraMsgBox.Show("关位位置不能为空!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else if (tblList.Rows[i]["AttendUnitName"].ToString() == "是"
                        && tblList.Rows[i]["pmark"] != "调出") 
                    {
                        count++;
                    }
                }
                if (tblList.Rows.Count == 1 && tblList.Rows[0]["pmark"] == "调出")
                {
                    count=1;
                }
                if (count != 1)
                {
                    XtraMsgBox.Show("请选择一个考勤单位!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
               
                isSucces = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 获取下拉框数据
        /// </summary>
        public void GetComboBox()
        {
            DataTable dt_temp = new DataTable();
            CboItemEntity item = new CboItemEntity();
            //获取向别下拉
            string str_sql = string.Format(@"Select distinct myteamName from V_Produce_Para");
            m_tblJobForID = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
            for (int i = 0; i < m_tblJobForID.Rows.Count; i++)
            {
                item = new CboItemEntity();
                item.Value = m_tblJobForID.Rows[i]["myteamName"].ToString();
                item.Text = m_tblJobForID.Rows[i]["myteamName"].ToString();
                repositoryItemComboBox2.Items.Add(item);
            }
            
            

            //考勤单位
            item = new CboItemEntity();
            item.Value = "是";
            item.Text = "是";
            repositoryItemComboBox7.Items.Add(item);
            item = new CboItemEntity();
            item.Value = "否";
            item.Text = "否";
            repositoryItemComboBox7.Items.Add(item);
           
        }

        /// <summary>
        /// 保存人员数据时新增一条数据到Attend_Move表中记录
        /// </summary>
        /// <param name="JobForID">向别</param>
        /// <param name="ProjectID">工程别</param>
        /// <param name="LineID">Line别</param>
        /// <param name="TeamID">班别</param>
        /// <param name="GuanWeiID">关位</param>
        /// <param name="GuanWeiID">关位位置</param>
        /// <param name="MoveType">调动类型</param>
        /// <returns></returns>
        public int SetAttendMoveData(string ID, string JobForID, string ProjectID, 
            string LineID, string TeamID, string GuanWeiID, string GuanWeiSite,string StrDate, string MoveType) 
        {
            int result = 0;
            string str_sql = "";
            m_dicItemData.Clear();
            m_dicItemData["UserID"] = txtID.Text.Trim();
            m_dicItemData["JobForID"] = JobForID;
            m_dicItemData["ProjectID"] = ProjectID;
            m_dicItemData["LineID"] = LineID;
            m_dicItemData["TeamID"] = TeamID;
            m_dicItemData["GuanWeiID"] = GuanWeiID;
            m_dicItemData["GuanWeiSite"] = GuanWeiSite;
            m_dicItemData["StrDate"] = StrDate;
            m_dicItemData["EndDate"] = "4000-01-01";
            m_dicItemData["OperID"] = Common._personid;
            m_dicItemData["OperDate"] = DateTime.Now.ToString();

            if (MoveType == "调入")
            {
                //人员调入
                m_dicItemData["PFlag"] = pFlag.pflag1.GetHashCode().ToString();
                
            }
            else if (MoveType == "调出")
            {
                str_sql = string.Format(@"update Attend_Move set EndDate='{0}',  OperID='{1}', OperDate='{2}' where 1=1 
                                and ID='{3}' 
                                AND UserID='{4}'  
                                AND EndDate='4000-01-01'",
                              StrDate, Common._personid, DateTime.Now.ToString(), ID,
                              txtID.EditValue.ToString());
                result = SysParam.m_daoCommon.SetModifyDataItemBySql(str_sql, new StringDictionary(), new StringDictionary(), new StringDictionary());
                //人员调出
                m_dicItemData["PFlag"] = pFlag.pflag2.GetHashCode().ToString();
                m_dicItemData["EndDate"] = "1900-01-01";
            }
            else if (MoveType == "调整关位")
            {
                str_sql = string.Format(@"update Attend_Move set EndDate='{0}' , OperID='{1}', OperDate='{2}' where 1=1 
                                and ID='{3}' 
                                AND UserID='{4}'  
                                AND EndDate='4000-01-01'",
                              StrDate, Common._personid, DateTime.Now.ToString(), ID,
                              txtID.EditValue.ToString());
                result = SysParam.m_daoCommon.SetModifyDataItemBySql(str_sql, new StringDictionary(), new StringDictionary(), new StringDictionary());
                //关位调整
                m_dicItemData["PFlag"] = pFlag.pflag3.GetHashCode().ToString();
            }

            result = SysParam.m_daoCommon.SetInsertDataItem(TableNames.Attend_Move, m_dicItemData);
            return result;
        }
        #endregion   



        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0) { return; }
                string strmyteamName = m_tblDataList.Rows[gridView1.FocusedRowHandle]["myTeamName"].ToString();

                string str_sql = string.Format(@"Select GuanweiID, GuanweiName,SetNum From V_Produce_Para where myTeamName='{0}' Order by GuanweiID", strmyteamName);
                m_tblGuanwei = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                repositoryItemComboBox3.Items.Clear();
                //关位
                for (int i = 0; i < m_tblGuanwei.Rows.Count; i++)
                {
                    CboItemEntity item = new CboItemEntity();
                    //item.Value = i;
                    item.Value = m_tblGuanwei.Rows[i]["GuanweiID"].ToString();
                    item.Text = m_tblGuanwei.Rows[i]["GuanweiName"].ToString();
                    repositoryItemComboBox3.Items.Add(item);

                }


                //位置
                string GuanweiName = m_tblDataList.Rows[gridView1.FocusedRowHandle]["GuanweiName"].ToString();
                DataView view = new DataView(m_tblGuanwei.Copy());
                view.RowFilter = "GuanweiName='" + GuanweiName + "'";
                DataTable dt = view.ToTable();
                if (dt.Rows.Count > 0)
                {
                    //位置
                    int guanweiSite = int.Parse(dt.Rows[0]["SetNum"].ToString());
                    CboItemEntity item = new CboItemEntity();
                    repositoryItemComboBox6.Items.Clear();
                    for (int i = 0; i < guanweiSite; i++)
                    {
                        item = new CboItemEntity();
                        item.Value = i + 1;
                        item.Text = i + 1;
                        repositoryItemComboBox6.Items.Add(item);

                    }
                    item = new CboItemEntity();
                    item.Value = "99";
                    item.Text = "99";
                    repositoryItemComboBox6.Items.Add(item);
                    gridView1.BestFitColumns();
                }
            }

         
    }
}
