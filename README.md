# NPoco.Migrations

Mission: Making migrations, for databasetypes supported by NPoco, as easy as making a query for NPoco.

Check the wiki for current status, getting started and roadmap.

[![NuGet](https://img.shields.io/appveyor/ci/Flaeng/npoco-migrations.svg?style=for-the-badge&label=appveyor&logo=appveyor&logoColor=FFF)](https://ci.appveyor.com/project/Flaeng/npoco-migrations)
[![NuGet](https://img.shields.io/appveyor/tests/Flaeng/npoco-migrations.svg?style=for-the-badge&label=appveyor&logo=appveyor&logoColor=FFF)](https://ci.appveyor.com/project/Flaeng/npoco-migrations) 

[![NuGet](https://img.shields.io/nuget/v/NPoco.Migrations.svg?style=for-the-badge&label=nuget&logo=nuget&logoColor=FFF)](https://www.nuget.org/packages/NPoco.Migrations/)
[![NuGet](https://img.shields.io/nuget/dt/NPoco.Migrations.svg?style=for-the-badge&label=nuget&logo=nuget&logoColor=FFF)](https://www.nuget.org/packages/NPoco.Migrations/)

# Supported databasetypes

Please be aware that not all databasetypes that are supported by NPoco is supported by NPoco.Migrations.

Current support:

![SqlServer2012 Supported](https://img.shields.io/badge/SqlServer2012-Supported-green?style=for-the-badge)

![SqlServer2008 Supported](https://img.shields.io/badge/SqlServer2008-Supported-green?style=for-the-badge)

![SqlServer2005 Supported](https://img.shields.io/badge/SqlServer2005-Supported-green?style=for-the-badge)

![PostgreSQL Not tested/supported](https://img.shields.io/badge/PostgreSQL-Not%20tested%2Fsupported-red?style=for-the-badge)

![Oracle Not tested/supported](https://img.shields.io/badge/Oracle-Not%20tested%2Fsupported-red?style=for-the-badge)

![OracleManaged Not tested/supported](https://img.shields.io/badge/OracleManaged-Not%20tested%2Fsupported-red?style=for-the-badge)

![MySQL Not tested/supported](https://img.shields.io/badge/MySQL-Not%20tested%2Fsupported-red?style=for-the-badge)

![SQLite Supported](https://img.shields.io/badge/SQLite-Supported-green?style=for-the-badge)

![SQLCe Supported](https://img.shields.io/badge/SQLCe-Supported-green?style=for-the-badge)

![Firebird Semi supported](https://img.shields.io/badge/Firebird-Semi%20supported-yellowgreen?style=for-the-badge)

_Firebird cannot handle DateTimeOffset, Guid, Byte[], Char[] and object-column types or autoincremented primary keys yet_

# Current priorities

These are just my priorities - Fell free to submit a pull request that creates support for any of the databasetypes not yet supported. 

1) Firebird support
2) MySql support
3) PostgreSQL support
4) (everything else)
