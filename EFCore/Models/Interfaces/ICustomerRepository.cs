using EFCore.Data.Enums;
using EFCore.Data.Models;
using EFCore.MySQL.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Models.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetByGender(Gender Gender);
        Task<Customer> ReadWithSubscriptions(int id);
        Task<IEnumerable<Customer>> GetAllCustomersWithSubscriptions();
    }
}
