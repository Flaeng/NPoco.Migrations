using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPoco.Migrations.Tests.NET.ConnectionProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPoco.Migrations.Tests.NET.BaseTests
{
    [TestClass]
    public abstract class BaseConnectionProviderUnitTests<TConnectionProvider> where TConnectionProvider : ConnectionProvider, new()
    {
        protected Migrator migrator { get; set; }
        protected Database database { get; set; }

        protected TConnectionProvider connectionProvider { get; private set; }

        public BaseConnectionProviderUnitTests()
        {
            this.connectionProvider = new TConnectionProvider();
        }

        [TestInitialize]
        public virtual void Initialize()
        {
            connectionProvider.Initialize();
            database = connectionProvider.Database;
            migrator = new Migrator(database);
        }

        [TestCleanup]
        public virtual void Cleanup()
        {
            connectionProvider.Cleanup();
        }

        protected IEnumerable<string> GetColumnNames(string tableName) => connectionProvider.GetColumnNames(tableName);

    }
}
