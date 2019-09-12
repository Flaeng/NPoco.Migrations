using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPoco.Migrations.Tests.NET.ConnectionProviders
{
    [TestClass, TestCategory("postgresql")]
    public class PostgreSqlConnectionProvider : ConnectionProvider<NpgsqlConnection>
    {
        public PostgreSqlConnectionProvider() : base(ConfigurationManager.ConnectionStrings["PostgreSqlConnectionString"].ConnectionString, DatabaseType.PostgreSQL)
        {
        }

        public override IEnumerable<string> GetColumnNames(string tableName)
        {
            throw new NotImplementedException();
        }
    }
}
