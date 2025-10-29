using Jay.Workflow.WebApi.Common.Interface.Base;
using Jay.Workflow.WebApi.Model.Dtos.Request.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Bll.Interfaces.Login
{
    public interface ILoginService:IScopedDependency
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<string> Login(LoginReq req);
    }
}
