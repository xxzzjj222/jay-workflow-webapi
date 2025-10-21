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
    /// 角色资源表
    /// </summary>
    [Table("role_resource")]
    [Comment("角色资源表")]
    public class RoleResource:BaseEntity
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [Column("role_id")]
        [Comment("角色ID")]
        public Guid RoleId { get; set; }

        /// <summary>
        /// 资源id
        /// </summary>
        [Column("resource_id")]
        [Comment("资源id")]
        public Guid ResourceId { get; set; }
    }
}
