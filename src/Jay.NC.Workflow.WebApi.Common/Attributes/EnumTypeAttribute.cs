using Jay.NC.Workflow.WebApi.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.NC.Workflow.WebApi.Common.Attributes
{
    /// <summary>
    /// 枚举类型标识
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumTypeAttribute : Attribute
    {
        /// <summary>
        /// 枚举类型集合
        /// </summary>
        public EnumType[] EnumTypes { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="enumTypes"></param>
        public EnumTypeAttribute(params EnumType[] enumTypes) => EnumTypes = enumTypes;
    }
}
