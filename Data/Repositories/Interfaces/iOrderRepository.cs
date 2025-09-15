using BusinessEntities;

namespace Data.Repositories.Interfaces;

public interface iOrderRepository
{
    Order Get(Guid id);
    Order Get(string customer);
    Order Create(Order entity);
    int Count();
    Order Update(Order entity);
    bool Delete(Order entity);
    IEnumerable<Order> GetAll();
    bool Delete(Guid id);
    int Delete(string customer);
    int DeleteAll();
    bool Validate(Order entity);
}

