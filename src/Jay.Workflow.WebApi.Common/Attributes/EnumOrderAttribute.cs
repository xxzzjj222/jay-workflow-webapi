using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Attributes
{
    /// <summary>
    /// 枚举排序标识
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumOrderAttribute : Attribute
    {
        /// <summary>
        /// 枚举排序编号
        /// </summary>
        public int Order { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="order"></param>
        public EnumOrderAttribute(int order) => Order = order;
    }
}
