using Jay.Workflow.WebApi.Common.Interface.Base;
using Jay.Workflow.WebApi.Common.Models.Base;
using Jay.Workflow.WebApi.Model.Dtos.Business.Workflow.Workflow;
using Jay.Workflow.WebApi.Model.Dtos.Request.Workflow.Workflow;
using Jay.Workflow.WebApi.Model.Dtos.Response.Workflow.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Bll.Interfaces.Workflow
{
    public interface IWorkflowService : IScopedDependency
    {
        Task<PagingResponse<GetPageWorkflowsResp>> GetPageWorkflowsAsync(GetPageWorkflowsReq req);

        Task<WorkflowDto> GetWorkflowAsync(Guid workflowId);

        Task<WorkflowDto> CreateWorkflowAsync(CreateWorkflowReq req);

        Task<WorkflowDto> UpdateWorkflowAsync(Guid workflowId, UpdateWorkflowReq req);

        Task<bool> DeleteWorkflowAsync(Guid workflowId);
    }
}
