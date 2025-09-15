using BusinessEntities;
using Data.DbContexts;
using Data.Repositories.Base;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class OrderRepository : Repository<Order>, iOrderRepository
{
    protected RelationalDbContext DBContext;

    public OrderRepository(RelationalDbContext dbContext) : base(dbContext)
    {
        DBContext = dbContext;
    }



    public override Order Get(Guid id)
    {
        Order order = DBContext.Order.FirstOrDefault(order => order.Identifier == id);
        return order;

    }

    public Order Get(string customer)
    {
        Order rootOrder = DBContext.Order.First(order => order.Customer == customer);

        var products = (from order in DBContext.Order
                        join product in DBContext.Product
                        on order.ProductId equals product.Identifier
                        where order.Customer == customer
                        select product).ToList();

        rootOrder.Products = products;

        return rootOrder;
    }




    public override Order Create(Order entity)
    {
        try
        {
            DBContext.Order.Add(entity);
            DBContext.SaveChanges();
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"Could not {nameof(Create)} {nameof(Order)}", ex);
        }
    }

    public override int Count()
    {
        int count = DBContext.Order.Count();
        return count;
    }

    public override Order Update(Order entity)
    {
        try
        {
            Order order = DBContext.Order.Where(order => order.Id == entity.Id).First();
            order.Customer = entity.Customer;
            order.ProductId = entity.ProductId;
            DBContext.SaveChanges();
            return order;
        }
        catch (Exception ex)
        {
            throw new Exception($"Could not {nameof(Update)}  {nameof(Update)}", ex);
        }
    }

    public override bool Delete(Order entity)
    {
        Order order = DBContext.Order.Where(order => order.Id == entity.Id).FirstOrDefault();
        DBContext.Order.Remove(order);
        DBContext.SaveChanges();
        return true;
    }



    public override IEnumerable<Order> GetAll()
    {
        IEnumerable<Order> orders = DBContext.Order.ToList();
        return orders;
    }


    public bool Delete(Guid id)
    {
        Order order = DBContext.Order.Where(order => order.Identifier == id).FirstOrDefault();
        if (order != null)
        {
            DBContext.Order.Remove(order);
            DBContext.SaveChanges();
            return true;
        }
        return false;
    }

    public int Delete(string customer)
    {
        IEnumerable<Order> orders = DBContext.Order.Where(order => order.Customer == customer);
        int deletedCount = orders.Count();
        DBContext.Order.RemoveRange(orders);
        DBContext.SaveChanges();
        return deletedCount;
    }

    public override int DeleteAll()
    {
        int deletedCount = DBContext.Order.ExecuteDelete();
        return deletedCount;
    }



    public override bool Validate(Order entity)
    {
        throw new NotImplementedException();
    }


}
