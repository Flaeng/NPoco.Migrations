using FirebirdSql.Data.FirebirdClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPoco.Migrations.Tests.NET.ConnectionProviders
{
    [TestClass, TestCategory("firebird")]
    public class FirebirdConnectionProvider : ConnectionProvider<FbConnection>
    {
        private readonly static string connectionStringFormat = ConfigurationManager.ConnectionStrings["FirebirdConnectionString"].ConnectionString;

        private string connectionString;
        
        public FirebirdConnectionProvider() : base(getConnectionString(out Guid id), DatabaseType.Firebird)
        {
            connectionString = String.Format(connectionStringFormat, id);
            FbConnection.CreateDatabase(connectionString, true);
        }

        [TestMethod]
        public void TestMethod()
        {
            FbParameter param = new FbParameter();
            param.DbType = System.Data.DbType.String;
            var fbDbType = param.FbDbType;
        }

        private static string getConnectionString(out Guid id)
        {
            id = Guid.NewGuid();
            return String.Format(connectionStringFormat, id);
        }

        public override IEnumerable<string> GetColumnNames(string tableName)
        {
            return Database.Query<Column>("select rdb$field_name from rdb$relation_fields where rdb$relation_name = @0", tableName).Select(x => x.ColumnName.Trim());
        }

        class Column
        {
            [Column("rdb$field_name")]
            public string ColumnName { get; set; }
        }

    }
}
