using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NPoco.Migrations.Extensions
{
    public static class TypeExtensions
    {

        public static IEnumerable<MemberInfo> GetPropertiesAndFields(this Type type)
        {
            foreach (var prop in type.GetProperties())
                yield return prop;
            foreach (var field in type.GetFields())
                yield return field;
        }

    }
}
