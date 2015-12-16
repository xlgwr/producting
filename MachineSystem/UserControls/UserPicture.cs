using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Framework.Libs;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using log4net;
using System.Reflection;
using System.Diagnostics;

namespace MachineSystem.UserControls
{
    public partial class UserPicture : UserControl
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public delegate void AllEvent(object sender, EventArgs e);
        public event AllEvent AllEventClick;

        public delegate void PictureEvent(object sender, EventArgs e);
        public event PictureEvent PictureEventClick;


        private Color m_bgColor;

        /// <summary>
        /// 边框颜色
        /// </summary>
        public Color bgColor
        {
            get { return m_bgColor; }
            set
            {
                m_bgColor = value;
                panel1.BackColor = m_bgColor;
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
                //autoLoadImage(value);
                ThreadPool.QueueUserWorkItem(autoLoadImage, value);
            }
        }

        private void autoLoadImage(object value)
        {
            Stopwatch _stopwatch = new Stopwatch();
            //logger.DebugFormat("***开始记时，文件名：{0}。", value);
            _stopwatch.Start();
            var tmpm_ImageUrl = value.ToString();

            if (string.IsNullOrEmpty(tmpm_ImageUrl))
            {
                this.pictureBox1.Image = MachineSystem.Properties.Resources._01;
                return;
            }
            else
            {
                m_ImageUrl = tmpm_ImageUrl;
            }

            try
            {
                if (File.Exists(tmpm_ImageUrl))
                {

                    var currPath = tmpm_ImageUrl;
                    if (!Program._dicCheckImage.ContainsKey(currPath))
                    {
                        FileInfo tmpfile = new FileInfo(currPath);
                        if (tmpfile.Length < 512)
                        {
                            File.Delete(currPath);
                        }
                        else
                        {
                            var tmpimage = (Image)Image.FromFile(currPath).Clone();
                            Program._dicCheckImage.Add(currPath, tmpimage);
                        }

                    }
                    //this.pictureBox1.ImageLocation = m_ImageUrl;

                    this.pictureBox1.Image = (Image)Program._dicCheckImage[currPath].Clone();

                    //_stopwatch.Stop();
                    //var msg = "使用时间:" + _stopwatch.Elapsed.ToString();
                    //logger.InfoFormat("***文件名存在：{0},{1}", tmpm_ImageUrl, msg);
                }
                else
                {
                    SetImage o = new SetImage() { nullPath = MachineSystem.Properties.Resources._01, userPicture = this.pictureBox1, strUserID = tmpm_ImageUrl, _stopwatch = _stopwatch };
                    ThreadPool.QueueUserWorkItem(initSetImage, o);
                }

            }
            catch (Exception ex)
            {
                this.pictureBox1.Image = MachineSystem.Properties.Resources._01;
                _stopwatch.Stop();
                var msg = "使用时间:" + _stopwatch.Elapsed.ToString();

                logger.InfoFormat("***加载计时: 用户名：{0}。{1}，Error:{2}.", tmpm_ImageUrl, msg, ex);
                //throw ex;
            }
        }


        void initSetImage(object setImage)
        {
            try
            {

                SetImage o = (SetImage)setImage;
                Task<string> t = new Task<string>(n => SetImageToPicture((string)n), o.strUserID);
                t.Start();
                t.Wait();
                var tmpresult = t.Result.ToString();
                try
                {
                    if (!string.IsNullOrEmpty(tmpresult))
                    {
                        var setImagePath = tmpresult;

                        var currPath = tmpresult;
                        if (!Program._dicCheckImage.ContainsKey(currPath))
                        {
                            FileInfo tmpfile = new FileInfo(currPath);
                            if (tmpfile.Length < 512)
                            {
                                File.Delete(currPath);
                            }
                            else
                            {
                                var tmpimage = Image.FromFile(currPath);
                                Program._dicCheckImage.Add(currPath, tmpimage);
                            }
                            
                        }
                        //this.pictureBox1.ImageLocation = m_ImageUrl;
                        //o.userPicture.ImageLocation = setImagePath;

                        o.userPicture.Image = Program._dicCheckImage[currPath];

                        o._stopwatch.Stop();
                        var msg = "使用时间:" + o._stopwatch.Elapsed.ToString();
                        logger.InfoFormat("***图片文件不存在，需要下载，用户名：{0}。{1}.", o.strUserID, msg);
                        return;
                    }
                    else
                    {
                        o.userPicture.Image = o.nullPath;

                        o._stopwatch.Stop();
                        var msg = "使用时间:" + o._stopwatch.Elapsed.ToString();
                        logger.InfoFormat("***图片文件不存在，需要下载,服务器不存在对应图片，用户名：{0}。{1}.", o.strUserID, msg);
                    }

                }
                catch (Exception ex)
                {
                    o.userPicture.Image = o.nullPath;
                    o._stopwatch.Stop();
                    var msg = "使用时间:" + o._stopwatch.Elapsed.ToString();
                    logger.InfoFormat("****Error.图片文件不存在，需要下载，用户名：{0}。{1}，Error:{2}.", o.strUserID, msg, ex);
                }

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("****Error.{0}", ex);
                //throw ex;
            }
        }
        /// <summary>
        /// set userid
        /// </summary>
        /// <param name="toUserID"></param>
        /// <returns></returns>
        public static string SetImageToPicture(object toUserID)
        {

            //考勤系统头像目录
            string AtPathDir = Application.StartupPath + "\\" + Common.AtPathDir;
            try
            {
                if (string.IsNullOrEmpty(Common.AtPathDir))
                {
                    Common.AtPathDir = @"USER_PIC\\";
                    AtPathDir = AtPathDir + Common.AtPathDir;
                }

                if (!Directory.Exists(AtPathDir))
                {
                    Directory.CreateDirectory(AtPathDir);
                }
                if (toUserID.ToString() == "")
                {
                    AtPathDir = AtPathDir + "01.png";
                    return AtPathDir;
                }

                var setImagePath = AtPathDir + toUserID.ToString() + ".jpg";
                if (!File.Exists(setImagePath))
                {
                    WebClient myWebClient = new WebClient();
                    string serverIp = Common.EmPathDir;
                    var getImagePath = serverIp + toUserID.ToString() + ".jpg";
                    myWebClient.DownloadFile(new Uri(getImagePath), setImagePath);
                }
                return setImagePath;
            }
            catch (Exception ex)
            {
                return "";
                //throw ex;
            }
        }
        public UserPicture()
        {
            InitializeComponent();
        }

        private void UserPicture_Click(object sender, EventArgs e)
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                if (PictureEventClick != null)
                {
                    PictureEventClick(this, e);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
    public class SetImage
    {

        public PictureBox userPicture { get; set; }
        public string strUserID { get; set; }
        public Image nullPath { get; set; }
        public Stopwatch _stopwatch { get; set; }
    }
}
