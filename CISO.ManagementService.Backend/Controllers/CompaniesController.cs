using CISO.ManagementService.Access.DbContext;
using CISO.ManagementService.Shared.Entities;
using STZ.Shared.Bases;

namespace CISO.ManagementService.Backend.Controllers;

public class CompaniesController: StzControllerBase<Company>
{
    private readonly ILogger<CompaniesController> _logger;
    private readonly ManagementServiceContext _context;

    public CompaniesController(ILogger<CompaniesController> logger, ManagementServiceContext context) : base(logger, context)
    {
        _logger = logger;
        _context = context;
    }
}