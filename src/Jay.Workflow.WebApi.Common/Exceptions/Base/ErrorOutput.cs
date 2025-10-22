using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Exceptions.Base
{
    /// <summary>
    /// 异常输出
    /// </summary>
    public class ErrorOutput
    {
        /// <summary>
        /// 异常码
        /// </summary>
        public InternalErrorCode ErrorCode { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 详细信息
        /// </summary>
        public string Detail { get; set; } = string.Empty;
    }
}
