using AutoMapper;
using EFCore.Data.Models;
using EFCore.Models.Dtos;
using EFCore.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Web.Api.Constants;

namespace Web.Api.Seeds
{
    public class DefaultUsers
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IRoleService roleService;
        private readonly IMapper mapper;

        public DefaultUsers(UserManager<AppUser> userManager, IRoleService roleService, IMapper mapper)
        {
            this.userManager = userManager;
            this.roleService = roleService;
            this.mapper = mapper;
        }
        public async Task Initialize()
        {
            await defaultSuperAdmin();
            await defaultAdmin();
            await defaultCustomer();
        }

        private async Task defaultSuperAdmin()
        {
            var defaultSuperAdmin = new AppUser
            {
                EmailConfirmed = true,
                Name = DefaultSuperAdmin.Name,
                UserName = DefaultSuperAdmin.UserName,
                Email = DefaultSuperAdmin.Email,
                PhoneNumber = DefaultSuperAdmin.PhoneNumber,
                Facebook = DefaultSuperAdmin.Facebook,
                Type = DefaultSuperAdmin.Type,
                Gender = DefaultSuperAdmin.Gender,
                Country = DefaultSuperAdmin.Country,
                Admin = DefaultSuperAdmin.Admin,
                Comments = DefaultSuperAdmin.Comments
            };

            defaultSuperAdmin = await CreateUserAsync(defaultSuperAdmin, DefaultSuperAdmin.Password);

            await roleService.AddUserToRolesAsync(defaultSuperAdmin, Roles.ToList());

            await SeedClaimsForSuperAdmin();
        }

        private async Task SeedClaimsForSuperAdmin()
        {
            var adminRole = await roleService.FindByNameAsync(Roles.SuperAdmin);
            await roleService.AddPermissionClaim(adminRole, "Products");
        }


        private async Task defaultAdmin()
        {
            var defaultAdmin = new AppUser
            {
                EmailConfirmed = true,
                Name = DefaultAdmin.Name,
                UserName = DefaultAdmin.UserName,
                Email = DefaultAdmin.Email,
                PhoneNumber = DefaultAdmin.PhoneNumber,
                Facebook = DefaultAdmin.Facebook,
                Type = DefaultAdmin.Type,
                Gender = DefaultAdmin.Gender,
                Country = DefaultAdmin.Country,
                Admin = DefaultAdmin.Admin,
                Comments = DefaultAdmin.Comments
            };

            defaultAdmin = await CreateUserAsync(defaultAdmin, DefaultAdmin.Password);
            await roleService.AddUserToRolesAsync(defaultAdmin, [Roles.Admin, Roles.Customer]);
        }
        private async Task defaultCustomer()
        {
            var defaultCustomer = new AppUser
            {
                EmailConfirmed = true,
                Name = DefaultCustomer.Name,
                UserName = DefaultCustomer.UserName,
                Email = DefaultCustomer.Email,
                PhoneNumber = DefaultCustomer.PhoneNumber,
                Facebook = DefaultCustomer.Facebook,
                Type = DefaultCustomer.Type,
                Gender = DefaultCustomer.Gender,
                Country = DefaultCustomer.Country,
                Admin = DefaultCustomer.Admin,
                Comments = DefaultCustomer.Comments
            };

            defaultCustomer = await CreateUserAsync(defaultCustomer, DefaultCustomer.Password);
            await roleService.AddUserToRoleAsync(defaultCustomer, Roles.Customer);
        }
        private async Task<AppUser> CreateUserAsync(AppUser user, string password)
        {
            AppUser result = await userManager.FindByEmailAsync(user.Email);
            if (result is null)
            {
                await userManager.CreateAsync(user, password);
                return user;
            }
            return result;
        }

    }
}
