using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPoco.Migrations.Tests.NET.BaseTests;
using NPoco.Migrations.Tests.NET.ConnectionProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPoco.Migrations.Tests.NET
{
    public abstract class BaseCreateTableTests<TConnectionProvider> : BaseConnectionProviderUnitTests<TConnectionProvider> where TConnectionProvider : ConnectionProvider, new()
    {

        [TestMethod]
        public virtual void Fail_on_creating_empty_table()
        {
            string tableName = "test1";

            Assert.ThrowsException<Exception>(() => migrator.CreateTable(tableName).Execute());

            bool tableExists = migrator.TableExists(tableName);
            Assert.IsFalse(tableExists);
        }

        [TestMethod]
        public virtual void Can_create_simple_table()
        {
            string tableName = "test2";

            migrator.CreateTable(tableName)
                .AddColumn("Id", typeof(int))
                .Execute();

            bool tableExists = migrator.TableExists(tableName);
            Assert.IsTrue(tableExists);
        }

        [TestMethod]
        public virtual void Can_create_primary_key_column_guid()
        {
            string tableName = "test4";

            migrator.CreateTable(tableName)
                .AddColumn("Id", typeof(Guid)).SetPrimaryKey(false)
                .Execute();

            bool tableExists = migrator.TableExists(tableName);
            Assert.IsTrue(tableExists);
        }

        [TestMethod]
        public virtual void Can_create_primary_key_column_int()
        {
            string tableName = "test5";

            migrator.CreateTable(tableName)
                .AddColumn("Id", typeof(int)).SetPrimaryKey(true)
                .Execute();

            bool tableExists = migrator.TableExists(tableName);
            Assert.IsTrue(tableExists);
        }

        [TestMethod]
        public virtual void Can_create_multiple_primary_keys()
        {
            string tableName = "test6";

            migrator.CreateTable(tableName)
                .AddColumn("Id", typeof(int)).SetPrimaryKey(false)
                .AddColumn("Version", typeof(int)).SetPrimaryKey(false)
                .Execute();

            bool tableExists = migrator.TableExists(tableName);
            Assert.IsTrue(tableExists);
        }

        [TableName("CreateTableWithLinqTestModel")]
        class CreateTableWithLinqModel
        {
            public int Id { get; set; }
            [Column("Givenname")]
            public string Name { get; set; }
        }

        [TestMethod]
        public virtual void Can_create_table_with_linq_expressions()
        {
            migrator.CreateTable<CreateTableWithLinqModel>()
                .AddColumn(x => x.Id)
                .AddColumn(x => x.Name)
                .Execute();

            bool exists = migrator.TableExists("CreateTableWithLinqTestModel");
            Assert.IsTrue(exists);

            var columnNames = GetColumnNames("CreateTableWithLinqTestModel");
            Assert.IsTrue(columnNames.Contains("Id"));
            Assert.IsTrue(columnNames.Contains("Givenname"));
        }

    }
}
