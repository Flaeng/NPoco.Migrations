using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPoco.Migrations.Tests.NET.BaseTests;
using NPoco.Migrations.Tests.NET.ConnectionProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPoco.Migrations.Tests.NET.MigrationsBuilder
{
    [TestClass]
    public class MigrationsBuilderTests : BaseUnitTests
    {

        public class Migration : IMigration
        {
            private Action action;
            public Migration(Action action)
            {
                this.action = action;
            }
            public void Execute(Migrator Migrator, Database Database)
            {
                action.Invoke();
            }
        }

        [TestMethod]
        public void Use_case_1()
        {
            Stack<Version> versions = new Stack<Version>();

            new Migrations.MigrationsBuilder("TestMigration1", Database)
                .Append(new Version(1, 0), new Migration(() => versions.Push(new Version(1, 0))))
                .Execute();

            Assert.AreEqual(1, versions.Count);
            Assert.AreEqual(new Version(1, 0), versions.Pop());
        }

        [TestMethod]
        public void Use_case_1_2()
        {
            Stack<Version> versions = new Stack<Version>();

            new Migrations.MigrationsBuilder("TestMigration1_2", Database)
                .Append(new Version(1, 0), new Migration(() => versions.Push(new Version(1, 0))))
                .Execute();

            new Migrations.MigrationsBuilder("TestMigration1_2", Database)
                .Append(new Version(1, 2), new Migration(() => versions.Push(new Version(1, 2))))
                .Execute();

            Assert.AreEqual(2, versions.Count);
            Assert.AreEqual(new Version(1, 2), versions.Pop());
            Assert.AreEqual(new Version(1, 0), versions.Pop());
        }

        [TestMethod]
        public void Use_case_2()
        {
            Stack<Version> versions = new Stack<Version>();

            new Migrations.MigrationsBuilder("TestMigration2", Database)
                .Append(new Version(0, 1), new Migration(() => versions.Push(new Version(0, 1))))
                .Append(new Version(0, 2), new Migration(() => versions.Push(new Version(0, 2))))
                .Execute();

            Assert.AreEqual(2, versions.Count);
            Assert.AreEqual(new Version(0, 2), versions.Pop());
            Assert.AreEqual(new Version(0, 1), versions.Pop());
        }

        [TestMethod]
        public void Use_case_2_1()
        {
            Stack<Version> versions = new Stack<Version>();

            new Migrations.MigrationsBuilder("TestMigration2", Database)
                .Append(new Version(0, 4), new Migration(() => versions.Push(new Version(0, 4))))
                .Append(new Version(0, 2), new Migration(() => versions.Push(new Version(0, 2))))
                .Append(new Version(0, 1), new Migration(() => versions.Push(new Version(0, 1))))
                .Execute();

            Assert.AreEqual(3, versions.Count);
            Assert.AreEqual(new Version(0, 4), versions.Pop());
            Assert.AreEqual(new Version(0, 2), versions.Pop());
            Assert.AreEqual(new Version(0, 1), versions.Pop());
        }

        [TestMethod]
        public void Use_case_3()
        {
            Stack<Version> versions = new Stack<Version>();

            new Migrations.MigrationsBuilder("TestMigration3", Database)
                .Append(new Version(0, 1), new Migration(() => versions.Push(new Version(0, 1))))
                .Append(new Version(1, 0), new Migration(() => versions.Push(new Version(1, 0))))
                .Append(new Version(0, 2), new Migration(() => versions.Push(new Version(0, 2))))
                .Execute();

            Assert.AreEqual(3, versions.Count);
            Assert.AreEqual(new Version(1, 0), versions.Pop());
            Assert.AreEqual(new Version(0, 2), versions.Pop());
            Assert.AreEqual(new Version(0, 1), versions.Pop());
        }

        [TestMethod]
        public void Use_case_4()
        {
            Stack<Version> versions = new Stack<Version>();

            var migrations = new Migrations.MigrationsBuilder("TestMigration4", Database)
                .Append(new Version(0, 1), new Migration(() => versions.Push(new Version(0, 1))));

            Assert.ThrowsException<Exception>(() => migrations.Append(new Version(0, 1), new Migration(() => versions.Push(new Version(0, 1)))));
        }

        [TestMethod]
        public void Use_case_5()
        {
            Stack<Version> versions = new Stack<Version>();

            new Migrations.MigrationsBuilder("TestMigration5", Database)
                .Append(new Version(0, 2), new Migration(() => versions.Push(new Version(0, 2))))
                .Execute();

            new Migrations.MigrationsBuilder("TestMigration5", Database)
                .Append(new Version(0, 1), new Migration(() => versions.Push(new Version(0, 1))))
                .Append(new Version(0, 3), new Migration(() => versions.Push(new Version(0, 3))))
                .Execute();

            Assert.AreEqual(2, versions.Count);
            Assert.AreEqual(new Version(0, 3), versions.Pop());
            Assert.AreEqual(new Version(0, 2), versions.Pop());
        }

    }
}
