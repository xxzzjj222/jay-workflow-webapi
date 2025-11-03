using Jay.Workflow.WebApi.Common.Models.Base;
using Jay.Workflow.WebApi.Model.Enums.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Model.Dtos.Request.Workflow.WorkflowForm
{
    public class UpdateWorkflowFormReq:RequestDto
    {
        /// <summary>
        /// 表单名称
        /// </summary>
        public string FormName { get; set; } = string.Empty;

        /// <summary>
        /// 表单类型
        /// </summary>
        public WorkflowFormType FormType { get; set; }

        /// <summary>
        /// 表单内容
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// 原表单内容
        /// </summary>
        public string? OriginalContent { get; set; }

        /// <summary>
        /// 表单url
        /// </summary>
        public string? FormUrl { get; set; }
    }
}
