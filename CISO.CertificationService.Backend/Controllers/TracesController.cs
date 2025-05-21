
using CISO.AuditService.Shared.Entities;
using CISO.CertificationService.Access.DbContext;
using CISO.CertificationService.Shared.Dtos;
using CISO.CertificationService.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STZ.Shared.Bases;
using STZ.Shared.Entities;

namespace CISO.CertificationService.Backend.Controllers;

public class TracesController : StzControllerBase<Trace>
{
    private readonly ILogger<TracesController> _logger;
    private readonly CertificationServiceContext _context;

    public TracesController(ILogger<TracesController> logger, CertificationServiceContext context) : base(logger, context)
    {
        _logger = logger;
        _context = context;
    }
    
    [HttpPost]
    [Route("update-traces")]
    public async Task<IActionResult> UpdateTracesByRegulation([FromBody] UpdateTracesRequest request)
    {
        try
        {
            var company = request.Company;
            var user = request.User;
            var items = request.Items.Where(x => x != null).ToList();

            // Trazas existentes para la compañía
            var existingTraces = await _context.Traces
                .Where(t => t.CompanyId == company.Id && !t.IsDeleted)
                .ToListAsync();

            var newRequirementIds = items.Select(r => r.Id).ToHashSet();
            var existingRequirementIds = existingTraces.Select(t => t.RequirementId).ToHashSet();

            // Crear nuevas trazas para requerimientos que no existan
            var toAdd = items.Where(r => !existingRequirementIds.Contains(r.Id)).ToList();
            foreach (var req in toAdd)
            {
                var trace = new Trace
                {
                    Id = Guid.NewGuid(),
                    CompanyId = company.Id,
                    UserId = user.Id,
                    RequirementId = req.Id,
                    Description = req.Title
                };
                _context.Traces.Add(trace);
            }

            // Eliminar trazas que ya no están en la lista nueva
            var toRemove = existingTraces.Where(t => !newRequirementIds.Contains(t.RequirementId)).ToList();
            _context.Traces.RemoveRange(toRemove);

            await _context.SaveChangesAsync();
            return Ok("Traces updated successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating traces");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    [Route("get-traces")]
    public async Task<IActionResult> GetTracesByCompany([FromBody] Guid companyId)
    {
        try
        {
            var traces = await _context.Traces.Where(x => x.CompanyId == companyId && !x.IsDeleted)
                .ToListAsync();

            return Ok(traces);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting traces");
            return StatusCode(500, "Internal server error");
        }
    }
}