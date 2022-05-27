using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace SampleApp.Web.AuthenticationHandlers;

public static class OnAuthorizationCodeReceived
{
    public static Task Handle(AuthorizationCodeReceivedContext context)
    {
        return Task.CompletedTask;
    }
}
