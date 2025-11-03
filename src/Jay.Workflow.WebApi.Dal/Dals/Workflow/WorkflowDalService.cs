using Jay.Workflow.WebApi.Common.Cache;
using Jay.Workflow.WebApi.Common.Utils;
using Jay.Workflow.WebApi.Dal.Interfaces.Workflow;
using Jay.Workflow.WebApi.Storage.Context;
using Jay.Workflow.WebApi.Storage.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Dal.Dals.Workflow
{
    public class WorkflowDalService:BaseDal<Storage.Entity.Workflow>,IWorkflowDalService
    {
        public WorkflowDalService(WorkflowDbContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<(List<Storage.Entity.Workflow> data, int count)> GetPageWorkflowsAsync(int pageIndex, int pageSize, string? keyword = null)
        {
            var workflows = await GetWorkflowQueryableAsync().ConfigureAwait(false);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                workflows = workflows.Where(w => w.FlowName.Contains(keyword));
            }

            var data = await workflows.OrderByDescending(w => w.CreatedTime).PagedListAsync(pageIndex, pageSize).ConfigureAwait(false);
            var count = await workflows.CountAsync().ConfigureAwait(false);

            return (data, count);
        }

        private async Task<IQueryable<Storage.Entity.Workflow>> GetWorkflowQueryableAsync(bool asNoTracking = true)
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
