using NPoco.Migrations.CurrentVersion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace NPoco.Migrations
{
    public class MigrationsBuilder
    {
        public static ICurrentVersionProvider CurrentVersionProvider { get; set; }

        protected string MigrationName { get; }
        protected Database Database { get; }

        private ICurrentVersionProvider currentVersionProvider => CurrentVersionProvider ?? new DatabaseCurrentVersionProvider(Database);
        private Dictionary<Version, IMigration> migrations { get; } = new Dictionary<Version, IMigration>();
        protected IReadOnlyDictionary<Version, IMigration> Migrations => migrations;

        public MigrationsBuilder(string MigrationName, Database Database)
        {
            this.MigrationName = MigrationName;
            this.Database = Database;
        }

        public MigrationsBuilder Append(Version version, IMigration migration)
        {
            if (migrations.ContainsKey(version))
                throw new Exception($"{MigrationName} has two or more migrations with version number {version}");

            migrations.Add(version, migration);
            return this;
        }

        public MigrationsBuilder Append<TMigration>(Version version) where TMigration : IMigration, new() => Append(version, new TMigration());

        public void Execute()
        {
            var currentVersion = currentVersionProvider.GetMigrationVersion(MigrationName);

            var orderedMigrations = migrations.Where(x => currentVersion < x.Key).OrderBy(x => x.Key);
            if (!orderedMigrations.Any())
                return;

            foreach (var migration in orderedMigrations)
            {
                var migrator = new Migrator(Database);
                using (var transaction = Database.GetTransaction())
                {
                    try
                    {
                        migration.Value.Execute(migrator, Database);
                        currentVersionProvider.SetMigrationVersion(MigrationName, migration.Key);
                        transaction.Complete();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

    }
}
