﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPoco.Migrations.Tests.NET.ConnectionProviders
{
    [TestClass, TestCategory("sqlce")]
    public class SqlCeConnectionProvider : ConnectionProvider<SqlCeConnection>
    {
        private readonly static string _dbFilename = "sqlce.sdf";
        private readonly static string _connectionString = "DataSource=" + _dbFilename;

        public SqlCeConnectionProvider() : base(_connectionString, DatabaseType.SQLCe)
        {
        }

        protected override void PreConnectionOpened()
        {
            if (File.Exists("sqlce.sdf"))
                return;

            using (var engine = new SqlCeEngine(_connectionString))
                engine.CreateDatabase();
            base.PreConnectionOpened();
        }

        public override void Cleanup()
        {
            base.Cleanup();
            File.Delete(_dbFilename);
        }

        protected override void PostConnectionOpened()
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
