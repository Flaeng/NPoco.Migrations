﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace NPoco.Migrations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class DbTypeAttribute : Attribute
    {
        public Type Type { get; set; }
        public string TypeParameter { get; set; }

        public static DbTypeAttribute FromMember(MemberInfo member)
        {
            DbTypeAttribute dbTypeAttribute = member.GetCustomAttribute<DbTypeAttribute>(true) ?? new DbTypeAttribute();

            if (dbTypeAttribute.Type == null)
                dbTypeAttribute.Type = member.GetMemberInfoType();

            return dbTypeAttribute;
        }
    }
}
