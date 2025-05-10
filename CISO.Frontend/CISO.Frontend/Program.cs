using Auth0.AspNetCore.Authentication;
using CISO.Frontend.Components;
using CISO.Frontend.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using MudBlazor.Services;
using STZ.Frontend.Authorization;
using STZ.Frontend.Configuration;

var builder = WebApplication.CreateBuilder(args);

// STZ Framework
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddSTZFrontendServices(builder.Configuration);
builder.Services.STZAuth0(builder.Configuration);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(RazorAssemblyConfiguration.GetAssembliesWithRazorPages(typeof(App).Assembly));

app.Run();