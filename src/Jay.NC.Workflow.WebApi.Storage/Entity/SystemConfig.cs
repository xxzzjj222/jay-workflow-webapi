using Jay.NC.Workflow.WebApi.Common.Enums;
using Jay.NC.Workflow.WebApi.Storage.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.NC.Workflow.WebApi.Storage.Entity
{
    /// <summary>
    /// 系统配置表
    /// </summary>
    [Table("system_config")]
    public class SystemConfig : BaseEntity
    {
        /// <summary>
        /// 配置key
        /// </summary>
        [Column("config_key")]
        public string ConfigKey { get; set; } = string.Empty;

        /// <summary>
        /// 配置名称
        /// </summary>
        [Column("config_name")]
        public string ConfigName { get; set; }= string.Empty;

        /// <summary>
        /// 配置值
        /// </summary>
        [Column("config_value")]
        public string ConfigValue { get; set; }=string.Empty;

        /// <summary>
        /// 配置类型
        /// </summary>
        [Column("config_type")]
        public ConfigTypeEnum ConfigType { get; set; } = ConfigTypeEnum.None;

        /// <summary>
        /// 扩展数据
        /// </summary>
        [Column("ext_data")]
        public string? ExtData { get; set; }

        /// <summary>
        /// 排序编号
        /// </summary>
        [Column("sort_no")]
        public int SortNo { get; set; }

        /// <summary>
        /// 启用状态，0：禁用，1：启用
        /// </summary>
        [Column("status")]
        public EnableDisableEnum Status { get; set; } = EnableDisableEnum.Enable;

        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string? Remark { get; set; }
    }
}
