using EFCore.Data.Models;
using EFCore.Models;
using EFCore.Models.Dtos;
using EFCore.Models.Dtos.Auth;
using EFCore.MySQL.Models.Dto;
using MG.Shared;

namespace EFCore.Services
{
    public interface IAuthService
    {
        Task<BaseResponse> LogIn(LoginDto user);
        Task<BaseResponse> Register(RegisterDto user);
        //Task<IBaseResponse> LogIn(CustomerLoginDto customerLoginDto);
        //Task<IBaseResponse> Register(CustomerRegisterDto customerRegisterDto);
    }
}
