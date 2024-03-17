using EFCore.Data.Models;
using EFCore.Models.Interfaces;
using EFCore.MySQL.Data;

namespace EFCore.Models.Repository
{
    public class SubscriptionRepository : GenericRepository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(ApplicationDbContext dbcontext) : base(dbcontext)
        {
        }

        public async Task<IEnumerable<Subscription>> GetByCustomerId(int id)
        {
            var subscriptions =  table.Where(c=>c.CustomerId == id);
            return subscriptions;
        }
    }
}
