using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPoco.Migrations.Tests.NET.BaseTests;
using NPoco.Migrations.Tests.NET.ConnectionProviders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPoco.Migrations.Tests.NET
{
    public abstract class BaseColumnTypeTests<TConnectionProvider> : BaseConnectionProviderUnitTests<TConnectionProvider> where TConnectionProvider : ConnectionProvider, new()
    {

        protected class Column<T>
        {
            public T Value { get; set; }
            public Column()
            {
            }
            public Column(T Value)
            {
                this.Value = Value;
            }
        }

        [TestMethod]
        public void byte_column_type()
        {
            if (!connectionProvider.IsSupported)
                return;

            migrator.CreateTable("byte").AddColumn("Value", typeof(byte)).SetPrimaryKey(false).Execute();
            var min = new Column<byte>(byte.MinValue);
            database.Insert("byte", "Value", false, min);
            var max = new Column<byte>(byte.MaxValue);
            database.Insert("byte", "Value", false, max);
        }

        [TestMethod]
        public void byte_array_column_type()
        {
            if (!connectionProvider.IsSupported)
                return;

            migrator.CreateTable("byte_array").AddColumn("byte_array", typeof(byte[])).Execute();
        }

        //[TestMethod]
        //public void sbyte_column_type()
        //{
        //    migrator.CreateTable("sbyte").AddColumn("Value", typeof(sbyte)).SetPrimaryKey(false).Execute();
        //    var min = new Column<sbyte>(sbyte.MinValue);
        //    database.Insert("sbyte", "Value", false, min);
        //    var max = new Column<sbyte>(sbyte.MaxValue);
        //    database.Insert("sbyte", "Value", false, max);
        //}

        [TestMethod]
        public void short_column_type()
        {
            if (!connectionProvider.IsSupported)
                return;

            migrator.CreateTable("short").AddColumn("Value", typeof(short)).SetPrimaryKey(false).Execute();
            var min = new Column<short>(short.MinValue);
            database.Insert("short", "Value", false, min);
            var max = new Column<short>(short.MaxValue);
            database.Insert("short", "Value", false, max);
        }

        //[TestMethod]
        //public void ushort_column_type()
        //{
        //    migrator.CreateTable("ushort").AddColumn("Value", typeof(ushort)).SetPrimaryKey(false).Execute();
        //    var min = new Column<ushort>(ushort.MinValue);
        //    database.Insert("ushort", "Value", false, min);
        //    var max = new Column<ushort>(ushort.MaxValue);
        //    database.Insert("ushort", "Value", false, max);
        //}

        [TestMethod]
        public void int_column_type()
        {
            if (!connectionProvider.IsSupported)
                return;

            migrator.CreateTable("int").AddColumn("Value", typeof(int)).SetPrimaryKey(false).Execute();
            var min = new Column<int>(int.MinValue);
            database.Insert("int", "Value", false, min);
            var max = new Column<int>(int.MaxValue);
            database.Insert("int", "Value", false, max);
        }

        //[TestMethod]
        //public void uint_column_type()
        //{
        //    migrator.CreateTable("uint").AddColumn("Value", typeof(uint)).SetPrimaryKey(false).Execute();
        //    var min = new Column<uint>(uint.MinValue);
        //    database.Insert("uint", "Value", false, min);
        //    var max = new Column<uint>(uint.MaxValue);
        //    database.Insert("uint", "Value", false, max);
        //}

        [TestMethod]
        public void long_column_type()
        {
            if (!connectionProvider.IsSupported)
                return;

            migrator.CreateTable("long").AddColumn("Value", typeof(long)).SetPrimaryKey(false).Execute();
            var min = new Column<long>(long.MinValue);
            database.Insert("long", "Value", false, min);
            var max = new Column<long>(long.MaxValue);
            database.Insert("long", "Value", false, max);
        }

        //[TestMethod]
        //public void ulong_column_type()
        //{
        //    migrator.CreateTable("ulong").AddColumn("Value", typeof(ulong)).SetPrimaryKey(false).Execute();
        //    var min = new Column<ulong>(ulong.MinValue);
        //    database.Insert("ulong", "Value", false, min);
        //    var max = new Column<ulong>(ulong.MaxValue);
        //    database.Insert("ulong", "Value", false, max);
        //}

        [TestMethod]
        public void float_column_type()
        {
            if (!connectionProvider.IsSupported)
                return;

            migrator.CreateTable("float").AddColumn("Value", typeof(float)).SetPrimaryKey(false).Execute();
            var min = new Column<float>(float.MinValue);
            database.Insert("float", "Value", false, min);
            var max = new Column<float>(float.MaxValue);
            database.Insert("float", "Value", false, max);
        }

        [TestMethod]
        public void double_column_type()
        {
            if (!connectionProvider.IsSupported)
                return;

            migrator.CreateTable("double").AddColumn("Value", typeof(double)).SetPrimaryKey(false).Execute();
            var min = new Column<double>(double.MinValue);
            database.Insert("double", "Value", false, min);
            var max = new Column<double>(double.MaxValue);
            database.Insert("double", "Value", false, max);
        }

        [TestMethod]
        public void decimal_column_type()
        {
            if (!connectionProvider.IsSupported)
                return;

            migrator.CreateTable("decimal").AddColumn("Value", typeof(decimal)).SetPrimaryKey(false).Execute();
            var min = new Column<decimal>(-decimal.Parse(String.Join("", Enumerable.Range(0, 13).Select(x => "9"))));
            min.Value -= 0.99m;
            database.Insert("decimal", "Value", false, min);
            var max = new Column<decimal>(decimal.Parse(String.Join("", Enumerable.Range(0, 13).Select(x => "9"))));
            min.Value += 0.99m;
            database.Insert("decimal", "Value", false, max);
        }

        [TestMethod]
        public void char_column_type()
        {
            if (!connectionProvider.IsSupported)
                return;

            migrator.CreateTable("char").AddColumn("Value", typeof(char)).SetPrimaryKey(false).Execute();
            var min = new Column<char>(char.MinValue);
            database.Insert("char", "Value", false, min);

            var a = new Column<char>('a');
            database.Insert("char", "Value", false, a);

            var Z = new Column<char>('Z');
            database.Insert("char", "Value", false, Z);

            var percentage = new Column<char>('%');
            database.Insert("char", "Value", false, percentage);
        }

        [TestMethod]
        public void char_array_column_type()
        {
            if (!connectionProvider.IsSupported)
                return;

            migrator.CreateTable("char_array").AddColumn("char_array", typeof(char[])).Execute();
        }

        [TestMethod]
        public void string_column_type()
        {
            if (!connectionProvider.IsSupported)
                return;

            migrator.CreateTable("string").AddColumn("string", typeof(string)).Execute();
        }

        [TestMethod]
        public void bool_column_type()
        {
            if (!connectionProvider.IsSupported)
                return;

            migrator.CreateTable("bool").AddColumn("Value", typeof(bool)).SetPrimaryKey(false).Execute();
            var min = new Column<bool>(false);
            database.Insert("bool", "Value", false, min);
            var max = new Column<bool>(true);
            database.Insert("bool", "Value", false, max);
        }

        [TestMethod]
        public void object_column_type()
        {
            if (!connectionProvider.IsSupported)
                return;

            migrator.CreateTable("object").AddColumn("object", typeof(object)).Execute();
        }

        class ObjectObject
        {
            public Object Object { get; set; }
        }

        [TestMethod]
        public virtual void DateTime_column_type()
        {
            if (!connectionProvider.IsSupported)
                return;

            migrator.CreateTable("DateTime").AddColumn("Value", typeof(DateTime)).SetPrimaryKey(false).Execute();
            var min = new Column<DateTime>(DateTime.MinValue);
            database.Insert("DateTime", "Value", false, min);
            var max = new Column<DateTime>(DateTime.MaxValue);
            database.Insert("DateTime", "Value", false, max);
        }

        [TestMethod]
        public void DateTimeOffset_column_type()
        {
            if (!connectionProvider.IsSupported)
                return;

            migrator.CreateTable("DateTimeOffset").AddColumn("Value", typeof(DateTimeOffset)).SetPrimaryKey(false).Execute();
            var min = new Column<DateTimeOffset>(DateTimeOffset.MinValue);
            database.Insert("DateTimeOffset", "Value", false, min);
            var max = new Column<DateTimeOffset>(DateTimeOffset.MaxValue);
            database.Insert("DateTimeOffset", "Value", false, max);
        }

        //[TestMethod]
        //public void TimeSpan_column_type()
        //{
        //    migrator.CreateTable("TimeSpan").AddColumn("Value", typeof(TimeSpan)).SetPrimaryKey(false).Execute();
        //    var min = new Column<TimeSpan>(TimeSpan.MinValue);
        //    database.Insert("TimeSpan", "Value", false, min);
        //    var max = new Column<TimeSpan>(TimeSpan.MaxValue);
        //    database.Insert("TimeSpan", "Value", false, max);
        //}

        [TestMethod]
        public void Guid_column_type()
        {
            if (!connectionProvider.IsSupported)
                return;

            migrator.CreateTable("Guid").AddColumn("Value", typeof(Guid)).Execute();
            Guid id = Guid.NewGuid();
            var item = new Column<Guid>(id);
            database.Insert("Guid", "Value", false, item);

            var items = database.Fetch<Column<Guid>>($"SELECT * FROM {database.DatabaseType.EscapeTableName("Guid")}");
            Assert.AreEqual(1, items.Count);
            Assert.AreEqual(id, items.ElementAt(0).Value);
        }



        protected class SmallStringModel
        {
            public int Id { get; set; }

            [DbType(TypeParameter = "20")]
            public string SmallString { get; set; }
        }

        [TestMethod]
        public virtual void SmallStringModel_Test()
        {
            if (!connectionProvider.IsSupported)
                return;

            migrator.CreateTable<SmallStringModel>().AddColumn(x => x.Id).AddColumn(x => x.SmallString).Execute();
            SmallStringModel item;

            database.Insert(new SmallStringModel { SmallString = "" });
            item = database.Fetch<SmallStringModel>().Last();
            Assert.AreEqual("", item.SmallString);

            database.Insert(new SmallStringModel { SmallString = "12345" });
            item = database.Fetch<SmallStringModel>().Last();
            Assert.AreEqual("12345", item.SmallString);

            database.Insert(new SmallStringModel { SmallString = "12345678901234567890" });
            item = database.Fetch<SmallStringModel>().Last();
            Assert.AreEqual("12345678901234567890", item.SmallString);
        }

        protected class PricisionDecimalModel
        {
            public int Id { get; set; }

            [DbType(TypeParameter = "7,4")]
            public decimal PricisionDecimal { get; set; }
        }

        [TestMethod]
        public virtual void PricisionDecimalModel_Test()
        {
            if (!connectionProvider.IsSupported)
                return;

            migrator.CreateTable<PricisionDecimalModel>().AddColumn(x => x.Id).AddColumn(x => x.PricisionDecimal).Execute();
            PricisionDecimalModel item;

            database.Insert(new PricisionDecimalModel { PricisionDecimal = 123.4567m });
            item = database.Fetch<PricisionDecimalModel>().Last();
            Assert.AreEqual(123.4567m, item.PricisionDecimal);
        }

        class NullableStringModel
        {
            public int Id { get; set; }

            [Nullable]
            public string NullableString { get; set; }
        }

        [TestMethod]
        public void NullableStringModel_Test()
        {
            if (!connectionProvider.IsSupported)
                return;

            migrator.CreateTable<NullableStringModel>().AddColumn(x => x.Id).AddColumn(x => x.NullableString).Execute();

            database.Insert(new NullableStringModel { NullableString = null });
            database.Insert(new NullableStringModel { NullableString = null });

            var items = database.Fetch<NullableStringModel>();
            Assert.AreEqual(2, items.Count);
            Assert.AreEqual(1, items[0].Id);
            Assert.IsNull(items[0].NullableString);
            Assert.AreEqual(2, items[1].Id);
            Assert.IsNull(items[1].NullableString);
        }

        class NullableNumberModel
        {
            public int Id { get; set; }

            [Nullable]
            public int? NullableNumber { get; set; }
        }

        [TestMethod]
        public void NullableNumberModel_Test()
        {
            if (!connectionProvider.IsSupported)
                return;

            migrator.CreateTable<NullableNumberModel>().AddColumn(x => x.Id).AddColumn(x => x.NullableNumber).Execute();

            database.Insert(new NullableNumberModel { NullableNumber = null });
            database.Insert(new NullableNumberModel { NullableNumber = null });

            var items = database.Fetch<NullableNumberModel>();
            Assert.AreEqual(2, items.Count);
            Assert.AreEqual(1, items[0].Id);
            Assert.IsNull(items[0].NullableNumber);
            Assert.AreEqual(2, items[1].Id);
            Assert.IsNull(items[1].NullableNumber);
        }

    }
}
