using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Extensions
{
    /// <summary>
    /// 类型转换扩展函数类
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// 字符串转decimal
        /// </summary>
        /// <param name="args">字符串</param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static decimal AsDecimal(this string args, decimal defaultVal = 0)
        {
            return decimal.TryParse(args, out var d) ? d : defaultVal;
        }

        /// <summary>
        /// 字符串转int
        /// </summary>
        /// <param name="args">字符串</param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static int AsInt(this string args, int defaultVal = 0)
        {
            return int.TryParse(args, out var i) ? i : defaultVal;
        }

        /// <summary>
        /// 将字符串转换为datetime
        /// </summary>
        /// <param name="args">字符串</param>
        /// <returns></returns>
        public static DateTime AsDateTime(this string args)
        {
            return DateTime.TryParse(args, out var dateTime) ? dateTime : DateTime.MinValue;
        }

        /// <summary>
        /// 将字符串转换为datetime
        /// </summary>
        /// <param name="args"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime ParseDateTimeExact(this string args, string format = "yyyy-MM-dd")
        {
            var dateTimeFormat = new System.Globalization.DateTimeFormatInfo { ShortDatePattern = format };

            return Convert.ToDateTime(args, dateTimeFormat);
        }

        /// <summary>
        /// 将字符串转换为guid
        /// </summary>
        /// <param name="args">字符串</param>
        /// <returns></returns>
        public static Guid AsGUID(this string args)
        {
            return Guid.TryParse(args, out var guid) ? guid : Guid.Empty;
        }

        /// <summary>
        /// 字符串转bool
        /// </summary>
        /// <param name="args">参数</param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static bool AsBoolean(this string args, bool defaultVal = false)
        {
            return bool.TryParse(args, out var b) ? b : defaultVal;
        }
    }
}
