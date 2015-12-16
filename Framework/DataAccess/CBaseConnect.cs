using System.Data;
using Framework.Libs;
using System;

namespace Framework.DataAccess
{
   public interface CBaseConnect
   {


        #region 属性设置

        string strConnectString { get; set; }
        string strServerNm { get; set; }
        string strDataBase { get; set; }     
        string strUserName { get; set; }
        string strPassWord { get; set; }

        #endregion

        #region 数据库初始化处理

        /// <summary>
        /// 数据库连接参数设置
        /// </summary>
        void SetParameter();

        /// <summary>
        /// 数据库连接参数设置
        /// </summary>
        /// <param name="strServerNm">服务器地址</param>
        /// <param name="strDataBaseNm">数据库名</param>
        /// <param name="strUserNm">用户名</param>
        /// <param name="strPswd">用户密码</param>
       void SetParameter(string strServerNm, string strDataBaseNm, string strUserNm, string strPswd);

         /// <summary>
         /// 数据库连接打开处理
         /// </summary>
       /// <param name="intDBtype">数据源类型</param>
         /// <returns></returns>
       bool ConnectOpen();

       /// <summary>
       /// 数据库连接关闭处理
       /// </summary>
       /// <param name="intDBtype">数据源类型</param>
       /// <returns></returns>
       bool ConnectClose();

       /// <summary>
       /// 数据库创建事务处理
       /// </summary>
       /// <param name="intDBtype">数据源类型</param>
       /// <returns></returns>
       bool CreateSqlTransaction();

       /// <summary>
       /// 数据库事务回滚处理
       /// </summary>
       /// <param name="intDBtype">数据源类型</param>
       /// <returns></returns>
       bool TransactionRollback();

       /// <summary>
       /// 数据库事务提交处理
       /// </summary>
       /// <param name="intDBtype">数据源类型</param>
       /// <returns></returns>
       bool TransactionCommit();

        #endregion

        #region 数据库查询处理

       /// <summary>
       /// 获取当前系统时间//
       /// </summary>
       /// <returns></returns>
        DateTime GetServerTime();


        /// <summary>
        /// 获取数据查询结果集
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns></returns>
        DataTable GetDataSet(string SQLString);

        /// <summary>
        /// 获取数据查询结果集
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="param">参数对象</param>
        /// <returns></returns>
        DataTable GetDataSet(string SQLString, params object[] param);

        /// <summary>
        /// 返回某一个字段的值
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns></returns>
        object GetFieldValue(string SQLString);

        /// <summary>
        /// 查询获取的记录集
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns></returns>
        Common.dbRecord GetRecord(string SQLString);

       /// <summary>
        /// 查询获取的记录集
       /// </summary>
        /// <param name="SQLString">SQL语句</param>
       /// <param name="param">参数</param>
       /// <returns></returns>
       Common.dbRecord GetRecord(string SQLString, params object[] param);

        /// <summary>
        /// 获取表格显示的记录集
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns></returns>
        Common.View GetView(string SQLString);

        /// <summary>
        /// 获取表格显示的记录集
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        Common.View GetView(string SQLString, params object[] param);

        #endregion

        #region 数据库执行处理

        /// <summary>
        /// 数据执行处理
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns></returns>
        int ExecuteNonQuery(string SQLString);

        /// <summary>
        /// 数据执行处理
        /// </summary>
        /// <param name="SQLString">SQL语句</param> 
        /// <param name="param">参数对象</param>
        /// <returns></returns>
       int ExecuteNonQuery(string SQLString, params object[] param);

        /// <summary>
        /// 存储过程执行处理
        /// </summary>
        /// <param name="StoreProcName"></param>
        /// <param name="MyChoose"> HasAOutput：仅有单个返回值;
        ///                         OnlyExecSp：仅执行无返回值;
        ///                         RetOneRecord：返回记录集DataTable</param>
        /// <param name="para"></param>
        /// <returns></returns>
        object SetExecuteSP(string StoreProcName, Common.Choose MyChoose, params object[] para);

        #endregion

    }
}
