using AutoMapper;
using Jay.Workflow.WebApi.Bll.Interfaces.Workflow;
using Jay.Workflow.WebApi.Common.Exceptions;
using Jay.Workflow.WebApi.Common.Models.Base;
using Jay.Workflow.WebApi.Common.Uow;
using Jay.Workflow.WebApi.Dal.Interfaces.Workflow;
using Jay.Workflow.WebApi.Model.Dtos.Business.Workflow.Workflow;
using Jay.Workflow.WebApi.Model.Dtos.Request.Workflow.Workflow;
using Jay.Workflow.WebApi.Model.Dtos.Response.Workflow.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Bll.Services.Workflow
{
    public class WorkflowService:IWorkflowService
    {
        private readonly IWorkflowDalService _workflowDalService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitofWork;

        public WorkflowService(IWorkflowDalService workflowDalService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _workflowDalService = workflowDalService;
            _mapper = mapper;
            _unitofWork = unitOfWork;
        }

        /// <summary>
        /// 分页查询工作流
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<PagingResponse<GetPageWorkflowsResp>> GetPageWorkflowsAsync(GetPageWorkflowsReq req)
        {
            var (data, count) = await _workflowDalService.GetPageWorkflowsAsync(req.PageIndex, req.PageSize, req.Keyword);
            var respData = _mapper.Map<List<GetPageWorkflowsResp>>(data);

            return new PagingResponse<GetPageWorkflowsResp>
            {
                Datas = respData,
                Count = count
            };
        }

        /// <summary>
        /// 查询工作流
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidParameterException"></exception>
        public async Task<WorkflowDto> GetWorkflowAsync(Guid workflowId)
        {
            var entity = await _workflowDalService.FirstOrDefaultAsync(e => e.FormId == workflowId).ConfigureAwait(false);
            if (entity == null) throw new InvalidParameterException("工作流不存在");

            return _mapper.Map<WorkflowDto>(entity);
        }

        /// <summary>
        /// 创建工作流
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        /// <exception cref="InvalidParameterException"></exception>
        public async Task<WorkflowDto> CreateWorkflowAsync(CreateWorkflowReq req)
        {
            var existEntity = await _workflowDalService.FirstOrDefaultAsyncAsNoTracking(e => e.FlowName == req.FlowName).ConfigureAwait(false);
            if (existEntity != null) throw new InvalidParameterException("工作流名称已存在");

            var workflow = new Storage.Entity.Workflow
            {
               FlowId=Guid.NewGuid(),
               FlowName=req.FlowName,
               FlowCode=req.FlowCode,
               FlowContent=req.FlowContent,
               FlowVersion=req.FlowVersion,
               FormId=req.FormId,
               CategoryId=req.CategoryId,
               Status=req.Status,
               Remark=req.Remark
            };

            await _workflowDalService.AddAndSaveChanges(workflow).ConfigureAwait(false);

            return _mapper.Map<WorkflowDto>(workflow);
        }

        /// <summary>
        /// 更新工作流
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        /// <exception cref="InvalidParameterException"></exception>
        public async Task<WorkflowDto> UpdateWorkflowAsync(Guid workflowId, UpdateWorkflowReq req)
        {
            var entity = await _workflowDalService.FirstOrDefaultAsync(e => e.FormId == workflowId).ConfigureAwait(false);
            if (entity == null) throw new InvalidParameterException("工作流不存在");

            var existEntity = await _workflowDalService.FirstOrDefaultAsyncAsNoTracking(e => e.FormId == workflowId).ConfigureAwait(false);
            if (existEntity != null) throw new InvalidParameterException("工作流名称已存在");

            entity.FlowName = req.FlowName;
            entity.FlowContent = req.FlowContent;
            entity.FlowVersion = req.FlowVersion;
            entity.FormId = req.FormId;
            entity.CategoryId = req.CategoryId;
            entity.Status = req.Status;

            entity.Remark = req.Remark;

            await _workflowDalService.UpdateAsync(entity).ConfigureAwait(false);
            await _unitofWork.CommitAsync().ConfigureAwait(false);

            return _mapper.Map<WorkflowDto>(entity);
        }

        /// <summary>
        /// 删除工作流
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidParameterException"></exception>
        public async Task<bool> DeleteWorkflowAsync(Guid workflowId)
        {
            var entity = await _workflowDalService.FirstOrDefaultAsync(e => e.FormId == workflowId).ConfigureAwait(false);
            if (entity == null) throw new InvalidParameterException("工作流不存在");

            await _workflowDalService.RemoveAsync(entity).ConfigureAwait(false);
            return await _unitofWork.CommitAsync().ConfigureAwait(false) > 0;
        }
    }
}
