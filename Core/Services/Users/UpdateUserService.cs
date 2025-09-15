using BusinessEntities;
using BusinessEntities.Base;
using Common;
using Common.Attributes;
using Data.Repositories.Interfaces;

namespace Core.Services.Users;

[AutoRegister(AutoRegisterTypes.Singleton)]
public class UpdateUserService : IUpdateUserService
{
    private IUserRepository Repository { get; init; }

    public UpdateUserService(IUserRepository repository)
    {
        Repository = repository;
    }


    public User Update(User user)
    {
        try
        {
            User updatedUser = Repository.Update(user);
            return updatedUser;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public User Update(ref User user, string name, string email, UserTypes type, decimal? annualSalary, IEnumerable<string> tags)
    {
        try
        {
            user.Email = email;
            user.Name = name;
            user.Type = type;
            user.MonthlySalary = (annualSalary==null)? 0: (annualSalary.Value / 12);
            user.Tags = tags?? Array.Empty<string>();

            User updatedUser = Update(user);
            return updatedUser;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void Populate(ref User user, string name, string email, UserTypes type, decimal? annualSalary, IEnumerable<string> tags)
    {
        user.Email = email;
        user.Name = name;
        user.Type = type;
        user.MonthlySalary = (annualSalary == null) ? 0 : (annualSalary.Value / 12);
        user.Tags = tags;
    }
}
