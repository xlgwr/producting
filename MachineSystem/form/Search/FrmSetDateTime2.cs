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
using System.Text.RegularExpressions;

namespace MachineSystem.form.Search
{
    public partial class FrmSetDateTime2 : Framework.Abstract.frmBaseXC
    {
        #region 变量定义

        public string m_DateTime = string.Empty;

        DateTime dt_DateTime = new DateTime();
        /// <summary>
        /// 自动刷新timer
        /// </summary>
        Timer timerYearAdd = new Timer();
        Timer timerYearMins = new Timer();
        Timer timerMonthAdd = new Timer();
        Timer timerMonthMins = new Timer();
        Timer timerDayAdd = new Timer();
        Timer timerDayMins = new Timer();
        Timer timerHourAdd = new Timer();
        Timer timerHourMins = new Timer();
        Timer timerSecondAdd = new Timer();
        Timer timerSecondMins = new Timer();
        /// <summary>
        /// 自动增加、减少延迟毫秒数
        /// </summary>
        int sleepCnt = 1200;
        #endregion

        #region 画面初始化

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmSetDateTime2()
        {
            InitializeComponent();

            SetFormValue();

            this.TopMost = true;

        }
        
        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                txtHour.Text = DateTime.Now.Hour.ToString();
                txtSecond.Text = DateTime.Now.Minute.ToString();
                dt_DateTime = DateTime.Now;

            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("画面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }
        #endregion

        #region 画面事件处理      

        //小时++按钮按下 不断++
        private void btnAddHour_MouseDown(object sender, MouseEventArgs e)
        {
            timerHourAdd.Enabled = true;
            timerHourAdd.Tick += new EventHandler(btnAddHour_Click);
            timerHourAdd.Interval = sleepCnt;
            timerHourAdd.Start();
        }
        //小时++按钮放开
        private void btnAddHour_MouseUp(object sender, MouseEventArgs e)
        {
            timerHourAdd.Stop();
            timerHourAdd.Enabled = false;
        }
        //小时--按钮按下 不断--
        private void btnMinusHour_MouseDown(object sender, MouseEventArgs e)
        {
            timerHourMins.Enabled = true;
            timerHourMins.Tick += new EventHandler(btnMinusHour_Click);
            timerHourMins.Interval = sleepCnt;
            timerHourMins.Start();
        }
        //小时--按钮放开 停止
        private void btnMinusHour_MouseUp(object sender, MouseEventArgs e)
        {
            timerHourMins.Stop();
            timerHourMins.Enabled = false;
        }

        //分钟++按钮按下 不断++
        private void btnAddSecond_MouseDown(object sender, MouseEventArgs e)
        {
            timerSecondAdd.Enabled = true;
            timerSecondAdd.Tick += new EventHandler(btnAddSecond_Click);
            timerSecondAdd.Interval = sleepCnt;
            timerSecondAdd.Start();
        }
        //分钟++按钮放开
        private void btnAddSecond_MouseUp(object sender, MouseEventArgs e)
        {
            timerSecondAdd.Stop();
            timerSecondAdd.Enabled = false;
        }
        //分钟--按钮按下 不断--
        private void btnMinusSecond_MouseDown(object sender, MouseEventArgs e)
        {
            timerSecondMins.Enabled = true;
            timerSecondMins.Tick += new EventHandler(btnMinusSecond_Click);
            timerSecondMins.Interval = sleepCnt;
            timerSecondMins.Start();
        }
        //分钟--按钮放开 停止
        private void btnMinusSecond_MouseUp(object sender, MouseEventArgs e)
        {
            timerSecondMins.Stop();
            timerSecondMins.Enabled = false;
        }
        
        /// <summary>
        /// 小时自动+1
        /// </summary>
        private void btnAddHour_Click(object sender, EventArgs e)
        {

            if (int.Parse(txtHour.Text.Trim()) <= 98)
            {
                txtHour.Text = (int.Parse(txtHour.Text.Trim()) + 1).ToString();
            }
        }
        /// <summary>
        /// 小时自动-1
        /// </summary>
        private void btnMinusHour_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtHour.Text.Trim()) > 0)
            {
                txtHour.Text = (int.Parse(txtHour.Text.Trim()) - 1).ToString();
            }
        }

        /// <summary>
        /// 分钟自动+1
        /// </summary>
        private void btnAddSecond_Click(object sender, EventArgs e)
        {

            if (int.Parse(txtSecond.Text.Trim()) <= 98)
            {
                txtSecond.Text = (int.Parse(txtSecond.Text.Trim()) + 1).ToString();
            }
        }

        /// <summary>
        /// 分钟自动-1
        /// </summary>
        private void btnMinusSecond_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtSecond.Text.Trim()) > 0)
            {
                txtSecond.Text = (int.Parse(txtSecond.Text.Trim()) - 1).ToString();
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (txtHour.Text.Length==1)
            {
                txtHour.Text = "0" + txtHour.Text;
            }
            if (txtSecond.Text.Length == 1)
            {
                txtSecond.Text = "0" + txtSecond.Text;
            }
            string str = txtHour.Text.Trim()+":"+txtSecond.Text.Trim() ; 
            m_DateTime = str;
            this.DialogResult = DialogResult.OK;
            
            Close();
        }

        /// <summary>
        /// 取消
        /// </summary>
        private void btnCanel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion

        #region 共通处理方法
        /// <summary>
        /// 是否为日期型字符串
        /// </summary>
        /// <param name="StrSource">日期字符串(2008-05-08)</param>
        /// <returns></returns>
        public static bool IsDate(string StrSource)
        {
            try
            {
               DateTime date= DateTime.Parse(StrSource);
            }
            catch (Exception ex) 
            {
                return false;
            }
            return true;
        }


        #endregion


        

    }
}
