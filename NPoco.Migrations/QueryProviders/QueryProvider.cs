using System;
using System.Collections.Generic;
using System.Text;

namespace NPoco.Migrations.QueryProviders
{
    public abstract class QueryProvider : IQueryProvider
    {
        public bool Executed { get; private set; } = false;
        public Database Database { get; }

        public QueryProvider(Database Database)
        {
            this.Database = Database;
        }

        public virtual void Execute()
        {
            Executed = true;
        }
    }
}
