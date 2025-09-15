using BusinessEntities;

namespace Data.Repositories.Interfaces;

public interface iProductRepository
{
    Product Get(Guid id);
    Product Get(string productName);
    IEnumerable<Product> GetAll();
    Product Create(Product entity);
    int Count();
    Product Update(Product entity);
    bool Delete(Product entity); 
    int DeleteAll();
}

