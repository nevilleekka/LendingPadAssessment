using BusinessEntities;
using Data.DbContexts;
using Data.Repositories.Interfaces;

namespace Data.Repositories;

public class ProductOrderRepository: iProductOrderRepository
{
    private RelationalDbContext DBContext { get; init; }

    public ProductOrderRepository(RelationalDbContext dbContext)
    {
        this.DBContext = dbContext;

    }

    public Order CreateCustomerProductOrder(string productName, string customerName)
    {
        var product = DBContext.Product.FirstOrDefault(p => p.Name == productName);
        if (product == null)
            throw new Exception($"Product with name '{productName}' not found.");

        var order = new Order
        {
            Customer = customerName,
            ProductId = product.Identifier,
            Products = new List<Product> { product }
        };

        DBContext.Order.Add(order);
        int savedCount = DBContext.SaveChanges();

        return order;
    }


}

