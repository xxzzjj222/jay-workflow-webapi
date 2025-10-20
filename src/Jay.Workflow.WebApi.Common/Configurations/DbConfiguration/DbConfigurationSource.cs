using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Configurations.DbConfiguration
{
    public class DbConfigurationSource : IConfigurationSource
    {
        private readonly DbConfigurationOption _dbConfigurationOption;

        public DbConfigurationSource(DbConfigurationOption dbConfigurationOption)
        {
            _dbConfigurationOption = dbConfigurationOption;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new DbConfigurationProvider(_dbConfigurationOption);
        }
    }
}
