﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework.Abstract;
using Framework.Libs;
using log4net;

/********************************************************************************
** 作者： liujinbao
** 创始时间：2015-6-8

** 修改人：xuensheng
** 修改时间：2015-7-9
** 修改内容：代码规范化

** 描述：
**    主要用于离职原因设置信息的资料新增、修改操作
*********************************************************************************/
namespace MachineSystem.TabPage
{
    public partial class frmEditP_LeaveReason : Framework.Abstract.frmBaseToolEntryXC
    {
        #region 变量定义
        public DataRow dr;
        // 日志
        private static readonly ILog log = LogManager.GetLogger(typeof(frmEditP_LeaveReason));
        #endregion

        #region 画面初始化

        public frmEditP_LeaveReason()
        {
            InitializeComponent();
            this.TableName = "Attend_LeaveReason";//操作表名称
        }

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                txtID.Properties.ReadOnly = true;

                if (this.ScanMode == Common.DataModifyMode.add)
                {
                    this.Text = "新增离职原因设置";
                }
                else if (this.ScanMode == Common.DataModifyMode.upd)
                {
                    if (dr != null)
                    {
                        GetDataRowValue(dr);
                        this.Text = "修改离职原因设置";
                    }
                }
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
        /// 保存
        /// </summary>
        /// <param name="frmbase"></param>
        protected override void SetSaveDataProc(frmBaseToolEntryXC frmbase)
        {
            try
            {
                base.SetSaveDataProc(frmbase);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("新增数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        #endregion

        #region 事件处理方法

        /// <summary>
        /// 新增
        /// </summary>
        protected override void SetInsertProc(ref int RtnValue)
        {
            base.SetInsertProc(ref RtnValue);
            try
            {
                m_dicItemData = new System.Collections.Specialized.StringDictionary();
                string count = SysParam.m_daoCommon.GetMaxNoteNo(this.TableName, "ID");
                txtID.Text = int.Parse(count).ToString();
                m_dicItemData["ID"] = txtID.Text.Trim();
                m_dicItemData["pName"] = txtpName.Text.Trim();

                int result = SysParam.m_daoCommon.SetInsertDataItem(this.TableName, m_dicItemData);
                if (result > 0)
                {
                    XtraMsgBox.Show("新增数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //日志
                    SysParam.m_daoCommon.WriteLog("离职原因设置", "新增", txtpName.Text.Trim());
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="RtnValue"></param>
        public override void SetModifyProc(ref int RtnValue)
        {
            base.SetModifyProc(ref RtnValue);
            try
            {
                m_dicPrimarName.Clear();
                m_dicPrimarName["ID"] = txtID.Text.Trim();
                m_dicItemData = new System.Collections.Specialized.StringDictionary();
                m_dicItemData["ID"] = txtID.Text.Trim();
                m_dicItemData["pName"] = txtpName.Text.Trim();

                int result = SysParam.m_daoCommon.SetModifyDataItem(this.TableName, m_dicItemData, m_dicPrimarName);
                if (result > 0)
                {
                    XtraMsgBox.Show("修改数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //日志
                    SysParam.m_daoCommon.WriteLog("离职原因设置", "修改", txtpName.Text.Trim());
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="isClear"></param>
        protected override void SetCancelInit(bool isClear)
        {
            this.Close();
        }


        #endregion

        #region 共同方法

        /// <summary>
        /// 名称是否存在判断
        /// </summary>
        /// <param name="table"></param>
        /// <param name="keyFieldName"></param>
        /// <param name="keyFieldValue"></param>
        /// <param name="checkFieldName"></param>
        /// <param name="checkFieldValue"></param>
        /// <returns></returns>
        private bool IsUserStatusDuplicated(string table, string keyFieldName, object keyFieldValue, string checkFieldName, object checkFieldValue)
        {
            StringBuilder sb = new StringBuilder("select count(1) as cnt from  ");
            sb.AppendFormat("{0} where 1=1", table);

            if (checkFieldValue.GetType() == typeof(string))
            {
                sb.AppendFormat(" and {0}='{1}'", checkFieldName, checkFieldValue);
            }
            else
            {
                sb.AppendFormat(" and {0}={1} ", checkFieldName, checkFieldValue);
            }
            if (keyFieldValue != null)
            {
                if (keyFieldValue.GetType() == typeof(string))
                {
                    sb.AppendFormat(" and {0}<>'{1}'", keyFieldName, keyFieldValue);
                }
                else
                {
                    sb.AppendFormat(" and {0}<>{1} ", keyFieldName, keyFieldValue);
                }
            }

            DataTable dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(sb.ToString());
            if (dt.Rows.Count > 0)
            {
                Int32 count = dt.Rows[0].Field<Int32>(0);
                dt.Dispose();
                return count > 0;
            }
            dt.Dispose();
            return true;
        }


        /// <summary>
        /// 画面数据有效检查处理
        /// </summary>
        protected override void GetInputCheck(ref bool isSucces)
        {
            try
            {
                base.GetInputCheck(ref isSucces);
                if (isSucces)
                {
                    if (string.IsNullOrEmpty(this.txtpName.Text) || this.txtpName.Text.Trim() == "")
                    {
                        isSucces = false;
                        DataValid.ShowErrorInfo(this.ErrorInfo, this.txtpName, "名称不能为空!");

                    }
                    else
                    {
                        if (Encoding.Default.GetByteCount(this.txtpName.Text.Trim()) > 50)
                        {
                            isSucces = false;
                            DataValid.ShowErrorInfo(this.ErrorInfo, this.txtpName, "名称不能超过25个汉字!");
                        }
                        //数据库重复判断
                        if (this.ScanMode == Common.DataModifyMode.add)
                        {
                            if (IsUserStatusDuplicated(this.TableName, "ID", null, "pName", this.txtpName.Text.Trim()))
                            {
                                isSucces = false;
                                DataValid.ShowErrorInfo(this.ErrorInfo, this.txtpName, "名称已经存在!");
                            }
                        }
                        if (this.ScanMode == Common.DataModifyMode.upd)
                        {
                            if (IsUserStatusDuplicated(this.TableName, "ID", int.Parse(this.txtID.Text.Trim()), "pName", this.txtpName.Text.Trim()))
                            {
                                isSucces = false;
                                DataValid.ShowErrorInfo(this.ErrorInfo, this.txtpName, "名称已经被使用!");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public void GetDataRowValue(DataRow dr)
        {
            txtID.Text = dr["ID"].ToString();
            txtpName.Text = dr["pName"].ToString();
        }

      

        #endregion
    }
}
