using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
public interface IGenericRepository<T> where T : class
{
    /// <summary>
    /// Adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    Task<int> Add(T entity);

    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>The retrieved entity, or null if not found.</returns>
    Task<T> GetById(string id);

    /// <summary>
    /// Asynchronously updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>The number of affected records.</returns>
    Task<int> Update(T entity);

    /// <summary>
    /// Asynchronously removes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to remove.</param>
    /// <returns>The number of affected records.</returns>
    Task<int> Remove(T entity);

    /// <summary>
    /// Asynchronously retrieves all entities from the repository.
    /// </summary>
    /// <returns>A collection of all entities in the repository.</returns>
    Task<IEnumerable<T>> GetAll();

    ///// <summary>
    ///// Asynchronously retrieves all entities from the repository, with the option to include related properties.
    ///// </summary>
    ///// <param name="includeProperties">A function to specify which related properties to include.</param>
    ///// <returns>A collection of all entities in the repository, with the specified related properties included.</returns>
    //IEnumerable<T> GetAll(Func<IQueryable<T>, IQueryable<T>> includeProperties);

    ///// <summary>
    ///// Asynchronously retrieves all entities from the repository, with the option to include related properties.
    ///// </summary>
    ///// <param name="includeProperties">A function to specify which related properties to include.</param>
    ///// <returns>A collection of all entities in the repository, with the specified related properties included.</returns>
    //IQueryable<T> Include(Func<IQueryable<T>, IQueryable<T>> includeProperties);

    /// <summary>
    /// Saves changes made to the repository synchronously.
    /// </summary>
    /// <returns>The number of affected records.</returns>
    //int SaveChanges();
    /// <summary>
    /// Saves changes made to the repository synchronously.
    /// </summary>
    /// <returns>The number of affected records.</returns>
    Task<int> SaveChangesAsync();
}
