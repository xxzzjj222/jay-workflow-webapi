using AutoMapper;
using Jay.Workflow.WebApi.Bll.Interfaces.User;
using Jay.Workflow.WebApi.Common.Exceptions;
using Jay.Workflow.WebApi.Common.Models.Base;
using Jay.Workflow.WebApi.Common.Uow;
using Jay.Workflow.WebApi.Dal.Interfaces.User;
using Jay.Workflow.WebApi.Model.Dtos.Business.User;
using Jay.Workflow.WebApi.Model.Dtos.Request.User;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Bll.Services.User
{
    public class UserService:IUserService
    {
        private readonly IUserDalService _userDalService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserDalService userDalService,IMapper mapper,IUnitOfWork unitOfWork)
        {
            _userDalService = userDalService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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

        public async Task<UserDto> GetUserAsync(Guid userId)
        {
            var entity = await _userDalService.GetUserByUserIdAsync(userId);
            if (entity == null) throw new InvalidParameterException("用户不存在");

            return _mapper.Map<UserDto>(entity);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        /// <exception cref="InvalidParameterException"></exception>
        public async Task<UserDto> CreateUserAsync(CreateUserReq req)
        {
            var entity = await _userDalService.GetUserByUserPhoneAsync(req.UserPhone);
            if (entity != null) throw new InvalidParameterException("用户手机号已存在");

            var user = new Storage.Entity.User
            {
                UserId = Guid.NewGuid(),
                UserName = req.UserName,
                UserPhone = req.UserPhone
            };
            var password = BCrypt.Net.BCrypt.HashPassword(req.Password);
            user.Password=password;

            await _userDalService.AddAndSaveChanges(user).ConfigureAwait(false);

            return _mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        /// <exception cref="InvalidParameterException"></exception>
        public async Task<UserDto> UpdateUserAsync(Guid userId, UpdateUserReq req)
        {
            var entity = await _userDalService.GetUserByUserIdAsync(userId);
            if (entity == null) throw new InvalidParameterException("用户不存在");

            entity.UserName=req.UserName;
            entity.UserAvatar=req.UserAvatar;
            entity.UserEmail=req.UserEmail;

            await _userDalService.UpdateAsync(entity).ConfigureAwait(false);
            await _unitOfWork.CommitAsync().ConfigureAwait(false);

            return _mapper.Map<UserDto>(entity);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidParameterException"></exception>
        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var entity = await _userDalService.GetUserByUserIdAsync(userId);
            if (entity == null) throw new InvalidParameterException("用户不存在");

            await _userDalService.RemoveAsync(entity);
            return await _unitOfWork.CommitAsync().ConfigureAwait(false) > 0;
        }
    }
}
