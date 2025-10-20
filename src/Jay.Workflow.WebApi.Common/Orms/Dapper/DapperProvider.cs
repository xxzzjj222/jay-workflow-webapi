using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Orms.Dapper
{
    public class DapperProvider : IDapperProvider
    {
        private readonly string _connectionString;

        public DapperProvider(IConfiguration configuration)
        {
            _connectionString = configuration["Db:WorkflowDb:ConnStr"];
        }

        /// <summary>
        /// 异步Sql查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<T> QueryAsync<T>(string sql, object param = null)
        {
            using var conn = new MySqlConnection(_connectionString);

            return await conn.QueryFirstOrDefaultAsync<T>(sql, param).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步Sql查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<List<T>> QueryListAsync<T>(string sql, object param = null)
        {
            using var conn = new MySqlConnection(_connectionString);

            var results = await conn.QueryAsync<T>(sql, param).ConfigureAwait(false);

            return results?.ToList() ?? new List<T>();
        }

        /// <summary>
        /// 异步Sql更新
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="trans"></param>
        /// <param name="sourceConn"></param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction trans = null, IDbConnection sourceConn = null)
        {
            if (sourceConn != null)
            {
                return await sourceConn.ExecuteAsync(sql, param, trans).ConfigureAwait(false);
            }

            using var conn = new MySqlConnection(_connectionString);

            return await conn.ExecuteAsync(sql, param, trans).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步执行事务
        /// 异常时回滚并返回错误信息
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public async ValueTask<string> ExecuteTransactionAsync(Func<DbConnection, DbTransaction, Task> func)
        {
            var errorMsg = string.Empty;

            if (func == null) return errorMsg;

            using var conn = new MySqlConnection(_connectionString);

            await conn.OpenAsync().ConfigureAwait(false);

            using var trans = await conn.BeginTransactionAsync().ConfigureAwait(false);

            try
            {
                await func(conn, trans).ConfigureAwait(false);

                await trans.CommitAsync().ConfigureAwait(false);
            }
            catch (System.Exception ex)
            {
                //_logger.LogError();

                errorMsg = ex.ToString();

                await trans.RollbackAsync().ConfigureAwait(false);
            }

            return errorMsg;
        }

        /// <summary>
        /// 异步执行事务
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public async ValueTask<int> ExecuteTransactionAsync(Func<DbTransaction, Task<int>> func)
        {
            var result = 0;

            if (func == null) return result;

            using var conn = new MySqlConnection(_connectionString);

            using var trans = await conn.BeginTransactionAsync().ConfigureAwait(false);

            try
            {
                result += await func(trans).ConfigureAwait(false);

                await trans.CommitAsync().ConfigureAwait(false);
            }
            catch (System.Exception)
            {
                result = 0;

                await trans.RollbackAsync().ConfigureAwait(false);
            }

            return result;
        }
    }
}
