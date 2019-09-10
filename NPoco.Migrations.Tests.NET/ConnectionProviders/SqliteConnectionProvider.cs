using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPoco.Migrations.Tests.NET.ConnectionProviders
{
    [TestClass, TestCategory("sqlite")]
    public class SqliteConnectionProvider : ConnectionProvider<SQLiteConnection>
    {
        public SqliteConnectionProvider() : base(ConfigurationManager.ConnectionStrings["SqliteConnectionString"].ConnectionString, DatabaseType.SQLite)
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
