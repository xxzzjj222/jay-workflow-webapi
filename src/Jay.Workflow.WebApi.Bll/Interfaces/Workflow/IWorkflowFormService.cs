using Jay.Workflow.WebApi.Common.Interface.Base;
using Jay.Workflow.WebApi.Common.Models.Base;
using Jay.Workflow.WebApi.Model.Dtos.Business.Workflow.WorkflowForm;
using Jay.Workflow.WebApi.Model.Dtos.Request.Workflow.WorkflowForm;
using Jay.Workflow.WebApi.Model.Dtos.Response.Workflow.WorkflowForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Bll.Interfaces.Workflow
{
    public interface IWorkflowFormService : IScopedDependency
    {
        Task<PagingResponse<GetPageWorkflowFormsResp>> GetPageWorkflowFormsAsync(GetPageWorkflowFormsReq req);

        Task<WorkflowFormDto> GetWorkflowFormAsync(Guid workflowFormId);

        Task<WorkflowFormDto> CreateWorkflowFormAsync(CreateWorkflowFormReq req);

        Task<WorkflowFormDto> UpdateWorkflowFormAsync(Guid workflowFormId, UpdateWorkflowFormReq req);

        Task<bool> DeleteWorkflowFormAsync(Guid workflowFormId);
    }
}
