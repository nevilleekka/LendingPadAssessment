using BusinessEntities;
using BusinessEntities.Base;

namespace Data.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    IEnumerable<User> Get(string[] tags);
    IEnumerable<User> GetAll();
    IEnumerable<User> Get(UserTypes? userType = null, string name = null, string email = null);
    int Count();
    int DeleteAll();
}
