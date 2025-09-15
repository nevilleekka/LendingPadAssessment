using BusinessEntities;
using BusinessEntities.Base;
using Data.DbContexts.Base;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Base;

public abstract class Repository<T> : IRepository<T> where T : IdObject
{
    private iDbContext DBContext { get; set; }

    public Repository(iDbContext dbContext)
    {
        DBContext = dbContext;
    }

    public abstract T Create(T entity);

    public abstract int Count();

    public abstract T Update(T entity);

    public abstract bool Delete(T entity);

    public abstract T Get(Guid id);

    public abstract IEnumerable<T> GetAll();

    public abstract int DeleteAll();

    public abstract bool Validate(T entity);
}
