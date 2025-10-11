using Jay.NC.Workflow.WebApi.Common.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.NC.Workflow.WebApi.Common.Configurations.DbConfiguration
{
    public static class DbConfigurationExtensions
    {
        public static IConfigurationBuilder AddDbConfiguration(this IConfigurationBuilder builder, string connStr, ConfigTypeEnum? configType = null)
        {
            if (builder == null) throw new Exception(nameof(builder));

            if (string.IsNullOrWhiteSpace(connStr)) throw new Exception(nameof(connStr));

            var configOption = new DbConfigurationOption
            {
                ConnStr = connStr,
                ConfigType = configType
            };

            return builder.Add(new DbConfigurationSource(configOption));
        }

        public static IConfigurationBuilder AddDbConfiguration(this IConfigurationBuilder builder, Action<DbConfigurationOption> optionAction)
        {
            if (builder == null) throw new Exception(nameof(builder));

            if (optionAction == null) throw new Exception(nameof(optionAction));

            var configOption = new DbConfigurationOption();

            optionAction(configOption);

            return builder.Add(new DbConfigurationSource(configOption));
        }
    }
}
