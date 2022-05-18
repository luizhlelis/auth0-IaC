using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SampleApp.Web;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(new OidcClientOptions
        {
            Authority = $"https://{Configuration["Auth0:Domain"]}",
            ClientId = Configuration["Auth0:ClientId"],
            ClientSecret = Configuration["Auth0:ClientSecret"],
            RedirectUri = Configuration["Auth0:RedirectUri"],
            Scope = Configuration["Auth0:Scope"],
        });

        services.AddDistributedMemoryCache();
        services.AddSession(options =>
        {
            options.Cookie.Name = ".SampleApp.Web.Session";
            options.IdleTimeout = TimeSpan.FromSeconds(120);
            options.Cookie.HttpOnly = false;
            options.Cookie.IsEssential = true;
        });
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
            options =>
            {
                options.Cookie.Name = ".SampleApp.Web.Authentication";
            });
        services.AddControllersWithViews();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
