using Jay.Workflow.WebApi.Common.Attributes;
using Jay.Workflow.WebApi.Common.Enums;
using Jay.Workflow.WebApi.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Extensions
{
    /// <summary>
    /// 枚举扩展类
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举对象的描述
        /// </summary>
        /// <param name="enumItemValue"></param>
        /// <returns></returns>
        public static string ToDescription(this Enum enumItemValue)
        {
            if (enumItemValue == null)
            {
                throw new InternalServerException("转换的枚举值不能为空！");
            }

            var attribute = enumItemValue.GetAttribute<DescriptionAttribute>();

            return attribute == null ? enumItemValue.ToString() : attribute.Description;
        }

        /// <summary>
        /// 获取枚举对象的属性T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumItemValue"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static T GetAttribute<T>(this Enum enumItemValue) where T : Attribute 
        {
            if(enumItemValue == null)
            {
                throw new InternalServerException("转换的枚举值不能为空！");
            }

            var memberInfo = enumItemValue.GetType().GetMember(enumItemValue.ToString());
            if (memberInfo.Length == 0) return null;

            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.Length != 0 ? (T)attributes[0] : null;
        }

        /// <summary>
        /// 验证枚举项是否包含自定义属性T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumItemValue"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool IsContainsAttribute<T>(this Enum enumItemValue) where T: Attribute
        {
            if (enumItemValue == null)
            {
                throw new InternalServerException("转换的枚举值不能为空！");
            }

            var fieldInfo=enumItemValue.GetType().GetField(enumItemValue.ToString());
            var attributes = fieldInfo.GetCustomAttributes(typeof(T), false);

            return attributes != null && attributes.Any();
        }

        /// <summary>
        /// 获取枚举排序编号
        /// 如果没有排序编号则返回枚举Int值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToEnumOrder(this Enum value)
        {
            if (value == null) return default;

            var attribute = value.GetAttribute<EnumOrderAttribute>();

            return attribute == null || attribute.Order == default ? Convert.ToInt32(value) : attribute.Order;
        }

        /// <summary>
        /// 获取枚举类型集合
        /// 如果没有枚举类型则返回默认的枚举类型Default
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<EnumType> ToEnumTypes(this Enum value)
        {
            if (value == null) return new List<EnumType> { EnumType.Default };

            var attribute=value.GetAttribute<EnumTypeAttribute>();

            return attribute?.EnumTypes?.ToList() ?? new List<EnumType> { EnumType.Default };
        }
    }
}
