using EFCore.Data.Models;
using EFCore.Models.Dtos;
using EFCore.MySQL.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Web.Api.Data.Entities.Enums;

namespace EFCore.Models.Repository.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<AppUser>
    {
        //Task<IBaseResponse> LogIn(Customer user);
        //Task<IBaseResponse> Register(Customer user);
        Task<AppUser> GetByEmail(string email,string Hwid);
        Task<IEnumerable<AppUser>> GetByGender(CustomerGender Gender);
        Task<AppUser> ReadWithSubscriptions(int id);
        Task<IEnumerable<AppUser>> GetAllCustomersWithSubscriptions();
    }
}
