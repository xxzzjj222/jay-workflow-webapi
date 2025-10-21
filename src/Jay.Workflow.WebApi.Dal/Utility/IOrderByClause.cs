using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Dal.Utility
{
    /// <summary>
    /// 排序
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IOrderByClause<T> where T : class, new()
    {
        /// <summary>
        /// 执行排序
        /// </summary>
        /// <param name="query"></param>
        /// <param name="isFirst"></param>
        /// <returns></returns>
        IOrderedQueryable<T> ApplySort(IQueryable<T> query, bool isFirst);
    }
}
