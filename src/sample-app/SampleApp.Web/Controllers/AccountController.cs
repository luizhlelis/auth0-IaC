using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Web.Models;

namespace SampleApp.Web.Controllers;

public class AccountController : Controller
{
    private readonly string? _redirectUri;

    public AccountController()
    {
        _redirectUri = "/";
    }

    public async Task Login()
    {
        await HttpContext.ChallengeAsync("Auth0",
            new AuthenticationProperties {RedirectUri = _redirectUri});
    }

    [Authorize]
    public async Task Logout()
    {
        var authenticationProperties =
            new AuthenticationProperties {RedirectUri = Url.ActionLink("Index", "Home")};

        await HttpContext.SignOutAsync("Auth0", authenticationProperties);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    [Authorize]
    public async Task<IActionResult> Profile()
    {
        return View(new UserProfileViewModel()
        {
            Id = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value,
            Name = User.Identity.Name,
            EmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
            ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value
        });
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
