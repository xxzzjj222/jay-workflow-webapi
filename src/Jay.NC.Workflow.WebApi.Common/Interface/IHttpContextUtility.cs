using Jay.NC.Workflow.WebApi.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.NC.Workflow.WebApi.Common.Interface
{
    /// <summary>
    /// IHttpContextUtility
    /// </summary>
    public interface IHttpContextUtility : IScopedDependency
    {
        /// <summary>
        /// 获取当前角色名(name)
        /// </summary>
        /// <returns></returns>
        string GetRoleName();

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        AuthUserInfoBusiness GetUserInfo();

        /// <summary>
        /// 获取当前UserId(sub)
        /// </summary>
        /// <returns></returns>
        int GetUserId();

        /// <summary>
        /// 获取当前用户名(nickname)
        /// </summary>
        /// <returns></returns>
        string GetUsername();

        /// <summary>
        /// 获取链路追踪编号
        /// </summary>
        /// <returns></returns>
        string GetTraceIdentifier();
    }
}
