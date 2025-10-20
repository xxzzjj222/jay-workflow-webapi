using Jay.Workflow.WebApi.Common.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Orms.EFCore
{
    /// <summary>
    /// EFCore日志处理程序
    /// </summary>
    public class EFCoreLogger : ILogger
    {
        private readonly string _categoryName;
        private readonly IHttpContextUtility _httpContextUtility;
        private readonly ILogger<EFCoreLogger> _logger;

        public EFCoreLogger(string categoryName, IServiceProvider serviceProvider)
        {
            _categoryName = categoryName;
            _httpContextUtility = serviceProvider.GetRequiredService<IHttpContextUtility>();
            _logger = serviceProvider.GetRequiredService<ILogger<EFCoreLogger>>();
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (formatter == null) return;

            var logContent = formatter(state, exception);

            if (logLevel == LogLevel.Information)
            {
                if (_categoryName == DbLoggerCategory.Database.Command.Name)
                {
                    var traceIdentifier = _httpContextUtility.GetTraceIdentifier();
                    if (!string.IsNullOrWhiteSpace(traceIdentifier))
                    {
                        _logger.LogInformation("{logContent}. TraceIdentifier: {traceIdentifier}-PG.", logContent, traceIdentifier);
                        return;
                    }
                }
                _logger.LogInformation(logContent);
            }
        }
    }
}
