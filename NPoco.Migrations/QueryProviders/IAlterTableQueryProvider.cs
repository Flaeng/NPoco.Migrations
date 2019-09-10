using NPoco.Migrations.Extensions;
using NPoco.Migrations.QueryProviders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;

namespace NPoco.Migrations.QueryProviders
{
    public interface IAlterTableQueryProvider<T> : IAlterTableQueryProvider
    {
        IAlterTableColumnQueryProvider<T> AddColumn<TMember>(Expression<Func<T, TMember>> memberExpression);
    }
    public interface IAlterTableQueryProvider : IQueryProvider
    {
        IAlterTableColumnQueryProvider AddColumn(ColumnMigratorInfo column);
    }
}

namespace NPoco.Migrations
{
    public static class IAlterTableQueryProviderExtensions
    {
        public static IAlterTableColumnQueryProvider AddColumn(this IAlterTableQueryProvider provider, string columnName, Type type, string typeParameter = null, bool allowNull = true)
        {
            return provider.AddColumn(new ColumnMigratorInfo(columnName, type) { DbTypeParameter = typeParameter, AllowNull = allowNull });
        }

        public static IAlterTableColumnQueryProvider AddColumn<T>(this IAlterTableQueryProvider provider, Expression<Func<T, object>> expression)
        {
            var member = expression.GetMember();
            return provider.AddColumn(ColumnMigratorInfo.FromMemberInfo(member));
        }
    }
}