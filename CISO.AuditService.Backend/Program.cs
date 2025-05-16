using CISO.AuditService.Access.DbContext;
using Microsoft.EntityFrameworkCore;
using STZ.Backend.Configuration;

var builder = WebApplication.CreateBuilder(args);

// STZFramework configuration
builder.Services.AddSTZBackendServices(builder.Configuration);

// DbContext configuration
builder.Services.AddDbContext<AuditServiceContext>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AuditServiceContext>();
    await dbContext.Database.MigrateAsync(); // Apply migrations
    await scope.ServiceProvider.ExecuteSeedersAsync(); // Execute seeders
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();