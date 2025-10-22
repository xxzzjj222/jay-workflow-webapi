using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using Jay.Workflow.WebApi.Common.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.DynamicInterceptor
{
    /// <summary>
    /// 动态拦截器服务集合扩展类
    /// </summary>
    public static class DynamicInterceptorServiceCollectionExtensions
    {
        public static IServiceCollection AddDynamicInterceptor(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new InternalServerException(nameof(services));
            }

            // 配置基于拦截器特性的动态代理
            services.ConfigureDynamicProxy();

            return services;
        }

        public static IServiceCollection AddDynamicInterceptor(this IServiceCollection services, Action<IAspectConfiguration> configure)
        {
            if (services == null)
            {
                throw new InternalServerException(nameof(services));
            }

            if (configure == null)
            {
                throw new InternalServerException(nameof(configure));
            }

            // 配置全局动态代理
            services.ConfigureDynamicProxy(configure);

            return services;
        }
    }
}
