using NPoco.Migrations.CurrentVersion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace NPoco.Migrations
{
    public class MigrationBuilder
    {
        private static Func<Database> database = null;
        public static void Config(Func<Database> database) => MigrationBuilder.database = database;

        public static ICurrentVersionProvider CurrentVersionProvider { get; set; }

        protected string MigrationName { get; }
        protected Database Database { get; }
        private bool disposeDatabase = false;

        private ICurrentVersionProvider currentVersionProvider => CurrentVersionProvider ?? new DatabaseCurrentVersionProvider(Database);
        private Dictionary<Version, IMigration> migrations { get; } = new Dictionary<Version, IMigration>();
        protected IReadOnlyDictionary<Version, IMigration> Migrations => migrations;

        public MigrationBuilder(string MigrationName) : this(MigrationName, MigrationBuilder.database.Invoke(), true)
        {
        }

        public MigrationBuilder(string MigrationName, Database Database) : this(MigrationName, Database, false)
        {
        }

        private MigrationBuilder(string MigrationName, Database Database, bool disposeDatabase)
        {
            this.MigrationName = MigrationName;
            this.Database = Database;
            this.disposeDatabase = disposeDatabase;
        }

        public MigrationBuilder Append(Version version, IMigration migration)
        {
            if (migrations.ContainsKey(version))
                throw new Exception($"{MigrationName} has two or more migrations with version number {version}");

            migrations.Add(version, migration);
            return this;
        }

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

            if (disposeDatabase)
                Database.Dispose();
        }

    }
    public static class MigrationBuilderExtensions
    {

        public static MigrationBuilder Append<TMigration>(this MigrationBuilder builder, Version version) where TMigration : IMigration, new()
        {
            return builder.Append(version, new TMigration());
        }

        public static MigrationBuilder Append<TMigration>(this MigrationBuilder builder) where TMigration : IMigrationVersion, new()
        {
            var migration = new TMigration();
            return builder.Append(migration.Version, migration);
        }

    }
}
