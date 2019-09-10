using System;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPoco.Migrations.Tests.NET.BaseTests;
using NPoco.Migrations.Tests.NET.ConnectionProviders;

namespace NPoco.Migrations.Tests.NET.DatabaseSpecificTests.Firebird
{
    [TestClass]
    public class ColumnTypeTests : BaseColumnTypeTests<FirebirdConnectionProvider>
    {
        
    }
}
