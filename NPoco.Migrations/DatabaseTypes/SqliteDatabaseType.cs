using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace NPoco.Migrations.DatabaseTypes
{
    public class SqliteDatabaseType : DefaultMigratorDatabaseType
    {
        
        public SqliteDatabaseType() : base(DatabaseType.SQLite)
        {
        }

        public override Sql TableExists(string tableName)
        {
            return new Sql($"SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = @0", tableName);
        }

        protected override string formatPrimaryKeys(TableMigratorInfo tableInfo)
        {
            var keys = tableInfo.PrimaryKey.Split(',');
            return $"CONSTRAINT PK PRIMARY KEY ({String.Join(",", keys.Select(databaseType.EscapeSqlIdentifier))})";
        }

        protected override string formatAutoIncrement() => "AUTOINCREMENT";

        public override string ParseDbType(DbType type)
        {
            switch (type)
            {
                case DbType.Double: return "DOUBLE";

                default: return base.ParseDbType(type);
            }
        }

        public override Sql TruncateTable(string tableName)
        {
            return new Sql($"DELETE FROM {databaseType.EscapeSqlIdentifier(tableName)}");
        }

    }
}
