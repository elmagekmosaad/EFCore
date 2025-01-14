using EFCore.Data.Models;
using EFCore.Models.Repository.Interfaces;
using Web.Api.Data.Context;

namespace EFCore.Models.Repository
{
    public class ComputerRepository : GenericRepository<Computer>, IComputerRepository
    {
        public ComputerRepository(AppDbContext dbcontext) : base(dbcontext)
        {
        }

        public async Task<IEnumerable<Computer>> GetByCustomerId(int customerId)
        {
            var computers = table.Where(c => c.ApplicationUserId.Equals(customerId)).AsEnumerable();
            return computers;
        }

        public async Task<Computer> GetBySubscriptionId(int subscriptionId)
        {
            var computer = table.SingleOrDefault(s=>s.SubscriptionId.Equals(subscriptionId));
            return computer;
        }


    }
}
