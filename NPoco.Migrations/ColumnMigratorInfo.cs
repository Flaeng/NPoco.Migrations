using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace NPoco.Migrations
{
    public class ColumnMigratorInfo
    {
        public string ColumnName { get; set; }
        public Type Type { get; set; }
        public string DbTypeParameter { get; set; }
        public bool AllowNull { get; set; }
        public string DefaultValue { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsAutoIncrement { get; set; }

        public ColumnMigratorInfo(string ColumnName, Type Type)
        {
            this.ColumnName = ColumnName;
            this.Type = Type;
        }

        public static ColumnMigratorInfo FromMemberInfo(MemberInfo member)
        {
            ColumnInfo columnInfo = ColumnInfo.FromMemberInfo(member);
            if (columnInfo.IgnoreColumn || columnInfo.ResultColumn)
                return null;

            var type = DbTypeAttribute.FromMember(member);
            var isNullable = NullableAttribute.FromMember(member);

            return new ColumnMigratorInfo(columnInfo.ColumnName, type.Type)
            {
                DbTypeParameter = type.TypeParameter,
                AllowNull = isNullable
            };
        }

        internal void SetDefaultValue(string defaultValue)
        {
            this.DefaultValue = defaultValue;
        }

        internal void SetPrimaryKey(bool isAutoIncrement)
        {
            IsPrimaryKey = true;
            this.IsAutoIncrement = isAutoIncrement;
        }

    }
}
