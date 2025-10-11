using Jay.NC.Workflow.WebApi.Common.Interface;
using Jay.NC.Workflow.WebApi.Common.Utils;
using System.Reflection;

namespace Jay.NC.Workflow.WebApi.Service.Extensions
{
    /// <summary>
    /// 添加业务对象注入服务
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddBussinessObjectInjection(this IServiceCollection services)
        {
            var assemblies = AssemblyHelper.GetAssemblies("Jay.NC.Workflow.WebApi.*.dll");

            services.InjectLifetimeService(assemblies, typeof(ITransientDependency), ServiceLifetime.Transient);
            services.InjectLifetimeService(assemblies, typeof(IScopedDependency), ServiceLifetime.Scoped);
            services.InjectLifetimeService(assemblies, typeof(ISingletonDependency), ServiceLifetime.Singleton);

            return services;
        }

        private static IServiceCollection InjectLifetimeService(this IServiceCollection services,IEnumerable<Assembly> assemblies,Type type,ServiceLifetime serviceLifetime)
        {
            var injectTypes = assemblies.SelectMany(a => a.GetTypes().Where(t => t.IsClass && t.GetInterfaces().Contains(type))).ToList();

            injectTypes.ForEach(implementType =>
            {
                implementType.GetInterfaces().ToList().ForEach(serviceType =>
                {
                    if (serviceType != type && !implementType.IsGenericType)
                    {
                        services.Add(new ServiceDescriptor(serviceType, implementType, serviceLifetime));
                    }
                });
            });

            return services;
        }
    }
}
