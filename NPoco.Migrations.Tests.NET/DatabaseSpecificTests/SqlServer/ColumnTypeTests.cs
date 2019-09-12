using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPoco.Migrations.Tests.NET.BaseTests;
using NPoco.Migrations.Tests.NET.ConnectionProviders;

namespace NPoco.Migrations.Tests.NET.DatabaseSpecificTests.SqlServer
{
    [TestClass, TestCategory("sqlserver")]
    public class ColumnTypeTests : BaseColumnTypeTests<SqlServerConnectionProvider>
    {
        
        [TestMethod]
        public override void DateTime_column_type()
        {
            migrator.CreateTable("DateTime").AddColumn("Value", typeof(DateTime)).SetPrimaryKey(false).Execute();
            var min = new Column<DateTime>(DateTime.Now.AddYears(-250));
            database.Insert("DateTime", "Value", false, min);
            var max = new Column<DateTime>(DateTime.MaxValue);
            database.Insert("DateTime", "Value", false, max);
        }

        [TestMethod]
        public override void SmallStringModel_Test()
        {
            base.SmallStringModel_Test();
            Assert.ThrowsException<SqlException>(() => database.Insert(new SmallStringModel { SmallString = "123456789012345678901" }));
        }

        [TestMethod]
        public override void PricisionDecimalModel_Test()
        {
            base.PricisionDecimalModel_Test();
            Assert.ThrowsException<SqlException>(() => database.Insert(new PricisionDecimalModel { PricisionDecimal = 1234.567m }));

            database.Insert(new PricisionDecimalModel { PricisionDecimal = 123.456789m });
            var item = database.Fetch<PricisionDecimalModel>().Last();
            Assert.AreEqual(123.4568m, item.PricisionDecimal);
        }

    }
}
