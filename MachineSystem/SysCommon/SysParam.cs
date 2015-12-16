using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Data;
using Framework.Libs;
using MachineSystem.TabPage;
using Framework.DataAccess;

namespace MachineSystem
{
    public class SysParam
    {
        #region 共通参数设置
 
        /// <summary>
        /// 语言下拉框
        /// </summary>
        public static DataTable tblLanguage;

        /// <summary>
        /// 中途停止开关
        /// </summary>
        public static int intStop=0;

        //public static frmPlanProgress w_frmPlanProgress = new frmPlanProgress();
        public static string StartupModule = "StartupMoldule";
        /// <summary>
        /// 常量
        /// </summary>
        public static string SymbolHor = "-";
        #endregion

        /// <summary>
        /// 共通数据对象
        /// </summary>
        public static daoCommon m_daoCommon;
    }
}
