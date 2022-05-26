using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

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
        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(
                options =>
                {
                    options.Cookie.Name = ".SampleApp.Web.Authentication";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.SameSite = SameSiteMode.Strict;
                })
            .AddOpenIdConnect("Auth0", options => ConfigureOpenIdConnect(options));
        services.AddControllersWithViews();
    }

    private void ConfigureOpenIdConnect(OpenIdConnectOptions options)
    {
        options.Authority = $"https://{Configuration["Auth0:Domain"]}";
        options.ClientId = Configuration["Auth0:ClientId"];
        options.ClientSecret = Configuration["Auth0:ClientSecret"];
        options.CallbackPath = new PathString(Configuration["Auth0:RedirectUri"]);
        options.ClaimsIssuer = "Auth0";

        // Set response type to code
        options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
        options.ResponseMode = OpenIdConnectResponseMode.FormPost;

        options.Scope.Clear();
        var scopeArray = Configuration["Auth0:Scope"].Split(',');
        foreach (var scope in scopeArray)
            options.Scope.Add(scope);

        services.AddDistributedMemoryCache();
        services.AddSession(options =>
        // This saves the tokens in the session cookie
        options.SaveTokens = true;

        options.Events = new OpenIdConnectEvents
        {
            options.Cookie.Name = ".SampleApp.Web.Session";
            options.IdleTimeout = TimeSpan.FromSeconds(120);
            options.Cookie.HttpOnly = false;
            options.Cookie.IsEssential = true;
        });
            // handle the logout redirection
            OnRedirectToIdentityProviderForSignOut = (context) =>
            {
                var logoutUri =
                    $"https://{Configuration["Auth0:Domain"]}/v2/logout?client_id={Configuration["Auth0:ClientId"]}";

                var postLogoutUri = context.Properties.RedirectUri;
                if (!string.IsNullOrEmpty(postLogoutUri))
                {
                    if (postLogoutUri.StartsWith("/"))
                    {
                        // transform to absolute
                        var request = context.Request;
                        postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase +
                                        postLogoutUri;
                    }

                    logoutUri += $"&returnTo={Uri.EscapeDataString(postLogoutUri)}";
                }

                context.Response.Redirect(logoutUri);
                context.HandleResponse();

                return Task.CompletedTask;
            }
        };
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
