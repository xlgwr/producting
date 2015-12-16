using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace MachineSystem
{

    /// <summary>
    /// 数据表名称
    /// </summary>
    public class TableNames
    {
        //向别信息
        public const string P_Produce_JobFor = "P_Produce_JobFor";

        //工程别表数据
        public const string P_Produce_Project = "P_Produce_Project";
        //Line 别 
        public const string P_Produce_Line = "P_Produce_Line";

        // 关位
        public const string P_Produce_Guanwei = "P_Produce_Guanwei";

        //职等信息
        public const string P_User_Duty = "P_User_Duty";

        //部门信息
        public const string V_User_Dept = "V_User_Dept";

        //类型信息
        public const string P_Oper_Type = "P_Oper_Type";

        //离职类型信息
        public const string Attend_LeaveType = "Attend_LeaveType";

        //人员信息
        //public const string User_Info = "User_Info";
        //public const string V_User_Info = "V_User_Info";
        public const string v_Produce_User = "V_Produce_User";
        public const string Oper_Info = "Oper_Info";
        //免许信息新增
        public const string License_Rec_i = "License_Rec_i";
       
        

        //权限信息
        public const string Oper_Role = "Oper_Role";
        public const string Oper_Role_Permissions = "Oper_Role_Permissions";


        //班别信息
        public const string P_Produce_Team = "P_Produce_Team";

        //状态类型
        public const string P_Produce_User_Status1 = "P_Produce_User_Status1";
         
        //请假类型
        public const string P_Attend_VacationType = "P_Attend_VacationType";

        //工程别
        public const string v_Produce_Para = "v_Produce_Para_i";

        /// <summary>
        /// 排班登记
        /// </summary>
        public const string Attend_TeamSet = "Attend_TeamSet";

     

        /// <summary>
        /// 调动记录表
        /// </summary>
        public const string Attend_Move = "Attend_Move";

        /// <summary>
        /// 申请理由
        /// </summary>
        public const string P_OTApply = "P_OTApply";

        /// <summary>
        /// 加班类型
        /// </summary>
        public const string P_OTType = "P_OTType";

        /// <summary>
        /// 免许类型
        /// </summary>
        public const string P_License_Entitle = "P_License_Entitle";

        /// <summary>
        /// 班种(白班，晚班...)
        /// </summary>
        public const string P_Produce_TeamKind = "P_Produce_TeamKind";

        /// <summary>
        /// 排班类型
        /// </summary>
        public const string P_Produce_Scheduling="P_Produce_Scheduling";

        /// <summary>
        /// 免许类型
        /// </summary>
        public const string P_License_Type = "P_License_Type";
        /// <summary>
        /// 排班类型详细表
        /// </summary>
        public const string P_Produce_Scheduling_detail = "P_Produce_Scheduling_detail";
    }
    /// <summary>
    /// 存储下拉框的文本值和value. 如：全部，-1
    /// </summary>
    public class LuetextValue
    {
        public const string M_Alltext = "全部";
        public const string M_value = "-1";
        public const string M_NoAhthor = "无权限";

    }

   
}
