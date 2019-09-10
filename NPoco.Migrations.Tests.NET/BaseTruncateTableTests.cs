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
    public abstract class BaseTruncateTableTests<TConnectionProvider> : BaseConnectionProviderUnitTests<TConnectionProvider> where TConnectionProvider : ConnectionProvider, new()
    {

        public class TruncateTestModel
        {
            public string Name { get; set; } = "John Doe";
        }

        [TestMethod]
        public void Can_truncate_table()
        {
            try
            {
                var query = migrator.CreateTable(nameof(TruncateTestModel));
                query = query.AddColumn(nameof(TruncateTestModel.Name), typeof(string));
                query.Execute();

                database.Insert(new TruncateTestModel());

                var count = database.Query(typeof(TruncateTestModel), $"SELECT * FROM {nameof(TruncateTestModel)}").Count();
                Assert.AreNotEqual(0, count);
            }
            catch (Exception)
            {
                return;
            }

            {
                migrator.TruncateTable(nameof(TruncateTestModel));

                bool tableExists = migrator.TableExists(nameof(TruncateTestModel));
                Assert.IsTrue(tableExists);
                var count = database.Query<TruncateTestModel>().Count();
                Assert.AreEqual(0, count);
            }
        }

    }
}
