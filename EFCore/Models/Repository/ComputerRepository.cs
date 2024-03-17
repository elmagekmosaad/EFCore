using EFCore.Data.Models;
using EFCore.Models.Interfaces;
using EFCore.MySQL.Data;

namespace EFCore.Models.Repository
{
    public class ComputerRepository : GenericRepository<Computer>, IComputerRepository
    {
        public ComputerRepository(ApplicationDbContext dbcontext) : base(dbcontext)
        {
        }

        public async Task<IEnumerable<Computer>> GetByCustomerId(int customerId)
        {
            var computers = table.Where(c => c.CustomerId.Equals(customerId)).AsEnumerable();
            return computers;
        }

        public async Task<Computer> GetBySubscriptionId(int subscriptionId)
        {
            var computer = table.SingleOrDefault(s=>s.SubscriptionId.Equals(subscriptionId));
            return computer;
        }


    }
}
