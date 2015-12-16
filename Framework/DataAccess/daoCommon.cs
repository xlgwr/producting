using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Framework.DataAccess;
using Framework.Libs;

namespace Framework.DataAccess
{
    public class daoCommon
    {
        
        #region 初始化处理

        #region 变量定义
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(daoCommon));
        #endregion

        #endregion

        #region 数据查询部分

        #region 数据查询

        /// <summary>
        /// 读取表格信息数据
        /// </summary>
        /// <param name="TableName">数据表名称</param>
        /// <param name="dicItemData">检索条件值</param>
        /// <param name="dicConds">精确查询列名</param>
        /// <param name="dicLikeConds">模糊查询列名</param>
        /// <param name="OrderBy">需要排序的列</param>
        /// <returns></returns>
        public DataTable GetTableInfo(String TableName,
                                    StringDictionary dicItemData,
                                    StringDictionary dicConds,
                                    StringDictionary dicLikeConds,
                                    string OrderBy)
        {
            int rowIndex = 0;
            DataTable w_RtnDataTable = null;
            SqlParameter[] cmdParamters=null;
            SqlParameter SqlParameter = null;
            try
            {

                string strSql = "SELECT  CAST('0' AS Bit) AS SlctValue," + TableName + ".* "
                            + " FROM " + TableName
                            + " WHERE 1=1 ";

                Common.AdoConnect.Connect.ConnectOpen();

                cmdParamters = new SqlParameter[dicConds.Count + dicLikeConds.Count];


                foreach (string strKey in dicConds.Keys)
                {
                    if (dicItemData.ContainsKey(strKey) == true)
                    {
                        strSql += " AND " + strKey + " =@" + strKey;

                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);

                        if (SqlParameter.SqlDbType == SqlDbType.Bit)
                        {
                            SqlParameter.Value = bool.Parse(dicItemData[strKey]);
                        }
                        else
                        {
                            SqlParameter.Value = dicItemData[strKey];
                        }

                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                }

                foreach (string strKey in dicLikeConds.Keys)
                {
                    if (dicItemData.ContainsKey(strKey) == true
                        && string.IsNullOrEmpty(dicItemData[strKey]) == false)
                    {
                        strSql += " AND " + strKey + " Like @" + strKey;

                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);
                        SqlParameter.Value = "%" + dicItemData[strKey] + "%";
                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                }

                if (!string.IsNullOrEmpty(OrderBy))
                {
                    strSql += " ORDER BY " + OrderBy;
                }
               
                w_RtnDataTable = Common.AdoConnect.Connect.GetDataSet(strSql, cmdParamters);

                return w_RtnDataTable;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// 读取表格信息数据
        /// </summary>
        /// <param name="TableName">Sql语句</param>
        /// <param name="dicItemData">检索条件值</param>
        /// <param name="dicConds">精确查询列名</param>
        /// <param name="dicLikeConds">模糊查询列名</param>
        /// <param name="OrderBy">需要排序的列</param>
        /// <returns></returns>
        public DataTable GetTableInfoBySql(String Sql,
                                    StringDictionary dicItemData,
                                    StringDictionary dicConds,
                                    StringDictionary dicLikeConds,
                                    string OrderBy)
        {
            int rowIndex = 0;
            DataTable w_RtnDataTable = null;
            SqlParameter[] cmdParamters = null;
            SqlParameter SqlParameter = null;
            try
            {

                string strSql = Sql
                            + " WHERE 1=1 ";

                Common.AdoConnect.Connect.ConnectOpen();

                cmdParamters = new SqlParameter[dicConds.Count + dicLikeConds.Count];


                foreach (string strKey in dicConds.Keys)
                {
                    if (dicItemData.ContainsKey(strKey) == true)
                    {
                        strSql += " AND " + strKey + " =@" + strKey;

                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);

                        if (SqlParameter.SqlDbType == SqlDbType.Bit)
                        {
                            SqlParameter.Value = bool.Parse(dicItemData[strKey]);
                        }
                        else
                        {
                            SqlParameter.Value = dicItemData[strKey];
                        }

                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                }

                foreach (string strKey in dicLikeConds.Keys)
                {
                    if (dicItemData.ContainsKey(strKey) == true
                        && string.IsNullOrEmpty(dicItemData[strKey]) == false)
                    {
                        strSql += " AND " + strKey + " Like @" + strKey;

                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);
                        SqlParameter.Value = "%" + dicItemData[strKey] + "%";
                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                }


                if (!string.IsNullOrEmpty(OrderBy))
                {
                    strSql += " ORDER BY " + OrderBy;
                }
               
                w_RtnDataTable = Common.AdoConnect.Connect.GetDataSet(strSql, cmdParamters);

                return w_RtnDataTable;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// 读取表格信息数据
        /// </summary>
        /// <param name="TableName">数据表名称</param>
        /// <param name="dicItemData">检索条件值</param>
        /// <param name="dicConds">精确查询列名</param>
        /// <param name="dicBetweenConds">时间段查询列名</param>
        /// <param name="dicLikeConds">模糊查询列名</param>
        /// <param name="OrderBy">需要排序的列</param>
        /// <returns></returns>
        public DataTable GetTableInfo(String TableName,
                                    StringDictionary dicItemData,
                                    StringDictionary dicConds,
                                    StringDictionary dicBetweenConds,
                                    StringDictionary dicLikeConds,
                                    string OrderBy)
        {
            int rowIndex = 0;
            DataTable w_RtnDataTable = null;
            SqlParameter[] cmdParamters = null;
            SqlParameter SqlParameter = null;
            try
            {

                string strSql = "SELECT  CAST('0' AS Bit) AS SlctValue," + TableName + ".* "
                             + " FROM " + TableName
                             + " WHERE 1=1 ";

                Common.AdoConnect.Connect.ConnectOpen();

                cmdParamters = new SqlParameter[dicConds.Count + dicLikeConds.Count + dicBetweenConds.Count * 2];


                foreach (string strKey in dicConds.Keys)
                {
                    if (dicItemData.ContainsKey(strKey) == true)
                    {
                        strSql += " AND " + strKey + " =@" + strKey;

                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);

                        if (SqlParameter.SqlDbType == SqlDbType.Bit)
                        {
                            SqlParameter.Value = bool.Parse(dicItemData[strKey]);
                        }
                        else
                        {
                            SqlParameter.Value = dicItemData[strKey];
                        }

                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                }


                foreach (string strKey in dicBetweenConds.Keys)
                {
                    if (dicItemData.ContainsKey(strKey + Common.DefineValue.FiledBegin) == true
                        && dicItemData.ContainsKey(strKey + Common.DefineValue.FiledEnd) == true)
                    {
                        strSql += " AND " + strKey + " >=@" + strKey + Common.DefineValue.FiledBegin;
                        strSql += " AND " + strKey + " <=@" + strKey + Common.DefineValue.FiledEnd;

                        SqlParameter = new SqlParameter("@" + strKey + Common.DefineValue.FiledBegin, DataFiledType.FiledType[strKey]);
                        SqlParameter.Value = dicItemData[strKey + Common.DefineValue.FiledBegin];
                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;


                        SqlParameter = new SqlParameter("@" + strKey + Common.DefineValue.FiledEnd, DataFiledType.FiledType[strKey]);
                        //获取结束时间处理
                        SqlParameter.Value = dicItemData[strKey + Common.DefineValue.FiledEnd];
                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                }


                foreach (string strKey in dicLikeConds.Keys)
                {
                    if (dicItemData.ContainsKey(strKey) == true
                        && string.IsNullOrEmpty(dicItemData[strKey]) == false)
                    {
                        strSql += " AND " + strKey + " Like @" + strKey;

                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);
                        SqlParameter.Value = "%" + dicItemData[strKey] + "%";
                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                }


                if (!string.IsNullOrEmpty(OrderBy))
                {
                    strSql += " ORDER BY " + OrderBy;
                }

                w_RtnDataTable = Common.AdoConnect.Connect.GetDataSet(strSql, cmdParamters);

                return w_RtnDataTable;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 获取最大编号
        /// </summary>
        /// <param name="TableName">数据表名称</param>
        /// <param name="FiledName">最大值列名</param>
        /// <returns></returns>
        public String GetMaxNoteNo(String TableName, String FiledName)
        {

            int iniItem = 1000000;
            DataTable dt = new DataTable();
            int MaxNo = 1;

            try
            {
                if (string.IsNullOrEmpty(TableName) || string.IsNullOrEmpty(FiledName)) return "";
                string strSql = "SELECT ISNULL(MAX( CONVERT(INT," + FiledName + ")), 0) AS MaxNo "
                             + " FROM " + TableName;

                Common.AdoConnect.Connect.ConnectOpen();

                dt = Common.AdoConnect.Connect.GetDataSet(strSql);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (int.TryParse(dt.Rows[0]["MaxNo"].ToString(), out MaxNo) == true)
                        {

                        }
                    }
                }

                MaxNo = iniItem + MaxNo + 1;

                return MaxNo.ToString().Substring(1, iniItem.ToString().Length - 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 数据查询(不含Where)

        /// <summary>
        /// 读取表格信息数据
        /// </summary>
        /// <param name="TableName">Sql语句</param>
        /// <param name="dicItemData">检索条件值</param>
        /// <param name="dicConds">精确查询列名</param>
        /// <param name="dicLikeConds">模糊查询列名</param>
        /// <param name="OrderBy">需要排序的列</param>
        /// <returns></returns>
        public DataTable GetTableInfoBySqlNoWhere(String Sql,
                                    StringDictionary dicItemData,
                                    StringDictionary dicConds,
                                    StringDictionary dicLikeConds,
                                    string OrderBy)
        {
            int rowIndex = 0;
            DataTable w_RtnDataTable = null;
            SqlParameter[] cmdParamters = null;
            SqlParameter SqlParameter = null;
            try
            {

                string strSql = Sql;

                Common.AdoConnect.Connect.ConnectOpen();

                cmdParamters = new SqlParameter[dicConds.Count + dicLikeConds.Count];


                foreach (string strKey in dicConds.Keys)
                {
                    if (dicItemData.ContainsKey(strKey) == true)
                    {
                        strSql += " AND " + strKey + " =@" + strKey;

                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);

                        if (SqlParameter.SqlDbType == SqlDbType.Bit)
                        {
                            SqlParameter.Value = bool.Parse(dicItemData[strKey]);
                        }
                        else
                        {
                            SqlParameter.Value = dicItemData[strKey];
                        }

                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                }

                foreach (string strKey in dicLikeConds.Keys)
                {
                    if (dicItemData.ContainsKey(strKey) == true
                        && string.IsNullOrEmpty(dicItemData[strKey]) == false)
                    {
                        strSql += " AND " + strKey + " Like @" + strKey;

                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);
                        SqlParameter.Value = "%" + dicItemData[strKey] + "%";
                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                }


                if (!string.IsNullOrEmpty(OrderBy))
                {
                    strSql += " ORDER BY " + OrderBy;
                }

                w_RtnDataTable = Common.AdoConnect.Connect.GetDataSet(strSql, cmdParamters);

                return w_RtnDataTable;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        /// <summary>
        /// 读取表格信息数据
        /// </summary>
        /// <param name="TableName">Sql语句</param>
        /// <param name="dicItemData">检索条件值</param>
        /// <param name="dicConds">精确查询列名</param>
        /// <param name="dicBetweenConds">时间段查询列名</param>
        /// <param name="dicLikeConds">模糊查询列名</param>
        /// <param name="OrderBy">需要排序的列</param>
        /// <returns></returns>
        public DataTable GetTableInfoBySqlNoWhere(String Sql,
                                    StringDictionary dicItemData,
                                    StringDictionary dicConds,
                                    StringDictionary dicBetweenConds,
                                    StringDictionary dicLikeConds,
                                    string OrderBy)
        {
            int rowIndex = 0;
            DataTable w_RtnDataTable = null;
            SqlParameter[] cmdParamters = null;
            SqlParameter SqlParameter = null;
            try
            {

                string strSql = Sql;

                Common.AdoConnect.Connect.ConnectOpen();

                cmdParamters = new SqlParameter[dicConds.Count + dicLikeConds.Count + dicBetweenConds.Count *2 ];


                foreach (string strKey in dicConds.Keys)
                {
                    if (dicItemData.ContainsKey(strKey) == true)
                    {
                        strSql += " AND " + strKey + " =@" + strKey;

                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);

                        if (SqlParameter.SqlDbType == SqlDbType.Bit)
                        {
                            SqlParameter.Value = bool.Parse(dicItemData[strKey]);
                        }
                        else
                        {
                            SqlParameter.Value = dicItemData[strKey];
                        }

                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                }


                foreach (string strKey in dicBetweenConds.Keys)
                {
                    if (dicItemData.ContainsKey(strKey + Common.DefineValue.FiledBegin) == true
                        && dicItemData.ContainsKey(strKey + Common.DefineValue.FiledEnd) == true)
                    {
                        strSql += " AND " + strKey + " >=@" + strKey + Common.DefineValue.FiledBegin;
                        strSql += " AND " + strKey + " <@" + strKey + Common.DefineValue.FiledEnd;

                        SqlParameter = new SqlParameter("@" + strKey + Common.DefineValue.FiledBegin, DataFiledType.FiledType[strKey]);
                        SqlParameter.Value = dicItemData[strKey + Common.DefineValue.FiledBegin];
                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;


                        SqlParameter = new SqlParameter("@" + strKey + Common.DefineValue.FiledEnd, DataFiledType.FiledType[strKey]);
                        //获取结束时间处理
                        SqlParameter.Value = dicItemData[strKey + Common.DefineValue.FiledEnd];
                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                }

                foreach (string strKey in dicLikeConds.Keys)
                {
                    if (dicItemData.ContainsKey(strKey) == true
                        && string.IsNullOrEmpty(dicItemData[strKey]) == false)
                    {
                        strSql += " AND " + strKey + " Like @" + strKey;

                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);
                        SqlParameter.Value = "%" + dicItemData[strKey] + "%";
                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                }


                if (!string.IsNullOrEmpty(OrderBy))
                {
                    strSql += " ORDER BY " + OrderBy;
                }
                
                w_RtnDataTable = Common.AdoConnect.Connect.GetDataSet(strSql, cmdParamters);

                return w_RtnDataTable;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
       /// <summary>
       /// 查询  由于筛选需要  定义此方法
       /// 查询条件在外部定义
       /// </summary>
       /// <param name="Sql"></param>
       /// <returns></returns>
        public DataTable GetTableInfoBySqlNoWhere(String Sql)
        {
             
            DataTable w_RtnDataTable = null; 
            try
            {

                string strSql = Sql;

                Common.AdoConnect.Connect.ConnectOpen();

                w_RtnDataTable = Common.AdoConnect.Connect.GetDataSet(strSql);

                return w_RtnDataTable;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 获取结束时间处理
        /// </summary>
        /// <param name="strEndTime"></param>
        /// <returns></returns>
        private string GetEndTime(string strEndTime)
        {
            DateTime dtEnd;
            string w_strRtnData;

            try
            {

                w_strRtnData=strEndTime;

                if (DateTime .TryParse (strEndTime, out dtEnd)==true)
                {
                    w_strRtnData = dtEnd.AddSeconds(1).ToString("yyyy/MM/dd HH:mm");
                }
            }
            catch (Exception ex )
            {         
                throw ex;
            }
            return w_strRtnData;
        }

        #endregion

        #region 数据检查查询(重复检查)

        /// <summary>
        /// 数据重名检查(主键重复)
        /// </summary>
        /// <param name="TableName">数据表名称</param>
        /// <param name="dicItemData">数据内容</param>
        /// <param name="dicPrimarName">主键列名</param>
        /// <param name="strRepFiledName">重名检测列名</param>
        /// <param name="ScanMode">画面模式</param>
        /// <returns></returns>
        public bool GetRepNameCheck(String TableName,
                                     StringDictionary dicItemData,
                                     StringDictionary dicPrimarName,
                                     String strRepFiledName,
                                     Common.DataModifyMode ScanMode)
        {
            bool IsExist = false;
            DataTable dt = new DataTable();
            int rowIndex = 0;
            SqlParameter[] cmdParamters = null;
            SqlParameter SqlParameter = null;

            string w_strWhere = "";
            strRepFiledName = strRepFiledName.ToLower();

            try
            {
                if (dicItemData == null || dicItemData.Count <= 0
                    || dicPrimarName == null || dicPrimarName.Count <= 0
                    || string.IsNullOrEmpty(strRepFiledName) == true) return false;

                string strSql = "SELECT  CAST('0' AS Bit) AS SlctValue," + TableName + ".* "
                             + " FROM " + TableName
                             + " WHERE 1=1 ";

                Common.AdoConnect.Connect.ConnectOpen();

                cmdParamters = new SqlParameter[dicPrimarName.Count + 1];

                foreach (string strKey in dicPrimarName.Keys)
                {
                    if (dicItemData.ContainsKey(strKey) == true && ScanMode == Common.DataModifyMode.upd)
                    {
                        w_strWhere += " AND " + strKey + "<>@" + strKey;

                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);
                        SqlParameter.Value = dicItemData[strKey];
                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                }


               if (dicItemData.ContainsKey(strRepFiledName) == true)
                {
                    w_strWhere += " AND " + strRepFiledName + "=@" + strRepFiledName;

                    SqlParameter = new SqlParameter("@" + strRepFiledName, DataFiledType.FiledType[strRepFiledName]);
                    SqlParameter.Value = dicItemData[strRepFiledName];
                    SqlParameter.Direction = ParameterDirection.Input;
                    cmdParamters[rowIndex] = SqlParameter;
                }

                 strSql += w_strWhere;

                 dt = Common.AdoConnect.Connect.GetDataSet(strSql, cmdParamters);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                        IsExist = true;
                }
                return IsExist;


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// 数据存在检查(主键重复)
        /// </summary>
        /// <param name="TableName">数据表名称</param>
        /// <param name="dicItemData">数据内容</param>
        /// <param name="dicPrimarName">主键列名</param>
        /// <returns></returns>
        public bool  GetExistDataItem(String TableName,
                                     StringDictionary dicItemData,
                                     StringDictionary dicPrimarName)
        {

            bool IsExist = false;
            DataTable dt = new DataTable();
            int rowIndex = 0;
            SqlParameter[] cmdParamters = null;
            SqlParameter SqlParameter = null;

            string w_strWhere = "";

            try
            {
                if (dicItemData == null || dicItemData.Count <= 0
                    || dicPrimarName == null || dicPrimarName.Count <= 0) return false ;


                string strSql = "SELECT  CAST('0' AS Bit) AS SlctValue," + TableName + ".* "
                             + " FROM " + TableName
                             + " WHERE 1=1 ";

                Common.AdoConnect.Connect.ConnectOpen();

                cmdParamters = new SqlParameter[dicPrimarName.Count];

                foreach (string strKey in dicPrimarName.Keys)
                {
                    if (dicItemData.ContainsKey(strKey) == true)
                    {

                        w_strWhere += " AND " + strKey + "=@" + strKey;

                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);
                        SqlParameter.Value = dicItemData[strKey];
                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                }

                strSql += w_strWhere;

                dt = Common.AdoConnect.Connect.GetDataSet(strSql, cmdParamters);
                if (dt != null)
                {
                    if (dt.Rows.Count>0)
                        IsExist = true;
                }
                return IsExist;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #endregion

        #region 下拉框控件绑定设置

        /// <summary>
        /// 下拉框控件绑定设置
        /// </summary>
        /// <param name="strTableName">数据表名称</param>
        /// <param name="blnAllVisible">是否显示全</param>
        /// <param name="itemAllNo">默认全部(Key：-1)</param>
        /// <param name="itemAllText">默认全部(Value：全部)</param>
        /// <param name="lue">下拉框控件</param>
         public bool SetLoopUpEdit(string strTableName,
                                    DevExpress.XtraEditors.LookUpEdit lue,
                                    Boolean blnAllVisible,
                                    string itemAllNo,
                                    string itemAllText)
        {

            DataTable tblSource = new DataTable();
            DataRow dr ;
 
            try
            {
                if (strTableName == null) return false;

                string strSql = "SELECT  * FROM " + strTableName 
                              + " WHERE 1=1 ";

                Common.AdoConnect.Connect.ConnectOpen();

                tblSource = Common.AdoConnect.Connect.GetDataSet(strSql);

                if (tblSource == null) return false;

                if (blnAllVisible == true)
                {

                    dr = tblSource.NewRow();

                    dr[0] = itemAllNo;
                    dr[1] = itemAllText;

                    tblSource.Rows .InsertAt (dr,0);
                }

                //lue.Properties.DataSource = new DataSourceWithEmptyItem(tblSource.DefaultView);

                lue.Properties.DataSource = tblSource.DefaultView;
                lue.Properties.DropDownRows = tblSource.Rows .Count ;
                lue.Properties.ValueMember = tblSource.Columns[0].ColumnName;
                lue.Properties.DisplayMember = tblSource.Columns[1].ColumnName;
                
                if (tblSource.Rows.Count > 0)
                {
                    lue.EditValue = tblSource.Rows[0][0].ToString();
                    //lue.Text = tblSource.Rows[0][1].ToString();
                }
                lue.ItemIndex = 0;
                lue.Properties.BestFit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


         /// <summary>
         /// 下拉框控件绑定设置
         /// </summary>
         /// <param name="strTableName">数据表名称</param>
         /// <param name="blnAllVisible">是否显示全</param>
         /// <param name="itemAllNo">默认全部(Key：-1)</param>
         /// <param name="itemAllText">默认全部(Value：全部)</param>
         /// <param name="orderby">排序</param>
         /// <param name="lue">下拉框控件</param>
         public bool SetLoopUpEdit(string strTableName,
                                    DevExpress.XtraEditors.LookUpEdit lue,
                                    Boolean blnAllVisible,
                                    string itemAllNo,
                                    string itemAllText,string orderby)
         {

             DataTable tblSource = new DataTable();
             DataRow dr;

             try
             {
                 if (strTableName == null) return false;

                 string strSql = "SELECT  * FROM " + strTableName
                               + " WHERE 1=1 ";
                 if (orderby != "") 
                 {
                     strSql += " order by " + orderby;
                 }

                 Common.AdoConnect.Connect.ConnectOpen();

                 tblSource = Common.AdoConnect.Connect.GetDataSet(strSql);

                 if (tblSource == null) return false;

                 if (blnAllVisible == true)
                 {

                     dr = tblSource.NewRow();

                     dr[0] = itemAllNo;
                     dr[1] = itemAllText;

                     tblSource.Rows.InsertAt(dr, 0);
                 }

                 lue.Properties.DataSource = tblSource.DefaultView;
                 lue.Properties.DropDownRows = tblSource.Rows.Count;
                 lue.Properties.ValueMember = tblSource.Columns[0].ColumnName;
                 lue.Properties.DisplayMember = tblSource.Columns[1].ColumnName;

                 if (tblSource.Rows.Count > 0)
                 {
                     lue.EditValue = tblSource.Rows[0][0].ToString();
                     //lue.Text = tblSource.Rows[0][1].ToString();
                 }
                 lue.ItemIndex = 0;
                 lue.Properties.BestFit();
                 return true;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }


         /// <summary>
         /// 下拉框控件绑定设置
         /// </summary>
         /// <param name="strTableName">数据表名称</param>
         /// <param name="blnAllVisible">是否显示全</param>
         /// <param name="itemAllNo">默认全部(Key：-1)</param>
         /// <param name="itemAllText">默认全部(Value：全部)</param>
         /// <param name="lue">下拉框控件</param>
         /// <param name="initNo">默认初始化NO</param>
         public bool SetLoopUpEditInit(string strTableName,
                                    DevExpress.XtraEditors.LookUpEdit lue,
                                    Boolean blnAllVisible,
                                    string itemAllNo,
                                    string itemAllText,
                                    string initNo)
         {

             DataTable tblSource = new DataTable();
             DataRow dr;
             DataView dv ;

             try
             {
                 if (strTableName == null) return false;

                 string strSql = "SELECT  * FROM " + strTableName
                               + " WHERE 1=1 ";

                 Common.AdoConnect.Connect.ConnectOpen();

                 tblSource = Common.AdoConnect.Connect.GetDataSet(strSql);

                 if (tblSource == null) return false;

                 if (blnAllVisible == true)
                 {

                     dr = tblSource.NewRow();

                     dr[0] = itemAllNo;
                     dr[1] = itemAllText;

                     tblSource.Rows.InsertAt(dr, 0);
                 }

                 //lue.Properties.DataSource = new DataSourceWithEmptyItem(tblSource.DefaultView);

                 lue.Properties.DataSource = tblSource.DefaultView;
                 lue.Properties.DropDownRows = tblSource.Rows.Count;
                 lue.Properties.ValueMember = tblSource.Columns[0].ColumnName;
                 lue.Properties.DisplayMember = tblSource.Columns[1].ColumnName;


                 if (tblSource.Rows.Count > 0)
                 {

                     dv = new DataView  (tblSource);
                     dv.RowFilter = tblSource.Columns[0].ColumnName + " ='" + initNo + "'";

                     if (dv.Count > 0)
                     {
                         lue.EditValue = dv[0][0].ToString();
                         lue.Text = dv[0][1].ToString();
                     }
                     else
                     {
                         lue.EditValue = tblSource.Rows[0][0].ToString();
                         //lue.Text = tblSource.Rows[0][1].ToString();
                     }
                 }
                 lue.Properties.BestFit();
                 return true;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }


         /// <summary>
         /// GRid下拉框控件绑定设置
         /// </summary>
         /// <param name="strTableName">数据表名称</param>
         /// <param name="blnAllVisible">是否显示全</param>
         /// <param name="itemAllNo">默认全部(Key：-1)</param>
         /// <param name="itemAllText">默认全部(Value：全部)</param>
         /// <param name="lue">下拉框控件</param>
         public bool SetGridLoopUpEdit(string strTableName,
                                    DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit  lue,
                                    Boolean blnAllVisible,
                                    string itemAllNo,
                                    string itemAllText)
         {

             DataTable tblSource = new DataTable();
             DataRow dr;

             try
             {
                 if (strTableName == null) return false;

                 string strSql = "SELECT  * FROM " + strTableName
                               + " WHERE 1=1 ";

                 Common.AdoConnect.Connect.ConnectOpen();

                 tblSource = Common.AdoConnect.Connect.GetDataSet(strSql);

                 if (tblSource == null) return false;

                 if (blnAllVisible == true)
                 {

                     dr = tblSource.NewRow();

                     dr[0] = itemAllNo;
                     dr[1] = itemAllText;

                     tblSource.Rows.InsertAt(dr, 0);
                 }

                 //lue.Properties.DataSource = new DataSourceWithEmptyItem(tblSource.DefaultView);

                 lue.Properties.DataSource = tblSource.DefaultView;
                 lue.Properties.DropDownRows = tblSource.Rows.Count;
                 lue.Properties.ValueMember = tblSource.Columns[0].ColumnName;
                 lue.Properties.DisplayMember = tblSource.Columns[1].ColumnName;
                 lue.Properties.BestFit();
                 return true;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

         /// <summary>
         /// 下拉框控件绑定设置
         /// </summary>
         /// <param name="Sql">Sql语句</param>
         /// <param name="dicCondItem">精确查询数据项</param>
         /// <param name="blnAllVisible">是否显示全</param>
         /// <param name="itemAllNo">默认全部(Key：-1)</param>
         /// <param name="itemAllText">默认全部(Value：全部)</param>
         /// <param name="lue">下拉框控件</param>
         public bool SetLoopUpEditBySql(string Sql,
                                    StringDictionary dicCondItem,
                                    StringDictionary dicNotCondItem,
                                    DevExpress.XtraEditors.LookUpEdit lue,
                                    Boolean blnAllVisible,                                
                                    string itemAllNo,
                                    string itemAllText,string orderby)
         {

             DataTable tblSource = new DataTable();
             DataRow dr;
             int rowIndex = 0;
             SqlParameter[] cmdParamters = null;
             SqlParameter SqlParameter = null;


             try
             {
                 if (Sql == null) return false;

                 string strSql = Sql
                               + " WHERE 1=1 ";
                 cmdParamters = new SqlParameter[dicCondItem.Count + dicNotCondItem.Count ];

                 foreach (string strKey in dicCondItem.Keys)
                 {

                     strSql += " AND " + strKey + " =@" + strKey;

                     SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);
                     SqlParameter.Value = dicCondItem[strKey];
                     SqlParameter.Direction = ParameterDirection.Input;
                     cmdParamters[rowIndex] = SqlParameter;
                     rowIndex++;
                 }

                 if (orderby != "")
                 {
                     strSql = strSql + " order by " + orderby;
                 }

                 Common.AdoConnect.Connect.ConnectOpen();

                 tblSource = Common.AdoConnect.Connect.GetDataSet(strSql, cmdParamters);
               
                 if (tblSource == null) return false;

                 if (blnAllVisible == true)
                 {

                     dr = tblSource.NewRow();

                     dr[0] = itemAllNo;
                     dr[1] = itemAllText;

                     tblSource.Rows.InsertAt(dr, 0);
                 }

                 //lue.Properties.DataSource = new DataSourceWithEmptyItem(tblSource.DefaultView);

                 lue.Properties.DataSource = tblSource.DefaultView;
                 lue.Properties.DropDownRows = tblSource.Rows.Count;
                 lue.Properties.ValueMember = tblSource.Columns[0].ColumnName;
                 lue.Properties.DisplayMember = tblSource.Columns[1].ColumnName;

                 if (tblSource.Rows.Count > 0)
                 {
                     lue.EditValue = tblSource.Rows[0][0].ToString();
                     //lue.Text = tblSource.Rows[0][1].ToString();
                 }

                 lue.Properties.BestFit();

                 return true;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

        /// <summary>
         /// 下拉框控件LoopUpEdit绑定一列
        /// </summary>
        /// <param name="Sql">选择一列的sql语句</param>
         /// <param name="lue">LookUpEdit控件名</param>
        /// <param name="blnAllVisible">是否显示定义文本</param>
         /// <param name="itemAllText">第一行显示定义文本</param>
        /// <returns></returns>
         public bool SetLoopUpEditBySql(string Sql,
                                   DevExpress.XtraEditors.LookUpEdit lue,
                                   Boolean blnAllVisible,
                                   string itemAllText)
         {

             DataTable tblSource = new DataTable();
             DataRow dr;

             try
             {
                 if (Sql == null) return false;

                 Common.AdoConnect.Connect.ConnectOpen();

                 tblSource = Common.AdoConnect.Connect.GetDataSet(Sql);

                 if (tblSource == null) return false;
                 //加入请选择
                 if (blnAllVisible == true)
                 {
                     dr = tblSource.NewRow();
                     dr[0] = itemAllText;
                     tblSource.Rows.InsertAt(dr, 0);
                 }
                 lue.Properties.DataSource = tblSource.DefaultView;
                 lue.Properties.DropDownRows = tblSource.Rows.Count;
                 lue.Properties.ValueMember = tblSource.Columns[0].ColumnName;
                 lue.Properties.DisplayMember = tblSource.Columns[0].ColumnName;

                 if (tblSource.Rows.Count > 0)
                 {
                     //显示一列
                     lue.Text  = tblSource.Rows[0][0].ToString();
                 }

                 lue.Properties.BestFit();

                 return true;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         /// <summary>
         /// 下拉框控件绑定设置
         /// </summary>
         /// <param name="Sql">Sql语句</param>
         /// <param name="dicCondItem">精确查询数据项</param>
         /// <param name="blnAllVisible">是否显示全</param>
         /// <param name="itemAllNo">默认全部(Key：-1)</param>
         /// <param name="itemAllText">默认全部(Value：全部)</param>
         /// <param name="lue">下拉框控件</param>
         public bool SetLoopUpEditNotWhereBySql(string Sql,
                                    StringDictionary dicCondItem,
                                    StringDictionary dicNotCondItem,
                                    DevExpress.XtraEditors.LookUpEdit lue,
                                    Boolean blnAllVisible,
                                    string itemAllNo,
                                    string itemAllText)
         {

             DataTable tblSource = new DataTable();
             DataRow dr;
             int rowIndex = 0;
             SqlParameter[] cmdParamters = null;
             SqlParameter SqlParameter = null;


             try
             {
                 if (Sql == null) return false;

                 string strSql = Sql;

                 cmdParamters = new SqlParameter[dicCondItem.Count + dicNotCondItem.Count];

                 foreach (string strKey in dicCondItem.Keys)
                 {

                     strSql += " AND " + strKey + " =@" + strKey;

                     SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);
                     SqlParameter.Value = dicCondItem[strKey];
                     SqlParameter.Direction = ParameterDirection.Input;
                     cmdParamters[rowIndex] = SqlParameter;
                     rowIndex++;
                 }

                 Common.AdoConnect.Connect.ConnectOpen();

                 tblSource = Common.AdoConnect.Connect.GetDataSet(strSql, cmdParamters);

                 if (tblSource == null) return false;

                 if (blnAllVisible == true)
                 {

                     dr = tblSource.NewRow();

                     dr[0] = itemAllNo;
                     dr[1] = itemAllText;

                     tblSource.Rows.InsertAt(dr, 0);
                 }

                 //lue.Properties.DataSource = new DataSourceWithEmptyItem(tblSource.DefaultView);

                 lue.Properties.DataSource = tblSource.DefaultView;
                 lue.Properties.DropDownRows = tblSource.Rows.Count;
                 lue.Properties.ValueMember = tblSource.Columns[0].ColumnName;
                 lue.Properties.DisplayMember = tblSource.Columns[1].ColumnName;

                 if (tblSource.Rows.Count > 0)
                 {
                     lue.EditValue = tblSource.Rows[0][0].ToString();
                     //lue.Text = tblSource.Rows[0][1].ToString();
                 }

                 lue.Properties.BestFit();

                 return true;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
        
         /// <summary>
         /// 下拉框控件绑定设置
         /// </summary>
         /// <param name="strTableName">数据表名称</param>
         /// <param name="dicCondItem">精确查询数据项</param>
         /// <param name="blnAllVisible">是否显示全</param>
         /// <param name="itemAllNo">默认全部(Key：-1)</param>
         /// <param name="itemAllText">默认全部(Value：全部)</param>
         /// <param name="lue">下拉框控件</param>
         public bool SetLoopUpEdit(string strTableName,
                                    StringDictionary dicCondItem,
                                    DevExpress.XtraEditors.LookUpEdit lue,
                                    Boolean blnAllVisible,                                
                                    string itemAllNo,
                                    string itemAllText)
         {

             DataTable tblSource = new DataTable();
             DataRow dr;
             int rowIndex = 0;
             SqlParameter[] cmdParamters = null;
             SqlParameter SqlParameter = null;


             try
             {
                 if (strTableName == null) return false;

                 string strSql = "SELECT  * FROM " + strTableName
                               + " WHERE 1=1 ";

                 cmdParamters = new SqlParameter[dicCondItem.Count];

                 foreach (string strKey in dicCondItem.Keys)
                 {

                     strSql += " AND " + strKey + " =@" + strKey;

                     SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);
                     SqlParameter.Value = dicCondItem[strKey];
                     SqlParameter.Direction = ParameterDirection.Input;
                     cmdParamters[rowIndex] = SqlParameter;
                     rowIndex++;
                 }

                 Common.AdoConnect.Connect.ConnectOpen();

                 tblSource = Common.AdoConnect.Connect.GetDataSet(strSql, cmdParamters);
               
                 if (tblSource == null) return false;

                 if (blnAllVisible == true)
                 {

                     dr = tblSource.NewRow();

                     dr[0] = itemAllNo;
                     dr[1] = itemAllText;

                     tblSource.Rows.InsertAt(dr, 0);
                 }

                 //lue.Properties.DataSource = new DataSourceWithEmptyItem(tblSource.DefaultView);

                 lue.Properties.DataSource = tblSource.DefaultView;
                 lue.Properties.DropDownRows = tblSource.Rows.Count;
                 lue.Properties.ValueMember = tblSource.Columns[0].ColumnName;
                 lue.Properties.DisplayMember = tblSource.Columns[1].ColumnName;

                 if (tblSource.Rows.Count > 0)
                 {
                     lue.EditValue = tblSource.Rows[0][0].ToString();
                     //lue.Text = tblSource.Rows[0][1].ToString();
                 }

                 lue.Properties.BestFit();

                 return true;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

         /// <summary>
         /// 下拉框控件绑定设置  和上面方法区别，不设置下面“此处” 后两行代码
         /// 而是在  使用控件中设置改2个属性 
         /// 同时传递数据表中的列名  去掉空行
         /// ziven  
         /// </summary>
         /// <param name="strTableName">数据表名称</param>
         /// <param name="blnAllVisible">是否显示全</param>
         /// <param name="itemAllNo">默认全部(Key：-1)</param>
         /// <param name="itemAllText">默认全部(Value：全部)</param>
         /// <param name="lue">下拉框控件</param>
         /// dtcolumn  绑定的table中的哪一列
         public bool SetLoopUpEditOverLoad(string strTableName,
                                    DevExpress.XtraEditors.LookUpEdit lue,
                                    string dtcolumn,
                                    Boolean blnAllVisible=false,
                                    string itemText="")
         {

             DataTable tblSource = new DataTable();
             DataRow dr;  
             try
             {
                 if (strTableName == null) return false;

                 string strSql = "SELECT  * FROM " + strTableName
                               + " WHERE 1=1 and " + dtcolumn+"<>''";

                 Common.AdoConnect.Connect.ConnectOpen();

                 tblSource = Common.AdoConnect.Connect.GetDataSet(strSql);

                 if (tblSource == null) return false;
                 
                 if (blnAllVisible == true)
                 {

                     dr = tblSource.NewRow();

                     dr[dtcolumn] = itemText;
                      
                     tblSource.Rows.InsertAt(dr, 0);
                 }

                 //lue.Properties.DataSource = new DataSourceWithEmptyItem(tblSource.DefaultView);

                 lue.Properties.DataSource = tblSource.DefaultView;
                 lue.Properties.DropDownRows = tblSource.Rows.Count;
                 //此处
                 //lue.Properties.ValueMember = tblSource.Columns[0].ColumnName;
                 //lue.Properties.DisplayMember = tblSource.Columns[1].ColumnName;

                 if (tblSource.Rows.Count > 0)
                 {
                     lue.EditValue = tblSource.Rows[0][0].ToString();
                     //lue.Text = tblSource.Rows[0][1].ToString();
                 }
                 lue.Properties.BestFit();
                 return true;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

        #endregion

        #region 数据新增部分

        /// <summary>
        /// 新增基本信息数据
        /// </summary>
        /// <param name="TableName">数据表名称</param>
        /// <param name="dicItemData">数据内容</param>
        /// <param name="dicUserColum">操作员信息列名</param>
        /// <returns></returns>
        public int SetInsertDataItem(String TableName, 
                                     StringDictionary dicItemData,
                                     StringDictionary dicUserColum)
        {

            int rowIndex = 0;
            int w_RtnCnt = 0;
            SqlParameter[] cmdParamters = null;
            SqlParameter SqlParameter = null;

            string w_strFileds="";
            string w_strValues = "";

            try
            {
                if (dicItemData == null || dicItemData.Count <= 0) return w_RtnCnt;
    
                string strSql = "INSERT INTO " + TableName + " ( ";

                Common.AdoConnect.Connect.ConnectOpen();

                cmdParamters = new SqlParameter[dicItemData.Count + dicUserColum.Count + dicUserColum.Count];
                
                foreach (string strKey in dicItemData.Keys)
                {
                    w_strFileds +="," + strKey ;
                    w_strValues += ", @" + strKey;

                    SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);
                    SqlParameter.Value = dicItemData[strKey];
                    SqlParameter.Direction = ParameterDirection.Input;
                    cmdParamters[rowIndex] = SqlParameter;
                    rowIndex++;
                }

                foreach (string strKey in dicUserColum.Keys)
                {
                    if (strKey.Equals(Common.UserColum.AddDateTime) == true || strKey.Equals(Common.UserColum.UpdDateTime) == true)
                    {
                        w_strFileds += "," + strKey;
                        w_strValues += ", CONVERT(VARCHAR, GETDATE(), 20) " ;

                        
                    }
                    else if (strKey.Equals(Common.UserColum.AddUserNo) == true || strKey.Equals(Common.UserColum.UpdUserNo) == true)
                    {
                        w_strFileds += "," + strKey;
                        w_strValues += ", @" + strKey;

                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);
                        SqlParameter.Value = Common._personid;
                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                    else if (strKey.Equals(Common.UserColum.UpdDate) == true)
                    {
                        w_strFileds += "," + strKey;
                        w_strValues += ", CONVERT(VARCHAR, GETDATE(), 20) ";

                    }
                    else if (strKey.Equals(Common.UserColum.UpdUserID) == true)
                    {
                        w_strFileds += "," + strKey;
                        w_strValues += ", @" + strKey;

                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);
                        SqlParameter.Value = Common._personid;
                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }            
                }

                strSql += w_strFileds.Substring (1)  +" )  VALUES (";
                strSql +=w_strValues.Substring (1)  +" )";

                w_RtnCnt = Common.AdoConnect.Connect.ExecuteNonQuery(strSql, cmdParamters);
                    
                return w_RtnCnt;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// 新增基本信息数据
        /// </summary>
        /// <param name="TableName">数据表名称</param>
        /// <param name="dicItemFiledNm">数据字段名称</param>
        /// <param name="dicItemData">数据内容</param>
        /// <param name="dicUserColum">操作员信息列名</param>
        /// <returns></returns>
        public int SetInsertDataItem(String TableName,
                                     StringDictionary dicItemFiledNm,
                                     StringDictionary dicItemData,
                                     StringDictionary dicUserColum)
        {
            int rowIndex = 0;
            int w_RtnCnt = 0;
            SqlParameter[] cmdParamters = null;
            SqlParameter SqlParameter = null;

            string w_strFileds = "";
            string w_strValues = "";

            try
            {
                if (dicItemData == null || dicItemData.Count <= 0) return w_RtnCnt;

                string strSql = "INSERT INTO " + TableName + " ( ";

                Common.AdoConnect.Connect.ConnectOpen();

                cmdParamters = new SqlParameter[dicItemData.Count + dicUserColum.Count + dicUserColum.Count];

                foreach (string strKey in dicItemFiledNm.Keys)
                {              
                    if (dicItemData.ContainsKey (strKey)==true)
                    {
                        w_strFileds += "," + strKey;
                        w_strValues += ", @" + strKey;

                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);
                        SqlParameter.Value = dicItemData[strKey];
                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                }

                foreach (string strKey in dicUserColum.Keys)
                {
                    if (strKey.Equals(Common.UserColum.AddDateTime) == true || strKey.Equals(Common.UserColum.UpdDateTime) == true)
                    {
                        w_strFileds += "," + strKey;
                        w_strValues += ", CONVERT(VARCHAR, GETDATE(), 20) ";


                    }
                    else if (strKey.Equals(Common.UserColum.AddUserNo) == true || strKey.Equals(Common.UserColum.UpdUserNo) == true)
                    {
                        w_strFileds += "," + strKey;
                        w_strValues += ", @" + strKey;

                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);
                        SqlParameter.Value = Common._personid;
                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                    else if (strKey.Equals(Common.UserColum.UpdDate) == true)
                    {
                        w_strFileds += "," + strKey;
                        w_strValues += ", CONVERT(VARCHAR, GETDATE(), 20) ";

                    }
                    else if (strKey.Equals(Common.UserColum.UpdUserID) == true)
                    {
                        w_strFileds += "," + strKey;
                        w_strValues += ", @" + strKey;

                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);
                        SqlParameter.Value = Common._personid;
                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }       
                }

                strSql += w_strFileds.Substring(1) + " )  VALUES (";
                strSql += w_strValues.Substring(1) + " )";

                w_RtnCnt = Common.AdoConnect.Connect.ExecuteNonQuery(strSql, cmdParamters);

                return w_RtnCnt;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 新增基本信息数据  调用前面的
        /// </summary>
        /// <param name="TableName">数据表名称</param>
        /// <param name="dicItemData">数据内容</param>
        /// <param name="dicUserColum">操作员信息列名</param>
        /// <returns></returns>
        public int SetInsertDataItem(String TableName,
                                     StringDictionary dicItemData)
        {

            int rowIndex = 0;
            int w_RtnCnt = 0;
            

            string _strFileds = "";
            string _strValues = "";

            try
            {
                if (dicItemData == null || dicItemData.Count <= 0) return w_RtnCnt;

                string strSql = "INSERT INTO " + TableName + " ( ";

                Common.AdoConnect.Connect.ConnectOpen();
                foreach (string strKey in dicItemData.Keys)
                {
                    if (rowIndex==(dicItemData.Count-1))
                    {
                        _strFileds+=strKey;
                        _strValues +="'"+dicItemData[strKey].ToString()+"'";
                    }
                    else
                    {
                        _strFileds += strKey+",";
                        _strValues += "'" + dicItemData[strKey].ToString() + "',";
                    }
                    rowIndex++;
                }

                strSql += _strFileds + " )  VALUES (";
                strSql += _strValues + " )";

                w_RtnCnt = Common.AdoConnect.Connect.ExecuteNonQuery(strSql);

                return w_RtnCnt;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 数据修改部分

        /// <summary>
        /// 修改基本信息数据
        /// </summary>
        /// <param name="TableName">数据表名称</param>
        /// <param name="dicItemData">数据内容</param>
        /// <param name="dicPrimarName">主键列名</param>
        /// <param name="dicUserColum">操作员信息列名</param>
        /// <returns></returns>
        public int SetModifyDataItem(String TableName,
                                     StringDictionary dicItemData,
                                     StringDictionary dicPrimarName,
                                     StringDictionary dicUserColum)
        {
            int rowIndex = 0;
            int w_RtnCnt = 0;
            SqlParameter[] cmdParamters = null;
            SqlParameter SqlParameter = null;

            string w_strSetValues = "";
            string w_strWhere = "";

            try
            {
                if (dicItemData == null || dicItemData.Count <= 0 || dicPrimarName == null) return w_RtnCnt;

                string strSql = "UPDATE " + TableName + " Set ";

                Common.AdoConnect.Connect.ConnectOpen();

                cmdParamters = new SqlParameter[dicItemData.Count + dicUserColum.Count ];

                foreach (string strKey in dicItemData.Keys)
                {
                    if (dicPrimarName.ContainsKey(strKey) != true)
                    {
                        w_strSetValues += "," + strKey + "=@" + strKey;
                    }

                    SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);

                    if (SqlParameter.SqlDbType  == SqlDbType.Bit)
                    {
                        SqlParameter.Value = bool.Parse(dicItemData[strKey]);
                    }
                    else {
                        SqlParameter.Value = dicItemData[strKey];
                    }
                    
                    SqlParameter.Direction = ParameterDirection.Input;
                    cmdParamters[rowIndex] = SqlParameter;
                    rowIndex++;
                }


                foreach (string strKey in dicUserColum.Keys)
                {
                    if (strKey.Equals(Common.UserColum.UpdDateTime) == true)
                    {
                        w_strSetValues += "," + strKey + "=CONVERT(VARCHAR, GETDATE(), 20) ";

                    }
                    else if (strKey.Equals(Common.UserColum.UpdDate) == true) 
                    {
                        w_strSetValues += "," + strKey + "=CONVERT(VARCHAR, GETDATE(), 20) ";
                    }
                    else if (strKey.Equals(Common.UserColum.UpdUserNo) == true)
                    {
                        w_strSetValues += "," + strKey + "=@" + strKey;

                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);
                        SqlParameter.Value = Common._personid;
                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                    else if (strKey.Equals(Common.UserColum.UpdUserID) == true)
                    {
                        w_strSetValues += "," + strKey + "=@" + strKey;

                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);
                        SqlParameter.Value = Common._personid;
                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                }

                strSql += w_strSetValues.Substring(1) + " WHERE 1=1";

                foreach (string strKey in dicPrimarName.Keys)
                {
                    w_strWhere += " AND " + strKey + "=@" + strKey;
                }

                strSql += w_strWhere;

                w_RtnCnt = Common.AdoConnect.Connect.ExecuteNonQuery(strSql, cmdParamters);

                return w_RtnCnt;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 修改基本信息数据
        /// </summary>
        /// <param name="Sql">SQL语句</param>
        /// <param name="dicItemData">数据内容</param>
        /// <param name="dicPrimarName">主键列名</param>
        /// <param name="dicUserColum">操作员信息列名</param>
        /// <returns></returns>
        public int SetModifyDataItemBySql(String Sql,
                                     StringDictionary dicItemData,
                                     StringDictionary dicPrimarName,
                                     StringDictionary dicUserColum)
        {
            int rowIndex = 0;
            int w_RtnCnt = 0;
            SqlParameter[] cmdParamters = null;
            SqlParameter SqlParameter = null;

            string w_strSetValues = "";
            string w_strWhere = "";

            try
            {
                if (dicItemData == null || dicPrimarName == null) return w_RtnCnt;

                string strSql = Sql;

                Common.AdoConnect.Connect.ConnectOpen();

                cmdParamters = new SqlParameter[dicItemData.Count + dicUserColum.Count];

                foreach (string strKey in dicItemData.Keys)
                {
     
                    SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);

                    if (SqlParameter.SqlDbType == SqlDbType.Bit)
                    {
                        SqlParameter.Value = bool.Parse(dicItemData[strKey]);
                    }
                    else
                    {
                        SqlParameter.Value = dicItemData[strKey];
                    }

                    SqlParameter.Direction = ParameterDirection.Input;
                    cmdParamters[rowIndex] = SqlParameter;
                    rowIndex++;
                }

                foreach (string strKey in dicUserColum.Keys)
                {
                    if (strKey.Equals(Common.UserColum.UpdDateTime) == true)
                    {
                        w_strSetValues += "," + strKey + "=CONVERT(VARCHAR, GETDATE(), 20) ";

                    }
                    else if (strKey.Equals(Common.UserColum.UpdUserNo) == true)
                    {
                        w_strSetValues += "," + strKey + "=@" + strKey;

                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);
                        SqlParameter.Value = Common._personid;
                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                    else if (strKey.Equals(Common.UserColum.UpdDate) == true)
                    {
                        w_strSetValues += "," + strKey + "=CONVERT(VARCHAR, GETDATE(), 20) ";
                    }
                    else if (strKey.Equals(Common.UserColum.UpdUserID) == true)
                    {
                        w_strSetValues += "," + strKey + "=@" + strKey;

                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);
                        SqlParameter.Value = Common._personid;
                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                }

                if (w_strSetValues.Length >1)
                strSql += w_strSetValues.Substring(1) + " WHERE 1=1";

                foreach (string strKey in dicPrimarName.Keys)
                {
                    w_strWhere += " AND " + strKey + "=@" + strKey;
                }

                strSql += w_strWhere;

                w_RtnCnt = Common.AdoConnect.Connect.ExecuteNonQuery(strSql, cmdParamters);

                return w_RtnCnt;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 修改基本信息数据
        /// </summary>
        /// <param name="TableName">数据表名称</param>
        /// <param name="dicItemFiledNm">数据字段名称</param>
        /// <param name="dicItemData">数据内容</param>
        /// <param name="dicPrimarName">主键列名</param>
        /// <param name="dicUserColum">操作员信息列名</param>
        /// <returns></returns>
        public int SetModifyDataItem(String TableName,
                                     StringDictionary dicItemFiledNm,
                                     StringDictionary dicItemData,
                                     StringDictionary dicPrimarName,
                                     StringDictionary dicUserColum)
        {
            int rowIndex = 0;
            int w_RtnCnt = 0;
            SqlParameter[] cmdParamters = null;
            SqlParameter SqlParameter = null;

            string w_strSetValues = "";
            string w_strWhere = "";

            try
            {
                if (dicItemData == null || dicItemData.Count <= 0 || dicPrimarName == null) return w_RtnCnt;

                string strSql = "UPDATE " + TableName + " Set ";

                Common.AdoConnect.Connect.ConnectOpen();

                cmdParamters = new SqlParameter[dicItemData.Count + dicUserColum.Count];

                foreach (string strKey in dicItemFiledNm.Keys)
                {
                    if (dicPrimarName.ContainsKey(strKey) != true)
                    {
                        w_strSetValues += "," + strKey + "=@" + strKey;
                    }

                    if (dicItemData.ContainsKey(strKey) == true)
                    {
                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);

                        if (SqlParameter.SqlDbType == SqlDbType.Bit)
                        {
                            SqlParameter.Value = bool.Parse(dicItemData[strKey]);
                        }
                        else
                        {
                            SqlParameter.Value = dicItemData[strKey];
                        }

                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                }

                foreach (string strKey in dicUserColum.Keys)
                {
                    if (strKey.Equals(Common.UserColum.UpdDateTime) == true)
                    {
                        w_strSetValues += "," + strKey + "=CONVERT(VARCHAR, GETDATE(), 20) ";

                    }
                    else if (strKey.Equals(Common.UserColum.UpdUserNo) == true)
                    {
                        w_strSetValues += "," + strKey + "=@" + strKey;

                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);
                        SqlParameter.Value = Common._personid;
                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                    else if (strKey.Equals(Common.UserColum.UpdDate) == true)
                    {
                        w_strSetValues += "," + strKey + "=CONVERT(VARCHAR, GETDATE(), 20) ";
                    }
                    else if (strKey.Equals(Common.UserColum.UpdUserID) == true)
                    {
                        w_strSetValues += "," + strKey + "=@" + strKey;

                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);
                        SqlParameter.Value = Common._personid;
                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                } 

                strSql += w_strSetValues.Substring(1) + " WHERE 1=1";

                foreach (string strKey in dicPrimarName.Keys)
                {
                    w_strWhere += " AND " + strKey + "=@" + strKey;
                }

                strSql += w_strWhere;

                w_RtnCnt = Common.AdoConnect.Connect.ExecuteNonQuery(strSql, cmdParamters);

                return w_RtnCnt;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 修改基本信息数据 前面的方法总是类型报错 重载此方法
        /// </summary>
        /// <param name="TableName">数据表名称</param>
        /// <param name="dicItemData">数据内容</param>
        /// <param name="dicPrimarName">主键列名</param>
        /// <param name="dicUserColum">操作员信息列名</param>
        /// <returns></returns>
        public int SetModifyDataItem(String TableName,
                                     StringDictionary dicItemData,
                                     StringDictionary dicPrimarName)
        {
            int rowIndex = 0;
            int w_RtnCnt = 0;
            string _strFiles = "";
            string _strSetValues = "";
            string _strWhere = "";

            try
            {
                if (dicItemData == null || dicItemData.Count <= 0 || dicPrimarName == null) return w_RtnCnt;

                string strSql = "UPDATE " + TableName + " Set ";

                Common.AdoConnect.Connect.ConnectOpen();

                foreach (string strKey in dicItemData.Keys)
                {
                    if (rowIndex==(dicItemData.Count-1))
                    {
                        _strFiles += strKey + "='" + dicItemData[strKey].ToString() + "'";
                    }
                    else
                    {
                        _strFiles +=strKey + "='" + dicItemData[strKey].ToString() + "',";
                    }

                    rowIndex++;
                }
                rowIndex = 0;
                strSql += _strFiles; 
                foreach (string strKey in dicPrimarName.Keys)
                {
                    //_strWhere += " AND " + strKey + "=@" + strKey;
                    if (dicItemData.ContainsKey(strKey))
                    {
                        if (rowIndex==0)
                        {
                            strSql += " where " + strKey + "='" + dicItemData[strKey].ToString() + "'";
                       
                        }
                        else
                        {
                             strSql += " and "+strKey + "='" + dicItemData[strKey].ToString() + "'";
                        }
                         
                    }
                   
                }
                w_RtnCnt = Common.AdoConnect.Connect.ExecuteNonQuery(strSql);

                return w_RtnCnt;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 更新包括自增长的列[ID] [int] IDENTITY(1,1)
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="dicItemData"></param>
        /// <param name="dicPrimarName"></param>
        /// <returns></returns>
        public int SetModifyDataIdentityColumn(String TableName,
                             StringDictionary dicItemData,
                             StringDictionary dicIdentityName)
        {
            int rowIndex = 0;
            int w_RtnCnt = 0;
            string _strFiles = "";

            try
            {
                if (dicItemData == null || dicItemData.Count <= 0 || dicIdentityName == null) return w_RtnCnt;

                string strSql = "UPDATE " + TableName + " Set ";

                Common.AdoConnect.Connect.ConnectOpen();

                foreach (string strKey in dicItemData.Keys)
                {
                    if (rowIndex == (dicItemData.Count - 1))
                    {
                        _strFiles += strKey + "='" + dicItemData[strKey].ToString() + "'";
                    }
                    else
                    {
                        _strFiles += strKey + "='" + dicItemData[strKey].ToString() + "',";
                    }

                    rowIndex++;
                }
                rowIndex = 0;
                strSql += _strFiles;
                //遍历dicIdentityName的列
                foreach (string strKey in dicIdentityName.Keys)
                {
                    if (rowIndex == 0)
                    {
                        strSql += " where " + strKey + "='" + dicIdentityName[strKey].ToString() + "'";

                    }
                    else
                    {
                        strSql += " and " + strKey + "='" + dicIdentityName[strKey].ToString() + "'";
                    }
                }
                w_RtnCnt = Common.AdoConnect.Connect.ExecuteNonQuery(strSql);

                return w_RtnCnt;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 数据删除部分

        /// <summary>
        /// 删除基本信息数据
        /// </summary>
        /// <param name="TableName">数据表名称</param>
        /// <param name="dicItemData">数据内容</param>
        /// <param name="dicPrimarName">主键列名</param>
        /// <returns></returns>
        public int SetDeleteDataItem(String TableName,
                                     StringDictionary dicItemData,
                                     StringDictionary dicPrimarName)
        {
            int rowIndex = 0;
            int w_RtnCnt = 0;
            SqlParameter[] cmdParamters = null;
            SqlParameter SqlParameter = null;

            string w_strWhere = "";

            try
            {
                if (dicItemData == null || dicPrimarName == null ) return w_RtnCnt;

 
                string strSql = "DELETE " + TableName + " WHERE 1=1";

                Common.AdoConnect.Connect.ConnectOpen();

                cmdParamters = new SqlParameter[dicPrimarName.Count];

                foreach (string strKey in dicPrimarName.Keys)
                {
                    if (dicItemData.ContainsKey(strKey) == true)
                    {

                        w_strWhere += " AND " + strKey + "=@" + strKey;

                        SqlParameter = new SqlParameter("@" + strKey, DataFiledType.FiledType[strKey]);
                        SqlParameter.Value = dicItemData[strKey];
                        SqlParameter.Direction = ParameterDirection.Input;
                        cmdParamters[rowIndex] = SqlParameter;
                        rowIndex++;
                    }
                }
                              
                strSql += w_strWhere;

                w_RtnCnt = Common.AdoConnect.Connect.ExecuteNonQuery(strSql, cmdParamters);

                return w_RtnCnt;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region 记录日志
        /// <summary>
        /// 记录日志信息
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <param name="functName">功能名称</param>
        /// <param name="OperType">操作类型</param>
        /// <param name="OperateContent">记录内容</param>
        public void WriteLog(string moduleName, string functName, string OperateContent)
        {
            StringDictionary w_dataItem=new StringDictionary();

            //StringDictionary w_dicUserColum = new StringDictionary();
            //w_dicUserColum[Common.UserColum.AddDateTime] = "true";
            //w_dicUserColum[Common.UserColum.AddUserNo] = "true";

            try
            {
                w_dataItem[SetLog.moduleName] = moduleName;
                w_dataItem[SetLog.functName] = functName;
                w_dataItem[SetLog.Memo] = OperateContent;
                w_dataItem[SetLog.myTeamName] = Common._myTeamName;
                w_dataItem[SetLog.OperNo] = Common._personid;
                w_dataItem[SetLog.OperName] = Common._personname;
                w_dataItem[SetLog.OperDate] = GetServerTime().ToString("yyyy-MM-dd HH:mm:ss");

                this.SetInsertDataItem(SetLog.TableName, w_dataItem);
            }
            catch (Exception ex)
            {

                Log.Error(ex);
            }
        }

        #endregion

        #region 获取当前系统时间

        public DateTime GetServerTime()
        {

            return Common.AdoConnect.Connect.GetServerTime();

        }

        #endregion
    }
}
