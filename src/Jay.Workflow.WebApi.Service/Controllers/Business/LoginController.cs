using Jay.Workflow.WebApi.Bll.Interfaces.Login;
using Jay.Workflow.WebApi.Common.Utils;
using Jay.Workflow.WebApi.Model.Dtos.Request.Login;
using Jay.Workflow.WebApi.Service.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jay.Workflow.WebApi.Service.Controllers.Business
{
    /// <summary>
    /// 登录控制器
    /// </summary>
    [Route("api/login")]
    public class LoginController:BusinessController
    {
        private readonly ILoginService _loginService;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="loginService"></param>
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("mobile-login")]
        public async Task<string> Login(LoginReq req)
        {
            ValidateHelper.IsNullOrWhiteSpace(req.UserPhone, "手机号不能为空！");
            ValidateHelper.IsNullOrWhiteSpace(req.Password, "密码不能为空！");
            ValidateHelper.IsPhoneNumber(req.UserPhone, "手机号格式错误！");

            return await _loginService.Login(req);
        }
    }
}
