using Jay.Workflow.WebApi.Common.Enums;
using Jay.Workflow.WebApi.Storage.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Storage.Entity
{
    [Table("department")]
    public class Department:BaseEntity
    {
        /// <summary>
        /// 部门id
        /// </summary>
        [Column("dept_id")]
        public Guid DeptId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [Column("dept_name")]
        public string DeptName { get; set; } = string.Empty;

        /// <summary>
        /// 部门图标
        /// </summary>
        [Column("dept_icon")]
        public string? DeptIcon { get; set; }

        /// <summary>
        /// 部门类型id
        /// </summary>
        [Column("dept_type_id")]
        public int? DeptTypeId { get; set; }

        /// <summary>
        /// 部门类型名称
        /// </summary>
        [Column("dept_type_name")]
        public string? DeptTypeName { get; set; }

        /// <summary>
        /// 上级部门id
        /// </summary>
        [Column("parent_dept_id")]
        public Guid? ParentDeptId { get; set; }

        /// <summary>
        /// 启用状态，0：禁用，1：启用'
        /// </summary>
        [Column("status")]
        public EnableDisableEnum Status { get; set; } = EnableDisableEnum.Enable;

        /// <summary>
        /// 排序
        /// </summary>
        [Column("sort_no")]
        public int SortNo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string? Remark { get; set; }
    }
}
