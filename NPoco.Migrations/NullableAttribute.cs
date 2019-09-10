using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NPoco.Migrations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class NullableAttribute : Attribute
    {
        public static bool FromMember(MemberInfo member)
        {
            return member.GetCustomAttribute<NullableAttribute>(true) != null || Nullable.GetUnderlyingType(member.GetMemberInfoType()) != null;
        }
    }
}
