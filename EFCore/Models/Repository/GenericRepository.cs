using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using Web.Api.Data.Context;


public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly AppDbContext _dbcontext = null;
    protected readonly DbSet<T> table = null;
    
    public GenericRepository(AppDbContext dbcontext)
    {
        _dbcontext = dbcontext;
        table = _dbcontext.Set<T>();
    }

    public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
    {
        IQueryable<T> query = table;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return orderBy(query).ToList();
        }
        else
        {
            return query.ToList();
        }
    }

    public async Task<int> Add(T entity)
    {
        table.Add(entity);
        return await SaveChangesAsync();
    }

    public async Task<T> GetById(string id)
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
        return table;
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
