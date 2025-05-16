using CISO.AuditService.Access.DbContext;
using CISO.AuditService.Shared.Entities;
using STZ.Shared.Bases;

namespace CISO.AuditService.Backend.Controllers;

public class RegulationSectionsController : StzControllerBase<RegulationSection>
{
    private readonly ILogger<RegulationSectionsController> _logger;
    private readonly AuditServiceContext _context;

    public RegulationSectionsController(ILogger<RegulationSectionsController> logger, AuditServiceContext context) : base(logger, context)
    {
        _logger = logger;
        _context = context;
    }
}