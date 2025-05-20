using CISO.AuditService.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using STZ.Shared.Bases;

namespace CISO.AuditService.Access.DbContext;

public class AuditServiceContext : DbContextBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuditServiceContext> _logger;
    private readonly IHttpContextAccessor? _httpContextAccessor;

    public DbSet<Regulation> Regulations { get; set; }
    public DbSet<RegulationSection> RegulationSections { get; set; }
    public DbSet<RegulationAttachment> RegulationAttachments { get; set; }
    public DbSet<Requirement> Requirements { get; set; }
    public DbSet<RequirementAttachment> RequirementAttachments { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<RequirementLanguage> RequirementLanguages { get; set; }
    
    public AuditServiceContext(IConfiguration configuration, ILogger<AuditServiceContext> logger, IHttpContextAccessor? httpContextAccessor = null) : base(configuration)
    {
        _configuration = configuration;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
}