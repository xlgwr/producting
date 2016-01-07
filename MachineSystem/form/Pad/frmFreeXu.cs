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
using Framework.Libs;
using log4net;
using MachineSystem.SettingTable;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using MachineSystem.form.Search;

namespace MachineSystem.form.Pad
{
    /********************************************************************************	
    ** 作者： libing   	
    ** 创始时间：2015-07-14	
    ** 修改人：libing	
    ** 修改时间：
    ** 修改内容：
    ** 描述：免许登记
    *********************************************************************************/
    public partial class frmFreeXu : Framework.Abstract.frmBaseXC
    {
        #region 变量定义
        /// <summary>
        /// 人员数据表
        /// </summary>
        public DataTable m_tblDataList = new DataTable();

        /// <summary>
        /// 向别
        /// </summary>
        DataTable m_tblJobForID = new DataTable();

        /// <summary>
        /// 工程别
        /// </summary>
        DataTable m_tblProjectID = new DataTable();

        /// <summary>
        /// Line别
        /// </summary>
        DataTable m_tblLineID = new DataTable();

        /// <summary>
        /// 关位
        /// </summary>
        DataTable m_tblGuanwei = new DataTable();

        /// <summary>
        /// 选中时间
        /// </summary>
        public DateTime m_DataTime;

        // 日志	
        private static readonly ILog log = LogManager.GetLogger(typeof(frmLessFrequently));

        /// <summary>
        /// 是否是界面初始化
        /// </summary>
        bool isLoad = true;

        #endregion

        #region 画面初始化
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmFreeXu()
        {
            InitializeComponent();

            SetFormValue();

            this.TopMost = true;
        }
        protected override CreateParams CreateParams
        {
            get
            {

                CreateParams cp = base.CreateParams;

                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED  
                this.Opacity = 1;

                //if (this.IsXpOr2003 == true)
                //{
                //    cp.ExStyle |= 0x00080000;  // Turn on WS_EX_LAYERED
                //    this.Opacity = 1;
                //}

                return cp;

            }

        }  //防止闪烁
        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                //免许类型
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_License_Type, this.lookUpEditLicenseType, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefalutItemAllText);

                //免许资格
                string str_sql = string.Format("SELECT ID,pMark FROM P_License_Entitle ");
                DataTable dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

                foreach (DataRow dr in dt.Rows) 
                {
                    this.lookQualification.Properties.Items.Add(dr["ID"].ToString(), dr["pMark"].ToString());
                }
                repositoryItemComboBox4.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(repositoryItemComboBox4_ParseEditValue);
                repositoryItemComboBox4.EditValueChanged += new EventHandler(repositoryItemComboBox4_EditValueChanged);

