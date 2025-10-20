using Jay.Workflow.WebApi.Common.Enums;
using Jay.Workflow.WebApi.Common.Configurations.DbConfiguration;

namespace Jay.Workflow.WebApi.Service.Extensions
{
    public static class ConfigurationBuilderExtension
    {
        public static IConfigurationBuilder AddDbConfiguration(this IConfigurationBuilder builder)
        {
            if(builder == null) throw new Exception(nameof(builder));

            var configuration= builder.Build();

            return builder.AddDbConfiguration(configuration["Db:WorkflowDb:ConnStr"], ConfigTypeEnum.Env);              
        }
    }
}
