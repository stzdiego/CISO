using CISO.ManagementService.Access.DbContext;
using CISO.ManagementService.Shared.Entities;
using STZ.Shared.Bases;

namespace CISO.ManagementService.Backend.Controllers;

public class PermissionRolesController : StzControllerBase<PermissionRole>
{
    private readonly ILogger<PermissionRolesController> _logger;
    private readonly ManagementServiceContext _context;

    public PermissionRolesController(ILogger<PermissionRolesController> logger, ManagementServiceContext context) : base(logger, context)
    {
        _logger = logger;
        _context = context;
    }
}