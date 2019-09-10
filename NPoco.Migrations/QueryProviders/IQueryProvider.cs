using System;
using System.Collections.Generic;
using System.Text;

namespace NPoco.Migrations.QueryProviders
{
    public interface IQueryProvider
    {
        Database Database { get; }
        bool Executed { get; }
        void Execute();
    }
}
