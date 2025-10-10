using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.NC.Workflow.WebApi.Common.Attributes
{
    /// <summary>
    /// 枚举扩展数据字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumExtDataAttribute : Attribute
    {
        /// <summary>
        /// 枚举排序编号
        /// </summary>
        public string ExtData { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="extData"></param>
        public EnumExtDataAttribute(string extData) => ExtData = extData;
    }
}
