using System;
using System.Collections.Generic;
using System.Text;

namespace NPoco.Migrations.QueryProviders
{
    public interface IColumnQueryProvider<TQueryProvider>
    {
        TQueryProvider SetPrimaryKey(bool isAutoIncrement);
    }
}
