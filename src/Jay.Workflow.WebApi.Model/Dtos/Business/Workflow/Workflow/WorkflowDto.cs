using Jay.Workflow.WebApi.Common.Enums;
using Jay.Workflow.WebApi.Common.Models.Base;
using Jay.Workflow.WebApi.Model.Enums.Workflow;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Model.Dtos.Business.Workflow.Workflow
{
    public class WorkflowDto:BusinessDto
    {
        /// <summary>
        /// 表单id
        /// </summary>
        public Guid FlowId { get; set; }

        /// <summary>
        /// 流程编码
        /// </summary>
        public string FlowCode { get; set; } = string.Empty;

        /// <summary>
        /// 流程名称
        /// </summary>
        public string FlowName { get; set; } = string.Empty;

        /// <summary>
        /// 流程内容
        /// </summary>
        public string? FlowContent { get; set; }

        /// <summary>
        /// 流程版本
        /// </summary>
        public int FlowVersion { get; set; } = 1;

        /// <summary>
        /// 状态
        /// </summary>
        public EnableDisableEnum Status { get; set; } = EnableDisableEnum.Enable;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 分类id
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// 表单id
        /// </summary>
        public Guid FormId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
    }
}
