﻿using System;
using System.Data.SQLite;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPoco.Migrations.Tests.NET.BaseTests;
using NPoco.Migrations.Tests.NET.ConnectionProviders;

namespace NPoco.Migrations.Tests.NET.DatabaseSpecificTests.Sqlite
{
    [TestClass, TestCategory("sqlite")]
    public class TruncateTable : BaseTruncateTableTests<SqliteConnectionProvider>
    {

    }
}
