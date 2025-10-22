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
    /// 数据不存在异常
    /// </summary>
    public class NotFoundException : ApiBaseException
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message"></param>
        public NotFoundException(string message)
            :base(InternalErrorCode.NotFoundException,StatusCodes.Status404NotFound)
        {
            Message= message;
        }
    }
}
