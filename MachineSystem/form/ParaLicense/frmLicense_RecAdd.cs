using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Framework.Abstract;
using Framework.Libs;
using MachineSystem.SysDefine;
using MachineSystem.SettingTable;
using log4net;
using MachineSystem.TabPage;
using DevExpress.XtraEditors.Controls;
using MachineSystem.SysCommon;

namespace MachineSystem.form.ParaLicense
{
    public partial class frmLicense_RecAdd : Framework.Abstract.frmBaseToolEntryXC
    {
        #region 变量定义
        public DataRow dr;
        private DataTable m_ViewDetail;//免许明细
        DataTable m_tblDataList = new DataTable();
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
        DataTable m_tblGuanweiID = new DataTable();

        /// <summary>
        /// 等级
        /// </summary>
        DataTable m_tblSID = new DataTable();

        /// <summary>
        /// 有效时间
        /// </summary>
        public DateTime m_DataTime;

        /// <summary>
        /// 是否是界面初始化
        /// </summary>
        bool isLoad = true;
        private string m_ParenSlctColName = "";

        /// <summary>
        /// 平板端查看
        /// </summary>
        public string m_isPad = string.Empty;

        // 日志	
        private readonly ILog log;
        #endregion

        #region 画面初始化

        public frmLicense_RecAdd()
        {
            InitializeComponent();
            log = LogManager.GetLogger("frmLicense_RecAdd");
            //SetFormValue();
            //SetCreateViewDetailTable();
            this.m_GridViewUtil.GridControlList = this.gridControl1;
            this.m_GridViewUtil.ParentGridView = this.gridView1;

            //初始化免许明细
            m_ViewDetail = new DataTable();
            //m_ViewDetail.Columns.Add("SlctValue", typeof(System.Boolean));
            m_ViewDetail.Columns.Add("id", typeof(System.Int32));
            m_ViewDetail.Columns.Add("JobForID", typeof(System.Int32));
            m_ViewDetail.Columns.Add("JobForName", typeof(System.String));
            m_ViewDetail.Columns.Add("ProjectID", typeof(System.Int32));
            m_ViewDetail.Columns.Add("ProjectName", typeof(System.String));
            m_ViewDetail.Columns.Add("LineID", typeof(System.Int32));
            m_ViewDetail.Columns.Add("LineName", typeof(System.String));
            m_ViewDetail.Columns.Add("guanweiID", typeof(System.Int32));
            m_ViewDetail.Columns.Add("guanweiName", typeof(System.String));
            m_ViewDetail.Columns.Add("position", typeof(System.Int32));
            m_ViewDetail.Columns.Add("levelID", typeof(System.Int32));
            m_ViewDetail.Columns.Add("levelName", typeof(System.String));
            m_ViewDetail.Columns.Add("OperID", typeof(System.String));
            m_ViewDetail.Columns.Add("OperDate", typeof(System.String));
            m_ViewDetail.Columns.Add("RowStatus", typeof(System.String));//0新增，1修改

            txtGuanwei.Focus();
            log.Info("构造函数");

            this.TopMost = true;

        }


        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                log.Info("免许类型");
                //免许类型
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_License_Type, this.lookUpEditLicenseType, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefalutItemAllText);

                //免许资格
                string str_sql = string.Format("SELECT ID,pMark FROM P_License_Entitle ");
                DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

                foreach (DataRow dr in dt_temp.Rows)
                {
                    this.lookEntitle.Properties.Items.Add(dr["ID"].ToString(), dr["pMark"].ToString());
                }

                log.Info("有效时间");
                //有效时间
                dateEditDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
                //取得页面免许列表下拉框数据
                GetDspDataList();

                log.Info("取得页面免许列表下拉框数据");
                if (this.ScanMode == Common.DataModifyMode.add)
                {
                    this.Text = "新增免许记录";
                    this.txtUserID.Enabled = true;
                }
                else if (this.ScanMode == Common.DataModifyMode.upd)
                {
                    if (dr != null)
                    {
                        //取得更新数据
                        GetDataRowValue(dr);
                        this.Text = "修改免许记录";
                        this.txtUserID.Enabled = false;

                        log.Info("修改免许记录");
                    }
                }
                SaveButtonEnabled = true;
                ExcelButtonEnabled = true;

                if (m_isPad != "")
                {
                    gridView1.OptionsBehavior.Editable = false;
                    btnAdd.Visible = false;
                    btnDel.Visible = false;
                }

