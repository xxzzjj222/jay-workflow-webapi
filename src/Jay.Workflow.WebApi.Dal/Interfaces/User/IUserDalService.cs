using Jay.Workflow.WebApi.Common.Interface.Base;
using Jay.Workflow.WebApi.Storage.Entity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Dal.Interfaces.User
{
    public interface IUserDalService:IBaseDal<Storage.Entity.User>, IScopedDependency, ICacheDependency
    {
        Task<List<Storage.Entity.User>> GetUsersAsync(bool asNoTracking = true);

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="userPhone"></param>
        /// <returns></returns>
        Task<Storage.Entity.User?> GetUserByUserPhoneAsync(string userPhone);
    }
}
