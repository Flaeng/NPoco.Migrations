using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NPoco.Migrations.DatabaseTypes
{
    public class FirebirdMigratorDatabaseType : DefaultMigratorDatabaseType
    {
        public FirebirdMigratorDatabaseType() : base(DatabaseType.Firebird)
        {
        }

        public override Sql[] AlterTable(string tableName, ColumnMigratorInfo[] columnToAdd)
        {
            return columnToAdd.Select(x => base.AlterTable(tableName, new[] { x }).Single()).ToArray();
        }

        public override Sql[] CreateTable(TableMigratorInfo table, ColumnMigratorInfo[] columns)
        {
            var isPrimaryKeyKeywordInline = shouldFormatPrimaryKeyInlinse(table);

            var columnString = String.Join(",\n", columns.Select(x => formatColumn(table, x)));

            if (!isPrimaryKeyKeywordInline && !String.IsNullOrWhiteSpace(table.PrimaryKey))
            {
                columnString += $", ";
                columnString += formatPrimaryKeys(table);
            }

            if (columnString.Length != 0)
            {
                var primaryQuery = new[] {
                    new Sql($"CREATE TABLE {databaseType.EscapeTableName(table.TableName)} (\n{columnString} )")
                };

                return (table.AutoIncrement) ? getAutoIncrementQuery(table).Concat(primaryQuery).ToArray() : primaryQuery;
            }
            throw new Exception("Cannot create table without columns");
        }

        private static Sql[] getAutoIncrementQuery(TableMigratorInfo table)
        {
            var generatorTableName = Regex.Replace(table.TableName, "[^A-Za-z]", "_");
            var generatorName = $"gen_{generatorTableName}_id";

            //var createGenerator = $"CREATE GENERATOR {generatorName};";
            //var setGenerator = $"SET SEQUENCE {generatorName} TO 0;";
            //            var createTrigger = $@"set term !! ;
            //CREATE TRIGGER {generatorTableName}_BI FOR {table.TableName}
            //ACTIVE BEFORE INSERT POSITION 0
            //AS
            //BEGIN
            //if (NEW.ID is NULL) then NEW.ID = GEN_ID({generatorName}, 1);
            //END!!
            //set term ; !!";
            //return new[] { new Sql(createGenerator), new Sql(setGenerator), new Sql(createTrigger) };


            var createGenerator = $"CREATE SEQUENCE {generatorName};";
            var createTrigger = $@"set term !! ;
CREATE TRIGGER {generatorTableName}_BI FOR {table.TableName}
ACTIVE BEFORE INSERT POSITION 0
AS
BEGIN
  NEW.ID = next value for {generatorName};
END!!
set term ; !!";
            return new[] { new Sql(createGenerator), new Sql(createTrigger) };
        }

        protected override string formatAutoIncrement() => String.Empty;

        public override string ParseDbType(DbType type)
        {
            switch (type)
            {
                case DbType.Boolean:
                case DbType.Byte: return "SMALLINT";

                case DbType.DateTime:
                case DbType.DateTime2: return "TIMESTAMP";

                case DbType.Guid: return "VARCHAR(100)";

                case DbType.String: return "VARCHAR";

                default: return base.ParseDbType(type);
            }
        }

        //protected override string formatColumn(TableMigratorInfo table, ColumnMigratorInfo column)
        //{
        //    var isPrimaryKeyKeywordInline = shouldFormatPrimaryKeyInlinse(table);

        //    var columnDbTypeString = formatColumnDataTypeString(column.Type, column.DbTypeParameter);
        //    var isPrimaryKey = table.PrimaryKey?.Split(',')?.Contains(column.ColumnName, new StringComparer()) ?? false;

        //    StringBuilder builder = new StringBuilder(column.ColumnName);

        //    builder.Append(" ");
        //    builder.Append(columnDbTypeString);

        //    builder.Append(" ");
        //    builder.Append(column.AllowNull ? "NULL" : "NOT NULL");

        //    if (isPrimaryKey)
        //    {
        //        if (isPrimaryKeyKeywordInline)
        //            builder.Append(" PRIMARY KEY");

        //        if (table.AutoIncrement)
        //        {
        //            builder.Append(" ");
        //            builder.Append(formatAutoIncrement());
        //        }
        //    }

        //    return builder.ToString();
        //}

        public override Sql DropTable(string tableName)
        {
            return base.DropTable(tableName);
        }

        public override Sql TableExists(string tableName)
        {
            return new Sql("SELECT 1 FROM RDB$RELATIONS WHERE RDB$RELATION_NAME = @0", tableName);
        }

        public override Sql TruncateTable(string tableName)
        {
            return new Sql($"DELETE FROM {databaseType.EscapeSqlIdentifier(tableName)}");
        }
    }
}
