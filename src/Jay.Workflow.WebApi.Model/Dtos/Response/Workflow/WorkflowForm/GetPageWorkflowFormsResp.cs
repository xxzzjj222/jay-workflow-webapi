using Jay.Workflow.WebApi.Common.Models.Base;
using Jay.Workflow.WebApi.Model.Enums.Workflow;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Model.Dtos.Response.Workflow.WorkflowForm
{
    public class GetPageWorkflowFormsResp:ResponseDto
    {
        /// <summary>
        /// 表单id
        /// </summary>
        public Guid FormId { get; set; }

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

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
    }
}
