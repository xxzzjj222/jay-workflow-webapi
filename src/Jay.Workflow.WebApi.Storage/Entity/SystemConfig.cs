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
    /// 系统配置表
    /// </summary>
    [Table("system_config")]
    [Comment("系统配置表")]
    public class SystemConfig : BaseEntity
    {
        /// <summary>
        /// 配置key
        /// </summary>
        [Column("config_key")]
        [Comment("配置key")]
        public string ConfigKey { get; set; } = string.Empty;

        /// <summary>
        /// 配置名称
        /// </summary>
        [Column("config_name")]
        [Comment("配置名称")]
        public string ConfigName { get; set; }= string.Empty;

        /// <summary>
        /// 配置值
        /// </summary>
        [Column("config_value")]
        [Comment("配置值")]
        public string ConfigValue { get; set; }=string.Empty;

        /// <summary>
        /// 配置类型
        /// </summary>
        [Column("config_type")]
        [Comment("配置类型")]
        public ConfigTypeEnum ConfigType { get; set; } = ConfigTypeEnum.None;

        /// <summary>
        /// 扩展数据
        /// </summary>
        [Column("ext_data")]
        [Comment("扩展数据")]
        public string? ExtData { get; set; }

        /// <summary>
        /// 排序编号
        /// </summary>
        [Column("sort_no")]
        [Comment("排序编号")]
        public int SortNo { get; set; }

        /// <summary>
        /// 启用状态，0：禁用，1：启用
        /// </summary>
        [Column("status")]
        [Comment("启用状态，0：禁用，1：启用")]
        public EnableDisableEnum Status { get; set; } = EnableDisableEnum.Enable;

        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        [Comment("备注")]
        public string? Remark { get; set; }
    }
}
