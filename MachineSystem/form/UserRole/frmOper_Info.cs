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
using DevExpress.XtraEditors.Controls;
using MachineSystem.form.UserRole;
using Framework.Libs;

namespace MachineSystem.TabPage
{
    public partial class frmOper_Info : Framework.Abstract.frmSearchBasic2
    {
        #region 变量定义

        /// <summary>
        /// 数据表
        /// </summary>
        DataTable m_tblDataList = new DataTable();

        #endregion


        #region 画面初始化

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmOper_Info()
        {
            InitializeComponent();
            this.m_GridViewUtil.GridControlList = this.gridControl1;

            this.m_GridViewUtil.ParentGridView = this.gridView1;

            this.TableName = "V_Oper_Info";
        }

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                base.SetFormValue();

                CopyAddVisibility = false;
                cbxSex.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                cbxStatus.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                gridView1.OptionsBehavior.Editable = false;
                //部门
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.V_User_Dept, this.lookPart, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefalutItemAllText);

                //职等
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_User_Duty, this.lookDuty, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefalutItemAllText);

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
                frmEditOper_Info frm = new frmEditOper_Info();
                frm.ScanMode = Common.DataModifyMode.add;
                if (frm.ShowDialog() == DialogResult.OK) 
                {
                    
                }

            }
            catch (Exception ex)
            {
                XtraMsgBox.Show("新增失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
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
                frmEditOper_Info frm = new frmEditOper_Info();
                frm.ScanMode = Common.DataModifyMode.upd;
                DataRow row=gridView1.GetFocusedDataRow();

                if (row["user_status"].ToString() == "离职")
                {
                    XtraMsgBox.Show("该用户已离职不可操作！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                frm.dr = row;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    SetSearchProc(this);
                }
            }
            catch (Exception ex)
            {
                XtraMsgBox.Show("新增失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        protected override void SetDeleteInit()
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                XtraMsgBox.Show("删除数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region 事件处理方法

          #region 共同方法

        /// <summary>
        /// 获取表格信息一览
        /// </summary>
        protected override void GetDspDataList()
        {
            try
            {
                txtoperNo.Focus();
                //不查出管理员本身
                string str = string.Format(@" select a.*,b.ReMark from V_Oper_Info a 
                                                    left join Oper_User_Role b on a.OperID=b.UserID where  a.OperID<>'Admin'  ");
                if (txtoperNo.Text.Trim() != "") 
                {
                    str += " and a.OperID like '%" + txtoperNo.Text.Trim() + "%'";
                } 
                if (txtoperName.Text.Trim() != "")
                {
                    str += " and a.EmpName like'%" + txtoperName.Text.Trim() + "%'";
                }
                if (lookPart.EditValue.ToString()!="-1")
                {
                    str += " and a.PartID='" + lookPart.EditValue.ToString() + "'";
                }
                if (lookDuty.EditValue.ToString() != "-1")
                {
                    str += " and a.DutyName like '%" + lookDuty.Text.ToString() + "%'";
                }
                if (cbxSex.SelectedIndex >= 1) 
                {
                    str += " and a.Sex='" +cbxSex.EditValue.ToString() + "'";
                }
                if (cbxStatus.SelectedIndex >= 1)
                {
                    str += " and a.User_Status='" + cbxStatus.Text.ToString() + "'";
                }
                str += " and a.User_Status='在职' ";
                str += " Order by id ";
                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str);
                if (!m_tblDataList.Columns.Contains("")) 
                {
                    m_tblDataList.Columns.Add("SexName");
                }
                
                if (m_tblDataList.Rows.Count > 0) 
                {
                    EditButtonEnabled = true;
                }
                gridControl1.DataSource = m_tblDataList;
                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

          #endregion

        #endregion
    }
}
