using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework.Abstract;
using System.Collections;


namespace MachineSystem.TabPage
{
    public partial class frmUserDept : Framework.Abstract.frmSearchBasic2//frmBaseToolXC
    {
     

        #region 变量定义

        #endregion

        #region 画面初始化

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmUserDept()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                deptTreeList.OptionsBehavior.Editable = false;
                this.ListData.OptionsBehavior.Editable = false;

                setControlStatus();

                //ArrayList pList = new ArrayList();
                //pList.Add(new DeptNodeRecord() {  DeptId="A", DeptName ="根", DeptParentId ="" });
                //pList.Add(new DeptNodeRecord() { DeptId = "B", DeptName = "B", DeptParentId = "A" });
                //pList.Add(new DeptNodeRecord() { DeptId = "B1", DeptName = "B1", DeptParentId = "B" });
                //pList.Add(new DeptNodeRecord() { DeptId = "B2", DeptName = "B2", DeptParentId = "B" });
                //pList.Add(new DeptNodeRecord() { DeptId = "B3", DeptName = "B3", DeptParentId = "B" });
                //pList.Add(new DeptNodeRecord() { DeptId = "C", DeptName = "C", DeptParentId = "B" });
                //pList.Add(new DeptNodeRecord() { DeptId = "D", DeptName = "C", DeptParentId = "C" });
                //deptTreeList.DataSource = pList;

                loadTree();
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("画面初始化失败！"+ ex.Message , this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 新增数据功能处理
        /// </summary>
        protected override void SetInsertInit(bool isClear)
        {
            base.SetInsertInit(isClear);
            //frmOperatorManage frm = new frmOperatorManage();
            //frm.ShowDialog();
        }


        public override void SetModifyInit()
        {
            base.SetModifyInit();
        }
        #endregion

        protected override void SetSearchProc(frmSearchBasic2 frmBaseToolXC)
        {
            
           // base.SetSearchProc(frmBaseToolXC);
            //XtraMsgBox.Show("Search！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            searchByConditions();
        }

        private void loadTree()
        {
            string sql = "select * from V_User_Dept_ALL";
            DataTable dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(sql);
            this.deptTreeList.DataSource = dt;
            //gridControl1.DataSource = dt;
        }

        private void searchDeptChildren(string fatherid)
        {
            string sql = "select * from V_User_Dept_ALL where fatherid='{0}'";
            sql = string.Format(sql, fatherid);
            DataTable dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(sql);
            gridControlChildren.DataSource = dt;
        }

        private void searchByConditions()
        {
            deptTreeList.Selection.Clear();
            gridControlChildren.DataSource = null;

            StringBuilder sb = new StringBuilder("select * from V_User_Dept_ALL where 1=1 ");
            if (!string.IsNullOrEmpty(this.txtDeptName.Text))
            {
                sb.AppendFormat(" AND pName LIKE '%{0}%'", this.txtDeptName.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtID.Text))
            {
                sb.AppendFormat(" AND id = '{0}'", this.txtID.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtID2.Text))
            {
                sb.AppendFormat(" AND id2 = '{0}'", this.txtID2.Text.Trim());
            }
            DataTable dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(sb.ToString ());
            gridControlChildren.DataSource = dt;


        }

        private void deptTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            gridControlChildren.DataSource = null;
            object obj = e.Node.GetValue("id");
            if (obj != null)
            {
                searchDeptChildren(obj.ToString());
            }
           
        }

        private void setControlStatus()
        {
            //tool button 
            this.CancelButtonEnabled = false;
            this.CancelButtonVisibility = false;
            this.CopyAddEnabled = false;
            this.CopyAddVisibility = false;
            this.DeleteButtonEnabled = false;
            this.DeleteButtonVisibility = false;
            this.EditButtonEnabled = false;
            this.EditButtonVisibility = false;
            this.ExcelButtonEnabled = false;
            this.ExcelButtonVisibility = false;
            this.ExitButtonEnabled = true;
            this.ExitButtonVisibility = true;
            this.ImportButtonEnabled = false;
            this.ImportButtonVisibility = false;
            this.NewButtonEnabled = false;
            this.NewButtonVisibility = false;
            this.PrintButtonEnabled = false;
            this.PrintButtonVisibility = false;
            this.SaveButtonEnabled = false;
            this.SaveButtonVisibility = false;
            this.SearchButtonEnabled = true;
            this.SearchButtonVisibility = true;
            this.SelectAllButtonEnabled = false;
            this.SelectAllButtonVisibility = false;
            this.SelectOffButtonEnabled = false;
            this.SelectOffButtonVisibility = false;
        }

        private void deptTreeList_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Column == null || e.Node == null) return;
            if (e.Node.Focused){ 
                e.Appearance.BackColor = Color.SkyBlue;
                 e.Appearance.ForeColor = Color.Black;
            }
            else
            {
                e.Appearance.BackColor = Color.White;
                e.Appearance.ForeColor = Color.Black;
            }
            

        }
    }
}
