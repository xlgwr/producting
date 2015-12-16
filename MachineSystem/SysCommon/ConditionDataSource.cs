using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using Framework.DataAccess;

namespace MachineSystem.SysCommon
{
    /// <summary>
    /// Report条件选项查询
    /// </summary>
    public class ConditionDataSource
    {
        private static StringDictionary combine(StringDictionary a, StringDictionary b)
        {
            StringDictionary dd = new StringDictionary();
            if (a != null)
            {
                foreach (string k in a.Keys)
                {
                    if (!string.IsNullOrEmpty(a[k]))
                        dd.Add(k, a[k]);
                }
            }
            if (b != null)
            {
                foreach (string k in b.Keys)
                {
                    if (!string.IsNullOrEmpty(b[k]))
                        dd.Add(k, b[k]);
                }
            }
            return dd;
        }

        public static DataTable GetTable(daoCommon dao, string sql, StringDictionary equalFieldsWithVal, StringDictionary likeFieldsWithVal,string afterOrderByString)
        {
            return dao.GetTableInfoBySql(sql, combine(equalFieldsWithVal, likeFieldsWithVal), equalFieldsWithVal, likeFieldsWithVal, afterOrderByString);
        }
        public static DataTable GetProduceJobForTable(daoCommon dao, StringDictionary equalFieldsWithVal, StringDictionary likeFieldsWithVal)
        {
            return GetTable(dao, "Select * From P_Produce_JobFor ", equalFieldsWithVal, likeFieldsWithVal, "id");
        }

        public static DataTable GetProduceProjectTable(daoCommon dao, StringDictionary equalFieldsWithVal, StringDictionary likeFieldsWithVal)
        {
            return GetTable(dao, "Select DISTINCT ProjectID,ProjectName From V_Produce_Para_i", equalFieldsWithVal, likeFieldsWithVal, "ProjectName");
        }

        public static DataTable GetProduceLineTable(daoCommon dao, StringDictionary equalFieldsWithVal, StringDictionary likeFieldsWithVal)
        {
            return GetTable(dao, "Select DISTINCT LineID,LineName FROM V_Produce_Para_i", equalFieldsWithVal, likeFieldsWithVal, "LineName");
        }

        public static DataTable GetProduceTeamTable(daoCommon dao, StringDictionary equalFieldsWithVal, StringDictionary likeFieldsWithVal)
        {
            return GetTable(dao, "Select * From P_Produce_Team", equalFieldsWithVal, likeFieldsWithVal, "id");
        }

        /// <summary>
        /// 取得某月的第一天
        /// </summary>
        /// <param name="datetime">要取得月份第一天的时间</param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day);
        }
        /**/
        /// <summary>
        /// 取得某月的最后一天
        /// </summary>
        /// <param name="datetime">要取得月份最后一天的时间</param>
        /// <returns></returns>
        public static DateTime LastDayOfMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1);
        }
    }
}
