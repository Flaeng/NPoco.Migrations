using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPoco.Migrations.Tests.NET.BaseTests;
using NPoco.Migrations.Tests.NET.ConnectionProviders;
using System;
using System.Collections.Generic;
using System.Data;
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
            migrator.CreateTable("byte").AddColumn("Value", typeof(byte)).SetPrimaryKey(false).Execute();
            var min = new Column<byte>(byte.MinValue);
            database.Insert("byte", "Value", false, min);
            var max = new Column<byte>(byte.MaxValue);
            database.Insert("byte", "Value", false, max);
        }

        [TestMethod]
        public void byte_array_column_type()
        {
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
            migrator.CreateTable("float").AddColumn("Value", typeof(float)).SetPrimaryKey(false).Execute();
            var min = new Column<float>(float.MinValue);
            database.Insert("float", "Value", false, min);
            var max = new Column<float>(float.MaxValue);
            database.Insert("float", "Value", false, max);
        }

        [TestMethod]
        public void double_column_type()
        {
            migrator.CreateTable("double").AddColumn("Value", typeof(double)).SetPrimaryKey(false).Execute();
            var min = new Column<double>(double.MinValue);
            database.Insert("double", "Value", false, min);
            var max = new Column<double>(double.MaxValue);
            database.Insert("double", "Value", false, max);
        }

        [TestMethod]
        public void decimal_column_type()
        {
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
            migrator.CreateTable("char").AddColumn("Value", typeof(char)).SetPrimaryKey(false).Execute();
            var min = new Column<char>(char.MinValue);
            database.Insert("char", "Value", false, min);
            var max = new Column<char>(char.MaxValue);
            database.Insert("char", "Value", false, max);
        }

        [TestMethod]
        public void char_array_column_type()
        {
            migrator.CreateTable("char_array").AddColumn("char_array", typeof(char[])).Execute();
        }

        [TestMethod]
        public void string_column_type()
        {
            migrator.CreateTable("string").AddColumn("string", typeof(string)).Execute();
        }

        [TestMethod]
        public void bool_column_type()
        {
            migrator.CreateTable("bool").AddColumn("Value", typeof(bool)).SetPrimaryKey(false).Execute();
            var min = new Column<bool>(false);
            database.Insert("bool", "Value", false, min);
            var max = new Column<bool>(true);
            database.Insert("bool", "Value", false, max);
        }

        [TestMethod]
        public void object_column_type()
        {
            migrator.CreateTable("object").AddColumn("object", typeof(object)).Execute();
        }

        class ObjectObject
        {
            public Object Object { get; set; }
        }

        [TestMethod]
        public virtual void DateTime_column_type()
        {
            migrator.CreateTable("DateTime").AddColumn("Value", typeof(DateTime)).SetPrimaryKey(false).Execute();
            var min = new Column<DateTime>(DateTime.MinValue);
            database.Insert("DateTime", "Value", false, min);
            var max = new Column<DateTime>(DateTime.MaxValue);
            database.Insert("DateTime", "Value", false, max);
        }

        [TestMethod]
        public void DateTimeOffset_column_type()
        {
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
            migrator.CreateTable("Guid").AddColumn("Value", typeof(Guid)).Execute();
            Guid id = Guid.NewGuid();
            var item = new Column<Guid>(id);
            database.Insert("Guid", "Value", false, item);

            var items = database.Fetch<Column<Guid>>($"SELECT * FROM {database.DatabaseType.EscapeTableName("Guid")}");
            Assert.AreEqual(1, items.Count);
            Assert.AreEqual(id, items.ElementAt(0).Value);
        }

    }
}
