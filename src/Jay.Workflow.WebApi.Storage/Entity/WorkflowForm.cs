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
    [Table("workflow_form")]
    [Comment("工作流表单表")]
    public class WorkflowForm:BaseEntity
    {
        /// <summary>
        /// 表单id
        /// </summary>
        [Column("form_id")]
        [Comment("表单id")]
        public Guid FormId { get; set; }

        /// <summary>
        /// 表单名称
        /// </summary>
        [Column("form_name")]
        [Comment("表单名称")]
        public string FormName { get; set; } = string.Empty;

        /// <summary>
        /// 表单类型
        /// </summary>
        [Column("form_type")]
        [Comment("表单类型")]
        public WorkflowFormType FormType { get; set; }

        /// <summary>
        /// 表单内容
        /// </summary>
        [Column("content")]
        [Comment("表单内容")]
        public string? Content {  get; set; }

        /// <summary>
        /// 原表单内容
        /// </summary>
        [Column("original_content")]
        [Comment("原表单内容")]
        public string? OriginalContent {  get; set; }

        /// <summary>
        /// 表单url
        /// </summary>
        [Column("form_url")]
        [Comment("表单url")]
        public string? FormUrl { get; set; }
    }
}
