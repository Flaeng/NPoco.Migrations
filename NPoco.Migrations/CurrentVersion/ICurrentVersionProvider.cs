using System;
using System.Collections.Generic;
using System.Text;

namespace NPoco.Migrations.CurrentVersion
{
    public interface ICurrentVersionProvider
    {
        Version GetMigrationVersion(string migrationName);
        void SetMigrationVersion(string migrationName, Version version);
    }
}
