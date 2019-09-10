using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPoco.Migrations.CurrentVersion;
using NPoco.Migrations.Tests.NET.BaseTests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPoco.Migrations.Tests.NET.CurrentVersions
{
    [TestClass]
    public class CsvCurrentVersionProviderTests : BaseVersionProviderTests
    {
        protected string filepath = null;

        protected override ICurrentVersionProvider CurrentVersionProvider
        {
            get
            {
                if (filepath == null)
                {
                    filepath = Directory.GetCurrentDirectory();
                    filepath = Path.Combine(filepath, "versions.csv");
                }
                return new CsvCurrentVersionProvider(filepath);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            File.Delete(filepath);
        }
        

    }
}
