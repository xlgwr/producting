using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MachineSystem.SysDefine;
using Framework.Abstract;
using System.Collections.Specialized;

namespace MachineSystem.TabPage
{
    public partial class frmRoleInfoSearch : Framework.Abstract.frmAssistBasic
    {
        #region 变量定义

        /// <summary>
        /// 编号
        /// </summary>
        private string m_RoleID = "";
        public string RoleID_Search
        {
            get { return m_RoleID; }
            set { m_RoleID = value; }
        }

        #endregion


        #region 初始化处理

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmRoleInfoSearch()
        {
            InitializeComponent();

            //初始化表格控件处理
            this.m_GridViewUtil.GridControlList = this.GridMain;

            //初始化主表对象
            this.m_GridViewUtil.ParentGridView = this.gridView1;

            //信息维护表所在控件
            this.m_GrpDataItem = this.grpInfo;

            //数据查询视图
            this.ViewTableName = TableNames.Oper_Role;

            //表格选择列名(父表)
            this.m_ParenSlctColName = EnumDefine.SlctValue; //"SlctButton";


            this.m_dicLikeConds[this.txtRoleD.Tag.ToString()] = "true";
            this.m_dicLikeConds[this.txtRoleName.Tag.ToString()] = "true";

            this.TopMost = true;

        }

          /// <summary>
        /// 初始化
        /// </summary>
        public override void SetFormValue()
        {

            //画面控件窗体初始化
            base.SetFormValue();
            if (!string.IsNullOrEmpty(m_RoleID))
            {
                this.txtRoleD.Text = m_RoleID;
            }
            GetGroupDataSearch(this.grpInfo, ref this.m_dicItemData, ref this.m_dicConds, ref this.m_dicLikeConds);
            SetSearchProc(this);
        }
        #endregion


        #region 画面按钮初始化处理方法

        protected override void SetSearchProc(frmAssistBasic frmBaseToolXC)
        {
            base.SetSearchProc(frmBaseToolXC);
        }

        /// <summary>
        /// 处理数据功能处理
        /// </summary>
        protected override void SetSaveDataProc(object sender, EventArgs e)
        {

            try
            {
                base.SetSaveDataProc(sender, e);

            }
            catch (Exception ex)
            {
                XtraMsgBox.Show("选择数据处理失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 获取画面输入数据信息及相应的列名
        /// </summary>
        /// <param name="ctl">控件对象</param>
        /// <param name="dicon">精确查询条件列</param>
        /// <param name="sd">显示的值列</param>
        /// <param name="dicLike">模糊查询条件列</param>
        private void GetGroupDataSearch(Control ctl, ref StringDictionary sd, ref StringDictionary dicon, ref StringDictionary dicLike)
        {
            if (ctl == null) return;

            if (ctl.Tag != null && !string.IsNullOrEmpty(ctl.Tag.ToString()))
                switch (ctl.GetType().ToString())
                {
                    case "DevExpress.XtraEditors.ButtonEdit":
                        if (!string.IsNullOrEmpty(ctl.Text.Trim()))
                        {
                            sd[ctl.Tag.ToString()] = ctl.Text;
                            dicLike[ctl.Tag.ToString()] = ctl.Tag.ToString();
                        }
                        break;
                    case "DevExpress.XtraEditors.TextEdit":
                        if (!string.IsNullOrEmpty(ctl.Text.Trim()))
                        {
                            sd[ctl.Tag.ToString()] = ctl.Text;
                            dicLike[ctl.Tag.ToString()] = ctl.Tag.ToString();
                        }
                        break;
                    case "DevExpress.XtraEditors.LookUpEdit":
                        if (((DevExpress.XtraEditors.LookUpEdit)ctl).EditValue != null && ((DevExpress.XtraEditors.LookUpEdit)ctl).EditValue.ToString() != Framework.Libs.Common.DefineValue.DefalutItemAllNo)
                        {
                            sd[ctl.Tag.ToString()] = ((DevExpress.XtraEditors.LookUpEdit)ctl).EditValue.ToString();
                            dicon[ctl.Tag.ToString()] = ctl.Tag.ToString();
                        }
                        break;
                    case "DevExpress.XtraEditors.RadioGroup":
                        sd[ctl.Tag.ToString()] = ((DevExpress.XtraEditors.RadioGroup)ctl).EditValue.ToString();
                        dicon[ctl.Tag.ToString()] = ctl.Tag.ToString();
                        break;
                    case "DevExpress.XtraEditors.ComboBoxEdit":
                        if (((DevExpress.XtraEditors.ComboBoxEdit)ctl).SelectedIndex >= 0)
                        {
                            sd[ctl.Tag.ToString()] = ((DevExpress.XtraEditors.ComboBoxEdit)ctl).EditValue.ToString();
                            dicon[ctl.Tag.ToString()] = ctl.Tag.ToString();
                        }
                        break;
                }

            if (ctl.Controls != null)
            {
                foreach (Control _ctlSub in ctl.Controls)
                {
                    //对窗体的所有控件处理

                    GetGroupDataSearch(_ctlSub, ref sd, ref dicon, ref dicLike);
                }
            }
        }

        #endregion
    }
}
