using Jay.Workflow.WebApi.Common.Interface.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Orms.Dapper
{
    public interface IDapperProvider : IScopedDependency
    {
        /// <summary>
        /// 异步Sql查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<T> QueryAsync<T>(string sql, object param = null);

        /// <summary>
        /// 异步Sql查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<List<T>> QueryListAsync<T>(string sql, object param = null);

        /// <summary>
        /// 异步Sql更新
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="trans"></param>
        /// <param name="sourceConn"></param>
        /// <returns></returns>
        Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction trans = null, IDbConnection sourceConn = null);

        /// <summary>
        /// 异步执行事务
        /// 异常时回滚并返回错误信息
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        ValueTask<string> ExecuteTransactionAsync(Func<DbConnection, DbTransaction, Task> func);

        /// <summary>
        /// 异步执行事务
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        ValueTask<int> ExecuteTransactionAsync(Func<DbTransaction, Task<int>> func);
    }
}
