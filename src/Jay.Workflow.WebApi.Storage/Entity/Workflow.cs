using Jay.Workflow.WebApi.Common.Enums;
using Jay.Workflow.WebApi.Model.Enums.Workflow;
using Jay.Workflow.WebApi.Storage.Entity.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Storage.Entity
{
    /// <summary>
    /// 工作流表单表
    /// </summary>
    [Table("workflow")]
    [Comment("工作流表")]
    public class Workflow:BaseEntity
    {
        /// <summary>
        /// 表单id
        /// </summary>
        [Column("flow_id")]
        [Comment("工作流id")]
        public Guid FlowId { get; set; }

        /// <summary>
        /// 流程编码
        /// </summary>
        [Column("flow_code")]
        [Comment("流程编码")]
        public string FlowCode { get; set; } = string.Empty;

        /// <summary>
        /// 流程名称
        /// </summary>
        [Column("flow_name")]
        [Comment("流程名称")]
        public string FlowName { get; set; } = string.Empty;

        /// <summary>
        /// 流程内容
        /// </summary>
        [Column("flow_content")]
        [Comment("流程内容")]
        public string? FlowContent { get; set; }

        /// <summary>
        /// 流程版本
        /// </summary>
        [Column("flow_version")]
        [Comment("流程版本")]
        public int FlowVersion { get; set; } = 1;

        /// <summary>
        /// 状态
        /// </summary>
        [Column("status")]
        [Comment("状态")]
        public EnableDisableEnum Status { get; set; } = EnableDisableEnum.Enable;

        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        [Comment("备注")]
        public string? Remark { get; set; }

        /// <summary>
        /// 分类id
        /// </summary>
        [Column("category_id")]
        [Comment("分类id")]
        public Guid CategoryId { get; set; }

        /// <summary>
        /// 表单id
        /// </summary>
        [Column("form_id")]
        [Comment("表单id")]
        public Guid FormId { get; set; }
    }
}
