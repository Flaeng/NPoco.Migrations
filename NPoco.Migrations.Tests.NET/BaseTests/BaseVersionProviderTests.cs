using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPoco.Migrations.CurrentVersion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPoco.Migrations.Tests.NET.BaseTests
{
    public abstract class BaseVersionProviderTests
    {
        protected abstract ICurrentVersionProvider CurrentVersionProvider { get; }

        [TestMethod]
        public void Can_get_default_version_for_non_existing_migration()
        {
            //Arrange
            string migrationName = "test_migration_name";

            //Act
            var version = CurrentVersionProvider.GetMigrationVersion(migrationName);

            //Assert
            Assert.AreEqual(new Version(0, 0), version);
        }

        [TestMethod]
        public void Can_Set_version_for_non_existing_migration()
        {
            //Arrange
            string migrationName = "test_migration_name";
            Version newVersion = new Version(2, 4);

            //Act
            CurrentVersionProvider.SetMigrationVersion(migrationName, newVersion);

            //Assert
            var version = CurrentVersionProvider.GetMigrationVersion(migrationName);
            Assert.AreEqual(newVersion, version);
        }

        [TestMethod]
        public void Can_set_new_version_for_migration()
        {
            //Arrange
            string migrationName = "test_migration_name";
            Version newVersion = new Version(2, 4);
            CurrentVersionProvider.SetMigrationVersion(migrationName, newVersion);
            newVersion = new Version(2, 6);

            //Act
            CurrentVersionProvider.SetMigrationVersion(migrationName, newVersion);

            //Assert
            var version = CurrentVersionProvider.GetMigrationVersion(migrationName);
            Assert.AreEqual(newVersion, version);
        }


    }
}
