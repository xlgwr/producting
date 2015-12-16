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

namespace MachineSystem.form.Pad
{
    public partial class frmV_Attend_Line :Framework.Abstract.frmBaseXC
    {
        #region 画面初始化
        private string m_CurrentTime;//记录日期
        private static readonly ILog log = LogManager.GetLogger(typeof(frmV_Attend_Line));
       
        public frmV_Attend_Line(string pardate)
        {
            InitializeComponent();
            
            DateTime dtpar = DateTime.Parse(pardate);
            m_CurrentTime = dtpar.ToString("yyyy-MM-dd");
            lblShowDate.Text = dtpar.ToLongDateString();
            ShowAttendSumData("1");
        }
        #endregion

        #region 共同方法
        //取得全部数据：aflag：全部1，白班2，晚班3
        private void ShowAttendSumData(String aflag)
        {
            try
            {
                SetTextClear();
                string str_sql = " select AttendDate,";
	            str_sql += "              sum(SumLineCnt) as SumLineCnt,";//line总数
                str_sql += "              sum(SelfLineCnt) as SelfLineCnt, ";//自line对应
                str_sql += "              sum(SupportLineCnt) as SupportLineCnt,";//其他line支援
                str_sql += "              sum(AbnormalLineCnt) as AbnormalLineCnt ,";//异常line
                str_sql += "              COUNT(*) as LineCount ";
                str_sql += "      from     V_Attend_Line_Corre ";
                str_sql += "      where ";
	            str_sql += "              AttendDate ='" + m_CurrentTime + "'";
                switch (aflag)//aflag：全部1，白班2，晚班3
                {
                    case "1"://全部
                        {
                            str_sql += "              and (TeamName='A' OR TeamName='B')";
                           
                            this.btnAll.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
                            this.btnDay.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                            this.btnNight.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                            break;
                        }
                    case "2"://白班
                        {
                            str_sql += "              and TeamName='A'";
                            this.btnDay.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
                            this.btnAll.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                            this.btnNight.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                            break;
                        }
                    case "3"://晚班
                        {
                            str_sql += "              and TeamName='B'";
                            this.btnNight.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
                            this.btnDay.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                            this.btnAll.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
                            break;
                        }
                }

                str_sql += " group by   AttendDate";
                DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

                if (dt_temp.Rows.Count > 0)
                {
                    //lblSelfLineCnt.Text = dt_temp.Rows[0]["SelfLineCnt"].ToString() + "/" + dt_temp.Rows[0]["SumLineCnt"].ToString();
                    //lblSupportLineCnt.Text = dt_temp.Rows[0]["SupportLineCnt"].ToString() + "/" + dt_temp.Rows[0]["SumLineCnt"].ToString();
                    //lblAbnormalLineCnt.Text = dt_temp.Rows[0]["AbnormalLineCnt"].ToString() + "/" + dt_temp.Rows[0]["SumLineCnt"].ToString();
                    lblSelfLineCnt.Text = dt_temp.Rows[0]["SelfLineCnt"].ToString() + "/" + dt_temp.Rows[0]["LineCount"].ToString();
                    lblSupportLineCnt.Text = dt_temp.Rows[0]["SupportLineCnt"].ToString() + "/" + dt_temp.Rows[0]["LineCount"].ToString();
                    lblAbnormalLineCnt.Text = dt_temp.Rows[0]["AbnormalLineCnt"].ToString() + "/" + dt_temp.Rows[0]["LineCount"].ToString();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }
        #endregion

        #region 画面点击事件
        //白班
        private void btnDay_Click(object sender, EventArgs e)
        {
            ShowAttendSumData("2");
        }
        //晚上
        private void btnNight_Click(object sender, EventArgs e)
        {
            ShowAttendSumData("3");
        }
        //全部
        private void btnAll_Click(object sender, EventArgs e)
        {

            ShowAttendSumData("1");
            
        }
        private void SetTextClear()
        {
            lblSelfLineCnt.Text = "0";
            lblSupportLineCnt.Text = "0";
            lblAbnormalLineCnt.Text = "0";
        }
        #endregion

       
    }
}
