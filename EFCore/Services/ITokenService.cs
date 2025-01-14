using EFCore.Data.Models;
using EFCore.Models.Dtos;
using EFCore.Models.Dtos.Auth;
using System.Security.Claims;

namespace EFCore.Services
{
    public interface ITokenService
    {
        Task<AuthDto> GenerateToken(AppUser user);
    }
}
