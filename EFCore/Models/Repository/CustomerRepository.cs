using EFCore.Data.Models;
using EFCore.Models.Repository.Interfaces;
using EFCore.MySQL.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Web.Api.Data.Context;
using Web.Api.Data.Entities.Enums;

namespace EFCore.Models.Repository
{
    public class CustomerRepository : GenericRepository<AppUser>, ICustomerRepository
    {

        public CustomerRepository(AppDbContext dbcontext) : base(dbcontext)
        {
        }

        public async Task<IEnumerable<AppUser>> GetAllCustomersWithSubscriptions()
        {
            var customersWithSubscriptions = table.Include(s => s.Subscriptions);
            return customersWithSubscriptions;
        }

        public async Task<AppUser> GetByEmail(string email, string Hwid)
        {
            //var customerWithSubscriptions = table.Include(s => s.Subscriptions).SingleOrDefault(c => c.Email.Equals(email));

            //var result = customerWithSubscriptions?.Subscriptions.Where(s => s.Computer.Hwid.Equals(Hwid)).FirstOrDefault();


            //return await ReadWithSubscriptions(result.Id);
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AppUser>> GetByGender(CustomerGender Gender)
        {
            var CustomersByGender = table.Where(x => x.Gender == Gender);
            return CustomersByGender;
        }


        [HttpGet("[action]/{id}")]
        public Task<AppUser> ReadWithSubscriptions(int id)
        {
            var customerWithSubscriptions = table.Include(s => s.Subscriptions).SingleOrDefault(c => c.Id.Equals(id));
            return Task.FromResult(customerWithSubscriptions);
        }

    }
}
