using NPoco.Migrations.DatabaseTypes;
using NPoco.Migrations.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NPoco.Migrations.QueryProviders
{
    public class AlterTableQueryProvider<T> : AlterTableQueryProvider, IAlterTableColumnQueryProvider<T>, IAlterTableQueryProvider<T>
    {
        public AlterTableQueryProvider(Database Database, IMigratorDatabaseType migratorSqlSyntaxProvider, string tableName) : base(Database, migratorSqlSyntaxProvider, tableName)
        {
        }

        public IAlterTableColumnQueryProvider<T> AddColumn<TMember>(Expression<Func<T, TMember>> memberExpression)
        {
            var member = memberExpression.GetMember();
            var column = ColumnMigratorInfo.FromMemberInfo(member);
            AddColumn(column);
            return this;
        }

        public new IAlterTableColumnQueryProvider<T> SetPrimaryKey(bool isAutoIncrement)
        {
            base.SetPrimaryKey(isAutoIncrement);
            return this;
        }
    }
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
