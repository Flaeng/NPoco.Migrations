using System;
using System.Collections.Generic;
using System.Text;

namespace NPoco.Migrations.QueryProviders
{
    public interface IAlterTableColumnQueryProvider<T> : IAlterTableColumnQueryProvider, IAlterTableQueryProvider<T>, IColumnQueryProvider<IAlterTableColumnQueryProvider<T>>
    {
    }
    public interface IAlterTableColumnQueryProvider : IAlterTableQueryProvider, IColumnQueryProvider<IAlterTableColumnQueryProvider>
    {
    }
}
