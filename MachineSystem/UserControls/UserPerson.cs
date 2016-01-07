using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MachineSystem.SysDefine;
using System.IO;

namespace MachineSystem.UserControls
{
    public partial class UserPerson : UserControl
    {

        #region 变量定义
        /// <summary>
        /// 标题名字
        /// </summary>
        private string m_TitleName = string.Empty;
        /// <summary>
        /// 标题名字
        /// </summary>
        public string TitleName
        {
            get { return m_TitleName; }
            set
            {
                m_TitleName = value;
                lblTitle.Text = m_TitleName;

                if (m_TitleName.Contains("替"))
                {
                    userPicture1.bgColor = Color.SkyBlue;
                }
            }
        }

        /// <summary>
        /// 关位ID
        /// </summary>
        private string m_GuanweiID = string.Empty;
        /// <summary>
        /// 关位ID
        /// </summary>
        public string GuanweiID
        {
            get { return m_GuanweiID; }
            set
            {
                m_GuanweiID = value;
            }
        }


        private string m_GuanweiSite = string.Empty;
        /// <summary>
        /// 关位位置
        /// </summary>
        public string GuanweiSite
        {
            get { return m_GuanweiSite; }
            set
            {
                m_GuanweiSite = value;
            }
        }

        private string m_Guanweicolor = string.Empty;
        /// <summary>
        /// 关位颜色
        /// </summary>
        public string GuanweiColor
        {
            get { return m_Guanweicolor; }
            set
            {
                m_Guanweicolor = value;
                Color col = Color.FromName(m_Guanweicolor);
                lblTitle.BackColor = col;
            }
        }

        /// <summary>
        /// 关位名称
        /// </summary>
        private string m_GuanweiNM = string.Empty;
        /// <summary>
        /// 关位关位名称
        /// </summary>
        public string GuanweiNM
        {
            get { return m_GuanweiNM; }
            set
            {
                m_GuanweiNM = value;
                if (m_GuanweiNM == "TPO" || m_GuanweiNM == "中检" || m_GuanweiNM == "中间检查" || m_GuanweiNM == "出荷" || m_GuanweiNM == "出荷检查")
                {
                    userPicture1.bgColor = Color.Orange;
                }
                else if (m_GuanweiNM == "电线准备" || m_GuanweiNM == "部品搬运" || m_GuanweiNM == "A/B搬运" || m_GuanweiNM == "熔着配套")
                {
                    userPicture1.bgColor = Color.Moccasin;
                }
                else if (m_GuanweiNM == "LQC")
                {
                    userPicture1.bgColor = Color.LightGreen;
                }
                else if (m_GuanweiNM.Contains("替关"))
                {
                    userPicture1.bgColor = Color.SkyBlue;
                }
                else if (m_TitleName.Contains("替"))
                {
                    userPicture1.bgColor = Color.SkyBlue;
                }
                else
                {
                    userPicture1.bgColor = Color.Black;
                }
            }
        }


        private string m_Status = string.Empty;
        /// <summary>
        /// 状态
        /// </summary>
        public string Status
        {
            get { return m_Status; }
            set
            {
                m_Status = value;
                lblStatus.Text = m_Status;

                if (m_Status == "新人作业")
                {
                    userPicture1.bgColor = Color.MediumSpringGreen;
                }
                else if (m_Status == "指导者")
                {
                    userPicture1.bgColor = Color.MediumBlue;
                }
                else if (m_GuanweiNM == "")
                {
                    userPicture1.bgColor = Color.Black;
                }
            }
        }

        private string m_StatusColor = string.Empty;
        /// <summary>
        /// 状态颜色
        /// </summary>
        public string StatusColor
        {
            get { return m_StatusColor; }
            set
            {
                m_StatusColor = value;
                Color col = Color.FromName(m_StatusColor);
                lblStatus.BackColor = col;
            }
        }

        private string m_Time = string.Empty;
        /// <summary>
        /// 打卡时间
        /// </summary>
        public string Time
        {
            get { return m_Time; }
            set
            {
                m_Time = value;
                lblTime.Text = m_Time;
            }
        }

        private string m_TimeColor = string.Empty;
        /// <summary>
        /// 打卡时间颜色
        /// </summary>
        public string TimeColor
        {
            get { return m_TimeColor; }
            set
            {
                m_TimeColor = value;
                Color col = Color.FromName(m_TimeColor);
                lblTime.BackColor = col;
            }
        }