                repositoryItemComboBox5.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(repositoryItemComboBox4_ParseEditValue);
                repositoryItemComboBox6.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(repositoryItemComboBox4_ParseEditValue);
                repositoryItemComboBox7.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(repositoryItemComboBox4_ParseEditValue);
                repositoryItemComboBox8.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(repositoryItemComboBox4_ParseEditValue);
                GetDspDataList();
                dtStartTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("画面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void frmFreeXu_Load(object sender, EventArgs e)
        {
            try
            {
                string str_sql = string.Format(@"select * from V_User_TotalShow_Image where 1=1 and AttendDate='" + m_DataTime.ToString("yyyy-MM-dd") + "' ");
                string str_or = string.Empty;

                for (int i = 0; i < m_tblDataList.Rows.Count; i++)
                {
                    str_or += " or UserID='" + m_tblDataList.Rows[i]["UserID"].ToString() + "'";
                }
                str_or = str_or.Substring(3);
                str_sql = str_sql + "and (" + str_or + ")";
                DataTable dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                gridControl1.DataSource = dt;
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("画面加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
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
                if (lookUpEditLicenseType.EditValue.ToString() == "-1")
                {
                    XtraMsgBox.Show("请选择免许类型！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return;
                }
                if (lookQualification.EditValue.ToString() == "-1")
                {
                    XtraMsgBox.Show("请选择免许资格！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return;
                }

                Common.AdoConnect.Connect.CreateSqlTransaction();
                DataTable dt_Data = ((DataView)this.gridView1.DataSource).Table.Copy();
                DataTable dt_temp = ((DataView)this.gridView2.DataSource).Table.Copy();
                string str_sql = string.Empty;
                int result = 0;
                int count = 0;
                DataTable dt = new DataTable();

                //循环保存人员信息
                for (int i = 0; i < dt_Data.Rows.Count; i++)
                {
                    //查询人员是否在免许主表中存在，如存在则返回
                    dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(" select * from License_Rec_i where UserID='" + dt_Data.Rows[i]["UserID"].ToString() + "'");
                    if (dt.Rows.Count > 0)
                    {
                        Common.AdoConnect.Connect.TransactionRollback();
                        XtraMsgBox.Show(dt_Data.Rows[i]["UserID"].ToString() + "：已存在免许记录！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    m_dicItemData.Clear();
                    string ID = SysParam.m_daoCommon.GetMaxNoteNo("License_Rec_i", "ID");
                    m_dicItemData["ID"] = int.Parse(ID).ToString();

                    m_dicItemData["UserID"] = dt_Data.Rows[i]["UserID"].ToString();
                    m_dicItemData["LicenseType"] = dt_Data.Rows[i]["LicenseType"].ToString();
                    m_dicItemData["ValidDate"] = dtStartTime.Text.Trim();
                    m_dicItemData["OperID"] = Common._personid;
                    m_dicItemData["OperDate"] = DateTime.Now.ToString();

                    //保存到免许登记主表
                    result = SysParam.m_daoCommon.SetInsertDataItem("License_Rec_i", m_dicItemData);

                    //保存用户免许资格
                    for (int j = 0; j < lookQualification.Properties.Items.Count; j++)
                    {
                        CheckedListBoxItem item = lookQualification.Properties.Items[j];
                        if (item.CheckState == CheckState.Checked)
                        {
                            m_dicItemData.Clear();
                            m_dicItemData["RecID"] = int.Parse(ID).ToString();
                            m_dicItemData["EntitleID"] = item.Value.ToString();
                            //保存用户免许资格
                            result = SysParam.m_daoCommon.SetInsertDataItem("P_License_Rec_Entitle", m_dicItemData);
                        }
                    }

                    //循环保存免许信息
                    for (int k = 0; k < dt_temp.Rows.Count; k++)
                    {

                        m_dicItemData.Clear();
                        m_dicItemData["ID1"] = int.Parse(ID).ToString();

                        //获取向别ID
                        string strJobFor = dt_temp.Rows[k]["JobForID"].ToString();
                        DataView view = new DataView(m_tblJobForID.Copy());
                        view.RowFilter = "JobForName='" + strJobFor + "'";
                        dt = view.ToTable();
                        m_dicItemData["JobForID"] = dt.Rows[0]["JobForID"].ToString();

                        //获取工程别ID
                        string strProject = dt_temp.Rows[k]["ProjectID"].ToString();
                        view = new DataView(m_tblProjectID.Copy());
                        view.RowFilter = "ProjectName='" + strProject + "'";
                        dt = view.ToTable();
                        m_dicItemData["ProjectID"] = dt.Rows[0]["ProjectID"].ToString();

                        //获取Line别ID
                        m_dicItemData["LineID"] = dt_temp.Rows[k]["LineID"].ToString();

                        //获取Line别ID
                        string strGuanwei = dt_temp.Rows[k]["GuanweiID"].ToString();
                        view = new DataView(m_tblGuanwei.Copy());
                        view.RowFilter = "GuanweiName='" + strGuanwei + "'";
                        dt = view.ToTable();
                        m_dicItemData["GuanweiID"] = "0";//dt.Rows[0]["GuanweiID"].ToString();

                        m_dicItemData["level"] = dt_temp.Rows[k]["pName"].ToString();
                        m_dicItemData["OperID"] = Common._personid;
                        m_dicItemData["OperDate"] = DateTime.Now.ToString();
                        //保存到免许登记明细表
                        result = SysParam.m_daoCommon.SetInsertDataItem("P_License_Detail", m_dicItemData);
                    }
                }
                Common.AdoConnect.Connect.TransactionCommit();
                XtraMsgBox.Show("保存数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Common.AdoConnect.Connect.TransactionRollback();
                XtraMsgBox.Show("保存数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
                log.Error(ex);
            }
        }
        #endregion

        #region 事件处理方法

        /// <summary>
        /// 表格改变事件
        /// </summary>
        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            return;
            gridView2.CloseEditor();
            gridView2.UpdateCurrentRow();
            if (gridView2.FocusedRowHandle < 0) return;
            DataView view ;
            DataTable dt_temp = new DataTable();
            string str_where = string.Empty;

            switch (e.Column.FieldName)
            {
                case "JobForID":
                    str_where = " where 1=1 ";
                    string strJobFor = gridView2.GetFocusedRowCellValue(e.Column.FieldName).ToString();
                     view = new DataView(m_tblJobForID.Copy());
                    view.RowFilter = "JobForName='" + strJobFor + "'";
                     dt_temp=view.ToTable();
                    if (dt_temp.Rows.Count == 1)
                    {
                        str_where += " and JobForID='" + dt_temp.Rows[0]["JobForID"].ToString() + "'";
                    }

                    //工程别
                    string str_sql = string.Format(@"select DISTINCT ProjectID,ProjectName FROM v_Produce_Para_i " + str_where + " order by ProjectName");
                    m_tblProjectID = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                    for (int i = 0; i < m_tblProjectID.Rows.Count; i++)
                    {
                        CboItemEntity item = new CboItemEntity();
                        item.Value = m_tblProjectID.Rows[i]["ProjectID"].ToString();
                        item.Text = m_tblProjectID.Rows[i]["ProjectName"].ToString();
                        repositoryItemComboBox5.Items.Add(item);
                    }
                    break;
                case "ProjectID":
                    string strProject = gridView2.GetFocusedRowCellValue(e.Column.FieldName).ToString();
                    view = new DataView(m_tblProjectID.Copy());
                     view.RowFilter = "ProjectName='" + strProject + "'";
                     dt_temp=view.ToTable();
                    if (dt_temp.Rows.Count == 1)
                    {
                        str_where = " where ProjectID='" + dt_temp.Rows[0]["ProjectID"].ToString() + "'";
                    }
                    str_sql = string.Format(@"select DISTINCT LineID,LineName FROM v_Produce_Para_i " + str_where + " order by LineName");
                    m_tblLineID= SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

                    //Line别
                    for (int i = 0; i < m_tblLineID.Rows.Count; i++)
                    {
                        CboItemEntity item = new CboItemEntity();
                        item.Value = m_tblLineID.Rows[i]["LineID"].ToString();
                        item.Text = m_tblLineID.Rows[i]["LineName"].ToString();
                        repositoryItemComboBox6.Items.Add(item);
                    }
                    break;
                case"":
                    break;

            }
        }
        void repositoryItemComboBox4_EditValueChanged(object sender, EventArgs e)
        {
            //gridView2.CloseEditor();
            //gridView2.UpdateCurrentRow();

            //if (gridView2.FocusedRowHandle < 0) return;
            //string str_where = " where 1=1 ";
            //string strJobForID = m_tblJobForID.Rows[gridView2.FocusedRowHandle]["JobForID"].ToString();
            //if (strJobForID != "")
            //{
            //    str_where += " and JobForID='" + strJobForID + "'";
            //}

            ////工程别
            //string str_sql = string.Format(@"select DISTINCT ProjectID,ProjectName FROM v_Produce_Para_i " + str_where + " order by ProjectName");
            //m_tblProjectID = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
            //for (int i = 0; i < m_tblProjectID.Rows.Count; i++)
            //{
            //    CboItemEntity item = new CboItemEntity();
            //    item.Value = m_tblProjectID.Rows[i]["ProjectID"].ToString();
            //    item.Text = m_tblProjectID.Rows[i]["ProjectName"].ToString();
            //    repositoryItemComboBox5.Items.Add(item);
            //}

        }

        /// <summary>
        /// 新增免许
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = null;
                DataRow drow;
                if (this.gridView2.DataSource != null)
                {
                    //如果表数据不为空的话把表结构给datatable
                    dt = ((DataView)this.gridView2.DataSource).Table;
                }
                else 
                {
                    dt = new DataTable();
                    dt.Columns.Add("SlctValue", typeof(Boolean));
                    dt.Columns.Add("JobForID");
                    dt.Columns.Add("ProjectID");
                    dt.Columns.Add("LineID");
                    dt.Columns.Add("GuanweiID");
                    dt.Columns.Add("pName");
                }

                DataRow dr = dt.NewRow();
                dr["SlctValue"] = "True";
                for (int i = 0; i < dt.Rows.Count; i++) 
                {
                    if (dt.Rows[i]["JobForID"].ToString() == "" || dt.Rows[i]["ProjectID"].ToString() == "" || dt.Rows[i]["LineID"].ToString() == "" || dt.Rows[i]["GuanweiID"].ToString() == "")
                    {
                        XtraMsgBox.Show("请填写完整信息！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                dt.Rows.Add(dr);
                dt.AcceptChanges();

                this.gridControl2.DataSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                XtraMsgBox.Show("加载数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 删除免许
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = gridView2.RowCount - 1; i >= 0; i--)
                {
                    if (Convert.ToBoolean(gridView2.GetRowCellValue(i, "SlctValue")) == true)
                    {
                        gridView2.DeleteRow(i);
                    }
                }
                if (gridView2.DataSource != null)
                {
                    ((DataView)gridView2.DataSource).Table.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 账号状态下拉框处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void repositoryItemComboBox4_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            if (e.Value != null)
            {
                e.Value = e.Value.ToString();
                e.Handled = true;
            }
        }


        #endregion

        #region 共同方法

        /// <summary>
        /// 获取表格信息一览
        /// </summary>
        protected override void GetDspDataList()
        {
            //获取向别下拉
            string str_sql = string.Format(@"Select id as JobForID,pName as JoBForName From P_Produce_JobFor Order by id");
            m_tblJobForID = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
            for (int i = 0; i < m_tblJobForID.Rows.Count; i++)
            {
                CboItemEntity item = new CboItemEntity();
                item.Value = m_tblJobForID.Rows[i]["JobForID"].ToString();
                item.Text = m_tblJobForID.Rows[i]["JoBForName"].ToString();
                repositoryItemComboBox4.Items.Add(item);
            }
            if (Common._myTeamName != "")
            {
                repositoryItemComboBox4.NullText = Common._myTeamName;
            }

            //工程别
            str_sql = string.Format(@"select DISTINCT ProjectID,ProjectName FROM v_Produce_Para_i  order by ProjectName");
            m_tblProjectID = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
            for (int i = 0; i < m_tblProjectID.Rows.Count; i++)
            {
                CboItemEntity item = new CboItemEntity();
                item.Value = m_tblProjectID.Rows[i]["ProjectID"].ToString();
                item.Text = m_tblProjectID.Rows[i]["ProjectName"].ToString();
                repositoryItemComboBox5.Items.Add(item);
            }

            str_sql = string.Format(@"select DISTINCT LineID,LineName FROM v_Produce_Para_i  order by LineName");
            m_tblLineID = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //Line别
            for (int i = 0; i < m_tblLineID.Rows.Count; i++)
            {
                CboItemEntity item = new CboItemEntity();
                item.Value = m_tblLineID.Rows[i]["LineID"].ToString();
                item.Text = m_tblLineID.Rows[i]["LineName"].ToString();
                repositoryItemComboBox6.Items.Add(item);
            }

            str_sql = string.Format(@"select Distinct T.GuanweiName from (
                                                    (select p.*,g.pName as GuanweiName from Produce_Guanwei p inner 
                                                        join P_Produce_Guanwei g on p.GuanweiID=g.ID) )T Order by GuanweiName");
            m_tblGuanwei = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //关位
            for (int i = 0; i < m_tblGuanwei.Rows.Count; i++)
            {
                CboItemEntity item = new CboItemEntity();
                item.Value = i;
                item.Text = m_tblGuanwei.Rows[i]["GuanweiName"].ToString();
                repositoryItemComboBox7.Items.Add(item);
            }

            str_sql = string.Format(@"select * from P_License_S");
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //等级
            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                CboItemEntity item = new CboItemEntity();
                item.Value = dt_temp.Rows[i]["ID"].ToString();
                item.Text = dt_temp.Rows[i]["pName"].ToString();
                repositoryItemComboBox8.Items.Add(item);
            }
        }

        #endregion   

        private void dtStartTime_Click(object sender, EventArgs e)
        {
            FrmSetDateTime frm = new FrmSetDateTime();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                dtStartTime.Text = DateTime.Parse(frm.m_DateTime).ToString("yyyy-MM-dd");
            }
        }
    }
}
