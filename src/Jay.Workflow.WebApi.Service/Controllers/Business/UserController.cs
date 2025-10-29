using Jay.Workflow.WebApi.Bll.Interfaces.Login;
using Jay.Workflow.WebApi.Bll.Interfaces.User;
using Jay.Workflow.WebApi.Common.Models.Base;
using Jay.Workflow.WebApi.Common.Utils;
using Jay.Workflow.WebApi.Model.Dtos.Request.Login;
using Jay.Workflow.WebApi.Model.Dtos.Request.User;
using Jay.Workflow.WebApi.Service.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jay.Workflow.WebApi.Service.Controllers.Business
{
    /// <summary>
    /// 登录控制器
    /// </summary>
    [Route("api/user")]
    public class UserController : BusinessController
    {
        private readonly IUserService _userService;

        /// <summary>
        /// 构造
        /// </summary>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 分页查询用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet("users/page")]
        public async Task<PagingResponse<GetUsersPageResp>> GetUsersPageAsync([FromQuery]GetUsersPageReq req)
        {        
            return await _userService.GetUsersPageAsync(req);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("user")]
        public async Task<bool> CreateUserAsync(CreateUserReq req)
        {
            ValidateHelper.IsNullOrWhiteSpace(req.UserName, "姓名不能为空");
            ValidateHelper.IsNullOrWhiteSpace(req.Password, "密码不能为空");
            ValidateHelper.IsNullOrWhiteSpace(req.UserPhone, "手机号不能为空");
            ValidateHelper.IsPhoneNumber(req.UserPhone, "手机号格式错误");
            return await _userService.CreateUserAsync(req) > 0;
        }
    }
}
