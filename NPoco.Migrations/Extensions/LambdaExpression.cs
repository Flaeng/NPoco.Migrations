using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace NPoco.Migrations.Extensions
{
    public static class LambdaExpressionExtensions
    {

        public static string GetMemberName(this LambdaExpression expression) => expression.GetMember().Name;

        public static MemberInfo GetMember(this LambdaExpression expression)
        {
            if (expression.Body is MemberExpression memberExpression)
                return memberExpression.Member;

            throw new NotSupportedException("Failed to get member from expression");
        }

    }
}
