using Dapper;
using Jay.Workflow.WebApi.Dal.Interfaces;
using Jay.Workflow.WebApi.Dal.Utility;
using Jay.Workflow.WebApi.Storage.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Dal.Dals
{
    public class BaseDal<T>:IBaseDal<T> where T : class,new()
    {
        /// <summary>
        /// 数据库上下文对象
        /// </summary>
        protected WorkflowDbContext _dbContext;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        public BaseDal(WorkflowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity).ConfigureAwait(false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> AddAndSaveChanges(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity).ConfigureAwait(false);

            return await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// insert list
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task<bool> AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities).ConfigureAwait(false);
            return true;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return await Task.FromResult(true).ConfigureAwait(false);
        }

        /// <summary>
        /// update list
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task<bool> UpdateRangeAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            return await Task.FromResult(true).ConfigureAwait(false);
        }

        /// <summary>
        /// delete
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);

            return await Task.FromResult(true).ConfigureAwait(false);
        }

        /// <summary>
        /// delete list
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task<bool> RemoveRangeAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);

            return await Task.FromResult(true).ConfigureAwait(false);
        }

        /// <summary>
        ///  get  list
        /// </summary>
        /// <param name="predict"></param>
        /// <returns></returns>
        public async Task<List<T>> GetListAsyncAsNoTracking(Expression<Func<T, bool>> predict = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            if (null != predict)
            {
                query = query.Where(predict);
            }

            return await query.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// no AsNoTracking
        /// </summary>
        /// <param name="predict"></param>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> predict = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            if (null != predict)
            {
                query = query.Where(predict);
            }

            return await query.ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="predict"></param>
        /// <returns></returns>
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predict)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (null != predict)
            {
                query = query.Where(predict);
            }

            return await query.FirstOrDefaultAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="predict"></param>
        /// <returns></returns>
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predict)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (null != predict)
            {
                query = query.Where(predict);
            }

            return await query.AnyAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="predict"></param>
        /// <returns></returns>
        public async Task<T> FirstOrDefaultAsyncAsNoTracking(Expression<Func<T, bool>> predict)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (null != predict)
            {
                query = query.Where(predict);
            }

            return await query.AsNoTracking().FirstOrDefaultAsync().ConfigureAwait(false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="predict"></param>
        /// <returns></returns>
        public async Task<IQueryable<T>> GetIQueryableAsNoTracking(Expression<Func<T, bool>> predict = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (null != predict)
            {
                query = query.Where(predict);
            }

            return await Task.FromResult(query.AsNoTracking()).ConfigureAwait(false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="predict"></param>
        /// <returns></returns>
        public async Task<IQueryable<T>> GetIQueryable(Expression<Func<T, bool>> predict = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (null != predict)
            {
                query = query.Where(predict);
            }

            return await Task.FromResult(query.AsNoTracking()).ConfigureAwait(false);
        }

        /// <summary>
        /// 查询sql
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="sql"></param>
        /// <param name="ignorePropertyCase"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TModel>> QueryUseSqlAsync<TModel>(string sql, bool ignorePropertyCase = false, params object[] parameters) where TModel : class, new()
        {
            var conn = _dbContext.Database.GetDbConnection();
            try
            {
                conn.Open();
                using var command = conn.CreateCommand();

                command.CommandText = sql;
                command.Parameters.AddRange(parameters);
                var propts = typeof(TModel).GetProperties();
                var list = new List<TModel>();
                TModel model;
                object val;
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    model = new TModel();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        PropertyInfo propt = null;
                        if (ignorePropertyCase)
                        {
                            propt = propts.FirstOrDefault(r => r.Name?.ToLower() == reader.GetName(i)?.ToLower());
                        }
                        else
                        {
                            propt = propts.FirstOrDefault(r => r.Name == reader.GetName(i));
                        }

                        val = reader[i];
                        if (null != propt)
                        {
                            if (val != DBNull.Value)
                            {
                                propt.SetValue(model, val);
                            }
                        }
                    }

                    list.Add(model);
                }

                return await Task.FromResult(list.Count > 0 ? list.AsEnumerable() : null).ConfigureAwait(false);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 使用sql查 第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<string> QueryUseSqlFirstOrDefaultAsync(string sql, params object[] parameters)
        {
            var conn = _dbContext.Database.GetDbConnection();
            try
            {
                conn.Open();
                using var command = conn.CreateCommand();

                command.CommandText = sql;
                command.Parameters.AddRange(parameters);
                object val;
                using var reader = command.ExecuteReader();

                reader.Read();
                val = reader[0];
                if (val != DBNull.Value)
                {
                    return await Task.FromResult(val.ToString()).ConfigureAwait(false);
                }

                return await Task.FromResult(string.Empty).ConfigureAwait(false);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="predict"></param>
        /// <returns></returns>
        public async Task<int> CountAsyncAsNoTracking(Expression<Func<T, bool>> predict)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (null != predict)
            {
                query = query.Where(predict);
            }

            return await query.AsNoTracking().CountAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="predict"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public async Task<(IEnumerable<T> result, int totalCount)> GetPagedListAsync(Expression<Func<T, bool>> predict, int pageSize, int pageIndex, IOrderByClause<T>[] orderBy = null)
        {
            var countQuery = _dbContext.Set<T>().AsQueryable();
            if (null != predict)
            {
                countQuery = countQuery.Where(predict);
            }

            var totalCount = await countQuery.AsNoTracking().CountAsync().ConfigureAwait(false);
            IEnumerable<T> result = null;
            if (0 < totalCount)
            {
                var query = _dbContext.Set<T>().AsQueryable();
                if (null != predict)
                {
                    query = query.Where(predict);
                }

                var isFirst = true;
                orderBy?.ToList().ForEach(r =>
                {
                    query = r.ApplySort(query, isFirst);
                    isFirst = false;
                });
                query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

                result = query.AsNoTracking().AsEnumerable();
            }

            return await Task.FromResult((result, totalCount)).ConfigureAwait(false);
        }

        /// <summary>
        /// 执行sql查询命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> ExecuteQueryAsync(string sql, IDictionary<string, object> parameters = null)
        {
            return await _dbContext.Database.GetDbConnection().QueryAsync<T>(sql, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// 执行sql命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual async Task<int> ExecuteCommandAsync(string sql, IDictionary<string, object> parameters = null)
        {
            return await _dbContext.Database.GetDbConnection().ExecuteAsync(sql, parameters).ConfigureAwait(false);
        }
    }
}
