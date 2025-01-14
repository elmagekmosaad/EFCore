using AutoMapper;
using EFCore.Data.Models;
using EFCore.Services;
using Microsoft.AspNetCore.Identity;

namespace Web.Api.Seeds
{
    public class Seed
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IRoleService roleService;
        private readonly IMapper mapper;


        public Seed(UserManager<AppUser> userManager, IRoleService roleService, IMapper mapper)
        {
            this.userManager = userManager;
            this.roleService = roleService;
            this.mapper = mapper;
        }

        public async Task InitializeAsync()
        {
            await new DefaultRoles(roleService).Initialize();
            await new DefaultUsers(userManager, roleService, mapper).Initialize();
        }
    }
}