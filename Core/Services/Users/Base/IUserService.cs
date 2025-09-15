using Data.Repositories.Interfaces;

namespace Core.Services.Users;

public interface IUserService
{
    IUserRepository UserRepository { get; }
}

