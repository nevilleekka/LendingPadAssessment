using BusinessEntities;
using BusinessEntities.Base;

namespace Core.Services.Users;

public interface IGetUserService
{
    User GetUser(Guid id);
    IEnumerable<User> GetUsers(string[] tags);
    IEnumerable<User> GetAllUsers();
    int GetCount();
    IEnumerable<User> GetUsers(UserTypes? userType = null, string name = null, string email = null);
    IEnumerable<User> GetUsers(UserTypes? userType = null, string name = null, string email = null, string[] tags = null);

}
