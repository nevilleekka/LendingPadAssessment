using Data;

namespace WebApi.Startup;

public class WebApiStartup : CoreStartup
{
    //   public CoreStartup coreStartup { get; set; }

    public WebApiStartup()
    {
        ConfigureObjectMapping();

    }

    public virtual void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

    }


    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        next = base.Configure(next);
        return next;
    }

    public void ConfigureObjectMapping()
    {
   

    }


}
