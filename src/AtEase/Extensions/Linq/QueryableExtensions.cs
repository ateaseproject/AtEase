﻿using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace AtEase.Extensions.Linq
{
    public static class QueryableExtensions
    {
        [DebuggerStepThrough]
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)

        {
            return (IQueryable<T>) OrderBy((IQueryable) source, propertyName);
        }

        [DebuggerStepThrough]
        public static IQueryable OrderBy(this IQueryable source, string propertyName)

        {
            var x = Expression.Parameter(source.ElementType, "x");

            var selector = Expression.Lambda(Expression.PropertyOrField(x, propertyName), x);

            return source.Provider.CreateQuery(
                Expression.Call(typeof(Queryable), "OrderBy",
                    new[] {source.ElementType, selector.Body.Type},
                    source.Expression, selector
                ));
        }

        [DebuggerStepThrough]
        public static IQueryable<T> OrderDescendingBy<T>(this IQueryable<T> source, string propertyName)

        {
            return (IQueryable<T>) OrderDescendingBy((IQueryable) source, propertyName);
        }

        [DebuggerStepThrough]
        public static IQueryable OrderDescendingBy(this IQueryable source, string propertyName)

        {
            var x = Expression.Parameter(source.ElementType, "x");

            var selector = Expression.Lambda(Expression.PropertyOrField(x, propertyName), x);

            return source.Provider.CreateQuery(
                Expression.Call(typeof(Queryable), "OrderByDescending",
                    new[] {source.ElementType, selector.Body.Type},
                    source.Expression, selector
                ));
        }
    }
}