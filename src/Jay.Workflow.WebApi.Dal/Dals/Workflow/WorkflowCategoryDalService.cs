using Jay.Workflow.WebApi.Common.Cache;
using Jay.Workflow.WebApi.Common.Utils;
using Jay.Workflow.WebApi.Dal.Interfaces.User;
using Jay.Workflow.WebApi.Dal.Interfaces.Workflow;
using Jay.Workflow.WebApi.Storage.Context;
using Jay.Workflow.WebApi.Storage.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Dal.Dals.Workflow
{
    public class WorkflowCategoryDalService : BaseDal<WorkflowCategory>, IWorkflowCategoryDalService
    {
        public WorkflowCategoryDalService(WorkflowDbContext dbContext)
            : base(dbContext)
        {
            
        }

        public async Task<(List<WorkflowCategory> data,int count)> GetPageWorkflowCategoriesAsync(int pageIndex,int pageSize,string? keyword=null)
        {
            var workflowCategories = await GetWorkflowCategoryQueryableAsync().ConfigureAwait(false);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                workflowCategories = workflowCategories.Where(w => w.Name.Contains(keyword));
            }

            var data = await workflowCategories.OrderByDescending(w => w.CreatedTime).PagedListAsync(pageIndex, pageSize).ConfigureAwait(false);
            var count = await workflowCategories.CountAsync().ConfigureAwait(false);

            return (data, count);
        }

        private async Task<IQueryable<WorkflowCategory>> GetWorkflowCategoryQueryableAsync(bool asNoTracking=true)
        {
            if (asNoTracking)
            {
                return await GetIQueryableAsNoTracking().ConfigureAwait(false);
            }
            else
            {
                return await GetIQueryable().ConfigureAwait(false);
            }
        }
    }
}
