using Jay.Workflow.WebApi.Bll.Interfaces.Login;
using Jay.Workflow.WebApi.Bll.Interfaces.User;
using Jay.Workflow.WebApi.Common.Models.Base;
using Jay.Workflow.WebApi.Common.Utils;
using Jay.Workflow.WebApi.Model.Dtos.Business.User;
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
    [AllowAnonymous]
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
        public async Task<PagingResponse<GetPageUsersResp>> GetPageUsersAsync([FromQuery] GetPageUsersReq req)
        {        
            return await _userService.GetPageUsersAsync(req);
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("users/userId")]
        public async Task<UserDto> GetUserAsync(Guid userId)
        {
            return await _userService.GetUserAsync(userId);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("users")]
        public async Task<UserDto> CreateUserAsync(CreateUserReq req)
        {
            ValidateHelper.IsNullOrWhiteSpace(req.UserName, "姓名不能为空");
            ValidateHelper.IsNullOrWhiteSpace(req.Password, "密码不能为空");
            ValidateHelper.IsNullOrWhiteSpace(req.UserPhone, "手机号不能为空");
            ValidateHelper.IsPhoneNumber(req.UserPhone, "手机号格式错误");

            return await _userService.CreateUserAsync(req);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("users/userId")]
        public async Task<UserDto> UpdateUserAsync(Guid userId, UpdateUserReq req)
        {
            ValidateHelper.IsNullOrWhiteSpace(req.UserName, "姓名不能为空");

            return await _userService.UpdateUserAsync(userId, req);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("users/userId")]
        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            return await _userService.DeleteUserAsync(userId);
        }
    }
}
