using AutoMapper;
using Jay.Workflow.WebApi.Bll.Interfaces.Workflow;
using Jay.Workflow.WebApi.Common.Exceptions;
using Jay.Workflow.WebApi.Common.Models.Base;
using Jay.Workflow.WebApi.Common.Uow;
using Jay.Workflow.WebApi.Dal.Interfaces.Workflow;
using Jay.Workflow.WebApi.Model.Dtos.Business.Workflow.WorkflowCategory;
using Jay.Workflow.WebApi.Model.Dtos.Request.Workflow.WorkflowCategory;
using Jay.Workflow.WebApi.Model.Dtos.Response.Workflow.WorkflowCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Bll.Services.Workflow
{
    public class WorkflowCategoryService:IWorkflowCategoryService
    {
        private readonly IWorkflowCategoryDalService _workflowCategoryDalService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitofWork;

        public WorkflowCategoryService(IWorkflowCategoryDalService workflowCategoryDalService,IMapper mapper, IUnitOfWork unitOfWork)
        {
            _workflowCategoryDalService = workflowCategoryDalService;
            _mapper = mapper;
            _unitofWork = unitOfWork;
        }

        /// <summary>
        /// 分页查询工作流分类
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<PagingResponse<GetPageWorkflowCategoriesResp>> GetPageWorkflowCategoriesAsync(GetPageWorkflowCategoriesReq req)
        {
            var (data, count) = await _workflowCategoryDalService.GetPageWorkflowCategoriesAsync(req.PageIndex, req.PageSize, req.Keyword);
            var respData = _mapper.Map<List<GetPageWorkflowCategoriesResp>>(data);

            return new PagingResponse<GetPageWorkflowCategoriesResp>
            {
                Datas = respData,
                Count = count
            };
        }

        /// <summary>
        /// 查询工作流分类
        /// </summary>
        /// <param name="workflowCategoryId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidParameterException"></exception>
        public async Task<WorkflowCategoryDto> GetWorkflowCategoryAsync(Guid workflowCategoryId)
        {
            var entity = await _workflowCategoryDalService.FirstOrDefaultAsync(wc => wc.CategoryId == workflowCategoryId).ConfigureAwait(false);
            if (entity == null) throw new InvalidParameterException("工作流分类不存在");

            return _mapper.Map<WorkflowCategoryDto>(entity);
        }

        /// <summary>
        /// 创建工作流分类
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        /// <exception cref="InvalidParameterException"></exception>
        public async Task<WorkflowCategoryDto> CreateWorkflowCategoryAsync(CreateWorkflowCategoryReq req)
        {
            var existWorkflowCategory = await _workflowCategoryDalService.FirstOrDefaultAsyncAsNoTracking(w => w.Name == req.Name && w.ParentId == req.ParentId).ConfigureAwait(false);
            if (existWorkflowCategory != null) throw new InvalidParameterException("工作流分类名称已存在");

            var workflowCategory = new Storage.Entity.WorkflowCategory
            {
                CategoryId = Guid.NewGuid(),
                Name = req.Name,
                Status = req.Status,
                Remark = req.Remark,
                ParentId = req.ParentId
            };

            await _workflowCategoryDalService.AddAndSaveChanges(workflowCategory).ConfigureAwait(false);

            return _mapper.Map<WorkflowCategoryDto>(workflowCategory);
        }

        /// <summary>
        /// 获取工作流分类树
        /// </summary>
        /// <returns></returns>
        public async Task<List<WorkflowCategoryDto>> GetWorkflowCategoriesTreeAsync()
        {
            var workflowCategories = await _workflowCategoryDalService.GetListAsyncAsNoTracking().ConfigureAwait(false);
            var workflowCategoriesDto=_mapper.Map<List<WorkflowCategoryDto>>(workflowCategories);

            var rootWorkflowCategoriesDto = workflowCategoriesDto.Where(wc => wc.ParentId == null).ToList();

            RecursiveWorkflowCategories(rootWorkflowCategoriesDto, workflowCategoriesDto);

            return rootWorkflowCategoriesDto;
        }

        /// <summary>
        /// 递归赋值子分类
        /// </summary>
        /// <param name="rootWorkflowCategoriesDto"></param>
        /// <param name="allWorkflowCategoriesDto"></param>
        private void RecursiveWorkflowCategories(List<WorkflowCategoryDto> rootWorkflowCategoriesDto,List<WorkflowCategoryDto> allWorkflowCategoriesDto)
        {
            foreach (var workflowCategoryDto in rootWorkflowCategoriesDto)
            {
                var childWorlflowCategoriesDto = allWorkflowCategoriesDto.Where(wc => wc.ParentId == workflowCategoryDto.CategoryId).ToList();
                workflowCategoryDto.ChildWorkflowCategories = childWorlflowCategoriesDto;
                RecursiveWorkflowCategories(childWorlflowCategoriesDto, allWorkflowCategoriesDto);
            }
        }

        /// <summary>
        /// 更新工作流分类
        /// </summary>
        /// <param name="workflowCategoryId"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        /// <exception cref="InvalidParameterException"></exception>
        public async Task<WorkflowCategoryDto> UpdateWorkflowCategoryAsync(Guid workflowCategoryId, UpdateWorkflowCategoryReq req)
        {
            var entity = await _workflowCategoryDalService.FirstOrDefaultAsync(wc => wc.CategoryId == workflowCategoryId).ConfigureAwait(false);
            if (entity == null) throw new InvalidParameterException("工作流分类不存在");

            var existEntity = await _workflowCategoryDalService.FirstOrDefaultAsyncAsNoTracking(w => w.Name == req.Name && w.ParentId == req.ParentId).ConfigureAwait(false);
            if (existEntity != null) throw new InvalidParameterException("工作流分类名称已存在");

            entity.Name = req.Name;
            entity.ParentId=req.ParentId;
            entity.Status = req.Status;
            entity.Remark = req.Remark;

            await _workflowCategoryDalService.UpdateAsync(entity).ConfigureAwait(false);
            await _unitofWork.CommitAsync().ConfigureAwait(false);

            return _mapper.Map<WorkflowCategoryDto>(entity);
        }

        /// <summary>
        /// 删除工作流分类
        /// </summary>
        /// <param name="workflowCategoryId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidParameterException"></exception>
        public async Task<bool> DeleteWorkflowCategoryAsync(Guid workflowCategoryId)
        {
            var entity = await _workflowCategoryDalService.FirstOrDefaultAsync(wc => wc.CategoryId == workflowCategoryId).ConfigureAwait(false);
            if (entity == null) throw new InvalidParameterException("工作流分类不存在");

            await _workflowCategoryDalService.RemoveAsync(entity).ConfigureAwait(false);
            return await _unitofWork.CommitAsync().ConfigureAwait(false) > 0;
        }
    }
}
