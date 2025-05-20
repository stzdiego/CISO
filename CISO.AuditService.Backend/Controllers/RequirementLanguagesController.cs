using CISO.AuditService.Access.DbContext;
using CISO.AuditService.Shared.Entities;
using STZ.Shared.Bases;

namespace CISO.AuditService.Backend.Controllers;

public class RequirementLanguagesController: StzControllerBase<RequirementLanguage>
{
    private readonly ILogger<RequirementLanguagesController> _logger;
    private readonly AuditServiceContext _context;

    public RequirementLanguagesController(ILogger<RequirementLanguagesController> logger, AuditServiceContext context) 
        : base(logger, context)
    {
        _logger = logger;
        _context = context;
    }
}