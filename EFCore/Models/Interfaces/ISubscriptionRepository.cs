using EFCore.Data.Models;

namespace EFCore.Models.Interfaces
{
    public interface ISubscriptionRepository:IGenericRepository<Subscription>
    {

        /// <summary>
        /// Retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The retrieved entity, or null if not found.</returns>
        Task<IEnumerable<Subscription>> GetByCustomerId(int id);
    }
}
