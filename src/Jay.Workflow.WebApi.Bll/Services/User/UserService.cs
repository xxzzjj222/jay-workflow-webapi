using AutoMapper;
using Jay.Workflow.WebApi.Bll.Interfaces.User;
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

        public async Task<PagingResponse<GetUsersPageResp>> GetUsersPageAsync(GetUsersPageReq req)
        {
            var users = await _userDalService.GetUsersAsync();

            if (!string.IsNullOrWhiteSpace(req.Keyword))
            {
                users = users.Where(u => u.UserName.Contains(req.Keyword) || u.UserPhone.Contains(req.Keyword)).ToList();
            }

            var response = new PagingResponse<GetUsersPageResp>()
            {
                Count = users.Count
            };

            var currentPageUsers = users.OrderBy(u => u.CreatedTime).Skip(req.PageSize * (req.PageIndex - 1)).Take(req.PageSize).ToList();
            var respUsers = _mapper.Map<List<GetUsersPageResp>>(currentPageUsers);

            response.Datas.AddRange(respUsers);

            return response;
        }

        public async Task<int> CreateUserAsync(CreateUserReq req)
        {
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
