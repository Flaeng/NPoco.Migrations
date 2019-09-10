using System;
using System.Collections.Generic;
using System.Text;

namespace NPoco.Migrations
{
    public class TableMigratorInfo
    {
        public string TableName { get; set; }
        public string PrimaryKey { get; set; }
        public bool AutoIncrement { get; set; }

        public static TableMigratorInfo FromPoco(Type type)
        {
            var tableInfo = TableInfo.FromPoco(type);
            return new TableMigratorInfo
            {
                TableName = tableInfo.TableName,
                PrimaryKey = tableInfo.PrimaryKey,
                AutoIncrement = tableInfo.AutoIncrement
            };
        }
    }
}
