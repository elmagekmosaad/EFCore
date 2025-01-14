namespace Web.Api.Constants
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            };
        }
        public static List<string> GenerateAllPermissions()
        {
            var allPermissions = new List<string>();
            var allModules = Enum.GetValues(typeof(Modules)).Cast<string>().ToList();
            foreach (var module in allModules)
            {
                allPermissions.AddRange(GeneratePermissionsForModule(module));
            }
            return allPermissions;
        }
        public static class Admin
        {
            public const string Create = "Permissions.Admin.Create";
            public const string View = "Permissions.Admin.View";
            public const string Edit = "Permissions.Admin.Edit";
            public const string Delete = "Permissions.Admin.Delete";
        }
        public static class Computer
        {
            public const string Create = "Permissions.Computer.Create";
            public const string View = "Permissions.Computer.View";
            public const string Edit = "Permissions.Computer.Edit";
            public const string Delete = "Permissions.Computer.Delete";
        }
        public static class Customer
        {
            public const string Create = "Permissions.Customer.Create";
            public const string View = "Permissions.Customer.View";
            public const string Edit = "Permissions.Customer.Edit";
            public const string Delete = "Permissions.Customer.Delete";
        }
        public static class Subscription
        {
            public const string Create = "Permissions.Subscription.Create";
            public const string View = "Permissions.Subscription.View";
            public const string Edit = "Permissions.Subscription.Edit";
            public const string Delete = "Permissions.Subscription.Delete";
        }
        public static class SuperAdmin
        {
            public const string Create = "Permissions.SuperAdmin.Create";
            public const string View = "Permissions.SuperAdmin.View";
            public const string Edit = "Permissions.SuperAdmin.Edit";
            public const string Delete = "Permissions.SuperAdmin.Delete";
        }

    }
}
