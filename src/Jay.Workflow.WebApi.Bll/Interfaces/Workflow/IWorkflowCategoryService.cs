using Jay.Workflow.WebApi.Common.Interface.Base;
using Jay.Workflow.WebApi.Common.Models.Base;
using Jay.Workflow.WebApi.Model.Dtos.Request.Workflow.WorkflowCategory;
using Jay.Workflow.WebApi.Model.Dtos.Response.Workflow.WorkflowCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Bll.Interfaces.Workflow
{
    public interface IWorkflowCategoryService : IScopedDependency
    {
        Task<PagingResponse<GetPageWorkflowCategoriesResp>> GetPageWorkflowCategoriesAsync(GetPageWorkflowCategoriesReq req);

        Task<int> CreateWorkflowCategoryAsync(CreateWorkflowCategoryReq req);
    }
}
