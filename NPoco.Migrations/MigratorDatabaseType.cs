using NPoco.Migrations.DatabaseTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPoco.Migrations
{
    public abstract class MigratorDatabaseType
    {
        public static IMigratorDatabaseType SqlServer2012 => new DefaultMigratorDatabaseType(DatabaseType.SqlServer2012);
        public static IMigratorDatabaseType SqlServer2008 => new DefaultMigratorDatabaseType(DatabaseType.SqlServer2008);
        public static IMigratorDatabaseType SqlServer2005 => new DefaultMigratorDatabaseType(DatabaseType.SqlServer2005);
        public static IMigratorDatabaseType PostgreSQL => new PostgreSQLDatabaseType();
        public static IMigratorDatabaseType Oracle => new DefaultMigratorDatabaseType(DatabaseType.Oracle);
        public static IMigratorDatabaseType OracleManaged => new DefaultMigratorDatabaseType(DatabaseType.OracleManaged);
        public static IMigratorDatabaseType MySQL => new MySqlDatabaseType();
        public static IMigratorDatabaseType SQLite => new SqliteDatabaseType();
        public static IMigratorDatabaseType SQLCe => new DefaultMigratorDatabaseType(DatabaseType.SQLCe);
        public static IMigratorDatabaseType Firebird => new FirebirdMigratorDatabaseType();

        public static IMigratorDatabaseType Resolve(DatabaseType databaseType)
        {
            return Resolve(String.Empty, databaseType.GetProviderName());
        }

        public static IMigratorDatabaseType Resolve(string typeName, string providerName)
        {
            //Yes, I stole this from https://github.com/schotime/NPoco/blob/2f41c21af8e8bfa13558b28b2cdbecd604d16655/src/NPoco/DatabaseType.cs#L248
            //Sue me - Wait, no! Please don't!

            // Try using type name first (more reliable)
            if (typeName.StartsWith("MySql"))
                return MySQL;
            if (typeName.StartsWith("SqlCe"))
                return SQLCe;
            if (typeName.StartsWith("Npgsql") || typeName.StartsWith("PgSql"))
                return PostgreSQL;
            if (typeName.StartsWith("OracleManaged"))
                return OracleManaged;
            if (typeName.StartsWith("Oracle"))
                return Oracle;
            if (typeName.StartsWith("SQLite"))
                return SQLite;
            if (typeName.StartsWith("SqlConnection"))
                return SqlServer2008;
            if (typeName.StartsWith("Fb") || typeName.StartsWith("Firebird"))
                return Firebird;

            if (!string.IsNullOrEmpty(providerName))
            {
                if (providerName.IndexOf("MySql", StringComparison.OrdinalIgnoreCase) >= 0)
                    return MySQL;
                if (providerName.IndexOf("SqlServerCe", StringComparison.OrdinalIgnoreCase) >= 0)
                    return SQLCe;
                if (providerName.IndexOf("pgsql", StringComparison.OrdinalIgnoreCase) >= 0)
                    return PostgreSQL;
                if (providerName.IndexOf("Oracle.DataAccess", StringComparison.OrdinalIgnoreCase) >= 0)
                    return Oracle;
                if (providerName.IndexOf("Oracle.ManagedDataAccess", StringComparison.OrdinalIgnoreCase) >= 0)
                    return OracleManaged;
                if (providerName.IndexOf("SQLite", StringComparison.OrdinalIgnoreCase) >= 0)
                    return SQLite;
                if (providerName.IndexOf("Firebird", StringComparison.OrdinalIgnoreCase) >= 0)
                    return Firebird;
            }

            return SqlServer2005;
        }

    }
}
