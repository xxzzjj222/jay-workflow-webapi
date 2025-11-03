using Jay.Workflow.WebApi.Bll.Interfaces.Workflow;
using Jay.Workflow.WebApi.Common.Models.Base;
using Jay.Workflow.WebApi.Common.Utils;
using Jay.Workflow.WebApi.Model.Dtos.Business.Workflow.WorkflowCategory;
using Jay.Workflow.WebApi.Model.Dtos.Request.Workflow.WorkflowCategory;
using Jay.Workflow.WebApi.Model.Dtos.Response.Workflow.WorkflowCategory;
using Jay.Workflow.WebApi.Service.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Jay.Workflow.WebApi.Service.Controllers.Business
{
    /// <summary>
    /// 工作流分类控制器
    /// </summary>
    [Route("api/workflow")]
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
        /// 分页查询工作流分类
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet("categories/page")]
        public async Task<PagingResponse<GetPageWorkflowCategoriesResp>> GetPageWorkflowCategoriesAsync([FromQuery]GetPageWorkflowCategoriesReq req)
        {
            return await _workflowCategoryService.GetPageWorkflowCategoriesAsync(req);
        }

        /// <summary>
        /// 查询工作流分类
        /// </summary>
        /// <param name="workflowCategoryId"></param>
        /// <returns></returns>
        [HttpGet("categories/workflowCategoryId")]
        public async Task<WorkflowCategoryDto> GetWorkflowCategoryAsync(Guid workflowCategoryId)
        {
            return await _workflowCategoryService.GetWorkflowCategoryAsync(workflowCategoryId);
        }

        /// <summary>
        /// 创建工作流分类
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("categories")]
        public async Task<WorkflowCategoryDto> CreateWorkflowCategoryAsync(CreateWorkflowCategoryReq req)
        {
            ValidateHelper.IsNullOrWhiteSpace(req.Name, "工作流分类名称不能为空");

            return await _workflowCategoryService.CreateWorkflowCategoryAsync(req);
        }

        /// <summary>
        /// 树形查询工作流分类
        /// </summary>
        /// <returns></returns>
        [HttpGet("categories/tree")]
        public async Task<List<WorkflowCategoryDto>> GetWorkflowCategoriesTreeAsync()
        {
            return await _workflowCategoryService.GetWorkflowCategoriesTreeAsync();
        }

        /// <summary>
        /// 更新工作流分裂
        /// </summary>
        /// <param name="workflowCategoryId"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("categories/workflowCategoryId")]
        public async Task<WorkflowCategoryDto> UpdateWorkflowCategoryAsync(Guid workflowCategoryId, UpdateWorkflowCategoryReq req)
        {
            ValidateHelper.IsNullOrWhiteSpace(req.Name, "工作流分类名称不能为空");

            return await _workflowCategoryService.UpdateWorkflowCategoryAsync(workflowCategoryId, req);
        }

        /// <summary>
        /// 删除工作流分类
        /// </summary>
        /// <param name="workflowCategoryId"></param>
        /// <returns></returns>
        [HttpDelete("categories/workflowCategoryId")]
        public async Task<bool> DeleteWorkflowCategoryAsync(Guid workflowCategoryId)
        {
            return await _workflowCategoryService.DeleteWorkflowCategoryAsync(workflowCategoryId);
        }
    }
}
