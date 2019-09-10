using FirebirdSql.Data.FirebirdClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPoco.Migrations.Tests.NET.ConnectionProviders
{
    [TestClass]
    public class FirebirdConnectionProvider : ConnectionProvider<FbConnection>
    {
        private const string connectionStringFormat = @"User=SYSDBA;Password=NyPPk6EP2j;Database=Firebird-{0}.fdb;DataSource=localhost;
            Port=3050;Dialect=3;Charset=NONE;Role=;Connection lifetime=15;Pooling=true;
            MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=0;";

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
