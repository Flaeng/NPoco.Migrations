using NPoco.Migrations.Extensions;
using NPoco.Migrations.QueryProviders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;

namespace NPoco.Migrations.QueryProviders
{
    public interface ICreateTableQueryProvider<T> : ICreateTableQueryProvider
    {
        ICreateTableColumnQueryProvider<T> AddColumn<TMember>(Expression<Func<T, TMember>> memberExpression);
    }
    public interface ICreateTableQueryProvider : IQueryProvider
    {
        ICreateTableColumnQueryProvider AddColumn(ColumnMigratorInfo column);
    }
}
namespace NPoco.Migrations
{
    public static class ICreateTableQueryProviderExtensions
    {
        public static ICreateTableColumnQueryProvider AddColumn(this ICreateTableQueryProvider provider, string columnName, Type type, string typeParameter = null, bool allowNull = false)
        {
            return provider.AddColumn(new ColumnMigratorInfo(columnName, type) { DbTypeParameter = typeParameter, AllowNull = allowNull });
        }

        public static ICreateTableColumnQueryProvider AddColumn(this ICreateTableQueryProvider provider, string columnName, Type type, int typeParameter, bool allowNull = false)
        {
            return provider.AddColumn(new ColumnMigratorInfo(columnName, type) { DbTypeParameter = typeParameter.ToString(), AllowNull = allowNull });
        }

    }
}
