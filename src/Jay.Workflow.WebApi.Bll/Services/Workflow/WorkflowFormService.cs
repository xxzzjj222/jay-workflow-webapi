using AutoMapper;
using Jay.Workflow.WebApi.Bll.Interfaces.Workflow;
using Jay.Workflow.WebApi.Common.Exceptions;
using Jay.Workflow.WebApi.Common.Models.Base;
using Jay.Workflow.WebApi.Common.Uow;
using Jay.Workflow.WebApi.Dal.Interfaces.Workflow;
using Jay.Workflow.WebApi.Model.Dtos.Business.Workflow.WorkflowForm;
using Jay.Workflow.WebApi.Model.Dtos.Request.Workflow.WorkflowForm;
using Jay.Workflow.WebApi.Model.Dtos.Response.Workflow.WorkflowForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Bll.Services.Workflow
{
    public class WorkflowFormService:IWorkflowFormService
    {
        private readonly IWorkflowFormDalService _workflowFormDalService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitofWork;

        public WorkflowFormService(IWorkflowFormDalService workflowFormDalService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _workflowFormDalService = workflowFormDalService;
            _mapper = mapper;
            _unitofWork = unitOfWork;
        }

        /// <summary>
        /// 分页查询工作流表单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<PagingResponse<GetPageWorkflowFormsResp>> GetPageWorkflowFormsAsync(GetPageWorkflowFormsReq req)
        {
            var (data, count) = await _workflowFormDalService.GetPageWorkflowFormsAsync(req.PageIndex, req.PageSize, req.Keyword);
            var respData = _mapper.Map<List<GetPageWorkflowFormsResp>>(data);

            return new PagingResponse<GetPageWorkflowFormsResp>
            {
                Datas = respData,
                Count = count
            };
        }

        /// <summary>
        /// 查询工作流表单
        /// </summary>
        /// <param name="workflowFormId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidParameterException"></exception>
        public async Task<WorkflowFormDto> GetWorkflowFormAsync(Guid workflowFormId)
        {
            var entity = await _workflowFormDalService.FirstOrDefaultAsync(e => e.FormId == workflowFormId).ConfigureAwait(false);
            if (entity == null) throw new InvalidParameterException("工作流表单不存在");

            return _mapper.Map<WorkflowFormDto>(entity);
        }

        /// <summary>
        /// 创建工作流表单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        /// <exception cref="InvalidParameterException"></exception>
        public async Task<WorkflowFormDto> CreateWorkflowFormAsync(CreateWorkflowFormReq req)
        {
            var existWorkflowForm = await _workflowFormDalService.FirstOrDefaultAsyncAsNoTracking(e => e.FormName == req.FormName).ConfigureAwait(false);
            if (existWorkflowForm != null) throw new InvalidParameterException("工作流表单名称已存在");

            var workflowForm = new Storage.Entity.WorkflowForm
            {
                FormId = Guid.NewGuid(),
                FormName= req.FormName,
                FormType=req.FormType,
                FormUrl=req.FormUrl,
                Content=req.Content,
                OriginalContent=req.OriginalContent
            };

            await _workflowFormDalService.AddAndSaveChanges(workflowForm).ConfigureAwait(false);

            return _mapper.Map<WorkflowFormDto>(workflowForm);
        }

        /// <summary>
        /// 更新工作流表单
        /// </summary>
        /// <param name="workflowFormId"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        /// <exception cref="InvalidParameterException"></exception>
        public async Task<WorkflowFormDto> UpdateWorkflowFormAsync(Guid workflowFormId, UpdateWorkflowFormReq req)
        {
            var entity = await _workflowFormDalService.FirstOrDefaultAsync(e => e.FormId == workflowFormId).ConfigureAwait(false);
            if (entity == null) throw new InvalidParameterException("工作流表单不存在");

            var existEntity = await _workflowFormDalService.FirstOrDefaultAsyncAsNoTracking(e => e.FormId == workflowFormId).ConfigureAwait(false);
            if (existEntity != null) throw new InvalidParameterException("工作流表单名称已存在");

            entity.FormName = req.FormName;
            entity.FormType = req.FormType;
            entity.FormUrl = req.FormUrl;
            entity.Content = req.Content;
            entity.OriginalContent = req.OriginalContent;

            await _workflowFormDalService.UpdateAsync(entity).ConfigureAwait(false);
            await _unitofWork.CommitAsync().ConfigureAwait(false);

            return _mapper.Map<WorkflowFormDto>(entity);
        }

        /// <summary>
        /// 删除工作流表单
        /// </summary>
        /// <param name="workflowFormId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidParameterException"></exception>
        public async Task<bool> DeleteWorkflowFormAsync(Guid workflowFormId)
        {
            var entity = await _workflowFormDalService.FirstOrDefaultAsync(e => e.FormId == workflowFormId).ConfigureAwait(false);
            if (entity == null) throw new InvalidParameterException("工作流表单不存在");

            await _workflowFormDalService.RemoveAsync(entity).ConfigureAwait(false);
            return await _unitofWork.CommitAsync().ConfigureAwait(false) > 0;
        }
    }
}
