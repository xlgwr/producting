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
using MachineSystem.TabPage;
using DevExpress.XtraGrid.Views.Grid;
using MachineSystem.SettingTable;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;

namespace MachineSystem.form.UserRole
{
    public partial class frmEditOper_Info : Framework.Abstract.frmBaseToolEntryXC
    {
        #region 变量定义

        /// <summary>
        /// 选中行信息
        /// </summary>
        public DataRow dr;

        /// <summary>
        /// 账号状态信息
        /// </summary>
        public DataTable m_tblCbxList = new DataTable();

        /// <summary>
        /// 用户类型
        /// </summary>
        public DataTable m_tblCbxType = new DataTable();

        #endregion

        #region 画面初始化

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmEditOper_Info()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                //txtID.Properties.ReadOnly = true;
                SaveButtonEnabled = true;
                CancelButtonEnabled = true;
                GetComBobox();
                if (this.ScanMode == Common.DataModifyMode.add)
                {
                    this.Text = "新增操作员资料登记";
                    GetDspDataList();
                    
                }
                else if (this.ScanMode == Common.DataModifyMode.upd)
                {
                    if (dr != null)
                    {
                        this.Text = "修改操作员资料登记";
                        GetDataRowValue(dr);
                    }
                }
                
                repositoryItemComboBox3.ParseEditValue += new ConvertEditValueEventHandler(repositoryItemComboBox3_ParseEditValue);
                repositoryItemComboBox3.EditValueChanged += new EventHandler(repositoryItemComboBox3_EditValueChanged);

