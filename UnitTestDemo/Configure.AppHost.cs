using Funq;
using ServiceStack.Auth;
using UnitTestDemo.ServiceInterface;

[assembly: HostingStartup(typeof(UnitTestDemo.AppHost))]

namespace UnitTestDemo;

public class AppHost : AppHostBase, IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices((context, services) =>
        {
            services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = context.Configuration.GetSection("IdentityServer")["Authority"];
                options.RequireHttpsMetadata = false;
                options.Audience = context.Configuration.GetSection("IdentityServer")["Audience"];
            });
        });

    public AppHost() : base("UnitTestDemo", typeof(MyServices).Assembly) { }

    public override void Configure(Container container)
    {
        // Configure ServiceStack only IOC, Config & Plugins
        SetConfig(new HostConfig
        {
            UseSameSiteCookies = true,
        });
        Plugins.Add(new AuthFeature(() => new AuthUserSession(),
            new IAuthProvider[]
            {
                new NetCoreIdentityAuthProvider(AppSettings)
            }));
    }
}
