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
using System.Collections.Specialized;
using DevExpress.XtraTreeList.Nodes;

namespace MachineSystem.TabPage
{
    public partial class frmRoleWithPermissions : Framework.Abstract.frmSearchBasic2 //
    {
        public frmRoleWithPermissions()
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

                this.gridView1.OptionsBehavior.Editable = false;
                this.funcsTreeList.OptionsBehavior.Editable = false;
                setControlStatus();
                loadData(null);
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("画面初始化失败！" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 新增数据功能处理
        /// </summary>
        protected override void SetInsertInit(bool isClear)
        {
            base.SetInsertInit(isClear);
            frmRoleWithPermissionsManager frm = new frmRoleWithPermissionsManager("新增角色", null);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData(this.txtDeptName.Text.Trim());
            }
            //XtraMsgBox.Show("SetInsertInit！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        public override void SetModifyInit()
        {
            base.SetModifyInit();
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                DataRow dr = this.gridView1.GetFocusedDataRow();
                string id = dr.Field<string>("RoleID");
                frmRoleWithPermissionsManager frm = new frmRoleWithPermissionsManager("修改角色", id);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    loadData(this.txtDeptName.Text.Trim());
                }
            }

        }

        protected override void SetDeleteInit()
        {
            base.SetDeleteInit();
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                DataRow dr = this.gridView1.GetFocusedDataRow();
                string id = dr.Field<string>("RoleID");
                m_dicItemData = new StringDictionary();
                m_dicItemData["RoleID"] = id;
                m_dicPrimarName = new StringDictionary();
                m_dicPrimarName["RoleID"] = id.ToString();
                DialogResult isok= XtraMsgBox.Show("您要删除该角色吗?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question );
                if (isok == DialogResult.OK) {
                    String str = string.Format(@" select * from  dbo.Oper_User_Role where RoleID='" + id + "'");
                    DataTable dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str);
                    if (dt.Rows.Count > 0)
                    {
                        XtraMsgBox.Show("该角色已分配人员，不能删除！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    int effcCnt1 = SysParam.m_daoCommon.SetDeleteDataItem("Oper_Role_Permissions", m_dicItemData, m_dicPrimarName);
                    int effcCnt = SysParam.m_daoCommon.SetDeleteDataItem("Oper_Role", m_dicItemData, m_dicPrimarName);
                    if (effcCnt > 0)
                    {
                        XtraMsgBox.Show("数据删除成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SysParam.m_daoCommon.WriteLog("角色权限", "删除", dr.Field<string>("RoleID"));
                        loadData(this.txtDeptName.Text.Trim());
                    }
                }
                setControlStatus();
            }
        }
        protected override void SetSearchProc(frmSearchBasic2 frmBaseToolXC)
        {
            loadData(this.txtDeptName.Text.Trim());
        }

        private void loadData(string name)
        {
            string sql = "SELECT RoleID ,RoleName FROM Oper_Role ";
            if (!string.IsNullOrEmpty(name))
            {
                sql += " where RoleName like '%" + name + "%'";
            }
            sql += "  order by RoleID ";

            DataTable dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(sql);
           this.myGrid.DataSource = dt;
        }

        private void setControlStatus()
        {
            //tool button 
            this.CancelButtonEnabled = false;
            this.CancelButtonVisibility = false;
            this.CopyAddEnabled = false;
            this.CopyAddVisibility = false;
            this.DeleteButtonEnabled = true;
            this.DeleteButtonVisibility = true;
            this.EditButtonEnabled = true;
            this.EditButtonVisibility = true;
            this.ExcelButtonEnabled = false;
            this.ExcelButtonVisibility = false;
            this.ExitButtonEnabled = true;
            this.ExitButtonVisibility = true;
            this.ImportButtonEnabled = false;
            this.ImportButtonVisibility = false;
            this.NewButtonEnabled = true;
            this.NewButtonVisibility = true;
            this.PrintButtonEnabled = false;
            this.PrintButtonVisibility = false;
            this.SaveButtonEnabled = true;
            this.SaveButtonVisibility = true;
            this.SearchButtonEnabled = true;
            this.SearchButtonVisibility = true;
            this.SelectAllButtonEnabled = false;
            this.SelectAllButtonVisibility = false;
            this.SelectOffButtonEnabled = false;
            this.SelectOffButtonVisibility = false;

            this.txtDeptName.Enabled = true;
            this.txtDeptName.Properties.ReadOnly = false;
        }

        private string FocusedRoleId = string.Empty;
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusedRoleId = string.Empty;
            if (e.FocusedRowHandle < 0)
            {
                return;
            }
            //加载树轮廓
            DataTable dt = new DataTable("main");
            dt.Columns.Add("fatherid", System.Type.GetType("System.String"));
            dt.Columns.Add("id", System.Type.GetType("System.String"));
            dt.Columns.Add("pFromName", System.Type.GetType("System.String"));
            dt.Columns.Add("pPageName", System.Type.GetType("System.String"));
            dt.Columns.Add("isleaf", System.Type.GetType("System.Boolean"));
            string[][] data = new string[][]{
               new string[] {"0","-","f0","人员管理"},
                   new string[] {"0,0","0","f00","人员调动"},
                   new string[] {"0,1","0","f01","人员考勤"},
                   new string[] {"0,2","0","f02","免许管理"},
                   //new string[] {"0,3","0","f03","用户管理"},
                   //new string[] {"0,4","0","f04","人员揭示"},
               new string[] {"1","-","f1","报表查看"},
                   new string[] {"1,0","1","f10","揭示报表"},
                   new string[] {"1,1","1","f11","考勤报表"},
               new string[] {"2","-","f2","系统管理"},
                   new string[] {"2,0","2","f20","用户权限"},
                   new string[] {"2,1","2","f21","参数管理"},
                   new string[] {"2,2","2","f22","免许参数"},
                   new string[] {"2,3","2","f23","日志查看"},
              new string[] {"3","-","f3","揭示管理"},
                   new string[] {"3,0","3","f30","人员揭示"},

            };
            foreach (string[] item in data)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = item[0];
                dr["fatherid"] = item[1];
                dr["pFromName"] = item[2];
                dr["pPageName"] = item[3];
                dr["isleaf"] = false;
                dt.Rows.Add(dr);
            }
            dt.Merge(loadTree());
            this.funcsTreeList.DataSource = dt;

            //加载选择项数据
            DataRow vdr = this.gridView1.GetFocusedDataRow();
            FocusedRoleId = vdr.Field<string>("RoleID");
            DataTable dataDt = loadTreeData(FocusedRoleId);
            foreach (TreeListNode node in funcsTreeList.Nodes)
            {
                SetCheckedChildNodes2(node, dataDt);
            }

        }

