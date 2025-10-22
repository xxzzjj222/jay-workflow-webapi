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
    /// 请求错误
    /// </summary>
    public class BadRequestException : ApiBaseException
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message"></param>
        public BadRequestException(string message)
            :base(InternalErrorCode.BadRequestException,StatusCodes.Status400BadRequest)
        {
            Message= message;
        }
    }
}
