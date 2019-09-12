using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data.SqlServerCe;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPoco.Migrations.Tests.NET.BaseTests;
using NPoco.Migrations.Tests.NET.ConnectionProviders;

namespace NPoco.Migrations.Tests.NET.DatabaseSpecificTests.PostgreSql
{
    [TestClass, TestCategory("postgresql")]
    public class ColumnTypeTests : BaseColumnTypeTests<PostgreSqlConnectionProvider>
    {

        //[TestMethod]
        //public override void DateTime_column_type()
        //{
        //    migrator.CreateTable("DateTime").AddColumn("Value", typeof(DateTime)).SetPrimaryKey(false).Execute();
        //    var min = new Column<DateTime>(DateTime.Now.AddYears(-250));
        //    database.Insert("DateTime", "Value", false, min);
        //    var max = new Column<DateTime>(DateTime.Now.AddYears(250));
        //    database.Insert("DateTime", "Value", false, max);
        //}

        //[TestMethod]
        //public override void PricisionDecimalModel_Test()
        //{
        //    base.PricisionDecimalModel_Test();
        //    Assert.ThrowsException<SqlCeException>(() => database.Insert(new PricisionDecimalModel { PricisionDecimal = 1234.567m }));

        //    database.Insert(new PricisionDecimalModel { PricisionDecimal = 123.456789m });
        //    var item = database.Fetch<PricisionDecimalModel>().Last();
        //    Assert.AreEqual(123.4568m, item.PricisionDecimal);
        //}

    }
}
