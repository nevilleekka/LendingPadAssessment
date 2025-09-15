using BusinessEntities;
using BusinessEntities.Base;

namespace Core.Services.Users;

public interface IUpdateUserService
{
    void Populate(ref User user, string name, string email, UserTypes type, decimal? annualSalary, IEnumerable<string> tags);
    User Update(ref User user, string name, string email, UserTypes type, decimal? annualSalary, IEnumerable<string> tags);
    User Update(User user);
}
