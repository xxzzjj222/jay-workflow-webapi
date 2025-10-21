using Jay.Workflow.WebApi.Common.Interface.Base;
using Jay.Workflow.WebApi.Common.Utils;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Grafana.Loki;
using System.Reflection;

namespace Jay.Workflow.WebApi.Service.Extensions
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
            var assemblies = AssemblyHelper.GetAssemblies("Jay.Workflow.WebApi.*.dll");

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

        public static Serilog.ILogger GetLogConfig(string name, bool enableLokiLogging)
        {
            // 日志格式模板
            var SerilogOutputTemplate = "{NewLine}时间:{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}日志等级:{Level}{NewLine}所在类:{SourceContext}{NewLine}日志信息:{Message}{NewLine}{Exception}";

            /*
                fileSizeLimitBytes: 10 * 1024 * 1024 表示日志文件的大小限制为 10MB。当日志文件达到这个大小时，将会触发滚动操作
                retainedFileCountLimit 系统会保留最新的 N 个日志文件，超过部分会被自动删除
                rollOnFileSizeLimit: true 这个设置表示当日志文件达到大小限制时，是否触发滚动操作。设为 true 表示当文件大小达到限制时进行滚动
                shared: true 这个设置表示日志文件是否为共享文件。如果设为 true，则多个进程可以同时写入同一个日志文件。
            */

            var fileSizeLimitBytes = 10 * 1024 * 1024;

            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Debug)
                    .WriteTo.File("Logs/Debug/log.txt", rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate, fileSizeLimitBytes: fileSizeLimitBytes,
                        retainedFileCountLimit: 24 * 7, rollOnFileSizeLimit: true, shared: true))
                .WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Information || p.Level == LogEventLevel.Warning)
                    .WriteTo.File("Logs/Info/log.txt", rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate, fileSizeLimitBytes: fileSizeLimitBytes,
                        retainedFileCountLimit: 24 * 7, rollOnFileSizeLimit: true, shared: true))
                .WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Error || p.Level == LogEventLevel.Fatal)
                    .WriteTo.File("Logs/Error/log.txt", rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate, fileSizeLimitBytes: fileSizeLimitBytes,
                        retainedFileCountLimit: 31, rollOnFileSizeLimit: true, shared: true));

            // 根据条件判断是否启用 Loki 日志输出
            if (enableLokiLogging)
            {
                // 添加Label
                List<LokiLabel> labels = new List<LokiLabel> { new LokiLabel { Key = "App", Value = name } };

                loggerConfiguration = loggerConfiguration.WriteTo.GrafanaLoki("http://106.227.19.247:3100", labels);
            }

            return loggerConfiguration.CreateLogger();
        }
    }
}
