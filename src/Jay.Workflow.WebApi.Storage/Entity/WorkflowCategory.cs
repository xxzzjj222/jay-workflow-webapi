using Jay.Workflow.WebApi.Common.Enums;
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
    /// 工作流分类
    /// </summary>
    public class WorkflowCategory : BaseEntity
    {
        /// <summary>
        /// 分类id
        /// </summary>
        [Column("category_id")]
        [Comment("分类id")]
        public Guid CategoryId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Column("name")]
        [Comment("名称")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 上级id
        /// </summary>
        [Column("parent_id")]
        [Comment("上级id")]
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        [Comment("备注")]
        public string? Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Column("status")]
        [Comment("状态")]
        public EnableDisableEnum Status { get; set; } = EnableDisableEnum.Enable;
    }
}
