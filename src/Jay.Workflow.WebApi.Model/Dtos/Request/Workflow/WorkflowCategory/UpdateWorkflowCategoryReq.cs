using Jay.Workflow.WebApi.Common.Enums;
using Jay.Workflow.WebApi.Common.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Model.Dtos.Request.Workflow.WorkflowCategory
{
    public class UpdateWorkflowCategoryReq:RequestDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 上级id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public EnableDisableEnum Status { get; set; } = EnableDisableEnum.Enable;
    }
}
