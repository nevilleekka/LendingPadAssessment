using BusinessEntities;
using Common.Attributes;
using Data.Repositories.Interfaces;

namespace Core.Services.Users;

[AutoRegister]
public class DeleteUserService : IDeleteUserService
{
    protected IUserRepository _userRepository { get; init; }

    public DeleteUserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public void Delete(User user)
    {
        _userRepository.Delete(user);
    }

    public void DeleteAll()
    {
        _userRepository.DeleteAll();
    }
}
