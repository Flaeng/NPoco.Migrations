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
    public abstract class BaseAlterTableTests<TConnectionProvider> : BaseConnectionProviderUnitTests<TConnectionProvider> where TConnectionProvider : ConnectionProvider, new()
    {


        [TestMethod]
        public virtual void Can_create_simple_table_and_add_column()
        {
            string tableName = "test3";

            try
            {
                var createQuery = migrator.CreateTable(tableName);
                createQuery = createQuery.AddColumn("Id", typeof(int));
                createQuery.Execute();
            }
            catch (Exception)
            {
                return;
            }

            var alterQuery = migrator.AlterTable(tableName);
            alterQuery = alterQuery.AddColumn("IdString", typeof(string));
            alterQuery.Execute();

            var columns = GetColumnNames(tableName).ToList();
            Assert.AreEqual(2, columns.Count);
            Assert.AreEqual("Id", columns[0]);
            Assert.AreEqual("IdString", columns[1]);
        }

        [TestMethod]
        public virtual void Can_create_simple_table_and_add_column_multiple_columns()
        {
            string tableName = "test4";

            try
            {
                var createQuery = migrator.CreateTable(tableName);
                createQuery = createQuery.AddColumn("Id", typeof(int));
                createQuery.Execute();
            }
            catch (Exception)
            {
                return;
            }

            var alterQuery = migrator.AlterTable(tableName);
            alterQuery = alterQuery.AddColumn("IdString", typeof(string));
            alterQuery = alterQuery.AddColumn("Name", typeof(string));
            alterQuery.Execute();

            var columns = GetColumnNames(tableName).ToList();
            Assert.AreEqual(3, columns.Count);
            Assert.AreEqual("Id", columns[0]);
            Assert.AreEqual("IdString", columns[1]);
            Assert.AreEqual("Name", columns[2]);
        }

        [TestMethod]
        public virtual void Can_handle_no_altering_table()
        {
            string tableName = "test6";

            try
            {
                migrator.CreateTable(tableName)
                    .AddColumn("Id", typeof(int))
                    .AddColumn("IdString", typeof(int))
                    .Execute();
            }
            catch (Exception)
            {
                return;
            }

            migrator.AlterTable(tableName)
                .Execute();
        }

    }
}
