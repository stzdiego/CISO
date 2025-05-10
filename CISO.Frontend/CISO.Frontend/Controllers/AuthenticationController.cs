using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace CISO.Frontend.Controllers;

[Route("authentication")]
public class AuthController : Controller
{
    private readonly IConfiguration Configuration;
    
    public AuthController(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    [HttpGet("login")]
    public IActionResult Login([FromQuery] string? redirectUri = "/")
    {
        var authProperties = new AuthenticationProperties { RedirectUri = redirectUri ?? "/" };
        return Challenge(authProperties, "Auth0");
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        // Cerrar sesión local
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        // Redirigir al logout de Auth0
        var returnTo = Url.Action("Index", "Home", null, Request.Scheme) ?? $"{Request.Scheme}://{Request.Host}/";
        var logoutUrl = $"https://{Configuration["Auth0:Domain"]}/v2/logout?client_id={Configuration["Auth0:ClientId"]}&returnTo={Uri.EscapeDataString(returnTo!)}";

        return Redirect(logoutUrl);
    }
}