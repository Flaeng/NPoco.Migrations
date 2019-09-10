using System;
using System.Collections.Generic;
using System.Text;

namespace NPoco.Migrations
{
    public interface IMigration
    {
        void Execute(Migrator Migrator, Database Database);
    }
}
