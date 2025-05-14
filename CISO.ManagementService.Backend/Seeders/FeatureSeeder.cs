using CISO.ManagementService.Access.DbContext;
using CISO.ManagementService.Shared.Entities;
using Microsoft.EntityFrameworkCore.DynamicLinq;
using STZ.Backend.Bases;

namespace CISO.ManagementService.Backend.Seeders;

public class FeatureSeeder : IDataSeeder
{
    public async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ManagementServiceContext>();
        var isAny = await context.Features.AnyAsync();

        if (!isAny)
        {
            await context.Features.AddRangeAsync(
                new Feature { Name = "Actions" },
                new Feature { Name = "Features" },
                new Feature { Name = "Permissions" },
                new Feature { Name = "Users" },
                new Feature { Name = "Roles" },
                new Feature { Name = "UsersRoles" },
                new Feature { Name = "Companies" },
                new Feature { Name = "Cultures" },
                new Feature { Name = "CultureResources" },
                new Feature { Name = "Resources" }
            );
            await context.SaveChangesAsync();
        }
    }
}