                //gridView 中dataItem的数据
                repositoryItemComboBox1.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(repositoryItemComboBox1_ParseEditValue);
                repositoryItemComboBox1.SelectedIndexChanged += new EventHandler(repositoryItemComboBox1_SelectedIndexChanged);

                repositoryItemComboBox2.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(repositoryItemComboBox2_ParseEditValue);
                repositoryItemComboBox2.SelectedIndexChanged += new EventHandler(repositoryItemComboBox2_SelectedIndexChanged);

                repositoryItemComboBox3.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(repositoryItemComboBox3_ParseEditValue);
                repositoryItemComboBox3.SelectedIndexChanged += new EventHandler(repositoryItemComboBox3_SelectedIndexChanged);

                repositoryItemComboBox4.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(repositoryItemComboBox4_ParseEditValue);
                repositoryItemComboBox4.SelectedIndexChanged += new EventHandler(repositoryItemComboBox4_SelectedIndexChanged);

                repositoryItemComboBox5.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(repositoryItemComboBox5_ParseEditValue);
                repositoryItemComboBox5.SelectedIndexChanged += new EventHandler(repositoryItemComboBox5_SelectedIndexChanged);


                txtGuanwei.Focus();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("画面初始化失败！" + ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
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

        /// <summary>
        /// 新增
        /// </summary>

        protected override void SetInsertProc(ref int RtnValue)
        {
            base.SetInsertProc(ref RtnValue);
            try
            {
                int result = 0;
                Common.AdoConnect.Connect.CreateSqlTransaction();

                m_dicItemData.Clear();
                //***********免许表数据插入*********
                m_dicItemData["UserID"] = txtUserID.Text.ToString();//用户id
                m_dicItemData["LicenseType"] = lookUpEditLicenseType.EditValue.ToString();//免许类型
                m_dicItemData["ValidDate"] = dateEditDate.EditValue.ToString();//有效日期
                m_dicItemData["OperID"] = Common._personid;//
                m_dicItemData["OperDate"] = DateTime.Now.ToString();//
                result = SysParam.m_daoCommon.SetInsertDataItem("License_Rec_i", m_dicItemData);
                //取得新插入免许的id
                string str_sql = "select ID FROM License_Rec_i WHERE UserID='" + txtUserID.Text.ToString() + "' order by OperDate desc";
                DataTable dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                string str_id = dt.Rows[0]["ID"].ToString();

                //***********免许资格数据插入*********
                List<string> Entitle_ls = new List<string>();

                //保存用户免许资格
                for (int j = 0; j < lookEntitle.Properties.Items.Count; j++)
                {
                    CheckedListBoxItem item = lookEntitle.Properties.Items[j];
                    if (item.CheckState == CheckState.Checked)
                    {
                        Entitle_ls.Add(item.Value.ToString());
                    }
                }
                m_dicItemData.Clear();
                for (int j = 0; j < Entitle_ls.Count; j++)//
                {
                    m_dicItemData["RecID"] = str_id;
                    m_dicItemData["EntitleID"] = Entitle_ls[j];
                    result = SysParam.m_daoCommon.SetInsertDataItem("P_License_Rec_Entitle", m_dicItemData);

                }

                //***********免许明细数据插入*********
                DataTable dt_temp = ((DataView)gridView1.DataSource).ToTable();
                for (int i = 0; i < dt_temp.Rows.Count; i++)
                {
                    m_dicItemData.Clear();
                    m_dicItemData["ID"] = str_id;
                    DataView view = new DataView(m_tblJobForID.Copy());
                    view.RowFilter = "JobForName='" + dt_temp.Rows[i]["JobForName"].ToString() + "'";
                    m_dicItemData["JobForID"] = view.ToTable().Rows[0]["JobForID"].ToString();//向别
                    view = new DataView(m_tblProjectID.Copy());
                    view.RowFilter = "ProjectName='" + dt_temp.Rows[i]["ProjectName"].ToString() + "'";
                    m_dicItemData["ProjectID"] = view.ToTable().Rows[0]["ProjectID"].ToString();//工程别
                    view = new DataView(m_tblLineID.Copy());
                    view.RowFilter = "LineName='" + dt_temp.Rows[i]["LineName"].ToString() + "'";
                    m_dicItemData["LineID"] = view.ToTable().Rows[0]["LineID"].ToString();//Line别
                    view = new DataView(m_tblGuanweiID.Copy());
                    view.RowFilter = "guanweiName='" + dt_temp.Rows[i]["guanweiName"].ToString() + "'";
                    m_dicItemData["guanweiID"] = view.ToTable().Rows[0]["guanweiID"].ToString();//关位

                    m_dicItemData["position"] = dt_temp.Rows[i]["position"].ToString();//位置

                    view = new DataView(m_tblSID.Copy());
                    view.RowFilter = "pName='" + dt_temp.Rows[i]["levelName"].ToString() + "'";
                    m_dicItemData["level"] = view.ToTable().Rows[0]["ID"].ToString();//关位

                    //操作员
                    m_dicItemData["OperID"] = Common._personid;
                    //操时间
                    m_dicItemData["OperDate"] = DateTime.Now.ToString();
                    result = SysParam.m_daoCommon.SetInsertDataItem("P_License_Detail", m_dicItemData);
                }
                Common.AdoConnect.Connect.TransactionCommit();
                if (result > 0)
                {
                    XtraMsgBox.Show("新增数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //日志
                    SysParam.m_daoCommon.WriteLog("免许登记", "新增", "用户编号：" + txtUserID.Text.ToString());
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
                Common.AdoConnect.Connect.CreateSqlTransaction();
                m_dicItemData = new System.Collections.Specialized.StringDictionary();
                m_dicPrimarName.Clear();
                m_dicItemData.Clear();
                string str_id = txtID.Text.ToString();
                //***********免许表数据修改*********
                m_dicPrimarName["ID"] = str_id;

                m_dicItemData["UserID"] = txtUserID.Text.ToString();//用户id
                m_dicItemData["LicenseType"] = lookUpEditLicenseType.EditValue.ToString();//免许类型
                m_dicItemData["ValidDate"] = dateEditDate.EditValue.ToString();//有效日期
                m_dicItemData["OperID"] = Common._personid;//
                m_dicItemData["OperDate"] = DateTime.Now.ToString();//
                int result = SysParam.m_daoCommon.SetModifyDataIdentityColumn("License_Rec_i", m_dicItemData, m_dicPrimarName);

                m_dicPrimarName.Clear();
                m_dicItemData.Clear();
                m_dicPrimarName["ID"] = str_id;
                m_dicItemData["ID"] = str_id;

                //***********免许明细数据删除*********
                result = SysParam.m_daoCommon.SetDeleteDataItem("P_License_Detail", m_dicItemData, m_dicPrimarName);
                //***********免许资格数据删除*********
                m_dicPrimarName.Clear();
                m_dicItemData.Clear();
                m_dicPrimarName["RecID"] = str_id;
                m_dicItemData["RecID"] = str_id;
                result = SysParam.m_daoCommon.SetDeleteDataItem("P_License_Rec_Entitle", m_dicItemData, m_dicPrimarName);


                //***********免许资格数据插入*********
                List<string> Entitle_ls = new List<string>();
                //保存用户免许资格
                for (int j = 0; j < lookEntitle.Properties.Items.Count; j++)
                {
                    CheckedListBoxItem item = lookEntitle.Properties.Items[j];
                    if (item.CheckState == CheckState.Checked)
                    {
                        Entitle_ls.Add(item.Value.ToString());
                    }
                }

                m_dicPrimarName.Clear();
                m_dicItemData.Clear();
                for (int j = 0; j < Entitle_ls.Count; j++)//
                {
                    m_dicItemData["RecID"] = str_id;
                    m_dicItemData["EntitleID"] = Entitle_ls[j];
                    SysParam.m_daoCommon.SetInsertDataItem("P_License_Rec_Entitle", m_dicItemData);
                }


                //***********免许明细数据插入*********
                DataTable dt_temp = ((DataView)gridView1.DataSource).ToTable();
                for (int i = 0; i < dt_temp.Rows.Count; i++)
                {
                    m_dicPrimarName.Clear();
                    m_dicItemData.Clear();
                    m_dicPrimarName["ID"] = str_id;
                    m_dicItemData["ID"] = str_id;


                    DataView view = new DataView(m_tblJobForID.Copy());
                    view.RowFilter = "JobForName='" + dt_temp.Rows[i]["JobForName"].ToString() + "'";
                    m_dicItemData["JobForID"] = view.ToTable().Rows[0]["JobForID"].ToString();//向别
                    view = new DataView(m_tblProjectID.Copy());
                    view.RowFilter = "ProjectName='" + dt_temp.Rows[i]["ProjectName"].ToString() + "'";
                    m_dicItemData["ProjectID"] = view.ToTable().Rows[0]["ProjectID"].ToString();//工程别
                    view = new DataView(m_tblLineID.Copy());
                    view.RowFilter = "LineName='" + dt_temp.Rows[i]["LineName"].ToString() + "'";
                    m_dicItemData["LineID"] = view.ToTable().Rows[0]["LineID"].ToString();//Line别
                    view = new DataView(m_tblGuanweiID.Copy());
                    view.RowFilter = "guanweiName='" + dt_temp.Rows[i]["guanweiName"].ToString() + "'";
                    m_dicItemData["guanweiID"] = view.ToTable().Rows[0]["guanweiID"].ToString();//关位

                    m_dicItemData["position"] = dt_temp.Rows[i]["position"].ToString();//位置

                    view = new DataView(m_tblSID.Copy());
                    view.RowFilter = "pName='" + dt_temp.Rows[i]["levelName"].ToString() + "'";
                    m_dicItemData["level"] = view.ToTable().Rows[0]["ID"].ToString();//关位


                    //操作员
                    m_dicItemData["OperID"] = Common._personid;
                    //操时间
                    m_dicItemData["OperDate"] = DateTime.Now.ToString();
                    result = SysParam.m_daoCommon.SetInsertDataItem("P_License_Detail", m_dicItemData);
                }


                if (result > 0)
                {
                    Common.AdoConnect.Connect.TransactionCommit();
                    XtraMsgBox.Show("修改数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //日志
                    SysParam.m_daoCommon.WriteLog("免许登记", "修改", txtUserID.Text.ToString());
                    DialogResult = DialogResult.OK;
                }
                txtGuanwei.Focus();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                Common.AdoConnect.Connect.TransactionRollback();
                throw ex;
            }
        }

        /// <summary>
        /// 新增免许明细
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = m_ViewDetail.NewRow();
                string strEntitle = string.Empty;//免许资格P_License_Rec_Entitle

                //***********免许资格*********
                for (int j = 0; j < lookEntitle.Properties.Items.Count; j++)
                {
                    if (lookEntitle.Properties.Items[j].CheckState == CheckState.Checked)
                    {
                        strEntitle += lookEntitle.Properties.Items[j].Description + ",";
                    }
                }

                if (!m_ViewDetail.Columns.Contains("LicenseTypeName"))
                {
                    m_ViewDetail.Columns.Add("LicenseTypeName");
                    m_ViewDetail.Columns.Add("ValidDate");
                    m_ViewDetail.Columns.Add("EntitleName");
                }

                dr["LicenseTypeName"] = lookUpEditLicenseType.Text;//免许类型
                dr["ValidDate"] = dateEditDate.Text;//有效日期
                dr["EntitleName"] = strEntitle;//免许资格
                dr["RowStatus"] = "0";//0新增，1修改

                m_ViewDetail.Rows.Add(dr);
                this.gridControl1.DataSource = m_ViewDetail.DefaultView;
                txtGuanwei.Focus();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("加载数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        //删除明细行
        private void btnDel_Click(object sender, EventArgs e)
        {
            gridView1.DeleteRow(gridView1.FocusedRowHandle);
        }
        #endregion

        #region 事件处理方法

        void repositoryItemComboBox1_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            if (e.Value != null)
            {
                e.Value = e.Value.ToString();
                e.Handled = true;
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

        void repositoryItemComboBox3_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            if (e.Value != null)
            {
                e.Value = e.Value.ToString();
                e.Handled = true;
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

        void repositoryItemComboBox5_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            if (e.Value != null)
            {
                e.Value = e.Value.ToString();
                e.Handled = true;
            }

        }
        /// <summary>
        /// 改变向别事件
        /// </summary>
        void repositoryItemComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //gridView1.CloseEditor();
            //gridView1.UpdateCurrentRow();
            //if (gridView1.FocusedRowHandle < 0) return;

            //DataTable dt_temp = ((DataView)gridView1.DataSource).ToTable();
            //DataRow dr = gridView1.GetFocusedDataRow();
            //ComboBoxEdit cbx = (ComboBoxEdit)sender;
            //CboItemEntity item = (CboItemEntity)cbx.SelectedItem;
            //gridView1.SetFocusedRowCellValue("JobForID", item.Value);
            //dt_temp.Rows[gridView1.FocusedRowHandle]["JobForID"] = item.Value.ToString();
        }

        /// <summary>
        /// 改变工程别事件
        /// </summary>
        void repositoryItemComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //gridView1.CloseEditor();
            //gridView1.UpdateCurrentRow();
            //if (gridView1.FocusedRowHandle < 0) return;

            //DataTable dt_temp = ((DataView)gridView1.DataSource).ToTable();
            //DataRow dr = gridView1.GetFocusedDataRow();
            //ComboBoxEdit cbx = (ComboBoxEdit)sender;
            //CboItemEntity item = (CboItemEntity)cbx.SelectedItem;
            //gridView1.SetFocusedRowCellValue("ProjectID", item.Value);
            //dt_temp.Rows[gridView1.FocusedRowHandle]["ProjectID"] = item.Value.ToString();
        }


        /// <summary>
        /// 改变Line别事件
        /// </summary>
        void repositoryItemComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //gridView1.CloseEditor();
            //gridView1.UpdateCurrentRow();
            //if (gridView1.FocusedRowHandle < 0) return;

            //DataTable dt_temp = ((DataView)gridView1.DataSource).ToTable();
            //DataRow dr = gridView1.GetFocusedDataRow();
            //ComboBoxEdit cbx = (ComboBoxEdit)sender;
            //CboItemEntity item = (CboItemEntity)cbx.SelectedItem;
            //gridView1.SetFocusedRowCellValue("LineID", item.Value);
            //dt_temp.Rows[gridView1.FocusedRowHandle]["LineID"] = item.Value.ToString();
        }

        /// <summary>
        /// 改变关位事件
        /// </summary>
        void repositoryItemComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //gridView1.CloseEditor();
            //gridView1.UpdateCurrentRow();
            //if (gridView1.FocusedRowHandle < 0) return;
            //DataTable dt_temp = ((DataView)gridView1.DataSource).ToTable();
            //DataRow dr = gridView1.GetFocusedDataRow();
            //ComboBoxEdit cbx = (ComboBoxEdit)sender;
            //CboItemEntity item = (CboItemEntity)cbx.SelectedItem;
            //gridView1.SetFocusedRowCellValue("guanweiID", item.Value);
            //dt_temp.Rows[gridView1.FocusedRowHandle]["guanweiID"] = item.Value.ToString();
        }

        /// <summary>
        /// 改变级别事件
        /// </summary>
        void repositoryItemComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            //gridView1.CloseEditor();
            //gridView1.UpdateCurrentRow();
            //if (gridView1.FocusedRowHandle < 0) return;

            //DataTable dt_temp = ((DataView)gridView1.DataSource).ToTable();
            //DataRow dr = gridView1.GetFocusedDataRow();
            //ComboBoxEdit cbx = (ComboBoxEdit)sender;
            //CboItemEntity item = (CboItemEntity)cbx.SelectedItem;
            //gridView1.SetFocusedRowCellValue("levelID", item.Value);
            //dt_temp.Rows[gridView1.FocusedRowHandle]["levelID"] = item.Value.ToString();
        }


        //根据用户id，查询用户其他信息
        private void txtUserID_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmUserInfoSearch _frm = new frmUserInfoSearch();
            if (_frm.ShowDialog() == DialogResult.OK)
            {
                this.txtUserID.EditValue = _frm.SelectRowData["UserID"].ToString();
                this.txtUserName.EditValue = _frm.SelectRowData["UserName"].ToString();
                this.txtmyTeamName.EditValue = _frm.SelectRowData["myTeamName"].ToString();
                this.txtGuanwei.EditValue = _frm.SelectRowData["GuanweiName"].ToString();

                //取得用户头像
                //GetUserImage();
                this.pictureEdit1.Image = Image.FromFile(GridCommon.GetUserImageOld(SysParam.m_daoCommon, _frm.SelectRowData["UserID"].ToString()));
            }
        }



