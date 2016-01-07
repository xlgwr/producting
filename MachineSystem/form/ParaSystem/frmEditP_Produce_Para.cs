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
using DevExpress.XtraEditors.DXErrorProvider;
using MachineSystem.form.ParaLicense;
using MachineSystem.SysDefine;
using System.Collections.Specialized;
using log4net;
/********************************************************************************
** 作者： liujinbao
** 创始时间：2015-6-8

** 修改人：libing
** 修改时间：2015-7-22
** 修改内容：代码规范化

** 描述：
**    关位参数设置的新增、修改操作
*********************************************************************************/
namespace MachineSystem.TabPage
{
    public partial class frmEditP_Produce_Para :Framework.Abstract.frmBaseToolEntryXC
    {
        #region 变量定义
        public DataRow dr;
        // 日志
        private static readonly ILog log = LogManager.GetLogger(typeof(frmEditP_Produce_Para));
        #endregion

        #region 画面初始化

        public frmEditP_Produce_Para()
        {
            InitializeComponent(); 
            
            //SetButtonEnabled();
            this.TableName = "Produce_Guanwei";//操作表名称
        }

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                //向别
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_Produce_JobFor, this.lookUpEditJobFor, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect,"pName");
                //工程别
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_Produce_Project, this.lookUpEditProjectName, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect, "pName");
                //Line别
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_Produce_Line, this.lookUpEditLineName, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect, "pName");
                //班别
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_Produce_Team, this.lookUpEditTeamName, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect, "pName");
                //关位
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_Produce_Guanwei, this.lookUpEditGuanweiName, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect, "pName");

                if (this.ScanMode == Common.DataModifyMode.add)
                {
                    GetDspDataList();
                    this.Text = "新增关位参数设置";
                }
                else if (this.ScanMode == Common.DataModifyMode.upd)
                {
                    if (dr != null)
                    {
                        GetDataRowValue(dr);
                        this.Text = "修改关位参数设置";
                    }
                }
                else if (this.ScanMode == Common.DataModifyMode.copyadd)
                {
                    if (dr != null)
                    {
                        GetDataRowValue(dr);
                        this.Text = "复制新增关位参数设置";
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
                m_dicItemData.Clear();

            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("新增失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        protected override void SetInsertProc(ref int RtnValue)
        {
            base.SetInsertProc(ref RtnValue);
            try
            {
                m_dicItemData = new System.Collections.Specialized.StringDictionary();
                m_dicItemData["JobForID"] = lookUpEditJobFor.EditValue.ToString();
                m_dicItemData["ProjectID"] = lookUpEditProjectName.EditValue.ToString();
                m_dicItemData["LineID"] = lookUpEditLineName.EditValue.ToString();
                m_dicItemData["TeamID"] = lookUpEditTeamName.EditValue.ToString();


                // 关位ID
                 m_dicItemData["GuanweiID"] = lookUpEditGuanweiName.EditValue.ToString();
                //m_dicItemData["GuanweiNames"] = lookUpEditGuanweiName.Text;
                //m_dicItemData["GuanweiType"] = comboBoxGuanweiType.Text;
                m_dicItemData["SetNum"] = txtSetNum.Text;
                m_dicItemData["RowID"] =txtRowID.Text;

                var tmpcheckSQL = "select top 1 * from " + this.TableName + " where JobForID='" + m_dicItemData["JobForID"] +
                    "' and ProjectID='" + m_dicItemData["ProjectID"] +
                    "' and LineID='" + m_dicItemData["LineID"] +
                    "' and TeamID='" + m_dicItemData["TeamID"] +
                    "' and GuanweiID='" + m_dicItemData["GuanweiID"] + "'";
                var tmptable = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(tmpcheckSQL);

                if (tmptable!=null)
                {
                    if (tmptable.Rows.Count>0)
                    {
                        XtraMsgBox.Show("新增数据失败，系统存在相同的 关位设置！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //日志
                        SysParam.m_daoCommon.WriteLog("关位参数设置-新增数据失败，系统存在相同的 关位设置", "新增", lookUpEditGuanweiName.Text);
                        //DialogResult = DialogResult.Cancel;
                        lookUpEditGuanweiName.Focus();

                        RtnValue = -1;
                        return;
                    }
                }

                int result = SysParam.m_daoCommon.SetInsertDataItem(this.TableName, m_dicItemData);
                if (result > 0)
                {
                    XtraMsgBox.Show("新增数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //日志
                    SysParam.m_daoCommon.WriteLog("关位参数设置","新增",lookUpEditGuanweiName.Text);
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
                m_dicItemData["JobForID"] = lookUpEditJobFor.EditValue.ToString();
                m_dicItemData["ProjectID"] = lookUpEditProjectName.EditValue.ToString();
                m_dicItemData["LineID"] = lookUpEditLineName.EditValue.ToString();
                m_dicItemData["TeamID"] = lookUpEditTeamName.EditValue.ToString();
                // 关位ID
                m_dicItemData["GuanweiID"] = lookUpEditGuanweiName.EditValue.ToString();
                //m_dicItemData["GuanweiNames"] = lookUpEditGuanweiName.Text;
                //m_dicItemData["GuanweiType"] = comboBoxGuanweiType.Text;
                m_dicItemData["SetNum"] = txtSetNum.Text;
                m_dicItemData["RowID"] = txtRowID.Text;

                StringDictionary dicIdentityName = new StringDictionary();
                dicIdentityName["ID"] = txtID.Text;

                int result = SysParam.m_daoCommon.SetModifyDataIdentityColumn(this.TableName, m_dicItemData, dicIdentityName);
                if (result > 0)
                {
                    XtraMsgBox.Show("修改数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //日志
                    SysParam.m_daoCommon.WriteLog("关位参数设置", "修改", lookUpEditGuanweiName.Text);
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
        /// 复制并新增
        /// </summary>
        protected override void SetCopyInsertProc(ref int RtnValue)
        {
            base.SetCopyInsertProc(ref RtnValue);
            try
            {
                m_dicItemData = new System.Collections.Specialized.StringDictionary();
                m_dicItemData["JobForID"] = lookUpEditJobFor.EditValue.ToString();
                m_dicItemData["ProjectID"] = lookUpEditProjectName.EditValue.ToString();
                m_dicItemData["LineID"] = lookUpEditLineName.EditValue.ToString();
                m_dicItemData["TeamID"] = lookUpEditTeamName.EditValue.ToString();
                // 关位ID
                m_dicItemData["GuanweiID"] = lookUpEditGuanweiName.EditValue.ToString();
                //m_dicItemData["GuanweiNames"] = lookUpEditGuanweiName.Text;
                //m_dicItemData["GuanweiType"] = comboBoxGuanweiType.Text;
                m_dicItemData["SetNum"] = txtSetNum.Text;
                m_dicItemData["RowID"] = txtRowID.Text;

                int result = SysParam.m_daoCommon.SetInsertDataItem(this.TableName, m_dicItemData);
                if (result > 0)
                {
                    XtraMsgBox.Show("新增数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //日志
                    SysParam.m_daoCommon.WriteLog("关位参数设置", "新增", lookUpEditGuanweiName.Text);
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
                    isSucces = false;
                    if (lookUpEditJobFor.EditValue.ToString()=="-1")
                    {
                        
                        DataValid.ShowErrorInfo(this.ErrorInfo, lookUpEditJobFor, "请选择向别!");
                        return;
                    }
                    if (lookUpEditProjectName.EditValue.ToString() == "-1")
                    {
                        DataValid.ShowErrorInfo(this.ErrorInfo, lookUpEditProjectName, "请选择工程别!");
                        return;
                    }
                    if (lookUpEditLineName.EditValue.ToString() == "-1")
                    {
                        DataValid.ShowErrorInfo(this.ErrorInfo, lookUpEditLineName, "请选择Line别!");
                        return;
                    }
                    if (lookUpEditTeamName.EditValue.ToString() == "-1")
                    {
                        DataValid.ShowErrorInfo(this.ErrorInfo, lookUpEditTeamName, "请选择班别!");
                        return;
                    }
                    if (lookUpEditGuanweiName.EditValue.ToString() == "-1")
                    {
                        DataValid.ShowErrorInfo(this.ErrorInfo, lookUpEditGuanweiName, "请选择关位!");
                        return;
                    }
                     if (comboBoxGuanweiType.Text == "-请选择-")
                     {
                         DataValid.ShowErrorInfo(this.ErrorInfo, comboBoxGuanweiType, "请选择类型!");
                         return;
                     }
                     if (!Framework.Libs.DataValid.IsNumeric(txtSetNum.Text.Trim()))
                     {
                         DataValid.ShowErrorInfo(this.ErrorInfo, txtSetNum, "请输入数字!");
                         return;
                     }
                     if (!Framework.Libs.DataValid.IsNumeric(txtRowID.Text.Trim()))
                     {
                          DataValid.ShowErrorInfo(this.ErrorInfo, txtRowID, "请输入数字!");
                          return;
                     }
                     else
                     {
                         // 组长，班长，替关者的.RowID不能大于	5
                         if (lookUpEditGuanweiName.Text == "组长" || lookUpEditGuanweiName.Text == "班长" || lookUpEditGuanweiName.Text == "替关者" || lookUpEditGuanweiName.Text == "副班长")
                         {
                             if (int.Parse(txtRowID.Text) < 1 || int.Parse(txtRowID.Text) > 5)
                             {
                                 DataValid.ShowErrorInfo(this.ErrorInfo, txtRowID, "请输入大于0小于5的数字!");
                                 return;
                             }
                         }
                         else
                         {
                             if (int.Parse(txtRowID.Text)<=5)
                             {
                                 DataValid.ShowErrorInfo(this.ErrorInfo, txtRowID, "请输入大于等于5的数字!");
                                 return;
                             }
                         }
                     }
                }
                isSucces = true;
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
            lookUpEditJobFor.Text = dr["JobForName"].ToString();//向别
            lookUpEditProjectName.Text = dr["ProjectName"].ToString();//工程别
            lookUpEditLineName.Text = dr["LineName"].ToString();//线别
            lookUpEditTeamName.Text = dr["TeamName"].ToString();//班别
            lookUpEditGuanweiName.Text = dr["GuanweiName"].ToString();//关位
            comboBoxGuanweiType.Text = dr["GuanweiType"].ToString();//类型
            txtRowID.Text = dr["RowID"].ToString();//关位顺序号
            txtSetNum.Text = dr["SetNum"].ToString();//标配人数
        }


        private void lookUpEditGuanweiName_EditValueChanged_1(object sender, EventArgs e)
        {
            StringDictionary m_GuanweiType = new StringDictionary();
            m_GuanweiType = new System.Collections.Specialized.StringDictionary();
            m_GuanweiType["pName"] = this.lookUpEditGuanweiName.Text.ToString();

            //班别
            if (lookUpEditGuanweiName.Text != "-请选择-")
            {
                string str = string.Format(@"select pType from P_Produce_Guanwei where pName='{0}' order by pName", this.lookUpEditGuanweiName.Text);
                DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str);
                if (dt_temp.Rows.Count > 0)
                {
                    comboBoxGuanweiType.Text = dt_temp.Rows[0][0].ToString();
                }
            }
        }
        #endregion

    }
}
