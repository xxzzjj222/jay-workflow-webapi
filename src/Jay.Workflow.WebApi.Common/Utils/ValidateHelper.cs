using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Utils
{
    public static class ValidateHelper
    {
        /// <summary>
        /// 判断对象是否为空Guid
        /// </summary>
        /// <param name="args">参数</param>
        /// <param name="message">异常信息</param>
        public static void IsEmpty(Guid args, string message)
        {
            if (args == Guid.Empty)
            {
                throw new Exception(message);
            }
        }

        /// <summary>
        /// 判断对象是否为true，为true抛出异常
        /// </summary>
        /// <param name="args">表达式结果</param>
        /// <param name="message">异常信息</param>
        public static void IsTrue(bool args, string message)
        {
            if (args)
            {
                throw new Exception(message);
            }
        }

        /// <summary>
        /// 判断对象是否为Null
        /// </summary>
        /// <param name="args">参数</param>
        /// <param name="message">异常信息</param>
        public static void IsNull(object args, string message)
        {
            if (args == null)
            {
                throw new Exception(message);
            }
        }

        /// <summary>
        /// 判断对象是否为Null
        /// </summary>
        /// <param name="args">参数</param>
        /// <param name="message">异常信息</param>
        public static void IsNone(Enum args, string message)
        {
            if (args != null && args.ToString() == "Sim")
            {
                throw new Exception(message);
            }
        }

        /// <summary>
        /// 判断字符串是否为Null或者空字符串
        /// </summary>
        /// <param name="args">参数</param>
        /// <param name="message">异常信息</param>
        public static void IsNullOrWhiteSpace(string args, string message)
        {
            if (string.IsNullOrWhiteSpace(args))
            {
                throw new Exception(message);
            }
        }

        /// <summary>
        /// 判断时间是否为默认时间
        /// </summary>
        /// <param name="args">参数</param>
        /// <param name="message">异常信息</param>
        public static void IsMinDateTime(DateTime args, string message)
        {
            if (args == DateTime.MinValue)
            {
                throw new Exception(message);
            }
        }

        /// <summary>
        /// 判断对象是否不为数组，确定则抛出异常
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="args">参数</param>
        /// <param name="message">异常信息</param>
        public static void IsNotArray<T>(string args, string message)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(args))
                    _ = JsonConvert.DeserializeObject<T[]>(args);

            }
            catch (Exception)
            {
                throw new Exception(message);
            }
        }

        /// <summary>
        /// 判断对象是否不为闭区间，确定则抛出异常
        /// </summary>
        /// <param name="args"></param>
        /// <param name="message"></param>
        public static void IsNotClosedInterval(string args, string message)
        {
            try
            {
                //if (string.IsNullOrWhiteSpace(args)) return;

                // 反序列化对象为数组
                var values = JsonConvert.DeserializeObject<decimal[]>(args);

                // 校验是否为闭区间
                if (values is null || values.Length != 2 || values[0] > values[1])
                {
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {
                throw new Exception(message);
            }
        }

        /// <summary>
        /// 判断对象是否不为枚举对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        /// <param name="message"></param>
        public static void IsNotEnum<T>(string args, string message) where T : struct, Enum
        {
            // 枚举值为空，则抛出异常
            IsNullOrWhiteSpace(args, message);

            // 枚举对象转换
            var isSuccess = Enum.TryParse<T>(args, out _);

            // 枚举值转换失败，则抛出异常
            IsTrue(!isSuccess, message);
        }

        /// <summary>
        /// 判断List是否为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="message"></param>
        public static void IsNullArray<T>(List<T> list, string message)
        {
            if (null == list || list.Count < 0)
            {
                throw new Exception(message);
            }
        }

        /// <summary>
        /// 判断字符串长度 大于length
        /// </summary>
        /// <param name="args"></param>
        /// <param name="length"></param>
        /// <param name="message"></param>
        public static void IsGtLength(string args, int length, string message)
        {
            if (args == null || args.Length > length)
            {
                throw new Exception(message);
            }
        }

        /// <summary>
        /// 判断是否为手机号
        /// </summary>
        /// <param name="args"></param>
        /// <param name="message"></param>
        /// <exception cref="Exception"></exception>
        public static void IsPhoneNumber(string args,string message)
        {
            // 简化的手机号正则表达式，匹配1开头且第二位为3-9的11位数字，支持带+86/0086区号
            const string PhonePattern = @"^(?:(?:\+|00)86)?1[3-9]\d{9}$";
            // 使用方法（带预编译的正则对象以提高性能）
            Regex PhoneRegex = new Regex(PhonePattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if(!PhoneRegex.IsMatch(args))
            {
                throw new Exception(message);
            }
        }
    }
}
