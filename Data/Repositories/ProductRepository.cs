using BusinessEntities;
using Data.DbContexts;
using Data.Repositories.Base;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProductRepository : Repository<Product>, iProductRepository
{
    protected RelationalDbContext DBContext { get; init; }

    public ProductRepository(RelationalDbContext relationalDbContext) : base(relationalDbContext)
    {
        DBContext = new RelationalDbContext();
    }


    public override Product Get(Guid id)
    {
        Product products = DBContext.Product.Where(order => order.Identifier == id).FirstOrDefault();
        return products;
    }


    public override Product Create(Product entity)
    {

        DBContext.Product.Add(entity);
        int savedCount = DBContext.SaveChanges();

        if (savedCount > 0)
        {
            return entity;
        }
        else { throw new Exception($"Unable to {nameof(Create)}"); }

    }

    public Product Get(string productName)
    {
        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Product name must not be empty.", nameof(productName));

        var product = DBContext.Product.FirstOrDefault(p => p.Name == productName);
        if (product == null) { throw new Exception($"Product with name '{productName}' not found."); }
        return product;
    }


    public override int Count()
    {
        int count = DBContext.Product.Count();
        return count;
    }

    public override Product Update(Product entity)
    {
        try
        {
            Product product = DBContext.Product.Where(product => product.Id == entity.Id).First();
            product = entity;
            DBContext.SaveChanges();
            return product;
        }
        catch (Exception ex)
        {
            throw new Exception($"Could not {nameof(Update)} {nameof(Product)}", ex);
        }
    }

    public override bool Delete(Product entity)
    {

        try
        {
            Product product = DBContext.Product.Where(product => product.Id == entity.Id).First();
            DBContext.Product.Remove(product);
            DBContext.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Could not {nameof(Delete)} : {nameof(Product)}", ex);
        }
    }




    public override IEnumerable<Product> GetAll()
    {
        IEnumerable<Product> product = DBContext.Product;
        return product;
    }

    public override int DeleteAll()
    {

        int deletedCount = DBContext.Product.ExecuteDelete();
        return deletedCount;
    }

    public override bool Validate(Product entity)
    {
        throw new NotImplementedException();
    }
}

