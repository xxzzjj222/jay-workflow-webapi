using Jay.NC.Workflow.WebApi.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.NC.Workflow.WebApi.Common.Configurations.DbConfiguration
{
    /// <summary>
    /// 数据库配置选项
    /// </summary>
    public class DbConfigurationOption
    {
        /// <summary>
        /// 配置库连接字符串
        /// </summary>
        public string ConnStr { get; set; } = string.Empty;

        /// <summary>
        /// 配置所属表
        /// </summary>
        public string TableName { get; set; } = "system_config";

        /// <summary>
        /// 配置Key所属列
        /// </summary>
        public string ConfigKeyField { get; set; } = "config_key";

        /// <summary>
        /// 配置Value所属列
        /// </summary>
        public string ConfigValueField { get; set; } = "config_value";

        /// <summary>
        /// 配置类型
        ///     仅当需要使用系统配置表(system_config)中的配置类型进行过滤时使用
        ///     通常使用ConfigTypeEnum.Env来过滤出环境变量的配置数据
        /// </summary>
        public ConfigTypeEnum? ConfigType { get; set; }

        /// <summary>
        /// 变更时是否重新加载
        /// </summary>
        public bool ReloadOnChange { get; set; } = true;
    }
}
