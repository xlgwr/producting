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

namespace MachineSystem.TabPage 
{
    public partial class frmUser_Info : Framework.Abstract.frmSearchBasic2	
    {
        #region 变量定义
        /// <summary>
        /// 数据表
        /// </summary>
        DataTable m_tblDataList = new DataTable();
        #endregion

        #region 画面初始化

        public frmUser_Info()
        {
            InitializeComponent();
            //查询,退出可用
            SetButtonEnabled();

            this.m_GridViewUtil.GridControlList = this.gridControl1;
            this.m_GridViewUtil.ParentGridView = this.gridView1;
            m_ParenSlctColName = "SlctValue";
        }

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                //设定只读
                //lookUpEditDept.Properties.ReadOnly = true;
                //lookUpEditUserDuty.Properties.ReadOnly = true;
                //cboUserStatus.Properties.ReadOnly = true;
                //cboSex.Properties.ReadOnly = true;
                //部门
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.V_User_Dept, this.lookUpEditDept, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect);
                //职等
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_User_Duty, this.lookUpEditUserDuty, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect);
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("画面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }
        #endregion

        #region 画面按钮功能处理方法

        protected override void SetSearchProc(frmSearchBasic2 frmBaseToolXC)
        {
            base.SetSearchProc(frmBaseToolXC);

            GetDspDataList();

            SetButtonEnabled();
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
            try
            {
                txtUserID.Enabled = true;
                txtUserID.Properties.ReadOnly = false;
                NewButtonEnabled = true;
                string str_sql = "Select  CAST('0' AS Bit) AS SlctValue,* From V_User_Info where 1=1 ";

                if (txtUserID.Text.Trim() != "")
                {
                    str_sql += " and UserID like '%" + txtUserID.Text.Trim() + "%' ";
                }
                if (txtUserName.Text.Trim() != "")
                {
                    str_sql += " and UserName like '%" + txtUserName.Text.Trim() + "%' ";
                }
                if (this.lookUpEditDept.Text.Trim() != "-请选择-")
                {
                    str_sql += " and PartName= '" + lookUpEditDept.Text.Trim() + "' ";
                }
                if (this.lookUpEditUserDuty.Text.Trim() != "-请选择-")
                {
                    str_sql += " and DutyName= '" + lookUpEditUserDuty.Text.Trim() + "' ";
                }
                if (this.cboUserStatus.Text.Trim() != "-请选择-")
                {
                    str_sql += " and User_Status= '" + cboUserStatus.Text.Trim() + "' ";
                }
                if (this.cboSex.Text.Trim() != "-请选择-")
                {
                    str_sql += " and Sex = '" + cboSex.Text.Trim() + "' ";
                }
                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (m_tblDataList.Rows.Count > 0)
                {
                    DeleteButtonEnabled = true;
                    SelectAllButtonEnabled = true;
                    SelectOffButtonEnabled = true;
                    EditButtonEnabled = true;
                }
                else
                {
                    DeleteButtonEnabled = false;
                    SelectAllButtonEnabled = false;
                    SelectOffButtonEnabled = false;
                    EditButtonEnabled = false;
                }
                this.m_GridViewUtil.GridControlList.DataSource = m_tblDataList;
                gridView1.Columns["UserID"].OptionsColumn.ReadOnly = true;
                gridView1.Columns["UserID"].OptionsColumn.AllowEdit = false;
                gridView1.Columns["UserName"].OptionsColumn.ReadOnly = true;
                gridView1.Columns["UserName"].OptionsColumn.AllowEdit = false;
                gridView1.Columns["Sex"].OptionsColumn.ReadOnly = true;
                gridView1.Columns["Sex"].OptionsColumn.AllowEdit = false;
                gridView1.Columns["UserDept"].OptionsColumn.ReadOnly = true;
                gridView1.Columns["UserDept"].OptionsColumn.AllowEdit = false;
                gridView1.Columns["DutyName"].OptionsColumn.ReadOnly = true;
                gridView1.Columns["DutyName"].OptionsColumn.AllowEdit = false;
                gridView1.Columns["User_Status"].OptionsColumn.ReadOnly = true;
                gridView1.Columns["User_Status"].OptionsColumn.AllowEdit = false;

            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 按钮可用设置,查询可用
        /// </summary>
        private void  SetButtonEnabled()
        {
            this.CancelButtonEnabled = false;
            this.CancelButtonVisibility = false;
            this.CopyAddEnabled = false;
            this.CopyAddVisibility = false;
            this.DeleteButtonEnabled = false;
            this.DeleteButtonVisibility =false;
            this.EditButtonEnabled = false;
            this.EditButtonVisibility = false;
            this.ExcelButtonEnabled = false;
            this.ExcelButtonVisibility = false;
            this.ExitButtonEnabled = true;
            this.ExitButtonVisibility = true;
            this.ImportButtonEnabled = false;
            this.ImportButtonVisibility = false;
            this.NewButtonEnabled = false;
            this.NewButtonVisibility = false;
            this.PrintButtonEnabled = false;
            this.PrintButtonVisibility = false;
            this.SaveButtonEnabled = false;
            this.SaveButtonVisibility = false;
            this.SearchButtonEnabled = true;
            this.SearchButtonVisibility = true;
            this.SelectAllButtonEnabled = false;
            this.SelectAllButtonVisibility = false;
            this.SelectOffButtonEnabled = false;
            this.SelectOffButtonVisibility = false;
        }

        #endregion
    }
}
