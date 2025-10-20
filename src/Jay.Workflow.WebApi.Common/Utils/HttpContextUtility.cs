using Jay.Workflow.WebApi.Common.Consts;
using Jay.Workflow.WebApi.Common.Extensions;
using Jay.Workflow.WebApi.Common.Interface;
using Jay.Workflow.WebApi.Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Utils
{
    /// <summary>
    /// HttpContextUtility
    /// </summary>
    public class HttpContextUtility : IHttpContextUtility
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public HttpContextUtility(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 获取角色名称
        /// </summary>
        /// <returns></returns>
        public string GetRoleName()
        {
            return "default";
            //return httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "name")?.Value;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public AuthUserInfoBusiness GetUserInfo()
        {
            var claims = httpContextAccessor?.HttpContext?.User?.Claims;

            var userId = claims?.FirstOrDefault(x => x.Type == "sub")?.Value
                    ?? httpContextAccessor.HttpContext.Request.Headers[HttpHeaderConst.UserId];

            var userName = claims?.FirstOrDefault(x => x.Type == "name")?.Value;

            var nickName = claims?.FirstOrDefault(x => x.Type == "nickname")?.Value;

            return new AuthUserInfoBusiness
            {
                UserId = userId.AsInt(),
                UserName = userName,
                NickName = nickName ?? userName,
                AppKey = GetRequestAppKey()
            };
        }

        /// <summary>
        /// 获取用户编号
        /// </summary>
        /// <returns></returns>
        public virtual int GetUserId()
        {
            var userId = httpContextAccessor.HttpContext.User.Claims?.FirstOrDefault(x => x.Type == "sub")?.Value
                    ?? httpContextAccessor.HttpContext.Request.Headers[HttpHeaderConst.UserId];

            return userId.AsInt();
        }

        /// <summary>
        /// 获取用户昵称，当昵称不存在时返回用户名称
        /// </summary>
        /// <returns></returns>
        public string GetUsername()
        {
            var claims = httpContextAccessor.HttpContext.User.Claims;

            return claims?.FirstOrDefault(x => x.Type == "nickname")?.Value ?? claims?.FirstOrDefault(x => x.Type == "name")?.Value;
        }

        /// <summary>
        /// 获取链路追踪编号
        /// </summary>
        /// <returns></returns>
        public string GetTraceIdentifier()
        {
            var traceIdentifier = httpContextAccessor?.HttpContext?.TraceIdentifier ?? string.Empty;

            return traceIdentifier;
        }

        /// <summary>
        /// 获取请求应用
        /// </summary>
        /// <returns></returns>
        private string GetRequestAppKey()
        {
            if (!httpContextAccessor.HttpContext.Request.Headers.TryGetValue(HttpHeaderConst.AppKey, out var xAppKey))
            {
                return null;
            }

            return xAppKey;
        }
    }
}
