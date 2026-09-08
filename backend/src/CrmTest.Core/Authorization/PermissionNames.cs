namespace CrmTest.Authorization
{
    public static class PermissionNames
    {
        public const string Pages = "Pages";

        // Customer
        public const string Customer_Read = "Customer.Read";
        public const string Customer_Create = "Customer.Create";
        public const string Customer_Update = "Customer.Update";
        public const string Customer_Delete = "Customer.Delete";

        // Note
        public const string Note_Read = "Note.Read";
        public const string Note_Create = "Note.Create";
        public const string Note_Update = "Note.Update";
        public const string Note_Delete = "Note.Delete";

        // RBAC management
        public const string AppUser_Read = "AppUser.Read";
        public const string AppRole_Read = "AppRole.Read";
        public const string AppUser_Create = "AppUser.Create";
        public const string AppRole_Create = "AppRole.Create";
        public const string AppUser_Update = "AppUser.Update";
        public const string AppRole_Update = "AppRole.Update";
        public const string AppUser_Delete = "AppUser.Delete";
        public const string AppRole_Delete = "AppRole.Delete";
        public const string AppRole_AssignPermissions = "AppRole.AssignPermissions";

    }
}
