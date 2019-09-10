using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPoco.Migrations.Tests.NET.ConnectionProviders
{
    [TestClass]
    public class SqliteConnectionProvider : ConnectionProvider<SQLiteConnection>
    {

        public SqliteConnectionProvider() : base("Data Source=:memory:;Version=3;", DatabaseType.SQLite)
        {
        }

        public override IEnumerable<string> GetColumnNames(string tableName)
        {
            var command = Database.CreateCommand(connection, System.Data.CommandType.Text, $"PRAGMA table_info({tableName})");
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                    yield return (string)reader["name"];
            }
        }

    }
}
