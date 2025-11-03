using Jay.Workflow.WebApi.Bll.Interfaces.Workflow;
using Jay.Workflow.WebApi.Common.Models.Base;
using Jay.Workflow.WebApi.Common.Utils;
using Jay.Workflow.WebApi.Model.Dtos.Business.Workflow.WorkflowForm;
using Jay.Workflow.WebApi.Model.Dtos.Request.Workflow.WorkflowForm;
using Jay.Workflow.WebApi.Model.Dtos.Response.Workflow.WorkflowForm;
using Jay.Workflow.WebApi.Service.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Jay.Workflow.WebApi.Service.Controllers.Business
{
    /// <summary>
    /// 工作流表单控制器
    /// </summary>
    [Route("api/workflow")]
    public class WorkflowFormController : BusinessController
    {
        private readonly IWorkflowFormService _workflowFormService;

        /// <summary>
        /// 构造
        /// </summary>
        public WorkflowFormController(IWorkflowFormService workflowFormService)
        {
            _workflowFormService = workflowFormService;
        }

        /// <summary>
        /// 分页查询工作流表单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet("forms/page")]
        public async Task<PagingResponse<GetPageWorkflowFormsResp>> GetPageWorkflowFormsAsync([FromQuery]GetPageWorkflowFormsReq req)
        {
            return await _workflowFormService.GetPageWorkflowFormsAsync(req);
        }

        /// <summary>
        /// 查询工作流表单
        /// </summary>
        /// <param name="workflowFormId"></param>
        /// <returns></returns>
        [HttpGet("forms/workflowFormId")]
        public async Task<WorkflowFormDto> GetWorkflowFormAsync(Guid workflowFormId)
        {
            return await _workflowFormService.GetWorkflowFormAsync(workflowFormId);
        }

        /// <summary>
        /// 创建工作流表单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("forms")]
        public async Task<WorkflowFormDto> CreateWorkflowFormAsync(CreateWorkflowFormReq req)
        {
            ValidateHelper.IsNullOrWhiteSpace(req.FormName, "工作流表单名称不能为空");

            return await _workflowFormService.CreateWorkflowFormAsync(req);
        }

        /// <summary>
        /// 更新工作流分裂
        /// </summary>
        /// <param name="workflowFormId"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("forms/workflowFormId")]
        public async Task<WorkflowFormDto> UpdateWorkflowFormAsync(Guid workflowFormId, UpdateWorkflowFormReq req)
        {
            ValidateHelper.IsNullOrWhiteSpace(req.FormName, "工作流表单名称不能为空");

            return await _workflowFormService.UpdateWorkflowFormAsync(workflowFormId, req);
        }

        /// <summary>
        /// 删除工作流表单
        /// </summary>
        /// <param name="workflowFormId"></param>
        /// <returns></returns>
        [HttpDelete("forms/workflowFormId")]
        public async Task<bool> DeleteWorkflowFormAsync(Guid workflowFormId)
        {
            return await _workflowFormService.DeleteWorkflowFormAsync(workflowFormId);
        }
    }
}
