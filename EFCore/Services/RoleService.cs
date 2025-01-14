using EFCore.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Web.Api.Constants;

namespace EFCore.Services
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleService(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task AddUserToRoleAsync(AppUser user, string role)
        {
            if (!await userManager.IsInRoleAsync(user, role))
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }
        public async Task AddUserToRolesAsync(AppUser user, IEnumerable<string> roles)
        {
            await userManager.AddToRolesAsync(user, roles);
            //if (await userManager.IsInRoleAsync(user, Roles.SuperAdmin))
            //{
                
            //    await userManager.AddToRoleAsync(user, Roles.Customer);
            //    await userManager.AddToRoleAsync(user, Roles.Admin);
            //}
            //else if (await userManager.IsInRoleAsync(user, Roles.Admin))
            //{
            //    await userManager.AddToRoleAsync(user, Roles.Customer);
            //}
            //else
            //{
            //    await userManager.AddToRoleAsync(user, Roles.Customer);
            //}

        }

        public async Task<IdentityResult> CreateAsync(IdentityRole identityRole)
        {
            return await roleManager.CreateAsync(identityRole);
        }

        public async Task<IdentityRole> FindByNameAsync(string roleName)
        {
            return await roleManager.FindByNameAsync(roleName);
        }

        public async Task<bool> RoleExistsAsync(string role)
        {
            return await roleManager.RoleExistsAsync(role);
        }
        public async Task AddPermissionClaim(IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == Constants.Permission && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim(Constants.Permission, permission));
                }
            }
        }
    }
}
