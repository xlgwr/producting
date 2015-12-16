using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Framework.Libs;
using System.Collections.Specialized;
using Framework.DataAccess;

namespace Framework.Abstract
{
    public partial class frmBaseEntry : frmBaseXC 
    {

        #region 变量定义

        #endregion

        #region 画面属性设置

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmBaseEntry()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 画面初始化显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBaseEntry_Load(object sender, EventArgs e)
        {
            try
            {
                //设定验证条件处理
                SetValidCondition();
                
                // 窗体初始化处理
                this.SetFormValue();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            ////按钮初始化默认描述显示处理
            //GetLanguage();
        }

        #endregion

        #region 共通处理方法

        /// <summary>
        /// 清空画面数据
        /// </summary>
        protected override void ClearMainText()
        {
            this.ErrorInfo.ClearErrors();
            this.validData.Dispose();
            //清除画面输入数据信息
            Common.ClearGroupData(this.m_GrpDataItem);
        }


        /// <summary>
        /// 获取需要编辑数据信息
        /// </summary>
        /// <param name="dr"></param>
        protected override void GetGrpDataItem()
        {

            if (this.TableName != null && this.m_GrpDataItem != null)
            {
                this.m_dicItemData = new StringDictionary();
                Common.GetGroupData(this.m_GrpDataItem, ref this.m_dicItemData);
            }
        }

        /// <summary>
        /// 获取选择行数据显示
        /// </summary>
        /// <param name="dr"></param>
        protected override void SetGridRowData(DataRow dr)
        {
    
            DataColumnCollection columns;
            columns = dr.Table.Columns;

            for (int i = 0; i < dr.ItemArray.Length; i++)
            {
                if (dr[i] != null)
                {
                    this.m_dicItemData[columns[i].ColumnName] = dr[i].ToString();
                }
            }

            Common.SetGroupData(m_GrpDataItem, ref this.m_dicItemData);

        }


        /// <summary>
        /// 数据存在检查(主键重复)
        /// </summary>
        /// <returns></returns>
        protected override bool GetExistDataItem()
        {
            bool isExist = false;

            if (this.TableName != null && this.m_GrpDataItem != null)
            {
                isExist = Common.m_daoCommon.GetExistDataItem(this.TableName, this.m_dicItemData, this.m_dicPrimarName);
            }

            return isExist;

        }

        /// <summary>
        /// 数据存在检查(主键重复)
        /// </summary>
        /// <returns></returns>
        protected override bool GetRepNameCheck()
        {
            bool isExist = false;

            if (this.TableName != null && this.m_GrpDataItem != null)
            {
                isExist = Common.m_daoCommon.GetRepNameCheck(this.TableName, this.m_dicItemData,
                            this.m_dicPrimarName, m_RepFiledName, this.ScanMode);
            }

            return isExist;

        }

        #endregion
    }
} 