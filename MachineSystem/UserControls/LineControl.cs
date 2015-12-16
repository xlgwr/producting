using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MachineSystem.TabPage;
using Framework.Libs;
using Framework.DataAccess;
using MachineSystem.form.Pad;

namespace MachineSystem.UserControls
{
    public partial class LineControl : UserControl
    {
        #region 变量定义

        Framework.DataAccess.daoCommon daocomon = new daoCommon();
        #endregion

        #region 画面初始化
        /// <summary>
        /// 颜色设置
        /// </summary>
        /// <param name="strMyTeamName"></param>
        /// <param name="strSearchDate"></param>
        /// <param name="backColor"></param>
        public LineControl(string strMyTeamName,string strSearchDate,string backColor)
        {
            InitializeComponent();

            lblTitle.Text = strMyTeamName;
            lblSearchDate.Text = strSearchDate;
            lblSearchDate.Visible = false;
            //颜色
            if (backColor == "yellow")
                lblTitle.BackColor = Color.Yellow;
            else if (backColor == "green")
                lblTitle.BackColor = Color.Green;
            else if (backColor == "grey")
                lblTitle.BackColor = Color.Gray;
            else if (backColor == "red")
                lblTitle.BackColor = Color.Red;
        }
        #endregion

        #region 事件处理方法
        //图表
        private void linkLabelChar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //更具myTeamName 获得其他参数 
             string str_sql = string.Format("select top 1 * from  v_Produce_para_i where myTeamName in ('" + lblTitle.Text.Trim() + "')");
             DataTable dt_temp = daocomon.GetTableInfoBySqlNoWhere(str_sql);

                if (dt_temp.Rows.Count > 0)
                {
                   string strJobForID = dt_temp.Rows[0]["JobForID"].ToString();
                   string strProjectID = dt_temp.Rows[0]["ProjectID"].ToString();
                   string  strTeamID = dt_temp.Rows[0]["TeamID"].ToString();

                   frmDataRevealSummaryCharShow frm = new frmDataRevealSummaryCharShow(lblSearchDate.Text, lblSearchDate.Text, strJobForID, strProjectID, strTeamID);
                   frm.Show();

                }
        }
        //关位
        private void linkLabelGuanwei_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmProduce_UserTotalShow frm = new frmProduce_UserTotalShow("","");
            frm.Show();
        }
        #endregion
    }
}
