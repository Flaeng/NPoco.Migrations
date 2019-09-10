using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NPoco.Migrations.Tests.NET.Mappers
{
    [TestClass]
    public class TableInfoTests
    {
        class SimpleTestClass
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        [TestMethod]
        public void Can_parse_table_info_simple()
        {
            var tableInfo = TableMigratorInfo.FromPoco(typeof(SimpleTestClass));

            Assert.AreEqual("SimpleTestClass", tableInfo.TableName);
            Assert.IsTrue(tableInfo.PrimaryKey.Equals("Id", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsTrue(tableInfo.AutoIncrement);
        }

        [TableName("testClass"), PrimaryKey(new[] { "Id", "Name" }, AutoIncrement = false)]
        class ExtendedTestClass
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        [TestMethod]
        public void Can_parse_table_info_extended()
        {
            var tableInfo = TableMigratorInfo.FromPoco(typeof(ExtendedTestClass));

            Assert.AreEqual("testClass", tableInfo.TableName);
            Assert.IsTrue(tableInfo.PrimaryKey.Equals("Id,Name", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsFalse(tableInfo.AutoIncrement);
        }

    }
}
