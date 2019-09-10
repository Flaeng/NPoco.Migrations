using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPoco.Migrations.CurrentVersion;
using NPoco.Migrations.Tests.NET.BaseTests;
using NPoco.Migrations.Tests.NET.ConnectionProviders;

namespace NPoco.Migrations.Tests.NET.CurrentVersions
{
    [TestClass]
    public class DatabaseCurrentVersionProviderTests : BaseVersionProviderTests
    {
        protected SqliteConnectionProvider SqliteConnectionProvider = new SqliteConnectionProvider();

        protected override ICurrentVersionProvider CurrentVersionProvider
        {
            get
            {
                SqliteConnectionProvider.Initialize();
                return new DatabaseCurrentVersionProvider(SqliteConnectionProvider.Database);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            SqliteConnectionProvider.Cleanup();
        }

    }
}
