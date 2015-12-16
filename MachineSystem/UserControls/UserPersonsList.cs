using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MachineSystem.UserControls
{
    public partial class UserPersonsList : UserControl
    {
        #region 变量定义

        private string m_TitleGuanwei = string.Empty;
        /// <summary>
        /// 关位
        /// </summary>
        public string TitleGuanwei
        {
            get { return m_TitleGuanwei; }
            set
            {
                m_TitleGuanwei = value;
                lblTitleName.Text = m_TitleGuanwei;
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

        private int m_StandardCount = 0;
        /// <summary>
        /// 标配人数
        /// </summary>
        public int StandardCount
        {
            get { return m_StandardCount; }
            set
            {
                m_StandardCount = value;
                lblStandardCount.Text = m_StandardCount.ToString();
            }
        }

        private int m_RealCount = 0;
        /// <summary>
        /// 实配人数
        /// </summary>
        public int RealCount
        {
            get { return m_RealCount; }
            set
            {
                m_RealCount = value;
                lblRealCount.Text = m_RealCount.ToString();
            }
        }

        #endregion

        public delegate void AllEvent(object sender, EventArgs e);
        public event AllEvent AllEventClick;

        public UserPersonsList()
        {
            InitializeComponent();
        }

        private void UserPersonsList_Click(object sender, EventArgs e)
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
    }
}
