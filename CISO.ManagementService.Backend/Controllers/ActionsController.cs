using CISO.ManagementService.Access.DbContext;
using Microsoft.AspNetCore.Mvc;
using STZ.Shared.Bases;
using Action = CISO.ManagementService.Shared.Entities.Action;

namespace CISO.ManagementService.Backend.Controllers;

public class ActionsController : StzControllerBase<Action>
{
    private readonly ILogger<ActionsController> _logger;
    private readonly ManagementServiceContext _context;

    public ActionsController(ILogger<ActionsController> logger, ManagementServiceContext context) : base(logger, context)
    {
        _logger = logger;
        _context = context;
    }
}