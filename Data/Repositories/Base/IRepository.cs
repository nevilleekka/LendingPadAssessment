using BusinessEntities.Base;

namespace Data.Repositories;

public interface IRepository<T> where T : IdObject
{
    T Create(T entity);
    T Update(T entity);
    bool Delete(T entity);
    T Get(Guid id);
    IEnumerable<T> GetAll();
    int Count();
}
