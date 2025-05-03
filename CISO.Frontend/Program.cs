using System.Reflection;
using CISO.Frontend.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor.Services;
using STZ.Frontend.Configuration;
using STZ.Frontend.Services;

var builder = WebApplication.CreateBuilder(args);

// STZ Framework
builder.Services.AddSTZFrontendServices(builder.Configuration);
builder.Services.AddMudServices();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

Assembly[] GetAssembliesWithRazorPages()
{
    var mainAssembly = typeof(App).Assembly;

    return AppDomain.CurrentDomain.GetAssemblies()
        .Where(asm => asm != mainAssembly)
        .Where(asm => asm.IsDefined(typeof(AssemblyCompanyAttribute), false))
        .Where(asm => asm.GetTypes().Any(t =>
            typeof(ComponentBase).IsAssignableFrom(t) &&
            t.GetCustomAttributes(typeof(RouteAttribute), inherit: true).Any()))
        .ToArray();
}

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(GetAssembliesWithRazorPages());
app.Run();