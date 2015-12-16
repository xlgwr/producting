using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Framework.Libs;

namespace MachineSystem
{
    public partial class frmSetting : DevExpress.XtraEditors.XtraForm
    {
        #region 初始化

        private SysRun _sysrun;
        public frmSetting()
        {
            InitializeComponent();
        }

        private void frmSetting_Load(object sender, EventArgs e)
        {
            //(初始化运行 frmSetting 需注释)
            this._sysrun = (SysRun)Serial.DeserializeBinary(Application.StartupPath + @"\" + Common._settingfilename);


            ////(初始化运行 frmSetting 可用)
            //this._sysrun = new SysRun();


            this.proList.SelectedObject = _sysrun;
        }

        #endregion

        #region 按钮事件

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                
                SetupParameter();
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ESICt Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        #endregion

        #region 参数更新


        //显示保存路径对话框

        private static string subShowSaveFileDialog(string title, string filter)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            string name = Application.ProductName;
            int n = name.LastIndexOf(".") + 1;
            if (n > 0) name = name.Substring(n, name.Length - n);
            //"Export To "
            dlg.Title = "select a file";
            dlg.FileName = name;
            dlg.Filter = filter;
            if (dlg.ShowDialog() == DialogResult.OK) return dlg.FileName;
            return "";
        }

        //序列化数据
        private void SetupParameter()
        {
            Serial.SerializeBinary(this._sysrun, Application.StartupPath + @"\" + Common._settingfilename);
            this._sysrun = null;
            MessageBox.Show("设定已保存!","信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Dispose();
        }


        #endregion

    }
}