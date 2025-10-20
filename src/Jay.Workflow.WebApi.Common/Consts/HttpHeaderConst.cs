using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Consts
{
    /// <summary>
    /// Http请求头常量
    /// </summary>
    public static class HttpHeaderConst
    {
        /// <summary>
        /// 鉴权标识
        /// </summary>
        public const string Authorization = "Authorization";

        /// <summary>
        /// 应用系统标识
        /// </summary>
        public const string AppKey = "X-App-Key";

        /// <summary>
        /// 链路追踪编号
        /// </summary>
        public const string TraceId = "X-Trace-Id";

        /// <summary>
        /// 用户编号
        /// </summary>
        public const string UserId = "X-User-Id";
    }
}
