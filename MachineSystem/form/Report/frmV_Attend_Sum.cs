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
using MachineSystem.form.Pad;

namespace MachineSystem.TabPage
{
    public partial class frmV_Attend_Sum :Framework.Abstract.frmSearchBasic2
    {
        #region 画面初始化
        public frmV_Attend_Sum()
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
               
            }
           catch (Exception ex)
            {

                XtraMsgBox.Show("画面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }
        #endregion

        #region 事件处理方法

        private void btnLineExChange_Click(object sender, EventArgs e)
        {

            frmV_Attend_Line frm = new frmV_Attend_Line(dateOperDate1.EditValue.ToString());
            
            frm.Show();
        }

        private void btnAttendDetail_Click(object sender, EventArgs e)
        {
            //显示制造部Line出勤状况明细
            frmV_Attend_Line_Detail frm = new frmV_Attend_Line_Detail(dateOperDate1.EditValue.ToString());
          
            frm.Show();
        }

        //日期变更
        private void dateOperDate1_EditValueChanged(object sender, EventArgs e)
        {

            try
            {
                GetDspDataList();

                
            }
            catch (Exception ex)
            {
               XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
        
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
                 string _sql = "select * from V_Attend_Sum where AttendDate='";

                DataTable _dt;
           
                if (dateOperDate1.EditValue == null || string.IsNullOrEmpty(dateOperDate1.EditValue.ToString())) return;
                _sql += DateTime.Parse(dateOperDate1.EditValue.ToString()).ToString("yyyy-MM-dd") + "'";

                if (Common._personid != Common._Administrator)
                {// &&Common._myTeamName != "" && Common._myTeamName != null
                    _sql += " and myTeamName='" + Common._myTeamName + "'";
                }
                _dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(_sql);
                if (_dt.Rows.Count>0)
                {
                    //请假
                    lblVacateCnt.Text = _dt.Rows[0]["VacateCnt"].ToString();
                    //在籍
                    lblInCnt.Text = _dt.Rows[0]["InCnt"].ToString();
                    //出勤
                    lblAttendCnt.Text = _dt.Rows[0]["AttendCnt"].ToString();
                    //已替关
                    lblAlredyReplaceJobsCnt.Text = _dt.Rows[0]["AlredyReplaceJobsCnt"].ToString();
                    //替关
                    lblReplaceJobsCnt.Text = _dt.Rows[0]["ReplaceJobsCnt"].ToString();
                   //未替关
                    lblEnReplaceJobsCnt.Text = _dt.Rows[0]["EnReplaceJobsCnt"].ToString();
                    //
                    //出勤率
                    lblAttendRate.Text = _dt.Rows[0]["OutVaul"].ToString() + "%";

    

                }
                else
                {
                    //请假
                    lblVacateCnt.Text = "0";
                    //在籍
                    lblInCnt.Text = "0";
                    //出勤
                    lblAttendCnt.Text = "0";
                    //已替关
                    lblAlredyReplaceJobsCnt.Text = "0";
                    //替关
                    lblReplaceJobsCnt.Text = "0";
                    //未替关
                    lblEnReplaceJobsCnt.Text = "0";
                    lblAttendRate.Text = "0%";
                }

            }
            catch (Exception ex)
            {
                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        #endregion 

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
