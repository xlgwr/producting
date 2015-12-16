using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Framework.Libs
{

    public static class DataFiledType
    {

        public static Dictionary<string, SqlDbType> FiledType = new Dictionary<string, SqlDbType>();

        static DataFiledType()
        {


            //=========================================/
            //=========共通业务字段列数据类型==========/
            //=========================================/
            FiledType["slctvalue"] = SqlDbType.Bit;

            //资格设置
            FiledType["id"] = SqlDbType.Int;
            FiledType["pname"] = SqlDbType.NVarChar;
            FiledType["pmark"] = SqlDbType.NVarChar;


            //用户信息
            FiledType["id"] = SqlDbType.Int;
            FiledType["username"] = SqlDbType.NVarChar;
            FiledType["sex"] = SqlDbType.NVarChar;
            FiledType["userdept"] = SqlDbType.NVarChar;
            FiledType["dutyname"] = SqlDbType.NVarChar;
            FiledType["user_status"] = SqlDbType.NVarChar;

            //角色信息
            FiledType["roleid"] = SqlDbType.NVarChar;
            FiledType["rolename"] = SqlDbType.NVarChar;

            //用户角色表
            FiledType["userid"] = SqlDbType.NVarChar;
            FiledType["remark"] = SqlDbType.NVarChar;
            FiledType["operid"] = SqlDbType.NVarChar;
            FiledType["operdate"] = SqlDbType.NVarChar;
            FiledType["partid"] = SqlDbType.NVarChar;

            //关位参数设置
            FiledType["jobforid"] = SqlDbType.NVarChar;
            //工程别
            FiledType["projectid"] = SqlDbType.NVarChar;
            //Line系列
            FiledType["linecatenaid"] = SqlDbType.NVarChar;
           //Line别
            FiledType["lineid"] = SqlDbType.NVarChar;
            //班别
            FiledType["teamid"] = SqlDbType.NVarChar;
            //关位
            FiledType["guanweiid"] = SqlDbType.NVarChar;
            FiledType["attenddate"] = SqlDbType.NVarChar;

            //排班
            FiledType["schedulingid"] = SqlDbType.Int;
            FiledType["strdate1"] = SqlDbType.Date;
            FiledType["enddate1"] = SqlDbType.Date;
            FiledType["type1"] = SqlDbType.NVarChar;

            //权限
            FiledType["roleid"] = SqlDbType.NVarChar;
            FiledType["pfromname"] = SqlDbType.NVarChar;
            FiledType["myuserid"] = SqlDbType.NVarChar;

            //加班登记
            FiledType["otstrdate"] = SqlDbType.DateTime;
            FiledType["otendtime"] = SqlDbType.DateTime;
            FiledType["otnum"] = SqlDbType.Int;
            FiledType["ottype"] = SqlDbType.NVarChar;
            FiledType["otapplyid"] = SqlDbType.NVarChar;

            //免许登记
            FiledType["recid"] = SqlDbType.NVarChar;
        }

    }

    public class SetLog
    {

        public const string TableName = "SiteLog";
        public const string ID = "ID";
        public const string OperNo = "OperNo";
        public const string OperName = "OperName";
        public const string myTeamName = "myTeamName";
        public const string moduleName = "moduleName";
        public const string functName = "functName";
        public const string OperDate = "OperDate";
        public const string Memo = "Memo";
    }

    public class M_FuncForm
    {
        public const string TableName = "M_FuncForm";

        public const string FormId = "FormId";
        public const string FunctionTyp = "FunctionTyp";
        public const string FormName = "FormName";
        public const string SortNo = "SortNo";
        public const string frmStatus = "frmStatus";
    }
}
