using CISO.AuditService.Access.DbContext;
using CISO.AuditService.Shared.Entities;
using STZ.Shared.Bases;

namespace CISO.AuditService.Backend.Controllers;

public class LanguagesController : StzControllerBase<Language>
{
    private readonly ILogger<LanguagesController> _logger;
    private readonly AuditServiceContext _context;
    
    public LanguagesController(ILogger<LanguagesController> logger, AuditServiceContext context) : base(logger, context)
    {
        _logger = logger;
        _context = context;
    }
}