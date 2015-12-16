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
using System.Diagnostics;
using log4net;

namespace MachineSystem.form.Search
{
    public partial class frmMyTeamNameSearch : Framework.Abstract.frmBaseXC
    {
        #region 变量定义

        public string m_myTeamName = string.Empty;

        log4net.ILog log; 
        #endregion

        #region 画面初始化

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmMyTeamNameSearch()
        {
            log = LogManager.GetLogger("frmMyTeamNameSearch"); 

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
                Stopwatch watch = new Stopwatch();
                watch.Start();
                //向别-班别
                string str = string.Format(@"SELECT distinct myTeamName  FROM V_Attend_Move_i where 1=1");
                if (!Common._personid.Equals(Common._Administrator))
                {
                    str += " and  UserID ='" + Common._personid + "' ";
                }
                str += @" AND MoveStatus in ('人员调入','关位调整') 
                                    AND CONVERT(VARCHAR(10), EndDate,120) ='4000-01-01'
                                    and myTeamName is not null "; 

                DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str);
                gridControl1.DataSource = dt_temp;
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("画面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        #endregion


        /// <summary>
        /// 选择行
        /// </summary>
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (e.Column.Caption == "选择")
            {
                DataRow row = gridView1.GetFocusedDataRow();
                m_myTeamName = row["myteamName"].ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
