using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPoco.Migrations.Tests.NET.BaseTests;
using NPoco.Migrations.Tests.NET.ConnectionProviders;

namespace NPoco.Migrations.Tests.NET.DatabaseSpecificTests.Sqlite
{
    [TestClass, TestCategory("sqlite")]
    public class ColumnTypeTests : BaseColumnTypeTests<SqliteConnectionProvider>
    {

        [TestMethod]
        public override void PricisionDecimalModel_Test()
        {
            base.PricisionDecimalModel_Test();
            PricisionDecimalModel item;

            database.Insert(new PricisionDecimalModel { PricisionDecimal = 1234.567m });
            item = database.Fetch<PricisionDecimalModel>().Last();
            Assert.AreEqual(1234.567m, item.PricisionDecimal);

            database.Insert(new PricisionDecimalModel { PricisionDecimal = 123.456789m });
            item = database.Fetch<PricisionDecimalModel>().Last();
            Assert.AreEqual(123.456789m, item.PricisionDecimal);
        }

    }
}
