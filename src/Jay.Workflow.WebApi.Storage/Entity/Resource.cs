using Jay.Workflow.WebApi.Common.Enums;
using Jay.Workflow.WebApi.Storage.Entity.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jay.Workflow.WebApi.Storage.Entity
{
    /// <summary>
    /// 资源表
    /// </summary>
    [Table("resource")]
    [Comment("资源表")]
    public class Resource : BaseEntity
    {
        /// <summary>
        /// 资源id
        /// </summary>
        [Column("resource_id")]
        [Comment("资源id")]
        public Guid ResourceId { get; set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        [Column("resource_name")]
        [Comment("资源名称")]
        public string ResourceName { get; set; } = string.Empty;

        /// <summary>
        /// 应用id
        /// </summary>
        [Column("app_id")]
        [Comment("应用id")]
        public Guid? AppId { get; set; }

        /// <summary>
        /// 资源Url
        /// </summary>
        [Column("resource_url")]
        [Comment("资源Url")]
        public string? ResourceUrl { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [Column("icon")]
        [Comment("图标")]
        public string? Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Column("sort")]
        [Comment("排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        [Column("is_show")]
        [Comment("是否显示")]
        public bool IsShow { get; set; }

        /// <summary>
        /// 资源类型
        /// </summary>
        [Column("resource_type")]
        [Comment("资源类型")]
        public ResourceType ResourceType { get; set; }

        /// <summary>
        /// 按钮类型
        /// </summary>
        [Column("button_type")]
        [Comment("按钮类型")]
        public ButtonType? ButtonType { get; set; }

        /// <summary>
        /// 上级id
        /// </summary>
        [Column("parent_id")]
        [Comment("上级id")]
        public Guid? ParentId { get; set; }
    }
}
