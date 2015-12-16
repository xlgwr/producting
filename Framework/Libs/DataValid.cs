using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Framework.Libs
{
    public class DataValid
    {
       
        /// <summary>
        ///  设定控件的警告信息
        /// </summary>
        /// <param name="ErrorInfo">异常信息对象提供者</param>
        /// <param name="ctl">控件对象</param>
        /// <param name="messageId">出错信息，可以用ID，可以用字符串</param>
        public static void ShowErrorInfo(DXErrorProvider ErrorInfo,  Control ctl, string messageId)
        {
          
            ErrorInfo.SetError (ctl,messageId);
            ErrorInfo.SetIconAlignment(ctl, ErrorIconAlignment.MiddleRight);
           
            //设定窗体控件的焦点
            Common.SetContorlFocus(ctl);
        }

        /// <summary>
        /// 设定控件的值不能为空白
        /// </summary>
        /// <param name="dXValidationProvider">验证对象提供者</param>
        /// <param name="ctl">控件对象</param>
        /// <param name="messageId">出错信息，可以用ID，可以用字符串</param>
        public static void SetControlValidBlank(DXValidationProvider dXValidationProvider, Control ctl, string messageId)
        {

            ConditionValidationRule notEmptyValidationRule = new ConditionValidationRule();
            notEmptyValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;
            notEmptyValidationRule.ErrorText = messageId;
            dXValidationProvider.SetValidationRule(ctl, notEmptyValidationRule);
            dXValidationProvider.SetIconAlignment(ctl, ErrorIconAlignment.MiddleRight);
            
        }



        /// <summary>
        /// 验证控件的值介一个区间内
        /// </summary>
        /// <param name="dXValidationProvider">验证对象提供者</param>
        /// <param name="ctl">控件对象</param>
        /// <param name="messageId">出错信息，可以用ID，可以用字符串</param>
        /// <param name="value1">起始值</param>
        /// <param name="value2">结束值</param>
        public static void SetControlValidBetween(DXValidationProvider dXValidationProvider, Control ctl, string messageId, DateTime value1, DateTime value2)
        {
            ConditionValidationRule rangeValidationRule = new ConditionValidationRule();
            rangeValidationRule.ConditionOperator = ConditionOperator.Between;
            rangeValidationRule.Value1 = value1;
            rangeValidationRule.Value2 = value2;
            rangeValidationRule.ErrorText = messageId;
            dXValidationProvider.SetValidationRule(ctl, rangeValidationRule);
            dXValidationProvider.SetIconAlignment(ctl, ErrorIconAlignment.MiddleRight);
        }

        /// <summary>
        /// 验证控件的值介一个区间内
        /// </summary>
        /// <param name="dXValidationProvider">验证对象提供者</param>
        /// <param name="ctl">控件对象</param>
        /// <param name="messageId">出错信息，可以用ID，可以用字符串</param>
        /// <param name="value1">起始值</param>
        /// <param name="value2">结束值</param>
        public static void SetControlValidBetween(DXValidationProvider dXValidationProvider, Control ctl, string messageId, 
                                                decimal value1, decimal value2)
        {
            ConditionValidationRule rangeValidationRule = new ConditionValidationRule();
            rangeValidationRule.ConditionOperator = ConditionOperator.Between;
            rangeValidationRule.Value1 = value1;
            rangeValidationRule.Value2 = value2;
            rangeValidationRule.ErrorText = messageId;
            dXValidationProvider.SetValidationRule(ctl, rangeValidationRule);
            dXValidationProvider.SetIconAlignment(ctl, ErrorIconAlignment.MiddleRight);
           
        }


        /// <summary>
        /// 验证控件的值是否大于指定的值
        /// </summary>
        /// <param name="dXValidationProvider">验证对象提供者</param>
        /// <param name="ctl">控件对象</param>
        /// <param name="messageId">出错信息，可以用ID，可以用字符串</param>
        /// <param name="value1">指定的值</param>
        public static void SetControlValidGreater(DXValidationProvider dXValidationProvider, Control ctl, 
                                                string messageId, DateTime value1)
        {
            ConditionValidationRule rangeValidationRule = new ConditionValidationRule();
            rangeValidationRule.ConditionOperator = ConditionOperator.Greater;
            rangeValidationRule.Value1 =DateTime.Parse(value1.ToString("HH:mm"));
            rangeValidationRule.Value2 =DateTime.Parse(ctl.Text );
            rangeValidationRule.ErrorText = messageId;
            dXValidationProvider.SetValidationRule(ctl, rangeValidationRule);
            dXValidationProvider.SetIconAlignment(ctl, ErrorIconAlignment.MiddleRight);
        }

        /// <summary>
        /// 验证控件的值是否大于指定的值
        /// </summary>
        /// <param name="dXValidationProvider">验证对象提供者</param>
        /// <param name="ctl">控件对象</param>
        /// <param name="messageId">出错信息，可以用ID，可以用字符串</param>
        /// <param name="value1">指定的值</param>
        public static void SetControlValidGreater(DXValidationProvider dXValidationProvider, Control ctl, 
                                                string messageId, decimal value1)
        {
            ConditionValidationRule rangeValidationRule = new ConditionValidationRule();
            rangeValidationRule.ConditionOperator = ConditionOperator.Greater;
            rangeValidationRule.Value1 = ctl.Text ;
            rangeValidationRule.Value2 = value1;
            rangeValidationRule.ErrorText = messageId;
            dXValidationProvider.SetValidationRule(ctl, rangeValidationRule);
            dXValidationProvider.SetIconAlignment(ctl, ErrorIconAlignment.MiddleRight);
        }

        /// <summary>
        /// 验证控件的值是否大于或等于指定的值
        /// </summary>
        /// <param name="dXValidationProvider">验证对象提供者</param>
        /// <param name="ctl">控件对象</param>
        /// <param name="messageId">出错信息，可以用ID，可以用字符串</param>
        /// <param name="value1">指定的值</param>
        public static void SetControlValidGreaterOrEqual(DXValidationProvider dXValidationProvider, Control ctl, 
                                                        string messageId, decimal value1)
        {
            ConditionValidationRule rangeValidationRule = new ConditionValidationRule();
            rangeValidationRule.ConditionOperator = ConditionOperator.GreaterOrEqual;
            rangeValidationRule.Value1 = ctl.Text ;
            rangeValidationRule.Value2 = value1;
            rangeValidationRule.ErrorText = messageId;
            dXValidationProvider.SetValidationRule(ctl, rangeValidationRule);
            dXValidationProvider.SetIconAlignment(ctl, ErrorIconAlignment.MiddleRight);
        }

        /// <summary>
        /// 时间值是否大于指定的时间值
        /// </summary>
        /// <param name="value">检测时间</param>
        /// <param name="maxvalue">指定的值</param>
        /// <returns>True： 大于指定时间 </returns>
        public static bool SetDateMaxGreater(DateTime value, DateTime maxvalue)
        {
            bool isCheckMax = false ;

            if (DateTime.Compare(value, maxvalue) > 0)
            {
                isCheckMax = true;
            }

            return isCheckMax;
        }

        /// <summary>
        /// 时间值是否小于等于指定的时间值
        /// </summary>
        /// <param name="value">检测时间</param>
        /// <param name="maxvalue">指定的值</param>
        /// <returns>True： 小于指定时间 </returns>
        public static bool SetDateMinGreater(DateTime value, DateTime maxvalue)
        {
            bool isCheckMax = false;

            if (DateTime.Compare(value, maxvalue) <= 0)
            {
                isCheckMax = true;
            }

            return isCheckMax;
        }


        /// <summary>
        /// 判断是否输入为空字符串
        /// </summary>
        /// <returns>True： 为空字符 </returns>
        public static bool isCheckNullOrEmpty(Object txt)
        {

            bool isCheckNull = true;

            if (txt != null && !string.IsNullOrEmpty(txt.ToString().Trim()))
            {
                isCheckNull = false;
            }

            return isCheckNull;
        }


        /// <summary>
        /// 判断是否是字母和数字组合
        /// </summary>
        /// <returns></returns>
        public static bool isCheckNumberW(string txt)
        {
            bool IsCheckNumberW = true;
            //Regex reg1 = new Regex("^[A-Za-z0-9]+$");
            ///匹配单字节 不能输入中文,全角 必须是字母数字开头 
            Regex reg1 = new Regex(@"^[A-Za-z0-9]+[\x00-\xff]+$");
            IsCheckNumberW = reg1.IsMatch(txt);

            return IsCheckNumberW;

        }
        /// <summary>
        /// 匹配单字节 不允许输入中文
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static bool GetCheckIsChar(string txt)
        {
            bool IsCheckNumberW = true;
            Regex reg1 = new Regex(@"^[A-Za-z0-9]+[\x00-\xff]+[A-Za-z0-9]+$");
            IsCheckNumberW = reg1.IsMatch(txt);

            return IsCheckNumberW;
        }

        /// <summary>
        /// 判断是否是字母和数字和下划线组合
        /// </summary>
        /// <returns></returns>
        public static bool isCheckFileName(string txt)
        {
            bool isCheckFileName = true;
            Regex reg1 = new Regex("^[A-Za-z0-9]+[\x00-\xff]+[A-Za-z0-9]+$");
            isCheckFileName = reg1.IsMatch(txt);

            return isCheckFileName;

        }


        // ///// <summary>
        // ///// 判断是否是yyyyMMdd型的时紒E
        // ///// </summary>
        // ///// <param name="strDate">字符串</param>
        // ///// <returns>如果正确，则返回諄E/returns>
        // //public static bool IsDate(string strDate)
        // //{
        // //    if (Regex.IsMatch(strDate, @"^([12]\d{3})(0\d|1[0-2])([0-2]\d|3[01])$"))
        // //        return true;
        // //    else
        // //        return false;
        // //}

        // #region  判断字符串是否是正确的 IP 地址格式 public static bool IsIp(string s)

          ///<summary> 
          ///判断字符串是否是正确的 IP 地址格式
          ///</summary>
         public static bool IsIp(string s)
         {
             string pattern = @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$";
             return Regex.IsMatch(s, pattern);
         }

        /// <summary>
        /// 验证字符串是否是正确的Email格式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
         public static bool IsEmail(string s) 
         {
             string pattern = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
             return Regex.IsMatch(s, pattern);
         }
        /// <summary>
        /// 验证是否是数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public  static bool IsNumeric(string str)
         {
             string pattern = @"^[-]?\d+[.]?\d*$";
             return Regex.IsMatch(str, pattern);
         }
        #region  正则表达式的规则
        /// <summary> 
        /// 非負數字。 
        /// </summary> 
        public const string ZZValidNumeric1 = @"^\d+[.]?\d*$"; 
        /// <summary> 
        /// 數字。 
        /// </summary> 
        public const string ZZValidNumeric2 = @"^[-]?\d+[.]?\d*$";
        /// <summary> 
        /// 非負整數。 
        /// </summary> 
        public const string ZZValidInt1 = @"^\d+$";
        /// <summary> 
        /// 整數。 
        /// </summary> 
        public const string ZZValidInt2 = @"^-?\d+$";
        /// <summary> 
        /// 負整數。 
        /// </summary> 
        public const string ZZValidInt3 = @"^-[0-9]*[1-9][0-9]*$";
        /// <summary> 
        /// 正整數。 
        /// </summary> 
        public const string ZZValidInt4 = @"^[0-9]*[1-9][0-9]*$";
        /// <summary> 
        /// 非正整數。 
        /// </summary> 
        public const string ZZValidInt5 = @"^((-\d+)|(0+))$";

        //时间格式 YYYY-MM-dd
        public const string ZZValidDateTime1 = @"([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|(02-(0[1-9]|[1][0-9]|2[0-8])))";

        /// <summary>
        /// 是否为日期+时间型字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsDateTime(string StrSource)
        {
            return Regex.IsMatch(StrSource, @"^(((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$ ");
        }


        /// <summary> 
        /// 正則表達式驗證。 
        /// </summary> 
        /// <param name="str">預檢查的字符串。</param> 
        /// <param name="ZZValidItem">正則表達式字符串。</param> 
        /// <returns>true:符合格式;false:不符合格式。</returns> 
        public static bool IsValidString(string str, string ZZValidItem)
        {
            return Regex.IsMatch(str, ZZValidItem);
        }
        #endregion
    }
}
