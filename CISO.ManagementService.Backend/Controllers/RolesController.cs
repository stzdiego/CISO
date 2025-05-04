using CISO.ManagementService.Access.DbContext;
using CISO.ManagementService.Shared.Entities;
using STZ.Shared.Bases;

namespace CISO.ManagementService.Backend.Controllers;

public class RolesController : StzControllerBase<Role>
{
    private readonly ILogger<RolesController> _logger;
    private readonly ManagementServiceContext _context;

    public RolesController(ILogger<RolesController> logger, ManagementServiceContext context) : base(logger, context)
    {
        _logger = logger;
        _context = context;
    }
}