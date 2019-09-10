using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NPoco.Migrations.Tests.NET
{
    [TestClass]
    public class ColumnMigratorInfoTests
    {
        class ColumnMigratorInfoTestModel
        {
            public int Number { get; set; }
            public string Name { get; set; }

            [Nullable]
            public string NullableName { get; set; }

            [Column("RenamedColumn")]
            public string ShouldBeRenamed { get; set; }

            [Ignore]
            public string ShouldBeIgnored { get; set; }

            [ResultColumn]
            public string Result { get; set; }

            [ComputedColumn]
            public string Computed { get; set; }

            [SerializedColumn]
            public string Serialized { get; set; }
        }

        [TestMethod]
        public void ColumnMigratorInfo_number()
        {
            var member = typeof(ColumnMigratorInfoTestModel).GetProperty(nameof(ColumnMigratorInfoTestModel.Number));
            var memberInfo = ColumnMigratorInfo.FromMemberInfo(member);

            Assert.IsNotNull(memberInfo);
            Assert.AreEqual("Number", memberInfo.ColumnName);
            Assert.IsFalse(memberInfo.AllowNull);
            Assert.IsFalse(memberInfo.IsPrimaryKey);
            Assert.IsFalse(memberInfo.IsAutoIncrement);
        }

        [TestMethod]
        public void ColumnMigratorInfo_string()
        {
            var member = typeof(ColumnMigratorInfoTestModel).GetProperty(nameof(ColumnMigratorInfoTestModel.Name));
            var memberInfo = ColumnMigratorInfo.FromMemberInfo(member);

            Assert.IsNotNull(memberInfo);
            Assert.AreEqual("Name", memberInfo.ColumnName);
            Assert.IsFalse(memberInfo.AllowNull);
            Assert.IsFalse(memberInfo.IsPrimaryKey);
            Assert.IsFalse(memberInfo.IsAutoIncrement);
        }

        [TestMethod]
        public void ColumnMigratorInfo_nullable_string()
        {
            var member = typeof(ColumnMigratorInfoTestModel).GetProperty(nameof(ColumnMigratorInfoTestModel.NullableName));
            var memberInfo = ColumnMigratorInfo.FromMemberInfo(member);

            Assert.IsNotNull(memberInfo);
            Assert.AreEqual("NullableName", memberInfo.ColumnName);
            Assert.IsTrue(memberInfo.AllowNull);
            Assert.IsFalse(memberInfo.IsPrimaryKey);
            Assert.IsFalse(memberInfo.IsAutoIncrement);
        }

        [TestMethod]
        public void ColumnMigratorInfo_ShouldBeRenamed()
        {
            var member = typeof(ColumnMigratorInfoTestModel).GetProperty(nameof(ColumnMigratorInfoTestModel.ShouldBeRenamed));
            var memberInfo = ColumnMigratorInfo.FromMemberInfo(member);

            Assert.IsNotNull(memberInfo);
            Assert.AreEqual("RenamedColumn", memberInfo.ColumnName);
        }

        [TestMethod]
        public void ColumnMigratorInfo_ShouldBeIgnored()
        {
            var member = typeof(ColumnMigratorInfoTestModel).GetProperty(nameof(ColumnMigratorInfoTestModel.ShouldBeIgnored));
            var memberInfo = ColumnMigratorInfo.FromMemberInfo(member);

            Assert.IsNull(memberInfo);
        }

        [TestMethod]
        public void ColumnMigratorInfo_ResultColumn()
        {
            var member = typeof(ColumnMigratorInfoTestModel).GetProperty(nameof(ColumnMigratorInfoTestModel.Result));
            var memberInfo = ColumnMigratorInfo.FromMemberInfo(member);

            Assert.IsNull(memberInfo);
        }

        [TestMethod]
        public void ColumnMigratorInfo_ComputedColumn()
        {
            var member = typeof(ColumnMigratorInfoTestModel).GetProperty(nameof(ColumnMigratorInfoTestModel.Computed));
            var memberInfo = ColumnMigratorInfo.FromMemberInfo(member);

            Assert.IsNotNull(memberInfo);
            Assert.AreEqual("Computed", memberInfo.ColumnName);
        }

        [TestMethod]
        public void ColumnMigratorInfo_SerializedColumn()
        {
            var member = typeof(ColumnMigratorInfoTestModel).GetProperty(nameof(ColumnMigratorInfoTestModel.Serialized));
            var memberInfo = ColumnMigratorInfo.FromMemberInfo(member);

            Assert.IsNotNull(memberInfo);
            Assert.AreEqual("Serialized", memberInfo.ColumnName);
        }



    }
}
