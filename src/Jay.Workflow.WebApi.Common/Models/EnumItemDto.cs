using Jay.Workflow.WebApi.Common.Attributes;
using Jay.Workflow.WebApi.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Models.Business
{
    /// <summary>
    /// 枚举dto
    /// </summary>
    public class EnumItemDto
    {
        /// <summary>
        /// 枚举名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 枚举值
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 枚举类型集合
        /// </summary>
        public List<EnumType> EnumTypes { get; set; }

        /// <summary>
        /// 枚举排序编号
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>
        public string ExtData { get; set; }

        /// <summary>
        /// 枚举描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否包含忽略标识属性
        /// </summary>
        public bool IsContainsIgnoreAttribute { set; get; } = false;
    }
}
