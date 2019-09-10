using NPoco.Migrations.Extensions;
using NPoco.Migrations.QueryProviders;
using NPoco.Migrations.DatabaseTypes;
using System;
using System.Linq;
using System.Reflection;
using IQueryProvider = NPoco.Migrations.QueryProviders.IQueryProvider;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

namespace NPoco.Migrations
{
    public class Migrator : IDisposable
    {
        public Database Database { get; }
        public IMigratorDatabaseType MigratorSqlSyntaxProvider { get; }

        private IQueryProvider lastQueryProvider = null;

        public Migrator(Database Database)
        {
            this.Database = Database;
            this.MigratorSqlSyntaxProvider = MigratorDatabaseType.Resolve(Database.DatabaseType);
        }

        public Migrator(Database Database, IMigratorDatabaseType MigratorSqlSyntaxProvider)
            : this(Database)
        {
            this.MigratorSqlSyntaxProvider = MigratorSqlSyntaxProvider;
        }

        /// <summary>
        /// Validates that last query was executed properly
        /// </summary>
        private void checkLastQueryProvider()
        {
            //No query was set, or last query was executed - All is good
            if (lastQueryProvider == null || lastQueryProvider.Executed)
                return;

            //Last query wasnt executed - throw exception to help developer call Execute
            throw new Exception($"Last IQueryProvider wasn't executed. Did you forget to call Execute? :)");
        }

        public bool TableExists(string tableName)
        {
            var sql = MigratorSqlSyntaxProvider.TableExists(tableName);
            return Database.ExecuteScalar<int>(sql) != 0;
        }

        public ICreateTableQueryProvider CreateTable(Type type, bool autoDetectColumns)
        {
            checkLastQueryProvider();
            var query = new CreateTableQueryProvider(Database, MigratorSqlSyntaxProvider, TableMigratorInfo.FromPoco(type));
            lastQueryProvider = query;
            if (autoDetectColumns)
            {
                var members = type.GetPropertiesAndFields();
                foreach (var member in members)
                {
                    var column = ColumnMigratorInfo.FromMemberInfo(member);
                    if (column != null)
                        query.AddColumn(column);
                }
            }
            return query;
        }

        public ICreateTableQueryProvider CreateTable(string tableName)
        {
            checkLastQueryProvider();
            var tableInfo = new TableMigratorInfo { TableName = tableName };
            var result = new CreateTableQueryProvider(Database, MigratorSqlSyntaxProvider, tableInfo);
            lastQueryProvider = result;
            return result;
        }

        public IAlterTableQueryProvider AlterTable(string tableName)
        {
            checkLastQueryProvider();
            var result = new AlterTableQueryProvider(Database, MigratorSqlSyntaxProvider, tableName);
            lastQueryProvider = result;
            return result;
        }

        public void TruncateTable(string tableName)
        {
            var sql = MigratorSqlSyntaxProvider.TruncateTable(tableName);
            Database.Execute(sql);
        }

        public void DropTable(string tableName)
        {
            var sql = MigratorSqlSyntaxProvider.DropTable(tableName);
            Database.Execute(sql);
        }

        public void Dispose()
        {
            checkLastQueryProvider();
            Database.Dispose();
        }

    }
}