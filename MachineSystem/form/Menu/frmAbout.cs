using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MachineSystem.SysDefine;
using Framework.Libs;

namespace MachineSystem.TabPage
{
    public partial class frmAbout : DevExpress.XtraEditors.XtraForm
    {
        public frmAbout()
        {
            InitializeComponent();

            lblVersion.Text = "V" + EnumDefine.VersionNos;
        }


        private void cmdOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
