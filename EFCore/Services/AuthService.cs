using AutoMapper;
using EFCore.Data.Models;
using EFCore.Models;
using EFCore.Models.Dtos.Auth;
using MG.Shared;
using Microsoft.AspNetCore.Identity;
using Web.Api.Constants;
using Web.Api.Data.Entities.Enums;

namespace EFCore.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly ITokenService tokenService;
        private readonly IRoleService roleService;
        private readonly IMapper mapper;
        public AuthService(UserManager<AppUser> userManager, IRoleService roleService, ITokenService tokenService,IMapper mapper)
        {
            this.userManager = userManager;
            this.roleService = roleService;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }
        public async Task<BaseResponse> LogIn(LoginDto loginDto)
        {
            BaseResponse result;
            AppUser? user = (loginDto.Email.Contains('@')
                                        ? await userManager.FindByEmailAsync(loginDto.Email)
                                        : await userManager.FindByNameAsync(loginDto.UserName));

            if (user is null)
            {
                result = new BaseResponse(Succeeded: false, Message: "invalid email or username", Data: null);
                return result;
            }

            if (await userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                AuthDto authDto = await tokenService.GenerateToken(user);

                result = new BaseResponse(Succeeded: true, Message: "Successfully Logged In", Data: authDto);
                return result;
            }
            else
            {
                result = new BaseResponse(Succeeded: false, Message: "invalid password", Data: null);
                return result;
            }

        }

        public async Task<BaseResponse> Register(RegisterDto registerDto)
        {
            AppUser user = mapper.Map<AppUser>(registerDto);
            
            if(await userManager.FindByEmailAsync(user.Email) is not null)
            {
                return new(Message: "Email Already Registered");
            }
             if(await userManager.FindByEmailAsync(user.UserName) is not null)
            {
                return new(Message: "UserName Already Registered");
            }

            IdentityResult identity = await userManager.CreateAsync(user, registerDto.Password);
            if (identity.Succeeded)
            {
                await roleService.AddUserToRoleAsync(user, Roles.Customer);
                var token = await tokenService.GenerateToken(user);

                return new BaseResponse(Succeeded: true, Message: "Successfully Registered", Data: token);
            }
            else
            {
                return new BaseResponse(Succeeded: false, Message: "Something went wrong", Data: identity.Errors);
            }
        }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Facebook { get; set; }
        public CustomerType Type { get; set; } = CustomerType.Person;
        public CustomerGender Gender { get; set; } = CustomerGender.UnKnown;
        public string Country { get; set; }
        public string Admin { get; set; }
        public string Comments { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

        //public async Task<IBaseResponse> Register(RegisterDto registerDto)
        //{
        //    BaseResponse result;
        //    AppUser user = new()
        //    {
        //        UserName = registerDto.UserName,
        //        Email = registerDto.Email,
        //        PhoneNumber = registerDto.PhoneNumber,
        //        //new()
        //        //{
        //        //    Name = registerDto.Name,
        //        //    UserName = registerDto.UserName,
        //        //    Email = registerDto.Email,
        //        //    Password = registerDto.Password,
        //        //    PhoneNumber = registerDto.PhoneNumber,
        //        //    gen = registerDto.Facebook,
        //        //};
        //    };
        //    IdentityResult identity = await userManager.CreateAsync(user, registerDto.Password);
        //    if (identity.Succeeded)
        //    {
        //        await roleService.AddUserToRoleAsync(user, Roles.Customer);
        //        var token = await tokenService.GenerateToken(user);

        //        result = new BaseResponse(Succeeded: true, Message: "Successfully Registered", Data: token);
        //        return result;
        //    }
        //    else
        //    {
        //        result = new BaseResponse(Succeeded: false, Message: string.Join(',', identity.Errors), Data: identity.Errors);
        //        return result;
        //    }
        //}

      
    }
}
