using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Utils
{
    /// <summary>
    /// 集合分页工具类
    /// </summary>
    public static class PagingUtillity
    {
        /// <summary>
        /// 取Queryable分页数据
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<List<TSource>> PagedListAsync<TSource>(this IQueryable<TSource> source, int pageIndex, int pageSize)
            => await source.Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize)
                           .ToListAsync();

        /// <summary>
        /// 取集合分页数据
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> PagedEnumerable<TSource>(this IEnumerable<TSource> source, int pageIndex, int pageSize)
            => source.Skip((pageIndex - 1) * pageSize)
                     .Take(pageSize);

        /// <summary>
        /// 取集合分页数据
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static List<TSource> PagedList<TSource>(this IEnumerable<TSource> source, int pageIndex, int pageSize)
            => source.PagedEnumerable(pageIndex, pageSize)
                     .ToList();
    }
}