                repositoryItemComboBox4.ParseEditValue +=new ConvertEditValueEventHandler(repositoryItemComboBox4_ParseEditValue);
                repositoryItemComboBox4.EditValueChanged += new EventHandler(repositoryItemComboBox4_EditValueChanged);
                
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("画面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
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
            DataRow _dr;
            try
            {
               
                gridView1.CloseEditor();
                gridView1.UpdateCurrentRow();
                gridView2.CloseEditor();
                gridView2.UpdateCurrentRow();
                bool isScuess = false;
                GetInputCheck(ref isScuess);
                if (!isScuess) return;

                int result = 0;
                DataTable dt_temp = ((DataView)gridView1.DataSource).ToTable();
                DataTable dt_temp2 = ((DataView)gridView2.DataSource).ToTable();
                string str_sql = string.Empty;

                Common.AdoConnect.Connect.CreateSqlTransaction();
                m_dicPrimarName.Clear();
                m_dicItemData.Clear();


                for (int i = 0; i < dt_temp.Rows.Count; i++)
                {
                    //如果Oper_Info表中没有数据 新增数据
                    str_sql = string.Format(@"select * from Oper_Info where OperID='{0}'", dt_temp.Rows[i]["UserID"].ToString());
                    DataTable dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                    if (dt.Rows.Count > 0)
                    {
                        //回写账号状态和密码到Oper_Info表中
                        str_sql = string.Format(@"update Oper_Info set StatusID='{0}', Pwd='{1}' where OperID='{2}'",
                                            dt_temp.Rows[i]["statusID"].ToString(), dt_temp.Rows[i]["passWord"].ToString(), dt_temp.Rows[i]["UserID"].ToString());
                        result = SysParam.m_daoCommon.SetModifyDataItemBySql(str_sql, m_dicItemData, m_dicPrimarName, m_dicUserColum);

                        //日志
                        SysParam.m_daoCommon.WriteLog("人员信息表", "修改", dt_temp.Rows[i]["UserID"].ToString());
                    }
                    else 
                    {
                        //新增用户到Oper_Info表中
                        m_dicItemData.Clear();
                        m_dicItemData["OperID"] = dt_temp.Rows[i]["UserID"].ToString();
                        m_dicItemData["TypeID"] = dt_temp.Rows[i]["OperType"].ToString();
                        m_dicItemData["StatusID"] = dt_temp.Rows[i]["StatusID"].ToString();
                        m_dicItemData["Pwd"] = dt_temp.Rows[i]["passWord"].ToString();
                        m_dicItemData["WebRole"] = "";
                        m_dicItemData["Memo"] = "";

                        result = SysParam.m_daoCommon.SetInsertDataItem(TableNames.Oper_Info, m_dicItemData);

                        //日志
                        SysParam.m_daoCommon.WriteLog("人员信息", "新增", dt_temp.Rows[i]["UserID"].ToString());
                    }

                    //SetSaveRoleOper(dt_temp2, dt_temp);

                    //保存到人员和角色绑定表
                    str_sql = string.Format(@"select * from Oper_User_Role where UserID='{0}'", dt_temp.Rows[i]["UserID"].ToString());
                    dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                    if (dt.Rows.Count > 0)//表中已存在该用户则是修改
                    {
                        m_dicPrimarName.Clear();
                        m_dicPrimarName["UserID"] = dt_temp.Rows[i]["UserID"].ToString();

                        //先删除已存在的用户权限
                        m_dicItemData["UserID"] = dt_temp.Rows[i]["UserID"].ToString();
                        result = SysParam.m_daoCommon.SetDeleteDataItem("Oper_User_Role", m_dicItemData, m_dicPrimarName);

                        //保存当前用户的角色信息
                        m_dicItemData.Clear();
                        for (int k = 0; k < dt_temp2.Rows.Count; k++)
                        {
                            _dr = dt_temp2.Rows[k];
                            if (!_dr["SlctValue"].ToString().ToLower().Equals("true"))
                            {
                                continue;
                            }
                            m_dicItemData["UserID"] = dt_temp.Rows[i]["UserID"].ToString();
                            m_dicItemData["OperID"] = Common._personid;
                            m_dicItemData["RoleID"] = dt_temp2.Rows[k]["RoleID"].ToString();
                            m_dicItemData["ReMark"] = dt_temp2.Rows[k]["RoleName"].ToString();
                            m_dicItemData["operDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            result = SysParam.m_daoCommon.SetInsertDataItem("Oper_User_Role", m_dicItemData);

                            //日志
                            SysParam.m_daoCommon.WriteLog("人员资料登记", "修改权限", dt_temp.Rows[i]["UserID"].ToString());
                        }
                    }
                    else //表中不存在该用户则是新增
                    {
                        m_dicPrimarName.Clear();
                        m_dicPrimarName["UserID"] = dt_temp.Rows[i]["UserID"].ToString();
                        m_dicPrimarName["RoleID"] = dt_temp2.Rows[i]["RoleID"].ToString();

                        //保存当前用户的角色信息
                        m_dicItemData.Clear();
                        for (int k = 0; k < dt_temp2.Rows.Count; k++)
                        {
                            _dr = dt_temp2.Rows[k];
                            if (!_dr["SlctValue"].ToString().ToLower().Equals("true"))
                            {
                                continue;
                            }
                            m_dicItemData["UserID"] = dt_temp.Rows[i]["UserID"].ToString();
                            m_dicItemData["OperID"] = Common._personid;
                            m_dicItemData["RoleID"] = dt_temp2.Rows[k]["RoleID"].ToString();
                            m_dicItemData["ReMark"] = dt_temp2.Rows[k]["RoleName"].ToString();
                            m_dicItemData["operDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            result = SysParam.m_daoCommon.SetInsertDataItem("Oper_User_Role", m_dicItemData, m_dicPrimarName);

                            //日志
                            SysParam.m_daoCommon.WriteLog("人员信息登记", "新增权限", dt_temp.Rows[i]["UserID"].ToString());
                        }
                    }
                }
                Common.AdoConnect.Connect.TransactionCommit();
                XtraMsgBox.Show("保存数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Common.AdoConnect.Connect.TransactionRollback();
                XtraMsgBox.Show("保存数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        private void SetSaveRoleOper(DataTable dt_temp2, DataTable dt_temp)
        {
           
            //DataRow _dr;
            //DataTable dt;
            //string str_sql;
            try
            {

                //for (int i = 0; i <= dt_temp2.Rows.Count - 1; i++)
                //{
                //    _dr = dt_temp2.Rows[i];

                //    if (_dr["SlctValue"].ToString().ToLower() == "true")
                //    {
                //        //this.m_drSelectRows.Add(dr);
                //    }
                //}

                //str_sql = string.Format(@"select * from Oper_User_Role where UserID='{0}'", dt_temp.Rows[i]["UserID"].ToString());
                //dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                //if (dt.Rows.Count > 0)//表中已存在该用户则是修改
                //{
                //    m_dicPrimarName.Clear();
                //    m_dicPrimarName["UserID"] = dt_temp.Rows[i]["UserID"].ToString();

                //    //先删除已存在的用户权限
                //    m_dicItemData["UserID"] = dt_temp.Rows[i]["UserID"].ToString();
                //    result = SysParam.m_daoCommon.SetDeleteDataItem("Oper_User_Role", m_dicItemData, m_dicPrimarName);

                //    //保存当前用户的角色信息
                //    m_dicItemData.Clear();
                //    for (int k = 0; k < dt_temp2.Rows.Count; k++)
                //    {
                //        m_dicItemData["UserID"] = dt_temp.Rows[i]["UserID"].ToString();
                //        m_dicItemData["OperID"] = Common._personid;
                //        m_dicItemData["RoleID"] = dt_temp2.Rows[k]["RoleID"].ToString();
                //        m_dicItemData["ReMark"] = dt_temp2.Rows[k]["RoleName"].ToString();
                //        m_dicItemData["operDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //        result = SysParam.m_daoCommon.SetInsertDataItem("Oper_User_Role", m_dicItemData);

                //        //日志
                //        SysParam.m_daoCommon.WriteLog("人员资料登记", "修改权限", dt_temp.Rows[i]["UserID"].ToString());
                //    }
                //}
                //else //表中不存在该用户则是新增
                //{
                //    m_dicPrimarName.Clear();
                //    m_dicPrimarName["UserID"] = dt_temp.Rows[i]["UserID"].ToString();
                //    m_dicPrimarName["RoleID"] = dt_temp2.Rows[i]["RoleID"].ToString();

                //    //保存当前用户的角色信息
                //    m_dicItemData.Clear();
                //    for (int k = 0; k < dt_temp2.Rows.Count; k++)
                //    {
                //        m_dicItemData["UserID"] = dt_temp.Rows[i]["UserID"].ToString();
                //        m_dicItemData["OperID"] = Common._personid;
                //        m_dicItemData["RoleID"] = dt_temp2.Rows[k]["RoleID"].ToString();
                //        m_dicItemData["ReMark"] = dt_temp2.Rows[k]["RoleName"].ToString();
                //        m_dicItemData["operDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //        result = SysParam.m_daoCommon.SetInsertDataItem("Oper_User_Role", m_dicItemData, m_dicPrimarName);

                //        //日志
                //        SysParam.m_daoCommon.WriteLog("人员信息登记", "新增权限", dt_temp.Rows[i]["UserID"].ToString());
                //    }
                //}
                
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// 添加人员信息
        /// </summary>
        private void btnAdd1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = null;
                frmUserInfoSearch frm = new frmUserInfoSearch();
                frm.ScanMode = Common.DataModifyMode.add;
                frm.blSearch = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    DataRow[] d = null;
                    DataRow drow;
                    DataRow dr;
                    if (this.gridView1.DataSource != null)
                        //如果表数据不为空的话把表结构给datatable
                        dt = ((DataView)this.gridView1.DataSource).Table;

                    if (dt != null && dt.Rows.Count <= 0)
                        dt = null;

                    for (int i = 0; i <= frm.SelectRowDatas.Count - 1; i++)
                    {
                        drow = (DataRow)frm.SelectRowDatas[i];

                        if (dt == null)
                        {
                            dt = drow.Table.Clone();
                        }

                        d = dt.Select("UserID ='" + drow["UserID"] + "'");
                        if (d.Count() <= 0)
                        {
                            if (!dt.Columns.Contains("SlctValue1"))
                            {
                                dt.Columns.Add("SlctValue1", typeof(Boolean));
                                dt.Columns.Add("status");
                                dt.Columns.Add("statusID");
                                dt.Columns.Add("passWord");
                                dt.Columns.Add("OperType");
                                dt.Columns.Add("OperT");
                            }
                            dr = dt.NewRow();
                            for (int index = 0; index <= dt.Columns.Count - 1; index++)
                            {
                                if (drow.Table.Columns.Contains(dt.Columns[index].ColumnName))
                                {
                                    dr[dt.Columns[index].ColumnName] = drow[dt.Columns[index].ColumnName];

                                }

                            }
                            dr["SlctValue1"] = dr["SlctValue"].ToString();
                            dr["status"] = "";
                            dr["passWord"] = "";
                            dr["statusID"] = "";
                            dr["OperType"] = "";
                            dr["OperT"] = "";
                            
                            dt.Rows.Add(dr);
                            dt.AcceptChanges();
                        }
                    }
                    if (dt == null)
                    {
                        dt = new DataTable();
                    }
                    this.gridControl1.DataSource = dt.DefaultView;
                    GetRoleDt();
                }
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("加载数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 删除人员信息选中的行
        /// </summary>
        private void btnDel1_Click(object sender, EventArgs e)
        {
            try
            {
                this.DeleteRow(this.gridView1, "SlctValue1");
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("删除行失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 添加人员权限信息
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = null;
                frmRoleInfoSearch frm = new frmRoleInfoSearch();
                frm.ScanMode = Common.DataModifyMode.add;
                frm.blSearch = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    DataRow[] d = null;
                    DataRow drow;
                    DataRow dr;
                    if (this.gridView2.DataSource != null)
                        //如果表数据不为空的话把表结构给datatable
                        dt = ((DataView)this.gridView2.DataSource).Table;

                    if (dt != null && dt.Rows.Count <= 0)
                        dt = null;

                    for (int i = 0; i <= frm.SelectRowDatas.Count - 1; i++)
                    {
                        drow = (DataRow)frm.SelectRowDatas[i];

                        if (dt == null)
                        {
                            dt = drow.Table.Clone();
                        }

                        d = dt.Select("RoleID ='" + drow["RoleID"] + "'");
                        if (d.Count() <= 0)
                        {
                            dr = dt.NewRow();
                            for (int index = 0; index <= dt.Columns.Count - 1; index++)
                            {
                                if (drow.Table.Columns.Contains(dt.Columns[index].ColumnName))
                                {
                                    dr[dt.Columns[index].ColumnName] = drow[dt.Columns[index].ColumnName];

                                }

                            }
                            dt.Rows.Add(dr);
                            dt.AcceptChanges();
                        }
                    }
                    if (dt == null)
                    {
                        dt = new DataTable();
                    }
                    this.gridControl2.DataSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("加载数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 删除人员权限的选中行
        /// </summary>
        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                this.DeleteRow(this.gridView2, "SlctValue");
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("删除行失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }


        #endregion

        #region 事件处理方法

        
        /// <summary>
        /// 账号状态下拉框处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 改变账号状态下拉事件
        /// </summary>
        void repositoryItemComboBox3_EditValueChanged(object sender, EventArgs e)
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            DataTable m_tblDataList = ((DataView)this.gridView1.DataSource).ToTable();
            DataRow row = gridView1.GetFocusedDataRow();
            DataView view = new DataView(m_tblCbxList.Copy());

            view.RowFilter = "pName='" + row["status"].ToString() + "'";
            DataTable dt = view.ToTable();
            m_tblDataList.Rows[gridView1.FocusedRowHandle]["statusID"] = dt.Rows[0]["ID"].ToString();
            gridControl1.DataSource = m_tblDataList;
        }


        /// <summary>
        /// 改变用户类型下拉事件
        /// </summary>
        void repositoryItemComboBox4_EditValueChanged(object sender, EventArgs e)
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            DataTable m_tblDataList = ((DataView)this.gridView1.DataSource).ToTable();
            DataRow row = gridView1.GetFocusedDataRow();
            DataView view = new DataView(m_tblCbxType.Copy());

            view.RowFilter = "pName='" + row["OperT"].ToString() + "'";
            DataTable dt = view.ToTable();
            m_tblDataList.Rows[gridView1.FocusedRowHandle]["OperType"] = dt.Rows[0]["ID"].ToString();
            gridControl1.DataSource = m_tblDataList;
       }
        /// <summary>
        /// 获取角色信息
        /// </summary>
        private void GetRoleDt()
        {
            DataTable _dt = new DataTable();
            try
            {
                //查出人员信息
                string str = string.Format(@"SELECT  CAST('0' AS Bit) AS SlctValue,Oper_Role.*  FROM Oper_Role WHERE 1=1 ");
                _dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str);
                this.gridControl2.DataSource = _dt.DefaultView;
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("获取角色权限信息失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
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
                //GetRoleDt();
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 画面数据有效检查处理
        /// </summary>
        protected override void GetInputCheck(ref bool isSucces)
        {
            base.GetInputCheck(ref isSucces);
            isSucces = false;
            try
            {
                if (gridView1.DataSource == null || gridView2.DataSource == null)
                {
                    XtraMsgBox.Show("请填写完整用户信息和角色信息！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DataTable dt_temp = ((DataView)gridView1.DataSource).ToTable();
                DataTable dt_temp2 = ((DataView)gridView2.DataSource).ToTable();
                if (dt_temp.Rows.Count <= 0 || dt_temp2.Rows.Count <= 0) 
                {
                    XtraMsgBox.Show("请填写完整用户信息和角色信息！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                for (int i = 0; i < dt_temp.Rows.Count; i++)
                {
                    if (dt_temp.Rows[i]["statusID"].ToString() == "")
                    {
                        XtraMsgBox.Show("用户状态不能为空！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (dt_temp.Rows[i]["passWord"].ToString() == "")
                    {
                        XtraMsgBox.Show("用户密码不能为空！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (dt_temp.Rows[i]["OperType"].ToString() == "")
                    {
                        XtraMsgBox.Show("用户类型不能为空！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                    isSucces = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void GetDataRowValue(DataRow dr)
        {
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataRow _dr;
            try
            {
                 m_dicItemData.Clear();
            m_dicItemData["UserID"] = dr["OperID"].ToString();
            m_dicConds["UserID"] = "true";

            //查出人员信息
            string str = string.Format(@"select u.*,o.Pwd as passWord,
                                                    o.StatusID,s.pName as status,
                                                    o.TypeID as OperType,'' as OperT 
                                            from V_User_Info u inner join Oper_Info o
                                            inner join P_Oper_Status s on o.StatusID=s.ID
                                            on u.UserID=o.OperID  where u.UserID='{0}'", 
                                            dr["OperID"].ToString());
            dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str);

            if (!dt.Columns.Contains("SlctValue1"))
            {
                dt.Columns.Add("SlctValue1", typeof(Boolean));
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["SlctValue1"] = "True";
                int opertype = int.Parse(dt.Rows[0]["OperType"].ToString())-1;
                dt.Rows[0]["OperT"] = m_tblCbxType.Rows[opertype]["pName"].ToString();

            }
            gridControl1.DataSource = dt;


            ////查出当前人员的角色
            ////dt2 = SysParam.m_daoCommon.GetTableInfo("Oper_User_Role", m_dicItemData, m_dicConds, m_dicLikeConds, "");
            //str = "SELECT  CAST('1' AS Bit) AS SlctValue,a.*,b.RoleName  FROM Oper_User_Role as a " +
            //             "left join Oper_Role as b on a.RoleID=b.RoleID  WHERE 1=1 and a.UserID='" +
            //              dr["OperID"].ToString() + "'";
            //    dt2 = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str);
            //gridControl2.DataSource = dt2;
                //////////////////////////
             str = "SELECT  CAST('1' AS Bit) AS SlctValue,a.*,b.RoleName  FROM Oper_User_Role as a " +
                         "left join Oper_Role as b on a.RoleID=b.RoleID  WHERE 1=1 and a.UserID='" +
                          dr["OperID"].ToString() + "'";
            DataTable _dt = new DataTable();
            _dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str);

            str = string.Format(@"SELECT  CAST('0' AS Bit) AS SlctValue,Oper_Role.*  FROM Oper_Role WHERE 1=1 ");
            dt2 = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str);

            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                string _val=_dt.Rows[i]["RoleID"].ToString();
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    _dr = dt2.Rows[j];
                    string ss = _dr["RoleID"].ToString();
                    if (_dr["RoleID"].ToString().Equals(_val))
                    {
                        _dr.BeginEdit();
                        _dr["SlctValue"] = "True";
                        _dr.EndEdit();
                    }
                }
            }

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
               
                
            }

            gridControl2.DataSource = dt2;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        /// <summary>
        /// 删除选中行数据
        /// </summary>
        private void DeleteRow(GridView dr,string FileName)
        {
            try
            {
                for (int i = dr.RowCount - 1; i >= 0; i--)
                {
                    if (Convert.ToBoolean(dr.GetRowCellValue(i, FileName)) == true)
                    {
                        dr.DeleteRow(i);
                    }
                }
                if (dr.DataSource != null)
                {
                    ((DataView)dr.DataSource).Table.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取下拉
        /// </summary>
        public void GetComBobox() 
        {
            //获取账号状态下拉
            string str = string.Format(@"select * from P_Oper_Status");
            m_tblCbxList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str);
            for (int i = 0; i < m_tblCbxList.Rows.Count; i++)
            {
                CboItemEntity item = new CboItemEntity();
                item.Value = m_tblCbxList.Rows[i]["ID"].ToString();
                item.Text = m_tblCbxList.Rows[i]["pName"].ToString();
                repositoryItemComboBox3.Items.Add(item);
            } 

            //获取用户类型下拉
            str = string.Format(@"select * from P_Oper_Type");
            m_tblCbxType = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str);
            for (int i = 0; i < m_tblCbxType.Rows.Count; i++)
            {
                CboItemEntity item = new CboItemEntity();
                item.Value = m_tblCbxType.Rows[i]["ID"].ToString();
                item.Text = m_tblCbxType.Rows[i]["pName"].ToString();
                repositoryItemComboBox4.Items.Add(item);
            } 
        }
        #endregion


    }
}
