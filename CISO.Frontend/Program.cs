using CISO.Frontend.Components;
using MudBlazor.Services;
using STZ.Frontend.Configuration;
using STZ.Frontend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSTZFrontendServices(builder.Configuration);
builder.Services.AddMudServices();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

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
    .AddAdditionalAssemblies(RazorAssemblyConfiguration.GetAssembliesWithRazorPages(typeof(App).Assembly));

app.Run();