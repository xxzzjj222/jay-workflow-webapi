
using Jay.Workflow.WebApi.Bll.Interfaces.Migration;

namespace Jay.Workflow.WebApi.Service.Filters
{
    public class MigrateStartupFilter : IStartupFilter
    {
        private readonly ILogger<MigrateStartupFilter> _logger;
        private readonly IServiceProvider _serviceProvider;

        public MigrateStartupFilter(ILogger<MigrateStartupFilter> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                Execute().Wait();

                next(builder);
            };
        }

        private async Task Execute()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var migrateService = scope.ServiceProvider.GetRequiredService<IMigrateService>();

                // 版本升级前数据预处理
                var currentDbVersion = await migrateService.PretreatHistoryVersionAsync().ConfigureAwait(false);

                // 新版本升级
                await migrateService.InitAppAsync(currentDbVersion).ConfigureAwait(false);
            }
        }
    }
}
