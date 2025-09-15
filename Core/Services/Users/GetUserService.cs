using BusinessEntities;
using BusinessEntities.Base;
using Common.Attributes;
using Data.Repositories.Interfaces;

namespace Core.Services.Users;

[AutoRegister]
public class GetUserService : IGetUserService
{
    private readonly IUserRepository _userRepository;

    public GetUserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User GetUser(Guid id)
    {
        try
        {
            return _userRepository.Get(id);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int GetCount()
    {
        int userCount = _userRepository.Count();
        return userCount;
    }

    public IEnumerable<User> GetUsers(UserTypes? userType = null, string name = null, string email = null)
    {
        try
        {
            return _userRepository.Get(userType, name, email);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public IEnumerable<User> GetUsers(UserTypes? userType = null, string name = null, string email = null, string[] tags = null)
    {
        try
        {
            return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public IEnumerable<User> GetUsers(string[] tags)
    {
        try
        {
            return _userRepository.Get(tags);
        }
        catch (Exception ex)
        {
             throw ex;
        }
    }

    public IEnumerable<User> GetAllUsers()
    {
        try
        {
            return _userRepository.GetAll();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
