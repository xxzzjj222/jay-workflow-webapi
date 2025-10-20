using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Enums
{
    /// <summary>
    /// 配置类型
    /// </summary>
    public enum ConfigTypeEnum
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        None = 0,

        /// <summary>
        /// 数据库
        /// </summary>
        [Description("数据库")]
        Db = 1,

        /// <summary>
        /// 环境变量
        /// </summary>
        [Description("环境变量")]
        Env = 2,

        /// <summary>
        /// 常量
        /// </summary>
        [Description("常量")]
        Const = 3
    }
}
