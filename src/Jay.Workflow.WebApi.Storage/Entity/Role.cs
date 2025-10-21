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
    /// 角色表
    /// </summary>
    [Table("role")]
    [Comment("角色表")]
    public class Role:BaseEntity
    {
        /// <summary>
        /// 角色id
        /// </summary>
        [Column("role_id")]
        [Comment("角色id")]
        public Guid RoleId { get; set; }

        /// <summary>
        /// 角色编码
        /// </summary>
        [Column("role_code")]
        [Comment("角色编码")]
        public string RoleCode { get; set; } = string.Empty;

        /// <summary>
        /// 角色名称
        /// </summary>
        [Column("role_name")]
        [Comment("角色名称")]
        public string RoleName { get; set; } = string.Empty;

        /// <summary>
        /// 角色名称
        /// </summary>
        [Column("role_description")]
        [Comment("角色描述")]
        public string? RoleDescription { get; set; }
    }
}
