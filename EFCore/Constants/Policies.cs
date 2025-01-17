﻿using System.Reflection;

namespace Web.Api.Constants
{
    public static class Policies
    {
        public const string SuperAdmin = nameof(SuperAdmin);
        public const string Admin = nameof(Admin);
        public const string Customer = nameof(Customer);
        public static List<string> List()
        {
            var t = typeof(Policies);
            var fields = typeof(Roles)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            var result = fields
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
                .Select(x => (string)x.GetRawConstantValue())
                .ToList();

            return result;
        }
        public static string ToString()
        {
            var result = string.Join(',', List());

            return result;
        }
    }

}
