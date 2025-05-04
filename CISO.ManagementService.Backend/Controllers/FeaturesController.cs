using CISO.ManagementService.Access.DbContext;
using CISO.ManagementService.Shared.Entities;
using STZ.Shared.Bases;

namespace CISO.ManagementService.Backend.Controllers;

public class FeaturesController : StzControllerBase<Feature>
{
    private readonly ILogger<FeaturesController> _logger;
    private readonly ManagementServiceContext _context;

    public FeaturesController(ILogger<FeaturesController> logger, ManagementServiceContext context) : base(logger, context)
    {
        _logger = logger;
        _context = context;
    }
}