        private DataTable loadTree()
        {
            string sql = @"
                select 
                cast(pMainMenuID as varchar)	+','+ cast(pSecMenuID as varchar)+','+cast(pIndex as varchar) as id
                ,cast(pMainMenuID as varchar)	+','+ cast(pSecMenuID as varchar) as fatherid
                ,pFromName
                ,pPageName, cast('1' as bit) as isleaf from P_Menu
            ";
            return SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(sql);
        }

        private DataTable loadTreeData(string roleid)
        {
            string sql = @"SELECT RoleID ,pFromName FROM Oper_Role_Permissions WHERE RoleID='" + roleid + "'";
            return SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(sql);
        }

        private void funcsTreeList_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            SetCheckedChildNodes(e.Node, e.Node.CheckState);
            SetCheckedParentNodes(e.Node, e.Node.CheckState);
        }

        private void funcsTreeList_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
        }

        private void SetCheckedChildNodes(TreeListNode node, CheckState check)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].CheckState = check;
                SetCheckedChildNodes(node.Nodes[i], check);
            }
        }
        private void SetCheckedParentNodes(TreeListNode node, CheckState check)
        {
            if (node.ParentNode != null)
            {
                bool b = false;
                CheckState state;
                for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                {
                    state = (CheckState)node.ParentNode.Nodes[i].CheckState;
                    if (!check.Equals(state))
                    {
                        b = !b;
                        break;
                    }
                }
                node.ParentNode.CheckState = b ? CheckState.Indeterminate : check;
                SetCheckedParentNodes(node.ParentNode, check);
            }
        }


        private void SetCheckedChildNodes2(TreeListNode node, DataTable dt)
        {
            if (dt != null)
            {
                if (node.ParentNode != null && node.ParentNode.CheckState == CheckState.Checked)
                {
                    for (int i = 0; i < node.Nodes.Count; i++)
                    {
                        node.Nodes[i].CheckState = CheckState.Checked;
                        SetCheckedChildNodes2(node.Nodes[i], dt);
                    }
                }
                else
                {
                    string pFromName = (string)node.GetValue("pFromName");
                    DataRow[] drs = dt.Select("pFromName='" + pFromName + "'");
                    if (drs != null && drs.Length > 0)
                    {
                        node.CheckState = CheckState.Checked;
                        node.Tag = CheckState.Checked;
                    }
                    else
                    {
                        node.Tag = CheckState.Unchecked;
                    }
                    for (int i = 0; i < node.Nodes.Count; i++)
                    {
                        SetCheckedChildNodes2(node.Nodes[i], dt);
                    }
                }
                SetCheckedParentNodes(node, node.CheckState);
            }
        }

        private void GetChangedNodes(TreeListNode fnode, ref List<TreeListNode> changedNodes)
        {
            if (fnode.CheckState != (CheckState)fnode.Tag && (bool)fnode.GetValue("isleaf"))
            {
                changedNodes.Add(fnode);
            }
            foreach (TreeListNode node in fnode.Nodes)
            {
                GetChangedNodes(node,ref changedNodes);
            }
        }

        private void funcsTreeList_Leave(object sender, EventArgs e)
        {
           List<TreeListNode> changedNodes =new List<TreeListNode>();
             foreach (TreeListNode node in funcsTreeList.Nodes)
            {
                GetChangedNodes(node, ref changedNodes);
            }

             if (changedNodes.Count > 0)
             {

             }
        }

        protected override void SetSaveDataProc(frmSearchBasic2 frmbase)
        {
            //base.SetSaveDataProc(frmbase);
            if (!string.IsNullOrEmpty(FocusedRoleId))
            {
                List<TreeListNode> changedNodes = new List<TreeListNode>();
                foreach (TreeListNode node in funcsTreeList.Nodes)
                {
                    GetChangedNodes(node, ref changedNodes);
                }

                if (changedNodes.Count > 0)
                {

                    foreach (TreeListNode node in changedNodes)
                    {
                        m_dicItemData = new StringDictionary();
                        m_dicItemData["RoleID"] = FocusedRoleId;
                        m_dicItemData["pFromName"] = (string)node.GetValue("pFromName");
                        int effcCnt1 = SysParam.m_daoCommon.SetDeleteDataItem("Oper_Role_Permissions", m_dicItemData, m_dicItemData);
                        if (node.CheckState == CheckState.Unchecked)
                        {
                            node.Tag = CheckState.Unchecked;
                        }
                        if(node.CheckState  == CheckState.Checked){
                            int effcCnt2 = SysParam.m_daoCommon.SetInsertDataItem("Oper_Role_Permissions", m_dicItemData);
                            if (effcCnt2>0)
                            {
                                node.Tag = CheckState.Checked;
                            }
                        }
                        
                    }

                }
                XtraMsgBox.Show("保存角色权限成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        
            }

           
        }
    }
}
