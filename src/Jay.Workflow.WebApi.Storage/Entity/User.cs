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
    /// 用户表
    /// </summary>
    [Table("user")]
    [Comment("用户表")]
    public class User:BaseEntity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Column("user_id")]
        [Comment("用户id")]
        public Guid UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [Column("user_name")]
        [Comment("用户名称")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 用户头像
        /// </summary>
        [Column("user_avatar")]
        [Comment("用户头像")]
        public string? UserAvatar { get; set; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        [Column("user_phone")]
        [Comment("用户手机号")]
        public string UserPhone { get; set; } = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        [Column("password")]
        [Comment("密码")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 用户邮箱
        /// </summary>
        [Column("user_email")]
        [Comment("用户邮箱")]
        public string? UserEmail { get; set; }
    }
}
