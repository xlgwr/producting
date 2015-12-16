using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MachineSystem.UserControls;
using Framework.Abstract;
using MachineSystem.SysDefine;
using Framework.Libs;

namespace MachineSystem.TabPage 
{
    public partial class frmProduce_LineShow : Framework.Abstract.frmBaseXC
    {
        #region 变量定义
        /// <summary>
        /// 人员数据表
        /// </summary>
        DataTable m_tblDataList = new DataTable();

        /// <summary>
        /// 关位数据表
        /// </summary>
        DataTable m_tblGuanweiList = new DataTable();

        /// <summary>
        /// 是否在左边panel中加入控件
        /// </summary>
        bool isLeftInto = false;

        /// <summary>
        /// 关位信息
        /// </summary>
       // UserPersonsList m_PersonList;

        /// <summary>
        /// 人员信息
        /// </summary>
        LineControl m_Line;
        string strparDate = DateTime.Now.ToString();

        #endregion

        #region 画面初始化
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmProduce_LineShow()
        {
            InitializeComponent();

            SetFormValue();
        }

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                dateOperDate1.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
                //排班,类型
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_Produce_TeamKind, lookTeamID, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect);

                GetDspDataList();
                
            }
           catch (Exception ex)
            {
                XtraMsgBox.Show("画面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }
        #endregion

        #region 画面按钮功能处理方法

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetDspDataList();
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
                m_tblDataList = new DataTable();
                m_tblGuanweiList = new DataTable();

                string str_sql = string.Format(@"select * from 	V_Line_Run_Info where 1=1 ");
                if ((dateOperDate1.EditValue != null && dateOperDate1.EditValue.ToString() != ""))
                {
                    DateTime dtBegin = DateTime.Parse(dateOperDate1.EditValue.ToString());
                    str_sql += " and  AttendDate = '" + dtBegin.ToString("yyyy-MM-dd") + "'";
                }
                if (lookTeamID.EditValue.ToString() != "-1")
                {
                    str_sql += " and TeamSetNM= '" + lookTeamID.Text.Trim() + "' ";
                }

                if (Common._personid != Common._Administrator)
                {// &&Common._myTeamName != "" && Common._myTeamName != null
                    str_sql += " and myTeamName='" + Common._myTeamName + "'";
                }
                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                //循环把人员信息放入panel
                panelContent.Controls.Clear();           
                Point _point;//控件坐标
                int lint_Left = 0;//左边距
                int lint_Top = 0;//上边距
                int count = 0;//当前生成控件数量
                int lint_TopCount = 0;//当前Top有几行
                isLeftInto = true;


                for (int a = 0; a < m_tblDataList.Rows.Count; a++)
                {

                    m_Line = new LineControl(m_tblDataList.Rows[a]["myTeamName"].ToString(), strparDate, m_tblDataList.Rows[a]["ShowColor"].ToString());
                    m_Line.Name = ("Line" + a).ToString();
                    count++;
                    _point = new Point(lint_Left + 2, lint_Top);
                    m_Line.Location = _point;
                    lint_Left += m_Line.Width+1;

                    if (this.Width - lint_Left < 0)
                    {
                        lint_Top += m_Line.Height + 1;
                    }
                    panelContent.Controls.Add(m_Line);
                }
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        #endregion 

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateOperDate1_EditValueChanged(object sender, EventArgs e)
        {
            strparDate = dateOperDate1.EditValue.ToString();
        }
    }
}
