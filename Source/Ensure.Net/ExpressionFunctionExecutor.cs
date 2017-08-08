﻿#if Expressions_Supported
using System;
using System.Linq.Expressions;

namespace Vima.Ensure.Net
{
    internal class ExpressionFunctionExecutor
    {
        public static IEnsurable<T> ExecuteFunctionWithExpression<T>(Expression<Func<T>> valueExpression, Func<T, string, IEnsurable<T>> action)
        {
            if (valueExpression == null)
            {
                throw new ArgumentException("Expression cannot be null.");
            }

            ConstantExpression constantExpression = valueExpression.Body as ConstantExpression;
            if (constantExpression != null)
            {
                return action.Invoke((T)constantExpression.Value, "Variable");
            }

            MemberExpression memberExpression = valueExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("Expression must be of type MemberExpression.");
            }

            T value = (T)Expression.Lambda(memberExpression).Compile().DynamicInvoke();
            return action.Invoke(value, memberExpression.Member.Name);
        }
    }
}
#endif