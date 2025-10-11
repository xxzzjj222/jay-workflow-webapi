using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.NC.Workflow.WebApi.Common.Configurations.DbConfiguration
{
    public class DbConfigurationProvider:ConfigurationProvider
    {
        private readonly DbConfigurationOption _dbConfigurationOption;

        public DbConfigurationProvider(DbConfigurationOption dbConfigurationOption)
        {
            _dbConfigurationOption = dbConfigurationOption;
        }

        public override void Load()
        {
            ValidateOptionArgs();

            var condition = _dbConfigurationOption.ConfigType.HasValue ? $" and config_type={(int)_dbConfigurationOption.ConfigType}" : string.Empty;
            var commandText = $"select {_dbConfigurationOption.ConfigKeyField} as key,{_dbConfigurationOption.ConfigValueField} as value from {_dbConfigurationOption.TableName} where status=1{condition};";

            using var mysqlConnection = new MySqlConnection(_dbConfigurationOption.ConnStr);
            mysqlConnection.Open();
            using var mysqlCommand=new MySqlCommand(commandText,mysqlConnection);
            var dataReader=mysqlCommand.ExecuteReader();

            var dictionary=new Dictionary<string, string?>();
            while(dataReader.Read())
            {
                var key = dataReader["key"]?.ToString() ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(key) && !dictionary.ContainsKey(key))
                {
                    dictionary.Add(key, dataReader["value"]?.ToString());
                }
            }

            Data= dictionary;
        }

        private void ValidateOptionArgs()
        {
            if (string.IsNullOrWhiteSpace(_dbConfigurationOption.ConnStr))
            {
                throw new Exception("数据库连接字符串不能为空！");
            }

            if (string.IsNullOrWhiteSpace(_dbConfigurationOption.TableName))
            {
                throw new Exception("数据库配置所属表不能为空！");
            }

            if (string.IsNullOrWhiteSpace(_dbConfigurationOption.ConfigKeyField))
            {
                throw new Exception("数据库配置Key所属列不能为空！");
            }

            if (string.IsNullOrWhiteSpace(_dbConfigurationOption.ConfigValueField))
            {
                throw new Exception("数据库配置Value所属列不能为空！");
            }
        }
    }
}
