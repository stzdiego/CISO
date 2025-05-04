using CISO.ManagementService.Access.DbContext;
using CISO.ManagementService.Shared.Entities;
using STZ.Shared.Bases;

namespace CISO.ManagementService.Backend.Controllers;

public class PermissionsController : StzControllerBase<Permission>
{
    private readonly ILogger<PermissionsController> _logger;
    private readonly ManagementServiceContext _context;

    public PermissionsController(ILogger<PermissionsController> logger, ManagementServiceContext context) : base(logger, context)
    {
        _logger = logger;
        _context = context;
    }
}