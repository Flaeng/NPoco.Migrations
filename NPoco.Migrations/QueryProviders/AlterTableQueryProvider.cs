using NPoco.Migrations.DatabaseTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace NPoco.Migrations.QueryProviders
{
    public class AlterTableQueryProvider : QueryProvider, IAlterTableColumnQueryProvider, IAlterTableQueryProvider
    {
        public IMigratorDatabaseType MigratorSqlSyntaxProvider { get; }
        public string TableName { get; }

        protected List<ColumnMigratorInfo> columnsToAdd { get; } = new List<ColumnMigratorInfo>();

        public AlterTableQueryProvider(Database Database, IMigratorDatabaseType migratorSqlSyntaxProvider, string tableName)
            : base(Database)
        {
            this.MigratorSqlSyntaxProvider = migratorSqlSyntaxProvider;
            this.TableName = tableName;
        }

        public IAlterTableColumnQueryProvider AddColumn(ColumnMigratorInfo column)
        {
            columnsToAdd.Add(column);
            return this;
        }
        
        public IAlterTableColumnQueryProvider SetPrimaryKey(bool isAutoIncrement)
        {
            columnsToAdd.Last().SetPrimaryKey(isAutoIncrement);
            return this;
        }

        public override void Execute()
        {
            if (!columnsToAdd.Any())
                return;

            Sql[] sqlList;
            try
            {
                sqlList = MigratorSqlSyntaxProvider.AlterTable(TableName, columnsToAdd.ToArray());
                foreach (var sql in sqlList)
                    Database.Execute(sql);
            }
            catch (NotSupportedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to alter table: '{TableName}'", ex);
            }
            base.Execute();
        }

    }
}
