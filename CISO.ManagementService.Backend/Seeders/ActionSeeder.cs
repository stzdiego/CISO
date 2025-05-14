using CISO.ManagementService.Access.DbContext;
using Microsoft.EntityFrameworkCore.DynamicLinq;
using STZ.Backend.Bases;
using Action = CISO.ManagementService.Shared.Entities.Action;

namespace CISO.ManagementService.Backend.Seeders;

public class ActionSeeder : IDataSeeder
{
    public async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ManagementServiceContext>();
        var isAny = await context.Actions.AnyAsync();

        if (!isAny)
        {
            await context.Actions.AddRangeAsync(
                new Action { Name = "View"},
                new Action { Name = "Add"},
                new Action { Name = "Edit"},
                new Action { Name = "Delete"},
                new Action { Name = "Export"}
            );
            await context.SaveChangesAsync();
        }
    }
}