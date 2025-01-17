﻿using EFCore.Data.Models;

namespace EFCore.Models.Repository.Interfaces
{
    public interface IComputerRepository : IGenericRepository<Computer>
    {
        Task<IEnumerable<Computer>> GetByCustomerId(int customerId);
        Task<Computer> GetBySubscriptionId(int subscriptionId);
    }
}
