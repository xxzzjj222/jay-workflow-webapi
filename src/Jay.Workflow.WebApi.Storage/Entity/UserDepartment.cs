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
    /// 用户部门表
    /// </summary>
    [Table("user_department")]
    [Comment("用户部门表")]
    public class UserDepartment
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Column("user_id")]
        [Comment("用户id")]
        public Guid UserId { get; set; }

        /// <summary>
        /// 部门id
        /// </summary>
        [Column("dept_id")]
        [Comment("部门id")]
        public Guid DeptId { get; set; }
    }
}
