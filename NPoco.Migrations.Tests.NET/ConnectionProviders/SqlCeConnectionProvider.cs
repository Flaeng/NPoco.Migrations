using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPoco.Migrations.Tests.NET.ConnectionProviders
{
    [TestClass]
    public class SqlCeConnectionProvider : ConnectionProvider<SqlConnection>
    {
        //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Test.mdf;Initial Catalog=Test;Integrated Security=True
        //Data Source=DESKTOP-L3CB7NJ;Initial Catalog=NPocoMigrations;Integrated Security=True;
        //Server=.\SQLEXPRESS;Database=OpenManagement;User ID=OpenManangement_User;Password=OpenManangement_User

        public SqlCeConnectionProvider() : base(@"Server=.\SQLEXPRESS;Database=NPocoMigrations;User ID=NPocoMigrations;Password=NPocoMigrations", DatabaseType.SQLCe)
        {
        }

        protected override void prepareDatabase()
        {
            var comm = connection.CreateCommand();
            comm.CommandText = "SELECT TOP 1 [TABLE_NAME] FROM INFORMATION_SCHEMA.TABLES";
            deleteTop1Table(comm);
        }

        private void deleteTop1Table(DbCommand comm)
        {
            string tableName = (string)comm.ExecuteScalar();
            if (String.IsNullOrWhiteSpace(tableName))
                return;

            using (var dropTableCommand = connection.CreateCommand())
            {
                dropTableCommand.CommandText = $"DROP TABLE [{tableName}]";
                dropTableCommand.ExecuteNonQuery();
            }
            deleteTop1Table(comm);
        }

        public override IEnumerable<string> GetColumnNames(string tableName)
        {
            return GetColumnNames(new Sql("SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = @0", tableName));
        }

    }
}
