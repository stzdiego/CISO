using CISO.ManagementService.Access.DbContext;
using CISO.ManagementService.Shared.Entities;
using STZ.Shared.Bases;

namespace CISO.ManagementService.Backend.Controllers;

public class UsersController : StzControllerBase<User>
{
    private readonly ILogger<UsersController> _logger;
    private readonly ManagementServiceContext _context;

    public UsersController(ILogger<UsersController> logger, ManagementServiceContext context) : base(logger, context)
    {
        _logger = logger;
        _context = context;
    }
}