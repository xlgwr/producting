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

namespace MachineSystem.TabPage
{
    public partial class frmRoleWithPermissionsManager : Framework.Abstract.frmBaseToolEntryXC
    {

        private string editId;
        public frmRoleWithPermissionsManager(string frmTitle, string editId)
        {
            InitializeComponent();
            this.editId = editId;
            this.Text = frmTitle;
            this.ScanMode = editId == null ? Framework.Libs.Common.DataModifyMode.add : Framework.Libs.Common.DataModifyMode.upd;
        }

        #region ""
        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            //tool button 
            this.CancelButtonEnabled = true;
            this.CancelButtonVisibility = true;
            this.DeleteButtonEnabled = false;
            this.DeleteButtonVisibility = false;
            this.EditButtonEnabled = false;
            this.EditButtonVisibility = false;
            this.ExcelButtonEnabled = false;
            this.ExcelButtonVisibility = false;
            this.ExitButtonEnabled = false;
            this.ExitButtonVisibility = false;
            this.ImportButtonEnabled = false;
            this.ImportButtonVisibility = false;
            this.NewButtonEnabled = false;
            this.NewButtonVisibility = false;
            this.PrintButtonEnabled = false;
            this.PrintButtonVisibility = false;
            this.SaveButtonEnabled = true;
            this.SaveButtonVisibility = true;
            this.SearchButtonEnabled = false;
            this.SearchButtonVisibility = false;
            this.RefreshButtonEnabled = false;
            this.RefreshButtonVisibility = false;
            try
            {
                if (this.ScanMode == Framework.Libs.Common.DataModifyMode.upd)
                {
                    this.txtId.Enabled = false;
                    string sql = "select * from Oper_Role where RoleID= '" + this.editId + "'";
                    DataTable dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(sql);
                    if (dt.Rows.Count > 0)
                    {
                        this.txtId.Text = dt.Rows[0].Field<string>("RoleID");
                        this.txtName.Text = dt.Rows[0].Field<string>("RoleName");
                    }
                    else
                    {
                        XtraMsgBox.Show("无法获取！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    this.txtId.Enabled = true;
                }
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("画面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        #endregion

        #region 画面按钮功能处理方法

        protected override void SetCancelInit(bool isClear)
        {
            this.Close();
        }

        protected override void SetInsertProc(ref int RtnValue)
        {
            //base.SetInsertProc(ref RtnValue);
            InsertOrUpdate();
        }

        public override void SetModifyProc(ref int RtnValue)
        {
           // base.SetModifyProc (ref RtnValue);
            InsertOrUpdate();
        }

        private void InsertOrUpdate()
        {
            m_dicItemData = new StringDictionary();
            m_dicItemData["RoleName"] = this.txtName.Text.Trim();
            if (this.ScanMode == Common.DataModifyMode.add)
            {
                m_dicItemData["RoleID"] = this.txtId.Text.Trim();
                int effCount = SysParam.m_daoCommon.SetInsertDataItem("Oper_Role", m_dicItemData);
                if (effCount > 0)
                {
                    XtraMsgBox.Show("数据保存成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SysParam.m_daoCommon.WriteLog("权限设置","新增",this.Text  );
                    DialogResult = DialogResult.OK;
                }
            }
            else
            {
                m_dicItemData["RoleID"] = this.txtId.Text.Trim();
                StringDictionary whereParamDic = new StringDictionary();
                whereParamDic["RoleID"] = this.txtId.Text.Trim();
                int effCount = SysParam.m_daoCommon.SetModifyDataItem("Oper_Role", m_dicItemData, whereParamDic);
                if (effCount > 0)
                {
                    XtraMsgBox.Show("数据保存成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SysParam.m_daoCommon.WriteLog("权限设置", "修改", this.Text);
                    DialogResult = DialogResult.OK;
                }
            }

           
            
        }
        #endregion

        #region 共同方法

        private bool IsFieldDuplicated(string table, string keyFieldName, object keyFieldValue, string checkFieldName,object checkFieldValue)
        {
            StringBuilder sb=new StringBuilder("select count(1) as cnt from  ");
            sb.AppendFormat("{0} where 1=1",table );

            if (checkFieldValue.GetType() == typeof(string))
            {
                sb.AppendFormat(" and {0}='{1}'", checkFieldName, checkFieldValue);
            }
            else
            {
                sb.AppendFormat(" and {0}={1} ",  checkFieldName, checkFieldValue);
            }
            if(keyFieldValue !=null){
                if(keyFieldValue.GetType() == typeof (string)){
                    sb.AppendFormat(" and {0}<>'{1}'",  keyFieldName, keyFieldValue);
                }else{
                    sb.AppendFormat(" and {0}<>{1} ",  keyFieldName, keyFieldValue);
                }
            }

           

           DataTable dt= SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(sb.ToString());
           if (dt.Rows.Count > 0)
           {
               Int32 count = dt.Rows[0].Field<Int32>(0);
               dt.Dispose();
               return count > 0;
           }
           dt.Dispose();
           return true;
        }

        //private Int32 GetNextId(string table, string keyFieldName)
        //{
        //    StringBuilder sb = new StringBuilder("");
        //    sb.AppendFormat("select MAX({0}) as id from {1}", keyFieldName, table);
        //    DataTable dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(sb.ToString());
        //    if (dt.Rows.Count > 0)
        //    {
        //        Int32 id = dt.Rows[0].Field<Int32>(0);
        //        dt.Dispose();
        //        return id +1;
        //    }
        //    dt.Dispose();
        //    return 1;
        //}

        /// <summary>
        /// 画面数据有效检查处理
        /// </summary>
        /// <param name="isSucces"></param>
        protected override void GetInputCheck(ref bool isSucces)
        {
            base.GetInputCheck(ref isSucces);
            if (isSucces)
            {
                if (string.IsNullOrEmpty(this.txtId.Text.Trim()))
                {
                    isSucces = false;
                    DataValid.ShowErrorInfo(this.ErrorInfo, this.txtId, "ID不能为空!");
                }
                else
                {
                    if (string.IsNullOrEmpty(this.txtName.Text.Trim()))
                    {
                        isSucces = false;
                        DataValid.ShowErrorInfo(this.ErrorInfo, this.txtName, "名称不能为空!");

                    }
                    else
                    {
                        if (this.txtName.Text.Trim().ToCharArray().Count() > 40)
                        {
                            isSucces = false;
                            DataValid.ShowErrorInfo(this.ErrorInfo, this.txtName, "名称不能超过40字符!");
                        }

                        //数据库重复判断
                        if (this.ScanMode == Common.DataModifyMode.add)
                        {
                            if (IsFieldDuplicated("Oper_Role", "RoleID", null, "RoleName", this.txtName.Text.Trim()))
                            {
                                isSucces = false;
                                DataValid.ShowErrorInfo(this.ErrorInfo, this.txtName, "名称已经存在!");
                            }
                        }
                        if (this.ScanMode == Common.DataModifyMode.upd)
                        {
                            if (IsFieldDuplicated("Oper_Role", "RoleID", this.txtId.Text.Trim(), "RoleName", this.txtName.Text.Trim()))
                            {
                                isSucces = false;
                                DataValid.ShowErrorInfo(this.ErrorInfo, this.txtName, "名称已经被使用!");
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
