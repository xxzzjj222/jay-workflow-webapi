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
    /// 用户角色表
    /// </summary>
    [Table("user_role")]
    [Comment("用户角色表")]
    public class UserRole:BaseEntity
    {   
        /// <summary>
        /// 用户id
        /// </summary>
        [Column("user_id")]
        [Comment("用户id")]
        public Guid UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [Column("role_id")]
        [Comment("角色ID")]
        public Guid RoleId { get; set; }
    }
}
