using Jay.Workflow.WebApi.Common.Exceptions.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Exceptions
{
    /// <summary>
    /// 内部服务异常
    /// </summary>
    public class InternalServerException:ApiBaseException
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message"></param>
        public InternalServerException(string message)
            :base(InternalErrorCode.InternalServerError,StatusCodes.Status500InternalServerError)
        {
            Message= message;
        }
    }
}
