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
    /// 无效的参数
    /// </summary>
    public class InvalidParameterException : ApiBaseException
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message"></param>
        public InvalidParameterException(string message)
            :base(InternalErrorCode.InvalidParameterException,StatusCodes.Status403Forbidden)
        {
            Message= message;
        }
    }
}
