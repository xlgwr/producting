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
    public partial class FrmAttendDialog : Framework.Abstract.frmBaseXC
    {
        #region 变量定义
        #endregion

        #region 画面初始化

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmAttendDialog(string msgTxt)
        {
           
            InitializeComponent();
            lbltxt.Text = msgTxt;
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
               
                
            }
            catch (Exception ex)
            {

               XtraMsgBox.Show("画面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }
        #endregion

        #region 画面事件处理
        

        /// <summary>
        /// 确定按钮
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion

       

    }
}
