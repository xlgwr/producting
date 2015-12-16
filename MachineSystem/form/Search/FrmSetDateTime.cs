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
    public partial class FrmSetDateTime : Framework.Abstract.frmBaseXC
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
        public FrmSetDateTime()
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
                txtYear.Text = DateTime.Now.Year.ToString();
                txtMonth.Text = DateTime.Now.Month.ToString();
                txtDay.Text = DateTime.Now.Day.ToString();
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
        //年++按钮按下 不断++
        private void btnAddYear_MouseDown(object sender, MouseEventArgs e)
        {
            timerYearAdd.Enabled = true;
            timerYearAdd.Tick += new EventHandler(btnAddYear_Click);
            timerYearAdd.Interval = sleepCnt;
            timerYearAdd.Start();
        }
        //年++按钮放开
        private void btnAddYear_MouseUp(object sender, MouseEventArgs e)
        {
            timerYearAdd.Stop();
            timerYearAdd.Enabled = false;
        }
        //年--按钮按下 不断--
        private void btnMinusYear_MouseDown(object sender, MouseEventArgs e)
        {
            timerYearMins.Enabled = true;
            timerYearMins.Tick += new EventHandler(btnMinusYear_Click);
            timerYearMins.Interval = sleepCnt;
            timerYearMins.Start();
        }
        //年--按钮放开 停止
        private void btnMinusYear_MouseUp(object sender, MouseEventArgs e)
        {
            timerYearMins.Stop();
            timerYearMins.Enabled = false;
        }


        //月++按钮按下 不断++
        private void btnAddMonth_MouseDown(object sender, MouseEventArgs e)
        {
            timerMonthAdd.Enabled = true;
            timerMonthAdd.Tick += new EventHandler(btnAddMonth_Click);
            timerMonthAdd.Interval = sleepCnt;
            timerMonthAdd.Start();
        }
        //月++按钮放开
        private void btnAddMonth_MouseUp(object sender, MouseEventArgs e)
        {
            timerMonthAdd.Stop();
            timerMonthAdd.Enabled = false;
        }
        //月--按钮按下 不断--
        private void btnMinusMonth_MouseDown(object sender, MouseEventArgs e)
        {
            timerMonthMins.Enabled = true;
            timerMonthMins.Tick += new EventHandler(btnMinusMonth_Click);
            timerMonthMins.Interval = sleepCnt;
            timerMonthMins.Start();
        }
        //月--按钮放开 停止
        private void btnMinusMonth_MouseUp(object sender, MouseEventArgs e)
        {
            timerMonthMins.Stop();
            timerMonthMins.Enabled = false;
        }

        //日++按钮按下 不断++
        private void btnAddDay_MouseDown(object sender, MouseEventArgs e)
        {
            timerDayAdd.Enabled = true;
            timerDayAdd.Tick += new EventHandler(btnAddDay_Click);
            timerDayAdd.Interval = sleepCnt;
            timerDayAdd.Start();
        }
        //日++按钮放开
        private void btnAddDay_MouseUp(object sender, MouseEventArgs e)
        {
            timerDayAdd.Stop();
            timerDayAdd.Enabled = false;
        }
        //日--按钮按下 不断--
        private void btnMinusDay_MouseDown(object sender, MouseEventArgs e)
        {
            timerDayMins.Enabled = true;
            timerDayMins.Tick += new EventHandler(btnMinusDay_Click);
            timerDayMins.Interval = sleepCnt;
            timerDayMins.Start();
        }
        //日--按钮放开 停止
        private void btnMinusDay_MouseUp(object sender, MouseEventArgs e)
        {
            timerDayMins.Stop();
            timerDayMins.Enabled = false;
        }

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
        /// 年自动+1
        /// </summary>
        private void btnAddYear_Click(object sender, EventArgs e)
        {
            dt_DateTime = dt_DateTime.AddYears(1);
            txtYear.Text = dt_DateTime.Year.ToString();
        }

        /// <summary>
        /// 年自动-1
        /// </summary>
        private void btnMinusYear_Click(object sender, EventArgs e)
        {
            dt_DateTime = dt_DateTime.AddYears(-1);
            txtYear.Text = dt_DateTime.Year.ToString();
        }

        /// <summary>
        /// 月自动+1
        /// </summary>
        private void btnAddMonth_Click(object sender, EventArgs e)
        {
            dt_DateTime = dt_DateTime.AddMonths(1);
            txtMonth.Text = dt_DateTime.Month.ToString();
            txtYear.Text = dt_DateTime.Year.ToString();
        }
        /// <summary>
        /// 月自动-1
        /// </summary>
        private void btnMinusMonth_Click(object sender, EventArgs e)
        {
            dt_DateTime = dt_DateTime.AddMonths(-1);
            txtMonth.Text = dt_DateTime.Month.ToString();
            txtYear.Text = dt_DateTime.Year.ToString();
        }

        /// <summary>
        /// 日自动+1
        /// </summary>
        private void btnAddDay_Click(object sender, EventArgs e)
        {
            dt_DateTime = dt_DateTime.AddDays(1);
            txtDay.Text = dt_DateTime.Day.ToString();
            txtMonth.Text = dt_DateTime.Month.ToString();
            txtYear.Text = dt_DateTime.Year.ToString();
        }
        /// <summary>
        /// 日自动-1
        /// </summary>
        private void btnMinusDay_Click(object sender, EventArgs e)
        {
            dt_DateTime = dt_DateTime.AddDays(-1);
            txtDay.Text = dt_DateTime.Day.ToString();
            txtMonth.Text = dt_DateTime.Month.ToString();
            txtYear.Text = dt_DateTime.Year.ToString();
        }

        /// <summary>
        /// 小时自动+1
        /// </summary>
        private void btnAddHour_Click(object sender, EventArgs e)
        {
            dt_DateTime = dt_DateTime.AddHours(1);
            txtHour.Text = dt_DateTime.Hour.ToString();
            txtDay.Text = dt_DateTime.Day.ToString();
            txtMonth.Text = dt_DateTime.Month.ToString();
            txtYear.Text = dt_DateTime.Year.ToString();
        }
        /// <summary>
        /// 小时自动-1
        /// </summary>
        private void btnMinusHour_Click(object sender, EventArgs e)
        {
            dt_DateTime = dt_DateTime.AddHours(-1);
            txtHour.Text = dt_DateTime.Hour.ToString();
            txtDay.Text = dt_DateTime.Day.ToString();
            txtMonth.Text = dt_DateTime.Month.ToString();
            txtYear.Text = dt_DateTime.Year.ToString();
        }

        /// <summary>
        /// 分钟自动+1
        /// </summary>
        private void btnAddSecond_Click(object sender, EventArgs e)
        {
            dt_DateTime = dt_DateTime.AddMinutes(1);
            txtSecond.Text = dt_DateTime.Minute.ToString();
            txtHour.Text = dt_DateTime.Hour.ToString();
            txtDay.Text = dt_DateTime.Day.ToString();
            txtMonth.Text = dt_DateTime.Month.ToString();
            txtYear.Text = dt_DateTime.Year.ToString();
        }

        /// <summary>
        /// 分钟自动-1
        /// </summary>
        private void btnMinusSecond_Click(object sender, EventArgs e)
        {
            dt_DateTime = dt_DateTime.AddMinutes(-1);
            txtSecond.Text = dt_DateTime.Minute.ToString();
            txtHour.Text = dt_DateTime.Hour.ToString();
            txtDay.Text = dt_DateTime.Day.ToString();
            txtMonth.Text = dt_DateTime.Month.ToString();
            txtYear.Text = dt_DateTime.Year.ToString();
        }

        /// <summary>
        /// 确定
        /// </summary>
        private void btnEnter_Click(object sender, EventArgs e)
        {
            string str = txtYear.Text.Trim() + "-" + txtMonth.Text.Trim() + "-" + txtDay.Text.Trim() + " " + txtHour.Text.Trim()+":"+txtSecond.Text.Trim() ;
            if (!IsDate(str))
            {
                DataValid.ShowErrorInfo(this.ErrorInfo, this.txtDay, "时间格式不正确!");
                return;
            }

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
