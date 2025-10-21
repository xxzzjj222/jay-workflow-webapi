using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Model.Dtos.Request.Login
{
    /// <summary>
    /// 登录req
    /// </summary>
    public class LoginReq
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string UserPhone { get; set; } = string.Empty;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
