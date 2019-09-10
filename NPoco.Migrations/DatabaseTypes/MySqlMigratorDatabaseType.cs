using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NPoco.Migrations.DatabaseTypes
{
    public class MySqlDatabaseType : DefaultMigratorDatabaseType
    {
        public MySqlDatabaseType() : base(DatabaseType.MySQL)
        {
        }

        public override Sql[] AlterTable(string tableName, ColumnMigratorInfo[] columnToAdd)
        {
            return base.AlterTable(tableName, columnToAdd);
        }

        public override Sql[] CreateTable(TableMigratorInfo table, ColumnMigratorInfo[] columns)
        {
            return base.CreateTable(table, columns);
        }

        public override Sql DropTable(string tableName)
        {
            return base.DropTable(tableName);
        }

        public override string ParseDbType(DbType type)
        {
            return base.ParseDbType(type);
        }

        public override string ParseType(Type type, out DbType dbType)
        {
            return base.ParseType(type, out dbType);
        }

        public override Sql TableExists(string tableName)
        {
            return base.TableExists(tableName);
        }

        public override Sql TruncateTable(string tableName)
        {
            return base.TruncateTable(tableName);
        }
    }
}