        #endregion

        #region 共同方法
        /// <summary>
        /// 获取表格信息一览下拉框初始数据
        /// </summary>
        protected override void GetDspDataList()
        {
            try
            {
                //获取向别下拉
                string str_sql = string.Format(@"Select id as JobForID,pName as JoBForName From P_Produce_JobFor Order by id");
                m_tblJobForID = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                repositoryItemComboBox1.Items.Clear();
                CboItemEntity item;
                for (int i = 0; i < m_tblJobForID.Rows.Count; i++)
                {
                    item = new CboItemEntity();
                    item.Value = m_tblJobForID.Rows[i]["JobForID"].ToString();
                    item.Text = m_tblJobForID.Rows[i]["JobForName"].ToString();
                    repositoryItemComboBox1.Items.Add(item);
                }


                //工程别
                str_sql = string.Format(@"select DISTINCT ProjectID,ProjectName FROM v_Produce_Para  order by ProjectName");
                m_tblProjectID = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                for (int i = 0; i < m_tblProjectID.Rows.Count; i++)
                {
                    item = new CboItemEntity();
                    item.Value = m_tblProjectID.Rows[i]["ProjectID"].ToString();
                    item.Text = m_tblProjectID.Rows[i]["ProjectName"].ToString();
                    repositoryItemComboBox2.Items.Add(item);
                }

                //Line别
                str_sql = string.Format(@"select DISTINCT LineID,LineName FROM V_Produce_Para  order by LineName");
                m_tblLineID = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                for (int i = 0; i < m_tblLineID.Rows.Count; i++)
                {
                    item = new CboItemEntity();
                    item.Value = m_tblLineID.Rows[i]["LineID"].ToString();
                    item.Text = m_tblLineID.Rows[i]["LineName"].ToString();
                    repositoryItemComboBox3.Items.Add(item);
                }

                //关位
                str_sql = string.Format(@"select DISTINCT GuanweiID,GuanweiName FROM V_Produce_Para  order by GuanweiName");
                m_tblGuanweiID = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                for (int i = 0; i < m_tblGuanweiID.Rows.Count; i++)
                {
                    item = new CboItemEntity();
                    item.Value = m_tblGuanweiID.Rows[i]["GuanweiID"].ToString();
                    item.Text = m_tblGuanweiID.Rows[i]["GuanweiName"].ToString();
                    repositoryItemComboBox4.Items.Add(item);
                }

                //等级
                str_sql = string.Format(@"select * from P_License_S");
                m_tblSID = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                for (int i = 0; i < m_tblSID.Rows.Count; i++)
                {
                    item = new CboItemEntity();
                    item.Value = m_tblSID.Rows[i]["ID"].ToString();
                    item.Text = m_tblSID.Rows[i]["pName"].ToString();
                    repositoryItemComboBox5.Items.Add(item);
                }
                txtGuanwei.Focus();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("下拉框初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }


        /// <summary>
        /// 画面数据有效检查处理
        /// </summary> 
        protected override void GetInputCheck(ref bool isSucces)
        {
            isSucces = false;
            base.GetInputCheck(ref isSucces);
            if (string.IsNullOrEmpty(txtUserID.Text.ToString()))
            {
                DataValid.ShowErrorInfo(this.ErrorInfo, this.txtUserID, "请输入免许用户!");
                return;
            }
            if (dateEditDate.EditValue == null || string.IsNullOrEmpty(dateEditDate.EditValue.ToString()))
            {
                DataValid.ShowErrorInfo(this.ErrorInfo, this.dateEditDate, "请选择免许有效日期!");
                return;
            }
            if (lookUpEditLicenseType.Text == "")
            {
                DataValid.ShowErrorInfo(this.ErrorInfo, this.lookUpEditLicenseType, "请选择免许类型!");
            }

            //免许资格检查
            if (lookEntitle.Text == "")
            {
                DataValid.ShowErrorInfo(this.ErrorInfo, this.lookEntitle, "请选择免许资格!");
            }
            if (gridView1.RowCount <= 0)
            {
                XtraMsgBox.Show("请添加免许明细！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    for (int j = 0; j < gridView1.Columns.Count; j++)
                    {
                        string _content = gridView1.GetRowCellValue(i, gridView1.Columns[j]).ToString();
                        if (string.IsNullOrEmpty(_content))
                        {
                            XtraMsgBox.Show("请检查明细表中，各列不能为空！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            isSucces = false;
                            return;
                        }
                    }

                    string position = gridView1.GetRowCellValue(i, "position").ToString();
                    if (!DataValid.IsNumeric(position))
                    {
                        XtraMsgBox.Show("请检查表格中，位置必须是数字！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                //    DataTable dt_temp = ((DataView)gridView1.DataSource).ToTable();
                //    for (int a = 0; a < dt_temp.Rows.Count; a++)
                //    {
                //        DataRow row = dt_temp.Rows[a];
                //        row = dt_temp.Rows[a];
                //        for (int b = 0; b < dt_temp.Rows.Count; b++)
                //        {
                //            if (a == b) continue;
                //            DataRow d_dr = dt_temp.Rows[b];
                //            if (row["JobForName"] == d_dr["JobForName"] && row["ProjectName"] == d_dr["ProjectName"] && row["LineName"] == d_dr["LineName"] && row["guanweiName"] == d_dr["guanweiName"] && row["levelName"] == d_dr["levelName"])
                //            {
                //                XtraMsgBox.Show("请检查明细表中，同一向别工程别Line别关位下的免许等级不能相同！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                isSucces = false;
                //                return;
                //            }
                //        }
                //    }

            }
            //数据库重复判断
            if (this.ScanMode == Common.DataModifyMode.add)
            {
                string str = string.Format(@"select * from License_Rec_i where UserID='{0}'", this.txtUserID.Text.Trim());
                DataTable dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str);
                if (dt.Rows.Count > 0)
                {
                    isSucces = false;
                    DataValid.ShowErrorInfo(this.ErrorInfo, this.txtUserID, "此用户的免许记录已经存在!");
                }
            }
            isSucces = true;

            txtGuanwei.Focus();
        }


        //取得修改数据
        public void GetDataRowValue(DataRow dr)
        {
            try
            {
                txtID.Text = dr["myID"].ToString();//id
                lookUpEditLicenseType.Text = dr["LicenseTypeName"].ToString();//免许类型
                dateEditDate.Text = dr["ValidDate"].ToString();//有效日期
                txtUserID.Text = dr["UserID"].ToString();//人员编号
                txtUserName.Text = dr["UserName"].ToString();//人员姓名
                txtmyTeamName.Text = dr["myTeamName"].ToString();//人员向别-班别
                txtGuanwei.Text = dr["GuanweiName"].ToString();//人员关位
                //GetUserImage();
                this.pictureEdit1.Image = Image.FromFile(GridCommon.GetUserImageOld(SysParam.m_daoCommon, dr["UserID"].ToString()));

                //免许资格P_License_Rec_Entitle
                string strEntitle = string.Empty;
                string str_sql = "SELECT RecID ,EntitleID   FROM  P_License_Rec_Entitle where 1=1 "
                                + "  AND RecID='" + dr["myID"].ToString() + "' ";
                DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                //***********免许资格数据插入*********
                foreach (DataRow dr2 in dt_temp.Rows)
                {
                    for (int j = 0; j < lookEntitle.Properties.Items.Count; j++)
                    {
                        if (lookEntitle.Properties.Items[j].Value.ToString() == dr2["EntitleID"].ToString())
                        {

                            lookEntitle.Properties.Items[j].CheckState = CheckState.Checked;
                            strEntitle += lookEntitle.Properties.Items[j].Description + ",";
                        }
                    }

                }

                //免许明细
                m_dicItemData = new System.Collections.Specialized.StringDictionary();

                str_sql = @"select a.ID ,a.JobForID,a.ProjectID ,a.LineID,a.guanweiID,a.position,a.level as levelID ,a.OperID,a.OperDate   
                                from P_License_Detail as a
                                inner join License_Rec_i as b on a.ID=b.ID  
                                where 1=1     AND a.ID='" + dr["myID"].ToString() + "' order by a.JobForID,a.ProjectID ,a.LineID,a.guanweiID";
                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (!m_ViewDetail.Columns.Contains("LicenseTypeName"))
                {
                    m_ViewDetail.Columns.Add("LicenseTypeName");
                    m_ViewDetail.Columns.Add("ValidDate");
                    m_ViewDetail.Columns.Add("EntitleName");
                }

                for (int i = 0; i < m_tblDataList.Rows.Count; i++)
                {
                    DataRow dr3 = m_ViewDetail.NewRow();

                    DataView view = new DataView(m_tblJobForID.Copy());
                    view.RowFilter = "JobForID='" + m_tblDataList.Rows[i]["JobForID"].ToString() + "'";
                    dr3["JobForID"] = m_tblDataList.Rows[i]["JobForID"].ToString();
                    dr3["JobForName"] = view.ToTable().Rows[0]["JobForName"].ToString();//向别

                    view = new DataView(m_tblProjectID.Copy());
                    view.RowFilter = "ProjectID='" + m_tblDataList.Rows[i]["ProjectID"].ToString() + "'";
                    dr3["ProjectID"] = m_tblDataList.Rows[i]["ProjectID"].ToString();
                    dr3["ProjectName"] = view.ToTable().Rows[0]["ProjectName"].ToString();//工程别

                    view = new DataView(m_tblLineID.Copy());
                    view.RowFilter = "LineID='" + m_tblDataList.Rows[i]["LineID"].ToString() + "'";
                    dr3["LineID"] = m_tblDataList.Rows[i]["LineID"].ToString();
                    dr3["LineName"] = view.ToTable().Rows[0]["LineName"].ToString();//Line别

                    view = new DataView(m_tblGuanweiID.Copy());
                    view.RowFilter = "guanweiID='" + m_tblDataList.Rows[i]["guanweiID"].ToString() + "'";
                    dr3["guanweiID"] = m_tblDataList.Rows[i]["guanweiID"].ToString();
                    dr3["guanweiName"] = view.ToTable().Rows[0]["guanweiName"].ToString();//关位
                    dr3["position"] = m_tblDataList.Rows[i]["position"].ToString();

                    view = new DataView(m_tblSID.Copy());
                    view.RowFilter = "ID='" + m_tblDataList.Rows[i]["levelID"].ToString() + "'";
                    dr3["levelID"] = m_tblDataList.Rows[i]["levelID"].ToString();
                    dr3["levelName"] = view.ToTable().Rows[0]["pName"].ToString();//等级

                    dr3["OperID"] = m_tblDataList.Rows[i]["OperID"].ToString();
                    dr3["OperDate"] = m_tblDataList.Rows[i]["OperDate"].ToString();

                    dr3["LicenseTypeName"] = dr["LicenseTypeName"].ToString();//免许类型
                    dr3["ValidDate"] = dr["ValidDate"].ToString();//有效日期
                    dr3["EntitleName"] = strEntitle;//免许资格

                    dr3["RowStatus"] = "1";//0新增，1修改
                    m_ViewDetail.Rows.Add(dr3);
                }

                this.m_GridViewUtil.GridControlList.DataSource = m_ViewDetail.DefaultView;
            }
            catch (Exception ex)
            {
                log.Error("取得修改数据失败" + ex.ToString());
                XtraMsgBox.Show("取得修改数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }

        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            try
            {
                if (xtraTabControl1.SelectedTabPageIndex == 1)
                {
                    if (txtUserID.Text.Trim() == "") return;
                    string str_sql = string.Format(@"select * from P_Subsisiary where Userid='{0}'", txtUserID.Text.Trim());
                    DataTable dt_data = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                    gridControl2.DataSource = dt_data;
                    gridView2.OptionsBehavior.Editable = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///// <summary>
        ///// 取得用户头像
        ///// </summary>
        //private void GetUserImage()
        //{
        //    try
        //    {
        //        //获取人员头像文件夹

        //        string _sql = string.Format(" select * from P_System_Para where pIndex='1' and Pcode='SYS_DIR'");
        //        DataTable _dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(_sql);
        //        string EmPathDir = _dt.Rows[0]["pName"].ToString();//人事系统头像共享文件夹

        //        //人事系统ip地址
        //        _sql = string.Format(" select * from P_System_Para where pIndex='3'  and Pcode='SYS_DIR'");
        //        _dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(_sql);
        //        string EmHttpIpWork = _dt.Rows[0]["pName"].ToString();//考勤系统ip（车间）

        //        _sql = string.Format(" select * from P_System_Para where pIndex='4'  and Pcode='SYS_DIR'");
        //        _dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(_sql);
        //        string EmHttpIpRoom = _dt.Rows[0]["pName"].ToString();//考勤系统ip（办公室）

        //        //考勤系统头像目录
        //        _sql = string.Format(" select * from P_System_Para where pIndex='2'  and Pcode='SYS_DIR'");
        //        _dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(_sql);

        //        string AtPathDir = Application.StartupPath + "\\" + _dt.Rows[0]["pName"].ToString();//考勤系统头像目录
        //        string userId = this.txtUserID.EditValue.ToString();
        //        if (userId == "")
        //        {
        //            AtPathDir = AtPathDir + "01.png";

        //        }
        //        //ip是10.71开始：EmHttpIpRoom；ip是192.168开始：EmHttpIpWork；
        //        string serverIp = EmHttpIpRoom;
        //        //string HttpIp = EmHttpIpWork;
        //        if (!File.Exists(AtPathDir + userId + ".jpg"))
        //        {//考勤系统人员头像文件不存在 
        //            int ret = NetworkSharedFolder.Connect(serverIp + EmPathDir, AtPathDir, "administrators", "i3b4m6");

        //            if (File.Exists(serverIp + EmPathDir + userId + ".jpg"))
        //            {//从人事系统复制头像到考勤系统：根据用户登录IP判断，

        //                System.IO.File.Copy(serverIp + EmPathDir + userId + ".jpg", AtPathDir + userId + ".jpg", false);

        //                AtPathDir = AtPathDir + userId + ".jpg";
        //            }
        //            else
        //            {
        //                AtPathDir = AtPathDir + "01.png";
        //            }
        //        }
        //        else
        //        {
        //            AtPathDir = AtPathDir + this.txtUserID.EditValue.ToString() + ".jpg";
        //        }

        //       this.pictureEdit1.Image = Image.FromFile(AtPathDir);

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        #endregion

    }
}

