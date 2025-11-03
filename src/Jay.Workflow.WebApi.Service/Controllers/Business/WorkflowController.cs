using Jay.Workflow.WebApi.Bll.Interfaces.Workflow;
using Jay.Workflow.WebApi.Common.Models.Base;
using Jay.Workflow.WebApi.Common.Utils;
using Jay.Workflow.WebApi.Model.Dtos.Business.Workflow.Workflow;
using Jay.Workflow.WebApi.Model.Dtos.Request.Workflow.Workflow;
using Jay.Workflow.WebApi.Model.Dtos.Response.Workflow.Workflow;
using Jay.Workflow.WebApi.Service.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Jay.Workflow.WebApi.Service.Controllers.Business
{
    /// <summary>
    /// 工作流控制器
    /// </summary>
    [Route("api/workflow")]
    public class WorkflowController : BusinessController
    {
        private readonly IWorkflowService _workflowService;

        /// <summary>
        /// 构造
        /// </summary>
        public WorkflowController(IWorkflowService workflowService)
        {
            _workflowService = workflowService;
        }

        /// <summary>
        /// 分页查询工作流
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet("workflows/page")]
        public async Task<PagingResponse<GetPageWorkflowsResp>> GetPageWorkflowsAsync([FromQuery]GetPageWorkflowsReq req)
        {
            return await _workflowService.GetPageWorkflowsAsync(req);
        }

        /// <summary>
        /// 查询工作流
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [HttpGet("workflows/workflowId")]
        public async Task<WorkflowDto> GetWorkflowAsync(Guid workflowId)
        {
            return await _workflowService.GetWorkflowAsync(workflowId);
        }

        /// <summary>
        /// 创建工作流
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("workflows")]
        public async Task<WorkflowDto> CreateWorkflowAsync(CreateWorkflowReq req)
        {
            ValidateHelper.IsNullOrWhiteSpace(req.FlowName, "工作流名称不能为空");

            return await _workflowService.CreateWorkflowAsync(req);
        }

        /// <summary>
        /// 更新工作流分裂
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("workflows/workflowId")]
        public async Task<WorkflowDto> UpdateWorkflowAsync(Guid workflowId, UpdateWorkflowReq req)
        {
            ValidateHelper.IsNullOrWhiteSpace(req.FlowName, "工作流名称不能为空");

            return await _workflowService.UpdateWorkflowAsync(workflowId, req);
        }

        /// <summary>
        /// 删除工作流
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [HttpDelete("workflows/workflowId")]
        public async Task<bool> DeleteWorkflowAsync(Guid workflowId)
        {
            return await _workflowService.DeleteWorkflowAsync(workflowId);
        }
    }
}
