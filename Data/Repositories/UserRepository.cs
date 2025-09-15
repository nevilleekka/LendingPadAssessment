using BusinessEntities;
using BusinessEntities.Base;
using Common.Attributes;
using Data.DbContexts;
using Data.Repositories.Base;
using Data.Repositories.Interfaces;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;

namespace Data.Repositories;

[AutoRegister]
public class UserRepository : Repository<User>, IUserRepository
{

    private DocumentDbContext DbContext { get; set; }
    private IList<Predicate<User>> Validators { get; set; }

    public UserRepository(DocumentDbContext dbContext) : base(dbContext)
    {
        DbContext = dbContext;




    }

    public IEnumerable<User> Get(UserTypes? userType = null, string name = null, string email = null)
    {


        Func<IDocumentSession, IRavenQueryable<User>> queryFunction = (session) =>
        {

            var finalQuery = session.Query<User>();
            finalQuery = name == null ? finalQuery : finalQuery.Search(user => (user.Name), name);
            finalQuery = email == null ? finalQuery : finalQuery.Search(user => (user.Email), email);
            finalQuery = userType == null ? finalQuery : finalQuery.Search(user => (user.Type.ToString()), userType.ToString()); 

            //finalQuery = name == null ? finalQuery : finalQuery.Where(user => (user.Name.Contains(name)));
            //finalQuery = email == null ? finalQuery : finalQuery.Where(user => (user.Email == email));
            //finalQuery = userType == null ? finalQuery : finalQuery.Where(user => (user.Type.ToString() == userType.ToString()));

            return finalQuery;
        };

        var user = DbContext.Retrieve<User>(queryFunction).ToList();
        return user;
    }

    public override User Get(Guid id)
    {
        Func<IDocumentSession, IRavenQueryable<User>> query = (session) => { return session.Query<User>().Where(user => user.Identifier == id); };

        var user = DbContext.Retrieve<User>(query).FirstOrDefault();
        return user;
    }

    public override IEnumerable<User> GetAll()
    {
        var users = DbContext.RetrieveAll<User>().ToList();
        return users;
    }

    public override int Count()
    {
        int user = DbContext.Count<User>();
        return user;
    }

    public override User Create(User entity)
    {
        try
        {
            entity.Tags = entity.Tags.Select(s => s.ToUpperInvariant()).ToArray();
            User updatedUser = DbContext.Create<User>(entity);
            return updatedUser;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public override User Update(User entity)
    {
        try
        {
            entity.Tags = entity.Tags.Select(s => s.ToUpperInvariant()).ToArray();
            DbContext.Update(entity);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return entity;
    }

    public IEnumerable<User> Get(string[] tags)
    {

        Func<IDocumentSession, IRavenQueryable<User>> query2 = (session) =>
        {
            return session.Query<User>().Where(x => x.Tags.In(tags));
        };

        var users = DbContext.Retrieve(query2);
        return users;
    }


    public override bool Delete(User entity)
    {
        try
        {
            DbContext.Delete(entity);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return true;
    }

    public override int DeleteAll()
    {
        int deletedRows = 0;
        try
        {
            deletedRows = DbContext.Count<User>();
            DbContext.DeleteAll<User>();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return deletedRows;
    }

    public override bool Validate(User entity)
    {
        //entity=  entity==null ? null : entity;


        //  Predicate<User> Email_NullorEmptyCheck = (user) => { return user.Email.IsNullOrEmpty(); };
        //  Predicate<User> Name_NullorEmptyCheck = (user) => { return user.Name.IsNullOrEmpty(); };
        //  Predicate<User> Type_NullCheck = (user) => { return user.Type == null; };

        //  Action<Predicate<User>,string, User> ValidateEmail = (validator, user) => {
        //      if(validator(p)) => { return p(user);}); 
        //      if (valid){ throw new Exception("User Email is null"); }
        //  };



        return true;

    }

}
