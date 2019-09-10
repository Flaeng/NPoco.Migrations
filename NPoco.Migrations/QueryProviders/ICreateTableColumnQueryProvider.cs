using System;
using System.Collections.Generic;
using System.Text;

namespace NPoco.Migrations.QueryProviders
{
    public interface ICreateTableColumnQueryProvider<T> : ICreateTableColumnQueryProvider, ICreateTableQueryProvider<T>, IColumnQueryProvider<ICreateTableColumnQueryProvider<T>>
    {
    }
    public interface ICreateTableColumnQueryProvider : ICreateTableQueryProvider, IColumnQueryProvider<ICreateTableColumnQueryProvider>
    {
    }
}
