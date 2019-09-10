using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPoco.Migrations.Tests.NET.ConnectionProviders
{
    [TestClass, TestCategory("mysql")]
    public class MySqlConnectionProvider : ConnectionProvider<MySqlConnection>
    {
        public MySqlConnectionProvider() : base(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString, DatabaseType.MySQL)
        {
        }

        public override IEnumerable<string> GetColumnNames(string tableName)
        {
            return base.GetColumnNames(new Sql("SELECT COLUMN_NAME FROM information_schema.columns WHERE table_schema='[database]' AND table_name=@0", tableName));
        }
    }
}
