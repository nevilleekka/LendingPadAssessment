using BusinessEntities;
using BusinessEntities.Base;
using Common.Attributes;
using Core.Factories.Base;
using Data.Repositories.Interfaces;

namespace Core.Services.Users;

[AutoRegister]
public class CreateUserService : ICreateUserService
{
    

    private readonly IUpdateUserService _updateUserService;
    private readonly IIdObjectFactory<User> _userFactory;
    private readonly IUserRepository _userRepository;

    public CreateUserService(IIdObjectFactory<IdObject> userFactory, IUserRepository userRepository, IUpdateUserService updateUserService)
    {
        _userFactory = (IIdObjectFactory<User>)userFactory;
        _updateUserService = updateUserService;
        _userRepository = userRepository;
    }

    public User Create(Guid id, string name, string email, UserTypes type, decimal? annualSalary, IEnumerable<string> tags)
    {
        try
        {
            var user = _userFactory.Create(id);
            _updateUserService.Populate(ref user, name, email, type, annualSalary, tags);
            var userCreated = _userRepository.Create(user);
            return user;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


}
