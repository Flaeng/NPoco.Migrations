using NPoco.Migrations.DatabaseTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace NPoco.Migrations.QueryProviders
{
    public class CreateTableQueryProvider : QueryProvider, ICreateTableColumnQueryProvider, ICreateTableQueryProvider
    {
        public IMigratorDatabaseType MigratorSqlSyntaxProvider { get; }
        public TableMigratorInfo Table { get; }
        public List<ColumnMigratorInfo> Columns { get; } = new List<ColumnMigratorInfo>();

        public CreateTableQueryProvider(Database Database, IMigratorDatabaseType MigratorSqlSyntaxProvider, TableMigratorInfo Table)
            : base(Database)
        {
            this.MigratorSqlSyntaxProvider = MigratorSqlSyntaxProvider;
            this.Table = Table;
        }

        public ICreateTableColumnQueryProvider AddColumn(string columnName, Type type, string typeParameter = null, bool allowNull = false)
        {
            return AddColumn(new ColumnMigratorInfo(columnName, type) { DbTypeParameter = typeParameter, AllowNull = allowNull });
        }

        public ICreateTableColumnQueryProvider SetDefaultValue(string defaultValue)
        {
            Columns.Last().DefaultValue = defaultValue;
            return this;
        }

        public ICreateTableColumnQueryProvider AddColumn(ColumnMigratorInfo column)
        {
            Columns.Add(column);
            return this;
        }

        public ICreateTableColumnQueryProvider SetPrimaryKey(bool isAutoIncrement)
        {
            var lastColumn = Columns.Last().ColumnName;
            Table.PrimaryKey = (String.IsNullOrWhiteSpace(Table.PrimaryKey)) ? lastColumn : Table.PrimaryKey + "," + lastColumn;
            Table.AutoIncrement = isAutoIncrement;
            return this;
        }

        public override void Execute()
        {
            if (!Columns.Any())
                throw new Exception("Cannot create table without columns");

            Sql[] sqlList;
#if DEBUG
#else
            try
            {
#endif
            sqlList = MigratorSqlSyntaxProvider.CreateTable(Table, Columns.ToArray());
            foreach (var sql in sqlList)
                Database.Execute(sql);
#if DEBUG
#else
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create table: '{Table.TableName}'", ex);
            }
#endif
            base.Execute();
        }

    }
}
