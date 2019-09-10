using System;
using System.Collections.Generic;
using System.Text;

namespace NPoco.Migrations
{
    public abstract class Migration : IMigration
    {
        protected Migrator Migrator { get; private set; }
        protected Database Database { get; private set; }

        public void Execute(Migrator Migrator, Database Database)
        {
            this.Migrator = Migrator;
            this.Database = Database;
            execute();
        }

        protected abstract void execute();
    }
}
