using System;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPoco.Migrations.Tests.NET.BaseTests;
using NPoco.Migrations.Tests.NET.ConnectionProviders;

namespace NPoco.Migrations.Tests.NET.DatabaseSpecificTests.SqlCe
{
    [TestClass]
    public class ColumnTypeTests : BaseColumnTypeTests<SqlCeConnectionProvider>
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

    }
}
