using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NPoco.Migrations.DatabaseTypes
{
    public class SqlCeDatabaseType : DefaultMigratorDatabaseType
    {
        public SqlCeDatabaseType() : base(DatabaseType.SQLCe)
        {
        }

        public override Sql TruncateTable(string tableName)
        {
            return new Sql("DELETE FROM " + databaseType.EscapeTableName(tableName));
        }

        public override string ParseDbType(DbType type)
        {
            if (type == DbType.StringFixedLength || type == DbType.AnsiStringFixedLength)
                return "nchar";
            return base.ParseDbType(type);
        }

    }
}
