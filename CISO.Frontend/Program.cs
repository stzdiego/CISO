using CISO.Frontend.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using MudBlazor.Services;
using STZ.Frontend.Configuration;
using STZ.Frontend.Services;

var builder = WebApplication.CreateBuilder(args);

// STZ Framework
builder.Services.AddSTZFrontendServices(builder.Configuration);
builder.Services.AddMudServices();

// Auth0

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(RazorAssemblyConfiguration.GetAssembliesWithRazorPages(typeof(App).Assembly));

app.Run();