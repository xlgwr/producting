using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Framework.Abstract
{
    public partial class ExcelMsg : Form
    {
        public ExcelMsg()
        {
            InitializeComponent();
        }

        private static ExcelMsg m_ExcelMsg;

        public static ExcelMsg ExcelMsgInfo
        {
            get
            {
                if (m_ExcelMsg == null)
                {
                    m_ExcelMsg = new ExcelMsg();
                }

                return m_ExcelMsg;
            }
        }

        /// <summary>
        /// 文件保存位置//
        /// </summary>
        private string m_FilePath = "";

        /// <summary>
        /// 画面弹出
        /// </summary>
        /// <param name="TextInfo">画面名称</param>
        /// <param name="MsgInfo">提示信息</param>
        /// <param name="Path">文件路径</param>
        public void Show(string TextInfo, string MsgInfo, string Path)
        {
            try
            {

                this.Text = TextInfo;
                this.lblMsgInfo.Text = MsgInfo;

                this.m_FilePath = Path;

                this.ShowDialog();
            }
            catch (Exception)
            {


            }
        }


        private void frmExcelMsg_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 打开文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnfolder_Click(object sender, EventArgs e)
        {
            try
            {
                string[] path=m_FilePath.Split('\\');

                string paths = path[ path.Length - 1];

                Openfolder(m_FilePath.Replace(paths,""));
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnfile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFile(m_FilePath);
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        /// <summary>
        /// 打开文件夹//
        /// </summary>
        /// <param name="fileName">文件名</param>
        public static void Openfolder(string fileName)
        {
            try
            {
                System.Diagnostics.Process.Start(fileName);
            }
            catch
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Cannot find an application on your system suitable for openning the file with exported data.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// 打开Excel文档对话框
        /// </summary>
        /// <param name="fileName">文件名</param>
        public static void OpenFile(string fileName)
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = fileName;
                process.StartInfo.Verb = "Open";
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                process.Start();
            }
            catch
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Cannot find an application on your system suitable for openning the file with exported data.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 关闭画面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception)
            {
                
              
            }

        }
    }
}
