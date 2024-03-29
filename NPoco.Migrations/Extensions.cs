﻿using NPoco.Migrations.QueryProviders;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NPoco.Migrations
{
    public static class MigratorExtensions
    {

        public static bool TableExists<T>(this Migrator Migrator)
            => Migrator.TableExists(typeof(T));

        public static bool TableExists(this Migrator Migrator, Type type)
            => Migrator.TableExists(TableInfo.FromPoco(type).TableName);

        public static ICreateTableQueryProvider CreateTable<T>(this Migrator Migrator, bool autoDetectColumns)
            => Migrator.CreateTable(typeof(T), autoDetectColumns);

        public static ICreateTableQueryProvider<T> CreateTable<T>(this Migrator Migrator)
            => Migrator.CreateTable<T>(TableMigratorInfo.FromPoco(typeof(T)));

        public static ICreateTableQueryProvider CreateTable(this Migrator Migrator, string tableName)
            => Migrator.CreateTable(new TableMigratorInfo { TableName = tableName });

        public static IAlterTableQueryProvider<T> AlterTable<T>(this Migrator Migrator)
            => Migrator.AlterTable<T>(TableMigratorInfo.FromPoco(typeof(T)).TableName);

        public static IAlterTableQueryProvider AlterTable(this Migrator Migrator, Type type)
            => Migrator.AlterTable(TableMigratorInfo.FromPoco(type).TableName);

        public static void TruncateTable<T>(this Migrator Migrator)
            => Migrator.TruncateTable(typeof(T));

        public static void TruncateTable(this Migrator Migrator, Type type)
            => Migrator.TruncateTable(TableInfo.FromPoco(type).TableName);

        public static void DropTable<T>(this Migrator Migrator)
            => Migrator.DropTable(typeof(T));

        public static void DropTable(this Migrator Migrator, Type type)
            => Migrator.DropTable(TableInfo.FromPoco(type).TableName);

    }
}
