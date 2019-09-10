using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPoco.Migrations.CurrentVersion;
using NPoco.Migrations.Tests.NET.ConnectionProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPoco.Migrations.Tests.NET.BaseTests
{
    public abstract class BaseUnitTests
    {
        protected SqliteConnectionProvider SqliteConnectionProvider = new SqliteConnectionProvider();

        protected Database Database
        {
            get
            {
                SqliteConnectionProvider.Initialize();
                return SqliteConnectionProvider.Database;
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            SqliteConnectionProvider.Cleanup();
        }
    }
}
