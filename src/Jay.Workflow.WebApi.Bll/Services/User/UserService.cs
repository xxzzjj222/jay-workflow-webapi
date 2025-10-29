using AutoMapper;
using Jay.Workflow.WebApi.Bll.Interfaces.User;
using Jay.Workflow.WebApi.Common.Exceptions;
using Jay.Workflow.WebApi.Common.Models.Base;
using Jay.Workflow.WebApi.Dal.Interfaces.User;
using Jay.Workflow.WebApi.Model.Dtos.Request.User;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Bll.Services.User
{
    public class UserService:IUserService
    {
        private readonly IUserDalService _userDalService;
        private readonly IMapper _mapper;

        public UserService(IUserDalService userDalService,IMapper mapper)
        {
            _userDalService = userDalService;
            _mapper = mapper;
        }

        /// <summary>
        /// 分页查询用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<PagingResponse<GetPageUsersResp>> GetPageUsersAsync(GetPageUsersReq req)
        {
            var (data, count) = await _userDalService.GetPageUsersAsync(req.PageIndex, req.PageSize, req.Keyword);
            var respData = _mapper.Map<List<GetPageUsersResp>>(data);

            return new PagingResponse<GetPageUsersResp>
            {
                Datas = respData,
                Count = count
            };
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        /// <exception cref="InvalidParameterException"></exception>
        public async Task<int> CreateUserAsync(CreateUserReq req)
        {
            var existUser = await _userDalService.GetUserByUserPhoneAsync(req.UserPhone);
            if (existUser != null) throw new InvalidParameterException("用户手机号已存在");

            var user = new Storage.Entity.User
            {
                UserId = Guid.NewGuid(),
                UserName = req.UserName,
                UserPhone = req.UserPhone
            };
            var password = BCrypt.Net.BCrypt.HashPassword(req.Password);
            user.Password=password;

            return await _userDalService.AddAndSaveChanges(user).ConfigureAwait(false);
        }
    }
}
