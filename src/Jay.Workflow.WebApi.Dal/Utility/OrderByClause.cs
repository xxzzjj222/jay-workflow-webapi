using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Dal.Utility
{
    /// <summary>
    /// 排序
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public class OrderByClause<T, TProperty> : IOrderByClause<T> where T : class, new()
    {
        /// <summary>
        /// 排序类型
        /// </summary>
        protected OrderBySortEnum _orderBySort;

        /// <summary>
        /// 排序字段
        /// </summary>
        protected Expression<Func<T, TProperty>> _orderBy;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="orderBySort"></param>
        public OrderByClause(Expression<Func<T, TProperty>> orderBy, OrderBySortEnum orderBySort)
        {
            _orderBySort = orderBySort;
            _orderBy = orderBy;
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="query"></param>
        /// <param name="isFirst"></param>
        /// <returns></returns>
        public IOrderedQueryable<T> ApplySort(IQueryable<T> query, bool isFirst)
        {
            if (isFirst)
            {
                if (_orderBySort == OrderBySortEnum.Asc)
                {
                    return query.OrderBy(_orderBy);
                }
                else
                {
                    return query.OrderByDescending(_orderBy);
                }
            }
            else
            {
                if (_orderBySort == OrderBySortEnum.Asc)
                {
                    return ((IOrderedQueryable<T>)query).ThenBy(_orderBy);
                }
                else
                {
                    return ((IOrderedQueryable<T>)query).ThenByDescending(_orderBy);
                }
            }
        }
    }

    /// <summary>
    /// 排序类型
    /// </summary>
    public enum OrderBySortEnum
    {
        /// <summary>
        /// 升序
        /// </summary>
        Asc = 0,

        /// <summary>
        /// 降序
        /// </summary>
        Desc = 1
    }
}
