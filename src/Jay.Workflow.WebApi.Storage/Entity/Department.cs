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
    /// 部门表
    /// </summary>
    [Table("department")]
    [Comment("部门表")]
    public class Department:BaseEntity
    {
        /// <summary>
        /// 部门id
        /// </summary>
        [Column("dept_id")]
        [Comment("部门id")]
        public Guid DeptId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [Column("dept_name")]
        [Comment("部门名称")]
        public string DeptName { get; set; } = string.Empty;

        /// <summary>
        /// 部门图标
        /// </summary>
        [Column("dept_icon")]
        [Comment("部门图标")]
        public string? DeptIcon { get; set; }

        /// <summary>
        /// 部门类型id
        /// </summary>
        [Column("dept_type_id")]
        [Comment("部门类型id")]
        public int? DeptTypeId { get; set; }

        /// <summary>
        /// 部门类型名称
        /// </summary>
        [Column("dept_type_name")]
        [Comment("部门类型名称")]
        public string? DeptTypeName { get; set; }

        /// <summary>
        /// 上级部门id
        /// </summary>
        [Column("parent_dept_id")]
        [Comment("上级部门id")]
        public Guid? ParentDeptId { get; set; }

        /// <summary>
        /// 启用状态，0：禁用，1：启用
        /// </summary>
        [Column("status")]
        [Comment("启用状态，0：禁用，1：启用")]
        public EnableDisableEnum Status { get; set; } = EnableDisableEnum.Enable;

        /// <summary>
        /// 排序
        /// </summary>
        [Column("sort_no")]
        [Comment("排序")]
        public int SortNo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        [Comment("备注")]
        public string? Remark { get; set; }
    }
}
