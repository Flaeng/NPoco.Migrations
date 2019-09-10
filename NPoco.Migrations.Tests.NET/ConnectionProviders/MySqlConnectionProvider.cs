using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPoco.Migrations.Tests.NET.ConnectionProviders
{
    [TestClass]
    public class MySqlConnectionProvider : ConnectionProvider<MySqlConnection>
    {
        //private const string connectionStringFormat = @"Server=localhost;Port=3306;Database=NPocoMigrations;Uid=root;Pwd=dennis8800!;";
        private const string connectionStringFormat = @"Server=localhost;Port=3306;Database=NPocoMigrations;IntegratedSecurity=yes;Uid=Dennis;";

        public MySqlConnectionProvider() : base(getConnectionString(), DatabaseType.MySQL)
        {
        }

        private static string getConnectionString()
        {
            return String.Format(connectionStringFormat);
        }

        public override IEnumerable<string> GetColumnNames(string tableName)
        {
            return base.GetColumnNames(new Sql("SELECT COLUMN_NAME FROM information_schema.columns WHERE table_schema='[database]' AND table_name=@0", tableName));
        }
    }
}
