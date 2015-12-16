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
using DevExpress.XtraEditors.Controls;
using Framework.Libs;

namespace MachineSystem.TabPage
{
    public partial class frmUserInfoSearch : Framework.Abstract.frmAssistBasic
    {
        #region 变量定义

        /// <summary>
        /// 编号
        /// </summary>
        private string m_UserID = "";
        public string UserID_Search
        {
            get { return m_UserID; }
            set { m_UserID = value; }
        }

        #endregion


        #region 初始化处理

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmUserInfoSearch()
        {
            InitializeComponent();

            //初始化表格控件处理
            this.m_GridViewUtil.GridControlList = this.GridMain;

            //初始化主表对象
            this.m_GridViewUtil.ParentGridView = this.gridView1;

            //信息维护表所在控件
            this.m_GrpDataItem = this.grpInfo;

            //数据查询视图
            this.ViewTableName = TableNames.v_Produce_User;

            //表格选择列名(父表)
            this.m_ParenSlctColName = EnumDefine.SlctValue; //"SlctButton";

            this.m_dicLikeConds[this.txtUserID.Tag.ToString()] = "true";
            this.m_dicLikeConds[this.txtUserName.Tag.ToString()] = "true";

            this.m_dicConds[this.cbxSex.Tag.ToString()] = "true";
            this.m_dicConds[this.lookPart.Tag.ToString()] = "true";
            this.m_dicConds[this.lookDuty.Tag.ToString()] = "true";
            this.m_dicConds[this.cbxStatus.Tag.ToString()] = "true";
            this.m_dicConds[this.lookPart.Tag.ToString()] = "true";

            this.TopMost = true;

        }
        
          /// <summary>
        /// 初始化
        /// </summary>
        public override void SetFormValue()
        {

            //画面控件窗体初始化
            base.SetFormValue();

            cbxSex.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            cbxStatus.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;

            //部门
            SysParam.m_daoCommon.SetLoopUpEdit(TableNames.V_User_Dept, this.lookPart, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefalutItemAllText);

            //职等
            SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_User_Duty, this.lookDuty, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefalutItemAllText);

            if (!string.IsNullOrEmpty(m_UserID))
            {
                this.txtUserID.Text = m_UserID;
            }

            txtUserID.Focus();
            GetGroupDataSearch(this.grpInfo, ref this.m_dicItemData, ref this.m_dicConds, ref this.m_dicLikeConds);
        }
        #endregion


        #region 画面按钮初始化处理方法
        
        protected override void SetSearchProc(frmAssistBasic frmBaseToolXC)
        {
            base.SetSearchProc(frmBaseToolXC);
            gridView1.OptionsView.ColumnAutoWidth = true;
            
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
                        if (((DevExpress.XtraEditors.ComboBoxEdit)ctl).SelectedIndex>=1)
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

        protected override void GetGrpDataItem()
        {
            base.GetGrpDataItem();
            if (cbxSex.SelectedIndex == 0)
            {
                m_dicConds.Remove("Sex");
                m_dicItemData.Remove("Sex");
            }
            if (txtUserID.Text.Trim() == "")
            {
                m_dicConds.Remove("userid");
                m_dicItemData.Remove("userid");
                m_dicLikeConds.Remove("userid");
            }
            else 
            {
                m_dicLikeConds["userid"] = txtUserID.Text.Trim();
            }
            if (txtUserName.Text.Trim() == "")
            {
                m_dicConds.Remove("username");
                m_dicItemData.Remove("username");
                m_dicLikeConds.Remove("username");
            }
            else
            {
                m_dicLikeConds["username"] = txtUserName.Text.Trim();
            }
            if (lookPart.EditValue.ToString() == "-1")
            {
                m_dicConds.Remove("PartID");
                m_dicItemData.Remove("PartID");
                m_dicLikeConds.Remove("PartID");
            } 
            if (lookDuty.EditValue.ToString() != "-1")
            {
                m_dicItemData["DutyName"] = lookDuty.Text.ToString();
            }
            else
            {
                m_dicConds.Remove("DutyName");
                m_dicItemData.Remove("DutyName");
                m_dicLikeConds.Remove("DutyName");
            }
        }

        #endregion
    }
}
