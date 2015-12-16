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
using MachineSystem.SysDefine;
using log4net;

namespace MachineSystem.TabPage
{
    public partial class frmProduceScheduling : Framework.Abstract.frmSearchBasic2
    {
        // 日志
        private static readonly ILog log = LogManager.GetLogger(typeof(frmP_Produce_JobFor));
        
        public frmProduceScheduling()
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
                this.gridView1.OptionsDetail.ShowDetailTabs = false;
                this.gridView1.OptionsDetail.EnableDetailToolTip = false;
                this.gridView1.OptionsDetail.AllowOnlyOneMasterRowExpanded = true;
                this.gridView2.OptionsBehavior.Editable = false;
   
                setControlStatus();
                loadData(null);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("画面初始化失败！" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 新增数据功能处理
        /// </summary>
        protected override void SetInsertInit(bool isClear)
        {
            base.SetInsertInit(isClear);
            frmProdSchedulingManager frm = new frmProdSchedulingManager("新增排班设置", null);
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
                Int32 id = dr.Field<Int32>("ID");
                frmProdSchedulingManager frm = new frmProdSchedulingManager("修改排班设置", id);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    loadData(this.txtDeptName.Text.Trim());
                }
            }

        }
        /// <summary>
        /// 获取勾选数据
        /// </summary>
        protected DataRow[] GetSelectList()
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            //获取删除行
            DataTable dt = ((DataView)this.gridView1.DataSource).Table.Copy();
            //选择所有选择的数据
            DataRow[] drs = dt.Select(EnumDefine.SlctValue + "='true'");
            return drs;
        }
        protected override void SetDeleteInit()
        {
            base.SetDeleteInit();
           
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                DialogResult isok = XtraMsgBox.Show("您要删除该排班设置吗? ", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (isok == DialogResult.OK)
                {
                    DataRow dr = this.gridView1.GetFocusedDataRow();
                    Int32 id = dr.Field<Int32>("ID");

                    m_dicItemData = new StringDictionary();
                    m_dicItemData["SchedulingID"] = id.ToString();
                    m_dicPrimarName = new StringDictionary();
                    m_dicPrimarName["SchedulingID"] = id.ToString();
                    SysParam.m_daoCommon.SetDeleteDataItem("P_Produce_Scheduling_detail", m_dicItemData, m_dicPrimarName);

                    m_dicItemData = new StringDictionary();
                    m_dicItemData["ID"] = id.ToString();
                    m_dicPrimarName = new StringDictionary();
                    m_dicPrimarName["ID"] = id.ToString();
                    int effcCnt = SysParam.m_daoCommon.SetDeleteDataItem("P_Produce_Scheduling", m_dicItemData, m_dicPrimarName);
                    if (effcCnt > 0)
                    {
                        XtraMsgBox.Show("数据删除成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SysParam.m_daoCommon.WriteLog("排班设置", "删除", id+"");
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
            
            string sql = "select * from P_Produce_Scheduling ";
            string sqldtail = @"select a.SchedulingID 
                            ,a.StrDate 
                            ,a.EndDate 
                            ,b.pName
                            from P_Produce_Scheduling_detail  a 
                            left  join P_Produce_TeamKind b
                            on a.TypeID =b.ID   where 1=1";
            if (!string.IsNullOrEmpty(name))
            {
                sql += " where Pname like '%" + name + "%'";
                sqldtail += " and SchedulingID IN (select ID   from P_Produce_Scheduling where  Pname like '%" + name + "%' )";
            }
            sqldtail += " order by SchedulingID,StrDate asc ";
            
            DataTable dtm = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(sql);
            dtm.TableName="M";
            DataTable dts = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(sqldtail);
            dts.TableName ="S";
            DataSet ds = dtm.DataSet;
            ds.Merge(dts);


            DataColumn keyColumn = ds.Tables["M"].Columns["ID"];
            DataColumn foreignKeyColumn = ds.Tables["S"].Columns["SchedulingID"];

            ds.Relations.Add("fsrelation", keyColumn,foreignKeyColumn,false);
            this.myGrid.DataSource = ds.Tables["M"];
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
            this.SaveButtonEnabled = false;
            this.SaveButtonVisibility = false;
            this.SearchButtonEnabled = true;
            this.SearchButtonVisibility = true;
            this.SelectAllButtonEnabled = false;
            this.SelectAllButtonVisibility = false;
            this.SelectOffButtonEnabled = false;
            this.SelectOffButtonVisibility = false;

            this.txtDeptName.Enabled = true;
            this.txtDeptName.Properties.ReadOnly = false;
        }
    }
}
