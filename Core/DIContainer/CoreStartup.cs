using BusinessEntities;
using BusinessEntities.Base;
using Common.Attributes;
using Core.Factories;
using Core.Factories.Base;
using Core.Services.Users;
using Data.Connections;
using Data.DbContexts;
using Data.Repositories;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Data;

public class CoreStartup : IStartupFilter
{


    public CoreStartup()
    {

    }

    public void ConfigureServices(IServiceCollection services)
    {

        
        services.AddDbContext<RelationalDbContext>();
        services.AddSingleton<RavenDBConnection>(); 
        services.AddSingleton<DocumentDbContext>();
        services.AddScoped<IUserRepository,UserRepository>();

        services.AddScoped<IIdObjectFactory<IdObject>, IdObjectFactory<User>>();
        services.AddKeyedScoped<IIdObjectFactory<IdObject>, IdObjectFactory<User>>(nameof(User));
        services.AddScoped<IIdObjectFactory<User>, IdObjectFactory<User>>();


        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<iProductRepository,ProductRepository>();
        services.AddScoped<iOrderRepository, OrderRepository>();

        services.AddScoped<ICreateUserService, CreateUserService>();
        services.AddScoped<IUpdateUserService, UpdateUserService>();
        services.AddScoped<IDeleteUserService, DeleteUserService>();
        services.AddScoped<IGetUserService, GetUserService>();


        //    AutoRegistration(this.GetType().Assembly, services);
    }


    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return next;
    }
    
    public void ObjectMappingConfiguration()
    {
      
    }


    private void AutoRegistration(Assembly assembly, IServiceCollection services)
    {
        try
        {
            var typesWithAttribute = from type in assembly.GetTypes()
                                     where Attribute.IsDefined(type, typeof(AutoRegisterAttribute))
                                     select type;


            foreach (var attributedType in typesWithAttribute)
            {

                Type serviceInterface = attributedType.GetInterfaces().Where(intf => intf.Name.Contains(attributedType.Name)).First();

                services.AddScoped(attributedType, serviceInterface);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
       
    }

 
}
