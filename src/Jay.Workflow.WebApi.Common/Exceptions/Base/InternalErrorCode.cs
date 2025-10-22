using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Exceptions.Base
{
    /// <summary>
    /// 异常码
    /// </summary>
    public enum InternalErrorCode
    {
        /// <summary>
        /// 内部服务错误
        /// </summary>
        [Description("内部服务错误")]
        InternalServerError=500500,

        /// <summary>
        /// 未找到
        /// </summary>
        [Description("未找到")]
        NotFoundException = 400404,


        /// <summary>
        /// 请求错误
        /// </summary>
        [Description("请求错误")]
        BadRequestException = 400400,

        /// <summary>
        /// 参数无效错误
        /// </summary>
        [Description("参数无效")]
        InvalidParameterException = 400403
    }
}
