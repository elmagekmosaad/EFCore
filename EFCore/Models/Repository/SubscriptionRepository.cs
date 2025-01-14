using EFCore.Data.Models;
using EFCore.Models.Repository.Interfaces;
using Web.Api.Data.Context;

namespace EFCore.Models.Repository
{
    public class SubscriptionRepository : GenericRepository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(AppDbContext dbcontext) : base(dbcontext)
        {
        }

        public async Task<IEnumerable<Subscription>> GetByCustomerId(int id)
        {
            var subscriptions = table.Where(c => c.ApplicationUserId.Equals(id));
            return subscriptions;
        }
    }
}
