using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Orms.EFCore
{
    /// <summary>
    /// EFCore日志供应商
    /// </summary>
    public class EFCoreLoggerProvider : ILoggerProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public EFCoreLoggerProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new EFCoreLogger(categoryName,_serviceProvider);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        { }
    }
}
