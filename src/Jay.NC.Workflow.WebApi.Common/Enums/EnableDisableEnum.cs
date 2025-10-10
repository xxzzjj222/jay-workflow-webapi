using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.NC.Workflow.WebApi.Common.Enums
{
    /// <summary>
    /// 启用禁用枚举
    /// </summary>
    public enum EnableDisableEnum
    {
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Disable=0,

        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        Enable =1
    }
}
