using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NPoco.Migrations.DatabaseTypes
{
    public interface IMigratorDatabaseType
    {
        Sql[] CreateTable(TableMigratorInfo table, ColumnMigratorInfo[] columns);
        Sql[] AlterTable(string tableName, ColumnMigratorInfo[] columnToAdd);
        Sql TableExists(string tableName);
        Sql TruncateTable(string tableName);
        Sql DropTable(string tableName);

        string ParseType(Type type, out DbType dbType);
        string ParseDbType(DbType type);
    }
}
