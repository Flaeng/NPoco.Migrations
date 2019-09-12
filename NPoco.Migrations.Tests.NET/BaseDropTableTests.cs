using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPoco.Migrations.Tests.NET.BaseTests;
using NPoco.Migrations.Tests.NET.ConnectionProviders;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPoco.Migrations.Tests.NET
{
    public abstract class BaseDropTableTests<TConnectionProvider> : BaseConnectionProviderUnitTests<TConnectionProvider> where TConnectionProvider : ConnectionProvider, new()
    {
        public class DropTestModel
        {
            public string Name { get; set; } = "John Doe";
        }

        [TestMethod]
        public void Can_drop_table()
        {
            if (!connectionProvider.IsSupported)
                return;

            try
            {
                migrator.CreateTable(nameof(DropTestModel))
                    .AddColumn(nameof(DropTestModel.Name), typeof(string))
                    .Execute();

                bool tableExists = migrator.TableExists(nameof(DropTestModel));
                Assert.IsTrue(tableExists);
            }
            catch (Exception)
            {
                return;
            }

            {
                migrator.DropTable(nameof(DropTestModel));
                bool tableExists = migrator.TableExists(nameof(DropTestModel));
                Assert.IsFalse(tableExists);
            }
        }

    }
}
