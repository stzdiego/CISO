using CISO.CertificationService.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using STZ.Shared.Bases;

namespace CISO.CertificationService.Access.DbContext;

public class CertificationServiceContext : DbContextBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<CertificationServiceContext> _logger;
    private readonly IHttpContextAccessor? _httpContextAccessor;
    
    public DbSet<RegulationCompany> RegulationCompanies { get; set; }
    public DbSet<Trace> Traces { get; set; }
    
    public CertificationServiceContext(IConfiguration configuration, ILogger<CertificationServiceContext> logger, IHttpContextAccessor? httpContextAccessor = null) : base(configuration)
    {
        _configuration = configuration;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
}