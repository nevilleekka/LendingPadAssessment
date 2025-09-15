using BusinessEntities;
using BusinessEntities.Base;

namespace Core.Services.Users;

public interface ICreateUserService
{
    User Create(Guid id, string name, string email, UserTypes type, decimal? annualSalary, IEnumerable<string> tags);
}
