using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPoco.Migrations.Tests.NET.ConnectionProviders
{
    [TestClass]
    public abstract class ConnectionProvider<TDbConnection> : ConnectionProvider where TDbConnection : DbConnection, new()
    {
        public ConnectionProvider(string ConnectionString, DatabaseType databaseType) : base(setConnectionString(new TDbConnection(), ConnectionString), databaseType)
        {
        }

        private static DbConnection setConnectionString(TDbConnection dbConnection, string connectionString)
        {
            dbConnection.ConnectionString = connectionString;
            return dbConnection;
        }

        [TestMethod]
        public void Can_connection()
        {
            Assert.AreEqual(System.Data.ConnectionState.Open, connection.State);
        }

    }
    public abstract class ConnectionProvider
    {
        private bool? isSupported = null;
        public bool IsSupported { get => isSupported.Value; }
        public Database Database { get; private set; }
        protected DbConnection connection { get; }
        protected DatabaseType databaseType { get; }

        private static object _syncRoot = new object();

        public ConnectionProvider(DbConnection connection, DatabaseType databaseType)
        {
            this.connection = connection;
            this.databaseType = databaseType;
        }

        [TestInitialize]
        public virtual void Initialize()
        {
            if (Database != null)
                return;

            if (isSupported.HasValue && isSupported.Value == false)
                return;

            lock (_syncRoot)
            {
                if (Database != null)
                    return;

                if (isSupported.HasValue && isSupported.Value == false)
                    return;

                try
                {
                    PreConnectionOpened();
                    if (connection.State != System.Data.ConnectionState.Open)
                        connection.Open();
                    PostConnectionOpened();
                    Database = new Database(connection, databaseType);
                    isSupported = true;
                }
                catch (Exception)
                {
                    isSupported = false;
                }
            }
        }

        protected virtual void PreConnectionOpened()
        {
        }

        protected virtual void PostConnectionOpened()
        {
        }

        [TestCleanup]
        public virtual void Cleanup()
        {
            Database?.Dispose();
            connection?.Dispose();
        }

        public abstract IEnumerable<string> GetColumnNames(string tableName);

        protected IEnumerable<string> GetColumnNames(Sql sql)
        {
            return Database.Query<Column>(sql).Select(x => x.COLUMN_NAME).ToList();
        }

        class Column
        {
            public string COLUMN_NAME { get; set; }
        }

    }
}
