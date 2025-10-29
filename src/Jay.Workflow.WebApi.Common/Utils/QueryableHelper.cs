using Jay.Workflow.WebApi.Common.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Utils
{
    public static class QueryableHelper
    {
        public static IQueryable<T1> QueryOrderBy<T,T1>(T dto,IQueryable<T1> query) where T : PagingRequest
        {
            if(dto.Sorts==null || !dto.Sorts.Any())
            {
                return query;
            }

            var parameter = Expression.Parameter(typeof(T1), "o");
            for(int i=0; i<dto.Sorts.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(dto.Sorts[i].SortBy)) continue;

                var property = typeof(T1).GetProperty(dto.Sorts[i].SortBy, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (property != null)
                {
                    var propertyAccess=Expression.MakeMemberAccess(parameter, property);
                    var orderbyExp = Expression.Lambda(propertyAccess, parameter);
                    string orderName = "";
                    if (i > 0)
                    {
                        orderName = dto.Sorts[i].IsDesc ? "ThenByDescending" : "ThenBy";
                    }
                    else
                    {
                        orderName = dto.Sorts[i].IsDesc ? "OrderByDescending" : "OrderBy";
                    }
                    MethodCallExpression exp = Expression.Call(typeof(Queryable), orderName, new Type[] { typeof(T1), property.PropertyType }, query.Expression, Expression.Quote(orderbyExp));
                    query=query.Provider.CreateQuery<T1>(exp);
                }
            }

            return query;
        }
    }
}
