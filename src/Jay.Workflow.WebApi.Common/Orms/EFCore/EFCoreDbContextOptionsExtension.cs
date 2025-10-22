using Jay.Workflow.WebApi.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Orms.EFCore
{
    /// <summary>
    /// 数据库上下文配置扩展类
    /// </summary>
    public static class EFCoreDbContextOptionsExtension
    {
        public static DbContextOptionsBuilder UseLoggerFactory(this DbContextOptionsBuilder optionsBuilder,IServiceProvider serviceProvider) 
        {
            if (optionsBuilder == null)
            {
                throw new InternalServerException(nameof(optionsBuilder));
            }
            var efcoreLoggerFactory= efcoreLoggerFactoryFunc(serviceProvider);
            return optionsBuilder.UseLoggerFactory(efcoreLoggerFactory);
        }

        private static readonly Func<IServiceProvider, LoggerFactory> efcoreLoggerFactoryFunc = serviceProvider => new LoggerFactory(new[] { new EFCoreLoggerProvider(serviceProvider) });
    }
}
