using CISO.ManagementService.Access.DbContext;
using CISO.ManagementService.Shared.Entities;
using STZ.Shared.Bases;

namespace CISO.ManagementService.Backend.Controllers;

public class UserRolesController : StzControllerBase<UserRole>
{
    private readonly ILogger<UserRolesController> _logger;
    private readonly ManagementServiceContext _context;

    public UserRolesController(ILogger<UserRolesController> logger, ManagementServiceContext context) : base(logger, context)
    {
        _logger = logger;
        _context = context;
    }
}