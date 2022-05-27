using System.Security.Claims;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Web.Models;

namespace SampleApp.Web.Controllers;

public class AccountController : Controller
{
    private readonly string _redirectUri;

    public AccountController()
    {
        _redirectUri = "/";
    }

    public async Task Login()
    {
        var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
            .WithRedirectUri(_redirectUri)
            .Build();

        await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme,
            authenticationProperties);
    }

    [Authorize]
    public async Task Logout()
    {
        var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
            .WithRedirectUri(Url.ActionLink("Index", "Home") ?? string.Empty)
            .Build();

        await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme,
            authenticationProperties);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    [Authorize]
    public Task<IActionResult> Profile()
    {
        return Task.FromResult<IActionResult>(View(new UserProfileViewModel()
        {
            Id = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value,
            Name = User.Identity.Name,
            EmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
            ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value
        }));
    }

    [Authorize]
    public IActionResult Claims()
    {
        return View();
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}
