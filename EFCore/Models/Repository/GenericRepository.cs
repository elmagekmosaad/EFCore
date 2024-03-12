using EFCore.MySQL.Data;
using Microsoft.EntityFrameworkCore;


public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly MgDbContext _dbcontext = null;
    protected readonly DbSet<T> table = null;

    
    public GenericRepository(MgDbContext dbcontext)
    {
        _dbcontext = dbcontext;
        table = _dbcontext.Set<T>();
    }

    public async Task<int> Add(T entity)
    {
        table.Add(entity);
        return await SaveChangesAsync();
    }

    public async Task<T> GetById(int id)
    {
        return await table.FindAsync(id);
    }

    public Task<int> Update(T entity)
    {
        table.Attach(entity);
        table.Entry(entity).State = EntityState.Modified;
        return SaveChangesAsync();
    }

    public Task<int> Remove(T entity)
    {
        table.Remove(entity);
        return SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return table.ToArray();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbcontext.SaveChangesAsync();
    }

    //public IEnumerable<T> GetAll(Func<IQueryable<T>, IQueryable<T>> includeProperties)
    //{
    //    IQueryable<T> query = table;

    //    if (includeProperties != null)
    //    {
    //        query = includeProperties(table);//ينشئ تضمين جديد في كل مرة
    //        //query = includeProperties(query);//يحفتظ بالتضمين بعد اول مرة
    //    }
    //    return query.ToArray();
    //}
    //public IQueryable<T> Include(Func<IQueryable<T>, IQueryable<T>> includeProperties)
    //{
    //    IQueryable<T> query = table;

    //    if (includeProperties != null)
    //    {
    //        query = includeProperties(query);
    //    }
    //    return query;
    //}
}
