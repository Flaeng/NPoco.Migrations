using System;
using System.Collections.Generic;
using System.Text;

namespace NPoco.Migrations.CurrentVersion
{
    public class DatabaseCurrentVersionProvider : ICurrentVersionProvider
    {
        protected readonly Database Database;

        public DatabaseCurrentVersionProvider(Database Database)
        {
            this.Database = Database;
            var migrator = new Migrator(Database);
            if (!migrator.TableExists<CurrentVersionEntry>())
            {
                migrator.CreateTable<CurrentVersionEntry>(false)
                    .AddColumn("MigrationName", typeof(string), 200)
                    .AddColumn("Version", typeof(string), 100)
                    .Execute();
            }
        }

        public Version GetMigrationVersion(string migrationName)
        {
            return Database.Query<CurrentVersionEntry>().SingleOrDefault(x => x.MigrationName == migrationName)?.Version ?? new Version(0, 0);
        }

        public void SetMigrationVersion(string migrationName, Version version)
        {
            Database.Save(new CurrentVersionEntry { MigrationName = migrationName, Version = version });
        }

        [TableName("_migrations"), PrimaryKey("MigrationName", AutoIncrement = false)]
        class CurrentVersionEntry
        {
            public string MigrationName { get; set; }

            [Column("Version")]
            public string VersionString { get; set; }

            [Ignore]
            public Version Version { get => Version.TryParse(VersionString, out var result) ? result : new Version(0, 0); set => VersionString = value.ToString(); }
        }
    }
}
