using Jay.Workflow.WebApi.Common.Attributes;
using Jay.Workflow.WebApi.Common.Exceptions;
using Jay.Workflow.WebApi.Common.Extensions;
using Jay.Workflow.WebApi.Common.Models.Business;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Utils
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 缓存指定程序集中的枚举dto信息集合
        /// </summary>
        private static readonly ConcurrentDictionary<string, List<EnumItemDto>> _enumItemDtosDic = new ConcurrentDictionary<string, List<EnumItemDto>>();

        /// <summary>
        /// 初始化指定程序集中的枚举dto信息集合
        /// </summary>
        public static void InitEnumItemDtosDic(string assemblyName)
        {
            var assembly = Assembly.Load(assemblyName);
            if (assembly == null) throw new InternalServerException($"未找到指定的程序集：{assemblyName}");

            var enumTypes=assembly.ExportedTypes.Where(a=>a.IsEnum).ToList();
            enumTypes.ForEach(e =>
            {
                var enumItemDtos = GetEnumItemDtos<EnumIgnoreAttribute>(e);
                if (!enumItemDtos?.Any() ?? true) return;
                _enumItemDtosDic.TryAdd(e.Name.Replace("Enum", string.Empty), enumItemDtos);
            });
        }

        /// <summary>
        /// 获取指定程序集中的枚举dto信息集合
        /// </summary>
        /// <returns></returns>
        public static ConcurrentDictionary<string, List<EnumItemDto>> GetEnumItemDtos() => _enumItemDtosDic;

        /// <summary>
        /// 将枚举类转换成枚举项的集合
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static List<EnumItemDto> GetEnumItems(Type enumType)
        {
            // 如果所属类型不是枚举类，则抛出异常
            if (enumType.BaseType.Name != typeof(Enum).Name)
            {
                throw new InternalServerException("需要转换的类型必须为枚举类！");
            }

            return Enum.GetValues(enumType)
                       .OfType<Enum>()
                       .Select(x => new EnumItemDto
                       {
                           Name = x.ToString(),
                           Value = Convert.ToInt32(x),
                           Description = x.ToDescription()
                       }).ToList();
        }

        /// <summary>
        /// 将枚举类转换成枚举项的集合
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static List<EnumItemDto> GetEnumItemDtos<T>(Type enumType) where T : Attribute
        {
            if (enumType.BaseType.Name != typeof(Enum).Name)
            {
                throw new InternalServerException("需要转换的类型必须为枚举类！");
            }

            return Enum.GetValues(enumType).OfType<Enum>().Select(e => new EnumItemDto
            {
                Name = e.ToString(),
                Value = Convert.ToInt32(e),
                EnumTypes = e.ToEnumTypes(),
                Order = e.ToEnumOrder(),
                Description = e.ToDescription(),
                IsContainsIgnoreAttribute = e.IsContainsAttribute<T>()
            }).OrderBy(e => e.Order).ToList();
        }

        /// <summary>
        /// 根据枚举描述获取枚举项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumDesc">枚举描述</param>
        /// <returns></returns>
        public static T GetEnum<T>(string enumDesc) where T : Enum
        {
            // 如果枚举描述不存在为空，则抛出异常
            if (string.IsNullOrWhiteSpace(enumDesc))
            {
                throw new InternalServerException("枚举描述不存在！");
            }
            var enumItem = Enum.GetValues(typeof(T)).OfType<T>().FirstOrDefault(p => p.ToDescription() == enumDesc);
            return enumItem;
        }

        /// <summary>
        /// 获取枚举项的名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumItemValue">枚举项的名称或者值</param>
        /// <returns></returns>
        public static string GetEnumName<T>(string enumItemValue) where T : struct, Enum
        {
            // 如果枚举值为空，则抛出异常
            if (string.IsNullOrWhiteSpace(enumItemValue))
            {
                throw new InternalServerException("需要转换的枚举值不能为空！");
            }

            var isSuccess = Enum.TryParse<T>(enumItemValue, out var @enum);

            return isSuccess ? @enum.ToString() : default;
        }

        /// <summary>
        /// 获取枚举项的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumItemValue">枚举项的名称或者值</param>
        /// <returns></returns>
        public static int GetEnumValue<T>(string enumItemValue) where T : struct, Enum
        {
            // 如果枚举值为空，则抛出异常
            if (string.IsNullOrWhiteSpace(enumItemValue))
            {
                throw new InternalServerException("需要转换的枚举值不能为空！");
            }

            var isSuccess = Enum.TryParse<T>(enumItemValue, out var @enum);

            return isSuccess ? Convert.ToInt32(@enum) : default;
        }

        /// <summary>
        /// 获取枚举项的描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumItemValue">枚举项的名称或者值</param>
        /// <returns></returns>
        public static string GetEnumDesc<T>(string enumItemValue) where T : struct, Enum
        {
            // 如果枚举值为空，则返回空
            if (string.IsNullOrWhiteSpace(enumItemValue))
            {
                return default;
            }

            var isSuccess = Enum.TryParse<T>(enumItemValue, out var @enum);

            return isSuccess ? @enum.ToDescription() : default;
        }

        /// <summary>
        /// 获取枚举项的描述
        /// 根据枚举项的Int值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumItemValueInt">枚举项的Int值</param>
        /// <returns></returns>
        public static string GetEnumDesc<T>(int enumItemValueInt) where T : struct, Enum
        {
            var enumItemValue = enumItemValueInt.ToString();

            return GetEnumDesc<T>(enumItemValue);
        }

        /// <summary>
        /// 获取枚举项的描述
        /// 根据枚举项的Bool值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumItemValueBool">枚举项的Bool值</param>
        /// <returns></returns>
        public static string GetEnumDesc<T>(bool enumItemValueBool) where T : struct, Enum
        {
            var enumItemValueInt = enumItemValueBool ? 1 : 0;

            return GetEnumDesc<T>(enumItemValueInt);
        }
    }
}
