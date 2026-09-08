using Abp.Authorization;
using Abp.Localization;

namespace CrmTest.Authorization
{
    public class CrmTestAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var pages = context.GetPermissionOrNull("Pages") ?? context.CreatePermission("Pages", L("Pages"));

            // Customer
            pages.CreateChildPermission(PermissionNames.Customer_Read, L("Customer.Read"));
            pages.CreateChildPermission(PermissionNames.Customer_Create, L("Customer.Create"));
            pages.CreateChildPermission(PermissionNames.Customer_Update, L("Customer.Update"));
            pages.CreateChildPermission(PermissionNames.Customer_Delete, L("Customer.Delete"));

            // Note
            pages.CreateChildPermission(PermissionNames.Note_Read, L("Note.Read"));
            pages.CreateChildPermission(PermissionNames.Note_Create, L("Note.Create"));
            pages.CreateChildPermission(PermissionNames.Note_Update, L("Note.Update"));
            pages.CreateChildPermission(PermissionNames.Note_Delete, L("Note.Delete"));

            // RBAC
            pages.CreateChildPermission(PermissionNames.AppUser_Read, L("AppUser.Read"));
            pages.CreateChildPermission(PermissionNames.AppRole_Read, L("AppRole.Read"));
            pages.CreateChildPermission(PermissionNames.AppUser_Create, L("AppUser.Create"));
            pages.CreateChildPermission(PermissionNames.AppRole_Create, L("AppRole.Create"));
            pages.CreateChildPermission(PermissionNames.AppUser_Update, L("AppUser.Update"));
            pages.CreateChildPermission(PermissionNames.AppRole_Update, L("AppRole.Update"));
            pages.CreateChildPermission(PermissionNames.AppUser_Delete, L("AppUser.Delete"));
            pages.CreateChildPermission(PermissionNames.AppRole_Delete, L("AppRole.Delete"));
            pages.CreateChildPermission(PermissionNames.AppRole_AssignPermissions, L("AppRole.AssignPermissions"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, CrmTestConsts.LocalizationSourceName);
        }
    }
}
