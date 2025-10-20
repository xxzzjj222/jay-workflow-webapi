using Jay.Workflow.WebApi.Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Enums
{
    /// <summary>
    /// 时间维度枚举
    /// </summary>
    public enum TimeDimensionEnum
    {
        /// <summary>
        /// 无
        /// </summary>
        [EnumIgnore]
        [Description("无")]
        None = 0,

        /// <summary>
        /// 小时
        /// </summary>
        [EnumIgnore]
        [Description("小时")]
        Hour = 10,

        /// <summary>
        /// 日
        /// </summary>
        [Description("日")]
        Day = 20,

        /// <summary>
        /// 月
        /// </summary>
        [Description("月")]
        Month = 30,

        /// <summary>
        /// 季
        /// </summary>
        [Description("季")]
        Quarter = 40,

        /// <summary>
        /// 半年
        /// </summary>
        [Description("半年")]
        HalfYear = 50,

        /// <summary>
        /// 年
        /// </summary>
        [Description("年")]
        Year = 60
    }
}
