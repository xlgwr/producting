﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework.Abstract;
using MachineSystem.form.ParaLicense;
using Framework.Libs;
using MachineSystem.SysDefine;
using log4net;
/********************************************************************************
** 作者： xxx
** 创始时间：2015-6-8

** 修改人：xuensheng
** 修改时间：2015-7-13
** 修改内容：代码规范化

** 描述：
**    主要用于 免许类型 设置 信息的资料查询、删除、批量删除操作
*********************************************************************************/

namespace MachineSystem.TabPage
{
    public partial class frmP_LicenseType : Framework.Abstract.frmSearchBasic2
    {
        #region 变量定义
        /// <summary>
        /// 数据表
        /// </summary>
        DataTable m_tblDataList = new DataTable();
        // 日志
        private static readonly ILog log = LogManager.GetLogger(typeof(frmP_LicenseType));
        #endregion

        #region 画面初始化

        public frmP_LicenseType()
        {
            InitializeComponent();

            this.m_GridViewUtil.GridControlList = this.gridControl1;
            this.m_GridViewUtil.ParentGridView = this.gridView1;
            this.TableName = "P_License_Type";//操作表名称
            m_ParenSlctColName = "SlctValue";
        }

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                base.SetFormValue();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("画面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }
        #endregion

        #region 画面按钮功能处理方法
        /// <summary>
        /// 查询
        /// </summary>
        protected override void SetSearchProc(frmSearchBasic2 frmBaseToolXC)
        {
            base.SetSearchProc(frmBaseToolXC);

            GetDspDataList();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="isClear"></param>
        protected override void SetInsertInit(bool isClear)
        {
            base.SetInsertInit(isClear);
            try
            {
                frmEditP_LicenseType frm = new frmEditP_LicenseType();
                frm.ScanMode = Common.DataModifyMode.add;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    this.ScanMode = Common.DataModifyMode.dsp;

                    GetDspDataList();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("新增数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        public override void SetModifyInit()
        {
            base.SetModifyInit();
            try
            {
                if (this.GetSelectList().Length <= 0)
                {
                    XtraMsgBox.Show("未勾选任何数据！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (this.GetSelectList().Length > 1)
                {
                    XtraMsgBox.Show("修改时，只能勾选一条数据！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                frmEditP_LicenseType frm = new frmEditP_LicenseType();
                frm.ScanMode = Common.DataModifyMode.upd;
                //选择所有选择的数据
                frm.dr = this.GetSelectList()[0];

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    this.ScanMode = Common.DataModifyMode.dsp;
                    GetDspDataList();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("修改数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        protected override void SetDeleteInit()
        {
            try
            {
                int result = 0;
                //选择所有选择的数据
                DataRow[] drs = this.GetSelectList();

                //没有选择任何数据情况
                if (drs.Length <= 0)
                {
                    XtraMsgBox.Show("未勾选任何数据！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (XtraMsgBox.Show("是否删除数据？", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    Common.AdoConnect.Connect.CreateSqlTransaction();
                    if (drs.Length > 0)
                    {
                        for (int i = 0; i < drs.Length; i++)
                        {
                            DataRow dr = drs[i];
                            m_dicItemData = new System.Collections.Specialized.StringDictionary();
                            m_dicItemData["ID"] = dr["ID"].ToString();
                            m_dicPrimarName["ID"] = dr["ID"].ToString();
                            result = SysParam.m_daoCommon.SetDeleteDataItem(this.TableName, m_dicItemData, m_dicPrimarName);
                            if (result > 0)
                            {
                                //日志
                                SysParam.m_daoCommon.WriteLog("免许类型设置:", "删除", dr["pName"].ToString());
                            }
                        }
                    }
                    if (result > 0)
                    {
                        Common.AdoConnect.Connect.TransactionCommit();
                        XtraMsgBox.Show("删除数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SetSearchProc(this);
                    }
                }

            }
            catch (Exception ex)
            {
                Common.AdoConnect.Connect.TransactionRollback();
                log.Error(ex);
                XtraMsgBox.Show("删除数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 事件处理方法


        #endregion


        #region 共同方法

        //// <summary>
        /// 获取表格信息一览
        /// </summary>
        protected override void GetDspDataList()
        {
            try
            {
                txtpName.Enabled = true;
                txtpName.Properties.ReadOnly = false;
                NewButtonEnabled = true;
                string str_sql = "Select  CAST('0' AS Bit) AS SlctValue,* From  " + this.TableName;

                if (txtpName.Text.Trim() != "")
                {
                    str_sql += " where ";
                    str_sql += "pName like '%" + txtpName.Text.Trim() + "%' ";
                }
                str_sql += " order by ID asc ";

                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (m_tblDataList.Rows.Count > 0)
                {
                    DeleteButtonEnabled = true;
                    SelectAllButtonEnabled = true;
                    SelectOffButtonEnabled = true;
                    EditButtonEnabled = true;
                }
                else
                {
                    DeleteButtonEnabled = false;
                    SelectAllButtonEnabled = false;
                    SelectOffButtonEnabled = false;
                    EditButtonEnabled = false;
                }
                this.m_GridViewUtil.GridControlList.DataSource = m_tblDataList;
                gridView1.Columns["ID"].OptionsColumn.ReadOnly = true;
                gridView1.Columns["ID"].OptionsColumn.AllowEdit = false;
                gridView1.Columns["pName"].OptionsColumn.ReadOnly = true;
                gridView1.Columns["pName"].OptionsColumn.AllowEdit = false;

            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
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
        #endregion


    }
}
