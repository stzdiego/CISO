using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using STZ.Shared.Bases;
using CISO.ManagementService.Shared.Entities;
using Action = CISO.ManagementService.Shared.Entities.Action;

namespace CISO.ManagementService.Access.DbContext;

public class ManagementServiceContext : DbContextBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ManagementServiceContext> _logger;
    private readonly IHttpContextAccessor? _httpContextAccessor;
    
    public DbSet<Action> Actions { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<PermissionRole> PermissionRoles { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    
    public ManagementServiceContext(IConfiguration configuration, ILogger<ManagementServiceContext> logger, IHttpContextAccessor? httpContextAccessor = null) : base(configuration)
    {
        _configuration = configuration;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
}