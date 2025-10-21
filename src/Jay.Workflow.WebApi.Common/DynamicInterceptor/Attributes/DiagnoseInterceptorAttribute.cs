using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.DynamicInterceptor.Attributes
{
    /// <summary>
    /// 诊断拦截器特性
    /// </summary>
    public class DiagnoseInterceptorAttribute : AbstractInterceptorAttribute
    {
        private bool _ignoreArgs;

        public DiagnoseInterceptorAttribute(bool ignoreArgs = false) => _ignoreArgs = ignoreArgs;

        [FromServiceContext]
        public ILogger<DiagnoseInterceptorAttribute> Logger { get; set; }

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            if (context == null || next == null) return;

            var serviceName = $"{context.Implementation}.{context.ImplementationMethod.Name}";

            var args = _ignoreArgs ? "ArgsIgnore" : JsonConvert.SerializeObject(context.Parameters);

            var stopwatch = new Stopwatch();

            try
            {
                stopwatch.Start();

                Logger.LogDebug("Executing DiagnoseInterceptor with {serviceName} Begin, Args: {args}.", serviceName, args);

                await next(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogError("Executing DiagnoseInterceptor with {serviceName} Error, Args: {args}，Ex: {ex}.", serviceName, args, ex.ToString());

                throw;
            }
            finally
            {
                stopwatch.Stop();

                Logger.LogInformation("Executed DiagnoseInterceptor ({elapsedTime}ms) with {serviceName} Args: {args}.", stopwatch.ElapsedMilliseconds, serviceName, args);
            }
        }
    }
}
