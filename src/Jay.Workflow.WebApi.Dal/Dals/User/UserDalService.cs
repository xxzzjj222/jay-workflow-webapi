using Jay.Workflow.WebApi.Common.Cache;
using Jay.Workflow.WebApi.Common.Utils;
using Jay.Workflow.WebApi.Dal.Interfaces.User;
using Jay.Workflow.WebApi.Model.Consts;
using Jay.Workflow.WebApi.Storage.Context;
using Jay.Workflow.WebApi.Storage.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Dal.Dals.User
{
    public class UserDalService : BaseDal<Storage.Entity.User>, IUserDalService
    {
        private readonly ICacheService _cacheService;

        public UserDalService(WorkflowDbContext dbContext,ICacheService cacheService)
            : base(dbContext)
        {
            _cacheService= cacheService;
        }

        public async Task<List<Storage.Entity.User>> GetUsersAsync(bool asNoTracking = true)
        {
            var users = await _cacheService.GetAsync<List<Storage.Entity.User>>(CacheKeyConst.User).ConfigureAwait(false);
            if (users != null && users.Any()) return users;

            if (asNoTracking)
            {
                users = await _dbContext.User.AsNoTracking().ToListAsync().ConfigureAwait(false);
            }
            else
            {
                users=await _dbContext.User.ToListAsync().ConfigureAwait(false);
            }

            await _cacheService.SetAsync(CacheKeyConst.User, users, TimeSpan.FromDays(7)).ConfigureAwait(false);

            return users;
        }

        public async Task<(List<Storage.Entity.User> data, int count)> GetPageUsersAsync(int pageIndex, int pageSize, string? keyword = null)
        {
            var users = await GetUsersAsync(true);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                users = users.Where(u => u.UserName.Contains(keyword) || u.UserPhone.Contains(keyword)).ToList();
            }

            var pageData = users.OrderByDescending(u => u.CreatedTime).PagedList(pageIndex, pageSize);
            var count = users.Count();

            return (pageData, count);
        }

        public async Task<Storage.Entity.User?> GetUserByUserPhoneAsync(string userPhone)
        {
            var users = await GetUsersAsync().ConfigureAwait(false);
            return users.Find(u => u.UserPhone == userPhone);
        }

        public async Task<bool> RefreshCacheAsync()
        {
            return await _cacheService.DeleteAsync(CacheKeyConst.User, CacheKeyConst.UserRole, CacheKeyConst.UserDepartment).ConfigureAwait(false);
        }
    }
}
