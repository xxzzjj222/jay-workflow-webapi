using Jay.Workflow.WebApi.Bll.Interfaces.Login;
using Jay.Workflow.WebApi.Bll.Interfaces.User;
using Jay.Workflow.WebApi.Bll.Interfaces.Workflow;
using Jay.Workflow.WebApi.Common.Models.Base;
using Jay.Workflow.WebApi.Common.Utils;
using Jay.Workflow.WebApi.Dal.Interfaces.Workflow;
using Jay.Workflow.WebApi.Model.Dtos.Request.Login;
using Jay.Workflow.WebApi.Model.Dtos.Request.User;
using Jay.Workflow.WebApi.Model.Dtos.Request.Workflow.WorkflowCategory;
using Jay.Workflow.WebApi.Model.Dtos.Response.Workflow.WorkflowCategory;
using Jay.Workflow.WebApi.Service.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jay.Workflow.WebApi.Service.Controllers.Business
{
    /// <summary>
    /// 登录控制器
    /// </summary>
    [Route("api/workflow/category")]
    public class WorkflowCategoryController : BusinessController
    {
        private readonly IWorkflowCategoryService _workflowCategoryService;

        /// <summary>
        /// 构造
        /// </summary>
        public WorkflowCategoryController(IWorkflowCategoryService workflowCategoryService)
        {
            _workflowCategoryService = workflowCategoryService;
        }

        /// <summary>
        /// 分页查询用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet("categories/page")]
        public async Task<PagingResponse<GetPageWorkflowCategoriesResp>> GetUsersPageAsync([FromQuery]GetPageWorkflowCategoriesReq req)
        {
            return await _workflowCategoryService.GetPageWorkflowCategoriesAsync(req);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("category")]
        public async Task<bool> CreateWorkflowCategoryAsync(CreateWorkflowCategoryReq req)
        {
            return await _workflowCategoryService.CreateWorkflowCategoryAsync(req) > 0;
        }
    }
}
