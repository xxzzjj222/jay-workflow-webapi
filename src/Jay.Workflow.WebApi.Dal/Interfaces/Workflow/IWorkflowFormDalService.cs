using Jay.Workflow.WebApi.Common.Interface.Base;
using Jay.Workflow.WebApi.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Dal.Interfaces.Workflow
{
    public interface IWorkflowFormDalService : IBaseDal<Storage.Entity.WorkflowForm>, IScopedDependency
    {
        Task<(List<WorkflowForm> data, int count)> GetPageWorkflowFormsAsync(int pageIndex, int pageSize, string? keyword = null);
    }
}
