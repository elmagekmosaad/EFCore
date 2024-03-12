using EFCore.Data.Enums;
using EFCore.Data.Models;
using EFCore.Models.Interfaces;
using EFCore.MySQL.Data;
using EFCore.MySQL.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace EFCore.Models.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {

        public CustomerRepository(MgDbContext dbcontext) : base(dbcontext)
        {
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersWithSubscriptions()
        {
            var customersWithSubscriptions = table.Include(s => s.Subscriptions);
            return customersWithSubscriptions;
        }

        public async Task<IEnumerable<Customer>> GetByGender(Gender Gender)
        {
            var CustomersByGender = _dbcontext.Customers.Where(x => x.Gender == Gender);
            return CustomersByGender;
        }
        [HttpGet("[action]/{id}")]
        public  Task<Customer> ReadWithSubscriptions(int id)
        {
            var customerWithSubscriptions = table.Include(s => s.Subscriptions).SingleOrDefault(c => c.Id == id);
            return Task.FromResult(customerWithSubscriptions);
        }

    }
}
