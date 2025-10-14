using Jay.NC.Workflow.WebApi.Common.Utils;
using Jay.NC.Workflow.WebApi.Storage.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.NC.Workflow.WebApi.Storage.EntityFrameworkCore
{
    public class WorkflowDbContextDesignTimeFactory : IDesignTimeDbContextFactory<WorkflowDbContext>
    {
        public WorkflowDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var optionsBuilder = new DbContextOptionsBuilder<WorkflowDbContext>();
            optionsBuilder.UseMySql(configuration["Db:WorkflowDb:ConnStr"], new MySqlServerVersion(new Version(8, 0, 43)));

            var httpContextUtility = new HttpContextUtility(new HttpContextAccessor());

            return new WorkflowDbContext(optionsBuilder.Options, httpContextUtility);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Jay.NC.Workflow.WebApi.Service"))
                .AddJsonFile("appsettings.Development.json", false);
            return builder.Build();
        }
    }
}
