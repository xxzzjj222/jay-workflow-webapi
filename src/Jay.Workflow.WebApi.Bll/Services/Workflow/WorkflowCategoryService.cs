using AutoMapper;
using Jay.Workflow.WebApi.Bll.Interfaces.Workflow;
using Jay.Workflow.WebApi.Common.Exceptions;
using Jay.Workflow.WebApi.Common.Models.Base;
using Jay.Workflow.WebApi.Dal.Interfaces.Workflow;
using Jay.Workflow.WebApi.Model.Dtos.Request.Workflow.WorkflowCategory;
using Jay.Workflow.WebApi.Model.Dtos.Response.Workflow.WorkflowCategory;
using Jay.Workflow.WebApi.Storage.Entity;
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

        public WorkflowCategoryService(IWorkflowCategoryDalService workflowCategoryDalService,IMapper mapper)
        {
            _workflowCategoryDalService = workflowCategoryDalService;
            _mapper = mapper;
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
        /// 创建工作流分类
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        /// <exception cref="InvalidParameterException"></exception>
        public async Task<int> CreateWorkflowCategoryAsync(CreateWorkflowCategoryReq req)
        {
            var existWorkflowCategory = await _workflowCategoryDalService.FirstOrDefaultAsyncAsNoTracking(w => w.Name == req.Name).ConfigureAwait(false);
            if (existWorkflowCategory != null) throw new InvalidParameterException("工作流分类名称已存在");

            var workflowCategory = new Storage.Entity.WorkflowCategory
            {
                CategoryId = Guid.NewGuid(),
                Name = req.Name,
                Status = req.Status,
                Remark = req.Remark,
                ParentId = req.ParentId
            };

            return await _workflowCategoryDalService.AddAndSaveChanges(workflowCategory).ConfigureAwait(false);
        }
    }
}
