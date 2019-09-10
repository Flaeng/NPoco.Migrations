using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace NPoco.Migrations.DatabaseTypes
{
    public class DefaultMigratorDatabaseType : IMigratorDatabaseType
    {
        protected virtual bool? isPrimaryKeyKeywordInline { get => null; }
        protected readonly DatabaseType databaseType;

        public DefaultMigratorDatabaseType(DatabaseType DatabaseType)
        {
            this.databaseType = DatabaseType;
        }

        public virtual Sql TableExists(string tableName)
        {
            return new Sql($"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @0", tableName);
        }

        public virtual Sql TruncateTable(string tableName)
        {
            return new Sql($"TRUNCATE TABLE {databaseType.EscapeTableName(tableName)}");
        }

        public virtual Sql DropTable(string tableName)
        {
            return new Sql($"DROP TABLE {databaseType.EscapeTableName(tableName)}");
        }

        public virtual Sql[] AlterTable(string tableName, ColumnMigratorInfo[] columnToAdd)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in columnToAdd)
                builder.Append($"ALTER TABLE {databaseType.EscapeTableName(tableName)} ADD {formatColumn(item)};");
            return new[] { new Sql(builder.ToString()) };
        }

        public virtual Sql[] CreateTable(TableMigratorInfo table, ColumnMigratorInfo[] columns)
        {
            var isPrimaryKeyKeywordInline = shouldFormatPrimaryKeyInlinse(table);

            var columnString = String.Join(", ", columns.Select(x => formatColumn(table, x)));

            if (!isPrimaryKeyKeywordInline && !String.IsNullOrWhiteSpace(table.PrimaryKey))
            {
                columnString += $", ";
                columnString += formatPrimaryKeys(table);
            }

            return columnString.Length != 0 ? new[] { new Sql($"CREATE TABLE {databaseType.EscapeTableName(table.TableName)} ({columnString})") } : throw new Exception("Cannot create table without columns");
        }

        protected virtual bool shouldFormatPrimaryKeyInlinse(TableMigratorInfo table)
        {
            return (table.PrimaryKey?.Split(',')?.Length ?? 0) <= 1 || table.AutoIncrement;
        }

        protected virtual string formatPrimaryKeys(TableMigratorInfo tableInfo)
        {
            var keys = tableInfo.PrimaryKey.Split(',');
            if (tableInfo.AutoIncrement)
                return "";

            return $"PRIMARY KEY ({String.Join(",", keys.Select(databaseType.EscapeSqlIdentifier))})";
        }

        protected virtual string formatAutoIncrement() => $"IDENTITY";

        protected class StringComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y) => x.Equals(y, StringComparison.InvariantCultureIgnoreCase);
            public int GetHashCode(string obj) => obj.GetHashCode();
        }

        protected virtual string formatColumn(TableMigratorInfo table, ColumnMigratorInfo column)
        {
            var isPrimaryKeyKeywordInline = shouldFormatPrimaryKeyInlinse(table);

            var columnDbTypeString = formatColumnDataTypeString(column.Type, column.DbTypeParameter);
            var isPrimaryKey = table.PrimaryKey?.Split(',')?.Contains(column.ColumnName, new StringComparer()) ?? false;

            StringBuilder builder = new StringBuilder(databaseType.EscapeSqlIdentifier(column.ColumnName));

            builder.Append(" ");
            builder.Append(columnDbTypeString);

            builder.Append(" ");
            builder.Append(column.AllowNull ? "NULL" : "NOT NULL");

            if (isPrimaryKey)
            {
                if (isPrimaryKeyKeywordInline)
                    builder.Append(" PRIMARY KEY");

                if (table.AutoIncrement)
                {
                    builder.Append(" ");
                    builder.Append(formatAutoIncrement());
                }
            }

            return builder.ToString();
        }

        protected virtual string formatColumn(ColumnMigratorInfo column)
        {
            return $"{databaseType.EscapeSqlIdentifier(column.ColumnName)} {formatColumnDataTypeString(column.Type, column.DbTypeParameter)}";
        }

        protected virtual string formatColumnDataTypeString(Type type, string dbTypeParameter)
        {
            var dbTypeString = ParseType(type, out var dbType);
            dbTypeParameter = dbTypeParameter?.Trim(' ', '(', ')');
            if (dbTypeParameter == null)
            {
                switch (dbType)
                {
                    case DbType.AnsiString:
                    case DbType.AnsiStringFixedLength:
                    case DbType.String:
                    case DbType.StringFixedLength:
                        dbTypeParameter = (!String.IsNullOrWhiteSpace(dbTypeParameter) && dbTypeParameter.Equals("max", StringComparison.InvariantCultureIgnoreCase)) ? "MAX" :
                            (!String.IsNullOrWhiteSpace(dbTypeParameter) && int.TryParse(dbTypeParameter, out int dbTypeInt)) ? dbTypeInt.ToString() : "200";
                        break;

                    case DbType.Decimal:
                        dbTypeParameter = (!String.IsNullOrWhiteSpace(dbTypeParameter) && TupleIntParse(dbTypeParameter, out Tuple<int, int> tupleInt)) ? $"{tupleInt.Item1},{tupleInt.Item2}" : "15,2";
                        break;
                }
            }
            return (dbTypeParameter == null) ? dbTypeString : $"{dbTypeString}({dbTypeParameter})";
        }

        public string ParseType(Type type)
            => ParseType(type, out var _);

        public virtual string ParseType(Type type, out DbType dbType)
        {
            dbType = databaseType.LookupDbType(type, String.Empty) ?? DbType.Binary;
            return ParseDbType(dbType);
        }

        public virtual string ParseDbType(DbType type)
        {
            switch (type)
            {
                case DbType.AnsiString: return "VARCHAR";

                case DbType.StringFixedLength:
                case DbType.AnsiStringFixedLength: return "CHAR";

                //case DbType.StringFixedLength: return "NCHAR";

                case DbType.String: return "NVARCHAR";

                case DbType.Boolean:
                case DbType.Binary: return "BIT";

                case DbType.Currency: return "CURRENCY";

                case DbType.Date: return "DATE";

                case DbType.DateTime:
                case DbType.DateTime2: return "DATETIME";

                case DbType.DateTimeOffset: return "DATETIMEOFFSET";

                case DbType.Int16: return "SMALLINT";

                case DbType.Decimal: return "DECIMAL";

                case DbType.Int32: return "INTEGER";

                case DbType.Int64: return "BIGINT";

                case DbType.Double:
                case DbType.Single: return "FLOAT";

                //case DbType.Double: return "DOUBLE";

                case DbType.Byte: return "TINYINT";

                case DbType.Guid: return "UNIQUEIDENTIFIER";

                case DbType.Object: return "sql_variant";

                case DbType.Time: return "TIME";

                default: throw new NotSupportedException();
            }
        }

        private bool TupleIntParse(string numbers, out Tuple<int, int> TupleIntParse)
        {
            TupleIntParse = null;
            if (String.IsNullOrWhiteSpace(numbers))
                return false;

            var arr = numbers.Split(',');
            if (arr.Length != 2 || arr.Any(x => !int.TryParse(x, out int dummy)))
                return false;

            var intArr = arr.Select(int.Parse).ToArray();
            TupleIntParse = new Tuple<int, int>(intArr.ElementAt(0), intArr.ElementAt(1));
            return true;
        }

    }
}
