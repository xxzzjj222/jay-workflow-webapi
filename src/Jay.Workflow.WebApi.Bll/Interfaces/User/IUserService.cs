using Jay.Workflow.WebApi.Common.Interface.Base;
using Jay.Workflow.WebApi.Common.Models.Base;
using Jay.Workflow.WebApi.Model.Dtos.Business.User;
using Jay.Workflow.WebApi.Model.Dtos.Request.Login;
using Jay.Workflow.WebApi.Model.Dtos.Request.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Bll.Interfaces.User
{
    public interface IUserService:IScopedDependency
    {
        Task<PagingResponse<GetPageUsersResp>> GetPageUsersAsync(GetPageUsersReq req);

        Task<UserDto> GetUserAsync(Guid userId);

        Task<UserDto> CreateUserAsync(CreateUserReq req);

        Task<UserDto> UpdateUserAsync(Guid userId, UpdateUserReq req);

        Task<bool> DeleteUserAsync(Guid userId);
    }
}
