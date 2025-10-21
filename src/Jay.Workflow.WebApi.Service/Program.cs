using Jay.Workflow.WebApi.Common.Configurations.DbConfiguration;
using Jay.Workflow.WebApi.Common.DynamicInterceptor;
using Jay.Workflow.WebApi.Service.Extensions;
using Serilog;

namespace Jay.Workflow.WebApi.Service
{
    /// <summary>
    /// ���
    /// </summary>
    public class Program
    {
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// ����Host
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configurationBuilder =>
                {
                    configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false, reloadOnChange: true)
                    .AddDbConfiguration()
                    .AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<StartUp>();
                })
                .ConfigureDynamicInterceptor()
                .UseSerilog();
    }
}