using AspectCore.Extensions.DependencyInjection;
using Jay.Workflow.WebApi.Common.Exceptions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.DynamicInterceptor
{
    /// <summary>
    /// 动态拦截器宿主服务扩展类
    /// </summary>
    public static class DynamicInterceptorHostBuilderExtensions
    {
        public static IHostBuilder ConfigureDynamicInterceptor(this IHostBuilder hostBuilder)
        {
            if (hostBuilder == null)
            {
                throw new InternalServerException(nameof(hostBuilder));
            }

            return hostBuilder.UseServiceProviderFactory(new DynamicProxyServiceProviderFactory());
        }
    }
}
