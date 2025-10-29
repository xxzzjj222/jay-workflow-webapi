using Jay.Workflow.WebApi.Common.Interface.Base;
using Jay.Workflow.WebApi.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Dal.Interfaces.Workflow
{
    public interface IWorkflowCategoryDalService : IBaseDal<Storage.Entity.WorkflowCategory>, IScopedDependency
    {
        Task<(List<WorkflowCategory> data, int count)> GetPageWorkflowCategoriesAsync(int pageIndex, int pageSize, string? keyword = null);
    }
}
