using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Libs
{
    public static class DataTypeConv
    {
        /// <summary>
        /// 时间时分转换为分钟处理
        /// </summary>
        /// <param name="dt">时间</param>
        /// <returns></returns>
        public static int GetMinuteByDateTime(DateTime dt)
        {

            try
            {
                return dt.Hour * 60 + dt.Minute;
            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}
