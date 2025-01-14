using EFCore.Data.Models;
using EFCore.Models.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web.Api.Authorization;
using Web.Api.Constants;

namespace EFCore.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtConfiguration jwt;
        private readonly UserManager<AppUser> userManager;
        private readonly IRoleService roleService;

        public TokenService(IOptions<JwtConfiguration> jwt, UserManager<AppUser> userManager, IRoleService roleService)
        {
            this.jwt = jwt.Value;
            this.userManager = userManager;
            this.roleService = roleService;
        }
        public async Task<AuthDto> GenerateToken(AppUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roleClaims = new List<Claim>();

            //foreach (var role in await userManager.GetRolesAsync(user))//not work in another project
            //{
            //    roleClaims.Add(new Claim("roles", role));
            //}
            foreach (var role in await userManager.GetRolesAsync(user))
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var claims = new List<Claim>()
                        {
                            new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Email,user.Email),
                            new Claim(ClaimTypes.NameIdentifier,user.Id),//to use name from (ClaimsIdentity)User.Identity
                            new Claim(ClaimTypes.Name,user.Name),//to use name from (ClaimsIdentity)User.Identity
                            new Claim("Uid",user.Id),
                            new Claim("App","MG-Control")
                        }
            .Union(userClaims)
            .Union(roleClaims);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(jwt.DurationInDays),
                signingCredentials: signingCredentials
                );

            //var accsessToken = new
            //{
            //    token = "Bearer " + new JwtSecurityTokenHandler().WriteToken(securityToken),
            //    expiration = securityToken.ValidTo
            //};
            AuthDto authDto = new AuthDto();
            authDto.Email = user.Email;
            authDto.UserName = user.UserName;
            authDto.IsAuthenticated = true;
            authDto.ExpiresOn = securityToken.ValidTo;
            //authDto.Token = "Bearer " + new JwtSecurityTokenHandler().WriteToken(securityToken);
            authDto.Token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            authDto.Roles = string.Join(',', securityToken.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c=>c.Value));
            authDto.Claims = securityToken.Claims;
            return authDto;
        }
    }
}
