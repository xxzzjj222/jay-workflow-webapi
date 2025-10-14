using Jay.NC.Workflow.WebApi.Bll.Interfaces.Migration;
using Jay.NC.Workflow.WebApi.Common.Orms.Dapper;
using Jay.NC.Workflow.WebApi.Model.Dtos.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.NC.Workflow.WebApi.Bll.Services.Migration
{
    public class MigrateService : IMigrateService
    {
        private readonly string _databaseName;
        private readonly IDapperProvider _dapperProvider;
        private readonly ILogger<MigrateService> _logger;

        public MigrateService(IConfiguration configuration,
            IDapperProvider dapperProvider,
            ILogger<MigrateService> logger)
        {
            _databaseName = configuration["Db:WorkflowDb:ConnStr"].Split(";").First(x => x.ToLower().Contains("database")).Split("=")[1];
            _dapperProvider = dapperProvider;
            _logger = logger;
        }

        public async Task<string> PretreatHistoryVersionAsync()
        {
            try
            {
                //检测schema、table是否存在，不存在则初始化
                var sql = $@"select tab1.SCHEMA_NAME as schemaName,tab2.TABLE_NAME as tableName
                        from information_schema.SCHEMATA tab1
                        left join (select * from information_schema.TABLES where TABLE_NAME='app_version') tab2
                        on tab1.SCHEMA_NAME=tab2.TABLE_SCHEMA
                        where tab1.SCHEMA_NAME='{_databaseName}';";
                var pretreatTableInfo = await _dapperProvider.QueryAsync<dynamic>(sql).ConfigureAwait(false);

                if (string.IsNullOrWhiteSpace(pretreatTableInfo?.schemaName))
                {
                    await CreateSchemaAsync().ConfigureAwait(false);
                    await CreateAppVersionTableAsync().ConfigureAwait(false);
                    return "v0.0.0";
                }
                if (string.IsNullOrWhiteSpace(pretreatTableInfo?.tableName))
                {
                    await CreateAppVersionTableAsync().ConfigureAwait(false);
                }
                else
                {
                    sql = $"SELECT version_name FROM {_databaseName}.app_version WHERE version_name != '' AND version_name IS NOT NULL ORDER BY id DESC LIMIT 1;";
                    var dbVersion = await _dapperProvider.QueryAsync<string>(sql).ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(dbVersion)) return dbVersion;
                }

                //处理版本号
                var versionInfos = await GetPretreatVersionInfosAsync().ConfigureAwait(false);
                if (versionInfos == null) return string.Empty;

                var currentDbVersion = await ExecuteHistoryVersionPretreatAsync(versionInfos).ConfigureAwait(false);
                return currentDbVersion;
            }
            catch (Exception ex)
            {
                _logger.LogError("程序初始化预处理失败，ex：{ex}", ex.ToString());
                throw;
            }
        }

        public async ValueTask InitAppAsync(string currentDbVersion)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(currentDbVersion)) return;

                // 获取所有待升级的版本信息
                var versionInfos = await GetUpgradeVersionInfosAsync(currentDbVersion).ConfigureAwait(false);
                if (!versionInfos?.Any() ?? true) return;

                //循环待升级的版本列表执行版本升级
                await ExecuteVersionUpgradeAsync(currentDbVersion, versionInfos).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                _logger.LogError("程序初始化升级失败，ex：{ex}", ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// 创建schema
        /// </summary>
        /// <returns></returns>
        private async Task CreateSchemaAsync()
        {
            var sql = $"CREATE SCHEMA IF NOT EXISTS {_databaseName};";
            await _dapperProvider.ExecuteAsync(sql).ConfigureAwait(false);
        }

        /// <summary>
        /// 创建app_version、app_upgrade_log表
        /// </summary>
        /// <returns></returns>
        private async Task CreateAppVersionTableAsync()
        {
            var sql = $@"CREATE TABLE IF NOT EXISTS {_databaseName}.app_version
                         (
	                         id int auto_increment
			                         PRIMARY KEY,
	                         version_name varchar(100) NOT NULL,
	                         remark varchar(100) DEFAULT '',
	                         creator integer NOT NULL,
	                         created_time timestamp NOT NULL,
	                         modifier integer NOT NULL,
	                         modified_time timestamp NOT NULL
                         );
                         CREATE TABLE IF NOT EXISTS {_databaseName}.app_upgrade_log
                         (
	                         id int auto_increment
			                         PRIMARY KEY,
	                         version_name char(36) NOT NULL,
	                         batch_id char(36) NOT NULL,
	                         remark varchar(100) DEFAULT '',
	                         creator integer NOT NULL,
	                         created_time timestamp NOT NULL,
	                         modifier integer NOT NULL,
	                         modified_time timestamp NOT NULL
                         );";
            await _dapperProvider.ExecuteAsync(sql).ConfigureAwait(false);
        }

        private async ValueTask<List<MigrateVersionInfoDto>> GetPretreatVersionInfosAsync()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Migrations", "Pretreat");
            if (!Directory.Exists(path)) return null;

            var versionInfos = new DirectoryInfo(path).GetDirectories()
                                                    .Where(x => x.Name.StartsWith("v"))
                                                    .Select(x => new MigrateVersionInfoDto
                                                    {
                                                        Version = x.Name,
                                                        VersionNum = ConvertVersionToNum(x.Name),
                                                        VersionFilePath = x.FullName
                                                    })
                                                    .OrderByDescending(x => x.VersionNum)
                                                    .ToList();
            return await Task.FromResult(versionInfos).ConfigureAwait(false); 
        }

        private int ConvertVersionToNum(string version)
        {
            try
            {
                return Convert.ToInt32(version.ToLower().Replace("v", string.Empty).Replace(".", string.Empty));
            }
            catch(Exception ex)
            {
                _logger.LogError("程序升级迁移脚本所属版本号异常，ex: {ex}.", ex.ToString());
                return 0;
            }
        }

        private async Task<string> ExecuteHistoryVersionPretreatAsync(List<MigrateVersionInfoDto> versionInfos)
        {
            //版本升级批次号
            var batchNo=Guid.NewGuid();
            var currentDbVersion = string.Empty;

            foreach (var item in versionInfos)
            {
                var versionSqlDic = await GetVersionSqlDicFromFileAsync(item.VersionFilePath).ConfigureAwait(false);
                if (!versionSqlDic.Any())
                {
                    _logger.LogInformation("历史版本信息预处理批次号为：{batchNo}，当前{version}版本无需要执行的脚本文件！", batchNo, item.Version);
                    continue;
                }

                var count = 0;
                try
                {
                    var versionSqlKv = versionSqlDic.First();
                    if (versionSqlKv.Key.Contains("table"))
                    {
                        count = await _dapperProvider.QueryAsync<int>(string.Format(versionSqlKv.Value, _databaseName)).ConfigureAwait(false);
                    }
                    else
                    {
                        count = await _dapperProvider.QueryAsync<int>(versionSqlKv.Value).ConfigureAwait(false);
                    }
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "版本号获取失败！");
                }

                if (count == 0)
                {
                    _logger.LogInformation("历史版本信息预处理批次号为：{batchNo}，{version}版本校验结果为不存在！", batchNo, item.Version);
                    continue;
                }

                var remark = "服务部署迁移历史版本信息预处理";

                var errorMsg = await _dapperProvider.ExecuteTransactionAsync(async (conn, trans) =>
                {
                    await CreateAppVersionAsync(item.Version, remark, trans, conn);
                    await CreateAppUpgradeLogAsync(item.Version, batchNo, remark, trans, conn);
                }).ConfigureAwait(false);

                if (!string.IsNullOrWhiteSpace(errorMsg))
                {
                    _logger.LogError("历史版本信息预处理批次号为：{batchNo}，执行{version}版本预处理操作失败，errorMsg：{errorMsg}！", batchNo, item.Version, errorMsg);
                    throw new Exception($"{item.Version}版本预处理操作失败，请查看错误日志！");
                }

                currentDbVersion = item.Version;
                _logger.LogInformation("历史版本信息预处理批次号为：{batchNo}，{version}版本预处理操作完成！", batchNo, item.Version);
            }

            _logger.LogInformation("历史版本信息预处理批次号为：{batchNo}，整体预处理操作完成，当前数据库版本为：{currentDbVersion}！", batchNo, currentDbVersion);
            return currentDbVersion;
        }

        private async Task<Dictionary<string,string>> GetVersionSqlDicFromFileAsync(string filePath)
        {
            var versionSqlDic=new Dictionary<string,string>(); 
            var fileInfos=new DirectoryInfo(filePath).GetFiles();

            foreach (var fileInfo in fileInfos)
            {
                using var fileStream = fileInfo.OpenRead();
                using var reader=new StreamReader(fileStream);
                var content = await reader.ReadToEndAsync().ConfigureAwait(false);
                if (!string.IsNullOrWhiteSpace(content))
                {
                    versionSqlDic.TryAdd(fileInfo.Name.Split(".").First(),content);
                }
            }

            return versionSqlDic.OrderBy(x => x.Key).ToDictionary();
        }

        /// <summary>
        /// 创建版本记录信息
        /// </summary>
        /// <param name="version"></param>
        /// <param name="remark"></param>
        /// <param name="trans"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        private async Task CreateAppVersionAsync(string version,string remark,IDbTransaction trans=null,IDbConnection conn = null)
        {
            var sql = $@"INSERT INTO app_version(version_name, remark, creator, created_time, modifier, modified_time)
                         VALUES ('{version}', '{remark}', 999, now(), 999, now());";

            await _dapperProvider.ExecuteAsync(sql, trans: trans, sourceConn: conn).ConfigureAwait(false);
        }

        /// <summary>
        /// 创建版本升级记录
        /// </summary>
        /// <param name="version"></param>
        /// <param name="batchId"></param>
        /// <param name="remark"></param>
        /// <param name="trans"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        private async Task CreateAppUpgradeLogAsync(string version, Guid batchId, string remark, IDbTransaction trans = null, IDbConnection conn = null)
        {
            var sql = $@"INSERT INTO app_upgrade_log(version_name, batch_id, remark, creator, created_time, modifier, modified_time)
                         VALUES ('{version}', '{batchId}', '{remark}', 999, now(), 999, now());";

            await _dapperProvider.ExecuteAsync(sql, trans: trans, sourceConn: conn).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取升级版本信息
        /// </summary>
        /// <param name="currentDbVersion"></param>
        /// <returns></returns>
        private async ValueTask<List<MigrateVersionInfoDto>> GetUpgradeVersionInfosAsync(string currentDbVersion)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Migrations", "Upgrade");
            if (!Directory.Exists(path)) return null;

            var versionInfos = new DirectoryInfo(path).GetDirectories()
                                                    .Where(x => x.Name.StartsWith("v"))
                                                    .Select(x => new MigrateVersionInfoDto
                                                    {
                                                        Version = x.Name,
                                                        VersionNum = ConvertVersionToNum(x.Name),
                                                        VersionFilePath = x.FullName
                                                    })
                                                    .Where(x => x.VersionNum > ConvertVersionToNum(currentDbVersion))
                                                    .OrderBy(x => x.VersionNum)
                                                    .ToList();

            return await Task.FromResult(versionInfos).ConfigureAwait(false);
        }

        /// <summary>
        /// 执行版本升级
        /// </summary>
        /// <param name="currentVersion"></param>
        /// <param name="upgradeVersionInfos"></param>
        /// <returns></returns>
        private async Task ExecuteVersionUpgradeAsync(string currentVersion, List<MigrateVersionInfoDto> upgradeVersionInfos)
        {
            var batchNo = Guid.NewGuid();

            _logger.LogInformation(@"本次版本升级批次号为：{batchNo}，当前版本为：{currentVersion}，升级内容为从版本{nextVersion}升级到{newestVersion}，共计{count}个版本：{versions}，升级开始！",
                                     batchNo, currentVersion, upgradeVersionInfos.First().Version, upgradeVersionInfos.Last().Version, upgradeVersionInfos.Count, string.Join("、", upgradeVersionInfos.Select(x => x.Version)));

            foreach (var item in upgradeVersionInfos)
            {
                var versionSqlDic = await GetVersionSqlDicFromFileAsync(item.VersionFilePath).ConfigureAwait(false);
                if (!versionSqlDic.Any())
                {
                    _logger.LogInformation("版本升级批次号为：{batchNo}，当前{version}版本无需要执行的脚本文件！", batchNo, item.Version);
                    await CreateAppVersionAsync(item.Version, "").ConfigureAwait(false);
                    await CreateAppUpgradeLogAsync(item.Version, batchNo, "").ConfigureAwait(false);
                    continue;
                }

                _logger.LogInformation("版本升级批次号为：{batchNo}，开始执行{version}版本的升级！", batchNo, item.Version);

                var errorMsg = await _dapperProvider.ExecuteTransactionAsync(async (conn, trans) =>
                {
                    foreach (var versionSqlKv in versionSqlDic)
                    {
                        _logger.LogInformation("开始执行{version}版本的{fileName}脚本！", item.Version, versionSqlKv.Key);
                        await _dapperProvider.ExecuteAsync(versionSqlKv.Value, trans: trans, sourceConn: conn).ConfigureAwait(false);
                    }
                    await CreateAppVersionAsync(item.Version, "", trans, conn).ConfigureAwait(false);
                }).ConfigureAwait(false);

                if(!string.IsNullOrWhiteSpace(errorMsg))
                {
                    _logger.LogError("版本升级批次号为：{batchNo}，执行{version}版本升级失败，errorMsg：{errorMsg}！", batchNo, item.Version, errorMsg);
                    throw new Exception($"{item.Version}版本升级失败，请查看错误日志！");
                }

                _logger.LogInformation("版本升级批次号为：{batchNo}，结束执行{version}版本的升级！", batchNo, item.Version);
            }

            _logger.LogInformation("本次版本升级批次号为：{batchNo}，整体升级完成！", batchNo);
        }
    }
}
