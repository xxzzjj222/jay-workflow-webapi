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
    public class WorkflowFormDalService:BaseDal<WorkflowForm>,IWorkflowFormDalService
    {
        public WorkflowFormDalService(WorkflowDbContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<(List<WorkflowForm> data, int count)> GetPageWorkflowFormsAsync(int pageIndex, int pageSize, string? keyword = null)
        {
            var workflowForms = await GetWorkflowFormQueryableAsync().ConfigureAwait(false);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                workflowForms = workflowForms.Where(w => w.FormName.Contains(keyword));
            }

            var data = await workflowForms.OrderByDescending(w => w.CreatedTime).PagedListAsync(pageIndex, pageSize).ConfigureAwait(false);
            var count = await workflowForms.CountAsync().ConfigureAwait(false);

            return (data, count);
        }

        private async Task<IQueryable<WorkflowForm>> GetWorkflowFormQueryableAsync(bool asNoTracking = true)
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
