using CISO.EvidenceService.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using STZ.Shared.Bases;
namespace CISO.EvidenceService.Access.DbContext;

public class EvidenceServiceContext : DbContextBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EvidenceServiceContext> _logger;
    private readonly IHttpContextAccessor? _httpContextAccessor;
    
    public DbSet<Attachment> Attachments { get; set; }
    
    public EvidenceServiceContext(IConfiguration configuration, ILogger<EvidenceServiceContext> logger, IHttpContextAccessor? httpContextAccessor = null) : base(configuration)
    {
        _configuration = configuration;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
}