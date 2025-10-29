using Jay.Workflow.WebApi.Common.Enums;
using Jay.Workflow.WebApi.Common.Models.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Model.Dtos.Response.Workflow.WorkflowCategory
{
    /// <summary>
    /// 工作流分类返回模型
    /// </summary>
    public class GetPageWorkflowCategoriesResp:ResponseDto
    {
        /// <summary>
        /// 分类id
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 上级id
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public EnableDisableEnum Status { get; set; } = EnableDisableEnum.Enable;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
    }
}
