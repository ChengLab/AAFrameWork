using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AA.Dapper.Util
{
   public static class DynamicWhereExpression
    {

        public static Expression<Func<T, bool>> Init<T>() { return f => true; }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            if (left == null)
            {
                return right;
            }

            var invokeExpression = Expression.Invoke(right, left.Parameters.Cast<Expression>());
            return (Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left.Body, invokeExpression), left.Parameters));
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            if (left == null)
            {
                return right;
            }

            var invokeExpression = Expression.Invoke(right, left.Parameters.Cast<Expression>());
            return (Expression.Lambda<Func<T, bool>>(Expression.OrElse(left.Body, invokeExpression), left.Parameters));
        }
    }
}
