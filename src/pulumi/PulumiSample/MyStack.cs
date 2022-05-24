using Pulumi;
using Pulumi.Auth0;
using Pulumi.Auth0.Inputs;

namespace PulumiSample;

public class MyStack : Stack
{
    public MyStack()
    {
        var myBrand = new Branding("auth_tenant_branding", new BrandingArgs
        {
            Colors = new BrandingColorsArgs
            {
                PageBackground = "#feda4a",
                Primary = "#805ac3",
            },
            LogoUrl = "https://www.pulumi.com/images/logo/logo-on-black.svg"
        });

        var myClient = new Client("auth0_sample_app_client", new ClientArgs
        {
            LogoUri = "https://www.pulumi.com/images/logo/logo-on-black.svg",
            Name = Environment.GetEnvironmentVariable("CLIENT_NAME"),
            Description = Environment.GetEnvironmentVariable("CLIENT_DESCRIPTION"),
            CustomLoginPageOn = true,
            IsFirstParty = true,
            IsTokenEndpointIpHeaderTrusted = true,
            OidcConformant = false,
            TokenEndpointAuthMethod = "client_secret_post",
            AllowedLogoutUrls =
            {
                Environment.GetEnvironmentVariable("ALLOWED_LOGOUT_URLS")
            },
            AllowedOrigins =
            {
                Environment.GetEnvironmentVariable("ALLOWED_ORIGINS")
            },
            AppType = "regular_web",
            Callbacks =
            {
                Environment.GetEnvironmentVariable("CALLBACK")
            },
            GrantTypes =
            {
                Environment.GetEnvironmentVariable("GRANT_TYPES")
            },
            WebOrigins =
            {
                Environment.GetEnvironmentVariable("WEB_ORIGINS")
            },
            JwtConfiguration = new ClientJwtConfigurationArgs
            {
                Alg = "RS256",
                LifetimeInSeconds = 300,
                Scopes =
                {
                    { "foo", "bar" },
                },
                SecretEncoded = true,
            },
            ClientMetadata =
            {
                { "foo", "zoo" },
            }
        });
    }
}
