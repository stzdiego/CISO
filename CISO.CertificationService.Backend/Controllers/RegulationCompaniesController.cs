using CISO.CertificationService.Access.DbContext;
using CISO.CertificationService.Shared.Entities;
using STZ.Shared.Bases;

namespace CISO.CertificationService.Backend.Controllers;

public class RegulationCompaniesController : StzControllerBase<RegulationCompany>
{
    private readonly ILogger<RegulationCompaniesController> _logger;
    private readonly CertificationServiceContext _context;

    public RegulationCompaniesController(ILogger<RegulationCompaniesController> logger, CertificationServiceContext context) : base(logger, context)
    {
        _logger = logger;
        _context = context;
    }
}