using Jay.Workflow.WebApi.Common.Interface.Base;
using Jay.Workflow.WebApi.Dal.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Dal.Interfaces
{
    public interface IBaseDal<T>:IScopedDependency where T:class,new()
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(T entity);

        /// <summary>
        /// 添加并保存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> AddAndSaveChanges(T entity);

        /// <summary>
        /// 添加一组数据
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<bool> AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// 更新数据一组数据
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>

        Task<bool> UpdateRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(T entity);

        /// <summary>
        /// 删除一组数据
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<bool> RemoveRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// 获取列表 AsNoTracking
        /// </summary>
        /// <param name="predict"></param>
        /// <returns>List</returns>
        Task<List<T>> GetListAsyncAsNoTracking(Expression<Func<T, bool>> predict = null);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="predict"></param>
        /// <returns>List</returns>
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> predict);

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="predict"></param>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predict);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="predict"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> predict);

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="predict"></param>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsyncAsNoTracking(Expression<Func<T, bool>> predict);

        /// <summary>
        /// 获取列表 AsNoTracking
        /// </summary>
        /// <param name="predict"></param>
        /// <returns>IQueryable</returns>
        Task<IQueryable<T>> GetIQueryableAsNoTracking(Expression<Func<T, bool>> predict = null);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="predict"></param>
        /// <returns>IQueryable</returns>
        Task<IQueryable<T>> GetIQueryable(Expression<Func<T, bool>> predict);

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="predict"></param>
        /// <returns>IQueryable</returns>
        Task<int> CountAsyncAsNoTracking(Expression<Func<T, bool>> predict);

        /// <summary>
        /// 根据sql 查询
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="sql"></param>
        /// <param name="ignorePropertyCase"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<IEnumerable<TModel>> QueryUseSqlAsync<TModel>(string sql, bool ignorePropertyCase = false, params object[] parameters) where TModel : class, new();

        /// <summary>
        /// 使用sql查 第一行第一列的值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<string> QueryUseSqlFirstOrDefaultAsync(string sql, params object[] parameters);

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="predict"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        Task<(IEnumerable<T> result, int totalCount)> GetPagedListAsync(Expression<Func<T, bool>> predict, int pageSize, int pageIndex, IOrderByClause<T>[] orderBy = null);

        /// <summary>
        /// 执行sql查询命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> ExecuteQueryAsync(string sql, IDictionary<string, object> parameters = null);

        /// <summary>
        /// 执行sql命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<int> ExecuteCommandAsync(string sql, IDictionary<string, object> parameters = null);
    }
}