        private string m_Remind = string.Empty;
        /// <summary>
        /// 提醒
        /// </summary>
        public string Remind
        {
            get { return m_Remind; }
            set
            {
                m_Remind = value;
                if (m_Remind.Length > 0)
                {
                    if (m_Remind.Contains("假"))
                    {
                        lblRemind.Text = m_Remind.Replace("-", "");
                    }
                    else
                    {
                        lblRemind.Text = m_Remind.Substring(2).Replace("-", "");
                    }

                }
                else
                {
                    lblRemind.Text = m_Remind;
                }

            }
        }

        private string m_JobForID = string.Empty;
        /// <summary>
        /// 向别
        /// </summary>
        public string JobForID
        {

            get { return m_JobForID; }
            set
            {
                m_JobForID = value;
            }
        }

        private string m_ProjectID = string.Empty;
        /// <summary>
        /// 工程别
        /// </summary>
        public string ProjectID
        {

            get { return m_ProjectID; }
            set
            {
                m_ProjectID = value;
            }
        }


        private string m_LineID = string.Empty;
        /// <summary>
        /// LineID
        /// </summary>
        public string LineID
        {

            get { return m_LineID; }
            set
            {
                m_LineID = value;
            }
        }


        private string m_RemindColor = string.Empty;
        /// <summary>
        /// 提醒颜色
        /// </summary>
        public string RemindColor
        {
            get { return m_RemindColor; }
            set
            {
                m_RemindColor = value;
                Color col = Color.FromName(m_RemindColor);
                lblRemind.BackColor = col;
            }
        }

        private string m_LicenseType = string.Empty;
        /// <summary>
        /// 免许类型
        /// </summary>
        public string LicenseType
        {
            get { return m_LicenseType; }
            set
            {
                m_LicenseType = value;
                lblType.Text = m_LicenseType;
            }
        }

        private string m_LicenseColor = string.Empty;
        /// <summary>
        /// 免许类型颜色
        /// </summary>
        public string LicenseColor
        {
            get { return m_LicenseColor; }
            set
            {
                m_LicenseColor = value;
                Color col = Color.FromName(m_LicenseColor);
                lblType.BackColor = col;
            }
        }

        private string m_UserID = string.Empty;
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserID
        {
            get { return m_UserID; }
            set
            {
                m_UserID = value;
                lblUserID.Text = m_UserID;
            }
        }

        private string m_UserName = string.Empty;
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            get { return m_UserName; }
            set
            {
                m_UserName = value;
                lblUserName.Text = m_UserName;
            }
        }
        private string m_UserIdNmColor = string.Empty;

        /// <summary>
        /// 用户颜色
        /// </summary>
        public string UserIdNmColor
        {
            get { return m_Guanweicolor; }
            set
            {
                m_UserIdNmColor = value;
                Color col = Color.FromName(m_UserIdNmColor);
                lblUserName.BackColor = col;
                lblUserID.BackColor = col;
            }
        }

        private string m_ImageUrl = string.Empty;

        /// <summary>
        /// 头像路径
        /// </summary>
        public string ImageUrl
        {
            get { return m_ImageUrl; }
            set
            {
                m_ImageUrl = value;
                userPicture1.ImageUrl = m_ImageUrl;
            }
        }


        #endregion

        public delegate void AllEvent(object sender, EventArgs e);
        public event AllEvent AllEventClick;

        public delegate void DoubleEvent(object sender, EventArgs e);
        public event DoubleEvent DoubleClick;

        public UserPerson()
        {

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);//防止窗口跳动
            SetStyle(ControlStyles.DoubleBuffer, true); //防止控件跳动 
            InitializeComponent();

        }

        //点击事件
        private void UserPerson_Click(object sender, EventArgs e)
        {
            try
            {
                if (AllEventClick != null)
                {
                    AllEventClick(this, e);
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 双击事件
        /// </summary>
        private void lblType_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (DoubleClick != null)
                {
                    DoubleClick(this, e);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void userPicture1_DoubleClick(object sender, EventArgs e)
        {

            try
            {
                if (DoubleClick != null)
                {
                    DoubleClick(this, e);
                }
            }
            catch (Exception ex)
            {

            }
        }



    }
}
